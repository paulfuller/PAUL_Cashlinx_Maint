using Common.Controllers.Database.Couch;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Inquiry.LoanInquiry;
using Pawn.Logic;
using Reports.Inquiry;
using Document = Common.Libraries.Objects.Doc.Document;

namespace Pawn.Forms.Inquiry.Retail
{
    public partial class RetailDetails : Form
    {
        private DataView _theData;
        private DataView _selectedData;
        private DataSet _dataSet;
        private int _rowNum;
        private int _currentTicketNumber;

        public RetailDetails(DataView s, int rowIdx)
        {
            InitializeComponent();

            #region DATABINDING
            _theData = s;
            _rowNum = rowIdx;

            pageInd.Text = string.Format(pageInd.Text, rowIdx + 1, _theData.Count);

            _selectedData = _theData.DataViewManager.CreateDataView(_theData.Table);
            BindingContext[_selectedData].Position = _rowNum;

            msr_lbl.DataBindings.Add("Text", _selectedData, "TICKET_NUMBER");
            layaway_lbl.DataBindings.Add("Text", _selectedData, "LAYAWAY_TICKET");
            user_id_lbl.DataBindings.Add("Text", _selectedData, "ENT_ID");
            cash_drawer_lbl.DataBindings.Add("Text", _selectedData, "CASHDRAWER");

            date_time_lbl.DataBindings.Add("Text", _selectedData, "TIME_MADE", true);
            date_time_lbl.DataBindings[0].FormatString = "g";
            terminal_id_lbl.DataBindings.Add("Text", _selectedData, "TTY");

            sales_tax_amount_lbl.DataBindings.Add("Text", _selectedData, "TAX", true);
            sales_tax_amount_lbl.DataBindings[0].FormatString = "c";
            shop_number_lbl.DataBindings.Add("Text", _selectedData, "STORENUMBER");
            status_lbl.DataBindings.Add("Text", _selectedData, "STATUS_CD");

            getAndBindDetails();

            #endregion

            if (_theData.Count > 0)
            {
                nextPage.Enabled = true;
                lastPage.Enabled = true;
                firstPage.Enabled = true;
            }


            if (_rowNum > 0)
                prevPage.Enabled = true;
        }

        private void History_dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 1)
                return;

            int ticketNumber = Convert.ToInt32(History_dg.Rows[e.RowIndex].Cells[0].Value.ToString());
            List<CouchDbUtils.PawnDocInfo> pawnDocs;
            string errString = String.Empty;

            //If a legit ticket number was pulled, then continue.
            if (ticketNumber <= 0)
                return;

