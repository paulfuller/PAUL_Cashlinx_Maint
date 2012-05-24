using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Couch.Impl;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Doc;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Type;
using Common.Libraries.Utility.Shared;
using Document = Common.Libraries.Objects.Doc.Document;

//using PawnUtilities.String;

namespace Common.Controllers.Database.Couch
{
    public class CouchDbUtils
    {
        public static readonly string[] DOC_SEARCH_TYPE = 
        {
            "STORAGE",
            "RECEIPT",
            "CUSTOMER",
            "STORE_TICKET",
            "STORE_CUSTOMER",
            "INVALID",
            "TICKET ADDENDUM",
            "PARTIAL PAYMENT",
            "POLICE CARD",
            "RELEASE_FINGERPRINTS"
        };

        public enum DocSearchType 
        {
            STORAGE         = 0,
            RECEIPT         = 1,
            CUSTOMER        = 2,
            STORE_TICKET    = 3,
            STORE_CUSTOMER  = 4,
            INVALID         = 5,
            TICKET_ADDENDUM = 6,
            PARTPAYMENT     = 7,
            POLICE_CARD     = 8,
            RELEASE_FINGERPRINTS = 9
        }

        public class PawnDocInfo
        {
            public string StorageId { set; get;}
            public string ParentStorageId { set; get;}
            public bool HasParentDocument { set; get;}
            public string StoreNumber { set; get;}
            public long ReceiptNumber
            {
                set; get;
            }
            public string AuxInfo
            {
                set; get;
            }
            public int TicketNumber { set; get;}
            public string CustomerNumber { set; get;}
            public string StorageDate { set; get;}
            public string StorageTime { set; get;}
            public bool UseCurrentShopDateTime { set; get;}
            public string DocFileName { set; get;}
            public bool OverrideDocument { set; get;} 
            public Document.DocTypeNames DocumentType { set; get;}
            public Document DocumentObject
            {
                set;
                get;
            }
            public string DocumentSearchType
            {
                private set; get;
            }

            private DocSearchType docSearchEnumType;
            public DocSearchType DocSearchEnumType
            {
                set
                {
                    this.docSearchEnumType = value;
                    this.SetDocumentSearchType(this.docSearchEnumType);
                }
                get
                {
                    return (this.docSearchEnumType);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public PawnDocInfo()
            {
                StorageId = string.Empty;
                ParentStorageId = string.Empty;
                HasParentDocument = false;
                StoreNumber = string.Empty;
                ReceiptNumber = 0L;
                TicketNumber = 0;
                CustomerNumber = string.Empty;
                StorageDate = string.Empty;
                StorageTime = string.Empty;
                UseCurrentShopDateTime = true;
                DocFileName = string.Empty;
                OverrideDocument = true;
                DocumentType = Document.DocTypeNames.PDF;
                DocumentObject = null;
                DocumentSearchType = string.Empty;
                AuxInfo = string.Empty;
                OverrideDocument = false;
                this.docSearchEnumType = DocSearchType.INVALID;
            }

            /// <summary>
            /// Set appropriate document search string based on input search
            /// type enumeration
            /// </summary>
            /// <param name="docSearchType"></param>
            public void SetDocumentSearchType(DocSearchType docSearchType)
            {
                this.docSearchEnumType = docSearchType;
                var docSearchInt = (uint)docSearchType;

                if (docSearchInt < (uint)DocSearchType.INVALID || docSearchInt == (uint)DocSearchType.POLICE_CARD
                    || docSearchInt == (uint)DocSearchType.RELEASE_FINGERPRINTS)
                {
                    this.DocumentSearchType = DOC_SEARCH_TYPE[docSearchInt];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="cC"></param>
        /// <param name="userID"></param>
        /// <param name="docInfo"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public static bool AddPawnDocument(
            OracleDataAccessor dA,
            SecuredCouchConnector cC,
            string userID,
            ref PawnDocInfo docInfo,
            out string errText)
        {
            return (AddPawnDocument(dA, cC, userID, null, null, ref docInfo, out errText));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="cC"></param>
        /// <param name="userID"></param>
        /// <param name="metaData"></param>
        /// <param name="auxInfo"></param>
        /// <param name="docInfo"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public static bool AddPawnDocument(
            OracleDataAccessor dA,
            SecuredCouchConnector cC,
            string userID,
            Dictionary<string, string> metaData,
            string auxInfo,
            ref PawnDocInfo docInfo,
            out string errText)
        {
            errText = string.Empty;
            if (dA == null || dA.Initialized == false)
            {
                errText = "AddPawnDocument: Database accessor is invalid.";
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CouchDbUtils::AddPawnDocument", errText);
                }
                return (false);
            }
            if (cC == null || docInfo == null || string.IsNullOrEmpty(userID))
            {
                errText = "AddPawnDocument: Couch DB Accessor info is invalid.";
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CouchDbUtils::AddPawnDocument", errText);
                }
                return (false);
            }

            //Generate storage id if the incoming storage id is blank
            if (string.IsNullOrEmpty(docInfo.StorageId))
            {
                docInfo.StorageId = System.Guid.NewGuid().ToString();
                FileLogger.Instance.logMessage(LogLevel.INFO, "Storage Id of the document to store in couch db", docInfo.StorageId);
            }

            //Validate other document info properties
            if (string.IsNullOrEmpty(docInfo.DocFileName))
            {
                errText = "AddPawnDocument: Must specify a valid file name";
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CouchDbUtils::AddPawnDocument", errText);
                    return (false);
                }
            }

            //Send document to couch DB server
            Document doc;
            if (!CouchDbUtils.StoreDocument(
                docInfo.DocFileName, 
                docInfo.StorageId, 
                cC, docInfo.OverrideDocument, 
                docInfo.DocumentType,
                metaData, out doc, out errText))
            {
                errText = "AddPawnDocument: Could not add the document to the storage server." + errText;
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CouchDbUtils::AddPawnDocument", errText);
                }
                return (false);
            }            

