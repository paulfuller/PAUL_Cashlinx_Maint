using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Pawn.Logic;

namespace Pawn.Forms.Panels.MenuPanels
{
    public partial class MainMenuPanel : UserControl
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

        private System.EventHandler exitButtonHandler;

        public void setExitButtonHandler(System.EventHandler evtHandler)
        {
            this.exitButtonHandler = evtHandler;
            this.exitButton.Click += evtHandler;
        }

        public MainMenuPanel()
        {
            InitializeComponent();
        }

        private void MainMenuPanel_Load(object sender, EventArgs e)
        {
            this.buttonControllers = null;
            var panelInstance = (UserControl)sender;
            this.menuController = new MenuLevelController(panelInstance);
            this.buttonControllers = new ImageButtonControllerGroup(GlobalDataAccessor.Instance.DesktopSession, this.Controls);
        }
    }
}
