using Common.Controllers.Database.Oracle;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Collections.Generic;
using System.Data;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Inquiry.PartialPaymentInquiry
{
    public class PartialPaymentInquiry : Inquiry
    {
        public enum sortField_enum
        {
            [StringDBMap("Payment Date", "PAYMENTDATE")]
            PAYMENTDATE,
            [StringDBMap("Loan Ticket Number", "TICKETNUMBER")]
            TICKETNUMBER,
            [StringDBMap("Customer Last Name", "LASTNAME")]
            LASTNAME,
            [StringDBMap("DOB", "DOB")]
            DOB,
            [StringDBMap("Customer Number", "CUSTOMERNUMBER")]
            CUSTOMERNUMBER,
            [StringDBMap("Partial Payment Amount", "AMOUNT")]
            PARTIALPAYMENTAMOUNT
        };
        public sortField_enum sortBy;

        public string startDate;
        public string endDate;
        public decimal lowAmount = -1;
        public decimal highAmount = -1;
        public string lastName;
        public string firstName;
        public string dob;
        public string customerNumber;
        public int loanTicketNumber = -1;

        //public static DataSet getJubaPFITest(string pfidate, string storeNumber, string pfi_mailer_days, string p_pfiMailerOption)
        //{
        //    DataSet outputDataSet = getDataSet("ED_PAWN", "get_pfi_mailer_data",
        //                        new List<OracleProcParam>
        //                        {
        //                            new OracleProcParam("p_pfi_date", pfidate),
        //                            new OracleProcParam("p_pfi_mailer_adjustment_days", pfi_mailer_days),
        //                            new OracleProcParam("p_store_number", storeNumber),
        //                            new OracleProcParam("p_startTicketNumber", "0"),
        //                            new OracleProcParam("p_endTicketNumber", "0"),
        //                            new OracleProcParam("p_pfiMailerOption", p_pfiMailerOption),
        //                        },
        //                        new Dictionary<string, string>
        //                        {
        //                            {"o_pfi_mailer_info", "pfi_mailer_data"},
        //                        });

        //    if (outputDataSet.IsNullOrEmpty())
        //    {
        //        return null;
        //    }

        //    return outputDataSet;
        //}


        public static DataSet getDetailData(string storeNumber, string _customerNumber, int _loanTicketNumber)
        {
            DataSet outputDataSet = getDataSet("PAWN_Inquiries", "get_partial_payment_detail",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_storenumber", storeNumber),
                                    new OracleProcParam("p_customerNumber", _customerNumber),
                                    new OracleProcParam("p_loanTicketNumber", (_loanTicketNumber >= 0) ? _loanTicketNumber.ToString() : "")
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_customer_info", "customer_info"},
                                    {"o_partial_pmt_detail_info", "partial_pmt_detail_info"}
                                });

            if (outputDataSet.IsNullOrEmpty())
            {
                return null;
            }

            return outputDataSet;
        }

        public DataSet getData()
        {
            DataSet outputDataSet = getDataSet("PAWN_Inquiries", "get_partial_payment_data",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_storenumber", GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                    new OracleProcParam("p_startDate", startDate),
                                    new OracleProcParam("p_endDate", endDate),
                                    new OracleProcParam("p_lowTransfer", (lowAmount >= 0) ? String.Format("{0:f}", lowAmount) : ""),
                                    new OracleProcParam("p_highTransfer", (highAmount >= 0) ? String.Format("{0:f}", highAmount) : ""),
                                    new OracleProcParam("p_lastName", lastName),
                                    new OracleProcParam("p_firstName", firstName),
                                    new OracleProcParam("p_dob", dob),
                                    new OracleProcParam("p_customerNumber", customerNumber),
                                    new OracleProcParam("p_loanTicketNumber", (loanTicketNumber >= 0) ? loanTicketNumber.ToString() : ""),
                                    new OracleProcParam("p_sortBy", StringDBMap_Enum<sortField_enum>.toDBValue(sortBy)),
                                    new OracleProcParam("p_sortDir", StringDBMap_Enum<sortDir_enum>.toDBValue(sortDir))
                                },
                                new Dictionary<string, string>
                                {
                                    {"partial_pmt_info_list", "PARTIAL_PAYMENT_INQ"}
                                });

            if (outputDataSet.IsNullOrEmpty())
            {
                return null;
            }
            
            return outputDataSet;
        }

        public override string ToString()
        {
            string retval = string.Empty;
            
            // startDate, endDate
            if (this.startDate.Length != 0 || this.endDate.Length != 0)
            {
                if (this.startDate.Length != 0 && this.endDate.Length != 0)
                {
                    retval += string.Format("Partial Payments Made: {0:C} to {1:C} \n", this.startDate, this.endDate);
                }
                else if (this.startDate.Length != 0)
                {
                    retval += string.Format("Partial Payments on: {0:C}\n", this.startDate);
                }
                else
                {
                    retval += string.Format("Partial Payments on or before: {0:C}\n", this.endDate);
                }
            }

            // lowAmount, highAmount
            if (this.lowAmount >= 0 || this.highAmount >= 0)
            {
                if (this.lowAmount >= 0 || this.highAmount >= 0)
                {
                    retval += string.Format("Partial Payments Amount: {0:C} to {1:C} \n", this.lowAmount, this.highAmount);
                }
                else if (this.lowAmount >= 0)
                {
                    retval += string.Format("Partial Payments Exactly: {0:C}\n", this.lowAmount);
                }
                else
                {
                    retval += string.Format("Partial Payments less than or equal to: {0:C}\n", this.highAmount);
                }
            }
            
            // lastName, firstName
            if (this.lastName.Length != 0 && this.firstName.Length != 0)
            {
                retval += string.Format("Name: {0}, {1}\n", lastName, firstName);
            }
            else if (this.lastName.Length != 0)
            {
                retval += string.Format("Name: {0}\n", lastName);
            }
            else if (this.firstName.Length != 0)
            {
                retval += string.Format("Name: {0}\n", firstName);
            }

            // DOB
            if (this.dob.Length != 0)
            {
                retval += string.Format("Date of Birth: {0}\n", this.dob);
            }

            // customerNumber
            if (this.customerNumber.Length != 0)
            {
                retval += string.Format("Customer Number: {0}\n", this.customerNumber);
            }

            // loanTicketNumber
            if (this.loanTicketNumber > 0)
            {
                retval += string.Format("Loan Ticket Number: {0}\n", this.loanTicketNumber);
            }
            
            // Sort By, Asc, Dsc
            retval += string.Format("\nSorted By: {0},  {1}\n",
                StringDBMap_Enum<sortField_enum>.displayValue(sortBy),
                StringDBMap_Enum<sortDir_enum>.displayValue(sortDir));
            
            return retval;
        }

    }
}
