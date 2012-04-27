using System.Windows.Forms;

namespace Common.Libraries.Forms.Components.EventArgs
{
    public class GridViewRowSelectingEventArgs : GridViewRowSelectedEventArgs
    {
        public GridViewRowSelectingEventArgs(int rowIndex, DataGridViewRow row, bool newRowSelectedValue)
            : base(rowIndex, row)
        {
            NewRowSelectedValue = newRowSelectedValue;
        }

        public bool Cancel { get; set; }
        public bool NewRowSelectedValue { get; private set; }
    }
}
