using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Database.Procedures;

namespace Pawn.Forms.Panels.MenuPanels
{
    public partial class GunBookMenuPanel : UserControl
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

        public GunBookMenuPanel()
        {
            InitializeComponent();
        }

        private void GunBookMenuPanel_Load(object sender, EventArgs e)
        {
            this.buttonControllers = null;
            var panelInstance = (UserControl)sender;
            this.menuController = new MenuLevelController(panelInstance);
            this.buttonControllers = new ImageButtonControllerGroup(GlobalDataAccessor.Instance.DesktopSession, this.Controls);
        }

        private void GunBookMenuPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (!DesignMode && this.Visible)
            {
                var currUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;

                //check if user has access to gun book utilities                
                if (GunBookPrintButton.Enabled)
                {
                    const string btnName = "PRINT GUN BOOK IN CURRENT LOCATION";
                    this.GunBookPrintButton.Enabled = (SecurityProfileProcedures.CanUserViewResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession));
                }
                //Check if user has access to buttons on this panel
/*                SecurityProfileProcedures.ModifyButtonAccessBasedOnSecurityProfile(this.Controls, currUser,
                    ImageButtonControllerGroup.BUTTON_SUFFIX,
                    ImageButtonControllerGroup.BUTTON_TAGSEP,
                    ImageButtonControllerGroup.BUTTON_LEAF);*/
            }
        }
    }
}
