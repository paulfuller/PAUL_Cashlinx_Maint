/********************************************************************
* Namespace: CashlinxDesktop.DesktopForms.Pawn.ShopAdministration
* FileName: ExitOptions
* This form is shown when the logged in user who 
* has access to safe functionalities exits the application
* Sreelatha Rengarajan 3/10/2010 Initial version
*******************************************************************/

using System;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility.Exception;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    public partial class ExitOptions : CustomBaseForm
    {
        public ExitOptions()
        {
            InitializeComponent();
        }

        private void customButtonBalanceCashDrawer_Click(object sender, EventArgs e)
        {
            bool checkPassed;
            GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = "BalanceCashDrawer";
            GlobalDataAccessor.Instance.DesktopSession.PerformCashDrawerChecks(out checkPassed);
            if (checkPassed)
            {
                BalanceCash cashbalanceForm = new BalanceCash();
                cashbalanceForm.SafeBalance = false;
                cashbalanceForm.ShowDialog();
            }

        }

        private void customButtonExit_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void customButtonBalanceSafe_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = "BalanceSafe";
                bool checkPassed = false;
                GlobalDataAccessor.Instance.DesktopSession.PerformCashDrawerChecks(out checkPassed);
                if (checkPassed)
                {
                    BalanceCash cashbalanceForm = new BalanceCash();
                    cashbalanceForm.SafeBalance = true;
                    cashbalanceForm.Show();
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Error balancing safe " + ex.Message, new ApplicationException());
            }
        }
    }
}
