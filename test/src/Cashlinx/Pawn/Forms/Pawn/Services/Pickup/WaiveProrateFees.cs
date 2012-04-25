using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Loan;

namespace Pawn.Forms.Pawn.Services.Pickup
{
    public partial class WaiveProrateFees : CustomBaseForm
    {
        private int currIndex = 0;
        private List<PawnLoan> pawnloansToView;
        private PawnLoan pawnLoan;
        SiteId currentStoreSiteId;
        private int totalLoansToWaive = 0;
        private decimal minAmt=0.0M;
        private decimal maxAmt = 0.0M;

        public WaiveProrateFees()
        {
            InitializeComponent();

        }



        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            if (customButtonSubmit.Text == "Continue")
            {
                currIndex++;
                tableLayoutPanelAddlFees.Controls.Clear();
                tableLayoutPanelAddlFeeHeading.Visible = false;
                panelAdditionalFeesHeading.Visible = false;
                LoadData();

            }
            else
            {
                //If any of the loan fees were waived or prorated
                //and if manager override is needed, show the manager override form
                bool updateserviceloans = true;
                List<ManagerOverrideType> overrideTypes = new List<ManagerOverrideType>();
                List<ManagerOverrideTransactionType> transactionTypes = new List<ManagerOverrideTransactionType>();
                List<int> transactionsForServiceOverride = new List<int>();

                foreach (PawnLoan pl in pawnloansToView)
                {
                    List<Fee> loanFees = pl.OriginalFees;
                    Fee feedata = (from fee in loanFees
                                   where fee.Waived || fee.Prorated
                                   select fee).FirstOrDefault();
                    if (feedata.Waived || feedata.Prorated)
                    {
                        if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsManagerOverrideRequiredForProrateWaive(currentStoreSiteId))
                        {
                            transactionsForServiceOverride.Add(pl.TicketNumber);
                            if (feedata.Waived)
                                overrideTypes.Add(ManagerOverrideType.WV);
                            else
                                overrideTypes.Add(ManagerOverrideType.PRO);
                            if (pl.TempStatus == StateStatus.P)
                                transactionTypes.Add(ManagerOverrideTransactionType.PU);
                            else if (pl.TempStatus == StateStatus.RN)
                                transactionTypes.Add(ManagerOverrideTransactionType.RN);
                            else if (pl.TempStatus == StateStatus.PD)
                                transactionTypes.Add(ManagerOverrideTransactionType.PD);

                        }
                    }
                }
                if (transactionsForServiceOverride.Count > 0)
                {

                    ManageOverrides overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER)
                                                      {
                                                          MessageToShow =
                                                              Commons.GetMessageString("WaiveFeesOverrideMessage"),
                                                          ManagerOverrideTypes = overrideTypes,
                                                          ManagerOverrideTransactionTypes = transactionTypes,
                                                          TransactionNumbers = transactionsForServiceOverride

                                                      };
                    overrideFrm.ShowDialog();
                    if (!(overrideFrm.OverrideAllowed))
                    {
                        //If Manager override Failed
                        //Waive or prorate of the fees cannot be persisted
                        updateserviceloans = false;
                    }
                }

