using System;
using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.ChargeOff
{
    public partial class ChargeOffPoliceInfo : CustomBaseForm
    {
        public string ATFNumber;
        public string CaseNumber;
        public ChargeOffPoliceInfo()
        {
            InitializeComponent();
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            ATFNumber = customTextBoxATF.Text;
            CaseNumber = customTextBoxCaseNo.Text;
            this.Close();
        }
    }
}
