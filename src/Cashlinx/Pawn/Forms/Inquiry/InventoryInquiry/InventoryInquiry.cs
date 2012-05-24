using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Inquiry;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility.Shared;
using Oracle.DataAccess.Client;

namespace Pawn.Forms.Inquiry.InventoryInquiry
{
    public enum sortField_enum
    {
        [StringDBMap("ICN", "ICN")]
        ICN,
        [StringDBMap("Category", "CAT_CODE")]
        CATEGORY,
        [StringDBMap("Status", "STATUS_CD")]
        STATUS,
        [StringDBMap("Status Date", "STATUS_TIME")]
        STATUS_DATE,
        [StringDBMap("Retail Price", "RETAIL_PRICE")]
        RETAIL_PRICE,
        [StringDBMap("Item Cost", "ITEM_AMT")]
        ITEM_COST,
        [StringDBMap("Aisle", "LOC_AISLE")]
        LOC_AISLE,
        [StringDBMap("Shelf", "LOC_SHELF")]
        LOC_SHELF,
        [StringDBMap("Other", "LOC")]
        LOC_OTHER
    };

    public enum statusField_enum
    {
        [StringDBMap("", "")]
        ALL,
        [StringDBMap("Audited Temp", "LREC")]
        LREC,
        [StringDBMap("Forfieted", "FORF")]
        FORF,
        [StringDBMap("In Pawn", "IP")]
        IP,
        [StringDBMap("Layaway", "LAY")]
        LAY,
        [StringDBMap("PFC", "PFC")]
        PFC,
        [StringDBMap("PFI", "PFI")]
        PFI,
        [StringDBMap("PFI Merged", "PFM")]
        PFM,
        [StringDBMap("Picked Up", "PU")]
        PU,
        [StringDBMap("Police Seized", "PS")]
        PS,
        [StringDBMap("Purchased", "PUR")]
        PUR,
        [StringDBMap("Purchase Returned", "RET")]
        RET,
        [StringDBMap("Refunded", "REF")]
        REF,
        [StringDBMap("Released To Claimant", "RTC")]
        RTC,
        [StringDBMap("Sold", "SOLD")]
        SOLD,
        [StringDBMap("Temp", "TEMP")]
        TEMP,
        [StringDBMap("Transferred", "TO")]
        TO,
        [StringDBMap("Voided", "VO")]
        VO

    };
    
    public class InventoryInquiries : Inquiry
    {
        public sortField_enum sortBy;
        public MDSE Mdse;
        public int RfbNr;
        public int GunNumber;
        public statusField_enum status; //statusField_enum
        public DateTime lowStatusDate, highStatusDate;
        public int age;
        public decimal lowRetail = -1, highRetail = -1;
        public decimal lowCost = -1, highCost = -1, fieldCost = -1;
        public string descr;
        public string manufacturer;
        public string model;
        public string serialNr;
        public string location;
        public string shelf;
        public string aisle;
        public bool icnYear_IsNull = true;
        public bool icnDocType_IsNull = true;
        public List<string> cat_code, cat_names;

        private int _wrapWidth = 120;
        
        
        public static DataSet getCategories()
        {
            return getDataSet("PAWN_Inquiries", "get_category_data",
                                null,
                                new Dictionary<string, string>
                                {
                                    {"o_category", "CATEGORIES"}
                                });
        }

        public static DataSet getItemCostRevision(Icn icn)
        {
            return getDataSet("PAWN_Inquiries", "get_item_cost_revision",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_icn", icn.GetFullIcn())
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_revisions", "REVISIONS"}
                                });
        }


