/*****************************************************************************************************
 * Namespace:       CashlinxDesktop.DesktopProcedures
 * Class:           MerchandiseProcedures
 * 
 * Description      The class retrieves Cat5 Manufacturer data.
 * 
 * History
 * David D Wise, 5-10-2009, Initial Development
 * 
 *  PWNU00000602 & PWNU00000632 4/9/2010 SMurphy problem when proknow answer id is not in answer table
 * SR 4/5/2010 Changed the status reason being passed in insert_mdse to ADDD from ADDED
 * SR 6/11/2010 Changed insertmerchandise and updatemerchandise to fix the divide by zero issue
 * 
 * **********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Security;
using System.Data;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public static class MerchandiseProcedures
    {
        public static readonly string GENERAL = "General";
        public static readonly string JEWELRY = "Jewelry";
        public static readonly string MANUFACTURERS = "Manufacturers";
        public static readonly string CAT_GENERAL = "Cat" + GENERAL;
        public static readonly string CAT_MASK_GENERAL = "CatMask" + GENERAL;
        public static readonly string CAT_MASK_ATTR_GENERAL = "CatMaskAttr" + GENERAL;
        public static readonly string CAT_MASK_ATTR_AVA_GENERAL = "CatMaskAttrAva" + GENERAL;
        public static readonly string CAT_MASK_ATTR_AVA_ANSWER_GENERAL = "CatMaskAttrAvaAnswer" + GENERAL;
        public static readonly string CAT_JEWELRY = "Cat" + JEWELRY;
        public static readonly string CAT_MASK_JEWELRY = "CatMask" + JEWELRY;
        public static readonly string CAT_MASK_ATTR_JEWELRY = "CatMaskAttr" + JEWELRY;
        public static readonly string CAT_MASK_ATTR_AVA_JEWELRY = "CatMaskAttrAva" + JEWELRY;
        public static readonly string CAT_MASK_ATTR_AVA_ANSWER_JEWELRY = "CatMaskAttrAvaAnswer" + JEWELRY;
        private static readonly string GUN_DATA = "gun_data";
        private static readonly string GUN_MDSE_DATA = "gun_mdsedata";
        private static readonly string GUN_MDSE_OTHERDSC_DATA = "gun_otherdsc_data";
        private static readonly string GUN_MDHIST_DATA = "gun_mdsehist_data";


        public static bool CheckForHolds(
            string storeNumber,
            int loanNumber,
            string refType,
            out List<HoldData> pawnHolds,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;
            pawnHolds = new List<HoldData>();

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "CheckForHolds";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("CheckForHolds", new ApplicationException("Cannot execute the Check_For_Holds stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_loan_number", loanNumber));
            inParams.Add(new OracleProcParam("p_ref_store", storeNumber));
            if (refType == ProductType.BUY.ToString())
                inParams.Add(new OracleProcParam("p_ref_type", "2"));
            else
                inParams.Add(new OracleProcParam("p_ref_type", "1"));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            refCursors.Add(new PairType<string, string>("o_pawn_hold", "pawn_hold"));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MANAGE_HOLDS", "Check_For_Holds",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "CheckForHoldsFailed";
                errorText = "Invocation of Check_For_Holds stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking Check_For_Holds stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            else
            {
                errorCode = "0";
                errorText = string.Empty;

                //Get the Data
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    foreach (DataRow myRow in outputDataSet.Tables[0].Rows)
                    {
                        HoldData holdData = new HoldData()
                        {
                            HoldTypeId = Utilities.GetIntegerValue(myRow["hold_type_id"], 0),
                            HoldDesc = Utilities.GetStringValue(myRow["hold_desc"], ""),
                            HoldType = Utilities.GetStringValue(myRow["hold_type"], "")
                        };
                        pawnHolds.Add(holdData);
                    }
                }
                return (true);
            }
        }

        public static bool IsAnswerIDValid(
            Int32 answerID,
            out string errorCode,
            out string errorText)
        {
            //PWNU00000602 & PWNU00000632 4/9/2010 SMurphy problem when proknow answer id is not in answer table
            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            DataSet outputDataSet = null;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            inParams.Add(new OracleProcParam("p_ans_id", answerID));
            inParams.Add(new OracleProcParam("o_ans_id", DataTypeConstants.PawnDataType.WHOLENUMBER, answerID, ParameterDirection.Output, 1, true));

            //Create output data set names
            bool retVal = true;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "get_answer_id",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);

                if (retVal)
                {
                    DataRow[] row = outputDataSet.Tables["OUTPUT"].Select();

                    if (row[0].ItemArray[1].ToString() == "null")
                    {
                        errorCode = "1";
                        errorText = "Answer ID: " + answerID.ToString() + " not found!";
                        return (false);
                    }
                    else
                    {
                        errorCode = "0";
                        errorText = string.Empty;
                        return (true);
                    }
                }
                else
                {
                    errorCode = "1";
                    errorText = "Answer ID: " + answerID.ToString() + " not found!";
                    return (false);
                }
            }
            catch (OracleException oEx)
            {
                errorCode = oEx.ErrorCode.ToString();
                errorText = "Error verifying Answer ID! " + dA.ErrorDescription;
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking get_answer_id stored proc", oEx);
                return (false);
            }
        }
        public static bool DeleteOtherDesc(
            string storeNumber,
            int icnStore,
            int icnYear,
            int icnDoc,
            string icnDocType,
            int icnItem,

            out string errorCode,
            out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "DeleteOtherDesc";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("DeleteStones",
                                                            new ApplicationException(
                                                                "Cannot execute the DeleteOtherDesc update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_icn_store", icnStore));
            inParams.Add(new OracleProcParam("p_icn_year", icnYear));
            inParams.Add(new OracleProcParam("p_icn_doc", icnDoc));
            inParams.Add(new OracleProcParam("p_icn_doc_type", icnDocType));
            inParams.Add(new OracleProcParam("p_icn_item", icnItem));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "delete_otherdsc",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "DeleteOtherDescFailed";
                errorText = "Invocation of DeleteOtherDesc stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking DeleteOtherDesc stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }



        public static bool DeleteStones(
            string storeNumber,
            int icnStore,
            int icnYear,
            int icnDoc,
            string icnDocType,
            int icnItem,

            out string errorCode,
            out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "DeleteStones";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("DeleteStones",
                                                            new ApplicationException(
                                                                "Cannot execute the DeleteStones update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_icn_store", icnStore));
            inParams.Add(new OracleProcParam("p_icn_year", icnYear));
            inParams.Add(new OracleProcParam("p_icn_doc", icnDoc));
            inParams.Add(new OracleProcParam("p_icn_doc_type", icnDocType));
            inParams.Add(new OracleProcParam("p_icn_item", icnItem));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "delete_stones",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "DeleteStonesFailed";
                errorText = "Invocation of DeleteStones stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking DeleteStones stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }

        public static bool ExecuteGetCat5Info(
            Int32 category,
            out DataSet outputDataSet,
            out bool hasJewelryData,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            hasJewelryData = false;
            errorCode = string.Empty;
            errorText = string.Empty;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetCat5InfoFailed";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetCat5InfoFailed", new ApplicationException("Cannot execute the category 5 retrieval stored procedure"));
                outputDataSet = null;
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add cat pointer
            inParams.Add(new OracleProcParam("p_cat_ptr", category));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("c_glist", CAT_GENERAL));
            refCursors.Add(new PairType<string, string>("cm_glist", CAT_MASK_GENERAL));
            refCursors.Add(new PairType<string, string>("cma_glist", CAT_MASK_ATTR_GENERAL));
            refCursors.Add(new PairType<string, string>("cmaa_glist", CAT_MASK_ATTR_AVA_GENERAL));
            refCursors.Add(new PairType<string, string>("cmaaa_glist", CAT_MASK_ATTR_AVA_ANSWER_GENERAL));

            //Add jewelry ref cursors
            refCursors.Add(new PairType<string, string>("c_jlist", CAT_JEWELRY));
            refCursors.Add(new PairType<string, string>("cm_jlist", CAT_MASK_JEWELRY));
            refCursors.Add(new PairType<string, string>("cma_jlist", CAT_MASK_ATTR_JEWELRY));
            refCursors.Add(new PairType<string, string>("cmaa_jlist", CAT_MASK_ATTR_AVA_JEWELRY));
            refCursors.Add(new PairType<string, string>("cmaaa_jlist", CAT_MASK_ATTR_AVA_ANSWER_JEWELRY));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_mdse_procs", "get_cat5_information",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "GetCat5InfoFailed";
                errorText = "Invocation of GetCat5Info stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking get_cat5_information stored proc", oEx);
                outputDataSet = null;
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }

            if (outputDataSet != null)
            {
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    //Get jewelry data if it exists
                    if (outputDataSet.Tables.Contains(CAT_JEWELRY))
                    {
                        hasJewelryData = true;
                    }

                    if (!hasJewelryData)
                    {
                        DataTable generalTable = outputDataSet.Tables[CAT_GENERAL];
                        if (generalTable != null)
                            hasJewelryData = generalTable.Rows[0]["md_type"].ToString() == "J" ? true : false;
                    }

                    errorCode = "0";
                    errorText = string.Empty;
                    return (true);
                }
            }

            errorCode = "GetCat5InfoFailed";
            errorText = "Operation failed";
            return (false);
        }

        /// <summary>
        /// Returns a single DataSet of Manufacturers from the Database
        /// </summary>
        /// <param name="manufacturerData"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns>Returns false if there are errors returned from the DB</returns>
        public static bool ExecuteGetManufacturers(
            int attributeID,
            out DataTable manufacturerData,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            manufacturerData = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetManufacturersFailed";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetManufacturerFailed",
                    new ApplicationException("Cannot execute the manufacturer retrieval stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_attr_id", attributeID));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add manufacturer ref cursor
            refCursors.Add(new PairType<string, string>("c_mlist", MANUFACTURERS));

            //Create output data set names
            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_mdse_procs", "get_manufacturers",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "GetManufacturersFailed";
                errorText = "Invocation of Get Manufacturers stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking get_manufacturers stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }

            if (outputDataSet != null)
            {
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    //Get manufacturer data if it exists
                    if (outputDataSet.Tables.Contains(MANUFACTURERS))
                    {
                        manufacturerData = outputDataSet.Tables[MANUFACTURERS];
                    }
                    return (true);
                }
            }

            errorCode = "GetManufacturersFailed";
            errorText = "Operation failed";
            return (false);
        }

        public static bool InsertMerchandise(DesktopSession desktopSession,
                Item pawnItem,
                int icnSubItem,
                DateTime originationDate,
                decimal originalItemAmount,
                string customerNumber,
                out string errorCode,
                out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "InsertMerchandise";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("InsertMerchandise",
                                                            new ApplicationException(
                                                                "Cannot execute the InsertMerchandise update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            var inParams = new List<OracleProcParam>();

            string sPfiAssignmentType = Commons.GetPfiAssignmentAbbreviation(pawnItem.PfiAssignmentType);

            string sStatusReason = "";
            switch (pawnItem.ItemReason)
            {
                case ItemReason.ADDD:
                    sStatusReason = "ADDD";
                    break;
                case ItemReason.COFFBRKN:
                    sStatusReason = "OFF-BRKN";
                    break;
                case ItemReason.CACC:
                    sStatusReason = "CACC";
                    break;
                case ItemReason.HPFI:
                    sStatusReason = "OFF-HPFI";
                    break;
                case ItemReason.MERGED:
                    sStatusReason = "MERGED";
                    break;
                case ItemReason.NOMD:
                    sStatusReason = "OFF-NOMD";
                    break;
                case ItemReason.COFFNXT:
                    sStatusReason = "OFF-NXT";
                    break;
                case ItemReason.COFFSTLN:
                    sStatusReason = "OFF-STLN";
                    break;
                case ItemReason.COFFSTRU:
                    sStatusReason = "OFF-STRU";
                    break;
            }

            inParams.Add(new OracleProcParam("p_storenumber", pawnItem.mStore.ToString().PadLeft(5, '0')));
            inParams.Add(new OracleProcParam("p_icn_store", pawnItem.mStore));
            inParams.Add(new OracleProcParam("p_icn_year", pawnItem.mYear));
            inParams.Add(new OracleProcParam("p_icn_doc", pawnItem.mDocNumber));
            inParams.Add(new OracleProcParam("p_icn_doc_type", pawnItem.mDocType));
            inParams.Add(new OracleProcParam("p_icn_item", pawnItem.mItemOrder));
            inParams.Add(new OracleProcParam("p_icn_sub_item", icnSubItem));
            inParams.Add(new OracleProcParam("p_cat_code", pawnItem.CategoryCode));
            inParams.Add(new OracleProcParam("p_customernumber", customerNumber));
            inParams.Add(new OracleProcParam("p_md_date", originationDate));
            inParams.Add(new OracleProcParam("p_amount", originalItemAmount));
            inParams.Add(new OracleProcParam("p_pfi_amount", icnSubItem > 0 ? 0 : pawnItem.ItemAmount));
            inParams.Add(new OracleProcParam("p_loc_aisle", ""));
            inParams.Add(new OracleProcParam("p_loc", ""));
            inParams.Add(new OracleProcParam("p_loc_shelf", ""));
            inParams.Add(new OracleProcParam("p_location", ""));
            inParams.Add(new OracleProcParam("p_gun_number", pawnItem.GunNumber));
            inParams.Add(new OracleProcParam("p_manufacturer", pawnItem.QuickInformation.Manufacturer));
            inParams.Add(new OracleProcParam("p_model", pawnItem.QuickInformation.Model));
            inParams.Add(new OracleProcParam("p_serial_number", pawnItem.QuickInformation.SerialNumber));
            inParams.Add(new OracleProcParam("p_weight", pawnItem.QuickInformation.Weight));
            inParams.Add(new OracleProcParam("p_quantity",
                                                             pawnItem.QuickInformation.Quantity > 0
                                                                 ? pawnItem.QuickInformation.Quantity
                                                                 : 1));
            Int64[] masks = !pawnItem.IsJewelry || icnSubItem < 1 ? GetMasks(pawnItem.Attributes) : GetMasks(pawnItem.Jewelry[icnSubItem - 1].ItemAttributeList);
            OracleProcParam maskParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTINT, "p_masks", masks.Length);
            for (int i = 0; i < masks.Length; ++i)
            {
                maskParam.AddValue(masks[i]);
            }
            inParams.Add(maskParam);

            inParams.Add(new OracleProcParam("p_fill1", "Y"));
            inParams.Add(new OracleProcParam("p_fill2", Utilities.GetStringValue(pawnItem.PfiTags).PadLeft(10, '0')));
            inParams.Add(new OracleProcParam("p_md_desc", icnSubItem <= 0 ? pawnItem.TicketDescription : pawnItem.Jewelry[icnSubItem - 1].TicketDescription != null ? pawnItem.Jewelry[icnSubItem - 1].TicketDescription.Trim() : string.Empty));
            inParams.Add(new OracleProcParam("p_release_date", null));
            inParams.Add(new OracleProcParam("p_disp_date", null));
            inParams.Add(new OracleProcParam("p_disp_type", pawnItem.mDocType == "2" ? "PURCHASE" : "LOAN"));
            inParams.Add(new OracleProcParam("p_disp_doc", 0));
            inParams.Add(new OracleProcParam("p_retail_price", pawnItem.RetailPrice));
            inParams.Add(new OracleProcParam("p_org_disp_date", null));
            inParams.Add(new OracleProcParam("p_org_disp_type", pawnItem.mDocType == "2" ? "PURCHASE" : "LOAN"));
            inParams.Add(new OracleProcParam("p_org_disp_doc", null));
            inParams.Add(new OracleProcParam("p_status_date", ShopDateTime.Instance.ShopDate));
            OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            inParams.Add(new OracleProcParam("p_status_time", ShopDateTime.Instance.ShopDate, tsType));
            inParams.Add(new OracleProcParam("p_pfi_date", ShopDateTime.Instance.ShopDate));
            inParams.Add(new OracleProcParam("p_purch_type", null));
            inParams.Add(new OracleProcParam("p_temp_status", ""));
            inParams.Add(new OracleProcParam("p_xicn", null));
            inParams.Add(new OracleProcParam("p_pol_jday", 0));
            inParams.Add(new OracleProcParam("p_item_amt",
                                                             pawnItem.ItemAmount /
                                                             (pawnItem.QuickInformation.Quantity <= 0
                                                                  ? 1
                                                                  : pawnItem.QuickInformation.Quantity)));
            inParams.Add(new OracleProcParam("p_cacc_lev", pawnItem.CaccLevel < 0 ? "" : pawnItem.CaccLevel.ToString()));
            inParams.Add(new OracleProcParam("p_jcase", null));
            inParams.Add(new OracleProcParam("p_tran_type", sPfiAssignmentType));
            inParams.Add(new OracleProcParam("p_rfb_no", pawnItem.RefurbNumber));
            inParams.Add(new OracleProcParam("p_rfb_store", pawnItem.mStore));
            inParams.Add(new OracleProcParam("p_final_dest", 0));
            inParams.Add(new OracleProcParam("p_num_bar", 0));
            inParams.Add(new OracleProcParam("p_state_flag", null));
            inParams.Add(new OracleProcParam("p_audit_flag", null));
            inParams.Add(new OracleProcParam("p_scrp_code", 0));
            inParams.Add(new OracleProcParam("p_scrp_cont", 0));
            inParams.Add(new OracleProcParam("p_scrp_ent_id", 0));
            inParams.Add(new OracleProcParam("p_warr_type", null));
            inParams.Add(new OracleProcParam("p_disp_item", 0));
            inParams.Add(new OracleProcParam("p_ldisp_print", null));
            inParams.Add(new OracleProcParam("p_ldisp_amount", 0));
            inParams.Add(new OracleProcParam("p_ldisp_paid", null));
            inParams.Add(new OracleProcParam("p_misc1", 0));
            inParams.Add(new OracleProcParam("p_misc2", null));
            inParams.Add(new OracleProcParam("p_misc3", null));
            inParams.Add(new OracleProcParam("p_misc4", null));
            inParams.Add(new OracleProcParam("p_misc5", null));
            inParams.Add(new OracleProcParam("p_pfi_cust", customerNumber));
            inParams.Add(new OracleProcParam("p_storage_fee", pawnItem.StorageFee));
            inParams.Add(new OracleProcParam("p_hold_Type", null));
            inParams.Add(new OracleProcParam("p_status_cd", pawnItem.ItemReason == ItemReason.CACC ? "PFC" : "PFI"));
            inParams.Add(new OracleProcParam("p_lastupdatedby", desktopSession.UserName));
            //        inParams.Add(new OracleProcParam("p_status_reason", pawnItem.ItemReason.ToString()));
            inParams.Add(new OracleProcParam("p_status_reason", sStatusReason));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "insert_mdse",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "InsertMerchandiseFailed";
                errorText = "Invocation of InsertMerchandise stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking InsertMerchandise stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }

        public static bool InsertMerchandiseArchive(
            string storeNumber,
            int icnStore,
            int icnYear,
            int icnDoc,
            string icnDocType,
            out string errorCode,
            out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "InsertMerchandiseArchive";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("InsertMerchandiseArchive",
                                                            new ApplicationException(
                                                                "Cannot execute the InsertMerchandiseArchive update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_icn_store", icnStore));
            inParams.Add(new OracleProcParam("p_icn_year", icnYear));
            inParams.Add(new OracleProcParam("p_icn_doc", icnDoc));
            inParams.Add(new OracleProcParam("p_icn_doc_type", icnDocType));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "insert_mdse_archive",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "InsertMerchandiseArchiveFailed";
                errorText = "Invocation of InsertMerchandiseArchive stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking InsertMerchandiseArchive stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }

        public static bool InsertMerchandiseRevision(DesktopSession desktopSession,
            string storeNumber,
            int icnYear,
            int icnDoc,
            string icnDocType,
            int icnItem,
            int icnSubItem,
            int icnStore,
            string refDesc,
            string refType,
            string merchandiseDesc,
            decimal merchandiseChange,
            string merchandiseType,
            string classCode,
            string acctNumber,
            string createdBy,
            out string errorCode,
            out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "InsertMerchandiseRevision";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("InsertMerchandiseRevision",
                                                            new ApplicationException(
                                                                "Cannot execute the InsertMerchandiseRevision update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_icn_year", icnYear));
            inParams.Add(new OracleProcParam("p_icn_doc", icnDoc));
            inParams.Add(new OracleProcParam("p_icn_doc_type", icnDocType));
            inParams.Add(new OracleProcParam("p_icn_item", icnItem));
            inParams.Add(new OracleProcParam("p_icn_sub_item", icnSubItem));
            inParams.Add(new OracleProcParam("p_icn_store", icnStore));
            inParams.Add(new OracleProcParam("p_mr_date", ShopDateTime.Instance.ShopDate));
            //OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            inParams.Add(new OracleProcParam("p_mr_time", ShopDateTime.Instance.ShopTransactionTime));
            inParams.Add(new OracleProcParam("p_mr_user", desktopSession.UserName));
            inParams.Add(new OracleProcParam("p_ref", refDesc));
            inParams.Add(new OracleProcParam("p_ref_type", refType));
            inParams.Add(new OracleProcParam("p_mr_desc", merchandiseDesc));
            inParams.Add(new OracleProcParam("p_mr_change", merchandiseChange));
            inParams.Add(new OracleProcParam("p_mr_type", merchandiseType));
            inParams.Add(new OracleProcParam("p_class_code", classCode));
            inParams.Add(new OracleProcParam("p_acct_num", acctNumber));
            inParams.Add(new OracleProcParam("p_created_by", desktopSession.UserName));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "insert_rev",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "InsertMerchandiseRevisionFailed";
                errorText = "Invocation of InsertMerchandiseRevision stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking InsertMerchandiseRevision stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }

        public static bool TubCalc(
            List<Tub_Param> lstParam,
            List<string> lstData,
            out string errorCode,
            out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "TubCalc";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("Tub_Calc",
                                                            new ApplicationException(
                                                                "Cannot execute the Tub_Calc stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            OracleProcParam maskParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_param_specifiers", lstParam.Count * 4);
            for (int i = 0; i < lstParam.Count; ++i)
            {
                maskParam.AddValue(lstParam[i].Name);
                maskParam.AddValue(lstParam[i].Type);
                maskParam.AddValue(lstParam[i].Size);
                maskParam.AddValue(lstParam[i].Position);
            }
            inParams.Add(maskParam);

            OracleProcParam maskData = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_data_specifiers", lstData.Count);
            for (int i = 0; i < lstData.Count; ++i)
            {
                maskData.AddValue(lstData[i]);
            }
            inParams.Add(maskData);

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_GEN_PROCS", "Tub_Calc",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "TubCalcFailed";
                errorText = "Invocation of Tub_Calc stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking Tub_Calc stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }

        public static bool UpdateCaccInfo(DesktopSession desktopSession,
            string storeNumber,
            int icnYear,
            int icnDoc,
            string icnDocType,
            int icnItem,
            int icnSubItem,
            int quantity,
            decimal pfiAmount,
            int catg,
            DateTime transactionDate,
            out string errorCode,
            out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateCaccInfo";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("UpdateCaccInfo",
                                                            new ApplicationException(
                                                                "Cannot execute the Update_Cacc_Info stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_icn_year", icnYear));
            inParams.Add(new OracleProcParam("p_icn_doc", icnDoc));
            inParams.Add(new OracleProcParam("p_icn_doc_type", icnDocType));
            inParams.Add(new OracleProcParam("p_icn_item", icnItem));
            inParams.Add(new OracleProcParam("p_icn_sub_item", icnSubItem));
            inParams.Add(new OracleProcParam("p_quantity", quantity));
            inParams.Add(new OracleProcParam("p_pfi_amount", pfiAmount));
            inParams.Add(new OracleProcParam("p_catg", catg));
            inParams.Add(new OracleProcParam("p_tx_date", transactionDate));
            inParams.Add(new OracleProcParam("p_current_date", ShopDateTime.Instance.ShopDate));
            OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            inParams.Add(new OracleProcParam("p_current_time", ShopDateTime.Instance.ShopDateCurTime, tsType));
            inParams.Add(new OracleProcParam("p_user_id", desktopSession.UserName));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "update_cacc_info",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "UpdateCaccInfoFailed";
                errorText = "Invocation of UpdateCaccInfo stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking UpdateCaccInfo stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }

        public static bool UpdateProductHeader(DesktopSession desktopSession,
            string storeNumber,
            int ticketNumber,
            string statusCd,
            string tempStatus,
            DateTime statusDate,
            string miscFlags,
            int lastLine,
            string refType,
            out string errorCode,
            out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdatePawnHeader";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("UpdatePawnHeader",
                                                            new ApplicationException(
                                                                "Cannot execute the UpdatePawnHeader update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_storenumber", storeNumber));
            inParams.Add(new OracleProcParam("p_ticket_number", ticketNumber));
            inParams.Add(new OracleProcParam("p_status_cd", statusCd));
            inParams.Add(new OracleProcParam("p_temp_status", tempStatus));
            inParams.Add(new OracleProcParam("p_status_date", statusDate));
            OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            inParams.Add(new OracleProcParam("p_status_time", statusDate, tsType));
            inParams.Add(new OracleProcParam("p_misc_flags", miscFlags));
            inParams.Add(new OracleProcParam("p_last_line", lastLine));
            inParams.Add(new OracleProcParam("p_user_id", desktopSession.UserName));
            if (refType == ProductType.PAWN.ToString())
                inParams.Add(new OracleProcParam("p_ref_type", "1"));
            else if (refType == ProductType.BUY.ToString())
                inParams.Add(new OracleProcParam("p_ref_type", "2"));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "update_header_generic",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "UpdatePawnHeaderFailed";
                errorText = "Invocation of UpdatePawnHeader stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking UpdatePawnHeader stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }

        public static bool UpdateMerchandise(DesktopSession desktopSession,
            string storeNumber,
            Item pawnItem,
            DateTime originationDate,
            int icnSubItem,
            decimal originalItemAmount,
            string customerNumber,
            out string errorCode,
            out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateMerchandise";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("UpdateMerchandise",
                                                            new ApplicationException(
                                                                "Cannot execute the UpdateMerchandise update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            string sPfiAssignmentType = "N";
            switch (pawnItem.PfiAssignmentType)
            {
                case PfiAssignment.CAF:
                    sPfiAssignmentType = "C";
                    break;
                case PfiAssignment.Refurb:
                    sPfiAssignmentType = "R";
                    break;
                case PfiAssignment.Scrap:
                    sPfiAssignmentType = "S";
                    break;
                case PfiAssignment.Sell_Back:
                    sPfiAssignmentType = "SB";
                    break;
                case PfiAssignment.Excess:
                    sPfiAssignmentType = "E";
                    break;
            }

            string sStatusReason = "";
            switch (pawnItem.ItemReason)
            {
                case ItemReason.ADDD:
                    sStatusReason = "ADDED";
                    break;
                case ItemReason.COFFBRKN:
                    sStatusReason = "OFF-BRKN";
                    break;
                case ItemReason.CACC:
                    sStatusReason = "CACC";
                    break;
                case ItemReason.HPFI:
                    sStatusReason = "OFF-HPFI";
                    break;
                case ItemReason.MERGED:
                    sStatusReason = "MERGED";
                    break;
                case ItemReason.NOMD:
                    sStatusReason = "OFF-NOMD";
                    break;
                case ItemReason.COFFNXT:
                    sStatusReason = "OFF-NXT";
                    break;
                case ItemReason.COFFSTLN:
                    sStatusReason = "OFF-STLN";
                    break;
                case ItemReason.COFFSTRU:
                    sStatusReason = "OFF-STRU";
                    break;
            }

            inParams.Add(new OracleProcParam("p_storenumber", storeNumber));
            inParams.Add(new OracleProcParam("p_icn_store", pawnItem.mStore));
            inParams.Add(new OracleProcParam("p_icn_year", pawnItem.mYear));
            inParams.Add(new OracleProcParam("p_icn_doc", pawnItem.mDocNumber));
            inParams.Add(new OracleProcParam("p_icn_doc_type", pawnItem.mDocType));
            inParams.Add(new OracleProcParam("p_icn_item", pawnItem.mItemOrder));
            inParams.Add(new OracleProcParam("p_icn_sub_item", icnSubItem));
            inParams.Add(new OracleProcParam("p_cat_code", pawnItem.CategoryCode));
            inParams.Add(new OracleProcParam("p_customernumber", customerNumber));
            inParams.Add(new OracleProcParam("p_md_date", originationDate));
            inParams.Add(new OracleProcParam("p_amount", originalItemAmount));
            inParams.Add(new OracleProcParam("p_pfi_amount", pawnItem.ItemAmount));
            inParams.Add(new OracleProcParam("p_loc_aisle", ""));
            inParams.Add(new OracleProcParam("p_loc", "N"));
            inParams.Add(new OracleProcParam("p_loc_shelf", ""));
            inParams.Add(new OracleProcParam("p_location", ""));
            inParams.Add(new OracleProcParam("p_gun_number", pawnItem.GunNumber));
            inParams.Add(new OracleProcParam("p_manufacturer", pawnItem.QuickInformation.Manufacturer));
            inParams.Add(new OracleProcParam("p_model", pawnItem.QuickInformation.Model));
            inParams.Add(new OracleProcParam("p_serial_number", pawnItem.QuickInformation.SerialNumber));
            inParams.Add(new OracleProcParam("p_weight", pawnItem.QuickInformation.Weight));
            inParams.Add(new OracleProcParam("p_quantity",
                                                             pawnItem.QuickInformation.Quantity > 0
                                                                 ? pawnItem.QuickInformation.Quantity
                                                                 : 1));
            Int64[] masks = GetMasks(pawnItem.Attributes);
            OracleProcParam maskParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTINT, "p_masks", masks.Length);
            for (int i = 0; i < masks.Length; ++i)
            {
                maskParam.AddValue(masks[i]);
            }
            inParams.Add(maskParam);

            inParams.Add(new OracleProcParam("p_fill1", "Y"));
            inParams.Add(new OracleProcParam("p_fill2", Utilities.GetStringValue(pawnItem.PfiTags, "").PadLeft(10, '0')));
            inParams.Add(new OracleProcParam("p_md_desc", pawnItem.TicketDescription));
            inParams.Add(new OracleProcParam("p_release_date", null));
            inParams.Add(new OracleProcParam("p_disp_date", null));
            inParams.Add(new OracleProcParam("p_disp_type", pawnItem.mDocType == "2" ? "PURCHASE" : "LOAN"));
            inParams.Add(new OracleProcParam("p_disp_doc", 0));
            inParams.Add(new OracleProcParam("p_retail_price", pawnItem.RetailPrice));
            inParams.Add(new OracleProcParam("p_org_disp_date", null));
            inParams.Add(new OracleProcParam("p_org_disp_type", pawnItem.mDocType == "2" ? "PURCHASE" : "LOAN"));
            inParams.Add(new OracleProcParam("p_org_disp_doc", pawnItem.mDocNumber));
            inParams.Add(new OracleProcParam("p_status_date", ShopDateTime.Instance.ShopDate));
            OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            inParams.Add(new OracleProcParam("p_status_time", ShopDateTime.Instance.ShopDate, tsType));
            inParams.Add(new OracleProcParam("p_pfi_date", ShopDateTime.Instance.ShopDate));
            inParams.Add(new OracleProcParam("p_purch_type", ""));
            inParams.Add(new OracleProcParam("p_temp_status", ""));
            inParams.Add(new OracleProcParam("p_xicn", null));
            inParams.Add(new OracleProcParam("p_pol_jday", 0));
            inParams.Add(new OracleProcParam("p_item_amt",
                                                             pawnItem.ItemAmount /
                                                             (pawnItem.QuickInformation.Quantity <= 0
                                                                  ? 1
                                                                  : pawnItem.QuickInformation.Quantity)));
            inParams.Add(new OracleProcParam("p_cacc_lev", pawnItem.CaccLevel < 0 ? "" : pawnItem.CaccLevel.ToString()));
            inParams.Add(new OracleProcParam("p_jcase", null));
            inParams.Add(new OracleProcParam("p_tran_type", sPfiAssignmentType));
            inParams.Add(new OracleProcParam("p_rfb_no", pawnItem.RefurbNumber));
            inParams.Add(new OracleProcParam("p_rfb_store", pawnItem.mStore));
            inParams.Add(new OracleProcParam("p_final_dest", 0));
            inParams.Add(new OracleProcParam("p_num_bar", 0));
            inParams.Add(new OracleProcParam("p_state_flag", null));
            inParams.Add(new OracleProcParam("p_audit_flag", null));
            inParams.Add(new OracleProcParam("p_scrp_code", "0"));
            inParams.Add(new OracleProcParam("p_scrp_cont", "0"));
            inParams.Add(new OracleProcParam("p_scrp_ent_id", "0"));
            inParams.Add(new OracleProcParam("p_warr_type", null));
            inParams.Add(new OracleProcParam("p_disp_item", 0));
            inParams.Add(new OracleProcParam("p_ldisp_print", null));
            inParams.Add(new OracleProcParam("p_ldisp_amount", 0));
            inParams.Add(new OracleProcParam("p_ldisp_paid", null));
            inParams.Add(new OracleProcParam("p_misc1", "0"));
            inParams.Add(new OracleProcParam("p_misc2", ""));
            inParams.Add(new OracleProcParam("p_misc3", ""));
            inParams.Add(new OracleProcParam("p_misc4", ""));
            inParams.Add(new OracleProcParam("p_misc5", ""));
            inParams.Add(new OracleProcParam("p_pfi_cust", customerNumber));
            inParams.Add(new OracleProcParam("p_storage_fee", pawnItem.StorageFee));
            inParams.Add(new OracleProcParam("p_hold_Type", null));
            inParams.Add(new OracleProcParam("p_status_cd", pawnItem.ItemReason == ItemReason.CACC ? "PFC" : "PFI"));
            inParams.Add(new OracleProcParam("p_lastupdatedby", desktopSession.UserName));
            inParams.Add(new OracleProcParam("p_status_reason", sStatusReason));
            //            inParams.Add(new OracleProcParam("p_status_reason", pawnItem.ItemReason.ToString()));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "update_mdse",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "UpdateMerchandiseFailed";
                errorText = "Invocation of UpdateMerchandise stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking UpdateMerchandise stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }

        public static bool UpdateMerchandiseRetailPrice(
            DesktopSession cds,
            Icn icn,
            decimal updatedRetailPrice,
            out string errorCode,
            out string errorText,
            string storeNumber=null)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (cds == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateMerchandiseRetailPrice";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("UpdateMerchandiseRetailPrice",
                                                            new ApplicationException(
                                                                "Cannot execute the UpdateMerchandiseRetailPrice update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            if (!string.IsNullOrEmpty(storeNumber))
            {

                inParams.Add(new OracleProcParam("p_storenumber", storeNumber));
            }
            else
            {
                inParams.Add(new OracleProcParam("p_storenumber", cds.CurrentSiteId.StoreNumber));
            }
            inParams.Add(new OracleProcParam("p_icn_store", icn.ShopNumber));
            inParams.Add(new OracleProcParam("p_icn_year", icn.LastDigitOfYear));
            inParams.Add(new OracleProcParam("p_icn_doc", icn.DocumentNumber));
            inParams.Add(new OracleProcParam("p_icn_doc_type", (int)icn.DocumentType));
            inParams.Add(new OracleProcParam("p_icn_item", icn.ItemNumber));
            inParams.Add(new OracleProcParam("p_icn_sub_item", icn.SubItemNumber));
            inParams.Add(new OracleProcParam("p_amount", updatedRetailPrice));
            inParams.Add(new OracleProcParam("p_user_id", cds.UserName));
            inParams.Add(new OracleProcParam("p_status_date", ShopDateTime.Instance.ShopDate.FormatDate()));
            inParams.Add(new OracleProcParam("p_status_time", ShopDateTime.Instance.ShopTransactionTime.ToString()));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAUL_PAWN_MDSE_PROCS", "update_mdse_retail_price",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "UpdateMerchandiseRetailPriceFailed";
                errorText = "Invocation of UpdateMerchandiseRetailPrice stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking UpdateMerchandiseRetailPrice stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }

        public static bool GetRetailPriceChangeHistory(DesktopSession desktopSession, Icn icn, List<RetailPriceChangeHistoryRecord> historyRecords, out string errorCode, out string errorText)
        {
            if (historyRecords == null)
            {
                throw new ArgumentNullException("historyRecords");
            }

            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", desktopSession.CurrentSiteId.StoreNumber));
            oParams.Add(new OracleProcParam("p_icn_store", icn.ShopNumber));
            oParams.Add(new OracleProcParam("p_icn_year", icn.LastDigitOfYear));
            oParams.Add(new OracleProcParam("p_icn_doc", icn.DocumentNumber));
            oParams.Add(new OracleProcParam("p_icn_doc_type", (int)icn.DocumentType));
            oParams.Add(new OracleProcParam("p_icn_item", icn.ItemNumber));
            oParams.Add(new OracleProcParam("p_icn_sub_item", icn.SubItemNumber));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_retail_price", "retail_price_ref_cursor"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {

                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                    "PAWN_MDSE_PROCS", "get_retail_price_history", oParams, refCursors, "o_return_code",
                    "o_return_text", out outputDataSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetRetailPriceChangeHistory Failed", oEx);
                errorCode = "ExecuteGetRetailPriceChangeHistoryFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetRetailPriceChangeHistory Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteGetRetailPriceChangeHistory";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet != null && outputDataSet.Tables.Count == 1)
            {
                if (outputDataSet.Tables[0].TableName == "retail_price_ref_cursor")
                {
                    foreach (DataRow dr in outputDataSet.Tables[0].Rows)
                    {
                        RetailPriceChangeHistoryRecord record = new RetailPriceChangeHistoryRecord();
                        record.ChangeDate = Utilities.GetDateTimeValue(dr["change_date"]);
                        record.PriceBefore = Utilities.GetDecimalValue(dr["price_before"]);
                        record.PriceAfter = Utilities.GetDecimalValue(dr["price_after"]);
                        record.ChangedBy = Utilities.GetStringValue(dr["user_id"]);
                        historyRecords.Add(record);
                    }

                    errorCode = "0";
                    errorText = "Success";
                    return true;
                }
            }
            return false;
        }


        public static bool UpdateGunType(
            string gunType,
            string storeNumber,
            string gunNumber,
            string userName,
            out string errorCode,
            out string errorText)
        {



            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "update_gun_type";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("update_gun_type",
                                                            new ApplicationException(
                                                                "Cannot execute the update_gun_type stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_gun_type", gunType));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_gun_number", gunNumber));
            inParams.Add(new OracleProcParam("p_user_name", userName));

            //Create output data set names
            bool retVal = false;
            try
            {
                DataSet outputDataSet = null;
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "update_gun_type",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "update_gun_typeFailed";
                errorText = "Invocation of update_gun_type stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking update_gun_type stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }


        public static bool GetGunData(
            OracleDataAccessor dA,
            string storeNumber,
            int gunNumber,
            out DataTable gunData,
            out Item gunItem,
            out string errorCode,
            out string errorText)
        {

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_gun_number", gunNumber));
            gunData = null;
            //Create output data set names
            bool retVal = false;
                DataSet outputDataSet = null;
                
            try
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_gun_data", GUN_DATA));
                refCursArr.Add(new PairType<string, string>("r_mdse_data", GUN_MDSE_DATA));
                refCursArr.Add(new PairType<string, string>("r_mdse_otherdsc_data", GUN_MDSE_OTHERDSC_DATA));
                refCursArr.Add(new PairType<string, string>("r_mdhist_data", GUN_MDHIST_DATA));

                
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "get_gun_data",
                    inParams, refCursArr, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                gunItem = null;
                errorCode = "get_gun_datafailed";
                errorText = "Invocation of get_gun_data stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking get_gun_data stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                gunItem = null;
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            if (dA.ErrorDescription.Contains("No Merchandise data found"))
            {
                gunItem = null;
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (true);
            }
            if (outputDataSet != null && outputDataSet.IsInitialized)
            {
                if (outputDataSet.Tables.Count > 0)
                {
                    gunData = outputDataSet.Tables[GUN_DATA];
                    bool hasGunLock = Utilities.GetStringValue(gunData.Rows[0]["GUNLOCK"], "") == "1"
                                                                    ? true : false;
                    Item tmpItem = new Item();
                    if (outputDataSet.Tables[GUN_MDSE_DATA].Rows.Count > 0)
                    {
                        DataRow dr = outputDataSet.Tables[GUN_MDSE_DATA].Rows[0];
                        tmpItem.TicketDescription = Utilities.GetStringValue(dr["MD_DESC"]);
                        tmpItem.ItemStatus = (ProductStatus)Enum.Parse(typeof(ProductStatus), Utilities.GetStringValue(dr["STATUS_CD"], ""));
                        tmpItem.RetailPrice = Utilities.GetDecimalValue(dr["RETAIL_PRICE"], 0);
                        /*if (tmpItem.RetailPrice <= 0)
                        tmpItem.RetailPrice = Utilities.GetDecimalValue(dr["ITEM_AMT"], 0);*/
                        tmpItem.ItemAmount = Utilities.GetDecimalValue(dr["AMOUNT"], 0.00M);
                        tmpItem.PfiAmount = Utilities.GetDecimalValue(dr["pfi_amount"], 0M);
                        tmpItem.CategoryCode = Utilities.GetIntegerValue(dr["CAT_CODE"]);
                        tmpItem.CategoryDescription = Utilities.GetStringValue(dr["CAT_DESC"]);
                        tmpItem.mDocNumber = Utilities.GetIntegerValue(dr["ICN_DOC"], 0);
                        tmpItem.mDocType = Utilities.GetStringValue(dr["ICN_DOC_TYPE"], "1");
                        tmpItem.mItemOrder = Utilities.GetIntegerValue(dr["ICN_ITEM"], 0);
                        tmpItem.mStore = Utilities.GetIntegerValue(dr["ICN_STORE"], 0);
                        tmpItem.mYear = Utilities.GetIntegerValue(dr["ICN_YEAR"], 0);
                        tmpItem.GunNumber = Utilities.GetIntegerValue(dr["GUN_NUMBER"], 0);
                        string sStoreNumber = Utilities.GetStringValue(dr["STORENUMBER"], "");
                        tmpItem.IsJewelry = Utilities.IsJewelry(tmpItem.CategoryCode);
                        tmpItem.Icn = Utilities.IcnGenerator(tmpItem.mStore,
                                                             tmpItem.mYear,
                                                             tmpItem.mDocNumber,
                                                             tmpItem.mDocType,
                                                             tmpItem.mItemOrder,
                                                             Utilities.GetIntegerValue(dr["ICN_SUB_ITEM"], 0));


                        tmpItem.HasGunLock = hasGunLock;
                        tmpItem.PfiDate = Utilities.GetDateTimeValue(dr["PFI_DATE"]);
                        tmpItem.Md_Date = Utilities.GetDateTimeValue(dr["MD_DATE"]);
                        tmpItem.PfiAssignmentType = tmpItem.IsJewelry ? PfiAssignment.Scrap : PfiAssignment.Normal;
                        if (!string.IsNullOrEmpty(Utilities.GetStringValue(dr["TEMP_STATUS"])))
                            tmpItem.TempStatus = (StateStatus)Enum.Parse(typeof(StateStatus), Utilities.GetStringValue(dr["TEMP_STATUS"], ""));
                        tmpItem.IsGun = Utilities.IsGun(tmpItem.GunNumber, tmpItem.CategoryCode, tmpItem.IsJewelry, tmpItem.MerchandiseType);

                        tmpItem.Attributes = new List<ItemAttribute>();

                        for (int iMask = 1; iMask <= 15; iMask++)
                        {
                            ItemAttribute itemAttribute = new ItemAttribute();

                            if (Utilities.GetIntegerValue(dr["MASK" + iMask.ToString()], 0) > 0)
                            {
                                itemAttribute.MaskOrder = iMask;

                                Answer answer = new Answer();
                                answer.AnswerCode = Utilities.GetIntegerValue(dr["MASK" + iMask.ToString()], 0);
                                answer.AnswerText = Utilities.GetStringValue(dr["MASK_DESC" + iMask.ToString()], "");

                                // Pull from Other Description List Table
                                if (outputDataSet.Tables[GUN_MDSE_OTHERDSC_DATA] != null && answer.AnswerCode == 999)
                                {
                                    string sOtherDscFilter = "MASK_SEQ = " + iMask.ToString();

                                    DataRow[] dataOtherDScRows = outputDataSet.Tables[GUN_MDSE_OTHERDSC_DATA].Select(sOtherDscFilter);

                                    if (dataOtherDScRows.Any())
                                    {
                                        answer.AnswerCode = 999;
                                        answer.AnswerText = Utilities.GetStringValue(dataOtherDScRows[0]["OD_DESC"], "");
                                    }
                                    else
                                    {
                                        answer.AnswerCode = 0;
                                        answer.AnswerText = string.Empty;
                                    }
                                }
                                itemAttribute.Answer = answer;
                            }
                            if (itemAttribute.Answer.AnswerCode == 999 || itemAttribute.Answer.AnswerCode > 0)
                                tmpItem.Attributes.Add(itemAttribute);
                        }
                        QuickCheck QuickInfo = new QuickCheck();
                        QuickInfo.Manufacturer = Utilities.GetStringValue(dr["MANUFACTURER"], "");
                        QuickInfo.Model = Utilities.GetStringValue(dr["MODEL"], "");
                        QuickInfo.SerialNumber = Utilities.GetStringValue(dr["SERIAL_NUMBER"], "");
                        QuickInfo.Weight = Utilities.GetDecimalValue(dr["WEIGHT"], 0);
                        QuickInfo.Quantity = Utilities.GetIntegerValue(dr["QUANTITY"], 0);
                        tmpItem.QuickInformation = QuickInfo;
                        if (outputDataSet.Tables[GUN_MDSE_OTHERDSC_DATA] != null)
                        {
                            string sCommentFilter = "MASK_SEQ = 999";

                            DataRow[] dataOtherCommentRows = outputDataSet.Tables[GUN_MDSE_OTHERDSC_DATA].Select(sCommentFilter);

                            if (dataOtherCommentRows.Count() > 0)
                            {
                                tmpItem.Comment = Utilities.GetStringValue(dataOtherCommentRows[0]["OD_DESC"], "");
                            }
                            else
                            {
                                tmpItem.Comment = "";
                            }
                        }



                    }

                    gunItem = tmpItem;
                    errorCode = "0";
                    errorText = string.Empty;
                    return (true);
                    
                }
            }

            gunItem = null;
            errorCode = "2";
            errorText = "No data returned from the stored procedure for gun " + gunNumber;
            return (false);
        }



        public static bool InsertInventoryChargeOff(
    OracleDataAccessor oDa,
    string storeNumber,
    string statusDate,
    string statusTime,
    string entityNumber,
    string userID,
    List<string> icn,
    List<int> qty,
            List<string> statusReason,
    List<string> retailPrice,
    string entityType,
    string refNumber,
    string refType,
    string salesTax,
    string cashDrawer,
    string tranType,
    string saleAmount,
    string shippingHandling,
    List<string> jewelryCase,
            string authorizedBy,
            string atfNumber,
            string policeCaseNumber,
            string charityOrg,
            string charityAddress,
            string charityCity,
            string charityState,
            string charityZip,
            string replacedICN,
            string comments,
    out int saleTicketNumber,
    out string errorCode,
    out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;
            saleTicketNumber = 0;

            OracleDataAccessor dA = oDa;

            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_status_date", statusDate));
            oParams.Add(new OracleProcParam("p_status_time", statusTime));
            oParams.Add(new OracleProcParam("p_entity_number", entityNumber));
            oParams.Add(new OracleProcParam("p_user_id", userID));
            oParams.Add(new OracleProcParam("p_icn", true, icn));
            oParams.Add(new OracleProcParam("p_qty", true, qty));
            oParams.Add(new OracleProcParam("p_status_reason", true, statusReason));
            oParams.Add(new OracleProcParam("p_retail_price", true, retailPrice));
            oParams.Add(new OracleProcParam("p_entity_type", entityType));
            oParams.Add(new OracleProcParam("p_ref_number", refNumber));
            oParams.Add(new OracleProcParam("p_ref_type", refType));
            oParams.Add(new OracleProcParam("p_sales_tax", salesTax));
            oParams.Add(new OracleProcParam("p_cash_drawer", cashDrawer));
            oParams.Add(new OracleProcParam("p_tran_type", tranType));
            oParams.Add(new OracleProcParam("p_sale_amount", saleAmount));
            oParams.Add(new OracleProcParam("p_shipping_handling", shippingHandling));
            oParams.Add(new OracleProcParam("p_jewelry_case", true, jewelryCase));
            oParams.Add(new OracleProcParam("p_authorized_by", authorizedBy));
            oParams.Add(new OracleProcParam("p_atf_incident_no", atfNumber));
            oParams.Add(new OracleProcParam("p_police_case_no", policeCaseNumber));
            oParams.Add(new OracleProcParam("p_charitable_organization", charityOrg));
            oParams.Add(new OracleProcParam("p_charitable_organization_add", charityAddress));
            oParams.Add(new OracleProcParam("p_charitable_organization_city", charityCity));
            oParams.Add(new OracleProcParam("p_charitable_organization_st", charityState));
            oParams.Add(new OracleProcParam("p_charitable_organization_zip", charityZip));
            oParams.Add(new OracleProcParam("p_replaced_icn", replacedICN));
            oParams.Add(new OracleProcParam("p_comments", comments));
            oParams.Add(new OracleProcParam("o_sale_ticket_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            //Make stored proc call
            bool retVal;
            DataSet outDSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_mdse_procs", "process_charge_off", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("InsertInventoryChargeOff Failed", oEx);
                errorCode = "InsertInventoryChargeOffFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("InsertInventoryChargeOff Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- InsertInventoryChargeOffFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            //Get output number
            DataTable outputDt = outDSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized &&
                outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object saleNumber = dr.ItemArray.GetValue(1);
                    if (saleNumber != null)
                    {
                        saleTicketNumber = Int32.Parse((string)saleNumber);

                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }
            return (false);
        }

        public static bool InsertAddlMdseInfo(
            List<string> icn,
            string storeNumber,
            List<string> couponCodes,
            List<string> couponAmounts,
            List<string> tranCouponCodes,
            List<string> tranCouponAmounts,
            List<string> infoType,
            List<string> retailPrice,
            string status,
            int ticketNumber,
            string userId,
            out string errorCode,
            out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "InsertAddlMdseInfo";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("InsertAddlMdseInfo",
                                                            new ApplicationException(
                                                                "Cannot execute the InsertAddlMdseInfo stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_icn", true, icn));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_coupon_codes", true, couponCodes));
            inParams.Add(new OracleProcParam("p_coupon_amounts", true, couponAmounts));
            inParams.Add(new OracleProcParam("p_tran_coupon_codes", true, tranCouponCodes));
            inParams.Add(new OracleProcParam("p_tran_coupon_amounts", true, tranCouponAmounts));
            inParams.Add(new OracleProcParam("p_info_type", true, infoType));
            inParams.Add(new OracleProcParam("p_retail_price", true, retailPrice));
            inParams.Add(new OracleProcParam("p_status_cd", status));
            inParams.Add(new OracleProcParam("p_ticket_number", ticketNumber));
            inParams.Add(new OracleProcParam("p_user_id", userId));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "insert_addlmdseinfo",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "insert_addlmdseinfoFailed";
                errorText = "Invocation of insert_addlmdseinfo stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking insert_addlmdseinfo stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }


        public static bool UpdateGunData(string storeNumber,
            Item pawnItem,
            string userName,
            out string errorCode,
            out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateGunData";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("UpdateGunData",
                                                            new ApplicationException(
                                                                "Cannot execute the UpdateGunData update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();


            inParams.Add(new OracleProcParam("p_storenumber", storeNumber));
            inParams.Add(new OracleProcParam("p_icn_store", pawnItem.mStore));
            inParams.Add(new OracleProcParam("p_icn_year", pawnItem.mYear));
            inParams.Add(new OracleProcParam("p_icn_doc", pawnItem.mDocNumber));
            inParams.Add(new OracleProcParam("p_icn_doc_type", pawnItem.mDocType));
            inParams.Add(new OracleProcParam("p_icn_item", pawnItem.mItemOrder));
            inParams.Add(new OracleProcParam("p_icn_sub_item", "0"));
            inParams.Add(new OracleProcParam("p_cat_code", pawnItem.CategoryCode));
            inParams.Add(new OracleProcParam("p_gun_number", pawnItem.GunNumber));
            inParams.Add(new OracleProcParam("p_manufacturer", pawnItem.QuickInformation.Manufacturer));
            inParams.Add(new OracleProcParam("p_importer", pawnItem.QuickInformation.Importer));
            inParams.Add(new OracleProcParam("p_model", pawnItem.QuickInformation.Model));
            inParams.Add(new OracleProcParam("p_serial_number", pawnItem.QuickInformation.SerialNumber));
            inParams.Add(new OracleProcParam("p_gun_type", pawnItem.QuickInformation.GunType));
            Int64[] masks = GetMasks(pawnItem.Attributes);
            OracleProcParam maskParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTINT, "p_masks", masks.Length);
            for (int i = 0; i < masks.Length; ++i)
            {
                maskParam.AddValue(masks[i]);
            }
            inParams.Add(maskParam);

            //inParams.Add(new OracleProcParam("p_md_desc", pawnItem.TicketDescription));
            inParams.Add(new OracleProcParam("p_user_name", userName));
            inParams.Add(new OracleProcParam("p_caliber", pawnItem.QuickInformation.Caliber));
            inParams.Add(new OracleProcParam("p_md_desc", pawnItem.TicketDescription));
            inParams.Add(new OracleProcParam("p_gunlock", pawnItem.HasGunLock?"1":"0"));
            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "update_gun_Desc_data",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "UpdateGunDataFailed";
                errorText = "Invocation of UpdateGunData stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking UpdateGunData stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }


        public static bool UpdateGunCustomerData(string storeNumber,
            string gunNumber,
            string customerType,
            string lastName,
            string firstName,
            string middleinitial,
            string address1,
            string city,
            string state,
            string zipcode,
            string idtype,
            string idAgency,
            string idNumber,
            string userName,
            out string errorCode,
            out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateGunCustomerData";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("UpdateGunCustomerData",
                                                            new ApplicationException(
                                                                "Cannot execute the UpdateGunCustomerData update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();


            inParams.Add(new OracleProcParam("p_storenumber", storeNumber));
            inParams.Add(new OracleProcParam("p_gunnnumber", gunNumber));
            inParams.Add(new OracleProcParam("p_customer_Type", customerType));
            inParams.Add(new OracleProcParam("p_last_name", lastName));
            inParams.Add(new OracleProcParam("p_first_name", firstName));
            inParams.Add(new OracleProcParam("p_middle_initial", middleinitial));
            inParams.Add(new OracleProcParam("p_address1", address1));
            inParams.Add(new OracleProcParam("p_city", city));
            inParams.Add(new OracleProcParam("p_state", state));
            inParams.Add(new OracleProcParam("p_postal_code", zipcode));
            inParams.Add(new OracleProcParam("p_id_type", idtype));
            inParams.Add(new OracleProcParam("p_id_agency", idAgency));
            inParams.Add(new OracleProcParam("p_id_number", idNumber));
            inParams.Add(new OracleProcParam("p_user_name", userName));
            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "update_gun_customer_data",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "UpdateGunCustomerDataFailed";
                errorText = "Invocation of UpdateGunCustomerData stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking UpdateGunCustomerData stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }


        private static Int64[] GetMasks(List<ItemAttribute> p)
        {
            if (p == null)
            {
                return (null);
            }

            Int64[] rt = new Int64[15];
            //Initialize array with all zeroes or 
            //the proper mask value
            for (int i = 1; i <= 15; ++i)
            {
                rt[i - 1] = 0L;
                int attrIdx = p.FindIndex(delegate(ItemAttribute itemAttrib)
                {
                    return itemAttrib.MaskOrder == i;
                });
                if (attrIdx >= 0)
                {
                    ItemAttribute pAttrib = p[attrIdx];
                    Int64 answerCode = pAttrib.Answer.AnswerCode;
                    rt[i - 1] = answerCode;
                }
                else
                {
                    rt[i - 1] = 0;
                }
            }
            return (rt);
        }
    }

    public class Tub_Param
    {
        public string Name;
        public string Type;
        public string Size;
        public string Position;
    }
}