                //Do not update the service loans if manager override did not go through
                if (updateserviceloans)
                {
                    GlobalDataAccessor.Instance.DesktopSession.ServiceLoans = pawnloansToView;
                }
                this.Close();
            }
        }

        private void LoadData()
        {


            CustomLabel feeName = new CustomLabel();
            CustomLabel feeValue = new CustomLabel();
            if (currIndex >= 0)
            {
                customButtonSubmit.Text = (currIndex + 1 != totalLoansToWaive && totalLoansToWaive > 1) ? "Continue" : "Submit";

                pawnLoan = pawnloansToView[currIndex];
                customLabelPageNo.Text = (currIndex + 1).ToString() + " of " + totalLoansToWaive.ToString();
                labelLoanNumber.Text = pawnLoan.TicketNumber.ToString();
                customLabelLoanAmtValue.Text = string.Format("{0:C}", pawnLoan.Amount);
                

                currentStoreSiteId = new SiteId()
                                                {
                                                    Alias = GlobalDataAccessor.Instance.CurrentSiteId.Alias,
                                                    Company = GlobalDataAccessor.Instance.CurrentSiteId.Company,
                                                    Date = ShopDateTime.Instance.ShopDate,
                                                    LoanAmount = 0,
                                                    State = GlobalDataAccessor.Instance.CurrentSiteId.State,
                                                    StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                    TerminalId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId
                                                };

                try
                {

                    int currentRow = tableLayoutPanelFeeAmount.RowCount;
                    int currentAddlFeesRow = tableLayoutPanelAddlFees.RowCount;

                    decimal totalFees = 0.0M;
                    foreach (Fee fee in pawnLoan.OriginalFees)
                 
                    {
                        if (fee.FeeType == FeeTypes.INTEREST)
                        {
                            customLabelInterestValue.Text = string.Format("{0:0.00}", fee.Value);
                            continue;
                        }
                        if (fee.FeeType == FeeTypes.SERVICE)
                        {
                            if (fee.Value > 0)
                            {
                                customLabelSrvChargeValue.Text = string.Format("{0:0.00}", fee.Value);
                                customLabelServiceChargeHeading.Visible = true;
                                customLabelSrvChargeValue.Visible = true;

                            }
                            else
                            {
                                customLabelServiceChargeHeading.Visible = false;
                                customLabelSrvChargeValue.Visible = false;
                            }
                            continue;
                        }
                        string feeTypeName = Commons.GetFeeName(fee.FeeType);
                        
                        feeName = new CustomLabel();
                        feeValue = new CustomLabel();
                        feeName.Text = feeTypeName + ":";
                        feeValue.Text = string.Format("{0:0.00}", fee.Value);
                        feeName.Anchor = AnchorStyles.Right;
                        feeValue.Anchor = AnchorStyles.Left;
                        if (fee.CanBeProrated || fee.CanBeWaived)
                        {
                            tableLayoutPanelAddlFees.Controls.Add(feeName, 0, currentAddlFeesRow);
                            tableLayoutPanelAddlFees.Controls.Add(feeValue, 1, currentAddlFeesRow);
                            tableLayoutPanelAddlFeeHeading.Visible = true;
                            panelAdditionalFeesHeading.Visible = true;
                            tableLayoutPanelAddlFees.Visible = true;
                            if (fee.CanBeWaived && !fee.CanBeProrated)
                            {
                                CheckBox waiveFeeChkBox = new CheckBox();
                                waiveFeeChkBox.Name = fee.FeeType.ToString();
                                waiveFeeChkBox.Anchor = AnchorStyles.Left;
                                waiveFeeChkBox.CheckedChanged += new EventHandler(waiveFeeChkBox_CheckedChanged);
                                tableLayoutPanelAddlFees.Controls.Add(waiveFeeChkBox, 2, currentAddlFeesRow);
                            }
                            else if (fee.CanBeProrated && fee.CanBeWaived)
                            {
                                CustomLabel prorateHeading = new CustomLabel();
                                prorateHeading.Text = "Prorate";
                                tableLayoutPanelAddlFeeHeading.Controls.Add(prorateHeading, 2, 0);
                                CustomLabel orHeading = new CustomLabel();
                                orHeading.Text = " or ";
                                tableLayoutPanelAddlFeeHeading.Controls.Add(prorateHeading, 3, 0);
                                RadioButton prorateFeeRadButton = new RadioButton();
                                prorateFeeRadButton.Name = fee.FeeType.ToString();
                                prorateFeeRadButton.CheckedChanged += new EventHandler(prorateFeeRadButton_CheckedChanged);
                                tableLayoutPanelAddlFees.Controls.Add(prorateFeeRadButton, 2, currentAddlFeesRow);
                                RadioButton waiveFeeRadButton = new RadioButton();
                                waiveFeeRadButton.Name = fee.FeeType.ToString();
                                waiveFeeRadButton.CheckedChanged += new EventHandler(waiveFeeRadButton_CheckedChanged);
                                tableLayoutPanelAddlFees.Controls.Add(waiveFeeRadButton, 3, currentAddlFeesRow);
                            }
                            currentAddlFeesRow++;

                        }
                        else
                        {
                            panelAdditionalFeesHeading.Visible = false;
                            tableLayoutPanelFeeAmount.Controls.Add(feeName, 0, currentRow);
                            tableLayoutPanelFeeAmount.Controls.Add(feeValue, 1, currentRow);

                            currentRow++;
                        }
                        totalFees = totalFees + fee.Value;
                    }

                    decimal totalPickupAmt = pawnLoan.Amount + pawnLoan.InterestAmount + pawnLoan.ServiceCharge +
                                             totalFees;
                    pawnLoan.PickupAmount = totalPickupAmt;
                    customLabelTotalPickupAmt.Text = string.Format("{0:C}", totalPickupAmt);

                }


                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException(
                        "Error in getting Fee information - storeloans.getcurrentloanfees",
                        new ApplicationException(ex.Message));
                }




            }

        }

        private void waiveFeeChkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selectedCheckBox = (CheckBox)sender;
            PawnLoan pl = pawnloansToView[currIndex];

            string checkBoxName = selectedCheckBox.Name;
            Fee feedata;
            //get the fee from the pawnloan object
            int idx = pl.OriginalFees.FindIndex(loanfee => loanfee.FeeType.ToString() == checkBoxName);
            if (idx >= 0)
            {
                feedata = pl.OriginalFees[idx];
                if (selectedCheckBox.Checked)
                {
                    if (pl.TempStatus == StateStatus.PD)
                    {
                        if (!(checkPaydownAmount(pl.Amount, pl.OriginalFees, feedata.OriginalAmount, pl.PaydownAmount)))
                        {
                            MessageBox.Show(Commons.GetMessageString("RollOverWaiveFeeMessage"));
                            return;
                        }
                    }
                    feedata.Waived = true;
                    feedata.Value = 0;
                    pl.OriginalFees.RemoveAt(idx);
                    pl.OriginalFees.Insert(idx, feedata);
                    pl.PickupAmount -= feedata.OriginalAmount;


                }
                else
                {
                    feedata.Waived = false;
                    feedata.Value = feedata.OriginalAmount;
                    pl.OriginalFees.RemoveAt(idx);
                    pl.OriginalFees.Insert(idx, feedata);
                    pl.PickupAmount += feedata.Value;

                }
            }


            pawnloansToView.RemoveAt(currIndex);
            pawnloansToView.Insert(currIndex, pl);
            customLabelTotalPickupAmt.Text = string.Format("{0:C}", pl.PickupAmount);
        }

        private bool checkPaydownAmount(decimal loanAmount, List<Fee> rolloverFees, decimal feeWaiveAmount, decimal paydownAmount)
        {
            //Get the business rule value for the min loan amount in the state
            
            return true;
        }

        private void prorateFeeRadButton_CheckedChanged(object sender, EventArgs e)
        {
            //To Do - implement
            //Details not known
        }

        private void waiveFeeRadButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedButton = (RadioButton)sender;
            PawnLoan pl = pawnloansToView[currIndex];

            string radioButtonName = selectedButton.Name;
            Fee feedata;
            //get the fee from the pawnloan object
            int idx = pl.OriginalFees.FindIndex(loanfee => loanfee.FeeType.ToString() == radioButtonName);
            if (idx >= 0)
            {
                feedata = pl.Fees[idx];
                if (selectedButton.Checked)
                {
                    feedata.Waived = true;
                    feedata.Value = 0;
                    pl.OriginalFees.RemoveAt(idx);
                    pl.OriginalFees.Insert(idx, feedata);
                    pl.PickupAmount -= feedata.Value;

                }
                else
                {
                    feedata.Waived = false;
                    feedata.Value = feedata.OriginalAmount;
                    pl.OriginalFees.RemoveAt(idx);
                    pl.OriginalFees.Insert(idx, feedata);
                    pl.PickupAmount += feedata.Value;

                }
            }


            pawnloansToView.RemoveAt(currIndex);
            pawnloansToView.Insert(currIndex, pl);
            customLabelTotalPickupAmt.Text = string.Format("{0:C}", pl.PickupAmount);

        }



        private void WaiveProrateFees_Load(object sender, EventArgs e)
        {
            pawnloansToView = Utilities.CloneObject(GlobalDataAccessor.Instance.DesktopSession.ServiceLoans);
            totalLoansToWaive = pawnloansToView.Count;

            new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMinMaxLoanAmounts(
                GlobalDataAccessor.Instance.CurrentSiteId, out minAmt, out maxAmt);
            LoadData();
        }

    }
}
