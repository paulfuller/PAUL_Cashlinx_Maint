using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Libraries.Utility.Collection;

namespace PawnStoreSetupTool
{
    public partial class SelectionForm : Form
    {
        private DataTable innerTable;
        private List<string> inputStrings;
        private List<bool> selections;
        public List<bool> Selections
        {
            get;
            private set;
        }

        public SelectionForm(List<string> inputs)
        {
            this.inputStrings = new List<string>();
            this.selections = new List<bool>();
            InitializeComponent();
            if (CollectionUtilities.isNotEmpty(inputs))
            {
                inputStrings.AddRange(inputs);
                foreach(var s in inputs)
                {
                    this.selections.Add(true);
                }
            }
        }

        private void selectionDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (sender == null)
                return;
            try
            {
                var sViewColumn = (this.selectionDataGridView.Columns.Contains("SelectColumn")
                                           ? this.selectionDataGridView.Columns["SelectColumn"]
                                           : null);

                if (sViewColumn != null && e.ColumnIndex == sViewColumn.Index)
                {
                    var dCell = (DataGridCell)sender;
                    var cellRow = (this.selectionDataGridView.Rows.Count > dCell.RowNumber)
                                                      ? this.selectionDataGridView.Rows[dCell.RowNumber]
                                                      : null;
                    if (cellRow != null)
                    {
                        var cellObj = (cellRow.Cells.Count > dCell.ColumnNumber)
                                              ? cellRow.Cells[dCell.ColumnNumber]
                                              : null;

                        if (cellObj != null && cellObj.Value != null)
                        {
                            var cellVal = (Boolean)cellObj.Value;
                            if (cellVal == false)
                            {
                                this.selections[dCell.RowNumber] = false;
                            }
                        }
                    }
                }
            }
            catch(Exception eX)
            {
                MessageBox.Show("Exception thrown when cell value changed: " + eX.Message);
                return;
            }
        }

        private void SelectionForm_Load(object sender, EventArgs e)
        {
            foreach(var s in this.inputStrings)
            {
                if (string.IsNullOrEmpty(s))
                {
                    continue;
                }
                var dR = new DataGridViewRow();
                var dCboxCell = new DataGridViewCheckBoxCell(false) {Value = false};
                var dTboxCell = new DataGridViewTextBoxCell {Value = s};

                dR.Cells.Add(dCboxCell);
                dR.Cells.Add(dTboxCell);
                this.selectionDataGridView.Rows.Add(dR);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }


    }
}
