using System;
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
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility;
using Support.Libraries.Objects.Customer;
using Support.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Support.Controllers.Database.Procedures;
using Support.Libraries.Utility;

namespace Support.Forms.Customer.ProductTabs
{
    public partial class SupportCustomerComments : Form
    {

        private const int dgColumnComment        = 0;
        private const int dgColumnDate              = 1;
        private const int dgColumnUpDatedBy     = 2;
        private const int dgColumnCategory      = 3;
        private const int dgColumnEmpNumber = 4;

        private string userid;
        private string customerfirstandlastname;
        private string customerssn;
        private string commenttext;
        private string customernumber;
        private CustomerVOForSupportApp _custToEdit;
        private SupportCommentsVO record;

        //private List<SupportCommentsVO> _supportCommentRecord = new List<SupportCommentsVO>();
        private List<SupportCommentsVO> _supportCommentsList; 
        

        private string DBReturnCode = "";
        private string errorMessage = "";

        private Form ownerfrm;
        public NavBox NavControlBox;

        #region FORMSTARTUP
        /*__________________________________________________________________________________________*/
        public SupportCustomerComments()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();

            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;

            CustToEdit = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;
            Record = CustToEdit.SupportCommentRecord;   // init a record state
            ReadSupportCommentFromDb();
            //if (DgvCustomerComments.Rows.Count > 0)
           // {
           //     DgvCustomerComments.Rows[Record.DGRowIndex].Selected = true; // set the rowindex 
           // }
        }
        /*__________________________________________________________________________________________*/
        private void SupportCustomerComments_Load(object sender, EventArgs e)
        {
            //ownerfrm = this.Owner;
            //this.NavControlBox.Owner = this;

            //CustToEdit = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;
            //Record = CustToEdit.SupportCommentRecord;   // init a record state
            //ReadSupportCommentFromDb();
            //DgvCustomerComments.Rows[Record.DGRowIndex].Selected = true;  // set the rowindex 
            if (DgvCustomerComments.Rows.Count > 0)
            {
                DgvCustomerComments.Rows[Record.DGRowIndex].Selected = true; // set the rowindex 
            }
        }
        #endregion
        #region MEMBER PROPERTIES
        /*__________________________________________________________________________________________*/
        private SupportCommentsVO Record { get; set; }
        /*__________________________________________________________________________________________*/
        private string CustomerSSN
        {
            get { return customerssn; }
            set {
                if (customerssn == null)
                    customerssn = CustToEdit.SocialSecurityNumber;

                customerssn = value;
                }
        }
        /*__________________________________________________________________________________________*/
        private string CustomerFirstAndLastName
        {
            get { return customerfirstandlastname; }
            set
            {
                if (customerfirstandlastname == null)
                    customerfirstandlastname = CustToEdit.CustomerName;

                customerfirstandlastname = value;
            }
        }
        /*__________________________________________________________________________________________*/
        private string CustomerNumber
        {
            get
            {
                if (customernumber == null)
                    customernumber = CustToEdit.CustomerNumber;

                return customernumber;
            }
            set
            { customernumber = value; }
        }

        /*__________________________________________________________________________________________*/
        private CustomerVOForSupportApp CustToEdit
        {
            set
            {
                _custToEdit = value;
                _supportCommentsList = _custToEdit.SupportComments; 
            }
            get
            {
                return _custToEdit;
            }
        }
        #endregion
        #region GETDATA
        /*__________________________________________________________________________________________*/
        private void LoadCommentsDateToGrid()
        {
            this.customerDisplayLabel.Text = CustToEdit.FirstName + " " + CustToEdit.MiddleInitial + " " + CustToEdit.LastName;
            this.SSNDisplayLabel.Text = CustToEdit.SocialSecurityNumber;

            foreach ( SupportCommentsVO record in  _supportCommentsList )
            {
                string       comment    = record.CommentNote;
                DateTime date           = record.LastUpDateDATE;
                string       editedby       = record.UpDatedBy;
                string       category       = record.Category;
                string       empnumber = record.EmployeeNumber;

                DataGridViewTextBoxCell grComment   = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell grDate              = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell grEditedBy       = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell grCategory              = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell grEmpNumber      = new DataGridViewTextBoxCell();

                grComment.Value = comment;

                if (!date.Equals(DateTime.MinValue.FormatDate()) || !date.Equals(DateTime.MaxValue.FormatDate()))
                    grDate.Value = date.FormatDate();
                else
                    grDate.Value = date;

                grEditedBy.Value = editedby;
                grCategory.Value = category;
                grEmpNumber.Value = empnumber;

                grComment.MaxInputLength = 4000;
                grComment.Style.Alignment = DataGridViewContentAlignment.TopLeft;

                DataGridViewRow dgRow;
                 using ( dgRow = new DataGridViewRow())
                 {

                     if (comment.Length > 75)
                     {
                         dgRow.Height = 42;
                     }

                     dgRow.Cells.Insert(dgColumnComment, grComment);
                     dgRow.Cells.Insert(dgColumnDate          , grDate       );    
                     dgRow.Cells.Insert(dgColumnUpDatedBy  ,grEditedBy    ); 
                     dgRow.Cells.Insert(dgColumnCategory     ,grCategory    ); 
                     dgRow.Cells.Insert(dgColumnEmpNumber,grEmpNumber); 

                 }

                DgvCustomerComments.Rows.Add(dgRow);

                /* Removed from the form init code
                this.DgvCustomerComments.RowHeightChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.DgvCustomerComments_RowHeightChanged);
                this.DgvCustomerComments.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvCustomerComments_RowPostPaint);
                this.DgvCustomerComments.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DgvCustomerComments_Scroll);
                this.DgvCustomerComments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
                this.DgvCustomerComments.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DgvCustomerComments_CellPainting);
                 */
            }
        }
        /*__________________________________________________________________________________________*/
        private Boolean ReadSupportCommentFromDb()
        {
            Boolean retValue = true;

            if (!_custToEdit.SupportComments.Any())
            {
                var customerDBProceduresForSupport =
                    new Support.Controllers.Database.Procedures.CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession);
                var comments = new DataTable();
                retValue = customerDBProceduresForSupport.getSupportCustomerCommentsFromDBData(
                    this.CustomerNumber,
                    out comments,
                    out DBReturnCode,
                    out errorMessage);

                if (retValue)
                {
                    _custToEdit.SupportComments.Clear();
                    CustomerProcedures.getCustomerCommentsDataInObject(comments, _custToEdit);
                }
            }
            
