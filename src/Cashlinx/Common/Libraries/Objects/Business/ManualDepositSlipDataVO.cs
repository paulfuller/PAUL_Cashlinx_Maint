using System;
using System.Data;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Business
{
    public class ManualDepositSlipDataVO
    {
        /*
         * checkid varchar2(47),
                              makername varchar2(40),
                              checkamount number(12, 2),
                              creationdate timestamp with time zone,
                              username varchar2(100)
         */
        public class ManualDepositSlipVO
        {
            public string CheckId { private set; get; }
            public string MakerName { private set; get; }
            public decimal CheckAmount { private set; get; }
            public DateTime RecordDate { private set; get; }
            public string UserName { private set; get; }

            //Initialize from a data row
            public ManualDepositSlipVO(DataRow dataRow)
            {
                this.CheckId = string.Empty;
                this.MakerName = string.Empty;
                this.CheckAmount = 0.0m;
                this.RecordDate = DateTime.Now;
                this.UserName = string.Empty;
                if (dataRow == null) return;
                this.CheckId = Utilities.GetStringValue(dataRow.GetDataObject("checkid"), string.Empty);
                this.MakerName = Utilities.GetStringValue(dataRow.GetDataObject("makername"), string.Empty);
                //this
            }
        }
    }
}
