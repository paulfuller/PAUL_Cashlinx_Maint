using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Authorization;
using Pawn.Logic;

namespace Pawn.Forms.Panels.MenuPanels
{
    public partial class UtilitiesMenuPanel : UserControl
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

        public UtilitiesMenuPanel()
        {
            InitializeComponent();
        }

        private void UtilitiesMenuPanel_Load(object sender, EventArgs e)
        {
            this.buttonControllers = null;
            var panelInstance = (UserControl)sender;
            this.menuController = new MenuLevelController(panelInstance);
            this.buttonControllers = new ImageButtonControllerGroup(GlobalDataAccessor.Instance.DesktopSession, this.Controls);
        }

        private void UtilitiesMenuPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !DesignMode)
            {
                int idx;
                string btnName;
                var dSession = GlobalDataAccessor.Instance.DesktopSession;
                var currUser = dSession.LoggedInUserSecurityProfile;
                //Check if user has access to Customer Holds
                if (CustomerHoldsButton != null && CustomerHoldsButton.Enabled && !string.IsNullOrEmpty(CustomerHoldsButton.Name))
                {                    
                    idx = this.CustomerHoldsButton.Name.IndexOf("Button", StringComparison.Ordinal);
                    btnName = CustomerHoldsButton.Name.Substring(0, idx).ToUpper();
                    this.CustomerHoldsButton.Enabled = (SecurityProfileProcedures.CanUserViewResource(btnName, currUser, dSession));
                }

                //Check if user has access to Police activities
                if (Police_ActivitiesButton != null && Police_ActivitiesButton.Enabled && !string.IsNullOrEmpty(Police_ActivitiesButton.Name))
                {
                    idx = this.Police_ActivitiesButton.Name.IndexOf("Button", StringComparison.Ordinal);
                    btnName = Police_ActivitiesButton.Name.Substring(0, idx).ToUpper();
                    this.Police_ActivitiesButton.Enabled = SecurityProfileProcedures.CanUserViewResource(btnName, currUser, dSession);
                }
                
                //Check if user has access to VOID
                if (voidTransactionButton != null && voidTransactionButton.Enabled && !string.IsNullOrEmpty(voidTransactionButton.Name))
                {
                    idx = this.voidTransactionButton.Name.IndexOf("Button", StringComparison.Ordinal);
                    btnName = voidTransactionButton.Name.Substring(0, idx).ToUpper();
                    this.voidTransactionButton.Enabled = SecurityProfileProcedures.CanUserViewResource(btnName, currUser, dSession);
                }
                
                this.ManageInventoryButton.Enabled = true;
                this.ManageCashButton.Enabled = true;

                //if (TransferButton.Enabled)
                //{
                //    this.TransferButton.Enabled = (CashlinxDesktop.Desktop.CashlinxDesktopSession.Instance.LoggedInUserSafeAccess);
                //}
               //Check if user has permissions to modify security profile
                btnName = "UPDATE USER PROFILE RESOURCES CURRENT LOCATION ONLY";
                const string multipleLocationResource = "UPDATE USER PROFILE RESOURCES MULTIPLE LOCATIONS";
                if (Update_Security_ProfileButton != null && Update_Security_ProfileButton.Enabled)
                {
                    this.Update_Security_ProfileButton.Enabled =
                        ((SecurityProfileProcedures.CanUserModifyResource(btnName, currUser, dSession)) ||
                         SecurityProfileProcedures.CanUserModifyResource(multipleLocationResource, currUser, dSession));
                }

            }
        }
    }
}
