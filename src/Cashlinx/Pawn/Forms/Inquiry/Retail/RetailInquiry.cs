using Common.Controllers.Database.Oracle;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Collections.Generic;
using Common.Libraries.Utility.Shared;
using System.Data;
using Pawn.Logic;

namespace Pawn.Forms.Inquiry.Retail
{
    public class RetailInquiry : Inquiry
    {
        public enum searchStatus_enum
        {
            [StringDBMap("", "")]
            ALL,
            [StringDBMap("Sold", "ACT")]
            SOLD,
            [StringDBMap("Refund", "REF")]
            REFUND,
            [StringDBMap("Void", "VO")]
            VOID
        };

        public enum sortField_enum
        {
            [StringDBMap("Date", "DATE")]
            DATE,
            [StringDBMap("Sale Amount", "AMOUNT")]
            SALE,
            [StringDBMap("Cost", "COST")]
            COST,
            [StringDBMap("Current Status", "STATUS")]
            STATUS,
            [StringDBMap("User ID", "CREATEDBY")]
            USER
        };

        public sortField_enum sortBy;
        
        public bool byDate = false;
        public string startDate, endDate;
        public int lowMSR = -1;
        public int highMSR = -1;
        public double lowSaleAmount = -1;
        public double highSaleAmount = -1;
        public double lowCostAmount = -1;
        public double highCostAmount = -1;
        public string layawayOriginated;
        public searchStatus_enum status;
        public string userID;
        public string includeVoids;

        public DataSet getData()
        {
            return getDataSet("PAWN_INQUIRY", "get_retail_data",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_storenumber", GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                    new OracleProcParam("p_startDate", startDate),
                                    new OracleProcParam("p_endDate", endDate),
                                    new OracleProcParam("p_low_msr", (lowMSR >= 0) ? lowMSR.ToString() : ""),
                                    new OracleProcParam("p_high_msr", (highMSR >= 0) ? highMSR.ToString() : ""),
                                    new OracleProcParam("p_low_amount", (lowSaleAmount >= 0) ? String.Format("{0:f}", lowSaleAmount) : ""),
                                    new OracleProcParam("p_high_amount", (highSaleAmount >= 0) ? String.Format("{0:f}", highSaleAmount) : ""),
                                    new OracleProcParam("p_low_cost", (lowCostAmount >= 0) ? String.Format("{0:f}", lowCostAmount) : ""),
                                    new OracleProcParam("p_high_cost", (highCostAmount >= 0) ? String.Format("{0:f}", highCostAmount) : ""),
                                    new OracleProcParam("p_org_from_layaway", layawayOriginated),
                                    new OracleProcParam("p_byDate", (byDate) ? "Y" : "N"),
                                    new OracleProcParam("p_Status", StringDBMap_Enum<searchStatus_enum>.toDBValue(status)),
                                    new OracleProcParam("p_userID", userID),
                                    new OracleProcParam("p_includeVoids", includeVoids),
                                    new OracleProcParam("p_sort", StringDBMap_Enum<sortField_enum>.toDBValue(sortBy)),
                                    new OracleProcParam("p_sortDir", StringDBMap_Enum<sortDir_enum>.toDBValue(sortDir))
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_sale_info", "RETAIL_INFO"}
                                });
        }

        public static DataSet getDetail(int ticketNumber)
        {
            return getDataSet("PAWN_INQUIRY", "get_retail_detail",
                            new List<OracleProcParam>
                            {
                                new OracleProcParam("p_storenumber", CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber),
                                new OracleProcParam("p_ticket_number", ticketNumber)
                            },
                            new Dictionary<string, string>
                            {
                                {"o_customer_info", "CUSTOMER_INFO"},
                                {"o_mdse_info", "MDSE_INFO"},
                                {"o_history_info", "HISTORY_INFO"},
                                {"o_tender_info", "TENDER_INFO"}
                            });
        }

        public override string ToString()
        {
            string retval = "";

            if (byDate)
            {
                retval += string.Format("Date: {0} to {1} \n", startDate, endDate);
            }
            else if (lowMSR >= 0 || highMSR >= 0)
            {
                retval += "Ticket:";

                if (lowMSR >= 0 && highMSR >= 0)
                    retval += string.Format("{0} to {1} \n", lowMSR, highMSR);

                else if (lowMSR >= 0)
                    retval += string.Format("{0}\n", lowMSR);

                else
                    retval += string.Format("{0}\n", highMSR);
            }

            if (lowSaleAmount >= 0 || highSaleAmount >= 0)
            {
                retval += "Sale Amount:";

                if (lowSaleAmount >= 0 && highSaleAmount >= 0)
                    retval += string.Format("{0} to {1} \n", lowSaleAmount, highSaleAmount);

                else if (lowSaleAmount >= 0)
                    retval += string.Format("{0}\n", lowSaleAmount);

                else
                    retval += string.Format("{0}\n", highSaleAmount);
            }

            if (lowCostAmount >= 0 || highCostAmount >= 0)
            {
                retval += "Cost Amount:";

                if (lowCostAmount >= 0 && highCostAmount >= 0)
                    retval += string.Format("{0} to {1} \n", lowCostAmount, highCostAmount);

                else if (lowCostAmount >= 0)
                    retval += string.Format("{0}\n", lowCostAmount);

                else
                    retval += string.Format("{0}\n", highCostAmount);
            }

            if (!string.IsNullOrEmpty(layawayOriginated))
                retval += string.Format("Originated from Layaway: {0}\n", layawayOriginated);

            if (status != searchStatus_enum.ALL)
                retval += string.Format("Status: {0}\n", StringDBMap_Enum<searchStatus_enum>.displayValue(status));

            if (!string.IsNullOrEmpty(userID))
                retval += string.Format("User ID: {0}\n", userID);

            if (!string.IsNullOrEmpty(includeVoids))
                retval += string.Format("Include Voids: {0}\n", includeVoids);

            //-------
            retval += string.Format("Sorted By: {0},  {1}\n",
                        StringDBMap_Enum<sortField_enum>.displayValue(sortBy),
                        StringDBMap_Enum<sortDir_enum>.displayValue(sortDir));

            return retval;
        }
    }
}
