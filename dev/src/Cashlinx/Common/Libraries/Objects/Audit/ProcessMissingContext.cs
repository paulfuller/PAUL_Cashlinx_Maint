using System.Collections.Generic;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Audit
{
    public class ProcessMissingContext
    {
        public InventoryAuditVO Audit { get; set; }
        public ItemReason ChargeOffReason { get; set; }
        public List<TrakkerItem> Items { get; set; }
        public int TrakkerSequenceNumber { get; set; }
        public ProcessMissingUserOptions UserOption { get; set; }
    }
}
