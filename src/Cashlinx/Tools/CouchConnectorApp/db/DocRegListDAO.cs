using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace CouchConsoleApp.db
{
    internal class DocRegListDAO
    {
        private static readonly DocRegListDAO daoinst = new DocRegListDAO();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(DocRegListDAO));
        private OracleConnection connection = null;
        private OracleCommand oracleCommand = null;
        private OracleDataReader reader = null;

        private bool moveLastClicked = false;

        public bool moveLastFlag()
        {
            return moveLastClicked;
        }

        private DocRegListDAO()
        {
        }

        public static DocRegListDAO getInstance()
        {
            return daoinst;
        }

        public void killCommand()
        {
            log.Debug("Aborting Execution of DocRegListDAO");
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
            catch(Exception e)
            {
                log.Error("Error during aborting DocRegListDAO", e);
            }
        }

        public bool docListGet(ref DataTable table, int recCount)
        {
            bool ret = false;
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
                using(connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    string sql = "SELECT  ROWNUM,ID,STORAGE_DATE,TICKET_NUMBER,STORENUMBER,CREATIONDATE FROM CCSOWNER.PAWNDOCUMENTREGISTRY REG" +
                                 " WHERE REG.STORAGE_DATE <= TRUNC(SYSDATE) - 90 AND ARCH_STATUS IS NULL AND ROWNUM<=" + recCount + " ORDER BY ID";
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
                            log.Debug("Loading data......docListGet");
                            //Load the data table with the reader data
                            table.Load(reader, LoadOption.OverwriteChanges);
                            log.Debug("Loading Complete......docListGet");
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
        }

        public bool docListNext(ref DataTable table, int rowid, int recCount)
        {
            bool ret = false;
            string retVal = "";
            OracleDataReader reader = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                return false;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using(connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    string sql = "SELECT ROWNUM,ID,STORAGE_DATE,TICKET_NUMBER,STORENUMBER,CREATIONDATE FROM PAWNDOCUMENTREGISTRY  REG " +
                                 "WHERE REG.STORAGE_DATE <= TRUNC(SYSDATE) - 90 AND ID > " + rowid + " AND ROWNUM<=" + rowid +
                                 " ORDER BY ID";

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
                            log.Debug("Loading data......docListGet");
                            //Load the data table with the reader data
                            table.Load(reader, LoadOption.OverwriteChanges);
                            log.Debug("Loading Complete......docListGet");
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
        }

        public bool docListPrev(ref DataTable table, int rowid, int recCount)
        {
            bool ret = false;
            string retVal = "";
            OracleDataReader reader = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                return false;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using(connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    string sql = "SELECT  ROWNUM,ID,STORAGE_DATE,TICKET_NUMBER,STORENUMBER,CREATIONDATE FROM PAWNDOCUMENTREGISTRY  REG " +
                                 "WHERE REG.STORAGE_DATE <= TRUNC(SYSDATE) - 90 AND ID < " + rowid + " AND ROWNUM<=" + rowid +
                                 " ORDER BY ID";

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
                            log.Debug("Loading data......docListGet");
                            //Load the data table with the reader data
                            table.Load(reader, LoadOption.OverwriteChanges);
                            log.Debug("Loading Complete......docListGet");
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
        }

        public bool docListLast(ref DataTable table, int recCount)
        {
            bool ret = false;
            string retVal = "";
            OracleDataReader reader = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                return false;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using(connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    string sql = "SELECT  ROWNUM,ID,STORAGE_DATE,TICKET_NUMBER,STORENUMBER,CREATIONDATE FROM PAWNDOCUMENTREGISTRY  REG " +
                                 "WHERE REG.STORAGE_DATE <= TRUNC(SYSDATE) - 90 AND ROWNUM<=" + recCount + " ORDER BY ID DESC";

                    using(oracleCommand = new OracleCommand(sql, connection))
                    {
                        log.Debug(string.Format("Executing: {0}", sql));
                        // oracleCommand.FetchSize = 100;
                        log.Debug("Loading data......docListLast");
                        reader = oracleCommand.ExecuteReader();
                        //reader.FetchSize = recCount;
                        log.Debug("Execution Completed..........");

                        if (reader.RowSize > 0 && reader.HasRows)
                        {
                            //Create data table with same table name as the one being queried
                            table = new DataTable("PAWNDOCUMENTREGISTRY");
                            log.Debug("Loading data......docListGet");
                            //Load the data table with the reader data
                            table.Load(reader, LoadOption.OverwriteChanges);
                            log.Debug("Loading Complete......docListGet");
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
        }
    }
}