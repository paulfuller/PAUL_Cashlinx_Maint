using System.Windows.Forms;

namespace Pawn.Forms.UserControls
{
    public partial class PickSlipPrintControl : UserControl
    {
        #region Private Fields
        private string _ItemLocationOther;
        private string _ItemLocationAisle;
        private string _ItemLocationShelf;
        private string _ItemDescription;
        private string _gunNumber;
        private int _itemNumber;
        #endregion

        #region enums
        public enum EnumPickSlipPrintControlProperties
        {
            enumItemLocationOther = 0,
            enumItemLocationAisle,
            enumItemLocationShelf,
            enumItemDescription,
            enumItemNumber,
            enumGunNumber
        }
        #endregion

        #region Delegates
        private delegate void delegateSetFieldValues(EnumPickSlipPrintControlProperties enumItem);
        #endregion

        #region Events
        private event delegateSetFieldValues onPropertyValueChange;
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
                onPropertyValueChange(EnumPickSlipPrintControlProperties.enumItemNumber);
            }
        }

        public string ItemLocationOther
        {
            get
            {
                return _ItemLocationOther;
            }
            set
            {
                _ItemLocationOther = value;
                onPropertyValueChange(EnumPickSlipPrintControlProperties.enumItemLocationOther);
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
                onPropertyValueChange(EnumPickSlipPrintControlProperties.enumItemLocationAisle);
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
                onPropertyValueChange(EnumPickSlipPrintControlProperties.enumItemLocationShelf);
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
                onPropertyValueChange(EnumPickSlipPrintControlProperties.enumItemDescription);
            }
        }

        public string GunNumber
        {
            get
            {
                return _gunNumber;
            }
            set
            {
                _gunNumber = value;
                onPropertyValueChange(EnumPickSlipPrintControlProperties.enumGunNumber);
            }
        }
        #endregion

        #region Private Event Handler Methods
        private void PickSlipPrintControl_onPropertyValueChange(PickSlipPrintControl.EnumPickSlipPrintControlProperties enumItem)
        {
            if (enumItem == EnumPickSlipPrintControlProperties.enumItemLocationOther)
            {
                itemLocationOtherLabel.Text = ItemLocationOther;
            }
            else if (enumItem == EnumPickSlipPrintControlProperties.enumItemDescription)
            {
                itemDescriptionLabel.Text = ItemDescription;
                this.Height = itemDescriptionLabel.Height;
            }
            else if (enumItem == EnumPickSlipPrintControlProperties.enumItemLocationAisle)
            {
                itemLocationAisleLabel.Text = ItemLocationAisle;
            }
            else if (enumItem == EnumPickSlipPrintControlProperties.enumItemLocationShelf)
            {
                itemLocationShelfLabel.Text = ItemLocationShelf;
            }
            else if (enumItem == EnumPickSlipPrintControlProperties.enumItemNumber)
            {
                itemNumberLabel.Text = "[" + ItemNumber + "]";
            }
            else if (enumItem == EnumPickSlipPrintControlProperties.enumGunNumber)
            {
                gunNumberLabel.Text = "[" + GunNumber + "]";
            }
        }

        #endregion

        #region Constructors
        public PickSlipPrintControl()
        {
            this.onPropertyValueChange += new delegateSetFieldValues(PickSlipPrintControl_onPropertyValueChange);
            InitializeComponent();
        }
        #endregion
    }
}