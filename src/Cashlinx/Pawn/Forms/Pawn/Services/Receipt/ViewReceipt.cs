using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.Receipt
{
    public partial class ViewReceipt : Form
    {
        public const string CASHAMERICATXT = "Cash America Intl, Inc.";
        public const string RECEIPTNUM = "Receipt # ";
        public const string CUSTOMERLABEL = "Customer: ";
        public const string LOANLABEL = "Loan Number: ";
        public const string EMPLOYEELABEL = "Employee: ";
        public const string BARDATA = "-----------------------------------------";
        public const string TBARDATA = "========================================";
        public const int TXTBOXWIDTH = 36;

        public NavBox NavControlBox;
        private PawnLoan pwnLoan;
        private PawnAppVO pwnApp;
        string store_short_name;
        string store_street_address;
        string store_city_state_zip;
        string store_phone;
        string f_date_and_time;
        string receipt_number;
        string f_cust_name;
        string emp_number;
        string ticketNumber;
        string ticketAmount;
        bool loaded;

        public ViewReceipt()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
            this.loaded = false;
        }

        public ViewReceipt(string recp)
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
            this.loaded = false;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "Back";
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            var data = new Dictionary<string, string>();
            data.Add("store_short_name", this.store_short_name);
            data.Add("store_street_address", this.store_street_address);
            data.Add("store_city_state_zip", this.store_city_state_zip);
            data.Add("store_phone", this.store_phone);
            data.Add("f_date_and_time", this.f_date_and_time);
            data.Add("f_cust_name", this.f_cust_name);
            data.Add("receipt_number", this.receipt_number);
            data.Add("emp_number", this.emp_number);
            data.Add("_BARDATA_H_02", this.receipt_number);
            if (this.pwnLoan.LoanStatus == ProductStatus.IP)
            {
                data.Add("DETAIL001", "<B>1 New Pawn Loan: " + ticketNumber + "  " + ticketAmount);
                data.Add("DETAIL002", "<L>");
                data.Add("DETAIL003", "<S>");
                data.Add("DETAIL004", "<R>Grand Total                      " + ticketAmount);
                data.Add("DETAIL005", "<R>Amount Received From Customer    " + " $0.00");
                data.Add("DETAIL006", "<R>Cash Paid To Customer            " + ticketAmount);
            }
            else if (this.pwnLoan.LoanStatus == ProductStatus.PU)
            {
                data.Add("DETAIL001", "<B>1 Pickup Pawn Loan: " + ticketNumber + "  " + ticketAmount);
                data.Add("DETAIL002", "<L>");
                data.Add("DETAIL003", "<S>");
                data.Add("DETAIL004", "<R>Grand Total                      " + ticketAmount);
                data.Add("DETAIL005", "<R>Amount Received From Customer    " + this.pwnLoan.PickupAmount.ToString("C"));
                data.Add("DETAIL006", "<R>Cash Paid To Customer            " + " $0.00");
            }
            //string s9 = "123456789A123456789B123456789C123456789D";
            data.Add("##TEMPLATEFILENAME##", "receiptTemplate.tpl");
            bool printEnabled =
                SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled;
            if (printEnabled && GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IsValid)
            {
                data.Add("##IPADDRESS01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IPAddress);
                data.Add("##PORTNUMBER01##", GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.Port.ToString());

                //-- TODO: Changed to two for pilot
                data.Add("##HOWMANYCOPIES##", "2");
                string fullFileName;
#if (!__MULTI__)
                PrintingUtilities.PrintReceipt(data, false, out fullFileName);
#endif
            }
            //this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void ViewReceipt_Load(object sender, EventArgs e)
        {
            this.NavControlBox.Owner = this;
            if (!this.loaded)
            {
                loadReceiptData();
            }
  
            this.loaded = true;
        }

        private void loadReceiptData()
        {
            //this.receiptDataTextBox.Text;
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(StringUtilities.centerString(CASHAMERICATXT, TXTBOXWIDTH));
            sb.AppendLine(StringUtilities.centerString(ProcessTenderController.STORE_NAME, TXTBOXWIDTH));
            this.store_short_name = ProcessTenderController.STORE_NAME;
            this.store_street_address = ProcessTenderController.STORE_ADDRESS;
            this.store_city_state_zip = ProcessTenderController.STORE_CITY + ", " +
                                        ProcessTenderController.STORE_STATE + ProcessTenderController.STORE_ZIP;
            this.store_phone = ProcessTenderController.STORE_PHONE;
            sb.AppendLine(StringUtilities.centerString(ProcessTenderController.STORE_ADDRESS, TXTBOXWIDTH));
            sb.AppendLine(StringUtilities.centerString(ProcessTenderController.STORE_CITY 
                                                       + ", " 
                                                       + ProcessTenderController.STORE_STATE 
                                                       + " " 
                                                       + ProcessTenderController.STORE_ZIP, TXTBOXWIDTH));

            sb.AppendLine();
            sb.AppendLine(StringUtilities.centerString(ProcessTenderController.STORE_PHONE, TXTBOXWIDTH));
            if (CollectionUtilities.isNotEmpty(cds.PawnReceipt))
            {
                Common.Libraries.Utility.Shared.Receipt pReceipt = cds.PawnReceipt[0];
                DateTime receiptDt = pReceipt.Date;

                //Load loan
                PawnLoan pLoan;
                PawnAppVO pApp;
                CustomerVO custVO;
                string errorCode;
                string errorText;
                try
                {
                    int receiptStore = Int32.Parse(pReceipt.StoreNumber);
                    int ticketNum = Int32.Parse(pReceipt.RefNumber);
                    if (!CustomerLoans.GetPawnLoan(GlobalDataAccessor.Instance.DesktopSession, receiptStore, ticketNum, "0", StateStatus.BLNK, true,
                                                   out pLoan, out pApp, out custVO, out errorCode, out errorText))
                    {
                        //Do something about the error here
                        MessageBox.Show("Cannot find loan associated with receipt.");
                        NavControlBox.Action = NavBox.NavAction.CANCEL;
                        return;
                    }
                    this.pwnLoan = pLoan;
                    this.pwnApp = pApp;

                    if (custVO == null)
                    {
                        //Do something about the error here
                        MessageBox.Show("Cannot find customer associated with receipt");
                        NavControlBox.Action = NavBox.NavAction.CANCEL;
                        return;
                    }
                    
                    /* this.voidReceiptButton.Enabled = 
                    (this.pwnLoan.LoanStatus == PawnLoanStatus.IP);*/
                    this.printButton.Enabled = true;
                    this.f_date_and_time = receiptDt.ToString("d", DateTimeFormatInfo.InvariantInfo) + " " +
                                           this.pwnLoan.MadeTime.ToShortTimeString();
                    sb.AppendLine(StringUtilities.centerString(this.f_date_and_time, TXTBOXWIDTH));
                    this.receipt_number = "" + pReceipt.ReceiptNumber;
                    sb.AppendLine(StringUtilities.centerString(RECEIPTNUM + pReceipt.ReceiptNumber, TXTBOXWIDTH));
                    sb.AppendLine();
                    sb.AppendLine(StringUtilities.centerString(CUSTOMERLABEL + 
                                                               custVO.LastName + ", " + custVO.FirstName, TXTBOXWIDTH));
                    this.f_cust_name = custVO.LastName + ", " + custVO.FirstName.Substring(0, 1);
                    sb.AppendLine(StringUtilities.centerString(LOANLABEL + 
                                                               pLoan.OrgShopNumber + pLoan.TicketNumber, TXTBOXWIDTH));
                    sb.AppendLine();
                    this.ticketAmount = this.pwnLoan.Amount.ToString("C");
                    this.ticketNumber = this.pwnLoan.TicketNumber.ToString();
                    if (this.pwnLoan.LoanStatus == ProductStatus.IP)
                    {
                        sb.AppendLine("1 Pawn Loan");
                        sb.AppendLine(BARDATA);
                        sb.AppendLine("Cash Paid TO Customer          " + pReceipt.Amount.ToString("C"));
                    }
                    else if (this.pwnLoan.LoanStatus == ProductStatus.PU)
                    {
                        sb.AppendLine("Pickup Pawn Loan");
                        sb.AppendLine(BARDATA);
                        sb.AppendLine("Cash Received FROM Customer    " + pReceipt.Amount.ToString("C"));
                    }
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.AppendLine(StringUtilities.centerString(EMPLOYEELABEL + pReceipt.EntID , 40));
                    this.emp_number = pReceipt.EntID;
                    sb.AppendLine(TBARDATA);

                    //Add receipt information to text box
                    this.receiptDataTextBox.Text = sb.ToString();
                    this.receiptDataTextBox.Update();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot find receipt.");
                    this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                    this.Close();
                }
            }
        }
    }
}
