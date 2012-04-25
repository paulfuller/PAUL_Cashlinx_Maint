/********************************************************************
* Namespace: CashlinxDesktop.DesktopForms.Pawn.ShopAdministration
* FileName: SafeLogin
* This form is shown to enter safe credentials. The password entered
* is encrypted in order to compare it with the value in the DB.
* Sreelatha Rengarajan 2/1/2010 Initial version
 * SR 3/19/2010 Changed the username passed from whats entered to whats entered + _storenumber
*******************************************************************/

using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    public partial class SafeLogin : Form
    {
        
        private static readonly int MIN_PASSWORD_LEN = 6;

 


        public SAFEOPERATION SafeOperation
        {
            get;
            set;
        }

        public bool SafeValidated
        {
            get;
            set;
        }

  

        public SafeLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fires when the password text has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                TextBox passwordBox = (TextBox)sender;
                string passwordBoxTxt = passwordBox.Text;
                if (StringUtilities.isNotEmpty(passwordBoxTxt) && passwordBoxTxt.Length >= MIN_PASSWORD_LEN)
                {
                    this.safeloginFormLoginButton.Enabled = true;
                    return;
                }
                //If we make it here, disable login button
                this.safeloginFormLoginButton.Enabled = false;
            }
        }

        private void loginFormLoginButton_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                string userName = this.userTextBox.Text.ToLower();
                string passWord = this.passwordTextBox.Text;
                if (StringUtilities.isNotEmpty(userName) && StringUtilities.isNotEmpty(passWord))
                {
                    this.DialogResult = DialogResult.OK;
                    //Call the DB to verify if the safe user name and
                    //password are valid
                    //Encrypt the password value entered to compare against the
                    //encrypted password stored in the DB
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(passWord);
                    var hasher = new MD5CryptoServiceProvider();

                    byte[] hBytes = hasher.ComputeHash(data);
                    var sb = new StringBuilder();
                    for (int c = 0; c < hBytes.Length; c++)
                    {
                        sb.AppendFormat("{0:x2}", hBytes[c]);
                    }
                    string encryptedPassword = sb.ToString();
                    string errorCode ;
                    string errorMsg;
                    string storeNumber=GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                    bool retValue = ShopCashProcedures.VerifySafeUser(string.Format("{0}_{1}", userName, storeNumber), encryptedPassword, 
                        storeNumber, GlobalDataAccessor.Instance.DesktopSession,
                        out errorCode, out errorMsg);
                    if (retValue)
                    {
                        //User is verified
                        //If the safe operation is OPEN go ahead and call open safe
                        if (SafeOperation == SAFEOPERATION.OPEN)
                        {
                            string cdID = GlobalDataAccessor.Instance.DesktopSession.StoreSafeID;
                            const int cashDrawerStatus = (int)CASHDRAWERSTATUS.OPEN;
                            string workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                            retValue = ShopCashProcedures.UpdateSafeStatus(cdID, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                GlobalDataAccessor.Instance.DesktopSession.FullUserName, ShopDateTime.Instance.ShopDate.FormatDate(), cashDrawerStatus.ToString(),
                                workstationID, GlobalDataAccessor.Instance.DesktopSession,
                                out errorCode, out errorMsg);
                        }
                        if (retValue)
                        SafeValidated = true;

                    }
 
                }
                else
                {
                    this.DialogResult = DialogResult.Abort;
                }
            }
            else
            {
                this.DialogResult = DialogResult.Abort;
            }
            this.Close();
        }

        private void loginFormCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}