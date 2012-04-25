/**********************************************************************************
 * Namespace:       PawnObjects.Doc
 * Class:           Document
 * 
 * Description      Abstract Document class providing the require functions for all
 *                  other classes to over-ride
 * 
 * History
 * David D Wise, Initial Development
 *   
 *************************************************************************************/

using System;
using System.Collections.Generic;

namespace Common.Libraries.Objects.Doc
{
    public abstract class Document: IDisposable
    {
        protected bool      _Initialized;
        protected object    _MutexObj;

        public const string DocumentRevison = "DocumentRevision";
        public const string MetaTag         = "MetaTag";
        public const string DocumentReplace = "DocumentReplace";

        public Dictionary<string, string>   MetaData    { get; private set; }   // Collection of Document attributes
        public string                       FileId      { get; set; }
        public string                       FileData    { get; set; }
        public string                       FilePath    { get; set; }
        public string                       FileName    { get; set; }           // Active Document's File Name
        public DocTypeNames                 TypeName    { get; set; }           // Active Document's File Type
        public List<string>                 ChildDocs   { get; private set; }

//        public enum TypeNames { BMP, DOC, DOCX, EMF, GIF, JPG, ICON, PDF, PNG, RTF, TIFF, WMF };
        public enum DocTypeNames { PDF = 0, RECEIPT, BARCODE, TEXT, BINARY, INVALID };

        public Document(string filePath, string fileName, DocTypeNames typeName)
        {
            _Initialized = false;

            if (_MutexObj == null)
                _MutexObj = new object();

            lock (_MutexObj)
            {
                MetaData = new Dictionary<string, string>();

                FileName = fileName;
                FilePath = filePath;
                TypeName = typeName;
                ChildDocs = new List<string>();
                _Initialized = true;
            }
        }

        public Document(string fileId)
        {
            _Initialized = false;

            if (_MutexObj == null)
                _MutexObj = new object();

            lock (_MutexObj)
            {
                MetaData = new Dictionary<string, string>();
                FileId = fileId;
                ChildDocs = new List<string>();
                _Initialized = true;
            }
        }

        /// <summary>
        /// Method to determine document byte[] size
        /// </summary>
        /// <returns></returns>
        public abstract long Length();
        /// <summary>
        /// Method to retrieve associated Child Documents attribute data from Document object
        /// </summary>
        /// <param name="data">Document attribute string value</param>
        /// <returns></returns>
        public abstract void GetChildDocuments(out List<string> data);
        /// <summary>
        /// Method to push associated Child Documents attribute data to Document object
        /// </summary>
        /// <param name="data">List of child documents</param>
        /// <returns></returns>
        public abstract void SetChildDocuments(List<string> data);
        /// <summary>
        /// Method to clear child documents (if exist) from the Document object
        /// </summary>
        /// <returns></returns>
        public abstract void ClearChildDocuments();
        /// <summary>
        /// Method to allow application to retrieve the Document binary data from Document object
        /// </summary>
        /// <param name="data">Document byte[] data</param>
        /// <returns></returns>
        public abstract bool GetSourceData(out byte[] data);
        /// <summary>
        /// Method to allow application to push the Document binary data to Document object
        /// </summary>
        /// <param name="data">Document byte[] data</param>
        /// <param name="documentID">Unique Document ID [if applicable]</param>
        /// <param name="overwrite">Boolean indicating if to over-ride existing data</param>
        /// <returns></returns>
        public abstract bool SetSourceData(byte[] data, string documentID, bool overwrite);
        /// <summary>
        /// Method to clear the document binary data from Document object
        /// </summary>
        /// <param name="overwrite">Unique reference to document attribute</param>
        /// <returns></returns>
        public abstract bool ClearSourceData(bool overwrite);
        /// <summary>
        /// Method to allow application to retrieve associated Document attribute data from Document object
        /// </summary>
        /// <param name="key">Unique reference to document attribute</param>
        /// <param name="data">Document attribute string value</param>
        /// <returns></returns>
        public abstract bool GetPropertyData(string key, out string data);
        /// <summary>
        /// Method to allow application to push associated Document attribute data to Document object
        /// </summary>
        /// <param name="key">Unique reference to document attribute</param>
        /// <param name="data">Document attribute string value</param>
        /// <returns></returns>
        public abstract bool SetPropertyData(string key, string data);
        /// <summary>
        /// Method to clear a document attribute (if exist) from the Document object
        /// </summary>
        /// <param name="key">Boolean indicating to over-ride existing data only</param>
        /// <returns></returns>
        public abstract bool ClearPropertyData(string key);
        /// <summary>
        /// Method to add Meta Tag values to Document object
        /// </summary>
        /// <param name="metaValue"></param>
        public abstract void AddMetaData(string metaValue);
        /// <summary>
        /// Method to retrieve Meta Tag values from Document object
        /// </summary>
        /// <returns></returns>
        public abstract string GetMetaData();

        public void Dispose()
        {
            if (_Initialized == false)
                return;

            lock (_MutexObj)
            {
                MetaData = null;
                _Initialized = false;
                GC.Collect();
            }
            //Null out mutex
            _MutexObj = null;
        }
    }
}
