using System;

namespace Common.Libraries.Objects.Layaway
{
    [Serializable]
    public class LayawayPayment
    {
        # region Constants

        private const string PAYMENT_TEXT_FORMAT = "{0:c} - {1}";

        # endregion

        # region Constructors

        // for Serialization
        public LayawayPayment()
        {
        }

        public LayawayPayment(decimal amount, DateTime? dueDate)
        {
            Amount = amount;
            DueDate = dueDate;
            //SalesTaxInfo = taxInfo;
        }

        # endregion

        # region Properties

        public decimal Amount { get; set; }
        public DateTime? DueDate { get; set; }

        # endregion

        # region Public Methods

        public string BuildPaymentStatement()
        {
            if (DueDate.HasValue)
            {
                return string.Format(PAYMENT_TEXT_FORMAT, Amount, DueDate.Value.ToString("d"));
            }

            return string.Empty;
        }

        # endregion
    }
}
