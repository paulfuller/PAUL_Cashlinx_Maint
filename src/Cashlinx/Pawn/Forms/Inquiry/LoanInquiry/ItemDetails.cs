using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Inquiry.InventoryInquiry;
using Pawn.Forms.Report;
using Pawn.Logic;

namespace Pawn.Forms.Inquiry.LoanInquiry
{
    public partial class ItemDetails : Form
    {
        private DataView _theData;
        private DataView _thisItem;
        private int _rowNum;

        private Boolean useDefaultBinding = true;

        private Boolean missingCustomerVendorData = false;
        private Boolean missingMdseData = false;
        private Boolean missingPawnData = false;

        private Boolean showingCustomerMdse = true;     // Customer/Vendor
        private Boolean showingLoan = true;             // Loan/Purchase

        public ItemDetails(DataView theData, int rowIndex)
        {
            _theData = theData;
            _rowNum = rowIndex;

            InitializeComponent();
            #region DATABINDING
            //DataView pawnLoan = theData.DefaultViewManager.CreateDataView(theData.Tables["PAWN_INFO"]);
            // DataView customer = theData.DefaultViewManager.CreateDataView(theData.Tables["PAWN_CUST"]) ;

            //_thisItem = theData.Tables["PAWN_MDSE"].DefaultView[rowIndex];

            try
            {
                // Check for missing data
                if (theData.DataViewManager.DataViewSettings["MDSE"] == null &&
                    theData.DataViewManager.DataViewSettings["PAWN_MDSE"] == null)
                {
                    useDefaultBinding = false;
                    missingMdseData = true;
                }
                if (theData.DataViewManager.DataViewSettings["CUSTOMER"] == null &&
                    theData.DataViewManager.DataViewSettings["PAWN_CUST"] == null)
                {
                    useDefaultBinding = false;
                    missingCustomerVendorData = true;
                }
                if (theData.DataViewManager.DataViewSettings["PAWN"] == null &&
                    theData.DataViewManager.DataViewSettings["PAWN_INFO"] == null)
                {
                    useDefaultBinding = false;
                    missingPawnData = true;
                }

                if (useDefaultBinding)
                {
                    _thisItem = theData[0].CreateChildView("merchandiseRelation");
                }
                else
                {
                    _thisItem = theData;
                }
            }
            catch (ArgumentException)
            {
                // does not contain a relationship named merchandiseRelation, default binding will not work.
                // determine if we have MDSE data, customer data, or pawn(pawn/loan/etc) data 

                useDefaultBinding = false;
            }

            BindingContext[_thisItem].Position = _rowNum;

            cur_loan_nr.DataBindings.Add("Text", _theData, "TICKET_NUMBER");

            if (_theData.Table.Columns.Contains("ORG_DATE") && 
                _theData.Table.Columns.Contains("PRIN_AMOUNT"))
            {
                cust_name.DataBindings.Add("Text", _theData, "customerRelation.CUST_NAME");
                cust_vendor_no.DataBindings.Add("Text", _theData, "customerRelation.CUSTOMERNUMBER");
                cust_id.DataBindings.Add("Text", _theData, "customerRelation.ID");
                cust_vendor_since.DataBindings.Add("Text", _theData, "customerRelation.SINCE", true);
                cust_vendor_since.DataBindings[0].FormatString = "d";
                cust_vendor_dob.DataBindings.Add("Text", _theData, "customerRelation.BIRTHDATE", true);
                cust_vendor_dob.DataBindings[0].FormatString = "d";

                org_date_time.DataBindings.Add("Text", _theData, "ORG_DATE", true);
                org_date_time.DataBindings[0].FormatString = "g";

                loan_purchase_amount.DataBindings.Add("Text", _theData, "PRIN_AMOUNT", true);
                loan_purchase_amount.DataBindings[0].FormatString = "c";
            }

            status_cd.DataBindings.Add("Text", _theData, "STATUS_CD");


            icn.DataBindings.Add("Text", _thisItem, "ICN");
            shop.DataBindings.Add("Text", _theData, "STORENUMBER");
            qty.DataBindings.Add("Text", _thisItem, "QUANTITY");

            item_status.DataBindings.Add("Text", _thisItem, "STATUS_CD");
            //item_status.Text = _thisItem.Row.Field<string>("STATUS_CD");

            //status_dt.Text = string.Format("{0:g}", _thisItem.Row.Field<DateTime>("STATUS_TIME"));
            status_dt.DataBindings.Add("Text", _thisItem, "STATUS_TIME", true);
            status_dt.DataBindings[0].FormatString = "g";

            xref.DataBindings.Add("Text", _thisItem, "XICN");

            cost_per_item.DataBindings.Add("Text", _thisItem, "ITEM_AMT", true);
            cost_per_item.DataBindings[0].FormatString = "c";
            ttl_cost.DataBindings.Add("Text", _thisItem, "AMOUNT", true);
            ttl_cost.DataBindings[0].FormatString = "c";
            disp_type.DataBindings.Add("Text", _thisItem, "DISP_ITEM");
            category.DataBindings.Add("Text", _thisItem, "CAT_CODE");

            sug_loan.DataBindings.Add("Text", _thisItem, "SUGGESTED_LOAN", true);
            sug_loan.DataBindings[0].FormatString = "c";
            sug_retail.DataBindings.Add("Text", _thisItem, "SUGGESTED_RETAIL", true);
            sug_retail.DataBindings[0].FormatString = "c";

            retail_per_item.DataBindings.Add("Text", _thisItem, "RETAIL_PRICE", true);
            retail_per_item.DataBindings[0].FormatString = "c";

            ttl_retail.Text = string.Format ("{0:c}",
                (_thisItem[_rowNum].Row.Field<double>("RETAIL_PRICE") * 
                _thisItem[_rowNum].Row.Field<int>("QUANTITY")));

            disp_date.DataBindings.Add("Text", _thisItem, "DISP_DATE", true);
            disp_date.DataBindings[0].FormatString = "g";

            gun_nbr.DataBindings.Add("Text", _thisItem, "GUN_NUMBER");
            manuf.DataBindings.Add("Text", _thisItem, "MANUFACTURER");
            model.DataBindings.Add("Text", _thisItem, "MODEL");
            serial_nr.DataBindings.Add("Text", _thisItem, "SERIAL_NUMBER");
            descr.DataBindings.Add("Text", _thisItem, "MD_DESC");

            cacc.DataBindings.Add("Text", _thisItem, "ISCACC"); // This is Y/N if this is a cacc item
            rfb_nr.DataBindings.Add("Text", _thisItem, "RFB_NO");
            j_case.DataBindings.Add("Text", _thisItem, "JCASE");
            disp_doc.DataBindings.Add("Text", _thisItem, "DISP_DOC");

            inv_age.DataBindings.Add("Text", _thisItem, "INVENTORY_AGE");
            days_since_sale.DataBindings.Add("Text", _thisItem, "DAYS_SINCE_SALE");
            inv_type.DataBindings.Add("Text", _thisItem, "TRAN_TYPE");
            PopulateUnboundData();

            #endregion

            pageInd.Text = string.Format("Page {0} of {1}", _rowNum + 1, _thisItem.Count);

        }

