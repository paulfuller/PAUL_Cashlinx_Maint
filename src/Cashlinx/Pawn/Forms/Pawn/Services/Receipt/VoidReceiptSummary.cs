using System;
using System.Globalization;
using System.Windows.Forms;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Collection;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.Receipt
{
    public partial class VoidReceiptSummary : Form
    {
        //public NavBox NavControlBox;
        private PawnLoan pwnLoan;
        private PawnAppVO pwnApp;
        public VoidReceiptSummary(PawnLoan pLoan, PawnAppVO pApp)
        {
            InitializeComponent();
            //this.NavControlBox = new NavBox();
            this.pwnApp = pApp;
            this.pwnLoan = pLoan;
        }

        private void VoidReceiptSummary_Load(object sender, EventArgs e)
        {
            //this.NavControlBox.Owner = this;
            this.loadData();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            //Fire off ProcessTender.voidProcess
            if (!ProcessTenderController.Instance.ExecuteProcessTender(
                    ProcessTenderProcedures.ProcessTenderMode.VOIDLOAN))
            {
                //Throw error
                this.DialogResult = DialogResult.Abort;
            }
            else
            {
                MessageBox.Show("Successfully voided loan");
                this.DialogResult = DialogResult.OK;
            }
            this.Close();            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void loadData()
        {
            this.orgDateTimeTextBox.Text = "" + 
                this.pwnLoan.OriginationDate.ToString("d", DateTimeFormatInfo.InvariantInfo) + " " +
                this.pwnLoan.OriginationDate.ToShortTimeString();

            this.dueDateTextBox.Text = "" +
                this.pwnLoan.DueDate.ToString("d", DateTimeFormatInfo.InvariantInfo);

            this.loanNumberTextBox.Text = "" + this.pwnLoan.OrgShopNumber + this.pwnLoan.TicketNumber;
            this.loanAmountTextBox.Text = "" + this.pwnLoan.Amount.ToString("C");
            this.userIdTextBox.Text = "" + this.pwnLoan.CreatedBy;

            if (!CollectionUtilities.isNotEmpty(this.pwnLoan.Items))
            {
                return;
            }
            foreach (Item pItem in this.pwnLoan.Items)
            {
                var lViewRow = new ListViewItem("" + pItem.Icn);
                if (pItem.QuickInformation.IsGun)
                {
                    lViewRow.SubItems.Add("#NUM");
                }
                else
                {
                    lViewRow.SubItems.Add(" ");
                }
                lViewRow.SubItems.Add("" + pItem.TicketDescription);
                lViewRow.SubItems.Add("" + pItem.ItemAmount.ToString("C"));
                this.pawnItemListView.Items.Add(lViewRow);
            }
            this.pawnItemListView.Update();
        }
    }
}
