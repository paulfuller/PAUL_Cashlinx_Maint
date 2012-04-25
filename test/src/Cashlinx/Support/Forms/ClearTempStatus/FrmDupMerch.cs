using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CashlinxPawnSupportApp.Forms.ClearTempStatus
{
    public partial class FrmDupMerch : Form
    {
        private string fullICN;
        private string lockDescription;

        public string FullICN
        {
            get
            {
                return (fullICN);
            }
        }

        public string LockDescription
        {
            get
            {
                return (lockDescription);
            }
        }

        public FrmDupMerch(DataTable inDataTable)
        {
            InitializeComponent();
            BindingSource bSource = new BindingSource();
            bSource.DataSource = inDataTable;
            dgDuplicates.AutoGenerateColumns = false;
            dgDuplicates.DataSource = bSource;
            dgDuplicates.Columns[0].DataPropertyName = "FULLICN";
            dgDuplicates.Columns[1].DataPropertyName = "MD_DESC";
            dgDuplicates.Columns[2].DataPropertyName = "STATUS_CD";
            dgDuplicates.Columns[3].DataPropertyName = "AMOUNT";
            dgDuplicates.Columns[4].DataPropertyName = "TEMP_STATUS_DESC";
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string SelectICN;
            string LockType;
            SelectICN = dgDuplicates[0, dgDuplicates.CurrentRow.Index].Value.ToString();
            LockType = dgDuplicates[4, dgDuplicates.CurrentRow.Index].Value.ToString();
            if (LockType == string.Empty)
            {
                MessageBox.Show("No temp lock found.", "Warning",
                                 MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            this.fullICN = SelectICN;
            this.lockDescription = dgDuplicates[4, dgDuplicates.CurrentRow.Index].Value.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
}
