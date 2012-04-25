using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Database.Procedures;

namespace Pawn.Forms.Panels.MenuPanels
{
    public partial class PFIMenuPanel : UserControl
    {
        private ImageButtonControllerGroup buttonControllers;
        private MenuLevelController menuController;

        public ImageButtonControllerGroup ButtonControllers
        {
            get
            {
                return (this.buttonControllers);
            }
        }

        public MenuLevelController MenuController
        {
            get
            {
                return (this.menuController);
            }
        }

        public PFIMenuPanel()
        {
            InitializeComponent();

        }

        private void PFIMenuPanel_Load(object sender, EventArgs e)
        {
            this.buttonControllers = null;
            var panelInstance = (UserControl)sender;
            this.menuController = new MenuLevelController(panelInstance);
            this.buttonControllers = new ImageButtonControllerGroup(GlobalDataAccessor.Instance.DesktopSession, this.Controls);
        }

        private void checkUserAccess()
        {
            //Check if user has access to PFI Create List
            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            var currUser = dSession.LoggedInUserSecurityProfile;
            if (currUser != null)
            {
                int idx = this.PFICreateListButton.Name.IndexOf("Button", System.StringComparison.Ordinal);
                var btnName = PFICreateListButton.Name.Substring(0, idx).ToUpper();
                if (SecurityProfileProcedures.CanUserViewResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession))
                {
                    idx = PFICreateListButton.Name.IndexOf("Button", StringComparison.Ordinal);
                    btnName = PFICreateListButton.Name.Substring(0, idx).ToUpper();
                    PFICreateListButton.Enabled = SecurityProfileProcedures.CanUserViewResource(btnName, currUser, dSession);
                }

                //Check if user has access to PFI Verify
                idx = this.PFIVerifyButton.Name.IndexOf("Button", System.StringComparison.Ordinal);
                btnName = PFIVerifyButton.Name.Substring(0, idx).ToUpper();
                if (SecurityProfileProcedures.CanUserViewResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession))
                {
                    idx = PFIVerifyButton.Name.IndexOf("Button", StringComparison.Ordinal);
                    btnName = PFIVerifyButton.Name.Substring(0, idx).ToUpper();
                    PFIVerifyButton.Enabled = SecurityProfileProcedures.CanUserViewResource(btnName, currUser, dSession);
                }

                //Check if user has access to PFI Post
                idx = this.PFIPostButton.Name.IndexOf("Button", System.StringComparison.Ordinal);
                btnName = PFIPostButton.Name.Substring(0, idx).ToUpper();
                if (SecurityProfileProcedures.CanUserViewResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession))
                {
                    idx = PFIPostButton.Name.IndexOf("Button", StringComparison.Ordinal);
                    btnName = PFIPostButton.Name.Substring(0, idx).ToUpper();
                    PFIPostButton.Enabled = SecurityProfileProcedures.CanUserViewResource(btnName, currUser, dSession);
                }

                var isPFIMailersRequiredForState = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPFIMailersRequiredForState(GlobalDataAccessor.Instance.CurrentSiteId);

                if (isPFIMailersRequiredForState)
                {
                    //Check if user has access to Print PFI Mailers
                    idx = PrintPFIMailersButton.Name.IndexOf("Button", StringComparison.Ordinal);
                    btnName = PrintPFIMailersButton.Name.Substring(0, idx).ToUpper();
                    PrintPFIMailersButton.Enabled = true;
                }
            }
        }

        private void PFIMenuPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !DesignMode)
            {
                checkUserAccess();
            }
        }
    }
}