            //Store document registry info in the database
            //DateTime storeDate, storeTime;
            if (docInfo.UseCurrentShopDateTime)
            {
                DateTime sDate = ShopDateTime.Instance.ShopDate;
                docInfo.StorageDate = sDate.FormatDate();
                docInfo.StorageTime = ShopDateTime.Instance.ShopTransactionTime;
            }

            string spErrCode, spErrText;
            if (!AddPawnDocumentRegistry(dA,
                                    docInfo.StorageId,
                                    docInfo.ParentStorageId,
                                    docInfo.StoreNumber,
                                    docInfo.TicketNumber,
                                    docInfo.ReceiptNumber,
                                    docInfo.CustomerNumber,
                                    docInfo.StorageDate,
                                    docInfo.StorageTime,
                                    docInfo.DocumentType.ToString(),
                                    auxInfo,
                                    userID,
                                    out spErrCode,
                                    out spErrText))
            {
                errText = 
                    "AddPawnDocument: Could not register document. " +
                    "Registry error code: " + spErrCode + ", error text: " + spErrText;
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CouchDbUtils.AddPawnDocument", errText);
                }
                return (false);
            }

            //Return true if we reach this point!!
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="cC"></param>
        /// <param name="searchInfo"></param>
        /// <param name="retrieveDocFlag">If set to true, will retrieve document data from CouchDB server</param>
        /// <param name="documents"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public static bool GetPawnDocument(
            OracleDataAccessor dA,
            SecuredCouchConnector cC,
            PawnDocInfo searchInfo,
            bool retrieveDocFlag,
            out List<PawnDocInfo> documents,
            out string errText)
        {
            errText = string.Empty;
            documents = new List<PawnDocInfo>(1);

            //Verify inputs
            if (dA == null || dA.Initialized == false)
            {
                errText = "GetPawnDocument: Database accessor is invalid.";
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CouchDbUtils::GetPawnDocument", errText);
                }
                return (false);
            }

