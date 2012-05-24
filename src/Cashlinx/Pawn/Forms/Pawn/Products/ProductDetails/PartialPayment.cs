using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic.DesktopProcedures;
using Reports;

namespace Pawn.Forms.Pawn.Products.ProductDetails
{
    public partial class PartialPayment : CustomBaseForm
    {
        private PawnLoan pawnLoan;
        private DateTime previousPartialPaymentDate;
        private decimal currentPrincipal;
        private decimal interestAmt;
        private decimal storageFee;
        private decimal refundAmt;
        private decimal lateFeeFin;
        private int partialPaymentDaysToPay;
        private decimal lateInterest;
        private decimal lateService;
        private int extensionsPaid;
        private decimal originalInterestAmount;
        private decimal originalServiceAmount;
        private decimal extensionInterestAmount;
        private decimal extensionServiceAmount;
        

        public PartialPayment()
        {
            InitializeComponent();
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            decimal principalAmt = Utilities.GetDecimalValue(customTextBoxPrincipal.Text, 0);
                decimal subTotal = Math.Round(principalAmt + interestAmt + storageFee + lateFeeFin - refundAmt,2);
            if (customButtonSubmit.Text == "Calculate")
            {
                if (string.IsNullOrEmpty(customTextBoxPrincipal.Text))
                {
                    MessageBox.Show("Please enter a principal reduction amount");
                    return;
                }
                if (principalAmt >= currentPrincipal)
                {
                    MessageBox.Show("Principal reduction cannot be more or equal to the current principal");
                    return;
                }
                //CHeck that the amount entered satisfies the minimum that is allowed
                decimal minPartialPmt = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetPartialPaymentMinAmount(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);
                
                if (principalAmt < minPartialPmt)
                {
                    MessageBox.Show("Payment minimum amount of $" + minPartialPmt + " has not been met. Please enter sufficient amount");
                    return;
                }
                decimal newAmount = currentPrincipal - principalAmt;
                newPrincipalAmount.Text = newAmount.ToString("f2");
                totalDueAmount.Text = String.Format("{0:C}", subTotal);
                customButtonSubmit.Text = "Submit";
                customTextBoxPrincipal.Enabled = false;

            }
            else
            {
                this.DialogResult = DialogResult.OK;
                SiteId siteId = Utilities.CloneObject(GlobalDataAccessor.Instance.CurrentSiteId);
                siteId.Date = ShopDateTime.Instance.ShopDate;

                siteId.LoanAmount = currentPrincipal - principalAmt;
                var newPawnLoan = Utilities.CloneObject(pawnLoan);
                siteId.Date = ShopDateTime.Instance.ShopDate;
                newPawnLoan.Amount = currentPrincipal - principalAmt;
                decimal lateFeeAmount=lateFeeFin-refundAmt;
                UnderwritePawnLoanVO uwVO;
                newPawnLoan=ServiceLoanProcedures.GetLoanFees(GlobalDataAccessor.Instance.CurrentSiteId,
                    ServiceTypes.PARTIALPAYMENT,
                    0, 0, lateFeeAmount, 0,
                    newPawnLoan,
                    out uwVO);
                decimal totalIntAmt = Math.Round(interestAmt + lateInterest - extensionInterestAmount, 2);
                //decimal totalServAmt = Math.Round(storageFee + lateService - (originalServiceAmount * extensionsPaid), 2);
                decimal totalServAmt = Math.Round(subTotal - (principalAmt + totalIntAmt), 2);
                decimal storageFeeAllowed = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetStorageFee(GlobalDataAccessor.Instance.CurrentSiteId);
                Common.Libraries.Objects.Pawn.PartialPayment pPmnt = new Common.Libraries.Objects.Pawn.PartialPayment();
                pPmnt.PMT_AMOUNT = subTotal;
                pPmnt.PMT_INT_AMT = totalIntAmt;
                pPmnt.PMT_PRIN_AMT = principalAmt;
                pPmnt.PMT_SERV_AMT = totalServAmt;
                pPmnt.CUR_AMOUNT = currentPrincipal - principalAmt;
                pPmnt.CUR_FIN_CHG = uwVO.totalFinanceCharge;
                pPmnt.Cur_Int_Pct = Math.Round(uwVO.APR,2);
                if (storageFeeAllowed != 0)
                {
                    pPmnt.Cur_Srv_Chg = (from newFee in newPawnLoan.Fees
                                         where newFee.FeeType == FeeTypes.STORAGE
                                         select newFee).FirstOrDefault().Value;
                }
                else
                {
                    pPmnt.Cur_Srv_Chg=(pPmnt.CUR_AMOUNT * uwVO.feeDictionary["CL_PWN_0010_SVCCHRGRATE"])/100;
                }
                pPmnt.Cur_Term_Fin = Utilities.GetIntegerValue(Math.Floor(100 * uwVO.totalFinanceCharge));
                pPmnt.Status_cde = "New";
                pawnLoan.PartialPayments.Add(pPmnt);
                pawnLoan.Fees.Clear();
                

                if (totalIntAmt != 0)
                {
                    Fee interestAmtFee = new Fee()
                    {
                        FeeType = FeeTypes.INTEREST,
                        Value = totalIntAmt,
                        OriginalAmount = totalIntAmt,
                        FeeState = FeeStates.ASSESSED,
                        FeeDate = ShopDateTime.Instance.ShopDate,
                        CanBeProrated = false,
                        CanBeWaived = false
                    };
                    pawnLoan.Fees.Add(interestAmtFee);
                }
                if (totalServAmt != 0)
                {
                    
                    Fee storageAmtFee = new Fee()
                    {
                        FeeType = storageFeeAllowed==0?FeeTypes.SERVICE:FeeTypes.STORAGE,
                        Value = totalServAmt,
                        OriginalAmount = totalServAmt,
                        FeeState = FeeStates.ASSESSED,
                        FeeDate = ShopDateTime.Instance.ShopDate,
                        CanBeProrated = false,
                        CanBeWaived = false
                    };
                    pawnLoan.Fees.Add(storageAmtFee);
                }
            

                
                
     
                this.Close();
            }
        }

