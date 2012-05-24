/********************************************************************
* PawnUtilities.Shared
* CustomerConstants
* Constants file that hold enums and constant strings used in
* all the customer related modules
* Sreelatha Rengarajan 4/1/2009 Initial version
 * Sreelatha Rengarajan 4/19/2010 Added creation date to mdse record
*******************************************************************/

using System.Collections.Generic;
using System.Linq;

namespace Common.Libraries.Utility.Shared
{
     
    public enum searchCriteria
    {
        CUSTINFORMATION = 1,
        CUSTPHONENUMBER = 2,
        CUSTID = 3,
        CUSTNUMBER = 4,
        CUSTLOANNUMBER = 5,
        CUSTSSN = 6
    }

    public struct StateIdTypes
    {
        public const string STATE_IDENTIFICATION_ID = "SI";
        public const string CONCEALED_WEAPONS_PERMIT = "CW";
        public const string DRIVERLICENSE = "DRIVERLIC";
        public const string MEXICAN_CONSULATE = "MC";
    }

    public struct StateIdTypeDescription
    {
        public const string STATE_ID = "State Issued ID";
        public const string DRIVER_LIC = "Driver's License";
        public const string CW = "Concealed Weapons Permit";
    }

    public struct IdNumberTypes
    {
        public const string CONCEALED_WEAPONS_PERMIT = "CW";
        public const string PASSPORT = "PASSPORT";
    }


    public enum CustomerIdTypes
    {
        DRIVERLIC=1,
        SI,
        RI,
        MC,
        MI,
        PASSPORT,
        GI,
        II,
        CW,
        OT,
        FFL=11
    }


 
    

    public struct ComboBoxData
    {
        private string _Code;
        private string _Description;

        public ComboBoxData(string strCode, string strDesc)
        {

            this._Code = strCode;
            this._Description = strDesc;
        }

        public string Code
        {
            get
            {
                return _Code;
            }
        }

        public string Description
        {

            get
            {
                return _Description;
            }
        }

    }


    public enum customerrecord
    {
        CUSTOMER_ROW_NUMBER = 0,
        CUSTOMER_NUMBER,
        CUST_LAST_NAME,
        CUST_FIRST_NAME,
        DATE_OF_BIRTH,
        CUST_TITLE,
        CUST_MIDDLE_NAME,
        SOCIAL_SECURITY_NUMBER,
        RACE,
        GENDER,
        EYE_COLOR,
        HAIR_COLOR,
        HEIGHT,
        WEIGHT,
        PARTY_ID,
        NAME_SUFFIX,
        PREFERCONTMTHDTEXT,
        NOCALLFLAG,
        NOMAILFLAG,
        NOEMAILFLAG,
        NOFAXFLAG,
        OPTOUTFLAG,
        REMINDERCONTACT,
        PREFERCONTTIME,
        RECEIVEPROMOTIONS,
        HOWDIDYOUHEAR,        
        CREATIONDATE,
        PREFERLANGCODE,
        EMPTITLE,
        EMPNAME = 29
        

    }


    public enum customerphonerecord
    {
        PARTY_ID = 0,
        AREADIALNUMCODE,
        TELECOMNUMBER,
        EXTENSIONNUMBER,
        TELECOMNUMTYPECODE,
        COUNTRYDIALNUMCODE,
        CONTACTTYPECODE,
        TELEUSRDEFTEXT=7

    }


    public enum customeridrecord
    {
        PARTY_ID = 0,
        DATEDIDENTTYPEDESC,
        ISSUEDNUMBER,
        ISSUERNAME,
        STATE_NAME,
        EXPIRYDATE,
        IDENTTYPECODE,
        IDENTID,
        IDCREATIONDATE=8,
        ISLATEST
    }

    public enum customeraddrrecord
    {
        PARTY_ID = 0,
        CONTACTTYPECODE,
        CONTMTHDTYPECODE,
        ADDRESS1_TEXT,
        ADDRESS2_TEXT,
        CITY_NAME,
        STATE_CODE,
        STATE_NAME,
        POSTAL_CODE,
        UNIT_NUM,
        COUNTRY_CODE,
        COUNTRY_NAME,
        NOTES1,
        NOTES2=13

    }

    public enum customeremailrecord
    {
        PARTY_ID=0,
        CUSTOMERNUMBER,
        CONTACTINFOID,
        EMAILADDRESS,
        EMAILADDRESSTYPECODE=4
    }

    public enum customernotesrecord
    {
        PARTY_ID=0,
        CUSTOMERNUMBER,
        CONTACTRESULT,
        CONTACTSTATUS,
        CONTACTNOTE,
        CONTACTDATE,
        CREATIONDATE,
        UPDATEDATE,
        CREATEDBY,
        CUSTOMERNOTESCODE,
        CUSTOMERPRODUCTNOTEID,
        STORENUMBER=11

    }

    public enum customerstorecreditrecord
    {
        PARTY_ID = 0,
        STORECREDITID,
        CUSTOMERNUMBER,
        STORENUMBER,
        ENTITY_NUMBER,
        ENTITY_TYPE,
        DOC_NUMBER,
        DOC_TYPE,
        DATE_MADE,
        TIME_MADE,
        AMOUNT,
        MISC_INC,
        BALANCE,
        STATUS_CD,
        STATUS_DATE,
        STATUS_TIME,
        REF_NUMBER,
        REF_TYPE,
        CREATEDBY,
        UPDATEDBY,
        CREATIONDATE,
        LASTUPDATEDATE=21

    }


