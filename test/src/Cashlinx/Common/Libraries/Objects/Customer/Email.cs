/********************************************************************
* PawnObjects.VO.Customer
* CustomerVO
* Customer object - inclues details about email
* Sreelatha Rengarajan 4/1/2009 Updated
*******************************************************************/

using System;

namespace Common.Libraries.Objects.Customer
{
    [Serializable]
    public struct CustomerEmailVO
    {
        public string EmailAddress;
        public string EmailAddressType;
        public string ContactInfoId;

        public CustomerEmailVO(string Email, string EmailType, string contInfoId)
        {
            this.EmailAddress = Email;
            this.EmailAddressType = EmailType;
            this.ContactInfoId = contInfoId;
        }
    }
}
