/**************************************************************************************************************
* CashlinxDesktop
* CustomerHoldList
* This form is used to show the list of transactions for a customer available for hold
* Sreelatha Rengarajan 8/6/2009 Initial version
* SR 7/23/2010 Fixed the issue of pick slip print not working
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Pawn.Products.DescribeMerchandise;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Pawn.Logic;
using Pawn.Logic.PrintQueue;

namespace Pawn.Forms.Pawn.Customer.Holds
{
    public partial class CustomerHoldsList : Form
    {
        public NavBox NavControlBox;
        private Form ownerfrm;
        private BindingSource _bindingSource1;
        private BindingSource _bindingSource2;
        private DataTable _transactionTable;
        private DataTable _merchandiseTable;
        private string _errorCode = "";
        private string _errorMsg = "";
        private string _storeNumber = "";
        private string _customerNumber = "";
        private string _userId = "";
        private readonly string holdType = HoldData.CUSTOMER_HOLD.ToString();
        private int _pageIndex = 0;
        private int _numberOfPages = 0;
        private const int maxRows = 7;
        private bool _rowSelected = false;
        private bool _locationSet = false;
        private int _numberOfSelections = 0;
        List<HoldData> _selectedTransactions = new List<HoldData>();
        List<int> _selectedLoanNumbers = new List<int>();
        private int _rowClicked = 0;

        public CustomerHoldsList()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }

        private void CustomerHoldsList_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;
            _userId = GlobalDataAccessor.Instance.DesktopSession.UserName;
            CustomerVO activeCustomer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            if (activeCustomer != null)
            {
                _storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                _customerNumber = activeCustomer.CustomerNumber;
            }
            else
            {
                MessageBox.Show(Commons.GetMessageString("MissingCustData"));
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
            //Create the transaction Table
            _transactionTable = new DataTable();
            _merchandiseTable = new DataTable();
            bool retVal = HoldsProcedures.ExecuteGetHolds(_storeNumber, _customerNumber, holdType,
                                                          out _transactionTable, out _merchandiseTable, out _errorCode, out _errorMsg);

            if (retVal && _transactionTable != null)
            {
                DataColumn[] key = new DataColumn[1];
                key[0] = _transactionTable.Columns[holdstransactioncursor.TICKETNUMBER];
                _transactionTable.PrimaryKey = key;
                _bindingSource1 = new BindingSource { DataSource = _transactionTable };
                this.customDataGridViewTransactions.AutoGenerateColumns = false;
                this.customDataGridViewTransactions.DataSource = _bindingSource1;
                this.customDataGridViewTransactions.Columns[2].DataPropertyName = holdstransactioncursor.TRANSACTIONDATE;
                this.customDataGridViewTransactions.Columns[3].DataPropertyName = holdstransactioncursor.TRANSACTIONTYPE;//"transactiontype";
                this.customDataGridViewTransactions.Columns[4].DataPropertyName = holdstransactioncursor.TICKETNUMBER;//"ticket_number";
                this.customDataGridViewTransactions.Columns[5].DataPropertyName = holdstransactioncursor.STATUS;//"pstatus";
                this.customDataGridViewTransactions.Columns[6].DataPropertyName = holdstransactioncursor.PFISTATE;//"pfi_state";

                if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId))
                {
                    customDataGridViewTransactions.Columns[7].DataPropertyName = holdstransactioncursor.CURRENTPRINCIPALAMOUNT;//"cur_amount in payment_detail table";
                    customDataGridViewTransactions.Columns[7].HeaderText = "Current Principal Amount";
                }
                else
                {
                    customDataGridViewTransactions.Columns[7].DataPropertyName = holdstransactioncursor.LOANAMOUNT;//"org_amount";
                     
                }

                this.customDataGridViewTransactions.Columns[8].DataPropertyName = holdstransactioncursor.ORIGINALTICKETNUMBER;//"org_ticket";
                this.customDataGridViewTransactions.Columns[9].DataPropertyName = holdstransactioncursor.PREVIOUSTICKETNUMBER;//"prev_ticket";
                this.customDataGridViewTransactions.Columns[1].ReadOnly = false;

                this.customDataGridViewTransactions.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.customDataGridViewMDSE.AutoGenerateColumns = false;
                FindNumberOfPages(_transactionTable);
            }
            else
            {
                MessageBox.Show(Commons.GetMessageString("CustHoldsNoTransactions"));
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        private void dataGridViewTransactions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 0 && !customDataGridViewTransactions.Rows[e.RowIndex].Cells[0].ReadOnly)
                {
                    try
                    {
                        if (this.customDataGridViewMDSE.Visible == false)
                        {
                            _bindingSource2 = new BindingSource();
                            DataRowView drv = (DataRowView)(_bindingSource1.Current);
                            DataRow selectedRow = _transactionTable.Rows.Find(drv.Row[holdstransactioncursor.TICKETNUMBER]);
                            string origTicketNumber = selectedRow[holdstransactioncursor.ORIGINALTICKETNUMBER].ToString();
                            if (_merchandiseTable != null && _merchandiseTable.Rows.Count > 0)
                            {
                                _rowSelected = true;
                                DataRow[] merchandiseRows = _merchandiseTable.Select(string.Format("{0}='{1}'", holdsmdsecursor.ICNDOC, origTicketNumber));
                                if (merchandiseRows.Length == 0)
                                    return;
                                ((DataGridViewImageCell)customDataGridViewTransactions.Rows[e.RowIndex].Cells[0]).Value = global::Common.Properties.Resources.minus_icon_small;
                                foreach (DataGridViewRow dgvr in customDataGridViewTransactions.Rows)
                                {
                                    if (dgvr.Index != e.RowIndex)
                                    {
                                        dgvr.Cells[0].ReadOnly = true;
                                        dgvr.Cells[1].ReadOnly = true;
                                    }
                                }

                                DataTable newMdseTable = _merchandiseTable.Clone();
                                foreach (DataRow dr in merchandiseRows)
                                    newMdseTable.ImportRow(dr);
                                _bindingSource2.DataSource = newMdseTable;
                                this.customDataGridViewTransactions.Rows[e.RowIndex].Height += this.customDataGridViewMDSE.Height;
                                this.customDataGridViewTransactions.Rows[e.RowIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                                this.customDataGridViewMDSE.DataSource = _bindingSource2;
                                this.customDataGridViewMDSE.Columns[0].DataPropertyName = holdsmdsecursor.ITEMDESCRIPTION;//"md_desc";
                                this.customDataGridViewMDSE.Columns[1].DataPropertyName = holdsmdsecursor.ICN;//"icn";
                                this.customDataGridViewMDSE.Columns[2].DataPropertyName = holdsmdsecursor.STATUS;//"mstatus";

                                if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId))
                                {
                                    this.customDataGridViewMDSE.Columns[3].DataPropertyName = holdsmdsecursor.AMOUNT;//"cur_amount in payment_detail table";
                                }
                                else
                                {
                                    this.customDataGridViewMDSE.Columns[3].DataPropertyName = holdsmdsecursor.AMOUNT;//"org_amount";
                                }

                                this.customDataGridViewMDSE.Visible = true;

                                _rowClicked = e.RowIndex;
                                if (_rowClicked >= maxRows)
                                    _rowClicked = _rowClicked - (maxRows * (_pageIndex - 1));

                                DisableControls();
                            }
                        }
                        else
                        {
                            EnableControls();
                            _rowClicked = -1;
                            _rowSelected = false;
                            _locationSet = false;
                            foreach (DataGridViewRow dgvr in customDataGridViewTransactions.Rows)
                            {
                                if (dgvr.Index != e.RowIndex)
                                {
                                    dgvr.Cells[0].ReadOnly = false;
                                    dgvr.Cells[1].ReadOnly = false;
                                }
                            }
                            this.customDataGridViewMDSE.Top = 0;
                            ((DataGridViewImageCell)customDataGridViewTransactions.Rows[e.RowIndex].Cells[0]).Value = global::Common.Properties.Resources.plus_icon_small;
                            this.customDataGridViewTransactions.Rows[e.RowIndex].Height = 22;
                            this.customDataGridViewTransactions.Rows[e.RowIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            customDataGridViewTransactions.Refresh();
                            this.customDataGridViewMDSE.Visible = false;
                        }
                    }
                    catch (SystemException ex)
                    {
                        BasicExceptionHandler.Instance.AddException("Error occurred when viewing customer holds list", new ApplicationException(ex.Message));
                        this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                    }
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void dataGridViewTransactions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 2)
                {
                    e.FormattingApplied = true;
                    DateTime transactionDate = Utilities.GetDateTimeValue(e.Value.ToString(), DateTime.MaxValue);
                    e.Value = transactionDate.FormatDate();
                }

                if (e.ColumnIndex == 5)
                {
                    e.FormattingApplied = true;
                    int idx = e.Value.ToString().IndexOf("-", System.StringComparison.Ordinal);
                    if (idx + 1 == e.Value.ToString().Length)
                        e.Value = e.Value.ToString().Substring(0, idx);
                }

                if (e.ColumnIndex == 7)
                {
                    e.FormattingApplied = true;
                    e.Value = string.Format("{0:C}", e.Value);
                }
            }
        }

        private void dataGridViewMdse_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 3)
            {
                e.FormattingApplied = true;
                e.Value = string.Format("{0:C}", e.Value);
            }

            if (e.RowIndex > -1 && e.ColumnIndex == 2)
            {
                e.FormattingApplied = true;
                int idx = e.Value.ToString().IndexOf("-", System.StringComparison.Ordinal);
                if (idx + 1 == e.Value.ToString().Length)
                    e.Value = e.Value.ToString().Substring(0, idx);
            }
        }

        private void FindNumberOfPages(DataTable tranTable)
        {
            if (tranTable != null && tranTable.Rows.Count > 0)
            {
                _numberOfPages = tranTable.Rows.Count / maxRows;
                int reminder = 0;
                Math.DivRem(tranTable.Rows.Count, maxRows, out reminder);
                _numberOfPages = reminder > 0 ? _numberOfPages + 1 : _numberOfPages;
                _pageIndex = 1;
                showPage(1);
            }
        }

        private void showPage(int pageNumber)
        {
            int startRowToShow = (maxRows * (pageNumber - 1)) + 1;
            int lastRowToShow = (maxRows * pageNumber);
            foreach (DataGridViewRow dgvr in customDataGridViewTransactions.Rows)
            {
                if (dgvr.Index + 1 >= startRowToShow && dgvr.Index + 1 <= lastRowToShow)
                {
                    dgvr.Visible = true;
                }
                else
                {
                    if (customDataGridViewTransactions.CurrentRow != null)
                        if (dgvr.Index == customDataGridViewTransactions.CurrentRow.Index)
                            customDataGridViewTransactions.CurrentCell = null;
                    dgvr.Visible = false;
                }
            }

            this.labelPageNo.Text = string.Format("Page {0} of {1}", _pageIndex, _numberOfPages);
        }

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            _pageIndex = 1;
            showPage(_pageIndex);
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            if (_pageIndex - 1 <= 0)
                return;
            _pageIndex--;
            showPage(_pageIndex);
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (_pageIndex + 1 > _numberOfPages)
                return;
            _pageIndex++;
            showPage(_pageIndex);
        }

        private void lastButton_Click(object sender, EventArgs e)
        {
            _pageIndex = _numberOfPages;
            showPage(_pageIndex);
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            try
            {
                if (_numberOfSelections > 0)
                {
                    getSelectedTransactions();
                    string strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                    bool retVal = ServiceLoanProcedures.CheckCurrentTempStatus(ref _selectedTransactions, strUserId, ServiceTypes.CUSTHOLD);
                    if (_selectedTransactions.Count == 0)
                    {
                        return;
                    }
                    GlobalDataAccessor.Instance.DesktopSession.HoldsData = _selectedTransactions;

                    if (retVal)
                        this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                    else
                    {
                        throw new Exception("Error when trying to update temp status on selected loans");
                    }
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Error when trying to put hold on selected transactions", new ApplicationException(ex.Message));
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        private void getSelectedTransactions()
        {
            _selectedTransactions = new List<HoldData>();
            _selectedLoanNumbers = new List<int>();
            List<Item> transactionItems = new List<Item>();
            var dateMade = string.Empty;

            foreach (DataGridViewRow dgvr in customDataGridViewTransactions.Rows)
            {
                if (dgvr.Cells[1].Value != null && dgvr.Cells[1].Value.ToString() == "true")
                {
                    HoldData customerHoldData = new HoldData();
                    int selectedTicketNumber = Utilities.GetIntegerValue(dgvr.Cells[4].Value);
                    string origTicketNumber = dgvr.Cells[8].Value.ToString();
                    customerHoldData.StatusCode = dgvr.Cells[5].Value.ToString().Substring(0, dgvr.Cells[5].Value.ToString().IndexOf("-"));
                    customerHoldData.TicketNumber = selectedTicketNumber;
                    customerHoldData.RefType = "1";
                    dateMade = dgvr.Cells[2].Value.ToString();
                    customerHoldData.CustomerNumber = _customerNumber;
                    customerHoldData.OrgShopNumber = _storeNumber;
                    customerHoldData.HoldType = HoldTypes.CUSTHOLD.ToString();
                    customerHoldData.UserId = _userId;
                    customerHoldData.TransactionDate = Utilities.GetDateTimeValue(dateMade, DateTime.MaxValue);
                    customerHoldData.Amount = Utilities.GetDecimalValue(dgvr.Cells[7].Value, 0);
                    customerHoldData.PrevTicketNumber = Utilities.GetIntegerValue(dgvr.Cells[9].Value, 0);
                    customerHoldData.OrigTicketNumber = Utilities.GetIntegerValue(dgvr.Cells[8].Value);
                    customerHoldData.TempStatus = (StateStatus)Enum.Parse(typeof(StateStatus),
                                                                          Utilities.GetStringValue(dgvr.Cells[6].Value) != string.Empty
                                                                          ? Utilities.GetStringValue(dgvr.Cells[6].Value)
                                                                          : StateStatus.BLNK.ToString());
                    _selectedLoanNumbers.Add(Utilities.GetIntegerValue(selectedTicketNumber));
                    transactionItems = new List<Item>();
                    if (_merchandiseTable != null)
                    {
                        DataRow[] merchandiseRows = _merchandiseTable.Select(string.Format("{0}='{1}'", holdsmdsecursor.ICNDOC, origTicketNumber));
                        int merchandiseRowCount = merchandiseRows.Length;
                        for (int j = 0; j < merchandiseRowCount; j++)
                        {
                            Item newItem = new Item();
                            newItem.TicketDescription = merchandiseRows[j][holdsmdsecursor.ITEMDESCRIPTION].ToString();
                            newItem.Icn = merchandiseRows[j][holdsmdsecursor.ICN].ToString();
                            newItem.Location_Aisle = merchandiseRows[j][holdsmdsecursor.AISLE].ToString();
                            newItem.Location_Shelf = merchandiseRows[j][holdsmdsecursor.SHELF].ToString();
                            newItem.Location = merchandiseRows[j][holdsmdsecursor.OTHERLOCATION].ToString();
                            newItem.mDocNumber = Utilities.GetIntegerValue(origTicketNumber, 0);
                            transactionItems.Add(newItem);
                        }
                        customerHoldData.Items = transactionItems;
                    }
                    _selectedTransactions.Add(customerHoldData);
                }
            }

            GlobalDataAccessor.Instance.DesktopSession.HoldsData = _selectedTransactions;
        }

        private void buttonDeselectAll_Click(object sender, EventArgs e)
        {
            if (customDataGridViewTransactions.IsCurrentCellDirty)
            {
                customDataGridViewTransactions.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            foreach (DataGridViewRow dgvr in customDataGridViewTransactions.Rows)
            {
                ((DataGridViewCheckBoxCell)dgvr.Cells[1]).Selected = false;
                ((DataGridViewCheckBoxCell)dgvr.Cells[1]).Value = false;
            }
            customDataGridViewTransactions.CurrentCell = customDataGridViewTransactions.Rows[(maxRows * (_pageIndex - 1))].Cells[0];
            _numberOfSelections = 0;
        }

        private void dataGridViewTransactions_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > -1 && _rowSelected && e.ColumnIndex == 3 && !_locationSet)
            {
                //if the row is selected then we need to manipulate the location of
                //mdse datagridview   
                this.customDataGridViewMDSE.Location = new System.Drawing.Point(e.CellBounds.X, customDataGridViewTransactions.Top + 22 + (e.CellBounds.Y + (_rowClicked * 22)));
                _locationSet = true;
                e.Handled = true;
            }
            if (e.RowIndex > -1 && e.ColumnIndex == 5)
            {
                //if the reason code is PFIWAIT or PFIVERIFY
                //then change the color of the status string to red
                string pawnStatus = e.Value.ToString();
                bool pfiString = pawnStatus.Contains("PFI");
                var idx = pawnStatus.IndexOf("-", System.StringComparison.Ordinal);
                var s1 = string.Empty;
                var s2 = string.Empty;
                if (idx > 0)
                {
                    s1 = pawnStatus.Substring(0, idx);
                    s2 = idx + 1 != pawnStatus.Length ? pawnStatus.Substring(idx, pawnStatus.Length - idx) : "";
                }

                using (Brush gridBrush = new SolidBrush(Color.Red),
                    backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(Color.Gray))
                    {
                        // Erase the cell- key thing to do
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        // Draw the grid lines (only the right and bottom lines;
                        // DataGridView takes care of the others).
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                                            e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                                            e.CellBounds.Bottom - 1);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                                            e.CellBounds.Top, e.CellBounds.Right - 1,
                                            e.CellBounds.Bottom);
                
                        // Draw the text content of the cell

                        e.Graphics.DrawString(s1, e.CellStyle.Font,
                                              Brushes.Black, e.CellBounds.X + 30,
                                              e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        if (pfiString)
                        {
                            List<PairType<StateStatus, string>> allTempStatus = GlobalDataAccessor.Instance.DesktopSession.TempStatus;
                            string s = s2.Substring(1);
                            var tempStatusValue = from tStatus in allTempStatus
                                                  where tStatus.Left.ToString() == s
                                                  select tStatus;
                            if (tempStatusValue != null)
                                s2 = s2.Substring(0, 1) + tempStatusValue.First().Right;
                            e.Graphics.DrawString(s2, e.CellStyle.Font,
                                                  Brushes.Red, e.CellBounds.X + 40, e.CellBounds.Y + 2,
                                                  StringFormat.GenericDefault);
                        }
                        else
                        {
                            e.Graphics.DrawString(s2, e.CellStyle.Font,
                                                  Brushes.Black, e.CellBounds.X + 40, e.CellBounds.Y + 2,
                                                  StringFormat.GenericDefault);
                        }

                        e.Handled = true;
                    }
                }
            }
        }

        private void DisableControls()
        {
            this.customButtonDeselectAll.Enabled = false;
            this.buttonFirst.Enabled = false;
            this.buttonNext.Enabled = false;
            this.buttonPrevious.Enabled = false;
            this.buttonLast.Enabled = false;
            this.customButtonCancel.Enabled = false;
            this.customButtonPickSlipPrint.Enabled = false;
            this.customButtonContinue.Enabled = false;
            this.customDataGridViewTransactions.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.customDataGridViewTransactions.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.customDataGridViewTransactions.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.customDataGridViewTransactions.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.customDataGridViewTransactions.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void EnableControls()
        {
            this.customButtonDeselectAll.Enabled = true;
            this.buttonFirst.Enabled = true;
            this.buttonNext.Enabled = true;
            this.buttonPrevious.Enabled = true;
            this.buttonLast.Enabled = true;
            this.customButtonCancel.Enabled = true;
            this.customButtonPickSlipPrint.Enabled = true;
            this.customButtonContinue.Enabled = true;
            this.customDataGridViewTransactions.Columns[2].SortMode = DataGridViewColumnSortMode.Automatic;
            this.customDataGridViewTransactions.Columns[3].SortMode = DataGridViewColumnSortMode.Automatic;
            this.customDataGridViewTransactions.Columns[4].SortMode = DataGridViewColumnSortMode.Automatic;
            this.customDataGridViewTransactions.Columns[5].SortMode = DataGridViewColumnSortMode.Automatic;
            this.customDataGridViewTransactions.Columns[7].SortMode = DataGridViewColumnSortMode.Automatic;
        }

        private void dataGridViewMdse_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 0)
            {
                try
                {
                    string errorCode;
                    string errorMsg;
                    int storeId = Utilities.GetIntegerValue(_storeNumber, 0);
                    PawnLoan custPawnLoan;
                    if (customDataGridViewTransactions.CurrentRow != null)
                    {
                        int ticketNumber = Utilities.GetIntegerValue(customDataGridViewTransactions.CurrentRow.Cells[8].Value);
                        bool retValue = HoldsProcedures.GetPawnLoanHolds(storeId, ticketNumber, "0", out custPawnLoan, out errorCode, out errorMsg);

                        if (retValue)
                        {
                            var activePawnLoan = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan;
                            if (custPawnLoan != null && custPawnLoan.Items.Count > 0)
                            {
                                //Go to describe item read only view                
                                if (custPawnLoan.Items[e.RowIndex].CategoryMask < 1)
                                {
                                    // Need to populate pawnLoan from GetCat5
                                    int iCategoryMask = GlobalDataAccessor.Instance.DesktopSession.CategoryXML.GetCategoryMask(custPawnLoan.Items[e.RowIndex].CategoryCode);
                                    DescribedMerchandise dmPawnItem = new DescribedMerchandise(iCategoryMask);
                                    Item pawnItem = custPawnLoan.Items[e.RowIndex];
                                    Item.PawnItemMerge(ref pawnItem, dmPawnItem.SelectedPawnItem, true);
                                    pawnItem.CategoryMask = iCategoryMask;
                                    custPawnLoan.Items.RemoveAt(e.RowIndex);
                                    custPawnLoan.Items.Insert(e.RowIndex, pawnItem);
                                    // End GetCat5 populate
                                }

                                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan = custPawnLoan;
                                // Call Describe Item Page
                                DescribeItem myForm = new DescribeItem(GlobalDataAccessor.Instance.DesktopSession, CurrentContext.READ_ONLY, e.RowIndex)
                                {
                                    SelectedProKnowMatch = custPawnLoan.Items[e.RowIndex].SelectedProKnowMatch
                                };
                                myForm.ShowDialog(this);
                                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan = activePawnLoan;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException("Error trying to view details of selected item", new ApplicationException(ex.Message));
                    this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                }
            }
        }

        private void dataGridViewTransactions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 1)
            {
                DataGridViewCheckBoxCell oCell = (DataGridViewCheckBoxCell)customDataGridViewTransactions.Rows[e.RowIndex].Cells[1];

                if (oCell.Value != null && oCell.Value.ToString() == "true")
                {
                    //check to make sure that the record is not locked for PFI processing
                    var lockedProcess = string.Empty;
                    if (Commons.IsLockedStatus(customDataGridViewTransactions.Rows[e.RowIndex].Cells[6].Value.ToString(), ref lockedProcess))
                    {
                        MessageBox.Show(
                            string.Format("This record is locked by {0} process. Please try again later", lockedProcess));
                        oCell.Value = false;
                        return;
                    }
                    _numberOfSelections++;
                }
                else
                if (_numberOfSelections != 0)
                    _numberOfSelections--;
            }
        }

        private void dataGridViewTransactions_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (customDataGridViewTransactions.IsCurrentCellInEditMode)
                customDataGridViewTransactions.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void buttonPrintPickSlip_Click(object sender, EventArgs e)
        {
            try
            {
                if (_numberOfSelections > 0)
                {
                    getSelectedTransactions();
                    var printSlip = new PickSlipPrint();
                    printSlip.PickSlipType = "Customer Hold";
                    printSlip.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Error when trying to print pick slips", new ApplicationException(ex.Message));
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        private void customDataGridViewTransactions_Sorted(object sender, EventArgs e)
        {
            showPage(_pageIndex);
        }
    }
}
