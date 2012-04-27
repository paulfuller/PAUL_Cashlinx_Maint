using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility.String;

namespace Support
{
    public static class Program
    {
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

        public static void RunApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                var pwnSecMsg = new ProcessingMessage("* SUPPORT APP SECURITY LOADING *");
                pwnSecMsg.Show();
                // decrypt pawnsec information from config file
                string key = Properties.Resources.PrivateKey;

                string dbHost = StringUtilities.Decrypt(
                    Properties.Settings.Default.PawnSecDBHost, key, true);
                string dbPassword = StringUtilities.Decrypt(
                    Properties.Settings.Default.PawnSecDBPassword, key, true);
                string dbPort = StringUtilities.Decrypt(
                    Properties.Settings.Default.PawnSecDBPort, key, true);
                string dbSchema = StringUtilities.Decrypt(
                    Properties.Settings.Default.PawnSecDBSchema, key, true);
                string dbService = StringUtilities.Decrypt(
                    Properties.Settings.Default.PawnSecDBService, key, true);
                string dbUser = StringUtilities.Decrypt(
                    Properties.Settings.Default.PawnSecDBUser, key, true);

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

                    pwnSecMsg.Message = "* SUPPORT APP SECURITY PROCESSING *";
                    // retrieve data from PawnSec
                    if (!SecurityAccessor.Instance.RetrieveSecurityData(key, ComputeAppHash(), true, PawnSecApplication.Support))
                    {
                        //SupportPawnSecurityAccessor.Instance.Close();
                        //Application.Exit();
                        pwnSecMsg.Close();
                        pwnSecMsg.Dispose();
                    }
                    else
                    {
                        pwnSecMsg.Close();
                        pwnSecMsg.Dispose();
                        Application.Run(new Support.Forms.SupportAppDesktop());
                    }
                }
            }
            catch (Exception eX)
            {
                MessageBox.Show("Exception caught in CashlinxPawnSupportApp.Program during Application.Run: " +
                    "\nMessage    : " + eX.Message +
                    "\nStack Trace: " + eX.StackTrace +
                    "\nTarget Site: " + eX.TargetSite +
                    "\nSource     : " + eX.Source +
                    "\nData       :  " + eX.Data +
                    "\nTerminating Application!", "Application Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ParseArgsForConfigFile(args);

            bool appStarted = true;
            using (Mutex mutex = new Mutex(true, "Cashlinx Pawn Support App", out appStarted))
            {
                if (appStarted)
                {
                    RunApplication();
                }
                else
                {
                    Process currentProcess = Process.GetCurrentProcess();

                    foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
                    {
                        if (process.Id == currentProcess.Id)
                        {
                            MessageBox.Show("Cashlinx pawn support app is already running.  Please click OK.",
                                            "Application Started",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Stop);
                            break;
                        }
                    }
                }
            }
        }

        private static void ParseArgsForConfigFile(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(args[0]))
            {
                return;
            }

            string configFile = Path.GetFullPath(args[0]);
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", configFile);
        }
    }
}
