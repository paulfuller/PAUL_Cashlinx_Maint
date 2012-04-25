using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Reports;

namespace Pawn.Logic.DesktopProcedures
{
    public class PfiProcedures
    {

        public static bool ExecuteVendorPFI
            (
            int ticketNumber,
            PurchaseVO vendorPurchaseObj,
            VendorVO vendor,
            out string sErrorCode,
            out string sErrorText
            )
        {

            List<TransferItemVO> _mdseToTransfer = new List<TransferItemVO>();
            int pawnItemIdx = 1;
            sErrorCode = "0";
            sErrorText = "";
            bool itemsPfid = false;
            try
            {
                //Start transaction block
                GlobalDataAccessor.Instance.beginTransactionBlock();


                //Step 1 Update Gun book if the item has a gun
                foreach (Item purchaseItem in vendorPurchaseObj.Items)
                {
                    if (purchaseItem.IsExpenseItem)
                    {
                        pawnItemIdx = pawnItemIdx + purchaseItem.Quantity;
                        continue;
                    }
                    for (int i = 1; i <= purchaseItem.Quantity; i++)
                    {
                       
                        if (!purchaseItem.IsJewelry && purchaseItem.Quantity > 1 && purchaseItem.SerialNumber != null && purchaseItem.SerialNumber.Count > 0)
                        {
                            Item itemCopy = Utilities.CloneObject(purchaseItem);
                            string sItemPrefix;
                            string sDescription;
                            //Get updated mdse description for each serial number
                            Item.RemoveSerialNumberFromDescription(ref itemCopy, out sItemPrefix, out sDescription);
                            purchaseItem.Attributes = itemCopy.Attributes;
                            purchaseItem.TicketDescription = sDescription;
                        }
                        purchaseItem.mDocNumber = ticketNumber;
                        purchaseItem.mItemOrder = i;
                        int qty = purchaseItem.QuickInformation.Quantity < 1 ? 1 : purchaseItem.QuickInformation.Quantity;
                        if (purchaseItem.CaccLevel==0)
                        {
                            // Call Update_Cacc_Info()
                            MerchandiseProcedures.UpdateCaccInfo(GlobalDataAccessor.Instance.DesktopSession,
                                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                0,
                                purchaseItem.mDocNumber,
                                "7",
                                1,
                                0,
                                qty,
                                purchaseItem.ItemAmount * qty,
                                purchaseItem.CategoryCode,
                                ShopDateTime.Instance.ShopDate,
                                out sErrorCode,
                                out sErrorText
                                );
                        }

                        if (purchaseItem.IsGun)
                        {
                            ProcessTenderProcedures.ExecuteUpdateGunBookRecord(
                                 ticketNumber.ToString(),
                                 GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                 ProductStatus.PFI.ToString(),
                                 "",
                                 vendor.Name,
                                 "",
                                 "",
                                 vendor.Address1,
                                 vendor.City,
                                 vendor.State,
                                 vendor.ZipCode,
                                 vendor.Ffl,
                                 "",
                                 "",
                                 "P",
                                 GlobalDataAccessor.Instance.DesktopSession.UserName,
                                 ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                 ShopDateTime.Instance.ShopTransactionTime,
                                 "",
                                 purchaseItem.QuickInformation.Caliber,
                                 purchaseItem.QuickInformation.Importer,
                                 purchaseItem.QuickInformation.SerialNumber,
                                 purchaseItem.QuickInformation.Model,
                                 purchaseItem.QuickInformation.Manufacturer,
                                 purchaseItem.mStore,
                                 purchaseItem.mYear,
                                 purchaseItem.mDocNumber,
                                 purchaseItem.mDocType,
                                pawnItemIdx,
                                 0,
                                 vendorPurchaseObj.GunNumbers[i-1],
                                 out sErrorCode,
                                 out sErrorText
                                 );
                        }

                        //Step 2 Insert mdse revision record


                        bool mdseRevVal = MerchandiseProcedures.InsertMerchandiseRevision(GlobalDataAccessor.Instance.DesktopSession,
                              GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                              purchaseItem.mYear,
                              ticketNumber,
                              purchaseItem.mDocType,
                              pawnItemIdx,
                              0,
                              purchaseItem.mStore,
                              ticketNumber.ToString(),
                              purchaseItem.mDocType,
                              "",
                              0,
                              purchaseItem.ItemReason == ItemReason.CACC ? "PFC" : "PFI",
                              "",
                              "",
                              GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                              out sErrorCode,
                              out sErrorText
                              );

                        purchaseItem.Icn = Utilities.IcnGenerator(purchaseItem.mStore,
                            purchaseItem.mYear, ticketNumber, "2", pawnItemIdx, 0);


                        TransferItemVO transferData = new TransferItemVO();
                        transferData.ICN = purchaseItem.Icn.ToString();
                        transferData.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                        transferData.ICNQty = purchaseItem.QuickInformation.Quantity > 0
                                                         ? purchaseItem.QuickInformation.Quantity.ToString()
                                                         : "1";
                        transferData.CustomerNumber = "";
                        transferData.TransactionDate = ShopDateTime.Instance.ShopDate;
                        transferData.MdseRecordDate = ShopDateTime.Instance.ShopDate;
                        transferData.MdseRecordTime = ShopDateTime.Instance.ShopTransactionTime;
                        transferData.MdseRecordUser = GlobalDataAccessor.Instance.DesktopSession.UserName;
                        transferData.MdseRecordDesc = "";
                        transferData.MdseRecordChange = 0;
                        transferData.MdseRecordType = "";
                        transferData.ClassCode = "";
                        transferData.AcctNumber = "";
                        transferData.CreatedBy = GlobalDataAccessor.Instance.DesktopSession.UserName;
                        transferData.GunNumber = vendorPurchaseObj.GunNumbers[i - 1] > 0 ? vendorPurchaseObj.GunNumbers[i - 1].ToString() : null;
                        transferData.GunType = purchaseItem.QuickInformation.GunType;
                        transferData.ItemDescription = purchaseItem.TicketDescription;
                        transferData.ItemCost = purchaseItem.ItemAmount;
                        _mdseToTransfer.Add(transferData);
                        itemsPfid = true;
                        pawnItemIdx++;
                    }

                }

                //Step 3 Insert receipt for PFI
                if (itemsPfid)
                {
                    ReceiptDetailsVO receiptDetailsVO = new ReceiptDetailsVO();
                    receiptDetailsVO.ReceiptDate = ShopDateTime.Instance.ShopDate;
                    receiptDetailsVO.RefDates = new List<string>() { ShopDateTime.Instance.ShopDate.ToShortDateString() };
                    receiptDetailsVO.RefTimes = new List<string>() { ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString() };
                    receiptDetailsVO.UserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                    receiptDetailsVO.RefNumbers = new List<string>() { ticketNumber.ToString() };
                    receiptDetailsVO.RefTypes = new List<string>() { "2" };
                    receiptDetailsVO.RefEvents = new List<string>() { "PFI" };
                    receiptDetailsVO.RefAmounts = new List<string>() { vendorPurchaseObj.Amount.ToString() };
                    receiptDetailsVO.RefStores = new List<string>() { GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber };

                    ProcessTenderProcedures.ExecuteInsertReceiptDetails(
                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                        ShopDateTime.Instance.ShopDate.ToShortDateString(),
                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                        ref receiptDetailsVO,
                        out sErrorCode,
                        out sErrorText);
                }

                GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);
            }
            catch (Exception ex)
            {
                GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                FileLogger.Instance.logMessage(LogLevel.ERROR, null, "Error in vendor purchase pfi process while processing PFI " + ex.Message);
                sErrorCode = "1";
                sErrorText = ex.Message;
                return false;
            }



