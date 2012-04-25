using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;
using Pawn.Logic.DesktopProcedures;
using Pawn.Logic.PrintQueue;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    public partial class ShopTransferOut : Form
    {

        private string errorCode;
        private string errorText;
        private const string DENOMINATIONCURRENCY = "USD";
        private bool destinationStoreDataFound = false;
        private string destShopName;
        private readonly string VOID = "Void";
        private decimal transferAmount;
        
        public ShopTransferOut()
        {
            InitializeComponent();
        }

        private void customButtonFind_Click(object sender, EventArgs e)
        {
            if (customTextBoxDestShopNo.isValid)
            {
                //If the shop number entered is valid, call data layer
                //to get the store information
                SiteId storeInfo = new SiteId();

                ShopProcedures.ExecuteGetStoreInfo(GlobalDataAccessor.Instance.OracleDA,
                    customTextBoxDestShopNo.Text.ToString(), ref storeInfo, out errorCode, out errorText);
                if (storeInfo != null)
                {
                    destinationStoreDataFound = true;
                    destShopName = storeInfo.StoreName;
                    labelDestAddr1.Text = storeInfo.StoreAddress1;
                    labelDestAddr2.Text = storeInfo.StoreCityName + "," + storeInfo.State + " " + storeInfo.StoreZipCode;
                    labelDestManager.Text = storeInfo.StoreManager;
                    labelDestPhone.Text = storeInfo.StorePhoneNo;
                    customButtonSubmit.Enabled = !string.IsNullOrEmpty(storeInfo.StoreName);
                }
            }
            else
            {
                MessageBox.Show(@"Please enter a valid destination shop number");
            }
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShopTransferOut_Load(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.CashTransferData != null)
            {
                CashTransferVO cashTransferData = GlobalDataAccessor.Instance.DesktopSession.CashTransferData;
                labelSourceShopNo.Text = Utilities.GetStringValue(cashTransferData.SourceShopInfo.StoreNumber);
                labelSourceAddr1.Text = Utilities.GetStringValue(cashTransferData.SourceShopInfo.StoreAddress1);
                labelSourceAddr2.Text = Utilities.GetStringValue(cashTransferData.SourceShopInfo.StoreCityName) +
                    "," + Utilities.GetStringValue(cashTransferData.SourceShopInfo.State) +
                    " " + Utilities.GetStringValue(cashTransferData.SourceShopInfo.StoreZipCode);
                labelSourceManager.Text = Utilities.GetStringValue(cashTransferData.SourceShopInfo.StoreManager);
                labelSourcePhone.Text = Utilities.GetStringValue(cashTransferData.SourceShopInfo.StorePhoneNo);
                customTextBoxTransporter.Text = Utilities.GetStringValue(cashTransferData.Transporter);
                customTextBoxBagNo.Text = Utilities.GetStringValue(cashTransferData.DepositBagNo);
                richTextBoxComment.Text = Utilities.GetStringValue(cashTransferData.SourceComment);
                customTextBoxDestShopNo.Text = Utilities.GetStringValue(cashTransferData.DestinationShopInfo.StoreNumber);
                labelDestAddr1.Text = Utilities.GetStringValue(cashTransferData.DestinationShopInfo.StoreAddress1);
                labelDestAddr2.Text = Utilities.GetStringValue(cashTransferData.DestinationShopInfo.StoreCityName) + 
                    "," + Utilities.GetStringValue(cashTransferData.DestinationShopInfo.State) +
                    " " + Utilities.GetStringValue(cashTransferData.DestinationShopInfo.StoreZipCode);
                labelDestManager.Text = Utilities.GetStringValue(cashTransferData.DestinationShopInfo.StoreManager);
                transferAmount = Utilities.GetDecimalValue(cashTransferData.TransferAmount);
                customTextBoxTrAmount.Text = string.Format("{0:C}", transferAmount);
                labelDestPhone.Text = Utilities.GetStringValue(cashTransferData.DestinationShopInfo.StorePhoneNo);
                labelTransferDate.Text = Utilities.GetDateTimeValue(cashTransferData.TransferDate).ToShortDateString();
                labelTransferTime.Visible = false;
                labelTransferTimeHeading.Visible = false;
                labelTransferStatus.Text = Utilities.GetStringValue(cashTransferData.TransferStatus);
                labelCashDrawerName.Text = GlobalDataAccessor.Instance.DesktopSession.StoreSafeName;
                //disable buttons and make controls readonly
                customButtonFind.Visible = false;
                pictureBox1.Visible = false;
                customTextBoxDestShopNo.Enabled = false;
                customTextBoxTrAmount.Enabled = false;
                customTextBoxTransporter.Enabled = false;
                customTextBoxBagNo.Enabled = false;
                richTextBoxComment.Enabled = false;
                customLabelComment.Visible = true;
                customTextBoxComment.Visible = true;
                customButtonSubmit.Text = VOID;
                customButtonSubmit.Enabled = true;


            }
            else
            {

                SiteId currentSite = GlobalDataAccessor.Instance.CurrentSiteId;
                labelSourceAddr1.Text = currentSite.StoreAddress1;
                labelSourceAddr2.Text = currentSite.StoreCityName + "," + currentSite.State + " " + currentSite.StoreZipCode;
                labelSourcePhone.Text = currentSite.StorePhoneNo;
                labelSourceShopNo.Text = currentSite.StoreNumber;
                labelSourceManager.Text = currentSite.StoreManager;
                labelCashDrawerName.Text = GlobalDataAccessor.Instance.DesktopSession.StoreSafeName;
                labelTransferDate.Text = ShopDateTime.Instance.ShopDate.ToShortDateString();
                labelTransferTime.Text = string.Format("{0:t}", ShopDateTime.Instance.ShopDateCurTime);

                currencyEntry1.Calculate += currencyEntry1_Calculate;
                currencyEntry1.OtherTenderClick += currencyEntry1_OtherTenderClick;
                customButtonSubmit.Enabled = false;
            }
            //Do not show the currency panel when it first loads
            panelCurrency.Visible = false;
            pictureBox1.Image = Common.Properties.Resources.plus_icon_small;
            panel2.Location = new Point(3, 106);
            this.Size = new Size(777, 492);
            
        }

        void currencyEntry1_OtherTenderClick()
        {
            panel2.Visible = !panel2.Visible;
        }

        void currencyEntry1_Calculate(decimal currencyTotal)
        {
            customTextBoxTrAmount.Text = string.Format("{0:C}",currencyTotal);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panelCurrency.Visible)
            {
                panelCurrency.Visible = false;
                pictureBox1.Image = Common.Properties.Resources.plus_icon_small;
                panel2.Location = new Point(3, 106);
                this.Size = new Size(777, 492);

            }
            else
            {
                panelCurrency.Visible = true;
                pictureBox1.Image = Common.Properties.Resources.minus_icon_small;
                panel2.Location = new Point(6, 417);
                this.Size = new Size(777, 724);
            }
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            if (customButtonSubmit.Text == VOID)
            {
                GlobalDataAccessor.Instance.beginTransactionBlock();
                bool retValue = VoidProcedures.VoidStoreCashTransfer(GlobalDataAccessor.Instance.OracleDA,
                    GlobalDataAccessor.Instance.DesktopSession.CashTransferData.TransferId,
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    "",
                    customTextBoxComment.Text,
                    GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID,
                    ShopDateTime.Instance.ShopDate.FormatDate() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                    transferAmount.ToString(),
                    GlobalDataAccessor.Instance.DesktopSession.StoreSafeID,
                    out errorCode,
                    out errorText);
                if (!retValue)
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Void store to store cash Transfer failed " + errorText);
                }
                else
                {

                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    MessageBox.Show(@"Void store to store cash Transfer completed successfully");
                }

            }
            else
            {
                List<string> denominationData = new List<string>();
                if (panelCurrency.Visible)
                {
                    denominationData = currencyEntry1.CurrencyEntryData();
                }
                else
                {
                    denominationData.Add("");
                }
                if (string.IsNullOrEmpty(customTextBoxDestShopNo.Text) || string.IsNullOrEmpty(customTextBoxTrAmount.Text) ||
                    string.IsNullOrEmpty(customTextBoxTransporter.Text) && destinationStoreDataFound)
                {
                    MessageBox.Show(@"Fill in the required information and submit");
                    return;
                }
                if (customTextBoxDestShopNo.Text == labelSourceShopNo.Text)
                {
                    MessageBox.Show(@"Source and destination stores cannot be the same");
                    return;
                }
                GlobalDataAccessor.Instance.beginTransactionBlock();
                string transferAmount = customTextBoxTrAmount.Text;
                if (transferAmount.StartsWith("$"))
                    transferAmount = transferAmount.Substring(1);
                int transferNumber = 0;
                bool retVal = ShopCashProcedures.InsertShopTransfer(
                    CashTransferTypes.SHOPTOSHOP.ToString(),
                    labelSourceShopNo.Text,
                    customTextBoxDestShopNo.Text,
                    Utilities.GetDecimalValue(transferAmount),
                    customTextBoxTransporter.Text,
                    customTextBoxBagNo.Text,
                    richTextBoxComment.Text,
                    GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID,
                    ShopDateTime.Instance.ShopDate.FormatDate() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                    GlobalDataAccessor.Instance.DesktopSession.StoreSafeID,
                    DENOMINATIONCURRENCY,
                    denominationData,
                    GlobalDataAccessor.Instance.DesktopSession,
                    out transferNumber,
                    out errorCode,
                    out errorText);
                if (retVal)
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    MessageBox.Show(@"Shop cash transfer successfully entered");
                    //Load the data in cashtransfervo object for printing
                    CashTransferVO shoptransfervo = new CashTransferVO();
                    shoptransfervo.TransferNumber = Utilities.GetIntegerValue(transferNumber, 0);
                    shoptransfervo.TransferAmount = Utilities.GetDecimalValue(transferAmount);
                    shoptransfervo.TransferStatus = CashTransferStatusCodes.PENDING.ToString();
                    shoptransfervo.Transporter = customTextBoxTransporter.Text;
                    shoptransfervo.SourceComment = richTextBoxComment.Text;
                    shoptransfervo.SourceEmployee = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName + "," +
                        GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.EmployeeNumber;
                    shoptransfervo.SourceShopInfo = GlobalDataAccessor.Instance.CurrentSiteId;
                    SiteId destSite = new SiteId();
                    destSite.StoreNumber = customTextBoxDestShopNo.Text;
                    destSite.StoreName = destShopName;
                    destSite.StorePhoneNo = labelDestPhone.Text;
                    destSite.StoreAddress1 = labelDestAddr1.Text;
                    destSite.StoreAddress2 = labelDestAddr2.Text;
                    shoptransfervo.DestinationShopInfo = destSite;
                    ShopToShopCashTransfer shopTransferFrm = new ShopToShopCashTransfer();
                    shopTransferFrm.ShopTransferData = shoptransfervo;
                    shopTransferFrm.ShowDialog();

                }
                else
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Shop transfer data not saved " + errorText);
                }
            }
            this.Close();

        }

        private void customTextBoxTrAmount_Leave(object sender, EventArgs e)
        {
            if (customTextBoxTrAmount.Text.StartsWith("$"))
                customTextBoxTrAmount.Text = customTextBoxTrAmount.Text.Substring(1);


            customTextBoxTrAmount.Text = string.Format("{0:C}", Utilities.GetDecimalValue(customTextBoxTrAmount.Text,0));
        }


  


  
    }
}
