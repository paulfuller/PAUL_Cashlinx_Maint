using System;
using System.Windows.Forms;
using Common.Libraries.Utility;

namespace Common.Libraries.Forms.Components
{
    public partial class CalendarCell : DataGridViewTextBoxCell
    {
        public CalendarCell()
            : base()
        {
            InitializeComponent();
            // Use the short date format.
            this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            var ctl =
                DataGridView.EditingControl as CalendarEditingControl;
            if (ctl != null)
                ctl.Value = Utilities.GetDateTimeValue(this.Value, DateTime.MaxValue);
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(CalendarEditingControl);
            }

        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.
                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return DateTime.Now;
            }
        }
    }
}
