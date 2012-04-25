using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Loan;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Tender.Base
{
    public partial class TenderTablePanel : UserControl
    {
        private const string TENDER_TYPE = "Tender Type";
        private const string REF_NUMBER = "Reference Number";
        private const string AMOUNT = "Amount";
        private const string CARDTYPE = "Type";
        private const string TENDERTYPE_PURCHASE = "Purchased Tender Type";
        private const string TENDERTYPE_REFUND = "Refunded Tender Type";
        private string currentCardType;

        public delegate void EditTenderEntry(string tenderType, string referenceNumber, string cardType, string amount);

        public event EditTenderEntry EditTenderDetails;

        public static readonly string[] TenderTypeStrings = new string[]
        {
            "Cash",
            "Bill To AP",
            "Cash",
            "Check",
            "Credit Card",
            "Debit Card",
            "Store Credit",
            "PayPal",
        };

        public enum TenderTablePanelType
        {
            TENDERIN,
            TENDEROUT_PURCHASE,
            TENDEROUT_REFUND,
            TENDEROUT_PARTIALREFUND
        }

        //        private DataTable tenderOutPurchaseDataTable;
        public DataGridView TenderOutPurchaseDataTable
        {
            get
            {
                return (tenderOutPurchaseDataGridView);
            }
        }

        //        private DataTable tenderOutRefundedDataTable;
        public DataGridView TenderOutRefundedDataTable
        {
            get
            {
                return (tenderOutRefundDataGridView);
            }
        }

        //        private DataTable tenderInDataTable;
        public DataGridView TenderInDataTable
        {
            get
            {
                return (tenderInDataGridView);
            }
        }

        public DataGridView TenderOutPartialRefundDataTable
        {
            get
            {
                return (tenderOutPartialRefundDataGridView);
            }
        }

        public List<TenderEntryVO> GetTenderList { get; private set; }

        public List<TenderEntryVO> GetRefundTenderList { get; private set; }

        public List<TenderEntryVO> GetPartialRefundTenderList
        {
            get
            {
                return (partialRefundTenderList);
            }
        }

        public int RowIdx;
        private TenderTablePanelType tenderTableType;
        private readonly List<TenderEntryVO> partialRefundTenderList;
        private bool initialTenderAdded;
        private decimal remainingAmount;

        public decimal RemainingAmount
        {
            get
            {
                return (this.remainingAmount);
            }
        }

        private decimal tenderAmount;

        public decimal TenderAmount
        {
            get
            {
                return (this.tenderAmount);
            }
        }

        public TenderTablePanel()
        {
            InitializeComponent();
            tenderTableType = TenderTablePanelType.TENDERIN;
            GetTenderList = new List<TenderEntryVO>();
            GetRefundTenderList = new List<TenderEntryVO>();
            partialRefundTenderList = new List<TenderEntryVO>();
            tenderAmount = 0.0M;
            //remainingAmount = 0.0M;
        }

        public void Setup(
            List<TenderEntryVO> existingTendOutList,
            List<TenderEntryVO> existingRefOutList,
            TenderTablePanelType tType,
            decimal remAmount)
        {
            this.tenderTableType = tType;
            //this.remainingAmount = remAmount;
            if (this.tenderTableType == TenderTablePanelType.TENDEROUT_REFUND ||
                this.tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND)
            {
                partialRefundTenderList.Clear();
                GetRefundTenderList.Clear();
                GetTenderList.Clear();
                if (CollectionUtilities.isNotEmpty(existingTendOutList))
                {
                    //refundTenderList.AddRange(existingTendOutList);
                    GetTenderList.AddRange(existingTendOutList);
                }

                if (this.tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND)
                {
                    partialRefundTenderList.AddRange(existingRefOutList);
                }
            }
        }

        public void SetRemainingAmount(decimal remAmount)
        {
            this.remainingAmount = remAmount;
            this.adjustAmount(0.00M);
        }

        public void AddTenderEntry(TenderEntryVO tenderEntry)
        {
            this.AddTenderEntry(tenderEntry, false);
        }

        public void AddTenderEntry(TenderEntryVO tenderEntry, bool addToInternalList)
        {
            if (tenderEntry == null) return;

            string cardType = string.Empty;
            DataGridView dV;
            cardType = tenderEntry.CardTypeString ?? string.Empty;
            //Determine table to create data row from
            if (this.tenderTableType == TenderTablePanelType.TENDERIN)
            {
                dV = this.tenderInDataGridView;
            }
            else
            {
                if (this.tenderTableType == TenderTablePanelType.TENDEROUT_REFUND ||
                    this.tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND)
                {
                    dV = (this.tenderTableType == TenderTablePanelType.TENDEROUT_REFUND) ?
                         this.tenderOutRefundDataGridView :
                         this.tenderOutPurchaseDataGridView;
                    if (!addToInternalList)
                    {
                        if (this.tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND)
                            dV = this.tenderOutPartialRefundDataGridView;
                        else
                        //Initial loading of purchases noted for refund
                            dV = this.tenderOutPurchaseDataGridView;
                    }
                    else
                    {
                        if (initialTenderAdded)
                            dV = this.tenderOutRefundDataGridView;
                    }
                }
                else
                {
                    dV = this.tenderOutPurchaseDataGridView;
                }
            }

            //Add common data row types
            string tenderType = TenderTypeStrings[(int)tenderEntry.TenderType];
            //Enum.Format(typeof(TenderTypes), tenderEntry.TenderType, "g");
            string refNumber = tenderEntry.ReferenceNumber ?? string.Empty;
            string amount = tenderEntry.Amount.ToString("C");

            //Construct data row array
            object[] rowData = null;
            rowData = new string[]
            {
                tenderType, refNumber, cardType, amount
            };

            if ((this.tenderTableType == TenderTablePanelType.TENDEROUT_REFUND ||
                 this.tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND) &&
                 addToInternalList && initialTenderAdded)
            {
                string msg;
                if (!this.validateRefundTenderMatrix(tenderEntry, out msg))
                {
                    MessageBox.Show("Improper tender added: " + msg);
                    return;
                }
            }

            /* SR 6/15/2011 Removing coupon as a MOP
            //SR 12/27/2010 If the current tender entry type is coupon make sure this is the
            //first type selected
            if (this.tenderTableType == TenderTablePanelType.TENDERIN && tenderEntry.TenderType == TenderTypes.COUPON)
            {
                if (GetTenderList.Count > 0)
                {
                    MessageBox.Show("Coupon Tender needs to be applied first. " + System.Environment.NewLine + "Please remove previously used tender types.", "Process Tender Warning");
                    return;
                }
            }*/
            
            //SR 03/12/2011 If the current tender entry for tender in is store credit ask for manager override
            //if the transaction is sale
            if (this.tenderTableType == TenderTablePanelType.TENDERIN && tenderEntry.TenderType == TenderTypes.STORECREDIT && GlobalDataAccessor.Instance.DesktopSession.CompleteSale)
            {
                var overrideTypes = new List<ManagerOverrideType>();
                var transactionTypes = new List<ManagerOverrideTransactionType>();
                var messageToShow = new StringBuilder();
                messageToShow.Append("Store Credit is being used ");
                overrideTypes.Add(ManagerOverrideType.SCRDT);
                transactionTypes.Add(ManagerOverrideTransactionType.SALE);
                var overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER)
                {
                    MessageToShow = messageToShow.ToString(),
                    ManagerOverrideTypes = overrideTypes,
                    ManagerOverrideTransactionTypes = transactionTypes

                };

                overrideFrm.ShowDialog();
                if (!overrideFrm.OverrideAllowed)
                {
                    MessageBox.Show("Manager override needed to proceed with using store credit");
                    return;
                }
            }

            //SR 12/27/2010 Check if the current tender type is cash and there is already a cash tender
            //type entered. In that case the amount needs to be added to the original row and should not
            //enter a new row
            //SR 2/23/2011 The same logic applies for Shop credit
            bool addRow = true;
            decimal currentRowAmount = 0.0m;

            if (RowIdx == -1)
            {
                if ((tenderTableType == TenderTablePanelType.TENDERIN && (tenderEntry.TenderType == TenderTypes.CASHIN || tenderEntry.TenderType == TenderTypes.STORECREDIT)) ||
                    ((tenderTableType == TenderTablePanelType.TENDEROUT_REFUND || tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND) &&
                     (tenderEntry.TenderType == TenderTypes.CASHOUT || (tenderEntry.TenderType == TenderTypes.STORECREDIT && addToInternalList))))
                {
                    foreach (DataGridViewRow dgvr in dV.Rows)
                    {
                        var strObjVal = (dgvr.Cells.Count > 0)
                                        ? dgvr.Cells[0].Value
                                        : null;
 
                        if (strObjVal != null && tenderType == strObjVal.ToString() && (tenderEntry.TenderType == TenderTypes.CASHIN || tenderEntry.TenderType == TenderTypes.CASHOUT) && (strObjVal.ToString().Equals("Cash", StringComparison.OrdinalIgnoreCase)))
                        {
                            var strObjVal2 = (dgvr.Cells.Count > 3)
                                             ? dgvr.Cells[3].Value
                                             : null;
                            if (strObjVal2 != null)
                            {
                                var cellValStr = strObjVal2.ToString();
                                if (!string.IsNullOrEmpty(cellValStr) && cellValStr.Length > 1)
                                {
                                    currentRowAmount = Utilities.GetDecimalValue(cellValStr.Substring(1));
                                    currentCardType = string.Empty;
                                    dgvr.Cells[3].Value = amount;
                                    addRow = false;
                                }
                            }
                        }
                        if (strObjVal != null && tenderEntry.TenderType == TenderTypes.STORECREDIT && (strObjVal.ToString().Equals("Store Credit", StringComparison.OrdinalIgnoreCase)))
                        {
                            var strObjVal2 = (dgvr.Cells.Count > 3)
                                             ? dgvr.Cells[3].Value
                                             : null;
                            if (strObjVal2 != null)
                            {
                                var cellValStr = strObjVal2.ToString();
                                if (!string.IsNullOrEmpty(cellValStr) && cellValStr.Length > 1)
                                {
                                    currentRowAmount = Utilities.GetDecimalValue(cellValStr.Substring(1));
                                    currentCardType = string.Empty;
                                    dgvr.Cells[3].Value = amount;
                                    addRow = false;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                currentRowAmount = Utilities.GetDecimalValue(dV.Rows[RowIdx].Cells[3].Value.ToString().Substring(1));
                currentCardType = Utilities.GetStringValue(dV.Rows[RowIdx].Cells[2].Value);
                dV.Rows.RemoveAt(RowIdx);
                dV.Rows.Insert(RowIdx, rowData);
                addRow = false;
            }

            //Add data row array to appropriate data grid view
            if (addRow)
                dV.Rows.Add(rowData);

            //Add entry to tender list

            if (addToInternalList)
            {
                if (this.tenderTableType == TenderTablePanelType.TENDERIN)
                {
                    if (!addRow)
                    {
                        int rowIdx = GetTenderList.FindIndex(tenderData => tenderData.TenderType == tenderEntry.TenderType && tenderData.CardTypeString == currentCardType);
                        if (rowIdx >= 0)
                        {
                            GetTenderList.RemoveAt(rowIdx);
                            GetTenderList.Insert(rowIdx, tenderEntry);
                            this.adjustAmount(-currentRowAmount);
                        }
                    }
                    else
                        this.GetTenderList.Add(tenderEntry);
                }
                else if (this.tenderTableType == TenderTablePanelType.TENDEROUT_REFUND || this.tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND)
                {
                    if (!addRow)
                    {
                        int rowIdx = GetRefundTenderList.FindIndex(tenderData => tenderData.TenderType == tenderEntry.TenderType && tenderData.CardTypeString == currentCardType);
                        if (rowIdx >= 0)
                        {
                            GetRefundTenderList.RemoveAt(rowIdx);
                            GetRefundTenderList.Insert(rowIdx, tenderEntry);
                            this.adjustAmount(-currentRowAmount);
                        }
                    }
                    else
                    if (initialTenderAdded)
                        this.GetRefundTenderList.Add(tenderEntry);
                }

                decimal adjustTenderAmount = tenderEntry.Amount;
                if (this.tenderTableType == TenderTablePanelType.TENDERIN)
                    this.adjustAmount(adjustTenderAmount);
                else if (dV != this.tenderOutPurchaseDataGridView)
                    this.adjustAmount(adjustTenderAmount);
            }
        }

        private bool findRefundAmountByTenderType(TenderTypes tType, string cardTypeString, out List<TenderEntryVO> entries, out decimal amount)
        {
            entries = null;
            amount = 0.0M;
            decimal existingRefundTender = 0.0M;
            decimal refundAmtForTender = 0.0M;
            //Find entries in the original tender 
            entries = this.GetTenderList.FindAll(x => x.TenderType == tType && x.CardTypeString.ToUpper() == cardTypeString.ToUpper());
            if (CollectionUtilities.isEmpty(entries))
            {
                return (false);
            }
            //Find entries already refunded
            var existRefundEntries = this.partialRefundTenderList.FindAll(y => y.TenderType == tType && y.CardTypeString.ToUpper() == cardTypeString.ToUpper());
            if (!CollectionUtilities.isEmpty(existRefundEntries))
                existingRefundTender = existRefundEntries.Sum(y => y.Amount);

            //Find entries in the tender currently refunded
            var refundEntries = this.GetRefundTenderList.FindAll(y => y.TenderType == tType && y.CardTypeString.ToUpper() == cardTypeString.ToUpper());
            if (!CollectionUtilities.isEmpty(refundEntries))
                refundAmtForTender = refundEntries.Sum(y => y.Amount);

            //If entries found, compute sum
            //The sum allowed will be the sum of amounts tendered at the time of sale minus the amounts selected for refund 
            //for the same tender type
            amount = entries.Sum(x => x.Amount) - (refundAmtForTender + existingRefundTender);

            return (true);
        }

        private bool validateRefundTenderMatrix(TenderEntryVO entry, out string validationErrorMsg)
        {
            validationErrorMsg = string.Empty;
            if (entry.TenderType == TenderTypes.CREDITCARD ||
                entry.TenderType == TenderTypes.DEBITCARD)
            {
                List<TenderEntryVO> tendCredDebitEntries;
                decimal amount;
                //Attempt to find a refund amount entry whose tender type matches that of a potential
                //refund line item
                if (!findRefundAmountByTenderType(entry.TenderType, entry.CardTypeString, out tendCredDebitEntries, out amount))
                {
                    validationErrorMsg =
                    "You cannot refund onto a credit/debit card when one was not used in the original transaction";
                }

                //If entry amount exceeds credit used in original transaction, entry is not valid
                decimal previousAmount = 0.0M;
                if (RowIdx >= 0)
                {
                    previousAmount = Utilities.GetDecimalValue(tenderOutRefundDataGridView.Rows[RowIdx].Cells[3].Value.ToString().Substring(1));
                }
                if (entry.Amount - previousAmount > amount)
                {
                    validationErrorMsg =
                    "You cannot refund more onto a credit/debit card than was used in the original transaction.";
                    return (false);
                }
            }

            return (true);
        }

        private void adjustAmount(decimal offset)
        {
            this.tenderAmount += offset;
            this.remainingAmount -= offset;

            if (this.tenderAmount < 0.00M)
            {
                this.tenderAmount = 0.00M;
            }

            //Update label
            this.amountLabel.Text = this.tenderAmount.ToString("C");

            if (remainingAmount < 0.0M)
            {
                this.remainingLabel.Text = "Change Due to Customer:";
                this.remainingLabel.Location = this.tenderTableType == TenderTablePanelType.TENDERIN ? new Point(167, 505) : new Point(167, this.remainingLabel.Location.Y);
                this.remainingAmountFieldLabel.Location = this.tenderTableType == TenderTablePanelType.TENDERIN || tenderTableType == TenderTablePanelType.TENDEROUT_REFUND ? new Point(remainingLabel.Location.X + remainingLabel.Text.Length + 180, 505) : new Point(remainingLabel.Location.X + remainingLabel.Text.Length + 180, this.remainingLabel.Location.Y);
                this.remainingAmountFieldLabel.Text = Math.Abs(remainingAmount).ToString("C");
            }
            else
            {
                this.remainingLabel.Text = "Balance Due:";
                this.remainingLabel.Location = this.tenderTableType == TenderTablePanelType.TENDERIN ? new Point(247, 505) : new Point(247, this.remainingLabel.Location.Y);
                this.remainingAmountFieldLabel.Location = this.tenderTableType == TenderTablePanelType.TENDERIN ? new Point(remainingLabel.Location.X + remainingLabel.Text.Length + 100, 505) : new Point(remainingLabel.Location.X + remainingLabel.Text.Length + 100, this.remainingLabel.Location.Y);
                this.remainingAmountFieldLabel.Text = this.remainingAmount.ToString("C");
            }
        }

        public void ShowTenderInTable(bool showVal)
        {
            if (showVal)
            {
                //Make tender out tables invisible
                this.ShowTenderOutTables(false);

                //Make tender in tables visible
                this.tenderInDataGridView.Enabled = true;
                this.tenderInDataGridView.Show();
                this.tenderInDataGridView.BringToFront();
            }
            else
            {
                this.tenderInDataGridView.Enabled = false;
                this.tenderInDataGridView.Hide();
                this.tenderInDataGridView.SendToBack();
            }
        }

        public void ShowTenderOutTables(bool showVal)
        {
            if (showVal)
            {
                //Make tender in table invisible
                this.ShowTenderInTable(false);

                //Based on the tender table panel type,
                //enable one or both tender out table views
                //Purchase should always be visible
                this.tenderOutPurchaseDataGridView.Enabled = true;
                this.tenderOutPurchaseDataGridView.Show();
                this.tenderOutPurchaseDataGridView.BringToFront();
                this.tenderOutPurchaseDataGridView.BackColor = Color.WhiteSmoke;
                this.tenderOutPurchaseDataGridView.ReadOnly = false;
                this.remainingLabel.Location = new Point(243, 541);
                this.remainingAmountFieldLabel.Location = new Point(356, 541);

                if (this.tenderTableType == TenderTablePanelType.TENDEROUT_REFUND ||
                    this.tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND)
                {
                    //Make tender out purchase table read-only
                    this.tenderOutPurchaseDataGridView.ReadOnly = true;
                    this.tenderOutPurchaseDataGridView.Enabled = true;
                    this.tenderOutPurchaseDataGridView.BackColor = Color.Gray;

                    //Make refund table viewable
                    this.tenderOutRefundDataGridView.Enabled = true;
                    this.tenderOutRefundDataGridView.Show();
                    this.tenderOutRefundDataGridView.BringToFront();

                    //If we have partial refund, make partial refund table visible                    
                    if (this.tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND)
                    {
                        this.tenderOutPartialRefundDataGridView.Enabled = true;
                        this.tenderOutPartialRefundDataGridView.Show();
                        this.tenderOutPartialRefundDataGridView.BringToFront();
                        this.tenderOutPartialRefundDataGridView.ReadOnly = true;
                        this.tenderOutPartialRefundDataGridView.BackColor = Color.Gray;

                        //Move refund table down by the height of the partial refund
                        //data grid view
                        this.tenderOutRefundDataGridView.Location = new Point(this.tenderOutPartialRefundDataGridView.Location.X,
                                                                              this.tenderOutPartialRefundDataGridView.Location.Y + this.tenderOutPartialRefundDataGridView.Height + 2);
                    }
                    //Move the remaining amount label
                    this.remainingLabel.Location = new Point(this.remainingLabel.Location.X, this.tenderOutRefundDataGridView.Location.Y + this.tenderOutRefundDataGridView.Height + 2);
                    this.remainingAmountFieldLabel.Location = new Point(this.remainingAmountFieldLabel.Location.X, this.tenderOutRefundDataGridView.Location.Y + this.tenderOutRefundDataGridView.Height + 2);
                }
            }
            else
            {
                //Refund table hide
                this.tenderOutRefundDataGridView.Enabled = false;
                this.tenderOutRefundDataGridView.Hide();
                this.tenderOutRefundDataGridView.SendToBack();
                //Move back to original location in case we had a partial refund situation
                this.tenderOutRefundDataGridView.Location = new Point(this.tenderOutPartialRefundDataGridView.Location.X,
                                                                      this.tenderOutPartialRefundDataGridView.Location.Y);
                //Partial refund table hide
                this.tenderOutPartialRefundDataGridView.Enabled = false;
                this.tenderOutPartialRefundDataGridView.Hide();
                this.tenderOutPartialRefundDataGridView.SendToBack();
                //Tender out table hide
                this.tenderOutPurchaseDataGridView.Enabled = false;
                this.tenderOutPurchaseDataGridView.Hide();
                this.tenderOutPurchaseDataGridView.SendToBack();
            }
        }

        private void TenderTablePanel_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            //Determine look of panel based on type
            //this.adjustAmount(0.00M);
            switch (this.tenderTableType)
            {
                case TenderTablePanelType.TENDERIN:
                    //Setup tender data table for the in situation
                    this.ShowTenderInTable(true);
                    break;

                case TenderTablePanelType.TENDEROUT_PURCHASE:
                    //Show tender out purchase table only
                    this.ShowTenderOutTables(true);
                    break;
                case TenderTablePanelType.TENDEROUT_REFUND:
                    //Show both tender out purchase table and refund table
                    this.ShowTenderOutTables(true);
                    initialTenderAdded = true;
                    //If existing tender entries exist, add them
                    if (CollectionUtilities.isNotEmpty(this.GetTenderList))
                    {
                        foreach (var entry in this.GetTenderList)
                        {
                            this.AddTenderEntry(entry);
                        }
                    }
                    break;
                case TenderTablePanelType.TENDEROUT_PARTIALREFUND:
                    //Show both tender out purchase table and refund table
                    this.ShowTenderOutTables(true);

                    //If existing tender entries exist, add them
                    if (CollectionUtilities.isNotEmpty(this.GetTenderList))
                    {
                        foreach (var entry in this.GetTenderList)
                        {
                            this.AddTenderEntry(entry, true);
                        }
                        initialTenderAdded = true;
                    }

                    if (CollectionUtilities.isNotEmpty(this.partialRefundTenderList))
                    {
                        foreach (var entry in this.partialRefundTenderList)
                        {
                            this.AddTenderEntry(entry, false);
                        }
                    }

                    break;
            }
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            //TODO: Does nothing now, need to determine functionality
            //in build 9.0.2
        }

        public bool DeleteLastEntry()
        {
            //Verify that we have entries to delete
            if (this.tenderTableType == TenderTablePanelType.TENDERIN)
            {
                if (CollectionUtilities.isEmpty(this.GetTenderList))
                {
                    return (false);
                }
            }
            if (this.tenderTableType == TenderTablePanelType.TENDEROUT_REFUND ||
                this.tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND)
            {
                if (CollectionUtilities.isEmpty(this.GetRefundTenderList))
                {
                    return (false);
                }
            }

            //If this is a refund situation, and we have the same number of entries in the tender
            //list than what is in the original refund list, then we have nothing to delete
            /* if ((this.tenderTableType == TenderTablePanelType.TENDEROUT_REFUND ||
            this.tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND) &&
            this.tenderList.Count == this.refundTenderList.Count)
            {
            return (false);
            }*/

            //If we made it here, remove the last entry from the tender list and
            //the last row from the appropriate data grid view
            int lastTenderEntryIdx = -1;
            DataGridView dV = null;
            if (this.tenderTableType == TenderTablePanelType.TENDERIN)
            {
                dV = this.tenderInDataGridView;
            }
            else if (this.tenderTableType == TenderTablePanelType.TENDEROUT_PURCHASE)
            {
                dV = this.tenderOutPurchaseDataGridView;
            }
            else if (this.tenderTableType == TenderTablePanelType.TENDEROUT_REFUND ||
                     this.tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND)
            {
                dV = this.tenderOutRefundDataGridView;
            }

            if (this.tenderTableType == TenderTablePanelType.TENDERIN)
            {
                lastTenderEntryIdx = this.GetTenderList.Count - 1;
                if (lastTenderEntryIdx < 0)
                {
                    return (false);
                }
            }
            else if (this.tenderTableType == TenderTablePanelType.TENDEROUT_REFUND ||
                     this.tenderTableType == TenderTablePanelType.TENDEROUT_PARTIALREFUND)
            {
                lastTenderEntryIdx = this.GetRefundTenderList.Count - 1;
                if (lastTenderEntryIdx < 0)
                {
                    return (false);
                }
            }

            //Ensure we have a view to delete from
            if (dV != null && lastTenderEntryIdx >= 0)
            {
                //SR 01/02/2011 When we implement deleting any row add this back
                //At present this will not work if the user entered another cash tender entry which
                //would have updated the original row and the user is in that row
                //but that may not be the last entry in the tenderList
                //var lastdRIdx = dV.Rows.GetLastRow(DataGridViewElementStates.Displayed);
                var lastdRIdx = dV.Rows.Count - 1;
                if (lastdRIdx >= 0)
                {
                    //Delete the row
                    dV.Rows.RemoveAt(lastdRIdx);

                    //Get the amount to adjust
                    var lastAmt = (this.tenderTableType == TenderTablePanelType.TENDERIN) ?
                                  this.GetTenderList[lastTenderEntryIdx].Amount :
                                  this.GetRefundTenderList[lastTenderEntryIdx].Amount;

                    if (this.tenderTableType == TenderTablePanelType.TENDERIN &&
                        GetTenderList[lastTenderEntryIdx].TenderType == TenderTypes.COUPON)
                    {
                        lastAmt = lastAmt + ((GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.SalesTaxPercentage / 100) * lastAmt);
                    }

                    //Delete the tender entry
                    if (this.tenderTableType == TenderTablePanelType.TENDERIN)
                    {
                        this.GetTenderList.RemoveAt(lastTenderEntryIdx);
                    }
                    else
                    {
                        this.GetRefundTenderList.RemoveAt(lastTenderEntryIdx);
                    }

                    //Adjust the amount by reversing the deleted entry amount
                    this.adjustAmount(-lastAmt);

                    return (true);
                }
            }

            return (false);
        }

        private void tenderInDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
                {
                    RowIdx = e.RowIndex;
                    string tenderInType = tenderInDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string refNumber = tenderInDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string cardType = tenderInDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string amount = tenderInDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString().Substring(1);
                    EditTenderDetails(tenderInType, refNumber, cardType, amount);
                }
            }
        }

        private void tenderOutRefundDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
                {
                    RowIdx = e.RowIndex;
                    string tenderInType = tenderOutRefundDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string refNumber = tenderOutRefundDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string cardType = tenderOutRefundDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string amount = tenderOutRefundDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString().Substring(1);
                    EditTenderDetails(tenderInType, refNumber, cardType, amount);
                }
            }
        }
    }
}
