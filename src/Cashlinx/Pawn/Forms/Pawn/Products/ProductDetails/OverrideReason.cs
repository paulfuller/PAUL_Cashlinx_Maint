using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Loan;

namespace Pawn.Forms.Pawn.Products.ProductDetails
{
    public partial class OverrideReason : Form
    {

        private BindingSource _bindingSource1;

        public OverrideReason()
        {
            InitializeComponent();
        }


        public List<OverrideTransaction> Transactions
        {
            get;
            set;
        }

        private void OverrideReason_Load(object sender, EventArgs e)
        {
            if (Transactions.Count > 0)
            {
                customLabelOverrideMsg.Text = "The following transactions need manager override.  " +
                    System.Environment.NewLine +
                "Unselect the transactions that you do not want to override.";

                _bindingSource1 = new BindingSource {DataSource = Transactions};
                customDataGridViewOverrideTransactions.AutoGenerateColumns = false;
                customDataGridViewOverrideTransactions.DataSource = _bindingSource1;
                customDataGridViewOverrideTransactions.Columns[1].DataPropertyName = "TicketNumber";
                //customDataGridViewOverrideTransactions.Columns[2].DataPropertyName = "ReasonForOverride";
                customDataGridViewOverrideTransactions.Columns[3].DataPropertyName = "OverrideType";
                customDataGridViewOverrideTransactions.Columns[4].DataPropertyName = "TransactionType";
                
                //set all the checkboxes to true
                foreach (DataGridViewRow dgvr in this.customDataGridViewOverrideTransactions.Rows)
                {
                    DataGridViewCheckBoxCell dgcell = (DataGridViewCheckBoxCell)dgvr.Cells[0];
                    dgcell.Value = true;

                }
                for(int i=0;i<Transactions.Count;i++)
                {

                    DataGridViewTextBoxCell dgCell2 = (DataGridViewTextBoxCell)customDataGridViewOverrideTransactions.Rows[i].Cells[2];
                    foreach(var s in Transactions[i].ReasonForOverride)
                    dgCell2.Value =s.Value;
                    

                }
            }
            else
            {
                this.customLabelOverrideMsg.Text = "No transactions selected";
                this.Close();
            }


        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Transactions = new List<OverrideTransaction>();
            Close();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            var messageToShow = new StringBuilder();
            Transactions = new List<OverrideTransaction>();
            foreach (DataGridViewRow dgvr in this.customDataGridViewOverrideTransactions.Rows)
            {
                DataGridViewCheckBoxCell dgcell = (DataGridViewCheckBoxCell)dgvr.Cells[0];
                
                if ((bool)dgcell.Value)
                {
                    messageToShow.Append(dgvr.Cells[1].Value.ToString());
                    messageToShow.Append("-");
                    messageToShow.Append(dgvr.Cells[2].Value.ToString());
                    messageToShow.Append(System.Environment.NewLine);
                    //Update the Transactions list with only the selected ones
                    //So the calling program would know
                    OverrideTransaction newTran = new OverrideTransaction();
                    newTran.ReasonForOverride = new List<Commons.StringValue>();
                    newTran.TicketNumber =
                        Utilities.GetIntegerValue(dgvr.Cells[1].Value.ToString(),
                                                  0);
                    
                    newTran.ReasonForOverride.Add(new Commons.StringValue(dgvr.Cells[2].Value.ToString()));
                    newTran.OverrideType = (ManagerOverrideType)dgvr.Cells[3].Value;
                    newTran.TransactionType = (ManagerOverrideTransactionType)dgvr.Cells[4].Value;
                                                      
                    Transactions.Add(newTran);
                }
            }

            if (Transactions.Count > 0)
            {
                List<ManagerOverrideType> overrideTypes = new List<ManagerOverrideType>();
                List<ManagerOverrideTransactionType> transactionTypes = new List<ManagerOverrideTransactionType>();
                List<int> ticketNumbers = new List<int>();
                foreach (OverrideTransaction tran in Transactions)
                {
                    overrideTypes.Add(tran.OverrideType);
                    transactionTypes.Add(tran.TransactionType);
                    ticketNumbers.Add(tran.TicketNumber);
                }
                ManageOverrides overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER)
                                                  {
                                                      MessageToShow = messageToShow.ToString(),
                                                      ManagerOverrideTypes=overrideTypes,
                                                      ManagerOverrideTransactionTypes=transactionTypes,
                                                      TransactionNumbers=ticketNumbers
                                                  };
                overrideFrm.ShowDialog(this);
                //If the override did not pass, send back 0 transactions
                //so the calling program would know that the override did not happen
                if (!(overrideFrm.OverrideAllowed))
                {
                    Transactions = new List<OverrideTransaction>();
                }

            }


            Close();


        }

        private void customDataGridViewOverrideTransactions_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (customDataGridViewOverrideTransactions.IsCurrentCellInEditMode)
                customDataGridViewOverrideTransactions.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

    }
}
