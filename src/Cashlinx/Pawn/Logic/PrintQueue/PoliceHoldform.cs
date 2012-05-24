using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Common.Controllers.Security;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Logic.PrintQueue
{
    public partial class PoliceHoldform : Form
    {
        string transactionDate = "";

        string empNo = "";

        private static string STORE_ADDRESS;
        private static string STORE_CITY;
        private static string STORE_STATE;
        private static string STORE_ZIP;
        private static string STORE_NAME;

        public PoliceHoldform()
        {
            InitializeComponent();
        }

        public List<HoldData> PoliceHoldLoans { get; set; }

        private void PoliceHoldform_Load(object sender, EventArgs e)
        {
            decimal tranAmt = 0.0m;
            //Populate store data
            STORE_NAME = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
            STORE_ADDRESS = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1;
            STORE_CITY = GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName;
            STORE_STATE = GlobalDataAccessor.Instance.CurrentSiteId.State;
            STORE_ZIP = GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;
 
            CustomerVO currentCust = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;

            empNo = GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant();
            //Set all the data for printing

            //Madhu 12/02/2010 fix for bug 9
            transactionDate = ShopDateTime.Instance.ShopDate.ToShortDateString();

            foreach (HoldData phdata in PoliceHoldLoans)
            {
                if (currentCust != null)
                {
                    label1CustName.Text = currentCust.CustomerName;
                    label1CustDOB.Text = currentCust.DateOfBirth.FormatDate();
                    labelCustAddress.Text = currentCust.CustHomeAddress;
                }
                //Madhu fix for bug 9
                //transactionDate = phdata.TransactionDate.ToShortDateString();

                labelTranDate.Text = @"Date : " + transactionDate;
                labelStoreName.Text = STORE_NAME;
                labelStoreAddr1.Text = STORE_ADDRESS;
                labelStoreAddr2.Text = STORE_CITY + " " + STORE_STATE + " " + STORE_ZIP;
                labelEmpNo.Text = empNo;
                foreach (Item pItem in phdata.Items)
                {
                    tranAmt += pItem.ItemAmount;
                }
                labelTranCost.Text = string.Format("{0:C}", tranAmt);
                labelTranNo.Text = phdata.TicketNumber.ToString();
                labelHoldComment.Text = phdata.HoldComment;
                labelOfficerName.Text = phdata.PoliceInformation.OfficerFirstName + " " +
                                        phdata.PoliceInformation.OfficerLastName;
                labelHoldExpire.Text = phdata.ReleaseDate.ToShortDateString();
                labelReqType.Text = phdata.PoliceInformation.RequestType;

                labelOfficerPhone.Text = Utilities.GetPhoneNumber(
                    Utilities.GetStringValue(
                        phdata.PoliceInformation.PhoneAreaCode, "")
                    + "-"
                    + Utilities.GetStringValue(phdata.PoliceInformation.PhoneNumber, ""));
  
                labelOfficerBadge.Text = phdata.PoliceInformation.BadgeNumber;
                labelCaseNo.Text = phdata.PoliceInformation.CaseNumber;
                labelAgency.Text = phdata.PoliceInformation.Agency;

                int n = 0;
                int j = 0;
                foreach (Item pawnItemData in phdata.Items)
                {
                    n++;
                    j++;
                    itemDescription.Text += j + " " + pawnItemData.TicketDescription
                                            + Environment.NewLine;
                    if (n == 3)
                    {
                        Print();
                        itemDescription.Text = "";
                        n = 0;
                    }
                }
                Print();
                PrintPDFReport(currentCust, PoliceHoldLoans);
                itemDescription.Text = "";
            }
            Application.DoEvents();
            this.Close();
        }

        private void Print()
        {
            for (int i = 0; i < 2; i++)
            {
                Bitmap bitMap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                DrawToBitmap(bitMap, new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height));
                PrintingUtilities.PrintBitmapDocument(bitMap, GlobalDataAccessor.Instance.DesktopSession);
            }
        }

        private void PrintPDFReport(CustomerVO currentCustomer, List<HoldData> policeHolds)
        {
            // 03082012 TG - Add printing using iText#
            //var rtcReport = new Reports.ReleaseToClaimantReport();
            var phReport = new Reports.PoliceHoldReport();
            var reportObject = new ReportObject();
            reportObject.ReportTempFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
            reportObject.CreateTemporaryFullName();

            phReport.reportObject = reportObject;
            phReport.ReportTempFileFullName = reportObject.ReportTempFileFullName;
            // set store information
            phReport.STORE_NAME = ProcessTenderController.STORE_NAME;
            phReport.STORE_ADDRESS = ProcessTenderController.STORE_ADDRESS;
            phReport.STORE_CITY = ProcessTenderController.STORE_CITY;
            phReport.STORE_STATE = ProcessTenderController.STORE_STATE;
            phReport.STORE_ZIP = ProcessTenderController.STORE_ZIP;

            phReport.CurrentCust = currentCustomer;
            // Pass hold data
            phReport.HoldData = policeHolds[0];


            if (phReport.CreateReport())
            {
                DesktopSession.ShowPDFFile(phReport.reportObject.ReportTempFileFullName, false);
                this.TopMost = false;
            }


        }

    }
}
