/********************************************************************
* CashlinxDesktop.UserControls
* Hearaboutus
* This user control can be used in a form to show list of selections for
 * how the Customer heard about us and the selections are shown from the
 * from the database
* Sreelatha Rengarajan 5/14/2009 Initial version
*******************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Support.Forms.Pawn.Customer;
using System.Collections;

namespace Support.Forms.UserControls
{
    public partial class HearAboutUs : UserControl
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

        public HearAboutUs()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    DataTable dtHearAboutUs = GlobalDataAccessor.Instance.DesktopSession.HearAboutUsTable;
                    ArrayList hearAboutUsTypes = new ArrayList();
                    if (dtHearAboutUs != null && dtHearAboutUs.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtHearAboutUs.Rows)
                        {
                            hearAboutUsTypes.Add(new ComboBoxData(dr["code"].ToString(), dr["codedesc"].ToString()));
                        }
                    }
                    else //call to db did not yield data...populate with static data
                    {

                        hearAboutUsTypes.Add(new ComboBoxData("MA", "Mail"));
                        hearAboutUsTypes.Add(new ComboBoxData("RA", "Radio"));
                        hearAboutUsTypes.Add(new ComboBoxData("TV", "TV"));
                        hearAboutUsTypes.Add(new ComboBoxData("YP", "Yellow Pages"));
                        hearAboutUsTypes.Add(new ComboBoxData("SS", "Store Signs"));
                        hearAboutUsTypes.Add(new ComboBoxData("OT", "Other"));
                    }
                    hearAboutUsTypes.Insert(0, new ComboBoxData("", "Select"));
                    this.comboBoxHearAbtUs.DataSource = hearAboutUsTypes;
                    this.comboBoxHearAbtUs.DisplayMember = "Description";
                    this.comboBoxHearAbtUs.ValueMember = "Code";
                }

            }
            catch (SystemException Ex)
            {
                //To do: Do we want to fill up the combobox with static data when an exception occurs?
                BasicExceptionHandler.Instance.AddException("Could not fetch Hear about us data from session instance - ", Ex);

            }

            base.OnLoad(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (_required)
            {
                if (this.comboBoxHearAbtUs.Text != "Select")
                {
                    _isValid = true;
                }
                else
                    _isValid = false;
            }

            //base.OnLayout(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_required)
                Commons.DrawAsterisk(this, this.Bounds.X + this.Bounds.Width + 2, this.Bounds.Y + 3);
        }

        protected override void OnEnter(EventArgs e)
        {
            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 3, this.Bounds.Height + 3);
            Commons.CustomPaint(this, rect);
        }


        protected override void OnLeave(EventArgs e)
        {
            if (_required)
                if (this.comboBoxHearAbtUs.SelectedItem != null && this.comboBoxHearAbtUs.Text != "Select")
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

    }
}
