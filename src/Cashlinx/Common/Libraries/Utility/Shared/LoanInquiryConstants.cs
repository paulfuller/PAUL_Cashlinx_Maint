/********************************************************************
* PawnUtilities.Shared
* LoanInquiryConstants
* This class has all the constants, enums for the loan inquiry 
* S. Murphy 2/19/2010 Initial version
*******************************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Common.Libraries.Utility.Shared
{
    public enum InquiryDataTypes
    {
        Customer, Loan, Item
    }
    public enum InquiryCustomerCriteria
    {
        LastName = 1, 
        FirstName, CustomerNumber, Address, City, State, ZipCode, CustomerID, Phone, DateOfBirth, 
        Age, SocialSecurityNumber, CustomerSince, Sex, Race, Weight, Height, Hair, Eyes, CAEmpNumber, 
        Comments = 21
    }
    public enum InquiryLoanCriteria
    {
        TicketNumber = 1,
        DateMade, TimeMade, LoanAmount, LoanStatus, DueDate, PFIEligibleDateOrLastDayOfGrace, PFIDay, 
        StatusDate, ReasonCode, PreviousTicketNumber, InterestAmount, InterestAmountNegotiated, ServiceCharge, 
        ServiceChargeNegotiated, OriginalTicketNumber, LateCharge, Hold, PFINotificationDate, RefundAmount, Extend, 
        Clothing = 22
    }
    public enum InquiryItemCriteria
    {
        Category = 1, 
        Status, LoanAmount, PFIAmount, RetailAmount, ICN, Location, Manufacturer, ModelNumber, SerialNumber, Description,
        JewelryCase = 12
    }
    public class InquiryType
    {
        public static string[,] Customer = { {"Last Name", "customer.cust_last_name"}, {"First Name", "customer.cust_first_name"},
                {"Customer #", "customer.customer_number"}, {"Address", "customer.cust_address, customer.cust_address"}, 
                {"City", "customer.cust_city"}, {"State", "customer.cust_state"}, {"Zip Code", "customer.cust_zip"}, 
                {"Customer ID", "customer.cust_state_id, customer.cust_state_id2, customer.cust_suppl_state, customer.cust_id_type, customer.cust_id_number"}, 
                {"Phone", "customer.cust_phone, customer.cust_phone"}, {"Date of Birth", ""}, {"Age", ""}, {"Social Security Number", ""}, 
                {"Customer Since", ""}, {"Sex", ""}, {"Race", ""}, {"Weight", ""}, {"Height", ""}, 
                {"Hair", ""}, {"Eyes", ""}, {"CA Emp #", ""}, {"Comments", ""} };

        public static string[,] Loan = { {"Ticket #", ""}, {"Date Made", ""}, {"Time Made", ""}, 
                {"Loan Amount", ""}, {"Loan Status", ""}, {"Due Date", ""},
                {"PFI Eligible Date or Last Day of Grace", ""}, {"PFI Day", ""}, {"Status Date", ""}, 
                {"Reason Code", ""}, {"Previous Ticket Number", ""}, {"Interest Amount", ""}, 
                {"Interest Amount Negotiated", ""}, {"Service Charge", ""}, {"Service Charge Negotiated", ""}, 
                {"Original Ticket Number", ""}, {"Late Charge", ""}, {"Hold", ""}, {"PFI Notification Date", ""}, 
                {"Refund Amount", ""}, {"Extend", ""}, {"Clothing", ""} };

        public static string[,] Item = { {"Category", ""}, {"Status", ""}, {"Loan Amount", ""}, 
                {"PFI Amount", ""}, {"Retail Amount", ""}, {"ICN", ""}, {"Location", ""}, {"Manufacturer", ""}, 
                {"Model Number", ""}, {"Serial Number", ""}, {"Description", ""}, {"Jewelry Case", ""} };
    }
    public class InquirySort
    {
        public static string[,] Customer = { {"NA", ""}, {"Last Name", "customer.last_name"}, {"Zip Code", "ZipFIELD"}, 
                                    {"Financial Institution", "FinancialInstitutionFIELD"} };

        public static string[,] Loan = { {"NA", ""}, {"Transaction Date", "Transaction_DateFIELD"}, 
                                          {"Shop Loan Status", "Shop_Loan_StatusZipFIELD"}, {"Status Date", "Status_DateFIELD"},
                                          {"Pawn Ticket Number", "Pawn_Ticket_NumberFIELD"}, {"Pawn Loan Amount", "Pawn_Loan_AmountFIELD"} };

        public static string[,] Item = { {"NA", ""}, {"Category", "CategoryFIELD"}, 
                                          {"Item Status", "Item_StatusFIELD"}, {"Merchandise Loan Amount", "Merchandise_Loan_AmountFIELD"},
                                          {"Financial Institution", "FinancialInstitutionFIELD"} };
    }
    public class InquirySearchTypes
    {
        public const string EQUALS = "=";
        public const string LESSTHAN = "<";
        public const string GREATERTHAN = ">";
        public const string RANGE = "Range";
        public const string INCLUDES = "Includes";
        public const string CONTAINS = "Contains";
    }
    public class InquiryConst
    {
        public const string SELECT = "Select";
        public const string ASC = "Ascending Order";
        public const string DESC = "Descending Order";
        public const string RESULTLABEL = " Search Criteria";
    }
    public class InquirySelectedCriteria
    {
        public string CriteriaType = string.Empty;
        public string CriteriaDisplayField = string.Empty;
        public string CriteriaDBField = string.Empty;
        public string CriteriaOperator = string.Empty;
        public string[] CriteriaValue = null;
        public bool IsValid = false;

        public InquirySelectedCriteria()
        {

        }
        public InquirySelectedCriteria(string type, string displayField, string dbField, string oper, string[] value, bool valid)
        {
            CriteriaType = type;
            CriteriaDisplayField = displayField;
            CriteriaDBField = dbField;
            CriteriaOperator = oper;
            CriteriaValue = value;
            IsValid = valid;
        }
    }
    public class InquiryCriteria
    {
        public List<InquirySelectedCriteria> SelectedCriteria;
        public List<Shops> SelectedShops;
        public InquiryDataTypes QueryType;
        public ComboBoxData SortField;
        public string SortDirection;

        public InquiryCriteria()
        {
            SelectedCriteria = new List<InquirySelectedCriteria>();
            SelectedShops = new List<Shops>();
            QueryType = InquiryDataTypes.Customer;
            SortField = new ComboBoxData("", "");
            SortDirection = string.Empty;
        }
    }
    public class Shops
    {
        public string Company { set; get; }
        public string Region { set; get; }
        public string Market { set; get; }
        public string Shop { set; get; }

        public Shops()
        {

        }
        public Shops(string company, string region, string market, string shop)
        {
            Company = company;
            Region = region;
            Market = market;
            Shop = shop;
        }
    }
    public class InquiryCommon
    {
        public InquiryCommon()
        {

        }
        public void PopulateComboBox(ComboBox combo, string[,] values, bool addSelect)
        {
            ArrayList array = new ArrayList();
 
            if (addSelect)
            {
                array.Add(new ComboBoxData("0", InquiryConst.SELECT));
            }

            for (int i = 0; i < values.Length / 2; i++)
            {
                array.Add(new ComboBoxData(values[i, 1].ToString(), values[i, 0].ToString()));
            }

            combo.DataSource = array;
            combo.DisplayMember = "Description";
            combo.ValueMember = "Code";
        }
    }
}

