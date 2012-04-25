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
    public partial class Print_AssignPhysicalLocation : Form
    {
        const int _FormOriginalRowY = 150;
        private DataRowCollection _Records;

        public Print_AssignPhysicalLocation(DataTable dataTable)
        {
            InitializeComponent();

            if (dataTable != null)
            {
                _Records = dataTable.Rows;
            }
        }

        private void Print_AssignPhysicalLocation_Load(object sender, EventArgs e)
        {
            if (_Records.Count > 0)
            {
                ProcessingMessage myForm = new ProcessingMessage("Please wait while we generate report.");
                myForm.Show();
                PrintQueue();
                myForm.Close();
                myForm.Dispose();
                MessageBox.Show("Printing Complete", "Merchandise Location Assignment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No records available to print", "Merchandise Location Assignment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Close();
        }

        public void PrintQueue()
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            string sStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            string sRunDate = String.Format("{0:MM/dd/yyyy HH:mm}", ShopDateTime.Instance.ShopDate);
            string sAsOfDate = String.Format("{0:MM/dd/yyyy}", ShopDateTime.Instance.ShopDate);

            int iRecordNumber = 0;
            int iRecords = _Records.Count;

            storeValueLabel.Text = sStoreNumber;
            runValueLabel.Text = sRunDate;
            asOfValueLabel.Text = sAsOfDate;

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
                    ClearTableLayoutPanel();
                    iRecordNumber = 0;
                }
            }

            Cursor = Cursors.Default;
            Application.DoEvents();
        }

        private void ClearTableLayoutPanel()
        {
            for (int i = tlpRecords.Controls.Count - 1; i >= tlpRecords.ColumnCount; i--)
            {
                tlpRecords.Controls.RemoveAt(i);
            }
        }

        private void CreatePrintRow(DataRow dataRow, int iRowIdx)
        {
            // User Label
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["userID"], "")), 0, iRowIdx);
            // Ticket No
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["icn_doc"], "")), 1, iRowIdx);
            // Ticket Description
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["md_desc"], "")), 2, iRowIdx);
            // Aisle
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["loc_aisle"], "")), 3, iRowIdx);
            // Shelf
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["loc_shelf"], "")), 4, iRowIdx);
            // Other
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["location"], "")), 5, iRowIdx);
            // Gun No
            string sGunNumber = Utilities.GetStringValue(dataRow["gun_number"], "");
            sGunNumber = sGunNumber == "0" ? "" : sGunNumber;
            tlpRecords.Controls.Add(CreateColumn(sGunNumber), 6, iRowIdx);

        }

        private Label CreateColumn(string sText)
        {
            Font font = new Font(userColumnLabel.Font, FontStyle.Regular);
            Label label = Utilities.CloneObject<Label>(userColumnLabel);
            label.AutoSize = true;
            label.Font = font;
            label.Text = sText;
            return label;
        }
    }
}
