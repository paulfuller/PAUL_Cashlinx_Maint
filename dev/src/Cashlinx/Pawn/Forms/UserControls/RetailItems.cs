using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components.Behaviors;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Retail;

namespace Pawn.Forms.UserControls
{
    public partial class RetailItems : UserControl
    {
        #region public events
        public delegate void ShowDescribeItem(string icn);

        public event ShowDescribeItem ShowItemDetails;
        public event EventHandler ReCalculatePayments;

        #endregion

        #region Private Properties
        private bool ControlAlreadyFocused { get; set; }

        #endregion

        #region Private Event Methods
        private void RetailItems_Load(object sender, EventArgs e)
        {
            SetFormValues();
        }

        private void RetailItems_OnEditableFieldsChanged()
        {
        }

        private void cmbSaleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSaleType.SelectedItem == null)
            {
                return;
            }
            SaleTypePair pair = cmbSaleType.SelectedItem as SaleTypePair;
            Item.SaleType = pair.SaleType;
            RaiseEditableFieldsChanged(Item);
        }

        private void RetailItems_Click(object sender, EventArgs e)
        {
            //write code to change background color of current control when clicked
            ActionsControlFocus();
        }

        private void cmbSaleType_Click(object sender, EventArgs e)
        {
            ActionsControlFocus();
        }

        private void lblICN_Click(object sender, EventArgs e)
        {
            ActionsControlFocus();
            if (!IsCaccOrNxt())
                ShowItemDetails(lblICN.Text);
        }

        private void lblDescription_Click(object sender, EventArgs e)
        {
            ActionsControlFocus();
        }

        private void lblRetailPrice_Click(object sender, EventArgs e)
        {
            ActionsControlFocus();
        }

        private void txtDiscount_Click(object sender, EventArgs e)
        {
            ActionsControlFocus();
        }

        private void txtNegotiatedPrice_Click(object sender, EventArgs e)
        {
            ActionsControlFocus();
        }

        private void txtQty_Click(object sender, EventArgs e)
        {
            ActionsControlFocus();
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {
            ActionsControlFocus();
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            if (IsCaccOrNxt())
            {
                return;
            }

            //set new total for property
            //int selStart = txtDiscount.SelectionStart;
            //txtDiscount.SelectionStart = selStart;
            decimal originalDiscount = Item.DiscountPercent;
            decimal negotiatedPrice = Item.NegotiatedPrice;
            decimal decDiscount = 0.0m;
            if (txtDiscount.TextLength > 0)
            {
                bool parsed = false;

                parsed = Commons.FormatStringAsDecimal(txtDiscount.Text, out decDiscount);
                if (parsed)
                {
                    decDiscount = Math.Round(decDiscount, 2);
                    negotiatedPrice = Math.Round(Item.RetailPrice - (Item.RetailPrice * (decDiscount / 100)), 2);

                    if (decDiscount < 0 || decDiscount > 100 || negotiatedPrice > Item.RetailPrice)
                    {
                        MessageBox.Show("Discount percent cannot be less than 0 or greater than 100.");
                        HighlightControl(txtDiscount);
                        txtDiscount.Text = txtDiscount.PreviousValue;
                        RaiseGreaterNegotiatePriceFoundEventArgs(true);
                        txtDiscount.Focus();
                        return;
                    }



                }

            }

            if (Math.Round(originalDiscount, 2) != decDiscount)
            {
                Item.NegotiatedPrice = negotiatedPrice;
                Item.DiscountPercent = decDiscount;
                Item.CouponAmount = 0;
                Item.CouponPercentage = 0;
                Item.CouponCode = string.Empty;
                RaiseReCalculatePayments();
            }
            SetFormValues();
            RaiseEditableFieldsChanged(Item);
            PriceNotSet = false;
            RaiseGreaterNegotiatePriceFoundEventArgs(false);
            OnTotalsChanged(null, null);
        }

        private void txtNegotiatedPrice_Leave(object sender, EventArgs e)
        {
            //set new total for property
            //int selStart = txtNegotiatedPrice.SelectionStart;
            //txtNegotiatedPrice.SelectionStart = selStart;
            decimal originalPrice = Math.Round(Item.NegotiatedPrice, 2);
            if (txtNegotiatedPrice.TextLength > 0)
            {
                decimal formattedNegoPrice;
                bool formatSuccess;
                formatSuccess = Commons.FormatStringAsDecimal(txtNegotiatedPrice.Text, out formattedNegoPrice);
                if (formatSuccess)
                {
                    formattedNegoPrice = Math.Round(formattedNegoPrice, 2);
                    if (formattedNegoPrice < 0.0m)
                    {
                        MessageBox.Show("Negotiated price cannot be less than zero.");
                        HighlightControl(txtNegotiatedPrice);
                        txtNegotiatedPrice.Text = txtNegotiatedPrice.PreviousValue;
                        txtNegotiatedPrice.Focus();
                        return;
                    }
                    //SR 09/15/2011 BZ 1217
                    /*if (Item.RetailPrice >= 0.0m && formattedNegoPrice > Item.RetailPrice && !IsCaccOrNxt())
                    {
                        MessageBox.Show("Negotiated Price cannot be more than Retail Price");
                        HighlightControl(txtNegotiatedPrice);
                        txtNegotiatedPrice.Text = txtNegotiatedPrice.PreviousValue;
                        //Item.NegotiatedPrice = Item.RetailPrice;
                        RaiseGreaterNegotiatePriceFoundEventArgs(true);
                        //SetFormValues();
                        //txtNegotiatedPrice.Focus();
                        return;
                    }*/
                    Item.NegotiatedPrice = formattedNegoPrice;
                    if (IsCaccOrNxt() && formattedNegoPrice > Item.RetailPrice)
                        Item.DiscountPercent = 0;
                    else
                    {
                        Item.DiscountPercent = Item.CalculateDiscountPercentage(Item.RetailPrice, Item.NegotiatedPrice);
                    }
                }
            }
            if (originalPrice != Item.NegotiatedPrice)
            {
                var couponPresent = from rItem in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems
                                    where rItem.CouponAmount > 0
                                    select rItem;
                if (couponPresent.Any() || GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount > 0)
                {
                    MessageBox.Show("All Coupon amounts will be discarded");
                    foreach (RetailItem rItem in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems)
                    {
                        rItem.CouponAmount = 0;
                        rItem.CouponPercentage = 0;
                        rItem.CouponCode = string.Empty;
                    }
                    GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponAmount = 0;
                    GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponPercentage = 0;
                    GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CouponCode = string.Empty;
                }

                RaiseReCalculatePayments();
            }
            RaiseEditableFieldsChanged(Item);
            SetFormValues();
            PriceNotSet = false;
            RaiseGreaterNegotiatePriceFoundEventArgs(false);
            OnTotalsChanged(null, null);
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            //set new total for property
            int originalQuantity = Item.Quantity;
            if (txtQty.TextLength > 0)
            {
                int qty = Convert.ToInt32(txtQty.Text);
                if (qty <= 0)
                {
                    MessageBox.Show("Quantity cannot be less than or equal to zero.");
                    HighlightControl(txtQty);
                    txtQty.Text = txtQty.PreviousValue;
                    return;
                }
                else
                {
                    Item.Quantity = qty;
                }
            }
            else
            {
                Item.Quantity = 1;
            }
            if (originalQuantity != Item.Quantity)
            {
                RaiseReCalculatePayments();
            }
            RaiseEditableFieldsChanged(Item);
            SetFormValues();
            OnTotalsChanged(null, null);
        }

        private void chkSelectControl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectControl.Checked)
                this.BackColor = Color.LightGray;
            else
                this.BackColor = Color.LightYellow;
        }

        private void chkSelectControl_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkSelectControl.Checked)
            {
                this.BackColor = Color.LightGray;
            }
            else
            {
                this.BackColor = Color.LightYellow;
            }
        }

        private void btnCoupon_Click(object sender, EventArgs e)
        {
            ApplyCoupon couponForm = new ApplyCoupon();
            couponForm.MdseDescription = lblDescription.Text;
            CouponData couponDetails = new CouponData();
            couponDetails.CouponType = CouponData.CouponTypes.ITEM;
            couponDetails.ItemAmount = Utilities.GetDecimalValue(txtNegotiatedPrice.Text, 0) * 
                Utilities.GetIntegerValue(txtQty.Text, 1);
            if (Item.CouponAmount > 0)
            {
                couponDetails.CouponAmount = Item.CouponAmount;
                couponDetails.CouponCode = Item.CouponCode;
                couponDetails.CouponPercentage = Item.CouponPercentage;
            }

            couponForm.CouponDetails = couponDetails;
            couponForm.ShowDialog();
            Item.CouponPercentage = couponDetails.CouponPercentage;
            Item.CouponAmount = Math.Round(couponDetails.CouponAmount, 2);
            Item.CouponCode = couponDetails.CouponCode;
            RaiseReCalculatePayments();
            SetFormValues();
            OnTotalsChanged(null, null);

        }

        #endregion

        #region Private Methods
        private void RaiseTotalsChanged(object sender, EventArgs e)
        {
            if (OnTotalsChanged == null)
            {
                return;
            }
            OnTotalsChanged(sender, e);
        }

        private void ActionsControlFocus()
        {
            if (!ControlAlreadyFocused)
            {
                if (chkSelectControl.Checked)
                {
                    chkSelectControl.CheckState = CheckState.Unchecked;
                    chkSelectControl.Checked = false;
                    Item.Selected = false;
                }
                else
                {
                    chkSelectControl.CheckState = CheckState.Checked;
                    chkSelectControl.Checked = true;
                    Item.Selected = true;
                    ControlAlreadyFocused = true;
                }
                RaiseItemClickedEvent(Item);
            }
        }

        private void HighlightControl(Control control)
        {
            new ControlHighlighter(control, Color.LightPink).Execute();
        }

        private void RaiseEditableFieldsChanged(RetailItem item)
        {
            if (EditableFieldsChanged == null)
            {
                return;
            }
            EditableFieldsChanged(this, new EditableFieldsChangedEventArgs(item));
        }

        private void RaiseItemClickedEvent(RetailItem item)
        {
            if (ItemClicked == null)
            {
                return;
            }

            ItemClicked(this, new RetailItemClickedEventArgs(item));
        }

        private void RaiseReCalculatePayments()
        {
            if (ReCalculatePayments != null)
            {
                ReCalculatePayments(this, EventArgs.Empty);
            }
        }

        private void SetFormValues()
        {

            txtQty.Text = Item.Quantity.ToString();
            lblICN.Text = Item.Icn;
            if (string.IsNullOrEmpty(Item.UserItemComments))
            {
                lblComments.Text = string.Empty;
            }
            else
            {
                lblComments.Text = "Comment: " + Item.UserItemComments;
            }
            lblDescription.Text = Item.TicketDescription;
            lblRetailPrice.Text = Item.RetailPrice.ToString("c");
            //string strNegoPrice = string.Format("{0:C}", NegotiatedPriceDecimal);
            txtNegotiatedPrice.Text = Math.Round(Item.NegotiatedPrice, 2).ToString("f2");
            //string strDiscountPercent = string.Format("{0:P}", DiscountPercent/100);
            txtDiscount.Text = Math.Round(Item.DiscountPercent, 2).ToString("f2");
            labelCouponAmt.Text = Item.CouponAmount.ToString("f2");
            lblTotal.Text = Item.TotalPrice.ToString("c");

            txtQty.Enabled = IsCaccOrNxt();
        }

        private bool IsCaccOrNxt()
        {
            var icn = new Icn(Item.Icn);
            return icn.DocumentType == DocumentType.CaccItem || icn.DocumentType == DocumentType.NxtAndStandardDescriptor;
        }

        #endregion

        #region Constructors
        public RetailItems(RetailItem item)
        {
            InitializeComponent();
            Item = item;
            LoadSaleTypeCombo(item);
        }

        #endregion

        #region public properties
        public RetailItem Item { get; set; }

        #endregion

        #region Public Events declarations
        public event EventHandler OnTotalsChanged;
        public event EventHandler<RetailItemClickedEventArgs> ItemClicked;
        public event EventHandler<EditableFieldsChangedEventArgs> EditableFieldsChanged;
        public event EventHandler<GreaterNegotiatePriceFoundEventArgs> EventGreaterNegotiatePriceFound;

        #endregion

        #region Public Methods
        public void RaiseGreaterNegotiatePriceFoundEventArgs(bool greaterNegPriceFound)
        {
            if (EventGreaterNegotiatePriceFound == null)
                return;
            EventGreaterNegotiatePriceFound(this, new GreaterNegotiatePriceFoundEventArgs(greaterNegPriceFound));
        }



        public void SetItemValues()
        {
            bool discountTextChanged = false;
            bool negotiatedTextChanged = false;
            decimal decDiscount = 0.0m;
            decimal formattedNegoPrice = 0.0m;
            if (txtDiscount.TextLength > 0)
            {
                bool parsed = false;
                parsed = Commons.FormatStringAsDecimal(txtDiscount.Text, out decDiscount);
                if (parsed)
                {
                    if (Item.DiscountPercent != decDiscount)
                    {
                        discountTextChanged = true;
                    }
                }
            }
            if (txtNegotiatedPrice.TextLength > 0)
            {
                bool formatSuccess;
                formatSuccess = Commons.FormatStringAsDecimal(txtNegotiatedPrice.Text, out formattedNegoPrice);
                if (formatSuccess)
                {
                    if (Item.NegotiatedPrice != formattedNegoPrice)
                    {
                        negotiatedTextChanged = true;
                    }
                }
            }
            if (discountTextChanged)
            {
                Item.NegotiatedPrice = Item.RetailPrice - (Item.RetailPrice * (decDiscount / 100));
                Item.DiscountPercent = decDiscount;
                if (Item.RetailPrice > 0.0m && Item.NegotiatedPrice > Item.RetailPrice)
                {
                    MessageBox.Show("Discount percent cannot be less than 0.");
                    txtDiscount.Focus();
                    RaiseGreaterNegotiatePriceFoundEventArgs(true);
                    return;
                    //Item.NegotiatedPrice = Item.RetailPrice;
                    //Item.DiscountPercent = 0m;
                    //SetFormValues();
                    //txtDiscount.Focus();
                }
            }
            if (negotiatedTextChanged)
            {
                Item.NegotiatedPrice = formattedNegoPrice;
                //SR 09/15/2011 BZ 1217
                /*if (Item.RetailPrice > 0.0m && Item.NegotiatedPrice > Item.RetailPrice && !IsCaccOrNxt())
                {
                    MessageBox.Show("Negotiated Price cannot be more than Retail Price");
                    RaiseGreaterNegotiatePriceFoundEventArgs(true);
                    //Item.NegotiatedPrice = Item.RetailPrice;
                    SetFormValues();
                    //txtNegotiatedPrice.Focus();
                    return;
                }*/
                if (IsCaccOrNxt() && Item.NegotiatedPrice > Item.RetailPrice)
                    Item.DiscountPercent = 0;
                else
                {
                    Item.DiscountPercent = Item.CalculateDiscountPercentage(Item.RetailPrice, Item.NegotiatedPrice);
                }
            }
            //set new total for property
            if (txtQty.TextLength > 0)
            {
                Item.Quantity = Convert.ToInt32(txtQty.Text);
            }
            SetFormValues();
            RaiseEditableFieldsChanged(Item);
        }

        public void LoadSaleTypeCombo(RetailItem item)
        {
            cmbSaleType.Items.Clear();
            cmbSaleType.Items.Add(new SaleTypePair(SaleType.InShop, "In Shop"));
            cmbSaleType.Items.Add(new SaleTypePair(SaleType.CraigsList, "Craigslist"));
            //if(!CashlinxDesktopSession.Instance.LayawayMode)
            //    cmbSaleType.Items.Add(new SaleTypePair(SaleType.Ebay, "Ebay"));
            if (item.SaleType == SaleType.InShop)
                cmbSaleType.SelectedIndex = 0;
            else if (item.SaleType == SaleType.CraigsList)
                cmbSaleType.SelectedIndex = 1;
            else if (item.SaleType == SaleType.Ebay && !GlobalDataAccessor.Instance.DesktopSession.LayawayMode)
                cmbSaleType.SelectedIndex = 2;
            else
                cmbSaleType.SelectedIndex = 0;
        }

        public void ResetUserControl()
        {
            ControlAlreadyFocused = false;
            chkSelectControl.Checked = false;
        }

        public bool PriceNotSet = true;

        #endregion

        # region Helper Class
        public class GreaterNegotiatePriceFoundEventArgs : EventArgs
        {
            public GreaterNegotiatePriceFoundEventArgs(bool greaterNegotiatePriceFound)
            {
                GreaterNegotiatePriceFound = greaterNegotiatePriceFound;
            }

            public bool GreaterNegotiatePriceFound { get; set; }
        }

        public class SaleTypePair
        {
            public SaleTypePair(SaleType saleType, string description)
            {
                SaleType = saleType;
                Description = description;
            }

            public SaleType SaleType { get; set; }

            public string Description { get; set; }

            public override string ToString()
            {
                return Description;
            }
        }

        public class EditableFieldsChangedEventArgs : EventArgs
        {
            public EditableFieldsChangedEventArgs(RetailItem item)
            {
                Item = item;
            }

            public RetailItem Item { get; private set; }
        }

        public class RetailItemClickedEventArgs : EventArgs
        {
            public RetailItemClickedEventArgs(RetailItem item)
            {
                Item = item;
            }

            public RetailItem Item { get; private set; }
        }
        # endregion
    }
}
