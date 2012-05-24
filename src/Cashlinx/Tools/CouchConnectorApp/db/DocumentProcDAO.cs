using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common.Libraries.Utility;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace CouchConsoleApp.db
{
    class DocumentProcDAO
    {
        private static readonly DocumentProcDAO daoinst = new DocumentProcDAO();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(DocumentProcDAO));

        private OracleConnection connection = null;
        private OracleCommand oracleCommand = null;
        private OracleDataReader reader = null;

        public static DocumentProcDAO getInstance()
        {
            return daoinst;
        }

        public DataTable GetDocuments(int startWith,out int totalCount,out int errorCode,out string errorMSG)
        {
            OracleDataReader reader = null;
            totalCount = 0;
            errorCode = 0;
            errorMSG = "";
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Error(" from GetDocuments :DB Connection not established: Search aborted");
                return null;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
  
            DataTable tbl=new DataTable("ARCHIVEDOCUMENT");
            try
            {
                using(connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    using(oracleCommand = new OracleCommand(null, connection))
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
                        log.Debug("Executing..:" + oracleCommand.CommandText);
                        oracleCommand.ExecuteNonQuery();

                        log.Debug("Input " + startWith);

                        totalCount = Utilities.GetIntegerValue(oracleCommand.Parameters[3].Value);
                        errorCode = Utilities.GetIntegerValue(oracleCommand.Parameters[5].Value);
                        errorMSG = Utilities.GetStringValue(oracleCommand.Parameters[6].Value);
                        log.Debug("Total Documents " + totalCount);
                        log.Debug("o_return_code " + errorCode);
                        log.Debug("o_return_text " + errorMSG);

                        var oraRefCursor = (OracleRefCursor)oracleCommand.Parameters[4].Value;
                        if (oraRefCursor != null && oraRefCursor.IsNull == false)
                        {
                            reader = oraRefCursor.GetDataReader();
                            if (reader.HasRows)
                            {
                                reader.FetchSize = oraRefCursor.RowSize * 100;
                                tbl.Load(reader);

                            }
                        }
                        log.Debug("Count of Results :"+tbl.Rows.Count);
                    }

                }
            }
            catch (Exception e)
            {
                log.Error("GetDocuments", e);
               
              
                
            }finally
            {
                stopwatch.Stop();
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                log.Info(string.Format("Time Taken for Get Doc Count SP   : {0} Msec", stopwatch.ElapsedMilliseconds));
                oracleCommand = null;
                if(oracleCommand!=null)
                {
                    oracleCommand.Cancel();
                    oracleCommand = null;
                }
                if(connection!=null)
                {
                    connection.Close();
                    connection = null;
                }

            }
            log.Debug("Data Returned.." + tbl);
            return tbl;
        }
    }
}
