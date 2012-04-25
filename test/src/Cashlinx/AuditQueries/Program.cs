using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AuditQueries.Forms;
using AuditQueries.Properties;
using System.IO;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility.String;

namespace AuditQueries
{
    static class Program
    {
        /// <summary>
        /// Hash the version of the assembly executing the program
        /// </summary>
        /// <returns></returns>
        private static string ComputeAppHash()
        {
            var ver = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var rt = StringUtilities.GenerateRawMD5Hash(ver, Encoding.ASCII);
            return (rt);
        }

        private static void RunApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                // decrypt pawnsec information from config file
                var pMesg = new ProcessingMessage("** AUDIT QUERY APP INIT **", 4000);
                pMesg.Show();
                string key = Resources.PrivateKey;

                string dbHost = StringUtilities.Decrypt(Settings.Default.PawnSecDBHost, key, true);
                string dbPassword = StringUtilities.Decrypt(Settings.Default.PawnSecDBPassword, key, true);
                string dbPort = StringUtilities.Decrypt(Settings.Default.PawnSecDBPort, key, true);
                string dbSchema = StringUtilities.Decrypt(Settings.Default.PawnSecDBSchema, key, true);
                string dbService = StringUtilities.Decrypt(Settings.Default.PawnSecDBService, key, true);
                string dbUser = StringUtilities.Decrypt(Settings.Default.PawnSecDBUser, key, true);

                if (!(string.IsNullOrEmpty(dbHost) || string.IsNullOrEmpty(dbPassword) || string.IsNullOrEmpty(dbPort) ||
                      string.IsNullOrEmpty(dbSchema) || string.IsNullOrEmpty(dbService) || string.IsNullOrEmpty(dbUser)))
                {
                    //Update message
                    pMesg.Message = "** AUDIT QUERY APP SECURITY INIT **";

                    // create connection with PawnSec
                    SecurityAccessor.Instance.InitializeConnection(dbHost, dbPassword, dbPort, dbSchema, dbService, dbUser);

                    // retrieve data from PawnSec
                    if (!SecurityAccessor.Instance.RetrieveSecurityData(key, ComputeAppHash(), true, PawnSecApplication.AuditQueries))
                    {
                        pMesg.Close();
                        pMesg.Dispose();
                        //TODO: Log error and report exception
                        //No security data means this machine is not allowed to access Cashlinx
                        //Fail immediately.
                    }
                    //Otherwise, the machine is now authenticated to run AuditQueries, proceed with execution
                    else
                    {
                        pMesg.Close();
                        pMesg.Dispose();
                        Application.Run(new AuditQueriesForm());
                    }
                }
                else
                {
                    pMesg.Close();
                    pMesg.Dispose();
                }
            }
            catch(Exception eX)
            {
                MessageBox.Show("Exception caught in CashlinxDesktop.Program during Application.Run: " + "\nMessage    : " + eX.Message +
                                "\nStack Trace: " + eX.StackTrace + "\nTarget Site: " + eX.TargetSite + "\nSource     : " + eX.Source +
                                "\nData       :  " + eX.Data + "\nTerminating Application!", "Application Exception", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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
            using (var mutex = new Mutex(true, "AUDIT QUERIES APP", out appStarted))
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
                            MessageBox.Show("AuditQueries is already running.  Please click OK.",
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
