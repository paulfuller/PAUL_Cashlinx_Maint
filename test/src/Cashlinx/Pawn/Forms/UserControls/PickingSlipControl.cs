using System;
using System.Windows.Forms;

namespace Pawn.Forms.UserControls
{


    public partial class PickingSlipControl : UserControl
    {
        #region Private Fields
        private string _ItemLocation;
        private string _ItemLocationAisle;
        private string _ItemLocationShelf;
        private string _ItemDescription;
        private string _StringAmount;
        private decimal _Amount;
        private int _itemNumber;
        private int _aislePreviousLocationY;
        #endregion

        #region enums
        public enum EnumSlipControlProperties
        {
            enumItemLocation = 0,
            enumItemLocationAisle,
            enumItemLocationShelf,
            enumItemDescription,
            enumItemAmount,
            enumItemNumber,
            enumItemStringAmount
        }
        #endregion

        #region Delegates
        private delegate void delSetFieldValues(EnumSlipControlProperties enumItem);
        #endregion

        #region Events
        private event delSetFieldValues onPropertyValueChange;
        #endregion

        #region Public Properties
        public int ItemNumber
        {
            get
            {
                return _itemNumber;
            }
            set
            {
                _itemNumber = value;
                onPropertyValueChange(EnumSlipControlProperties.enumItemNumber);
            }
        }

        public string ItemLocation 
        { 
            get
            {
                return _ItemLocation;
            }
            set
            {
                _ItemLocation = value;
                onPropertyValueChange(EnumSlipControlProperties.enumItemLocation);
            }
        }

        public string ItemLocationAisle 
        { 
            get
            {
                return _ItemLocationAisle;   
            } 
            set
            {
                _ItemLocationAisle = value;
                onPropertyValueChange(EnumSlipControlProperties.enumItemLocationAisle);
            }
        }
        public string ItemLocationShelf
        {
            get
            {
                return _ItemLocationShelf;
            }
            set
            {
                _ItemLocationShelf = value;
                onPropertyValueChange(EnumSlipControlProperties.enumItemLocationShelf);
            }
        }

        public string ItemDescription
        {
            get
            {
                return _ItemDescription;
            }
            set
            {
                _ItemDescription = value;
                onPropertyValueChange(EnumSlipControlProperties.enumItemDescription);
            }
        }

        public string StringAmount
        {
            get
            {
                return _StringAmount;
            }
            set
            {
                _StringAmount = value;
                onPropertyValueChange(EnumSlipControlProperties.enumItemStringAmount);
            }
        }

        public decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;
                onPropertyValueChange(EnumSlipControlProperties.enumItemAmount);
            }
        }
        #endregion

        #region Private Events Handler Methods
        private void itemLocationAisleLabel_LocationChanged(object sender, EventArgs e)
        {
            if (itemLocationShelfLabel.Text != string.Empty
                && itemLocationShelfLabel.Text != "0")
            {
                itemLocationShelfLabel.Top = _aislePreviousLocationY;
            }
        }

        private void itemLocationAisleLabel_VisibleChanged(object sender, EventArgs e)
        {
            if (!itemLocationAisleLabel.Visible)
            {
                if (itemLocationShelfLabel.Text != string.Empty
                    && itemLocationShelfLabel.Text != "0")
                    itemLocationShelfLabel.Top = itemLocationAisleLabel.Top;
            }
        }

        private void itemLocationLabel_VisibleChanged(object sender, EventArgs e)
        {
            if (!itemLocationLabel.Visible)
            {
                if (itemLocationAisleLabel.Text != string.Empty && itemLocationAisleLabel.Text != "0")
                {
                    _aislePreviousLocationY = itemLocationAisleLabel.Top;
                    itemLocationAisleLabel.Top = itemLocationLabel.Top;
                }
                else if (itemLocationAisleLabel.Text == string.Empty || itemLocationAisleLabel.Text == "0"
                    && itemLocationShelfLabel.Text != string.Empty 
                    && itemLocationShelfLabel.Text != "0")
                {
                    itemLocationShelfLabel.Top = itemLocationLabel.Top;
                }
            }
        }

        private void PickingSlipControl_onPropertyValueChange(PickingSlipControl.EnumSlipControlProperties enumItem)
        {
            if (enumItem == EnumSlipControlProperties.enumItemLocation)
                itemLocationLabel.Text = ItemLocation;
            else if (enumItem == EnumSlipControlProperties.enumItemStringAmount)
                itemAmountLabel.Text = StringAmount;
            else if (enumItem == EnumSlipControlProperties.enumItemDescription)
                itemDescriptionLabel.Text = ItemDescription;
            else if (enumItem == EnumSlipControlProperties.enumItemLocationAisle)
                itemLocationAisleLabel.Text = ItemLocationAisle;
            else if (enumItem == EnumSlipControlProperties.enumItemLocationShelf)
                itemLocationShelfLabel.Text = ItemLocationShelf;
            else if (enumItem == EnumSlipControlProperties.enumItemNumber)
                itemNumberLabel.Text = "[" + ItemNumber + "]";
        }
        #endregion

        #region Private Methods
        public void SetLocationLabelsVisibility()
        {
            if (itemLocationLabel.Text == string.Empty || itemLocationLabel.Text == "0")
                itemLocationLabel.Visible = false;                

            if (itemLocationAisleLabel.Text == string.Empty || itemLocationAisleLabel.Text == "0")
                itemLocationAisleLabel.Visible = false;

            if (itemLocationShelfLabel.Text == string.Empty || itemLocationShelfLabel.Text == "0")
                itemLocationShelfLabel.Visible = false;

        }
        #endregion

        #region Constructors
        public PickingSlipControl()
        {
            this.onPropertyValueChange += new delSetFieldValues(PickingSlipControl_onPropertyValueChange);
            InitializeComponent();
            itemLocationLabel.VisibleChanged += new EventHandler(itemLocationLabel_VisibleChanged);
            itemLocationAisleLabel.VisibleChanged += new EventHandler(itemLocationAisleLabel_VisibleChanged);
            itemLocationAisleLabel.LocationChanged += new EventHandler(itemLocationAisleLabel_LocationChanged);
        }
        #endregion
    }
}
