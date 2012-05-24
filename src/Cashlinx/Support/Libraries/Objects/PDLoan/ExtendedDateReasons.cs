using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Libraries.Objects.PDLoan
{
    [Serializable]
    public class ExtendedDateReasons
    {

        public ExtendedDateReasons()
        {
            this.initialize();
        }

        public string Reason_Description { get; set; }

        public string ReasonCode { get; set; }

        private void initialize()
        {
            Reason_Description = string.Empty;
            ReasonCode = string.Empty;
        }

    }
}
