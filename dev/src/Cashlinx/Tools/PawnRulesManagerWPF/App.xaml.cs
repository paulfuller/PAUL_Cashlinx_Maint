using System;
using System.Reflection;
using System.Text;
using System.Windows;
using Common.Controllers.Network;
using Common.Controllers.Security;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using PawnRulesManagerWPF.Views;

namespace PawnRulesManagerWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static UserLogin _userLogin = null;
        public static RulesTreeView RulesTreeView { get; set; }
        public static UserLogin UserLogin
        {
            get
            {
                if(_userLogin == null)
                {
                    _userLogin = new UserLogin();
                }
                return _userLogin;
            }
            set { _userLogin = value; }
        }

        private const string DEVHASHKEYPRE = "-1-1-1";
        private const string DEVHASHKEYPOST = "1-1-1-";

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                if (LoginUser())
                {
                    //ShutdownMode = ShutdownMode.OnMainWindowClose;
                    var w = new MainWindow();
                    this.MainWindow = w;
                    w.ShowDialog();
                }
                else
                {
                    App.Current.Shutdown();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("An unrecoverable error has occurred.  Please try restarting the app.  Error: " 
                    + ex.Message
                    + "Stack: "
                    + ex.StackTrace.ToString());
                App.Current.Shutdown();
                FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Business Rules Manager has crashed: " + ex.Message);
            }
        }

        private void InitPawnSec()
        {
            string key = PawnRulesManagerWPF.Properties.Resources.PrivateKey;
            
            string dbHost = StringUtilities.Decrypt(
                PawnRulesManagerWPF.Properties.Settings.Default.PawnSecDBHost, key, true);
            string dbPassword = StringUtilities.Decrypt(
               PawnRulesManagerWPF.Properties.Settings.Default.PawnSecDBPassword, key, true);
            string dbPort = StringUtilities.Decrypt(
                PawnRulesManagerWPF.Properties.Settings.Default.PawnSecDBPort, key, true);
            string dbSchema = StringUtilities.Decrypt(
                PawnRulesManagerWPF.Properties.Settings.Default.PawnSecDBSchema, key, true);
            string dbService = StringUtilities.Decrypt(
                PawnRulesManagerWPF.Properties.Settings.Default.PawnSecDBService, key, true);
            string dbUser = StringUtilities.Decrypt(
                PawnRulesManagerWPF.Properties.Settings.Default.PawnSecDBUser, key, true);

            if (!(string.IsNullOrEmpty(dbHost) || string.IsNullOrEmpty(dbPassword) ||
                string.IsNullOrEmpty(dbPort) || string.IsNullOrEmpty(dbSchema) ||
                string.IsNullOrEmpty(dbService) || string.IsNullOrEmpty(dbUser)))
            {
                // create connection with PawnSec
                SecurityAccessor.Instance.InitializeConnection(
                    dbHost,
                    dbPassword,
                    dbPort,
                    dbSchema,
                    dbService,
                    dbUser);
            }

            SecurityAccessor.Instance.RetrieveSecurityData(Common.Properties.Resources.PrivateKey, ComputeAppHash(), true, PawnSecApplication.Cashlinx);

            if (PawnLDAPAccessor.Instance.State ==
                PawnLDAPAccessor.LDAPState.DISCONNECTED)
            {
                string loginDN;
                string pwdPolicyCN;
                string searchDN;
                string userIdKey;
                string userPwd;

                try
                {

                    var conf = SecurityAccessor.Instance.EncryptConfig;
                    var ldapService =
                        conf.GetLDAPService(
                            out loginDN,
                            out searchDN,
                            out userIdKey,
                            out userPwd,
                            out pwdPolicyCN);
                    PawnLDAPAccessor.Instance.InitializeConnection(
                        conf.DecryptValue(ldapService.Server),
                        conf.DecryptValue(ldapService.Port),
                        loginDN,
                        userPwd,
                        pwdPolicyCN,
                        searchDN,
                        userIdKey);

                }
                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException("Exception thrown in PerformLDAPAuthentication" + ex.Message, new ApplicationException("PerformLDAPAuthentication Exception", ex));
                }
            }
        }

        private bool LoginUser()
        {
            int attemptCount = 0;
            bool lockedOut;
            //bool successLogin = false;
            //this.Hide();
            App.UserLogin.ShowDialog();

            if(App.UserLogin.DialogResult == false)
            {
                return false;
            }

            while(App.UserLogin.DialogResult == true && attemptCount < 4)
            {

                InitPawnSec();

                string username = App.UserLogin.UserName;
                string password = App.UserLogin.Password;
                var pawnLDAPAccessor = PawnLDAPAccessor.Instance;

                if (pawnLDAPAccessor.State == PawnLDAPAccessor.LDAPState.CONNECTED)
                {
                    try
                    {
                        DateTime initialLastModifiedDate;
                        string userDisplayName;
                        string[] pwdHistory;
                        if (pawnLDAPAccessor.AuthorizeUser(
                            username,
                            password,
                            ref attemptCount,
                            out initialLastModifiedDate,
                            out pwdHistory,
                            out userDisplayName,
                            out lockedOut))
                        {
                            //this.Show();                            
                            return true;
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Username or password entered is incorrect.");
                            App.UserLogin = new UserLogin();
                            App.UserLogin.ShowDialog();
                        }

                    }
                    catch (Exception eX)
                    {
                        BasicExceptionHandler.Instance.AddException("Exception thrown in PerformLDAPAuthentication" + eX.Message, new ApplicationException("PerformLDAPAuthentication Exception", eX));
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Only using pre-canned MD5 for now
        /// TODO: Finalized MD5 implementation in build 7.0
        /// </summary>
        /// <returns></returns>
        public static string ComputeAppHash()
        {
            var ver = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var rt = StringUtilities.GenerateRawMD5Hash(ver, Encoding.ASCII);
            return (rt);
        }

    }
}
