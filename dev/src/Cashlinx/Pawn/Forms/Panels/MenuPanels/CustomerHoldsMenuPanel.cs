using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Pawn.Logic;

namespace Pawn.Forms.Panels.MenuPanels
{
    public partial class CustomerHoldsMenuPanel : UserControl
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

        public CustomerHoldsMenuPanel()
        {
            InitializeComponent();            
        }

        private void LookupMenuPanel_Load(object sender, EventArgs e)
        {
            this.buttonControllers = null;
            UserControl panelInstance = (UserControl)sender;
            this.menuController = new MenuLevelController(panelInstance);
            this.buttonControllers = new ImageButtonControllerGroup(GlobalDataAccessor.Instance.DesktopSession, this.Controls);
        }
    }
}
