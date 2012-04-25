using System;

namespace Common.Libraries.Objects.Rules.Type
{
    [Serializable()]
    public class RetrievalTypeVO
    {
        public string Id { get; set; }
        public string Code { get; set; }

        public RetrievalTypeVO()
        {
            this.Id = string.Empty;
            this.Code = string.Empty;
        }
    }
}
