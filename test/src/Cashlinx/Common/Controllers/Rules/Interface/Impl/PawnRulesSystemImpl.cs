using System;
using System.Collections.Generic;
using Common.Controllers.Rules.Data;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Type;

namespace Common.Controllers.Rules.Interface.Impl
{
    public class PawnRulesSystemImpl : PawnRulesSystemInterface
    {
        private Dictionary<string, PawnRules> pawnRules = null;
        private SiteId currentSite;
        private Dictionary<string, BusinessRuleVO> businessRules = null;
        private Dictionary<string, Dictionary<string, BusinessRuleVO>> mergedBusinessRules = null;
        private Dictionary<object, PairType<SiteId, bool>> beginBlockRegistry;
        private List<BusinessRuleComponentVO> allComponents = new List<BusinessRuleComponentVO>();

        public PawnRulesSystemImpl()
        {            
            this.pawnRules = new Dictionary<string, PawnRules>();
            this.businessRules = new Dictionary<string, BusinessRuleVO>();
            this.mergedBusinessRules = new Dictionary<string, Dictionary<string, BusinessRuleVO>>();
            this.beginBlockRegistry = new Dictionary<object, PairType<SiteId, bool>>();
        }

        /// <summary>
        /// Old update business rule component method
        /// - Retaining in code base for reference purposes
        /// - Marked as obsolete for now
        /// </summary>
        /// <param name="busRules"></param>
        /// <param name="bvoHierarchy"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [Obsolete]
        private bool updateBusinessRuleComponent(List<string> busRules, ref Dictionary<string, BusinessRuleVO> bvoHierarchy, ref PawnRule p)
        {
            if (p == null || busRules == null || bvoHierarchy == null)
            {
                return (false);
            }
            string pawnRuleCompKey = p.RuleKey;
            foreach(string bRule in busRules)
            {
                string curRule = bRule;
                BusinessRuleVO curBR = bvoHierarchy[curRule];
                if (curBR == null || !curBR.ContainsKey(pawnRuleCompKey))
                    continue;

                BusinessRuleComponentVO bvo = curBR[pawnRuleCompKey];
                bvo.FromDate = p.FromDate;
                bvo.ToDate = p.ToDate;
                //bvo.CheckLoanRange = p.CheckLoanRange;
                //bvo.CheckLoanAmount = p.CheckLoanAmount;
                bvo.Alias = p.Alias;

                switch (bvo.ValueType)
                {
                    case BusinessRuleComponentVO.RuleValueType.FEES:
                        bvo.FeesValue.Alias.Code = p.Alias;
                        bvo.FeesValue.ParamKeyCode = p.RuleKey;
                        bvo.FeesValue.Value = p.RuleValue;
                        break;
                    case BusinessRuleComponentVO.RuleValueType.INTEREST:
                        bvo.InterestValue.Alias.Code = p.Alias;
                        bvo.InterestValue.InterestAmount = 0.0M;
                        bvo.InterestValue.InterestRate = decimal.Parse(p.RuleValue);
                        bvo.InterestValue.MinAmount = p.LoanAmountMin;
                        bvo.InterestValue.MaxAmount = p.LoanAmountMax;
                        break;
                    case BusinessRuleComponentVO.RuleValueType.PARAM:
                        bvo.ParamValue.Alias.Code = p.Alias;
                        bvo.ParamValue.Cacheable = false;
                        bvo.ParamValue.Company = "1";
                        bvo.ParamValue.Code = p.RuleKey;
                        bvo.ParamValue.Value = p.RuleValue;
                        break;
                }
            }
            return (true);
        }

