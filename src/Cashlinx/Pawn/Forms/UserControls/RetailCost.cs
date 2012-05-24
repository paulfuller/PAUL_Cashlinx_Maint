using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Retail;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.UserControls
{
    public partial class RetailCost : UserControl
    {
        private const string F2 = "f2";

        #region public events
        public delegate void ViewItem(string icn);
        public event ViewItem ItemDetails;
        #endregion

        #region public properties
        public LayawayPaymentCalculator LayawayPaymentCalc
        {
            get { return ((CDS == null) ? null : CDS.LayawayPaymentCalc); }
            set 
            {   
                if (CDS == null) return;                    
                CDS.LayawayPaymentCalc = value; 
            }
        }
        public decimal SubTotal
        {
            get { return _subTotal; }
        }
        public decimal ShippingAndHandling
        {
            get { return _shippinAndHandling; }
            set
            {
                _shippinAndHandling = value;
            }
        }
        public SalesTaxInfo SalesTaxInfo { get; set; }
        public decimal TotalRetailCost
        {
            get
            {
                return Math.Round(_retailTotal, 2);
            }
        }

        #endregion

        #region Private Fields
        private bool _hasShipping = false;
        private decimal _subTotal = 0.0m;
        private decimal _shippinAndHandling = 0.0m;
        //private decimal _taxRate = 0.0m;
        private decimal _taxAmount = 0.0m;
        private decimal _retailTotal = 0.0m;
        private decimal _outTheDoorPrice = 0.0m;
        private decimal _subTotalRetailPrice = 0.0m;
        private decimal _outTheDoorSubTotal = 0.0m;
        private decimal _couponAmount = 0.0m;
        private decimal _transactionCouponAmount = 0.0m;
        #endregion

        #region Private Properties
        private DesktopSession CDS
        {
            get
            {
                if (GlobalDataAccessor.Instance == null)return(null);
                return GlobalDataAccessor.Instance.DesktopSession;
            }
        }
        #endregion

        #region Private Event Methods
        private void userItemCommentsForm_OnCommentsChanged(object sender, EventArgs e)
        {
            //listSelectedItems.Clear();
            PublishUserControls();
        }

        private void usercontrolRetailitem_EventGreaterNegotiatePriceFound(object sender, RetailItems.GreaterNegotiatePriceFoundEventArgs e)
        {
            if (e.GreaterNegotiatePriceFound)
                RaiseEnableDisableSaleAndLayaway(false);
            else
                RaiseEnableDisableSaleAndLayaway(true);
        }

        private void txtShippingAndHandling_Leave(object sender, EventArgs e)
        {
            if (txtShippingAndHandling.TextLength > 0)
            {
                decimal formattedShipping;
                bool formatSuccess;
                formatSuccess = Commons.FormatStringAsDecimal(txtShippingAndHandling.Text, out formattedShipping);
                if (formatSuccess)
                {
                    ShippingAndHandling = formattedShipping;
                    CalculateSubTotals();
                }
            }
        }

        private void txtBackgroundCheckFee_Leave(object sender, EventArgs e)
        {
            //Madhu BZ # 99
            if (txtBackgroundCheckFee.TextLength == 0)
                txtBackgroundCheckFee.Text = "0.00";

            decimal formattedCheckFee;
            bool formatSuccess = Commons.FormatStringAsDecimal(txtBackgroundCheckFee.Text, out formattedCheckFee);
            SetBackgroundCheckFee(formattedCheckFee);
            CalculateSubTotals();

            /*
              if (txtBackgroundCheckFee.TextLength > 0)
               {
                  decimal formattedCheckFee;
                  bool formatSuccess;
                  bool formatSuccess = Common.FormatStringAsDecimal(txtBackgroundCheckFee.Text, out formattedCheckFee);
                  if (formatSuccess)
                  {
                      SetBackgroundCheckFee(formattedCheckFee);
                      CalculateSubTotals();
                  }
            } */

        }

        private void ClearBackgroundCheckFee()
        {
            SetBackgroundCheckFee(-1);
        }

        private decimal GetBackgroundCheckFee()
        {
            if (CDS.ActiveRetail == null || CDS.ActiveRetail.Fees.Count == 0)
            {
                return 0;
            }

            return (from feeData in CDS.ActiveRetail.Fees
                    where feeData.FeeType == FeeTypes.BACKGROUND_CHECK_FEE
                    select feeData.Value).FirstOrDefault();
        }

        private void SetBackgroundCheckFee(decimal backgroundCheckFee)
        {
            if (CDS.ActiveRetail.Fees != null)
            {
                int idx = CDS.ActiveRetail.Fees.FindIndex(feeData => feeData.FeeType == FeeTypes.BACKGROUND_CHECK_FEE);
                if (idx >= 0)
                    CDS.ActiveRetail.Fees.RemoveAt(idx);
            }
            else
                CDS.ActiveRetail.Fees = new List<Fee>();

            if (backgroundCheckFee <= 0)
            {
                return;
            }

            Fee fee = new Fee()
            {
                FeeType = FeeTypes.BACKGROUND_CHECK_FEE,
                Value = Utilities.GetDecimalValue(backgroundCheckFee),
                OriginalAmount = Utilities.GetDecimalValue(backgroundCheckFee),
                FeeState = FeeStates.ASSESSED,
                FeeDate = Utilities.GetDateTimeValue(ShopDateTime.Instance.ShopTransactionTime, DateTime.Now)
            };
            CDS.ActiveRetail.Fees.Add(fee);
            txtBackgroundCheckFee.Text = backgroundCheckFee.ToString(F2);
        }

        private void retailItemControl_ItemClicked(object sender, RetailItems.RetailItemClickedEventArgs e)
        {
            /*SelectedItem = e.Item;
            //here 
            if (SelectedItem.Selected)
            {
                if (!listSelectedItems.Contains(SelectedItem))
                    listSelectedItems.Add(SelectedItem);
            }
            else
            {
                listSelectedItems.Remove(SelectedItem);
            }
            foreach (RetailItems retailItemControl in tableLayoutPanel1.Controls)
            {
                if (retailItemControl.Item.Equals(e.Item))
                {
                    retailItemControl.ResetUserControl(e.Item.Selected);
                }
            }*/
            SelectedItem = e.Item;
            foreach (RetailItems retailItemControl in tableLayoutPanel1.Controls)
            {
                if (!retailItemControl.Item.Equals(e.Item))
                {
                    retailItemControl.ResetUserControl();
                }
            }
        }

        private void usercontrolRetailitem_EditableFieldsChanged(object sender, RetailItems.EditableFieldsChangedEventArgs e)
        {
            bool ebayItemFound = ContainsEbayItem();
            HideShowShippingAndHandling(ebayItemFound);
            CalculateSubTotals();
        }

        private void usercontrolRetailitem_OnTotalsChanged(object sender, EventArgs e)
        {
            CalculateAllTotals();
        }

        private void customButtonOutTheDoor_Click(object sender, EventArgs e)
        {
            //_retailTotal = 225.05m;
            SetEditableFees();
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
            OutTheDoor outTheDoorForm = new OutTheDoor(100);
            outTheDoorForm.OutTheDoorPriceChanged += new EventHandler<OutTheDoor.OutTheDoorPriceChangedEventArgs>(outTheDoorForm_OutTheDoorPriceChanged);
            outTheDoorForm.ShowDialog();
            SetOutTheDoorItems();
        }

        private void outTheDoorForm_OutTheDoorPriceChanged(object sender, OutTheDoor.OutTheDoorPriceChangedEventArgs e)
        {
            OutTheDoorPrice = e.OutTheDoorPrice;
        }

        private void customButtonCoupon_Click(object sender, EventArgs e)
        {
            ApplyCoupon couponForm = new ApplyCoupon();
            couponForm.MdseDescription = string.Empty;
            CouponData couponDetails = new CouponData();
            couponDetails.CouponType = CouponData.CouponTypes.TRANSACTION;
            if (CDS.ActiveRetail.CouponAmount > 0)
            {
                couponDetails.CouponAmount = CDS.ActiveRetail.CouponAmount;
                couponDetails.CouponCode = CDS.ActiveRetail.CouponCode;
                couponDetails.CouponPercentage = CDS.ActiveRetail.CouponPercentage;
            }
            couponDetails.TransactionAmount = SubTotal;
            couponForm.CouponDetails = couponDetails;
            couponForm.ShowDialog();
            CDS.ActiveRetail.CouponPercentage = couponDetails.CouponPercentage;
            CDS.ActiveRetail.CouponAmount = Math.Round(couponDetails.CouponAmount, 2);
            CDS.ActiveRetail.CouponCode = couponDetails.CouponCode;
            CalculateAllTotals();
        }
        #endregion

        #region Private Methods
        private void SetOutTheDoorItems()
        {
            if (OutTheDoorPrice <= 0)
                return;

            var calc = new OutTheDoorCalculator();
            calc.BackgroundCheckFee = GetBackgroundCheckFee();
            calc.RetailItems = CDS.ActiveRetail.RetailItems;
            calc.SalesTaxInfo = SalesTaxInfo;
            calc.ShippingAndHandlingCost = _shippinAndHandling;
            calc.Calculate(OutTheDoorPrice);

            _outTheDoorSubTotal = calc.GetCalculatedTotalNegotiatedPrice();
            if (calc.HasItemWithNegotiatedPriceGreaterThanRetail)
            {
                MessageBox.Show("Discounted Price cannot be greater than Retail price for any of the items.");
                RaiseEnableDisableSaleAndLayaway(false);
                return;
            }
            else
            {
                RaiseEnableDisableSaleAndLayaway(true);
            }
            PublishUserControls();
            CalculateAllTotals();
            OutTheDoorPrice = 0.0m;
        }

        private void CheckOverride()
        {

        }

        private void SetFormValues()
        {
            ClearForm();
            txtSubTotal.Text = Math.Round(_subTotal, 2).ToString(F2);
            txtShippingAndHandling.Text = ShippingAndHandling.ToString(F2);
            txtRetailTotal.Text = Math.Round(_retailTotal, 2).ToString(F2);
            SetEstimatedTaxLabel();
            txtTaxAmount.Text = Math.Round(_taxAmount, 2).ToString(F2);
            txtBackgroundCheckFee.Text = GetBackgroundCheckFee().ToString(F2);
            textBoxCouponAmt.Text = _transactionCouponAmount.ToString(F2);
            if (LayawayPaymentCalc != null)
            {
                var bProcedures = new BusinessRulesProcedures(CDS);
                if (bProcedures.IsServiceFeeTaxable(CDS.CurrentSiteId))
                {
                    txtLayawayServiceFee.Text = LayawayPaymentCalc.ServiceFee.ToString(F2);
                }
                else
                    txtLayawayServiceFee.Text = LayawayPaymentCalc.ServiceFeeTotal.ToString(F2);
            }

            customButtonOutTheDoor.Enabled = _subTotal > 0.0m;
            customButtonCoupon.Location = new Point(customButtonOutTheDoor.Location.X, customButtonOutTheDoor.Location.Y - 118);
        }

        private void SetEstimatedTaxLabel()
        {
            labTaxRate.Text = string.Format("{0}Tax {1}: %", CDS.LayawayMode ? "Estimated " : "", SalesTaxInfo.AdjustedTaxRate.ToString(F2));
        }

        private void ClearForm()
        {
            txtSubTotal.Text = string.Empty;
            txtShippingAndHandling.Text = string.Empty;
            txtRetailTotal.Text = string.Empty;
            txtTaxAmount.Text = string.Empty;
            txtBackgroundCheckFee.Text = string.Empty;
        }

        public void ReloadData()
        {
            decimal backGroundCheckFee = GetBackgroundCheckFee();
            PublishUserControls();
            SetBackgroundCheckFee(backGroundCheckFee);
        }

        private void PublishUserControls()
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.Height = 0;
            if (CDS.ActiveRetail.RetailItems.Count > 0)
            {
                tableLayoutPanel1.Controls.Clear();
                tableLayoutPanel1.Height = 0;
                bool hasGun = false;
                bool hasHandGun = false;
                bool hasLongGun = false;
                txtBackgroundCheckFee.Text = GetBackgroundCheckFee().ToString(F2);
                List<RetailItem> caccItems = CDS.ActiveRetail.RetailItems.FindAll(ri => ri.mDocType == "7");
                decimal caccRetailPrice = 0;
                string caccIcn = string.Empty;
                if (caccItems.Count > 0)
                {
                    caccRetailPrice = caccItems[0].NegotiatedPrice;
                    caccIcn = caccItems[0].Icn;

                }
                for (int i = 1; i < caccItems.Count; i++)
                {
                    if (caccItems[i].Icn == caccIcn && caccItems[i].NegotiatedPrice == caccRetailPrice)
                    {
                        MessageBox.Show("CACC items should be combined");
                        CDS.ActiveRetail.RetailItems.Remove(caccItems[i]);
                    }
                }
                foreach (RetailItem item in CDS.ActiveRetail.RetailItems)
                {
                    RetailItems usercontrolRetailitem = new RetailItems(item);
                    usercontrolRetailitem.Item.Quantity = item.Quantity > 0 ? item.Quantity : 1;
                    tableLayoutPanel1.Height = tableLayoutPanel1.Height + usercontrolRetailitem.Height;
                    usercontrolRetailitem.ItemClicked += new EventHandler<RetailItems.RetailItemClickedEventArgs>(retailItemControl_ItemClicked);
                    usercontrolRetailitem.OnTotalsChanged += new EventHandler(usercontrolRetailitem_OnTotalsChanged);
                    usercontrolRetailitem.ShowItemDetails += new RetailItems.ShowDescribeItem(usercontrolRetailitem_ShowItemDetails);
                    usercontrolRetailitem.EditableFieldsChanged += new EventHandler<RetailItems.EditableFieldsChangedEventArgs>(usercontrolRetailitem_EditableFieldsChanged);
                    usercontrolRetailitem.EventGreaterNegotiatePriceFound += new EventHandler<RetailItems.GreaterNegotiatePriceFoundEventArgs>(usercontrolRetailitem_EventGreaterNegotiatePriceFound);
                    usercontrolRetailitem.ReCalculatePayments += new EventHandler(usercontrolRetailitem_ReCalculatePayments);
                    if (usercontrolRetailitem.Item.RetailPrice <= 0.0m)
                        usercontrolRetailitem.RaiseGreaterNegotiatePriceFoundEventArgs(true);


                    if (item.IsHandGun())
                    {
                        hasGun = true;
                        hasHandGun = true;
                    }
                    else if (item.IsLongGun())
                    {
                        hasGun = true;
                        hasLongGun = true;
                    }

                    CalculateAllTotals();
                    tableLayoutPanel1.Controls.Add(usercontrolRetailitem);


                }
                HideShowShippingAndHandling(ContainsEbayItem());
                HideShowBackgroundCheckFee();
                lblGunMessage.Visible = hasGun;

                if (hasGun)
                {
                    if (hasLongGun && hasHandGun)
                        lblGunMessage.Text = "The age limit for selling or pawning a hand gun is " + new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMinimumAgeHandGun(GlobalDataAccessor.Instance.CurrentSiteId) + " while for a long gun it is " + new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMinimumAgeLongGun(GlobalDataAccessor.Instance.CurrentSiteId);
                    else if (!hasLongGun && hasHandGun)
                        lblGunMessage.Text = "The age limit for selling or pawning a hand gun is " + new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMinimumAgeHandGun(GlobalDataAccessor.Instance.CurrentSiteId);
                    else if (hasLongGun && !hasHandGun)
                        lblGunMessage.Text = "The age limit for selling or pawning a long gun is " + new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMinimumAgeLongGun(GlobalDataAccessor.Instance.CurrentSiteId);
                }
            }
            else
            {
                ClearForm();
                RaiseHideRetailPanelOnZeroItems(false);
            }
        }

        void usercontrolRetailitem_ReCalculatePayments(object sender, EventArgs e)
        {
            if (LayawayPaymentCalc != null)
            {

                LayawayPaymentCalc.CalculateDefaultValues(Math.Round(TotalRetailCost));
            }
            else
                PublishUserControls();
        }

        private void usercontrolRetailitem_ShowItemDetails(string icn)
        {
            ItemDetails(icn);
        }

        private void RaiseEditableFieldsChanged(object sender, EventArgs e)
        {
            if (EditableFieldsChanged == null)
            {
                return;
            }
            EditableFieldsChanged(sender, e);
        }

        //private void PopulateUsercontrols()
        //{
        //    if (CDS.ActiveRetail.Items.Count > 0)
        //    {
        //        int counter = 1;
        //        foreach (Item retailItem in CDS.ActiveRetail.Items)
        //        {
        //            UserControls.RetailItems usercontrolRetailitem = new CashlinxDesktop.UserControls.RetailItems();
        //            usercontrolRetailitem.ICN = retailItem.Icn;
        //            usercontrolRetailitem.Description = retailItem.TicketDescription;
        //            usercontrolRetailitem.RetailPriceDecimal = retailItem.RetailPrice;
        //            usercontrolRetailitem.Qty = retailItem.Quantity;
        //            usercontrolRetailitem.UserControlID = counter;
        //            counter++;
        //            pnlItemCost.Controls.Add(usercontrolRetailitem);
        //            //now add userontrol to Location
        //        }
        //    }
        //}
        private void AddItemToList(RetailItem item)
        {
            item.NegotiatedPrice = item.RetailPrice;
            item.DiscountPercent = 0.0m;
            CDS.ActiveRetail.RetailItems.Add(item);
            PublishUserControls();
        }

        private void RearrangePanels()
        {
            panelBackgroundCheckFee.Visible = HasGunItem() && !CDS.LayawayMode;
            panelShippingAndHandling.Visible = _hasShipping && !CDS.LayawayMode;
            panelLayawayServiceFee.Visible = CDS.LayawayMode;

            if (!panelBackgroundCheckFee.Visible)
            {
                //txtBackgroundCheckFee.Text = string.Empty;
                if (txtBackgroundCheckFee.TextLength == 0)
                    txtBackgroundCheckFee.Text = "0.00";
                ClearBackgroundCheckFee();
            }
            if (!panelShippingAndHandling.Visible)
            {
                txtShippingAndHandling.Text = string.Empty;
                CDS.ActiveRetail.ShippingHandlingCharges = 0.0m;
                _shippinAndHandling = 0.0m;
            }
            SetEstimatedTaxLabel();
            lblRetailTotal.Text = CDS.LayawayMode ? "Layaway Total: $" : "Retail Total: $";
        }

        private void HideShowBackgroundCheckFee()
        {
            panelBackgroundCheckFee.Visible = HasGunItem();
            RearrangePanels();
        }

        private bool HasGunItem()
        {
            return CDS.ActiveRetail.RetailItems.Any(i => i.IsGun);
        }

        private void HideShowShippingAndHandling(bool enable)
        {
            panelShippingAndHandling.Visible = enable;
            _hasShipping = enable;
            RearrangePanels();
        }

        private void ResetItems()
        {
            if (CDS.ActiveRetail != null)
            {
                foreach (RetailItem retailItem in CDS.ActiveRetail.RetailItems)
                {
                    if (retailItem.OutTheDoor)
                    {
                        retailItem.OutTheDoor = false;
                        retailItem.NegotiatedPrice = retailItem.RetailPrice;
                        retailItem.DiscountPercent = 0;
                    }
                }
                OutTheDoorPrice = 0.0m;
                _outTheDoorSubTotal = 0.0m;
            }
        }


        private void CalculateSubTotals()
        {
            //_taxRate = 8.25m;
            //_backGroundCheckfee = 0.0m; //need to get more info on setting this value.

            //_taxAmount = TaxExclusionInfo.AdjustedTaxRate * _subTotal / 100;
            //_taxAmount = _taxAmount + _outTheDoorTaxAmt;
            //_subTotal = _subTotal + _outTheDoorSubTotal;
            if (LayawayPaymentCalc == null)
            {
                LayawayPaymentCalc = GlobalDataAccessor.Instance.DesktopSession.LayawayPaymentCalc;
            }
            decimal subTotal = Math.Round(SubTotal, 2);
            if (CDS.LayawayMode)
            {
                BusinessRulesProcedures bProcedures = new BusinessRulesProcedures(CDS);
                if (bProcedures.IsServiceFeeTaxable(CDS.CurrentSiteId))
                {
                    this._taxAmount = this.SalesTaxInfo.CalculateTax(subTotal - this._transactionCouponAmount) + this.LayawayPaymentCalc.ServiceFeeTax;
                    this._retailTotal = subTotal + this.LayawayPaymentCalc.ServiceFee + this._taxAmount + GetBackgroundCheckFee() - this._transactionCouponAmount;//plus other totals after they are calculated
                    if (_transactionCouponAmount > subTotal + this.LayawayPaymentCalc.ServiceFee + GetBackgroundCheckFee())
                    {
                        MessageBox.Show("Transaction level coupon is more than the total retail price. Cannot proceed");

                    }

                }
                else
                {
                    this._taxAmount = this.SalesTaxInfo.CalculateTax(subTotal - this._transactionCouponAmount);
                    this._retailTotal = subTotal + this.LayawayPaymentCalc.ServiceFeeTotal + this._taxAmount + GetBackgroundCheckFee() - this._transactionCouponAmount;//plus other totals after they are calculated
                    if (_transactionCouponAmount > subTotal + this.LayawayPaymentCalc.ServiceFeeTotal + GetBackgroundCheckFee())
                    {
                        MessageBox.Show("Transaction level coupon is more than the total retail price. Cannot proceed");

                    }

                }



            }
            else
            {
                _taxAmount = SalesTaxInfo.CalculateTax(subTotal - _transactionCouponAmount);
                _retailTotal = Math.Round(subTotal + ShippingAndHandling + _taxAmount + GetBackgroundCheckFee() - _transactionCouponAmount, 2);//plus other totals after they are calculated
                if (_transactionCouponAmount > subTotal + ShippingAndHandling + GetBackgroundCheckFee())
                {
                    MessageBox.Show("Transaction level coupon is more than the total retail price. Cannot proceed");

                }


            }
            //customButtonOutTheDoor.Enabled = _retailTotal > 0;

            SetFormValues();
            RaiseTotalsChanged();
        }

        private bool SetEditableFees()
        {
            if (txtShippingAndHandling.TextLength > 0)
            {
                decimal formattedShippingPrice;
                bool formatSuccess;
                formatSuccess = Commons.FormatStringAsDecimal(txtShippingAndHandling.Text, out formattedShippingPrice);
                if (formatSuccess)
                {
                    if (formattedShippingPrice < 0.0m)
                    {
                        MessageBox.Show("Shipping and Handling cannot be less than zero.");
                        txtShippingAndHandling.Focus();
                        return false;
                    }
                    else
                    {
                        _shippinAndHandling = formattedShippingPrice;
                    }
                }
                //CalculateSubTotals();
            }

            if (txtBackgroundCheckFee.TextLength > 0)
            {
                decimal formattedBackGroundFee;
                bool formatSuccess;
                formatSuccess = Commons.FormatStringAsDecimal(txtBackgroundCheckFee.Text, out formattedBackGroundFee);
                if (formatSuccess)
                {
                    if (formattedBackGroundFee < 0.0m)
                    {
                        MessageBox.Show("Background check fee cannot be less than zero.");
                        txtBackgroundCheckFee.Focus();
                        return false;
                    }
                    else
                    {
                        SetBackgroundCheckFee(formattedBackGroundFee);
                    }
                }
                //CalculateSubTotals();
            }
            return true;
        }
        #endregion

        #region Constructors
        public RetailCost()
        {
            InitializeComponent();
            listSelectedItems = new List<RetailItem>();
        }
        #endregion

        # region Public Properties
        public decimal OutTheDoorPrice
        {
            get { return _outTheDoorPrice; }
            set { _outTheDoorPrice = value; }
        }
        public RetailItem SelectedItem { get; set; }
        public List<RetailItem> listSelectedItems { get; set; }

        # endregion

        #region Public Methods
        public void RaiseEnableDisableSaleAndLayaway(bool enableDisable)
        {
            if (EnableDisableSaleAndLayaway == null)
                return;
            EnableDisableSaleAndLayaway(this, new EnableDisableSaleAndLayawayArgs(enableDisable));
            if (enableDisable)
            {
                foreach (RetailItems rItem in this.tableLayoutPanel1.Controls)
                {
                    if (rItem.PriceNotSet)
                        EnableDisableSaleAndLayaway(this, new EnableDisableSaleAndLayawayArgs(false));
                }
            }

        }

        public void RaiseHideRetailPanelOnZeroItems(bool hideRetailPanel)
        {
            if (HideRetailPanelOnZeroItems == null)
                return;
            HideRetailPanelOnZeroItems(this, new HideRetailPanelOnZeroItemsEventArgs(hideRetailPanel));
        }

        public void RaiseTotalsChanged()
        {
            if (TotalsChanged == null)
            {
                return;
            }

            TotalsChanged(this, EventArgs.Empty);
        }

        public void CalculateAllTotals()
        {
            _subTotal = 0.0m;
            _subTotalRetailPrice = 0.0m;
            _couponAmount = 0;
            _transactionCouponAmount = 0;
            foreach (RetailItem item in CDS.ActiveRetail.RetailItems)
            {
                _subTotal += Math.Max(item.TotalPrice, 0);
                _subTotalRetailPrice += (Math.Max(item.RetailPrice, 0) * item.Quantity);
                _couponAmount += item.CouponAmount;
            }
            if (CDS.ActiveRetail.CouponPercentage > 0)
            {
                _transactionCouponAmount = Math.Round((CDS.ActiveRetail.CouponPercentage / 100) * _subTotal, 2);
                CDS.ActiveRetail.CouponAmount = _transactionCouponAmount;
            }
            else
                _transactionCouponAmount = CDS.ActiveRetail.CouponAmount;

            CalculateSubTotals();
        }

        public bool OutTheDoorApplied()
        {
            if (_outTheDoorSubTotal > 0.0m)
                return true;
            else
                return false;
        }

        public bool ValidateShippingAndBackgroundFields(ref string shippingOrBackground)
        {
            //shippingOrBackground = string.Empty;
            if (panelShippingAndHandling.Visible)
            {
                if (txtShippingAndHandling.TextLength > 0)
                {
                    decimal formattedShipping;
                    bool formatSuccess;
                    formatSuccess = Commons.FormatStringAsDecimal(txtShippingAndHandling.Text, out formattedShipping);
                    if (formatSuccess)
                    {
                        if (formattedShipping <= 0.0m)
                        {
                            shippingOrBackground = "Shipping";
                            txtShippingAndHandling.Focus();
                            return false;
                        }
                    }
                }
                else
                {
                    shippingOrBackground = "Shipping";
                    txtShippingAndHandling.Focus();
                    return false;
                }
            }
            if (panelBackgroundCheckFee.Visible)
            {
                if (txtBackgroundCheckFee.TextLength > 0)
                {
                    decimal formattedBackgroundFee;
                    bool formatSuccess;
                    formatSuccess = Commons.FormatStringAsDecimal(txtBackgroundCheckFee.Text, out formattedBackgroundFee);
                    if (formatSuccess)
                    {
                        if (formattedBackgroundFee <= 0.0m)
                        {
                            shippingOrBackground = "Back ground Check Fee";
                            txtBackgroundCheckFee.Focus();
                            return false;
                        }
                    }
                }
                else
                {
                    shippingOrBackground = "Back ground Check Fee";
                    txtBackgroundCheckFee.Focus();
                    return false;
                }
            }
            return true;
        }

        public bool AddItem(RetailItem item)
        {
            string errorText = null;
            string errorCode = null;
            if (item.mDocType != "7" && item.mDocType != "9")
                RetailProcedures.LockItem(CDS, item.Icn, out errorCode, out errorText, "Y");

            AddItemToList(item);
            if (LayawayPaymentCalc != null)
            {
                LayawayPaymentCalc.CalculateDefaultValues(Math.Round(TotalRetailCost, 2));
            }
            return true;
        }

        public bool ItemIsDuplicate(RetailItem item)
        {
            if (CDS.ActiveRetail != null)
            {
                return CDS.ActiveRetail.RetailItems.Any(ri => (ri.Icn.Equals(item.Icn) && ri.mDocType == item.mDocType && ri.mDocType != "7" && ri.mDocType != "9"));

            }

            return false;
        }

        public void ReleaseLocks()
        {
            string errorText = null;
            string errorCode = null;
            if (CDS.ActiveRetail != null)
            {
                if (CDS.ActiveRetail.RetailItems.Count > 0)
                {
                    foreach (RetailItem i in CDS.ActiveRetail.RetailItems)
                    {
                        if (i.mDocType != "7" && i.mDocType != "9")
                            RetailProcedures.LockItem(CDS, i.Icn, out errorCode, out errorText, "N");
                    }
                }
            }
        }

        public void AddComments()
        {
            listSelectedItems = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems;
            if (listSelectedItems != null && listSelectedItems.Count > 0)
            {
                UserItemCommentsForm userItemCommentsForm = new UserItemCommentsForm(listSelectedItems);
                userItemCommentsForm.OnCommentsChanged += new EventHandler(userItemCommentsForm_OnCommentsChanged);
                userItemCommentsForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must first search and add an item.");
            }
        }

        public void DeleteItem()
        {
            if (SelectedItem != null)
            {
                CDS.ActiveRetail.RetailItems.Remove(SelectedItem);
                if (SelectedItem.OutTheDoor)
                    ResetItems();
                string errorText = null;
                string errorCode = null;
                if (SelectedItem.mDocType != "7" && SelectedItem.mDocType != "9")
                    RetailProcedures.LockItem(CDS, SelectedItem.Icn, out errorCode, out errorText, "N");
                SelectedItem = null;
                PublishUserControls();

                if (LayawayPaymentCalc != null)
                {
                    LayawayPaymentCalc.CalculateDefaultValues(Math.Round(TotalRetailCost, 2));
                }
            }
            
            /*if (listSelectedItems != null && listSelectedItems.Count > 0)
            {
                foreach (RetailItem item in listSelectedItems)
                {
                    CDS.ActiveRetail.RetailItems.Remove(item);
                    if (item.OutTheDoor)
                        ResetItems();
                    string errorText = null;
                    string errorCode = null;
                    if (item.mDocType != "7" && item.mDocType != "9")
                        RetailProcedures.LockItem(CDS, item.Icn, out errorCode, out errorText, "N");
                }
                //SelectedItem = null;
                PublishUserControls();
                if (LayawayPaymentCalculator != null)
                {
                    LayawayPaymentCalculator.CalculateDefaultValues(Math.Round(TotalRetailCost, 2));
                }
            }
            else
            {
                MessageBox.Show("You must first click and select an item to delete.");
            }*/
        }

        public void ClearOutTheDoor()
        {
            foreach (RetailItem item in CDS.ActiveRetail.RetailItems)
            {
                item.NegotiatedPrice = item.RetailPrice;
                item.DiscountPercent = 0.0m;
            }
        }

        public void ChangeToLayawayMode()
        {
            ClearBackgroundCheckFee();
            PublishUserControls();
            Refresh();
        }
        #endregion

        #region Public Event declarations
        public event EventHandler EditableFieldsChanged;
        public event EventHandler<EnableDisableSaleAndLayawayArgs> EnableDisableSaleAndLayaway;
        public event EventHandler<HideRetailPanelOnZeroItemsEventArgs> HideRetailPanelOnZeroItems;
        public event EventHandler TotalsChanged;
        # endregion

        #region Helper Classes
        public class EnableDisableSaleAndLayawayArgs : EventArgs
        {
            public EnableDisableSaleAndLayawayArgs(bool enableDisable)
            {
                EnableDisable = enableDisable;
            }
            public bool EnableDisable { get; set; }
        }

        public class HideRetailPanelOnZeroItemsEventArgs : EventArgs
        {
            public HideRetailPanelOnZeroItemsEventArgs(bool hideRetailPanel)
            {
                HideRetailPanel = hideRetailPanel;
            }
            public bool HideRetailPanel { get; set; }
        }
        #endregion

        private void RetailCost_Paint(object sender, PaintEventArgs e)
        {
            if (!DesignMode)
            {
                if (CDS != null)
                {
                    customButtonOutTheDoor.Visible = !CDS.LayawayMode;
                }
            }
        }

        public bool ContainsEbayItem()
        {
            var ebayItem = (from item in CDS.ActiveRetail.RetailItems
                            where item.SaleType == SaleType.Ebay
                            select item).FirstOrDefault();

            return ebayItem != null;
        }

        public bool ContainsNonEbayItem()
        {
            var nonEbayItem = (from item in CDS.ActiveRetail.RetailItems
                               where item.SaleType != SaleType.Ebay
                               select item).FirstOrDefault();

            return nonEbayItem != null;
        }

        //Madhu BZ # 99
        private void TxtBackgroundCheckFee_KeyDown(object sender, KeyPressEventArgs e)
        {
            System.Globalization.NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;

            if (!Char.IsDigit(e.KeyChar) && !(e.KeyChar.ToString()).Equals(decimalSeparator))
                if (e.KeyChar != (char)Keys.Back)
                    e.Handled = true;
        }





    }
}
