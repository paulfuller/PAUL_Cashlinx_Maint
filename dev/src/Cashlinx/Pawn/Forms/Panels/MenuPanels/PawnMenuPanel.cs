using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Authorization;
using Pawn.Logic;

namespace Pawn.Forms.Panels.MenuPanels
{
    public partial class PawnMenuPanel : UserControl
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

        public PawnMenuPanel()
        {
            InitializeComponent();
        }

        private void PawnMenuPanel_Load(object sender, EventArgs e)
        {
            this.buttonControllers = null;
            var panelInstance = (UserControl)sender;
            this.menuController = new MenuLevelController(panelInstance);
            this.buttonControllers = new ImageButtonControllerGroup(GlobalDataAccessor.Instance.DesktopSession, this.Controls);

        }

        private void PawnMenuPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !DesignMode)
            {
                //Check if user has access to New Pawn Loan
                var dSession = GlobalDataAccessor.Instance.DesktopSession;
                var currUser = dSession.LoggedInUserSecurityProfile;
                var newPawnLoanButton = this.NewPawnLoanButton;
                if (newPawnLoanButton != null)
                {
                    var idx = newPawnLoanButton.Name.IndexOf("Button", System.StringComparison.Ordinal);
                    var btnName = NewPawnLoanButton.Name.Substring(0, idx).ToUpper();
                    if (!(SecurityProfileProcedures.CanUserViewResource(btnName, currUser, dSession)))
                        newPawnLoanButton.Enabled = false;
                    else
                        newPawnLoanButton.Enabled = true;
                }
            }

        }

  
    }
}
