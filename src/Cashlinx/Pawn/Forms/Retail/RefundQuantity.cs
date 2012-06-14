using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Couch;
using Common.Libraries.Forms.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Document = Common.Libraries.Objects.Doc.Document;

namespace Pawn.Forms.Retail
{
    public partial class RefundQuantity : CustomBaseForm
    {
        private const bool DisablePartialRefund = false;
        public NavBox NavControlBox;
        private decimal couponAmt = 0;

        public RefundQuantity()
        {
            InitializeComponent();
            submitButton.Enabled = false;
            NavControlBox = new NavBox();
            Sale = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail;
            Return = Utilities.CloneObject(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail);
            Return.ShippingHandlingCharges = 0;
            Return.Fees.Clear();
        }

        # region Properties

        public bool Proceed;

        public SaleVO Return { get; set; }

        public SaleVO Sale { get; set; }

        # endregion

        # region Event Handlers

        private void RefundQuantity_Load(object sender, EventArgs e)
        {
            NavControlBox.Owner = this;
            //SR 04/01/2011 Added the logic below to not show the items that are
            //not currently part of this sale(but were once part of it)
            var notCurrentItems = (from rItems in Sale.RetailItems
                                             where rItems.DispDoc != Sale.TicketNumber || rItems.Quantity == 0
                                             select rItems).ToList();

            if (Sale.RetailItems.Count > 0)
            {

                Sale.RetailItems.ForEach((item) =>
                {
                    if (item.PreviousRetailPrice == 0)
                        item.PreviousRetailPrice = item.RetailPrice;
                });

                var _bindingSource1 = new BindingSource { DataSource = Sale.RetailItems };
                dataGridViewItems.AutoGenerateColumns = false;
                this.dataGridViewItems.DataSource = _bindingSource1;
                this.dataGridViewItems.Columns[colIcn.Index].DataPropertyName = "icn";
                this.dataGridViewItems.Columns[colMerchandiseDescription.Index].DataPropertyName = "ticketdescription";
                this.dataGridViewItems.Columns[colRetailPrice.Index].DataPropertyName = "previousretailprice";//"retailprice";
                this.dataGridViewItems.Columns[colDiscount.Index].DataPropertyName = "discountpercent";
                this.dataGridViewItems.Columns[colQuantity.Index].DataPropertyName = "quantity";
                this.dataGridViewItems.Columns[colRetailPrice.Index].DefaultCellStyle.Format = "c";
                this.dataGridViewItems.Columns[colDiscount.Index].DefaultCellStyle.Format = "0.00\\%";

                lblOriginalAmount.Text = (Sale.Amount + Sale.SalesTaxAmount).ToString("c");
                lblMsrNumber.Text = Sale.TicketNumber.ToString();

                lblCustomerName.Text = Sale.EntityName.ToString();
                lblEmployeeNumber.Text = Sale.CreatedBy.ToString();
                lblRefundedToDate.Text = GetRefundedToDate().ToString("c");
                lblBalance.Text = ((Sale.Amount + Sale.SalesTaxAmount) - GetRefundedToDate()).ToString("c");
                lblTransactionDate.Text = Sale.MadeTime.ToString();

                lblCustomerName.Visible = !string.IsNullOrEmpty(Sale.CustomerNumber) && Sale.CustomerNumber != "0";

                Calculate();

                if (DisablePartialRefund)
                {
                    selectAllButton_Click(sender, e);
                    selectAllButton.Enabled = false;
                    deselectAllButton.Enabled = false;
                    calculateButton.Enabled = false;
                    this.dataGridViewItems.Columns[colIcn.Index].ReadOnly = true;
                    Return.RetailItems = Sale.RetailItems;
                }
            }
            else
            {
                MessageBox.Show("No items left to refund in the sale");
                NavControlBox.Action = NavBox.NavAction.CANCEL;
            }

            foreach (DataGridViewRow dgvr in dataGridViewItems.Rows)
            {
                var icn = (from icnData in notCurrentItems
                          where icnData.Icn == dgvr.Cells["colIcn"].Value.ToString()
                          select icnData).FirstOrDefault();
                if (icn != null)
                {
                    dgvr.ReadOnly = true;
                    dgvr.Cells["colIcn"].ReadOnly = true;
                    dgvr.DefaultCellStyle.BackColor = Color.Gray;
                    dgvr.Cells["colQuantity"].Value = icn.Quantity;
                }
                    
            }
            if (notCurrentItems.Count == Sale.RetailItems.Count)
            {
                this.calculateButton.Enabled = false;
                this.selectAllButton.Enabled = false;
                this.deselectAllButton.Enabled = false;
                this.submitButton.Enabled = false;
            }

        }

