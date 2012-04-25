using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Pawn.Logic;

namespace Pawn.Forms.Panels.MenuPanels
{
    public partial class CashDrawerMenuPanel : UserControl
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
        public CashDrawerMenuPanel()
        {
            InitializeComponent();
        }

        private void CashDrawerMenuPanel_Load(object sender, EventArgs e)
        {
            this.buttonControllers = null;
            var panelInstance = (UserControl)sender;
            this.menuController = new MenuLevelController(panelInstance);
            this.buttonControllers = new ImageButtonControllerGroup(GlobalDataAccessor.Instance.DesktopSession, this.Controls);
        }

        private void CashDrawerMenuPanel_VisibleChanged(object sender, EventArgs e)
        {
          /*  if (!DesignMode && this.Visible)
            {
                UserVO currUser = CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile;

                //Check if user has access to buttons on this panel
                SecurityProfileProcedures.ModifyButtonAccessBasedOnSecurityProfile(this.Controls, currUser,
                    ImageButtonControllerGroup.BUTTON_SUFFIX,
                    ImageButtonControllerGroup.BUTTON_TAGSEP,
                    ImageButtonControllerGroup.BUTTON_LEAF);
            }*/

        }
    }
}
