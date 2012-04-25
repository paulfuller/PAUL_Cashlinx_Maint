using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Admin
{
    public partial class SecurityBarCode : CustomBaseForm
    {
        private List<BarcodeInfo> _Employees;

        public SecurityBarCode(List<BarcodeInfo> Employees)
        {
            InitializeComponent();
            _Employees = Employees;
            Setup();
        }

        private void Setup()
        {

            // temp while building out page.
            DataGridViewRowCollection dc = gvEmployees.Rows;

            foreach(BarcodeInfo barcodeInfo in _Employees)
            {
                int gvIdx = gvEmployees.Rows.Add();
                gvEmployees.Rows[gvIdx].Cells[colEmpNo.Name].Value = barcodeInfo.EmployeeID;
                gvEmployees.Rows[gvIdx].Cells[colEmployeeFirstName.Name].Value = barcodeInfo.FirstName;
                //gvEmployees.Rows[gvIdx].Cells[colEmployeeLastName.Name].Value = barcodeInfo.LastName;
                gvEmployees.Rows[gvIdx].Cells[colEncryptData.Name].Value = barcodeInfo.EncryptData;
            }            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvEmployees.Columns[colPrint.Name].Index)
            {
                string sEmployeeNumber  = Utilities.GetStringValue(gvEmployees.Rows[e.RowIndex].Cells[colEmpNo.Name].Value);
                string sEmployeeFirstName = Utilities.GetStringValue(gvEmployees.Rows[e.RowIndex].Cells[colEmployeeFirstName.Name].Value);
                //string sEmployeeLastName = Utilities.GetStringValue(gvEmployees.Rows[e.RowIndex].Cells[colEmployeeLastName.Name].Value);
                string sEncryptData = Utilities.GetStringValue(gvEmployees.Rows[e.RowIndex].Cells[colEncryptData.Name].Value);

                var confRef = SecurityAccessor.Instance.EncryptConfig;
                var dSession = GlobalDataAccessor.Instance.DesktopSession;
                //confRef.ClientConfig.ClientConfiguration.TerminalNumber;
                //Ensure that we have a valid printer to send the bar code data
                if (GlobalDataAccessor.Instance.DesktopSession.BarcodePrinter.IsValid)
                {
                    var intermecBarcodeTagPrint = 
                        new IntermecBarcodeTagPrint("",
                            Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                            IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i,
                            dSession.BarcodePrinter.IPAddress,
                            (uint)dSession.BarcodePrinter.Port, dSession);

                    intermecBarcodeTagPrint.PrintUserBarCode(sEncryptData, sEmployeeFirstName);
                }
                else
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                       "Could not send barcode security tag to printer. ");
                    }
                }

                if (SecurityAccessor.Instance.WriteUsbAuthentication(GlobalDataAccessor.Instance.DesktopSession, sEncryptData))
                {
                    if (FileLogger.Instance.IsLogDebug)
                    {
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Wrote fully encrypted string to usb drive");
                    }
#if DEBUG
                    MessageBox.Show("USB authentication data written from security bar code form");
#endif
                }
            }
        }

/*        private void testButton_Click(object sender, EventArgs e)
        {
            var confRef = SecurityAccessor.Instance.EncryptConfig;
            //confRef.ClientConfig.ClientConfiguration.TerminalNumber;
            
            // Need to pull the Printer Name from a Configuration File. Currently, passed as string below
            IntermecBarcodeTagPrint intermecBarcodeTagPrint = new IntermecBarcodeTagPrint("",
                                                            Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                                            GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                                                            IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i,
                                                            CashlinxDesktopSession.Instance.IntermecPrinterName);

            intermecBarcodeTagPrint.PrintUserBarCode("1234567890123456", "JOHN", "DOE");
        }*/

        private void doneButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cancelCloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class BarcodeInfo
    {
        public int EmployeeID;
        public string FirstName;
        public string LastName;
        public string EncryptData;
    }
}
