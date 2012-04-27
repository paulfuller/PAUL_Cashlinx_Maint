using System;
using System.Windows.Forms;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.UserControls
{
    public partial class FirearmBuyoutItem : UserControl
    {
        #region Private Event Handlers
        private void txtPrice_Leave(object sender, EventArgs e)
        {
            if (txtPrice.TextLength > 0)
            {
                decimal formattedPrice;
                bool formatSuccess;
                formatSuccess = Commons.FormatStringAsDecimal(txtPrice.Text, out formattedPrice);
                if (formatSuccess)
                {
                    formattedPrice = Math.Round(formattedPrice, 2);
                    Item.ItemAmount = formattedPrice;
                }
            }
        }
        #endregion

        #region Constructor
        public FirearmBuyoutItem(Item item)
        {
            InitializeComponent();
            Item = item;
        }
        #endregion

        #region Public Properties
        public Item Item
        {
            get;
            set;
        }
        #endregion

        #region Public Methods
        public void SetItem(bool editable)
        {
            lblDescription.Text = Item.TicketDescription;
            if (editable)
            {
                txtPrice.Visible = true;
                lblPrice.Visible = false;
                txtPrice.Text = Math.Round(Item.ItemAmount, 2).ToString("f");
            }
            else
            {
                txtPrice.Visible = false;
                lblPrice.Visible = true;
                lblPrice.Text = Item.ItemAmount.ToString("C");
            }
        }
        #endregion
    }
}
