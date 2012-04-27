using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.Services.MerchandiseTransfer
{
    public partial class SelectTransferInMethod : CustomBaseForm
    {
        public SelectTransferInMethod()
        {
            InitializeComponent();
            NavControlBox = new NavBox
            {
                Owner = this
            };
        }

        # region Properties

        public NavBox NavControlBox { get; set; }

        private DesktopSession CDS
        {
            get { return GlobalDataAccessor.Instance.DesktopSession; }
        }

        # endregion

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (rdoManual.Checked)
            {
                CDS.TransferMethod = TransferMethod.Manual;
            }
            else
            {
                CDS.TransferMethod = TransferMethod.QuickReceive;
            }

            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || (this.ActiveControl.Equals(btnCancel) && keyData == Keys.Enter))
            {
                this.btnCancel_Click(null, new EventArgs());
                return true;
            }

            if (keyData == Keys.Enter)
            {
                this.btnContinue_Click(null, new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
