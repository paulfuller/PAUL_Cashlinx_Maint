/********************************************************************
* PawnUtilities.Shared
* CustomerConstants
* Constants file that hold enums and constant strings used in
* all the customer related modules
* Sreelatha Rengarajan 4/1/2009 Initial version
 * Sreelatha Rengarajan 4/19/2010 Added creation date to mdse record
*******************************************************************/
namespace Common.Libraries.Utility.Shared
{
     
    public enum VendorSearchCriteria
    {
        VENDORNAME = 1,
        VENDORTAXID = 2
    }

    public class LookupVendorSearchData
    {

        public string VendName="";
        public string TaxID="";
        public int TypeOfSearch=0;        
    }

    public struct VendorTriggerTypes
    {
        public const string ADDNEWVENDOR = "Addnewvendor";
        public const string LOOKUPVENDOR = "LookupVendor";
        public const string EXISTINGVENDOR = "ExistingVendor";
    }

    public enum vendorrecord
    {
        ROW_NUMBER = 0,
        ID,
        TAX_ID,
        NAME,
        ADDRESS1,
        ADDRESS2,
        ZIP_CODE,
        ZIP_PLUS4,
        CITY,
        STATE,
        COMMENT,
        FFL,
        CREATIONSTORE,
        CONTACT_NAME,
        CONTACT_PHONE,
        CONTACT_PHONE2,
        FAX_PHONE,
        CREATIONDATE
    }

}
