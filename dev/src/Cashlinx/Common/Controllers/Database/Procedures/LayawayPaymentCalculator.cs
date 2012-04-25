using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Common.Controllers.Database.Procedures
{
    public class LayawayPaymentCalculator
    {
        # region Constants

        private const int MAX_PAYMENTS = 12;
        private const decimal REQUIRED_SERVICE_FEE_TOTAL = 2;

        # endregion

        # region Constructors

        public LayawayPaymentCalculator(SalesTaxInfo taxInfo)
        {
            DownPaymentPercentage = .10M; //TODO: Need to update the percentage value.
            SalesTaxInfo = taxInfo;
        }

        # endregion

        # region Properties

        public decimal DefaultDownPayment { get; set; }
        public DateTime DefaultFirstPaymentDate { get; set; }
        public int DefaultNumberOfPayments { get; private set; }
        public decimal DownPayment { get; set; }
        private decimal DownPaymentPercentage { get; set; }
        public DateTime FirstPaymentDate { get; set; }
        public decimal LayawayAmount { get; private set; }

        public decimal MonthlyPaymentAmount { get; private set; }
        public int NumberOfPayments { get; set; }
        public List<LayawayPayment> Payments { get; private set; }
        public SalesTaxInfo SalesTaxInfo { get; private set; }
        public decimal ServiceFee { get; private set; }
        public decimal ServiceFeeTax { get; private set; }
        public decimal ServiceFeeTotal
        {
            get { return ServiceFee + ServiceFeeTax; }
        }

        # endregion

        # region Public Methods

        public bool CanDistributeIfDownpaymentSetTo(decimal value)
        {
            decimal remainingValue = LayawayAmount - REQUIRED_SERVICE_FEE_TOTAL - value;
            return Utilities.CanCurrencyValueDistibuteOverItems(remainingValue, NumberOfPayments);
        }

        public bool CanDistributeIfNumberOfPaymentsSetTo(int value)
        {
            decimal remainingValue = LayawayAmount - REQUIRED_SERVICE_FEE_TOTAL - DownPayment;
            return Utilities.CanCurrencyValueDistibuteOverItems(remainingValue, value);
        }

        public void ReCalculate()
        {
            Payments = new List<LayawayPayment>();

            if (LayawayAmount <= 0)
            {
                AddEmptyPaymentsIfRequired();
                return;
            }

            CalculateMonthlyPayments();
            AddEmptyPaymentsIfRequired();
        }

        public void CalculateDefaultValues(decimal layawayAmount)
        {
            LayawayAmount = layawayAmount;
            CalculateServiceFee();
            CalculateDefaultDownPayment();
            CalculateDefaultFirstPayment();
            DefaultNumberOfPayments = CalculateDefaultNumberOfPayments();
            NumberOfPayments = DefaultNumberOfPayments;
            ReCalculate();
        }

        # endregion

        # region Helper Methods

        private void AddEmptyPaymentsIfRequired()
        {
            for (var i = Payments.Count; i < MAX_PAYMENTS; i++)
            {
                Payments.Add(new LayawayPayment(0, null));
            }
        }

        private void CalculateDefaultDownPayment()
        {
            DefaultDownPayment = ((LayawayAmount - REQUIRED_SERVICE_FEE_TOTAL) * DownPaymentPercentage).RoundUp(2);
            DownPayment = DefaultDownPayment;
        }

        private void CalculateDefaultFirstPayment()
        {
            var shopDateTime = ShopDateTime.Instance;
            DefaultFirstPaymentDate = CalculatePaymentDate(shopDateTime.ShopDate, 1);
            FirstPaymentDate = DefaultFirstPaymentDate;
        }

        //TODO: This should be a rule!!!!
        private int CalculateDefaultNumberOfPayments()
        {
            var amountRemaining = LayawayAmount - REQUIRED_SERVICE_FEE_TOTAL - DownPayment;

            if (amountRemaining <= 100)
            {
                return 1;
            }
            else if (amountRemaining <= 200)
            {
                return 2;
            }
            else if (amountRemaining <= 300)
            {
                return 3;
            }
            else if (amountRemaining <= 400)
            {
                return 4;
            }
            else if (amountRemaining <= 500)
            {
                return 5;
            }
            else
            {
                return 6;
            }
        }

        public static DateTime CalculatePaymentDate(DateTime baseDate, int numberOfMonthsToAdd)
        {
            DateTime nextPayment = baseDate.AddMonths(numberOfMonthsToAdd);

            if (nextPayment.Day != baseDate.Day)
            {
                nextPayment = nextPayment.AddDays(1);
            }

            return nextPayment;
        }

        private void CalculateMonthlyPayments()
        {
            decimal totalRemaining = LayawayAmount - REQUIRED_SERVICE_FEE_TOTAL - DownPayment;
            MonthlyPaymentAmount = (totalRemaining / NumberOfPayments).RoundUp(2);
            decimal allocated = 0;

            for (int i = 1; i <= NumberOfPayments - 1; i++)
            {
                allocated += MonthlyPaymentAmount;
                Payments.Add(new LayawayPayment(MonthlyPaymentAmount, CalculatePaymentDate(FirstPaymentDate, i - 1)));
            }

            Payments.Add(new LayawayPayment(totalRemaining - allocated, CalculatePaymentDate(FirstPaymentDate, NumberOfPayments - 1)));
        }

        private void CalculateServiceFee()
        {
            
            ServiceFee = Math.Round(SalesTaxInfo.CalculatePreTaxAmount(REQUIRED_SERVICE_FEE_TOTAL), 2);
            ServiceFeeTax = REQUIRED_SERVICE_FEE_TOTAL - ServiceFee;
        }

        # endregion
    }
}
