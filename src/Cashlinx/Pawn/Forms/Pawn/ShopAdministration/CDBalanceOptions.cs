/********************************************************************
* Namespace: CashlinxDesktop.DesktopForms.Pawn.ShopAdministration
* FileName: ExitOptions
* This form is shown when the logged in user who 
* has access to safe functionalities exits the application
* Sreelatha Rengarajan 3/10/2010 Initial version
*******************************************************************/

using System;
using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    public partial class CDBalanceOptions : CustomBaseForm
    {
        public bool TrialBalance
        {
            get
            {
                return _trialBalance;
            }
            
        }
        private bool _trialBalance;
        private bool _otherCDbalance;
        public bool OtherCDBalance
        {
            get
            {
                return _otherCDbalance;
            }
        }
        public CDBalanceOptions()
        {
            InitializeComponent();
        }

        private void customButtonBalanceCashDrawer_Click(object sender, EventArgs e)
        {
            _otherCDbalance = true;
            this.Close();
        }

        private void customButtonExit_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void customButtonBalanceSafe_Click(object sender, EventArgs e)
        {
            _trialBalance = true;
            this.Close();
 
        }
    }
}
