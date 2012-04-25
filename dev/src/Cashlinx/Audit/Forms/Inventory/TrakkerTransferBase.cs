using System;
using System.Drawing;
using System.Windows.Forms;
using Audit.Logic;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Audit.Forms.Inventory
{
    public partial class TrakkerTransferBase : AuditWindowBase
    {
        public string FtpHost { get; set; }
        public string FtpPassword { get; set; }
        public string FtpUser { get; set; }
        public TransferMode Mode { get; set; }

        public TrakkerTransferBase()
        {
            InitializeComponent();

            FtpHost = ADS.FtpHost;
            FtpUser = ADS.FtpUser;
            FtpPassword = ADS.FtpPassword;

            SetUILabels();
        }

        protected void ChangeHeadingMessage(string message)
        {
            lblHeader.Text = message;
        }

        protected void ChangeStatusMessage(string message, Color color)
        {
            lblStatusMessage.Text = message;
            lblStatusMessage.ForeColor = color;
        }

        protected void ChangeStatusMessage1(string message)
        {
            lblStatusMessage1.Text = message;
        }

        protected void ChangeStatusMessage2(string message)
        {
            lblStatusMessage2.Text = message;
        }

        protected void ChangeStatusValue1(string value)
        {
            lblStatusValue1.Text = value;
        }

        protected void ChangeStatusValue2(string value)
        {
            lblStatusValue2.Text = value;
        }

        protected virtual void SetUILabels()
        {
        }

        private void lnkInstructions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrakkerInstructions instructions = new TrakkerInstructions(Mode);
            instructions.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }
    }
}