            LoadCommentsDateToGrid();
            return (retValue);
        }
        #endregion
        #region EVENTS
        /*__________________________________________________________________________________________*/
        private void btnAddComment_Click(object sender, EventArgs e)
        {
            Form currForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(this);
            //if (currForm.GetType() == typeof(UpdateAddress))
            Boolean ExitForm = (currForm.GetType() == typeof(SupportCustomerComments));

            if (ExitForm)
            {
                Support.Logic.CashlinxPawnSupportSession.Instance.FormDisplayType = "APPEND";
                CustToEdit.SupportCommentRecord = null;
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "AddViewSupportCustomerComment";
                this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
            }
        }
        /*__________________________________________________________________________________________*/
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Form currForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(this);
            //if (currForm.GetType() == typeof(UpdateAddress))
            Boolean ExitForm = (currForm.GetType() == typeof(SupportCustomerComments));

            if (ExitForm)
            {
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "SupportCustomerComment";
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }

        }
        /*__________________________________________________________________________________________*/
        private void DgvCustomerComments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Form currForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(this);
            Boolean ExitForm = (currForm.GetType() == typeof(SupportCustomerComments));

            if (ExitForm)
            {
                //SupportCommentsVO Record;

                Support.Logic.CashlinxPawnSupportSession.Instance.FormDisplayType = "VIEW";

                Record = CustToEdit.SupportCommentRecord;

                DataGridViewRow RowSelect = DgvCustomerComments.Rows[e.RowIndex];

                Record.DGRowIndex = e.RowIndex;
                Record.Category = Utilities.GetStringValue(RowSelect.Cells[(int)customercommentrecord.CATEGORY].Value, "");
                Record.CommentNote = Utilities.GetStringValue(RowSelect.Cells[(int)customercommentrecord.COMMENTS].Value, "");
                Record.EmployeeNumber = Utilities.GetStringValue(RowSelect.Cells[(int)customercommentrecord.EMPLOYEE_NBR].Value, "");
                Record.LastUpDateDATE = Utilities.GetDateTimeValue(RowSelect.Cells[(int)customercommentrecord.DATA_MADE].Value, DateTime.Now);
                Record.UpDatedBy = Utilities.GetStringValue(RowSelect.Cells[(int)customercommentrecord.MADE_BY].Value, "");


                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "AddViewSupportCustomerComment";
                this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
            }
        }
        /*__________________________________________________________________________________________*/
        private void DgvCustomerComments_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
        {
            int rowHeight = e.Row.Height;
            if (this.DgvCustomerComments.RowTemplate.Height > rowHeight ) // < rowHeight)
            {
                this.DgvCustomerComments.RowTemplate.Height = rowHeight;
            }
        }
        /*__________________________________________________________________________________________*/
        private void DgvCustomerComments_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                DgvCustomerComments.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                DgvCustomerComments.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            }
        }
        /*__________________________________________________________________________________________*/
        private void DgvCustomerComments_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            int rowHeight = this.DgvCustomerComments.RowTemplate.Height;

            this.DgvCustomerComments.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            int numRows = this.DgvCustomerComments.Rows.Count;
            for (int i = 0; i < numRows; i++)
            {
                this.DgvCustomerComments.Rows[i].Height = 42; 

            }
        }
        /*__________________________________________________________________________________________*/
        private void DgvCustomerComments_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {


            if ((e.ColumnIndex == 0) && (e.FormattedValue != null))
            {
                SizeF sizeGraph = e.Graphics.MeasureString(e.FormattedValue.ToString(), e.CellStyle.Font, e.CellBounds.Width);

                RectangleF cellBounds = e.CellBounds;
                cellBounds.Height = cellBounds.Height;

                if (e.CellBounds.Height > sizeGraph.Height)
                {
                    cellBounds.Y += (e.CellBounds.Height - sizeGraph.Height) / 2;
                }
                else
                {
                    cellBounds.Y += 0; // paddingValue;
                }
                e.PaintBackground(e.ClipBounds, true);

                using (SolidBrush sb = new SolidBrush(e.CellStyle.ForeColor))
                {
                    e.Graphics.DrawString(e.FormattedValue.ToString(), e.CellStyle.Font, sb, cellBounds);
                }
                e.Handled = true;
            }
        }
        #endregion
    }
}
