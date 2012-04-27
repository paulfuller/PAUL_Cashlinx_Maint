/*************************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Admin
 * Class:           Resets
 * 
 * Description      Reloads the Thermal Printer Barcode Formats
 * 
 * History
 * David D Wise, Initial Development
 * 
 *************************************************************************************/

using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Admin
{
    public partial class Resets : Form
    {
        public Resets()
        {
            InitializeComponent();
        }

        private void buttonReloadPrintFormats_Click(object sender, EventArgs e)
        {            
            var cds = GlobalDataAccessor.Instance.DesktopSession;
            var intermecPrinterIp = cds.BarcodePrinter.IPAddress;
            var intermecPrinterPort = cds.BarcodePrinter.Port;
            const IntermecBarcodeTagPrint.PrinterModel intermecPrinter = IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i;
            if (!cds.BarcodePrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "Thermal Security Tag", "Cannot determine thermal printer ip address");
                }
                MessageBox.Show("Unable to find Intermec printer.  Cannot load printer formats.",
                                "Pawn Application Message",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                this.Close();
            }
            string msg;
            if (PrintingUtilities.SendASCIIStringToPrinter(
                    intermecPrinterIp,
                    (uint)intermecPrinterPort,
                    Common.Properties.Resources.IPL_Print_Formats, out msg))
            {
                MessageBox.Show(
                        "Intermec EasyCoder PM4i formats loaded to: "
                        + Environment.NewLine
                        + intermecPrinter + ".",
                        "Intermec EasyCoder PM4i Setup",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                        "Intermec EasyCoder PM4i formats failed to load to "
                        + Environment.NewLine
                        +  intermecPrinter + ".",
                        "Intermec EasyCoder PM4i Setup",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /*private void testButton_Click(object sender, EventArgs e)
        {*/
            /*IntermecBarcodeTagPrint mySetup = new IntermecBarcodeTagPrint("LePage Coffees",
                                                    Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                                    GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                                                    IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i,
                                                    Properties.Settings.Default.IntermecBarcodePrinter);
            bool bError;
            string sMessage;
            PawnItem pawnItem = new PawnItem();
            pawnItem.CaccLevel = -1;
            pawnItem.QuickInformation = new QuickCheck()
            {
                Quantity = 1,
                SerialNumber = "123"
            };
            pawnItem.CategoryCode = 1110;
            pawnItem.GunNumber = 0;
            pawnItem.IsJewelry = false;
            pawnItem.IsGun = false;
            pawnItem.ItemReason = PawnItemReason.BLNK;
            pawnItem.ItemAmount = 89.95M;
            pawnItem.MerchandiseType = "";
            pawnItem.mDocNumber = 1711;
            pawnItem.mDocType = "L";
            pawnItem.mYear = 9;
            pawnItem.mStore = 6016;
            pawnItem.mItemOrder = 1;
            pawnItem.PawnStatus = PawnLoanStatus.IP;
            pawnItem.PfiAssignmentType = PfiAssignment.Normal;
            pawnItem.PfiTags = 1;
            pawnItem.RefurbNumber = 0;
            pawnItem.RetailPrice = 89.95M;
            pawnItem.TempStatus = StateStatus.PFI;
            pawnItem.TicketDescription = "NUG DROP; Y/G; 08 KT.; APPRX 12 GRM; FLORIDA SHAPED;";

            pawnItem.Attributes = new List<ItemAttribute>();
            ItemAttribute itemAttribute = new ItemAttribute();
            itemAttribute.Answer = new Answer()
            {
                AnswerCode = 50,
                AnswerText = ""
            };

            pawnItem.Attributes.Add(itemAttribute);

            string sErrorCode;
            string sErrorText;

            mySetup.PrintTag(DateTime.Now,
                             pawnItem,
                             false,
                             false,
                             true,
                             true,
                             false,
                             IntermecBarcodeTagPrint.TagMedias.COMBO,
                             out sErrorCode,
                             out sErrorText);*/
        //}

        /*public static void PlaySound(Stream msFileName)
        {*/
           /* try
            {
                System.Media.SoundPlayer m = new System.Media.SoundPlayer();
                m.Stream = msFileName;
                m.PlaySync();
            }
            catch
            {
            }*/
        //}

    }
}
