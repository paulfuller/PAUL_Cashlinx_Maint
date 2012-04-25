using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Customer
{
    [Serializable]

    [XmlInclude(typeof(PawnLoan))]
    [XmlInclude(typeof(PurchaseVO))]
    public class CustomerProductDataVO
    {


        public decimal Amount { get; set; }
        public string ApprovedBy { get; set; }
        public string CreatedBy { get; set; }
        public List<Document> Documents { get; set; }
        public string CustomerNumber
        {
            get;
            set;
        }
        public List<Fee> Fees { get; set; }
        public string HoldDesc { get; set; }
        public List<Fee> OriginalFees { get; set; }
        public bool GunInvolved
        {
            get;
            set;
        }

        public string LastUpdatedBy { get; set; }
        public ProductStatus LoanStatus { get; set; }
        public DateTime MadeTime { get; set; }
        public DateTime OriginationDate { get; set; }
        public string OrgShopNumber { get; set; }
        public string OrgShopState
        {
            get;
            set;
        }
        public decimal OrgAmount
        {
            get;
            set;
        }
        public List<Item> Items { get; set; }
        public DateTime PfiEligible { get; set; }
        public List<Receipt> Receipts { get; set; }
        public DateTime StatusDate { get; set; }
        public StateStatus TempStatus { get; set; }
        public int TicketNumber { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime StatusTime { get; set; }
        public bool ProductDataComplete { get; set; }
        public string ProductType { get; set; }
        public List<int> GunNumbers { get; set; }


        public CustomerProductDataVO()
        {
            Fees = new List<Fee>();
            OriginalFees = new List<Fee>();
            Documents = new List<Document>();
            Items = new List<Item>();
            Receipts = new List<Receipt>();
            GunNumbers = new List<int>();

        }

    }
}