        #region Intra PageButton UI Events 
        #region Navigation Buttons
        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void nextPage_Click(object sender, EventArgs e)
        {
            if (_rowNum + 1 < _thisItem.Count)
            {
                pageInd.Text = string.Format("Page {0} of {1}", ++_rowNum + 1, _thisItem.Count);

                setEnableDisableFirstLastNextPrevButtons();

                BindingContext[_thisItem].Position = _rowNum;
                ResetBindings();

                PopulateUnboundData();
            }
        }

        private void prevPage_Click(object sender, EventArgs e)
        {
            if (_rowNum > 0)
            {
                pageInd.Text = string.Format("Page {0} of {1}", --_rowNum + 1, _thisItem.Count);

                setEnableDisableFirstLastNextPrevButtons();

                BindingContext[_thisItem].Position = _rowNum;

                ResetBindings();

                PopulateUnboundData();
            }
        }

        private void firstPage_Click(object sender, EventArgs e)
        {
            pageInd.Text = string.Format("Page {0} of {1}", 1, _thisItem.Count);

            _rowNum = 0;

            setEnableDisableFirstLastNextPrevButtons();

            BindingContext[_thisItem].Position = _rowNum;

            ResetBindings();

            PopulateUnboundData();
        }

        private void lastPage_Click(object sender, EventArgs e)
        {
            pageInd.Text = string.Format("Page {0} of {0}", _thisItem.Count);

            _rowNum = _thisItem.Count - 1;

            setEnableDisableFirstLastNextPrevButtons();

            BindingContext[_thisItem].Position = _rowNum;

            ResetBindings();

            PopulateUnboundData();
        }
        #endregion

