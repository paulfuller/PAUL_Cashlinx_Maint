/* File modified 12/30/2010 for Bugzilla number 0053.  Comments in line below where changes made
 * Notation is RB 0053.   Randall Brickler
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Network;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;

namespace PawnStoreSetupTool
{
    public partial class UserMgmtForm : Form
    {
        private const string RESETPWD = "xyz12345";
        public DataTable ExistingUsers;
        public List<PairType<UserVO, LDAPUserVO>> CreatedUsers
        {
            private set; get;
        }
        public UserVO NewUser
        {
            private set; get;
        }
        public LDAPUserVO NewLDAPUser
        {
            private set; get;
        }

        public string NewUserName
        {
            set; get;
        }

        public bool AddedUsers
        {
            private set; get;
        }

        public string SelectedUser
        {
            private set; get;
        }

        public UserMgmtForm()
        {
            InitializeComponent();
            CreatedUsers = new List<PairType<UserVO, LDAPUserVO>>();
            NewUser = new UserVO();
            NewLDAPUser = new LDAPUserVO();
            ExistingUsers = null;
            AddedUsers = false;
            SelectedUser = string.Empty;
        }

        private void newUserButton_Click(object sender, EventArgs e)
        {
            if (this.showAddLDAPForm())
            {
                AddedUsers = true;
                if (ExistingUsers == null)
                {
                    ExistingUsers = new DataTable("Existing Employees");
                    ExistingUsers.Columns.Add("User Name");
                    ExistingUsers.Columns.Add("Employee #");
                    ExistingUsers.Columns.Add("Home Store #");
                    ExistingUsers.Columns.Add("Active Flag");
                }

                DataRow dR = ExistingUsers.NewRow();
                dR["User Name"] = NewLDAPUser.UserName;
                dR["Employee #"] = NewLDAPUser.EmployeeNumber;
                dR["Home Store #"] = NewUser.FacNumber;
                dR["Active Flag"] = "1";
                ExistingUsers.Rows.Add(dR);

                this.summaryDataGridView.DataSource = ExistingUsers;
                this.summaryDataGridView.Update();

                CreatedUsers.Add(new PairType<UserVO, LDAPUserVO>(NewUser, NewLDAPUser));
                NewUser = new UserVO();
                NewLDAPUser = new LDAPUserVO();
            }
        }

        private void UserMgmtForm_Load(object sender, EventArgs e)
        {
            bool showLDAPForm = false;
            if (!string.IsNullOrEmpty(NewUserName))
            {
                NewUser.UserName = NewUserName;
                NewLDAPUser.UserName = NewUserName;
                showLDAPForm = true;
            }

            if (ExistingUsers != null)
            {
                this.summaryDataGridView.DataSource = ExistingUsers;
                this.summaryDataGridView.Update();
            }

            if (showLDAPForm)
            {
                this.showAddLDAPForm();

            }
        }

        private bool showAddLDAPForm()
        {
            var res = this.addNewUserToLDAP();
            if (!res)
            {
                MessageBox.Show("Could not add a new user.  Please try again.",
                                PawnStoreSetupForm.SETUPALERT_TXT);
            }
            return (res);
        }

        private bool addNewUserToLDAP()
        {
            if (PawnLDAPAccessor.Instance.State ==
                PawnLDAPAccessor.LDAPState.CONNECTED)
            {
                bool validDataSet = false;

                while (!validDataSet)
                {
                    var dataReqForm = new DataRequestForm();
                    dataReqForm.DataObject = NewLDAPUser;
                    dataReqForm.Text = "Add LDAP User";
                    var res = dataReqForm.ShowDialog(this);
                    if (res == DialogResult.OK)
                    {
                        var completedData = (LDAPUserVO)dataReqForm.DataObject;
                        var sB = new StringBuilder(256);
                        var errorOccurred = false;
                        if (string.IsNullOrEmpty(completedData.UserName))
                        {
                            sB.AppendLine("You must enter a valid user name");
                            errorOccurred = true;
                        }
                        var errMsg = string.Empty;
                        if (string.IsNullOrEmpty(completedData.Password) ||
                            PawnLDAPAccessor.Instance.PasswordPolicy == null ||
                            !PawnLDAPAccessor.Instance.PasswordPolicy.IsValid(completedData.Password, null, out errMsg))
                        {
                            sB.AppendLine("You must enter a valid password");
                            if (!string.IsNullOrEmpty(errMsg))
                            {
                                sB.AppendLine("Password invalid because: " + errMsg);
                            }
                            errorOccurred = true;
                        }

                        if (string.IsNullOrEmpty(completedData.EmployeeHomeStore) ||
                            completedData.EmployeeHomeStore.Length < PawnStoreSetupForm.MIN_STORENUM_LENGTH ||
                            completedData.EmployeeHomeStore.Length > PawnStoreSetupForm.MAX_STORENUM_LENGTH)
                        {
                            sB.AppendLine("You must enter a valid store number");                            
                            errorOccurred = true;
                        }
                        else if (!string.IsNullOrEmpty(completedData.EmployeeHomeStore) &&
                            completedData.EmployeeHomeStore.Length >= PawnStoreSetupForm.MIN_STORENUM_LENGTH &&
                            completedData.EmployeeHomeStore.Length <= PawnStoreSetupForm.MAX_STORENUM_LENGTH)
                        {
                            string stoNumStr = completedData.EmployeeHomeStore;
                            if (stoNumStr.StartsWith("0"))
                            {
                                stoNumStr = stoNumStr.Substring(1);
                            }
                            int stoNum;
                            if (!Int32.TryParse(stoNumStr, out stoNum))
                            {
                                sB.AppendLine(
                                        "You must enter a valid store number. A store number must be comprised of numbers only.");
                                errorOccurred = true;
                            }
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
                        NewLDAPUser = completedData;
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
                    bool addedUserToLdap = true;
                    if (!PawnLDAPAccessor.Instance.CreateUser(
                        NewLDAPUser.UserName,
                        NewLDAPUser.Password,
                        NewLDAPUser.DisplayName,
                        NewLDAPUser.EmployeeNumber,
                        NewLDAPUser.EmployeeType,
                        out errMsg))
                    {
                        progBox.HideMessage();
                        MessageBox.Show("The LDAP Add User operation has failed: " + errMsg + 
                            ".  Will attempt to validate against existing credentials to " +
                            "verify if the user is already in the LDAP system");
                        addedUserToLdap = false;
                    }

                    int numTries = 0;
                    DateTime pwdLastModified;
                    string[] pwdHistory;
                    string dispName;
                    bool lockedOut;
                    if (!addedUserToLdap && 
                        PawnLDAPAccessor.Instance.AuthorizeUser(
                        NewLDAPUser.UserName,
                        NewLDAPUser.Password, 
                        ref numTries, 
                        out pwdLastModified, 
                        out pwdHistory, out dispName, out lockedOut))
                    {
                        addedUserToLdap = true;
                    }

                    if (addedUserToLdap)
                    {   
                        if (NewLDAPUser.EmployeeHomeStore.Length == PawnStoreSetupForm.MIN_STORENUM_LENGTH)
                        {
                            NewLDAPUser.EmployeeHomeStore = NewLDAPUser.EmployeeHomeStore.PadLeft(5, '0');
                        }
                        NewUser.UserName = NewLDAPUser.UserName;
                        NewUser.UserCurrentPassword = NewLDAPUser.Password;
                        NewUser.EmployeeNumber = NewLDAPUser.EmployeeNumber;
                        NewUser.FacNumber = NewLDAPUser.EmployeeHomeStore;
                        NewUser.StoreNumber = NewUser.FacNumber;
                        NewUser.UserID = "0";
                        NewUser.UserRole = new RoleVO();
                        NewUser.UserRole.RoleId = PawnStoreSetupForm.DEFAULT_ROLE_NAME;
                        progBox.HideMessage();
                        MessageBox.Show("Successfully added " + NewLDAPUser.UserName + " to the LDAP server.");
                        return (true);
                    }
                }

            }
            else
            {
                MessageBox.Show("Please set up the connection to LDAP prior to adding users.",
                                PawnStoreSetupForm.SETUPALERT_TXT);
            }
            return (false);
        }

        private void formDoneButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void resetUserPwdButton_Click(object sender, EventArgs e)
        {
            if (PawnLDAPAccessor.Instance == null ||
                PawnLDAPAccessor.Instance.State == PawnLDAPAccessor.LDAPState.DISCONNECTED)
            {
                MessageBox.Show(
                        "Please set up the connection to LDAP prior to resetting a user's password.",
                        PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }

            //Figure out what user is selected
            if (string.IsNullOrEmpty(this.SelectedUser))
            {
                MessageBox.Show("Please select a user prior to attempting to reset a password",
                                PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }
            
            //Send reset command to LDAP server
            bool success = false;
            var dRes = DialogResult.No;
            while (!success)
            {
                //RB 0053:  Need to switch username to fully qualified distinguished name for this method.
                var dn = "uid=" + this.SelectedUser + ",ou=People,dc=cashamerica";
                success = PawnLDAPAccessor.Instance.ChangePassword(dn, RESETPWD);
                if (!success)
                {
                    dRes = MessageBox.Show(
                            "The password reset operation for user: "
                            + this.SelectedUser
                            + System.Environment.NewLine
                            + "failed. Would you like to retry?",
                            PawnStoreSetupForm.SETUPALERT_TXT,
                            MessageBoxButtons.YesNoCancel);
                    if (dRes == DialogResult.No || dRes == DialogResult.Cancel)
                    {
                        break;
                    }
                }
            }

            if (success)
            {
                MessageBox.Show("Successfully reset password for user: " + this.SelectedUser,
                                PawnStoreSetupForm.SETUPALERT_TXT);
            }
            else if (!success && (dRes == DialogResult.Cancel))
            {
                MessageBox.Show("Unable to reset password for user: " + this.SelectedUser,
                                PawnStoreSetupForm.SETUPALERT_TXT);
            }
        }

        private string SelectUserFromSelectedIndex(int idx)
        {
            if (ExistingUsers == null || 
                ExistingUsers.Rows == null ||
                ExistingUsers.Rows.Count <= 0)
            {
                return (string.Empty);
            }
            //RB 0053: Last evaluation in if statment below would always evaluate to True, corrected.
            if (idx == -1 ||
                ExistingUsers.Rows.Count <= 0 ||
                ExistingUsers.Rows.Count < idx)
            {
                return (string.Empty);
            }

            DataRow selUserRow = ExistingUsers.Rows[idx];
            string usrName = string.Empty;
            if (selUserRow != null)
            {
                var usrNameCell = selUserRow.GetDataObject("User Name");
                if (usrNameCell != null)
                {
                    usrName = Utilities.GetStringValue(usrNameCell, string.Empty);
                }
            }

            return (usrName);
        }

        private void summaryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            var rowIdx = e.RowIndex;
            if (rowIdx == -1)
            {
                return;
            }

            this.SelectedUser = this.SelectUserFromSelectedIndex(rowIdx);
            //MessageBox.Show("User Selected = " + this.SelectedUser ?? "null");
        }

        private void summaryDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (e == null)
            {
                return;
            }
            if (this.summaryDataGridView.SelectedRows != null &&
                this.summaryDataGridView.SelectedRows.Count > 0)
            {
                this.SelectedUser =
                        this.SelectUserFromSelectedIndex(
                        this.summaryDataGridView.SelectedRows[0].Cells[0].RowIndex);
                //MessageBox.Show("User Selected DGV = " + this.SelectedUser ?? "null");
            }            
        }
    }
}
