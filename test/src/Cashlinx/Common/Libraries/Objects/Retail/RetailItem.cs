using System;
using System.Reflection;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Logger;

namespace Common.Libraries.Objects.Retail
{
    public enum SaleType
    {
        InShop,
        Ebay,
        CraigsList,
    }
    [Serializable]
    public class RetailItem : Item
    {
        public decimal DiscountPercent { get; set; }
        public decimal NegotiatedPrice { get; set; }
        public string NxtComments { get; set; }
        public decimal PreviousRetailPrice { get; set; }
        public string TranType { get; set; }
        public SaleType SaleType { get; set; }
        public bool OutTheDoor { get; set; }
        public int DispDoc { get; set; }
        public string DispDocType { get; set; }
        public decimal CouponPercentage { get; set; }
        public decimal CouponAmount { get; set; }
        public decimal ProratedCouponAmount { get; set; }
        public string TranCouponCode { get; set; }
        public string CouponCode { get; set; }
        public int RefundQuantity { get; set; }
        public string UserItemComments { get; set; }
        public string TempUserItemComments { get; set; }
        public bool Selected { get; set; }

        public decimal TotalPrice
        {
            get { return Math.Round((NegotiatedPrice * Quantity) - CouponAmount, 2); }
        }

        public RetailItem()
        {
            Quantity = 0;
            DiscountPercent = 0.0m;
            JeweleryCaseNumber = string.Empty;
            NegotiatedPrice = 0.0m;
            NxtComments = string.Empty;
            OutTheDoor = false;
            RefundQuantity = 0;
        }

        public RetailItem(Item pawnItem)
        {

            PropertyInfo[] itemProperties = pawnItem.GetType().GetProperties();
            foreach (PropertyInfo pro in itemProperties)
            {
                PropertyInfo prop2 = pawnItem.GetType().GetProperty(pro.Name);
                try
                {
                    prop2.SetValue(this, pro.GetValue(pawnItem, null), null);
                }
                catch (Exception ex)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, null, ex.Message);
                }
            }
        }

        public decimal CalculateDiscountPercentage(decimal retailPrice, decimal negotiatedPrice)
        {
            if (retailPrice == 0)
            {
                return 0;
            }

            return Math.Round(((retailPrice - negotiatedPrice) / retailPrice) * 100, 3);
        }
    }
}
