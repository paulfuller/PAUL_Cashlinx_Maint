using System;
using System.Collections.Generic;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Purchase
{
    [Serializable]
    public class PurchaseVO:CustomerProductDataVO
    {


        public DateTime DateMade
        {
            get;
            set;
        }
        public DateTime TimeMade
        {
            get;
            set;
        }
        public string EntityNumber
        {
            get;
            set;
        }
        public string EntityType
        {
            get;
            set;
        }
        public string OrgCust { get; set; }
        public DateTime OrgDate { get; set; }
        public int RefNumber { get; set; }
        public string RefType { get; set; }
        public decimal Freight { get; set; }
        public decimal SalesTax { get; set; }
        public string DispId { get; set; }
        public string MiscFlags { get; set; }
        public string TtyId { get; set; }



        public string PurchaseOrderNumber { get; set; }



        public string EntityId { get; set; }

        public string ShipType
        {
            get;
            set;
        }
        public string ShipNumber
        {
            get;
            set;
        }
        public string ShipComment
        {
            get;
            set;
        }
        public string StoreNumber
        {
            get;
            set;
        }
        public int IcnStoreNumber
        {
            get;
            set;
        }
        public int LastLine
        {
            get;
            set;
        }
        public int ManualTicketNumber
        {
            get;
            set;
        }
        public string ReturnReason
        {
            get;
            set;
        }
        public string PurchaseTenderType
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        public PurchaseVO()
        {


            Items = new List<Item>();
            OriginalFees = new List<Fee>();

        }

    }
}

