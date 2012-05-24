using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common.Libraries.Utility;
using CouchConsoleApp.vo;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace CouchConsoleApp.db
{
    class DocumentDAO
    {

        private static readonly DocumentDAO daoinst = new DocumentDAO();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(DocumentDAO));
        //private OracleConnection connection = null;
        //private OracleCommand oracleCommand = null;
        //private OracleDataReader reader = null;

        public static DocumentDAO getInstance()
        {
            return daoinst;
        }

        public void killCommand()
        {
            /*log.Debug("Aborting Execution of DocRegListDAO");
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
            }
            catch (Exception e)
            {
                log.Error("Error during aborting DocRegListDAO", e);
            }*/

        }


        public bool GetTempGetDocsIDs(ref List<PawnDocRegVO> docList, int fetchCount, bool isFirst, int lastIndex)
        {
            bool ret = true;
            string retVal = "";
            OracleDataReader reader = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from docListGet :DB Connection not established: Search aborted");
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
                    string sql = "";
                    if (isFirst)
                    {
                        sql = "SELECT REG.ID,REG.STORAGE_ID,CREATIONDATE FROM CCSOWNER.PAWNDOCUMENTREGISTRY REG" +
                            " WHERE REG.CREATIONDATE <= TRUNC(SYSDATE) - 90 AND REG.ARCH_STATUS IS NULL" + " AND ROWNUM<=" +
                            fetchCount + " ORDER BY ID";
                    }
                    else
                    {
                        sql = "SELECT REG.ID,REG.STORAGE_ID,CREATIONDATE FROM CCSOWNER.PAWNDOCUMENTREGISTRY REG" +
                                " WHERE REG.CREATIONDATE <= TRUNC(SYSDATE) - 90 AND REG.ARCH_STATUS IS NULL AND REG.ID > " + lastIndex +
                                " AND ROWNUM <=" + fetchCount + " ORDER BY REG.ID";
                    }
                    using (var oracleCommand = new OracleCommand(sql, connection))
                    {
                        log.Info(string.Format("Executing: {0}", sql));
                        reader = oracleCommand.ExecuteReader();

                        if (reader.RowSize > 0 && reader.HasRows)
                        {
                            docList = new List<PawnDocRegVO>();
                            CouchConsoleApp.vo.PawnDocRegVO vo = null;
                            while (reader.Read())
                            {
                                vo = new CouchConsoleApp.vo.PawnDocRegVO();
                                vo.DocID = Utilities.GetIntegerValue(reader["ID"]);
                                vo.StorageID = Utilities.GetStringValue(reader["STORAGE_ID"]);
                                vo.CreationDate = Utilities.GetDateTimeValue(reader["CREATIONDATE"]);
                                docList.Add(vo);
                            }
                            ret = false;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Recovery document get failed:", e);
                log.Error(e.StackTrace);
                //log.Debug(e.StackTrace.ToString());
            }
            finally
            {
                stopwatch.Stop();
                log.Info(string.Format("Time Taken for Recovery doc get  : {0} Msec", stopwatch.Elapsed));
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                //oracleCommand = null;
            }
            return ret;
        }

        public bool GetTempGetDocsIDs_ForAdd(ref List<PawnDocRegVO> docList, int fetchCount, bool isFirst, int lastIndex)
        {
            bool ret = false;
            string retVal = "";
            //OracleDataReader reader = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from docListGet :DB Connection not established: Search aborted");
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
                    string sql = "";
                    if (isFirst)
                    {
                        sql = "SELECT REG.ID,REG.STORAGE_ID,CREATIONDATE FROM CCSOWNER.PAWNDOCUMENTREGISTRY REG" +
                            " WHERE REG.CREATIONDATE <= TRUNC(SYSDATE) - 90 AND REG.ARCH_STATUS IS NULL" + " AND ROWNUM<=" +
                            fetchCount + " ORDER BY ID";
                    }
                    else
                    {
                        sql = "SELECT REG.ID, REG.STORAGE_ID,CREATIONDATE FROM CCSOWNER.PAWNDOCUMENTREGISTRY REG" +
                               " WHERE REG.CREATIONDATE <= TRUNC(SYSDATE) - 90 AND REG.ARCH_STATUS IS NULL AND REG.ID > " + lastIndex +
                               " AND ROWNUM <=" + fetchCount + " ORDER BY REG.ID";
                    }
                    using (var oracleCommand = new OracleCommand(sql, connection))
                    {
                        log.Info(string.Format("Executing: {0}", sql));
                        using (var reader = oracleCommand.ExecuteReader())
                        {

                            if (reader.RowSize > 0 && reader.HasRows)
                            {
                                docList = new List<PawnDocRegVO>();
                                CouchConsoleApp.vo.PawnDocRegVO vo = null;
                                while(reader.Read())
                                {
                                    vo = new CouchConsoleApp.vo.PawnDocRegVO();
                                    vo.DocID = Utilities.GetIntegerValue(reader["ID"]);
                                    vo.StorageID = Utilities.GetStringValue(reader["STORAGE_ID"]);
                                    vo.CreationDate = Utilities.GetDateTimeValue(reader["CREATIONDATE"]);
                                    docList.Add(vo);
                                }
                                ret = true;
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Recovery document get failed:", e);
                log.Error(e.StackTrace);
                //log.Debug(e.StackTrace.ToString());
            }
            finally
            {
                stopwatch.Stop();
                log.Info(string.Format("Time Taken for Recovery doc get  : {0} Msec", stopwatch.Elapsed));
                /*if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                oracleCommand = null;*/
            }
            return ret;
        }

        public bool GetDocumentSets(ref List<PawnDocRegVO> docList, int fetchCount,bool isFirst,int lastIndex)
        {
            bool ret = false;
            string retVal = "";
           // OracleDataReader reader = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from docListGet :DB Connection not established: Search aborted");
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
                    string sql = "";
                    if (isFirst)
                    {
                        sql = "SELECT REG.ID,REG.STORAGE_ID,CREATIONDATE FROM CCSOWNER.PAWNDOCUMENTREGISTRY REG" +
                              " WHERE REG.ARCH_STATUS IN ('G','A')" + " AND ROWNUM<=" +
                              fetchCount + " ORDER BY ID";
                    }else
                    {
                        sql = "SELECT REG.ID,REG.STORAGE_ID,CREATIONDATE FROM  CCSOWNER.PAWNDOCUMENTREGISTRY REG" +
                              " WHERE REG.ARCH_STATUS IN ('G','A') AND REG.ID > " + lastIndex +
                              " AND ROWNUM <=" + fetchCount + " ORDER BY REG.ID";
                    }
                    using (var oracleCommand = new OracleCommand(sql, connection))
                    {
                        log.Info(string.Format("Executing: {0}", sql));
                        using (var reader = oracleCommand.ExecuteReader())
                        {

                            if (reader.RowSize > 0 && reader.HasRows)
                            {
                                docList = new List<PawnDocRegVO>();
                                CouchConsoleApp.vo.PawnDocRegVO vo = null;
                                while(reader.Read())
                                {
                                    vo = new CouchConsoleApp.vo.PawnDocRegVO();
                                    vo.DocID = Utilities.GetIntegerValue(reader["ID"]);
                                    vo.StorageID = Utilities.GetStringValue(reader["STORAGE_ID"]);
                                    vo.CreationDate = Utilities.GetDateTimeValue(reader["CREATIONDATE"]);
                                    docList.Add(vo);
                                }
                                ret = true;
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Recovery document get failed:", e);
                log.Error(e.StackTrace);
                //log.Debug(e.StackTrace.ToString());
            }
            finally
            {
                stopwatch.Stop();
                log.Info(string.Format("Time Taken for Recovery doc get  : {0} Msec", stopwatch.Elapsed));
                /*if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }*/
               // oracleCommand = null;
            }
            return ret;
        }

        public bool GetDocumentSets(ref List<PawnDocRegVO> docList)
        {
            bool ret = false;
            string retVal = "";
            OracleDataReader reader = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from docList for recovery :DB Connection not established: Search aborted");
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
                    string sql = "SELECT REG.ID,REG.STORAGE_ID,CREATIONDATE FROM CCSOWNER.PAWNDOCUMENTREGISTRY REG" +
                                 " WHERE REG.ARCH_STATUS IN ('G','A') ORDER BY ID";

                    /*string sql = "SELECT REG.ID,REG.STORAGE_ID,CREATIONDATE FROM CCSOWNER.PAWNDOCUMENTREGISTRY REG" +
                                " WHERE REG.ARCH_STATUS IN ('A') ORDER BY ID";*/
                    
                    using (var oracleCommand = new OracleCommand(sql, connection))
                    {
                        log.Info(string.Format("Executing: {0}", sql));
                        reader = oracleCommand.ExecuteReader();

                        if (reader.RowSize > 0 && reader.HasRows)
                        {
                            docList = new List<PawnDocRegVO>();
                            CouchConsoleApp.vo.PawnDocRegVO vo = null;
                            while (reader.Read())
                            {
                                vo = new CouchConsoleApp.vo.PawnDocRegVO();
                                vo.DocID = Utilities.GetIntegerValue(reader["ID"]);
                                vo.StorageID = Utilities.GetStringValue(reader["STORAGE_ID"]);
                                vo.CreationDate = Utilities.GetDateTimeValue(reader["CREATIONDATE"]);
                                docList.Add(vo);
                            }
                            ret = true;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Recovery document get failed:", e);
                log.Error(e.StackTrace);
                //log.Debug(e.StackTrace.ToString());
            }
            finally
            {
                stopwatch.Stop();
                log.Info(string.Format("Time Taken for Recovery doc get  : {0} Msec", stopwatch.Elapsed));
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                //oracleCommand = null;
            }
            return ret;
        }



        public bool GetDocumentSets(int startWith, out List<PawnDocRegVO> docList, out int totalCount, out int errorCode, out string errorMSG)
        {
            //OracleDataReader reader = null;
            totalCount = 0;
            errorCode = 0;
            errorMSG = "";
            bool ret = false;
            docList = new List<PawnDocRegVO>();
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from docListGet :DB Connection not established: Search aborted");
                return false;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            DataTable tbl = new DataTable("ARCHIVEDOCUMENT");
            try
            {
                using (var connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    using (var oracleCommand = new OracleCommand(null, connection))
                    {
                        oracleCommand.CommandText = "ccsowner.PAWN_GENERATE_DOCUMENTS.getDocumentsToArchive";
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        oracleCommand.Parameters.Add("p_current_date", OracleDbType.Varchar2, DateTime.Now.ToShortDateString(), ParameterDirection.Input);
                        oracleCommand.Parameters.Add("p_time_span", OracleDbType.Int32, 90, ParameterDirection.Input);
                        oracleCommand.Parameters.Add("p_record_start", OracleDbType.Int32, startWith, ParameterDirection.Input);
                        oracleCommand.Parameters.Add("o_total_documents", OracleDbType.Int32, DBNull.Value, ParameterDirection.Output);
                        oracleCommand.Parameters.Add("o_documents", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output);
                        oracleCommand.Parameters.Add("o_return_code", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output);
                        oracleCommand.Parameters.Add("o_return_text", OracleDbType.Varchar2, 32768, DBNull.Value, ParameterDirection.Output);
                        log.Info("Executing..:" + oracleCommand.CommandText);
                        log.Info("Input " + startWith);
                        oracleCommand.ExecuteNonQuery();
                        totalCount = Utilities.GetIntegerValue(oracleCommand.Parameters[3].Value);
                        errorCode = Utilities.GetIntegerValue(oracleCommand.Parameters[5].Value);
                        errorMSG = Utilities.GetStringValue(oracleCommand.Parameters[6].Value);
                        log.Info("Total Documents " + totalCount);
                        log.Info("o_return_code " + errorCode);
                        log.Info("o_return_text " + errorMSG);

                        if (errorCode != 0)
                        {
                            return false;
                        }

                        var oraRefCursor = (OracleRefCursor)oracleCommand.Parameters[4].Value;
                        if (oraRefCursor != null && oraRefCursor.IsNull == false)
                        {
                            using (var reader = oraRefCursor.GetDataReader())
                            {
                                if (reader.RowSize > 0 && reader.HasRows)
                                {
                                    CouchConsoleApp.vo.PawnDocRegVO vo = null;

                                    while(reader.Read())
                                    {
                                        vo = new CouchConsoleApp.vo.PawnDocRegVO();
                                        vo.DocID = Utilities.GetIntegerValue(reader["ID"]);
                                        vo.StorageID = Utilities.GetStringValue(reader["STORAGE_ID"]);
                                        vo.CreationDate = Utilities.GetDateTimeValue(reader["STORAGE_DATE"]);
                                        docList.Add(vo);
                                    }
                                    ret = true;
                                }
                            }
                        }
                        oraRefCursor.Dispose();
                    }
                    log.Info("Count from Proc " + docList.Count);
                }
            }
            catch (Exception e)
            {
                log.Error("GetDocumentSets", e);
                errorMSG = e.Message;
                ret = false;
            }
            finally
            {
                stopwatch.Stop();
                /*if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }*/
                log.Info(string.Format("Time Taken for GetDocumentSets  : {0} Msec", stopwatch.ElapsedMilliseconds));
                //oracleCommand = null;
            }
            log.Debug("Data Returned.." + tbl);
            return ret;
        }

       /* public bool GetDocumentSets1(int startWith, out List<PawnDocRegVO> docList, out int totalCount, out int errorCode, out string errorMSG)
        {
            //OracleDataReader reader = null;
            totalCount = 0;
            errorCode = 0;
            errorMSG = "";
            bool ret = false;
            docList=new List<PawnDocRegVO>();
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from docListGet :DB Connection not established: Search aborted");
                return false;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            //DataTable tbl = new DataTable("ARCHIVEDOCUMENT");
            try
            {
                using (var connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    using (var oracleCommand = new OracleCommand(null, connection))
                    {
                        oracleCommand.CommandText = "ccsowner.PAWN_GENERATE_DOCUMENTS.getDocumentsToArchive";
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        oracleCommand.Parameters.Add("p_current_date", OracleDbType.Varchar2, DateTime.Now.ToShortDateString(), ParameterDirection.Input);
                        oracleCommand.Parameters.Add("p_time_span", OracleDbType.Int32, 90, ParameterDirection.Input);
                        oracleCommand.Parameters.Add("p_record_start", OracleDbType.Int32, startWith, ParameterDirection.Input);
                        oracleCommand.Parameters.Add("o_documents", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output);
                        oracleCommand.Parameters.Add("o_return_code", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output);
                        oracleCommand.Parameters.Add("o_return_text", OracleDbType.Varchar2, 32768, DBNull.Value, ParameterDirection.Output);
                        log.Info("Executing..:" + oracleCommand.CommandText);
                        log.Info("Input " + startWith);
                        oracleCommand.ExecuteNonQuery();
                        errorCode = PawnUtilities.Utilities.GetIntegerValue(oracleCommand.Parameters[4].Value);
                        errorMSG = PawnUtilities.Utilities.GetStringValue(oracleCommand.Parameters[5].Value);
                        //log.Info("Total Documents " + totalCount);
                        log.Info("o_return_code " + errorCode);
                        log.Info("o_return_text " + errorMSG);

                        if(errorCode!=0)
                        {
                            return false;
                        }

                        var oraRefCursor = (OracleRefCursor)oracleCommand.Parameters[3].Value;
                        if (oraRefCursor != null && oraRefCursor.IsNull == false)
                        {
                            using (var reader = oraRefCursor.GetDataReader())
                            {
                                if (reader.RowSize > 0 && reader.HasRows)
                                {
                                    CouchConsoleApp.vo.PawnDocRegVO vo = null;

                                    while(reader.Read())
                                    {
                                        vo = new CouchConsoleApp.vo.PawnDocRegVO();
                                        vo.DocID = PawnUtilities.Utilities.GetIntegerValue(reader["ID"]);
                                        vo.StorageID = PawnUtilities.Utilities.GetStringValue(reader["STORAGE_ID"]);
                                        vo.CreationDate = PawnUtilities.Utilities.GetDateTimeValue(reader["STORAGE_DATE"]);
                                        docList.Add(vo);
                                    }
                                    ret = true;
                                }
                            }
                        }
                    }
                    log.Info("Count from Proc " + docList.Count);
                }
            }
            catch (Exception e)
            {
                log.Error("GetDocumentSets", e);
                errorMSG = e.Message;
                ret = false;
            }
            finally
            {
                stopwatch.Stop();
                
                log.Info(string.Format("Time Taken for GetDocumentSets  : {0} Msec", stopwatch.ElapsedMilliseconds));
                //oracleCommand = null;
            }
            //log.Debug("Data Returned.." + tbl);
            return ret;
        }*/


        public bool CountProcTest(int startWith, out List<PawnDocRegVO> docList, out int totalCount, out int errorCode, out string errorMSG)
        {
            OracleDataReader reader = null;
            totalCount = 0;
            errorCode = 0;
            errorMSG = "";
            bool ret = false;
            docList = new List<PawnDocRegVO>();
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from docListGet :DB Connection not established: Search aborted");
                return false;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            DataTable tbl = new DataTable("ARCHIVEDOCUMENT");
            try
            {
                using (var connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    using (var oracleCommand = new OracleCommand(null, connection))
                    {
                        oracleCommand.CommandText = "ccsowner.sree_pawn_generate_documents.getArchiveDocumentsCount";
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        oracleCommand.Parameters.Add("p_current_date", OracleDbType.Varchar2, DateTime.Now.ToShortDateString(), ParameterDirection.Input);
                        oracleCommand.Parameters.Add("p_time_span", OracleDbType.Int32, 90, ParameterDirection.Input);
                        oracleCommand.Parameters.Add("o_total_documents", OracleDbType.Int32, DBNull.Value, ParameterDirection.Output);
                        oracleCommand.Parameters.Add("o_return_code", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output);
                        oracleCommand.Parameters.Add("o_return_text", OracleDbType.Varchar2, 32768, DBNull.Value, ParameterDirection.Output);
                        log.Info("Executing..:" + oracleCommand.CommandText);
                        log.Info("Input " + startWith);
                        oracleCommand.ExecuteNonQuery();
                        totalCount = Utilities.GetIntegerValue(oracleCommand.Parameters[2].Value);
                        errorCode = Utilities.GetIntegerValue(oracleCommand.Parameters[3].Value);
                        errorMSG = Utilities.GetStringValue(oracleCommand.Parameters[4].Value);
                        log.Info("Total Documents " + totalCount);
                        log.Info("o_return_code " + errorCode);
                        log.Info("o_return_text " + errorMSG);

                        if (errorCode != 0)
                        {
                            return false;
                        }
 
                    }

                }
            }
            catch (Exception e)
            {
                log.Error("GetDocumentSets", e);
                errorMSG = e.Message;
                ret = false;
            }
            finally
            {
                stopwatch.Stop();
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                log.Info(string.Format("Time Taken for GetDocumentSets  : {0} Msec", stopwatch.ElapsedMilliseconds));
                //oracleCommand = null;
            }
            log.Debug("Data Returned.." + tbl);
            return ret;
        }

        public bool GetDocumentSetsForCount(int startWith, out int pendCount, out int errorCode, out string errorMsg)
        {
            //OracleDataReader reader = null;
            pendCount = 0;
            errorCode = 0;
            errorMsg = "";
            bool ret = false;
            List<PawnDocRegVO> voList = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from docListGet :DB Connection not established: Search aborted");
                return false;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            DataTable tbl = new DataTable("ARCHIVEDOCUMENT");
            try
            {
                using (var connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    using(var oracleCommand = new OracleCommand(null, connection))
                    {
                        oracleCommand.CommandText = "ccsowner.PAWN_GENERATE_DOCUMENTS.getDocumentsToArchive";
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        oracleCommand.Parameters.Add("p_current_date", OracleDbType.Varchar2, DateTime.Now.ToShortDateString(), ParameterDirection.Input);
                        oracleCommand.Parameters.Add("p_time_span", OracleDbType.Int32, 90, ParameterDirection.Input);
                        oracleCommand.Parameters.Add("p_record_start", OracleDbType.Int32, startWith, ParameterDirection.Input);
                        oracleCommand.Parameters.Add("o_total_documents", OracleDbType.Int32, DBNull.Value, ParameterDirection.Output);
                        oracleCommand.Parameters.Add("o_documents", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output);
                        oracleCommand.Parameters.Add("o_return_code", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output);
                        oracleCommand.Parameters.Add("o_return_text", OracleDbType.Varchar2, 32768, DBNull.Value, ParameterDirection.Output);
                        log.Info("Executing..:" + oracleCommand.CommandText);
                        log.Info("Input " + startWith);
                        oracleCommand.ExecuteNonQuery();
                        pendCount = Utilities.GetIntegerValue(oracleCommand.Parameters[3].Value);
                        errorCode = Utilities.GetIntegerValue(oracleCommand.Parameters[5].Value);
                        errorMsg = Utilities.GetStringValue(oracleCommand.Parameters[6].Value);
                        log.Info("Total Documents " + pendCount);
                        log.Info("o_return_code " + errorCode);
                        log.Info("o_return_text " + errorMsg);

                        if (errorCode != 0)
                        {
                            return false;
                        }

                    }

                }
            }
            catch (Exception e)
            {
                log.Error("GetDocumentSets", e);
                errorMsg = e.Message;
                ret = false;
            }
            finally
            {
                stopwatch.Stop();
                /*if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }*/
                log.Info(string.Format("Time Taken for GetDocumentSetsForCount  : {0} Msec", stopwatch.ElapsedMilliseconds));
                //oracleCommand = null;
            }
            log.Debug("Data Returned.." + tbl);
            return ret;
        }
    }
}
