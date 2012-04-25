/********************************************************************
* CashlinxDesktop.UserControls
* Race
* This user control can be used in a form to show list of valid values for race
* from the database and allow selection
* Sreelatha Rengarajan 4/5/2009 Initial version
* SR 6/1/2010 Added logic for changing back color
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

namespace Pawn.Forms.UserControls
{
    public partial class Race : UserControl
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

        public Race()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            this.raceList.SelectedIndex = -1;
        }

        protected override void OnLoad(EventArgs e)
        {

            try
            {
                if (!this.DesignMode)
                {
                    DataTable raceTable = GlobalDataAccessor.Instance.DesktopSession.RaceTable;
                    ArrayList raceTypes = new ArrayList();
                    if (raceTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in raceTable.Rows)
                        {
                            raceTypes.Add(new ComboBoxData(dr["code"].ToString(), dr["codedesc"].ToString()));
                        }
                        raceTypes.Insert(0, new ComboBoxData("", "Select"));
                        this.raceList.DataSource = raceTypes;
                        this.raceList.DisplayMember = "Description";
                        this.raceList.ValueMember = "Code";

                    }
                }

                //TO DO: The call to the database failed or did not yield rows
                //then populate the titles using static data

            }
            catch (SystemException Ex)
            {
                BasicExceptionHandler.Instance.AddException("Could not load Race data", Ex);
                
            }

            base.OnLoad(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (_required)
            {
                if (this.raceList.SelectedItem != null && this.raceList.Text != "Select")
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
                Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 3, this.Bounds.Height + 3);
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
                if (this.raceList.SelectedItem != null && this.raceList.Text != "Select")
                {
                    _isValid = true;
                }
                else
                {
                    _isValid = false;
                }

            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 3, this.Bounds.Height + 3);
            Commons.RemoveBorder(this, rect);
            
            base.OnLeave(e);


        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!DesignMode && this.BackColor != Color.Transparent)
                this.raceList.BackColor = this.BackColor;
        }

        private void raceList_DrawItem(object sender, DrawItemEventArgs e)
        {

            if (e.Index == -1)
                return;
            else
            {
                e.DrawBackground();
                Brush textBrush = SystemBrushes.ControlText;
                Font drawFont = e.Font;
                ComboBoxData eIdType = (ComboBoxData)raceList.Items[e.Index];
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
