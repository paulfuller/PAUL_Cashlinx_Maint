using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using System;
using System.Windows.Forms;

namespace Pawn.Forms.Retail
{
    public partial class InternetSaleInformation : Form
    {
        public InternetSaleInformation()
        {
            InitializeComponent();
        }

        private void InternetSaleInformation_Load(object sender, EventArgs e)
        {
            txtShippingShopNumber.Text = GlobalDataAccessor.Instance.CurrentSiteId.StoreNickName;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
