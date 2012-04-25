using Common.Libraries.Forms.Components;
using System;
using System.Windows.Forms;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Retail
{
    public partial class OutTheDoor : CustomBaseForm
    {

        #region Private Fields
        private decimal _OutTheDoorPrice = 0.0m;
        #endregion

        #region Private Event Handler Methods
        private void continueButton_Click(object sender, EventArgs e)
        {
            if (txtOutTheDoorPrice.TextLength > 0)
            {
                bool parsed = false;
                decimal decOutTheDoor = 0.0m;
                parsed = Commons.FormatStringAsDecimal(txtOutTheDoorPrice.Text, out decOutTheDoor);
                if (parsed)
                {
                    this.OutTheDoorPrice = decOutTheDoor;
                    if (decOutTheDoor <= 0)
                    {
                        MessageBox.Show("Out the door price cannot be less than or equal to zero.");
                        return;
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RaiseOutTheDoorPriceChanged(decimal price)
        {
            if (OutTheDoorPriceChanged == null)
                return;
            OutTheDoorPriceChanged(this, new OutTheDoorPriceChangedEventArgs(price));
        }
        #endregion

        #region Constructors
        public OutTheDoor(decimal initialTotal)
        {
            InitializeComponent();
            InitialTotal = initialTotal;
            txtOutTheDoorPrice.Text = Math.Round(_OutTheDoorPrice, 2).ToString("f2");
        }
        #endregion

        #region Public Event Declarations
        public event EventHandler<OutTheDoorPriceChangedEventArgs> OutTheDoorPriceChanged;
        #endregion

        #region Public Properties
        private decimal InitialTotal { get; set; }
        public decimal OutTheDoorPrice
        {
            get 
            { 
                return _OutTheDoorPrice; 
            }
            set 
            { 
                _OutTheDoorPrice = value;
                RaiseOutTheDoorPriceChanged(_OutTheDoorPrice);
            }
        }
        #endregion

        #region Public Classes
        public class OutTheDoorPriceChangedEventArgs : EventArgs
        {
            public OutTheDoorPriceChangedEventArgs(decimal outTheDoorPrice)
            {
                OutTheDoorPrice = outTheDoorPrice;
            }

            public decimal OutTheDoorPrice { get; set; }
        }
        #endregion
    }
}
