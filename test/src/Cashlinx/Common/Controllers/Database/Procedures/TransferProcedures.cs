using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public static class TransferProcedures
    {
        #region Private Static Members
        private static string storeNumber;
        private static List<string> icn = new List<string>();
        private static List<string> icnQty = new List<string>();
        private static string custNumber;
        private static DateTime tranDate;
        private static DateTime mrDate;
        private static string mrTime;
        private static string mrUser;
        private static string mrDesc;
        private static int mrChange;
        private static string mrType;
        private static string classcode;
        private static string acctNum;
        private static string createdBy;
        private static List<string> gunNumber = new List<string>();
        private static List<string> gunType = new List<string>();
        #endregion

        public const String _TRAN_TYPE_EXCESS = "JOEXC";
        public const String _TRAN_TYPE_SCRAP = "JOSCR";
        public const String _TRAN_TYPE_REFURB = "JORFB";

        /// <summary>
        /// Processes pending transfer items by getting a list of pending items
        /// and transferring them via a web service call.
        /// </summary>
        /// <param name="pStoreNumber">
        /// Unique identifier of the store where items are being transferred FROM.
        /// </param>
        /// <param name="transferDate">
        /// ShopDate
        /// </param>
        /// <param name="errorMessage">
        /// Out parameter that is set with the message of any error occurring during this process.
        /// </param>
        /// <returns>
        /// Returns true if no errors encountered. False is returned if any errors occur.
        /// </returns>
        public static bool ProcessPendingTransfers(string pStoreNumber, string transferDate, out string errorMessage)
        {
            errorMessage = "";
            string errorCode = "";
            bool errorbool = true;

            DataTable transferItems = new DataTable();
            List<int> cashlinxStores = new List<int>();

            TransfersDBProcedures.ExecuteGetCommonTOTickets(pStoreNumber, transferDate, out transferItems, out cashlinxStores, out errorCode, out errorMessage);
            if (!InvokeWebServiceForAllTransfers(pStoreNumber, transferItems, cashlinxStores, out errorMessage)) errorbool = false;

            return errorbool;
        }

        public static bool InvokeWebServiceForTransfer(string pStoreNumber, DataTable transferItems, out string errorMessage, string tranType)
        {
            bool returnVal = true;
            errorMessage = null;
            if (transferItems.Rows.Count > 0)
            {
                foreach (DataRow row in transferItems.Rows)
                {
                    int transferTicketNumber = Utilities.GetIntegerValue(row["TRANSTICKETNUM"], -1);
                    int destinationStore = Utilities.GetIntegerValue(row["ENTITYNUMBER"], -1);
                    //Make the web service call
                    TransferWebService transferwebService = new TransferWebService();

                    if (transferwebService.CompleteCatcoTypeTransfersWS(pStoreNumber, transferTicketNumber, destinationStore, tranType, out errorMessage))
                    {
                        // FileLogger.Instance.logMessage(LogLevel.DEBUG, transferwebService,
                        //                               "Successful " + tranType + " Transfer Call for " + transferTicketNumber);
                    }
                    else
                    {
                        FileLogger.Instance.logMessage(
                            LogLevel.ERROR, transferwebService, " Transfer Call for " + transferTicketNumber + " Failed due to " + transferwebService.ErrorMessage);
                        //errorMessage = "Transfer web service call failed for :"+tranType;
                        returnVal = false;
                    }
                }
            }
            else
                FileLogger.Instance.logMessage(LogLevel.INFO, null,
                                               "No data passed for transferring");
            //errorMessage = null;
            return returnVal;
        }

        public static bool InvokeWebServiceForAllTransfers(string pStoreNumber, DataTable transferItems, List<int> cashlinxStores, out string errorMessage)
        {
            bool returnVal = true;
            errorMessage = null;
            TransferWebService transferwebService = null;

            if (transferItems.Rows.Count > 0)
            {
                foreach (DataRow row in transferItems.Rows)
                {
                    int transferTicketNumber = Utilities.GetIntegerValue(row["TRANSTICKETNUM"], -1);
                    int destinationStore = Utilities.GetIntegerValue(row["ENTITYNUMBER"], -1);
                    string transType = Utilities.GetStringValue(row["TRANTYPE"], string.Empty);
                    //Make the web service call

                    if (transType == null || transType.Equals(string.Empty))
                    {
                        //Shop to Shop and Gun Transfer
                        if (cashlinxStores != null && !cashlinxStores.Contains(destinationStore))
                        {
                            transferwebService = new TransferWebService();
                            if (!transferwebService.CompleteShopAndGunTransferWS(pStoreNumber, transferTicketNumber, destinationStore))
                            {
                                returnVal = false;
                            }
                        }
                    }
                    else
                    {
                        transferwebService = new TransferWebService();
                        //CATCO transfer
                        if (transferwebService.CompleteCatcoTypeTransfersWS(pStoreNumber, transferTicketNumber, destinationStore, transType, out errorMessage))
                        {
                            // FileLogger.Instance.logMessage(LogLevel.DEBUG, transferwebService,
                            //                                "Successful " + transType + " Transfer Call for " + transferTicketNumber);
                        }
                        else
                        {
                            FileLogger.Instance.logMessage(
                                LogLevel.ERROR, transferwebService, " Transfer Call for " + transferTicketNumber + " Failed due to " + transferwebService.ErrorMessage);
                            returnVal = false;
                        }
                    }
                }
            }
            else
                FileLogger.Instance.logMessage(LogLevel.INFO, null,
                                               "No data passed for transferring");
            //errorMessage = null;
            return returnVal;
        }

        public static bool TransferScrap(
            List<TransferItemVO> mdseToTransfer,
            int scrapTransferFacilityNumber,
            int excessTransferFacilityNumber,
            int refurbTransferFacilityNumber,
            string carrier,
            out int transferNumber,
            out string errorMessage)
        {
            errorMessage = String.Empty;
            transferNumber = 0;
            string errorCode = String.Empty;
            icn = new List<string>();
            icnQty = new List<string>();
            gunNumber = new List<string>();
            gunType = new List<string>();
            if (mdseToTransfer.Count > 0)
            {
                foreach (TransferItemVO transfermdse in mdseToTransfer)
                {
                    storeNumber = transfermdse.StoreNumber;
                    icn.Add(transfermdse.ICN);
                    icnQty.Add(transfermdse.ICNQty);
                    custNumber = transfermdse.CustomerNumber;
                    tranDate = transfermdse.TransactionDate;
                    mrDate = transfermdse.MdseRecordDate;
                    mrTime = transfermdse.MdseRecordTime;
                    mrUser = transfermdse.MdseRecordUser;
                    mrDesc = transfermdse.MdseRecordDesc;
                    mrChange = transfermdse.MdseRecordChange;
                    mrType = transfermdse.MdseRecordType;
                    classcode = transfermdse.ClassCode;
                    acctNum = transfermdse.AcctNumber;
                    createdBy = transfermdse.CreatedBy;
                    gunNumber.Add(transfermdse.GunNumber);
                    gunType.Add(transfermdse.GunType);
                }

                if (icn.Count > 0)
                {
                    //Make the db call for transfer
                    /* storeNumber will be value from last item being transferred (set in foreach loop above), 
                    * but all items should be coming FROM the same store.
                    */
                    if (!(TransfersDBProcedures.ExecuteTransferOutOfStore(storeNumber, icn, icnQty,
                                                                          custNumber, tranDate, mrDate, mrTime, mrUser, mrDesc,
                                                                          mrChange, mrType, classcode, acctNum, createdBy, gunNumber, gunType, scrapTransferFacilityNumber, excessTransferFacilityNumber, refurbTransferFacilityNumber,
                                                                          null,true, out transferNumber, carrier, out errorCode, out errorMessage)))
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, null, "(Error " + errorCode + ") " + errorMessage);
                        errorMessage = "Transfer database call failed with error code " + errorCode;
                    }
                    else
                        return true;
                }
            }
            else
                errorMessage = "No data passed for transferring";

            return (false);
        }

        public static bool TransferToGunRoom(List<TransferItemVO> mdseToTransfer, out int transferNumber, string carrier, out string errorMessage, string gunFacilityName)
        {
            errorMessage = "";
            transferNumber = 0;
            string errorCode = "";
            icn = new List<string>();
            icnQty = new List<string>();
            gunNumber = new List<string>();
            gunType = new List<string>();
            if (mdseToTransfer.Count > 0)
            {
                foreach (TransferItemVO transfermdse in mdseToTransfer)
                {
                    storeNumber = transfermdse.StoreNumber;
                    icn.Add(transfermdse.ICN);
                    icnQty.Add(transfermdse.ICNQty);
                    custNumber = transfermdse.CustomerNumber;
                    tranDate = transfermdse.TransactionDate;
                    mrDate = transfermdse.MdseRecordDate;
                    mrTime = transfermdse.MdseRecordTime;

                    mrUser = transfermdse.MdseRecordUser;
                    mrDesc = transfermdse.MdseRecordDesc;
                    mrChange = transfermdse.MdseRecordChange;
                    mrType = transfermdse.MdseRecordType;
                    classcode = transfermdse.ClassCode;
                    acctNum = transfermdse.AcctNumber;
                    createdBy = transfermdse.CreatedBy;
                    gunNumber.Add(transfermdse.GunNumber);
                    gunType.Add(transfermdse.GunType);
                }

                if (icn.Count > 0)
                {
                    //Make the db call for transfer
                    GlobalDataAccessor.Instance.beginTransactionBlock();
                    /* storeNumber will be value from last item being transferred (set in foreach loop above), 
                    * but all items should be coming FROM the same store.
                    */
                    if (TransfersDBProcedures.ExecuteTransferOutOfStore(storeNumber, icn, icnQty,
                                                                        custNumber, tranDate, mrDate, mrTime, mrUser, mrDesc,
                                                                        mrChange, mrType, classcode, acctNum, createdBy, gunNumber, gunType, 0, 0, 0, gunFacilityName,true, out transferNumber,
                                                                        carrier, out errorCode, out errorMessage))
                    {
                        GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                        if (transferNumber > 0 && errorCode == "0")
                        {
                            return true;
                        }
                    }
                    else
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, null, errorMessage);
                        errorMessage = "Gun Transfer database call failed";
                        GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                    }
                }
            }
            else
                errorMessage = "No data passed for transferring";

            return (false);
        }

        public static bool TransferItemsOutOfStore(List<TransferItemVO> mdseToTransfer, out int transferNumber, string carrier, out string errorMessage, bool isClxToClx, string toShopName)
        {
            errorMessage = "";
            transferNumber = 0;
            string errorCode = "";
            icn = new List<string>();
            icnQty = new List<string>();
            gunNumber = new List<string>();
            gunType = new List<string>();
            if (mdseToTransfer.Count > 0)
            {
                foreach (TransferItemVO transfermdse in mdseToTransfer)
                {
                    storeNumber = transfermdse.StoreNumber;
                    icn.Add(transfermdse.ICN);
                    icnQty.Add(transfermdse.ICNQty);
                    custNumber = transfermdse.CustomerNumber;
                    tranDate = transfermdse.TransactionDate;
                    mrDate = transfermdse.MdseRecordDate;
                    mrTime = transfermdse.MdseRecordTime;

                    mrUser = transfermdse.MdseRecordUser;
                    mrDesc = transfermdse.MdseRecordDesc;
                    mrChange = transfermdse.MdseRecordChange;
                    mrType = transfermdse.MdseRecordType;
                    classcode = transfermdse.ClassCode;
                    acctNum = transfermdse.AcctNumber;
                    createdBy = transfermdse.CreatedBy;
                    gunNumber.Add(transfermdse.GunNumber);
                    gunType.Add(transfermdse.GunType);
                }

                if (icn.Count > 0)
                {
                    //Make the db call for transfer
                    GlobalDataAccessor.Instance.beginTransactionBlock();
                    /* storeNumber will be value from last item being transferred (set in foreach loop above), 
                    * but all items should be coming FROM the same store.
                    */
                    if (TransfersDBProcedures.ExecuteTransferOutOfStore(storeNumber, icn, icnQty,
                                                                        custNumber, tranDate, mrDate, mrTime, mrUser, mrDesc,
                                                                        mrChange, mrType, classcode, acctNum, createdBy, gunNumber, gunType, 0, 0, 0, toShopName,isClxToClx, out transferNumber,
                                                                        carrier, out errorCode, out errorMessage))
                    {
                        GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                        return true;
                        //Web Service invocation will be done during shop close
                        //Make the web service call
                        /*TransferWebService transferwebService = new TransferWebService();
                        storeNumber will be value from last item being transferred (set in foreach loop above), 
                        but all items should be coming FROM the same store.
                        if (!isClxToClx)
                        {
                        if (transferwebService.CompleteShopAndGunTransferWS(storeNumber, transferNumber, Utilities.GetIntegerValue(toShopName)))
                        {
                        return true;
                        }
                        }
                        else
                        {
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, transferwebService, "Web Service call Aborted since transfer is Clx to Clx");
                        return true;
                        }
                        FileLogger.Instance.logMessage(LogLevel.ERROR, transferwebService, transferwebService.ErrorMessage);
                        errorMessage = "Transfer web service call failed";
                        return false;*/
                    }
                    else
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, null, errorMessage);
                        errorMessage = "Transfer database call failed";
                        GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                    }
                }
            }
            else
                errorMessage = "No data passed for transferring";

            return (false);
        }

        public static List<IItem> GetJsupItemsByStore(DesktopSession dSession, string pStoreNumber, string pTransferType, out string errorMessage)
        {
            // Declare OUT parameters.
            DataTable table = new DataTable();
            DataTable tableDesc = new DataTable();
            string errorCode;
            string errorText;

            errorMessage = string.Empty;

            List<IItem> items = new List<IItem>();

            bool retVal =
            TransfersDBProcedures.ExecuteGetJsupMerchandise(
                pStoreNumber, pTransferType,
                out table, out tableDesc, out errorCode, out errorText);

            if (retVal == false || table == null || string.IsNullOrEmpty(errorText) == false)
            {
                errorMessage = errorCode + ": " + errorText;
                FileLogger.Instance.logMessage(LogLevel.ERROR, null, errorMessage);
                return items;
            }

            IItem item = new Item(); //re-instantiated and re-used in each loop iteration
            foreach (DataRow r in table.Rows)
            {
                if (pTransferType.Equals("SCRAP", StringComparison.CurrentCultureIgnoreCase))
                    item = new ScrapItem();
                else if (pTransferType.Equals("REFURB", StringComparison.CurrentCultureIgnoreCase))
                //item = new Item();
                    item = new ScrapItem();
                else if (pTransferType.Equals("EXCESS", StringComparison.CurrentCultureIgnoreCase))
                // item = new Item();
                    item = new ScrapItem();
                //else handled by TransfersDBProcedures's call above returning false with errorCode and errorText having been set

                int icndocFromRow = Utilities.GetIntegerValue(r["ICN_DOC"], -1);
                string storeNumberFromRow = Utilities.GetStringValue(r["STORENUMBER"], "");

                item.RefurbNumber = Utilities.GetIntegerValue(r["RFB_NO"]);
                item.Icn = Utilities.GetStringValue(r["ICN"]);
                item.TicketDescription = Utilities.GetStringValue(r["MD_DESC"]);
                item.ItemAmount = Utilities.GetDecimalValue(r["PFI_AMOUNT"]);

                item.ItemStatus = (ProductStatus)Enum.Parse(typeof(ProductStatus), Utilities.GetStringValue(r["STATUS_CD"], ""));
                item.mDocType = Utilities.GetStringValue(r["ICN_DOC_TYPE"]);
                item.mStore = Utilities.GetIntegerValue(r["ICN_STORE"]);
                item.CategoryCode = Utilities.GetIntegerValue(r["CAT_CODE"], 0);
                item.CategoryDescription = Utilities.GetStringValue(r["CAT_DESC"], "");
                item.mItemOrder = Utilities.GetIntegerValue(r["ICN_ITEM"]);
                item.CaccLevel = Utilities.GetIntegerValue(r["CACC_LEV"], -1);

                item.Attributes = new List<ItemAttribute>();

                //Used to get the attributes of the item.
                //This will be helpful later, such as determining the type of metal for scraps.  
                for (int iMask = 1; iMask <= 15; iMask++)
                {
                    ItemAttribute itemAttribute = new ItemAttribute();

                    if (Utilities.GetIntegerValue(r["MASK" + iMask.ToString()], 0) > 0)
                    {
                        itemAttribute.MaskOrder = iMask;

                        Answer answer = new Answer();
                        answer.AnswerCode = Utilities.GetIntegerValue(r["MASK" + iMask.ToString()], 0);
                        answer.AnswerText = Utilities.GetStringValue(r["MASK_DESC" + iMask.ToString()], "");

                        // Pull from Other Description List Table
                        if (tableDesc != null && answer.AnswerCode == 999)
                        {
                            string sOtherDscFilter = "STORENUMBER = '" + storeNumberFromRow + "'";
                            sOtherDscFilter += " and ICN_STORE = " + item.mStore.ToString();
                            sOtherDscFilter += " and ICN_YEAR = " + item.mYear.ToString();
                            sOtherDscFilter += " and ICN_DOC = '" + icndocFromRow + "' ";
                            sOtherDscFilter += " and ICN_DOC_TYPE = " + item.mDocType;
                            sOtherDscFilter += " and ICN_ITEM = " + item.mItemOrder;
                            sOtherDscFilter += " and ICN_SUB_ITEM = 0";
                            sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();

                            DataRow[] dataOtherDScRows = tableDesc.Select(sOtherDscFilter);
                            if (dataOtherDScRows.Length > 0)
                            {
                                answer.AnswerCode = 999;
                                answer.AnswerText = Utilities.GetStringValue(dataOtherDScRows[0]["OD_DESC"], "");
                            }
                            else
                            {
                                answer.AnswerCode = 0;
                                answer.AnswerText = "";
                            }
                        }
                        itemAttribute.Answer = answer;
                    }
                    if (itemAttribute.Answer.AnswerCode == 999 || itemAttribute.Answer.AnswerCode > 0)
                        item.Attributes.Add(itemAttribute);
                }

                //Set item attributes
                int iCategoryMask = dSession.CategoryXML.GetCategoryMask(item.CategoryCode);
                DescribedMerchandise dmPawnItem = new DescribedMerchandise(iCategoryMask);

                Item pawnItem = (Item)item;
                Item.PawnItemMerge(ref pawnItem, dmPawnItem.SelectedPawnItem, true);

                //Should copy the description, etc. over to the item.
                PropertyInfo[] fromFields = typeof(Item).GetProperties();
                PropertyInfo[] toFields = //typeof(ScrapItem).GetProperties();
                item.GetType().GetProperties();
                PropertyHandler.SetProperties(fromFields, toFields, pawnItem, item);

                //Does not merge well.
                item.Quantity = Utilities.GetIntegerValue(r["QUANTITY"]);

                if (item is ScrapItem)
                {
                    //Wait to add scrap items until after merge.
                    ((ScrapItem)item).StoreNumber = storeNumberFromRow;
                    ((ScrapItem)item).TicketNumber = icndocFromRow;
                }

                items.Add(item);
            }

            return items;
        }

        public static bool GetTransferInTickets(DesktopSession cds, List<TransferVO> transfers, out string errorCode, out string errorText)
        {
            if (transfers == null)
            {
                throw new ArgumentNullException("transfers");
            }

            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", cds.CurrentSiteId.StoreNumber));
            oParams.Add(new OracleProcParam("p_user_id", cds.FullUserName));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_ti_tickets", "ti_tickets_ref_cursor"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "transfers", "retrieve_TI_tickets", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetTransferInTickets Failed", oEx);
                errorCode = "ExecuteGetTransferInTicketsFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetTransferInTickets Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteGetTransferInTickets";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet != null && outputDataSet.Tables.Count == 1)
            {
                if (outputDataSet.Tables[0].TableName == "ti_tickets_ref_cursor")
                {
                    foreach (DataRow dr in outputDataSet.Tables[0].Rows)
                    {
                        TransferVO transfer = new TransferVO();
                        transfer.Amount = Utilities.GetDecimalValue(dr["amount"], 0);
                        transfer.Carrier = Utilities.GetStringValue(dr["carrier"]);
                        transfer.NumberOfItems = Utilities.GetIntegerValue(dr["LastLine"], 0);
                        transfer.Status = Utilities.ParseEnum<TransferStatus>(Utilities.GetStringValue(dr["status"], string.Empty), TransferStatus.TI);
                        transfer.StatusDate = Utilities.GetDateTimeValue(dr["statusdate"]);
                        transfer.StoreNickName = Utilities.GetStringValue(dr["storenickname"]);
                        transfer.OriginalStoreNumber = Utilities.GetStringValue(dr["storenumber"]);
                        transfer.TempStatus = Utilities.ParseEnum<TransferTempStatus>(Utilities.GetStringValue(dr["tempstatus"], string.Empty), TransferTempStatus.NONE);
                        transfer.TransferTicketNumber = Utilities.GetIntegerValue(dr["transticketnum"], 0);
                        transfer.TransferType = Utilities.ParseEnum<TransferTypes>(Utilities.GetStringValue(dr["trantype"], string.Empty), TransferTypes.STORETOSTORE);
                        transfer.TransferSource = Utilities.ParseEnum<TransferSource>(Utilities.GetStringValue(dr["ticket_type"], string.Empty), TransferSource.NONE);
                        transfer.RejectReason = Utilities.GetStringValue(dr["reject_reason"], string.Empty);
                        transfer.DestinationStoreNumber = Utilities.GetStringValue(dr["entitynumber"], string.Empty);

                        if (transfer.TransferSource == TransferSource.NONE)
                        {
                            continue;
                        }
                        transfers.Add(transfer);
                    }
                }

                errorCode = "0";
                errorText = "Success";
                return true;
            }
            return false;
        }

        public static bool GetTransferInItems(DesktopSession cds, TransferVO transfer, out string errorCode, out string errorText)
        {
            if (transfer == null)
            {
                throw new ArgumentNullException("transfer");
            }

            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_ticket_number", transfer.TransferTicketNumber));
            oParams.Add(new OracleProcParam("p_store_number", cds.CurrentSiteId.StoreNumber));
            oParams.Add(new OracleProcParam("p_user_id", cds.FullUserName));
            oParams.Add(new OracleProcParam("p_transfer_type", (transfer.TransferSource == TransferSource.TOPSTICKET && transfer.TempStatus == TransferTempStatus.REJCT) ? TransferSource.CLXTICKET.ToString() : transfer.TransferSource.ToString()));
            oParams.Add(new OracleProcParam("p_origination_store_number", transfer.OriginalStoreNumber));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_ti_mdse", "ti_mdse_ref_cursor"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "transfers", "retrieve_TI_ticket_mdse", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetTransferInTickets Failed", oEx);
                errorCode = "ExecuteGetTransferInTicketsFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetTransferInTickets Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteGetTransferInTickets";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet != null && outputDataSet.Tables.Count == 1)
            {
                foreach (DataRow dr in outputDataSet.Tables[0].Rows)
                {
                    TransferItemVO item = new TransferItemVO();
                    item.ICN = Utilities.GetStringValue(dr["icn"]);
                    item.TransferType = Utilities.ParseEnum<TransferTypes>(Utilities.GetStringValue(dr["trantype"], string.Empty), TransferTypes.STORETOSTORE);
                    item.MdseRecordDesc = Utilities.GetStringValue(dr["md_desc"]);
                    item.PfiAmount = Utilities.GetDecimalValue(dr["pfi_amount"], 0);
                    item.RefurbNumber = Utilities.GetIntegerValue(dr["rfb_no"], 0);
                    item.ICNQty = Utilities.GetStringValue(dr["quantity"]);
                    item.Transfer = transfer;
                    transfer.Items.Add(item);
                }
            }
            return true;
        }

        public static bool ProcessTransferIn(DesktopSession cds, TransferVO transfer, out int ticketNumber, out string errorCode, out string errorText)
        {
            if (transfer == null)
            {
                throw new ArgumentNullException("transfer");
            }

            ticketNumber = 0;

            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_transfer_ticket_number", transfer.TransferTicketNumber));
            oParams.Add(new OracleProcParam("p_store_number", cds.CurrentSiteId.StoreNumber));
            oParams.Add(new OracleProcParam("p_user_id", cds.FullUserName));
            oParams.Add(new OracleProcParam("p_transfer_type", transfer.GetTransferTypeDatabaseValue()));
            oParams.Add(new OracleProcParam("p_carrier", transfer.Carrier));
            oParams.Add(new OracleProcParam("p_status_date", ShopDateTime.Instance.ShopDate.FormatDate()));
            oParams.Add(new OracleProcParam("p_status_time", ShopDateTime.Instance.ShopTransactionTime.ToString()));
            oParams.Add(new OracleProcParam("p_origination_store_number", transfer.OriginalStoreNumber));
            oParams.Add(new OracleProcParam("p_ticket_type", transfer.TransferSource.ToString()));
            oParams.Add(new OracleProcParam("o_ti_ticket", OracleDbType.Int32, DBNull.Value, ParameterDirection.Output, 1));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "transfers", "process_ti", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteProcessTransferIn Failed", oEx);
                errorCode = "ExecuteProcessTransferInFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteProcessTransferInTickets Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteProcessTransferIn";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            DataTable outputDt = outputDataSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized &&
                outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object ticketNumberObj = dr.ItemArray.GetValue(1);
                    if (ticketNumberObj != null)
                    {
                        ticketNumber = Utilities.GetIntegerValue(dr.ItemArray.GetValue(1));
                        errorCode = "0";
                        errorText = "Success";
                        SaveTransferInReceipts(cds, ticketNumber, transfer.Amount);
                        return (true);
                    }
                }
            }

            errorCode = "Error";
            errorText = "NoSuccess";

            
            return (false);
        }

        public static bool RejectTransfer(DesktopSession cds, TransferVO transfer, out string errorCode, out string errorText)
        {
            if (transfer == null)
            {
                throw new ArgumentNullException("transfer");
            }

            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_transfer_ticket_number", transfer.TransferTicketNumber));
            oParams.Add(new OracleProcParam("p_store_number", cds.CurrentSiteId.StoreNumber));
            oParams.Add(new OracleProcParam("p_user_id", cds.FullUserName));
            oParams.Add(new OracleProcParam("p_origination_store_number", transfer.OriginalStoreNumber));
            oParams.Add(new OracleProcParam("p_reject_reason", transfer.RejectReason));
            oParams.Add(new OracleProcParam("p_status_date", ShopDateTime.Instance.ShopDate.FormatDate()));
            oParams.Add(new OracleProcParam("p_status_time", ShopDateTime.Instance.ShopTransactionTime.ToString()));
            oParams.Add(new OracleProcParam("p_carrier", transfer.Carrier));
            oParams.Add(new OracleProcParam("p_ticket_type", transfer.TransferSource.ToString()));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "transfers", "reject_transfer", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("Execute RejectTransfer Failed", oEx);
                errorCode = "ExecuteRejectTransferFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("Execute RejectTransfer Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteRejectTransfer";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }
            
            return true;
        }

        private static void SaveTransferInReceipts(DesktopSession dSession, int transferNumber, decimal TotalTransferAmount)
        {
            #region receipt
            var errorCode = String.Empty;
            var errorText = String.Empty;
            var hasErrors = false;
            ReceiptDetailsVO transferReceiptDetailsVO = new ReceiptDetailsVO();
            transferReceiptDetailsVO.ReceiptDate = ShopDateTime.Instance.ShopDate;
            transferReceiptDetailsVO.RefDates = new List<string>() { ShopDateTime.Instance.ShopDate.ToShortDateString() };
            transferReceiptDetailsVO.RefTimes = new List<string>() { ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString() };
            transferReceiptDetailsVO.UserId = dSession.UserName;
            transferReceiptDetailsVO.RefNumbers = new List<string>() { Convert.ToString(transferNumber) };
            transferReceiptDetailsVO.RefTypes = new List<string>() { "6" };
            transferReceiptDetailsVO.RefStores = new List<string>() { dSession.CurrentSiteId.StoreNumber };
            transferReceiptDetailsVO.RefEvents = new List<string>() { "TI" };
            transferReceiptDetailsVO.RefAmounts = new List<string>() { Convert.ToString(TotalTransferAmount) };

            ProcessTenderProcedures.ExecuteInsertReceiptDetails(
                dSession.CurrentSiteId.StoreNumber,
                dSession.UserName,
                ShopDateTime.Instance.ShopDate.ToShortDateString(),
                dSession.FullUserName,
                ref transferReceiptDetailsVO,
                out errorCode,
                out errorText);

            if (errorCode != "0")
            {
                hasErrors = true;
            }

            errorCode = String.Empty;
            errorText = String.Empty;

            if (hasErrors)
            {
                BasicExceptionHandler.Instance.AddException("Transfer receipts were not created", new ApplicationException());
                FileLogger.Instance.logMessage(LogLevel.ERROR, "TransferProcedures.cs", "Transfer receipts were not created");
            }
            #endregion receipt
        }
    }
}
