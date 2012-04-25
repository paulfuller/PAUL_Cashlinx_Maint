using System.Windows.Forms;
using Audit.Logic;
using Common.Controllers.Application.ApplicationFlow;
using Common.Libraries.Objects.Authorization;

namespace Audit.Panels.MenuPanels
{
    public partial class MenuPanelBase : UserControl
    {
        public MenuPanelBase()
        {
            InitializeComponent();

            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            HideOnReset = false;
        }

        public AuditDesktopSession ADS
        {
            get { return AuditDesktopSession.Instance; }
        }

        public ImageButtonControllerGroup ButtonControllers { get; protected set; }

        public MenuLevelController MenuController { get; protected set; }

        public virtual bool HideOnReset { get; set; }

        public virtual void ResetPanel()
        {
            this.Enabled = !HideOnReset;
            this.Visible = !HideOnReset;
            //TODO:MHM reset the button controllers when added to project
            if (HideOnReset)
            {
                this.SendToBack();
            }
            else
            {
                this.BringToFront();
                this.Update();
            }
        }
    }
}
