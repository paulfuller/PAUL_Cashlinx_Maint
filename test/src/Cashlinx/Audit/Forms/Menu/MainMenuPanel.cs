using System;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow;

namespace Audit.Panels.MenuPanels
{
    public partial class MainMenuPanel : MenuPanelBase
    {
        public MainMenuPanel()
        {
            InitializeComponent();
        }

        private System.EventHandler exitButtonHandler;

        public void setExitButtonHandler(System.EventHandler evtHandler)
        {
            this.exitButtonHandler = evtHandler;
            this.exitButton.Click += evtHandler;
        }

        private void btnInventoryAudit_Click(object sender, EventArgs e)
        {
            //if (!LogIn())
            //{
            //    return;
            //}
        }

        private void MainMenuPanel_Load(object sender, EventArgs e)
        {
            this.ButtonControllers = null;
            UserControl panelInstance = (UserControl)sender;
            this.MenuController = new MenuLevelController(panelInstance);
            this.ButtonControllers = new ImageButtonControllerGroup(ADS, this.Controls);
        }
    }
}
