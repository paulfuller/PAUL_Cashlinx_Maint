using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common.Libraries.Utility;
using CouchConsoleApp.vo;
using Oracle.DataAccess.Client;

namespace CouchConsoleApp.db
{
    class SourceDocByDateDAO
    {

        private static readonly SourceDocByDateDAO daoinst = new SourceDocByDateDAO();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SourceDocByDateDAO));
        //private OracleConnection connection = null;
        //private OracleCommand oracleCommand = null;
        //private OracleDataReader reader = null;

        private SourceDocByDateDAO()
        {
        }

        public static SourceDocByDateDAO getInstance()
        {
           return daoinst;
        }

        public void killCommand()
        {
            /*log.Debug("Aborting Execution of SourceDocByDateDAO");
            try
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if (this.oracleCommand != null)
                {
                    this.oracleCommand.Cancel();
                }

                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }catch(Exception e)
            {
                log.Error("Error during aborting SourceDocByDateDAO",e);
            }*/

        }

        public bool getPreviousRunStats(out PreviousArchStatVO vo)
        {
            vo = new PreviousArchStatVO();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
                {
                    log.Error("DB Connection not established: Search aborted");
                    return false;
                }
                using (var connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    string countSql = "SELECT COUNT(*) CNT FROM CCSOWNER.PWN_DOC_REG_ARCH_STAT STAT WHERE STATUS='Y'";
                    vo.SucessCount = executeCountQuery(countSql, connection);
                    countSql = "SELECT COUNT(*) CNT FROM CCSOWNER.PWN_DOC_REG_ARCH_STAT STAT WHERE STATUS='G'";
                    vo.GetErrorCount = executeCountQuery(countSql, connection);
                    countSql = "SELECT COUNT(*) CNT FROM CCSOWNER.PWN_DOC_REG_ARCH_STAT STAT WHERE STATUS='A'";
                    vo.AddErrorCount = executeCountQuery(countSql, connection);
                    countSql = "SELECT COUNT(*) CNT FROM CCSOWNER.PWN_DOC_REG_ARCH_STAT STAT WHERE STATUS='D'";
                    vo.DelErrorCount = executeCountQuery(countSql, connection);
                    setArchLogData(vo,connection);
                    return true;
                }

            }
            catch(Exception e)
            {
                log.Error("Getting previoud archival statistics failed",e);
            }finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for getPreviousRunStats : {0} Msec", stopwatch.Elapsed));
                /*if (var reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                if(connection != null)
                {
                    connection.Close();
                    connection = null;
                }*/
            }
            return false;
        }

        private void setArchLogData(PreviousArchStatVO vo,OracleConnection connection)
        {
            string sql = "SELECT TRUNC(ARCH_DATE),SOURCE_DOC_DB,TARGET_DOC_DB,TOTAL_DOCS,ARCHIVED,FAILED," +
                         "TO_CHAR(STARTTIME, 'HH12:MI:SS AM'), TO_CHAR(ENDTIME, 'HH12:MI:SS AM') FROM" +
                         " CCSOWNER.PWN_DOC_REG_ARCH_LOG ORDER BY STARTTIME DESC";
            using (var oracleCommand = new OracleCommand(sql, connection))
            {
                log.Debug(string.Format("Executing: {0}", sql));
                using (var reader = oracleCommand.ExecuteReader())
                {
                    if (reader.RowSize > 0 && reader.HasRows)
                    {
                        DataTable dataTable = new DataTable("PAWNARCHLOG");
                        dataTable.Load(reader, LoadOption.OverwriteChanges);
                        vo.DataTableValue = dataTable;
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                        reader.Close();
                    }
                }

            }
        }

        private string executeCountQuery(string countSQL, OracleConnection connection)
        {
            string retCount = "0";
            using (var oracleCommand = new OracleCommand(countSQL, connection))
            {
                log.Debug(string.Format("Executing: {0}", countSQL));
                using (var reader = oracleCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            retCount = Utilities.GetIntegerValue(reader["CNT"]).ToString();
                        }

                    }
                }
            }
            return retCount;
        }

        public bool docSourceDocsListByDate( ref List<SourceDocTreeVO> voList,ref List<SourceDocTreeVO> targetvoList,
            out string errorMsg)
        {
            bool ret = false;
            string retVal = "";
           // OracleDataReader reader = null;
            errorMsg = "";
            voList = new List<SourceDocTreeVO>();
            targetvoList = new List<SourceDocTreeVO>();
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error("DB Connection not established: Search aborted");
                return false;
            }
            var stopwatch = new Stopwatch();
          
            stopwatch.Start();
            SourceDocTreeVO vo = null;
            try
            {
                using (var connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();

                    string countSql = "SELECT TRUNC(STORAGE_DATE) dt,COUNT(*) CNT FROM PAWNDOCUMENTREGISTRY REG,PWN_DOC_REG_ARCH_STAT STAT" +
                                     " WHERE STAT.DOC_REG_ID=REG.ID AND ARCH_STATUS IS NOT NULL GROUP BY STORAGE_DATE ORDER BY STORAGE_DATE";

                    using (var oracleCommand = new OracleCommand(countSql, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", countSql));
                        using (var reader = oracleCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    vo = new SourceDocTreeVO
                                    {
                                        date = (DateTime)reader["dt"],
                                        count = Utilities.GetIntegerValue(reader["CNT"])
                                    };
                                    targetvoList.Add(vo);
                                }

                            }
                        }
                    }
                   // log.Debug("Time to get count(*)" + stopwatch.Elapsed + ": Count: " + totalRecords);

                    string sql = "SELECT TRUNC(STORAGE_DATE) dt,COUNT(*) CNT FROM PAWNDOCUMENTREGISTRY  REG WHERE" +
                                " REG.STORAGE_DATE <= TRUNC(SYSDATE) - 90 AND ARCH_STATUS IS NULL"+
                                 " GROUP BY STORAGE_DATE ORDER BY STORAGE_DATE";
                    using (var oracleCommand = new OracleCommand(sql, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        // oracleCommand.FetchSize = 100;
                        using (var reader = oracleCommand.ExecuteReader())
                        {
                            log.Debug("Execution Completed..........");
                            if (reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    vo = new SourceDocTreeVO
                                    {
                                        date = (DateTime)reader["dt"],
                                        count = Utilities.GetIntegerValue(reader["CNT"])
                                    };
                                    voList.Add(vo);
                                }

                            }
                        }

                    }
                    ret = true;
                }
            }
            catch (Exception e)
            {
                //msg = e.Message;
                log.Error("docSourceDocsListByDate Failed:", e);
                log.Debug(e.Message);
                errorMsg = e.Message;
                //log.Debug(e.StackTrace.ToString());
            }
            finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for docSourceDocsListByDate for tree  : {0} Msec", stopwatch.Elapsed));
                /*if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                oracleCommand = null;*/
            }
            return ret;
        }
    }

}
