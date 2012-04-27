/**************************************************************************************************************
* CashlinxDesktop
* PoliceHoldList
* This form is used to show the list of transactions for a customer available for police hold
* Sreelatha Rengarajan 9/21/2009 Initial version
* SR 7/23/2010 Fixed the issue of pick slip print not working
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
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
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
using Pawn.Logic.PrintQueue;

namespace Pawn.Forms.Pawn.Customer.Holds
{
    public partial class PoliceHoldsList : Form
    {
        public NavBox NavControlBox;
        private Form ownerfrm;
        private BindingSource _bindingSource1;
        private BindingSource _bindingSource2;
        private DataTable _transactionTable;
        private DataTable _merchandiseTable;
        private string _storeNumber = "";
        private string _customerNumber = "";
        private string _userId = "";
        private const string holdType = HoldData.POLICE_HOLD;
        private int _pageIndex = 0;
        private int _numberOfPages = 0;
        private const int maxRows = 7;
        private bool _rowSelected = false;
        private bool _locationSet = false;
        private int _numberOfSelections = 0;
        List<HoldData> _selectedTransactions = new List<HoldData>();
        List<string> mdseICN = new List<string>();
        List<int> _selectedLoanNumbers = new List<int>();
        List<string> restrictICN = new List<string>();
        private int _rowClicked = 0;
        private int enabledRowsCount = 0;
        private int selectedMdseRowCount = 0;
        private int mdseRowCount = 0;
        private bool currentPoliceHold = false;
        private const string POLICEHOLDTYPE = "Police Hold";
        StringBuilder strTicketMessage = new StringBuilder();

        public PoliceHoldsList()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }

        public bool PoliceSeize { get; set; }

        private void PoliceHoldsList_Load(object sender, EventArgs e)
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
            if (PoliceSeize)
                labelMainHeading.Text = "Select for Police Seize";
            //Create the transaction Table
            _transactionTable = new DataTable();
            _merchandiseTable = new DataTable();
            string _errorCode;
            string _errorMsg;
            var retVal = HoldsProcedures.ExecuteGetHolds(_storeNumber, _customerNumber, holdType,
                                                          out _transactionTable, out _merchandiseTable, out _errorCode, out _errorMsg);

            if (retVal && _transactionTable != null)
            {
                try
                {
                    var key = new DataColumn[2];
                    key[0] = _transactionTable.Columns[holdstransactioncursor.TICKETNUMBER];
                    key[1] = _transactionTable.Columns["time_made"];
                    _transactionTable.PrimaryKey = key;
                    _bindingSource1 = new BindingSource
                    {
                        DataSource = _transactionTable
                    };
                    this.dataGridViewTransactions.AutoGenerateColumns = false;
                    this.dataGridViewTransactions.DataSource = _bindingSource1;
                    this.dataGridViewTransactions.Columns[2].DataPropertyName = holdstransactioncursor.TRANSACTIONDATE;
                    this.dataGridViewTransactions.Columns[3].DataPropertyName = holdstransactioncursor.TRANSACTIONTYPE;//"transactiontype";
                    this.dataGridViewTransactions.Columns[4].DataPropertyName = holdstransactioncursor.TICKETNUMBER;//"ticket_number";
                    this.dataGridViewTransactions.Columns[5].DataPropertyName = holdstransactioncursor.STATUS;//"pstatus";
                    this.dataGridViewTransactions.Columns[6].DataPropertyName = holdstransactioncursor.PFISTATE;//"pfi_state";
                   
                    if (!new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId))
                    {
                        this.dataGridViewTransactions.Columns[7].DataPropertyName = holdstransactioncursor.LOANAMOUNT;//"org_amount";
                    }
                    else
                    {
                        this.dataGridViewTransactions.Columns[7].DataPropertyName = holdstransactioncursor.CURRENTPRINCIPALAMOUNT;//"cur_amount in payment_detail table"; 
                    }

                    this.dataGridViewTransactions.Columns[8].DataPropertyName = holdstransactioncursor.ORIGINALTICKETNUMBER;//"org_ticket";
                    this.dataGridViewTransactions.Columns[9].DataPropertyName = holdstransactioncursor.PREVIOUSTICKETNUMBER;//"prev_ticket";
                    this.dataGridViewTransactions.Columns[1].ReadOnly = false;

                    this.dataGridViewTransactions.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridViewMdse.AutoGenerateColumns = false;
                    if (!PoliceSeize)
                    {
                        foreach (DataGridViewRow dgvr in dataGridViewTransactions.Rows)
                        {
                            if (dgvr.Cells[5].Value.ToString().Contains(POLICEHOLDTYPE))
                            {
                                currentPoliceHold = true;
                                dgvr.Cells[0].ReadOnly = false;
                                dgvr.Cells[1].ReadOnly = true;
                                dgvr.Cells[1].Value = "true";
                            }
                        }
                    }
                    currentPoliceHold = false;
                    FindNumberOfPages(_transactionTable);
                }
                catch (Exception ex)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, ex.Message);
                }
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
                if (e.ColumnIndex == 0 && !dataGridViewTransactions.Rows[e.RowIndex].Cells[0].ReadOnly)
                {
                    try
                    {
                        if (this.dataGridViewMdse.Visible == false)
                        {
                            selectedMdseRowCount = 0;
                            mdseRowCount = 0;
                            enabledRowsCount = 0;
                            ShowMerchandiseData(e.RowIndex);
                        }
                        else
                        {
                            HideMerchandiseData(e.RowIndex);
                        }
                    }
                    catch (SystemException ex)
                    {
                        BasicExceptionHandler.Instance.AddException("Error occurred when viewing customer holds list", new ApplicationException(ex.Message));
                        this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                    }
                }
            }
            else
            {
                //Headers were clicked for sort
                //Maintain paging
                FindNumberOfPages(_transactionTable);
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
                    if (dataGridViewTransactions.CurrentRow.Cells[5].Value.ToString().Contains(POLICEHOLDTYPE))
                        dataGridViewTransactions.CurrentRow.Cells[1].ReadOnly = true;
                }
                _numberOfSelections++;
            }
            else
            {
                if (dataGridViewTransactions.CurrentRow != null)
                {
                    if (!(dataGridViewTransactions.CurrentRow.Cells[5].Value.ToString().Contains(POLICEHOLDTYPE)))
                        dataGridViewTransactions.CurrentRow.Cells[1].Value = "false";
                    int index = _selectedLoanNumbers.FindIndex(loanno => loanno == Utilities.GetIntegerValue(dataGridViewTransactions.CurrentRow.Cells[4].Value));
                    if (index > -1)
                    {
                        _selectedLoanNumbers.RemoveAt(index);
                    }
                }
            }
            this.dataGridViewMdse.Visible = false;
        }

        private void ShowMerchandiseData(int rowIndex)
        {
            _bindingSource2 = new BindingSource();
            var keys = new object[2];
            
            var drv = (DataRowView)(_bindingSource1.Current);
            keys[0] = drv.Row[holdstransactioncursor.TICKETNUMBER];
            keys[1] = drv.Row["time_made"];

            var selectedRow = _transactionTable.Rows.Find(keys);
            var selectedTicketNumber = selectedRow[holdstransactioncursor.ORIGINALTICKETNUMBER].ToString();
            var selectedDocType = selectedRow[holdstransactioncursor.TRANSACTIONTYPE].ToString();
            var docType = string.Empty;
            docType = selectedDocType == "PAWN LOAN" ? "1" : "2";
            var transactionStatus = string.Empty;
            if (_merchandiseTable != null && _merchandiseTable.Rows.Count > 0)
            {
                _rowSelected = true;
                string filterExpression = string.Format("{0}='{1}' AND {2}='{3}'", holdsmdsecursor.ICNDOC, selectedTicketNumber, holdsmdsecursor.ICNDOCTYPE.ToString(), docType);
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
                        transactionStatus = dgvr.Cells[5].Value.ToString();
                    }
                }
                DataTable newMdseTable = _merchandiseTable.Clone();
                foreach (DataRow dr in merchandiseRows)
                {
                    mdseRowCount++;
                    newMdseTable.ImportRow(dr);
                }
                _bindingSource2.DataSource = newMdseTable;
                this.dataGridViewTransactions.Rows[rowIndex].Height += this.dataGridViewMdse.Height + 20;
                this.dataGridViewTransactions.Rows[rowIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                this.dataGridViewMdse.DataSource = _bindingSource2;
                this.dataGridViewMdse.Columns[1].DataPropertyName = holdsmdsecursor.ITEMDESCRIPTION;//"md_desc";
                this.dataGridViewMdse.Columns[2].DataPropertyName = holdsmdsecursor.ICN;//"icn";
                this.dataGridViewMdse.Columns[3].DataPropertyName = holdsmdsecursor.STATUS;//"mstatus";
                this.dataGridViewMdse.Columns[4].DataPropertyName = holdsmdsecursor.AMOUNT;//"org_amount";
                this.dataGridViewMdse.Columns[5].DataPropertyName = holdsmdsecursor.HOLDID;//"hold_id";
                this.dataGridViewMdse.Visible = true;
                bool showMessage = false;
                foreach (DataGridViewRow dgvr in dataGridViewMdse.Rows)
                {
                    DataGridViewRow row = dgvr;
                    string merchandiseICN = row.Cells[2].Value.ToString();
                    int index = mdseICN.FindIndex(icnnumber => icnnumber == merchandiseICN);
                    if (index > -1)
                    {
                        //Check if it is in the restrictICN list in which case the row should be read only
                        //and not checked
                        int idx = restrictICN.FindIndex(icnno => icnno == merchandiseICN);
                        if (idx > -1)
                        {
                            if (transactionStatus.Contains(POLICEHOLDTYPE))
                                row.Cells[0].Value = "true";
                            else
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
                        if (enabledRowsCount > 0)
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
                    else
                    {
                        string strValue = row.Cells[3].Value.ToString();
                        string mdseStatus = strValue.Substring(0, strValue.IndexOf("-"));
                        string mdseHoldType = strValue.Substring(strValue.IndexOf("-") + 1);
                        // mdseICN.Add(merchandiseICN);
                        if (PoliceSeize)
                        {
                            if (Commons.CanBePoliceSeized(mdseStatus))
                            {
                                enabledRowsCount++;
                            }
                            else
                            {
                                restrictICN.Add(merchandiseICN);
                                if (transactionStatus.Contains(POLICEHOLDTYPE))
                                    row.Cells[0].Value = "true";
                                else
                                    row.Cells[0].Value = "false";
                                showMessage = true;
                                row.DefaultCellStyle.BackColor = Color.Gray;
                                row.ReadOnly = true;
                            }
                        }
                        else
                        {
                            if (Commons.CanBePutOnPoliceHold(mdseStatus, mdseHoldType))
                            {
                                // row.DefaultCellStyle.BackColor = Color.White;
                                // row.Cells[0].Value = "true";
                                enabledRowsCount++;
                            }
                            else
                            {
                                restrictICN.Add(merchandiseICN);
                                row.ReadOnly = true;
                                if (transactionStatus.Contains(POLICEHOLDTYPE))
                                    row.Cells[0].Value = "true";
                                else
                                    row.Cells[0].Value = "false";
                                showMessage = true;
                                row.DefaultCellStyle.BackColor = Color.Gray;
                            }
                        }
                    }
                }
                labelMdseUnavailableMsg.Visible = showMessage;
                _rowClicked = rowIndex;
                if (_rowClicked >= maxRows)
                    _rowClicked = _rowClicked - (maxRows * (_pageIndex - 1));
                DisableControls();
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
            if (e.RowIndex > -1 && e.ColumnIndex == 4)
            {
                e.FormattingApplied = true;
                e.Value = string.Format("{0:C}", e.Value);
            }

            if (e.RowIndex > -1 && e.ColumnIndex == 3)
            {
                e.FormattingApplied = true;
                int idx = e.Value.ToString().IndexOf("-");
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

            this.labelPageNo.Text = "Page " + _pageIndex + " of " + _numberOfPages;
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
                    strTicketMessage = new StringBuilder();
                    getSelectedTransactions();
                    if (strTicketMessage.Length > 0)
                        MessageBox.Show(strTicketMessage.ToString());
                    if (_selectedTransactions.Count > 0)
                    {
                        string strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                        bool retVal = false;
                        if (PoliceSeize)
                            retVal = ServiceLoanProcedures.CheckCurrentTempStatus(ref _selectedTransactions, strUserId, ServiceTypes.POLICESEIZE);
                        else
                            retVal = ServiceLoanProcedures.CheckCurrentTempStatus(ref _selectedTransactions, strUserId, ServiceTypes.POLICEHOLD);
                        if (_selectedTransactions.Count == 0)
                        {
                            return;
                        }
                        GlobalDataAccessor.Instance.DesktopSession.HoldsData = _selectedTransactions;
                        if (PoliceSeize)
                        {
                            foreach (var polHoldData in GlobalDataAccessor.Instance.DesktopSession.HoldsData)
                                polHoldData.TempStatus = StateStatus.PS;
                        }

                        if (retVal)
                        {
                            //here put in code that checks to see if there's a jewelry item
                            bool containsJewelry = false;
                            foreach (var holdData in GlobalDataAccessor.Instance.DesktopSession.HoldsData)
                            { 
                                Item itemi = holdData.Items.Find(item => item.IsJewelry);
                                if (itemi != null && itemi.ItemStatus == ProductStatus.PFI)
                                {
                                    containsJewelry = true;
                                    break;
                                }
                            }
                            if (containsJewelry)
                            {
                                global::Pawn.Forms.Pawn.Customer.Holds.MultiJewelryCaseNumber multiJcase = new global::Pawn.Forms.Pawn.Customer.Holds.MultiJewelryCaseNumber();
                                multiJcase.ShowDialog();
                            }
                            //bool containsJewelry = _selectedTransactions.find
                            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                        }
                        else
                        {
                            throw new Exception("Error when trying to update temp status on selected transactions");
                        }
                    }
                    else
                        MessageBox.Show("No transactions to process");
                }
                else
                {
                    return;
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
            //_selectedLoanNumbers = new List<int>();
            List<Item> transactionItems = new List<Item>();
            var dateMade = string.Empty;

            var amount = 0;
            foreach (DataGridViewRow dgvr in dataGridViewMdse.Rows)
            {
                amount += Utilities.GetIntegerValue(dgvr.Cells[4].Value, 0);
            }

            foreach (DataGridViewRow dgvr in dataGridViewTransactions.Rows)
            {
                int selectedTicketNumber = Utilities.GetIntegerValue(dgvr.Cells[4].Value);
                int origTicketNumber = Utilities.GetIntegerValue(dgvr.Cells[8].Value.ToString());
                int loanIndex = _selectedLoanNumbers.FindIndex(loanNumber => loanNumber == selectedTicketNumber);
                if (loanIndex > -1)
                {
                    //Check the current temp status If it is a locked status do not add it
                    HoldData policeHoldData = new HoldData();
                    policeHoldData.StatusCode = dgvr.Cells[5].Value.ToString().Substring(0, dgvr.Cells[5].Value.ToString().IndexOf("-", System.StringComparison.Ordinal));
                    policeHoldData.TicketNumber = selectedTicketNumber;
                    string tranType = dgvr.Cells[3].Value.ToString();
                    if (tranType == "PAWN LOAN")
                        policeHoldData.RefType = "1";
                    else if (tranType == "PURCHASE")
                        policeHoldData.RefType = "2";
                    dateMade = dgvr.Cells[2].Value.ToString();
                    policeHoldData.CustomerNumber = _customerNumber;
                    policeHoldData.OrgShopNumber = _storeNumber;
                    policeHoldData.UserId = _userId;
                    policeHoldData.TransactionDate = Utilities.GetDateTimeValue(dateMade, DateTime.MaxValue);
                    policeHoldData.Amount = amount;
                    policeHoldData.CurrentPrincipalAmount = Utilities.GetDecimalValue(dgvr.Cells[7].Value, 0);
                    policeHoldData.PrevTicketNumber = Utilities.GetIntegerValue(dgvr.Cells[9].Value, 0);
                    policeHoldData.OrigTicketNumber = Utilities.GetIntegerValue(dgvr.Cells[8].Value);
                    policeHoldData.TempStatus = (StateStatus)Enum.Parse(typeof(StateStatus),
                                                                        Utilities.GetStringValue(dgvr.Cells[6].Value) != string.Empty
                                                                        ? Utilities.GetStringValue(dgvr.Cells[6].Value)
                                                                        : StateStatus.BLNK.ToString());
                    var lockedProcess = string.Empty;
                    if (Commons.IsLockedStatus(policeHoldData.TempStatus.ToString(), ref lockedProcess))
                    {
                        strTicketMessage.Append("Loan " + selectedTicketNumber + " is locked by " + lockedProcess + ". Cannot process this loan." + System.Environment.NewLine);
                        continue;
                    }
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
                                newItem.mDocNumber = Utilities.GetIntegerValue(origTicketNumber, 0);
                                newItem.ItemAmount = Utilities.GetDecimalValue(merchandiseRows[j][holdsmdsecursor.AMOUNT], 0);
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
                labelMdseUnavailableMsg.Location = new System.Drawing.Point(dataGridViewMdse.Location.X, dataGridViewMdse.Location.Y - 19);
                _locationSet = true;
                e.Handled = true;
            }
            if (e.RowIndex > -1 && e.ColumnIndex == 5)
            {
                //if the reason code is PFIWAIT or PFIVERIFY
                //then change the color of the status string to red
                string pawnStatus = e.Value.ToString();
                bool pfiString = false;
                bool purString = false;
                if (pawnStatus.Contains("PFI"))
                {
                    pfiString = true;
                }
                if (pawnStatus.Contains("PUR"))
                    purString = true;
                int idx = pawnStatus.IndexOf("-");
                var s1 = string.Empty;
                var s2 = string.Empty;
                if (idx > 0)
                {
                    s1 = pawnStatus.Substring(0, idx);
                    s2 = idx + 1 != pawnStatus.Length ? pawnStatus.Substring(idx, pawnStatus.Length - idx) : "";
                }

                using (
                    Brush gridBrush = new SolidBrush(Color.Red),
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

                        if (pfiString)
                        {
                            e.Graphics.DrawString(s1, e.CellStyle.Font,
                                                  Brushes.Red, e.CellBounds.X + 30,
                                                  e.CellBounds.Y + 2, StringFormat.GenericDefault);
                            if (s2.Length > 0)
                            {
                                e.Graphics.DrawString(s2, e.CellStyle.Font,
                                                      Brushes.Black, e.CellBounds.X + 50, e.CellBounds.Y + 2,
                                                      StringFormat.GenericDefault);
                            }
                        }
                        else
                        {
                            e.Graphics.DrawString(s1, e.CellStyle.Font,
                                                  Brushes.Black, e.CellBounds.X + 30,
                                                  e.CellBounds.Y + 2, StringFormat.GenericDefault);
                            if (purString)
                            {
                                e.Graphics.DrawString(s2, e.CellStyle.Font,
                                                      Brushes.Black, e.CellBounds.X + 50, e.CellBounds.Y + 2,
                                                      StringFormat.GenericDefault);
                            }
                            else
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
            this.buttonCancel.Enabled = false;
            this.buttonPrintPickSlip.Enabled = false;
            this.buttonContinue.Enabled = false;
            this.dataGridViewTransactions.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTransactions.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTransactions.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTransactions.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTransactions.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void EnableControls()
        {
            this.customButtonDeselectAll.Enabled = true;
            this.buttonFirst.Enabled = true;
            this.buttonNext.Enabled = true;
            this.buttonPrevious.Enabled = true;
            this.buttonLast.Enabled = true;
            this.buttonCancel.Enabled = true;
            this.buttonPrintPickSlip.Enabled = true;
            this.buttonContinue.Enabled = true;
            this.dataGridViewTransactions.Columns[2].SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTransactions.Columns[3].SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTransactions.Columns[4].SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTransactions.Columns[5].SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTransactions.Columns[7].SortMode = DataGridViewColumnSortMode.Automatic;
        }

        private void dataGridViewMdse_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 1)
            {
                try
                {
                    int storeId = Utilities.GetIntegerValue(_storeNumber, 0);
                    if (dataGridViewTransactions.CurrentRow != null)
                    {
                        var origticketNumber = Utilities.GetIntegerValue(dataGridViewTransactions.CurrentRow.Cells[8].Value);
                        PawnLoan custPawnLoan;
                        string errorMsg;
                        string errorCode;
                        var retValue = HoldsProcedures.GetPawnLoanHolds(storeId, origticketNumber, "0", out custPawnLoan, out errorCode, out errorMsg);

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
                if (dataGridViewMdse.Visible || currentPoliceHold)
                    return;
                bool allMdsePFI = true;
                DataGridViewCheckBoxCell oCell = (DataGridViewCheckBoxCell)dataGridViewTransactions.Rows[e.RowIndex].Cells[1];
                string tranStatus = dataGridViewTransactions.Rows[e.RowIndex].Cells[5].Value.ToString();
                if (oCell.Value != null && oCell.Value.ToString() == "true")
                {
                    //if the transaction is already added to the list no need for the mdse addition
                    int loanIndex = _selectedLoanNumbers.FindIndex(loanNumber => loanNumber == (Utilities.GetIntegerValue(dataGridViewTransactions.Rows[e.RowIndex].Cells[4].Value)));
                    if (loanIndex < 0)
                    {
                        if (_merchandiseTable != null)
                        {
                            DataRow[] merchandiseRows = _merchandiseTable.Select(holdsmdsecursor.ICNDOC + "='" + dataGridViewTransactions.Rows[e.RowIndex].Cells[8].Value.ToString() + "'");

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
                                    string strValue = row[2].ToString();
                                    string mdseStatus = strValue.Substring(0, strValue.IndexOf("-"));
                                    string locHoldType = Utilities.GetStringValue(row[5], "");
                                    
                                    if (PoliceSeize)
                                    {
                                        if (Commons.CanBePoliceSeized(mdseStatus))
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
                                    else
                                    {
                                        if (Commons.CanBePutOnPoliceHold(mdseStatus, locHoldType))
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
                    else
                    {
                        if (_merchandiseTable != null)
                        {
                            var merchandiseRows = _merchandiseTable.Select(string.Format("{0}='{1}'", holdsmdsecursor.ICNDOC, dataGridViewTransactions.Rows[e.RowIndex].Cells[8].Value.ToString()));
                            var merchandiseRowCount = merchandiseRows.Length;
                            var mdseCount = 0;
                            foreach (var dgvr in merchandiseRows)
                            {
                                var row = dgvr;
                                var merchandiseICN = row[1].ToString();

                                int index = mdseICN.FindIndex(icnnumber => icnnumber == merchandiseICN);
                                if (index > -1)
                                {
                                    mdseCount++;
                                }
                                else
                                {
                                    mdseICN.Add(merchandiseICN);
                                    mdseCount++;
                                }
                            }
                            if (tranStatus.Contains("PFI"))
                            {
                                if (mdseCount != merchandiseRowCount)
                                {
                                    oCell.ReadOnly = true;
                                    ShowMerchandiseData(e.RowIndex);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (_merchandiseTable != null)
                    {
                        var merchandiseRows = _merchandiseTable.Select(holdsmdsecursor.ICNDOC + "='" + dataGridViewTransactions.Rows[e.RowIndex].Cells[8].Value.ToString() + "'");
                        var merchandiseRowCount = merchandiseRows.Length;
                        for (var j = 0; j < merchandiseRowCount; j++)
                        {
                            var merchandiseICN = merchandiseRows[j][holdsmdsecursor.ICN].ToString();
                            var idx = mdseICN.FindIndex(mICN => mICN == merchandiseICN);
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

        private void dataGridViewTransactions_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewTransactions.IsCurrentCellInEditMode)
                dataGridViewTransactions.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void buttonPrintPickSlip_Click(object sender, EventArgs e)
        {
            try
            {
                if (_numberOfSelections > 0)
                {
                    strTicketMessage = new StringBuilder();
                    getSelectedTransactions();
                    if (strTicketMessage.Length > 0)
                    {
                        MessageBox.Show(strTicketMessage.ToString());
                        return;
                    }
                    else
                    {
                        PickSlipPrint printSlip = new PickSlipPrint();
                        if (PoliceSeize)
                            printSlip.PickSlipType = " Police Seize ";
                        else
                            printSlip.PickSlipType = " Police Hold ";
                        printSlip.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Error when trying to print pick slips", new ApplicationException(ex.Message));
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        private void dataGridViewMdse_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 0 && !dataGridViewMdse.Rows[e.RowIndex].ReadOnly)
            {
                var oCell = (DataGridViewCheckBoxCell)dataGridViewMdse.Rows[e.RowIndex].Cells[0];
                var itemICN = string.Empty;
                if (oCell.Value != null && oCell.Value.ToString() == "true")
                {
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
                    {
                        mdseICN.RemoveAt(index);
                        if (selectedMdseRowCount > 0)
                            selectedMdseRowCount--;
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
            showPage(_pageIndex);
        }
    }
}