        #region Navigation to other pages
        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Refine_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void Print_btn_Click(object sender, EventArgs e)
        {
            string dob = string.Empty;
            string since = string.Empty;
            if (_theData != null && _thisItem != null)
            {
                CreateReportObject cRo = new CreateReportObject();
                if (!string.IsNullOrEmpty(cust_vendor_since.Text))
                    since = cust_vendor_since.Text;
                if (!string.IsNullOrEmpty(cust_vendor_dob.Text))
                    dob = cust_vendor_dob.Text;
                cRo.GetItemDetailsReport(_theData, _thisItem, _rowNum, true, since, dob);
            }
            //System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //var dInitDate = Convert.ToDateTime(string.Format("{0:d}", ShopDateTime.Instance.ShopDate));
            //var stoNum = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

            //string fileName = @"extension_detail_report_" + dInitDate.Ticks + ".pdf";

            //string rptDir =
            //    SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.
            //    BaseLogPath;

            //var rpt = new ExtensionReport(
            //    rptDir + "\\" + fileName, stoNum,
            //    GlobalDataAccessor.Instance.CurrentSiteId.StoreName,
            //    dInitDate, "Loan Extension\nTicket # " + cur_loan_nr.Text
            //    );

            //rpt.CreateReport(_theData);
            //this.TopMost = false;
            //System.Windows.Forms.Cursor.Current = Cursors.Default;
            //CashlinxDesktopSession.ShowPDFFile(rptDir + "\\" + fileName, false);







            //DetailInventory = 223,
            //DetailInventoryReport detailInventoryReport = new DetailInventoryReport();
            //detailInventoryReport.reportObject = this.reportObject;
            //Boolean isSuccessful = detailInventoryReport.CreateReport();
            //this.TopMost = false;
            //PawnObjects.VO.Business.ReportObject reportObject = new PawnObjects.VO.Business.ReportObject();
            //reportObject.ReportNumber = (int)PawnUtilities.Shared.ReportIDs.DetailInventory;
            //reportObject.ReportTitle = "Item Detail";
            //PawnReports.Reports.ReportProcessing.DoReport(reportObject);
        }
        #endregion
        #endregion

