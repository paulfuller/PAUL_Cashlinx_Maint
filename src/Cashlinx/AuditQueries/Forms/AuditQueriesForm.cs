using System;
using System.Windows.Forms;
using AuditQueries.Logic;
using Common.Libraries.Utility;

namespace AuditQueries.Forms
{
    public partial class AuditQueriesForm : Form
    {
        private const int MINUSERNAME_LENGTH = 6;
        
        public String UserName
        {
            get;
            private set;
        }

        public String Password
        {
            get;
            private set;
        }


        public AuditQueriesForm()
        {
            InitializeComponent();
        }

        private void AuditQueriesForm_Load(object sender, EventArgs e)
        {
            //Populate the queries list from resources
            if (!AuditQueriesSession.Instance.Setup())
            {
                MessageBox.Show("Audit queries session could not be started");
                Application.Exit();
            }

            //Disable fields that require specific order
            this.userNameTextBox.Enabled = true;
            this.passwordTextBox.Enabled = false;
            this.loginButton.Enabled = false;
            this.selectQueryButton.Enabled = false;
            this.queryListView.Visible = false;
            this.queryListView.Enabled = false;
            this.queriesGroupBox.Enabled = false;

            //Add queries to list box
            QueryStorage qStore = AuditQueriesSession.Instance.GetQueryStorage();
            //queryListView.Items.Clear();
            queryListView.BeginUpdate();
            int idx = 0;
            var qStoreIds = qStore.GetQueryIds();
            foreach(var curId in qStoreIds)
            {
                ListViewItem newListRow;
                if (this.queryListView.Items.Count < (idx + 1))
                {
                    newListRow = this.queryListView.Items.Add(string.Empty);
                    newListRow.SubItems.Add(string.Empty);
                    newListRow.SubItems.Add(string.Empty);
                }
                else
                {
                    newListRow = this.queryListView.Items[0];
                }

                //Set row id
                newListRow.Text = string.Format("{0}", curId);
                newListRow.SubItems[1].Text = qStore.GetQueryName(curId);
                newListRow.SubItems[2].Text = qStore.GetQueryDesc(curId);

                //Increment index
                ++idx;
            }
            queryListView.EndUpdate();
        }

        private void userNameTextBox_TextChanged(object sender, EventArgs e)
        {
            //Validate characters in the user name??
            if (!string.IsNullOrEmpty(userNameTextBox.Text) && userNameTextBox.Text.Length >= MINUSERNAME_LENGTH)
            {
                passwordTextBox.Enabled = true;
            }
            else
            {
                passwordTextBox.Enabled = false;
            }
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            //Validate characters in the password??
            if (!string.IsNullOrEmpty(passwordTextBox.Text))
            {
                loginButton.Enabled = true;
            }
            else
            {
                loginButton.Enabled = false;
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            //Disable button upon click
            this.loginButton.Enabled = false;

            //Get user name and password
            this.UserName = this.userNameTextBox.Text;
            this.Password = this.passwordTextBox.Text;

            //Authenticate the user
            if (!AuditQueriesSession.Instance.AuthenticateUser(this.UserName, this.Password))
            {
                MessageBox.Show("Please enter a different user name and/or password and try again.", "Audit Query App Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.loginButton.Enabled = true;
            }
            else
            {
                this.queryListView.Visible = true;
                this.queryListView.Enabled = true;
                this.queriesGroupBox.Enabled = true;
                this.selectQueryButton.Enabled = false;
            }
        }

        private void selectQueryButton_Click(object sender, EventArgs e)
        {
            if (AuditQueriesSession.Instance.SelectedQueryId != -1)
            {                
                var auditResultForm = new AuditParamResultForm();
                auditResultForm.ShowDialog(this);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Do you want to exit the application?", "Audit Query App Message", MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                AuditQueriesSession.Instance.Shutdown();
            }
        }

        private void queryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (queryListView.SelectedIndices.Count > 0 && queryListView.SelectedIndices[0] != -1)
            {
                var selectedIdx = queryListView.SelectedIndices[0];
                var selectedQueryId = Utilities.GetIntegerValue(queryListView.Items[selectedIdx].Text, -1);
                //Convert from a zero-based index to a one-based index when setting
                if (selectedQueryId == -1 || !AuditQueriesSession.Instance.SetSelectedQuery(selectedQueryId))
                {
                    MessageBox.Show("The query you are trying to select is not valid and may not be used at this time!");
                    queryListView.SelectedIndices.Clear();
                    queryListView.Update();
                    AuditQueriesSession.Instance.ClearSelectedQuery();
                    this.selectQueryButton.Enabled = false;
                    this.selectQueryButton.Update();
                }
                else
                {
                    this.selectQueryButton.Enabled = true;
                    this.selectQueryButton.Update();
                }
            }
            else
            {
                AuditQueriesSession.Instance.ClearSelectedQuery();
                this.selectQueryButton.Enabled = false;
                this.selectQueryButton.Update();
            }
        }
    }
}
