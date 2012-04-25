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
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Common.Controllers.Database.Procedures
{
    public static class CustomerProcedures
    {
        private static BusinessRuleVO _BusinessRule;
        public const string DUPLICATESSNORACLEERRORCODE = "101";

        # region Public Methods

        public static int getPawnBRCustomerLegalAge(DesktopSession desktopSession)
        {
            _BusinessRule = desktopSession.PawnBusinessRuleVO[Commons.PAWNGENERALBR];
            string sComponentValue = "";
            bool retVal = _BusinessRule.getComponentValue(Commons.PAWNLEGALAGECOMPONENT, ref sComponentValue);
            int validAge = 0;
            if (retVal)
            {
                //get the valid age
                try
                {
                    validAge = Convert.ToInt32(sComponentValue);

                }
                catch (Exception)
                {
                    validAge = 0;
                }
            }
            return validAge;

        }

        public static List<string> getPawnRequiredFields(DesktopSession desktopSession)
        {
            List<string> ManagePawnRequiredFields = new List<string>();
            List<string> PawnRuleComponents = Commons.GetPawnAppBusinessRuleComponents();
            bool layaway = false;
            bool retailSaleNoFirearm = false;
            bool firearm = false;
            bool layawayPickupOfFirearm = false;
            _BusinessRule = null;
            desktopSession.UpdateRequiredFieldsForCustomer = false;
            BusinessRulesProcedures bProcedures=new BusinessRulesProcedures(desktopSession);

            if (desktopSession.PawnBusinessRuleVO != null)
            {
                if (desktopSession.ActiveRetail != null)
                {
                    if (desktopSession.ActiveRetail.LoanStatus == ProductStatus.LAY)
                    {
                        layaway = true;
                    }

                    var prod = (from product in desktopSession.ActiveRetail.RetailItems
                                where product.IsGun
                                select product).FirstOrDefault();
                    if (prod != null)
                    {
                        //Retail has a firearm
                        _BusinessRule = desktopSession.PawnBusinessRuleVO[Commons.PAWNRETAILWITHFIREARM];
                        firearm = true;
                    }
                    else
                    {
                        //Check if the retail being processed is a layaway
                        //If it is then it should have the same required fields as a retail firearm sale
                        if (layaway)
                        {
                            _BusinessRule = desktopSession.PawnBusinessRuleVO[Commons.PAWNRETAILWITHFIREARM];

                        }
                        else
                        {
                            retailSaleNoFirearm = true;

                            /*if (!desktopSession.CashSaleCustomer)
                                _BusinessRule = desktopSession.PawnBusinessRuleVO[Common.PAWNRETAILNOFIREARM];*/
                        }
                    }
                }
                else
                {
                    //_BusinessRule = desktopSession.PawnBusinessRuleVO[Common.PAWNGENERALBR];
                    _BusinessRule = bProcedures.GetCustomerBusinessRule(desktopSession.CurrentSiteId);

                    // BZ-1404
                    if (desktopSession.ServiceLayaways != null)
                    {
                        foreach (var layawayVO in desktopSession.ServiceLayaways)
                        {
                            var prod = (from product in layawayVO.RetailItems
                                        where product.IsGun
                                        select product).FirstOrDefault();
                            if (prod != null)
                            {
                                layawayPickupOfFirearm = true;
                                break;
                            }
                        }
                    }
                }

                if (_BusinessRule != null)
                {
                    foreach (string s in PawnRuleComponents)
                    {
                        string sComponentValue = "";

                        bool retVal = _BusinessRule.getComponentValue(s, ref sComponentValue);
                        if (retVal)
                        {
                            //get the component value
                            if (sComponentValue.Equals("Y"))
                            {
                                ManagePawnRequiredFields.Add(s);
                            }
                        }
                    }
                }

                if (retailSaleNoFirearm)
                {
                    desktopSession.UpdateRequiredFieldsForCustomer = true;

                    ManagePawnRequiredFields.Add("PWNAPP_FIRSTNAME");
                    ManagePawnRequiredFields.Add("PWNAPP_LASTNAME");
                    ManagePawnRequiredFields.Add("PWNAPP_IDENTIFICATIONTYPE");
                    ManagePawnRequiredFields.Add("PWNAPP_IDENTIFICATIONSTATE");
                    ManagePawnRequiredFields.Add("PWNAPP_IDENTIFICATIONCOUNTRY");
                    ManagePawnRequiredFields.Add("PWNAPP_IDENTIFICATIONNUMBER");
                    ManagePawnRequiredFields.Add("PWNAPP_PHONENUMBER");
                    ManagePawnRequiredFields.Add("PWNAPP_DATEOFBIRTH");
                }

                if (layaway)
                {
                    ManagePawnRequiredFields.Add("PWNAPP_PHONENUMBER");
                    if (!firearm)
                    {
                        ManagePawnRequiredFields.Remove("PWNAPP_SEX");
                        ManagePawnRequiredFields.Remove("PWNAPP_RACE");
                    }
                }

                if (layawayPickupOfFirearm)
                {
                    // Layaway Pickup of a firearm
                    ManagePawnRequiredFields.Remove("PWNAPP_HEIGHT");
                    ManagePawnRequiredFields.Remove("PWNAPP_HAIR");
                    ManagePawnRequiredFields.Remove("PWNAPP_EYES");   
                }
            }

            return ManagePawnRequiredFields;
        }

        public static string getHandGunValidAge(DesktopSession desktopSession)
        {
            // Call PWN_BR-59 to retrieve Minimum Age
            BusinessRuleVO brMINIMUM_AGE = desktopSession.PawnBusinessRuleVO[Commons.PAWNGUNMIMIMUMAGEBR];

            string sComponentValue = "";

            string handGunMinAge = "";


            bool bMinAgeFound = brMINIMUM_AGE.getComponentValue("CL_PWN_0174_MINAGEHGUNLN", ref sComponentValue);
            if (bMinAgeFound)
                handGunMinAge = sComponentValue.ToString();

            return handGunMinAge;

        }

        public static string getLongGunValidAge(DesktopSession desktopSession)
        {
            // Call PWN_BR-59 to retrieve Minimum Age
            BusinessRuleVO brMINIMUM_AGE = desktopSession.PawnBusinessRuleVO[Commons.PAWNGUNMIMIMUMAGEBR];

            string sComponentValue = "";
            string longGunMinAge = "";

            bool bMinAgeFound = brMINIMUM_AGE.getComponentValue("CL_PWN_0175_MINAGELGUNLN", ref sComponentValue);
            if (bMinAgeFound)
                longGunMinAge = sComponentValue.ToString();

            return longGunMinAge;
        }

        public static bool isValidAgeForGuns(DesktopSession desktopSession, Item pawnItem, CustomerVO custToCheck)
        {
            string handGunMinAge = getHandGunValidAge(desktopSession);
            string longGunMinAge = getLongGunValidAge(desktopSession);
            if (pawnItem.IsHandGun()) //merchandise is hand gun
            {
                if (custToCheck.Age < Convert.ToInt16(handGunMinAge))
                {
                    return false;
                }

            }
            else if (pawnItem.IsLongGun()) //merchandise is long gun
            {
                if (custToCheck.Age < Convert.ToInt16(longGunMinAge))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsBackgroundCheckRequired(DesktopSession desktopSession)
        {
            BusinessRuleVO brBACKGROUNDCHECK = desktopSession.PawnBusinessRuleVO[Commons.PAWNBACKGROUNDCHECKBR];

            string sComponentValue = "";

            bool ruleFound = brBACKGROUNDCHECK.getComponentValue("CL_PWN_0181_USECONCEALEDWEAPONSPERMIT", ref sComponentValue);
            if (ruleFound)
                if (sComponentValue == "N")
                    return true;
            return false;

        }

        public static CustomerVO getCustomerDataByCustomerNumber(DesktopSession desktopSession, string CustNumber)
        {
            DataTable custDatatable = new DataTable();
            DataTable custPhoneDatatable = new DataTable();
            DataTable custIdentDatatable = new DataTable();
            DataTable custAddrDatatable = new DataTable();
            DataTable custEmailDatatable = new DataTable();
            DataTable custNotesDatatable = new DataTable();
            DataTable custStoreCreditDatatable = new DataTable();
            bool retValue = false;
            string errorCode = "";
            string errorMsg = "";
            string selectedPartyId = "";
            CustomerVO customerObject = new CustomerVO();
            //Call get_cust_details to get this customer information
            retValue = CustomerDBProcedures.CreateInstance(desktopSession).ExecuteLookupCustomer("", "", "", "", CustNumber, "", "", "", "", "", "", desktopSession.CurrentSiteId.StoreNumber, desktopSession.CurrentSiteId.State, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            if (retValue && string.IsNullOrEmpty(errorCode))
            {
                customerObject.NewCustomer = false;
                //Get the row data from the custdatatable which pertains to customer
                DataRow[] customerRows = custDatatable.Select("customer_number='" + CustNumber + "'");
                if (customerRows.Length > 0)
                {
                    selectedPartyId = customerRows[0][(int)customerrecord.PARTY_ID].ToString();
                }
                //Get all the identity data for the selected customer
                DataRow[] custIdentRows;
                if (custIdentDatatable != null)
                {
                    custIdentRows = custIdentDatatable.Select("party_id='" + selectedPartyId + "'", "datedidenttypedesc asc");
                }
                else
                    custIdentRows = null;
                //Get all the phone data for the selected customer
                DataRow[] custPhoneRows;
                if (custPhoneDatatable != null)
                {
                    custPhoneRows = custPhoneDatatable.Select("party_id='" + selectedPartyId + "'");
                }
                else
                    custPhoneRows = null;

                //Get all the Address data for the selected customer
                DataRow[] custAddrRows;
                if (custAddrDatatable != null)
                {
                    custAddrRows = custAddrDatatable.Select("party_id='" + selectedPartyId + "'");
                }
                else
                    custAddrRows = null;
                //Get all the email rows for the selected customer
                DataRow[] custEmailRows;
                if (custEmailDatatable != null)
                    custEmailRows = custEmailDatatable.Select("party_id='" + selectedPartyId + "'");
                else
                    custEmailRows = null;
                //Get all the notes rows for the selected customer
                DataRow[] custNotesRows;
                if (custNotesDatatable != null)
                    custNotesRows = custNotesDatatable.Select("party_id='" + selectedPartyId + "'");
                else
                    custNotesRows = null;
                //Assign the values of the columns in the selected row
                //to the customer object properties
                DataRow[] custStoreCreditRows;
                if (custStoreCreditDatatable != null)
                    custStoreCreditRows = custStoreCreditDatatable.Select("party_id='" + selectedPartyId + "'");
                else
                    custStoreCreditRows = null;

                setCustomerData(customerRows[0], customerObject);
                setCustomerAddressData(custAddrRows, customerObject);
                setCustomerIDData(custIdentRows, customerObject);
                setCustomerPhoneData(custPhoneRows, customerObject);
                setCustomerEmailData(custEmailRows, customerObject);
                setCustomerNotesData(custNotesRows, customerObject);
                setCustomerStoreCreditData(custStoreCreditRows, customerObject);

            }
            return customerObject;

        }

        public static bool GetCustomerLoanData(DesktopSession desktopSession, string customerNumber)
        {
            bool retValue = false;
            DataSet custLoanData;
            string errorCode;
            string errorText;
            List<PawnLoan> pawnLoans;
            List<PawnAppVO> pawnApplications;
            //Set end date to be shopdate
            string toDate = ShopDateTime.Instance.ShopDate.ToShortDateString();

            retValue = CustomerLoans.GetCustomerLoans(customerNumber, ProductStatus.ALL, StateStatus.BLNK, null, toDate, out custLoanData, out errorCode, out errorText);
            if (retValue)
            {
                CustomerLoans.ParseDataSet(custLoanData, out pawnLoans, out pawnApplications);
                if (pawnLoans != null)
                    desktopSession.CustomerHistoryLoans = pawnLoans;
            }

            return retValue;
        }

        public static bool checkCustomerExistsBySSN(DesktopSession desktopSession, string ssn)
        {
            DataTable custDatatable = new DataTable();
            DataTable custPhoneDatatable = new DataTable();
            DataTable custIdentDatatable = new DataTable();
            DataTable custAddrDatatable = new DataTable();
            DataTable custEmailDatatable = new DataTable();
            DataTable custNotesDatatable = new DataTable();
            DataTable custStoreCreditDataTable = new DataTable();
            bool retValue = false;
            string errorCode = "";
            string errorMsg = "";
            CustomerVO customerObject = new CustomerVO();
            //Call get_cust_details to get this customer information
            retValue = CustomerDBProcedures.CreateInstance(desktopSession).ExecuteLookupCustomer("", "", "", ssn, "", "", "", "", "", "", "", "", desktopSession.CurrentSiteId.State, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDataTable, out errorCode, out errorMsg);
            if (retValue)
            {
                if (custDatatable != null && custDatatable.Rows.Count > 0)
                    return true;

            }

            return false;


        }

        private static void setCustomerNotesData(DataRow[] custNotesRows, CustomerVO customerObject)
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

        private static void setCustomerEmailData(DataRow[] custEmailRows, CustomerVO customerObject)
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

        private static void setCustomerPhoneData(DataRow[] custPhoneRows, CustomerVO customerObject)
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

        private static void setCustomerIDData(DataRow[] custIdentRows, CustomerVO customerObject)
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

        private static void setCustomerAddressData(DataRow[] custAddrRows, CustomerVO customerObject)
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

        private static void setCustomerStoreCreditData(DataRow[] custSoreCreditRows, CustomerVO customerObject)
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

        public static CustomerVO getCustomerDataInObject(string selectedPartyId,
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
            CustomerVO customerObject = new CustomerVO();
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

        private static void setCustomerData(DataRow customerRow, CustomerVO customerObject)
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

        }

        #endregion

        public static bool AddCustomer(DesktopSession desktopSession,CustomerVO customer, string userId, string storeNumber, out string custNumber, out string partyId, out string errorDesc)
        {
            string errorCode;
            string errorMsg;

            string[] strPrimaryPhone;
            string[] strPhoneNumber;
            string[] strAreaCode;
            string[] strPhoneExtension;
            string[] strCountryCode;
            string[] strContactType;
            string[] strTelecomNumTypeCode;
            string[] strIdType;
            string[] strIdNumber;
            string[] strIdExpiryDate;
            string[] strIdIssuerCode;
            string[] strIdIssueDate;

            //Get the ID data
            int numberofIdentifications = customer.NumberIdentities;
            int numberofContacts = customer.NumberContacts;
            if (numberofContacts > 0)
            {
                strPrimaryPhone = new string[numberofContacts];
                strPhoneNumber = new string[numberofContacts];
                strAreaCode = new string[numberofContacts];
                strPhoneExtension = new string[numberofContacts];
                strCountryCode = new string[numberofContacts];
                strContactType = new string[numberofContacts];
                strTelecomNumTypeCode = new string[numberofContacts];
                int i = 0;
                foreach (ContactVO custContact in customer.CustomerContacts)
                {
                    strPhoneNumber[i] = custContact.ContactPhoneNumber;
                    strPhoneExtension[i] = custContact.ContactExtension;
                    strAreaCode[i] = custContact.ContactAreaCode;
                    strContactType[i] = custContact.ContactType;
                    strTelecomNumTypeCode[i] = custContact.TelecomNumType;
                    strPrimaryPhone[i] = custContact.TeleusrDefText;
                    strCountryCode[i] = custContact.CountryDialNumCode;
                    i++;
                }
            }
            else
            {
                strPrimaryPhone = new string[1];
                strPhoneNumber = new string[1];
                strAreaCode = new string[1];
                strPhoneExtension = new string[1];
                strCountryCode = new string[1];
                strContactType = new string[1];
                strTelecomNumTypeCode = new string[1];
                strPrimaryPhone[0] = "";
                strPhoneExtension[0] = "";
                strPhoneNumber[0] = "";
                strAreaCode[0] = "";
                strCountryCode[0] = "";
                strContactType[0] = "";
                strTelecomNumTypeCode[0] = "";
            }

            if (numberofIdentifications > 0)
            {
                strIdType = new string[numberofIdentifications];
                strIdNumber = new string[numberofIdentifications];
                strIdExpiryDate = new string[numberofIdentifications];
                strIdIssuerCode = new string[numberofIdentifications];
                strIdIssueDate = new string[numberofIdentifications];

                int i = 0;
                foreach (IdentificationVO custId in customer.getAllIdentifications())
                {
                    strIdType[i] = custId.IdType;
                    strIdIssuerCode[i] = custId.IdIssuerCode;
                    strIdNumber[i] = custId.IdValue;
                    strIdExpiryDate[i] = custId.IdExpiryData.FormatDate();
                    strIdIssueDate[i] = custId.IdIssueDate.FormatDate();
                    i++;
                }
            }
            else
            {
                strIdType = new string[1];
                strIdNumber = new string[1];
                strIdExpiryDate = new string[1];
                strIdIssuerCode = new string[1];
                strIdIssueDate = new string[1];
                strIdType[0] = "";
                strIdIssuerCode[0] = "";
                strIdNumber[0] = "";
                strIdExpiryDate[0] = "";
                strIdIssueDate[0] = "";


            }

            //Get the comments data from the customer object
            string strComments = "";
            if (customer.getLatestNote().ContactNote != null)
                strComments = customer.getLatestNote().ContactNote;
            //Get the primary email data from the customer object
            string strPrimaryEmail = "";

            if (customer.getPrimaryEmail().EmailAddress != null)
                strPrimaryEmail = customer.getPrimaryEmail().EmailAddress;

            bool retValue = CustomerDBProcedures.CreateInstance(desktopSession).InsertCustomer(CustomerTypes.PERSONTYPE, customer.FirstName,
                 customer.MiddleInitial, customer.LastName, customer.DateOfBirth.FormatDate(), userId, "", "",
                 "", "", "", strContactType, strPhoneNumber, strAreaCode, strCountryCode, strPhoneExtension,
                 strTelecomNumTypeCode, strPrimaryPhone,
                 "", "", "", "", "", strIdNumber, strIdIssueDate, strIdExpiryDate, strIdIssuerCode, strIdType,
                 customer.SocialSecurityNumber, customer.CustTitle, "", customer.CustTitleSuffix, strPrimaryEmail,
                 strComments, customer.HearAboutUs, customer.ReceivePromotionOffers,
                 storeNumber, out partyId, out custNumber, out errorCode, out errorMsg);

            if (!retValue)
            {
                errorDesc = errorCode.Equals(DUPLICATESSNORACLEERRORCODE) ? DUPLICATESSNORACLEERRORCODE : "";
            }
            else
                errorDesc = "";

            return retValue;
        }

        public static bool AddCustomerAddress(DesktopSession desktopSession, CustomerVO customer, string userId,
            string storeNumber)
        {
            string errorCode;
            string errorMsg;

            //Get the address from the customer object and store in array
            //Array of address data
            string[] strAddr;
            string[] strCity;
            string[] strState;
            string[] strZip;
            string[] strUnit;
            string[] strCountry;
            string[] strAddrNotes;
            string[] strAddlString;
            string[] strAddrType;
            bool physicalAddrEntered = false;
            bool mailAddrEntered = false;
            bool workAddrEntered = false;
            bool addlAddrEntered = false;
            int numberOfAddresses = 0;
            AddressVO physicalAddr = customer.getHomeAddress();
            if (physicalAddr != null)
            {
                numberOfAddresses++;
                physicalAddrEntered = true;
            }
            AddressVO mailingAddr = customer.getMailingAddress();
            if (mailingAddr != null)
            {
                numberOfAddresses++;
                mailAddrEntered = true;
            }
            AddressVO workAddr = customer.getWorkAddress();
            if (workAddr != null)
            {
                numberOfAddresses++;
                workAddrEntered = true;
            }
            AddressVO addlAddr = customer.getAdditionalAddress();
            if (addlAddr != null)
            {
                numberOfAddresses++;
                addlAddrEntered = true;
            }

            strAddr = new string[numberOfAddresses];
            strCity = new string[numberOfAddresses];
            strState = new string[numberOfAddresses];
            strCountry = new string[numberOfAddresses];
            strAddrType = new string[numberOfAddresses];
            strAddrNotes = new string[numberOfAddresses];
            strAddlString = new string[numberOfAddresses];
            strZip = new string[numberOfAddresses];
            strUnit = new string[numberOfAddresses];
            int i = 0;
            if (physicalAddrEntered)
            {
                strAddr[i] = physicalAddr.Address1;
                strCity[i] = physicalAddr.City;
                strState[i] = physicalAddr.State_Code;
                strCountry[i] = physicalAddr.Country_Code;
                strAddrType[i] = CustomerAddressTypes.HOME_ADDRESS;
                strAddrNotes[i] = physicalAddr.Notes;
                strAddlString[i] = "";
                strZip[i] = physicalAddr.ZipCode;
                strUnit[i] = physicalAddr.UnitNum;
                i++;
            }
            if (mailAddrEntered)
            {
                strAddr[i] = mailingAddr.Address1;
                strCity[i] = mailingAddr.City;
                strState[i] = mailingAddr.State_Code;
                strCountry[i] = mailingAddr.Country_Code;
                strAddrType[i] = CustomerAddressTypes.MAILING_ADDRESS;
                strAddrNotes[i] = mailingAddr.Notes;
                strAddlString[i] = "";
                strZip[i] = mailingAddr.ZipCode;
                strUnit[i] = mailingAddr.UnitNum;
                i++;
            }
            if (workAddrEntered)
            {
                strAddr[i] = workAddr.Address1;
                strCity[i] = workAddr.City;
                strState[i] = workAddr.State_Code;
                strCountry[i] = workAddr.Country_Code;
                strAddrType[i] = CustomerAddressTypes.WORK_ADDRESS;
                strAddrNotes[i] = workAddr.Notes;
                strAddlString[i] = "";
                strZip[i] = workAddr.ZipCode;
                strUnit[i] = workAddr.UnitNum;
                i++;
            }
            if (addlAddrEntered)
            {
                strAddr[i] = addlAddr.Address1;
                strCity[i] = addlAddr.City;
                strState[i] = addlAddr.State_Code;
                strCountry[i] = addlAddr.Country_Code;
                strAddrType[i] = CustomerAddressTypes.ADDITIONAL_ADDRESS;
                strAddrNotes[i] = addlAddr.Notes;
                strAddlString[i] = addlAddr.AlternateString;
                strZip[i] = addlAddr.ZipCode;
                strUnit[i] = addlAddr.UnitNum;
            }

            bool retVal = false;
            if (numberOfAddresses > 0)
            {
                retVal = CustomerDBProcedures.CreateInstance(desktopSession).UpdateAddress(strAddrType, strAddr,
                   strUnit, strCity, strZip, strState, strCountry,
                   strAddrNotes, strAddlString, customer.CustomerNumber, customer.PartyId,
                   userId, out errorCode, out errorMsg);
            }
            else
                retVal = true;


            return retVal;
        }

        public static void setCustomerDataInObject(DesktopSession desktopSession, CustomerVO Customer, string strFirstName, string strMiddleName,
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

        public static void setCustomerAddressDataInObject(CustomerVO Customer, string[] strAddr, string[] strAddr2, string[] strCity,
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

        public static void setCustomerIDDataInObject(CustomerVO Customer, string[] strIdNumber,
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

        public static void setCustomerPhoneDataInObject(CustomerVO Customer, string[] strContactType, string[] strPhoneNumber,
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

        public static void setCustomerEmailDataInObject(CustomerVO customer, string[] emailAddress, string[] emailType)
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
