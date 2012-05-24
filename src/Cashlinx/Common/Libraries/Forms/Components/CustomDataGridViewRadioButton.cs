/********************************************************************
* CustomFormElements
* CustomDataGridViewRadioButton
* Radio button column and cell type for the datagridview
* Sreelatha Rengarajan 6/15/2009 Initial version
*******************************************************************/

using System;
using System.Windows.Forms;
using System.Drawing;

namespace Common.Libraries.Forms.Components
{
    public class DataGridViewRadioButtonColumn : DataGridViewColumn
    {
        public DataGridViewRadioButtonColumn()
        {
            this.CellTemplate = new DataGridViewRadioButtonCell();
        }
    }

    public delegate void RadioButtonClickedHandler(bool state);
    public class DataGridViewRadioButtonCellEventArgs : System.EventArgs
    {
        bool _bChecked;
        public DataGridViewRadioButtonCellEventArgs(bool bChecked)
        {
            _bChecked = bChecked;
        }

        public bool Checked
        {
            get
            {
                return _bChecked;
            }
        }
    }

    public class DataGridViewRadioButtonCell : DataGridViewTextBoxCell
    {
        Point radioButtonLocation;
        Size radioButtonSize;
        bool _checked = false;
        public bool CHECKED
        {
            get
            {
                return _checked;
            }
            set
            {
                _checked = value;
                if (this.DataGridView != null)
                this.DataGridView.InvalidateCell(this);
            }
        }

        Point _cellLocation = new Point();
        System.Windows.Forms.VisualStyles.RadioButtonState _cbState =
        System.Windows.Forms.VisualStyles.RadioButtonState.UncheckedNormal;
        public event RadioButtonClickedHandler OnRadioButtonClicked;

        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            Point p = new Point();
            Size s = RadioButtonRenderer.GetGlyphSize(graphics, System.Windows.Forms.VisualStyles.RadioButtonState.UncheckedNormal);

            p.X = cellBounds.Location.X + (cellBounds.Width / 2) - (s.Width / 2);
            p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2);

            _cellLocation = cellBounds.Location;
            radioButtonLocation = p;
            radioButtonSize = s;

            if (_checked)
                _cbState = System.Windows.Forms.VisualStyles.RadioButtonState.CheckedNormal;

            else
                _cbState = System.Windows.Forms.VisualStyles.RadioButtonState.UncheckedNormal;

            RadioButtonRenderer.DrawRadioButton(graphics, radioButtonLocation, _cbState);
        }


        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);

            if (p.X >= radioButtonLocation.X && p.X <= radioButtonLocation.X + radioButtonSize.Width && p.Y >= radioButtonLocation.Y && p.Y <= radioButtonLocation.Y + radioButtonSize.Height)
            {
                _checked = !_checked;

                if (OnRadioButtonClicked != null)
                {
                    OnRadioButtonClicked(_checked);
                    
                }
                this.DataGridView.InvalidateCell(this);
            }
            base.OnMouseClick(e);
        }

        protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
        {
            base.OnKeyUp(e, rowIndex);
            if (e.KeyCode == Keys.Space)
            {
                _checked = true;

                if (_checked)
                {
                    OnRadioButtonClicked(_checked);
                    
                }
                this.DataGridView.InvalidateCell(this);
            }
        }

        /// <summary>
        /// To Set the status of the Check-Box Header column
        /// </summary>
        /// <param name="flagIndicator">True - to mark checked state. False : To mark it as Unchecked.</param>

        public void SetRadioButton(bool flagIndicator)
        {
            _checked = flagIndicator;

            this.DataGridView.InvalidateCell(this);            
            
        }

        // Force the cell to repaint itself when the mouse pointer enters it.
        protected override void OnMouseEnter(int rowIndex)
        {
            this.DataGridView.InvalidateRow(rowIndex);
        }

        // Force the cell to repaint itself when the mouse pointer leaves it.
        protected override void OnMouseLeave(int rowIndex)
        {
            this.DataGridView.InvalidateRow(rowIndex);
        }


    }
}


