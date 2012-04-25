/********************************************************************
* CustomValidation
* ValidationSummary
* This form will be shown if there are errors in a submitted form
* Sreelatha Rengarajan 3/17/2009 Initial version
*******************************************************************/

using System;
using System.Windows.Forms;

namespace Common.Libraries.Forms.Validation
{
    public partial class ValidationSummary : Form
    {
        public ValidationSummary()
        {
            InitializeComponent();
            //When the form is initialized, the error list box is cleared
            this.listBox_errors.Items.Clear();
        }

        public void setErrors(string[] strError)
        {
            
            if (strError.GetLength(0) > 0)
            {
                this.listBox_errors.Items.AddRange(strError);
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
           
        }
    }
}
