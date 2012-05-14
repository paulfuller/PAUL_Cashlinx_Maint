/****************************************************************************
* Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products.ProductHistory
* Class:           Controller_ProductHistory
* 
* Description      Form used by Controller to View Customer Pawn Loans
* 
* History
* David D Wise, Initial Development
* SR 3/30/2010 Changed type to event for the receipt shown
*               Changed the filter from origination date to status date
* SR 6/13/2010 Added type description for receipts instead of type
* BZ# 1289 10/7/2011 Ewaltmon - Modified Date fields to include time stamps
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.Libraries.Utility.Type;
//using Pawn.Forms.Pawn.Customer.ItemRelease;
using Pawn.Logic.DesktopProcedures;
using Support.Logic;
using Support.Logic.DesktopProcedures;
using Document = Common.Libraries.Objects.Doc.Document;

namespace Support.Forms.Customer.Products.ProductHistory
{
    public partial class Controller_ProductHistory : Form
    {
        private int _SelectedRow;
        private int _MaxLoanKeysDisplayed = 10;
        private List<PawnLoan> _ProductHistoryLoanKeys;
        private int _ProductHistoryLoanKeysIndex;
        private string _TicketNumber = "";
        private object _selectedProduct = null;
        private List<PawnLoan> _LoanKeys;
        //Used to help determine what links not to hyperlink under
        //the event type hyperlink column.  
        private readonly string[] _receiptEventWithNoScreen =
        {
            ReceiptEventTypes.VEX.ToString(),
            ReceiptEventTypes.VNL.ToString(),
            ReceiptEventTypes.VPD.ToString(),
            ReceiptEventTypes.VPU.ToString(),
            ReceiptEventTypes.VRN.ToString(),
            ReceiptEventTypes.TO.ToString()
        };

        public DesktopSession CDS
        {
            get
            {
                return GlobalDataAccessor.Instance.DesktopSession;
            }
        }

        public NavBox NavControlBox;

        public Controller_ProductHistory()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
            Setup();
        }

        public Controller_ProductHistory(string sTicketNumber)
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
            _TicketNumber = sTicketNumber;

            if (string.IsNullOrEmpty(_TicketNumber))
            {
                MessageBox.Show("No valid ticket number was passed in.", "Ticket Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Setup();
        }

        private void Setup()
        {
            this.NavControlBox.Owner = this;
            //TODO: This should be a rule!!!
            PH_FromDateTextBox.Text = ShopDateTime.Instance.ShopDate.AddDays(-90).ToShortDateString();
            PH_ToDateTextBox.Text = ShopDateTime.Instance.ShopDate.ToShortDateString();
            _ProductHistoryLoanKeys = new List<PawnLoan>();

            if (CDS.PH_TicketLookedUp > 0 && (CDS.PH_TicketTypeLookedUp == ProductType.PAWN || CDS.PH_TicketTypeLookedUp == ProductType.LAYAWAY))
            {
                PawnLoan loan = CDS.PawnLoanKeys.Find(pl => pl.TicketNumber == CDS.PH_TicketLookedUp);
                //TODO: This should be a rule!!!
                if (loan != null && loan.StatusDate < ShopDateTime.Instance.ShopDate.AddDays(-90))
                {
                    PH_FromDateTextBox.Text = loan.StatusDate.Date.ToShortDateString() + " " + loan.StatusDate.Date.ToShortTimeString();
                    //TODO: This should be a rule!!!
                    PH_ToDateTextBox.Text = loan.StatusDate.Date.AddDays(90).ToShortDateString();
                }

                switch (CDS.PH_TicketTypeLookedUp)
                {
                    case ProductType.PAWN:
                        PH_ShowComboBox.SelectedItem = "Pawn";
                        break;
                    case ProductType.LAYAWAY:
                        PH_ShowComboBox.SelectedItem = "Layaway";
                        break;
                }

                LoadPresetData();
                int index = _ProductHistoryLoanKeys.FindIndex(ph => ph.TicketNumber == CDS.PH_TicketLookedUp);
                if (index >= 0)
                {
                    _ProductHistoryLoanKeysIndex = index;
                    LoadProductHistoryLoanData(index);
                    PH_TicketsDataGridView.CurrentCell = PH_TicketsDataGridView.Rows[index].Cells[0];
                    PH_TicketsDataGridView.Rows[index].Selected = true;
                }
            }
            else
            {
                if (LoadPresetData())
                    PH_ShowComboBox.SelectedIndex = 0;
            }

            if (CDS.ShowOnlyHistoryTabs)
                this.customButtonExit.Text = "Exit";
            else
                this.customButtonExit.Text = "Cancel";
        }

        private bool LoadPresetData()
        {
            bool retval = true;

            PH_PawnLoanLabel.Visible = false;
            PH_ItemDescriptionDataGridView.Visible = false;
            PH_LoanStatsLayoutPanel.Visible = false;
            PH_ReceiptsDataGridView.Visible = false;

            PH_TicketsDataGridView.Rows.Clear();
            PH_ItemDescriptionDataGridView.Rows.Clear();
            PH_ReceiptsDataGridView.Rows.Clear();

            // Populate _LoanKeys
            _LoanKeys = GlobalDataAccessor.Instance.DesktopSession.PawnLoanKeys;
            _ProductHistoryLoanKeys.Clear();
            // End Populate _LoanKeys

            //SR 3/30/2010 Changed the filter to look at updated date instead of origination date
            //since the form is supposed to show all loans that had some transaction done
            //in the period. 

            int filter = 0;
            if (PH_ShowComboBox.SelectedItem != null)
                switch (PH_ShowComboBox.SelectedItem.ToString())
                {
                    case "Pawn":
                        filter = 1;
                        break;
                    case "Buy":
                        filter = 2;
                        break;
                    case "Sale":
                        filter = 3;
                        break;
                    case "Layaway":
                        filter = 4;
                        break;
                    case "Refund":
                        filter = -1;
                        break;
                }

            List<PawnLoan> foundKeys = _LoanKeys.FindAll(ky =>
                                                         ky.StatusDate >= Convert.ToDateTime(PH_FromDateTextBox.Text) &&
                                                         ky.StatusDate < Convert.ToDateTime(PH_ToDateTextBox.Text).AddDays(1) &&
                                                         ky.DocType == ((filter > 0) ? filter : ky.DocType) &&
                                                         ky.LoanStatus.ToString().Equals(((filter == -1) ? "REF" : ky.LoanStatus.ToString())));

            if (foundKeys.Count > 0)
            {
                for (int i = 0; i < foundKeys.Count; i++)
                {
                    if (_ProductHistoryLoanKeys.FindIndex(ph => ph.TicketNumber == foundKeys[i].TicketNumber) < 0)
                        _ProductHistoryLoanKeys.Add(foundKeys[i]);
                }

                _ProductHistoryLoanKeys.Sort(delegate(PawnLoan pl, PawnLoan pl2)
                                             {
                                                 return pl2.StatusDate.CompareTo(pl.StatusDate);
                                             });

                _ProductHistoryLoanKeysIndex = 0;
                LoadProductHistoryLoanData();
            }
            else
            {
                MessageBox.Show("No loans were found for the specified data range.", "Search Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                retval = false;
            }

            return retval;
        }

        private void PH_SearchButton_Click(object sender, EventArgs e)
        {
            string sStartDate = PH_FromDateTextBox.Text;
            string sEndDate = PH_ToDateTextBox.Text;

            if (!StringUtilities.IsDate(sStartDate))
            {
                MessageBox.Show("The From Date is invalid.", "Date Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!StringUtilities.IsDate(sEndDate))
            {
                MessageBox.Show("The To Date is invalid.", "Date Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadPresetData();
        }

        private void showDetails()
        {
            PawnLoan pawnLoanKey;

            pawnLoanKey = _ProductHistoryLoanKeys[_SelectedRow];

            _selectedProduct = GetSelectedProduct(pawnLoanKey);

            if (_selectedProduct != null)
            {
                CustomerProductDataVO data = (CustomerProductDataVO)_selectedProduct;
                int nrItems = data.Items.Count;

                if (_selectedProduct is SaleVO)
                {
                    nrItems = ((SaleVO)_selectedProduct).RetailItems.Count;
                }

                if (PH_ItemDescriptionDataGridView.Rows.Count < nrItems)
                {
                    for (int i = 0; i < nrItems; i++)
                    {
                        int gvIdx = PH_ItemDescriptionDataGridView.Rows.Add();
                        DataGridViewRow myRow = PH_ItemDescriptionDataGridView.Rows[gvIdx];

                        Item item;
                        if (_selectedProduct is SaleVO)
                        {
                            item = ((SaleVO)_selectedProduct).RetailItems[i];
                        }
                        else
                        {
                            item = data.Items[i];
                        }

                        PairType<ProductStatus, string> pairType = GlobalDataAccessor.Instance.DesktopSession.LoanStatus.FirstOrDefault(
                            p => p.Left == item.ItemStatus);

                        myRow.Cells["PH_Description_ItemStatusColumn"].Value = (pairType != null) ? pairType.Right.ToString() : item.ItemStatus.ToString();
                        myRow.Cells["PH_Description_TicketDescriptionColumn"].Value = item.TicketDescription;
                        myRow.Cells["PH_Description_ICN"].Value = item.Icn;
                        myRow.Cells["PH_Description_ItemAmountColumn"].Value = String.Format("{0:C}", item.ItemAmount);
                    }
                }

                if (_ProductHistoryLoanKeys[_SelectedRow].DocType == 2)
                {
                    PH_PawnLoanLabel.Text = "Purchase " +
                                            _ProductHistoryLoanKeys[_SelectedRow].TicketNumber;
                    PH_ReceiptsDataGridView.Columns["PH_Receipt_DueDateColumn"].Visible = false;
                }
                else if (_ProductHistoryLoanKeys[_SelectedRow].DocType == 3)
                {
                    PH_PawnLoanLabel.Text = "Sale " +
                                            _ProductHistoryLoanKeys[_SelectedRow].TicketNumber;
                    PH_ReceiptsDataGridView.Columns["PH_Receipt_DueDateColumn"].Visible = false;
                }
                else if (_ProductHistoryLoanKeys[_SelectedRow].DocType == 4)
                {
                    PH_PawnLoanLabel.Text = "Layaway " +
                                            _ProductHistoryLoanKeys[_SelectedRow].TicketNumber;
                }
                else
                {
                    PH_PawnLoanLabel.Text = "Pawn Loan " +
                                            _ProductHistoryLoanKeys[_SelectedRow].TicketNumber;
                    PH_ReceiptsDataGridView.Columns["PH_Receipt_DueDateColumn"].Visible = true;
                }

                if (_selectedProduct is PawnLoan)
                {
                    DateTime dateTimeTemp = Utilities.GetDateTimeValue(((PawnLoan)_selectedProduct).OriginationDate, DateTime.MinValue);
                    PH_OriginationDateValue.Text =
                        dateTimeTemp.ToShortDateString() + " " + dateTimeTemp.ToShortTimeString();
                }

                PH_OriginationShopValue.Text = Utilities.GetStringValue(data.OrgShopNumber, "").PadLeft(5, '0');
                PH_LoanAmountValue.Text = String.Format("{0:C}", Utilities.GetDecimalValue(data.Amount, 0.00M));

                if (data is LayawayVO)
                {
                    if (data.LoanStatus == ProductStatus.PAID)
                    {
                        PH_BalanceValue.Text = String.Format("{0:C}", 0);
                    }
                    else
                    {
                        PH_BalanceValue.Text = String.Format("{0:C}", Utilities.GetDecimalValue(data.Amount - ((LayawayVO)data).GetAmountPaid(), 0.00M));
                    }
                }

                PH_LastUpdatedByValue.Text = Utilities.GetStringValue(data.LastUpdatedBy, "");

                PH_PawnLoanLabel.Visible = true;
                PH_ItemDescriptionDataGridView.Visible = true;
                PH_LoanStatsLayoutPanel.Visible = true;

                if (data.Receipts != null)
                {
                    PH_ReceiptsDataGridView.Rows.Clear();

                    data.Receipts.Sort((x, y) =>
                                       {
                                           int xInt = int.Parse(x.ReceiptNumber);
                                           int yInt = int.Parse(y.ReceiptNumber);

                                           if (xInt == yInt)
                                               return 0;
                                           else if (xInt > yInt)
                                               return 1;
                                           else
                                               return -1;
                                       });

                    if (_selectedProduct is LayawayVO)
                    {
                        LayawayPaymentHistoryBuilder _builder = new LayawayPaymentHistoryBuilder((LayawayVO)_selectedProduct);

                        foreach (LayawayHistory lh in _builder.ScheduledPayments)
                        {
                            foreach (LayawayHistoryPaymentInfo pmt in lh.Payments)
                            {
                                int gvIdx = PH_ReceiptsDataGridView.Rows.Add();
                                DataGridViewRow myRow = PH_ReceiptsDataGridView.Rows[gvIdx];

                                myRow.Cells["PH_Receipt_ReceiptNumberColumn"].Value = pmt.ReceiptNumber;
                                myRow.Cells["PH_Receipt_TypeColumn"].Value = pmt.Receipt.TypeDescription;
                                myRow.Cells["PH_Receipt_DateColumn"].Value = pmt.Receipt.Date.ToShortDateString() + " " + pmt.PaymentMadeOn.ToShortTimeString();
                                myRow.Cells["PH_Receipt_AmountDueColumn"].Value = String.Format(
                                    "{0:C}", lh.PaymentAmountDue);
                                myRow.Cells["PH_Receipt_DueDateColumn"].Value = lh.PaymentDueDate.ToShortDateString();
                                myRow.Cells["PH_Receipt_AmountColumn"].Value = String.Format(
                                    "{0:C}", pmt.PaymentAmountMade);

                                myRow.Cells["PH_Receipt_detail_number"].Value = pmt.Receipt.ReceiptDetailNumber;

                                int offset = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetWaitDaysForLayawayForfeitureEligibility(GlobalDataAccessor.Instance.CurrentSiteId);

                                myRow.Cells["PH_Receipt_PfiDateColumn"].Value = "N/A";

                                if (lh.PaymentDueDate.ToShortDateString() != "1/1/0001" && data.LoanStatus != ProductStatus.PAID)
                                {
                                    myRow.Cells["PH_Receipt_PfiDateColumn"].Value = lh.PaymentDueDate.AddDays(offset).ToShortDateString();
                                }
                            }
                        }
                    }
                    else
                        foreach (Receipt receipt in data.Receipts)
                        {
                            int gvIdx = PH_ReceiptsDataGridView.Rows.Add();
                            DataGridViewRow myRow = PH_ReceiptsDataGridView.Rows[gvIdx];

                            myRow.Cells["PH_Receipt_ReceiptNumberColumn"].Value = receipt.ReceiptNumber;
                            myRow.Cells["PH_Receipt_DateColumn"].Value = receipt.Date.ToShortDateString();

                            // BZ#1289 - EDW - Show time portion data, where available.
                            if (_selectedProduct is SaleVO || 
                                _selectedProduct is PurchaseVO)
                            {
                                myRow.Cells["PH_Receipt_DateColumn"].Value = receipt.Date.ToShortDateString();
                            }
                            else if (_selectedProduct is CustomerProductDataVO)
                            {
                                myRow.Cells["PH_Receipt_DateColumn"].Value = receipt.Date.ToShortDateString() + " " + receipt.RefTime.ToShortTimeString();
                            }

                            if ((receipt.Event == ReceiptEventTypes.Renew.ToString() || receipt.Event == ReceiptEventTypes.Paydown.ToString())
                                && receipt.Amount == 0)
                            {
                                myRow.Cells["PH_Receipt_AmountColumn"].Value = String.Format(
                                    "{0:C}", pawnLoanKey.Amount);
                                myRow.Cells["PH_Receipt_TypeColumn"].Value = "New Loan";
                            }
                            else
                            {
                                myRow.Cells["PH_Receipt_AmountColumn"].Value = String.Format(
                                    "{0:C}", receipt.Amount);
                                myRow.Cells["PH_Receipt_TypeColumn"].Value = receipt.TypeDescription;
                            }

                            if (data is PawnLoan)
                            {
                                myRow.Cells["PH_Receipt_DueDateColumn"].Value = ((PawnLoan)data).DueDate.ToShortDateString();
                            }
                            //receipt.AuxillaryDate.ToShortDateString();
                            else
                            {
                                myRow.Cells["PH_Receipt_DueDateColumn"].Value = receipt.AuxillaryDate.ToShortDateString();
                            }

                            string expirDate = data.PfiEligible.ToShortDateString();

                            if (expirDate != "1/1/0001")
                            {
                                myRow.Cells["PH_Receipt_PfiDateColumn"].Value = expirDate;
                            }
                            else
                            {
                                myRow.Cells["PH_Receipt_PfiDateColumn"].Value = "N/A";
                            }

                            //UnderwritePawnLoanUtility.GetPfiDate(receipt.AuxillaryDate, siteID).ToShortDateString();
                            myRow.Cells["PH_Receipt_detail_number"].Value = receipt.ReceiptDetailNumber;

                            if (IsAnEventWithNoScreen(receipt.Event) || (receipt.Event == ReceiptEventTypes.PFI.ToString() && receipt.Type == "2"))
                            {
                                ((DataGridViewLinkCell)myRow.Cells["PH_Receipt_TypeColumn"]).
                                    LinkBehavior = LinkBehavior.NeverUnderline;
                                ((DataGridViewLinkCell)myRow.Cells["PH_Receipt_TypeColumn"]).
                                    LinkColor = Color.Black;
                                ((DataGridViewLinkCell)myRow.Cells["PH_Receipt_TypeColumn"]).
                                    ActiveLinkColor = Color.Black;
                                ((DataGridViewLinkCell)myRow.Cells["PH_Receipt_TypeColumn"]).
                                    VisitedLinkColor = Color.Black;
                            }
                        }
                    PH_ReceiptsDataGridView.Visible = true;
                }
            }
            else
            {
                //clear PH_ReceiptsDataGridView
                //clear PH_ItemDescriptionDataGridView
                PH_ReceiptsDataGridView.Rows.Clear();
                PH_ItemDescriptionDataGridView.Rows.Clear();

                if (_ProductHistoryLoanKeys[_SelectedRow].LoanStatus.ToString().ToUpper() == "PUR")
                {
                    PH_PawnLoanLabel.Text = "Purchase " +
                                            _ProductHistoryLoanKeys[_SelectedRow].TicketNumber;
                }
                else if (_ProductHistoryLoanKeys[_SelectedRow].LoanStatus.ToString().ToUpper() == "ACT")
                {
                    PH_PawnLoanLabel.Text = "Sale " +
                                            _ProductHistoryLoanKeys[_SelectedRow].TicketNumber;
                }
                else
                {
                    PH_PawnLoanLabel.Text = "Pawn Loan " +
                                            _ProductHistoryLoanKeys[_SelectedRow].TicketNumber;
                }
                return;
            }
        }

        private void PH_TicketsDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectTicketRow(e.RowIndex);
        }

        private void SelectTicketRow(int index)
        {
            if (index < 0)
            {
                return;
            }

            PH_ItemDescriptionDataGridView.Rows.Clear();
            _SelectedRow = index;

            showDetails();
        }

        private object GetSelectedProduct(PawnLoan pawnLoanKey)
        {
            PurchaseVO purchaseObj;
            CustomerVO customerObj;
            PawnAppVO pawnAppObj;
            string errorCode;
            string errorText;
            object product = null;

            bool isSale = pawnLoanKey.LoanStatus == ProductStatus.VO ||
                          pawnLoanKey.LoanStatus == ProductStatus.ACT ||
                          pawnLoanKey.LoanStatus == ProductStatus.REF;

            bool isPurchase = pawnLoanKey.LoanStatus == ProductStatus.PUR ||
                              pawnLoanKey.LoanStatus == ProductStatus.RET;
            //CALL FOR PURCHASES ITEM.
            if (pawnLoanKey.DocType == 2 || isPurchase)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.Purchases == null)
                {
                    GlobalDataAccessor.Instance.DesktopSession.Purchases = new List<PurchaseVO>();
                }

                int iDx = GlobalDataAccessor.Instance.DesktopSession.Purchases.FindIndex(
                    p1 => p1.TicketNumber == pawnLoanKey.TicketNumber);

                if (iDx >= 0)
                {
                    product = GlobalDataAccessor.Instance.DesktopSession.Purchases[iDx];
                }
                else
                {
                    string tenderType;
                    bool retValue =
                    PurchaseProcedures.GetPurchaseData(
                        Utilities.GetIntegerValue(pawnLoanKey.OrgShopNumber, 0),
                        //GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, 0),
                        pawnLoanKey.TicketNumber,
                        "2",
                        StateStatus.BLNK,
                        pawnLoanKey.LoanStatus.ToString(),
                        false,
                        out purchaseObj,
                        out customerObj,
                        out tenderType,
                        out errorCode,
                        out errorText);

                    if (purchaseObj != null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.Purchases.Add(purchaseObj);
                    }
                    product = purchaseObj;

                    if (!retValue || purchaseObj == null)
                    {
                        MessageBox.Show(@"Error retrieving purchases loan details");
                    }
                }
            }
            else if (pawnLoanKey.DocType == 4)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.Layaways == null)
                {
                    GlobalDataAccessor.Instance.DesktopSession.Layaways = new List<LayawayVO>();
                }

                //Get the sale data pertaining to the selected ticket number
                int iDx = GlobalDataAccessor.Instance.DesktopSession.Layaways.FindIndex(
                    pl => pl.TicketNumber == pawnLoanKey.TicketNumber);

                if (iDx >= 0)
                {
                    product = GlobalDataAccessor.Instance.DesktopSession.Layaways[iDx];
                }
                else
                {
                    LayawayVO layaway = new LayawayVO();
                    string tranType;

                    switch (pawnLoanKey.LoanStatus)
                    {
                        case ProductStatus.ACT:
                            tranType = "LAY";
                            break;
                        case ProductStatus.PAID:
                            tranType = "PAID";
                            break;
                        default:
                            tranType = "REF";
                            break;
                    }

                    bool retValue = RetailProcedures.GetLayawayData(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.OracleDA, Utilities.GetIntegerValue(
                                                                        pawnLoanKey.OrgShopNumber, 0),
                                                                    pawnLoanKey.TicketNumber, "4", StateStatus.BLNK, tranType, false, out layaway, out customerObj, out errorCode, out errorText);

                    if (retValue)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.Layaways.Add(layaway);
                        product = layaway;
                    }
                }
            }
            else if ((pawnLoanKey.DocType == 1 || pawnLoanKey.DocType == 3) && isSale)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.Sales == null)
                {
                    GlobalDataAccessor.Instance.DesktopSession.Sales = new List<SaleVO>();
                }

                //Get the sale data pertaining to the selected ticket number
                int iDx = GlobalDataAccessor.Instance.DesktopSession.Sales.FindIndex(
                    pl => pl.TicketNumber == pawnLoanKey.TicketNumber);

                if (iDx >= 0)
                {
                    product = GlobalDataAccessor.Instance.DesktopSession.Sales[iDx];
                }
                else
                {
                    SaleVO sale = new SaleVO();

                    string tranType;
                    switch (pawnLoanKey.LoanStatus)
                    {
                        case ProductStatus.ACT:
                            tranType = "SALE";
                            break;
                        case ProductStatus.VO:
                            tranType = "VO";
                            break;

                        default:
                            tranType = "REFUND";
                            break;
                    }

                    bool retValue = RetailProcedures.GetSaleData(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.OracleDA, Utilities.GetIntegerValue(
                                                                     pawnLoanKey.OrgShopNumber, 0),
                                                                 pawnLoanKey.TicketNumber, "3", StateStatus.BLNK, tranType, false, out sale, out customerObj, out errorCode, out errorText);

                    if (retValue)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.Sales.Add(sale);
                        product = sale;
                    }
                }
            }
            else
            {
                if (GlobalDataAccessor.Instance.DesktopSession.PawnLoans == null)
                {
                    GlobalDataAccessor.Instance.DesktopSession.PawnLoans = new List<PawnLoan>();
                }

                //CALL FOR PAWNLOAN
                int iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans.FindIndex(
                    delegate(PawnLoan pl)
                    {
                        return pl.TicketNumber == pawnLoanKey.TicketNumber;
                    });

                if (iDx >= 0)
                {
                    product = GlobalDataAccessor.Instance.DesktopSession.PawnLoans[iDx];
                }
                else
                {
                    PawnLoan loan = new PawnLoan();
                    if (CustomerLoans.GetPawnLoan(GlobalDataAccessor.Instance.DesktopSession,
                        Convert.ToInt32(pawnLoanKey.OrgShopNumber),
                        pawnLoanKey.TicketNumber,
                        "0",
                        StateStatus.BLNK,
                        false,
                        out loan,
                        out pawnAppObj,
                        out customerObj,
                        out errorCode,
                        out errorText))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Add(loan);
                        product = loan;
                    }
                    else
                    {
                        MessageBox.Show(@"Error retrieving pawn loan details");
                    }
                }
            }
            return product;
        }

        private void LoadProductHistoryLoanData()
        {
            LoadProductHistoryLoanData(0);
        }

        private void LoadProductHistoryLoanData(int selectedRowIndex)
        {
            int iEndIdx = _ProductHistoryLoanKeysIndex + _MaxLoanKeysDisplayed;

            if (iEndIdx > _ProductHistoryLoanKeys.Count)
            {
                iEndIdx = _ProductHistoryLoanKeys.Count;
            }

            if (_ProductHistoryLoanKeys.Count >= 0)
            {
                PH_TicketsDataGridView.Rows.Clear();
                // _ListTicketsDataGridViewRows = new List<TicketsDataGridViewRow>();
                //for (int i = iStartIdx; i < iEndIdx; i++)
                for (int i = 0; i < _ProductHistoryLoanKeys.Count; i++)
                {
                    /* var ticketsDataRow = new TicketsDataGridViewRow()
                    {
                    PH_Tickets_TicketNumberColumn = Utilities.GetStringValue(_ProductHistoryLoanKeys[i].OrgShopNumber, "").PadLeft(5, '0') + Utilities.GetStringValue(_ProductHistoryLoanKeys[i].TicketNumber, ""),
                    PH_Tickets_StatusDateColumn = _ProductHistoryLoanKeys[i].LoanStatus.ToString(),
                    PH_Tickets_StatusColumn = _ProductHistoryLoanKeys[i].LoanStatus.ToString()
                    };
                    _ListTicketsDataGridViewRows.Add(ticketsDataRow);
                    if (_ListTicketsDataGridViewRows.Count > 0)
                    {
                    PH_TicketsDataGridView.DataSource = _ListTicketsDataGridViewRows;
                    }
                    */
                    int gvIdx = PH_TicketsDataGridView.Rows.Add();
                    DataGridViewRow myRow = PH_TicketsDataGridView.Rows[gvIdx];

                    myRow.Cells["PH_Tickets_TicketNumberColumn"].Value =
                    Utilities.GetStringValue(_ProductHistoryLoanKeys[i].OrgShopNumber, "").PadLeft(5, '0') +
                    Utilities.GetStringValue(_ProductHistoryLoanKeys[i].TicketNumber, "");
                    myRow.Cells["PH_Tickets_StatusDateColumn"].Value = _ProductHistoryLoanKeys[i].StatusDate.ToShortDateString() + " " + _ProductHistoryLoanKeys[i].StatusTime.ToShortTimeString();
                    myRow.Cells["PH_Tickets_StatusColumn"].Value = _ProductHistoryLoanKeys[i].LoanStatus.ToString();
                }

                SelectTicketRow(selectedRowIndex);
            }
        }

        private void PH_FirstButton_Click(object sender, EventArgs e)
        {
            _ProductHistoryLoanKeysIndex = 0;
            LoadProductHistoryLoanData();
        }

        private void Ph_PreviousButton_Click(object sender, EventArgs e)
        {
            _ProductHistoryLoanKeysIndex = _ProductHistoryLoanKeysIndex - _MaxLoanKeysDisplayed > 0 ? _ProductHistoryLoanKeysIndex - _MaxLoanKeysDisplayed : 0;
            LoadProductHistoryLoanData();
        }

        private void PH_NextButton_Click(object sender, EventArgs e)
        {
            _ProductHistoryLoanKeysIndex = _ProductHistoryLoanKeysIndex + _MaxLoanKeysDisplayed <= _ProductHistoryLoanKeys.Count ? _ProductHistoryLoanKeysIndex + _MaxLoanKeysDisplayed : _ProductHistoryLoanKeysIndex;
            LoadProductHistoryLoanData();
        }

        private void PH_LastButton_Click(object sender, EventArgs e)
        {
            _ProductHistoryLoanKeysIndex = (_ProductHistoryLoanKeys.Count / _MaxLoanKeysDisplayed) * _MaxLoanKeysDisplayed;
            LoadProductHistoryLoanData();
        }

        private void PH_ReceiptsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void customButtonExit_Click(object sender, EventArgs e)
        {
            //if (GlobalDataAccessor.Instance.DesktopSession.ShowOnlyHistoryTabs)
            //{
            //    GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan = null;
            //    GlobalDataAccessor.Instance.DesktopSession.ShowOnlyHistoryTabs = false;
            //    this.NavControlBox.IsCustom = true;
            //    this.NavControlBox.CustomDetail = "Exit";
            //}
            //else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals("newpawnloan", StringComparison.OrdinalIgnoreCase) ||
            //         GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals("describemerchandise", StringComparison.OrdinalIgnoreCase))
            //{
            //    GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
            //    this.NavControlBox.IsCustom = true;
            //    this.NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly";
            //    this.NavControlBox.Action = NavBox.NavAction.BACK;
            //}
            //else
            //{
            //    DialogResult dR = MessageBox.Show(
            //        "Do you want to continue processing this customer?", "Product History", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dR == DialogResult.No)
            //    {
            //        GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
            //    }
            //}
            //1/29/2010 According to QA requirement Cancel should take you to ring menu!
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly";
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            
        }

        private void PH_ItemDescriptionDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*   if (e.RowIndex >= 0 && e.ColumnIndex == 2 && !String.IsNullOrEmpty(PH_ItemDescriptionDataGridView[2,e.RowIndex].Value.ToString()))
            {
            if (CashlinxDesktopSession.Instance.PawnLoans != null)
            {
            DataGridViewRow myRow = PH_TicketsDataGridView.SelectedRows[0];
            int ticketsRow = myRow.Index+_ProductHistoryLoanKeysIndex;
            PawnLoan pawnLoanKey = _ProductHistoryLoanKeys[ticketsRow];
            int iDx = CashlinxDesktopSession.Instance.PawnLoans.FindIndex(
            pl => pl.TicketNumber == pawnLoanKey.TicketNumber);
            PawnLoan pawnLoan = null;
            if (iDx >= 0)
            {
            pawnLoan = CashlinxDesktopSession.Instance.PawnLoans[iDx];
            }
            if (pawnLoan != null)
            {
            myRow = PH_ItemDescriptionDataGridView.Rows[e.RowIndex];
            string sICN = Utilities.GetStringValue(myRow.Cells["PH_Description_ICN"].Value, "");
            PawnItem pawnItem = pawnLoan.Items
            .First(pi => pi.Icn == sICN);
            if (pawnItem != null)
            {
            if (CashlinxDesktopSession.Instance.ActivePawnLoan == null)
            {
            CashlinxDesktopSession.Instance.PawnLoans = new List<PawnLoan>(1)
            {
            new PawnLoan()
            };
            if (CashlinxDesktopSession.Instance.ActivePawnLoan != null)
            CashlinxDesktopSession.Instance.ActivePawnLoan.Items = new List<PawnItem>();
            }
            else if (CashlinxDesktopSession.Instance.ActivePawnLoan.TicketNumber != pawnLoan.TicketNumber &&
            CashlinxDesktopSession.Instance.ActivePawnLoan.TicketNumber != 0)
            {
            CashlinxDesktopSession.Instance.PawnLoans = new List<PawnLoan>(1)
            {
            new PawnLoan()
            };
            if (CashlinxDesktopSession.Instance.ActivePawnLoan != null)
            CashlinxDesktopSession.Instance.ActivePawnLoan.Items = new List<PawnItem>();
            }
            //The attributes of the pawn item need to be derived
            int iCategoryMask = CashlinxDesktopSession.Instance.CategoryXML.GetCategoryMask
            (pawnItem.CategoryCode);
            DescribedMerchandise dmPawnItem = new DescribedMerchandise(iCategoryMask);
            PawnItem.PawnItemMerge(ref pawnItem, dmPawnItem.SelectedPawnItem, true);
            pawnItem.CategoryMask = iCategoryMask;
            pawnItem.ItemReason = PawnItemReason.BLNK;
            if (CashlinxDesktopSession.Instance.ActivePawnLoan != null)
            {
            CashlinxDesktopSession.Instance.ActivePawnLoan.Items.Add(pawnItem);
            }
            //When the Add new loan flow starts only the product history and Item history 
            //tabs should be enabled
            CashlinxDesktopSession.Instance.ShowOnlyHistoryTabs = true;
            NavControlBox.CustomDetail = "AddNewLoan";
            NavControlBox.IsCustom = true;
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            } // pawn item not null
            }//pawn loan not null
            }//pawnloans in session is not null
            }*/
        }

        private void PH_FromDateTextBox_Leave(object sender, EventArgs e)
        {
            if (PH_FromDateTextBox.Text.Length >= 8)
            {
                DateTime fromdate = Utilities.GetDateTimeValue(PH_FromDateTextBox.Text, ShopDateTime.Instance.ShopDate.AddDays(-30));
                PH_ToDateTextBox.Text = fromdate.AddDays(90).ToShortDateString();
            }
        }

        private void PH_ReceiptsDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //TLR - 7/16/2010 This even was written to handle link label column clicks.  
            CustomerProductDataVO product = (CustomerProductDataVO)_selectedProduct;

            //RECEIPT CLICKED.
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                int ticketNumber = product.TicketNumber;

                if (_selectedProduct is SaleVO && ((SaleVO)_selectedProduct).RefType == "4")
                    ticketNumber = ((SaleVO)_selectedProduct).RefNumber;

                string stringReceiptNumber = PH_ReceiptsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                long longReceiptNumber = long.Parse(stringReceiptNumber);
                List<CouchDbUtils.PawnDocInfo> pawnDocs;
                var errString = string.Empty;

                //If a legit ticket number was pulled, then continue.
                if (ticketNumber > 0)
                {
                    //Instantiate docinfo which will return info we need to be able to 
                    //call reprint ticket.
                    CouchDbUtils.PawnDocInfo docInfo = new CouchDbUtils.PawnDocInfo();
                    docInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.STORE_TICKET);
                    docInfo.StoreNumber = product.OrgShopNumber; // GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                    docInfo.TicketNumber = ticketNumber;
                    //Use couch DB to get the document.
                    if (CouchDbUtils.GetPawnDocument(GlobalDataAccessor.Instance.OracleDA, GlobalDataAccessor.Instance.CouchDBConnector,
                                                     docInfo, false, out pawnDocs, out errString))
                    {
                        if (pawnDocs != null)
                        {
                            long.Parse(
                                PH_ReceiptsDataGridView.Rows[e.RowIndex].Cells[
                                e.ColumnIndex].Value.ToString());
                            //Find that there is a document with a receipt.
                            var results = from p in pawnDocs
                                          where p.ReceiptNumber == longReceiptNumber && p.DocumentType == Document.DocTypeNames.RECEIPT
                                          select p;
                            if (results.Count() > 0)
                            {
                                //Get the only one receipt that should exist.
                                docInfo = results.First();

                                //Call the reprint screen.
                                //docInfo.DocumentType = PawnObjects.Doc.Document.DocTypeNames.RECEIPT;
                                ReprintDocument docViewPrint = new ReprintDocument("Receipt# " + stringReceiptNumber, docInfo.StorageId, docInfo.DocumentType);
                                docViewPrint.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Did not find a receipt # " + stringReceiptNumber);
                                return;
                                //Todo: Log or show problem if one exists here.
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Did not find a receipt # " + stringReceiptNumber);
                        return;
                    }
                }
            }
            //TYPE CLICKED
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                string eventType = PH_ReceiptsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Receipt selectedReceipt = (from Receipt r in product.Receipts
                                           where r.ReceiptNumber == PH_ReceiptsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()
                                           select r).First();
                //New loan has its own screen, while everything else falls under the dynamic loan dialog
                if ((selectedReceipt.Event == ReceiptEventTypes.New.ToString()
                     || ((selectedReceipt.Event == ReceiptEventTypes.Renew.ToString()
                          || selectedReceipt.Event == ReceiptEventTypes.Paydown.ToString())
                          && selectedReceipt.Amount == 0)) && _selectedProduct is PawnLoan)
                {
                    ProductHistory_Dialog dialog = new ProductHistory_Dialog((PawnLoan)_selectedProduct, e.RowIndex);
                    dialog.ShowDialog();
                }
                else if (selectedReceipt.Event == ReceiptEventTypes.PolSeize.ToString()
                         || selectedReceipt.Event == ReceiptEventTypes.RTC.ToString())
                {
                    /*
                    PoliceHoldRelease dialog = new PoliceHoldRelease();
                    dialog.ReadOnly = true;
                    dialog.ReceiptDetailId = PH_ReceiptsDataGridView.Rows[e.RowIndex].Cells["PH_Receipt_detail_number"].Value.ToString();
                    if (selectedReceipt.Event == ReceiptEventTypes.PolSeize.ToString())
                        dialog.PoliceSeize = true;
                    else if (selectedReceipt.Event == ReceiptEventTypes.RTC.ToString())
                        dialog.ReleaseToClaimant = true;
                    dialog.ShowDialog(); */
                }
                else if ((selectedReceipt.Event == ReceiptEventTypes.PUR.ToString() ||
                          selectedReceipt.Event == ReceiptEventTypes.VPR.ToString()) &&
                          _selectedProduct is PurchaseVO)
                {
                    PurchaseHistory_Dialog d = new PurchaseHistory_Dialog(
                        (PurchaseVO)_selectedProduct, e.RowIndex);
                    d.ShowDialog(); 
                }
                else if ((selectedReceipt.Event == ReceiptEventTypes.RET.ToString()
                          || selectedReceipt.Event == ReceiptEventTypes.VRET.ToString()) &&
                          _selectedProduct is PurchaseVO)
                {
                    ReturnHistory_Dialog d = new ReturnHistory_Dialog(
                        (PurchaseVO)_selectedProduct, e.RowIndex);
                    d.ShowDialog();
                }
                else if ((eventType.Equals("Extension") || selectedReceipt.Event == ReceiptEventTypes.Extend.ToString()) &&
                         _selectedProduct is PawnLoan)
                {
                    PawnLoan pl = (PawnLoan)_selectedProduct;
                    DataSet extensionData;

                    CustomerLoans.GetExtensionDetails(
                        pl.OrgShopNumber,
                        //GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, 
                        pl.TicketNumber,
                        pl.Receipts[e.RowIndex].ReceiptDetailNumber,
                        out extensionData);

                    if (extensionData != null && extensionData.Tables.Count > 0)
                    {
                        DataRow r = extensionData.Tables[0].Rows[0];

                        Extension_Dialog d = new Extension_Dialog(pl, r);
                        d.ShowDialog();
                    }
                }
                else if (eventType.Equals("Sale") && _selectedProduct is SaleVO)
                {
                    SaleVO sale = (SaleVO)_selectedProduct;
                    Sale_dialog saleDialog = new Sale_dialog(sale);
                    saleDialog.ShowDialog();
                }
                else if ((eventType.Equals("Layaway") || eventType == ReceiptEventTypes.LAY.ToString() ||
                          eventType == ReceiptEventTypes.VLAYPMT.ToString() || eventType.Equals("Layaway Payment") ||
                          eventType.Equals("Renewal") || selectedReceipt.Event == ReceiptEventTypes.LAY.ToString()) &&
                          _selectedProduct is LayawayVO )
                {
                    LayawayVO layawayData = (LayawayVO)_selectedProduct;
                    Layaway_dialog layawayDialog = new Layaway_dialog(layawayData, selectedReceipt);
                    layawayDialog.ShowDialog();
                }
                else if ((eventType.Equals("Layaway")  || eventType == ReceiptEventTypes.FORF.ToString() ||
                    eventType == ReceiptEventTypes.LAY.ToString() || eventType == ReceiptEventTypes.VLAYPMT.ToString() ||
                    eventType == ReceiptEventTypes.LAYDOWNREF.ToString() || eventType == ReceiptEventTypes.LAYPMT.ToString () ||
                    eventType == ReceiptEventTypes.LAYREF.ToString() || eventType == ReceiptEventTypes.LAYSF.ToString() ||
                    eventType == ReceiptEventTypes.VLAY.ToString() || eventType.Equals("Layaway Payment") || eventType.Equals("Layaway Service Fee")) &&
                    _selectedProduct is SaleVO) 
                {
                    LayawayVO layawayData = new LayawayVO( (SaleVO) _selectedProduct);
                    Layaway_dialog layawayDialog = new Layaway_dialog(layawayData, selectedReceipt);
                    layawayDialog.ShowDialog();
                }
                else if (eventType.Equals("Refund") && _selectedProduct is SaleVO)
                {
                    SaleVO sale = (SaleVO)_selectedProduct;
                    SaleRefund_Dialog saleRefDialog = new SaleRefund_Dialog(sale);
                    saleRefDialog.ShowDialog();
                }
                else if (eventType.Equals(ReceiptEventTypes.VSALE.ToString()) &&
                         _selectedProduct is SaleVO)
                {
                    SaleVO sale = (SaleVO)_selectedProduct;
                    VoidSale_dialog vdSaleDialog = new VoidSale_dialog(sale);
                    vdSaleDialog.ShowDialog();
                }
                else if (eventType.Equals(ReceiptEventTypes.VSALEREF.ToString()) &&
                         _selectedProduct is SaleVO)
                {
                    SaleVO sale = (SaleVO)_selectedProduct;
                    VoidSaleRefund_Dialog vdSaleRefDialog = new VoidSaleRefund_Dialog(sale);
                    vdSaleRefDialog.ShowDialog();
                }
                else if (_selectedProduct is PawnLoan && !IsAnEventWithNoScreen(selectedReceipt.Event) &&
                         !(selectedReceipt.Event == ReceiptEventTypes.PFI.ToString() &&
                           selectedReceipt.Type == "2") &&
                         _selectedProduct is PawnLoan)
                {
                    if (selectedReceipt.Event == ReceiptEventTypes.PARTP.ToString())
                    {
                        if (GlobalDataAccessor.Instance.CurrentSiteId.State == States.Ohio)
                        {
                            PawnLoan pl = (PawnLoan)_selectedProduct;
                            string receiptDetailNumber = selectedReceipt.ReceiptDetailNumber;
                            PartialPayment ppayment = pl.PartialPayments.Find(p => p.ReceiptDetail_Number == Convert.ToInt32(receiptDetailNumber));            
                            if (ppayment != null)
                            {
                                ppayment.LoanNumber = pl.TicketNumber;
                                PartialPawnPaymentHistory dialog = new PartialPawnPaymentHistory(PartialPawnPaymentHistoryFormMode.PartialPawnPaymentHistory, ppayment, pl);
                                dialog.ShowDialog();
                            }
                        }
                    }
                    else if (selectedReceipt.Event == ReceiptEventTypes.VPARTP.ToString())
                    {
                        if (GlobalDataAccessor.Instance.CurrentSiteId.State == States.Ohio)
                        {
                            PawnLoan pl = (PawnLoan)_selectedProduct;
                            string receiptDetailNumber = selectedReceipt.ReceiptDetailNumber;
                            PartialPayment ppayment = pl.PartialPayments.Find(p => p.ReceiptDetail_Number == Convert.ToInt32(receiptDetailNumber));
                            if (ppayment != null)
                            {
                                ppayment.LoanNumber = pl.TicketNumber;
                                PartialPawnPaymentHistory dialog = new PartialPawnPaymentHistory(PartialPawnPaymentHistoryFormMode.VoidPartialPawnPaymentHistory, ppayment, pl);
                                dialog.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        //ProductHistoryDynamicPanel_Dialog dialog = new ProductHistoryDynamicPanel_Dialog((PawnLoan)_selectedProduct, e.RowIndex);
                        //dialog.ShowDialog();
                    }
                }
            }
        }

        private void PH_ReceiptsDataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Can potentially throw an 'IndexOutOfRangeException' if not checked.
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                PH_ReceiptsDataGridView.Columns["PH_Receipt_TypeColumn"].Index == e.ColumnIndex)
            {
                //If the event type is not in the list above then keep the cursor normal
                if (_selectedProduct != null)
                {
                    if (((CustomerProductDataVO)_selectedProduct).Receipts.Count > e.RowIndex &&
                        IsAnEventWithNoScreen(
                            ((CustomerProductDataVO)_selectedProduct).Receipts[e.RowIndex].Event))
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        /// <summary>
        /// Tests to see if an event has a screen.
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private bool IsAnEventWithNoScreen(string eventType)
        {
            if ((from n in _receiptEventWithNoScreen
                 where n.ToUpper() == eventType.ToUpper()
                 select n).Any())
            {
                return true;
            }
            return false;
        }

        private void PH_TicketsDataGridView_Sorted(object sender, EventArgs e)
        {
            //LoadProductHistoryLoanData();
        }

        public struct TicketsDataGridViewRow
        {
            public string PH_Tickets_TicketNumberColumn { get; set; }

            public string PH_Tickets_StatusDateColumn { get; set; }

            public string PH_Tickets_StatusColumn { get; set; }
        }

        private void PH_ShowComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPresetData();

            if (PH_ShowComboBox.SelectedIndex == 3)
            {
                PH_LoanAmountLabel.Text = "Layaway Amount";
                PH_Description_ICN.Visible = false;
                PH_Description_ItemAmountColumn.Visible = false;
                PH_BalanceLabel.Visible = true;
                PH_BalanceValue.Visible = true;

                PH_Receipt_AmountDueColumn.Visible = true;
                PH_Receipt_DueDateColumn.Visible = true;
            }
            else
            {
                PH_LoanAmountLabel.Text = "Amount";
                PH_Description_ICN.Visible = false;
                PH_Description_ItemAmountColumn.Visible = false;
                PH_BalanceLabel.Visible = false;
                PH_BalanceValue.Visible = false;

                PH_Receipt_AmountDueColumn.Visible = false;
                PH_Receipt_DueDateColumn.Visible = false;
            }
        }
    }
}
