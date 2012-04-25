using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common.Libraries.Utility;
using CouchConsoleApp.thread;
using CouchConsoleApp.vo;
using Oracle.DataAccess.Client;

namespace CouchConsoleApp.db
{
    public sealed class PwnArchLogDAO
    {
        private static readonly PwnArchLogDAO daoinst = new PwnArchLogDAO();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PwnArchLogDAO));
        public static PwnArchLogDAO getInstance()
        {
            return daoinst;
        }

        public bool CreateArchLog(PawnDocRegVO vo, char succCodeChar, bool isRecovery)
        {
            bool ret = false;
            string retVal = "";
            //OracleDataReader reader = null;
            //OracleConnection connection = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from GetDocumentSets :DB Connection not established: Search aborted");
                return false;
            }
            int uqID = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            OracleTransaction myTrans = null;
           
            using(var connection = new OracleConnection())
            {
                try
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    if(!isRecovery)
                    {
                       uqID=ConnDAO.getInstance().getUniqueIDNew(ref retVal, connection);
                        //insertArchLogNew
                    }

                    myTrans = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    if (isRecovery)
                    {
                        updateArchLog(connection, vo, succCodeChar);
                    }
                    else
                    {
                        insertArchLogNew(connection, vo, succCodeChar,uqID);
                    }
                    //log.Debug("Time for insertArchLog " + stopwatch.ElapsedMilliseconds);
                    //stopwatch.Restart();
                    updatePwnDocReg(connection, vo, succCodeChar);
                    //log.Debug("Time for updatePwnDocReg " + stopwatch.ElapsedMilliseconds);
                    myTrans.Commit();
                    ret = true;

                }
                catch(Exception e)
                {
                    log.Error("CreateArchLog Failed:", e);
                    string str =
                            string.Format("---DB Archive Log Failure Doc ID{0} Storage ID {1} Status {2} Target DB ID{3} Target DB Name{4} ---",
                                          vo.DocID, vo.StorageID, succCodeChar, vo.TargetCouchDBID, vo.TargetCouchDBName);
                    log.Error(str);
                    log.Debug(e.StackTrace.ToString());
                    if (myTrans != null && connection.State != ConnectionState.Closed)
                    {
                       try
                        {
                            myTrans.Rollback();
                        }
                        catch(OracleException oracleException)
                        {
                            log.Error("Oracle Exception" + oracleException.Message);
                            return false;
                        }
                    }
                    ret = false;
                }
                finally
                {
                    stopwatch.Stop();
                    log.Info(string.Format("Time Taken for Success CreateArchLog  : {0} Msec", stopwatch.ElapsedMilliseconds));
                    //oracleCommand = null;
                }
                return ret;
            }
        }

       /* public bool CreateArchLog(PawnDocRegVO vo, char succCodeChar, bool isRecovery,out string errorMessage)
        {
            bool ret = false;
            string retVal = "";
            errorMessage = "";
            //OracleDataReader reader = null;
            //OracleConnection connection = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from GetDocumentSets :DB Connection not established: Search aborted");
                return false;
            }
            int uqID = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var stopwatch1 = new Stopwatch();

            long t0 = 0;
            long t1 = 0;
            long t2 = 0;
            long t3 = 0;
            long t4 = 0;

            OracleTransaction myTrans = null;

            using (var connection = new OracleConnection())
            {
                try
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();

                    connection.Open();
                    t0 = stopwatch1.ElapsedMilliseconds;
                    stopwatch1.Restart();

                    if (!isRecovery)
                    {
                        uqID = ConnDAO.getInstance().getUniqueIDNew(ref retVal, connection);
                        //insertArchLogNew
                    }
                    t1 = stopwatch1.ElapsedMilliseconds;
                    stopwatch1.Restart();
                    myTrans = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    if (isRecovery)
                    {
                        updateArchLog(connection, vo, succCodeChar);
                    }
                    else
                    {
                        insertArchLogNew(connection, vo, succCodeChar, uqID);
                    }
                    t2 = stopwatch1.ElapsedMilliseconds;
                    stopwatch1.Restart();

                    //log.Debug("Time for insertArchLog " + stopwatch.ElapsedMilliseconds);
                    //stopwatch.Restart();
                    updatePwnDocReg(connection, vo, succCodeChar);
                    t3 = stopwatch1.ElapsedMilliseconds;
                    stopwatch1.Restart();
                    //log.Debug("Time for updatePwnDocReg " + stopwatch.ElapsedMilliseconds);
                    myTrans.Commit();
                    t4 = stopwatch1.ElapsedMilliseconds;
                    stopwatch1.Restart();
                    ret = true;

                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                    log.Error("CreateArchLog Failed:", e);
                    string str =
                            string.Format("---DB Archive Log Failure Doc ID{0} Storage ID {1} Status {2} Target DB ID{3} Target DB Name{4} ---",
                                          vo.DocID, vo.StorageID, succCodeChar, vo.TargetCouchDBID, vo.TargetCouchDBName);
                    log.Error(str);
                    log.Debug(e.StackTrace.ToString());
                    if (myTrans != null && connection.State != ConnectionState.Closed)
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
                    ret = false;
                }
                finally
                {
                    stopwatch.Stop();
                    log.Debug(string.Format("Time Taken for CreateArchLog  : {0} Msec", stopwatch.ElapsedMilliseconds));
                    //oracleCommand = null;
                    log.Info(string.Format(" conn {0} success UQ ID {1} arch log insert {2} update reg{3} final commit {4}", t0, t1, t2, t3, t4));
                }
                return ret;
            }
        }*/

        public bool CreateArchLogForBatch(List <PawnDocRegVO> voList, char succCodeChar, bool isRecovery)
        {
            bool ret = false;
            string retVal = "";
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from GetDocumentSets :DB Connection not established: Search aborted");
                return false;
            }
            int uqID = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            OracleTransaction myTrans = null;

            using (var connection = new OracleConnection())
            {
                try
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    foreach(var pawnDocRegVO in voList)
                    {
                        if (!isRecovery)
                        {
                            uqID = ConnDAO.getInstance().getUniqueIDNew(ref retVal, connection);
                            //insertArchLogNew
                        }

                        myTrans = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                        if (isRecovery)
                        {
                            updateArchLog(connection, pawnDocRegVO, succCodeChar);
                        }
                        else
                        {
                            insertArchLogNew(connection, pawnDocRegVO, succCodeChar, uqID);
                        }
                        updatePwnDocReg(connection, pawnDocRegVO, succCodeChar);
                        myTrans.Commit();
                    }
                    ret = true;
                }
                catch (Exception e)
                {
                    log.Error("CreateArchLog Failed:", e);
                    log.Debug(e.StackTrace.ToString());
                    if (myTrans != null && connection.State != ConnectionState.Closed)
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
                    ret = false;
                    throw e;
                }
                finally
                {
                    stopwatch.Stop();
                    log.Debug(string.Format("Time Taken for CreateArchLog  : {0} Msec", stopwatch.ElapsedMilliseconds));
                    //oracleCommand = null;
                }
                return ret;
            }
        }

        private bool getConnectionWithRetry(OracleConnection conn)
        {
            int i = 0;
            while(i<3)
            {
                try
                {
                   conn.Open();
                   if(conn.State==ConnectionState.Open)
                   {
                       return true;
                   }
                }
                catch (Exception e)
                {
                    log.Error("Error @ getConnectionWithRetry" + e.Message+" Retry "+i);
                }
                i++;
            }
            return false;
        }

        public bool CreateArchLogWithError(PawnDocRegVO vo, int errorCode, string msg, Char errorCodeChar,bool isRecovery)
        {
            bool ret = false;
            string retVal = "";
            //OracleDataReader reader = null;
            //OracleConnection connection = null;
            //OracleCommand oracleCommand = null;
            int errorID = 0;
            int uqID = 0;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from GetDocumentSets :DB Connection not established: Search aborted");
                return false;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            //var stopwatch1 = new Stopwatch();
            OracleTransaction myTrans = null;
           /* long t0 = 0;
            long t1 = 0;
            long t2 = 0;
            long t3 = 0;
            long t4 = 0;*/

            using(var connection = new OracleConnection())
            {
                try
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                   // stopwatch1.Start();
                    connection.Open();
                    /*if(!getConnectionWithRetry(connection))
                    {
                        log.Error("AddErrorsToDB Failed: due to connection error");
                        string str =
                         string.Format("---DB Archive Log Failure Doc ID{0} Storage ID {1} Status {2} Target DB ID{3} Target DB Name{4}  Error Code{5} ErrorMSG {6}---",
                                       vo.DocID, vo.StorageID, errorCodeChar, vo.TargetCouchDBID, vo.TargetCouchDBName, errorCode, msg);
                        log.Error(str);
                        return false;
                    }*/
                    if (!isRecovery)
                    {
                        uqID = ConnDAO.getInstance().getUniqueIDNew(ref retVal, connection);
                        //insertArchLogNew
                    }
                  //  t0 = stopwatch1.ElapsedMilliseconds;
                  //  stopwatch1.Restart();
                    //connection.Open();
                    if (errorCode == -1)
                    {
                        errorID = getErrorCodeSeq(connection);
                    }else
                    {
                        errorID = errorCode;
                    }
                    

                    myTrans = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    if (errorCode == -1)
                    insertErrorMSG(connection, msg, errorID);

                 //   t1 = stopwatch1.ElapsedMilliseconds;
                  //  stopwatch1.Restart();

                    //log.Debug("Time for Insert Error " + stopwatch.ElapsedMilliseconds);
                    //stopwatch.Restart();
                    if (isRecovery)
                        updateArchLog(connection, vo, errorID, errorCodeChar);
                    else
                        insertArchLog(connection, vo, errorID, errorCodeChar,uqID);

                 //   t2 = stopwatch1.ElapsedMilliseconds;
                 //   stopwatch1.Restart();
                    //log.Debug("Time for insertArchLog " + stopwatch.ElapsedMilliseconds);
                   // stopwatch.Restart();
                    updatePwnDocReg(connection, vo, errorCodeChar);
                 //   t3 = stopwatch1.ElapsedMilliseconds;
                //    stopwatch1.Restart();
                    //log.Debug("Time for updatePwnDocReg " + stopwatch.ElapsedMilliseconds);
                    myTrans.Commit();

               //     t4 = stopwatch1.ElapsedMilliseconds;
                 //   stopwatch1.Stop();
                    ret = true;
                }
                catch(Exception e)
                {
                    log.Error("AddErrorsToDB Failed:", e);
                    string str =
                           string.Format("---DB Archive Log Failure Doc ID{0} Storage ID {1} Status {2} Target DB ID{3} Target DB Name{4}  Error Code{5} ErrorMSG {6}---",
                                         vo.DocID, vo.StorageID, errorCodeChar, vo.TargetCouchDBID, vo.TargetCouchDBName, errorCode,msg);
                    log.Error(str);
                    log.Debug(e.StackTrace.ToString());
                    if (myTrans != null && connection.State != ConnectionState.Closed)
                    {
                        try
                        {
                            myTrans.Rollback();
                        }
                        catch(OracleException oracleException)
                        {
                            log.Error("Oracle Exception" + oracleException.Message);
                            return false;
                        }
                        
                    }
                    ret = false;
                }
                finally
                {
                    stopwatch.Stop();
                    log.Info(string.Format("Time Taken for Error ArchLog  : {0} Msec", stopwatch.ElapsedMilliseconds));
                    //log.Info(string.Format(" conn {0} error insert {1} arch log insert {2} update reg{3} final commit {4}", t0, t1, t2, t3, t4));
                    //oracleCommand = null;
                }
            }

            return ret;
        }


        /*public bool CreateArchLogWithError(PawnDocRegVO vo, int errorCode, string msg, Char errorCodeChar, bool isRecovery,out string oraError)
        {
            bool ret = false;
            string retVal = "";
            oraError = "";
            //OracleDataReader reader = null;
            //OracleConnection connection = null;
            //OracleCommand oracleCommand = null;
            int errorID = 0;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from GetDocumentSets :DB Connection not established: Search aborted");
                return false;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var stopwatch1 = new Stopwatch();
            OracleTransaction myTrans = null;
            long t0 = 0;
            long t1 = 0;
            long t2 = 0;
            long t3 = 0;
            long t4 = 0;

            using (var connection = new OracleConnection())
            {
                try
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    stopwatch1.Start();
                    if (!getConnectionWithRetry(connection))
                    {
                        log.Error("AddErrorsToDB Failed: due to connection error");
                        string str =
                         string.Format("---DB Archive Log Failure Doc ID{0} Storage ID {1} Status {2} Target DB ID{3} Target DB Name{4}  Error Code{5} ErrorMSG {6}---",
                                       vo.DocID, vo.StorageID, errorCodeChar, vo.TargetCouchDBID, vo.TargetCouchDBName, errorCode, msg);
                        log.Error(str);
                        return false;
                    }
                    t0 = stopwatch1.ElapsedMilliseconds;
                    stopwatch1.Restart();
                    //connection.Open();
                    if (errorCode == -1)
                    {
                        errorID = getErrorCodeSeq(connection);
                    }
                    else
                    {
                        errorID = errorCode;
                    }


                    myTrans = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    if (errorCode == -1)
                        insertErrorMSG(connection, msg, errorID);

                    t1 = stopwatch1.ElapsedMilliseconds;
                    stopwatch1.Restart();

                    //log.Debug("Time for Insert Error " + stopwatch.ElapsedMilliseconds);
                    //stopwatch.Restart();
                    if (isRecovery)
                        updateArchLog(connection, vo, errorID, errorCodeChar);
                    else
                        insertArchLog(connection, vo, errorID, errorCodeChar);

                    t2 = stopwatch1.ElapsedMilliseconds;
                    stopwatch1.Restart();
                    //log.Debug("Time for insertArchLog " + stopwatch.ElapsedMilliseconds);
                    // stopwatch.Restart();
                    updatePwnDocReg(connection, vo, errorCodeChar);
                    t3 = stopwatch1.ElapsedMilliseconds;
                    stopwatch1.Restart();
                    //log.Debug("Time for updatePwnDocReg " + stopwatch.ElapsedMilliseconds);
                    myTrans.Commit();

                    t4 = stopwatch1.ElapsedMilliseconds;
                    stopwatch1.Stop();
                    ret = true;
                }
                catch (Exception e)
                {
                    log.Error("AddErrorsToDB Failed:", e);
                    oraError = e.Message;
                    string str =
                           string.Format("---DB Archive Log Failure Doc ID{0} Storage ID {1} Status {2} Target DB ID{3} Target DB Name{4}  Error Code{5} ErrorMSG {6}---",
                                         vo.DocID, vo.StorageID, errorCodeChar, vo.TargetCouchDBID, vo.TargetCouchDBName, errorCode, msg);
                    log.Error(str);
                    log.Debug(e.StackTrace.ToString());
                    if (myTrans != null && connection.State != ConnectionState.Closed)
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
                    ret = false;
                }
                finally
                {
                    stopwatch.Stop();
                    log.Info(string.Format("Time Taken for AddErrorsToDB  : {0} Msec", stopwatch.ElapsedMilliseconds));
                    log.Info(string.Format(" conn {0} error insert {1} arch log insert {2} update reg{3} final commit {4}", t0, t1, t2, t3, t4));
                    //oracleCommand = null;
                }
            }

            return ret;
        }*/

        public bool CreateBatchArchLogWithError(List<ArchiveJob3> jobList, bool isRecovery)
        {
            bool ret = false;
            string retVal = "";
            //OracleDataReader reader = null;
            //OracleConnection connection = null;
            //OracleCommand oracleCommand = null;
            int errorID = 0;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from GetDocumentSets :DB Connection not established: Search aborted");
                return false;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            OracleTransaction myTrans = null;

            using (var connection = new OracleConnection())
            {
                try
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    foreach(var archiveJob3 in jobList)
                    {
                        if (archiveJob3.getErrorCode() == -1)
                        {
                            errorID = getErrorCodeSeq(connection);
                        }
                        else
                        {
                            errorID = archiveJob3.getErrorCode();
                        }
                        int uqID = 0;
                        if (!isRecovery)
                        {
                           uqID = ConnDAO.getInstance().getUniqueIDNew(ref retVal, connection);
                            //insertArchLogNew
                        }

                        myTrans = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                        if (archiveJob3.getErrorCode() == -1)
                            insertErrorMSG(connection, archiveJob3.getErrorMessageForDB(), errorID);

                        if (isRecovery)
                            updateArchLog(connection, archiveJob3.getDocObject(), errorID, archiveJob3.getErrorCodeChar());
                        else
                            insertArchLog(connection, archiveJob3.getDocObject(), errorID, archiveJob3.getErrorCodeChar(), uqID);
                        //log.Debug("Time for insertArchLog " + stopwatch.ElapsedMilliseconds);
                        // stopwatch.Restart();
                        updatePwnDocReg(connection, archiveJob3.getDocObject(), archiveJob3.getErrorCodeChar());
                        //log.Debug("Time for updatePwnDocReg " + stopwatch.ElapsedMilliseconds);
                        myTrans.Commit();
                        ret = true;
                    }
                   
                }
                catch (Exception e)
                {
                    log.Error("AddErrorsToDB Failed:", e);
                    log.Debug(e.StackTrace.ToString());
                    if (myTrans != null && connection.State != ConnectionState.Closed)
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
                    ret = false;
                    throw e;
                }
                finally
                {
                    stopwatch.Stop();
                    log.Debug(string.Format("Time Taken for AddErrorsToDB  : {0} Msec", stopwatch.ElapsedMilliseconds));
                    //oracleCommand = null;
                }
            }

            return ret;
        }

        private void updatePwnDocReg(OracleConnection oracleConnection, PawnDocRegVO vo, char charCode)
        {
            //OracleCommand oracleCommand = null;
            string sql = "";
            if (charCode.Equals(ArchiveJob.CHAR_CODE_SUCCESS))
            {
                sql = "UPDATE CCSOWNER.PAWNDOCUMENTREGISTRY PWNREG SET PWNREG.ARCH_STATUS=:STAT,PWNREG.ARCH_DB_ID=:DBID" +
                    " WHERE PWNREG.ID=:ID";
                
            }else
            {
                sql = "UPDATE CCSOWNER.PAWNDOCUMENTREGISTRY PWNREG SET PWNREG.ARCH_STATUS=:STAT" + " WHERE PWNREG.ID=:ID";
            }
            using(var oracleCommand = new OracleCommand(null, oracleConnection))
            {
                //log.Info(string.Format("Executing: {0}", sql));
                //log.Info(string.Format("Values: id {0} code {1} targetdb{2}", vo.DocID, charCode, vo.TargetCouchDBID));
                oracleCommand.CommandText = sql;
                oracleCommand.Parameters.Add(":STAT", OracleDbType.Char);
                if (charCode.Equals(ArchiveJob.CHAR_CODE_SUCCESS))
                {
                    oracleCommand.Parameters.Add(":DBID", OracleDbType.Int32);
                }
                oracleCommand.Parameters.Add(":ID", OracleDbType.Int32);
                oracleCommand.Prepare();
                
                oracleCommand.Parameters[0].Value = charCode;
                if (charCode.Equals(ArchiveJob.CHAR_CODE_SUCCESS))
                {
                    oracleCommand.Parameters[1].Value = vo.TargetCouchDBID;
                    oracleCommand.Parameters[2].Value = vo.DocID;
                }else
                {
                    oracleCommand.Parameters[1].Value = vo.DocID;
                }

                oracleCommand.ExecuteNonQuery();
                //log.Info(string.Format("Values: id {0} code {1} targetdb{2} done..", vo.DocID, charCode, vo.TargetCouchDBID));
            }
        }

        private void insertArchLog(OracleConnection oracleConnection, PawnDocRegVO vo, int errorCode, char errorCodeChar,int uqID)
        {
            //OracleCommand oracleCommand = null;
            string sql = "INSERT INTO CCSOWNER.PWN_DOC_REG_ARCH_STAT(ID,DOC_REG_ID,STORAGE_ID,ARCH_DATE,USER_ID,STATUS,ERROR_ID,TARGET_DOC_DB)" +
                         " VALUES(:ID,:DOC_REG_ID,:STORAGE_ID,:ARCH_DATE,:USER_ID,:STATUS,:ERROR_ID,:TARGET_DOC_DB)";

            string str = null;
            using(var oracleCommand = new OracleCommand(null, oracleConnection))
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
                oracleCommand.Parameters.Add(":TARGET_DOC_DB", OracleDbType.Varchar2);
                oracleCommand.Prepare();
                //oracleCommand.Parameters[0].Value = ConnDAO.getInstance().getUniqueIDNew(ref str, oracleConnection);
                oracleCommand.Parameters[0].Value = uqID;
                oracleCommand.Parameters[1].Value = vo.DocID;
                oracleCommand.Parameters[2].Value = vo.StorageID;
                oracleCommand.Parameters[3].Value = Utilities.GetTimestampValue(DateTime.Now);
                oracleCommand.Parameters[4].Value = vo.UserID;
                oracleCommand.Parameters[5].Value = errorCodeChar;
                oracleCommand.Parameters[6].Value = errorCode;
                oracleCommand.Parameters[7].Value = vo.TargetCouchDBName;
                //log.Info("executin.." + sql + " " + vo.DocID);
                oracleCommand.ExecuteNonQuery();
                //log.Info("executin.." + sql + " " + vo.DocID+" Done ");
            }
        }


        private void updateArchLog(OracleConnection oracleConnection, PawnDocRegVO vo, int errorCode, char errorCodeChar)
        {
            //OracleCommand oracleCommand = null;
            string sql = "UPDATE CCSOWNER.PWN_DOC_REG_ARCH_STAT SET ARCH_DATE=:ARCH_DATE,STATUS=:STATUS," +
                         "ERROR_ID=:ERROR_ID,TARGET_DOC_DB=:TARGET_DOC_DB WHERE DOC_REG_ID=:DOC_REG_ID";

            using (var oracleCommand = new OracleCommand(null, oracleConnection))
            {
                log.Debug(string.Format("Executing: {0}", sql));
                oracleCommand.CommandText = sql;
                oracleCommand.Parameters.Add(":ARCH_DATE", OracleDbType.TimeStampTZ);
                oracleCommand.Parameters.Add(":STATUS", OracleDbType.Char);
                oracleCommand.Parameters.Add(":ERROR_ID", OracleDbType.Int32);
                oracleCommand.Parameters.Add(":TARGET_DOC_DB", OracleDbType.Varchar2);
                oracleCommand.Parameters.Add(":DOC_REG_ID", OracleDbType.Int32);
                oracleCommand.Prepare();
                oracleCommand.Parameters[0].Value = Utilities.GetTimestampValue(DateTime.Now);
                oracleCommand.Parameters[1].Value = errorCodeChar;
                oracleCommand.Parameters[2].Value = errorCode;
                oracleCommand.Parameters[3].Value = vo.TargetCouchDBName;
                oracleCommand.Parameters[4].Value = vo.DocID;
                //log.Info("executin.." + sql + " " + vo.DocID);
                oracleCommand.ExecuteNonQuery();
                //log.Info("executin.." + sql + " " + vo.DocID+" Done");
                
            }
        }

        private void insertArchLog(OracleConnection oracleConnection, PawnDocRegVO vo, char succCodeChar)
        {
            //OracleCommand oracleCommand = null;
            string sql = "INSERT INTO CCSOWNER.PWN_DOC_REG_ARCH_STAT(ID,DOC_REG_ID,STORAGE_ID,ARCH_DATE,USER_ID,STATUS,TARGET_DOC_DB)" +
                         " VALUES(:ID,:DOC_REG_ID,:STORAGE_ID,:ARCH_DATE,:USER_ID,:STATUS,:TARGET_DOC_DB)";

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
                oracleCommand.Parameters.Add(":TARGET_DOC_DB", OracleDbType.Varchar2);
                oracleCommand.Prepare();
                oracleCommand.Parameters[0].Value = ConnDAO.getInstance().getUniqueID(ref str, oracleConnection);
                oracleCommand.Parameters[1].Value = vo.DocID;
                oracleCommand.Parameters[2].Value = vo.StorageID;
                oracleCommand.Parameters[3].Value = Utilities.GetTimestampValue(DateTime.Now);
                oracleCommand.Parameters[4].Value = vo.UserID;
                oracleCommand.Parameters[5].Value = succCodeChar;
                oracleCommand.Parameters[6].Value = vo.TargetCouchDBName;
                oracleCommand.ExecuteNonQuery();
            }
        }

        private void insertArchLogNew(OracleConnection oracleConnection, PawnDocRegVO vo, char succCodeChar,int uqID )
        {
            //OracleCommand oracleCommand = null;
            string sql = "INSERT INTO CCSOWNER.PWN_DOC_REG_ARCH_STAT(ID,DOC_REG_ID,STORAGE_ID,ARCH_DATE,USER_ID,STATUS,TARGET_DOC_DB)" +
                         " VALUES(:ID,:DOC_REG_ID,:STORAGE_ID,:ARCH_DATE,:USER_ID,:STATUS,:TARGET_DOC_DB)";

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
                oracleCommand.Parameters.Add(":TARGET_DOC_DB", OracleDbType.Varchar2);
                oracleCommand.Prepare();
                oracleCommand.Parameters[0].Value = uqID;
                oracleCommand.Parameters[1].Value = vo.DocID;
                oracleCommand.Parameters[2].Value = vo.StorageID;
                oracleCommand.Parameters[3].Value = Utilities.GetTimestampValue(DateTime.Now);
                oracleCommand.Parameters[4].Value = vo.UserID;
                oracleCommand.Parameters[5].Value = succCodeChar;
                oracleCommand.Parameters[6].Value = vo.TargetCouchDBName;
                oracleCommand.ExecuteNonQuery();
            }
        }

        

        private void updateArchLog(OracleConnection oracleConnection, PawnDocRegVO vo, char succCodeChar)
        {
            //OracleCommand oracleCommand = null;
            /*string sql = "UPDATE CCSOWNER.PWN_DOC_REG_ARCH_STAT SET ARCH_DATE=:ARCH_DATE,STATUS=:STATUS," +
                         "ERROR_ID=:ERROR_ID,TARGET_DOC_DB=:TARGET_DOC_DB WHERE DOC_REG_ID=:DOC_REG_ID";*/

            string sql = "UPDATE CCSOWNER.PWN_DOC_REG_ARCH_STAT SET ARCH_DATE=:ARCH_DATE,STATUS=:STATUS," +
             "TARGET_DOC_DB=:TARGET_DOC_DB WHERE DOC_REG_ID=:DOC_REG_ID";

            using (var oracleCommand = new OracleCommand(null, oracleConnection))
            {
                log.Debug(string.Format("Executing: {0}", sql));
                oracleCommand.CommandText = sql;
                oracleCommand.Parameters.Add(":ARCH_DATE", OracleDbType.TimeStampTZ);
                oracleCommand.Parameters.Add(":STATUS", OracleDbType.Char);
                oracleCommand.Parameters.Add(":TARGET_DOC_DB", OracleDbType.Varchar2);
                oracleCommand.Parameters.Add(":DOC_REG_ID", OracleDbType.Int32);
                oracleCommand.Prepare();
                oracleCommand.Parameters[0].Value = Utilities.GetTimestampValue(DateTime.Now);
                oracleCommand.Parameters[1].Value = succCodeChar;
                oracleCommand.Parameters[2].Value = vo.TargetCouchDBName;
                oracleCommand.Parameters[3].Value = vo.DocID;
                oracleCommand.ExecuteNonQuery();
            }
        }
        private static object errorCodeSeqlockingObject = new Object();
        private int getErrorCodeSeq(OracleConnection oracleConnection)
        {
            int errorID = -1;
            OracleTransaction oracleTransaction = null;
            lock (errorCodeSeqlockingObject)
            {
                try
                {
                    string seqSql = "SELECT CCSOWNER.PWN_DOC_REG_ARCH_ERROR_SEQ.NEXTVAL val FROM DUAL";
                    oracleTransaction = oracleConnection.BeginTransaction();
                    using(var oracleCommand = new OracleCommand(seqSql, oracleConnection))
                    {
                        log.Debug(string.Format("Executing: {0}", seqSql));
                        using(var reader = oracleCommand.ExecuteReader())
                        {
                            if (reader.RowSize > 0 && reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    errorID = Utilities.GetIntegerValue(reader["val"]);
                                }
                            }
                            else
                            {
                                log.Error("AddErrorsToDB Failed due to sequance:");
                                return -1;
                            }
                        }
                        oracleTransaction.Commit();
                    }

                }
                catch(Exception)
                {
                    if (oracleTransaction != null)
                        oracleTransaction.Rollback();
                }
            }
            return errorID;
        }


        private void insertErrorMSG(OracleConnection oracleConnection, string msg,int errorID)
        {
            //OracleCommand oracleCommand = null;
            //OracleDataReader reader = null;
            //int errorID = -1;

            /*string seqSql = "SELECT CCSOWNER.PWN_DOC_REG_ARCH_ERROR_SEQ.NEXTVAL val FROM DUAL";
            using(var oracleCommand = new OracleCommand(seqSql, oracleConnection))
            {
                log.Debug(string.Format("Executing: {0}", seqSql));
                using (var reader = oracleCommand.ExecuteReader())
                {
                    if (reader.RowSize > 0 && reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            errorID = PawnUtilities.Utilities.GetIntegerValue(reader["val"]);
                        }
                    }
                    else
                    {
                        log.Error("AddErrorsToDB Failed due to sequance:");
                        return -1;
                    }
                }
            }*/

            string sql = "INSERT INTO CCSOWNER.PWN_DOC_REG_ARCH_ERROR(ID,ERROR_MSG) VALUES " + "(:ID,:ERROR_MSG)";

            //string str = null;
            using(var oracleCommand = new OracleCommand(null, oracleConnection))
            {
                log.Debug(string.Format("Executing: {0}", sql));
                oracleCommand.CommandText = sql;
                oracleCommand.Parameters.Add(":ID", OracleDbType.Int32);
                oracleCommand.Parameters.Add(":ERROR_MSG", OracleDbType.Varchar2);
                oracleCommand.Prepare();
                oracleCommand.Parameters[0].Value = errorID;
                oracleCommand.Parameters[1].Value = msg;
                oracleCommand.ExecuteNonQuery();
            }

            //return errorID;
        }
    }
}