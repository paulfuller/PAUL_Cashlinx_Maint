using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Network;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Authorization;

namespace Common.Libraries.Forms
{
    public partial class UserChangePassword : CustomBaseForm
    {
        private readonly string OriginalCurrentPassword;
        private readonly PasswordPolicyVO pwdPolicy;
        public string EnteredCurrentPassword { private set; get; }
        public string EnteredNewPassword { private set; get; }
        public string EnteredConfirmNewPassword { private set; get; }


        public UserChangePassword(PasswordPolicyVO pwd, string origPwd)
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(origPwd))
            {
                throw new ApplicationException("Cannot change password without the original");
            }
            if (pwd == null)
            {
                throw new ApplicationException("Cannot change password without a password policy");
            }
            this.OriginalCurrentPassword = origPwd;
            this.EnteredConfirmNewPassword = string.Empty;
            this.EnteredNewPassword = string.Empty;
            this.EnteredConfirmNewPassword = string.Empty;
            this.pwdPolicy = pwd;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            var dR = MessageBox.Show(
                "Are you sure you want to cancel changing your password?",
                "Application Security",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop,
                MessageBoxDefaultButton.Button2);
            if (dR == DialogResult.Yes)
            {
                
                this.Close();
 
            }

 
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (this.currentPasswordTextBox.Text.Length == 0)
            {

                MessageBox.Show("You must enter your current password.");
                return;
            }

            if (this.EnteredNewPassword.Length == 0)
            {
                MessageBox.Show("You must enter a new password.");
                return;
            }

            //Validate that the new password matches the confirmed password
            if (!(this.EnteredConfirmNewPassword.Equals(this.EnteredNewPassword)))
            {

                MessageBox.Show(
                    "The password in the confirmation box does not match your new password.",
                    "Application Security");
                return;
            }
            
            var dR = MessageBox.Show(
                "Are you sure you want to submit this password change?",
                "Application Security",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop,
                MessageBoxDefaultButton.Button2);
            if (dR == DialogResult.Yes)
            {


                if (GlobalDataAccessor.Instance.DesktopSession.IsSkipLDAP)
                {
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                //Call change password in LDAP
                if (PawnLDAPAccessor.Instance.ChangePassword(GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserDN, this.newPasswordTextBox.Text))
                {
                    MessageBox.Show("Password changed successfully.");
                    this.DialogResult = DialogResult.OK;
                    GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserCurrentPassword = this.newPasswordTextBox.Text;
                }
                else
                    MessageBox.Show("Password could not be changed.");
                
                this.Close();


            }
            
            
        }

        private void currentPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            this.EnteredCurrentPassword = this.currentPasswordTextBox.Text;
            if (!string.IsNullOrEmpty(this.EnteredCurrentPassword) &&
                this.OriginalCurrentPassword.Equals(this.EnteredCurrentPassword))
            {
                this.newPasswordTextBox.Enabled = true;                
            }
            else if (!string.IsNullOrEmpty(this.EnteredCurrentPassword) &&
                this.EnteredCurrentPassword.Length == this.OriginalCurrentPassword.Length)
            {
                MessageBox.Show(
                    "Please enter the correct current password.",
                    "Application Security");
                this.currentPasswordTextBox.Clear();
            }
        }

        private void newPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            this.EnteredNewPassword = this.newPasswordTextBox.Text;
            if (this.EnteredNewPassword.Length < pwdPolicy.MinLength) return;

  

        }

        private void confirmNewPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            this.EnteredConfirmNewPassword = this.confirmNewPasswordTextBox.Text;
            if (!string.IsNullOrEmpty(this.EnteredConfirmNewPassword))
            {
                if (this.EnteredConfirmNewPassword.Length < this.EnteredNewPassword.Length)
                {
                    return;
                }
 
                    if (this.EnteredConfirmNewPassword.Equals(this.EnteredNewPassword))
                    {
                        this.submitButton.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(
                            "The password in the confirmation box does not match your new password.",
                            "Application Security");
                    }
                
            }                
        }

        private void UserChangePassword_Load(object sender, EventArgs e)
        {
            this.currentPasswordTextBox.Focus();
        }

        private void newPasswordTextBox_Leave(object sender, EventArgs e)
        {
            string errMsg;

            if (string.IsNullOrEmpty(this.EnteredNewPassword) && this.ActiveControl == this.cancelButton)
            {
                this.cancelButton_Click(sender, e);
                return;
            }
                

            if (pwdPolicy.IsValid(this.EnteredNewPassword, new List<string>(), out errMsg))
            {
                this.confirmNewPasswordTextBox.Enabled = true;
                //confirmNewPasswordTextBox.Focus();
                this.ActiveControl = confirmNewPasswordTextBox;
            }
            else
            {
                MessageBox.Show(errMsg, "Application Security");
                this.newPasswordTextBox.Clear();
                this.newPasswordTextBox.Focus();
            }
        }
    }
}
