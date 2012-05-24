/********************************************************************
* Namespace: CashlinxDesktop.Desktop.PrintQueue
* FileName: TransferOutReport
* Prints the store to store transfer report
* Sreelatha Rengarajan 12/19/2009 Initial version
 * SR 3/17/10 Added store name
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;
using Common.Controllers.Database.Procedures;

namespace Pawn.Logic.PrintQueue
{
    public partial class PoliceSeizeform : Form
    {
        private Bitmap bitMap;
        string transactionDate = "";

        string empNo = "";



        int numofSeizeLoans = 0;
        private static string STORE_ADDRESS;
        private static string STORE_CITY;
        private static string STORE_STATE;
        private static string STORE_ZIP;
        private static string STORE_NAME;



        public PoliceSeizeform()
        {
            InitializeComponent();

        }

        public List<HoldData> PoliceSeizeLoans
        {
            get;
            set;
        }

        private void PoliceSeizeform_Load(object sender, EventArgs e)
        {

            //Populate store data
            STORE_NAME = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
            STORE_ADDRESS = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1;
            STORE_CITY = GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName;
            STORE_STATE = GlobalDataAccessor.Instance.CurrentSiteId.State;
            STORE_ZIP = GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;
            CustomerVO currentCust = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
 
            numofSeizeLoans = PoliceSeizeLoans.Count;
            empNo = GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant();
            //Set all the data for printing
            transactionDate = ShopDateTime.Instance.ShopDate.FormatDate();


            foreach (HoldData seizedata in PoliceSeizeLoans)
            {
                if (currentCust != null)
                {

                    labelCustName.Text = currentCust.CustomerName;
                    var custHomeAddr = currentCust.getHomeAddress();
                    if (custHomeAddr != null)
                    {
                        labelCustAddressLine1.Text = custHomeAddr.Address1;
                        labelCustAddressLine2.Text = custHomeAddr.City + "," + custHomeAddr.State_Code + "," + custHomeAddr.ZipCode;
                    }
                    else
                    {
                        labelCustAddressLine1.Text = string.Empty;
                        labelCustAddressLine2.Text = string.Empty;
                    }
                }
                labelTranDate.Text = "Date : " + transactionDate;
                labelStoreName.Text = STORE_NAME;
                labelStoreAddr1.Text = STORE_ADDRESS;
                labelStoreAddr2.Text = STORE_CITY + " " + STORE_STATE + " " + STORE_ZIP;
                labelEmpNo.Text = empNo;
                labelCaseNo.Text = seizedata.PoliceInformation.CaseNumber;
                if (seizedata.RefType=="2")
                    labelLoanNoHeading.Text = "Purchase Number:";
                
                labelLoanNo.Text = seizedata.TicketNumber.ToString();
                labelAmt.Text = string.Format("{0:C}", seizedata.Amount);

                if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.CurrentSiteId))
                {
                    labelCurrentPrincipal.Text = String.Format("{0:C}", seizedata.CurrentPrincipalAmount);
                }
                else
                {
                    labelCurrentPrincipalHeading.Visible = false;
                    labelCurrentPrincipal.Visible = false;
                }

                labelCurrentPrincipal.Text = string.Format("{0:C}", seizedata.CurrentPrincipalAmount);
                labelAgency.Text = seizedata.PoliceInformation.Agency;
                labelSeizeNumber.Text = seizedata.PoliceInformation.SeizeNumber.ToString();

                int i = 0;
                foreach (Item pawnItemData in seizedata.Items)
                {
                    i++;
                    itemDescription.Text += string.Format("{0} {1}{2}", i, pawnItemData.TicketDescription, System.Environment.NewLine);
                }
                Print();
            }
            Application.DoEvents();
            this.Close();

        }

        private void Print()
        {
            for (int i = 0; i < 2; i++)
            {
                bitMap = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
                this.DrawToBitmap(bitMap, new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
                PrintingUtilities.PrintBitmapDocument(bitMap, GlobalDataAccessor.Instance.DesktopSession);
            }
        }




    }
}
