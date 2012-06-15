using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Common.Libraries.Utility.String;

namespace Common.Libraries.Forms
{
    public partial class UserLogin : Form
    {
        private const int MIN_USERNAME_LEN = 6;
        private const int MIN_PASSWORD_LEN = 7;

        private string enteredUserName;
        private string enteredPassWord;
        private DesktopSession desktopSession;

        public string EnteredUserName
        {
            get
            {
                return (enteredUserName);
            }
            set
            {
                enteredUserName = value;
                userTextBox.Text = value;
            }
        }

        public string EnteredPassWord
        {
            get
            {
                return (enteredPassWord);
            }
            set
            {
                passwordTextBox.Text = value;
                enteredPassWord = value;
            }
        }

        public DesktopSession DesktopSession
        {
            get
            {
                return (desktopSession);
            }
        }

        public UserLogin(DesktopSession iSession)
        {
            if (iSession == null)
            {
                throw new ApplicationException("Must have a valid desktop session object prior to log in!");
            }
            this.desktopSession = iSession;
            InitializeComponent();
        }

        private void userTextBox_TextChanged(object sender, EventArgs e)
        {
            //Make a copy of the user text box data so that any decryption
            //attempt does not affect
            var txt = new string(this.userTextBox.Text.ToCharArray());

            //If the user array is not empty, attempt to parse it into 
            //user name and password
            if (!string.IsNullOrEmpty(txt) && !GlobalDataAccessor.Instance.DesktopSession.ScannedCredentials)
            {
                this.desktopSession.ScanParse(this, txt, this.userTextBox, this.passwordTextBox, this.loginFormLoginButton);
                
            }

            if (!string.IsNullOrEmpty(txt) && txt.Length >= MIN_USERNAME_LEN)
            {
                this.passwordTextBox.Enabled = true;
                if (!string.IsNullOrEmpty(passwordTextBox.Text) &&
                    passwordTextBox.Text.Length >= MIN_PASSWORD_LEN)
                    this.loginFormLoginButton.Enabled = true;
                return;
            }
            //If we make it here, disable related forms
            this.passwordTextBox.Enabled = false;
            this.loginFormLoginButton.Enabled = false;
        }

        /// <summary>
        /// Fires when the password text has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            string passwordBoxTxt = passwordTextBox.Text;
            if (!string.IsNullOrEmpty(passwordBoxTxt) && 
                passwordBoxTxt.Length >= MIN_PASSWORD_LEN)
            {
                this.loginFormLoginButton.Enabled = true;
                return;
            }
            this.loginFormLoginButton.Enabled = false;
        }

        private void loginFormLoginButton_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                string userName = this.userTextBox.Text;
                string passWord = this.passwordTextBox.Text;
                if (StringUtilities.isNotEmpty(userName) && StringUtilities.isNotEmpty(passWord))
                {
                    this.DialogResult = DialogResult.OK;
                    this.enteredUserName = userName;
                    this.enteredPassWord = passWord;
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

        protected override System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.ActiveControl.TabIndex != this.loginFormLoginButton.TabIndex-1 &&
                    this.ActiveControl.TabIndex != this.loginFormLoginButton.TabIndex)
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                else
                    this.loginFormLoginButton_Click(loginFormLoginButton, new EventArgs());
                return true;
            }
            if ((keyData == (Keys.Control | Keys.C)) || (keyData == (Keys.Control | Keys.V)) || (keyData == (Keys.Control | Keys.X)))
            {

                return true;
            }

            return base.ProcessDialogKey(keyData);
        }



        private void UserLogin_Load(object sender, EventArgs e)
        {
            string usrName;
            string usrPass;
            if (SecurityAccessor.Instance.ReadUsbAuthentication(this.desktopSession, out usrName, out usrPass))
            {
                this.enteredUserName = usrName;
                this.enteredPassWord = usrPass;
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.userTextBox.Text = this.desktopSession.ActiveUserData.CurrentUserFullName;
            this.userTextBox.Focus();
        }

        private void loginFormLoginButton_EnabledChanged(object sender, EventArgs e)
        {
            if (loginFormLoginButton.Enabled)
            {
                if (this.desktopSession.ScannedCredentials)
                {
                    this.desktopSession.ScannedCredentials = false;
                    this.loginFormLoginButton_Click(this, new EventArgs());
                }
            }
        }
    }
}
