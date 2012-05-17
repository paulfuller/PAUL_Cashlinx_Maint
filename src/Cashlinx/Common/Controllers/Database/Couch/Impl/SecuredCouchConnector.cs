using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Common.Controllers.Application;
using Common.Controllers.Database.Couch.CouchSecurity;
using Common.Controllers.Network;
using Common.Controllers.Security;
using JsonFx.Json;

namespace Common.Controllers.Database.Couch.Impl
{
   public class SecuredCouchConnector:CouchConnector
    {
        //Security implementation
        private readonly string _userId = String.Empty;
        private readonly string _password = String.Empty;
        private readonly string _dbName = String.Empty;
        private readonly string _connUri = string.Empty;

        private bool addRetryCompleted = false;

        public SecuredCouchConnector(string sWebDomain, string sWebPort,string secWebPort, string sCouchDb,
        string userId, string password,bool connSecured)
        : base(sWebDomain, sWebPort, sCouchDb)
        {
            if (string.IsNullOrEmpty(sWebDomain) || string.IsNullOrEmpty(sWebPort) ||  string.IsNullOrEmpty(secWebPort) || string.IsNullOrEmpty(sCouchDb) ||
                string.IsNullOrEmpty(userId)|| string.IsNullOrEmpty(password))
            {
                throw (new ArgumentNullException("SecuredCouchConnector Couch DB Connection Error:Argument Error")); 
            }

            this._userId = userId;

            this._password = password;
            this._dbName = sCouchDb;
            if (!connSecured)
                _connUri = String.Format("http://{0}:{1}/", sWebDomain, sWebPort);
            else
                _connUri = String.Format("https://{0}:{1}/", sWebDomain, secWebPort);

        }


        public Cookie GetCouchSession()
        {
            var security = new PawnCouchSecurity(_connUri, _userId, _password);
            return security.GetSession();
        }

        public new bool Document_Add(bool bOverride,
        byte[] btData,
        string sDocumentId,
        string sFilePath,
        string sFileName,
        string sDocumentType,
        List<string> lstChildDocuments,
        IDictionary objParms)
        {
            return DocumentAdd(bOverride,btData, sDocumentId, sFilePath, sFileName, sDocumentType, lstChildDocuments, objParms,false);
        }

       

       public new IDictionary GetDocument(string sDocumentId, bool liteFetch = false)
        {
            Error = false;
            Message = "";
            IDictionary objParms = null;
            try
            {
                byte[] btData = DocumentGet(sDocumentId,false,liteFetch);

                if (btData != null)
                {
                    string sResult = ByteArrayToString(btData);
                    objParms = (IDictionary)JsonReader.Deserialize(sResult);
                }
            }
            catch (Exception exp)
            {
                Error = true;
                Message = exp.Message;
            }
            return objParms;
       }
       
       public override bool Document_Delete(string sDocumentId)
       {
           Error = false;
           Message = "";

           return DeleteDocument(sDocumentId,false);
       }

       /**
        * Below method added for testing purpose only not to be used from the use cases
        * will cause performance issues, access to be changed
        ***/
       public IDictionary<string, object>[] doTestGetALL()
       {
           return GetALLDocuments();
       }

       private IDictionary<string, object>[] GetALLDocuments()
       {
           Error = false;
           Message = "";
           IDictionary<string, object>[] objParms = null;
           byte[] btData = null;
           HttpWebRequest _WebRequester = null;
           try
           {
               string sGetURL = _connUri + _dbName + "/" + "_all_docs";
               try
               {

                   _WebRequester = CreateRequest(sGetURL, true).Get().Json().GetRequest();

               }
               catch (Exception e)
               {
                   Error = true;
                   Message = "Document get all aborted:" + sGetURL + ":" + e.Message;
                   return objParms;
               }

               using (HttpWebResponse response = (HttpWebResponse)_WebRequester.GetResponse())
               {
                   DocGetErrAndRetry(response.ResponseUri.ToString(), "ALL");
                   if (Error == true)
                       return objParms;
                       

                   // Get the stream.
                   using (Stream stream = response.GetResponseStream())
                   {
                       // Use the ReadFully method from the link above:
                       btData = ReadFully(stream, response.ContentLength);
                   }
               }

               if (btData != null)
               {
                   string sResult = this.ByteArrayToString(btData);
                   IDictionary objRawParms = (IDictionary)JsonReader.Deserialize(sResult);

                   if (objRawParms != null)
                   {
                       if (objRawParms["total_rows"] != null)
                       {
                           int iTotalRows = Convert.ToInt32(objRawParms["total_rows"]);
                           if (iTotalRows > 0)
                           {
                               objParms = (IDictionary<string, object>[])objRawParms["rows"];
                           }else
                           {
                               Error = true;
                               Message = "No data available";
                           }
                       }
                   }
               }
           }
           catch (Exception exp)
           {
               Error = true;
               Message = exp.Message;
           }
           return objParms;
       }

