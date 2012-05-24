using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Forms.Retail
{
    public partial class RetailItemPricingHistory : CustomBaseForm
    {
        public RetailItemPricingHistory(DesktopSession desktopSession, RetailItem item, QuickCheck quickCheck)
        {
            DesktopSession = desktopSession;
            InitializeComponent();
            RetailItem = item;
            QuickInformation = quickCheck;
        }

        private void LoadRaiseHistory()
        {
            string errorCode, errorText;
            List<RetailPriceChangeHistoryRecord> changeHistory = new List<RetailPriceChangeHistoryRecord>();
            if (!MerchandiseProcedures.GetRetailPriceChangeHistory(DesktopSession, new Icn(RetailItem.Icn), changeHistory, out errorCode, out errorText))
            {
                MessageBox.Show(errorText);
                return;
            }

            gvPriceChanges.AutoGenerateColumns = false;

            foreach (RetailPriceChangeHistoryRecord record in changeHistory)
            {
                DataGridViewRow row = gvPriceChanges.Rows.AddNew();
                row.Cells[colPriceChangeDate.Index].Value = record.ChangeDate.ToString("d");
                row.Cells[colReductionPercent.Index].Value = record.ChangePercentage.ToString("f0") + "%";
                row.Cells[colRetailBefore.Index].Value = record.PriceBefore.ToString("c");
                row.Cells[colRetailAfter.Index].Value = record.PriceAfter.ToString("c");
                row.Cells[colUserId.Index].Value = record.ChangedBy;
                row.Tag = record;
            }
        }

        public RetailItem RetailItem { get; private set; }
        public QuickCheck QuickInformation { get; private set; }

        public DesktopSession DesktopSession { get; private set; }

        private void RetailItemPricingHistory_Load(object sender, EventArgs e)
        {
            Icn icn = new Icn(RetailItem.Icn);
            lblCreationDateValue.Text = RetailItem.Md_Date.ToString("d");
            txtDescription.Text = RetailItem.TicketDescription;
            lblGunNumberValue.Text = RetailItem.GunNumber.ToString();
            lblIcnValue.Text = RetailItem.Icn;
            lblManufacturerValue.Text = GetStringValue(QuickInformation.Manufacturer);
            lblModelValue.Text = GetStringValue(QuickInformation.Model);
            lblNewRetailValue.Text = RetailItem.RetailPrice.ToString("c");
            lblPfiDateValue.Text = RetailItem.PfiDate.ToString("d");
            lblQuantityValue.Text = RetailItem.Quantity.ToString();
            lblSerialValue.Text = (RetailItem.SerialNumber == null || RetailItem.SerialNumber.Count == 0) ? string.Empty : RetailItem.SerialNumber[0];
            lblShortCodeValue.Text = icn.GetShortCode();
            lblStatusValue.Text = RetailItem.ItemStatus.ToString();
            lblWeightValue.Text = QuickInformation.Weight.ToString();

            LoadRaiseHistory();
        }

        private string GetStringValue(string value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
