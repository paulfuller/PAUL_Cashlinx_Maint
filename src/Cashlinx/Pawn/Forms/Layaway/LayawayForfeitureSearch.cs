using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Report;
using Pawn.Logic;

namespace Pawn.Forms.Layaway
{
    public partial class LayawayForfeitureSearch : CustomBaseForm
    {
        ProcessingMessage procMsg;
        private bool processedAllForfeit = true;
        List<LayawayVO> processedLayaways = new List<LayawayVO>();

        # region Constructors

        public LayawayForfeitureSearch()
        {
            InitializeComponent();
            dpEligibilityDate.SetSelectedDate(ShopDateTime.Instance.ShopDate);
            txtShopNumber.Text = CDS.CurrentSiteId.StoreNumber;
            WaitDaysForLayawayForfeitureEligibility = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetWaitDaysForLayawayForfeitureEligibility(CDS.CurrentSiteId);
        }

        # endregion

        # region Properties

        public DesktopSession CDS
        {
            get { return GlobalDataAccessor.Instance.DesktopSession; }
        }

        private LayawayForfeitureResults LayawayForfeitureResults { get; set; }
        private LayawayDetailViewer DetailViewer { get; set; }
        private int WaitDaysForLayawayForfeitureEligibility { get; set; }

        # endregion

        # region Event Handlers

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            if (rdoLayawayNumber.Checked)
            {
                procMsg = new ProcessingMessage("Loading layaway");
                bw.DoWork += new DoWorkEventHandler(bw_DoWorkLookupByLayawayTransactionNumber);
                object[] values = new object[] { txtShopNumber.Text, txtTransactionNumber.Text, this };
                bw.RunWorkerAsync(values);
            }
            else
            {
                DateTime eligibilityDate;

                if (!DateTime.TryParse(dpEligibilityDate.SelectedDate, out eligibilityDate))
                {
                    return;
                }

                if (eligibilityDate < ShopDateTime.Instance.ShopDate.Date)
                {
                    MessageBox.Show("Cannot select a date in the past.");
                    return;
                }

                procMsg = new ProcessingMessage("Loading layaways");
                object[] values = new object[] { eligibilityDate, txtShopNumber.Text, this };
                bw.DoWork += new DoWorkEventHandler(bw_DoWorkLookupByEligibilityDate);
                bw.RunWorkerAsync(values);
            }

            try
            {
                SetButtonState(false);
                procMsg.ShowDialog(this);
            }
            catch (TargetInvocationException exc)
            {
                if (exc.InnerException != null)
                {
                    throw exc.InnerException;
                }
                else
                {
                    throw exc;
                }
            }
        }

