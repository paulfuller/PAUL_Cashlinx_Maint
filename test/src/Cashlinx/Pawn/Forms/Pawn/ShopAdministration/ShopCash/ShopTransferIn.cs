using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic.DesktopProcedures;
using Pawn.Logic.PrintQueue;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    public partial class ShopTransferIn : Form
    {
        private int selectedTransferNumber;
        private string selectedTransferId;
        private const string DENOMINATIONCURRENCY = "USD";
        private readonly string VOID = "Void";
        private decimal transferAmount;
        private string sourceShopName;
        private string destinationShopName;
        private string sourceEmployee;
        private bool processRejected;
        public bool Back;

        public ShopTransferIn()
        {
            InitializeComponent();
        }

        private void ShopTransferIn_Load(object sender, EventArgs e)
        {
            selectedTransferNumber = GlobalDataAccessor.Instance.DesktopSession.SelectedStoreCashTransferNumber;
            selectedTransferId = GlobalDataAccessor.Instance.DesktopSession.SelectedStoreCashTransferID;
            labelTransferNumber.Text = selectedTransferNumber.ToString();
            string errorCode;
            string errorText;
            DataTable transferDetails;
            DataTable denominationDetails;
            Back = false;
            if (GlobalDataAccessor.Instance.DesktopSession.CashTransferData != null)
            {
                CashTransferVO cashTransferData = GlobalDataAccessor.Instance.DesktopSession.CashTransferData;
                labelSourceShopNo.Text = Utilities.GetStringValue(cashTransferData.SourceShopInfo.StoreNumber);
                labelSourceShopAddr1.Text = Utilities.GetStringValue(cashTransferData.SourceShopInfo.StoreAddress1);
                labelSourceShopAddr2.Text = Utilities.GetStringValue(cashTransferData.SourceShopInfo.StoreCityName) +
                    "," + Utilities.GetStringValue(cashTransferData.SourceShopInfo.State) +
                    " " + Utilities.GetStringValue(cashTransferData.SourceShopInfo.StoreZipCode);
                labelManagerName.Text = Utilities.GetStringValue(cashTransferData.SourceShopInfo.StoreManager);
                labelSourceShopPhone.Text = Utilities.GetStringValue(cashTransferData.SourceShopInfo.StorePhoneNo);
                labelTransporterName.Text = Utilities.GetStringValue(cashTransferData.Transporter);
                labelBagNo.Text = Utilities.GetStringValue(cashTransferData.DepositBagNo);
                richTextBoxDestComment.Text = Utilities.GetStringValue(cashTransferData.SourceComment);
                labelDestShop.Text = Utilities.GetStringValue(cashTransferData.DestinationShopInfo.StoreNumber);
                labelDestAddr1.Text = Utilities.GetStringValue(cashTransferData.DestinationShopInfo.StoreAddress1);
                labelDestAddr2.Text = Utilities.GetStringValue(cashTransferData.DestinationShopInfo.StoreCityName) +
                    "," + Utilities.GetStringValue(cashTransferData.DestinationShopInfo.State) +
                    " " + Utilities.GetStringValue(cashTransferData.DestinationShopInfo.StoreZipCode);
                labelDestMgrName.Text = Utilities.GetStringValue(cashTransferData.DestinationShopInfo.StoreManager);
                transferAmount = Utilities.GetDecimalValue(cashTransferData.TransferAmount);
                labelTrAmount.Text = string.Format("{0:C}", transferAmount);
                labelDestPhone.Text = Utilities.GetStringValue(cashTransferData.DestinationShopInfo.StorePhoneNo);
                labelTransferDate.Text = Utilities.GetDateTimeValue(cashTransferData.TransferDate).ToShortDateString();
                labelStatus.Text = Utilities.GetStringValue(cashTransferData.TransferStatus);
                labelTransferNumber.Text = Utilities.GetStringValue(cashTransferData.TransferNumber);
                //disable buttons and make controls readonly
                pictureBox1.Visible = false;
                richTextBoxDestComment.Enabled = false;
                customButtonAccept.Text = VOID;
                customButtonBack.Visible = false;
                customButtonReject.Visible = false;
                customLabelComment.Visible = true;
                customTextBoxComment.Visible = true;



            }
            else
            {

                try
                {
                    //Get the transfer details
                    string storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                    bool retval = ShopCashProcedures.GetShopTransferDetails(selectedTransferNumber.ToString(), selectedTransferId,
                        storeNumber, GlobalDataAccessor.Instance.DesktopSession, out transferDetails, out denominationDetails, out errorCode, out errorText);
                    //Parse the transferdetails datatable to get all the information to show in the screen
                    //transferdetails should have only row for the selected transfer number
                    if (transferDetails != null && transferDetails.Rows.Count > 0)
                    {
                        labelSourceShop.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STORENUMBER]);
                        labelSourceShopAddr1.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STOREADDRESS1]);
                        labelSourceShopAddr2.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STORECITYNAME]) +
                            "," + Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STORESTATE]) +
                            " " + Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STOREZIPCODE]);
                        labelManagerName.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STOREMANAGER]);
                        labelSourceShopPhone.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STOREPHONENO]);
                        labelTransporterName.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.TRANSPORTEDBY]);
                        labelBagNo.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.DEPOSITBAGNUMBER]);
                        labelSourceComment.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.SOURCECOMMENT]);
                        labelDestShop.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STORENUMBER]);
                        labelDestAddr1.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STOREADDRESS]);
                        labelDestAddr2.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STORECITYNAME]) +
                            "," + Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STORESTATE]) +
                            " " + Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STOREZIPCODE]);
                        labelDestMgrName.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STOREMANAGER]);
                        transferAmount = Utilities.GetDecimalValue(transferDetails.Rows[0][(int)TransferDetailsRecord.TRANSFERAMOUNT]);
                        labelTrAmount.Text = string.Format("{0:C}", transferAmount);
                        labelDestPhone.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STOREPHONENO]);
                        labelTransferDate.Text = Utilities.GetDateTimeValue(transferDetails.Rows[0][(int)TransferDetailsRecord.TRANSFERDATE]).ToShortDateString();
                        labelStatus.Text = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.TRANSFERSTATUS]);
                        sourceShopName = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STORENAME]);
                        destinationShopName = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STORENAME]);
                        sourceEmployee = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.USERNAME])
                                        + "," +
                                        Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.USEREMPLOYEENUMBER]);
                        if (labelStatus.Text == CashTransferStatusCodes.REJECTED.ToString())
                        {
                            customButtonAccept.Enabled = string.IsNullOrEmpty(labelTransporterName.Text);
                            processRejected = string.IsNullOrEmpty(labelTransporterName.Text);
                            customButtonReject.Enabled = false;
                            richTextBoxDestComment.Enabled = false;
                        }

                    }
                    //DenominationDetails datatable will have data for the currency user control
                    if (denominationDetails != null && denominationDetails.Rows.Count > 0)
                    {
                        List<String> currencyData = new List<string>();
                        foreach (DataRow dr in denominationDetails.Rows)
                        {
                            string displayName = Utilities.GetStringValue(dr["displayname"], "");
                            string currencyDenom = Utilities.GetDecimalValue(dr["denomination_amount"]).ToString();
                            //The following is necessary since in the DB both coin and dollar 1 is 
                            //the same as far as amount is concerned but is different only in display name
                            if (displayName == "USD COIN 1")
                                currencyDenom = "1.00";
                            currencyData.Add(currencyDenom);
                            currencyData.Add(Utilities.GetStringValue(dr["amount"]));


                        }

                        var errorMesg = string.Empty;
                        bool retVal = currencyEntry1.SetCurrencyData(currencyData, true,out errorMesg);
                        currencyEntry1.Enabled = false;
                        currencyEntry1.Calculate += (currencyEntry1_Calculate);
                        currencyEntry1.OtherTenderClick += (currencyEntry1_OtherTenderClick);
                        
                        if (!retval)
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot load currency data for shop transfer " + selectedTransferNumber + errorMesg);
                    }
                }
                catch (Exception ex)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot load shop transfer details " + ex.Message);
                }
            }
            
            //Do not show the currency panel when it first loads
            panelCurrency.Visible = false;
            pictureBox1.Image = Common.Properties.Resources.plus_icon_small;
            panel1.Location = new Point(6, 111);
            this.Size = new Size(773, 454);

        }

        void currencyEntry1_OtherTenderClick()
        {
            //Do nothing
        }

        void currencyEntry1_Calculate(decimal currencyTotal)
        {
            //Do nothing
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panelCurrency.Visible)
            {
                panelCurrency.Visible = false;
                pictureBox1.Image = Common.Properties.Resources.plus_icon_small;
                panel1.Location = new Point(6, 111);
                this.Size = new Size(773, 454);

            }
            else
            {
                panelCurrency.Visible = true;
                currencyEntry1.Enabled = false;
                pictureBox1.Image = Common.Properties.Resources.minus_icon_small;
                panel1.Location = new Point(6, 417);
                this.Size = new Size(773, 775);
            }
        }

        private void customButtonBack_Click(object sender, EventArgs e)
        {
            Back = true;
            GlobalDataAccessor.Instance.DesktopSession.SelectedStoreCashTransferNumber = 0;
            this.Close();
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customButtonAccept_Click(object sender, EventArgs e)
        {
            string errorCode;
            string errorText;
            if (customButtonAccept.Text == VOID)
            {
                GlobalDataAccessor.Instance.beginTransactionBlock();
                bool retValue = VoidProcedures.VoidStoreCashTransfer(GlobalDataAccessor.Instance.OracleDA,
                        GlobalDataAccessor.Instance.DesktopSession.CashTransferData.TransferId,
                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        "",
                        customTextBoxComment.Text,
                        GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID,
                        ShopDateTime.Instance.ShopDate.FormatDate() + " " + ShopDateTime.Instance.ShopTime,
                        transferAmount.ToString(),
                        GlobalDataAccessor.Instance.DesktopSession.StoreSafeID,
                        out errorCode,
                        out errorText);
                    if (!retValue)
                    {
                        GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Void store to store cash Transfer failed " + errorText);
                    }
                    else
                        GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);

            }
            else
            {

                GlobalDataAccessor.Instance.beginTransactionBlock();
                bool retVal = ShopCashProcedures.UpdateShopCashTransfer(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    selectedTransferNumber.ToString(), selectedTransferId, processRejected ? CashTransferStatusCodes.RACCEPTED.ToString() : CashTransferStatusCodes.ACCEPTED.ToString(),
                    richTextBoxDestComment.Text, "",
                    GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID,
                    ShopDateTime.Instance.ShopDate.FormatDate().ToString() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                    DENOMINATIONCURRENCY, GlobalDataAccessor.Instance.DesktopSession, out errorCode, out errorText);
                if (retVal)
                {
                    MessageBox.Show(!processRejected
                                            ? @"Shop transfer accepted"
                                            : @"Shop transfer reject accepted");
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    if (!processRejected)
                        printTransferData("ACCEPTED");
                }
                else
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, errorText);
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                }
            }
            this.Close();


        }

        private void printTransferData(string transferStatus)
        {
            //Load the data in cashtransfervo object for printing
            CashTransferVO shoptransfervo = new CashTransferVO();
            shoptransfervo.TransferNumber = selectedTransferNumber;
            shoptransfervo.TransferAmount = Utilities.GetDecimalValue(transferAmount);
            shoptransfervo.TransferStatus = transferStatus;
            shoptransfervo.SourceComment = labelSourceComment.Text;
            shoptransfervo.DestinationComment = richTextBoxDestComment.Text;
            shoptransfervo.DestinationEmployee = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName + "," +
                GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.EmployeeNumber;
            shoptransfervo.SourceEmployee = sourceEmployee;
            
            SiteId sourceSite = new SiteId();
            sourceSite.StoreNumber = labelSourceShop.Text;
            sourceSite.StoreName = sourceShopName;
            sourceSite.StorePhoneNo = labelSourceShopPhone.Text;
            sourceSite.StoreAddress1 = labelSourceShopAddr1.Text;
            sourceSite.StoreAddress2 = labelSourceShopAddr2.Text;
            shoptransfervo.SourceShopInfo = sourceSite;


            SiteId destSite = new SiteId();
            destSite.StoreNumber = labelDestShop.Text;
            destSite.StoreName = destinationShopName;
            destSite.StorePhoneNo = labelDestPhone.Text;
            destSite.StoreAddress1 = labelDestAddr1.Text;
            destSite.StoreAddress2 = labelDestAddr2.Text;
            shoptransfervo.DestinationShopInfo = destSite;


            ShopToShopCashTransfer shopTransferFrm = new ShopToShopCashTransfer();
            shopTransferFrm.ShopTransferData = shoptransfervo;
            shopTransferFrm.ShowDialog();

        }

        private void customButtonReject_Click(object sender, EventArgs e)
        {
            //Show the reject reason form
            TransferReject rejectForm = new TransferReject();
            rejectForm.ShowDialog();
            string rejectReason = rejectForm.RejectReason;
            string errorCode;
            string errorText;
            GlobalDataAccessor.Instance.beginTransactionBlock();
            bool retVal = ShopCashProcedures.UpdateShopCashTransfer(
                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                selectedTransferNumber.ToString(), selectedTransferId, 
                CashTransferStatusCodes.REJECTED.ToString(),
                richTextBoxDestComment.Text, 
                rejectReason,
                GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID,
                ShopDateTime.Instance.ShopDate.FormatDate().ToString() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                DENOMINATIONCURRENCY, GlobalDataAccessor.Instance.DesktopSession,
                out errorCode, 
                out errorText);
            if (retVal)
            {
                MessageBox.Show(@"Shop transfer rejected");
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                printTransferData("REJECTED");
            }
            else
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, errorText);
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
            }
            this.Close();

        }

 
    }
}