        public static DataSet getRetailPriceRevision(Icn icn)
        {
            return getDataSet("PAWN_Inquiries", "get_retail_price_revision",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_icn", icn.GetFullIcn())
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_revisions", "REVISIONS"}
                                });
        }

        public static DataSet getItemStatusRevision(Icn icn)
        {
            return getDataSet("PAWN_Inquiries", "get_item_STATUS_revision",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_icn", icn.GetFullIcn())
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_revisions", "REVISIONS"}
                                });
        }

        public static DataSet getItemDescRevision(Icn icn)
        {
            return getDataSet("PAWN_Inquiries", "get_item_desc_revision",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_icn", icn.GetFullIcn())
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_revisions", "REVISIONS"}
                                });
        }

        public static DataSet getVendorDataForIcn(Icn icn)
        {
            return getDataSet("PAWN_Inquiries", "get_Vendor_for_icn",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_icn", icn.GetFullIcn())
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_revisions", "REVISIONS"}
                                });
        }

        public static DataSet getMinPawnData(Icn icn)
        {
            return getDataSet("PAWN_Inquiries", "get_min_pawn_data",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_storenumber", (icn.ShopNumber == -1) ? "" : icn.ShopNumber.ToString()),
                                    new OracleProcParam("p_ticket_number", (icn.DocumentNumber == -1) ? "" : icn.DocumentNumber.ToString()),

                                    new OracleProcParam("o_date_made", OracleDbType.Date, DBNull.Value, ParameterDirection.Output, 1),
                                    new OracleProcParam("o_loan_amount", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1),
                                    new OracleProcParam("o_loan_status", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 32),
                                },
                                null);
        }

        public static DataSet getMinPurchaseData(Icn icn)
        {
            return getDataSet("PAWN_Inquiries", "get_min_purchase_data",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_storenumber", (icn.ShopNumber == -1) ? "" : icn.ShopNumber.ToString()),
                                    new OracleProcParam("p_ticket_number", (icn.DocumentNumber == -1) ? "" : icn.DocumentNumber.ToString()),

                                    new OracleProcParam("o_date_made", OracleDbType.Date, DBNull.Value, ParameterDirection.Output, 1),
                                    new OracleProcParam("o_purchase_amount", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1),
                                    new OracleProcParam("o_purchase_status", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 32),
                                },
                                null);
        }

        public DataSet getData()
        {
            return getDataSet("PAWN_Inquiries", "get_inventory_data",
                                new List<OracleProcParam>
                                {
                                    new OracleProcParam("p_storenumber", GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber),
                                    new OracleProcParam("p_icn_store", (Mdse.Icn.ShopNumber == -1)? "" : Mdse.Icn.ShopNumber.ToString()),
                                    new OracleProcParam("p_icn_year", icnYear_IsNull ? "" : Mdse.Icn.LastDigitOfYear.ToString()),
                                    new OracleProcParam("p_icn_doc", (Mdse.Icn.DocumentNumber == -1) ? "" : Mdse.Icn.DocumentNumber.ToString()),
                                    new OracleProcParam("p_icn_doc_type", (icnDocType_IsNull) ? "" : ((int) Mdse.Icn.DocumentType).ToString()),
                                    new OracleProcParam("p_icn_item", (Mdse.Icn.ItemNumber == -1) ? "" : Mdse.Icn.ItemNumber.ToString()),
                                    new OracleProcParam("p_icn_sub_item", (Mdse.Icn.SubItemNumber == -1) ? "" : Mdse.Icn.SubItemNumber.ToString()),

                                    new OracleProcParam("p_rfb_number", (RfbNr == -1) ? "" : RfbNr.ToString()),
                                    new OracleProcParam("p_gun_number", (GunNumber == -1) ? "" : GunNumber.ToString()),
                                    addPCatCodes(new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTINT, "p_categories", cat_code.Count)),

                                    new OracleProcParam("p_status_cd", StringDBMap_Enum<statusField_enum>.toDBValue(status)),
                                    new OracleProcParam("p_start_status_dt", (lowStatusDate == DateTime.MinValue ) ? "" : lowStatusDate.ToShortDateString()),
                                    new OracleProcParam("p_end_status_dt", (highStatusDate == DateTime.MaxValue) ? "" : highStatusDate.ToShortDateString()),
                                    new OracleProcParam("p_age", (age == -1) ? "" : age.ToString()),

                                    new OracleProcParam("p_lowRtlPrc", (lowRetail == -1) ? "" : lowRetail.ToString()),
                                    new OracleProcParam("p_highRtlPrc", (highRetail == -1) ? "" : highRetail.ToString()),
                                    new OracleProcParam("p_lowItmAmt", (lowCost == -1) ? "" : lowCost.ToString()),
                                    new OracleProcParam("p_highItmAmt", (highCost == -1) ? "" : highCost.ToString()),
                                    new OracleProcParam("p_itemCost", (fieldCost == -1) ? "" : fieldCost.ToString()),
                                    new OracleProcParam("p_desc", descr),
                                    new OracleProcParam("p_manuf", manufacturer),

                                    new OracleProcParam("p_model", model),
                                    new OracleProcParam("p_sn", serialNr),
                                    new OracleProcParam("p_loc", location),
                                    new OracleProcParam("p_aisle", aisle),
                                    new OracleProcParam("p_shelf", shelf),

                                    new OracleProcParam("p_sort", StringDBMap_Enum<sortField_enum>.toDBValue(sortBy)),
                                    new OracleProcParam("p_sortDir", StringDBMap_Enum<sortDir_enum>.toDBValue(sortDir)),
                                },
                                new Dictionary<string, string>
                                {
                                    {"o_mdse", "MDSE"}
                                });
        }

        private OracleProcParam addPCatCodes(OracleProcParam procParam)
        {
            procParam.AddValues(cat_code.Select(x => int.Parse(x) as object));
            return procParam;
        }

        public string ToString (int width)
        {
            _wrapWidth = width;

            string retval =  ToString();

            _wrapWidth = 120;

            return retval;
        }

        public override string ToString()
        {

            string retval = string.Empty;
            if (Mdse.Icn.ShopNumber != -1 || !icnYear_IsNull || Mdse.Icn.DocumentNumber != -1 ||
                !icnDocType_IsNull || Mdse.Icn.ItemNumber != -1 || Mdse.Icn.SubItemNumber != -1)
            {
                retval += string.Format("ICN: {0} {1} {2} {3} {4} {5}\n",
                    (Mdse.Icn.ShopNumber == -1) ? "*" : Mdse.Icn.ShopNumber.ToString(),
                    (icnYear_IsNull) ? "*" : Mdse.Icn.LastDigitOfYear.ToString(),
                    (Mdse.Icn.DocumentNumber == -1) ? "*" : Mdse.Icn.DocumentNumber.ToString(),
                    (icnDocType_IsNull) ? "*" : ((int)Mdse.Icn.DocumentType).ToString(),
                    (Mdse.Icn.ItemNumber == -1) ? "*" : Mdse.Icn.ItemNumber.ToString(),
                    (Mdse.Icn.SubItemNumber == -1) ? "*" : Mdse.Icn.SubItemNumber.ToString());
            }

            if (RfbNr > 0)
                retval += string.Format("RFB: {0}\n", RfbNr);

            if (GunNumber > 0)
                retval += string.Format("Gun Number: {0}\n", GunNumber);

            if (cat_code.Count > 0 )
            {
                if (! (cat_code.Count == 1 && cat_code[0] == "-1"))
                {
                    retval += "Categories:  ";
                    bool first = true;
                    int catLength = 14;

                    foreach (string s in cat_names)
                    {
                        if (!first)
                        {
                            retval += ", ";
                        }
                        else
                        {
                            first = false;
                        }

                        if (catLength > _wrapWidth)
                        {
                            retval += "\n                   ";
                            catLength = 14;
                        }

                        retval += s;
                        catLength += s.Length + 2;
                    }

                    retval += "\n";
                }
            }


            if (status != statusField_enum.ALL)
                retval += string.Format("Status:   {0}\n", StringDBMap_Enum<statusField_enum>.displayValue(status));

            if (highStatusDate != DateTime.MaxValue && lowStatusDate != DateTime.MinValue)
                retval += string.Format("Status updated between {0:d} and {1:d}\n", lowStatusDate, highStatusDate);

            if (age >= 0)
                retval += string.Format("Inventory Age: {0}\n", age);

            if (lowRetail > 0 || highRetail > 0)
            {

                if (highRetail > 0)
                {
                    retval += string.Format("Retail Price between {0:c} and {1:c}\n",
                        (lowRetail > 0) ? lowRetail : 0, highRetail);
                }
                else if (lowRetail > 0)
                {
                    retval += string.Format("Retail Price >= {0:c}\n", lowRetail);
                }
            }

            if (lowCost > 0 || highCost > 0)
            {

                if (highCost > 0)
                {
                    retval += string.Format("Item Price between {0:c} and {1:c}\n",
                        (lowCost > 0) ? lowCost : 0, highCost);
                }
                else if (lowCost > 0)
                {
                    retval += string.Format("Item Price >= {0:c}\n", lowCost);
                }
            }

            if (descr.Length > 0)
                retval += string.Format("Description is like *{0}*\n", descr);

            if (manufacturer.Length > 0)
                retval += string.Format("Manufacturer:  {0}\n", manufacturer);

            if (model.Length > 0)
                retval += string.Format("Model:  {0}\n", model);

            if (serialNr.Length > 0)
                retval += string.Format("Serial Nr:  {0}\n", serialNr);

            if (aisle.Length > 0 || shelf.Length > 0 || location.Length > 0)
                retval += string.Format("Location (aisle / shelf / other):  {0} / {1} / {2}\n", aisle, shelf, location);

            retval += string.Format("\nSorted By: {0} {1}", 
                    StringDBMap_Enum<sortField_enum>.displayValue(sortBy),
                    StringDBMap_Enum<sortDir_enum>.displayValue(sortDir));

            return retval;
        }

        public override string sortByField()
        {
            return StringDBMap_Enum<sortField_enum>.toDBValue(sortBy);
        }

        public override string sortByName()
        {
            return StringDBMap_Enum<sortField_enum>.displayValue(sortBy);
        }
    }
}
