using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.DataAccessLayer;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Network;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Config;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Config;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.Libraries.Utility.Type;
using PawnStoreSetupTool.Data;

namespace PawnStoreSetupTool
{
    public partial class PawnStoreSetupForm : Form
    {
        //Madhu BZ # 238
        //static fields
        private const string P_PORT = "9100";
        private const string WU_PORT = "2101";
        private const string VF_PORT = "9001";

        private string StoreId = null;

        //Static class data
        public const string TXT_ERR = "T";
        public static readonly Color TXT_ERR_COL = Color.Red;
        public const string TXT_CHK = "R";
        public static readonly Color TXT_CHK_COL = Color.Green;
        public const string SPC = " ";
        public const string SETUPALERT_TXT = "PawnStoreSetup Alert";
        public const int MIN_STORENUM_LENGTH = 4;
        public const int MAX_STORENUM_LENGTH = 5;
        public const string CCSOWNER = "ccsowner";
        public const string PAWNSEC = "PAWNSEC";
        public const int GUID_MIN_LENGTH = 40;
        public const string DEFAULT_NETNAME = "1160";
        public const string DEFAULT_ROLE_NAME = "10074";
        public const string LDAP_LOGINKEY = "LDAPLogin";
        public const string LDAP_PWDKEY = "LDAPPassword";
        public const string LDAP_PWDPOLICYKEY = "LDAPPassPolicyDN";
        public const string LDAP_PORTKEY = "LDAPPort";
        public const string LDAP_SERVERKEY = "LDAPServer";
        public const string LDAP_USERDNKEY = "LDAPUserDN";
        public const string LDAP_USERIDKEY = "LDAPUserIdKey";
        public const string LDAP_CXNSUCCESSKEY = "LDAPCxnSuccess";
        public const string LDAP_SEARCHPASSKEY = "LDAPSearchPass";
        public const string LDAP_SEARCHSUCCESSKEY = "LDAPSearchSuccess";
        public const string LDAP_SEARCHUSERKEY = "LDAPSearchUser";
        public const string PWN_DBSERVICE = "PWN_DBSERVICE";
        public const string PWN_DBSCHEMA = "PWN_DBSCHEMA";
        public const string PWN_DBPORT = "PWN_DBPORT";
        public const string PWN_DBHOST = "PWN_DBHOST";
        public const string PWN_PASSWORD = "PWN_PASSWORD";
        public const string PWN_USERNAME = "PWN_USERNAME";
        public const string PWNSEC_DBSERVICE = "PWNSEC_DBSERVICE";
        public const string PWNSEC_DBSCHEMA = "PWNSEC_DBSCHEMA";
        public const string PWNSEC_DBPORT = "PWNSEC_DBPORT";
        public const string PWNSEC_DBHOST = "PWNSEC_DBHOST";
        public const string PWNSEC_PASSWORD = "PWNSEC_PASSWORD";
        public const string PWNSEC_USERNAME = "PWNSEC_USERNAME";
        public const string PWNPUBLICKEY = "PUBLICKEY";
        public const string COUCHDB_SERVER = "COUCHDB_SERVER";
        public const string COUCHDB_PORT = "COUCHDB_PORT";
        public const string COUCHDB_SCHEMA = "COUCHDB_SCHEMA";

        //Private class fields
        private string dbHostName;
        private string dbPort;
        private string dbServiceName;
        private string dbUserName;
        private string dbPassword;
        private string dbSchemaName;
        private string pawnsecUsername;
        private string pawnsecPassword;
        private string pawnsecDbHost;
        private string pawnsecDbPort;
        private string pawnsecServiceName;
        private string pawnsecSchemaName;
        private string storeNumber;
        private string storeAlias;
        private string newMachineName;
        private string newUserName;
        private DataAccessTools dACcs;
        private Credentials dACcsCred;
        private DataAccessTools dAPawnSec;
        private Credentials dAPawnSecCred;
        private uint infoFlag;
        private StringBuilder msgStrBuilder;
        private bool ccsCxn;
        private bool pwnSecCxn;
        private DateTime curDatabaseTime;
        private string encryptionKey;
        private string encryptionKeyPawn;
        private string publicKey;

        //Configuration data
        private Dictionary<string, string> ldapData;
        private StoreSetupVO storeData;
        private string newCashDrawerName;
        private EncryptedConfigContainer eConfig;
        private ShopDateTime shopDateTime;
        private List<EsbServiceVO> esbServices;
        private DatabaseServiceVO couchDBService;
        //private DatabaseServiceVO ldapService;
        //private DatabaseServiceVO ccsownerService;
        private string peripheralTypeSelected;
        private bool pwnSecLoaded;

        //Madhu BZ # 238
        public static bool batchMode = false;
        private List<string> ccsowner_queries = new List<string>();
        private List<string> ccsowner_workstationperipheral_queries = new List<string>();
        private List<string> ccsowner_pawnworkstationperipheral_queries = new List<string>();
        private List<string> pawnsec_clientReg_queries = new List<string>();
        private List<string> pawnsec_storeclientconfig_queries = new List<string>();
        private List<string> pawnsec_clientstoremap_queries = new List<string>();
        private List<string> pawnsec_storesiteinfo_queries = new List<string>();
        private string cdowner_safe_query = null;
        private List<string> cdowner_cashdrawer_queries = new List<string>();
        private List<string> cdowner_cashdraweruser_queries = new List<string>();
        private List<string> cdowner_userroles_queries = new List<string>();
        private List<string> cdowner_userinfodetail_queries = new List<string>();
        private List<string> cdowner_userinfo_queries = new List<string>();
        private List<string> cdowner_nextnum_queries = new List<string>();
        private List<string> consolidated_queries_list = new List<string>();
        private List<string> consolidated_roolback_queries_list = new List<string>();

        private List<string> userlimits_resources_queries_list = new List<string>();

        private List<string> pVFDevices = new List<string>();
        private List<String> cdowner_usergroups_queries = new List<string>();
        private List<PairType<string, string>> storeProdcutsList = new List<PairType<string, string>>();
        private List<PairType<string, string>> securityMasks = new List<PairType<string, string>>();
        private List<PairType<string, string>> securityResources = new List<PairType<string, string>>();

        private List<string> pReceiptPrinterList = new List<string>();

        private ulong newUserId = 0;

        private List<string> nextNums = new List<string>();
        private const string WU_PRINTER_PREFIX = "-WU";
        private const string VF_PRINTER_PREFIX = "-VF";
        private const string BARCODE_PRINTER_PREFIX = "-BPRN";
        private const string RECEIPT_PRINTER_PREFIX = "-RPRN";
        private const string LASER_PRINTER_PREFIX = "-LPRN";

        private const string VF_PRINTER_NAME = "Payment Terminal";
        private const string WU_PRINTER_NAME = "WUMOPrinter";
        private const string BARCODE_PRINTER_NAME = "Intermec Barcode Printer";
        private const string RECEIPT_PRINTER_NAME = "Receipt Printer";
        private const string LASER_PRINTER_NAME = "Laser Printer";

        private const string USERNAME_PREFIX = "vuser_";
        private const string PASSWORD = "xyz12345";
        private const string EMPNO_PREFIX = "9999";
        private const string EMP_TYPE = "Shop Manager";
        private const string LOADTEST_FIRSTNAME = "loadTest";
        private const string LOADTEST_LASTNAME = "loadTest";
        private const string OBJECT_TYPE = "RESERVE";
        private const string CDNAME_SAFE = "_SAFE";
        private const string CDNAME_CASHDRAWER = "CASHDRAWER";
        private const string COMPANY_NAME = "Cash America";
        private const string MGR_PREFIX = "-MGR1";
        private const string WS_PREFIX = "-POS";
        private const string MGR_IP_PREFIX = "61";
        private const string PAWNSEC_PREFIX = "_PawnSec_";
        //to get the store product details for bootstraping 
        //for load runnner scripts
        private const string BOOTSTRAP_STORE = "00152";
        private const string DOMAIN_NAME = ".casham.com";
        private const string ROLLBACK_SCRIPT_PREFIX1 = "DELETE FROM ";
        private const string ROLLBACK_SCRIPT_PREFIX2 = " WHERE ID = ";
        private const string DELETE_PATH_PREFIX = "_Rollbak_";
        private const string COMMIT_STRING = "COMMIT;";
        private const string path_rollback = "c:/GeneratedScripts/RollbackScripts/";
        private const string USERSECURITYPROFILE_UPDATE = "UPDATE USERSECURITYPROFILE SET ASSIGNED='N' WHERE USERID =";
        private string cdowner_conversion_query_path = string.Empty;
        private string cdowner_conversion_rollback_path = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool exceptionCallbackMethod()
        {
            var bEx = BasicExceptionHandler.Instance;
            var fLog = FileLogger.Instance;
            if (fLog != null) fLog.logMessage(LogLevel.FATAL, this, "Exception Callback Method Has Executed");
            if (bEx == null)
                return (true);
            if ((bEx.ApplicationExceptions != null && bEx.ApplicationExceptions.Count > 0) ||
                (bEx.SystemExceptions != null && bEx.SystemExceptions.Count > 0) ||
                (bEx.BaseExceptions != null && bEx.BaseExceptions.Count > 0))
            {
                var sF = new StringBuilder();
                var sb = new StringBuilder();
                sb.AppendLine("Exceptions Occurred:");
                int j = 0;
                if (bEx.ApplicationExceptions != null && bEx.ApplicationExceptions.Count > 0)
                {
                    foreach (ApplicationException aEx in bEx.ApplicationExceptions)
                    {
                        if (aEx == null)
                        {
                            continue;
                        }
                        var msg = string.Format("Application Exception[{0}] = {1}, {2}", j, aEx,
                                                   (aEx.StackTrace ?? "NoStackTrace"));
                        if (j == 0)
                            sF.AppendLine(msg);
                        sb.AppendLine(msg);
                        j++;
                    }
                    j = 0;
                }

                if (bEx.SystemExceptions != null && bEx.SystemExceptions.Count > 0)
                {
                    foreach (SystemException aEx in bEx.SystemExceptions)
                    {
                        if (aEx == null)
                        {
                            continue;
                        }
                        var msg = string.Format("System Exception[{0}] = {1}, {2}", j, aEx,
                                                (aEx.StackTrace ?? "NoStackTrace"));
                        if (j == 0)
                        {
                            sF.AppendLine(msg);
                        }
                        sb.AppendLine(msg);
                        j++;
                    }
                    j = 0;
                }

                if (bEx.BaseExceptions != null && bEx.BaseExceptions.Count > 0)
                {
                    foreach (Exception aEx in bEx.BaseExceptions)
                    {
                        if (aEx == null)
                        {
                            continue;
                        }
                        var msg = string.Format("Base Exception[{0}] = {1}, {2}", j, aEx, (aEx.StackTrace ?? "NoStackTrace"));
                        if (j == 0)
                            sF.AppendLine(msg);
                        sb.AppendLine(msg);
                        j++;
                    }
                }

                if (fLog != null) fLog.logMessage(LogLevel.FATAL, this, "Errors: {0}", sb);
                if (fLog != null) fLog.flush();

                MessageBox.Show(sF.ToString(), "Error(s) Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (true);
            }
            return (false);
        }


        //State flags
        private enum REQINFOFLAGS
        {
            DBHOSTNAME = 0x0001,
            DBPORT = 0x0002,
            DBSERVICE = 0x0004,
            DBUSERNAME = 0x0008,
            DBPASSWORD = 0x0010,
            DBSCHEMA = 0x0020,
            PWNSECPWD = 0x0040,
            STORENUM = 0x0080,
            STOREALIAS = 0x0100,
            NEWMACHINE = 0x0200,
            NEWCASHDRAWER = 0x0400,
            NEWUSERNAME = 0x0800
        };

        /// <summary>
        /// Log message to details pane
        /// </summary>
        /// <param name="msg"></param>
        private void writeMessage(string msg)
        {
            DateTime dtNow = DateTime.Now;
            msgStrBuilder.Remove(0, msgStrBuilder.Length);
            msgStrBuilder.Append(dtNow.Ticks);
            msgStrBuilder.Append(SPC);
            msgStrBuilder.Append(dtNow.ToShortDateString());
            msgStrBuilder.Append(SPC);
            msgStrBuilder.Append(dtNow.ToShortTimeString());
            msgStrBuilder.Append(SPC);
            msgStrBuilder.Append(msg);
            this.msgList.BeginUpdate();
            int lastIdx = this.msgList.Items.Count;
            string msgStrBuilderStr = msgStrBuilder.ToString();
            this.msgList.Items.Add(msgStrBuilderStr);
            this.msgList.EndUpdate();
            this.msgList.BeginUpdate();
            this.msgList.SelectionMode = SelectionMode.One;
            this.msgList.SetSelected(lastIdx, true);
            this.msgList.EndUpdate();
            this.msgList.BeginUpdate();
            this.msgList.SetSelected(lastIdx, false);
            this.msgList.EndUpdate();

            var fLog = FileLogger.Instance;
            fLog.logMessage(LogLevel.DEBUG, "PawnStoreSetupForm", msg);

        }

        public static void CheckmarkLabel(bool val, Label labelCtl)
        {
            if (labelCtl == null) return;
            if (val)
            {
                labelCtl.Text = TXT_CHK;
                labelCtl.ForeColor = TXT_CHK_COL;
            }
            else
            {
                labelCtl.Text = TXT_ERR;
                labelCtl.ForeColor = TXT_ERR_COL;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="validator"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static bool GetTextBoxData(TextBox ctl, Regex validator, out string output)
        {
            output = string.Empty;
            if (ctl == null || string.IsNullOrEmpty(ctl.Text) || ctl.Text.Length == 0) return (false);
            var txt = ctl.Text;
            if (validator != null &&
                !validator.IsMatch(txt))
            {
                return (false);
            }
            output = txt;
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        public PawnStoreSetupForm()
        {
            msgStrBuilder = new StringBuilder();
            msgStrBuilder.EnsureCapacity(64);
            InitializeComponent();

            //Construct objects
            dACcs = DataAccessService.CreateDataAccessTools();
            dACcsCred = null;
            dAPawnSec = DataAccessService.CreateDataAccessTools();
            dAPawnSecCred = null;

            //Set data
            infoFlag = 0;
            ccsCxn = false;
            pwnSecCxn = false;
            ldapData = new Dictionary<string, string>();
            storeData = new StoreSetupVO();
            pawnsecUsername = string.Empty;
            pawnsecPassword = string.Empty;
            pawnsecDbHost = string.Empty;
            pawnsecDbPort = string.Empty;
            pawnsecSchemaName = string.Empty;
            pawnsecServiceName = string.Empty;
            eConfig = null;
            esbServices = null;
            couchDBService = null;
            //ccsownerService = null;
            //ldapService = null;
            pwnSecLoaded = false;

            //Get main encryption key
            this.encryptionKey = global::Common.Properties.Resources.PrivateKey;
            this.encryptionKeyPawn = string.Empty;
            this.publicKey = string.Empty;

            //Setup file logger
            FileLogger fLogger = FileLogger.Instance;
            fLogger.setEnabled(true);
            fLogger.initializeLogger(@"c:\tmp\pawnstoresetup_" + DateTime.Now.Ticks + @".log",
                DefaultLoggerHandlers.defaultLogLevelCheckHandler,
                DefaultLoggerHandlers.defaultLogLevelGenerator,
                DefaultLoggerHandlers.defaultDateStampGenerator,
                DefaultLoggerHandlers.defaultLogMessageHandler,
                DefaultLoggerHandlers.defaultLogMessageFormatHandler);
            fLogger.setLogLevel(LogLevel.DEBUG);

            //Setup exception handler
            BasicExceptionHandler exInstance = BasicExceptionHandler.Instance;
#if DEBUG
            exInstance.setExceptionCallback(exceptionCallbackMethod);
#else
            exInstance.setExceptionCallback(null);
#endif
        }

        private void PawnStoreSetupForm_Load(object sender, EventArgs e)
        {
            writeMessage("Loading Pawn Store Setup Tool");
            DialogResult res = MessageBox.Show("Would you like to load a config file?",
                                               "Pawn Store Setup Message",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                this.loadConfigData();
            }
        }

        private void PawnStoreSetupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.dACcs != null)
            {
                DataAccessService.Disconnect(ref this.dACcs);
                this.dACcs.Dispose();
                this.dACcs = null;
            }

            if (this.dAPawnSec != null)
            {
                DataAccessService.Disconnect(ref this.dAPawnSec);
                this.dAPawnSec.Dispose();
                this.dAPawnSec = null;
            }
        }

        private void databaseHostNameTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!GetTextBoxData(this.databaseServerTextBox, null, out dat))
            {
                CheckmarkLabel(false, this.databaseServerLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.DBHOSTNAME;
                return;
            }
            this.dbHostName = dat;
            infoFlag |= (uint)REQINFOFLAGS.DBHOSTNAME;
            this.databasePortTextBox.Enabled = true;
            CheckmarkLabel(true, this.databaseServerLabel);
        }

        private void databasePortTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!GetTextBoxData(this.databasePortTextBox, null, out dat))
            {
                CheckmarkLabel(false, this.databasePortLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.DBPORT;
                return;
            }
            this.dbPort = dat;
            infoFlag |= (uint)REQINFOFLAGS.DBPORT;
            this.databaseServiceNameTextBox.Enabled = true;
            CheckmarkLabel(true, this.databasePortLabel);
        }

        private void databaseServiceNameTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!GetTextBoxData(this.databaseServiceNameTextBox, null, out dat))
            {
                CheckmarkLabel(false, this.databaseServiceNameLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.DBSERVICE;
                return;
            }
            this.dbServiceName = dat;
            infoFlag |= (uint)REQINFOFLAGS.DBSERVICE;
            this.databaseUserNameTextBox.Enabled = true;
            CheckmarkLabel(true, this.databaseServiceNameLabel);
        }

