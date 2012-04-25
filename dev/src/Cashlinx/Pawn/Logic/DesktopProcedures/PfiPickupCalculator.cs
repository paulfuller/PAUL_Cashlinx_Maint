using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Objects;
using Common.Libraries.Utility;

namespace Pawn.Logic.DesktopProcedures
{
    public class PfiPickupCalculator
    {
        public PfiPickupCalculator(PawnLoan pawnLoan, SiteId currentSiteId, DateTime pickupDate)
        {
            ApplicableFees = new List<Fee>();
            PawnLoan = pawnLoan;
            PickupDate = pickupDate;
            CurrentSiteId = currentSiteId;
        }

        public List<Fee> ApplicableFees { get; private set; }
        public SiteId CurrentSiteId { get; private set; }
        public int DaysToPay { get; private set; }
        public int DaysAlreadyPaid { get; private set; }
        public bool HasPartialPayment { get; private set; }
        public PartialPayment LastPartialPayment { get; private set; }
        public int MonthsToPay { get; private set; }
        public int MonthsAlreadyPaid { get; private set; }
        public PawnLoan PawnLoan { get; private set; }
        public decimal PickupAmount { get; private set; }
        public DateTime PickupDate { get; private set; }
        private decimal refundAmount;

        public void Calculate()
        {
            ApplicableFees.Clear();
            PickupAmount = PawnLoan.CurrentPrincipalAmount;

            DeterminePartialPaymentInfo();
            refundAmount = 0;
            if (PawnLoan.IsExtended && PawnLoan.PartialPaymentPaid)
            {
                var extnReceipts = (from r in PawnLoan.Receipts
                                    where r.Event == ReceiptEventTypes.Extend.ToString()
                                          && r.RefTime > PawnLoan.LastPartialPaymentDate
                                    select r).Except(from r in PawnLoan.Receipts
                                                     where r.Event == ReceiptEventTypes.VEX.ToString()
                                                           && r.RefTime > PawnLoan.LastPartialPaymentDate
                                                     select r);

                refundAmount = (from rcpt in extnReceipts select rcpt.Amount).Sum();


            }
            else if (PawnLoan.IsExtended)
            {
                refundAmount = PawnLoan.ExtensionAmount;
            }

            foreach (var fee in PawnLoan.OriginalFees)
            {
                if (fee.Waived || fee.FeeState == FeeStates.VOID)
                {
                    continue;
                }



                var feeValue = GetFeeValue(fee);



                if (feeValue != 0)
                {
                    this.ApplicableFees.Add(new Fee
                                       {
                                           FeeType = fee.FeeType,
                                           CanBeProrated = fee.CanBeProrated,
                                           CanBeWaived = fee.CanBeWaived,
                                           FeeDate = fee.FeeDate,
                                           FeeRef = fee.FeeRef,
                                           FeeRefType = fee.FeeRefType,
                                           FeeState = fee.FeeState,
                                           OriginalAmount = fee.OriginalAmount,
                                           Prorated = fee.Prorated,
                                           Tag = fee.Tag,
                                           Value = feeValue,
                                           Waived = fee.Waived
                                       });
                }

                this.PickupAmount += feeValue;
            }


            
        }

        public void DeterminePartialPaymentInfo()
        {
            HasPartialPayment = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(CurrentSiteId) && PawnLoan.PartialPayments != null && PawnLoan.PartialPayments.Count > 0 && PawnLoan.PartialPayments[0].Date_Made != DateTime.MaxValue;

            if (HasPartialPayment)
            {
                if (PawnLoan.PartialPayments != null)
                    LastPartialPayment = PawnLoan.PartialPayments.OrderByDescending(pp => pp.Time_Made).FirstOrDefault();
                if (LastPartialPayment.Time_Made > PawnLoan.DueDate)
                {
                    DaysToPay = 0;
                    MonthsToPay = 0;
                }
                else
                {
                    DaysToPay = GetDaysToPay();
                    MonthsToPay = GetMonthsToPay();
                    DaysAlreadyPaid = GetDaysAlreadyPaid();
                    MonthsAlreadyPaid = GetMonthsAlreadyPaid();
                }
            }
        }

