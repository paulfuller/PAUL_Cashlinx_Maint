using System.Collections.Generic;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Business
{
    public class ChargeOffDatabaseContext
    {
        public int AuditId { get; set; }
        public string AtfIncidentId { get; set; }
        public string AuthorizedBy { get; set; }
        public string CharitableOrganization { get; set; }
        public string CharitableAddress { get; set; }
        public string CharitableCity { get; set; }
        public string CharitableState { get; set; }
        public string CharitablePostalCode { get; set; }
        public string ReplacedIcn { get; set; }
        public string Comments { get; set; }
        public bool Destroyed { get; set; }
        public ItemReason ItemReason { get; set; }
        public string PoliceCaseNumber { get; set; }
        public string StoreNumber { get; set; }
        public string TranType { get; set; }
        public List<string> Icns { get; set; }
    }
}
