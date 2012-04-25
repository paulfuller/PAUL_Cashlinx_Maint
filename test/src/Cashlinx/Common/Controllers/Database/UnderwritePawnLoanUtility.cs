using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common.Controllers.Application;
using Common.Controllers.Rules.Interface;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Config;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility;

namespace Common.Controllers.Database
{
    public class UnderwritePawnLoanUtility
    {
        private const string YES_VALUE = "Y";

        public UnderwritePawnLoanUtility(DesktopSession desktopSession)
        {
            DesktopSession = desktopSession;
        }

        public DesktopSession DesktopSession { get; private set; }

        public void RunUWP(SiteId newPawnItem)
        {
            this.PawnLoanVO = new UnderwritePawnLoanVO();
            PawnRulesSystemInterface prs = DesktopSession.PawnRulesSys;
            Dictionary<string, decimal> feeDictionary = new Dictionary<string, decimal>();
            List<String> bRules = new List<String>();
            bRules.Add("PWN_BR-002");
            bRules.Add("PWN_BR-017");
            bRules.Add("PWN_BR-020");
            bRules.Add("PWN_BR-021");
            bRules.Add("PWN_BR-028");
            bRules.Add("PWN_BR-032");
            bRules.Add("PWN_BR-048");

            Dictionary<string, BusinessRuleVO> rules;
            prs.beginSite(prs, newPawnItem, bRules, out rules);
            prs.getParameters(prs, newPawnItem, ref rules);
            prs.endSite(prs, newPawnItem);

            BusinessRuleVO businessRules;
            businessRules = null;
            string componentValue = "";
            decimal CL_PWN_0008_INTRATE = 0.0M;
            decimal loanAmount = newPawnItem.LoanAmount;
            decimal InterestAmountForLoanTermCycle = 0.0M;
            StringBuilder sbUpw = new StringBuilder();

            decimal CL_PWN_0018_SETUPFEEAMT = 0.0M;
            decimal CL_PWN_0022_CITYFEEAMT = 0.0M;

            //Add the interest rate grandfather rule component to the fee dictionary
            businessRules = rules["PWN_BR-048"];
            if (businessRules.getComponentValue("CL_PWN_0045_RENEWFINANCESAME", ref componentValue))
            {
                if (!string.IsNullOrEmpty(componentValue))
                {
                    if (componentValue.Equals(YES_VALUE, StringComparison.OrdinalIgnoreCase))
                    {
                        feeDictionary.Add("CL_PWN_0045_RENEWFINANCESAME", 1.0M);
                    }
                    else
                    {
                        feeDictionary.Add("CL_PWN_0045_RENEWFINANCESAME", 0.0M);
                    }
                }
                else
                {
                    feeDictionary.Add("CL_PWN_0045_RENEWFINANCESAME", 0.0M);
                }
            }

            businessRules = rules["PWN_BR-002"];
            List<decimal> tieredInterestValues = new List<decimal>();
            //CL_PWN_0008_INTRATE-002
            if (newPawnItem.State == States.Texas || newPawnItem.State == States.Ohio)
            {
                var intValues = businessRules.Keys.Where(key => key.Contains("CL_PWN_0008_INTRATE")).ToList();
                foreach (string str in intValues)
                {
                    BusinessRuleComponentVO bVo = new BusinessRuleComponentVO();
                    var getInterestValue = businessRules.getComponent(str, ref bVo);
                    if (getInterestValue)
                    {
                        decimal minLoan = bVo.InterestValue.MinAmount;
                        decimal maxLoan = bVo.InterestValue.MaxAmount;
                        if (loanAmount >= minLoan && loanAmount <= maxLoan)
                        {
                            if (businessRules.getComponentValue(str, ref componentValue))
                            {
                                try
                                {
                                    CL_PWN_0008_INTRATE = decimal.Parse(componentValue);
                                    this.PawnLoanVO.APR = CL_PWN_0008_INTRATE;

                                    InterestAmountForLoanTermCycle = loanAmount * ((CL_PWN_0008_INTRATE / 12) / 100);
                                    break;
                                }
                                catch (Exception exc)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                                    return;
                                }
                            }
                            else
                            {
                                sbUpw.AppendLine("CL_PWN_0008_INTRATE Not found");
                            }

                        }
                    }
                }

            }
            else if (newPawnItem.State == States.Oklahoma)
            {
                decimal tieredInterestValue = 0.0m;
                //Get all the interest tiers
                var intValues = businessRules.Keys.Where(key => key.Contains("CL_PWN_0008_INTRATE")).ToList();
                decimal loanAmountCopy = loanAmount;
                foreach (string str in intValues)
                {
                    BusinessRuleComponentVO bVo=new BusinessRuleComponentVO();
                    if (loanAmountCopy > 0)
                    {
                        var getInterestValue = businessRules.getComponent(str, ref bVo);
                        if (getInterestValue)
                        {
                            decimal maxLoan = bVo.InterestValue.MaxAmount;
                            decimal minLoan = bVo.InterestValue.MinAmount;
                            decimal tieredLoanAmt = maxLoan-minLoan;
                            if (loanAmountCopy >= tieredLoanAmt)
                            {
                                businessRules.getComponentValue(str, ref componentValue);
                                tieredInterestValue = Math.Round(tieredLoanAmt * (decimal.Parse(componentValue) / 100), 2);
                                tieredInterestValues.Add(tieredInterestValue);
                                loanAmountCopy -= tieredLoanAmt;
                            }
                            else
                            {
                                tieredLoanAmt = loanAmountCopy;
                                businessRules.getComponentValue(str, ref componentValue);
                                tieredInterestValue = Math.Round(tieredLoanAmt * (decimal.Parse(componentValue) / 100), 2);
                                tieredInterestValues.Add(tieredInterestValue);
                                loanAmountCopy = 0;

                            }
                        }
                    }
                    else
                        break;

                }
                
                this.PawnLoanVO.APR = (tieredInterestValues.Sum() / loanAmount) * 100;
                InterestAmountForLoanTermCycle = loanAmount * ((this.PawnLoanVO.APR / 12) / 100);
                decimal minIntAmount=0.0m;
                //Added ythe logic below to account for the minimum interest amount for the state
                if (businessRules.getComponentValue("CL_PWN_0013_MININTAMT", ref componentValue))
                {
                    try
                    {
                        minIntAmount = decimal.Parse(componentValue);
                        
                    }
                    catch (Exception exc)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                        
                    }
                }
                //If the interest amount comes to less than minimum interest amount
                //set the interest to the minimum interest amount
                //and set the APR appropriately
                if (InterestAmountForLoanTermCycle < minIntAmount)
                {
                    InterestAmountForLoanTermCycle = minIntAmount;
                    this.PawnLoanVO.APR = (minIntAmount / loanAmount) * 100 * 12;
                }
                

            }