       private bool DeleteDocument(string sDocumentId,bool isRetry)
       {
           try
           {
               IDictionary iDictionary = GetDocument(sDocumentId);
               HttpWebRequest _WebRequester = null;
               if (iDictionary != null)
               {
                   //DELETE http://localhost:5984/example/some_doc?rev=1582603387
                    string sGetURL = _connUri + _dbName + "/" + sDocumentId + "?rev=" + iDictionary["_rev"];
                   try
                   {
                       if (isRetry)
                       {
                           _WebRequester = CreateRequest(sGetURL, true).Delete().Json().GetRequest();
                       }
                       else
                       {
                           _WebRequester = CreateRequest(sGetURL, false).Delete().Json().GetRequest();
                       }
                   }
                   catch (Exception e)
                   {
                       Error = true;
                       Message = "Document delete aborted:" + sGetURL + ":" + e.Message + " : DocID :" + sDocumentId;
                       return true;
                   }
                   using (HttpWebResponse response = (HttpWebResponse)_WebRequester.GetResponse())
                   {

                       if (DocGetErrAndRetry(response.ResponseUri.ToString(), sDocumentId))
                       {
                           if ((!isRetry))
                           {
                              // Console.WriteLine("Doing re-try");
                               return DeleteDocument(sDocumentId, true); //perform re-try since session expired unexpectedly
                           }
                           else
                           {
                               return false; //return error since re-try also failed
                           }
                       }
                       // Get the stream.
                       if(response.StatusCode==HttpStatusCode.OK)
                       {
                           Message = "Document deleted.";
                       }
                }
               }else
               {
                   Message = "Specified Document not found,delete aborted.";
               }
           }
           catch (Exception exp)
           {
               Error = true;
               Message = exp.Message;
               /*if(DocGetErrAndRetry(exp.Message,sDocumentId))
               {
                   
               }*/
               return false;
           }
           return true;
       }


       private bool DocumentAdd(bool bOverride, byte[] btData, string sDocumentId, string sFilePath, string sFileName,
           string sDocumentType, List<string> lstChildDocuments, IDictionary objParms, bool isRetry)
       {
           DocumentData = btData;
           DocumentID = sDocumentId;
           string sPostURL = string.Format("{0}{1}/{2}", this._connUri, this._dbName, sDocumentId);
           Error = false;
           Message = string.Empty;
           var _WebResult = string.Empty;
           IDictionary iDictionary = null;
           try
           {
               //document override has been removed since delete is avoided
               if (bOverride)
               {
                   Document_Delete(sDocumentId);
                   //Console.WriteLine(string.Format("Override is true deleting doc {0} before adding"),sDocumentId);
               }
               List<KeyValuePair<string, object>> keyPairs = GetKeyPairs(btData, sDocumentId, sFilePath, sFileName, sDocumentType, lstChildDocuments);
               AddObjectParams(objParms, keyPairs);
               string sJSON = ListToJSON(keyPairs);
               byte[] bytes = Encoding.ASCII.GetBytes(sJSON);
               HttpWebRequest webRequester = null;
               try
               {
                   if (isRetry)
                   {
                       webRequester = CreateRequest(sPostURL, true).Put().GetRequest();
                   }
                   else
                   {
                       webRequester = CreateRequest(sPostURL, false).Put().GetRequest();
                   }
               }
               catch (Exception e)
               {
                   Error = true;
                   Message = string.Format("Document add could not be completed for '{0}' due to '{1}' post url {2}",
                       sDocumentId, e.Message, sPostURL);
                   return true;
               }

               try
               {
                   var start = DateTime.Now;
                   webRequester.ContentLength = bytes.Length;
                   var writer = webRequester.GetRequestStream();
                   long length = bytes.Length;
                   writer.Write(bytes, 0, bytes.Length);
                   var nStr = new string(Encoding.ASCII.GetChars(bytes));
                   //FileLogger.Instance.logMessage(LogLevel.DEBUG, "CouchConnector", "PUT Data: " + Environment.NewLine + nStr);
                   var transStart = DateTime.Now;
                   writer.Flush();
                   writer.Close();
                   var _WebResponser = (HttpWebResponse)webRequester.GetResponse();
                   //Check out the html.    

                   using (var sr = new StreamReader(_WebResponser.GetResponseStream()))
                   {
                       _WebResult = sr.ReadToEnd();
                       iDictionary = (IDictionary)JsonReader.Deserialize(_WebResult);
                       if (iDictionary != null)
                       {
                           RevisionID = (string)iDictionary["rev"];
                           Message = "Document added.";
                       }
                       // Close and clean up the StreamReader       
                       sr.Close();
                   }
                   DateTime transEnd = DateTime.Now;
                   //addRetryCompleted = false; //reset the re-try flag after completion
                   DateTime end = DateTime.Now;

                   var encCfg = SecurityAccessor.Instance.EncryptConfig;
                   if (encCfg.ClientConfig.ClientConfiguration.CPNHSEnabled)
                   {
                       CPNHSController.Instance.AddDataTransOut(
                           GlobalDataAccessor.Instance.DesktopSession.UserName,
                           length,
                           (long)Math.Ceiling((transEnd - transStart).TotalMilliseconds),
                           (long)Math.Ceiling((end - start).TotalMilliseconds), true);
                   }
               }
               catch (Exception ex)
               {
                   string msg = string.Empty;
                   // Perform single re-try incase session or access
                   if ((DocAddErrReasonAndRetry(ex.Message, sDocumentId)) && !isRetry)
                   {
                       // docAddRetry(btData, sDocumentId, sFilePath, sFileName, sDocumentType, lstChildDocuments, objParms, docIDStr, msg, bOverride);
                       //addRetryCompleted = true;//set the re-try to true to get new session
                      // Console.WriteLine(String.Format("Doing re-try for '{0}' due to error '{1}'", sDocumentId, Message));
                       DocumentAdd(bOverride, btData, sDocumentId, sFilePath, sFileName, sDocumentType,
                                    lstChildDocuments, objParms, true);//call the same method to re-try
                       if (!Error)
                       {
                           Message = "Document added.";
                       }
                       /*else
                       {
                          normally not executed
                           Message = msg;
                       }*/
                   }
                   else
                   {

                       //Console.WriteLine("Failed again.................." + Message +  PawnCouchSecurity.cookiestore["authcookie"]);
                       Error = true;
                       //Message = msg;
                   }
                   return true;
               }
           }
           catch (Exception exp)
           {
               //Console.WriteLine("Failed again..................1111" + Message + PawnCouchSecurity.cookiestore["authcookie"]);
               Error = true;
               Message = exp.Message;
           }

           return true;
       }

