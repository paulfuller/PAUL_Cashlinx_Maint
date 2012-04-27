using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Libraries.Objects.Layaway
{
    public class LayawayHistory
    {
        # region Constructors

        // for Serialization
        public LayawayHistory()
            : this(0, DateTime.MinValue)
        {
        }

        public LayawayHistory(decimal paymentAmountDue, DateTime paymentDueDate)
        {
            Payments = new List<LayawayHistoryPaymentInfo>();
            PaymentAmountDue = paymentAmountDue;
            PaymentDueDate = paymentDueDate;
        }

        # endregion

        # region Properties

        public decimal PaymentAmountDue { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public List<LayawayHistoryPaymentInfo> Payments { get; set; }

        # endregion

        # region Public Methods

        public bool IsPaid()
        {
            return PaymentAmountDue <= GetTotalPayments();
        }

        public decimal GetRemainingBalance()
        {
            return PaymentAmountDue - GetTotalPayments();
        }

        public decimal GetTotalPayments()
        {
            return (from p in Payments
                    select p.PaymentAmountMade).Sum();
        }

        # endregion
    }
}
