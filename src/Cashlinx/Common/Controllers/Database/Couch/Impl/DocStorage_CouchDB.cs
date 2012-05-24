/**********************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products.Describe Merchandise
 * Class:           DocStorage_CouchDB
 * 
 * Description      Inherited class for Couch DB Document Actions
 * 
 * History
 * David D Wise, Initial Development
 *   
 *************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using Common.Libraries.Objects.Doc;

namespace Common.Controllers.Database.Couch.Impl
{
    public class DocStorage_CouchDB : IDocStorage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startupData"></param>
        /// <param name="sErrorCode"></param>
        /// <param name="sErrorText"></param>
        /// <returns></returns>
        public bool Startup(
            DocumentAccess startupData, 
            out string sErrorCode, 
            out string sErrorText)
        {
            sErrorCode = string.Empty;
            sErrorText = string.Empty;
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shutdownData"></param>
        /// <param name="sErrorCode"></param>
        /// <param name="sErrorText"></param>
        /// <returns></returns>
        public bool Shutdown(
            DocumentAccess shutdownData, 
            out string sErrorCode, 
            out string sErrorText)
        {
            sErrorCode = string.Empty;
            sErrorText = string.Empty;
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessSettings"></param>
        /// <param name="document"></param>
        /// <param name="sErrorCode"></param>
        /// <param name="sErrorText"></param>
        /// <returns></returns>
        public bool AddDocument(
            DocumentAccess accessSettings,
            ref Document document,
            out string sErrorCode,
            out string sErrorText)
        {
            sErrorCode = "-1";
            sErrorText = "Adding document to database failed.";
            if (!string.IsNullOrEmpty(accessSettings.SystemAddress) && 
                !string.IsNullOrEmpty(accessSettings.AddressPort) &&
                !string.IsNullOrEmpty(accessSettings.DatabaseName))
            {
                var couchConnector = new CouchConnector(accessSettings.SystemAddress,
                                                        accessSettings.AddressPort,
                                                        accessSettings.DatabaseName);
                return (AddDocument(couchConnector, ref document, out sErrorCode, out sErrorText));
            }
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="couchConnector"></param>
        /// <param name="document"></param>
        /// <param name="sErrorCode"></param>
        /// <param name="sErrorText"></param>
        /// <returns></returns>
        public bool AddDocument(
            CouchConnector couchConnector, 
            ref Document document, 
            out string sErrorCode, 
            out string sErrorText)
        {
            sErrorCode = "-1";
            sErrorText = "Adding document to database failed.";

            if (document != null)
            {
                var documentStorage = (Document_Couch)document;

                byte[] btDocumentInfo;
                string sDocumentAttribute;
                bool bDocumentReplace = false;

                if (documentStorage.GetPropertyData(Document_Couch.DocumentReplace, out sDocumentAttribute))
                {
                    bDocumentReplace = Convert.ToBoolean(sDocumentAttribute);
                    documentStorage.ClearPropertyData(Document_Couch.DocumentReplace);
                }

                if (string.IsNullOrEmpty(document.FileId))
                {
                    sErrorCode = "-4";
                    sErrorText = "Unique FileID is missing.";
                    return false;
                }


                if (documentStorage.GetSourceData(out btDocumentInfo))
                {
                    if (!couchConnector.Document_Add(bDocumentReplace, btDocumentInfo, documentStorage.FileId,
                                                     documentStorage.FilePath, documentStorage.FileName,
                                                     documentStorage.TypeName.ToString(),
                                                     documentStorage.ChildDocs, documentStorage.MetaData))
                    {
                        sErrorText = couchConnector.Message;
                    }
                    else
                    {
                        if (!couchConnector.Error)
                        {
                            documentStorage.SetPropertyData(Document_Couch.DocumentRevison, couchConnector.RevisionID);
                            sErrorCode = "0";
                        }
                        sErrorText = couchConnector.Message;
                    }
                }
                return true;
            }
            sErrorCode = "-2";
            sErrorText = "Database address information missing. [ServerAddress, ServerPort, DatabaseName]";
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="couchConnector"></param>
        /// <param name="document"></param>
        /// <param name="sErrorCode"></param>
        /// <param name="sErrorText"></param>
        /// <returns></returns>
        public bool SecuredAddDocument(
            SecuredCouchConnector couchConnector,
            ref Document document,
            out string sErrorCode,
            out string sErrorText)
        {
            sErrorCode = "-1";
            sErrorText = "Adding document to database failed.";

            if (document != null)
            {
                var documentStorage = (Document_Couch)document;

                byte[] btDocumentInfo;
                string sDocumentAttribute;
                bool bDocumentReplace = false;

                if (documentStorage.GetPropertyData(Document_Couch.DocumentReplace, out sDocumentAttribute))
                {
                    bDocumentReplace = Convert.ToBoolean(sDocumentAttribute);
                    documentStorage.ClearPropertyData(Document_Couch.DocumentReplace);
                }

                if (string.IsNullOrEmpty(document.FileId))
                {
                    sErrorCode = "-1";
                    sErrorText = "Unique FileID is missing.";
                    return false;
                }


                if (documentStorage.GetSourceData(out btDocumentInfo))
                {
                    if (!couchConnector.Document_Add(bDocumentReplace, btDocumentInfo, documentStorage.FileId,
                                                     documentStorage.FilePath, documentStorage.FileName,
                                                     documentStorage.TypeName.ToString(),
                                                     documentStorage.ChildDocs, documentStorage.MetaData))
                    {
                        sErrorText = couchConnector.Message;
                        sErrorCode = "-2";
                        return false;
                    }
                    if (!couchConnector.Error)
                    {
                        documentStorage.SetPropertyData(Document_Couch.DocumentRevison, couchConnector.RevisionID);
                        sErrorCode = "0";
                        return true;
                    }
                    sErrorCode = "-3";
                    sErrorText = couchConnector.Message;
                    return false;
                }
            }
            sErrorCode = "-4";
            sErrorText = "No data in document to add";
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessSettings"></param>
        /// <param name="document"></param>
        /// <param name="sErrorCode"></param>
        /// <param name="sErrorText"></param>
        /// <returns></returns>
        public bool GetDocument(
            DocumentAccess accessSettings,
            ref Document document,
            out string sErrorCode,
            out string sErrorText)
        {
            sErrorCode = "-1";
            sErrorText = "Retrieving document failed.";

            if (accessSettings != null &&
                document != null)
            {
                if (!string.IsNullOrEmpty(accessSettings.SystemAddress) &&
                    !string.IsNullOrEmpty(accessSettings.AddressPort) &&
                    !string.IsNullOrEmpty(accessSettings.DatabaseName))
                {
                    var couchConnector = new CouchConnector(accessSettings.SystemAddress,
                                                            accessSettings.AddressPort,
                                                            accessSettings.DatabaseName);
                    return (GetDocument(couchConnector, ref document, out sErrorCode, out sErrorText));
                }
            }
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="couchConnector"></param>
        /// <param name="document"></param>
        /// <param name="sErrorCode"></param>
        /// <param name="sErrorText"></param>
        /// <returns></returns>
        public bool GetDocument(
            CouchConnector couchConnector, 
            ref Document document, 
            out string sErrorCode, 
            out string sErrorText)
        {
            sErrorCode = "-1";
            sErrorText = "Retrieving document failed.";

            if (document != null)
            {
                IDictionary objParms = couchConnector.GetDocument(document.FileId);

                if (!couchConnector.Error)
                {
                    foreach (DictionaryEntry objParm in objParms)
                    {
                        string sKey = objParm.Key.ToString();
                        string sValue = objParm.Value.ToString();

                        switch (sKey)
                        {
                            case "_id":
                                document.FileId = sValue;
                                break;
                            case "_rev":
                                document.SetPropertyData(Document_Couch.DocumentRevison, sValue);
                                break;
                            case "FileData":
                                document.FileData = sValue;
                                break;
                            case "FileName":
                                document.FileName = sValue;
                                break;
                            case "FilePath":
                                document.FilePath = sValue;
                                break;
                            case "FileType":
                                var typeName = (Document.DocTypeNames)Enum.Parse(typeof(Document.DocTypeNames), sValue);
                                document.TypeName = typeName;
                                break;
                            case "ChildDocuments":
                                var lstChildDocs = new List<string>();
                                if(!string.IsNullOrEmpty(sValue))
                                {
                                    string[] arChildDocs = sValue.Split(',');
                                    if(arChildDocs.Length > 0)
                                        lstChildDocs.AddRange(arChildDocs);
                                }
                                document.ChildDocs.AddRange(lstChildDocs);
                                break;
                            case Document_Couch.MetaTag:
                                document.AddMetaData(sValue);
                                break;
                            default:
                                document.SetPropertyData(sKey, sValue);
                                break;
                        }
                    }
                    sErrorCode = "0";
                }
                sErrorText = couchConnector.Message;
                return true;
            }
            return false;
        }

        /// <summary>
        /// <param name="couchConnector"></param>
        /// <param name="document"></param>
        /// <param name="sErrorCode"></param>
        /// <param name="sErrorText"></param>
        /// <param name="liteFetch"> </param>
        /// <returns></returns>
        /// </summary>
        public bool SecuredGetDocument(
            SecuredCouchConnector couchConnector,
            ref Document document,
            out string sErrorCode,
            out string sErrorText,
            bool liteFetch = false)
        {
            sErrorCode = "-1";
            sErrorText = "Retrieving document failed.";

            if (document != null)
            {
                IDictionary objParms = couchConnector.GetDocument(document.FileId, liteFetch);

                if (!couchConnector.Error)
                {
                    foreach (DictionaryEntry objParm in objParms)
                    {
                        string sKey = objParm.Key.ToString();
                        string sValue = objParm.Value.ToString();

                        switch (sKey)
                        {
                            case "_id":
                                document.FileId = sValue;
                                break;
                            case "_rev":
                                document.SetPropertyData(Document_Couch.DocumentRevison, sValue);
                                break;
                            case "FileData":
                                document.FileData = sValue;
                                break;
                            case "FileName":
                                document.FileName = sValue;
                                break;
                            case "FilePath":
                                document.FilePath = sValue;
                                break;
                            case "FileType":
                                var typeName = (Document.DocTypeNames)Enum.Parse(typeof(Document.DocTypeNames), sValue);
                                document.TypeName = typeName;
                                break;
                            case "ChildDocuments":
                                var lstChildDocs = new List<string>();
                                if (!string.IsNullOrEmpty(sValue))
                                {
                                    string[] arChildDocs = sValue.Split(',');
                                    if (arChildDocs.Length > 0)
                                        lstChildDocs.AddRange(arChildDocs);
                                }
                                document.ChildDocs.AddRange(lstChildDocs);
                                break;
                            case Document_Couch.MetaTag:
                                document.AddMetaData(sValue);
                                break;
                            default:
                                document.SetPropertyData(sKey, sValue);
                                break;
                        }
                    }
                    sErrorCode = "0";
                }
                sErrorText = couchConnector.Message;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessSettings"></param>
        /// <param name="document"></param>
        /// <param name="sErrorCode"></param>
        /// <param name="sErrorText"></param>
        /// <returns></returns>
        public bool DeleteDocument(
            DocumentAccess accessSettings, 
            ref Document document, 
            out string sErrorCode, 
            out string sErrorText)
        {
            sErrorCode = "-1";
            sErrorText = "Deleting document failed.";

            if (accessSettings != null && document != null)
            {
                if (!string.IsNullOrEmpty(accessSettings.SystemAddress) && 
                    !string.IsNullOrEmpty(accessSettings.AddressPort) &&
                    !string.IsNullOrEmpty(accessSettings.DatabaseName))
                {
                    if (!string.IsNullOrEmpty(document.FileId))
                    {
                        var couchConnector = new CouchConnector(accessSettings.SystemAddress,
                                                                accessSettings.AddressPort,
                                                                accessSettings.DatabaseName);

                        if (couchConnector.Document_Delete(document.FileId))
                        {
                            if (!couchConnector.Error)
                                sErrorCode = "0";
                        }
                        sErrorText = couchConnector.Message;
                    }
                    else
                    {
                        sErrorCode = "-4";
                        sErrorText = "Unique ID is missing. [FileID]";
                        return true;
                    }
                }
                else
                {
                    sErrorCode = "-2";
                    sErrorText = "Database address information missing. [ServerAddress, ServerPort, DatabaseName]";
                }
                return false;
            }
            sErrorCode = "-3";
            sErrorText = "Database access settings missing.";
            return false;
        }
    }
}
