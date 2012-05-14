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
using Common.Controllers.Rules.Interface;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Pawn.Forms.Pawn.Loan;
using Pawn.Forms.Pawn.Loan.ProcessTender;
using Pawn.Forms.Retail;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Products.ManageMultiplePawnItems
{
    public partial class ManageMultiplePawnItems : Form
    {
        private const string CURRENCY_FORMAT = "{0:f}";
        private const string INITIAL_AMOUNT = "0.00";
        private const string PERITEM = "PER ITEM";
        private const string PERLOAN = "PER LOAN";

        private LoanComponent feeComponent;
        private LoanComponent serviceChargeComponent;
        private List<string> ruleComponents;
        private Dictionary<string, BusinessRuleVO> businessRules;
        private Dictionary<string, decimal> feeComponentList;
        private Form _OwnerForm;
        private bool managerOverrideForLoanLimit;
        private bool managerOverrideSkipped;
        private bool purchaseFlow;
        private bool vendorPurchaseFlow;
        private bool firaremAgeCheckFailed;
        
        public NavBox NavControlBox;

        /// <summary>
        /// 
        /// </summary>
        public ManageMultiplePawnItems()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();

            this.feeComponent = new LoanComponent();
            this.feeComponent.Location = this.loanInformation.Location;
            this.feeComponent.Text = "Fee Details";

            this.serviceChargeComponent = new LoanComponent();
            this.serviceChargeComponent.Location = this.loanInformation.Location;
            this.serviceChargeComponent.Text = "Service Charge Details";

            this.ruleComponents = new List<string>();

            // make call to rules engine
            // Retrieve parameters for the following business rules (get List):
            // PWN_BR_027
            // PWN_BR_010
            // PWN_BR_008
            // PWN_BR_019
            // PWN_BR_022
            this.ruleComponents.Add("PWN_BR-008");
            this.ruleComponents.Add("PWN_BR-010");
            this.ruleComponents.Add("PWN_BR-019");
            this.ruleComponents.Add("PWN_BR-022");
            this.ruleComponents.Add("PWN_BR-027");

            this.aprComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageMultiplePawnItems_Load(object sender, EventArgs e)
        {
            _OwnerForm = this.Owner;
            this.NavControlBox.Owner = this;

            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.CUSTOMERPURCHASE.ToString() ||
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.DESCRIBEITEMCUSTOMERPURCHASE ||
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.VENDORPURCHASE)
            {
                purchaseFlow = true;
            }
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(
                Commons.TriggerTypes.VENDORPURCHASE))
            {
                vendorPurchaseFlow = true;
            }
            if (purchaseFlow)
            {
                loanInformation.Visible = false;
                panelPurchase.Visible = true;
                panelPurchase.Location = new Point(12, 251);
                if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase != null &&
                    GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.ProductDataComplete)
                {
                    var error = ValidatePawnLoan();
                    if (string.IsNullOrEmpty(error))
                    {
                        //invoke process tender
                        showProcessTender();
                    }
                    else
                    {
                        MessageBox.Show(error, "Manage Item Error", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (vendorPurchaseFlow)
                    {
                        labelVendorName.Visible = true;
                        customLabelPONumber.Visible = true;
                        customTextBoxPONumber.Visible = true;
                        labelVendorName.Text = CashlinxDesktopSession.Instance.ActiveVendor.Name;
                        gvPawnItems.Columns[1].Visible = true;
                        gvPawnItems.Columns[3].Visible = true;
                        gvPawnItems.Columns[5].Visible = true;
                        gvPawnItems.Columns[4].HeaderText = "Total Amount";
                    }
                }
 
            }
            else
            {
                if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan != null &&
                    GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.ProductDataComplete)
                {
                    string error = ValidatePawnLoan();
                    if (string.IsNullOrEmpty(error))
                    {
                        //invoke process tender
                        showProcessTender();
                    }
                    else
                    {
                        MessageBox.Show(error, "Manage Item Error", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    loanInformation.Visible = true;
                    panelPurchase.Visible = false;
                    loanInformation.Location = new Point(539, 251);
                }
            }
                
        }

        private void Setup()
        {
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer == null)
            {
                this.DialogResult = DialogResult.Abort;
                System.Windows.Forms.MessageBox.Show("No customer selected.  Please enter customer data.");
            }
            if (purchaseFlow)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase == null)
                {
                    this.DialogResult = DialogResult.Abort;
                    MessageBox.Show("There is no active purchase");
                    this.closeOutToDesktop();
                }
            }
            else
            {
                if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan == null)
                {
                    this.DialogResult = DialogResult.Abort;
                    System.Windows.Forms.MessageBox.Show("There is no active pawn loan.");
                    this.closeOutToDesktop();
                }
            }

            this.LoadAllData();
            ShowAllFees(false);
        }

        private void ShowAllFees(bool bShowAll)
        {
            financeChargesLabel.Visible = bShowAll;
            interestTextBox.Visible = bShowAll;
            serviceChargesLabel.Visible = bShowAll;
            serviceTextBox.Visible = bShowAll;

            feesTextBox.Visible = bShowAll;
            totalLabel.Visible = bShowAll;
            totalTextBox.Visible = bShowAll;
            aprLabel.Visible = bShowAll;
            aprComboBox.Visible = bShowAll;

            Size size = loanInformation.Size;
            size.Height = bShowAll ? 153 : 51;
            loanInformation.Size = size;

            continueButton.Text = bShowAll ? "Submit" : "Continue";
        }

        /// <summary>
        /// 
        /// </summary>
        public int SelectedPawnItemIndex { get; private set; }

        #region Loan Amount Calculations
        /// <summary>
        /// 
        /// </summary>
        public void LoadAllData()
        {
            gvPawnItems.Rows.Clear();
            if (!purchaseFlow)
                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.UpdateTicketLength();

            this.amountFinancedTextBox.Text = INITIAL_AMOUNT;
            this.interestTextBox.Text = INITIAL_AMOUNT;
            this.serviceTextBox.Text = INITIAL_AMOUNT;
            this.feesTextBox.Text = INITIAL_AMOUNT;

            List<Item> pawnItems;
            if (!purchaseFlow)
                pawnItems = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items;
            else
                pawnItems = GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items;
            int itemCount = pawnItems.Count;
            decimal amountFinanced = 0.0M;

            //Used to track the number of items -- usually just plus 
            //one per row, but if a vendor drops off more than one 
            //item then it will add that. CACC counts as 1 item.
            int numberOfItems = 0;
            if (vendorPurchaseFlow || purchaseFlow)
                gvPawnItems.Columns["colAmount"].HeaderText = "Purchase Amount";
            else
                gvPawnItems.Columns["colAmount"].HeaderText = "Loan Amount";
            for (int i = 0; i < itemCount; i++)
            {
                Item currentItem = pawnItems.ElementAt<Item>(i);

                string indexString = "[" + (i + 1) + "] ";

                int gvIdx = gvPawnItems.Rows.Add();
                var myRow = gvPawnItems.Rows[gvIdx];
                //              myRow.DefaultCellStyle.BackColor = Color.White;

                var myImage = new Bitmap(Properties.Resources.blueglossy_edit2, new Size(45, 45));
                myRow.Cells["colImage"].Value = myImage;
                //              myRow.Cells["colImage"].Style.BackColor = Color.White;
                myRow.Cells["colImage"].ToolTipText = "Click to Edit Item.";
                myRow.Cells["colDescription"].Value = indexString + currentItem.TicketDescription;
                myRow.Cells["colAmount"].Value = String.Format(CURRENCY_FORMAT, currentItem.ItemAmount);
                if (vendorPurchaseFlow)
                {
                    if (currentItem.CaccLevel == 0)
                    {
                        //Perform Cacc related display logic
                        int caccQty = currentItem.QuickInformation.Quantity;
                        //Ensure that we do not divide by zero
                        if (caccQty <= 0)
                        {
                            caccQty = 1;
                        }

                        //Compute the per item amount
                        //decimal perItemAmount = currentItem.ItemAmount / caccQty;
                        myRow.Cells["qty"].Value = currentItem.Quantity;
                        myRow.Cells["itemamount"].Value = currentItem.ItemAmount;
                        //myRow.Cells["itemamount"].Value = String.Format(CURRENCY_FORMAT,
                        //    Math.Round(perItemAmount, 2,
                        //        MidpointRounding.AwayFromZero));
                        myRow.Cells["colAmount"].Value = String.Format(CURRENCY_FORMAT, (caccQty * currentItem.ItemAmount));
                        myRow.Cells["retailamount"].Value = String.Format(CURRENCY_FORMAT, (caccQty * currentItem.RetailPrice));
                        numberOfItems++;
                    }
                    else
                    {
                        myRow.Cells["qty"].Value = currentItem.Quantity;
                        myRow.Cells["itemamount"].Value = String.Format(CURRENCY_FORMAT, currentItem.ItemAmount);
                        myRow.Cells["colAmount"].Value = String.Format(CURRENCY_FORMAT, (currentItem.Quantity * currentItem.ItemAmount));
                        myRow.Cells["retailamount"].Value = String.Format(CURRENCY_FORMAT, (currentItem.Quantity * currentItem.RetailPrice));
                        numberOfItems += currentItem.Quantity;
                    }
                }
                else
                {
                    numberOfItems++;
                }

                //SR 02/07/2011 Commented the code that makes the gun row yellow as per BZ 139
                // Gun Rule Logic
                /*if (currentItem.CategoryCode == 4390 || currentItem.MerchandiseType == "H" || currentItem.MerchandiseType == "L")
                gvPawnItems.Rows[gvIdx].DefaultCellStyle.BackColor = Color.Yellow;*/

                if (vendorPurchaseFlow)
                    if (currentItem.CaccLevel != 0)
                    {
                        amountFinanced += Convert.ToDecimal(currentItem.ItemAmount * currentItem.Quantity);
                    }
                    else
                    {
                        //Perform Cacc related display logic
                        int caccQty = currentItem.QuickInformation.Quantity;
                        //Ensure that we do not divide by zero
                        if (caccQty <= 0)
                        {
                            caccQty = 1;
                        }

                        amountFinanced += Convert.ToDecimal(currentItem.ItemAmount * caccQty);
                    }
                else
                    amountFinanced += Convert.ToDecimal(currentItem.ItemAmount);
            }

            if (itemCount > 0)
            {
                gvPawnItems.Rows[itemCount - 1].Selected = true;
                gvPawnItems.FirstDisplayedScrollingRowIndex = itemCount - 1;
            }

            // format fields
            if (!purchaseFlow)
            {
                this.amountFinancedTextBox.Text = string.Format(CURRENCY_FORMAT, amountFinanced);
                GlobalDataAccessor.Instance.CurrentSiteId.LoanAmount = amountFinanced;
            }
            else
            {
                labelNumOfItemsValue.Text = numberOfItems.ToString();
                labelPurchaseAmtvalue.Text = string.Format(CURRENCY_FORMAT, amountFinanced);
            }
            if (itemCount > 0 && !purchaseFlow)
                this.ProcessCalculations();
            if (!purchaseFlow)
            {
                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Amount = amountFinanced;
                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.InterestRate = Convert.ToDecimal(String.Format("{0:00}", this.aprComboBox.SelectedItem));
                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.InterestAmount = Convert.ToDecimal(String.Format("{0:00}", interestTextBox.Text));
                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.ServiceCharge = Convert.ToDecimal(String.Format("{0:00}", serviceTextBox.Text));
            }
            else
                GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Amount = amountFinanced;

            var resultString = string.Empty;
            if (itemCount > 0)
            {
                resultString = ValidatePawnLoan();
                if (deleteButton.Enabled == false)
                    deleteButton.Enabled = true;

                if (!string.IsNullOrEmpty(resultString))
                {
                    if (managerOverrideSkipped)
                        this.continueButton.Enabled = true;
                    else
                        this.continueButton.Enabled = false;
                    //jk: Bug 314 Added null check and removed message for exceeding limit
                    if (resultString != null && resultString.Contains("Loan Amount exceeds limit"))
                        return;
                    MessageBox.Show(resultString,
                                    !purchaseFlow ? "Pawn Loan Errors" : "Purchase Errors",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase != null)
                        GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.ProductDataComplete = false;
                }
                else
                {
                    if (this.continueButton.Enabled == false)
                    { 
                        this.continueButton.Enabled = true;
                    }
                }
            }
            else
            {
                this.continueButton.Enabled = false;
                this.deleteButton.Enabled = false;
            }
        }

        /*
        private string DescriptionWrap(string sStringToWrap)
        {
        string sWrappedString = "";
        int iDescriptionLineLength = 0;

        foreach (string s in sStringToWrap.Split(';'))
        {
        int iStringLength = s.Length;

        if (iDescriptionLineLength + iStringLength < 50)
        {
        sWrappedString += s + ";";
        iDescriptionLineLength += iStringLength;
        }
        else
        {
        sWrappedString += s + ";" + Environment.NewLine;
        iDescriptionLineLength = 0;
        }
        }

        return sWrappedString;
        }
        * */

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string CalculateLoanAmount()
        {
            decimal loanAmount = 0.0M;
            decimal currentAmount = 0.0M;

            decimal.TryParse(this.amountFinancedTextBox.Text, out currentAmount);
            loanAmount += currentAmount;

            decimal.TryParse(this.interestTextBox.Text, out currentAmount);
            loanAmount += currentAmount;

            decimal.TryParse(this.serviceTextBox.Text, out currentAmount);
            loanAmount += currentAmount;

            decimal.TryParse(this.feesTextBox.Text, out currentAmount);
            loanAmount += currentAmount;

            // set the SiteId's loan amount
            GlobalDataAccessor.Instance.CurrentSiteId.LoanAmount = loanAmount;
            GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Amount = loanAmount;

            return string.Format(CURRENCY_FORMAT, loanAmount);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ProcessCalculations()
        {
            // call UnderWrite Pawn Loan
            var upw = new UnderwritePawnLoanUtility(GlobalDataAccessor.Instance.DesktopSession);
            upw.RunUWP(GlobalDataAccessor.Instance.CurrentSiteId);

            var uwpVO = upw.PawnLoanVO;

            // populate form fields
            this.interestTextBox.Text = string.Format(CURRENCY_FORMAT, uwpVO.totalFinanceCharge);
            this.serviceTextBox.Text = string.Format(CURRENCY_FORMAT, uwpVO.totalServiceCharge);

            decimal totalFees = 0.0M;

            // CL_PWN_0013_MININTAMT
            decimal minIntAmt = 0.0M;
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0013_MININTAMT", out minIntAmt);
            totalFees += minIntAmt;

            // CL_PWN_0018_SETUPFEEAMT
            decimal setupFeeAmt = 0.0M;
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0018_SETUPFEEAMT", out setupFeeAmt);
            totalFees += setupFeeAmt;

            // CL_PWN_0022_CITYFEEAMT
            decimal cityFeeAmt = 0.0M;
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0022_CITYFEEAMT", out cityFeeAmt);
            totalFees += cityFeeAmt;

            // CL_PWN_0026_FIREARMFEEAMT
            decimal firearmFeeAmt = 0.0M;
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0026_FIREARMFEEAMT", out firearmFeeAmt);
            totalFees += firearmFeeAmt;

            // CL_PWN_0040_PFIMAILFEE
            decimal pfiMailFeeAmt = 0.0M;
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0040_PFIMAILFEE", out pfiMailFeeAmt);
            totalFees += pfiMailFeeAmt;

            // CL_PWN_0115_PPCITYFEEAMT
            decimal ppCityFeeAmt = 0.0M;
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0115_PPCITYFEEAMT", out ppCityFeeAmt);
            totalFees += ppCityFeeAmt;

            // CL_PWN_0030_STRGFEE
            decimal strgFeeAmt = 0.0M;
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0030_STRGFEE", out strgFeeAmt);
            

            // CL_PWN_0101_LOANFEEAMT
            //SR 11/20/09 This fee is not part of the list in the business rules
            //to be calculated at the time of new loan
            /*
            decimal loanFeeAmt = 0.0M;
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0101_LOANFEEAMT", out loanFeeAmt);
            totalFees += loanFeeAmt;*/

            // CL_PWN_0103_ORIGINFEEAMT
            //SR 11/20/09 This fee is not part of the list in the business rules
            //to be calculated at the time of new loan
            /*
            decimal originFeeAmt = 0.0M;
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0103_ORIGINFEEAMT", out originFeeAmt);
            totalFees += originFeeAmt;
            */

            // CL_PWN_0104_ADMINFEEAMT
            //SR 11/20/09 This fee is not part of the list in the business rules
            //to be calculated at the time of new loan
            /*
            decimal adminFeeAmt = 0.0M;
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0104_ADMINFEEAMT", out adminFeeAmt);
            totalFees += adminFeeAmt;
            */

            // CL_PWN_0105_INITCHGFEEAMT
            //SR 11/20/09 This fee is not part of the list in the business rules
            //to be calculated at the time of new loan
            /*
            decimal initChgFeeAmt = 0.0M;
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0105_INITCHGFEEAMT", out initChgFeeAmt);
            totalFees += initChgFeeAmt;
            * /

            // CL_PWN_0106_PROCFEEAMT
            //SR 11/20/09 This fee is not part of the list in the business rules
            //to be calculated at the time of new loan
            /*decimal procFeeAmt = 0.0M;
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0106_PROCFEEAMT", out currentValue);
            totalFees += currentValue;*/

            //SR 11/20/09 The following fees are not part of the list in the business rules
            //to be calculated at the time of new loan
            // CL_PWN_0033_MAXSTRGFEE
            /*
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0033_MAXSTRGFEE", out currentValue);
            totalFees += currentValue;

            // CL_PWN_0037_TICKETFEE
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0037_TICKETFEE", out currentValue);
            totalFees += currentValue;

            // CL_PWN_0102_PREPFEEAMT
            uwpVO.feeDictionary.TryGetValue("CL_PWN_0102_PREPFEEAMT", out currentValue);
            totalFees += currentValue;*/

            var _BusinessRule = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO["PWN_BR-054"];
            var sComponentValue = string.Empty;

            bool bFound = _BusinessRule.getComponentValue("CL_PWN_0025_APRCALCRNDFAC", ref sComponentValue);
            decimal dAPR = uwpVO.APR;

            if (sComponentValue.Equals("ROUNDED"))
            {
                bFound = _BusinessRule.getComponentValue("CL_PWN_0021_APRCALCTODEC", ref sComponentValue);
                dAPR = Math.Round(dAPR, Convert.ToInt32(sComponentValue));
            }
            string sAPR=dAPR.ToString();
            if (CashlinxDesktopSession.Instance.CurrentSiteId.Alias==States.Oklahoma ||
                CashlinxDesktopSession.Instance.CurrentSiteId.Alias == States.Ohio)
            {
                sAPR = dAPR.ToString("f2");
            }
                
            this.aprComboBox.Items.Add(sAPR);
            this.aprComboBox.SelectedIndex = this.aprComboBox.Items.Count - 1;

            var activePawnLoan = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan;
            activePawnLoan.ObjectUnderwritePawnLoanVO = uwpVO;

            _BusinessRule = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO["PWN_BR-000"];
            bFound = _BusinessRule.getComponentValue("STORE_FIN_NEGOT", ref sComponentValue);
            if (bFound)
            {
                activePawnLoan.NegotiableFinanceCharge = Convert.ToChar(sComponentValue);
                if (sComponentValue == "Y")
                    this.aprComboBox.Enabled = true;
                else
                    this.aprComboBox.Enabled = false;
            }

            bFound = _BusinessRule.getComponentValue("STORE_SERV_NEGOT", ref sComponentValue);
            if (bFound)
            {
                activePawnLoan.NegotiableServiceCharge = Convert.ToChar(sComponentValue);
            }

            _BusinessRule = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO["PWN_BR-003"];
            bFound = _BusinessRule.getComponentValue("CL_PWN_0037_STRGFEEALLWD", ref sComponentValue);
            if (bFound)
            {
                activePawnLoan.StorageFeeAllowed = sComponentValue == "Y";
            }

            var feedate = string.Format("{0} {1}", ShopDateTime.Instance.ShopDate.ToShortDateString(), ShopDateTime.Instance.ShopTime);

            //Add the interest and service charge as fees in the fee list object of active pawn loan
            Fee intFee = new Fee
            {
                FeeType = FeeTypes.INTEREST,
                OriginalAmount = Math.Round(uwpVO.totalFinanceCharge, 2),
                Value = Math.Round(uwpVO.totalFinanceCharge, 2),
                FeeState = FeeStates.ASSESSED,
                FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
            };
            UpdatePawnLoanFee(ref activePawnLoan, intFee);

            var srvChargFee = new Fee
            {
                FeeType = FeeTypes.SERVICE,
                OriginalAmount = Math.Round(uwpVO.totalServiceCharge, 2),
                Value = Math.Round(uwpVO.totalServiceCharge, 2),
                FeeState = FeeStates.ASSESSED,
                FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
            };
            UpdatePawnLoanFee(ref activePawnLoan, srvChargFee);

            //Add all the other fees to the fee list object of active pawn loan
            if (minIntAmt > 0)
            {
                var minIntFee = new Fee
                {
                    FeeType = FeeTypes.MINIMUM_INTEREST,
                    OriginalAmount = minIntAmt,
                    Value = minIntAmt,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
                };
                UpdatePawnLoanFee(ref activePawnLoan, minIntFee);
            }

            if (setupFeeAmt > 0)
            {
                var setupFee = new Fee
                {
                    FeeType = FeeTypes.SETUP,
                    OriginalAmount = setupFeeAmt,
                    Value = setupFeeAmt,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
                };
                UpdatePawnLoanFee(ref activePawnLoan, setupFee);
            }

            if (cityFeeAmt > 0)
            {
                var cityFee = new Fee
                {
                    FeeType = FeeTypes.CITY,
                    OriginalAmount = cityFeeAmt,
                    Value = cityFeeAmt,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
                };
                UpdatePawnLoanFee(ref activePawnLoan, cityFee);
            }
            if (firearmFeeAmt > 0)
            {
                var firearmFee = new Fee
                {
                    FeeType = FeeTypes.FIREARM,
                    OriginalAmount = firearmFeeAmt,
                    Value = firearmFeeAmt,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
                };
                UpdatePawnLoanFee(ref activePawnLoan, firearmFee);
            }
            if (pfiMailFeeAmt > 0)
            {
                var pfiMailFee = new Fee
                {
                    FeeType = FeeTypes.MAILER_CHARGE,
                    OriginalAmount = pfiMailFeeAmt,
                    Value = pfiMailFeeAmt,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
                };
                UpdatePawnLoanFee(ref activePawnLoan, pfiMailFee);
            }

            if (ppCityFeeAmt > 0)
            {
                var ppCityFee = new Fee
                {
                    FeeType = FeeTypes.PREPAID_CITY,
                    OriginalAmount = ppCityFeeAmt,
                    Value = ppCityFeeAmt,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
                };
                UpdatePawnLoanFee(ref activePawnLoan, ppCityFee);
            }

            //If storage fee is allowed add it to fees list in pawn loan
            if (activePawnLoan.StorageFeeAllowed)
            {
                _BusinessRule = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO["PWN_BR-005"];
                //Get the type of storage fee - if it is by item or by loan
                var strgFeeType = string.Empty;
                bFound = _BusinessRule.getComponentValue("CL_PWN_0092_STRGFEETYPE", ref strgFeeType);
                if (bFound)
                {
                    if (strgFeeAmt > 0)
                    {
                        decimal totalStorageFees = 0.0M;
                        if (strgFeeType == PERITEM && activePawnLoan.Items != null && activePawnLoan.Items.Count > 0)
                        {
                            totalStorageFees = activePawnLoan.Items.Count * strgFeeAmt;
                        }
                        if (strgFeeType == PERLOAN)
                            totalStorageFees = strgFeeAmt;
                        Fee fStorage = new Fee
                        {
                            FeeType = FeeTypes.STORAGE,
                            OriginalAmount = totalStorageFees,
                            Value = totalStorageFees,
                            FeeState = FeeStates.ASSESSED,
                            FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
                        };

                        UpdatePawnLoanFee(ref activePawnLoan, fStorage);
                        totalFees += totalStorageFees;
                    }
                }
            }
            this.feesTextBox.Text = string.Format(CURRENCY_FORMAT, totalFees);
            this.totalTextBox.Text = CalculateLoanAmount();
            this.feeComponentList = uwpVO.feeDictionary;
        }

        private static void UpdatePawnLoanFee(ref PawnLoan pawnLoan, Fee fee)
        {
            if (pawnLoan.Fees == null)
                pawnLoan.Fees = new List<Fee>();
            //To Do: should the fee date be compared?
            int idx = pawnLoan.Fees.FindIndex(f => f.FeeType == fee.FeeType);

            if (idx >= 0)
            {
                pawnLoan.Fees.RemoveAt(idx);
                pawnLoan.Fees.Insert(idx, fee);
            }
            else
            {
                pawnLoan.Fees.Add(fee);
            }
        }

        private void closeOutToDesktop()
        {
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {
                var dR = MessageBox.Show("Do you want to continue processing this customer?", "Describe Item Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dR == DialogResult.No)
                {
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
                }
            }

            GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
            if (!purchaseFlow)
                GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Add(new PawnLoan());
            else
                GlobalDataAccessor.Instance.DesktopSession.Purchases.Add(new PurchaseVO());
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        #endregion

        #region Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.closeOutToDesktop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void continueButton_Click(object sender, EventArgs e)
        {
            if (!string.Equals(continueButton.Text, "Submit", StringComparison.OrdinalIgnoreCase))
            {
                string sError = ValidatePawnLoan();

                if (string.IsNullOrEmpty(sError))
                {
                    //If vendor purchase flow check that the PO number is entered
                    if (vendorPurchaseFlow)
                    {
                        if (!(customTextBoxPONumber.isValid))
                        {
                            MessageBox.Show("Purchase order number cannot be blank");
                            return;
                        }
                    }
                    ShowAllFees(true);
                }
                else
                {
                    MessageBox.Show("Error when validating transaction: " + sError, "Manage Item Error", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                //DesktopForms.ProcessTender processTender = new DesktopForms.ProcessTender();                
                //CashlinxDesktopSession.Instance.HistorySession.AddForm(processTender);
                //processTender.Show(_OwnerForm);
                //Check if a pawn app id exists
                if (!purchaseFlow)
                {
                    //Hide tabs
                    NavControlBox.IsCustom = true;
                    NavControlBox.CustomDetail = "CloseTabs";
                    NavControlBox.Action = NavBox.NavAction.SUBMIT;
                    if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer == null ||
                        String.IsNullOrEmpty(
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber) ||
                        GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Any(i=>i.IsGun))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.ProductDataComplete =
                        true;
                        //Go to lookup customer
                        NavControlBox.IsCustom = true;
                        NavControlBox.CustomDetail = "LookupCustomer";
                        NavControlBox.Action = NavBox.NavAction.SUBMIT;
                    }
                    else
                    {
                        //SR 3/24/2010
                        //Add check for making sure that the user can only do a loan per the allowed limit
                        //The limit that is assigned to him or to his role
                        UserVO loggedInUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;
                        decimal loanLimitAmount = SecurityProfileProcedures.GetPawnLoanLimit(loggedInUser, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, GlobalDataAccessor.Instance.DesktopSession, new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession));
                        if (loanLimitAmount != 0)
                        {
                            //The loan amount is greater than the max allowed limit for the logged in user 
                            //Show manager override
                            if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Amount > loanLimitAmount && !GlobalDataAccessor.Instance.DesktopSession.ManagerOverrideLoanLimit)
                            {
                                var overrideTypes = new List<ManagerOverrideType>();
                                var transactionTypes = new List<ManagerOverrideTransactionType>();
                                var messageToShow = new StringBuilder();
                                messageToShow.Append("Loan Amount exceeded the amount allowed for the user " + System.Environment.NewLine + " Amount allowed is " + String.Format("{0:c}", loanLimitAmount));
                                overrideTypes.Add(ManagerOverrideType.NLO);
                                transactionTypes.Add(ManagerOverrideTransactionType.NL);
                                var overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER, GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Amount)
                                {
                                    MessageToShow = messageToShow.ToString(),
                                    ManagerOverrideTypes = overrideTypes,
                                    ManagerOverrideTransactionTypes = transactionTypes

                                };

                                overrideFrm.ShowDialog();
                                if (!overrideFrm.OverrideAllowed)
                                {
                                    managerOverrideSkipped = true;
                                    MessageBox.Show("Error:  Loan Amount exceeds limit ($" +loanLimitAmount.ToString() + ")");
                                    return;
                                }
                                else
                                   GlobalDataAccessor.Instance.DesktopSession.ManagerOverrideLoanLimit = true;
                            }
                        }
                        if (string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.CurPawnAppId))
                        {
                            //check if customer is identified
                            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
                            {
                                var currentCustomer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                                var strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                                var strClothing = string.Empty;
                                var strNotes = string.Empty;
                                var strIdentTypeCode = string.Empty;
                                var strIdentNumber = string.Empty;
                                var strIdentIssuer = string.Empty;
                                var strIdentExpirydate = string.Empty;
                                var strPawnAppId = string.Empty;
                                var errorCode = string.Empty;
                                var errorMsg = string.Empty;
                                string strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;

                                var firstId = currentCustomer.getFirstIdentity();
                                strIdentTypeCode = firstId.IdType;
                                strIdentNumber = firstId.IdValue;
                                strIdentIssuer = firstId.IdIssuerCode;
                                strIdentExpirydate = firstId.IdExpiryData.FormatDate();
                                //create a pawn application Id for the customer
                                //Generate Loan application and persist the loan application
                                var dgr = DialogResult.Retry;
                                bool retValue;
                                do
                                {
                                    retValue = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).InsertPawnApplication(currentCustomer.CustomerNumber, strStoreNumber, strClothing, strNotes, strIdentTypeCode, strIdentNumber, strIdentIssuer, strIdentExpirydate, strUserId, out strPawnAppId, out errorCode, out errorMsg);
                                    if (!retValue)
                                    {
                                        dgr = MessageBox.Show(Commons.GetMessageString("LoanIdCreationError"), "Error", MessageBoxButtons.RetryCancel);
                                        if (dgr == DialogResult.Cancel)
                                        {
                                            closeOutToDesktop();
                                            break;
                                        }
                                    }
                                    else
                                        break;
                                }
                                while (dgr == DialogResult.Retry);
                                GlobalDataAccessor.Instance.DesktopSession.CurPawnAppId = strPawnAppId;
                            }
                        }
                        //Get business rule value to see if customer should sign back of ticket
                        var bProcedures = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession);
                        var custSignatureReqd=bProcedures.CustomerSignatureOnBackofTicket(GlobalDataAccessor.Instance.CurrentSiteId);

                        if (custSignatureReqd)
                        {
                            //Call Stored procedure to get the number of active loan counts if
                            //customer signature required
                            int loanCount;
                            string errCode;
                            string errMesg;
                            var activeCustomer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;

                            CustomerLoans.getActiveLoanCount(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                ShopDateTime.Instance.ShopDate.ToShortDateString(),
                               activeCustomer.CustomerNumber,
                                out loanCount,
                                out errCode,
                                out errMesg);

                            activeCustomer.ActiveLoanCount = loanCount;
                        }
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        GlobalDataAccessor.Instance.DesktopSession.showProcessTender(ProcessTenderProcedures.ProcessTenderMode.NEWLOAN);
                    }
                }
                else
                {
                    //Add check for making sure that the user can only do a purchase per the allowed limit
                    //The limit that is assigned to him or to his role
                    if (!GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.ProductDataComplete)
                    {
                        var loggedInUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;
                        var purchaseLimitAmount = SecurityProfileProcedures.GetPawnPurchaseLimit(loggedInUser, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
                        if (purchaseLimitAmount != 0)
                        {
                            //The loan amount is greater than the max allowed limit for the logged in user 
                            //Show manager override
                            if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Amount > purchaseLimitAmount && !GlobalDataAccessor.Instance.DesktopSession.ManagerOverrideBuyLimit)
                            {
                                var overrideTypes = new List<ManagerOverrideType>();
                                var transactionTypes = new List<ManagerOverrideTransactionType>();
                                var messageToShow = new StringBuilder();
                                messageToShow.Append("The buy Amount of " + String.Format("{0:c}", GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Amount) + " is over your authorized limit!" + System.Environment.NewLine + "  Manager's Authorization Required. ");
                                overrideTypes.Add(ManagerOverrideType.PURO);
                                transactionTypes.Add(ManagerOverrideTransactionType.PUR);
                                var overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER, GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Amount)
                                {
                                    MessageToShow = messageToShow.ToString(),
                                    ManagerOverrideTypes = overrideTypes,
                                    ManagerOverrideTransactionTypes = transactionTypes

                                };

                                overrideFrm.ShowDialog();
                                if (!overrideFrm.OverrideAllowed)
                                {
                                    managerOverrideSkipped = true;
                                    MessageBox.Show("Error:  Buy Amount exceeds limit ($" + purchaseLimitAmount.ToString() + ")");
                                    return;
                                }
                                GlobalDataAccessor.Instance.DesktopSession.ManagerOverrideBuyLimit = true;
                            }
                        }
                    }

                    if (vendorPurchaseFlow)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.ProductDataComplete = true;
                        //Fill the purchase order number
                        GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.PurchaseOrderNumber = customTextBoxPONumber.Text;
                        //Show disbursement tender type screen
                        GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount = GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Amount;
                        var tenderTypeForm = new DisbursementDetails();
                        tenderTypeForm.ShowDialog();
                        if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.PurchaseTenderType == PurchaseTenderTypes.BILLTOAP.ToString() ||
                            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.PurchaseTenderType == PurchaseTenderTypes.CASHOUT.ToString())
                        //Go to process tender
                            showProcessTender();
                        else
                        {
                            MessageBox.Show("No tender type selected. Cannot proceed");
                            return;
                        }
                    }
                    else
                    {
                        //In purchase flow check if the customer is identified
                        if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer == null ||
                            String.IsNullOrEmpty(
                                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.ProductDataComplete =
                            true;
                            //Go to lookup customer
                            NavControlBox.IsCustom = true;
                            NavControlBox.CustomDetail = "LookupCustomer";
                            NavControlBox.Action = NavBox.NavAction.SUBMIT;
                        }
                        else
                        {
                            //Go to process tender
                            showProcessTender();
                        }
                    }
                }
            }
        }

        private void showProcessTender()
        {
            bool completeProcess = false;
            if (vendorPurchaseFlow)
            {
                string ffl = GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Ffl;

                if ((string.IsNullOrWhiteSpace(ffl) || ffl.Length != 15) && 
                    GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items.Any(i => i.IsGun) && 
                    !GlobalDataAccessor.Instance.DesktopSession.VendorValidated)
                {
                    GlobalDataAccessor.Instance.DesktopSession.VenderFFLRequired = true;
                    NavControlBox.CustomDetail = "ShowVendor";
                    NavControlBox.IsCustom = true;
                    NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                }
                else
                    completeProcess = true;
            }
            else
                completeProcess = true;

            if (completeProcess)
            {
                if (!vendorPurchaseFlow)
                {
                    var customerSurvey = new IssueCustomerCommentsCard();
                    customerSurvey.ShowDialog();
                }

                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                if (purchaseFlow)
                {
                    GlobalDataAccessor.Instance.DesktopSession.showProcessTender(
                        vendorPurchaseFlow
                        ? ProcessTenderProcedures.ProcessTenderMode.
                        VENDORPURCHASE
                        : ProcessTenderProcedures.ProcessTenderMode.
                        PURCHASE);
                }
                else
                    GlobalDataAccessor.Instance.DesktopSession.showProcessTender(
                        ProcessTenderProcedures.ProcessTenderMode.NEWLOAN);
            }


        }

        private void gvPawnItems_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                this.SelectedPawnItemIndex = e.RowIndex;
                GotoDescribeItem();
            }
        }

        private void GotoDescribeItem()
        {
            GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext = CurrentContext.EDIT_MMP;
            GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = SelectedPawnItemIndex;
            if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan != null && 
                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Count > 0)
            {
                GlobalDataAccessor.Instance.DesktopSession.DescribeItemSelectedProKnowMatch = 
                    GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[SelectedPawnItemIndex].SelectedProKnowMatch;
            }
            else
                GlobalDataAccessor.Instance.DesktopSession.DescribeItemSelectedProKnowMatch = 
                    GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items[SelectedPawnItemIndex].SelectedProKnowMatch;
            NavControlBox.CustomDetail = "DescribeItem";
            NavControlBox.IsCustom = true;
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, EventArgs e)
        {
            if (gvPawnItems.SelectedRows.Count >= 0)
            {
                var myRows = gvPawnItems.SelectedRows;
                this.SelectedPawnItemIndex = myRows[0].Index;
                this.DialogResult = DialogResult.Yes;
                GotoDescribeItem();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            BusinessRuleVO _BusinessRule;
            int iCurrentItemCount = gvPawnItems.Rows.Count + 1;
            this.DialogResult = DialogResult.Retry;
            var sComponentValue = string.Empty;
            if (!purchaseFlow)
            {
                _BusinessRule = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO["PWN_BR-008"];
                bool bFound = _BusinessRule.getComponentValue("CL_PWN_004_MAXITEMSPERTKT", ref sComponentValue);

                if (bFound)
                {
                    if (iCurrentItemCount > Convert.ToInt32(sComponentValue))
                    {
                        MessageBox.Show("You cannot exceed " + sComponentValue + " items on a loan.", "Max Loan Items Verification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                _BusinessRule = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO["PWN_BR-117"];
                bool bFound = _BusinessRule.getComponentValue("CL_PWN-210MAXITEMSPURCHASE", ref sComponentValue);

                if (bFound)
                {
                    if (iCurrentItemCount > Convert.ToInt32(sComponentValue))
                    {
                        MessageBox.Show("You cannot exceed the maximum number of Items (" + sComponentValue + ") allowed for a Buy.", "Max Buy Items Verification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "DescribeMerchandise";
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void feesTextBox_Click(object sender, EventArgs e)
        {
            double totalFees = 0.0d;
            double.TryParse(this.feesTextBox.Text, out totalFees);

            this.feeComponent.ClearComponents();

            if (totalFees == 0.00d)
            {
                this.feeComponent.AddComponent("No fees");
            }
            else
            {
                foreach (Fee f in GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Fees)
                {
                    this.feeComponent.AddComponent(f.FeeType.ToString());
                }
            }

            this.feeComponent.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void feesTextBox_KeyPress(object sender, KeyEventArgs e)
        {
            double totalFees = 0.0d;
            double.TryParse(this.feesTextBox.Text, out totalFees);

            this.feeComponent.ClearComponents();

            if (totalFees == 0.00d)
            {
                this.feeComponent.AddComponent("No fees");
            }
            else
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (gvPawnItems.SelectedRows.Count >= 0)
            {
                var myRows = gvPawnItems.SelectedRows;
                int index = myRows[0].Index;
                if (!purchaseFlow)
                    GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.RemoveAt(index);
                else
                    GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items.RemoveAt(index);

                //this.pawnItemsListView.SelectedIndices.Remove(0);
                //this.pawnItemsListView.Clear();
                if (!purchaseFlow)
                {
                    for (var i = 0; i < GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Count; i++)
                    {
                        var sTicket = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[i].TicketDescription;
                        //           sTicket = "[" + (i + 1).ToString() + "] " + sTicket.Substring(sTicket.IndexOf("] ") + 2);
                        GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[i].mItemOrder = i + 1;
                        GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[i].TicketDescription = sTicket;
                    }
                }
                else
                {
                    for (int i = 0; i < CashlinxDesktopSession.Instance.ActivePurchase.Items.Count; i++)
                    {
                        var sTicket = GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items[i].TicketDescription;
                        //           sTicket = "[" + (i + 1).ToString() + "] " + sTicket.Substring(sTicket.IndexOf("] ") + 2);
                        GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items[i].mItemOrder = i + 1;
                        GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items[i].TicketDescription = sTicket;
                    }
                }

                this.LoadAllData();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pawnItemsListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gvPawnItems.SelectedRows.Count >= 0)
            {
                var myRows = gvPawnItems.SelectedRows;
                this.SelectedPawnItemIndex = myRows[0].Index;
                this.DialogResult = DialogResult.Yes;
                MessageBox.Show("Selected " + e.KeyChar.ToString());
            }
        }

        #endregion

        #region Validate Pawn Loan
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string ValidatePawnLoan()
        {
            var sb = new StringBuilder();

            var returnCode = GlobalDataAccessor.Instance.DesktopSession.PawnRulesSys.beginSite(
                this,
                GlobalDataAccessor.Instance.CurrentSiteId,
                this.ruleComponents,
                out this.businessRules);

            if (returnCode.ReturnCode != PawnRulesSystemReturnCode.Code.SUCCESS)
            {
                sb.AppendLine("The pawn rules system has encountered the following error(s): " +
                              returnCode.ReturnDesc);
                return sb.ToString();
            }

            var getParameterReturnCode =
            GlobalDataAccessor.Instance.DesktopSession.PawnRulesSys.getParameters(this, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId, ref this.businessRules);

            if (getParameterReturnCode.ReturnCode != PawnRulesSystemReturnCode.Code.SUCCESS)
            {
                sb.AppendLine("The pawn rules system has encountered the following error(s): " +
                              returnCode.ReturnDesc);
                return sb.ToString();
            }

            var endSiteReturnCode =
            GlobalDataAccessor.Instance.DesktopSession.PawnRulesSys.endSite(this, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);

            if (endSiteReturnCode.ReturnCode != PawnRulesSystemReturnCode.Code.SUCCESS)
            {
                sb.AppendLine("The pawn rules system has encountered the following error(s): " +
                              returnCode.ReturnDesc);
                return sb.ToString();
            }

            int numLines = 0;
            BusinessRuleVO currentBusinessRule;

            if (!purchaseFlow)
            {
                #region PWN_BR-022
                currentBusinessRule = this.businessRules["PWN_BR-022"];

                var componentValue = string.Empty;
                int maxLineLength = 0;
                int maxLinesPerTicket = 0;
                bool addendumAllowed = false;
                bool contDescAllowed = false;

                // CL_PWN_0005_MAXLINELNGTH
                if (currentBusinessRule.getComponentValue("CL_PWN_0005_MAXLINELNGTH", ref componentValue))
                {
                    try
                    {
                        maxLineLength = int.Parse(componentValue);
                    }
                    catch (Exception)
                    {
                        sb.AppendLine("CL_PWN_0005_MAXLINELNGTH not in valid format");
                    }
                }
                else
                {
                    sb.AppendLine("CL_PWN_0005_MAXLINELNGTH Not found");
                }

                // CL_PWN_0006_MAXLINESPTKT
                if (currentBusinessRule.getComponentValue("CL_PWN_0006_MAXLINESPTKT", ref componentValue))
                {
                    try
                    {
                        maxLinesPerTicket = int.Parse(componentValue);
                    }
                    catch (Exception)
                    {
                        sb.AppendLine("CL_PWN_0006_MAXLINESPTKT is not a valid format");
                    }
                }
                else
                {
                    sb.AppendLine("CL_PWN_0006_MAXLINESPTKT Not found");
                }

                // CL_PWN_0068_ADDNDMALLWD
                if (currentBusinessRule.getComponentValue("CL_PWN_0068_ADDNDMALLWD", ref componentValue))
                {
                    if (componentValue == "N")
                    {
                        addendumAllowed = false;
                    }
                    else
                    {
                        addendumAllowed = true;
                    }
                }
                else
                {
                    sb.AppendLine("CL_PWN_0068_ADDNDMALLWD Not found");
                }

                // CL_PWN_0069_CONTDDESCALLWD
                if (currentBusinessRule.getComponentValue("CL_PWN_0069_CONTDDESCALLWD", ref componentValue))
                {
                    if (componentValue == "N")
                    {
                        contDescAllowed = false;
                    }
                    else
                    {
                        contDescAllowed = true;
                    }
                }
                else
                {
                    sb.AppendLine("CL_PWN_0069_CONTDDESCALLWD Not found");
                }
                #endregion

                #region PWN_BR-010
                // CL_PWN_0001_MAXLOANAMT
                currentBusinessRule = this.businessRules["PWN_BR-010"];
                decimal maxLoanAmount = 0.0M;

                if (currentBusinessRule.getComponentValue("CL_PWN_0001_MAXLOANAMT", ref componentValue))
                {
                    try
                    {
                        maxLoanAmount = Decimal.Parse(componentValue);
                    }
                    catch (Exception)
                    {
                        sb.AppendLine("CL_PWN_0001_MAXLOANAMT not in valid format");
                    }
                }
                else
                {
                    sb.AppendLine("CL_PWN_0001_MAXLOANAMT Not found");
                }

                #endregion

                #region PWN_BR-019

                // CL_PWN_0002_MINLOANAMT
                currentBusinessRule = this.businessRules["PWN_BR-019"];
                decimal minLoanAmount = 0.0M;

                if (currentBusinessRule.getComponentValue("CL_PWN_0002_MINLOANAMT", ref componentValue))
                {
                    try
                    {
                        minLoanAmount = (Decimal)double.Parse(componentValue);
                    }
                    catch (Exception)
                    {
                        sb.AppendLine("CL_PWN_0002_MINLOANAMT not in valid format");
                    }
                }
                else
                {
                    sb.AppendLine("CL_PWN_0002_MINLOANAMT Not found");
                }

                #endregion

                // CL_PWN_0004_MAXNOOFITMSPLNTKT
                // CL_PWN_0007_JEWGENMERC (not in scope)

                #region Calculations
                if (!contDescAllowed || !addendumAllowed)
                {
                    /*                    if (maxLineLength != 0)
                                        {
                                            numLines = CashlinxDesktopSession.Instance.ActivePawnLoan.TicketLength / maxLineLength;

                                            if (numLines > maxLinesPerTicket)
                                            {
                                                sb.AppendLine("Error:  The number of lines on the pawn ticket exceed the maximum allowed ($" +
                                                              maxLinesPerTicket.ToString() + ")");
                                            }
                                        }
                                        else
                                        {
                                            sb.AppendLine("Error:  Max Line Length is zero");
                                        }*/
                    if (maxLineLength > 0 && maxLinesPerTicket > 0)
                    {
                        string desc;
                        //Pre-compute description
                        if (!LoanTicketLengthCalculator.ComputeDescription(GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan, out desc))
                        {
                            sb.AppendLine("Error:  Cannot pre-compute loan description");
                        }
                        else
                        {
                            //Split description into lines
                            List<string> descLines;
                            List<PairType<int, long>> gunNumLines;
                            if (!LoanTicketLengthCalculator.SplitDescription(desc, maxLineLength, out descLines, out gunNumLines))
                            {
                                sb.AppendLine("Error:  Cannot split pre-computed loan description");
                            }
                            else
                            {
                                if (CollectionUtilities.isEmpty(descLines))
                                {
                                    sb.AppendLine("Error:  Number lines computed for loan description is zero");
                                }
                                else if (descLines.Count > maxLinesPerTicket)
                                {
                                    if (!addendumAllowed && !contDescAllowed)
                                    {
                                        sb.AppendLine(
                                                      "Error:  Cannot fit this loan on one ticket.  Please remove an item.  Loan addendum and continuation is not allowed.");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        sb.AppendLine("Error:  Value for maximum line length = 0 or maximum lines per ticket = 0");
                    }
                }
                if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Amount > maxLoanAmount)
                {
                    sb.AppendLine("Error:  Loan Amount exceeds maximum allowed loan amount ($" +
                                  maxLoanAmount.ToString() + ")");
                }

                if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Amount < minLoanAmount)
                {
                    sb.AppendLine("Error:  Loan Amount is less than minimum allowed loan amount ($" +
                                  minLoanAmount.ToString() + ")");
                }

                #endregion


            }

            if (purchaseFlow)
            {
                int numberOfGunItems = 0;
                int numberOfJewelryItems = 0;
                int numberOfGenMdseItems = 0;
                firaremAgeCheckFailed = false;
                int totalItems = GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items.Count;

                //Do business rules check for the purchase flow
                foreach (var pItem in GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items)
                {
                    if (pItem.Quantity > 1)
                        totalItems += pItem.Quantity;
                    //First check age for firearm if the customer is identified

                    if (pItem.IsGun
                        || pItem.MerchandiseType == "H"
                        || pItem.MerchandiseType == "L"
                    )
                    {
                        numberOfGunItems++;
                        if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null &&
                            !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName))
                        {
                            if (pItem.MerchandiseType == "H")
                            {
                                if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.Age < Convert.ToInt16(CustomerProcedures.getHandGunValidAge(CashlinxDesktopSession.Instance)))
                                {
                                    firaremAgeCheckFailed = true;
                                }
                            }
                            else if (pItem.MerchandiseType == "L")
                            {
                                if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.Age < Convert.ToInt16(CustomerProcedures.getLongGunValidAge(CashlinxDesktopSession.Instance)))
                                {
                                    firaremAgeCheckFailed = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (pItem.IsJewelry)
                            numberOfJewelryItems++;
                        else
                            numberOfGenMdseItems++;
                    }
                }
                if (firaremAgeCheckFailed)
                    sb.Append("This customer does not meet the age requirements for this firearm buy");
                if (numberOfGunItems > 0 && numberOfGunItems != totalItems &&
                    new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsSeparateTicketForFirearm(CashlinxDesktopSession.Instance.CurrentSiteId))
                    sb.Append("Firearms cannot be combined with any other merchandise type on a purchase ticket");
                if (numberOfGenMdseItems > 0 && numberOfJewelryItems > 0 &&
                    !new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsAllowedJewelryGenMDSEOneBuy(CashlinxDesktopSession.Instance.CurrentSiteId))
                    sb.Append("General Merchandise and Jewelry cannot be on the same buy ticket");
                if (totalItems > new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxItemsInBuyTransaction(CashlinxDesktopSession.Instance.CurrentSiteId))
                    sb.Append("You have exceeded the maximum number of Items allowed for a buy");
            }

            return sb.ToString();
        }

        #endregion

        private void ManageMultiplePawnItems_Shown(object sender, EventArgs e)
        {
            Setup();
        }
    }
}
