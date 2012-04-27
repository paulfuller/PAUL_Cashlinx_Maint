using System;
using Common.Libraries.Objects.Rules.Type;

namespace Common.Libraries.Objects.Rules.Data
{
    [Serializable()]
    public class FeesVO
    {
        public FeeLookupTypeVO FeeLookupType { get; set; }
        public FeeTypeVO FeeType { get; set; }
        public AliasVO Alias { get; set; }
        public string ParamKeyCode { get; set; }
        public string Value { get; set; }
        public string ParentValue { get; set; }

        public FeesVO()
        {
            this.FeeLookupType = new FeeLookupTypeVO();
            this.FeeType = new FeeTypeVO();
            this.Alias = new AliasVO();
            this.ParamKeyCode = string.Empty;
            this.Value = string.Empty;
            this.ParentValue = string.Empty;
        }
    }
}