        private void databaseUserNameTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!GetTextBoxData(this.databaseUserNameTextBox, null, out dat))
            {
                CheckmarkLabel(false, this.databaseUserNameLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.DBUSERNAME;
                return;
            }
            this.dbUserName = dat;
            infoFlag |= (uint)REQINFOFLAGS.DBUSERNAME;
            this.databasePasswordTextBox.Enabled = true;
            CheckmarkLabel(true, this.databaseUserNameLabel);
        }

        private void databasePasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!GetTextBoxData(this.databasePasswordTextBox, null, out dat))
            {
                CheckmarkLabel(false, this.databasePasswordLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.DBPASSWORD;
                return;
            }
            this.dbPassword = dat;
            infoFlag |= (uint)REQINFOFLAGS.DBPASSWORD;
            this.databaseSchemaTextBox.Enabled = true;
            CheckmarkLabel(true, this.databasePasswordLabel);
        }

        private void databaseSchemaTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!GetTextBoxData(this.databaseSchemaTextBox, null, out dat))
            {
                CheckmarkLabel(false, this.databaseSchemaLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.DBSCHEMA;
                return;
            }
            this.dbSchemaName = dat;
            infoFlag |= (uint)REQINFOFLAGS.DBSCHEMA;
            this.pawnsecUserPasswordTextBox.Enabled = true;
            CheckmarkLabel(true, this.databaseSchemaLabel);

        }

        private void pawnsecUserPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!GetTextBoxData(this.pawnsecUserPasswordTextBox, null, out dat))
            {
                CheckmarkLabel(false, this.pawnSecUserPwdLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.PWNSECPWD;
                return;
            }
            this.pawnsecPassword = dat;
            infoFlag |= (uint)REQINFOFLAGS.PWNSECPWD;
            CheckmarkLabel(true, this.pawnSecUserPwdLabel);
            databaseContinueButton.Enabled = true;

            //Pawnsec user
            if (!GetTextBoxData(this.pawnsecUserNameTextBox, null, out dat))
            {
                return;
            }
            this.pawnsecUsername = dat;

        }

        /*        private void verifyDBCxnButton_Click(object sender, EventArgs e)
                {
                    if (!(((infoFlag & (uint)REQINFOFLAGS.DBHOSTNAME) > 0) &&
                          ((infoFlag & (uint)REQINFOFLAGS.DBPASSWORD) > 0) &&
                          ((infoFlag & (uint)REQINFOFLAGS.DBPORT)     > 0) &&
                          ((infoFlag & (uint)REQINFOFLAGS.DBSCHEMA)   > 0) &&
                          ((infoFlag & (uint)REQINFOFLAGS.DBUSERNAME) > 0) &&
                          ((infoFlag & (uint)REQINFOFLAGS.DBPASSWORD) > 0) &&
                           (infoFlag & (uint)REQINFOFLAGS.PWNSECPWD)  > 0))
                    {
                        MessageBox.Show("Please enter all required data for database connection.",
                                        "PawnStoreSetup Alert",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (!setupDatabaseConnections(this.pawnsecPublicSeedTextBox.Text, true))
                    {
                        return;
                    }
                }*/

        /// <summary>
        /// Generate encryption keys with seed or public key
        /// </summary>
        /// <param name="pSeed"></param>
        /// <param name="hashKey"></param>
        /// <returns></returns>
        private bool generateKeys(string pSeed, bool hashKey)
        {
            if (string.IsNullOrEmpty(pSeed))
            {
                return (false);
            }
            //Generate pawnsec public key
            this.publicKey = hashKey ? StringUtilities.GenerateMD5Hash(pSeed) : pSeed;

            //Generate key
            this.encryptionKeyPawn =
                this.publicKey + this.encryptionKey;

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pSeed"></param>
        /// <param name="genKeys"></param>
        /// <returns></returns>
        private bool setupDatabaseConnections(string pSeed, bool genKeys)
        {
            InProgressForm iPForm = new InProgressForm("* CONNECTING TO THE DATABASE *");
            //Set database status to yellow
            this.databaseCxnStatusImage.Image =
                Properties.Resources.metal_button_yellow_small;
            this.databaseCxnStatusLabel.Text = "Connecting...";

            if (genKeys && !this.generateKeys(pSeed, true))
            {
                iPForm.HideMessage();
                //Set database status to red
                this.databaseCxnStatusImage.Image =
                    Properties.Resources.metal_button_red_small;
                writeMessage("Could not connect to either data source");
                this.databaseCxnStatusLabel.Text = "Not Connected";
                MessageBox.Show("Could not verify database connection",
                                SETUPALERT_TXT,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return (false);
            }

            //Set CCSOWNER credentials
            this.dACcsCred = new Credentials
            {
                UserName = this.dbUserName,
                PassWord = this.dbPassword,
                DBHost = this.dbHostName,
                DBPort = this.dbPort,
                DBSchema = this.dbSchemaName,
                DBService = this.dbServiceName
            };

            //Set PAWNSEC credentials
            this.dAPawnSecCred = new Credentials
            {
                UserName = this.pawnsecUsername,
                PassWord = this.pawnsecPassword,
                DBHost = this.dbHostName,
                DBPort = this.dbPort,
                DBSchema = this.pawnsecUsername,
                DBService = this.dbServiceName
            };

            //Dump credentials
#if DEBUG
            writeMessage("CCSOWNER credentials: " + this.dACcsCred);
            writeMessage("PAWNSEC  credentials: " + this.dAPawnSecCred);
#else
            writeMessage("Retrieved database credentials from text boxes");
#endif

            //Create CCSOWNER accessor            
            var resCcs = DataAccessService.Connect(
                CCSOWNER,
                this.dACcsCred,
                DataAccessTools.ConnectMode.MULTIPLE,
                DataAccessTools.LogMode.DEBUG,
                ref this.dACcs);

            //Create PAWNSEC accessor
            var resPwnSec = DataAccessService.Connect(
                PAWNSEC,
                this.dAPawnSecCred,
                DataAccessTools.ConnectMode.MULTIPLE,
                DataAccessTools.LogMode.DEBUG,
                ref this.dAPawnSec);

            //Determine the success of the operation)
            if (resCcs && resPwnSec)
            {
                iPForm.Message = "* CONNECTION SUCCESSFUL *";
                //Set database status to green
                this.databaseCxnStatusImage.Image =
                    Properties.Resources.metal_button_green_small;
                writeMessage("Successfully connected to both data sources");
                this.databaseCxnStatusLabel.Text = "Connected";

                //Get one time data and static data from the database
                ShopProcedures.ExecuteGetDatabaseTime(CCSOWNER,
                        this.dACcs.OracleDA, out curDatabaseTime);
                writeMessage("Captured database time");

                //Get peripheral models and types
                PawnStoreProcedures.AcquirePeripheralTypeModel(
                    ref this.dACcs, ref storeData.AllPeripheralTypes,
                    ref storeData.AllPeripheralModels);
                writeMessage("Retrieved peripheral types and models");

                this.populatePeripheralTypeComboBox();
                iPForm.HideMessage();
                return (true);

            }
            iPForm.Message = "* FAILED TO CONNECT *";
            //Set database status to red
            this.databaseCxnStatusImage.Image =
                Properties.Resources.metal_button_red_small;
            writeMessage("Could not connect to either data source");
            this.databaseCxnStatusLabel.Text = "Not Connected";
            MessageBox.Show("Could not verify database connection",
                            SETUPALERT_TXT,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
            iPForm.HideMessage();
            return (false);
        }

        private void populatePeripheralTypeComboBox()
        {
            this.peripheralTypeComboBox.BeginUpdate();
            this.peripheralTypeComboBox.Items.Clear();
            foreach (var perType in this.storeData.AllPeripheralTypes)
            {
                if (perType == null) continue;
                this.peripheralTypeComboBox.Items.Add(perType.PeripheralTypeName);
            }
            this.peripheralTypeComboBox.EndUpdate();
        }

        private void databaseContinueButton_Click(object sender, EventArgs e)
        {
            if (this.dACcs == null ||
                this.dACcs.OracleDA == null ||
                this.dACcs.OracleDA.GetConnectionStatus(CCSOWNER) != OracleDataAccessor.Status.CONNECTED ||
                this.dAPawnSec == null ||
                this.dAPawnSec.OracleDA == null ||
                this.dAPawnSec.OracleDA.GetConnectionStatus(PAWNSEC) != OracleDataAccessor.Status.CONNECTED)
            {
                if (!(((infoFlag & (uint)REQINFOFLAGS.DBHOSTNAME) > 0) &&
                      ((infoFlag & (uint)REQINFOFLAGS.DBPASSWORD) > 0) &&
                      ((infoFlag & (uint)REQINFOFLAGS.DBPORT) > 0) &&
                      ((infoFlag & (uint)REQINFOFLAGS.DBSCHEMA) > 0) &&
                      ((infoFlag & (uint)REQINFOFLAGS.DBUSERNAME) > 0) &&
                      ((infoFlag & (uint)REQINFOFLAGS.DBPASSWORD) > 0) &&
                      (infoFlag & (uint)REQINFOFLAGS.PWNSECPWD) > 0))
                {
                    MessageBox.Show("Please enter all required data for database connection.",
                                    "PawnStoreSetup Alert",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }

                if (!setupDatabaseConnections(this.pawnsecPublicSeedTextBox.Text, true))
                {
                    return;
                }
            }
            else
            {
                writeMessage("Database connections are valid.");
            }

            //Disable database connection button
            this.databaseContinueButton.Enabled = false;

            //Enable store section
            this.storeAliasTextBox.Enabled = true;
            this.storeNumberTextBox.Enabled = true;
            this.continueStoreButton.Enabled = true;
            this.BatchImport.Enabled = true;
            this.loadRunnerInd.Enabled = true;
            writeMessage("Enabling store selection area");
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(this,
                                               "Would you like to save the configuration data?",
                                               "Pawn Store Setup Message",
                                               MessageBoxButtons.YesNoCancel,
                                               MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                //Save data
                this.saveConfigData();
            }
            else if (res == DialogResult.Cancel)
            {
                //Do not close form
                return;
            }

            //Otherwise drop out to the close command
            this.Close();
            Application.Exit();
        }

        #region Configuration Load/Save Operations
        private void saveConfigData()
        {
            //Create and setup save file dialog
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "p2a files (*.p2a)|*.p2a|All files (*.*)|*.*",
                FilterIndex = 1,
                InitialDirectory = @"c:\",
                RestoreDirectory = true,
                FileName = "pwnstore_config.p2a"
            };

            DialogResult res = saveFileDialog.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                try
                {
                    //Write file
                    var fStream = saveFileDialog.OpenFile();
                    var sWriter = new StreamWriter(fStream);


                    //Write encrypted pawnsec public key
                    ConfigUtilities.WriteEncConfigEntry(sWriter,
                                             PWNPUBLICKEY,
                                             this.publicKey,
                                             this.encryptionKey);

                    //pawnsec credentials
                    if (this.dAPawnSecCred != null)
                    {
                        ConfigUtilities.WriteEncConfigEntry(sWriter, PWNSEC_USERNAME,
                            this.dAPawnSecCred.UserName, this.encryptionKey);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, PWNSEC_PASSWORD,
                            this.dAPawnSecCred.PassWord, this.encryptionKey);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, PWNSEC_DBHOST,
                            this.dAPawnSecCred.DBHost, this.encryptionKey);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, PWNSEC_DBPORT,
                            this.dAPawnSecCred.DBPort, this.encryptionKey);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, PWNSEC_DBSCHEMA,
                            this.dAPawnSecCred.DBSchema, this.encryptionKey);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, PWNSEC_DBSERVICE,
                            this.dAPawnSecCred.DBService, this.encryptionKey);
                    }
                    //ccsowner credentials
                    if (this.dACcsCred != null)
                    {
                        ConfigUtilities.WriteEncConfigEntry(sWriter, PWN_USERNAME,
                            this.dACcsCred.UserName, this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, PWN_PASSWORD,
                            this.dACcsCred.PassWord, this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, PWN_DBHOST,
                            this.dACcsCred.DBHost, this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, PWN_DBPORT,
                            this.dACcsCred.DBPort, this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, PWN_DBSCHEMA,
                            this.dACcsCred.DBSchema, this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, PWN_DBSERVICE,
                            this.dACcsCred.DBService, this.encryptionKeyPawn);
                    }

                    //If ldap data was edited, save it to the config file
                    if (CollectionUtilities.isNotEmpty(this.ldapData))
                    {
                        ConfigUtilities.WriteEncConfigEntry(sWriter, LDAP_LOGINKEY,
                            this.ldapData[LDAP_LOGINKEY], this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, LDAP_PWDPOLICYKEY,
                            this.ldapData[LDAP_PWDPOLICYKEY], this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, LDAP_PWDKEY,
                            this.ldapData[LDAP_PWDKEY], this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, LDAP_SERVERKEY,
                            this.ldapData[LDAP_SERVERKEY], this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, LDAP_PORTKEY,
                            this.ldapData[LDAP_PORTKEY], this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, LDAP_USERDNKEY,
                            this.ldapData[LDAP_USERDNKEY], this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter, LDAP_USERIDKEY,
                            this.ldapData[LDAP_USERIDKEY], this.encryptionKeyPawn);
                    }

                    //If esb data was edited, save it to the config file
                    if (CollectionUtilities.isNotEmpty(this.esbServices))
                    {
                        foreach (var esb in this.esbServices)
                        {
                            if (esb == null) continue;

                        }
                    }

                    //If couch db data was edited, save it to the config file
                    string cServer = string.Empty;
                    string cPort = string.Empty;
                    string cSchema = string.Empty;
                    if (this.couchDBService != null)
                    {
                        cServer = this.couchDBService.Server;
                        cPort = this.couchDBService.Port;
                        cSchema = this.couchDBService.Schema;
                    }
                    else if (eConfig != null && this.couchDBService == null)
                    {
                        DatabaseServiceVO cVo = eConfig.GetCouchDBService();
                        if (cVo != null)
                        {
                            cServer = eConfig.DecryptValue(cVo.Server);
                            cPort = eConfig.DecryptValue(cVo.Port);
                            cSchema = eConfig.DecryptValue(cVo.Schema);
                        }
                    }

                    if (!string.IsNullOrEmpty(cServer) &&
                        !string.IsNullOrEmpty(cPort) &&
                        !string.IsNullOrEmpty(cSchema))
                    {
                        ConfigUtilities.WriteEncConfigEntry(sWriter,
                                                            COUCHDB_SERVER,
                                                            cServer,
                                                            this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter,
                                                            COUCHDB_PORT,
                                                            cPort,
                                                            this.encryptionKeyPawn);
                        ConfigUtilities.WriteEncConfigEntry(sWriter,
                                                            COUCHDB_SCHEMA,
                                                            cSchema,
                                                            this.encryptionKeyPawn);
                    }

                    //Close the writer
                    sWriter.Close();
                    sWriter.Dispose();
                }
                catch (Exception eX)
                {
                    MessageBox.Show(this, "Cannot write configuration file! Exception: " + eX, SETUPALERT_TXT);
                }
            }
        }

        private void loadConfigData()
        {
            //Show open file dialog
            var openFileDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                InitialDirectory = @"c:\",
                FileName = "pwnstore_config.p2a",
                Filter = "p2a files|*.p2a|All files (*.*)|*.*",
                FilterIndex = 1,
                Multiselect = false
            };

            DialogResult res = openFileDialog.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                try
                {
                    //Open the file for reading
                    var fStream = openFileDialog.OpenFile();
                    var sReader = new StreamReader(fStream);

                    //Read the encrypted pawn sec key
                    string pSecEncKey = ConfigUtilities.ReadEncConfigEntry(
                        sReader,
                        PWNPUBLICKEY,
                        this.encryptionKey);
                    if (string.IsNullOrEmpty(pSecEncKey))
                    {
                        MessageBox.Show("Could not read public key from file!",
                                        "Pawn Store Setup Alert",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return;
                    }

                    //Setup encryption keys
                    if (!this.generateKeys(pSecEncKey, false))
                    {
                        return;
                    }

                    //Retrieve entries from file
                    this.pawnsecUsername = ConfigUtilities.ReadEncConfigEntry(sReader, PWNSEC_USERNAME,
                            this.encryptionKey);
                    this.pawnsecPassword = ConfigUtilities.ReadEncConfigEntry(sReader, PWNSEC_PASSWORD,
                            this.encryptionKey);
                    this.pawnsecDbHost = ConfigUtilities.ReadEncConfigEntry(sReader, PWNSEC_DBHOST,
                            this.encryptionKey);
                    this.pawnsecDbPort = ConfigUtilities.ReadEncConfigEntry(sReader, PWNSEC_DBPORT,
                            this.encryptionKey);
                    this.pawnsecSchemaName = ConfigUtilities.ReadEncConfigEntry(sReader, PWNSEC_DBSCHEMA,
                            this.encryptionKey);
                    this.pawnsecServiceName = ConfigUtilities.ReadEncConfigEntry(sReader, PWNSEC_DBSERVICE,
                            this.encryptionKey);

                    //ccsowner credentials
                    this.dbUserName = ConfigUtilities.ReadEncConfigEntry(sReader, PWN_USERNAME,
                        this.encryptionKeyPawn);
                    this.dbPassword = ConfigUtilities.ReadEncConfigEntry(sReader, PWN_PASSWORD,
                        this.encryptionKeyPawn);
                    this.dbHostName = ConfigUtilities.ReadEncConfigEntry(sReader, PWN_DBHOST,
                        this.encryptionKeyPawn);
                    this.dbPort = ConfigUtilities.ReadEncConfigEntry(sReader, PWN_DBPORT,
                        this.encryptionKeyPawn);
                    this.dbSchemaName = ConfigUtilities.ReadEncConfigEntry(sReader, PWN_DBSCHEMA,
                        this.encryptionKeyPawn);
                    this.dbServiceName = ConfigUtilities.ReadEncConfigEntry(sReader, PWN_DBSERVICE,
                        this.encryptionKeyPawn);

                    //See if the config file contains any ldap information
                    string ldapLogin = ConfigUtilities.ReadEncConfigEntry(sReader, LDAP_LOGINKEY,
                        this.encryptionKeyPawn);
                    string pwdPolicy = ConfigUtilities.ReadEncConfigEntry(sReader, LDAP_PWDPOLICYKEY,
                        this.encryptionKeyPawn);
                    string usrPwdKey = ConfigUtilities.ReadEncConfigEntry(sReader, LDAP_PWDKEY,
                        this.encryptionKeyPawn);
                    string ldapServer = ConfigUtilities.ReadEncConfigEntry(sReader, LDAP_SERVERKEY,
                        this.encryptionKeyPawn);
                    string ldapPort = ConfigUtilities.ReadEncConfigEntry(sReader, LDAP_PORTKEY,
                        this.encryptionKeyPawn);
                    string searchDN = ConfigUtilities.ReadEncConfigEntry(sReader, LDAP_USERDNKEY,
                        this.encryptionKeyPawn);
                    string uidKey = ConfigUtilities.ReadEncConfigEntry(sReader, LDAP_USERIDKEY,
                        this.encryptionKeyPawn);

                    if (!string.IsNullOrEmpty(ldapLogin) &&
                        !string.IsNullOrEmpty(pwdPolicy) &&
                        !string.IsNullOrEmpty(usrPwdKey) &&
                        !string.IsNullOrEmpty(ldapServer) &&
                        !string.IsNullOrEmpty(ldapPort) &&
                        !string.IsNullOrEmpty(searchDN) &&
                        !string.IsNullOrEmpty(uidKey) &&
                        this.ldapData == null)
                    {
                        //Initialize map
                        this.ldapData = new Dictionary<string, string>(7);
                        this.ldapData.Add(LDAP_LOGINKEY, ldapLogin);
                        this.ldapData.Add(LDAP_PWDPOLICYKEY, pwdPolicy);
                        this.ldapData.Add(LDAP_PWDKEY, usrPwdKey);
                        this.ldapData.Add(LDAP_SERVERKEY, ldapServer);
                        this.ldapData.Add(LDAP_PORTKEY, ldapPort);
                        this.ldapData.Add(LDAP_USERDNKEY, searchDN);
                        this.ldapData.Add(LDAP_USERIDKEY, uidKey);
                    }


                    //Fetch esb data

                    //If couch db data was edited, save it to the config file
                    string cServer = ConfigUtilities.ReadEncConfigEntry(sReader,
                        COUCHDB_SERVER, this.encryptionKeyPawn);
                    string cPort = ConfigUtilities.ReadEncConfigEntry(sReader,
                        COUCHDB_PORT, this.encryptionKeyPawn);
                    string cSchema = ConfigUtilities.ReadEncConfigEntry(sReader,
                        COUCHDB_SCHEMA,
                        this.encryptionKeyPawn);

                    if (!string.IsNullOrEmpty(cServer) &&
                        !string.IsNullOrEmpty(cPort) &&
                        !string.IsNullOrEmpty(cSchema) &&
                        this.couchDBService == null)
                    {
                        this.couchDBService = new DatabaseServiceVO(string.Empty, string.Empty, true);
                        this.couchDBService.Server = cServer;
                        this.couchDBService.Port = cPort;
                        this.couchDBService.Schema = cSchema;
                        this.couchDBService.ServiceType = EncryptedConfigContainer.COUCHDBKEY;
                    }

                    //Close the file stream
                    sReader.Close();
                    sReader.Dispose();

                    //Set text boxes
                    this.databaseUserNameTextBox.Text = this.dbUserName;
                    this.databasePasswordTextBox.Text = this.dbPassword;
                    this.databaseServerTextBox.Text = this.dbHostName;
                    this.databasePortTextBox.Text = this.dbPort;
                    this.databaseSchemaTextBox.Text = this.dbSchemaName;
                    this.databaseServiceNameTextBox.Text = this.dbServiceName;
                    this.pawnsecUserNameTextBox.Text = this.pawnsecUsername;
                    this.pawnsecUserPasswordTextBox.Text = this.pawnsecPassword;

                    //Setup database connections
                    if (!this.setupDatabaseConnections(pSecEncKey, false))
                    {
                        return;
                    }
                }
                catch (Exception eX)
                {
                    MessageBox.Show(eX.Message);
                }
            }
        }
        #endregion

        private void ldapSetupButton_Click(object sender, EventArgs e)
        {
            writeMessage("Entering LDAP Setup...");
            var ldapForm = new LDAPSetupForm();
            string loginDN = null,
                   searchDN = null,
                   userIdKey = null,
                   userPwd = null,
                   pwdPolicyCN = null,
                   port = null,
                   hostname = null;
            //Get the store
            //Madhu
            //var pStore = this.storeData.PawnSecData.GetStore();

            //If the encryption config is ready, check for the ldap service there
            //var setLdapFromEConfig = true;
            //See if we have any data in the ldap data hash map
            if (CollectionUtilities.isNotEmpty(this.ldapData))
            {
                if (CollectionUtilities.isNotEmptyContainsKey(this.ldapData, LDAP_LOGINKEY))
                    loginDN = this.ldapData[LDAP_LOGINKEY];
                if (CollectionUtilities.isNotEmptyContainsKey(this.ldapData, LDAP_PWDKEY))
                    userPwd = this.ldapData[LDAP_PWDKEY];
                if (CollectionUtilities.isNotEmptyContainsKey(this.ldapData, LDAP_PWDPOLICYKEY))
                    pwdPolicyCN = this.ldapData[LDAP_PWDPOLICYKEY];
                if (CollectionUtilities.isNotEmptyContainsKey(this.ldapData, LDAP_PORTKEY))
                    port = this.ldapData[LDAP_PORTKEY];
                if (CollectionUtilities.isNotEmptyContainsKey(this.ldapData, LDAP_SERVERKEY))
                    hostname = this.ldapData[LDAP_SERVERKEY];
                if (CollectionUtilities.isNotEmptyContainsKey(this.ldapData, LDAP_USERDNKEY))
                    searchDN = this.ldapData[LDAP_USERDNKEY];
                if (CollectionUtilities.isNotEmptyContainsKey(this.ldapData, LDAP_USERIDKEY))
                    userIdKey = this.ldapData[LDAP_USERIDKEY];
            }
            else
            {
                //Load LDAP from the pawnsec object (if available) and populate the map
                DatabaseServiceVO ldapVo = null;
                if (this.eConfig == null)
                {
                    this.ldapData = new Dictionary<string, string>();
                    loginDN = "cn=db2inst1";
                    searchDN = "ou=People, dc=cashamerica";//string.Empty;
                    userIdKey = "uid";
                    userPwd = "db2inst1";
                    pwdPolicyCN = "cn=pwdpolicy,cn=ibmpolicies";//string.Empty;
                    port = "389";
                    hostname = "cashldap3.casham.com";//string.Empty;
                }
                else
                {
                    ldapVo = this.eConfig.GetLDAPService(
                            out loginDN, out searchDN, out userIdKey, out userPwd, out pwdPolicyCN);
                    if (ldapVo != null)
                    {
                        port = this.eConfig.DecryptValue(ldapVo.Port);
                        hostname = this.eConfig.DecryptValue(ldapVo.Server);
                    }
                    else
                    {
                        this.ldapData = new Dictionary<string, string>();
                        loginDN = "cn=db2inst1";
                        searchDN = string.Empty;
                        userIdKey = "uid";
                        userPwd = "db2inst1";
                        pwdPolicyCN = string.Empty;
                        port = "389";
                        hostname = string.Empty;
                    }
                }
            }
            //Setup form data
            try
            {
                if (!string.IsNullOrEmpty(loginDN)) ldapForm.LDAPLogin = loginDN;
                if (!string.IsNullOrEmpty(userPwd)) ldapForm.LDAPPassword = userPwd;
                if (!string.IsNullOrEmpty(pwdPolicyCN)) ldapForm.LDAPPassPolicyDN = pwdPolicyCN;
                if (!string.IsNullOrEmpty(searchDN)) ldapForm.LDAPUserDN = searchDN;
                if (!string.IsNullOrEmpty(userIdKey)) ldapForm.LDAPUserIdKey = userIdKey;
                if (!string.IsNullOrEmpty(hostname)) ldapForm.LDAPServer = hostname;
                if (!string.IsNullOrEmpty(port)) ldapForm.LDAPPort = port;

                //Show LDAP form
                ldapForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not setup LDAP - " + ex.Message,
                                SETUPALERT_TXT,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }
            writeMessage("Finished LDAP Setup");

            //Get data from LDAP form
            if (CollectionUtilities.isEmpty(this.ldapData))
            {
                this.ldapData.Add(LDAP_CXNSUCCESSKEY, ldapForm.LDAPCxnSuccess.ToString());
                this.ldapData.Add(LDAP_LOGINKEY, ldapForm.LDAPLogin);
                this.ldapData.Add(LDAP_PWDPOLICYKEY, ldapForm.LDAPPassPolicyDN);
                this.ldapData.Add(LDAP_PWDKEY, ldapForm.LDAPPassword);
                this.ldapData.Add(LDAP_PORTKEY, ldapForm.LDAPPort);
                this.ldapData.Add(LDAP_SERVERKEY, ldapForm.LDAPServer);
                this.ldapData.Add(LDAP_SEARCHPASSKEY, ldapForm.LDAPSearchPass);
                this.ldapData.Add(LDAP_SEARCHSUCCESSKEY, ldapForm.LDAPSearchSuccess.ToString());
                this.ldapData.Add(LDAP_SEARCHUSERKEY, ldapForm.LDAPSearchUser);
                this.ldapData.Add(LDAP_USERDNKEY, ldapForm.LDAPUserDN);
                this.ldapData.Add(LDAP_USERIDKEY, ldapForm.LDAPUserIdKey);
            }
            //Update data
            else
            {
                this.ldapData[LDAP_CXNSUCCESSKEY] = ldapForm.LDAPCxnSuccess.ToString();
                this.ldapData[LDAP_LOGINKEY] = ldapForm.LDAPLogin;
                this.ldapData[LDAP_PWDPOLICYKEY] = ldapForm.LDAPPassPolicyDN;
                this.ldapData[LDAP_PWDKEY] = ldapForm.LDAPPassword;
                this.ldapData[LDAP_PORTKEY] = ldapForm.LDAPPort;
                this.ldapData[LDAP_SERVERKEY] = ldapForm.LDAPServer;
                this.ldapData[LDAP_SEARCHPASSKEY] = ldapForm.LDAPSearchPass;
                this.ldapData[LDAP_SEARCHSUCCESSKEY] = ldapForm.LDAPSearchSuccess.ToString();
                this.ldapData[LDAP_SEARCHUSERKEY] = ldapForm.LDAPSearchUser;
                this.ldapData[LDAP_USERDNKEY] = ldapForm.LDAPUserDN;
                this.ldapData[LDAP_USERIDKEY] = ldapForm.LDAPUserIdKey;
            }
        }

        private void esbSetupButton_Click(object sender, EventArgs e)
        {
            var esbList = new List<PairType<ulong, QuadType<string, string, string, TupleType<string, string, string>>>>(3);
            if (eConfig != null && CollectionUtilities.isEmpty(this.esbServices))
            {
                EsbServiceVO addrServ = eConfig.GetAddressESBService();
                if (addrServ != null)
                {
                    esbList.Add(new PairType<ulong, QuadType<string, string, string, TupleType<string, string, string>>>(
                        addrServ.Id, new QuadType<string, string, string, TupleType<string, string, string>>(
                        eConfig.DecryptValue(addrServ.Name),
                        eConfig.DecryptValue(addrServ.Server),
                        eConfig.DecryptValue(addrServ.Port),
                        new TupleType<string, string, string>(eConfig.DecryptValue(addrServ.Domain), eConfig.DecryptValue(addrServ.Uri), eConfig.DecryptValue(addrServ.EndPointName)))));
                }
                EsbServiceVO pknoServ = eConfig.GetProKnowESBService();
                if (pknoServ != null)
                {
                    esbList.Add(new PairType<ulong, QuadType<string, string, string, TupleType<string, string, string>>>(
                        pknoServ.Id, new QuadType<string, string, string, TupleType<string, string, string>>(
                        eConfig.DecryptValue(pknoServ.Name),
                        eConfig.DecryptValue(pknoServ.Server),
                        eConfig.DecryptValue(pknoServ.Port),
                        new TupleType<string, string, string>(eConfig.DecryptValue(pknoServ.Domain), eConfig.DecryptValue(pknoServ.Uri), eConfig.DecryptValue(pknoServ.EndPointName)))));
                }
                EsbServiceVO mdseServ = eConfig.GetMDSETransferService();
                if (mdseServ != null)
                {
                    esbList.Add(new PairType<ulong, QuadType<string, string, string, TupleType<string, string, string>>>(
                                        mdseServ.Id,
                                        new QuadType<string, string, string, TupleType<string, string, string>>(
                                                eConfig.DecryptValue(mdseServ.Name),
                                                eConfig.DecryptValue(mdseServ.Server),
                                                eConfig.DecryptValue(mdseServ.Port),
                                                new TupleType<string, string, string>(eConfig.DecryptValue(mdseServ.Domain),
                                                                              eConfig.DecryptValue(mdseServ.Uri),
                                                                              eConfig.DecryptValue(mdseServ.EndPointName)))));
                }
            }
            else if (eConfig != null && CollectionUtilities.isNotEmpty(this.esbServices))
            {
                foreach (var esb in this.esbServices)
                {
                    if (esb == null) continue;
                    esbList.Add(new PairType<ulong, QuadType<string, string, string, TupleType<string, string, string>>>(
                        esb.Id, new QuadType<string, string, string, TupleType<string, string, string>>(
                        eConfig.DecryptValue(esb.Name),
                        eConfig.DecryptValue(esb.Server),
                        eConfig.DecryptValue(esb.Port),
                        new TupleType<string, string, string>(
                            eConfig.DecryptValue(esb.Domain),
                            eConfig.DecryptValue(esb.Uri),
                            eConfig.DecryptValue(esb.EndPointName)))));
                }
            }
            //Call ESB setup form
            var esbForm = new ESBSetupForm(esbList);
            esbForm.ShowDialog(this);
            this.esbServices = new List<EsbServiceVO>(3);
            this.storeData.PawnSecData.ESBServiceList.Clear();
            foreach (var qTEsbObj in esbForm.EsbServices)
            {
                if (qTEsbObj == null) continue;
                var pTypeId = qTEsbObj.Left;
                var qTEsb = qTEsbObj.Right;
                var evo = new EsbServiceVO(true);
                evo.Id = pTypeId;
                evo.Name = (eConfig != null) ? eConfig.EncryptValue(qTEsb.X) : qTEsb.X;
                evo.Server = (eConfig != null) ? eConfig.EncryptValue(qTEsb.Y) : qTEsb.Y;
                evo.Port = (eConfig != null) ? eConfig.EncryptValue(qTEsb.Z) : qTEsb.Z;
                evo.Domain = (eConfig != null) ? eConfig.EncryptValue(qTEsb.W.Left) : qTEsb.W.Left;
                evo.Uri = (eConfig != null) ? eConfig.EncryptValue(qTEsb.W.Right) : qTEsb.W.Right;
                evo.EndPointName = (eConfig != null) ? eConfig.EncryptValue(qTEsb.W.Mid) : qTEsb.W.Mid;
                if (string.IsNullOrEmpty(evo.EndPointName))
                {
                    if (!string.IsNullOrEmpty(qTEsb.W.Right))
                    {
                        int lastSlashIdx = qTEsb.W.Right.LastIndexOf('/');
                        if (lastSlashIdx != -1)
                        {
                            var ePName = qTEsb.W.Right.Substring(lastSlashIdx + 1);
                            if (!string.IsNullOrEmpty(ePName))
                            {
                                evo.EndPointName = (eConfig != null) ? eConfig.EncryptValue(ePName) : ePName;
                            }
                        }
                    }
                }
                this.esbServices.Add(evo);
                this.storeData.PawnSecData.ESBServiceList.Add(evo);
            }
            esbForm.Close();
            esbForm.Dispose();
        }

        private void addNewWorkstationButton_Click(object sender, EventArgs e)
        {
            //Do nothing
        }

        private void setupExistingWorkstationButton_Click(object sender, EventArgs e)
        {
            //Do nothing
        }

        /*private EncryptedConfigContainer loadPawnsecDatabase()
        {
            //Attempt to retrieve pawn security data
            this.pwnSecLoaded = false;
            CashlinxDesktop.PawnSecurityAccessor pSec =
                    CashlinxDesktop.PawnSecurityAccessor.Instance;
            pSec.InitializeConnection(
                    this.dAPawnSec.OracleDA,
                    this.dAPawnSecCred);
            if (!pSec.RetrieveSecurityData(CashlinxDesktop.Program.ComputeAppHash(),
                                               false))
            {
                writeMessage("Could not retrieve pawn security database. Entering pawn sec init form");
                DialogResult res =
                        MessageBox.Show(
                                "Pawn security data not retrieved.  You must configure the initial pawnsec database now.",
                                SETUPALERT_TXT,
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Exclamation);
                if (res == DialogResult.OK)
                {
                    //Initialize pawn sec form
                    var pSecForm = new PawnSecSetupForm(pSec,
                                                        this.storeData,
                                                        this.storeData.PawnSecData,
                                                        null,
                                                        this.publicKey,
                                                        this.storeNumber,
                                                        true);
                    pSecForm.ShowDialog(this);
                    this.reconcilePawnsecMachinesToWorkstations();
                }
                else
                {
                    return (null);
                }

            }
            return (pSec.EncryptConfig);
        }*/

        private void reconcileUsersToCashdrawerUsers()
        {
            foreach (var usrInf in this.storeData.AllUsers)
            {
                var curUsr = usrInf;
                if (curUsr == null || curUsr.X == null)
                    continue;

                //See if we have a matching cash drawer user
                var cDrawUsr = this.storeData.AllCashDrawerUsers.Find(x => x.UserId.ToString().Equals(curUsr.X.UserID));
                //If a user match was found, continue
                if (cDrawUsr == null)
                {


                    //Otherwise, create a new cash drawer user
                    //this.storeData.AllCashDrawerUsers.Add(
                    cDrawUsr = new CashDrawerUserVO();
                    cDrawUsr.Id = "0";
                    cDrawUsr.UserId = 0;
                    cDrawUsr.UserName = curUsr.X.UserName.ToLowerInvariant();
                    cDrawUsr.BranchId = this.storeData.StoreInfo.StoreId;
                    this.storeData.AllCashDrawerUsers.Add(cDrawUsr);
                    writeMessage("- Reconciled user " + cDrawUsr.UserName + " to cash drawer user");
                }

                var drawerName = this.storeData.StoreInfo.StoreNumber.PadLeft(5, '0') + "_" + curUsr.X.UserName;
                var cDrawer =
                        this.storeData.AllCashDrawers.Find(
                                x => x.Name.Equals(drawerName, StringComparison.OrdinalIgnoreCase));
                if (cDrawer != null)
                    continue;

                cDrawer = new CashDrawerVO();
                cDrawer.Id = "0";
                cDrawer.Name = drawerName.ToLowerInvariant();
                cDrawer.BranchId = this.storeData.StoreInfo.StoreId;
                cDrawer.OpenFlag = "0";
                //cDrawer.RegisterUserId = 
                cDrawer.NetName = "1160";
                this.storeData.AllCashDrawers.Add(cDrawer);
                writeMessage("- Reconciled user " + cDrawUsr.UserName + " to cash drawer " + drawerName);
            }
        }

        private void continueStoreButton_Click(object sender, EventArgs e)
        {
            //Disable entry into store text fields
            this.storeNumberTextBox.Enabled = false;
            this.storeAliasTextBox.Enabled = false;

            //Get store info
            if ((infoFlag & (uint)REQINFOFLAGS.STORENUM) > 0)
            {
                var ipform = new InProgressForm("* LOADING STORE DATA *");
                //Acquire store data (siteid)
                string errCode, errString;
                if (!ShopProcedures.ExecuteGetStoreInfo(
                        CCSOWNER,
                        this.dACcs.OracleDA,
                        this.storeNumber,
                        ref storeData.StoreInfo,
                        out errCode,
                        out errString))
                {
                    //Must create the store via conversion
                    ipform.HideMessage();
                    MessageBox.Show("Invalid store number entered.", SETUPALERT_TXT);
                    this.storeNumberTextBox.Enabled = true;
                    this.storeAliasTextBox.Enabled = false;
                    return;
                }

                //Set store number into siteid
                this.storeData.StoreInfo.StoreNumber = this.storeNumber;

                //Pawn sec encrypted config container initialization
                this.eConfig = new EncryptedConfigContainer(
                    Common.Properties.Resources.PrivateKey,
                    this.publicKey, this.storeNumber, this.storeData.PawnSecData);
                writeMessage("Pawn encryption system initialized");
                this.generateConfigFile();
                writeMessage("Template config file written");


                //Enable workstations/cashdrawers/users/peripherals
                //Enable text boxes
                //this.newMachineTextBox.Enabled = true;
                this.newUserNameTextBox.Enabled = true;
                //this.newCashDrawerNameTextBox.Enabled = true;
                //this.newMachineTextBox.Enabled = true;

                //Enable combo boxes
                this.peripheralTypeComboBox.Enabled = true;

                //Enable buttons
                //this.addNewCashDrawerButton.Enabled = true;
                this.addNewUserButton.Enabled = false;
                //this.addNewWorkstationButton.Enabled = true;
                this.addPeripheralButton.Enabled = true;
                //this.assignCashDrawerButton.Enabled = true;
                this.mapPeripheralButton.Enabled = true;
                //this.setupExistingWorkstationButton.Enabled = true;
                this.setupUserButton.Enabled = true;
                this.viewPeripheralsSummaryButton.Enabled = true;
                this.viewUsersSummaryButton.Enabled = true;
                this.viewCashDrawerSummaryButton.Enabled = true;
                this.viewWorkstationSummaryButton.Enabled = true;
                this.submitConfigurationButton.Enabled = true;
                this.BackupButton.Enabled = true;


                //Retrieve workstations
                ipform.Message = "* RETRIEVING STORE WORKSTATIONS *";
                DataTable workTable;
                if (!ShopProcedures.GetAllWorkstations(
                        CCSOWNER,
                        this.dACcs.OracleDA,
                        this.storeNumber,
                        out workTable,
                        out errCode,
                        out errString))
                {
                    ipform.HideMessage();
                    //No workstations found in store, must create some
                    MessageBox.Show("No workstations found.", SETUPALERT_TXT);
                    writeMessage("No workstations found.");
                }
                else
                {
                    ipform.Message = "* PROCESSING WORKSTATIONS *";
                    //Process workstation results
                    PawnStoreProcedures.ProcessWorkstationTable(
                            storeData.StoreInfo.StoreId,
                            workTable,
                            out storeData.AllWorkstations);
                }
                ipform.HideMessage();
                ipform.Close();
                ipform.Dispose();
            }
            else
            {
                MessageBox.Show("Please enter a valid store number",
                                SETUPALERT_TXT);
                this.storeNumberTextBox.Enabled = true;
                this.storeAliasTextBox.Enabled = false;
                return;
            }

            //Initialize pawn sec data
            this.storeData.PawnSecData = new PawnSecVO(this.publicKey, this.storeNumber);

            var ipform2 = new InProgressForm("* LOADING PERIPHERALS *");


            //Get peripherals
            writeMessage("Retrieving peripherals and mappings");
            PawnStoreProcedures.AcquirePeripheralData(
                ref this.dACcs,
                this.storeData.StoreInfo.StoreId,
                this.storeData.AllPeripheralTypes,
                ref this.storeData.AllPeripherals);
            PawnStoreProcedures.GetWorkstationPeripheralMapping(
                ref this.dACcs, this.storeData.StoreInfo.StoreId,
                this.storeData.AllWorkstations, this.storeData.AllPeripherals,
                ref this.storeData.PawnWorkstationPeripheralMap);

            ipform2.Message = "* LOADING CASHDRAWERS *";
            //Get cash drawer / user info
            writeMessage("Retrieving cash drawers and available users");
            DataTable storeCashDrawers;
            DataTable availableCashDrawerUsers;
            DataTable assignedCashDrawerUsers;
            DataTable auxiliaryCashDrawerUserList;
            string errorCode, errText;
            if (!ShopProcedures.ExecuteGetCashDrawerDetails(
               dACcs.OracleDA,
               CCSOWNER,
               storeNumber,
               out storeCashDrawers,
               out availableCashDrawerUsers,
               out assignedCashDrawerUsers,
               out auxiliaryCashDrawerUserList,
               out errorCode,
               out errText))
            {
                MessageBox.Show("No cash drawers and/or available users found. You must add some before you can proceed to setup cash drawers.", SETUPALERT_TXT);
            }
            this.processCashDrawerDetails(storeCashDrawers, availableCashDrawerUsers,
                assignedCashDrawerUsers);

            ipform2.Message = "* RETREIVING USERS *";
            PawnStoreProcedures.GetAllUsers(ref this.dACcs, storeNumber, ref this.storeData.AllUsers);

            //Reconcile users to cash drawer users
            this.reconcileUsersToCashdrawerUsers();


            this.ldapSetupButton.Enabled = true;
            this.couchSetupButton.Enabled = true;
            this.esbSetupButton.Enabled = true;
            this.pawnSecSetupButton.Enabled = true;
            //this.addNewCashDrawerButton.Enabled = false;
            this.addNewUserButton.Enabled = false;
            this.addPeripheralButton.Enabled = false;

            bool changedPublicKey;
            ipform2.Message = "* LOADING PAWN SECURITY *";
            if (!PawnStoreProcedures.GetAllPawnSecData(ref this.dAPawnSec, storeNumber, this.storeData.StoreInfo,
                this.publicKey, ref this.storeData.PawnSecData, out changedPublicKey))
            {
                //Still need to add a barebone store config if no pawn sec data was loaded
                //                this.storeData.PawnSecData.Stores.Add();
                ipform2.Dispose();
                MessageBox.Show("Could not find any data in the pawn sec database.");
                return;
            }

            ipform2.Message = "* GENERATING PAWN SECURITY MAPS *";
            //Generate maps
            this.storeData.PawnSecData.GenerateMaps();
            if (!this.storeData.PawnSecData.MapsValid)
            {
                ipform2.Dispose();
                writeMessage("Retrieved partial pawn security information. No maps generated");
            }
            else
            {
                writeMessage("Retrieved full pawn security information. Maps have been generated");
            }
            //Pawn sec has been setup, retrieve and set data
            /*this.eConfig = new EncryptedConfigContainer(CashlinxDesktop.Properties.Resources.PrivateKey,
                this.publicKey, this.storeNumber, this.storeData.PawnSecData);*/
            this.eConfig.Refresh(this.storeNumber, this.storeData.PawnSecData);
            //Set shop date time
            this.shopDateTime = ShopDateTime.Instance;
            this.shopDateTime.setOffsets(0, 0, 0, 0, 0, 0, 0);
            this.shopDateTime.SetDatabaseTime(curDatabaseTime);
            PawnSecVO.PawnSecStoreVO curStore = this.storeData.PawnSecData.GetStore();
            if (curStore != null)
            {
                this.shopDateTime.SetPawnSecOffsetTime(curStore.StoreConfiguration);
                writeMessage("Shop date time has been set to " +
                             this.shopDateTime.ShopTransactionTime);
            }
            //Set pawn sec flag
            this.pwnSecLoaded = this.eConfig.Initialized;
            if (this.pwnSecLoaded)
            {
                var pStore = this.storeData.PawnSecData.GetStore();
                if (pStore != null)
                {
                    var sConfig = pStore.StoreConfiguration;
                    if (sConfig != null)
                    {
                        if (string.IsNullOrEmpty(sConfig.CompanyName))
                        {
                            sConfig.CompanyName = "Cash America";
                        }
                        if (string.IsNullOrEmpty(sConfig.TimeZone))
                        {
                            sConfig.TimeZone = "CST";
                        }
                        if (string.IsNullOrEmpty(sConfig.CompanyNumber))
                        {
                            sConfig.CompanyNumber = "1";
                        }
                    }
                    var sSiteInfo = pStore.StoreSite;
                    if (sSiteInfo != null)
                    {
                        if (string.IsNullOrEmpty(sSiteInfo.Alias))
                        {
                            sSiteInfo.Alias = "TX";
                        }

                        if (string.IsNullOrEmpty(sSiteInfo.TerminalId))
                        {
                            sSiteInfo.TerminalId = "1";
                        }

                        if (string.IsNullOrEmpty(sSiteInfo.StoreNumber))
                        {
                            sSiteInfo.StoreNumber = this.storeNumber;
                        }

                        if (string.IsNullOrEmpty(sSiteInfo.State))
                        {
                            sSiteInfo.State = "TX";
                        }

                        if (string.IsNullOrEmpty(sSiteInfo.Company))
                        {
                            sSiteInfo.Company = "Cash America";
                        }
                    }

                    var gConfig = this.eConfig.ClientConfig.GlobalConfiguration;
                    if (gConfig != null)
                    {
                        if (string.IsNullOrEmpty(gConfig.BaseLogPath))
                        {
                            gConfig.BaseLogPath = "c:\\Program Files\\Phase2App\\logs";
                        }

                    }
                }
            }
            ipform2.Dispose();
        }

        private bool generateConfigFile()
        {
            var configData = Properties.Resources.configtemplate;
            if (string.IsNullOrEmpty(configData) || this.eConfig == null)
            {
                return (false);
            }

            //Replace key strings in config file
            //PAWNSEC_USER
            string newCfgData = configData.Replace("?PAWNSEC_USER?",
                                            this.eConfig.EncryptPublicValue(this.dAPawnSecCred.UserName));
            //PAWNSEC_PWD
            newCfgData = newCfgData.Replace("?PAWNSEC_PWD?",
                                            this.eConfig.EncryptPublicValue(this.dAPawnSecCred.PassWord));
            //PAWNSEC_SCHEMA
            newCfgData = newCfgData.Replace("?PAWNSEC_SCHEMA?",
                                            this.eConfig.EncryptPublicValue(this.dAPawnSecCred.DBSchema));
            //PAWNSEC_PORT
            newCfgData = newCfgData.Replace("?PAWNSEC_PORT?",
                                            this.eConfig.EncryptPublicValue(this.dAPawnSecCred.DBPort));
            //PAWNSEC_HOST
            newCfgData = newCfgData.Replace("?PAWNSEC_HOST?",
                                            this.eConfig.EncryptPublicValue(this.dAPawnSecCred.DBHost));
            //PAWNSEC_SERVICE
            newCfgData = newCfgData.Replace("?PAWNSEC_SERVICE?",
                                            this.eConfig.EncryptPublicValue(this.dAPawnSecCred.DBService));

            //Create stream writer
            try
            {
                var sWriter = new StreamWriter(@"c:\CashlinxDesktop.exe.config", false, Encoding.Default);
                sWriter.WriteLine(newCfgData);
                sWriter.Close();
            }
            catch (Exception eX)
            {
                MessageBox.Show("Could not generate cashlinx pawn configuration file! Reason: " + eX,
                                SETUPALERT_TXT);
                return (false);
            }
            return (true);
        }

        private void processCashDrawerDetails(
            DataTable storeCashDrawerList,
            DataTable availCashDrawerUsersList,
            DataTable assignCashDrawerUsersList)
        {
            //Load cash drawers
            if (storeCashDrawerList != null && storeCashDrawerList.IsInitialized &&
                storeCashDrawerList.Rows != null && storeCashDrawerList.Rows.Count > 0)
            {
                foreach (DataRow dR in storeCashDrawerList.Rows)
                {
                    var cDrawer = new CashDrawerVO
                    {
                        Id = Utilities.GetStringValue(dR["id"]),
                        Name = Utilities.GetStringValue(dR["name"]).ToLowerInvariant(),
                        OpenFlag = Utilities.GetStringValue(dR["openflag"]),
                        RegisterUserId = Utilities.GetStringValue(dR["registeruserid"]),
                        NetName = Utilities.GetStringValue(dR["netname"]),
                        BranchId = Utilities.GetStringValue(dR["branchid"])
                    };

                    this.storeData.AllCashDrawers.Add(cDrawer);
                }
            }

            //Load available cash drawer users
            if (availCashDrawerUsersList != null && availCashDrawerUsersList.IsInitialized &&
                availCashDrawerUsersList.Rows != null && availCashDrawerUsersList.Rows.Count > 0)
            {

                foreach (DataRow dR in availCashDrawerUsersList.Rows)
                {
                    CashDrawerUserVO cDrawUsr = getCashDrawerUser(dR);
                    this.storeData.AvailCashDrawerUsers.Add(cDrawUsr);
                }

            }

            //Load assigned cash drawer users
            if (assignCashDrawerUsersList != null && assignCashDrawerUsersList.IsInitialized &&
                assignCashDrawerUsersList.Rows != null && assignCashDrawerUsersList.Rows.Count > 0)
            {
                foreach (DataRow dR in assignCashDrawerUsersList.Rows)
                {
                    CashDrawerUserVO cDrawUsr = getCashDrawerUser(dR);
                    this.storeData.AllCashDrawerUsers.Add(cDrawUsr);
                }
            }
        }

        private CashDrawerUserVO getCashDrawerUser(DataRow dR)
        {
            if (dR == null)
                return (null);

            var cDrawUsr = new CashDrawerUserVO
            {
                Id = Utilities.GetStringValue(dR["id"]),
                UserId = Utilities.GetIntegerValue(dR["userid"]),
                UserName = Utilities.GetStringValue(dR["username"]).ToLowerInvariant(),
                BranchId = Utilities.GetStringValue(dR["branchid"]),
                NetName = Utilities.GetStringValue(dR["netname"])
            };

            return (cDrawUsr);
        }

        private void storeNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!GetTextBoxData(this.storeNumberTextBox, null, out dat))
            {
                CheckmarkLabel(false, this.storeNumberLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.STORENUM;
                return;
            }
            //Store number must be at least 4 digits long
            if (dat.Length < MIN_STORENUM_LENGTH || dat.Length > MAX_STORENUM_LENGTH)
            {
                CheckmarkLabel(false, this.storeNumberLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.STORENUM;
                return;
            }
            this.storeNumber = dat;
            infoFlag |= (uint)REQINFOFLAGS.STORENUM;
            //Ensure that the store number is pre-pended with a zero if it
            //is less than the length specified by MAX_STORENUM_LENGTH
            if (this.storeNumber.Length < MAX_STORENUM_LENGTH)
            {
                this.storeNumber = this.storeNumber.PadLeft(MAX_STORENUM_LENGTH, '0');
            }
            CheckmarkLabel(true, this.storeNumberLabel);
        }

        private void storeAliasTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!GetTextBoxData(this.storeAliasTextBox, null, out dat))
            {
                CheckmarkLabel(false, this.storeAliasLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.STOREALIAS;
                return;
            }
            this.storeAlias = dat;
            infoFlag |= (uint)REQINFOFLAGS.STOREALIAS;
            CheckmarkLabel(true, this.storeAliasLabel);
        }

        private void newMachineTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!GetTextBoxData(this.newMachineTextBox, null, out dat))
            {
                //this.addNewWorkstationButton.Enabled = false;
                CheckmarkLabel(false, this.workstationMachineNameLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.NEWMACHINE;
                return;
            }
            this.newMachineName = dat;
            infoFlag |= (uint)REQINFOFLAGS.NEWMACHINE;
            //this.addNewWorkstationButton.Enabled = true;
            CheckmarkLabel(true, this.workstationMachineNameLabel);
        }

        private void newCashDrawerNameTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!GetTextBoxData(this.newCashDrawerNameTextBox, null, out dat))
            {
                //this.addNewCashDrawerButton.Enabled = false;
                CheckmarkLabel(false, this.cashdrawerLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.NEWCASHDRAWER;
                return;
            }
            this.newCashDrawerName = dat;
            infoFlag |= (uint)REQINFOFLAGS.NEWCASHDRAWER;
            //this.addNewCashDrawerButton.Enabled = true;
            CheckmarkLabel(true, this.cashdrawerLabel);
        }

        private void newUserNameTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!GetTextBoxData(this.newUserNameTextBox, null, out dat))
            {
                infoFlag &= ~(uint)REQINFOFLAGS.NEWUSERNAME;
                CheckmarkLabel(false, this.usernameLabel);
                this.addNewUserButton.Enabled = false;
                return;
            }
            this.newUserName = dat;
            infoFlag |= (uint)REQINFOFLAGS.NEWUSERNAME;
            this.addNewUserButton.Enabled = true;
            CheckmarkLabel(true, this.usernameLabel);
        }

        #region Combo Box Handlers
        private void peripheralTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.peripheralTypeComboBox.SelectedItem == null &&
                string.IsNullOrEmpty(this.peripheralTypeComboBox.SelectedText))
            {
                this.peripheralTypeSelected = string.Empty;
                this.addPeripheralButton.Enabled = false;
                return;
            }
            this.peripheralTypeSelected = this.peripheralTypeComboBox.Text;
            this.addPeripheralButton.Enabled = true;
        }

        #endregion

        #region Button Handlers
        private void mapPeripheralButton_Click(object sender, EventArgs e)
        {
            var pMgmtForm = new PeripheralMgmtForm(ref this.storeData);
            pMgmtForm.ShowDialog(this);
        }

        private void addPeripheralButton_Click(object sender, EventArgs e)
        {
            //Enter peripheral mgmt form with new peripheral type
            var pMgmtForm = new PeripheralMgmtForm(ref this.storeData);
            pMgmtForm.IncomingType = this.storeData.AllPeripheralTypes.Find(
                    p => (p.PeripheralTypeName.Equals(this.peripheralTypeSelected, StringComparison.OrdinalIgnoreCase)));
            //pMgmtForm.IncomingType.PeripheralTypeName = this.peripheralTypeSelected;
            pMgmtForm.ShowDialog(this);
        }

        private DataTable generateExistingUserTable()
        {
            if (CollectionUtilities.isEmpty(this.storeData.AllUsers)) return (null);
            var dt = new DataTable("Existing Employees");
            dt.Columns.Add("User Name");
            dt.Columns.Add("Employee #");
            dt.Columns.Add("Home Store #");
            dt.Columns.Add("Active Flag");
            foreach (var aUsr in this.storeData.AllUsers)
            {
                if (aUsr == null) continue;

                DataRow dR = dt.NewRow();
                dR["User Name"] = aUsr.X.UserName.ToLowerInvariant();
                dR["Employee #"] = aUsr.X.EmployeeNumber;
                dR["Home Store #"] = aUsr.X.FacNumber;
                dR["Active Flag"] = aUsr.Z;
                dt.Rows.Add(dR);
            }
            return (dt);
        }

        private void addNewUserButton_Click(object sender, EventArgs e)
        {
            //Enter user mgmt form with new user name
            var uForm = new UserMgmtForm();
            uForm.NewUserName = newUserName;
            DataTable exUsr = generateExistingUserTable();
            if (exUsr != null) uForm.ExistingUsers = exUsr;
            uForm.ShowDialog(this);

            if (CollectionUtilities.isNotEmpty(uForm.CreatedUsers))
            {
                foreach (var newUsr in uForm.CreatedUsers)
                {
                    //this.storeData.UsersInserts.Add(new QuadType<UserVO, LDAPUserVO, string, string>(newUsr.Left, newUsr.Right, "1", string.Empty));
                    this.storeData.AllUsers.Add(new QuadType<UserVO, LDAPUserVO, string, string>(newUsr.Left, newUsr.Right, "1", string.Empty));
                    this.storeData.AllCashDrawerUsers.Add(new CashDrawerUserVO("0",
                                                                               0,
                                                                               newUsr.Left.UserName.ToLowerInvariant(),
                                                                               this.storeData.StoreInfo.StoreId,
                                                                               "0"));
                    this.storeData.AllCashDrawers.Add(
                        new CashDrawerVO("0",
                            string.Format("{0}_{1}",
                            this.storeNumber.PadLeft(5, '0'),
                            newUsr.Left.UserName.ToLowerInvariant()), "0", "0", "0"));
                    //                    this.storeData.CashDrawerUserInserts.Add(new CashDrawerUserVO("0", 0, "0", this.storeData.StoreInfo.StoreId, "1160"));
                    //                    this.storeData.CashDrawerInserts.Add(new CashDrawerVO("0", string.Empty, "0", "0", "1160"));
                }
            }
        }

        private void setupUserButton_Click(object sender, EventArgs e)
        {
            //Enter user mgmt form
            var uForm = new UserMgmtForm();
            DataTable exUsr = generateExistingUserTable();
            if (exUsr != null) uForm.ExistingUsers = exUsr;
            uForm.ShowDialog(this);

            if (uForm.AddedUsers && CollectionUtilities.isNotEmpty(uForm.CreatedUsers))
            {
                foreach (var newUsr in uForm.CreatedUsers)
                {
                    //this.storeData.UsersInserts.Add(new QuadType<UserVO, LDAPUserVO, string, string>(newUsr.Left, newUsr.Right, "1", string.Empty));
                    this.storeData.AllUsers.Add(new QuadType<UserVO, LDAPUserVO, string, string>(newUsr.Left, newUsr.Right, "1", string.Empty));
                    this.storeData.AllCashDrawerUsers.Add(new CashDrawerUserVO("0",
                                                                               0,
                                                                               newUsr.Left.UserName.ToLowerInvariant(),
                                                                               this.storeData.StoreInfo.StoreId,
                                                                               "0"));
                    this.storeData.AllCashDrawers.Add(
                        new CashDrawerVO("0",
                            string.Format("{0}_{1}",
                            this.storeNumber.PadLeft(5, '0'),
                            newUsr.Left.UserName.ToLowerInvariant()), "0", "0", "0"));
                    //this.storeData.CashDrawerUserInserts.Add(new CashDrawerUserVO("0", 0, newUsr.Left.UserName, this.storeData.StoreInfo.StoreId, "1160"));
                    //this.storeData.CashDrawerInserts.Add(new CashDrawerVO("0", newUsr.Left.UserName + "_CD", "0", "0", "1160"));
                }
            }
        }

        private void addNewCashDrawerButton_Click(object sender, EventArgs e)
        {
            //Do Nothing
        }

        private void assignCashDrawerButton_Click(object sender, EventArgs e)
        {
            //Do Nothing
        }

        private void viewWorkstationSummaryButton_Click(object sender, EventArgs e)
        {
            //Show workstation state summary 
            var dt = new DataTable("Workstations");
            //dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("# Peripherals");
            dt.Columns.Add("Store Number");
            foreach (var wkvo in this.storeData.AllWorkstations)
            {
                if (wkvo == null) continue;
                var dR = dt.NewRow();
                var wkName = wkvo.Name;
                dR["Name"] = wkName;
                //Lookup workstation peripheral map
                int numPeriphs = 0;
                if (CollectionUtilities.isNotEmptyContainsKey(this.storeData.PawnWorkstationPeripheralMap, wkName))
                {
                    var listPer = this.storeData.PawnWorkstationPeripheralMap[wkName];
                    if (CollectionUtilities.isNotEmpty(listPer))
                    {
                        numPeriphs = listPer.Count;
                    }
                }
                dR["# Peripherals"] = numPeriphs.ToString();
                dR["Store Number"] = wkvo.StoreNumber;
                dt.Rows.Add(dR);
            }
            var sumWk = new SummaryForm(dt, "Workstation Summary", true);
            sumWk.ShowDialog(this);
        }

        private void viewCashDrawerSummaryButton_Click(object sender, EventArgs e)
        {
            //Show cash drawer state summary
            var dt = new DataTable("Cash Drawers");
            dt.Columns.Add("Name");
            dt.Columns.Add("User");
            dt.Columns.Add("Store #");
            dt.Columns.Add("Status");
            foreach (var cdvo in this.storeData.AllCashDrawers)
            {
                if (cdvo == null) continue;
                string registerUserId = cdvo.RegisterUserId;
                CashDrawerUserVO cUser = this.storeData.AllCashDrawerUsers.Find(x => x.Id.Equals(registerUserId, StringComparison.OrdinalIgnoreCase));
                if (cUser == null) continue;
                var dR = dt.NewRow();
                dR["Name"] = cdvo.Name;
                dR["User"] = cUser.UserName.ToLowerInvariant();
                dR["Store #"] = (cdvo.BranchId.Equals(this.storeData.StoreInfo.StoreId, StringComparison.OrdinalIgnoreCase))
                                        ? this.storeData.StoreInfo.StoreNumber : cdvo.BranchId;
                dR["Status"] =
                    (cdvo.OpenFlag == "0") ? "CLOSED" :
                    (cdvo.OpenFlag == "1") ? "OPEN" :
                    (cdvo.OpenFlag == "3") ? "DELETED" : "UNVERIFIED";
                dt.Rows.Add(dR);
            }
            var sumCd = new SummaryForm(dt, "Cash Drawer Summary", true);
            sumCd.ShowDialog(this);
        }

        private void viewUsersSummaryButton_Click(object sender, EventArgs e)
        {
            //Show users summary
            var dt = generateExistingUserTable();
            if (dt != null)
            {
                var sumUsr = new SummaryForm(dt, "User Summary", true);
                sumUsr.ShowDialog(this);
            }
        }

        private void viewPeripheralsSummaryButton_Click(object sender, EventArgs e)
        {
            //Show peripherals summary
            var dt = new DataTable("Peripherals");
            dt.Columns.Add("Name");
            dt.Columns.Add("Type");
            dt.Columns.Add("IP Address");
            dt.Columns.Add("Port");
            dt.Columns.Add("Store #");
            foreach (var pvo in this.storeData.AllPeripherals)
            {
                if (pvo == null) continue;
                var dR = dt.NewRow();
                dR["Name"] = pvo.Name;
                dR["Type"] = pvo.PeriphType.PeripheralTypeName;
                dR["IP Address"] = pvo.IPAddress;
                dR["Port"] = pvo.Port;
                dR["Store #"] = (pvo.StoreId == this.storeData.StoreInfo.StoreId)
                                        ? this.storeData.StoreInfo.StoreNumber : pvo.StoreId;
                dt.Rows.Add(dR);
            }
            var sumPer = new SummaryForm(dt, "Peripheral Summary", true);
            sumPer.ShowDialog(this);
        }

        private void submitConfigurationButton_Click(object sender, EventArgs e)
        {
            //Perform smart select / insert(or update) operations to sync
            //the associated tables to the proper state as shown in the tool
            var progForm = new InProgressForm("* GENERATING AUTO-BACKUP *");
            DateTime now = DateTime.Now;
            List<string> ccsownerSql;
            List<string> pwnSecSql;
            //Generate auto back up set
            var backupListSuccess = generateQueries(@"c:\autobackup_" + now.Ticks + @".sql",
                                                    @"c:\pwnsecautobackup_" + now.Ticks + @".sql",
                                                    true,
                                                    out ccsownerSql,
                                                    out pwnSecSql);

            //Do not hold backup SQL statements in the list, they only go to the file
            if (ccsownerSql != null) ccsownerSql.Clear();
            if (pwnSecSql != null) pwnSecSql.Clear();

            if (!backupListSuccess)
            {
                MessageBox.Show(
                        "Backup operation did not successfully write all records.  Insufficient data to create a working backup.",
                        SETUPALERT_TXT,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }

            progForm.Message = "* GENERATING ALL QUERIES *";

            //Generate the full non-backup set (includes new entries)
            var nonBackupListSuccess = generateQueries(null, null, false, out ccsownerSql, out pwnSecSql);
            progForm.HideMessage();
            progForm.Dispose();

            if (!nonBackupListSuccess)
            {
                MessageBox.Show(
                        "Could not generate all necessary records for proper configuration.  Please check your setup data.",
                        SETUPALERT_TXT,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                return;
            }
            #region DONOTUSECODE
            /*var res =
                    MessageBox.Show(
                            "SQL Generation Complete.  Are you ready to start the transaction block to merge these changes to the database?",
                            SETUPALERT_TXT,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Stop);
            bool errFnd = false;
            try
            {
                if (res == DialogResult.Yes)
                {
                    var checkRes = MessageBox.Show(
                            "Would you like to select which CCSOWNER statements to run individually?",
                            SETUPALERT_TXT,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                    List<string> ccsownerSqlSelect = null;
                    if (checkRes == DialogResult.Yes)
                    {
                        generateSelectableListAndShow(ccsownerSql, out ccsownerSqlSelect);
                    }
                    var sqlList = ccsownerSqlSelect;
                    if (sqlList == null || CollectionUtilities.isEmpty(sqlList))
                    {
                        MessageBox.Show("You have no CCSOWNER sql statements to execute. Continuing to PAWNSEC section.", SETUPALERT_TXT);                        
                    }
                    else
                    {
                        //Run through all statements generated and execute them
                        if (DataAccessService.StartTransactionBlock(CCSOWNER, "CCSOWNER CHANGE", ref dACcs))
                        {
                            foreach (var sql in sqlList)
                            {
                                if (string.IsNullOrEmpty(sql))
                                    continue;
                                if (!DataAccessService.ExecuteInsertUpdateDeleteQuery(sql, CCSOWNER, ref dACcs))
                                {
                                    errFnd = true;
                                    break;
                                }
                            }

                            if (errFnd)
                            {
                                DataAccessService.RollbackTransactionBlock(CCSOWNER, "CCSOWNER CHANGE", ref dACcs);
                                MessageBox.Show(
                                        "Error occurred during CCSOWNER SQL execution.  Executing transaction roll back",
                                        SETUPALERT_TXT);
                                return;
                            }
                            var ccsRes =
                                    MessageBox.Show(
                                            "All CCSOWNER related changes have been executed.  Would you like to commit?",
                                            SETUPALERT_TXT,
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);
                            if (ccsRes == DialogResult.Yes)
                            {
                                var commitRes = DialogResult.Yes;
                                var successFlag = false;
                                while (commitRes == DialogResult.Yes && !successFlag)
                                {
                                    successFlag = DataAccessService.CommitTransactionBlock(CCSOWNER,
                                                                                           "CCSOWNER CHANGE",
                                                                                           ref dACcs);
                                    if (!successFlag)
                                    {
                                        commitRes =
                                                MessageBox.Show(
                                                        "Could not commit transaction for CCSOWNER changes.  Would you like to try the commit again?",
                                                        SETUPALERT_TXT,
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Error);
                                    }
                                }
                                if (!successFlag)
                                {
                                    DataAccessService.RollbackTransactionBlock(CCSOWNER, "CCSOWNER CHANGE", ref dACcs);
                                    return;
                                }
                            }
                                    //User did not want to commit changes
                            else
                            {
                                DataAccessService.RollbackTransactionBlock(CCSOWNER, "CCSOWNER CHANGE", ref dACcs);
                                return;
                            }
                        }
                                //Transaction block not started
                        else
                        {
                            MessageBox.Show("Could not start transaction block for CCSOWNER changes.",
                                            SETUPALERT_TXT);
                            return;
                        }
                    }
                }
                //User did not want to execute sql statements
                else
                {
                    //See if the user wants to generate the pawn sec statements
                    var dRes = MessageBox.Show("Would you like to continue on to the PAWNSEC portion of this process?  If you choose no, the submit process will be cancelled.",
                                               SETUPALERT_TXT,
                                               MessageBoxButtons.YesNo);
                    if (dRes == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            catch(Exception eX)
            {
                MessageBox.Show("Error occurred while processing CCSOWNER changes. Please try again.  Error Details: " + Environment.NewLine + eX.Message,
                                SETUPALERT_TXT);
                return;
            }*/

            /*
            try
            {
                res = MessageBox.Show("Would you like to execute the PAWNSEC merge changes against the database?",
                                      SETUPALERT_TXT,
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Stop);
                if (res == DialogResult.No)
                {
                    return;
                }

                var checkRes = MessageBox.Show(
                        "Would you like to select which PAWNSEC statements to run individually?",
                        SETUPALERT_TXT,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                List<string> pawnsecSqlSelect = null;
                if (checkRes == DialogResult.Yes)
                {
                    generateSelectableListAndShow(pwnSecSql, out pawnsecSqlSelect);
                }

                var sqlList = pawnsecSqlSelect;
                if (sqlList == null || CollectionUtilities.isEmpty(sqlList))
                {
                    MessageBox.Show("You have no PAWNSEC sql statements to execute. Exiting PAWNSEC sql execution.", SETUPALERT_TXT);
                }
                else
                {

                    if (DataAccessService.StartTransactionBlock(PAWNSEC, "PAWNSEC CHANGE", ref dAPawnSec))
                    {
                        foreach (var sql in pwnSecSql)
                        {
                            if (string.IsNullOrEmpty(sql))
                                continue;
                            if (!DataAccessService.ExecuteInsertUpdateDeleteQuery(sql, PAWNSEC, ref dAPawnSec))
                            {
                                errFnd = true;
                                break;
                            }
                        }
                        if (errFnd)
                        {
                            DataAccessService.RollbackTransactionBlock(PAWNSEC, "PAWNSEC CHANGE", ref dAPawnSec);
                            MessageBox.Show(
                                    "Error occurred during PAWNSEC SQL execution.  Executing transaction roll back",
                                    SETUPALERT_TXT);
                            return;
                        }
                        var ccsRes =
                                MessageBox.Show(
                                        "All PAWNSEC related changes have been executed.  Would you like to commit?",
                                        SETUPALERT_TXT,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);
                        if (ccsRes == DialogResult.Yes)
                        {
                            var commitRes = DialogResult.Yes;
                            var successFlag = false;
                            while (commitRes == DialogResult.Yes && !successFlag)
                            {
                                successFlag = DataAccessService.CommitTransactionBlock(PAWNSEC,
                                                                                       "PAWNSEC CHANGE",
                                                                                       ref dAPawnSec);
                                if (!successFlag)
                                {
                                    commitRes =
                                            MessageBox.Show(
                                                    "Could not commit transaction for PAWNSEC changes.  Would you like to try the commit again?",
                                                    SETUPALERT_TXT,
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Error);
                                }
                            }
                            if (!successFlag)
                            {
                                DataAccessService.RollbackTransactionBlock(PAWNSEC, "PAWNSEC CHANGE", ref dAPawnSec);
                                return;
                            }
                        }
                                //User did not want to commit changes
                        else
                        {
                            DataAccessService.RollbackTransactionBlock(PAWNSEC, "PAWNSEC CHANGE", ref dAPawnSec);
                            return;
                        }                    
                    }
                    //Could not begin transaction block
                    else
                    {
                        MessageBox.Show("Could not start transaction block for PAWNSEC changes.",
                                        SETUPALERT_TXT);
                        return;
                    }
                }
            }
            catch(Exception eX)
            {
                MessageBox.Show("Error occurred while processing PAWNSEC changes. Please try again.  Error Details: " + Environment.NewLine + eX.Message,
                                SETUPALERT_TXT);
                return;
                
            }*/
            #endregion
        }

        private void generateSelectableListAndShow(List<string> origList, out List<string> selectedList)
        {
            selectedList = new List<string>();
            if (CollectionUtilities.isEmpty(origList))
            {
                return;
            }

            var sumFm = new SelectionForm(origList);
            var sFmRes = sumFm.ShowDialog(this);
            if (sFmRes == DialogResult.Cancel)
            {
                return;
            }

            selectedList = new List<string>(origList.Count);
            var selections = sumFm.Selections;
            var idx = 0;
            foreach (var bVal in selections)
            {
                if (bVal)
                {
                    selectedList.Add(origList[idx]);
                }
                ++idx;
            }
        }

        private void couchSetupButton_Click(object sender, EventArgs e)
        {
            var cForm = new CouchDBForm();
            if (eConfig != null && this.couchDBService == null)
            {
                DatabaseServiceVO cVo = eConfig.GetCouchDBService();
                if (cVo != null)
                {
                    cForm.CouchDBServer = eConfig.DecryptValue(cVo.Server);
                    cForm.CouchDBPort = eConfig.DecryptValue(cVo.Port);
                    cForm.CouchDBDatabase = eConfig.DecryptValue(cVo.Schema);
                }
            }
            else if (this.couchDBService == null)
            {
                cForm.CouchDBDatabase = "";
                cForm.CouchDBPort = "5984";
                cForm.CouchDBServer = "";
            }
            else
            {
                cForm.CouchDBServer = this.couchDBService.Server;
                cForm.CouchDBPort = this.couchDBService.Port;
                cForm.CouchDBDatabase = this.couchDBService.Schema;
            }
            cForm.ShowDialog(this);
            this.couchDBService = new DatabaseServiceVO(string.Empty, string.Empty, true);
            this.couchDBService.Server = cForm.CouchDBServer;
            this.couchDBService.Port = cForm.CouchDBPort;
            this.couchDBService.Schema = cForm.CouchDBDatabase;
        }
        #endregion

        private void reconcilePawnsecMachinesToWorkstations()
        {
            //bool fndMachines = this.storeData.PawnSecData.ClientMachines.Count > 0;
            var pSecMappedMachines =
                    from workStations in this.storeData.AllWorkstations
                    from pSecMachines in this.storeData.PawnSecData.ClientMachines
                    where workStations.Name.Equals(pSecMachines.Machine.WorkstationName, StringComparison.OrdinalIgnoreCase)
                    select pSecMachines;
            if (!pSecMappedMachines.Any())
            {
                MessageBox.Show("No pawn sec machines have workstation mappings.");
                foreach (var pMac in this.storeData.PawnSecData.ClientMachines)
                {
                    if (pMac == null) continue;
                    var wkVo = new WorkstationVO(this.storeData.StoreInfo.StoreId);
                    wkVo.Name = pMac.Machine.WorkstationName;
                    wkVo.StoreNumber = this.storeNumber;
                    var wkst = this.storeData.AllWorkstations.Find(x => x.Name.Equals(wkVo.Name, StringComparison.OrdinalIgnoreCase));
                    if (wkst == null)
                    {
                        this.storeData.AllWorkstations.Add(wkVo);
                        //this.storeData.WorkstationsInserts.Add(wkVo);
                    }
                }

                //Should not have a case where existent workstations are not mapped to
                //pawn security workstations
                /*foreach (var wkst in this.storeData.AllWorkstations)
                {
                    if (wkst == null) continue;
                    WorkstationVO wkst1 = wkst;
                    var pSecMac =
                            this.storeData.PawnSecData.ClientMachines.Find(
                                    x => x.Machine.WorkstationName.Equals(wkst1.Name));
                    if (pSecMac == null)
                    {
                        var newCliMac = new PawnSecVO.ClientPawnSecMachineVO();
                        ulong nexId;
                        this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.CLIREG_ID, out nexId);
                        newCliMac.Machine.ClientId = nexId;
                        newCliMac.Machine.MachineName = wkst1.Name + PawnSecSetupForm.CASHAM_DOMAIN;
                        newCliMac.Machine.WorkstationName = wkst1.Name;
                        newCliMac.Machine.IsAllowed = true;
                        this.storeData.PawnSecData.ClientMachines.Add(new PawnSecVO.ClientPawnSecMachineVO());
                        var newCliMacMapEntry = new PawnSecVO.ClientStoreMapVO();
                        this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.CLISTOMAP_ID, out nexId);
                        newCliMacMapEntry.Id = nexId;
                        newCliMacMapEntry.


                    }*/
            }
            else
            {
                var pSecUnmappedMachines = this.storeData.PawnSecData.ClientMachines.Except(pSecMappedMachines);

                if (pSecUnmappedMachines != null)
                {
                    MessageBox.Show("There are some pawn sec machines that have no workstation mappings.");
                    foreach (var uMac in pSecUnmappedMachines)
                    {
                        var wkVo = new WorkstationVO(this.storeData.StoreInfo.StoreId);
                        wkVo.Name = uMac.Machine.WorkstationName;
                        wkVo.StoreNumber = this.storeNumber;
                        var wkst = this.storeData.AllWorkstations.Find(x => x.Name.Equals(wkVo.Name, StringComparison.OrdinalIgnoreCase));
                        if (wkst == null)
                        {
                            this.storeData.AllWorkstations.Add(wkVo);
                            //this.storeData.WorkstationsInserts.Add(wkVo);
                        }
                    }
                }
            }

            //Verify that the encryption container is active
            if (this.eConfig == null)
            {
                //Pawn sec has been setup, retrieve and set data
                this.eConfig = new EncryptedConfigContainer(Common.Properties.Resources.PrivateKey,
                    this.publicKey, this.storeNumber, this.storeData.PawnSecData);
                //Set shop date time
                this.shopDateTime = ShopDateTime.Instance;
                this.shopDateTime.setOffsets(0, 0, 0, 0, 0, 0, 0);
                this.shopDateTime.SetDatabaseTime(curDatabaseTime);
                PawnSecVO.PawnSecStoreVO curStore = this.storeData.PawnSecData.GetStore();
                if (curStore != null)
                {
                    this.shopDateTime.SetPawnSecOffsetTime(curStore.StoreConfiguration);
                    writeMessage("Shop date time has been set to " +
                                 this.shopDateTime.ShopTransactionTime);
                }
            }
        }

        private void pawnSecSetupButton_Click(object sender, EventArgs e)
        {
            var pSecForm = new PawnSecSetupForm(this.dAPawnSec,
                                                SecurityAccessor.Instance,
                                                this.storeData,
                                                this.storeData.PawnSecData,
                                                this.eConfig,
                                                this.publicKey,
                                                this.storeNumber,
                                                false);
            pSecForm.ShowDialog(this);
            this.reconcilePawnsecMachinesToWorkstations();
            pSecForm.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        private string generateGuid(string candidate)
        {
            if (string.IsNullOrEmpty(candidate) || candidate.Length < GUID_MIN_LENGTH)
            {
                string newGuid;
                if (!PawnStoreProcedures.GenerateGUID(ref this.dACcs, out newGuid))
                {
                    return (candidate);
                }
                return (newGuid);
            }
            return (candidate);
        }

        private void generateAllGuids(StreamWriter writer)
        {
            //writer.WriteLine("--generateAllGuids()...");

            //Peripherals
            for (int i = 0; i < this.storeData.AllPeripherals.Count; ++i)
            {
                var curPer = this.storeData.AllPeripherals[i];
                if (curPer == null)
                    continue;
                curPer.Id = this.generateGuid(curPer.Id);
                writer.WriteLine("-- Peripheral Id = " + curPer.Id);
            }
            //cd workstations
            for (int i = 0; i < this.storeData.AllWorkstations.Count; ++i)
            {
                var wkst = this.storeData.AllWorkstations[i];
                if (wkst == null)
                    continue;
                wkst.Id = this.generateGuid(wkst.Id);
                writer.WriteLine("-- CDWorkstation Id = " + wkst.Id);
            }
            //cd users
            for (int i = 0; i < this.storeData.AllCashDrawerUsers.Count; ++i)
            {
                var curUsr = this.storeData.AllCashDrawerUsers[i];
                if (curUsr == null)
                    continue;

                //Ensure the cashdraweruser id is populated
                curUsr.Id = this.generateGuid(curUsr.Id);
                writer.WriteLine("-- CDCashDrawerUser Id = " + curUsr.Id);
            }
            //cd cashdrawers
            for (int i = 0; i < this.storeData.AllCashDrawers.Count; ++i)
            {
                var curDrawer = this.storeData.AllCashDrawers[i];
                if (curDrawer == null)
                    continue;
                curDrawer.Id = this.generateGuid(curDrawer.Id);
                writer.WriteLine("-- CDCashDrawer Id = " + curDrawer.Id);
            }
            //workstation peripherals
            for (int i = 0; i < this.storeData.AllWorkstations.Count; ++i)
            {
                var mapKey = this.storeData.AllWorkstations[i];
                if (mapKey == null || string.IsNullOrEmpty(mapKey.Name))
                    continue;
                var mapKeyNm = mapKey.Name;
                if (CollectionUtilities.isNotEmptyContainsKey(this.storeData.PawnWorkstationPeripheralMap, mapKeyNm))
                {
                    var mapEntry = this.storeData.PawnWorkstationPeripheralMap[mapKeyNm];
                    if (mapEntry == null || CollectionUtilities.isEmpty(mapEntry))
                        continue;

                    for (int j = 0; j < mapEntry.Count; ++j)
                    {
                        var mapEntrySubItem = mapEntry[j];
                        if (mapEntrySubItem == null)
                            continue;
                        mapEntrySubItem.Left = this.generateGuid(mapEntrySubItem.Left);
                        writer.WriteLine("-- CDworkstation Id = " + mapKey.Id + " - Mapping Id = " +
                                         mapEntrySubItem.Left);
                    }
                }
            }
            //userinfo details
            for (int i = 0; i < this.storeData.AllUsers.Count; ++i)
            {
                var userEntry = this.storeData.AllUsers[i];
                if (userEntry == null)
                    continue;

                userEntry.W = this.generateGuid(userEntry.W);
                writer.WriteLine("-- UserInfoDetail Id = " + userEntry.W);
            }
        }

        /// <summary>
        /// Generates all queries used to update the state of the database
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="pSecFileName"></param>
        /// <param name="isBackup"></param>
        /// <param name="sqlStatements"></param>
        /// <param name="pwnSecSqlStatements"></param>
        private bool generateQueries(
            string fileName, string pSecFileName,
            bool isBackup,
            out List<string> sqlStatements,
            out List<string> pwnSecSqlStatements)
        {
            sqlStatements = new List<string>(100);
            pwnSecSqlStatements = new List<string>(100);
            DateTime now = DateTime.Now;
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = @"c:\queries_" + now.Ticks + @".sql";
            }
            if (string.IsNullOrEmpty(pSecFileName))
            {
                pSecFileName = @"c:\psecqueries_" + now.Ticks + @".sql";
            }
            var fileStream = new FileStream(fileName, FileMode.Create);
            var pFileStream = new FileStream(pSecFileName, FileMode.Create);
            var writer = new StreamWriter(fileStream);
            var pWriter = new StreamWriter(pFileStream);

            if (!isBackup)
                this.generateAllGuids(writer);
            else
                writer.WriteLine("-- Not generating GUIDs because this is a backup!");

            /*if (!isBackup)
            {
                //Need peripheral mapping delete statements
                var sqlArr = this.generatePeripheralDeletes(this.storeData.AllPeripherals);
                if (sqlArr != null && sqlArr.Count() > 0)
                {
                    writer.WriteLine("-- CCSOWNER PERIPHERAL DELETE STATEMENTS --");
                    foreach (var str in sqlArr)
                    {
                        sqlStatements.Add(str);
                        writer.WriteLine(str + ";" + Environment.NewLine);
                    }
                    writer.WriteLine("-- END CCSOWNER PERIPHERAL DELETE STATEMENTS --");
                }


                //Need peripheral delete statements
                foreach(var wkst in this.storeData.AllWorkstations)
                {
                    if (wkst == null) continue;
                    if (!string.IsNullOrEmpty(wkst.Id) && 
                        wkst.Id.Length >= GUID_MIN_LENGTH && 
                        CollectionUtilities.isNotEmptyContainsKey(this.storeData.WorkstationPeripheralMap, wkst))
                    {
                        sqlArr = this.generateWorkstationPeripheralDeletes(wkst, this.storeData.WorkstationPeripheralMap[wkst]);
                        if (sqlArr != null && sqlArr.Count() > 0)
                        {
                            writer.WriteLine("-- CCSOWNER WORKSTATIONPERIPHERAL DELETE STATEMENTS --");
                            foreach(var str in sqlArr)
                            {
                                sqlStatements.Add(str);
                                writer.WriteLine(str + ";" + Environment.NewLine);
                            }
                            writer.WriteLine("-- END CCSOWNER WORKSTATIONPERIPHERAL DELETE STATEMENTS --");
                        }
                    }
                }                
            }*/

            writer.WriteLine("-- CCSOWNER PERIPHERAL MERGE STATEMENTS");
            if (CollectionUtilities.isNotEmpty(this.storeData.AllPeripherals))
            {
                for (int i = 0; i < this.storeData.AllPeripherals.Count; ++i)
                {
                    PeripheralVO per = this.storeData.AllPeripherals[i];
                    if (per == null)
                        continue;

                    var insPerQuery = generatePeripheralInsert(ref per, isBackup);
                    if (!string.IsNullOrEmpty(insPerQuery))
                    {
                        sqlStatements.Add(insPerQuery);
                        writer.WriteLine(insPerQuery + ";" + Environment.NewLine);
                    }
                }
            }
            writer.WriteLine("-- END CCSOWNER PERIPHERAL MERGE STATEMENTS");


            writer.WriteLine("-- CDOWNER WORKSTATION MERGE STATEMENTS");
            for (int i = 0; i < this.storeData.AllWorkstations.Count; ++i)
            {
                WorkstationVO wkst = this.storeData.AllWorkstations[i];
                if (wkst == null)
                    continue;
                var insWkstQuery = generateWorkstationInsert(ref wkst, isBackup);
                if (!string.IsNullOrEmpty(insWkstQuery))
                {
                    sqlStatements.Add(insWkstQuery);
                    writer.WriteLine(insWkstQuery + ";" + Environment.NewLine);
                }
            }
            writer.WriteLine("-- END CDOWNER WORKSTATION MERGE STATEMENTS");

            writer.WriteLine("-- CCSOWNER \\ CDOWNER WORKSTATION PERIPHERAL MAPPING MERGE STATEMENTS");
            for (int i = 0; i < this.storeData.AllWorkstations.Count; ++i)
            {
                WorkstationVO wkst = this.storeData.AllWorkstations[i];
                if (wkst == null || string.IsNullOrEmpty(wkst.Name))
                    continue;
                string wkstName = wkst.Name;
                //For each workstation, take the peripheral mapping and insert into the workstation
                //peripheral table
                if (CollectionUtilities.isNotEmptyContainsKey(this.storeData.PawnWorkstationPeripheralMap, wkstName))
                {
                    var perMappedList = this.storeData.PawnWorkstationPeripheralMap[wkstName];
                    if (CollectionUtilities.isNotEmpty(perMappedList))
                    {
                        var insWkstMapQuerys = generateWorkstationPeripheralInsert(ref wkst, ref perMappedList, isBackup);
                        foreach (var insStr in insWkstMapQuerys)
                        {
                            if (!string.IsNullOrEmpty(insStr))
                            {
                                writer.WriteLine(insStr + ";" + Environment.NewLine);
                                sqlStatements.Add(insStr);
                            }
                        }
                    }
                }
            }
            writer.WriteLine("-- END CCSOWNER \\ CDOWNER WORKSTATION PERIPHERAL MAPPING MERGE STATEMENTS");

            if (isBackup)
            {
                writer.WriteLine("-- CDOWNER CASH DRAWER USER MERGE STATEMENTS (BACKUP ONLY)");
                for (int i = 0; i < this.storeData.AllCashDrawerUsers.Count; ++i)
                {
                    CashDrawerUserVO cuVo = this.storeData.AllCashDrawerUsers[i];
                    if (cuVo == null)
                        continue;

                    var query = this.generateCashDrawerUserInsert(ref cuVo, true);
                    if (!string.IsNullOrEmpty(query))
                    {
                        sqlStatements.Add(query);
                        writer.WriteLine(query + ";" + Environment.NewLine);
                    }
                }
                writer.WriteLine("-- END CDOWNER CASH DRAWER USER MERGE STATEMENTS (BACKUP ONLY)");

                writer.WriteLine("-- CDOWNER CASHDRAWER MERGE STATEMENTS (BACKUP ONLY)");
                for (int i = 0; i < this.storeData.AllCashDrawers.Count; ++i)
                {
                    CashDrawerVO cVo = this.storeData.AllCashDrawers[i];
                    if (cVo == null)
                        continue;
                    CashDrawerVO vo = cVo;
                    var cUsr = this.storeData.AllCashDrawerUsers.Find(x => x.Id.Equals(vo.RegisterUserId, StringComparison.OrdinalIgnoreCase));
                    var query = this.generateCashDrawerInsert(ref cUsr, ref cVo, true);
                    if (!string.IsNullOrEmpty(query))
                    {
                        sqlStatements.Add(query);
                        writer.WriteLine(query + ";" + Environment.NewLine);
                    }
                }
                writer.WriteLine("-- END CDOWNER CASHDRAWER MERGE STATEMENTS (BACKUP ONLY)");

            }

            writer.WriteLine("-- CCSOWNER \\ CDOWNER USER RELATED MERGE STATEMENTS");
            //For each new user
            //Insert into userinfo, userinfodetail, userroles, cd_cashdraweruser, and cd_cashdrawer, 
            var startUsrId = 0;
            if (!isBackup)
            {
                ulong retUsrId;
                if (PawnStoreProcedures.GetNextIdStr(ref this.dACcs, "USERID", "USERINFO", out retUsrId))
                {
                    startUsrId = (int)retUsrId;
                }
                writer.WriteLine("-- STARTING NEW USERID IF NEEDED = {0}", startUsrId);
            }

            int curUsrId;
            for (int i = 0; i < this.storeData.AllUsers.Count; ++i)
            {
                QuadType<UserVO, LDAPUserVO, string, string> usr = this.storeData.AllUsers[i];
                var usrVo = usr.X;
                var userInfoDetId = usr.W;
                if (usrVo == null)
                {
                    continue;
                }
                if (!isBackup)
                {
                    UserVO vo = usrVo;
                    CashDrawerUserVO cUser = null;
                    if (CollectionUtilities.isNotEmpty(this.storeData.AllCashDrawerUsers) &&
                        !string.IsNullOrEmpty(vo.UserID))
                    {
                        cUser = this.storeData.AllCashDrawerUsers.Find(x => x.UserName.Equals(vo.UserName, StringComparison.OrdinalIgnoreCase));
                    }
                    CashDrawerVO cDrawer = null;
                    var cDrawerName = string.Format("{0}_{1}", this.storeNumber.PadLeft(5, '0'), usrVo.UserName.ToLowerInvariant());
                    if (CollectionUtilities.isNotEmpty(this.storeData.AllCashDrawers))
                    {
                        cDrawer = this.storeData.AllCashDrawers.Find(x => x.Name.Equals(cDrawerName, StringComparison.OrdinalIgnoreCase));
                    }
                    if (cUser == null)
                    {
                        if (!string.IsNullOrEmpty(vo.UserID))
                        {
                            if (!Int32.TryParse(vo.UserID, out curUsrId))
                            {
                                curUsrId = startUsrId;
                                ++startUsrId;
                                usrVo.UserID = curUsrId.ToString();
                            }
                            else
                            {
                                if (curUsrId == 0)
                                {
                                    curUsrId = startUsrId;
                                    ++startUsrId;
                                }
                                usrVo.UserID = curUsrId.ToString();
                            }
                        }
                        else
                        {
                            curUsrId = startUsrId;
                            ++startUsrId;
                            usrVo.UserID = curUsrId.ToString();
                        }
                    }
                    else
                    {
                        if (usrVo.UserID.Equals("0"))
                        {
                            usrVo.UserID = startUsrId.ToString();
                            ++startUsrId;
                        }
                        cUser.UserId = Utilities.GetIntegerValue(usrVo.UserID);
                        curUsrId = cUser.UserId;
                    }
                    if (cUser == null)
                    {
                        curUsrId = startUsrId;
                        ++startUsrId;
                        cUser = new CashDrawerUserVO(
                                null,
                                curUsrId,
                                usrVo.UserName.ToLowerInvariant(),
                                this.storeData.StoreInfo.StoreId,
                                DEFAULT_NETNAME);
                        cUser.UserId = curUsrId;
                        usrVo.UserID = curUsrId.ToString();
                    }

                    if (cDrawer == null)
                    {
                        cDrawer = new CashDrawerVO(null,
                                                   cDrawerName.ToLowerInvariant(),
                                                   "0",
                                                   null,
                                                   DEFAULT_NETNAME);
                    }



                    //UI_ID, UI_NAME, UI_FNAME, UI_LNAME, UI_STONUM
                    //MERGE_CCSOWNER_USERINFO
                    var insUsrInfoQuery = generateUserInfoInsert(ref usrVo, isBackup);
                    //UID_ID, UID_USID, UID_STONUM, UID_EMNUM
                    //MERGE_CCSOWNER_USERINFODETAIL
                    var insUsrInfoDetQuery = generateUserInfoDetailInsert(ref usrVo, ref userInfoDetId, isBackup);
                    //UR_ID, UR_RID
                    //MERGE_CCSOWNER_USERROLES
                    var insUsrRolesQuery = generateUserRolesInsert(usrVo, isBackup);
                    //CDU_ID, CDU_NAME, CDU_STONUM, CDU_BRID
                    //MERGE_CDOWNER_CASHDRAWERUSER
                    var insCDUsr = generateCashDrawerUserInsert(ref cUser, isBackup);
                    //CD_ID, CD_NAME, CD_MGRID, CD_OTYPE, CD_STONUM, CD_BRID
                    //MERGE_CDOWNER_CASHDRAWER
                    var insCD = generateCashDrawerInsert(ref cUser, ref cDrawer, isBackup);

                    //Only add the inserts if all insert strings (except for the role insert) are valid
                    if (!string.IsNullOrEmpty(insUsrInfoQuery) &&
                        !string.IsNullOrEmpty(insUsrInfoDetQuery) &&
                        !string.IsNullOrEmpty(insCDUsr) &&
                        !string.IsNullOrEmpty(insCD))
                    {
                        writer.WriteLine(insUsrInfoQuery + ";" + Environment.NewLine);
                        sqlStatements.Add(insUsrInfoQuery);
                        writer.WriteLine(insUsrInfoDetQuery + ";" + Environment.NewLine);
                        sqlStatements.Add(insUsrInfoDetQuery);
                        writer.WriteLine(insCDUsr + ";" + Environment.NewLine);
                        sqlStatements.Add(insCDUsr);
                        writer.WriteLine(insCD + ";" + Environment.NewLine);
                        sqlStatements.Add(insCD);
                    }

                    //If we are performing a backup, no need to utilize this query
                    if (!string.IsNullOrEmpty(insUsrRolesQuery))
                    {
                        writer.WriteLine(insUsrRolesQuery + ";" + Environment.NewLine);
                        sqlStatements.Add(insUsrRolesQuery);
                    }
                }
                else
                {
                    //UI_ID, UI_NAME, UI_FNAME, UI_LNAME, UI_STONUM
                    //MERGE_CCSOWNER_USERINFO
                    var insUsrInfoQuery = generateUserInfoInsert(ref usrVo, isBackup);
                    //UID_ID, UID_USID, UID_STONUM, UID_EMNUM
                    //MERGE_CCSOWNER_USERINFODETAIL
                    var insUsrInfoDetQuery = generateUserInfoDetailInsert(ref usrVo, ref userInfoDetId, isBackup);
                    //UR_ID, UR_RID
                    //MERGE_CCSOWNER_USERROLES
                    var insUsrRolesQuery = generateUserRolesInsert(usrVo, isBackup);

                    if (!string.IsNullOrEmpty(insUsrInfoQuery))
                    {
                        writer.WriteLine(insUsrInfoQuery + ";" + Environment.NewLine);
                        sqlStatements.Add(insUsrInfoQuery);
                    }
                    if (!string.IsNullOrEmpty(insUsrInfoDetQuery))
                    {
                        writer.WriteLine(insUsrInfoDetQuery + ";" + Environment.NewLine);
                        sqlStatements.Add(insUsrInfoDetQuery);
                    }
                    if (!string.IsNullOrEmpty(insUsrRolesQuery))
                    {
                        writer.WriteLine(insUsrRolesQuery + ";" + Environment.NewLine);
                        sqlStatements.Add(insUsrRolesQuery);
                    }
                }
            }
            writer.WriteLine("-- END CCSOWNER \\ CDOWNER USER RELATED MERGE STATEMENTS");


            //Generate pawn sec inserts
            var queryList = generatePawnSecInserts(pWriter, isBackup);
            if (CollectionUtilities.isNotEmpty(queryList))
            {
                pwnSecSqlStatements.AddRange(queryList);
            }

            //Close the writer
            writer.Close();
            pWriter.Close();

            //Return the sql statement set
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wkst"></param>
        /// <param name="perMappedList"></param>
        /// <param name="isBackup"></param>
        /// <returns></returns>
        private IEnumerable<string> generateWorkstationPeripheralInsert(ref WorkstationVO wkst, ref List<PairType<string, PeripheralVO>> perMappedList, bool isBackup)
        {
            //WKSP_ID, WKSP_PRID, WKSP_WKID
            //MERGE_CCSOWNER_WORKSTATIONPERIPHERALS
            string[] rt = null;
            if (wkst != null && CollectionUtilities.isNotEmpty(perMappedList))
            {
                rt = new string[perMappedList.Count];
                int idx = 0;
                foreach (var per in perMappedList)
                {
                    if (per == null) continue;
                    string guid = per.Left;
                    var periph = per.Right;
                    if (string.IsNullOrEmpty(guid) || guid.Length < GUID_MIN_LENGTH)
                    {
                        if (isBackup)
                        {
                            //If we are performing a backup, do not put new entries into the database
                            rt[idx] = string.Empty;
                            ++idx;
                            continue;
                        }
                    }

                    var paramArr = new PairType<string, string>[3];
                    paramArr[0] = new PairType<string, string>("WKSP_ID", guid);
                    paramArr[1] = new PairType<string, string>("WKSP_PRID", periph.Id);
                    paramArr[2] = new PairType<string, string>("WKSP_WKID", wkst.Id);

                    //var insWkspMapQuery = DataAccessService.PrepareVariableQuery(
                    //        ref this.dACcs,
                    //        PawnStoreSetupQueries.MERGE_CCSOWNER_WORKSTATIONPERIPHERALS,
                    //        paramArr);

                    var insWkspMapQuery = DataAccessService.PrepareVariableQuery(
                            ref this.dACcs,
                            PawnStoreSetupQueries.MERGE_CCSOWNER_PAWNWORKSTATIONPERIPHERALS,
                            paramArr);

                    if (!string.IsNullOrEmpty(insWkspMapQuery))
                    {
                        rt[idx] = insWkspMapQuery;
                    }
                    else
                    {
                        rt[idx] = string.Empty;
                    }
                    ++idx;
                }
            }

            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cdUsrVo"></param>
        /// <param name="ldapUVo"></param>
        /// <param name="isBackup"></param>
        /// <returns></returns>
        private string generateCashDrawerUserInsert(ref CashDrawerUserVO cdUsrVo, bool isBackup)
        {
            //CDU_ID, CDU_UID, CDU_NAME, CDU_STONUM, CDU_BRID
            //MERGE_CDOWNER_CASHDRAWERUSER
            if (cdUsrVo == null)
            {
                return (string.Empty);
            }

            bool needGuid = false;
            if (string.IsNullOrEmpty(cdUsrVo.Id) || cdUsrVo.Id.Length < GUID_MIN_LENGTH)
            {
                needGuid = true;

                if (isBackup)
                {
                    return (string.Empty);
                }
                cdUsrVo.Id = generateGuid(cdUsrVo.Id);
            }

            var cduParms = new PairType<string, string>[5];
            cduParms[0] = new PairType<string, string>("CDU_ID", cdUsrVo.Id);
            cduParms[1] = new PairType<string, string>("CDU_UID", cdUsrVo.UserId.ToString());
            cduParms[2] = new PairType<string, string>("CDU_NAME", cdUsrVo.UserName.ToLowerInvariant());
            cduParms[3] = new PairType<string, string>("CDU_STONUM", this.storeNumber);
            cduParms[4] = new PairType<string, string>("CDU_BRID", this.storeData.StoreInfo.StoreId);

            var insCDUsrQuery = DataAccessService.PrepareVariableQuery(
                ref this.dACcs,
                PawnStoreSetupQueries.MERGE_CDOWNER_CASHDRAWERUSER, cduParms);

            return (insCDUsrQuery);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cduVo"></param>
        /// <param name="cdVo"></param>
        /// <param name="isBackup"></param>
        /// <returns></returns>
        private string generateCashDrawerInsert(ref CashDrawerUserVO cduVo, ref CashDrawerVO cdVo, bool isBackup)
        {
            //MERGE_CDOWNER_CASHDRAWER            
            //CD_ID, CD_NAME, CD_MGRID, CD_OTYPE, CD_STONUM, CD_BRID
            if (cdVo == null || cduVo == null)
            {
                return (string.Empty);
            }

            bool needGuid = false;
            if (string.IsNullOrEmpty(cdVo.Id) || cdVo.Id.Length < GUID_MIN_LENGTH)
            {
                needGuid = true;

                if (isBackup)
                {
                    return (string.Empty);
                }
            }

            //Create parms
            var cdParms = new PairType<string, string>[6];
            cdParms[0] = new PairType<string, string>("CD_ID", cdVo.Id);
            string objTypeStr = (cdVo.Name.ToUpper().Contains("_SAFE")) ? "RESERVE" : "CASHDRAWER";
            cdParms[1] = new PairType<string, string>("CD_NAME", cdVo.Name.ToLowerInvariant());
            cdParms[2] = new PairType<string, string>("CD_MGRID", cduVo.Id);
            cdParms[3] = new PairType<string, string>("CD_OTYPE", objTypeStr);
            cdParms[4] = new PairType<string, string>("CD_STONUM", this.storeNumber);
            cdParms[5] = new PairType<string, string>("CD_BRID", this.storeData.StoreInfo.StoreId);

            var insCDQuery = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CDOWNER_CASHDRAWER,
                    cdParms);

            return (insCDQuery);
        }

        private string generateUserInfoInsert(ref UserVO uVo, bool isBackup)
        {
            //UI_ID, UI_NAME, UI_FNAME, UI_LNAME, UI_STONUM
            //MERGE_CCSOWNER_USERINFO
            if (uVo == null || uVo.UserRole == null ||
                string.IsNullOrEmpty(uVo.UserID) ||
                uVo.UserID.Equals("0"))
            {
                return (string.Empty);
            }

            //Create parms
            var cdParms = new PairType<string, string>[5];
            cdParms[0] = new PairType<string, string>("UI_ID", uVo.UserID);
            cdParms[1] = new PairType<string, string>("UI_NAME", uVo.UserName.ToLowerInvariant());
            cdParms[2] = new PairType<string, string>("UI_FNAME", uVo.UserFirstName ?? " ");
            cdParms[3] = new PairType<string, string>("UI_LNAME", uVo.UserLastName ?? " ");
            cdParms[4] = new PairType<string, string>("UI_STONUM", uVo.StoreNumber);

            var insUInfQuery = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CCSOWNER_USERINFO,
                    cdParms);

            return (insUInfQuery);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uVo"></param>
        /// <param name="uDetId">UserInfoDetailId, if null, function will generate</param>
        /// <param name="ldapUVo"></param>
        /// <param name="isBackup"></param>
        /// <returns></returns>
        private string generateUserInfoDetailInsert(ref UserVO uVo, ref string uDetId, bool isBackup)
        {
            if (uVo == null || uVo.UserRole == null ||
                string.IsNullOrEmpty(uVo.UserID) ||
                uVo.UserID.Equals("0"))
            {
                return (string.Empty);
            }
            //UID_ID, UID_USID, UID_STONUM, UID_EMNUM
            //MERGE_CCSOWNER_USERINFODETAIL
            bool needGuid = false;
            if (string.IsNullOrEmpty(uDetId) || uDetId.Length < GUID_MIN_LENGTH)
            {
                needGuid = true;

                if (isBackup)
                {
                    return (string.Empty);
                }
            }

            //Create parms
            var cdParms = new PairType<string, string>[4];
            string guidStr;
            cdParms[0] = new PairType<string, string>("UID_ID", uDetId);
            cdParms[1] = new PairType<string, string>("UID_USID", uVo.UserID);
            cdParms[2] = new PairType<string, string>("UID_STONUM", this.storeNumber);
            cdParms[3] = new PairType<string, string>("UID_EMNUM", uVo.EmployeeNumber);

            var uDetQuery = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CCSOWNER_USERINFODETAIL,
                    cdParms);


            return (uDetQuery);
        }

        private string generateUserRolesInsert(UserVO uVo, bool isBackup)
        {
            //UR_ID, UR_RID
            //MERGE_CCSOWNER_USERROLES
            if (uVo == null || uVo.UserRole == null ||
                string.IsNullOrEmpty(uVo.UserID) ||
                uVo.UserID.Equals("0"))
            {
                return (string.Empty);
            }

            var roleParms = new PairType<string, string>[2];
            roleParms[0] = new PairType<string, string>("UR_ID", uVo.UserID);
            roleParms[1] = new PairType<string, string>("UR_RID", uVo.UserRole.RoleId);

            var usrRolesQuery = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CCSOWNER_USERROLES,
                    roleParms);

            return (usrRolesQuery);
        }

        /*private IEnumerable<string> generateWorkstationPeripheralDeletes(WorkstationVO wkst, List<PairType<string, PeripheralVO>> wkstPeripherals)
        {
            if (wkst == null || CollectionUtilities.isEmpty(wkstPeripherals) || wkstPeripherals.Count > 0)
            {
                return (null);
            }

            var rt = new string[wkstPeripherals.Count];
            int idx = 0;

            foreach(var wkPr in wkstPeripherals)
            {
                if (wkPr == null) continue;
                if (wkPr.Left.Length >= GUID_MIN_LENGTH)
                {
                    //WP_ID
                    var parmList = new PairType<string, string>[1];
                    parmList[0] = new PairType<string, string>("WP_ID", wkPr.Left);
                    var delQuery = DataAccessService.PrepareVariableQuery(
                        ref this.dACcs, PawnStoreSetupQueries.DELETE_CCSOWNER_WORKSTATIONPERIPHERALS,
                        parmList);
                    if (!string.IsNullOrEmpty(delQuery))
                    {
                        rt[idx] = delQuery;
                        ++idx;
                    }
                }
            }

            return (rt);
        }*/

        /*private IEnumerable<string> generatePeripheralDeletes(List<PeripheralVO> peripherals)
        {
            if (CollectionUtilities.isEmpty(peripherals))
            {
                return (null);
            }

            var rt = new string[peripherals.Count];
            int idx = 0;

            foreach (var per in peripherals)
            {
                if (per == null)
                    continue;
                if (!string.IsNullOrEmpty(per.Id) && per.Id.Length >= GUID_MIN_LENGTH)
                {
                    //WP_ID
                    var parmList = new PairType<string, string>[1];
                    parmList[0] = new PairType<string, string>("P_ID", per.Id);
                    var delQuery = DataAccessService.PrepareVariableQuery(
                        ref this.dACcs, PawnStoreSetupQueries.DELETE_CCSOWNER_PERIPHERAL,
                        parmList);
                    if (!string.IsNullOrEmpty(delQuery))
                    {
                        rt[idx] = delQuery;
                        ++idx;
                    }
                }
            }

            return (rt);
        }*/

        private string generatePeripheralInsert(ref PeripheralVO per, bool isBackup)
        {
            if (per == null)
                return (string.Empty);
            bool needGuid = false;
            if (string.IsNullOrEmpty(per.Id) || per.Id.Length < GUID_MIN_LENGTH)
            {
                needGuid = true;
                if (isBackup)
                {
                    //If we are performing a backup, do not put new entries into the database
                    return (string.Empty);
                }
            }
            string newGuid;
            //PER_ID, PER_TID, PER_IP, PER_PT, PER_STOID, PER_NAME, PER_MDID
            var paramArr = new PairType<string, string>[7];
            paramArr[0] = new PairType<string, string>("PER_ID", per.Id);
            per.StoreId = this.storeData.StoreInfo.StoreId;
            paramArr[1] = new PairType<string, string>("PER_TID", per.PeriphType.PeripheralTypeId);
            paramArr[2] = new PairType<string, string>("PER_IP", per.IPAddress);
            paramArr[3] = new PairType<string, string>("PER_PT", per.Port);
            paramArr[4] = new PairType<string, string>("PER_STOID", this.storeData.StoreInfo.StoreId);
            paramArr[5] = new PairType<string, string>("PER_NAME", per.Name);
            paramArr[6] = new PairType<string, string>("PER_MDID", per.PeriphType.PeripheralModel.Id);

            var insPerQuery = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CCSOWNER_PERIPHERALS,
                    paramArr);
            return (insPerQuery);
        }

        private string generateWorkstationInsert(ref WorkstationVO wkst, bool isBackup)
        {
            if (wkst == null)
                return (string.Empty);
            bool needGuid = false;
            if (string.IsNullOrEmpty(wkst.Id) || wkst.Id.Length < GUID_MIN_LENGTH)
            {
                needGuid = true;
            }
            string newGuid;
            //CDW_ID, CDW_NAME, CDW_BRID, CDW_STONUM
            var paramArr = new PairType<string, string>[4];
            paramArr[0] = new PairType<string, string>("CDW_ID", wkst.Id);
            paramArr[1] = new PairType<string, string>("CDW_NAME", wkst.Name);
            paramArr[2] = new PairType<string, string>("CDW_BRID", wkst.StoreId);
            paramArr[3] = new PairType<string, string>("CDW_STONUM", this.storeNumber);

            var insWkstQuery = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CDOWNER_WORKSTATION,
                    paramArr);

            //public const string MERGE_CDOWNER_WORKSTATION =)
            return (insWkstQuery);
        }

        /// <summary>
        /// Generate pawn security insert statements
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="isBackup"></param>
        /// <returns></returns>
        private List<string> generatePawnSecInserts(StreamWriter writer, bool isBackup)
        {
            writer.WriteLine("-- PAWN SEC MERGE STATEMENTS");
            var p = this.storeData.PawnSecData;

            if (p == null)
            {
                MessageBox.Show("Could not generate pawn sec merge statements.  Please setup all Pawn Sec data", SETUPALERT_TXT);
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO PAWNSEC DATA)");
                return (null);
            }

            if (CollectionUtilities.isEmpty(p.ClientMachines))
            {
                MessageBox.Show("Could not generate pawn sec merge statements.  Please setup client machines in Pawn Sec", SETUPALERT_TXT);
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO CLIENT MACHINES)");
                return (null);
            }

            var pStore = p.GetStore();
            if (pStore == null)
            {
                MessageBox.Show("Could not generate pawn sec merge statements.  Please setup store in Pawn Sec", SETUPALERT_TXT);
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO STORE INFO / CONFIG)");
                return (null);
            }

            var appVer = pStore.AppVersion;
            if (appVer == null)
            {
                MessageBox.Show("Could not generate pawn sec merge statements.  Please set an app version in Pawn Sec", SETUPALERT_TXT);
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO APP VERSION)");
                return (null);
            }

            var sConfig = pStore.StoreConfiguration;
            if (sConfig == null)
            {
                MessageBox.Show("Could not generate pawn sec merge statements.  Please setup store config in Pawn Sec", SETUPALERT_TXT);
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO STORE CONFIG)");
                return (null);
            }
            var storeSite = pStore.StoreSite;
            if (storeSite == null)
            {
                MessageBox.Show("Could not generate pawn sec merge statements.  Please setup store site in Pawn Sec", SETUPALERT_TXT);
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO STORE SITE INFO)");
                return (null);
            }

            var gConfig = p.GlobalConfiguration;
            if (gConfig == null)
            {
                MessageBox.Show("Could not generate pawn sec merge statements.  Please setup global config in Pawn Sec", SETUPALERT_TXT);
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO GLOBAL CONFIG)");
                return (null);
            }

            var eCfg = this.eConfig;
            if (eCfg == null)
            {
                MessageBox.Show("Could not generate pawn sec merge statements.  Please setup encryption data", SETUPALERT_TXT);
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO ENCRYPTED CONFIG CONTAINER)");
                return (null);
            }



            var rt = new List<string>(11);
            PairType<string, string>[] parms;
            string query;

            //Client machine merges
            foreach (var clientMac in p.ClientMachines)
            {
                if (clientMac == null) continue;
                if (clientMac.Machine.ClientId == 0 && !isBackup)
                {
                    ulong nexId = 0;
                    string temp;
                    this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.CLIREG_ID,
                                                                    ref nexId,
                                                                    out temp);
                    clientMac.Machine.ClientId = nexId;
                }
                if (clientMac.Machine.ClientId == 0)
                    continue;
                parms = new PairType<string, string>[8];
                parms[0] = new PairType<string, string>("CR_ID1", clientMac.Machine.ClientId.ToString());
                parms[1] = new PairType<string, string>("CR_IPADDRESS", this.eConfig.EncryptValue(clientMac.Machine.IPAddress));
                parms[2] = new PairType<string, string>("CR_MACADDRESS", this.eConfig.EncryptValue(clientMac.Machine.MACAddress));
                parms[3] = new PairType<string, string>("CR_ISALLOWED", clientMac.Machine.IsAllowed ? "1" : "0");
                parms[4] = new PairType<string, string>("CR_ISCONNECTED", "0");
                parms[5] = new PairType<string, string>("CR_ADOBEOVERRIDE", clientMac.Machine.AdobeOverride.EscapePath());
                parms[6] = new PairType<string, string>("CR_GHOSTSCRIPTOVERRIDE", clientMac.Machine.GhostOverride.EscapePath());
                parms[7] = new PairType<string, string>("CR_MACHINENAME", clientMac.Machine.MachineName);

                query = DataAccessService.PrepareVariableQuery(
                        ref this.dAPawnSec,
                        PawnStoreSetupQueries.MERGE_PAWNSEC_CLIENTREGISTRY,
                        parms);
                if (!string.IsNullOrEmpty(query))
                {
                    writer.WriteLine(query + ";" + Environment.NewLine);
                    rt.Add(query);
                }
            }

            //Store app version merge
            parms = new PairType<string, string>[3];
            if (appVer.AppVersionId.Equals("0") && !isBackup)
            {
                ulong nexId = 1;
                string temp;
                this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.STOAPPVER_ID,
                                                                ref nexId,
                                                                out temp);
                appVer.AppVersionId = nexId.ToString();
            }

            if (!appVer.AppVersionId.Equals("0"))
            {
                parms[0] = new PairType<string, string>("SAV_ID1", appVer.AppVersionId);
                parms[1] = new PairType<string, string>("SAV_APPVERSION", appVer.AppVersion);
                parms[2] = new PairType<string, string>("SAV_APPVERSIONDESC", appVer.Description);
                query = DataAccessService.PrepareVariableQuery(
                        ref this.dAPawnSec,
                        PawnStoreSetupQueries.MERGE_PAWNSEC_STOREAPPVERSION,
                        parms);
                if (!string.IsNullOrEmpty(query))
                {
                    writer.WriteLine(query + ";" + Environment.NewLine);
                    rt.Add(query);
                }
            }

            //Store site info merge
            if (pStore.StoreSiteId == 0 && !isBackup)
            {
                ulong nexId = 1;
                string temp;
                this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.STOSITINF_ID,
                                                                ref nexId,
                                                                out temp);
                pStore.StoreSiteId = nexId;
            }

            if (pStore.StoreSiteId > 0)
            {
                parms = new PairType<string, string>[7];
                parms[0] = new PairType<string, string>("SSI_ID1", pStore.StoreSiteId.ToString());
                parms[1] = new PairType<string, string>("SSI_STORENUMBER", storeSite.StoreNumber);
                parms[2] = new PairType<string, string>("SSI_STATE", storeSite.State);
                parms[3] = new PairType<string, string>("SSI_COMPANYNUMBER", storeSite.CompanyNumber);
                parms[4] = new PairType<string, string>("SSI_ALIAS", storeSite.Alias);
                parms[5] = new PairType<string, string>("SAV_ID1", appVer.AppVersionId);
                parms[6] = new PairType<string, string>("SSI_COMPANYNAME", storeSite.Company);
                query = DataAccessService.PrepareVariableQuery(
                        ref this.dAPawnSec,
                        PawnStoreSetupQueries.MERGE_PAWNSEC_STORESITEINFO,
                        parms);
                if (!string.IsNullOrEmpty(query))
                {
                    writer.WriteLine(query + ";" + Environment.NewLine);
                    rt.Add(query);
                }
            }

            //Store config merge
            if (sConfig.Id == 0 && !isBackup)
            {
                ulong nexId = 1;
                string temp;
                this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.STOCFG_ID,
                                                                ref nexId,
                                                                out temp);
                sConfig.Id = nexId;
            }

            if (sConfig.Id > 0)
            {
                parms = new PairType<string, string>[12];
                parms[0] = new PairType<string, string>("SC_ID1", sConfig.Id.ToString());
                parms[1] = new PairType<string, string>("SC_METALSFILE", sConfig.MetalsFile);
                parms[2] = new PairType<string, string>("SC_STONESFILE", sConfig.StonesFile);
                parms[3] = new PairType<string, string>("SC_TIMEZONE", sConfig.TimeZone);
                parms[4] = new PairType<string, string>("SC_FETCHSZMX", sConfig.FetchSizeMultiplier.ToString());
                parms[5] = new PairType<string, string>("SC_MILLIOFF", sConfig.MillisecondOffset.ToString());
                parms[6] = new PairType<string, string>("SC_SECONDOFF", sConfig.SecondOffset.ToString());
                parms[7] = new PairType<string, string>("SC_MINUTEOFF", sConfig.MinuteOffset.ToString());
                parms[8] = new PairType<string, string>("SC_HOUROFF", sConfig.HourOffset.ToString());
                parms[9] = new PairType<string, string>("SC_DAYOFF", sConfig.DayOffset.ToString());
                parms[10] = new PairType<string, string>("SC_MONTHOFF", sConfig.MonthOffset.ToString());
                parms[11] = new PairType<string, string>("SC_YEAROFF", sConfig.YearOffset.ToString());
                query = DataAccessService.PrepareVariableQuery(
                        ref this.dAPawnSec,
                        PawnStoreSetupQueries.MERGE_PAWNSEC_STORECONFIG,
                        parms);
                if (!string.IsNullOrEmpty(query))
                {
                    writer.WriteLine(query + ";" + Environment.NewLine);
                    rt.Add(query);
                }
            }

            //Store client config merges
            foreach (var sC in p.ClientMachines)
            {
                if (sC == null) continue;
                var sCCfg = sC.StoreMachine;
                parms = new PairType<string, string>[6];
                if (sCCfg.Id == 0 && !isBackup)
                {
                    ulong nexId = 0;
                    string temp;
                    this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.STOCLICFG_ID,
                                                                    ref nexId,
                                                                    out temp);
                    sCCfg.Id = nexId;
                }
                if (sCCfg.Id == 0)
                    continue;
                parms[0] = new PairType<string, string>("SCC_ID1", sCCfg.Id.ToString());
                parms[1] = new PairType<string, string>("SCC_WORKSTATIONID", sCCfg.WorkstationId);
                parms[2] = new PairType<string, string>("SCC_TERMINALNUMBER", sCCfg.TerminalNumber.ToString());
                parms[3] = new PairType<string, string>("SCC_LOGLEVEL", sCCfg.LogLevel);
                parms[4] = new PairType<string, string>("SCC_TRACELEVEL", sCCfg.TraceLevel.ToString());
                parms[5] = new PairType<string, string>("SCC_PRINTENABLED", sCCfg.PrintEnabled ? "1" : "0");
                query = DataAccessService.PrepareVariableQuery(
                        ref this.dAPawnSec,
                        PawnStoreSetupQueries.MERGE_PAWNSEC_STORECLIENTCONFIG,
                        parms);
                if (!string.IsNullOrEmpty(query))
                {
                    writer.WriteLine(query + ";" + Environment.NewLine);
                    rt.Add(query);
                }
            }

            //Global config merge
            parms = new PairType<string, string>[8];
            parms[0] = new PairType<string, string>("GC_ID1", "1");
            parms[1] = new PairType<string, string>("GC_VERSION", appVer.AppVersionId);
            parms[2] = new PairType<string, string>("GC_BASETEMPLATEPATH", gConfig.BaseTemplatePath.EscapePath());
            parms[3] = new PairType<string, string>("GC_BASELOGPATH", gConfig.BaseLogPath.EscapePath());
            parms[4] = new PairType<string, string>("GC_BASEMEDIAPATH", gConfig.BaseMediaPath.EscapePath());
            parms[5] = new PairType<string, string>("GC_ADOBEREADERPATH", gConfig.AdobeReaderPath.EscapePath());
            parms[6] = new PairType<string, string>("GC_GHOSTSCRIPTPATH", gConfig.GhostScriptPath.EscapePath());
            parms[7] = new PairType<string, string>("GC_PUBLICKEY", this.eConfig.EncryptPublicValue(gConfig.DataPublicKey));
            query = DataAccessService.PrepareVariableQuery(
                    ref this.dAPawnSec,
                    PawnStoreSetupQueries.MERGE_PAWNSEC_GLOBALCONFIG,
                    parms);
            if (!string.IsNullOrEmpty(query))
            {
                writer.WriteLine(query + ";" + Environment.NewLine);
                rt.Add(query);
            }

            //DB list vars
            var dbList = p.DatabaseServiceList;
            var dbMapList = p.DatabaseServiceMapList;

            //Lookup the Oracle data service VO
            DatabaseServiceVO oraService = null;
            if (CollectionUtilities.isNotEmpty(dbList))
            {
                oraService = dbList.Find(x => x.ServiceType == "ORACLE");
            }

            if (oraService == null && !isBackup)
            {
                //If not found, ensure current data is put in
                oraService = new DatabaseServiceVO(
                    eCfg.EncryptValue(this.dbUserName),
                    eCfg.EncryptValue(this.dbPassword), true);
                ulong uNextOraId = 0;
                //p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERV_ID, ref uNextOraId);
                string temp;
                p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERV_ID, ref uNextOraId, out temp);
                oraService.Id = uNextOraId;
                oraService.Name = eCfg.EncryptValue(this.dbServiceName);
                oraService.Server = eCfg.EncryptValue(this.dbHostName);
                oraService.Port = eCfg.EncryptValue(this.dbPort);
                oraService.Schema = eCfg.EncryptValue(this.dbSchemaName);
                oraService.AuxInfo = eCfg.EncryptValue(this.dbServiceName);
                oraService.ServiceType = "ORACLE";

                //NOTE: Add to db list
                dbList.Add(oraService);

                //Ensure mapping entry is inserted
                var oraServiceMapEntry = new PawnSecVO.DatabaseServiceStoreMapVO();
                oraServiceMapEntry.DatabaseServiceId = oraService.Id;
                oraServiceMapEntry.StoreConfigId = sConfig.Id;
                ulong uNextOraMapId = 0;
                p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERVMAP_ID, ref uNextOraMapId, out temp);
                oraServiceMapEntry.Id = uNextOraMapId;

                //Add to map
                dbMapList.Add(oraServiceMapEntry);

                //Regenerate pawn sec map
                //p.GenerateMaps();
            }

            //Oracle query generation
            parms = new PairType<string, string>[9];
            if (oraService != null && !isBackup && oraService.Id == 0)
            {
                ulong uNextOraId = 0;
                string temp;
                p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERV_ID, ref uNextOraId, out temp);
                oraService.Id = uNextOraId;
                //NOTE: Do not add to the db list!!!

                //Ensure mapping entry is inserted
                var oraServiceMapEntry = new PawnSecVO.DatabaseServiceStoreMapVO();
                oraServiceMapEntry.DatabaseServiceId = oraService.Id;
                oraServiceMapEntry.StoreConfigId = sConfig.Id;
                ulong uNextOraMapId = 0;
                p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERVMAP_ID, ref uNextOraMapId, out temp);
                oraServiceMapEntry.Id = uNextOraMapId;

                //Add to map
                dbMapList.Add(oraServiceMapEntry);
            }

            if (oraService != null && oraService.Id > 0)
            {
                parms[0] = new PairType<string, string>("DS_ORACLEID1", oraService.Id.ToString());
                parms[1] = new PairType<string, string>("DS_ORACLENAME1", oraService.Name);
                parms[2] = new PairType<string, string>("DS_ORACLESERVER1", oraService.Server);
                parms[3] = new PairType<string, string>("DS_ORACLEPORT1", oraService.Port);
                parms[4] = new PairType<string, string>("DS_ORACLESCHEMA1", oraService.Schema);
                parms[5] = new PairType<string, string>("DS_ORACLEUSER1", oraService.DbUser);
                parms[6] = new PairType<string, string>("DS_ORACLEPWD1", oraService.DbUserPwd);
                parms[7] = new PairType<string, string>("DS_ORACLEAUXINFO1", oraService.AuxInfo);
                parms[8] = new PairType<string, string>("DS_ORACLEENABLED1", oraService.Enabled ? "1" : "0");
                query = DataAccessService.PrepareVariableQuery(
                        ref this.dAPawnSec,
                        PawnStoreSetupQueries.MERGE_PAWNSEC_DATABASESERVICE_ORACLE,
                        parms);
                if (!string.IsNullOrEmpty(query))
                {
                    writer.WriteLine(query + ";" + Environment.NewLine);
                    rt.Add(query);
                }
            }


            DatabaseServiceVO ldapServiceVar = null;
            if (CollectionUtilities.isNotEmpty(dbList))
            {
                ldapServiceVar = dbList.Find(x => x.ServiceType == "LDAP");
            }

            if (CollectionUtilities.isEmpty(this.ldapData) && ldapServiceVar == null && !isBackup)
            {
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (LDAP)");
                MessageBox.Show("You must configure the LDAP server data to get a complete SQL set for PAWNSEC");
                return (rt);
            }

            if (!isBackup && !eConfig.IsLDAPExistent(this.ldapData[LDAP_SERVERKEY], this.ldapData[LDAP_PORTKEY]))
            {
                if (CollectionUtilities.isNotEmptyContainsKey(this.ldapData, LDAP_LOGINKEY))
                {
                    var loginDN = this.ldapData[LDAP_LOGINKEY];
                    var userPwd = this.ldapData[LDAP_PWDKEY];
                    var pwdPolicyCN = this.ldapData[LDAP_PWDPOLICYKEY];
                    var searchDN = this.ldapData[LDAP_USERDNKEY];
                    var userIdKey = this.ldapData[LDAP_USERIDKEY];
                    string userName = loginDN;
                    if (userName.StartsWith("cn=") && userName.Length > 4)
                    {
                        userName = loginDN.Substring(3);
                    }
                    var ldapPort = this.ldapData[LDAP_PORTKEY];
                    var ldapServer = this.ldapData[LDAP_SERVERKEY];
                    ldapServiceVar = new DatabaseServiceVO(string.Empty, string.Empty, true);

                    ulong nextLdapId = 0;
                    string temp;
                    p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERV_ID, ref nextLdapId, out temp);

                    ldapServiceVar.Id = nextLdapId;
                    ldapServiceVar.Server = eConfig.EncryptValue(ldapServer);
                    ldapServiceVar.Port = eConfig.EncryptValue(ldapPort);
                    ldapServiceVar.AuxInfo =
                            eConfig.EncryptValue(
                                EncryptedConfigContainer.ComputeUnEncryptedAuxInfo(
                                    loginDN,
                                    searchDN,
                                    userIdKey,
                                    userPwd,
                                    pwdPolicyCN));
                    ldapServiceVar.Name = eConfig.EncryptValue(EncryptedConfigContainer.LDAPKEY.ToLower());

                    //NOTE: Add to the db list
                    dbList.Add(ldapServiceVar);

                    //Create a map entry
                    var ldapServiceMapEntry = new PawnSecVO.DatabaseServiceStoreMapVO();
                    ulong nextLdapMapId = 0;
                    p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERVMAP_ID, ref nextLdapMapId, out temp);
                    ldapServiceMapEntry.Id = nextLdapMapId;
                    ldapServiceMapEntry.DatabaseServiceId = ldapServiceVar.Id;
                    ldapServiceMapEntry.StoreConfigId = sConfig.Id;

                    //Add the entry
                    dbMapList.Add(ldapServiceMapEntry);

                    //Generate the maps
                    //p.GenerateMaps();
                }
                else
                {
                    writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (LDAP)");
                    MessageBox.Show("You must configure the LDAP server data to get a complete SQL set for PAWNSEC");
                    return (rt);
                }
            }

            if (ldapServiceVar != null && !isBackup && ldapServiceVar.Id == 0)
            {
                ulong uNextLdapId = 0;
                string temp;
                p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERV_ID, ref uNextLdapId, out temp);
                ldapServiceVar.Id = uNextLdapId;
                //NOTE: Do not add to db list!

                //Create a map entry
                var ldapServiceMapEntry = new PawnSecVO.DatabaseServiceStoreMapVO();
                ulong nextLdapMapId = 0;
                p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERVMAP_ID, ref nextLdapMapId, out temp);
                ldapServiceMapEntry.Id = nextLdapMapId;
                ldapServiceMapEntry.DatabaseServiceId = ldapServiceVar.Id;
                ldapServiceMapEntry.StoreConfigId = sConfig.Id;

                //Add the entry
                dbMapList.Add(ldapServiceMapEntry);
            }

            if (ldapServiceVar != null)
            {
                parms = new PairType<string, string>[9];
                parms[0] = new PairType<string, string>("DS_LDAPID1", ldapServiceVar.Id.ToString());
                parms[1] = new PairType<string, string>("DS_LDAPNAME1", ldapServiceVar.Name);
                parms[2] = new PairType<string, string>("DS_LDAPSERVER1", ldapServiceVar.Server);
                parms[3] = new PairType<string, string>("DS_LDAPPORT1", ldapServiceVar.Port);
                parms[4] = new PairType<string, string>("DS_LDAPUSER1", ldapServiceVar.DbUser);
                parms[5] = new PairType<string, string>("DS_LDAPPWD1", ldapServiceVar.DbUserPwd);
                parms[6] = new PairType<string, string>("DS_LDAPAUXINFO1", ldapServiceVar.AuxInfo);
                parms[7] = new PairType<string, string>("DS_LDAPSCHEMA1", ldapServiceVar.Schema);
                parms[8] = new PairType<string, string>("DS_LDAPENABLED1", ldapServiceVar.Enabled ? "1" : "0");

                query = DataAccessService.PrepareVariableQuery(
                        ref this.dAPawnSec,
                        PawnStoreSetupQueries.MERGE_PAWNSEC_DATABASESERVICE_LDAP,
                        parms);
                if (!string.IsNullOrEmpty(query))
                {
                    writer.WriteLine(query + ";" + Environment.NewLine);
                    rt.Add(query);
                }
            }

            DatabaseServiceVO couchServiceVar = null;
            if (CollectionUtilities.isNotEmpty(dbList))
            {
                couchServiceVar = dbList.Find(x => x.ServiceType == "COUCHDB");
            }

            if (couchServiceVar == null && !isBackup)
            {
                if (this.couchDBService != null)
                {
                    //Create couch db entry
                    couchServiceVar = new DatabaseServiceVO(string.Empty, string.Empty, true);
                    couchServiceVar.Server = eCfg.EncryptValue(this.couchDBService.Server);
                    couchServiceVar.Port = eCfg.EncryptValue(this.couchDBService.Port);
                    couchServiceVar.Schema = eCfg.EncryptValue(this.couchDBService.Schema);
                    couchServiceVar.Name = eCfg.EncryptValue(EncryptedConfigContainer.COUCHDBKEY.ToLower());

                    //Get the next id available
                    ulong nextDbId = 0;
                    string temp;
                    p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERV_ID, ref nextDbId, out temp);
                    couchServiceVar.Id = nextDbId;

                    //NOTE: Add it to the db list!
                    dbList.Add(couchServiceVar);

                    //Create a mapping entry
                    var couchMapEntry = new PawnSecVO.DatabaseServiceStoreMapVO();
                    ulong nextDbMapId = 0;
                    p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERVMAP_ID, ref nextDbMapId, out temp);
                    couchMapEntry.Id = nextDbMapId;
                    couchMapEntry.DatabaseServiceId = couchServiceVar.Id;
                    couchMapEntry.StoreConfigId = sConfig.Id;

                    //Add it to the list
                    dbMapList.Add(couchMapEntry);

                    //Regenerate the maps
                    //p.GenerateMaps();
                }
                else
                {
                    writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (COUCHDB)");
                    MessageBox.Show("You must configure the Couch DB server data to get a complete SQL set for PAWNSEC");
                    return (rt);
                }
            }

            if (couchServiceVar != null && !isBackup && couchServiceVar.Id == 0)
            {
                //Get the next id available
                ulong nextDbId = 0;
                string temp;
                p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERV_ID, ref nextDbId, out temp);
                couchServiceVar.Id = nextDbId;
                //NOTE: Do not add to the db list!

                //Create a mapping entry
                var couchMapEntry = new PawnSecVO.DatabaseServiceStoreMapVO();
                ulong nextDbMapId = 0;
                p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERVMAP_ID, ref nextDbMapId, out temp);
                couchMapEntry.Id = nextDbMapId;
                couchMapEntry.DatabaseServiceId = couchServiceVar.Id;
                couchMapEntry.StoreConfigId = sConfig.Id;

                //Add it to the list
                dbMapList.Add(couchMapEntry);
            }

            if (couchServiceVar != null)
            {
                parms = new PairType<string, string>[9];
                parms[0] = new PairType<string, string>("DS_COUCHDBID1", couchServiceVar.Id.ToString());
                parms[1] = new PairType<string, string>("DS_COUCHDBNAME1", couchServiceVar.Name);
                parms[2] = new PairType<string, string>("DS_COUCHDBSERVER1", couchServiceVar.Server);
                parms[3] = new PairType<string, string>("DS_COUCHDBPORT1", couchServiceVar.Port);
                parms[4] = new PairType<string, string>("DS_COUCHDBUSER1", couchServiceVar.DbUser);
                parms[5] = new PairType<string, string>("DS_COUCHDBPWD1", couchServiceVar.DbUserPwd);
                parms[6] = new PairType<string, string>("DS_COUCHDBAUXINFO1", couchServiceVar.AuxInfo);
                parms[7] = new PairType<string, string>("DS_COUCHDBSCHEMA1", couchServiceVar.Schema);
                parms[8] = new PairType<string, string>("DS_COUCHDBENABLED1", couchServiceVar.Enabled ? "1" : "0");

                query = DataAccessService.PrepareVariableQuery(
                        ref this.dAPawnSec,
                        PawnStoreSetupQueries.MERGE_PAWNSEC_DATABASESERVICE_COUCHDB,
                        parms);
                if (!string.IsNullOrEmpty(query))
                {
                    writer.WriteLine(query + ";" + Environment.NewLine);
                    rt.Add(query);
                }
            }

            if (CollectionUtilities.isEmpty(dbMapList))
            {
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO DATABASE LIST AND/OR DATABASE SERVICE MAP)");
                return (rt);
            }

            for (int i = 0; i < dbMapList.Count; ++i)
            {
                var dMap = dbMapList[i];
                if (dMap == null)
                    continue;

                parms = new PairType<string, string>[3];
                if (dMap.Id == 0 && !isBackup)
                {
                    ulong nextDMapId = 0;
                    string temp;
                    p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.DATSERVMAP_ID, ref nextDMapId, out temp);
                    dMap.Id = nextDMapId;
                }

                if (dMap.Id > 0)
                {
                    parms[0] = new PairType<string, string>("DSSM_ID1", dMap.Id.ToString());
                    parms[1] = new PairType<string, string>("DS_ID1", dMap.DatabaseServiceId.ToString());
                    parms[2] = new PairType<string, string>("SC_ID1", dMap.StoreConfigId.ToString());

                    query = DataAccessService.PrepareVariableQuery(
                            ref this.dAPawnSec,
                            PawnStoreSetupQueries.MERGE_PAWNSEC_DATABASESERVICESTOREMAP,
                            parms);

                    if (!string.IsNullOrEmpty(query))
                    {
                        writer.WriteLine(query + ";" + Environment.NewLine);
                        rt.Add(query);
                    }
                }
            }

            var esbList = p.ESBServiceList;
            var esbMapList = p.ESBServiceMapList;
            if (CollectionUtilities.isEmpty(esbList))
            {
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO ESB SERVICES)");
                return (rt);
            }

            for (int i = 0; i < esbList.Count; ++i)
            {
                var esbServ = esbList[i];
                if (esbServ == null)
                    continue;

                parms = new PairType<string, string>[9];
                if (esbServ.Id == 0 && !isBackup)
                {
                    ulong nextEsbId = 0;
                    string temp;
                    p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.ESBSERV_ID, ref nextEsbId, out temp);
                    esbServ.Id = nextEsbId;
                    //NOTE: Do not add to esb list!

                    ulong nextEsbMapId = 0;
                    p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.ESBSERVMAP_ID, ref nextEsbMapId, out temp);
                    var esbServMapVo = new PawnSecVO.ESBServiceStoreMapVO();
                    esbServMapVo.Id = nextEsbMapId;
                    esbServMapVo.StoreConfigId = sConfig.Id;
                    esbServMapVo.ESBServiceId = nextEsbId;
                    //NOTE: Add to esb map id list
                    esbMapList.Add(esbServMapVo);
                }

                if (esbServ.Id > 0)
                {
                    parms[0] = new PairType<string, string>("ES_ID1", esbServ.Id.ToString());
                    parms[1] = new PairType<string, string>("ES_NAME1", esbServ.Name);
                    parms[2] = new PairType<string, string>("ES_SERVER1", esbServ.Server);
                    parms[3] = new PairType<string, string>("ES_PORT1", esbServ.Port);
                    parms[4] = new PairType<string, string>("ES_DOMAIN1", esbServ.Domain);
                    parms[5] = new PairType<string, string>("ES_URI1", esbServ.Uri);
                    parms[6] = new PairType<string, string>("ES_ENDPOINT1", esbServ.EndPointName);
                    parms[7] = new PairType<string, string>("ES_CLIENTBINDING1", esbServ.ClientBinding);
                    parms[8] = new PairType<string, string>("ES_HTTPBINDING1", esbServ.HttpBinding);


                    query = DataAccessService.PrepareVariableQuery(
                            ref this.dAPawnSec,
                            PawnStoreSetupQueries.MERGE_PAWNSEC_ESBSERVICE,
                            parms);
                    if (!string.IsNullOrEmpty(query))
                    {
                        writer.WriteLine(query + ";" + Environment.NewLine);
                        rt.Add(query);
                    }
                }
            }

            if (CollectionUtilities.isEmpty(esbMapList))
            {
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO ESB MAP ENTRIES)");
                return (rt);
            }

            for (int i = 0; i < esbMapList.Count; ++i)
            {
                var eMap = esbMapList[i];
                if (eMap == null)
                    continue;

                if (eMap.Id == 0 && !isBackup)
                {
                    ulong nextEsbMapId = 0;
                    string temp;
                    p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.ESBSERVMAP_ID, ref nextEsbMapId, out temp);
                    var esbServMapVo = new PawnSecVO.ESBServiceStoreMapVO();
                    esbServMapVo.Id = nextEsbMapId;
                    //NOTE: do not add to esb map list
                }

                if (eMap.Id > 0)
                {
                    parms = new PairType<string, string>[3];
                    parms[0] = new PairType<string, string>("ES_ID1", eMap.ESBServiceId.ToString());
                    parms[1] = new PairType<string, string>("SC_ID1", eMap.StoreConfigId.ToString());
                    parms[2] = new PairType<string, string>("ESSM_ID1", eMap.Id.ToString());

                    query = DataAccessService.PrepareVariableQuery(
                            ref this.dAPawnSec,
                            PawnStoreSetupQueries.MERGE_PAWNSEC_ESBSERVICESERVICESTOREMAP,
                            parms);

                    if (!string.IsNullOrEmpty(query))
                    {
                        writer.WriteLine(query + ";" + Environment.NewLine);
                        rt.Add(query);
                    }
                }
            }

            if (CollectionUtilities.isEmpty(p.ClientMachines))
            {
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO CLIENT MACHINES)");
                return (rt);
            }

            //CSM_ID1, CR_ID1, SSI_ID1, SCC_ID1, SC_ID1
            //public const string MERGE_PAWNSEC_CLIENTSTOREMAP =
            //p.GenerateMaps();
            //var clientMapList = p.ClientStoreMapList;
            //p.ClientStoreMapList.Clear();
            //Populate client map list
            for (int i = 0; i < p.ClientMachines.Count; ++i)
            {
                var client = p.ClientMachines[i];
                if (client == null)
                {
                    continue;
                }

                var sCCfg = client.StoreMachine;

                var curCliMap = p.ClientStoreMapList.Find(x => x.ClientRegistryId == client.Machine.ClientId);
                if (curCliMap == null && !isBackup)
                {
                    curCliMap = new PawnSecVO.ClientStoreMapVO();
                    ulong nexId = 0;
                    string temp;
                    p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.CLISTOMAP_ID, ref nexId, out temp);

                    curCliMap.Id = nexId;
                    curCliMap.StoreClientConfigId = sCCfg.Id;
                    curCliMap.StoreConfigId = sConfig.Id;
                    curCliMap.StoreNumber = p.StoreNumber;
                    curCliMap.StoreSiteId = pStore.StoreSiteId;
                    curCliMap.ClientRegistryId = client.Machine.ClientId;

                    //NOTE: Add to client map list!
                    p.ClientStoreMapList.Add(curCliMap);
                }
            }

            if (CollectionUtilities.isEmpty(p.ClientStoreMapList))
            {
                writer.WriteLine("-- BAILED EARLY ON PAWN SEC MERGE STATEMENTS (NO CLIENT TO STORE MAP)");
                return (rt);
            }


            p.ClientStoreMapList.RemoveAll(x => x.ClientRegistryId == 0);

            for (int i = 0; i < p.ClientStoreMapList.Count; ++i)
            {
                var cEntry = p.ClientStoreMapList[i];
                if (cEntry == null)
                    continue;

                if (cEntry.Id == 0 && !isBackup)
                {
                    ulong nexId = 0;
                    string temp;
                    p.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.CLISTOMAP_ID, ref nexId, out temp);
                    cEntry.Id = nexId;
                    //NOTE: Do not add to client map list!
                }

                if (cEntry.Id > 0)
                {
                    parms = new PairType<string, string>[5];
                    parms[0] = new PairType<string, string>("CSM_ID1", cEntry.Id.ToString());
                    parms[1] = new PairType<string, string>("CR_ID1", cEntry.ClientRegistryId.ToString());
                    parms[2] = new PairType<string, string>("SSI_ID1", cEntry.StoreSiteId.ToString());
                    parms[3] = new PairType<string, string>("SCC_ID1", cEntry.StoreClientConfigId.ToString());
                    parms[4] = new PairType<string, string>("SC_ID1", cEntry.StoreConfigId.ToString());

                    query = DataAccessService.PrepareVariableQuery(
                            ref this.dAPawnSec,
                            PawnStoreSetupQueries.MERGE_PAWNSEC_CLIENTSTOREMAP,
                            parms);
                    if (!string.IsNullOrEmpty(query))
                    {
                        writer.WriteLine(query + ";" + Environment.NewLine);
                        rt.Add(query);
                    }
                }
            }


            writer.WriteLine("-- END PAWN SEC MERGE STATEMENTS");
            return (rt);
        }

        private void BackupButton_Click(object sender, EventArgs e)
        {
            var progFm = new InProgressForm(@"* Backup In Progress *");
            DateTime now = DateTime.Now;

            string fileName = @"c:\backup_" + now.ToShortDateString().Replace('/', '_') + "__" + now.Ticks + @".sql";
            string pwnSecFileName = @"c:\pwnSecBackup_" + now.ToShortDateString().Replace('/', '_') + "__" + now.Ticks + @".sql";
            List<string> sql;
            List<string> pwnSql;
            //Specify that this is a backup
            this.generateQueries(fileName, pwnSecFileName, true, out sql, out pwnSql);
            progFm.Dispose();
            MessageBox.Show("Backup merge SQL statements saved in the following files: " + fileName + Environment.NewLine + " and " + pwnSecFileName);
        }


        private void BatchImport_Click(object sender, EventArgs e)
        {
            /*  if (this.dACcs == null ||
                  this.dACcs.OracleDA == null ||
                  this.dACcs.OracleDA.GetConnectionStatus(CCSOWNER) != OracleDataAccessor.Status.CONNECTED ||
                  this.dAPawnSec == null ||
                  this.dAPawnSec.OracleDA == null ||
                  this.dAPawnSec.OracleDA.GetConnectionStatus(PAWNSEC) != OracleDataAccessor.Status.CONNECTED)
              {
                  if (!(((infoFlag & (uint)REQINFOFLAGS.DBHOSTNAME) > 0) &&
                        ((infoFlag & (uint)REQINFOFLAGS.DBPASSWORD) > 0) &&
                        ((infoFlag & (uint)REQINFOFLAGS.DBPORT) > 0) &&
                        ((infoFlag & (uint)REQINFOFLAGS.DBSCHEMA) > 0) &&
                        ((infoFlag & (uint)REQINFOFLAGS.DBUSERNAME) > 0) &&
                        ((infoFlag & (uint)REQINFOFLAGS.DBPASSWORD) > 0) &&
                        (infoFlag & (uint)REQINFOFLAGS.PWNSECPWD) > 0))
                  {
                      MessageBox.Show("Please enter all required data for database connection.",
                                      "PawnStoreSetup Alert",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation);
                      return;
                  }

                  if (!setupDatabaseConnections(this.pawnsecPublicSeedTextBox.Text, true))
                  {
                      return;
                  }
              }
              else
              {
                  writeMessage("Database connections are valid.");
              } */

            //ldapSetupButton_Click(sender, e);
            importInputFile();
        }

        private void importInputFile()
        {
            //Show open file dialog
            var openFileDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                InitialDirectory = @"c:\",
                FileName = "ImportFile.p2a",
                Filter = "p2a files|*.p2a|All files (*.*)|*.*",
                FilterIndex = 1,
                Multiselect = false
            };

            DialogResult res = openFileDialog.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                ldapSetupButton_Click(null, null);
                try
                {
                    newUserId = 0;
                    string batchImportFileName = openFileDialog.FileName;
                    //Open the file for reading
                    FileStream fStream = new FileStream(batchImportFileName, FileMode.Open, FileAccess.Read);
                    var sReader = new StreamReader(fStream);

                    string line = null, storeNo = null, nickName = null, ip = null;

                    String[] inputs = null;
                    DateTime now = DateTime.Now;
                    const string path = "c:/GeneratedScripts";
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);

                    if (!System.IO.Directory.Exists(path_rollback))
                        System.IO.Directory.CreateDirectory(path_rollback);

                    List<string> peripIdsList = null;
                    List<string> peripIdsListForPawnWSPerips = null;
                    string storeAppId = null;
                    bool isFirstStore = true;
                    consolidated_queries_list = new List<string>();
                    consolidated_roolback_queries_list = new List<string>();
                    while ((line = sReader.ReadLine()) != null)
                    {
                        ccsowner_queries = new List<string>();
                        pawnsec_clientReg_queries = new List<string>();
                        pawnsec_storeclientconfig_queries = new List<string>();
                        pawnsec_storesiteinfo_queries = new List<string>();
                        pawnsec_clientstoremap_queries = new List<string>();
                        ccsowner_workstationperipheral_queries = new List<string>();
                        ccsowner_pawnworkstationperipheral_queries = new List<string>();
                        peripIdsList = new List<string>();
                        peripIdsListForPawnWSPerips = new List<string>();
                        pVFDevices = new List<string>();
                        pReceiptPrinterList = new List<string>();

                        inputs = line.Split(',');
                        storeNo = validateStoreNum(inputs[0]);
                        nickName = inputs[1];
                        ip = inputs[2];
                        int index = ip.LastIndexOf('.');

                        this.storeNumber = storeNo;
                        if (PawnStoreProcedures.IsStoreSiteInfoPopulated(ref this.dAPawnSec, storeNo))
                        {
                            if (!this.NoDebugMsg.Checked)
                            {
                                DialogResult dR = MessageBox.Show("The store " + storeNo + " already exist in Pawnsec. Click Yes to conitnue with other stores or No to stop the script generation", "Config Tool Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dR == DialogResult.Yes)
                                    continue;
                                else
                                    return;
                            }
                        }

                        //if (this.loadRunnerInd.Checked)
                        processStoreBootStrap(storeNo);

                        StoreId = getStoreId(storeNumber);

                        if (isFirstStore)
                        {
                            isFirstStore = false;
                            LoadStoreInfoForInputFileProcess();
                            //hard coded for now....
                            storeAppId = "1";//GenerateAppVersion();
                            //this.storeData.PawnSecData.Stores[0].AppVersion.AppVersionId;
                        }
                        string storeSiteId = GenerateStoreSiteInfo(storeNo, storeAppId);

                        //GenerateWorkstations(storeNo, ip, nickName, storeSiteId, peripIdsList);
                        //ccsowner_queries = new List<string>();
                        //ccsowner_workstationperipheral_queries = new List<string>();

                        if (!this.loadRunnerInd.Checked)
                        {
                            ccsowner_queries.Add("--GENERATING WU PRINTERS *****");
                            //printer number .5. - WU
                            ip = ip.Substring(0, index + 1) + "4";
                            peripIdsList.Add(GeneratePeripherals(storeNo, ip, (nickName + WU_PRINTER_PREFIX).ToLowerInvariant(), WU_PRINTER_NAME, 1, WU_PORT, false, 1));

                            ccsowner_queries.Add("--GENERATING VF PRINTERS *****");
                            //printer number from .71 to 90.
                            ip = ip.Substring(0, index + 1) + "70";
                            //peripIdsList.Add(GeneratePeripherals(storeNo, ip, (nickName + VF_PRINTER_PREFIX).ToLowerInvariant(), VF_PRINTER_NAME, 20, VF_PORT, true));

                            //as per the new requirement the VF is not any more required
                            //GeneratePeripherals(storeNo, ip, (nickName + VF_PRINTER_PREFIX).ToLowerInvariant(), VF_PRINTER_NAME, 20, VF_PORT, true, 1);

                            //The verifone devices should map 1:1 relationship within workstationperipherals.  
                            //Instead vf1 is being mapped to every workstation.
                            //for (int i = 0; i < pVFDevices.Count(); i++)
                            //{
                            //    peripIdsList.Add(pVFDevices[i]);
                            //}

                            GenerateConversionUser(storeNo, path);
                            if (cdowner_conversion_query_path != string.Empty)
                                consolidated_queries_list.Add(cdowner_conversion_query_path);
                            if (cdowner_conversion_rollback_path != string.Empty)
                                consolidated_roolback_queries_list.Add(cdowner_conversion_rollback_path);
                        }

                        ccsowner_queries.Add("--GENERATING BAR CODE PRINTERS *****");
                        //printer number starts from .6. The .8 and .9 ips are for label printers...
                        ip = ip.Substring(0, index + 1) + "5";
                        //peripIdsList.Add(GeneratePeripherals(storeNo, ip, (nickName + BARCODE_PRINTER_PREFIX).ToLowerInvariant(), BARCODE_PRINTER_NAME, 4, P_PORT));
                        peripIdsListForPawnWSPerips.Add(GeneratePeripherals(storeNo, ip, (nickName + BARCODE_PRINTER_PREFIX).ToLowerInvariant(), BARCODE_PRINTER_NAME, 4, P_PORT, false, 1));

                        ccsowner_queries.Add("--GENERATING RECEIPT PRINTERS*****");
                        //printer number starts from .31
                        ip = ip.Substring(0, index + 1) + "30";
                        string returnPeripId = GeneratePeripherals(storeNo, ip, (nickName + RECEIPT_PRINTER_PREFIX).ToLowerInvariant(), RECEIPT_PRINTER_NAME, 9, P_PORT, false, 1);
                        //peripIdsList.Add(returnPeripId);
                        //peripIdsListForPawnWSPerips.Add(returnPeripId);

                        ccsowner_queries.Add("-- GENERATING LASER PRINTERS*****");

                        //for phase1 workstationperipherals
                        ip = ip.Substring(0, index + 1) + "40";
                        peripIdsList.Add(GeneratePeripherals(storeNo, ip, (nickName + LASER_PRINTER_PREFIX).ToLowerInvariant(), LASER_PRINTER_NAME, 1, P_PORT, false, 1));

                        //for phase2 pawnworkstationperipherals
                        ip = ip.Substring(0, index + 1) + "41";
                        peripIdsListForPawnWSPerips.Add(GeneratePeripherals(storeNo, ip, (nickName + LASER_PRINTER_PREFIX).ToLowerInvariant(), LASER_PRINTER_NAME, 2, P_PORT, false, 2));

                        //extra peripherals
                        ip = ip.Substring(0, index + 1) + "42";
                        GeneratePeripherals(storeNo, ip, (nickName + LASER_PRINTER_PREFIX).ToLowerInvariant(), LASER_PRINTER_NAME, 8, P_PORT, false, 3);

                        ccsowner_queries.Add("--GENERATING WORKSTATIONS *****");
                        GenerateWorkstations(storeNo, ip, nickName.ToUpperInvariant(), storeSiteId, peripIdsList, peripIdsListForPawnWSPerips);

                        string ccsownerFilePath = path + "/" + storeNo + "_" + now.Ticks + ".sql";
                        StreamWriter sWriter = File.CreateText(ccsownerFilePath);
                        string queryTemp = string.Empty;
                        for (int i = 0; i < ccsowner_queries.Count; i++)
                        {
                            queryTemp = ccsowner_queries[i];
                            if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                                sWriter.WriteLine(ccsowner_queries[i] + ";" + Environment.NewLine);
                        }

                        sWriter.WriteLine("--GENERATING WORKSTATIONPERIPHERAL MAPPINGS *****" + Environment.NewLine);
                        for (int i = 0; i < ccsowner_workstationperipheral_queries.Count; i++)
                        {
                            queryTemp = ccsowner_workstationperipheral_queries[i];
                            if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                                sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                        }

                        sWriter.WriteLine("--GENERATING PAWNWORKSTATIONPERIPHERAL MAPPINGS *****" + Environment.NewLine);
                        for (int i = 0; i < ccsowner_pawnworkstationperipheral_queries.Count; i++)
                        {
                            queryTemp = ccsowner_pawnworkstationperipheral_queries[i];
                            if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                                sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                        }

                        consolidated_queries_list.Add(ccsownerFilePath);
                        //Close the file stream;
                        sWriter.Close();
                        sWriter.Dispose();

                        // DELETE QUERIES
                        string rollbackFilePath = path_rollback + storeNo + DELETE_PATH_PREFIX + now.Ticks + ".sql";
                        sWriter = File.CreateText(rollbackFilePath);

                        for (int i = 0; i < ccsowner_workstationperipheral_queries.Count; i++)
                        {
                            queryTemp = ccsowner_workstationperipheral_queries[i];
                            if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(COMMIT_STRING))
                                sWriter.WriteLine(ccsowner_workstationperipheral_queries[i] + ";" + Environment.NewLine);
                        }

                        for (int i = 0; i < ccsowner_pawnworkstationperipheral_queries.Count; i++)
                        {
                            queryTemp = ccsowner_pawnworkstationperipheral_queries[i];
                            if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(COMMIT_STRING))
                                sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                        }

                        for (int i = 0; i < ccsowner_queries.Count; i++)
                        {
                            queryTemp = ccsowner_queries[i];
                            if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(COMMIT_STRING))
                                sWriter.WriteLine(ccsowner_queries[i] + ";" + Environment.NewLine);
                        }

                        consolidated_roolback_queries_list.Add(rollbackFilePath);
                        //Close the file stream;
                        sWriter.Close();
                        sWriter.Dispose();

                        // PAWNSEC QUERIES
                        string pawnsecFilePath = path + "/" + storeNo + PAWNSEC_PREFIX + now.Ticks + ".sql";
                        StreamWriter pSecWriter = File.CreateText(pawnsecFilePath);

                        /*pSecWriter.WriteLine("--GENERATING STOREAPPVERSION*****" + Environment.NewLine);
                        for (int i = 0; i < pawnsec_storeappversion_queries.Count; i++)
                            pSecWriter.WriteLine(pawnsec_storeappversion_queries[i] + ";" + Environment.NewLine);
                        pSecWriter.WriteLine("--STOREAPPVERSION GENERATION COMPLETED*****" + Environment.NewLine);
                         */

                        pSecWriter.WriteLine("--GENERATING CLIENTREGISTRYS*****" + Environment.NewLine);
                        for (int i = 0; i < pawnsec_clientReg_queries.Count; i++)
                        {
                            queryTemp = pawnsec_clientReg_queries[i];
                            if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                                pSecWriter.WriteLine(pawnsec_clientReg_queries[i] + ";" + Environment.NewLine);
                        }
                        pSecWriter.WriteLine("--CLIENTREGISTRYS GENERATION COMPLETED*****" + Environment.NewLine);

                        pSecWriter.WriteLine("--GENERATING STORECLIENTCONFIG*****" + Environment.NewLine);
                        for (int i = 0; i < pawnsec_storeclientconfig_queries.Count; i++)
                        {
                            queryTemp = pawnsec_storeclientconfig_queries[i];
                            if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                                pSecWriter.WriteLine(pawnsec_storeclientconfig_queries[i] + ";" + Environment.NewLine);
                        }
                        pSecWriter.WriteLine("--STORECLIENTCONFIG GENERATION COMPLETED*****" + Environment.NewLine);

                        pSecWriter.WriteLine("-- GENERATING STORESITEINFO*****" + Environment.NewLine);
                        for (int i = 0; i < pawnsec_storesiteinfo_queries.Count; i++)
                        {
                            queryTemp = pawnsec_storesiteinfo_queries[i];
                            if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                                pSecWriter.WriteLine(pawnsec_storesiteinfo_queries[i] + ";" + Environment.NewLine);
                        }
                        pSecWriter.WriteLine("--STORESITEINFO GENERATION COMPLETED*****" + Environment.NewLine);

                        pSecWriter.WriteLine("-- GENERATING CLIENTSTOREMAP*****" + Environment.NewLine);
                        for (int i = 0; i < pawnsec_clientstoremap_queries.Count; i++)
                        {
                            queryTemp = pawnsec_clientstoremap_queries[i];
                            if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                                pSecWriter.WriteLine(pawnsec_clientstoremap_queries[i] + ";" + Environment.NewLine);
                        }
                        pSecWriter.WriteLine("--CLIENTSTOREMAP GENERATION COMPLETED*****" + Environment.NewLine);

                        consolidated_queries_list.Add(pawnsecFilePath);
                        pSecWriter.Close();
                        pSecWriter.Dispose();

                        // DELETE QUERIES
                        rollbackFilePath = path_rollback + storeNo + DELETE_PATH_PREFIX + PAWNSEC_PREFIX + now.Ticks + ".sql";
                        pSecWriter = File.CreateText(rollbackFilePath);

                        for (int i = 0; i < pawnsec_clientstoremap_queries.Count; i++)
                        {
                            queryTemp = pawnsec_clientstoremap_queries[i];
                            if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(COMMIT_STRING))
                                pSecWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                        }

                        for (int i = 0; i < pawnsec_storeclientconfig_queries.Count; i++)
                        {
                            queryTemp = pawnsec_storeclientconfig_queries[i];
                            if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(COMMIT_STRING))
                                pSecWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                        }

                        for (int i = 0; i < pawnsec_clientReg_queries.Count; i++)
                        {
                            queryTemp = pawnsec_clientReg_queries[i];
                            if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(COMMIT_STRING))
                                pSecWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                        }

                        for (int i = 0; i < pawnsec_storesiteinfo_queries.Count; i++)
                        {
                            queryTemp = pawnsec_storesiteinfo_queries[i];
                            if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(COMMIT_STRING))
                                pSecWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                        }
                        pSecWriter.WriteLine(COMMIT_STRING + Environment.NewLine);
                        consolidated_roolback_queries_list.Add(rollbackFilePath);
                        //Close the file stream;
                        pSecWriter.Close();
                        pSecWriter.Dispose();

                        if (this.loadRunnerInd.Checked)
                            createScriptsForLoadRun(storeNo);

                        if (!this.loadRunnerInd.Checked)
                            DataAccessService.RollbackTransactionBlock(CCSOWNER, "CCSOWNER CHANGE", ref dACcs);

                        //to generate store types and store products scripts
                        GenerateStoreTypesAndStoreProductsQueries(path);
                    }

                    if (sReader.EndOfStream)
                    {
                        //Close the file stream;
                        sReader.Close();
                        sReader.Dispose();
                        //reset the collection values
                        ccsowner_queries = new List<string>();
                        pawnsec_clientReg_queries = new List<string>();
                        pawnsec_storeclientconfig_queries = new List<string>();
                        pawnsec_storesiteinfo_queries = new List<string>();
                        pawnsec_clientstoremap_queries = new List<string>();
                        ccsowner_workstationperipheral_queries = new List<string>();
                        //pawnsec_storeappversion_queries = new List<string>();
                        string cosolidatedFilePath = path + "/CosolidatedSQLs.sql";
                        StreamWriter fWriter = File.CreateText(cosolidatedFilePath);
                        //@C:/MyActivities/DataAnalysis/LoanTransaction_update_scripts.sql;
                        for (int i = 0; i < consolidated_queries_list.Count(); i++)
                        {
                            string sqlFileName = consolidated_queries_list[i];
                            if (!sqlFileName.Contains(PAWNSEC_PREFIX))
                            {
                                fWriter.WriteLine("@" + consolidated_queries_list[i] + ";" + Environment.NewLine);
                                fWriter.WriteLine(COMMIT_STRING + Environment.NewLine);
                            }
                        }
                        fWriter.Close();
                        fWriter.Dispose();

                        cosolidatedFilePath = path + "/CosolidatedPawnSecSQLs.sql";
                        fWriter = File.CreateText(cosolidatedFilePath);
                        //@C:/MyActivities/DataAnalysis/LoanTransaction_update_scripts.sql;
                        for (int i = 0; i < consolidated_queries_list.Count(); i++)
                        {
                            string sqlFileName = consolidated_queries_list[i];
                            if (sqlFileName.Contains(PAWNSEC_PREFIX))
                            {
                                fWriter.WriteLine("@" + consolidated_queries_list[i] + ";" + Environment.NewLine);
                                fWriter.WriteLine(COMMIT_STRING + Environment.NewLine);
                            }
                        }
                        fWriter.Close();
                        fWriter.Dispose();

                        cosolidatedFilePath = path_rollback + "CosolidatedRollBackSQLs.sql";
                        fWriter = File.CreateText(cosolidatedFilePath);
                        //@C:/MyActivities/DataAnalysis/LoanTransaction_update_scripts.sql;
                        for (int i = 0; i < consolidated_roolback_queries_list.Count(); i++)
                        {
                            string sqlFileName = consolidated_roolback_queries_list[i];
                            if (!sqlFileName.Contains(PAWNSEC_PREFIX))
                            {
                                fWriter.WriteLine("@" + consolidated_roolback_queries_list[i] + ";" + Environment.NewLine);
                                fWriter.WriteLine(COMMIT_STRING + Environment.NewLine);
                            }
                        }
                        fWriter.Close();
                        fWriter.Dispose();

                        cosolidatedFilePath = path_rollback + "CosolidatedPawnSecRollBackSQLs.sql";
                        fWriter = File.CreateText(cosolidatedFilePath);
                        //@C:/MyActivities/DataAnalysis/LoanTransaction_update_scripts.sql;
                        for (int i = 0; i < consolidated_roolback_queries_list.Count(); i++)
                        {
                            string sqlFileName = consolidated_roolback_queries_list[i];
                            if (sqlFileName.Contains(PAWNSEC_PREFIX))
                            {
                                fWriter.WriteLine("@" + consolidated_roolback_queries_list[i] + ";" + Environment.NewLine);
                                fWriter.WriteLine(COMMIT_STRING + Environment.NewLine);
                            }
                        }
                        fWriter.Close();
                        fWriter.Dispose();

                        MessageBox.Show("Generation Completed.");
                    }
                }
                catch (Exception eX)
                {
                    MessageBox.Show(eX.Message);
                }
            }
        }

        private void createScriptsForLoadRun(string storeNo)
        {
            try
            {
                DateTime now = DateTime.Now;
                const string path = "c:/GeneratedScripts/LoadRunner";
                const string LOAD_RUN_PREFIX = "_LoadRun_";
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                //userNamePrefix = USERNAME_PREFIX  + storeNo;
                //empNoPrefix = EMPNO_PREFIX + storeNo;
                //string EMP_TYPE  = "Shop Manager";

                cdowner_safe_query = null;
                cdowner_cashdrawer_queries = new List<string>();
                cdowner_cashdraweruser_queries = new List<string>();
                cdowner_userroles_queries = new List<string>();
                cdowner_userinfodetail_queries = new List<string>();
                cdowner_userinfo_queries = new List<string>();
                cdowner_nextnum_queries = new List<string>();
                cdowner_usergroups_queries = new List<string>();
                for (int i = 1; i < 6; i++)
                {
                    //Scripts for Load Runner
                    addUserToLDAP(USERNAME_PREFIX + storeNo + i, PASSWORD, EMPNO_PREFIX + storeNo + i, EMP_TYPE);
                }

                //Generate NextNum queries...
                GenerateNextNumForLoadRun(storeNo);

                string cdownerFilePath = path + "/" + storeNo + LOAD_RUN_PREFIX + now.Ticks + ".sql";
                StreamWriter cdownerWriter = File.CreateText(cdownerFilePath);

                string queryTemp = string.Empty;

                cdownerWriter.WriteLine("-- GENERATING USERINFO*****" + Environment.NewLine);
                for (int i = 0; i < cdowner_userinfo_queries.Count; i++)
                {
                    queryTemp = cdowner_userinfo_queries[i];
                    if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }
                cdownerWriter.WriteLine("-- COMPLETED USERINFO*****" + Environment.NewLine);

                cdownerWriter.WriteLine("-- GENERATING USERINFODETAIL*****" + Environment.NewLine);
                for (int i = 0; i < cdowner_userinfodetail_queries.Count; i++)
                {
                    queryTemp = cdowner_userinfodetail_queries[i];
                    if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }
                cdownerWriter.WriteLine("-- COMPLETED USERINFODETAIL*****" + Environment.NewLine);

                cdownerWriter.WriteLine("-- GENERATING CASHDRAWERUSER*****" + Environment.NewLine);
                for (int i = 0; i < cdowner_cashdraweruser_queries.Count; i++)
                {
                    queryTemp = cdowner_cashdraweruser_queries[i];
                    if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                cdownerWriter.WriteLine("-- COMPLETED CASHDRAWERUSER*****" + Environment.NewLine);

                cdownerWriter.WriteLine("-- GENERATING SAFE*****" + Environment.NewLine);
                cdownerWriter.WriteLine(cdowner_safe_query + ";" + Environment.NewLine);
                cdownerWriter.WriteLine("-- COMPLETED SAFE*****" + Environment.NewLine);

                cdownerWriter.WriteLine("-- GENERATING CASHDRAWER*****" + Environment.NewLine);
                for (int i = 0; i < cdowner_cashdrawer_queries.Count; i++)
                {
                    queryTemp = cdowner_cashdrawer_queries[i];
                    if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }
                cdownerWriter.WriteLine("-- COMPLETED CASHDRAWER*****" + Environment.NewLine);

                cdownerWriter.WriteLine("-- GENERATING USERROLE*****" + Environment.NewLine);
                for (int i = 0; i < cdowner_userroles_queries.Count; i++)
                {
                    queryTemp = cdowner_userroles_queries[i];
                    if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(USERSECURITYPROFILE_UPDATE))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }
                cdownerWriter.WriteLine("-- COMPLETED USERROLE*****" + Environment.NewLine);

                cdownerWriter.WriteLine("-- GENERATING USERGROUP*****" + Environment.NewLine);
                for (int i = 0; i < cdowner_usergroups_queries.Count; i++)
                {
                    queryTemp = cdowner_usergroups_queries[i];
                    if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }
                cdownerWriter.WriteLine("-- COMPLETED USERGROUP*****" + Environment.NewLine);

                if (cdowner_nextnum_queries.Count > 0)
                {
                    cdownerWriter.WriteLine("-- GENERATING NEXTNUM*****" + Environment.NewLine);
                    for (int i = 0; i < cdowner_nextnum_queries.Count; i++)
                    {
                        queryTemp = cdowner_nextnum_queries[i];
                        //if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(USERSECURITYPROFILE_UPDATE))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                    }
                    cdownerWriter.WriteLine("-- COMPLETED NEXTNUM*****" + Environment.NewLine);
                }

                consolidated_queries_list.Add(cdownerFilePath);

                cdownerWriter.Close();
                cdownerWriter.Dispose();

                // ROLLBACK QUERIES
                string rollbackFilePath = path_rollback + storeNo + DELETE_PATH_PREFIX + LOAD_RUN_PREFIX + now.Ticks + ".sql";
                cdownerWriter = File.CreateText(rollbackFilePath);

                for (int i = 0; i < cdowner_cashdrawer_queries.Count; i++)
                {
                    queryTemp = cdowner_cashdrawer_queries[i];
                    if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(COMMIT_STRING))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                for (int i = 0; i < cdowner_cashdraweruser_queries.Count; i++)
                {
                    queryTemp = cdowner_cashdraweruser_queries[i];
                    if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(COMMIT_STRING))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                for (int i = 0; i < cdowner_userroles_queries.Count; i++)
                {
                    queryTemp = cdowner_userroles_queries[i];
                    if ((queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) || queryTemp.Contains(USERSECURITYPROFILE_UPDATE))
                        && !queryTemp.Contains(COMMIT_STRING))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                cdownerWriter.WriteLine(COMMIT_STRING + Environment.NewLine);

                for (int i = 0; i < cdowner_usergroups_queries.Count; i++)
                {
                    queryTemp = cdowner_usergroups_queries[i];
                    if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                for (int i = 0; i < cdowner_userinfodetail_queries.Count; i++)
                {
                    queryTemp = cdowner_userinfodetail_queries[i];
                    if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(COMMIT_STRING))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                for (int i = 0; i < cdowner_userinfo_queries.Count; i++)
                {
                    queryTemp = cdowner_userinfo_queries[i];
                    if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(COMMIT_STRING))
                        cdownerWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                cdownerWriter.WriteLine(COMMIT_STRING + Environment.NewLine);
                consolidated_roolback_queries_list.Add(rollbackFilePath);

                cdownerWriter.Close();
                cdownerWriter.Dispose();

            }
            catch (Exception eX)
            {
                MessageBox.Show(eX.Message);
            }
        }

        private void addUserToLDAP(string userName, string password, string empNo, string empType)
        {
            //if (PawnLDAPAccessor.Instance.State == PawnLDAPAccessor.LDAPState.CONNECTED)
            // {

            bool addedUserToLdap = true;
            string errMsg;
            if (!PawnLDAPAccessor.Instance.CreateUser(
                userName,
                password,
                userName,
                empNo,
                empType,
                out errMsg))
            {
                if (!this.NoDebugMsg.Checked)
                {
                    MessageBox.Show("The LDAP Add User operation has failed: " + errMsg +
                        ".  Will attempt to validate against existing credentials to " +
                        "verify if the user is already in the LDAP system");
                }
                addedUserToLdap = false;
            }

            int numTries = 0;
            DateTime pwdLastModified;
            string[] pwdHistory;
            string dispName;
            bool lockedOut;
            if (!addedUserToLdap &&
                PawnLDAPAccessor.Instance.AuthorizeUser(
                userName,
                password,
                ref numTries,
                out pwdLastModified,
                out pwdHistory, out dispName, out lockedOut))
            {
                addedUserToLdap = true;
            }
            if (addedUserToLdap)
                GenerateUserInfo(userName, empNo);
            else
                Console.WriteLine("Error while adding using to LDAP..");
            //}
            //else
            //    Console.WriteLine("LDAP not connected...");
        }

        private void GenerateConversionUser(string StoreId, string path)
        {
            string safeId = null;
            string registeredUserId = null;
            PawnStoreProcedures.RetrieveSafe(ref dACcs, StoreId + CDNAME_SAFE, ref safeId, ref registeredUserId);
            if (!PawnStoreProcedures.IsCashdrawerUserExist(ref dACcs, registeredUserId))
            {
                string userName = "CONV" + StoreId;
                addUserToLDAP(userName, PASSWORD, EMPNO_PREFIX + StoreId, EMP_TYPE);
                string cashdrawerUserId = GenerateCashdrawerUser(userName);
                //?CD_NAME?, ?SAFE_ID?
                var cdParms = new PairType<string, string>[2];
                cdParms[0] = new PairType<string, string>("CD_ID", cashdrawerUserId);
                cdParms[1] = new PairType<string, string>("SAFE_ID", safeId);
                string insCDQuery = string.Empty;
                if (safeId != null)
                {
                    insCDQuery = DataAccessService.PrepareVariableQuery(
                            ref this.dACcs, PawnStoreSetupQueries.UPDATE_CDOWNER_CASHDRAWER, cdParms);
                }
                //else
                //{
                //    MessageBox.Show("The safe is not available for the Store -->" + StoreId, PawnStoreSetupForm.SETUPALERT_TXT);
                //    //return;
                //}
                cdowner_conversion_query_path = path + "/" + StoreId + "_CDOwner_" + DateTime.Now.Ticks + ".sql";
                StreamWriter sWriter = File.CreateText(cdowner_conversion_query_path);

                for (int i = 0; i < cdowner_userinfo_queries.Count; i++)
                {
                    string queryTemp = cdowner_userinfo_queries[i];
                    if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                for (int i = 0; i < cdowner_userinfodetail_queries.Count; i++)
                {
                    string queryTemp = cdowner_userinfodetail_queries[i];
                    if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                for (int i = 0; i < cdowner_userroles_queries.Count; i++)
                {
                    string queryTemp = cdowner_userroles_queries[i];
                    if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) && !queryTemp.Contains(USERSECURITYPROFILE_UPDATE))
                        sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                for (int i = 0; i < cdowner_usergroups_queries.Count; i++)
                {
                    string queryTemp = cdowner_usergroups_queries[i];
                    if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                if (!cdowner_cashdraweruser_queries.Contains(ROLLBACK_SCRIPT_PREFIX1))
                    sWriter.WriteLine(cdowner_cashdraweruser_queries[0] + ";" + Environment.NewLine);

                GenerateConrsionUserLimitsAndResources();
                for (int i = 0; i < userlimits_resources_queries_list.Count; i++)
                {
                    string queryTemp = userlimits_resources_queries_list[i];
                    if (!queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }


                if (!insCDQuery.Equals(string.Empty))
                    sWriter.WriteLine(insCDQuery + ";" + Environment.NewLine);
                sWriter.Close();
                sWriter.Dispose();

                //ROLLBACK SCRIPTS

                cdowner_conversion_rollback_path = path_rollback + StoreId + DELETE_PATH_PREFIX + "CDOwner_" + DateTime.Now.Ticks + ".sql";
                sWriter = File.CreateText(cdowner_conversion_rollback_path);

                //if (cdowner_userinfo_queries.Contains(ROLLBACK_SCRIPT_PREFIX1))
                //    sWriter.WriteLine(cdowner_userinfo_queries[0] + ";" + Environment.NewLine);

                //if (cdowner_userinfodetail_queries.Contains(ROLLBACK_SCRIPT_PREFIX1))
                //    sWriter.WriteLine(cdowner_userinfodetail_queries[0] + ";" + Environment.NewLine);

                for (int i = 0; i < cdowner_userinfodetail_queries.Count; i++)
                {
                    string queryTemp = cdowner_userinfodetail_queries[i];
                    if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                for (int i = 0; i < cdowner_userroles_queries.Count; i++)
                {
                    string queryTemp = cdowner_userroles_queries[i];
                    if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1) || queryTemp.Contains(USERSECURITYPROFILE_UPDATE))
                        sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                for (int i = 0; i < cdowner_cashdraweruser_queries.Count; i++)
                {
                    string queryTemp = cdowner_cashdraweruser_queries[i];
                    if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                for (int i = 0; i < cdowner_usergroups_queries.Count; i++)
                {
                    string queryTemp = cdowner_usergroups_queries[i];
                    if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }
                //if (cdowner_cashdraweruser_queries.Contains(ROLLBACK_SCRIPT_PREFIX1))
                //    sWriter.WriteLine(cdowner_cashdraweruser_queries[0] + ";" + Environment.NewLine);

                cdParms[0] = new PairType<string, string>("CD_ID", registeredUserId);
                cdParms[1] = new PairType<string, string>("SAFE_ID", safeId);
                insCDQuery = DataAccessService.PrepareVariableQuery(
                        ref this.dACcs, PawnStoreSetupQueries.UPDATE_CDOWNER_CASHDRAWER, cdParms);

                sWriter.WriteLine(insCDQuery + ";" + Environment.NewLine);

                for (int i = 0; i < cdowner_userinfo_queries.Count; i++)
                {
                    string queryTemp = cdowner_userinfo_queries[i];
                    if (queryTemp.Contains(ROLLBACK_SCRIPT_PREFIX1))
                        sWriter.WriteLine(queryTemp + ";" + Environment.NewLine);
                }

                sWriter.Close();
                sWriter.Dispose();

                cdowner_userroles_queries = new List<string>();
                cdowner_userinfodetail_queries = new List<string>();
                cdowner_userinfo_queries = new List<string>();
                cdowner_cashdraweruser_queries = new List<string>();
                cdowner_usergroups_queries = new List<string>();

            }
            else
            {
                Console.WriteLine("CashdrawerUser does not exist for the safe -->" + StoreId + CDNAME_SAFE);
                //MessageBox.Show("CashdrawerUser does not exist for the safe -->" + StoreId + CDNAME_SAFE, PawnStoreSetupForm.SETUPALERT_TXT);
                //return;

            }

        }

        private void GenerateConrsionUserLimitsAndResources()
        {
            securityMasks = new List<PairType<string, string>>();
            securityResources = new List<PairType<string, string>>();
            userlimits_resources_queries_list = new List<string>();
            PawnStoreProcedures.RetrieveSecurityMasks(ref dACcs, DEFAULT_ROLE_NAME, ref securityMasks);
            PawnStoreProcedures.RetrieveSecurityResources(ref dACcs, DEFAULT_ROLE_NAME, ref securityResources);
            if (securityMasks.Count() > 0)
            {
                for (int i = 0; i < securityMasks.Count(); i++)
                {
                    PairType<string, string> maskValues = securityMasks[i];
                    var cdParms = new PairType<string, string>[3];
                    cdParms[0] = new PairType<string, string>("OBJECT_ID", maskValues.Left);
                    cdParms[1] = new PairType<string, string>("SECURITY_MASK", maskValues.Right);
                    cdParms[2] = new PairType<string, string>("USER_ID", newUserId.ToString());

                    var query = DataAccessService.PrepareVariableQuery(
                            ref this.dACcs,
                            PawnStoreSetupQueries.INSERT_SECURITY_MASKS_RESOURCES,
                            cdParms);
                    userlimits_resources_queries_list.Add(query);
                }
            }

            if (securityResources.Count() > 0)
            {
                for (int i = 0; i < securityResources.Count(); i++)
                {
                    PairType<string, string> resourceValues = securityResources[i];
                    var cdParms = new PairType<string, string>[3];
                    cdParms[0] = new PairType<string, string>("LIMIT", resourceValues.Left);
                    cdParms[1] = new PairType<string, string>("PRODOFFERINGID", resourceValues.Right);
                    cdParms[2] = new PairType<string, string>("USER_ID", newUserId.ToString());

                    var query = DataAccessService.PrepareVariableQuery(
                            ref this.dACcs,
                            PawnStoreSetupQueries.INSERT_SECURITY_MASKS_LIMITS,
                            cdParms);
                    userlimits_resources_queries_list.Add(query);
                }
            }

        }

        private void GenerateUserInfo(string userName, string empNo)
        {
            if (newUserId == 0)
            {
                ulong retUsrId = 0;
                PawnStoreProcedures.GetNextIdStr(ref this.dACcs, "USERID", "USERINFO", out retUsrId);
                newUserId = retUsrId;
            }
            else
                newUserId = newUserId + 1;

            //Create parms
            var cdParms = new PairType<string, string>[5];
            cdParms[0] = new PairType<string, string>("UI_ID", newUserId.ToString());
            cdParms[1] = new PairType<string, string>("UI_NAME", userName);
            if (this.loadRunnerInd.Checked)
            {
                cdParms[2] = new PairType<string, string>("UI_FNAME", LOADTEST_FIRSTNAME);
                cdParms[3] = new PairType<string, string>("UI_LNAME", LOADTEST_LASTNAME);
            }
            else
            {
                cdParms[2] = new PairType<string, string>("UI_FNAME", "CONVUser");
                cdParms[3] = new PairType<string, string>("UI_LNAME", "CONVUser");
            }
            cdParms[4] = new PairType<string, string>("UI_STONUM", this.storeNumber);

            var query = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CCSOWNER_USERINFO,
                    cdParms);
            cdowner_userinfo_queries.Add(query);
            cdowner_userinfo_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "CCSOWNER.USERINFO WHERE USERID =" + "'" + newUserId + "'");

            GenerateUserInfoDetail(empNo);
            GenerateUserRoles();
            GenerateUserGroup(newUserId.ToString());
            if (this.loadRunnerInd.Checked)
            {
                string cashdrawerUserId = GenerateCashdrawerUser(userName);
                GenerateCashdrawer(userName, cashdrawerUserId);
            }
        }

        private void GenerateUserGroup(string userId)
        {
            //Create parms
            var cdParms = new PairType<string, string>[1];
            cdParms[0] = new PairType<string, string>("UR_ID", userId);

            var query = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CCSOWNER_USERGROUP,
                    cdParms);
            cdowner_usergroups_queries.Add(query);
            cdowner_usergroups_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "CCSOWNER.USERGROUP WHERE USERID=" + "'" + userId + "'");
        }

        private void GenerateUserInfoDetail(string empNo)
        {
            string guid = this.generateGuid("0");
            //Create parms
            var cdParms = new PairType<string, string>[4];
            cdParms[0] = new PairType<string, string>("UID_ID", guid);
            cdParms[1] = new PairType<string, string>("UID_USID", newUserId.ToString());
            cdParms[2] = new PairType<string, string>("UID_STONUM", this.storeNumber);
            cdParms[3] = new PairType<string, string>("UID_EMNUM", empNo);

            var query = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CCSOWNER_USERINFODETAIL,
                    cdParms);
            cdowner_userinfodetail_queries.Add(query);
            cdowner_userinfodetail_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "CCSOWNER.USERINFODETAIL WHERE USERINFODETAILID=" + "'" + guid + "'");
        }

        private void GenerateUserRoles()
        {
            var roleParms = new PairType<string, string>[2];
            roleParms[0] = new PairType<string, string>("UR_ID", newUserId.ToString());
            roleParms[1] = new PairType<string, string>("UR_RID", DEFAULT_ROLE_NAME);

            var usrRolesQuery = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CCSOWNER_USERROLES,
                    roleParms);

            //Madhu...since the userroles trigger is removed the commit can be taken out
            //if (cdowner_userroles_queries.Count() == 0)
            //    cdowner_userroles_queries.Add(COMMIT_STRING);
            cdowner_userroles_queries.Add(usrRolesQuery);
            cdowner_userroles_queries.Add(USERSECURITYPROFILE_UPDATE + "'" + newUserId + "'");
            cdowner_userroles_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "CCSOWNER.USERSECURITYPROFILE WHERE USERID =" + "'" + newUserId + "'");
            cdowner_userroles_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "CCSOWNER.USERLIMITS WHERE USERID =" + "'" + newUserId + "'");
            cdowner_userroles_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "CCSOWNER.USERROLES WHERE USERID =" + "'" + newUserId + "'");
            //Madhu...since the userroles trigger is removed the commit can be taken out
            //cdowner_userroles_queries.Add(COMMIT_STRING);
        }

        private string GenerateCashdrawerUser(string userName)
        {
            string guid = generateGuid("0");
            var cduParms = new PairType<string, string>[5];
            cduParms[0] = new PairType<string, string>("CDU_ID", guid);
            cduParms[1] = new PairType<string, string>("CDU_UID", newUserId.ToString());
            cduParms[2] = new PairType<string, string>("CDU_NAME", userName);
            cduParms[3] = new PairType<string, string>("CDU_STONUM", this.storeNumber);
            cduParms[4] = new PairType<string, string>("CDU_BRID", this.StoreId);

            var insCDUsrQuery = DataAccessService.PrepareVariableQuery(
                ref this.dACcs,
                PawnStoreSetupQueries.MERGE_CDOWNER_CASHDRAWERUSER, cduParms);
            cdowner_cashdraweruser_queries.Add(insCDUsrQuery);
            cdowner_cashdraweruser_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "CDOWNER.CD_CASHDRAWERUSER" + ROLLBACK_SCRIPT_PREFIX2 + "'" + guid + "'");
            return guid;
        }

        private void GenerateCashdrawer(string userName, string cashdrawerUserId)
        {
            string guid = generateGuid("0");
            //Create parms
            var cdParms = new PairType<string, string>[6];
            cdParms[0] = new PairType<string, string>("CD_ID", guid);
            string objTypeStr = null;
            string CWName = null;
            if (string.IsNullOrEmpty(cdowner_safe_query))
            {
                objTypeStr = OBJECT_TYPE;
                CWName = this.storeNumber + CDNAME_SAFE;
                cdParms[1] = new PairType<string, string>("CD_NAME", CWName);
            }
            else
            {
                objTypeStr = CDNAME_CASHDRAWER;
                CWName = this.storeNumber + "_" + userName;
                cdParms[1] = new PairType<string, string>("CD_NAME", CWName);
            }

            cdParms[2] = new PairType<string, string>("CD_MGRID", cashdrawerUserId);
            cdParms[3] = new PairType<string, string>("CD_OTYPE", objTypeStr);
            cdParms[4] = new PairType<string, string>("CD_STONUM", this.storeNumber);
            cdParms[5] = new PairType<string, string>("CD_BRID", this.StoreId);
            var insCDQuery = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CDOWNER_CASHDRAWER,
                    cdParms);
            if (string.IsNullOrEmpty(cdowner_safe_query))
            {
                cdowner_safe_query = insCDQuery;
                //newUserId = newUserId + 1;
                cashdrawerUserId = GenerateCashdrawerUser(userName);
                GenerateCashdrawer(userName, cashdrawerUserId);
            }
            else
            {
                cdowner_cashdrawer_queries.Add(insCDQuery);
            }
            cdowner_cashdrawer_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "CDOWNER.CD_CASHDRAWER" + ROLLBACK_SCRIPT_PREFIX2 + "'" + cashdrawerUserId + "'");
        }

        private void LoadStoreInfoForInputFileProcess()
        {
            batchMode = true;
            storeData = new StoreSetupVO();
            //Get peripheral models and types
            PawnStoreProcedures.AcquirePeripheralTypeModel(
                ref this.dACcs, ref storeData.AllPeripheralTypes,
                ref storeData.AllPeripheralModels);

            //Acquire store data (siteid)
            string errCode, errString;
            ShopProcedures.ExecuteGetStoreInfo(
                    CCSOWNER,
                    this.dACcs.OracleDA,
                    this.storeNumber,
                    ref storeData.StoreInfo,
                    out errCode,
                    out errString);

            //Set store number into siteid
            this.storeData.StoreInfo.StoreNumber = this.storeNumber;

            //Pawn sec encrypted config container initialization
            this.eConfig = new EncryptedConfigContainer(
                Common.Properties.Resources.PrivateKey,
                this.publicKey, this.storeNumber, this.storeData.PawnSecData);
            writeMessage("Pawn encryption system initialized");
            this.generateConfigFile();
            writeMessage("Template config file written");
            bool changedPublicKey;
            //Initialize pawn sec data
            this.storeData.PawnSecData = new PawnSecVO(this.publicKey, this.storeNumber);
            PawnStoreProcedures.GetAllPawnSecData(ref this.dAPawnSec, storeNumber, this.storeData.StoreInfo,
                this.publicKey, ref this.storeData.PawnSecData, out changedPublicKey);
            batchMode = false;
        }

        private void GenerateClientStoreMap(string clientRegistryId, string storeSiteId, string storeClientConfigId)
        {
            ulong nexId = 0;
            string temp;
            this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.CLISTOMAP_ID, ref nexId, out temp);
            PairType<string, string>[] parms = new PairType<string, string>[5];
            parms[0] = new PairType<string, string>("CSM_ID1", nexId.ToString());
            parms[1] = new PairType<string, string>("CR_ID1", clientRegistryId);
            parms[2] = new PairType<string, string>("SSI_ID1", storeSiteId);
            parms[3] = new PairType<string, string>("SCC_ID1", storeClientConfigId);
            parms[4] = new PairType<string, string>("SC_ID1", "1"); //TODO

            string query = DataAccessService.PrepareVariableQuery(
                    ref this.dAPawnSec,
                    PawnStoreSetupQueries.MERGE_PAWNSEC_CLIENTSTOREMAP,
                    parms);

            pawnsec_clientstoremap_queries.Add(query);
            pawnsec_clientstoremap_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "clientstoremap" + ROLLBACK_SCRIPT_PREFIX2 + "'" + nexId + "'");
        }
        private string getNextIP(string currentIP)
        {
            int index = currentIP.LastIndexOf('.');
            int ipLastNum = Convert.ToInt32(currentIP.Substring(index + 1)) + 1;
            currentIP = currentIP.Substring(0, index + 1) + "" + ipLastNum;
            return currentIP;
        }

        /*        private string GeneratePeripherals(string storeNo, string ip, string peripName, string peripType,
                    int pMaxCount, string port,  bool isVFDevice, int pStartCount)
                {
                    string peripheralTypeID = null, peripModelId = null;
                    foreach (var perType in this.storeData.AllPeripheralTypes)
                    {
                        if (perType == null) continue;
                        if (perType.PeripheralTypeName.Equals(peripType))
                        {
                            peripheralTypeID = perType.PeripheralTypeId;
                            peripModelId = perType.PeripheralModel.Id;
                        }
                    }
                    string returnId = null;
                    string newGuid = null;
            
                    bool laserPrintFlag = false;
                    if(pStartCount == 2)
                        laserPrintFlag = true;

                    for (int i = pStartCount; i <= pMaxCount; i++)
                    {
                        newGuid = this.generateGuid("0");
                        if ((i == 1 && !isVFDevice) || laserPrintFlag)
                            returnId = newGuid;
                        else if (isVFDevice)
                        {
                            pVFDevices.Add(newGuid);
                        }

                        var paramArr = new PairType<string, string>[9];
                        paramArr[0] = new PairType<string, string>("PER_ID", newGuid);
                        paramArr[1] = new PairType<string, string>("PER_TID", peripheralTypeID);
                        ip = getNextIP(ip);
                        paramArr[2] = new PairType<string, string>("PER_IP", ip);
                        paramArr[3] = new PairType<string, string>("PER_PT", port);
                        paramArr[4] = new PairType<string, string>("PER_STOID", StoreId);
                        paramArr[5] = new PairType<string, string>("PER_NAME", peripName + i);
                        paramArr[6] = new PairType<string, string>("PER_MDID", peripModelId);
                        if (i == 1)
                            paramArr[7] = new PairType<string, string>("IS_PRI", "Y");
                        else
                            paramArr[7] = new PairType<string, string>("IS_PRI", "N");
                        paramArr[8] = new PairType<string, string>("PER_PRFID", i.ToString());

                        var insPerQuery = DataAccessService.PrepareVariableQuery(
                                ref this.dACcs,
                                PawnStoreSetupQueries.MERGE_CCSOWNER_PERIPHERALS_NEW,
                                paramArr);
                        ccsowner_queries.Add(insPerQuery);
                        ccsowner_queries.Add(ROLLBACK_SCRIPT_PREFIX1+" CCSOWNER.PERIPHERALS WHERE PERIPHERALID="+"'"+newGuid+"'");
                    }
                    return returnId;
                }
        */

        private string GeneratePeripherals(string storeNo, string ip, string peripName, string peripType,
        int pMaxCount, string port, bool isVFDevice, int pStartCount)
        {
            string peripheralTypeID = null, peripModelId = null;
            foreach (var perType in this.storeData.AllPeripheralTypes)
            {
                if (perType == null) continue;
                if (perType.PeripheralTypeName.Equals(peripType))
                {
                    peripheralTypeID = perType.PeripheralTypeId;
                    peripModelId = perType.PeripheralModel.Id;
                }
            }
            string returnId = null;
            string newGuid = null;

            bool laserPrintFlag = false;
            if (pStartCount == 2)
                laserPrintFlag = true;

            for (int i = pStartCount; i <= pMaxCount; i++)
            {
                newGuid = this.generateGuid("0");
                if (peripName.Contains(RECEIPT_PRINTER_PREFIX.ToLowerInvariant()))
                {
                    pReceiptPrinterList.Add(newGuid);
                }
                else if ((i == 1 && !isVFDevice) || laserPrintFlag)
                    returnId = newGuid;
                else if (isVFDevice)
                {
                    pVFDevices.Add(newGuid);
                }

                var paramArr = new PairType<string, string>[9];
                paramArr[0] = new PairType<string, string>("PER_ID", newGuid);
                paramArr[1] = new PairType<string, string>("PER_TID", peripheralTypeID);
                ip = getNextIP(ip);
                paramArr[2] = new PairType<string, string>("PER_IP", ip);
                paramArr[3] = new PairType<string, string>("PER_PT", port);
                paramArr[4] = new PairType<string, string>("PER_STOID", StoreId);
                paramArr[5] = new PairType<string, string>("PER_NAME", peripName + i);
                paramArr[6] = new PairType<string, string>("PER_MDID", peripModelId);
                if (i == 1)
                    paramArr[7] = new PairType<string, string>("IS_PRI", "Y");
                else
                    paramArr[7] = new PairType<string, string>("IS_PRI", "N");
                paramArr[8] = new PairType<string, string>("PER_PRFID", i.ToString());

                var insPerQuery = DataAccessService.PrepareVariableQuery(
                        ref this.dACcs,
                        PawnStoreSetupQueries.MERGE_CCSOWNER_PERIPHERALS_NEW,
                        paramArr);
                ccsowner_queries.Add(insPerQuery);
                ccsowner_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + " CCSOWNER.PERIPHERALS WHERE PERIPHERALID=" + "'" + newGuid + "'");
            }
            return returnId;
        }
        private void GenerateClientRegistry(string ip, string wsName, int teminalNum, string storeSiteId)
        {
            PairType<string, string>[] parms;
            ulong nexId = 0;
            string temp;
            this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.CLIREG_ID,
                                                                    ref nexId,
                                                                    out temp);
            parms = new PairType<string, string>[8];
            parms[0] = new PairType<string, string>("CR_ID1", nexId.ToString());

            if (!string.IsNullOrEmpty(ip))
                parms[1] = new PairType<string, string>("CR_IPADDRESS", ip);
            else
                parms[1] = new PairType<string, string>("CR_IPADDRESS", "");

            //parms[2] = new PairType<string, string>("CR_MACADDRESS", this.eConfig.EncryptValue(clientMac.Machine.MACAddress));
            parms[2] = new PairType<string, string>("CR_MACADDRESS", "");
            parms[3] = new PairType<string, string>("CR_ISALLOWED", "1");
            parms[4] = new PairType<string, string>("CR_ISCONNECTED", "0");
            //parms[5] = new PairType<string, string>("CR_ADOBEOVERRIDE", clientMac.Machine.AdobeOverride.EscapePath());
            parms[5] = new PairType<string, string>("CR_ADOBEOVERRIDE", "");
            //parms[6] = new PairType<string, string>("CR_GHOSTSCRIPTOVERRIDE", clientMac.Machine.GhostOverride.EscapePath());
            parms[6] = new PairType<string, string>("CR_GHOSTSCRIPTOVERRIDE", "");
            parms[7] = new PairType<string, string>("CR_MACHINENAME", (wsName + DOMAIN_NAME).ToUpperInvariant());

            string query = DataAccessService.PrepareVariableQuery(
                        ref this.dAPawnSec,
                        PawnStoreSetupQueries.MERGE_PAWNSEC_CLIENTREGISTRY,
                        parms);

            GenerateStoreClientConfig(nexId.ToString(), wsName, teminalNum, storeSiteId);

            if (!string.IsNullOrEmpty(query))
            {
                pawnsec_clientReg_queries.Add(query);
                pawnsec_clientReg_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "Pawnsec.clientregistry" + ROLLBACK_SCRIPT_PREFIX2 + "'" + nexId + "'");
            }
        }

        private void GenerateStoreClientConfig(string Id, string wsName, int terminalNum, string storeSiteId)
        {
            PairType<string, string>[] parms = new PairType<string, string>[6];
            ulong nexId = 0;
            string temp;
            this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.STOCLICFG_ID,
                                                                ref nexId,
                                                                out temp);
            parms[0] = new PairType<string, string>("SCC_ID1", nexId.ToString());
            parms[1] = new PairType<string, string>("SCC_WORKSTATIONID", wsName.ToUpperInvariant());
            parms[2] = new PairType<string, string>("SCC_TERMINALNUMBER", terminalNum.ToString());
            parms[3] = new PairType<string, string>("SCC_LOGLEVEL", "DEBUG");
            parms[4] = new PairType<string, string>("SCC_TRACELEVEL", "0");
            if (this.loadRunnerInd.Checked)
                parms[5] = new PairType<string, string>("SCC_PRINTENABLED", "0");
            else
                parms[5] = new PairType<string, string>("SCC_PRINTENABLED", "1");

            string query = DataAccessService.PrepareVariableQuery(
                    ref this.dAPawnSec,
                    PawnStoreSetupQueries.MERGE_PAWNSEC_STORECLIENTCONFIG,
                    parms);
            pawnsec_storeclientconfig_queries.Add(query);
            pawnsec_storeclientconfig_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "pawnsec.storeclientconfig" + ROLLBACK_SCRIPT_PREFIX2 + "'" + nexId + "'");
            GenerateClientStoreMap(Id, storeSiteId, nexId.ToString());
            //Generate audit queries
            if (wsName.Contains(MGR_PREFIX))
            {
                string storeSiteIdForAudit = GenerateStoreSiteInfo(this.storeNumber, "3");
                GenerateClientStoreMap(Id, storeSiteIdForAudit, nexId.ToString());
                string storeSiteIdForAuditQueries = GenerateStoreSiteInfo(this.storeNumber, "4");
                GenerateClientStoreMap(Id, storeSiteIdForAuditQueries, nexId.ToString());
            }
        }

        private void GenerateStoreTypesAndStoreProductsQueries(string path)
        {

            PairType<string, string>[] parms1 = new PairType<string, string>[1];
            parms1[0] = new PairType<string, string>("STORE_NO", storeNumber);
            string query1 = DataAccessService.PrepareVariableQuery(
            ref this.dACcs,
            StoreTypeAndStoreProductQueries.STORETYPE_QUERIES,
            parms1);

            query1 = query1 + DataAccessService.PrepareVariableQuery(
            ref this.dACcs,
            StoreTypeAndStoreProductQueries.STOREPRODUCT_QUERIES1,
            parms1);

            query1 = query1 + DataAccessService.PrepareVariableQuery(
            ref this.dACcs,
            StoreTypeAndStoreProductQueries.STOREPRODUCT_QUERIES2,
            parms1);

            query1 = query1 + DataAccessService.PrepareVariableQuery(
            ref this.dACcs,
            StoreTypeAndStoreProductQueries.STOREPRODUCT_QUERIES3,
            parms1);

            query1 = query1 + DataAccessService.PrepareVariableQuery(
            ref this.dACcs,
            StoreTypeAndStoreProductQueries.STOREPRODUCT_QUERIES4,
            parms1);

            query1 = query1 + DataAccessService.PrepareVariableQuery(
            ref this.dACcs,
            StoreTypeAndStoreProductQueries.STOREPRODUCT_QUERIES5,
            parms1);

            query1 = query1 + DataAccessService.PrepareVariableQuery(
            ref this.dACcs,
            StoreTypeAndStoreProductQueries.STOREPRODUCT_QUERIES6,
            parms1);

            query1 = query1 + DataAccessService.PrepareVariableQuery(
            ref this.dACcs,
            StoreTypeAndStoreProductQueries.STOREPRODUCT_QUERIES7,
            parms1);

            query1 = query1 + DataAccessService.PrepareVariableQuery(
            ref this.dACcs,
            StoreTypeAndStoreProductQueries.STOREPRODUCT_QUERIES8,
            parms1);

            query1 = query1 + DataAccessService.PrepareVariableQuery(
            ref this.dACcs,
            StoreTypeAndStoreProductQueries.STOREPRODUCT_QUERIES9,
            parms1);

            query1 = query1 + DataAccessService.PrepareVariableQuery(
            ref this.dACcs,
            StoreTypeAndStoreProductQueries.STOREPRODUCT_QUERIES10,
            parms1);

            query1 = query1 + DataAccessService.PrepareVariableQuery(
            ref this.dACcs,
            StoreTypeAndStoreProductQueries.STOREPRODUCT_QUERIES11,
            parms1);



            //query1 = query1.Replace("            ", "");
            query1 = query1.Replace(";", ";" + Environment.NewLine + Environment.NewLine);

            DateTime dtNow = DateTime.Now;
            string storeTypesAndProductsPath = path + "/" + storeNumber + "_StoreTypesAndProducts_" + dtNow.Ticks + ".sql";
            StreamWriter pSecWriter = File.CreateText(storeTypesAndProductsPath);

            pSecWriter.Write(query1 + Environment.NewLine);
            pSecWriter.Close();
            pSecWriter.Dispose();
            consolidated_queries_list.Add(storeTypesAndProductsPath);

        }
        private string GenerateAppVersion()
        {
            PairType<string, string>[] parms = new PairType<string, string>[3];
            ulong nexId = 1;
            string temp;
            this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.STOAPPVER_ID,
                                                            ref nexId,
                                                            out temp);
            if (nexId > 0)
                nexId = nexId - 2; // revisit this.
            return nexId.ToString();
        }

        private string GenerateStoreSiteInfo(string storeNo, string storeAppId)
        {
            ulong nexId = 1;
            string temp;
            this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.STOSITINF_ID,
                                                            ref nexId,
                                                            out temp);
            PairType<string, string>[] parms = new PairType<string, string>[7];
            parms[0] = new PairType<string, string>("SSI_ID1", nexId.ToString());
            parms[1] = new PairType<string, string>("SSI_STORENUMBER", storeNo);
            string stateNm = storeData.StoreInfo.State;

            if (stateNm != null && stateNm.Length > 2)
            {
                parms[2] = new PairType<string, string>("SSI_STATE", Commons.GetStateByName(storeData.StoreInfo.State).ToString());
                parms[4] = new PairType<string, string>("SSI_ALIAS", Commons.GetStateByName(storeData.StoreInfo.State).ToString());
            }
            else if (stateNm != null && stateNm.Length == 2)
            {
                parms[2] = new PairType<string, string>("SSI_STATE", storeData.StoreInfo.State);
                parms[4] = new PairType<string, string>("SSI_ALIAS", storeData.StoreInfo.State);
            }
            else
            {
                parms[2] = new PairType<string, string>("SSI_STATE", "");
                parms[4] = new PairType<string, string>("SSI_ALIAS", "");
            }

            parms[3] = new PairType<string, string>("SSI_COMPANYNUMBER", storeData.StoreInfo.CompanyNumber);
            //parms[4] = new PairType<string, string>("SSI_ALIAS", storeData.StoreInfo.State);
            //parms[4] = new PairType<string, string>("SSI_ALIAS", PawnUtilities.Shared.Common.GetStateByName(storeData.StoreInfo.State).ToString());
            parms[5] = new PairType<string, string>("SAV_ID1", storeAppId);
            parms[6] = new PairType<string, string>("SSI_COMPANYNAME", COMPANY_NAME);
            string query = DataAccessService.PrepareVariableQuery(
                    ref this.dAPawnSec,
                    PawnStoreSetupQueries.MERGE_PAWNSEC_STORESITEINFO,
                    parms);
            pawnsec_storesiteinfo_queries.Add(query);
            pawnsec_storesiteinfo_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "pawnsec.storesiteinfo" + ROLLBACK_SCRIPT_PREFIX2 + "'" + nexId + "'");
            return nexId.ToString();
        }

        /*        private void GenerateWorkstations(string storeNo, string ip, string nickName, string storeSiteId,
                    List<string> peripIdsList, List<string> peripIdsListForPawnWSPerips)
                {
                    //moving to load method
                    //StoreId = getStoreId(storeNo);

                    //For manager workstation
                    if (!string.IsNullOrEmpty(ip) && !this.loadRunnerInd.Checked)
                    {
                        int index = ip.LastIndexOf('.');
                        //hard coding .25 for manager work station
                        ip = ip.Substring(0, index + 1) + MGR_IP_PREFIX;
                        GenerateClientRegistry(ip, (nickName + MGR_PREFIX).ToLowerInvariant(), 1, storeSiteId);
                    }

                    string wsName = null;
                    int i = 0;
                    for (i = 0; i <= 15; i++)
                    {
                        string guid = this.generateGuid("0");

                        var paramArr = new PairType<string, string>[4];
                        paramArr[0] = new PairType<string, string>("CDW_ID", guid);
                        if (i == 0)
                        {
                            //int index = ip.LastIndexOf('.');
                            //hard coding .25 for manager work station
                            //ip = ip.Substring(0, index + 1) + MGR_IP_PREFIX;
                            if (!this.loadRunnerInd.Checked)
                                wsName = nickName + MGR_PREFIX;
                            else
                                continue;
                        }
                        else
                        {
                            wsName = nickName + WS_PREFIX + i;
                            GenerateClientRegistry("", wsName.ToLowerInvariant(), i, storeSiteId);
                            //ip = "";
                        }
                        paramArr[1] = new PairType<string, string>("CDW_NAME", wsName.ToLowerInvariant());
                        paramArr[2] = new PairType<string, string>("CDW_BRID", StoreId);
                        paramArr[3] = new PairType<string, string>("CDW_STONUM", storeNo);

                        var insWkstQuery = DataAccessService.PrepareVariableQuery(
                                ref this.dACcs,
                                PawnStoreSetupQueries.MERGE_CDOWNER_WORKSTATION,
                                paramArr);
                        // sWriter.WriteLine(insWkstQuery + ";" + Environment.NewLine); 
                        ccsowner_queries.Add(insWkstQuery);
                        ccsowner_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "cdowner.cd_workstation" + ROLLBACK_SCRIPT_PREFIX2 + "'" + guid + "'");

                        //generating workstationperipherals
                        for (int j = 0; j < peripIdsList.Count; j++)
                        {
                            GenerateWorkstationPeripherals(peripIdsList[j], guid);
                        }

                        //to generate 1:1 mappings for VF with workstations...
                        if (!this.loadRunnerInd.Checked)
                            GenerateWorkstationPeripherals(pVFDevices[i], guid);

                        //generating pawnworkstationperipherals
                        for (int j = 0; j < peripIdsListForPawnWSPerips.Count; j++)
                        {
                            GeneratePawnWorkstationPeripherals(peripIdsListForPawnWSPerips[j], guid);
                        }
                    }
                }
        */

        private void GenerateWorkstations(string storeNo, string ip, string nickName, string storeSiteId,
            List<string> peripIdsList, List<string> peripIdsListForPawnWSPerips)
        {
            //moving to load method
            //StoreId = getStoreId(storeNo);

            //For manager workstation
            if (!string.IsNullOrEmpty(ip) && !this.loadRunnerInd.Checked)
            {
                int index = ip.LastIndexOf('.');
                //hard coding .25 for manager work station
                ip = ip.Substring(0, index + 1) + MGR_IP_PREFIX;
                GenerateClientRegistry(ip, (nickName + MGR_PREFIX).ToUpperInvariant(), 1, storeSiteId);
            }

            string wsName = null;
            int receiptPrinterCnt = 1;
            int i = 0;
            for (i = 0; i <= 15; i++)
            {
                string guid = this.generateGuid("0");

                var paramArr = new PairType<string, string>[4];
                paramArr[0] = new PairType<string, string>("CDW_ID", guid);
                if (i == 0)
                {
                    //int index = ip.LastIndexOf('.');
                    //hard coding .25 for manager work station
                    //ip = ip.Substring(0, index + 1) + MGR_IP_PREFIX;
                    if (!this.loadRunnerInd.Checked)
                        wsName = nickName + MGR_PREFIX;
                    else
                        continue;
                }
                else
                {
                    wsName = nickName + WS_PREFIX + i;
                    GenerateClientRegistry("", wsName.ToUpperInvariant(), i, storeSiteId);
                    //ip = "";
                }
                paramArr[1] = new PairType<string, string>("CDW_NAME", wsName.ToUpperInvariant());
                paramArr[2] = new PairType<string, string>("CDW_BRID", StoreId);
                paramArr[3] = new PairType<string, string>("CDW_STONUM", storeNo);

                var insWkstQuery = DataAccessService.PrepareVariableQuery(
                        ref this.dACcs,
                        PawnStoreSetupQueries.MERGE_CDOWNER_WORKSTATION,
                        paramArr);
                // sWriter.WriteLine(insWkstQuery + ";" + Environment.NewLine); 
                ccsowner_queries.Add(insWkstQuery);
                ccsowner_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "cdowner.cd_workstation" + ROLLBACK_SCRIPT_PREFIX2 + "'" + guid + "'");

                //generating workstationperipherals
                for (int j = 0; j < peripIdsList.Count; j++)
                {
                    GenerateWorkstationPeripherals(peripIdsList[j], guid);
                }

                GenerateWorkstationPeripherals(pReceiptPrinterList[0], guid);
                //to generate 1:1 mappings for VF with workstations...

                //as per the requirement the VF mappings are not required
                //if (!this.loadRunnerInd.Checked)
                //    GenerateWorkstationPeripherals(pVFDevices[i], guid);

                //generating pawnworkstationperipherals
                for (int j = 0; j < peripIdsListForPawnWSPerips.Count; j++)
                {
                    GeneratePawnWorkstationPeripherals(peripIdsListForPawnWSPerips[j], guid);
                }

                GeneratePawnWorkstationPeripherals(pReceiptPrinterList[0], guid);

                if (receiptPrinterCnt == 2)
                {
                    pReceiptPrinterList.RemoveAt(0);
                    receiptPrinterCnt = 1;
                }
                else if (i > 0)
                    receiptPrinterCnt = receiptPrinterCnt + 1;
            }
        }

        //CCSOWNER.PAWNWORKSTATIONPERIPHERALS
        private void GeneratePawnWorkstationPeripherals(string peripId, string wsId)
        {
            string guid = this.generateGuid("0");
            var paramArr = new PairType<string, string>[3];
            paramArr[0] = new PairType<string, string>("WKSP_ID", guid);
            paramArr[1] = new PairType<string, string>("WKSP_PRID", peripId);
            paramArr[2] = new PairType<string, string>("WKSP_WKID", wsId);

            var insWkspMapQuery = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CCSOWNER_PAWNWORKSTATIONPERIPHERALS,
                    paramArr);
            ccsowner_pawnworkstationperipheral_queries.Add(insWkspMapQuery);
            ccsowner_pawnworkstationperipheral_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "CCSOWNER.PAWNWORKSTATIONPERIPHERALS where storeperipheralid ='" + guid + "'");
        }

        private void GenerateWorkstationPeripherals(string peripId, string wsId)
        {
            string guid = this.generateGuid("0");
            var paramArr = new PairType<string, string>[3];
            paramArr[0] = new PairType<string, string>("WKSP_ID", guid);
            paramArr[1] = new PairType<string, string>("WKSP_PRID", peripId);
            paramArr[2] = new PairType<string, string>("WKSP_WKID", wsId);

            var insWkspMapQuery = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CCSOWNER_WORKSTATIONPERIPHERALS,
                    paramArr);
            ccsowner_workstationperipheral_queries.Add(insWkspMapQuery);
            ccsowner_workstationperipheral_queries.Add(ROLLBACK_SCRIPT_PREFIX1 + "ccsowner.workstationperipherals where storeperipheralid ='" + guid + "'");
        }

        private string getStoreId(string storeNo)
        {
            //If the shop number entered is valid, call data layer
            //to get the store information
            var storeInfo = new SiteId();
            string errorCode = null;
            string errorText = null;
            var storeId = "0";
            ShopProcedures.ExecuteGetStoreInfo(CCSOWNER, this.dACcs.OracleDA,
                storeNo, ref storeInfo, out errorCode, out errorText);
            if (storeInfo != null)
            {
                storeId = storeInfo.StoreId;
            }
            return storeId;
        }

        private string validateStoreNum(string storeNo)
        {
            if (string.IsNullOrEmpty(storeNo))
                return string.Empty;
            if (storeNo.Length < 5)
                storeNo = storeNo.PadLeft(5, '0');
            return storeNo;
        }

        private void processStoreBootStrap(string storeNum)
        {
            bool errFnd = false;
            ulong storeTypeId = 0;
            PawnStoreProcedures.GetNextIdStr(ref this.dACcs, "ID", "STORETYPE", out storeTypeId);
            var paramArr = new PairType<string, string>[6];
            paramArr[0] = new PairType<string, string>("ST_ID1", storeTypeId.ToString());
            paramArr[1] = new PairType<string, string>("ST_STORENUM", storeNum);
            paramArr[2] = new PairType<string, string>("IS_TPS_SAFE", "0");
            paramArr[3] = new PairType<string, string>("IS_INTEG", "1");
            paramArr[4] = new PairType<string, string>("PAWN_PRIM", "1");
            paramArr[5] = new PairType<string, string>("TOPS_EXIST", "0");

            var insWkstQuery = DataAccessService.PrepareVariableQuery(
                    ref this.dACcs,
                    PawnStoreSetupQueries.MERGE_CCSOWNER_STORETYPE,
                    paramArr);

            if (!PawnStoreProcedures.IsStoreTypeBootStraped(ref dACcs, storeNum))
            {
                if (DataAccessService.StartTransactionBlock(CCSOWNER, "CCSOWNER CHANGE", ref dACcs))
                {

                    if (!DataAccessService.ExecuteInsertUpdateDeleteQuery(insWkstQuery, CCSOWNER, ref dACcs))
                    {
                        errFnd = true;
                    }
                    else if (this.loadRunnerInd.Checked)
                    {
                        if (storeProdcutsList.Count() == 0)
                        {
                            PawnStoreProcedures.RetrieveStoreProducts(ref dACcs, BOOTSTRAP_STORE, ref storeProdcutsList);
                        }

                        if (storeProdcutsList.Any())
                        {
                            for (int i = 0; i < storeProdcutsList.Count(); i++)
                            {
                                if (i == 49)
                                    Console.WriteLine();
                                PairType<string, string> storeProdValues = storeProdcutsList[i];
                                //STORE_TYPEID, PRODUCT_MENUID, PRODUCTID
                                var paramArr1 = new PairType<string, string>[2];
                                paramArr[0] = new PairType<string, string>("STORE_TYPEID", storeTypeId.ToString());
                                //if (!string.IsNullOrEmpty(storeProdValues.Left))
                                paramArr[1] = new PairType<string, string>("PRODUCTID", storeProdValues.Left);
                                //else
                                //paramArr[1] = new PairType<string, string>("PRODUCTID", "00033");
                                paramArr[2] = new PairType<string, string>("PRODUCT_MENUID", storeProdValues.Right);

                                var storeProdQuery = DataAccessService.PrepareVariableQuery(
                                        ref this.dACcs,
                                        PawnStoreSetupQueries.MERGE_CCSOWNER_STOREPRODUCTS,
                                        paramArr);

                                if (!DataAccessService.ExecuteInsertUpdateDeleteQuery(storeProdQuery, CCSOWNER, ref dACcs))
                                {
                                    errFnd = true;
                                    break;
                                }
                            }
                        }
                        //GenerateNextNumForLoadRun(storeNum);

                    }
                    if (errFnd)
                    {
                        DataAccessService.RollbackTransactionBlock(CCSOWNER, "CCSOWNER CHANGE", ref dACcs);
                        MessageBox.Show(
                                "Error occurred during CCSOWNER.STORETYPE, CCSOWNER.STOREPRODUCTS SQL execution.  Executing transaction roll back",
                                SETUPALERT_TXT);
                    }
                }
                if (!errFnd && this.loadRunnerInd.Checked)
                    DataAccessService.CommitTransactionBlock(CCSOWNER, "CCSOWNER CHANGE", ref dACcs);
            }

            //if (!PawnStoreProcedures.IsNextNumPopulated(ref dACcs, storeNum) &&
            //    this.loadRunnerInd.Checked)
            //if(this.loadRunnerInd.Checked)
            //{
            //    GenerateNextNumForLoadRun(storeNum);
            //}
            //else
            //{
            //    Console.WriteLine("NEXTNUM already populated for the store -->"+storeNum);
            //}

        }

        private void GenerateNextNumForLoadRun(string storeNum)
        {
            if (nextNums.Count == 0)
                PawnStoreProcedures.RetrieveNextNum(ref dACcs, BOOTSTRAP_STORE, ref nextNums);

            if (nextNums.Any())
            {
                foreach (var nextNumType in nextNums)
                {
                    if (!PawnStoreProcedures.IsNextNumPopulated(ref dACcs, storeNum, nextNumType))
                    {
                        var paramArr = new PairType<string, string>[2];
                        paramArr[0] = new PairType<string, string>("NEXTNUM_TYPE", nextNumType);
                        paramArr[1] = new PairType<string, string>("STORENUMBER", storeNum);

                        var insWkstQuery = DataAccessService.PrepareVariableQuery(
                                ref this.dACcs,
                                PawnStoreSetupQueries.MERGE_CCSOWNER_NEXTNUM,
                                paramArr);
                        cdowner_nextnum_queries.Add(insWkstQuery);
                        //cdowner_nextnum_queries.Add(ROLLBACK_SCRIPT_PREFIX1 +" CCSOWNER.NEXTNUM "+ROLLBACK_SCRIPT_PREFIX2+);
                    }
                }
            }
        }
    }
}
