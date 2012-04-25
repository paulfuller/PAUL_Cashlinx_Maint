/********************************************************************
* CashlinxDesktop.UserControls
* Country
* This user control will show a list of all countries 
* from the database
* Sreelatha Rengarajan 3/13/2009 Initial version
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
    public partial class Country : UserControl
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


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        public Country()
        {

            InitializeComponent();
            this.countryComboBox.DrawMode = DrawMode.OwnerDrawFixed;

        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {

                if (!this.DesignMode)
                {
                    DataTable countryTable = GlobalDataAccessor.Instance.DesktopSession.CountryTable;
                    if (countryTable.Rows.Count > 0)
                    {

                        ArrayList countries = new ArrayList();
                        foreach (DataRow dr in countryTable.Rows)
                        {
                            countries.Add(new CountryData(dr["country_code"].ToString(), dr["country_name"].ToString()));
                        }

                        //Madhu 12/07/2010 fix forbugzilla 10
                        countries.Insert(0, new CountryData("", "Select One"));

                        this.countryComboBox.DataSource = countries;
                        this.countryComboBox.DisplayMember = "Name";
                        this.countryComboBox.ValueMember = "Code";

                    }
                }

                //To do: There should be an application wide data structure that holds the country values
                //which will be used to populate the drop down in case the call to DB could not be completed or
                //the call did not yield any rows


            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException(Commons.GetMessageString("CountryDataFetchError"), ex);
            }


            base.OnLoad(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (!DesignMode)
            {
                if (_required)
                {
                    //Madhu fix for the bugzilla defect number 49
                    //if (this.countryComboBox.SelectedItem != null)
                    if (this.countryComboBox.SelectedItem != null && countryComboBox.SelectedIndex > 0)
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
            }

        }



        protected override void OnLeave(EventArgs e)
        {
            if (_required)
            {
                //Madhu fix for the bugzilla defect number 49
                //if (this.countryComboBox.SelectedItem != null)
                if (this.countryComboBox.SelectedItem != null && countryComboBox.SelectedIndex > 0)
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

            var rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 1, this.Bounds.Width + 2, this.Bounds.Height + 1);
            Commons.RemoveBorder(this, rect);

            base.OnLeave(e);


        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!DesignMode && this.BackColor != Color.Transparent)
                this.countryComboBox.BackColor = this.BackColor;
        }

        protected override void OnEnter(EventArgs e)
        {
            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 1, this.Bounds.Width + 2, this.Bounds.Height + 1);
            Commons.CustomPaint(this, rect);
        }


        private void countryComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            else
            {
                e.DrawBackground();
                Brush textBrush = SystemBrushes.ControlText;
                Font drawFont = e.Font;
                CountryData eCountry = (CountryData)countryComboBox.Items[e.Index];
                string strToShow = eCountry.Name.ToString();
                if (eCountry.Selected)
                {
                    textBrush = Brushes.Red;
                    if ((e.State & DrawItemState.Selected) > 0)
                    {
                        drawFont = new Font(drawFont.FontFamily, drawFont.Size, FontStyle.Bold);                        
                    }

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

        private void countryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Madhu fix for the bugzilla defect number 49
            //if (countryComboBox.SelectedIndex > -1)
            if (_required)
            {
                if (countryComboBox.SelectedIndex > 0)
                    _isValid = true;
            }
        }

        private void Country_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == false)
                this.countryComboBox.BackColor = Color.LightGray;
            else
                this.countryComboBox.BackColor = Color.White;
        }




    }

    /// <summary>
    /// Class to represent Country with 2 properties - one for the code
    /// and the other for the name
    /// </summary>
    public class CountryData
    {
        private string _Code;
        private string _Name;
        private bool _selected;

        public CountryData(string strCode, string strName)
        {

            this._Code = strCode;
            this._Name = strName;
            this._selected = false;
        }

        public string Code
        {
            get
            {
                return _Code;
            }
        }

        public string Name
        {

            get
            {
                return _Name;
            }
        }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }

    }

}
