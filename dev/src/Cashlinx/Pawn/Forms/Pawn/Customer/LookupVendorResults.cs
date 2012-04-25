/********************************************************************
* CashlinxDesktop
* LookupCustomerResults
* This form will show the results of the search for a customer
* based on criteria entered in the lookup customer search screen
* Sreelatha Rengarajan 3/13/2009 Initial version
 * SR 5/24/2010 Disable Add and View Customer buttons if we are in the customer hold or police hold flow
*********************************************************************************************************/

using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Customer
{
    public partial class LookupVendorResults : Form
    {


        private BindingSource _bindingSource1;
        private VendorVO _vendorObject;
        private DataTable _vendors;
        private Form ownerFrm;
        private int pageSize = 15;
        private int curPage = 1;
        private int nrPages = 1;


        private DataTable _vendDatatable;
        public NavBox NavControlBox;// { get; set; }

        public LookupVendorResults()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }


        private void lookupVendorBackButton_Click(object sender, EventArgs e)
        {
            loadLookupForm();

        }

        private void addVendorButton_Click(object sender, EventArgs e)
        {
            string trigger = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger;
            //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.ADDCUSTOMER;            
            //CustomerController.NavigateUser(ownerFrm);
            VendorVO newVendor = new VendorVO
            {
                NewVendor = true
            };
            GlobalDataAccessor.Instance.DesktopSession.ActiveVendor = newVendor;
            /*if (trigger.Equals("newpawnloan", StringComparison.OrdinalIgnoreCase))
            {


                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "ManagePawnApplication";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {*/

                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "CreateVendor";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

           // }

        }

        private void LookupVendorResults_Load(object sender, EventArgs e)
        {
            //Set the owner form
            ownerFrm = this.Owner;
            this.NavControlBox.Owner = this;
            //Get data from the desktop session global variables

            _vendDatatable = GlobalDataAccessor.Instance.DesktopSession.VendDataTable;

            errorLabel.Visible = false;
            try
            {
                if (_vendDatatable != null)
                {
                    //store this for when the user selects a vendor
                    _vendors = _vendDatatable.Copy();
                    //Add primary key to the table
                    DataColumn[] key = new DataColumn[1];
                    key[0] = _vendors.Columns["VENDOR_ID"];
                    _vendors.PrimaryKey = key;




                    if (_vendDatatable.Rows.Count > pageSize)
                    {
                        this.customButtonAddVendor.Visible = false;
                        errorLabel.Text = "* Warning : " + Commons.GetMessageString("MultipleVendSearchResults");
                        errorLabel.Visible = true;
                        //this.nrPages = (int) Math.Ceiling((decimal) ((decimal)_vendDatatable.Rows.Count / (decimal)pageSize));
                        //this.pageInd.Visible = true;
                        //this.prevPage.Visible = true;
                        //this.nextPage.Visible = true;
                        //this.firstPage.Visible = true;
                        //this.lastPage.Visible = true;
                    }
                    else
                    {
                        this.customButtonAddVendor.Visible = true;
                    }


                    DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;


                    populateDataGrid();

                    this.lookupVendorResultsGrid.AutoGenerateColumns = false;
                    this.lookupVendorResultsGrid.Columns[1].DataPropertyName = "VENDOR_ID";
                    this.lookupVendorResultsGrid.Columns[2].DataPropertyName = "TAX_ID";
                    this.lookupVendorResultsGrid.Columns[3].DataPropertyName = "NAME";
                    this.lookupVendorResultsGrid.Columns[4].DataPropertyName = "ADDRESS1";
                    this.lookupVendorResultsGrid.Columns[5].DataPropertyName = "CONTACT_NAME";
                    this.lookupVendorResultsGrid.Columns[6].DataPropertyName = "CONTACT_PHONE";

                    this.lookupVendorResultsGrid.Focus();
                }
                else
                //If the Vendor data table is empty return back to caller
                {
                    return;
                }

            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException(Commons.GetMessageString("LookupResultsExceptionError"), ex);
                MessageBox.Show(Commons.GetMessageString("ProcessingError"));
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }

            finally
            {
                _vendDatatable = null;
            }

        }

        private void populateDataGrid()
        {
            if (_vendDatatable == null)
                _vendDatatable = GlobalDataAccessor.Instance.DesktopSession.VendDataTable;

            int sortedColIdx = -1;
            ListSortDirection sortedOrder = ListSortDirection.Ascending;

            _bindingSource1 = new BindingSource();

            _bindingSource1.DataSource = _vendDatatable;//

            lookupVendorResultsGrid.DataSource = _bindingSource1; //
            if (sortedColIdx >= 0)
                lookupVendorResultsGrid.Sort(
                    lookupVendorResultsGrid.Columns[sortedColIdx], sortedOrder);//

            this.pageInd.Text = String.Format("Page {0} of {1}", curPage, nrPages);//

        }


        public void firstPage_Click(object sender, EventArgs e)
        {
            curPage = 1;
            populateDataGrid();
        }

        public void lastPage_Click(object sender, EventArgs e)
        {
            curPage = nrPages;

            populateDataGrid();
        }

        public void prevPage_Click(object sender, EventArgs e)
        {
            if (curPage - 1 > 1)
                curPage--;
            else
                curPage = 1;

            populateDataGrid();
        }

        private void nextPage_Click(object sender, EventArgs e)
        {
            if (curPage + 1 < nrPages)
                curPage++;
            else
                curPage = nrPages;

            populateDataGrid();
        }


        /// <summary>
        /// This method loads the look up Vendor control in the parent form
        /// and removes the lookup Vendor results control from the parent form
        /// </summary>
        private void loadLookupForm()
        {

            this.NavControlBox.Action = NavBox.NavAction.BACK;
        }

        /// <summary>
        /// When a row is double clicked, user should be taken to Edit 
        /// after creating a Vendor object with all the data of the selected Vendor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookupVendorResultsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showVendor();
        }

        private void showVendor()
        {
            DialogResult dgr = DialogResult.Retry;
            do
            {
                try
                {
                    LoadSelectedVendor();

                    
                    if (_vendorObject != null)
                    {
                        //Set the selected customer in the desktop session as the ActiveCustomer
                        GlobalDataAccessor.Instance.DesktopSession.ActiveVendor = _vendorObject;
                        NavControlBox.IsCustom = true;
                        NavControlBox.CustomDetail = "CreateVendor";
                        NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;


                        break;
                    }
                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                    this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                }
                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException(Commons.GetMessageString("SelectedVendorLoadError"), ex);
                    dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                    if (dgr == DialogResult.Retry)
                        continue;
                    else
                    {
                        _vendorObject = null;
                        break;
                    }
                }
            } while (dgr == DialogResult.Retry);

        }

        private void LoadSelectedVendor()
        {

            //Get the selected row from the grid and get the corresponding data from the customer table
            //for the selected customer
            DataRowView drv = (DataRowView)(_bindingSource1.Current);
            DataRow selectedVendorRow = _vendors.Rows.Find(drv.Row["vendor_id"]);
            string selectedId = selectedVendorRow.ItemArray[(int)vendorrecord.ID].ToString();
            _vendorObject = VendorProcedures.getVendorDataInObject(selectedId, selectedVendorRow);
        }

        //when the select button is clicked, the selected customer's information
        //should be loaded in manage pawn application
        private void lookupCustomerResultsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                showVendor();
            }
        }


        private void dataSort(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView theGrid = (DataGridView) sender;
            string sortDirection = (theGrid.SortOrder == SortOrder.Ascending) ? "ASC" : "DESC";
            string sortString = theGrid.SortedColumn.DataPropertyName + " " + sortDirection;

            GlobalDataAccessor.Instance.DesktopSession.VendDataTable.DefaultView.Sort = sortString;

            curPage = 1;

            populateDataGrid();

        }
    }
}
