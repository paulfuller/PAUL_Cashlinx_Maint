using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Pawn.Logic.PrintQueue
{
    public partial class ShopToShopCashTransfer : Form
    {
        public CashTransferVO ShopTransferData;
        private Bitmap bitMap;
        public ShopToShopCashTransfer()
        {
            InitializeComponent();
        }

        private void ShopToShopCashTransfer_Load(object sender, EventArgs e)
        {
            if (ShopTransferData != null)
            {
                cashTransferHeader1.SetHeaderData("SHOP TO SHOP TRANSFER",
                    ShopTransferData.SourceShopInfo.StoreName,
                    ShopDateTime.Instance.ShopDate.ToShortDateString(),
                    String.Format("{0:t}", ShopDateTime.Instance.ShopDateCurTime),
                    ShopTransferData.TransferNumber.ToString(),
                    ShopTransferData.TransferStatus);

                labelSourceShopNo.Text = ShopTransferData.SourceShopInfo.StoreNumber;
                labelSourceShopName.Text = ShopTransferData.SourceShopInfo.StoreName;
                labelSourceShopAddress.Text = ShopTransferData.SourceShopInfo.StoreAddress1;
                if (string.IsNullOrEmpty(ShopTransferData.SourceShopInfo.StoreAddress2))
                {
                    labelSourceShopAddr2.Text = ShopTransferData.SourceShopInfo.StoreCityName + "," +
                        ShopTransferData.SourceShopInfo.State + " " +
                        ShopTransferData.SourceShopInfo.StoreZipCode;
                }
                else
                {
                    labelSourceShopAddr2.Text = ShopTransferData.SourceShopInfo.StoreAddress2;
                }
                labelSourcePhone.Text = Utilities.GetPhoneNumber(ShopTransferData.SourceShopInfo.StorePhoneNo);
                labelempname.Text = ShopTransferData.SourceEmployee;
                labelsourcecomment.Text = ShopTransferData.SourceComment;
                labelTransferAmount.Text = String.Format("{0:C}",ShopTransferData.TransferAmount);
                labeldestshopname.Text = ShopTransferData.DestinationShopInfo.StoreName;
                labeldestshopno.Text = ShopTransferData.DestinationShopInfo.StoreNumber;
                labeldestshopaddr.Text = ShopTransferData.DestinationShopInfo.StoreAddress1;
                labeldestshopaddr2.Text = ShopTransferData.DestinationShopInfo.StoreAddress2;
                labeldestphone.Text = Utilities.GetPhoneNumber(ShopTransferData.DestinationShopInfo.StorePhoneNo);
                if (string.IsNullOrEmpty(ShopTransferData.DestinationEmployee))
                {
                    labeldestempnameheading.Visible = false;
                    labeldestcommentheading.Visible = false;
                    labeldestComment.Visible = false;
                    labelDestempName.Visible = false;
                }
                else
                {
                    labeldestcommentheading.Visible = true;
                    labeldestempnameheading.Visible = true;
                    labeldestComment.Visible = true;
                    labelDestempName.Visible = true;
                    labelDestempName.Text = ShopTransferData.DestinationEmployee;
                    labeldestComment.Text = ShopTransferData.DestinationComment;
                }
                Print(2);
            }
            this.Close();
        }
        /**
         * Modified print method to include parameter for printing document
         * more than once. John K Def Fix 0017
         * */
        private void Print(int noOfTimes)
        {
            bitMap = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            this.DrawToBitmap(bitMap, new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            for (int i = 0; i < noOfTimes; i++)
            {
                PrintingUtilities.PrintBitmapDocument(bitMap, GlobalDataAccessor.Instance.DesktopSession);
            }
        }


    }
}
