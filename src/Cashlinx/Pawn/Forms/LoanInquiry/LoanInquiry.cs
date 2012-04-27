/**************************************************************************************************************
* CashlinxDesktop
* LoanInquiry
* 
* This form is used to provide the selection criteria for a Loan Inquiry
* History
*   S.Murphy 2/14/2010 Initial version
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;
using Pawn.Logic;
using CashlinxDesktop.UserControls;
using System.ComponentModel;
using Pawn.Properties;

//using System.Data.OracleClient;

namespace Pawn.Forms.LoanInquiry
{
    public partial class LoanInquiry : Form
    {
        public NavBox NavControlBox;
        Form _ownerfrm;
        public List<LoanInquiryCriteria> listCriteria = new List<LoanInquiryCriteria>();
        public List<Shops> selectedShops = new List<Shops>();
        private List<Shops> _shops = new List<Shops>();
        ProcessingMessage procMsg;

        public LoanInquiry()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
            this.NavControlBox.Owner = this;
        }

        private void LoanInquiry_Load(object sender, EventArgs e)
        {
            _ownerfrm = Owner;
            NavControlBox.Owner = this;

            //add minimum required criteria
            for (var i = 0; i < Convert.ToInt16(Settings.Default.LoanInquiryMinimumCriteria); i++)
            {
                AddCriteria(i);
            }

            //add sort data
            this.cboSortDirection.Items.Clear();
            this.cboSortDirection.Items.AddRange(new[] { InquiryConst.SELECT, InquiryConst.ASC, InquiryConst.DESC });

            //display selected query if one is in the session variable
            if (GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria == null)
            {
                GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria = new InquiryCriteria();
                //defaults
                this.customTextBoxShop.Text = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                this.radioButtonCustomerSearch.Checked = true;
                //select "Select"
                this.cboSortDirection.Text = InquiryConst.SELECT;
            }
            else
            {
                PopulateScreen();
            }

            this.PerformLayout();
        }
        //back to main menu
        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }
        //clear out all criteria controls
        private void customButtonClear_Click(object sender, EventArgs e)
        {
            RemoveCriteriaCommon();
            this.listCriteria.Clear();
            LoanInquiry_Load(this, e);
        }
        //allows for the saving of the current query
        private void customButtonSave_Click(object sender, EventArgs e)
        {

        }
        //activates the search
        private void customButtonFind_Click(object sender, EventArgs e)
        {
            //Proceed to search results form only if the form is validated
            //if (formValidate())
            //{
            //isFormValid = true;
            //load up CashlinxDesktopSession.Instance.InquirySelectionCriteria
            AddShopsToSessionVar();
            AddCriteriaToSessionVar();
            //Validate()?
            procMsg = new ProcessingMessage("Loading Customer Search Results");
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
            procMsg.ShowDialog(this);
            //if (_errorMessage.Length > 0)
            //{
            //    errorLabel.Text = _errorMessage;
            //    errorLabel.Visible = true;
            //    return;
            //}
            //}
            //else
            //{
            //    if (errorLabel.Text.Length > 0)
            //        MessageBox.Show(Commons.GetMessageString("FormErrorSubmitAgain"));
            //}

            //for now - build SQL
            //string sql = SelectClause() + FromClause() + WhereClause() + Sort();
            ////if > row - show results screen - else show individual?
            //LoanInquiryResults inquiryResults = new LoanInquiryResults();
            //if (this.radioButtonCustomerSearch.Checked)
            //{
            //    inquiryResults.labelInquiryType.Text = InquiryDataTypes.Customer.ToString() + InquiryConst.RESULTLABEL;
            //}
            //else if (this.radioButtonItemSearch.Checked)
            //{
            //    inquiryResults.labelInquiryType.Text = InquiryDataTypes.Item.ToString() + InquiryConst.RESULTLABEL;
            //}
            //else
            //{
            //    inquiryResults.labelInquiryType.Text = InquiryDataTypes.Loan.ToString() + InquiryConst.RESULTLABEL;
            //}
            //inquiryResults.listCriteria = this.listCriteria;

            //inquiryResults.Show();
        }
        //query type selection
        private void radioButtonCustomerSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonCustomerSearch.Checked)
            {
                DoSearchSort(InquiryDataTypes.Customer);
                GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.QueryType = InquiryDataTypes.Customer;
            }
        }
        private void radioButtonLoanSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonLoanSearch.Checked)
            {
                DoSearchSort(InquiryDataTypes.Loan);
                GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.QueryType = InquiryDataTypes.Loan;
            }
        }
        private void radioButtonItemSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonItemSearch.Checked)
            {
                DoSearchSort(InquiryDataTypes.Item);
                GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.QueryType = InquiryDataTypes.Item;
            }
        }
        //select from store locations treeview
        private void radioButtonSelectShops_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSelectShops.Checked)
            {
                _shops.Clear();

                if (LoadShops() == 0)
                {
                    MessageBox.Show("There are no locations to display!");
                }
                else
                {
                    LoanInquiryShops loanInquiryMultStore = new LoanInquiryShops();
                    LoanInquiryShops.Shops = this._shops;

                    loanInquiryMultStore.Show(this);

                    this.Width = loanInquiryMultStore.Width;
                    this.Top = loanInquiryMultStore.Top;
                    this.Left = loanInquiryMultStore.Left;
                }
            }
        }
        private void cboSortField_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxData sort = (ComboBoxData)this.cboSortField.SelectedItem;
            GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SortField = sort;
        }
        private void cboSortDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SortDirection = this.cboSortDirection.SelectedItem.ToString();
        }
        //add to CashlinxDesktopSession.Instance.InquirySelectionCriteria.SelectedCriteria
        private void AddShopsToSessionVar()
        {
            GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SelectedShops = new List<Shops>();
            if (selectedShops.Count > 0)
            {
                GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SelectedShops = selectedShops;
            }
            else
            {
                GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SelectedShops.Add(new Shops("", "", "", this.customTextBoxShop.Text));
            }
        }
        private void AddCriteriaToSessionVar()
        {
            //individual selection criteria
            foreach (LoanInquiryCriteria criteria in this.listCriteria)
            {
                LoanInquiryCriteria inqCriteria = new LoanInquiryCriteria();
                List<Control> ctrlvalues = inqCriteria.FindControls(new[] { "loaninquiry", "temp" }, criteria.Controls);

                if (ctrlvalues.Count > 0)
                {
                    ComboBoxData dataName = criteria.DataName;
                    string[] values = new string[3];
                    int i = 0;

                    //get all non-loaninquiry* objects - these have the values
                    foreach (Control ctrl in ctrlvalues)
                    {
                        switch (ctrl.GetType().Name)
                        {
                            case "State":
                                ComboBox states = (ComboBox)ctrl.Controls[0];
                                USState state = (USState)states.SelectedItem;
                                values[i++] = state.ShortName;
                                break;

                            case "Country":
                                ComboBox countries = (ComboBox)ctrl.Controls[0];
                                CountryData country = (CountryData)countries.SelectedItem;
                                values[i++] = country.Name;
                                break;

                            case "Gender":
                            case "Race":
                            case "Haircolor":
                            case "EyeColor":
                                ComboBox list = (ComboBox)ctrl.Controls[0];
                                ComboBoxData item = (ComboBoxData)list.SelectedItem;
                                values[i++] = item.Description;
                                break;

                            case "Zipcode":
                            case "Date":
                                values[i++] = ctrl.Controls[0].Text;
                                break;

                            case "TextBox":
                            case "ComboBox":
                                values[i++] = ctrl.Text;
                                break;
                        }
                    }

                    InquirySelectedCriteria selCriteria = new InquirySelectedCriteria(criteria.loaninquiryDataTypeCombobox.SelectedItem.ToString(),
                                    dataName.Description, dataName.Code, criteria.SearchType,
                                    values, false);

                    GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SelectedCriteria.Add(selCriteria);
                }
            }
        }
        //populate screen if CashlinxDesktopSession.Instance.InquirySelectionCriteria.SelectedCriteria is populated
        private void PopulateScreen()
        {
            //load the query shops
            if (GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SelectedShops != null)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SelectedShops.Count.Equals(1))
                {
                    this.customTextBoxShop.Text = GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SelectedShops[0].Shop;
                    this.radioButtonSearchShop.Checked = true;
                }
                else
                {
                    this.radioButtonSelectShops.Checked = true;
                    this.selectedShops = GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SelectedShops;
                }
            }
            //load query type
            switch (GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.QueryType)
            {
                case InquiryDataTypes.Customer:
                    this.radioButtonCustomerSearch.Checked = true;
                    break;

                case InquiryDataTypes.Item:
                    this.radioButtonItemSearch.Checked = true;
                    break;

                case InquiryDataTypes.Loan:
                    this.radioButtonLoanSearch.Checked = true;
                    break;
            }
            //load sort info

            this.cboSortDirection.SelectedItem = GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SortDirection;

            MessageBox.Show(GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SortField.Description);
            MessageBox.Show(GlobalDataAccessor.Instance.DesktopSession.InquirySelectionCriteria.SortField.Code);
            ComboBoxData field = (ComboBoxData)this.cboSortField.SelectedItem;

            this.cboSortField.SelectedValue = field;// CashlinxDesktopSession.Instance.InquirySelectionCriteria.SortField;
            this.PerformLayout();
        }
        //for removing LoanInquiryCriteria
        public void RemoveCriteria(int intXButtonNumber)
        {
            //remove all LoanInquiryCriteria from control group 
            RemoveCriteriaCommon();

            //remove selected List<LoanInquiryCriteria>
            listCriteria.Remove(listCriteria[intXButtonNumber - 1]);

            //readd to controls
            List<LoanInquiryCriteria> tempListLoanInquiryCriteria = new List<LoanInquiryCriteria>();
            for (int i = 0; i < listCriteria.Count; i++)
            {
                tempListLoanInquiryCriteria.Add(this.AddCriteria(listCriteria[i], i));
            }

            this.listCriteria = tempListLoanInquiryCriteria;
        }
        //common code for removing LoanInquiryCriteria
        private void RemoveCriteriaCommon()
        {
            for (int i = 0; i < listCriteria.Count; i++)
            {
                Control[] controls = this.panelCriteriaListPanel.Controls.Find(listCriteria[i].Name, false);
                this.panelCriteriaListPanel.Controls.Remove(controls[0]);
            }
        }
        //backgroundworker events
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //retValue = getSearchResultsData();
        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            procMsg.Close();
            procMsg.Dispose();
            //loadLookupCustomerSearchCriteria();
            //CashlinxDesktopSession.Instance.ActiveLookupCriteria = _lookupCustSearch;
            //if (retValue && custDatatable != null)
            //{

            //    isFormValid = true;
            //    CashlinxDesktop.Desktop.CashlinxDesktopSession.Instance.CustDataTable = custDatatable;
            //    if (custAddrDatatable != null)
            //        CashlinxDesktopSession.Instance.CustAddrDataTable = custAddrDatatable;
            //    if (custIdentDatatable != null)
            //        CashlinxDesktopSession.Instance.CustIdentDataTable = custIdentDatatable;
            //    if (custPhoneDatatable != null)
            //        CashlinxDesktopSession.Instance.CustPhoneDataTable = custPhoneDatatable;
            //    if (custEmailDatatable != null)
            //        CashlinxDesktopSession.Instance.CustEmailDataTable = custEmailDatatable;
            //    if (custNotesDatatable != null)
            //        CashlinxDesktopSession.Instance.CustNotesDataTable = custNotesDatatable;

            //Call the lookup results form

            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "LoanInquiryResults";
            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            //}
            //else
            //{
            //    //Create the search criteria string to be shown in the error control 
            //    //If the date of birth is not entered, it should not be shown as '//' in the error message
            //    //hence need to be formatted
            //    string strFilteredDOB = "";
            //    if (_dob.Equals("mm/dd/yyyy"))
            //    {
            //        strFilteredDOB = "";
            //    }
            //    else
            //        strFilteredDOB = _dob;

            //    string strFilteredPhone = "";
            //    if (_phoneAreaCode.Trim().Length > 0 && _phoneNumber.Trim().Length > 0)
            //    {
            //        strFilteredPhone = "(" + _phoneAreaCode + ")" + _phoneNumber.Substring(0, 3) + "-" + _phoneNumber.Substring(3, 4);
            //    }
            //    string searchCriteria = " [" + " " + _ssn + " " + _firstName + " " + _lastName + " " + strFilteredDOB + " " + strFilteredPhone + " " + _idTypeCode + " " + _idNumber + " " + _idIssuer + " " + _custNumber + " " + _loanNumber + " ]";
            //    _errorMessage = Commons.GetMessageString("ZeroCustSearchResults") + searchCriteria;
            //    this.customButtonAddCustomer.Visible = true;

            //}
        }
        //
        //for adding LoanInquiryCriteria
        public void AddCriteria(int intAndButtonNumber)
        {
            //only add another criteria if the "And" was the last And in the collection
            if (intAndButtonNumber == listCriteria.Count)
            {
                LoanInquiryCriteria criteriaList = new LoanInquiryCriteria();

                AddCriteriaCommon(criteriaList, listCriteria.Count);
                listCriteria.Add(criteriaList);
            }

        }
        //for re-adding LoanInquiryCriteria after a remove has been done
        public LoanInquiryCriteria AddCriteria(LoanInquiryCriteria loanCriteria, int intInCriteria)
        {
            AddCriteriaCommon(loanCriteria, intInCriteria);
            return loanCriteria;
        }
        //common code for AddCriteria
        private void AddCriteriaCommon(LoanInquiryCriteria loanCriteria, int intInCriteria)
        {
            if (intInCriteria == 0)
            {
                loanCriteria.Location = new System.Drawing.Point(10, 10);
            }
            else
            {
                loanCriteria.Location = new System.Drawing.Point(10, listCriteria[intInCriteria - 1].Top + listCriteria[intInCriteria - 1].Height);
            }

            loanCriteria.Index = intInCriteria + 1;
            loanCriteria.Name = "LoanInquiryCriteria" + intInCriteria.ToString();
            this.panelCriteriaListPanel.Controls.Add(loanCriteria);
            //move instruction label
            labelMessageLabel.Top = loanCriteria.Top + loanCriteria.Height + 20;

        }
        //
        //load the store locations - return count of locations
        public Int32 LoadShops()
        {
            //string connectionString = "user id=ccsowner;password=prime98s;data source=cashdevdb.casham.com:1521/clxd3.casham.com";

            //OracleConnection connection = new OracleConnection();
            //connection.ConnectionString = connectionString;
            //try
            //{
            //    connection.Open();
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}

            //OracleCommand command = connection.CreateCommand();
            //string sql = "select s.storenumber, s.region, s.market from store s, userinfo u where s.storenumber = u.location order by region desc";// and u.userid = '"

            //command.CommandText = sql;

            //try
            //{
            //    OracleDataReader reader = command.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        Shops loc = new Shops();
            //        loc.Company = "Cash America";
            //        loc.Region = reader["region"].ToString();
            //        loc.Market = reader["market"].ToString();
            //        loc.Shop = reader["storenumber"].ToString();

            //        _shops.Add(loc);
            //    }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}

            //return _shops.Count;
            return 0;
        }
        private void DoSearchSort(InquiryDataTypes sort)
        {
            string[,] list = null;

            switch (sort)
            {
                case InquiryDataTypes.Customer:
                    list = InquirySort.Customer;

                    break;

                case InquiryDataTypes.Item:
                    list = InquirySort.Item;

                    break;

                case InquiryDataTypes.Loan:
                    list = InquirySort.Loan;

                    break;
            }

            InquiryCommon inquiryCommon = new InquiryCommon();
            inquiryCommon.PopulateComboBox(this.cboSortField, list, true);
        }
        //
        private string SelectClause()
        {
            string select = string.Empty;
            //"Select 

            return select;
        }
        private string FromClause()
        {
            string from = string.Empty;

            return from;
        }
        private string WhereClause()
        {
            string where = string.Empty;

            return where;
        }
        private string Sort()
        {
            string sort = string.Empty;

            return sort;
        }
    }
}
