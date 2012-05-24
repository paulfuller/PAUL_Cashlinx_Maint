/*************************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Admin
 * Class:           AdminSection
 * 
 * Description      Admin Main Screen
 * 
 * History
 * David D Wise, Initial Development
 * 
 *************************************************************************************/

using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Admin
{
    public partial class AdminSection : Form
    {
        public AdminSection()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonReloadPrintFormats_Click(object sender, EventArgs e)
        {
            string msg;
            if (GlobalDataAccessor.Instance.DesktopSession.BarcodePrinter.IsValid &&
                PrintingUtilities.SendASCIIStringToPrinter(
                 GlobalDataAccessor.Instance.DesktopSession.BarcodePrinter.IPAddress,
                 (uint)GlobalDataAccessor.Instance.DesktopSession.BarcodePrinter.Port,
                 Common.Properties.Resources.IPL_Print_Formats, out msg))
            {
                MessageBox.Show(
                    "Intermec EasyCoder PM4i formats loaded to: "
                    + Environment.NewLine
                    + GlobalDataAccessor.Instance.DesktopSession.BarcodePrinter + ".",
                    "Intermec EasyCoder PM4i Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
                MessageBox.Show(
                    "Intermec EasyCoder PM4i formats failed to load to "
                    + Environment.NewLine
                    + GlobalDataAccessor.Instance.DesktopSession.BarcodePrinter + ".",
                    "Intermec EasyCoder PM4i Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }

        private void buttonTerminalEmulator_Click(object sender, EventArgs e)
        {
            TerminalClient UForm = null;
            if ((UForm = (TerminalClient)IsFormAlreadyOpen(typeof(TerminalClient))) == null)
            {
                UForm = new TerminalClient();
                UForm.Show();
            }
            else
            {
                MessageBox.Show(
                    "Only one instance of Terminal Emulator is allowed.",
                    "Terminal Emulator",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private static Form IsFormAlreadyOpen(Type FormType)
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == FormType)
                    return OpenForm;
            }
            return null;
        }

        private void btnEmployeeBarcodeTag_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                    "Not yet implemented.",
                    "Employee Barcode Tag",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }
    }
}
