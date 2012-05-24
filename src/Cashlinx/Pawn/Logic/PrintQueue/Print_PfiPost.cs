/*********************************************
 * Print_PfiPost.cs
 * 
 * History:
 *  PWNU00000635 SMurphy 5/6/2010 formatting issues
 *  no ticket SMurphy 5/6/2010 date formatting issues
 *********************************************/

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
    public partial class Print_PfiPost : Form
    {
        const int _FormOriginalRowY = 150;
        private string _AsOf;
        private DataRowCollection _Records;
        private string _Tags;
        private string _TotalCost;

        public Print_PfiPost(DataTable dataTable, string sTotalCost, string sTags, string sAsOf)
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

        private void Print_PfiPost_Load(object sender, EventArgs e)
        {
            if (_Records.Count > 0)
            {
                ProcessingMessage myForm = new ProcessingMessage("Please wait while we generate report.");
                myForm.Show();
                PrintQueue();
                myForm.Close();
                myForm.Dispose();
                MessageBox.Show("Printing Complete", "PFI POST", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No records available to print", "PFI POST", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Close();
        }

        public void PrintQueue()
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            string sStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            string sRunDate = Convert.ToDateTime(ShopDateTime.Instance.ShopTime.ToString()).ToString("MM/dd/yyyy HH:mm");// String.Format("{0:MM/dd/yyyy HH:mm:ss}", ShopDateTime.Instance.ShopTime);
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
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["colRefurb"], ""), 0), 0, iRowIdx);
            // Number
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["colNumber"], ""), 1), 1, iRowIdx);
            // Ticket Description
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["colDescription"], ""), 2), 2, iRowIdx);
            // Tags
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["colTags"], ""), 3), 3, iRowIdx);
            // Cost
            string[] arr = Utilities.GetStringValue(dataRow["colCost"], "").Split(new[] {"\r\n\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length > 1)
                tlpRecords.Controls.Add(CreateColumn(arr[0].PadLeft(12, ' ') + "\r\n" + arr[1].PadLeft(12, ' '), 4), 4, iRowIdx);
            else
                tlpRecords.Controls.Add(CreateColumn(arr[0], 4), 4, iRowIdx);
            // Retail
            arr = Utilities.GetStringValue(dataRow["colRetail"], "").Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length > 1)
                tlpRecords.Controls.Add(CreateColumn(arr[0].PadLeft(12, ' ') + "\r\n" + arr[1].PadLeft(12, ' '), 5), 5, iRowIdx);
            else
                tlpRecords.Controls.Add(CreateColumn(arr[0], 5), 5, iRowIdx);
            // Reason
            tlpRecords.Controls.Add(CreateColumn(Utilities.GetStringValue(dataRow["colReason"], ""), 6), 6, iRowIdx);
            //tlpRecords.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            tlpRecords.Controls.Add(CreateColumn(string.Empty, iRowIdx++));


        }

        private Label CreateColumn(string sText, int column)
        {
            Font font = new Font(refurbColumnLabel.Font, FontStyle.Regular);
            Label label = Utilities.CloneObject<Label>(refurbColumnLabel);
            label.AutoSize = true;
            label.Font = font;
            label.Text = sText;
            switch (column)
            {
                case 1:
                    label.Anchor = AnchorStyles.Top;
                    break;

                case 3:
                case 4: 
                case 5:
                    label.Anchor = AnchorStyles.Right;
                    break;

                default:
                    label.Anchor = AnchorStyles.Left;
                    break;
            }

            return label;
        }
    }
}
