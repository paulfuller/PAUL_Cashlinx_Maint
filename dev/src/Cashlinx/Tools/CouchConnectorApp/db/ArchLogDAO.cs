using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common.Libraries.Utility;
using Oracle.DataAccess.Client;

namespace CouchConsoleApp.db
{
    public class ArchLogDAO
    {
        private static readonly ArchLogDAO daoinst = new ArchLogDAO();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ArchLogDAO));

        public static ArchLogDAO getInstance()
        {
            return daoinst;
        }

        public int CreateArchLog(int totalDocs, string srcDB, string tarDB, string userID, out bool isSuccess, out string errorMSG)
        {
            //OracleConnection connection = null;
            //OracleCommand oracleCommand = null;
            isSuccess = false;
            errorMSG = "";
            int logID = 0;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from CreateArchLog :DB Connection not established: create aborted");
                return 0;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using(var connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    string sql = "INSERT INTO PWN_DOC_REG_ARCH_LOG(ID,ARCH_DATE,TOTAL_DOCS,SOURCE_DOC_DB,TARGET_DOC_DB," +
                                 "STARTTIME,USER_ID) VALUES (:ID,:ARCH_DATE,:TOTAL_DOCS,:SOURCE_DOC_DB,:TARGET_DOC_DB,:STARTTIME,:USER_ID)";
                    string str = null;
                    using(var oracleCommand = new OracleCommand(null, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        oracleCommand.CommandText = sql;
                        oracleCommand.Parameters.Add(":ID", OracleDbType.Int32);
                        oracleCommand.Parameters.Add(":ARCH_DATE", OracleDbType.Date);
                        oracleCommand.Parameters.Add(":TOTAL_DOCS", OracleDbType.Int32);
                        oracleCommand.Parameters.Add(":SOURCE_DOC_DB", OracleDbType.Varchar2);
                        oracleCommand.Parameters.Add(":TARGET_DOC_DB", OracleDbType.Varchar2);
                        oracleCommand.Parameters.Add(":STARTTIME", OracleDbType.TimeStampTZ);
                        oracleCommand.Parameters.Add(":USER_ID", OracleDbType.Varchar2);
                        oracleCommand.Prepare();
                        logID = ConnDAO.getInstance().getUniqueID(ref str, connection);
                        oracleCommand.Parameters[0].Value = logID;
                        oracleCommand.Parameters[1].Value = DateTime.Today;
                        oracleCommand.Parameters[2].Value = totalDocs;
                        oracleCommand.Parameters[3].Value = srcDB;
                        oracleCommand.Parameters[4].Value = tarDB;
                        oracleCommand.Parameters[5].Value = Utilities.GetTimestampValue(DateTime.Now);
                        oracleCommand.Parameters[6].Value = userID;
                        oracleCommand.ExecuteNonQuery();
                        isSuccess = true;
                    }
                  
                }
            }
            catch(Exception e)
            {
                log.Error("CreateArchLog Failed:", e);
                errorMSG = "CreateArchLog Failed:" + e.Message;
                log.Debug(e.StackTrace.ToString());
                isSuccess = false;
            }
            finally
            {
                stopwatch.Stop();
                log.Info(string.Format("Time Taken for CreateArchLog  : {0} Msec", stopwatch.ElapsedMilliseconds));
               // oracleCommand = null;
            }
            return logID;
        }

        public int UpdateArchLog(int docID,int archived,int failed,out bool isSuccess, out string errorMSG)
        {
            //OracleConnection connection = null;
            //OracleCommand oracleCommand = null;
            isSuccess = false;
            errorMSG = "";
            int logID = 0;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from UpdateArchLog :DB Connection not established: create aborted");
                return 0;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using (var connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    string sql = "UPDATE PWN_DOC_REG_ARCH_LOG SET ARCHIVED=:ARCHIVED,FAILED=:FAILED,ENDTIME=:ENDTIME" +
                                 " WHERE ID=:ID";
                    string str = null;
                    using (var oracleCommand = new OracleCommand(null, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        oracleCommand.CommandText = sql;
                        oracleCommand.Parameters.Add(":ARCHIVED", OracleDbType.Int32);
                        oracleCommand.Parameters.Add(":FAILED", OracleDbType.Int32);
                        oracleCommand.Parameters.Add(":ENDTIME", OracleDbType.TimeStampTZ);
                        oracleCommand.Parameters.Add(":ID", OracleDbType.Int32);
                        oracleCommand.Prepare();
                        oracleCommand.Parameters[0].Value = archived;
                        oracleCommand.Parameters[1].Value = failed;
                        oracleCommand.Parameters[2].Value = Utilities.GetTimestampValue(DateTime.Now);
                        oracleCommand.Parameters[3].Value = docID;
                        oracleCommand.ExecuteNonQuery();
                        isSuccess = true;
                    }

                }
            }
            catch (Exception e)
            {
                log.Error("UpdateArchLog Failed:", e);
                errorMSG = "UpdateArchLog Failed:" + e.Message;
                log.Debug(e.StackTrace.ToString());
                isSuccess = false;
            }
            finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for UpdateArchLog  : {0} Msec", stopwatch.ElapsedMilliseconds));
               // oracleCommand = null;
            }
            return logID;
        }


    }




}