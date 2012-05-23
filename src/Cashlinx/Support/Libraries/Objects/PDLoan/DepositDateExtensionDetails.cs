using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Libraries.Objects.PDLoan
{
    public class DepositDateExtensionDetails
    {
        public string Max_Extend_Date
        {
            get;
            set;
        }
        public string Cur_Dep_Date
        {
            get;
            set;
        }
        public List<ExtendedDateReasons> GetExtendedDateReasonsList
        {
            get;
            set;
        }
        public DepositDateExtensionDetails()
        {
            GetExtendedDateReasonsList = new List<ExtendedDateReasons>();
        }
    }
}
