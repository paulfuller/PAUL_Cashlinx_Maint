/********************************************************************
* CashlinxDesktop
* ViewCustomerInformation
* This form is shown a customer's identification information
* needs to be updated
* Sreelatha Rengarajan 5/14/2009 Initial version
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
//using Support.Forms.UserControls;
using Support.Forms.Pawn.Customer;
using Support.Libraries.Objects.Customer;

//using Pawn.Logic;

namespace Support.Forms.Customer
{
    public partial class UpdateCustomerIdentification : Form
    {
        private List<IdentificationVO> _custIds;
        private CustomerVOForSupportApp _custToEdit;
        private CustomerVOForSupportApp _updatedCustomer;
        private string _partyId;


 

        //All the IDs shown in the datagrid
        string[] _strIdType;
        string[] _strIdIssuer;
        string[] _strIdNumber;
        string[] _strIdExpiryDate;
        string[] _strIdentId;
        string[] _strIdTypeDesc;
        string[] _strIdIssuerCode;
        string[] _strIdIssueDate;

        //The updated IDs in the datagrid
        string[] _strUpdatedIdType;
        string[] _strUpdatedIdIssuer;
        string[] _strUpdatedIdNumber;
        string[] _strUpdatedIdExpiryDate;
        string[] _strUpdatedIdentId;
        //Data table that will be returned with the IDs after the update stored proc call
        DataTable _custIdentities = new DataTable();

        //Number of rows updated
        int _numRows;

        List<IdentificationVO> _updatedIds;

        Form ownerFrm;
        public NavBox NavControlBox;
        #region FORMSTARTUP
        /*__________________________________________________________________________________________*/
        public UpdateCustomerIdentification()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
        }
        /*__________________________________________________________________________________________*/
        private void UpdateCustomerIdentification_Load(object sender, EventArgs e)
        {
            ownerFrm = this.Owner;
            NavControlBox.Owner = this;
            //CustToEdit = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            CustToEdit = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;
            LoadIDDataInForm();
        }
        /*__________________________________________________________________________________________*/
        private void LoadIDDataInForm()
        {
            this.identification1.populateCustomerIdentification(_custIds);
        }
        #endregion
        #region MEMBER PROPERTIES
        /*__________________________________________________________________________________________*/
        public CustomerVOForSupportApp CustToEdit
        {
            set
            {
                _custToEdit = value;
                _custIds = _custToEdit.getAllIdentifications();
            }

        }
        #endregion
        #region GETDATA
        /*__________________________________________________________________________________________*/
        private void LoadIDDataInObject()
        {
            //_updatedCustomer = new CustomerVOForSupportApp();
            _updatedCustomer = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;
            _updatedCustomer.removeIdentities();
            if (_custIdentities != null)
            {

                foreach (DataRow cust in _custIdentities.Rows)
                {
                    IdentificationVO custid = new IdentificationVO
                    {
                        IdType =
                            Utilities.GetStringValue(
                            cust.ItemArray[(int)customeridrecord.IDENTTYPECODE], ""),
                        IdValue =
                            Utilities.GetStringValue(
                            cust.ItemArray[(int)customeridrecord.ISSUEDNUMBER], ""),
                        IdIssuer =
                            Utilities.GetStringValue(
                            cust.ItemArray[(int)customeridrecord.STATE_NAME], ""),
                        IdIssuerCode =
                            Utilities.GetStringValue(
                            cust.ItemArray[(int)customeridrecord.ISSUERNAME], ""),
                        DatedIdentDesc =
                            Utilities.GetStringValue(
                            cust.ItemArray[(int)customeridrecord.DATEDIDENTTYPEDESC], ""),
                        IdExpiryData =
                            Utilities.GetDateTimeValue(
                            cust.ItemArray[(int)customeridrecord.EXPIRYDATE],
                            DateTime.MaxValue),
                        IdentId =
                            Utilities.GetStringValue(
                            cust.ItemArray[(int)customeridrecord.IDENTID], "")
                    };
                    _updatedCustomer.addIdentity(custid);
                }

            }

        }

        /*__________________________________________________________________________________________*/
        private bool identDataChanged()
        {

            _custIds = new List<IdentificationVO>();
            DataTable custDatatable;

            for (int i = 0; i < _strIdType.Length; i++)
            {
                IdentificationVO custIdObj = _custToEdit.getIdentity(_strIdType[i], _strIdNumber[i], _strIdIssuer[i]);
                IdentificationVO identobj = _custToEdit.getIdentity(_strIdType[i], _strIdNumber[i], _strIdIssuerCode[i], _strIdExpiryDate[i]);
                if (identobj == null)
                {
                    string strIdentTypeCode = _strIdType[i];
                    string strIdentNumber = _strIdNumber[i];
                    string strIdentIssuer = _strIdIssuerCode[i];
                    //do the id check if the expiry date is not the only change
                    if (custIdObj == null)
                    {
                        string errorCode;
                        string errorMsg;
                        bool retValue = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).CheckDuplicateID(strIdentTypeCode, strIdentNumber, strIdentIssuer, out custDatatable, out errorCode, out errorMsg);
                        if (retValue)
                        {
                            if (custDatatable != null)
                            //call to duplicate id check yielded records which means there are users that match the ID
                            {
                                if (custDatatable.Rows.Count > 0)
                                {
                                    StringBuilder errorMessage = new StringBuilder();
                                    errorMessage.Append(Commons.GetMessageString("DuplicateIDMessage"));
                                    errorMessage.Append("  <");
                                    errorMessage.Append(strIdentTypeCode);
                                    errorMessage.Append(",");
                                    errorMessage.Append(strIdentIssuer);
                                    errorMessage.Append(",");
                                    errorMessage.Append(strIdentNumber);
                                    errorMessage.Append(">");
                                    MessageBox.Show(errorMessage.ToString(), "Error");
                                    throw new ApplicationException("-1");
                                }

                            }
                        }
                        else
                        {
                            //If it is fatal error from the database, it would have been handled at the oracledataaccessor layer
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, errorMsg);

                        }
                    }
                    else
                    {
                        if (i >= _custToEdit.NumberIdentities && (custIdObj.IdExpiryData).FormatDate() != _strIdExpiryDate[i])
                        {
                            //no duplicate ID check needed since the user has entered an ID which is the same
                            //as another ID that exists for the customer but only the expiry date is different
                            //This scenario is only when the user enters a brand new row with the exact same ID 
                            //as another id for the same user but enters a different expiry date
                            StringBuilder errorMessage = new StringBuilder();
                            errorMessage.Append(Commons.GetMessageString("DuplicateIDSameCustomerDiffExpiryDt"));
                            errorMessage.Append(System.Environment.NewLine + " <");
                            errorMessage.Append(strIdentTypeCode);
                            errorMessage.Append(",");
                            errorMessage.Append(strIdentIssuer);
                            errorMessage.Append(",");
                            errorMessage.Append(strIdentNumber);
                            errorMessage.Append(">");
                            throw new ApplicationException(errorMessage.ToString());
                        }
                    }

                    IdentificationVO newid = new IdentificationVO();
                    newid.IdType = _strIdType[i];
                    newid.IdValue = _strIdNumber[i];
                    newid.IdIssuer = _strIdIssuer[i];
                    try
                    {
                        newid.IdExpiryData = DateTime.Parse(_strIdExpiryDate[i], CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        newid.IdExpiryData = DateTime.MaxValue;
                    }
                    newid.IdentId = _strIdentId[i];
                    newid.DatedIdentDesc = _strIdTypeDesc[i];
                    newid.IdIssuerCode = _strIdIssuerCode[i];
                    _updatedIds.Add(newid);
                    _custIds.Add(newid);
                }
                else
                {
                    //The same ID already exists for the customer..check to see
                    //if this was entered as a new row in the datagrid
                    if (i >= _custToEdit.NumberIdentities)
                        throw new ApplicationException(Commons.GetMessageString("DuplicateIDSameCustomer"));
                    else
                        _custIds.Add(identobj);
                }
            }
            if (_updatedIds.Count == 0)
                return false;
            else
            {
                return true;
            }
        }
        #endregion
        #region EVENTS
        /*__________________________________________________________________________________________*/
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly";
            this.NavControlBox.Action = NavBox.NavAction.BACK;
            //this.Close();
            //this.Dispose(true);
        }
        /*__________________________________________________________________________________________*/
        private void buttonReset_Click(object sender, EventArgs e)
        {
            LoadIDDataInForm();
        }
        /*__________________________________________________________________________________________*/
        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (this.identification1.isValid())
            {
                List<IdentificationVO> customerIds = this.identification1.getIdentificationData();
                if (customerIds != null)
                {
                    _numRows = customerIds.Count;
                    //Array inputs
                    _strIdType = new string[_numRows];
                    _strIdIssuer = new string[_numRows];
                    _strIdNumber = new string[_numRows];
                    _strIdExpiryDate = new string[_numRows];
                    _strIdentId = new string[_numRows];
                    _strIdIssuerCode = new string[_numRows];
                    _strIdTypeDesc = new string[_numRows];
                    _strIdIssueDate = new string[_numRows];

                    foreach (IdentificationVO id in customerIds)
                    {
                        _strIdTypeDesc[i] = id.DatedIdentDesc;
                        _strIdType[i] = id.IdType;
                        _strIdIssuer[i] = id.IdIssuer;
                        _strIdNumber[i] = id.IdValue;
                        _strIdIssuerCode[i] = id.IdIssuerCode;
                        _strIdIssueDate[i] = id.IdIssueDate.FormatDate();
                        _strIdExpiryDate[i] = id.IdExpiryData.FormatDate();
                        _strIdentId[i] = id.IdentId;
                        i++;
                    }
                }
                else
                    //no id data
                    return;
            }
            else
                //id data is invalid
                return;

            string strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
            _updatedIds = new List<IdentificationVO>();
            _partyId = _custToEdit.PartyId;

            if (i > 0)
            {
                try
                {
                    if (identDataChanged())
                    {
                        bool updateIdData = true;
                        DialogResult dgr;
                        do
                        {
                            _strUpdatedIdType = new string[_updatedIds.Count];
                            _strUpdatedIdNumber = new string[_updatedIds.Count];
                            _strUpdatedIdIssuer = new string[_updatedIds.Count];
                            _strUpdatedIdExpiryDate = new string[_updatedIds.Count];
                            _strUpdatedIdentId = new string[_updatedIds.Count];

                            int k = 0;
                            foreach (IdentificationVO id in _updatedIds)
                            {
                                _strUpdatedIdType[k] = id.IdType;
                                _strUpdatedIdNumber[k] = id.IdValue;
                                _strUpdatedIdIssuer[k] = id.IdIssuerCode;
                                _strUpdatedIdExpiryDate[k] = id.IdExpiryData == DateTime.MaxValue ? "" : id.IdExpiryData.ToShortDateString();
                                _strUpdatedIdentId[k] = id.IdentId;
                                k++;
                            }
                            var errorCode = string.Empty;
                            var errorMsg = string.Empty;
                            updateIdData = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).UpdateCustomerPersonalIdentifications(strUserId, _partyId, _strUpdatedIdNumber, _strUpdatedIdExpiryDate, _strUpdatedIdIssuer, _strUpdatedIdType, _strUpdatedIdentId, out _custIdentities, out errorCode, out errorMsg);
                            if (updateIdData)
                            {
                                MessageBox.Show("Customer Identification update successful");
                                //Load id data returned from the sp call into the customer object
                                //LoadIDDataInObject();
                                //Form ownerForm = this.Owner;
                                //Console.WriteLine("ownerForm.GetType() -->" + ownerForm.GetType());
                                //if (ownerForm.GetType() == typeof(ViewCustomerInformation))
                                {
                                    //var custInfoFrm = (ViewCustomerInformation)ownerForm;
                                    /*
                                            DataTable custDatatable = new DataTable();
                                            DataTable custPhoneDatatable = new DataTable();
                                            DataTable custIdentDatatable = new DataTable();
                                            DataTable custAddrDatatable = new DataTable();
                                            DataTable custEmailDatatable = new DataTable();
                                            DataTable custNotesDatatable = new DataTable();
                                            DataTable custStoreCreditDatatable = new DataTable();

                                            Support.Controllers.Database.Procedures.CustomerDBProcedures customerDBProceduresForSupport
                                            = new Support.Controllers.Database.Procedures.CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession);

                                    bool b = customerDBProceduresForSupport.ExecuteLSupporApptookupCustomer(
                                    "", "", "", "", _updatedCustomer.CustomerNumber, "", "", "", "", "", "", "", "", "", "", "", "CUST_INFO", out custDatatable, out custIdentDatatable, out custPhoneDatatable,
                                    out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);

                                    if (b)
                                    {
                                        _custIdentities = custIdentDatatable;
                                        Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer = _updatedCustomer;
                                        LoadIDDataInObject();
                                    } */
                                    //custInfoFrm.UpdatedCustomerToView = _updatedCustomer;
                                    //custInfoFrm.ShowUpdates = true;

                                    LoadIDDataInObject();
                                }
                                break;
                            }
                            else
                            {
                                dgr = MessageBox.Show(Commons.GetMessageString("CustIdentUpdateFailure"), "Error", MessageBoxButtons.RetryCancel);
                            }
                        } while (dgr == DialogResult.Retry);
                    }
                    else
                    {
                        MessageBox.Show(Commons.GetMessageString("NoChangesInForm"));
                    }
                }
                catch (ApplicationException aEx)
                {
                    //This means there was a duplicate ID error
                    //and the ID belongs to another customer
                    if (aEx.Message == "-1")
                    {
                        return;
                    }
                    else
                    {
                        //This means that a new ID was entered which already
                        //exists for this customer
                        MessageBox.Show(aEx.Message);
                        return;
                    }
                }
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly";
                this.NavControlBox.Action = NavBox.NavAction.BACK;
                //this.Close();
                //this.Dispose(true);
            }
            else
            {
                MessageBox.Show(Commons.GetMessageString("NoChangesInForm"));
            }

        }
        #endregion
    }
}