        private decimal GetRefundedToDate()
        {
            if (Sale == null || Sale.RefundTenderData == null || Sale.RefundTenderData.Count == 0)
            {
                return 0;
            }

            return (from TenderData rd in Sale.RefundTenderData
                    select rd.TenderAmount).Sum();
        }

        void refundItem_SelectionChanged(object sender, EventArgs e)
        {
            submitButton.Enabled = false;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.Sales = new List<SaleVO>();
            foreach (RetailItem rItem in Return.RetailItems)
                rItem.PfiTags = 1;
            GlobalDataAccessor.Instance.DesktopSession.Sales.Add(Return);

            //Construct refund tender entry list
            var refundEntries = new List<TenderEntryVO>();
            var partialrefundEntries = new List<TenderEntryVO>();
            TenderTypes tType;
            CreditCardTypes cType;
            DebitCardTypes dType;
            TenderEntryVO couponTender = null;
            bool isInbound;
            if (Sale.TenderDataDetails != null)
            {
                foreach (var j in Sale.TenderDataDetails)
                {
                    var newRefEntry = new TenderEntryVO();
                    newRefEntry.Amount = j.TenderAmount;
                    if (Commons.GetTenderAndCardTypeFromOpCode(
                        j.TenderType,
                        out tType,
                        out cType,
                        out dType,
                        out isInbound))
                    {
                        if (tType == TenderTypes.COUPON)
                            continue;
                        newRefEntry.TenderType = tType;
                        if (newRefEntry.TenderType == TenderTypes.CREDITCARD)
                        {
                            newRefEntry.CardTypeString = cType.ToString();
                            newRefEntry.CreditCardType = cType;
                        }
                        else if (newRefEntry.TenderType == TenderTypes.DEBITCARD)
                        {
                            newRefEntry.CardTypeString = dType.ToString();
                            newRefEntry.DebitCardType = dType;
                        }
                    }
                    else
                    {
                        //Assume it is cash out being refunded
                        newRefEntry.TenderType = TenderTypes.CASHOUT;
                    }

                    if (newRefEntry.TenderType != TenderTypes.CASHIN &&
                        newRefEntry.TenderType != TenderTypes.CASHOUT)
                    {
                        newRefEntry.ReferenceNumber = j.TenderAuth ?? " ";
                    }
                    //if (newRefEntry.TenderType == TenderTypes.COUPON)
                    //{
                    //    couponTender = newRefEntry;
                    //}

                    //Add entry to list
                    refundEntries.Add(newRefEntry);
                }
            }
           /* if (couponTender != null)
                CashlinxDesktopSession.Instance.CouponTender = couponTender;*/
            if (Sale.RefundTenderData != null)
            {
                foreach (var j in Sale.RefundTenderData)
                {
                    var newRefEntry = new TenderEntryVO();
                    newRefEntry.Amount = j.TenderAmount;
                    if (Commons.GetTenderAndCardTypeFromOpCode(
                        j.TenderType,
                        out tType,
                        out cType,
                        out dType,
                        out isInbound))
                    {
                        if (tType == TenderTypes.COUPON)
                            continue;
                        newRefEntry.TenderType = tType;
                        if (newRefEntry.TenderType == TenderTypes.CREDITCARD)
                        {
                            newRefEntry.CardTypeString = cType.ToString();
                            newRefEntry.CreditCardType = cType;
                        }
                        else if (newRefEntry.TenderType == TenderTypes.DEBITCARD)
                        {
                            newRefEntry.CardTypeString = dType.ToString();
                            newRefEntry.DebitCardType = dType;
                        }
                    }
                    else
                    {
                        //Assume it is cash out being refunded
                        newRefEntry.TenderType = TenderTypes.CASHOUT;
                    }

                    if (newRefEntry.TenderType != TenderTypes.CASHIN &&
                        newRefEntry.TenderType != TenderTypes.CASHOUT)
                    {
                        newRefEntry.ReferenceNumber = j.TenderAuth ?? " ";
                    }

                    //Add entry to list
                    partialrefundEntries.Add(newRefEntry);
                }
            }

            decimal totalRefundAmount = Return.TotalSaleAmount;
            
            //Add call to tender out
            GlobalDataAccessor.Instance.DesktopSession.RefundEntries = refundEntries;
            GlobalDataAccessor.Instance.DesktopSession.PartialRefundEntries = partialrefundEntries;
            GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount = totalRefundAmount;
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "Tender";
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dataGridViewItems.Rows)
            {
                dgvr.Cells[colCheckbox.Index].Value = true;
                SelectUnselectRow(dgvr.Index, true);
            }

