using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Libraries.Objects.PDLoan
{
    [Serializable]
    public class PDLoanOtherDetails
    {
        public string ApprovalNumber
        {
            get;
            set;
        }
	
        public string ThirdPartyLoanNumber
        {
            get;
            set;
        }
	
        public decimal LoanApr
        {
            get;
            set;
        }
	
        public bool MultipleLoan
        {
            get;
            set;
        }
	
        public decimal ApprovedFinanceChrgRate
        {
            get;
            set;
        }
	
        public decimal RefunedServiceChrgAmt
        {
            get;
            set;
        }
	
        public decimal CourtCostAmt
        {
            get;
            set;
        }
	
        public decimal RequestedLoanAmt
        {
            get;
            set;
        }
	
        public decimal ApprovedLoanAmt
        {
            get;
            set;
        }
	
        public decimal ActualFinanceChrg
        {
            get;
            set;
        }
	
        public decimal OrigMaintFee
        {
            get;
            set;
        }
	
        public decimal ActualFinanceChrgRate
        {
            get;
            set;
        }
	
        public decimal APR
        {
            get;
            set;
        }
	
        public decimal EstRefinanceAmt
        {
            get;
            set;
        }
	
        public DateTime StatusChgDate
        {
            get;
            set;
        }
	
        public string CollectionStatusDesc
        {
            get;
            set;
        }
	
        public string CollectionStatusID
        {
            get;
            set;
        }
	
        public DateTime InsOnDate
        {
            get;
            set;
        }
	
        public int RefisAvailable
        {
            get;
            set;
        }
	
        public string PaymentFrequency
        {
            get;
            set;
        }
	
        public bool SuspendACH
        {
            get;
            set;
        }
	
        public decimal ActualBrokerAmt
        {
            get;
            set;
        }

        public decimal BrokerRate
        {
            get;
            set;
        }
       
    }
}
