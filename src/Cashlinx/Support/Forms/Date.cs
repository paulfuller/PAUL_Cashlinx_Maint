/********************************************************************
* CashlinxDesktop.UserControls
* Date
* This user control will allow user to enter a date in mm/dd/yyyy format
* Sreelatha Rengarajan 4/29/2009 Initial version
* SR 6/1/2010 Added logic to change the backcolor* 
*******************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Common.Libraries.Utility.Shared;

namespace Support.Forms
{
    public partial class Date : UserControl
    {

        private bool _required;
        private bool _isValid;
        private string _errorMessage = "";
        private string _validationExpression = "";
        private bool nonNumberEntered = false;

        private bool backspacePressed = false;

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

        [Category("Behavior")]
        [Description("Sets the Error Message to show when validation fails")]
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public HorizontalAlignment TextAlign
        {
            get { return dateTextBox.TextAlign; }
            set { dateTextBox.TextAlign = value; }
        }
        
        [Category("Type")]
        [Description("Sets the regular expression validation to validate the date of birth")]
        public string ValidationExpression
        {
            get { return _validationExpression; }
            set { _validationExpression = value; }
        }

        [Browsable(false)]
        public TextBox DateTextBox
        {
            get { return dateTextBox; }
        }

        public event EventHandler TextBoxTextChanged;

        public Date()
        {
            InitializeComponent();


        }

        protected override void OnEnter(EventArgs e)
        {
            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 3, this.Bounds.Height + 3);
            Commons.CustomPaint(this, rect);
            if (this.dateTextBox.Text == "mm/dd/yyyy")
                this.dateTextBox.Text = "";
            base.OnEnter(e);

        }

        protected override void OnGotFocus(EventArgs e)
        {
            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 3, this.Bounds.Height + 3);
            Commons.CustomPaint(this, rect);
            if (this.dateTextBox.Text == "mm/dd/yyyy")
                this.dateTextBox.Text = "";
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (_required)
            {
                if (!(this.dateTextBox.Text.Equals("mm/dd/yyyy")))
                {
                    if (Regex.IsMatch(dateTextBox.Text, _validationExpression))
                    {
                        _isValid = true;
                    }
                }
                else
                {
                    _isValid = false;
                }

            }
            if (this.dateTextBox.Text == "")
                this.dateTextBox.Text = "mm/dd/yyyy";
            //base.OnLayout(e);
        }


        protected override void OnLeave(EventArgs e)
        {
            CheckValid();
            base.OnLeave(e);
            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 3, this.Bounds.Height + 3);
            Commons.RemoveBorder(this, rect);
            

        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!DesignMode && this.BackColor != Color.Transparent)
                this.dateTextBox.BackColor = this.BackColor;
        }



        private void CheckValid()
        {
            if (this.dateTextBox.Text.Trim().Length > 0)
            {
                if (!(this.dateTextBox.Text.Equals("mm/dd/yyyy")))
                {
                    if (Regex.IsMatch(dateTextBox.Text, _validationExpression))
                    {
                        _isValid = true;
                    }
                    else
                    {
                        _isValid = false;
                        MessageBox.Show(ErrorMessage);
                        this.Focus();
                    }
                }
                else
                    this.dateTextBox.Text = "mm/dd/yyyy";
            }
            else
                this.dateTextBox.Text = "mm/dd/yyyy";

        }

        private void dateTextBox_TextChanged(object sender, EventArgs e)
        {
   
            if (this.dateTextBox.Text.Length == 10)
            {
                CheckValid();
            }

            if (TextBoxTextChanged != null)
            {
                TextBoxTextChanged(sender, e);
            }
        }


        private void dateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
            else
            {
                if (this.dateTextBox.Text.Length > 0)
                {
                    //insert slashes
                    if (this.dateTextBox.Text.Length == 2 && !backspacePressed)
                    {
                        this.dateTextBox.Text = this.dateTextBox.Text.Substring(0, 2) + "/";
                        this.dateTextBox.SelectionStart = this.dateTextBox.Text.Length;
                    }
                    if (this.dateTextBox.Text.Length == 5 && !backspacePressed)
                    {
                        this.dateTextBox.Text = this.dateTextBox.Text.Substring(0, 5) + "/";
                        this.dateTextBox.SelectionStart = this.dateTextBox.Text.Length;
                    }

                }
                else
                    backspacePressed = false;
            }

        }

        private void dateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad.
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumberEntered = true;
                        backspacePressed = false;
                    }
                    else
                        backspacePressed = true;
                }
            }
            else
                backspacePressed = false;
            //If shift key was pressed, it's not a number.
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
            base.OnKeyDown(e);

        }

 


    }
}
