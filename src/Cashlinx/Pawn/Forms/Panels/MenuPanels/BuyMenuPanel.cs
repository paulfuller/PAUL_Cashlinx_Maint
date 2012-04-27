using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Authorization;
using Pawn.Logic;

namespace Pawn.Forms.Panels.MenuPanels
{
    public partial class BuyMenuPanel : UserControl
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

        public BuyMenuPanel()
        {
            InitializeComponent();
        }

        private void BuyMenuPanel_Load(object sender, EventArgs e)
        {
            this.buttonControllers = null;
            UserControl panelInstance = (UserControl)sender;
            this.menuController = new MenuLevelController(panelInstance);
            this.buttonControllers = new ImageButtonControllerGroup(GlobalDataAccessor.Instance.DesktopSession, this.Controls);
        }

 

        private void BuyMenuPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !DesignMode)
            {
                UserVO currUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;
                //Check if user has permissions to do returns on customer purchase
                string btnName = "CUSTOMERBUY";
                this.CustomerBuyButton.Enabled = (SecurityProfileProcedures.CanUserViewResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession));
                //Check if user has permissions to do vendor purchase
                btnName = "VENDORBUY";
                this.VendorBuyButton.Enabled = (SecurityProfileProcedures.CanUserViewResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession));

            }
        }
    }
}
