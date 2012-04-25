/********************************************************************
* CashlinxDesktop
* ViewCustomerInformation
* This form is shown when all the comments pertaining to the customer needs
* to be viewed or edited
* Sreelatha Rengarajan 5/14/2009 Initial version
 * SR 6/29/2010 Fixed an issue wherein the data became editable for
 * a role who did not have the resource when the reset button was clicked
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Customer
{
    public partial class UpdateCommentsandNotes : Form
    {

        private bool _viewCommentsandNotes = false;
        private List<CustomerNotesVO> _custNotes;
        private CustomerVO _custToEdit;
        private CustomerVO _updatedCustomer;
        private string _custNumber;

        string[] _strComments;
        string[] _strCustProdNoteId;
        string[] _strUpdatedComments;
        string[] _strUpdatedCustProdNoteId;
        string[] _strUpdatedUserId;

        private List<CustomerNotesVO> updatedNotes = new List<CustomerNotesVO>();

        private string _strUser;
        private string _strStoreNumber;
        private string _strDate;
        private bool userCanEdit;

        DataTable _notesTable = new DataTable();

        //Will be set by the calling program to determine if the comments should 
        //be shown in view or edit mode. If false, edit mode else view mode
        public bool ViewCommentsAndNotes
        {
            set
            {
                _viewCommentsandNotes = value;
            }
        }


        //The customer object whose data is to be viewed is set by the
        //calling program
        public CustomerVO CustToEdit
        {
            set
            {
                _custToEdit = value;
                _custNotes = _custToEdit.getAllCommentNotes();
                _custNumber = _custToEdit.CustomerNumber;
            }
        }

        public UpdateCommentsandNotes()
        {
            InitializeComponent();
        }

        private void UpdateCommentsandNotes_Load(object sender, EventArgs e)
        {

            if (_viewCommentsandNotes)
            {
                this.customButtonCancel.Visible = false;
                this.customButtonClose.Visible = true;
                this.customButtonReset.Visible = false;
                this.customButtonSubmit.Visible = false;
                this.customDataGridViewComments.ReadOnly = true;
            }
            else
            {

                    this.customButtonCancel.Visible = true;
                    this.customButtonClose.Visible = false;
                    this.customButtonReset.Visible = true;
                    this.customButtonSubmit.Visible = true;
                    this.customDataGridViewComments.ReadOnly = false;
                
            }
            _strUser = GlobalDataAccessor.Instance.DesktopSession.UserName;
            _strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            _strDate = ShopDateTime.Instance.ShopDate.ToString();
            //Load the comments data
            LoadCommentsData();
            //check the privileges of the logged in user to determine
            //if the user can edit comments or only add comments
            if (!(SecurityProfileProcedures.CanUserModifyResource("EDITCOMMENTS", GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, GlobalDataAccessor.Instance.DesktopSession)))
            {
                userCanEdit = false;
                foreach (DataGridViewRow dgvr in customDataGridViewComments.Rows)
                {
                    dgvr.ReadOnly = true;
                }

            }
            else
                userCanEdit = true; 

        }

        private void LoadCommentsData()
        {
            if (_viewCommentsandNotes)
            {
                customDataGridViewComments.Columns[0].SortMode = DataGridViewColumnSortMode.Automatic;
                customDataGridViewComments.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
                customDataGridViewComments.Columns[2].SortMode = DataGridViewColumnSortMode.Automatic;
                customDataGridViewComments.Columns[3].SortMode = DataGridViewColumnSortMode.Automatic;
                this.customButtonAdd.Visible = false;
            }
            else
            {
                customButtonAdd.Visible = true;
                customDataGridViewComments.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                customDataGridViewComments.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                customDataGridViewComments.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                customDataGridViewComments.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

            }


            foreach (CustomerNotesVO custnote in _custNotes)
            {

                string shopNumber = custnote.StoreNumber;
                string commentDate = custnote.UpdatedDate.ToString();
                string comment = custnote.ContactNote;
                string commentEnteredBy = custnote.CreatedBy;
                string custprodnoteid = custnote.CustomerProductNoteId;

                DataGridViewTextBoxCell shopcell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell commentdatecell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell commentcell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell commententeredbycell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell custprodnoteidcell = new DataGridViewTextBoxCell();
                shopcell.Value = shopNumber;
                commentdatecell.Value = commentDate;
                commentcell.Value = comment;
                commententeredbycell.Value = commentEnteredBy;
                custprodnoteidcell.Value = custprodnoteid;

                commentcell.MaxInputLength = 1024;
                commentcell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

                DataGridViewRow dgRow;
                using (dgRow = new DataGridViewRow())
                {
                    dgRow.Cells.Insert(0, shopcell);
                    dgRow.Cells.Insert(1, commentdatecell);
                    dgRow.Cells.Insert(2, commentcell);
                    dgRow.Cells.Insert(3, commententeredbycell);
                    dgRow.Cells.Insert(4, custprodnoteidcell);

                    customDataGridViewComments.Rows.Add(dgRow);
                }
            }
  

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            //Reset only if the user has privileges
            if (userCanEdit)
            {
                customDataGridViewComments.Rows.Clear();
                LoadCommentsData();
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (userCanEdit)
            {
                //Number of rows
                int numRows = customDataGridViewComments.Rows.Count;

                //Array inputs
                _strComments = new string[numRows];
                _strCustProdNoteId = new string[numRows];


                //Get the values from each row
                int j = 0;
                for (int i = 0; i < customDataGridViewComments.Rows.Count; i++)
                {

                    string comment = (string)customDataGridViewComments.Rows[i].Cells[2].EditedFormattedValue;
                    string custprodnoteid = (string)customDataGridViewComments.Rows[i].Cells[4].Value;
                    if (comment != string.Empty)
                    {
                        _strComments[i] = comment;
                        if (custprodnoteid == null)
                            _strCustProdNoteId[i] = "";
                        else
                            _strCustProdNoteId[i] = custprodnoteid;

                        j++;
                    }

                }
                if (j > 0)
                {
                    if (commentDataChanged())
                    {
                        DialogResult dgr;
                        do
                        {
                            _strUpdatedComments = new string[updatedNotes.Count];
                            _strUpdatedCustProdNoteId = new string[updatedNotes.Count];
                            _strUpdatedUserId = new string[updatedNotes.Count];
                            int k = 0;
                            foreach (CustomerNotesVO note in updatedNotes)
                            {
                                _strUpdatedUserId[k] = _strUser;
                                _strUpdatedComments[k] = note.ContactNote;
                                _strUpdatedCustProdNoteId[k] = note.CustomerProductNoteId;
                                k++;
                            }
                            string errorCode;
                            string errorMsg;
                            bool updateCommentData = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).UpdateCustomerComments(_strUpdatedUserId, _custNumber, _strUpdatedComments, _strUpdatedCustProdNoteId, _strStoreNumber, out _notesTable, out errorCode, out errorMsg);
                            if (updateCommentData)
                            {
                                MessageBox.Show("Customer Comments update successful");

                                //Set updated notes info for the customer
                                LoadUpdatedNotesInCustomerObject();
                                Form ownerForm = this.Owner;
                                if (ownerForm.GetType() == typeof(ViewCustomerInformation))
                                {

                                    ((ViewCustomerInformation)ownerForm).UpdatedCustomerToView = _updatedCustomer;
                                    ((ViewCustomerInformation)ownerForm).ShowUpdates = true;
                                }
                                break;
                            }
                            else
                            {
                                dgr = MessageBox.Show(Commons.GetMessageString("CustCommentsUpdateFailure"), "Error", MessageBoxButtons.RetryCancel);
                            }
                        } while (dgr == DialogResult.Retry);

                    }
                    else
                    {
                        MessageBox.Show(Commons.GetMessageString("NoChangesInForm"));
                    }
                }
                else
                {
                    MessageBox.Show(Commons.GetMessageString("NoChangesInForm"));
                }

                this.Close();
                this.Dispose(true);
            }

        }

        private void LoadUpdatedNotesInCustomerObject()
        {
            //Set the Notes data for the customer
            _updatedCustomer = new CustomerVO();
            if (_notesTable != null)
            {

                foreach (DataRow note in _notesTable.Rows)
                {
                    var commentDate = Utilities.GetDateTimeValue(note.ItemArray[(int)customernotesrecord.CREATIONDATE], DateTime.MaxValue);
                    var contactDate = Utilities.GetDateTimeValue(note.ItemArray[(int)customernotesrecord.CONTACTDATE], DateTime.MaxValue);
                    var commentUpdateDate = Utilities.GetDateTimeValue(note.ItemArray[(int)customernotesrecord.UPDATEDATE], DateTime.MaxValue);
                    string storeNumber = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.STORENUMBER], "");
                    string contactResult = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.CONTACTRESULT], "");
                    string contactStatus = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.CONTACTSTATUS], "");
                    string comments = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.CONTACTNOTE], "");
                    string commentsby = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.CREATEDBY], "");
                    string custProdNoteId = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.CUSTOMERPRODUCTNOTEID], "");
                    string commentCode = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.CUSTOMERNOTESCODE], "");
                    var custnote = new CustomerNotesVO(contactResult, contactStatus, comments, contactDate, commentDate, commentUpdateDate, commentsby, commentCode, custProdNoteId, storeNumber);
                    _updatedCustomer.addNotes(custnote);
                }

            }

        }

        private bool commentDataChanged()
        {
            

            for (int i = 0; i < _strComments.Length; i++)
            {
                if ((!string.IsNullOrEmpty(_strComments[i])) && (i >= _custNotes.Count || _strComments[i] != _custNotes[i].ContactNote))
                {
                    CustomerNotesVO newnote = new CustomerNotesVO();
                    newnote.ContactNote = _strComments[i];
                    newnote.CustomerProductNoteId = _strCustProdNoteId[i];
                    newnote.CreatedBy = _strUser;
                    newnote.CreationDate = ShopDateTime.Instance.ShopDate;
                    newnote.UpdatedDate = ShopDateTime.Instance.ShopDate;
                    newnote.StoreNumber = _strStoreNumber;
                    updatedNotes.Add(newnote);
                }
            }
            if (updatedNotes.Count > 0)
            {
                return true;
            }
            else
                return false;


        }

        private void dataGridViewComments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //When a cell is clicked, if the comment cell is being clicked is in a new row, pre populate
            //store number, date and user ID
            if (e.RowIndex >= _custNotes.Count && !(_viewCommentsandNotes))
            {
                if (e.ColumnIndex == 2)
                {
                    customDataGridViewComments.Rows[e.RowIndex].Cells[0].Value = _strStoreNumber;
                    customDataGridViewComments.Rows[e.RowIndex].Cells[1].Value = _strDate;
                    customDataGridViewComments.Rows[e.RowIndex].Cells[3].Value = _strUser;
                    
                }
                
            }

        }

        private void dataGridViewComments_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //When a cell is clicked, if the comment cell is being clicked is in a new row, pre populate
            //store number, date and user ID
            if (e.RowIndex >= _custNotes.Count && !(_viewCommentsandNotes))
            {
                if (e.ColumnIndex == 2)
                {
                    customDataGridViewComments.Rows[e.RowIndex].Cells[0].Value = _strStoreNumber;
                    customDataGridViewComments.Rows[e.RowIndex].Cells[1].Value = _strDate;
                    customDataGridViewComments.Rows[e.RowIndex].Cells[3].Value = _strUser;
                }
            }

        }

        private void customButtonAdd_Click(object sender, EventArgs e)
        {
            if (customDataGridViewComments.Rows.Count > 0)
            {
                if (!(customDataGridViewComments.Rows[customDataGridViewComments.Rows.Count - 1].Cells[0].Value == null))
                    customDataGridViewComments.Rows.Add();
            }
            else
                customDataGridViewComments.Rows.Add();
            
        }







    }
}
