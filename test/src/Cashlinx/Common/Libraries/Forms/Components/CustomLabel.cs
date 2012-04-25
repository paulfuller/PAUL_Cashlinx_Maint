/********************************************************************
* CustomFormElements
* CustomLabel
* Custom Label derived from Label
* Sreelatha Rengarajan 6/11/2009 Initial version
*******************************************************************/

using System.ComponentModel;
using System.Windows.Forms;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Forms.Components
{
    /// <summary>
    /// custom label derived from label
    /// </summary>
    public partial class CustomLabel : Label
    {
        private bool _required = false;



        /// <summary>
        /// Property to specify if this is a required field 
        /// and if it is, a red asterisk is shown next to the label
        /// </summary>
        [Category("Type")]
        [Description("Sets whether the field is required")]
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

        /// <summary>
        /// default constructor
        /// </summary>
        public CustomLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// overridden method that adds a red asterisk depending
        /// on whether it is a required field or not.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!DesignMode)
            {
                Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                if (_required)
                {
                    Commons.DrawAsterisk(this, this.Bounds.X + this.Bounds.Width + 2, this.Bounds.Y + 2);
                }
                else
                    Commons.RemoveAsterisk(this, this.Bounds.X + this.Bounds.Width + 2, this.Bounds.Y + 2);
            }


            base.OnPaint(e);
        }

    
        

    }


}
