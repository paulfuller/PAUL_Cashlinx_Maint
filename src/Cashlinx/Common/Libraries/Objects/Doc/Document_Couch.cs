/**********************************************************************************
 * Namespace:       PawnObjects.Doc.Couch
 * Class:           Document_Couch
 * 
 * Description      Class override of Document for CouchDB Document Access Actions
 * 
 * History
 * David D Wise, Initial Development
 *   
 *************************************************************************************/

using System;
using System.Collections.Generic;

namespace Common.Libraries.Objects.Doc
{
    public class Document_Couch: Document
    {
        public Document_Couch(string filePath, string FileName, DocTypeNames typeName)
            : base(filePath, FileName, typeName)
        {
        }

        public Document_Couch(string fileId)
            : base(fileId)
        {
        }

        public override long Length()
        {
            long lDocumentLength = 0;

            if (!string.IsNullOrEmpty(FileData))
            {
                byte[] btData = Convert.FromBase64String(FileData);
                lDocumentLength = btData.LongLength;
            }
            return lDocumentLength;
        }

        public override void GetChildDocuments(out List<string> data)
        {
            lock (_MutexObj)
            {
                data = ChildDocs;
            }
        }

        public override void SetChildDocuments(List<string> data)
        {
            lock (_MutexObj)
            {
                ChildDocs.Clear();
                ChildDocs.AddRange(data);
            }
        }

        public override void ClearChildDocuments()
        {
            lock (_MutexObj)
            {
                ChildDocs.Clear();
            }
        }

        public override bool GetSourceData(out byte[] data)
        {
            data = null;
            lock (_MutexObj)
            {
                if (FileData != null)
                {
                    data = Convert.FromBase64String(FileData);
                }
            }
            return true;
        }

        public override bool SetSourceData(byte[] data, string documentId, bool overwrite)
        {
            lock (_MutexObj)
            {
                FileData = data == null ? null : Convert.ToBase64String(data);
                FileId = documentId;

                if (MetaData.ContainsKey(DocumentReplace))
                {
                    MetaData[DocumentReplace] = overwrite.ToString();
                }
                else
                {
                    MetaData.Add(DocumentReplace, overwrite.ToString());
                }
            }
            return true;
        }

        public override bool ClearSourceData(bool overwrite)
        {
            lock (_MutexObj)
            {
                SetPropertyData(DocumentReplace, overwrite.ToString());
                FileData = null;
            }
            return true;
        }

        public override bool GetPropertyData(string key, out string data)
        {
            data = null;
            lock (_MutexObj)
            {
                if (MetaData.ContainsKey(key))
                {
                    if (MetaData[key] != null)
                    {
                        data = MetaData[key];
                    }
                }
            }
            return true;
        }

        public override bool SetPropertyData(string key, string data)
        {
            lock (_MutexObj)
            {
                if (MetaData.ContainsKey(key))
                {
                    MetaData[key] = data;
                }
                else
                {
                    MetaData.Add(key, data);
                }
            }
            return true;
        }

        public override bool ClearPropertyData(string key)
        {
            lock (_MutexObj)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    if (MetaData.ContainsKey(key))
                    {
                        MetaData.Remove(key);
                    }
                }
                else
                {
                    MetaData.Clear();
                }
            }
            return true;
        }

        public override void AddMetaData(string metaValue)
        {
            lock (_MutexObj)
            {
                if (MetaData.ContainsKey(MetaTag))
                {
                    MetaData[MetaTag] = metaValue;
                }
                else
                {
                    MetaData.Add(MetaTag, metaValue);
                }
            }
        }

        public override string GetMetaData()
        {
            string sMetaKey = "";
            lock (_MutexObj)
            {
                if (MetaData.ContainsKey(MetaTag))
                {
                    sMetaKey = MetaData[MetaTag];
                }
            }
            return sMetaKey;
        }

    }
}
