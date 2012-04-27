/**************************************************************************************************************
* CashlinxDesktop
* CustomerHoldInfo
* This form is used to get the release date and reason for hold for the selected transactions
* that the user wishes to put on customer hold
* Sreelatha Rengarajan 8/6/2009 Initial version
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Customer.ItemRelease
{
    public partial class CustomerHoldReleaseInfo : Form
    {
        public NavBox NavControlBox;
        private Form ownerfrm;
        private List<HoldData> custHolds;
        BindingSource bindingSource1;


        public CustomerHoldReleaseInfo()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }

        private void CustomerHoldInfo_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;

            custHolds = GlobalDataAccessor.Instance.DesktopSession.HoldsData;
            bindingSource1 = new BindingSource();
            bindingSource1.DataSource = custHolds;
            this.customDataGridViewSelectedTransactions.AutoGenerateColumns = false;
            this.customDataGridViewSelectedTransactions.DataSource = bindingSource1;
            this.customDataGridViewSelectedTransactions.Columns[0].DataPropertyName = "TicketNumber";
            this.customDataGridViewSelectedTransactions.Columns[1].DataPropertyName = "HoldDate";
            this.customDataGridViewSelectedTransactions.Columns[2].DataPropertyName = "ReleaseDate";
            this.customDataGridViewSelectedTransactions.Columns[3].DataPropertyName = "UserId";
            this.customDataGridViewSelectedTransactions.Columns[4].DataPropertyName = "HoldComment";


  
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
           if (RemoveTempStatusOnLoans())
            {
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "Back";
            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
           else
           {
               BasicExceptionHandler.Instance.AddException("Error when trying to remove temp status on selected transactions", new ApplicationException());
           }
        }

        private bool RemoveTempStatusOnLoans()
        {
            List<int> _selectedLoanNumbers = new List<int>();
            List<string> _selectedRefTypes=new List<string>();
            foreach (var custHold in custHolds)
            {
                _selectedLoanNumbers.Add(Utilities.GetIntegerValue(custHold.TicketNumber));
                _selectedRefTypes.Add(Utilities.GetStringValue(custHold.RefType));
            }
            string strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            string errorCode;
            string errorMsg;
            bool retVal = StoreLoans.UpdateTempStatus(_selectedLoanNumbers, StateStatus.BLNK,
                                                      strStoreNumber, true, _selectedRefTypes, out errorCode, out errorMsg);
            return retVal;

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            bool retVal = RemoveTempStatusOnLoans();
            if (!retVal)
                BasicExceptionHandler.Instance.AddException("Error when trying to remove temp status on selected transactions", new ApplicationException());
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }
        
 


  

        private void buttonSubmit_Click(object sender, EventArgs e)
        {

            bool returnValue = false;
            DialogResult dgr=DialogResult.Retry;
            do
            {
                returnValue = HoldsProcedures.RemoveCustomerHolds(custHolds, this.richTextBoxReason.Text);
                if (returnValue)
                {
                    MessageBox.Show("Selected loans released from customer hold successfully");
                    break;                    
                }
                else
                {
                    dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                }

            } while (dgr == DialogResult.Retry);
            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void dataGridViewSelectedTransactions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 1)
                {
                    e.FormattingApplied = true;
                    DateTime transactionDate = Utilities.GetDateTimeValue(e.Value.ToString(), DateTime.MaxValue);
                    e.Value = transactionDate.FormatDate();

                }
            }
        }
    }
}
