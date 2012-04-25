/********************************************************************
* CashlinxDesktop.UserControls
* Social Security Number
* This user control can be used in a form to allow the user to enter
* a social security number. Allows only 9 digits
* Sreelatha Rengarajan 7/1/2009 Initial version
*******************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Utility.Shared;

namespace Support.Forms
{
    public partial class SocialSecurityNumber : UserControl
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

        public SocialSecurityNumber()
        {
            InitializeComponent();
            this.ssnTextBox.ErrorMessage = Commons.GetMessageString("SSNError");
        }

        protected override void OnEnter(EventArgs e)
        {
            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 2, this.Bounds.Height + 2);
            Commons.CustomPaint(this, rect);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {


            if (this.ssnTextBox.isValid)
            {
                _isValid = true;
            }
            else
            {

                _isValid = false;
            }



        }


        protected override void OnLeave(EventArgs e)
        {

            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 2, this.Bounds.Height + 2);
            Commons.RemoveBorder(this, rect);
            var ssn = new Common.Libraries.Objects.Customer.SocialSecurityNumber(this.ssnTextBox.Text);

            if (_required && string.IsNullOrWhiteSpace(ssn.OriginalValue))
            {
                MessageBox.Show(this.ssnTextBox.ErrorMessage);
                isValid = false;
            }
            else if (!string.IsNullOrWhiteSpace(ssn.OriginalValue) && !ssn.IsValid)
            {
                MessageBox.Show(Commons.GetMessageString("InvalidSSN"));
                _isValid = false;
            }
            else
            {
                _isValid = true;
            }
        }

    }
}
