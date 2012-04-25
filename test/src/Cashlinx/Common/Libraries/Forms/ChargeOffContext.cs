using System;
using Common.Controllers.Application;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Forms
{
    public class ChargeOffContext
    {
        public ChargeOffContext(DesktopSession desktopSession)
        {
            if (desktopSession == null)
            {
                throw new ArgumentNullException("desktopSession");
            }

            DesktopSession = desktopSession;
            DefaultItemReason = ItemReason.COFFMISSING;
        }

        public string AuthorizedBy { get; set; }
        public decimal ChargeOffAmount { get; set; }
        public ChargeOffDatabaseContext ChargeOffDatabaseContext { get; set; }
        public string Comment { get; set; }
        public ItemReason DefaultItemReason { get; set; }
        public string Description { get; set; }
        public DesktopSession DesktopSession { get; private set; }
        public Icn Icn { get; set; }
        public bool IsGun { get; set; }
        public ItemReason ItemReason { get; set; }
        public bool MultipleItems { get; set; }
        public string PoliceCaseNumber { get; set; }
    }
}
