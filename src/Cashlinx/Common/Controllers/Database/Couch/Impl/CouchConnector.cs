/**********************************************************************************
 * Namespace:       PawnDataAccess.Couch.Impl
 * Class:           CouchConnector
 * 
 * Description      Handles the detailed steps in accessing CouchDB, parsing JSON,
 *                  and establishing the associated Request/Response properties
 * 
 * History
 * David D Wise, Initial Development
 *   
 *************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using JsonFx.Json;

namespace Common.Controllers.Database.Couch.Impl
{
    public class CouchConnector
    {
        private const int INITIAL_LENGTH = 32768;
        private const int MIN_LENGTH = 1;
        public byte[] DocumentData { get; set; }
        public string DocumentID { get; set; }
        public string RevisionID { get; set; }
        public string WebURL { get; private set; }
        public bool Error { get; set; }
        public string Message { get; set; }

        public CouchConnector(string sWebDomain, string sWebPort, string sCouchDB)
        {
            WebURL = String.Format("http://{0}:{1}/{2}/", sWebDomain, sWebPort, sCouchDB);
        }

        protected string ListToJSON(List<KeyValuePair<string, object>> keyPairs)
        {
            string sJSON = "{ ";

            foreach (KeyValuePair<string, object> keyValuePair in keyPairs)
            {
                sJSON += "\"" + keyValuePair.Key + "\"";
                sJSON += " : ";
                sJSON += "\"" + keyValuePair.Value + "\", ";
            }

            sJSON = sJSON.Substring(0, sJSON.Length - 2);
            sJSON += " }";
            return sJSON;
        }

        public bool Document_Add(bool bOverride,
            byte[] btData,
            string sDocumentId,
            string sFilePath,
            string sFileName,
            string sDocumentType,
            List<string> lstChildDocuments,
            IDictionary objParms)
        {
            //PUT /example/some_doc_id HTTP/1.0
            //Content-Length: 29
            //Content-Type: application/json
            //{"greetings":"Hello, World!"} 

            DocumentData = btData;
            DocumentID = sDocumentId;

            try
            {
                if (bOverride)
                    Document_Delete(sDocumentId);

                Error = false;
                Message = "";

                var keyPairs = new List<KeyValuePair<string, object>>();

                string sPostURL = WebURL + sDocumentId;

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

                var sChildDocuments = string.Empty;
                if (lstChildDocuments.Count > 0)
                    sChildDocuments = string.Join(",", lstChildDocuments.ToArray());

                keyValuePair = new KeyValuePair<string, object>("ChildDocuments", sChildDocuments);
                keyPairs.Add(keyValuePair);

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

                string sJSON = ListToJSON(keyPairs);

                byte[] bytes = Encoding.ASCII.GetBytes(sJSON);

                HttpWebRequest _WebRequester = (HttpWebRequest)WebRequest.Create(sPostURL);
                _WebRequester.Method = "PUT";
                _WebRequester.ContentLength = bytes.Length;
                _WebRequester.ContentType = "application/json";

                Stream _Writer = _WebRequester.GetRequestStream();
                _Writer.Write(bytes, 0, bytes.Length);
                String nStr = new string(Encoding.ASCII.GetChars(bytes));
                //FileLogger.Instance.logMessage(LogLevel.DEBUG, "CouchConnector", "PUT Data: " + Environment.NewLine + nStr);
                _Writer.Flush();
                _Writer.Close();

                var _WebResponser = (HttpWebResponse)_WebRequester.GetResponse();

                //Check out the html.    
                using (StreamReader sr = new StreamReader(_WebResponser.GetResponseStream()))
                {
                    var _WebResult = sr.ReadToEnd();
                    var iDictionary = (IDictionary)JsonReader.Deserialize(_WebResult);
                    if (iDictionary != null)
                    {
                        RevisionID = (string)iDictionary["rev"];
                        Message = "Document added.";
                    }
                    // Close and clean up the StreamReader       
                    sr.Close();
                }
            }
            catch (Exception exp)
            {
                Error = true;
                Message = exp.Message;
            }

            return true;
        }

        public virtual bool Document_Delete(string sDocumentId)
        {
            Error = false;
            Message = "";

            try
            {
                IDictionary iDictionary = GetDocument(sDocumentId);

                if (iDictionary != null)
                {
                    //DELETE http://localhost:5984/example/some_doc?rev=1582603387
                    string sGetURL = WebURL + sDocumentId + "?rev=" + iDictionary["_rev"];

                    HttpWebRequest _WebRequester = (HttpWebRequest)WebRequest.Create(sGetURL);
                    _WebRequester.ContentType = "application/json";
                    _WebRequester.Method = "DELETE";

                    using (HttpWebResponse response = (HttpWebResponse)_WebRequester.GetResponse())
                    {
                        // Get the stream.
                        using (Stream stream = response.GetResponseStream())
                        {
                            // Use the ReadFully method from the link above:
                            byte[] btData = ReadFully(stream, response.ContentLength);
                            Message = "Document deleted.";
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Error = true;
                Message = exp.Message;
                return false;
            }
            return true;
        }

        private byte[] Document_Get(string sDocumentId, bool liteFetch = false)
        {
            Error = false;
            Message = "";
            byte[] btData = null;

            try
            {
                //Get http://localhost:5984/example/some_doc_id
                string sGetURL = WebURL + sDocumentId;
                HttpWebRequest _WebRequester;
                if (!liteFetch)
                {
                    _WebRequester = (HttpWebRequest)WebRequest.Create(sGetURL);
                    _WebRequester.ContentType = "application/json";
                    _WebRequester.Method = "GET";
                }
                else
                {
                    _WebRequester = (HttpWebRequest)WebRequest.Create(sGetURL);
                    _WebRequester.ContentType = "application/json";
                    _WebRequester.Method = "HEAD";
                    
                }
                using (var response = (HttpWebResponse)_WebRequester.GetResponse())
                {
                    // Get the stream.
                    using (var stream = response.GetResponseStream())
                    {
                        // Use the ReadFully method from the link above:
                        btData = ReadFully(stream, response.ContentLength);
                        Message = "Document retrieved.";
                    }
                }
            }
            catch (Exception exp)
            {
                Error = true;
                Message = exp.Message;
            }
            return btData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDocumentId"></param>
        /// <returns></returns>
        public IDictionary GetDocument(string sDocumentId)
        {
            Error = false;
            Message = "";
            IDictionary objParms = null;

            try
            {
                byte[] btData = Document_Get(sDocumentId);

                if (btData != null)
                {
                    string sResult = this.ByteArrayToString(btData);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object>[] GetDocuments()
        {
            Error = false;
            Message = "";
            IDictionary<string, object>[] objParms = null;
            byte[] btData = null;

            try
            {
                //Get http://localhost:5984/example/
                string sGetURL = WebURL + "_all_docs";

                HttpWebRequest _WebRequester = (HttpWebRequest)WebRequest.Create(sGetURL);
                _WebRequester.ContentType = "application/json";
                _WebRequester.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)_WebRequester.GetResponse())
                {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="btData"></param>
        /// <returns></returns>
        protected string ByteArrayToString(byte[] btData)
        {
            Error = false;
            Message = "";
            var sStringData = string.Empty;

            try
            {
                var enc = Encoding.ASCII;
                sStringData = enc.GetString(btData);
            }
            catch (Exception exp)
            {
                Error = true;
                Message = exp.Message;
            }
            return sStringData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="initialLength"></param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream stream, long initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < MIN_LENGTH)
            {
                initialLength = INITIAL_LENGTH;
            }

            byte[] buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            // Buffer is now too big. Shrink it.
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }

    }

}
