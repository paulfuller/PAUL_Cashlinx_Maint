/********************************************************************************************
* Namespace: CashlinxDesktop.DesktopForms.Pawn.ShopAdministration
* FileName: BalanceCash
* This form is shown when the logged in user tries to balance cash drawer or safe
* Depending on who the logged in user is the content in the form and the
* tabs shown will be different
* Sreelatha Rengarajan 2/3/2010 Initial version
* SR 4/12/2010 Changed the operation codes being passed to the SP to balance cashdrawer
* SR 4/14/2010 Made sure that safe cannot be closed if there are open or unverified cash drawers
* SR 4/15/2010 Changed the print ledger call
* SR 4/26/2010 Added some checks to not show error message if there were no transactions in the
* cash drawer for a particular day
* SR 5/4/2010 Changed the cash drawer ledger print call and how values are passed to it
* no ticket SMurphy changed reference for ReportsObject
* SR 5/26/2010 Fixed an issue where error message was printed in log even when printing pdf was success
* SR 7/28/2010 Removed the safe login user
* SR 9/29/2010 Changed to load currency user control and added different balancing scenarios
* SR 10/19/2010 in update_Cash _info call added the comments entered in the screen
* Tracy 12/2/2010 added support for auto-run of mult-pistol disposition
* SR 2/18/2011 Added logic to show manager override if user clicks update after clicking balance button
 * SR 03/26/2012 Removed inserting in cd_balance if we are doing trial balance
****************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CashlinxDesktop.UserControls;
using Common.Controllers.Application;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Loan;
using Pawn.Forms.UserControls;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;
using Pawn.Logic.PrintQueue;
using Reports;
using Reports.DSTR;
using Document = Common.Libraries.Objects.Doc.Document;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    public partial class BalanceCash : CustomBaseForm
    {
        decimal balanceAmount = 0.0M;
        DataTable cashdrawerTransactions;
        DataTable cashdrawerPDATransactions;

        BindingSource _bindingSource1;
        BindingSource _bindingSource2;
        private const int maxRows = 15;
        private int _pageIndex = 0;
        private int _numberOfPages = 0;
        private DataTable CDTransactionsTable;
        private DataTable ShopCashDetails;
        decimal cashdrawerAmount = 0.0M;
        private bool SafeUserValidated = false;
        private const string OK = "OK";
        private decimal totDisbursedAmount = 0.0M;
        private decimal totReceiptAmount = 0.0M;
        private bool trialBalance = false;
        private StringBuilder closedUnverifiedCD = new StringBuilder();
        private bool openCashDrawers = false;
        private List<string> denominationData;
        private TabPage shopCashPositionTab;
        private ComboBox safeCombobox;
        private List<string> employeeNumbers;
        private bool allShopTransactions;
        private decimal actualCashAmount;
        private bool isFormValid;
        private string balanceDate;
        private string priorBalance = "N";
        private string balanceDateTime;
        private bool formValid = false;
        ProcessingMessage procMsg;
        bool retval = false;
        DataTable cashdrawerAmounts = null;
        string errorCode = "";
        string errorMesg = "";
        string updateCashDrawerId;
        bool updateIsSafe;
        decimal updateForcedAmount;
        string updateOverShortCode;
        bool updateClicked;
        bool updateViewClicked;


        public BalanceCash()
        {
            InitializeComponent();
        }

        public bool SafeBalance { get; set; }

        private void BalanceCash_Load(object sender, EventArgs e)
        {
            CashlinxDesktopSession.Instance.SafeBeginningBalance = 0;
            CashlinxDesktopSession.Instance.CashDrawerBeginningBalance = 0;
            labelCashDrawerNumber.Text = SafeBalance ? CashlinxDesktopSession.Instance.StoreSafeName : CashlinxDesktopSession.Instance.BalanceOtherCashDrawerName != string.Empty ? CashlinxDesktopSession.Instance.BalanceOtherCashDrawerName : CashlinxDesktopSession.Instance.CashDrawerName;
            labelDate.Text = ShopDateTime.Instance.ShopDate.ToShortDateString();
            labelTrandate.Text = ShopDateTime.Instance.ShopDate.ToShortDateString();
            tabControl1.SelectedIndex = 0;
            denominationData = new List<string>();
            employeeNumbers = new List<string>();
            shopCashPositionTab = tabControl1.TabPages[2];
            safeCombobox = comboBoxFilter1;
            this.Controls.RemoveByKey("comboBoxFilter1");
            customLabelViewHeading.Location = new Point(380, 8);
            isFormValid = true;

            if (SafeBalance)
            {
                if (tabControl1.TabPages.Count != 3)
                    tabControl1.TabPages.Insert(2, shopCashPositionTab);
                string errorCode;
                string errorMesg;
                bool retValue = ShopCashProcedures.GetShopCashPosition(CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber,
                                                                       CashlinxDesktopSession.Instance,
                                                                       out ShopCashDetails,
                                                                       out errorCode, out errorMesg);

                if (panelInitial.Controls.Count > 0)
                    panelInitial.Controls.RemoveAt(0);
                SafeCurrencyEntry safecurrencyentry = new SafeCurrencyEntry();
                safecurrencyentry.Calculate += currencyEntry1_Calculate;
                safecurrencyentry.OtherTenderClick += new SafeCurrencyEntry.AddOTClick(safecurrencyentry_OtherTenderClick);
                panelInitial.Controls.Add(safecurrencyentry);
                safeCombobox.Location = new Point(280, 8);
                safeCombobox.Visible = true;
                this.tabPageTransactions.Controls.Add(safeCombobox);
                customLabelViewHeading.Location = new Point(241, 8);
                comboBoxFilter2.Visible = false;
            }
            else
            {
                tabControl1.TabPages.RemoveAt(2);
                if (panelInitial.Controls.Count > 0)
                    panelInitial.Controls.RemoveAt(0);
                CurrencyEntry currencyentry = new CurrencyEntry();
                currencyentry.Calculate += currencyEntry1_Calculate;
                currencyentry.OtherTenderClick += new CurrencyEntry.AddOTClick(currencyentry_OtherTenderClick);
                panelInitial.Controls.Add(currencyentry);
            }

            if (CashlinxDesktopSession.Instance.CurrentSiteId.IsTopsSafe)
                panelInitial.Enabled = false;

            tabControl1.TabPages[0].Focus();
            customButtonSubmit.Enabled = false;
            customButtonPrint.Enabled = false;
            customButtonPrintLedger.Enabled = false;
            updateClicked = false;
        }

        void safecurrencyentry_OtherTenderClick()
        {
            panelBalance.Visible = !panelBalance.Visible;
        }


        void currencyentry_OtherTenderClick()
        {
            panelBalance.Visible = !panelBalance.Visible;
        }

        void currencyEntry1_Calculate(decimal currencyTotal)
        {
            customLabelActualCashAmount.Text = string.Format("{0:C}", currencyTotal);
            actualCashAmount = currencyTotal;
            updateClicked = false;
        }

        private void populateCashDrawerDataTable()
        {
            if (CDTransactionsTable == null)
            {
                if (cashdrawerTransactions != null)
                    CDTransactionsTable = cashdrawerTransactions.Clone();
                else if (cashdrawerPDATransactions != null)
                    CDTransactionsTable = cashdrawerPDATransactions.Clone();
                if (CDTransactionsTable != null)
                {
                    CDTransactionsTable.Columns.Add("receiptamount");
                    CDTransactionsTable.Columns.Add("disbursedamount");
                    CDTransactionsTable.Columns.Add("PDL");
                }
            }

            if (cashdrawerTransactions != null && cashdrawerTransactions.Rows.Count > 0)
            {
                foreach (DataRow dr in cashdrawerTransactions.Rows)
                {
                    decimal amount = Utilities.GetDecimalValue(dr["amount"], 0);
                    //The following line is to prevent the duplicate entry that we
                    //insert on a renew and paydown from showing
                    //When we renew/paydown we enter 1 row for the old loan and one row
                    //for the new loan in the receipt detail table
                    if (amount == 0)
                        continue;
                    if (CDTransactionsTable != null)
                    {
                        CDTransactionsTable.ImportRow(dr);
                        CDTransactionsTable.Rows[CDTransactionsTable.Rows.Count - 1]["PDL"] = "N";
                    }
                }
            }
            if (cashdrawerPDATransactions != null && cashdrawerPDATransactions.Rows.Count > 0)
            {
                foreach (DataRow dr in cashdrawerPDATransactions.Rows)
                {
                    if (CDTransactionsTable != null)
                    {
                        CDTransactionsTable.ImportRow(dr);
                        CDTransactionsTable.Rows[CDTransactionsTable.Rows.Count - 1]["PDL"] = "Y";
                    }
                }
            }

            if (!allShopTransactions)
            {
                totDisbursedAmount = 0;
                totReceiptAmount = 0;
            }
            if (CDTransactionsTable != null)
            {
                foreach (DataRow drow in CDTransactionsTable.Rows)
                {
                    if (employeeNumbers.Count > 0)
                    {
                        DataRow dr1 = drow;
                        var emp = (from empno in employeeNumbers
                                   where empno == dr1["empnumber"].ToString()
                                   select empno).FirstOrDefault();
                        if (emp == null)
                            employeeNumbers.Add(drow["empnumber"].ToString());
                    }
                    else
                        employeeNumbers.Add(drow["empnumber"].ToString());
                    string opCode = Utilities.GetStringValue(drow["tendertype"], "");
                    decimal amount = Utilities.GetDecimalValue(drow["amount"], 0);
                    if (amount < 0)
                        amount = -amount;

                    //In the receipt details table the amounts are not stored as positive or negative
                    //The operation code should be used to determine if the amount was given out or money came in
                    decimal receiptAmt = 0.0m;
                    decimal disbursedAmt = 0.0m;
                    if (Commons.IsMoneyInOpCode(opCode))
                    {
                        receiptAmt = amount;
                    }
                    if (Commons.IsMoneyOutOpCode(opCode))
                    {
                        disbursedAmt = amount;
                    }
                    drow.SetField("receiptamount", String.Format("{0:0.00}", receiptAmt));
                    drow.SetField("disbursedamount", String.Format("{0:0.00}", disbursedAmt));
                    totReceiptAmount += receiptAmt;
                    totDisbursedAmount += disbursedAmt;
                    DateTime balDate = Utilities.GetDateTimeValue(drow["transactiontime"], DateTime.MinValue);
                    if (balDate != DateTime.MinValue)
                    {
                        balanceDate = balDate.FormatDate();
                        balanceDateTime = balanceDate + " " + ShopDateTime.Instance.ShopTime.ToString();
                    }
                    if (priorBalance=="Y" && balanceDate == ShopDateTime.Instance.ShopDate.ToShortDateString())
                    {
                        string cdID;
                        if (!SafeBalance)
                        {
                            cdID = string.IsNullOrEmpty(
    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID) ?
    GlobalDataAccessor.Instance.DesktopSession.CashDrawerId :
    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID;
                            
                        }
                        else
                        {
                            cdID = GlobalDataAccessor.Instance.DesktopSession.StoreSafeID;
                            
                        }
                        string lastbalDate;
                        ShopCashProcedures.GetLastBalanceDate(cdID, out lastbalDate);
                        balanceDateTime = lastbalDate + " " + ShopDateTime.Instance.ShopTime.ToString();
                        balanceDate = lastbalDate;
                    }
                    //balanceDate = Utilities.GetStringValue(drow["transactiontime"], "").Substring(1, 10);
                }
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
                    if (!dgvr.IsNewRow)
                        dgvr.Visible = false;
                }
            }
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

        private void customButtonBalance_Click(object sender, EventArgs e)
        {
            //panelInitial.Visible = false;
            //panelBalance.Visible = true;
            //panelBalance.Location = new Point(13, 248);
            //Call the stored procedure to get the actual cash left in the drawer at this time

            if (panelInitial.Enabled)
            {
                denominationData = SafeBalance ?
                                   ((SafeCurrencyEntry)panelInitial.Controls[0]).CurrencyEntryData() :
                                   ((CurrencyEntry)panelInitial.Controls[0]).CurrencyEntryData();
            }
            else
            {
                denominationData.Add("");
            }
            if (updateClicked || (denominationData.Count > 0 && !denominationData[0].Equals(string.Empty)) && (customLabelActualCashAmount.Text.Trim().Count() == 0 || customLabelActualCashAmount.Text.Equals("$0.00")))
            {
                MessageBox.Show("Please click the Calculate button first before clicking Balance button");
                return;
            }

            if (customButtonBalance.Text == "Balance")
            {
                if (!GlobalDataAccessor.Instance.CurrentSiteId.IsTopsSafe)
                    customButtonBalance.Text = "Update";

                panelInitial.Enabled = false;


                cashdrawerAmount = 0.0M;

                if (SafeBalance)
                {
                    if (GlobalDataAccessor.Instance.DesktopSession.ClosedUnverifiedSafe)
                        priorBalance = "Y";
                }
                else
                {
                    if (GlobalDataAccessor.Instance.DesktopSession.ClosedUnverifiedCashDrawer)
                        priorBalance = "Y";
                }
                if (string.IsNullOrEmpty(balanceDate))
                {
                    if (priorBalance == "N")
                    {
                        balanceDate = ShopDateTime.Instance.ShopDate.FormatDate();
                        balanceDateTime = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString();
                    }
                    else
                    {
                        string cdID;
                        
                        if (!SafeBalance)
                        {
                            cdID = string.IsNullOrEmpty(
    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID) ?
    GlobalDataAccessor.Instance.DesktopSession.CashDrawerId :
    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID;

                        }
                        else
                        {
                            cdID = GlobalDataAccessor.Instance.DesktopSession.StoreSafeID;

                        }
                        string balDate;
                        ShopCashProcedures.GetLastBalanceDate(cdID, out balDate);
                        balanceDateTime = balDate + " " + ShopDateTime.Instance.ShopTime.ToString();
                        balanceDate = balDate;

                    }
                }


                customButtonBalance.Enabled = false;
                procMsg = new ProcessingMessage("Balancing Operation is in progress");
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.RunWorkerAsync();
                procMsg.ShowDialog(this);




    

            }
            else
            {
                customButtonSubmit.Enabled = false;
                updateClicked = true;
                //Show manager override if its cash drawer or safe balancing but not trial balancing
                if (!CashlinxDesktopSession.Instance.TrialBalance)
                {
                    List<ManagerOverrideType> overrideTypes = new List<ManagerOverrideType>();
                    List<ManagerOverrideTransactionType> transactionTypes = new List<ManagerOverrideTransactionType>();
                    StringBuilder messageToShow = new StringBuilder();
                    messageToShow.Append("Cash drawer balance is being updated ");
                    overrideTypes.Add(ManagerOverrideType.UPDATE);
                    transactionTypes.Add(ManagerOverrideTransactionType.CD);
                    ManageOverrides overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER)
                    {
                        MessageToShow = messageToShow.ToString(),
                        ManagerOverrideTypes = overrideTypes,
                        ManagerOverrideTransactionTypes = transactionTypes

                    };

                    overrideFrm.ShowDialog();
                    if (!overrideFrm.OverrideAllowed)
                    {
                        MessageBox.Show("Manager override needed to proceed with updating the balance");
                        customButtonSubmit.Enabled = true;
                        updateClicked = false;
                        return;
                    }
                }

                //TODO: an audit event needs to be entered in the database
                customButtonBalance.Text = "Balance";
                panelInitial.Enabled = true;
            }
        }


        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            DialogResult dgr = DialogResult.Retry;
            do
            {
                try
                {

                    //Call to SP to get the list of transactions against the cash drawer
                    string cdId = SafeBalance ? CashlinxDesktopSession.Instance.StoreSafeID : CashlinxDesktopSession.Instance.BalanceOtherCashDrawerID == string.Empty ? CashlinxDesktopSession.Instance.CashDrawerId : CashlinxDesktopSession.Instance.BalanceOtherCashDrawerID;
                    retval = ShopCashProcedures.GetCashDrawerAmount(cdId, balanceDate, priorBalance, CashlinxDesktopSession.Instance,
                                                                    out cashdrawerAmounts, out errorCode, out errorMesg);
                    dgr = !retval ? MessageBox.Show("Failed to get the cash drawer balance. Cash drawer cannot be closed", "Cash Drawer Error", MessageBoxButtons.RetryCancel) : DialogResult.Cancel;
                }
                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException("Failed to get the balance amount in the cash drawer", new ApplicationException(ex.Message));
                }
            } while (dgr == DialogResult.Retry);

        }


        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (retval && cashdrawerAmounts != null && cashdrawerAmounts.Rows.Count > 0)
            {
                //Parse the transactions to add and subtract the amounts based on the
                //Transactions
                cashdrawerAmount = ShopCashProcedures.GetCashDrawerAmount(cashdrawerAmounts);
                GetBeginningBalance();
                customButtonBalance.Enabled = true;
                string actualCash = customLabelActualCashAmount.Text;
                if (actualCash.StartsWith("$"))
                    actualCash = actualCash.Substring(1);

                balanceAmount = Utilities.GetDecimalValue(actualCash, 0) - cashdrawerAmount;
                customLabelEndingCashAmount.Text = string.Format("{0:C}", cashdrawerAmount);
                customLabelCashBalanceAmount.Text = string.Format("{0:C}", balanceAmount);

                if (CashlinxDesktopSession.Instance.CurrentSiteId.IsTopsSafe)
                {
                    customButtonSubmit.Enabled = cashdrawerAmount == 0;
                }
                else
                    customButtonSubmit.Enabled = true;
                customButtonPrint.Enabled = true;
                customButtonPrintLedger.Enabled = true;
            }

        }

        private void GetBeginningBalance()
        {
            //Get the beginning cash amount for the cash drawer
            decimal beginningCashAmount;
            string errorCode;
            string errorMesg;
            string cdId;
            cdId = SafeBalance ? 
                GlobalDataAccessor.Instance.DesktopSession.StoreSafeID : 
                string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID) ? 
                    GlobalDataAccessor.Instance.DesktopSession.CashDrawerId : 
                    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID;

            bool retValue = ShopCashProcedures.GetCashDrawerBeginningAmount(
                cdId, balanceDateTime, priorBalance, GlobalDataAccessor.Instance.DesktopSession,                                                                            
                out beginningCashAmount, out errorCode, out errorMesg);

            if (retValue)
            {
                cashdrawerAmount += beginningCashAmount;
                if (SafeBalance)
                    GlobalDataAccessor.Instance.DesktopSession.SafeBeginningBalance = beginningCashAmount;
                else
                    if (string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID))
                        GlobalDataAccessor.Instance.DesktopSession.CashDrawerBeginningBalance = beginningCashAmount;
                    else
                        GlobalDataAccessor.Instance.DesktopSession.BalanceOtherBegBalance = beginningCashAmount;
            }
            else
            {
                if (SafeBalance)
                    GlobalDataAccessor.Instance.DesktopSession.SafeBeginningBalance = 0;
                else
                    if (string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID))
                        GlobalDataAccessor.Instance.DesktopSession.CashDrawerBeginningBalance = 0;
                    else
                        GlobalDataAccessor.Instance.DesktopSession.BalanceOtherBegBalance = 0;
            }



        }

        private void customButton1Cancel_Click(object sender, EventArgs e)
        {
            CheckFormValid();
            if (!string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID))
                ClearCashDrawerEvent();
            GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID = string.Empty;
            this.Close();
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            this.formValid = true;
            //Check if its the safe
            if (balanceAmount != 0)
            {
                //Get the reason for over or short amount if not already entered
                if (string.IsNullOrEmpty(customTextBoxComment.Text.Trim()))
                {
                    MessageBox.Show("Please enter comment for cash difference and submit");
                    customTextBoxComment.Focus();
                    formValid = false;
                    isFormValid = false;

                }
                else
                {
                    isFormValid = true;
                }
 

            }
            if (formValid)
            {
                SetButtonState(false);
                if (SafeBalance)
                {
                    DataTable storeTransferData;
                    string errorCode;
                    string errorText;
                    if (!GlobalDataAccessor.Instance.DesktopSession.TrialBalance)
                    {

                    //Get the list of pending transfers for the store
                    bool retVal = ShopCashProcedures.GetShopTransfers(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                                      GlobalDataAccessor.Instance.DesktopSession, out storeTransferData, out errorCode, out errorText);
                    if (retVal && storeTransferData != null && storeTransferData.Rows.Count > 0)
                    {
                        //If there are pending transfers ask for confirmation from the user if they wish to proceed
                        DialogResult dgr = MessageBox.Show(Commons.GetMessageString("EM07ShopTransferPending"), "Safe Balance", MessageBoxButtons.YesNo);
                        if (dgr == DialogResult.No)
                        {
                            SetButtonState(true);
                            return;
                        }
                    }
                    string cashDrawerId = GlobalDataAccessor.Instance.DesktopSession.StoreSafeID;

                        if (closedUnverifiedCD.Length > 0)
                        {
                            MessageBox.Show(Commons.GetMessageString("EM05CDClosedUnverified"));
                            customButtonSubmit.Enabled = true;
                            return;
                        }
                        //TODO Future when auditing events are added to check if the safe was balanced previously
                        //on the same day
                        //and if yes, to write a re balance audit event
                        //Set the safe to closed-balanced
                        //Call update_cash_info stored procedure
                        bool updatedCash = false;
                        if (GlobalDataAccessor.Instance.CurrentSiteId.IsTopsSafe)
                            updatedCash = UpdateCashInfo(cashDrawerId, true, 0, "N");
                        else
                        {
                            //decimal overShortAmount = Utilities.GetDecimalValue(cashdrawerAmount - balanceAmount, 0);
                            updatedCash = UpdateCashInfo(cashDrawerId, true, balanceAmount, balanceAmount > 0 ? "O" : balanceAmount == 0 ? "N" : "S");
                        }
                        string cdID = GlobalDataAccessor.Instance.DesktopSession.StoreSafeID;
                        if (updatedCash)
                        {
                            string errorMsg;
                            const int cashDrawerStatus = (int)CASHDRAWERSTATUS.CLOSED_BALANCED;
                            string workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                            bool retValue = ShopCashProcedures.UpdateSafeStatus(cdID, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                                                GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                                                ShopDateTime.Instance.ShopDate.FormatDate(), cashDrawerStatus.ToString(),
                                                                                workstationID, GlobalDataAccessor.Instance.DesktopSession,
                                                                                out errorCode, out errorMsg);
                            if (!retValue)
                            {
                                MessageBox.Show("Safe could not be closed");
                                BasicExceptionHandler.Instance.AddException("Error trying to set the cash drawer " + cdID + " to closed-balanced state", new ApplicationException());
                            }
                            PrintFinalLedger();
                            //trigger shop close events if not balancing for prior day
                            if (priorBalance == "N")
                                ShopCloseActivities();
                            else
                                ShopCloseActivities(true);
                        }
                    }
                   /* else
                    {
                        //Check if the balance exceeds maximum ending balance
                        //call update_cash_info stored procedure
                        bool updatedCash = false;
                        if (GlobalDataAccessor.Instance.CurrentSiteId.IsTopsSafe)
                            updatedCash = UpdateCashInfo(cashDrawerId, true, 0, "N");
                        else
                        {
                            //decimal overShortAmount = Utilities.GetDecimalValue(cashdrawerAmount - balanceAmount, 0);
                            updatedCash = UpdateCashInfo(cashDrawerId, true, balanceAmount, balanceAmount > 0 ? "O" : balanceAmount == 0 ? "N" : "S");
                        }
                        if (!updatedCash)
                        {
                            MessageBox.Show("Cash drawer balance entry could not be written");
                            SetButtonState(true);
                            return;
                        }
                        //Future TODO when auditing is added, to add a rebalance audit event
                    }*/
                }
                else
                {
                    if (balanceAmount != 0)
                    {
                        //Show manager override
                        var overrideTypes = new List<ManagerOverrideType>();
                        var transactionTypes = new List<ManagerOverrideTransactionType>();
                        var messageToShow = new StringBuilder();
                        messageToShow.Append("Cash Drawer is out of balance ");
                        overrideTypes.Add(ManagerOverrideType.OOBAL);
                        transactionTypes.Add(ManagerOverrideTransactionType.CD);
                        var overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER)
                        {
                            MessageToShow = messageToShow.ToString(),
                            ManagerOverrideTypes = overrideTypes,
                            ManagerOverrideTransactionTypes = transactionTypes

                        };

                        overrideFrm.ShowDialog();
                        if (!overrideFrm.OverrideAllowed)
                        {
                            MessageBox.Show("Manager override needed to complete balancing cash drawer");
                            SetButtonState(true);
                            return;
                        }
                    }
                    //Get maximum ending balance value from business rule PWN_BR-94
                    //check if balance is greater than the max ending balance
                    decimal maxEndingBalance = new BusinessRulesProcedures(CashlinxDesktopSession.Instance).GetCashDrawerMaxEndingBalance(CashlinxDesktopSession.Instance.CurrentSiteId);
                    if (maxEndingBalance != 0 && maxEndingBalance < actualCashAmount)
                    {
                        //if yes, show manager override(excess cash)
                        var overrideTypes = new List<ManagerOverrideType>();
                        var transactionTypes = new List<ManagerOverrideTransactionType>();
                        var messageToShow = new StringBuilder();
                        messageToShow.Append("Cash Drawer has excess ending balance. ");
                        overrideTypes.Add(ManagerOverrideType.EXCASH);
                        transactionTypes.Add(ManagerOverrideTransactionType.CD);
                        var overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER)
                        {
                            MessageToShow = messageToShow.ToString(),
                            ManagerOverrideTypes = overrideTypes,
                            ManagerOverrideTransactionTypes = transactionTypes

                        };

                        overrideFrm.ShowDialog();
                        if (!overrideFrm.OverrideAllowed)
                        {
                            MessageBox.Show("Manager override needed to complete balancing cash drawer");
                            SetButtonState(true);
                            return;
                        }
                    }
                    var cdID = string.IsNullOrEmpty(
                        GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID) ? 
                        GlobalDataAccessor.Instance.DesktopSession.CashDrawerId : 
                        GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID;
                    //decimal overShortAmount=Utilities.GetDecimalValue(cashdrawerAmount-balanceAmount,0);
                    //call update_cash_info stored procedure
                    UpdateCashInfo(cdID, false, balanceAmount, balanceAmount > 0 ? "O" : balanceAmount == 0 ? "N" : "S");
                    //set cash drawer to closed balanced

                    string errorMsg;
                    string errorCode;
                    const int cashDrawerStatus = (int)CASHDRAWERSTATUS.CLOSED_BALANCED;
                    var workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                    bool retValue = ShopCashProcedures.UpdateSafeStatus(
                        cdID, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        GlobalDataAccessor.Instance.DesktopSession.FullUserName, 
                        ShopDateTime.Instance.ShopDate.FormatDate(), cashDrawerStatus.ToString(),
                        workstationID, GlobalDataAccessor.Instance.DesktopSession,
                        out errorCode, out errorMsg);
                    if (!retValue)
                    {
                        MessageBox.Show("Cash drawer could not be set to closed-balanced");
                        BasicExceptionHandler.Instance.AddException("Error trying to set the cash drawer " + cdID + " to closed-balanced state", new ApplicationException());
                    }

                    PrintFinalLedger();

                }
                //SetButtonState(true);
                this.Close();
            }
        }

        private void SetButtonState(bool buttonState)
        {
            customButtonSubmit.Enabled = buttonState;
            customButtonBalance.Enabled = buttonState;
            customButtonPrintLedger.Enabled = buttonState;
            customButton1Cancel.Enabled = buttonState;
            customButtonPrint.Enabled = buttonState;
        }

        private bool UpdateCashInfo(string cashDrawerId, bool isSafe, decimal forcedAmount, string overShortCode)
        {
            retval = false;
            procMsg = new ProcessingMessage("Saving Balance data");
            updateCashDrawerId = cashDrawerId;
            updateIsSafe = isSafe;
            updateForcedAmount = forcedAmount;
            updateOverShortCode = overShortCode;
            BackgroundWorker bw1 = new BackgroundWorker();
            bw1.DoWork += new DoWorkEventHandler(bw1_DoWork);
            bw1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw1_RunWorkerCompleted);
            bw1.RunWorkerAsync();
            procMsg.ShowDialog(this);

            return retval;
        }


        void bw1_DoWork(object sender, DoWorkEventArgs e)
        {
            DialogResult dgr = DialogResult.Retry;
            string errorCode;
            string errorText;
            string safeBalance = updateIsSafe ? "Y" : "N";
            retval = ShopCashProcedures.UpdateCashInfo(
                GlobalDataAccessor.Instance.OracleDA,
                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                updateCashDrawerId,
                safeBalance,
                ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                "USD",
                actualCashAmount,
                updateForcedAmount,
                updateOverShortCode,
                GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                customTextBoxComment.Text,
                denominationData,
                out errorCode,
                out errorText);


        }


        void bw1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!retval)
            {
                MessageBox.Show("Balance data could not be set for " + updateCashDrawerId);
                BasicExceptionHandler.Instance.AddException("Error trying to balance " + updateCashDrawerId, new ApplicationException());
            }

        }


        private void PrintFinalLedger()
        {
            if (CDTransactionsTable == null || updateViewClicked)
            {
                string cashDrawerId = SafeBalance ? 
                    GlobalDataAccessor.Instance.DesktopSession.StoreSafeID : 
                    string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID) ? 
                    GlobalDataAccessor.Instance.DesktopSession.CashDrawerId : 
                    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID;
                GetCashDrawerTransactions(cashDrawerId);
            }

            var rptObj = new ReportObject();
            rptObj.ReportImage = Properties.Resources.logo;
            rptObj.ReportNumber = 226;
            rptObj.ReportStoreDesc = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
            rptObj.ReportTitle = "Final Cash Drawer Ledger";
            rptObj.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\Ledger" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
            LedgerReport ledgerRpt = new LedgerReport();
            ledgerRpt.CashDrawerName = SafeBalance ? GlobalDataAccessor.Instance.DesktopSession.StoreSafeName : CashlinxDesktopSession.Instance.BalanceOtherCashDrawerName == string.Empty ? CashlinxDesktopSession.Instance.CashDrawerName : GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerName;
            ledgerRpt.EmployeeName = GlobalDataAccessor.Instance.DesktopSession.UserName;
            ledgerRpt.ReportType = LedgerReportType.Final;
            ledgerRpt.CashDrawerTransactions = CDTransactionsTable;
            ledgerRpt.IsSafe = SafeBalance;
            ledgerRpt.BeginningBalance = SafeBalance ? GlobalDataAccessor.Instance.DesktopSession.SafeBeginningBalance : CashlinxDesktopSession.Instance.BalanceOtherCashDrawerName == string.Empty ? CashlinxDesktopSession.Instance.CashDrawerBeginningBalance : GlobalDataAccessor.Instance.DesktopSession.BalanceOtherBegBalance;
            string actualCash = customLabelActualCashAmount.Text;
            if (actualCash.StartsWith("$"))
                actualCash = actualCash.Substring(1);
            ledgerRpt.ActualCashCount = Utilities.GetDecimalValue(actualCash, 0);
            ledgerRpt.OverShortComment = Utilities.GetStringValue(customTextBoxComment.Text, "");
            ledgerRpt.LedgerDenomination = denominationData;
            var ledgerReport = new CashDrawerLedger();
            ledgerReport.RptObject = rptObj;
            ledgerReport.LedgerReportData = ledgerRpt;
            if (ledgerReport.Print())
                PrintLedger(rptObj.ReportTempFileFullName);
        }

        private void customButtonUpdateView_Click(object sender, EventArgs e)
        {
            updateViewClicked = true;
            if (comboBoxFilter1.Visible)
            {
                if (comboBoxFilter1.SelectedItem.ToString() == "All Shop Transactions")
                {
                    allShopTransactions = true;
                    if (CDTransactionsTable != null)
                        CDTransactionsTable.Rows.Clear();
                    employeeNumbers = new List<string>();
                    //Get the transactions that happened in all the cash drawers in the store for that day
                    var dtCashDrawers = GlobalDataAccessor.Instance.DesktopSession.StoreCashDrawerAssignments;
                    foreach (DataRow dr in dtCashDrawers.Rows)
                    {
                        cashdrawerTransactions = null;
                        cashdrawerPDATransactions = null;
                        string cdID = dr["id"].ToString();
                        GetCashDrawerTransactions(cdID);
                    }
                }
                else if (comboBoxFilter1.SelectedItem.ToString() == "Safe Transactions")
                {
                    employeeNumbers = new List<string>();
                    if (CDTransactionsTable != null)
                        CDTransactionsTable.Rows.Clear();
                    cashdrawerTransactions = null;
                    cashdrawerPDATransactions = null;
                    string cdID = GlobalDataAccessor.Instance.DesktopSession.StoreSafeID;
                    GetCashDrawerTransactions(cdID);
                }
            }
            if (!comboBoxFilter1.Visible)
                allShopTransactions = false;
            var filter2Type = string.Empty;
            if (CDTransactionsTable != null && CDTransactionsTable.Rows.Count > 0)
            {
                _bindingSource1.DataSource = CDTransactionsTable;
                if (comboBoxFilter2.Visible && comboBoxFilter2.SelectedItem != null)
                    filter2Type = comboBoxFilter2.SelectedItem.ToString();
                var filter3Type = string.Empty;
                if (comboBoxFilter3.Visible && comboBoxFilter3.SelectedItem != null)
                    filter3Type = comboBoxFilter3.SelectedItem.ToString();
                if (filter3Type == "All")
                {
                    if (filter2Type == "Product")
                        _bindingSource1.Filter = "PDL='Y' OR PDL = 'N'";
                    else if (filter2Type == "Method of Payment")
                        _bindingSource1.Filter = "method_of_pmt='Cash' OR method_of_pmt ='Check' OR method_of_pmt = 'Credit Card' OR method_of_pmt = 'Debit Card' OR method_of_pmt='Coupon'";
                    else if (filter2Type == "Employee")
                    {
                        _bindingSource1.Filter = null;
                    }
                }
                else if (filter3Type == "PDL")
                    _bindingSource1.Filter = "PDL='Y'";
                else if (filter3Type == "Pawn")
                    _bindingSource1.Filter = "PDL='N'";
                else if (filter3Type == "Cash")
                    _bindingSource1.Filter = "method_of_pmt='Cash'";
                else if (filter3Type == "Check")
                    _bindingSource1.Filter = "method_of_pmt='Check'";
                else if (filter3Type == "Credit Card")
                    _bindingSource1.Filter = "method_of_pmt='Credit Card'";
                else if (filter3Type == "Debit Card")
                    _bindingSource1.Filter = "method_of_pmt='Debit Card'";
                else if (filter3Type == "Coupon")
                    _bindingSource1.Filter = "method_of_pmt='Coupon'";
                else if (filter3Type == string.Empty)
                {
                    if (filter2Type == "Cash Transfer")
                        _bindingSource1.Filter = "tranType = 'Shop To Shop Transfer' OR tranType = 'Bank Transfer' OR tranType = 'Internal Transfer'";
                }
                else
                    _bindingSource1.Filter = "empnumber='" + filter3Type + "'";
            }
            showTransactionsData();
 
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!(SafeBalance))
            {
                if (e.TabPageIndex == 2)
                    e.Cancel = true;
            }
            else
            {
                if (e.TabPageIndex == 2 && !SafeUserValidated)
                {
                    //Check if the logged in user has safe access
                    if (GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSafeAccess)
                        SafeUserValidated = true;
                }
            }
            if (e.TabPageIndex == 1)
            {
                //Load transactions for the cash drawer
                //Get the transactions for the current cash drawer
                //If safe balance is being done,get transactions for all users else get transactions for the current user
                if (SafeBalance)
                {
                    //Trial balance check not needed if they are only looking at transactions
                    /* CheckOpenCashDrawers();
                    if (!trialBalance)
                    {
                    MessageBox.Show("Cannot balance safe when there are open or unverified cash drawers");
                    e.Cancel = true;
                    }*/
                    if (panelInitial.Controls.Count > 0)
                        panelInitial.Controls.RemoveAt(0);
                    var safecurrencyentry = new SafeCurrencyEntry();
                    safecurrencyentry.Calculate += currencyEntry1_Calculate;
                    safecurrencyentry.OtherTenderClick += new SafeCurrencyEntry.AddOTClick(safecurrencyentry_OtherTenderClick);
                    string errorMesg;
                    if (denominationData != null)
                        safecurrencyentry.SetCurrencyData(denominationData, false, out errorMesg);
                    panelInitial.Controls.Add(safecurrencyentry);
                    string cashDrawerId = GlobalDataAccessor.Instance.DesktopSession.StoreSafeID;
                    if (CDTransactionsTable == null)
                        GetCashDrawerTransactions(cashDrawerId);
                }
                else
                {
                    if (panelInitial.Controls.Count > 0)
                        panelInitial.Controls.RemoveAt(0);
                    var currencyentry = new CurrencyEntry();
                    currencyentry.Calculate += currencyEntry1_Calculate;
                    currencyentry.OtherTenderClick += new CurrencyEntry.AddOTClick(currencyentry_OtherTenderClick);
                    string errorMesg;
                    if (denominationData != null)
                        currencyentry.SetCurrencyData(denominationData, false, out errorMesg);
                    panelInitial.Controls.Add(currencyentry);
                    string cashDrawerId = string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID) ? CashlinxDesktopSession.Instance.CashDrawerId : CashlinxDesktopSession.Instance.BalanceOtherCashDrawerID;
                    if (CDTransactionsTable == null)
                        GetCashDrawerTransactions(cashDrawerId);
                }

                _bindingSource1 = new BindingSource();
                showTransactionsData();
                if (CDTransactionsTable == null || CDTransactionsTable.Rows.Count == 0)
                {
                    MessageBox.Show("No Transactions to view for today");
                    //e.Cancel = true;
                }
                else
                    FindNumberOfPages(CDTransactionsTable);
            }
            else if (e.TabPageIndex == 2)
            {
                if (ShopCashDetails != null)
                {
                    bool safe = false;
                    var datatableShopcashPositions = ShopCashDetails.Clone();
                    datatableShopcashPositions.Columns.Add("Status");
                    datatableShopcashPositions.Columns.Add("endingAmount");
                    datatableShopcashPositions.Columns.Add("overShortAmount");

                    foreach (DataRow dr in ShopCashDetails.Rows)
                    {
                        datatableShopcashPositions.ImportRow(dr);
                    }
                    var totalEndingBalance = 0.0M;
                    var totalBeginningBalance = 0.0M;
                    var totalCashOverShort = 0.0M;
                    foreach (DataRow drow in datatableShopcashPositions.Rows)
                    {
                        safe = false;
                        string cdStatus = Utilities.GetStringValue(drow["openflag"], "");
                        if (cdStatus == "3")
                            continue;
                        if (cdStatus == "1")
                            drow.SetField("Status", CASHDRAWERSTATUS.OPEN.ToString());
                        else if (cdStatus == "0")
                            drow.SetField("Status", CASHDRAWERSTATUS.CLOSED_BALANCED.ToString());
                        else if (cdStatus == "2")
                            drow.SetField("Status", CASHDRAWERSTATUS.CLOSED_UNVERIFIED.ToString());
                        decimal beginAmount = Utilities.GetDecimalValue(drow["beginningAmount"], 0);
                        decimal amount = cdStatus == "0" ? Utilities.GetDecimalValue(drow["ending_Amount"], 0) : 0;
                        decimal overshortamount = cdStatus == "0" ? Utilities.GetDecimalValue(drow["over_short_amount"], 0) : 0;
                        drow.SetField("endingAmount", amount);
                        drow.SetField("overShortAmount", overshortamount);
                        totalBeginningBalance += beginAmount;
                        totalEndingBalance += amount;
                        totalCashOverShort += (overshortamount);
                    }

                    _bindingSource2 = new BindingSource();
                    _bindingSource2.DataSource = datatableShopcashPositions;
                    this.dataGridViewCashDrawerStatus.AutoGenerateColumns = false;
                    this.dataGridViewCashDrawerStatus.DataSource = _bindingSource2;

                    this.dataGridViewCashDrawerStatus.Columns[0].DataPropertyName = "cashdrawername";
                    this.dataGridViewCashDrawerStatus.Columns[1].DataPropertyName = "Status";
                    this.dataGridViewCashDrawerStatus.Columns[2].DataPropertyName = "beginningAmount";
                    this.dataGridViewCashDrawerStatus.Columns[3].DataPropertyName = "endingAmount";
                    this.dataGridViewCashDrawerStatus.Columns[4].DataPropertyName = "overShortAmount";
                    labelBegBalanceTotal.Text = string.Format("{0:C}", totalBeginningBalance);
                    labelEndBalanceTotal.Text = string.Format("{0:C}", totalEndingBalance);
                    labelCashBalanceTotal.Text = string.Format("{0:C}", totalCashOverShort);
                }
            }
        }

        private void showTransactionsData()
        {
            if (CDTransactionsTable != null)
            {
                _bindingSource1.DataSource = CDTransactionsTable;
                this.dataGridViewTransactions.AutoGenerateColumns = false;
                this.dataGridViewTransactions.DataSource = _bindingSource1;
                this.dataGridViewTransactions.Columns[0].DataPropertyName = "empnumber";
                this.dataGridViewTransactions.Columns[1].DataPropertyName = "transactiontime";
                this.dataGridViewTransactions.Columns[2].DataPropertyName = "receipt_number";
                this.dataGridViewTransactions.Columns[3].DataPropertyName = "trantype";
                this.dataGridViewTransactions.Columns[4].DataPropertyName = "name";
                this.dataGridViewTransactions.Columns[5].DataPropertyName = "method_of_pmt";
                this.dataGridViewTransactions.Columns[6].DataPropertyName = "receiptamount";
                this.dataGridViewTransactions.Columns[7].DataPropertyName = "disbursedamount";
                customLabelTotReceiptAmt.Text = string.Format("{0:C}", totReceiptAmount);
                customLabelTotDisbursedAmt.Text = string.Format("{0:C}", totDisbursedAmount);
            }
           
        }

        private void GetCashDrawerTransactions(string cashDrawerId)
        {
            Cursor.Current = Cursors.WaitCursor;
            bool retValue = false;
            bool pdaRetValue = false;
            string errorCode;
            string errorMesg;
            priorBalance = "N";
            if (SafeBalance)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.ClosedUnverifiedSafe)
                    priorBalance = "Y";
            }
            else
            {
                if (GlobalDataAccessor.Instance.DesktopSession.ClosedUnverifiedCashDrawer)
                    priorBalance = "Y";
            }
            retValue = ShopCashProcedures.GetPawnCashDrawerTransactions(cashDrawerId,
                                                                        ShopDateTime.Instance.ShopDate.FormatDate(),
                                                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, priorBalance,
                                                                        GlobalDataAccessor.Instance.DesktopSession, out cashdrawerTransactions, out errorCode, out errorMesg);
            if (GlobalDataAccessor.Instance.CurrentSiteId.IsIntegrated)
            {
                pdaRetValue = ShopCashProcedures.GetPdaCashDrawerTransactions(GlobalDataAccessor.Instance.OracleDA,
                                                                              cashDrawerId,
                                                                              ShopDateTime.Instance.ShopDate.FormatDate(),
                                                                              GlobalDataAccessor.Instance.CurrentSiteId.StoreId, priorBalance, out cashdrawerPDATransactions, out errorCode, out errorMesg);
            }

            if ((retValue && cashdrawerTransactions != null) || (pdaRetValue && cashdrawerPDATransactions != null))
            {
                populateCashDrawerDataTable();
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void customButtonPrintLedger_Click(object sender, EventArgs e)
        {
            PrintPrelimLedger();
        }

        private void PrintLedger(string rptFileName)
        {
            const string formName = "memoss.pdf";
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
            {
                string strReturnMessage;
                if (GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                {
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, this, "Printing cash drawer ledger on {0}",
                                                       GlobalDataAccessor.Instance.DesktopSession.LaserPrinter);
                    }
                    strReturnMessage = PrintingUtilities.printDocument(
                        rptFileName,
                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);
                }
                else
                {
                    strReturnMessage = "FAIL - NO VALID LASER PRINTER";
                }

                if (strReturnMessage.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print cash drawer ledger report " + strReturnMessage);
                    }
                }
            }
            //Store cash ledger report
            var cds = GlobalDataAccessor.Instance.DesktopSession;
            if (cds != null)
            {
                var pDoc = new CouchDbUtils.PawnDocInfo();

                //Set document add calls
                pDoc.UseCurrentShopDateTime = true;
                pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                pDoc.DocumentType = Document.DocTypeNames.PDF;
                pDoc.DocFileName = rptFileName;

                //Add this document to the pawn document registry and document storage
                string errText;
                if (!CouchDbUtils.AddPawnDocument(
                    GlobalDataAccessor.Instance.OracleDA, 
                    GlobalDataAccessor.Instance.CouchDBConnector,
                    cds.UserName, ref pDoc, out errText))
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot store cash drawer ledger report!");
                }
            }
            File.Delete(rptFileName);
        }

        private void ShopCloseActivities(bool reportsOnly=false)
        {
            //TODO Set all items with a reason code of suspend and change the reason code to PFI WAIT
            try
            {
                var confRef = SecurityAccessor.Instance.EncryptConfig;
                var clientConfigDB = confRef.GetOracleDBService();

                //Print end of day reports
                var credentials = new Credentials
                {
                    UserName = confRef.DecryptValue(clientConfigDB.DbUser),
                    PassWord = confRef.DecryptValue(clientConfigDB.DbUserPwd),
                    DBHost = confRef.DecryptValue(clientConfigDB.Server),
                    DBPort = confRef.DecryptValue(clientConfigDB.Port),
                    DBService = confRef.DecryptValue(clientConfigDB.AuxInfo),
                    DBSchema = confRef.DecryptValue(clientConfigDB.Schema)
                };
                ExecuteDSTR(credentials, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("DSTR report could not be printed " + ex.Message);
            }
            var errorCode = string.Empty;
            var errorMsg = string.Empty;

 
                //Process Pending transfers
                bool retValue =
                    TransferProcedures.ProcessPendingTransfers(
                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        //ShopDateTime.Instance.ShopDate.ToShortDateString(),
                        balanceDate,
                        out errorMsg);
                if (!retValue)
                {
                    MessageBox.Show("Merchandise transfers partially completed.");
                }
                if (!reportsOnly)
                {
                    //Run the check deposit slip report
                    if (GlobalDataAccessor.Instance.CurrentSiteId.IsIntegrated)
                    {
                        var mcdForm = new ManualCheckDepositSlipPrint();
                        mcdForm.GenerateMCDSlipDocument();


                    }
                }
            
            
            //Run Full Locations Report
            try
            {
                Report.Reports.PrintGunDispositionReport();

                ReportObject ro = new ReportObject();
                ro.ReportNumber = (int)ReportIDs.FullLocationsReport;
                ro.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                ro.ReportStoreDesc = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
                if (priorBalance=="Y")
                    ro.ReportParms.AddRange(new object[] { balanceDate, 
                    balanceDate });
                else
                ro.ReportParms.AddRange(new object[] { ShopDateTime.Instance.ShopDate.FormatDate(), 
                    ShopDateTime.Instance.ShopDate.FormatDate() });
                ro.ReportDetail = string.Empty;
                ro.ReportFilter = string.Empty;

                DataSet outputDataSet = ReportsProcedures.GetCursors(ro);
                ro.FullLocationsData = ReportsProcedures.GetFullLocationData(outputDataSet, ro);

                if (ro.FullLocationsData.Count > 0)
                {
                    ro.CreateTemporaryFullName();
                    ro.ReportTempFileFullName = 
                        SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + ro.ReportTempFileFullName;
                    ro.ReportTitle = "Full Locations";

                    var fullLocationsReport = new FullLocationsReport();
                    fullLocationsReport.reportObject = ro;

                    bool isSuccessful = fullLocationsReport.CreateReport();


                    if (isSuccessful)
                    {
                        //Change to force the Full Locations Report to print
                        if (GlobalDataAccessor.Instance.DesktopSession.PDALaserPrinter.IsValid)
                        {
                            PrintingUtilities.printDocument(
                                ro.ReportTempFileFullName,
                                GlobalDataAccessor.Instance.DesktopSession.PDALaserPrinter.IPAddress,
                                GlobalDataAccessor.Instance.DesktopSession.PDALaserPrinter.Port, 1);
                        }
                        else if (GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                        {
                            PrintingUtilities.printDocument(
                                ro.ReportTempFileFullName,
                                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);
                        }
                        else
                        {
                            MessageBox.Show("No printer available for Full Locations Report\n");
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error printing Full Locations Report " + errorMsg);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error encountered while producing Full Locations Report\n" + ro.ReportError);
                    }


                }
                else
                {
                    MessageBox.Show("Full Locations Report returned no data.");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Full Locations report could not be printed " + ex.Message);
            }

            if (!reportsOnly)
            {
                
                //Run auto forfeiture store credit
                bool retCode = StoreCloseProcedures.AutoForfeitStoreCredit(GlobalDataAccessor.Instance.OracleDA,
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                    GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                    out errorCode,
                    out errorMsg);

                if (!retCode)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error auto forfeiting store credit " + errorMsg);
                    MessageBox.Show("Error auto forfeiting store credit. Please notify support");
                }

                //Run auto release of customer holds
                retCode = HoldsProcedures.ExecuteAutoReleaseHolds(
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    out errorCode, out errorMsg);
                if (!retCode)
                {
                    string errMsg = string.Format("Error auto releasing holds: {0}, {1}", errorCode, errorMsg);
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, errMsg);
                    }
                    MessageBox.Show(errMsg + ": Please notify support.");
                }

                //Set the shop status to closed
                retValue = ShopCashProcedures.CloseShop(
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    GlobalDataAccessor.Instance.DesktopSession,
                    out errorCode, out errorMsg);

                if (!retValue)
                {
                    MessageBox.Show("Error closing store. Store is still Open");
                }
                else
                {
                    MessageBox.Show("Store is closed");
                    //Clear the event in pawn_cashdrawer_event
                    string errCode;
                    string errMesg;
                    string workstationId = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                    ShopCashProcedures.RemoveTellerEvent(GlobalDataAccessor.Instance.DesktopSession.StoreSafeID, workstationId, out errCode, out errMesg);
                    if (errCode != "0")
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Safe balance event could not be removed from pawn_Cashdrawer_event " + errMesg);

                        
                    Application.Exit();
                }
            }

            this.Close();
        }

        public void ExecuteDSTR(Credentials credentials, string storeNumber, bool viewPDF = true)
        {
            var dInitDate = Convert.ToDateTime(balanceDateTime); // Convert.ToDateTime(ShopDateTime.Instance.ShopDate.FormatDate());
            var stoNum = storeNumber;
            var oRptObj = new GetDstrData(dInitDate, stoNum, credentials);
            var dataSets = oRptObj.BuildDataset();
            var fileName = @"dstr_report_" + dInitDate.Ticks + ".pdf";
            string rptDir =
            SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.
            BaseLogPath;
            var pdfRpt = new BuildDstrRpt(stoNum, dInitDate, dataSets, rptDir, fileName);
            pdfRpt.CreateRpt();
            if (pdfRpt.ErrorCode == OK && pdfRpt.ErrorText == OK)
            {
                //Store the report
                var cds = GlobalDataAccessor.Instance.DesktopSession;
                if (cds != null)
                {
                    var pDoc = new CouchDbUtils.PawnDocInfo();

                    //Set document add calls
                    pDoc.UseCurrentShopDateTime = true;
                    pDoc.StoreNumber = cds.CurrentSiteId.StoreNumber;
                    pDoc.DocumentType = Document.DocTypeNames.PDF;
                    pDoc.DocFileName = pdfRpt.ReportDir + @"\" + pdfRpt.RptFileName;

                    //Add this document to the pawn document registry and document storage
                    string errText;
                    if (!CouchDbUtils.AddPawnDocument(
                        GlobalDataAccessor.Instance.OracleDA, 
                        GlobalDataAccessor.Instance.CouchDBConnector,
                        cds.UserName, ref pDoc, out errText))
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot store DSTR!");
                    }
                }
                if (viewPDF)
                {
                    if (ReportProcessing.AdobeReaderOpen() != null)
                    {
                        MessageBox.Show("All open Adobe files will be closed.", "Report Message");
                        ReportProcessing.AdobeReaderOpen().Kill();
                    }

                    DesktopSession.ShowPDFFile(pdfRpt.ReportDir + @"\" + pdfRpt.RptFileName, false);
                }
                else if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                {
                    if (GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                    {
                        PrintingUtilities.printDocument(pdfRpt.ReportDir + @"\" + pdfRpt.RptFileName,
                                                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                                                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);
                    }
                }

            }
            else
            {
                if (pdfRpt.ErrorText.Equals("No records found."))
                {
                    MessageBox.Show("The DSTR could not be generated because there were no records found.", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Could not generate DSTR report: " + "Code: " +
                                 pdfRpt.ErrorCode + ", Reason: " + pdfRpt.ErrorText,
                                 "DSTR Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxFilter1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxFilter2.Visible = true;
            comboBoxFilter3.Visible = false;
            if (comboBoxFilter1.SelectedItem.ToString() == "Safe Transactions")
            {
                comboBoxFilter2.Items.Clear();
                comboBoxFilter2.Items.Add("All");
                comboBoxFilter2.Items.Add("Employee");
            }
            else
            {
                comboBoxFilter2.Items.Clear();
                comboBoxFilter2.Items.Add("Product");
                comboBoxFilter2.Items.Add("Method of Payment");
                comboBoxFilter2.Items.Add("Cash Transfer");
                comboBoxFilter2.Items.Add("Employee");
            }
        }

        private void comboBoxFilter2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxFilter3.Visible = true;
            customButtonUpdateView.Visible = false;
            if (comboBoxFilter2.SelectedItem.ToString() == "Product")
            {
                comboBoxFilter3.Items.Clear();
                comboBoxFilter3.Items.Add("All");
                comboBoxFilter3.Items.Add("Pawn");
                comboBoxFilter3.Items.Add("PDL");
            }
            else if (comboBoxFilter2.SelectedItem.ToString() == "Method of Payment")
            {
                comboBoxFilter3.Items.Clear();
                comboBoxFilter3.Items.Add("All");
                comboBoxFilter3.Items.Add("Cash");
                comboBoxFilter3.Items.Add("Check");
                comboBoxFilter3.Items.Add("Credit Card");
                comboBoxFilter3.Items.Add("Debit Card");
                comboBoxFilter3.Items.Add("Coupon");
            }
            else if (comboBoxFilter2.SelectedItem.ToString() == "Cash Transfer")
            {
                comboBoxFilter3.Visible = false;
                customButtonUpdateView.Visible = true;
            }
            else if (comboBoxFilter2.SelectedItem.ToString() == "Employee")
            {
                comboBoxFilter3.Items.Clear();
                comboBoxFilter3.Items.Add("All");
                foreach (var s in employeeNumbers)
                    comboBoxFilter3.Items.Add(s);
            }
            else if (comboBoxFilter2.SelectedItem.ToString() == "All")
            {
                comboBoxFilter3.Visible = false;
                customButtonUpdateView.Visible = true;
            }
        }

        private void comboBoxFilter3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!customButtonUpdateView.Visible)
                customButtonUpdateView.Visible = true;
        }

        private void customButtonClose_Click(object sender, EventArgs e)
        {
            CheckFormValid();
            if (!string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID))
            ClearCashDrawerEvent();
            GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID = string.Empty;
            this.Close();
        }

        private static void ClearCashDrawerEvent()
        {
            bool retValue;
            string errorCode;
            string errorMesg;
            string workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
            ShopCashProcedures.RemoveTellerEvent(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID, workstationID, out errorCode, out errorMesg);

        }

        private void customButtonPrint_Click(object sender, EventArgs e)
        {
            if (CDTransactionsTable == null)
            {
                string cashDrawerId = SafeBalance ? 
                    GlobalDataAccessor.Instance.DesktopSession.StoreSafeID : 
                    !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID) ? 
                    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID : 
                    GlobalDataAccessor.Instance.DesktopSession.CashDrawerId;
                GetCashDrawerTransactions(cashDrawerId);
            }
            PrintPrelimLedger();
        }

        private void PrintPrelimLedger()
        {
            if (SafeBalance && GlobalDataAccessor.Instance.DesktopSession.SafeBeginningBalance == 0
                 || !SafeBalance && GlobalDataAccessor.Instance.DesktopSession.CashDrawerBeginningBalance == 0)
                GetBeginningBalance();
            var rptObj = new ReportObject();
            rptObj.ReportImage = Properties.Resources.logo;
            rptObj.ReportNumber = 226;
            rptObj.ReportStoreDesc = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
            rptObj.ReportTitle = "Preliminary Cash Drawer Ledger";
            rptObj.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\Ledger" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
            var ledgerRpt = new LedgerReport();
            ledgerRpt.CashDrawerName = SafeBalance ? 
                GlobalDataAccessor.Instance.DesktopSession.StoreSafeName : 
                string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerName) ? 
                GlobalDataAccessor.Instance.DesktopSession.CashDrawerName : 
                GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerName;
            ledgerRpt.EmployeeName = GlobalDataAccessor.Instance.DesktopSession.UserName;
            ledgerRpt.CashDrawerTransactions = CDTransactionsTable;
            ledgerRpt.IsSafe = SafeBalance;
            var ledgerReport = new CashDrawerLedger();
            ledgerReport.RptObject = rptObj;
            string actualCash = customLabelActualCashAmount.Text;
            if (actualCash.StartsWith("$"))
                actualCash = actualCash.Substring(1);
            ledgerRpt.ActualCashCount = Utilities.GetDecimalValue(actualCash, 0);
            ledgerRpt.OverShortComment = Utilities.GetStringValue(customTextBoxComment.Text, "");
            ledgerRpt.BeginningBalance = SafeBalance ? GlobalDataAccessor.Instance.DesktopSession.SafeBeginningBalance : CashlinxDesktopSession.Instance.BalanceOtherCashDrawerID == string.Empty ? GlobalDataAccessor.Instance.DesktopSession.CashDrawerBeginningBalance : GlobalDataAccessor.Instance.DesktopSession.BalanceOtherBegBalance;
            ledgerRpt.LedgerDenomination = denominationData;
            ledgerReport.LedgerReportData = ledgerRpt;
            if (ledgerReport.Print())
                PrintLedger(rptObj.ReportTempFileFullName);
        }

        private void BalanceCash_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !isFormValid;
        }

        private void CheckFormValid()
        {
            if (customButtonBalance.Text == "Update")
            {
                //if the user has clicked balance button that means they know what their cash drawer balance 
                //ought to be so in order to avoid the scenario where the user is trying to just
                //see what their balance ought to be and adjust their cash accordingly at this point
                //if they click exit then the manager ought to know it
                //Hence show manager override
                var overrideTypes = new List<ManagerOverrideType>();
                var transactionTypes = new List<ManagerOverrideTransactionType>();
                var messageToShow = new StringBuilder();
                messageToShow.Append("Cash drawer balance was viewed ");
                overrideTypes.Add(ManagerOverrideType.UPDATE);
                transactionTypes.Add(ManagerOverrideTransactionType.CD);
                var overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER)
                {
                    MessageToShow = messageToShow.ToString(),
                    ManagerOverrideTypes = overrideTypes,
                    ManagerOverrideTransactionTypes = transactionTypes

                };

                overrideFrm.ShowDialog();
                if (!overrideFrm.OverrideAllowed)
                {
                    MessageBox.Show("Manager override needed to proceed with exit");
                    isFormValid = false;
                }
                else
                    isFormValid = true;
            }
            else
                isFormValid = true;
        }
    }
}
