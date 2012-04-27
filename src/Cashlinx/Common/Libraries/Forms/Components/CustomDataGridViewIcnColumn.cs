using System;
using System.Windows.Forms;

namespace Common.Libraries.Forms.Components
{
    public partial class CustomDataGridViewIcnColumn : DataGridViewColumn
    {
        public CustomDataGridViewIcnColumn()
            : base(new CustomDataGridViewIcnCell())
        {
            InitializeComponent();
            this.ReadOnly = true;
            this.Resizable = DataGridViewTriState.False;
            this.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CustomDataGridViewIcnCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(CustomDataGridViewIcnCell)))
                {
                    throw new InvalidCastException("Must be a CustomDataGridViewIcnCell");
                }
                base.CellTemplate = value;
            }
        }
    }
}
