using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashlinxDesktopLoadTester
{
    public class LoanItem
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Catg { get; set; }
        public List<string> CatgAnswers { get; set; }
        public string LoanAmount { set; get; }
        public List<LoanItem> JewelryStones { get; set; }

        public LoanItem()
        {
            this.Manufacturer = "";
            this.Model = "";
            this.Catg = "";
            this.CatgAnswers = new List<string>();
            this.LoanAmount = "";
            this.JewelryStones = new List<LoanItem>();
        }
    }
}
