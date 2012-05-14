/**************************************************************************************************************
* CashlinxDesktop.DesktopProcedures
* CustomerProcedures 
* Acts as the Middle layer functions called by customer related use case forms
* to talk to the database layer and to set the customer object with the data
* entered in the forms
* EDW - 1/12/12 - BZ#1404 - Layaway w/ Firearm, Gender+Race are now mandatory for all states.
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Common.Libraries.Objects.Business;
//using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
//using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Shared;
using Support.Libraries.Objects.Customer;
using Support.Libraries.Utility;
using customerrecord = Support.Libraries.Utility.customerrecord;

namespace Support.Controllers.Database.Procedures
{
    public static class CustomerProcedures
    {
        private static BusinessRuleVO _BusinessRule;
        public const string DUPLICATESSNORACLEERRORCODE = "101";

        # region Public Methods

 

        private static void setCustomerNotesData(DataRow[] custNotesRows, CustomerVOForSupportApp customerObject)
        {
            //Set the Notes data for the customer

            if (custNotesRows != null && custNotesRows.Length > 0)
            {
                foreach (DataRow note in custNotesRows)
                {
                    DateTime commentDate;

                    commentDate = Utilities.GetDateTimeValue(note.ItemArray[(int)customernotesrecord.CREATIONDATE], DateTime.MaxValue);

                    DateTime contactDate;

                    contactDate = Utilities.GetDateTimeValue(note.ItemArray[(int)customernotesrecord.CONTACTDATE], DateTime.MaxValue);

                    DateTime commentUpdateDate;

                    commentUpdateDate = Utilities.GetDateTimeValue(note.ItemArray[(int)customernotesrecord.UPDATEDATE], DateTime.MaxValue);


                    string storeNumber = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.STORENUMBER], "");
                    string contactResult = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.CONTACTRESULT], "");
                    string contactStatus = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.CONTACTSTATUS], "");
                    string comments = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.CONTACTNOTE], "");
                    string commentsby = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.CREATEDBY], "");
                    string custProdNoteId = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.CUSTOMERPRODUCTNOTEID], "");
                    string commentCode = Utilities.GetStringValue(note.ItemArray[(int)customernotesrecord.CUSTOMERNOTESCODE], "");
                    CustomerNotesVO custnote = new CustomerNotesVO(contactResult, contactStatus, comments, contactDate, commentDate, commentUpdateDate, commentsby, commentCode, custProdNoteId, storeNumber);
                    customerObject.addNotes(custnote);
                }
            }


        }

        private static void setCustomerEmailData(DataRow[] custEmailRows, CustomerVOForSupportApp customerObject)
        {
            //Set the Email data for the customer

            if (custEmailRows != null && custEmailRows.Length > 0)
            {
                foreach (DataRow email in custEmailRows)
                {
                    string emailAddr = Utilities.GetStringValue(email.ItemArray[(int)customeremailrecord.EMAILADDRESS], "");
                    string emailAddrType = Utilities.GetStringValue(email.ItemArray[(int)customeremailrecord.EMAILADDRESSTYPECODE], "");
                    string contactinfoid = Utilities.GetStringValue(email.ItemArray[(int)customeremailrecord.CONTACTINFOID], "");
                    CustomerEmailVO custemail = new CustomerEmailVO(emailAddr, emailAddrType, contactinfoid);
                    customerObject.addEmail(custemail);
                }
            }


        }

        private static void setCustomerPhoneData(DataRow[] custPhoneRows, CustomerVOForSupportApp customerObject)
        {
            //set the phone data for the customer

            if (custPhoneRows != null && custPhoneRows.Length > 0)
            {
                foreach (DataRow contact in custPhoneRows)
                {
                    ContactVO custcontact = new ContactVO
                                                {
                                                    TelecomNumType =
                                                        Utilities.GetStringValue(
                                                        contact.ItemArray[(int)customerphonerecord.TELECOMNUMTYPECODE],
                                                        ""),
                                                    ContactAreaCode =
                                                        Utilities.GetStringValue(
                                                        contact.ItemArray[(int)customerphonerecord.AREADIALNUMCODE], ""),
                                                    ContactPhoneNumber =
                                                        Utilities.GetStringValue(
                                                        contact.ItemArray[(int)customerphonerecord.TELECOMNUMBER], ""),
                                                    ContactExtension =
                                                        Utilities.GetStringValue(
                                                        contact.ItemArray[(int)customerphonerecord.EXTENSIONNUMBER], ""),
                                                    ContactType =
                                                        Utilities.GetStringValue(
                                                        contact.ItemArray[(int)customerphonerecord.CONTACTTYPECODE], ""),
                                                    TeleusrDefText =
                                                        Utilities.GetStringValue(
                                                        contact.ItemArray[(int)customerphonerecord.TELEUSRDEFTEXT], ""),
                                                    CountryDialNumCode =
                                                        Utilities.GetStringValue(
                                                        contact.ItemArray[(int)customerphonerecord.COUNTRYDIALNUMCODE],
                                                        "")
                                                };
                    customerObject.addContact(custcontact);

                }
            }


        }

        private static void setCustomerIDData(DataRow[] custIdentRows, CustomerVOForSupportApp customerObject)
        {
            //Set the Identity data in the customer object
            if (custIdentRows != null && custIdentRows.Length > 0)
            {
                foreach (DataRow cust in custIdentRows)
                {
                    IdentificationVO custid = new IdentificationVO
                                                  {
                                                      IdType =
                                                          Utilities.GetStringValue(
                                                          cust.ItemArray[(int)customeridrecord.IDENTTYPECODE], ""),
                                                      IdValue =
                                                          Utilities.GetStringValue(
                                                          cust.ItemArray[(int)customeridrecord.ISSUEDNUMBER], ""),
                                                      IdIssuer =
                                                          Utilities.GetStringValue(
                                                          cust.ItemArray[(int)customeridrecord.STATE_NAME], ""),
                                                      IdIssuerCode =
                                                          Utilities.GetStringValue(
                                                          cust.ItemArray[(int)customeridrecord.ISSUERNAME], ""),
                                                      DatedIdentDesc =
                                                          Utilities.GetStringValue(
                                                          cust.ItemArray[(int)customeridrecord.DATEDIDENTTYPEDESC], ""),
                                                      IdentId =
                                                          Utilities.GetStringValue(
                                                          cust.ItemArray[(int)customeridrecord.IDENTID], ""),
                                                      CreationDate =
                                                          Utilities.GetDateTimeValue(
                                                          cust.ItemArray[(int)customeridrecord.IDCREATIONDATE],
                                                          DateTime.MaxValue),
                                                      IdExpiryData =
                                                      Utilities.GetDateTimeValue(
                                                          cust.ItemArray[(int)customeridrecord.EXPIRYDATE],
                                                          DateTime.MaxValue),

                                                      //Madhu 12/07/2010 fix for bugzilla defect 10
                                                      //IsLatest = false
                                                      IsLatest = Utilities.GetCharValue(cust.ItemArray[(int)customeridrecord.ISLATEST]) == 'Y' ? true : false
                                                  };
                    customerObject.addIdentity(custid);
                }
            }


        }

        private static void setCustomerAddressData(DataRow[] custAddrRows, CustomerVOForSupportApp customerObject)
        {
            //Set the address data in the customer object

            if (custAddrRows != null && custAddrRows.Length > 0)
            {
                foreach (DataRow addr in custAddrRows)
                {
                    AddressVO custAddr = new AddressVO();
                    custAddr.Address1 = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.ADDRESS1_TEXT], "");
                    custAddr.Address2 = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.ADDRESS2_TEXT], "");
                    custAddr.City = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.CITY_NAME], "");
                    custAddr.ContactTypeCode = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.CONTACTTYPECODE], "");
                    custAddr.ContMethodTypeCode = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.CONTMTHDTYPECODE], "");
                    custAddr.Country_Code = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.COUNTRY_CODE], "");
                    custAddr.Country_Name = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.COUNTRY_NAME], "");
                    custAddr.Notes = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.NOTES2], "");
                    custAddr.AlternateString = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.NOTES1], "");
                    custAddr.State_Code = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.STATE_CODE], "");
                    custAddr.State_Name = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.STATE_NAME], "");
                    custAddr.UnitNum = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.UNIT_NUM], "");
                    custAddr.ZipCode = Utilities.GetStringValue(addr.ItemArray[(int)customeraddrrecord.POSTAL_CODE], "");
                    customerObject.addAddress(custAddr);
                }
            }


        }

        private static void setCustomerStoreCreditData(DataRow[] custSoreCreditRows, CustomerVOForSupportApp customerObject)
        {
            //Set the Store Credit data for the customer

            if (custSoreCreditRows != null && custSoreCreditRows.Length > 0)
            {
                foreach (DataRow storeCredit in custSoreCreditRows)
                {
                    DateTime dateMade = Utilities.GetDateTimeValue(storeCredit.ItemArray[(int)customerstorecreditrecord.DATE_MADE], DateTime.MaxValue);
                    string customerNumber = Utilities.GetStringValue(storeCredit.ItemArray[(int)customerstorecreditrecord.CUSTOMERNUMBER], "");
                    string storeNumber = Utilities.GetStringValue(storeCredit.ItemArray[(int)customerstorecreditrecord.STORENUMBER], "");
                    decimal creditBalance = Utilities.GetDecimalValue(storeCredit.ItemArray[(int)customerstorecreditrecord.BALANCE], 0);
                    StoreCreditVO custCredit = new StoreCreditVO();
                    custCredit.Amount = creditBalance;
                    custCredit.StoreNumber = storeNumber;
                    custCredit.CustomerNumber = customerNumber;
                    custCredit.DateMade = dateMade;
                    customerObject.addStoreCredit(custCredit);
                }
            }


        }

        public static void getCustomerCommentsDataInObject(DataTable comments, CustomerVOForSupportApp CustomerContainer)
        {
            

            if(comments != null)
            {
                DataRow[] dataRows = comments.Select();
                if (dataRows != null && dataRows.Length > 0)
                {
                    foreach (DataRow cust in dataRows)
                    {
                        var commentRecord = new SupportCommentsVO();
                        commentRecord.CommentNote = Utilities.GetStringValue(cust.ItemArray[(int)customercommentrecord.COMMENTS], "");
                        commentRecord.LastUpDateDATE = DateTime.Parse(Utilities.GetStringValue(cust.ItemArray[(int)customercommentrecord.DATA_MADE], ""));
                        commentRecord.UpDatedBy = Utilities.GetStringValue(cust.ItemArray[(int)customercommentrecord.MADE_BY], "");
                        commentRecord.Category = Utilities.GetStringValue(cust.ItemArray[(int)customercommentrecord.CATEGORY], "");
                        commentRecord.EmployeeNumber = Utilities.GetStringValue(cust.ItemArray[(int)customercommentrecord.EMPLOYEE_NBR], "");
                        CustomerContainer.addSupportComment(commentRecord);
                    }
                }

            }
        }

        public static CustomerVOForSupportApp getCustomerDataInObject(string selectedPartyId,
            DataTable customerIdentities,
            DataTable customerPhoneNumbers,
            DataTable customerAddresses,
            DataTable customerEmails,
            DataTable customerNotes,
            DataTable customerStoreCredit,
            DataRow selectedCustomerRow)
        {
            //Get all the row data from the custdatatable which pertains to the same customer
            //DataRow[] customerRows = customers.Select("party_id='" + selectedPartyId + "'");
            //Get all the identity data for the selected customer
            DataRow[] custIdentRows = customerIdentities != null ? customerIdentities.Select("party_id='" + selectedPartyId + "'", "datedidenttypedesc asc") : null;
            //Get all the phone data for the selected customer
            DataRow[] custPhoneRows = customerPhoneNumbers != null ? customerPhoneNumbers.Select("party_id='" + selectedPartyId + "'") : null;
            //Get all the Address data for the selected customer
            DataRow[] custAddrRows = customerAddresses != null ? customerAddresses.Select("party_id='" + selectedPartyId + "'") : null;
            //Get the Email data for the selected customer
            DataRow[] custEmailRows = customerEmails != null ? customerEmails.Select("party_id='" + selectedPartyId + "'") : null;
            //Get the Notes data for the selected customer
            DataRow[] custNotesRows = customerNotes != null ? customerNotes.Select("party_id='" + selectedPartyId + "'") : null;
            //Get the store credit data for the selected customer
            DataRow[] custStoreCreditRows = customerStoreCredit != null ? customerStoreCredit.Select("party_id='" + selectedPartyId + "'") : null;
            //Assign the values of the columns in the selected row
            //to the customer object properties
            CustomerVOForSupportApp customerObject = new CustomerVOForSupportApp();
            customerObject.NewCustomer = false;
            setCustomerData(selectedCustomerRow, customerObject);
            setCustomerAddressData(custAddrRows, customerObject);
            setCustomerIDData(custIdentRows, customerObject);
            setCustomerPhoneData(custPhoneRows, customerObject);
            setCustomerEmailData(custEmailRows, customerObject);
            setCustomerNotesData(custNotesRows, customerObject);
            setCustomerStoreCreditData(custStoreCreditRows, customerObject);
            return customerObject;

        }

        private static void setCustomerData(DataRow customerRow, CustomerVOForSupportApp customerObject)
        {
            customerObject.CustomerNumber = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.CUSTOMER_NUMBER], "");

            customerObject.CustTitle = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.CUST_TITLE], "");
            customerObject.DateOfBirth = Utilities.GetDateTimeValue(customerRow.ItemArray[(int)customerrecord.DATE_OF_BIRTH], DateTime.MaxValue);
            customerObject.Age = Utilities.GetIntegerValue(Commons.getAge(customerObject.DateOfBirth.FormatDate(), ShopDateTime.Instance.ShopDate), 0);
            customerObject.EyeColor = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.EYE_COLOR], "");
            customerObject.FirstName = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.CUST_FIRST_NAME], "");
            customerObject.LastName = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.CUST_LAST_NAME], "");
            customerObject.MiddleInitial = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.CUST_MIDDLE_NAME], "");
            customerObject.Gender = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.GENDER], "");
            customerObject.HairColor = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.HAIR_COLOR], "");
            string customerheight = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.HEIGHT], "");
            customerObject.Height = customerheight.Trim().Length != 0 ? customerheight : "";

            customerObject.PartyId = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.PARTY_ID], "");
            customerObject.Race = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.RACE], "");
            customerObject.SocialSecurityNumber = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.SOCIAL_SECURITY_NUMBER], "");
            customerObject.Weight = Utilities.GetIntegerValue(customerRow.ItemArray[(int)customerrecord.WEIGHT], 0);
            customerObject.CustTitleSuffix = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.NAME_SUFFIX], "");
            customerObject.CompanyName = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.EMPNAME], "");

            customerObject.CustomerSince = Utilities.GetDateTimeValue(customerRow.ItemArray[(int)customerrecord.CREATIONDATE], DateTime.MaxValue);

            customerObject.HearAboutUs = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.HOWDIDYOUHEAR], "");
            customerObject.NegotiationLanguage = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.PREFERLANGCODE], "");
            customerObject.NoCallFlag = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.NOCALLFLAG], "");
            customerObject.NoEmailFlag = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.NOEMAILFLAG], "");
            customerObject.NoFaxFlag = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.NOFAXFLAG], "");
            customerObject.NoMailFlag = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.NOMAILFLAG], "");
            customerObject.Occupation = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.EMPTITLE], "");
            customerObject.PreferredCallTime = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.PREFERCONTTIME], "");
            customerObject.PreferredContactMethod = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.PREFERCONTMTHDTEXT], "");
            customerObject.ReceivePromotionOffers = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.RECEIVEPROMOTIONS], "");
            customerObject.ReminderContact = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.REMINDERCONTACT], "");

            //Support Application Feilds
            customerObject.MaritalStatus = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.MARITAL_STATUS], "");
            customerObject.SpouseFirstName = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.SPOUSE_FIRST_NAME], "");
            customerObject.SpouseLastName = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.SPOUSE_LAST_NAME], "");
            customerObject.SpouseSsn = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.SPOUSE_SSN], "");
            customerObject.CustSequenceNumber = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.CUST_SEQUENCE_NUMBER], "");
            customerObject.PrivacyNotificationDate = Utilities.GetDateTimeValue(customerRow.ItemArray[(int)customerrecord.PRIVACY_NOTIFICATION_DATE]);
            customerObject.OptOutFlag = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.OPT_OUT_FLAG], "N");
            customerObject.Status = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.STATUS], "");
            customerObject.ReasonCode = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.REASON_CODE], "");
            customerObject.LastVerificationDate = Utilities.GetDateTimeValue(customerRow.ItemArray[(int)customerrecord.LAST_VERIFICATION_DATE]);
            customerObject.NextVerificationDate = Utilities.GetDateTimeValue(customerRow.ItemArray[(int)customerrecord.NEXT_VERIFICATION_DATE]);
            customerObject.CoolingOffDatePDL = Utilities.GetDateTimeValue(customerRow.ItemArray[(int)customerrecord.COOLING_OFF_DATE_PDL]);
            customerObject.CustomerSincePDL = Utilities.GetDateTimeValue(customerRow.ItemArray[(int)customerrecord.CUSTOMER_SINCE_PDL]);
            customerObject.SpanishForm = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.SPANISH_FORM], "N");
            customerObject.PRBC = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.PRBC], "N");
            customerObject.PlanBankruptcyProtection = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.PLANBANKRUPTCY_PROTECTION], "N");
            customerObject.Years = Utilities.GetIntegerValue(customerRow.ItemArray[(int)customerrecord.YEARS], 0);
            customerObject.Months = Utilities.GetIntegerValue(customerRow.ItemArray[(int)customerrecord.MONTHS], 0);
            customerObject.OwnHome = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.OWN_HOME], "");
            customerObject.MonthlyRent = Utilities.GetIntegerValue(customerRow.ItemArray[(int)customerrecord.MONTHLY_RENT], 0);
            customerObject.MilitaryStationedLocal = Utilities.GetStringValue(customerRow.ItemArray[(int)customerrecord.MILITARY_STATIONED_LOCAL], "N");
        }

        #endregion

 
        public static void setCustomerDataInObject(DesktopSession desktopSession, CustomerVOForSupportApp Customer, string strFirstName, string strMiddleName,
            string strLastName, string strDOB, string strSSN, string strTitle, string strTitleSuffix,
            string strComments, string howDidYouHear, string receiveOffers,
            string strEyeColor, string strGender, string strHairColor, string strHeight, string strRace,
            string custNumber, string strWeight)
        {



            Customer.FirstName = strFirstName;
            Customer.LastName = strLastName;
            Customer.MiddleInitial = strMiddleName;
            Customer.CustTitle = strTitle;
            Customer.CustTitleSuffix = strTitleSuffix;
            Customer.SocialSecurityNumber = strSSN;
            if (strDOB.Trim().Length == 10)
                Customer.DateOfBirth = DateTime.Parse(strDOB);
            //Customer.Age = customerAge;
            Customer.EyeColor = strEyeColor;
            Customer.Gender = strGender;
            Customer.HairColor = strHairColor;
            Customer.Height = strHeight;
            Customer.Race = strRace;
            Customer.CustomerNumber = custNumber;
            if (strWeight != string.Empty)
                try
                {
                    Customer.Weight = Convert.ToInt32(strWeight);
                }
                catch (Exception)
                {

                    Customer.Weight = 0;
                }
            Customer.HearAboutUs = howDidYouHear;
            Customer.ReceivePromotionOffers = receiveOffers;
            Customer.Gender = strGender;
            Customer.Race = strRace;
            Customer.HairColor = strHairColor;
            Customer.EyeColor = strEyeColor;
            Customer.CustomerNumber = custNumber;
            if (strComments != string.Empty)
            {
                CustomerNotesVO custComment = new CustomerNotesVO("", "", strComments,
                    ShopDateTime.Instance.ShopDate,
                    ShopDateTime.Instance.ShopDate, ShopDateTime.Instance.ShopDate,
                    "", CustomerNoteTypes.CUSTCOMMENTS, "", desktopSession.CurrentSiteId.StoreNumber);

                Customer.addNotes(custComment);
            }


        }

        public static void setCustomerAddressDataInObject(CustomerVOForSupportApp Customer, string[] strAddr, string[] strAddr2, string[] strCity,
                    string[] strCustAddrState, string[] strZipCode, string[] strCustUnit, string[] strCountry,
                    string[] addrnotes, string[] addrAltString, string[] addrType)
        {

            //Add the address if passed. If Addr1 is empty the address is not passed in
            if (strAddr != null)
            {
                for (int i = 0; i < strAddr.Length; i++)
                {
                    AddressVO newCustAddr = new AddressVO();
                    newCustAddr.Address1 = strAddr[i];
                    if (strAddr2 != null)
                        newCustAddr.Address2 = strAddr2[i];
                    newCustAddr.City = strCity[i];
                    newCustAddr.State_Code = strCustAddrState[i];
                    newCustAddr.ZipCode = strZipCode[i];
                    newCustAddr.UnitNum = strCustUnit[i];
                    newCustAddr.ContactTypeCode = addrType[i];
                    newCustAddr.ContMethodTypeCode = "POSTALADDR";
                    newCustAddr.Country_Code = strCountry[i];
                    newCustAddr.AlternateString = addrAltString[i];
                    newCustAddr.Notes = addrnotes[i];

                    if (strAddr2 != null && strAddr2[i] != "")
                        newCustAddr.CustAddress = strAddr[i] + "," + strAddr2[i] + "," + strCity[i] + "," + strCustAddrState[i] + "," + strZipCode[i];
                    else
                        newCustAddr.CustAddress = strAddr[i] + "," + strCity[i] + "," + strCustAddrState[i] + "," + strZipCode[i];
                    Customer.addAddress(newCustAddr);

                }
            }
        }

        public static void setCustomerIDDataInObject(CustomerVOForSupportApp Customer, string[] strIdNumber,
                string[] strIdIssueDate, string[] strIdExpiryDate, string[] strIdIssuerCode, string[] strIdIssuer,
                string[] strIdType)
        {
            //Add identities
            if (strIdType != null)
            {
                for (int i = 0; i < strIdType.Length; i++)
                {
                    IdentificationVO newCustId = new IdentificationVO();
                    newCustId.IdType = strIdType[i];
                    newCustId.IdValue = strIdNumber[i];
                    newCustId.IdIssuer = strIdIssuer[i];
                    newCustId.IdIssuerCode = strIdIssuerCode[i];
                    if (strIdExpiryDate[i].Trim().Length == 10)
                        newCustId.IdExpiryData = DateTime.Parse(strIdExpiryDate[i]);
                    Customer.addIdentity(newCustId);
                }
            }
        }

        public static void setCustomerPhoneDataInObject(CustomerVOForSupportApp Customer, string[] strContactType, string[] strPhoneNumber,
            string[] strAreaCode, string[] strCountryCode, string[] strPhoneExtension,
            string[] strTelecomNumTypeCode, string[] strPrimaryPhone)
        {

            if (strTelecomNumTypeCode != null)
            {
                for (int i = 0; i < strTelecomNumTypeCode.Length; i++)
                {
                    ContactVO custcontact = new ContactVO();
                    custcontact.TelecomNumType = strTelecomNumTypeCode[i];
                    custcontact.ContactAreaCode = strAreaCode[i];
                    custcontact.ContactPhoneNumber = strPhoneNumber[i];
                    custcontact.ContactExtension = strPhoneExtension[i];
                    custcontact.ContactType = strContactType[i];
                    custcontact.TeleusrDefText = strPrimaryPhone[i];
                    custcontact.CountryDialNumCode = strCountryCode[i];
                    Customer.addContact(custcontact);
                }
            }
        }

        public static void setCustomerEmailDataInObject(CustomerVOForSupportApp customer, string[] emailAddress, string[] emailType)
        {
            if (emailAddress != null)
            {
                for (int i = 0; i < emailAddress.Length; i++)
                {
                    CustomerEmailVO custemail = new CustomerEmailVO
                        (emailAddress[i], emailType[i], "");
                    customer.addEmail(custemail);

                }
            }
        }

    }
}
