using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.Common
{
    public class IsLoanMerchandiseLookedUp : ConditionBlock
    {
        public static readonly string NAME = "IsLoanMerchandiseLookedUp";

        private static object isLoanMerchandiseLookedUp(object data)
        {
            var cds = GlobalDataAccessor.Instance.DesktopSession;
            if (cds == null)
            {
                return (null);
            }

            if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan == null ||
                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items == null ||
                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Count == 0 || 
                GlobalDataAccessor.Instance.DesktopSession.DescribeItemSelectedProKnowMatch == null)
                return false;
            return true;

            
        }

        public IsLoanMerchandiseLookedUp()
            : base(NAME)
        {
            this.LogicFxn = new FxnBlock();
            this.LogicFxn.Function = isLoanMerchandiseLookedUp;
        }
    }
}
