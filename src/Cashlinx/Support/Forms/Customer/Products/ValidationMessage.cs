using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Support.Forms.Customer.Products
{
    public partial class ValidationMessage : Form
    {
        public ValidationMessage()
        {
            InitializeComponent();
            IneligibleLoanMessage = string.Empty;
            SelectedLoansCount = 0;
            PFIELoanMessage = string.Empty;
        }

        public List<PawnLoan> PFIELoans { get; set; }
        public string PFIELoanMessage { private get; set; }

        public bool ContinueService { get; set; }
        public string IneligibleLoanMessage { private get; set; }
        public int SelectedLoansCount { private get; set; }

        private void customButtonYes_Click(object sender, EventArgs e)
        {
            ContinueService = true;
            Close();
        }

        private void ValidationMessage_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (IneligibleLoanMessage.Length != 0 || PFIELoanMessage.Length != 0)
            {
                var messageToShow = string.Empty;
                messageToShow = IneligibleLoanMessage + PFIELoanMessage +
                                Environment.NewLine;
                var pfieLoanCount = 0;
                //If there are PFIE loans the refresh button is shown
                if (PFIELoans != null && PFIELoans.Count > 0)
                {
                    pfieLoanCount = PFIELoans.Count;
                    this.customButtonRefresh.Visible = true;
                }
                else
                {
                    this.customButtonRefresh.Visible = false;
                }
                //if the number of selected loans is more than 0 and
                //if the selected loans is more than the ones on PFIE
                //Show the continue with the others - yes or no buttons
                //otherwise show the close button
                if (SelectedLoansCount > 0 && (SelectedLoansCount > pfieLoanCount))
                {
                    messageToShow += Commons.GetMessageString("ValidationForServicesFailedMessage");
                    this.customButtonNo.Visible = true;
                    this.customButtonYes.Visible = true;
                }
                else
                {
                    this.customButtonNo.Text = "Cancel";
                    this.customButtonNo.Visible = true;
                    this.customButtonYes.Visible = false;
                }
                this.customLabelMessage.Text = messageToShow;
            }
            else
            {
                Close();
            }
        }

        private void customButtonRefresh_Click(object sender, EventArgs e)
        {
            //call check for temp status on all the pfie loans
            var pfieTicketNumbers = new List<int>();
            var pfieStoreNumbers = new List<string>();
            foreach (var pl in PFIELoans)
            {
                pfieTicketNumbers.Add(pl.TicketNumber);
                pfieStoreNumbers.Add(pl.OrgShopNumber);
            }
            DataTable currentTempStatus;
            var errorCode = string.Empty;
            var errorMsg = string.Empty;
            var pfieLoanMessage = new StringBuilder();
            var retval = CustomerLoans.CheckForTempStatus(pfieTicketNumbers, pfieStoreNumbers, out currentTempStatus, out errorCode, out errorMsg);
            if (currentTempStatus != null && currentTempStatus.Rows.Count > 0)
            {
                var nonPFIETicketNumbers = new List<int>();
                foreach (DataRow dr in currentTempStatus.Rows)
                {
                    //check if the loan still has PFIE status
                    if (!(dr[Tempstatuscursor.TEMPSTATUS].ToString().Contains("PFIE")))
                    {
                        nonPFIETicketNumbers.Add(Utilities.GetIntegerValue(dr[Tempstatuscursor.TICKETNUMBER], 0));
                    }
                    else
                    {
                        pfieLoanMessage.Append(dr[Tempstatuscursor.TICKETNUMBER] + " is being processed by PFI.");
                    }
                }
                if (nonPFIETicketNumbers.Count > 0)
                {
                    foreach (var i in nonPFIETicketNumbers)
                    {
                        var i1 = i;
                        var index = PFIELoans.FindIndex(loan => loan.TicketNumber == i1);
                        if (index >= 0)
                        {
                            PFIELoans.RemoveAt(index);
                        }
                    }
                }
            }
            else
            {
                BasicExceptionHandler.Instance.AddException("Error fetching temp status for loans", new ApplicationException());
            }
            PFIELoanMessage = pfieLoanMessage.ToString();
            LoadData();
        }

        private void customButtonNo_Click(object sender, EventArgs e)
        {
            ContinueService = false;
            Close();
        }
    }
}