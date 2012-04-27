using System.Collections.Generic;

namespace Common.Libraries.Objects.Audit
{
    public class ProcessUnexpectedContext
    {
        public InventoryAuditVO Audit { get; set; }
        public List<TrakkerItem> Items { get; set; }
        public ProcessUnexpectedUserOption UserOption { get; set; }
    }
}
