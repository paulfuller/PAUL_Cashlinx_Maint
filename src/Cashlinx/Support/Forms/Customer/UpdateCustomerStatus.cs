using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Support.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;
using Support.Controllers.Database.Procedures;

namespace Support.Forms.Customer
{
    public partial class UpdateCustomerStatus : Form
    {
        private string customernumber;
        private string currentcustomerstatus; 
        private string changestatusto;
        private string currentreasoncode;
        private string changereasonto;

        private bool customerstatuschanged;
        private bool customerreasonchanged;

        private string DBReturnCode = "";
        private string errorMessage = "";


        private CustomerVOForSupportApp _custToEdit;

        private Form ownerfrm;
        public NavBox NavControlBox;

        #region FORMSTARTUP
        /*__________________________________________________________________________________________*/
        public UpdateCustomerStatus()

        {
            InitializeComponent();
            this.NavControlBox = new NavBox();

            ArrayList StatusPickList = new ArrayList();
            StatusPickList.Add(new ComboBoxData("", "Active"));
            StatusPickList.Add(new ComboBoxData("", "InActive"));

            ArrayList ReasonPickList = new ArrayList();
            ReasonPickList.Add(new ComboBoxData("", "Bad"));
            ReasonPickList.Add(new ComboBoxData("", "Bankrupt"));
            ReasonPickList.Add(new ComboBoxData("", "Null"));

            this.cmbChangeCustStatusTo.DataSource = StatusPickList;
            this.cmbChangeCustStatusTo.DisplayMember = "Description";
            this.cmbChangeCustStatusTo.ValueMember = "Code";

            this.cmbChangeCustReasonTo.DataSource = ReasonPickList;
            this.cmbChangeCustReasonTo.DisplayMember = "Description";
            this.cmbChangeCustReasonTo.ValueMember = "Code";

        }
        /*__________________________________________________________________________________________*/
        private void UpdateCustomerStatus_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;

            GetCustomerStatusData();
            
        }
        #endregion
        #region GETDATA
        /*__________________________________________________________________________________________*/
        private Boolean WriteCustomerStatusChangeToDB()
        {
            Boolean retValue = false;

            var customerDBProceduresForSupport = 
                new Support.Controllers.Database.Procedures.CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession);

            retValue = customerDBProceduresForSupport.GetChangeCustomerStatusDBData(CustomerNumber, 
            CustomerStatusChanged ? "Y": "N",
            ChangeStatusTo,
            CustomerReasonChanged ? "Y" : "N",
            ChangeReasonTo,
            out DBReturnCode,
            out errorMessage  );

