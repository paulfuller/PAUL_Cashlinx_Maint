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
    public sealed class ConnDAO
    {
        private static readonly ConnDAO daoinst = new ConnDAO();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ConnDAO));
        //private OracleConnection connection = null;
        //private OracleCommand oracleCommand = null;
        //private OracleDataReader reader = null;

        private ConnDAO()
        {
        }

        public static ConnDAO getInstance()
        {
            return daoinst;
        }

       /* public void killCommand()
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
        }*/

        /*public bool TestConnection(ref string msg)
        {
            bool ret = false;
            string retVal = "";
            //OracleDataReader reader = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                return false;
            }
            string temp=
                "User Id=ccsowner;Password=prime98s;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=clxdbdev)(PORT =1521))(CONNECT_DATA=(SID=CLXD6)));Min Pool Size=1;Connection Lifetime=60;Connection Timeout=120;Incr Pool Size=1;Decr Pool Size=1";
             
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using(connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    log.Debug(string.Format("State: {0}", connection.State));
                    //log.Debug(string.Format("ConnectionString: {0}", connection.ConnectionString));
                    string sql = "SELECT sysdate FROM dual";
                    using(oracleCommand = new OracleCommand(sql, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        reader = oracleCommand.ExecuteReader();
                        while(reader.Read())
                        {
                            string myField = ((DateTime)reader["sysdate"]).ToString();
                            log.Debug("Result :" + myField);
                            ret = true;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                msg = e.Message;
                log.Error("Test Connection Failed:", e);
            }
            finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for Connect test : {0} Msec", stopwatch.Elapsed));
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
            }
            return ret;
        }*/

        /*public int getUniqueID(ref string msg)
        {
            OracleConnection connection1 = null;
            OracleCommand oracleCommand1 = null;
            OracleDataReader reader1 = null;
            bool ret = false;
            int retVal = 0;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                return 0;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using(connection1 = new OracleConnection())
                {
                    connection1.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection1.Open();
                    string sql = "SELECT CCSOWNER.PWN_DOC_REG_ARCH_ERROR_SEQ.NEXTVAL val FROM DUAL";
                    using(oracleCommand1 = new OracleCommand(sql, connection1))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        reader1 = oracleCommand1.ExecuteReader();
                        if (reader1.RowSize > 0 && reader1.HasRows)
                        {
                            while(reader1.Read())
                            {
                                retVal = PawnUtilities.Utilities.GetIntegerValue(reader1["val"]);
                                log.Debug("Result :" + retVal);
                            }
                        }
                    }
                   // updatePwnDocReg(connection1);
                }
            }
            catch(Exception e)
            {
                msg = e.Message;
                log.Error("Test Connection Failed:", e);
            }
            finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for getUniqueID: {0} Msec", stopwatch.ElapsedMilliseconds));
                if (reader1 != null)
                {
                    reader1.Dispose();
                    reader1.Close();
                }
            }
            return retVal;
        }*/
        private static object SeqlockingObject = new Object();

        public int getUniqueIDNew(ref string msg, OracleConnection connection1)
        {
            bool ret = false;
            int retVal = 0;

            lock (SeqlockingObject)
            {
                if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
                {
                    return 0;
                }
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                OracleTransaction oracleTransaction = null;
                try
                {
                    //to be changed to another seq
                    string sql = "SELECT CCSOWNER.PWN_DOC_REG_ARCH_SEQ.NEXTVAL val FROM DUAL";
                    oracleTransaction = connection1.BeginTransaction();
                    using(var oracleCommand1 = new OracleCommand(sql, connection1))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        using(var reader1 = oracleCommand1.ExecuteReader())
                        {
                            if (reader1.RowSize > 0 && reader1.HasRows)
                            {
                                while(reader1.Read())
                                {
                                    retVal = Utilities.GetIntegerValue(reader1["val"]);
                                    log.Debug("Result :" + retVal);
                                }
                            }
                        }
                    }
                    oracleTransaction.Commit();
                }
                catch(Exception e)
                {
                    msg = e.Message;
                    log.Error("Test Connection Failed:", e);
                    oracleTransaction.Rollback();
                }
                finally
                {
                    stopwatch.Stop();
                    log.Debug(string.Format("Time Taken for getUniqueID: {0} Msec", stopwatch.ElapsedMilliseconds));
                }
            }
            return retVal;
        }


        public int getUniqueID(ref string msg, OracleConnection connection1)
        {
            //OracleConnection connection1 = null;
            //OracleCommand oracleCommand1 = null;
            //OracleDataReader reader1 = null;
            bool ret = false;
            int retVal = 0;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                return 0;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                    string sql = "SELECT CCSOWNER.PWN_DOC_REG_ARCH_ERROR_SEQ.NEXTVAL val FROM DUAL";
                    using (var oracleCommand1 = new OracleCommand(sql, connection1))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        using (var reader1 = oracleCommand1.ExecuteReader())
                        {
                            if (reader1.RowSize > 0 && reader1.HasRows)
                            {
                                while(reader1.Read())
                                {
                                    retVal = Utilities.GetIntegerValue(reader1["val"]);
                                    log.Debug("Result :" + retVal);
                                }
                            }
                        }
                    }
                    // updatePwnDocReg(connection1);
               // }
            }
            catch (Exception e)
            {
                msg = e.Message;
                log.Error("Test Connection Failed:", e);
            }
            finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for getUniqueID: {0} Msec", stopwatch.ElapsedMilliseconds));
            }
            return retVal;
        }

        /*public void testUpdates()
        {
            OracleConnection connection1 = null;
            OracleCommand oracleCommand1 = null;
            OracleDataReader reader1 = null;
            bool ret = false;
            int retVal = 0;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                return;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using(connection1 = new OracleConnection())
                {
                    connection1.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection1.Open();
                    updatePwnDocReg(connection1,'G');
                    insertArchLog(connection1, 212, 'G');
                }
            }catch(Exception e)
            {
                log.Debug(e.StackTrace);
            }

        }*/


       /* public int getUniqueErrorID(ref string msg)
        {
            bool ret = false;
            int retVal = 0;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                return 0;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using(connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    //log.Debug(string.Format("State: {0}", connection.State));
                    //log.Debug(string.Format("ConnectionString: {0}", connection.ConnectionString));
                    string sql = "SELECT CCSOWNER.PWN_DOC_REG_ARCH_SEQ.NEXTVAL val FROM DUAL";
                    using(oracleCommand = new OracleCommand(sql, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        reader = oracleCommand.ExecuteReader();
                        if (reader.RowSize > 0 && reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                retVal = PawnUtilities.Utilities.GetIntegerValue(reader["val"]);
                                log.Debug("Result :" + retVal);
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                msg = e.Message;
                log.Error("Test Connection Failed:", e);
            }
            finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for getUniqueID: {0} Msec", stopwatch.ElapsedMilliseconds));
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
            }
            return retVal;
        }*/

       private void updatePwnDocReg(OracleConnection oracleConnection,char ch)
        {
            //OracleCommand oracleCommand = null;
            string sql = " UPDATE CCSOWNER.PAWNDOCUMENTREGISTRY PWNREG SET "+
                         " PWNREG.ARCH_STATUS=:STATUS,PWNREG.ARCH_DB_ID=:DBID" +
                         " WHERE PWNREG.ID=:IDVAL";

           /* string sql = " UPDATE CCSOWNER.PAWNDOCUMENTREGISTRY PWNREG SET " +
                         " PWNREG.ARCH_DB_ID=:DBID" +
                         " WHERE PWNREG.ID=:IDVAL";*/
            using (var oracleCommand = new OracleCommand(null, oracleConnection))
            {
                log.Debug(string.Format("Executing: {0}", sql));
                oracleCommand.CommandText = sql;
                oracleCommand.Parameters.Add(":STATUS", OracleDbType.Char);
                oracleCommand.Parameters.Add(":DBID", OracleDbType.Int16);
                oracleCommand.Parameters.Add(":IDVAL", OracleDbType.Int16);
                oracleCommand.Prepare();
                oracleCommand.Parameters[0].Value = ch;
                oracleCommand.Parameters[1].Value = 1111;
                oracleCommand.Parameters[2].Value = 1;
                oracleCommand.ExecuteNonQuery();
            }
        }



       private void insertArchLog(OracleConnection oracleConnection, int errorCode, char errorCodeChar)
       {
           //OracleCommand oracleCommand = null;
           string sql = "INSERT INTO CCSOWNER.PWN_DOC_REG_ARCH_STAT(ID,DOC_REG_ID,STORAGE_ID,ARCH_DATE,USER_ID,STATUS,ERROR_ID)" +
                        " VALUES(:ID,:DOC_REG_ID,:STORAGE_ID,:ARCH_DATE,:USER_ID,:STATUS,:ERROR_ID)";

           string str = null;
           using (var oracleCommand = new OracleCommand(null, oracleConnection))
           {
               log.Debug(string.Format("Executing: {0}", sql));
               oracleCommand.CommandText = sql;
               oracleCommand.Parameters.Add(":ID", OracleDbType.Int32);
               oracleCommand.Parameters.Add(":DOC_REG_ID", OracleDbType.Int32);
               oracleCommand.Parameters.Add(":STORAGE_ID", OracleDbType.Varchar2);
               oracleCommand.Parameters.Add(":ARCH_DATE", OracleDbType.TimeStampTZ);
               oracleCommand.Parameters.Add(":USER_ID", OracleDbType.Varchar2);
               oracleCommand.Parameters.Add(":STATUS", OracleDbType.Char);
               oracleCommand.Parameters.Add(":ERROR_ID", OracleDbType.Int32);
               oracleCommand.Prepare();
               oracleCommand.Parameters[0].Value = ConnDAO.getInstance().getUniqueID(ref str, oracleConnection);
               oracleCommand.Parameters[1].Value = 1;
               oracleCommand.Parameters[2].Value = "43434232242";
               oracleCommand.Parameters[3].Value = Utilities.GetTimestampValue(DateTime.Now);
               oracleCommand.Parameters[4].Value = "sssssss";
               oracleCommand.Parameters[5].Value = errorCodeChar;
               oracleCommand.Parameters[6].Value = errorCode;
               oracleCommand.ExecuteNonQuery();
           }
       }

        /*public bool docListGet(ref DataTable table)
        {
            bool ret = false;
            string retVal = "";
            OracleDataReader reader = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                return false;
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using(var connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    string sql =
                            "SELECT ID,STORAGE_DATE,TICKET_NUMBER,STORENUMBER FROM CCSOWNER.PAWNDOCUMENTREGISTRY  REG WHERE REG.STORAGE_DATE <= TRUNC(SYSDATE) - 90" +
                            " ORDER BY REG.STORAGE_DATE DESC";
                    using(oracleCommand = new OracleCommand(sql, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        // oracleCommand.FetchSize = 100;
                        reader = oracleCommand.ExecuteReader();
                        log.Debug("Execution Completed..........");

                        if (reader.RowSize > 0 && reader.HasRows)
                        {
                            //Create data table with same table name as the one being queried
                            table = new DataTable("PAWNDOCUMENTREGISTRY");
                            log.Debug("Loading data......");
                            //Load the data table with the reader data
                            table.Load(reader, LoadOption.OverwriteChanges);
                            log.Debug("Loading Complete......");
                            ret = true;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                //msg = e.Message;
                log.Error("docListGet Failed:", e);
                log.Debug(e.StackTrace.ToString());
            }
            finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for docListGet  : {0} Msec", stopwatch.Elapsed));
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                oracleCommand = null;
            }
            return ret;
        }*/
    }
}