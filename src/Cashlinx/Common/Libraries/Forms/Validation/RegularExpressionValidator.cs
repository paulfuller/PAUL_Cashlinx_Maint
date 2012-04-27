using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Common.Libraries.Forms.Validation
{
    public partial class RegularExpressionValidator : BaseValidator
    {
        public RegularExpressionValidator()
        {
            InitializeComponent();
        }

        public RegularExpressionValidator(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        private string _validationExpression = "";

        [Category("Behavior")]
        [Description("Sets or returns the regular expression that determines the pattern used to validate a field.")]
        [DefaultValue("")]
        public string ValidationExpression
        {
            get { return _validationExpression; }
            set { _validationExpression = value; }
        }

        protected override bool EvaluateIsValid()
        {
            // Don't validate if empty
            if (ControlToValidate.Text.Trim() == "") return true;
            // Successful if match matches the entire text of ControlToValidate
            string input = ControlToValidate.Text.Trim();
            return Regex.IsMatch(input, _validationExpression.Trim());
        }
    }




}
