using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using AuditQueries.Logic;
using Common.Controllers.Application;
using Common.Libraries.Forms;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;

namespace AuditQueries.Forms
{
    public partial class AuditParamResultForm : Form
    {
        public class AuditParamObject
        {
            public string ParameterName
            {
                private set;
                get;
            }

            public string ParameterType
            {
                private set;
                get;
            }

            public string ParameterValue
            {
                set;
                get;
            }

            public AuditParamObject()
            {
                ParameterName = string.Empty;
                ParameterType = string.Empty;
                ParameterValue = string.Empty;
            }

            public void SetParameterNameAndType(string name, string type)
            {
                this.ParameterName = name;
                this.ParameterType = type;
            }

        }
        private AuditParamObject[] auditParameters;
        private object cellEditMutex;
        private DataTable resultTable;
        private BindingSource resultBindingSource;

        public AuditParamResultForm()
        {
            InitializeComponent();
            QueryStorage qStore = AuditQueriesSession.Instance.GetQueryStorage();
            //Get number of parameters
            var qParams = qStore.GetQueryParams(AuditQueriesSession.Instance.SelectedQueryId);
            if (qParams != null)
            {
                var numParams = qParams.Keys.Count;
                auditParameters = new AuditParamObject[numParams];
            }
            cellEditMutex = new object();
            resultTable = null;
            resultBindingSource = new BindingSource();
        }


        private void AuditParamResultForm_Load(object sender, EventArgs e)
        {
            int selectedQueryId = AuditQueriesSession.Instance.SelectedQueryId;

            //Get the query from storage
            QueryStorage qStore = AuditQueriesSession.Instance.GetQueryStorage();
            var qParams = qStore.GetQueryParams(selectedQueryId);

            //Setup the parameter objects
            if (CollectionUtilities.isNotEmpty(qParams))
            {
                int idx = 0;
                foreach(var curKey in qParams.Keys)
                {
                    if (string.IsNullOrEmpty(curKey))
                        continue;
                    auditParameters[idx] = new AuditParamObject();
                    var curQParam = qParams[curKey];
                    auditParameters[idx].SetParameterNameAndType(curKey, curQParam.Left);
                    auditParameters[idx].ParameterValue = curQParam.Right;
                    ++idx;
                }
            }

            //If any parameters exist, set them into param data grid view
            this.populateDataGridWithAuditParameters();

            //Disable the execute button
            this.executeQueryButton.Enabled = false;
            this.saveButton.Enabled = false;
        }

        private void populateDataGridWithAuditParameters()
        {
            if (auditParameters != null && auditParameters.Length > 0)
            {                
                var curRows = this.queryParamDataGrid.Rows;
                if (curRows.Count > 0)
                {
                    this.queryParamDataGrid.Rows.Clear();
                }
                this.queryParamDataGrid.Rows.Add(auditParameters.Length);
                if (this.queryParamDataGrid.Rows.Count == auditParameters.Length)
                {
                    for (var idx = 0; idx < auditParameters.Length; ++idx)
                    {
                        var curAParam = auditParameters[idx];
                        var curRow = curRows[idx];
                        curRow.Cells[0].Value = curAParam.ParameterName;
                        curRow.Cells[1].Value = curAParam.ParameterType;
                        if (curAParam.ParameterType.Equals("string", StringComparison.OrdinalIgnoreCase))
                        {
                            if (curAParam.ParameterName.IndexOf("store", StringComparison.OrdinalIgnoreCase) != -1)
                            {
                                curRow.Cells[2].ValueType = typeof(string);
                                curRow.Cells[2].Value = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                                curRow.Cells[2].ReadOnly = true;

                                //Set audit parameters with the store number also
                                curAParam.ParameterValue = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

                            }
                            else
                            {
                                curRow.Cells[2].ValueType = typeof(string);
                                curRow.Cells[2].Value = "";
                            }
                        }
                        else
                        {
                            curRow.Cells[2].ValueType = typeof(string);
                            curRow.Cells[2].Value = "";
                        }                        
                    }
                }
            }
            this.queryParamDataGrid.Update();
        }