    public enum customerduprecord
    {
        BIRTHDATE = 0,
        FAMILY_NAME,
        FIRST_NAME,
        ISSUER_NAME,
        IDENT_TYPE_CODE,
        ISSUED_NUMBER,
        ADDRESS1_TEXT,
        ADDRESS2_TEXT,
        CITY_NAME,
        UNIT_NUM,
        STATE_PROVINCE_NAME,
        POSTAL_CODE_TEXT,
        STATE_NAME,
        CUSTOMER_NUMBER = 13
    }

    public struct holdstransactioncursor
    {
        public const string TRANSACTIONDATE="time_made";
        public const string TRANSACTIONTYPE="transactiontype";
        public const string TICKETNUMBER="ticket_number";
        public const string STATUS="pstatus";
        public const string PFISTATE="temp_status";
        public const string LOANAMOUNT="org_amount";
        public const string CURRENTPRINCIPALAMOUNT = "cur_amount";
        public const string PREVIOUSTICKETNUMBER = "prev_ticket";
        public const string ORIGINALTICKETNUMBER="org_ticket";
        public const string RELEASEDATE="release_date";
        public const string CREATEDBY="createdby";
        public const string UPDATEDBY="updatedby";
        public const string CREATIONDATE="creationdate";
        public const string LASTUPDATEDDATE="lastupdatedate";
        public const string HOLDCOMMENT = "hold_comment";
    }

    public struct holdsmdsecursor
    {
        public const string ITEMDESCRIPTION="md_desc";
        public const string ICN="icn";
        public const string STATUS="mstatus";
        public const string AMOUNT="org_amount";
        public const string ICNDOC="icn_doc";
        public const string ICNDOCTYPE="icn_doc_type";
        public const string ICNITEM="icn_item";
        public const string AISLE="loc_aisle";
        public const string SHELF="loc_shelf";
        public const string OTHERLOCATION = "location";
        public const string RELEASEDATE = "release_date";
        public const string HOLDID = "hold_type_id";
        public const string GUNNUMBER = "gun_number";
        public const string HOLDCOMMENT = "hold_comment";
        public const string HOLDCREATIONDATE = "creationdate";
        public const string CATCODE = "cat_code";
    }

    public struct Tempstatuscursor
    {
        public const string STORENUMBER = "store_number";
        public const string TICKETNUMBER = "loan_number";
        public const string TEMPSTATUS = "temp_status";

    }


    public struct CustomerPhoneTypes
    {
        public const string HOME_NUMBER = "HOME";
        public const string CELL_NUMBER = "CELL";
        public const string WORK_NUMBER = "WORK";
        public const string VOICE_NUMBER = "VOICE";
        public const string MOBILE_NUMBER = "MOBILE";
        public const string PAGER_NUMBER = "PAGER";
        public const string FAX_NUMBER = "FAX";



    }

    public struct CustomerAddressTypes
    {
        public const string HOME_ADDRESS = "HOME";
        public const string WORK_ADDRESS = "WORK";
        public const string MAILING_ADDRESS = "ALTERNATE1";
        public const string ADDITIONAL_ADDRESS = "ALTERNATE2";
    }

    public struct CustomerEmailTypes
    {
        public const string PRIMARY_EMAIL = "EMAIL";
        public const string SECONDARY_EMAIL = "EMAIL2";

    }

    public struct CustomerContactPermissions
    {
        public const string NOCALLS = "No Calls";
        public const string NOEMAILS = "No Emails";
        public const string NOFAXES = "No Faxes";
        public const string NOMAILS = "No Mails";
        public const string OPTOUT = "Opt Out";
        public const string NOPERMISSION = "NONE";
    }

    public struct CustomerTypes
    {
        public const string PERSONTYPE = "PERSON";
    }



    public struct CustomerNoteTypes
    {
        public const string PHYSICALDESCNOTES = "PHYSDESC";
        public const string CUSTCOMMENTS = "CUSTCOMMENT";
    }

    public class CustLoanLostTicketFee
    {
        public string LoanNumber;
        public string StoreNumber;
        public string LSDTicket;
        public decimal LostTicketFee;
        public bool TicketLost;
        public const string LOSTTICKETTYPE = "L";
        public const string STOLENTICKETTYPE = "S";
        public const string DESTROYEDTICKETTYPE = "D";
    }

    public struct CustomerValidAge
    {
        public const int PAWNCUSTLEGALAGE = 18;
    }

    public class LookupCustomerSearchData
    {
        public string LastName=string.Empty;
        public string LoanNumber=string.Empty;
        public string PhoneAreaCode=string.Empty;
        public string PhoneNumber=string.Empty;
        public string FirstName=string.Empty;
        public string CustNumber=string.Empty;
        public string DOB=string.Empty;
        public string IdTypeCode=string.Empty;
        public string IDNumber=string.Empty;
        public string IDIssuer=string.Empty;
        public string SSN=string.Empty;
        public string IdTypeDesc = string.Empty;
        public string IdIssuerCode = string.Empty;
        public int TypeOfSearch=0;        
    }

     //Structure for transactions that need override
    public struct OverrideTransaction
    {
        public int TicketNumber
        {
            get;
            set;
        }
        public List<Commons.StringValue> ReasonForOverride
        {
            get;
            set;
        }
        public ManagerOverrideTransactionType TransactionType
        {
            get;
            set;
        }
        public ManagerOverrideType OverrideType
        {
            get;
            set;
        }
    
    }

}
