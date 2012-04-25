/************************************************************************
* Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products
* Class:           AssignPhysicalLocation
* 
* Description      Form to assign actual location of item in Store
* 
* History
* David D Wise, Initial Development
* 
* 8/23/2010 TLM    Added support for merchandise relocation.
* 8/31/2010 TLM    Improved efficiency 
* 1/3/2011  TLM    Added code to prevent returning to ring menu, if relocating (BZ-51)
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
using Pawn.Logic.PrintQueue;

namespace Pawn.Forms.Pawn.Products
{
    public partial class AssignPhysicalLocation : Form
    {
        private int _CurrentRowIndex;
        private DataTable _UnassignedItems = null;
        private DataTable _GridTable = null;
        private int _GridPages = 0;
        private int _GridPageMaxRows = 20;
        private List<LocationData> _LocationData;
        private TextBox _OtherTextBox;
        private bool _SetupInProgress;
        private int MIN_CRITERIA_LEN = 1;
        private int lastSelectedIndex = -1;

        private enum SEARCH_METHOD
        {
            ICN = 0,
            LOAN_NR = 1,
            PURCHASE_NR = 2
        }

        public NavBox NavControlBox;

        public AssignPhysicalLocation()
        {
            _SetupInProgress = true;
            InitializeComponent();

            this.NavControlBox = new NavBox();
            this.NavControlBox.Owner = this;

            gvLocation.externalPagingControl = true;
            firstButton.Click += gvLocation.firstPage;
            lastButton.Click += gvLocation.lastPage;
            nextButton.Click += gvLocation.nextPage;
            previousButton.Click += gvLocation.prevPage;

            gvLocation.onPageChange += updatePageNumber;
        }

        private void updatePageNumber(object sender, PropertyChangedEventArgs a)
        {
            try
            {
                if (pageIndexLabel.Visible)
                    pageIndexLabel.Text = "Page " + gvLocation.currPage + " of " +
                                          gvLocation.pageCount;

                if (rowIndexLabel.Visible)
                {
                    int curRecd = (gvLocation.currPage - 1) * gvLocation.pageSize + 1;
                    rowIndexLabel.Text = "Record " + curRecd + " of " +
                                         (gvLocation.nrRecords).ToString();

                    // restore any changes made on other data pages
                    if (_LocationData != null && _LocationData.Count > 0)
                    {
                        for (int i = 0; i < gvLocation.pageSize; i++)
                        {
                            int iDx = _LocationData.FindIndex(delegate(LocationData ld)
                                                              {
                                                                  return
                                                                  Convert.ToInt32(
                                                                      gvLocation.Rows[i]
                                                                      .Cells[
                                                                          "recID"
                                                                      ].Value) ==
                                                                  ld.RecordID;
                                                              });

                            if (iDx >= 0)
                            {
                                gvLocation.Rows[i].Cells["loc_aisle"].Value =
                                _LocationData[iDx].Aisle;
                                gvLocation.Rows[i].Cells["loc_shelf"].Value =
                                _LocationData[iDx].Shelf;
                                gvLocation.Rows[i].Cells["location"].Value =
                                _LocationData[iDx].Location;
                            }
                        }
                    }
                }

                gvLocation.Columns["status_cd"].DisplayIndex = 2;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void AssignPhysicalLocation_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            viewTransactionComboBox.SelectedIndex = 1;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Initial Setup of Form
        /// </summary>
        /// <param name="existingItems">Indicates whether this is for initial or re-assignments.</param>
        private void Setup(bool existingItems)
        {
            _SetupInProgress = true;

            string sErrorCode;
            string sErrorText;

            _CurrentRowIndex = -1;

            if (gvLocation.Controls.Count < 3)
            {
                _OtherTextBox = new TextBox();
                _OtherTextBox.Name = "other_TextBox";
                _OtherTextBox.AutoCompleteCustomSource.Add("Gun Safe");
                _OtherTextBox.AutoCompleteCustomSource.Add("Pawn Guard Safe");
                _OtherTextBox.AutoCompleteCustomSource.Add("Standard Safe");
                _OtherTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                _OtherTextBox.AutoCompleteMode = AutoCompleteMode.Append;
                _OtherTextBox.Enter += new EventHandler(validatedEntryOtherTextBox_Enter);
                _OtherTextBox.LostFocus += new EventHandler(validatedEntryOtherTextBox_Exit);
                _OtherTextBox.Visible = false;
                _OtherTextBox.MaxLength = 50;
                gvLocation.Controls.Add(_OtherTextBox);
            }

            _LocationData = new List<LocationData>();

            if (_UnassignedItems == null || existingItems || lastSelectedIndex == 2)
                _UnassignedItems = new DataTable();

            string criteria = searchCriteria.Text;
            int srchType = searchType.SelectedIndex;

            gvLocation.ShowCellToolTips = false;

            if (!existingItems) // show all unassigned items
            {
                criteria = "";
                srchType = -1;
            }
            else
            {
                switch ((SEARCH_METHOD) srchType)
                {
                    case SEARCH_METHOD.LOAN_NR:
                    case SEARCH_METHOD.PURCHASE_NR:
                        if (criteria.Length > 6)
                        {
                            if (criteria.Substring(0, 5) != GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber)
                            {
                                MessageBox.Show(Commons.GetMessageString("WrongStore") + " " + criteria.Substring(0, 5),
                                                "Warning",
                                                MessageBoxButtons.OK);

                                _SetupInProgress = false;

                                return;
                            }

                            criteria = criteria.Substring(5).PadLeft(6, '0');
                        }
                        break;

                    case SEARCH_METHOD.ICN: // ICN Search

                        if (criteria.Length <= 10)// Short ICN Search
                        {
                            int shortICN_dotLoctn = criteria.IndexOf('.');
                            bool isValidICN = false;

                            if (shortICN_dotLoctn > 0 && shortICN_dotLoctn < criteria.Length)
                            {
                                string[] shortICN = criteria.Split(new char[]
                                                                   {
                                                                       '.'
                                                                   });

                                if (shortICN.Length == 2)
                                {
                                    string ICNStore = shortICN[0].PadLeft(6, '0');
                                    string ICNItem = shortICN[1];

                                    isValidICN = (ICNItem.Length > 0 && ICNItem.Length <= 3);

                                    if (ICNItem.Length > 0 && ICNItem.Length <= 3)
                                        ICNItem = ICNItem.PadLeft(3, '0');

                                    string ICN = ICNStore + ICNItem;

                                    criteria = ICN;

                                    isValidICN = isValidICN && (criteria.Length == 9);
                                }
                            }

                            if (!isValidICN)
                            {
                                MessageBox.Show(Commons.GetMessageString("ICNInvalid"),
                                                "Warning",
                                                MessageBoxButtons.OK);

                                _SetupInProgress = false;

                                return;
                            }

                            gvLocation.ShowCellToolTips = true;
                        }
                            
                        break;
                }
            }

            if (_UnassignedItems.Rows.Count == 0)
                AssignMDSE.GetAssignableItems(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                                srchType,  // NOTE there is an assumption here that the list sequence is directly mappable to SearchType, for re-assignments
                                                                criteria,
                                                                out _UnassignedItems,
                                                                out sErrorCode,
                                                                out sErrorText);
            else
            {
                sErrorCode = "0";
                sErrorText = "Success";
            }

            //BZ-56-------------------------
            string[] unassignableStatuses = { "PS", "PU", "CO", "RET", "TO" };
            var e = from DataRow item in _UnassignedItems.Rows
                    where unassignableStatuses.Contains(item["status_cd"].ToString())
                    select item;
           
            if (e.Count() > 0)
            {
                MessageBox.Show("One or more items are ineligible for relocation.", "Merchandise Location Assignment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clearGrid();
                searchCriteria.Text = "";

                searchCriteria.Focus();
            }
            else
            //---------------------BZ-56
            if (_UnassignedItems.Rows.Count > 0)
            {
                if (!_UnassignedItems.Columns.Contains("RecID"))
                {
                    DataColumn myColumn = new DataColumn("RecID", typeof(System.Int32));
                    _UnassignedItems.Columns.Add(myColumn);

                    for (int i = 0; i < _UnassignedItems.Rows.Count; i++)
                    {
                        _UnassignedItems.Rows[i]["RecID"] = i + 1;
                    }
                }

                if (!_UnassignedItems.Columns.Contains("Ticket"))
                {
                    DataColumn myColumn = new DataColumn("Ticket", typeof(System.Int32));
                    _UnassignedItems.Columns.Add(myColumn);
                    for (int i = 0; i < _UnassignedItems.Rows.Count; i++)
                    {
                        if (_UnassignedItems.Rows[i]["status_cd"].ToString().Equals("LAY"))
                            _UnassignedItems.Rows[i]["Ticket"] = _UnassignedItems.Rows[i]["disp_doc"];
                        else
                            _UnassignedItems.Rows[i]["Ticket"] = _UnassignedItems.Rows[i]["icn_doc"];

                        if (srchType != 0)
                        {
                            _UnassignedItems.Rows[i]["LOC_AISLE"] = string.Empty;
                            _UnassignedItems.Rows[i]["LOC_SHELF"] = string.Empty;
                            _UnassignedItems.Rows[i]["LOCATION"] = string.Empty;
                        }

                    }
                }

                UpdateDataGridData();

                if (_UnassignedItems.Rows.Count > _GridPageMaxRows)
                    panel1.Visible = true;

                rowIndexLabel.Visible = true;
            }
            else
            {
                if (sErrorCode == "1")
                {
                    MessageBox.Show("No Items Found.", "Merchandise Location Assignment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Close();
                }
                else
                {
                    MessageBox.Show(sErrorCode + "::" + sErrorText + ".  Please contact support", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }

            _SetupInProgress = false;
        }

        private void validatedEntryOtherTextBox_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate()
                             {
                                 _OtherTextBox.Focus();
                                 _OtherTextBox.SelectAll();
                             });
        }

        private void validatedEntryOtherTextBox_Exit(object sender, EventArgs e)
        {
            gvLocation_UpdateRow(gvLocation.Columns["location"].Index, _CurrentRowIndex);
        }

        private void gvLocation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvLocation.Columns[e.ColumnIndex].Name == "location")
                {
                    gvLocation_AdjustColumnOther(e.ColumnIndex, e.RowIndex);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void gvLocation_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (_SetupInProgress)
                return;

            if (e.RowIndex >= 0)
            {
                if (gvLocation.Columns[e.ColumnIndex].Name == "location")
                {
                    gvLocation_AdjustColumnOther(e.ColumnIndex, e.RowIndex);
                }
                else if (_OtherTextBox.Visible && gvLocation.Columns["location"] != null)
                {
                    gvLocation_UpdateRow(gvLocation.Columns["location"].Index, _CurrentRowIndex);
                }

                //this._UnassignedItems.Rows[e.RowIndex].Table.Columns._list
                int curRecd = (gvLocation.currPage - 1) * gvLocation.pageSize + 1 + e.RowIndex;

                rowIndexLabel.Text = "Record " + curRecd + " of " +
                                     (gvLocation.nrRecords).ToString();
            }
        }

        private void gvLocation_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < this._UnassignedItems.Rows.Count && 
                e.ColumnIndex >= 0 && e.ColumnIndex < gvLocation.Columns.Count)
            {
                if (gvLocation.Columns[e.ColumnIndex].Name == "Ticket")
                {
                    DataGridViewCell cell =
                    ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex];

                    cell.ToolTipText = this._UnassignedItems.Rows[e.RowIndex]["STORENUMBER"].ToString().PadLeft(5, '0')
                                       + this._UnassignedItems.DefaultView[e.RowIndex]["ICN_YEAR"]
                                       + this._UnassignedItems.DefaultView[e.RowIndex]["ICN_DOC"].ToString().PadLeft(6, '0')
                                       + this._UnassignedItems.DefaultView[e.RowIndex]["ICN_DOC_TYPE"]
                                       + this._UnassignedItems.DefaultView[e.RowIndex]["ICN_ITEM"].ToString().PadLeft(3, '0')
                                       + this._UnassignedItems.DefaultView[e.RowIndex]["ICN_SUB_ITEM"].ToString().PadLeft(2, '0');
                }
            }
        }

        private void gvLocation_AdjustColumnOther(int iColumnIndex, int iRowIndex)
        {
            try
            {
                Rectangle rect = gvLocation.GetCellDisplayRectangle(iColumnIndex, iRowIndex, true);
                _OtherTextBox.Size = rect.Size;
                _OtherTextBox.Location = rect.Location;

                _CurrentRowIndex = iRowIndex;

                string sControlValue = gvLocation[iColumnIndex,iRowIndex].Value != null ? gvLocation[iColumnIndex,iRowIndex].Value.ToString() : "";

                OtherTextBoxFocus(sControlValue);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void OtherTextBoxFocus(string sControlValue)
        {
            _OtherTextBox.Text = sControlValue;
            _OtherTextBox.Visible = true;
            _OtherTextBox.Focus();
        }

        private void gvLocation_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvLocation_UpdateRow(e.ColumnIndex, e.RowIndex);
        }

        private void gvLocation_UpdateRow(int iColumnIndex, int iRowIndex)
        {
            try
            {
                var sOtherData = string.Empty;

                if (iColumnIndex == gvLocation.Columns["location"].Index)
                {
                    string sControlValue = _OtherTextBox.Text.Trim().ToUpper();
                    gvLocation[iColumnIndex,iRowIndex].Value = sControlValue;
                    _OtherTextBox.Visible = false;
                    sOtherData = sControlValue;
                }
                else
                    sOtherData = Utilities.GetStringValue(gvLocation["location",iRowIndex].Value, "");

                string sAisleData = Utilities.GetStringValue(gvLocation["loc_aisle",iRowIndex].Value, "");
                string sShelfData = Utilities.GetStringValue(gvLocation["loc_shelf",iRowIndex].Value, "");
                string sUserID = Utilities.GetStringValue(gvLocation["userId",iRowIndex].Value, "");
                int iRecID = Utilities.GetIntegerValue(gvLocation["recID",iRowIndex].Value, 0);
                
                if (sAisleData != string.Empty || sShelfData != string.Empty || sOtherData != string.Empty)
                {
                    int iDx = _LocationData.FindIndex(ld => iRecID == ld.RecordID);
                    if (iDx >= 0)
                    {
                        LocationData myData = _LocationData[iDx];
                        myData.Aisle = sAisleData;
                        myData.Shelf = sShelfData;
                        myData.Location = sOtherData;
                        _LocationData.RemoveAt(iDx);
                        _LocationData.Insert(iDx, myData);
                    }
                    else
                    {
                        string sFilter = "recID=" + iRecID.ToString();
                        DataRow[] myRows = _UnassignedItems.Select(sFilter);

                        LocationData myData = new LocationData();
                        myData.Aisle = sAisleData;
                        myData.Shelf = sShelfData;
                        myData.Location = sOtherData;
                        myData.Icn_Doc = Utilities.GetStringValue(myRows[0]["icn_doc"], "");
                        myData.Icn_Doc_Type = Utilities.GetStringValue(myRows[0]["icn_doc_type"], "");
                        myData.Icn_Item = Utilities.GetStringValue(myRows[0]["icn_item"], "");
                        myData.Icn_Store = Utilities.GetStringValue(myRows[0]["icn_store"], "");
                        myData.Icn_Sub_Item = Utilities.GetStringValue(myRows[0]["icn_sub_item"], "");
                        myData.Icn_Year = Utilities.GetStringValue(myRows[0]["icn_year"], "");
                        myData.Status = Utilities.GetStringValue(myRows[0]["status_cd"], "");
                        myData.Store_Number = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                        myData.User_ID = sUserID;
                        myData.Description = Utilities.GetStringValue(myRows[0]["md_desc"], "");
                        myData.RecordID = iRecID;

                        _LocationData.Add(myData);
                    }
                }
                else
                {
                    int iDx = _LocationData.FindIndex(delegate(LocationData ld)
                                                      {
                                                          return iRecID == ld.RecordID;
                                                      });
                    if (iDx >= 0)
                    {
                        _LocationData.RemoveAt(iDx);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void UpdateDataGridData()
        {
            DataView myView = _UnassignedItems.DefaultView;

            if (viewTransactionComboBox.SelectedIndex == 0 && 
                !String.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.UserName))
            {
                myView.RowFilter = string.Format("userId='{0}'", GlobalDataAccessor.Instance.DesktopSession.UserName);                
            }
            else
                myView.RowFilter = string.Empty;

            gvLocation.DataSource = myView;
            gvLocation.Columns["status_cd"].DisplayIndex = 2;

            gvLocation.ScrollBars = ScrollBars.None;
            _GridTable = _UnassignedItems;
            GridPageBind(_UnassignedItems);
            lastSelectedIndex = viewTransactionComboBox.SelectedIndex;
        }

        private void GridPageBind(DataTable myTable)
        {
            if (myTable.Rows.Count == 0)
                return;

            _GridPages = myTable.Rows.Count / _GridPageMaxRows;
            int iDividend = 0;
            Math.DivRem(myTable.Rows.Count, _GridPageMaxRows, out iDividend);
            _GridPages = iDividend > 0 ? _GridPages + 1 : _GridPages;

            rowIndexLabel.Text = "Record 1 of " + (gvLocation.nrRecords).ToString();
            pageIndexLabel.Text = "Page 1 of " + gvLocation.pageCount;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            processSubmit();                
        }

        private void processSubmit() 
        {
            var ItemList = string.Empty;
            for (int i = 0; i < _LocationData.Count; i++)
            {
                ItemList += GeneratePipeString(i);
            }

            if (ItemList != string.Empty)
            {
                ItemList = ItemList.Remove(ItemList.Length - 1);

                string sErrorCode;
                string sErrorText;

                AssignMDSE.UpdateAssignedItems(ItemList, out sErrorCode, out sErrorText);
                if (sErrorCode == "0")
                {
                    // BZ-51 ------------------
                    if (viewTransactionComboBox.SelectedIndex == 2) 
                    {
                        clearGrid();
                        searchCriteria.Text = string.Empty;

                        searchCriteria.Focus();
                    }
                    else
                    //------------------- BZ-51 
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                }
                else if (sErrorCode == "1")
                {
                    MessageBox.Show(sErrorText, "Merchandise Location Assignment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                }
                else if (sErrorCode == "2")
                {
                    MessageBox.Show("The update operation has failed.  Please retry", "Merchandise Location Assignment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    submitButton.Focus();
                }
                else if (sErrorCode == "3")
                {
                    MessageBox.Show(sErrorText, "Merchandise Location Assignment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                }
            }
        }

        private void viewTransactionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (viewTransactionComboBox.Text == "")
                return;

            if (viewTransactionComboBox.Text.Equals("Relocate Merchandise"))
            {
                // show search panel
                searchType.SelectedIndex = 0;
                reassignSearchPanel.Visible = true;
                reassignSearchPanel.Height = 55;

                // adjust grid size to accomadate search panel
                _GridPageMaxRows = 18;
                gvLocation.Height = 420;
                gvLocation.Location = new Point(gvLocation.Location.X, 191);

                // reset and hide paging.
                _UnassignedItems.Clear();
                _GridPages = 0;
                panel1.Visible = false;
                rowIndexLabel.Visible = false;

                searchCriteria.Text = "";
                searchCriteria.Focus();
              
                searchButton.Enabled = false;

                UpdateDataGridData();
            }
            else
            {
                searchCriteria.MaxLength = 18;
                reassignSearchPanel.Visible = false;
                reassignSearchPanel.Height = 1;
                searchButton.Enabled = false;
                gvLocation.Height = 464;
                _GridPageMaxRows = 20;
                gvLocation.Location = new Point(gvLocation.Location.X, 135);
                Setup(false);
            }
        }

        #region Printing
        private void printButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Print_AssignPhysicalLocation myForm = new Print_AssignPhysicalLocation(_GridTable);
            myForm.ShowDialog();
            Cursor = Cursors.Default;
        }

        #endregion

        private string GeneratePipeString(int iLocationIndex)
        {
            var sPipeString = string.Empty;
            LocationData myData = _LocationData[iLocationIndex];

            sPipeString += myData.Store_Number + "|";
            sPipeString += myData.Icn_Store + "|";
            sPipeString += myData.Icn_Doc_Type + "|";
            sPipeString += myData.Icn_Doc.PadLeft(6, ' ') + "|";
            sPipeString += myData.Icn_Item + "|";
            sPipeString += myData.Icn_Sub_Item + "|";
            sPipeString += myData.Icn_Year + "|";
            sPipeString += myData.Aisle + "|";
            sPipeString += myData.Shelf + "|";
            sPipeString += myData.Location + "|";
            sPipeString += myData.User_ID + "|";
            sPipeString += myData.Status + "|";

            return sPipeString;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            // Add code here to validate search criteria based upon type
            if (this.searchType.SelectedIndex == 0 &&
                (this.searchCriteria.Text.Length != 18 && this.searchCriteria.Text.Length > 10))
                MessageBox.Show(Commons.GetMessageString("ICNInvalid"),
                                "Warning",
                                MessageBoxButtons.RetryCancel);

            else if (this.searchType.SelectedIndex >= 1 && this.searchCriteria.Text.Length < MIN_CRITERIA_LEN)
                MessageBox.Show(Commons.GetMessageString("TicketInvalid"),
                                "Warning",
                                MessageBoxButtons.RetryCancel);

            else
                Setup(true);
        }

        private void searchCriteria_lengthCheck(object sender, EventArgs e)
        {
            int minLen = MIN_CRITERIA_LEN;
            int maxLen = 99;
            string criteria = ((CustomTextBox)sender).Text;
            int curLen = criteria.Length;

            searchButton.Enabled = false;

            switch (this.searchType.SelectedIndex)
            {
                case 0: // ICN Search
                    if (curLen > 12) // full ICN
                    {
                        if (curLen == 18)
                            searchButton.Enabled = true;
                    }
                    else if (curLen >= 3 && criteria.IndexOf('.') > 0) // Short Code
                    {
                        searchButton.Enabled = true;
                    }
                    break;

                case 1:
                case 2:
                    searchButton.Enabled = true;
                    break;

                default:
                    if (curLen >= minLen && curLen <= maxLen)
                        searchButton.Enabled = true;
                    break;
            }

            if (_UnassignedItems.Rows.Count > 0)
                clearGrid();
        }

        private void searchTypeChanged(object sender, EventArgs e)
        {
            if (searchType.SelectedIndex == 0)
                searchCriteria.MaxLength = 18;
            else
                searchCriteria.MaxLength = 11;

            clearGrid();
            searchCriteria.Text = "";

            searchCriteria.Focus();
        }

        private void clearGrid()
        {
            int nrItems = _UnassignedItems.Rows.Count;
            _UnassignedItems.Clear();

            if (nrItems > 0)
                UpdateDataGridData();

            rowIndexLabel.Visible = false;            
        }
    }
}
