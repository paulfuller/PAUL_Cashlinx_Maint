/*************************************************************************************
* CashlinxDesktop.ModalForms
* ManageOverrides
* This form is shown when a manager override is needed in a use case
* Sreelatha Rengarajan 3/13/2009 Initial version
* Sreelatha Rengarajan 4/28/2009   Updated to include LDAP calls and 
*                                  changed error messages to come from resource files
*SR 6/09/2010 Updated to include checks on whether the overriding user has the
*correct limits to override
***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Network;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Forms.Pawn.Loan
{
    public partial class ManageOverrides : CustomBaseForm
    {
        private readonly string trigger = "";
        private List<decimal> _OverrideAmount;
        private List<decimal> _SuggestedLoanAmount;
        private bool _overrideAllowed = false;
        private string _errorMsg = "";
        string _infoMessage = "";
        private bool showReasonCombobox = false;
        private static UserVO managerUserProfile;

        private static readonly int MIN_USERNAME_LEN = 6;
        private static readonly int MIN_PASSWORD_LEN = 7;

        public const string SHOPPINGCART_TRIGGER = "Shopping cart";
        public const string DESCRIBEMERCH_TRIGGER = "Describe Item";
        public const string PRODUCTDETAILS_TRIGGER = "Product Details";
        public const string OVERRIDE_TRIGGER = "Override";
        public const string VOID_TRIGGER = "Void";

        public DesktopSession DesktopSession { get; private set; }

        public string ManagerUserName
        {
            get
            {
                return userNameTextbox.Text.Trim();
            }
        }

        /// <summary>
        /// Error message is set when the override failed
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return _errorMsg;
            }
        }

        /// <summary>
        /// Set to indicate if the override was successful or not
        /// If set to false, use the error set in ErrorMessage
        /// </summary>
        public bool OverrideAllowed
        {
            get
            {
                return _overrideAllowed;
            }
        }

        public string MessageToShow
        {
            set
            {
                _infoMessage = value;
            }
            get
            {
                return _infoMessage;
            }
        }

        public List<ManagerOverrideType> ManagerOverrideTypes { private get; set; }

        public List<ManagerOverrideTransactionType> ManagerOverrideTransactionTypes { private get; set; }

        public List<int> TransactionNumbers { private get; set; }

        public ManageOverrides(DesktopSession desktopSession, string trigger)
        {
            DesktopSession = desktopSession;
            this.trigger = trigger;
            InitializeComponent();
            _OverrideAmount = new List<decimal>();
            _SuggestedLoanAmount = new List<decimal>();
            TransactionNumbers = new List<int>();
            ManagerOverrideTransactionTypes = new List<ManagerOverrideTransactionType>();
            ManagerOverrideTypes = new List<ManagerOverrideType>();
        }

        public ManageOverrides(DesktopSession desktopSession, string trigger, decimal overrideAmount)
        {
            DesktopSession = desktopSession;
            this.trigger = trigger;
            InitializeComponent();
            _OverrideAmount = new List<decimal>();
            _SuggestedLoanAmount = new List<decimal>();
            TransactionNumbers = new List<int>();
            ManagerOverrideTransactionTypes = new List<ManagerOverrideTransactionType>();
            ManagerOverrideTypes = new List<ManagerOverrideType>();
            _OverrideAmount.Add(overrideAmount);
        }

        /// <summary>
        /// Pass the trigger, override amount, suggested loan amount and transaction type
        /// The valid transaction types are L,A, P and T. If something other than these transaction
        /// types are passed, an exception is returned to the calling program
        /// </summary>
        /// <param name="desktopSession"> </param>
        /// <param name="trigger"></param>
        /// <param name="iTransactionNumber"></param>
        /// <param name="overrideAmt"></param>
        /// <param name="suggLoanAmt"></param>
        /// <param name="managerOverrideTransactionType"></param>
        /// <param name="managerOverrideType"></param>
        public ManageOverrides(DesktopSession desktopSession, string trigger, int iTransactionNumber, decimal overrideAmt, decimal suggLoanAmt, ManagerOverrideTransactionType managerOverrideTransactionType, ManagerOverrideType managerOverrideType)
        {
            this.DesktopSession = desktopSession;
            this.trigger = trigger;
            InitializeComponent();
            _OverrideAmount = new List<decimal>();
            _SuggestedLoanAmount = new List<decimal>();
            TransactionNumbers = new List<int>();
            ManagerOverrideTransactionTypes = new List<ManagerOverrideTransactionType>();
            ManagerOverrideTypes = new List<ManagerOverrideType>();

            _OverrideAmount.Add(overrideAmt);
            _SuggestedLoanAmount.Add(suggLoanAmt);
            TransactionNumbers.Add(iTransactionNumber);
            ManagerOverrideTransactionTypes.Add(managerOverrideTransactionType);
            ManagerOverrideTypes.Add(managerOverrideType);
        }

        private void ManageOverrides_Load(object sender, EventArgs e)
        {
            decimal difference;
            decimal diff;
            switch (trigger)
            {
                case SHOPPINGCART_TRIGGER:
                    _infoMessage = Commons.GetMessageString("ManageOverrideLoanMsg") + _OverrideAmount[0] + " " + Commons.GetMessageString("ManageOverrideLoanMsg2") + System.Environment.NewLine + Commons.GetMessageString("ManageOverrideMgrMsg");
                    break;
                case PRODUCTDETAILS_TRIGGER:
                    _infoMessage = Commons.GetMessageString("ManageOverridePFIMsg") + System.Environment.NewLine + " " + Commons.GetMessageString("ManageOverrideMgrMsg");
                    break;
                case DESCRIBEMERCH_TRIGGER:
                    difference = _OverrideAmount[0] - _SuggestedLoanAmount[0];
                    diff = -difference;
                    foreach (ManagerOverrideTransactionType type in ManagerOverrideTransactionTypes)
                    {
                        if ((type == ManagerOverrideTransactionType.PUR))
                        {
                            if (difference < 0)
                            {
                                _infoMessage = string.Format("{0}{1} {2}{3}{4}{5}!{3}{6}", Commons.GetMessageString("ManageOverridePurchaseMsg"), _OverrideAmount[0], Commons.GetMessageString("ManageOverrideDescMercMsgLess"), System.Environment.NewLine, Commons.GetMessageString("ManageOverrideDescMercMsg2"), diff.ToString(), Commons.GetMessageString("ManageOverrideMgrMsg"));
                            }
                            else
                            {
                                _infoMessage = string.Format("{0}{1} {2}{3}{4}{5}!{3}{6}", Commons.GetMessageString("ManageOverridePurchaseMsg"), _OverrideAmount[0], Commons.GetMessageString("ManageOverrideDescMercMsgMore"), System.Environment.NewLine, Commons.GetMessageString("ManageOverrideDescMercMsg2"), difference.ToString(), Commons.GetMessageString("ManageOverrideMgrMsg"));
                            }
                        }
                        else
                        {
                            if (difference < 0)
                            {
                                _infoMessage = string.Format("{0}{1} {2}{3}{4}{5}!{3}{6}", Commons.GetMessageString("ManageOverrideLoanMsg"), _OverrideAmount[0], Commons.GetMessageString("ManageOverrideDescMercMsgLess"), System.Environment.NewLine, Commons.GetMessageString("ManageOverrideDescMercMsg2"), diff.ToString(), Commons.GetMessageString("ManageOverrideMgrMsg"));
                            }
                            else
                            {
                                _infoMessage = string.Format("{0}{1} {2}{3}{4}{5}!{3}{6}", Commons.GetMessageString("ManageOverrideLoanMsg"), _OverrideAmount[0], Commons.GetMessageString("ManageOverrideDescMercMsgMore"), System.Environment.NewLine, Commons.GetMessageString("ManageOverrideDescMercMsg2"), difference.ToString(), Commons.GetMessageString("ManageOverrideMgrMsg"));
                            }
                        }
                       
                    }
                   
                    this.Location = new Point(this.Location.X - 50, this.Location.Y - 50);
                    break;
                case OVERRIDE_TRIGGER:
                    _infoMessage=MessageToShow;
                    break;
                default:
                    _infoMessage = Commons.GetMessageString("ManageOverrideDefaultMessage");
                    break;
            }
            if (_infoMessage.Length != 0)
            {
                infoMessageLine1Label.Text = _infoMessage;
            }
            errorMessageLabel.Text = "";

            // Only show Manager Override ComboBox if coming from DescribeItem on a Loan

            foreach (ManagerOverrideTransactionType type in ManagerOverrideTransactionTypes)
            {
                if ((type == ManagerOverrideTransactionType.NL && trigger != OVERRIDE_TRIGGER) ||
                    type == ManagerOverrideTransactionType.PFI)
                {
                    showReasonCombobox = true;
                    break;
                }
            }
            foreach (ManagerOverrideType ovrType in ManagerOverrideTypes)
            {
                //For police hold or police hold during PFI do not show reason box
                if (ovrType == ManagerOverrideType.PHOLD || ovrType == ManagerOverrideType.PFIP)
                {
                    showReasonCombobox = false;
                    break;
                }
                if (ovrType == ManagerOverrideType.PKV)
                {
                    showReasonCombobox = true;
                    break;
                }
            }
            if (showReasonCombobox)
            {
                // Populate Combo Box Reason Codes from Enum
                reasonComboBox.DataSource = DesktopSession.ManagerOverrideReasonCodes;
                reasonComboBox.DisplayMember = "Right";
                reasonComboBox.ValueMember = "Left";
            }
            else
            {
                reasonLabel.Visible = false;
                reasonComboBox.Visible = false;
            }
            
            this.userNameTextbox.Focus();
        }

        private void userNameTextbox_TextChanged(object sender, EventArgs e)
        {
            //Make a copy of the user text box data so that any decryption
            //attempt does not affect
            var txt = new string(this.userNameTextbox.Text.ToCharArray());

            //If the user array is not empty, attempt to parse it into 
            //user name and password
            if (!string.IsNullOrEmpty(txt) && !DesktopSession.ScannedCredentials)
            {
                this.DesktopSession.ScanParse(this, txt, userNameTextbox, passwordTextBox, submitButton);
            }

            if (!string.IsNullOrEmpty(txt) && txt.Length >= MIN_USERNAME_LEN)
            {
                this.passwordTextBox.Enabled = true;
                if (!string.IsNullOrEmpty(passwordTextBox.Text) &&
                    passwordTextBox.Text.Length >= MIN_PASSWORD_LEN)
                    submitButton.Enabled = true;
                return;
            }

            //If we make it here, disable related forms
            this.passwordTextBox.Enabled = false;
            submitButton.Enabled = false;

           /* if (passwordTextBox.Text.Trim().Length >= MIN_PASSWORD_LEN && userNameTextbox.Text.ToString().Trim().Length >= MIN_USERNAME_LEN)
                submitButton.Enabled = true;
            else
                submitButton.Enabled = false;*/
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            string passwordBoxTxt = passwordTextBox.Text;
            if (!string.IsNullOrEmpty(passwordBoxTxt) &&
                passwordBoxTxt.Length >= MIN_PASSWORD_LEN)
            {
                this.submitButton.Enabled = true;
                return;
            }
            this.submitButton.Enabled = false;

        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            errorMessageLabel.Text = "";
            string userName = userNameTextbox.Text.Trim();
            string overridingUserId = userNameTextbox.Text.Trim();
            string userPassword = passwordTextBox.Text.Trim();
            
            //Make a call to the LDAP service
            try
            {
                if (callLdap(userName, userPassword))
                {
                    //if the call to LDAP was successful, check that the user has the right permissions to do the override
                    if (checkRole(userName))
                    {
                        if (_OverrideAmount.Count == 0)
                        {
                            for (int i = 0; i < ManagerOverrideTransactionTypes.Count; i++)
                                _OverrideAmount.Add(0);
                        }
                        if (_SuggestedLoanAmount.Count == 0)
                        {
                            for (int i = 0; i < ManagerOverrideTransactionTypes.Count; i++)
                                _SuggestedLoanAmount.Add(0);
                        }
                        if ( TransactionNumbers.Count == 0)
                        {
                            for (int i = 0; i < ManagerOverrideTransactionTypes.Count; i++)
                                TransactionNumbers.Add(0);
                        }
                        var auditLogMap = new Dictionary<string, object>();
                        auditLogMap.Add(DesktopSessionConstants.AUDIT_STORENUMBER, DesktopSession.CurrentSiteId.StoreNumber);
                        auditLogMap.Add(DesktopSessionConstants.AUDIT_OVERRIDE_ID, overridingUserId);
                        if (ManagerOverrideTransactionTypes.Count > 0)
                        {
                            int auditArraySize = ManagerOverrideTransactionTypes.Count;

                            auditLogMap.Add(DesktopSessionConstants.AUDIT_CARDINALITY, auditArraySize);
                            auditLogMap.Add(DesktopSessionConstants.AUDIT_OVERRIDE_TRANS_TYPE, ManagerOverrideTransactionTypes.ToArray());
                            auditLogMap.Add(DesktopSessionConstants.AUDIT_OVERRIDE_TYPE, ManagerOverrideTypes.ToArray());
                            auditLogMap.Add(DesktopSessionConstants.AUDIT_OVERRIDE_SUGGVAL, _SuggestedLoanAmount.ToArray());
                            auditLogMap.Add(DesktopSessionConstants.AUDIT_OVERRIDE_APPRVAL, _OverrideAmount.ToArray());
                            auditLogMap.Add(DesktopSessionConstants.AUDIT_OVERRIDE_TRANSNUM, TransactionNumbers.ToArray());
                            // Only provide Manager Override reason if from Describe Item on a Loan
                            if (showReasonCombobox)
                            {
                                var managerOverrideReason = (ManagerOverrideReason)reasonComboBox.SelectedValue;
                                auditLogMap.Add(DesktopSessionConstants.AUDIT_OVERRIDE_COMMENT, managerOverrideReason.ToString());
                            }
                        }
                        //if the user is authorized to override, create an audit log
                        if (AuditLogger.Instance.IsEnabled)
                        {
                            AuditLogger.Instance.LogAuditMessage(AuditLogType.OVERRIDE, auditLogMap);
                        }
                        //createAuditLog(overridingUserId, csrUserId, terminal, this._transactionType);
                        _overrideAllowed = true;
                        _errorMsg = "";

                        Close();
                    }
                    else
                    {
                        errorMessageLabel.Text = Commons.GetMessageString("ManageOverrideUsernotAllowerd");
                        _overrideAllowed = false;
                        _errorMsg = errorMessageLabel.Text;
                    }
                }
                else
                {
                    errorMessageLabel.Text = Commons.GetMessageString("ManageOverrideLoginFailed");
                    _overrideAllowed = false;
                    _errorMsg = errorMessageLabel.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BasicExceptionHandler.Instance.AddException("Failure to validate user ", new ApplicationException());
                _overrideAllowed = false;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Function to call LDAP to validate the user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool callLdap(string userName, string password)
        {
            if (DesktopSession.IsSkipLDAP)
            {
                return (true);
            }
            //See if ldap is enabled
            var ldapObj = PawnLDAPAccessor.Instance;
            if (ldapObj.State == PawnLDAPAccessor.LDAPState.CONNECTED)
            {
                var refCount = 0;
                bool lockedOut;
                string userDisplayName;
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
                    if (!lockedOut)
                    {
                        return true;
                    }
                    MessageBox.Show(
                        "You are now locked out of the system.  Please call Shop System Support",
                        "Application Security");
                    try
                    {
                        Application.Exit();
                        return (false);
                    }
                    catch
                    { 
                        throw new ApplicationException("Application security violation during an override.  Manager is now locked out");
                    }
                    finally
                    {
                        throw new ApplicationException("Application security violation during an override.  Manager is now locked out");
                    }
                }
            }
            else
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot authorize override. The LDAP connection is not active");
                BasicExceptionHandler.Instance.AddException("Cannot authorize override.  The LDAP connection is not active", new ApplicationException());
            }
            return (false);
        }

        /// <summary>
        /// Function to to check if the user is authorized to override
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool checkRole(string userName)
        {
            string errorCode;
            string errorMesg;
            //Get the security profile of the user whose credentials were entered in the override form
            if (SecurityProfileProcedures.GetUserSecurityProfile(userName, DesktopSession.CurrentSiteId.StoreNumber, null, "N", DesktopSession, out managerUserProfile, out errorCode, out errorMesg))
            {
                //check if the manager whose credentials were entered
                //has modify access on the override resource
                //return true if yes and false if not
                if (SecurityProfileProcedures.CanUserModifyResource("OVERRIDE", managerUserProfile, DesktopSession))
                {
                    //If the override type is for new pawn loan override
                    //check that the overriding user's limits allow override
                    decimal managerLimit = 0.0m;
                    if (ManagerOverrideTypes[0] == ManagerOverrideType.NLO)
                    {
                        if (!(SecurityProfileProcedures.CanUserOverridePawnLoanLimit(managerUserProfile, _OverrideAmount[0], DesktopSession, new BusinessRulesProcedures(DesktopSession), out managerLimit)))
                        {
                            MessageBox.Show(@"The amount to override exceeds the amount you can override. Your override limit is " + managerLimit);
                            return false;
                        }
                    }
                    if (ManagerOverrideTypes[0] == ManagerOverrideType.PURO)
                    {
                        if (!(SecurityProfileProcedures.CanUserOverrideBuyLimit(managerUserProfile, _OverrideAmount[0], out managerLimit)))
                        {
                            MessageBox.Show(@"The amount to override exceeds the amount you can override. Your override limit is " + managerLimit);
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        protected override System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.ActiveControl.TabIndex != this.submitButton.TabIndex - 1 &&
                    this.ActiveControl.TabIndex != this.submitButton.TabIndex)
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                else
                    this.submitButton_Click(submitButton, new EventArgs());
                return true;
            }
            if ((keyData == (Keys.Control | Keys.C)) || (keyData == (Keys.Control | Keys.V)) || (keyData == (Keys.Control | Keys.X)))
            {

                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void submitButton_EnabledChanged(object sender, EventArgs e)
        {
            if (submitButton.Enabled)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.ScannedCredentials)
                {
                    GlobalDataAccessor.Instance.DesktopSession.ScannedCredentials = false;
                    this.submitButton_Click(this, new EventArgs());
                }
            }
        }
    }
}
