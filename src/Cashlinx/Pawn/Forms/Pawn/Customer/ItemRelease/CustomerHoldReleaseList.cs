/**************************************************************************************************************
* CashlinxDesktop
* CustomerHoldList
* This form is used to show the list of transactions for a customer available for hold
* Sreelatha Rengarajan 8/6/2009 Initial version
*   PWNU00000611 SMurphy 4/7/2010 changed buttonDeselectAll to custom button  
 * SR 4/7/2010 Fixed the issue of hold comment and release date not showing up in the release info page-PWNU00000620
 * Also added check to not call the stored procedure to update release date if the release date
 * was not changed by the user. PWNU00000579
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
    public partial class CustomerHoldReleaseList : Form
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
        private int _rowClicked = 0;



        public CustomerHoldReleaseList()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
        }

        private void CustomerHoldsList_Load(object sender, EventArgs e)
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
            bool retVal = HoldsProcedures.ExecuteGetReleases(_storeNumber, _customerNumber, _statusCode, HoldData.CUSTOMER_HOLD,
                                                                     out _transactionTable, out _merchandiseTable, out _errorCode, out _errorMsg);

            if (retVal && _transactionTable != null)
            {
                try
                {

                    DataColumn[] key = new DataColumn[1];
                    key[0] = _transactionTable.Columns[holdstransactioncursor.TICKETNUMBER];

                    _transactionTable.PrimaryKey = key;


                    _bindingSource1 = new BindingSource
                    {
                        DataSource = _transactionTable
                    };
                    customDataGridViewTransactions.AutoGenerateColumns = false;

                    if (customDataGridViewTransactions != null)
                    {

                        customDataGridViewTransactions.DataSource = _bindingSource1;
                        customDataGridViewTransactions.Columns[2].DataPropertyName = holdstransactioncursor.TRANSACTIONDATE;
                        customDataGridViewTransactions.Columns[3].DataPropertyName = holdstransactioncursor.TRANSACTIONTYPE;//"transactiontype";
                        customDataGridViewTransactions.Columns[4].DataPropertyName = holdstransactioncursor.TICKETNUMBER;//"ticket_number";
                        customDataGridViewTransactions.Columns[5].DataPropertyName = holdstransactioncursor.STATUS;//"pstatus";
                        customDataGridViewTransactions.Columns[6].DataPropertyName = holdstransactioncursor.PFISTATE;//"temp_status";
                        customDataGridViewTransactions.Columns[7].DataPropertyName = holdstransactioncursor.RELEASEDATE;//"release_date";

                        if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId))
                        {
                            customDataGridViewTransactions.Columns[8].DataPropertyName = holdstransactioncursor.CURRENTPRINCIPALAMOUNT;//"cur_amount in payment_detail table";
                        }
                        else
                        {
                            customDataGridViewTransactions.Columns[8].DataPropertyName = holdstransactioncursor.LOANAMOUNT;//"org_amount";
                        }

                        customDataGridViewTransactions.Columns[9].DataPropertyName = holdstransactioncursor.CREATIONDATE;//"creationdate";
                        customDataGridViewTransactions.Columns[10].DataPropertyName = holdstransactioncursor.CREATEDBY;//"createdby";
                        customDataGridViewTransactions.Columns[11].DataPropertyName = holdstransactioncursor.HOLDCOMMENT;//"hold_comment";
                        customDataGridViewTransactions.Columns[12].DataPropertyName = holdstransactioncursor.ORIGINALTICKETNUMBER;//"org_ticket";

                        customDataGridViewTransactions.Columns[1].ReadOnly = false;
                        //Set sort mode
                        customDataGridViewTransactions.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                        customDataGridViewTransactions.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                        customDataGridViewTransactions.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    customDataGridViewMDSE.AutoGenerateColumns = false;
                    FindNumberOfPages(_transactionTable);
                }
                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException(ex.Message, new ApplicationException("Error when trying to retrieve transactions to release customer hold"));
                    NavControlBox.Action = NavBox.NavAction.CANCEL;
                }
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

                if (e.ColumnIndex == 0 && !customDataGridViewTransactions.Rows[e.RowIndex].Cells[0].ReadOnly)
                {
                    try
                    {
                        if (this.customDataGridViewMDSE.Visible == false)
                        {

                            _bindingSource2 = new BindingSource();
                            DataRowView drv = (DataRowView)(_bindingSource1.Current);
                            DataRow selectedRow = _transactionTable.Rows.Find(drv.Row[holdstransactioncursor.TICKETNUMBER]);
                            string selectedTicketNumber = selectedRow[holdstransactioncursor.TICKETNUMBER].ToString();
                            string origTicketNumber = selectedRow[holdstransactioncursor.ORIGINALTICKETNUMBER].ToString();
                            if (_merchandiseTable != null && _merchandiseTable.Rows.Count > 0)
                            {
                                _rowSelected = true;
                                DataRow[] merchandiseRows = _merchandiseTable.Select(holdsmdsecursor.ICNDOC + "='" + origTicketNumber + "'");
                                if (merchandiseRows.Length == 0) return;
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
                                this.customDataGridViewMDSE.Columns[3].DataPropertyName = holdsmdsecursor.AMOUNT;//"org_amount";
                                this.customDataGridViewMDSE.Visible = true;
                                _rowClicked = e.RowIndex;
                                if (_rowClicked >= MaxRows)
                                    _rowClicked = _rowClicked - (MaxRows * (_pageIndex - 1));
                                DisableControls();


                            }


                        }
                        else
                        {
                            EnableControls();
                            _rowSelected = false;
                            _rowClicked = -1;
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
                        BasicExceptionHandler.Instance.AddException("Error occurred when viewing customer holds release list", new ApplicationException(ex.Message));
                        this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    //Set cell style for eligible for release
                    //If the checkbox is checked, set the readonly to false and change the backcolor to blue
                    //if not set the readonly to true and change the backcolor to white
                    if (customDataGridViewTransactions.Rows[e.RowIndex].Cells[7].ReadOnly)
                    {
                        customDataGridViewTransactions.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                        customDataGridViewTransactions.Rows[e.RowIndex].Cells[7].Style.BackColor = Color.Blue;
                        customDataGridViewTransactions.Rows[e.RowIndex].Cells[7].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        customDataGridViewTransactions.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                        customDataGridViewTransactions.Rows[e.RowIndex].Cells[7].Style.BackColor = Color.White;
                        customDataGridViewTransactions.Rows[e.RowIndex].Cells[7].Style.ForeColor = Color.Black;
                        customDataGridViewTransactions.CancelEdit();

                    }


                }

            }
            else
            {
                if (_rowSelected)
                    return;
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
                    int idx = e.Value.ToString().LastIndexOf("-", System.StringComparison.Ordinal);
                    if (idx + 1 == e.Value.ToString().Length)
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
            if (e.RowIndex > -1 && e.ColumnIndex == 3)
            {
                e.FormattingApplied = true;
                e.Value = string.Format("{0:C}", e.Value);
            }

            if (e.RowIndex > -1 && e.ColumnIndex == 2)
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
            this.labelPageNo.Text = "Page " + _pageIndex + " of " + _numberOfPages;


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
            List<Item> transactionItems = new List<Item>();
            var dateMade = string.Empty;

            foreach (DataGridViewRow dgvr in customDataGridViewTransactions.Rows)
            {
                if (dgvr.Cells[1].Value != null && dgvr.Cells[1].Value.ToString() == "true")
                {
                    int selectedTicketNumber = Utilities.GetIntegerValue(dgvr.Cells[4].Value);
                    string origTicketNumber = dgvr.Cells[12].Value.ToString();
                    dateMade = dgvr.Cells[2].Value.ToString();
                    HoldData customerHoldData = new HoldData
                                                    {
                                                        StatusCode =
                                                            dgvr.Cells[5].Value.ToString().Substring(0,
                                                                                                     dgvr.Cells[5].Value
                                                                                                         .ToString().IndexOf("-", System.StringComparison.Ordinal)),
                                                        TicketNumber = selectedTicketNumber,
                                                        RefType = "1",
                                                        CustomerNumber = _customerNumber,
                                                        OrgShopNumber = _storeNumber,
                                                        TransactionDate =
                                                            Utilities.GetDateTimeValue(dateMade, DateTime.MaxValue),
                                                        Amount = Utilities.GetDecimalValue(dgvr.Cells[8].Value, 0),
                                                        HoldDate =
                                                            Utilities.GetDateTimeValue(dgvr.Cells[9].Value.ToString(),
                                                                                       DateTime.MaxValue),
                                                        UserId = GlobalDataAccessor.Instance.DesktopSession.UserName,
                                                        HoldComment = dgvr.Cells[11].Value != null ? dgvr.Cells[11].Value.ToString() : string.Empty,
                                                        HoldType = HoldTypes.CUSTHOLD.ToString(),
                                                        ReleaseDate =
                                                            Utilities.GetDateTimeValue(
                                                            dgvr.Cells[7].Value.ToString().FormatStringAsDate())
                                                    };
                    transactionItems = new List<Item>();
                    if (_merchandiseTable != null)
                    {
                        DataRow[] merchandiseRows = _merchandiseTable.Select(holdsmdsecursor.ICNDOC + "='" + origTicketNumber + "'");
                        int merchandiseRowCount = merchandiseRows.Length;
                        for (int j = 0; j < merchandiseRowCount; j++)
                        {
                            ProductStatus itemStatus = new ProductStatus();
                            string locStatus = merchandiseRows[j][holdsmdsecursor.STATUS].ToString();
                            if (locStatus.StartsWith("PFI"))
                                itemStatus = ProductStatus.PFI;
                            else
                                itemStatus = ProductStatus.ALL;
                            Item newItem = new Item
                                        {
                                            TicketDescription =
                                                merchandiseRows[j][holdsmdsecursor.ITEMDESCRIPTION].
                                                ToString(),
                                            Icn = merchandiseRows[j][holdsmdsecursor.ICN].ToString(),
                                            Location_Aisle = merchandiseRows[j][holdsmdsecursor.AISLE].ToString(),
                                            Location_Shelf = merchandiseRows[j][holdsmdsecursor.SHELF].ToString(),
                                            Location =
                                                merchandiseRows[j][holdsmdsecursor.OTHERLOCATION].
                                                ToString(),
                                                ItemStatus = itemStatus,
                                                IsJewelry = Utilities.IsJewelry(Convert.ToInt32(merchandiseRows[j][holdsmdsecursor.CATCODE]))  
                                        };
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
            customDataGridViewTransactions.CurrentCell = customDataGridViewTransactions.Rows[(MaxRows * (_pageIndex - 1))].Cells[0];
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
                if (pawnStatus.Contains("PFI"))
                {
                    int idx = pawnStatus.IndexOf("-");
                    var s1 = string.Empty;
                    var s2 = string.Empty;
                    if (idx > 0)
                    {
                        s1 = pawnStatus.Substring(0, idx + 1);
                        s2 = pawnStatus.Substring(idx, pawnStatus.Length - idx);
                    }


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
                                                  Brushes.Red, e.CellBounds.X + 15, e.CellBounds.Y + 5, StringFormat.GenericDefault);

                            e.Handled = true;
                        }
                    }



                }
                else
                {
                    //For all the other columns just paint
                    e.Paint(e.ClipBounds, e.PaintParts);
                    e.Handled = true;
                }
            }
            else if (e.RowIndex > -1 && e.ColumnIndex == 7)
            {
                //If the release date was updated do not change it
                if (_releaseDateValid)
                    return;
                //If the cell being painted is the release date show the release
                //date from the mdse list
                DateTime relDate = DateTime.MaxValue;
                string holdCommentValue = string.Empty;
                string holdCreationDate = string.Empty;
                int selectedTicketNumber = Utilities.GetIntegerValue(customDataGridViewTransactions.Rows[e.RowIndex].Cells[12].Value);
                if (_merchandiseTable != null)
                {
                    DataRow[] merchandiseRows = _merchandiseTable.Select(holdsmdsecursor.ICNDOC + "='" + selectedTicketNumber + "'");
                    int merchandiseRowCount = merchandiseRows.Length;
                    relDate = Utilities.GetDateTimeValue(merchandiseRows[0][holdsmdsecursor.RELEASEDATE].
                                                          ToString());
                    holdCommentValue = Utilities.GetStringValue(merchandiseRows[0][holdsmdsecursor.HOLDCOMMENT].ToString());
                    holdCreationDate = Utilities.GetStringValue(merchandiseRows[0][holdsmdsecursor.HOLDCREATIONDATE].ToString());
                }
                using (
 Brush gridBrush = new SolidBrush(Color.Black),
       backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    if (relDate == DateTime.MaxValue)
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                    else
                        customDataGridViewTransactions.Rows[e.RowIndex].Cells[7].Value = relDate.FormatDate();
                    customDataGridViewTransactions.Rows[e.RowIndex].Cells[11].Value = holdCommentValue;
                    customDataGridViewTransactions.Rows[e.RowIndex].Cells[9].Value = holdCreationDate;
                }
            }


        }


        private void DisableControls()
        {
            this.buttonDeselectAll.Enabled = false;
            this.buttonFirst.Enabled = false;
            this.buttonNext.Enabled = false;
            this.buttonPrevious.Enabled = false;
            this.buttonLast.Enabled = false;
            this.customButtonCancel.Enabled = false;
            this.customButtonUpdateReleaseDate.Enabled = false;
            this.customButtonReleaseHold.Enabled = false;
            this.customDataGridViewTransactions.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.customDataGridViewTransactions.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.customDataGridViewTransactions.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.customDataGridViewTransactions.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.customDataGridViewTransactions.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.customDataGridViewTransactions.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
        }


        private void EnableControls()
        {
            this.buttonDeselectAll.Enabled = true;
            this.buttonFirst.Enabled = true;
            this.buttonNext.Enabled = true;
            this.buttonPrevious.Enabled = true;
            this.buttonLast.Enabled = true;
            this.customButtonCancel.Enabled = true;
            this.customButtonUpdateReleaseDate.Enabled = true;
            this.customButtonReleaseHold.Enabled = true;
            this.customDataGridViewTransactions.Columns[2].SortMode = DataGridViewColumnSortMode.Automatic;
            this.customDataGridViewTransactions.Columns[3].SortMode = DataGridViewColumnSortMode.Automatic;
            this.customDataGridViewTransactions.Columns[4].SortMode = DataGridViewColumnSortMode.Automatic;
            this.customDataGridViewTransactions.Columns[5].SortMode = DataGridViewColumnSortMode.Automatic;
            this.customDataGridViewTransactions.Columns[7].SortMode = DataGridViewColumnSortMode.Automatic;
            this.customDataGridViewTransactions.Columns[8].SortMode = DataGridViewColumnSortMode.Automatic;


        }

        private void dataGridViewMdse_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex > -1 && e.ColumnIndex == 0)
            {
                try
                {
                    string errorMsg;
                    int storeId = Utilities.GetIntegerValue(_storeNumber, 0);
                    PawnLoan custPawnLoan;
                    if (customDataGridViewTransactions.CurrentRow != null)
                    {
                        int ticketNumber = Utilities.GetIntegerValue(customDataGridViewTransactions.CurrentRow.Cells[12].Value);
                        //Send the control back if the ticket number is not valid
                        if (!(ticketNumber > 0))
                            return;
                        string errorCode;
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
                                    int iCategoryMask = GlobalDataAccessor.Instance.DesktopSession.CategoryXML.GetCategoryMask
                                        (custPawnLoan.Items[e.RowIndex].CategoryCode);
                                    var dmPawnItem = new DescribedMerchandise(iCategoryMask);
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
                    _numberOfSelections++;
                }
                else
                    _numberOfSelections--;

            }
        }

        private bool checkReleaseDate(int rowIdx)
        {
            DateTime dtRelease;
            string releaseDate = ((string)customDataGridViewTransactions.Rows[rowIdx].Cells[7].EditedFormattedValue).ToString();
            if (releaseDate != customDataGridViewTransactions.Rows[rowIdx].Cells[7].Value.ToString())
            {

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
                customDataGridViewTransactions.Rows[rowIdx].Cells[7].Value = dtRelease.FormatDate();
            }

                
            return true;

        }

        private void dataGridViewTransactions_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (customDataGridViewTransactions.IsCurrentCellInEditMode && customDataGridViewTransactions.CurrentCell.ColumnIndex == 1)
                customDataGridViewTransactions.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void buttonUpdateReleaseDate_Click(object sender, EventArgs e)
        {
            if (_numberOfSelections > 0)
            {
                GetSelectedTransactions();
                //Check to see if the transactions that are being sent have a different release
                //date than what was there
                DateTime relDate = DateTime.MaxValue;
                foreach (DataRow dr in _transactionTable.Rows)
                {
                    int selectedTicketNumber = Utilities.GetIntegerValue(dr["ticket_number"]);
                    int origTicketNumber = Utilities.GetIntegerValue(dr["org_ticket"]);
                    if (_merchandiseTable != null)
                    {
                        DataRow[] merchandiseRows = _merchandiseTable.Select(holdsmdsecursor.ICNDOC + "='" + origTicketNumber + "'");
                        relDate = Utilities.GetDateTimeValue(merchandiseRows[0]["release_date"]);
                    }
                        
                        int iDx = _selectedTransactions.FindIndex(
                           pl => pl.TicketNumber == selectedTicketNumber);
                        if (iDx >= 0)
                        {
                            DateTime releaseDate = _selectedTransactions[iDx].ReleaseDate;
                            //If the release date in the selected list is the same as what
                            //was pulled out of the database on load then no need to call the
                            //SP on this loan to update release date
                            if (releaseDate == relDate)
                            _selectedTransactions.RemoveAt(iDx);
                        }
                }
  
                    
                
                bool returnValue = false;
                DialogResult dgr = DialogResult.Retry;
                if (_selectedTransactions.Count > 0)
                {
                    do
                    {
                        returnValue = HoldsProcedures.UpdateReleaseDateOnHolds(_selectedTransactions);
                        if (returnValue)
                        {
                            labelReleaseDateUpdate.Visible = true;
                            this.customButtonCancel.Text = "Close";
                            //Get latest data
                            bool retVal = HoldsProcedures.ExecuteGetReleases(_storeNumber, _customerNumber, _statusCode, HoldData.CUSTOMER_HOLD,
                                                                                     out _transactionTable, out _merchandiseTable, out _errorCode, out _errorMsg);

                            break;
                        }
                        dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                    } while (dgr == DialogResult.Retry);
                }
                else
                {
                    MessageBox.Show("No changes done for update");
                    return;
                }

            }
            else
            {
                MessageBox.Show("No transactions selected for update.");
                return;
            }
        }

        private void buttonReleaseHold_Click(object sender, EventArgs e)
        {
            if (_numberOfSelections > 0)
            {
                try
                {
                    GetSelectedTransactions();
                    string strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                    bool retVal = ServiceLoanProcedures.CheckCurrentTempStatus(ref _selectedTransactions, strUserId, ServiceTypes.CUSTHOLD);
                    if (_selectedTransactions.Count == 0)
                    {
                        return;
                    }
                    GlobalDataAccessor.Instance.DesktopSession.HoldsData = _selectedTransactions;


                    if (retVal)
                    {
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
                        throw new Exception("Error when trying to update temp status on selected loans");
                    }


                }
                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException("Error when trying to put hold on selected transactions", new ApplicationException(ex.Message));
                    this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                }


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
                    customDataGridViewTransactions.CancelEdit();
                }
                else
                {

                }
            }
        }

        private void dataGridViewTransactions_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void customDataGridViewTransactions_Sorted(object sender, EventArgs e)
        {
            ShowPage(_pageIndex);
        }
    }
}
