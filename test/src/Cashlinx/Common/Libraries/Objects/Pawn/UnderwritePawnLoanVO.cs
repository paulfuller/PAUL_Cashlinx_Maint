using System;
using System.Collections.Generic;

namespace Common.Libraries.Objects.Pawn
{
    public class UnderwritePawnLoanVO
    {
        
        public decimal totalLoanAmount { get; set; }
        public decimal totalFinanceCharge { get; set; }
        public decimal totalServiceCharge { get; set; }
        public decimal APR { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime MadeDate { get; set; }
        public TimeSpan MadeTime { get; set; }
        public DateTime PFIDate { get; set; }
        public DateTime PFINotifyDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public DateTime GraceDate { get; set; }
        public Dictionary<string, decimal> feeDictionary { get; set; }
        

        public UnderwritePawnLoanVO()
        {
            this.totalLoanAmount = 0.0M;
            this.totalFinanceCharge = 0.0M;
            this.totalServiceCharge = 0.0M;
            this.feeDictionary = new Dictionary<string, decimal>();
            this.APR = 0.0M;
        }
        /*
        private double totalLoanAmount = 0.0d;

        public int getTotalLoanAmount()
        {
            return totalLoanAmount;
        }

        public void setTotalLoanAmount(double loanAmount)
        {
            totalLoanAmount = loanAmount;
        }

        private double totalFinanceCharge = 0.0d;

        public int getTotalFinanceCharge()
        {
            return totalFinanceCharge;
        }

        public void setTotalFinanceCharge(double financeCharge)
        {
            totalFinanceCharge = financeCharge;
        }


        private double totalServiceCharge = 0.0d;

        public int getTotalServiceCharge()
        {
            return totalServiceCharge;
        }

        public void setTotalServiceCharge(double serviceCharge)
        {
            totalServiceCharge = serviceCharge;
        }


        private double totalServiceCharge = 0.0d;

        public int getTotalServiceCharge()
        {
            return totalServiceCharge;
        }

        public void setTotalServiceCharge(double serviceCharge)
        {
            totalServiceCharge = serviceCharge;
        }


        private Dictionary<string, double> feeDictionary;
        
        public Dictionary<string, double> getFeeDictionary()
        {
            return feeDictionary;
        }

        public void setFeeDictionary(Dictionary<string, double> feeDict)
        {
            feeDictionary = feeDict;
        }
         */

    }
}
