/************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.Services.Rollover
 * Class:           LoanFees
 * 
 * Description      Popup of Pawn Loan Fees
 * 
 * History
 * David D Wise, Initial Development
 * Sreelatha Rengarajan 03/11/10 Fixed issue with fees not showing up if it was negative and
 * when cloning failed
 * 
 * **********************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;

namespace Support.Forms.Customer.Services.Rollover
{
    public partial class LoanFees : CustomBaseForm
    {
        private readonly PawnLoan pawnLoan;
        private readonly PawnLoan newPawnLoan;
        private readonly List<TupleType<FeeTypes, string, string>> feeTypes;

        public LoanFees(PawnLoan currentLoan, PawnLoan newLoan)
        {
            InitializeComponent();
            pawnLoan = Utilities.CloneObject(currentLoan);
            newPawnLoan = Utilities.CloneObject(newLoan);
            //Check to make sure that the cloning worked and if it
            //did not set it to be the same as newLoan
            if (newPawnLoan.TicketNumber == 0)
                newPawnLoan = newLoan;
            feeTypes = new List<TupleType<FeeTypes, string, string>>();
        }

        private void Setup()
        {
            int iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans.FindIndex(p => p.TicketNumber == pawnLoan.TicketNumber);

            if (iDx < 0)
            {
                iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.FindIndex(p => p.TicketNumber == pawnLoan.TicketNumber);
                if (iDx < 0)
                {
                    MessageBox.Show(
                        "Ticket Number is not associated to a Loan.",
                        "Ticket Number Lookup",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    Close();
                }
            }
            rolloverLoanHeaderLabel.Text = "Fees For Pawn Loan " + pawnLoan.TicketNumber;

            foreach(var fee in pawnLoan.OriginalFees)
            {
                feeTypes.Add(new TupleType<FeeTypes, string, string>(fee.FeeType, fee.Value.ToString(), "N/A"));
            }

            // populate Panel
            var label = new Label
                          {
                              Text = ""
                          };
            AddToLayoutPanel(label);
            label = new Label
                    {
                        TextAlign = ContentAlignment.MiddleRight,
                        Text = "Current Loan"
                    };
            AddToLayoutPanel(label);
            label = new Label
                    {
                        TextAlign = ContentAlignment.MiddleRight,
                        Text = "New Loan"
                    };
            AddToLayoutPanel(label);

            //SR 01/18/2009
            //The fees are already calculated for the loan when this form
            //is invoked so no need to recalculate
          /*  var underwritePawnLoanVO = new UnderwritePawnLoanVO();

            newPawnLoan.Fees = new List<Fee>();
            newPawnLoan.InterestAmount = 0;
            newPawnLoan.ServiceCharge = 0;

            var siteId = Utilities.CloneObject(GlobalDataAccessor.Instance.CurrentSiteId);
            siteId.Date = ShopDateTime.Instance.ShopDate;
            siteId.LoanAmount = pawnLoan.Amount;

            PawnLoan currentPawnLoan = StoreLoans.GetCurrentLoanFees(siteId, newPawnLoan, out underwritePawnLoanVO);
            */
            if (newPawnLoan.Fees == null)
                return;
            foreach (var fee in newPawnLoan.Fees)
            {
                var fee1 = fee;
                iDx = feeTypes.FindIndex(k => k.Left == fee1.FeeType);
                if (iDx >= 0)
                {
                    var curFee = feeTypes[iDx];
                    var newFee = new TupleType<FeeTypes, string, string>(curFee.Left, curFee.Mid, fee1.Value.ToString());
                    feeTypes.RemoveAt(iDx);
                    feeTypes.Insert(iDx, newFee);
                }
                else
                {
                    var newFee = new TupleType<FeeTypes, string, string>(fee1.FeeType, "N/A", fee1.Value.ToString());
                    feeTypes.Add(newFee);
                }
            }

            var iFeeCnt = 0;
            var currFeeTypes = from feetypes in feeTypes
                               where feetypes.Left != FeeTypes.INTEREST
                               select feetypes;
            foreach (var fee in currFeeTypes)
            {
                
                if (Utilities.GetDecimalValue(fee.Right, -1) >= 0 || Utilities.GetStringValue(fee.Right,"") == "N/A")
                {
                    iFeeCnt++;
                    label = new Label
                            {
                                TextAlign = ContentAlignment.MiddleLeft,
                                Text = GetFeeTypeText(fee.Left)
                            };
                    AddToLayoutPanel(label);
                    label = new Label
                            {
                                TextAlign = ContentAlignment.MiddleRight,
                                Text = String.Format("{0:C}", Utilities.GetDecimalValue(fee.Mid, 0))
                            };
                    AddToLayoutPanel(label);
                    label = new Label
                            {
                                TextAlign = ContentAlignment.MiddleRight,
                                Text =
                                    String.Format("{0:C}", Utilities.GetDecimalValue(fee.Right, 0))
                            };
                    AddToLayoutPanel(label);
                }
            }

            if (iFeeCnt == 0)
            {
                label = new Label
                        {
                            TextAlign = ContentAlignment.MiddleLeft,
                            Text = "None"
                        };
                AddToLayoutPanel(label);
                label = new Label
                        {
                            TextAlign = ContentAlignment.MiddleRight,
                            Text = String.Format("{0:C}", 0)
                        };
                AddToLayoutPanel(label);
                label = new Label
                        {
                            TextAlign = ContentAlignment.MiddleRight,
                            Text = String.Format("{0:C}", 0)
                        };
                AddToLayoutPanel(label);
            }
        }

        private static string GetFeeTypeText(FeeTypes feeType)
        {
            var titleText = string.Empty;

            try
            {
                titleText = GlobalDataAccessor.Instance.DesktopSession.ServiceFeeTypes.Find(f => f.Left == feeType).Right;
            }
            catch (System.ArgumentException)
            {
                titleText = "";
            }

            return titleText;
        }

        private void AddToLayoutPanel(Control control)
        {
            feesTablePanel.Controls.Add(control);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoanFees_Load(object sender, EventArgs e)
        {
            Setup();
        }
    }
}
