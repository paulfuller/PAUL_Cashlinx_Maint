using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Libraries.Objects.PDLoan
{
    [Serializable]
    public class PDLoanEvents
    {
        
       
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
    }
}
