using Common.Controllers.Database.Oracle;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Collections.Generic;
using System.Data;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Inquiry.CashTransferInquiry
{
    public class CashTransferInquiry : Inquiry
    {
        public enum transferType_enum
        {
            [StringDBMap("Internal", "INTERNAL")]
            INTERNAL,
            [StringDBMap("Bank", "BANK")]
            BANK,
            [StringDBMap("Shop to Shop", "SHOP_TO_SHOP")]
            SHOP_TO_SHOP,
            [StringDBMap("All Safe Transfers", "SAFE")]
            SAFE,
        };

        public bool byDate = false;
        public string startDate, endDate;
        public int lowTransferNumber = -1;
        public int highTransferNumber = -1;
        public transferType_enum transferType;
        public double lowAmount = -1;
        public double highAmount = -1;
        public string status;
        public string userID;
        public string sourcePrimary;
        public string sourceSecondary;
        public string destinationPrimary;
        public string destinationSecondary;

        public enum sortField_enum
        {
            [StringDBMap("Transfer Date", "transferdate")]
            DATE,
            [StringDBMap("Transfer Amount", "transferamount")]
            AMOUNT,
            [StringDBMap("Source", "SOURCE")]
            SOURCE,
            [StringDBMap("Destination", "DESTINATION")]
            DESTINATION,
            [StringDBMap("Status", "transferstatus")]
            STATUS,
            [StringDBMap("User ID", "userid")]
            USER,
            [StringDBMap("Transfer Number", "transfernumber")]
            TRANSFERNUMBER
        };
        public sortField_enum sortBy;

        public static DataSet getCashDrawersForStore(String storeId)
        {
            return getDataSet("PAWN_Inquiries", "get_cashdrawers_for_store",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_storenumber", storeId)
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_cash_drawers", "CASHDRAWER"}
                                });
        }

        public static DataSet getAllBanksStore(String storeId)
        {
            return getDataSet("PAWN_Inquiries", "get_all_banks_for_store",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_storenumber", storeId)
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_banks", "BANKS"}
                                });
        }

        public static DataSet getStoreToStoreCashXferDetails(String transferNumber)
        {
            return getDataSet("PAWN_Inquiries", "get_shoptoshop_cash_xfer__dtl",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_transfernumber", transferNumber)
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_xfer_details", "CASH_XFER_DETAILS"}
                                });
        }

        public static DataSet getBankCashXferDetails(String transferNumber)
        {
            return getDataSet("PAWN_Inquiries", "get_bank_cash_xfer__dtl",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_transfernumber", transferNumber)
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_xfer_details", "CASH_XFER_DETAILS"}
                                });
        }

        public static DataSet getInternalCashXferDetails(String transferNumber)
        {
            return getDataSet("PAWN_Inquiries", "get_internal_cash_xfer__dtl",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_transfernumber", transferNumber)
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_xfer_details", "CASH_XFER_DETAILS"}
                                });
        }

        public DataSet getData()
        {
            DataSet outputDataSet = getDataSet("PAWN_Inquiries", "get_cash_transfer_data",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_byDate", (byDate) ? "Y" : "N"),
                                    new OracleProcParam("p_startDate", startDate),
                                    new OracleProcParam("p_endDate", endDate),
                                    new OracleProcParam("p_lowTransfer", (lowTransferNumber >= 0) ? lowTransferNumber.ToString() : ""),
                                    new OracleProcParam("p_highTransfer", (highTransferNumber >= 0) ? highTransferNumber.ToString() : ""),
                                    new OracleProcParam("p_transferType", StringDBMap_Enum<transferType_enum>.toDBValue(transferType)),
                                    new OracleProcParam("p_lowLoanAmount", (lowAmount >= 0) ? String.Format("{0:f}", lowAmount) : ""),
                                    new OracleProcParam("p_highLoanAmount", (highAmount >= 0) ? String.Format("{0:f}", highAmount) : ""),
                                    new OracleProcParam("p_sourcePrimary", sourcePrimary),
                                    new OracleProcParam("p_sourceSecondary", sourceSecondary),
                                    new OracleProcParam("p_destinationPrimary", destinationPrimary),
                                    new OracleProcParam("p_destinationSecondary", destinationSecondary),
                                    new OracleProcParam("p_Status", status),
                                    new OracleProcParam("p_userID", userID),
                                    new OracleProcParam("p_storenumber", GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                    new OracleProcParam("p_sortBy", sortBy.ToString()),
                                    new OracleProcParam("p_sortDir", StringDBMap_Enum<sortDir_enum>.toDBValue(sortDir))
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_bank_transfer_list", "BANK_TRANSFERS"},
                                    {"o_store_transfer_list", "STORE_TRANSFERS"},
                                    {"o_store_to_store_transferlist", "SHOP_TO_SHOP_TRANSFERS"}
                                });

            if (outputDataSet.IsNullOrEmpty())
            {
                return null;
            }

            return outputDataSet;
        }

        public override string ToString()
        {
            string retval = "";

            if (byDate)
            {
                // Transfer Date
                retval += string.Format("Date: {0} to {1} \n", startDate, endDate);
            }
            else
            {
                // Transfer Numbers
                if (lowTransferNumber >= 0 || highTransferNumber >= 0)
                {
                    if (lowTransferNumber >= 0 && highTransferNumber >= 0)
                    {
                        retval += string.Format("Transfer Number: {0} to {1} \n", lowTransferNumber, highTransferNumber);
                    }
                    else if (lowTransferNumber >= 0)
                    {
                        retval += string.Format("Transfer Number: {0}\n", lowTransferNumber);
                    }
                    else
                    {
                        retval += string.Format("Transfer Number: {0}\n", highTransferNumber);
                    }
                }

            }

            // Transfer Type
            retval += string.Format("Transfer Type: {0}\n", transferType);

            // Amount
            if (this.lowAmount >= 0 || this.highAmount >= 0)
            {
                if (lowAmount >= 0 && highAmount >= 0)
                {
                    retval += string.Format("Amount: {0:C} to {1:C} \n", lowAmount, highAmount);
                }
                else if (lowAmount >= 0)
                {
                    retval += string.Format("Amount: {0:C}\n", lowAmount);
                }
                else
                {
                    retval += string.Format("Amount: {0:C}\n", highAmount);
                }
            }

            // Sources
            if (this.sourcePrimary.Length != 0 && this.sourceSecondary.Length != 0)
            {
                retval += string.Format("Source: {0}, {1}\n", sourcePrimary, sourceSecondary);
            }
            else if (this.sourcePrimary.Length != 0)
            {
                retval += string.Format("Source: {0}\n", sourcePrimary);
            }

            // Destinations
            if (this.destinationPrimary.Length != 0 && this.destinationSecondary.Length != 0)
            {
                retval += string.Format("Destination: {0}, {1}\n", destinationPrimary, destinationSecondary);
            }
            else if (this.destinationPrimary.Length != 0)
            {
                retval += string.Format("Destination: {0}\n", destinationPrimary);
            }

            // Status
            if (this.status.Length != 0)
            {
                retval += string.Format("Current Status: {0}\n", this.status);
            }

            // User Id
            if (this.userID.Length != 0)
            {
                retval += string.Format("User Id: {0}\n", this.userID);
            }

            // Sort By, Asc, Dsc
            retval += string.Format("\nSorted By: {0},  {1}\n",
                StringDBMap_Enum<sortField_enum>.displayValue(sortBy),
                StringDBMap_Enum<sortDir_enum>.displayValue(sortDir));

            return retval;
        }
    }
}
