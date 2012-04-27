using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Utility.Shared;
using System.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using Common.Libraries.Utility.Type;

namespace Pawn.Forms.Inquiry
{
    public abstract class Inquiry
    {
        //bool isInitialized = false;

        public int errorLevel;
        public string errorMessage;

        public enum sortDir_enum
        {
            [StringDBMap("Ascending", "ASC")]
            ASCENDING,
            [StringDBMap("Descending", "DESC")]
            DESCENDING
        }
        public sortDir_enum sortDir; 

        public virtual string sortByField()
        {
            return string.Empty;
        }

        public virtual string sortByName()
        {
            return string.Empty;
        }

        protected static DataSet getDataSet(string package, string procedure, List<OracleProcParam> oracleProcParams, Dictionary<string, string> refCursors)
        {
            DataSet outputDataSet = null;

            try
            {
                GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand(
                    "ccsowner", package, procedure,
                    oracleProcParams ?? new List<OracleProcParam>(),
                    refCursors == null ? new List<PairType<string, string>>() : refCursors.Select(x => new PairType<string, string>(x.Key, x.Value)).ToList(),
                    "o_error_code", "o_error_desc",
                    out outputDataSet);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error processing your request. Please contact your administrator.", ex);
            }

            return outputDataSet;
        }
    }

}


    



