using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Libraries.Objects.Business
{
    public class ReleaseFingerprintsInfo: PoliceInfo
    {
        public string RefType { get; set; }
        public int RefNumber { get; set; }
        public string Comment { get; set; }
        public string StoreNumber { get; set; }
        public string SubpoenaNumber { get; set; }
        public string TransactionDate { get; set; }
        public string SeizeStatus { get; set; }
    }
}
