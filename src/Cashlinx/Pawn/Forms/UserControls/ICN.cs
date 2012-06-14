using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Windows.Forms;
using Common.Libraries.Utility;

namespace Pawn.Forms.UserControls
{
    public partial class ICN : UserControl
    {
        public ICN()
        {
            InitializeComponent();
        }

        public int Shop
        {
            get { return this.ICN_shop_tb.X<int>(); }
        }

        public int SubItem
        {
            get { return this.ICN_sub_item_tb.X<int>(-1); }
        }

        public int Year
        {
            get { return this.ICN_year_tb.X<int>(); }
        }

        public int Doc { get { return this.ICN_doc_tb.X<int>(); } }

        public string DocType
        {
            get { return this.ICN_doc_type_tb.X<string>(); }
        }

        public int Item { get { return this.ICN_item_tb.X<int>(); } }
    }
}
