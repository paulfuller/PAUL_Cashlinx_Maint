using System;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Tender
{
    public partial class DisbursementConfirmation : CustomBaseForm
    {
        #region Private event Handlers
        private void okButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber))
            {
                var dR = MessageBox.Show("Would you like to continue servicing this customer?",
                                                  "Process Tender Message", MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
                if (dR == DialogResult.No)
                {
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
                }
                else
                {
                    //Reload customer data
                    var activeCustomer = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber);
                    if (activeCustomer != null)
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = activeCustomer;
                }
            }

            GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();
            var nDesk = (NewDesktop)GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
            nDesk.handleEndFlow(null);

            this.Close();
            this.Dispose();
        }


        private void DisbursementConfirmation_Load(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {
                this.customerNameFieldLabel.Text = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerName;
            }
            //BZ # 563
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway != null &&
                !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.DownPayment.ToString("C")))
            {
                this.amountToPayFieldLabel.Text = GlobalDataAccessor.Instance.DesktopSession.TenderAmounts.Sum(s => Utilities.GetDecimalValue(s, 0)).ToString("C"); 
            }
            else
            {
                this.amountToPayFieldLabel.Text = GlobalDataAccessor.Instance.DesktopSession.TenderTransactionAmount.TotalAmount.ToString("C");
            }
            //BZ # 563 end

            if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail != null && !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TicketNumber.ToString()))
                this.tickerNumberFieldLabel.Text = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TicketNumber.ToString();
            else if (GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway != null && !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.TicketNumber.ToString()))
                this.tickerNumberFieldLabel.Text = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.TicketNumber.ToString();
        }
        #endregion

        #region Constructors
        public DisbursementConfirmation()
        {
            InitializeComponent();
        }
        #endregion

    }
}
