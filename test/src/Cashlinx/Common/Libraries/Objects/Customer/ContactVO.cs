/********************************************************************
* PawnObjects.VO.Customer
* ContactVO
* Contact object that has the phone details of a customer
* Sreelatha Rengarajan 4/1/2009 Updated
*******************************************************************/

using System;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Customer
{
    [Serializable]
    public class ContactVO
    {
        public string ContactType  { get; set; }
        public string ContactAreaCode { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactExtension { get; set; }
        public string TelecomNumType { get; set; }
        public string CountryDialNumCode { get; set; }
        public string TeleusrDefText { get; set; }
        

 

        public ContactVO()
        {
            this.initialize();
        }

  
        public ContactVO(ContactVO cvo)
        {
            if (cvo != null)
            {
                this.ContactType = cvo.ContactType;
                this.ContactAreaCode = cvo.ContactAreaCode;
                this.ContactExtension = cvo.ContactExtension;
                this.ContactPhoneNumber = cvo.ContactPhoneNumber;
                this.TelecomNumType = cvo.TelecomNumType;
                this.CountryDialNumCode = cvo.CountryDialNumCode;
                this.TeleusrDefText = cvo.TeleusrDefText;
            }
            else
            {
                this.initialize();
            }
        }

        private void initialize()
        {
            this.ContactType = string.Empty;
            this.ContactAreaCode = string.Empty;
            this.ContactExtension = string.Empty;
            this.ContactPhoneNumber = string.Empty;
            this.TelecomNumType = string.Empty;
            this.CountryDialNumCode = string.Empty;
            this.TeleusrDefText = string.Empty;
        }


        /// <summary>
        /// Returns the values HOME, CELL or WORK based on the contacttype and telecomnumtype
        /// </summary>
        /// <returns></returns>
        public string TypeOfContact()
        {
            if (this.ContactType == CustomerPhoneTypes.HOME_NUMBER && this.TelecomNumType == CustomerPhoneTypes.VOICE_NUMBER)
                return CustomerPhoneTypes.HOME_NUMBER;
            if (this.ContactType == CustomerPhoneTypes.HOME_NUMBER && this.TelecomNumType == CustomerPhoneTypes.MOBILE_NUMBER)
                return CustomerPhoneTypes.MOBILE_NUMBER;
            if (this.ContactType == CustomerPhoneTypes.WORK_NUMBER && this.TelecomNumType == CustomerPhoneTypes.VOICE_NUMBER)
                return CustomerPhoneTypes.WORK_NUMBER;
            if (this.ContactType == CustomerPhoneTypes.HOME_NUMBER && this.TelecomNumType == CustomerPhoneTypes.FAX_NUMBER)
                return CustomerPhoneTypes.FAX_NUMBER;
            if (this.ContactType == CustomerPhoneTypes.HOME_NUMBER && this.TelecomNumType == CustomerPhoneTypes.PAGER_NUMBER)
                return CustomerPhoneTypes.PAGER_NUMBER;
            return string.Empty;
        }

    }
}
