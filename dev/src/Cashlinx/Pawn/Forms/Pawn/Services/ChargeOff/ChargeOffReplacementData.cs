using System;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.Services.ChargeOff
{
    public partial class ChargeOffReplacementData : CustomBaseForm
    {
        public string CustNumber;
        public string ReplacedICN;
        public ChargeOffReplacementData()
        {
            InitializeComponent();
        }

        private void customButtonFind_Click(object sender, EventArgs e)
        {
            PawnLoan pawnLoan;
            CustomerVO customerVO;
            CustNumber = string.Empty;
            ReplacedICN = string.Empty;
            string errorCode;
            string errorText;
            var retVal = CustomerLoans.GetPawnLoanLite(GlobalDataAccessor.Instance.DesktopSession, Utilities.GetIntegerValue(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                Utilities.GetIntegerValue(customTextBoxLoanNo.Text), "0", StateStatus.BLNK, true, out pawnLoan, out customerVO, out errorCode, out errorText);
            if (!retVal || pawnLoan == null)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error trying to get loan data" + errorText);
                MessageBox.Show("Error trying to get loan details for the number entered.");
                return;
            }
            var itemSelectionFrm = new ChargeOffItemSelection();
            itemSelectionFrm.ReplaceLoan = pawnLoan;
            itemSelectionFrm.ReplaceCustomer = customerVO;
            itemSelectionFrm.ShowDialog();

            if (itemSelectionFrm.ReplacedICN != null && itemSelectionFrm.ReplacedICN.Count > 0)
            {
                customLabelCustName.Text = customerVO.CustomerName;
                var rICN = new StringBuilder();
                var i = 1;
                foreach (var s in itemSelectionFrm.ReplacedICN)
                {
                    rICN.Append(s);
                    if (i != itemSelectionFrm.ReplacedICN.Count)
                    {
                        rICN.Append(",");
                    }
                    i++;
                }
                customLabelReplacedICN.Text=rICN.ToString();
                CustNumber = customerVO.CustomerNumber;
            }
 
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            
            ReplacedICN = customLabelReplacedICN.Text;
            this.Close();

        }
    }
}
