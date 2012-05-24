/********************************************************************
* Namespace: CashlinxDesktop.DesktopForms.Pawn.ShopAdministration
* FileName: TOPSTransferDialog
* This form shows the data that will be entered for TOPS transfer
* in order to balance the cash drawer
* Sreelatha Rengarajan 2/5/2010 Initial version
*******************************************************************/

using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    public partial class TOPSTransferDialog : Form
    {
        private decimal BalanceAmount;

        private const string TOPSINMESSAGE = "OTHER GOODS AND SERVICES - IN / MISC. (RECEIPT ONLY)";
        private const string TOPSOUTMESSAGE = "OTHER GOODS AND SERVICES - out / MISC. EXPENSES";
        private const string CASHLINXTRANSFERINMESSAGE = "Comment: Transfer to Cashlinx Drawer #";
        private const string CASHLINXTRANSFEROUTMESSAGE = "Comment: Transfer from cashlinx Drawer #";
        private const string TRANSFERMESSAGE = "Please make the following corresponding entry in TOPS before balancing your TOPS drawer";


        public TOPSTransferDialog()
        {
            InitializeComponent();
        }




        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TOPSTransferDialog_Load(object sender, EventArgs e)
        {

            //Get cashdrawer amount
            var errorCode = string.Empty;
            var errorMesg = string.Empty;
            DialogResult dgr = DialogResult.Retry;

            DataTable cashdrawerAmounts = null;
            bool retval = false;
            string cashDrawerId = GlobalDataAccessor.Instance.DesktopSession.CashDrawerId;
            do
            {
                try
                {
                    
                    //Call to SP to get the list of transactions against the cash drawer
                    retval = ShopCashProcedures.GetCashDrawerAmount(cashDrawerId, ShopDateTime.Instance.ShopDate.FormatDate(), "N", GlobalDataAccessor.Instance.DesktopSession,
                        out cashdrawerAmounts, out errorCode, out errorMesg);
                    if (!retval)
                    {
                        dgr = MessageBox.Show("Failed to get the cash drawer balance. Cash drawer cannot be closed", "Cash Drawer Error", MessageBoxButtons.RetryCancel);
                    }
                    else
                        dgr = DialogResult.Cancel;
                }
                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException("Failed to get the balance amount in the cash drawer", new ApplicationException(ex.Message));
                }
            } while (dgr == DialogResult.Retry);
            if (retval && cashdrawerAmounts != null && cashdrawerAmounts.Rows.Count > 0)
            {
                //Parse the transactions to add and subtract the amounts based on the
                //Transactions
                BalanceAmount = ShopCashProcedures.GetCashDrawerAmount(cashdrawerAmounts);

            }
            if (BalanceAmount < 0)
            {
                richTextBoxTopsTransfer.Text = "Your ledger balance will be adjusted by " + String.Format("{0:C}", BalanceAmount * -1) + " to settle your Cashlinx drawer." + System.Environment.NewLine +
                    TRANSFERMESSAGE + Environment.NewLine + 
                    TOPSOUTMESSAGE + Environment.NewLine + "Amount:" + String.Format("{0:C}", BalanceAmount * -1) + Environment.NewLine + 
                    CASHLINXTRANSFERINMESSAGE + GlobalDataAccessor.Instance.DesktopSession.CashDrawerName;
                
            }
            else
            {
                richTextBoxTopsTransfer.Text = "Your ledger balance will be adjusted by " + String.Format("{0:C}", BalanceAmount) + " to settle your Cashlinx drawer." + System.Environment.NewLine +
                    TRANSFERMESSAGE + Environment.NewLine + 
                    TOPSINMESSAGE + Environment.NewLine + "Amount:" + String.Format("{0:C}", BalanceAmount) + Environment.NewLine + 
                    CASHLINXTRANSFEROUTMESSAGE + GlobalDataAccessor.Instance.DesktopSession.CashDrawerName;
                
            }
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            string tranTime = ShopDateTime.Instance.ShopTransactionTime;
            string cashDrawerId = GlobalDataAccessor.Instance.DesktopSession.CashDrawerId;
            string errorCode;
            string errorMsg;
            DialogResult dgr = DialogResult.Retry;
            do
            {
                bool retVal = ShopCashProcedures.TopsTransfer(GlobalDataAccessor.Instance.OracleDA, cashDrawerId, GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                      tranTime, GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                      GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                      BalanceAmount, ShopDateTime.Instance.ShopDate.ToShortDateString(),
                      GlobalDataAccessor.Instance.DesktopSession.UserName,
                      "",
                      out errorCode, out errorMsg);
                dgr = !retVal ? MessageBox.Show("Error writing TOPS transfer records " + errorMsg + " for " + cashDrawerId, "Cash Drawer Error", MessageBoxButtons.RetryCancel) : DialogResult.Cancel;
            }
            while (dgr == DialogResult.Retry);
            this.Close();

        }

    }
}
