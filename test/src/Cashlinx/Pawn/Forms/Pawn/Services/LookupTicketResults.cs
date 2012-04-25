/**************************************************************************************************************
* CashlinxDesktop
* CreateCustomer
* This form is used to show the results of a ticket lookup
* Sreelatha Rengarajan 7/9/2009 Initial version
**************************************************************************************************************/

using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using System;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.Services
{
    public partial class LookupTicketResults : Form
    {
        public NavBox NavControlBox;
        Form ownerfrm;
        public LookupTicketResults()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
        }

        private void LookupTicketResults_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            NavControlBox.Owner = this;
            //Show customer data using the customer object stored in session
            var custdata = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            if (custdata != null)
            {
                dataGridViewCustomer.Rows[0].Cells["custlastname"].Value = custdata.LastName;
                dataGridViewCustomer.Rows[0].Cells["custfirstname"].Value = custdata.FirstName;
                dataGridViewCustomer.Rows[0].Cells["dob"].Value = custdata.DateOfBirth.FormatDate();
                var custAddr = custdata.getHomeAddress();
                if (custAddr != null)
                {
                    dataGridViewCustomer.Rows[0].Cells["address"].Value = custAddr.Address1 + " " +
                        custAddr.UnitNum + " " + custAddr.City + "," + custAddr.State_Code + " " +
                        custAddr.ZipCode;
                }
                //Get ID data from the pawn app object
                var pawnApplications = GlobalDataAccessor.Instance.DesktopSession.PawnApplications;
                long pawnAppId;
                try
                {
                    pawnAppId = Convert.ToInt64(GlobalDataAccessor.Instance.DesktopSession.CurPawnAppId);
                }
                catch (Exception)
                {
                    pawnAppId = 0;
                }
                if (pawnAppId != 0 && pawnApplications != null)
                {
                    var pawnApplication = pawnApplications.First
                        (papp => papp.PawnAppID == pawnAppId);
                    if (pawnApplication != null)
                        dataGridViewCustomer.Rows[0].Cells["IDData"].Value = pawnApplication.PawnAppCustIDType + "-" +
                            pawnApplication.PawnAppCustIDIssuer + "-" + pawnApplication.PawnAppCustIDNumber;

                    //Get the pawn loan from session to get the ticket number
                    //Only 1 pawn loan for this application id
                    var pawnLoans = GlobalDataAccessor.Instance.DesktopSession.PawnLoans;
                    var tktNumber = string.Empty;
                    if (pawnLoans != null)
                    {
                        var pawnLoanObj = pawnLoans.First
                            (ploan => ploan.PawnAppId == pawnAppId.ToString());
                        if (pawnLoanObj != null)
                            tktNumber = pawnLoanObj.TicketNumber.ToString();

                    }
                    tktNumberLabel.Text = tktNumber;


                }
                else
                {
                    BasicExceptionHandler.Instance.AddException("Pawn Application Data is not found in Lookup Ticket Results ", new ApplicationException());
                    //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.CANCEL;
                    //CustomerController.NavigateUser(ownerfrm);
                    this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                }


            }
            else
            {
                BasicExceptionHandler.Instance.AddException("Customer object is missing in session ", new ApplicationException());
                //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.CANCEL;
                //CustomerController.NavigateUser(ownerfrm);
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            //CashlinxDesktop.Desktop.CashlinxDesktopSession.Instance.HistorySession.Back();
            this.NavControlBox.Action = NavBox.NavAction.BACK;
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            //CashlinxDesktopSession.Instance.Trigger = CustomerTriggerTypes.LOOKUPTICKET;
            //ViewCustomerInformation viewcustfrm = new ViewCustomerInformation();
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "ViewCustomerInformation";
            this.NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
            //this.Hide();
            //CashlinxDesktopSession.Instance.HistorySession.AddForm(viewcustfrm);
            //viewcustfrm.Show(ownerfrm);
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            if (radioButtonYes.Checked)
                MessageBox.Show("**BLOCK** - Show view pawn customer product details - NOT IN THIS RELEASE");
            if (radioButtonNo.Checked)
            {
                GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary = GlobalDataAccessor.Instance.DesktopSession.PawnLoans;
                GlobalDataAccessor.Instance.DesktopSession.PawnLoans = null;
                MessageBox.Show(Commons.GetMessageString("LookupTicketContinuePromptMsg"), "Prompt", MessageBoxButtons.OK);
                //Invoke lookup customer
                //LookupCustomer lkupCustomer = new LookupCustomer();
                //CashlinxDesktopSession.Instance.HistorySession.Back();
                //CashlinxDesktopSession.Instance.HistorySession.AddForm(lkupCustomer);
                //lkupCustomer.Show(ownerfrm);
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "LookupCustomer";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
        }
    }
}
