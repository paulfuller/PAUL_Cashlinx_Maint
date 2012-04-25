using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using Common.Libraries.Objects.Pawn;

namespace Pawn.Forms.UserControls.LoanDetailPanels
{
    /// <summary>
    /// Used for small generic panels in the product history dialog.
    /// </summary>
    public partial class LoanGenericPanel : ProductHistoryPanels.LoanDetailBasePanel
    {
        private PanelType _panelType;

        /// <summary>
        /// Builds a small generic panel based on the panel type specified.
        /// </summary>
        /// <param name="pawnLoan"></param>
        /// <param name="receiptIdx"></param>
        /// <param name="panelType"></param>
        public LoanGenericPanel(PawnLoan pawnLoan, int receiptIdx, PanelType panelType)
        : base(pawnLoan, receiptIdx)
        {
            InitializeComponent();
            _panelType = panelType;
            Setup();
        }

        private void Setup()
        {
            switch (_panelType)
            {
                case PanelType.PFICol1:                   
                    this.PH_Generic1Label.Text = "PFI Eligibility Date:";
                    this.PH_Generic1Text.Text = _pawnLoan.PfiEligible.ToShortDateString();

                    this.PH_Generic2Text.Visible = true;
                    this.PH_Generic2Label.Visible = true;
                    this.PH_Generic2Label.Text = "PFI Amount:";
                    this.PH_Generic2Text.Text = String.Format("{0:C}", _pawnLoan.Amount); 
                    
                    break;
                case PanelType.PFICol2:
                    this.PH_Generic1Label.Text = "PFI Notification Date:";
                    this.PH_Generic1Text.Text = _pawnLoan.PfiNote.ToShortDateString(); 
                    break;
                case PanelType.PFICol3:
                    this.PH_Generic1Label.Text = "PFI Date:";                    
                    this.PH_Generic1Text.Text = _pawnLoan.StatusDate.ToShortDateString();
                    this.PH_Generic2Text.Visible = true;
                    this.PH_Generic2Label.Visible = true;
                    this.PH_Generic2Label.Text = "Employee:";
                    this.PH_Generic2Text.Text = _pawnLoan.LastUpdatedBy; 

                    break;
                default:
                    throw new NotImplementedException("PanelType not yet defined.");
            }
        }

        public enum PanelType
        {
            PFICol1,
            PFICol2,
            PFICol3
        }
    }
}