        private void PopulateUnboundData()
        {
            try
            {
                var loc_aisle = string.Empty;
                var loc_shelf = string.Empty;

                if (!(_thisItem[_rowNum].Row["LOC_AISLE"] is System.DBNull))
                {
                    loc_aisle = string.Format("aisle:{0} ", _thisItem[_rowNum].Row["LOC_AISLE"]);
                }

                if (!(_thisItem[_rowNum].Row["LOC_SHELF"] is System.DBNull))
                {
                    loc_shelf = string.Format("shelf:{0} ", _thisItem[_rowNum].Row["LOC_SHELF"]);
                }

                loctn.Text = string.Format("{0} {1}  {2}", loc_aisle, loc_shelf, _thisItem[_rowNum].Row["LOCATION"]);

                if (useDefaultBinding)
                {
                    return;
                }

                //-----------------------------------------------------------------------------------------------
                String icn = _thisItem[this._rowNum]["ICN"].ToString();
                Icn icnObject = new Icn(icn.Replace(" ", ""));

                if (icnObject.DocumentType == DocumentType.MerchandisePurchaseReceipt &&
                    _thisItem[this._rowNum]["PURCH_TYPE"].ToString() == "V")
                {
                    showingCustomerMdse = false;
                }
                else
                {
                    showingCustomerMdse = true;
                }

                if (missingCustomerVendorData && showingCustomerMdse)
                {
                    Object o = _thisItem[this._rowNum]["CUSTOMERNUMBER"];
                    if (o != System.DBNull.Value)
                    {
                        String custNumber = (String)_thisItem[this._rowNum]["CUSTOMERNUMBER"];

                        if (custNumber.Length != 0)
                        {
                            CustomerVO custObject = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, custNumber);

                            cust_vendor_no.Text = custNumber;
                            if (custObject != null)
                            {
                                cust_name.Text = custObject.CustomerName;

                                if (custObject.CustomerIDs.Count != 0)
                                {
                                    cust_id.Text = custObject.CustomerIDs[0].IdValue;
                                }
                                else
                                {
                                    cust_id.Text = "";
                                }

                                cust_vendor_since.Text = custObject.CustomerSince.ToShortDateString();
                                cust_vendor_dob.Text = custObject.DateOfBirth.ToShortDateString();
                            }
                        }
                    }
                    else
                    {
                        cust_vendor_no.Text = "Unknown";
                        cust_id.Text = "Unknown";
                        cust_name.Text = "Unknown";
                        cust_vendor_since.Text = "Unknown";
                        cust_vendor_dob.Text = "Unknown";

                        //CommonUI.DesktopProcedures.ProcedureUtilities..RetailProcedures.
                    }
                }
                else if (missingCustomerVendorData && !showingCustomerMdse)
                {
                    cust_vendor_no.Text = "Unknown";
                    cust_id.Text = "Unknown";
                    cust_name.Text = "Unknown";
                    cust_vendor_since.Text = "Unknown";
                    cust_vendor_dob.Text = "Unknown";

                    DataSet ds = InventoryInquiries.getVendorDataForIcn(icnObject);

                    if (!ds.IsNullOrEmpty() && ds.Tables.Contains("REVISIONS"))
                    {
                        // Get output number
                        DataTable outputDt = ds.Tables["REVISIONS"];

                        if (outputDt != null && outputDt.IsInitialized &&
                            outputDt.Rows != null && outputDt.Rows.Count > 0)
                        {
                            cust_name.Text = outputDt.Rows[0]["VENDOR_NAME"].ToString();
                            cust_id.Text = outputDt.Rows[0]["VENDOR_ID"].ToString();
                            DateTime dt = DateTime.Parse(outputDt.Rows[0]["EFFECTIVE_DATE"].ToString());
                            cust_vendor_since.Text = dt.ToShortDateString();
                            cust_vendor_no.Text = outputDt.Rows[0]["TAX_ID"].ToString();
                        }
                    }
                }

                if (missingMdseData)
                {
                    System.Diagnostics.Debug.Assert(false, "Missing MDSE data. Need to load from Oracle.");
                }

                if (missingPawnData)
                {
                    org_date_time.Text = "Unknown";
                    loan_purchase_amount.Text = "Unknown";
                    status_cd.Text = "Unknown";

                    cur_loan_nr.Text = _thisItem[this._rowNum]["TICKET_NUMBER"].ToString();

                    DataSet ds = InventoryInquiries.getMinPawnData(new Icn(icn.Replace(" ", "")));

                    if (!ds.IsNullOrEmpty() && ds.Tables.Contains("OUTPUT"))
                    {

                        //Get output number
                        DataTable outputDt = ds.Tables["OUTPUT"];

                        if (outputDt != null && outputDt.IsInitialized &&
                            outputDt.Rows != null && outputDt.Rows.Count > 0)
                        {
                            DataRow dr = outputDt.Rows[0];
                            if (dr != null && dr.ItemArray.Length > 0)
                            {
                                object o_date_made = dr.ItemArray.GetValue(1);
                                if ((String)o_date_made != "null")
                                {
                                    String day = o_date_made.ToString().Substring(0, 2);
                                    String year = o_date_made.ToString().Substring(7, 2);
                                    String month = o_date_made.ToString().Substring(3, 3);
                                    DateTime dt = DateTime.Parse(month + " " + day + " " + year);
                                    this.org_date_time.Text = dt.ToShortDateString();
                                }
                            }

                            dr = outputDt.Rows[1];
                            if (dr != null && dr.ItemArray.Length > 0)
                            {
                                object o_loan_amount = dr.ItemArray.GetValue(1);
                                if (o_loan_amount != null)
                                {
                                    Decimal loan = Decimal.Parse((string)o_loan_amount);
                                    this.loan_purchase_amount.Text = "$" + loan.ToString();
                                }
                            }

                            dr = outputDt.Rows[2];
                            if (dr != null && dr.ItemArray.Length > 0)
                            {
                                object o_loan_status = dr.ItemArray.GetValue(1);
                                if ((String)o_loan_status != "null")
                                {
                                    String status = (string)o_loan_status;
                                    this.status_cd.Text = status;
                                }
                            }
                        }
                    }
                    else
                    {
                        // no Pawn data, assume if it is a purchase
                        showingLoan = false;

                        //cur_loan_nr.Text = _thisItem[this._rowNum]["TICKET_NUMBER"].ToString();

                        ds = InventoryInquiries.getMinPurchaseData(new Icn(icn.Replace(" ", "")));

                        if (!ds.IsNullOrEmpty())
                        {

                            //Get output number
                            DataTable outputDt = ds.Tables["OUTPUT"];

                            if (outputDt != null && outputDt.IsInitialized &&
                                outputDt.Rows != null && outputDt.Rows.Count > 0)
                            {
                                DataRow dr = outputDt.Rows[0];
                                if (dr != null && dr.ItemArray.Length > 0)
                                {
                                    object o_date_made = dr.ItemArray.GetValue(1);
                                    if ((String)o_date_made != "null")
                                    {
                                        String day = o_date_made.ToString().Substring(0, 2);
                                        String year = o_date_made.ToString().Substring(7, 2);
                                        String month = o_date_made.ToString().Substring(3, 3);
                                        DateTime dt = DateTime.Parse(month + " " + day + " " + year);
                                        this.org_date_time.Text = dt.ToShortDateString();
                                    }
                                }

                                dr = outputDt.Rows[1];
                                if (dr != null && dr.ItemArray.Length > 0)
                                {
                                    object o_loan_amount = dr.ItemArray.GetValue(1);
                                    if (o_loan_amount != null)
                                    {
                                        Decimal loan = Decimal.Parse((string)o_loan_amount);
                                        this.loan_purchase_amount.Text = "$" + loan.ToString();
                                    }
                                }

                                dr = outputDt.Rows[2];
                                if (dr != null && dr.ItemArray.Length > 0)
                                {
                                    object o_loan_status = dr.ItemArray.GetValue(1);
                                    if ((String)o_loan_status != "null")
                                    {
                                        String status = (string)o_loan_status;
                                        this.status_cd.Text = status;
                                    }
                                }
                            }
                        }
                    }

                }

                if (showingCustomerMdse)
                {
                    labelCustomerVendorNumber.Text = "Customer #:";

                    labelCustomerVendorDOB.Text = "DOB:";
                    labelCustomerVendorDOB.Visible = true;
                    cust_vendor_dob.Visible = true;

                    labelCustomerVendorSince.Text = "Customer Since:";
                }
                else
                {
                    labelCustomerVendorNumber.Text = "Vendor #";

                    labelCustomerVendorDOB.Visible = false;
                    cust_vendor_dob.Visible = false;

                    labelCustomerVendorSince.Text = "Vendor Since:";
                }

                if (showingLoan)
                {
                    labelLoanPurchaseAmount.Text = "Loan Amount:";
                }
                else
                {
                    labelLoanPurchaseAmount.Text = "Purchase Amount:";
                }
            }
            catch (Exception ex)
            {
                int i = 0;
            }
        }

