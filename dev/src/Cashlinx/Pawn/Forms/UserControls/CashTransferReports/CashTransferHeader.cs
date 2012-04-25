using System.Windows.Forms;

namespace Pawn.Forms.UserControls.CashTransferReports
{
    public partial class CashTransferHeader : UserControl
    {

        public CashTransferHeader()
        {
            InitializeComponent();
        }

  


        public void SetHeaderData(
            string Heading,
            string ShopName,
            string CurrDate,
            string CurrTime,
            string TransferNo,
            string Status
            )
        {
            labelDate.Text = CurrDate;
            labelTime.Text = CurrTime;
            labelShopName.Text = ShopName;
            labelHeading.Text = Heading;
            labelTransferNo.Text = TransferNo;
            labelStatus.Text = Status;
            if (string.IsNullOrEmpty(Status))
            {
                labelStatus.Visible = false;
                labelStatusHeading.Visible = false;
            }
            if (string.IsNullOrEmpty(ShopName))
                labelShopName.Visible = false;
        }

    }
}