        private void PartialPayment_Load(object sender, EventArgs e)
        {
            pawnLoan = GlobalDataAccessor.Instance.DesktopSession.PartialPaymentLoans[0];
            
            if (pawnLoan != null)
            {
                extensionServiceAmount = 0;
                extensionInterestAmount = 0;

                originalInterestAmount = (from f in pawnLoan.Fees
                                          where f.FeeType == FeeTypes.INTEREST
                                          select f.Value).Sum();
                originalServiceAmount=(from f in pawnLoan.Fees
                                          where (f.FeeType == FeeTypes.SERVICE || f.FeeType == FeeTypes.STORAGE)
                                          select f.Value).Sum();
                currentPrincipal = pawnLoan.CurrentPrincipalAmount;
                refundAmt = 0;
                extensionsPaid = 0;
                PartialPaymentProcedures.GetExtensionAmountSplit(pawnLoan, out refundAmt, out extensionsPaid, out extensionInterestAmount, out extensionServiceAmount);
                 int daysLate=0;
   
                //There is no charge when partial payment is made on loan made date
                if (ShopDateTime.Instance.ShopDate == pawnLoan.DateMade)
                {
                    originalInterestAmount = 0;
                    storageFee = 0;
                    lateFeeFin = 0;
                    refundAmt = 0;
                    interestAmount.Text = "0";
                    serviceChargeAmount.Text = "0";
                    latefeeAmount.Text = "0";
                    refundAmount.Text = "0";

                }
                else
                {
                    if (pawnLoan.PartialPayments != null && pawnLoan.PartialPayments.Count > 0)
                    {
                        Common.Libraries.Objects.Pawn.PartialPayment pmt=pawnLoan.PartialPayments.OrderByDescending(pp => pp.Time_Made).FirstOrDefault(); 
                        previousPartialPaymentDate = pawnLoan.PartialPayments.OrderByDescending(pp => pp.Time_Made).FirstOrDefault().Date_Made;
                        int days_late=0;
                        PartialPaymentProcedures.GetPartialPaymentDaysToPay(previousPartialPaymentDate,
                            ShopDateTime.Instance.ShopDate, pawnLoan.DateMade,
                            pawnLoan.DueDate, pawnLoan.PfiEligible, pawnLoan.PfiNote,
                            out partialPaymentDaysToPay);
                        PartialPaymentProcedures.GetPartialPaymentDaysToPay(pawnLoan.DueDate,
                            ShopDateTime.Instance.ShopDate, pawnLoan.DateMade,
                            pawnLoan.DueDate, pawnLoan.PfiEligible, pawnLoan.PfiNote,
                            out days_late);

                        int daysToCharge = partialPaymentDaysToPay >= days_late && days_late > 0 ? partialPaymentDaysToPay - days_late : partialPaymentDaysToPay;
                        interestAmt = Math.Round(((pmt.CUR_AMOUNT * 5 / 100) / 30) * (daysToCharge), 2);
                        storageFee = Math.Round(pmt.Cur_Srv_Chg / 30 * daysToCharge, 2);
                        interestAmount.Text = interestAmt.ToString("f2");
                        serviceChargeAmount.Text = storageFee.ToString("f2");
                        bool daysLateGreater = days_late > partialPaymentDaysToPay;
                        if (!daysLateGreater && days_late > 0)
                        {
                            lateInterest = Math.Round(((pmt.CUR_AMOUNT * 5 / 100) / 30) * (days_late), 2);
                            //lateInterest = pawnLoan.OtherTranLateFinAmount;
                            //lateService = pawnLoan.OtherTranLateServAmount;
                            lateService = Math.Round(pmt.Cur_Srv_Chg / 30 * days_late, 2);
                            lateFeeFin = lateInterest +lateService;
                        }
                        latefeeAmount.Text = lateFeeFin.ToString("f2");
                        refundAmount.Text = refundAmt.ToString("f2");


                    }
                    else
                    {
                        PartialPaymentProcedures.GetPartialPaymentDaysToPay(pawnLoan.DateMade,
                            ShopDateTime.Instance.ShopDate, pawnLoan.DateMade,
                            pawnLoan.DueDate, pawnLoan.PfiEligible, pawnLoan.PfiNote,
                            out partialPaymentDaysToPay);
 
                daysLate = (ShopDateTime.Instance.ShopDate - pawnLoan.DueDate).Days;
                //If the payment date is within grace period
                //customer need not pay anything
                int graceDays = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetGracePeriod(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);
                if (daysLate < 0 || daysLate <= graceDays)
                    daysLate = 0;

                        int monthsToCharge=0;
                        int daysToCharge=0;
                        if (daysLate > 0)
                        {
                            monthsToCharge = partialPaymentDaysToPay / 30;
                            daysToCharge = partialPaymentDaysToPay - (monthsToCharge *30);
                        }
                        else
                            daysToCharge = partialPaymentDaysToPay;
                        if (daysLate > 0)
                        {
                            lateInterest = Math.Round(((originalInterestAmount / 30) * daysToCharge), 2);
                            lateService = Math.Round(((originalServiceAmount / 30) * daysToCharge), 2);
                            lateFeeFin = lateInterest + lateService;
                            interestAmt = Math.Round((originalInterestAmount * monthsToCharge));
                            interestAmount.Text = interestAmt.ToString("f2");
                            storageFee = Math.Round((originalServiceAmount * monthsToCharge));
                            serviceChargeAmount.Text = storageFee.ToString("f2");
                        }
                        else
                        {
                            interestAmt = Math.Round(originalInterestAmount / 30 * daysToCharge, 2);
                            interestAmount.Text = interestAmt.ToString("f2");
                            storageFee = Math.Round(originalServiceAmount / 30 * daysToCharge, 2);
                            serviceChargeAmount.Text = storageFee.ToString("f2");
                            lateInterest = 0;
                            lateService = 0;
                            lateFeeFin = 0;

                        }

                        latefeeAmount.Text = lateFeeFin.ToString("f2");
                        refundAmount.Text = refundAmt.ToString("f2");
                    }
                }
            }
            currentPrincipalAmount.Text = currentPrincipal.ToString("f2");
            totalDueAmount.Text = string.Empty;
            newPrincipalAmount.Text = string.Empty;
            
        }

        private void customButtonReset_Click(object sender, EventArgs e)
        {
            customTextBoxPrincipal.Text = string.Empty;
            customTextBoxPrincipal.Enabled = true;
            totalDueAmount.Text = string.Empty;
            newPrincipalAmount.Text = string.Empty;
            customButtonSubmit.Text = "Calculate";
        }


    }
}
