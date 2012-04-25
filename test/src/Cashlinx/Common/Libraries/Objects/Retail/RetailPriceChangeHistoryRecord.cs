using System;

namespace Common.Libraries.Objects.Retail
{
    public class RetailPriceChangeHistoryRecord
    {
        public DateTime ChangeDate { get; set; }
        public decimal ChangePercentage
        {
            get
            {
                if (PriceBefore == 0)
                {
                    return 0M;
                }

                return (PriceBefore - PriceAfter) / PriceBefore * 100;
            }
        }
        public decimal PriceBefore { get; set; }
        public decimal PriceAfter { get; set; }
        public string ChangedBy { get; set; }
    }
}