        //private decimal GetFeeValue(Fee fee)
        //{
        //    if (CurrentSiteId.State.Equals(States.Ohio))
        //    {
        //        return GetOhioFeeValue(fee);
        //    }

        //    return fee.Value;
        //}

        private decimal GetFeeValue(Fee fee)
        {
            if (fee.FeeType == FeeTypes.INTEREST)
            {
                if (HasPartialPayment)
                {
                    if (this.LastPartialPayment.Date_Made > PawnLoan.DateMade.AddMonths(1))
                    {
                        return 0;
                    }
                    var feeValue = Math.Round(DaysToPay * PawnLoan.InterestAmount / 30, 2);
                    if (PickupDate.Date <= PawnLoan.DateMade.AddMonths(1))
                    {
                        feeValue += Math.Round(MonthsToPay * PawnLoan.InterestAmount, 2);
                    }
                    return feeValue;
                }
                return this.PawnLoan.InterestAmount;

            }

            if (fee.FeeType == FeeTypes.STORAGE)
            {
                if (HasPartialPayment)
                {
                    if (this.LastPartialPayment.Date_Made > PawnLoan.DateMade.AddMonths(1))
                    {
                        return 0;
                    }
                    var feeValue = Math.Round(DaysToPay * PawnLoan.ServiceCharge / 30, 2);
                    if (PickupDate.Date <= PawnLoan.DateMade.AddMonths(1))
                    {
                        feeValue += Math.Round(MonthsToPay * PawnLoan.ServiceCharge, 2);
                    }
                    return feeValue;

                    //return Math.Round(DaysToPay * PawnLoan.ServiceCharge / 30, 2);
                }

                return this.PawnLoan.ServiceCharge;
            }

            if (fee.FeeType == FeeTypes.LATE)
            {
                return (PawnLoan.PickupLateFinAmount + PawnLoan.PickupLateServAmount - refundAmount);
            }

            return fee.Value;
        }

        public int GetDaysToPay()
        {
            var dateMade = PawnLoan.DateMade;
            var nextLoanDate = PawnLoan.DateMade.AddMonths(1);


            while (nextLoanDate <= LastPartialPayment.Date_Made)
            {
                nextLoanDate = nextLoanDate.AddMonths(1);
            }

            if (nextLoanDate == LastPartialPayment.Date_Made)
            {
                nextLoanDate = nextLoanDate.AddMonths(-1);
            }
            int days=0;
            if (LastPartialPayment.Date_Made < nextLoanDate)
                days = (LastPartialPayment.Date_Made - dateMade).Days;
            else
            //Do this only when LastPartialPayment is same as nextLoanDate else do not reduce 1 month
                days = (LastPartialPayment.Date_Made - nextLoanDate).Days;

            return 30 - Math.Min(30, days);
        }

        public int GetMonthsToPay()
        {
            return (this.PickupDate - LastPartialPayment.Date_Made).Days / 30;
        }

        public int GetMonthsAlreadyPaid()
        {
            if (!CurrentSiteId.State.Equals(States.Ohio))
            {
                return 0;
            }

            var dateMade = PawnLoan.DateMade;
            var nextLoanDate = new DateTime(dateMade.Year, dateMade.Month, dateMade.Day);

            var months = 1;
            var monthsAfterDueDate = 0;
            while (dateMade.AddMonths(months) <= LastPartialPayment.Date_Made)
            {
                if (dateMade.AddMonths(months) > PawnLoan.DateMade.AddMonths(1))
                {
                    monthsAfterDueDate++;
                }
                nextLoanDate = dateMade.AddMonths(months++);
            }

            return monthsAfterDueDate;
        }

        public int GetDaysAlreadyPaid()
        {
            if (!CurrentSiteId.State.Equals(States.Ohio))
            {
                return 0;
            }

            var dateMade = PawnLoan.DateMade;
            var lastLoanDate = new DateTime(dateMade.Year, dateMade.Month, dateMade.Day);

            var months = 1;
            while (dateMade.AddMonths(months) <= LastPartialPayment.Date_Made)
            {
                lastLoanDate = dateMade.AddMonths(months++);
            }
            
            return (LastPartialPayment.Date_Made - lastLoanDate).Days;
        }
    }
}
