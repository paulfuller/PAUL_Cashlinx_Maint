#define USECLASS
using System;

namespace Support.Libraries.Objects.Customer
{
#if USECLASS
    #region CLASS

    public class SupportCommentsVO
    {
        /*__________________________________________________________________________________________*/
        public SupportCommentsVO()
        {
            this.initialize();
        }
        /*__________________________________________________________________________________________*/
        public SupportCommentsVO(SupportCommentsVO record)
        {
            if (record != null)
            {
                this.CommentNote     = record.CommentNote;
                this.EmployeeNumber = record.EmployeeNumber;
                this.CustomerNumber = record.CustomerNumber;
                this.LastUpDateDATE = record.LastUpDateDATE;
                this.UpDatedBy = record.UpDatedBy;
                this.Category = record.Category;
            }
        }
        /*__________________________________________________________________________________________*/
        public int DGRowIndex { get; set; }
        /*__________________________________________________________________________________________*/
        public string CommentNote { get; set; }
        /*__________________________________________________________________________________________*/
        public string EmployeeNumber { get; set; }
        /*__________________________________________________________________________________________*/
        public string CustomerNumber{ get; set; }
        /*__________________________________________________________________________________________*/
        public DateTime LastUpDateDATE { get; set; }
        /*__________________________________________________________________________________________*/
        public string UpDatedBy { get; set; }
        /*__________________________________________________________________________________________*/
        public string Category { get; set; }
        /*__________________________________________________________________________________________*/
        private void initialize()
        {
            this.CommentNote = string.Empty;
            this.EmployeeNumber = string.Empty;
            this.CustomerNumber = string.Empty;
            this.DGRowIndex = 0;
            //this.LastUpDateDATE = new DateTime(); //.Today;
            this.UpDatedBy = string.Empty; //  "No Comments Entered"; // string.Empty;
            this.Category = string.Empty;
        }
    }

#endregion
#else
    #region STRUCT
[Serializable]
public struct SupportCommentsVO
{
    public string CommentNote;
    public string EmployeeNumber;
    public DateTime LastUpDateDATE;
    public string UpDatedBy;
    public string Category;


    public void SuppportCommentVO(
        string comment,
        string employeenumber,
        DateTime updateDATE,
        string UpDatedByWhom,
        string categroy
        )
    {
        CommentNote = comment;
        EmployeeNumber = employeenumber;
        LastUpDateDATE = updateDATE;
        UpDatedBy = UpDatedByWhom;
        Category = Category;
    }
}
#endregion
#endif
}



