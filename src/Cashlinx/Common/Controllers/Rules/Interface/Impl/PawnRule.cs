using System;

namespace Common.Controllers.Rules.Interface.Impl
{
    public class PawnRule
    {
        public bool CheckLoanAmount { get; private set; }
        public bool CheckLoanRange { get; private set; }
        public decimal LoanAmount { get; private set; }
        public string RuleValue { get; private set; }
        public string RuleKey { get; private set; }
        public string Alias { get; private set; }
        public string Company { get; private set; }
        public decimal LoanAmountMin { get; private set; }
        public decimal LoanAmountMax { get; private set; }
        public DateTime ToDate { get; private set; }
        public DateTime FromDate { get; private set; }

        public PawnRule(bool cLA, decimal amt, string rKey, string rVal, string a, DateTime f, DateTime t)
        {
            this.CheckLoanAmount = cLA;
            this.CheckLoanRange = false;
            this.LoanAmount = amt;
            this.LoanAmountMin = -1.0M;
            this.LoanAmountMax = -1.0M;
            this.RuleValue = rVal;
            this.RuleKey = rKey;
            this.Alias = a;
            this.Company = "1";
            this.FromDate = f;
            this.ToDate = t;
        }

        public PawnRule(bool cLR, decimal minAmt, decimal maxAmt, string rKey, string rVal, String a, DateTime f, DateTime t)
        {
            this.CheckLoanAmount = false;
            this.CheckLoanRange = cLR;
            this.LoanAmount = 0.0M;
            this.LoanAmountMin = minAmt;
            this.LoanAmountMax = maxAmt;
            this.RuleValue = rVal;
            this.RuleKey = rKey;
            this.Alias = a;
            this.Company = "1";
            this.FromDate = f;
            this.ToDate = t;
        }
    }
}
