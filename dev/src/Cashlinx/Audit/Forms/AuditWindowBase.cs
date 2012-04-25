using Audit.Logic;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects;

namespace Audit.Forms
{
    public partial class AuditWindowBase : CustomBaseForm
    {
        public AuditWindowBase()
        {
            InitializeComponent();

            NavControlBox = new NavBox
            {
                Owner = this
            };
        }

        public NavBox NavControlBox { get; set; }

        protected AuditDesktopSession ADS
        {
            get { return AuditDesktopSession.Instance; }
        }

        protected CommonDatabaseContext CreateCommonDatabaseContext()
        {
            CommonDatabaseContext dataContext = new CommonDatabaseContext();
            dataContext.CurrentSiteId = ADS.CurrentSiteId;
            dataContext.FullUserName = ADS.FullUserName;
            dataContext.User = ADS.LoggedInUserSecurityProfile;
            dataContext.UserName = ADS.UserName;

            return dataContext;
        }
    }
}
