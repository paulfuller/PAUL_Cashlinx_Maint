/********************************************************************
* CashlinxDesktop.UserControls
* Eyecolor
* This user control can be used in a form to show list of valid eye colors
 * from the database and allow selection
* Sreelatha Rengarajan 4/5/2009 Initial version
 * SR 6/1/2010 Added logic to change the backcolor 
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
using Pawn.Logic;

namespace Pawn.Forms.UserControls
{
    public partial class EyeColor : UserControl
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


        public EyeColor()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            this.eyecolorList.SelectedIndex = -1;
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    DataTable dtEyeColorData = GlobalDataAccessor.Instance.DesktopSession.EyeColorTable;
                    ArrayList eyecolorTypes = new ArrayList();
                    if (dtEyeColorData.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtEyeColorData.Rows)
                        {
                            eyecolorTypes.Add(new ComboBoxData(dr["eye_color_code"].ToString(), dr["codedesc"].ToString()));
                        }
                    }
                    eyecolorTypes.Insert(0, new ComboBoxData("", "Select"));
                    this.eyecolorList.DataSource = eyecolorTypes;
                    this.eyecolorList.DisplayMember = "Description";
                    this.eyecolorList.ValueMember = "Code";
                }

            }
            catch (SystemException Ex)
            {
                BasicExceptionHandler.Instance.AddException("Could not fetch eye color data from session instance - ", Ex);
            }

            base.OnLoad(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (_required)
            {
                if (eyecolorList.Text != "Select")
                {
                    _isValid = true;
                }
                else
                    _isValid = false;
            }
 
            //base.OnLayout(e);
        }


        protected override void OnEnter(EventArgs e)
        {
            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width+2, this.Bounds.Height + 2);
            Commons.CustomPaint(this, rect);
        }


        protected override void OnLeave(EventArgs e)
        {
            if (_required)
                if (this.eyecolorList.SelectedItem != null && this.eyecolorList.Text!= "Select")
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
                this.eyecolorList.BackColor = this.BackColor;
        }

        private void eyecolorList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            else
            {
                e.DrawBackground();
                Brush textBrush = SystemBrushes.ControlText;
                Font drawFont = e.Font;
                ComboBoxData eIdType = (ComboBoxData)eyecolorList.Items[e.Index];
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
