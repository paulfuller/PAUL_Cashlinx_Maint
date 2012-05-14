using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Support.Logic;
using Support.Libraries.Objects.PDLoan;

namespace Support.Forms.Customer.Products.ProductHistory
{
    public partial class PDLLoanStats : Form
    {
        private List<PDLoan> PDLLoanList = CashlinxPawnSupportSession.Instance.PDLoanKeys;

        /*__________________________________________________________________________________________*/
        public PDLLoanStats()
        {
            InitializeComponent();
        }
        #region GETDATA
        /*__________________________________________________________________________________________*/
        private void MapPDL_LoanStatsFromProperties()
        {
            foreach (var PDLRecord in PDLLoanList)
            {
                PDLoanDetails Record;
                Record = PDLRecord.GetPDLoanDetails;

                this.lblCustomerSSNData.Text = Record.CustomerSSN;
                this.lblUWNameData.Text = Record.UWName;
                this.lblLoanRequestDateData.Text = Record.LoanRequestDate ==  DateTime.MaxValue ? "" : (Record.LoanRequestDate).FormatDate();

                this.lblLoanAmtData.Text = Record.LoanAmt.ToString();
                this.lblLoanPayOffAmtData.Text = Record.LoanPayOffAmt.ToString();
                this.lblActualLoanAmtData.Text = Record.ActualLoanAmt.ToString();

                this.TxbLoanNumberOrig.Text = Record.LoanNumberOrig;
                this.TxbLoanNumberPrev.Text = Record.LoanNumberPrev;
                this.TxbLoanRolloverNotes.Text = Record.LoanRolloverNotes;
                this.TxbLoanRollOverAmt.Text = Record.LoanRollOverAmt.ToString();
                this.TxbRevokeACH.Text = Record.RevokeACH == Record.RevokeACH ? "Yes" : "No";
                this.TxbXPPAvailable.Text =  Record.XPPAvailable == Record.XPPAvailable ? "Yes" : "No";
                this.TxbActualFinanceChrgAmt.Text = Record.ActualFinanceChrgAmt.ToString();
                this.TxbAcutalServiceChrgAmt.Text = Record.AcutalServiceChrgAmt.ToString();
                this.TxbAccruedFinanceAmt.Text = Record.AccruedFinanceAmt.ToString();
                this.TxbLateFeeAmt.Text = Record.LateFeeAmt.ToString();
                this.TxbNSFFeeAmt.Text = Record.NSFFeeAmt.ToString();
                this.TxbACHWaitingToClear.Text = Record.ACHWaitingToClear; // == Record.ACHWaitingToClear ? "Yes" : "No";
                this.TxbEstRolloverAmt.Text = Record.EstRolloverAmt.ToString();

            }
        }
        /*__________________________________________________________________________________________*/
        private void MapPDL_xppLoanScheduleFromProperties()
        {
            foreach (var PDLRecord in PDLLoanList)
            {
                List <PDLoanXPPScheduleList> Records;
                Records = PDLRecord.GetPDLoanXPPScheduleList;

                for ( int index = 0; index < Records.Count(); index++)
                {
                    int gvIdx = DGVxxpPaySchedule.Rows.Add();
                    DataGridViewRow LineItem = DGVxxpPaySchedule.Rows[gvIdx];
                    LineItem.Cells["DgvColxppLineItem"].Value = "StartDate";
                    LineItem.Cells["DgvColxppPaymentSeqNumber"].Value = Records[index].xppPaymentSeqNumber.ToString();
                    LineItem.Cells["DgvColxppPaymentNumber"].Value = Records[index].xppPaymentNumber;
                    LineItem.Cells["DgvColxppPaymentDate"].Value = Records[index].xppDate == DateTime.MaxValue ? "" : (Records[index].xppDate).FormatDate();
                    LineItem.Cells["DgvColxppPaymentAmt"].Value = Records[index].xppAmount.ToString();

                }

            }
        }

        /*__________________________________________________________________________________________*/
        private void MapPDL_EventsFromProperties()
        {
            //DgvHistoryLoanEvents
            foreach (var PDLRecord in PDLLoanList)
            {
                PDLoanDetails Record;
                Record = PDLRecord.GetPDLoanDetails;

                this.TxbOriginationDate.Text = Record.OrginationDate == DateTime.MaxValue ? "" : (Record.OrginationDate).FormatDate();

                this.TxbDueDate.Text = Record.DueDate == DateTime.MaxValue ? "" : (Record.DueDate).FormatDate();
                this.TxbOrigDepDate.Text = Record.OrigDepDate == DateTime.MaxValue ? "" : (Record.OrigDepDate).FormatDate();
                this.TxbExtendedDate.Text = Record.ExtendedDate == DateTime.MaxValue ? "" : (Record.ExtendedDate).FormatDate();
                this.TxbLastUpdatedBy.Text = Record.LastUpdatedBy;
                this.TxbShopNo.Text = Record.ShopNo;
                this.TxbShopName.Text = Record.ShopName;
                this.TxbShopState.Text = Record.ShopState;

            }

        }
        /*__________________________________________________________________________________________*/
        private void MapPDL_HistoryFromProperties()
        {
            foreach (var PDLRecord in PDLLoanList)
            {
                List<PDLoanHistoryList> Records;
                Records = PDLRecord.GetPDLoanHistoryList;

                for (int index = 0; index < Records.Count(); index++)
                {
                    int gvIdx = DgvHistoryLoanEvents.Rows.Add();
                    DataGridViewRow GridRow = DgvHistoryLoanEvents.Rows[gvIdx];
                    GridRow.Cells["DgvColHistDate"].Value = Records[index].Date == DateTime.MaxValue ? "" : (Records[index].Date).FormatDate(); ;
                    GridRow.Cells["DgvColHistEventType"].Value = Records[index].EventType;
                    GridRow.Cells["DgvColHistDetails"].Value = Records[index].Details;
                    GridRow.Cells["DgvColHistAmount"].Value = Records[index].Amount.ToString();
                    GridRow.Cells["DgvColHistSource"].Value = Records[index].Source;
                    GridRow.Cells["DgvColHistReceipt"].Value = Records[index].Receipt;
                }
            }
        }
        #endregion

    }
}