       private void docAddRetry(byte[] btData, string sDocumentId, string sFilePath, string sFileName, string sDocumentType, List<string> lstChildDocuments, IDictionary objParms, string docIDStr, string msg, bool bOverride)
       {
           addRetryCompleted = true;//set the re-try to true to get new session
           //Console.WriteLine(String.Format("Doing re-try for '{0}' due to error '{1}'",sDocumentId,msg));
           Document_Add(bOverride, btData, sDocumentId, sFilePath, sFileName, sDocumentType,
                        lstChildDocuments, objParms);//call the same method to re-try
           if(!Error)
           {
               Message = "Document added.";  
           }else
           {
               //normally not executed
               Message = "Error while posting web request, document add failed even after re-try" + docIDStr + ":" + " Reason :" +
                         msg;
           }
       }

       private PawnCouchRequest CreateRequest(string postUrl,bool useNewSession)
       {
            var security = new PawnCouchSecurity(_connUri, _userId, _password);
            PawnCouchRequest request = security.GetRequest(postUrl, useNewSession);
            return request;
       }

       private bool DocAddErrReasonAndRetry(string msg,string docId)
        {
            if (msg.Contains("404"))
            {
                Message = String.Format("Specified DB '{0}' not found ", _dbName);
                return false;
            }
            else if (msg.Contains("405"))
            {
                Message = String.Format("Specified user '{0}' does not have access to DB '{1}' or session is invalid", _userId, _dbName);
                return true;
            }
            else if (msg.Contains("409"))
            {
                Message = String.Format("Document with same id '{0}' already exists in DB '{1}'", docId, _dbName);
                return false;
            }
            else
            {
                //Console.WriteLine("#########################"+msg);
                Message = msg;
                return true;
            }
        }
        
       private static void AddObjectParams(IDictionary objParms, List<KeyValuePair<string, object>> keyPairs)
        {
            KeyValuePair<string, object> keyValuePair;
            if (objParms != null)
            {
                foreach (DictionaryEntry objParm in objParms)
                {
                    string sKey = objParm.Key.ToString();
                    string sValue = objParm.Value.ToString();

                    if (sKey == "_id" ||
                        sKey == "FilePath" ||
                        sKey == "FileName" ||
                        sKey == "FileData" ||
                        sKey == "FileType" ||
                        sKey == "ChildDocuments")
                        continue;

                    keyValuePair = new KeyValuePair<string, object>(sKey, sValue);
                    keyPairs.Add(keyValuePair);
                }
            }
           //add date/time by default
           keyValuePair = new KeyValuePair<string, object>("CDate", DateTime.Now);
           keyPairs.Add(keyValuePair);
        }

