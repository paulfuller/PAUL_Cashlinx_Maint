using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.Void
{
    public partial class VoidTransactionForm : CustomBaseForm
    {
        bool voidBuyReturn;
        bool voidBuy;
        //bool voidCashTransfer;
        bool voidBanktoShopCashTransfer;
        bool voidShopToBankCashTransfer;
        bool voidStoreCashTransfer;
        bool voidMerchandiseTransfer;
        bool voidSaleRefund;
        bool voidLayaway;
        bool noDataFound;
        bool voidSale;
        bool voidReleaseFingerprints;
        bool getCustomerInfo;

        bool isVoidMDSETransIN = false;

        bool isVoidMDSETransOut = false;

        public VoidTransactionForm()
        {
            InitializeComponent();
        }

        public VoidTransactionForm(VoidSelector.VoidTransactionType type)
        {
            InitializeComponent();

            if (type == VoidSelector.VoidTransactionType.VOIDMDSETRANSOUT)
            {
                isVoidMDSETransOut = true;
            }
            else
            {
                isVoidMDSETransIN = true;
            }
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
        }

        private void customButtonOK_Click(object sender, EventArgs e)
        {
            labelErrorMessage.Visible = false;
            noDataFound = false;
            PurchaseVO purchaseObj = null;
            CustomerVO customerObj = null;
            SaleVO saleObj = null;
            LayawayVO layawayObj = null;
            CashTransferVO bankTransferObj = null;
            CashTransferVO shopCashTransferObj = null;
            DataSet mdseInfo = null;
            string errorCode;
            string errorText;
            getCustomerInfo = true;
            string tenderType;
            GlobalDataAccessor.Instance.DesktopSession.Purchases = new List<PurchaseVO>();
            GlobalDataAccessor.Instance.DesktopSession.Sales = new List<SaleVO>();
            GlobalDataAccessor.Instance.DesktopSession.Layaways = new List<LayawayVO>();
            ReleaseFingerprintsInfo releaseFingerprintsequest = null;
            bool retValue = false;
            if (voidBuyReturn)
            {
                retValue = PurchaseProcedures.GetPurchaseData(Utilities.GetIntegerValue(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, 0),
                                                              Utilities.GetIntegerValue(customTextBoxTranNo.Text, 0), "2", StateStatus.BLNK, "RET", true, out purchaseObj, out customerObj, out tenderType, out errorCode, out errorText);
            }
            else if (voidBuy)
            {
                retValue = PurchaseProcedures.GetPurchaseData(Utilities.GetIntegerValue(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, 0),
                                                              Utilities.GetIntegerValue(customTextBoxTranNo.Text, 0), "2", StateStatus.BLNK, "", true, out purchaseObj, out customerObj, out tenderType,  out errorCode, out errorText);
            }
            else if (voidSale)
            {
                retValue = RetailProcedures.GetSaleData(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.OracleDA, Utilities.GetIntegerValue(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, 0),
                                                        Utilities.GetIntegerValue(customTextBoxTranNo.Text, 0), "3", StateStatus.BLNK, "SALE", false, out saleObj, out customerObj, out errorCode, out errorText);
            }
            else if (voidLayaway)
            {
                retValue = RetailProcedures.GetLayawayData(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.OracleDA, Utilities.GetIntegerValue(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, 0),
                                                           Utilities.GetIntegerValue(customTextBoxTranNo.Text, 0), "4", StateStatus.BLNK, "ALL", false, out layawayObj, out customerObj, out errorCode, out errorText);
            }
            else if (voidSaleRefund)
            {
                retValue = RetailProcedures.GetSaleData(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.OracleDA, Utilities.GetIntegerValue(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, 0),
                                                        Utilities.GetIntegerValue(customTextBoxTranNo.Text, 0), "3", StateStatus.BLNK, "REFUND", false, out saleObj, out customerObj, out errorCode, out errorText);
            } //BZ # 419
            else if (voidBanktoShopCashTransfer)
            {
                retValue = ShopCashProcedures.GetBankTransferDetails(customTextBoxTranNo.Text, "BANKTOSHOP", GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,GlobalDataAccessor.Instance.DesktopSession,
                                                                     out bankTransferObj, out errorCode, out errorText);
            }
            else if (voidShopToBankCashTransfer)
            {
                retValue = ShopCashProcedures.GetBankTransferDetails(customTextBoxTranNo.Text, "SHOPTOBANK", GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,GlobalDataAccessor.Instance.DesktopSession,
                                                                     out bankTransferObj, out errorCode, out errorText);
            }//BZ # 419 end

            else if (voidStoreCashTransfer)
            {
                string storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                retValue = ShopCashProcedures.GetShopCashTransferData(customTextBoxTranNo.Text, "", storeNumber, GlobalDataAccessor.Instance.DesktopSession, out shopCashTransferObj, out errorCode, out errorText);
            }
            else if (voidMerchandiseTransfer)
            {
                string storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                string tranNo = customTextBoxTranNo.Text;

                if (customTextBoxTranNo.Text.Length > 6)
                    tranNo = tranNo.Substring(5);

                retValue = VoidProcedures.getTransferToVoid(Utilities.GetIntegerValue(tranNo, 0),
                                                            storeNumber, out mdseInfo, out errorCode, out errorText);
                //                retValue = VoidProcedures.GetEligibleToScrapItems(Utilities.GetIntegerValue(customTextBoxTranNo.Text, 0), 
                //                    storeNumber, out mdse , out errorCode, out errorText);
            }
            else if (voidReleaseFingerprints)
            {
                string storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                string tranNo = customTextBoxTranNo.Text;

                if (tranNo.Length > 6)
                    tranNo = tranNo.Substring(5);

                // Add procedure to VoidsProcedurees to Locate the Release Fingerprint Authorization
                GlobalDataAccessor.Instance.FingerPrintRelaseAuthorizationInfo = 
                    HoldsProcedures.GetReleaseFingerprintAuthorization(tranNo, 
                                    storeNumber, out errorCode, out errorText);
                


                

            }

            if (voidBuy || voidBuyReturn)
            {
                if (retValue && purchaseObj != null)
                {
                    GlobalDataAccessor.Instance.DesktopSession.Purchases.Add(purchaseObj);
                    if (purchaseObj.EntityType != "V" && customerObj != null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = customerObj;
                    }

                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                }
                else
                {
                    noDataFound = true;
                }
            }
            else if (voidSale || voidSaleRefund)
            {
                if (retValue && saleObj != null)
                {
                    if (saleObj.RefType == "4")
                    {
                        MessageBox.Show("This sale originated from a layaway. You have to void the last layaway payment and not the sale");
                        return;
                    }
                    GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = customerObj;
                    GlobalDataAccessor.Instance.DesktopSession.Sales.Add(saleObj);
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                }
                else
                    noDataFound = true;
            }
            else if (voidLayaway)
            {
                if (retValue && layawayObj != null)
                {
                    GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = customerObj;
                    GlobalDataAccessor.Instance.DesktopSession.Layaways.Add(layawayObj);
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                }
                else
                    noDataFound = true;
            }
            //BZ # 419
            //else if (voidCashTransfer)
            else if (voidBanktoShopCashTransfer || voidShopToBankCashTransfer)
            {
            //BZ # 419 end
                if (retValue && bankTransferObj != null)
                {
                    GlobalDataAccessor.Instance.DesktopSession.CashTransferData = bankTransferObj;
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                }
                else
                {
                    noDataFound = true;
                }
            }
            else if (voidStoreCashTransfer)
            {
                if (retValue && shopCashTransferObj != null)
                {
                    GlobalDataAccessor.Instance.DesktopSession.CashTransferData = shopCashTransferObj;
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                }
                else
                {
                    noDataFound = true;
                }
            }
            else if (voidMerchandiseTransfer)
            {
                if (retValue && mdseInfo != null && mdseInfo.Tables.Count > 0)
                {
                    if (isFaultyTransferINOUT(mdseInfo))
                    {
                        noDataFound = true;
                    }
                    else
                    {
                        GlobalDataAccessor.Instance.DesktopSession.MdseTransferData = mdseInfo;
                        GlobalDataAccessor.Instance.DesktopSession.SelectedTransferNumber = customTextBoxTranNo.Text;
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    }
                }
                else
                    noDataFound = true;
            }
            else if (voidReleaseFingerprints && GlobalDataAccessor.Instance.FingerPrintRelaseAuthorizationInfo != null)
            {
                this.DialogResult = DialogResult.OK;
                //GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
            }

            if (noDataFound)
            {
                labelErrorMessage.Text = "The number entered is not valid. Please enter another number";
                labelErrorMessage.Visible = true;
                return;
            }
        }

        private bool isFaultyTransferINOUT(DataSet mdseInfo)
        {
            bool retVal = false;

            if (mdseInfo == null || (mdseInfo.Tables["TRANSINFO"].Rows[0] == null)) return false;

            //Added check to avoid picking already voided transfer
            bool retVal1 = (mdseInfo.Tables["TRANSINFO"].Rows[0]["status"].Equals("VO")) ? true : false;
            if (retVal1)
                return true;

            if (isVoidMDSETransIN)
            {
                retVal = (mdseInfo.Tables["TRANSINFO"].Rows[0]["status"].Equals("TO")) ? true : false;
                return retVal;
            }

            if (isVoidMDSETransOut)
            {
                retVal = (mdseInfo.Tables["TRANSINFO"].Rows[0]["status"].Equals("TI")) ? true : false;
                return retVal;
            }

            return retVal;
        }

        private void VoidTransactionForm_Load(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDBUYRETURN, StringComparison.OrdinalIgnoreCase))
                voidBuyReturn = true;
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDBUY, StringComparison.OrdinalIgnoreCase))
                voidBuy = true;
            //BZ # 419
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDBANKTOSHOP, StringComparison.OrdinalIgnoreCase))
                voidBanktoShopCashTransfer = true;
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDSHOPTOBANK, StringComparison.OrdinalIgnoreCase))
                voidShopToBankCashTransfer = true;
            //BZ # 419 end
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDSHOPTOSHOPTRANSFER, StringComparison.OrdinalIgnoreCase))
                voidStoreCashTransfer = true;
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDMERCHANDISETRANSFER, StringComparison.OrdinalIgnoreCase))
                voidMerchandiseTransfer = true;
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDSALE, StringComparison.OrdinalIgnoreCase))
                voidSale = true;
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDREFUND, StringComparison.OrdinalIgnoreCase))
                voidSaleRefund = true;
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDLAYAWAY, StringComparison.OrdinalIgnoreCase))
                voidLayaway = true;
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDRELEASEFINGERPRINTS, StringComparison.OrdinalIgnoreCase))
            {
                voidReleaseFingerprints = true;
                customLabelHeading.Text = "Authorization #: ";
            }

            if (voidBuy || voidBuyReturn)
                GlobalDataAccessor.Instance.DesktopSession.Purchases = null;
            else if (voidSale || voidSaleRefund)
                GlobalDataAccessor.Instance.DesktopSession.Sales = null;
            else if (voidLayaway)
                GlobalDataAccessor.Instance.DesktopSession.Layaways = null;

            //BZ # 419
            //if (voidMerchandiseTransfer || voidCashTransfer || voidStoreCashTransfer)
            if (voidMerchandiseTransfer || voidBanktoShopCashTransfer || voidShopToBankCashTransfer || voidStoreCashTransfer)
            {
                //BZ # 419 end
                customLabelHeading.Text = "Enter Transfer Number";
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.ActiveControl.Equals(customButtonCancel) && customButtonCancel.Enabled && keyData == Keys.Enter)
            {
                customButtonCancel.PerformClick();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                customButtonOK.PerformClick();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                customButtonCancel.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
