using System;


namespace Support.Libraries.Objects.Customer
{
    public class SupportCommentVO
    {
        /*__________________________________________________________________________________________*/
        public SupportCommentVO()
        {
            this.initialize();
        }
        /*__________________________________________________________________________________________*/
        public SupportCommentVO(SupportCommentVO record)
        {
            if (record != null)
            {
                this.CommentNote     = record.CommentNote;
                this.CustomerNumber = record.CustomerNumber;
                this.LastUpDateDATE = record.LastUpDateDATE;
                this.UpDatedBy = record.UpDatedBy;        
            }
        }
        /*__________________________________________________________________________________________*/
        public string CommentNote { get; set; }
        /*__________________________________________________________________________________________*/
        public string CustomerNumber{ get; set; }
        /*__________________________________________________________________________________________*/
        public DateTime LastUpDateDATE { get; set; }
        /*__________________________________________________________________________________________*/
        public string UpDatedBy { get; set; }
        /*__________________________________________________________________________________________*/
        private void initialize()
        {
            this.CommentNote = string.Empty;
            this.CustomerNumber = string.Empty;
            //this.LastUpDateDATE = new DateTime(); //.Today;
            this.UpDatedBy = string.Empty; //  "No Comments Entered"; // string.Empty;
        }
    }
}

#region STRUCT
//[Serializable]
//public struct SupportCommentVO
//{
//   public string CommentNote;      
//   public string CustomerNumber;
//   public DateTime LastUpDateDATE;
//   public string UpDatedBy;

//   public void SuppportCommentVO(
//       string comment,
//       string custnumber,
//       DateTime updateDATE,
//       string UpDatedByWhom
//       )
//   {
//        CommentNote = comment;       
//        CustomerNumber = custnumber;
//        LastUpDateDATE = updateDATE;
//        UpDatedBy         = UpDatedByWhom;
//   }
//}
#endregion

