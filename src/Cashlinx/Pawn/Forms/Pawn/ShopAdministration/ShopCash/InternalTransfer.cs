using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Network;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
using Pawn.Logic.PrintQueue;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    public partial class InternalTransfer : Form
    {
        private const string DENOMINATIONCURRENCY = "USD";
        private bool DestinationUserValidated;
        private bool TransferFromSafe;
        private string userDisplayName;
        private bool transferUserSafeAccess;
        private bool TransferToSafe;
        private const int MIN_USERNAME_LEN = 6;
        private const int MIN_PASSWORD_LEN = 7;
        private bool checkPassed = true;
        private bool initialValues = true;

        public InternalTransfer()
        {
            InitializeComponent();
        }

        private void InternalTransfer_Load(object sender, EventArgs e)
        {
            //Get the list of cash drawers that do not belong to the logged in user
            DataTable cdAssignments = GlobalDataAccessor.Instance.DesktopSession.StoreCashDrawerAssignments;
            DataRowCollection cdRows = null;
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.INTERNALSAFETRANSFER, StringComparison.OrdinalIgnoreCase))
                TransferFromSafe = true;

            if (cdAssignments != null)
            {
                cdRows = cdAssignments.Rows;
            }
            if (cdRows != null)
            {
                ArrayList cdData = new ArrayList();
                foreach (DataRow dr in cdRows)
                {
                    if (TransferFromSafe)
                    {
                        //Source will be safe and destination cannot have safe
                        if (dr["name"].ToString() != GlobalDataAccessor.Instance.DesktopSession.StoreSafeName)
                        {
                            cdData.Add(new ComboBoxData(dr["id"].ToString(), dr["name"].ToString()));
                        }
                    }
                    else
                    {
                        //Source will be the logged in user's cash drawer and the destination
                        //can have safe but not the user's cash drawer
                        if (dr["id"].ToString() != GlobalDataAccessor.Instance.DesktopSession.CashDrawerId)
                        {
                            cdData.Add(new ComboBoxData(dr["id"].ToString(), dr["name"].ToString()));
                        }
                    }
                }
                
                comboBoxCashDrawerData.DataSource = cdData;
                comboBoxCashDrawerData.DisplayMember = "Description";
                comboBoxCashDrawerData.ValueMember = "Code";
                initialValues = false;
                comboBoxCashDrawerData.SelectedIndex = -1;
            }
            if (TransferFromSafe)
                labelCashDrawerName.Text = GlobalDataAccessor.Instance.DesktopSession.StoreSafeName;
            else
                labelCashDrawerName.Text = GlobalDataAccessor.Instance.DesktopSession.CashDrawerName;
            labelTransferDate.Text = ShopDateTime.Instance.ShopDate.ToShortDateString();
            currencyEntry1.Calculate +=currencyEntry1_Calculate;
            currencyEntry1.OtherTenderClick += currencyEntry1_OtherTenderClick;
            //Do not show the currency panel when it first loads
            panelCurrency.Visible = false;
            pictureBox1.Image = Common.Properties.Resources.plus_icon_small;
            panel1.Location = new Point(13, 97);
            this.Size = new Size(777, 673);
            if (!checkPassed)
                this.Close();
        }

        void currencyEntry1_Calculate(decimal currencyTotal)
        {
            customTextBoxTrAmount.Text = string.Format("{0:C}", currencyTotal);
        }

        void currencyEntry1_OtherTenderClick()
        {
            panel1.Visible = !panel1.Visible;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panelCurrency.Visible)
            {
                panelCurrency.Visible = false;
                pictureBox1.Image = Common.Properties.Resources.plus_icon_small;
                panel1.Location = new Point(13, 97);
                this.Size = new Size(777, 421);
            }
            else
            {
                panelCurrency.Visible = true;
                pictureBox1.Image = Common.Properties.Resources.minus_icon_small;
                panel1.Location = new Point(10, 421);
                this.Size = new Size(777, 673);
            }
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            string errorCode;
            string errorText;

            //Validate the user
            if (callLdap(customTextBoxDestUser.Text, customTextBoxDestUserPwd.Text))
            {
                var cashdrawerstatus = string.Empty;
                if ((TransferToSafe && transferUserSafeAccess) || (!TransferToSafe && GlobalDataAccessor.Instance.DesktopSession.IsUserAssignedCashDrawer(customTextBoxDestUser.Text,
                                                                                                                                               comboBoxCashDrawerData.Text.ToString(), out cashdrawerstatus)))
                {
                    if (cashdrawerstatus != "1")
                    {
                        //Check if the cash drawer was balanced today
                        //If it is we show a message that the cash drawer needs to be rebalanced
                        //else proceed.
                        bool cashdrawerbalanced;
                        ShopCashProcedures.CheckCashDrawerBalanced(comboBoxCashDrawerData.SelectedValue.ToString(),
                                                                   ShopDateTime.Instance.ShopDate.FormatDate(), GlobalDataAccessor.Instance.DesktopSession,
                                                                   out cashdrawerbalanced, out errorCode, out errorText);
                        if (cashdrawerbalanced)
                        {
                            MessageBox.Show(Commons.GetMessageString("WM02RebalanceDrawerRequired") + " " +
                                            comboBoxCashDrawerData.Text);
                        }
                        //Open the cash drawer of the destination user
                        string errorMsg;
                        string cdID = comboBoxCashDrawerData.SelectedValue.ToString();
                        const int cashDrawerStatus = (int)CASHDRAWERSTATUS.OPEN;
                        string workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;

                        if (!ShopCashProcedures.UpdateSafeStatus(cdID, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                            customTextBoxDestUser.Text.ToString(), ShopDateTime.Instance.ShopDate.FormatDate(), cashDrawerStatus.ToString(),
                                                            workstationID,GlobalDataAccessor.Instance.DesktopSession,
                                                            out errorCode, out errorMsg))
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error when trying to open the cash drawer" + errorMsg);
                        }
                        
                    }
                    DestinationUserValidated = true;
                }
                else
                {
                    MessageBox.Show(@"The destination user is not authorized to receive transfer to the selected cash drawer or safe");
                    return;
                }
            }
            else
            {
                MessageBox.Show(@"Destination user credentials invalid");
                return;
            }
            string transferAmount = customTextBoxTrAmount.Text;
            if (transferAmount.StartsWith("$"))
                transferAmount = transferAmount.Substring(1);

            if (Utilities.GetDecimalValue(transferAmount, 0) == 0)
            {
                MessageBox.Show("Transfer amount cannot be 0");
                return;
            }
            string sourceCashDrawerId;
            if (TransferFromSafe)
                sourceCashDrawerId = GlobalDataAccessor.Instance.DesktopSession.StoreSafeID;
            else
                sourceCashDrawerId = GlobalDataAccessor.Instance.DesktopSession.CashDrawerId;
            string selectedCashDrawerId = comboBoxCashDrawerData.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(selectedCashDrawerId) && !string.IsNullOrEmpty(customTextBoxTrAmount.Text) &&
                DestinationUserValidated)
            {
                int transferNumber;
                GlobalDataAccessor.Instance.beginTransactionBlock();
                bool retVal = ShopCashProcedures.InsertcashTransfer(sourceCashDrawerId, selectedCashDrawerId,
                                                                    Utilities.GetDecimalValue(transferAmount, 0), DENOMINATIONCURRENCY,
                                                                    ShopDateTime.Instance.ShopDate.FormatDate().ToString() + " " + ShopDateTime.Instance.ShopTime.ToString(), GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID,
                                                                    customTextBoxDestUser.Text, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,GlobalDataAccessor.Instance.DesktopSession,
                                                                    out transferNumber, out errorCode, out errorText);

                if (retVal)
                {
                    GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                    MessageBox.Show(@"Internal Cash Transfer Created Successfully");
                    string destinationUser = customTextBoxDestUser.Text;
                    CashTransferVO cashTransferdata = new CashTransferVO();
                    cashTransferdata.TransferNumber = transferNumber;
                    cashTransferdata.TransferStatus = CashTransferStatusCodes.ACCEPTED.ToString();
                    cashTransferdata.TransferAmount = Utilities.GetDecimalValue(transferAmount, 0);
                    cashTransferdata.SourceEmployeeName = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserFirstName + " " + GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserLastName;
                    string sourceUserName = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName;
                    cashTransferdata.SourceEmployeeNumber = sourceUserName.Substring(sourceUserName.Length - 5, 5);
                    if (TransferFromSafe)
                        cashTransferdata.SourceDrawerName = GlobalDataAccessor.Instance.DesktopSession.StoreSafeName;
                    else
                        cashTransferdata.SourceDrawerName = GlobalDataAccessor.Instance.DesktopSession.CashDrawerName;
                    cashTransferdata.DestinationDrawerName = comboBoxCashDrawerData.Text.ToString();
                    cashTransferdata.DestinationEmployeeName = userDisplayName;
                    cashTransferdata.DestinationEmployeeNumber = destinationUser.Substring(destinationUser.Length - 5, 5);
                    var sourceSite = new SiteId();
                    sourceSite = GlobalDataAccessor.Instance.CurrentSiteId;
                    cashTransferdata.SourceShopInfo = sourceSite;
                    var bankTransferFrm = new BankAndInternalCashTransfer();
                    bankTransferFrm.CashTransferdata = cashTransferdata;
                    bankTransferFrm.TransferToBank = false;
                    bankTransferFrm.InternalTransfer = true;
                    bankTransferFrm.ShowDialog();
                }
                else
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Internal Cash Transfer from store failed " + errorText);
                    GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Fill in all the required information and submit");
                return;
            }
        }

        private void checkCashDrawerStatus()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Function to call LDAP to validate the user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool callLdap(string userName, string password)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.IsSkipLDAP)
            {
                return (true);
            }

            //See if ldap is enabled
            var ldapObj = PawnLDAPAccessor.Instance;
            if (ldapObj.State == PawnLDAPAccessor.LDAPState.CONNECTED)
            {
                var refCount = 0;
                bool lockedOut;

                DateTime lastModified;
                string[] pwdHistory;
                if (ldapObj.AuthorizeUser(
                    userName,
                    password,
                    ref refCount,
                    out lastModified,
                    out pwdHistory,
                    out userDisplayName,
                    out lockedOut))
                {
                    if (lockedOut)
                    {
                        MessageBox.Show(
                            "You are now locked out of the system.  Please call Shop System Support",
                            "Application Security");
                        return false;
                    }
                }
                else
                    return false;
                if (TransferToSafe)
                {
                    string errorCode;
                    string errorMesg;
                    UserVO transferUser;
                    if (!SecurityProfileProcedures.GetUserSecurityProfile(userName, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, "", "N", GlobalDataAccessor.Instance.DesktopSession, 
                        out transferUser, out errorCode, out errorMesg))
                    {
                        BasicExceptionHandler.Instance.AddException(
                            "Security Profile could not be loaded for the logged in user. Cannot Authorize",
                            new ApplicationException());
                        MessageBox.Show(
                            "User's security profile could not be loaded. Exiting the application");
                        return false;
                    }

                    //Check if the user who logged in for destination has safe access if
                    //the transfer is done to safe from cash drawer
                    if (SecurityProfileProcedures.CanUserModifyResource(
                        "SAFEMANAGEMENT", transferUser, GlobalDataAccessor.Instance.DesktopSession))
                        transferUserSafeAccess = true;
                }
            }
            else
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot validate destination cash drawer user. The LDAP connection is not active");
                BasicExceptionHandler.Instance.AddException("Cannot authorize transfer.  The LDAP connection is not active", new ApplicationException());
            }
            return (true);
        }

        private void customTextBoxTrAmount_Leave(object sender, EventArgs e)
        {
            if (customTextBoxTrAmount.isValid)
            {
                if (customTextBoxTrAmount.Text.StartsWith("$"))
                    customTextBoxTrAmount.Text = customTextBoxTrAmount.Text.Substring(1);

                customTextBoxTrAmount.Text = string.Format("{0:C}", Utilities.GetDecimalValue(customTextBoxTrAmount.Text, 0));
            }
        }

        private void comboBoxCashDrawerData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!initialValues)
            {
                if (comboBoxCashDrawerData.Text.ToString() == GlobalDataAccessor.Instance.DesktopSession.StoreSafeName)
                    TransferToSafe = true;
                else
                    TransferToSafe = false;
                //SR 06/12/2012
                //Whether it is transfer to safe or transfer to another cashdrawer check that
                //safe is not being balanced right now. If it is, transfer should not be allowed until balance
                //operation is complete
                string wrkId;
                string cdEvent;
                string errorCode;
                string errorMesg;


                bool retValue = ShopCashProcedures.GetTellerEvent(GlobalDataAccessor.Instance.DesktopSession.StoreSafeID, GlobalDataAccessor.Instance.DesktopSession, out wrkId, out cdEvent, out errorCode, out errorMesg);
                if (retValue)
                {

                    if (errorCode != "100")
                    {
                        if (cdEvent.ToUpper().Contains("BALANCE"))
                        {
                            MessageBox.Show("There is a safe balance event in progress. Please complete that operation first");
                            checkPassed = false;
                            return;
                        }
                    }
                }
                if (!TransferToSafe && comboBoxCashDrawerData.SelectedIndex >= 0)
                {
                    //Check that the destination drawer is not being balanced
                    retValue = ShopCashProcedures.GetTellerEvent(comboBoxCashDrawerData.SelectedValue.ToString(), GlobalDataAccessor.Instance.DesktopSession, out wrkId, out cdEvent, out errorCode, out errorMesg);
                    if (retValue)
                    {

                        if (errorCode != "100")
                        {
                            if (cdEvent.ToUpper().Contains("BALANCE"))
                            {
                                MessageBox.Show(comboBoxCashDrawerData.Text + " balance event is in progress. Please complete that operation first");
                                checkPassed = false;
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void customTextBoxDestUser_TextChanged(object sender, EventArgs e)
        {
            var txt = new string(this.customTextBoxDestUser.Text.ToCharArray());

            //If the user array is not empty, attempt to parse it into 
            //user name and password
            if (!string.IsNullOrEmpty(txt) && !GlobalDataAccessor.Instance.DesktopSession.ScannedCredentials)
            {
                GlobalDataAccessor.Instance.DesktopSession.ScanParse(this, txt, this.customTextBoxDestUser, this.customTextBoxDestUserPwd, this.customButtonSubmit);
            }
            if (!string.IsNullOrEmpty(txt) && txt.Length >= MIN_USERNAME_LEN)
            {
                this.customTextBoxDestUserPwd.Enabled = true;
                if (!string.IsNullOrEmpty(customTextBoxDestUserPwd.Text) &&
                    customTextBoxDestUserPwd.Text.Length >= MIN_PASSWORD_LEN)
                    this.customButtonSubmit.Enabled = true;
                return;
            }
            //If we make it here, disable related forms
            this.customTextBoxDestUserPwd.Enabled = false;
            this.customButtonSubmit.Enabled = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData == (Keys.Control | Keys.C)) || (keyData == (Keys.Control | Keys.V)) || (keyData == (Keys.Control | Keys.X)))
            {

                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }



        private void customTextBoxDestUserPwd_TextChanged(object sender,EventArgs e)
        {
            string passwordBoxTxt = customTextBoxDestUserPwd.Text;
            if (!string.IsNullOrEmpty(passwordBoxTxt) &&
                passwordBoxTxt.Length >= MIN_PASSWORD_LEN)
            {
                this.customButtonSubmit.Enabled = true;
                return;
            }
            this.customButtonSubmit.Enabled = false;

        }


        private void customButtonSubmit_EnabledChanged(object sender, EventArgs e)
        {
            if (customButtonSubmit.Enabled)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.ScannedCredentials)
                {
                    GlobalDataAccessor.Instance.DesktopSession.ScannedCredentials = false;
                    this.customButtonSubmit_Click(this, new EventArgs());
                }
            }
        }
    }
}
