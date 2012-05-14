/****************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products.ProductHistory
 * Class:           Extension_Dialog
 * 
 * Description      Popup Form to show specific information of a Customer
 *                  Pawn Loan Extension.
 * 
 * History
 * Tracy McConnell, Initial Development
 * 
 * Fixes/Mods
 *  PWNU00000220 S.Murphy populate PH_SuggestedAmountColumn and PH_LocationColumn
 *****************************************************************************/

using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;

namespace Support.Forms.Customer.Products.ProductHistory
{
    public partial class Extension_Dialog : Form
    {
        private DataRow _Data;
        private PawnLoan _PawnLoan;

        public NavBox NavControlBox;

        public Extension_Dialog(PawnLoan pawn, DataRow extensionData)
        {
            InitializeComponent();

            //Does not need this. -- Removed by TLR 6/14/2010
            //this.NavControlBox = new NavBox();
            //this.NavControlBox.Owner = this;

            _PawnLoan = pawn;
            _Data = extensionData;

            if (_Data == null)
            {
                MessageBox.Show("Pass in a Pawn Loan to display page.", "Pawn Loan Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                Setup();
        }

        private void Setup()
        {
            PH_OriginationDateValue.Text = Utilities.GetStringValue(_Data["ORIG_DTS"], "");

            string loanStatus = (from l in GlobalDataAccessor.Instance.DesktopSession.LoanStatus
                                where l.Left == _PawnLoan.LoanStatus
                                select l.Right).First<string>();
            PH_LoanStatusValue.Text =  loanStatus;

            Text = "Pawn Loan " + _PawnLoan.TicketNumber.ToString() + " - "
                        + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName
                        + " "
                        + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName;


            PH_TerminalIDValue.Text = Utilities.GetStringValue(_Data["EXT_SHOP"], "");

            PH_DueDateValue.Text = GetDateValue(_Data["ORIG_DUE"]);                
            //    _PawnLoan.DueDate.ToShortDateString();

            PH_EligibilityDateValue.Text = GetDateValue(_Data["ORIG_PFI"]); 
            //_PawnLoan.PfiEligible.ToShortDateString();

            PH_NotificationDateValue.Text = GetDateValue(_Data["ORIG_PFI_NOTE"]); 
            //_PawnLoan.PfiNote.ToShortDateString();

            PH_LoanValue.Text = String.Format("{0:C}", Utilities.GetFloatValue(_Data["EXT_AMT"],(float)_PawnLoan.Amount) );

            PH_MadeByEmpValue.Text = Utilities.GetStringValue(_Data["EXT_MADE_BY"], _PawnLoan.CreatedBy);
            ExtDateTime_Value.Text = Utilities.GetStringValue(_Data["EXT_ON"], "");
            ExtDueDate_Value.Text = GetDateValue(_Data["EXT_DUE"]);
            ExtPFIDate_Value.Text = GetDateValue(_Data["EXT_PFI"]);
            ExtPFINotifyDate_Value.Text = GetDateValue(_Data["EXT_PFI_NOTE"]);

            if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId))
            {
                PH_CurrentPrincipal.Text = _PawnLoan.CurrentPrincipalAmount.ToString("c");
            }
            else
            {
                PH_CurrentPrincipal.Visible = false;
                PH_CurrentPrincipalLabel.Visible = false;
            }

        }

        private string GetDateValue(object p)
        {
            var stringValue = Utilities.GetStringValue(p, string.Empty);
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return stringValue;
            }
            
            return Utilities.GetDateTimeValue(stringValue).ToString("d");
        }

        private void PH_CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
