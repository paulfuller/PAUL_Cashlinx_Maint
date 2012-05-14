using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Libraries.Objects.PDLoan
{
    [Serializable]
    public class PDLoanDetails
    {
        #region PDL Loan details

        public string CustomerSSN
        {
            get;
            set;
        }

        public string UWName
        {
            get;
            set;
        }

        public DateTime LoanRequestDate
        {
            get;
            set;
        }

        public decimal LoanAmt
        {
            get;
            set;
        }

        public decimal LoanPayOffAmt
        {
            get;
            set;
        }

        public decimal ActualLoanAmt
        {
            get;
            set;
        }

        public string LoanNumberOrig
        {
            get;
            set;
        }

        public string LoanNumberPrev
        {
            get;
            set;
        }

	    public string DeclineReasonDescription
        {
            get;
            set;
        }

        public string DeclineReasonCode
        {
            get;
            set;
        }

        public string LoanRolloverNotes
        {
            get;
            set;
        }

        public decimal LoanRollOverAmt
        {
            get;
            set;
        }

        public bool RevokeACH
        {
            get;
            set;
        }

        public bool XPPAvailable
        {
            get;
            set;
        }

        public decimal ActualFinanceChrgAmt
        {
            get;
            set;
        }

        public decimal AcutalServiceChrgAmt
        {
            get;
            set;
        }

        public decimal AccruedFinanceAmt
        {
            get;
            set;
        }

        public decimal LateFeeAmt
        {
            get;
            set;
        }

        public decimal NSFFeeAmt
        {
            get;
            set;
        }

        public string ACHWaitingToClear
        {
            get;
            set;
        }

        public decimal EstRolloverAmt
        {
            get;
            set;
        }


        public DateTime OrginationDate
        {
            get;
            set;
        }

        public DateTime DueDate
        {
            get;
            set;
        }

        public DateTime OrigDepDate
        {
            get;
            set;
        }

        public DateTime ExtendedDate
        {
            get;
            set;
        }

        public string LastUpdatedBy
        {
            get;
            set;
        }

        public string ShopNo
        {
            get;
            set;
        }

        public string ShopName
        {
            get;
            set;
        }

        public string ShopState
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public PDLoanDetails()
        {

        }

    }
}
