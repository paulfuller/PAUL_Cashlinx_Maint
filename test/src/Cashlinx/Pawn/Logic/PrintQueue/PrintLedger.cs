/********************************************************************
* Namespace: CashlinxDesktop.Desktop.PrintQueue
* FileName: PrintLedger
* Prints the ledger report - preliminary or final
* Sreelatha Rengarajan 2/5/2010 Initial version
 * SR 3/16/2010 Changed format according to the new specs
 * SR 4/13/2010 Changed REIN and REOUT
*******************************************************************/

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Pawn.Logic.PrintQueue
{
    public partial class PreliminaryLedger : Form
    {
        private const int MAX_ROWS = 30;
        decimal totalReceiptAmount = 0.0M;
        decimal totalDisbursedAmount = 0.0M;
        public DataTable CashDrawerTransactions
        {
            get;
            set;
        }
        public DataTable TopsTransfers
        {
            get;
            set;
        }

        public LedgerReportType LedgerReportToPrint
        {
            get;
            set;
        }
       
        public PreliminaryLedger()
        {
            InitializeComponent();
        }

        private void Print()
        {
            Bitmap bitMap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            DrawToBitmap(bitMap, new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height));
            PrintingUtilities.PrintBitmapDocument(bitMap, GlobalDataAccessor.Instance.DesktopSession);

        }

        private void PreliminaryLedger_Load(object sender, EventArgs e)
        {
            int pageNo = 1;
            int reminder = 0;
            Math.DivRem(CashDrawerTransactions.Rows.Count, MAX_ROWS, out reminder);
            int numberOfPages = (CashDrawerTransactions.Rows.Count / MAX_ROWS);
            int numOfPages = reminder > 0 ? numberOfPages + 1 : numberOfPages;
            string employeeName = GlobalDataAccessor.Instance.DesktopSession.UserName;

            if (CashDrawerTransactions != null && CashDrawerTransactions.Rows.Count > 0)
            {
                int rowNum = 0;
                labelDateValue.Text = ShopDateTime.Instance.ShopDate.ToShortDateString();
                customLabelHeading.Text = LedgerReportToPrint.ToString() + " Cash Drawer Ledger";
                customLabelFooter.Text = customLabelHeading.Text;
                customLabelEmployee.Text = 
                        employeeName.Substring(employeeName.Length - 5, employeeName.Length) + " - " +
                GlobalDataAccessor.Instance.DesktopSession.FullUserName;
                customLabelStoreName.Text = GlobalDataAccessor.Instance.CurrentSiteId.StoreName + "-" + 
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                customLabelCashDrawerName.Text = GlobalDataAccessor.Instance.DesktopSession.CashDrawerName;
                foreach (DataRow dr in CashDrawerTransactions.Rows)
                {
                    customLabelPageNo.Text = "Page " + pageNo.ToString() + " of " + numOfPages.ToString();
                    decimal transactionAmt = Utilities.GetDecimalValue(dr["ref_amt"]);
                    if (transactionAmt != 0)
                    {
                        //Column 1 Employee Number    
                        string empNo = Utilities.GetStringValue(dr["updatedby"]);
                        CreateNewColumn(empNo, 0, rowNum);
                        //Column 2 Transaction Number
                        string refNumber = Utilities.GetStringValue(dr["ref_number"]);
                        CreateNewColumn(refNumber, 1, rowNum);
                        //Column 3 Previous Transaction Number
                        string prevRefNumber = Utilities.GetStringValue(dr["prev_ticket"]);
                        CreateNewColumn(prevRefNumber, 2, rowNum);
                        //Column 4 Customer Name
                        string customerName = Utilities.GetStringValue(dr["custname"]);
                        CreateNewColumn(customerName, 3, rowNum);
                        //Column 5 Method of Payment
                        string pmtMethod = Utilities.GetStringValue(dr["methodofpmt"]);
                        CreateNewColumn(pmtMethod, 4, rowNum);
                        //Column 6 Status
                        string status = Utilities.GetStringValue(dr["status_cd"]);
                        CreateNewColumn(status, 5, rowNum);
                        //Column 7 Transaction Type
                        string tranType = Utilities.GetStringValue(dr["ref_event"]);
                        CreateNewColumn(tranType, 6, rowNum);
                        //Column 8 Transaction Amount
                        string tranAmt = string.Format("{0:C}", dr["ref_amt"]);
                        CreateNewColumn(tranAmt, 7, rowNum);
                        //Column 9 Receipt Amount
                        string receiptAmt = string.Format("{0:C}", dr["receiptamount"]);
                        CreateNewColumn(receiptAmt, 8, rowNum);
                        //Column 10 Disbursement Amount
                        string disburseAmt = string.Format("{0:C}", dr["disbursedamount"]);
                        CreateNewColumn(disburseAmt, 9, rowNum);
                        totalReceiptAmount += Utilities.GetDecimalValue(dr["receiptamount"], 0);
                        totalDisbursedAmount += Utilities.GetDecimalValue(dr["disbursedamount"], 0);
                        rowNum++;
                        if (rowNum > MAX_ROWS)
                        {
                            Print();
                            tableLayoutPanelTranData.Controls.Clear();
                            rowNum = 0;
                            pageNo++;
                        }
                    }
                }
 
                if (TopsTransfers != null)
                {
                    foreach (DataRow dr in TopsTransfers.Rows)
                    {
                        //Column 1 Employee Number    
                        string empNo = GlobalDataAccessor.Instance.DesktopSession.UserName;
                        CreateNewColumn(empNo, 0, rowNum);
                        //Column 2 Transaction Number
                        const string refNumber = " ";
                        CreateNewColumn(refNumber, 1, rowNum);
                        //Column 3 Previous Transaction Number
                        const string prevRefNumber = "";
                        CreateNewColumn(prevRefNumber, 2, rowNum);
                        //Column 4 Customer Name
                        const string customerName = "Auto Transfer";
                        CreateNewColumn(customerName, 3, rowNum);
                        //Column 5 Method of Payment
                        const string pmtMethod = "CASH";
                        CreateNewColumn(pmtMethod, 4, rowNum);
                        //Column 6 Status
                        const string status = "ACT";
                        CreateNewColumn(status, 5, rowNum);
                        //Column 7 Transaction Type
                        string tranType = Utilities.GetStringValue(dr["operationcode"]);
                        if (tranType == TOPSTRANSFEROPERATIONS.REIN.ToString())
                            CreateNewColumn("XFER From TOPS", 6, rowNum);
                        else
                            CreateNewColumn("XFER To TOPS", 6, rowNum);
                        //Column 8 Transaction Amount
                        string tranAmt = string.Format("{0:C}", dr["amount"]);
                        var receiptAmt = string.Empty;
                        var disburseAmt = string.Empty;
                        if (tranType == TOPSTRANSFEROPERATIONS.REIN.ToString())
                        {
                            receiptAmt = tranAmt;
                            totalReceiptAmount += Utilities.GetDecimalValue(dr["amount"], 0);
                        }
                        else
                        {
                            disburseAmt = tranAmt;
                            totalDisbursedAmount += Utilities.GetDecimalValue(dr["amount"], 0);
                        }
                        CreateNewColumn("", 7, rowNum);
                        //Column 9 Receipt Amount
                        CreateNewColumn(receiptAmt, 8, rowNum);
                        //Column 10 Disbursement Amount
                        CreateNewColumn(disburseAmt, 9, rowNum);
                        rowNum++;


                        if (rowNum > MAX_ROWS)
                        {
                            Print();
                            tableLayoutPanelTranData.Controls.Clear();
                            rowNum = 0;
                            pageNo++;
                        }
                    }


                }
                if (rowNum > 0)
                {
                    ShowEndOfReport();
                    Print();
                }
                Application.DoEvents();
                this.Close();
            }
        }

        private void CreateNewColumn(string text, int colNo, int rowNo)
        {
            Label newLabel = new Label();
            newLabel.AutoSize = true;
            newLabel.Font = this.labelEmpNoHeading.Font;
            newLabel.Text = text;
            newLabel.Anchor = AnchorStyles.None;
            tableLayoutPanelTranData.Controls.Add(newLabel, colNo, rowNo);
        }


        private void ShowEndOfReport()
        {
            //First Line
            GroupBox grpBox1 = new GroupBox();
            grpBox1.Location = new System.Drawing.Point(11, 436);
            grpBox1.Name = "groupBox1";
            grpBox1.Size = new System.Drawing.Size(867, 2);
            grpBox1.TabIndex = 16;
            grpBox1.TabStop = false;
            grpBox1.Text = "groupBox1";
            grpBox1.Visible = true;
            this.Controls.Add(grpBox1);
            //Show total amounts
            Label labelFinalTotalHeading = new Label();
            labelFinalTotalHeading.AutoSize = true;
            labelFinalTotalHeading.Location = new System.Drawing.Point(230, 452);
            labelFinalTotalHeading.Name = "labelFinalTotalHeading";
            labelFinalTotalHeading.Size = new System.Drawing.Size(182, 13);
            labelFinalTotalHeading.TabIndex = 17;
            labelFinalTotalHeading.Text = "TOTAL FOR THIS CASH DRAWER:";
            this.Controls.Add(labelFinalTotalHeading);
            CustomLabel customLabelTotRecptAmount = new CustomLabel();
            customLabelTotRecptAmount.AutoSize = true;
            customLabelTotRecptAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            customLabelTotRecptAmount.Location = new System.Drawing.Point(732, 452);
            customLabelTotRecptAmount.Name = "customLabelTotRecptAmount";
            customLabelTotRecptAmount.Size = new System.Drawing.Size(35, 13);
            customLabelTotRecptAmount.TabIndex = 18;
            customLabelTotRecptAmount.Text = string.Format("{0:C}",totalReceiptAmount);
            this.Controls.Add(customLabelTotRecptAmount);
            CustomLabel customLabelTotDisbursedAmt = new CustomLabel();
            customLabelTotDisbursedAmt.AutoSize = true;
            customLabelTotDisbursedAmt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            customLabelTotDisbursedAmt.Location = new System.Drawing.Point(821, 452);
            customLabelTotDisbursedAmt.Name = "customLabelTotDisbursedAmt";
            customLabelTotDisbursedAmt.Size = new System.Drawing.Size(35, 13);
            customLabelTotDisbursedAmt.TabIndex = 19;
            customLabelTotDisbursedAmt.Text = string.Format("{0:C}",totalDisbursedAmount);
            this.Controls.Add(customLabelTotDisbursedAmt);
            //Ending line
            GroupBox groupBox2 = new GroupBox();
            groupBox2.Location = new System.Drawing.Point(15, 480);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(867, 2);
            groupBox2.TabIndex = 20;
            groupBox2.TabStop = false;
            groupBox2.Text = "groupBox2";
            groupBox2.Visible = true;
            this.Controls.Add(groupBox2);
            this.customLabelEndHeading.Visible = true;
            this.groupBoxFinal.Visible = true;
        }

 

    
    }
}
