/****************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products.ProductHistory
 * Class:           ProductHistory_Dialog
 * 
 * Description      Popup Form to show specific information of a Customer
 *                  Pawn Loan.
 * 
 * History
 * David D Wise, Initial Development
 * 
 * Fixes/Mods
 *  PWNU00000220 S.Murphy populate PH_SuggestedAmountColumn and PH_LocationColumn
 *****************************************************************************/

using System;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Objects.Pawn;
using CashlinxDesktop.UserControls;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls.LoanDetailPanels;
using LoanBasicInfoPanel = Pawn.Forms.UserControls.ProductHistoryPanels.LoanBasicInfoPanel;
using LoanChargesPanel = Pawn.Forms.UserControls.ProductHistoryPanels.LoanChargesPanel;
using LoanExtensionInfoPanel = Pawn.Forms.UserControls.ProductHistoryPanels.LoanExtensionInfoPanel;
using LoanPFIPanel = Pawn.Forms.UserControls.ProductHistoryPanels.LoanPFIPanel;

namespace Pawn.Forms.Pawn.Products.ProductHistory
{
    public partial class DynamicProductHistory_Dialog : Form
    {
        private PawnLoan _pawnLoan;
        private int _receiptIdx;        //Index of the receipt that was selected.

        public NavBox NavControlBox;

        public DynamicProductHistory_Dialog(PawnLoan pawnLoan, int receiptIdx)
        {
            InitializeComponent();
                        
            _pawnLoan = pawnLoan;
            _receiptIdx = receiptIdx;
        }

        private void PH_CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DynamicProductHistory_Dialog_Load(object sender, EventArgs e)
        {
            //On load the build the table dynamically.
            BuildTable();
        }

        /// <summary>
        /// Builds the table and adds panels based on the event type
        /// related to the receipt that was selected.  These
        /// user controls that make up this screen can be found under 
        /// UserControls project directory for Cashlinx desktop.
        /// </summary>
        private void BuildTable()
        {
            //PAYDOWN EVENT
            if (_pawnLoan.Receipts[_receiptIdx].Event ==
                ReceiptEventTypes.Paydown.ToString())
            {

            }
            //EXTEND EVENT
            else if (_pawnLoan.Receipts[_receiptIdx].Event ==
                     ReceiptEventTypes.Extend.ToString())
            {                
                tblContentLayout.Controls.Add(new LoanBasicInfoPanel(_pawnLoan, _receiptIdx), 0, 0);
                tblContentLayout.Controls.Add(new LoanExtensionInfoPanel(_pawnLoan, _receiptIdx), 1, 0);
                tblContentLayout.Controls.Add(new LoanPFIPanel(_pawnLoan, _receiptIdx), 2, 0);
            }
            //PICKUP EVENT
            else if (_pawnLoan.Receipts[_receiptIdx].Event ==
                     ReceiptEventTypes.Pickup.ToString())
            {
                tblContentLayout.Controls.Add(new LoanBasicInfoPanel(_pawnLoan, _receiptIdx), 0, 0);
                tblContentLayout.Controls.Add(new LoanPickupPanel(_pawnLoan, _receiptIdx), 1, 0);
                tblContentLayout.Controls.Add(new LoanChargesPanel(_pawnLoan, _receiptIdx), 2, 0);

                //If there's a gun involved then show the firearm pickup ID panel
                if (_pawnLoan.GunInvolved)
                {
                    tblContentLayout.Controls.Add(new LoanFireamPickupPanel(_pawnLoan, _receiptIdx), 0, 1);
                }
            }
            //RENEW EVENT
            else if (_pawnLoan.Receipts[_receiptIdx].Event ==
                     ReceiptEventTypes.Renew.ToString())
            {
                tblContentLayout.Controls.Add(new LoanBasicInfoPanel(_pawnLoan, _receiptIdx), 0, 0);
                tblContentLayout.Controls.Add(new LoanRenewPanel(_pawnLoan, _receiptIdx), 1, 0);
                tblContentLayout.Controls.Add(new LoanChargesPanel(_pawnLoan, _receiptIdx), 2, 0);
            }
            //NO EVENT DEFINED
            else
            {
                //Maybe should just show a default view, rather than throw exception?
                throw new NotImplementedException("Receipt event type not yet defined for product history details.");
            }
        }

    }
}
