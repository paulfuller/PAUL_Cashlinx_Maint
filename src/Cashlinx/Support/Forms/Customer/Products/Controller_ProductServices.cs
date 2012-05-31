/************************************************************************************************
* Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products.ProductDetails
* Class:           Controller_ProductServices
* 
* Description      Form used by Controller to View/Edit Customer Pawn Loans
* 
* History
* David D Wise, Initial Development
* Sreelatha Rengarajan Added Pickup process
*                      Added Extend Process
*  SMurphy no ticket 3/16/2010 don't add to Additional Tickets if it's already in the original 
*  or Additional lists
* Sreelatha Rengarajan 4/1/2010 Fixed PWN 589 wherein if the customer did not have
* any loans but you were trying to service a loan for this customer as a non pledgor it errored
* SR 4/7/2010 Added logic in the receipt display section to show the pawn loan amount
* if the receipt event was renew or paydown since in the database we store the receipt amount
* as 0 for a renew or paydown against the new loan.
* SR 4/9/2010 Added logic to enable applying lost ticket fee on a rollover(renew or paydown) stemming
* from a production issue but which was not part of the original requirements.
* SR 5/21/2010 Fixed an issue wherein lost ticket could not be applied to multiple loans
* SR 5/26/2010 IF a loan is deselected changed the selection color on it. Also added logic to 
* default selection to the looked up ticket
**************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Pawn.Products.DescribeMerchandise;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

using Common.Libraries.Utility.Type;
//using Pawn.Forms.Layaway;
using Support.Flows.AppController.Impl.Common;
using Support.Forms.Customer.Pawn.Loan;
using Support.Forms.Customer.Products.Layaway;
using Support.Forms.Customer.Services.Pickup;
using Support.Forms.Customer.Services.Rollover;
using Support.Libraries.Objects.PDLoan;
using Support.Libraries.Utility;
using Support.Logic;
using Support.Logic.DesktopProcedures;
using Support.Libraries.Objects.PDLoan;
/*using Pawn.Forms.Pawn.Loan;
using Pawn.Forms.Pawn.Loan.ProcessTender;
using Pawn.Forms.Pawn.Services.Extend;
using Pawn.Forms.Pawn.Services.Pickup;
using Pawn.Forms.Pawn.Services.Receipt;
using Pawn.Forms.Pawn.Services.Rollover;
using Pawn.Forms.Pawn.Services.Ticket;
using Pawn.Forms.Report;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;*/
using Document = Common.Libraries.Objects.Doc.Document;

namespace Support.Forms.Customer.Products
{
    public partial class Controller_ProductServices : Form
    {
        #region PRIVATES
        public NavBox NavControlBox;

        private List<Document> _ActiveLoanDocuments;
        private List<PawnLoan> _AuxillaryLoanKeys = new List<PawnLoan>();
        private List<PawnLoan> _LoanKeys;
        private List<LayawayVO> _SelectedLayaways;
        private List<int> _selectedLoanNumbers = new List<int>();
        private List<PawnLoan> _SelectedLoans;
        private List<PDLoan> _PDLoanKeys;
        private List<PDLoan> PDLLoanList = CashlinxPawnSupportSession.Instance.PDLoanKeys;

        List<int> overrideTransactionNumbers = new List<int>();
        List<CouchDbUtils.PawnDocInfo> pawnDocs;

        private ReceiptLookupInfo _currReceiptLookupInfo = null;
        private SiteId currentStoreSiteId;
        private string strStoreNumber = "";

        private bool _Setup;
        private bool _continuePickupProcess;
        private bool _gunItem = false;
        private bool _gunItemIdValidated = false;


        private int _currentTicketNumber = 0;
        private int _TicketNumber = 0;

        private decimal _totalExtensionAmount = 0.0M;
        private decimal _totalPaydownAmount = 0.0M;
        private decimal _totalPickupAmount = 0.0M;
        private decimal _totalRenewalAmount = 0.0M;
        private decimal _totalServiceAmount;

        private bool ctrlKeyPressed;
        private bool extensionServiceAllowed;
        private bool paydownAllowed;
        private bool renewalAllowed;
        private bool partialPaymentAllowed;
        private bool loanRemoved;
        private bool loanupServiceAllowed;
        private int lookedUpTicketIndex;
        
        private bool showRefreshIcon;
        
        private bool ticketSearched;
        private string lastLayawayPayment = string.Empty;
        private readonly string DECLINE_VALUE = "DECLINED";
        #endregion
        #region CONSTRUCTOR
        /*__________________________________________________________________________________________*/
        public Controller_ProductServices(int iTicketNumber)
        {
            _Setup = false;
            InitializeComponent();

            //  Code from designer PS_ShowComboBox to put back in place when other products are active.
            // this.PS_ShowComboBox.Items.AddRange(new object[] {
            //"Pawn",
            //"Layaway",
            //"PDL/INST"});
            this.labelServiceAmountHeading.Visible = false;
            this.labelServiceAmount.Visible = false;
            this.PS_ShowComboBox.Text = "PDL/INST";
            //PS_ShowComboBox.SelectedIndex = 2;
            this.NavControlBox = new NavBox();
            _TicketNumber = iTicketNumber;

            if (_TicketNumber == 0)
            {
                MessageBox.Show("No valid ticket number was passed in.", "Ticket Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Setup();
        }
        /*__________________________________________________________________________________________*/
        public Controller_ProductServices()
        {
            _Setup = false;
            InitializeComponent();
            this.NavControlBox = new NavBox();
            Setup();
        }
        #endregion
        #region FORMSTARTUP
        /*__________________________________________________________________________________________*/
        private void Setup()
        {
            ShowPawnPanels();
            
            this.NavControlBox.Owner = this;
            currentStoreSiteId = new SiteId()
            {
                Alias = GlobalDataAccessor.Instance.CurrentSiteId.Alias,
                Company = GlobalDataAccessor.Instance.CurrentSiteId.Company,
                Date = ShopDateTime.Instance.ShopDate,
                LoanAmount = 0,
                State = GlobalDataAccessor.Instance.CurrentSiteId.State,
                StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                TerminalId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId
            };

            _SelectedLoans = new List<PawnLoan>();
            _SelectedLayaways = new List<LayawayVO>();

            if (GlobalDataAccessor.Instance.DesktopSession.ServicePawnLoans)
                PS_ShowComboBox.Enabled = false;
            if (_TicketNumber == 0)
                _TicketNumber = GlobalDataAccessor.Instance.DesktopSession.TicketLookedUp;

            switch (Support.Logic.CashlinxPawnSupportSession.Instance.TicketTypeLookedUp)
            {
                case SupportProductType.LAYAWAY:
                    PS_ShowComboBox.SelectedIndex = 1;
                    break;
                default: // default to Pawn
                    PS_ShowComboBox.SelectedIndex = 0;
                    break;
            }

            LW_DetailsLayoutPanel.Visible = false;

            PS_PawnLoanLabel.Visible = false;
            PS_PawnNameLabel.Visible = false;
            PS_ItemDescriptionDataGridView.Visible = false;
            PS_LoanStatsLayoutPanel.Visible = false;
            tlpDocuments.Visible = false;

            //get the store number from session
            strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            if (string.Equals(
                Properties.Resources.MultipleLoanSelection,
                Boolean.TrueString, StringComparison.OrdinalIgnoreCase))
            {
                PS_TicketsDataGridView.MultiSelect = true;
                PS_AddTicketsDataGridView.MultiSelect = true;
            }
            else
            {
                PS_TicketsDataGridView.MultiSelect = false;
                PS_AddTicketsDataGridView.MultiSelect = false;
            }
            if (GlobalDataAccessor.Instance.DesktopSession.LockProductsTab)
                this.customButtonCancel.Visible = false;
            else
                this.customButtonCancel.Visible = true;

            //if (CmbLoanStatus.SelectedIndex == -1)
            //    this.CmbLoanStatus.SelectedItem = 1;
            PS_ShowComboBox.SelectedIndex = 2;
            _Setup = true;
        }
        /*__________________________________________________________________________________________*/
        private void Controller_ProductServices_Shown(object sender, EventArgs e)
        {
            //if (_Setup)
            //{
                //if (GlobalDataAccessor.Instance.DesktopSession.PawnLoanKeys.Count() != PS_TicketsDataGridView.Rows.Count)
                //if (Support.Logic.CashlinxPawnSupportSession.Instance.PawnLoanKeys.Count() != PS_TicketsDataGridView.Rows.Count)
                    LoadData(GetSelectedProductType());
                //else if (GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.Count != PS_AddTicketsDataGridView.Rows.Count)
                //    LoadAdditionalTicketsData();
            //}
        }
        /*__________________________________________________________________________________________*/
        private void ShowPawnPanels()
        {
            //SupportProductType loantype = GetSelectedProductType();
            
            LW_LayawayButtonsPanel.Location = PS_PawnButtonsPanel.Location;
            PS_PawnButtonsPanel.Visible = false;
            LW_LayawayButtonsPanel.Visible = false;
            PS_PawnButtonsPanel.BringToFront();

            LW_DetailsLayoutPanel.Location = PS_LoanStatsLayoutPanel.Location;
            PS_LoanStatsLayoutPanel.Visible = true;
            LW_DetailsLayoutPanel.Visible = false;
            PS_LoanStatsLayoutPanel.BringToFront();
        
        }
        /*__________________________________________________________________________________________*/
        private void ShowStatusValue(PawnLoan pawnLoan)
        {
            //SR 9/4/09 If there are holds on the loan or if the loan is in a PFI processing state show it
            //otherwise no status is shown
            if (Utilities.IsPFI(pawnLoan.TempStatus) || pawnLoan.HoldDesc != string.Empty)
            {
                PS_StatusValue.Text = pawnLoan.HoldDesc != string.Empty ? Utilities.GetStringValue(pawnLoan.HoldDesc) : pawnLoan.TempStatus.ToString();
                PS_StatusValue.ForeColor = Color.Red;
                PS_StatusValue.Visible = true;
                PS_StatusLabel.Visible = true;
                PS_StatusLabel.Location = new Point(67, 73);
                PS_StatusValue.Location = new Point(181, 73);
                PS_LastDayOfGraceLabel.Location = new Point(60, 89);
                PS_LastDayOfGraceValue.Location = new Point(181, 89);
                PS_ReceiptNoLabel.Location = new Point(67, 106);
                PS_ReceiptNoValue.Location = new Point(181, 106);
            }
            else
            {
                PS_StatusValue.Visible = false;
                PS_StatusLabel.Visible = false;
                PS_LastDayOfGraceLabel.Location = new Point(67, 73);
                PS_ReceiptNoLabel.Location = new Point(67, 89);
                PS_LastDayOfGraceValue.Location = new Point(181, 73);
                PS_ReceiptNoValue.Location = new Point(181, 89);
            }
            //End of changes SR
        }
    /*__________________________________________________________________________________________*/
        private bool SearchGrid(string loan, DataGridView grid)
        {
            //no ticket SMurphy 3/16/2010 don't add to Additional Tickets if it's already in the original or Additional lists
            //this looks thru gridviews so a message box can be displayed when attempting to add duplicates
            bool found = false;

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells[2].Value != null)
                    if (row.Cells[2].Value.ToString().Substring(5) == loan)
                    {
                        found = true;
                        break;
                    }
            }

            return found;
        }
        /*__________________________________________________________________________________________*/
        private bool ShouldEnableUndoButton()
        {
            if (GetSelectedProductType() == SupportProductType.PAWN)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Count == 0 || _SelectedLoans.Count == 0)
                {
                    return false;
                }

                bool hasPawnInService = false;
                foreach (PawnLoan loan in _SelectedLoans)
                {
                    bool loanExists = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Exists(pl => pl.TicketNumber == loan.TicketNumber);
                    if (loanExists)
                    {
                        hasPawnInService = true;
                        break;
                    }
                }

