﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace CashlinxDesktop.Desktop.PrintQueue
{
    public partial class RTCform : Form
    {
        string transactionDate = "";

        string empNo = "";

        int numofRTCLoans = 0;

        private static string STORE_ADDRESS;
        private static string STORE_CITY;
        private static string STORE_STATE;
        private static string STORE_ZIP;
        private static string STORE_NAME;

        public RTCform()
        {
            InitializeComponent();
        }

        public List<HoldData> RTCLoans { get; set; }

        private void RTCform_Load(object sender, EventArgs e)
        {
            //Populate store data
            STORE_NAME = CashlinxDesktopSession.Instance.CurrentSiteId.StoreName;
            STORE_ADDRESS = CashlinxDesktopSession.Instance.CurrentSiteId.StoreAddress1;
            STORE_CITY = CashlinxDesktopSession.Instance.CurrentSiteId.StoreCityName;
            STORE_STATE = CashlinxDesktopSession.Instance.CurrentSiteId.State;
            STORE_ZIP = CashlinxDesktopSession.Instance.CurrentSiteId.StoreZipCode;

            CustomerVO currentCust = CashlinxDesktopSession.Instance.ActiveCustomer;

            numofRTCLoans = RTCLoans.Count;
            empNo = CashlinxDesktopSession.Instance.UserName.ToLowerInvariant();
            //Set all the data for printing
            transactionDate = ShopDateTime.Instance.ShopDate.FormatDate();

            foreach (HoldData rtcdata in RTCLoans)
            {
                if (currentCust != null)
                {
                    label1CustName.Text = currentCust.CustomerName;
                    label1CustDOB.Text = currentCust.DateOfBirth.FormatDate();
                    labelCustAddress.Text = currentCust.CustHomeAddress;
                }
                labelTranDate.Text = "Date:  " + transactionDate;
                labelStoreName.Text = STORE_NAME;
                labelStoreAddr1.Text = STORE_ADDRESS;
                labelStoreAddr2.Text = STORE_CITY + ", " + STORE_STATE + " " + STORE_ZIP;
                labelTktNo.Text = rtcdata.TicketNumber.ToString();
                labelEmpNo.Text = empNo;
                labelReleaseComment.Text = rtcdata.HoldComment;
                labelOfficerName.Text = rtcdata.PoliceInformation.OfficerFirstName + " " +
                                        rtcdata.PoliceInformation.OfficerLastName;
                labelOfficerBadge.Text = rtcdata.PoliceInformation.BadgeNumber;
                labelOfficerPhone.Text = Utilities.GetPhoneNumber(
                    Utilities.GetStringValue(
                        rtcdata.PoliceInformation.PhoneAreaCode, "")
                    + "-"
                    + Utilities.GetStringValue(rtcdata.PoliceInformation.PhoneNumber, ""));
                // Only show dash and extension if value exists.  -DDW, 6/30/2010
                if (Utilities.GetStringValue(rtcdata.PoliceInformation.PhoneExtension, "") != "")
                    labelOfficerPhone.Text += "-" + rtcdata.PoliceInformation.PhoneExtension;

                //To DO: Change the following 2 fields to denote actual values when restitution is added
                labelRestitutionCollected.Text = "No";
                labelRestitutionAmt.Text = "$0.00";

                int i = 0;
                foreach (Item pawnItemData in rtcdata.Items)
                {
                    i++;
                    itemDescription.Text += i.ToString() + " " + pawnItemData.TicketDescription
                                            + System.Environment.NewLine;
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
                Bitmap bitMap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                DrawToBitmap(bitMap, new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height));
                PrintingUtilities.PrintBitmapDocument(bitMap, CashlinxDesktopSession.Instance);
            }
        }
    }
}