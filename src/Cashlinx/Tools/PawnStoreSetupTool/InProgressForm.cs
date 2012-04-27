using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PawnStoreSetupTool
{
    public partial class InProgressForm : Form
    {
        public const string PROCMSG = "* Processing *";
        private bool isLoaded;
        private string msg;
        public string Message
        {
            set
            {
                this.msg = value ?? PROCMSG;
                this.updateMessage();
            }
        }

        private void updateMessage()
        {
            if (!this.isLoaded) return;
            this.processingMessageLabel.Text = !string.IsNullOrEmpty(this.msg)
                                        ? this.msg : PROCMSG;
            this.BringToFront();
            this.Visible = true;
            this.Enabled = true;
            this.TopMost = true;
            this.Update();
        }

        public InProgressForm(string str)
        {
            InitializeComponent();
            this.msg = str ?? PROCMSG;
            this.isLoaded = false;
            Show();
        }

        private void InProgressForm_Load(object sender, EventArgs e)
        {
            this.isLoaded = true;
            this.updateMessage();
        }

        public void HideMessage()
        {
            this.SendToBack();
            this.Visible = false;
            this.TopMost = false;
            this.Update();
        }

    }
}
