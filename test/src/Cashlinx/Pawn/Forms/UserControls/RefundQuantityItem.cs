using System;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;

namespace Pawn.Forms.UserControls
{
    public partial class RefundQuantityItem : UserControl
    {
        private bool _selected;

        # region Constructors

        public RefundQuantityItem(RetailItem item)
        {
            InitializeComponent();
            Item = item;
        }

        # endregion

        # region Properties

        public RetailItem Item { get; set; }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                SetSelectedValue(value);
            }
        }

        # endregion

        # region Public Methods

        public int GetQuantity()
        {
            return Utilities.GetIntegerValue(txtRefundQuantity.Text);
        }

        # endregion

        # region Events

        public event EventHandler SelectionChanged;

        # endregion

        # region Event Handlers

        private void RefundQuantityItem_Paint(object sender, PaintEventArgs e)
        {
            lblIcn.Text = Item.Icn;
            lblDescription.Text = Item.TicketDescription;
            lblDiscount.Text = Item.DiscountPercent.ToString();
            lblQuantity.Text = Item.Quantity.ToString();
            lblRetailPrice.Text = Item.RetailPrice.ToString("c");
            lblTotal.Text = Utilities.GetDecimalValue(Item.Quantity * Item.RetailPrice, 0).ToString();

            this.BackColor = Selected ? Color.LightYellow : Color.Transparent;
            txtRefundQuantity.Enabled = Selected;
        }

        private void RefundQuantityItem_Click(object sender, EventArgs e)
        {
            Selected = !Selected;
        }

        # endregion

        # region Helper Methods

        private void SetSelectedValue(bool value)
        {
            bool changed = _selected != value;
            _selected = value;

            if (changed && _selected)
            {
                txtRefundQuantity.Text = Item.Quantity.ToString();
            }
            else if (changed)
            {
                txtRefundQuantity.Text = string.Empty;
            }

            if (changed && SelectionChanged != null)
            {
                SelectionChanged(this, EventArgs.Empty);
            }

            Refresh();
        }

        # endregion
    }
}
