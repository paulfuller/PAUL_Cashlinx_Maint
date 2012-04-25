using System;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow;
using Support.Logic;

namespace Support.Forms.Panels.MenuPanels
{
    public partial class ShopAdminMenuPanel : UserControl
    {
        private ImageButtonControllerGroup buttonControllers;
        private MenuLevelController menuController;

        public ShopAdminMenuPanel()
        {
            InitializeComponent();
        }

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

        private void ShopAdminMenuPanel_Load(object sender, EventArgs e)
        {
            this.buttonControllers = null;
            var panelInstance = (UserControl)sender;
            this.menuController = new MenuLevelController(panelInstance);
            this.buttonControllers = new ImageButtonControllerGroup(CashlinxPawnSupportSession.Instance, this.Controls);
        }



    }
}