            //Search type must be specified
            if (cC == null || searchInfo == null || string.IsNullOrEmpty(searchInfo.DocumentSearchType))
            {
                errText = "GetPawnDocument: Couch DB Accessor info is invalid.";
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CouchDbUtils::GetPawnDocument", errText);
                }
                return (false);
            }

            //Log search type
            if (FileLogger.Instance.IsLogDebug)
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG, 
                    "CouchDbUtils::GetPawnDocument", 
                    "Doc search string = {0}", searchInfo.DocumentSearchType);
            }

            string srchID01 = string.Empty;
            string srchID02 = string.Empty;
            //Switch search type
            switch(searchInfo.DocSearchEnumType)
            {
                case DocSearchType.STORAGE:
                    srchID01 = searchInfo.StorageId;
                    break;
                case DocSearchType.RECEIPT:
                    srchID01 = searchInfo.ReceiptNumber.ToString();
                    break;
                case DocSearchType.CUSTOMER:
                    srchID01 = searchInfo.CustomerNumber;
                    break;
                case DocSearchType.STORE_TICKET:
                case DocSearchType.POLICE_CARD:
                    srchID01 = searchInfo.StoreNumber;
                    srchID02 = searchInfo.TicketNumber.ToString();
                    searchInfo.DocSearchEnumType = DocSearchType.STORE_TICKET;

                    break;
                case DocSearchType.RELEASE_FINGERPRINTS:
                    srchID01 = searchInfo.StoreNumber;
                    srchID02 = searchInfo.TicketNumber.ToString();
                    searchInfo.DocSearchEnumType = DocSearchType.STORE_TICKET;
                    break;

                case DocSearchType.STORE_CUSTOMER:
                    srchID01 = searchInfo.StoreNumber;
                    srchID02 = searchInfo.CustomerNumber;
                    break;
                case DocSearchType.INVALID:
                    break;
            }

            //Ensure that at least one of the search constraints
            //have been specified
            if (string.IsNullOrEmpty(srchID01) &&
                string.IsNullOrEmpty(srchID02))
            {
                errText = "Not enough information specified to " + 
                    "search for document";                    
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, 
                        "CouchDbUtils::GetPawnDocument",
                        errText);
                }
            }

            //Retrieve document registry information
            string spErrCode, spErrText;
            DataTable dataTable;

            //string nowTime = DateTime.Now.FormatDate();
            //Fix to base the time the document search is performed on the current shop date time
            var searchTime = ShopDateTime.Instance.FullShopDateTime.FormatDate();
            if (!GetPawnDocumentRegistry(
                    dA, 
                    srchID01,
                    srchID02,
                    "0", //TODO: Disabled doc chain fetch for now
                    "0", //TODO: Disabled date range fetch for now
                    searchInfo.DocumentSearchType,
                    searchTime,
                    searchTime,
                    out dataTable,
                    out spErrCode,
                    out spErrText))
            {
                errText = 
                    "GetPawnDocument: Could not retrieve document registry info. " +
                    "Registry error code: " + spErrCode + ", error text: " + spErrText;
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CouchDbUtils::GetPawnDocument", errText);
                }
                return (false);
            }
                
            if (dataTable == null || dataTable.Rows == null || dataTable.Rows.Count <= 0)
            {
                errText = "GetPawnDocument: Data table returned from registry is invalid.";
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CouchDbUtils.GetPawnDocument", errText);
                }
                return (false);
            }

            foreach(DataRow dR in dataTable.Rows)
            {
                //Validate data row
                if (dR == null || dR.HasErrors || dR.ItemArray.Length <= 0)
                {
                    continue;
                }

                //Set data from data table
                searchInfo.StorageId    = Utilities.GetStringValue(dR["storage_id"], string.Empty);
                searchInfo.StoreNumber = Utilities.GetStringValue(dR["storenumber"], string.Empty);
                searchInfo.TicketNumber = Utilities.GetIntegerValue(dR["ticket_number"], 0);
                //GJL - 6/15/2010 - Column for receipt detail will now hold receipt number
                searchInfo.ReceiptNumber = Utilities.GetIntegerValue(dR["receiptdetail_number"], 0);
                searchInfo.CustomerNumber = Utilities.GetStringValue(dR["customernumber"], string.Empty);
                searchInfo.AuxInfo = Utilities.GetStringValue(dR["storage_auxinf"], string.Empty);
                searchInfo.DocumentType = (Document.DocTypeNames)
                    Enum.Parse(typeof(Document.DocTypeNames), Utilities.GetStringValue(dR["doc_type"], "INVALID"));
                
                //If retrieve document flag is true, make the call to the couch server
                Document doc = null;
                if (retrieveDocFlag)
                {
                    //Get next document from couch DB server
                    if (!CouchDbUtils.GetDocument(
                            searchInfo.StorageId,
                            cC,
                            out doc,
                            out errText))
                    {
                        errText =
                                "GetPawnDocument: Could not get the document from the storage server.";
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR,
                                                           "CouchDbUtils::GetPawnDocument",
                                                           errText);
                        }

                        //If the couch retrieval fails, do not add an entry to returned
                        //document list
                        continue;
                    }
                }

                //Create doc info container
                var newInfo = new PawnDocInfo
                {
                        StorageId = searchInfo.StorageId,
                        StoreNumber = searchInfo.StoreNumber,
                        TicketNumber = searchInfo.TicketNumber,
                        ReceiptNumber = searchInfo.ReceiptNumber,
                        CustomerNumber = searchInfo.CustomerNumber,
                        AuxInfo = searchInfo.AuxInfo,
                        DocumentType = searchInfo.DocumentType,
                        DocumentObject = doc
                };
                newInfo.SetDocumentSearchType(searchInfo.DocSearchEnumType);
                documents.Add(newInfo);
            }

            return (true);
        }


        public static bool AddPawnDocumentRegistry(
            OracleDataAccessor dA,
            string storageID,
            string parentStorageID, 
            string storeNumber, 
            int ticketNumber, 
            long receiptDetailID, 
            string customerNumber, 
            string storageDate, 
            string storageTime, 
            string docType, 
            string docAuxInfo,
            string userID,
            out string errorCode, 
            out string errorMesg)
        {
            var inputParamsList = new List<OracleProcParam>();
            inputParamsList.Add(new OracleProcParam("p_storage_id", storageID));
            inputParamsList.Add(new OracleProcParam("p_parent_storage_id", parentStorageID));
            inputParamsList.Add(new OracleProcParam("p_store_number", storeNumber));
            inputParamsList.Add(new OracleProcParam("p_ticket_number", ticketNumber));
            inputParamsList.Add(new OracleProcParam("p_receipt_detail_id", receiptDetailID));
            inputParamsList.Add(new OracleProcParam("p_customer_number", customerNumber));
            inputParamsList.Add(new OracleProcParam("p_storage_date", storageDate));
            inputParamsList.Add(new OracleProcParam("p_storage_time", storageTime));
            inputParamsList.Add(new OracleProcParam("p_doc_type", docType));
            inputParamsList.Add(new OracleProcParam("p_doc_auxinfo", docAuxInfo));
            inputParamsList.Add(new OracleProcParam("p_user_id", userID));

            return(internalAddPawnDocument(dA, inputParamsList, out errorCode, out errorMesg));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="inputParams"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        private static bool internalAddPawnDocument(
            OracleDataAccessor dA, 
            List<OracleProcParam> inputParams, 
            out string errorCode, 
            out string errorMesg)
        {
            errorCode = string.Empty;
            errorMesg = string.Empty;
            bool returnVal;            

            if (dA == null || dA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException(
                    "Internal add_pawn_document call to stored procedure failed - invalid data accessor", 
                    new ApplicationException("Internal add_pawn_document call to stored procedure failed - invalid data accessor"));
                return (false);
            }

            try
            {
                DataSet outputDataSet;
                returnVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_GENERATE_DOCUMENTS",
                    "add_pawn_document", inputParams,
                    null, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling add_pawn_document stored procedure", oEx);
                errorCode = (dA.ErrorCode ?? string.Empty) + " - Major fail code 1";
                errorMesg = (dA.ErrorDescription ?? string.Empty) + " -- Exception: " + oEx.Message;
                return (false);
            }

            if (returnVal == false)
            {
                errorCode = dA.ErrorCode;
                errorMesg = dA.ErrorDescription;
                return (false);
            }
            return true;
        }

        //cursor method
        public const string GET_PAWN_DOCUMENT = "getpawndocument";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="searchID01"></param>
        /// <param name="searchID02"></param>
        /// <param name="fetchDocChain"></param>
        /// <param name="useDateRange"></param>
        /// <param name="searchMode"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="getpawndoc"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool GetPawnDocumentRegistry(
            OracleDataAccessor dA,
            string searchID01,
            string searchID02, 
            string fetchDocChain, 
            string useDateRange,
            string searchMode,
            string fromDate,
            string toDate,
            out DataTable getpawndoc, 
            out string errorCode, 
            out string errorMesg)
        {
            //Generate input list
            var inputParamsList = new List<OracleProcParam>();
            inputParamsList.Add(new OracleProcParam("p_search_id_01", searchID01));
            inputParamsList.Add(new OracleProcParam("p_search_id_02", searchID02));
            inputParamsList.Add(new OracleProcParam("p_fetch_doc_chain", fetchDocChain));
            inputParamsList.Add(new OracleProcParam("p_use_date_range", useDateRange));
            inputParamsList.Add(new OracleProcParam("p_search_mode", searchMode));
            inputParamsList.Add(new OracleProcParam("p_from_date", fromDate));
            inputParamsList.Add(new OracleProcParam("p_to_date", toDate));
            bool returnVal = internalGetPawnDocument(
                    dA,
                    inputParamsList, 
                    out getpawndoc, 
                    out errorCode, 
                    out errorMesg);
            return returnVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="inputParams"></param>
        /// <param name="getpawndoc"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        private static bool internalGetPawnDocument(
            OracleDataAccessor dA,
            List<OracleProcParam> inputParams, 
            out DataTable getpawndoc, 
            out string errorCode, 
            out string errorMesg)
        {
            bool returnVal = false;
            errorCode = string.Empty;
            errorMesg = string.Empty;
            getpawndoc = null;

            //Verify data accessor parameter
            if (dA == null || dA.Initialized == false)
            {
                return(false);
            }

            var refCursList = new List<PairType<string, string>>();
            refCursList.Add(new PairType<String, String>("o_documents", GET_PAWN_DOCUMENT));
            DataSet outputDataSet;

            try
            {
                returnVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", 
                    "PAWN_GENERATE_DOCUMENTS",
                    "get_pawn_document", 
                    inputParams,
                    refCursList, 
                    "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException(
                    "Calling get_pawn_document stored procedure", oEx);
                errorCode = "Get Pawn Document";
                errorMesg = "Exception: " + oEx.Message;
                return (false);
            }

            if (returnVal == false)
            {
                errorCode = dA.ErrorCode;
                errorMesg = dA.ErrorDescription;
                return (false);
            }

            if (outputDataSet != null)
            {
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    getpawndoc = outputDataSet.Tables[GET_PAWN_DOCUMENT];
                    errorCode = "0";
                    errorMesg = "Success";
                    return (true);
                }
            }
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileId"></param>
        /// <param name="couchConnector"></param>
        /// <param name="overrideDoc"></param>
        /// <param name="fileType"></param>
        /// <param name="docMetaData"></param>
        /// <param name="couchDocObj"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool StoreDocument(
            string fileName, 
            string fileId,
            SecuredCouchConnector couchConnector,
            bool overrideDoc,
            Document.DocTypeNames fileType, 
            Dictionary<string, string> docMetaData,
            out Document couchDocObj,
            out string errorMessage)
        {
            couchDocObj = null;
            errorMessage = string.Empty;
            
            if (string.IsNullOrEmpty(fileName) ||
                !System.IO.File.Exists(fileName) ||
                string.IsNullOrEmpty(fileId) ||
                couchConnector == null)
            {
                errorMessage = "Input parameters are invalid";
                return (false);
            }            

            var sErrCode = string.Empty;
            var sErrText = string.Empty;
            try
            {
                byte[] fileBytes = File.ReadAllBytes(fileName);
                int lastSlashIdx = fileName.LastIndexOf('\\');
                if (lastSlashIdx == -1)
                {
                    errorMessage = "Absolute file name is invalid.";
                    return (false);
                }

                string fName = fileName.Substring(lastSlashIdx+1);
                couchDocObj = new Document_Couch(fileName, fName, fileType);                
                if (couchDocObj.SetSourceData(fileBytes, fileId, overrideDoc))
                {
                    if (CollectionUtilities.isNotEmpty(docMetaData))
                    {
                        foreach(string key in docMetaData.Keys)
                        {
                            if (key == null)
                                continue;
                            string val;
                            if (docMetaData.TryGetValue(key, out val))
                            {
                                couchDocObj.SetPropertyData(key, val);
                            }
                        }
                    }
                    var dStorage = new DocStorage_CouchDB();
                    if (dStorage.SecuredAddDocument(couchConnector, ref couchDocObj, out sErrCode, out sErrText))
                    {
                        errorMessage = sErrText;
                        return (true);
                    }
                    errorMessage = sErrText;
                    return false;
                }
            }
            catch (Exception eX)
            {
                errorMessage = "Exception occurred while storing document: " + eX.Message + " (" + sErrCode + ", " +
                               sErrText + ")";
            }
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storageId"></param>
        /// <param name="couchConnector"></param>
        /// <param name="doc"></param>
        /// <param name="errorMessage"></param>
        /// <param name="liteFetch"> </param>
        /// <returns></returns>
        public static bool GetDocument(
            string storageId,
            SecuredCouchConnector couchConnector,
            out Document doc,
            out string errorMessage,
            bool liteFetch = false)
        {
            //Set defaults for outgoing params
            doc = null;
            errorMessage = string.Empty;

            if (!string.IsNullOrEmpty(storageId) &&
                couchConnector != null)
            {
                doc = new Document_Couch(storageId);
                var dStorage = new DocStorage_CouchDB();
                string errCode, errTxt;
                if (dStorage.SecuredGetDocument(couchConnector, ref doc, out errCode, out errTxt, liteFetch))
                {
                    return (true);
                }
                errorMessage = "Could not retrieve document: " + errCode + " " + errTxt;
                return (false);
            }

            errorMessage = "Could not retrieve document";
            return (false);
        }

    }
}
