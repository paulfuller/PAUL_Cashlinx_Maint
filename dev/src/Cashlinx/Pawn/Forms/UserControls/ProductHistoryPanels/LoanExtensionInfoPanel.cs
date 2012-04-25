using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.UserControls.ProductHistoryPanels
{
    public partial class LoanExtensionInfoPanel : LoanDetailBasePanel
    {
        //public LoanStatusInfoPanel()
        //{
        //    InitializeComponent();
        //}

        public LoanExtensionInfoPanel(PawnLoan pawnLoan, int receiptIdx)
            : base(pawnLoan, receiptIdx)
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            this.PH_CurrentLoanStatusText.Text = _pawnLoan.LoanStatus.ToString();
            this.PH_MadeByEmployeeText.Text = _pawnLoan.CreatedBy;

            //Get all receipts that are exensions and order them by desc date.
            List<Receipt> extensions = (from r in _pawnLoan.Receipts
                                        orderby r.Date descending 
                                        where
                                            r.Event.ToString() ==
                                            ReceiptEventTypes.Extend.ToString()                                        
                                        select r).ToList<Receipt>();
            if(extensions.Count() > 0)
            {
                //Should be the first extension item since we sorted.
                this.PH_TerminalIDShopText.Text = extensions[0].StoreNumber;
                this.PH_ExtensionDTText.Text = Utilities.GetStringValue(extensions[0].AuxillaryDate);
                this.PH_ExtAmtPaidToDTText.Text = String.Format("{0:C}", (from e in extensions
                                                   select e.Amount).Sum());
            }


        }
    }
}
