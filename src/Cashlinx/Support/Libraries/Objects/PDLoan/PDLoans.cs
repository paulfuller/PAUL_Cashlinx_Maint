using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Libraries.Objects.PDLoan
{
    class PDLoans
    {
        private List<PDLoan> pdloans = new List<PDLoan>();
        private PDLoan pdlloan;
        private int state;
        private PDLoan loaninfocus;

        /*__________________________________________________________________________________________*/

        public int State { get; set; }
        /*__________________________________________________________________________________________*/
        public PDLoan GetActiveLoan { get; set; }
        /*__________________________________________________________________________________________*/
        public PDLoan FetchLoanByTicketNo(string ticketnumber)
        {
            int ndx = 0;
            ndx = pdloans.FindIndex(item => item.PDLLoanNumber == ticketnumber);
            return pdloans[ndx];
        }
        /*__________________________________________________________________________________________*/
        public PDLoan FetchLoanByApplicationID( string ApplicationID )
        {
            int ndx = 0;
            ndx = pdloans.FindIndex(item => item.LoanApplicationId == ApplicationID);
            return pdloans[ndx];
        }

        /*__________________________________________________________________________________________*/
        public void  AddPDLoan( PDLoan loan)
        {
            pdloans.Add(loan);
        }

    }
}
