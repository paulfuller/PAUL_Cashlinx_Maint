using System;
using Common.Libraries.Objects.Business;

namespace Common.Libraries.Objects.Inquiry
{
    public class MDSE
    {
        public Icn Icn = new Icn();
        public int RfbStore;
        public int GunNumber;
        public string Status;
        public DateTime StartDate;
        public DateTime EndDate;

        public MDSE ()
        {

        }
    }
}
