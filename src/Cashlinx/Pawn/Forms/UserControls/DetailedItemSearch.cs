using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
using System;

namespace Pawn.Forms.UserControls
{
    public partial class DetailedItemSearch : UserControl
    {
        public delegate void ShowDescribeMerchandise();
        public event ShowDescribeMerchandise ShowCategory;
        public event EventHandler ValueChanged;
        public event EventHandler Search;
        # region Properties

        private DesktopSession CDS
        {
            get { return GlobalDataAccessor.Instance.DesktopSession; }
        }

        # endregion

        # region Constructors

        public DetailedItemSearch()
            : base()
        {
            InitializeComponent();
        }

        # endregion

        # region Public Methods

        public void Clear()
        {
            this.txtCategory.Text = string.Empty;
            this.txtModelNumber.Text = string.Empty;
            this.txtSerialNumber.Text = string.Empty;
            this.txtManufacturer.Text = string.Empty;
            this.txtDescription.Text = string.Empty;

            CDS.ActiveItemSearchData = new ItemSearchCriteria();
        }

        public void BuildParameters(List<string> searchFor, List<string> searchValues)
        {
            searchFor.Clear();
            searchValues.Clear();

            if (CDS.ActiveItemSearchData.CategoryID > 0)
            {
                searchFor.Add("CAT_CODE");
                searchValues.Add(CDS.ActiveItemSearchData.CategoryID.ToString());
            }

            if (!string.IsNullOrEmpty(txtManufacturer.Text))
            {
                searchFor.Add("MANUFACTURER");
                searchValues.Add(txtManufacturer.Text);
            }

            if (!string.IsNullOrEmpty(txtModelNumber.Text))
            {
                searchFor.Add("MODEL");
                searchValues.Add(txtModelNumber.Text);
            }

            if (!string.IsNullOrEmpty(txtSerialNumber.Text))
            {
                searchFor.Add("SERIAL_NUMBER");
                searchValues.Add(txtSerialNumber.Text);
            }

            if (!string.IsNullOrEmpty(txtDescription.Text))
            {
                searchFor.Add("MD_DESC");
                searchValues.Add(txtDescription.Text);
            }
        }

        public bool HasValues()
        {
            return CDS.ActiveItemSearchData.CategoryID > 0 ||
                !string.IsNullOrEmpty(txtManufacturer.Text) ||
                !string.IsNullOrEmpty(txtModelNumber.Text) ||
                !string.IsNullOrEmpty(txtSerialNumber.Text) ||
                !string.IsNullOrEmpty(txtDescription.Text);
        }

        # endregion

        # region Event Handlers

        private void txt_TextChanged(object sender, EventArgs e)
        {
            RaiseValueChanged();
        }

       /* private void txtDescription_Leave(object sender, EventArgs e)
        {
            if (txtDescription.Text.Length > 0)
            {
                RaiseSearch();
            }
        }*/

        private void txtManufacturer_TextChanged(object sender, EventArgs e)
        {
            CDS.ActiveItemSearchData.Manufacturer = string.Empty;
            txt_TextChanged(sender, e);
        }

        private void txtModelNumber_TextChanged(object sender, EventArgs e)
        {
            CDS.ActiveItemSearchData.Model = string.Empty;
            txt_TextChanged(sender, e);
        }

        # endregion

        # region Helper Methods

        private void cmdCategoryID_Click(object sender, System.EventArgs e)
        {
            ShowCategory();
        }

        private void DetailedItemSearch_Paint(object sender, PaintEventArgs e)
        {
            if (!DesignMode)
            {
                txtCategory.Text = CDS.ActiveItemSearchData.CategoryDescription;
                if (!string.IsNullOrEmpty(CDS.ActiveItemSearchData.Manufacturer) && !CDS.ActiveItemSearchData.Manufacturer.Equals("N/A"))
                {
                    txtManufacturer.Text = CDS.ActiveItemSearchData.Manufacturer;
                }
                if (!string.IsNullOrEmpty(CDS.ActiveItemSearchData.Model) && !CDS.ActiveItemSearchData.Model.Equals("N/A"))
                {
                    txtModelNumber.Text = CDS.ActiveItemSearchData.Model;
                }
            }
        }

        private void RaiseValueChanged()
        {
            if (ValueChanged == null)
            {
                return;
            }

            ValueChanged(this,EventArgs.Empty);
        }

        private void RaiseSearch()
        {
            if (Search == null)
            {
                return;
            }

            Search(this,EventArgs.Empty);
        }

        # endregion



        public void SetFocusOnDescription()
        {
            txtDescription.Focus();
        }
    }
}
