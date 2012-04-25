using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common.Libraries.Utility;
using Oracle.DataAccess.Client;

namespace CouchConsoleApp.db
{
    public class ErrorMsgDAO
    {
        private static readonly ErrorMsgDAO daoinst = new ErrorMsgDAO();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ErrorMsgDAO));
        //private OracleConnection connection = null;
       // private OracleCommand oracleCommand = null;
       // private OracleDataReader reader = null;

        private ErrorMsgDAO()
        {
        }

        public static ErrorMsgDAO getInstance()
        {
           return daoinst;
        }

        /*public void killCommand()
        {

            if (reader != null && !reader.IsClosed)
            {
                reader.Close();
            }

            if (this.oracleCommand != null)
            {
                this.oracleCommand.Cancel();
            }

            if (connection!=null &&connection.State==ConnectionState.Open)
            {
                connection.Close();
            }

        }*/

        public bool AddErrorsToDB(Dictionary<string, int> errorDict)
        {
            bool ret = false;
            string retVal = "";
           // OracleDataReader reader = null;
            OracleTransaction myTrans = null;
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
                    myTrans = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    string del_sql = "DELETE FROM CCSOWNER.PWN_DOC_REG_ARCH_ERROR WHERE ID<"+":IDVAL";

                    using (var oracleCommand = new OracleCommand(null, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", del_sql));
                        oracleCommand.CommandText = del_sql;
                        oracleCommand.Parameters.Add(":IDVAL", OracleDbType.Int32);

                        oracleCommand.Prepare();
                        oracleCommand.Parameters[0].Value = 10;
                        oracleCommand.ExecuteNonQuery();
                    }


                    string sql = "INSERT INTO CCSOWNER.PWN_DOC_REG_ARCH_ERROR(ID,ERROR_MSG) VALUES " +
                                "(:ID,:ERROR_MSG)";

                    string str = null;
                    using (var oracleCommand = new OracleCommand(null, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        oracleCommand.CommandText = sql;
                        oracleCommand.Parameters.Add(":ID", OracleDbType.Int32);
                        oracleCommand.Parameters.Add(":ERROR_MSG", OracleDbType.Varchar2);
                        oracleCommand.Prepare(); 
                        foreach(var i in errorDict)
                        {
                            oracleCommand.Parameters[0].Value = i.Value;
                            oracleCommand.Parameters[1].Value = i.Key;
                            oracleCommand.ExecuteNonQuery();
                        }
                        ret = true;
                    }
                    myTrans.Commit();
                }
            }
            catch (Exception e)
            {
                log.Error("AddErrorsToDB Failed:", e);
                log.Debug(e.StackTrace.ToString());
                ret = false;
                if (myTrans != null )//&& connection.State != ConnectionState.Closed)
                {
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch (OracleException oracleException)
                    {
                        log.Error("Oracle Exception" + oracleException.Message);
                        return false;
                    }

                }
            }
            finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for AddErrorsToDB  : {0} Msec", stopwatch.ElapsedMilliseconds));
                //oracleCommand = null;
            }
            return ret;
        }


        public bool AddErrorToDB(string msg,out int idVal)
        {
            bool ret = false;
            string retVal = "";
            //OracleDataReader reader = null;
            idVal = -1;
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

                    string seqSql = "SELECT CCSOWNER.PWN_DOC_REG_ARCH_ERROR_SEQ.NEXTVAL FROM DUAL";

                    using (var oracleCommand = new OracleCommand(seqSql, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", seqSql));
                        using (var reader = oracleCommand.ExecuteReader())
                        {
                            if (reader.RowSize > 0 && reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    idVal = Utilities.GetIntegerValue(reader["val"]);
                                    log.Debug("Result :" + retVal);
                                }
                            }
                            else
                            {
                                log.Error("AddErrorsToDB Failed due to sequance:");
                                return false;
                            }
                        }

                    }

                    string sql = "INSERT INTO CCSOWNER.PWN_DOC_REG_ARCH_ERROR(ID,ERROR_MSG) VALUES " +
                                "(:ID,:ERROR_MSG)";

                    string str = null;
                    using (var oracleCommand = new OracleCommand(null, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        oracleCommand.CommandText = sql;
                        oracleCommand.Parameters.Add(":ID", OracleDbType.Int32);
                        oracleCommand.Parameters.Add(":ERROR_MSG", OracleDbType.Varchar2);
                        oracleCommand.Prepare();
                        oracleCommand.Parameters[0].Value = idVal;
                        oracleCommand.Parameters[1].Value = msg;
                        oracleCommand.ExecuteNonQuery();
                      }
                      ret = true;
                  }
                
            }
            catch (Exception e)
            {
                log.Error("AddErrorsToDB Failed:", e);
                log.Debug(e.StackTrace.ToString());
                ret = false;
            }
            finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for AddErrorsToDB  : {0} Msec", stopwatch.ElapsedMilliseconds));
                //oracleCommand = null;
            }
            return ret;
        }

    }
}
