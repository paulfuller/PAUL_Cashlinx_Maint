using System;

namespace Common.Libraries.Objects.Rules.Type
{
    [Serializable()]
    public class FeeLookupTypeVO
    {
        public string Id { get; set; }
        public string Code { get; set; }

        public FeeLookupTypeVO()
        {
            this.Id = string.Empty;
            this.Code = string.Empty;
        }
    }
}
