using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Forms.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Retail
{
    public partial class ReceiptSearch : CustomBaseForm
    {
        public NavBox NavControlBox;

        public ReceiptSearch()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
        }

        public DesktopSession CDS
        {
            get { return GlobalDataAccessor.Instance.DesktopSession; }
        }

        private ProcessingMessage ProcessingMessage { get; set; }

        private void submitButton_Click(object sender, EventArgs e)
        {
            int ticketNumber = Utilities.GetIntegerValue(txtReceiptNumber.Text, 0);

            if (ticketNumber <= 0)
            {
                MessageBox.Show("A valid ticket number is required.");
                return;
            }

            this.Hide();
            ProcessingMessage = new ProcessingMessage("Loading...");
            SetButtonState(false);
            var workerLoadRetail = new BackgroundWorker();
            workerLoadRetail.DoWork += workerLoadRetail_DoWork;
            workerLoadRetail.RunWorkerCompleted += workerLoadRetail_RunWorkerCompleted;
            workerLoadRetail.RunWorkerAsync(Utilities.GetIntegerValue(txtReceiptNumber.Text, 0));
            ProcessingMessage.ShowDialog(this);
        }

        void workerLoadRetail_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ProcessingMessage != null)
            {
                ProcessingMessage.Close();
                ProcessingMessage.Dispose();
                SetButtonState(true);
            }

            bool b = Utilities.GetBooleanValue(e.Result, true);
            if (b)
            {
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "SHOWITEMS";
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                this.Show();
            }
        }

        private void SetButtonState(bool enable)
        {
            cancelButton.Enabled = enable;
            submitButton.Enabled = enable;
        }

        void workerLoadRetail_DoWork(object sender, DoWorkEventArgs e)
        {
            string errorCode;
            string errorText;
            CustomerVO customerObj = null;
            SaleVO saleObj = null;
            int ticketNumber = (int)e.Argument;

            RetailProcedures.GetSaleData(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.OracleDA, Utilities.GetIntegerValue(CDS.CurrentSiteId.StoreNumber, 0),
                    ticketNumber, "3", StateStatus.BLNK, "SALE", true, out saleObj, out customerObj, out errorCode, out errorText);
            
            Action<string> actionShowMessageBox = s => MessageBox.Show(s);
            Action<Control> setFocus = c => c.Focus();

            if (saleObj == null || saleObj.RetailItems.Count == 0)
            {
                this.Invoke(actionShowMessageBox, new object[] { "No records found or not the originating shop of the number entered." });
                this.Invoke(setFocus, new object[] { txtReceiptNumber });
                e.Result = false;
                return;
            }
            else
            {
                int maxDaysForRefundEligibility = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxDaysForRefundEligibility(CDS.CurrentSiteId);
                if (ShopDateTime.Instance.FullShopDateTime.Date > saleObj.DateMade.AddDays(maxDaysForRefundEligibility))
                {
                    this.Invoke(actionShowMessageBox, new object[] { "The number of days eligible for refund has expired for the MSR number entered." });
                    this.Invoke(setFocus, new object[] { txtReceiptNumber });
                    e.Result = false;
                    return;
                }

                this.Invoke(new Action(() =>
                    {
                        GlobalDataAccessor.Instance.DesktopSession.Sales = new List<SaleVO>();
                        GlobalDataAccessor.Instance.DesktopSession.Sales.Add(saleObj);
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = customerObj;
                    }));
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void ReceiptSearch_Load(object sender, EventArgs e)
        {
            NavControlBox.Owner = this;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && !this.ActiveControl.Equals(cancelButton))
            {
                submitButton_Click(this, EventArgs.Empty);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
