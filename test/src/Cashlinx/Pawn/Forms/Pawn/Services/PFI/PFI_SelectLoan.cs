using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.ProKnowService;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.PFI
{
    public partial class PFI_SelectLoan : Form
    {
        private List<PawnLoan> _PawnLoans;
        private List<PurchaseVO> _Purchases;
        private List<PawnAppVO> _PawnApplications;
        private List<CustomerVO> _CustomerVOs;
        private List<LoanPrinted> _PrintedLoans;
        private List<groupOfValuesOutputType> _ResponseValues;
        private int _RowsSelected;
        private DataTable _ItemsToPFI = null;

        public struct LoanPrinted
        {
            public int LoanNumber;
            public int RefType;
            public bool bPrinted;
        }

        public PFI_SelectLoan()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            DateTime dt = ShopDateTime.Instance.ShopDate;
            dateCalendarSearchDate.SelectedDate = dt.FormatDate();
            selectAllButton.Enabled = false;
            deselectAllButton.Enabled = false;
            backButton.Enabled = false;
            printButton.Enabled = false;
            continueButton.Enabled = false;
            asOfLabel.Visible = false;
            totalPanel.Visible = false;
            selectAllButton.Visible = false;
            deselectAllButton.Visible = false;
            _PawnLoans = new List<PawnLoan>();
            _PawnApplications = new List<PawnAppVO>();
            _PrintedLoans = new List<LoanPrinted>();
            _ResponseValues = new List<groupOfValuesOutputType>();
            _RowsSelected = 0;
        }

        private void PFI_SelectLoan_Load(object sender, EventArgs e)
        {
        }

        //----

        //----

        private void dateFindButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            string sErrorCode;
            string sErrorText;

            //Removed for defect to another location -- TLR 6/1/2010
            //StoreLoans.CheckForPriorPFI(
            //    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
            //    out refNumbers,
            //    out sErrorCode,
            //    out sErrorText);

            //if(refNumbers.Count > 0)
            //{
            //    string sMsg = "The following loans need to be PFI posted before continuing." + Environment.NewLine + Environment.NewLine;
            //    sMsg += "Pending Loans: ";

            //    foreach (int i in refNumbers)
            //    {
            //        sMsg += i.ToString() + ", ";
            //    }
            //    sMsg = sMsg.Substring(0, sMsg.Length - 2) + ".";

            //    MessageBox.Show(sMsg, "PFI Posting Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (dateCalendarSearchDate.SelectedDate == "mm/dd/yyyy")
            {
                MessageBox.Show("You need to provide a date before continuing.", "PFI Posting Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            backButton.Enabled = false;
            printButton.Enabled = false;
            continueButton.Enabled = false;
            selectAllButton.Enabled = false;
            deselectAllButton.Enabled = false;
            selectAllButton.Visible = false;
            deselectAllButton.Visible = false;
          
            asOfLabel.Text = dateCalendarSearchDate.SelectedDate;
            DateTime dt = Convert.ToDateTime(dateCalendarSearchDate.SelectedDate);
            //dt = dt.AddDays(-1);
            bool noDataFound = false;

            if (StoreLoans.GetPFIableItems(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, dt, out _ItemsToPFI, out sErrorCode, 
                                           out sErrorText))
            //if (StoreLoans.GetStoreLoans(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
            //    ProductStatus.IP, StateStatus.BLNK, dt, true, out _PawnLoans, out _PawnApplications, out _CustomerVOs, out sErrorCode, out  sErrorText))
            {
                gvLoans.Rows.Clear();
                /*
                _PawnLoans.Sort(delegate(PawnLoan p, PawnLoan p2)
                {
                return p.PfiEligible.CompareTo(p2.PfiEligible);
                });
                */
                for (int i = 0; i < _ItemsToPFI.Rows.Count; i++)
                {
                    int gvIdx = gvLoans.Rows.Add();
                    DataGridViewRow myRow = gvLoans.Rows[gvIdx];
                    
                    // VERIFY WHAT COMES BACK TO POPULATE DATAGRIDVIEW
                    myRow.Cells[colPfiDate.Name].Value = String.Format("{0:MM/dd/yyyy}", _ItemsToPFI.Rows[i]["PFI_ELIG"]);
                    myRow.Cells[colType.Name].Value = _ItemsToPFI.Rows[i]["TYPE"];
                    myRow.Cells[colNumber.Name].Value = Utilities.GetIntegerValue(_ItemsToPFI.Rows[i]["TICKET_NUMBER"], 0);
                    myRow.Cells[colCustomerName.Name].Value = _ItemsToPFI.Rows[i]["NAME"];

                    myRow.Cells[colMDSE.Name].Value = _ItemsToPFI.Rows[i]["MDSE_TYPE"];
                    myRow.Cells[colAmount.Name].Value = String.Format("{0:c2}", Utilities.GetDecimalValue(_ItemsToPFI.Rows[i]["AMOUNT"], 0.00M));
                    myRow.Cells[colPaidIn.Name].Value = String.Format("{0:c2}", Utilities.GetDecimalValue(_ItemsToPFI.Rows[i]["PAID_IN"], 0.00M));

                    myRow.Cells[colSelect.Name].ReadOnly = false; 

                    if (Utilities.GetDateTimeValue(_ItemsToPFI.Rows[i]["PFI_ELIG"]).Ticks > dt.Ticks)
                    {
                        myRow.Cells[colSelect.Name].ReadOnly = true;                        
                    }
                    else
                    {
                        myRow.Cells[colSelect.Name].ReadOnly = false; 
                    }
                }

                asOfLabel.Visible = true;
                totalPanel.Visible = true;

                if (dt.Ticks > ShopDateTime.Instance.ShopDate.Ticks)
                {
                    gvLoans.Columns[colSelect.Name].Visible = false;
                    totalSelectedPfiLabelLabel.Visible = false;
                    totalSelectedPfiLabel.Visible = false;
                }
                else
                {
                    gvLoans.Columns[colSelect.Name].Visible = true;
                    totalSelectedPfiLabelLabel.Visible = true;
                    totalSelectedPfiLabel.Visible = true;

                    selectAllButton.Enabled = (gvLoans.Rows.Count > 0);
                    deselectAllButton.Enabled = (gvLoans.Rows.Count > 0);
                    selectAllButton.Visible = (gvLoans.Rows.Count > 0);
                    deselectAllButton.Visible = (gvLoans.Rows.Count > 0);

                    if (selectAllButton.Enabled)
                        selectAllButton.PerformClick();
                }
                
                UpdateCounts();
                this.Cursor = Cursors.Default;
            }
            else
            {
                noDataFound = true;
            }
            
            /*
            if (PurchaseProcedures.GetStorePurchases(CashlinxDesktopSession.Instance.OracleDA,
            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
            ProductStatus.PUR, StateStatus.BLNK, "",dt, true, out _Purchases, out _CustomerVOs, out sErrorCode, out  sErrorText))
            {
                
            _Purchases.Sort((p, p2) => p.PfiEligible.CompareTo(p2.PfiEligible));

            for (int i = 0; i < _Purchases.Count; i++)
            {
            int gvIdx = gvLoans.Rows.Add();
            DataGridViewRow myRow = gvLoans.Rows[gvIdx];
            CustomerVO customerVO = _CustomerVOs.Find(c => c.CustomerNumber == _Purchases[i].CustomerNumber);
            string sCustomerName = "";
            if (customerVO != null)
            {
            sCustomerName = customerVO.LastName + ", ";
            if (customerVO.FirstName != "")
            sCustomerName += customerVO.FirstName + " ";
            if (customerVO.MiddleInitial != "")
            sCustomerName += customerVO.MiddleInitial;
            sCustomerName = sCustomerName.Replace("  ", " ");
            }

            // VERIFY WHAT COMES BACK TO POPULATE DATAGRIDVIEW
            myRow.Cells[colPfiDate.Name].Value = String.Format("{0:MM/dd/yyyy}", _Purchases[i].PfiEligible);
            myRow.Cells[colType.Name].Value = "PURCHASE"; // Utilities.GetStringValue("", "");
            myRow.Cells[colNumber.Name].Value = Utilities.GetIntegerValue(_Purchases[i].TicketNumber, 0);
            myRow.Cells[colCustomerName.Name].Value = sCustomerName;
            myRow.Cells[colMDSE.Name].Value = MerchandiseType(_Purchases[i]);
            myRow.Cells[colAmount.Name].Value = String.Format("{0:c2}", Utilities.GetDecimalValue(_Purchases[i].Amount, 0.00M));
            myRow.Cells[colPaidIn.Name].Value = String.Format("{0:c2}", GetPaidInAmount(_Purchases[i]));

            if (_Purchases[i].PfiEligible.Ticks > dt.Ticks)
            myRow.Cells[colSelect.Name].ReadOnly = true;
            else
            {
            myRow.Cells[colSelect.Name].ReadOnly = false;
            }
            }

            asOfLabel.Visible = true;
            totalPanel.Visible = true;

            if (dt.Ticks > CashlinxDesktop.Desktop.ShopDateTime.Instance.ShopDate.Ticks)
            {
            gvLoans.Columns[colSelect.Name].Visible = false;
            totalSelectedPfiLabelLabel.Visible = false;
            totalSelectedPfiLabel.Visible = false;
            }
            else
            {
            gvLoans.Columns[colSelect.Name].Visible = true;
            totalSelectedPfiLabelLabel.Visible = true;
            totalSelectedPfiLabel.Visible = true;

            selectAllButton.Enabled = (gvLoans.Rows.Count > 0);
            deselectAllButton.Enabled = (gvLoans.Rows.Count > 0);
            selectAllButton.Visible = (gvLoans.Rows.Count > 0);
            deselectAllButton.Visible = (gvLoans.Rows.Count > 0);

            if (selectAllButton.Enabled)
            selectAllButton.PerformClick();
            }

            UpdateCounts();
            _FoundRecords = true;
            this.Cursor = Cursors.Default;
            }
            else
            {
            noPURDataFound = true;
            }
            * */
            if (noDataFound)
                MessageBox.Show(@"No Data Found", "PFI Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            this.Cursor = Cursors.Default;
        }

        private string MerchandiseType(CustomerProductDataVO purchaseObject)
        {
            var sMerchandiseType = string.Empty;
            int iGeneralPass = 0;
            int iJewelryPass = 0;

            foreach (Item item in purchaseObject.Items)
            {
                string sCategoryCodePrefix = item.CategoryCode.ToString().Substring(0, 1);
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

        private decimal GetPaidInAmount(CustomerProductDataVO productObject)
        {
            decimal dPaidAmount = 0;

            foreach (var receipt in productObject.Receipts)
            {
                if (receipt.Event == "Extend")
                    dPaidAmount += Utilities.GetDecimalValue(receipt.Amount, 0);
            }
            return dPaidAmount;
        }

        private void gvLoans_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //           if (_FoundRecords)
            //           {
            if (e.ColumnIndex == gvLoans.Columns[colSelect.Name].Index && e.RowIndex >= 0)
            {
                printButton.Enabled = false;
                continueButton.Enabled = false;

                DataGridViewRow myRow = gvLoans.Rows[e.RowIndex];
                gvLoans.EndEdit();

                int iDx = _PrintedLoans.FindIndex(l => l.LoanNumber == Utilities.GetIntegerValue(myRow.Cells[colNumber.Name].Value, 0));
                bool bSelected = Utilities.GetBooleanValue(myRow.Cells[colSelect.Name].Value, false);

                if (iDx >= 0)
                {
                    LoanPrinted lp = _PrintedLoans[iDx];
                    lp.bPrinted = bSelected;
                    _PrintedLoans.RemoveAt(iDx);
                    _PrintedLoans.Insert(iDx, lp);
                }
                else
                {
                    LoanPrinted lp = new LoanPrinted()
                    {
                        bPrinted = bSelected,
                        LoanNumber = Utilities.GetIntegerValue(myRow.Cells[colNumber.Name].Value, 0)
                    };
                    _PrintedLoans.Add(lp);
                }
            }
            //           }
        }

        private void gvLoans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == gvLoans.Columns[colSelect.Name].Index)
            {
                gvLoans.EndEdit();

                if (Utilities.GetBooleanValue(gvLoans.Rows[e.RowIndex].Cells[colSelect.Name].Value, false))
                    _RowsSelected++;
                else
                    _RowsSelected--;

                _RowsSelected = _RowsSelected < 0 ? 0 : _RowsSelected;

                totalSelectedPfiLabel.Text = _RowsSelected.ToString();
                printButton.Enabled = _RowsSelected > 0;
            }
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            SelectGridRows(true);
            UpdateCounts();
        }

        private void deselectAllButton_Click(object sender, EventArgs e)
        {
            SelectGridRows(false);
            UpdateCounts();
        }

        private void SelectGridRows(bool bSelect)
        {
            _RowsSelected = 0;

            foreach (DataGridViewRow myRow in gvLoans.Rows)
            {
                _RowsSelected++;

                if (!myRow.Cells[colSelect.Name].ReadOnly)
                {
                    myRow.Cells[colSelect.Name].Value = bSelect;
                    _RowsSelected = bSelect ? _RowsSelected : _RowsSelected - 1;
                }
                else
                {
                    _RowsSelected--;
                    myRow.Cells[colSelect.Name].Value = false;
                }
            }
        }

        private void UpdateCounts()
        {
            totalPanel.Visible = true;
            totalEligiblePfiLabel.Text = gvLoans.Rows.Count.ToString();

            int iRowCount = 0;
            foreach (DataGridViewRow myRow in gvLoans.Rows)
            {
                iRowCount += Convert.ToBoolean(myRow.Cells[colSelect.Name].Value) ? 1 : 0;
            }
            totalSelectedPfiLabel.Text = iRowCount.ToString();

            printButton.Enabled = iRowCount > 0;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            // Have extra enumeration to go ProKnow with only ONE call
            List<CustomerProductDataVO> productObjects = new List<CustomerProductDataVO>();
            List<String> customerNames = new List<String>();

            List<int> tickets = new List<int>(gvLoans.Rows.Count);

            Cursor = Cursors.WaitCursor;
            try
            {
                // improve efficiency -- only return new numbers if these have been previously loaded
                foreach (DataGridViewRow myRow in gvLoans.Rows)
                {
                    if (Convert.ToBoolean(myRow.Cells[colSelect.Name].Value) &&
                        !myRow.Cells[colSelect.Name].ReadOnly)
                    {
                        int iTktNumber =
                        Utilities.GetIntegerValue(myRow.Cells[colNumber.Name].Value, 0);

                        if (iTktNumber != 0)
                            tickets.Add(iTktNumber);
                    }
                }

                string errorCode;
                string errorTxt;
                DateTime dt = Convert.ToDateTime(dateCalendarSearchDate.SelectedDate);

                StoreLoans.Get_PFI_Details(CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber, dt,
                                           tickets, out _PawnLoans, out _PawnApplications, out _CustomerVOs,
                                           out _Purchases, out errorCode, out errorTxt);

                foreach (DataGridViewRow myRow in gvLoans.Rows)
                {
                    if (Convert.ToBoolean(myRow.Cells[colSelect.Name].Value) &&
                        !myRow.Cells[colSelect.Name].ReadOnly)
                    {
                        int iTktNumber =
                        Utilities.GetIntegerValue(myRow.Cells[colNumber.Name].Value, 0);

                        string customer = (string)myRow.Cells[4].Value;
                        PawnLoan pawnLoan = null;

                        if (myRow.Cells[colType.Name].Value.Equals("LOAN"))
                        {
                            if (_PawnLoans != null)
                            {
                                pawnLoan = (from ploan in _PawnLoans
                                            where
                                            ploan.TicketNumber == iTktNumber &&
                                            ploan.ProductType == ProductType.PAWN.ToString()
                                            select ploan).FirstOrDefault();
                            }

                            if (pawnLoan != null)
                            {
                                LoanPrinted loanPrinted =
                                _PrintedLoans.Find(
                                    l => l.LoanNumber == iTktNumber && l.RefType == 1);
                                loanPrinted.bPrinted = true;
                                _PrintedLoans.RemoveAll(
                                    l => l.LoanNumber == iTktNumber && l.RefType == 1);
                                _PrintedLoans.Add(loanPrinted);
                                productObjects.Add(pawnLoan);
                                customerNames.Add(customer);
                            }
                        }
                        else
                        {
                            PurchaseVO purchaseObj = null;
                            if (_Purchases != null)
                            {
                                purchaseObj = (from purchase in _Purchases where purchase.TicketNumber == iTktNumber select purchase).FirstOrDefault();
                            }
                            if (purchaseObj != null)
                            {
                                LoanPrinted loanPrinted =
                                _PrintedLoans.Find(l => l.LoanNumber == iTktNumber && l.RefType == 2);
                                loanPrinted.bPrinted = true;
                                _PrintedLoans.RemoveAll(l => l.LoanNumber == iTktNumber && l.RefType == 2);
                                _PrintedLoans.Add(loanPrinted);
                                productObjects.Add(purchaseObj);
                                customerNames.Add(customer);
                            }
                        }
                    }
                }
                if (productObjects.Count() > 0)
                {
                    ProcessingMessage processingForm = new ProcessingMessage("Please wait while we generate report.");
                    try
                    {
                        processingForm.Show();

                        this.Cursor = Cursors.WaitCursor;

                        //----- Tracy 12/15/2010
                        List<int> lstTicketNumbers = new List<int>();
                        List<string> lstRefTypes = new List<string>();
                        string sErrorCode;
                        string sErrorText;

                        foreach (CustomerProductDataVO o in productObjects)
                        {
                            lstTicketNumbers.Add(o.TicketNumber);
                            lstRefTypes.Add(o.ProductType == "PAWN" ? "1" : "2");
                        }

                        if (lstTicketNumbers.Count > 0 && !StoreLoans.UpdateTempStatus(
                            lstTicketNumbers,
                            StateStatus.PFIW,
                            CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber,
                            true,
                            lstRefTypes,
                            out sErrorCode,
                            out sErrorText))

                            MessageBox.Show("Error updating PFI Verify.  " + sErrorText, "PFI Verification Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //------- Tracy 12/15/2010

                        var context = new PickingSlipHelper().GetPickingSlipReportContext(productObjects, customerNames, true);
                        var reportObject = new ReportObject();
                        reportObject.ReportTempFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
                        reportObject.CreateTemporaryFullName();
                        context.ReportObject = reportObject;
                        var pickingSlip = new PickingSlip(context);

                        if (!pickingSlip.CreateReport())
                        {
                            processingForm.Close();
                            processingForm.Dispose();
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Failed to generate report", "Picking Slip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        //Dictionary<string, string> eDeviceData = new PrintUtilities().GetPrintDeviceData("pfipickslip");
                        if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                            GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                        {
                            if (FileLogger.Instance.IsLogInfo)
                            {
                                FileLogger.Instance.logMessage(LogLevel.INFO, "PFI_SelectLoan", "Printing PFI picking slip on {0}",
                                                               GlobalDataAccessor.Instance.DesktopSession.LaserPrinter);
                            }
                            string strReturnMessage =
                                PrintingUtilities.printDocument(reportObject.ReportTempFileFullName,
                                                                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                                                                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);
                            if (!strReturnMessage.Contains("SUCCESS"))
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print picking slip : " + strReturnMessage);
                        }

                        processingForm.Close();
                        processingForm.Dispose();
                        this.Cursor = Cursors.Default;

                        MessageBox.Show("Printing Complete", "Picking Slip", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception exc)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, exc.Message);
                        processingForm.Close();
                        processingForm.Dispose();
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(exc.Message);
                    }
                }
                //else
                //{
                //    continueButton.Enabled = false;
                //}
                continueButton.Enabled = true;
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error in printing.  Retry. (" + exp.Message + ")", "PFI Printing");
            }

            Cursor = Cursors.Default;
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            gvLoans.EndEdit();
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}