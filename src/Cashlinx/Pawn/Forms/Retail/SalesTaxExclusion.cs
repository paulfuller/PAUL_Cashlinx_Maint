using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Retail;

namespace Pawn.Forms.Retail
{
    public partial class SalesTaxExclusion : Form
    {
        #region Private Properties

        private DesktopSession CDS
        {
            get { return GlobalDataAccessor.Instance.DesktopSession; }
        }

        private SalesTaxInfo TaxInfo { get; set; }

        #endregion

        public SalesTaxExclusion(SalesTaxInfo taxInfo)
        {
            InitializeComponent();
            TaxInfo = taxInfo;

            if (taxInfo == null)
            {
                return;
            }

            txtComments.Text = taxInfo.Comments;
            chkCity.Checked = taxInfo.ExcludeCity;
            chkState.Checked = taxInfo.ExcludeState;
            chkCounty.Checked = taxInfo.ExcludeCounty;

            //Madhu BZ # 368
            if (chkCity.Checked && chkState.Checked && chkCounty.Checked)
                chkSelectAll.Checked = true;

            lblCityRate.Text = taxInfo.CityStoreTax.TaxRate.ToString("f2");
            lblStateRate.Text = taxInfo.StateStoreTax.TaxRate.ToString("f2");
            lblCountyRate.Text = taxInfo.CountyStoreTax.TaxRate.ToString("f2");
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            TaxInfo.Comments = txtComments.Text;
            TaxInfo.ExcludeCity = chkCity.Checked;
            TaxInfo.ExcludeCounty = chkCounty.Checked;
            TaxInfo.ExcludeState = chkState.Checked;

            this.Close();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            chkCity.Checked = chkState.Checked = chkCounty.Checked = chkSelectAll.Checked;
        }
    }
}
