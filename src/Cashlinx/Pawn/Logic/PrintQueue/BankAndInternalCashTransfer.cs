using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;

namespace Pawn.Logic.PrintQueue
{
    public partial class BankAndInternalCashTransfer : Form
    {
        public CashTransferVO CashTransferdata;
        public bool TransferToBank;
        public bool InternalTransfer;
        private Bitmap bitMap;
        public BankAndInternalCashTransfer()
        {
            InitializeComponent();
        }

        private void BankAndInternalCashTransfer_Load(object sender, EventArgs e)
        {
            if (CashTransferdata != null)
            {
                if (InternalTransfer)
                {
                    cashTransferHeader1.SetHeaderData("INTERNAL TRANSFER",
                        "",
                        ShopDateTime.Instance.ShopDate.ToShortDateString(),
                        String.Format("{0:t}", ShopDateTime.Instance.ShopDateCurTime),
                        CashTransferdata.TransferNumber.ToString(),
                        "");
                    labelbankname.Text = "Emp # " + CashTransferdata.SourceEmployeeNumber;
                    labelcchecknoheading.Text = "Emp. Name";
                    labelchecknumber.Text = CashTransferdata.SourceEmployeeName;
                    labelCarrierSignature.Text = "Signature";
                    labelFromDrawerHeading.Visible = true;
                    labelFromDrawerName.Visible = true;
                    labelFromDrawerName.Text = CashTransferdata.SourceDrawerName;
                    labelToDrawerNoHeading.Visible = true;
                    labelToDrawerName.Visible = true;
                    labelToDrawerName.Text = CashTransferdata.DestinationDrawerName;


                }
                else
                {
                    if (!TransferToBank)
                    {
                        cashTransferHeader1.SetHeaderData("TRANSFER FROM BANK",
                            CashTransferdata.SourceShopInfo.StoreName,
                            ShopDateTime.Instance.ShopDate.ToShortDateString(),
                            String.Format("{0:t}", ShopDateTime.Instance.ShopDateCurTime),
                            CashTransferdata.TransferNumber.ToString(),
                            CashTransferdata.TransferStatus.ToString());
                        labelbankname.Text = CashTransferdata.BankName;
                        labelchecknumber.Text = CashTransferdata.CheckNumber;
                    }
                    else
                    {

                        cashTransferHeader1.SetHeaderData("TRANSFER TO BANK",
                            CashTransferdata.SourceShopInfo.StoreName,
                            ShopDateTime.Instance.ShopDate.ToShortDateString(),
                            String.Format("{0:t}", ShopDateTime.Instance.ShopDateCurTime),
                            CashTransferdata.TransferNumber.ToString(),
                            CashTransferdata.TransferStatus.ToString());
                        labelbankname.Text = CashTransferdata.BankName;
                        labelchecknumber.Text = CashTransferdata.DepositBagNo;
                        panelBankData.Location = new Point(17, 326);
                        panelEmpData.Location = new Point(17, 147);
                        labelcchecknoheading.Text = "Bag #";

                    }
                }


                labelempname.Text = CashTransferdata.DestinationEmployeeName;
                labelempnumber.Text = CashTransferdata.DestinationEmployeeNumber;
                labelTransferAmount.Text = string.Format("{0:C}", CashTransferdata.TransferAmount);
                Print();
            }
            this.Close();
        }

        private void Print()
        {


            bitMap = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            this.DrawToBitmap(bitMap, new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            PrintingUtilities.PrintBitmapDocument(bitMap, GlobalDataAccessor.Instance.DesktopSession);
        }

   
    }
}
