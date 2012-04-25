using System.Drawing;
using System.Windows.Forms;

namespace Support.Libraries.Forms
{
    public partial class FlowTabController : UserControl
    {
        public enum State
        {
            Customer = 0,
            ProductsAndServices = 1,
            ProductHistory = 2,
            ItemHistory = 3,
            Stats = 4,
            None = 5
        }

        private Form belowForm;
        public Form BelowForm
        {
            set
            {
                belowForm = value;
            }
            get
            {
                return belowForm;
            }
        }


        private State tabState;
        public State TabState
        {
            get
            {
                return (this.tabState);
            }
            set
            {
                tabState = value;
                if (tabState == FlowTabController.State.Customer)
                    this.controllerTabs.SelectedTab = customerTab;
                if (tabState == FlowTabController.State.ProductsAndServices)
                    this.controllerTabs.SelectedTab = productsServicesTab;
                if (tabState == FlowTabController.State.Stats)
                    this.controllerTabs.SelectedTab = statsTab;
            }
        }

        public delegate void TabChangedHandler();

        private TabChangedHandler tabHandler;
        public TabChangedHandler TabHandler
        {
            set
            {
                this.tabHandler = value;
            }
 
            get
            {
                return this.tabHandler;
            }
        }
        


        public FlowTabController()
        {
            InitializeComponent();
            this.controllerTabs.DrawMode = TabDrawMode.OwnerDrawFixed;            
        }

        public void setTabEnabledFlag(State tab, bool enableVal)
        {
            switch (tab)
            {
                case State.Customer:
                        this.customerTab.Enabled = enableVal;
     
                    break;
                case State.ItemHistory:

                    this.itemHistoryTab.Enabled = enableVal;
                    break;
                case State.ProductHistory:
                    this.productHistoryTab.Enabled = enableVal;
                    break;
                case State.ProductsAndServices:
                    this.productsServicesTab.Enabled = enableVal;
                    break;
                case State.Stats:
                    this.statsTab.Enabled = enableVal;
                    break;
            }
        }


        private void controllerTabs_DrawItem(object sender, DrawItemEventArgs e)
        {

            var g = e.Graphics;
            var tp = controllerTabs.TabPages[e.Index];
            Brush brColor;
            var r = new RectangleF(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width+1, e.Bounds.Height - 1);
            var sf = new StringFormat();
            Font xFont;
            sf.Alignment = StringAlignment.Center;
            var strTitle = tp.Text;
            if (tp.Enabled)
            {
                brColor = new SolidBrush(Color.Black);
                xFont = new Font(controllerTabs.Font, FontStyle.Bold);
                g.DrawString(strTitle, xFont, brColor, r, sf);    //Draw the labels.
            }
            else
            {
                brColor = new SolidBrush(Color.Gray);
                xFont = new Font(controllerTabs.Font, FontStyle.Regular);
                g.DrawString(strTitle, xFont, brColor, r, sf);    //Draw the labels.
            }


            g.Dispose();
            brColor.Dispose();


        }




        private void controllerTabs_Selected(object sender, TabControlEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                return;
            }

            if (e.TabPage == this.customerTab)
            {
                this.tabState = State.Customer;
            }
            else if (e.TabPage == this.productsServicesTab)
            {
                this.tabState = State.ProductsAndServices;
            }
            else if (e.TabPage == this.productHistoryTab)
            {
                this.tabState = State.ProductHistory;
            }
            else if (e.TabPage == this.itemHistoryTab)
            {
                this.tabState = State.ItemHistory;
            }
            else if (e.TabPage == this.statsTab)
            {
                this.tabState = State.Stats;
            }

            this.tabHandler.Invoke();
        }

        private void controllerTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                return;
            }

        }
    }
}
