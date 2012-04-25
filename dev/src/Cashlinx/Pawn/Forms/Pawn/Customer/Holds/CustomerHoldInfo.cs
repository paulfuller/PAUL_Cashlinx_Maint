/**************************************************************************************************************
* CashlinxDesktop
* CustomerHoldInfo
* This form is used to get the release date and reason for hold for the selected transactions
* that the user wishes to put on customer hold
* Sreelatha Rengarajan 8/6/2009 Initial version
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Customer.Holds
{
    public partial class CustomerHoldInfo : Form
    {
        public NavBox NavControlBox;
        private Form ownerfrm;
        private List<HoldData> custHolds;
        private bool _formValid = true;
        
        public CustomerHoldInfo()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }

        private void CustomerHoldInfo_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;

            custHolds = GlobalDataAccessor.Instance.DesktopSession.HoldsData;
            
            //set the release date by default to 3 business days from today
            //TODO - This should be a rule!!!
            this.dateCalendarRelease.SelectedDate = ShopDateTime.Instance.ShopDate.AddDays(3).FormatDate();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            //Remove the temp status of CH on the selected records
            if (RemoveTempStatusOnLoans())
            {
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "Back";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                BasicExceptionHandler.Instance.AddException("Error when trying to remove temp status on selected transactions", new ApplicationException());
            }
        }

        private bool RemoveTempStatusOnLoans()
        {
            List<int> _selectedLoanNumbers = new List<int>();
            List<string> _selectedRefTypes = new List<string>();
            foreach (HoldData custHold in custHolds)
            {
                _selectedLoanNumbers.Add(Utilities.GetIntegerValue(custHold.TicketNumber));
                _selectedRefTypes.Add(Utilities.GetStringValue(custHold.RefType));
            }
            string strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            string errorCode;
            string errorMsg;
            bool retVal = StoreLoans.UpdateTempStatus(_selectedLoanNumbers, StateStatus.BLNK,
                                                      strStoreNumber, true, _selectedRefTypes, out errorCode, out errorMsg);
            return retVal;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            bool retVal = RemoveTempStatusOnLoans();
            if (!retVal)
                BasicExceptionHandler.Instance.AddException("Error when trying to remove temp status on selected transactions", new ApplicationException());
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            DialogResult dgr = DialogResult.Retry;
            if (_formValid)
            {
                do
                {
                    //Check that the loans selected for holds have not been picked up for PFI
                    //from the time they were fetched
                    if (custHolds.Count > 0)
                    {
                        //Continue with Hold if there are any transactions to put on Hold
                        bool returnValue = HoldsProcedures.AddCustomerHolds(custHolds, this.richTextBoxReason.Text,
                                                                            Utilities.GetDateTimeValue(
                                                                                this.dateCalendarRelease.SelectedDate,
                                                                                DateTime.MaxValue));
                        if (returnValue)
                        {
                            MessageBox.Show("Selected loans placed on customer hold successfully");
                            break;
                        }
                        else
                        {
                            dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error",
                                                  MessageBoxButtons.RetryCancel);
                        }
                    }
                    else //no holds to do
                    {
                        dgr = DialogResult.Cancel;
                    }
                }while (dgr == DialogResult.Retry);
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                MessageBox.Show(Commons.GetMessageString("FormErrorSubmitAgain"));
                return;
            }
        }

        private void dateCalendarRelease_Leave(object sender, EventArgs e)
        {
            DateTime relDate = Utilities.GetDateTimeValue(dateCalendarRelease.SelectedDate);
            if (relDate < ShopDateTime.Instance.ShopDate)
            {
                MessageBox.Show(Commons.GetMessageString("InvalidDate"));
                labelReleaseEligible.ForeColor = Color.Red;
                dateCalendarRelease.Focus();
                _formValid = false;
            }
            else
            {
                labelReleaseEligible.ForeColor = Color.Black;
                _formValid = true;
            }
        }
    }
}
