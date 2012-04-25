using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Database.Procedures;

namespace Pawn.Forms.Panels.MenuPanels
{
    public partial class ManageInventoryMenuPanel : UserControl
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
        public ManageInventoryMenuPanel()
        {
            InitializeComponent();
        }

        private void ManageInventoryMenuPanel_Load(object sender, EventArgs e)
        {
            this.buttonControllers = null;
            var panelInstance = (UserControl)sender;
            this.menuController = new MenuLevelController(panelInstance);
            this.buttonControllers = new ImageButtonControllerGroup(GlobalDataAccessor.Instance.DesktopSession, this.Controls);
        }

        private void ManageInventoryMenuPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (!DesignMode && this.Visible)
            {
                /*  UserVO currUser = CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile;

                  //Check if user has access to buttons on this panel
                  SecurityProfileProcedures.ModifyButtonAccessBasedOnSecurityProfile(this.Controls, currUser, 
                      ImageButtonControllerGroup.BUTTON_SUFFIX,
                      ImageButtonControllerGroup.BUTTON_TAGSEP,
                      ImageButtonControllerGroup.BUTTON_LEAF);
              */
                string btnName;
                var currUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;

                //Check if user has access to Assign Item Physical Location
                if (AssignItemPhysicalLocationButton.Enabled)
                {
                    var idx = this.AssignItemPhysicalLocationButton.Name.IndexOf("Button", System.StringComparison.Ordinal);
                    btnName = AssignItemPhysicalLocationButton.Name.Substring(0, idx).ToUpper();
                    this.AssignItemPhysicalLocationButton.Enabled = SecurityProfileProcedures.CanUserViewResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession);
                }

                //Check if user has access to Change Retail Price Item
                if (ChangePricingButton.Enabled)
                {
                    //idx = this.ChangePricingButton.Name.IndexOf("Button");
                    //btnName = this.ChangePricingButton.Name.Substring(0, idx).ToUpper();
                    btnName = "CHANGERETAILPRICE";
                    this.ChangePricingButton.Enabled = SecurityProfileProcedures.CanUserViewResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession);
                }
            }

        }
    }
}
