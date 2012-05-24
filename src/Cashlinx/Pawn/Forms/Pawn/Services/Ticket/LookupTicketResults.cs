/**************************************************************************************************************
* CashlinxDesktop
* CreateCustomer
* This form is used to show the results of a ticket lookup
* Sreelatha Rengarajan 7/9/2009 Initial version
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.Services.Ticket
{
    public partial class LookupTicketResults : Form
    {
        public NavBox NavControlBox;
        Form _ownerfrm;
        public LookupTicketResults()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
        }

        private void LookupTicketResults_Load(object sender, EventArgs e)
        {
            _ownerfrm = Owner;
            NavControlBox.Owner = this;
            //Show customer data using the customer object stored in session
            CustomerVO custdata = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            if (custdata != null)
            {
                customDataGridViewTicketResults.Rows.Add(1);
                customDataGridViewTicketResults.Rows[0].Cells["custlastname"].Value = custdata.LastName;
                customDataGridViewTicketResults.Rows[0].Cells["custfirstname"].Value = custdata.FirstName;
                customDataGridViewTicketResults.Rows[0].Cells["dob"].Value = custdata.DateOfBirth.FormatDate();
                AddressVO custAddr = custdata.getHomeAddress();
                if (custAddr != null)
                {
                    customDataGridViewTicketResults.Rows[0].Cells["address"].Value = custAddr.Address1 + " " +
                                                                          custAddr.UnitNum + " " + custAddr.City + "," + custAddr.State_Code + " " +
                                                                          custAddr.ZipCode;
                }
                IdentificationVO currentId = custdata.getIdentity(0);
                if (GlobalDataAccessor.Instance.DesktopSession.TicketTypeLookedUp == ProductType.LAYAWAY)
                {
                    customDataGridViewTicketResults.Rows[0].Cells["IDData"].Value = currentId.IdType + "-" +
                                                                         currentId.IdIssuer + "-" + currentId.IdValue;

                    //Get the layaway from session to get the ticket number
                    List<LayawayVO> layaway = GlobalDataAccessor.Instance.DesktopSession.Layaways;
                    var tktNumber = string.Empty;
                    if (layaway != null)
                    {
                        LayawayVO layawayObj = layaway.First();
                        if (layawayObj != null)
                            tktNumber = layawayObj.TicketNumber.ToString();

                    }
                    tktNumberLabel.Text = tktNumber;

                }
                else
                {
                    //Get ID data from the pawn app object
                    List<PawnAppVO> pawnApplications = GlobalDataAccessor.Instance.DesktopSession.PawnApplications;
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
                        PawnAppVO pawnApplication = pawnApplications.First
                            (papp => papp.PawnAppID == pawnAppId);
                        if (pawnApplication != null)
                            customDataGridViewTicketResults.Rows[0].Cells["IDData"].Value = pawnApplication.PawnAppCustIDType + "-" +
                                                                                 pawnApplication.PawnAppCustIDIssuer + "-" + pawnApplication.PawnAppCustIDNumber;

                        //Get the pawn loan from session to get the ticket number
                        //Only 1 pawn loan for this application id
                        List<PawnLoan> pawnLoans = GlobalDataAccessor.Instance.DesktopSession.PawnLoans;
                        var tktNumber = string.Empty;
                        if (pawnLoans != null)
                        {
                            PawnLoan pawnLoanObj = pawnLoans.First
                                (ploan => ploan.PawnAppId == pawnAppId.ToString());
                            if (pawnLoanObj != null)
                                tktNumber = pawnLoanObj.TicketNumber.ToString();

                        }
                        tktNumberLabel.Text = tktNumber;


                    }
                    else
                    {
                        BasicExceptionHandler.Instance.AddException("Pawn Application Data is not found in Lookup Ticket Results ", new ApplicationException());
                        NavControlBox.Action = NavBox.NavAction.CANCEL;
                    }
                }


            }
            else
            {
                BasicExceptionHandler.Instance.AddException("Customer object is missing in session ", new ApplicationException());
                //NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.BACK;
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            //SR 11/09/2009 Removed the check to see if customer is pledgor
            //and changed the logic so that the View button always shows the
            //read only version of view pawn customer information form
            /*if (radioButtonYes.Checked)
            {
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "ViewCustomerInformation";
                NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
            }
            if (radioButtonNo.Checked)
            {*/
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "ViewCustomerReadOnlyInformation";
                NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
            
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            if (radioButtonYes.Checked)
            {
                GlobalDataAccessor.Instance.DesktopSession.CustomerNotPledgor = false;
                GlobalDataAccessor.Instance.DesktopSession.PawnLoans = new List<PawnLoan>();
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = GlobalDataAccessor.Instance.DesktopSession.PH_TicketLookedUpActive ? "ViewPawnCustomerProductDetails" : "ViewPawnCustomerProductHistory";
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

            }
            if (radioButtonNo.Checked)
            {
                GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary=GlobalDataAccessor.Instance.DesktopSession.PawnLoans;
                GlobalDataAccessor.Instance.DesktopSession.PawnLoans = new List<PawnLoan>();
                GlobalDataAccessor.Instance.DesktopSession.CustomerNotPledgor = true;
                MessageBox.Show(Commons.GetMessageString("LookupTicketContinuePromptMsg"), "Prompt", MessageBoxButtons.OK);                
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "LookupCustomer";
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || (this.ActiveControl.Equals(this.customButtonBack) && keyData == Keys.Enter))
            {
                this.buttonBack_Click(null, new EventArgs());
                return true;
            }

            if (this.ActiveControl.Equals(this.customButtonView) && keyData == Keys.Enter)
            {
                this.buttonView_Click(null, new EventArgs());
                return true;
            }

            if (keyData == Keys.Enter)
            {
                this.buttonContinue_Click(null, new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
