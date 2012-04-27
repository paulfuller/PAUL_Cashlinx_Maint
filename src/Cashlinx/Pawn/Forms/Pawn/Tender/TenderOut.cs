using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Pawn.Forms.Pawn.Tender.Base;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Tender
{
    public partial class TenderOut : CustomBaseForm
    {
        public NavBox NavControlBox;
        private decimal amountDueToCustomer;
        private readonly bool ShowCouponAmount;
        private decimal totalAmountDueToCustomer;
        private bool disableCardTenderType;
        private decimal previousAmount = 0.0M;
        public bool CancelledTransaction { get; private set; }
        public List<TenderEntryVO> TenderEntries
        {
            get
            {
                return (this.tenderTablePanel.GetTenderList);
            }
        }
        public List<TenderEntryVO> RefundTenderEntries
        {
            get
            {
                return (this.tenderTablePanel.GetRefundTenderList);
            }
        }


        private void internalTenderTypeSelectorHandler(TenderTypePanel.TenderTypeButton t)
        {
            //Check to see if amount out matches the balance already
            if (this.tenderTablePanel.TenderAmount > this.amountDueToCustomer)
            {
                MessageBox.Show(
                        "Amount tendered to the customer already meets or exceeds " + 
                        System.Environment.NewLine + "the total due to the customer.",
                        "Process Tender Warning");
                return;
            }

            TenderEntryVO tEVO = null;
            TenderEntryForm tEntryForm = null;
            switch(t)
            {
                case TenderTypePanel.TenderTypeButton.CASH:
                    tEntryForm = new TenderEntryForm(TenderTypes.CASHOUT);
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
                    tEntryForm = new TenderEntryForm(TenderTypes.STORECREDIT);
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
                decimal cashAmt = 0.0M;
                if (tEVO.TenderType == TenderTypes.CASHOUT)
                {
                    var cashTender = (from tenderData in RefundTenderEntries
                                      where tenderData.TenderType == TenderTypes.CASHOUT
                                      select tenderData).FirstOrDefault();
                    if (cashTender != null)
                    {
                        cashAmt = cashTender.Amount;
                    }
                }

                if (tEVO.Amount + (this.tenderTablePanel.TenderAmount - cashAmt - previousAmount) > this.totalAmountDueToCustomer)
                {
                    MessageBox.Show(
                            "You cannot tender more money to the customer than they are owed.",
                            "Process Tender Warning");
                    return;
                }
                this.tenderTablePanel.AddTenderEntry(tEVO, true);
                GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount -= tEVO.Amount;
            }

            //Check to see if remaining amount is zero, if so, enable continue button
            if (this.tenderTablePanel.RemainingAmount == 0.00M)
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
                //Check if the customer is looked up
                //If not enable only cash as MOP
                if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer == null ||
                    string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber) ||
                    GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber == "0")
                {
                    if (newEntry.Left != TenderTypePanel.TenderTypeButton.CASH)
                        continue;
                }
                if (disableCardTenderType && (newEntry.Left == TenderTypePanel.TenderTypeButton.CREDITORBILLAP || newEntry.Left == TenderTypePanel.TenderTypeButton.DEBIT))
                    continue;
                //Add to the list
                rt.Add(newEntry);
            }            

            return (rt);
        }

        public TenderOut()
        {            
            InitializeComponent();
            NavControlBox = new NavBox
            {
                Owner = this
            };
            SetAmountDueToCustomer(GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount);
            amountDueToCustomer = 0.00M;
            tenderTypeSelector.SetupPanel(
                TenderTypePanel.TenderTypePanelType.TENDEROUT,
                    generateEnableList(),
                        internalTenderTypeSelectorHandler);
            CancelledTransaction = false;
            this.tenderTablePanel.RowIdx = -1;
            tenderTablePanel.EditTenderDetails += new TenderTablePanel.EditTenderEntry(tenderTablePanel_EditTenderDetails);
        }

        /// <summary>
        /// Initialize the tender out form with an optional refund
        /// tender items list
        /// </summary>
        /// <param name="refundItems">If null or empty, assumed not a refund scenario</param>
        /// <param name="existingRefundItems"></param>
        public TenderOut(List<TenderEntryVO> refundItems,
            List<TenderEntryVO> existingRefundItems)
        {
            InitializeComponent();
            disableCardTenderType = false;
            NavControlBox = new NavBox
            {
                Owner = this
            };
            amountDueToCustomer = 0.00M;
            //Check if credit card or debit card were used in original payment
            if (refundItems != null)
            {
                var cardTender = (from tData in refundItems where tData.TenderType == TenderTypes.CREDITCARD || tData.TenderType == TenderTypes.DEBITCARD select tData).FirstOrDefault();
                if (cardTender == null)
                {
                    disableCardTenderType = true;
                }
            }

            SetAmountDueToCustomer(GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount);
            tenderTypeSelector.SetupPanel(
                TenderTypePanel.TenderTypePanelType.TENDEROUT,
                    generateEnableList(),
                        internalTenderTypeSelectorHandler);
            if (CollectionUtilities.isEmpty(refundItems) && CollectionUtilities.isEmpty(existingRefundItems))
            {
                tenderTablePanel.Setup(
                    null, null,
                    TenderTablePanel.TenderTablePanelType.TENDEROUT_PURCHASE,
                    0.00M);
            }
            else if (CollectionUtilities.isNotEmpty(refundItems) && CollectionUtilities.isEmpty(existingRefundItems))
            {

                tenderTablePanel.Setup(
                    refundItems,
                    null,
                    TenderTablePanel.TenderTablePanelType.TENDEROUT_REFUND,
                    0.00M);
                //Get any coupon amount tendered
                /*decimal couponAmt = (from refundData in refundItems where refundData.TenderType == TenderTypes.COUPON select refundData.Amount).Sum();
                if (couponAmt > 0)
                {
                    ShowCouponAmount = true;
                    labelCoupon.Visible = true;
                    labelCouponAmt.Text = couponAmt.ToString("C");
                    labelCouponAmt.Visible = true;
                    labelTotal.Visible = true;
                    labelTotAmount.Visible = true;
                }
                else
                {
                    ShowCouponAmount = false;
                    labelCoupon.Visible = false;
                    labelCouponAmt.Visible = false;
                    labelTotal.Visible = false;
                    labelTotAmount.Visible = false;

                */
            }
            else if (CollectionUtilities.isNotEmpty(refundItems) && CollectionUtilities.isNotEmpty(existingRefundItems))
            {

                tenderTablePanel.Setup(
                    refundItems,
                    existingRefundItems,
                    TenderTablePanel.TenderTablePanelType.TENDEROUT_PARTIALREFUND,
                    0.00M);
                //Get any coupon amount tendered
               /* decimal couponAmt = (from refundData in refundItems where refundData.TenderType == TenderTypes.COUPON select refundData.Amount).Sum();
                if (couponAmt > 0)
                {
                    ShowCouponAmount = true;
                    labelCoupon.Visible = true;
                    labelCouponAmt.Text = couponAmt.ToString("C");
                    labelCouponAmt.Visible = true;
                    labelTotal.Visible = true;
                    labelTotAmount.Visible = true;
                }
                else
                {
                    ShowCouponAmount = false;
                    labelCoupon.Visible = false;
                    labelCouponAmt.Visible = false;
                    labelTotal.Visible = false;
                    labelTotAmount.Visible = false;

                }*/

            }
            this.tenderTablePanel.RowIdx = -1;
            tenderTablePanel.EditTenderDetails += new TenderTablePanel.EditTenderEntry(tenderTablePanel_EditTenderDetails);
        }

        public void SetAmountDueToCustomer(decimal amt)
        {
            if (amt < 0.00M)
            {
                return;
            }
            this.amountDueToCustomer = amt;
            this.amountFieldLabel.Text = amt.ToString("C");
            this.amountFieldLabel.Update();
            decimal totalRefundAmt = 0.0M;

            //if coupon amount is visible then set the total amount
           /* if (ShowCouponAmount)
            {
                //Prorate the coupon amount
                decimal couponAmt = Utilities.GetDecimalValue(labelCouponAmt.Text.Substring(1), 0);
                decimal totalTranAmount=Desktop.CashlinxDesktopSession.Instance.TenderTransactionAmount.TotalAmount;
                decimal amtBeforeTax = Desktop.CashlinxDesktopSession.Instance.TenderTransactionAmount.SubTotalAmount;
                decimal proratedCouponAmt = 0.0M;
                if (totalTranAmount > 0)
                    proratedCouponAmt = ((amtBeforeTax / totalTranAmount) * couponAmt);
                if (proratedCouponAmt != 0 && proratedCouponAmt != couponAmt)
                    labelCouponAmt.Text = proratedCouponAmt.ToString("C");
                totalRefundAmt = amt - proratedCouponAmt;
                labelTotAmount.Text = (totalRefundAmt).ToString("C");
                totalAmountDueToCustomer = totalRefundAmt;
            }
            else
            */
                totalRefundAmt = amt;
                totalAmountDueToCustomer = totalRefundAmt;
            //}

            //Set remaining amount
            this.tenderTablePanel.SetRemainingAmount(totalRefundAmt);
            this.continueButton.Enabled = this.tenderTablePanel.RemainingAmount == 0.00M;            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            
            var res = MessageBox.Show("Are you sure you want to cancel this transaction?",
                            "Process Tender Warning",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {


            var res = MessageBox.Show("Are you sure you want to delete the last tender entry?",
                                      "Process Tender Warning",
                                      MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                var delRes = this.tenderTablePanel.DeleteLastEntry();
                if (delRes == false)
                {
                    MessageBox.Show("Could not delete the last tender entry. Please try again.");
                }
                else
                {
                    //Check to see if remaining amount is zero, if so, enable continue button
                    this.continueButton.Enabled = this.tenderTablePanel.RemainingAmount == 0.00M;
                }

            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            CancelledTransaction = false;
            GlobalDataAccessor.Instance.DesktopSession.TenderAmounts = new List<string>();
            GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber = new List<string>();
            GlobalDataAccessor.Instance.DesktopSession.TenderTypes = new List<string>();

            var tendOutEntries = this.RefundTenderEntries;
            if (tendOutEntries.Count > 0)
            {
                GlobalDataAccessor.Instance.DesktopSession.TenderData = tendOutEntries;
                if (GlobalDataAccessor.Instance.DesktopSession.CouponTender != null)
                {
                    GlobalDataAccessor.Instance.DesktopSession.TenderData.Add(GlobalDataAccessor.Instance.DesktopSession.CouponTender);
                }


                foreach (var tEntry in tendOutEntries)
                {
                    if (tEntry.TenderType == TenderTypes.COUPON)
                        continue;
                    string opCode = Commons.GetOutOpCode(tEntry.TenderType.ToString(),
                                                       tEntry.TenderType ==
                                                       TenderTypes.CREDITCARD
                                                               ? tEntry.CreditCardType.ToString()
                                                               : tEntry.DebitCardType.ToString());
                    decimal amount = tEntry.Amount;
                    string authNumber = tEntry.ReferenceNumber;
                    if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail != null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.TotalAmount = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TotalSaleAmount;
                        GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.SubTotalAmount = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.Amount;
                        GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.SalesTaxPercentage = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.SalesTaxPercentage;
                    }
                    else
                    {
                        GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.TotalAmount = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.TotalSaleAmount;
                        GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.SubTotalAmount = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.Amount;
                        GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.SalesTaxPercentage = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.SalesTaxPercentage;

                    }

                    GlobalDataAccessor.Instance.DesktopSession.TenderAmounts.Add(amount.ToString());
                    GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber.Add(authNumber);
                    GlobalDataAccessor.Instance.DesktopSession.TenderTypes.Add(opCode);
                }
            }
            else
            {
                GlobalDataAccessor.Instance.DesktopSession.TenderAmounts.Add("");
                GlobalDataAccessor.Instance.DesktopSession.TenderReferenceNumber.Add("");
                GlobalDataAccessor.Instance.DesktopSession.TenderTypes.Add("");
            }

                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "ProcessTender";
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            
        }




        private void TenderOut_Shown(object sender, EventArgs e)
        {
            SetAmountDueToCustomer(GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount);
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
                    tEntryForm = new TenderEntryForm(TenderTypes.CASHOUT);
                    break;
                case "Store Credit":
                    tEntryForm = new TenderEntryForm(TenderTypes.STORECREDIT);
                    break;
                case "Check":
                    tEntryForm = new TenderEntryForm(TenderTypes.CHECK);
                    break;
                case "PayPal":
                    tEntryForm = new TenderEntryForm(TenderTypes.PAYPAL);
                    break;
                default:
                    tEntryForm = new TenderEntryForm(TenderTypes.CASHOUT);
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

  
    }
}
