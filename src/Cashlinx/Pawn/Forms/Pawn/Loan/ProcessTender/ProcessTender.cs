using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Logger;
using Pawn.Forms.Pawn.Tender;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Loan.ProcessTender
{
    public partial class ProcessTender : Form
    {
        private bool success;
        private int selectedItem;
        private bool validSelection;
        private ProcessTenderProcedures.ProcessTenderMode procTendMode;

        /// <summary>
        /// 
        /// </summary>
        public ProcessTender(ProcessTenderProcedures.ProcessTenderMode pMode)
        {
            InitializeComponent();
            this.success = false;
            this.customerNameValueLabel.Text = string.Empty;
            this.disburseLabel.Text = "$ 0.00";
            this.okButton.Enabled = false;
            this.selectedItem = -1;
            this.validSelection = false;
            this.viewButton.Enabled = false;
            this.procTendMode = pMode;

            this.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="amount"></param>
        private void setProcessTenderDetails(string firstName, string lastName, string amount)
        {
            if (firstName.Contains("Topscustomer"))
            {
                this.customerNameValueLabel.Visible = false;
                customerNameLabel.Visible = false;
            }
            else
            {
                this.customerNameValueLabel.Visible = true;
                customerNameLabel.Visible = true;
                this.customerNameValueLabel.Text = firstName + " " + lastName;
            }
            this.disburseLabel.Text = amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            //CashlinxDesktopSession.Instance.ClearPawnLoan();
            if (this.procTendMode != ProcessTenderProcedures.ProcessTenderMode.VENDORPURCHASE)
            {
                if (this.procTendMode != ProcessTenderProcedures.ProcessTenderMode.SERVICELOAN)
                {
                    if ((this.procTendMode == ProcessTenderProcedures.ProcessTenderMode.NEWLOAN) &&
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.ActiveLoanCount > 0)
                    {
                        MessageBox.Show(Common.Libraries.Utility.Shared.Commons.GetMessageString("SameDayLoanPrompt"));

                    }
                    if (!String.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber))
                    {
                        DialogResult dR = MessageBox.Show("Would you like to continue servicing this customer?",
                                                          "Process Tender Message", MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question);
                        if (dR == DialogResult.No)
                        {
                            GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
                        }
                        else
                        {
                            //Reload customer data
                            CustomerVO activeCustomer = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber);
                            if (activeCustomer != null)
                            {
                                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = activeCustomer;
                            }
                        }
                    }
                }
                if (this.procTendMode == ProcessTenderProcedures.ProcessTenderMode.SERVICELOAN && ProcessTenderController.Instance.ProcessedTickets.Count > 0)
                {
                    for (int cnt = 0;
                           cnt < ProcessTenderController.Instance.ProcessedTickets.Count;
                      cnt++)
                    {
                        var ticketData = ProcessTenderController.Instance.ProcessedTickets[cnt];
                        string s = ticketData.Right.ToString();
                        try
                        {
                            if (File.Exists(s))
                                File.Delete(s);
                        }
                        catch (IOException iex)
                        {
                            MessageBox.Show(@"File is still open", "File Open Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();

            NewDesktop nDesk = (NewDesktop)GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
            nDesk.handleEndFlow(null);

            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessTender_Load(object sender, EventArgs e)
        {
            ProcessTenderController pCntrl = ProcessTenderController.Instance;
            //Perform the process tender process
            this.loanTicketGroup.Visible = true;
            this.success = pCntrl.ExecuteProcessTender(procTendMode);
            this.Update();
            switch (this.procTendMode)
            {
                case ProcessTenderProcedures.ProcessTenderMode.NEWLOAN:
                    if (!this.success ||
                        CollectionUtilities.isEmpty(pCntrl.TicketFiles))
                    {
                        MessageBox.Show(
                            "New loan process tender failed.  \n" +
                            "Please call Shop System Support.  \n" +
                            "The loan number is: " + ProcessTenderController.Instance.TicketNumber,
                            "Application Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                        GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                        NewDesktop nDesk =
                        (NewDesktop)GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        nDesk.handleEndFlow(null);
                    }
                    else
                    {
                        this.okButton.Enabled = true;
                        //Update the labels
                        this.setProcessTenderDetails(
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName,
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName,
                            String.Format("{0:C}", GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Amount));

                        //Update the list box
                        for (int cnt = 1;
                             cnt <= ProcessTenderController.Instance.TicketFiles.Count;
                        ++cnt)
                        {
                            string s = ProcessTenderController.Instance.TicketFiles[cnt - 1];
                            if (string.IsNullOrEmpty(s))
                                continue;
                            string ticketDesc = "Loan #" +
                                                ProcessTenderController.Instance.TicketNumber + "[" +
                                                cnt + "]";
                            this.loanDocListView.Items.Add(new ListViewItem(ticketDesc, 0));
                        }
                        this.loanDocListView.Update();
                        this.Update();
                    }
                    break;
                case ProcessTenderProcedures.ProcessTenderMode.SERVICELOAN:
  
                    if (!this.success)
                    {
                        MessageBox.Show(
                            "Servicing loan process failed.  \n" +
                            "Please call Shop System Support.  \n",
                            "Application Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                        GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                        NewDesktop nDesk =
                            (NewDesktop)GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        nDesk.handleEndFlow(null);
                    }
                    else
                    {
                        if (GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount > 0)
                            this.initialProcessTenderLabel.Text = "Received From Customer:";
                        else
                            this.initialProcessTenderLabel.Text = "Cash to Customer:";
                        this.okButton.Enabled = true;
                        this.setProcessTenderDetails(
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName,
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName,
                            String.Format("{0:C}", GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount));
                        for (int cnt = 0;
                             cnt < ProcessTenderController.Instance.ProcessedTickets.Count;
                        cnt++)
                        {
                            var ticketData = ProcessTenderController.Instance.ProcessedTickets[cnt];
                            string s = ticketData.Right.ToString();
                            if (string.IsNullOrEmpty(s))
                                continue;
                            string ticketDesc = "Loan #" +
                                                ticketData.Left.ToString() + "[" +
                                                cnt + 1 + "]";
                            this.loanDocListView.Items.Add(new ListViewItem(ticketDesc, 0));
                        }
                        this.loanDocListView.Update();
                        this.Update();                            
                        //this.loanTicketGroup.Visible = false;
                        //this.loanDocListView.Visible = false;
                        //this.Update();
                    }
                    break;
                case ProcessTenderProcedures.ProcessTenderMode.VOIDLOAN:
                    //Shouldn't be here for void loan
                    break;
                case ProcessTenderProcedures.ProcessTenderMode.PURCHASE:
                    if (!this.success)
                    {
                        MessageBox.Show(
                            "Purchase process failed.  \n" +
                            "Please call Shop System Support.  \n",
                            "Application Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                        GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                        NewDesktop nDesk =
                            (NewDesktop)GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        nDesk.handleEndFlow(null);
                    }
                    else
                    {
                        this.okButton.Enabled = true;
                        this.setProcessTenderDetails(
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName,
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName,
                            String.Format("{0:C}", GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Amount));
                        string ticketDesc = "Purchase #" +
                                            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.TicketNumber;
                        this.loanDocListView.Items.Add(new ListViewItem(ticketDesc, 0));
                        this.Update();
                    }
                    break;
                case ProcessTenderProcedures.ProcessTenderMode.VENDORPURCHASE:
                    if (!this.success)
                    {
                        MessageBox.Show(
                            "Purchase process failed.  \n" +
                            "Please call Shop System Support.  \n",
                            "Application Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                        GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                        NewDesktop nDesk =
                            (NewDesktop)GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        nDesk.handleEndFlow(null);
                    }
                    else
                    {
                        this.okButton.Enabled = true;
                        this.setProcessTenderDetails(
                            GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name,
                            "",
                            String.Format("{0:C}", GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Amount));
                        string ticketDesc = "Vendor Purchase #" +
                                            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.TicketNumber;
                        this.loanDocListView.Items.Add(new ListViewItem(ticketDesc, 0));
                        this.Update();
                    }
                    break;

                case ProcessTenderProcedures.ProcessTenderMode.RETURNBUY:
                    if (!this.success)
                    {
                        MessageBox.Show(
                            "Purchase process failed.  \n" +
                            "Please call Shop System Support.  \n",
                            "Application Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                        GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                        NewDesktop nDesk =
                            (NewDesktop)GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        nDesk.handleEndFlow(null);
                    }
                    else
                    {
                        initialProcessTenderLabel.Text = "Received From Customer:";

                        this.okButton.Enabled = true;
                        this.setProcessTenderDetails(
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName,
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName,
                            String.Format("{0:C}", (from item in GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items
                                                    select item.ItemAmount).Sum()));
                        string ticketDesc = "Return #" +
                                            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.TicketNumber;
                        this.loanDocListView.Items.Add(new ListViewItem(ticketDesc, 0));
                        this.Update();
                    }
                    break;
                case ProcessTenderProcedures.ProcessTenderMode.RETURNSALE:
                    if (!this.success)
                    {
                        MessageBox.Show(
                            "Refund process failed.  \n" +
                            "Please call Shop System Support.  \n",
                            "Application Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                        GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                        NewDesktop nDesk =
                            (NewDesktop)GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        nDesk.handleEndFlow(null);
                    }
                    else
                    {
                        this.okButton.Enabled = true;
                        this.setProcessTenderDetails(
                            string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CustomerNumber) ?
                                GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.EntityName :
                                GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.CustomerNumber == "0" ?
                                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerName :
                                GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.EntityName,
                            "",
                            String.Format("{0:C}", GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TotalSaleAmount));
                        string ticketDesc = "Refund Ticket #" +
                                            GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TicketNumber;
                        this.loanDocListView.Items.Add(new ListViewItem(ticketDesc, 0));
                        this.Update();
                    }
                    break;
                case ProcessTenderProcedures.ProcessTenderMode.LAYAWAY:
                    if (!this.success)
                    {
                        MessageBox.Show(
                            "Layaway process failed.  \n" +
                            "Please call Shop System Support.  \n",
                            "Application Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                        GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                        NewDesktop nDesk =
                            (NewDesktop)GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        nDesk.handleEndFlow(null);
                    }
                    else
                    {
                        initialProcessTenderLabel.Text = "Received From Customer:";

                        this.okButton.Enabled = true;

                        this.setProcessTenderDetails(
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName,
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName,
                            String.Format("{0:C}", GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.TotalSaleAmount));
                        string ticketDesc = "Layaway Ticket #" +
                                            GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway.TicketNumber;
                        this.loanDocListView.Items.Add(new ListViewItem(ticketDesc, 0));
                        this.Update();
                    }
                    break;
                case ProcessTenderProcedures.ProcessTenderMode.LAYPAYMENT:
                    if (!this.success)
                    {
                        MessageBox.Show(
                            "Layaway payment process failed.  \n" +
                            "Please call Shop System Support.  \n",
                            "Application Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                        GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                        NewDesktop nDesk =
                            (NewDesktop)GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        nDesk.handleEndFlow(null);
                    }
                    else
                    {
                        initialProcessTenderLabel.Text = "Received From Customer:";

                        this.okButton.Enabled = true;
                        this.setProcessTenderDetails(
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName,
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName,
                            String.Format("{0:C}", GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount));
                        this.loanTicketGroup.Visible = false;
                        this.Update();
                        ReceiptConfirmation rcptConf = new ReceiptConfirmation();
                        rcptConf.ShowDialog();
                        this.Close();
                    }
                    break;
                case ProcessTenderProcedures.ProcessTenderMode.LAYPAYMENTREF:
                    if (!this.success)
                    {
                        MessageBox.Show(
                            "Layaway payment refund process failed.  \n" +
                            "Please call Shop System Support.  \n",
                            "Application Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                        GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                        NewDesktop nDesk =
                            (NewDesktop)GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        nDesk.handleEndFlow(null);
                    }
                    else
                    {
                        decimal totalAmt = GlobalDataAccessor.Instance.DesktopSession.TenderAmounts.Sum(s => Utilities.GetDecimalValue(s, 0));

                        this.okButton.Enabled = true;
                        this.setProcessTenderDetails(
                            CashlinxDesktopSession.Instance.ActiveCustomer.FirstName,
                            CashlinxDesktopSession.Instance.ActiveCustomer.LastName,
                            String.Format("{0:C}", totalAmt));
                        this.loanTicketGroup.Visible = false;
                        this.Update();

                        DisbursementConfirmation rcptConf = new DisbursementConfirmation();
                        rcptConf.ShowDialog();
                        this.Close();
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException(this.procTendMode.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewButton_Click(object sender, EventArgs e)
        {
            if (this.loanDocListView.SelectedIndices.Count > 0)
            {
                //this.selectedItem = this.loanDocListView.SelectedIndices[0];
                if (this.selectedItem >= 0)
                {
                    this.validSelection = true;
                }
                else
                {
                    this.validSelection = false;
                }
            }

            if (this.validSelection && ProcessTenderController.Instance.TicketFiles.Count > 0)
            {
                //this.validSelection = false;
                FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Viewing ticket file");
                string fileName = ProcessTenderController.Instance.TicketFiles[this.selectedItem];
                FileInfo inFileName = new FileInfo(fileName);
                FileInfo readerPath = new FileInfo(SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.AdobeReaderPath);
                if (readerPath.Exists)
                {
                    if (inFileName.Exists && this.viewButton.Enabled == true)
                    {
                        this.viewButton.Enabled = false;
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "- FileName = " + fileName + ", FullFileName = " + inFileName.FullName);
                        Process procHandle = new Process();

                        procHandle.StartInfo.FileName = readerPath.FullName;
                        procHandle.StartInfo.CreateNoWindow = false;
                        procHandle.StartInfo.Arguments = "\"" + inFileName.FullName + "\"";
                        procHandle.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procHandle.Start();
                        procHandle.WaitForExit();
                        procHandle.Dispose();
                        this.viewButton.Enabled = true;
                    }
                    else
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot find ticket file: " + fileName);
                        MessageBox.Show("Cannot find ticket file to display", "Application Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    FileLogger.Instance.logMessage(LogLevel.WARN, this, "Cannot find Adobe Reader");
                    MessageBox.Show("Cannot find Adobe Reader to view ticket", "Application Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                this.viewButton.Update();
            }
            else if (ProcessTenderController.Instance.TicketFiles.Count > 0)
            {
                FileLogger.Instance.logMessage(LogLevel.WARN, this, "No File selected to view");
                MessageBox.Show("Please select file to print", "Application Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "No files generated.");
                MessageBox.Show("No ticket file generated to display", "Application Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loanDocListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.validSelection = false;
            if (this.loanDocListView.SelectedIndices.Count > 0)
            {
                this.selectedItem = this.loanDocListView.SelectedIndices[0];
                if (this.selectedItem >= 0)
                {
                    this.validSelection = true;
                }
                else
                {
                    this.validSelection = false;
                }
            }
            else
            {
                this.selectedItem = -1;
                this.validSelection = false;
            }

            if (this.validSelection)
            {
                this.viewButton.Enabled = true;
            }
            else
            {
                this.viewButton.Enabled = false;
            }
            this.viewButton.Update();
        }

        private void loanDocListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            /*this.selectedItem = -1;
            this.viewButton.Enabled = false;
            this.validSelection = false;
            //this.viewButton.Update();*/
        }

        private void loanDocListView_Leave(object sender, EventArgs e)
        {
            /*this.selectedItem = -1;
            this.validSelection = false;
            this.viewButton.Enabled = false;
            this.Update();*/
        }

        private void loanDocListView_Click(object sender, EventArgs e)
        {
        }
    }
}
