using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CouchConsoleApp.form;
using form.CouchConsoleApp;
using log4net;
using log4net.Config;

namespace CouchConsoleApp
{
    static class Program
    {
        private readonly static ILog log = LogManager.GetLogger(typeof(Form1));
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CouchConnector conn=new CouchConnector(Properties.Settings.Default.CouchServerName,
                Properties.Settings.Default.CouchPort,
                Properties.Settings.Default.DBName);
            XmlConfigurator.Configure();
            conn.FormBorderStyle = FormBorderStyle.FixedDialog;
            //conn.MaximizeBox = false;
            //conn.MinimizeBox = true;
            conn.StartPosition = FormStartPosition.CenterScreen;
            try
            {
                Application.Run(conn);
            }catch (Exception e)
            {
                MessageBox.Show("Critical Error occured, Application will exit.\n Please check the error log for details");
                log.Error("Application level error occured: "+e.Message);
                log.Error("Detailed trace"+e.StackTrace);
            }

            /*CouchWebTestForm couchWebTestForm=new CouchWebTestForm();
            Application.Run(couchWebTestForm);*/

        }
    }
}
