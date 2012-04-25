using System;
using System.Windows.Forms;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;

namespace Pawn.Forms.Pawn.Tender.Base
{
    public partial class TenderEntryForm : CustomBaseForm
    {
        public TenderEntryVO TenderEntry { get; private set; }
        public bool IsValid { get; private set; }
        public decimal SetAmount
        {
            set
            {
                amountEntryTextBox.Text = value.ToString();
            }
        }
        public string SetReferenceNumber
        {
            set
            {
                this.refNumTextBox.Text = value;
            }
        }
        public string SetCardType
        {
            set
            {
                this.creditTypeListBox.SelectedIndex = creditTypeListBox.FindString(value);
            }
        }

        public TenderEntryForm(TenderTypes tendType)
        {
            InitializeComponent();
            TenderEntry = new TenderEntryVO();
            TenderEntry.TenderType = tendType;
            IsValid = false;
        }

        private void showCreditBox(bool showVal)
        {
            if (showVal)
            {
                showDebitBox(false);
                creditTypeListBox.Enabled = true;
                creditTypeListBox.Show();
                creditTypeListBox.BringToFront();
                //creditTypeListBox.ClearSelected();
                creditLabel.Show();
                creditLabel.BringToFront();
            }
            else
            {
                creditTypeListBox.Enabled = false;
                creditTypeListBox.Hide();
                creditTypeListBox.SendToBack();
                creditLabel.Hide();
                creditLabel.SendToBack();
            }
        }

        private void showDebitBox(bool showVal)
        {
            if (showVal)
            {
                showCreditBox(false);
                debitTypeListBox.Enabled = true;
                debitTypeListBox.Show();
                debitTypeListBox.BringToFront();
                //debitTypeListBox.ClearSelected();
                creditLabel.Show();
                creditLabel.BringToFront();
            }
            else
            {
                debitTypeListBox.Enabled = false;
                debitTypeListBox.Hide();
                debitTypeListBox.SendToBack();
                creditLabel.Hide();
                creditLabel.SendToBack();
            }
        }

        private void hideCreditDebitBox()
        {
            showCreditBox(false);
            showDebitBox(false);
        }

        private void showRefInput(bool showVal)
        {
            if (showVal)
            {
                this.refNumLabel.Show();
                this.refNumLabel.BringToFront();
                this.refNumTextBox.Visible = true;
                this.refNumTextBox.Enabled = true;
                this.refNumTextBox.BringToFront();
                //this.refNumTextBox.Clear();

            }
            else
            {
                this.refNumLabel.Hide();
                this.refNumLabel.SendToBack();
                this.refNumTextBox.Clear();
                this.refNumTextBox.Hide();
                this.refNumTextBox.Enabled = false;
                this.refNumTextBox.SendToBack();
            }
        }

        private void TenderEntryForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.Controls["refNumTextBox"];
            switch(TenderEntry.TenderType)
            {
                case TenderTypes.CASHIN:
                    tenderTypeComboBox.SelectedItem = 
                        tenderTypeComboBox.Items[0];
                    IsValid = true;
                    showRefInput(false);
                    hideCreditDebitBox();
                    this.ActiveControl = this.Controls["amountEntryTextBox"];
                    break;
                case TenderTypes.BILLTOAP:
                    tenderTypeComboBox.SelectedItem =
                        tenderTypeComboBox.Items[6];
                    IsValid = false;
                    showRefInput(true);
                    hideCreditDebitBox();
                    break;
                case TenderTypes.CASHOUT:
                    tenderTypeComboBox.SelectedItem =
                        tenderTypeComboBox.Items[0];
                    IsValid = true;
                    showRefInput(false);
                    hideCreditDebitBox();
                    this.ActiveControl = this.Controls["amountEntryTextBox"];
                    break;
                case TenderTypes.CHECK:
                    tenderTypeComboBox.SelectedItem =
                        tenderTypeComboBox.Items[1];
                    IsValid = true;
                    showRefInput(true);
                    hideCreditDebitBox();
                    break;
                case TenderTypes.CREDITCARD:
                    tenderTypeComboBox.SelectedItem =
                        tenderTypeComboBox.Items[2];
                    IsValid = true;
                    showCreditBox(true);
                    showRefInput(true);
                    break;
                case TenderTypes.DEBITCARD:
                    tenderTypeComboBox.SelectedItem =
                        tenderTypeComboBox.Items[3];
                    IsValid = true;
                    showDebitBox(true);
                    showRefInput(true);
                    break;
                case TenderTypes.STORECREDIT:
                    tenderTypeComboBox.SelectedItem =
                        tenderTypeComboBox.Items[7];
                    IsValid = false;
                    showRefInput(false);
                    hideCreditDebitBox();
                    break;
                case TenderTypes.PAYPAL:
                    tenderTypeComboBox.SelectedItem =
                        tenderTypeComboBox.Items[5];
                    IsValid = false;
                    showRefInput(true);
                    hideCreditDebitBox();
                    break;
                case TenderTypes.COUPON:
                    tenderTypeComboBox.SelectedItem =
                        tenderTypeComboBox.Items[4];
                    IsValid = true;
                    showRefInput(true);
                    hideCreditDebitBox();
                    break;
            }
            
