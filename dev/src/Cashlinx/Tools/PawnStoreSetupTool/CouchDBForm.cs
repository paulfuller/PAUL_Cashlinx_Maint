using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PawnStoreSetupTool
{
    public partial class CouchDBForm : Form
    {
        public string CouchDBServer
        {
            set; get;
        }

        public string CouchDBPort
        {
            set; get;
        }

        public string CouchDBDatabase
        {
            set; get;
        }

        public CouchDBForm()
        {
            InitializeComponent();
        }

        private void serverTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(serverTextBox, null, out dat))
            {
                return;
            }
            this.CouchDBServer = dat;
            this.TryEnableDoneButton();
        }

        private void portTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(portTextBox, null, out dat))
            {
                return;
            }
            this.CouchDBPort = dat;
            this.TryEnableDoneButton();
        }

        private void databaseTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(databaseTextBox, null, out dat))
            {
                return;
            }
            this.CouchDBDatabase = dat;
            this.TryEnableDoneButton();
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CouchDBForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CouchDBServer))
            {
                serverTextBox.Text = this.CouchDBServer;
            }
            if (!string.IsNullOrEmpty(this.CouchDBPort))
            {
                portTextBox.Text = this.CouchDBPort;
            }
            if (!string.IsNullOrEmpty(this.CouchDBDatabase))
            {
                databaseTextBox.Text = this.CouchDBDatabase;
            }
            this.TryEnableDoneButton();
        }

        private void TryEnableDoneButton()
        {
            if (!string.IsNullOrEmpty(this.CouchDBServer) &&
                !string.IsNullOrEmpty(this.CouchDBPort) &&
                !string.IsNullOrEmpty(this.CouchDBServer))
            {
                this.doneButton.Enabled = true;
            }
            else
            {
                this.doneButton.Enabled = false;
            }
        }
    }
}
