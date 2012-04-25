/********************************************************************
* CashlinxDesktop.UserControls
* HairColor
* This user control can be used in a form to show list of valid hair colors
* and allows selection
* Sreelatha Rengarajan 4/5/2009 Initial version
* SR 6/1/2010 Added logic to change the backcolor* 
*******************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using Common.Controllers.Application;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
//using Pawn.Logic;

namespace Support.Forms
{
    public partial class Haircolor : UserControl
    {
        private bool _required;
        private bool _isValid;

        [Category("Type")]
        [Description("Sets whether the Control is required to be entered in the form")]
        [DefaultValue(false)]
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }

        [Category("Validation")]
        [Description("Sets if the control is valid")]
        [DefaultValue(false)]
        [Browsable(false)]
        public bool isValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        public Haircolor()
        {
            InitializeComponent();

        }

        public void Clear()
        {
            this.haircolorList.SelectedIndex = -1;
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    DataTable haircolorTable = GlobalDataAccessor.Instance.DesktopSession.HairColorTable;
                    ArrayList haircolorTypes = new ArrayList();
                    if (haircolorTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in haircolorTable.Rows)
                        {
                            haircolorTypes.Add(new ComboBoxData(dr["hair_color_code"].ToString(), dr["codedesc"].ToString()));
                        }
                    }
                    haircolorTypes.Insert(0, new ComboBoxData("", "Select"));
                    this.haircolorList.DataSource = haircolorTypes;
                    this.haircolorList.DisplayMember = "Description";
                    this.haircolorList.ValueMember = "Code";
                }

            }
            catch (SystemException Ex)
            {
                BasicExceptionHandler.Instance.AddException("Could not fetch hair color data ", Ex);
            }

            base.OnLoad(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (_required)
            {

                if (this.haircolorList.SelectedItem != null && this.haircolorList.Text != "Select")
                {
                    _isValid = true;
                }
                else
                {
                    _isValid = false;                    
                }
            }

            //base.OnLayout(e);
        }


        protected override void OnEnter(EventArgs e)
        {
            try
            {
                Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 2, this.Bounds.Height + 2);
                Commons.CustomPaint(this, rect);
            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException("Error calling the paint method to draw border", ex);
            }
        }


        protected override void OnLeave(EventArgs e)
        {
            if (_required)
                if (this.haircolorList.SelectedItem != null && this.haircolorList.Text != "Select")
                {
                    _isValid = true;
                }
                else
                {
                    _isValid = false;
                }

            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 2, this.Bounds.Height + 2);
            Commons.RemoveBorder(this, rect);            
            base.OnLeave(e);


        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!DesignMode && this.BackColor != Color.Transparent)
                this.haircolorList.BackColor = this.BackColor;
        }

        private void haircolorList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            else
            {
                e.DrawBackground();
                Brush textBrush = SystemBrushes.ControlText;
                Font drawFont = e.Font;
                ComboBoxData eIdType = (ComboBoxData)haircolorList.Items[e.Index];
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
