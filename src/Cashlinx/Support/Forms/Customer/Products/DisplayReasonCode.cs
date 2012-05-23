using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility.Shared;
using Support.Libraries.Objects.Customer;
using Support.Libraries.Objects.PDLoan;

namespace Support.Forms.Customer.Products
{
    public partial class DisplayReasonCode : Form
    {

        private Form ownerfrm;
        public NavBox NavControlBox;

        /*__________________________________________________________________________________________*/
        public DisplayReasonCode()
        {
            this.NavControlBox = new NavBox();
            InitializeComponent();

        }

        /*__________________________________________________________________________________________*/
        private void AddViewSupportCustomerComment_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;
            PDLoan pdLoan = Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan;
            this.ReasonTypeDisplay.Text = pdLoan.Status;
            this.ReasonCodeDisplay.Text = pdLoan.Decline_Reason;
            this.DescriptionDisplay.Text = pdLoan.Decline_Description;
        }
        /*__________________________________________________________________________________________*/
        private void MapFormDataForEmptyRecord()
        {

        }

        /*__________________________________________________________________________________________*/
    }
}