        #region Implementation of PawnRulesSystemInterface
        /// <summary>
        /// Begin site method - call prior to getParameters
        /// </summary>
        /// <param name="caller">Calling class object</param>
        /// <param name="siteId">SiteId specifier object</param>
        /// <param name="ruleIds">List of Business rule strings</param>
        /// <param name="rules">List of BusinessRuleVO objects - Output param</param>
        /// <returns>PawnRulesSystemReturnCode</returns>
        public PawnRulesSystemReturnCode beginSite(object caller, SiteId siteId, List<string> ruleIds, out Dictionary<string, BusinessRuleVO> rules)
        {
            rules = new Dictionary<string, BusinessRuleVO>();

            //Validate input
            if (caller == null || CollectionUtilities.isEmpty(ruleIds))
            {
                return
                    (new PawnRulesSystemReturnCode(
                        PawnRulesSystemReturnCode.Code.ERROR,
                        "Input values to beginSite are invalid."));
            }
            //Check if this object is already using the pawn rules system 
            if (this.beginBlockRegistry.ContainsKey(caller))
            {
                PairType<SiteId, bool> curSiteBegin = this.beginBlockRegistry[caller];
                if (curSiteBegin.Right)
                {
                    return
                        (new PawnRulesSystemReturnCode(
                            PawnRulesSystemReturnCode.Code.WARNING,
                            "Already in an active begin block for this site and this caller parent object."));
                }
                curSiteBegin.Right = true;
                curSiteBegin.Left = siteId;
            }
            else
            {
                this.beginBlockRegistry.Add(caller, new PairType<SiteId, bool>(siteId, true));
            }

            //Set site id
            this.currentSite = siteId;
            //Change last parameter to true if you want to save the rules file to the disk after it is loaded
            //into the rules engine data structure
            //rules = RulesHelper.BuildBusinessRules(siteId, true);
            rules = RulesHelper.BuildBusinessRules(siteId);

            return (new PawnRulesSystemReturnCode(PawnRulesSystemReturnCode.Code.SUCCESS));
        }

        /// <summary>
        /// End site method - call after done with getParameters
        /// </summary>
        /// <param name="caller">Calling class object</param>
        /// <param name="siteId">SiteId specifier object</param>
        /// <returns>PawnRulesSystemReturnCode</returns>
        public PawnRulesSystemReturnCode endSite(object caller, SiteId siteId)
        {
            if (caller == null || siteId == null)
            {
                return (new PawnRulesSystemReturnCode(PawnRulesSystemReturnCode.Code.ERROR, "Inputs to endSite are invalid"));
            }

            if (this.beginBlockRegistry.ContainsKey(caller))
            {
                PairType<SiteId, bool> curBlock = this.beginBlockRegistry[caller];
                if (curBlock.Left == siteId)
                {
                    if (curBlock.Right)
                    {
                        curBlock.Right = false;
                        curBlock.Left = null;
                    }
                }
                return (new PawnRulesSystemReturnCode(PawnRulesSystemReturnCode.Code.SUCCESS));
            }
            return (new PawnRulesSystemReturnCode(PawnRulesSystemReturnCode.Code.WARNING, "No block found for given site id / caller pair"));
        }

        /// <summary>
        /// Get parameters method - call between beginSite and endSite
        /// </summary>
        /// <param name="caller">Calling class object</param>
        /// <param name="siteId">SiteId specifier object</param>
        /// <param name="rules" >List of BusinessRuleVO objects - </param>
        /// <returns></returns>
        public PawnRulesSystemReturnCode getParameters(
            object caller,
            SiteId siteId,
            ref Dictionary<string, BusinessRuleVO> rules)
        {
            return (new PawnRulesSystemReturnCode(PawnRulesSystemReturnCode.Code.SUCCESS));
        }

        /// <summary>
        /// Get parameter method - call between beginSite and endSite
        /// </summary>
        /// <param name="caller">Calling class object</param>
        /// <param name="siteId">SiteId specifier object</param>
        /// <param name="paramKey">Non-business rule param key</param>
        /// <param name="values">List of param values with associated aliases</param>
        /// <returns></returns>
        public PawnRulesSystemReturnCode getParameter(object caller, SiteId siteId, string paramKey, ref Dictionary<string, string> values)
        {
            return (new PawnRulesSystemReturnCode(PawnRulesSystemReturnCode.Code.SUCCESS));
        }
        #endregion
    }
}