            //Step 4 Invoke transfer process
            int transferNumber = 0;

            var errorMessage = string.Empty;
            if (_mdseToTransfer.Count > 0 && GlobalDataAccessor.Instance.CurrentSiteId.IsTopsExist)
            {

                bool retValue = TransferProcedures.TransferItemsOutOfStore(_mdseToTransfer, out transferNumber,"", out errorMessage, false, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
                if (!retValue)
                {
                    MessageBox.Show("Error:" + errorMessage);
                    sErrorText = errorMessage;
                    return false;
                }
                foreach (TransferItemVO transfer in _mdseToTransfer)
                {
                    transfer.TransferNumber = transferNumber;
                }
            }

            if (transferNumber > 0)
            {
                try
                {
                    GlobalDataAccessor.Instance.beginTransactionBlock();
                    //Step 5 Insert receipt for transfer
                    ReceiptDetailsVO receiptDetailsVO = new ReceiptDetailsVO();
                    receiptDetailsVO.ReceiptDate = ShopDateTime.Instance.ShopDate;
                    receiptDetailsVO.RefDates = new List<string>() { ShopDateTime.Instance.ShopDate.ToShortDateString() };
                    receiptDetailsVO.RefTimes = new List<string>() { ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString() };
                    receiptDetailsVO.UserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                    receiptDetailsVO.RefNumbers = new List<string>() { ticketNumber.ToString() };
                    receiptDetailsVO.RefTypes = new List<string>() { "2" };
                    receiptDetailsVO.RefEvents = new List<string>() { "TO" };
                    receiptDetailsVO.RefAmounts = new List<string>() { vendorPurchaseObj.Amount.ToString() };
                    receiptDetailsVO.RefStores = new List<string>() { GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber };

                    ProcessTenderProcedures.ExecuteInsertReceiptDetails(
                         GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                         GlobalDataAccessor.Instance.DesktopSession.UserName,
                         ShopDateTime.Instance.ShopDate.ToShortDateString(),
                         GlobalDataAccessor.Instance.DesktopSession.UserName,
                         ref receiptDetailsVO,
                         out sErrorCode,
                         out sErrorText);
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    //Step 6 Print transfer report

                    /*TransferOutReport trnsfrRpt = new TransferOutReport();
                    trnsfrRpt.MdseTransfer = _mdseToTransfer;
                     */
                    string logPath =
                      SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration
                              .BaseLogPath;

                    TransferOutReport trnsfrRpt = new TransferOutReport(_mdseToTransfer, ShopDateTime.Instance.ShopDateCurTime, GlobalDataAccessor.Instance.CurrentSiteId.StoreName,
                              Convert.ToString(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber), GlobalDataAccessor.Instance.DesktopSession.UserName, Convert.ToString(transferNumber), logPath, "PFIProc", new ReportObject.TransferReport(), PdfLauncher.Instance);
                    trnsfrRpt.CreateReport();
                    //TODO: Store report in couch db
                    if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                    {
                        string laserPrinterIp = GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress;
                        int laserPrinterPort = GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port;
                        PrintingUtilities.printDocument(trnsfrRpt.getReportWithPath(),
                                                            laserPrinterIp,
                                                            laserPrinterPort,
                                                            1);
                    }

                    //trnsfrRpt.ShowDialog();
                    return true;
                }
                catch (Exception ex)
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    FileLogger.Instance.logMessage(LogLevel.ERROR, null, "Error in vendor purchase pfi process while entering receipt for transfer and printing transfer report " + ex.Message);
                    sErrorCode = "1";
                    sErrorText = ex.Message;
                    return false;
                }
            }
            return true;
        }
    }
}
