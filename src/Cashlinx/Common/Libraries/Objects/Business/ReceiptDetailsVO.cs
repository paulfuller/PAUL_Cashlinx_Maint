#region FileHeaderRegion

// /************************************************************************
//  * Namespace: PawnObjects.Pawn
//  * Class:     ReceiptDetailsVO
//  * 
//  * Description: Contains key receipt data necessary for receipt print
//  * 
//  * History:
//  * Date                Author            Reason                                 
//  *------------------------------------------------------------------
//  * 09/10/09            GJL               Reformatted object code
//  * 09/10/09            GJL               Added constructor and lists 
//  * 09/11/09            GJL               Cleaned up code per ReSharper
//  * **********************************************************************/

#endregion

using System;
using System.Collections.Generic;

namespace Common.Libraries.Objects.Business
{
    public class ReceiptDetailsVO
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReceiptDetailsVO()
        {
            this.ReceiptNumber = string.Empty;
            this.ReceiptType = string.Empty;
            this.ReceiptType = string.Empty;
            this.UserId = string.Empty;
            this.ReceiptDate = DateTime.Now;
            this.RefAmounts = new List<string>();
            this.RefDates = new List<string>();
            this.RefEvents = new List<string>();
            this.RefNumbers = new List<string>();
            this.RefStores = new List<string>();
            this.RefTypes = new List<string>();
            this.RefTimes = new List<string>();
        }

        public string ReceiptNumber { get; set; }
        public string StoreNumber { get; set; }
        public string ReceiptType { get; set; }
        public string UserId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public List<string> RefNumbers { get; set; }
        public List<string> RefDates { get; set; }
        public List<string> RefTypes { get; set; }
        public List<string> RefEvents { get; set; }
        public List<string> RefAmounts { get; set; }
        public List<string> RefStores { get; set; }
        public List<string> RefTimes { get; set; }
    }
}