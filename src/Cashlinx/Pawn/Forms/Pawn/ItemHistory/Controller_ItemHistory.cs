/************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.ItemHistory
 * Class:           Controller_ItemHistory
 * 
 * Description      Form used by Controller for Item History View
 * 
 * History
 * David D Wise, Initial Development
 * SR 6/25/2010 Fixed an issue wherein when an item from history was added
 * to new pawn loan some of the data that should not be carried over from the
 * old pawn item did carry forward to the new one
 * 
 * Tracy 11/18/2010 Enhanced usability by sorting items by status and
 *      scrolling previously picked up items into view.
 *      SR 07/27/2011 CQ INTG100014113 Added logic to not allow users
 *      to select a firearm from item history to start a new loan
 ************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Pawn.Forms.Pawn.Products.ProductHistory;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.ItemHistory
{
    public partial class Controller_ItemHistory : Form
    {
        private int _ActiveTicketNumber;
        private string _ActiveICN;
        private string custNumber;
        private ProcessingMessage procMsgForm;


        //private BackgroundWorker [] bg_tasks = new BackgroundWorker[3];

        //private AutoResetEvent[] bgControl = new AutoResetEvent[3];
       // private bool[] bg_isDone = {
       //        false, false, false
       // };

       // private int priority = -1;

        private bool [] _Setup = {
                false, false, false, false
        };
        public NavBox NavControlBox;

        public Controller_ItemHistory()
        {
            InitializeComponent();
            Setup();
           
            IH_ProductComboBox.SelectedIndex = 1;
            this.NavControlBox = new NavBox();
            this.NavControlBox.Owner = this;
            
        }

        private void Setup()
        {
            IH_CategoryComboBox.SelectedText = "Pawn";
            IH_CategoryComboBox.Enabled = false;
            _ActiveTicketNumber = 0;

            DataView theData = new DataView();
            theData.Table = new DataTable("ItemHistory");
            theData.Table.Columns.Add(IH_History_TransactionNumberColumn.Name);
            theData.Table.Columns.Add(IH_History_StatusDateColumn.Name);
            theData.Table.Columns.Add(IH_History_ItemStatusColumn.Name);
            theData.Table.Columns.Add(IH_History_ItemDescriptionColumn.Name);
            theData.Table.Columns.Add(IH_History_DocType.Name);
            theData.Table.Columns.Add(IH_History_TktNo.Name);


            IH_ItemHistoryDataGridView.DataSource = theData;

            //Get all the customer loans
            if (GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryLoans == null ||
                GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryLoans.Count == 0)
            {

                if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
                    custNumber = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber;
                if (!string.IsNullOrEmpty(custNumber))
                {
                    Cursor = Cursors.WaitCursor;
                    procMsgForm = new ProcessingMessage("Retrieving Customer Loan History");
					SetButtonState(false);
                    procMsgForm.Show(this);


                    if (!_Setup[0])
                    {
                        CustomerProcedures.GetCustomerLoanData(GlobalDataAccessor.Instance.DesktopSession, custNumber);


                    }

                    //CustomerProcedures.GetCustomerLoanData(custNumber);
                    /*
                    bgControl[0] = new AutoResetEvent(false);
                    bg_tasks[0] = new BackgroundWorker();
                    bg_tasks[0].DoWork += bw_getSales;
                    bg_tasks[0].RunWorkerAsync();

                    bgControl[1] = new AutoResetEvent(false);
                    bg_tasks[1] = new BackgroundWorker();
                    bg_tasks[1].DoWork += bw_getBuys;
                    bg_tasks[1].RunWorkerAsync();

                    bgControl[2] = new AutoResetEvent(false);
                    bg_tasks[2] = new BackgroundWorker();
                    bg_tasks[2].DoWork += bw_getLayaways;
                    bg_tasks[2].RunWorkerAsync();                                                                           
                    */
                    //procMsgForm.Dispose();

                }
            }
            if (GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryLoans != null)
            {
                sync_populateGrid(0, GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryLoans,
                                          o => o.Items.Count);

                //addData(theData,  (List<CustomerProductDataVO>) CashlinxDesktopSession.Instance.CustomerHistorySales.Cast<CustomerProductDataVO>());

                theData.Sort = IH_History_ItemStatusColumn.Name;

                sortHistoryData();

                _Setup[0] = true;

                IH_AddItemToNewPawnLoan.Enabled = false;
                if (GlobalDataAccessor.Instance.DesktopSession.ShowOnlyHistoryTabs)
                    this.customButtonExit.Text = "Exit";
                else
                    this.customButtonExit.Text = "Cancel";
            }

            SetButtonState(true);

            if (procMsgForm != null)
                procMsgForm.Close();

            this.Cursor = Cursors.Default;
        }


        void sortHistoryData()
        {
            // Tracy 11/18/2010 - corrects for usability issue by making it easier to re-pawn an earlier picked up item            
            //IH_ItemHistoryDataGridView.Sort(IH_ItemHistoryDataGridView.Columns[2],
            //                               ListSortDirection.Ascending);
            var rows =
                    from DataGridViewRow r in IH_ItemHistoryDataGridView.Rows
                    where r.Cells[2].Value.ToString().Equals("Pickup")
                    select r;

            if (rows.Any())
            {
                int targetIdx = IH_ItemHistoryDataGridView.Rows.IndexOf(rows.First());
                IH_ItemHistoryDataGridView.Rows[targetIdx].Selected = true;
                IH_ItemHistoryDataGridView.FirstDisplayedScrollingRowIndex = targetIdx;

                
                IH_ItemHistoryDataGridView.CurrentCell = IH_ItemHistoryDataGridView.Rows[targetIdx].Cells[1];
            }
            // Tracy 11/18/2010------------------------------------------------------------------------------    
        }


        void initRow (DataRow aRow, CustomerProductDataVO aProduct, int idx_j)
        {
            if (aProduct is SaleVO)
            {
                aRow[IH_History_TransactionNumberColumn.Name] = ((SaleVO)aProduct).RetailItems[idx_j].Icn;
                aRow[IH_History_ItemDescriptionColumn.Name] = ((SaleVO)aProduct).RetailItems[idx_j].TicketDescription;

                aRow[IH_History_DocType.Name] = ((SaleVO)aProduct).RetailItems[idx_j].mDocType;
                if (aProduct.LoanStatus == ProductStatus.ACT)
                    aRow[IH_History_ItemStatusColumn.Name] = (aProduct is LayawayVO) ?  "On Layaway" : "SOLD" ;
            }

            else
            {
                aRow[IH_History_TransactionNumberColumn.Name] = aProduct.Items[idx_j].Icn;
                aRow[IH_History_ItemDescriptionColumn.Name] = aProduct.Items[idx_j].TicketDescription;

                aRow[IH_History_DocType.Name] = aProduct.Items[idx_j].mDocType;
                if (GlobalDataAccessor.Instance.DesktopSession.LoanStatus != null)
                {
                    var pairType =
                        GlobalDataAccessor.Instance.DesktopSession.LoanStatus.Find(
                            delegate(PairType<ProductStatus, string> p)
                            {
                                return p.Left == aProduct.LoanStatus;
                            });

                    aRow[IH_History_ItemStatusColumn.Name] = (pairType != null) ? pairType.Right : aProduct.LoanStatus.ToString();
                }

            }


            aRow[IH_History_StatusDateColumn.Name] =  
                Utilities.GetDateTimeValue(aProduct.StatusDate, DateTime.MinValue).ToShortDateString();


            aRow[IH_History_TktNo.Name] = Utilities.GetStringValue(aProduct.TicketNumber, "");

        }

        /*
        void bw_getSales(object sender, DoWorkEventArgs e)
        {
            //CustomerProcedures.GetCustomerLoanData(custNumber);
            if (priority > 0)
                Thread.Sleep(10);
            
            RetailProcedures.GetCustomerSales(custNumber);
            bg_isDone[0] = true;
            bgControl[0].Set();
            
        }

        void bw_getBuys(object sender, DoWorkEventArgs e)
        {
            CustomerProcedures.GetCustomerLoanData(custNumber);
           
            CustomerProcedures.GetCustomerLoanData(custNumber);
            if (priority >= 0 && priority != 1)
                Thread.Sleep(10);

            //CustomerProcedures.GetCustomerLoanData(custNumber);
            PurchaseProcedures.GetCustomerPurchases(custNumber);
            bg_isDone[1] = true;
            bgControl[1].Set();
        }

        void bw_getLayaways(object sender, DoWorkEventArgs e)
        {
            if (priority >= 0 && priority != 2)
                Thread.Sleep(10);

            //CustomerProcedures.GetCustomerLoanData(custNumber);
            RetailProcedures.GetCustomerLayaways(custNumber);
            bg_isDone[2] = true;
            bgControl[2].Set();

        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            procMsgForm.Close();
            procMsgForm.Dispose();
            SetButtonState(true);

        }
         * */
        private void SetButtonState(bool enable)
        {
            this.customButtonExit.Enabled = enable;
            
        }


        private void IH_ItemHistoryDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            bool enableAddItem = true;
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                _ActiveTicketNumber = Utilities.GetIntegerValue(IH_ItemHistoryDataGridView.Rows[e.RowIndex].Cells[IH_History_TktNo.Name].Value, 0);
                int docType = int.Parse(((string)IH_ItemHistoryDataGridView.Rows[e.RowIndex].Cells[0].Value).Substring(12, 1));
                string selectedICN = Utilities.GetStringValue(IH_ItemHistoryDataGridView.Rows[e.RowIndex].Cells[0].Value);
                string status =
                    IH_ItemHistoryDataGridView.Rows[e.RowIndex].Cells[
                        IH_History_ItemStatusColumn.Name].Value.ToString();

                if (e.ColumnIndex == 0)
                {
                    //  string docType =
                    //      IH_ItemHistoryDataGridView.Rows[e.RowIndex].Cells[IH_History_DocType.Name].
                    //          Value.ToString();

                    switch (status)
                    {
                        case "SOLD":
                            if (GlobalDataAccessor.Instance.DesktopSession.CustomerHistorySales != null)
                            {
                                Sale_dialog sd = new Sale_dialog(GlobalDataAccessor.Instance.DesktopSession.CustomerHistorySales.Find
                                   (l => l.TicketNumber == _ActiveTicketNumber));

                                sd.ShowDialog();
                            }
                            break;

                        case "On Layaway":
                            if (GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryLayaways != null)
                            {
                                LayawayVO ly = GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryLayaways.Find
                                    (l => l.TicketNumber == _ActiveTicketNumber);
                                Receipt r;

                                if (ly.Receipts.Count > 0)
                                    r = ly.Receipts[0];
                                else
                                    r = new Receipt();

                                Layaway_dialog ld = new Layaway_dialog(ly, r);

                                ld.ShowDialog();
                            }
                            break;

                        case "REF":

                            SaleRefund_Dialog srd = new SaleRefund_Dialog(GlobalDataAccessor.Instance.DesktopSession.CustomerHistorySales.Find
                                (l => l.TicketNumber == _ActiveTicketNumber));

                            srd.ShowDialog();
                            break;

                        default:
                            if (GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryPurchases != null && docType == 2)
                            {
                                PurchaseHistory_Dialog d = new PurchaseHistory_Dialog(GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryPurchases.Find(l => l.TicketNumber == _ActiveTicketNumber), 0, status);

                                if (d.isSetup)
                                    d.ShowDialog();
                            }
                            else if (status.ToUpper() == "VOID")
                            {
                                if (docType == 1)
                                {
                                    PawnLoan pawnLoan = Utilities.CloneObject(GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryLoans.Find(l => l.TicketNumber == _ActiveTicketNumber));
                                    ProductHistory_Dialog productHistory = new ProductHistory_Dialog(pawnLoan, 0);
                                    productHistory.ShowDialog();
                                }
                                else if (docType == 2)
                                {
                                    PurchaseHistory_Dialog d =
                                        new PurchaseHistory_Dialog(
                                            GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryPurchases.Find(
                                                l => l.TicketNumber == _ActiveTicketNumber), 0, status);
                                    d.ShowDialog();
                                }
                            }
                            else
                            {
                                var pawnLoan = Utilities.CloneObject(GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryLoans.Find(l => l.TicketNumber == _ActiveTicketNumber));
                                var productHistory = new ProductHistory_Dialog(pawnLoan, 0);
                                productHistory.ShowDialog();
                            }

                            break;
                    }


                }
                    if (docType == 1)
                    {
                        PawnLoan pawnLoan = Utilities.CloneObject(GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryLoans.Find(l => l.TicketNumber == _ActiveTicketNumber));
                        var gunItems = pawnLoan.Items.Find(i => i.GunNumber > 0 && i.Icn == selectedICN);
                        if (gunItems != null)
                            enableAddItem = false;
                    }
 

                

                _ActiveICN = Utilities.GetStringValue(IH_ItemHistoryDataGridView.Rows[e.RowIndex].Cells[IH_History_TransactionNumberColumn.Name].Value, "");
                string sLoanStatus = Utilities.GetStringValue(IH_ItemHistoryDataGridView.Rows[e.RowIndex].Cells[IH_History_ItemStatusColumn.Name].Value, "");

                PairType<ProductStatus, string> pairType = GlobalDataAccessor.Instance.DesktopSession.LoanStatus
                                                            .First(pt => pt.Right == sLoanStatus);

                if (pairType.Left == ProductStatus.PU)
                {
                    const string resName = "NEWPAWNLOAN";
                    UserVO currUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;
                    IH_AddItemToNewPawnLoan.Enabled = SecurityProfileProcedures.CanUserViewResource(resName, currUser, GlobalDataAccessor.Instance.DesktopSession);
                    IH_AddItemToNewPawnLoan.Enabled = enableAddItem;
                }
                else
                {
                    IH_AddItemToNewPawnLoan.Enabled = false;
                }
            }
        }

        private void IH_AddItemToNewPawnLoan_Click(object sender, EventArgs e)
        {
            var pawnLoan = GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryLoans
                                .First(pl => pl.TicketNumber == _ActiveTicketNumber);

            if (pawnLoan != null)
            {

                Item pawnItem = pawnLoan.Items
                                    .First(pi => pi.Icn == _ActiveICN);

                if (pawnItem != null)
                {
                    if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan == null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.PawnLoans = new List<PawnLoan>(1)
                                                                    {
                                                                        new PawnLoan()
                                                                    };
                        if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan != null)
                            GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items = new List<Item>();
                        GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = 0;

                    }
                    else if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.TicketNumber != pawnLoan.TicketNumber
                        && GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.TicketNumber != 0
                        )
                    {
                        GlobalDataAccessor.Instance.DesktopSession.PawnLoans = new List<PawnLoan>(1)
                                                                    {
                                                                        new PawnLoan()
                                                                    };
                        GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items = new List<Item>();
                        GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = 0;
                    }
                    else
                    {
                        int itemAlreadyExists=GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.FindIndex(s => s.Icn == pawnItem.Icn);
                        if (itemAlreadyExists >= 0)
                            GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.RemoveAt(itemAlreadyExists);
                        GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Count;
                    }
 

                    //The attributes of the pawn item need to be derived
                    int iCategoryMask = GlobalDataAccessor.Instance.DesktopSession.CategoryXML.GetCategoryMask
                        (pawnItem.CategoryCode);
                    
                    var dmPawnItem = new DescribedMerchandise(iCategoryMask);
                    Item.PawnItemMerge(ref pawnItem, dmPawnItem.SelectedPawnItem, false);
                    pawnItem.CategoryMask = iCategoryMask;
                    pawnItem.ItemReason = ItemReason.BLNK;
                    pawnItem.mDocType = "1";
                    if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan != null)
                    {
                        if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Count > 0)
                        pawnItem.mItemOrder = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Count-1;
                        else
                            pawnItem.mItemOrder = 0;
                    }

                    pawnItem.mStore = Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
                    pawnItem.mYear = Convert.ToInt16(ShopDateTime.Instance.ShopDate.Year.ToString().Substring(ShopDateTime.Instance.ShopDate.Year.ToString().Length - 1));
                    if (pawnItem.IsGun)
                    {
                        QuickCheck quickInfo = new QuickCheck
                                               {
                                                       GunType = pawnItem.CategoryDescription
                                               };
                        pawnItem.QuickInformation = quickInfo;

                    }
                    GlobalDataAccessor.Instance.DesktopSession.DescribeItemSelectedProKnowMatch = pawnItem.SelectedProKnowMatch;
                    
                    GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext = CurrentContext.NEW;
                    if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan != null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Fees.Clear();
                        GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Add(pawnItem);
                    }
                    NavControlBox.CustomDetail = "AddNewLoan";
                    NavControlBox.IsCustom = true;
                    NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                }
            }
        }

        private void Controller_ItemHistory_Load(object sender, EventArgs e)
        {
            IH_ItemHistoryDataGridView.Enabled = true;
        }

        private void customButtonExit_Click(object sender, EventArgs e)
        {
            /*
            if (bg_tasks[0].IsBusy)
                bg_tasks[0].CancelAsync();

            if (bg_tasks[1].IsBusy)
                bg_tasks[1].CancelAsync();

            if (bg_tasks[2].IsBusy)
                bg_tasks[2].CancelAsync();
            */
            if (procMsgForm != null)
                procMsgForm.Dispose();

            if (GlobalDataAccessor.Instance.DesktopSession.ShowOnlyHistoryTabs)
            {
                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan = null;
                GlobalDataAccessor.Instance.DesktopSession.ShowOnlyHistoryTabs = false;
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "Exit";
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals("newpawnloan", StringComparison.OrdinalIgnoreCase) ||
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals("DescribeMerchandise",StringComparison.OrdinalIgnoreCase))
            {
                GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "Exit";
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }
            else
            {
                DialogResult dR = MessageBox.Show("Do you want to continue processing this customer?", "Item History", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dR == DialogResult.No)
                {
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();

                }

                //1/29/2010 According to QA requirement Cancel should take you to ring menu!
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "Menu";
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }


        }
        /*
        private void async_populateGrid<T>(int idx, string msg, List<T> data) where T : CustomerProductDataVO
        {
            priority = idx;

            if (!bg_isDone[idx])
            {
                procMsgForm = new ProcessingMessage(msg);
                procMsgForm.Show(this);
                
                bgControl[idx].WaitOne();

                procMsgForm.Close();
                bgControl[idx].Close();
                // procMsgForm.Dispose();
            }


            if (!_Setup_bg[idx] )
            {
                //PurchaseProcedures.GetCustomerPurchases(custNumber);

                if (data != null)
                {
                    // PurchaseProcedures.GetCustomerPurchases(custNumber);

                    for (int i = 0; i < data.Count; i++)
                    {
                        for(int j = 0; j < data[i].Items.Count(); j++)
                        {
                            if (data[i].TicketNumber > 0)
                            {
                                DataRowView aRowView = ((DataView)IH_ItemHistoryDataGridView.DataSource).AddNew();
                                initRow(aRowView.Row, data[i], j);
                            }
                        }
                    }
                }

                _Setup_bg[idx] = true;
            }

            priority = -1;
        }

        */

        public delegate int getCountDelegate(CustomerProductDataVO o);

        private void sync_populateGrid<T>(int idx, List<T> data, getCountDelegate getCount) where T : CustomerProductDataVO
        {
            if (!_Setup[idx])
            {

                if (data != null)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        int itemCount = getCount(data[i]);

                        for (int j = 0; j < itemCount; j++)
                        {
                            if (data[i].TicketNumber > 0)
                            {
                                DataRowView aRowView = ((DataView)IH_ItemHistoryDataGridView.DataSource).AddNew();
                                initRow(aRowView.Row, data[i], j);
                            }
                        }
                    }
                }

                _Setup[idx] = true;


            }
        }
        


        private void IH_ProductComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (_Setup[0])
            {
                Cursor = Cursors.WaitCursor;


                string filter = string.Empty;
                string docType = null;

                IH_ItemHistoryDataGridView.ClearSelection();
                IH_ItemHistoryDataGridView.CurrentCell = null;
                var test = GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryPurchases;
                SetButtonState(false);
                switch (IH_ProductComboBox.SelectedItem.ToString())
                {
                    case "Pawn":
                        docType = "1";
                        filter = IH_History_DocType.Name + "=" + 1;
                        break;

                    case "Buy":
                        docType = "2";
                        filter = IH_History_DocType.Name + "=" + 2;
                        procMsgForm = new ProcessingMessage("Retrieving Customer Purchase History");
                        procMsgForm.Show(this);
                        
                        
                        //async_populateGrid(1, "Retrieving Customer Purchase History", CashlinxDesktopSession.Instance.CustomerHistoryPurchases);
                        if (!_Setup[2])
                        {
                            PurchaseProcedures.GetCustomerPurchases(custNumber);

                            sync_populateGrid<PurchaseVO>(2, GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryPurchases, o => o.Items.Count);
                        }
                        break;

                    case "Sale":
                        docType = null;
                        //CashlinxDesktopSession.Instance.CustomerHistorySales
                        procMsgForm = new ProcessingMessage("Retrieving Customer Sales History");
                        procMsgForm.Show(this);
                        filter = IH_History_ItemStatusColumn.Name + "= 'SOLD'";

                        //async_populateGrid(0, "Retrieving Customer Sales History", CashlinxDesktopSession.Instance.CustomerHistorySales);
                        if (!_Setup[1])
                        {
                            RetailProcedures.GetCustomerSales(GlobalDataAccessor.Instance.DesktopSession, custNumber);

                            sync_populateGrid<SaleVO>(1, GlobalDataAccessor.Instance.DesktopSession.CustomerHistorySales,
                                                      o => ((SaleVO)o).RetailItems.Count);
                        }
                        break;

                    case "Layaway":
                        docType = null;
                        filter = IH_History_ItemStatusColumn.Name + "= 'On Layaway'";
                        procMsgForm = new ProcessingMessage("Retrieving Customer Layaway History");
                        procMsgForm.Show(this);

                        //async_populateGrid(2, "Retrieving Customer Layaway History", CashlinxDesktopSession.Instance.CustomerHistoryLayaways);
                        if (!_Setup[3])
                        {
                            RetailProcedures.GetCustomerLayaways(GlobalDataAccessor.Instance.DesktopSession, custNumber);

                            sync_populateGrid<LayawayVO>(3, GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryLayaways, o => ((LayawayVO)o).RetailItems.Count);
                        }
                        break;

                    case "Refund":
                        docType = null;
                        filter = IH_History_ItemStatusColumn.Name + "= 'REF'";
                        break;

                    case "All":
                        procMsgForm = new ProcessingMessage("Retrieving Customer Item History");
                        procMsgForm.Show(this);
                        if (!_Setup[1])
                        {
                            RetailProcedures.GetCustomerSales(GlobalDataAccessor.Instance.DesktopSession, custNumber);

                            sync_populateGrid<SaleVO>(1, GlobalDataAccessor.Instance.DesktopSession.CustomerHistorySales,
                                                      o => ((SaleVO)o).RetailItems.Count);
                        }

                        if (!_Setup[2])
                        {
                            PurchaseProcedures.GetCustomerPurchases(custNumber);

                            sync_populateGrid<PurchaseVO>(2, GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryPurchases, o => o.Items.Count);
                        }


                        if (!_Setup[3])
                        {
                            RetailProcedures.GetCustomerLayaways(GlobalDataAccessor.Instance.DesktopSession, custNumber);

                            sync_populateGrid<LayawayVO>(3, GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryLayaways,  o => ((LayawayVO) o).RetailItems.Count);                            
                        }

                        //async_populateGrid(0, "Retrieving Customer Sales History", CashlinxDesktopSession.Instance.CustomerHistorySales);
                        //async_populateGrid(1, "Retrieving Customer Purchase History", CashlinxDesktopSession.Instance.CustomerHistoryPurchases);
                        //async_populateGrid(2, "Retrieving Customer Layaway History", CashlinxDesktopSession.Instance.CustomerHistoryLayaways);
                        break;
                }


                //-- The filter doesn't seem to handle the very last row
                ((DataView)IH_ItemHistoryDataGridView.DataSource).RowFilter = filter;

                IH_ItemHistoryDataGridView.ClearSelection();
                IH_ItemHistoryDataGridView.CurrentCell = null;

                if (IH_ItemHistoryDataGridView.Rows.Count > 0 && docType != null &&
                    IH_ItemHistoryDataGridView.Rows[IH_ItemHistoryDataGridView.Rows.Count - 1].Cells[IH_History_DocType.Name].Value != null)
                {
                    if ((string)IH_ItemHistoryDataGridView.Rows[IH_ItemHistoryDataGridView.Rows.Count - 1].Cells[IH_History_DocType.Name].Value != docType)
                        IH_ItemHistoryDataGridView.Rows[IH_ItemHistoryDataGridView.Rows.Count - 1].Visible = false;
                    else
                        IH_ItemHistoryDataGridView.Rows[IH_ItemHistoryDataGridView.Rows.Count - 1].Visible = true;
                }

                else if (IH_ItemHistoryDataGridView.Rows.Count > 0)
                {
                    IH_ItemHistoryDataGridView.Rows[IH_ItemHistoryDataGridView.Rows.Count - 1].Visible = true;

                    switch (IH_ProductComboBox.SelectedItem.ToString())
                    {
                        case "Sale":
                            if ((string)IH_ItemHistoryDataGridView.Rows[IH_ItemHistoryDataGridView.Rows.Count - 1].Cells[this.IH_History_ItemStatusColumn.Name].Value != 
                                "SOLD")
                                IH_ItemHistoryDataGridView.Rows[IH_ItemHistoryDataGridView.Rows.Count - 1].Visible = false;

                            break;

                        case "Layaway":
                            if ((string)IH_ItemHistoryDataGridView.Rows[IH_ItemHistoryDataGridView.Rows.Count - 1].Cells[this.IH_History_ItemStatusColumn.Name].Value !=
                                "On Layaway")
                                IH_ItemHistoryDataGridView.Rows[IH_ItemHistoryDataGridView.Rows.Count - 1].Visible = false;
                            break;

                        case "Refund":
                            if ((string)IH_ItemHistoryDataGridView.Rows[IH_ItemHistoryDataGridView.Rows.Count - 1].Cells[this.IH_History_ItemStatusColumn.Name].Value !=
                                "REF")
                                IH_ItemHistoryDataGridView.Rows[IH_ItemHistoryDataGridView.Rows.Count - 1].Visible = false;
                            break;
                    }
                }

                sortHistoryData();


                if (procMsgForm != null) procMsgForm.Close();
                Cursor = Cursors.Default;
                SetButtonState(true);
            }
        }

    }
}
