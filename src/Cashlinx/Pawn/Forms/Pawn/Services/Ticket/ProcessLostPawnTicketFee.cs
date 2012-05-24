/********************************************************************
* CashlinxDesktop
* ProcessLostPawnTicketFee
* This form allows the calculation of lost ticket fee 
* Sreelatha Rengarajan 6/15/2009 Initial version
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.Services.Ticket
{
    public partial class ProcessLostPawnTicketFee : Form
    {
        private List<CustLoanLostTicketFee> _custLoans;
        private decimal _lostFeeAmount;
        

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
            if (_custLoans == null || !CheckCustLoandata())
            {
                MessageBox.Show(Commons.GetMessageString("ProcessLostPawnTktNoCustLoans"));
                throw new ApplicationException("_custloans object is empty or loan/ticket number or store number is not populated");
            }
            BusinessRuleVO _BusinessRule;
            _BusinessRule = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO[Commons.PAWNTKTFEELOSTFEEBR];
            var sComponentValue = string.Empty;
            bool retVal = _BusinessRule.getComponentValue(Commons.PAWNTKTLOSTFEECOMPONENT, ref sComponentValue);

            if (retVal)
            {
                //get the fee amount
                try
                {
                    _lostFeeAmount = Convert.ToDecimal(sComponentValue);
                }
                catch (Exception)
                {
                    if (FileLogger.Instance.IsLogDebug)
                    {
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Lost Ticket Fee amount is invalid. Value returned is " + sComponentValue);
                    }

                }
            }

            customDataGridViewPickupLoans.ColumnCount = 6;
            customDataGridViewPickupLoans.Columns[0].Name = "Loan";
            customDataGridViewPickupLoans.Columns[1].Name = "TktLost";
            customDataGridViewPickupLoans.Columns[2].Name = "TktStolen";
            customDataGridViewPickupLoans.Columns[3].Name = "TktDestroyed";
            customDataGridViewPickupLoans.Columns[4].Name = "TktLostFee";
            customDataGridViewPickupLoans.Columns[5].Name = "StoreNumber";

            customDataGridViewPickupLoans.Columns[0].HeaderText = "Loan Number";
            customDataGridViewPickupLoans.Columns[1].HeaderText = "Lost Ticket";
            customDataGridViewPickupLoans.Columns[2].HeaderText = "Stolen Ticket";
            customDataGridViewPickupLoans.Columns[3].HeaderText = "Destroyed Ticket";
            customDataGridViewPickupLoans.Columns[4].HeaderText = "Lost Ticket Fee";


            customDataGridViewPickupLoans.Columns[0].ReadOnly = true;
            customDataGridViewPickupLoans.Columns[4].ReadOnly = true;
            customDataGridViewPickupLoans.Columns[5].Visible = false;



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
                    tktlostcell.CHECKED = _custLoans[i].LSDTicket == CustLoanLostTicketFee.LOSTTICKETTYPE;
                    tktstolencell.CHECKED = _custLoans[i].LSDTicket == CustLoanLostTicketFee.STOLENTICKETTYPE;
                    tktdestroyedcell.CHECKED = _custLoans[i].LSDTicket == CustLoanLostTicketFee.DESTROYEDTICKETTYPE;
                    if (string.IsNullOrEmpty(_custLoans[i].LSDTicket))
                        tktlostcell.CHECKED = true;
                    tktlostfeecell.Value = Commons.CURRENCYSIGN + _lostFeeAmount;
                    storenumbercell.Value = _custLoans[i].StoreNumber;

                    DataGridViewRow dgRow = new DataGridViewRow();
                    //BZ # 504
                    dgRow.ReadOnly = true;
                    //BZ # 504 end
                    dgRow.Cells.Insert(0, loannbrcell);
                    dgRow.Cells.Insert(1, tktlostcell);
                    dgRow.Cells.Insert(2, tktstolencell);
                    dgRow.Cells.Insert(3, tktdestroyedcell);
                    dgRow.Cells.Insert(4, tktlostfeecell);
                    dgRow.Cells.Insert(5, storenumbercell);
                    customDataGridViewPickupLoans.Rows.Add(dgRow);
                }
            }
        }

        private bool CheckCustLoandata()
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
            //string userId = CashlinxDesktopSession.Instance.UserName;
            //Get the number of rows in the datagrid where the lost or stolen or destroyed ticket
            //values are checked
            //int numRows = 0;
            List<string> storeNbr = new List<string>();
            List<string> tktNumber = new List<string>();
            List<string> lsdValue = new List<string>();
            foreach (DataGridViewRow dgvr in customDataGridViewPickupLoans.Rows)
            {
                customDataGridViewPickupLoans.RowHeadersVisible = false;
                DataGridViewTextBoxCell loannbrcell = (DataGridViewTextBoxCell)dgvr.Cells[0];
                DataGridViewTextBoxCell storenumbercell = (DataGridViewTextBoxCell)dgvr.Cells[5];
                DataGridViewRadioButtonCell tktstolencell = (DataGridViewRadioButtonCell)dgvr.Cells[2];
                DataGridViewRadioButtonCell tktdestroyedcell = (DataGridViewRadioButtonCell)dgvr.Cells[3];
                DataGridViewRadioButtonCell tktlostcell = (DataGridViewRadioButtonCell)dgvr.Cells[1];
                if (tktstolencell.CHECKED || tktdestroyedcell.CHECKED || tktlostcell.CHECKED)
                {
                    //numRows++;
                    storeNbr.Add(storenumbercell.Value.ToString());
                    tktNumber.Add(loannbrcell.Value.ToString());
                    CustLoanLostTicketFee custLoanObject = GetCustLoanObject(loannbrcell.Value.ToString());
                    if (custLoanObject.LoanNumber != null)
                    {
                        custLoanObject.LostTicketFee = _lostFeeAmount;
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
            
            Close();
            Dispose(true);
        }



        private void dataGridViewPickupLoans_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                CheckData(e.RowIndex, e.ColumnIndex);
            }
        }

        private void CheckData(int rowIdx, int colIdx)
        {
            DataGridViewRadioButtonCell tktstolencell = (DataGridViewRadioButtonCell)customDataGridViewPickupLoans.Rows[rowIdx].Cells[2];
            DataGridViewRadioButtonCell tktdestroyedcell = (DataGridViewRadioButtonCell)customDataGridViewPickupLoans.Rows[rowIdx].Cells[3];
            DataGridViewRadioButtonCell tktlostcell = (DataGridViewRadioButtonCell)customDataGridViewPickupLoans.Rows[rowIdx].Cells[1];

            if (colIdx == 1)
            {
                if (tktlostcell.CHECKED)
                {
                    customDataGridViewPickupLoans.Rows[rowIdx].Cells[4].Value = Commons.CURRENCYSIGN + _lostFeeAmount;
                    tktstolencell.CHECKED = false;
                    tktdestroyedcell.CHECKED = false;
                }

            }
            else if (colIdx == 2)
            {
                if (tktstolencell.CHECKED)
                {
                    customDataGridViewPickupLoans.Rows[rowIdx].Cells[4].Value = Commons.CURRENCYSIGN + _lostFeeAmount;
                    tktlostcell.CHECKED = false;
                    tktdestroyedcell.CHECKED = false;
                }

            }
            else if (colIdx == 3)
            {
                if (tktdestroyedcell.CHECKED)
                {
                    customDataGridViewPickupLoans.Rows[rowIdx].Cells[4].Value = Commons.CURRENCYSIGN + _lostFeeAmount;
                    tktlostcell.CHECKED = false;
                    tktstolencell.CHECKED = false;
                }

            }



        }

        private CustLoanLostTicketFee GetCustLoanObject(string loanNumber)
        {
            CustLoanLostTicketFee custLoan = _custLoans.Find(delegate(CustLoanLostTicketFee loanObj)
                                                                 {
                                                                     return (loanObj.LoanNumber == loanNumber);
                                                                 });
            return custLoan;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in customDataGridViewPickupLoans.Rows)
            {
                customDataGridViewPickupLoans.RowHeadersVisible = false;
                DataGridViewTextBoxCell loannbrcell = (DataGridViewTextBoxCell)dgvr.Cells[0];
                    CustLoanLostTicketFee custLoanObject = GetCustLoanObject(loannbrcell.Value.ToString());
                    if (custLoanObject.LoanNumber != null)
                    {
                        custLoanObject.LostTicketFee = 0;
                        custLoanObject.TicketLost = false;
                    }
            }

            Close();
            Dispose(true);
        }

        private void dataGridViewPickupLoans_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
                {
                    CheckData(e.RowIndex, e.ColumnIndex);
                }
            }

        }

        private void dataGridViewPickupLoans_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                DataGridViewRadioButtonCell tktstolencell = (DataGridViewRadioButtonCell)customDataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[2];
                DataGridViewRadioButtonCell tktdestroyedcell = (DataGridViewRadioButtonCell)customDataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[3];
                DataGridViewRadioButtonCell tktlostcell = (DataGridViewRadioButtonCell)customDataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[1];

                if (tktlostcell.CHECKED)
                {
                    customDataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[4].Value = Commons.CURRENCYSIGN + _lostFeeAmount;
                    tktstolencell.CHECKED = false;
                    tktdestroyedcell.CHECKED = false;
                }

                if (tktstolencell.CHECKED)
                {
                    customDataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[4].Value = Commons.CURRENCYSIGN + _lostFeeAmount;
                    tktlostcell.CHECKED = false;
                    tktdestroyedcell.CHECKED = false;
                }

                if (tktdestroyedcell.CHECKED)
                {
                    customDataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[4].Value = Commons.CURRENCYSIGN + _lostFeeAmount;
                    tktlostcell.CHECKED = false;
                    tktstolencell.CHECKED = false;
                }

                if (!tktlostcell.CHECKED && !tktstolencell.CHECKED && !tktdestroyedcell.CHECKED)
                    customDataGridViewPickupLoans.Rows[e.RowIndex - 1].Cells[4].Value = 0;

            }
        }

     




    }
}