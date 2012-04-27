using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Authorization;
using Pawn.Logic;

namespace Pawn.Forms.Panels.MenuPanels
{
    public partial class RefundReturnMenuPanel : UserControl
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

        public RefundReturnMenuPanel()
        {
            InitializeComponent();
        }

        private void RefundReturnMenuPanel_Load(object sender, EventArgs e)
        {
            this.buttonControllers = null;
            UserControl panelInstance = (UserControl)sender;
            this.menuController = new MenuLevelController(panelInstance);
            this.buttonControllers = new ImageButtonControllerGroup(GlobalDataAccessor.Instance.DesktopSession, this.Controls);
        }

        private void RefundReturnMenuPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !DesignMode)
            {
                //Check if user has permissions to do returns on customer purchase
                UserVO currUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;
                string btnName = "RETURNCUSTOMERBUY";
                this.ReturnCustomerBuyButton.Enabled = (SecurityProfileProcedures.CanUserViewResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession));
                btnName = "RETURNVENDORBUY";
                ReturnVendorBuyButton.Enabled = (SecurityProfileProcedures.CanUserViewResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession));
                btnName = "RETAILSALEREFUND";
                this.RefundSaleButton.Enabled = (SecurityProfileProcedures.CanUserViewResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession));
                btnName = "LAYAWAYPAYMENTREFUND";
                this.RefundLayawayButton.Enabled = (SecurityProfileProcedures.CanUserViewResource(btnName, currUser, GlobalDataAccessor.Instance.DesktopSession));
            }
        }
    }
}
