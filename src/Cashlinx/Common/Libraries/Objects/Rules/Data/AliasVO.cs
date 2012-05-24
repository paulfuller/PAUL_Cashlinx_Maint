using System;

namespace Common.Libraries.Objects.Rules.Data
{
    [Serializable()]
    public class AliasVO
    {
        public string Id   { get; set; }
        public string Code { get; set; }

        public AliasVO()
        {
            Id = string.Empty;
            Code = string.Empty;
        }
    }
}
