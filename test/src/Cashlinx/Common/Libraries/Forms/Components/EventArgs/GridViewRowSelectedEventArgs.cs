using System.Windows.Forms;

namespace Common.Libraries.Forms.Components.EventArgs
{
    public class GridViewRowSelectedEventArgs : System.EventArgs
    {
        public GridViewRowSelectedEventArgs(int rowIndex, DataGridViewRow row)
        {
            RowIndex = rowIndex;
            Row = row;
        }

        public DataGridViewRow Row { get; private set; }
        public int RowIndex { get; private set; }
    }
}
