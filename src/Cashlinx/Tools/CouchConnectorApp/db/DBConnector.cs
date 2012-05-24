using System;
using System.Text;
using Oracle.DataAccess.Client;

namespace CouchConsoleApp.db
{
    public class DBConnector
    {
        #region Status enum
        public enum Status
        {
            INITIALIZED,
            CONNECTED,
            DISCONNECTED
        } ;
        #endregion

        public const string DS_WILDCARD = "?";
        public const string DS_DATASRC = "data source=";
        public const string SLA = "/";
        public const string CLN = ":";
        public static readonly string DS_USERID = "user ID=" + DS_WILDCARD + ";";
        public static readonly string DS_PASSWORD = "password=" + DS_WILDCARD + ";";
        //private static DBConnector dbConnIns;
        private string connLife;
        private string connTout;
        private string dataService;

        private string databaseHost;
        private string databasePort;
        private string databaseSchema;
        private string databaseService;
        private string decrPoolSize;
        private string incrPoolSize;
        private string minPoolSize;
        private string mxPoolSize;
        private OracleConnection oracleConnection;
        private string password;
        private Status status;
        private string userName;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(DBConnector)); 
        private DBConnector()
        {
            this.oracleConnection = null;
            this.userName = string.Empty;
            this.password = string.Empty;
            this.databaseHost = string.Empty;
            this.databasePort = string.Empty;
            this.databaseService = string.Empty;
            this.databaseSchema = string.Empty;
            this.status = Status.DISCONNECTED;
            this.minPoolSize = string.Empty;
            this.mxPoolSize = string.Empty;
            this.connLife = string.Empty;
            this.connTout = string.Empty;
            this.incrPoolSize = string.Empty;
            this.decrPoolSize = string.Empty;
        }

        public Status getStatus()
        {
            return this.status;
        }
        private static readonly DBConnector dbConnIns = new DBConnector();

        public static  DBConnector getInstance()
        {
           
            return dbConnIns;
        }

        public string serverName()
        {
            return this.databaseHost;
        }

        public string serverSID()
        {
            return this.databaseService;
        }

        public string databaseServiceName()
        {
            return this.dataService;
        }

        public bool initialize(string userNm, string passwd, string dbaseHost, string dbasePort, string dbaseService, string dbaseSchema,
                               bool useConnPool,string minPoolSize, string mxPoolSize, string connLife, string connTout, string incrPoolSize,
                               string decrPoolSize,out string retVal)
        {
            bool rt = false;
            this.userName = userNm;
            this.password = passwd;
            this.databaseHost = dbaseHost;
            this.databasePort = dbasePort;
            this.databaseService = dbaseService;
            this.databaseSchema = dbaseSchema;
            this.minPoolSize = minPoolSize;
            this.mxPoolSize = mxPoolSize;
            this.connLife = connLife;
            this.connTout = connTout;
            this.incrPoolSize = incrPoolSize;
            this.decrPoolSize = decrPoolSize;
            retVal = "";
            if (generateDataService(useConnPool))
            {
                //this.appendStatementCaching();
                if (TestConnect(ref retVal))
                {
                    this.status = Status.INITIALIZED;
                    rt = true;
                }else
                {
                    this.status = Status.DISCONNECTED;
                }
            }
           /* if (this.status == Status.DISCONNECTED)
            {
                if (generateDataService(useConnPool))
                {
                    //this.appendStatementCaching();
                    this.status = Status.INITIALIZED;
                    rt = true;
                }
            }
            else if (this.status == Status.INITIALIZED)
            {
                rt = true;
            }
            else if (this.status == Status.CONNECTED)
            {
                rt = true;
            }*/
            log.Info("initialization called :"+this.status);
            return (rt);
        }

        public void testCreate()
        {
            //Console.WriteLine(getSeqVal());
        }

