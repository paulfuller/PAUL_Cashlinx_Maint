/**************************************************************************************************************
* CashlinxDesktop
* CustomerHoldInfo
* This form is used to get the release date and reason for hold for the selected transactions
* that the user wishes to put on customer hold
* Sreelatha Rengarajan 9/21/2009 Initial version
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;
using Pawn.Logic.PrintQueue;

namespace Pawn.Forms.Pawn.Customer.Holds
{
    public partial class PoliceHoldInfo : Form
    {
        public NavBox NavControlBox;
        private Form ownerfrm;
        private List<HoldData> policeHolds;
        private bool _formValid = true;

        public PoliceHoldInfo()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }

        private void PoliceHoldInfo_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;

            policeHolds = GlobalDataAccessor.Instance.DesktopSession.HoldsData;
            BusinessRulesProcedures bProcedures = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession);
            int policeHoldDays=bProcedures.GetPoliceHoldDefaultDays(GlobalDataAccessor.Instance.CurrentSiteId);
            dateCalendarRelease.SelectedDate = DateTime.Now.AddDays(policeHoldDays).FormatDate();
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
            var _selectedLoanNumbers = new List<int>();
            var _selectedRefType = new List<string>();
            foreach (var custHold in policeHolds)
            {
                _selectedLoanNumbers.Add(Utilities.GetIntegerValue(custHold.TicketNumber));
                _selectedRefType.Add(Utilities.GetStringValue(custHold.RefType));
            }
            string strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            string errorCode;
            string errorMsg;
            bool retVal = StoreLoans.UpdateTempStatus(_selectedLoanNumbers, StateStatus.BLNK,
                                                      strStoreNumber, true, _selectedRefType, out errorCode, out errorMsg);
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
            //Check that the officer first name, officer last name, badge number,
            //agency, phone area code, phone number, request type, reason for hold
            //and eleigible for release is entered
            if (customTextBoxAgency.isValid && customTextBoxBadgeNumber.isValid &&
                customTextBoxOfficerFirstName.isValid && customTextBoxOfficerLastName.isValid &&
                customTextBoxPhoneAreaCode.isValid && customTextBoxPhoneNumber.isValid &&
                comboBoxReqType.SelectedItem != null &&
                richTextBoxReason.Text.Trim().Length > 0 &&
                dateCalendarRelease.SelectedDate != string.Empty)
                _formValid = true;
            else
                _formValid = false;
            if (_formValid)
            {
                do
                {
                    if (policeHolds.Count > 0)
                    {
                        //Continue with Hold if there are any transactions to put on Hold
                        bool returnValue = HoldsProcedures.AddPoliceHolds(policeHolds, this.richTextBoxReason.Text,
                                                                          Utilities.GetDateTimeValue(
                                                                              this.dateCalendarRelease.SelectedDate,
                                                                              DateTime.MaxValue), this.customTextBoxOfficerFirstName.Text,
                                                                          customTextBoxOfficerLastName.Text, customTextBoxBadgeNumber.Text,
                                                                          customTextBoxPhoneAreaCode.Text,
                                                                          customTextBoxPhoneExt.Text,
                                                                          customTextBoxPhoneNumber.Text,
                                                                          comboBoxReqType.Text,
                                                                          customTextBoxCaseNumber.Text,
                                                                          customTextBoxAgency.Text);
                        if (returnValue)
                        {
                            var policeInfo = new PoliceInfo
                            {
                                Agency = customTextBoxAgency.Text,
                                BadgeNumber = customTextBoxBadgeNumber.Text,
                                CaseNumber = customTextBoxCaseNumber.Text,
                                OfficerFirstName = customTextBoxOfficerFirstName.Text,
                                OfficerLastName = customTextBoxOfficerLastName.Text,
                                PhoneAreaCode = customTextBoxPhoneAreaCode.Text,
                                PhoneExtension = customTextBoxPhoneExt.Text,
                                PhoneNumber = customTextBoxPhoneNumber.Text,
                                RequestType = comboBoxReqType.Text
                            };
                            foreach (var policehold in policeHolds)
                            {
                                policehold.PoliceInformation = policeInfo;
                                policehold.HoldComment = richTextBoxReason.Text;
                                policehold.ReleaseDate = Utilities.GetDateTimeValue(
                                    this.dateCalendarRelease.SelectedDate,
                                    DateTime.MaxValue);
                            }
                            //Call print Police seize form if print is enabled
                            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                            {
                                var phFrm = new PoliceHoldform();
                                phFrm.PoliceHoldLoans = policeHolds;
                                phFrm.ShowDialog();
                            }
                            MessageBox.Show("Selected transactions placed on police hold successfully");
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
                customLabelReleaseEligible.ForeColor = Color.Red;
                dateCalendarRelease.Focus();
                _formValid = false;
            }
            else
            {
                customLabelReleaseEligible.ForeColor = Color.Black;
                _formValid = true;
            }
        }

        //Madhu 12/02/2010 fix for the bug 9
        private void customTextBoxPhoneNumber_Leave(object sender, EventArgs e)
        {
            if (customTextBoxPhoneNumber.Text.Length > 0 && customTextBoxPhoneNumber.Text.Length < 8)
            {
                MessageBox.Show(Commons.GetMessageString("InvalidPhoneNumber"));
                customTextBoxPhoneNumber.Focus();
            }
            else
            {
                customTextBoxPhoneAreaCode_Leave(sender, e);
            }
        }

        private void customTextBoxPhoneAreaCode_Leave(object sender, EventArgs e)
        {
            if (customTextBoxPhoneNumber.Text.Length > 0 && customTextBoxPhoneAreaCode.Text.Length == 0)
            {
                MessageBox.Show(Commons.GetMessageString("PhoneAreaCodeNotEntered"));
                //customTextBoxPhoneAreaCode.Focus();
            }
            else if (customTextBoxPhoneAreaCode.Text.Length > 0 && customTextBoxPhoneAreaCode.Text.Length < 3)
            {
                MessageBox.Show(Commons.GetMessageString("InvalidPhoneAreaCode"));
                customTextBoxPhoneAreaCode.Focus();
            }
        }
    }
}