            //Instantiate docinfo which will return info we need to be able to 
            //call reprint ticket.
            var docInfo = new CouchDbUtils.PawnDocInfo();
            docInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.RECEIPT);
            docInfo.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            docInfo.TicketNumber = ticketNumber;
            int receiptNumber = 0;
            if (!string.IsNullOrEmpty(History_dg.Rows[e.RowIndex].Cells[1].Value.ToString()))
                receiptNumber = Convert.ToInt32(History_dg.Rows[e.RowIndex].Cells[1].Value.ToString());
            docInfo.ReceiptNumber = receiptNumber;

            //Use couch DB to get the document.
            if (!CouchDbUtils.GetPawnDocument(GlobalDataAccessor.Instance.OracleDA, GlobalDataAccessor.Instance.CouchDBConnector,
                                                docInfo, false, out pawnDocs, out errString))
            {
                return;
            }

            if (pawnDocs == null)
                return;

            //Find that there is a document with a receipt.
            var results = from p in pawnDocs
                          where p.DocumentType ==
                              Document.DocTypeNames.RECEIPT
                          select p;
            if (results.Any())
            {
                //Get the only one receipt that should exist.
                docInfo = results.First();

                //Call the reprint screen
                ViewPrintDocument docViewPrint = new ViewPrintDocument("Receipt# ", docInfo.ReceiptNumber.ToString(), docInfo.StorageId, docInfo.DocumentType, docInfo);
                this.TopMost = false; // "this" is modal, must not be on top to display dialog
                docViewPrint.ShowDialog();
                this.TopMost = true;
            }
        }

        private void getAndBindDetails()
        {
            try
            {

                int.TryParse(_selectedData[_rowNum]["TICKET_NUMBER"].ToString(), out _currentTicketNumber);
                _dataSet = RetailInquiry.getDetail(_currentTicketNumber);

                ItemsList_dg.DataSource = null;
                if (_dataSet.Tables.Contains("MDSE_INFO"))
                {
                    ItemsList_dg.AutoGenerateColumns = false;
                    ItemsList_dg.DataSource = _dataSet.Tables["MDSE_INFO"];
                }

                History_dg.DataSource = null;
                if (_dataSet.Tables.Contains("HISTORY_INFO"))
                {
                    History_dg.AutoGenerateColumns = false;
                    DataTable dt = _dataSet.Tables["HISTORY_INFO"];
                    dt.Columns.Add("TOTAL_AMOUNT", typeof(double));
                    foreach (DataRow row in dt.Rows)
                    {
                        Decimal sale = 0.0m;
                        Decimal tax = 0.0m;
                        Decimal.TryParse(row["AMOUNT"].ToString(), out sale);
                        Decimal.TryParse(row["SALES_TAX"].ToString(), out tax);

                        row["TOTAL_AMOUNT"] = sale + tax;

                    }
                    History_dg.DataSource = _dataSet.Tables["HISTORY_INFO"];

                    History_dg.Columns["TranAmount"].DefaultCellStyle.Format = "c";
                    History_dg.Columns["SalesTax"].DefaultCellStyle.Format = "c";
                    History_dg.Columns["TotalAmount"].DefaultCellStyle.Format = "c";
                }

                cust_name.Text = String.Empty;
                cust_dob.Text = String.Empty;
                cust_no.Text = String.Empty;
                cust_id.Text = String.Empty;
                cust_since.Text = String.Empty;
                if (_dataSet.Tables.Contains("CUSTOMER_INFO"))
                {
                    cust_name.Text = _dataSet.Tables["CUSTOMER_INFO"].DefaultView[0]["CUST_NAME"].ToString();
                    cust_dob.Text = _dataSet.Tables["CUSTOMER_INFO"].DefaultView[0]["DOB"].ToString();
                    cust_no.Text = _dataSet.Tables["CUSTOMER_INFO"].DefaultView[0]["CUSTOMERNUMBER"].ToString();
                    cust_id.Text = _dataSet.Tables["CUSTOMER_INFO"].DefaultView[0]["ID"].ToString();
                    cust_since.Text = System.DateTime.Parse(_dataSet.Tables["CUSTOMER_INFO"].DefaultView[0]["SINCE"].ToString()).ToString("MM/dd/yyyy");
                }

                txtTenderTypes.Text = String.Empty;
                if (_dataSet.Tables.Contains("TENDER_INFO"))
                {
                    foreach (DataRow row in _dataSet.Tables["TENDER_INFO"].Rows)
                    {
                        txtTenderTypes.Text += row["TENDER_TYPE"] + " $" + row["REF_AMT"] + Environment.NewLine;
                    }
                }

                //-------------------------------
                if (_dataSet.Relations.Contains("customerRelation"))
                {
                    _dataSet.Relations.Remove("customerRelation");
                }

                if (_dataSet.Tables.Contains("CUSTOMER_INFO") &&
                    _dataSet.Tables.Contains("MDSE_INFO"))
                {

                    _dataSet.Relations.Add("customerRelation",
                              _dataSet.Tables["CUSTOMER_INFO"].Columns["CUSTOMERNUMBER"],
                              _dataSet.Tables["MDSE_INFO"].Columns["CUSTOMERNUMBER"], false);
                }

                Double totalAmountWithTax = ((double)_selectedData[_rowNum]["AMOUNT"]) + ((double)_selectedData[_rowNum]["TAX"]);
                total_sale_amount_lbl.Text = totalAmountWithTax.ToString("c");
            }
            catch (Exception)
            {
                return;
            }
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Refine_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ItemsList_dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var key = _selectedData[_rowNum].Row.Field<int>("TICKET_NUMBER");

            try
            {
                if (e.ColumnIndex == 0)
                {

                    this.Visible = false;

                    DataView itemView = _dataSet.DefaultViewManager.CreateDataView(_selectedData.Table);

                    itemView.RowFilter = "TICKET_NUMBER=" + key + "";

                    Form details = new ItemDetails(itemView, e.RowIndex);
                    details.ShowDialog();

                    switch (details.DialogResult)
                    {
                        case DialogResult.OK:
                            this.Visible = true;
                            break;

                        default:
                            this.DialogResult = details.DialogResult;
                            this.Close();
                            break;
                    }
                }
            }
            catch (Exception)
            {
                this.Visible = true;
                MessageBox.Show("Unable to display data for " + key.ToString());
            }
        }

        private void Print_btn_Click(object sender, EventArgs e)
        {
            var rpt = new RetailSaleInquiryDetailReport(PdfLauncher.Instance);

            ReportObject reportObject = new ReportObject();

            reportObject.ReportTempFileFullName = "Retail Sale Inquiry Detail";

            rpt.ReportObject = reportObject;

            var retailSaleHistory = GetRetailSaleHistory();
            var retailSaleTender = GetRetailSaleTender();
            var retailSaleMerchandise = GetRetailSaleMerchandise();
            var retailSaleCustomer = GetRetailSaleCustomer();
            var retailSaleListing = GetRetailSaleListing();

            rpt.CreateReport(retailSaleListing, retailSaleCustomer, retailSaleMerchandise, retailSaleTender, retailSaleHistory);
        }

        private List<ReportObject.RetailSaleHistory> GetRetailSaleHistory()
        {
            if (_dataSet.Tables["HISTORY_INFO"] != null)
            {
                var retailSaleHistoryList = new List<ReportObject.RetailSaleHistory>();

                ReportObject.RetailSaleHistory retailSale;
                foreach (DataRow dr in _dataSet.Tables["HISTORY_INFO"].Rows)
                {
                    retailSale = new ReportObject.RetailSaleHistory()
                    {
                        receiptNumber = Convert.ToInt32(dr["RECEIPT_NUMBER"]),
                        receiptDetailNumber = Convert.ToInt32(dr["RECEIPTDETAIL_NUMBER"]),
                        refEvent = dr["REF_EVENT"].ToString(),
                        refTime = Convert.ToDateTime(dr["REF_TIME"]),
                        amount = Convert.ToDecimal(dr["AMOUNT"]),
                        entId = dr["ENT_ID"].ToString(),
                        tax = Convert.ToDecimal(dr["SALES_TAX"])
                    };

                    retailSaleHistoryList.Add(retailSale);
                }

                return retailSaleHistoryList;
            }
            else
            {
                return null;
            }
        }

        private List<ReportObject.RetailSaleTender> GetRetailSaleTender()
        {
            if (_dataSet.Tables["TENDER_INFO"] != null)
            {
                var retailSaleTenderList = new List<ReportObject.RetailSaleTender>();

                ReportObject.RetailSaleTender retailSaleTender;
                foreach (DataRow dr in _dataSet.Tables["TENDER_INFO"].Rows)
                {
                    retailSaleTender = new ReportObject.RetailSaleTender()
                    {
                        tenderType = dr["tender_type"].ToString(),
                        refAmount = Convert.ToDecimal(dr["REF_AMT"])
                    };

                    retailSaleTenderList.Add(retailSaleTender);
                }

                return retailSaleTenderList;
            }
            return null;
        }

        private List<ReportObject.RetailSaleMerchandise> GetRetailSaleMerchandise()
        {
            if (_dataSet.Tables["MDSE_INFO"] != null)
            {
                var retailSaleMerchandiseList = new List<ReportObject.RetailSaleMerchandise>();

                ReportObject.RetailSaleMerchandise retailSaleMechandise;
                foreach (DataRow dr in _dataSet.Tables["MDSE_INFO"].Rows)
                {
                    retailSaleMechandise = new ReportObject.RetailSaleMerchandise()
                    {
                        ticketNumber = Convert.ToInt32(dr["disp_doc"]),
                        shortCode = dr["shortCode"].ToString(),
                        ICN = dr["ICN"].ToString(),
                        description = dr["md_desc"].ToString(),
                        isLocated = dr["loc"].ToString(),
                        location = dr["location"].ToString(),
                        locationShelf = dr["loc_shelf"].ToString(),
                        locationAisle = dr["loc_aisle"].ToString(),
                        amount = !string.IsNullOrEmpty(dr["amount"].ToString()) ? Convert.ToDecimal(dr["amount"]) : 0m,
                        itemAmount = !string.IsNullOrEmpty(dr["item_amt"].ToString()) ? Convert.ToDecimal(dr["item_amt"]) : 0m,
                        gunNumber = dr["GUN_NUMBER"].ToString(),
                        jcase = dr["jcase"].ToString(),
                        statusCD = dr["status_cd"].ToString(),
                        statusDate = Convert.ToDateTime(dr["status_time"]),
                        quantity = Convert.ToInt32(dr["quantity"]),
                        pfiAmount = !string.IsNullOrEmpty(dr["pfi_amount"].ToString()) ? Convert.ToDecimal(dr["pfi_amount"]) : 0m,
                        retailPrice = !string.IsNullOrEmpty(dr["retail_price"].ToString()) ? Convert.ToDecimal(dr["retail_price"]) : 0m,
                        xicn = dr["xicn"].ToString(),
                        categoryCode = Convert.ToInt32(dr["cat_code"]),
                        rfbNumber = Convert.ToInt32(dr["rfb_no"]),
                        rfbStore = Convert.ToInt32(dr["rfb_store"]),
                        dispostionDate = Convert.ToDateTime(dr["disp_date"]),
                        dispostionType = dr["disp_type"].ToString(),
                        dispostionItem = Convert.ToInt32(dr["disp_item"]),
                        dispostionDoc = Convert.ToInt32(dr["disp_doc"]),
                        manufacturer = dr["manufacturer"].ToString(),
                        model = dr["model"].ToString(),
                        serialNumber = dr["serial_number"].ToString(),
                        isCacc = dr["isCacc"].ToString(),
                        transactionType = dr["tran_type"].ToString(),
                        invAge = Convert.ToInt32(dr["inventory_age"]),
                        daysSinceSale = Convert.ToInt32(dr["days_since_sale"]),
                        categoryDescription = dr["CAT_DESC"].ToString(),
                        customerNumber = dr["CUSTOMERNUMBER"].ToString(),
                        purchType = dr["PURCH_TYPE"].ToString(),
                        weight = dr["WEIGHT"].ToString(),
                        caccLevel = dr["CACC_LEVEL"].ToString()
                    };


                    retailSaleMerchandiseList.Add(retailSaleMechandise);
                }

                return retailSaleMerchandiseList;
            }
            else
            {
                return null;
            }
        }

        private ReportObject.RetailSaleCustomer GetRetailSaleCustomer()
        {
            if (_dataSet.Tables["CUSTOMER_INFO"] != null)
            {
                DataRow dr = _dataSet.Tables["CUSTOMER_INFO"].Rows[0];
                return new ReportObject.RetailSaleCustomer()
                {
                    ticketNumber = Convert.ToInt32(dr["ticket_number"]),
                    customerNumber = dr["customernumber"].ToString(),
                    customerName = dr["cust_name"].ToString(),
                    customerId = dr["id"].ToString(),
                    customerAddress = dr["address"].ToString(),
                    customerAddress2 = dr["address2"].ToString(),
                    city = dr["city"].ToString(),
                    state = dr["state"].ToString(),
                    zipCode = dr["zipCode"].ToString(),
                    DOB = Convert.ToDateTime(dr["dob"]),
                    weight = dr["wt"].ToString(),
                    height = dr["ht"].ToString(),
                    gender = dr["gender"].ToString(),
                    race = dr["race"].ToString(),
                    since = Convert.ToDateTime(dr["since"]),
                    eyeColorCode = dr["eye_color_code"].ToString(),
                    hairColorCode = dr["hair_color_code"].ToString(),
                    phone = dr["phone"].ToString()
                };
            }
            else
            {
                return new ReportObject.RetailSaleCustomer();
            }
        }

        private ReportObject.RetailSaleListing GetRetailSaleListing()
        {
            if (_theData.Table != null)
            {
                DataRow dr = _theData.Table.Rows[0];
                return new ReportObject.RetailSaleListing()
                {
                    shopNumber = Convert.ToInt32(dr["storenumber"]),
                    ticketNumber = Convert.ToInt32(dr["ticket_number"]),
                    date = Convert.ToDateTime(dr["time_made"]),
                    saleAmount = Convert.ToDecimal(dr["amount"]),
                    cost = Convert.ToDecimal(dr["cost"]),
                    status = dr["status_cd"].ToString(),
                    entId = dr["ent_id"].ToString(),
                    userId = dr["createdby"].ToString(),
                    terminalId = dr["tty"].ToString(),
                    cashDrawer = dr["cashdrawer"].ToString(),
                    tax = Convert.ToDecimal(dr["tax"]),
                    createType = dr["create_type"].ToString(),
                    layawayTicketNumber = dr["layaway_ticket"].ToString() != String.Empty ? Convert.ToInt32(dr["layaway_ticket"]) : 0,
                };
            }
            else
            {
                return new ReportObject.RetailSaleListing();
            }
        }

        private void prevPage_Click(object sender, EventArgs e)
        {
            if (_rowNum - 1 >= 0 && _theData.Count > 0)
            {
                pageInd.Text = string.Format("Page {0} of {1}", --_rowNum + 1, _theData.Count);
                BindingContext[_selectedData].Position = _rowNum;

                setEnableDisableFirstLastNextPrevButtons();

                ResetBindings();
                getAndBindDetails();
            }
        }

        private void lastPage_Click(object sender, EventArgs e)
        {
            if (_theData.Count > 0)
            {
                pageInd.Text = string.Format("Page {0} of {0}", _theData.Count);

                _rowNum = _theData.Count - 1;
                BindingContext[_selectedData].Position = _rowNum;
                //var pawnTicketNumber = _theData[_rowNum].Row.Field<int>("TICKET_NUMBER").ToString();
                //_theData.DefaultViewManager.DataViewSettings["PAWN_INFO"].RowFilter  
                //_selectedData.RowFilter = "TICKET_NUMBER='" + pawnTicketNumber + "'";

                setEnableDisableFirstLastNextPrevButtons();

                ResetBindings();
                getAndBindDetails();
            }

        }

        private void nextPage_Click(object sender, EventArgs e)
        {
            if (_rowNum + 1 < _theData.Count)
            {
                pageInd.Text = string.Format("Page {0} of {1}", ++_rowNum + 1, _theData.Count);
                BindingContext[_selectedData].Position = _rowNum;
                //var pawnTicketNumber = _theData[_rowNum].Row.Field<int>("TICKET_NUMBER").ToString();
                //_theData.DefaultViewManager.DataViewSettings["PAWN_INFO"].RowFilter  
                // _selectedData.RowFilter = "TICKET_NUMBER='" + pawnTicketNumber + "'";

                setEnableDisableFirstLastNextPrevButtons();

                ResetBindings();
                getAndBindDetails();
            }

        }

        private void firstPage_Click(object sender, EventArgs e)
        {
            if (_theData.Count > 0)
            {
                pageInd.Text = string.Format("Page {0} of {1}", 1, _theData.Count);

                _rowNum = 0;
                BindingContext[_selectedData].Position = _rowNum;
                //var pawnTicketNumber = _theData[_rowNum].Row.Field<int>("TICKET_NUMBER").ToString();
                //_theData.DefaultViewManager.DataViewSettings["PAWN_INFO"].RowFilter  
                //_selectedData.RowFilter = "TICKET_NUMBER='" + pawnTicketNumber + "'";

                setEnableDisableFirstLastNextPrevButtons();

                ResetBindings();
                getAndBindDetails();
            }
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
