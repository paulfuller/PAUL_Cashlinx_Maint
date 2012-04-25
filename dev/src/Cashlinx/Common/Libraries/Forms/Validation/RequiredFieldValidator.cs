using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Common.Libraries.Forms.Validation
{
    public partial class RequiredFieldValidator : BaseValidator
    {
        public RequiredFieldValidator()
        {
            InitializeComponent();
        }

        public RequiredFieldValidator(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        private string _initialValue = null;


        private void ControlToValidate_Validating(object sender, CancelEventArgs e)
        {
            // We don't cancel if invalid since we don't want to force
            // the focus to remain on ControlToValidate if invalid
            Validate();
        }
        protected override bool EvaluateIsValid()
        {
            string controlValue = ControlToValidate.Text.Trim();
            string initialValue;
            if (_initialValue == null) initialValue = "";
            else initialValue = _initialValue.Trim();
            return (controlValue != initialValue);
        }
    }

    public class ValidatableControlConverter : ReferenceConverter
    {
        public ValidatableControlConverter(Type type) : base(type) { }
        protected override bool IsValueAllowed(ITypeDescriptorContext context, object value)
        {
            return ((value is TextBox) ||
              (value is ListBox) ||
              (value is ComboBox) ||
              (value is UserControl) ||
              (value is MaskedTextBox));
        }
    }



}
