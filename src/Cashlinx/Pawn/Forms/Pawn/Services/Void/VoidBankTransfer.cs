using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.Void
{
    public partial class VoidBankTransfer : CustomBaseForm
    {
        private CashTransferVO bankCashTransferData;
        private string selectedTransferId;
        private string selectedTransferType;
        private decimal transferAmount;
        //BZ # 419
        private bool isBankToShop = false;
        private bool isShopToBank = false;

        public VoidBankTransfer(VoidSelector.VoidTransactionType type)
        {
            if (type == VoidSelector.VoidTransactionType.VOIDCASHTRANSSHOPTOBANK)
            {
                isShopToBank = true;                
            }
            else
            {
                isBankToShop = true;
            }

            InitializeComponent();
        }

        private void VoidBankTransfer_Load(object sender, EventArgs e)
        {
            bankCashTransferData = GlobalDataAccessor.Instance.DesktopSession.CashTransferData;
            
            customLabelTransferNumber.Text = "";
            customLabelDate.Text = "";
            customLabelUserId.Text = "";
            customLabelSafe.Text = "";
            customLabelTrAmount.Text = "";
            customLabelBankName.Text = "";
            customLabelAcctNumber.Text = "";
            customLabelCheckNumber.Text = "";
            customButtonVoid.Enabled = false;

            try
            {
                //BZ # 419
                if (bankCashTransferData != null)
                {
                    if (isBankToShop)
                    {
                        this.labelHeading.Text = "Transfer To Shop";

                        this.customLabelSafeHeading.Location = new System.Drawing.Point(327, 181);
                        this.customLabelSafe.Location = new System.Drawing.Point(400, 181);

                        this.customLabelBankNameHeading.Location = new System.Drawing.Point(61, 181);
                        customLabelBankName.Location = new System.Drawing.Point(147, 181);

                        this.customLabelAcctNumHeading.Location = new System.Drawing.Point(61, 202);
                        customLabelAcctNumber.Location = new System.Drawing.Point(147, 202);

                        this.customLabelChkNumHeading.Location = new System.Drawing.Point(61, 223);
                        customLabelCheckNumber.Location = new System.Drawing.Point(147, 223);

                        //customLabelTrAmount.Location = new System.Drawing.Point(147, 244);
                        this.customLabelTrAmtHeading.Location = new System.Drawing.Point(61, 244);
                        this.customLabelTrAmount.Location = new System.Drawing.Point(155, 244);

                        this.customLabelComment.Location = new System.Drawing.Point(61, 282);
                        this.customTextBoxComment.Location = new System.Drawing.Point(141, 282);
                    }
                    else
                    {
                        this.labelHeading.Text = "Transfer To Bank";
                    }
                    selectedTransferId = bankCashTransferData.TransferId;
                    selectedTransferType = bankCashTransferData.TransferType;
                    customLabelTransferNumber.Text = bankCashTransferData.TransferNumber.ToString();
                    var dtTransferDate = Utilities.GetDateTimeValue(bankCashTransferData.TransferDate, DateTime.Now);
                    customLabelDate.Text = string.Format("{0} {1}", dtTransferDate.FormatDate(), dtTransferDate.ToShortTimeString());
                    customLabelUserId.Text = bankCashTransferData.SourceEmployee;
                    customLabelSafe.Text = GlobalDataAccessor.Instance.DesktopSession.StoreSafeName;
                    transferAmount = bankCashTransferData.TransferAmount;
                    customLabelTrAmount.Text = String.Format("{0:C}", transferAmount);
                    customLabelBankName.Text = bankCashTransferData.BankName;
                    customLabelAcctNumber.Text = bankCashTransferData.BankAccountNumber;
                    customLabelCheckNumber.Text = bankCashTransferData.CheckNumber;
                    customButtonVoid.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error when fetching bank transfer details " + ex.Message);
            }
        }

        private void customButtonVoid_Click(object sender, EventArgs e)
        {
            string errorCode;
            string errorText;
            GlobalDataAccessor.Instance.beginTransactionBlock();
            string trAmount = Utilities.GetStringValue(transferAmount);
           // if (trAmount.StartsWith("$"))
           //     trAmount = trAmount.Substring(1);
            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            bool retValue = VoidProcedures.VoidBankTransfer(GlobalDataAccessor.Instance.OracleDA,
                                                            selectedTransferId,
                                                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                            selectedTransferType,
                                                            "",
                                                            customTextBoxComment.Text,
                                                            dSession.LoggedInUserSecurityProfile.UserID,
                                                            ShopDateTime.Instance.ShopDate.FormatDate() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                                                            trAmount,
                                                            dSession.StoreSafeID,
                                                            out errorCode,
                                                            out errorText);
            if (!retValue)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Void Bank Transfer failed " + errorText);
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
            }
            else
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
            MessageBox.Show("Void Bank Transfer completed successfully");
            this.Close();
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
