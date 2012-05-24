using System;
using Common.Libraries.Objects.Rules.Type;

namespace Common.Libraries.Objects.Rules.Retrieval
{
    [Serializable()]
    public class RetrievalVO
    {
        public RetrievalTypeVO RetrievalType { get; set; }
        public string Code { get; set; }
        public string PackageCode { get; set; }

        public RetrievalVO()
        {
            this.RetrievalType = new RetrievalTypeVO();
            this.Code = string.Empty;
            this.PackageCode = string.Empty;
        }
    }
}
