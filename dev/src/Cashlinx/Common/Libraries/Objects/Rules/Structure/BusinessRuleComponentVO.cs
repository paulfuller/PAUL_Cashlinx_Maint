using System;
using System.Collections.ObjectModel;
using Common.Libraries.Objects.Rules.Data;
using Common.Libraries.Objects.Rules.Retrieval;

namespace Common.Libraries.Objects.Rules.Structure
{
    [Serializable()]
    public class BusinessRuleComponentVO
    {
        public Guid Id { get; private set; }
        public Guid ParentId { get; set; }
        public string ParentAlias { get; set; }
        public bool Deleted { get; set; }

        /// <summary>
        /// A list of business rule components that are children of this component.
        /// List will be null if not populated from XML or there were no children.
        /// </summary>
        public ObservableCollection<BusinessRuleComponentVO> Children { get; set; }

        public string Alias { get;  set; }

        public enum RuleValueType
        {
            PARAM,
            INTEREST,
            FEES,
            METHOD
        }
        public static string GetRuleValueTypeString(RuleValueType rType)
        {
            switch(rType)
            {
                case RuleValueType.PARAM:
                    return ("PARAM");
                case RuleValueType.INTEREST:
                    return ("INTEREST");
                case RuleValueType.FEES:
                    return ("FEES");
                case RuleValueType.METHOD:
                    return ("METHOD");

                default:
                    return(string.Empty);
            }
        }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { 
            get; 
            set; 
        }
        public string Code { get; set; }
        public RetrievalVO Retrieval { get; set; }
        public RuleValueType ValueType { get; set; }

        public FeesVO FeesValue { get; set; }
        public InterestVO InterestValue { get; set; }
        public ParamVO ParamValue { get; set; }
        public MethodVO MethodValue { get; set; }
        public bool IsEditable
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public BusinessRuleComponentVO()
        {

        }

        public BusinessRuleComponentVO(Guid id, string code, RuleValueType vt, bool isEditable)
        {
            this.Id = id;
            this.Code = code;
            this.Retrieval = new RetrievalVO();
            this.ValueType = vt;
            this.FeesValue = new FeesVO();
            this.InterestValue = new InterestVO();
            this.ParamValue = new ParamVO();
            this.MethodValue = new MethodVO();
            this.IsEditable = isEditable;
        }
        
        public BusinessRuleComponentVO(Guid id, string code, RuleValueType vt, bool isEditable, DateTime fD, 
            DateTime tD, string alias, Guid parentId, string parentAlias)
            :this(id, code, vt, isEditable)
        {
            this.ParentId = parentId;
            this.ParentAlias = parentAlias;
            this.FromDate = fD;
            this.ToDate = tD;
            this.Alias = alias;
        }
                
    }

}
