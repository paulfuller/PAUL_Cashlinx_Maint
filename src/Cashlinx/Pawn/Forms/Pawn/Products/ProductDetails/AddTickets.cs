/****************************************************************************
* Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products.ProductDetails
* Class:           AddTickets
* 
* Description      Popup Form to add tickets to existing Pawn Loan Items of
*                  a Customer.
* 
* History
*  David D Wise, Initial Development
*  no ticket 3/4/2010 - S. Murphy need store to default to current store if none is entered
*  Add ticket validations 2/9/2011 - Madhu added validations to fix BZ # 147
*  ****************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Products.ProductDetails
{
    public partial class AddTickets : Form
    {
        private List<PawnLoan> _AddedPawnLoans;
        private bool _SetupInProgress;

        public delegate void AddHandler(List<PawnLoan> addedPawnLoans);

        public event AddHandler AddPawnLoans;
        private Controller_ProductServices parentWindow;

        public AddTickets(Controller_ProductServices parentWindow)
        {
            //Madhu - Fix BZ defect 147
            this.parentWindow = parentWindow;
            _SetupInProgress = true;
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            _SetupInProgress = true;
            _AddedPawnLoans = new List<PawnLoan>();
            TicketsDataGridView.Rows.Add();
            //clearButton.Enabled = false;
            addButton.Enabled = false;
            _SetupInProgress = false;

            TicketsDataGridView.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(TicketsDataGridView_EditingControlShowing);
        }

        //Madhu - Fix BZ defect 147 - to prevent user entering chars in 
        // store number and ticket numbet columns
        private void TicketsDataGridView_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
                if (e.KeyChar != (char)Keys.Back)
                    e.Handled = true; 
        }

        //Madhu BZ # 147
        /*private void TicketsDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
        if (e.ColumnIndex == TicketsDataGridView.Columns[StoreColumn.Name].Index && e.RowIndex >= 0)
        {
        string sStoreNumber = Utilities.GetStringValue(TicketsDataGridView[StoreColumn.Name, e.RowIndex].Value, string.Empty);
        string sTicketNumber = Utilities.GetStringValue(TicketsDataGridView[TicketColumn.Name, e.RowIndex].Value, "");
        //no ticket - S. Murphy need store to default to current store if none is entered                
        if (string.IsNullOrEmpty(sStoreNumber))
        sStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

        if (sStoreNumber != "" && sTicketNumber != "")
        {
        TicketsDataGridView[StoreColumn.Name, e.RowIndex].Value = "";
        TicketsDataGridView[TicketColumn.Name, e.RowIndex].Value = "";
        }
        } 
        } */

        private void TicketsDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string sStoreNumber = Utilities.GetStringValue(TicketsDataGridView["StoreColumn",e.RowIndex].Value, string.Empty);
            string sTicketNumber = Utilities.GetStringValue(TicketsDataGridView["TicketColumn",e.RowIndex].Value, "");

            //no ticket - S. Murphy need store to default to current store if none is entered
            if (string.IsNullOrEmpty(sStoreNumber))
                sStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

            TicketsDataGridView["FindColumn",e.RowIndex].ReadOnly = !(sStoreNumber != string.Empty && sTicketNumber != string.Empty);
            //EnableAddButton();
        }

        void TicketsDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(TicketsDataGridView_KeyDown); 

            if (_SetupInProgress)
                return;
            /*if (TicketsDataGridView.CurrentCell.ColumnIndex == TicketsDataGridView.Columns[StoreColumn.Name].Index)
            {
            ((TextBox)e.Control).TextChanged += new EventHandler(StoreColumn_TextChanged);
            }*/
        }

        /*void StoreColumn_TextChanged(object sender, EventArgs e)
        {
        if (TicketsDataGridView.CurrentCell.RowIndex >= 0
        && TicketsDataGridView.CurrentCell.ColumnIndex == TicketsDataGridView.Columns[StoreColumn.Name].Index)
        ScanParse(TicketsDataGridView.CurrentCell.RowIndex, ((TextBox)sender).Text);
             
        }*/

        // Parses captured (or manually-entered) text and store in specific 
        // form fields
        private void ScanParse(int iRowIndex, string sBarCodeData)
        {
            if (sBarCodeData == "" || sBarCodeData.Length < 5)
                return;

            try
            {
                TicketsDataGridView[StoreColumn.Name,iRowIndex].Value = sBarCodeData.Substring(0, 5);
                string sTicketNumber = Utilities.GetStringValue(TicketsDataGridView["TicketColumn",iRowIndex].Value, "");

                if (sBarCodeData.Length == 5 && sTicketNumber == "")
                {
                    TicketsDataGridView.CurrentCell = TicketsDataGridView[TicketColumn.Name,iRowIndex];
                    return;
                }
                TicketsDataGridView[TicketColumn.Name,iRowIndex].Value = sBarCodeData.Substring(5);
            }
            catch (Exception e)
            {
                Console.WriteLine("error-->" + e.Message);
            }
        }

        private void TicketsDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == TicketsDataGridView.Columns["FindColumn"].Index && e.RowIndex >= 0)
            {
                if (TicketsDataGridView["FindColumn",e.RowIndex].ReadOnly)
                    return;

                string sStoreNumber = Utilities.GetStringValue(TicketsDataGridView["StoreColumn",e.RowIndex].Value, string.Empty);
                int iTicketNumber = Utilities.GetIntegerValue(TicketsDataGridView["TicketColumn",e.RowIndex].Value, 0);

                //Duplicate check -- Madhu - BZ # 147 - prevent adding if it already exists in the grid
                bool exists = false;
                int existingTicketNum = 0;
                foreach (DataGridViewRow dr in TicketsDataGridView.Rows)
                {
                    existingTicketNum = Utilities.GetIntegerValue(dr.Cells["TicketColumn"].Value, 0);
                    if (dr.Index != e.RowIndex && existingTicketNum != 0 && iTicketNumber != 0 
                        && existingTicketNum == iTicketNumber)
                    {
                        MessageBox.Show("Loan Number " + existingTicketNum + " has already been added in this list.");
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    if (!string.IsNullOrEmpty(iTicketNumber.ToString()))
                    {
                        exists = parentWindow.SearchGrid(iTicketNumber.ToString());
                        if (exists)
                            MessageBox.Show("Loan Number " + iTicketNumber + " has already been added.");
                    }
                }

                if (exists)
                    return;
                //duplicate check end

                //no ticket - S. Murphy need store to default to current store if none is entered
                if (string.IsNullOrEmpty(sStoreNumber))
                    sStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

                // Place Holder for Stored Proc call
                PawnLoan pawnLoan = new PawnLoan();
                PawnAppVO pawnAppVO = new PawnAppVO();
                CustomerVO customerVO = new CustomerVO();
                var sErrorCode = string.Empty;
                var sErrorText = string.Empty;

                if (CustomerLoans.GetPawnLoan(GlobalDataAccessor.Instance.DesktopSession, Convert.ToInt32(sStoreNumber), iTicketNumber, "0", StateStatus.BLNK, true,
                                              out pawnLoan, out pawnAppVO, out customerVO, out sErrorCode, out sErrorText))
                {
                    if (pawnLoan != null && pawnLoan.LoanStatus != ProductStatus.IP)
                    {
                        //TicketsDataGridView["NameColumn", e.RowIndex].Style.ForeColor = Color.Red;
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.ForeColor = Color.Red;
                        TicketsDataGridView["NameColumn",e.RowIndex].Style.ApplyStyle(style);

                        TicketsDataGridView["NameColumn",e.RowIndex].Value = "Invalid Ticket";
                        //Madhu fix for BZ # 147
                        addButton.Enabled = false;
                    }
                    else
                    {
                        //Madhu fix for BZ # 147
                        EnableAddButton();
                        //addButton.Enabled = true;

                        if (customerVO.LastName != "")
                        {
                            TicketsDataGridView["NameColumn",e.RowIndex].Value = (
                            customerVO.FirstName
                            + " "
                            + customerVO.MiddleInitial + " "
                            + customerVO.LastName
                            ).Replace("  ", " ");
                            if (pawnLoan != null)
                            {
                                if (_AddedPawnLoans.FindIndex(delegate(PawnLoan pl)
                                                              {
                                                                  return pl.TicketNumber == pawnLoan.TicketNumber;
                                                              }) < 0)
                                    _AddedPawnLoans.Add(pawnLoan);
                            }
                        }
                    }
                    if (TicketsDataGridView.CurrentRow != null)

                    //no ticket - S. Murphy need store to default to current store if none is entered
                        if (string.IsNullOrEmpty(sStoreNumber))
                            sStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

                    if (TicketsDataGridView.CurrentRow.Index == e.RowIndex
                        && TicketsDataGridView.Rows.Count - 1 == e.RowIndex
                        && sStoreNumber != "" && iTicketNumber != 0)
                    {
                        TicketsDataGridView.Rows.Add();
                        //Madhu BZ # 147
                        TicketsDataGridView.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(TicketsDataGridView_EditingControlShowing);
                        foreach (DataGridViewRow dr in TicketsDataGridView.Rows)
                        {
                            string sStoreNum = Utilities.GetStringValue(dr.Cells["StoreColumn"].Value);
                            int iTicketNum = Utilities.GetIntegerValue(dr.Cells["TicketColumn"].Value);

                            if (string.IsNullOrEmpty(sStoreNum) || iTicketNum == 0)
                                dr.Cells["FindColumn"].ReadOnly = true;
                        }
                    }
                }
                else
                {
                    //Madhu fix for BZ # 147
                    if (TicketsDataGridView != null && TicketsDataGridView.Rows.Count <= 1)
                        addButton.Enabled = false;

                    MessageBox.Show("A Ticket Number was not found using the Store Number entered.", "Lookup Ticket Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void EnableAddButton()
        {
            addButton.Enabled = false;

            foreach (DataGridViewRow myRow in TicketsDataGridView.Rows)
            {
                string sStoreNumber = Utilities.GetStringValue(myRow.Cells["StoreColumn"].Value, "");
                string sTicketNumber = Utilities.GetStringValue(myRow.Cells["TicketColumn"].Value, "");
                string sNameColumn = Utilities.GetStringValue(myRow.Cells["NameColumn"], "");
                //no ticket - S. Murphy need store to default to current store if none is entered
                if (string.IsNullOrEmpty(sStoreNumber))
                    sStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber; 

                if (sStoreNumber != "" && sTicketNumber != "" && sNameColumn != "")
                {
                    addButton.Enabled = true;
                    clearButton.Enabled = true;
                    break;
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            TicketsDataGridView.Rows.Clear();
            Setup();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddPawnLoans(_AddedPawnLoans);
            this.Close();
        }
    }
}