                return hasPawnInService;
            }
            else
            {
                if (GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.Count == 0 || _SelectedLayaways.Count == 0)
                {
                    return false;
                }

                bool hasLayawayInService = false;
                foreach (LayawayVO layaway in _SelectedLayaways)
                {
                    bool layawayExists = GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.Exists(lw => lw.TicketNumber == layaway.TicketNumber);
                    if (layawayExists)
                    {
                        hasLayawayInService = true;
                        break;
                    }
                }

                return hasLayawayInService;
            }
        }
        /*__________________________________________________________________________________________*/
        private void ShowLayawayPanels()
        {
            LW_LayawayButtonsPanel.Location = PS_PawnButtonsPanel.Location;
            PS_PawnButtonsPanel.Visible = false;
            LW_LayawayButtonsPanel.Visible = true;
            LW_LayawayButtonsPanel.BringToFront();

            LW_DetailsLayoutPanel.Location = PS_LoanStatsLayoutPanel.Location;
            PS_LoanStatsLayoutPanel.Visible = false;
            LW_DetailsLayoutPanel.Visible = true;
            LW_DetailsLayoutPanel.BringToFront();
        }
        #endregion
        #region MEMBER PROPERTIES
        /*__________________________________________________________________________________________*/
        public decimal ServiceAmount
        {
            get
            {
                return _totalServiceAmount;
            }
            set
            {
                _totalServiceAmount = value;
                GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount = _totalServiceAmount;
                UpdateServiceAmountLabel();
            }
        }
        /*__________________________________________________________________________________________*/
        public bool ContinuePickup
        {
            get
            {
                return _continuePickupProcess;
            }
            set
            {
                _continuePickupProcess = value;
                if (_continuePickupProcess)
                {
                    if (GetSelectedProductType() == SupportProductType.LAYAWAY)
                    {
                        this.LayawayCheckout();
                    }
                    else
                    {
                        this.ContinuePawnPickupProcess();
                    }
                }
            }
        }
        #endregion
        #region GETDATA
        /*__________________________________________________________________________________________*/
        private void LoadData(SupportProductType productType)
        {
            PS_TicketsDataGridView.Rows.Clear();
            PS_AddTicketsDataGridView.Rows.Clear();

            Support.Logic.CashlinxPawnSupportSession.Instance.ServiceLoans = new List<PawnLoan>();
            Support.Logic.CashlinxPawnSupportSession.Instance.Layaways = new List<LayawayVO>();
            Support.Logic.CashlinxPawnSupportSession.Instance.ServiceLayaways = new List<LayawayVO>();

            lookedUpTicketIndex = 0;

            this.PS_Ticket_Status.Visible = false;
            this.PS_Ticket_Type.Visible = false;
            this.PS_Staus_Date.Visible = false;

            if (productType == SupportProductType.PAWN)
            {
                _LoanKeys = (from loankey in Support.Logic.CashlinxPawnSupportSession.Instance.PawnLoanKeys
                             where loankey.LoanStatus == ProductStatus.IP
                             select loankey).ToList();
                _AuxillaryLoanKeys = (from loankey in Support.Logic.CashlinxPawnSupportSession.Instance.PawnLoanKeysAuxillary
                                      where loankey.LoanStatus == ProductStatus.IP
                                      select loankey).ToList();

            }
            else if (productType == SupportProductType.LAYAWAY)
            {
                _LoanKeys = (from loankey in Support.Logic.CashlinxPawnSupportSession.Instance.PawnLoanKeys
                             where loankey.LoanStatus == ProductStatus.ACT && loankey.DocType == 4
                             select loankey).ToList();
                _AuxillaryLoanKeys = (from loankey in Support.Logic.CashlinxPawnSupportSession.Instance.PawnLoanKeysAuxillary
                                      where loankey.LoanStatus == ProductStatus.LAY && loankey.DocType == 4
                                      select loankey).ToList();
            }
            else if (productType == SupportProductType.PDL)
            {
                this.PS_Ticket_Status.Visible = true;
                this.PS_Ticket_Type.Visible = true;
                this.PS_Staus_Date.Visible = true;
                
                this.PS_Tickets_LastDayColumn.Visible = false;
                this.PS_Tickets_Refresh.Visible = false;
                this.PS_Tickets_Extend.Visible = false;
                this.PS_Tickets_ServiceIndicatorColumn.Visible = false;
                this.PS_AddTicketsLabel.Visible = false;
                this.PS_AddTicketsDataGridView.Visible = false;
            }
            else
            {
                _LoanKeys = new List<PawnLoan>();
                _AuxillaryLoanKeys = new List<PawnLoan>();
                _PDLoanKeys = new List<PDLoan>();
            }


            if (productType == SupportProductType.PDL)
            {

                var _PDLoanKeys = Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys;

                var tempLoanKeys = new List<PDLoan>();
                if (CmbLoanStatus.SelectedIndex == 1)
                    tempLoanKeys = _PDLoanKeys.FindAll(plk => (plk.open_closed == "CLOSED"));
                else if (CmbLoanStatus.SelectedIndex == 2)
                    tempLoanKeys = _PDLoanKeys.FindAll(plk => (plk.open_closed == "OPEN"));
                else
                    tempLoanKeys = _PDLoanKeys;

                Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan = new PDLoan();
                MapPDL_LoanStatsFromProperties(new PDLoanDetails());
                MapPDL_EventsFromProperties(new PDLoanDetails());
                MapPDL_xppLoanScheduleFromProperties(new List<PDLoanXPPScheduleList>());
                MapPDL_HistoryFromProperties(new List<PDLoanHistoryList>());

                this.LnkOtherDetails.Enabled = false;
                this.btnExtendDeposit.Enabled = false;
                this.ChkBGetAllHistory.Checked = false;
                this.ChkBGetAllHistory.Enabled = false;
                this.CmbHistoryLoanEvents.SelectedIndex = 0;
                this.CmbHistoryLoanEvents.Enabled = false;

                for (int i = 0; i < tempLoanKeys.Count(); i++)
                {
                    int gvIdx = PS_TicketsDataGridView.Rows.Add();
                    var myRow = PS_TicketsDataGridView.Rows[gvIdx];
                    myRow.Cells["PS_Tickets_TicketNumberColumn"].Value = tempLoanKeys[i].PDLLoanNumber;
                    myRow.Cells["PS_Staus_Date"].Value = tempLoanKeys[i].StatusDate.FormatDate();
                    myRow.Cells["PS_Ticket_Type"].Value = tempLoanKeys[i].Type;
                    myRow.Cells["PS_Ticket_Status"].Value = tempLoanKeys[i].Status;
                    myRow.Cells["LoanApplicationId"].Value = tempLoanKeys[i].LoanApplicationId;
                    if (tempLoanKeys[i].Status.Equals(DECLINE_VALUE))
                    {
                        myRow.Cells["PS_Ticket_Status"].Style.ForeColor = Color.Blue;
                        myRow.Cells["PS_Ticket_Status"].Style.Font = new Font(PS_TicketsDataGridView.Font, FontStyle.Bold);
                    }
                }

                PS_TicketsDataGridView.ClearSelection();
            }
            else
            {

                for (int i = 0; i < _LoanKeys.Count(); i++)
                {
                    int gvIdx = PS_TicketsDataGridView.Rows.Add();

                    DataGridViewRow myRow = PS_TicketsDataGridView.Rows[gvIdx];
                    //myRow.Cells["PS_Tickets_SelectColumn"].Value = false;
                    if (_LoanKeys[i].IsExtended)
                        myRow.Cells["PS_Tickets_Extend"].Value = "E";

                    myRow.Cells["PS_Tickets_TicketNumberColumn"].Value = Utilities.GetStringValue(
                        _LoanKeys[i].OrgShopNumber, "").PadLeft(
                            5, '0') +
                                                                         Utilities.GetStringValue(
                                                                             _LoanKeys[i].TicketNumber, "");
                    DateTime dtLastDayGrace = Utilities.GetDateTimeValue(_LoanKeys[i].PfiEligible, DateTime.MinValue);
                    if (dtLastDayGrace != DateTime.MinValue)
                        myRow.Cells["PS_Tickets_LastDayColumn"].Value = dtLastDayGrace;
                    else
                        myRow.Cells["PS_Tickets_LastDayColumn"].Value = string.Empty;

                    //TODO: This should be a rule!!
                    if (productType != SupportProductType.LAYAWAY)
                    {
                        if (dtLastDayGrace <= ShopDateTime.Instance.ShopDate.AddDays(-5))
                            PS_TicketsDataGridView.Rows[gvIdx].DefaultCellStyle.ForeColor = Color.Red;
                    }

                    //_OrigTicketNumber = Utilities.GetIntegerValue(_LoanKeys[i].OrigTicketNumber, 0);

                    if (_TicketNumber == Utilities.GetIntegerValue(_LoanKeys[i].TicketNumber, 0))
                    {
                        lookedUpTicketIndex = i;
                        PS_TicketsDataGridView.CurrentCell = PS_TicketsDataGridView.Rows[gvIdx].Cells[0];
                        ticketSearched = true;
                        //_ActiveLoanIndex = i;
                    }
                }
                //Check if the store state allows Extension, renewal, paydown and loan up services
                //extensionServiceAllowed = new BusinessRulesProcedures(Support.Logic.CashlinxPawnSupportSession.Instance).IsExtensionAllowed(currentStoreSiteId);
                //loanupServiceAllowed = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsLoanUpAllowed(currentStoreSiteId);
                //paydownAllowed = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPayDownAllowed(currentStoreSiteId);
                //renewalAllowed = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsRenewalAllowed(currentStoreSiteId);
                //partialPaymentAllowed = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(currentStoreSiteId);

                //Show details of the looked up ticket row
                //If not coming here through lookup ticket show the first row
                if (_LoanKeys.Count > 0)
                {
                    if (_LoanKeys.Count > lookedUpTicketIndex && IsLayawayPawnKey(_LoanKeys[lookedUpTicketIndex]))
                    {
                        ApplyLayawayBusinessRules(_LoanKeys[lookedUpTicketIndex].OrgShopNumber, _LoanKeys[lookedUpTicketIndex].TicketNumber, true);

                        if (ticketSearched)
                        {
                            UpdateActiveLayawayInformation(_LoanKeys[lookedUpTicketIndex].TicketNumber);
                            PS_TicketsDataGridView.Rows[PS_TicketsDataGridView.CurrentCell.RowIndex].Selected = true;

                        }
                        else
                        {
                            UpdateTicketSelections();
                        }

                    }
                    else
                    {
                        if (!ticketSearched)
                        {
                            //ApplyBusinessRules(_LoanKeys[lookedUpTicketIndex], _LoanKeys[lookedUpTicketIndex].OrgShopNumber, _LoanKeys[lookedUpTicketIndex].TicketNumber,
                            //                   false, true);
                            UpdateTicketSelections();
                        }
                        else
                        {
                            ApplyBusinessRules(
                                _LoanKeys[lookedUpTicketIndex], _LoanKeys[lookedUpTicketIndex].OrgShopNumber, _LoanKeys[lookedUpTicketIndex].TicketNumber,
                                true, true);
                            UpdateActivePawnInformation(_LoanKeys[lookedUpTicketIndex].TicketNumber);
                            PS_TicketsDataGridView.Rows[PS_TicketsDataGridView.CurrentCell.RowIndex].Selected = true;
                        }
                    }

                    //TLR - not making the documents accessible even though we're loading up ticket info.
                    //This will make the document appears as well.
                    LoadDocuments(_LoanKeys[lookedUpTicketIndex].TicketNumber, productType);
                }
                LoadAdditionalTicketsData();
            }  // end of NON PDL Logic
        }
        /*__________________________________________________________________________________________*/
        private void UpdateTicketSelections()
        {
            foreach (DataGridViewRow dgvr in PS_TicketsDataGridView.Rows)
            {
                dgvr.Selected = false;
                dgvr.DefaultCellStyle.SelectionBackColor = Color.White;
            }
            foreach (DataGridViewRow dgvr in PS_AddTicketsDataGridView.Rows)
            {
                dgvr.Selected = false;
                dgvr.DefaultCellStyle.SelectionBackColor = Color.White;
            }
            //Reset the selected loans list
            _SelectedLoans = new List<PawnLoan>();
            _SelectedLayaways = new List<LayawayVO>();
            if (_LoanKeys != null && _LoanKeys.Count > 0)
            {
                PawnLoan loankey = _LoanKeys[0];

                if (ticketSearched)
                {
                    loankey = _LoanKeys[lookedUpTicketIndex];
                }

                if (loankey.LoanStatus == ProductStatus.ACT && loankey.DocType == 4)
                {
                    ApplyLayawayBusinessRules(loankey.OrgShopNumber, loankey.TicketNumber, false);
                    UpdateActiveLayawayInformation(loankey.TicketNumber);
                }
                else
                {
                    ApplyBusinessRules(loankey, loankey.OrgShopNumber, loankey.TicketNumber, false, true);
                    UpdateActivePawnInformation(loankey.TicketNumber);
                }
            }
        }
        /*__________________________________________________________________________________________*/
        private void UpdateActiveLayawayInformation(int iTicketNumber)
        {
            LayawayVO layaway = new LayawayVO();

            PS_PawnLoanLabel.Visible = false;
            PS_PawnNameLabel.Visible = false;
            PS_ItemDescriptionDataGridView.Visible = false;
            PS_LoanStatsLayoutPanel.Visible = false;
            LW_DetailsLayoutPanel.Visible = false;
            PS_TicketsDataGridView.Columns[2].HeaderText = "Layaway Number";
            PS_TicketsDataGridView.Columns[3].HeaderText = "Last Payment Date";
            PS_ItemDescriptionDataGridView.Columns[colItemAmount.Index].HeaderText = "Sale Amount";

            int iDx = -1;
            if (GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.Count > 0)
            {
                iDx = GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.FindIndex(pl => pl.TicketNumber == iTicketNumber);
                if (iDx >= 0)
                {
                    layaway = GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways[iDx];
                }
            }

            //If the loan is not in the service loans list
            if (iDx < 0)
            {
                iDx = GlobalDataAccessor.Instance.DesktopSession.Layaways.FindIndex(pl => pl.TicketNumber == iTicketNumber);
                if (iDx >= 0)
                    layaway = GlobalDataAccessor.Instance.DesktopSession.Layaways[iDx];
            }

            if (_SelectedLayaways.Count > 1)
            {
                PS_ItemDescriptionDataGridView.Rows.Clear();
                PS_ItemDescriptionDataGridView.Visible = true;
                PS_ItemDescriptionDataGridView.Columns[PS_ItemDescriptionDataGridView_SelectedLoan.Index].Visible = true;
                PS_ItemDescriptionDataGridView.Columns[PS_ItemDescriptionDataGridView_SelectedLoan.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
                PS_ItemDescriptionDataGridView.Columns[PS_Description_ICNColumn.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
                PS_ItemDescriptionDataGridView.Columns[PS_Description_TicketDescriptionColumn.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
                PS_ItemDescriptionDataGridView.Columns[colStatus.Index].Visible = true;
                PS_ItemDescriptionDataGridView.Columns[colLocation.Index].Visible = true;
                PS_ItemDescriptionDataGridView.Columns[colItemAmount.Index].Visible = true;

                foreach (LayawayVO lw in _SelectedLayaways)
                {
                    bool printLoanNumber = true;
                    foreach (RetailItem pItem in lw.RetailItems)
                    {
                        int gvIdx = PS_ItemDescriptionDataGridView.Rows.Add();
                        DataGridViewRow myRow = PS_ItemDescriptionDataGridView.Rows[gvIdx];
                        //Show the loan number only once for a set of items belonging to the loan
                        myRow.Cells[PS_ItemDescriptionDataGridView_SelectedLoan.Index].Value = lw.TicketNumber;
                        if (printLoanNumber)
                        {
                            printLoanNumber = false;
                        }
                        //To do: Hide the cell contents if printloannumber is false
                        myRow.Cells[PS_Description_ICNColumn.Index].Value = pItem.Icn;
                        myRow.Cells[PS_Description_TicketDescriptionColumn.Index].Value = pItem.TicketDescription;
                        myRow.Cells[colStatus.Index].Value = pItem.ItemStatus.ToString();
                        myRow.Cells[colLocation.Index].Value = pItem.Location;
                        myRow.Cells[colItemAmount.Index].Value = pItem.RetailPrice.ToString("c");
                    }
                }

                LW_DetailsLayoutPanel.Visible = false;
            }
            else if (layaway.TicketNumber != 0)
            {
                var sFirstName = string.Empty;
                var sMiddleName = string.Empty;
                var sLastName = string.Empty;
                var sErrorCode = string.Empty;
                var sErrorText = string.Empty;

                PS_ItemDescriptionDataGridView.Rows.Clear();
                PS_ItemDescriptionDataGridView.Visible = true;
                PS_ItemDescriptionDataGridView.Rows.Clear();
                PS_ItemDescriptionDataGridView.Columns[PS_ItemDescriptionDataGridView_SelectedLoan.Index].Visible = false;
                PS_ItemDescriptionDataGridView.Columns[PS_Description_ICNColumn.Index].SortMode = DataGridViewColumnSortMode.Automatic;
                PS_ItemDescriptionDataGridView.Columns[PS_Description_TicketDescriptionColumn.Index].SortMode = DataGridViewColumnSortMode.Automatic;
                PS_ItemDescriptionDataGridView.Columns[colStatus.Index].Visible = true;
                PS_ItemDescriptionDataGridView.Columns[colLocation.Index].Visible = true;
                PS_ItemDescriptionDataGridView.Columns[colItemAmount.Index].Visible = true;

                for (int i = 0; i < layaway.RetailItems.Count; i++)
                {
                    RetailItem retailItem = layaway.RetailItems[i];

                    int gvIdx = PS_ItemDescriptionDataGridView.Rows.Add();
                    DataGridViewRow myRow = PS_ItemDescriptionDataGridView.Rows[gvIdx];
                    myRow.Cells[PS_ItemDescriptionDataGridView_SelectedLoan.Index].Value = layaway.TicketNumber;
                    myRow.Cells[PS_Description_ICNColumn.Index].Value = retailItem.Icn;
                    myRow.Cells[PS_Description_TicketDescriptionColumn.Index].Value = retailItem.TicketDescription;
                    myRow.Cells[colStatus.Index].Value = retailItem.ItemStatus.ToString();
                    myRow.Cells[colLocation.Index].Value = retailItem.Location;
                    myRow.Cells[colItemAmount.Index].Value = retailItem.RetailPrice.ToString("c");
                }
                if (layaway.CustomerNumber != GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber)
                {
                    CustomerLoans.GetCustomerName(
                        Utilities.GetStringValue(layaway.OrgShopNumber, "").PadLeft(5, '0'),
                        Utilities.GetIntegerValue(layaway.TicketNumber, 0), SupportProductType.PAWN.ToString(),
                        out sFirstName, out sMiddleName, out sLastName, out sErrorCode, out sErrorText);
                }
                else
                {
                    CustomerVO activeCustomer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                    sFirstName = activeCustomer.FirstName + " " + activeCustomer.MiddleInitial + " " +
                                 activeCustomer.LastName;
                }

                PS_PawnNameLabel.Visible = true;
                PS_ItemDescriptionDataGridView.Visible = true;
                PS_LoanStatsLayoutPanel.Visible = false;
                LW_DetailsLayoutPanel.Visible = true;

                LayawayPaymentHistoryBuilder paymentBuilder;

                try
                {
                    paymentBuilder = new LayawayPaymentHistoryBuilder(layaway);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error building the payment schedule");
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "UpdateActiveLayawayInformation errored:  " + exc.Message);
                    return;
                }
                decimal delinquentAmount = paymentBuilder.GetDelinquentAmount(ShopDateTime.Instance.FullShopDateTime);

                PS_ReceiptNoValue.Text = "";
                if (CollectionUtilities.isNotEmpty(layaway.Receipts))
                {
                    int inDx = layaway.Receipts.FindLastIndex(delegate(Receipt r)
                    {
                        return r.Date ==
                               layaway.Receipts.Min(m => m.Date);
                    });

                    PS_ReceiptNoValue.Text = layaway.Receipts[inDx].ReceiptNumber;
                }

                if (CollectionUtilities.isNotEmpty(layaway.Receipts))
                {
                    int idx = layaway.Receipts.FindLastIndex(r => r.Date == layaway.Receipts.Max(m => m.Date) && r.Event == "LAYPMT");
                    if (idx >= 0)
                        lastLayawayPayment = layaway.Receipts[idx].Date.ToShortDateString();
                }


                PS_PawnLoanLabel.Text = "Layaway " + layaway.TicketNumber;
                PS_PawnLoanLabel.Visible = true;
                LW_NumberValue.Text = layaway.TicketNumber.ToString();
                PS_PawnNameLabel.Text = (sFirstName + " " + sMiddleName + " " + sLastName).Replace("  ", " ");
                LW_FirstPaymentDueDateValue.Text = layaway.FirstPayment.ToString("d");
                LW_PaidToDateValue.Text = layaway.GetAmountPaid().ToString("c");
                LW_PaymentAmountDueValue.Text = paymentBuilder.GetTotalDueNextPayment(ShopDateTime.Instance.FullShopDateTime).ToString("c");
                LW_CreatedOnValue.Text = layaway.DateMade.ToString("d");
                LW_TotalAmountOfLayawayValue.Text = (layaway.Amount + layaway.SalesTaxAmount).ToString("c");
                LW_OutstandingValue.Text = paymentBuilder.GetBalanceOwed().ToString("c");
                LW_NumberOfPaymentsValue.Text = layaway.NumberOfPayments.ToString();
                LW_ByValue.Text = layaway.CreatedBy;
                LW_DownPaymentValue.Text = layaway.DownPayment.ToString("c");
                LW_DelinquentValue.Text = delinquentAmount.ToString("c");


                LW_ServiceFeeIncludingTaxValue.Text = layaway.GetLayawayFees().ToString("c");
            }
            else
            {
                PS_ItemDescriptionDataGridView.Rows.Clear();
                PH_ReceiptsDataGridView.Rows.Clear();
                PS_PawnNameLabel.Visible = true;
                PS_ItemDescriptionDataGridView.Visible = true;
                PS_LoanStatsLayoutPanel.Visible = false;
                LW_DetailsLayoutPanel.Visible = true;

                LW_NumberValue.Text = string.Empty;
                PS_PawnNameLabel.Text = string.Empty;
                LW_FirstPaymentDueDateValue.Text = string.Empty;
                LW_PaidToDateValue.Text = string.Empty;
                LW_PaymentAmountDueValue.Text = string.Empty;
                LW_CreatedOnValue.Text = string.Empty;
                LW_TotalAmountOfLayawayValue.Text = string.Empty;
                LW_OutstandingValue.Text = string.Empty;
                LW_NumberOfPaymentsValue.Text = string.Empty;
                LW_ByValue.Text = string.Empty;
                LW_DownPaymentValue.Text = string.Empty;
                LW_DelinquentValue.Text = string.Empty;
                LW_ServiceFeeIncludingTaxValue.Text = string.Empty;
            }
        }
        /*__________________________________________________________________________________________*/
        private void UpdateActivePawnInformation(int iTicketNumber)
        {
            PawnLoan pawnLoan = new PawnLoan();

            PS_PawnLoanLabel.Visible = false;
            PS_PawnNameLabel.Visible = false;
            PS_ItemDescriptionDataGridView.Visible = false;
            PS_LoanStatsLayoutPanel.Visible = false;
            PS_ItemDescriptionDataGridView.Columns[colItemAmount.Index].HeaderText = "Item Amount";
            bool gunFound = false;

            int iDx = -1;
            //if the pawn loan being viewed is already marked for some sort of service get
            //the details of the loan from service loans else get from pawnloans list
            //But if the service marked on the loan is Paydown or renew it should show the old loan
            if (GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Count > 0)
            {
                iDx = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.FindIndex(pl => pl.TicketNumber == iTicketNumber);
                if (iDx >= 0)
                {
                    if (GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[iDx].TempStatus == StateStatus.RN ||
                        GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[iDx].TempStatus == StateStatus.PD)
                        iDx = -1;
                    else
                        pawnLoan = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[iDx];
                }
            }
            //If the loan is not in the service loans list
            if (iDx < 0)
            {
                iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans.FindIndex(pl => pl.TicketNumber == iTicketNumber);
                if (iDx >= 0)
                    pawnLoan = GlobalDataAccessor.Instance.DesktopSession.PawnLoans[iDx];

                else
                {
                    iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.FindIndex(
                        pl => pl.TicketNumber == iTicketNumber);
                    if (iDx >= 0)
                    {
                        pawnLoan = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[iDx];
                    }
                }
            }

            if (_SelectedLoans.Count > 1)
            {
                PS_ItemDescriptionDataGridView.Rows.Clear();
                PS_ItemDescriptionDataGridView.Visible = true;
                PS_ItemDescriptionDataGridView.Columns[PS_ItemDescriptionDataGridView_SelectedLoan.Index].Visible = true;
                PS_ItemDescriptionDataGridView.Columns[PS_ItemDescriptionDataGridView_SelectedLoan.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
                PS_ItemDescriptionDataGridView.Columns[PS_Description_ICNColumn.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
                PS_ItemDescriptionDataGridView.Columns[PS_Description_TicketDescriptionColumn.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
                PS_ItemDescriptionDataGridView.Columns[colStatus.Index].Visible = true;
                PS_ItemDescriptionDataGridView.Columns[colLocation.Index].Visible = true;
                PS_ItemDescriptionDataGridView.Columns[colItemAmount.Index].Visible = false;

                foreach (PawnLoan pl in _SelectedLoans)
                {
                    bool printLoanNumber = true;
                    foreach (Item pItem in pl.Items)
                    {
                        int gvIdx = PS_ItemDescriptionDataGridView.Rows.Add();
                        DataGridViewRow myRow = PS_ItemDescriptionDataGridView.Rows[gvIdx];
                        //Show the loan number only once for a set of items belonging to the loan
                        myRow.Cells[PS_ItemDescriptionDataGridView_SelectedLoan.Index].Value = pl.TicketNumber;
                        if (printLoanNumber)
                        {
                            printLoanNumber = false;
                        }
                        //To do: Hide the cell contents if printloannumber is false
                        myRow.Cells[PS_Description_ICNColumn.Index].Value = pItem.Icn;
                        myRow.Cells[PS_Description_TicketDescriptionColumn.Index].Value = pItem.TicketDescription;
                        if (pItem.IsGun && !pl.IsExtended)
                            gunFound = true;
                    }
                }
                PS_LoanStatsLayoutPanel.Visible = false;
            }
            else
            {
                if (pawnLoan.TicketNumber != 0)
                {
                    var sFirstName = string.Empty;
                    var sMiddleName = string.Empty;
                    var sLastName = string.Empty;
                    var sErrorCode = string.Empty;
                    var sErrorText = string.Empty;

                    PS_ItemDescriptionDataGridView.Rows.Clear();
                    PS_ItemDescriptionDataGridView.Columns[PS_ItemDescriptionDataGridView_SelectedLoan.Index].Visible = false;
                    PS_ItemDescriptionDataGridView.Columns[PS_Description_ICNColumn.Index].SortMode = DataGridViewColumnSortMode.Automatic;
                    PS_ItemDescriptionDataGridView.Columns[PS_Description_TicketDescriptionColumn.Index].SortMode = DataGridViewColumnSortMode.Automatic;
                    PS_ItemDescriptionDataGridView.Columns[colStatus.Index].Visible = true;
                    PS_ItemDescriptionDataGridView.Columns[colLocation.Index].Visible = true;
                    PS_ItemDescriptionDataGridView.Columns[colItemAmount.Index].Visible = false;

                    for (int i = 0; i < pawnLoan.Items.Count; i++)
                    {
                        Item pawnItem = pawnLoan.Items[i];

                        int gvIdx = PS_ItemDescriptionDataGridView.Rows.Add();
                        DataGridViewRow myRow = PS_ItemDescriptionDataGridView.Rows[gvIdx];
                        myRow.Cells[PS_ItemDescriptionDataGridView_SelectedLoan.Index].Value = pawnLoan.TicketNumber;
                        myRow.Cells[PS_Description_ICNColumn.Index].Value = pawnItem.Icn;
                        myRow.Cells[colStatus.Index].Value = pawnItem.ItemStatus;
                        string itemLocation = string.Empty;
                        if (!string.IsNullOrEmpty(pawnItem.Location_Aisle))
                            itemLocation = "Aisle: " + pawnItem.Location_Aisle;
                        if (!string.IsNullOrEmpty(pawnItem.Location_Shelf))
                            itemLocation += " Shelf: " + pawnItem.Location_Shelf;
                        if (!string.IsNullOrEmpty(pawnItem.Location))
                            itemLocation += " Other: " + pawnItem.Location;
                        myRow.Cells[colLocation.Index].Value = itemLocation;
                        myRow.Cells[PS_Description_TicketDescriptionColumn.Index].Value = pawnItem.TicketDescription;
                        if (pawnItem.IsGun && !pawnLoan.IsExtended)
                            gunFound = true;
                    }
                    if (pawnLoan.CustomerNumber != GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber)
                    {
                        CustomerLoans.GetCustomerName(
                            Utilities.GetStringValue(pawnLoan.OrgShopNumber, "").PadLeft(5, '0'),
                            Utilities.GetIntegerValue(pawnLoan.TicketNumber, 0), SupportProductType.PAWN.ToString(),
                            out sFirstName, out sMiddleName, out sLastName, out sErrorCode, out sErrorText);
                    }
                    else
                    {
                        CustomerVO activeCustomer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                        sFirstName = activeCustomer.FirstName + " " + activeCustomer.MiddleInitial + " " +
                                     activeCustomer.LastName;
                    }

                    PS_PawnLoanLabel.Text = "Pawn Loan " + pawnLoan.TicketNumber;
                    PS_PawnNameLabel.Text = (sFirstName + " " + sMiddleName + " " + sLastName).Replace("  ", " ");

                    PS_OriginationDateValue.Text =
                    Utilities.GetDateTimeValue(pawnLoan.OriginationDate, DateTime.MinValue).ToShortDateString();
                    PS_DueDateValue.Text =
                    Utilities.GetDateTimeValue(pawnLoan.DueDate, DateTime.MinValue).ToShortDateString();
                    ShowStatusValue(pawnLoan);
                    PS_LastDayOfGraceValue.Text =
                    Utilities.GetDateTimeValue(pawnLoan.LastDayOfGrace, DateTime.MinValue).ToShortDateString();
                    PS_ReceiptNoValue.Text = "";

                    if (CollectionUtilities.isNotEmpty(pawnLoan.Receipts))
                    {
                        int inDx = pawnLoan.Receipts.FindLastIndex(delegate(Receipt r)
                        {
                            return r.Date ==
                                   pawnLoan.Receipts.Min(m => m.Date);
                        });

                        PS_ReceiptNoValue.Text = pawnLoan.Receipts[inDx].ReceiptNumber;
                    }
                    if (PS_ReceiptNoValue.Text.Length == 0)
                        PS_ReceiptNoLabel.Visible = false;
                    else
                        PS_ReceiptNoLabel.Visible = true;
                    PS_OriginationShopValue.Text = Utilities.GetStringValue(pawnLoan.OrgShopNumber, "").PadLeft(5,
                                                                                                                '0');
                    if (PS_OriginationShopValue.Text != GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber)
                        PS_OriginationShopValue.ForeColor = Color.Red;
                    else
                        PS_OriginationShopValue.ForeColor = Color.Black;
                    PS_LoanAmountValue.Text = string.Format("{0:C}", pawnLoan.Amount);

                    if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.CurrentSiteId))
                    {
                        lblCurrentPrincipalAmount.Text = string.Format("{0:C}", pawnLoan.CurrentPrincipalAmount);
                    }
                    else
                    {
                        lblCurrentPrincipalAmountTitle.Visible = false;
                        lblCurrentPrincipalAmount.Visible = false;
                    }

                    PS_PickupAmountValue.Links[0].LinkData = iTicketNumber;
                    //SR - Commented the following lines since we do
                    //not want to show the data when the loan is selected but
                    //add this logic in validateselectedloans when the service button is clicked
                    /*PS_PickupAllowedLinkLabel.Text =
                    PS_RenewalAmountValue.Text = string.Format("{0:C}", pawnLoan.RenewalAmount);
                    PS_RenewalAllowedValue.Text = Utilities.GetBooleanValue(pawnLoan.RenewalAllowed, false) ? "Yes" : "No";
                    if (pawnLoan.PickupAllowed)
                    {
                    PS_PickupAllowedLinkLabel.Text = "Yes";
                    PS_PickupAllowedLinkLabel.Links[0].LinkData = "";
                    PS_PickupAllowedLinkLabel.LinkBehavior = LinkBehavior.NeverUnderline;
                    }
                    else
                    {
                    PS_PickupAllowedLinkLabel.Text = "No";
                    PS_PickupAllowedLinkLabel.LinkBehavior = LinkBehavior.AlwaysUnderline;
                    PS_PickupAllowedLinkLabel.Links[0].LinkData = pawnLoan.PickupNotAllowedReason;
                    }
                    if (pawnLoan.IsExtensionAllowed)
                    {
                    PS_ExtensionAllowedLinkLabel.Text = "Yes";
                    PS_ExtensionAllowedLinkLabel.Links[0].LinkData = "";
                    PS_ExtensionAllowedLinkLabel.LinkBehavior = LinkBehavior.NeverUnderline;
                    }
                    else
                    {
                    PS_ExtensionAllowedLinkLabel.Text = "No";
                    PS_ExtensionAllowedLinkLabel.Links[0].LinkData = pawnLoan.ExtensionNotAllowedReason;
                    PS_ExtensionAllowedLinkLabel.LinkBehavior = LinkBehavior.AlwaysUnderline;
                    }*/

                    if (pawnLoan.ServiceMessage.Length > 0)
                    {
                        PS_ServiceMessageLabel.Text = pawnLoan.ServiceMessage;
                        PS_ServiceMessageLabel.Visible = true;
                    }
                    else
                    {
                        PS_ServiceMessageLabel.Visible = false;
                    }
                    PS_PawnLoanLabel.Visible = true;
                    PS_PawnNameLabel.Visible = true;
                    PS_ItemDescriptionDataGridView.Visible = true;
                    PS_LoanStatsLayoutPanel.Visible = true;

                    if (pawnLoan.Receipts != null)
                    {
                        PH_ReceiptsDataGridView.Rows.Clear();
                        // History and Receipt info 
                        foreach (Receipt myReceipt in pawnLoan.Receipts)
                        {
                            int gvIdx = PH_ReceiptsDataGridView.Rows.Add();
                            DataGridViewRow myRow = PH_ReceiptsDataGridView.Rows[gvIdx];
                            myRow.Cells["PH_Receipt_DateColumn"].Value = myReceipt.Date.ToShortDateString();
                            if ((myReceipt.Event == ReceiptEventTypes.Renew.ToString() || myReceipt.Event == ReceiptEventTypes.Paydown.ToString())
                                && myReceipt.Amount == 0)
                            {
                                myRow.Cells["PH_Receipt_AmountColumn"].Value = String.Format(  "{0:C}", pawnLoan.Amount);
                                myRow.Cells["PH_Receipt_EventColumn"].Value = "New Loan";
                            }
                            else
                            {
                                myRow.Cells["PH_Receipt_AmountColumn"].Value = String.Format( "{0:C}", myReceipt.Amount);
                                myRow.Cells["PH_Receipt_EventColumn"].Value = myReceipt.TypeDescription;
                            }

                            //myRow.Cells["PH_Receipt_EventColumn"].Value = myReceipt.Event;
                            //if (myReceipt.Event == ReceiptEventTypes.Renew.ToString() || myReceipt.Event == ReceiptEventTypes.Paydown.ToString())
                            //    myRow.Cells["PH_Receipt_AmountColumn"].Value = String.Format("{0:C}", pawnLoan.Amount);
                            //else
                            //myRow.Cells["PH_Receipt_AmountColumn"].Value = string.Format("{0:C}", myReceipt.Amount);
                            myRow.Cells["PH_Receipt_EntIDColumn"].Value = myReceipt.EntID;
                            myRow.Cells["PH_Receipt_ReceiptColumn"].Value = myReceipt.ReceiptNumber;
                        }
                    }
                    else
                    {
                        PH_ReceiptsDataGridView.Rows.Clear();
                    }
                }
            }
        }
        /*__________________________________________________________________________________________*/
        private void LoadAdditionalTicketsData()
        {
            if (GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.Count > 0)
            {
                //no ticket SMurphy 3/16/2010 don't add to Additional Tickets if it's already in the original or Additional lists
                //since the duplicate loans are already in the PawnLoan object (from 2 possible entry points) do cleanup of the object on the way in
                //SR QA Issue 589 4/1/2010
                //Fixed an issue wherein if the customer being looked at has no loans and if he is 
                //selected a non original pledgor to process a loan
                GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.Sort(delegate(PawnLoan pk1, PawnLoan pk2)
                {
                    return pk1.TicketNumber.CompareTo(pk2.TicketNumber);
                });
                List<PawnLoan> fixAuxLoans = new List<PawnLoan>();
                DataGridView gridViewTemp = PS_AddTicketsDataGridView;
                for (int i = 0; i < GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.Count; i++)
                {
                    if ((fixAuxLoans.Count > 0 && !(fixAuxLoans.Exists(p => p.TicketNumber == GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].TicketNumber)) ||
                         (_LoanKeys != null && _LoanKeys.Exists(pk1 => pk1.TicketNumber == GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].TicketNumber))))
                    {
                        fixAuxLoans.Add(GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i]);
                    }
                    else if (SearchGrid(GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].TicketNumber.ToString(), PS_TicketsDataGridView) ||
                             SearchGrid(GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].TicketNumber.ToString(), gridViewTemp))
                    {
                        MessageBox.Show("Loan Number " + GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].TicketNumber.ToString() + " has already been added.");
                    }
                }
                if (fixAuxLoans.Count > 0)
                    GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary = fixAuxLoans;

                PS_AddTicketsDataGridView.Rows.Clear();
                for (int i = 0; i < GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.Count; i++)
                {
                    //no ticket SMurphy 3/16/2010 don't add to Additional Tickets if it's already in the original or Additional lists
                    if (SearchGrid(GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].TicketNumber.ToString(), PS_TicketsDataGridView) ||
                        SearchGrid(GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].TicketNumber.ToString(), gridViewTemp))
                    {
                        MessageBox.Show("Loan Number " + GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].TicketNumber.ToString() + " has already been added.");
                    }

                    int gvIdx = PS_AddTicketsDataGridView.Rows.Add();

                    DateTime dtLastDayGrace = Utilities.GetDateTimeValue(GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].LastDayOfGrace, DateTime.MinValue).Date;
                    DataGridViewRow myRow = PS_AddTicketsDataGridView.Rows[gvIdx];

                    if (GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].IsExtended)
                        myRow.Cells["PS_AddTickets_Extend"].Value = "E";
                    myRow.Cells["PS_AddTickets_TicketNumberColumn"].Value =
                    Utilities.GetStringValue(
                        GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].OrgShopNumber, "").PadLeft(5, '0') +
                    Utilities.GetStringValue(GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].TicketNumber, "");
                    myRow.Cells["PS_AddTickets_LastDayColumn"].Value = dtLastDayGrace;
                    //TODO: This should be a rule!!!!
                    if (dtLastDayGrace <= ShopDateTime.Instance.ShopDate.AddDays(-5))
                        PS_AddTicketsDataGridView.Rows[gvIdx].DefaultCellStyle.BackColor = Color.Yellow;

                    if (_TicketNumber == Utilities.GetIntegerValue(GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].TicketNumber, 0))
                    {
                        PS_AddTicketsDataGridView.Rows[gvIdx].DefaultCellStyle.BackColor = Color.Blue;
                        //_ActiveLoanIndex = PS_TicketsDataGridView.Rows.Count + i;
                    }

                    //If Auxillary loan keys data already exists
                    //Check and see if they are the loan keys of the current customer 
                    //If not, need to call get loan keys on this new customer whose
                    //ticket has been added
                    if (_AuxillaryLoanKeys.Count > 0)
                    {
                        int tktNumber = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].TicketNumber;
                        int idx = _AuxillaryLoanKeys.FindIndex(pk1 => pk1.TicketNumber == tktNumber);
                        if (idx >= 0)
                            continue;
                    }
                    //Get the minimum store interest charge from business rule
                    decimal minFinanceCharge = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetStoreMinimumIntCharge();
                    string errorCode;
                    string errorDesc;
                    List<PawnLoan> pawnLoanKeys;
                    bool spCallRes =
                    CustomerLoans.GetLoanKeys(GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[i].CustomerNumber, minFinanceCharge,
                                              ShopDateTime.Instance.ShopDate, out pawnLoanKeys, out errorCode, out errorDesc);
                    if (spCallRes)
                    {
                        pawnLoanKeys = pawnLoanKeys.FindAll(plk => (plk.LoanStatus == ProductStatus.IP || (plk.LoanStatus == ProductStatus.LAY && plk.DocType == 4)));
                        GlobalDataAccessor.Instance.DesktopSession.PawnLoanKeysAuxillary.AddRange(_AuxillaryLoanKeys);
                        _AuxillaryLoanKeys.AddRange(pawnLoanKeys.AsEnumerable());
                    }

                    PS_TicketsDataGridView.Height = 260;
                    PS_AddTicketsDataGridView.Visible = true;
                    PS_AddTicketsLabel.Visible = true;
                }
            }
            else
            {
                PS_TicketsDataGridView.Height = 353;
                PS_AddTicketsDataGridView.Visible = false;
                PS_AddTicketsLabel.Visible = false;
            }
        }
        /*__________________________________________________________________________________________*/
        private void MapPDL_LoanStatsFromProperties(PDLoanDetails Record )
        {

            decimal MoneyValue;
            string LegalDeptMsg = "Refer to Legal Department";
                
            var PDLoan = Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan;
            //var otherDetails = Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan.GetPDLoanOtherDetails;

            this.lblCustomerSSNData.Text = Commons.FormatSSN(Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer.SocialSecurityNumber);
//            this.lblCustomerSSNData.Text = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer.SocialSecurityNumber;
            this.lblUWNameData.Text = Record.UWName;

            string requested_date = string.Empty;
            if (Record.LoanRequestDate != null)
            {
                if (Record.LoanRequestDate == DateTime.MaxValue || Record.LoanRequestDate == DateTime.MinValue)
                    requested_date = string.Empty;
                else
                {
                    requested_date = (Record.LoanRequestDate.FormatDateWithTimeZone()).ToString();
                }
            }
            this.lblLoanRequestDateData.Text = requested_date;//Record.LoanRequestDate == DateTime.MaxValue ? "" : (Record.LoanRequestDate).FormatDate();
            //Commons.FormatDollarStringAsDecimal(Record.LoanAmt.ToString(), out MoneyValue);

            this.lblLoanAmtData.Text = Record.LoanAmt.ToString("C");

            this.lblLoanPayOffAmtData.Text = Record.LoanPayOffAmt.ToString("C");
            this.lblActualLoanAmtData.Text = Record.ActualLoanAmt.ToString("C");

            this.TxbLoanNumberOrig.Text = Record.LoanNumberOrig;
            this.TxbLoanNumberPrev.Text = Record.LoanNumberPrev;
            this.TxbLoanStatus.Text = Record.Status;
            this.TxbLoanStatusReason.Text = Record.Status_Reason;
            this.TxbLoanRolloverNotes.Text = Record.LoanRolloverNotes;
            this.TxbLoanRollOverAmt.Text = Record.LoanRollOverAmt.ToString("C");
            this.TxbRevokeACH.Text = Record.RevokeACH.ToString();// == Record.RevokeACH ? "Yes" : "No";

            this.TxbXPPAvailable.Text = Record.XPPAvailable.ToString();// == Record.XPPAvailable ? "Yes" : "No";
            this.TxbActualFinanceChrgAmt.Text = Record.ActualFinanceChrgAmt.ToString("C");
            this.TxbAcutalServiceChrgAmt.Text = Record.AcutalServiceChrgAmt.ToString("C");
            this.TxbAccruedFinanceAmt.Text = Record.AccruedFinanceAmt.ToString("C");
            this.TxbLateFeeAmt.Text = Record.LateFeeAmt.ToString("C");
            this.TxbNSFFeeAmt.Text = Record.NSFFeeAmt.ToString("C");
            this.TxbACHWaitingToClear.Text = Record.ACHWaitingToClear; // == Record.ACHWaitingToClear ? "Yes" : "No";
            this.TxbEstRolloverAmt.Text = Record.EstRolloverAmt.ToString();

            this.XPPFeeAmountToDisplay.Text = checkNull(Record.XPP_Fee_Amount).ToString();

            if (!checkNull(Record.XPP_Start_Date).Equals(string.Empty))
            {
                this.XPPStartDateToDisplay.Text = DateTime.Parse(Record.XPP_Start_Date).FormatDate();
            }
            //this.XPPStartDateToDisplay.Text = checkNull(Record.XPP_Start_Date).ToString();

            if (!checkNull(Record.XPP_End_Date).Equals(string.Empty))
            {
                this.XPPEndDateToDisplay.Text = DateTime.Parse(Record.XPP_End_Date).FormatDate();
            }
            //int tempval;
            //int.TryParse(otherDetails.CourtCostAmt, out tempval);
            //if (tempval > 0) //otherDetails.CourtCostAmt > 0)
            //    this.PS_PawnNameLabel.Text = LegalDeptMsg;

            //this.XPPEndDateToDisplay.Text = checkNull(Record.XPP_End_Date).ToString();
            this.LblLoanNumber.Text = Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan.PDLLoanNumber;
        }

        /*__________________________________________________________________________________________*/
        private void MapPDL_xppLoanScheduleFromProperties(List<PDLoanXPPScheduleList> Records)
        {
            this.DGVxxpPaySchedule.Rows.Clear();
            for (int index = 0; index < Records.Count(); index++)
            {
                int gvIdx = DGVxxpPaySchedule.Rows.Add();
                DataGridViewRow LineItem = DGVxxpPaySchedule.Rows[gvIdx];
                //LineItem.Cells["DgvColxppLineItem"].Value = "StartDate";
                LineItem.Cells["DgvColxppPaymentSeqNumber"].Value = Records[index].xppPaymentSeqNumber.ToString();
                LineItem.Cells["DgvColxppPaymentNumber"].Value = Records[index].xppPaymentNumber;
                LineItem.Cells["DgvColxppPaymentDate"].Value = Records[index].xppDate == DateTime.MaxValue ? "" : (Records[index].xppDate).FormatDate();
                LineItem.Cells["DgvColxppPaymentAmt"].Value = Records[index].xppAmount.ToString();
            }
        }
        /*__________________________________________________________________________________________*/
        private void MapPDL_EventsFromProperties(PDLoanDetails Record)
        {
            // put these elements in a groupbox for more controll.
            this.TxbOriginationDate.Text = Record.OrginationDate == DateTime.MinValue ? "" : (Record.OrginationDate).FormatDate();
            this.TxbDueDate.Text = Record.DueDate == DateTime.MinValue ? "" : (Record.DueDate).FormatDate();
            this.TxbOrigDepDate.Text = Record.OrigDepDate == DateTime.MinValue ? "" : (Record.OrigDepDate).FormatDate();
            this.TxbExtendedDate.Text = Record.ExtendedDate == DateTime.MinValue ? "" : (Record.ExtendedDate).FormatDate();
            this.TxbLastUpdatedBy.Text = Record.LastUpdatedBy;
            this.TxbShopNo.Text = Record.ShopNo;
            this.TxbShopName.Text = Record.ShopName;
            this.TxbShopState.Text = Record.ShopState;
        }
        /*__________________________________________________________________________________________*/
        private void MapPDL_HistoryFromProperties(List<PDLoanHistoryList> eventsList)
        {
            this.DGVHistoryLoanEvents.Rows.Clear();
            for (int index = 0; index < eventsList.Count(); index++)
            {
                    int gvIdx = DGVHistoryLoanEvents.Rows.Add();
                    DataGridViewRow GridRow = DGVHistoryLoanEvents.Rows[gvIdx];
                    GridRow.Cells["DgvColHistDate"].Value = eventsList[index].Date == DateTime.MaxValue ? "" : (eventsList[index].Date).FormatDate(); ;
                    GridRow.Cells["DgvColHistEventType"].Value = eventsList[index].EventType;
                    GridRow.Cells["DgvColHistDetails"].Value = eventsList[index].Details;
                    GridRow.Cells["DgvColHistAmoutPaid"].Value = eventsList[index].Amount.ToString();
                    GridRow.Cells["DgvColHistSource"].Value = eventsList[index].Source;
                    GridRow.Cells["DgvColHistReceipt"].Value = eventsList[index].Receipt;
            }
        }
        /*__________________________________________________________________________________________*/
        private SupportProductType GetSelectedProductType()
        {
            if (PS_ShowComboBox.SelectedIndex == 0)
            {
                return SupportProductType.PAWN;
            }
            else if (PS_ShowComboBox.SelectedIndex == 1)
            {
                return SupportProductType.LAYAWAY;
            }
            else if (PS_ShowComboBox.SelectedIndex == 2)
            {
                return SupportProductType.PDL;
            }

            return SupportProductType.NONE;
        }
        /*__________________________________________________________________________________________*/
        private void LoadDocuments(int iTicketNumber, SupportProductType productType)
        {
            string errString;
            tlpDocuments.Visible = false;
            CouchDbUtils.PawnDocInfo docInfo = new CouchDbUtils.PawnDocInfo();
            docInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.STORE_TICKET);
            docInfo.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            docInfo.TicketNumber = iTicketNumber;
            _currentTicketNumber = iTicketNumber;
            if (CouchDbUtils.GetPawnDocument(GlobalDataAccessor.Instance.OracleDA, GlobalDataAccessor.Instance.CouchDBConnector,
                                             docInfo, false, out pawnDocs, out errString))
            {
                if (pawnDocs != null)
                {
                    _ActiveLoanDocuments = new List<Document>();
                    tlpDocuments.Controls.Clear();
                    int colCount = 0;

                    foreach (CouchDbUtils.PawnDocInfo document in pawnDocs)
                    {
                        //If it's a receipt, do not display, but save document info off
                        //so that it can be displayed when the hyperlink is clicked.
                        if (CheckCustomerNumber(document.CustomerNumber))
                        {
                            string rowEventType = string.Empty;
                            if (productType == SupportProductType.PAWN)
                            {
                                PawnLoan pLoan = null;
                                int idx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans.FindIndex(i => i.TicketNumber == iTicketNumber);
                                if (idx >= 0)
                                    pLoan = GlobalDataAccessor.Instance.DesktopSession.PawnLoans[idx];
                                if (pLoan != null)
                                {
                                    Receipt rcpt = pLoan.Receipts.Find(r => r.ReceiptNumber == document.ReceiptNumber.ToString());
                                    if (rcpt.Event != null)
                                        rowEventType = rcpt.Event.ToString();
                                }
                            }
                            else
                            {
                                LayawayVO layData = null;
                                int idx = GlobalDataAccessor.Instance.DesktopSession.Layaways.FindIndex(i => i.TicketNumber == iTicketNumber);
                                if (idx >= 0)
                                    layData = GlobalDataAccessor.Instance.DesktopSession.Layaways[idx];
                                if (layData != null)
                                {
                                    Receipt rcpt = layData.Receipts.Find(r => r.ReceiptNumber == document.ReceiptNumber.ToString());
                                    if (rcpt.Event != null)
                                        rowEventType = rcpt.Event.ToString();
                                }

                            }
                            /*if (document.ReceiptNumber > 0)
                            {
                                ProcessTenderProcedures.ExecuteGetReceiptDetails(
                                    0,
                                    document.StoreNumber,
                                    "PAWN", document.ReceiptNumber.ToString(),
                                    out receiptDataTable,
                                    out errorCode,
                                    out errorText);

                                if (receiptDataTable != null && receiptDataTable.Rows != null && receiptDataTable.Rows.Count > 0)
                                {
                                    receiptRows = receiptDataTable.Select("Receipt_number=" + document.ReceiptNumber.ToString());
                                }
                                if (receiptRows != null)
                                {
                                    rowEventType = receiptRows[0]["REF_EVENT"].ToString();
                                }
                            }*/
                            if (document.DocumentType ==
                                Document.DocTypeNames.RECEIPT)
                            {
                                //Track the current receipt info for the link click event to call...
                                this._currReceiptLookupInfo = new ReceiptLookupInfo
                                {
                                    DocumentName = "Receipt# " + PS_ReceiptNoValue.Text,
                                    StorageID = document.StorageId,
                                    DocumentType = document.DocumentType
                                };
                            }
                            else
                            {
                                PictureBox pic = new PictureBox();
                                pic.BackgroundImageLayout = ImageLayout.None;
                                Label fileLabel = new Label();
                                fileLabel.Font = new Font(PS_OriginationDateLabel.Font, FontStyle.Bold);
                                pic.Click += pic_Click;
                                pic.Cursor = Cursors.Hand;
                                pic.Tag = document.DocumentType.ToString();
                                pic.Name = document.StorageId;
                                var tagText = string.Empty;
                                if (document.DocumentType ==
                                    Document.DocTypeNames.PDF)
                                {
                                    pic.BackgroundImage = Common.Properties.Resources.pdf_icon;
                                    if (rowEventType == "New")
                                        tagText = "Loan# " + iTicketNumber;
                                    else if (rowEventType == "Extend")
                                        tagText = "Extension Memo";//# " + iTicketNumber;
                                    else if (rowEventType == "PURV")
                                        tagText = "Vendor Purchase";//# " + iTicketNumber;
                                    else if (rowEventType == "Renew")
                                        tagText = "Renewal# " + iTicketNumber;
                                    else if (rowEventType == "Paydown")
                                        tagText = "Paydown# " + iTicketNumber;
                                    else if (rowEventType == "Pickup")
                                        tagText = "Pickup# " + iTicketNumber;
                                    else if (rowEventType == "RTC")
                                        tagText = "Return to Claimaint# " + iTicketNumber;
                                    else if (rowEventType == "PolSeize")
                                        tagText = "Police Seize# " + iTicketNumber;
                                    else if (rowEventType == "VPR")
                                        tagText = "Void Purchase# " + iTicketNumber;
                                    else if (rowEventType == "RET")
                                        tagText = "Return of Purchase# " + iTicketNumber;
                                    else if (rowEventType == "PFI")
                                        tagText = "Pulled for Inventory# " + iTicketNumber;
                                    else if (rowEventType == "TO")
                                        tagText = "Transfer Out# " + iTicketNumber;
                                    else if (rowEventType == "VEX")
                                        tagText = "Void Extension# " + iTicketNumber;
                                    else if (rowEventType == "VNL")
                                        tagText = "Void New Loan# " + iTicketNumber;
                                    else if (rowEventType == "VPD")
                                        tagText = "Void Paydown# " + iTicketNumber;
                                    else if (rowEventType == "VRN")
                                        tagText = "Void Renewal# " + iTicketNumber;
                                    else if (rowEventType == "PUR")
                                        tagText = "Purchase# " + iTicketNumber;
                                    else if (rowEventType == "VRET")
                                        tagText = "Void Purchase Return# " + iTicketNumber;
                                    else if (rowEventType == ReceiptEventTypes.LAY.ToString())
                                        tagText = "Layaway# " + iTicketNumber;
                                    else
                                        tagText = "Loan# " + iTicketNumber;
                                }
                                else if (document.DocumentType == Document.DocTypeNames.RECEIPT)
                                {
                                    pic.BackgroundImage = Common.Properties.Resources.receipt_icon;
                                    tagText = "Receipt# " + PS_ReceiptNoValue.Text;
                                }
                                pic.Tag += string.Format(" {0}", tagText);
                                fileLabel.Text = tagText;
                                pic.Width = pic.BackgroundImage.Width;
                                tlpDocuments.Controls.Add(pic, colCount, 0);
                                tlpDocuments.Controls.Add(fileLabel, colCount, 1);
                                colCount++;
                            }
                        }
                    }
                    if (tlpDocuments.Controls.Count > 0)
                        tlpDocuments.Visible = true;
                }
            }
            else
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Pawn Documents could not be loaded " + errString);
            }
        }
        #endregion
        #region BOOL

        /*__________________________________________________________________________________________*/
        public bool SearchGrid(string loan)
        {
            //no ticket SMurphy 3/16/2010 don't add to Additional Tickets if it's already in the original or Additional lists
            //this looks thru gridviews so a message box can be displayed when attempting to add duplicates
            bool found = false;

            foreach (DataGridViewRow row in PS_TicketsDataGridView.Rows)
            {
                if (row.Cells[2].Value != null)
                    if (row.Cells[2].Value.ToString().Substring(5) == loan)
                    {
                        found = true;
                        break;
                    }
            }

            foreach (DataGridViewRow row in PS_AddTicketsDataGridView.Rows)
            {
                if (row.Cells[2].Value != null)
                    if (row.Cells[2].Value.ToString().Substring(5) == loan)
                    {
                        found = true;
                        break;
                    }
            }

            return found;
        }
        /*__________________________________________________________________________________________*/
        protected override Boolean ProcessDialogKey(Keys keyData)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.LockProductsTab && keyData == Keys.Escape)
            {
                //If there are any loans set for service undo them
                if (GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Count > 0)
                    UndoPawnTransactions(GlobalDataAccessor.Instance.DesktopSession.ServiceLoans);
                this.NavControlBox.Action = NavBox.NavAction.BACK;
                //return true to indicate that the key has been handled
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }
        /*__________________________________________________________________________________________*/
        private bool AllowLostTicket(PawnLoan pawnLoan)
        {
            int loanindex = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.FindIndex(pl => pl.TicketNumber == pawnLoan.TicketNumber && (pl.TempStatus == StateStatus.P || pl.TempStatus == StateStatus.RN || pl.TempStatus == StateStatus.PD));
            if (loanindex >= 0)
                return true;
            return false;
        }
        /*__________________________________________________________________________________________*/
        private bool CheckCustomerNumber(string customerNumber)
        {
            bool found = false;
            if (_LoanKeys != null)
            {
                for (int i = 0; i < _LoanKeys.Count(); i++)
                {
                    if (_LoanKeys[i].CustomerNumber == customerNumber)
                    {
                        found = true;
                        break;
                    }
                }
            }
            return found;
        }
        /*__________________________________________________________________________________________*/
        private bool ContinueAfterBackgroundChecked()
        {
            CustomerVO currentCustomer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;

            if (GlobalDataAccessor.Instance.DesktopSession.BackgroundCheckCompleted || !_gunItem)
            {
                return true;
            }

            /* DateTime currentDate = ShopDateTime.Instance.ShopDate;
             string strStoreState = GlobalDataAccessor.Instance.CurrentSiteId.State;
             if (currentCustomer.HasValidConcealedWeaponsPermitInState(strStoreState, currentDate))
             {
                 if (CustomerProcedures.IsBackgroundCheckRequired())
                 {
                     FirearmsBackgroundCheck backgroundcheckFrm = new FirearmsBackgroundCheck();
                     backgroundcheckFrm.ShowDialog(this);
                 }
                 else
                 {
                     Support.Logic.CashlinxPawnSupportSession.Instance.BackgroundCheckCompleted = true; //If the background check is not needed
                 }
             }
             //else if they do not have CWP or not a CWP in the store state or expired 
             //then show the background check form
             else
             {
                 FirearmsBackgroundCheck backgroundcheckFrm = new FirearmsBackgroundCheck();
                 backgroundcheckFrm.ShowDialog(this);
             }*/
            //commencted - madhu
            /*FirearmsBackgroundCheck backgroundcheckFrm = new FirearmsBackgroundCheck();
            backgroundcheckFrm.ShowDialog(this);
             */
            return GlobalDataAccessor.Instance.DesktopSession.BackgroundCheckCompleted;

        }
        /*__________________________________________________________________________________________*/
        private bool IsLayawayPawnKey(PawnLoan pawnKey)
        {
            return pawnKey.LoanStatus == ProductStatus.ACT && pawnKey.DocType == 4;
        }
        /*__________________________________________________________________________________________*/
        private bool IsAtLeastOneSelectedLayawayNotServiced()
        {
            return _SelectedLayaways.Any(selectedLayaway => !IsLayawayBeingServicedOrAlreadyBeenServiced(selectedLayaway));
        }
        /*__________________________________________________________________________________________*/
        private bool IsExactlyOneSelectedLayawayNotServiced()
        {
            if (_SelectedLayaways.Count != 1)
            {
                return false;
            }

            return !IsLayawayBeingServicedOrAlreadyBeenServiced(_SelectedLayaways[0]);
        }
        /*__________________________________________________________________________________________*/
        private bool IsLayawayBeingServicedOrAlreadyBeenServiced(LayawayVO selectedLayaway)
        {
            if (selectedLayaway.LoanStatus == ProductStatus.TERM || selectedLayaway.LoanStatus == ProductStatus.FORF)
            {
                return true;
            }

            return GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.Any(
                serviceLayaway => serviceLayaway.TicketNumber == selectedLayaway.TicketNumber);
        }
        //SR 04/22/2011 BZ 609
        //Check to see that all the layaway that are selected for service belong to the store where the service is being done
        /*__________________________________________________________________________________________*/
        private bool selectedLayawayInStore()
        {
            return !_SelectedLayaways.Any(sLayaway => sLayaway.StoreNumber != GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
        }

        //SR 05/09/2011 BZ 663
        //Check to see that all the loans that are selected for service belong to the store where the service is being done
        /*__________________________________________________________________________________________*/
        private bool selectedLoanInStore()
        {
            return !_SelectedLoans.Any(sLoan => sLoan.OrgShopNumber != GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
        }
        /*__________________________________________________________________________________________*/
        private bool validateSelectedLoans(ServiceTypes typeOfService)
        {
            //messages for the different checks
            var pfieLoanMessage = new StringBuilder();
            var ineligibleLoanMessage = new StringBuilder();
            var markedforServiceMessage = new StringBuilder();
            var lockedLoanMessage = new StringBuilder();
            var firearmCheckMessage = new StringBuilder();
            //The list of loans to validate
            var selectedLoans = _SelectedLoans;
            //List of ineligible loans and pfieloans
            var ineligibleLoans = new List<PawnLoan>();
            var pfieLoans = new List<PawnLoan>();
            //Go through the list of loans selected
            //Step 1- Check if any of the loans are already set for service. Remove from selection.
            //Step 2 - Check if any of the loans are in PFIE status. Allow user to refresh to see if 
            //status has changed. Continue refresh until the user wishes to continue with the others
            //or the status changed from PFIE
            //Step 3 - Evaluate the business rules on them to see if they
            //are eligible. If not remove from selection
            //Step 4 - check if any of the loans are locked. Remove from selection
            //Step 5 - Perform age check if any of the loan item is firearm and service is pickup
            //If check fails remove loan from selection
            foreach (PawnLoan ploan in selectedLoans)
            {
                //step 1 - check to see if the loan is already in the service loans list
                PawnLoan loan = ploan;
                int idx = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.FindIndex(pl => pl.TicketNumber == loan.TicketNumber);
                if (idx >= 0)
                {
                    markedforServiceMessage.Append(loan.TicketNumber.ToString() + " is already set for service" + System.Environment.NewLine);
                    ineligibleLoans.Add(loan);
                    continue;
                }
                //Step 2 - check if the loan has temp status of PFIE
                if (loan.TempStatus == StateStatus.PFIE)
                {
                    pfieLoans.Add(loan);
                    pfieLoanMessage.Append(loan.TicketNumber.ToString() + " is being processed by PFI." + System.Environment.NewLine);
                }
                //Step 3 - Check if business rules passed for the loan
                var siteID = new SiteId()
                {
                    Alias = GlobalDataAccessor.Instance.CurrentSiteId.Alias,
                    Company = GlobalDataAccessor.Instance.CurrentSiteId.Company,
                    Date = ShopDateTime.Instance.ShopDate,
                    LoanAmount = loan.Amount,
                    State = loan.OrgShopState,
                    StoreNumber = loan.OrgShopNumber,
                    TerminalId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId
                };

                ServiceLoanProcedures.SetBusinessRules(ref loan, siteID, strStoreNumber,
                                                       typeOfService);

                if (!loan.ServiceAllowed)
                {
                    ineligibleLoans.Add(loan);
                    ineligibleLoanMessage.Append(loan.TicketNumber.ToString() + " " + loan.ServiceMessage + System.Environment.NewLine);
                    continue;
                }
                //In some cases the service might be allowed but there may be some service messages
                //to show
                if (loan.ServiceAllowed && loan.ServiceMessage.Length > 0)
                {
                    ineligibleLoanMessage.Append(loan.TicketNumber.ToString() + " " + loan.ServiceMessage +
                                                 System.Environment.NewLine);
                }
                //Step 4 Check if the loan is locked by some other process
                var lockedProcess = string.Empty;
                if (loan.TempStatus != StateStatus.PFIE && Commons.IsLockedStatus(loan.TempStatus.ToString(), ref lockedProcess))
                {
                    ineligibleLoans.Add(loan);
                    lockedLoanMessage.Append(loan.TicketNumber + " Record locked for processing by " + lockedProcess + " process." + System.Environment.NewLine);
                }

                //Step 5 Do Firearm check if type of service is pickup, rollover, renew, and/or paydown
                if (typeOfService == ServiceTypes.PICKUP ||
                    typeOfService == ServiceTypes.ROLLOVER ||
                    typeOfService == ServiceTypes.RENEW ||
                    typeOfService == ServiceTypes.PAYDOWN)
                {
                    bool ageCheckPassed = true;

                    for (int j = 0; j < ploan.Items.Count; j++)
                    {
                        Item _pawnItem = ploan.Items[j];
                        if (_pawnItem.IsGun)
                        {
                            _gunItem = true;
                            overrideTransactionNumbers.Add(ploan.TicketNumber);
                            CustomerVO customerToCheck = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                            ageCheckPassed = CustomerProcedures.isValidAgeForGuns(GlobalDataAccessor.Instance.DesktopSession, _pawnItem, customerToCheck);
                            if (!ageCheckPassed)
                            {
                                ineligibleLoans.Add(loan);
                                firearmCheckMessage.Append(ploan.TicketNumber.ToString() + " " + Commons.GetMessageString("CustomerFirearmMessage") + System.Environment.NewLine);
                                break;
                            }
                        }
                    }
                }
            }

            ValidationMessage infoFrm = null;
            if (ineligibleLoans.Count > 0)
            {
                selectedLoans = selectedLoans.Except(ineligibleLoans).ToList();
                _SelectedLoans = selectedLoans;
                var MessageToShow = markedforServiceMessage.ToString() + ineligibleLoanMessage.ToString()
                                       + firearmCheckMessage.ToString() + lockedLoanMessage.ToString();

                infoFrm = new ValidationMessage
                {
                    IneligibleLoanMessage = MessageToShow,
                    SelectedLoansCount = this._SelectedLoans.Count
                };
                if (pfieLoans.Count > 0)
                {
                    infoFrm.PFIELoans = pfieLoans;
                    infoFrm.PFIELoanMessage = pfieLoanMessage.ToString();
                }
                infoFrm.ShowDialog();
            }
            else if (ineligibleLoans.Count == 0 && ineligibleLoanMessage.Length > 0)
            {
                var infoForm = new InfoDialog
                {
                    MessageToShow = ineligibleLoanMessage.ToString()
                };
                infoForm.ShowDialog();
            }
            else if (pfieLoans.Count > 0)
            {
                //Show the validation message form for just the pfie loans
                infoFrm = new ValidationMessage
                {
                    PFIELoans = pfieLoans,
                    PFIELoanMessage = pfieLoanMessage.ToString(),
                    SelectedLoansCount = this._SelectedLoans.Count
                };
                infoFrm.ShowDialog();
            }
            if (infoFrm != null)
            {
                if (infoFrm.ContinueService)
                {
                    //if continue service is a yes but there are still some PFIELoans remove them
                    //from the selected list
                    if (infoFrm.PFIELoans != null && infoFrm.PFIELoans.Count > 0)
                    {
                        selectedLoans = selectedLoans.Except(infoFrm.PFIELoans).ToList();
                        _SelectedLoans = selectedLoans;
                    }
                    return true;
                }
                //If continue service is a no, return false
                return false;
            }

            return true;
        }
        #endregion
        #region ACTIONS
        private string checkNull(string value)
        {
            if (string.IsNullOrEmpty(value) || "null".Equals(value))
                value = string.Empty;

            return value;
        }
        /*__________________________________________________________________________________________*/
        private void CelMouseUpActions(int rowIndex, int columnIndex)
        {
            int i = rowIndex;
            showRefreshIcon = false;
            DataGridViewRow myRow = PS_TicketsDataGridView.Rows[i];
            string sCellTicket = Utilities.GetStringValue(myRow.Cells["PS_Tickets_TicketNumberColumn"].Value, "");

            if (GetSelectedProductType() == SupportProductType.PDL)
            {
                PDLoan pdLoan = new PDLoan();
                int iDx = Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys.FindIndex(delegate(PDLoan p)
                                                                                                     {
                                                                                                         return
                                                                                                             p.
                                                                                                                 PDLLoanNumber
                                                                                                                 .Equals
                                                                                                                 (sCellTicket);
                                                                                                     });

                //int iDx = CashlinxPawnSupportSession.Instance.PDLoanKeys.FindIndex(item => item.PDLLoanNumber == sCellTicket);

                if (iDx >= 0)
                    pdLoan = Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys[iDx];

                if (checkNull(pdLoan.PDLLoanNumber).Equals(string.Empty))
                {
                    Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan = new PDLoan();
                    MapPDL_LoanStatsFromProperties(new PDLoanDetails());
                    MapPDL_EventsFromProperties(new PDLoanDetails());
                    MapPDL_xppLoanScheduleFromProperties(new List<PDLoanXPPScheduleList>());
                    MapPDL_HistoryFromProperties(new List<PDLoanHistoryList>());
                    this.LnkOtherDetails.Enabled = false;
                    this.btnExtendDeposit.Enabled = false;
                    this.ChkBGetAllHistory.Checked = false;
                    this.ChkBGetAllHistory.Enabled = false;
                    this.CmbHistoryLoanEvents.SelectedIndex = 0;
                    this.CmbHistoryLoanEvents.Enabled = false;
                    //return;
                }
                else
                {
                    string errorCode;
                    string errorDesc;
                    // logic to only get data if not already recieved.
                    if (Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys[iDx].SqlDataRetrieved)
                    {
                        pdLoan = Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys[iDx];
                        Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan = pdLoan;
                    }
                    else
                    {
                        //bool returnVal = 
                        if (Support.Controllers.Database.Procedures.CustomerLoans.GetPDLoanDetails(
                            pdLoan, out errorCode, out errorDesc))
                        {
                            Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys[iDx].SqlDataRetrieved = true;
                            Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan = pdLoan;
                        }
                    }

                    //if(returnVal)
                    //{
                    //Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan = pdLoan;
                    //var loanDetails = pdLoan.GetPDLoanDetails;
                    MapPDL_LoanStatsFromProperties(pdLoan.GetPDLoanDetails);
                    MapPDL_EventsFromProperties(pdLoan.GetPDLoanDetails);
                    MapPDL_xppLoanScheduleFromProperties(pdLoan.GetPDLoanXPPScheduleList);
                    MapPDL_HistoryFromProperties(pdLoan.GetPDLoanHistorySummaryList);
                    this.LnkOtherDetails.Enabled = true;
                    this.btnExtendDeposit.Enabled = true;
                    this.ChkBGetAllHistory.Checked = false;
                    this.ChkBGetAllHistory.Enabled = true;
                    this.CmbHistoryLoanEvents.SelectedIndex = 0;
                    this.CmbHistoryLoanEvents.Enabled = true;
                    //}
                }
            }
            //TODO revisit this - Madhu
            /*
            string sStoreNumber = sCellTicket.Substring(0, 5);
            int iTicketNumber = Convert.ToInt32(sCellTicket.Substring(5));
            _currentTicketNumber = Convert.ToInt32(sCellTicket.Substring(5));
            //_OrigTicketNumber = Utilities.GetIntegerValue(_LoanKeys[i].OrigTicketNumber, 0);
            PS_TicketsDataGridView.EndEdit();

            if (columnIndex == 4)
            {
                RefreshTempStatus(iTicketNumber, sStoreNumber);
                //TODO revisit this - Madhu
                //myRow.Cells["PS_Tickets_Refresh"].Value = Properties.Resources.blank;
            }
            else
            {

                PawnLoan selectedRow = _LoanKeys[i];
                if (IsLayawayPawnKey(selectedRow)) // Layaway
                {
                    ApplyLayawayBusinessRules(sStoreNumber, iTicketNumber, true);
                    PS_TicketsDataGridView.Rows[rowIndex].DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255); ;
                    UpdateActiveLayawayInformation(iTicketNumber);
                    PS_TicketsDataGridView.Rows[rowIndex].Cells[3].Value = lastLayawayPayment;
                    LoadDocuments(iTicketNumber, SupportProductType.LAYAWAY);
                }
                else
                {
                    ApplyBusinessRules(selectedRow, sStoreNumber, iTicketNumber, true, true);
                    if (loanRemoved)
                        PS_TicketsDataGridView.Rows[rowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
                    else
                        PS_TicketsDataGridView.Rows[rowIndex].DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255); ;
                    if (showRefreshIcon)
                        myRow.Cells["PS_Tickets_Refresh"].Value =
                        global::Common.Properties.Resources.refresh_icon;

                    UpdateActivePawnInformation(iTicketNumber);
                    //SR 6/15/2010 Load all the documents associated to the selected ticket
                    LoadDocuments(iTicketNumber, SupportProductType.PAWN);
                }
                 
            }*/
        }
        /*__________________________________________________________________________________________*/
        private int GetFirstRowTicketNumber()
        {
            int ticketNumber = 0;
            DataGridViewRow myRow = PS_TicketsDataGridView.Rows[0];
            string sCellTicket = Utilities.GetStringValue(myRow.Cells["PS_Tickets_TicketNumberColumn"].Value, "");
            ticketNumber = Convert.ToInt32(sCellTicket.Substring(5));
            return ticketNumber;
        }
        /*__________________________________________________________________________________________*/
        private static void calculateAmountsForLoan(ref PawnLoan pawnLoan)
        {
            //decimal feeAmount = 0.0M;
            //foreach (Fee fee in pawnLoan.Fees)
            //{
            //    if (!fee.Waived && fee.FeeState != FeeStates.VOID)
            //        feeAmount += fee.Value;
            //}

            //pawnLoan.PickupAmount = pawnLoan.Amount + feeAmount;

            var pickupCalculator = new PfiPickupCalculator(pawnLoan, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId, ShopDateTime.Instance.FullShopDateTime);
            pickupCalculator.Calculate();
            pawnLoan.PickupAmount = pickupCalculator.PickupAmount;
        }
        /*__________________________________________________________________________________________*/
        private void ApplyBusinessRules(PawnLoan pawnLoanKey, string sStoreNumber, int iTicketNumber, bool bSelected, bool bFromPrimaryPawnLoanTable)
        {
            /*    PawnLoan pawnLoan = null;
                PawnAppVO pawnAppVO;
                CustomerVO customerVO;
                var sErrorCode = string.Empty;
                var sErrorText = string.Empty;
                bool serviceLoan = false;
                PawnLoan rolloverLoan = null;

                int iDx = -1;
                //if the pawn loan being viewed is already marked for some sort of service get
                //the details of the loan from service loans else get from pawnloans list
                if (GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Count > 0)
                {
                    iDx = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.FindIndex(pl => pl.TicketNumber == iTicketNumber);
                    if (iDx >= 0)
                    {
                        if (GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[iDx].TempStatus == StateStatus.RN ||
                            GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[iDx].TempStatus == StateStatus.PD)
                        {
                            rolloverLoan = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[iDx];
                            iDx = -1;
                        }
                        else
                        {
                            pawnLoan = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[iDx];
                        }
                        serviceLoan = true;
                    }
                }
                //If the loan is not in the service loans list
                if (iDx < 0)
                {
                    if (bFromPrimaryPawnLoanTable)
                    {
                        iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans.FindIndex(pl => pl.TicketNumber == iTicketNumber);
                        if (iDx >= 0)
                            pawnLoan = GlobalDataAccessor.Instance.DesktopSession.PawnLoans[iDx];
                    }
                    else
                    {
                        iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.FindIndex(
                            pl => pl.TicketNumber == iTicketNumber);
                        if (iDx >= 0)
                        {
                            pawnLoan = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[iDx];
                        }
                    }

                    if (iDx < 0)
                    {
                        bool retVal = CustomerLoans.GetPawnLoan(GlobalDataAccessor.Instance.DesktopSession, Convert.ToInt32(sStoreNumber), iTicketNumber, "0",
                                                                StateStatus.BLNK,
                                                                false, out pawnLoan, out pawnAppVO, out customerVO,
                                                                out sErrorCode, out sErrorText);
                        if (!retVal)
                            BasicExceptionHandler.Instance.AddException(
                                "Error getting loan information for the selected loan",
                                new ApplicationException("GetPawnLoan Failed for " + iTicketNumber));
                    }
                }
                if (!serviceLoan)
                {
                    if (pawnLoan != null && pawnLoan.OriginalFees.Count == 0)
                    {
                        UnderwritePawnLoanVO underwritePawnLoanVO;
                        pawnLoan = ServiceLoanProcedures.GetLoanFees(currentStoreSiteId, ServiceTypes.PICKUP,
                                                                     pawnLoanKey.PickupLateFinAmount, pawnLoanKey.PickupLateServAmount,
                                                                     pawnLoanKey.OtherTranLateFinAmount, pawnLoanKey.OtherTranLateServAmount,
                                                                     pawnLoan, out underwritePawnLoanVO);
                        pawnLoan.OriginalFees = Utilities.CloneObject(pawnLoan.Fees);

                        if (bFromPrimaryPawnLoanTable)
                        {
                            if (iDx >= 0)
                            {
                                GlobalDataAccessor.Instance.DesktopSession.PawnLoans.RemoveAt(iDx);
                                GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Insert(iDx, pawnLoan);
                            }
                            else
                            {
                                GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Add(pawnLoan);
                            }
                        }
                        else
                        {
                            if (iDx >= 0)
                            {
                                GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.RemoveAt(iDx);
                                GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.Insert(iDx, pawnLoan);
                            }
                            else
                            {
                                GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.Add(pawnLoan);
                            }
                        }
                    }
                }

                if (pawnLoan != null)
                {
                    //Set PFI message
                    switch (pawnLoan.TempStatus)
                    {
                        case StateStatus.PFI:
                        case StateStatus.PFIW:
                        case StateStatus.PFIS:
                            pawnLoan.ServiceMessage = Commons.GetMessageString("TempStatusPFI") + System.Environment.NewLine;
                            break;
                        case StateStatus.PFIE:
                            showRefreshIcon = true;
                            break;
                    }

                    //Add all the fees to the pickup amount 
                    //Calculate the pickup amount and renewal amount
                    if (!serviceLoan)
                        calculateAmountsForLoan(ref pawnLoan);

                    if (!serviceLoan || pawnLoan.RenewalAmount == 0)
                        pawnLoan.RenewalAmount = pawnLoan.RenewalAllowed ? pawnLoan.PickupAmount - pawnLoan.Amount : 0.0M;

                    int loanindex = _SelectedLoans.FindIndex(
                        pl => pl.TicketNumber == pawnLoan.TicketNumber);

                    if (bSelected)
                    {
                        if (loanindex < 0)
                        //If the loan not found but the ctrl key was not pressed do
                        //not add it
                        {
                            PawnLoan ploan = Utilities.CloneObject(pawnLoan);
                            if (string.Equals(
                                Properties.Resources.MultipleLoanSelection,
                                Boolean.TrueString, StringComparison.OrdinalIgnoreCase))
                            {
                                if (_SelectedLoans.Count > 0 && ctrlKeyPressed)
                                {
                                    _SelectedLoans.Add(ploan);
                                }
                                if (_SelectedLoans.Count > 0 && !ctrlKeyPressed)
                                {
                                    //If the ctrl key was not pressed remove all the 
                                    //selected loans and place the newly selected loan
                                    //as the only selected loan
                                    _SelectedLoans.RemoveRange(0, _SelectedLoans.Count);
                                    _SelectedLoans.Insert(0, ploan);
                                }
                                if (_SelectedLoans.Count == 0)
                                    _SelectedLoans.Add(ploan);
                            }
                            else
                            {
                                _SelectedLoans.RemoveRange(0, _SelectedLoans.Count);
                                _SelectedLoans.Insert(0, ploan);
                            }
                            loanRemoved = false;
                        }
                        else
                        {
                            _SelectedLoans.RemoveAt(loanindex);
                            loanRemoved = true;
                        }
                    }

                    UpdateButtonsStates(false);
                    //If pickup service was performed on a loan and lost ticket is not already
                    //set on the loan the button is enabled. If the customer is not the pledgor the
                    //button is disabled
                    //SR 6/2/2010 If the loan is marked for rollover the lost ticket button rule should be calculated against the service loan data.
                    if (rolloverLoan == null)
                        PS_LostPawnTicketButton.Enabled = AllowLostTicket(pawnLoan) && (pawnLoan.LostTicketInfo == null || !pawnLoan.LostTicketInfo.TicketLost) && (GlobalDataAccessor.Instance.DesktopSession.CustomerNotPledgor == false);
                    else
                        PS_LostPawnTicketButton.Enabled = AllowLostTicket(rolloverLoan) && (rolloverLoan.LostTicketInfo == null || !rolloverLoan.LostTicketInfo.TicketLost) && (GlobalDataAccessor.Instance.DesktopSession.CustomerNotPledgor == false);
                } */
        }
        /*__________________________________________________________________________________________*/
        private void ApplyLayawayBusinessRules(string sStoreNumber, int iTicketNumber, bool bSelected)
        {
            LayawayVO layaway = null;
            CustomerVO customerVO;
            var sErrorCode = string.Empty;
            var sErrorText = string.Empty;
            bool serviceLayaway = false;

            int iDx = -1;
            List<LayawayVO> serviceLayaways = GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways;

            //if the layaway being viewed is already marked for some sort of service get
            //the details of the loan from service layaways else get from pawnloans list
            if (serviceLayaways.Count > 0)
            {
                iDx = serviceLayaways.FindIndex(pl => pl.TicketNumber == iTicketNumber);
                if (iDx >= 0)
                {
                    layaway = serviceLayaways[iDx];
                    serviceLayaway = true;
                }
            }
            //If the loan is not in the service layaways list
            if (iDx < 0)
            {
                iDx = GlobalDataAccessor.Instance.DesktopSession.Layaways.FindIndex(pl => pl.TicketNumber == iTicketNumber);
                if (iDx >= 0)
                    layaway = GlobalDataAccessor.Instance.DesktopSession.Layaways[iDx];

                if (iDx < 0)
                {
                    bool retVal = RetailProcedures.GetLayawayData(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.OracleDA, Convert.ToInt32(sStoreNumber),
                                                                  iTicketNumber, "0", StateStatus.BLNK, "LAY", true, out layaway, out customerVO, out sErrorCode, out sErrorText);

                    if (!retVal)
                        BasicExceptionHandler.Instance.AddException(
                            "Error getting loan information for the selected loan",
                            new ApplicationException("GetPawnLoan Failed for " + iTicketNumber));
                }
            }

            if (!serviceLayaway)
            {
                if (layaway != null)
                {
                    if (iDx >= 0)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.Layaways.RemoveAt(iDx);
                        GlobalDataAccessor.Instance.DesktopSession.Layaways.Insert(iDx, layaway);
                    }
                    else
                    {
                        GlobalDataAccessor.Instance.DesktopSession.Layaways.Add(layaway);
                    }
                }
            }

            if (layaway != null)
            {
                int loanindex = _SelectedLayaways.FindIndex(
                    pl => pl.TicketNumber == layaway.TicketNumber);

                if (loanindex >= 0)
                {
                    _SelectedLayaways.RemoveAt(loanindex);
                    loanRemoved = true;
                }

                if (bSelected)
                {
                    //If the loan not found but the ctrl key was not pressed do
                    //not add it
                    {
                        LayawayVO lway = Utilities.CloneObject(layaway);
                        if (string.Equals(
                            Properties.Resources.MultipleLoanSelection,
                            Boolean.TrueString, StringComparison.OrdinalIgnoreCase))
                        {
                            if (_SelectedLayaways.Count > 0 && ctrlKeyPressed)
                            {
                                _SelectedLayaways.Add(lway);
                            }
                            if (_SelectedLayaways.Count > 0 && !ctrlKeyPressed)
                            {
                                //If the ctrl key was not pressed remove all the 
                                //selected loans and place the newly selected loan
                                //as the only selected loan
                                _SelectedLayaways.RemoveRange(0, _SelectedLayaways.Count);
                                _SelectedLayaways.Insert(0, lway);
                            }
                            if (_SelectedLayaways.Count == 0)
                                _SelectedLayaways.Add(lway);
                        }
                        else
                        {
                            _SelectedLayaways.RemoveRange(0, _SelectedLoans.Count);
                            _SelectedLayaways.Insert(0, lway);
                        }
                        loanRemoved = false;
                    }
                }
            }

            UpdateButtonsStates(true);
        }
        /*__________________________________________________________________________________________*/
        private void callLockProductsTab()
        {
            GlobalDataAccessor.Instance.DesktopSession.LockProductsTab = true;

            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "LoanService";
            this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }
        /*__________________________________________________________________________________________*/
        private void callUnLockProductsTab()
        {
            GlobalDataAccessor.Instance.DesktopSession.LockProductsTab = false;
            this.customButtonCancel.Visible = true;
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "LoanService";
            this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }
        /*__________________________________________________________________________________________*/
        /*        private void CallViewReceipt(string sReceiptNumber)
                {
                    // View Receipt Form
                    List<Receipt> receipts;
                    string errorMsg;
                    if (!LookupReceipt.LoadReceiptData(sReceiptNumber, out receipts, out errorMsg))
                    {
                        MessageBox.Show("Cannot view receipt.");
                        return;
                    }
                    else
                    {
                        this.NavControlBox.IsCustom = true;
                        this.NavControlBox.CustomDetail = "ViewReceipt";
                        this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
                        //ViewReceipt myForm = new ViewReceipt(sReceiptNumber);
                        //myForm.ShowDialog();
                    }
                }
        */
        /*__________________________________________________________________________________________*/
        /// <summary>
        /// Update temp status and the other processes in pickup
        /// </summary>
        /*__________________________________________________________________________________________*/
        private void ContinueBuyoutProcess()
        {
            try
            {
                List<PawnLoan> serviceLoans = GlobalDataAccessor.Instance.DesktopSession.BuyoutLoans;
                //Check if the customer has a valid concealed weapons permit in the store state
                //If they do, check if background reference number is required
                if (serviceLoans.Count == 0)
                    return;
                bool _updateBuyoutStatus = true;

                //To proceed either the background check should have been completed
                //or _updatePickupStatus should be true. If the item being picked up is not a gun
                //pickupbackgroundcheckcompleted will  be false but the other one will be true hence
                //the user will be able to proceed
                if (GlobalDataAccessor.Instance.DesktopSession.BackgroundCheckCompleted || _updateBuyoutStatus)
                {
                    string strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                    List<PawnLoan> selectedLoans = GlobalDataAccessor.Instance.DesktopSession.BuyoutLoans;
                    //After the override check if there are still selected loans to process then proceed
                    if (!(selectedLoans.Count > 0))
                    {
                        UpdateTicketSelections();
                        return;
                    }

                    ServiceLoanProcedures.CheckCurrentTempStatus(ref selectedLoans, strUserId, ServiceTypes.BUYOUT);
                    if (selectedLoans.Count > 0)
                    {
                        //update the pickup allowed and temp status of the selected loans
                        //which were picked up
                        StringBuilder loansPickedup = new StringBuilder();
                        for (int i = 0; i < selectedLoans.Count; i++)
                        {
                            PawnLoan loanToUpdate = selectedLoans[i];
                            loanToUpdate.TempStatus = StateStatus.BYOUT;
                            loanToUpdate.PickupAllowed = false;
                            ShowStatusValue(loanToUpdate);
                            loansPickedup.Append(loanToUpdate.TicketNumber.ToString());
                            loansPickedup.Append(System.Environment.NewLine);
                            UpdateServiceIndicator(loanToUpdate.TicketNumber, ServiceIndicators.Buyout.ToString());
                        }
                        GlobalDataAccessor.Instance.DesktopSession.ServiceLoans = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Union(selectedLoans).ToList();
                        //DisableAllServiceButtons();
                        UpdateTicketSelections();
                        if (GlobalDataAccessor.Instance.DesktopSession.TotalBuyoutAmount != 0 || GlobalDataAccessor.Instance.DesktopSession.TotalOtherPickupItems != 0)
                        {
                            ServiceAmount = GlobalDataAccessor.Instance.DesktopSession.TotalOtherPickupItems + GlobalDataAccessor.Instance.DesktopSession.TotalBuyoutAmount;
                            GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount = ServiceAmount;
                            //Support.Logic.CashlinxPawnSupportSession.Instance.TotalPickupAmount = _totalPickupAmount;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No loans to process for Buyout");
                    }
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Error in the pickup process", new ApplicationException(ex.Message));
            }
            finally
            {
                _continuePickupProcess = false;
                GlobalDataAccessor.Instance.DesktopSession.PickupProcessContinue = false;
            }
        }
        /*__________________________________________________________________________________________*/
        private void ContinuePawnPickupProcess()
        {
            /*          try
                      {
                          List<PawnLoan> serviceLoans = GlobalDataAccessor.Instance.DesktopSession.PickupLoans;
                          if (serviceLoans.Count == 0)
                              return;

                          if (!ContinueAfterBackgroundChecked())
                          {
                              return;
                          }
                          string strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                          bool overrideCheck = false;
                          var overrideFailedMessage = string.Empty;
                          List<PawnLoan> selectedLoans = GlobalDataAccessor.Instance.DesktopSession.PickupLoans;

                          //Check overrides
                          overrideCheck = ServiceLoanProcedures.CheckForOverrides(ServiceTypes.PICKUP, GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber,
                                                                                  ref selectedLoans, ref overrideFailedMessage);
                          if (!overrideCheck)
                          {
                              InfoDialog infoFrm = new InfoDialog();
                              infoFrm.MessageToShow = overrideFailedMessage;
                              infoFrm.ShowDialog();
                          }
                          //After the override check if there are still selected loans to process then proceed
                          if (!(selectedLoans.Count > 0))
                          {
                              UpdateTicketSelections();
                              return;
                          }

                          ServiceLoanProcedures.CheckCurrentTempStatus(ref selectedLoans, strUserId, ServiceTypes.PICKUP);

                          if (selectedLoans.Count > 0)
                          {
                              //update the pickup allowed and temp status of the selected loans
                              //which were picked up
                              StringBuilder loansPickedup = new StringBuilder();
                              _totalPickupAmount = 0;
                              for (int i = 0; i < selectedLoans.Count; i++)
                              {
                                  PawnLoan loanToUpdate = selectedLoans[i];
                                  loanToUpdate.TempStatus = StateStatus.P;
                                  loanToUpdate.PickupAllowed = false;
                                  ShowStatusValue(loanToUpdate);
                                  loansPickedup.Append(loanToUpdate.TicketNumber.ToString());
                                  loansPickedup.Append(System.Environment.NewLine);
                                  _totalPickupAmount += loanToUpdate.PickupAmount;
                                  //Update the service indicator to indicate Pickup
                                  UpdateServiceIndicator(loanToUpdate.TicketNumber, ServiceIndicators.Pickup.ToString());
                              }
                              GlobalDataAccessor.Instance.DesktopSession.ServiceLoans = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Union(selectedLoans).ToList();

                              DisableAllServiceButtons();
                              UpdateTicketSelections();

                              if (_totalPickupAmount != 0)
                              {
                                  ServiceAmount += _totalPickupAmount;
                                  GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount = ServiceAmount;
                                  GlobalDataAccessor.Instance.DesktopSession.TotalPickupAmount = _totalPickupAmount;
                              }
                          }
                          else
                          {
                              MessageBox.Show("No loans to process for Pickup");
                          }
                      }
                      catch (Exception ex)
                      {
                          BasicExceptionHandler.Instance.AddException("Error in the pickup process", new ApplicationException(ex.Message));
                      }
                      finally
                      {
                          _continuePickupProcess = false;
                          GlobalDataAccessor.Instance.DesktopSession.PickupProcessContinue = false;
                      } */
        }
        /*__________________________________________________________________________________________*/
        private void RefreshTempStatus(int iTicketNumber, string sStoreNumber)
        {
            //If the refresh button is clicked get the latest temp status of the loan
            List<int> pfieTicketNumbers = new List<int>();
            List<string> pfieStoreNumbers = new List<string>();
            pfieTicketNumbers.Add(iTicketNumber);
            pfieStoreNumbers.Add(sStoreNumber);

            DataTable currentTempStatus;
            var errorCode = string.Empty;
            var errorMsg = string.Empty;

            CustomerLoans.CheckForTempStatus(pfieTicketNumbers, pfieStoreNumbers, out currentTempStatus, out errorCode, out errorMsg);
            if (currentTempStatus != null && currentTempStatus.Rows.Count > 0)
            {
                foreach (DataRow dr in currentTempStatus.Rows)
                {
                    //check if the loan still has PFIE status
                    //If it does not, update the temp status in the pawnloans or pawnloans auxillary 
                    //depending on where the ticket number exists
                    if (!(dr[Tempstatuscursor.TEMPSTATUS].ToString().Contains("PFIE")))
                    {
                        int iDx = -1;
                        //update the loan object in pawn loans or pawn loans auxillary list
                        PawnLoan ploan = null;
                        bool additionalTicket = false;
                        iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans.FindIndex(pl => pl.TicketNumber == iTicketNumber);
                        if (iDx >= 0)
                            ploan = GlobalDataAccessor.Instance.DesktopSession.PawnLoans[iDx];
                        else
                        {
                            iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.FindIndex(
                                pl => pl.TicketNumber == iTicketNumber);
                            if (iDx >= 0)
                            {
                                ploan = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[iDx];
                                additionalTicket = true;
                            }
                        }
                        if (ploan != null)
                        {
                            ploan.TempStatus = (StateStatus)Enum.Parse(typeof(StateStatus),
                                                                       Utilities.GetStringValue(dr[Tempstatuscursor.TEMPSTATUS]) != ""
                                                                       ? Utilities.GetStringValue(dr[Tempstatuscursor.TEMPSTATUS])
                                                                       : StateStatus.BLNK.ToString());
                            if (!(additionalTicket))
                            {
                                GlobalDataAccessor.Instance.DesktopSession.PawnLoans.RemoveAt(iDx);
                                GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Insert(iDx, ploan);
                            }
                            else
                            {
                                GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.RemoveAt(iDx);
                                GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.Insert(iDx, ploan);
                            }
                        }
                    }
                }
            }
            else
            {
                FileLogger.Instance.logMessage(LogLevel.WARN, this, "Refresh temp status for PFIE loans failed to yield rows");
            }
        }
 

        /*__________________________________________________________________________________________*/
        private void LayawayCheckout()
        {
            _gunItemIdValidated = false;
            _gunItem = false;

            //List<LayawayVO> servicedLayaways = Support.Logic.CashlinxPawnSupportSession.Instance.ServiceLayaways;
            List<LayawayVO> servicedLayaways = Support.Logic.CashlinxPawnSupportSession.Instance.ServiceLayaways;
            //List<LayawayVO> pickupLayaways = Support.Logic.CashlinxPawnSupportSession.Instance.ServiceLayaways.FindAll(l => l.LoanStatus == ProductStatus.PU).ToList();
            List<LayawayVO> pickupLayaways = Support.Logic.CashlinxPawnSupportSession.Instance.ServiceLayaways.FindAll(l => l.LoanStatus == ProductStatus.PU).ToList();

            if (pickupLayaways.Count > 0)
            {
                List<LayawayVO> pickupLayawaysWithGun = new List<LayawayVO>();

                if (!Support.Logic.CashlinxPawnSupportSession.Instance.CompleteLayaway)
                {
                    foreach (LayawayVO layaway in pickupLayaways)
                    {
                        var gunItems = layaway.RetailItems.FindAll(r => r.IsGun);
                        if (gunItems.Count > 0)
                        {
                            _gunItem = true;
                            pickupLayawaysWithGun.Add(layaway);

                            if (!LayawayProcedures.CustomerPassesFirearmAgeCheckForItems(layaway, Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer))
                            {
                                Support.Logic.CashlinxPawnSupportSession.Instance.CompleteLayaway = false;
                                MessageBox.Show("Customer does not meet age criteria for the sale of guns");
                                return;
                            }
                        }
                    }

                    if (_gunItem && !ContinuePickup)
                    {
                        _gunItemIdValidated = true;
                        //This session data will be used by manager pawn application
                        //in case a manager override is needed for out of state ID
                        Support.Logic.CashlinxPawnSupportSession.Instance.OverrideTransactionNumbers = overrideTransactionNumbers;
                        this.NavControlBox.IsCustom = true;
                        this.NavControlBox.CustomDetail = "ManagePawnApplication";
                        this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
                        return;
                    }
                }

                if (!ContinueAfterBackgroundChecked())
                {
                    return;
                }

                if (_gunItem && Support.Logic.CashlinxPawnSupportSession.Instance.BackgroundCheckFeeValue > 0)
                {
                    decimal[] feeValues = Utilities.GetDistributeValuesForCurrencyOverItems(Support.Logic.CashlinxPawnSupportSession.Instance.BackgroundCheckFeeValue, pickupLayawaysWithGun.Count);
                    for (int i = 0; i < pickupLayawaysWithGun.Count; i++)
                    {
                        LayawayProcedures.SetBackgroundCheckFee(pickupLayawaysWithGun[i], feeValues[i]);
                    }
                }
            }

            Support.Logic.CashlinxPawnSupportSession.Instance.CompleteLayaway = true;

            decimal totalServiceAmount = servicedLayaways.SelectMany(lay => lay.Payments).Sum(layPmt => layPmt.Amount) + Support.Logic.CashlinxPawnSupportSession.Instance.BackgroundCheckFeeValue;
            Support.Logic.CashlinxPawnSupportSession.Instance.TenderTransactionAmount.TotalAmount = totalServiceAmount;
            Support.Logic.CashlinxPawnSupportSession.Instance.TenderTransactionAmount.SubTotalAmount = totalServiceAmount;
            Support.Logic.CashlinxPawnSupportSession.Instance.TenderTransactionAmount.SalesTaxPercentage = 0.0M;
            Support.Logic.CashlinxPawnSupportSession.Instance.DisableCoupon = true;
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "ProcessTender";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }
        /*__________________________________________________________________________________________*/
        private void LoanCheckout()
        {
            bool cashProcessed = false;

            if (Support.Logic.CashlinxPawnSupportSession.Instance.TotalServiceAmount > 0)
            {
                TenderCash cashTenderForm = new TenderCash();
                cashTenderForm.ShowDialog();
                if (Support.Logic.CashlinxPawnSupportSession.Instance.CashTenderFromCustomer > 0)
                    cashProcessed = true;
            }
            else
            {
                cashProcessed = true;
            }

            //Commented - Madhu
            //if (cashProcessed)
            //{
            //    //Call process tender
            //    var processTender = new ProcessTender(ProcessTenderProcedures.ProcessTenderMode.SERVICELOAN);
            //    processTender.ShowDialog();

            //    Support.Logic.CashlinxPawnSupportSession.Instance.ClearPawnLoan();
            //    Support.Logic.CashlinxPawnSupportSession.Instance.ActivePawnLoan = null;

            //    DialogResult dR = MessageBox.Show("Do you want to continue processing this customer?", "Prompt",
            //                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dR == DialogResult.No)
            //    {
            //        Support.Logic.CashlinxPawnSupportSession.Instance.ClearCustomerList();
            //        NavControlBox.IsCustom = false;
            //        this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
            //    }
            //    else
            //    {
            //        //Clear the logged in user
            //        Support.Logic.CashlinxPawnSupportSession.Instance.ClearLoggedInUser();
            //        Support.Logic.CashlinxPawnSupportSession.Instance.PerformAuthorization();
            //        if (!string.IsNullOrEmpty(Support.Logic.CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserName))
            //        //reload
            //        {
            //            NavControlBox.IsCustom = true;
            //            NavControlBox.CustomDetail = "Reload";
            //            NavControlBox.Action = NavBox.NavAction.SUBMIT;
            //        }
            //        else
            //        {
            //            Support.Logic.CashlinxPawnSupportSession.Instance.ClearCustomerList();
            //            NavControlBox.IsCustom = false;
            //            this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
            //        }
            //    }
            //}
        }
        /*__________________________________________________________________________________________*/
        private void UndoLayawayTransactions(List<LayawayVO> layaways)
        {
            foreach (LayawayVO layaway in layaways)
            {
                GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.Remove(layaway);
                ServiceAmount -= layaway.Payments.Last().Amount;
                layaway.Payments.Remove(layaway.Payments.Last());
                UpdateServiceIndicator(layaway.TicketNumber, ServiceIndicators.Blank.ToString());
            }
        }
        /*__________________________________________________________________________________________*/
        private void UndoPawnTransactions(List<PawnLoan> loansToUndo)
        {
            /*
            var selectedLoans = loansToUndo;
            var selectedServiceLoanNumbers = new List<int>();
            var loanTempStatus = new List<TupleType<int, StateStatus, decimal>>();
            var amountToDeductFromServiceAmount = 0.0M;
            var pickupAmountTodeduct = 0.0M;
            var extensionAmountToDeduct = 0.0M;
            var renewalAmountToDeduct = 0.0M;
            var paydownAmountToDeduct = 0.0M;
            var ppmtAmountToDeduct = 0.0M;
            //Only used if loan is still marked RO and no distinction has been made to renew or paydown
            var rolloverAmountToDeduct = 0.0M;

            foreach (var pl in selectedLoans)
            {
                selectedServiceLoanNumbers.Add(pl.TicketNumber);
                TupleType<int, StateStatus, decimal> loanTempData = null;
                switch (pl.TempStatus)
                {
                    case StateStatus.P:
                        loanTempData = new TupleType<int, StateStatus, decimal>(
                            pl.TicketNumber, pl.TempStatus, pl.PickupAmount);
                        break;
                    case StateStatus.E:
                        loanTempData = new TupleType<int, StateStatus, decimal>(
                            pl.TicketNumber, pl.TempStatus, pl.ExtensionAmount);
                        break;
                    case StateStatus.RN:
                        loanTempData = new TupleType<int, StateStatus, decimal>(
                            pl.TicketNumber, pl.TempStatus, pl.RenewalAmount);
                        break;
                    case StateStatus.PD:
                        loanTempData = new TupleType<int, StateStatus, decimal>(
                            pl.TicketNumber, pl.TempStatus, pl.PaydownAmount);
                        break;
                    case StateStatus.RO:
                        loanTempData = new TupleType<int, StateStatus, decimal>(
                            pl.TicketNumber, pl.TempStatus,
                            (pl.RenewalAmount <= 0.0M) ? pl.PaydownAmount : pl.RenewalAmount);
                        break;
                    case StateStatus.PPMNT:
                        decimal partialPmtAmt= pl.PartialPayments.Where(ppmt => ppmt.Status_cde == "New").Sum(ppmt => ppmt.PMT_AMOUNT);
                        loanTempData = new TupleType<int, StateStatus, decimal>(
                            pl.TicketNumber, pl.TempStatus,partialPmtAmt);
                        break;

                }
                if (loanTempData != null)
                    loanTempStatus.Add(loanTempData);
            }
            if (selectedLoans.Count > 0)
            {
                var errorCode = string.Empty;
                var errorMsg = string.Empty;
                //Ret types of all products will be loans
                List<string> lstRefTypes = new List<string>();
                for (int i = 0; i < selectedServiceLoanNumbers.Count; i++)
                    lstRefTypes.Add("1");
                bool retVal = StoreLoans.UpdateTempStatus(selectedServiceLoanNumbers, StateStatus.BLNK, strStoreNumber, true, lstRefTypes, out errorCode, out errorMsg);

                if (retVal)
                {
                    foreach (int tktNumber in selectedServiceLoanNumbers)
                    {
                        UpdateServiceIndicator(tktNumber, ServiceIndicators.Blank.ToString());
                        int number = tktNumber;
                        var loanToUpdate = new TupleType<int, StateStatus, decimal>(0, StateStatus.BLNK, 0.0M);

                        try
                        {
                            IEnumerable<TupleType<int, StateStatus, decimal>> loansToUpdate = (loanTempStatus.Where(loan => loan.Left == number));
                            if (loansToUpdate != null && loansToUpdate.Count() > 0)
                            {
                                loanToUpdate = loansToUpdate.First();
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        if (loanToUpdate.Mid == StateStatus.P)
                        {
                            pickupAmountTodeduct += loanToUpdate.Right;
                        }
                        else if (loanToUpdate.Mid == StateStatus.E)
                        {
                            extensionAmountToDeduct += loanToUpdate.Right;
                        }
                        else if (loanToUpdate.Mid == StateStatus.PD)
                        {
                            paydownAmountToDeduct += loanToUpdate.Right;
                        }
                        else if (loanToUpdate.Mid == StateStatus.RN)
                        {
                            renewalAmountToDeduct += loanToUpdate.Right;
                        }
                        else if (loanToUpdate.Mid == StateStatus.RO)
                        {
                            rolloverAmountToDeduct += loanToUpdate.Right;
                        }
                        else if (loanToUpdate.Mid == StateStatus.PPMNT)
                        {
                            ppmtAmountToDeduct += loanToUpdate.Right;
                        }
                        amountToDeductFromServiceAmount += loanToUpdate.Right;
                        //Remove the loan from service loan
                        int loanNo = tktNumber;
                        int index = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.FindIndex(loan => loan.TicketNumber == loanNo);
                        if (index >= 0)
                        {
                            GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.RemoveAt(index);
                        }
                        //Set the temp status to blank on the loan in the PawnLoans or PawnLoans_Auxillary loan list
                        //Since we cannot revert to what it was as the temp status in DB has now been changed to blank
                        int iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans.FindIndex(pl => pl.TicketNumber == loanNo);
                        if (iDx >= 0)
                        {
                            GlobalDataAccessor.Instance.DesktopSession.PawnLoans[iDx].TempStatus =
                            StateStatus.BLNK;
                        }
                        else
                        {
                            iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.FindIndex(
                                pl => pl.TicketNumber == loanNo);
                            if (iDx >= 0)
                            {
                                GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[iDx].TempStatus = StateStatus.BLNK;
                            }
                        }
                    }
                    if (renewalAmountToDeduct > 0.0M)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.TotalRenewalAmount -= renewalAmountToDeduct;
                    }
                    if (paydownAmountToDeduct > 0.00M)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.TotalPaydownAmount -= paydownAmountToDeduct;
                    }
                    if (renewalAmountToDeduct <= 0.00M && paydownAmountToDeduct <= 0.00M)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.TotalRolloverAmount -=
                        rolloverAmountToDeduct;
                    }
                    if (extensionAmountToDeduct > 0.00M)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.TotalExtendAmount -= extensionAmountToDeduct;
                    }
                    if (pickupAmountTodeduct > 0.00M)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.TotalPickupAmount -= pickupAmountTodeduct;
                    }
                    
                    if (ServiceAmount > 0)
                    ServiceAmount -= amountToDeductFromServiceAmount;
                    //Remove the selected flag on all the rows
                    UpdateTicketSelections();
                    DisableAllServiceButtons();
                    //If there are no more service loans show all tabs
                    if (GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Count == 0)
                        callUnLockProductsTab();
                }
                else
                {
                    MessageBox.Show("Error in the undo process");
                    BasicExceptionHandler.Instance.AddException("Calling update temp status to undo pickup failed",
                                                                new ApplicationException());
                }
            } */
        }
        /*__________________________________________________________________________________________*/
        private void UndoSkippedLoans(List<PawnLoan> selectedLoans)
        {
            List<int> skippedTicketNumbers = GlobalDataAccessor.Instance.DesktopSession.SkippedTicketNumbers;
            List<PawnLoan> skippedLoans = new List<PawnLoan>();
            foreach (int tktNo in skippedTicketNumbers)
            {
                int i = tktNo;
                int idx = selectedLoans.FindIndex(loan => loan.TicketNumber == i);
                if (idx >= 0)
                    skippedLoans.Add(selectedLoans[idx]);
            }
            if (skippedLoans.Count > 0)
                UndoPawnTransactions(skippedLoans);
        }


        /*__________________________________________________________________________________________*/
        private void UpdateButtonsStates(bool isLayaway)
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            /*
                        // Set the buttons
                        //If there are any service loans checkout is enabled
                        if (cds.ServiceLoans != null)
                            PS_CheckoutButton.Enabled = cds.ServiceLoans.Count > 0;
                        if (!PS_CheckoutButton.Enabled)
                        {
                            if (cds.ServiceLayaways != null)
                                PS_CheckoutButton.Enabled = cds.ServiceLayaways.Count > 0;
                        }
                        //If there are any service loans waive prorate button is enabled
                        //SR 02/08/2010 Disable for pilot
                        //PS_WaiveProrate.Enabled = Support.Logic.CashlinxPawnSupportSession.Instance.ServiceLoans.Count > 0;
                        PS_WaiveProrate.Enabled = false;

                        //check if logged in user can do services
                        bool servicesAllowed = false;
                        UserVO currUser = cds.LoggedInUserSecurityProfile;
                        if (currUser != null)
                        {
                            if (SecurityProfileProcedures.CanUserViewResource("SERVICES", currUser, GlobalDataAccessor.Instance.DesktopSession))
                                servicesAllowed = true;
                        }
                        if (servicesAllowed)
                        {
                            //If extension is allowed in the store state button is enabled
                            PS_ExtendButton.Enabled = (extensionServiceAllowed && !isLayaway && selectedLoanInStore()) ? true : false;
                            //Pickup button is enabled always unless the logged in user
                            //does not have permissions to do pickup
                            PS_PickUpButton.Enabled = !isLayaway;
                            PS_RollOverButton.Enabled = (paydownAllowed || renewalAllowed) && !isLayaway;
                            PS_PartPmntButton.Enabled = (partialPaymentAllowed && !isLayaway && selectedLoanInStore()) ? true : false;

                            LW_LayawayPaymentButton.Enabled = isLayaway && IsAtLeastOneSelectedLayawayNotServiced() && selectedLayawayInStore();
                            LW_LayawayTerminateButton.Enabled = isLayaway && IsExactlyOneSelectedLayawayNotServiced() && selectedLayawayInStore();
                        }
                        else
                        {
                            PS_ExtendButton.Enabled = false;
                            PS_PickUpButton.Enabled = false;
                            PS_RollOverButton.Enabled = false;
                            PS_PartPmntButton.Enabled = false;
                            LW_LayawayPaymentButton.Enabled = false;
                            LW_LayawayTerminateButton.Enabled = false;
                        }
                        //if loan up service is allowed in the store state button is enabled
                        PS_ViewNewLoanDetailsButton.Enabled = loanupServiceAllowed ? true : false;

                        //Add more tickets is enabled only when no service loans exist
                        if (cds.ServiceLoans != null)
                        {
                            this.PS_AddMoreTicketsButton.Enabled = cds.ServiceLoans.Count == 0 && !isLayaway;
                        }
                        //If any loans are selected for service undo, waive buttons are enabled
                        PS_UnDoButton.Enabled = ShouldEnableUndoButton();
            */
            if (GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways != null)
            {
                this.PS_ShowComboBox.Enabled = GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.Count == 0 && GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Count == 0 && !GlobalDataAccessor.Instance.DesktopSession.ServicePawnLoans;
            }

            if (isLayaway)
            {
                ShowLayawayPanels();
            }
            else
            {
                ShowPawnPanels();
            }
        }
        /// <summary>
        /// Update the service amount whenever the total service amount is updated
        /// </summary>
        /*__________________________________________________________________________________________*/
        private void UpdateServiceAmountLabel()
        {
            labelServiceAmount.Text = String.Format("{0:C}", _totalServiceAmount);
        }
        /// <summary>
        /// call to update the service indicator to show
        /// the type of service being processed on the loan
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <param name="serviceIndicator"></param>
        /*__________________________________________________________________________________________*/
        private void UpdateServiceIndicator(int ticketNumber, string serviceIndicator)
        {
            var indicatorSet = false;
            foreach (DataGridViewRow dgvr in PS_TicketsDataGridView.Rows)
            {
                var ticketValue = dgvr.Cells["PS_Tickets_TicketNumberColumn"].Value.ToString();
                if (Utilities.GetIntegerValue(ticketValue.Substring(5, (ticketValue.Length - 5))) == ticketNumber)
                {
                    if (serviceIndicator == ServiceIndicators.Blank.ToString())
                        dgvr.Cells["PS_Tickets_ServiceIndicatorColumn"].Value = string.Empty;
                    else
                    {
                        dgvr.Cells["PS_Tickets_ServiceIndicatorColumn"].Value += serviceIndicator;
                    }

                    indicatorSet = true;

                    break;
                }
            }
            if (!indicatorSet)
            {
                foreach (DataGridViewRow dgvr in PS_AddTicketsDataGridView.Rows)
                {
                    var ticketValue = dgvr.Cells["PS_AddTickets_TicketNumberColumn"].Value.ToString();
                    if (Utilities.GetIntegerValue(ticketValue.Substring(5, (ticketValue.Length - 5))) == ticketNumber)
                    {
                        if (serviceIndicator == ServiceIndicators.Blank.ToString())
                            dgvr.Cells["PS_AddTickets_ServiceIndicator"].Value = string.Empty;
                        else
                        {
                            dgvr.Cells["PS_AddTickets_ServiceIndicator"].Value += serviceIndicator;
                        }
                        break;
                    }
                }
            }
        }
 
        /// <summary>
        /// Method to check if all the selected loans are eligible for the service selected
        /// </summary>
        /// <returns></returns>


#endregion
        #region EVENTS
        /*__________________________________________________________________________________________*/
        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            //If there are any loans set for service undo them
            //if (GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Count > 0)
            //{
            //    DialogResult dgr = MessageBox.Show("All loans set for service will be cancelled.Do you want to Continue?", "Prompt", MessageBoxButtons.YesNo);
            //    if (dgr == DialogResult.No)
            //        return;

            //    UndoPawnTransactions(GlobalDataAccessor.Instance.DesktopSession.ServiceLoans);
            //}
            //DialogResult dR = MessageBox.Show("Do you want to continue processing this customer?", "Products and Services", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dR == DialogResult.No)
            //{
            //    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
            //}

            //1/29/2010 According to QA requirement Cancel should take you to ring menu!
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "ViewPersonalInformationHistory";
            this.NavControlBox.Action = NavBox.NavAction.BACK;
        }
        /*__________________________________________________________________________________________*/
        private void PS_ShowComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PS_PawnLoanLabel.Visible = false;
            PS_PawnNameLabel.Visible = false;
            PS_ItemDescriptionDataGridView.Visible = false;
            PS_LoanStatsLayoutPanel.Visible = false;
            tlpDocuments.Visible = false;

            if (_Setup)
            {
                LoadData(GetSelectedProductType());
            }
        }
        #region LINKED KEYS
        /*__________________________________________________________________________________________*/

        /*__________________________________________________________________________________________*/
        private void LnkReasonType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        #endregion
        #region DGV MOUSE CONTROL
        /*__________________________________________________________________________________________*/
        private void PS_TicketsDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (GetSelectedProductType() == SupportProductType.PDL)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (e.ColumnIndex == 7)
                    {
                        int i = e.RowIndex;
                        DataGridViewRow myRow = PS_TicketsDataGridView.Rows[i];
                        string cellValue = PS_TicketsDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString();
                        string LoanApplicationId = Utilities.GetStringValue(myRow.Cells["LoanApplicationId"].Value, "");
                        if (checkNull(cellValue).Equals(DECLINE_VALUE))
                        {
                            int iDx = 0;
                            //foreach (var record in Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys)
                            //{
                            //    if(record.LoanApplicationId.Equals(LoanApplicationId))
                            //    {
                            //        iDx++;
                            //        break;
                            //    }
                            //    iDx++;
                            //}

                            PDLoan pdLoan = new PDLoan();
                            //int iDx = Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys.FindIndex(delegate(PDLoan p)
                            //    {
                            //        return p.LoanApplicationId.Equals(LoanApplicationId);
                            //    });
                            iDx = Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys.FindIndex(item => item.LoanApplicationId == LoanApplicationId);
                            if (iDx >= 0)
                            {
                                pdLoan = Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys[iDx];
                                Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan = pdLoan;
                            }
                            //MessageBox.Show("Call the new form here....");
                            DisplayReasonCode displayRC = new DisplayReasonCode();
                            displayRC.ShowDialog();
                            return;
                        }
                    }
                    CelMouseUpActions(e.RowIndex, e.ColumnIndex);
                }
            }
        }
        /*__________________________________________________________________________________________*/
        private void PS_TicketsDataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (e.ColumnIndex == 7)
                {
                    string cellValue = PS_TicketsDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString();
                    if (checkNull(cellValue).Equals(DECLINE_VALUE))
                    {
                        this.Cursor = Cursors.Hand;
                    }
                }
            }
        }
        /*__________________________________________________________________________________________*/
        private void PH_ReceiptsDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {
                int ticketNumber = this._currentTicketNumber;
                if (ticketNumber == 0)
                    ticketNumber = GetFirstRowTicketNumber();
                List<CouchDbUtils.PawnDocInfo> pawnDocs;
                var errString = string.Empty;
                string strReceiptNumber = PH_ReceiptsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                int intReceiptNumber = 0;
                intReceiptNumber = Convert.ToInt32(strReceiptNumber);
                //If a legit ticket number was pulled, then continue.
                if (ticketNumber > 0)
                {
                    //Instantiate docinfo which will return info we need to be able to 
                    //call reprint ticket.
                    var docInfo = new CouchDbUtils.PawnDocInfo();
                    docInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.RECEIPT);
                    docInfo.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                    docInfo.TicketNumber = ticketNumber;
                    //int receiptNumber = 0;
                    //if (!string.IsNullOrEmpty(PS_ReceiptNoValue.Text))
                    //receiptNumber = Convert.ToInt32(PS_ReceiptNoValue.Text);
                    docInfo.ReceiptNumber = intReceiptNumber;

                    //Use couch DB to get the document.
                    if (CouchDbUtils.GetPawnDocument(GlobalDataAccessor.Instance.OracleDA, GlobalDataAccessor.Instance.CouchDBConnector,
                                                     docInfo, false, out pawnDocs, out errString))
                    {
                        if (pawnDocs != null)
                        {
                            //Find that there is a document with a receipt.
                            var results = from p in pawnDocs
                                          where p.DocumentType ==
                                                Document.DocTypeNames.RECEIPT
                                          select p;
                            if (results.Any())
                            {
                                //Get the only one receipt that should exist.
                                docInfo = results.First();

                                //Call the reprint screen.
                                ViewPrintDocument docViewPrint = new ViewPrintDocument("Receipt# ", strReceiptNumber, docInfo.StorageId, docInfo.DocumentType, docInfo);
                                docViewPrint.ShowDialog();
                            }
                            else
                            {
                                Console.WriteLine(
                                    "Did not find a receipt for ticket: " + ticketNumber.ToString());
                                //Todo: Log or show problem if one exists here.
                            }
                        }
                    }
                    else if (this._currReceiptLookupInfo != null)
                    {
                        ViewPrintDocument docViewPrint = new ViewPrintDocument("Receipt# ", strReceiptNumber, this._currReceiptLookupInfo.StorageID, this._currReceiptLookupInfo.DocumentType, docInfo);
                        docViewPrint.ShowDialog();
                    }
                }
            }
        }
        /*__________________________________________________________________________________________*/
        private void PS_ItemDescriptionDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                DataGridViewRow myRow = PS_ItemDescriptionDataGridView.Rows[e.RowIndex];
                int itemTicketNumber = 0;
                itemTicketNumber = Utilities.GetIntegerValue(myRow.Cells[PS_ItemDescriptionDataGridView_SelectedLoan.Index].Value);

                if (GetSelectedProductType() == SupportProductType.PAWN)
                {
                    PawnLoan pawnLoan = new PawnLoan();
                    int iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans.FindIndex(delegate(PawnLoan p)
                    {
                        return p.TicketNumber == itemTicketNumber;
                    });

                    if (iDx >= 0)
                        pawnLoan = GlobalDataAccessor.Instance.DesktopSession.PawnLoans[iDx];
                    else
                    {
                        iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.FindIndex(delegate(PawnLoan p)
                        {
                            return p.TicketNumber == itemTicketNumber;
                        });
                        if (iDx >= 0)
                            pawnLoan = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary[iDx];
                    }

                    string sICN = Utilities.GetStringValue(myRow.Cells[PS_Description_ICNColumn.Index].Value, "");

                    if (iDx >= 0)
                    {
                        PawnLoan activePawnLoan = null;
                        if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan != null)
                        {
                            activePawnLoan = Utilities.CloneObject(GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan);
                        }

                        int iPawnItemIdx = pawnLoan.Items.FindIndex(delegate(Item pi)
                        {
                            return pi.Icn == sICN;
                        });

                        // Need to populate pawnLoan from GetCat5
                        int iCategoryMask = GlobalDataAccessor.Instance.DesktopSession.CategoryXML.GetCategoryMask(pawnLoan.Items[iPawnItemIdx].CategoryCode);
                        DescribedMerchandise dmPawnItem = new DescribedMerchandise(iCategoryMask);
                        Item pawnItem = pawnLoan.Items[iPawnItemIdx];
                        Item.PawnItemMerge(ref pawnItem, dmPawnItem.SelectedPawnItem, true);
                        pawnLoan.Items.RemoveAt(iPawnItemIdx);
                        pawnLoan.Items.Insert(iPawnItemIdx, pawnItem);
                        // End GetCat5 populate
                        //Add the current loan as the active pawn loan
                        GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Insert(0, pawnLoan);
                        // Placeholder for ReadOnly DescribedItem.cs
                        DescribeItem myForm = new DescribeItem(GlobalDataAccessor.Instance.DesktopSession, CurrentContext.READ_ONLY, iPawnItemIdx)
                        {
                            SelectedProKnowMatch = pawnLoan.Items[iPawnItemIdx].SelectedProKnowMatch
                        };
                        myForm.ShowDialog(this);

                        // Reset Active Pawn Loan back to Original
                        if (activePawnLoan != null)
                            GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan = activePawnLoan;
                        else
                            GlobalDataAccessor.Instance.DesktopSession.PawnLoans.RemoveAt(0);
                    }
                }
                else
                {
                    LayawayVO layaway = new LayawayVO();
                    int iDx = GlobalDataAccessor.Instance.DesktopSession.Layaways.FindIndex(delegate(LayawayVO l)
                    {
                        return l.TicketNumber == itemTicketNumber;
                    });
                    string sICN = Utilities.GetStringValue(myRow.Cells[PS_Description_ICNColumn.Index].Value, "");
                    if (iDx >= 0)
                    {
                        layaway = GlobalDataAccessor.Instance.DesktopSession.Layaways[iDx];

                        int iPawnItemIdx = layaway.RetailItems.FindIndex(pi => pi.Icn == sICN);

                        int iItemIdx = iPawnItemIdx;
                        if (iItemIdx >= 0 && GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway != null)
                        {
                            //Show describe item form as show dialog
                            int iCategoryMask = GlobalDataAccessor.Instance.DesktopSession.CategoryXML.GetCategoryMask(layaway.RetailItems[iItemIdx].CategoryCode);
                            DescribedMerchandise dmPawnItem = new DescribedMerchandise(iCategoryMask);
                            Item pawnItem = layaway.RetailItems[iItemIdx];
                            Item.PawnItemMerge(ref pawnItem, dmPawnItem.SelectedPawnItem, true);
                            ((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway).Items.Insert(0, pawnItem);
                            // End GetCat5 populate
                            // Placeholder for ReadOnly DescribedItem.cs
                            DescribeItem myForm = new DescribeItem(GlobalDataAccessor.Instance.DesktopSession, CurrentContext.READ_ONLY, 0)
                            {
                                SelectedProKnowMatch = ((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway).Items[0].SelectedProKnowMatch
                            };
                            myForm.ShowDialog(this);
                        }
                    }
                }
            }
        }
        /*__________________________________________________________________________________________*/
        private void PS_AddTicketsDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                int i = e.RowIndex;
                DataGridViewRow myRow = PS_AddTicketsDataGridView.Rows[i];
                bool bRowSelected = myRow.Selected;

                string sCellTicket = Utilities.GetStringValue(myRow.Cells["PS_AddTickets_TicketNumberColumn"].Value, "");
                string sStoreNumber = sCellTicket.Substring(0, 5);
                int iTicketNumber = Convert.ToInt32(sCellTicket.Substring(5));

                PS_AddTicketsDataGridView.EndEdit();
                if (e.ColumnIndex == 4)
                {
                    RefreshTempStatus(iTicketNumber, sStoreNumber);
                    //TODO - revisit - Mahdu
                    //myRow.Cells["PS_AddTickets_Refresh"].Value = Properties.Resources.blank;
                }
                else
                {
                    int index = _AuxillaryLoanKeys.FindIndex(pl => pl.TicketNumber == iTicketNumber);

                    if (index >= 0)
                        ApplyBusinessRules(_AuxillaryLoanKeys[index], sStoreNumber, iTicketNumber, true, false);
                    else
                        BasicExceptionHandler.Instance.AddException("Loan not found in Auxillary Loan Keys",
                                                                    new ApplicationException(
                                                                        "Auxillary LoanKeys keys does not have selected loan"));

                    if (loanRemoved)
                        myRow.DefaultCellStyle.SelectionBackColor = Color.White;
                    else
                        myRow.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
                    if (showRefreshIcon)
                        myRow.Cells["PS_AddTickets_Refresh"].Value =
                        global::Common.Properties.Resources.refresh_icon;

                    if (bRowSelected)
                    {
                        //_ActiveLoanIndex = PS_AddTicketsDataGridView.Rows.Count + i;
                        UpdateActivePawnInformation(iTicketNumber);
                        //SR 6/15/2010 Load all the documents associated to the selected ticket
                        LoadDocuments(iTicketNumber, SupportProductType.PAWN);
                    }
                }
            }
        }
        #endregion
        #region KEY CONTROL
        /*__________________________________________________________________________________________*/
        private void PS_TicketsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control || e.Shift) && string.Equals(
                Properties.Resources.MultipleLoanSelection,
                Boolean.TrueString, StringComparison.OrdinalIgnoreCase))
            {
                ctrlKeyPressed = true;
                PS_TicketsDataGridView.MultiSelect = true;
                e.Handled = false;
            }
            else
                PS_TicketsDataGridView.MultiSelect = false;
        }
        /*__________________________________________________________________________________________*/
        private void PS_TicketsDataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (ctrlKeyPressed && (e.KeyValue == 16 || e.KeyValue == 17) && string.Equals(
                Properties.Resources.MultipleLoanSelection,
                Boolean.TrueString, StringComparison.OrdinalIgnoreCase))
            {
                ctrlKeyPressed = false;
                e.Handled = false;
            }
        }
        /*__________________________________________________________________________________________*/
        private void PS_AddTicketsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && string.Equals(
                Properties.Resources.MultipleLoanSelection,
                Boolean.TrueString, StringComparison.OrdinalIgnoreCase))
            {
                ctrlKeyPressed = true;
                PS_AddTicketsDataGridView.MultiSelect = true;
                e.Handled = false;
            }
            else
                PS_AddTicketsDataGridView.MultiSelect = false;
        }
        /*__________________________________________________________________________________________*/
        private void PS_AddTicketsDataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (ctrlKeyPressed && e.KeyValue == 17 && string.Equals(
                Properties.Resources.MultipleLoanSelection,
                Boolean.TrueString, StringComparison.OrdinalIgnoreCase))
            {
                ctrlKeyPressed = false;
                e.Handled = false;
            }
        }
        #endregion
        #region BUTTON CONTROL
        /*__________________________________________________________________________________________*/
        private void PS_LostPawnTicketButton_Click(object sender, EventArgs e)
        {
            /*
            List<CustLoanLostTicketFee> ticketFees = new List<CustLoanLostTicketFee>(0);

            for (int i = 0; i < _SelectedLoans.Count; i++)
            {
                int index = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.FindIndex(pl => pl.TicketNumber == _SelectedLoans[i].TicketNumber);
                if (index >= 0)
                {
                    CustLoanLostTicketFee ticketFee = new CustLoanLostTicketFee();
                    ticketFee.LoanNumber = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[index].TicketNumber.ToString();
                    ticketFee.StoreNumber = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[index].OrgShopNumber.PadLeft(5, '0');
                    ticketFee.TicketLost = true;
                    ticketFee.LSDTicket = CustLoanLostTicketFee.LOSTTICKETTYPE;
                    ticketFee.LostTicketFee = 0;
                    ticketFees.Add(ticketFee);
                }
            }
            if (ticketFees.Count > 0)
            {
                ProcessLostPawnTicketFee myForm = new ProcessLostPawnTicketFee();
                myForm.CustomerLoans = ticketFees;
                myForm.ShowDialog(this);

                for (int i = 0; i < _SelectedLoans.Count; i++)
                {
                    PawnLoan pawnLoan;
                    int index = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.FindIndex(pl => pl.TicketNumber == _SelectedLoans[i].TicketNumber);
                    if (index < 0)
                    {
                        continue;
                    }
                    pawnLoan = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[index];

                    int iDx = ticketFees.FindIndex(
                        cf => cf.LoanNumber == pawnLoan.TicketNumber.ToString());

                    if (iDx >= 0)
                    {
                        pawnLoan.LostTicketInfo = ticketFees[iDx];
                        if (pawnLoan.LostTicketInfo.TicketLost)
                        {
                            string feedate = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                                             ShopDateTime.Instance.ShopTime.ToString();

                            //Add the fees to the fee property of pawn loan
                            Fee lostTicketFee = new Fee
                            {
                                FeeType = FeeTypes.LOST_TICKET,
                                Value = ticketFees[iDx].LostTicketFee,
                                FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now),
                                OriginalAmount = ticketFees[iDx].LostTicketFee,
                                FeeState = FeeStates.ASSESSED
                            };
                            int idx = pawnLoan.OriginalFees.FindIndex(fee => fee.FeeType == FeeTypes.LOST_TICKET);
                            if (idx >= 0)
                            {
                                pawnLoan.OriginalFees.RemoveAt(idx);
                            }
                            pawnLoan.OriginalFees.Add(lostTicketFee);
                            decimal feeAmt = lostTicketFee.OriginalAmount;
                            if (pawnLoan.TempStatus == StateStatus.P)
                            {
                                pawnLoan.PickupAmount += feeAmt;
                                _totalPickupAmount += feeAmt;
                            }
                            else if (pawnLoan.TempStatus == StateStatus.PD)
                            {
                                _totalPaydownAmount += feeAmt;
                                pawnLoan.PaydownAmount += feeAmt;
                            }
                            else if (pawnLoan.TempStatus == StateStatus.RN)
                            {
                                _totalRenewalAmount += feeAmt;
                                pawnLoan.RenewalAmount += feeAmt;
                            }
                            ServiceAmount += feeAmt;
                            GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.RemoveAt(index);
                            GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Insert(index, pawnLoan);

                            //Update the service indicator to indicate lost ticket
                            UpdateServiceIndicator(pawnLoan.TicketNumber, ServiceIndicators.LT.ToString());
                            UpdateTicketSelections();
                        }
                    }
                }
            }
            GlobalDataAccessor.Instance.DesktopSession.TotalPickupAmount = _totalPickupAmount;
            GlobalDataAccessor.Instance.DesktopSession.TotalRenewalAmount = _totalRenewalAmount;
            GlobalDataAccessor.Instance.DesktopSession.TotalPaydownAmount = _totalPaydownAmount;
            */
        }
        /*__________________________________________________________________________________________*/
        private void ps_PickupAmountValue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //no need for nav action since the form is opened only from here and
            //the only action from the view pickup amount form is to close the form
            int tktNumber = Utilities.GetIntegerValue(e.Link.LinkData);
            var pickupamtForm = new PickupAmountDetails(tktNumber);
            pickupamtForm.ShowDialog();
        }
        /*__________________________________________________________________________________________*/
        private void PS_AddMoreTicketsButton_Click(object sender, EventArgs e)
        {
            //Madhu BZ # 147 - passing the current object to child window.
            //TODO - revisit 
            //AddTickets myForm = new AddTickets(this);
            //myForm.AddPawnLoans += myForm_AddPawnLoans;
            //myForm.ShowDialog(this);
        }
        /*__________________________________________________________________________________________*/
        private void PS_CheckoutButton_Click(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Count > 0)
            {
                LoanCheckout();
            }
            else if (GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.Count > 0)
            {
                GlobalDataAccessor.Instance.DesktopSession.CompleteLayaway = false;
                LayawayCheckout();
            }
        }
        /*__________________________________________________________________________________________*/
        private void PS_ExtendButton_Click(object sender, EventArgs e)
        {
            /*
            List<PawnLoan> selectedLoans = _SelectedLoans;
            if (selectedLoans.Count <= 0)
            {
                MessageBox.Show("No loans to process for Extend");
                return;
            }
            if (selectedLoans.Count > 0)
            {
                //Check to see that all the selected loans are eligible for the
                //selected service
                if (!validateSelectedLoans(ServiceTypes.EXTEND))
                {
                    UpdateTicketSelections();
                    return;
                }
                //Disable all tabs except products and services tab
                callLockProductsTab();

                selectedLoans = _SelectedLoans;
                string strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                bool overrideCheck = false;
                var overrideFailedMessage = string.Empty;

                //Check overrides
                overrideCheck = ServiceLoanProcedures.CheckForOverrides(ServiceTypes.EXTEND, GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber,
                                                                        ref selectedLoans, ref overrideFailedMessage);
                if (!overrideCheck)
                {
                    var infoFrm = new InfoDialog
                                  {
                                      MessageToShow = overrideFailedMessage
                                  };
                    infoFrm.ShowDialog();
                }
                //After the override check if there are still selected loans to process then proceed
                if (!(selectedLoans.Count > 0))
                    return;
                ServiceLoanProcedures.CheckCurrentTempStatus(ref selectedLoans, strUserId, ServiceTypes.EXTEND);

                //First check what the extension term is for the state
                var extensionTerm = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetStateExtensionTerm(currentStoreSiteId);

                
                if (selectedLoans.Count > 0)
                {
                    //Set the temp status of the selected loans to E since by
                    //this time the DB has been updated with temp status E
                    foreach (var t in selectedLoans)
                    {
                        t.TempStatus = StateStatus.E;
                    }
                    
                    GlobalDataAccessor.Instance.DesktopSession.ExtensionLoans = Utilities.CloneObject(selectedLoans);
                    var dailyExtnForm = new ExtendPawnLoan();
                    
                    if (extensionTerm.Equals(ExtensionTerms.MONTHLY.ToString()))
                    {
                        dailyExtnForm.ExtensionType = ExtensionTerms.MONTHLY;
                    }
                    else
                    {
                        dailyExtnForm.ExtensionType = ExtensionTerms.DAILY;
                    }
                    dailyExtnForm.ShowDialog();
                    if (dailyExtnForm.LoansExtended)
                    {
                        _totalExtensionAmount = 0;
                        var extendedLoans = GlobalDataAccessor.Instance.DesktopSession.ExtensionLoans;
                        for (int i = 0; i < extendedLoans.Count; i++)
                        {
                            ShowStatusValue(extendedLoans[i]);
                            _totalExtensionAmount += extendedLoans[i].ExtensionAmount;
                            //Update the service indicator to indicate Extended
                            UpdateServiceIndicator(extendedLoans[i].TicketNumber,
                                                   ServiceIndicators.Extend.ToString());
                        }
                        //Set the service loans in session
                        GlobalDataAccessor.Instance.DesktopSession.ServiceLoans = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Union(extendedLoans).ToList();
                        //For the loans that were skipped during the extend process
                        //undo temp status should be called
                        UndoSkippedLoans(selectedLoans);
                        DisableAllServiceButtons();
                        UpdateTicketSelections();
                        if (_totalExtensionAmount > 0)
                        {
                            ServiceAmount += _totalExtensionAmount;
                            GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount = ServiceAmount;
                            GlobalDataAccessor.Instance.DesktopSession.TotalExtendAmount = _totalExtensionAmount;
                        }
                    }
                    else
                    {
                        UndoPawnTransactions(selectedLoans);
                    }
                }
                else
                {
                    UpdateTicketSelections();
                }
            } */
        }
        /// <summary>
        /// SR 8/24/09 - Item Pickup Functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*__________________________________________________________________________________________*/
        private void PS_PickUpButton_Click(object sender, EventArgs e)
        {
            _gunItemIdValidated = false;
            _selectedLoanNumbers = new List<int>();
            _gunItem = false;
            var selectedLoans = _SelectedLoans;

            if (selectedLoans.Count <= 0)
            {
                MessageBox.Show("No loans to process for Pickup");
                return;
            }
            if (selectedLoans.Count > 1)
            {
                MessageBox.Show("Cannot process more than 1 loan at a time for pickup");
                return;
            }

            //Check to see that all the selected loans are eligible for the service selected
            if (!validateSelectedLoans(ServiceTypes.PICKUP))
            {
                UpdateTicketSelections();
                return;
            }

            //Disable all tabs except
            //products and services tab
            callLockProductsTab();

            //If pickup is a gun, go to manage pawn app
            GlobalDataAccessor.Instance.DesktopSession.PickupLoans = selectedLoans;
            if (_gunItem && !_gunItemIdValidated)
            {
                _gunItemIdValidated = true;
                //This session data will be used by manager pawn application
                //in case a manager override is needed for out of state ID
                GlobalDataAccessor.Instance.DesktopSession.OverrideTransactionNumbers = overrideTransactionNumbers;
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "ManagePawnApplication";
                this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
            }
            else
            {
                //Go on to update temp status and complete pickup
                ContinuePawnPickupProcess();
            }
        }
        #endregion
        #region OBSOLETE EVENTS
        /*__________________________________________________________________________________________*/
        private void PS_ReceiptNoValue_Click(object sender, EventArgs e)
        {
            int ticketNumber = 0;
            if (this._currentTicketNumber == 0)
                ticketNumber = GetFirstRowTicketNumber();
            else
                ticketNumber = this._currentTicketNumber;
            string errString = string.Empty;

            //If a legit ticket number was pulled, then continue.
            if (ticketNumber > 0)
            {
                //Instantiate docinfo which will return info we need to be able to 
                //call reprint ticket.
                CouchDbUtils.PawnDocInfo docInfo = new CouchDbUtils.PawnDocInfo();
                docInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.RECEIPT);
                docInfo.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                docInfo.TicketNumber = ticketNumber;
                int receiptNumber = 0;
                if (!string.IsNullOrEmpty(PS_ReceiptNoValue.Text))
                    receiptNumber = Convert.ToInt32(PS_ReceiptNoValue.Text);
                docInfo.ReceiptNumber = receiptNumber;

                //Use couch DB to get the document.
                List<CouchDbUtils.PawnDocInfo> pawnDocInfos;
                if (CouchDbUtils.GetPawnDocument(GlobalDataAccessor.Instance.OracleDA, GlobalDataAccessor.Instance.CouchDBConnector,
                                                 docInfo, false, out pawnDocInfos, out errString))
                {
                    if (pawnDocInfos != null)
                    {
                        //Find that there is a document with a receipt.
                        var results = from p in pawnDocInfos
                                      where p.DocumentType ==
                                            Document.DocTypeNames.RECEIPT
                                      select p;
                        if (results.Any())
                        {
                            //Get the only one receipt that should exist.
                            docInfo = results.First();

                            //Call the reprint screen
                            ViewPrintDocument docViewPrint = new ViewPrintDocument("Receipt# ", docInfo.ReceiptNumber.ToString(), docInfo.StorageId, docInfo.DocumentType, docInfo);
                            docViewPrint.ShowDialog();
                        }
                    }
                }
                else if (this._currReceiptLookupInfo != null)
                {
                    ViewPrintDocument docViewPrint = new ViewPrintDocument("Receipt# ", docInfo.ReceiptNumber.ToString(), this._currReceiptLookupInfo.StorageID, this._currReceiptLookupInfo.DocumentType, docInfo);
                    docViewPrint.ShowDialog();
                }
            }
        }
        /*__________________________________________________________________________________________*/
        private void PS_ReceiptNoValue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //CallViewReceipt(PS_ReceiptNoValue.Text);
        }
        /*__________________________________________________________________________________________*/
        private void PS_RollOverButton_Click(object sender, EventArgs e)
        {
            /*
            var selectedLoans = _SelectedLoans;
            _totalRenewalAmount = 0;
            _totalPaydownAmount = 0;

            if (selectedLoans.Count <= 0)
            {
                MessageBox.Show("No loans to process for Rollover");
                return;
            }
            if (selectedLoans.Count > 1)
            {
                MessageBox.Show("Cannot process more than 1 loan at a time for pickup");
                return;
            }

            if (!validateSelectedLoans(ServiceTypes.ROLLOVER))
            {
                MessageBox.Show("No loans to process for Rollover");
                return;
            }

            callLockProductsTab();
            selectedLoans = _SelectedLoans;

            bool overrideCheck = false;
            var overrideFailedMessage = string.Empty;

            //Check overrides
            overrideCheck = ServiceLoanProcedures.CheckForOverrides(ServiceTypes.ROLLOVER,
                                                                    GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber,
                                                                    ref selectedLoans,
                                                                    ref overrideFailedMessage);

            if (!overrideCheck)
                MessageBox.Show(overrideFailedMessage);

            //After the override check if there are still selected loans to process then proceed
            if (!(selectedLoans.Count > 0))
                return;

            //Update temp status on selected loans
            foreach (PawnLoan p in selectedLoans)
            {
                if (p == null) continue;

                p.TempStatus = StateStatus.RO;
            }

            //Check current temp status on selected loans and update
            bool retVal = ServiceLoanProcedures.CheckCurrentTempStatus(ref selectedLoans,
                                                                       GlobalDataAccessor.Instance.DesktopSession.UserName,
                                                                       ServiceTypes.ROLLOVER);

            //If temp check succeeded, create rollover loans container 
            //Cannot determine at this point which loans are paydown 
            //and which loans are renewal...wait for RolloverLoan form
            if (!retVal ||
                selectedLoans.Count <= 0)
            {
                MessageBox.Show("No loans to process for Rollover");
                return;
            }
            GlobalDataAccessor.Instance.DesktopSession.RolloverLoans = Utilities.CloneObject(selectedLoans);

            //Create and show rollover processing form
            decimal minAmt, maxAmt;
            new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMinMaxLoanAmounts(
                GlobalDataAccessor.Instance.CurrentSiteId, out minAmt, out maxAmt);
            var rolloverForm = new RolloverLoan();
            rolloverForm.MinLoanAmt = minAmt;
            rolloverForm.MaxLoanAmt = maxAmt;
            rolloverForm.ShowDialog();

            if (!rolloverForm.RolloverSuccess)
            {
                MessageBox.Show("No loans to process for Rollover");
                UndoPawnTransactions(selectedLoans);
                return;
            }
            //Rollover form successful, split rollover loans into
            //their respective paydown and renewal containers
            if (rolloverForm.RolloverPawnLoans.Count <= 0)
            {
                MessageBox.Show("No loans to process for Rollover");
                return;
            }
            //Process any loans that were skipped
            if (rolloverForm.SkippedPawnLoans.Count > 0)
            {
                UndoPawnTransactions(rolloverForm.SkippedPawnLoans);
            }

            //NOTE:Must cache count to ensure idx does not exceed bounds
            //Loop variable i will demarcate an operation, not the
            //position in the rollover loans list
            int idx = 0, cnt = rolloverForm.RolloverPawnLoans.Count;

            //Merge rollover pawn loans into service loans list
            for (int i = 0; i < cnt; ++i)
            {
                PawnLoan p = rolloverForm.RolloverPawnLoans[idx];
                ShowStatusValue(p);
                if (p.TempStatus == StateStatus.RN)
                {
                    //Add to total renewal amount
                    _totalRenewalAmount += p.RenewalAmount;

                    //Update service indicator for renewal
                    UpdateServiceIndicator(
                        p.TicketNumber, ServiceIndicators.Renew.ToString());

                    //Ensure that the renewal loans container is valid
                    if (CollectionUtilities.isEmpty(GlobalDataAccessor.Instance.DesktopSession.RenewalLoans))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.RenewalLoans =
                        new List<PawnLoan>(selectedLoans.Count);
                    }
                    //Add loan to renewal list
                    GlobalDataAccessor.Instance.DesktopSession.RenewalLoans.Add(p);
                }
                else if (p.TempStatus == StateStatus.PD)
                {
                    //Add to total paydown amount
                    _totalPaydownAmount += p.PaydownAmount;

                    //Update service indicator for paydown
                    UpdateServiceIndicator(
                        p.TicketNumber, ServiceIndicators.Paydown.ToString());

                    //Ensure that the renewal loans container is valid
                    if (CollectionUtilities.isEmpty(GlobalDataAccessor.Instance.DesktopSession.PaydownLoans))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.PaydownLoans =
                        new List<PawnLoan>(selectedLoans.Count);
                    }

                    //Add loan to paydown list
                    GlobalDataAccessor.Instance.DesktopSession.PaydownLoans.Add(p);
                }

                //Increment rollover loan index
                ++idx;
            }
            //Set all rollover loans as service loans in session
            GlobalDataAccessor.Instance.DesktopSession.ServiceLoans =
            GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Union(
                rolloverForm.RolloverPawnLoans).ToList();

            //Perform UI updates
            DisableAllServiceButtons();
            UpdateTicketSelections();

            //Compute service amount for paydown
            if (_totalPaydownAmount != 0)
            {
                ServiceAmount += _totalPaydownAmount;
                //Update total paydown amount
                GlobalDataAccessor.Instance.DesktopSession.TotalPaydownAmount =
                _totalPaydownAmount;
            }

            //Compute service amount for renewal
            if (_totalRenewalAmount != 0)
            {
                ServiceAmount += _totalRenewalAmount;
                //Update total renewal amount
                GlobalDataAccessor.Instance.DesktopSession.TotalRenewalAmount =
                _totalRenewalAmount;
            } */
        }

        /*__________________________________________________________________________________________*/
        private void PS_UnDoButton_Click(object sender, EventArgs e)
        {
            if (GetSelectedProductType() == SupportProductType.LAYAWAY)
            {
                if (_SelectedLayaways.Count == 0)
                {
                    MessageBox.Show("No Layaways selected for Undo");
                    return;
                }

                List<LayawayVO> selectedServiceLayaways = new List<LayawayVO>();
                foreach (LayawayVO layaway in _SelectedLayaways)
                {
                    //Is this loan in the service loans then process it
                    int iDx = GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.FindIndex(lw => lw.TicketNumber == layaway.TicketNumber);

                    if (iDx >= 0)
                    {
                        selectedServiceLayaways.Add(GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways[iDx]);
                    }
                }
                if (selectedServiceLayaways.Count > 0)
                {
                    UndoLayawayTransactions(selectedServiceLayaways);
                    UpdateButtonsStates(true);
                }
                else
                {
                    MessageBox.Show("Layaway is not eligible for undo operation");
                    return;
                }
            }
            else
            {
                if (_SelectedLoans.Count == 0)
                {
                    MessageBox.Show("No Loans selected for Undo");
                    return;
                }

                List<PawnLoan> selectedServiceLoans = new List<PawnLoan>();
                foreach (PawnLoan loan in _SelectedLoans)
                {
                    //Is this loan in the service loans then process it
                    int iDx = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.FindIndex(pl => pl.TicketNumber == loan.TicketNumber);

                    if (iDx >= 0)
                    {
                        selectedServiceLoans.Add(GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[iDx]);
                    }
                }
                if (selectedServiceLoans.Count > 0)
                {
                    UndoPawnTransactions(selectedServiceLoans);
                    UpdateTicketSelections();
                    UpdateButtonsStates(false);
                }
                else
                {
                    MessageBox.Show("Loan is not eligible for undo operation");
                    return;
                }
            }
        }
        /*__________________________________________________________________________________________*/
        private void PS_ViewNewLoanDetailsButton_Click(object sender, EventArgs e)
        {
            List<PawnLoan> selectedLoans = _SelectedLoans;
            if (selectedLoans.Count == 0)
            {
                MessageBox.Show("No Loans selected for service");
                return;
            }
            foreach (PawnLoan currLoan in selectedLoans)
            {
                ViewNewLoanDetails myForm = new ViewNewLoanDetails(currLoan.TicketNumber);
                myForm.ShowDialog();
            }
        }
        /*__________________________________________________________________________________________*/
        private void PS_WaiveProrate_Click(object sender, EventArgs e)
        {
            /*WaiveProrateFees waivefees = new WaiveProrateFees();
            waivefees.ShowDialog();

            ServiceAmount -= _totalPickupAmount;
            _totalPickupAmount = 0;
            //To do: subtract the renew or paydown amounts
            for (int i = 0; i < GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Count; i++)
            {
                PawnLoan pawnLoan = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[i];
                if (pawnLoan.TempStatus == StateStatus.P)
                {
                    _totalPickupAmount += pawnLoan.PickupAmount;
                }
                //To do: Add logic for renew/paydown loans
            }
            ServiceAmount += _totalPickupAmount;
            UpdateTicketSelections();
            GlobalDataAccessor.Instance.DesktopSession.TotalPickupAmount = _totalPickupAmount;
            //Add logic for rollover and paydown amounts
             */
        }
        /*__________________________________________________________________________________________*/
        private void LW_LayawayPaymentButton_Click(object sender, EventArgs e)
        {
            /*
            if (_SelectedLayaways.Count == 0)
            {
                return;
            }

            List<LayawayVO> unservicedLayaways = (from selectedLayaway in _SelectedLayaways
                                                  where !IsLayawayBeingServicedOrAlreadyBeenServiced(selectedLayaway)
                                                  select selectedLayaway).ToList();
            LayawayPaymentValues layawayPaymentValues = new LayawayPaymentValues(unservicedLayaways);
            if (layawayPaymentValues.ShowDialog() == DialogResult.OK)
            {
                ServiceAmount += layawayPaymentValues.LayawayServiceAmount;

                // get the service layaways that were just serviced with at least one payment
                List<LayawayVO> servicedLayaways = (from serviceLayaway in GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways
                                                    where unservicedLayaways.Any(selectedLayaway => selectedLayaway.TicketNumber == serviceLayaway.TicketNumber)
                                                          && serviceLayaway.Payments.Count > 0
                                                    select serviceLayaway).ToList();

                foreach (LayawayVO layaway in servicedLayaways)
                {
                    LayawayPaymentHistoryBuilder paymentBuilder = new LayawayPaymentHistoryBuilder();
                    paymentBuilder.Layaway = layaway;
                    paymentBuilder.AddTemporaryReceipt(layaway.Payments[0].Amount, ReceiptEventTypes.LAYPMT, DateTime.MaxValue);
                    paymentBuilder.Calculate();
                    LayawayHistory nextPayment = paymentBuilder.GetFirstUnpaidPayment();

                    if (nextPayment != null)
                    {
                        layaway.NextDueAmount = nextPayment.GetRemainingBalance();
                        layaway.NextPayment = nextPayment.PaymentDueDate;
                    }

                    if (paymentBuilder.IsLayawayPaidOff())
                    {
                        UpdateServiceIndicator(layaway.TicketNumber, ServiceIndicators.Pickup.ToString());
                        layaway.LoanStatus = ProductStatus.PU;
                    }
                    else
                    {
                        UpdateServiceIndicator(layaway.TicketNumber, ServiceIndicators.Payment.ToString());
                    }

                    UpdateActiveLayawayInformation(layaway.TicketNumber);
                }
            }

            UpdateButtonsStates(true);
            */
        }
        /*__________________________________________________________________________________________*/
        private void LW_LayawayTerminateButton_Click(object sender, EventArgs e)
        {
            /*
            if (_SelectedLayaways.Count != 1)
            {
                return;
            }
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            LayawayVO layaway = cds.Layaways.Find(l => l.TicketNumber == _SelectedLayaways[0].TicketNumber);

            if (IsLayawayBeingServicedOrAlreadyBeenServiced(_SelectedLayaways[0]))
            {
                return;
            }

            if (MessageBox.Show("Are you sure you want to terminate this layaway?", "Confirm Termination", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            string statusDate = ShopDateTime.Instance.ShopDate.FormatDate();
            string statusTime = ShopDateTime.Instance.ShopTransactionTime.ToString();

            string errorCode;
            string errorText;
            decimal restockingFee = 0.0m;
            if (RetailProcedures.ProcessLayawayServices(
                cds,
                _SelectedLayaways.Take(1).ToList(),
                cds.CurrentSiteId.StoreNumber,
                statusDate,
                statusTime,
                ProductStatus.TERM,
                out restockingFee,
                out errorCode,
                out errorText))
            {
                UpdateServiceIndicator(_SelectedLayaways[0].TicketNumber, ServiceIndicators.Terminate.ToString());
                _SelectedLayaways[0].LoanStatus = ProductStatus.TERM;
                layaway.LoanStatus = ProductStatus.TERM;
                LayawayCreateReportObject lcrpt = new LayawayCreateReportObject();
                lcrpt.GetLayawayTerminationPickingSlip(_SelectedLayaways, restockingFee);
                lcrpt.GetLayawayTerminatedListings(_SelectedLayaways, restockingFee);
                DisableAllServiceButtons();
                UpdateTicketSelections();

                MessageBox.Show("Layaway Termination Successful.");
                //here print slips for each terminated layaway
            }
            else
            {
                MessageBox.Show("An error occurred while terminating the layaways.");
            } */
        }
        /*__________________________________________________________________________________________*/
        private void LW_PaymentScheduleHistory_Click(object sender, EventArgs e)
        {
            if (_SelectedLayaways.Count == 0)
            {
                return;
            }

            LayawayVO layaway = _SelectedLayaways[0];
            LayawayPaymentHistory layawayPaymentHistory = new LayawayPaymentHistory(layaway);
            layawayPaymentHistory.ShowDialog();
        }
        /*__________________________________________________________________________________________*/
        private void myForm_AddPawnLoans(List<PawnLoan> addedPawnLoans)
        {
            GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.AddRange(addedPawnLoans);
            LoadAdditionalTicketsData();
        }
        //Madhu BZ # 147 - duplicate check - this will be called from AddTickets.cs to
        //check duplicate ticket numbers.
        //Checks whether the pawnloan is set for service in this session
        //Handle the click event on the document icon
        //For all the skipped transactions call the database to undo the temp status update
        //Process the escape key only if this form is shown in the scenario
        //where the ticket holder is not the same as the pledgor
        //since only at that time the customer tab will be disabled.
        //The ctrl key is also processed to indicate that the user pressed the ctrl key
        //to enable multiple selection of loans
        //Uncheck the transactions
        //Called when a service is completed
        /*__________________________________________________________________________________________*/
        private void BuyoutButton_Click(object sender, EventArgs e)
        {
 /*           if (_SelectedLoans.Count > 1)
            {
                MessageBox.Show("You can only set one Buy out at a time.");
                return;
            }
            FailedBackGroundCheckForm failedBackGroundCheckForm = new FailedBackGroundCheckForm(_SelectedLoans);
            if (failedBackGroundCheckForm.ShowDialog() == DialogResult.OK)
            {
                ContinueBuyoutProcess();
            }
  */ 
        }
        /*__________________________________________________________________________________________*/
        private void PS_PartPmntButton_Click(object sender, EventArgs e)
        {
            /*
            List<PawnLoan> selectedLoans = _SelectedLoans;
            if (selectedLoans.Count <= 0)
            {
                MessageBox.Show("No loans to process for Partial Payment");
                return;
            }
            if (selectedLoans.Count > 0)
            {
                //Check to see that all the selected loans are eligible for the
                //selected service
                if (!validateSelectedLoans(ServiceTypes.PARTIALPAYMENT))
                {
                    UpdateTicketSelections();
                    return;
                }
                //Disable all tabs except products and services tab
                callLockProductsTab();

                selectedLoans = _SelectedLoans;
                string strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                bool overrideCheck = false;
                var overrideFailedMessage = string.Empty;

                //Check overrides
                overrideCheck = ServiceLoanProcedures.CheckForOverrides(ServiceTypes.PARTIALPAYMENT, GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber,
                                                                        ref selectedLoans, ref overrideFailedMessage);
                if (!overrideCheck)
                {
                    InfoDialog infoFrm = new InfoDialog();
                    infoFrm.MessageToShow = overrideFailedMessage;
                    infoFrm.ShowDialog();
                }
                //After the override check if there are still selected loans to process then proceed
                if (!(selectedLoans.Count > 0))
                    return;
                ServiceLoanProcedures.CheckCurrentTempStatus(ref selectedLoans, strUserId, ServiceTypes.PARTIALPAYMENT);

                if (selectedLoans.Count > 0)
                {
                    //Set the temp status of the selected loans to PPMNT since by
                    //this time the DB has been updated with temp status
                    for (int i = 0; i < selectedLoans.Count; i++)
                    {
                        selectedLoans[i].TempStatus = StateStatus.PPMNT;
                    }

                    GlobalDataAccessor.Instance.DesktopSession.PartialPaymentLoans = Utilities.CloneObject(selectedLoans);
                    PartialPayment partPaymentForm = new PartialPayment();
                    partPaymentForm.ShowDialog();
                    if (partPaymentForm.DialogResult != DialogResult.Cancel)
                    {
                        foreach (PawnLoan t in selectedLoans)
                        {
                            //Update the service indicator to indicate partial payment
                            UpdateServiceIndicator(t.TicketNumber, ServiceIndicators.LoanPmt.ToString());

                        }


                        //Set all partial payment loans as service loans in session
                        GlobalDataAccessor.Instance.DesktopSession.ServiceLoans =
                        GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.Union(
                            GlobalDataAccessor.Instance.DesktopSession.PartialPaymentLoans).ToList();

                        //Perform UI updates
                        DisableAllServiceButtons();
                        UpdateTicketSelections();
                        decimal totalPartialPaymentAmount = GlobalDataAccessor.Instance.DesktopSession.PartialPaymentLoans.Sum(ploan => ploan.PartialPayments.Find(p => p.Status_cde == "New").PMT_AMOUNT);
                        ServiceAmount += totalPartialPaymentAmount;
                    }

                }
            } */

        }
        /*__________________________________________________________________________________________*/
        void pic_Click(object sender, EventArgs e)
        {
            int ticketNumber = this._currentTicketNumber;

            //If a legit ticket number was pulled, then continue.
            if (ticketNumber > 0)
            {
                //Instantiate docinfo which will return info we need to be able to 
                //call reprint ticket.
                CouchDbUtils.PawnDocInfo docInfo = new CouchDbUtils.PawnDocInfo();
                docInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.RECEIPT);
                docInfo.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                docInfo.TicketNumber = ticketNumber;
                int receiptNumber = 0;
                if (!string.IsNullOrEmpty(PS_ReceiptNoValue.Text))
                    receiptNumber = Convert.ToInt32(PS_ReceiptNoValue.Text);
                docInfo.ReceiptNumber = receiptNumber;
                try
                {
                    string storageId = ((PictureBox)sender).Name.ToString();
                    //ReprintDocument docViewPrint = new ReprintDocument(documentName, storageId, docType);
                    ViewPrintDocument docViewPrint = new ViewPrintDocument("Receipt# ", docInfo.ReceiptNumber.ToString(), storageId, docInfo.DocumentType, docInfo);
                    docViewPrint.ShowDialog();
                }
                catch (Exception)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not load the customer document");
                }
            }
        }

        private void CMBLoanStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadData(GetSelectedProductType());
        }
        #endregion

        #endregion
        /// <summary>
        /// Used to track a recipts lookup info for couch DB.
        /// </summary>
        /*__________________________________________________________________________________________*/
        public class ReceiptLookupInfo
        {
            public string DocumentName { get; set; }

            public Document.DocTypeNames DocumentType { get; set; }

            public string StorageID { get; set; }
        }
        /*__________________________________________________________________________________________*/
        private void ps_OtherDetails_LinkClicked(object sender, EventArgs e)
        {
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "PDLoanOtherDetails";
            this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }
        /*__________________________________________________________________________________________*/
        private void btnExtendDeposit_Click(object sender, EventArgs e)
        {
            string errorCode;
            string errorDesc;
            var depositDetails = new DepositDateExtensionDetails();
            bool returnVal = Support.Controllers.Database.Procedures.CustomerLoans.GetExtendDepositDateInfo(
                Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan.PDLLoanNumber,
                depositDetails, out errorCode, out errorDesc);

            if (returnVal)
            {
                Support.Logic.CashlinxPawnSupportSession.Instance.DepositDateExtensionDetailsObject = depositDetails;
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "ExtendedDepositDate";
                this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
            }
            else
            {
                if (errorCode.Equals("50"))
                {
                    MessageBox.Show(errorDesc, "Not Eligible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(errorDesc, "Database call failed.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ChkBGetAllHistory_CheckStateChanged(object sender, EventArgs e)
        {
            if (Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan != null &&
                Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan.PDLLoanNumber != null)
            {
                int iDx = Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys.FindIndex(
                    delegate(PDLoan p)
                    {
                        return p.PDLLoanNumber.Equals(Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan.PDLLoanNumber);
                    });
                PDLoan pdLoan = new PDLoan();
                if (iDx >= 0)
                    pdLoan = Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys[iDx];

                // logic to only get data if not already recieved.
                if (Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys[iDx].EventHistoryRetrieved)
                {
                    pdLoan = Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys[iDx];
                }
                else
                {
                    string errorCode;
                    string errorDesc;
                    if (Support.Controllers.Database.Procedures.CustomerLoans.GetPDLoanEventDetails(
                        pdLoan, out errorCode, out errorDesc))
                    {
                        Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys[iDx].EventHistoryRetrieved = true;
                        Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan = pdLoan;
                    }
                    else
                    {
                        MessageBox.Show(errorDesc, "Error while retreiving the data.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (this.ChkBGetAllHistory.Checked)
                {
                    MapPDL_HistoryFromProperties(pdLoan.GetPDLoanHistoryList);
                }
                else
                {
                    MapPDL_HistoryFromProperties(pdLoan.GetPDLoanHistorySummaryList);
                }
                this.CmbHistoryLoanEvents.SelectedIndex = 0;
                this.DGVHistoryLoanEvents.Refresh();
            }
        }
        private void CmbHistoryLoanEvents_SelectedValueChanged(object sender, EventArgs e)
        {
            string filterValue = checkNull(this.CmbHistoryLoanEvents.SelectedItem.ToString());

            if (!filterValue.Equals(string.Empty))
            {
                var historyList = new List<PDLoanHistoryList>();
                var tempLoanHistory = new List<PDLoanHistoryList>();
                if (this.ChkBGetAllHistory.Checked)
                {
                    tempLoanHistory = Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan.GetPDLoanHistoryList;
                    historyList = Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan.GetPDLoanHistoryList;
                }
                else
                {
                    tempLoanHistory = Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan.GetPDLoanHistorySummaryList;
                    historyList = Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan.GetPDLoanHistorySummaryList;
                }

                if (filterValue.Equals("POS"))
                {
                    historyList = new List<PDLoanHistoryList>();
                    historyList = tempLoanHistory.FindAll(plk => (plk.Source.ToUpperInvariant() == "POS"));
                }
                else if (filterValue.Equals("NIGHTLY"))
                {
                    historyList = new List<PDLoanHistoryList>();
                    historyList = tempLoanHistory.FindAll(plk => (plk.Source.ToUpperInvariant() == "NIGHTLY"));
                }

                MapPDL_HistoryFromProperties(historyList);
                this.DGVHistoryLoanEvents.Refresh();
            }
       
        }
    }
}
