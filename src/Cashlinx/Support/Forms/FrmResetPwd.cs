using System;
using System.Globalization;
using System.Windows.Forms;
using Common.Controllers.Network;
using Common.Libraries.Forms.Components;
using Support.Logic;

namespace Support.Forms
{
    public partial class FrmResetPwd : CustomBaseForm
    {

        public FrmResetPwd()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("Are you sure you want to cancel resetting the password?",
                                                 "Application Security", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                 MessageBoxDefaultButton.Button2);
            if (reply == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                TxtUserId.Select();
            }

        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            string CurrentPWD,UserName;

            string UserID = TxtUserId.Text;

            if ((UserID == string.Empty) || ( UserID == " "))
            {
                MessageBox.Show("You must enter a valid User ID to continue", "Application Security");
                TxtUserId.Clear();
                TxtUserId.Select();
                return;
            }

            bool ReturnValue = PawnLDAPAccessor.Instance.GetUserPassword(UserID, out CurrentPWD, out UserName);


            if (ReturnValue == false)
            {
                MessageBox.Show(UserID + " is not a valid User ID", "Application Security");
                TxtUserId.Clear();
                TxtUserId.Select();
                return;
            }

            if (UserName == string.Empty)
            {
                MessageBox.Show(UserID + "You must enter a valid User ID to continue", "Application Security");
                TxtUserId.Clear();
                TxtUserId.Select();
                return;
            }

            TextInfo info = CultureInfo.CurrentCulture.TextInfo;

            UserName = info.ToTitleCase(UserName.ToLower());


            DialogResult reply = MessageBox.Show(string.Format("Are you sure you want to reset the password for {0}?", UserName),
                                              "Application Security", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                              MessageBoxDefaultButton.Button2);
            if (reply == DialogResult.Yes)
            {
                string UserDN = string.Format("uid={0},ou=People,dc=cashamerica", UserID);
                ReturnValue = PawnLDAPAccessor.Instance.ChangePassword(UserDN, "xyz12345");
                if (ReturnValue)
                {
                    MessageBox.Show(string.Format("Password successfully changed for {0} to 'xyz12345'.", UserName), "Application Security");
                    CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserCurrentPassword = "xyz12345";
                    this.Close();

                }
                else
                {
                    MessageBox.Show(string.Format("{0}Error Changing Password", UserID), "System Error");
                    return;
                }

            }
            else
            {
                return;
            }
        }
    }
}
