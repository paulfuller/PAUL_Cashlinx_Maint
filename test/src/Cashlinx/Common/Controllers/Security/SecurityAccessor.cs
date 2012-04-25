using System;
using System.Data;
using System.Text;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Config;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.String;
using System.IO;

namespace Common.Controllers.Security
{
    public sealed class SecurityAccessor : MarshalByRefObject, IDisposable
    {
        #region Singleton Fields
        static readonly SecurityAccessor instance = new SecurityAccessor();

        static SecurityAccessor()
        {
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public static SecurityAccessor Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region Constants and Enums

        public enum PawnSecState
        {
            DISCONNECTED,
            CONNECTED
        } ;

        #endregion

        #region Private Members

        private OracleDataAccessor dataAccessor;
        private string errorMessage;
        private readonly TempFileLogger pawnSecLogger;
        private PawnSecState state;
        private HostInformation hostInfo;

        private string dbHost;
        private string dbPassword;
        private string dbPort;
        private string dbSchema;
        private string dbService;
        private string dbUser;

        private const string KEY = "PAWNSEC";
        private const uint FETCH_SZ_MX = 100;
        private const string MACHINE_SERVER = ".casham.com";
        public const string SEP = "-";

        private EncryptedConfigContainer encryptedConfig;

        #endregion

        #region Private Methods

        /// <summary>
        /// Disconnects the user from the LDAP server
        /// </summary>
        private void Disconnect()
        {
            try
            {
                if (dataAccessor != null && this.state == PawnSecState.CONNECTED)
                {
                    this.dataAccessor.DisconnectDbConnection(KEY);
                    this.state = PawnSecState.DISCONNECTED;

                    /*if(UpdateConnectionInfo(false))
                    {
                        this.pawnSecLogger.logMessage(LogLevel.INFO, this, "Application successfully disconnected from PAWNSEC.");
                    }
                    else
                    {
                        this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "Application failed to disconnect from PAWNSEC.");
                    }*/
                }
            }
            catch (Exception eX)
            {
                this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "Exception thrown when disconnecting from PAWNSEC: " + eX.Message);
            }
            finally
            {
                this.state = PawnSecState.DISCONNECTED;
            }
        }

        #endregion

