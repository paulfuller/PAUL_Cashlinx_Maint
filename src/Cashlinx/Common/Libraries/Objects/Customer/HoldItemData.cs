/********************************************************************
* PawnObjects.VO.Customer
* HoldsItemData
* Class to denote the items in a transaction selected to be put on hold
* Sreelatha Rengarajan 8/6/2009 Updated
*******************************************************************/

namespace Common.Libraries.Objects.Customer
{
    public class HoldItemData
    {
        public string HoldType
        {
            get;
            set;
        }
        public string Icn
        {
            get;
            set;
        }
        public string Aisle
        {
            get;
            set;
        }
        public string Shelf
        {
            get;
            set;
        }
        public bool IsJewelry
        {
            get;
            set;
        } //
        public decimal LoanAmount
        {
            get;
            set;
        } //
        public int mDocNumber
        {
            get;
            set;
        } //
        public int mDocType
        {
            get;
            set;
        } //
        public string TicketDescription
        {
            get;
            set;
        } //
        public string LocationOther
        {
            get;
            set;
        }
    }
}
