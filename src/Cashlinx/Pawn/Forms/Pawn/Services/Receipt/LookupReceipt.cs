using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.String;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Services.Receipt
{
    public partial class LookupReceipt : Form
    {
        public NavBox NavControlBox;

        public LookupReceipt()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();           
        }

        private void updateErrorMessage(string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                this.errorMessageLabel.Text = string.Empty;
            }
            else
            {
                this.errorMessageLabel.Text = msg;
            }
            this.errorMessageLabel.Update();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            this.updateErrorMessage(null);
            List<Common.Libraries.Utility.Shared.Receipt> loadedReceipts;
            string errorMsg;
            if (!LoadReceiptData(this.receiptTextBox.Text, out loadedReceipts, out errorMsg))
            {
                this.updateErrorMessage(errorMsg);
            }
            else if (CollectionUtilities.isNotEmpty(loadedReceipts))
            {
                GlobalDataAccessor.Instance.DesktopSession.PawnReceipt =
                    loadedReceipts;
                this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
            }
            else if (CollectionUtilities.isEmpty(loadedReceipts))
            {
                this.updateErrorMessage("Unable to find receipt.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiptNum"></param>
        /// <param name="rec"></param>
        public static bool LoadReceiptData(string receiptNum, out List<Common.Libraries.Utility.Shared.Receipt> rec, out string errorMessage)
        {
            errorMessage = string.Empty;
            rec = null;
            if (string.IsNullOrEmpty(receiptNum))
            {
                errorMessage = "Invalid receipt number.";
                return(false);
            }

            DataTable receiptDataTable;
            string errorCode;
            string errorText;

            if (!ProcessTenderProcedures.ExecuteGetReceiptDetails(
                0,
                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                "PAWN", receiptNum,
                out receiptDataTable,
                out errorCode,
                out errorText))
            {
                MessageBox.Show("Could not find receipt.");
                //errorMessage = "Unable to find receipt.";
                return(false);
            }
            rec = CustomerLoans.CreateReceipt(receiptDataTable);
            GlobalDataAccessor.Instance.DesktopSession.PawnReceipt = rec;
            return (true);
        }

        private void LookupReceipt_Load(object sender, EventArgs e)
        {
            this.NavControlBox.Owner = this;
            this.submitButton.Enabled = false;
            this.updateErrorMessage(null);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.updateErrorMessage(null);
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void receiptTextBox_TextChanged(object sender, EventArgs e)
        {
            this.updateErrorMessage(null);
            string receiptText = this.receiptTextBox.Text;
            this.submitButton.Enabled = false;
            this.submitButton.Update();
            if (string.IsNullOrEmpty(receiptText))
            {
                MessageBox.Show("Please enter a valid receipt number", "Receipt Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!StringUtilities.IsInteger(receiptText))
            {
                MessageBox.Show("Receipt number must be numeric", "Receipt Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.submitButton.Enabled = true;
            this.submitButton.Update();
        }
    }
}
