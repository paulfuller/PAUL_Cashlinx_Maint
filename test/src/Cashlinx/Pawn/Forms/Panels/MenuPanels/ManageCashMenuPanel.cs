using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Authorization;
using Pawn.Logic;

namespace Pawn.Forms.Panels.MenuPanels
{
    public partial class ManageCashMenuPanel : UserControl
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
        
        public ManageCashMenuPanel()
        {
            InitializeComponent();
        }

        private void ManageCashMenuPanel_Load(object sender, EventArgs e)
        {
            this.buttonControllers = null;
            var panelInstance = (UserControl)sender;
            this.menuController = new MenuLevelController(panelInstance);
            this.buttonControllers = new ImageButtonControllerGroup(GlobalDataAccessor.Instance.DesktopSession, this.Controls);
        }

        private void ManageCashMenuPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !DesignMode)
            {
                var currUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;
                //check if user has access to shop cash button
                const string btnName = "SAFEMANAGEMENT";
                if (!(SecurityProfileProcedures.CanUserModifyResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession)))
                {
                    this.SafeOperationsButton.Enabled = false;
                }
                else
                    this.SafeOperationsButton.Enabled = true;
                

                //Check if user has access to buttons on this panel
               /* SecurityProfileProcedures.ModifyButtonAccessBasedOnSecurityProfile(this.Controls, currUser,
                    ImageButtonControllerGroup.BUTTON_SUFFIX,
                    ImageButtonControllerGroup.BUTTON_TAGSEP,
                    ImageButtonControllerGroup.BUTTON_LEAF);*/
            }
        }
    }
}
