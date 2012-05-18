using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Common.Controllers.Application;
using Common.Controllers.Database.DataAccessLayer;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Network;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Config;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.String;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace DSTRViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseServiceVO couchServer;
        private DatabaseServiceVO databaseServer;
        private DatabaseServiceVO ldapServer;
        private PawnSecVO pawnSecData;
        private AuditLogger auditLogger;
        private Credentials pwnSecCred;
        private DataAccessTools pwnSecDataTools;
        private Credentials cshLnxCred;
        private DataAccessTools cshLnxDataTools;
        private EncryptedConfigContainer encConfig;
        private string curEnvString;
        private string curUserName;
        private string curPassword;

        private void cleanup()
        {
            if (pwnSecDataTools != null)
            {
                if (pwnSecDataTools.OracleDA != null && 
                    pwnSecDataTools.OracleDA.Initialized &&
                    pwnSecDataTools.OracleDA.GetConnectionStatus(PawnStoreProcedures.PAWNSEC) != 
                        OracleDataAccessor.Status.DISCONNECTED)
                {
                    pwnSecDataTools.OracleDA.DisconnectDbConnection(PawnStoreProcedures.PAWNSEC);
                }            
                pwnSecDataTools.Dispose();
                pwnSecDataTools = null;
            }

            if (cshLnxDataTools != null)
            {
                if (cshLnxDataTools.OracleDA != null &&
                    cshLnxDataTools.OracleDA.Initialized &&
                    cshLnxDataTools.OracleDA.GetConnectionStatus(PawnStoreProcedures.CCSOWNER) !=
                        OracleDataAccessor.Status.DISCONNECTED)
                {
                    cshLnxDataTools.OracleDA.DisconnectDbConnection(PawnStoreProcedures.CCSOWNER);
                }
                cshLnxDataTools.Dispose();
                cshLnxDataTools = null;
            }
        }

        private bool loadEnvironmentData(string environmentStr, out string errTxt)
        {
            errTxt = string.Empty;
            var rt = true;
            if (string.IsNullOrEmpty(environmentStr))
            {
                errTxt = "Invalid environment string.";
                return (false);
            }

            //Setup file logger
            FileLogger.Instance.initializeLogger(
                string.Format("logs/dstr_viewer_{0}.log", DateTime.Now.Ticks),
                DefaultLoggerHandlers.defaultLogLevelCheckHandler, 
                DefaultLoggerHandlers.defaultLogLevelGenerator, 
                DefaultLoggerHandlers.defaultDateStampGenerator, 
                DefaultLoggerHandlers.defaultLogMessageHandler, 
                DefaultLoggerHandlers.defaultLogMessageFormatHandler);
            FileLogger.Instance.setEnabled(true);
            FileLogger.Instance.setLogLevel(LogLevel.DEBUG);

            //Pwn sec tuples (user name, password, host, port, schema, service)
            var internalStorage = new Dictionary<string, Tuple<string, string, string, string, string, string>>(8);

            //Construct data storage at runtime and select based on environment string
            internalStorage.Add("CLXD3", new Tuple<string, string, string, string, string, string>
                (@"Ny2VIxVYqnA=", @"jZekk5GlbvfnRVTll7RpCw==", @"5HYH35IsmBLxuFKgDA0deV4cSI9w/aeE", @"c5oa+iWxTPs=", @"Ny2VIxVYqnA=", @"tN2vG1Y6pleVpj7+YIrMdxtI3uI0kDL1"));
            internalStorage.Add("CLXI", new Tuple<string, string, string, string, string, string>
                (@"Ny2VIxVYqnA=", @"jZekk5GlbvfnRVTll7RpCw==", @"1xaOn6Ot6HRjXbIPl7E2WJ3Bs9SmWpEy", @"c5oa+iWxTPs=", @"Ny2VIxVYqnA=", @"7GS8RS4GC4MRrLvSkAHG8w=="));
            internalStorage.Add("CLXT", new Tuple<string, string, string, string, string, string>
                (@"Ny2VIxVYqnA=", @"jZekk5GlbvfnRVTll7RpCw==", @"bYtNZ/PbgPkj30psRLKPF+5CvrNdg5WA", @"07s4aRvDFLs=", @"Ny2VIxVYqnA=", @"BBBxdZodCA0RrLvSkAHG8w=="));
            internalStorage.Add("CLXT2", new Tuple<string, string, string, string, string, string>
                (@"Ny2VIxVYqnA=", @"jZekk5GlbvfnRVTll7RpCw==", @"bYtNZ/PbgPkj30psRLKPF+5CvrNdg5WA", @"07s4aRvDFLs=", @"Ny2VIxVYqnA=", @"Ny/sG2mylyCVpj7+YIrMdxtI3uI0kDL1"));
            internalStorage.Add("CLXP", new Tuple<string, string, string, string, string, string>
                (@"Ny2VIxVYqnA=", @"jZekk5GlbvfnRVTll7RpCw==", @"bIyV3M7QftbxuFKgDA0deV4cSI9w/aeE", @"07s4aRvDFLs=", @"Ny2VIxVYqnA=", @"ZisF3qmLAEMRrLvSkAHG8w=="));


            //Grab the tuple and make the proper connections
            if (CollectionUtilities.isNotEmptyContainsKey(internalStorage, environmentStr))
            {
                var cxnInfo = internalStorage[environmentStr];
                var privKey = Common.Properties.Resources.PrivateKey;
                if (cxnInfo != null)
                {
                    //Create pawn security connection credentials
                    this.pwnSecCred = new Credentials
                                      {
                                          UserName = StringUtilities.Decrypt(cxnInfo.Item1, privKey, true),
                                          PassWord = StringUtilities.Decrypt(cxnInfo.Item2, privKey, true),
                                          DBHost = StringUtilities.Decrypt(cxnInfo.Item3, privKey, true),
                                          DBPort = StringUtilities.Decrypt(cxnInfo.Item4, privKey, true),
                                          DBService = StringUtilities.Decrypt(cxnInfo.Item6, privKey, true),
                                          DBSchema = StringUtilities.Decrypt(cxnInfo.Item5, privKey, true)
                                      };

                    //Create pawn security data access tools
                    this.pwnSecDataTools = DataAccessService.CreateDataAccessTools();
                    if (!DataAccessService.Connect(PawnStoreProcedures.PAWNSEC, this.pwnSecCred, DataAccessTools.ConnectMode.MULTIPLE, DataAccessTools.LogMode.DEBUG, ref this.pwnSecDataTools))
                    {
                        errTxt = "Could not connect to pawn security database.";
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, errTxt);
                        }
                        rt = false;
                    }
                    else
                    {
                        //Create pawn sec vo
                        this.pawnSecData = new PawnSecVO();
                        string decryptKey;

                        //Get primary oracle connection credentials
                        if (!PawnStoreProcedures.GetAllPawnSecData(ref this.pwnSecDataTools, ref this.pawnSecData, out decryptKey))
                        {
                            errTxt = "Could not load pawn security data for selected environment";
                            if (FileLogger.Instance.IsLogError)
                            {
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, errTxt);
                            }
                            rt = false;
                        }
                        else
                        {
                            //Get the oracle server info  
                            this.databaseServer = this.pawnSecData.DatabaseServiceList.Find(
                                vo => (string.Equals(vo.ServiceType, EncryptedConfigContainer.ORACLEKEY, StringComparison.Ordinal)));

                            //Connect to the primary Oracle server
                            this.cshLnxCred = new Credentials
                                              {
                                                  UserName = StringUtilities.Decrypt(this.databaseServer.DbUser, decryptKey, true),
                                                  PassWord = StringUtilities.Decrypt(this.databaseServer.DbUserPwd, decryptKey, true),
                                                  DBHost = StringUtilities.Decrypt(this.databaseServer.Server, decryptKey, true),
                                                  DBPort = StringUtilities.Decrypt(this.databaseServer.Port, decryptKey, true),
                                                  DBService = StringUtilities.Decrypt(this.databaseServer.AuxInfo, decryptKey, true),
                                                  DBSchema = StringUtilities.Decrypt(this.databaseServer.Schema, decryptKey, true)
                                              };

                            this.cshLnxDataTools = DataAccessService.CreateDataAccessTools();
                            if (!DataAccessService.Connect(PawnStoreProcedures.CCSOWNER, this.cshLnxCred, DataAccessTools.ConnectMode.MULTIPLE, 
                                DataAccessTools.LogMode.DEBUG, ref this.cshLnxDataTools))
                            {
                                errTxt = "Could not connect to primary Cashlinx database";
                                if (FileLogger.Instance.IsLogError)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, errTxt);
                                }
                                rt = false;
                            }
                            else
                            {
                                //Get the couch server info
                                this.couchServer = this.pawnSecData.DatabaseServiceList.Find(
                                    vo => (string.Equals(vo.ServiceType, EncryptedConfigContainer.COUCHDBKEY, StringComparison.Ordinal)));
                                //Get the LDAP server info
                                this.ldapServer = this.pawnSecData.DatabaseServiceList.Find(
                                    vo => (string.Equals(vo.ServiceType, EncryptedConfigContainer.LDAPKEY, StringComparison.Ordinal)));

                                //Setup the LDAP connection
                                this.encConfig = new EncryptedConfigContainer(
                                    Common.Properties.Resources.PrivateKey,
                                    this.pawnSecData.GlobalConfiguration.DataPublicKey,
                                    "00152",   //Hard coded to 00152 for pawn security retrieval - GJL 05/08/2012
                                    this.pawnSecData,
                                    PawnSecApplication.None,
                                    true);

                                var conf = this.encConfig;
                                string loginDN;
                                string searchDN;
                                string userIdKey;
                                string userPwd;
                                string pwdPolicyCN;
                                var ldapService =
                                    conf.GetLDAPService(
                                        out loginDN,
                                        out searchDN,
                                        out userIdKey,
                                        out userPwd,
                                        out pwdPolicyCN);
                                if (ldapService != null && FileLogger.Instance.IsLogDebug)
                                {
                                    FileLogger.Instance.logMessage(
                                        LogLevel.DEBUG, this, "- Connecting to LDAP server:{0}{1}",
                                        System.Environment.NewLine, conf.DecryptValue(ldapService.Server));
                                }

                                //Connect to the LDAP server
                                PawnLDAPAccessor.Instance.InitializeConnection(
                                    conf.DecryptValue(ldapService.Server),
                                    conf.DecryptValue(ldapService.Port),
                                    loginDN,
                                    userPwd,
                                    pwdPolicyCN,
                                    searchDN,
                                    userIdKey);

                                if (PawnLDAPAccessor.Instance.State != PawnLDAPAccessor.LDAPState.CONNECTED)
                                {
                                    errTxt = "Could not connect to the LDAP Server";
                                    if (FileLogger.Instance.IsLogError)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, errTxt);
                                    }
                                    rt = false;
                                }
                                else
                                {
                                    //Authenticate the user
                                    var attemptCount = 1;
                                    DateTime pwdLastMod;
                                    bool lockedOut;
                                    string[] pwdHistory;
                                    string displayName;
                                    if (!PawnLDAPAccessor.Instance.AuthorizeUser(
                                        this.curUserName, this.curPassword, ref attemptCount,
                                        out pwdLastMod, out pwdHistory, out displayName, out lockedOut))
                                    {
                                        errTxt = "Could not verify user name and password";
                                        if (FileLogger.Instance.IsLogError)
                                        {
                                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, errTxt);
                                        }
                                        rt = false;
                                    }
                                }
                            }
                        }                        
                    }
                }
                else
                {
                    errTxt = "Could not find valid connection info in internal environment data.";
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, errTxt);
                    }
                    rt = false;
                }
            }
            else
            {
                errTxt = "Environment string not found in internal environment data.";
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, errTxt);
                }
                rt = false;
            }

            return (rt);

        }

        private bool setupInternalData(string userName, string password, out string errTxt)
        {
            errTxt = string.Empty;
            if (!loadEnvironmentData(this.curEnvString, out errTxt))
            {
                return (false);
            }

            return (true);
        }


        public MainWindow()
        {
            //Check to ensure that only one DSTRViewer is running at a time
            bool appStarted;
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            using (new Mutex(true, "DSTRViewer", out appStarted))
            {
                if (appStarted)
                {
                    InitializeComponent();
                    this.curEnvString = string.Empty;
                    this.curUserName = string.Empty;
                    this.curPassword = string.Empty;
                    this.encConfig = null;
                    return;
                }
                var currentProcess = Process.GetCurrentProcess();
                foreach (var process in Process.GetProcessesByName(currentProcess.ProcessName))
                {
                    if (process.Id == currentProcess.Id)
                    {
                        MessageBox.Show(
                            "DSTRViewer is already running.  Please click OK.",
                            "Application Started",
                            MessageBoxButton.OK, MessageBoxImage.Stop);
                        Application.Current.Shutdown();
                        this.Close();
                        return;
                    }
                }                    
            }

            //If we are still here, something is wrong, kill the application
            MessageBox.Show(
                "Invalid DSTRViewer process response.  Killing the application.",
                "Oooooops - Something is Wrong Here", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Application.Current.Shutdown();
            this.Close();            
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.cleanup();
            this.Close();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            //Ensure the user name / password are valid strings
            if (string.IsNullOrEmpty(this.curUserName))
            {
                MessageBox.Show("Please enter a valid user name.");
                return;
            }
            if (string.IsNullOrEmpty(this.curPassword))
            {
                MessageBox.Show("Please enter a valid password");
                return;
            }
            var procMsg = new ProcessingMessage("*** VALIDATING LOGIN ***");
            procMsg.Show();
            //Load pawn security data and authenticate
            string errTxt;
            if (!setupInternalData(this.curUserName, this.curPassword, out errTxt))
            {
                procMsg.Hide();
                MessageBox.Show(string.Format("Could not log in to the environment chosen. Error: {0}", errTxt));
                Application.Current.Shutdown(3);
                this.Close();
                return;
            }
            //Set and show the viewer window
            procMsg.Hide();
            var viewer = new DSTRViewerWindow(this.curEnvString, this.curUserName);
            viewer.PawnSecData = this.pawnSecData;
            viewer.EncryptedConfig = this.encConfig;
            viewer.CouchServer = this.couchServer;
            viewer.DatabaseServer = this.databaseServer;
            viewer.CshLnxCred = this.cshLnxCred;
            viewer.CshLnxDataTools = this.cshLnxDataTools;
            viewer.PwnSecCred = this.pwnSecCred;
            viewer.PwnSecDataTools = this.pwnSecDataTools;
            var res = viewer.ShowDialog();
            if (res == false)
            {
                this.Close();
            }
        }

        private void environmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.environmentComboBox.SelectedIndex > 0)
            {
                var comboItem = this.environmentComboBox.Items[this.environmentComboBox.SelectedIndex];
                if (comboItem != null)
                {
                    var itemStr = comboItem.ToString();
                    if (!string.IsNullOrEmpty(itemStr))
                    {
                        var colonIdx = itemStr.LastIndexOf(@":", StringComparison.Ordinal);
                        if (colonIdx > -1)
                        {
                            this.curEnvString = itemStr.Substring(colonIdx + 1).TrimStart(' ');
                        }
                    }
                }
            }
        }

        private void DSTRViewer_Initialized(object sender, EventArgs e)
        {

        }

        private void DSTRViewer_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.prodRestrict)
            {
                environmentComboBox.Items.Clear();
                var newItem = new ComboBoxItem()
                              {
                                  Content = "             --- PRODUCTION ---"
                              };
                environmentComboBox.Items.Add(newItem);
                environmentComboBox.SelectedIndex = 0;
                environmentComboBox.UpdateLayout();
                environmentComboBox.IsEnabled = false;
                this.curEnvString = 
                    (!string.IsNullOrEmpty(Properties.Settings.Default.prodEnv)) ? 
                        Properties.Settings.Default.prodEnv : "CLXP";
            }
        }

        private void userNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.userNameTextBox.Text))
            {
                this.curUserName = this.userNameTextBox.Text;
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.passwordBox.Password))
            {
                this.curPassword = this.passwordBox.Password;
            }

        }
    }
}
