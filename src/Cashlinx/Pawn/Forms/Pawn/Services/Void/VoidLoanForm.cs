/************************************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.Services.Void
 * Class:           VoidLoanForm
 * 
 * Description      Form used to void a loan or service
 * 
 * History
 * 
 * SR 6/1/2010 Added check for making sure converted loans cannot be voided and to show warning message
 * if the loan being voided will reach pfi eligibility date
 * SR 6/2/2010 when exiting the void form clear the customer data
 * Madhu 3/5/2011 BZ # 512 -- Added variables for VoidReason and Void comment
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Products.ManageMultiplePawnItems;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.Void
{
    public partial class VoidLoanForm : CustomBaseForm
    {
        public class LoanVoidDetails
        {
            public object TkNum { set; get; }
            public string TickNum { set; get; }
            public object StNum { set; get; }
            public string StoreNum { set; get; }
            public object OpRf { set; get; }
            public string OpRef { set; get; }
            public object OpCd { set; get; }
            public string OpCode { set; get; }
            public object OpDt { set; get; }
            public DateTime OpDate { set; get; }
            public object OpTim { set; get; }
            public DateTime OpTime { set; get; }
            public object PfiElig { set; get; }
            public DateTime PfiEligDate { set; get; }

            public object RcId { set; get; }
            public Int64 RecId { set; get; }

            public object Amt { set; get; }
            public decimal Amount { set; get; }
            public object DtMd { set; get; }
            public DateTime DateMd { set; get; }
            public object TmMd { set; get; }
            public DateTime TimeMd { set; get; }
            public object HType { set; get; }
            public string HoldType { set; get; }

            public bool OpRefIsLoan { set; get; }
            public bool OpRefIsRollover { set; get; }
            public bool LoanSpecificTime { set; get; }
            public bool GreyedOut { set; get; }
            public string NodeTag { set; get; }
            public bool CanVoid { set; get; }
            public string CantVoidReason { set; get; }
            public bool MarkedForVoid { set; get; }
            public object CreatedByData { set; get; }
            public string CreatedBy { set; get; }
            public string VoidReason { set; get; }
            public string VoidComment { set; get; }
            //Output fields
            public bool VoidSuccess { set; get; }


            public LoanVoidDetails()
            {
            }
        }

        #region Constants
        public static readonly int STORE_NUM_MIN_LENGTH = 3;
        public const long TICKET_NUM_MIN = 1;
        public const long TICKET_NUM_MAX = 999999;
        public const string MAXVOID_TAG = "MAXVOID";
        public const string BADSTORE_TAG = "BADSTORE";
        public const string VALID_TAG = "VALID";
        public const string NOVOID_TAG = "NOVOID";

        #endregion

        #region Fields
        private ProcessingMessage processMsg;
        private Int64 curTicketNumber;
        private string curStoreNumber;
        private bool validTicketNumber;
        private bool validStoreNumber;
        private Int64 maxVoidDays;
        private DateTime maxVoidShopDate;
        private bool pfiRecord;
        private bool forceOverride = false;

        #endregion


        #region Properties
        public string CurStoreNumber
        {
            get
            {
                return (this.curStoreNumber);
            }
        }

        public long CurTicketNumber
        {
            get
            {
                return (this.curTicketNumber);
            }
        }

        public ProcessingMessage ProcessMsg
        {
            get
            {
                return (this.processMsg);
            }
        }

        public DataTable LoanChainData
        {
            set; get;
        }

        public Dictionary<string, List<LoanVoidDetails>> Loans { get; private set; }

        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tktNum"></param>
        /// <param name="stoNum"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private bool executeRetrieveLoanChainWork(string tktNum, string stoNum, out string errorMsg)
        {
            errorMsg = string.Empty;
            bool rt = false;
            Int64 tktVal;
            this.curTicketNumber = 0L;
            this.curStoreNumber = string.Empty;

            if (Int64.TryParse(tktNum, out tktVal))
            {
                this.curTicketNumber = tktVal;
                this.curStoreNumber = stoNum;
                this.processMsg.BringToFront();
                this.processMsg.Activate();
                this.processMsg.Show();
                this.processMsg.Update();
                this.loanChainRetrieveWorker.RunWorkerAsync(this);
                rt = true;
            }

            if (!rt)
            {
                errorMsg = "Could not retrieve loan chain because of a bad ticket number.";
            }

            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool finalizeRetrieveLoanChainWork()
        {
            bool rt = false;
            this.processMsg.Hide();
            this.processMsg.SendToBack();

            //Validate data table
            if (this.LoanChainData != null && 
                this.LoanChainData.IsInitialized &&
                !this.LoanChainData.HasErrors &&
                this.LoanChainData.Rows != null &&
                this.LoanChainData.Rows.Count > 0)
            {
                rt = true;
            }

            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="rolloverOp"></param>
        /// <returns></returns>
        private bool isOpCodeRolloverOrNew(string opCode, out bool rolloverOp)
        {
            rolloverOp = false;
            if (string.IsNullOrEmpty(opCode))
                return (false);

            if (opCode.Equals("New", StringComparison.OrdinalIgnoreCase))
            {
                return (true);
            }
            
            if (opCode.Equals("Paydown", StringComparison.OrdinalIgnoreCase) || 
                opCode.Equals("Renew", StringComparison.OrdinalIgnoreCase))
            {
                rolloverOp = true;
                return (true);
            }

            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dR"></param>
        /// <param name="lvd"></param>
        /// <returns></returns>
        private bool retrieveLoanRowDetails(DataRow dR, out LoanVoidDetails lvd)
        {
            lvd = new LoanVoidDetails
                  {
                      TkNum = dR["ticket_number"],
                      StNum = dR["ref_store"],
                      OpRf  = dR["ref_number"],
                      OpCd  = dR["ref_event"],
                      OpDt  = dR["ref_date"],
                      OpTim = dR["ref_time"],
                      Amt   = dR["ref_amt"],
                      DtMd  = dR["date_made"],
                      TmMd  = dR["time_made"],
                      HType = dR["hold_type"],
                      RcId  = dR["receiptdetail_number"],
                      PfiElig = dR["pfi_elig"],
                      CreatedByData=dR["createdby"]
                  };

            lvd.TickNum  = Utilities.GetStringValue(lvd.TkNum, string.Empty);
            lvd.StoreNum = Utilities.GetStringValue(lvd.StNum, string.Empty);
            lvd.OpRef    = Utilities.GetStringValue(lvd.OpRf, string.Empty);
            lvd.OpCode   = Utilities.GetStringValue(lvd.OpCd, string.Empty);
            lvd.Amount   = Utilities.GetDecimalValue(lvd.Amt, 0.0M);
            lvd.HoldType = Utilities.GetStringValue(lvd.HType, string.Empty);
            lvd.RecId    = Utilities.GetLongValue(lvd.RcId, 0L);
            lvd.PfiEligDate = Utilities.GetDateTimeValue(lvd.PfiElig, DateTime.MaxValue);
            lvd.CreatedBy = Utilities.GetStringValue(lvd.CreatedByData, string.Empty);


            if (lvd.OpDt == null || lvd.OpTim == null || string.IsNullOrEmpty(lvd.TickNum) ||
                string.IsNullOrEmpty(lvd.StoreNum) || string.IsNullOrEmpty(lvd.OpRef) ||
                string.IsNullOrEmpty(lvd.OpCode) || lvd.RecId == 0L)
            {
                lvd.CanVoid = false;
                lvd.CantVoidReason = "Invalid loan transaction data";
                return (false);
            }

            //If op code is new loan, renewal, or paydown, fetch the date made
            //and time made columns
            bool opRfRoll;
            lvd.OpRefIsLoan = this.isOpCodeRolloverOrNew(lvd.OpCode, out opRfRoll);
            lvd.OpRefIsRollover = opRfRoll;
            lvd.CanVoid = true;
            lvd.MarkedForVoid = false;

            //Get op date and time
            lvd.OpDate = (DateTime)lvd.OpDt;
            if (lvd.OpTim.ToString() != string.Empty)
            {
                lvd.OpTime = (DateTime)lvd.OpTim;
            }

            if (lvd.OpRefIsLoan)
            {
                //Check for hold
                if (!string.IsNullOrEmpty(lvd.HoldType))
                {
                    if (!(lvd.HoldType.Equals("CUSTHOLD", StringComparison.OrdinalIgnoreCase) ||
                        lvd.HoldType.Equals("BKHOLD", StringComparison.OrdinalIgnoreCase)))
                    {
                        lvd.CanVoid = false;
                        lvd.GreyedOut = true;
                        lvd.CantVoidReason = "Loan is in void restricted hold status of " +
                                             lvd.HoldType;
                        return (false);
                    }
                }
                //Pull date_made and time_made
               // if (lvd.DtMd != null && lvd.TmMd != null)
                //{
                    lvd.DateMd = (DateTime)lvd.DtMd;
                    lvd.TimeMd = (DateTime)lvd.TmMd;
                    lvd.LoanSpecificTime = true;
                    lvd.GreyedOut = lvd.OpDate.Date <= this.maxVoidShopDate;
                    if (lvd.OpCode.ToString() == "PFI" || lvd.OpCode.ToString() == "PARTP")
                        lvd.GreyedOut = lvd.OpDate.Date != ShopDateTime.Instance.ShopDate;
                    lvd.CanVoid = lvd.GreyedOut == false;
                    if (!lvd.CanVoid)
                    {
                        lvd.CantVoidReason = "Loan exceeds maximum days to void boundary.";
                    }
              /*  }
                else
                {
                    lvd.LoanSpecificTime = false;
                    lvd.GreyedOut = lvd.OpDate.Date <= this.maxVoidShopDate;
                    lvd.CanVoid = lvd.GreyedOut == false;
                    if (!lvd.CanVoid)
                    {
                        lvd.CantVoidReason = "Loan exceeds maximum days to void boundary.";
                    }
                }*/
            }
            else
            {
                lvd.LoanSpecificTime = false;
                lvd.GreyedOut = lvd.OpDate.Date <= this.maxVoidShopDate;
                if (lvd.OpCode.ToString() == "PFI" || lvd.OpCode.ToString() == "PARTP")
                    lvd.GreyedOut = lvd.OpDate.Date != ShopDateTime.Instance.ShopDate;

                lvd.CanVoid = lvd.GreyedOut == false;
                if (!lvd.CanVoid)
                {
                    lvd.CantVoidReason = "Loan exceeds maximum days to void boundary.";
                }
            }
            //SR 5/18/2010 Gray out converted loans
            //Check if this is a converted loan

            if (lvd.CreatedBy == "CONV")
            {
                lvd.GreyedOut = true;
                lvd.CanVoid = false;
                lvd.CantVoidReason = "Converted Loans cannot be voided.";
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        private void populateVoidTreeView()
        {
            //Call to clear and ready tree
            this.voidTreeView.BeginUpdate();
            this.voidTreeView.Nodes.Clear();

            //Compute date prior to which is beyond
            //the max void days
            var curShopDate = ShopDateTime.Instance.ShopDate;
            this.maxVoidShopDate = curShopDate.AddDays(-maxVoidDays).Date;

            //Clear loan records
            if (this.Loans == null)
            {
                this.Loans = new Dictionary<string, List<LoanVoidDetails>>();
            }
            this.Loans.Clear();

            //Pull rows out of data table
            for (int j = 0; j < this.LoanChainData.Rows.Count; ++j)
            {
                DataRow dR = this.LoanChainData.Rows[j];
                if (dR == null) continue;

                LoanVoidDetails lvd;
                if (!this.retrieveLoanRowDetails(dR, out lvd))
                {
                    continue;
                }
                if ((this.LoanChainData.Rows.Count - 1) == j)
                {
                    lvd.CanVoid = true;
                    lvd.CantVoidReason = "";
                }
                else
                {
                    if (lvd.CanVoid)
                    {
                        lvd.CanVoid = false;
                        lvd.CantVoidReason = "This is not the last transaction in the loan chain.";
                    }
                }

                if (lvd.CanVoid   && lvd.OpCode == "New")
                {
                    var mask = ResourceSecurityMask.NONE;
                    lvd.CanVoid = Common.Controllers.Database.Procedures.SecurityProfileProcedures.GetUserResourceAccess("NEWPAWNLOAN", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance, out mask);

                    if (!lvd.CanVoid || (lvd.CanVoid && mask == ResourceSecurityMask.NONE))
                    {
                        lvd.CanVoid = false;
                        lvd.CantVoidReason = "User does not have permissions to void new loan.";
                    }
                    else
                        lvd.CanVoid = true;
                }

                List<LoanVoidDetails> curList;
                if (CollectionUtilities.isNotEmptyContainsKey(this.Loans, lvd.TickNum))
                {
                    curList = this.Loans[lvd.TickNum];
                    curList.Add(lvd);
                    this.Loans[lvd.TickNum] = curList;
                }
                else
                {
                    curList = new List<LoanVoidDetails>(1)
                              {
                                  lvd
                              };
                    this.Loans.Add(lvd.TickNum, curList);
                }
            }

            LoanVoidDetails rollOverPrev = null;
            foreach (string curLoanNum in this.Loans.Keys)
            {
                List<LoanVoidDetails> curLoans = this.Loans[curLoanNum];
                //If current loan number is not valid (no tree started) or the loan number is
                //different from the 
                TreeNode tParNode = null;
                TreeNode childNode = null;
                foreach(LoanVoidDetails curLvd in curLoans)
                {
                    if (curLvd == null) continue;
                    childNode = null;
                    //If we have a loan that is either new or after a rollover,
                    //create a new parent tree node
                    if ((curLvd.OpRefIsLoan && !curLvd.OpRefIsRollover) ||
                        (rollOverPrev != null))
                    {
                        if (rollOverPrev == null)
                        {
                            //Create parent node for loan
                            tParNode = new TreeNode(
                                curLvd.OpCode + " #" +
                                curLvd.TickNum + " " +
                                curLvd.OpDate.FormatDate() + " " +
                                curLvd.Amount.ToString("C") + " (" +
                                curLvd.RecId + ")");
                        }
                        else
                        {
                            tParNode = new TreeNode(
                                rollOverPrev.OpCode + " New #" +
                                curLvd.TickNum + " " +
                                curLvd.OpDate.FormatDate() + " (" +
                                curLvd.RecId + ")");
                            curLvd.Amount = rollOverPrev.Amount;
                            rollOverPrev = null;
                        }
                    }
                    else if (curLvd.OpRefIsLoan && curLvd.OpRefIsRollover)
                    {
                        rollOverPrev = curLvd;
                        continue;
                    }
                    else if (!curLvd.OpRefIsLoan)
                    {
                        childNode = new TreeNode(
                            curLvd.OpCode + " " +
                            curLvd.OpDate.FormatDate() + " " +
                            curLvd.Amount.ToString("C") + " (" +
                                curLvd.RecId + ")");
                    }
                    
                    if (curLvd.GreyedOut)
                    {
                        if (tParNode != null && childNode == null)
                        {
                            curLvd.NodeTag = MAXVOID_TAG;
                            tParNode.Tag = curLvd;
                            tParNode.BackColor = Color.Gray;
                            this.voidTreeView.Nodes.Add(tParNode);
                        }
                        else if (tParNode != null)
                        {
                            curLvd.NodeTag = MAXVOID_TAG;
                            childNode.Tag = curLvd;
                            childNode.BackColor = Color.Gray;
                            tParNode.Nodes.Add(childNode);
                        }
                    }
                    else
                    {
                        if (!curStoreNumber.Equals(curLvd.StoreNum, StringComparison.OrdinalIgnoreCase))
                        {
                            if (tParNode != null && childNode == null)
                            {
                                curLvd.NodeTag = BADSTORE_TAG;
                                curLvd.GreyedOut = true;
                                curLvd.CanVoid = false;
                                curLvd.CantVoidReason = "Cannot void a transaction in a non originating store.";
                                tParNode.Tag = curLvd;
                                tParNode.BackColor = Color.Red;
                                tParNode.ForeColor = Color.White;
                                this.voidTreeView.Nodes.Add(tParNode);
                            }
                            else if (tParNode != null)
                            {
                                curLvd.NodeTag = BADSTORE_TAG;
                                curLvd.GreyedOut = true;
                                curLvd.CanVoid = false;
                                curLvd.CantVoidReason = "Cannot void a transaction in a non originating store.";
                                childNode.Tag = curLvd;
                                childNode.BackColor = Color.Red;
                                childNode.ForeColor = Color.White;
                                tParNode.Nodes.Add(childNode);
                            }
                        }
                        else
                        {
                            if (curLvd.CanVoid)
                            {
                                if (tParNode != null && childNode == null)
                                {
                                    curLvd.NodeTag = VALID_TAG;
                                    tParNode.Tag = curLvd;
                                    tParNode.BackColor = Color.WhiteSmoke;
                                    tParNode.ForeColor = Color.Black;
                                    this.voidTreeView.Nodes.Add(tParNode);
                                }
                                else if (tParNode != null)
                                {
                                    curLvd.NodeTag = VALID_TAG;
                                    childNode.Tag = curLvd;
                                    childNode.BackColor = Color.WhiteSmoke;
                                    childNode.ForeColor = Color.Black;
                                    tParNode.Nodes.Add(childNode);
                                }
                            }
                            else
                            {
                                if (tParNode != null && childNode == null)
                                {
                                    curLvd.NodeTag = NOVOID_TAG;
                                    curLvd.CanVoid = false;
                                    curLvd.CantVoidReason = "You cannot void this transaction.";
                                    tParNode.Tag = curLvd;
                                    tParNode.BackColor = Color.Gray;
                                    this.voidTreeView.Nodes.Add(tParNode);
                                }
                                else if (tParNode != null)
                                {
                                    curLvd.NodeTag = NOVOID_TAG;
                                    curLvd.CanVoid = false;
                                    curLvd.CantVoidReason = "You cannot void this transaction.";
                                    childNode.Tag = curLvd;
                                    childNode.BackColor = Color.Gray;
                                    tParNode.Nodes.Add(childNode);
                                }
                            }
                        }
                    }
                }
 
            }


            this.voidTreeView.EndUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="showFlag"></param>
        /// <param name="text"></param>
        private void showErrorLabel(bool showFlag, string text)
        {
            if (!showFlag)
            {
                this.errorLabel.Text = "";
                this.errorLabel.Hide();
            }
            else
            {
                this.errorLabel.Text = text ?? "No Error";
                this.errorLabel.Show();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        private bool voidFromTreeNode(TreeNode treeNode)
        {
            if (treeNode == null || treeNode.Tag == null)
            {
                return (false);
            }
            bool rt = false;
            //Extract loan void details 
            var lvd = (LoanVoidDetails)treeNode.Tag;
            if (lvd.CanVoid == false)
            {
                    this.showErrorLabel(true, "Cannot void this transaction: " + lvd.CantVoidReason);

                return (false);
            }
            if (ShopDateTime.Instance.ShopDate >= lvd.PfiEligDate)
                MessageBox.Show(@"Voiding this transaction makes the item (s) associated eligible for PFI .");
#if DEBUG
            debugLabel.Text = lvd.OpCode ?? "null";
            debugLabel.Enabled = true;
            debugLabel.Show();
#endif
            //(1st case)If node has no children
            //treeNode has not been serviced
            //Can proceed with loan void event
            if (treeNode.Nodes.Count <= 0 && treeNode.NextNode == null)
            {
                DialogResult dR = DialogResult.Yes;
                while (dR != DialogResult.No)
                {
                    string errCode;
                    string errText;
                    var success = VoidProcedures.PerformVoid(lvd, out errCode, out errText);

                    if (success)
                    {
                        MessageBox.Show("Void successful of " + treeNode.Text, "Void Success");
                        rt = true;
                        break;
                    }
                    //SR 08/31/2011 Added the 2 error codes that will be returned by the stored procedure if the
                    //ticket that we are trying to void is not in the same status as it was when it was pulled in for void 
                    //in this workstation
                    if (errCode == "97" || errCode == "98")
                    {
                        dR = MessageBox.Show("Void failed: Ticket not in the correct status to void", "Void Failure",
                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    dR = MessageBox.Show("Void failed: " + errCode + ", " +
                                         errText + System.Environment.NewLine +
                                         "Would you like to retry?", "Void Failure", 
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                }
            }
            //(2nd case)If node has children and no younger siblings
            //treeNode has not been rolled over but has been serviced
            //Must select later loan op
            else if (treeNode.Nodes.Count > 0 || 
                     treeNode.NextNode != null)
            {
                MessageBox.Show("Cannot void a loan service that is not at the end of the loan chain.  Please reselect.");
                rt = false;
            }

            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curNode"></param>
        private void handleTreeSelectionChange(TreeNode curNode)
        {
            //Verify that the current node is valid
            if (curNode == null)
                return;

            //Acquire the node tag
            //string nodeTag = (curNode.Tag != null) ? (string)curNode.Tag : string.Empty;
            var lvd = (LoanVoidDetails)curNode.Tag;
            this.showErrorLabel(false, null);
            this.voidButton.Enabled = false;

            //Ensure the node tag is marked as valid
            if (lvd.NodeTag == VALID_TAG)
            {
                this.voidButton.Enabled = true;
                this.voidButton.Update();
            }
            else
            {
                if (lvd.NodeTag == BADSTORE_TAG)
                {
                    //Print message regarding inability to select to
                    //void a transaction from a different store
                    this.showErrorLabel(true, "You cannot void a transaction that " +
                                           System.Environment.NewLine +
                                           "occurred in a different store");
                }
                else if (lvd.NodeTag == MAXVOID_TAG)
                {
                    //Print message regarding inability to select to
                    //void a transaction that exceeds the max void days 
                    //limit
                    this.showErrorLabel(true, "You cannot void a transaction that " +
                                           System.Environment.NewLine +
                                           "exceeds the maximum days allowed.");
                }
                else if (lvd.NodeTag == NOVOID_TAG)
                {
                    //Print general message regarding inability to
                    //void this transaction
                    this.showErrorLabel(true, "You cannot void this transaction.");
                }
            }
            this.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sBarCodeData"></param>
        private void ScanParse(string sBarCodeData)
        {
            if (sBarCodeData == "" || sBarCodeData.Length < 5)
                return;

            try
            {

                this.storeNumberTextBox.Text = sBarCodeData.Substring(0, 5);
                if (sBarCodeData.Length == 5)
                {
                    ticketNumberTextBox.Focus();
                    ticketNumberTextBox.SelectionStart = 0;
                    return;
                }
                ticketNumberTextBox.Text = sBarCodeData.Substring(5);
            }
            catch(Exception ex)
            {
                return;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public VoidLoanForm(bool hasResource)
        {
            this.validTicketNumber = false;
            this.validStoreNumber = false;
            this.LoanChainData = null;
            this.Loans = new Dictionary<string, List<LoanVoidDetails>>();
            this.processMsg = new ProcessingMessage("Retrieving Loan Chain")
                              {
                                  Visible = false
                              };

            this.forceOverride = !hasResource; // if user doesn't have resource force the override

            InitializeComponent();
        }
        #endregion

        #region UI Event Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VoidLoanForm_Load(object sender, EventArgs e)
        {
            //Set store number to current site store number
            this.storeNumberTextBox.Text = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

            //Disable void button
            this.voidButton.Enabled = false;

            //Disable void tree control
            this.voidTreeView.Enabled = false;

            //Hide error message
            this.showErrorLabel(false, null);

            //Initialize max void days
            this.maxVoidDays = 0L;
            if (!new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxVoidDays(GlobalDataAccessor.Instance.CurrentSiteId,
                out this.maxVoidDays))
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, 
                        "Cannot retrieve maximum void days. Defaulting to {0}", maxVoidDays);
                }
            }

            //Update form
            this.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitTicketButton_Click(object sender, EventArgs e)
        {
            //Clear validation flags
            this.validTicketNumber = false;
            this.validStoreNumber = false;
            this.showErrorLabel(false, null);

            //In the instance that a user looks up another number, clear
            //the tree first.  -- TLR 7/9/2010
            this.voidTreeView.Nodes.Clear();

            Int64 ticketNum = 0L;
            string storeNum = string.Empty;

            //Validate the text boxes
            if (this.ticketNumberTextBox.isValid)
            {
                Int64 tickNum;
                if (Int64.TryParse(this.ticketNumberTextBox.Text, out tickNum))
                {
                    if (tickNum >= TICKET_NUM_MIN && tickNum <= TICKET_NUM_MAX)
                    {
                        this.validTicketNumber = true;
                        ticketNum = tickNum;
                    }
                }
            }

            if (this.storeNumberTextBox.isValid)
            {
                Int64 storNum;
                string stoNumTrim = this.storeNumberTextBox.Text;
                if (!string.IsNullOrEmpty(stoNumTrim))
                {
                    stoNumTrim = stoNumTrim.TrimStart('0');
                    if (Int64.TryParse(stoNumTrim, out storNum))
                    {
                        this.validStoreNumber = true;
                        storeNum = this.storeNumberTextBox.Text;
                    }
                }
            }

            if (this.validTicketNumber && this.validStoreNumber)
            {
                //TLR - 7/9/2010
                //Ability to look up additional tickets need to be present.  
                //
                //Disable submit button
                //this.submitTicketButton.Enabled = false;
                //this.ticketNumberTextBox.Enabled = false;
                //this.storeNumberTextBox.Enabled = false;
                //this.ticketNumberTextBox.Update();
                //this.storeNumberTextBox.Update();
                //this.submitTicketButton.Update();

                //Get store number
                if (!storeNum.Equals(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    StringComparison.OrdinalIgnoreCase))
                {
                    this.showErrorLabel(true, "The store number entered must match the current store number.");
                    return;
                }

                string errMsg;
                this.LoanChainData = null;
                if (!this.executeRetrieveLoanChainWork(ticketNum.ToString(), storeNum, out errMsg))
                {
                    this.processMsg.Hide();
                    this.showErrorLabel(true, "Could not retrieve loan chain: " + errMsg);
                    this.submitTicketButton.Enabled = true;
                    this.ticketNumberTextBox.Enabled = true;
                    this.storeNumberTextBox.Enabled = true;
                    this.ticketNumberTextBox.Update();
                    this.storeNumberTextBox.Update();
                    this.submitTicketButton.Update();
                }
            }
            else if (!this.validTicketNumber && this.validStoreNumber)
            {
                this.showErrorLabel(true, "You must enter a valid ticket number.");
                this.submitTicketButton.Enabled = true;
                this.ticketNumberTextBox.Enabled = true;
                this.storeNumberTextBox.Enabled = true;
                this.ticketNumberTextBox.Update();
                this.storeNumberTextBox.Update();
                this.submitTicketButton.Update();
                return;
            }
            else if (this.validTicketNumber && !this.validStoreNumber)
            {
                this.showErrorLabel(true, "You must enter a valid store number.");
                this.submitTicketButton.Enabled = true;
                this.ticketNumberTextBox.Enabled = true;
                this.storeNumberTextBox.Enabled = true;
                this.ticketNumberTextBox.Update();
                this.storeNumberTextBox.Update();
                this.submitTicketButton.Update();
                return;
            }
            else
            {
                this.showErrorLabel(true, "You must enter a valid ticket number and a valid store number.");
                this.submitTicketButton.Enabled = true;
                this.ticketNumberTextBox.Enabled = true;
                this.storeNumberTextBox.Enabled = true;
                this.ticketNumberTextBox.Update();
                this.storeNumberTextBox.Update();
                this.submitTicketButton.Update();
                return;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void voidButton_Click(object sender, EventArgs e)
        {
            //Acquire loan event that must be voided
            TreeNode selectedNode = this.voidTreeView.SelectedNode;
            this.showErrorLabel(false, null);
            if (selectedNode == null)
            {
                //Must select a node error message
                this.showErrorLabel(true, "Please select a valid service entry to void.");
                return;
            }

            if (!this.voidFromTreeNode(selectedNode))
            {
                //Cannot complete void, please reselect
                this.showErrorLabel(true, "The selected service cannot be voided. Please select another entry.");
                return;
            }

            //Clear the form
            this.voidTreeView.BeginUpdate();
            this.voidTreeView.Nodes.Clear();
            this.voidTreeView.EndUpdate();

            //Clear the data set
            this.LoanChainData.Dispose();
            this.showErrorLabel(false, null);


            //Reset the form
            this.voidButton.Enabled = false;
            this.submitTicketButton.Enabled = true;
            this.ticketNumberTextBox.Text = "";
            this.ticketNumberTextBox.Enabled = true;
            this.storeNumberTextBox.Enabled = true;
            this.ticketNumberTextBox.Focus();
            this.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageBox.Show("Are you sure you want to exit the Void Screen?",
                                              "Void Screen Message", MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question,
                                              MessageBoxDefaultButton.Button2);
            if (dR == DialogResult.Yes)
            {
                GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
                this.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loanChainRetrieveWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string errorCode;
            string errorText;
            var curFm = (VoidLoanForm)e.Argument;
            DataTable dT;

            if (VoidProcedures.ExecuteGetLoanChain(curFm.CurStoreNumber, curFm.CurTicketNumber, 
                out dT, out errorCode, out errorText))
            {
                curFm.LoanChainData = dT;
                if (curFm.LoanChainData.Rows == null || curFm.LoanChainData.Rows.Count <= 0)
                {
                    curFm.showErrorLabel(true, "No loans found matching input store number and ticket number");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loanChainRetrieveWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!this.finalizeRetrieveLoanChainWork())
            {
                this.submitTicketButton.Enabled = true;
                this.ticketNumberTextBox.Enabled = true;
                this.storeNumberTextBox.Enabled = true;
                this.showErrorLabel(true, "No loans found matching input store number and ticket number");
                this.Update();
                return;
            }
            //Enable loan tree and populate
            this.voidTreeView.Enabled = true;
            this.populateVoidTreeView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void voidTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e != null)
            {
                this.handleTreeSelectionChange(e.Node);
            }
        }

        private void ticketNumberTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void storeNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            
            if (storeNumberTextBox.Text != "")
            {
                ScanParse(storeNumberTextBox.Text);
            }
        }

        #endregion
    }
}
