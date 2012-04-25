/**************************************************************************************************************
* CashlinxDesktop
* CreateCustomer
* This form is used to lookup a ticket transaction using store number and ticket number
* Sreelatha Rengarajan 6/30/2009 Initial version
*
*   Fixes/Mods: 
*
*   PWN00000337 S.Murphy allow missing store number - will default to current store
****************************************************************************************************************/
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Services
{
    public partial class LookupTicket : Form
    {
        Form _ownerfrm;
        private int _storeNumber;
        private int _ticketNumber;
        public NavBox NavControlBox;
        ProcessingMessage _procMsg;
        bool _retValue = false;
        PawnLoan _pawnLoan;
        LayawayVO _layaway;
        PawnAppVO _pawnApplication;
        CustomerVO _custObject;
        const string idType = "0";
        string _errorCode;
        string _errorText;

        public LookupTicket()
        {
            InitializeComponent();
            NavControlBox = new NavBox();

            ddlProduct.SelectedIndex = 0;
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            errorLabel.Text = "";
            if (IsPawnLoadProductTypeSelected())
            {
                FindPawnTicket();
            }
            else
            {
                FindLayawayTicket();
            }
        }

        void bwLookupPawnTicket_DoWork(object sender, DoWorkEventArgs e)
        {
            _retValue = CustomerLoans.GetPawnLoan(GlobalDataAccessor.Instance.DesktopSession, _storeNumber, _ticketNumber, idType, StateStatus.BLNK, true, out _pawnLoan,
                                                  out _pawnApplication, out _custObject, out _errorCode, out _errorText);
        }

        void bwLookupLayawayTicket_DoWork(object sender, DoWorkEventArgs e)
        {
            _retValue = RetailProcedures.GetLayawayData(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.OracleDA, Convert.ToInt32(_storeNumber),
                                                        _ticketNumber, "0", StateStatus.BLNK, "LAY", true, out _layaway, out _custObject, out _errorCode, out _errorText);
        }

        void bwLookupPawnTicket_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _procMsg.Update();
            _procMsg.Close();
            _procMsg.Dispose();

            if (!_retValue)
            {
                MessageBox.Show(Commons.GetMessageString("InvalidTicketMessage"), "Prompt", MessageBoxButtons.OK);
                errorLabel.Text = Commons.GetMessageString("LookupTicketInvalidMessage");
                errorLabel.Visible = true;
                return;
            }

            if (_pawnApplication == null)
            {
                errorLabel.Text = Commons.GetMessageString("LookupTicketInvalidMessage");
                errorLabel.Visible = true;
                return;
            }

            if (_pawnApplication == null || _pawnLoan == null)
            {
                MessageBox.Show(Commons.GetMessageString("InvalidTicketMessage"), "Prompt", MessageBoxButtons.OK);
                errorLabel.Text = Commons.GetMessageString("LookupTicketInvalidMessage");
                errorLabel.Visible = true;
                return;
            }
            if (string.IsNullOrEmpty(_custObject.PartyId))
            {
                DialogResult dgr = MessageBox.Show(Commons.GetMessageString("LookupTicketNoCustomerMessage"), "Warning", MessageBoxButtons.RetryCancel);
                if (dgr != DialogResult.Retry)
                {
                    NavControlBox.Action = NavBox.NavAction.CANCEL;
                }
                return;
            }

            GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
            GlobalDataAccessor.Instance.DesktopSession.TicketLookedUp = _ticketNumber;
            GlobalDataAccessor.Instance.DesktopSession.TicketTypeLookedUp = ProductType.PAWN;
            GlobalDataAccessor.Instance.DesktopSession.PH_TicketLookedUp = _ticketNumber;
            GlobalDataAccessor.Instance.DesktopSession.PH_TicketTypeLookedUp = ProductType.PAWN;
            GlobalDataAccessor.Instance.DesktopSession.PH_TicketLookedUpActive = _pawnLoan.LoanStatus == ProductStatus.IP;
            GlobalDataAccessor.Instance.DesktopSession.PawnApplications.Add(_pawnApplication);
            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = _custObject;
            GlobalDataAccessor.Instance.DesktopSession.CurPawnAppId = _pawnApplication.PawnAppID.ToString();
            GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Add(_pawnLoan);
            this.NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
        }

        private void ShowInvalidTicketMessage()
        {
            MessageBox.Show(Commons.GetMessageString("InvalidTicketMessage"), "Prompt", MessageBoxButtons.OK);
            errorLabel.Text = Commons.GetMessageString("LookupTicketInvalidMessage");
            errorLabel.Visible = true;
        }

        void bwLookupLayawayTicket_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _procMsg.Update();
            _procMsg.Close();
            _procMsg.Dispose();

            SetButtonState(true);
            if (!_retValue)
            {
                ShowInvalidTicketMessage();
                return;
            }
            //TODO: Make this a rule!!! Also subtract from ShopDateTime.Instance.ShopDate, not datetime.now
            TimeSpan spanYears = new TimeSpan();
            spanYears = DateTime.Now.Subtract(_layaway.DateMade);
            if (spanYears.Days > 730)
            {
                ShowInvalidTicketMessage();
                return;
            }

            if (_layaway.LoanStatus != ProductStatus.ACT)
            {
                string msg = "This ticket number is inactive. Do you want to view the details for the ticket?";
                if (MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            if (string.IsNullOrEmpty(_custObject.PartyId) || _layaway == null)
            {
                DialogResult dgr = MessageBox.Show(Commons.GetMessageString("LookupTicketNoCustomerMessage"), "Warning", MessageBoxButtons.RetryCancel);
                if (dgr != DialogResult.Retry)
                {
                    NavControlBox.Action = NavBox.NavAction.CANCEL;
                }
                return;
            }

            GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
            GlobalDataAccessor.Instance.DesktopSession.TicketLookedUp = _ticketNumber;
            GlobalDataAccessor.Instance.DesktopSession.TicketTypeLookedUp = ProductType.LAYAWAY;
            GlobalDataAccessor.Instance.DesktopSession.PH_TicketLookedUp = _ticketNumber;
            GlobalDataAccessor.Instance.DesktopSession.PH_TicketTypeLookedUp = ProductType.LAYAWAY;
            GlobalDataAccessor.Instance.DesktopSession.PH_TicketLookedUpActive = _layaway.LoanStatus == ProductStatus.ACT;
            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = _custObject;
            GlobalDataAccessor.Instance.DesktopSession.Layaways.Add(_layaway);

            //BZ # 490
            //this.NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
            GlobalDataAccessor.Instance.DesktopSession.CustomerNotPledgor = false;
            GlobalDataAccessor.Instance.DesktopSession.PawnLoans = new List<PawnLoan>();
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = GlobalDataAccessor.Instance.DesktopSession.PH_TicketLookedUpActive ? "ViewPawnCustomerProductDetails" : "ViewPawnCustomerProductHistory";
            if (_layaway.LoanStatus != ProductStatus.ACT)
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            else
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            //BZ # 490 - end
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            //BZ # 700
            //this.textBoxStoreNumber.Text = CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber;
            this.textBoxStoreNumber.Focus();
            //BZ # 700 - end

            this.textBoxTicketNumber.Text = "";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void LookupTicket_Load(object sender, EventArgs e)
        {
            _ownerfrm = this.Owner;
            NavControlBox.Owner = this;
            ddlProduct.Enabled = !GlobalDataAccessor.Instance.DesktopSession.ServicePawnLoans;
            this.textBoxStoreNumber.Text = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
        }

        private void textBoxStoreNumber_Click(object sender, EventArgs e)
        {
            textBoxStoreNumber.Text = "";
            textBoxTicketNumber.Text = "";
        }

        private void textBoxStoreNumber_TextChanged(object sender, EventArgs e)
        {
            if (textBoxStoreNumber.Text != "")
                ScanParse(textBoxStoreNumber.Text);
        }

        private void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPawnLoadProductTypeSelected())
            {
                textBoxStoreNumber.Enabled = true;
            }
            else
            {
                textBoxStoreNumber.Enabled = false;
                this.textBoxStoreNumber.Text = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || (this.ActiveControl.Equals(this.customButtonCancel) && keyData == Keys.Enter))
            {
                this.buttonCancel_Click(null, new EventArgs());
                return true;
            }

            if (this.ActiveControl.Equals(this.customButtonClear) && keyData == Keys.Enter)
            {
                this.buttonClear_Click(null, new EventArgs());
                return true;
            }

            if (keyData == Keys.Enter)
            {
                this.buttonFind_Click(null, new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        # region Helper Methods

        private void FindLayawayTicket()
        {
            if (!(textBoxStoreNumber.isValid || string.IsNullOrEmpty(textBoxStoreNumber.Text)))
            {
                errorLabel.Text = Commons.GetMessageString("LookupTicketStoreNumberMsg");
            }
            if (!textBoxTicketNumber.isValid || textBoxTicketNumber.Text.Length < 3)
            {
                if (errorLabel.Text.Length > 0)
                    errorLabel.Text += System.Environment.NewLine + Commons.GetMessageString("LookupTicketTicketNumberMsg");
                else
                    errorLabel.Text = Commons.GetMessageString("LookupTicketTicketNumberMsg");
            }

            if (errorLabel.Text.Length > 0)
            {
                errorLabel.Visible = true;
                return;
            }

            errorLabel.Visible = false;
            SetDefaultStoreIfNeeded();
            _ticketNumber = Convert.ToInt32(this.textBoxTicketNumber.Text);

            try
            {
                _procMsg = new ProcessingMessage("Retrieving Ticket Data");
                SetButtonState(false);
                var bwLookupLayawayTicket = new BackgroundWorker();
                bwLookupLayawayTicket.DoWork += bwLookupLayawayTicket_DoWork;
                bwLookupLayawayTicket.RunWorkerCompleted += bwLookupLayawayTicket_RunWorkerCompleted;
                bwLookupLayawayTicket.RunWorkerAsync();
                _procMsg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Failed to get the ticket data for " + _ticketNumber.ToString(), ex);
                NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        private void SetButtonState(bool enable)
        {
            customButtonFind.Enabled = enable;
            customButtonCancel.Enabled = enable;
            customButtonClear.Enabled = enable;
        }

        private void FindPawnTicket()
        {
            //PWN00000337 S.Murphy allow missing store number - will default to current store
            if (!(textBoxStoreNumber.isValid || string.IsNullOrEmpty(textBoxStoreNumber.Text)))
            {
                errorLabel.Text = Commons.GetMessageString("LookupTicketStoreNumberMsg");
            }
            if (!textBoxTicketNumber.isValid || textBoxTicketNumber.Text.Length < 3)
            {
                if (errorLabel.Text.Length > 0)
                    errorLabel.Text += System.Environment.NewLine + Commons.GetMessageString("LookupTicketTicketNumberMsg");
                else
                    errorLabel.Text = Commons.GetMessageString("LookupTicketTicketNumberMsg");
            }
            if (errorLabel.Text.Length > 0)
            {
                errorLabel.Visible = true;
                return;
            }
            else
            {
                errorLabel.Visible = false;
                SetDefaultStoreIfNeeded();
                _ticketNumber = Convert.ToInt32(this.textBoxTicketNumber.Text);

                try
                {
                    _procMsg = new ProcessingMessage("Retrieving Ticket Data");
                    var bwLookupPawnTicket = new BackgroundWorker();
                    bwLookupPawnTicket.DoWork += bwLookupPawnTicket_DoWork;
                    bwLookupPawnTicket.RunWorkerCompleted += bwLookupPawnTicket_RunWorkerCompleted;
                    bwLookupPawnTicket.RunWorkerAsync();
                    _procMsg.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException("Failed to get the ticket data for " + _ticketNumber, ex);
                    NavControlBox.Action = NavBox.NavAction.CANCEL;
                }
            }
        }

        private bool IsPawnLoadProductTypeSelected()
        {
            return ddlProduct.SelectedItem.ToString().Equals("Pawn Loan", StringComparison.InvariantCultureIgnoreCase);
        }

        private void ScanParse(string sBarCodeData)
        {
            if (string.IsNullOrEmpty(sBarCodeData) || sBarCodeData.Length < 5)
                return;

            try
            {
                textBoxStoreNumber.Text = sBarCodeData.Substring(0, 5);
                if (sBarCodeData.Length == 5)
                {
                    textBoxTicketNumber.Focus();
                    //textBoxTicketNumber.SelectionStart = 0;//commented out by Drew
                    return;
                }
                textBoxTicketNumber.Text = sBarCodeData.Substring(5);
            }
            catch (Exception ex)
            {
                errorLabel.Text = Commons.GetMessageString("LookupTicketTicketNumberMsg");
                return;
            }
        }

        private void SetDefaultStoreIfNeeded()
        {
            if (string.IsNullOrEmpty(this.textBoxStoreNumber.Text))
            {
                _storeNumber = Convert.ToInt32(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber);
            }
            else
            {
                _storeNumber = Convert.ToInt32(this.textBoxStoreNumber.Text);
            }
        }

        # endregion
    }
}
