using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Pawn.Forms.Inquiry.LoanInquiry;
using Pawn.Logic;
using Common.Libraries.Utility.Logger;

namespace Pawn.Forms.Inquiry.LoanInquiry
{
    public class PawnInquiry : Inquiry
    {
        public enum searchDateType_enum
        {
            [StringDBMap("Made", "DATE_MADE")]
            MADE,
            [StringDBMap("Due", "DATE_DUE")]
            DUE,
            [StringDBMap("PFI Eligible", "PFI_ELIG")]
            PFI_ELIG,
            [StringDBMap("Status", "STATUS_DATE")]
            STATUS,
            [StringDBMap("PFI Notification", "PFI_NOTE")]
            PFI_NOTICE,
            [StringDBMap("PFI Mailer Printed", "PFI_MAILER")]
            PFI_MAILER
        };

        public enum searchTicketType_enum
        {
            [StringDBMap("Current", "TICKET_NUMBER")]
            CURRENT,
            [StringDBMap("Previous", "PREV_TICKET")]
            PREVIOUS,
            [StringDBMap("Original", "ORG_TICKET")]
            ORIGINAL
        };

        public enum searchStatus_enum
        {
            [StringDBMap("", "")]
            ALL,
            [StringDBMap("Active", "ACTIVE")]
            ACTIVE,
            [StringDBMap("Inactive", "INACTIVE")]
            INACTIVE,
            [StringDBMap("PFI Working", "PFI_WORKING")]
            PFI_WORKING,
            [StringDBMap("Hold", "HOLD")]
            HOLD,
            [StringDBMap("In Pawn", "IP")]
            IP,
            [StringDBMap("Void", "VO")]
            VO,
            [StringDBMap("PFI", "PFI")]
            PFI,
            [StringDBMap("Picked Up", "PU")]
            PU,
            [StringDBMap("Renewed", "RN")]
            RN,
            [StringDBMap("Release To Claimant", "RTC")]
            RTC,
            [StringDBMap("Police Seize", "PS")]
            PS,
            [StringDBMap("Paid Down", "PD")]
            PD
        };

        public bool byDate = false;
        public searchDateType_enum dateType;
        public searchTicketType_enum ticketType;
        public string startDate, endDate;
        public int lowTicketNumber = -1;
        public int highTicketNumber = -1;
        public double lowAmount = -1;
        public double highAmount = -1;
        public searchStatus_enum status;
        public string pfiMailer;
        public string userID;
        public enum sortField_enum
        {
            [StringDBMap("Date", "DATE")]
            DATE,
            [StringDBMap("Loan Amount", "PRIN_AMOUNT")]
            AMOUNT,
            [StringDBMap("Current Status", "STATUS_CD")]
            STATUS,
            [StringDBMap("Ticket Number", "TICKET")]
            TICKET,
            [StringDBMap("User ID", "ENT_ID")]
            USER
        };
        public sortField_enum sortBy;


        public DataSet getData()
        {
            DataSet outputDataSet = getDataSet("PAWN_Inquiries", "get_pawn_data",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_byDate", (byDate) ? "Y" : "N"),
                                    new OracleProcParam("p_dateType", StringDBMap_Enum<searchDateType_enum>.toDBValue(dateType)),
                                    new OracleProcParam("p_startDate", startDate),
                                    new OracleProcParam("p_endDate", endDate),
                                    new OracleProcParam("p_ticketType", StringDBMap_Enum<searchTicketType_enum>.toDBValue(ticketType)),
                                    new OracleProcParam("p_lowTicket", (lowTicketNumber >= 0) ? lowTicketNumber.ToString() : ""),
                                    new OracleProcParam("p_highTicket", (highTicketNumber >= 0) ? highTicketNumber.ToString() : ""),
                                    new OracleProcParam("p_lowLoanAmount", (lowAmount >= 0) ? String.Format("{0:f}", lowAmount) : ""),
                                    new OracleProcParam("p_highLoanAmount", (highAmount >= 0) ? String.Format("{0:f}", highAmount) : ""),
                                    new OracleProcParam("p_Status", StringDBMap_Enum<searchStatus_enum>.toDBValue(status)),
                                    new OracleProcParam("p_PFIMailer", pfiMailer),
                                    new OracleProcParam("p_userID", userID),
                                    new OracleProcParam("p_storenumber", CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber),
                                    new OracleProcParam("p_sort", StringDBMap_Enum<sortField_enum>.toDBValue(sortBy)),
                                    new OracleProcParam("p_sortDir", StringDBMap_Enum<sortDir_enum>.toDBValue(sortDir))
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_pawn_list", "PAWN_INFO"},
                                    {"o_pawn_cust_list", "PAWN_CUST"},
                                    {"o_pawn_mdse_list", "PAWN_MDSE"}
                                });

            if (outputDataSet.IsNullOrEmpty())
            {
                return null;
            }