            decimal ServiceChargeAmountForLoanTermCycle = 0.0M;
            decimal CL_PWN_0010_SVCCHRGRATE = 0.0M;
            decimal CL_PWN_0030_STRGFEE = 0.0M;
            decimal CL_PWN_0044_TICKETFEE = 0.0M;
            decimal CL_PWN_0026_FIREARMFEEAMT = 0.0M;
            string CL_PWN_0037_STRGFEEALLWD = "";
            decimal CL_PWN_0190_GUNLOCKFEE = 0.0M;

            //CL_PWN_0010_SVCCHRGRATE-002
            if (businessRules.getComponentValue("CL_PWN_0010_SVCCHRGRATE", ref componentValue))
            {
                try
                {
                    CL_PWN_0010_SVCCHRGRATE = decimal.Parse(componentValue);
                    ServiceChargeAmountForLoanTermCycle = loanAmount * CL_PWN_0010_SVCCHRGRATE;
                    feeDictionary.Add("CL_PWN_0010_SVCCHRGRATE", CL_PWN_0010_SVCCHRGRATE);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0010_SVCCHRGRATE Not found");
            }

            //CL_PWN_0018_SETUPFEEAMT-002
            if (businessRules.getComponentValue("CL_PWN_0018_SETUPFEEAMT", ref componentValue))
            {
                try
                {
                    CL_PWN_0018_SETUPFEEAMT = decimal.Parse(componentValue);
                    feeDictionary.Add("CL_PWN_0018_SETUPFEEAMT", CL_PWN_0018_SETUPFEEAMT);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0018_SETUPFEEAMT Not found");
            }

            //CL_PWN_0022_CITYFEEAMT-002
            if (businessRules.getComponentValue("CL_PWN_0022_CITYFEEAMT", ref componentValue))
            {
                try
                {
                    CL_PWN_0022_CITYFEEAMT = decimal.Parse(componentValue);
                    feeDictionary.Add("CL_PWN_0022_CITYFEEAMT", CL_PWN_0022_CITYFEEAMT);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0022_CITYFEEAMT Not found");
            }

            //CL_PWN_0037_STRGFEEALLWD-002
            if (businessRules.getComponentValue("CL_PWN_0037_STRGFEEALLWD", ref componentValue))
            {
                try
                {
                    CL_PWN_0037_STRGFEEALLWD = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0037_STRGFEEALLWD Not found");
            }

            //CL_PWN_0030_STRGFEE-002
            if (businessRules.getComponentValue("CL_PWN_0030_STRGFEE", ref componentValue))
            {
                try
                {
                    CL_PWN_0030_STRGFEE = decimal.Parse(componentValue);

                    if (CL_PWN_0037_STRGFEEALLWD == YES_VALUE)
                    {
                        feeDictionary.Add("CL_PWN_0030_STRGFEE", CL_PWN_0030_STRGFEE);
                    }
                    else
                    {
                        feeDictionary.Add("CL_PWN_0030_STRGFEE", 0.0M);
                    }
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0030_STRGFEE Not found");
            }

            //CL_PWN_0026_FIREARMFEEAMT-002
            if (businessRules.getComponentValue("CL_PWN_0026_FIREARMFEEAMT", ref componentValue))
            {
                try
                {
                    CL_PWN_0026_FIREARMFEEAMT = decimal.Parse(componentValue);
                    feeDictionary.Add("CL_PWN_0026_FIREARMFEEAMT", CL_PWN_0026_FIREARMFEEAMT);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0026_FIREARMFEEAMT Not found");
            }

            businessRules = rules["PWN_BR-017"];
            //CL_PWN_0190_GUNLOCKFEE
            if (businessRules.getComponentValue("CL_PWN_0190_GUNLOCKFEE", ref componentValue))
            {
                try
                {
                    CL_PWN_0190_GUNLOCKFEE = decimal.Parse(componentValue);

                    feeDictionary.Add("CL_PWN_0190_GUNLOCKFEE", CL_PWN_0190_GUNLOCKFEE);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0190_GUNLOCKFEE Not found");
            }

            if (feeDictionary.Count > 0)
            {
                this.PawnLoanVO.feeDictionary = feeDictionary;
            }
            else
            {
                FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "No Entries");
            }

            businessRules = rules["PWN_BR-028"];
            decimal InterestAmount = 0.0M;
            decimal ServiceChargeAmount = 0.0M;
            int N = 0;
            int CL_PWN_0060_PYMTPROJCNT = 0;

            //CL_PWN_0060_PYMTPROJCNT-028
            if (businessRules.getComponentValue("CL_PWN_0060_PYMTPROJCNT", ref componentValue))
            {
                try
                {
                    CL_PWN_0060_PYMTPROJCNT = int.Parse(componentValue);
                    if (CL_PWN_0060_PYMTPROJCNT == 0)
                        N = 1;
                    else if (CL_PWN_0060_PYMTPROJCNT > 30 && CL_PWN_0060_PYMTPROJCNT < 61)
                        N = 2;
                    else if (CL_PWN_0060_PYMTPROJCNT > 60 && CL_PWN_0060_PYMTPROJCNT < 91)
                        N = 3;
                    else if (CL_PWN_0060_PYMTPROJCNT > 90 && CL_PWN_0060_PYMTPROJCNT < 121)
                        N = 4;

                    InterestAmount = InterestAmountForLoanTermCycle * N;
                    this.PawnLoanVO.totalFinanceCharge = InterestAmount;
                    ServiceChargeAmount = ServiceChargeAmountForLoanTermCycle * N;
                    this.PawnLoanVO.totalServiceCharge = ServiceChargeAmount;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0060_PYMTPROJCNT Not found");
            }

            //calculation of ProjectedPaymentAmount
            int CL_PWN_0012_SVCCHRGAPP = 0;
            decimal ProjectedPaymentAmount = 0.0M;
            string CL_PWN_0020_INCLDSETUPFEEAPC = "";
            string CL_PWN_0024_INCLDCITYFEEAPC = "";
            string CL_PWN_0032_INCLDSTRGFEEAPC = "";
            string CL_PWN_0039_INCLDTKTFEEAPC = "";
            string CL_PWN_0028_INCLDFIRARMFEEAPC = "";
            int CL_PWN_0019_SETUPFEEAPP = 0;
            int CL_PWN_0023_CTYFEEAPP = 0;
            int CL_PWN_0031_STRGFEEAPP = 0;
            int CL_PWN_0038_TICKETFEEAPP = 0;
            int CL_PWN_0027_FIRARMFEEAPP = 0;

            //all ints
            //CL_PWN_0012_SVCCHRGAPP-028
            if (businessRules.getComponentValue("CL_PWN_0012_SVCCHRGAPP", ref componentValue))
            {
                try
                {
                    CL_PWN_0012_SVCCHRGAPP = int.Parse(componentValue);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0012_SVCCHRGAPP Not found");
            }

            //CL_PWN_0019_SETUPFEEAPP-028
            if (businessRules.getComponentValue("CL_PWN_0019_SETUPFEEAPP", ref componentValue))
            {
                try
                {
                    CL_PWN_0019_SETUPFEEAPP = int.Parse(componentValue);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0019_SETUPFEEAPP Not found");
            }

            //CL_PWN_0023_CTYFEEAPP-028
            if (businessRules.getComponentValue("CL_PWN_0023_CTYFEEAPP", ref componentValue))
            {
                try
                {
                    CL_PWN_0023_CTYFEEAPP = int.Parse(componentValue);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0023_CTYFEEAPP Not found");
            }

            //CL_PWN_0031_STRGFEEAPP-028
            if (businessRules.getComponentValue("CL_PWN_0031_STRGFEEAPP", ref componentValue))
            {
                try
                {
                    CL_PWN_0031_STRGFEEAPP = int.Parse(componentValue);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0031_STRGFEEAPP Not found");
            }

            //CL_PWN_0038_TICKETFEEAPP-028
            if (businessRules.getComponentValue("CL_PWN_0038_TICKETFEEAPP", ref componentValue))
            {
                try
                {
                    CL_PWN_0038_TICKETFEEAPP = int.Parse(componentValue);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0038_TICKETFEEAPP Not found");
            }

            //CL_PWN_0027_FIRARMFEEAPP-028
            if (businessRules.getComponentValue("CL_PWN_0027_FIRARMFEEAPP", ref componentValue))
            {
                try
                {
                    CL_PWN_0027_FIRARMFEEAPP = int.Parse(componentValue);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0027_FIRARMFEEAPP Not found");
            }
            //end ints

            //begin strings
            businessRules = rules["PWN_BR-021"];
            //CL_PWN_0020_INCLDSETUPFEEAPC-021
            if (businessRules.getComponentValue("CL_PWN_0020_INCLDSETUPFEEAPC", ref componentValue))
            {
                try
                {
                    CL_PWN_0020_INCLDSETUPFEEAPC = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0020_INCLDSETUPFEEAPC Not found");
            }

            //CL_PWN_0024_INCLDCITYFEEAPC-021
            if (businessRules.getComponentValue("CL_PWN_0024_INCLDCITYFEEAPC", ref componentValue))
            {
                try
                {
                    CL_PWN_0024_INCLDCITYFEEAPC = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0024_INCLDCITYFEEAPC Not found");
            }

            //CL_PWN_0032_INCLDSTRGFEEAPC-021
            if (businessRules.getComponentValue("CL_PWN_0032_INCLDSTRGFEEAPC", ref componentValue))
            {
                try
                {
                    CL_PWN_0032_INCLDSTRGFEEAPC = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0032_INCLDSTRGFEEAPC Not found");
            }

            //CL_PWN_0039_INCLDTKTFEEAPC-021
            if (businessRules.getComponentValue("CL_PWN_0039_INCLDTKTFEEAPC", ref componentValue))
            {
                try
                {
                    CL_PWN_0039_INCLDTKTFEEAPC = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0039_INCLDTKTFEEAPC Not found");
            }

            //CL_PWN_0028_INCLDFIRARMFEEAPC-021
            if (businessRules.getComponentValue("CL_PWN_0028_INCLDFIRARMFEEAPC", ref componentValue))
            {
                try
                {
                    CL_PWN_0028_INCLDFIRARMFEEAPC = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0028_INCLDFIRARMFEEAPC Not found");
            }

            if (CL_PWN_0012_SVCCHRGAPP == 1)
            {
                ProjectedPaymentAmount = loanAmount + InterestAmount + ServiceChargeAmountForLoanTermCycle;
                this.PawnLoanVO.totalLoanAmount = ProjectedPaymentAmount;
            }
            else
            {
                ProjectedPaymentAmount = loanAmount + InterestAmount + (N * ServiceChargeAmount);
                this.PawnLoanVO.totalLoanAmount = ProjectedPaymentAmount;
            }

            if (CL_PWN_0020_INCLDSETUPFEEAPC.Equals(YES_VALUE))
            {
                if (CL_PWN_0019_SETUPFEEAPP.Equals(1))
                {
                    ProjectedPaymentAmount = loanAmount + InterestAmount + CL_PWN_0018_SETUPFEEAMT;
                    this.PawnLoanVO.totalLoanAmount = ProjectedPaymentAmount;
                }
                else
                {
                    ProjectedPaymentAmount = loanAmount + InterestAmount + (N * CL_PWN_0018_SETUPFEEAMT);
                    this.PawnLoanVO.totalLoanAmount = ProjectedPaymentAmount;
                }
            }

            if (CL_PWN_0024_INCLDCITYFEEAPC.Equals(YES_VALUE))
            {
                if (CL_PWN_0023_CTYFEEAPP.Equals(1))
                {
                    ProjectedPaymentAmount = loanAmount + InterestAmount + CL_PWN_0022_CITYFEEAMT;
                    this.PawnLoanVO.totalLoanAmount = ProjectedPaymentAmount;
                }
                else
                {
                    ProjectedPaymentAmount = loanAmount + InterestAmount + (N * CL_PWN_0022_CITYFEEAMT);
                    this.PawnLoanVO.totalLoanAmount = ProjectedPaymentAmount;
                }
            }

            if (CL_PWN_0032_INCLDSTRGFEEAPC.Equals(YES_VALUE))
            {
                if (CL_PWN_0031_STRGFEEAPP.Equals(1))
                {
                    ProjectedPaymentAmount = loanAmount + InterestAmount + CL_PWN_0030_STRGFEE;
                    this.PawnLoanVO.totalLoanAmount = ProjectedPaymentAmount;
                }
                else
                {
                    ProjectedPaymentAmount = loanAmount + InterestAmount + (N * CL_PWN_0030_STRGFEE);
                    this.PawnLoanVO.totalLoanAmount = ProjectedPaymentAmount;
                }
                this.PawnLoanVO.APR = (((InterestAmount + CL_PWN_0030_STRGFEE) * 12) / loanAmount * 100);
            }

            if (CL_PWN_0039_INCLDTKTFEEAPC.Equals(YES_VALUE))
            {
                if (CL_PWN_0038_TICKETFEEAPP.Equals(1))
                {
                    ProjectedPaymentAmount = loanAmount + InterestAmount + CL_PWN_0044_TICKETFEE;
                    this.PawnLoanVO.totalLoanAmount = ProjectedPaymentAmount;
                }
                else
                {
                    ProjectedPaymentAmount = loanAmount + InterestAmount + (N * CL_PWN_0044_TICKETFEE);
                    this.PawnLoanVO.totalLoanAmount = ProjectedPaymentAmount;
                }
            }

            if (CL_PWN_0028_INCLDFIRARMFEEAPC.Equals(YES_VALUE))
            {
                if (CL_PWN_0027_FIRARMFEEAPP.Equals(1))
                {
                    ProjectedPaymentAmount = loanAmount + InterestAmount + CL_PWN_0026_FIREARMFEEAMT;
                    this.PawnLoanVO.totalLoanAmount = ProjectedPaymentAmount;
                }
                else
                {
                    ProjectedPaymentAmount = loanAmount + InterestAmount + (N * CL_PWN_0026_FIREARMFEEAMT);
                    this.PawnLoanVO.totalLoanAmount = ProjectedPaymentAmount;
                }
            }

            /////////////////////////////////////////////////////
            //
            //calculation of due date
            //
            /////////////////////////////////////////////////////
            string CL_PWN_0061_LOANTERMCYCLE = "";
            DateTime currentDate = ShopDateTime.Instance.ShopDate;
            DateTime dueDate = currentDate;
            int val;
            bool result = false;
            const string MONTHNAME = "1 MONTH";
            const string CONSTANTB = "B";
            const string CONSTANTF = "F";
            const string CONSTANTY = YES_VALUE;
            string CL_PWN_0054_DUEDATEADJT = "";
            string CL_PWN_0093_DUEDATENBDADJTREQ = "";

            //begin strings

            businessRules = rules["PWN_BR-020"];

            //CL_PWN_0061_LOANTERMCYCLE-020
            if (businessRules.getComponentValue("CL_PWN_0061_LOANTERMCYCLE", ref componentValue))
            {
                try
                {
                    CL_PWN_0061_LOANTERMCYCLE = componentValue;
                }
                catch (Exception exc)
                {
                    //Console.WriteLine(exc.Message);
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0061_LOANTERMCYCLE Not found");
            }

            //CL_PWN_0054_DUEDATEADJT-020
            if (businessRules.getComponentValue("CL_PWN_0054_DUEDATEADJT", ref componentValue))
            {
                try
                {
                    CL_PWN_0054_DUEDATEADJT = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0054_DUEDATEADJT Not found");
            }

            //CL_PWN_0093_DUEDATENBDADJTREQ-020
            if (businessRules.getComponentValue("CL_PWN_0093_DUEDATENBDADJTREQ", ref componentValue))
            {
                try
                {
                    CL_PWN_0093_DUEDATENBDADJTREQ = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0093_DUEDATENBDADJTREQ Not found");
            }

            //end strings

            if (IsAllDigits(CL_PWN_0061_LOANTERMCYCLE))
            {
                result = Int32.TryParse(CL_PWN_0061_LOANTERMCYCLE, out val);
                if (result.Equals(true))
                {
                    dueDate = currentDate.AddDays(val).Date;
                }
            }
            else if (CL_PWN_0061_LOANTERMCYCLE.Equals(MONTHNAME))
            {
                dueDate = currentDate.Date.AddMonths(1).Date;//currentDate.AddMonths(1);
                DateTime dueDateDt = dueDate.Date;

                if (IsShopClosed(dueDateDt))
                {
                    if (CL_PWN_0093_DUEDATENBDADJTREQ.Equals(CONSTANTY))
                    {
                        if (CL_PWN_0054_DUEDATEADJT.Equals(CONSTANTB))
                        {
                            do
                            {
                                dueDateDt = dueDateDt.AddDays(-1.0d).Date;
                            }
                            while (IsShopClosed(dueDateDt));
                        }
                        else if (CL_PWN_0054_DUEDATEADJT.Equals(CONSTANTF))
                        {
                            do
                            {
                                dueDateDt = dueDateDt.AddDays(1.0d).Date;
                            }
                            while (IsShopClosed(dueDateDt));
                        }
                    }
                }
            }

            /////////////////////////////////////////////////////
            //
            //calculation of maturity date
            //
            /////////////////////////////////////////////////////
            DateTime maturityDate = currentDate.Date;
            string CL_PWN_0055_MATDATEADJT = "";

            //CL_PWN_0055_MATDATEADJT-020
            if (businessRules.getComponentValue("CL_PWN_0055_MATDATEADJT", ref componentValue))
            {
                try
                {
                    CL_PWN_0055_MATDATEADJT = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0055_MATDATEADJT Not found");
            }

            if (IsAllDigits(CL_PWN_0061_LOANTERMCYCLE))
            {
                result = Int32.TryParse(CL_PWN_0061_LOANTERMCYCLE, out val);
                if (result)
                {
                    maturityDate = currentDate.Date.AddDays(val);
                }
            }
            else if (CL_PWN_0061_LOANTERMCYCLE.Equals(MONTHNAME))
            {
                maturityDate = currentDate.Date.AddMonths(1).Date;

                if (IsShopClosed(maturityDate.Date))
                {
                    if (CL_PWN_0055_MATDATEADJT.Equals(CONSTANTB))
                    {
                        do
                        {
                            maturityDate = maturityDate.AddDays(-1.0d).Date;
                        }
                        while (IsShopClosed(maturityDate));
                    }
                    else if (CL_PWN_0055_MATDATEADJT.Equals(CONSTANTF))
                    {
                        do
                        {
                            maturityDate = maturityDate.AddDays(1.0d).Date;
                        }
                        while (IsShopClosed(maturityDate));
                    }
                }
            }

            /////////////////////////////////////////////////////
            //
            //calculation of PFI Day
            //
            /////////////////////////////////////////////////////
            string CL_PWN_0058_PFICALCDATEUSED = "";
            int CL_PWN_0056_WTDAYSPRPFI = 0;
            int CL_PWN_0057_WTMNTHSPRPFI = 0;
            string CL_PWN_0059_PFIDATEADJT = "";
            string CONSTANT_MADE_DATE = "MADE DATE";
            string CONSTANT_DUE_DATE = "DUE DATE";
            DateTime pfiDate = currentDate.Date;

            //CL_PWN_0059_PFIDATEADJT-020
            if (businessRules.getComponentValue("CL_PWN_0059_PFIDATEADJT", ref componentValue))
            {
                try
                {
                    CL_PWN_0059_PFIDATEADJT = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0059_PFIDATEADJT Not found");
            }

            //CL_PWN_0058_PFICALCDATEUSED-020
            if (businessRules.getComponentValue("CL_PWN_0058_PFICALCDATEUSED", ref componentValue))
            {
                try
                {
                    CL_PWN_0058_PFICALCDATEUSED = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0058_PFICALCDATEUSED Not found");
            }

            //Check to see if days or months component should be used
            string pfiWait="D";
            if (businessRules.getComponentValue("PFIWAIT", ref componentValue))
            {
                pfiWait = componentValue;
            }
            //CL_PWN_0056_WTDAYSPRPFI-020
            if (pfiWait.Equals("D"))
            {
                if (businessRules.getComponentValue("CL_PWN_0056_WTDAYSPRPFI", ref componentValue))
                {
                    int parseVal_56;
                    if (Int32.TryParse(componentValue, out parseVal_56))
                    {
                        CL_PWN_0056_WTDAYSPRPFI = parseVal_56;
                    }
                }
                else
                {
                    sbUpw.AppendLine("CL_PWN_0056_WTDAYSPRPFI Not found");
                }
            }
            else
            {
                if (businessRules.getComponentValue("CL_PWN_0057_WTMNTHSPRPFI", ref componentValue))
                {
                    int parseVal_57;
                    if (Int32.TryParse(componentValue, out parseVal_57))
                    {
                        CL_PWN_0057_WTMNTHSPRPFI = parseVal_57;
                    }
                }
                else
                {
                    sbUpw.AppendLine("CL_PWN_0057_WTMNTHSPRPFI Not found");
                }

            }
            //CL_PWN_0169_PFIDATENBDADJTREQ
            bool adjustPfiDate = false;
            if (businessRules.getComponentValue("CL_PWN_0169_PFIDATENBDADJTREQ", ref componentValue))
            {
                if (!string.IsNullOrEmpty(componentValue))
                {
                    if (string.Equals(componentValue, YES_VALUE, StringComparison.OrdinalIgnoreCase))
                    {
                        adjustPfiDate = true;
                    }
                }
            }

            if (CL_PWN_0058_PFICALCDATEUSED.Equals(CONSTANT_MADE_DATE))
            {
                pfiDate = currentDate.Date;
            }
            else if (CL_PWN_0058_PFICALCDATEUSED.Equals(CONSTANT_DUE_DATE))
            {
                pfiDate = dueDate.Date;
            }
            pfiDate = CL_PWN_0056_WTDAYSPRPFI > 0 ? pfiDate.AddDays(CL_PWN_0056_WTDAYSPRPFI).Date : pfiDate.AddMonths(CL_PWN_0057_WTMNTHSPRPFI).Date;

            if (IsShopClosed(pfiDate.Date))
            {
                //Verify with rule value 169
                if (adjustPfiDate)
                {
                    if (CL_PWN_0059_PFIDATEADJT.Equals(CONSTANTB))
                    {
                        do
                        {
                            pfiDate = pfiDate.AddDays(-1.0d).Date;
                        }
                        while (IsShopClosed(pfiDate));
                    }
                    else if (CL_PWN_0059_PFIDATEADJT.Equals(CONSTANTF))
                    {
                        do
                        {
                            pfiDate = pfiDate.AddDays(1.0d).Date;
                        }
                        while (IsShopClosed(pfiDate));
                    }
                }
            }

            /////////////////////////////////////////////////////
            //
            //calculation of PFI Notification Date
            //
            /////////////////////////////////////////////////////
            DateTime pfiNotificationDate = new DateTime();
            int CL_PWN_0088_PFIMLRTRGRDAYS = 0;
            int CL_PWN_0113_PFIMLRDEFADJT = 0;
            int CL_PWN_266_PFINOTIFICATON = 0;

            //CL_PWN_0088_PFIMLRTRGRDAYS-020
            if (businessRules.getComponentValue("CL_PWN_0088_PFIMLRTRGRDAYS", ref componentValue))
            {
                try
                {
                    CL_PWN_0088_PFIMLRTRGRDAYS = int.Parse(componentValue);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0088_PFIMLRTRGRDAYS Not found");
            }

            //CL_PWN_0113_PFIMLRDEFADJT-020
            if (businessRules.getComponentValue("CL_PWN_0113_PFIMLRDEFADJT", ref componentValue))
            {
                try
                {
                    CL_PWN_0113_PFIMLRDEFADJT = int.Parse(componentValue);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_0113_PFIMLRDEFADJT Not found");
            }

            if (businessRules.getComponentValue("CL_PWN_266_PFINOTIFICATIONDATE", ref componentValue))
            {
                try
                {
                    CL_PWN_266_PFINOTIFICATON = int.Parse(componentValue);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return;
                }
            }
            else
            {
                sbUpw.AppendLine("CL_PWN_266_PFINOTIFICATIONDATE Not found");
            }

            pfiNotificationDate = newPawnItem.State.ToUpper().Equals(States.Ohio) ? currentDate.Date.AddMonths(CL_PWN_266_PFINOTIFICATON) : pfiDate.Date.AddDays(-CL_PWN_0088_PFIMLRTRGRDAYS);
            if (IsShopClosed(pfiNotificationDate) && CL_PWN_0113_PFIMLRDEFADJT > 0)
            {
                do
                {
                    pfiNotificationDate = pfiNotificationDate.Date.AddDays(CL_PWN_0113_PFIMLRDEFADJT);
                }
                while (IsShopClosed(pfiNotificationDate));
            }

            


            //Store dates in underwrite pawn
            PawnLoanVO.DueDate = dueDate;
            //            upwVO.GraceDate = dueDate.Date.AddDays(1.0d);
            PawnLoanVO.GraceDate = pfiDate;
            PawnLoanVO.MadeDate = currentDate;
            PawnLoanVO.MadeTime = ShopDateTime.Instance.ShopTime;
            PawnLoanVO.PFIDate = pfiDate;
            PawnLoanVO.PFINotifyDate = pfiNotificationDate;
            PawnLoanVO.MaturityDate = maturityDate;
        }

        /// <summary>
        /// 
        /// </summary>
        public UnderwritePawnLoanVO PawnLoanVO { get; private set; }

        //method to check if the string is all digits
        public static bool IsAllDigits(string sRaw)
        {
            string s = sRaw.Trim();
            if (s.Length == 0)
            {
                return false;
            }
            for (int index = 0; index < s.Length; index++)
            {
                if (Char.IsDigit(s[index]) == false)
                {
                    return false;
                }
            }
            return true;
        }

        //method to check if it is a valid date
        public bool isDate(string strDate)
        {
            //string strRegex = @"((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1- 9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))";
            string strRegex = @"((^(10|12|0?[13578])([/])(3[01]|[12][0\-9]|0?[1\-9])([/])((1[8\-9]\d{2})|([2\-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0\-9]|0?[1\-9])([/])((1[8\-9]\d{2})|([2\-9]\d{3}))$)|(^(0?2)([/])(2[0\-8]|1[0\-9]|0?[1\- 9])([/])((1[8\-9]\d{2})|([2\-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2\-9][0\-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2\-9][0\-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2\-9][0\-9][13579][26])$))";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(strDate))
                return (true);
            else
                return (false);
        }

        //method to check if a date is valid when coming as string
        //this method checks to see if the dueDate is a holiday Or a shop Closed Date
        public bool IsShopClosed(DateTime dateInput)
        {
            /* if (FileLogger.Instance.IsLogDebug)
            {
            FileLogger.Instance.logMessage(LogLevel.DEBUG, 
            "Underwrite Pawn Loan",
            "IsShopClosed({0})...", dateInput);
            }*/
            var shopCalendar = DesktopSession.ShopCalendar;
            var holidayCalendar = DesktopSession.HolidayCalendar;

            //If both calendars are empty, we have an invalid scenario
            if (CollectionUtilities.isEmpty(shopCalendar) &&
                CollectionUtilities.isEmpty(holidayCalendar))
            {
                FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan", "Invalid shop/holiday calendar!");
                return (false);
            }

            if (CollectionUtilities.isEmpty(shopCalendar))
            {
                FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan", "Invalid shop calendar!");
                return (false);
            }

            var currentShopTime = ShopDateTime.Instance.ShopDate;
            var dOW = dateInput.DayOfWeek;

            //Get minimum year
            var minYear = currentShopTime.Year;
            if (dateInput.Year < minYear)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, "Underwrite Pawn Loan", "Trying to check a date in the past: {0}", dateInput);
                return (false);
            }

            //Set flag to assume workday, not holiday
            var isHoliday = false;

            if (CollectionUtilities.isNotEmpty(holidayCalendar))
            {
                //Verify that the date is not a holiday
                foreach (var hC in holidayCalendar)
                {
                    if (hC == null) continue;
                    if (hC.CalendarDate.Year < minYear) continue;
                    if (hC.CalendarDate.Date.Equals(dateInput))
                    {
                        isHoliday = true;
                        /*if (FileLogger.Instance.IsLogDebug)
                        {
                        FileLogger.Instance.logMessage(
                        LogLevel.DEBUG,
                        "Underwrite Pawn Loan",
                        "Date input {0} is a holiday - checked against {1}",
                        dateInput,
                        hC);
                        }*/
                        break;
                    }
                }
            }

            //If date input is not a holiday, check for work date
            if (!isHoliday)
            {
                var isWorkday = false;
                var dOWStr = dOW.ToString();
                foreach (var sC in shopCalendar)
                {
                    if (sC == null) continue;
                    if (sC.CalendarDate.Year < minYear) continue;
                    if (!sC.WorkDay)
                    {
                        continue;
                    }
                    var dayName = sC.NameOfDay;
                    if (!dOWStr.Equals(dayName, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    isWorkday = true;
                    /* if (FileLogger.Instance.IsLogDebug)
                            {
                            FileLogger.Instance.logMessage(
                            LogLevel.DEBUG,
                            "Underwrite Pawn Loan",
                            "Date input {0} is a workday - checked against {1}",
                            dateInput,
                            sC);
                            }*/
                    break;
                }
                return (!isWorkday);
            }

            //If we get here, the shop is closed
            return (true);
        }

        public DateTime GetPfiDate(DateTime dateTime, SiteId siteID)
        {
            string CL_PWN_0058_PFICALCDATEUSED = "";
            int CL_PWN_0056_WTDAYSPRPFI = 0;
            string CL_PWN_0059_PFIDATEADJT = "";
            string CONSTANT_MADE_DATE = "MADE DATE";
            string CONSTANT_DUE_DATE = "DUE DATE";
            string CONSTANTB = "B";
            string CONSTANTF = "F";

            DateTime dueDate = dateTime;
            DateTime pfiDate = dateTime;

            PawnRulesSystemInterface prs = DesktopSession.PawnRulesSys;
            List<String> bRules = new List<String>();
            bRules.Add("PWN_BR-020");

            Dictionary<string, BusinessRuleVO> rules;
            prs.beginSite(prs, siteID, bRules, out rules);
            prs.getParameters(prs, siteID, ref rules);
            prs.endSite(prs, siteID);

            BusinessRuleVO businessRules;
            businessRules = null;
            string componentValue = "";

            businessRules = rules["PWN_BR-020"];

            //CL_PWN_0059_PFIDATEADJT-020
            if (businessRules.getComponentValue("CL_PWN_0059_PFIDATEADJT", ref componentValue))
            {
                try
                {
                    CL_PWN_0059_PFIDATEADJT = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return DateTime.MinValue;
                }
            }

            //CL_PWN_0058_PFICALCDATEUSED-020
            if (businessRules.getComponentValue("CL_PWN_0058_PFICALCDATEUSED", ref componentValue))
            {
                try
                {
                    CL_PWN_0058_PFICALCDATEUSED = componentValue;
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return DateTime.MinValue;
                }
            }

            //CL_PWN_0056_WTDAYSPRPFI-020
            if (businessRules.getComponentValue("CL_PWN_0056_WTDAYSPRPFI", ref componentValue))
            {
                try
                {
                    CL_PWN_0056_WTDAYSPRPFI = int.Parse(componentValue);
                }
                catch (Exception exc)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "Underwrite Pawn Loan Utility", "Exception: " + exc);
                    return DateTime.MinValue;
                }
            }

            if (CL_PWN_0058_PFICALCDATEUSED.Equals(CONSTANT_MADE_DATE))
            {
                pfiDate = dateTime.Date;
            }
            else if (CL_PWN_0058_PFICALCDATEUSED.Equals(CONSTANT_DUE_DATE))
            {
                pfiDate = dueDate.Date;
            }
            pfiDate = pfiDate.AddDays(CL_PWN_0056_WTDAYSPRPFI).Date;

            bool adjustPfiDate = false;
            if (businessRules.getComponentValue("CL_PWN_0169_PFIDATENBDADJTREQ", ref componentValue))
            {
                if (!string.IsNullOrEmpty(componentValue))
                {
                    if (string.Equals(componentValue, YES_VALUE, StringComparison.OrdinalIgnoreCase))
                    {
                        adjustPfiDate = true;
                    }
                }
            }

            if (IsShopClosed(pfiDate.Date))
            {
                if (adjustPfiDate)
                {
                    if (CL_PWN_0059_PFIDATEADJT.Equals(CONSTANTB))
                    {
                        do
                        {
                            pfiDate = pfiDate.AddDays(-1.0d).Date;
                        }
                        while (IsShopClosed(pfiDate));
                    }
                    else if (CL_PWN_0059_PFIDATEADJT.Equals(CONSTANTF))
                    {
                        do
                        {
                            pfiDate = pfiDate.AddDays(1.0d).Date;
                        }
                        while (IsShopClosed(pfiDate));
                    }
                }
            }

            return pfiDate;
        }
    }
}
