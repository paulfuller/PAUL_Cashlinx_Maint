/**************************************************************************************************************
* CashlinxDesktop
* PoliceHoldreleaseInfo
* This form is used to gather the release info when a police hold is released or when released
* to claimant or seized by police
* 
* Sreelatha Rengarajan 9/21/2009 Initial version
* EDW 1/9/2011 - Minor code cleanup/Refactoring changes
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Services.Pickup;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;
using Pawn.Logic.PrintQueue;
using Reports;


namespace Pawn.Forms.Pawn.Customer.ItemRelease

{
    public partial class PoliceHoldRelease : Form
    {

     
        private DataSet _theData;

        public NavBox NavControlBox;
        private Form ownerfrm;
        private List<HoldData> policeHolds;
        private string STORE_NAME;
        private object currentCust;

        public bool ReleaseToClaimant { get; set; }

        public bool PoliceSeize { get; set; }

        public bool ReadOnly { get; set; }

        public string ReceiptDetailId { get; set; }

        public PoliceHoldRelease()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }

        private void PoliceHoldInfo_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;
            if (!ReadOnly)
            {
                policeHolds = GlobalDataAccessor.Instance.DesktopSession.HoldsData;
                if (GlobalDataAccessor.Instance.DesktopSession.ReleaseToClaimant &&
                    GlobalDataAccessor.Instance.DesktopSession.PoliceInformation != null)
                {
                    //invoke the submit button for release to claimant
                    ReleaseToClaimant = true;
                    buttonSubmit_Click(null, new EventArgs());
                }
            }
            else
            {
                this.buttonSubmit.Enabled = false;
                this.buttonCancel.Enabled = false;
                this.buttonBack.Text = "Close";
                //Get the police info and hold data
                DataTable policeInfo;
                string errorCode;
                string errorMesg;
                bool retVal = HoldsProcedures.getRTCSeizeInfo(ReceiptDetailId, ReleaseToClaimant ? ReceiptEventTypes.RTC.ToString() : ReceiptEventTypes.PolSeize.ToString(),
                                                              out policeInfo, out errorCode, out errorMesg);
                this.customTextBoxAgency.ReadOnly = true;
                this.customTextBoxBadgeNumber.ReadOnly = true;
                customTextBoxOfficerLastName.ReadOnly = true;
                customTextBoxPhoneAreaCode.ReadOnly = true;
                customTextBoxPhoneNumber.ReadOnly = true;
                customTextBoxCaseNumber.ReadOnly = true;
                customTextBoxOfficerFirstName.ReadOnly = true;
                customTextBoxPhoneExt.ReadOnly = true;
                richTextBoxReason.ReadOnly = true;

                if (retVal && policeInfo != null && policeInfo.Rows.Count > 0)
                {
                    this.customTextBoxAgency.Text = policeInfo.Rows[0]["agency"].ToString();
                    this.customTextBoxBadgeNumber.Text = policeInfo.Rows[0]["badge"].ToString();
                    this.customTextBoxOfficerLastName.Text = policeInfo.Rows[0]["officer_last_name"].ToString();
                    this.customTextBoxPhoneAreaCode.Text = policeInfo.Rows[0]["area_cd"].ToString();
                    this.customTextBoxPhoneNumber.Text = policeInfo.Rows[0]["phone"].ToString();
                    this.customTextBoxCaseNumber.Text = policeInfo.Rows[0]["case_number"].ToString();
                    this.customTextBoxOfficerFirstName.Text = policeInfo.Rows[0]["officer_first_name"].ToString();
                    this.customTextBoxPhoneExt.Text = policeInfo.Rows[0]["extension"].ToString();
                    this.richTextBoxReason.Text = policeInfo.Rows[0]["hcomment"].ToString();
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (buttonBack.Text == "Close")
                this.Close();
            else
            {
                //Remove the temp status of PH on the selected records
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
        }

        private bool RemoveTempStatusOnLoans()
        {
            List<int> _selectedLoanNumbers = new List<int>();       
            List<string> _selectedRefTypes = new List<string>();
            foreach (HoldData hold in policeHolds)
            {
                _selectedLoanNumbers.Add(Utilities.GetIntegerValue(hold.TicketNumber));
                _selectedRefTypes.Add(Utilities.GetStringValue(hold.RefType));
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
            bool returnValue = false;
            DialogResult dgr = DialogResult.Retry;

            //Release police hold
            if (!PoliceSeize && !ReleaseToClaimant)
            {
                do
                {
                    if (!customTextBoxAgency.isValid || !customTextBoxBadgeNumber.isValid ||
                        !customTextBoxOfficerFirstName.isValid || !customTextBoxOfficerLastName.isValid ||
                        !customTextBoxPhoneAreaCode.isValid || !customTextBoxPhoneNumber.isValid ||
                        richTextBoxReason.Text.Trim().Length <= 0)
                    {
                        MessageBox.Show("Please enter all the required fields and submit");
                        return;
                    }
                    returnValue = HoldsProcedures.RemovePoliceHolds(policeHolds, this.richTextBoxReason.Text,
                                                                    customTextBoxOfficerFirstName.Text,
                                                                    customTextBoxOfficerLastName.Text,
                                                                    customTextBoxBadgeNumber.Text,
                                                                    customTextBoxAgency.Text,
                                                                    customTextBoxCaseNumber.Text,
                                                                    customTextBoxPhoneAreaCode.Text,
                                                                    customTextBoxPhoneNumber.Text,
                                                                    customTextBoxPhoneExt.Text);
                    if (returnValue)
                    {
                        MessageBox.Show("selected transactions released from police hold successfully");
                        break;
                    }
                    else
                    {
                        dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                    }
                }while (dgr == DialogResult.Retry);
            }
            //Process police seize
            if (PoliceSeize)
            {
                if (!customTextBoxAgency.isValid || !customTextBoxBadgeNumber.isValid ||
                    !customTextBoxOfficerFirstName.isValid || !customTextBoxOfficerLastName.isValid ||
                    !customTextBoxPhoneAreaCode.isValid || !customTextBoxPhoneNumber.isValid ||
                    richTextBoxReason.Text.Trim().Length <= 0 || !customTextBoxCaseNumber.isValid)
                {
                    MessageBox.Show("Please enter all the required fields and submit");
                    return;
                }
                CustomerVO currentCustomer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                foreach (HoldData policeHold in policeHolds)
                {
                    policeHold.HoldComment = richTextBoxReason.Text;
                }
                PoliceInfo policeInfo = new PoliceInfo
                                        {
                                            Agency = customTextBoxAgency.Text,
                                            BadgeNumber = customTextBoxBadgeNumber.Text,
                                            CaseNumber = customTextBoxCaseNumber.Text,
                                            OfficerFirstName = customTextBoxOfficerFirstName.Text,
                                            OfficerLastName = customTextBoxOfficerLastName.Text,
                                            PhoneAreaCode = customTextBoxPhoneAreaCode.Text,
                                            PhoneExtension = customTextBoxPhoneExt.Text,
                                            PhoneNumber = customTextBoxPhoneNumber.Text,
                                            RequestType = ""
                                        };

                do
                {
                    int seizeNumber = 0;

                    returnValue = HoldsProcedures.AddPoliceSeize(
                        policeHolds, policeHolds[0].HoldComment,
                        policeInfo, currentCustomer, out seizeNumber);
                    if (returnValue && seizeNumber > 0)
                    {
                        policeInfo.SeizeNumber = seizeNumber;
                        ReceiptDetailsVO rDVO = new ReceiptDetailsVO();
                        if (!HoldsProcedures.insertPoliceReceipt(policeHolds, ref rDVO))
                            FileLogger.Instance.logMessage(LogLevel.ERROR, null, "Receipt details could not be entered for police seize " + seizeNumber);

                        MessageBox.Show("Selected items police seized successfully");
                        //Print police seize document
                        foreach (HoldData policehold in policeHolds)
                        {
                            policehold.PoliceInformation = policeInfo;
                        }
                        //Call print Police seize form if print is enabled
                        if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                        {
                            //PoliceSeizeform seizeFrm = new PoliceSeizeform();
                            //seizeFrm.PoliceSeizeLoans = policeHolds;
                            //seizeFrm.ShowDialog();

                            //Calling policeseizereport(Itextsharp) instead of bitmap(policeseizeform) calling
                            var policeseizereport = new Reports.PoliceSeizeReport();
                            var reportObject = new ReportObject();
                            reportObject.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\Police Seize" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
                            policeseizereport.reportObject = reportObject;
                            policeseizereport.ReportTempFileFullName = reportObject.ReportTempFileFullName;
                            policeseizereport.STORE_NAME = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
                            policeseizereport.STORE_ADDRESS = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1;
                            policeseizereport.STORE_CITY = GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName;
                            policeseizereport.STORE_STATE = GlobalDataAccessor.Instance.CurrentSiteId.State;
                            policeseizereport.STORE_ZIP = GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;
                            policeseizereport.CurrentCust = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                            policeseizereport.EmpNo = GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant();
                            policeseizereport.TransactionDate = ShopDateTime.Instance.ShopDate.FormatDate();
                            policeseizereport.HoldData = policeHolds[0];
                            policeseizereport.CustHomeAddr = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerAddress[0];
                            policeseizereport.CreateReport();
                            string strReturnMessage;
                            if (GlobalDataAccessor.Instance.DesktopSession.PDALaserPrinter.IsValid)
                            {
                                if (FileLogger.Instance.IsLogInfo)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.INFO, this, "Printing PoliceSeize report on PDA Laser printer: {0}",
                                                                   GlobalDataAccessor.Instance.DesktopSession.PDALaserPrinter);
                                }
                                strReturnMessage = PrintingUtilities.printDocument(
                                    reportObject.ReportTempFileFullName,
                                    GlobalDataAccessor.Instance.DesktopSession.PDALaserPrinter.IPAddress,
                                    GlobalDataAccessor.Instance.DesktopSession.PDALaserPrinter.Port,
                                   2);
                            }
                            else if (GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                            {
                                if (FileLogger.Instance.IsLogWarn)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.WARN, this,
                                                                   "Could not find valid PDA laser printer to print the PoliceSeize report." + Environment.NewLine +
                                                                   " Printing on default pawn laser printer: {0}",
                                                                   GlobalDataAccessor.Instance.DesktopSession.LaserPrinter);
                                }
                                strReturnMessage = PrintingUtilities.printDocument(
                                    reportObject.ReportTempFileFullName,
                                    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                                    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port,
                                    2);
                            }
                            else
                            {
                                if (FileLogger.Instance.IsLogError)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                                   "Could not find a valid laser printer to print the PoliceSeize report");
                                }
                                strReturnMessage = "FAIL - NO PRINTER FOUND";
                            }
                            if (strReturnMessage.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                            {
                                if (FileLogger.Instance.IsLogError)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                                   "Cannot print the PoliceSeize report: " + strReturnMessage);
                                }
                            }


                        }

                        break;
                           
                        }
                    
                    dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                    }
                    while (dgr == DialogResult.Retry) ;
              
        }
            //Process Release to claimant
            if (ReleaseToClaimant)
            {
                //Store the police info in session
                if (GlobalDataAccessor.Instance.DesktopSession.PoliceInformation == null)
                {
                    if (!customTextBoxAgency.isValid || !customTextBoxBadgeNumber.isValid ||
                        !customTextBoxOfficerFirstName.isValid || !customTextBoxOfficerLastName.isValid ||
                        !customTextBoxPhoneAreaCode.isValid || !customTextBoxPhoneNumber.isValid ||
                        richTextBoxReason.Text.Trim().Length <= 0)
                    {
                        MessageBox.Show("Please enter all the required fields and submit");
                        return;
                    }
                    foreach (HoldData policeHold in policeHolds)
                    {
                        policeHold.HoldComment = richTextBoxReason.Text;
                    }
                    PoliceInfo policeInfo = new PoliceInfo
                    {
                        Agency = customTextBoxAgency.Text,
                        BadgeNumber = customTextBoxBadgeNumber.Text,
                        CaseNumber = customTextBoxCaseNumber.Text,
                        OfficerFirstName = customTextBoxOfficerFirstName.Text,
                        OfficerLastName = customTextBoxOfficerLastName.Text,
                        PhoneAreaCode = customTextBoxPhoneAreaCode.Text,
                        PhoneExtension = customTextBoxPhoneExt.Text,
                        PhoneNumber = customTextBoxPhoneNumber.Text
                    };
                    GlobalDataAccessor.Instance.DesktopSession.PoliceInformation = policeInfo;
                    GlobalDataAccessor.Instance.DesktopSession.ReleaseToClaimant = true;
                    NavControlBox.IsCustom = true;
                    NavControlBox.CustomDetail = "FindClaimant";
                    this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                }
                else
                {
                    bool gunInvolved = false;
                    CustomerVO currentCustomer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                    //Check if any of the loans being released has a gun

                    foreach (HoldData pl in policeHolds)
                    {
                        var gunItems = from pItem in pl.Items
                                       where pItem.GunNumber > 0
                                       select pItem;
                        if (gunItems.Any())
                        {
                            gunInvolved = true;
                            break;
                        }
                    }
                    //if gun involved check for cwp
                    bool backgroundCheck = false;
                    if (gunInvolved)
                    {
                        /*DateTime currentDate = ShopDateTime.Instance.ShopDate;
                        string strStoreState = GlobalDataAccessor.Instance.CurrentSiteId.State;
                        if (currentCustomer.HasValidConcealedWeaponsPermitInState(strStoreState, currentDate))
                        {
                            if (CustomerProcedures.IsBackgroundCheckRequired())
                            {
                                FirearmsBackgroundCheck backgroundcheckFrm = new FirearmsBackgroundCheck();
                                backgroundcheckFrm.ShowDialog(this);
                            }
                            else //If the background check is not needed
                                CashlinxDesktopSession.Instance.BackgroundCheckCompleted = true;
                        }
                        //else if they do not have CWP or not a CWP in the store state or expired 
                        //then show the background check form
                        else
                        {
                            FirearmsBackgroundCheck backgroundcheckFrm = new FirearmsBackgroundCheck();
                            backgroundcheckFrm.ShowDialog(this);
                        }*/
                        FirearmsBackgroundCheck backgroundcheckFrm = new FirearmsBackgroundCheck();
                        backgroundcheckFrm.ShowDialog(this);
                        if (GlobalDataAccessor.Instance.DesktopSession.BackgroundCheckCompleted)
                            backgroundCheck = true;
                    }
                    else
                    {
                        backgroundCheck = true;
                    }

                    if (backgroundCheck)
                    {
                        do
                        {
                            returnValue = HoldsProcedures.AddReleaseToClaimant(policeHolds, policeHolds[0].HoldComment,
                                                                               GlobalDataAccessor.Instance.DesktopSession.PoliceInformation, currentCustomer);
                            if (returnValue)
                            {
                                MessageBox.Show("selected transactions released to claimant successfully");
                                //Print RTC form
                                foreach (var policeHold in policeHolds)
                                {
                                    policeHold.PoliceInformation = GlobalDataAccessor.Instance.DesktopSession.PoliceInformation;
                                    policeHold.RestitutionPaid = radioButtonYes.Checked;

                                    if (panelRestitution.Visible == true && radioButtonYes.Checked == true)
                                    {
                                        // there was restitution paid
                                        if (customTextBoxResAmount.Text.Trim() == "")
                                        {
                                            // Probably should have caught this before 
                                            policeHold.RestitutionAmount = 0;
                                            MessageBox.Show("Please enter the restitution amount!", "Restitution amount missing");
                                            customTextBoxResAmount.Focus();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        // No restitution paid
                                        policeHold.RestitutionPaid = false;
                                        policeHold.RestitutionAmount = 0;
                                    }

                                }
                                //Call print RTC if print is enabled
                                if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                                {
                                    var rtcprintFrm = new RTCform();
                                    rtcprintFrm.RTCLoans = policeHolds;

                                    rtcprintFrm.ShowDialog();
                                }
                                break;
                            }
                            dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                        }while (dgr == DialogResult.Retry);
                    }
                    else
                    {
                        MessageBox.Show("Background check not completed. selected transactions not released to claimant");
                        RemoveTempStatusOnLoans();
                    }

                    GlobalDataAccessor.Instance.DesktopSession.PoliceInformation = null;
                    GlobalDataAccessor.Instance.DesktopSession.ReleaseToClaimant = false;
                }
            }
            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void PoliceHoldReleaseInfo_Shown(object sender, EventArgs e)
        {
            if (ReleaseToClaimant)
            {
                this.labelHeading.Text = "Release to Claimant";
                this.labelReason.Text = "Reason for Release to Claimant";
                //SR 12/08/2009 "Restitution is not for the pilot build" - GJL
                //this.panelRestitution.Visible = true;
            }
            else if (PoliceSeize)
            {
                this.labelHeading.Text = "Police Seize - Information";
                this.labelReason.Text = "Reason for Seize";
                //SR 12/08/2009 "Restitution is not for the pilot build" - GJL
                //this.panelRestitution.Visible = true;
            }
        }

        private void radioButtonYes_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonYes.Checked)
                customTextBoxResAmount.Enabled = true;
            else
            {
                customTextBoxResAmount.Enabled = false;
            }
        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNo.Checked)
                customTextBoxResAmount.Enabled = false;
            else
            {
                customTextBoxResAmount.Enabled = true;
            }
        }

       
       
    }
}