            Calculate();
            dataGridViewItems.RefreshEdit();
        }

        private void deselectAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dataGridViewItems.Rows)
            {
                dgvr.Cells[colCheckbox.Index].Value = false;
                SelectUnselectRow(dgvr.Index, false);
            }

            Calculate();
            dataGridViewItems.RefreshEdit();
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void dataGridViewItems_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colReturnQty.Index)
            {
                return;
            }

            DataGridViewCell checkBoxCell = dataGridViewItems[colCheckbox.Index,e.RowIndex];
            if (checkBoxCell.Value == null || checkBoxCell.Value.Equals(false))
            {
                return;
            }

            DataGridViewCell returnQtyCell = dataGridViewItems[colReturnQty.Index,e.RowIndex];
            RetailItem item = dataGridViewItems.Rows[e.RowIndex].DataBoundItem as RetailItem;
            int currentValue = Utilities.GetIntegerValue(returnQtyCell.EditedFormattedValue);
            if (item != null)
            {
                if (currentValue != item.Quantity && (currentValue <= 0 || currentValue > item.Quantity))
                {
                    MessageBox.Show("Invalid quantity.");
                    dataGridViewItems.CancelEdit();
                    return;
                }

            }

            dataGridViewItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            Calculate();
        }

        private void dataGridViewItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colCheckbox.Index || dataGridViewItems.Rows[e.RowIndex].ReadOnly)
            {
                return;
            }

            dataGridViewItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            submitButton.Enabled = false;

            DataGridViewCell checkBoxCell = dataGridViewItems[colCheckbox.Index,e.RowIndex];

            if (checkBoxCell.Value != null && checkBoxCell.Value.Equals(true))
            {
                SelectUnselectRow(e.RowIndex, true);
            }
            else
            {
                SelectUnselectRow(e.RowIndex, false);
            }

            Calculate();
        }

        private void dataGridViewItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            submitButton.Enabled = false;
        }

        # endregion

        # region Helper Methods

        private void Calculate()
        {
            decimal subTotal = 0;
            couponAmt = 0;
            var completeRefund = true;
            Return.RetailItems.Clear();
            var notAllowedRows = 0;
            decimal proratedCouponAmt = 0;

            foreach (DataGridViewRow row in dataGridViewItems.Rows)
            {
                var checkBoxCell = dataGridViewItems[colCheckbox.Index,row.Index];
                var returnQtyCell = dataGridViewItems[colReturnQty.Index,row.Index];

                var item = row.DataBoundItem as RetailItem;
                if (item != null)
                {
                    if (item.Quantity == 0)
                    {
                        DataGridViewCell origQtyCell = dataGridViewItems[colQuantity.Index, row.Index];
                        origQtyCell.Value = item.RefundQuantity;
                        row.ReadOnly = true;
                        row.DefaultCellStyle.BackColor = Color.Gray;
                        notAllowedRows++;
                    }

                    if (checkBoxCell.Value != null && !checkBoxCell.ReadOnly && checkBoxCell.Value.Equals(true))
                    {

                        var originalQty = 0;
                        item = Utilities.CloneObject<RetailItem>(item);
                        var quantity = Utilities.GetIntegerValue(returnQtyCell.Value);
                        originalQty = item.RefundQuantity;
                        if (item.Quantity != quantity)
                        {
                            completeRefund = false;
                            item.Quantity = quantity;
                        }
                        decimal total = Math.Round(((item.RetailPrice  * quantity) - ((item.CouponAmount/originalQty) * quantity) - ((item.ProratedCouponAmount/originalQty)*quantity)),2);
                        couponAmt += Math.Round(((item.CouponAmount/originalQty)* quantity)+ ((item.ProratedCouponAmount/originalQty) * quantity),2);
                        proratedCouponAmt += Math.Round((item.ProratedCouponAmount/originalQty) * quantity,2);
                        subTotal += total;
                        dataGridViewItems[colTotal.Index,row.Index].Value = total.ToString("c");
                        Return.RetailItems.Add(item);

                        submitButton.Enabled = true;
                    }
                    else
                    {
                        completeRefund = false;
                        dataGridViewItems[colTotal.Index,row.Index].Value = 0.ToString("c");
                    }
                }
            }

            var selectedItemsCount = SelectedItemsCountLINQ();

            // TG (02-29-2012)-> Added validation to see if at least one item is checked.
            submitButton.Enabled = notAllowedRows != dataGridViewItems.Rows.Count && selectedItemsCount > 0;

            decimal taxRate;
            if (Sale.RefNumber == 0)
                taxRate = Sale.SalesTaxAmount / (Sale.Amount - couponAmt);
            else
                taxRate = Sale.SalesTaxAmount / Sale.Amount;
            //decimal taxAmt = Math.Round(subTotal * taxRate, 2);
            decimal taxAmt = 0.0m;
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.SalesTaxPercentage > 0.0m)
            {
                taxAmt = Math.Round((GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.SalesTaxPercentage / 100) * subTotal, 2);
            }
            else
            {
                taxAmt = Math.Round(subTotal * taxRate, 2);
            }

            if (completeRefund)
            {
                taxAmt = Sale.RefNumber == 0 ? Math.Round((Sale.Amount - couponAmt) + Sale.SalesTaxAmount - GetRefundedToDate() - subTotal, 2) : Math.Round(Sale.Amount + Sale.SalesTaxAmount - GetRefundedToDate() - subTotal, 2);
            }

            lblSubtotal.Text = subTotal.ToString("c");
            lblTax.Text = taxAmt.ToString("c");
            decimal refundTotal = subTotal + taxAmt;
            lblRefundTotal.Text = refundTotal.ToString("c");

            Return.SalesTaxAmount = taxAmt;
            Return.Amount = subTotal;
            Return.TotalSaleAmount = refundTotal;
        }

        private int SelectedItemsCountLINQ()
        {
            // TG (02-29-2012)-> Calculates how many items are checked in the view. 
            int selectedItemsCount = (from DataGridViewRow r in this.dataGridViewItems.Rows
                                      where r.Cells[this.colCheckbox.Index].Value != null &&
                                            r.Cells[this.colCheckbox.Index].Value.Equals(true)
                                      select r).Count();

            return selectedItemsCount;
        }

        private void SelectUnselectRow(int rowIndex, bool selected)
        {
            DataGridViewCell returnQtyCell = dataGridViewItems[colReturnQty.Index,rowIndex];

            if (selected)
            {
                RetailItem item = dataGridViewItems.Rows[rowIndex].DataBoundItem as RetailItem;
                returnQtyCell.Value = item.Quantity;

                if (item.Quantity > 1 && !DisablePartialRefund)
                {
                    returnQtyCell.ReadOnly = false;
                    returnQtyCell.Style.BackColor = SystemColors.Highlight;
                    returnQtyCell.Style.ForeColor = Color.White;
                    dataGridViewItems.CurrentCell = returnQtyCell;
                    dataGridViewItems.BeginEdit(true);
                }
            }
            else
            {
                returnQtyCell.ReadOnly = true;
                returnQtyCell.Style.BackColor = Color.White;
                returnQtyCell.Style.ForeColor = Color.Black;

                returnQtyCell.Value = null;
            }

            dataGridViewItems.RefreshEdit();
        }

        # endregion

        private void lblMsrNumber_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Sale.Receipts.Count == 0)
            {
                return;
            }

            var docInfo = new CouchDbUtils.PawnDocInfo();
            string errString;
            string receiptNumber = Sale.Receipts[0].ReceiptNumber;

            docInfo.ReceiptNumber = Convert.ToInt32(receiptNumber);
            docInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.RECEIPT);

            var documentHelper = new DocumentHelper();
            var storageId = documentHelper.GetStorageId(DocumentHelper.RECIEPT, docInfo, out errString);

            if (string.IsNullOrEmpty(storageId))
            {
                MessageBox.Show("Could not find the document.");
                return;
            }

            documentHelper.StorageId = storageId;
            documentHelper.DocumentType = Document.DocTypeNames.RECEIPT;
            documentHelper.View(GlobalDataAccessor.Instance.DesktopSession);
        }
    }
}
