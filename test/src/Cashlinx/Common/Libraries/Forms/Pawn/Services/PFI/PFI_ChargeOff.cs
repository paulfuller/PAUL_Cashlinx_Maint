/****************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products.ProductDetails
 * Class:           ChargeOff
 * 
 * Description      Popup Form to select Charge Off Reason
 * 
 * History
 * David D Wise,    Initial Development
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Forms.Pawn.Services.PFI
{
    public partial class PFI_ChargeOff : CustomBaseForm
    {
        public delegate void ChargeOffHandler(ItemReason pisChargeCode);
        public event ChargeOffHandler ChargeOff;

        public PFI_ChargeOff(DesktopSession desktopSession)
        {
            DesktopSession = desktopSession;
            InitializeComponent();
            Setup();
        }

        public DesktopSession DesktopSession { get; private set; }

        private void Setup()
        {
            List<ItemReasonCode> ptChargeCodes = ItemReasonFactory.Instance.GetChargeOffCodes(DesktopSession.PawnSecApplication);
            //List<ItemReasonCode> ptChargeCodes = DesktopSession.LoanPawnItemReason.FindAll(delegate(PairType<ItemReason, string> p)
            //{
            //    return p.Right.StartsWith("Charge Off -");
            //});

            // Populate Combo Box Charge Off Codes from Enum
            chargeCodeComboBox.DataSource = ptChargeCodes;
            chargeCodeComboBox.DisplayMember = "Description";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chargeOffButton_Click(object sender, EventArgs e)
        {
            ItemReasonCode reasonCode = chargeCodeComboBox.SelectedItem as ItemReasonCode;

            if (reasonCode == null)
            {
                return;
            }

            ChargeOff(reasonCode.Reason);
            this.Close();
        }
    }
}