            // need to trap error in the case that referential integrity is not preserved.
            outputDataSet.Tables["PAWN_INFO"].Columns.Add("CUST_NAME", typeof(string));
            string errorType = "Customer";
            try
            {
                outputDataSet.Relations.Add("customerRelation", outputDataSet.Tables["PAWN_INFO"].Columns["TICKET_NUMBER"],
                                                outputDataSet.Tables["PAWN_CUST"].Columns["TICKET_NUMBER"]);

                errorType = "Merchandise";
                outputDataSet.Relations.Add("merchandiseRelation", outputDataSet.Tables["PAWN_INFO"].Columns["TICKET_NUMBER"],
                                                outputDataSet.Tables["PAWN_MDSE"].Columns["TICKET_NUMBER"]);
            }
            catch
            {
                throw new BusinessLogicException(string.Format("An error was detected in the {0} data retreived", errorType));
            }

            if (outputDataSet.DefaultViewManager.DataViewSettings["PAWN_MDSE"] != null)
                outputDataSet.DefaultViewManager.DataViewSettings["PAWN_MDSE"].Sort = "TICKET_NUMBER, ICN";

            foreach (DataRow r in outputDataSet.Tables["PAWN_INFO"].Rows)
            {
                DataRow[] customer = r.GetChildRows("customerRelation");
                r.SetField<string>("CUST_NAME", customer[0].Field<string>("CUST_NAME"));
            }

            return outputDataSet;
        }

        public override string sortByField()
        {
            string retval;


            switch (sortBy)
            {
                case sortField_enum.DATE:
                    if (byDate)
                        retval = StringDBMap_Enum<searchDateType_enum>.toDBValue(dateType);
                    else
                        retval = "DATE_MADE";

                    break;

                case sortField_enum.TICKET:
                    if (byDate)
                        retval = "TICKET_NUMBER";
                    else
                        retval = StringDBMap_Enum<searchTicketType_enum>.toDBValue(ticketType);
                    break;

                default:
                    retval = StringDBMap_Enum<sortField_enum>.toDBValue(sortBy);
                    break;
            }


            return retval;
        }

        public override string ToString()
        {
            string retval = "";
            int len = 0;

            if (byDate)
            {
                retval += string.Format("{0} Date: {1} to {2} \n",
                    StringDBMap_Enum<searchDateType_enum>.displayValue(dateType), startDate, endDate);
                len = StringDBMap_Enum<searchDateType_enum>.displayValue(dateType).Length;
            }
            else
            {
                if (lowTicketNumber >= 0 || highTicketNumber >= 0)
                {
                    retval += string.Format("{0} Ticket:", StringDBMap_Enum<searchTicketType_enum>.displayValue(ticketType));


                    if (lowTicketNumber >= 0 && highTicketNumber >= 0)
                        retval += string.Format("{0} to {1} \n", lowTicketNumber, highTicketNumber);

                    else if (lowTicketNumber >= 0)
                        retval += string.Format("{0}\n", lowTicketNumber);

                    else
                        retval += string.Format("{0}\n", highTicketNumber);

                    len = StringDBMap_Enum<searchDateType_enum>.displayValue(dateType).Length;
                }

            }

            if (status != searchStatus_enum.ALL)
                retval += string.Format("Status: {0}\n",
                    StringDBMap_Enum<searchStatus_enum>.displayValue(status));
            //-------
            if (byDate)
            {
                if (sortBy == sortField_enum.DATE)
                {
                    retval += string.Format("Sorted By: {0} DATE, {1}\n",
                        StringDBMap_Enum<searchDateType_enum>.displayValue(dateType),
                        StringDBMap_Enum<sortDir_enum>.displayValue(sortDir));
                }
                else if (sortBy == sortField_enum.TICKET)
                {
                    retval += string.Format("Sorted By: Current Ticket #,  {0}\n",
                        StringDBMap_Enum<sortDir_enum>.displayValue(sortDir));
                }
                else
                {
                    retval += string.Format("Sorted By: {0},  {1}\n",
                        StringDBMap_Enum<sortField_enum>.displayValue(sortBy),
                        StringDBMap_Enum<sortDir_enum>.displayValue(sortDir));
                }
            }
            else
            {
                if (sortBy == sortField_enum.DATE)
                {
                    retval += string.Format("Sorted By: MADE DATE,  {0}\n",
                        StringDBMap_Enum<sortDir_enum>.displayValue(sortDir));
                }
                else if (sortBy == sortField_enum.TICKET)
                {
                    retval += string.Format("Sorted By: {0} Ticket #,  {1}\n",
                        StringDBMap_Enum<searchTicketType_enum>.displayValue(ticketType),
                        StringDBMap_Enum<sortDir_enum>.displayValue(sortDir));
                }
                else
                {
                    retval += string.Format("Sorted By: {0},  {1}\n",
                        StringDBMap_Enum<sortField_enum>.displayValue(sortBy),
                        StringDBMap_Enum<sortDir_enum>.displayValue(sortDir));
                }

            }


            return retval;
        }
    }
}
