/********************************************************************
* Namespace: CashlinxDesktop.DesktopForms.Pawn.Loan
* FileName: TenderCash
* This form is shown to enter the cash tendered by the customer for a transaction
* Sreelatha Rengarajan 9/30/2009 Initial version
*******************************************************************/
//***********************************************************************************
// 4-mar-2010 rjm  modified screen to allign values and removed $
//                 from textBoxDueFromCustomer.text
//
// 5-mar-2010 rjm  modified customTextBoxCashIn_Leave() to have 
//                 the display of 2 digits after decimal point
// SR 4/7/2010 Fixed PWNU00000495. Added checks to make sure that the app
// does not proceed when continue button is clicked if the amount entered is
//less than amount owed. The check was happening only once on textbox leave event
//but not when they clicked on continue right after the message without changing value
//**************************************************************************************

using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Support.Forms.Customer.Pawn.Loan
{
    public partial class TenderCash : Form
    {
        private bool isFormValid = false;
        public TenderCash()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.CashTenderFromCustomer = 0;
            isFormValid = true;
            this.Close();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            if (isFormValid || buttonContinue.Text == "C&ontinue")
            {

                if (!customTextBoxCashIn.isValid || Utilities.GetDecimalValue(customTextBoxCashIn.Text) <= 0)
                {
                    MessageBox.Show(Commons.GetMessageString("InvalidAmount"));
                    isFormValid = false;
                }
                else
                {
                    if (buttonContinue.Text == "C&alculate")
                    {
                        buttonContinue.Text = "C&ontinue";
                        customTextBoxCashIn.ReadOnly = true;
                        isFormValid = false;
                        return;
                    }
                    isFormValid = true;
                    GlobalDataAccessor.Instance.DesktopSession.CashTenderFromCustomer =
                        Utilities.GetDecimalValue(this.customTextBoxCashIn.Text);
                    this.Close();
                }
            }
            else
                return;

        }

        private void TenderCash_Load(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.CashTenderFromCustomer = 0;
            customTextBoxCashIn.ErrorMessage = Commons.GetMessageString("InvalidAmount");
           textBoxDueFromCustomer.Text = GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount.ToString("0.00");
            this.customTextBoxCashIn.Focus();

        }

        private void customTextBoxCashIn_Leave(object sender, EventArgs e)
        {
           decimal tmp1;
            if (customTextBoxCashIn.isValid && Utilities.GetDecimalValue(customTextBoxCashIn.Text) > 0)
            {
               decimal.TryParse(customTextBoxCashIn.Text, out tmp1);
               customTextBoxCashIn.Text = tmp1.ToString("0.00");
                decimal changeDue = Math.Round(Utilities.GetDecimalValue(customTextBoxCashIn.Text) -
                                    Utilities.GetDecimalValue(
                                        GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount),2);
                if (changeDue < 0)
                {
                    MessageBox.Show("Cash from customer cannot be less than the amount due");
                    isFormValid = false;
                }
                else
                {
                    labelChangeDueCustomer.Text = changeDue.ToString();
                    isFormValid = true;
                }
            }
 

        }

        private void TenderCash_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !isFormValid;
        }

    
    }
}
