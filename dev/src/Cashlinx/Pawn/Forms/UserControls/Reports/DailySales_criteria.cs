using System;
using System.Windows.Forms;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.UserControls.Reports
{


    public partial class DailySalesCriteria_panel : UserControl
    {
        public enum SortType_enum
        {
            [StringDBMap("Date", "Date")]  DATE,
            [StringDBMap("Transaction Type", "Type")] TRANS_TYPE
        };      

        public enum OptionType_enum
        {
            [StringDBMap("Standard (Without)", "STD")]   STANDARD,
            [StringDBMap("Include New Layaway", "NEW")]  NEW_LAYAWAY,
            [StringDBMap("Include Paid Out Layaway", "PAID")]  PAID_LAYAWAY,
            [StringDBMap("Include Both", "BOTH")]        ALL
        };

        public string SortBy
        {
            get
            {
                return StringDBMap_Enum<SortType_enum>.toDBValue((SortType_enum)SortBy_cb.SelectedIndex);
            }
        }

        public string Option
        {
            get
            {
                return StringDBMap_Enum<OptionType_enum>.toDBValue((OptionType_enum)Option_cb.SelectedIndex);
            }
        }

        public string ReportDetail
        {
            get
            {
                return comboboxReportDetail.SelectedItem.ToString();
            }
        }

        public DailySalesCriteria_panel()
        {
            InitializeComponent();
            SortBy_cb.DataSource = StringDBMap_Enum<SortType_enum>.displayValues();

            Option_cb.DataSource = StringDBMap_Enum<OptionType_enum>.displayValues();

            clearPanelFields();
        }

        private void DailySalesCriteria_panel_VisibleChanged(object sender, EventArgs e)
        {
            clearPanelFields();
        }

        private void clearPanelFields()
        {
            dateCalendarStart.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            dateCalendarEnd.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");

            LowSalesAmt_tb.Text = "";
            HighSalesAmt_tb.Text = "";

            LowVariance_tb.Text = "";
            HighVariance_tb.Text = "";
            SortBy_cb.SelectedIndex = 0;
            Option_cb.SelectedIndex = 0;
            comboboxReportDetail.SelectedIndex = 0;

        }


    }

    public class ComboBoxValue
    {
        public string Display { get; set; }

        public string Fields { get; set; }

        public ComboBoxValue(string display, string fields)
        {
            this.Display = display;
            this.Fields = fields;
        }
    }
}
