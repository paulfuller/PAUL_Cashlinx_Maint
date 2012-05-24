using System.Collections.Generic;
using System.Data;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Oracle
{
    /*
     * Utilizing thread safe lock free singleton strategy 
     * Singleton implementation strategy from http://www.yoda.arachsys.com/csharp/singleton.html
     */
    /// <summary>
    /// 
    /// </summary>
    public sealed class OracleSqlDbTypeMapper
    {
        private Dictionary<SqlDbType, OracleDbType> mapping;
        static readonly OracleSqlDbTypeMapper instance = new OracleSqlDbTypeMapper();

        static OracleSqlDbTypeMapper()
        {
        }

        public static OracleSqlDbTypeMapper Instance
        {
            get
            {
                return (instance);
            }
        }

        public int Count
        {
            get
            {
                return (this.mapping.Count);
            }
        }

        public OracleDbType this[SqlDbType s]
        {
            get
            {
                return (this.mapping[s]);
            }
        }

        public OracleSqlDbTypeMapper()
        {
            this.mapping = new Dictionary<SqlDbType, OracleDbType>();

            foreach (PairType<SqlDbType, OracleDbType> p in OracleDbTypeConstants.SqlToOracleTypeArray)
            {
                if (p == null) continue;
                this.mapping.Add(p.Left, p.Right);
            }
        }
    }
}
