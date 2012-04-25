using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.Common
{
    public class IsMerchandiseLookedUp : ConditionBlock
    {
        public static readonly string NAME = "IsMerchandiseLookedUp";

        private static object isMerchandiseLookedUp(object data)
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
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

        public IsMerchandiseLookedUp()
            : base(NAME)
        {
            this.LogicFxn = new FxnBlock();
            this.LogicFxn.Function = isMerchandiseLookedUp;
        }
    }
}