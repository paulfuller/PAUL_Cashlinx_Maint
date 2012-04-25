using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.Common
{
    public class IsBuyMerchandiseLookedUp : ConditionBlock
    {
        public const string NAME = "IsBuyMerchandiseLookedUp";

        private static object isBuyMerchandiseLookedUp(object data)
        {
            var cds = GlobalDataAccessor.Instance.DesktopSession;
            if (cds == null)
            {
                return (null);
            }

            if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase == null ||
                GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items == null || 
                GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items.Count == 0 || 
                GlobalDataAccessor.Instance.DesktopSession.DescribeItemSelectedProKnowMatch == null)
                return false;
            return true;

            
        }

        public IsBuyMerchandiseLookedUp()
            : base(NAME)
        {
            this.LogicFxn = new FxnBlock();
            this.LogicFxn.Function = isBuyMerchandiseLookedUp;
        }
    }
}
