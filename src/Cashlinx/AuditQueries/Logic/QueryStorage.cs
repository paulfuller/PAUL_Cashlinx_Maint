using System;
using System.Collections.Generic;
using System.Linq;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Type;

namespace AuditQueries.Logic
{
    public class QueryStorage
    {
        class QueryStorageItem
        {
            private string queryTemplate;
            private readonly Dictionary<string, PairType<string, string>> queryParameters;
            private string queryName;
            private string queryDescription;

            public string QueryName
            {
                get
                {
                    return (this.queryName);
                }
            }

            public string QueryDescription
            {
                get
                {
                    return (this.queryDescription);
                }
            }

            public Dictionary<string, PairType<string,string>> QueryParameters
            {
                get
                {
                    return (this.queryParameters);
                }
            }


            public QueryStorageItem()
            {
                this.queryTemplate = string.Empty;
                this.queryParameters = new Dictionary<string, PairType<string, string>>();
                this.queryName = string.Empty;
                this.queryDescription = string.Empty;
            }

            public bool AddParameter(string name, string typeName, string initialValue)
            {
                if (string.IsNullOrEmpty(name) || 
                    string.IsNullOrEmpty(typeName))
                {
                    return (false);
                }

                if (CollectionUtilities.isNotEmptyContainsKey(this.queryParameters, name))
                {
                    //Parameter already exists, return false
                    return (false);
                }

                this.queryParameters.Add(name, new PairType<string, string>(typeName, initialValue));
                return(true);
            }

            public bool UpdateParameter(string name, string newValue)
            {
                if (string.IsNullOrEmpty(name) || 
                    CollectionUtilities.isEmpty(this.queryParameters))
                {
                    return (false);
                }

                bool rt = true;
                if (CollectionUtilities.isNotEmptyContainsKey(this.queryParameters, name))
                {
                    if (this.queryParameters[name].Left.Equals("string", StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.IsNullOrEmpty(newValue))
                        {
                            this.queryParameters[name].Right = string.Empty;
                        }
                        else
                        {
                            this.queryParameters[name].Right = newValue;
                        }
                    }
                    else if (!string.IsNullOrEmpty(newValue))
                    {
                        this.queryParameters[name].Right = newValue;
                    }
                    else
                    {
                        //Bad parameter value
                        //TODO: Log error and report exception
                        rt = false;
                    }
                }
                else
                {
                    //Could not find parameter
                    //TODO: Log error and report exception
                    rt = false;
                }

                return (rt);
            }

            public void SetQueryProperties(string template, string name, string desc)
            {
                this.queryTemplate = template;
                this.queryName = name;
                this.queryDescription = desc;
            }


            public string GetPopulatedQuery()
            {
                string rt = string.Empty;
                if (string.IsNullOrEmpty(this.queryTemplate) || CollectionUtilities.isEmpty(this.queryParameters))
                {
                    return (rt);
                }

                //Set the template
                rt = this.queryTemplate;

                //Inject the parameters
                foreach(var p in this.queryParameters)
                {
                    var repToken = @"?" + p.Key + @"?";
                    var pType = p.Value.Left;
                    string newVal;
                    if (pType.Equals(@"string", StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.IsNullOrEmpty(p.Value.Right) || string.IsNullOrWhiteSpace(p.Value.Right))
                        {
                            newVal = "null";
                        }
                        else
                        {
                            newVal = string.Format("'{0}'", p.Value.Right);    
                        }                        
                    }
                    else
                    {
                        newVal = p.Value.Right;
                    }
                    rt = rt.Replace(repToken, newVal);
                }

                //Return the populated query
                return (rt);
            }
        }

        private QueryStorageItem getStorageItem(int id)
        {
            if (CollectionUtilities.isNotEmptyContainsKey(this.queries, id))
            {
                return (this.queries[id]);
            }
            return (null);
        }

        private readonly Dictionary<int, QueryStorageItem> queries;

        public int Size()
        {
            return (queries.Count);
        }

        public QueryStorage()
        {
            this.queries = new Dictionary<int, QueryStorageItem>();
        }

        public IEnumerable<int> GetQueryIds()
        {
            return (this.queries.Keys);
        }

        public string GetQueryName(int id)
        {
            var qsi = getStorageItem(id);
            return ((qsi != null) ? qsi.QueryName : string.Empty);
        }

        public string GetQueryDesc(int id)
        {
            var qsi = getStorageItem(id);
            return ((qsi != null) ? qsi.QueryDescription : string.Empty);
        }

        public Dictionary<string, PairType<string,string>> GetQueryParams(int id)
        {
            var qsi = getStorageItem(id);
            return ((qsi != null) ? qsi.QueryParameters : null);
        }

        public string GetPopulatedQuery(int id)
        {
            var qsi = getStorageItem(id);
            return ((qsi != null) ? qsi.GetPopulatedQuery() : string.Empty);
        }
                
        public bool AddQuery(int id, string queryTemplate, string queryName, string queryDesc)
        {
            if (getStorageItem(id) != null)
            {
                return(false);
            }

            var qsi = new QueryStorageItem();
            qsi.SetQueryProperties(queryTemplate, queryName, queryDesc);
            this.queries.Add(id, qsi);
            return (true);
        }

        public bool AddQueryParameter(int id, string paramName, string paramTypeName, string paramInitialValue)
        {
            if (CollectionUtilities.isEmpty(this.queries) ||
                !this.queries.ContainsKey(id) ||
                string.IsNullOrEmpty(paramName) || 
                string.IsNullOrEmpty(paramTypeName))
            {
                return (false);
            }

            bool rt = true;
            QueryStorageItem qsi = getStorageItem(id);
            if (qsi == null || !qsi.AddParameter(paramName, paramTypeName, paramInitialValue))
            {
                //Could not add parameter to query
                //TODO: Log error and report exception
                rt = false;
            }
            return (rt);
        }

        public bool UpdateQueryParameter(int id, string paramName, string paramValue)
        {
            if (!this.queries.Keys.Contains(id) ||
                string.IsNullOrEmpty(paramName))
            {
                return (false);
            }

            bool rt = true;
            QueryStorageItem qsi = getStorageItem(id);
            if (qsi == null || !qsi.UpdateParameter(paramName, paramValue))
            {
                //Could not update a query parameter
                //TODO: Log error and report exception
                rt = false;
            }
            return (rt);
        }
    }
}
