using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility.Shared;
using Support.Libraries.Objects.Customer;

namespace Support.Forms.Customer.ProductTabs
{
    public partial class AddViewSupportCustomerComment : Form
    {

        private CategoryVO ListOfCategories;

        private CustomerVOForSupportApp CustToEdit;
        //private string displayType;

        private string category;
        private string employeenumber;

        private Form ownerfrm;
        public NavBox NavControlBox;

        #region FORMSTARTUP
        /*__________________________________________________________________________________________*/
        public AddViewSupportCustomerComment()
        {
            this.NavControlBox = new NavBox();
            InitializeComponent();

        }
        /*__________________________________________________________________________________________*/
        private void SetFormDisplay()
        {
            string displayType = Support.Logic.CashlinxPawnSupportSession.Instance.FormDisplayType;

            //this.TxbComment.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if (displayType == "VIEW")
            {
                MapFormDataFromGlobalRecord();
                this.TxbComment.ReadOnly = true;                      // toggle edit
                CmbBoxCategory.Enabled = false;                      // toggel combo box and TextBox
                CmbBoxCategory.Visible = false;
                TxbCategory.Text = this.Category;
                TxbCategory.Enabled = false;
                TxbEmployeeNumber.Text = EmployeeNumber;    // no change 
                TxbEmployeeNumber.Enabled = false;
                BtnAppend.Visible = false;                                  // toggle enable/disable & visiable
                BtnAppend.Enabled = false;
                BtnCancel.Text = "Close";    // toggle text
                this.LblHeader.Text = "View Comment";
            }
            else if(displayType == "APPEND")
            {
                this.TxbComment.Enabled = true;                      // toggle edit
                this.CmbBoxCategory.Enabled = true;                      // toggel combo box and TextBox
                this.TxbCategory.Text = "";
                this.TxbComment.Focus();
                this.TxbEmployeeNumber.Text = EmployeeNumber;    // no change 
                this.BtnAppend.Visible = true;                                  // toggle enable/disable & visiable
                this.BtnAppend.Enabled = true;
                this.BtnCancel.Text = "Cancel";
                this.LblHeader.Text = "Add Comment";

                var catVo = Support.Logic.CashlinxPawnSupportSession.Instance.CustomerCommentCategories;
                List<Category> categoryList = catVo.CommentCategories;
                var bindingSource = new BindingSource();
                bindingSource.DataSource = categoryList;
                CmbBoxCategory.DataSource = bindingSource;
                CmbBoxCategory.DisplayMember = "CategoryName";
                CmbBoxCategory.ValueMember = "CategoryId";
                MapFormDataFromGlobalRecord();
                CmbBoxCategory.SelectedItem = 0;
                CmbBoxCategory.Text = "Select";
            }

        }

        /*__________________________________________________________________________________________*/
        private void AddViewSupportCustomerComment_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;
            
            CustToEdit = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;
            SetFormDisplay();            

        }
        /*__________________________________________________________________________________________*/
        private void MapFormDataForEmptyRecord()
        {

        }

        /*__________________________________________________________________________________________*/
        private void MapFormDataFromGlobalRecord()
        {
            SupportCommentsVO Record = CustToEdit.SupportCommentRecord;

            TxbComment.Text = Record.CommentNote;
            CmbBoxCategory.Text = Record.Category;
            TxbEmployeeNumber.Text = Record.EmployeeNumber;
            this.Category = Record.Category;
            this.EmployeeNumber = Record.EmployeeNumber;
        }
        #endregion
        #region PROPERTIES
        /*__________________________________________________________________________________________*/
        private string Category { get; set; }
        /*__________________________________________________________________________________________*/
        private string EmployeeNumber { get; set; }

        #endregion
        #region EVENTS
        /*__________________________________________________________________________________________*/
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //this.Close();
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "SupportCustomerComment";
            this.NavControlBox.Action = NavBox.NavAction.BACK;
        }
        private string checkNull(string value)
        {
            if (string.IsNullOrEmpty(value))
                value = string.Empty;

            return value;
        }
        private bool validateForm()
        {
            bool isValid = false;
            string commentText = checkNull(this.TxbComment.Text);
            return isValid;
        }

        /*__________________________________________________________________________________________*/
        private void BtnAppend_Click(object sender, EventArgs e)
        {
            string commentText = checkNull(this.TxbComment.Text);
            string empNumber = checkNull(this.TxbEmployeeNumber.Text);
            Category selectedCategory = (Category)this.CmbBoxCategory.SelectedItem;
            string categoryId = selectedCategory.CategoryId.ToString();

            if(commentText.Equals(string.Empty) || categoryId.Equals("0"))
            {
                MessageBox.Show("Please enter Comments and category and submit again.", "Error", MessageBoxButtons.OK);

                if (commentText.Equals(string.Empty))
                    this.TxbComment.Focus();

                if (categoryId.Equals(string.Empty))
                {
                    this.LblCategory.ForeColor = Color.Red;
                    this.LblCategory.Focus();
                }
                return;
            }

            bool retValue = false;
            DialogResult dgr;
            string errorCode = string.Empty;
            string errorMessage = string.Empty;
            try
            {
                do
                {

                    string userId = Support.Logic.CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserID;
                    retValue = new Support.Controllers.Database.Procedures.CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).
                        WriteSupportCustomerCommentToDBData(
                            CustToEdit.CustomerNumber,
                            commentText,
                            userId,
                            categoryId,
                            empNumber,
                            out errorCode,
                            out errorMessage);

                    if (retValue)
                    {
                        var commentRecord = new SupportCommentsVO();
                        commentRecord.CommentNote = commentText;
                        commentRecord.LastUpDateDATE = DateTime.Now;
                        commentRecord.UpDatedBy = Support.Logic.CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserID; ;
                        commentRecord.Category = selectedCategory.CategoryName;//categoryId;
                        commentRecord.EmployeeNumber = empNumber;
                        //CustToEdit.addSupportComment(commentRecord);
                        CustToEdit.insertSupportComment(commentRecord, 0);
                        break;
                    }
                    else
                    {
                        dgr = errorCode.Equals("1") ? MessageBox.Show("Inserting Customer Comments Failed. Please retry or cancel" + ":  " + errorMessage, "Warning", MessageBoxButtons.OK) : MessageBox.Show("Inserting Customer Comments Failed. Please retry or cancel" + ":  " + errorMessage, "Error", MessageBoxButtons.OK);
                    }
                } while (dgr == DialogResult.Retry);
                if (errorCode.Equals("1"))
                //    return;
                //this.Close();
                {
                    this.NavControlBox.IsCustom = true;
                    this.NavControlBox.CustomDetail = "SupportCustomerComment";
                    this.NavControlBox.Action = NavBox.NavAction.BACK;
                }
            }
            catch (ApplicationException aEx)
            {
                //exception already handled at the oracledataaccessor level
                MessageBox.Show(aEx.Message);
            }
            //this.Close();
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "SupportCustomerComment";
            this.NavControlBox.Action = NavBox.NavAction.BACK;
        }

        public void TxbEmployeeNumber_Leave(object sender, EventArgs e)
        {
            string empNumber = checkNull(this.TxbEmployeeNumber.Text);

            if(empNumber.Length > 20)
            {
                MessageBox.Show("Invalid Employee Number, the Employee Number can not be more than 20 characters.");
                return;
            }
        }
        #endregion
    }
}
