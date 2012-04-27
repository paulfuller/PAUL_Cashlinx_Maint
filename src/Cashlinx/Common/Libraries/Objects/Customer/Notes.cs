/********************************************************************
* PawnObjects.VO.Customer
* NotesVO
* Notes object that has the notes data of a customer
* Sreelatha Rengarajan 4/1/2009 Updated
*******************************************************************/

using System;

namespace Common.Libraries.Objects.Customer
{
    [Serializable]
    public struct CustomerNotesVO
    {
        public string ContactResult;
        public string ContactStatus;
        public string ContactNote;
        public DateTime ContactDate;
        public DateTime CreationDate;
        public string CreatedBy;
        public string Code;
        public string CustomerProductNoteId;
        public string StoreNumber;
        public DateTime UpdatedDate;

        public CustomerNotesVO(
            string result,
            string status,
            string note,
            DateTime contactDate,
            DateTime creationDate,
            DateTime updateDate,
            string createdBy,
            string code,
            string custProductNoteId,
            string storeNumber
            )
        {
            this.ContactResult = result;
            this.ContactStatus = status;
            this.ContactNote = note;
            this.ContactDate = contactDate;
            this.CreationDate = creationDate;
            this.CreatedBy = createdBy;
            this.Code = code;
            this.CustomerProductNoteId = custProductNoteId;
            this.StoreNumber = storeNumber;
            this.UpdatedDate = updateDate;

        }
    }
}
