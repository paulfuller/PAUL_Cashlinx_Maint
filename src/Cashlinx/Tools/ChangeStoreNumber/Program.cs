using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ChangeStoreNumber
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace);
            }
        }
    }
}