            return retValue;
        }
        /*__________________________________________________________________________________________*/
        private void GetCustomerStatusData()
        {
            _custToEdit = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;
            //MapCustomerStatusFormData();

            CustomerNumber = _custToEdit.CustomerNumber;
            CurrentCustomerStatus = _custToEdit.Status;
            CurrentReasonCode = _custToEdit.ReasonCode;
            //CurrentReasonCode = "";
            ChangeReasonTo = "";

            MapCustomerStatusFormData();
        }
        /*__________________________________________________________________________________________*/
        private CustomerVOForSupportApp CustToEdit
        {
            set
            {
                _custToEdit = value;
                CurrentCustomerStatus = _custToEdit.Status;
                CurrentReasonCode = _custToEdit.ReasonCode;
                CustomerNumber = _custToEdit.CustomerNumber;
            }
        }
        /*__________________________________________________________________________________________*/
        private void MapCustomerStatusFormData()
        {
            this.TxBCurrCustStatus.Text             = CurrentCustomerStatus;
            this.cmbChangeCustStatusTo.Text     = ChangeStatusTo;
            this.TxBCurrCustReasonCode.Text     = CurrentReasonCode;
            this.cmbChangeCustReasonTo.Text    = ChangeReasonTo;
        }
        /*__________________________________________________________________________________________*/
        private void MapCustomerStatusPropertiesDataFromForm()
        {
            CurrentCustomerStatus = this.TxBCurrCustStatus.Text         ;
            ChangeStatusTo          = this.cmbChangeCustStatusTo.Text   ;       
            CurrentReasonCode      = this.TxBCurrCustReasonCode.Text    ;
            ChangeReasonTo          = this.cmbChangeCustReasonTo.Text;  
            HasCustomerStatusChanged();
            HasReasonTextChanged();
        }
        /*__________________________________________________________________________________________*/
        private void PutCustomerStatusDataToGlobal()
        {
             _custToEdit.Status = ChangeStatusTo;
             _custToEdit.ReasonCode = ChangeReasonTo;
        }

        #endregion
        #region MEMBER PROPERTIES
        /*__________________________________________________________________________________________*/
        //private string CustomerNumber { get { return customernumber; } set { customernumber = _custToEdit.CustomerNumber; } }
        private string CustomerNumber { get; set; }
        /*__________________________________________________________________________________________*/
        //private string CurrentCustomerStatus { get { return currentcustomerstatus; } set { currentcustomerstatus = _custToEdit.Status; } }
        private string CurrentCustomerStatus { get; set; }
        /*__________________________________________________________________________________________*/
        private string ChangeStatusTo{get;set;}
        /*__________________________________________________________________________________________*/
        //private string CurrentReasonCode { get { return currentreasoncode; } set { currentreasoncode = _custToEdit.ReasonCode; } }
        private string CurrentReasonCode { get; set; }
        /*__________________________________________________________________________________________*/
        private string ChangeReasonTo
        {
            get { return changereasonto; } 
                set{
                    if (value == "Null")
                        changereasonto = "";
                    else
                        changereasonto = value;
                    } 
            }
        /*__________________________________________________________________________________________*/
        private Boolean CustomerStatusChanged { get; set; }
        /*__________________________________________________________________________________________*/
        private Boolean CustomerReasonChanged { get; set; }
        #endregion
        #region BOOLEAN
        /*__________________________________________________________________________________________*/
        private bool HasCustomerStatusChanged()
        {
            //MapCustomerStatusPropertiesData();

            CustomerStatusChanged = false;
            if (!string.IsNullOrEmpty(ChangeStatusTo) && CurrentCustomerStatus != ChangeStatusTo)
                CustomerStatusChanged = true;

            return CustomerStatusChanged;
        }
        /*__________________________________________________________________________________________*/
        private bool HasReasonTextChanged()
        {
            //MapCustomerStatusPropertiesData();

            CustomerReasonChanged = false;
            //if (!string.IsNullOrEmpty(ChangeReasonTo) && CurrentReasonCode != ChangeReasonTo)
            if (CurrentReasonCode != ChangeReasonTo)
                CustomerReasonChanged = true;

            return CustomerReasonChanged;
        }

        #endregion
        #region EVENTS
        /*__________________________________________________________________________________________*/
        private void BtnCancel_Click(object sender, EventArgs e)
        {

            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "UpdateCustomerDetails";
            this.NavControlBox.Action = NavBox.NavAction.BACK;
        }
        /*__________________________________________________________________________________________*/
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            string NoChangeInCustomerStatusFormMsg = "You cannot submit the Change Customer Status because you have NOT made any changes to customer status information.";
            string DataSentToSQLDidNotWriteMsg = "Your change request failed to write to the Database, you might have a connection problem.";
            MessageBoxButtons MsgButton = MessageBoxButtons.YesNo;


            MapCustomerStatusPropertiesDataFromForm();

            //if (HasCustomerStatusChanged() || HasReasonTextChanged())
            if (CustomerStatusChanged || CustomerReasonChanged )
            {
                
                if (WriteCustomerStatusChangeToDB())
                    PutCustomerStatusDataToGlobal();
                else
                    MessageBox.Show(Commons.GetMessageString("ProcessingError"));
                    
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "UpdateCustomerDetails";
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }
            else
            {
                //var dgr = MessageBox.Show(Commons.GetMessageString("NoChangesInForm"), "Warning", MessageBoxButtons.YesNo);
                DialogResult msgResults = MessageBox.Show(NoChangeInCustomerStatusFormMsg,"Warning", MessageBoxButtons.OK);
                if (msgResults == DialogResult.OK)
                {
                    this.NavControlBox.IsCustom = true;
                    this.NavControlBox.CustomDetail = "UpdateCustomerDetails";
                    this.NavControlBox.Action = NavBox.NavAction.BACK;
                }
            }



        }
        #endregion
    }
}
