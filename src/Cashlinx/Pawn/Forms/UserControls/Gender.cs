/********************************************************************
* CashlinxDesktop.UserControls
* Gender
* This user control can be used in a form to show list of valid genders
 * and allows selection
* Sreelatha Rengarajan 4/5/2009 Initial version
 * SR 6/1/2010 Added logic to change the backcolor* 
*******************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.UserControls
{
    public partial class Gender : UserControl
    {
        private bool _required;
        private bool _isValid;

        [Category("Type")]
        [Description("Sets whether the Control is required to be entered in the form")]
        [DefaultValue(false)]
        public bool Required
        {
            get
            {
                return _required;
            }
            set
            {
                _required = value;
            }
        }

        [Category("Validation")]
        [Description("Sets if the control is valid")]
        [DefaultValue(false)]
        [Browsable(false)]
        public bool isValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
            }
        }

        public Gender()
        {
            InitializeComponent();
            try
            {
                ArrayList genderTypes = new ArrayList();
                genderTypes.Add(new ComboBoxData("", "Select"));
                genderTypes.Add(new ComboBoxData("M", "Male"));
                genderTypes.Add(new ComboBoxData("F", "Female"));
                //genderTypes.Add(new GenderData("U", "Unknown"));
                this.genderList.DataSource = genderTypes;
                this.genderList.DisplayMember = "Description";
                this.genderList.ValueMember = "Code";



            }
            catch (SystemException Ex)
            {
                BasicExceptionHandler.Instance.AddException("Error generating gender data", Ex);
            }
        }

        public void Clear()
        {
            this.genderList.SelectedIndex = -1;
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (_required)
            {
                if (genderList.Text != "Select")
                {
                    _isValid = true;
                }
                else
                    _isValid = false;
            }
            else
                _isValid = true;

        }




        protected override void OnEnter(EventArgs e)
        {
            try
            {
                Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 1, this.Bounds.Height + 3);
                Commons.CustomPaint(this, rect);
            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException("Error calling paint method to draw border", ex);
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            RemoveBorder();
        }

        private void RemoveBorder()
        {
            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 1, this.Bounds.Height + 3);
            Commons.RemoveBorder(this, rect);

        }

        protected override void OnLeave(EventArgs e)
        {
            if (_required)
                if (this.genderList.SelectedItem != null && this.genderList.Text != "Select")
                {
                    _isValid = true;
                }
                else
                {
                    _isValid = false;
                }

            RemoveBorder();
            base.OnLeave(e);


        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!DesignMode && this.BackColor != Color.Transparent)
                this.genderList.BackColor = this.BackColor;
        }

        private void genderList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            else
            {
                e.DrawBackground();
                Brush textBrush = SystemBrushes.ControlText;
                Font drawFont = e.Font;
                ComboBoxData eIdType = (ComboBoxData)genderList.Items[e.Index];
                string strToShow = eIdType.Description.ToString();
                if ((e.State & DrawItemState.Selected) > 0)
                {
                    textBrush = SystemBrushes.HighlightText;
                }

                e.Graphics.DrawString(strToShow, drawFont, textBrush, e.Bounds);
                e.DrawFocusRectangle();
            }
        }


    } 


}
