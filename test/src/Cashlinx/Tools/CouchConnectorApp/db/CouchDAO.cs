using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common.Libraries.Utility;
using CouchConsoleApp.vo;
using Oracle.DataAccess.Client;

namespace CouchConsoleApp.db
{
    class CouchDAO
    {

        private static readonly CouchDAO daoinst = new CouchDAO();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CouchDAO));
        //private OracleConnection connection = null;
        //private OracleCommand oracleCommand = null;
        //private OracleDataReader reader = null;

        public static CouchDAO getInstance()
        {
            return daoinst;
        }

        public bool GetCouchDBRepos(ref List<CouchConsoleApp.vo.PwnDocArchDBRepVO> dbList)
        {
            bool ret = false;
            string retVal = "";
            //OracleDataReader reader = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from GetDocumentSets :DB Connection not established: Search aborted");
                return false;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using (var connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    string sql = "SELECT ID,DBNAME,DBINFO FROM PWN_DOC_ARCH_DB_REP";
                    
                    using (var oracleCommand = new OracleCommand(sql, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        using (var reader = oracleCommand.ExecuteReader())
                        {

                            if (reader.RowSize > 0 && reader.HasRows)
                            {
                                dbList = new List<CouchConsoleApp.vo.PwnDocArchDBRepVO>();
                                CouchConsoleApp.vo.PwnDocArchDBRepVO vo = null;
                                while(reader.Read())
                                {
                                    vo = new CouchConsoleApp.vo.PwnDocArchDBRepVO();
                                    vo.Id = Utilities.GetIntegerValue(reader["ID"]);
                                    vo.DBName = Utilities.GetStringValue(reader["DBNAME"]);
                                    vo.DBInfo = Utilities.GetStringValue(reader["DBINFO"]);
                                    dbList.Add(vo);
                                }

                            }
                            ret = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("GetCouchDBRepos Failed:", e);
                log.Debug(e.StackTrace.ToString());
                ret = false;
            }
            finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for GetCouchDBRepos  : {0} Msec", stopwatch.Elapsed));
            }
            return ret;
        }


        public bool AddCouchDbToRepos(List<CouchConsoleApp.vo.PwnDocArchDBRepVO> dbList,out int dbid)
        {
            bool ret = false;
            string retVal = "";
            dbid = 0;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from GetDocumentSets :DB Connection not established: Search aborted");
                return false;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using (var connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    string sql = "INSERT INTO PWN_DOC_ARCH_DB_REP(ID,DBNAME,DBINFO,CREATIONDATE) VALUES " +
                        "(:ID,:DBNAME,:DBINFO,:CREATIONDATE)";
                   string str= null;
                    using (var oracleCommand = new OracleCommand(null, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        oracleCommand.CommandText = sql;
                        oracleCommand.Parameters.Add(":ID", OracleDbType.Int32);
                        oracleCommand.Parameters.Add(":DBNAME", OracleDbType.Varchar2);
                        oracleCommand.Parameters.Add(":DBINFO", OracleDbType.Varchar2);
                        oracleCommand.Parameters.Add(":CREATIONDATE", OracleDbType.TimeStampTZ);
                        oracleCommand.Prepare();  // Calling Prepare after having set the Commandtext and parameters.
                        //oracleCommand.ExecuteNonQuery();

                        foreach (var vo in dbList)
                        {
                            
                            dbid = ConnDAO.getInstance().getUniqueID(ref str, connection);
                            oracleCommand.Parameters[0].Value = dbid;
                            oracleCommand.Parameters[1].Value = vo.DBName;
                            oracleCommand.Parameters[2].Value = vo.DBInfo;
                            oracleCommand.Parameters[3].Value = Utilities.GetTimestampValue(DateTime.Now);
                            oracleCommand.ExecuteNonQuery();
                        }
                        
                        ret = true;
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("AddCouchDbToRepos Failed:", e);
                log.Debug(e.StackTrace.ToString());
                ret = false;
            }
            finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for AddCouchDbToRepos  : {0} Msec", stopwatch.ElapsedMilliseconds));
                //oracleCommand = null;
            }
            return ret;
        }
    }
}
