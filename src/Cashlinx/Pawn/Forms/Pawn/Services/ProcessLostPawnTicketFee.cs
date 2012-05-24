/********************************************************************
* CashlinxDesktop
* ProcessLostPawnTicketFee
* This form allows the calculation of lost ticket fee and the update to the
* database against the loan to indicate that the ticket is lost or stolen or destroyed
* Sreelatha Rengarajan 6/15/2009 Initial version
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.Services
{
    public partial class ProcessLostPawnTicketFee : Form
    {
        private List<CustLoanLostTicketFee> _custLoans;
        private decimal lostFeeAmount = 0.0M;
        

        public List<CustLoanLostTicketFee> CustomerLoans
        {
            get
            {
                return _custLoans;
            }
            set
            {
                _custLoans = value;
            }
        }

 
        public ProcessLostPawnTicketFee()
        {
            InitializeComponent();
        }

        private void ProcessLostPawnTicketFee_Load(object sender, EventArgs e)
        {
            if (_custLoans == null || !checkCustLoandata())
            {
                MessageBox.Show(Commons.GetMessageString("ProcessLostPawnTktNoCustLoans"));
                throw new ApplicationException("_custloans object is empty or loan/ticket number or store number is not populated");
            }
            else
            {
 
                    BusinessRuleVO _BusinessRule;
                    _BusinessRule = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO[Commons.PAWNTKTFEELOSTFEEBR];
                var sComponentValue = string.Empty;
                bool retVal = _BusinessRule.getComponentValue(Commons.PAWNTKTLOSTFEECOMPONENT, ref sComponentValue);

                    if (retVal)
                    {
                        //get the fee amount
                        try
                        {
                            lostFeeAmount = Convert.ToDecimal(sComponentValue);
                        }
                        catch (Exception)
                        {
                            if (FileLogger.Instance.IsLogDebug)
                            {
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Lost Ticket Fee amount is invalid. Value returned is " + sComponentValue);
                            }

                        }
                    }

                    dataGridViewPickupLoans.ColumnCount = 6;
                    dataGridViewPickupLoans.Columns[0].Name = "Loan";
                    dataGridViewPickupLoans.Columns[1].Name = "TktLost";
                    dataGridViewPickupLoans.Columns[2].Name = "TktStolen";
                    dataGridViewPickupLoans.Columns[3].Name = "TktDestroyed";
                    dataGridViewPickupLoans.Columns[4].Name = "TktLostFee";
                    dataGridViewPickupLoans.Columns[5].Name = "StoreNumber";

                    dataGridViewPickupLoans.Columns[0].HeaderText = "Loan Number";
                    dataGridViewPickupLoans.Columns[1].HeaderText = "Lost Ticket";
                    dataGridViewPickupLoans.Columns[2].HeaderText = "Stolen Ticket";
                    dataGridViewPickupLoans.Columns[3].HeaderText = "Destroyed Ticket";
                    dataGridViewPickupLoans.Columns[4].HeaderText = "Lost Ticket Fee";


                    dataGridViewPickupLoans.Columns[0].ReadOnly = true;
                    dataGridViewPickupLoans.Columns[4].ReadOnly = true;
                    dataGridViewPickupLoans.Columns[5].Visible = false;



                    for (int i = 0; i < _custLoans.Count; i++)
                    {
                        if (_custLoans[i].TicketLost)
                        {
                            DataGridViewTextBoxCell loannbrcell = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell tktlostfeecell = new DataGridViewTextBoxCell();
                            DataGridViewRadioButtonCell tktstolencell = new DataGridViewRadioButtonCell();
                            DataGridViewRadioButtonCell tktdestroyedcell = new DataGridViewRadioButtonCell();
                            DataGridViewRadioButtonCell tktlostcell = new DataGridViewRadioButtonCell();
                            DataGridViewTextBoxCell storenumbercell = new DataGridViewTextBoxCell();
                            loannbrcell.Value = _custLoans[i].LoanNumber;
                            if (_custLoans[i].LSDTicket == CustLoanLostTicketFee.LOSTTICKETTYPE)
                                tktlostcell.CHECKED = true;
                            else
                                tktlostcell.CHECKED = false;
                            if (_custLoans[i].LSDTicket == CustLoanLostTicketFee.STOLENTICKETTYPE)
                                tktstolencell.CHECKED = true;
                            else
                                tktstolencell.CHECKED = false;
                            if (_custLoans[i].LSDTicket == CustLoanLostTicketFee.DESTROYEDTICKETTYPE)
                                tktdestroyedcell.CHECKED = true;
                            else
                                tktdestroyedcell.CHECKED = false;
                            if (_custLoans[i].LSDTicket == "")
                                tktlostcell.CHECKED = true;
                            tktlostfeecell.Value = Commons.CURRENCYSIGN + lostFeeAmount;
                            storenumbercell.Value = _custLoans[i].StoreNumber;

                            DataGridViewRow dgRow = new DataGridViewRow();
                            dgRow.Cells.Insert(0, loannbrcell);
                            dgRow.Cells.Insert(1, tktlostcell);
                            dgRow.Cells.Insert(2, tktstolencell);
                            dgRow.Cells.Insert(3, tktdestroyedcell);
                            dgRow.Cells.Insert(4, tktlostfeecell);
                            dgRow.Cells.Insert(5, storenumbercell);
                            dataGridViewPickupLoans.Rows.Add(dgRow);
                        }
                    }
    
            }


        }

        private bool checkCustLoandata()
        {
            for (int i = 0; i < _custLoans.Count; i++)
            {
                if (_custLoans[i].LoanNumber == null || _custLoans[i].StoreNumber == null)
                {
                    return false;                    
                }
            }
            return true;
        }



        private void buttonContinue_Click(object sender, EventArgs e)
        {
                string errorCode;
                string errorMsg;
                string userId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                //Get the number of rows in the datagrid where the lost or stolen or destroyed ticket
                //values are checked
                int numRows = 0;
                List<string> storeNbr = new List<string>();
                List<string> tktNumber = new List<string>();
                List<string> lsdValue = new List<string>();
                foreach (DataGridViewRow dgvr in dataGridViewPickupLoans.Rows)
                {
                    dataGridViewPickupLoans.RowHeadersVisible = false;
                    DataGridViewTextBoxCell loannbrcell = (DataGridViewTextBoxCell)dgvr.Cells[0];
                    DataGridViewTextBoxCell storenumbercell = (DataGridViewTextBoxCell)dgvr.Cells[5];
                    DataGridViewRadioButtonCell tktstolencell = (DataGridViewRadioButtonCell)dgvr.Cells[2];
                    DataGridViewRadioButtonCell tktdestroyedcell = (DataGridViewRadioButtonCell)dgvr.Cells[3];
                    DataGridViewRadioButtonCell tktlostcell = (DataGridViewRadioButtonCell)dgvr.Cells[1];
                    if (tktstolencell.CHECKED || tktdestroyedcell.CHECKED || tktlostcell.CHECKED)
                    {
                        numRows++;
                        storeNbr.Add(storenumbercell.Value.ToString());
                        tktNumber.Add(loannbrcell.Value.ToString());
                        CustLoanLostTicketFee custLoanObject = getCustLoanObject(loannbrcell.Value.ToString());
                        if (custLoanObject.LoanNumber != null)
                        {
                            custLoanObject.LostTicketFee = lostFeeAmount;
                            if (tktstolencell.CHECKED)
                            {
                                custLoanObject.LSDTicket = CustLoanLostTicketFee.STOLENTICKETTYPE;
                                lsdValue.Add(CustLoanLostTicketFee.STOLENTICKETTYPE);
                            }
                            else if (tktlostcell.CHECKED)
                            {
                                custLoanObject.LSDTicket = CustLoanLostTicketFee.LOSTTICKETTYPE;
                                lsdValue.Add(CustLoanLostTicketFee.LOSTTICKETTYPE);
                            }
                            else if (tktdestroyedcell.CHECKED)
                            {
                                custLoanObject.LSDTicket = CustLoanLostTicketFee.DESTROYEDTICKETTYPE;
                                lsdValue.Add(CustLoanLostTicketFee.DESTROYEDTICKETTYPE);
                            }
                        }

                    }
                }
                if (numRows > 0)
                {
                    string[] storeNumbers = new string[numRows];
                    int[] ticketNumbers = new int[numRows];
                    string[] lsdFlag = new string[numRows];

                    for (int i = 0; i < numRows; i++)
                    {
                        storeNumbers[i] = storeNbr[i];
                        try
                        {
                            ticketNumbers[i] = Convert.ToInt32(tktNumber[i]);
                        }
                        catch (FormatException)
                        {
                            if (FileLogger.Instance.IsLogDebug)
                            {
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Ticket number is invalid. Value is " + tktNumber[i]);
                            }
                            throw new ApplicationException("Ticket number is not valid");

                        }
                        lsdFlag[i] = lsdValue[i];

                    }
                    DialogResult dgr = DialogResult.Retry;
                    do
                    {
                    bool retValue = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).updatePawnLostTicketFlag(new List<string>(storeNumbers), new List<int>(ticketNumbers), userId, new List<string>(lsdFlag), out errorCode, out errorMsg);                        
                    if (retValue)
                    {
                        MessageBox.Show("Lost Pawn ticket status successfully updated");
                        break;
                    }
                    else
                        dgr = MessageBox.Show(Commons.GetMessageString("ProcessLostTktFeeError"), "Error", MessageBoxButtons.RetryCancel);
                    } while (dgr == DialogResult.Retry);

                    if (dgr == DialogResult.Cancel)
                    {
                        throw new ApplicationException("Error when updating the database for the ticket to update lost ticket status");
                    }
                }
            
            this.Close();
            this.Dispose(true);
        }



        private void dataGridViewPickupLoans_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                checkData(e.RowIndex, e.ColumnIndex);
            }
        }

        private void checkData(int rowIdx, int colIdx)
        {
            DataGridViewRadioButtonCell tktstolencell = (DataGridViewRadioButtonCell)dataGridViewPickupLoans.Rows[rowIdx].Cells[2];
            DataGridViewRadioButtonCell tktdestroyedcell = (DataGridViewRadioButtonCell)dataGridViewPickupLoans.Rows[rowIdx].Cells[3];
            DataGridViewRadioButtonCell tktlostcell = (DataGridViewRadioButtonCell)dataGridViewPickupLoans.Rows[rowIdx].Cells[1];

            if (colIdx == 1)
            {
                if (tktlostcell.CHECKED)
                {
                    dataGridViewPickupLoans.Rows[rowIdx].Cells[4].Value = Commons.CURRENCYSIGN + lostFeeAmount;
                    tktstolencell.CHECKED = false;
                    tktdestroyedcell.CHECKED = false;
                }

            }
            else if (colIdx == 2)
            {
                if (tktstolencell.CHECKED)
                {
                    dataGridViewPickupLoans.Rows[rowIdx].Cells[4].Value = Commons.CURRENCYSIGN + lostFeeAmount;
                    tktlostcell.CHECKED = false;
                    tktdestroyedcell.CHECKED = false;
                }

            }
            else if (colIdx == 3)
            {
                if (tktdestroyedcell.CHECKED)
                {
                    dataGridViewPickupLoans.Rows[rowIdx].Cells[4].Value = Commons.CURRENCYSIGN + lostFeeAmount;
                    tktlostcell.CHECKED = false;
                    tktstolencell.CHECKED = false;
                }

            }



        }

        private CustLoanLostTicketFee getCustLoanObject(string loanNumber)
        {
            CustLoanLostTicketFee custLoan = _custLoans.Find(delegate(CustLoanLostTicketFee loanObj)
            {
                return (loanObj.LoanNumber == loanNumber);
            });
            return custLoan;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose(true);
        }

        private void dataGridViewPickupLoans_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
                {
                    checkData(e.RowIndex, e.ColumnIndex);
                }
            }

        }

        private void dataGridViewPickupLoans_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                DataGridViewRadioButtonCell tktstolencell = (DataGridViewRadioButtonCell)dataGridViewPickupLoans.Rows[e.RowIndex-1].Cells[2];
                DataGridViewRadioButtonCell tktdestroyedcell = (DataGridViewRadioButtonCell)dataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[3];
                DataGridViewRadioButtonCell tktlostcell = (DataGridViewRadioButtonCell)dataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[1];

                    if (tktlostcell.CHECKED)
                    {
                        dataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[4].Value = Commons.CURRENCYSIGN + lostFeeAmount;
                        tktstolencell.CHECKED = false;
                        tktdestroyedcell.CHECKED = false;
                    }

                    if (tktstolencell.CHECKED)
                    {
                        dataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[4].Value = Commons.CURRENCYSIGN + lostFeeAmount;
                        tktlostcell.CHECKED = false;
                        tktdestroyedcell.CHECKED = false;
                    }

                    if (tktdestroyedcell.CHECKED)
                    {
                        dataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[4].Value = Commons.CURRENCYSIGN + lostFeeAmount;
                        tktlostcell.CHECKED = false;
                        tktstolencell.CHECKED = false;
                    }

                    if (!tktlostcell.CHECKED && !tktstolencell.CHECKED && !tktdestroyedcell.CHECKED)
                        dataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[4].Value = 0;

            }
        }

     




    }
}