        /*public int getSeqVal()
        {
            try
            {
                using(var connection = new OracleConnection())
                {
                    connection.ConnectionString = this.dataService;
                    connection.Open();
                    Console.WriteLine("State: {0}", connection.State);
                    Console.WriteLine("ConnectionString: {0}", connection.ConnectionString);

                    using (var command = connection.CreateCommand())
                    {
                        string sql = "SELECT CCSOWNER.PWN_DOC_REG_ARCH_SEQ.NEXTVAL FROM DUAL";
                        command.CommandText = sql;

                        using (var reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                var myField = ((int)reader["CCSOWNER.PWN_DOC_REG_ARCH_SEQ.NEXTVAL"]);
                                return myField;
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                return -1;
            }
            return -1;
        }*/

        private void InvokeCommand(OracleConnection oracleConnection, string tableCommand)
        {
            using(var oracleCommand = new OracleCommand(tableCommand, oracleConnection))
            {
                oracleCommand.ExecuteNonQuery();
            }
        }

        public bool TestConnect(ref string retVal)
        {
            bool ret = false;
            retVal = "";
            try
            {
                /*string CONNECTION_STRING = 
                "User Id=ccsowner;Password=prime98s;Data Source=(DESCRIPTION=" + 
                "(ADDRESS=(PROTOCOL=TCP)(HOST=CLXDBDEV)(PORT=1521))" + 
                "(CONNECT_DATA=(SID=CLXD6)));"*/
                ;

                using(var connection = new OracleConnection())
                {
                    connection.ConnectionString = this.dataService;
                    connection.Open();
                    log.Info(string.Format("State: {0}", connection.State));
                    log.Info(string.Format("ConnectionString: {0}", connection.ConnectionString));

                    using (var command = connection.CreateCommand())
                    {
                        string sql = "SELECT sysdate FROM dual";
                        command.CommandText = sql;

                        using (var reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                string myField = ((DateTime)reader["sysdate"]).ToString();
                                //Console.WriteLine(myField);
                                retVal = "Date " + myField;
                                this.status = Status.CONNECTED;
                                ret = true;
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                this.status = Status.DISCONNECTED;
                retVal = e.Message;
            }
            return ret;
        }

        private bool generateDataService(bool useConnPool)
        {
            if (string.IsNullOrEmpty(this.userName) || string.IsNullOrEmpty(this.password) || string.IsNullOrEmpty(this.databaseHost) ||
                string.IsNullOrEmpty(this.databasePort) || string.IsNullOrEmpty(this.databaseSchema) ||
                string.IsNullOrEmpty(this.databaseService))
            {
                return (false);
            }

            //Build data service string
            /*con.ConnectionString = "User Id=scott;Password=tiger;Data Source=oracle;" +
            "Min Pool Size=10;Connection Lifetime=120;Connection Timeout=60;" +
            "Incr Pool Size=5; Decr Pool Size=2";*/
            var sbuilder = new StringBuilder();
            sbuilder.Append("User Id=" + this.userName + ";");
            sbuilder.Append("Password=" + this.password + ";");
            sbuilder.Append("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)");
            sbuilder.Append("(HOST=" + this.databaseHost + ")(");
            sbuilder.Append("PORT =" + this.databasePort + "))(");
            sbuilder.Append("CONNECT_DATA=(");
            sbuilder.Append("SID=" + this.databaseService + ")));");
            if (useConnPool)
            {
                sbuilder.Append("Min Pool Size=" + this.minPoolSize + ";");
                sbuilder.Append("Max Pool Size=" + this.mxPoolSize + ";");
                sbuilder.Append("Connection Lifetime=" + this.connLife + ";");
                sbuilder.Append("Connection Timeout=" + this.connTout + ";");
                sbuilder.Append("Incr Pool Size=" + this.incrPoolSize + ";");
                sbuilder.Append("Decr Pool Size=" + this.decrPoolSize + ";");
                sbuilder.Append("Validate Connection=True;");
                log.Info(string.Format("Connection Pool true Min {0} Max {1} Life {2} Timeout {3}",
                    this.minPoolSize, this.mxPoolSize, this.connLife,this.connTout));
            }else
            {
                log.Info("Connection pool set to False..............");
            }
            this.dataService = sbuilder.ToString();
            //log.Debug("Connect String"+ this.dataService);
            return (true);
        }
    }
}