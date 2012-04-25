using Common.Controllers.Application;
using Common.Libraries.Forms;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Pawn.Forms.GunUtilities.GunBook;

namespace Pawn.Logic.PrintQueue
{
    /// <summary>
    /// This class get the list of gun records as input and prints them.
    /// </summary>
    /// Sreeny Chintha Initial version 02/05/10
    public partial class PrintGunBook : Form
    {
        const int _FormOriginalRowY = 150;
        private DataRowCollection records;
        private TableLayoutPanel tblOriginal;
        private string reportTitle = "";
        private Bitmap bitMap = null;
        private const string PRINTER_NAME = "PAWN_LEXMARK" ;

        public PrintGunBook(DataTable gunBookData,string reportTitle)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            if (gunBookData != null)
            {
                records = gunBookData.Rows;
                this.reportTitle = reportTitle;
            }
        }

        
        private void PrintGunBook_Load(object sender, EventArgs e)
        {
           if (records.Count > 0)
            {
                ProcessingMessage myForm = new ProcessingMessage("Please wait while we generate report.");
                myForm.TopMost = true;
                this.rpttitle.Text = this.reportTitle;
                Point p = new Point((ClientRectangle.Width - rpttitle.Width) / 2,this.rpttitle.Location.Y);
                this.rpttitle.Location = p;
                myForm.Show();
                PrintQueue();
                FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "**********  Print Queue Compelted :" + DateTime.Now);
                myForm.Close();
                myForm.Dispose();
                MessageBox.Show("Printing Complete", "Gun Book Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No records available to print", "Gun Book Reports", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Close();
        }

        public void PrintQueue()
        {
            tblOriginal = tlpRecords;
            Cursor = Cursors.WaitCursor;
            //NOTE: This causes the application to process windows events asynchronously from this thread of execution.
            //NOTE: (cont)This behavior can lead to many problems.  Do not use Application.DoEvents()
            //Application.DoEvents();
            //NOTE: End of note

            string sStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            string sRunDate = String.Format("{0:MM/dd/yyyy HH:mm:ss}", DateTime.Now);
            string sAsOfDate = String.Format("{0:MM/dd/yyyy HH:mm:ss}", DateTime.Now);
            
            int iRecords = records.Count;

            storeValueLabel.Text = sStoreNumber;
            runValueLabel.Text = sRunDate;
            int currentPageNumber = -1;
            int tblCurrentRowIndex = 0;
            int nextPageNumber = -1;
            tlpRecords.Visible = false;
            for (int i = 0; i < iRecords; i++)
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "********** Processing record  :  "+i +" Started.....  "+ DateTime.Now);
                DataRow dataRow = records[i];
                currentPageNumber = Utilities.GetIntegerValue(Utilities.GetStringValue(dataRow["GUN_PAGE"], ""));
                // Create a row and add to Tablelayoutpanel
                CreatePrintRow(dataRow, tblCurrentRowIndex + 3);
                // if next record is on another page , print current page.
                if (i + 1 != iRecords)
                {
                    nextPageNumber = Utilities.GetIntegerValue(Utilities.GetStringValue(records[i + 1]["GUN_PAGE"], ""));
                }

                if ((nextPageNumber != -1 && currentPageNumber != nextPageNumber) || (i + 1) == iRecords)
                {
                    pageNumberPlace.Text = String.Format(currentPageNumber + "");
                    this.Height = _FormOriginalRowY + tlpRecords.Height + 5;
                    //NOTE: This causes the application to process windows events asynchronously from this thread of execution.
                    //NOTE: (cont)This behavior can lead to many problems.  Do not use Application.DoEvents()
                    //Application.DoEvents();
                    //NOTE: End of note

                    if ((nextPageNumber != -1 && currentPageNumber != nextPageNumber))
                    {
                        Height = Height < 875 ? 875 : Height;
                    }

                    tlpRecords.Visible = true;
                    bitMap  = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                    DrawToBitmap(bitMap, new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height));
                    //PrintingUtilities.PrintBitmapDocument(bitMap);
                    printDocument1.PrintPage += printDocument1_PrintPage;
                    printDocument1.PrinterSettings.PrinterName = PRINTER_NAME; // read this dynamically.
                    PaperSize lettter = new PaperSize("Letter", 850, 1100);
                    PaperSize legal = new PaperSize("Legal", 850, 1400);
                    
                    //PaperSource paperSource = new PaperSource();
                    //paperSource.SourceName = "Tray 2";

                    printDocument1.DefaultPageSettings.PaperSize = legal;
                    
                    printDocument1.DefaultPageSettings.Landscape = true;
                   
                    printDocument1.Print();
                    resetTableLayoutPanel();
                    tblCurrentRowIndex = 0;
                    tlpRecords.Visible = false;
                }
                tblCurrentRowIndex++;
                FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "********** Processing record  :  " + i + " Completed.....  " + DateTime.Now);
            }
        }

        private void CreatePrintRow(DataRow dataRow, int iRowIdx)
        {
            
            // Gun Number
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["GUN_NUMBER"], "")), 0, iRowIdx);
            
            // Manufacturer and Importer
            string manufacturer = Utilities.GetStringValue(dataRow["MANUFACTURER"], "");
            string importer = Utilities.GetStringValue(dataRow["IMPORTER"], "");
            TableLayoutPanel tmpContorl = createInternalTable(manufacturer,importer,86);
            tlpRecords.Controls.Add(tmpContorl, 2, iRowIdx);
            
            // Model
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["MODEL"], "")), 3, iRowIdx);
           
            // Serial Number
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["SERIAL_NUMBER"], "")), 4, iRowIdx);
           
            // Gauge / Caliber
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["CALIBER"], "")), 5, iRowIdx);
            // Gun Type
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["GUN_TYPE"], "")), 6, iRowIdx);

            // Acquire Transaction 

            string transType = Utilities.GetStringValue(dataRow["ACQUIRE_TRANSACTION_TYPE"], "");
            string transNumber = Utilities.GetStringValue(dataRow["ACQUIRE_DOCUMENT_NUMBER"], "");
            tlpRecords.Controls.Add(CreateColumn(transType + "   " + transNumber), 8, iRowIdx);

            // Acquire Trans Date
            string tmpAcqDate = Utilities.GetStringValue(dataRow["ACQUIRE_DATE"], "");
            if(tmpAcqDate == null || tmpAcqDate.Trim().Length==0)
                tlpRecords.Controls.Add(CreateColumn(""), 9, iRowIdx);
            else
                tlpRecords.Controls.Add(CreateColumn(Utilities.GetDateTimeValue(tmpAcqDate).ToShortDateString()), 9, iRowIdx);

            // Acquire Name and ID
            string acqFirstName = Utilities.GetStringValue(dataRow["ACQUIRE_FIRST_NAME"], "");
            string acqLastName = Utilities.GetStringValue(dataRow["ACQUIRE_LAST_NAME"], "");
            
            string acqIdAgency = Utilities.GetStringValue(dataRow["ACQUIRE_ID_AGENCY"], "");
            string acqIdType = Utilities.GetStringValue(dataRow["ACQUIRE_ID_TYPE"], "");
            string acqIdNumber = Utilities.GetStringValue(dataRow["ACQUIRE_ID_NUMBER"], "");

            TableLayoutPanel nameIDTmpLayoutPanel = createInternalTable(acqFirstName + " " + acqLastName, acqIdType + " " + acqIdAgency + acqIdNumber,131);
            tlpRecords.Controls.Add(nameIDTmpLayoutPanel, 10, iRowIdx);

            // Acquire Address

            string acqAddress = Utilities.GetStringValue(dataRow["ACQUIRE_ADDRESS"], "");
            string acqCity = Utilities.GetStringValue(dataRow["ACQUIRE_CITY"], "");
            string acqState = Utilities.GetStringValue(dataRow["ACQUIRE_STATE"], "");
            string acqPostalCode = Utilities.GetStringValue(dataRow["ACQUIRE_POSTAL_CODE"], "");

            TableLayoutPanel addressTmpLayoutPanel = createInternalTable(acqAddress, acqCity + " " + acqState + " " + acqPostalCode,153);
            tlpRecords.Controls.Add(addressTmpLayoutPanel, 11, iRowIdx);

            // Disposition Transaction 

            string dispTransType = Utilities.GetStringValue(dataRow["DISPOSITION_TRANSACTION_TYPE"], "");
            string dispTransNumber = Utilities.GetStringValue(dataRow["DISPOSITION_DOCUMENT_NUMBER"], "");
            tlpRecords.Controls.Add(CreateColumn(dispTransType + "   " + dispTransNumber), 13, iRowIdx);

            // Disp Trans Date
            string tmpDispDate = Utilities.GetStringValue(dataRow["DISPOSITION_DATE"], "");
            if(tmpDispDate==null || tmpDispDate.Trim().Length==0)
                tlpRecords.Controls.Add(CreateColumn(""), 14, iRowIdx);
            else
                tlpRecords.Controls.Add(CreateColumn(Utilities.GetDateTimeValue(tmpDispDate).ToShortDateString()), 14, iRowIdx);

            // Disp Name and ID
            string dispFirstName = Utilities.GetStringValue(dataRow["DISPOSITION_FIRST_NAME"], "");
            string dispLastName = Utilities.GetStringValue(dataRow["DISPOSITION_LAST_NAME"], "");

            string dispIdType = Utilities.GetStringValue(dataRow["DISPOSITION_ID_TYPE"], "");
            string dispIdNumber = Utilities.GetStringValue(dataRow["DISPOSITION_ID_NUMBER"], "");

            TableLayoutPanel dispNmeIDTmpLayoutPanel = createInternalTable(dispFirstName + " " + dispLastName, dispIdType + " " + dispIdNumber,153);
            tlpRecords.Controls.Add(dispNmeIDTmpLayoutPanel, 15, iRowIdx);

            // Disp Address

            string dispAddress = Utilities.GetStringValue(dataRow["DISPOSITION_ADDRESS"], "");
            string dispCity = Utilities.GetStringValue(dataRow["DISPOSITION_CITY"], "");
            string dispState = Utilities.GetStringValue(dataRow["DISPOSITION_STATE"], "");
            string dispPostalCode = Utilities.GetStringValue(dataRow["DISPOSITION_POSTAL_CODE"], "");

            TableLayoutPanel dispaddressTmpLayoutPanel = createInternalTable(dispAddress, dispCity + " " + dispState + " " + dispPostalCode,153);
            tlpRecords.Controls.Add(dispaddressTmpLayoutPanel, 16, iRowIdx);

        }

        private Label CreateColumn(string sText)
        {
            Font font = new Font(label28.Font, FontStyle.Regular);
            Label label = Utilities.CloneObject<Label>(label28);
            label.AutoSize = true;
            label.Font = font;
            label.BackColor = Color.White;
            label.Text = sText;
            return label;
            
        }
        private string wrapText(string sText)
        {
            var wrappedText = string.Empty;
            string tText = (String)sText.Clone();
            if (tText != null && tText.Length > 30)
            {
                for (int i = 0; i < tText.Length; i = i + 30)
                {
                    wrappedText = wrappedText + (i != 0 ? "\n" : "") + tText.Substring(i, (i + 30 < tText.Length ? 30 : tText.Length - i));
                }
            }
            else
                return sText;

            return wrappedText;
        }
        private void resetTableLayoutPanel(){
            int count  = tlpRecords.Controls.Count;
            for (int i = count-1; i >= 30; --i)
            {
                //tlpRecords.RowStyles.RemoveAt(i);
                tlpRecords.Controls.RemoveAt(i);
            }

        }
       private TableLayoutPanel createInternalTable(string text1, string text2,int lengthOfLine)
        {

            DoubleBufferedTableLayoutPanel tbl = new DoubleBufferedTableLayoutPanel();
            tbl.Dock = DockStyle.Fill;
            tbl.AutoSize = tlpRecords.AutoSize;
            tbl.ColumnCount = 1;
            tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, lengthOfLine));
            tbl.AutoSizeMode = tlpRecords.AutoSizeMode;
            tbl.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
            tbl.Controls.Add(CreateColumn(text1), 0, 0);
            tbl.Controls.Add(CreateLineColumn(lengthOfLine), 0, 1);
            tbl.Controls.Add(CreateColumn(text2), 0, 2);
            tbl.AutoSize = true;
            tbl.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            tbl.Margin = new Padding(0); 
            return tbl;
        }


         private Label CreateLineColumn(int length)
        {
            Font font = new Font(label28.Font, FontStyle.Regular);
            Label label = Utilities.CloneObject<Label>(label28);
            label.AutoSize = false;
            //label.Image = global::CashlinxDesktop.Properties.Resources.line;
            label.Paint += new PaintEventHandler(label_Paint);
            label.Text = "";
            label.BackColor = Color.White;
            label.Height = 2;
            label.Margin = new System.Windows.Forms.Padding(0);
            label.Padding = new Padding(0); 
            label.Width = length;
            return label;
            
        }

         void label_Paint(object sender, PaintEventArgs e)
         {
             Pen p = new Pen(Color.LightGray, 0.001f);
             p.Width = 1;
             Point p1 = new Point(0,0);
             Point p2 = new Point(((Label)sender).Width, 0);
             e.Graphics.DrawLine(p, p1, p2);
         }

         private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
         {
             e.Graphics.DrawImage(bitMap, new Point(0, 0));
         }
       
   
    }
    }

