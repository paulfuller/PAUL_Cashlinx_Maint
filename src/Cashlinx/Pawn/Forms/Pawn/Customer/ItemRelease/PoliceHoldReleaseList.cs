/**************************************************************************************************************
* CashlinxDesktop
* PoliceHoldReleaseList
* This form is used to show the list of transactions for a customer available for release
* Sreelatha Rengarajan 8/6/2009 Initial version
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CashlinxDesktop.DesktopForms.Pawn.Customer;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Pawn.Products.DescribeMerchandise;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Customer.ItemRelease
{
    public partial class PoliceHoldReleaseList : Form
    {
        public NavBox NavControlBox;
        private Form _ownerfrm;
        private BindingSource _bindingSource1;
        private BindingSource _bindingSource2;
        private DataTable _transactionTable;
        private DataTable _merchandiseTable;
        private string _errorCode = "";
        private string _errorMsg = "";
        private string _storeNumber = "";
        private string _customerNumber = "";
        private string _userId = "";
        private readonly string _statusCode = ProductStatus.IP.ToString();
        private int _pageIndex = 0;
        private int _numberOfPages = 0;
        private const int MaxRows = 7;
        private bool _rowSelected = false;
        private bool _locationSet = false;
        private int _numberOfSelections = 0;
        private bool _releaseDateValid = false;
        List<HoldData> _selectedTransactions = new List<HoldData>();
        List<int> _selectedLoanNumbers = new List<int>();
        List<string> mdseICN = new List<string>();
        private int _rowClicked = 0;
        List<string> restrictICN = new List<string>();
        int enabledRowsCount = 0;
        int selectedMdseRowCount = 0;
        int mdseRowCount = 0;
        private bool expandButtonClicked = false;

        public PoliceHoldReleaseList()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
        }

        private void PoliceHoldsList_Load(object sender, EventArgs e)
        {
            _ownerfrm = Owner;
            NavControlBox.Owner = this;
            _userId = GlobalDataAccessor.Instance.DesktopSession.UserName;
            var activeCustomer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            if (activeCustomer != null)
            {
                _storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                _customerNumber = activeCustomer.CustomerNumber;
            }
            else
            {
                MessageBox.Show(Commons.GetMessageString("MissingCustData"));
                NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
            //Create the transaction Table
            _transactionTable = new DataTable();
            _merchandiseTable = new DataTable();
            bool retVal = HoldsProcedures.ExecuteGetReleases(_storeNumber, _customerNumber, _statusCode, HoldData.POLICE_HOLD,
                                                             out _transactionTable, out _merchandiseTable, out _errorCode, out _errorMsg);

            if (retVal && _transactionTable != null)
            {
                DataColumn[] key = new DataColumn[2];
                key[0] = _transactionTable.Columns[holdstransactioncursor.TICKETNUMBER];
                key[1] = _transactionTable.Columns["time_made"];
                _transactionTable.PrimaryKey = key;
                
                _bindingSource1 = new BindingSource
                {
                    DataSource = _transactionTable
                };
                dataGridViewTransactions.AutoGenerateColumns = false;

                if (dataGridViewTransactions != null)
                {
                    dataGridViewTransactions.DataSource = _bindingSource1;
                    dataGridViewTransactions.Columns[2].DataPropertyName = holdstransactioncursor.TRANSACTIONDATE;
                    dataGridViewTransactions.Columns[3].DataPropertyName = holdstransactioncursor.TRANSACTIONTYPE;//"transactiontype";
                    dataGridViewTransactions.Columns[4].DataPropertyName = holdstransactioncursor.TICKETNUMBER;//"ticket_number";
                    dataGridViewTransactions.Columns[5].DataPropertyName = holdstransactioncursor.STATUS;//"pstatus";
                    dataGridViewTransactions.Columns[6].DataPropertyName = holdstransactioncursor.PFISTATE;//"temp_status";
                    //dataGridViewTransactions.Columns[7].DataPropertyName = holdstransactioncursor.RELEASEDATE;//"release_date";

                    if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId))
                    {
                        dataGridViewTransactions.Columns[8].DataPropertyName = holdstransactioncursor.CURRENTPRINCIPALAMOUNT; //"cur_amount in payment_detail table"; 
                    }
                    else
                    {
                        dataGridViewTransactions.Columns[8].DataPropertyName = holdstransactioncursor.LOANAMOUNT; //"org_amount";
                    }

                    dataGridViewTransactions.Columns[9].DataPropertyName = holdstransactioncursor.CREATIONDATE;//"creationdate";
                    dataGridViewTransactions.Columns[10].DataPropertyName = holdstransactioncursor.CREATEDBY;//"createdby";
                    dataGridViewTransactions.Columns[11].DataPropertyName = holdstransactioncursor.HOLDCOMMENT;//"hold_comment";
                    dataGridViewTransactions.Columns[12].DataPropertyName = holdstransactioncursor.ORIGINALTICKETNUMBER;//"org_ticket";

                    dataGridViewTransactions.Columns[1].ReadOnly = false;
                    //Set sort mode
                    dataGridViewTransactions.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewTransactions.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewTransactions.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dataGridViewMdse.AutoGenerateColumns = false;
                FindNumberOfPages(_transactionTable);
            }
            else
            {
                MessageBox.Show(Commons.GetMessageString("CustHoldsNoTransactions"));
                NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        private void dataGridViewTransactions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if ((e.ColumnIndex == 1 || e.ColumnIndex == 0) && !dataGridViewTransactions.Rows[e.RowIndex].Cells[0].ReadOnly)
                {
                    try
                    {
                        if (e.ColumnIndex == 0)
                            expandButtonClicked = true;
                        else
                            expandButtonClicked = false;
                        if (this.dataGridViewMdse.Visible == false)
                        {
                            selectedMdseRowCount = 0;
                            mdseRowCount = 0;
                            dataGridViewTransactions.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                            dataGridViewTransactions.Rows[e.RowIndex].Cells[7].Style.BackColor = Color.White;
                            dataGridViewTransactions.Rows[e.RowIndex].Cells[7].Style.ForeColor = Color.Black;
                            dataGridViewTransactions.CancelEdit();
                            ShowMerchandiseData(e.RowIndex);
                        }
                        else
                        {
                            HideMerchandiseData(e.RowIndex);
                        }
                    }
                    catch (SystemException ex)
                    {
                        BasicExceptionHandler.Instance.AddException("Error occurred when viewing police holds release list", new ApplicationException(ex.Message));
                        this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                    }
                }
            }
            else
            {
                if (_rowSelected)
                    return;
            }
        }

        private void ShowMerchandiseData(int rowIndex)
        {
            bool showMdseReleaseDate = false;
            var keys = new object[2];
            _bindingSource2 = new BindingSource();
            DataRowView drv = (DataRowView)(_bindingSource1.Current);
            keys[0] = drv.Row[holdstransactioncursor.TICKETNUMBER];
            keys[1] = drv.Row["time_made"];

            DataRow selectedRow = _transactionTable.Rows.Find(keys);

            //string selectedTicketNumber = selectedRow[holdstransactioncursor.TICKETNUMBER].ToString();
            string origTicketNumber = selectedRow[holdstransactioncursor.ORIGINALTICKETNUMBER].ToString();
            string selectedDocType = selectedRow[holdstransactioncursor.TRANSACTIONTYPE].ToString();
            var docType = string.Empty;
            docType = selectedDocType == "PAWN LOAN" ? "1" : "2";

            if (_merchandiseTable != null && _merchandiseTable.Rows.Count > 0)
            {
                _rowSelected = true;
                string filterExpression = string.Format("{0}='{1}' AND {2}='{3}'", holdsmdsecursor.ICNDOC, origTicketNumber, holdsmdsecursor.ICNDOCTYPE, docType);
                var merchandiseRows = _merchandiseTable.Select(filterExpression);

                if (merchandiseRows.Length == 0)
                    return;

                ((DataGridViewImageCell)dataGridViewTransactions.Rows[rowIndex].Cells[0]).Value = global::Common.Properties.Resources.minus_icon_small;
                foreach (DataGridViewRow dgvr in dataGridViewTransactions.Rows)
                {
                    if (dgvr.Index != rowIndex)
                    {
                        dgvr.Cells[0].ReadOnly = true;
                        dgvr.Cells[1].ReadOnly = true;
                    }
                    else
                    {
                        dgvr.Cells[1].ReadOnly = true;
                        if (dgvr.Cells[7].Value == null)
                            showMdseReleaseDate = true;
                    }
                }
                var newMdseTable = _merchandiseTable.Clone();
                foreach (var dr in merchandiseRows)
                {
                    mdseRowCount++;
                    newMdseTable.ImportRow(dr);
                }
                _bindingSource2.DataSource = newMdseTable;
                this.dataGridViewTransactions.Rows[rowIndex].Height += this.dataGridViewMdse.Height + 50;
                this.dataGridViewTransactions.Rows[rowIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                this.dataGridViewMdse.DataSource = _bindingSource2;
                this.dataGridViewMdse.Columns[1].DataPropertyName = holdsmdsecursor.ITEMDESCRIPTION;//"md_desc";
                this.dataGridViewMdse.Columns[2].DataPropertyName = holdsmdsecursor.ICN;//"icn";
                this.dataGridViewMdse.Columns[3].DataPropertyName = holdsmdsecursor.STATUS;//"mstatus";
                this.dataGridViewMdse.Columns[4].DataPropertyName = holdsmdsecursor.AMOUNT;//"org_amount";
                this.dataGridViewMdse.Columns[5].DataPropertyName = holdsmdsecursor.RELEASEDATE; //"release_date";
                this.dataGridViewMdse.Visible = true;
                dataGridViewMdse.Columns[5].Visible = false;
                bool showMessage = false;
                foreach (DataGridViewRow dgvr in dataGridViewMdse.Rows)
                {
                    var row = dgvr;
                    string merchandiseICN = row.Cells[2].Value.ToString();
                    int index = mdseICN.FindIndex(icnnumber => icnnumber == merchandiseICN);
                    if (index > -1)
                    {
                        if (showMdseReleaseDate)
                            dataGridViewMdse.Columns[5].Visible = true;
                        //Check if it is in the restrictICN list in which case the row should be read only
                        //and not checked
                        int idx = restrictICN.FindIndex(icnno => icnno == merchandiseICN);
                        if (idx > -1)
                        {
                            row.Cells[0].Value = "false";
                            showMessage = true;
                            row.DefaultCellStyle.BackColor = Color.Gray;
                            row.ReadOnly = true;
                        }
                        else
                        {
                            enabledRowsCount++;
                            row.DefaultCellStyle.BackColor = Color.White;
                            row.Cells[0].Value = "true";
                        }
                    }
                    else
                    {
                        string strValue = row.Cells[3].Value.ToString();
                        string mdseStatus = strValue.Substring(0, strValue.IndexOf("-", System.StringComparison.Ordinal));
                        string holdType = strValue.Substring(strValue.IndexOf("-", System.StringComparison.Ordinal) + 1);
                        if (showMdseReleaseDate)
                            dataGridViewMdse.Columns[5].Visible = true;
                        if (Commons.CanBeReleased(mdseStatus, holdType))
                        {
                            enabledRowsCount++;
                        }
                        else
                        {
                            restrictICN.Add(merchandiseICN);
                            row.Cells[0].Value = "false";
                            showMessage = true;
                            row.DefaultCellStyle.BackColor = Color.Gray;
                            row.ReadOnly = true;
                        }
                    }
                }
                if (enabledRowsCount > 0)
                {
                    if (!expandButtonClicked)
                    {
                        dataGridViewTransactions.Rows[rowIndex].Cells[1].Value = "true";

                        if (enabledRowsCount != dataGridViewMdse.Rows.Count)
                            dataGridViewTransactions.Rows[rowIndex].Cells[1].ReadOnly = true;
                        else
                            dataGridViewTransactions.Rows[rowIndex].Cells[1].ReadOnly = false;
                        int loanIndex = _selectedLoanNumbers.FindIndex(loanNumber => loanNumber == (Utilities.GetIntegerValue(dataGridViewTransactions.Rows[rowIndex].Cells[4].Value)));
                        if (loanIndex < 0)
                        {
                            _selectedLoanNumbers.Add(Utilities.GetIntegerValue(Utilities.GetIntegerValue(dataGridViewTransactions.Rows[rowIndex].Cells[4].Value)));
                            _numberOfSelections++;
                        }
                    }
                }

                labelMdseUnavailableMsg.Visible = showMessage;
                _rowClicked = rowIndex;
                if (_rowClicked >= MaxRows)
                    _rowClicked = _rowClicked - (MaxRows * (_pageIndex - 1));
                DisableControls();
            }
        }

        private void HideMerchandiseData(int rowIndex)
        {
            labelMdseUnavailableMsg.Visible = false;
           
            EnableControls();
            _rowClicked = -1;
            _rowSelected = false;
            _locationSet = false;
            foreach (DataGridViewRow dgvr in dataGridViewTransactions.Rows)
            {
                if (dgvr.Index != rowIndex)
                {
                    dgvr.Cells[0].ReadOnly = false;
                    dgvr.Cells[1].ReadOnly = false;
                }
            }
            this.dataGridViewMdse.Top = 0;
            ((DataGridViewImageCell)dataGridViewTransactions.Rows[rowIndex].Cells[0]).Value = global::Common.Properties.Resources.plus_icon_small;
            this.dataGridViewTransactions.Rows[rowIndex].ReadOnly = false;
            this.dataGridViewTransactions.Rows[rowIndex].Height = 22;
            this.dataGridViewTransactions.Rows[rowIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTransactions.Refresh();
            if (selectedMdseRowCount > 0)
            {
                if (dataGridViewTransactions.CurrentRow != null)
                {
                    _selectedLoanNumbers.Add(Utilities.GetIntegerValue(dataGridViewTransactions.CurrentRow.Cells[4].Value));
                    dataGridViewTransactions.CurrentRow.Cells[1].Value = "true";
                    if (selectedMdseRowCount != mdseRowCount)
                        dataGridViewTransactions.CurrentRow.Cells[1].ReadOnly = true;
                }
                _numberOfSelections++;
            }
            else
            {
                if (dataGridViewTransactions.CurrentRow != null)
                {
                    dataGridViewTransactions.CurrentRow.Cells[1].Value = "false";
                    int index = _selectedLoanNumbers.FindIndex(loanno => loanno == Utilities.GetIntegerValue(dataGridViewTransactions.CurrentRow.Cells[4].Value));
                    if (index > 0)
                        _selectedLoanNumbers.RemoveAt(index);
                }
            }
            this.dataGridViewMdse.Visible = false;
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
                    int idx = e.Value.ToString().LastIndexOf("-", System.StringComparison.Ordinal);
                    if (idx == 0)
                        e.Value = e.Value.ToString().Substring(0, idx);
                }

                if (e.ColumnIndex == 8)
                {
                    e.FormattingApplied = true;
                    e.Value = string.Format("{0:C}", e.Value);
                }
            }
        }

        private void dataGridViewMdse_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 4)
            {
                e.FormattingApplied = true;
                e.Value = string.Format("{0:C}", e.Value);
            }

            if (e.RowIndex > -1 && e.ColumnIndex == 3)
            {
                e.FormattingApplied = true;
                int idx = e.Value.ToString().LastIndexOf("-", System.StringComparison.Ordinal);
                if (idx + 1 == e.Value.ToString().Length)
                    e.Value = e.Value.ToString().Substring(0, idx);
            }
        }

        private void FindNumberOfPages(DataTable tranTable)
        {
            if (tranTable != null && tranTable.Rows.Count > 0)
            {
                _numberOfPages = tranTable.Rows.Count / MaxRows;
                int reminder = 0;
                Math.DivRem(tranTable.Rows.Count, MaxRows, out reminder);
                _numberOfPages = reminder > 0 ? _numberOfPages + 1 : _numberOfPages;
                _pageIndex = 1;
                ShowPage(1);
            }
        }

        private void ShowPage(int pageNumber)
        {
            int startRowToShow = (MaxRows * (pageNumber - 1)) + 1;
            int lastRowToShow = (MaxRows * pageNumber);
            foreach (DataGridViewRow dgvr in dataGridViewTransactions.Rows)
            {
                if (dgvr.Index + 1 >= startRowToShow && dgvr.Index + 1 <= lastRowToShow)
                {
                    dgvr.Visible = true;
                }
                else
                {
                    if (dataGridViewTransactions.CurrentRow != null)
                        if (dgvr.Index == dataGridViewTransactions.CurrentRow.Index)
                            dataGridViewTransactions.CurrentCell = null;
                    dgvr.Visible = false;
                }
            }
            labelPageNo.Text = "Page " + _pageIndex + " of " + _numberOfPages;
        }

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            _pageIndex = 1;
            ShowPage(_pageIndex);
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            if (_pageIndex - 1 <= 0)
                return;
            _pageIndex--;
            ShowPage(_pageIndex);
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (_pageIndex + 1 > _numberOfPages)
                return;
            _pageIndex++;
            ShowPage(_pageIndex);
        }

        private void lastButton_Click(object sender, EventArgs e)
        {
            _pageIndex = _numberOfPages;
            ShowPage(_pageIndex);
        }

        private void GetSelectedTransactions()
        {
            _selectedTransactions = new List<HoldData>();
            var transactionItems = new List<Item>();
            var dateMade = string.Empty;

            var amount = 0;
            foreach (DataGridViewRow dgvr in dataGridViewMdse.Rows)
            {
                amount += Utilities.GetIntegerValue(dgvr.Cells[4].Value, 0);
            }

            foreach (DataGridViewRow dgvr in dataGridViewTransactions.Rows)
            {
                int selectedTicketNumber = Utilities.GetIntegerValue(dgvr.Cells[4].Value);
                int loanIndex = _selectedLoanNumbers.FindIndex(loanNumber => loanNumber == selectedTicketNumber);
                if (loanIndex > -1)
                {
                    //check to make sure that the transaction is not locked for PFI processing
                    var lockedProcess = string.Empty;
                    if (Commons.IsLockedStatus(dgvr.Cells[6].Value.ToString(), ref lockedProcess))
                    {
                        MessageBox.Show(
                            dgvr.Cells[1].Value.ToString() + " is locked by " + lockedProcess + " process. Please try again later");
                        dgvr.Cells[1].Value = "false";
                        return;
                    }

                    string origTicketNumber = dgvr.Cells[12].Value.ToString();
                    dateMade = dgvr.Cells[2].Value.ToString();
                    var relDate = string.Empty;
                    if (dgvr.Cells[7].Value != null)
                        relDate = dgvr.Cells[7].Value.ToString();

                    string tranType = dgvr.Cells[3].Value.ToString();

                    HoldData policeHoldData = new HoldData
                    {
                        StatusCode =
                        dgvr.Cells[5].Value.ToString().
                        Substring(dgvr.Cells[5].Value.ToString().IndexOf("-") + 1),
                        TicketNumber = selectedTicketNumber,
                        RefType = tranType == "PAWN LOAN" ? "1" : "2",
                        CustomerNumber = _customerNumber,
                        OrgShopNumber = _storeNumber,
                        TransactionDate = Utilities.GetDateTimeValue(dateMade, DateTime.MaxValue),
                        CurrentPrincipalAmount = Utilities.GetDecimalValue(dgvr.Cells[8].Value, 0),
                        Amount = amount,
                        UserId = GlobalDataAccessor.Instance.DesktopSession.
                        UserName, ReleaseDate = Utilities.GetDateTimeValue(relDate),
                        HoldType = HoldTypes.POLICEHOLD.ToString()
                    };

                    transactionItems = new List<Item>();
                    if (_merchandiseTable != null)
                    {
                        DataRow[] merchandiseRows = _merchandiseTable.Select(holdsmdsecursor.ICNDOC + "='" + origTicketNumber + "'");
                        int merchandiseRowCount = merchandiseRows.Length;
                        for (int j = 0; j < merchandiseRowCount; j++)
                        {
                            string merchandiseICN = merchandiseRows[j][holdsmdsecursor.ICN].ToString();
                            int idx = mdseICN.FindIndex(mICN => mICN == merchandiseICN);
                            if (idx > -1)
                            {
                                Item newItem = new Item();
                                newItem.TicketDescription = merchandiseRows[j][holdsmdsecursor.ITEMDESCRIPTION].ToString();
                                newItem.Icn = merchandiseRows[j][holdsmdsecursor.ICN].ToString();
                                newItem.Location_Aisle = merchandiseRows[j][holdsmdsecursor.AISLE].ToString();
                                newItem.Location_Shelf = merchandiseRows[j][holdsmdsecursor.SHELF].ToString();
                                newItem.Location = merchandiseRows[j][holdsmdsecursor.OTHERLOCATION].ToString();
                                newItem.GunNumber = Utilities.GetLongValue(merchandiseRows[j][holdsmdsecursor.GUNNUMBER], 0);
                                string locStatus = merchandiseRows[j][holdsmdsecursor.STATUS].ToString();
                                if (locStatus.StartsWith("PFI"))
                                    newItem.ItemStatus = ProductStatus.PFI;
                                else
                                    newItem.ItemStatus = ProductStatus.ALL;
                                newItem.IsJewelry = Utilities.IsJewelry(Convert.ToInt32(merchandiseRows[j][holdsmdsecursor.CATCODE]));  
                                transactionItems.Add(newItem);
                            }
                        }
                        policeHoldData.Items = transactionItems;
                    }
                    _selectedTransactions.Add(policeHoldData);
                }
            }

            GlobalDataAccessor.Instance.DesktopSession.HoldsData = _selectedTransactions;
        }

        private void buttonDeselectAll_Click(object sender, EventArgs e)
        {
            if (dataGridViewTransactions.IsCurrentCellDirty)
            {
                dataGridViewTransactions.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            foreach (DataGridViewRow dgvr in dataGridViewTransactions.Rows)
            {
                ((DataGridViewCheckBoxCell)dgvr.Cells[1]).Selected = false;
                ((DataGridViewCheckBoxCell)dgvr.Cells[1]).Value = false;
            }
            dataGridViewTransactions.CurrentCell = dataGridViewTransactions.Rows[0].Cells[0];
            mdseICN = new List<string>();
            _numberOfSelections = 0;
            _selectedLoanNumbers.Clear();
            mdseRowCount = 0;
            enabledRowsCount = 0;
            selectedMdseRowCount = 0;
        }

        private void dataGridViewTransactions_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > -1 && _rowSelected && e.ColumnIndex == 3 && !_locationSet)
            {
                //if the row is selected then we need to manipulate the location of
                //mdse datagridview
                this.dataGridViewMdse.Location = new System.Drawing.Point(e.CellBounds.X, dataGridViewTransactions.Top + 35 + (e.CellBounds.Y + (_rowClicked * 22)));
                if (labelMdseUnavailableMsg.Visible)
                    labelMdseUnavailableMsg.Location = new System.Drawing.Point(dataGridViewMdse.Location.X, dataGridViewMdse.Location.Y - 19);

                _locationSet = true;
                e.Handled = true;
            }

            if (e.RowIndex > -1 && e.ColumnIndex == 5)
            {
                //if the reason code is PFIWAIT or PFIVERIFY
                //then change the color of the status string to red
                string pawnStatus = e.Value.ToString();
                int idx = pawnStatus.IndexOf("-", System.StringComparison.Ordinal);
                var s1 = string.Empty;
                var s2 = string.Empty;
                if (idx > 0)
                {
                    s1 = pawnStatus.Substring(0, idx + 1);
                    s2 = pawnStatus.Substring(idx + 1);
                }

                if (pawnStatus.Contains("PFI"))
                {
                    using (
                        Brush gridBrush = new SolidBrush(Color.Red),
                        backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                    {
                        using (Pen gridLinePen = new Pen(Color.Black))
                        {
                            // Erase the cell- key thing to do
                            //otherwise the cell content looks weird
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                            // Draw the grid lines (only the right and bottom lines;
                            // DataGridView takes care of the others).
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                                                e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                                                e.CellBounds.Bottom - 1);
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                                                e.CellBounds.Top, e.CellBounds.Right - 1,
                                                e.CellBounds.Bottom);

                            // Draw the text content of the cell, ignoring alignment.
                            e.Graphics.DrawString(s1, e.CellStyle.Font,
                                                  Brushes.Black, e.CellBounds.X,
                                                  e.CellBounds.Y + 5, StringFormat.GenericDefault);
                            e.Graphics.DrawString(s2, e.CellStyle.Font,
                                                  Brushes.Red, e.CellBounds.X + 65, e.CellBounds.Y + 5, StringFormat.GenericDefault);

                            e.Handled = true;
                        }
                    }
                }
                else
                {
                    using (
                        Brush gridBrush = new SolidBrush(Color.Red),
                        backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                    {
                        using (Pen gridLinePen = new Pen(Color.Black))
                        {
                            // Erase the cell- key thing to do
                            //otherwise the cell content looks weird
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                            // Draw the grid lines (only the right and bottom lines;
                            // DataGridView takes care of the others).
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                                                e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                                                e.CellBounds.Bottom - 1);
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                                                e.CellBounds.Top, e.CellBounds.Right - 1,
                                                e.CellBounds.Bottom);

                            // Draw the text content of the cell, ignoring alignment.
                            e.Graphics.DrawString(s1, e.CellStyle.Font,
                                                  Brushes.Black, e.CellBounds.X,
                                                  e.CellBounds.Y + 5, StringFormat.GenericDefault);
                            e.Graphics.DrawString(s2, e.CellStyle.Font,
                                                  Brushes.Black, e.CellBounds.X + 65, e.CellBounds.Y + 5, StringFormat.GenericDefault);

                            e.Handled = true;
                        }
                    }
                }
            }
            else if (e.RowIndex > -1 && e.ColumnIndex == 7)
            {
                //If the release date was updated do not change it
                if (_releaseDateValid)
                    return;
                //If the cell being painted is the release date make sure that the 
                //release date is same for all the mdse associated to the loan
                //if not do not show the release date at the loan level
                //DateTime loanRelDate = Utilities.GetDateTimeValue(e.Value.ToString());
                bool hideRelDate = false;
                DateTime relDate = DateTime.MaxValue;
                int selectedTicketNumber = Utilities.GetIntegerValue(dataGridViewTransactions.Rows[e.RowIndex].Cells[12].Value);
                if (_merchandiseTable != null)
                {
                    var merchandiseRows = _merchandiseTable.Select(string.Format("{0}='{1}'", holdsmdsecursor.ICNDOC, selectedTicketNumber));
                    int merchandiseRowCount = merchandiseRows.Length;
                    relDate = Utilities.GetDateTimeValue(merchandiseRows[0][holdsmdsecursor.RELEASEDATE].
                                                         ToString());
                    for (int j = 0; j < merchandiseRowCount; j++)
                    {
                        var mdseRelDate = Utilities.GetDateTimeValue(merchandiseRows[j][holdsmdsecursor.RELEASEDATE].
                                                                          ToString());
                        if (mdseRelDate != relDate)
                        {
                            hideRelDate = true;
                            break;
                        }
                    }
                }
                using (
                    Brush gridBrush = new SolidBrush(Color.Black),
                    backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    if (hideRelDate)
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                    else
                        dataGridViewTransactions.Rows[e.RowIndex].Cells[7].Value = relDate.FormatDate();
                }
            }
            else
            {
                //For all the other columns just paint
                e.Paint(e.ClipBounds, e.PaintParts);
                e.Handled = true;
            }
        }

        private void DisableControls()
        {
            this.buttonDeselectAll.Enabled = false;
            this.buttonFirst.Enabled = false;
            this.buttonNext.Enabled = false;
            this.buttonPrevious.Enabled = false;
            this.buttonLast.Enabled = false;
            this.buttonCancel.Enabled = false;
            this.buttonUpdateReleaseDate.Enabled = false;
            this.buttonReleaseHold.Enabled = false;
            this.buttonClaimantRelease.Enabled = false;
            this.dataGridViewTransactions.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTransactions.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTransactions.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTransactions.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTransactions.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTransactions.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void EnableControls()
        {
            this.buttonDeselectAll.Enabled = true;
            this.buttonFirst.Enabled = true;
            this.buttonNext.Enabled = true;
            this.buttonPrevious.Enabled = true;
            this.buttonLast.Enabled = true;
            this.buttonCancel.Enabled = true;
            this.buttonUpdateReleaseDate.Enabled = true;
            this.buttonReleaseHold.Enabled = true;
            this.buttonClaimantRelease.Enabled = true;
            this.dataGridViewTransactions.Columns[2].SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTransactions.Columns[3].SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTransactions.Columns[4].SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTransactions.Columns[5].SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTransactions.Columns[7].SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTransactions.Columns[8].SortMode = DataGridViewColumnSortMode.Automatic;
        }

        private void dataGridViewMdse_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 1)
            {
                try
                {
                    string errorCode;
                    string errorMsg;
                    int storeId = Utilities.GetIntegerValue(_storeNumber, 0);
                    PawnLoan custPawnLoan;
                    if (dataGridViewTransactions.CurrentRow != null)
                    {
                        int ticketNumber = Utilities.GetIntegerValue(dataGridViewTransactions.CurrentRow.Cells[12].Value);
                        //Send the control back if the ticket number is not valid
                        if (!(ticketNumber > 0))
                            return;
                        bool retValue = HoldsProcedures.GetPawnLoanHolds(storeId, ticketNumber, "0", out custPawnLoan, out errorCode, out errorMsg);

                        if (retValue)
                        {
                            var activePawnLoan = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan;
                            if (custPawnLoan != null)
                            {
                                //Go to describe item read only view                
                                if (custPawnLoan.Items[e.RowIndex].CategoryMask < 1)
                                {
                                    // Need to populate pawnLoan from GetCat5
                                    int iCategoryMask = GlobalDataAccessor.Instance.DesktopSession.CategoryXML.GetCategoryMask(custPawnLoan.Items[e.RowIndex].CategoryCode);
                                    var dmPawnItem = new DescribedMerchandise(iCategoryMask);
                                    var pawnItem = custPawnLoan.Items[e.RowIndex];
                                    Item.PawnItemMerge(ref pawnItem, dmPawnItem.SelectedPawnItem, true);
                                    pawnItem.CategoryMask = iCategoryMask;
                                    custPawnLoan.Items.RemoveAt(e.RowIndex);
                                    custPawnLoan.Items.Insert(e.RowIndex, pawnItem);
                                    // End GetCat5 populate
                                }

                                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan = custPawnLoan;
                                // Call Describe Item Page
                                var myForm = new DescribeItem(GlobalDataAccessor.Instance.DesktopSession, CurrentContext.READ_ONLY, e.RowIndex)
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
                enabledRowsCount = 0;
                if (dataGridViewMdse.Visible)
                    return;
                bool allMdsePFI = true;
                DataGridViewCheckBoxCell oCell = (DataGridViewCheckBoxCell)dataGridViewTransactions.Rows[e.RowIndex].Cells[1];
                string tranStatus = dataGridViewTransactions.Rows[e.RowIndex].Cells[5].Value.ToString();
                if (oCell.Value != null && oCell.Value.ToString() == "true")
                {
                    //Set cell style for eligible for release
                    //If the checkbox is checked, set the readonly to false and change the backcolor to blue
                    //if not set the readonly to true and change the backcolor to white
                    if (dataGridViewTransactions.Rows[e.RowIndex].Cells[7].ReadOnly && dataGridViewTransactions.Rows[e.RowIndex].Cells[7].Value != null)
                    {
                        dataGridViewTransactions.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                        dataGridViewTransactions.Rows[e.RowIndex].Cells[7].Style.BackColor = Color.Blue;
                        dataGridViewTransactions.Rows[e.RowIndex].Cells[7].Style.ForeColor = Color.White;
                    }
                    //if the transaction is already added to the list no need for the mdse addition
                    int loanIndex = _selectedLoanNumbers.FindIndex(loanNumber => loanNumber == (Utilities.GetIntegerValue(dataGridViewTransactions.Rows[e.RowIndex].Cells[4].Value)));
                    if (loanIndex < 0)
                    {
                        if (_merchandiseTable != null)
                        {
                            DataRow[] merchandiseRows = _merchandiseTable.Select(holdsmdsecursor.ICNDOC + "='" + dataGridViewTransactions.Rows[e.RowIndex].Cells[12].Value.ToString() + "'");

                            enabledRowsCount = 0;
                            foreach (DataRow dgvr in merchandiseRows)
                            {
                                DataRow row = dgvr;
                                string merchandiseICN = row[1].ToString();

                                int index = mdseICN.FindIndex(icnnumber => icnnumber == merchandiseICN);
                                if (index > -1)
                                {
                                    //Check if it is in the restrictICN list in which case the row should be read only
                                    //and not checked
                                    int idx = -1;
                                    if (restrictICN.Count > 0)
                                        idx = restrictICN.FindIndex(icnno => icnno == merchandiseICN);
                                    if (idx < 0)
                                    {
                                        enabledRowsCount++;
                                    }
                                    if (enabledRowsCount > 0)
                                    {
                                        _numberOfSelections++;
                                        _selectedLoanNumbers.Add(Utilities.GetIntegerValue(dataGridViewTransactions.Rows[e.RowIndex].Cells[4].Value));
                                    }
                                }
                                else
                                {
                                    //The mdse is not in the selected list
                                    var strValue = row[2].ToString();
                                    var mdseStatus = strValue.Substring(0, strValue.IndexOf("-", System.StringComparison.Ordinal));

                                    if (Commons.CanBePutOnPoliceHold(mdseStatus, ""))
                                    {
                                        enabledRowsCount++;
                                        mdseICN.Add(merchandiseICN);
                                    }
                                    else
                                    {
                                        restrictICN.Add(merchandiseICN);
                                        if (tranStatus.Contains("PFI"))
                                            allMdsePFI = false;
                                    }
                                }
                            }
                            if (!allMdsePFI && restrictICN.Count > 0)
                                ShowMerchandiseData(e.RowIndex);
                            if (allMdsePFI && enabledRowsCount > 0)
                            {
                                _selectedLoanNumbers.Add(Utilities.GetIntegerValue(dataGridViewTransactions.Rows[e.RowIndex].Cells[4].Value));
                                _numberOfSelections++;
                            }
                        }
                    }
                }
                else
                {
                    dataGridViewTransactions.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                    dataGridViewTransactions.Rows[e.RowIndex].Cells[7].Style.BackColor = Color.White;
                    dataGridViewTransactions.Rows[e.RowIndex].Cells[7].Style.ForeColor = Color.Black;

                    if (_merchandiseTable != null)
                    {
                        DataRow[] merchandiseRows = _merchandiseTable.Select(holdsmdsecursor.ICNDOC + "='" + dataGridViewTransactions.Rows[e.RowIndex].Cells[12].Value.ToString() + "'");
                        int merchandiseRowCount = merchandiseRows.Length;
                        for (int j = 0; j < merchandiseRowCount; j++)
                        {
                            string merchandiseICN = merchandiseRows[j][holdsmdsecursor.ICN].ToString();
                            int idx = mdseICN.FindIndex(mICN => mICN == merchandiseICN);
                            //Remove only if its there already
                            if (idx >= 0)
                            {
                                mdseICN.Remove(merchandiseICN);
                                if (selectedMdseRowCount > 0)
                                    selectedMdseRowCount--;
                                if (enabledRowsCount > 0)
                                    enabledRowsCount = 0;
                            }
                        }
                    }
                    if (_numberOfSelections > 0)
                    {
                        _numberOfSelections--;
                        int loanIndex = _selectedLoanNumbers.FindIndex(loanNumber => loanNumber == (Utilities.GetIntegerValue(dataGridViewTransactions.Rows[e.RowIndex].Cells[4].Value)));
                        if (loanIndex > -1)
                            _selectedLoanNumbers.RemoveAt(loanIndex);
                    }
                }
            }
        }

        private bool checkMdseReleaseDate(int rowIdx)
        {
            DateTime dtRelease;
            string releaseDate = ((string)dataGridViewMdse.Rows[rowIdx].Cells[5].EditedFormattedValue).ToString();

            try
            {
                dtRelease = Convert.ToDateTime(releaseDate);
            }
            catch (Exception)
            {
                return false;
            }
            
            if (dtRelease < ShopDateTime.Instance.ShopDate)
            {
                return false;
            }
            dataGridViewMdse.Rows[rowIdx].Cells[5].Value = dtRelease.FormatDate();
            return true;
        }

        private bool checkReleaseDate(int rowIdx)
        {
            DateTime dtRelease;
            string releaseDate = ((string)dataGridViewTransactions.Rows[rowIdx].Cells[7].EditedFormattedValue).ToString();

            try
            {
                dtRelease = Convert.ToDateTime(releaseDate);
            }
            catch (Exception)
            {
                return false;
            }
            if (dtRelease < ShopDateTime.Instance.ShopDate)
            {
                return false;
            }
            dataGridViewTransactions.Rows[rowIdx].Cells[7].Value = dtRelease.FormatDate();
            return true;
        }

        private void dataGridViewTransactions_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewTransactions.IsCurrentCellInEditMode && dataGridViewTransactions.CurrentCell.ColumnIndex == 1)
                dataGridViewTransactions.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void buttonUpdateReleaseDate_Click(object sender, EventArgs e)
        {
            //Handle mdse release date update if the button was clicked when the mdse detail view is visible
            if (dataGridViewMdse.Visible)
            {
                DataGridViewRow currentTransaction = dataGridViewTransactions.CurrentRow;
                if (currentTransaction != null)
                {
                    List<DateTime> mdseReleaseDates = new List<DateTime>();
                    _selectedTransactions = new List<HoldData>();
                    //Build the hold data of all the mdse whose release dates need to be updated
                    //in the current loan context
                    string tranType = currentTransaction.Cells[3].Value.ToString();
                    HoldData policeHoldData = new HoldData
                    {
                        StatusCode =
                        currentTransaction.Cells[5].Value.ToString().Substring(currentTransaction.Cells[5].Value
                                                                               .ToString().
                                                                               IndexOf("-") + 1),
                        TicketNumber = Utilities.GetIntegerValue(currentTransaction.Cells[4].Value),
                        RefType = tranType == "PAWN LOAN" ?"1": "2",
                        CustomerNumber = _customerNumber,
                        OrgShopNumber = _storeNumber,
                        UserId = GlobalDataAccessor.Instance.DesktopSession.UserName,
                        HoldType = HoldTypes.POLICEHOLD.ToString()
                    };
                    List<Item> transactionItems = new List<Item>();
                    foreach (DataGridViewRow dgvr in dataGridViewMdse.Rows)
                    {
                        if (dgvr.Cells[0].Value != null &&
                            dgvr.Cells[0].Value.ToString() == "true" && !dgvr.ReadOnly)
                        {
                            string merchandiseICN = dgvr.Cells[2].Value.ToString();
                            int idx = mdseICN.FindIndex(mICN => mICN == merchandiseICN);
                            if (idx > -1)
                            {
                                Item newItem = new Item();
                                newItem.Icn = merchandiseICN;
                                mdseReleaseDates.Add(Utilities.GetDateTimeValue(dgvr.Cells[5].Value.ToString()));
                                transactionItems.Add(newItem);
                                //remove the item from mdseicn
                                mdseICN.RemoveAt(idx);
                                selectedMdseRowCount--;
                            }
                        }
                    }
                    if (transactionItems.Count > 0)
                    {
                        policeHoldData.Items = transactionItems;

                        bool returnValue = false;
                        DialogResult dgr = DialogResult.Retry;
                        do
                        {
                            returnValue = HoldsProcedures.UpdateReleaseDateOnMdse(policeHoldData, mdseReleaseDates);
                            if (returnValue)
                            {
                                labelReleaseDateUpdate.Visible = true;
                                //When the release dates are updated the cancel button changes to close
                                //to not give the user the impression that when they click on this button
                                //they can cancel the update release date operation that has already been persisted
                                buttonCancel.Text = "Close";

                                break;
                            }
                            dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                        }while (dgr == DialogResult.Retry);
                    }
                }
            }
            else
            {
                if (_numberOfSelections > 0)
                {
                    GetSelectedTransactions();
                    bool returnValue = false;
                    DialogResult dgr = DialogResult.Retry;
                    do
                    {
                        returnValue = HoldsProcedures.UpdateReleaseDateOnHolds(_selectedTransactions);
                        if (returnValue)
                        {
                            labelReleaseDateUpdate.Visible = true;
                            for (int i = 0; i < dataGridViewTransactions.Rows.Count; i++)
                            {
                                if (dataGridViewTransactions.Rows[i].Cells[7].Value != null)
                                {
                                    dataGridViewTransactions.Rows[i].Cells[7].ReadOnly = true;
                                    dataGridViewTransactions.Rows[i].Cells[7].Style.BackColor = Color.White;
                                    dataGridViewTransactions.Rows[i].Cells[7].Style.ForeColor = Color.Black;
                                }
                            }
                            //When the release dates are updated the cancel button changes to close
                            //to not give the user the impression that when they click on this button
                            //they can cancel the update release date operation that has already been persisted
                            buttonCancel.Text = "Close";
                            break;
                        }
                        dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                    }while (dgr == DialogResult.Retry);
                }
                else
                {
                    MessageBox.Show("No transactions selected for update.");
                    return;
                }
            }
        }

        private void buttonReleaseHold_Click(object sender, EventArgs e)
        {
            if (_numberOfSelections > 0)
            {
                GetSelectedTransactions();
                bool containsJewelry = false;
                foreach (HoldData holdData in GlobalDataAccessor.Instance.DesktopSession.HoldsData)
                {
                    Item itemi = holdData.Items.Find(item => item.IsJewelry);
                    if (itemi != null)
                    {
                        if (itemi.ItemStatus == ProductStatus.PFI)
                        {
                            containsJewelry = true;
                            break;
                        }
                    }
                }
                if (containsJewelry)
                {
                    MultiJewelryCaseNumber multiJcase = new MultiJewelryCaseNumber();
                    multiJcase.ShowDialog();
                }
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "HoldReleaseInfo";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                MessageBox.Show("No transactions selected for update.");
                return;
            }
        }

        private void dataGridViewTransactions_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 7)
            {
                _releaseDateValid = checkReleaseDate(e.RowIndex);
                if (!_releaseDateValid)
                {
                    MessageBox.Show(Commons.GetMessageString("InvalidDate"));
                    dataGridViewTransactions.CancelEdit();
                }
                else
                {
                    dataGridViewTransactions.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void dataGridViewTransactions_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void buttonClaimantRelease_Click(object sender, EventArgs e)
        {
            if (_numberOfSelections > 0)
            {
                GetSelectedTransactions();
                foreach (HoldData polHoldData in GlobalDataAccessor.Instance.DesktopSession.HoldsData)
                    polHoldData.TempStatus = StateStatus.RTC;
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "ReleaseToClaimant";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                MessageBox.Show("No transactions selected for releasing to claimant.");
                return;
            }
        }

        private void dataGridViewMdse_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 0 && !dataGridViewMdse.Rows[e.RowIndex].ReadOnly)
            {
                DataGridViewCheckBoxCell oCell = (DataGridViewCheckBoxCell)dataGridViewMdse.Rows[e.RowIndex].Cells[0];
                var itemICN = string.Empty;
                if (oCell.Value != null && oCell.Value.ToString() == "true")
                {
                    //If the release date is visible at the mdse level,
                    //enable the field to be edited
                    if (dataGridViewMdse.Rows[e.RowIndex].Cells[5].Visible)
                    {
                        dataGridViewMdse.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                        dataGridViewMdse.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.Blue;
                        dataGridViewMdse.Rows[e.RowIndex].Cells[5].Style.ForeColor = Color.White;
                        buttonUpdateReleaseDate.Enabled = true;
                    }

                    itemICN = dataGridViewMdse.Rows[e.RowIndex].Cells["ICN"].Value.ToString();
                    int index = -1;
                    if (mdseICN.Count > 0 && itemICN != string.Empty)
                    {
                        index = mdseICN.FindIndex(icnnumber => icnnumber == itemICN);
                    }
                    //Add it to the list only if it is not already selected
                    if (index < 0)
                    {
                        mdseICN.Add(itemICN);
                    }

                    selectedMdseRowCount++;
                }
                else
                {
                    itemICN = dataGridViewMdse.Rows[e.RowIndex].Cells["ICN"].Value.ToString();
                    //Remove the item from the selected item list
                    int index = -1;
                    if (mdseICN.Count > 0 && itemICN != string.Empty)
                    {
                        index = mdseICN.FindIndex(icnnumber => icnnumber == itemICN);
                    }
                    if (index > -1)
                        mdseICN.RemoveAt(index);
                    if (selectedMdseRowCount > 0)
                    {
                        selectedMdseRowCount--;
                    }
                    if (selectedMdseRowCount == 0)
                        buttonUpdateReleaseDate.Enabled = false;

                    if (dataGridViewMdse.Rows[e.RowIndex].Cells[5].Visible)
                    {
                        dataGridViewMdse.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                        dataGridViewMdse.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.White;
                        dataGridViewMdse.Rows[e.RowIndex].Cells[5].Style.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void dataGridViewMdse_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewMdse.IsCurrentCellInEditMode)
                dataGridViewMdse.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridViewTransactions_Sorted(object sender, EventArgs e)
        {
            ShowPage(_pageIndex);
        }

        private void buttonMdseUpdateReleaseDate_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentTransaction = dataGridViewTransactions.CurrentRow;
            if (currentTransaction != null)
            {
                List<DateTime> mdseReleaseDates = new List<DateTime>();
                _selectedTransactions = new List<HoldData>();
                //Build the hold data of all the mdse whose release dates need to be updated
                //in the current loan context
                HoldData policeHoldData = new HoldData
                {
                    StatusCode =
                    currentTransaction.Cells[5].Value.ToString().Substring(currentTransaction.Cells[5].Value
                                                                           .ToString().
                                                                           IndexOf("-") + 1),
                    TicketNumber = Utilities.GetIntegerValue(currentTransaction.Cells[4].Value),
                    RefType = "1",
                    CustomerNumber = _customerNumber,
                    OrgShopNumber = _storeNumber,
                    UserId = GlobalDataAccessor.Instance.DesktopSession.UserName,
                    HoldType = HoldTypes.POLICEHOLD.ToString()
                };
                List<Item> transactionItems = new List<Item>();
                foreach (DataGridViewRow dgvr in dataGridViewMdse.Rows)
                {
                    if (dgvr.Cells[0].Value != null && 
                        dgvr.Cells[0].Value.ToString() == "true" && !dgvr.ReadOnly)
                    {
                        string merchandiseICN = dgvr.Cells[2].Value.ToString();
                        int idx = mdseICN.FindIndex(mICN => mICN == merchandiseICN);
                        if (idx > -1)
                        {
                            Item newItem = new Item();
                            newItem.Icn = merchandiseICN;
                            mdseReleaseDates.Add(Utilities.GetDateTimeValue(dgvr.Cells[5].Value.ToString()));
                            transactionItems.Add(newItem);
                            //remove the item from mdseicn
                            mdseICN.RemoveAt(idx);
                            selectedMdseRowCount--;
                        }
                    }
                }
                if (transactionItems.Count > 0)
                {
                    policeHoldData.Items = transactionItems;

                    bool returnValue = false;
                    DialogResult dgr = DialogResult.Retry;
                    do
                    {
                        returnValue = HoldsProcedures.UpdateReleaseDateOnMdse(policeHoldData, mdseReleaseDates);
                        if (returnValue)
                        {
                            labelReleaseDateUpdate.Visible = true;
                            //When the release dates are updated the cancel button changes to close
                            //to not give the user the impression that when they click on this button
                            //they can cancel the update release date operation that has already been persisted
                            buttonCancel.Text = "Close";
                            
                            break;
                        }
                        dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                    }while (dgr == DialogResult.Retry);
                }
            }
        }

        private void dataGridViewMdse_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 5)
            {
                bool mdseReleaseDateValid = checkMdseReleaseDate(e.RowIndex);
                if (!mdseReleaseDateValid)
                {
                    MessageBox.Show(Commons.GetMessageString("InvalidDate"));
                    dataGridViewMdse.CancelEdit();
                }
                else
                {
                    dataGridViewMdse.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }
    }
}
