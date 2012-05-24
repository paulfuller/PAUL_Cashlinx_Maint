/********************************************************************
* CustomValidation
* ValidationManager
* This component keeps track of all the validators of type Basevalidators
* in the containing control
* Sreelatha Rengarajan 3/19/2009 Initial version
*******************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Common.Libraries.Forms.Validation
{
    public partial class ValidationManager : Component
    {
 
        public ValidationManager()
        {
            InitializeComponent();
  
        }

        public ValidationManager(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            Register(container);
            
        }
        private List<String> _errMessage = new List<string>();
        private List<BaseValidator> _validators = new List<BaseValidator>();

        
        /// <summary>
        /// Find all the validators in the container where the validation manager is
        /// added and add it to a collection
        /// </summary>
        /// <param name="container"></param>
        private void Register(IContainer container)
        {

            try
            {
                foreach (BaseValidator bValidator in container.Components.OfType<BaseValidator>())
                {
                    // Add this component to the list of registered validators for this container
                    //that need to be checked
                    _validators.Add(bValidator);
                    bValidator.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Go through each validator in the collection and check if it
        /// is valid. If it is not, add the error message to a error list
        /// Open the validation summary form and assign the errors to its listbox
        /// </summary>
        public void checkValidators()
        {
            int i = 0;
            _errMessage.Clear();
            foreach (BaseValidator vr in _validators)
                if (!(vr.IsValid))
                {
                    _errMessage.Add(vr.ErrorMessage);
                    i++;
                }

            if (_errMessage.Count > 0)
            {
                ValidationSummary valsummary = new ValidationSummary();
                valsummary.setErrors(_errMessage.ToArray());
                valsummary.Show();
            }
        }


    }
}
