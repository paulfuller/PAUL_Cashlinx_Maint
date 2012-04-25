using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PawnStoreSetupTool
{
    public partial class SummaryForm : Form
    {
        private DataTable summaryData;
        private string formHeaderName;
        private bool isReadOnly;
        private bool isCheckBox;
        private bool checkBoxAll;
        private Dictionary<int, bool> checkedState;
        public Dictionary<int, bool> CheckedState
        {
            get
            {
                return (this.checkedState);
            }
        }


        public DataTable SummaryData
        {
            get
            {
                return (this.summaryData);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sData">Data table to display</param>
        /// <param name="fmHeaderName">Form header name</param>
        /// <param name="isROnly">Flag to set the data grid view to read only</param>
        public SummaryForm(DataTable sData, string fmHeaderName, bool isROnly)
        {
            InitializeComponent();
            this.summaryData = sData;
            this.formHeaderName = fmHeaderName;
            this.isReadOnly = isROnly;
            this.isCheckBox = false;
            this.checkBoxAll = false;
            this.checkedState = new Dictionary<int, bool>();
        }

        /// <summary>
        /// Constructor - turns check boxes on, allows turning all checks on or off
        /// </summary>
        /// <param name="sData">Data table to display</param>
        /// <param name="fmHeaderName">Form header name</param>
        /// <param name="isROnly">Flag to set the data grid view to read only</param>
        /// <param name="chkBoxAll">Flag to show check boxes all enabled</param>
        public SummaryForm(DataTable sData, string fmHeaderName, bool isROnly, bool chkBoxAll)
        {
            InitializeComponent();
            this.summaryData = sData;
            this.formHeaderName = fmHeaderName;
            this.isReadOnly = isROnly;
            this.isCheckBox = true;
            this.checkBoxAll = true;
            this.checkedState = new Dictionary<int, bool>();
        }

        private void SummaryForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.formHeaderName))
            {
                //Set form header
                Text = this.formHeaderName;
            }

            //Set read-only flag of datagrid view
            this.summaryDataGridView.ReadOnly = this.isReadOnly;
            this.summaryDataGridView.AllowUserToDeleteRows = !this.isReadOnly;
            this.summaryDataGridView.ShowEditingIcon = !this.isReadOnly;
            this.summaryDataGridView.RowHeadersVisible = !this.isReadOnly;
            this.summaryDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.summaryDataGridView.MultiSelect = !this.isReadOnly && this.isCheckBox;
            /*if (this.summaryDataGridView.Columns.Contains("CheckColumn"))
            {
                var col = this.summaryDataGridView.Columns["CheckColumn"];
                if (col != null)
                {
                    col.Visible = this.isCheckBox;
                }
            }
            if (this.summaryDataGridView.Columns.Contains("Query"))
            {
                var col = this.summaryDataGridView.Columns["Query"];
                if (col != null)
                {
                    col.Visible = this.isCheckBox;    
                }
            }*/


            if (this.summaryData != null &&
                this.summaryData.IsInitialized &&
                this.summaryData.Rows != null &&
                this.summaryData.Rows.Count > 0)
            {
                this.summaryDataGridView.DataSource = this.summaryData;
                this.summaryDataGridView.Update();
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            /*if (this.isCheckBox)
            {
                foreach (DataGridViewRow dR in this.summaryDataGridView.Rows)
                {
                    if (dR == null)
                        continue;
                    if (dR.Cells != null && 
                        dR.Cells.Count > 0 && 
                        dR.Cells[0] != null && 
                        dR.Cells[0].Value != null)
                    {
                        this.checkedState.Add(dR.Index, true);
                    }
                    else
                    {
                        this.checkedState.Add(dR.Index, false);
                    }
                }
            }*/
            Close();
        }
    }
}
