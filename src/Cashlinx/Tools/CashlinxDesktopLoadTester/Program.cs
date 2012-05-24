using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CashlinxDesktopLoadTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Phase2LoadTestForm());
            }
            catch (Exception eX)
            {
                MessageBox.Show("Exception thrown: " + eX.Message + ", " + eX.StackTrace);
            }
        }
    }
}
