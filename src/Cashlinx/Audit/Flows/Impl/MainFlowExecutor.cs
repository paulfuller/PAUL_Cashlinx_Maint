using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Impl;

namespace Audit.Flows.Impl
{
    public class MainFlowExecutor : MainFlowExecutorBase
    {
        public static readonly string INVENTORYAUDIT = "inventoryaudit";

        public MainFlowExecutor(DesktopSession desktopSession)
            : base(desktopSession)
        {
            this.AddFlowExecutor(INVENTORYAUDIT, "Audit.Flows.Impl.MainSubFlows.InventoryAuditFlowExecutor");
        }
    }
}
