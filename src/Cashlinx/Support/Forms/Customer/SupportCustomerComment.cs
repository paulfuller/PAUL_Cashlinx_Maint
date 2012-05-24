using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms.Components;
using Support.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;
using Support.Controllers.Database.Procedures;


namespace Support.Forms.Customer
{
    public partial class SupportCustomerComment : Form
    {
        private Boolean UserEnteredIntoTextBox;

        private string userid;
        private string commenttext;
        private string customernumber;
        private CustomerVOForSupportApp _custToEdit;

        private SupportCommentVO _supportCommentRecord;

        private string DBReturnCode = "";
        private string errorMessage = "";

        private Form ownerfrm;
        public NavBox NavControlBox;

        #region FORMSTARTUP
        /*__________________________________________________________________________________________*/
        public SupportCustomerComment()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }
        /*__________________________________________________________________________________________*/
        private void SupportCustomerComment_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;

            CustToEdit = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;
            
            if (string.IsNullOrEmpty(_supportCommentRecord.CustomerNumber))
                ReadSupportCommentFromDb();
            else
            {   // load existing data
                this.CommentText = _supportCommentRecord.CommentNote;  // save orginal copy to compare for exit 
                MapFormDataFromGlobalProperties();

                //this.TxbComment.Text = _supportCommentRecord.CommentNote;
                //this.TxbLastEditedBy.Text = _supportCommentRecord.UpDatedBy;
                //this.TxbUpdatedDate.Text = _supportCommentRecord.LastUpDateDATE.FormatDate();
            }
        }
        #endregion
        #region MEMBER PROPERTIES
        /*__________________________________________________________________________________________*/
        private Boolean userenteredintotextbox
        {
            get
            {return userenteredintotextbox;}

            set
            {
                if (userenteredintotextbox == null)
                    userenteredintotextbox = false;

                userenteredintotextbox = value;
            }
        }
        /*__________________________________________________________________________________________*/
        private string UserId
        {
            get
            {
                if (userid == null)
                    userid = GlobalDataAccessor.Instance.DesktopSession.DisplayName; //.UserName;

                return userid;
            }
            set{ userid = value; }
        }
        /*__________________________________________________________________________________________*/
        private string CommentText { 
            get
            {
                if (commenttext == null)
                    commenttext = "";

                return commenttext;
            }
            set
            {commenttext = value.Replace(" ","");}
        }
        /*__________________________________________________________________________________________*/
        private string CustomerNumber 
        { 
                get
                    {   
                        if (customernumber == null)
                            customernumber =  CustToEdit.CustomerNumber;

                    return customernumber;
                    }
                set
                    {customernumber = value;}
        }
        /*__________________________________________________________________________________________*/
        private CustomerVOForSupportApp CustToEdit
        {
            set
            {
                _custToEdit = value;
                _supportCommentRecord = _custToEdit.SupportComment; 
                this.CustomerNumber = _custToEdit.CustomerNumber;
            }
            get
            {
                return _custToEdit;
            }
        }   

        #endregion
        #region BOOLEAN
        /*__________________________________________________________________________________________*/
        private Boolean HasCommentChanged()
        {
            Boolean retVal = false;
            string text = this.TxbComment.Text; 

            text = text.Replace(" ","").TrimStart().TrimEnd();

            if ( CommentText.Length != text.Length )
                retVal = true;

            return ( retVal );
        }
        #endregion
        #region GETDATA
        /*__________________________________________________________________________________________*/
        private void MapGlobalPropertiesFromForm()
        {
            _supportCommentRecord.CommentNote =                               this.TxbComment.Text ;    
            _supportCommentRecord.UpDatedBy =                                   this.UserId;
            _supportCommentRecord.LastUpDateDATE = DateTime.Parse( DateTime.Today.FormatDate() );
        }
        /*__________________________________________________________________________________________*/
        private void MapFormDataFromGlobalProperties( )
        {
            //DateTime tempDate;
            _supportCommentRecord.CustomerNumber = this.CustomerNumber;       // Did not get customer number in the DBRead
            this.CommentText = _supportCommentRecord.CommentNote;               // save orginal copy to compare for exit 
            this.TxbComment.Text = _supportCommentRecord.CommentNote;
            this.TxbLastEditedBy.Text = _supportCommentRecord.UpDatedBy;
            //this.TxbUpdatedDate.Text = string.IsNullOrEmpty(_supportCommentRecord.LastUpDateDATE)?_supportCommentRecord.LastUpDateDATE.FormatDate():"" ;

            if (DateNullIf( _supportCommentRecord.LastUpDateDATE ))
                this.TxbUpdatedDate.Text = "";
            else
                this.TxbUpdatedDate.Text = _supportCommentRecord.LastUpDateDATE.FormatDate();
                //DateTime.TryParse(_supportCommentRecord.LastUpDateDATE.FormatDate(), out tempDate) ? tempDate.FormatDate() : "";
        }
        /*__________________________________________________________________________________________*/
        private bool DateNullIf(DateTime date)
        {
            return date.Ticks == 0; // true; //date = "

        }
        /*__________________________________________________________________________________________*/
        private Boolean ReadSupportCommentFromDb()
        {
            Boolean retValue = false;

            var customerDBProceduresForSupport = 
                 new Support.Controllers.Database.Procedures.CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession);

            if ((retValue = customerDBProceduresForSupport.ReadSupportCustomerCommentToDBData(
                this.CustomerNumber,
                _supportCommentRecord,
               out DBReturnCode,
               out errorMessage)))

                MapFormDataFromGlobalProperties();

            return (retValue);
        }
        /*__________________________________________________________________________________________*/
        private Boolean WriteSupportCommetToDb()
        {
            Boolean retValue = false;

            Support.Controllers.Database.Procedures.CustomerDBProcedures DBProceduresForSupport =
                 new Support.Controllers.Database.Procedures.CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession);
            
            //Prep the message 
            PrepMessageBeforeSave();

            retValue = DBProceduresForSupport.WriteSupportCustomerCommentToDBData(
                this.CustomerNumber,  
                this.TxbComment.Text,
                this.UserId,                
                out DBReturnCode,
                out errorMessage);

            return ( retValue );
        }
        /*__________________________________________________________________________________________*/
        private void PrepMessageBeforeSave()
        {
            this.TxbComment.Text = this.TxbComment.Text.TrimStart().TrimEnd();

            // inserts username and datestamp to comment
            //if (!(string.IsNullOrEmpty(this.TxbComment.Text)))
            //    this.TxbComment.Text = this.TxbComment.Text + "\r\n" +
            //        (string.IsNullOrEmpty(_supportCommentRecord.UpDatedBy) ? _supportCommentRecord.UpDatedBy : this.UserId) + " : " +
            //        DateTime.Now.FormatDateWithTimeZone().Substring(0,20);
        }
        #endregion
        #region EVENTS
        /*__________________________________________________________________________________________*/
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            string VerifySaveChangeInCustomerNoteMsg = "Your change to the customers note will over write the previous note in the Database.  Are you sure you want to SUBMIT?";

            if (HasCommentChanged() ) //|| this.UserEnteredIntoTextBox)
            {
                DialogResult msgResults = MessageBox.Show(VerifySaveChangeInCustomerNoteMsg,"Warning", MessageBoxButtons.YesNo);
                if (msgResults == DialogResult.Yes)
                {
                    if (WriteSupportCommetToDb())
                        MapGlobalPropertiesFromForm();
                }
            }

            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "UpdateCustomerDetails";
            this.NavControlBox.Action = NavBox.NavAction.BACK;
        }
        /*__________________________________________________________________________________________*/
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Boolean ExitForm = true;
            string VerifyCancelChangeInCustomerNoteMsg = "You changed the customers note and CANCEL will not save your changes.  Are you sure you want to CANCEL?";

            if (HasCommentChanged()) // || this.UserEnteredIntoTextBox )
            {
                DialogResult msgResults = MessageBox.Show(VerifyCancelChangeInCustomerNoteMsg, "Warning", MessageBoxButtons.YesNo);
                if (msgResults == DialogResult.No)
                    ExitForm = false;
            }

            if (ExitForm)
                {
                    this.NavControlBox.IsCustom = true;
                    this.NavControlBox.CustomDetail = "UpdateCustomerDetails";
                    this.NavControlBox.Action = NavBox.NavAction.BACK;
                }
        }
        /*__________________________________________________________________________________________*/
        private void TxbComment_TextChanged(object sender, EventArgs e)
        {
            //UserEnteredIntoTextBox = true;
        }
        #endregion


    }
}
