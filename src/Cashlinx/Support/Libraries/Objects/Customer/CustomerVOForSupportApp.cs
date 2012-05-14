/********************************************************************
* PawnObjects.VO.Customer
* CustomerVO
* Customer object - inclues identities, phone and address, email and notes
* Sreelatha Rengarajan 4/1/2009 Updated
 * SR 4/6/2010 IN addidentity set all the other identities except the one being added as latest - PWNU00000571
************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;

namespace Support.Libraries.Objects.Customer
{
    // WCM 4/26/12  Change the SupportCommentsVO type from single instance of a class to List with struct
    /*__________________________________________________________________________________________*/

    [Serializable]
    public class CustomerVOForSupportApp : CustomerVO
    {
        //Nested VO attributes
        private List<AddressVO> addresses;
        private List<ContactVO> contacts;
        private List<IdentificationVO> identities;
        private List<CustomerEmailVO> emails;
        private List<CustomerNotesVO> notes;
        private List<StoreCreditVO> storecredit;
        private List<SupportCommentsVO> comments;

        private SupportCommentsVO supportcommentrecord;

        /*__________________________________________________________________________________________*/
        public CustomerVOForSupportApp()
        {
            initialize();
            InitSubClassVO();
        }

        /*__________________________________________________________________________________________*/
        private void InitSubClassVO()
        {

            this.comments = new List<SupportCommentsVO>(); //SupportCommentVO();

        }

        #region EXTENDED CUSTOMERVO PROPERTIES
        //Simple attributes / accessors (Properties)
        public string MaritalStatus { get; set; }

        public string SpouseFirstName { get; set; }

        public string SpouseLastName { get; set; }

        public string SpouseSsn { get; set; }

        public string CustSequenceNumber { get; set; }

        public DateTime PrivacyNotificationDate { get; set; }

        public string OptOutFlag { get; set; }

        public string Status { get; set; }

        public string ReasonCode { get; set; }

        public DateTime LastVerificationDate { get; set; }

        public DateTime NextVerificationDate { get; set; }

        public DateTime CoolingOffDatePDL { get; set; }

        public DateTime CustomerSincePDL { get; set; }

        public string SpanishForm { get; set; }

        public string PRBC { get; set; }

        public string PlanBankruptcyProtection { get; set; }


        public int Years { get; set; }

        public int Months { get; set; }

        public string OwnHome { get; set; }

        public int MonthlyRent { get; set; }

        public string MilitaryStationedLocal { get; set; }

        #endregion

        /*__________________________________________________________________________________________*/

        public List<SupportCommentsVO> SupportComments
        {
            get { return comments; }
            set { comments = value; }
        }

        /*__________________________________________________________________________________________*/

        public void addSupportComment(SupportCommentsVO vo)
        {
            if (this.SupportComments.Contains(vo))
                return;
            this.SupportComments.Add(vo);
        }

        public void insertSupportComment(SupportCommentsVO vo, int index)
        {
            if (this.SupportComments.Contains(vo))
                return;
            this.SupportComments.Insert(index, vo);
        }

        /*__________________________________________________________________________________________*/
        public SupportCommentsVO SupportCommentRecord
        {
            get
            {
                if (supportcommentrecord == null)
                    supportcommentrecord = new SupportCommentsVO(); 

                return supportcommentrecord;
            }
            set { supportcommentrecord = value; }
        }

        /*__________________________________________________________________________________________*/
        public List<SupportCommentsVO> getCustomerSupportComments()
        {

            var customerComments = from comment in this.SupportComments
                                  orderby comment.UpDatedBy descending 
                                select comment  ;
            return customerComments.ToList();
        }
    }
}
