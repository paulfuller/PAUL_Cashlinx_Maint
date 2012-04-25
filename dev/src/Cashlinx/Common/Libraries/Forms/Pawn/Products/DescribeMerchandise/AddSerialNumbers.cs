using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;

namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    public partial class AddSerialNumbers : CustomBaseForm
    {
        public int ItemQuantity;
        public int ItemOrder;
        public string FirstSerialNumber;
        private List<string> oldSerialNumbers;
        private List<string> newSerialNumbers;

        public DesktopSession DesktopSession { get; private set; }

        public AddSerialNumbers(DesktopSession desktopSession)
        {
            DesktopSession = desktopSession;
            InitializeComponent();
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddSerialNumbers_Load(object sender, EventArgs e)
        {
            newSerialNumbers = new List<string>();
            if (DesktopSession.ActivePurchase.Items[ItemOrder].SerialNumber != null &&
                    DesktopSession.ActivePurchase.Items[ItemOrder].SerialNumber.Count > 0)
                oldSerialNumbers = DesktopSession.ActivePurchase.Items[ItemOrder].SerialNumber;
            dataGridViewSerialNos.Rows.Add(ItemQuantity);
            for (int i = 0; i < ItemQuantity; i++)
            {
                dataGridViewSerialNos.Rows[i].Cells[0].Value = (i + 1).ToString();
                if (oldSerialNumbers != null && oldSerialNumbers.Count >= (i+1) && oldSerialNumbers[i] != null)
                dataGridViewSerialNos.Rows[i].Cells[1].Value = oldSerialNumbers[i];
            }

            if (!string.IsNullOrEmpty(FirstSerialNumber))
            {
                newSerialNumbers.Add(FirstSerialNumber);
                dataGridViewSerialNos.Rows[0].Cells[1].Value = FirstSerialNumber;

            }
            
            
        }

        private void customButtonContinue_Click(object sender, EventArgs e)
        {
            newSerialNumbers = new List<string>();
            foreach (DataGridViewRow dgvr in dataGridViewSerialNos.Rows)
            {
                if (dgvr.Cells[1].Value != null && !string.IsNullOrEmpty(dgvr.Cells[1].Value.ToString()))
                    newSerialNumbers.Add(dgvr.Cells[1].Value.ToString());

            }
            if (newSerialNumbers.Count != ItemQuantity)
            {
                MessageBox.Show("Please enter serial numbers for all the items before continuing");
                return;
            }

            if (DesktopSession.ActivePurchase != null &&
                DesktopSession.ActivePurchase.Items.Count >= ItemOrder)
            {
                DesktopSession.ActivePurchase.Items[ItemOrder].SerialNumber = null;
                DesktopSession.ActivePurchase.Items[ItemOrder].SerialNumber = newSerialNumbers;
            }
            this.Close();
        }

        private void dataGridViewSerialNos_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex > 0)
            {
                if (string.IsNullOrEmpty(dataGridViewSerialNos.Rows[e.RowIndex].Cells[1].EditedFormattedValue.ToString()))
                {
                    MessageBox.Show("Serial number cannot be blank");
                    dataGridViewSerialNos.Rows[e.RowIndex].ErrorText = "Serial number cannot be blank";
                    return;
                }
                
                newSerialNumbers.Add(dataGridViewSerialNos.Rows[e.RowIndex].Cells[1].EditedFormattedValue.ToString());
                if (e.RowIndex == ItemQuantity - 1)
                {
                    customButtonContinue.Focus();
                    return;
                }
            }
        }
    }
}
