using System;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Pawn
{
    public class PawnLoanKey
    {
        public string StoreNumber { get; set; }
        public int TicketNumber { get; set; }
        public DateTime OriginationDate { get; set; }
        public decimal LateRef {get; set;}
        public decimal LateRefPick {get; set;}
        public ProductStatus LoanStatus { get; set; }
        public decimal LoanAmount { get; set; }
        public string StoreState { get; set; }
        public string StoreAlias { get; set; }
        
        private DateTime dueDate;
        public DateTime DueDate {
            get
            {
                return (this.dueDate);
            }
            set
            {
                this.dueDate = value;
 //               this.GraceDate = this.dueDate.Date.AddDays(1.0d);
                this.GraceDate = this.dueDate.Date;
            }
        }
        public DateTime GraceDate
        {
            get;
            private set;
        }

        public PawnLoanKey()
        {
            this.StoreNumber = string.Empty;
            this.TicketNumber = 0;
            this.OriginationDate = DateTime.MinValue;
            this.LateRef = 0.0M;
            this.LateRefPick = 0.0M;
            this.LoanStatus = ProductStatus.ALL;
            this.LoanAmount = decimal.MinValue;
            this.DueDate = DateTime.MinValue;
            this.StoreAlias = string.Empty;
        }
    }
}
