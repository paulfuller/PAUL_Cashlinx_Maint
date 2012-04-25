using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashlinxDesktopLoadTester
{
    public class LoadTestInputVO
    {
        //public enum LoanItemType
        //{
        //    GENERAL,
        //    JEWELRY,
        //    GUN
        //}

        //public LoanItemType LoanType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string StreetAddress { get; set; }
        //public string ZipCode { get; set; }
        //public int SelectionChoice { get; set; }
        /*private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get
            {
                return (this.dateOfBirth.Date);
            }
            set
            {
                this.dateOfBirth = value;
            }
        }
        public string IdType { get; set; }
        public string IdNumber { get; set; }
        public string IdState { get; set; }
        public string SSN{ get; set; }
        private DateTime idExpire;
        public DateTime IdExpire
        {
            get
            {
                return (this.idExpire.Date);
            }
            set
            {
                this.idExpire = value;
            }
        }

        private List<LoanItem> loanItems;
        public List<LoanItem> LoanItems
        {
            get
            {
                return (this.loanItems);
            }
            set
            {
                this.loanItems = value;
            }
        }
        */

        public LoadTestInputVO()
        {
            FirstName = "";
            LastName = "";
/*            StreetAddress = "";
            ZipCode = "";
            dateOfBirth = DateTime.Now;
            IdType = "";
            IdNumber = "";
            IdState = "";
            SSN = "";
            loanItems = new List<LoanItem>() {new LoanItem()};
            SelectionChoice = 0;*/
        }

    }
}
