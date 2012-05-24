using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Retail;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Loan;
using Pawn.Forms.Pawn.Services.Pickup;
using Pawn.Forms.UserControls;
using Pawn.Logic;

namespace Pawn.Forms.Retail
{
    public partial class ItemSearch : CustomBaseForm
    {
        #region Fields
        public NavBox NavControlBox;
        private const string _MINUS = "-";
        private const string _PLUS = "+";
        private const string _RetailSaleHeading = "Retail - Sale";
        private const string _RetailLayawayHeading = "Retail - Layaway";
        private const string _ItemSearchHeading = "Item Search";
        private bool gunItemSale;
        private bool nxtItemOverride;
        #endregion

        # region Properties

        private DesktopSession CDS
        {
            get { return GlobalDataAccessor.Instance.DesktopSession; }
        }
        private bool IsExpandedSearchPanelVisible { get; set; }
        private bool IsItemSelected
        {
            get
            {
                return CDS.ActiveRetail != null && CDS.ActiveRetail.RetailItems.Count > 0;
            }
        }
        private SalesTaxInfo SalesTaxInfo
        {
            get { return CDS.ActiveRetail.SalesTaxInfo; }
            set { CDS.ActiveRetail.SalesTaxInfo = value; }
        }

        # endregion

        # region Constructors
        public ItemSearch()
        {
            InitializeComponent();
            customButtonFind.Enabled = false;
            NavControlBox = new NavBox
                            {
                                Owner = this
                            };
            if (CDS.ActiveRetail == null)
            {
                return;
            }
            if (SalesTaxInfo == null)
            {
                SalesTaxInfo = new SalesTaxInfo(CDS.CurrentSiteId.StoreTaxes);
            }
            retailCost1.SalesTaxInfo = SalesTaxInfo;
            this.MaximumSize = new Size(this.Size.Width, this.Size.Height);
            this.pnlSearchButtons.Tag = this.pnlSearchButtons.Location;
            this.pnlItemCost.Tag = this.pnlItemCost.Location;
            this.pnlItemCostButtons.Tag = this.pnlItemCostButtons.Location;
            pnlSearchDetails.ShowCategory += new DetailedItemSearch.ShowDescribeMerchandise(pnlSearchDetails_ShowCategory);
            retailCost1.ItemDetails += new RetailCost.ViewItem(retailCost1_ItemDetails);
            retailCost1.HideRetailPanelOnZeroItems += new System.EventHandler<RetailCost.HideRetailPanelOnZeroItemsEventArgs>(retailCost1_HideRetailPanelOnZeroItems);
            retailCost1.EnableDisableSaleAndLayaway += new System.EventHandler<RetailCost.EnableDisableSaleAndLayawayArgs>(retailCost1_EnableDisableSaleAndLayaway);
            retailCost1.TotalsChanged += new EventHandler(retailCost1_TotalsChanged);
        }

        void retailCost1_TotalsChanged(object sender, EventArgs e)
        {
            EnableDisableSaleAndLayaway();
            customButtonTaxExempt.Enabled = retailCost1.SubTotal > 0;
            customButtonSale.Enabled = retailCost1.TotalRetailCost > 0;
            customButtonLayaway.Enabled = retailCost1.TotalRetailCost > 0;
        }

