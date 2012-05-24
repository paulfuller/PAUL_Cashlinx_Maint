using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Services.MerchandiseTransfer
{
    public partial class TransferRejectComment : CustomBaseForm
    {
        public TransferRejectComment()
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

        private void btnComplete_Click(object sender, EventArgs e)
        {
            var errorMessage = string.Empty;
            bool success = false;
            if (GlobalDataAccessor.Instance.DesktopSession.Transfers.Count > 0 && CDS.ActiveTransferIn.TransferSource == TransferSource.TOPSTICKET)
            {
                TransferVO transfer = GlobalDataAccessor.Instance.DesktopSession.Transfers[0];

                TransferWebService ws = new TransferWebService();
                success = ws.MDSETransferINRejectWS(transfer.OriginalStoreNumber, transfer.TransferTicketNumber.ToString(), txtComments.Text, out errorMessage);
                if (!success)
                {
                    MessageBox.Show("Reject WS Call Failed :" + errorMessage);
                }
                else
                {
                    MessageBox.Show("Reject WS Call was Successful");
                }
            }

            CDS.ActiveTransferIn.RejectReason = txtComments.Text;
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void txtComments_TextChanged(object sender, EventArgs e)
        {
            btnComplete.Enabled = txtComments.Text.Trim().Length > 0;
        }

        private void TransferRejectComment_Shown(object sender, EventArgs e)
        {
            txtComments_TextChanged(sender, e);
        }
    }
}
