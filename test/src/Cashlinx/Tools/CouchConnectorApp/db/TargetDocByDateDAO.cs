using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace CouchConsoleApp.db
{
    class TargetDocByDateDAO
    {

        private static readonly TargetDocByDateDAO daoinst = new TargetDocByDateDAO();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(TargetDocByDateDAO));
        //private OracleConnection connection = null;
        //private OracleCommand oracleCommand = null;
        //private OracleDataReader reader = null;

        private TargetDocByDateDAO()
        {
        }

        public static TargetDocByDateDAO getInstance()
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

        public bool docTargetDocsListByDate(ref DataTable table)
        {
            bool ret = false;
            string retVal = "";
            //OracleDataReader reader = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED) return false;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using (var connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    string sql =
                            "SELECT ID,STORAGE_DATE,TICKET_NUMBER,STORENUMBER FROM CCSOWNER.PAWNDOCUMENTREGISTRY  REG WHERE REG.STORAGE_DATE <= TRUNC(SYSDATE) - 90" +
                            " ORDER BY REG.STORAGE_DATE DESC";
                    using (var oracleCommand = new OracleCommand(sql, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        // oracleCommand.FetchSize = 100;
                        using (var reader = oracleCommand.ExecuteReader())
                        {
                            log.Debug("Execution Completed..........");

                            if (reader.RowSize > 0 && reader.HasRows)
                            {
                                //Create data table with same table name as the one being queried
                                table = new DataTable("PAWNDOCUMENTREGISTRY");
                                log.Debug("Loading data......for docTargetDocsListByDate");
                                //Load the data table with the reader data
                                table.Load(reader, LoadOption.OverwriteChanges);
                                log.Debug("Loading Complete......for docTargetDocsListByDate");
                                ret = true;
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                //msg = e.Message;
                log.Error("docListGet Failed:", e);
                log.Debug(e.StackTrace.ToString());
            }
            finally
            {
                stopwatch.Stop();
                log.Debug(string.Format("Time Taken for docListGet  : {0} Msec", stopwatch.Elapsed));
                /*if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }*/
                //oracleCommand = null;
            }
            return ret;
        }

    }
}
