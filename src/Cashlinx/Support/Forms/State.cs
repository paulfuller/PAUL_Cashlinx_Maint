/********************************************************************
* CashlinxDesktop.DesktopForms
* State
* This user control will show a list of all the states in USA 
* from the database
* Sreelatha Rengarajan 3/13/2009 Initial version
 * SR 6/1/2010 Added logic for changing back color* 
*******************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Support.Logic;

//
//
//
//
//

namespace Support.Forms
{
    public partial class State : UserControl
    {
        private bool _showCode;
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

        [Category("Display")]
        [Description("Sets whether to display the code for the states when the list is rendered")]
        [DefaultValue("false")]
        public bool DisplayCode
        {
            get
            {
                return _showCode;
            }
            set
            {
                _showCode = value;
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


        public State()
        {
            InitializeComponent();
            this.stateList.DrawMode = DrawMode.OwnerDrawFixed;

        }

        [Category("Validation")]
        [Description("Sets the current selected value of the control based upon the shortName of the USState")]
        [DefaultValue(false)]
        [Browsable(false)]
        public string selectedValue
        { 
            get
            {
                if (this.stateList.SelectedItem == null)
                    return "";
                else
                    return ((USState)this.stateList.SelectedItem).ShortName;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    System.Collections.Generic.IEnumerable<USState> r =
                            from USState v in stateList.Items
                            where v.ShortName.Equals(value)
                            select v
                            ;

                    //r.First().Selected = true;
                    stateList.SelectedIndex = stateList.Items.IndexOf(r.First());
                }
                else
                {                   
                    stateList.SelectedIndex = -1;
                }
            }
        }

         

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    DataTable stateTable = Support.Logic.CashlinxPawnSupportSession.Instance.StateTable;

                    if (stateTable.Rows.Count > 0)
                    {
                        ArrayList USStates = new ArrayList();

                        foreach (DataRow dr in stateTable.Rows)
                        {
                            USStates.Add(new USState(dr["state_name"].ToString(), dr["state_code"].ToString()));
                        }
                        
                        //Madhu 12/07/2010 fix forbugzilla 10
                        USStates.Insert(0, new USState("", "Select One"));
                        this.stateList.DataSource = USStates;
                    }
                }

                //To do: There should be an application wide data structure that holds the state values
                //which will be used to populate the drop down in case the call to DB could not be completed or
                //the call did not yield any rows
            }
            catch (SystemException EX)
            {
                BasicExceptionHandler.Instance.AddException("States data could not be loaded", EX);
            }

            base.OnLoad(e);
        }


        private void stateList_Layout(object sender, LayoutEventArgs e)
        {
            if (_showCode)
            {
                this.stateList.DisplayMember = "ShortName";
                this.stateList.ValueMember = "LongName";
              }
            else
            {
                this.stateList.DisplayMember = "LongName";
                this.stateList.ValueMember = "ShortName";
            }

        }

        protected override void OnEnter(EventArgs e)
        {
            try
            {
                Rectangle rect;
                rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 2, this.Bounds.Height + 2);
                Commons.CustomPaint(this, rect);
            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException("Error calling the paint method to draw border", ex);
            }
            //base.OnEnter(e);

        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (!DesignMode)
            {
                if (_required)
                {
                    //Madhu fix for the bugzilla defect number 49
                    //if (stateList.SelectedIndex > -1)
                    if (stateList.SelectedIndex > 0)
                    {
                        _isValid = true;
                    }
                    else
                        _isValid = false;
                }
                else
                    _isValid = true;
            }

            //base.OnLayout(e);
        }



        protected override void OnLeave(EventArgs e)
        {
            if (_required)
            {
                if (this.stateList.SelectedItem != null && stateList.SelectedIndex > 0)
                {
                    _isValid = true;
                }
                else
                {
                    _isValid = false;
                }
            }
            else
                _isValid = true;

            Rectangle rect;
            rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 2, this.Bounds.Height + 2);

            Commons.RemoveBorder(this, rect);
            base.OnLeave(e);


        }


        private void stateList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            else
            {
                e.DrawBackground();
                Brush textBrush = SystemBrushes.ControlText;
                Font drawFont = e.Font;
                USState eState = (USState)stateList.Items[e.Index];
                string strToShow = eState.ShortName.ToString();
                if (eState.Selected)
                {
                    textBrush = Brushes.Red;
                    if ((e.State & DrawItemState.Selected) > 0)
                        drawFont = new Font(drawFont.FontFamily, drawFont.Size, System.Drawing.FontStyle.Bold);

                }
                else
                {
                    if ((e.State & DrawItemState.Selected) > 0)
                    {
                        textBrush = SystemBrushes.HighlightText;
                    }
                }
                e.Graphics.DrawString(strToShow, drawFont, textBrush, e.Bounds);
                e.DrawFocusRectangle();
            }
        }

        private void stateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Madhu fix for the bugzilla defect number 49
            //if (stateList.SelectedIndex > -1)
            if (_required)
            {
                if (stateList.SelectedIndex > 0)
                    _isValid = true;
            }
            else
                _isValid = true;


        }

        private void State_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == false)
                this.stateList.BackColor = Color.LightGray;
            else
                this.stateList.BackColor = Color.White;
        }


        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!DesignMode && this.BackColor != Color.Transparent)
                this.stateList.BackColor = this.BackColor;
        }

        public void dependentTextChanged(Object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            this.selectedValue = e.NewValue;
        }

    }

    /// <summary>
    /// Class to represent US state with 3 properties - one for the state code
    /// and the other for the full name. The third property is for indicating which
    /// state should be highlighted in a different color when shown in the combobox
    /// </summary>
    public class USState
    {
        private string myShortName;
        private string myLongName;
        private bool selected;

        public USState(string strLongName, string strShortName)
        {

            this.myShortName = strShortName;
            this.myLongName = strLongName;
            this.selected = false;
        }

        public string ShortName
        {
            get
            {
                return myShortName;
            }
        }

        public string LongName
        {

            get
            {
                return myLongName;
            }
        }

        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
            }

        }

    }

}