        void retailCost1_ItemDetails(string icn)
        {
            GlobalDataAccessor.Instance.DesktopSession.LayawayPaymentCalc = retailCost1.LayawayPaymentCalc;
            int iDx = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.FindIndex(
                    p => p.Icn == icn);
            if (iDx >= 0)
                GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = iDx;
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "ViewItemDetails";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        # endregion

        # region Event Handlers
        private void buttonAddComments_Click(object sender, EventArgs e)
        {

        }

        private void retailCost1_EnableDisableSaleAndLayaway(object sender, RetailCost.EnableDisableSaleAndLayawayArgs e)
        {
            if (!e.EnableDisable)
            {
                customButtonLayaway.Enabled = false;
                customButtonContinue.Enabled = false;
                customButtonSale.Enabled = false;
            }
            else
            {
                EnableDisableSaleAndLayaway();
            }
        }

        private void retailCost1_HideRetailPanelOnZeroItems(object sender, RetailCost.HideRetailPanelOnZeroItemsEventArgs e)
        {
            if (!e.HideRetailPanel)
            {
                labelHeading.ForeColor = Color.Black;
                labelHeading.Text = _ItemSearchHeading;
            }
            else
            {
                labelHeading.ForeColor = Color.White;
                labelHeading.Text = _RetailSaleHeading;
            }
            IsExpandedSearchPanelVisible = e.HideRetailPanel;
            SetWindowSizeAndVisibility();
        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            IsExpandedSearchPanelVisible = !IsExpandedSearchPanelVisible;
            SetWindowSizeAndVisibility();
            if (IsExpandedSearchPanelVisible)
            {
                pnlSearchDetails.SetFocusOnDescription();
            }
        }

        private void customButtonClear_Click(object sender, EventArgs e)
        {
            txtICN.Text = string.Empty;
            this.pnlSearchDetails.Clear();
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            string errorText = null;
            string errorCode = null;
            foreach (RetailItem selectedItem in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems)
            {
                if (selectedItem.mDocType != "7")
                    RetailProcedures.LockItem(CDS, selectedItem.Icn, out errorCode, out errorText, "N");
            }
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void customButtonFind_Click(object sender, EventArgs e)
        {
            string icn = txtICN.Text.Trim();

            List<string> searchFor = new List<string>() { "" };
            List<string> searchValues = new List<string>() { icn };

            if (IsExpandedSearchPanelVisible)
            {
                pnlSearchDetails.BuildParameters(searchFor, searchValues);
                if (searchValues.Count == 0)
                    return;
            }
            else
            {
                if (!Utilities.IsIcnValid(icn))
                    return;
            }
            FindItem(searchFor, searchValues);

        }

        private void customButtonDeleteItem_Click(object sender, EventArgs e)
        {
            retailCost1.DeleteItem();
            if (CDS.ActiveRetail.RetailItems.Count == 0)
            {
                CDS.LayawayMode = false;
                CDS.ActiveRetail.SalesTaxInfo.RevertExclusions();
                SalesTaxInfo = new SalesTaxInfo(CDS.CurrentSiteId.StoreTaxes);
            }
            EnableDisableSaleAndLayaway();
        }

        private void customButtonTaxExempt_Click(object sender, EventArgs e)
        {
            SalesTaxExclusion taxExclusion = new SalesTaxExclusion(SalesTaxInfo);
            taxExclusion.ShowDialog();

            if (CDS.LayawayPaymentCalc != null)
            {
                CDS.LayawayPaymentCalc.CalculateDefaultValues(Math.Round(retailCost1.TotalRetailCost, 2));
            }
            retailCost1.CalculateAllTotals();
        }

        private void customButtonAddComments_Click(object sender, EventArgs e)
        {
            retailCost1.AddComments();
        }

        private void customButtonLayaway_Click(object sender, EventArgs e)
        {
            //Madhu - BZ # 99
            var item = (from itemData in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems
                        where itemData.IsGun
                        select itemData).FirstOrDefault();
            if (item != null)
                MessageBox.Show("This customer is buying firearm(s). Check current address and edit.", "Firearm Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //end

            if (retailCost1.ContainsEbayItem())
            {
                MessageBox.Show("In order to perform a Layaway the Ebay List item must be removed.");
                return;
            }

            //check outTheDoor
            if (retailCost1.OutTheDoorApplied())
            {
                string msg = "The entered Out the Door value will be cleared if Layaway is selected. Would you like to proceed?'";
                DialogResult dgr = MessageBox.Show(msg, "Out the door", MessageBoxButtons.YesNo);
                if (dgr == DialogResult.No)
                    return;
                retailCost1.ClearOutTheDoor();
            }
            ((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveRetail).Items.Clear();
            CDS.LayawayMode = true;
            CDS.LayawayPaymentCalc = new LayawayPaymentCalculator(SalesTaxInfo);
            CDS.LayawayPaymentCalc.CalculateDefaultValues(Math.Round(retailCost1.TotalRetailCost, 2));
            ChangeToLayaway();
        }

        private void customButtonContinue_Click(object sender, EventArgs e)
        {
            if (CDS.LayawayPaymentCalc == null)
                CDS.LayawayPaymentCalc = new LayawayPaymentCalculator(SalesTaxInfo);
            CDS.LayawayPaymentCalc.CalculateDefaultValues(Math.Round(retailCost1.TotalRetailCost, 2));

            LayawayDetails ld = new LayawayDetails();
            ld.MaximumDownPayment = Math.Round(retailCost1.SubTotal, 2);
            if (ld.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            LayawayPaymentCalculator paymentCalculator = CDS.LayawayPaymentCalc;

            var payments = from p in paymentCalculator.Payments
                           where p.DueDate.HasValue
                           select p;

            var nextPayment = payments.First();

            GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.LoanStatus = ProductStatus.LAY;
            LayawayVO layaway = new LayawayVO(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail);
            layaway.Payments = payments.ToList();
            layaway.DownPayment = paymentCalculator.DownPayment;
            layaway.MonthlyPayment = paymentCalculator.MonthlyPaymentAmount;
            layaway.NumberOfPayments = paymentCalculator.NumberOfPayments;
            layaway.FirstPayment = paymentCalculator.Payments.First().DueDate.Value;
            if (nextPayment.DueDate != null)
            {
                //layaway.FirstPayment = nextPayment.DueDate.Value;//payments.Count() > 1 ? payments.First().DueDate.Value : DateTime.MaxValue;
                layaway.LastPayment = payments.Last().DueDate.Value;
                layaway.NextPayment = nextPayment.DueDate.Value;
            }
            layaway.NextDueAmount = nextPayment.Amount;
            layaway.Amount = Math.Round(retailCost1.SubTotal - layaway.CouponAmount, 2);
            layaway.SalesTaxPercentage = SalesTaxInfo.AdjustedTaxRate;
            layaway.SalesTaxAmount = SalesTaxInfo.CalculateTax(retailCost1.SubTotal - layaway.CouponAmount);
            layaway.ShippingHandlingCharges = Math.Round(retailCost1.ShippingAndHandling, 2);
            //SR 01/18/2012 Added logic to check if the service fee is taxable by checking the value of business rule

            BusinessRulesProcedures bProcedures = new BusinessRulesProcedures(CDS);
            AddLayawayServiceFee(layaway, paymentCalculator.ServiceFeeTotal,
                                 bProcedures.IsServiceFeeTaxable(this.CDS.CurrentSiteId) ? paymentCalculator.ServiceFeeTax : 0);
            //end change
            decimal totalFees = layaway.Fees.Aggregate(0.0m, (current, fee) => current + fee.Value);

            layaway.TotalSaleAmount = layaway.DownPayment + totalFees;
            CashlinxDesktopSession.Instance.Layaways = new List<LayawayVO>();
            CashlinxDesktopSession.Instance.Layaways.Add(layaway);
            CashlinxDesktopSession.Instance.CompleteLayaway = true;
            CashlinxDesktopSession.Instance.TenderTransactionAmount.TotalAmount = layaway.TotalSaleAmount;
            CashlinxDesktopSession.Instance.TenderTransactionAmount.SubTotalAmount = layaway.DownPayment;
            // Warning fix.
            var tta = CashlinxDesktopSession.Instance.TenderTransactionAmount;
            tta.SalesTaxPercentage = layaway.SalesTaxPercentage;
            CashlinxDesktopSession.Instance.TenderTransactionAmount = tta;

            bool checkValid = CheckOverrides(true);
            if (checkValid)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer == null ||
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber == string.Empty)
                {
                    NavControlBox.IsCustom = true;
                    NavControlBox.CustomDetail = "LookupCustomer";
                    NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }
                else
                {
                    CompleteLayawayProcess();
                }
            }
            else
            {
                MessageBox.Show("Manager Override did not happen. Not proceeding with sale");
                GlobalDataAccessor.Instance.DesktopSession.CompleteLayaway = false;
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "Reload";
                NavControlBox.Action = NavBox.NavAction.SUBMIT;
            }
        }

        private static void AddLayawayServiceFee(LayawayVO layaway, decimal serviceFee, decimal serviceFeeTax)
        {

            //Add the service fee
            if (layaway.Fees != null)
            {
                int idx = layaway.Fees.FindIndex(feeData => feeData.FeeType == FeeTypes.SERVICE);
                if (idx >= 0)
                    layaway.Fees.RemoveAt(idx);
            }
            else
                layaway.Fees = new List<Fee>();
            Fee fee = new Fee()
            {
                FeeType = FeeTypes.SERVICE,
                Value = serviceFee - serviceFeeTax,
                OriginalAmount = serviceFee - serviceFeeTax,
                FeeState = FeeStates.ASSESSED,
                FeeDate = Utilities.GetDateTimeValue(ShopDateTime.Instance.ShopTransactionTime, DateTime.Now)
            };
            layaway.Fees.Add(fee);
            //Add the service fee tax
            int index = layaway.Fees.FindIndex(feeData => feeData.FeeType == FeeTypes.INTEREST);
            if (index >= 0)
                layaway.Fees.RemoveAt(index);
            Fee taxFee = new Fee()
            {
                FeeType = FeeTypes.INTEREST,
                Value = serviceFeeTax,
                OriginalAmount = serviceFeeTax,
                FeeState = FeeStates.ASSESSED,
                FeeDate = Utilities.GetDateTimeValue(ShopDateTime.Instance.ShopTransactionTime, DateTime.Now)
            };
            layaway.Fees.Add(taxFee);

        }

        private void CompleteLayawayProcess()
        {
            if (!LayawayProcedures.CustomerPassesFirearmAgeCheckForItems(CDS.ActiveLayaway, CDS.ActiveCustomer))
            {
                MessageBox.Show("Customer does not meet age criteria for the sale of guns");
                CDS.CompleteLayaway = false;
                CDS.ActiveLookupCriteria = null;
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "Reload";
                NavControlBox.Action = NavBox.NavAction.SUBMIT;
                return;
            }

            if (!string.IsNullOrEmpty(CDS.ActiveCustomer.CustomerNumber) && !CDS.CompleteLayaway)
            {
                return;
            }

            CDS.DisableCoupon = true;
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "ProcessTender";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void customButtonSale_Click(object sender, EventArgs e)
        {
            if (CDS.ActiveRetail.RetailItems.Count > 1 && retailCost1.ContainsEbayItem() && retailCost1.ContainsNonEbayItem())
            {
                MessageBox.Show("Combination of Sales Type = EBay and Sales Type = In Store or Craig's List is not permitted. Please remove appropriate item.");
                return;
            }

            if (retailCost1.ContainsEbayItem())
            {
                InternetSaleInformation internetSaleInformation = new InternetSaleInformation();
                if (internetSaleInformation.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            string shippingOrBackgroundCheck = string.Empty;
            if (!retailCost1.ValidateShippingAndBackgroundFields(ref shippingOrBackgroundCheck))
            {
                string msg = shippingOrBackgroundCheck + " amount is less or equal to zero. Is that okay?";
                DialogResult dgr = MessageBox.Show(msg, shippingOrBackgroundCheck + " equals zero", MessageBoxButtons.YesNo);
                if (dgr == DialogResult.No)
                    return;
            }
            ((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveRetail).Items.Clear();
            retailCost1.CalculateAllTotals();

            CDS.ActiveRetail.Amount = Math.Round(retailCost1.SubTotal - CDS.ActiveRetail.CouponAmount, 2);
            if (this.SalesTaxInfo != null)
            {
                this.CDS.ActiveRetail.SalesTaxPercentage = this.SalesTaxInfo.AdjustedTaxRate;
            }
            else
            {
                this.SalesTaxInfo = retailCost1.SalesTaxInfo;
            }
            CDS.ActiveRetail.SalesTaxAmount = Math.Round(retailCost1.SalesTaxInfo.CalculateTax(retailCost1.SubTotal - CDS.ActiveRetail.CouponAmount), 2);
            CDS.ActiveRetail.ShippingHandlingCharges = Math.Round(retailCost1.ShippingAndHandling, 2);
            decimal totalFees = CDS.ActiveRetail.Fees.Aggregate(0.0m, (current, fee) => current + fee.Value);
            CDS.ActiveRetail.TotalSaleAmount = CDS.ActiveRetail.Amount + Math.Round(CDS.ActiveRetail.SalesTaxAmount, 2) + CDS.ActiveRetail.ShippingHandlingCharges + totalFees;
            GlobalDataAccessor.Instance.DesktopSession.CompleteSale = true;
            var item = (from itemData in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems
                        where itemData.IsGun
                        select itemData).FirstOrDefault();
            if (item != null)
            {
                //Madhu - BZ # 99
                MessageBox.Show("This customer is buying firearm(s). Check current address and edit.", "Firearm Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //end BZ

                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "LookupCustomer";
                NavControlBox.Action = NavBox.NavAction.SUBMIT;

            }
            else
            {
                CompleteSaleProcess();
            }

        }

        private void CompleteSaleProcess()
        {
            bool proceed = true;
            var item = (from itemData in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems
                        where itemData.IsGun
                        select itemData).FirstOrDefault();
            if (item != null)
            {
                gunItemSale = true;
                proceed = false;
            }
            var handGunItem = (from itemData in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems
                               where itemData.IsHandGun()
                               select itemData).FirstOrDefault();

            var lGunItem = (from itemData in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems
                            where itemData.IsLongGun()
                            select itemData).FirstOrDefault();
            bool firearmAgeCheckFailed = false;

            CustomerVO currentCustomer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            if (currentCustomer.CustomerNumber != string.Empty)
            {
                if (gunItemSale)
                {
                    if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null &&
                        !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName))
                    {
                        if (lGunItem != null)
                        {
                            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.Age < Convert.ToInt16(CustomerProcedures.getLongGunValidAge(GlobalDataAccessor.Instance.DesktopSession)))
                            {
                                firearmAgeCheckFailed = true;

                            }
                        }
                        if (!firearmAgeCheckFailed && handGunItem != null)
                        {
                            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.Age < Convert.ToInt16(CustomerProcedures.getHandGunValidAge(GlobalDataAccessor.Instance.DesktopSession)))
                            {
                                firearmAgeCheckFailed = true;

                            }
                        }
                    }
                    if (firearmAgeCheckFailed)
                    {
                        MessageBox.Show("Customer does not meet age criteria for the sale of guns");
                        GlobalDataAccessor.Instance.DesktopSession.CompleteSale = false;
                        GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = null;
                        NavControlBox.IsCustom = true;
                        NavControlBox.CustomDetail = "Reload";
                        NavControlBox.Action = NavBox.NavAction.SUBMIT;
                    }

                }
                if (!GlobalDataAccessor.Instance.DesktopSession.CompleteSale)
                    return;
                if (!GlobalDataAccessor.Instance.DesktopSession.BackgroundCheckCompleted && gunItemSale)
                {
                    /*DateTime currentDate = ShopDateTime.Instance.ShopDate;
                    string strStoreState = GlobalDataAccessor.Instance.CurrentSiteId.State;
                    if (currentCustomer.HasValidConcealedWeaponsPermitInState(strStoreState, currentDate))
                    {
                        if (CustomerProcedures.IsBackgroundCheckRequired())
                        {
                            proceed = false;
                            FirearmsBackgroundCheck backgroundcheckFrm = new FirearmsBackgroundCheck();
                            backgroundcheckFrm.ShowDialog(this);

                        }
                        else //If the background check is not needed
                        {
                            CashlinxDesktopSession.Instance.BackgroundCheckCompleted = true;
                            proceed = true;
                        }
                    }
                    //else if they do not have CWP or not a CWP in the store state or expired 
                    //then show the background check form
                    else
                    {*/
                        proceed = false;
                        FirearmsBackgroundCheck backgroundcheckFrm = new FirearmsBackgroundCheck();
                        backgroundcheckFrm.ShowDialog(this);
                    //}
                }
            }
            else
            {
                //This is a vendor sale so no background check form needed
                proceed = true;
            }

            if (GlobalDataAccessor.Instance.DesktopSession.BackgroundCheckCompleted || proceed)
            {
                bool checkValid = CheckOverrides(false);
                if (checkValid)
                {
                    GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.TotalAmount = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TotalSaleAmount;
                    GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.SubTotalAmount = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.Amount;
                    GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.SalesTaxPercentage = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.SalesTaxPercentage;
                    NavControlBox.IsCustom = true;
                    NavControlBox.CustomDetail = "ProcessTender";
                    NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }
                else
                {
                    MessageBox.Show("Manager Override did not happen. Not proceeding with sale");
                    GlobalDataAccessor.Instance.DesktopSession.CompleteSale = false;
                    NavControlBox.IsCustom = true;
                    NavControlBox.CustomDetail = "Reload";
                    NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }
            }
            else
            {
                MessageBox.Show("Background check did not happen. Not proceeding with sale");
                GlobalDataAccessor.Instance.DesktopSession.CompleteSale = false;
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "Reload";
                NavControlBox.Action = NavBox.NavAction.SUBMIT;

            }
        }


        public bool CheckOverrides(bool layawayMode)
        {
            //get ActiveSession that checks user's discount percent
            //throw up message, "Retail amount has decreased by more than 10%. A manager's authorization is required."
            //pull up the manager override dialog when user clicks ok
            UserVO loggedInUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;
            decimal userRetailDiscountLimit = SecurityProfileProcedures.GetRetailDiscountPercentLimit(loggedInUser, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
            bool discountPercentGreater = CDS.ActiveRetail.RetailItems.Any(retailItem => retailItem.DiscountPercent > userRetailDiscountLimit);
            //Check if there are any nxt Items in the sale list
            string nxtDocType = ((int)DocumentType.NxtAndStandardDescriptor).ToString();
            bool nxtItems = CDS.ActiveRetail.RetailItems.Any(retailItem => retailItem.mDocType == nxtDocType);
            int managerApprovalAmt = 0;
            if (nxtItems)
            {
                managerApprovalAmt = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetNXTItemApprovalAmount(CDS.CurrentSiteId);
                //Check if there are any nxt items whose price is over the manager approval amount
                nxtItemOverride = CDS.ActiveRetail.RetailItems.Any(retailItem => retailItem.mDocType == nxtDocType && retailItem.NegotiatedPrice > managerApprovalAmt);

            }
            else
                nxtItemOverride = false;

            bool downPaymentLessThanDefault = false;
            bool numberOfPaymentsDifferenceGreater = false;
            if (layawayMode)
            {
                LayawayPaymentCalculator paymentCalculator = retailCost1.LayawayPaymentCalc;

                downPaymentLessThanDefault = paymentCalculator.DefaultDownPayment > paymentCalculator.DownPayment;

                int userMaxNumberOfPaymentsChangeLimit = SecurityProfileProcedures.GetLayawayNumberOfPaymentsLimit(loggedInUser, CDS.CurrentSiteId.StoreNumber);
                numberOfPaymentsDifferenceGreater = paymentCalculator.NumberOfPayments - paymentCalculator.DefaultNumberOfPayments > userMaxNumberOfPaymentsChangeLimit;
            }

            if (discountPercentGreater || SalesTaxInfo.HasExclusions() || nxtItemOverride || numberOfPaymentsDifferenceGreater || downPaymentLessThanDefault)
            {
                StringBuilder messageToShow = new StringBuilder();
                messageToShow.Append("A manager's authorization is required." + System.Environment.NewLine);
                if (discountPercentGreater)
                    messageToShow.Append("Retail amount has decreased by more than " + userRetailDiscountLimit.ToString() + "%." + System.Environment.NewLine);
                if (SalesTaxInfo.HasExclusions())
                    messageToShow.Append("Tax exemption has been given" + System.Environment.NewLine);
                if (nxtItemOverride)
                    messageToShow.Append("One or more of NXT items have a selling price more than " + managerApprovalAmt + System.Environment.NewLine);
                if (downPaymentLessThanDefault)
                    messageToShow.AppendLine("To reduce the default down payment a manager's authorization is required");
                if (numberOfPaymentsDifferenceGreater)
                    messageToShow.AppendLine("To increase the number of payments a manager's authorization is required");
                //bring up dialog
                List<ManagerOverrideType> overrideTypes = new List<ManagerOverrideType>();
                List<ManagerOverrideTransactionType> transactionTypes = new List<ManagerOverrideTransactionType>();

                if (layawayMode)
                {
                    transactionTypes.Add(ManagerOverrideTransactionType.LAY);
                }
                else
                {
                    transactionTypes.Add(ManagerOverrideTransactionType.SALE);
                }
                if (discountPercentGreater)
                    overrideTypes.Add(ManagerOverrideType.DSCPCT);
                if (SalesTaxInfo.HasExclusions())
                    overrideTypes.Add(ManagerOverrideType.TXEXMP);
                if (nxtItemOverride)
                    overrideTypes.Add(ManagerOverrideType.NXT);
                if (downPaymentLessThanDefault)
                    overrideTypes.Add(ManagerOverrideType.DWNPMT);
                if (numberOfPaymentsDifferenceGreater)
                    overrideTypes.Add(ManagerOverrideType.NOFPMT);
                ManageOverrides overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER)
                {
                    MessageToShow = messageToShow.ToString(),
                    ManagerOverrideTypes = overrideTypes,
                    ManagerOverrideTransactionTypes = transactionTypes
                };
                overrideFrm.ShowDialog();
                if (!overrideFrm.OverrideAllowed)
                {
                    SalesTaxInfo = new SalesTaxInfo(CDS.CurrentSiteId.StoreTaxes);
                    if (CDS.LayawayMode)
                    {
                        CDS.LayawayPaymentCalc.ReCalculate();
                    }
                    return false;
                }
            }

            //Madhu - fix for BZ # 87 - to display customer survey card to the user.
            //CustomerSurveyCardComments customerSurvey = new CustomerSurveyCardComments();
            IssueCustomerCommentsCard customerSurvey = new IssueCustomerCommentsCard();
            customerSurvey.ShowDialog();

            return true;
        }

        private void ItemSearch_Load(object sender, EventArgs e)
        {
            SetWindowSizeAndVisibility();
        }

        private void retailCost1_EditableFieldsChanged(object sender, EventArgs e)
        {
            customButtonLayaway.Enabled = false;
            customButtonContinue.Enabled = false;
            customButtonSale.Enabled = false;
        }

        private void txtICN_TextChanged(object sender, EventArgs e)
        {
            EnableDisableFind();
        }

        private void ItemSearch_Shown(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.CompleteSale)
                CompleteSaleProcess();
            if (GlobalDataAccessor.Instance.DesktopSession.CompleteLayaway)
            {
                CompleteLayawayProcess();
            }
  
                if (!GlobalDataAccessor.Instance.DesktopSession.CompleteLayaway && !GlobalDataAccessor.Instance.DesktopSession.CompleteSale)
                {
                    if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems != null &&
                        GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Count > 0)
                    {
                        labelHeading.Text = _RetailSaleHeading;
                        labelHeading.ForeColor = Color.White;
                        if (CDS.ActiveRetail.ShippingHandlingCharges > 0.0m)
                            retailCost1.ShippingAndHandling = CDS.ActiveRetail.ShippingHandlingCharges;
                        if (CDS.LayawayMode && GlobalDataAccessor.Instance.DesktopSession.LayawayPaymentCalc == null)
                        {
                            CDS.LayawayPaymentCalc = new LayawayPaymentCalculator(SalesTaxInfo);
                            CDS.LayawayPaymentCalc.CalculateDefaultValues(Math.Round(retailCost1.TotalRetailCost, 2));
                        }

                        retailCost1.ReloadData();
                        EnableDisableSaleAndLayaway();
                    }
                    if (CDS.LayawayMode)
                    {
                        ChangeToLayaway();
                    }
                    if (IsExpandedSearchPanelVisible)
                    {
                        if (CDS.ActiveItemSearchData.CategoryID != 0 ||
                            CDS.ActiveItemSearchData.Manufacturer != string.Empty ||
                            CDS.ActiveItemSearchData.Model != string.Empty)
                            pnlSearchDetails.Refresh();
                    }

                    else
                    {
                        if (((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveRetail).Items != null &&
                            ((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveRetail).Items.Count > 0)
                        {
                            RetailItem rItem = new RetailItem(((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveRetail).Items[0]);
                            rItem.Icn = RetailProcedures.GenerateTempICN(GlobalDataAccessor.Instance.DesktopSession, rItem.mStore, rItem.mYear);
                            rItem.mDocNumber = Utilities.GetIntegerValue(rItem.Icn.Substring(6, 6), 0);
                            rItem.mDocType = RetailProcedures.DOC_TYPE_FOR_TEMPICN;
                            rItem.RetailPrice = rItem.ItemAmount;
                            AddItem(rItem);
                            SetWindowSizeAndVisibility();
                            ((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveRetail).Items.Clear();
                        }
                    }
                }
            


        }

        private void searchResults_ShowDescMerchandise()
        {
            GlobalDataAccessor.Instance.DesktopSession.GenerateTemporaryICN = true;
            ((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveRetail).Items.Clear();
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "TemporaryICN";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void ItemSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Lock should be released by whichever process is using the item
            //INTG100016137
            //retailCost1.ReleaseLocks();
        }
        # endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.customButtonCancel_Click(null, new EventArgs());
                return true;
            }

            if (keyData == Keys.Enter)
            {
                if (this.ActiveControl == this.txtICN || this.ActiveControl == this.pnlSearchDetails)
                {
                    this.customButtonFind_Click(null, new EventArgs());
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        # region Helper Methods

        private void ChangeToLayaway()
        {
            customButtonContinue.Visible = true;
            customButtonContinue.Location = new Point(676, 0);
            customButtonSale.Visible = false;
            customButtonLayaway.Visible = false;
            labelHeading.ForeColor = Color.White;
            labelHeading.Text = _RetailLayawayHeading;
            retailCost1.ChangeToLayawayMode();
        }

        private void AddItem(RetailItem item)
        {
            Icn icn = new Icn(item.Icn);

            if (item.IsJewelry && icn.SubItemNumber > 0)
            {
                MessageBox.Show("Sub items cannot be sold");
                return;
            }
            if (item.IsJewelry && icn.DocumentType != DocumentType.NxtAndStandardDescriptor)
            {
                JewelryCase jewelryCase = new JewelryCase(item);
                if (jewelryCase.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
 

            if (retailCost1.ItemIsDuplicate(item))
            {
                MessageBox.Show("Duplicate ICNs are not allowed.");
                return;
            }

            if (icn.DocumentType == DocumentType.NxtAndStandardDescriptor)
            {
                NxtComments nxtComments = new NxtComments(item);
                if (nxtComments.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                item.UserItemComments = item.NxtComments;
            }

            if (!retailCost1.AddItem(item))
            {
                return;
            }
        }

        private Size CalculateWindowSize()
        {
            Size s = new Size(this.MaximumSize.Width, this.MaximumSize.Height);

            if (!IsExpandedSearchPanelVisible)
            {
                s.Height -= this.pnlSearchDetails.Height;
            }

            if (!IsItemSelected)
            {
                s.Height -= this.pnlItemCost.Height;
                s.Height -= this.pnlItemCostButtons.Height;
            }

            return s;
        }

        private void EnableDisableFind()
        {
            if (IsExpandedSearchPanelVisible)
            {
                customButtonFind.Enabled = pnlSearchDetails.HasValues();
            }
            else
            {
                string icn = this.txtICN.Text.Trim();

                if (Utilities.IsIcnValid(icn))
                {
                    customButtonFind.Enabled = true;

                    if (icn.Length == Icn.ICN_LENGTH)
                    {
                        List<string> searchFor = new List<string>() { "" };
                        List<string> searchValues = new List<string>() { icn };
                        FindItem(searchFor, searchValues);
                    }
                }
                else
                {
                    customButtonFind.Enabled = false;
                }
            }
        }

        private void EnableDisableSaleAndLayaway()
        {
            bool hasEbayItem = retailCost1.ContainsEbayItem();

            customButtonLayaway.Enabled = !hasEbayItem;
            customButtonContinue.Enabled = !hasEbayItem;
            if (CDS.ActiveRetail.RetailItems.Count > 1)
            {
                customButtonSale.Enabled = !hasEbayItem && retailCost1.SubTotal > 0;
            }

            if (!hasEbayItem)
            {
                customButtonSale.Enabled = retailCost1.SubTotal > 0;
                customButtonLayaway.Enabled = retailCost1.SubTotal > 0;
                customButtonContinue.Enabled = retailCost1.SubTotal > 0;
            }
        }

        private void FindItem(List<string> searchFor, List<string> searchValues)
        {
            string errorText = null;
            string errorCode = null;
            List<RetailItem> searchItems;
            string searchFlag;
            if (!IsExpandedSearchPanelVisible)
                searchFlag = "NORMAL";
            else
                searchFlag = "EXPAND";
            RetailProcedures.SearchForItem(searchFor, searchValues, CDS, searchFlag, false, out searchItems, out errorCode, out errorText);

            RetailItem item = null;
            ItemSearchResults searchResults = new ItemSearchResults(GlobalDataAccessor.Instance.DesktopSession, ItemSearchResultsMode.RETAIL_SALE);
            searchResults.ShowDescMerchandise += searchResults_ShowDescMerchandise;
            if (searchItems.Count == 0)
            {
                searchResults.ShowDialog();
                return;
            }


            if (searchItems.Count == 1 && searchItems[0].ItemStatus != ProductStatus.PFI)
            {
                searchResults.ShowDialog();
                return;
            }

            if (searchItems.Count == 1)
            {
                item = searchItems[0];
                if (Item.ItemLocked(item))
                {
                    searchResults.Message = Item.ItemLockedMessage;
                    searchResults.ShowDialog();
                    return;
                }
            }
            else if (searchItems.Count > 1)
            {
                var distinctItems = from sItem in searchItems
                                    where ((sItem.IsJewelry && sItem.Jewelry.Any(j => j.SubItemNumber == 0)) ||
                                    !sItem.IsJewelry)
                                    select sItem;
                if (distinctItems.Count() > 0)
                {
                    SelectItems selectItems = new SelectItems(searchItems, ItemSearchResultsMode.RETAIL_SALE);
                    selectItems.ShowDescMerchandise += searchResults_ShowDescMerchandise;
                    if (!IsExpandedSearchPanelVisible)
                    {
                        selectItems.ErrorMessage = "The Short code entered is a duplicate. Please make your selection from the list";
                    }
                    if (selectItems.ShowDialog() == DialogResult.OK)
                    {
                        item = selectItems.SelectedItem;
                        if (Item.ItemLocked(item))
                        {
                            searchResults.Message = Item.ItemLockedMessage;
                            searchResults.ShowDialog();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    item = searchItems[0];
                    if (Item.ItemLocked(item))
                    {
                        searchResults.Message = Item.ItemLockedMessage;
                        searchResults.ShowDialog();
                        return;
                    }
                }
            }

            if (item != null && item.HoldType == HoldTypes.POLICEHOLD.ToString())
            {
                SelectItems selectItems = new SelectItems(new List<RetailItem> { item }, true, ItemSearchResultsMode.RETAIL_SALE);
                selectItems.ShowDescMerchandise += searchResults_ShowDescMerchandise;
                selectItems.ErrorMessage = "The item number entered is not eligible for retail sale or layaway.";
                selectItems.ShowDialog();
                return;
            }

            if (CDS.LayawayMode)
                labelHeading.Text = _RetailLayawayHeading;
            else
                labelHeading.Text = _RetailSaleHeading;
            labelHeading.ForeColor = Color.White;
            AddItem(item);
            SetWindowSizeAndVisibility();
            txtICN.Text = "";
            this.pnlSearchDetails.Clear();
        }

        private void SetWindowSizeAndVisibility()
        {
            Size newSize = CalculateWindowSize();
            this.Height = newSize.Height;

            Point searchButtonsLocation = (Point)this.pnlSearchButtons.Tag;
            Point itemCostLocation = (Point)this.pnlItemCost.Tag;
            Point itemCostButtonsLocation = (Point)this.pnlItemCostButtons.Tag;

            if (IsExpandedSearchPanelVisible)
            {
                cmdCollapse.Text = _MINUS;
            }
            else
            {
                cmdCollapse.Text = _PLUS;
                searchButtonsLocation.Y -= this.pnlSearchDetails.Height;
                itemCostLocation.Y -= this.pnlSearchDetails.Height;
                itemCostButtonsLocation.Y -= this.pnlSearchDetails.Height;
            }

            this.txtICN.Enabled = !IsExpandedSearchPanelVisible;

            this.pnlSearchButtons.Location = searchButtonsLocation;
            this.pnlItemCost.Location = itemCostLocation;
            this.pnlItemCostButtons.Location = itemCostButtonsLocation;

            this.pnlSearchDetails.Visible = IsExpandedSearchPanelVisible;
            this.pnlItemCost.Visible = this.pnlItemCostButtons.Visible = IsItemSelected;
        }

        private void pnlSearchDetails_ShowCategory()
        {
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "GetCategory";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void pnlSearchDetails_ValueChanged(object sender, EventArgs e)
        {
            EnableDisableFind();
        }

        private void pnlSearchDetails_Search(object sender, EventArgs e)
        {
            customButtonFind_Click(sender, e);
        }

        #endregion




    }
}
