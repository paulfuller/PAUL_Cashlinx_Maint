using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Libraries.Objects.Business;

namespace Pawn.Flows.AppController.Impl.Common
{

        public class IsGunMerchandiseLookedUp : ConditionBlock
        {
            public static readonly string NAME = "IsGunMerchandiseLookedUp";

            private static object isGunMerchandiseLookedUp(object data)
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
                return GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Any(item => item.IsGun);
            }

            public IsGunMerchandiseLookedUp()
                : base(NAME)
            {
                this.LogicFxn = new FxnBlock();
                this.LogicFxn.Function = isGunMerchandiseLookedUp;
            }
        }
    
}
