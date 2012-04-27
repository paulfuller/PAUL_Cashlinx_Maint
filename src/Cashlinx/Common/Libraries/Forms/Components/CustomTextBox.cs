/********************************************************************
* CustomFormElements
* CustomTextBox
* Custom textbox derived from textbox
* Sreelatha Rengarajan 3/23/2009 Initial version
* Madhu 11/16/2010 fix for the defect PWNU00001448
*******************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using Common.Libraries.Forms.Components.Behaviors;
using Common.Libraries.Utility.Exception;
using MessageBox = System.Windows.Forms.MessageBox;
using SystemColors = System.Drawing.SystemColors;

namespace Common.Libraries.Forms.Components
{
    /// <summary>
    /// Custom Textbox class derived from Textbox
    /// </summary>
    public partial class CustomTextBox : TextBox
    {

        private bool _required;
        private bool _isValid;
        private bool _firstLetterUppercase;
        private string _validationExpression = "";
        private bool _regularExpression;
        private string _errorMessage = "";
        private bool _formatPhoneNumber;
        private bool _formatFullPhoneNumber;
        private bool _onlyNumbers;
        private bool _onlyDecimals;
        private bool nonNumberEntered = false;
        private bool backspaceEntered = false;

        public void dependentTextChanged(Object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            this.Text = e.NewValue;
        }


        /// <summary>
        /// Property to specify if this is a required field wherein text has to be entered
        /// for it to be valid.
        /// </summary>
        [Category("Type")]
        [Description("Sets whether the Textbox is required")]
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

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                PreviousValue = value;
                base.Text = value;
            }
        }

        /// <summary>
        /// Property to specify that when text is entered in the textbox the first
        /// character has to be changed to upper case.
        /// </summary>
        [Category("Type")]
        [Description("Sets whether the first letter entered in the textbox should be upper case")]
        [DefaultValue(false)]
        public bool FirstLetterUppercase
        {
            get
            {
                return _firstLetterUppercase;
            }
            set
            {
                _firstLetterUppercase = value;
            }
        }

        /// <summary>
        /// Property to specify that the textbox should only accept numbers
        /// </summary>
        [Category("Type")]
        [Description("Sets whether the textbox should allow only numbers")]
        [DefaultValue(false)]
        public bool AllowOnlyNumbers
        {
            get
            {
                return _onlyNumbers;
            }
            set
            {
                _onlyNumbers = value;
            }
        }

        /// <summary>
        /// Property to specify that the textbox should accept decimal numbers
        /// </summary>
        [Category("Type")]
        [Description("Sets whether the textbox should allow decimal numbers")]
        [DefaultValue(false)]
        public bool AllowDecimalNumbers
        {
            get
            {
                return _onlyDecimals;
            }
            set
            {
                _onlyDecimals = value;
            }
        }


        /// <summary>
        /// Property to indicate if the text entered should be formatted as a phone number with
        /// a hyphen after the 3rd character
        /// </summary>
        [Category("Type")]
        [Description("Sets whether the text should be formatted as a phone number with a - after the 3rd digit")]
        [DefaultValue(false)]
        public bool FormatAsPhone
        {
            get
            {
                return _formatPhoneNumber;
            }
            set
            {
                _formatPhoneNumber = value;
            }
        }

        /// <summary>
        /// Property to indicate if the text entered should be formatted as a phone number with
        /// a hyphen after the 3rd character
        /// </summary>
        [Category("Type")]
        [Description("Sets whether the text should be formatted as a phone number with a - after the 3rd digit")]
        [DefaultValue(false)]
        public bool FormatAsFullPhone
        {
            get
            {
                return _formatFullPhoneNumber;
            }
            set
            {
                _formatFullPhoneNumber = value;
            }
        }

        /// <summary>
        /// Property to indicate if the textbox should be validated against a regular expression
        /// </summary>
        [Category("Type")]
        [Description("Sets whether regular expression validation should be done on the text entered")]
        [DefaultValue(false)]
        public bool RegularExpression
        {
            get
            {
                return _regularExpression;
            }
            set
            {
                _regularExpression = value;
            }
        }

        /// <summary>
        /// Internally set field to indicate if the control passed validations
        /// </summary>
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
        }

        /// <summary>
        /// The regular expression to validate the data in the text against
        /// </summary>
        [Category("Type")]
        [Description("Sets the regular expression validation to validate")]
        public string ValidationExpression
        {
            get
            {
                return _validationExpression;
            }
            set
            {
                _validationExpression = value;
            }
        }

        /// <summary>
        /// Error message to show when regular expression validation fails
        /// </summary>
        [Category("Behavior")]
        [Description("Sets the Error Message to show when regular expression validation fails")]
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
            }
        }

        public string PreviousValue { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomTextBox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        /// <summary>
        /// overridden method that sets the isvalid property if data is entered based
        /// on whether it is a required field or not.
        /// </summary>
        /// <param name="levent"></param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (!DesignMode)
            {
                if (_required)
                {
                    _isValid = base.Text.Trim().Length > 0;
                }
            }

        }

 
        /// <summary>
        /// Overridden method that draws a border around the textbox when it is in focus
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(System.EventArgs e)
        {
            //Draw border
            DrawBorder();
            //base.OnEnter(e);

        }

        private void DrawBorder()
        {

                IntPtr hwnd = new IntPtr();
                hwnd = Parent.Handle;
                Graphics g = Graphics.FromHwndInternal(hwnd);
                Pen p = new Pen(new SolidBrush(Color.DarkBlue), 1);
                g.DrawRectangle(p, Bounds.X - 2, Bounds.Y - 2, Bounds.Width + 2, Bounds.Height + 2);
                g.Dispose();
                p.Dispose();
  
        }

 



        /// <summary>
        /// Overridden leave method
        /// If required is set to true then checks to see if data is entered. If yes, set isvalid to true else false
        /// If Regularexpression is true and validationexpressionvalue is specified, validation is done on the data entered against the expression
        /// and isvalid is set to true or false. If false, the string entered in Errormessage property is shown
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(System.EventArgs e)
        {
            try
            {
                if (_required)
                {
                    _isValid = base.Text.Trim().Length > 0;
                }
                if (RegularExpression && _validationExpression.Length > 0)
                {
                    if (base.Text.Trim().Length > 0)
                    {
                        if (Regex.IsMatch(base.Text, _validationExpression))
                            _isValid = true;
                        else
                        {
                            //this.Invalidate();
                            _isValid = false;
                            MessageBox.Show(ErrorMessage);
                            this.Focus();
                        }
                    }
                }
            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException("Error validating custom textbox on leave event", ex);
            }
            RemoveBorder();
            base.OnLeave(e);
            PreviousValue = Text;
        }

        private void RemoveBorder()
        {
            IntPtr hwnd = new IntPtr();
            hwnd = this.Parent.Handle;
            Graphics g = Graphics.FromHwndInternal(hwnd);
            Pen p = new Pen(new SolidBrush(Color.White), 1);
            g.DrawRectangle(p, this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 2, this.Bounds.Height + 2);
            g.Dispose();
            p.Dispose();

        }



        /// <summary>
        /// Makes sure that the user's cursor remains in the first position if there
        /// are no letters entered in the textbox or when blanks are entered
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (base.Text.Trim().Length == 0)
            {
                //if (this.SelectionStart != 0)
                //this.SelectionStart = 0;//commented out by Drew
            }
            base.OnMouseClick(e);
        }

        /// <summary>
        /// Overridden TextChanged method. If it is a required field, check is made
        /// and isvalid is set to true or false.
        /// If firstLetterUppercase is set to true for the field, the first letter is converted
        /// If FormatAsPhone is set to true, a - is added after the 3rd character of the text entered
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            if (DesignMode) return;
            if (_required)
            {
                _isValid = base.Text.Trim().Length > 0;
            }
            else
                _isValid = true;
            if (_firstLetterUppercase)
            {
                Regex r = new Regex(@"\b(\w)(\w+)?\b", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                MatchEvaluator ME = new MatchEvaluator(ReplaceM);
                if (base.Text.Length > 0)
                {
                    string m_string = base.Text.Substring(0, 1).ToLower();
                    m_string = r.Replace(m_string, ME);
                    base.Text = m_string + base.Text.Substring(1, base.Text.Length - 1);
                    if (base.SelectionStart == 0)
                        base.SelectionStart = 1;
                }
            }

            base.OnTextChanged(e);
        }

        /// <summary>
        ///  Handle the KeyDown event to determine the type of character entered into the textbox.
        /// </summary>
        /// <param name="e"></param>

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (AllowOnlyNumbers == false && AllowDecimalNumbers == false) return;
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                //Key stroke was not a number from the top of the keyboard
                
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Keystroke is not a number from the keypad.
                    
                    if (e.KeyCode != Keys.Back)
                    {
                        // keystroke is not a backspace.
                        if (AllowDecimalNumbers)
                        {
                            //If its not the period or the decimal key then do not accept it
                            if (e.KeyValue != 190 && e.KeyCode != Keys.Decimal)
                            nonNumberEntered = true;
                        }

                        if (AllowOnlyNumbers)
                            // A non-numerical keystroke was pressed.
                            // Set the flag to true and evaluate in KeyPress event.
                            nonNumberEntered = true;
                    }
                    else
                        backspaceEntered = true;
                }
            }
            //If shift key was pressed, it's not a number.
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
            // Initialize the flag to false.
        }




        /// <summary>
        ///    This event occurs after the KeyDown event and can be used to prevent
        ///    characters from entering the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (AllowOnlyNumbers || AllowDecimalNumbers)
            {
                // Check for the flag being set in the KeyDown event.
                if (nonNumberEntered == true)
                {
                    // Stop the character from being entered into the control since it is non-numerical.
                    e.Handled = true;
                }
            }

            if (!FormatAsPhone && !FormatAsFullPhone) return;

            if (FormatAsPhone)
            {
                if (base.Text.Length > 0)
                {
                    if (base.Text.Length == 3 && !backspaceEntered)
                    {
                        base.Text = base.Text + "-";
                        base.SelectionStart = base.Text.Length;
                    }
                }
                else
                    backspaceEntered = false;
            }
            else if (FormatAsFullPhone)
            {
                //Madhu 11/16/2010 fix for the defect PWNU00001448
                if (base.Text.Length > 0)
                {
                    if (base.Text.Length == 3 && !backspaceEntered)
                    {
                        base.Text = base.Text + "-";
                        base.SelectionStart = base.Text.Length;
                    }
                    else if (base.Text.Length == 7 && !backspaceEntered)
                    {
                        base.Text = base.Text + "-";
                        base.SelectionStart = base.Text.Length;
                    }
                }
                else
                    backspaceEntered = false;
            }
        }

        private string ReplaceM(Match m)
        {
            string FirstLetter;
            FirstLetter = m.Groups[1].Value.ToUpper();
            return FirstLetter;
        }

        /// <summary>
        /// Since this is a custom textbox derived from textbox we need
        /// to change the back color programatically if the control was disabled on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
   
        private void CustomTextBox_EnabledChanged(object sender, System.EventArgs e)
        {
            if (this.DesignMode) return;
            BackColor = Enabled == false ? SystemColors.Control : Color.White;
        }

        private void CustomTextBox_ReadOnlyChanged(object sender, System.EventArgs e)
        {
            if (DesignMode) return;
            if (ReadOnly == true)
                this.BackColor = Color.White;
        }

        public void RevertValue()
        {
            ControlHighlighter ch = new ControlHighlighter(this, Color.LightPink);
            ch.Execute();
            this.Text = this.PreviousValue;
        }
    }

}
