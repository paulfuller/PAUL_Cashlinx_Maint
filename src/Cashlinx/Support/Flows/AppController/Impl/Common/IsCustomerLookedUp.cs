using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Libraries.Objects.Customer;
//using Pawn.Logic;

namespace Support.Flows.AppController.Impl.Common
{
    public class IsCustomerLookedUp : ConditionBlock
    {
        public static readonly string NAME = "IsCustomerLookedUp";

        private object isCustomerLookedUp(object data)
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            if (cds == null)
            {
                return (null);
            }

            CustomerVO cust = cds.ActiveCustomer;
            if (cust != null)
            {
                if (!string.IsNullOrEmpty(cust.CustomerNumber))
                {
                    return (true);
                }
            }

            return (null);
        }

        public IsCustomerLookedUp() : base(NAME)
        {
            this.LogicFxn = new FxnBlock();
            this.LogicFxn.Function = this.isCustomerLookedUp;
        }
    }
}
