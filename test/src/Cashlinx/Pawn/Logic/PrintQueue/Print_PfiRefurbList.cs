using Common.Controllers.Application;
using Common.Libraries.Forms;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Pawn.Logic.PrintQueue
{
    public partial class Print_PfiRefurbList : Form
    {
        const int _FormOriginalRowY = 150;
        private string _AsOf;
        private DataRowCollection _Records;
        private string _Tags;
        private string _TotalCost;

        public Print_PfiRefurbList(DataTable dataTable, string sTotalCost, string sTags, string sAsOf)
        {
            InitializeComponent();

            if (dataTable != null)
            {
                _Records = dataTable.Rows;
                _Tags = sTags;
                _TotalCost = sTotalCost;
                _AsOf = sAsOf;
            }
        }

        private void Print_PfiRefurbList_Load(object sender, EventArgs e)
        {
            if (_Records.Count > 0)
            {
                ProcessingMessage myForm = new ProcessingMessage("Please wait while we generate report.");
                myForm.Show();
                PrintQueue();
                myForm.Close();
                myForm.Dispose();
                MessageBox.Show("Printing Complete", "PFI Refurb List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No records available to print", "PFI Refurb List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Close();
        }

        public void PrintQueue()
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            string sStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            string sRunDate = String.Format("{0:MM/dd/yyyy HH:mm}", ShopDateTime.Instance.ShopDate);
            string sAsOfDate = _AsOf;

            int iRecordNumber = 0;
            int iRecords = _Records.Count;

            storeValueLabel.Text = sStoreNumber;
            runValueLabel.Text = sRunDate;
            asOfValueLabel.Text = sAsOfDate;
            totalAmountValueLabel.Text = _TotalCost;
            tagsValueLabel.Text = _Tags;

            for (int i = 0; i < iRecords; i++)
            {
                if (iRecordNumber == 0)
                    iRecordNumber = i + 1;
                DataRow dataRow = _Records[i];
                CreatePrintRow(dataRow, i + 1);

                if (tlpRecords.Height >= 870 || (i + 1) == iRecords)
                {
                    pageLabel.Text = String.Format("{0} thru {1} of {2}", iRecordNumber, i + 1, iRecords);
                    this.Height = _FormOriginalRowY + tlpRecords.Height + 5;
                    Application.DoEvents();

                    if ((i + 1) == iRecords)
                    {
                        Height = Height < 875 ? 875 : Height;
                    }

                    Bitmap bitMap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                    DrawToBitmap(bitMap, new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height));
                    PrintingUtilities.PrintBitmapDocument(bitMap, GlobalDataAccessor.Instance.DesktopSession);
                    tlpRecords.Controls.Clear();
                    iRecordNumber = 0;
                }
            }

            Cursor = Cursors.Default;
            Application.DoEvents();
        }

        private void CreatePrintRow(DataRow dataRow, int iRowIdx)
        {
            // Refurb
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["colRefurb"], "")), 0, iRowIdx);
            // Number
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["colNumber"], "")), 1, iRowIdx);
            // Ticket Description
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["colDescription"], "")), 2, iRowIdx);
            // Tags
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["colTags"], "")), 3, iRowIdx);
            // Cost
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["colCost"], "")), 4, iRowIdx);
            // Retail
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["colRetail"], "")), 5, iRowIdx);
            // Reason
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["colReason"], "")), 6, iRowIdx);

        }

        private Label CreateColumn(string sText)
        {
            Font font = new Font(refurbColumnLabel.Font, FontStyle.Regular);
            Label label = Utilities.CloneObject<Label>(refurbColumnLabel);
            label.AutoSize = true;
            label.Font = font;
            label.Text = sText;
            return label;
        }
    }
}