       private static List<KeyValuePair<string, object>> GetKeyPairs(byte[] btData, string sDocumentId, string sFilePath, string sFileName, string sDocumentType, List<string> lstChildDocuments)
        {
            var keyPairs = new List<KeyValuePair<string, object>>();
            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>("_id", sDocumentId);
            keyPairs.Add(keyValuePair);
            keyValuePair = new KeyValuePair<string, object>("FilePath", sFilePath);
            keyPairs.Add(keyValuePair);
            keyValuePair = new KeyValuePair<string, object>("FileName", sFileName);
            keyPairs.Add(keyValuePair);
            keyValuePair = new KeyValuePair<string, object>("FileData", Convert.ToBase64String(btData));
            keyPairs.Add(keyValuePair);
            keyValuePair = new KeyValuePair<string, object>("FileType", sDocumentType);
            keyPairs.Add(keyValuePair);

            string sChildDocuments = "";
            if (lstChildDocuments.Count > 0)
                sChildDocuments = string.Join(",", lstChildDocuments.ToArray());

            keyValuePair = new KeyValuePair<string, object>("ChildDocuments", sChildDocuments);
            keyPairs.Add(keyValuePair);
            return keyPairs;
        }

       private byte[] DocumentGet(string sDocumentId,bool isRetry, bool liteFetch = false)
       {
            Error = false;
            Message = string.Empty;
            byte[] btData = null;
            HttpWebRequest webRequester = null;
            try
            {
                DateTime start = DateTime.Now;
                var sGetURL = string.Format("{0}{1}/{2}", this._connUri, this._dbName, sDocumentId);
                try
                {
                    if (isRetry)
                    {
                        webRequester = CreateRequest(sGetURL, true).Get().Json().GetRequest();
                    }
                    else
                    {
                        webRequester = CreateRequest(sGetURL, false).Get().Json().GetRequest();
                    }
                }
                catch (Exception e)
                {
                    Error = true;
                    Message = string.Format("Document get aborted:{0}:{1} : DocID :{2}", sGetURL, e.Message, sDocumentId);
                    return btData;
                }
                using (var response = (HttpWebResponse)webRequester.GetResponse())
                {

                    if(DocGetErrAndRetry(response.ResponseUri.ToString(), sDocumentId))
                    {
                        if ((!isRetry))
                        {
                            //Console.WriteLine("Doing re-try");
                            btData= DocumentGet(sDocumentId,true, liteFetch); //perform re-try since session expired unexpectedly
                            return btData;
                        }
                        else
                        {
                            return btData; //return error since re-try also failed
                        }
                    }
                    // Get the stream.
                    DateTime transStart = DateTime.Now;
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use the ReadFully method from the link above:
                        btData = ReadFully(stream, response.ContentLength);
                        Message = "Document retrieved.";
                    }
                    DateTime transEnd = DateTime.Now;

                    if (SecurityAccessor.Instance != null)
                    {
                        var encCfg = SecurityAccessor.Instance.EncryptConfig;
                        if (encCfg != null &&
                            encCfg.ClientConfig != null &&
                            encCfg.ClientConfig.ClientConfiguration != null &&
                            encCfg.ClientConfig.ClientConfiguration.CPNHSEnabled)
                        {
                            CPNHSController.Instance.AddDataTransIn(
                                GlobalDataAccessor.Instance.DesktopSession.UserName,
                                response.ContentLength,
                                (long)Math.Ceiling((transEnd - transStart).TotalMilliseconds),
                                (long)Math.Ceiling((transEnd - start).TotalMilliseconds), true);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                DocGetErrAndRetry(exp.Message, sDocumentId);
            }
            return btData;
        }

       private bool DocGetErrAndRetry(string respExp,string docID)
       {
           if ((respExp != null) && (respExp.ToLower().Contains("not authorized")))
           {
               Error = true;
               Message = String.Format("Specified user '{0}' does not have access to DB '{1}' for document '{2}' or session is invalid", _userId, _dbName, docID);
               return true; //do re-try
           }
           else if ((respExp != null) && respExp.Contains("404"))
           {
               Error = true;
               Message = String.Format("Specified document '{0}' not found in DB '{1}'", docID,_dbName);
               return false; //No re-try
           }else
           {
               return false;
           }
          
       }

    }
}
