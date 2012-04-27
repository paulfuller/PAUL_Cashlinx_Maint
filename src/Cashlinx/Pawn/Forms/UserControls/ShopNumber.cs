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
    public partial class ShopNumber : UserControl
    {
        public ShopNumber()
        {
            InitializeComponent();
        }

        public void Clear()
        {

        }

        public TextBox getShopNoTextObj()
        {
            return this.textBox1;
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

            //MessageBox.Show("OnLeave");
            
            base.OnLeave(e);


        }

        protected override void OnBackColorChanged(EventArgs e)
        {
        }

        private void TransferToList_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Changed " + sender.ToString());
        }
    }

}
