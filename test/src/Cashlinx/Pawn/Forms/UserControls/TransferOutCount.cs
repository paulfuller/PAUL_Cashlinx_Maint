/********************************************************************
* CashlinxDesktop.UserControls
* Race
* This user control can be used in a form to show list of valid values for race
* from the database and allow selection
* Sreelatha Rengarajan 4/5/2009 Initial version
* SR 6/1/2010 Added logic for changing back color
*******************************************************************/

using System;
using System.Windows.Forms;

namespace Pawn.Forms.UserControls
{
    public partial class TransferOutCount : UserControl
    {
        public TransferOutCount()
        {
            InitializeComponent();
        }

        private const String _DEFAULT_TRANSFER_TYPE_LABEL = "Counts";

        #region Public Properties

        //public TextBox BeginCountTextBox
        //{
        //    get
        //    {
        //        return this.txtBeginCount;
        //    } 
        //}
        //public TextBox ProcessedCountTextBox
        //{
        //    get
        //    {
        //        return this.txtProcessedCount;
        //    } 
        //}
        //public TextBox RemainingCountTextBox
        //{
        //    get
        //    {
        //        return this.txtRemainingCount;
        //    } 
        //}

        public String TransferTypeLabel
        {
            get { return this.label10.Text; }
            set
            {
                this.label10.Text = value;
                // Use default value if being set to null or empty string.
                if (String.IsNullOrEmpty(this.label10.Text) || String.IsNullOrEmpty(this.label10.Text.Trim()))
                {
                    this.label10.Text = _DEFAULT_TRANSFER_TYPE_LABEL;
                }
                // Ensure last character is a colon.
                if (this.label10.Text.Substring(this.label10.Text.Length - 1, 1) != ":")
                {
                    this.label10.Text += ":";
                }
            }
        }

        #endregion Public Properties

        public void Clear()
        {
            this.txtBeginCount.Text = String.Empty;
            this.txtProcessedCount.Text = String.Empty;
            this.txtRemainingCount.Text = String.Empty;
        }

        /// <summary>
        /// Sets the text box counts of this control.
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="processed"></param>
        public void SetCounts(int begin, int processed)
        {
            this.txtBeginCount.Text = begin.ToString();
            this.txtProcessedCount.Text = processed.ToString();
            this.txtRemainingCount.Text = (begin - processed).ToString();
        }

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            //base.OnLayout(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            //try
            //{
            //    Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 3, this.Bounds.Height + 3);
            //    Common.CustomPaint(this, rect);
            //}
            //catch (SystemException ex)
            //{
            //    BasicExceptionHandler.Instance.AddException("Error calling the paint method to draw border", ex);
            //}
        }


        protected override void OnLeave(EventArgs e)
        {
            //if (_required)
            //    if (this.transferList.SelectedItem != null && this.transferList.Text != "Select")
            //    {
            //        _isValid = true;
            //    }
            //    else
            //    {
            //        _isValid = false;
            //    }

            //Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 3, this.Bounds.Height + 3);
            //Common.RemoveBorder(this, rect);
            
            base.OnLeave(e);


        }

        protected override void OnBackColorChanged(EventArgs e)
        {
        }

        private void TransferOutCount_Load(object sender, EventArgs e)
        {
            this.txtBeginCount.ReadOnly = true;
            this.txtProcessedCount.ReadOnly = true;
            this.txtRemainingCount.ReadOnly = true;
        }

    }

}
