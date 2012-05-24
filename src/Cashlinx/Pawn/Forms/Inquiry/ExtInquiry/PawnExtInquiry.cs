using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Inquiry.ExtInquiry
{
    public class PawnExtInquiry : Inquiry
    {
        public bool byDate = false;
        public string startDate, endDate;
        public int lowTicketNumber, highTicketNumber;
        public double lowAmount, highAmount;

        public string userID;
        public enum sortField_enum
        {
            [StringDBMap("Extension Date", "CREATIONDATE")]
            DATE,
            [StringDBMap("Extension Amount", "REF_AMT")]
            AMOUNT,
            [StringDBMap("Ticket Number", "TICKET_NUMBER")]
            TICKET,
            [StringDBMap("User ID", "ENT_ID")]
            USER
        };
        public sortField_enum sortBy;

        public DataSet getData()
        {
            DataSet outputDataSet = getDataSet("PAWN_Inquiries", "get_extension_data",
                                        new List<OracleProcParam>
                                        {
                                            new OracleProcParam("p_byDate", (byDate) ? "Y" : "N"),
                                            new OracleProcParam("p_startDate", startDate),
                                            new OracleProcParam("p_endDate", endDate),
                                            new OracleProcParam("p_lowTicket", (lowTicketNumber > 0) ? lowTicketNumber.ToString() : ""),
                                            new OracleProcParam("p_highTicket", (highTicketNumber > 0) ? highTicketNumber.ToString() : ""),
                                            new OracleProcParam("p_lowAmount", (lowAmount > 0) ? String.Format("{0:f}", lowAmount) : ""),
                                            new OracleProcParam("p_highAmount", (highAmount > 0) ? String.Format("{0:f}", highAmount) : ""),
                                            new OracleProcParam("p_userID", userID),
                                            new OracleProcParam("p_storenumber", CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber),
                                            new OracleProcParam("p_sort", StringDBMap_Enum<sortField_enum>.toDBValue(sortBy)),
                                            new OracleProcParam("p_sortDir", StringDBMap_Enum<sortDir_enum>.toDBValue(sortDir)),
                                        },
                                        new Dictionary<string, string>
                                        {
                                            {"o_ext_data", "EXT_INFO"},
                                            {"o_pawn_cust_list", "PAWN_CUST"}
                                        });

            if (outputDataSet.IsNullOrEmpty())
            {
                return null;
            }

            try
            {
                outputDataSet.Tables["EXT_INFO"].Columns.Add("CUST_NAME", typeof(string));
                outputDataSet.Relations.Add("customerRelation", outputDataSet.Tables["PAWN_CUST"].Columns["TICKET_NUMBER"],
                                                outputDataSet.Tables["EXT_INFO"].Columns["TICKET_NUMBER"]);
            }
            catch
            {
                throw new BusinessLogicException(string.Format("An error was detected in the {0} data retreived", "Customer"));
            }

            foreach (DataRow r in outputDataSet.Tables["EXT_INFO"].Rows)
            {
                DataRow customer = r.GetParentRow("customerRelation");
                r.SetField<string>("CUST_NAME", customer.Field<string>("CUST_NAME"));
            }

            return outputDataSet;
        }

        public override string ToString()
        {
            var retval = string.Empty;
            //int len = 0;

            if (byDate)
            {
                retval = string.Format("Extension Date: {0} to {1}\n", startDate, endDate);
            }
            else
            {
                retval = string.Format("Ticket Number: {0} to {1}\n", lowTicketNumber, highTicketNumber);
            }

            if (lowAmount > 0 || highAmount > 0)
                retval += string.Format("Extension Amount between {0:c} and {1:c}\n", lowAmount, highAmount);

            if (userID.Length > 0)
                retval += string.Format("User ID: {0}\n", userID);

            retval += string.Format("Sorted By: {0},  {1}\n",
                StringDBMap_Enum<sortField_enum>.displayValue(sortBy),
                StringDBMap_Enum<sortDir_enum>.displayValue(sortDir));

            return retval;
        }

        public override string sortByField()
        {
            return StringDBMap_Enum<sortField_enum>.toDBValue(sortBy);
        }
    }
}
