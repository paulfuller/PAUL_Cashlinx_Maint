using System;
using Audit.Logic;

namespace Audit.Forms.Inventory
{
    public partial class TrakkerInstructions : AuditWindowBase
    {
        public TransferMode Mode { get; private set; }

        public TrakkerInstructions(TransferMode mode)
        {
            InitializeComponent();
            Mode = mode;
            SetUILabels();
        }

        private void SetUILabels()
        {
            if (Mode == TransferMode.DownloadToTrakker)
            {
                lblMessage1.Text = "Download to Trakker from Audit on the PC.";
                lblMessage2.Text = "Download to Trakker from Inventory on the Trakker.";
                lblMessage3.Text = string.Empty;
                lblNumber3.Text = string.Empty;
            }
            else
            {
                lblMessage1.Text = "Upload from Trakker from Inventory on the Trakker.";
                lblMessage2.Text = "Upload from Trakker from Audit on the PC.";
                lblMessage3.Text = string.Empty;
                lblNumber3.Text = string.Empty;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
