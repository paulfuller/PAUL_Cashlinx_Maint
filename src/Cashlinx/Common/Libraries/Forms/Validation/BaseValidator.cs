using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Common.Libraries.Forms.Validation
{
    public abstract partial class BaseValidator : Component
    {
        public BaseValidator()
        {
            InitializeComponent();
        }

        public BaseValidator(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
 
        private Control _controlToValidate = null;
        private string _errorMessage = "";
        private Icon _icon = new Icon(typeof(ErrorProvider), "Error.ico");
        private ErrorProvider _errorProvider = new ErrorProvider();
        private bool _isValid = false;
        private bool _validateOnLoad = false;
  

        [Category("Appearance")]
        [Description("Sets or returns the text for the error message.")]
        [DefaultValue("")]
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        [Category("Appearance")]
        [Description("Sets or returns the Icon to display ErrorMessage.")]
        public Icon Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        [Category("Behavior")]
        [Description("Sets or returns the input control to validate.")]
        [DefaultValue(null)]
        [TypeConverter(typeof(ValidatableControlConverter))]
        public Control ControlToValidate
        {
            get { return _controlToValidate; }
            set
            {
                _controlToValidate = value;
                // Hook up ControlToValidate’s Validating event at run-time ie not from VS.NET
                if ((_controlToValidate != null) && (!DesignMode))
                {
                    _controlToValidate.Validating += new CancelEventHandler(ControlToValidate_Validating);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        [Category("Behavior")]
        [Description("Sets or returns whether this validator will validate itself when its host form loads.")]
        [DefaultValue(false)]
        public bool ValidateOnLoad
        {
            get { return _validateOnLoad; }
            set { _validateOnLoad = value; }
        }

        public void Validate()
        {

            // Validate control
            _isValid = this.EvaluateIsValid();
            // Display an error if ControlToValidate is invalid
            string errorMessage = "";
            if (!_isValid)
            {
                errorMessage = _errorMessage;
                _errorProvider.Icon = _icon;
            }
            _errorProvider.SetError(_controlToValidate, errorMessage);
            OnValidated(new EventArgs());
        }

        public override string ToString()
        {
            return _errorMessage;
        }

   

        public event EventHandler Validated;
        protected void OnValidated(EventArgs e)
        {
            if (Validated != null)
            {
                Validated(this, e);
            }
        }

        protected abstract bool EvaluateIsValid();

        private void ControlToValidate_Validating(object sender, CancelEventArgs e)
        {
            // We don't cancel if invalid since we don't want to force
            // the focus to remain on ControlToValidate if invalid
            Validate();
        }

  
}

}