        #region Property Accessors
        public HostInformation HostInfo
        {
            set
            {
                if (value != null)
                {
                    this.hostInfo = value;
                }
            }
            get
            {
                return (this.hostInfo);
            }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Parameterless Constructor to establish default assignments
        /// </summary>
        public SecurityAccessor()
        {
            this.dataAccessor = null;
            this.errorMessage = String.Empty;
            this.state = PawnSecState.DISCONNECTED;
            this.hostInfo = null;

            this.dbHost = String.Empty;
            this.dbPassword = String.Empty;
            this.dbPort = String.Empty;
            this.dbSchema = String.Empty;
            this.dbService = String.Empty;
            this.dbUser = String.Empty;

            var dNow = DateTime.Now;
            var yearStr = dNow.Date.Year.ToString().PadLeft(4, '0');
            var monthStr = dNow.Date.Month.ToString().PadLeft(2, '0');
            var dayStr = dNow.Date.Day.ToString().PadLeft(2, '0');
            var hrStr = dNow.Hour.ToString().PadLeft(2, '0');
            var minStr = dNow.Minute.ToString().PadLeft(2, '0');
            var sb = new StringBuilder(64);
            //Determine current executable location and log directory if it exists
            string curDir = System.IO.Directory.GetCurrentDirectory();
            sb.Append(curDir + @"\logs\pawnsec_details_");
            sb.AppendFormat("{0}_{1}_{2}-{3}_{4}.log", yearStr, monthStr, dayStr, hrStr, minStr);
            this.pawnSecLogger = new TempFileLogger(sb.ToString(),
                DefaultLoggerHandlers.defaultLogLevelCheckHandler,
                DefaultLoggerHandlers.defaultLogLevelGenerator,
                DefaultLoggerHandlers.defaultLogMessageHandler,
                DefaultLoggerHandlers.defaultLogMessageFormatHandler,
                DefaultLoggerHandlers.defaultDateStampGenerator);
            this.pawnSecLogger.setLogLevel(LogLevel.DEBUG);
            this.pawnSecLogger.logMessage(LogLevel.INFO, this, "PAWNSECAccessor instance constructed");

            //Clear out encrypted container
            this.encryptedConfig = null;
        }

        #endregion

        #region Public Methods

        public void InitializeConnection(
            OracleDataAccessor dA,
            Credentials dACred)
        {
            this.dataAccessor = dA;
            this.state = PawnSecState.CONNECTED;
            this.dbHost = dACred.DBHost;
            this.dbPort = dACred.DBPort;
            this.dbService = dACred.DBService;
            this.dbSchema = dACred.DBSchema;
            this.dbUser = dACred.UserName;
            this.dbPassword = dACred.PassWord;
            this.dataAccessor.AuxLogger = this.pawnSecLogger;
        }

        /// <summary>
        /// Initializes a connection to the PawnSec database
        /// </summary>
        /// <param name="dHost"></param>
        /// <param name="dPassword"></param>
        /// <param name="dPort"></param>
        /// <param name="dSchema"></param>
        /// <param name="dService"></param>
        /// <param name="dUser"></param>
        public void InitializeConnection(
            string dHost, string dPassword,
            string dPort, string dSchema,
            string dService, string dUser)
        {
            if (this.pawnSecLogger.IsLogDebug)
            {
                this.pawnSecLogger.logMessage(
                        LogLevel.DEBUG,
                        this,
                        "InitializeConnection({0},{1})...",
                        dHost,
                        dPort);
            }
            // check to see if connection is already established);
            if (this.state == PawnSecState.CONNECTED)
            {
                this.pawnSecLogger.logMessage(LogLevel.WARN,
                    this, "- Already connected");
                return;
            }

            // check all inputs
            if (string.IsNullOrEmpty(dHost) ||
                string.IsNullOrEmpty(dPassword) ||
                string.IsNullOrEmpty(dPort) ||
                string.IsNullOrEmpty(dSchema) ||
                string.IsNullOrEmpty(dService) ||
                string.IsNullOrEmpty(dUser))
            {
                this.pawnSecLogger.logMessage(LogLevel.ERROR,
                    this, "- Invalid inputs");
                return;
            }

            // assign db related class members
            this.dbHost = dHost;
            this.dbPassword = dPassword;
            this.dbPort = dPort;
            this.dbSchema = dSchema;
            this.dbService = dService;
            this.dbUser = dUser;

            // establish connection to database
            try
            {
                this.dataAccessor = new OracleDataAccessor(
                    this.dbUser,
                    this.dbPassword,
                    this.dbHost,
                    this.dbPort,
                    this.dbService,
                    this.dbSchema,
                    FETCH_SZ_MX,
                    true,
                    true,
                    KEY
                    );
                this.dataAccessor.AuxLogger = this.pawnSecLogger;
                
                if (this.dataAccessor.Initialized)
                {
                    this.state = PawnSecState.CONNECTED;
                    this.pawnSecLogger.logMessage(LogLevel.INFO, this, "- PawnSec Oracle data accessor connected successfully.");
                }
                else
                {
                    this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "- PawnSec Oracle data accessor is not initialized.  Cannot interact with the database. Exiting!");
                    this.Close();
                    return;
                }
            }
            catch (Exception eX)
            {
                this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "- Could not connect to the PawnSec database: {0}", eX.Message);
                this.Disconnect();
            }
            finally
            {
                if (this.state == PawnSecState.DISCONNECTED)
                {
                    this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "- Still disconneted from database.");
                    this.pawnSecLogger.Dispose();
                }
            }
        }

        /// <summary>
        /// Reconnects with the database
        /// </summary>
        public void Reconnect()
        {
            this.pawnSecLogger.logMessage(
                LogLevel.DEBUG,
                this,
                "Reconnect()...");
            // check to see if connection is already established
            if (this.state == PawnSecState.CONNECTED)
            {
                this.pawnSecLogger.logMessage(LogLevel.WARN,
                    this, "- Already connected");
                return;
            }

            // establish connection to database
            try
            {
                if (this.dataAccessor != null)
                {
                    this.dataAccessor.ReconnectDbConnection(KEY);
                }
                else
                {
                    this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "- PawnSec Oracle data accessor is null.  Cannot interact with the database. Exiting!");
                }

                if (this.dataAccessor != null && this.dataAccessor.Initialized)
                {
                    this.state = PawnSecState.CONNECTED;
                    this.pawnSecLogger.logMessage(LogLevel.INFO, this, "- PawnSec Oracle data accessor reconnected successfully.");
                }
                else
                {
                    this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "- PawnSec Oracle data accessor is not initialized.  Cannot interact with the database. Exiting!");
                    this.Close();
                    return;
                }
            }
            catch (Exception eX)
            {
                this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "- Could not reconnect to the PawnSec database: {0}", eX.Message);
                this.Disconnect();
            }
            finally
            {
                if (this.state == PawnSecState.DISCONNECTED)
                {
                    this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "- Still disconneted from database.");
                    this.pawnSecLogger.Dispose();
                }
            }
        }

        /// <summary>
        /// Closes the connection and disposes the class
        /// </summary>
        public void Close()
        {
            this.pawnSecLogger.logMessage(LogLevel.INFO, this, "Closing security accessor connection...");
            this.Disconnect();
            this.dataAccessor = null;
        }

        /// <summary>
        /// Retrieves data from PawnSec
        /// </summary>
        public bool RetrieveSecurityData(string privateKey, string clientKey, bool disconnectAfter, PawnSecApplication app)
        {
            this.pawnSecLogger.logMessage(LogLevel.DEBUG, this, "RetrievingSecurityData()...");
            if (this.dataAccessor == null || this.state == PawnSecState.DISCONNECTED)
            {
                this.pawnSecLogger.logMessage(LogLevel.ERROR, this, "- Data Accessor is invalid or disconnected");
                return false;
            }

            //Retrieve the machine name
            var machineName = System.Environment.MachineName;
            if (this.pawnSecLogger.IsLogDebug)
            {
                this.pawnSecLogger.logMessage(
                    LogLevel.DEBUG, "- Machine Name From Environment: {0}", machineName);
            }
            machineName = string.Concat(machineName, MACHINE_SERVER);
            this.pawnSecLogger.logMessage(LogLevel.INFO, this, "- Machine Name = {0}", machineName);

            string ipAddress;
            string macAddress;
            try
            {
                //Create the host information object
                this.hostInfo = new HostInformation(this.pawnSecLogger);

                //Retrieve the Ip address
                ipAddress = hostInfo.IPAddress;
                this.pawnSecLogger.logMessage(LogLevel.DEBUG, this, "- IP Address  = {0}", ipAddress);

                //Retrieve the MAC address
                macAddress = hostInfo.MACAddress;
                this.pawnSecLogger.logMessage(LogLevel.DEBUG, this, "- MAC Address = {0}", macAddress);
            }
            catch (Exception eX)
            {
                ipAddress = null;
                macAddress = null;
                this.pawnSecLogger.logMessage(LogLevel.WARN, this, "- Could not retrieve MAC address or IP address - Exception thrown {0}- default to machine name: {1}", eX, machineName);
            }

            //Create output variables
            string errorCode;
            string errorText;
            DataTable clientData;
            DataTable esbData;
            DataTable dbData;
            DataTable macData;

            bool retVal = PawnSecurityProcedures.ExecuteGetClientConfiguration(
                this.dataAccessor,
                KEY,
                ipAddress,
                machineName,
                macAddress,
                clientKey,
                app,
                out clientData,
                out esbData,
                out dbData,
                out macData,
                out errorCode,
                out errorText);

            // check the table data
            if (retVal != true || clientData == null || !clientData.IsInitialized || clientData.HasErrors || 
                esbData == null || !esbData.IsInitialized || esbData.HasErrors ||
                dbData == null || !dbData.IsInitialized || dbData.HasErrors ||
                macData == null || !macData.IsInitialized || macData.HasErrors)
            {
                this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "Incomplete data retrieval occurred.");
                return false;
            }

            // check public key data
            if (clientData.Rows != null && clientData.Rows.Count > 0)
            {
                DataRow row = clientData.Rows[0];

                if (row != null)
                {
                    string publicKey = row["datapublickey"].ToString();
                    if (string.IsNullOrEmpty(publicKey))
                    {
                        this.pawnSecLogger.logMessage(
                            LogLevel.FATAL, this, "No Public Key found.");
                        
                        return false;
                    }
                }
                else
                {
                    this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "No row data found.");
                    
                    return false;
                }
            }
            else
            {
                 this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "No row data exists.");
                 
                 return false;
            }

            // check to make sure that we have at least one db server and three esb servers
            if (esbData.Rows == null || dbData.Rows == null || esbData.Rows.Count < 3 || dbData.Rows.Count < 1)
            {
                this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "Application critical information was not retrieved.");
                return false;
            }

            try
            {
                // set data into configuration
                this.encryptedConfig =
                    new EncryptedConfigContainer(privateKey,
                        clientData, dbData, esbData, macData, app);
                this.pawnSecLogger.logMessage(
                    LogLevel.DEBUG, this, "Set Encrypted Configuration data");

                if (!string.IsNullOrWhiteSpace(encryptedConfig.ClientConfig.GlobalConfiguration.AdobeReaderPath)
                    && !File.Exists(encryptedConfig.ClientConfig.GlobalConfiguration.AdobeReaderPath))
                {
                    this.pawnSecLogger.logMessage(LogLevel.WARN, this, "Pdf Viewer does not exist at \"" + encryptedConfig.ClientConfig.GlobalConfiguration.AdobeReaderPath + "\"");
                }
                
                /*
                if (UpdateConnectionInfo(true))
                {
                    this.pawnSecLogger.logMessage(LogLevel.INFO, this, "Client successfully connected to PAWNSEC.");
                }
                else
                {
                    this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "Client failed to connect to PAWNSEC.");
                }*/
                
                // disconnect from PAWNSEC database);
                if (disconnectAfter)this.Disconnect();
            }
            catch (Exception eX)
            {
                this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "Encrypted Configuration failed to initialize: {0}.", eX);
                return false;
            }

            // everything has succeeded at this point
            return true;
        }

        public bool InitializeSecurityData(ResourceProperties resourceProperties, string clientKey, string storeNumber, PawnSecVO pSecVo,
            out string machineName, out string ipAddress, out string macAddress)
        {
            machineName = string.Empty;
            ipAddress = string.Empty;
            macAddress = string.Empty;
            if (this.encryptedConfig != null) return (true);
            this.pawnSecLogger.logMessage(LogLevel.DEBUG, this, "InitializeSecurityData()...");
            if (this.dataAccessor == null || this.state == PawnSecState.DISCONNECTED)
            {
                this.pawnSecLogger.logMessage(LogLevel.ERROR, this, "- Data Accessor is invalid or disconnected");
                return false;
            }

            //Retrieve the machine name
            machineName = System.Environment.MachineName;
            if (this.pawnSecLogger.IsLogDebug)
            {
                this.pawnSecLogger.logMessage(
                    LogLevel.DEBUG, "- Machine Name From Environment: {0}", machineName);
            }
            machineName = string.Concat(machineName, MACHINE_SERVER);
            this.pawnSecLogger.logMessage(LogLevel.INFO, this, "- Machine Name = {0}", machineName);

            try
            {
                //Create the host information object
                this.hostInfo = new HostInformation(this.pawnSecLogger);

                //Retrieve the Ip address
                ipAddress = hostInfo.IPAddress;
                this.pawnSecLogger.logMessage(LogLevel.DEBUG, this, "- IP Address  = {0}", ipAddress);

                //Retrieve the MAC address
                macAddress = hostInfo.MACAddress;
                this.pawnSecLogger.logMessage(LogLevel.DEBUG, this, "- MAC Address = {0}", macAddress);
            }
            catch (Exception eX)
            {
                ipAddress = null;
                macAddress = null;
                this.pawnSecLogger.logMessage(LogLevel.WARN, this, "- Could not retrieve MAC address or IP address - default to machine name: {0}", machineName);
                return (false);
            }

            this.encryptedConfig = new EncryptedConfigContainer(resourceProperties.PrivateKey, 
                clientKey, storeNumber, pSecVo);
            return (true);
        }

        /// <summary>
        /// Updates the connection info in the PAWNSEC database
        /// </summary>
        /// <param name="isConnected"></param>
        /// <returns></returns>
        public bool UpdateConnectionInfo(bool isConnected)
        {
            if (this.dataAccessor == null || this.state == PawnSecState.DISCONNECTED)
            {
                return false;
            }

            string connected = (isConnected ? "1" : "0");

            string errorCode = String.Empty;
            string errorText = String.Empty;

            var machineName = System.Environment.MachineName.ToLower();
            machineName = string.Concat(machineName, MACHINE_SERVER);

            bool retVal = PawnSecurityProcedures.ExecuteUpdateClientInfo(
                this.dataAccessor,
                KEY,
                null,
                machineName,
                null,
                null,
                connected,
                out errorCode,
                out errorText);

            if (retVal != true)
            {
                this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "Error Updating Connection Info.");
                return false;
            }

            // success
            return true;
        }

        /// <summary>
        /// Updates whether client is allowed or not
        /// </summary>
        /// <param name="isAllowed"></param>
        /// <returns></returns>
        public bool UpdateAllowedInfo(bool isAllowed)
        {
            if (this.dataAccessor == null || this.state == PawnSecState.DISCONNECTED)
            {
                return false;
            }

            string allowed = (isAllowed ? "1" : "0");

            string errorCode = String.Empty;
            string errorText = String.Empty;

            var machineName = System.Environment.MachineName.ToLower();
            machineName = string.Concat(machineName, MACHINE_SERVER);

            bool retVal = PawnSecurityProcedures.ExecuteUpdateClientInfo(
                this.dataAccessor,
                KEY,
                null,
                machineName,
                null,
                allowed,
                null,
                out errorCode,
                out errorText);

            if (retVal != true)
            {
                this.pawnSecLogger.logMessage(LogLevel.FATAL, this, "Error Updating Allowed Info.");
                return false;
            }

            // success
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="desktopSession"> </param>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public bool ReadUsbAuthentication(DesktopSession desktopSession, out string userName, out string userPassword)
        {
            userName = string.Empty;
            userPassword = string.Empty;
            if (desktopSession == null)
                return (false);
            var storage = desktopSession.CurrentUSBStorage;
            if (storage == null || !storage.IsValid)
            {
                return (false);
            }

            string serNumDecryptString = storage.DriveSerialNumber;
            string dataToDecrypt = storage.DriveData;
            string partiallyDecryptData = 
                StringUtilities.Decrypt(dataToDecrypt, serNumDecryptString, true);
            var fullDecrypt = this.encryptedConfig.DecryptValue(partiallyDecryptData);
            int sepIdx = fullDecrypt.IndexOf(SecurityAccessor.SEP, System.StringComparison.OrdinalIgnoreCase);
            if (sepIdx < 0)
                return(false);
            userName = fullDecrypt.Substring(0, sepIdx).ToLowerInvariant();
            userPassword = fullDecrypt.Substring(sepIdx + 1);
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public bool WriteUsbAuthentication(DesktopSession desktopSession, string userName, string userPassword)
        {
            if (desktopSession == null)
                return (false);

            var storage = desktopSession.CurrentUSBStorage;
            if (storage == null || string.IsNullOrEmpty(storage.DriveSerialNumber) || 
                string.IsNullOrEmpty(userName) ||
                string.IsNullOrEmpty(userPassword))
            {
                return (false);
            }
            var rt = false;
            string initialStr = userName + SecurityAccessor.SEP + userPassword;
            string partialEncrypt = this.encryptedConfig.EncryptValue(initialStr);
            string fullEncryption = StringUtilities.Encrypt(partialEncrypt, storage.DriveSerialNumber, true);
            if (!string.IsNullOrEmpty(fullEncryption))
            {
                rt = desktopSession.WriteUsbData(fullEncryption);
            }
            

            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partialEncrypt"></param>
        /// <returns></returns>
        public bool WriteUsbAuthentication(DesktopSession desktopSession, string partialEncrypt)
        {
            if (desktopSession == null)
                return (false);

            var storage = desktopSession.CurrentUSBStorage;
            if (storage == null || string.IsNullOrEmpty(storage.DriveSerialNumber) ||
                string.IsNullOrEmpty(partialEncrypt))
            {
                return (false);
            }
            var rt = false;
            string fullEncryption = StringUtilities.Encrypt(partialEncrypt, storage.DriveSerialNumber, true);
            if (!string.IsNullOrEmpty(fullEncryption))
            {
                rt = desktopSession.WriteUsbData(fullEncryption);
            }

            return (rt);
        }

        #endregion

        #region Public Accessors

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
        }

        public PawnSecState State
        {
            get
            {
                return this.state;
            }
        }

        public EncryptedConfigContainer EncryptConfig
        {
            get
            {
                if (this.encryptedConfig != null &&
                    this.encryptedConfig.Initialized)
                {
                    return (this.encryptedConfig);
                }
                else return (null);
                //throw new ApplicationException(EncryptedConfigContainer.ERRMSG);
            }
        }        

        #endregion

        #region IDisposable Methods

        public void Dispose()
        {
            try
            {
                this.state = PawnSecState.DISCONNECTED;
                this.dataAccessor = null;
            }
            catch (Exception eX)
            {
                this.errorMessage = "Exception thrown while disposing accessor:" + eX.Message;
            }
            finally
            {
                this.dataAccessor = null;
                this.pawnSecLogger.Dispose();
            }
        }

        #endregion
    }
}
