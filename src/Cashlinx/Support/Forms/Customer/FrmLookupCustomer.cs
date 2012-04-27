using System;
using System.Windows.Forms;

namespace Support.Forms.Customer
{
    public partial class FrmLookupCustomer : Form
    {
        public FrmLookupCustomer()
        {
            InitializeComponent();
            rbSSN.Checked = true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rbSSN.Checked = true;
            txtSSN1.Text = string.Empty;
            txtSSN2.Text = string.Empty;
            txtSSN3.Text = string.Empty;


        }

        private void rbSSN_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSSN.Checked)
            {
                txtSSN1.Enabled = true;
                txtSSN2.Enabled = true;
                txtSSN3.Enabled = true;
            }
            else
            {
                txtSSN1.Enabled = false;
                txtSSN2.Enabled = false;
                txtSSN3.Enabled = false;
                txtSSN1.Text = string.Empty;
                txtSSN2.Text = string.Empty;
                txtSSN3.Text = string.Empty;
            }
        }

        private void rbCustInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCustInfo.Checked)
            {
                txtSSN4.Enabled = true;
                txtAreaCode.Enabled = true;
                txtPhonePre.Enabled = true;
                txtPhoneSuf.Enabled = true;
                txtLastname.Enabled = true;
                txtFirstName.Enabled = true;
                txtDOB.Enabled = true;
                txtCity.Enabled = true;
                txtBankAcct.Enabled = true;
                cbState.Enabled = true;
            }
            else
            {
                txtSSN4.Enabled = false;
                txtAreaCode.Enabled = false;
                txtPhonePre.Enabled = false;
                txtPhoneSuf.Enabled = false;
                txtLastname.Enabled = false;
                txtFirstName.Enabled = false;
                txtDOB.Enabled = false;
                txtCity.Enabled = false;
                txtBankAcct.Enabled = false;
                cbState.Enabled = false;
                txtSSN4.Text = string.Empty;
                txtAreaCode.Text = string.Empty;
                txtPhonePre.Text = string.Empty;
                txtPhoneSuf.Text = string.Empty;
                txtLastname.Text = string.Empty;
                txtFirstName.Text = string.Empty;
                txtDOB.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtBankAcct.Text = string.Empty;
            }
        }

        private void rbIDSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIDSearch.Checked)
            {
                cbIDType.Enabled = true;
                cbIssuer.Enabled = true;
                txtIDNumber.Enabled = true;
            }
            else
            {
                cbIDType.Enabled = false;
                cbIssuer.Enabled = false;
                txtIDNumber.Enabled = false;

                txtIDNumber.Text = string.Empty;
            }
        }

        private void rbCustNum_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCustNum.Checked)
            {
                txtCustNum.Enabled = true;
            }
            else
            {
                txtCustNum.Enabled = false;
                txtCustNum.Text = string.Empty;
            }
        }

        private void rbLoan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLoan.Checked)
            {
                cbProductType.Enabled = true;
                txtShopName.Enabled = true;
                txtLoanNbr.Enabled = true;
            }
            else
            {
                cbProductType.Enabled = false;
                txtShopName.Enabled = false;
                txtLoanNbr.Enabled = false;

                txtShopName.Text = string.Empty;
                txtLoanNbr.Text = string.Empty;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (rbSSN.Checked)
            {
                string SocSecNum;

                try
                {
                    Convert.ToInt32(txtSSN1.Text);
                    Convert.ToInt32(txtSSN2.Text);
                    Convert.ToInt32(txtSSN3.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid Social Security Number");
                    txtSSN1.Text = string.Empty;
                    txtSSN2.Text = string.Empty;
                    txtSSN3.Text = string.Empty;
                    txtSSN1.Focus();
                    return;
                }

                SocSecNum = txtSSN1.Text + txtSSN2.Text + txtSSN3.Text;
            }
        }
    }
}
