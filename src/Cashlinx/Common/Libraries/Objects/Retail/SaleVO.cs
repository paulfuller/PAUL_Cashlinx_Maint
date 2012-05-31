using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Retail
{
    [Serializable]
    public class SaleVO : CustomerProductDataVO
    {
        public SaleVO()
        {
            RetailItems = new List<RetailItem>();
        }

        public SalesTaxInfo SalesTaxInfo;
        public decimal TotalSaleAmount;
        public string StoreNumber { get; set; } // TG - 05/22/2012 - changed to property so the cloning would work through reflection
        public decimal SalesTaxPercentage;
        public decimal SalesTaxAmount;
        public decimal CouponPercentage { get; set; } // changed to property so that the cloning would work through reflection
        public decimal CouponAmount { get; set; } // changed to property so that the cloning would work through reflection
        public string CouponCode { get; set; } // changed to property so that the cloning would work through reflection
        public decimal ShippingHandlingCharges;
        public string LayawayTicketNumber;

        public string CreateType;


        public decimal Cost;

        public string CashDrawerID;

        public List<RetailItem> RetailItems { get; set; }

        public List<TenderData> TenderDataDetails { get; set; }

        public List<TenderData> RefundTenderData { get; set; }

        public DateTime DateMade { get; set; }

        public DateTime TimeMade { get; set; }

        public string EntityNumber { get; set; }

        public string EntityType {get; set; }

        public string EntityName { get; set; }

        public string EntityId { get; set; }

        public int RefNumber { get; set; }

        public string RefType { get; set; }


        public SaleVO(SaleVO sale)
        {
            try
            {
                foreach (PropertyInfo pi in typeof(SaleVO).GetProperties())
                    GetType().GetProperty(pi.Name).SetValue(this, pi.GetValue(sale, null), null); 
            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, null, ex.Message);
            }
        }

    }
}
