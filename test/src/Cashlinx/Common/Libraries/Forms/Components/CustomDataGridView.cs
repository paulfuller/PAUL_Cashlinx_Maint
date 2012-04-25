using System;
using System.Windows.Forms;
using Common.Libraries.Forms.Components.EventArgs;

namespace Common.Libraries.Forms.Components
{
    public partial class CustomDataGridView : DataGridView
    {
        # region Constructors

        public CustomDataGridView()
        {
            InitializeComponent();
        }

        # endregion

        # region Events

        public event EventHandler<GridViewRowSelectedEventArgs> GridViewRowSelected;
        public event EventHandler<GridViewRowSelectingEventArgs> GridViewRowSelecting;

        # endregion

        # region Overridden Methods

        protected override void SetSelectedRowCore(int rowIndex, bool selected)
        {
            DataGridViewRow row = this.Rows.Count == rowIndex ? null : this.Rows[rowIndex];
            var args = new GridViewRowSelectingEventArgs(rowIndex, row, selected);
            RaiseGridViewRowSelecting(args);

            if (args.Cancel)
            {
                return;
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => base.SetSelectedRowCore(rowIndex, selected)));
            }
            else
            {
                base.SetSelectedRowCore(rowIndex, selected);
            }

            RaiseGridViewRowSelected(new GridViewRowSelectedEventArgs(rowIndex, row));
        }

        # endregion

        # region Helper Methods

        private void RaiseGridViewRowSelected(GridViewRowSelectedEventArgs args)
        {
            if (GridViewRowSelected == null)
            {
                return;
            }

            GridViewRowSelected(this, args);
        }
        
        private void RaiseGridViewRowSelecting(GridViewRowSelectingEventArgs args)
        {
            if (GridViewRowSelecting == null)
            {
                return;
            }

            GridViewRowSelecting(this, args);
        }

        # endregion

        public void SetupAutoSizeColumns()
        {
            var widths = new int[Columns.Count];
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            foreach (DataGridViewColumn column in Columns)
            {
                if (column.AutoSizeMode == DataGridViewAutoSizeColumnMode.None)
                {
                    continue;
                }

                widths[column.Index] = column.Width;
            }

            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            foreach (DataGridViewColumn column in Columns)
            {
                if (column.AutoSizeMode == DataGridViewAutoSizeColumnMode.None)
                {
                    continue;
                }

                column.Width = widths[column.Index];
            }

            AllowUserToResizeColumns = true;
        }
    }
}
