/**************************************************************************
* PFI_Verify.cs 
* 
* History:
*  no ticket SMurphy 5/6/2010 issues with PFI Merge, PFI Add and navigation
*  Madhu 11/17/2010 fix for defect PWNU00001458
*  Madhu 11/18/2010 fix for defect PWNU00001443
* 
**************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Loan;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.PFI
{
    public partial class PFI_Verify : Form
    {
        private List<string> _AssignmentTypeValues;     // Stores list of enum value Assignment Types
        private decimal _CostDifference;                // Cost Differential between Cost and Loan Amount
        private bool _Setup;                     // Boolean to ensure DataGrid Events dont fire off during initial Setup
        private bool _SetupInitialComplete;
        private int _IcnYear;                   // Used for all record changes
        private PageState _PageState;                 // Keep if Page State is Verify or Merge
        private PFI_ProductData _PfiActiveLoan;             // Current PFI Loan being worked on
        private Queue<int> _SelectedMergeRowIndex;     // First Index during Merge Process
        private int _SelectedRowIndex;          // Current selected row of DataGrid
        public NavBox NavControlBox;
        private bool pfiVerifyPurchase;

        private enum PageState
        {
            Verify,
            Merge
        }

        public PFI_Verify()
        {
            _Setup = true;
            InitializeComponent();
            Setup();
            this.NavControlBox = new NavBox();
            this.NavControlBox.Owner = this;
        }

        private void Setup()
        {
            _PageState = PageState.Verify;
            PageStateButtons();

            undoButton.Enabled = false;
            continueButton.Enabled = false;
            asOfLabel.Visible = false;
            infoPanel.Visible = false;
            variancePanel.Visible = false;
            gvLoanItems.Visible = false;

            PageSize(true);

            // Populate DataGrid Combo Box Assignment Types from Enum
            _AssignmentTypeValues = new List<string>();
            foreach (PfiAssignment myEnum in Enum.GetValues(typeof(PfiAssignment)))
            {
                _AssignmentTypeValues.Add(myEnum.ToString());
            }

            DataGridViewComboBoxColumn myCombo = (DataGridViewComboBoxColumn)gvLoanItems.Columns[colAssignmentType.Name];
            myCombo.Items.AddRange(_AssignmentTypeValues.ToArray());

            titleLabel.Text = "PFI Verify/Edit ";
        }

        // Resizes page for Ticket # Search or PFI Verify/Merge Page
        private void PageSize(bool bSearchBox)
        {
            if (bSearchBox)
            {
                this.Width = 335;
                this.Height = 170;
                cancelButton.Location = new Point(120, 125);
                titleLabel.Location = new Point(18, 2);

                customerNameLabel.Text = "";
                infoPanel.Visible = false;
                variancePanel.Visible = false;
                gvLoanItems.Visible = false;

                //Madhu 11/17/2010 fix for defect PWNU00001458
                if (radioButtonPurchase.Checked)
                {
                    radioButtonPurchase.Checked = true;
                }
                else
                {
                    radioButtonLoan.Checked = true;
                }

                ticketNumber.Select();
            }
            else
            {
                this.Width = 780;
                this.Height = 524;
                cancelButton.Location = new Point(13, 475);
                titleLabel.Location = new Point(18, 29);

                infoPanel.Visible = true;
                variancePanel.Visible = true;
                gvLoanItems.Visible = true;
            }
            this.CenterToScreen();
        }

        private void PFI_SelectLoan_Load(object sender, EventArgs e)
        {
        }

        private void PageStateButtons()
        {
            if (_PageState == PageState.Verify)
            {
                titleLabel.Text = "PFI Verify/Edit ";

                gvLoanItems.Columns[colVerify.Name].Visible = true;
                gvLoanItems.Columns[colMerge.Name].Visible = false;
                backButton.Visible = true;
                suspendButton.Visible = true;
                addNewItemButton.Visible = true;
                undoButton.Visible = true;
                mergeButton.Visible = true;

                int iRowCount = 0;

                for (int i = 0; i < gvLoanItems.Rows.Count; i++)
                {
                    gvLoanItems.Rows[i].DefaultCellStyle.BackColor = Color.White;

                    if (Utilities.GetBooleanValue(gvLoanItems.Rows[i].Cells[colVerify.Name].Value, false))
                        iRowCount++;
                    else if (Utilities.GetStringValue(gvLoanItems.Rows[i].Cells[colStatus.Name].Value, "") == ProductStatus.RET.ToString())
                        iRowCount++;
                    else if (Utilities.GetStringValue(gvLoanItems.Rows[i].Cells[colStatus.Name].Value, "") == ProductStatus.PS.ToString())
                        iRowCount++;
                }
                continueButton.Enabled = (iRowCount == gvLoanItems.Rows.Count && _CostDifference == 0);
            }
            else
            {
                titleLabel.Text = "PFI Merge";

                gvLoanItems.Columns[colVerify.Name].Visible = false;
                gvLoanItems.Columns[colMerge.Name].Visible = true;
                backButton.Visible = false;
                suspendButton.Visible = false;
                addNewItemButton.Visible = false;
                undoButton.Visible = false;
                mergeButton.Visible = false;

                int iRowCount = 0;
                foreach (DataGridViewRow myRow in gvLoanItems.Rows)
                {
                    if (Utilities.GetBooleanValue(myRow.Cells[colMerge.Name].Value, false))
                        iRowCount++;
                    else if (Utilities.GetStringValue(myRow.Cells[colStatus.Name].Value, "") == ProductStatus.RET.ToString())
                        iRowCount++;
                    else if (Utilities.GetStringValue(myRow.Cells[colStatus.Name].Value, "") == ProductStatus.PS.ToString())
                        iRowCount++;
                }
                continueButton.Enabled = iRowCount > 1;
            }
        }

        private void UpdateDataGrid(string sCustomerName)
        {
            _Setup = true;

            decimal dPawnItemCost = 0;

            customerNameLabel.Text = sCustomerName;
            eligibilityDateLabel.Text = _PfiActiveLoan.UpdatedObject.PfiEligible.ToShortDateString();
            if (_PfiActiveLoan.UpdatedObject.GetType() == typeof(PawnLoan))
                extendLabel.Text = ((PawnLoan)_PfiActiveLoan.UpdatedObject).IsExtensionAllowed ? "Yes" : "No";
            else
                extendLabel.Text = "";
            loanNumberLabel.Text = _PfiActiveLoan.UpdatedObject.TicketNumber.ToString();
            typeLabel.Text = _PfiActiveLoan.UpdatedObject.GetType() == typeof(PawnLoan) ? "LOAN" : "BUY";
            mdseLabel.Text = MerchandiseType(_PfiActiveLoan.UpdatedObject);

            gvLoanItems.Rows.Clear();

            int iRowCount = 0;

            if (_PfiActiveLoan.UpdatedObject.Items != null)
            {
                for (int i = 0; i < _PfiActiveLoan.UpdatedObject.Items.Count(); i++)
                {
                    Item pi = _PfiActiveLoan.UpdatedObject.Items[i];
                    int iRowIdx = gvLoanItems.Rows.Add();
                    DataGridViewRow myRow = gvLoanItems.Rows[iRowIdx];
                    PfiAssignment pfiAssignment;
                    if (pi.IsJewelry)
                    {
                        if ((pi.PfiAssignmentType == PfiAssignment.Refurb
                             && pi.PfiAssignmentType != PfiAssignment.CAF
                             && pi.PfiAssignmentType != PfiAssignment.Scrap
                        ))
                            pfiAssignment = pi.PfiAssignmentType;
                        else
                            pfiAssignment = PfiAssignment.Scrap;
                    }
                    else
                    {
                        if (pi.PfiAssignmentType == PfiAssignment.CAF || !_SetupInitialComplete)
                            pfiAssignment = PfiAssignment.Normal;
                        else
                            pfiAssignment = pi.PfiAssignmentType;
                    }

                    dPawnItemCost += pi.ItemAmount;
                    myRow.Cells[colVerify.Name].Value = pi.PfiVerified;
                    myRow.Cells[colCost.Name].Value = String.Format("{0:C}", pi.ItemAmount);
                    myRow.Cells[colDescription.Name].Value = "[" + (iRowIdx + 1).ToString() + "] " + pi.TicketDescription;
                    myRow.Cells[colAssignmentType.Name].Value = pfiAssignment.ToString();
                    myRow.Cells[colRetail.Name].Value = String.Format("{0:C}", pi.RetailPrice);
                    myRow.Cells[colStatus.Name].Value = pi.ItemStatus.ToString();
                    if (pi.ItemStatus.ToString() == ProductStatus.RET.ToString())
                    {
                        myRow.ReadOnly = true;
                        iRowCount++;
                    }

                    ItemReason pawnItemStatus = pi.ItemReason;
                    if (pi.HoldType == "2")
                    {
                        pawnItemStatus = ItemReason.HPFI;
                    }

                    ItemReasonCode reasonCode = ItemReasonFactory.Instance.FindByReason(pawnItemStatus);

                    myRow.Cells[colReason.Name].Value = reasonCode.Description;

                    if (reasonCode.Reason == ItemReason.HPFI)
                    {
                        myRow.Cells[colAssignmentType.Name].ReadOnly = true;
                        myRow.Cells[colReason.Name].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        myRow.Cells[colAssignmentType.Name].ReadOnly = false;
                        myRow.Cells[colReason.Name].Style.ForeColor = Color.Black;
                    }

                    myRow.Cells[colTags.Name].Value = pi.PfiTags;

                    if (i >= _PfiActiveLoan.OriginalObject.Items.Count())
                    {
                        pi.mItemOrder = i + 1;
                        pi.Icn = Utilities.IcnGenerator(
                            pi.mStore,
                            _IcnYear,
                            _PfiActiveLoan.UpdatedObject.GetType() == typeof(PawnLoan) ? ((PawnLoan)_PfiActiveLoan.UpdatedObject).OrigTicketNumber : _PfiActiveLoan.UpdatedObject.TicketNumber,
                            _PfiActiveLoan.UpdatedObject.GetType() == typeof(PawnLoan) ? "1" : "2",
                            pi.mItemOrder,
                            0);
                    }

                    if (pi.PfiVerified)
                        iRowCount++;
                }
                _SetupInitialComplete = true;
            }

            decimal currentLoanAmount = _PfiActiveLoan.UpdatedObject.Amount;
            if (_PfiActiveLoan.UpdatedObject.GetType() == typeof(PawnLoan))
            {
                if (((PawnLoan)_PfiActiveLoan.UpdatedObject).PartialPaymentPaid)
                {
                    currentLoanAmount = ((PawnLoan)_PfiActiveLoan.UpdatedObject).CurrentPrincipalAmount;
                }
            }
            _CostDifference = dPawnItemCost - currentLoanAmount;
            continueButton.Enabled = (iRowCount == gvLoanItems.Rows.Count && _CostDifference == 0);

            costAmountLabel.Text = String.Format("{0:C}", dPawnItemCost);
            differenceLabel.Text = String.Format("{0:C}", _CostDifference);
           
            if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.CurrentSiteId))
            {

                label5.Text = "Current Principal Amount:";
                loanAmountLabel.Text = String.Format("{0:C}", currentLoanAmount);

            }
            else
            {
                label5.Text = "Loan Amount:";
                loanAmountLabel.Text = String.Format("{0:C}", currentLoanAmount);

            }
            

            _Setup = false;
        }

        private string MerchandiseType(CustomerProductDataVO pawnLoan)
        {
            var sMerchandiseType = string.Empty;
            int iGeneralPass = 0;
            int iJewelryPass = 0;

            foreach (Item pawnItem in pawnLoan.Items)
            {
                string sCategoryCodePrefix = pawnItem.CategoryCode.ToString().Substring(0, 1);
                iGeneralPass += sCategoryCodePrefix == "1" ? 0 : 1;
                iJewelryPass += sCategoryCodePrefix == "1" ? 1 : 0;
            }

            if (iGeneralPass > 0 && iJewelryPass > 0)
                sMerchandiseType = "Both";
            else if (iGeneralPass > 0)
                sMerchandiseType = "General";
            else
                sMerchandiseType = "Jewelry";

            return sMerchandiseType;
        }

        private bool GetProductData(int iTicketNumber, StateStatus stateStatus, out PFI_ProductData pfiPawnLoan, out string sCustomerName)
        {
            // Check Suspend List first.  If it exist, must do suspended Loan first
            string sErrorCode = "0";
            var sErrorText = string.Empty;
            var sFirstName = string.Empty;
            var sMiddleName = string.Empty;
            var sLastName = string.Empty;

            PawnLoan pawnLoan = new PawnLoan();
            PurchaseVO purchaseObj = new PurchaseVO();
            PawnAppVO pawnAppVO = new PawnAppVO();
            CustomerVO customerVO = new CustomerVO();
            pfiPawnLoan = new PFI_ProductData();

            sCustomerName = "";

            // Check to see if the Ticket Number is on the PFI Suspended List.  If so, use the record
            bool bSuspendedData = GetSuspendData(iTicketNumber, stateStatus, ref pfiPawnLoan);
            string tenderType;
            if (!bSuspendedData)
            {
                // Ticket Number was not on PFI Suspended List.
                //Get pawn loan data if this is a loan pfi
                bool retValue;
                if (!pfiVerifyPurchase)
                {
                    retValue = CustomerLoans.GetPawnLoan(GlobalDataAccessor.Instance.DesktopSession, 
                        Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                                         iTicketNumber,
                                                         "0",
                                                         StateStatus.BLNK,
                                                         true,
                                                         out pawnLoan,
                                                         out pawnAppVO,
                                                         out customerVO,
                                                         out sErrorCode,
                                                         out sErrorText);
                }
                else
                {
                    retValue = PurchaseProcedures.GetPurchaseData(Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                                                  iTicketNumber,
                                                                  "0",
                                                                  StateStatus.BLNK,
                                                                  "",
                                                                  true,
                                                                  out purchaseObj,
                                                                  out customerVO,
                                                                  out tenderType,
                                                                  out sErrorCode,
                                                                  out sErrorText);
                }

                if (retValue)
                {
                    sCustomerName = customerVO.LastName + ", ";
                    if (customerVO.FirstName != "")
                        sCustomerName += customerVO.FirstName + " ";
                    if (customerVO.MiddleInitial != "")
                        sCustomerName += customerVO.MiddleInitial;
                    sCustomerName = sCustomerName.Replace("  ", " ");

                    if (!pfiVerifyPurchase)
                    {
                        pawnLoan.CustomerNumber = customerVO.CustomerNumber;
                        pfiPawnLoan = new PFI_ProductData();
                        pfiPawnLoan.OriginalObject = pawnLoan;
                        pfiPawnLoan.UpdatedObject = Utilities.CloneObject<PawnLoan>(pawnLoan);
                        pfiPawnLoan.MergedItems = new List<PFI_Merged>();
                        int iDx = -1;
                        if (GlobalDataAccessor.Instance.DesktopSession.PawnLoans != null)
                        {
                            iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans.FindIndex(
                                pl => pl.TicketNumber == pawnLoan.TicketNumber);
                        }
                        else
                            GlobalDataAccessor.Instance.DesktopSession.PawnLoans = new List<PawnLoan>();
                        if (iDx < 0)
                            GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Add(pawnLoan);
                    }
                    else
                    {
                        purchaseObj.CustomerNumber = customerVO.CustomerNumber;
                        pfiPawnLoan = new PFI_ProductData();
                        pfiPawnLoan.OriginalObject = purchaseObj;
                        pfiPawnLoan.UpdatedObject = Utilities.CloneObject<PurchaseVO>(purchaseObj);
                        pfiPawnLoan.MergedItems = new List<PFI_Merged>();

                        int iDx = -1;
                        if (GlobalDataAccessor.Instance.DesktopSession.Purchases != null)
                        {
                            iDx = GlobalDataAccessor.Instance.DesktopSession.Purchases.FindIndex(
                                pl => pl.TicketNumber == purchaseObj.TicketNumber);
                        }
                        else
                            GlobalDataAccessor.Instance.DesktopSession.Purchases = new List<PurchaseVO>();
                        if (iDx < 0)
                            GlobalDataAccessor.Instance.DesktopSession.Purchases.Add(purchaseObj);
                    }

                    return true;
                }
            }
            else
            {
                CustomerLoans.GetCustomerName(
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    iTicketNumber,
                    !pfiVerifyPurchase
                    ? ProductType.PAWN.ToString() : ProductType.BUY.ToString(),
                    out sFirstName,
                    out sMiddleName,
                    out sLastName,
                    out sErrorCode,
                    out sErrorText);

                sCustomerName = Utilities.GetStringValue(sLastName, "") + ", ";
                if (sFirstName != "null")
                    sCustomerName += Utilities.GetStringValue(sFirstName, "") + " ";
                if (sMiddleName != "null")
                    sCustomerName += Utilities.GetStringValue(sMiddleName, "");
                sCustomerName = sCustomerName.Replace("  ", " ");
                return true;
            }
            // If user enters bad Ticket Number, instantiate blank PFI Loan object and return false
            pfiPawnLoan = new PFI_ProductData();
            if (!pfiVerifyPurchase)
            {
                pfiPawnLoan.OriginalObject = new PawnLoan();
                pfiPawnLoan.UpdatedObject = new PawnLoan();
            }
            else
            {
                pfiPawnLoan.OriginalObject = new PurchaseVO();
                pfiPawnLoan.UpdatedObject = new PurchaseVO();
            }
            pfiPawnLoan.MergedItems = new List<PFI_Merged>();
            return false;
        }

        private void FindPawnLoad()
        {
            _Setup = true;

            Cursor = Cursors.WaitCursor;

            var sCustomerName = string.Empty;

            _PfiActiveLoan = new PFI_ProductData();
            _CostDifference = 0;
            _SelectedRowIndex = -1;

            if (GetProductData(Convert.ToInt32(ticketNumber.Text), StateStatus.PFIS, out _PfiActiveLoan, out sCustomerName))
            {
                string sErrorCode;
                string sErrorText;
                List<int> lstTicketNumbers = new List<int>();
                List<string> lstRefTypes = new List<string>();

                lstTicketNumbers.Add(_PfiActiveLoan.UpdatedObject.TicketNumber);
                lstRefTypes.Add(pfiVerifyPurchase ? "2" : "1");

                if (_PfiActiveLoan.UpdatedObject.TempStatus != StateStatus.PFIW
                    && _PfiActiveLoan.UpdatedObject.TempStatus != StateStatus.PFIE
                    && _PfiActiveLoan.UpdatedObject.TempStatus != StateStatus.PFI
                    && _PfiActiveLoan.UpdatedObject.TempStatus != StateStatus.PFIS
                )
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("Transaction is not available for PFI Verify.", "Ticket Number Lookup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (_PfiActiveLoan.UpdatedObject.HoldDesc.Equals("Police Hold", StringComparison.OrdinalIgnoreCase))
                {
                    //Show manager override
                    ManageOverrides overrideForm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER);
                    overrideForm.MessageToShow = Commons.GetMessageString("ManageOverrideDefaultMessage") +
                                                 System.Environment.NewLine +
                                                 "Transaction is on Police Hold";

                    List<ManagerOverrideType> overrideTypes = new List<ManagerOverrideType> { ManagerOverrideType.PFIP };
                    overrideForm.ManagerOverrideTypes = overrideTypes;
                    List<ManagerOverrideTransactionType> tranTypes = new List<ManagerOverrideTransactionType> { ManagerOverrideTransactionType.PFI };
                    overrideForm.ManagerOverrideTransactionTypes = tranTypes;
                    overrideForm.ShowDialog(this);
                    if (!overrideForm.OverrideAllowed)
                    {
                        MessageBox.Show(@"Manager override needed for processing PFI on a transaction that is on police hold");
                        return;
                    }
                }

                if (_PfiActiveLoan.UpdatedObject.TempStatus == StateStatus.PFI)
                {
                    //Madhu 11/18/2010 fix for defect PWNU00001443
                    DialogResult dialogResult = MessageBox.Show("Transaction was previously edited and now ready for PFI Posting.  Hit OK to reset it back for Editing.", "Ticket Number Lookup", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dialogResult != DialogResult.OK)
                    {
                        StoreLoans.SetLoanTransition(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                     _PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString() ? ProductType.PAWN : ProductType.BUY,
                                                     _PfiActiveLoan.UpdatedObject.TicketNumber,
                                                     Utilities.Serialize(_PfiActiveLoan),
                                                     StateStatus.PFI,
                                                     ShopDateTime.Instance.ShopDate,
                                                     out sErrorCode,
                                                     out sErrorText);

                        StoreLoans.UpdateTempStatus(
                            lstTicketNumbers,
                            StateStatus.PFI,
                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                            true,
                            lstRefTypes,
                            out sErrorCode,
                            out sErrorText);

                        Cursor = Cursors.Default;
                        return;
                    }
                    GetProductData(Convert.ToInt32(ticketNumber.Text), StateStatus.PFI, out _PfiActiveLoan, out sCustomerName);
                    StoreLoans.DeleteLoanTransition(
                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        _PfiActiveLoan.UpdatedObject.TicketNumber,
                        !pfiVerifyPurchase ? ProductType.PAWN : ProductType.BUY,
                        out sErrorCode,
                        out sErrorText);
                }
                else if (_PfiActiveLoan.UpdatedObject.TempStatus == StateStatus.PFIS)
                    MessageBox.Show("Suspended transaction information retrieved.", "Ticket Number Lookup",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!StoreLoans.UpdateTempStatus(
                    lstTicketNumbers,
                    StateStatus.PFIE,
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    true,
                    lstRefTypes,
                    out sErrorCode,
                    out sErrorText))
                    MessageBox.Show("Error updating database with edit status.  " + sErrorText, "PFI Edit Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                _IcnYear = _PfiActiveLoan.OriginalObject.Items[0].mYear;

                searchPanel.Visible = false;
                PageSize(false);
                UpdateDataGrid(sCustomerName);
                undoButton.Enabled = UndoButtonEnablement(0);
            }
            else
            {
                searchPanel.Visible = true;
                PageSize(true);
                MessageBox.Show("Ticket Number was not found.", "Ticket Number Lookup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            PageStateButtons();
            Cursor = Cursors.Default;

            _Setup = false;
        }

        private void ticketFindButton_Click(object sender, EventArgs e)
        {
            FindTicket();
        }

        private void FindTicket()
        {
            if (ticketNumber.Text == "")
            {
                MessageBox.Show("Ticket Number required before continuing.", "Ticket Number Lookup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ticketNumber.Focus();
                return;
            }
            if (Utilities.GetIntegerValue(ticketNumber.Text) < 1)
            {
                MessageBox.Show("Ticket Number should not include non-numeric digits.", "Ticket Number Lookup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ticketNumber.Text = "";
                ticketNumber.Focus();
                return;
            }
            if (radioButtonPurchase.Checked)
            {
                pfiVerifyPurchase = true;
                //Madhu 11/18/2010 fix for defect PWNU00001443
                label5.Text = "Purchase Amount:";
            }
            else
            {
                pfiVerifyPurchase = false;
                //Madhu 11/18/2010 fix for defect PWNU00001443
                label5.Text = "Loan Amount:";
            }
            FindPawnLoad();
        }

        private void ExitOut()
        {
            // Replace with Controller Logic.
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (_PageState == PageState.Verify && infoPanel.Visible)
            {
                if (_PfiActiveLoan.UpdatedObject.TempStatus != StateStatus.BLNK)
                {
                    string sErrorCode;
                    string sErrorText;
                    List<int> lstTicketNumbers = new List<int>();
                    List<string> lstRefTypes = new List<string>();

                    lstTicketNumbers.Add(_PfiActiveLoan.UpdatedObject.TicketNumber);
                    if (pfiVerifyPurchase)
                        lstRefTypes.Add("2");
                    else
                        lstRefTypes.Add("1");

                    StoreLoans.UpdateTempStatus(
                        lstTicketNumbers,
                        StateStatus.PFIW,
                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        true,
                        lstRefTypes,
                        out sErrorCode,
                        out sErrorText);
                }
                ExitOut();
            }
            else if (_PageState == PageState.Verify && !infoPanel.Visible)
            {
                ExitOut();
            }
            else
            {
                _PageState = PageState.Verify;
                PageStateButtons();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (_PfiActiveLoan.UpdatedObject.TempStatus != StateStatus.BLNK)
            {
                string sErrorCode;
                string sErrorText;
                List<int> lstTicketNumbers = new List<int>();
                List<string> lstRefTypes = new List<string>();

                lstTicketNumbers.Add(_PfiActiveLoan.UpdatedObject.TicketNumber);
                if (!pfiVerifyPurchase)
                    lstRefTypes.Add("1");
                else
                    lstRefTypes.Add("2");

                StoreLoans.UpdateTempStatus(
                    lstTicketNumbers,
                    StateStatus.PFIW,
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    true,
                    lstRefTypes,
                    out sErrorCode,
                    out sErrorText);
            }

            searchPanel.Visible = true;
            PageSize(true);
        }

        private void suspendButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // First, get Assignment & Tags and populate back into PawnLoan Items
            for (int i = 0; i < gvLoanItems.Rows.Count; i++)
            {
                DataGridViewRow myRow = gvLoanItems.Rows[i];
                PfiAssignment pfiAssignment = (PfiAssignment)Enum.Parse(typeof(PfiAssignment), Utilities.GetStringValue(myRow.Cells[colAssignmentType.Name].Value, PfiAssignment.Normal.ToString()));
                int iTags = Utilities.GetIntegerValue(myRow.Cells[colTags.Name].Value, 0);

                _PfiActiveLoan.UpdatedObject.Items[i].PfiAssignmentType = pfiAssignment;
                _PfiActiveLoan.UpdatedObject.Items[i].PfiTags = iTags;
            }

            _PfiActiveLoan.SuspendDate = ShopDateTime.Instance.ShopDate;
            _PfiActiveLoan.UpdatedObject.TempStatus = StateStatus.PFIS;

            string sErrorCode;
            string sErrorText;
            List<HoldData> listHolds;

            MerchandiseProcedures.CheckForHolds(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                _PfiActiveLoan.UpdatedObject.TicketNumber,
                                                _PfiActiveLoan.UpdatedObject.ProductType,
                                                out listHolds,
                                                out sErrorCode,
                                                out sErrorText);

            Cursor = Cursors.Default;

            if (sErrorCode == "0")
            {
                int iPawnItemHoldCount = _PfiActiveLoan.OriginalObject.Items
                .FindAll(fa => listHolds
                               .FindIndex(h => h.HoldType != fa.HoldType && h.HoldType != "") >= 0)
                .Count;

                if (iPawnItemHoldCount > 0)
                {
                    MessageBox.Show("Information cannot be suspended due to recent hold placed on Pawn Loan item.", "PFI Suspension", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    searchPanel.Visible = true;
                    PageSize(true);
                }
                else
                {
                    if (SetSuspendData())
                    {
                        List<int> lstTicketNumbers = new List<int>();
                        lstTicketNumbers.Add(_PfiActiveLoan.UpdatedObject.TicketNumber);
                        List<string> lstReftypes = new List<string>();
                        if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString())
                            lstReftypes.Add("1");
                        else
                            lstReftypes.Add("2");

                        if (!StoreLoans.UpdateTempStatus(
                            lstTicketNumbers,
                            StateStatus.PFIS,
                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                            true,
                            lstReftypes,
                            out sErrorCode,
                            out sErrorText))
                            MessageBox.Show("Error updating database with suspension.  " + sErrorText,
                                            "PFI Suspension Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                        {
                            MessageBox.Show("Information suspended.", "PFI Suspension", MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            searchPanel.Visible = true;
                            PageSize(true);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error suspending.  ", "PFI Suspension Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void addNewItemButton_Click(object sender, EventArgs e)
        {
            // Get current Desktop Active Loan and store temporarily
            PawnLoan activePawnLoan = null;
            PurchaseVO activePurchase = null;
            if (_PfiActiveLoan.UpdatedObject.GetType() == typeof(PawnLoan))
            {
                activePawnLoan = Utilities.CloneObject<PawnLoan>(GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan);
                GlobalDataAccessor.Instance.DesktopSession.PurchasePFIAddItem = false;
            }
            else if (_PfiActiveLoan.UpdatedObject.GetType() == typeof(PurchaseVO))
            {
                activePurchase = Utilities.CloneObject<PurchaseVO>(GlobalDataAccessor.Instance.DesktopSession.ActivePurchase);
                GlobalDataAccessor.Instance.DesktopSession.PurchasePFIAddItem = true;
            }
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "DescribeMerchandisePFIADD";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
            try
            {
                Item newPawnItem = null;
                if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString() && GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.TicketNumber > -1)
                {
                    newPawnItem = Utilities.CloneObject<Item>(GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Count() - 1]);
                    newPawnItem.PfiVerified = true;
                }
                else if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.BUY.ToString() && GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.TicketNumber > -1)
                {
                    newPawnItem = Utilities.CloneObject<Item>(GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items[GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items.Count() - 1]);
                    newPawnItem.PfiVerified = true;
                }

                if (newPawnItem != null)
                {
                    if (newPawnItem.IsJewelry)
                    {
                        // Update Jewelry ICN.  UpdateDataGrid will update PawnItem ICN
                        for (int i = 0; i < newPawnItem.Jewelry.Count(); i++)
                        {
                            JewelrySet jewelrySet = newPawnItem.Jewelry[i];
                            jewelrySet.Icn = Utilities.IcnGenerator(newPawnItem.mStore, _IcnYear,
                                                                    _PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString() ? ((PawnLoan)_PfiActiveLoan.UpdatedObject).OrigTicketNumber : _PfiActiveLoan.UpdatedObject.TicketNumber,
                                                                    _PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString() ? "1" : "2", _PfiActiveLoan.UpdatedObject.Items.Count() + 1, i + 1);
                            jewelrySet.SubItemNumber = i + 1;
                        }
                        newPawnItem.PfiAssignmentType = PfiAssignment.Scrap;
                    }
                    else
                        newPawnItem.PfiAssignmentType = PfiAssignment.Normal;

                    // Set up Pawn Item for PFI Page
                    newPawnItem.ItemReason = newPawnItem.CaccLevel == 0 ? ItemReason.CACC : ItemReason.ADDD;
                    //newPawnItem.mDocNumber = _PfiActiveLoan.UpdatedObject.TicketNumber;
                    if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString())
                        newPawnItem.mDocNumber = ((PawnLoan)_PfiActiveLoan.UpdatedObject).OrigTicketNumber;
                    else if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.BUY.ToString())
                    {
                        newPawnItem.mDocNumber = string.IsNullOrEmpty(((PurchaseVO)_PfiActiveLoan.UpdatedObject).RefType) ? ((PurchaseVO)_PfiActiveLoan.UpdatedObject).TicketNumber : ((PurchaseVO)_PfiActiveLoan.UpdatedObject).RefNumber;
                    }
                    newPawnItem.mStore = Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
                    newPawnItem.mYear = _IcnYear;
                    newPawnItem.mItemOrder = _PfiActiveLoan.UpdatedObject.Items.Count() + 1;
                    newPawnItem.Tag = ItemReason.ADDD.ToString();
                    if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString())
                        newPawnItem.Icn = Utilities.IcnGenerator(newPawnItem.mStore, _IcnYear, ((PawnLoan)_PfiActiveLoan.UpdatedObject).OrigTicketNumber,
                                                                 "1", _PfiActiveLoan.UpdatedObject.Items.Count() + 1, 0);
                    else if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.BUY.ToString())
                        newPawnItem.Icn = Utilities.IcnGenerator(newPawnItem.mStore, _IcnYear, _PfiActiveLoan.UpdatedObject.TicketNumber,
                                                                 "2", _PfiActiveLoan.UpdatedObject.Items.Count() + 1, 0);

                    _PfiActiveLoan.UpdatedObject.Items.Add(newPawnItem);

                    // Lastly, reGrid the information on screen
                    UpdateDataGrid(customerNameLabel.Text);
                    gvLoanItems.Rows[gvLoanItems.Rows.Count - 1].Selected = true;
                    gvLoanItems.FirstDisplayedScrollingRowIndex = gvLoanItems.Rows.Count - 1;
                    undoButton.Enabled = UndoButtonEnablement(gvLoanItems.Rows.Count - 1);
                }
            }
            catch (Exception eX)
            {
                return;
            }
            // Reset Active Pawn Loan back to Original
            if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString())
                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan = activePawnLoan;
            else if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.BUY.ToString())
                GlobalDataAccessor.Instance.DesktopSession.ActivePurchase = activePurchase;
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            if (_SelectedRowIndex >= 0)
            {
                Item uPawnItem = _PfiActiveLoan.UpdatedObject.Items[_SelectedRowIndex];

                int iDx = _PfiActiveLoan.MergedItems.FindIndex(delegate(PFI_Merged pfm)
                                                               {
                                                                   return pfm.NewItem.Icn == uPawnItem.Icn || pfm.OriginalItems.FindIndex(ol => ol.Icn == uPawnItem.Icn) >= 0;
                                                               });

                if (iDx >= 0)
                {
                    PFI_Merged PFI_Merge = _PfiActiveLoan.MergedItems[iDx];

                    foreach (Item pi in PFI_Merge.OriginalItems)
                    {
                        int fDx = _PfiActiveLoan.OriginalObject.Items.FindIndex(delegate(Item p)
                                                                                {
                                                                                    return p.Icn == pi.Icn;
                                                                                });

                        if (fDx >= 0)
                        {
                            Item oPawnItem = Utilities.CloneObject<Item>(_PfiActiveLoan.OriginalObject.Items[fDx]);

                            fDx = _PfiActiveLoan.UpdatedObject.Items.FindIndex(delegate(Item opi)
                                                                               {
                                                                                   return opi.Icn == oPawnItem.Icn;
                                                                               });
                            if (fDx >= 0)
                            {
                                _PfiActiveLoan.UpdatedObject.Items.RemoveAt(fDx);
                                _PfiActiveLoan.UpdatedObject.Items.Insert(fDx, oPawnItem);
                            }
                        }
                    }
                    _PfiActiveLoan.MergedItems.RemoveAt(iDx);
                    UpdateDataGrid(customerNameLabel.Text);
                }

                //{
                iDx = _PfiActiveLoan.OriginalObject.Items.FindIndex(delegate(Item pi)
                                                                    {
                                                                        return pi.Icn == uPawnItem.Icn;
                                                                    });

                if (iDx >= 0)
                {
                    Item oPawnItem = Utilities.CloneObject<Item>(_PfiActiveLoan.OriginalObject.Items[iDx]);

                    int fDx = _PfiActiveLoan.UpdatedObject.Items.FindIndex(delegate(Item opi)
                                                                           {
                                                                               return opi.Icn == oPawnItem.Icn;
                                                                           });
                    if (fDx >= 0)
                    {
                        _PfiActiveLoan.UpdatedObject.Items.RemoveAt(fDx);
                        _PfiActiveLoan.UpdatedObject.Items.Insert(fDx, oPawnItem);
                    }
                }
                else
                {
                    // Removing Added PFI Loan Item
                    _PfiActiveLoan.UpdatedObject.Items.RemoveAt(_SelectedRowIndex);
                    _SelectedRowIndex--;
                }

                UpdateDataGrid(customerNameLabel.Text);
                //undoButton.Enabled = UndoButtonEnablement(0); //commented out by Drew
                undoButton.Enabled = UndoButtonEnablement(_SelectedRowIndex);
                // }
            }
        }

        private void mergeButton_Click(object sender, EventArgs e)
        {
            _PageState = PageState.Merge;
            _SelectedMergeRowIndex = new Queue<int>();
            PageStateButtons();

            gvLoanItems.Columns[colAssignmentType.Name].ReadOnly = true;

            for (int i = 0; i < gvLoanItems.Rows.Count; i++)
            {
                DataGridViewRow myRow = gvLoanItems.Rows[i];

                bool bFlagged = false;
                string sHoldType = _PfiActiveLoan.UpdatedObject.Items[i].HoldType;

                if (Utilities.GetStringValue(myRow.Cells[colReason.Name].Value, "") != "" || sHoldType == "2")
                    bFlagged = true;
                else if (_PfiActiveLoan.MergedItems.FindIndex(delegate(PFI_Merged pfm)
                                                              {
                                                                  return pfm.NewItem.Icn == _PfiActiveLoan.UpdatedObject.Items[i].Icn;
                                                              }) >= 0)
                    bFlagged = true;
                //else if (_PfiActiveLoan.UpdatedObject.Items[i].CategoryCode == 4390)
                //    bFlagged = true;
                else if (
                    _PfiActiveLoan.UpdatedObject.Items[i].MerchandiseType == "H"
                    || _PfiActiveLoan.UpdatedObject.Items[i].MerchandiseType == "L"
                    )
                    bFlagged = true;

                else if (Utilities.IsGun(
                    _PfiActiveLoan.UpdatedObject.Items[i].GunNumber,
                    _PfiActiveLoan.UpdatedObject.Items[i].CategoryCode,
                    _PfiActiveLoan.UpdatedObject.Items[i].IsJewelry,
                    _PfiActiveLoan.UpdatedObject.Items[i].MerchandiseType))
                    bFlagged = true;
                else if (_PfiActiveLoan.UpdatedObject.Items[i].ItemReason == ItemReason.ADDD)
                    bFlagged = true;

                myRow.Cells[colMerge.Name].Value = false;
                myRow.Cells[colMerge.Name].ReadOnly = bFlagged;
                myRow.DefaultCellStyle.BackColor = bFlagged ? Color.SlateGray : Color.White;
            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            if (_PageState == PageState.Verify)
            {
                Cursor = Cursors.WaitCursor;

                string sErrorCode;
                string sErrorText;
                List<int> lstTicketNumbers = new List<int>();
                lstTicketNumbers.Add(_PfiActiveLoan.UpdatedObject.TicketNumber);

                // First, get Assignment & Tags and populate back into PawnLoan Items
                for (int i = 0; i < gvLoanItems.Rows.Count; i++)
                {
                    DataGridViewRow myRow = gvLoanItems.Rows[i];
                    PfiAssignment pfiAssignment = (PfiAssignment)Enum.Parse(typeof(PfiAssignment), myRow.Cells[colAssignmentType.Name].Value.ToString());
                    int iTags = Utilities.GetIntegerValue(myRow.Cells[colTags.Name].Value, 0);

                    _PfiActiveLoan.UpdatedObject.Items[i].PfiAssignmentType = pfiAssignment;
                    _PfiActiveLoan.UpdatedObject.Items[i].PfiTags = iTags;
                }
                // Update Oracle Storage Chamber
                _PfiActiveLoan.UpdatedObject.TempStatus = StateStatus.PFI;
                List<string> lstReftypes = new List<string>();
                if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString())
                    lstReftypes.Add("1");
                else
                    lstReftypes.Add("2");

                string transitionData = Utilities.Serialize<PFI_ProductData>(_PfiActiveLoan);
                if (string.IsNullOrEmpty(transitionData))
                {
                    MessageBox.Show("Error serializing object to xml.", "PFI Update",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!StoreLoans.SetLoanTransition(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                      _PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString() ? ProductType.PAWN : ProductType.BUY,
                                                      _PfiActiveLoan.UpdatedObject.TicketNumber,
                                                      transitionData,
                                                      StateStatus.PFI,
                                                      ShopDateTime.Instance.ShopDate,
                                                      out sErrorCode,
                                                      out sErrorText)
                    ||
                    !StoreLoans.UpdateTempStatus(
                        lstTicketNumbers,
                        StateStatus.PFI,
                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        true,
                        lstReftypes,
                        out sErrorCode,
                        out sErrorText))
                {
                    Cursor = Cursors.Default;
                    //Madhu 11/18/2010 fix for defect PWNU00001443
                    MessageBox.Show("Error updating database for PFI ready to post items.  " + sErrorText, "PFI Update",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Cursor = Cursors.Default;
                    //Madhu 11/18/2010 fix for defect PWNU00001443
                    MessageBox.Show("Item is ready for PFI Posting.", "PFI Posting Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Add to Desktop PawnLoans
                if (_PfiActiveLoan.UpdatedObject.GetType() == typeof(PawnLoan))
                {
                    int iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans.FindIndex(
                        pl => pl.TicketNumber == _PfiActiveLoan.UpdatedObject.TicketNumber);

                    if (iDx < 0)
                        GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Add((PawnLoan)_PfiActiveLoan.UpdatedObject);
                    else
                    {
                        GlobalDataAccessor.Instance.DesktopSession.PawnLoans.RemoveAt(iDx);
                        GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Insert(iDx, (PawnLoan)_PfiActiveLoan.UpdatedObject);
                    }
                }
                else if (_PfiActiveLoan.UpdatedObject.GetType() == typeof(PurchaseVO))
                {
                    int iDx = GlobalDataAccessor.Instance.DesktopSession.Purchases.FindIndex(
                        pl => pl.TicketNumber == _PfiActiveLoan.UpdatedObject.TicketNumber);

                    if (iDx < 0)
                        GlobalDataAccessor.Instance.DesktopSession.Purchases.Add((PurchaseVO)_PfiActiveLoan.UpdatedObject);
                    else
                    {
                        GlobalDataAccessor.Instance.DesktopSession.Purchases.RemoveAt(iDx);
                        GlobalDataAccessor.Instance.DesktopSession.Purchases.Insert(iDx, (PurchaseVO)_PfiActiveLoan.UpdatedObject);
                    }
                }
                Cursor = Cursors.Default;

                searchPanel.Visible = true;
                PageSize(true);
            }
            else
            {
                //int iQueueIdx = _SelectedMergeRowIndex.Dequeue();
                GlobalDataAccessor.Instance.DesktopSession.SelectedPFIMergeItemIndex = new List<int>();

                List<int> selectedItemRowIndex = _SelectedMergeRowIndex.ToList();
                GlobalDataAccessor.Instance.DesktopSession.SelectedPFIMergeItemIndex = selectedItemRowIndex;
                gvLoanItems.Columns[colAssignmentType.Name].ReadOnly = false;
                PawnLoan activePawnLoan = null;
                PurchaseVO activePurchase = null;
                // Get current Desktop Active Loan and store temporarily
                if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString())
                {
                    activePawnLoan = Utilities.CloneObject<PawnLoan>(GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan);
                }
                else if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.BUY.ToString())
                {
                    activePurchase = Utilities.CloneObject<PurchaseVO>(GlobalDataAccessor.Instance.DesktopSession.ActivePurchase);
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.CUSTOMERPURCHASEPFI;
                }

                // Call Describe Merchandise Page with PFI MERGE context
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "DescribeMerchandisePFIMerge";
                NavControlBox.Action = NavBox.NavAction.SUBMIT;
                /*DescribeMerchandise myForm = new DescribeMerchandise(CurrentContext.PFI_MERGE);
                CashlinxDesktopSession.Instance.HistorySession.AddForm(myForm);
                myForm.ShowDialog(this);*/

                //find min in queueu - you want the smallest index of the selected items to be merged - this will be the merged record
                int iQueueIdx = _SelectedMergeRowIndex.Dequeue();
                for (int i = 0; i < _SelectedMergeRowIndex.Count; i++)
                {
                    int j = _SelectedMergeRowIndex.Dequeue();
                    if (j < iQueueIdx)
                        iQueueIdx = j;
                }

                // Any updates made in Describe Item is in Desktop Active Pawn Loan
                // Save Desktop Active Pawn Loan back to local PFI Active Loan object.
                // If Desktop Active Pawn Loan is null, do not update local PFI Active Loan
                Item newPawnItem = null;
                if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString()
                    && GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.TicketNumber > -1)
                {
                    //PawnItem newPawnItem = CashlinxDesktopSession.Instance.ActivePawnLoan.Items[0];
                    newPawnItem = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Count - 1];
                    newPawnItem.PfiVerified = true;
                }
                else if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.BUY.ToString()
                         && GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.TicketNumber > -1)
                {
                    //PawnItem newPawnItem = CashlinxDesktopSession.Instance.ActivePawnLoan.Items[0];
                    newPawnItem = GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items[GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items.Count - 1];
                    newPawnItem.PfiVerified = true;
                }

                if (newPawnItem != null)
                {
                    if (newPawnItem.IsJewelry)
                    {
                        // Update Jewelry ICN.  UpdateDataGrid will update PawnItem ICN
                        if (newPawnItem.Jewelry.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(newPawnItem.Jewelry[0].TicketDescription))
                            {
                                List<JewelrySet> newJewelry = new List<JewelrySet>();

                                for (int i = 0; i < newPawnItem.Jewelry.Count(); i++)
                                {
                                    JewelrySet jewelrySet = newPawnItem.Jewelry[i];
                                    jewelrySet.Icn = Utilities.IcnGenerator(newPawnItem.mStore, _IcnYear,
                                                                            _PfiActiveLoan.UpdatedObject.TicketNumber,
                                                                            _PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString() ? "1" : "2",
                                                                            iQueueIdx + 1, i + 1);
                                    jewelrySet.SubItemNumber = i + 1;
                                    newJewelry.Add(jewelrySet);
                                }
                                newPawnItem.Jewelry = newJewelry;
                            }
                            else
                                newPawnItem.Jewelry.Remove(newPawnItem.Jewelry[0]);
                        }
                        newPawnItem.PfiAssignmentType = PfiAssignment.Scrap;
                    }
                    else
                    {
                        newPawnItem.PfiAssignmentType = PfiAssignment.Normal;
                    }

                    // Set up Pawn Item for PFI Page
                    newPawnItem.ItemReason = newPawnItem.CaccLevel == 0 ? ItemReason.CACC : ItemReason.BLNK;

                    newPawnItem.Icn = _PfiActiveLoan.UpdatedObject.Items[iQueueIdx].Icn;
                    //newPawnItem.mDocNumber = _PfiActiveLoan.UpdatedObject.TicketNumber;
                    if (_PfiActiveLoan.UpdatedObject.GetType() == typeof(PawnLoan))
                        newPawnItem.mDocNumber = ((PawnLoan)_PfiActiveLoan.UpdatedObject).OrigTicketNumber;
                    else if (_PfiActiveLoan.UpdatedObject.GetType() == typeof(PurchaseVO))
                    {
                        newPawnItem.mDocNumber = string.IsNullOrEmpty(((PurchaseVO)_PfiActiveLoan.UpdatedObject).RefType) ? ((PurchaseVO)_PfiActiveLoan.UpdatedObject).TicketNumber : ((PurchaseVO)_PfiActiveLoan.UpdatedObject).RefNumber;
                    }
                    newPawnItem.mStore = Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
                    newPawnItem.mYear = _IcnYear;
                    newPawnItem.mItemOrder = iQueueIdx + 1;

                    PFI_Merged PfiMerge = new PFI_Merged();
                    PfiMerge.NewItem = newPawnItem;
                    GlobalDataAccessor.Instance.DesktopSession.PrintTags(newPawnItem, CurrentContext.PFI_MERGE);
                    PfiMerge.OriginalItems = new List<Item>();

                    // Iterate through Merge Items and change Item Status
                    for (int i = 0; i < gvLoanItems.Rows.Count; i++)
                    {
                        DataGridViewRow myRow = gvLoanItems.Rows[i];
                        if (Utilities.GetBooleanValue(myRow.Cells[colMerge.Name].Value, false))
                        {
                            // Cloan and add original Pawn Item that was merged 
                            PfiMerge.OriginalItems.Add(Utilities.CloneObject<Item>(_PfiActiveLoan.UpdatedObject.Items[i]));
                            // Updated current PFI Updated Loan Pawn Item to displayed merged Pawn Item on screen

                            // If First Merge selected, replace with new Pawn Item, else, 
                            // blank out existing Pawn Item
                            if (iQueueIdx == i)
                            {
                                _PfiActiveLoan.UpdatedObject.Items.RemoveAt(i);
                                _PfiActiveLoan.UpdatedObject.Items.Insert(i, newPawnItem);
                            }
                            else
                            {
                                Item mergedPawnItem = _PfiActiveLoan.UpdatedObject.Items[i];
                                mergedPawnItem.PfiVerified = true;
                                mergedPawnItem.ItemReason = ItemReason.MERGED;
                                mergedPawnItem.ItemAmount = 0;

                                _PfiActiveLoan.UpdatedObject.Items.RemoveAt(i);
                                _PfiActiveLoan.UpdatedObject.Items.Insert(i, mergedPawnItem);
                            }
                        }
                    }
                    _PfiActiveLoan.MergedItems.Add(PfiMerge);
                    // Update DataGrid in case of changes from Describe Item
                    UpdateDataGrid(customerNameLabel.Text);
                    undoButton.Enabled = UndoButtonEnablement(0);
                }
                else
                {
                    MessageBox.Show("PFI Merge was not performed.", "PFI Merge Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Reset Active Pawn Loan back to Original
                if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString() &&
                    activePawnLoan != null)
                    GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan = activePawnLoan;
                else if (_PfiActiveLoan.UpdatedObject.GetType() == typeof(PurchaseVO) && activePurchase != null)
                    GlobalDataAccessor.Instance.DesktopSession.ActivePurchase = activePurchase;

                _PageState = PageState.Verify;
                PageStateButtons();
            }
        }

        private void gvLoanItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (_PageState == PageState.Verify)
                {
                    int selectedRow = e.RowIndex;
                    //Check if the item status is return in which case dont allow to proceed
                    string sItemStatus = Utilities.GetStringValue(_PfiActiveLoan.UpdatedObject.Items[e.RowIndex].ItemStatus);
                    if (sItemStatus == ProductStatus.RET.ToString())
                    {
                        MessageBox.Show("You may not change the cost of returned item");

                        return;
                    }
                    if (sItemStatus == ProductStatus.PS.ToString())
                    {
                        MessageBox.Show("You may not process a police seized item");

                        return;
                    }

                    ItemReason pawnItemStatus = _PfiActiveLoan.UpdatedObject.Items[e.RowIndex].ItemReason;

                    int iDx = _PfiActiveLoan.MergedItems.FindIndex(
                        pfm =>
                        pfm.NewItem.Icn == _PfiActiveLoan.UpdatedObject.Items[e.RowIndex].Icn);

                    if (pawnItemStatus == ItemReason.MERGED
                        || pawnItemStatus == ItemReason.COFFSTLN
                        || pawnItemStatus == ItemReason.NOMD
                        || pawnItemStatus == ItemReason.HPFI
                    ) // || iDx >= 0)
                        return;

                    if (gvLoanItems.Columns[e.ColumnIndex].Name == gvLoanItems.Columns[colDescription.Name].Name)
                    {
                        // Need to populate pawnLoan from GetCat5
                        int iCategoryMask = GlobalDataAccessor.Instance.DesktopSession.CategoryXML.GetCategoryMask(_PfiActiveLoan.UpdatedObject.Items[e.RowIndex].CategoryCode);
                        DescribedMerchandise dmPawnItem = new DescribedMerchandise(iCategoryMask);
                        Item pawnItem = _PfiActiveLoan.UpdatedObject.Items[e.RowIndex];
                        if (pawnItem.Jewelry == null || pawnItem.Jewelry.Count == 0)
                            dmPawnItem.SelectedPawnItem.Jewelry = null;
                        // Due to holding and updated Item Amount, add it to Selected Pawn Item
                        dmPawnItem.SelectedPawnItem.ItemAmount = pawnItem.ItemAmount;
                        Item.PawnItemMerge(ref pawnItem, dmPawnItem.SelectedPawnItem, false);
                        // Update local PFI Active Loan Pawn Item with Cat5 Info
                        pawnItem.CategoryMask = iCategoryMask;
                        _PfiActiveLoan.UpdatedObject.Items.RemoveAt(e.RowIndex);
                        _PfiActiveLoan.UpdatedObject.Items.Insert(e.RowIndex, pawnItem);
                        // End GetCat5 populate
                        Item tempPawnItem = Utilities.CloneObject<Item>(_PfiActiveLoan.UpdatedObject.Items[e.RowIndex]);
                        if (iDx >= 0)
                            tempPawnItem.Tag = ItemReason.MERGED.ToString();

                        // Get current Desktop Active Loan and store temporarily
                        PawnLoan activePawnLoan = null;
                        PurchaseVO activePurchase = null;
                        if (!pfiVerifyPurchase)
                        {
                            if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan != null)
                            {
                                activePawnLoan = Utilities.CloneObject(GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan);
                            }
                            GlobalDataAccessor.Instance.DesktopSession.PawnLoans = new List<PawnLoan>
                            {
                                new PawnLoan()
                            };

                            if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan != null)
                            {
                                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Add(tempPawnItem);
                            }
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = "";
                        }
                        else
                        {
                            if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase != null)
                            {
                                activePurchase = Utilities.CloneObject(GlobalDataAccessor.Instance.DesktopSession.ActivePurchase);
                            }
                            GlobalDataAccessor.Instance.DesktopSession.Purchases = new List<PurchaseVO>
                            {
                                new PurchaseVO()
                            };
                            if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase != null)
                            {
                                GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items.Add(tempPawnItem);
                            }
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.CUSTOMERPURCHASEPFI;
                        }
                        // Call Describe Item Page using PFI Redescribe context
                        GlobalDataAccessor.Instance.DesktopSession.SelectedItemOrder = e.RowIndex + 1;
                        GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext = CurrentContext.PFI_REDESCRIBE;
                        GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = 0;
                        GlobalDataAccessor.Instance.DesktopSession.DescribeItemSelectedProKnowMatch = tempPawnItem.SelectedProKnowMatch;

                        NavControlBox.IsCustom = true;
                        NavControlBox.CustomDetail = "DescribeItemPFIReDescribe";
                        NavControlBox.Action = NavBox.NavAction.SUBMIT;

                        // Any updates made in Describe Item is in Desktop Active Pawn Loan
                        // Save Desktop Active Pawn Loan back to local PFI Active Loan object.
                        // If Desktop Active Pawn Loan is null, do not update local PFI Active Loan
                        Item redescribedPawnItem = null;
                        if (!pfiVerifyPurchase)
                        {
                            if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan != null)
                            {
                                if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.TicketNumber != -1)
                                    redescribedPawnItem = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0];
                            }
                        }
                        else
                        {
                            if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase != null)
                            {
                                if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.TicketNumber != -1)
                                    redescribedPawnItem = GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items[0];
                            }
                        }
                        if (redescribedPawnItem != null)
                        {
                            if (redescribedPawnItem.IsJewelry)
                            {
                                // Update Jewelry ICN.  UpdateDataGrid will update PawnItem ICN
                                for (int i = 0; i < redescribedPawnItem.Jewelry.Count(); i++)
                                {
                                    JewelrySet jewelrySet = redescribedPawnItem.Jewelry[i];
                                    jewelrySet.Icn = _PfiActiveLoan.UpdatedObject.ProductType ==
                                                     ProductType.PAWN.ToString()
                                                     ? Utilities.IcnGenerator(
                                                         redescribedPawnItem.mStore,
                                                         _IcnYear,
                                                         _PfiActiveLoan.UpdatedObject.
                                                         TicketNumber,
                                                         "1",
                                                         _PfiActiveLoan.UpdatedObject.
                                                         Items.Count() + 1,
                                                         i + 1)
                                                     : Utilities.IcnGenerator(
                                                         redescribedPawnItem.mStore,
                                                         _IcnYear,
                                                         _PfiActiveLoan.UpdatedObject.
                                                         TicketNumber,
                                                         "2",
                                                         _PfiActiveLoan.UpdatedObject.
                                                         Items.Count() + 1,
                                                         i + 1);

                                    jewelrySet.SubItemNumber = i + 1;
                                }
                                if (redescribedPawnItem.PfiAssignmentType != PfiAssignment.Normal
                                    && redescribedPawnItem.PfiAssignmentType != PfiAssignment.Refurb)
                                    redescribedPawnItem.PfiAssignmentType = PfiAssignment.Scrap;
                            }
                            if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString())
                                redescribedPawnItem.mDocNumber = ((PawnLoan)_PfiActiveLoan.UpdatedObject).OrigTicketNumber;
                            else if (_PfiActiveLoan.UpdatedObject.ProductType == ProductType.BUY.ToString())
                            {
                                redescribedPawnItem.mDocNumber = string.IsNullOrEmpty(((PurchaseVO)_PfiActiveLoan.UpdatedObject).RefType) ? ((PurchaseVO)_PfiActiveLoan.UpdatedObject).TicketNumber : ((PurchaseVO)_PfiActiveLoan.UpdatedObject).RefNumber;
                            }

                            // Set up Pawn Item for PFI Page
                            if (redescribedPawnItem.CaccLevel == 0 && redescribedPawnItem.ItemReason==ItemReason.BLNK)
                                redescribedPawnItem.ItemReason = ItemReason.CACC;

                            redescribedPawnItem.Icn = _PfiActiveLoan.UpdatedObject.Items[e.RowIndex].Icn;
                            redescribedPawnItem.PfiVerified = true;

                            if (iDx > 0)
                            {
                                PFI_Merged pfiMerged = new PFI_Merged()
                                {
                                    NewItem = Utilities.CloneObject<Item>(redescribedPawnItem),
                                    OriginalItems = new List<Item>()
                                };
                                pfiMerged.OriginalItems.Add(Utilities.CloneObject<Item>(_PfiActiveLoan.UpdatedObject.Items[e.RowIndex]));
                                _PfiActiveLoan.MergedItems.Add(pfiMerged);
                            }

                            _PfiActiveLoan.UpdatedObject.Items.RemoveAt(e.RowIndex);
                            //need to update the icn of the jewelry items if this is a re-describe merged item
                            if (redescribedPawnItem.IsJewelry && redescribedPawnItem.Jewelry.Count > 0)
                            {
                                if (redescribedPawnItem.Jewelry[0].Icn != null &&
                                    !redescribedPawnItem.Jewelry[0].Icn.Substring(0, redescribedPawnItem.Jewelry[0].Icn.Length - 2).Equals(
                                        redescribedPawnItem.Icn.Substring(0, redescribedPawnItem.Jewelry[0].Icn.Length - 2)))
                                    for (int i = 0; i < redescribedPawnItem.Jewelry.Count; i++)
                                    {
                                        string jeweryICN = ("00" + (i + 1).ToString()).Substring(("00" + (i + 1).ToString()).Length - 3, 3);
                                        redescribedPawnItem.Jewelry[i].Icn =
                                        redescribedPawnItem.Icn.Substring(0, redescribedPawnItem.Jewelry[0].Icn.Length - 2) +
                                        jeweryICN;
                                    }
                            }
                            _PfiActiveLoan.UpdatedObject.Items.Insert(e.RowIndex, redescribedPawnItem);
                        }
                        else if (tempPawnItem.HoldType == "2")
                        {
                            tempPawnItem.PfiVerified = true;
                            _PfiActiveLoan.UpdatedObject.Items.RemoveAt(e.RowIndex);
                            _PfiActiveLoan.UpdatedObject.Items.Insert(e.RowIndex, tempPawnItem);
                        }

                        // Update DataGrid in case of changes from Describe Item
                        UpdateDataGrid(customerNameLabel.Text);
                        gvLoanItems.Rows[selectedRow].Selected = true;
                        gvLoanItems.FirstDisplayedScrollingRowIndex = selectedRow;

                        //undoButton.Enabled = UndoButtonEnablement(0); //commented out by Drew 
                        undoButton.Enabled = UndoButtonEnablement(e.RowIndex);

                        // Reset Active Pawn Loan or purchase back to Original
                        if (!pfiVerifyPurchase)
                        {
                            if (activePawnLoan != null)
                            {
                                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan = activePawnLoan;
                            }
                        }
                        else if (activePurchase != null)
                        {
                            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase = activePurchase;
                        }
                    }
                }
                else
                {
                    gvLoanItems.Rows[e.RowIndex].Cells[colMerge.Name].Value = false;
                    gvLoanItems.Rows[e.RowIndex].Cells[colCost.Name].Selected = true;
                }
            }
        }

        private void gvLoanItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!_Setup)
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (_PageState == PageState.Merge)
                    {
                        gvLoanItems.EndEdit();
                        int iRowIdx = e.RowIndex;
                        if (gvLoanItems.Columns[e.ColumnIndex].Name == gvLoanItems.Columns[colMerge.Name].Name)
                        {
                            if (Utilities.GetBooleanValue(gvLoanItems.Rows[iRowIdx].Cells[colMerge.Name].Value, false))
                            {
                                if (MergeEnabled(iRowIdx))
                                    _SelectedMergeRowIndex.Enqueue(_SelectedRowIndex);
                                else
                                {
                                    gvLoanItems.Rows[iRowIdx].Cells[colMerge.Name].Value = false;
                                    gvLoanItems.Rows[iRowIdx].Cells[colCost.Name].Selected = true;
                                }
                            }
                            else
                            {
                                if (_SelectedMergeRowIndex.Count() > 0)
                                    _SelectedMergeRowIndex.Dequeue();
                            }
                        }
                    }
                    PageStateButtons();
                }
            }
        }

        private bool MergeEnabled(int iMergeRowIdx)
        {
            bool bEnable = true;
            PfiAssignment selectedAssignmentRow = (PfiAssignment)Enum.Parse(typeof(PfiAssignment), gvLoanItems.Rows[iMergeRowIdx].Cells[colAssignmentType.Name].Value.ToString());

            foreach (DataGridViewRow myRow in gvLoanItems.Rows)
            {
                if (Utilities.GetBooleanValue(myRow.Cells[colMerge.Name].Value, false))
                {
                    PfiAssignment searchedAssignmentRow = (PfiAssignment)Enum.Parse(typeof(PfiAssignment), myRow.Cells[colAssignmentType.Name].Value.ToString());
                    if (selectedAssignmentRow != searchedAssignmentRow)
                    {
                        bEnable = false;
                        break;
                    }
                }
            }
            return bEnable;
        }

        private void gvLoanItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!_Setup)
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    gvLoanItems.EndEdit();
                    PageStateButtons();
                }
            }
        }

        private void gvLoanItems_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!_Setup)
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    _SelectedRowIndex = e.RowIndex;

                    if (gvLoanItems.Columns[colAssignmentType.Name].Index == e.ColumnIndex && _PageState == PageState.Verify)
                    {
                        bool bIsGun = _PfiActiveLoan.UpdatedObject.Items[e.RowIndex].IsGun;
                        bool bIsjewelry = _PfiActiveLoan.UpdatedObject.Items[e.RowIndex].IsJewelry;

                        DataGridViewComboBoxCell myCombo = (DataGridViewComboBoxCell)gvLoanItems.Rows[e.RowIndex].Cells[colAssignmentType.Name];

                        for (int i = 0; i < myCombo.Items.Count; i++)
                        {
                            if (FilterAssignmentTypes(bIsGun, bIsjewelry, myCombo.Items[i].ToString()))
                            {
                                myCombo.Items.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
                else
                    _SelectedRowIndex = -1;

                if (_PageState == PageState.Verify)
                    undoButton.Enabled = UndoButtonEnablement(e.RowIndex);
            }
        }

        private bool FilterAssignmentTypes(bool bIsGun, bool bIsJewelry, string sAssignmentType)
        {
            bool bFilter = false;

            if (bIsJewelry)
            {
                bFilter = !(sAssignmentType == PfiAssignment.Scrap.ToString()
                            || sAssignmentType == PfiAssignment.Normal.ToString()
                            || sAssignmentType == PfiAssignment.Refurb.ToString()
                            || sAssignmentType == PfiAssignment.Sell_Back.ToString()
                            || sAssignmentType == PfiAssignment.Excess.ToString()
                            );
            }
            else if (bIsGun)
            {
                bFilter = !(sAssignmentType == PfiAssignment.Normal.ToString()
                            || sAssignmentType == PfiAssignment.CAF.ToString()
                            || sAssignmentType == PfiAssignment.Sell_Back.ToString()
                            );
            }
            else
            {
                bFilter = !(sAssignmentType == PfiAssignment.Normal.ToString()
                            || sAssignmentType == PfiAssignment.Sell_Back.ToString()
                            );
            }
            return bFilter;
        }

        private bool UndoButtonEnablement(int iRowIndex)
        {
            bool bEnableButton = false;

            string sHoldType = Utilities.GetStringValue(_PfiActiveLoan.UpdatedObject.Items[iRowIndex].HoldType);
            string sReason = Utilities.GetStringValue(gvLoanItems.Rows[iRowIndex].Cells[colReason.Name].Value, "");

            if (sHoldType == "2")
                sReason = "Police Hold";

            ItemReasonCode reasonCode = ItemReasonFactory.Instance.FindByDescription(sReason);

            int iDx = _PfiActiveLoan.MergedItems.FindIndex(delegate(PFI_Merged pfm)
                                                           {
                                                               return pfm.NewItem.Icn == _PfiActiveLoan.UpdatedObject.Items[iRowIndex].Icn;
                                                           });

            if (iRowIndex < _PfiActiveLoan.OriginalObject.Items.Count())
            {
                PfiAssignment oPfiAssignment = _PfiActiveLoan.OriginalObject.Items[iRowIndex].PfiAssignmentType;
                string oTicketDescription = _PfiActiveLoan.OriginalObject.Items[iRowIndex].TicketDescription;
                int oTags = _PfiActiveLoan.OriginalObject.Items[iRowIndex].PfiTags;
                int oRefurbNumber = _PfiActiveLoan.OriginalObject.Items[iRowIndex].RefurbNumber;
                decimal oCostAmount = _PfiActiveLoan.OriginalObject.Items[iRowIndex].ItemAmount;

                PfiAssignment uPfiAssignment = _PfiActiveLoan.UpdatedObject.Items[iRowIndex].PfiAssignmentType;
                string uTicketDescription = _PfiActiveLoan.UpdatedObject.Items[iRowIndex].TicketDescription;
                int uTags = _PfiActiveLoan.UpdatedObject.Items[iRowIndex].PfiTags;
                int uRefurbNumber = _PfiActiveLoan.UpdatedObject.Items[iRowIndex].RefurbNumber;
                //decimal uCostAmount = _PfiActiveLoan.UpdatedLoan.PawnItems[iRowIndex].ItemAmount;
                decimal uCostAmount = _PfiActiveLoan.UpdatedObject.Items[iRowIndex].ItemAmount;
                bEnableButton =
                (
                iDx >= 0
                || oPfiAssignment != uPfiAssignment
                || oTicketDescription != uTicketDescription
                || oTags != uTags
                || oRefurbNumber != uRefurbNumber
                || oCostAmount != uCostAmount
                )
                ||
                (
                reasonCode.Reason == ItemReason.COFFBRKN
                || reasonCode.Reason == ItemReason.CACC
                || reasonCode.Reason == ItemReason.NOMD
                || reasonCode.Reason == ItemReason.COFFNXT
                || reasonCode.Reason == ItemReason.COFFSTLN
                || reasonCode.Reason == ItemReason.COFFSTRU
                    //added by drew
                    //|| pairType.Left == PawnItemReason.BLNK
                    //|| pairType.Left == PawnItemReason.ADDD
                    //|| pairType.Left == PawnItemReason.HPFI
                    //|| pairType.Left == PawnItemReason.MERGED
                );
            }
            else if (iRowIndex >= _PfiActiveLoan.OriginalObject.Items.Count())
            {
                // Added Items
                bEnableButton =
                iDx >= 0
                ||
                (
                reasonCode.Reason == ItemReason.ADDD
                || reasonCode.Reason == ItemReason.COFFBRKN
                || reasonCode.Reason == ItemReason.CACC
                || reasonCode.Reason == ItemReason.NOMD
                || reasonCode.Reason == ItemReason.COFFNXT
                || reasonCode.Reason == ItemReason.COFFSTLN
                || reasonCode.Reason == ItemReason.COFFSTRU
                );
            }
            else
            {
                bEnableButton = true;
            }

            return bEnableButton;
        }

        #region Serialization
        // Deserialize the saved PFI Suspended data from the File System
        private bool GetSuspendData(int iTicketNumber, StateStatus stateStatus, ref PFI_ProductData vdData)
        {
            try
            {
                vdData = new PFI_ProductData();

                var sErrorCode = string.Empty;
                var sErrorText = string.Empty;
                List<PFI_TransitionData> transitionDatas;
                decimal dStatusCode = 0;
                if (!pfiVerifyPurchase)
                {
                    StoreLoans.GetLoanTransition(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                 iTicketNumber,
                                                 ProductType.PAWN,
                                                 stateStatus,
                                                 out transitionDatas,
                                                 out dStatusCode,
                                                 out sErrorCode,
                                                 out sErrorText);
                }
                else
                {
                    StoreLoans.GetLoanTransition(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                 iTicketNumber,
                                                 ProductType.BUY,
                                                 stateStatus,
                                                 out transitionDatas,
                                                 out dStatusCode,
                                                 out sErrorCode,
                                                 out sErrorText);
                }

                if (transitionDatas.Count > 0)
                {
                    vdData = transitionDatas[0].pfiLoan;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error retrieving prior Suspended data.  " + exp.Message, "File Retrieval Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        // Serialize current List of PFI Loans and saved to File System for temporary storage
        private bool SetSuspendData()
        {
            try
            {
                var sErrorCode = string.Empty;
                var sErrorText = string.Empty;

                _PfiActiveLoan.UpdatedObject.TempStatus = StateStatus.PFIS;

                StoreLoans.SetLoanTransition(
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    _PfiActiveLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString()
                    ? ProductType.PAWN : ProductType.BUY,
                    _PfiActiveLoan.UpdatedObject.TicketNumber,
                    Utilities.Serialize<PFI_ProductData>(_PfiActiveLoan),
                    StateStatus.PFIS,
                    ShopDateTime.Instance.ShopDate,
                    out sErrorCode,
                    out sErrorText);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error saving Suspended data.  " + exp.Message, "File Retrieval Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        //SR 8/31/2010 Commenting since its not called anywhere in the code
        /*  private bool beginTransaction(string section)
        {
        //Start transaction block
        bool finished = false;

        while (!finished)
        {
        try
        {
        CashlinxDesktopSession.Instance.beginTransactionBlock();
        finished = true;
        }
        catch (Exception eX)
        {
        DialogResult dR = MessageBox.Show("Cannot start loan database transaction (" + section + "). Please retry or cancel", "Process Tender", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        if (dR == DialogResult.Cancel)
        {
        if (FileLogger.Instance.IsLogFatal)
        {
        FileLogger.Instance.logMessage(LogLevel.FATAL, ProcessTenderController.Instance, "User chose to cancel process tender operation (" + section + "). No loan data has been committed");
        }

        BasicExceptionHandler.Instance.AddException("User cancelled pfi (" + section + ").  No loan data has been committed", eX);
        return (false);
        }
        }
        }
        return (finished);
        }

        private bool commitTransaction(string section)
        {

        bool finished = false;
        while (!finished)
        {
        try
        {
        CashlinxDesktopSession.Instance.endTransactionBlock(EndTransactionType.COMMIT);
        finished = true;
        }

        catch (Exception eX)
        {
        DialogResult dR = MessageBox.Show("Cannot put loan data into database (" + section + "). Please retry or cancel", "PFI Posting", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        if (dR == DialogResult.Cancel)
        {
        if (FileLogger.Instance.IsLogFatal)
        {
        FileLogger.Instance.logMessage(LogLevel.FATAL, ProcessTenderController.Instance, "User chose to cancel process tender operation (" + section + "). No loan data has been committed");
        }

        CashlinxDesktopSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
        BasicExceptionHandler.Instance.AddException("User cancelled pfi posting (" + section + ").  No loan data has been committed", eX);
        return (false);
        }
        }
        }
        return (finished);
        }*/

        #endregion

        private void gvLoanItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox assignmentTypeCombo = e.Control as ComboBox;
            assignmentTypeCombo.SelectedIndexChanged += new EventHandler(assignmentTypeCombo_SelectedIndexChanged);
        }

        void assignmentTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox assignmentTypeCombo = sender as ComboBox;
            string sAssignmentType = assignmentTypeCombo.Text;
            DataGridViewRow row = gvLoanItems.CurrentRow;
            Item item = _PfiActiveLoan.UpdatedObject.Items[row.Index];
            List<int> currentRefurbNumbers= (from DataGridViewRow dgvr in gvLoanItems.Rows
                                             select _PfiActiveLoan.UpdatedObject.Items[dgvr.Index]
                                             into currItem select currItem.RefurbNumber).ToList();

            if (!item.PfiAssignmentType.ToString().Equals(sAssignmentType) && sAssignmentType == PfiAssignment.Refurb.ToString())
            {
                PFI_AssignmentTypeRefurb refurbForm = new PFI_AssignmentTypeRefurb
                                                      {
                                                              Item = item,
                                                              CurrentRefurbNumbers =
                                                                      currentRefurbNumbers
                                                      };
                refurbForm.ShowDialog();
                
            }
            if (_PfiActiveLoan.UpdatedObject.Items[row.Index].PfiAssignmentType == PfiAssignment.Scrap &&
                _PfiActiveLoan.UpdatedObject.Items[row.Index].PfiAssignmentType != (PfiAssignment)Enum.Parse(typeof(PfiAssignment), sAssignmentType))
            {
                if (_PfiActiveLoan.UpdatedObject.Items[row.Index].RetailPrice == 0)
                {
                    _PfiActiveLoan.UpdatedObject.Items[row.Index].PfiVerified = false;
                    row.Cells[colVerify.Name].Value = false;

                }
            }
            _PfiActiveLoan.UpdatedObject.Items[row.Index].PfiAssignmentType = (PfiAssignment)Enum.Parse(typeof(PfiAssignment), sAssignmentType);
        }

        private void ticketNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                FindTicket();
        }
    }
}
