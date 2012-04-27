namespace Common.Libraries.Objects.Authorization
{
    public class LimitsVO
    {
        public string ProductId { get; set; }
        public string ServiceOffering { get; set; }
        public decimal Limit { get; set; }
        public string StoreID { get; set; }
        public string StoreNumber { get; set; }
        public int ProdOfferingId { get; set; }
        public int RoleLimitId { get; set; }
        public string ResourceName { get; set; }

    }
}