        private void executeQueryButton_Click(object sender, EventArgs e)
        {
            this.saveButton.Enabled = false;
            if (!GlobalDataAccessor.Instance.IsDataAccessorValid())
            {
                MessageBox.Show("There is no valid database connection available at this time.  Please exit and try again");
                return;
            }
            var qStore = AuditQueriesSession.Instance.GetQueryStorage();
            var selQ = AuditQueriesSession.Instance.SelectedQueryId;
            if (qStore == null)
            {
                MessageBox.Show("There are no valid queries defined for this audit query session.  Please exit and try again.");
                return;
            }

            //Update the parameters for this query
            bool flagInvalid = false;
            foreach(var p in this.auditParameters)
            {
                if (!qStore.UpdateQueryParameter(selQ, p.ParameterName, p.ParameterValue))
                {
                    flagInvalid = true;
                    break;
                }
            }
            if (flagInvalid)
            {
                MessageBox.Show("Could not execute the query due to an invalid parameter.");
                return;
            }

            try
            {
                //Extract the populated query to execute
                var qExec = qStore.GetPopulatedQuery(selQ);
                if (!string.IsNullOrEmpty(qExec) && qExec.Contains("?") == false)
                {
                    var pMesg = new ProcessingMessage("* QUERY PROCESSING - PLEASE WAIT *", 100000);
                    pMesg.Show();
                    if (!GlobalDataAccessor.Instance.OracleDA.issueSqlTextSelectCommand(qExec, string.Empty, CommandBehavior.Default, null, out this.resultTable))
                    {
                        pMesg.Close();
                        pMesg.Dispose();
                        MessageBox.Show("Failed to execute query!  Please try again");
                    }
                    else
                    {
                        //Check the number of rows and columns returned, if nothing, query was empty
                        if (this.resultTable != null && this.resultTable.Rows.Count > 0 && this.resultTable.Columns.Count > 0)
                        {
                            pMesg.Close();
                            pMesg.Dispose();
                            MessageBox.Show("Query was successful!");
                            //Put the datatable in the view
                            this.queryResultDataGridView.DataSource = this.resultBindingSource;
                            this.resultBindingSource.DataSource = this.resultTable;
                            this.queryResultDataGridView.Update();
                            this.saveButton.Enabled = true;
                        }
                        else
                        {
                            pMesg.Close();
                            pMesg.Dispose();
                            MessageBox.Show("Query returned zero rows.  Please try again with different parameters!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The query parameters are not specified properly.  Please try again");
                }
            }
            catch (SystemException rEx)
            {
                MessageBox.Show("Query was not successful and/or returned zero rows.  Please try again." + Environment.NewLine + "Error Details: " + rEx,
                    "AuditQueryApp Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(ApplicationException aEx)
            {
                MessageBox.Show("Query was not successful and/or returned zero rows.  Please try again." + Environment.NewLine + "Error Details: " + aEx,
                    "AuditQueryApp Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception eX)
            {
                MessageBox.Show("Query was not successful and/or returned zero rows.  Please try again." + Environment.NewLine + "Error Details: " + eX,
                    "AuditQueryApp Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (this.resultTable != null && 
                this.resultTable.IsInitialized && 
                !this.resultTable.HasErrors &&
                this.resultTable.Rows.Count > 0 &&
                this.resultTable.Columns.Count > 0)
            {
                try
                {

                    var pathName = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.None);
                    var dNow = DateTime.Now;
                    var fName = string.Format("auditResult_{0}_{1}_{2}-{3}_{4}_{5}_{6}.csv", dNow.Day.ToString().PadLeft(2, '0'),
                                              dNow.Month.ToString().PadLeft(2, '0'), dNow.Year, dNow.Hour.ToString().PadLeft(2, '0'),
                                              dNow.Minute.ToString().PadLeft(2, '0'),
                                              dNow.Second.ToString().PadLeft(2, '0'),
                                              dNow.Millisecond.ToString().PadLeft(4, '0'));
                    MessageBox.Show("Result file will be saved in this directory:" + Environment.NewLine + pathName + Environment.NewLine +
                                    "with this file name:" + Environment.NewLine + fName, "AuditQueriesApp Message");

                    //Adjust path name to end with slash
                    if (!pathName.EndsWith("\\"))
                    {
                        pathName += "\\";
                    }

                    //Concatenate path and file name
                    var fullFileName = pathName + fName;

                    //Open to write text file
                    var swriter = new StreamWriter(fullFileName);



                    if (swriter != StreamWriter.Null)
                    {
                        var columnCount = resultTable.Columns.Count;
                        //Write columns first
                        for(var i = 0; i < columnCount; ++i)
                        {
                            if (i > 0)
                            {
                                swriter.Write(",");
                            }
                            swriter.Write(resultTable.Columns[i].ColumnName.ToUpperInvariant());
                        }

                        //End the column line with a comma and a new line feed
                        swriter.WriteLine(",");

                        //Write rows last
                        for (var r = 0; r < resultTable.Rows.Count; ++r)
                        {
                            var curRow = resultTable.Rows[r];
                            for (var c = 0; c < columnCount; ++c)
                            {
                                var curCell = curRow[c];
                                if (c > 0)
                                {
                                    swriter.Write(",");
                                }
                                if (curCell == null)
                                {
                                    swriter.Write(",");
                                }
                                else
                                {
                                    swriter.Write(curCell.ToString());
                                }
                            } 
                            //After each row, write a comma and a new line feed as long as it is not the last row
                            if (r < resultTable.Rows.Count - 1)
                            {
                                swriter.WriteLine(",");
                                swriter.Flush();
                            }
                        }
                        //Done writing file
                        swriter.Close();
                        MessageBox.Show("File saved successfully.", "AuditQueriesApp Message", MessageBoxButtons.OK);
                    }
                }
                catch (System.IO.IOException iEx)
                {
                    //Specific IO system exception
                    MessageBox.Show("Could not save file!  Please try again", "AuditQueriesApp Message", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                }
                catch(SystemException rEx)
                {
                    //General system exception
                    MessageBox.Show("Could not save file!  Please try again", "AuditQueriesApp Message", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                catch (ApplicationException aEx)
                {
                    //General app exception
                    MessageBox.Show("Could not save file!  Please try again", "AuditQueriesApp Message", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                catch (Exception eX)
                {
                    //General exception
                    MessageBox.Show("Could not save file!  Please try again", "AuditQueriesApp Message", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No results to save! Please try the query again with different parameters.", "AuditQueriesApp Message", MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            var res = 
                MessageBox.Show("Are you sure you want to exit this form?  All unsaved data will be lost!", 
                    "AuditQueriesApp Message",
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Stop);
            if (res == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void queryParamDataGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (sender == null)
                return;
        }

        private void queryParamDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (sender == null)
                return;

            var rowIdx = e.RowIndex;
            var colIdx = e.ColumnIndex;
            if (this.queryParamDataGrid.Rows.Count <= rowIdx)
            {
                return;
            }
            var cellRow = this.queryParamDataGrid.Rows[rowIdx];
            if (cellRow.Cells.Count <= colIdx)
            {
                return;
            }
            var cellEdited = cellRow.Cells[colIdx];
            if (cellEdited != null)
            {
                var cellVal = cellEdited.Value;
                updateAuditParameterValue(rowIdx, cellVal);
                updateExecuteQueryButton();
            }
        }

        private void updateExecuteQueryButton()
        {
            //Verify that all parameters have values and then enable the execute query button
            int validCnt = 0;
            foreach(var p in this.auditParameters)
            {
                if (p == null)
                    continue;
                if (p.ParameterType.Equals("string", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrEmpty(p.ParameterValue) && !string.IsNullOrWhiteSpace(p.ParameterValue))
                    {
                        validCnt++;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(p.ParameterValue))
                    {
                        decimal val = Utilities.GetDecimalValue(p.ParameterValue, Decimal.MinValue);
                        if (val != Decimal.MinValue)
                        {
                            validCnt++;
                        }
                    }
                }
            }

            //Must have at least one valid parameter
            this.executeQueryButton.Enabled = validCnt > 0;
        }

        private void updateAuditParameterValue(int idx, object val)
        {
            if (idx < 0 || idx >= auditParameters.Length)
                return;
            if (val == null)
            {
                val = string.Empty;
            }
            this.auditParameters[idx].ParameterValue = (string)val;
        }
    }
}
