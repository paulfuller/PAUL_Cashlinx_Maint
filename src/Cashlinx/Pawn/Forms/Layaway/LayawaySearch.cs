using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Layaway
{
    public partial class LayawaySearch : CustomBaseForm
    {
        public NavBox NavControlBox;
        public LayawaySearch()
        {
            InitializeComponent();
            NavControlBox = new NavBox
            {
                Owner = this
            };
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            string errorCode;
            string errorText;

            CustomerVO customerObj = null;
            LayawayVO layawayObj = null;

            bool retVal = RetailProcedures.GetLayawayData(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.OracleDA, Utilities.GetIntegerValue(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, 0),
                        Utilities.GetIntegerValue(txtLayawayNumber.Text, 0), "0", StateStatus.BLNK, "LAY", true, out layawayObj, out customerObj, out errorCode, out errorText);

            if (!retVal || layawayObj == null)
            {
                MessageBox.Show("No records found or not the originating shop of the number entered.");
                txtLayawayNumber.Focus();
                return;
            }
            //Check if the layaway has a status of PAID in which case only the sale can be refunded and not the layaway payment
            if (layawayObj.LoanStatus == ProductStatus.PAID)
            {
                MessageBox.Show("Layaway has been paid out. Can only refund sale at this point.");
                txtLayawayNumber.Focus();
                return;

            }
            //SR 07/01/2011 Check if the layaway has a status of LAY. Any other status cannot be refunded.
            if (layawayObj.LoanStatus != ProductStatus.ACT)
            {
                MessageBox.Show("Layaway is in " + layawayObj.LoanStatus.ToString() + " status. Cannot be refunded.");
                txtLayawayNumber.Focus();
                return;
            }

            /*int maxDaysForRefundEligibility = BusinessRulesProcedures.GetMaxDaysForRefundEligibility(CDS.CurrentSiteId);
            if (ShopDateTime.Instance.FullShopDateTime > layawayObj.DateMade.AddDays(maxDaysForRefundEligibility))
            {
                MessageBox.Show("The number of days eligible for refund has expired for the MSR number entered");
                return;
            }?*/

            GlobalDataAccessor.Instance.DesktopSession.Layaways = new List<LayawayVO>();
            GlobalDataAccessor.Instance.DesktopSession.Layaways.Add(layawayObj);
            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = customerObj;
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "SHOWITEMS";
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;


        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || (this.ActiveControl.Equals(cancelButton) && keyData == Keys.Enter))
            {
                this.cancelButton_Click(null, new EventArgs());
                return true;
            }

            if (keyData == Keys.Enter)
            {
                this.submitButton_Click(null, new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