        private void Revisions_btn_Click(object sender, EventArgs e)
        {
            var revisionFilterSelect = new RevisionsType();
            this.TopMost = false;
            revisionFilterSelect.ShowDialog();

            if (revisionFilterSelect.DialogResult == DialogResult.OK)
            {
                string filter = revisionFilterSelect.getFilterName();

                if (filter.Length != 0)
                {
                    switch (filter)
                    {
                        case ("Cost Revision"):
                            {
                                ItemCostRevisionHistory revisionForm = new ItemCostRevisionHistory(this.icn.Text, this.descr.Text);
                                revisionForm.ShowDialog();
                            }
                            break;
                        case ("Retail Price Change"):
                            {
                                RetailPriceRevisionHistory revisionForm = new RetailPriceRevisionHistory(this.icn.Text, this.descr.Text);
                                revisionForm.ShowDialog();
                            }
                            break;
                        case ("Merchandise Description Change"):
                            {
                                MsdeDescriptionRevisionHistory revisionForm = new MsdeDescriptionRevisionHistory(this.icn.Text, this.descr.Text);
                                revisionForm.ShowDialog();
                            }
                            break;
                        case ("Status Change"):
                            {
                                StatusChangeRevisionHistory revisionForm = new StatusChangeRevisionHistory(this.icn.Text, this.descr.Text);
                                revisionForm.ShowDialog();
                            }
                            break;
                        default:
                            System.Diagnostics.Debug.Assert(false, "Invalid value. Forget to add handler?");
                            break;
                    }

                }
                else
                {
                    MessageBox.Show("No Revision Type Selected");
                }
            }
            else
            {
                MessageBox.Show("No Revision Type Selected");
            }

            this.TopMost = true;
        }

        private void setEnableDisableFirstLastNextPrevButtons()
        {
            if (_theData.Count == 0)
            {
                // No Data, disable everything
                this.firstPage.Enabled = false;
                this.prevPage.Enabled = false;
                this.nextPage.Enabled = false;
                this.lastPage.Enabled = false;
            }
            else
            {
                this.firstPage.Enabled = true;
                this.prevPage.Enabled = true;
                this.nextPage.Enabled = true;
                this.lastPage.Enabled = true;

                if (_rowNum == 0)
                {
                    // First Record, disable going left
                    this.firstPage.Enabled = false;
                    this.prevPage.Enabled = false;
                }

                if (_theData.Count - 1 == _rowNum)
                {
                    // Last Record, disable going right
                    this.nextPage.Enabled = false;
                    this.lastPage.Enabled = false;
                }

            }
        }
    }
}