            tenderTypeComboBox.Enabled = false;
        }

        private void debitTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selDebitValue = (debitTypeListBox.SelectedItem == null) 
                ? string.Empty : debitTypeListBox.SelectedItem.ToString();
            if (string.IsNullOrEmpty(selDebitValue)) return;
            var selDebitValueUp = selDebitValue.ToUpperInvariant();
            var tObj = Utilities.GetEnumFromConstantName<DebitCardTypes>(selDebitValueUp);
            if (tObj != null)
            {
                var debType = (DebitCardTypes)tObj;
                this.TenderEntry.DebitCardType = debType;
                this.TenderEntry.CardTypeString = selDebitValue;
            }
            else
            {
                this.TenderEntry.CardTypeString = string.Empty;
            }
        }

        private void creditTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selCreditValue = (creditTypeListBox.SelectedItem == null)
                ? string.Empty : creditTypeListBox.SelectedItem.ToString();
            if (string.IsNullOrEmpty(selCreditValue))
                return;
            var selCreditValueUp = selCreditValue.ToUpperInvariant();
            var tObj = Utilities.GetEnumFromConstantName<CreditCardTypes>(selCreditValueUp);
            if (tObj != null)
            {
                var credType = (CreditCardTypes)tObj;
                this.TenderEntry.CreditCardType = credType;
                this.TenderEntry.CardTypeString = selCreditValue;
            }
            else
            {
                this.TenderEntry.CardTypeString = string.Empty;
            }
        }

        private void amountEntryTextBox_TextChanged(object sender, EventArgs e)
        {
            var amountVal = this.amountEntryTextBox.Text;
            if (string.IsNullOrEmpty(amountVal))
            {
                this.TenderEntry.Amount = 0.0M;
                return;
            }

            if (amountVal.Length == 1 && amountVal.Equals("."))
            {
                amountVal = "0.";
            }


            if (StringUtilities.IsDecimal(amountVal))
            {
                decimal dLoanAmount = Convert.ToDecimal(amountVal);
                this.TenderEntry.Amount = dLoanAmount;
            }
            else
            {
                
                amountEntryTextBox.Text = amountVal.Substring(0, amountVal.Length - 1);
                int selStart = amountEntryTextBox.SelectionStart;
                amountEntryTextBox.SelectionStart = selStart;
                this.TenderEntry.Amount = 0.0M;
            }
        }

        private void refNumTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(refNumTextBox.Text))
            {
                this.TenderEntry.ReferenceNumber = string.Empty;
                return;
            }
            this.TenderEntry.ReferenceNumber = refNumTextBox.Text;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(amountEntryTextBox.Text))
            {
                MessageBox.Show("Please enter a valid monetary amount.", 
                    "Process Tender Warning");
                return;
            }
            if (this.TenderEntry.TenderType != TenderTypes.CASHIN &&
                this.TenderEntry.TenderType != TenderTypes.CASHOUT &&
                this.TenderEntry.TenderType != TenderTypes.STORECREDIT)
            {
                //Check reference number
                if (string.IsNullOrEmpty(this.refNumTextBox.Text))
                {
                    MessageBox.Show("Please enter a valid reference number.",
                                    "Process Tender Warning");
                    return;
                }

                //Check card types
                if (this.TenderEntry.TenderType == TenderTypes.CREDITCARD ||
                    this.TenderEntry.TenderType == TenderTypes.DEBITCARD)
                {
                    if (string.IsNullOrEmpty(this.TenderEntry.CardTypeString))
                    {
                        MessageBox.Show("Please select a valid card type.",
                                        "Process Tender Warning");
                        return;
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
