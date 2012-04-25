using System;
using System.Windows.Forms;
using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.ChargeOff
{
    public partial class ChargeOffCharityData : CustomBaseForm  
    {
        public string CharityOrg;
        public string CharityAddr;
        public string CharityCity;
        public string CharityState;
        public string CharityZip;

        public ChargeOffCharityData()
        {
            InitializeComponent();
        }

        private void customButton1_Click(object sender, EventArgs e)
        {

            if (customTextBoxOrg.Text.Length == 0 || customTextBoxAddr1.Text.Length == 0 ||
                customTextBoxCity.Text.Length == 0 || customTextBoxState.Text.Length == 0 ||
                customTextBoxZip.Text.Length == 0)
            {
                MessageBox.Show("All Fields are required", "Invalid Input", MessageBoxButtons.OK);
            }
            else
            {
                CharityOrg = customTextBoxOrg.Text;
                CharityAddr = customTextBoxAddr1.Text + " " + customTextBoxAddr2.Text;
                CharityCity = customTextBoxCity.Text;
                CharityState = customTextBoxState.Text;
                CharityZip = customTextBoxZip.Text;

                this.DialogResult = DialogResult.OK;

                this.Close();
            }

        }


        private void customButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
