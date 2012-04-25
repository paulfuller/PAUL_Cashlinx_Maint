/**************************************************************************************************************
* CashlinxDesktop
* CustomerHoldInfo
* This form is used to get the release date and reason for hold for the selected transactions
* that the user wishes to put on customer hold
* Sreelatha Rengarajan 8/6/2009 Initial version
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Customer.ItemRelease
{
    public partial class HoldUpdateReleaseDate : Form
    {
        public NavBox NavControlBox;
        private Form ownerfrm;
        MonthCalendar newCalendar = new MonthCalendar();
        private List<HoldData> custHolds;
        BindingSource bindingSource1;
        string userId = "";


        public HoldUpdateReleaseDate()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }

        private void CustomerHoldInfo_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;

            userId = GlobalDataAccessor.Instance.DesktopSession.UserName;

            custHolds = GlobalDataAccessor.Instance.DesktopSession.CustHoldsData;

            bindingSource1 = new BindingSource();
            bindingSource1.DataSource = custHolds;

            this.dataGridViewSelectedTransactions.AutoGenerateColumns = false;
            this.dataGridViewSelectedTransactions.DataSource = bindingSource1;
            this.dataGridViewSelectedTransactions.Columns[0].DataPropertyName = "RefNumber";
            this.dataGridViewSelectedTransactions.Columns[1].DataPropertyName = "HoldDate";
            this.dataGridViewSelectedTransactions.Columns[2].DataPropertyName = "UserId";
            this.dataGridViewSelectedTransactions.Columns[3].DataPropertyName = "HoldComment";

            
            this.newCalendar.Name = "newCalendar";            
            this.newCalendar.Visible = false;
            this.newCalendar.DateSelected += this.newCalendar_DateSelected;
            this.Controls.Add(newCalendar);

            //set the release date by default to 3 business days from today
            DateTime releaseDate=ShopDateTime.Instance.ShopDate.AddDays(3);
            this.dateRelease.Controls[0].Text = releaseDate.FormatDate();
        }        

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void pictureBoxCalendar_Click(object sender, EventArgs e)
        {
                     
            newCalendar.Location = new System.Drawing.Point(230, 71);
            newCalendar.Visible = true;
            newCalendar.BringToFront();
            this.pictureBoxCalendar.Visible = false;

        }


        private void newCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            newCalendar.Visible = false;
            this.pictureBoxCalendar.Visible = true;
            this.dateRelease.Controls[0].Text = newCalendar.SelectionEnd.Date.FormatDate();

        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {

            bool returnValue = false;
            DialogResult dgr=DialogResult.Retry;
            do
            {
                foreach (HoldData custhold in custHolds)
                {
                    custhold.ReleaseDate = Utilities.GetDateTimeValue(dateRelease.Controls[0].Text,DateTime.MaxValue);
                }
                returnValue = HoldsProcedures.UpdateReleaseDateOnHolds(custHolds);
                if (returnValue)
                {
                    break;                    
                }
                else
                {
                    dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                }

            } while (dgr == DialogResult.Retry);
            this.Close();
        }
    }
}