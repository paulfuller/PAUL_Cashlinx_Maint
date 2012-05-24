/****************************************************************************
* 03/26/2010 SR   -Added transaction time to the gun book update SP call
 *                - Changed the status code that was passed to update gun book to 
 *                 pass PFI instead of the pawn item's status which would be IP when
 *                 transaction type is E or P
 *04/01/2010 SR Commented all the merchandise revision SP calls as per Jake
 *04/12/2010 SR Uncommented calls to merchandise revision inserts for RETAIL and PFI/PFC
 *              Only the loan Principal data will be written by call to tub calc.
 *04/14/2010 DW Moved PFI Tag printing to DescribeItem.cs page
 *
 *04/26/2010 SR Changed the ref number for the receipt detail entry for PFI and TO
 *              to be the ticket number not the original ticket number
 *05/04/2010 SR If an item was merged removed it from transfer
 *05/07/2010 SR If transfer failed added logic so tub calc was not called
 *10/05/2010 SR Added check to not do TOPS transfer if the store does not have TOPS
 *11/18/2010 Madhu fix for defect PWNU00001443
 *12/09/2011 SR Added logic to not update cacc bucket if item is on PH or its set as 
 *merchandise not available
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Pawn.Logic.DesktopProcedures;
using Reports;

namespace Pawn.Forms.Pawn.Services.PFI
{
    public partial class PFI_Posting : Form
    {
        private List<PairType<string, string>> _PostingErrors;

        private DataTable _GridTable = null;
        private PFI_ProductData _pfiLoan;
        private int _Tags;
        private int _Tickets;
        private decimal _TotalAmount;
        private List<PFI_TransitionData> _lstTransitionData;
        private List<TransferItemVO> _mdseToTransfer;
        private List<string> chargeOffBRKNIcn;
        private List<string> chargeOffNXTIcn;
        private List<string> chargeOffSTRUIcn;
        private List<string> jCase;
        private List<string> statusBRKNReason;
        private List<string> statusNXTReason;
        private List<string> statusSTRUReason;
        private List<string> retailPrice;
        private List<int> struqty;
        private List<int> nxtqty;
        private List<int> brknqty;
        private List<string> CompletedItems;


        private int chargeOffItems;

        private List<int> qty;

        private List<ReceiptDetailsVO> transferReceipts; // TL 02-09-2010 added for transfer receipt details

        public PFI_Posting()
        {
            InitializeComponent();
            _mdseToTransfer = new List<TransferItemVO>();
            chargeOffBRKNIcn = new List<string>();
            chargeOffNXTIcn = new List<string>();
            chargeOffSTRUIcn = new List<string>();
            jCase = new List<string>();
            statusBRKNReason = new List<string>();
            statusNXTReason = new List<string>();
            statusSTRUReason = new List<string>();
            retailPrice = new List<string>();
            qty = new List<int>();
            struqty = new List<int>();
            brknqty = new List<int>();
            nxtqty = new List<int>();
            CompletedItems = new List<string>();
            
        }

        private void Setup()
        {
            string sErrorCode;
            string sErrorText;
            decimal dStatusCode = 0;
            _PostingErrors = new List<PairType<string, string>>();

            refurbButton.Enabled = false;
            chargeOffListButton.Enabled = false;
            printButton.Enabled = false;

            asOfLabel.Text = ShopDateTime.Instance.ShopDate.ToShortDateString();

            if (StoreLoans.GetLoanTransition(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                 0,
                 ProductType.ALL,
                 StateStatus.PFI,
                 out _lstTransitionData,
                 out dStatusCode,
                 out sErrorCode,
                 out sErrorText) == false)
            {
                // There was an issue with the PFI posting
                MessageBox.Show(sErrorText);
                this.Close();
                return;

            }

            foreach (PFI_TransitionData pfiTransitionData in _lstTransitionData)
            {
                if (pfiTransitionData.pfiLoan.UpdatedObject != null)
                {
                    if (pfiTransitionData.pfiLoan.UpdatedObject.TempStatus != StateStatus.PFI)
                        _lstTransitionData.Remove(pfiTransitionData);
                }
            }

            if (_lstTransitionData.Count > 0)
            {
                DataTable myTable = new DataTable();
                myTable.Columns.Add("colRefurb");
                myTable.Columns.Add("colAssignmentType");
                myTable.Columns.Add("colNumber");
                myTable.Columns.Add("colDescription");
                //myTable.Columns.Add("colTags");
                myTable.Columns.Add("colCost");
                myTable.Columns.Add("colRetail");
                myTable.Columns.Add("colReason");
                

                foreach (PFI_TransitionData pfiTransitionData in _lstTransitionData)
                {
                    _pfiLoan = pfiTransitionData.pfiLoan;

                    int iRowNumber = 1;
                    _Tickets++;
                    foreach (Item pawnItem in _pfiLoan.UpdatedObject.Items)
                    {
                        string sOrigDescription;
                        string sUpdatedDescription;
                        int iOrigTags;
                        int iUpdatedTags;
                        decimal dOrigAmount;
                        decimal dUpdatedAmount;
                        decimal dOrigRetail;
                        decimal dUpdatedRetail;
                        string pirOrigReason;
                        string pirUpdatedReason;

                        GetItemInformation(pawnItem,
                                           out sOrigDescription,
                                           out sUpdatedDescription,
                                           out iOrigTags,
                                           out iUpdatedTags,
                                           out dOrigAmount,
                                           out dUpdatedAmount,
                                           out dOrigRetail,
                                           out dUpdatedRetail,
                                           out pirOrigReason,
                                           out pirUpdatedReason
                            );

                        DataRow myRow = myTable.NewRow();
                        myRow["colRefurb"] = pawnItem.RefurbNumber > 0
                                                                ? pawnItem.RefurbNumber.ToString()
                                                                : "";
                        myRow["colAssignmentType"] = pawnItem.PfiAssignmentType;
                        myRow["colNumber"] = _pfiLoan.UpdatedObject.TicketNumber;

                        if (sOrigDescription != sUpdatedDescription)
                        {
                            myRow["colDescription"] = "[" + (iRowNumber++).ToString() + "] " +
                                                                     sUpdatedDescription
                                                                     + Environment.NewLine + Environment.NewLine
                                                                     + "[OLD] "
                                                                     + sOrigDescription;
                        }
                        else
                        {
                            myRow["colDescription"] = "[" + (iRowNumber++).ToString() + "] " +
                                                                     sOrigDescription;
                        }

                        //myRow["colTags"] = iUpdatedTags.ToString();
                        _Tags += iUpdatedTags;

                        if (dOrigAmount != dUpdatedAmount)
                        {
                            _TotalAmount += dUpdatedAmount;
                            myRow["colCost"] = String.Format("{0:C}", dUpdatedAmount)
                                                              + Environment.NewLine + Environment.NewLine
                                                              + "[" + String.Format("{0:C}", dOrigAmount) + "]";
                        }
                        else
                        {
                            _TotalAmount += dOrigAmount;
                            myRow["colCost"] = String.Format("{0:C}", dOrigAmount);
                        }

                        if (dOrigRetail != dUpdatedRetail) // && dOrigRetail > 0)
                        {
                            myRow["colRetail"] = String.Format("{0:C}", dUpdatedRetail);
                            if (dOrigRetail > 0)
                            {
                                myRow["colRetail"] = String.Format("{0:C}", dUpdatedRetail)
                                                                    + Environment.NewLine + Environment.NewLine
                                                                    + "[" + String.Format("{0:C}", dOrigRetail) + "]";
                            }
                        }
                        else
                        {
                            myRow["colRetail"] = String.Format("{0:C}", dOrigRetail);
                        }

                        if (pirOrigReason != pirUpdatedReason) // && pirOrigReason != "")
                        {
                            myRow["colReason"] = pirUpdatedReason;
                            if (pirOrigReason != string.Empty)
                                myRow["colReason"] += pirUpdatedReason
                                                        + Environment.NewLine
                                                        + Environment.NewLine
                                                        + "[" + pirOrigReason + "]";
                        }
                        else
                        {
                            myRow["colReason"] = pirOrigReason;
                        }
                        myTable.Rows.Add(myRow);
                    }
                    totalTicketsLabel.Text = _Tickets.ToString();
                    totalCostLabel.Text = String.Format("{0:C}", _TotalAmount);
                }

                List<ItemReasonCode> reasonCodes = ItemReasonFactory.Instance.GetChargeOffCodes(GlobalDataAccessor.Instance.DesktopSession.PawnSecApplication);
                
                foreach (PFI_TransitionData pfiTransitionData in _lstTransitionData)
                {
                    _pfiLoan = pfiTransitionData.pfiLoan;

                    if (!refurbButton.Enabled)
                        refurbButton.Enabled = _pfiLoan.UpdatedObject.Items
                                           .FindIndex(pi => pi.RefurbNumber > 0) >= 0;

                    if (!chargeOffListButton.Enabled)
                        chargeOffListButton.Enabled = _pfiLoan.UpdatedObject.Items
                                              .FindIndex(pi => reasonCodes
                                              .FindIndex(rc => rc.Reason == pi.ItemReason) >= 0)
                                              >= 0;
                }

                gvPostings.DataSource = myTable;
                _GridTable = myTable;
            }

            if (_lstTransitionData.Count > 0)
            {
                printButton.Enabled = true;
            }
            else
            {
                MessageBox.Show("No records eligible for PFI posting found.");
                this.Close();
            }
        }

        private void GetItemInformation(Item pawnItem,
            out string sOrigDescription, out string sUpdatedDescription,
            out int iOrigTags, out int iUpdatedTags,
            out decimal dOrigAmount, out decimal dUpdatedAmount,
            out decimal dOrigRetail, out decimal dUpdatedRetail,
            out string pirOrigReason, out string pirUpdatedReason
            )
        {
            sOrigDescription = pawnItem.TicketDescription;
            sUpdatedDescription = pawnItem.TicketDescription;
            iOrigTags = Utilities.GetIntegerValue(pawnItem.PfiTags, 0);
            iUpdatedTags = Utilities.GetIntegerValue(pawnItem.PfiTags, 0);
            dOrigAmount = pawnItem.ItemAmount;
            dUpdatedAmount = pawnItem.ItemAmount;
            dOrigRetail = pawnItem.RetailPrice;
            dUpdatedRetail = pawnItem.RetailPrice;
            pirOrigReason = pawnItem.ItemReason.ToString();
            pirUpdatedReason = pawnItem.ItemReason.ToString();

            string pirTemp = string.Empty;

            try
            {
                int iOriginalIdx = _pfiLoan.OriginalObject.Items
                                            .FindIndex(pi => pi.Icn == pawnItem.Icn);
                int iMergedStgItemIdx = _pfiLoan.MergedItems
                                            .FindIndex(pi => pi.OriginalItems
                                                .FindIndex(opi => opi.Icn == pawnItem.Icn) >= 0);

                if (iOriginalIdx >= 0)
                {
                    sOrigDescription = _pfiLoan.OriginalObject.Items[iOriginalIdx].TicketDescription;
                    iOrigTags = Utilities.GetIntegerValue(_pfiLoan.OriginalObject.Items[iOriginalIdx].PfiTags, 0);
                    dOrigAmount = _pfiLoan.OriginalObject.Items[iOriginalIdx].ItemAmount;
                    dOrigRetail = _pfiLoan.OriginalObject.Items[iOriginalIdx].RetailPrice;
                    pirOrigReason = _pfiLoan.OriginalObject.Items[iOriginalIdx].ItemReason.ToString();
                }
                //else
                //{
                //    sUpdatedDescription = _pfiLoan.UpdatedObject.Items
                //                           .Find(pi => pi.Icn == pawnItem.Icn).TicketDescription;
                //    iUpdatedTags        = Utilities.GetIntegerValue(_pfiLoan.UpdatedObject.Items
                //                            .Find(pi => pi.Icn == pawnItem.Icn).PfiTags, 0);
                //    dUpdatedAmount      = _pfiLoan.UpdatedObject.Items
                //                            .Find(pi => pi.Icn == pawnItem.Icn).LoanAmount;
                //    dUpdatedRetail      = _pfiLoan.UpdatedObject.Items
                //                            .Find(pi => pi.Icn == pawnItem.Icn).RetailPrice;
                //    pirUpdatedReason    = _pfiLoan.UpdatedObject.Items
                //                            .Find(pi => pi.Icn == pawnItem.Icn).ItemReason.ToString();
                //}

                if (iMergedStgItemIdx >= 0)
                {
                    string sMergeICN = _pfiLoan.MergedItems[iMergedStgItemIdx].NewItem.Icn;
                    string sMergeDescription = _pfiLoan.MergedItems[iMergedStgItemIdx].NewItem.TicketDescription;
                    int iMergeTags = Utilities.GetIntegerValue(_pfiLoan.MergedItems[iMergedStgItemIdx].NewItem.PfiTags, 0);
                    decimal dMergeAmount = _pfiLoan.MergedItems[iMergedStgItemIdx].NewItem.ItemAmount;
                    decimal dRetail = _pfiLoan.MergedItems[iMergedStgItemIdx].NewItem.RetailPrice;
                    ItemReason pirMerge = _pfiLoan.MergedItems[iMergedStgItemIdx].NewItem.ItemReason;

                    if (pawnItem.Icn == sMergeICN)
                    {
                        sUpdatedDescription = sMergeDescription;
                        iUpdatedTags = Utilities.GetIntegerValue(pawnItem.PfiTags, 0);
                        dUpdatedAmount = pawnItem.ItemAmount;
                        dUpdatedRetail = pawnItem.RetailPrice;
                        pirUpdatedReason = pawnItem.ItemReason.ToString();
                    }
                    else
                    {
                        sUpdatedDescription = pawnItem.TicketDescription;
                        iUpdatedTags = Utilities.GetIntegerValue(pawnItem.PfiTags, 0);
                        dUpdatedAmount = pawnItem.ItemAmount;
                        dUpdatedRetail = pawnItem.RetailPrice;
                        pirUpdatedReason = pawnItem.ItemReason.ToString();
                    }
                }

                pirTemp = pirOrigReason;
                pirOrigReason = ItemReasonFactory.Instance.FindByReason(pirTemp).Description;
                pirTemp = pirUpdatedReason;
                pirUpdatedReason = ItemReasonFactory.Instance.FindByReason(pirTemp).Description;

            }
            catch(Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "PFI_Posting", "Exception thrown in GetItemInformation: {0}", eX);
                }
                BasicExceptionHandler.Instance.AddException("PFI_Posting - Exception thrown in GetItemInformation", eX);
            }
        }

        private static string GetItemReason(ItemReason pawnItemReason)
        {
            string sReason;
            try
            {
                sReason = ItemReasonFactory.Instance.FindByReason(pawnItemReason).Description;
            }
            catch (Exception)
            {
                sReason = string.Empty;
            }
            return sReason;
        }

        private void PFI_Posting_Load(object sender, EventArgs e)
        {
            Setup();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            foreach (var tranData in _lstTransitionData)
            {
                var loan = tranData.pfiLoan.UpdatedObject;
                string errorCode;
                string errorText;
                StoreLoans.UpdateLoanTransitionStatus(loan.TicketNumber, "", 
                        loan.OrgShopNumber.ToString().PadLeft(5, '0'), out errorCode, out errorText);
            }
            //StoreLoans.UpdateLoanTransitionStatus()
            this.Close();
        }

        private List<PFI_TransitionData> getChargeOffData ()
        {
            var ptChargeCodes = ItemReasonFactory.Instance.GetChargeOffCodes(GlobalDataAccessor.Instance.DesktopSession.PawnSecApplication);

            return  _lstTransitionData
                                                        .FindAll(td => td.pfiLoan.UpdatedObject.Items
                                                        .FindIndex(pi => ptChargeCodes
                                                        .FindIndex(cc => cc.Reason == pi.ItemReason) >= 0) >= 0);        
        }
        private void chargeOffListButton_Click(object sender, EventArgs e)
        {
            var _lstChargeOffData = getChargeOffData();

            if (_lstChargeOffData != null && _lstChargeOffData.Any())
            {
                PFI_ChargeOffList myForm = new PFI_ChargeOffList()
                {
                    TransitionDatas = _lstChargeOffData,
                    AsOf = asOfLabel.Text
                };
                myForm.ShowDialog(this);
            }
        }

        private void refurbButton_Click(object sender, EventArgs e)
        {

            List<PFI_TransitionData> _lstRefurbData = _lstTransitionData
                                                       .FindAll(td => td.pfiLoan.UpdatedObject
                                                       .Items.FindIndex(pi => pi.RefurbNumber > 0) >= 0);


            if (_lstRefurbData != null)
            {
                PFI_RefurbList myForm = new PFI_RefurbList()
                {
                    TransitionDatas = _lstRefurbData,
                    AsOf = asOfLabel.Text
                };

                myForm.ShowDialog(this);
            }
        }

        private void postButton_Click(object sender, EventArgs e)
        {
            //Madhu 11/18/2010 fix for defect PWNU00001443
            DialogResult dialogResult = MessageBox.Show("Click Yes to post", "PFI Posting Confirmation",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult != System.Windows.Forms.DialogResult.Yes) return;

            var sFirstName = string.Empty;
            var sMiddleInitial = string.Empty;
            var sLastName = string.Empty;
            string sCustomerNumber = string.Empty;
            var sAddress = string.Empty;
            var sCity = string.Empty;
            var sState = string.Empty;
            var sZipCode = string.Empty;
            var sAgency = string.Empty;
            var sIDType = string.Empty;
            var sIDValue = string.Empty;
            int iPawnLoanMYear;

            List<PairType<string, long>> gunRecords = new List<PairType<string, long>>();

            this.transferReceipts = new List<ReceiptDetailsVO>(); // TL 02-09-2010 Transfer receipts

            try
            {
                if (new UnderwritePawnLoanUtility(GlobalDataAccessor.Instance.DesktopSession).IsShopClosed(ShopDateTime.Instance.ShopDate))
                {
                    MessageBox.Show("Cannot PFI Post on a non-business day.  Please Post on next business day.", "PFI Posting Data Validation",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check for Holds while on Page
                List<int> lstHoldTicketNumbers = new List<int>();
                if (BeginTransaction("Gun Record Generator"))
                {
                    // Get all Gun Next Record calls before getting into parent Loop
                    foreach (PFI_TransitionData pfiTransitionData in _lstTransitionData)
                    {
                        _pfiLoan = pfiTransitionData.pfiLoan;
                        string sErrorCode;
                        string sErrorText;
                        bool bOriginallyGun;
                        
                        // Iteration for Gun Next Records
                        foreach (Item pawnItem in _pfiLoan.UpdatedObject.Items)
                        {
                            if (pawnItem.ItemStatus == ProductStatus.RET || pawnItem.ItemStatus == ProductStatus.PS)
                            {
                                //Not able to be PFI'd
                                continue;
                            }
                            bOriginallyGun = _pfiLoan.OriginalObject.Items.FindIndex(opi => opi.Icn == pawnItem.Icn) >= 0;
                            if (bOriginallyGun)
                                bOriginallyGun = _pfiLoan.OriginalObject.Items.Find(opi => opi.Icn == pawnItem.Icn).IsGun;

                            // If Item previously was not a gun but now is
                            if ((pawnItem.IsGun || (pawnItem.ItemReason == ItemReason.ADDD && pawnItem.IsGun)) && (pawnItem.GunNumber <= 0))
                            {
                                Int64 nextGunNumber;
                                if (ProcessTenderProcedures.ExecuteGetNextNumber(
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    "GUN",
                                    ShopDateTime.Instance.ShopDate,
                                    out nextGunNumber,
                                    out sErrorCode,
                                    out sErrorText))
                                {
                                    pawnItem.GunNumber = nextGunNumber;
                                }
                                else
                                {
                                    sErrorCode = (sErrorCode ?? "") + "Unable to get next gun number";
                                }

                                if (!sErrorCode.Equals("0"))
                                {
                                    _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                   sErrorText + " | " + pawnItem.Icn));
                                    if (RollbackTransaction("Gun Record Generator"))
                                    {
                                        MessageBox.Show("Cannot PFI Post due to inability to retrieve new gun numbers.",
                                                        "PFI Posting Gun Record Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //Exit pfi posting
                                        return;
                                    }
                                    //Unable to rollback transaction
                                    MessageBox.Show("(1)Cannot PFI Post due to transaction problems.",
                                                    "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                   "(1)Unable to rollback gun record generator block");
                                    BasicExceptionHandler.Instance.AddException("(1)Unable to rollback gun record generator block",
                                                                                new ApplicationException(
                                                                                        "(1)Unable to rollback gun record generator block"));
                                    //Exit pfi posting
                                    return;
                                }
                                gunRecords.Add(new PairType<string, long>(pawnItem.Icn, nextGunNumber));
                            } //End check if item is gun or not
                        } // End foreach for gun number retrieval
                    } //End foreach for pfi transition data

                    //Check if we still have the transaction block
                    if (InTransactionBlock())
                    {
                        if (!CommitTransaction("Gun Record Generator"))
                        {
                            //TODO:Attempt to rollback
                            if (!RollbackTransaction("Gun Record Generator"))
                            {
                                //Log fatal error
                                MessageBox.Show("(2)Cannot PFI Post due to transaction problems.",
                                                "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //Unable to rollback transaction
                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                               "(2)Unable to rollback gun record generator block");
                                BasicExceptionHandler.Instance.AddException("(2)Unable to rollback gun record generator block",
                                                                            new ApplicationException(
                                                                                    "(2)Unable to rollback gun record generator block"));
                                //Exit pfi posting
                                return;
                            }
                        }
                        else
                        {
                            //We were successful
                            if (FileLogger.Instance.IsLogDebug)
                            {
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, "PFI_Posting",
                                                               "Successfully committed gun record generator transaction block");
                            }
                        }
                    }
                    else
                    {
                        //Exit method
                        MessageBox.Show("(3)Cannot PFI Post due to transaction problems.",
                                        "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                       "(3)Unable to stay in gun record generator transaction block");
                        BasicExceptionHandler.Instance.AddException("(3)Unable to stay in gun record generator transaction block",
                                                                    new ApplicationException(
                                                                            "(3)Unable to stay in gun record generator transaction block"));
                        //Exit pfi posting
                        return;
                    }
                } // End BeginTransaction(gun records)
                else
                {
                    //Could not retrieve gun numbers due to inability to start transaction
                    MessageBox.Show("(4)Cannot PFI Post due to transaction problems.",
                                    "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Unable to rollback transaction
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                   "(4)Unable to create gun record generator block");
                    BasicExceptionHandler.Instance.AddException("Unable to create gun record generator block",
                                                                new ApplicationException(
                                                                        "(4)Unable to create gun record generator block"));
                    //Exit pfi posting
                    return;
                }
                var section = string.Empty;
                foreach (PFI_TransitionData pfiTransitionData in _lstTransitionData)
                {
                    section = "Main PFI Posting";
                    if (!BeginTransaction(section))
                    {
                        MessageBox.Show("(5)Cannot PFI Post due to transaction problems in " + section + " section.",
                                        "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        string errMsg = string.Format("(5)Unable to perform pfi posting in {0} section", section);
                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                       errMsg);
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        //Exit pfi posting
                        return;
                    }

                    _pfiLoan = pfiTransitionData.pfiLoan;
                    //Get the first item that is not in return status
                    var itemData = (from item in _pfiLoan.UpdatedObject.Items
                                    where item.ItemStatus != ProductStatus.RET
                                    select item).FirstOrDefault();
                    iPawnLoanMYear = -1;
                    if (itemData != null && itemData.mDocNumber > 0)
                    {
                        iPawnLoanMYear = itemData.mYear;
                    }
                    else
                    {
                        MessageBox.Show("Cannot PFI Post since there are no items to process.");
                        if (InTransactionBlock() && !RollbackTransaction(section))
                        {
                            MessageBox.Show("(6)Cannot PFI Post due to transaction problems in " + section + " section.",
                                            "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            string errMsg = string.Format("(6)Unable to perform pfi posting in {0} section - rollback failed", section);
                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                           errMsg);
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                        return;

                    }
                    //Get the first item that is not in Police seize status
                    var itemPSData = (from item in _pfiLoan.UpdatedObject.Items
                                      where item.ItemStatus != ProductStatus.PS
                                      select item).FirstOrDefault();

                    if (itemPSData != null && itemPSData.mDocNumber > 0)
                    {
                        iPawnLoanMYear = itemPSData.mYear;
                    }
                    else
                    {
                        MessageBox.Show("Cannot PFI Post since there are no items which are not police sezied to process");
                        if (InTransactionBlock() && !RollbackTransaction(section))
                        {
                            MessageBox.Show("(7)Cannot PFI Post due to transaction problems in " + section + " section.",
                                            "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            string errMsg = string.Format("(7)Unable to perform pfi posting in {0} section - rollback failed", section);
                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                           errMsg);
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                        return;
                    }


                    string sErrorCode;
                    string sErrorText;
                    bool bOriginallyGun;

                    List<Tub_Param> lstParams = new List<Tub_Param>();
                    List<string> lstData = new List<string>();
                    List<string> lstICN = new List<string>();
                    int iParamPosition = 1;
                    int iParamSize = 1;

                    lstParams.Add(new Tub_Param
                    {
                        Name = "STORENUMBER",
                        Type = "VARCHAR2",
                        Size = iParamSize.ToString(),
                        Position = iParamPosition.ToString()
                    });
                    iParamPosition += iParamSize;
                    lstData.Add(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);

                    iParamSize = 1;
                    lstParams.Add(new Tub_Param
                    {
                        Name = "ALIAS",
                        Type = "VARCHAR2",
                        Size = iParamSize.ToString(),
                        Position = iParamPosition.ToString()
                    });
                    iParamPosition += iParamSize;
                    lstData.Add(GlobalDataAccessor.Instance.CurrentSiteId.Alias);

                    iParamSize = 1;
                    lstParams.Add(new Tub_Param
                    {
                        Name = "P_CREATEDBY",
                        Type = "VARCHAR2",
                        Size = iParamSize.ToString(),
                        Position = iParamPosition.ToString()
                    });
                    iParamPosition += iParamSize;
                    lstData.Add(GlobalDataAccessor.Instance.DesktopSession.UserName);

                    iParamSize = 1;
                    lstParams.Add(new Tub_Param
                    {
                        Name = "MRDATE",
                        Type = "VARCHAR2",
                        Size = iParamSize.ToString(),
                        Position = iParamPosition.ToString()
                    });
                    iParamPosition += iParamSize;
                    lstData.Add(ShopDateTime.Instance.ShopDate.ToShortDateString());

                    iParamSize = 1;
                    lstParams.Add(new Tub_Param
                    {
                        Name = "MRTIME",
                        Type = "VARCHAR2",
                        Size = iParamSize.ToString(),
                        Position = iParamPosition.ToString()
                    });
                    iParamPosition += iParamSize;
                    lstData.Add(ShopDateTime.Instance.ShopDate.ToShortDateString()
                        + " "
                        + ShopDateTime.Instance.ShopTime.ToString());

                    iParamSize = 1;
                    lstParams.Add(new Tub_Param
                    {
                        Name = "OPCODE",
                        Type = "VARCHAR2",
                        Size = iParamSize.ToString(),
                        Position = iParamPosition.ToString()
                    });
                    iParamPosition += iParamSize;
                    lstData.Add("EARNED");

                    iParamSize = 1;
                    lstParams.Add(new Tub_Param
                    {
                        Name = "MR_STORE",
                        Type = "VARCHAR2",
                        Size = iParamSize.ToString(),
                        Position = iParamPosition.ToString()
                    });
                    iParamPosition += iParamSize;
                    lstData.Add(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);

                    iParamSize = 1;
                    lstParams.Add(new Tub_Param
                    {
                        Name = "P_REF",
                        Type = "VARCHAR2",
                        Size = iParamSize.ToString(),
                        Position = iParamPosition.ToString()
                    });
                    iParamPosition += iParamSize;
                    lstData.Add(_pfiLoan.UpdatedObject.TicketNumber.ToString());

                    iParamSize = 1;
                    lstParams.Add(new Tub_Param
                    {
                        Name = "P_REF_TYPE",
                        Type = "VARCHAR2",
                        Size = iParamSize.ToString(),
                        Position = iParamPosition.ToString()
                    });
                    iParamPosition += iParamSize;
                    lstData.Add("1");

                    if (_pfiLoan.UpdatedObject.GetType() == typeof(PawnLoan))
                    {

                        iParamSize = 1;
                        lstParams.Add(new Tub_Param
                        {
                            Name = "P_INTEREST_CHARGE",
                            Type = "VARCHAR2",
                            Size = iParamSize.ToString(),
                            Position = iParamPosition.ToString()
                        });
                        iParamPosition += iParamSize;
                        lstData.Add(Utilities.GetStringValue(((PawnLoan)_pfiLoan.UpdatedObject).InterestAmount, "0"));

                    }
                    iParamSize = 1;
                    lstParams.Add(new Tub_Param
                    {
                        Name = "MR_STORE",
                        Type = "VARCHAR2",
                        Size = iParamSize.ToString(),
                        Position = iParamPosition.ToString()
                    });
                    iParamPosition += iParamSize;
                    lstData.Add(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);

                    List<int> lstTicketNumbers = new List<int>();
                    List<string> lstRefTypes = new List<string>();
                    lstTicketNumbers.Add(_pfiLoan.UpdatedObject.TicketNumber);
                    lstRefTypes.Add("1");



                    if (_pfiLoan.UpdatedObject.TempStatus != StateStatus.PFI)
                    {
                        MessageBox.Show("Loan # " + _pfiLoan.UpdatedObject.TicketNumber + " cannot be processed because the status changed",
                "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        _PostingErrors.Add(
                            new PairType<string, string>(
                                "INVALIDLOAN",
                                "Loan #" + _pfiLoan.UpdatedObject.TicketNumber
                                + " not processed.  Another process invalidated this loan for PFI" +
                                " | "
                                + _pfiLoan.UpdatedObject.TicketNumber));

                        // Since we were able to commit the Loan, Blow away the Loan Transition Data
                        StoreLoans.DeleteLoanTransition(
                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                            _pfiLoan.UpdatedObject.TicketNumber,
                            ProductType.PAWN,
                            out sErrorCode,
                            out sErrorText
                            );
                        if (InTransactionBlock() && !CommitTransaction(section))
                        {
                            MessageBox.Show("(8)Cannot PFI Post due to transaction problems in " + section + " section.",
                                            "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            string errMsg = string.Format("(8)Unable to perform pfi posting in {0} section - commit failed", section);
                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                           errMsg);
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                            if (!RollbackTransaction(section))
                            {
                                MessageBox.Show("(9)Cannot PFI Post due to transaction problems in " + section + " section.",
                                                "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                errMsg = string.Format("(9)Unable to perform pfi posting in {0} section - rollback failed", section);
                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting", errMsg);
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                return;
                            }
                        }
                        //Go to next pfi transition section
                        continue;
                    }

                    // Call Insert_Mdse_Archive()


                    if (itemData != null && itemData.mDocNumber > 0)
                    {
                        MerchandiseProcedures.InsertMerchandiseArchive(
                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                            itemData.mStore,
                            iPawnLoanMYear,
                            itemData.mDocNumber,
                            itemData.mDocType,
                            out sErrorCode,
                            out sErrorText
                            );
                        if (sErrorCode != "0")
                        {
                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                           sErrorText + " | " +
                                                                           _pfiLoan.UpdatedObject.TicketNumber));
                            MessageBox.Show("Cannot PFI Post due to failed stored procedure call. [Insert Merchandise Archive]", "PFI Posting Validation",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (InTransactionBlock() && !RollbackTransaction(section))
                            {
                                MessageBox.Show("(10)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + itemData.Icn,
                                                "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                string errMsg = string.Format("(10)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, itemData.Icn);
                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                               errMsg);
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                return;
                            }
                            return;
                        }
                    }
                    else
                    {
                        _PostingErrors.Add(new PairType<string, string>("NOK",
                                                                       "Pawn Items do not exist for Loan." + " | " +
                                                                       _pfiLoan.UpdatedObject.TicketNumber));
                        MessageBox.Show("Cannot PFI Post - no Pawn Items exist for Loan.", "PFI Posting Validation",
                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        if (InTransactionBlock() && !RollbackTransaction(section))
                        {
                            MessageBox.Show("(11)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + itemData.Icn,
                                            "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            string errMsg = string.Format("(11)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, itemData.Icn);
                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                           errMsg);
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                            return;
                        }
                        return;
                    }

                    // For every PawnItem
                    // Iteration Loop for each PawnItem
                    // Set Pawn Loan PawnItemsCommitted to true
                    bool bPawnItemsCommitted = true;
                    int iNumberOriginalPawnItems = (from iItems in _pfiLoan.OriginalObject.Items
                                                    where iItems.ItemReason != ItemReason.ADDD
                                                    select iItems).Count();

                    foreach (Item pawnItem in _pfiLoan.UpdatedObject.Items)
                    {
                        if (pawnItem.ItemStatus == ProductStatus.RET)
                        {
                            //insert into mdse revision
                            MerchandiseProcedures.InsertMerchandiseRevision(GlobalDataAccessor.Instance.DesktopSession,
                                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                iPawnLoanMYear,
                                pawnItem.mDocNumber,
                                pawnItem.mDocType,
                                pawnItem.mItemOrder,
                                0,
                                pawnItem.mStore,
                                pawnItem.mDocNumber.ToString().PadLeft(6, '0'),
                                pawnItem.mDocType,
                                "",
                                0,
                                "RETAIL",
                                "",
                                "0",
                                GlobalDataAccessor.Instance.DesktopSession.UserName,
                                out sErrorCode,
                                out sErrorText
                                );
                            if (sErrorCode != "0")
                            {
                                _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                               sErrorText + " | " + pawnItem.Icn));
                                //MessageBox.Show("Cannot post PFI - a pawn item " + pawnItem.Icn + " is in RET status", "PFI Posting Error");
                                if (InTransactionBlock() && !RollbackTransaction(section))
                                {
                                    MessageBox.Show("(12)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn,
                                                    "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    string errMsg = string.Format("(12)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                   errMsg);
                                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                    return;
                                }
                                break;
                            }
                        }
                        else
                        {
                            if (pawnItem.ItemStatus == ProductStatus.PS)
                            {
                                continue;
                            }

                            if (_pfiLoan.UpdatedObject.ProductType == ProductType.PAWN.ToString())
                            {
                                lstICN.Add(pawnItem.Icn);
                            }

                            bOriginallyGun = _pfiLoan.OriginalObject.Items.FindIndex(opi => opi.Icn == pawnItem.Icn) >= 0;
                            if (bOriginallyGun)
                            {
                                bOriginallyGun = _pfiLoan.OriginalObject.Items.Find(opi => opi.Icn == pawnItem.Icn).IsGun;
                            }

                            // If a new Gun Record Number was generated in initial loop, add it to the Pawn Item
                            if (!bOriginallyGun && pawnItem.IsGun || pawnItem.ItemReason == ItemReason.ADDD && pawnItem.IsGun)
                            {
                                pawnItem.GunNumber = gunRecords.Find(g => g.Left == pawnItem.Icn).Right;
                            }

                            if (pawnItem.ItemReason != ItemReason.ADDD
                                && pawnItem.mItemOrder <= iNumberOriginalPawnItems) // Not Added
                            {
                                // Call Insert_MdHist()
                                if (pawnItem.SelectedProKnowMatch.selectedPKData.LoanAmount > 0.0M)
                                {
                                    ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                        pawnItem.mStore,
                                        iPawnLoanMYear,
                                        pawnItem.mDocNumber,
                                        pawnItem.mDocType,
                                        pawnItem.mItemOrder,
                                        0,
                                        "PFI",
                                        "PK",
                                        pawnItem.SelectedProKnowMatch.selectedPKData.LoanAmount,
                                        pawnItem.SelectedProKnowMatch.selectedPKData.LoanAmount,
                                        0.0M,
                                        0.0M,
                                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                                        ShopDateTime.Instance.ShopDate,
                                        out sErrorCode,
                                        out sErrorText
                                        );
                                    if (sErrorCode != "0")
                                    {
                                       // MessageBox.Show("(13)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                       //                 "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("(13)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }
                                }
                                if (pawnItem.SelectedProKnowMatch.selectedPKData.RetailAmount > 0.0M)
                                {
                                    ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                        pawnItem.mStore,
                                        iPawnLoanMYear,
                                        pawnItem.mDocNumber,
                                        pawnItem.mDocType,
                                        pawnItem.mItemOrder,
                                        0,
                                        "PFI",
                                        "PKR",
                                        0.0M,
                                        0.0M,
                                        pawnItem.SelectedProKnowMatch.selectedPKData.RetailAmount,
                                        0.0M,
                                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                                        ShopDateTime.Instance.ShopDate,
                                        out sErrorCode,
                                        out sErrorText
                                        );
                                    if (sErrorCode != "0")
                                    {
                                       // MessageBox.Show("(14)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                       //                 "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("(14)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }
                                }
                                if (pawnItem.SelectedProKnowMatch.proCallData.NewRetail > 0.0M)
                                {
                                    ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                        pawnItem.mStore,
                                        iPawnLoanMYear,
                                        pawnItem.mDocNumber,
                                        pawnItem.mDocType,
                                        pawnItem.mItemOrder,
                                        0,
                                        "PFI",
                                        "PC",
                                        0.0M,
                                        0.0M,
                                        0.0M,
                                        pawnItem.SelectedProKnowMatch.proCallData.NewRetail,
                                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                                        ShopDateTime.Instance.ShopDate,
                                        out sErrorCode,
                                        out sErrorText
                                        );
                                    if (sErrorCode != "0")
                                    {
                                       // MessageBox.Show("(15)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                       //                 "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("(15)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }
                                }

                                // Call Delete_OtherDSC()
                                MerchandiseProcedures.DeleteOtherDesc(
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    pawnItem.mStore,
                                    iPawnLoanMYear,
                                    pawnItem.mDocNumber,
                                    pawnItem.mDocType,
                                    pawnItem.mItemOrder,
                                    out sErrorCode,
                                    out sErrorText
                                    );
                                if (sErrorCode != "0")
                                {
                                   // MessageBox.Show("(16)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                   //                 "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                   sErrorText + " | " + pawnItem.Icn));
                                    bPawnItemsCommitted = false;
                                    if (InTransactionBlock() && !RollbackTransaction(section))
                                    {
                                        string errMsg = string.Format("(16)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                       errMsg);
                                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                        return;
                                    }
                                    break;
                                }

                                // Call Update_Mdse()
                                int iOriginalLoanAmountIdx = _pfiLoan.OriginalObject.Items.FindIndex(ol => ol.Icn == pawnItem.Icn);
                                decimal dOriginalLoanAmount = 0;
                                if (iOriginalLoanAmountIdx >= 0)
                                {
                                    dOriginalLoanAmount = _pfiLoan.OriginalObject.Items[iOriginalLoanAmountIdx].ItemAmount;
                                }

                                MerchandiseProcedures.UpdateMerchandise(GlobalDataAccessor.Instance.DesktopSession,
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    pawnItem,
                                    _pfiLoan.UpdatedObject.OriginationDate,
                                    0,
                                    dOriginalLoanAmount,
                                    _pfiLoan.UpdatedObject.CustomerNumber,
                                    out sErrorCode,
                                    out sErrorText
                                    );
                                if (sErrorCode != "0")
                                {
                                  //  MessageBox.Show("(17)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                  //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                   sErrorText + " | " + pawnItem.Icn));
                                    bPawnItemsCommitted = false;
                                    if (InTransactionBlock() && !RollbackTransaction(section))
                                    {
                                        string errMsg = string.Format("(17)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                       errMsg);
                                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                        return;
                                    }
                                    break;
                                }
                                // Call Delete_Stones() for current mdse header
                                MerchandiseProcedures.DeleteStones(
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    pawnItem.mStore,
                                    iPawnLoanMYear,
                                    pawnItem.mDocNumber,
                                    pawnItem.mDocType,
                                    pawnItem.mItemOrder,
                                    out sErrorCode,
                                    out sErrorText
                                    );
                                if (sErrorCode != "0")
                                {
                                   // MessageBox.Show("(18)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                   //                 "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                   sErrorText + " | " + pawnItem.Icn));
                                    bPawnItemsCommitted = false;
                                    if (InTransactionBlock() && !RollbackTransaction(section))
                                    {
                                        string errMsg = string.Format("(18)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                       errMsg);
                                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                        return;
                                    }
                                    break;
                                }

                                #region Pawn Item Attributes
                                // Call Insert_OtherDsc_NewLoan() if ItemAttribute.Code == 999 or MaskSeq = 999
                                foreach (ItemAttribute itemAttribute in pawnItem.Attributes)
                                {
                                    if (itemAttribute.MaskOrder == 999 || itemAttribute.Answer.AnswerCode == 999)
                                    {
                                        ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                            pawnItem.mStore,
                                            iPawnLoanMYear,
                                            pawnItem.mDocNumber,
                                            pawnItem.mDocType,
                                            pawnItem.mItemOrder,
                                            0,
                                            itemAttribute.MaskOrder,
                                            itemAttribute.Answer.AnswerText,
                                            GlobalDataAccessor.Instance.DesktopSession.UserName,
                                            out sErrorCode,
                                            out sErrorText
                                            );
                                        if (sErrorCode != "0")
                                        {
                                          //  MessageBox.Show("(19)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                          //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                           sErrorText + " | " + pawnItem.Icn));
                                            bPawnItemsCommitted = false;
                                            if (InTransactionBlock() && !RollbackTransaction(section))
                                            {
                                                string errMsg = string.Format("(19)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                               errMsg);
                                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                return;
                                            }
                                            continue;
                                        }
                                    }
                                }

                                //BZ # 610
                                if (!string.IsNullOrEmpty(pawnItem.Comment) && pawnItem.Icn.Substring(16, 2) == "00")
                                {
                                    ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                        pawnItem.mStore, pawnItem.mYear, pawnItem.mDocNumber,
                                        pawnItem.mDocType, pawnItem.mItemOrder, 0, 999,
                                        pawnItem.Comment, GlobalDataAccessor.Instance.DesktopSession.UserName, out sErrorCode, out sErrorText);
                                    if (sErrorCode != "0")
                                    {
                                       // MessageBox.Show("(20)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                       //                 "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("(20)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }

                                }
                                //BZ # 610 end

                                //if (!bPawnItemsCommitted)
                                //    continue;
                                #endregion

                                // For every stones if a Jewelry Item
                                if (pawnItem.IsJewelry)
                                {
                                    if (pawnItem.TotalLoanGoldValue > 0.0M)
                                    {
                                        ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                            pawnItem.mStore,
                                            iPawnLoanMYear,
                                            pawnItem.mDocNumber,
                                            pawnItem.mDocType,
                                            pawnItem.mItemOrder,
                                            0,
                                            "PFI",
                                            "PMETL",
                                            0.0M,
                                            0.0M,
                                            0.0M,
                                            pawnItem.TotalLoanGoldValue,
                                            GlobalDataAccessor.Instance.DesktopSession.UserName,
                                            ShopDateTime.Instance.ShopDate,
                                            out sErrorCode,
                                            out sErrorText
                                            );
                                        if (sErrorCode != "0")
                                        {
                                           // MessageBox.Show("(21)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                            //                "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                           sErrorText + " | " + pawnItem.Icn));
                                            bPawnItemsCommitted = false;
                                            if (InTransactionBlock() && !RollbackTransaction(section))
                                            {
                                                string errMsg = string.Format("(21)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                               errMsg);
                                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                return;
                                            }
                                            break;

                                        }
                                    }
                                    if (pawnItem.TotalLoanStoneValue > 0.0M)
                                    {
                                        ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                            pawnItem.mStore,
                                            iPawnLoanMYear,
                                            pawnItem.mDocNumber,
                                            pawnItem.mDocType,
                                            pawnItem.mItemOrder,
                                            0,
                                            "PFI",
                                            "STONE",
                                            0.0M,
                                            0.0M,
                                            0.0M,
                                            pawnItem.TotalLoanStoneValue,
                                            GlobalDataAccessor.Instance.DesktopSession.UserName,
                                            ShopDateTime.Instance.ShopDate,
                                            out sErrorCode,
                                            out sErrorText
                                            );
                                        if (sErrorCode != "0")
                                        {
                                           // MessageBox.Show("(22)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                           //                 "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                           sErrorText + " | " + pawnItem.Icn));
                                            bPawnItemsCommitted = false;
                                            if (InTransactionBlock() && !RollbackTransaction(section))
                                            {
                                                string errMsg = string.Format("(22)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                               errMsg);
                                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                return;
                                            }
                                            break;
                                        }
                                    }

                                    foreach (JewelrySet jewelrySet in pawnItem.Jewelry)
                                    {
                                        int iOriginalJewelryAmountIdx = _pfiLoan.OriginalObject.Items.FindIndex(ol => ol.Icn == pawnItem.Icn);
                                        decimal dOriginalJewelryAmount = 0;
                                        if (iOriginalJewelryAmountIdx >= 0)
                                            dOriginalJewelryAmount = _pfiLoan.OriginalObject.Items[iOriginalJewelryAmountIdx].ItemAmount;

                                        // Call Insert_MDSE
                                        MerchandiseProcedures.InsertMerchandise(GlobalDataAccessor.Instance.DesktopSession,
                                            pawnItem,
                                            jewelrySet.SubItemNumber,
                                            _pfiLoan.UpdatedObject.OriginationDate,
                                            dOriginalJewelryAmount,
                                            _pfiLoan.UpdatedObject.CustomerNumber,
                                            out sErrorCode,
                                            out sErrorText
                                            );
                                        if (sErrorCode != "0")
                                        {
                                          //  MessageBox.Show("(23)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                          //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                           sErrorText + " | " +
                                                                                           jewelrySet.Icn));
                                            bPawnItemsCommitted = false;
                                            if (InTransactionBlock() && !RollbackTransaction(section))
                                            {
                                                string errMsg = string.Format("(23)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                               errMsg);
                                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                return;
                                            }
                                            continue;
                                        }
                                        // Insert_OtherDsc_NewLoan()
                                        foreach (ItemAttribute itemAttribute in jewelrySet.ItemAttributeList)
                                        {
                                            if (itemAttribute.MaskOrder == 999 || itemAttribute.Answer.AnswerCode == 999)
                                            {
                                                ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                    pawnItem.mStore,
                                                    iPawnLoanMYear,
                                                    pawnItem.mDocNumber,
                                                    pawnItem.mDocType,
                                                    pawnItem.mItemOrder,
                                                    jewelrySet.SubItemNumber,
                                                    itemAttribute.MaskOrder,
                                                    itemAttribute.Answer.AnswerText,
                                                    GlobalDataAccessor.Instance.DesktopSession.UserName,
                                                    out sErrorCode,
                                                    out sErrorText
                                                    );
                                                if (sErrorCode != "0")
                                                {
                                                  //  MessageBox.Show("(24)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                                  //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                    _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                                   sErrorText + " | " +
                                                                                                   jewelrySet.Icn));
                                                    bPawnItemsCommitted = false;
                                                    if (InTransactionBlock() && !RollbackTransaction(section))
                                                    {
                                                        string errMsg = string.Format("(24)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                                       errMsg);
                                                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                        return;
                                                    }
                                                    continue;
                                                }
                                            }
                                        }

                                    }
                                }


                                // Gun Logic Synopsis
                                if (bOriginallyGun && !pawnItem.IsGun)
                                {
                                    // Call Update_Gun_Book()
                                    ProcessTenderProcedures.ExecuteUpdateGunBookRecord(
                                        _pfiLoan.UpdatedObject.TicketNumber.ToString(),
                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                        "VO",
                                        _pfiLoan.UpdatedObject.CustomerNumber,
                                        "PFI Edit",
                                        "VOID due to",
                                        "",
                                        "",
                                        "",
                                        "",
                                        "",
                                        "",
                                        "",
                                        "",
                                        "C",
                                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                                        ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                        ShopDateTime.Instance.ShopTransactionTime,
                                        "",
                                        pawnItem.QuickInformation.Caliber,
                                        pawnItem.QuickInformation.Importer,
                                        pawnItem.QuickInformation.SerialNumber,
                                        pawnItem.QuickInformation.Model,
                                        pawnItem.QuickInformation.Manufacturer,
                                        pawnItem.mStore,
                                        iPawnLoanMYear,
                                        pawnItem.mDocNumber,
                                        pawnItem.mDocType,
                                        pawnItem.mItemOrder,
                                        0,
                                        pawnItem.GunNumber,
                                        out sErrorCode,
                                        out sErrorText
                                        );
                                    if (sErrorCode != "0")
                                    {
                                       // MessageBox.Show("(26)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                       //                 "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("(26)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }
                                }
                                else if (bOriginallyGun && pawnItem.IsGun)
                                {
                                    if (_pfiLoan.OriginalObject.Items.Find(opi => opi.Icn == pawnItem.Icn).TicketDescription == pawnItem.TicketDescription)
                                    {
                                        ProcessTenderProcedures.ExecuteUpdateGunBookRecord(
                                            _pfiLoan.UpdatedObject.TicketNumber.ToString(),
                                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                            //pawnItem.PawnStatus.ToString(),
                                            ProductStatus.PFI.ToString(),
                                            _pfiLoan.UpdatedObject.CustomerNumber,
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "P",
                                            GlobalDataAccessor.Instance.DesktopSession.UserName,
                                            ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                            ShopDateTime.Instance.ShopTransactionTime,
                                            "",
                                            pawnItem.QuickInformation.Caliber,
                                            pawnItem.QuickInformation.Importer,
                                            pawnItem.QuickInformation.SerialNumber,
                                            pawnItem.QuickInformation.Model,
                                            pawnItem.QuickInformation.Manufacturer,
                                            pawnItem.mStore,
                                            iPawnLoanMYear,
                                            pawnItem.mDocNumber,
                                            pawnItem.mDocType,
                                            pawnItem.mItemOrder,
                                            0,
                                            pawnItem.GunNumber,
                                            out sErrorCode,
                                            out sErrorText
                                            );
                                        if (sErrorCode != "0")
                                        {
                                          //  MessageBox.Show("(27)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                          //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                           sErrorText + " | " + pawnItem.Icn));
                                            bPawnItemsCommitted = false;
                                            if (InTransactionBlock() && !RollbackTransaction(section))
                                            {
                                                string errMsg = string.Format("(27)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                               errMsg);
                                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                return;
                                            }
                                            break;
                                        }
                                    }
                                    else if (_pfiLoan.OriginalObject.Items.Find(opi => opi.Icn == pawnItem.Icn).TicketDescription != pawnItem.TicketDescription)
                                    {
                                        ProcessTenderProcedures.ExecuteUpdateGunBookRecord(
                                            _pfiLoan.UpdatedObject.TicketNumber.ToString(),
                                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                            //pawnItem.PawnStatus.ToString(),
                                            ProductStatus.PFI.ToString(),
                                            _pfiLoan.UpdatedObject.CustomerNumber,
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "E",
                                            GlobalDataAccessor.Instance.DesktopSession.UserName,
                                            ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                            ShopDateTime.Instance.ShopTransactionTime,
                                            "",
                                            pawnItem.QuickInformation.Caliber,
                                            pawnItem.QuickInformation.Importer,
                                            pawnItem.QuickInformation.SerialNumber,
                                            pawnItem.QuickInformation.Model,
                                            pawnItem.QuickInformation.Manufacturer,
                                            pawnItem.mStore,
                                            iPawnLoanMYear,
                                            pawnItem.mDocNumber,
                                            pawnItem.mDocType,
                                            pawnItem.mItemOrder,
                                            0,
                                            pawnItem.GunNumber,
                                            out sErrorCode,
                                            out sErrorText
                                            );
                                        if (sErrorCode != "0")
                                        {
                                          //  MessageBox.Show("(28)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                          //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                           sErrorText + " | " + pawnItem.Icn));
                                            bPawnItemsCommitted = false;
                                            if (InTransactionBlock() && !RollbackTransaction(section))
                                            {
                                                string errMsg = string.Format("(28)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                               errMsg);
                                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                return;
                                            }
                                            break;
                                        }
                                        if (_pfiLoan.OriginalObject.Items.Find(opi => opi.Icn == pawnItem.Icn).CategoryCode != pawnItem.CategoryCode)
                                        {
                                            MerchandiseProcedures.UpdateGunType(pawnItem.QuickInformation.GunType,
                                                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                pawnItem.GunNumber.ToString(),
                                                GlobalDataAccessor.Instance.DesktopSession.UserName,
                                                out sErrorCode,
                                                out sErrorText);
                                        }
                                        if (sErrorCode != "0")
                                        {
                                          //  MessageBox.Show("(28.1)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                          //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                           sErrorText + " | " + pawnItem.Icn));
                                            bPawnItemsCommitted = false;
                                            if (InTransactionBlock() && !RollbackTransaction(section))
                                            {
                                                string errMsg = string.Format("(28)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                               errMsg);
                                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                return;
                                            }
                                            break;
                                        }

                                    }
                                }
                                else if (!bOriginallyGun && pawnItem.IsGun)
                                {
                                    string errorCode, errorText;
                                    string orgCustNo;
                                    string ignoreOut;
                                    sFirstName = string.Empty;
                                    sLastName = string.Empty;
                                    sMiddleInitial = string.Empty;
                                    sAddress = string.Empty;
                                    sCity = string.Empty;
                                    sState = string.Empty;
                                    sZipCode = string.Empty;
                                    sAgency = string.Empty;
                                    sIDType = string.Empty;
                                    sIDValue = string.Empty;
                                    
                                    bool haveCustomer =
                                    CustomerLoans.GetCustomerInfo(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                        ((PawnLoan)(_pfiLoan.UpdatedObject)).OrigTicketNumber,
                                        "1", out sFirstName, out sMiddleInitial, out sLastName, out orgCustNo,
                                        out sAddress, out sCity, out sState,
                                        out sZipCode, out sAgency, out sIDType,
                                        out sIDValue, out errorCode, out errorText);
                                    

 

                                    if (!haveCustomer)
                                    {
                                       // MessageBox.Show("(29)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + errorText,
                                      //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(errorCode,
                                                                                       errorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("(29)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;

                                    }
                                    if (string.IsNullOrEmpty(sAddress) || string.IsNullOrEmpty(sCity) || string.IsNullOrEmpty(sState) ||
                                        string.IsNullOrEmpty(sZipCode) || string.IsNullOrEmpty(sAgency) || string.IsNullOrEmpty(sIDType) ||
                                        string.IsNullOrEmpty(sIDValue) || sAddress == "null" || sCity == "null" || sState == "null" || sZipCode == "null" || sAgency == "null"
                                        || sIDType == "null" || sIDValue == "null")
                                    {
                                        bPawnItemsCommitted = false;
                                        _PostingErrors.Add(new PairType<string, string>("999",
                                                "Address and/or ID data is empty for customer " + sCustomerNumber + " Cannot update gun record for " + " | " +
                                                pawnItem.Icn));

                                      //  MessageBox.Show("Cannot PFI Post loan " + pawnItem.mDocNumber + " Address and/or ID data is empty for customer. Cannot update Gun record",
               // "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting", sErrorText);
                                        break;

                                    }


                                    // Call Insert_Gun_Book()
                                    ProcessTenderProcedures.ExecuteInsertGunBookRecord(
                                        GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                                        _pfiLoan.OriginalObject.OriginationDate,
                                        //ShopDateTime.Instance.ShopDate,
                                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                                        0L,
                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                        pawnItem.mStore,
                                        iPawnLoanMYear,
                                        pawnItem.mDocNumber,
                                        pawnItem.mDocType,
                                        pawnItem.mItemOrder,
                                        0,
                                        pawnItem.QuickInformation.Manufacturer,
                                        pawnItem.QuickInformation.Importer,
                                        pawnItem.QuickInformation.SerialNumber,
                                        pawnItem.QuickInformation.Caliber,
                                        pawnItem.QuickInformation.GunType,
                                        pawnItem.QuickInformation.Model,
                                        sLastName,
                                        sFirstName,
                                        sMiddleInitial,
                                        //_pfiLoan.UpdatedObject.CustomerNumber,
                                        (haveCustomer) ? orgCustNo : _pfiLoan.UpdatedObject.CustomerNumber,
                                        sAddress,
                                        sCity,
                                        sState,
                                        sZipCode,
                                        sIDType,
                                        sAgency,
                                        sIDValue,
                                        
                                        ShopDateTime.Instance.ShopDate,
                                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                                        pawnItem.HasGunLock ? 1 : 0,
                                        ProductStatus.PFI.ToString(),
                                        out sErrorCode,
                                        out sErrorText
                                        );
                                    if (sErrorCode != "0")
                                    {
                                       // MessageBox.Show("(29)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                       //                 "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("(29)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }
                                }
                                // If Item is CACC
                                if (pawnItem.CaccLevel == 0 && pawnItem.ItemReason != ItemReason.NOMD && pawnItem.HoldDesc != "Police Hold")
                                {
                                    // Call Update_Cacc_Info()
                                    MerchandiseProcedures.UpdateCaccInfo(GlobalDataAccessor.Instance.DesktopSession,
                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                        0,
                                        pawnItem.mDocNumber,
                                        "7",
                                        1,
                                        0,
                                        pawnItem.QuickInformation.Quantity < 1 ? 1 : pawnItem.QuickInformation.Quantity,
                                        pawnItem.ItemAmount,
                                        pawnItem.CategoryCode,
                                        ShopDateTime.Instance.ShopDate,
                                        out sErrorCode,
                                        out sErrorText
                                        );
                                    if (sErrorCode != "0")
                                    {
                                      //  MessageBox.Show("(30)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                      //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("(30)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }
                                }
                                if (pawnItem.IsChargedOff())
                                {
                                    chargeOffItems++;
                                    FileLogger.Instance.logMessage(LogLevel.INFO, "PFI_Posting",
                                                                   "Charge off item added " + pawnItem.Icn + " with reason " + pawnItem.ItemReason.ToString());

                                    string chargeoffReason;
                                    switch (pawnItem.ItemReason)
                                    {
                                        case ItemReason.COFFBRKN:
                                            chargeoffReason = "COFFBRKN";
                                            brknqty.Add(pawnItem.QuickInformation.Quantity);
                                            chargeOffBRKNIcn.Add(pawnItem.Icn);
                                            statusBRKNReason.Add(chargeoffReason);
                                            break;
                                        case ItemReason.COFFSTRU:
                                            chargeoffReason = "COFFSTRU";
                                            chargeOffSTRUIcn.Add(pawnItem.Icn);
                                            struqty.Add(pawnItem.QuickInformation.Quantity);
                                            statusSTRUReason.Add(chargeoffReason);
                                            break;
                                        case ItemReason.COFFNXT:
                                            chargeoffReason = "COFFNXT";
                                            chargeOffNXTIcn.Add(pawnItem.Icn);
                                            nxtqty.Add(pawnItem.QuickInformation.Quantity);
                                            statusNXTReason.Add(chargeoffReason);
                                            break;
                                        default:
                                            chargeoffReason = "COFFBRKN";
                                            chargeOffBRKNIcn.Add(pawnItem.Icn);
                                            brknqty.Add(pawnItem.QuickInformation.Quantity);
                                            statusBRKNReason.Add(chargeoffReason);
                                            break;
                                    }


                                }
                                // Call Insert_Rev() for Retail Value
                                MerchandiseProcedures.InsertMerchandiseRevision(GlobalDataAccessor.Instance.DesktopSession,
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    iPawnLoanMYear,
                                    pawnItem.mDocNumber,
                                    pawnItem.mDocType,
                                    pawnItem.mItemOrder,
                                    0,
                                    pawnItem.mStore,
                                    pawnItem.mDocNumber.ToString(),
                                    pawnItem.mDocType,
                                    "",
                                    _pfiLoan.OriginalObject.Items.Find(pi => pi.Icn == pawnItem.Icn).SelectedProKnowMatch.
                                        selectedPKData.RetailAmount,
                                    "RETAIL",
                                    "",
                                    pawnItem.RetailPrice.ToString(),
                                    GlobalDataAccessor.Instance.DesktopSession.UserName,
                                    out sErrorCode,
                                    out sErrorText
                                    );
                                if (sErrorCode != "0")
                                {
                                   // MessageBox.Show("(31)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                  //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                   sErrorText + " | " + pawnItem.Icn));
                                    bPawnItemsCommitted = false;
                                    if (InTransactionBlock() && !RollbackTransaction(section))
                                    {
                                        string errMsg = string.Format("(31)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                       errMsg);
                                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                        return;
                                    }
                                    break;
                                }
                                // Call Insert_Rev() for Added to Inventory
                                MerchandiseProcedures.InsertMerchandiseRevision(GlobalDataAccessor.Instance.DesktopSession,
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    iPawnLoanMYear,
                                    pawnItem.mDocNumber,
                                    pawnItem.mDocType,
                                    pawnItem.mItemOrder,
                                    0,
                                    pawnItem.mStore,
                                    pawnItem.mDocNumber.ToString(),
                                    pawnItem.mDocType,
                                    "",
                                    0,
                                    pawnItem.ItemReason == ItemReason.CACC ? "PFC" : "PFI",
                                    "",
                                    "",
                                    GlobalDataAccessor.Instance.DesktopSession.UserName,
                                    out sErrorCode,
                                    out sErrorText
                                    );
                                if (sErrorCode != "0")
                                {
                                 //   MessageBox.Show("(32)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                  //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                   sErrorText + " | " + pawnItem.Icn));
                                    bPawnItemsCommitted = false;
                                    if (InTransactionBlock() && !RollbackTransaction(section))
                                    {
                                        string errMsg = string.Format("(32)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                       errMsg);
                                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                        return;
                                    }
                                    break;
                                }
                            }
                            else // New Item Added during PFI
                            {
                                // Call Insert_Mdse()
                                MerchandiseProcedures.InsertMerchandise(GlobalDataAccessor.Instance.DesktopSession,
                                    pawnItem,
                                    0,
                                    _pfiLoan.UpdatedObject.OriginationDate,
                                    pawnItem.ItemAmount,
                                    _pfiLoan.UpdatedObject.CustomerNumber,
                                    out sErrorCode,
                                    out sErrorText
                                    );
                                if (sErrorCode != "0")
                                {
                                    _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                   sErrorText + " | " + pawnItem.Icn));
                                    bPawnItemsCommitted = false;
                                    if (InTransactionBlock() && !RollbackTransaction(section))
                                    {
                                        MessageBox.Show("(33)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn,
                                                        "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        string errMsg = string.Format("(33)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                       errMsg);
                                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                        return;
                                    }
                                    break;
                                }
                                // Call Insert_OtherDsc_NewLoan() if ItemAttribute.Code == 999 or MaskSeq = 999
                                foreach (ItemAttribute itemAttribute in pawnItem.Attributes)
                                {
                                    if (itemAttribute.MaskOrder == 999 || itemAttribute.Answer.AnswerCode == 999)
                                    {
                                        ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                            pawnItem.mStore,
                                            iPawnLoanMYear,
                                            pawnItem.mDocNumber,
                                            pawnItem.mDocType,
                                            pawnItem.mItemOrder,
                                            0,
                                            itemAttribute.MaskOrder,
                                            itemAttribute.Answer.AnswerText,
                                            GlobalDataAccessor.Instance.DesktopSession.UserName,
                                            out sErrorCode,
                                            out sErrorText
                                            );
                                        if (sErrorCode != "0")
                                        {
                                          //  MessageBox.Show("(34)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                           //                 "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                           sErrorText + " | " + pawnItem.Icn));
                                            bPawnItemsCommitted = false;
                                            if (InTransactionBlock() && !RollbackTransaction(section))
                                            {
                                                string errMsg = string.Format("(34)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                               errMsg);
                                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                return;
                                            }
                                            continue;
                                        }
                                    }
                                }

                                //BZ # 610
                                //SR 6/13/2011 Add only for sub item 0
                                if (!string.IsNullOrEmpty(pawnItem.Comment) && pawnItem.Icn.Substring(16, 2) == "00")
                                {
                                    ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                        pawnItem.mStore, pawnItem.mYear, pawnItem.mDocNumber,
                                        pawnItem.mDocType, pawnItem.mItemOrder, 0, 999,
                                        pawnItem.Comment, GlobalDataAccessor.Instance.DesktopSession.UserName, out sErrorCode, out sErrorText);
                                    if (sErrorCode != "0")
                                    {
                                      //  MessageBox.Show("(35)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                     //                   "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("(35)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }

                                }
                                //BZ # 610 end

                                // For every stones if a Jewelry Item
                                if (pawnItem.IsJewelry)
                                {
                                    if (pawnItem.TotalLoanGoldValue > 0.0M)
                                    {
                                        ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                            pawnItem.mStore,
                                            iPawnLoanMYear,
                                            pawnItem.mDocNumber,
                                            pawnItem.mDocType,
                                            pawnItem.mItemOrder,
                                            0,
                                            "PFI",
                                            "PMETL",
                                            0.0M,
                                            0.0M,
                                            0.0M,
                                            pawnItem.TotalLoanGoldValue,
                                            GlobalDataAccessor.Instance.DesktopSession.UserName,
                                            ShopDateTime.Instance.ShopDate,
                                            out sErrorCode,
                                            out sErrorText
                                            );
                                        if (sErrorCode != "0")
                                        {
                                          //  MessageBox.Show("(36)Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                          //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                           sErrorText + " | " + pawnItem.Icn));
                                            bPawnItemsCommitted = false;
                                            if (InTransactionBlock() && !RollbackTransaction(section))
                                            {
                                                string errMsg = string.Format("(36)Unable to perform pfi posting in {0} section - rollback failed for item {1}", section, pawnItem.Icn);
                                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                               errMsg);
                                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                return;
                                            }
                                            break;
                                        }
                                    }
                                    if (pawnItem.TotalLoanStoneValue > 0.0M)
                                    {
                                        ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                            pawnItem.mStore,
                                            iPawnLoanMYear,
                                            pawnItem.mDocNumber,
                                            pawnItem.mDocType,
                                            pawnItem.mItemOrder,
                                            0,
                                            "PFI",
                                            "STONE",
                                            0.0M,
                                            0.0M,
                                            0.0M,
                                            pawnItem.TotalLoanStoneValue,
                                            GlobalDataAccessor.Instance.DesktopSession.UserName,
                                            ShopDateTime.Instance.ShopDate,
                                            out sErrorCode,
                                            out sErrorText
                                            );
                                        if (sErrorCode != "0")
                                        {
                                            int num = 37;
                                           // MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                          //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                           sErrorText + " | " + pawnItem.Icn));
                                            bPawnItemsCommitted = false;

                                            if (InTransactionBlock() && !RollbackTransaction(section))
                                            {
                                                string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                               errMsg);
                                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                return;
                                            }
                                            break;
                                        }
                                    }

                                    foreach (JewelrySet jewelrySet in pawnItem.Jewelry)
                                    {
                                        // Call Insert_MDSE
                                        MerchandiseProcedures.InsertMerchandise(GlobalDataAccessor.Instance.DesktopSession,
                                            pawnItem,
                                            jewelrySet.SubItemNumber,
                                            _pfiLoan.UpdatedObject.OriginationDate,
                                            pawnItem.ItemAmount,
                                            _pfiLoan.UpdatedObject.CustomerNumber,
                                            out sErrorCode,
                                            out sErrorText
                                            );
                                        if (sErrorCode != "0")
                                        {
                                            int num = 38;
                                          //  MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                           //                 "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                           sErrorText + " | " +
                                                                                           jewelrySet.Icn));
                                            bPawnItemsCommitted = false;
                                            if (InTransactionBlock() && !RollbackTransaction(section))
                                            {
                                                string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                               errMsg);
                                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                return;
                                            }
                                            continue;
                                        }
                                        // Insert_OtherDsc_NewLoan()
                                        foreach (ItemAttribute itemAttribute in jewelrySet.ItemAttributeList)
                                        {
                                            if (itemAttribute.MaskOrder == 999 || itemAttribute.Answer.AnswerCode == 999)
                                            {
                                                ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                    pawnItem.mStore,
                                                    iPawnLoanMYear,
                                                    pawnItem.mDocNumber,
                                                    pawnItem.mDocType,
                                                    pawnItem.mItemOrder,
                                                    jewelrySet.SubItemNumber,
                                                    itemAttribute.MaskOrder,
                                                    itemAttribute.Answer.AnswerText,
                                                    GlobalDataAccessor.Instance.DesktopSession.UserName,
                                                    out sErrorCode,
                                                    out sErrorText
                                                    );
                                                if (sErrorCode != "0")
                                                {
                                                    int num = 39;
                                                 //   MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                                 //                   "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                    _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                                   sErrorText + " | " +
                                                                                                   jewelrySet.Icn));
                                                    bPawnItemsCommitted = false;
                                                    if (InTransactionBlock() && !RollbackTransaction(section))
                                                    {
                                                        string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                                       errMsg);
                                                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                                        return;
                                                    }
                                                    continue;
                                                }
                                            }
                                        }

                                    }
                                }
                                else if (pawnItem.IsGun)
                                {
                                    CustomerLoans.GetCustomerInfo(
                                        pawnItem.mStore.ToString(),
                                        _pfiLoan.UpdatedObject.TicketNumber,
                                        _pfiLoan.UpdatedObject.ProductType,
                                        out sFirstName,
                                        out sMiddleInitial,
                                        out sLastName,
                                        out sCustomerNumber,
                                        out sAddress,
                                        out sCity,
                                        out sState,
                                        out sZipCode,
                                        out sAgency,
                                        out sIDType,
                                        out sIDValue,
                                        out sErrorCode,
                                        out sErrorText
                                        );

                                    if (sErrorCode != "0")
                                    {
                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " +
                                                                                       pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        int num = 40;

                                        MessageBox.Show("Cannot PFI Post in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                                        "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting", sErrorText);


                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        return;
                                    }
                                    if (sAddress == "null" || sCity == "null" || sState == "null" || sZipCode == "null" || sAgency == "null"
                                        || sIDType == "null" || sIDValue == "null")
                                    {
                                        bPawnItemsCommitted = false;
                                        _PostingErrors.Add(new PairType<string, string>("999",
                                                "Address and/or ID data is empty for customer " + sCustomerNumber + "Cannot update gun record " + " | " +
                                                pawnItem.Icn));

                                     //   MessageBox.Show("Cannot PFI Post in " + pawnItem.mDocNumber + " Address and/or ID data is empty for customer. Cannot update Gun record",
              //  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting", sErrorText);
                                        break;

                                    }
                                    // Call Insert_Gun_Book()
                                    ProcessTenderProcedures.ExecuteInsertGunBookRecord(
                                        GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                                        ShopDateTime.Instance.ShopDate,
                                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                                        0L,
                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                        pawnItem.mStore,
                                        iPawnLoanMYear,
                                        pawnItem.mDocNumber,
                                        pawnItem.mDocType,
                                        pawnItem.mItemOrder,
                                        0,
                                        pawnItem.QuickInformation.Manufacturer,
                                        pawnItem.QuickInformation.Importer,
                                        pawnItem.QuickInformation.SerialNumber,
                                        pawnItem.QuickInformation.Caliber,
                                        pawnItem.QuickInformation.GunType,
                                        pawnItem.QuickInformation.Model,
                                        sLastName,
                                        sFirstName,
                                        sMiddleInitial,
                                        sCustomerNumber,
                                        sAddress,
                                        sCity,
                                        sState,
                                        sZipCode,
                                        sIDType,
                                        sAgency,
                                        sIDValue,
                                        ShopDateTime.Instance.ShopDate,
                                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                                        pawnItem.HasGunLock ? 1 : 0,
                                        ProductStatus.IP.ToString(),
                                        out sErrorCode,
                                        out sErrorText
                                        );
                                    if (sErrorCode != "0")
                                    {
                                        int num = 41;
                                       // MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                        //                "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }
                                }
                                // If Item is CACC
                                if (pawnItem.CaccLevel == 0 && pawnItem.ItemReason != ItemReason.NOMD && pawnItem.HoldDesc != "Police Hold")
                                {
                                    // Call Update_Cacc_Info()
                                    MerchandiseProcedures.UpdateCaccInfo(GlobalDataAccessor.Instance.DesktopSession,
                                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                        0,
                                        pawnItem.mDocNumber,
                                        "7",
                                        1,
                                        0,
                                        pawnItem.QuickInformation.Quantity < 1 ? 1 : pawnItem.QuickInformation.Quantity,
                                        pawnItem.ItemAmount,
                                        pawnItem.CategoryCode,
                                        ShopDateTime.Instance.ShopDate,
                                        out sErrorCode,
                                        out sErrorText
                                        );
                                    if (sErrorCode != "0")
                                    {
                                        int num = 42;
                                     //   MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                     //                   "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }
                                }
                                if (pawnItem.IsChargedOff())
                                {
                                    chargeOffItems++;
                                    FileLogger.Instance.logMessage(LogLevel.INFO, "PFI_Posting",
                                                                   "Charge off item added " + pawnItem.Icn + " with reason " + pawnItem.ItemReason.ToString());

                                    string chargeoffReason;
                                    switch (pawnItem.ItemReason)
                                    {
                                        case ItemReason.COFFBRKN:
                                            chargeoffReason = "COFFBRKN";
                                            brknqty.Add(pawnItem.QuickInformation.Quantity);
                                            chargeOffBRKNIcn.Add(pawnItem.Icn);
                                            statusBRKNReason.Add(chargeoffReason);
                                            break;
                                        case ItemReason.COFFSTRU:
                                            chargeoffReason = "COFFSTRU";
                                            chargeOffSTRUIcn.Add(pawnItem.Icn);
                                            struqty.Add(pawnItem.QuickInformation.Quantity);
                                            statusSTRUReason.Add(chargeoffReason);
                                            break;
                                        case ItemReason.COFFNXT:
                                            chargeoffReason = "COFFNXT";
                                            chargeOffNXTIcn.Add(pawnItem.Icn);
                                            nxtqty.Add(pawnItem.QuickInformation.Quantity);
                                            statusNXTReason.Add(chargeoffReason);
                                            break;
                                        default:
                                            chargeoffReason = "COFFBRKN";
                                            chargeOffBRKNIcn.Add(pawnItem.Icn);
                                            brknqty.Add(pawnItem.QuickInformation.Quantity);
                                            statusBRKNReason.Add(chargeoffReason);
                                            break;
                                    }
                                }

                                // Call Insert_MDHIST()
                                if (pawnItem.SelectedProKnowMatch.selectedPKData.LoanAmount > 0.0M)
                                {
                                    ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                        pawnItem.mStore,
                                        iPawnLoanMYear,
                                        pawnItem.mDocNumber,
                                        pawnItem.mDocType,
                                        pawnItem.mItemOrder,
                                        0,
                                        "PFI",
                                        "PK",
                                        pawnItem.SelectedProKnowMatch.selectedPKData.LoanAmount,
                                        pawnItem.SelectedProKnowMatch.selectedPKData.LoanAmount,
                                        0.0M,
                                        0.0M,
                                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                                        ShopDateTime.Instance.ShopDate,
                                        out sErrorCode,
                                        out sErrorText
                                        );
                                    if (sErrorCode != "0")
                                    {
                                        int num = 43;
                                      //  MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                      //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }
                                }
                                if (pawnItem.SelectedProKnowMatch.selectedPKData.RetailAmount > 0.0M)
                                {
                                    ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                        pawnItem.mStore,
                                        iPawnLoanMYear,
                                        pawnItem.mDocNumber,
                                        pawnItem.mDocType,
                                        pawnItem.mItemOrder,
                                        0,
                                        "PFI",
                                        "PKR",
                                        0.0M,
                                        0.0M,
                                        pawnItem.SelectedProKnowMatch.selectedPKData.RetailAmount,
                                        0.0M,
                                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                                        ShopDateTime.Instance.ShopDate,
                                        out sErrorCode,
                                        out sErrorText
                                        );
                                    if (sErrorCode != "0")
                                    {
                                        int num = 44;
                                      //  MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                      //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }
                                }
                                if (pawnItem.SelectedProKnowMatch.proCallData.NewRetail > 0.0M)
                                {
                                    ProcessTenderProcedures.ExecuteInsertMDHistRecord(
                                        pawnItem.mStore,
                                        iPawnLoanMYear,
                                        pawnItem.mDocNumber,
                                        pawnItem.mDocType,
                                        pawnItem.mItemOrder,
                                        0,
                                        "PFI",
                                        "PC",
                                        0.0M,
                                        0.0M,
                                        0.0M,
                                        pawnItem.SelectedProKnowMatch.proCallData.NewRetail,
                                        GlobalDataAccessor.Instance.DesktopSession.UserName,
                                        ShopDateTime.Instance.ShopDate,
                                        out sErrorCode,
                                        out sErrorText
                                        );
                                    if (sErrorCode != "0")
                                    {
                                        int num = 45;
                                      //  MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                     //                   "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                       sErrorText + " | " + pawnItem.Icn));
                                        bPawnItemsCommitted = false;
                                        if (InTransactionBlock() && !RollbackTransaction(section))
                                        {
                                            string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                           errMsg);
                                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                            return;
                                        }
                                        break;
                                    }
                                }
                                // Call Insert_Rev()
                                MerchandiseProcedures.InsertMerchandiseRevision(GlobalDataAccessor.Instance.DesktopSession,
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    iPawnLoanMYear,
                                    pawnItem.mDocNumber,
                                    pawnItem.mDocType,
                                    pawnItem.mItemOrder,
                                    0,
                                    pawnItem.mStore,
                                    pawnItem.mDocNumber.ToString().PadLeft(6, '0'),
                                    pawnItem.mDocType,
                                    "",
                                    pawnItem.SelectedProKnowMatch.selectedPKData.RetailAmount,
                                    "RETAIL",
                                    "",
                                    pawnItem.RetailPrice.ToString(),
                                    GlobalDataAccessor.Instance.DesktopSession.UserName,
                                    out sErrorCode,
                                    out sErrorText
                                    );
                                if (sErrorCode != "0")
                                {
                                    int num = 46;
                                 //   MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                 //                   "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                   sErrorText + " | " + pawnItem.Icn));
                                    bPawnItemsCommitted = false;
                                    if (InTransactionBlock() && !RollbackTransaction(section))
                                    {
                                        string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                       errMsg);
                                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                        return;
                                    }
                                    break;
                                }
                                // Call Insert_Rev() for Added to Inventory
                                MerchandiseProcedures.InsertMerchandiseRevision(GlobalDataAccessor.Instance.DesktopSession,
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    iPawnLoanMYear,
                                    pawnItem.mDocNumber,
                                    pawnItem.mDocType,
                                    pawnItem.mItemOrder,
                                    0,
                                    pawnItem.mStore,
                                    pawnItem.mDocNumber.ToString().PadLeft(6, '0'),
                                    pawnItem.mDocType,
                                    "",
                                    0,
                                    pawnItem.ItemReason == ItemReason.CACC ? "PFC" : "PFI",
                                    "",
                                    "",
                                    GlobalDataAccessor.Instance.DesktopSession.UserName,
                                    out sErrorCode,
                                    out sErrorText
                                    );
                                if (sErrorCode != "0")
                                {
                                    int num = 47;
                                 //   MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                  //                  "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                   sErrorText + " | " + pawnItem.Icn));
                                    bPawnItemsCommitted = false;
                                    if (InTransactionBlock() && !RollbackTransaction(section))
                                    {
                                        string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                       errMsg);
                                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                        return;
                                    }
                                    break;
                                }

                            }
                            if (sErrorCode != "0")
                            {
                                int num = 48;
                                MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                                "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                _PostingErrors.Add(
                                    new PairType<string, string>(
                                        sErrorCode,
                                        sErrorText + " | " + pawnItem.Icn));
                                bPawnItemsCommitted = false;
                                if (InTransactionBlock() && !RollbackTransaction(section))
                                {
                                    string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                   errMsg);
                                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                    return;
                                }
                                break;
                            }
                            //If the item is a purchase item then one more entry needs to be inserted
                            //in mdse revision table
                            if (pawnItem.ItemStatus == ProductStatus.PUR)
                            {
                                MerchandiseProcedures.InsertMerchandiseRevision(GlobalDataAccessor.Instance.DesktopSession,
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    iPawnLoanMYear,
                                    pawnItem.mDocNumber,
                                    pawnItem.mDocType,
                                    pawnItem.mItemOrder,
                                    0,
                                    pawnItem.mStore,
                                    pawnItem.mDocNumber.ToString().PadLeft(6, '0'),
                                    pawnItem.mDocType,
                                    "PURCHASE PRICE",
                                    pawnItem.ItemAmount,
                                    "PURCH",
                                    "",
                                    "",
                                    GlobalDataAccessor.Instance.DesktopSession.UserName,
                                    out sErrorCode,
                                    out sErrorText
                                    );
                                if (sErrorCode != "0")
                                {
                                    int num = 49;
                               //     MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                                //                    "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                                   sErrorText + " | " + pawnItem.Icn));
                                    bPawnItemsCommitted = false;
                                    if (InTransactionBlock() && !RollbackTransaction(section))
                                    {
                                        string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, pawnItem.Icn);
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                       errMsg);
                                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                        return;
                                    }
                                    break;
                                }
                            }


                            //PFI posting a success for the item. Add it to a transfer VO object
                            //Do not add items that are merged or a return item or police seized item
                            if (pawnItem.ItemReason != ItemReason.MERGED)
                            {
                                TransferItemVO transferData = new TransferItemVO();
                                transferData.ICN = pawnItem.Icn.ToString();
                                transferData.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                                transferData.ICNQty = pawnItem.QuickInformation.Quantity > 0
                                                              ? pawnItem.QuickInformation.Quantity.ToString()
                                                              : "1";
                                transferData.CustomerNumber = _pfiLoan.UpdatedObject.CustomerNumber;
                                transferData.TransactionDate = ShopDateTime.Instance.ShopDate;
                                transferData.MdseRecordDate = ShopDateTime.Instance.ShopDate;
                                transferData.MdseRecordTime = ShopDateTime.Instance.ShopTransactionTime;
                                transferData.MdseRecordUser = GlobalDataAccessor.Instance.DesktopSession.UserName;
                                transferData.MdseRecordDesc = string.Empty;
                                transferData.MdseRecordChange = 0;
                                transferData.MdseRecordType = string.Empty;
                                transferData.ClassCode = string.Empty;
                                transferData.AcctNumber = string.Empty;
                                transferData.CreatedBy = GlobalDataAccessor.Instance.DesktopSession.UserName;
                                transferData.GunNumber = pawnItem.GunNumber > 0 ? pawnItem.GunNumber.ToString() : null;
                                transferData.GunType = pawnItem.QuickInformation.GunType;
                                transferData.ItemDescription = pawnItem.TicketDescription;
                                transferData.ItemCost = pawnItem.ItemAmount;
                                _mdseToTransfer.Add(transferData);
                            }
                        }
                        
                    }
                    iParamSize = lstICN.Count;
                    lstParams.Add(new Tub_Param
                    {
                        Name = "ICNARRAY",
                        Type = "VARCHAR2",
                        Size = iParamSize.ToString(),
                        Position = iParamPosition.ToString()
                    });
                    iParamPosition += iParamSize;
                    lstData.AddRange(lstICN);

                    if (_pfiLoan.UpdatedObject.GetType() == typeof(PawnLoan))
                    {
                        bool retValue = MerchandiseProcedures.TubCalc(lstParams, lstData, out sErrorCode, out sErrorText);
                        if (!retValue || sErrorCode != "0")
                        {
                            int num = 50;
                            //    MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + pawnItem.Icn + " " + sErrorText,
                            //                    "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                            sErrorText + " | " +
                                                                            _pfiLoan.UpdatedObject.TicketNumber));
                            bPawnItemsCommitted = false;
                            if (InTransactionBlock() && !RollbackTransaction(section))
                            {
                                string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for loan {2}", num, section, _pfiLoan.UpdatedObject.TicketNumber);
                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                               errMsg);
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                return;
                            }
                            break;
                        }
                    }

                    if (bPawnItemsCommitted)
                    {

                        CompletedItems.Add(_pfiLoan.UpdatedObject.TicketNumber.ToString());
                        // Update Pawn or purchase Header 
                        MerchandiseProcedures.UpdateProductHeader(GlobalDataAccessor.Instance.DesktopSession,
                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                            _pfiLoan.UpdatedObject.TicketNumber,
                            StateStatus.PFI.ToString(),
                            "",
                            ShopDateTime.Instance.ShopDateCurTime,
                            null,
                            _pfiLoan.UpdatedObject.Items.Count + 1,
                            _pfiLoan.UpdatedObject.ProductType.ToString(),
                            out sErrorCode,
                            out sErrorText
                            );


                        if (sErrorCode != "0")
                        {
                            int num = 51;
                         //   MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + itemData.Icn + " " + sErrorText,
                         //                   "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                            sErrorText + " | " +
                                                                            _pfiLoan.UpdatedObject.TicketNumber));
                            bPawnItemsCommitted = false;
                            if (InTransactionBlock() && !RollbackTransaction(section))
                            {
                                string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, itemData.Icn);
                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                errMsg);
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                return;
                            }
                            return;
                        }

                        ReceiptDetailsVO receiptDetailsVO = new ReceiptDetailsVO();
                        receiptDetailsVO.ReceiptDate = ShopDateTime.Instance.ShopDate;
                        if (_pfiLoan.UpdatedObject.ProductType.ToString() == ProductType.PAWN.ToString())
                        {
                            receiptDetailsVO.RefTypes = new List<string>() { "1" };
                            receiptDetailsVO.RefStores = new List<string>() { ((PawnLoan)_pfiLoan.UpdatedObject).OrgShopNumber };
                        }
                        else
                        {
                            receiptDetailsVO.RefTypes = new List<string>() { "2" };
                            receiptDetailsVO.RefStores = new List<string>() { ((PurchaseVO)_pfiLoan.UpdatedObject).StoreNumber };

                        }
                        receiptDetailsVO.RefDates = new List<string>() { ShopDateTime.Instance.ShopDate.FormatDate() };
                        receiptDetailsVO.RefTimes = new List<string>() { ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString() };
                        receiptDetailsVO.UserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                        receiptDetailsVO.RefNumbers = new List<string>() { _pfiLoan.UpdatedObject.TicketNumber.ToString() };

                        receiptDetailsVO.RefEvents = new List<string>() { "PFI" };
                        receiptDetailsVO.RefAmounts = new List<string>() { _pfiLoan.UpdatedObject.Amount.ToString() };


                        ProcessTenderProcedures.ExecuteInsertReceiptDetails(
                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                            GlobalDataAccessor.Instance.DesktopSession.UserName,
                            ShopDateTime.Instance.ShopDate.ToShortDateString(),
                            GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                            ref receiptDetailsVO,
                            out sErrorCode,
                            out sErrorText);

                        if (sErrorCode != "0")
                        {
                            int num = 52;
                            MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Item = " + itemData.Icn + " " + sErrorText,
                                            "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            _PostingErrors.Add(new PairType<string, string>(sErrorCode,
                                                                            sErrorText + " | " +
                                                                            _pfiLoan.UpdatedObject.TicketNumber));
                            bPawnItemsCommitted = false;
                            if (InTransactionBlock() && !RollbackTransaction(section))
                            {
                                string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for item {2}", num, section, itemData.Icn);
                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                errMsg);
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                return;
                            }
                            return;
                        }


                        // temp during testing.

                        if (CommitTransaction(section))
                        {
                            section = "PFI Posting - After - Loan Transition Cleanup";
                            if (BeginTransaction(section))
                            {
                                // Since we were able to commit the Loan, delete the transition data
                                StoreLoans.DeleteLoanTransition(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                                _pfiLoan.UpdatedObject.TicketNumber, ProductType.ALL, out sErrorCode,
                                                                out sErrorText);

                                // TL 02-09-2010 Put in receipt detail for TO event
                                ReceiptDetailsVO transferReceiptDetailsVO = new ReceiptDetailsVO();
                                transferReceiptDetailsVO.ReceiptDate = ShopDateTime.Instance.ShopDate;
                                transferReceiptDetailsVO.RefDates = new List<string>()
                                    {
                                            ShopDateTime.Instance.ShopDate.ToShortDateString()
                                    };
                                transferReceiptDetailsVO.RefTimes = new List<string>()
                                    {
                                            ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                                            ShopDateTime.Instance.ShopTime.ToString()
                                    };
                                transferReceiptDetailsVO.UserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                                transferReceiptDetailsVO.RefNumbers = new List<string>()
                                    {
                                            _pfiLoan.UpdatedObject.TicketNumber.ToString()
                                    };
                                if (_pfiLoan.UpdatedObject.ProductType.ToString() == ProductType.PAWN.ToString())
                                {
                                    transferReceiptDetailsVO.RefTypes = new List<string>()
                                        {
                                                "1"
                                        };
                                    transferReceiptDetailsVO.RefStores = new List<string>()
                                        {
                                                _pfiLoan.UpdatedObject.OrgShopNumber
                                        };
                                }
                                else
                                {
                                    transferReceiptDetailsVO.RefTypes = new List<string>()
                                        {
                                                "2"
                                        };
                                    transferReceiptDetailsVO.RefStores = new List<string>()
                                        {
                                                ((PurchaseVO)_pfiLoan.UpdatedObject).StoreNumber
                                        };

                                }

                                transferReceiptDetailsVO.RefEvents = new List<string>()
                                    {
                                            "TO"
                                    };

                                transferReceiptDetailsVO.RefAmounts = new List<string>()
                                    {
                                            _pfiLoan.UpdatedObject.Amount.ToString()
                                    };


                                this.transferReceipts.Add(transferReceiptDetailsVO);
                            }
                            else
                            {
                                int num = 53;
                                MessageBox.Show("(" + num + ")Unable to start transaction block to remove loan transition data");
                                string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for PFI Post of loan {2}", num, section, itemData.Icn);
                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                errMsg);
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                return;
                            }
                            if (InTransactionBlock() && !CommitTransaction(section))
                            {
                                int num = 54;
                                MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Loan = " + itemData.Icn,
                                                "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for PFI Post of loan {2}", num, section, itemData.Icn);
                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                errMsg);
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                return;
                            }
                        }
                        else
                        {
                            if (InTransactionBlock() && !RollbackTransaction(section))
                            {
                                int num = 55;
                                MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section. Loan = " + itemData.Icn,
                                                "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                string errMsg = string.Format("({0})Unable to perform pfi posting in {1} section - rollback failed for PFI Post of loan {2}", num, section, itemData.Icn);
                                FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                                                errMsg);
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                return;
                            }
                            return;
                        }







                    }

                }
                //Process items that are set for chargeoff
                if (chargeOffItems > 0)
                {

                    if (chargeOffBRKNIcn.Count > 0)
                    {
                        jCase = new List<string>();
                        retailPrice = new List<string>();
                        foreach (string s in chargeOffBRKNIcn)
                        {
                            jCase.Add("");
                            retailPrice.Add("0");
                        }
                        CompleteChargeoff(chargeOffBRKNIcn, brknqty, statusBRKNReason, retailPrice, jCase);
                    }
                    if (chargeOffNXTIcn.Count > 0)
                    {
                        jCase = new List<string>();
                        retailPrice = new List<string>();
                        foreach (string t in this.chargeOffNXTIcn)
                        {
                            this.jCase.Add("");
                            this.retailPrice.Add("0");
                        }
                        CompleteChargeoff(chargeOffNXTIcn, nxtqty, statusNXTReason, retailPrice, jCase);

                    }
                    if (chargeOffSTRUIcn.Count > 0)
                    {
                        jCase = new List<string>();
                        retailPrice = new List<string>();
                        foreach (string s in chargeOffSTRUIcn)
                        {
                            jCase.Add("");
                            retailPrice.Add("0");
                        }
                        CompleteChargeoff(chargeOffSTRUIcn, struqty, statusSTRUReason, retailPrice, jCase);

                    }


                }


                // Popup a MessageBox if any errors occured during PFI Posting.
                if (_PostingErrors.Count > 0)
                {
                    string sMsg = "Errors during posting: " + Environment.NewLine;
                    foreach (PairType<string, string> pairType in _PostingErrors)
                    {
                        sMsg += pairType.Right + "[" + pairType.Left + "]" + Environment.NewLine;
                    }
                    MessageBox.Show(sMsg, "PFI Posting Errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                DataTable completedTable = _GridTable.Clone();
                foreach(DataRow dr in _GridTable.Rows)
                {
                    foreach(string data in CompletedItems)
                    {
                        if (Utilities.GetStringValue(dr["colNumber"]) == data)
                        {
                            DataRow dr1 = completedTable.NewRow();
                            dr1["colRefurb"] = dr["colRefurb"];
                            dr1["colAssignmentType"] = dr["colAssignmentType"];
                            dr1["colNumber"] =dr["colNumber"];

                            dr1["colDescription"] = dr["colDescription"];


                            dr1["colCost"] = dr["colCost"];


                                dr1["colRetail"] = dr["colRetail"];


                                dr1["colReason"] = dr["colReason"];

                               
                            completedTable.Rows.Add(dr1);
                        }
                    }
                }
                    PrintPFIUtilities printPFIUtilities = new PrintPFIUtilities(completedTable, this, "pfipost", "PFI Posting List");
                    printPFIUtilities.Print(totalCostLabel.Text, totalTicketsLabel.Text, false);


                    // Charge Off Report

                    var _lstChargeOffData = getChargeOffData();

                    if (_lstChargeOffData.Count() > 0)
                    {
                        int count = 0;
                        decimal total = 0;

                        DataTable ChargeOffData = PFI_ChargeOffList.setupDataTable(_lstChargeOffData, ref count, ref total);

                        PrintPFIUtilities chargeOffReport = new PrintPFIUtilities(ChargeOffData, this, "pfichargeoff",
                                                                                    "PFI Charge Off List");
                        chargeOffReport.Print(String.Format( "{0:c}", total), count.ToString(), false);
                    }


                    // Refurb Report
                    List<PFI_TransitionData> _lstRefurbData = _lstTransitionData
                                           .FindAll(td => td.pfiLoan.UpdatedObject
                                           .Items.FindIndex(pi => pi.RefurbNumber > 0) >= 0);

                    if (_lstRefurbData.Count > 0)
                    {
                        int count = 0;
                        decimal total = 0;

                        DataTable refurbData = PFI_RefurbList.setupDataTable(_lstRefurbData, ref count, ref total);

                        PrintPFIUtilities chargeOffReport = new PrintPFIUtilities(refurbData, this, "pfichargeoff",
                                                                                    "PFI Refurb List");
                        chargeOffReport.Print(String.Format("{0:c}", total), count.ToString(), false);                       
                    }

                    string msg = "PFI Posting completed successfully.";
                    if (_PostingErrors.Count > 0)
                        msg = "The remaining loans PFI posted successfully";
                    MessageBox.Show(msg, "PFI Posting", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                

                //Call to initiate transfer
                int transferNumber = 0;
                try
                {
                    string errorMessage = string.Empty;
                    if (_mdseToTransfer.Count > 0 && GlobalDataAccessor.Instance.CurrentSiteId.IsTopsExist)
                    {

                        bool retValue = TransferProcedures.TransferItemsOutOfStore(_mdseToTransfer, out transferNumber, "", out errorMessage, false, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
                        if (!retValue)
                        {
                            this.transferReceipts.Clear(); // TL 02-09-2010 clear out transfer receipts
                            MessageBox.Show("Error:" + errorMessage);
                        }
                        else
                        {
                            foreach (TransferItemVO transfer in _mdseToTransfer)
                            {
                                transfer.TransferNumber = transferNumber;
                            }

                            // TL 02-09-2010 Insert receipts for transfers
                            var errorCode = String.Empty;
                            var errorText = String.Empty;
                            var hasErrors = false;

                            try
                            {
                                section = "PFI Post - Insert Transfer Receipts";
                                if (BeginTransaction(section))
                                {
                                    foreach (ReceiptDetailsVO tr in this.transferReceipts)
                                    {
                                        ReceiptDetailsVO transferReceipt = tr;

                                        ProcessTenderProcedures.ExecuteInsertReceiptDetails(
                                                                                            GlobalDataAccessor.Instance.DesktopSession.
                                                                                                    CurrentSiteId.StoreNumber,
                                                                                            GlobalDataAccessor.Instance.DesktopSession.UserName,
                                                                                            ShopDateTime.Instance.ShopDate.
                                                                                                    ToShortDateString(),
                                                                                            GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                                                            ref transferReceipt, out errorCode,
                                                                                            out errorText);

                                        if (errorCode != "0")
                                        {
                                            hasErrors = true;
                                        }

  
                                    }
                                }
                            }
                            catch (Exception exc)
                            {
                                if (InTransactionBlock() && !RollbackTransaction(section))
                                {
                                    int num = 55;
                                    MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section.",
                                                    "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    string errMsg =
                                            string.Format(
                                                            "({0})Unable to perform pfi posting in {1} section - rollback failed for PFI insert transfer receipts: Exception {2} {3}",
                                                            num, section, exc, exc.StackTrace ?? "No Stack Trace");
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting", errMsg);
                                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, exc));
                                    return;
                                }
                                return;
                            }

                            if (!hasErrors)
                            {
                                if (InTransactionBlock() && !CommitTransaction(section))
                                {
                                    int num = 56;
                                    MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section.",
                                                    "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    string errMsg =
                                            string.Format(
                                                            "({0})Unable to perform pfi posting in {1} section - commit failed for PFI insert transfer receipts",
                                                            num, section);
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting", errMsg);
                                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                    if (!RollbackTransaction(section))
                                    {
                                        num = 57;
                                        MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section.",
                                                        "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        errMsg =
                                                string.Format(
                                                                "({0})Unable to perform pfi posting in {1} section - rollback failed for PFI insert transfer receipts",
                                                                num, section);
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting", errMsg);
                                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                        return;
                                    }
                                    return;
                                }
                            }
                            else
                            {
                                if (InTransactionBlock() && !RollbackTransaction(section))
                                {
                                    int num = 58;
                                    MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section.",
                                                    "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    string errMsg =
                                            string.Format(
                                                            "({0})Unable to perform pfi posting in {1} section - rollback failed",
                                                            num, section);
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting", errMsg);
                                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                    return;
                                }
                                return;
                            }

                            //Print the transfer report
                            string logPath =
                            SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
                            TransferOutReport trnsfrRpt = new TransferOutReport(_mdseToTransfer, ShopDateTime.Instance.ShopDateCurTime, GlobalDataAccessor.Instance.CurrentSiteId.StoreName,
                            Convert.ToString(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber), GlobalDataAccessor.Instance.DesktopSession.UserName, Convert.ToString(transferNumber), logPath, "PFIPosting", new ReportObject.TransferReport(), PdfLauncher.Instance);
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

                            /*TransferOutReport trnsfrRpt = new TransferOutReport();
                            trnsfrRpt.MdseTransfer = _mdseToTransfer;
                            trnsfrRpt.ShowDialog();*/
                        }
                    }
                }
                catch (Exception exc)
                {
                    if (InTransactionBlock() && !RollbackTransaction(section))
                    {
                        int num = 59;
                        MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in " + section + " section.",
                                        "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        string errMsg =
                                string.Format(
                                                "({0})Unable to perform pfi posting in {1} section - rollback failed for PFI insert transfer: Exception {2} {3}",
                                                num, section, exc, exc.StackTrace ?? "No Stack Trace");
                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting", errMsg);
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, exc));
                    }
                }
                this.Close();
            }
            catch (Exception exc)
            {
                if (InTransactionBlock() && !RollbackTransaction("Entire PFI Post"))
                {
                    int num = 60;
                    MessageBox.Show("(" + num + ")Cannot PFI Post due to transaction problems in Entire PFI Post section.",
                                    "PFI Posting General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    string errMsg =
                            string.Format(
                                            "({0})Unable to perform pfi posting in Entire PFI Post section - rollback failed: Exception {1} {2}",
                                            num, exc, exc.StackTrace ?? "No Stack Trace");
                    FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting", errMsg);
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, exc));
                }
            }
            this.Close();
        }

        private void CompleteChargeoff(List<string> chargeOffIcn,List<int> qnty, List<string> statusReason, List<string> chargeoffRetailPrice,List<string> jewelCase)
        {
            string errorCode;
            string errorText;
            int saleTicketNumber;
            GlobalDataAccessor.Instance.beginTransactionBlock();
            bool retValue = MerchandiseProcedures.InsertInventoryChargeOff(GlobalDataAccessor.Instance.OracleDA,
                                                                           GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                                           ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                                                           ShopDateTime.Instance.ShopDate.ToShortDateString() +
                                                                           " " + ShopDateTime.Instance.ShopTime.ToString(), "",
                                                                           GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                                           chargeOffIcn, qnty, statusReason, chargeoffRetailPrice, "", "",
                                                                           "", "0",
                                                                           GlobalDataAccessor.Instance.DesktopSession.CashDrawerName,
                                                                           "SALE", "0", "0", jewelCase,
                                                                           GlobalDataAccessor.Instance.DesktopSession.FullUserName, "", "",
                                                                           "", "", "", "", "", "", "", out saleTicketNumber,
                                                                           out errorCode, out errorText);
            if (!retValue)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error trying to complete chargeoff for " + statusReason[0] + " Error is " + errorText);
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                MessageBox.Show("Error completing charge off for " + statusReason[0]);
            }
            else
            {
                //MessageBox.Show("Charge off completed successfully");
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
            }
        }

        private void RemoveMdseFromTransferList(int pawnTicketNumber)
        {
            _mdseToTransfer.RemoveAll(pl => pl.ICN.Substring(7, 6) == pawnTicketNumber.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool RollbackTransaction(string section)
        {
            //Initialize out flag
            bool endTransBlock = false;
            bool finished = false;

            while (!finished)
            {
                try
                {
                    endTransBlock = GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    finished = true;
                }
                catch (Exception eX)
                {
                    var errMsg = string.Format("Cannot rollback transaction block in PFI_Posting in {0} section. Exception thrown: {1} {2}",
                                               section ?? "General", eX, eX.StackTrace ?? "No Stack Trace");
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting", errMsg);
                    }

                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                    endTransBlock = false;
                    finished = true;
                }
            }
            return (endTransBlock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        private bool BeginTransaction(string section)
        {
            //Initialize out flag
            bool startTransBlock = false;
            //Start transaction block
            bool finished = false;

            while (!finished)
            {
                try
                {
                    startTransBlock = GlobalDataAccessor.Instance.beginTransactionBlock();
                    finished = true;
                }
                catch (Exception eX)
                {
                    var errMsg = string.Format("Cannot start transaction block in PFI_Posting in {0} section", section ?? "General");

                    DialogResult dR = MessageBox.Show(errMsg + ". Please retry or cancel", "PFI Posting", MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error);
                    if (dR == DialogResult.Cancel)
                    {
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                "User chose to cancel PFI posting transaction start in {0} section. No loan data has been committed. Initial exception cause: {0} {1}",
                                    section ?? "General", eX, eX.StackTrace ?? "No Stack Trace");
                        }

                        BasicExceptionHandler.Instance.AddException(errMsg, eX);
                        startTransBlock = false;
                        finished = true;
                    }
                    else
                    {
                        finished = false;
                    }
                }
            }
            return (startTransBlock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        private bool CommitTransaction(string section)
        {
            bool endTransBlock = false;
            bool finished = false;
            while (!finished)
            {
                try
                {
                    endTransBlock = GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    finished = true;
                }

                catch (Exception eX)
                {
                    var errMsg = string.Format("Cannot commit transaction block in PFI_Posting in {0} section", section ?? "General");

                    DialogResult dR = MessageBox.Show(errMsg + ". Please retry or cancel", "PFI Posting", MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error);
                    if (dR == DialogResult.Cancel)
                    {
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, "PFI_Posting",
                                "User chose to cancel PFI posting transaction commit operation in {0} section. No loan data has been committed. Initial exception cause: {0} {1}",
                                    section ?? "General", eX, eX.StackTrace ?? "No Stack Trace");
                        }

                        BasicExceptionHandler.Instance.AddException(errMsg, eX);
                        finished = true;
                        endTransBlock = false;
                    }
                    else
                    {
                        finished = false;
                    }
                }
            }
            return (endTransBlock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool InTransactionBlock()
        {
            return (GlobalDataAccessor.Instance.DesktopSession.InTransactionBlock());
        }

        #region Printing

        private void printButton_Click(object sender, EventArgs e)
        {
            PrintPFIUtilities printPFIUtilities = new PrintPFIUtilities(_GridTable, this, "pfipost", "PFI Posting List");
            printPFIUtilities.Print(totalCostLabel.Text, totalTicketsLabel.Text);
        }

        #endregion

    }
}
