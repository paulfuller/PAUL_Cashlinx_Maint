using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.UserControls
{
    public partial class VendorSearch : UserControl
    {
        private string _vendName = "";
        private string _vendTaxID = "";
        private int _typeOfSearch = -1;
        private string _errorMessage = "";
        private string strStoreNumber = "";
        private LookupVendorSearchData _lookupVendSearch;
        DataTable vendDatatable = new DataTable();
        public delegate void VendorSelected();

        public event VendorSelected VendorSelectClick;

        bool retValue;

        [Browsable(true)]
        public bool CustomerSearchIncluded { get; set; }

        public VendorSearch()
        {
            InitializeComponent();
        }

        public void VendorSearchClear()
        {
            if (CustomerSearchIncluded)
            {
                this.VendorRadiobtn.Checked = false;
                ((RadioButton)this.vendorSearchGroup.Controls["VendorNameRadiobtn"]).Checked = false;
            }
            else
            {
                this.VendorRadiobtn.Checked = true;
                ((RadioButton)this.vendorSearchGroup.Controls["VendorNameRadiobtn"]).Checked = true;
                this.ActiveControl = this.vendorSearchGroup.Controls["lookupVendorName"]; 
            }
            ((CustomTextBox)this.vendorSearchGroup.Controls["lookupVendorName"]).Text = "";
            ((CustomTextBox)this.vendorSearchGroup.Controls["lookupVendorTaxID"]).Text = "";
        }

        public bool VendorFind(out string errorMessage)
        {
            errorMessage = "";

            //Proceed to search results form only if the form is validated
            if (formValidate(ref errorMessage))
            {
                _errorMessage = "";
                bw_findVendor();
                bw_vendorSearchCompleted();
                    
                if (_errorMessage.Length > 0)
                {
                    errorMessage = _errorMessage;
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        void bw_vendorSearchCompleted()
        {
            loadLookupVendorSearchCriteria();
            GlobalDataAccessor.Instance.DesktopSession.LookupCriteria = _lookupVendSearch;
            if (vendDatatable != null)
            {
                GlobalDataAccessor.Instance.DesktopSession.VendDataTable = vendDatatable;
                GlobalDataAccessor.Instance.DesktopSession.VendDataTable.DefaultView.
                Sort = "ROWNUM ASC";
            }
            else
            {
                string searchCriteria = string.Format(" [ {0} {1}  ]", _vendName, _vendTaxID);
                _errorMessage = Commons.GetMessageString("ZeroVendSearchResults") + searchCriteria;
            }
        }

        private bool getVendorSearchResultsData()
        {
            bool b = false;
            string errorCode;
            string errorMsg;
            if (_vendName.Length != 0)
            {
                b = VendorDBProcedures.ExecuteLookupVendor(_vendName, "", strStoreNumber, out vendDatatable, out errorCode, out errorMsg);
            }
            else if (_vendTaxID.Length != 0)
            {
                b = VendorDBProcedures.ExecuteLookupVendor("", _vendTaxID, strStoreNumber, out vendDatatable, out errorCode, out errorMsg);
            }

            return b;
        }

        void bw_findVendor()
        {
            retValue = getVendorSearchResultsData();
        }

        /// <summary>
        /// Function to validate the user input in the lookup form. If there are errors,
        /// the error control of the parent form is set with the message and the function
        /// returns false else returns true
        /// </summary>
        /// <returns></returns>
        private bool formValidate(ref string errorMessage)
        {
            //Flag to specify if the form passed validation
            bool boolValidated = true;
            if (VendorRadiobtn.Checked)
            {
                if (((RadioButton)this.vendorSearchGroup.Controls["VendorNameRadiobtn"]).Checked)
                {
                    _typeOfSearch = (int)VendorSearchCriteria.VENDORNAME;
                    _vendName = ((TextBox)this.vendorSearchGroup.Controls["lookupVendorName"]).Text;
                    if (_vendName.Trim().Length == 0)
                    {
                        boolValidated = false;
                        errorMessage = Commons.GetMessageString("LookupVendorNameError");
                    }
                }
                else
                {
                    _typeOfSearch = (int)VendorSearchCriteria.VENDORTAXID;
                    _vendTaxID = ((TextBox)this.vendorSearchGroup.Controls["lookupVendorTaxID"]).Text;
                    if (_vendTaxID.Trim().Length == 0)
                    {
                        boolValidated = false;
                        errorMessage = Commons.GetMessageString("LookupVendorTaxIDError");
                    }
                }
            }
            else
            {
                boolValidated = false;
                errorMessage = Commons.GetMessageString("LookupSearchCriteriaError");
            }
            return boolValidated;
        }

        /// <summary>
        /// This method is to load the search criteria object with values entered
        /// in the search criteria fields in the form
        /// </summary>
        private void loadLookupVendorSearchCriteria()
        {
            _lookupVendSearch = new LookupVendorSearchData();
            //If search type was Vendor Name
            if (_typeOfSearch == (int)VendorSearchCriteria.VENDORNAME)
            {
                if (_vendName != null && _vendName.Trim().Length != 0)
                    _lookupVendSearch.VendName = _vendName;
            }
            //search type was Vendor Tax ID
            else
            {
                if (_vendTaxID != null && _vendTaxID.Trim().Length != 0)
                {
                    _lookupVendSearch.TaxID = _vendTaxID;
                }
            }

            _lookupVendSearch.TypeOfSearch = _typeOfSearch;
        }

        private void VendorNameRadiobtn_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked =
            ((RadioButton)this.vendorSearchGroup.Controls["VendorNameRadiobtn"]).Checked;

            if (isChecked)
            {
                _vendTaxID = "";
                ((TextBox)this.vendorSearchGroup.Controls["lookupVendorTaxID"]).Text = "";
                ((TextBox)this.vendorSearchGroup.Controls["lookupVendorName"]).Focus();
                this.ActiveControl = this.vendorSearchGroup.Controls["lookupVendorName"];
            }
            else
            {
                ((TextBox)this.vendorSearchGroup.Controls["lookupVendorName"]).Text = "";
                ((TextBox)this.vendorSearchGroup.Controls["lookupVendorTaxID"]).Focus();
                this.ActiveControl = this.vendorSearchGroup.Controls["lookupVendorTaxID"];
            }

            ((TextBox)this.vendorSearchGroup.Controls["lookupVendorName"]).Enabled = isChecked;
            ((TextBox)this.vendorSearchGroup.Controls["lookupVendorTaxID"]).Enabled = !isChecked;
        }

        private void VendorSearch_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                if (!CustomerSearchIncluded)
                {
                    this.VendorRadiobtn.Checked = true;
                    ((RadioButton)this.vendorSearchGroup.Controls["VendorNameRadiobtn"]).Checked = true;
                    this.ActiveControl = this.vendorSearchGroup.Controls["lookupVendorName"];
                }
                else
                {
                    this.VendorRadiobtn.Checked = false;
                    ((RadioButton)this.vendorSearchGroup.Controls["VendorNameRadiobtn"]).Checked = false;
                }
            }
        }

        private void VendorRadiobtn_CheckedChanged(object sender, EventArgs e)
        {
            VendorSelectClick();
            ((RadioButton)this.vendorSearchGroup.Controls["VendorNameRadiobtn"]).Checked = true;
        }
    }
}
