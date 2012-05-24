using System;
using System.Windows.Forms;
using Common.Controllers.Network;
using Common.Libraries.Objects.Authorization;

namespace PawnStoreSetupTool
{
    public partial class LDAPSetupForm : Form
    {
        public string LDAPServer { set; get; }
        public string LDAPPort { set; get; }
        public string LDAPLogin { set; get; }
        public string LDAPPassword { set; get; }
        public string LDAPPassPolicyDN { set; get; }
        public string LDAPUserDN { set; get;  }
        public string LDAPUserIdKey { set; get; }
        public string LDAPSearchUser { private set; get; }
        public string LDAPSearchPass { private set; get; }
        public bool LDAPCxnSuccess { private set; get; }
        public bool LDAPSearchSuccess { private set; get; }
        public PasswordPolicyVO LDAPPwdPolicy
        {
            private set; get;
        }
        /*public List<LDAPAddUserVO> AddedUsers
        {
            private set; get;
        }*/

        private uint infoFlag;
        private enum REQINFOFLAGS
        {
            SERVER = 0x0001,
            PORT = 0x0002,
            LOGIN = 0x0004,
            PASSWORD = 0x0008,
            PASSPOLICY = 0x0010,
            USERDN = 0x0020,
            USERIDKEY = 0x0040,
        };

        public LDAPSetupForm()
        {
            InitializeComponent();
            LDAPCxnSuccess = false;
            LDAPSearchSuccess = false;
            //this.AddedUsers = new List<LDAPAddUserVO>();
        }

