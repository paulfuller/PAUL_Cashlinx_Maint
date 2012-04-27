using System;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Business
{
    public class StoreCreditVO
    {

        public string CustomerNumber;
        public string StoreNumber;
        public string EntityNumber;
        public string EntityType;
        public Int32 DocNumber;
        public string DocType;
        public DateTime DateMade;
        public DateTime TimeMade;
        public decimal Amount;
        public StoreCreditStatus Status;
        public DateTime StatusDate;
        public DateTime StatusTime;
        public Int32 RefNumber;
        public string RefType;

    }
}
