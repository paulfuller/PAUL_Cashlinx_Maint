using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Pawn.Forms.Pawn.Tender.Base;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Tender
{
    public partial class TenderIn : CustomBaseForm
    {
        public NavBox NavControlBox;
        private decimal amountDueFromCustomer;
        private decimal customerStoreCreditAmount;
        private decimal customerStoreCreditBalance;
        public bool CancelledTransaction { private set; get; }
        private const string shopCreditStatement = " SHOP CREDIT AVAILABLE FOR THIS CUSTOMER (BALANCE = ";
        private const string noShopCredit = "CUSTOMER HAS NO SHOP CREDIT AVAILABLE";
        private bool shopCreditShown;
        private decimal previousAmount=0.0M;

        

        private void internalTenderTypeSelectorHandler(TenderTypePanel.TenderTypeButton t)
        {
            //Check to see if amount out matches the balance already
            if (this.tenderTablePanel.TenderAmount > this.amountDueFromCustomer)
            {
                MessageBox.Show(
                        "Amount received from the customer already meets or exceeds " +
                        System.Environment.NewLine + "the total amount due from the customer.",
                        "Process Tender Warning");
                return;
            }

            TenderEntryVO tEVO = null;
            TenderEntryForm tEntryForm = null;
            List<TenderEntryVO> currentTenders = this.tenderTablePanel.GetTenderList;
            switch (t)
            {
                case TenderTypePanel.TenderTypeButton.CASH:
                    {

                        var cashTender = (from tenderData in currentTenders
                                          where tenderData.TenderType == TenderTypes.CASHIN
                                          select tenderData.Amount).FirstOrDefault();
                        tEntryForm = new TenderEntryForm(TenderTypes.CASHIN);
                        if (Utilities.GetDecimalValue(cashTender, 0) > 0)
                            tEntryForm.SetAmount = Utilities.GetDecimalValue(cashTender, 0);
                    }
                    break;
                case TenderTypePanel.TenderTypeButton.CHECK:
                    tEntryForm = new TenderEntryForm(TenderTypes.CHECK);
                    break;
                case TenderTypePanel.TenderTypeButton.CREDITORBILLAP:
                    tEntryForm = new TenderEntryForm(TenderTypes.CREDITCARD);
                    break;
                case TenderTypePanel.TenderTypeButton.DEBIT:
                    tEntryForm = new TenderEntryForm(TenderTypes.DEBITCARD);
                    break;
                case TenderTypePanel.TenderTypeButton.SHOPCREDIT:

                    //Check if the customer is looked up
                    if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer == null ||
                        string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.ShopCreditFlow = true;
                        LookupCustomer();

                    }

                    else
                    {

                        var screditTender = (from tenderData in currentTenders
                                             where tenderData.TenderType == TenderTypes.STORECREDIT
                                             select tenderData.Amount).FirstOrDefault();
                        tEntryForm = new TenderEntryForm(TenderTypes.STORECREDIT);
                        if (Utilities.GetDecimalValue(screditTender, 0) > 0)
                            tEntryForm.SetAmount = Utilities.GetDecimalValue(screditTender, 0);


                    }
                    break;
                case TenderTypePanel.TenderTypeButton.PAYPAL:
                    tEntryForm = new TenderEntryForm(TenderTypes.PAYPAL);
                    break;
            }

            if (tEntryForm != null)
            {
                var res = tEntryForm.ShowDialog(this);
                if (res == DialogResult.OK)
                {
                    //Get the tender entry
                    tEVO = tEntryForm.TenderEntry;
                }
            }
            previousAmount = 0.0M;
            this.tenderTablePanel.RowIdx = -1;
            ProcessTenderEntered(tEVO);
        }

        private void ProcessTenderEntered(TenderEntryVO tEVO)
        {
            if (tEVO != null)
            {
                if (tEVO.TenderType == TenderTypes.COUPON)
                {
                    if (tEVO.Amount > GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.SubTotalAmount)
                    {
                        MessageBox.Show("Change will exceed amount of cash tendered");
                        return;
                    }
                }
                
                decimal totalAmtCollectedFromCustomer=0.0M;
                if (this.tenderTablePanel.RemainingAmount < 0)
                    totalAmtCollectedFromCustomer = this.tenderTablePanel.TenderAmount + this.tenderTablePanel.RemainingAmount;
                else
                    totalAmtCollectedFromCustomer = this.tenderTablePanel.TenderAmount;
                if (((tEVO.TenderType != TenderTypes.CASHIN && (tEVO.Amount + totalAmtCollectedFromCustomer - previousAmount) > this.amountDueFromCustomer) || (tEVO.TenderType == TenderTypes.CASHIN && tenderTablePanel.RemainingAmount == 0)))
                {
                    MessageBox.Show(
                            "You cannot take more money from the customer than what they owe.",
                            "Process Tender Warning");
                    return;
                }
                if (tEVO.TenderType == TenderTypes.STORECREDIT)
                {
                    if (tEVO.Amount > customerStoreCreditBalance)
                    {
                        MessageBox.Show("Amount entered exceeds available shop credit", "Process Tender Error");
                        return;
                    }
                }


                this.tenderTablePanel.AddTenderEntry(tEVO, true);
                if (tEVO.TenderType == TenderTypes.STORECREDIT)
                {
                    var tenders = tenderTablePanel.GetTenderList;
                    var scTender = (from tenderData in tenders
                                    where tenderData.TenderType == TenderTypes.STORECREDIT
                                    select tenderData).FirstOrDefault();
                    if (scTender != null)
                    {
                        customerStoreCreditBalance = customerStoreCreditAmount - scTender.Amount;
                        string shopCreditAmt = customerStoreCreditBalance.ToString("C");
                        this.shopCreditLabel.Text = customerStoreCreditAmount.ToString("C") + shopCreditStatement + shopCreditAmt + @")";
                    }

                }
                /* SR 6/15/2011 Not needed anymore since coupon will not be a mop in process tender
                //If tender is coupon disable the coupon button on successful addition
                if (tEVO.TenderType == TenderTypes.COUPON)
                {
                    var tenders = tenderTablePanel.GetTenderList;
                    var couponTender = (from tenderData in tenders
                                        where tenderData.TenderType == TenderTypes.COUPON
                                        select tenderData).FirstOrDefault();
                    if (couponTender != null)
                    {

                        this.tenderTypeSelector.SetEnableButton(TenderTypePanel.TenderTypeButton.COUPON, false);
                    }
                }
                 * */
            }

            //Check to see if remaining amount is 0.00, then enable continue button
            if (this.tenderTablePanel.RemainingAmount <= 0.00M)
            {
                this.continueButton.Enabled = true;
            }
            else
            {
                this.continueButton.Enabled = false;
            }

        }

        private List<PairType<TenderTypePanel.TenderTypeButton, bool>> generateEnableList()
        {
            var rt =
                new List<PairType<TenderTypePanel.TenderTypeButton, bool>>(
                    (int)TenderTypePanel.TenderTypeButton.TTYPELENGTH);

            for (var j = TenderTypePanel.TenderTypeButton.CASH; j < TenderTypePanel.TenderTypeButton.TTYPELENGTH; ++j)
            {
                //Enabled all by default except PayPal
                var newEntry = new PairType<TenderTypePanel.TenderTypeButton, bool>(j,
                    (j != TenderTypePanel.TenderTypeButton.PAYPAL));
                //Add to the list
                rt.Add(newEntry);
            }

            return (rt);
        }

        public TenderIn()
        {
            InitializeComponent();
            NavControlBox = new NavBox
            {
                Owner = this
            };
            amountDueFromCustomer = 0.00M;
            tenderTypeSelector.SetupPanel(
                TenderTypePanel.TenderTypePanelType.NORMALSET,
                    generateEnableList(),
                        internalTenderTypeSelectorHandler);
            tenderTablePanel.Setup(null, null,
                TenderTablePanel.TenderTablePanelType.TENDERIN,
                0.00M);
            if (GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.TotalAmount < 0)
                GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.TotalAmount = 0;
            SetAmountDueFromCustomer(
                GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.TotalAmount);
            CancelledTransaction = false;
            tenderTablePanel.EditTenderDetails += new TenderTablePanel.EditTenderEntry(tenderTablePanel_EditTenderDetails);
            //SR 03/09/2011 Disabling coupon as MOP until all issues around this from the requirements standpoint is resolved
            GlobalDataAccessor.Instance.DesktopSession.DisableCoupon = true;

        }

        void tenderTablePanel_EditTenderDetails(string tenderType, string referenceNumber, string cardType, string amount)
        {
            TenderEntryForm tEntryForm = null;
            TenderEntryVO tEVO = null;
            switch (tenderType)
            {
                case "Credit Card":
                    tEntryForm = new TenderEntryForm(TenderTypes.CREDITCARD);
                    break;
                case "Debit Card":
                    tEntryForm = new TenderEntryForm(TenderTypes.DEBITCARD);
                    break;
                case "Cash":
                    tEntryForm = new TenderEntryForm(TenderTypes.CASHIN);
                    break;
                case "Store Credit":
                    tEntryForm = new TenderEntryForm(TenderTypes.STORECREDIT);
                    break;
                case "Check":
                    tEntryForm = new TenderEntryForm(TenderTypes.CHECK);
                    break;
                case "Coupon":
                    tEntryForm = new TenderEntryForm(TenderTypes.COUPON);
                    break;
                case "PayPal":
                    tEntryForm = new TenderEntryForm(TenderTypes.PAYPAL);
                    break;
                default:
                    tEntryForm = new TenderEntryForm(TenderTypes.CASHIN);
                    break;

            }
            
            tEntryForm.SetReferenceNumber = referenceNumber;
            tEntryForm.SetCardType = cardType;
            tEntryForm.SetAmount = Utilities.GetDecimalValue(amount);
            previousAmount = Utilities.GetDecimalValue(amount);
                var res = tEntryForm.ShowDialog(this);
                if (res == DialogResult.OK)
                {
                    //Get the tender entry
                    tEVO = tEntryForm.TenderEntry;
                }
            

            ProcessTenderEntered(tEVO);
        }


        public void SetAmountDueFromCustomer(decimal amt)
        {
            if (amt < 0.00M)
            {
                return;
            }
            this.amountDueFromCustomer = amt;
            this.amountFieldLabel.Text = amt.ToString("C");
            this.amountFieldLabel.Update();

            //Set remaining amount
            this.tenderTablePanel.SetRemainingAmount(amt);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            NavControlBox.IsCustom = false;
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("The cancel button is disabled in build PWN_9.0.1");
            var res = MessageBox.Show("Are you sure you want to cancel this transaction?",
                                      "Process Tender Warning",
                                      MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                CancelledTransaction = true;
                GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
                GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();
                NavControlBox.IsCustom = false;
                NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("The delete button is disabled in build PWN_9.0.1");
            var res = MessageBox.Show("Are you sure you want to delete the last tender entry?",
                                      "Process Tender Warning",
                                      MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                //Check if shop credit is a tender entry and get the amount
                var tenderList = tenderTablePanel.GetTenderList;
                var scTender = (from tenderData in tenderList
                                where tenderData.TenderType == TenderTypes.STORECREDIT
                                select tenderData).FirstOrDefault();
                decimal scTenderAmount = 0.0M;
                if (scTender != null)
                    scTenderAmount = scTender.Amount;

                var delRes = this.tenderTablePanel.DeleteLastEntry();
                if (delRes == false)
                {
                    MessageBox.Show("Could not delete the last tender entry. Please try again.");
                }
                else
                {
                    //SR 03/26/2011 Commenting until coupon tender is reenabled
                   /* tenderList = tenderTablePanel.GetTenderList;
                    var couponTender = (from tenderData in tenderList
                                        where tenderData.TenderType == TenderTypes.COUPON
                                        select tenderData).FirstOrDefault();
                    if (couponTender == null)
                        this.tenderTypeSelector.SetEnableButton(TenderTypePanel.TenderTypeButton.COUPON, true);*/
                    tenderList = tenderTablePanel.GetTenderList;
                    //If after delete shopcredit is not a tender anymore then the amount needs to be updated
                    //to the shop credit amount available for the customer
                    scTender = (from tenderData in tenderList
                                where tenderData.TenderType == TenderTypes.STORECREDIT
                                select tenderData).FirstOrDefault();
                    if (scTender == null && scTenderAmount > 0)
                    {
                        customerStoreCreditBalance += scTenderAmount;
                        if (customerStoreCreditBalance > 0)
                        {
                            string shopCreditAmt = customerStoreCreditBalance.ToString("C");
                            this.shopCreditLabel.Text = customerStoreCreditAmount.ToString("C") + shopCreditStatement + shopCreditAmt + @")";
                            shopCreditLabel.Visible = true;
                        }

                    }
                    //Check to see if remaining amount is 0.00, then enable continue button
                    this.continueButton.Enabled = this.tenderTablePanel.RemainingAmount <= 0.00M;


                }
            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            bool getCustomerData = false;
            GlobalDataAccessor.Instance.DesktopSession.TenderAmounts = new List<string>();
            GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber = new List<string>();
            GlobalDataAccessor.Instance.DesktopSession.TenderTypes = new List<string>();
            GlobalDataAccessor.Instance.DesktopSession.TenderData = this.tenderTablePanel.GetTenderList;
            foreach (var tEntry in this.tenderTablePanel.GetTenderList)
            {
                string opCode = Commons.GetInOpCode(tEntry.TenderType.ToString(), tEntry.TenderType == TenderTypes.CREDITCARD ? tEntry.CreditCardType.ToString() : tEntry.DebitCardType.ToString());

                if (tEntry.TenderType != TenderTypes.CASHIN && tEntry.TenderType != TenderTypes.COUPON)
                    getCustomerData = true;

                if (tEntry.TenderType == TenderTypes.CASHIN)
                {
                    if (tenderTablePanel.RemainingAmount < 0)
                    {
                        tEntry.Amount = tEntry.Amount + tenderTablePanel.RemainingAmount;
                        GlobalDataAccessor.Instance.DesktopSession.CashTenderToCustomer = tenderTablePanel.RemainingAmount;
                    }
                }
                decimal amount = tEntry.Amount;
                //SR 07/05/2011 If the tender amount is 0 then the tender is invalid
                if (amount == 0)
                    continue;
                string authNumber = tEntry.ReferenceNumber;
                GlobalDataAccessor.Instance.DesktopSession.TenderAmounts.Add(amount.ToString());
                GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber.Add(authNumber);
                GlobalDataAccessor.Instance.DesktopSession.TenderTypes.Add(opCode);

            }

            //lookup customer if any tender type besides cash was selected
            // if amount is > value specified in CL_RTL_240VERIFYCUSTOMERINFOREQUIRED and the customer has not been looked up yet
            //If the customer used coupon then the amount to be looked at is the amount after subtracting coupon amount
            decimal amtCollectedFromCustomer = 0.0M;
            /*var tenders = tenderTablePanel.GetTenderList;
            var couponTender = (from tenderData in tenders
                                where tenderData.TenderType == TenderTypes.COUPON
                                select tenderData).FirstOrDefault();
            if (couponTender != null)
            {
                amtCollectedFromCustomer = this.amountDueFromCustomer - couponTender.Amount;
            }
            else*/
                amtCollectedFromCustomer = this.amountDueFromCustomer;

            bool vendorSelected = false;
            bool customerSelected = false;
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null && !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name))
                vendorSelected = true;
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null && !String.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber))
                customerSelected = true;
            bool processTender = false;

            if (getCustomerData)
            {
                if ((vendorSelected || customerSelected))
                    processTender = true;
            }
            else
            {
                if (amtCollectedFromCustomer >= new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetCustomerInfoRequiredSaleValue(GlobalDataAccessor.Instance.CurrentSiteId))
                {
                    if ((vendorSelected || customerSelected))
                        processTender = true;
                }
                else
                    processTender = true;
                
            }

 
            

            if (!processTender)
            {
                GlobalDataAccessor.Instance.DesktopSession.CashSaleCustomer = !getCustomerData;
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "LookupCustomer";
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "ProcessTender";
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
        }

        private void LookupCustomer()
        {
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "LookupCustomer";
            NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;

        }

        private void TenderIn_Shown(object sender, EventArgs e)
        {
            if (!shopCreditShown)
                ShowShopCreditData();
        }

        private void TenderIn_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !shopCreditShown)
            {
                ShowShopCreditData();
            }

        }

        private void TenderIn_GotFocus(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.ShopCreditFlow)
                internalTenderTypeSelectorHandler(TenderTypePanel.TenderTypeButton.SHOPCREDIT);

        }

        private void ShowShopCreditData()
        {
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null &&
    !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber))
            {
                customerStoreCreditAmount = Utilities.GetDecimalValue((from sCredit in GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerStoreCredits
                                                                       select sCredit.Amount).Sum(), 0);
                customerStoreCreditBalance = customerStoreCreditAmount;
                if (customerStoreCreditAmount > 0)
                {
                    string shopCreditAmt = customerStoreCreditAmount.ToString("C");
                    this.shopCreditLabel.Text = shopCreditAmt + shopCreditStatement + shopCreditAmt + @")";
                    shopCreditLabel.Visible = true;
                    shopCreditShown = true;
                }
                else
                {
                    if (GlobalDataAccessor.Instance.DesktopSession.ShopCreditFlow)
                    {
                        this.shopCreditLabel.Text = noShopCredit;
                        shopCreditLabel.Visible = true;
                        this.tenderTypeSelector.SetEnableButton(TenderTypePanel.TenderTypeButton.SHOPCREDIT, false);
                        shopCreditShown = true;
                    }

                }
            }

        }



    }
}