        private void LDAPSetupForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LDAPServer))
            {
                this.ldapServerTextBox.Text = LDAPServer;
            }
            if (!string.IsNullOrEmpty(LDAPPort))
            {
                this.ldapPortTextBox.Text = LDAPPort;
            }
            if (!string.IsNullOrEmpty(LDAPLogin))
            {
                this.ldapLoginTextBox.Text = LDAPLogin;
            }
            if (!string.IsNullOrEmpty(LDAPPassword))
            {
                this.ldapPasswordTextBox.Text = LDAPPassword;
            }
            if (!string.IsNullOrEmpty(LDAPPassPolicyDN))
            {
                this.pwdPolicyDNTextBox.Text = LDAPPassPolicyDN;
            }
            if (!string.IsNullOrEmpty(LDAPUserDN))
            {
                this.userDNTextBox.Text = LDAPUserDN;
            }
            if (!string.IsNullOrEmpty(LDAPUserIdKey))
            {
                this.userIdKeyTextBox.Text = LDAPUserIdKey;
            }
        }

        private void testLDAPCxnButton_Click(object sender, EventArgs e)
        {
            if (!(((infoFlag & (uint)REQINFOFLAGS.LOGIN) > 0) &&
                  ((infoFlag & (uint)REQINFOFLAGS.PASSPOLICY) > 0) &&
                  ((infoFlag & (uint)REQINFOFLAGS.PASSWORD) > 0) &&
                  ((infoFlag & (uint)REQINFOFLAGS.PORT) > 0) &&
                  ((infoFlag & (uint)REQINFOFLAGS.SERVER) > 0) &&
                  ((infoFlag & (uint)REQINFOFLAGS.USERDN) > 0) &&
                   (infoFlag & (uint)REQINFOFLAGS.USERIDKEY) > 0))
            {
                MessageBox.Show("Please enter all required data for LDAP connection.",
                                "PawnStoreSetup Alert",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            //Ensure LDAP login field is properly
            //formatted
            string login = LDAPLogin;
            if (LDAPLogin.IndexOf("cn=", StringComparison.OrdinalIgnoreCase) == -1)
            {
                login = "cn=" + LDAPLogin;
            }

            //Call LDAP connection class
            if (PawnLDAPAccessor.Instance.State == PawnLDAPAccessor.LDAPState.DISCONNECTED)
            {
                PawnLDAPAccessor.Instance.InitializeConnection(
                        this.LDAPServer,
                        this.LDAPPort,
                        login,
                        LDAPPassword,
                        LDAPPassPolicyDN,
                        LDAPUserDN,
                        LDAPUserIdKey);
            }

            LDAPCxnSuccess = false;
            if (PawnLDAPAccessor.Instance.State == PawnLDAPAccessor.LDAPState.CONNECTED)
            {
                this.LDAPPwdPolicy = PawnLDAPAccessor.Instance.PasswordPolicy;
                LDAPCxnSuccess = true;
            }
            
            if (!LDAPCxnSuccess)
            {
                MessageBox.Show(
                        "LDAP Connection Failed. Please change the field values and try again.");
                return;
            }

            //Show message box that LDAP is now connected
            MessageBox.Show("LDAP Connection Successful", "PawnStoreSetup Alert");

            //Enable test user search and done button
            this.testSearchButton.Enabled = false;
            this.testSearchUserTextBox.Enabled = true;
            this.testPasswordSearchTextBox.Enabled = true;
            this.doneButton.Enabled = true;

            //Disable test connection button
            this.testLDAPCxnButton.Enabled = false;
        }

        private void testSearchButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.testSearchUserTextBox.Text) &&
                !string.IsNullOrEmpty(this.testPasswordSearchTextBox.Text))
            {
                if (PawnLDAPAccessor.Instance.State == 
                    PawnLDAPAccessor.LDAPState.CONNECTED)
                {
                    string usrName = this.testSearchUserTextBox.Text;
                    string usrPass = this.testPasswordSearchTextBox.Text;
                    int attemptCnt = 0;
                    DateTime lastmodified;
                    string[] passHist;
                    string displayName;
                    bool lockedOut;
                    LDAPSearchSuccess = PawnLDAPAccessor.Instance.AuthorizeUser(
                            usrName,
                            usrPass,
                            ref attemptCnt,
                            out lastmodified,
                            out passHist,
                            out displayName,
                            out lockedOut);
                    if (LDAPSearchSuccess)
                    {
                        MessageBox.Show("Successfully found user in LDAP server",
                                        "PawnStoreSetup Alert");
                    }
                    else
                    {
                        MessageBox.Show("Failed to find user in LDAP server. Please try again.",
                                        "PawnStoreSetup Alert");
                    }
                }
            }
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            //TODO: Set LDAP data
            this.Close();
        }

        private void ldapServerTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.ldapServerTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.ldapServerLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.SERVER;
                return;
            }
            LDAPServer = dat;
            infoFlag |= (uint)REQINFOFLAGS.SERVER;
            PawnStoreSetupForm.CheckmarkLabel(true, this.ldapServerLabel);
            this.ldapPortTextBox.Enabled = true;
        }

        private void ldapPortTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.ldapPortTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.ldapPortLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.PORT;
                return;
            }
            LDAPPort = dat;
            infoFlag |= (uint)REQINFOFLAGS.PORT;
            PawnStoreSetupForm.CheckmarkLabel(true, this.ldapPortLabel);
            this.ldapLoginTextBox.Enabled = true;
        }

        private void ldapLoginTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.ldapLoginTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.ldapLoginLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.LOGIN;
                return;
            }
            LDAPLogin = dat;
            infoFlag |= (uint)REQINFOFLAGS.LOGIN;
            PawnStoreSetupForm.CheckmarkLabel(true, this.ldapLoginLabel);
            this.ldapPasswordTextBox.Enabled = true;
        }

        private void ldapPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.ldapPasswordTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.ldapPasswordLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.PASSWORD;
                return;
            }
            LDAPPassword = dat;
            infoFlag |= (uint)REQINFOFLAGS.PASSWORD;
            PawnStoreSetupForm.CheckmarkLabel(true, this.ldapPasswordLabel);
            this.pwdPolicyDNTextBox.Enabled = true;
        }

        private void pwdPolicyDNTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.pwdPolicyDNTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.pwdPolicyDNLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.PASSPOLICY;
                return;
            }
            LDAPPassPolicyDN = dat;
            infoFlag |= (uint)REQINFOFLAGS.PASSPOLICY;
            PawnStoreSetupForm.CheckmarkLabel(true, this.pwdPolicyDNLabel);
            this.userDNTextBox.Enabled = true;
        }

        private void userDNTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.userDNTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.userDNLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.USERDN;
                return;
            }
            LDAPUserDN = dat;
            infoFlag |= (uint)REQINFOFLAGS.USERDN;
            PawnStoreSetupForm.CheckmarkLabel(true, this.userDNLabel);
            this.userIdKeyTextBox.Enabled = true;
        }

        private void userIdKeyTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.userIdKeyTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.userIdKeyLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.USERIDKEY;
                return;
            }
            LDAPUserIdKey = dat;
            infoFlag |= (uint)REQINFOFLAGS.USERIDKEY;
            PawnStoreSetupForm.CheckmarkLabel(true, this.userIdKeyLabel);
            this.testLDAPCxnButton.Enabled = true;
        }

        private void testSearchUserTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.testSearchUserTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.testUserSearchLabel);
                return;
            }
            LDAPSearchUser = dat;
            PawnStoreSetupForm.CheckmarkLabel(true, this.testUserSearchLabel);
            if (PawnLDAPAccessor.Instance.State ==
                    PawnLDAPAccessor.LDAPState.CONNECTED && 
                !string.IsNullOrEmpty(this.testPasswordSearchTextBox.Text) &&
                !string.IsNullOrEmpty(this.testSearchUserTextBox.Text))
            {
                this.testSearchButton.Enabled = true;
                //this.addLDAPUserButton.Enabled = true;
            }
            else
            {
                this.testSearchButton.Enabled = false;
                //this.addLDAPUserButton.Enabled = false;
            }
        }

        private void testPasswordSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.testPasswordSearchTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.testPasswordSearchLabel);
                return;
            }
            LDAPSearchPass = dat;
            PawnStoreSetupForm.CheckmarkLabel(true, this.testPasswordSearchLabel);
            if (PawnLDAPAccessor.Instance.State ==
                    PawnLDAPAccessor.LDAPState.CONNECTED && 
                !string.IsNullOrEmpty(this.testPasswordSearchTextBox.Text) &&
                !string.IsNullOrEmpty(this.testSearchUserTextBox.Text))
            {
                this.testSearchButton.Enabled = true;
                //this.addLDAPUserButton.Enabled = true;
            }
            else
            {
                this.testSearchButton.Enabled = false;
                //this.addLDAPUserButton.Enabled = false;
            }
        }



        /*private void addLDAPUserButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.testSearchUserTextBox.Text) &&
                !string.IsNullOrEmpty(this.testPasswordSearchTextBox.Text))
            {
                if (PawnLDAPAccessor.Instance.State ==
                    PawnLDAPAccessor.LDAPState.CONNECTED)
                {
                    bool validDataSet = false;
                    var ldapUserAdd = new LDAPAddUserVO();

                    while (!validDataSet)
                    {
                        var dataReqForm = new DataRequestForm();
                        string usrName = this.testSearchUserTextBox.Text;
                        string usrPass = this.testPasswordSearchTextBox.Text;
                        ldapUserAdd.UserName = usrName;
                        ldapUserAdd.Password = usrPass;
                        dataReqForm.DataObject = ldapUserAdd;
                        dataReqForm.Text = "Add LDAP User";
                        var res = dataReqForm.ShowDialog(this);
                        if (res == DialogResult.OK)
                        {
                            var completedData = (LDAPAddUserVO)dataReqForm.DataObject;
                            var sB = new StringBuilder(256);
                            bool errorOccurred = false;
                            if (string.IsNullOrEmpty(completedData.UserName))
                            {
                                sB.AppendLine("You must enter a valid user name");
                                errorOccurred = true;
                            }
                            var errMsg = string.Empty;
                            if (string.IsNullOrEmpty(completedData.Password) ||
                                this.LDAPPwdPolicy == null || 
                                !this.LDAPPwdPolicy.IsValid(completedData.Password, null, out errMsg))
                            {
                                sB.AppendLine("You must enter a valid password");
                                if (!string.IsNullOrEmpty(errMsg))
                                {
                                    sB.AppendLine("Password invalid because: " + errMsg);
                                }
                                errorOccurred = true;
                            }

                            if (string.IsNullOrEmpty(completedData.DisplayName))
                            {
                                sB.AppendLine("You must enter a valid display name");
                                errorOccurred = true;
                            }

                            int empNum;
                            if (string.IsNullOrEmpty(completedData.EmployeeNumber) ||
                                completedData.EmployeeNumber.Length < 4 ||
                                !Int32.TryParse(completedData.EmployeeNumber, out empNum))
                            {
                                sB.AppendLine("You must enter a valid employee number");
                                errorOccurred = true;
                            }

                            if (string.IsNullOrEmpty(completedData.EmployeeType))
                            {
                                sB.AppendLine("You must enter a valid employee type");
                                errorOccurred = true;
                            }

                            if (errorOccurred)
                            {
                                MessageBox.Show(
                                        "The data provided for the LDAP user add operation is invalid.  Please check the following reasons and correct: " +
                                        sB,
                                        PawnStoreSetupForm.SETUPALERT_TXT);
                                continue;
                            }
                            
                            //We have a valid data set if we made it here
                            validDataSet = true;
                        }
                        else
                        {
                            var cancelRes = MessageBox.Show(
                                    "You have unsaved changes. Are you sure you want to cancel the user add operation?",
                                    PawnStoreSetupForm.SETUPALERT_TXT,
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning);
                            if (cancelRes == DialogResult.Yes)
                            {
                                validDataSet = false;
                                break;
                            }
                        }
                    }

                    if (validDataSet)
                    {
                        var progBox = new InProgressForm("* ADDING USER TO LDAP *");
                        string errMsg;
                        if (!PawnLDAPAccessor.Instance.CreateUser(
                            ldapUserAdd.UserName,
                            ldapUserAdd.Password,
                            ldapUserAdd.DisplayName,
                            ldapUserAdd.EmployeeNumber,
                            ldapUserAdd.EmployeeType,
                            out errMsg))
                        {
                            progBox.HideMessage();
                            MessageBox.Show("Could not add user to LDAP: " + errMsg);
                        }
                        else
                        {
                            progBox.HideMessage();
                            MessageBox.Show("Successfully added " + ldapUserAdd.UserName + " to the LDAP server.");
                            this.addLDAPUserButton.Enabled = false;
                            this.AddedUsers.Add(ldapUserAdd);
                        }
                    }
                }
            }
        }*/
    }
}
