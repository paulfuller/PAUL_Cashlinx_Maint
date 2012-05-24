using System;

namespace Common.Libraries.Objects.Rules.Type
{
    [Serializable()]
    public class FeeTypeVO
    {
        public string Id { get; set; }
        public string Code { get; set; }

        public FeeTypeVO()
        {
            this.Id = string.Empty;
            this.Code = string.Empty;
        }
    }
}
