using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.String;

namespace Common.Libraries.Objects.Rules.Structure
{
    [Serializable()]
    public class BusinessRuleVO
    {
        public BusinessRuleVO()
        {
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        private Dictionary<string, Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO> ruleComponents;
        public Dictionary<string, Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO>.KeyCollection Keys
        {
            get
            {
                return (ruleComponents.Keys);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO this[int i]
        {
            get
            {
                if(i < 0 || CollectionUtilities.isEmpty(ruleComponents) || i >= this.ruleComponents.Keys.Count) return (null);
                return (this.ruleComponents[this.ruleComponents.Keys.ToList()[i]]);
            }
        }

        public Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO this[string s]
        {
            get
            {
                if (StringUtilities.isEmpty(s) || CollectionUtilities.isEmpty(ruleComponents) || !this.ruleComponents.ContainsKey(s)) return (null);
                return (this.ruleComponents[s]);
            }

            set
            {
                if (!StringUtilities.isEmpty(s) && !CollectionUtilities.isEmpty(ruleComponents) && this.ruleComponents.ContainsKey(s))
                {
                    this.ruleComponents[s] = value;
                }
            }
        }

        public bool ContainsKey(string s)
        {
            if (StringUtilities.isEmpty(s)) return (false);
            return (this.ruleComponents.ContainsKey(s));
        }

        public Dictionary<string, Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO>.Enumerator ComponentEnumerator
        {
            get
            {
                return (ruleComponents.GetEnumerator());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vo"></param>
        public void addComponent(Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO vo)
        {
            string keyData = vo.Code;
            if (this.ruleComponents == null || this.ruleComponents.ContainsKey(vo.Code))return;
            if (vo.ValueType==BusinessRuleComponentVO.RuleValueType.INTEREST)
            {
                if (vo.InterestValue.MaxAmount >= 0)
                    keyData = keyData + "_" + vo.InterestValue.MaxAmount;
                else
                {
                    if (this.ruleComponents.ContainsKey(vo.Code))
                        return;
                }

            }
            this.ruleComponents.Add(keyData, vo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentKey"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public bool getComponent(string componentKey, ref Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO vo)
        {
            if (StringUtilities.isEmpty(componentKey) ||
                !this.ruleComponents.ContainsKey(componentKey)) return (false);

            vo = this.ruleComponents[componentKey];
            if (vo == null) return (false);

            //switch (brvo.ValueType)
            //{
            //    case BusinessRuleComponentVO.RuleValueType.FEES:
            //        vo = brvo.FeesValue;
            //        break;
            //    case BusinessRuleComponentVO.RuleValueType.INTEREST:
            //        vo = brvo.InterestValue;
            //        break;
            //    case BusinessRuleComponentVO.RuleValueType.PARAM:
            //        vo = brvo.ParamValue;
            //        break;
            //    case BusinessRuleComponentVO.RuleValueType.METHOD:
            //        vo = brvo.MethodValue;
            //        break;
            //}

            return (true);
        }

        public bool getComponentValue(string componentKey, ref string vo)
        {
            if (StringUtilities.isEmpty(componentKey) ||
                !this.ruleComponents.ContainsKey(componentKey)) return (false);

            Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO brvo = this.ruleComponents[componentKey];
            if (brvo == null) return (false);

            switch (brvo.ValueType)
            {
                case Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO.RuleValueType.FEES:
                    if (brvo.FeesValue == null || StringUtilities.isEmpty(brvo.FeesValue.Value))
                    {
                        return (false);
                    }
                    vo = brvo.FeesValue.Value;
                    break;
                case Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO.RuleValueType.INTEREST:
                    if (brvo.InterestValue == null)
                    {
                        return (false);
                    }
                    vo = ""+brvo.InterestValue.InterestRate;
                    break;
                case Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO.RuleValueType.PARAM:
                    if (brvo.ParamValue == null || brvo.ParamValue.Value == null)
                    {
                        return (false);
                    }
                    vo = (string)(brvo.ParamValue.Value);
                    break;
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        public BusinessRuleVO(string code, Guid id)
        {
            this.Id = id;
            this.Code = code;
            this.ruleComponents = new Dictionary<string, Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO>();
        }

    }

    /// <summary>
    /// Light-weight class used to just hold business rule/comp
    /// relationship references.
    /// </summary>
    public class BusinessRuleCompLookupVO
    {
        #region Private Members
        
        private string _code;        
        private List<ComponentDetails> _componentCodes;
        
        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initialize with the code and component reference codes.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="compCodes"></param>
        public BusinessRuleCompLookupVO(string code, List<ComponentDetails> compCodes, Guid id)
        {
            this.Id = id;
            _code = code;
            _componentCodes = compCodes;
        }

        #endregion Constructor

        #region Public Properties

        public Guid Id { get; set; }

        /// <summary>
        /// Business rules code.
        /// </summary>
        public string Code
        {
            get { return _code; }
            private set { _code = value; }
        }

        /// <summary>
        /// Components code that are related to this business rule.
        /// </summary>
        public List<ComponentDetails> ComponentCodes
        {
            get { return _componentCodes; }
            private set { _componentCodes = value; }
        }

        #endregion Public Properties

    }

    public class ComponentDetails
    {
        public string ComponentCode { get; set; }
        public string ComponentType { get; set; }
    }

    
    // <summary> Represents a business rule node in the business rule tree. </summary>
    [Serializable()]
    public class BusinessRuleNodeVO : BusinessRuleVO
    {

        public BusinessRuleNodeVO()
        {
        }

        public BusinessRuleNodeVO(string code, Guid id) 
            : base(code, id){
        }

        /// <summary>
        /// List of business rule components that should contain
        /// children and children of child components, etc.          
        /// </summary>
        public ObservableCollection<Common.Libraries.Objects.Rules.Structure.BusinessRuleComponentVO> ComponentList { get; set; }
        
        /// <summary>
        /// Indicates this business rule and its children 
        /// have been deleted.
        /// </summary>
        public bool Deleted { get; set; }
    }

}
