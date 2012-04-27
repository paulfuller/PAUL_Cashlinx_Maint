using System;
using System.Windows.Forms;

namespace Pawn.Forms.Pawn.Loan
{
    public partial class LoanSummary : Form
    {
        private static readonly string BLANK = "";
        private static readonly string CURRENCY_FORMAT = "{0:f}";

        public LoanSummary()
        {
            InitializeComponent();
        }

        public void PopulateForm(string name, string loanTotal, string ticketText)
        {
            this.nameTextBox.Text = (name ?? BLANK);
            this.loanTotalTextBox.Text = string.Format(CURRENCY_FORMAT, loanTotal);
            this.dateTextBox.Text = DateTime.Now.ToShortDateString();
            this.ticketDetailsTextBox.Text = (ticketText ?? BLANK);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
    }
}