        private void SetButtonState(bool enable)
        {
            this.btnCancel.Enabled = enable;
            this.btnSubmit.Enabled = enable;
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (procMsg != null)
            {
                procMsg.Close();
                procMsg.Dispose();
                SetButtonState(true);
            }

            Action action = e.Result as Action;
            if (action != null)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(action);
                }
                else
                {
                    action.Invoke();
                }
            }
        }

        void bw_DoWorkLookupByLayawayTransactionNumber(object sender, DoWorkEventArgs e)
        {
            object[] values = e.Argument as object[];
            int storeNumber = Utilities.GetIntegerValue(values[0]);
            int ticketNumber = Utilities.GetIntegerValue(values[1]);
            string errorCode;
            string errorText;
            LayawayForfeitureSearch currentForm = values[2] as LayawayForfeitureSearch;

            LayawayVO layaway = LoadLayaway(storeNumber, ticketNumber);

            if (layaway == null)
            {
                e.Result = new Action(() =>
                    {
                        MessageBox.Show(currentForm, "No records found or not the originating shop of the number entered.");
                        txtTransactionNumber.Focus();
                    });
                return;
            }
            else if (layaway.LoanStatus != ProductStatus.ACT)
            {
                e.Result = new Action(() =>
                {
                    MessageBox.Show(currentForm, "Layaway is not active.");
                    txtTransactionNumber.Focus();
                });
                return;
            }
            else
            {
                //LayawayHistory payment = new LayawayPaymentHistoryBuilder(layaway).GetFirstUnpaidPayment();
                //if (payment != null && payment.PaymentDueDate > ShopDateTime.Instance.FullShopDateTime.AddDays(-WaitDaysForLayawayForfeitureEligibility))
                if (!LayawayProcedures.IsLayawayEligibleForForfeiture(layaway.NextPayment, GlobalDataAccessor.Instance.CurrentSiteId))
                {
                    e.Result = new Action(() =>
                    {
                        MessageBox.Show("Layaway is not eligible for forfeiture.");
                    });
                    return;
                }
                RetailProcedures.SetLayawayTempStatus(layaway.TicketNumber, layaway.StoreNumber, "LYFOR", out errorCode, out errorText);
                e.Result = new Action(() =>
                    {
                        DetailViewer = new LayawayDetailViewer(new List<LayawayVO>() { layaway });
                        DetailViewer.ForfeitCompleted += new EventHandler(detailViewer_ForfeitCompleted);
                        DetailViewer.ShowDialog(currentForm);
                    });
            }
        }

        void bw_DoWorkLookupByEligibilityDate(object sender, DoWorkEventArgs e)
        {
            object[] values = e.Argument as object[];
            DateTime eligibilityDate = Utilities.GetDateTimeValue(values[0]);
            int storeNumber = Utilities.GetIntegerValue(values[1]);
            LayawayForfeitureSearch currentForm = values[2] as LayawayForfeitureSearch;

            List<LayawayVO> eligibleLayaways = SearchForLayaways(storeNumber, eligibilityDate.AddDays(-WaitDaysForLayawayForfeitureEligibility));

            if (eligibleLayaways.Count == 0)
            {
                e.Result = new Action(() =>
                {
                    MessageBox.Show(currentForm, "There were no tickets found");
                });
                return;
            }
            else if (eligibleLayaways.Count > 100)
            {
                eligibleLayaways = eligibleLayaways.Take(100).ToList();
                e.Result = new Action(() =>
                    {
                        MessageBox.Show(currentForm, "The search criteria results have found over 100 records. The system will only display the first 100 records.");
                        OpenLayawayResults(eligibilityDate, eligibleLayaways, currentForm);
                    });
            }
            else
            {
                e.Result = new Action(() =>
                    {
                        OpenLayawayResults(eligibilityDate, eligibleLayaways, currentForm);
                    });
            }
        }

        private void OpenLayawayResults(DateTime eligibilityDate, List<LayawayVO> eligibleLayaways, Form currentForm)
        {
            LayawayForfeitureResults = new LayawayForfeitureResults(eligibilityDate, eligibleLayaways);
            LayawayForfeitureResults.CancelRequested += new EventHandler(LayawayForfeitureResults_CancelRequested);
            LayawayForfeitureResults.ForfeitCompleted += new EventHandler(LayawayForfeitureResults_ForfeitCompleted);
            LayawayForfeitureResults.ShowDialog(currentForm);
        }

        void LayawayForfeitureResults_CancelRequested(object sender, EventArgs e)
        {
            LayawayForfeitureResults.Dispose();
            this.Close();
        }

        void LayawayForfeitureResults_ForfeitCompleted(object sender, EventArgs e)
        {
            List<LayawayVO> layaways = LayawayForfeitureResults.GetSelectedLayaways();
            
            if (layaways.Count == 0)
                return;
            bool ineligibleFound = layaways.Any(d => !IsLayawayEligibleToForfeit(d.NextPayment));
            if (ineligibleFound)
            {
                MessageBox.Show("You can only complete Eligible layaways");
                return;
            }

            LayawayForfeitureResults.Dispose();
            CompleteForfeit(layaways);
            if (processedLayaways.Count > 0)
            {
                LayawayCreateReportObject lco = new LayawayCreateReportObject();
                //lco.GetLayawayForfeitPickingSlip(layaways);
                decimal restockingFee = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetLayawayRestockingFee(GlobalDataAccessor.Instance.CurrentSiteId);
                lco.GetForfeitedLayawaysListings(layaways, restockingFee);
                MessageBox.Show(this.processedLayaways.Count != layaways.Count
                                        ? "Some Layaways did not forfeit successfully" : "Layaways forfeited successfully");
            }
            else
            {
                MessageBox.Show("Errors during layaway forfeiture.");
            }
        }

        void detailViewer_ForfeitCompleted(object sender, EventArgs e)
        {
            List<LayawayVO> layaways = DetailViewer.Layaways;
            bool ineligibleFound = layaways.Any(d => !IsLayawayEligibleToForfeit(d.NextPayment));
            if (ineligibleFound)
            {
                MessageBox.Show("You can only complete Eligible layaways");
                return;
            }
            DetailViewer.Dispose();
            CompleteForfeit(layaways);
            if (processedLayaways.Count > 0)
            {
                LayawayCreateReportObject lco = new LayawayCreateReportObject();
                lco.GetLayawayForfeitPickingSlip(layaways);
                MessageBox.Show(this.processedLayaways.Count != layaways.Count
                                        ? "Some Layaways did not forfeit successfully" : "Layaways forfeited successfully");
            }
            else
            {
                MessageBox.Show("Errors during layaway forfeiture.");
            }
        }

        private void CompleteForfeit(List<LayawayVO> layaways)
        {
            string statusDate = ShopDateTime.Instance.ShopDate.FormatDate();
            string statusTime = ShopDateTime.Instance.ShopTransactionTime.ToString();

            string errorCode;
            string errorText;
            decimal restockingFee = 0.0m;
            foreach (LayawayVO layData in layaways)
            {
                List<LayawayVO> layawayToProcess = new List<LayawayVO>
                {
                        layData
                };
                if (!RetailProcedures.ProcessLayawayServices(
                    CDS,
                    layawayToProcess,
                    CDS.CurrentSiteId.StoreNumber,
                    statusDate,
                    statusTime,
                    ProductStatus.FORF,
                    out restockingFee,
                    out errorCode,
                    out errorText
                ))
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error in processing forfeit of layaway " + layData.TicketNumber);
                    //MessageBox.Show("An error occurred while forfeiting the layaways.");
                }
                else
                    processedLayaways.Add(layData);
            }

            this.Close();
        }

        private void LayawayForfeitureSearch_Shown(object sender, EventArgs e)
        {
            rdoLayawayNumber_CheckedChanged(this, EventArgs.Empty);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || (this.ActiveControl.Equals(btnCancel) && keyData == Keys.Enter))
            {
                this.btnCancel_Click(null, new EventArgs());
                return true;
            }

            if (keyData == Keys.Enter)
            {
                this.btnSubmit_Click(null, new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void rdoLayawayNumber_CheckedChanged(object sender, EventArgs e)
        {
            txtTransactionNumber.Enabled = rdoLayawayNumber.Checked;
            dpEligibilityDate.Enabled = rdoAllEligible.Checked;
        }

        # endregion

        # region Helper Methods
        private bool IsLayawayEligibleToForfeit(DateTime lastPaymentDate)
        {
            return LayawayProcedures.IsLayawayEligibleForForfeiture(lastPaymentDate, CDS.CurrentSiteId);
        }

        private LayawayVO LoadLayaway(int storeNumber, int ticketNumber)
        {
            string errorCode;
            string errorText;

            CustomerVO customerObj = null;
            LayawayVO layawayObj = null;

            bool retVal = RetailProcedures.GetLayawayData(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.OracleDA, storeNumber,
                        ticketNumber, "0", StateStatus.BLNK, "LAY", true, out layawayObj, out customerObj, out errorCode, out errorText);

            if (!retVal || layawayObj == null)
            {
                return null;
            }

            return layawayObj;
        }

        private List<LayawayVO> SearchForLayaways(int storeNumber, DateTime eligibilityDate)
        {
            string errorCode;
            string errorText;

            List<LayawayVO> layaways;

            bool retVal = RetailProcedures.SearchForLayaways(GlobalDataAccessor.Instance.OracleDA, storeNumber, eligibilityDate, out layaways, out errorCode, out errorText);

            if (!retVal || layaways == null)
            {
                return new List<LayawayVO>();
            }

            return layaways;
        }

        # endregion
    }
}
