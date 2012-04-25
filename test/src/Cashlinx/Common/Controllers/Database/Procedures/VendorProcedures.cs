/**************************************************************************************************************
* CashlinxDesktop.DesktopProcedures
* CustomerProcedures 
* Acts as the Middle layer functions called by customer related use case forms
* to talk to the database layer and to set the customer object with the data
* entered in the forms

**************************************************************************************************************/

using System;
using System.Data;
using Common.Controllers.Application;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Common.Controllers.Database.Procedures
{
    public static class VendorProcedures
    {
        public const string DUPLICATESSNORACLEERRORCODE = "101";



        # region Public Methods

        public static VendorVO getVendorDataByID(string id, string storeNumber)
        {
            bool retValue = false;
            string errorCode = "";
            string errorMsg = "";

            DataTable vendDatatable = new DataTable();

            VendorVO vendorObject = new VendorVO();

            retValue = VendorDBProcedures.ExecuteLookupVendor("",
                                                              "",
                                                              storeNumber,
                                                              out vendDatatable,
                                                              out errorCode,
                                                              out errorMsg);


            if (retValue && errorCode == "")
            {
                vendorObject.NewVendor = false;
                //Get the row data from the custdatatable which pertains to customer
                DataRow[] vendorRows = vendDatatable.Select("vendor_id='" + id + "'");
                setVendorData(vendorRows[0], vendorObject);

            }

            return vendorObject;
        }

        public static VendorVO getVendorDataInObject(string id, DataRow selectedVendorRow)
        {
            VendorVO vendorObject = new VendorVO();
            vendorObject.NewVendor = false;
            setVendorData(selectedVendorRow, vendorObject);

            return vendorObject;

        }

        private static void setVendorData(DataRow vendorRow, VendorVO vendorObject)
        {
            vendorObject.ID = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.ID], "");
            vendorObject.TaxID = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.TAX_ID], "");

            vendorObject.Name = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.NAME], "");
            vendorObject.Address1 = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.ADDRESS1], "");
            vendorObject.Address2 = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.ADDRESS2], "");
            vendorObject.ZipCode = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.ZIP_CODE], "");
            vendorObject.Zip4 = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.ZIP_PLUS4], "");
            vendorObject.City = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.CITY], "");
            vendorObject.State = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.STATE], "");
            vendorObject.Comment = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.COMMENT], "");

            vendorObject.Ffl = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.FFL], "");
            vendorObject.CreationStore = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.CREATIONSTORE], "");
            //vendorObject.ActiveFlag = Utilities.GetBooleanValue(vendorRow.ItemArray[(int)vendorrecord.ACTIVE_FLAG], false);
            vendorObject.ContactName = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.CONTACT_NAME], "");
            vendorObject.ContactPhone = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.CONTACT_PHONE], "");
            vendorObject.ContactPhone2 = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.CONTACT_PHONE2], "");

            vendorObject.faxPhone = Utilities.GetStringValue(vendorRow.ItemArray[(int)vendorrecord.FAX_PHONE], "");

            vendorObject.CreationDate = Utilities.GetDateTimeValue(vendorRow.ItemArray[(int)vendorrecord.CREATIONDATE], DateTime.Now);

        }



        #endregion

        public static bool AddVendor(VendorVO vendor, string userId, string storeNumber, out string vendorID, out string errorDesc)
        {
            string errorCode;
            string errorMsg;

            bool retValue = VendorDBProcedures.InsertVendor(
                   vendor.Name, vendor.TaxID, vendor.Ffl, vendor.Address1,
                   vendor.Address2, vendor.ZipCode, vendor.Zip4, vendor.City,
                   vendor.State, vendor.ContactPhone, vendor.ContactPhone2,
                   vendor.faxPhone, vendor.ContactName, vendor.Comment, storeNumber, ShopDateTime.Instance.ShopDate.FormatDate(),
                   vendor.ID, out vendorID, userId, out errorCode, out errorMsg);
            if (!retValue)
            {
                errorDesc = errorCode.Equals(DUPLICATESSNORACLEERRORCODE) ? DUPLICATESSNORACLEERRORCODE : "";
            }
            else
                errorDesc = "";

            return retValue;
        }


        public static void setVendorDataInObject(ref VendorVO Vendor, string strName, string strTaxID,
            string strFfl, string strAddress, string strAddress2, string strZip, string strZip4,
            string strCity, string strState, string strPhone,
            string strPhone2, string strFax, string strContact, string strComment)
        {
            Vendor.Name = strName;
            Vendor.TaxID = strTaxID;
            Vendor.Ffl = strFfl;
            Vendor.Address1 = strAddress;
            Vendor.Address2 = strAddress2;
            Vendor.ZipCode = strZip;
            Vendor.Zip4 = strZip4;
            Vendor.City = strCity;
            Vendor.State = strState;
            Vendor.ContactPhone = strPhone;
            Vendor.ContactPhone2 = strPhone2;
            Vendor.faxPhone = strFax;
            Vendor.ContactName = strContact;
            Vendor.Comment = strComment;
        }


    }
}
