using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;
using Pawn.Forms.Pawn.Services.Void;

namespace Pawn.Logic.DesktopProcedures
{
    class VoidProcedures
    {
        public enum VoidCode
        {
            VOID_NEWLOAN = 0,
            VOID_PAYDOWN = 1,
            VOID_RENEWAL = 2,
            VOID_PICKUP = 3,
            VOID_EXTEND = 4,
            VOID_PURCHASE = 5,
            VOID_PURCHASERETURN = 6,
            VOID_BANKTRANSFER = 7,
            VOID_SHOPTOSHOPTRANSFER = 8,
            VOID_VENDORBUY = 9,
            VOID_PFI = 10,
            VOID_PARTPAYMENT = 11
        }

        public static readonly string[] VoidCodeValues =
        {
            "VNL", "VPD", "VRN", "VPU", "VEX","VPR","VRET","VBT","VST","VVB","VPARTP"
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="voidCode"></param>
        /// <returns></returns>
        public static string GetVoidCodeValue(VoidCode voidCode)
        {
            return (VoidCodeValues[(int)voidCode]);
        }

        public static bool PerformVoid(VoidLoanForm.LoanVoidDetails lvd, out string errCode, out string errText)
        {
            errCode = string.Empty;
            errText = string.Empty;
            if (lvd == null || 
                string.IsNullOrEmpty(lvd.OpCode) || 
                string.IsNullOrEmpty(lvd.OpRef) ||
                string.IsNullOrEmpty(lvd.TickNum) ||
                string.IsNullOrEmpty(lvd.StoreNum))
            {
                errCode = "1";
                errText = "Invalid LoanVoidDetails object or invalid object fields";
                return (false);
            }

            Int64 opRefVal;
            if (!Int64.TryParse(lvd.OpRef, out opRefVal))
            {
                errCode = "2";
                errText = "Invalid LoanVoidDetails operation reference";
                return (false);
            }

            Int64 tickNum;
            if (!Int64.TryParse(lvd.TickNum, out tickNum))
            {
                errCode = "3";
                errText = "Invalid LoanVoidDetails ticket number";
                return (false);
            }

            //Prepare common data
            Int64[] opRef =
            {
                opRefVal
            };

            Int64[] opOrder =
            {
                1L
            };
            bool transactStarted = false;
            DateTime statusTime = ShopDateTime.Instance.ShopDate;
            DateTime statusDate = statusTime.Date;
            Int64 ticketNumber = tickNum;
            string storeNumber = lvd.StoreNum;
            string[] opAmt =
            {
                lvd.Amount.ToString("F")
            };
            Int64[] recIds =
            {
                lvd.RecId
            };
            string createdBy = GlobalDataAccessor.Instance.DesktopSession.FullUserName;
            string[] voidOp = null;

            //Determine void code 
            if (lvd.OpCode.Equals("New", StringComparison.OrdinalIgnoreCase))
            {
                //New loan void
                voidOp = new[]
                {
                    GetVoidCodeValue(VoidCode.VOID_NEWLOAN)
                };
            }
            else if (lvd.OpCode.Equals("Paydown", StringComparison.OrdinalIgnoreCase))
            {
                //Paydown void
                voidOp = new[]
                {
                    GetVoidCodeValue(VoidCode.VOID_PAYDOWN)
                };
            }
            else if (lvd.OpCode.Equals("Renew", StringComparison.OrdinalIgnoreCase))
            {
                //Renewal void
                voidOp = new[]
                {
                    GetVoidCodeValue(VoidCode.VOID_RENEWAL)
                };
            }
            else if (lvd.OpCode.Equals("Pickup", StringComparison.OrdinalIgnoreCase))
            {
                //Pickup void
                voidOp = new[]
                {
                    GetVoidCodeValue(VoidCode.VOID_PICKUP)
                };
            }
            else if (lvd.OpCode.Equals("Extend", StringComparison.OrdinalIgnoreCase))
            {
                //Extend void
                voidOp = new[]
                {
                    GetVoidCodeValue(VoidCode.VOID_EXTEND)
                };
            }
            else if (lvd.OpCode.Equals("Purchase", StringComparison.OrdinalIgnoreCase))
            {
                //Purchase void
                voidOp = new[]
                {
                    GetVoidCodeValue(VoidCode.VOID_PURCHASE)
                };
            }
            else if (lvd.OpCode.Equals("Return", StringComparison.OrdinalIgnoreCase))
            {
                //Purchase Return void
                voidOp = new[]
                {
                    GetVoidCodeValue(VoidCode.VOID_PURCHASERETURN)
                };
            }
            else if (lvd.OpCode.Equals("PFI", StringComparison.OrdinalIgnoreCase))
            {
                bool retVal = false;
                try
                {
                    int receiptNumber;
                    decimal dStatusCode = 0;
                    CashlinxDesktopSession.Instance.beginTransactionBlock();
                    transactStarted = true;
                    retVal= VoidPFI(Utilities.GetIntegerValue(lvd.TickNum),
                        CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber,
                        "",
                        "",
                        "1",
                        Utilities.GetIntegerValue(lvd.RecId),
                       ShopDateTime.Instance.ShopDate.ToShortDateString(),
                        ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                        createdBy,
                       out receiptNumber,
                       out errCode,
                       out errText);
                }
                catch (Exception eX)
                {
                    errCode = errCode ?? "4";
                    errText = errText ?? "Exception thrown when voiding PFI: " + eX;
                    if (transactStarted)
                    {
                        CashlinxDesktopSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                        transactStarted = false;
                    }
                    return false;
                }
                if (!retVal)
                {
                    CashlinxDesktopSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                    transactStarted = false;                    
                    return false;
                }
                CashlinxDesktopSession.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                transactStarted = false;
                return true;
            }
            else if (lvd.OpCode.Equals("PARTP", StringComparison.OrdinalIgnoreCase))
            {
                bool retVal = false;
                int receiptNumber=0;
                try
                {
                    
                    decimal dStatusCode = 0;
                    CashlinxDesktopSession.Instance.beginTransactionBlock();
                    transactStarted = true;
                    retVal = VoidPartialPayment(CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber,
                        "",
                        "",
                        Utilities.GetIntegerValue(lvd.RecId),
                       ShopDateTime.Instance.ShopDate.ToShortDateString(),
                        ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                        createdBy,
                       out receiptNumber,
                       out errCode,
                       out errText);
                }
                catch (Exception eX)
                {
                    errCode = errCode ?? "4";
                    errText = errText ?? "Exception thrown when voiding Partial Payment: " + eX;
                    if (transactStarted)
                    {
                        CashlinxDesktopSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                        transactStarted = false;
                    }
                    return false;
                }
                if (!retVal)
                {
                    CashlinxDesktopSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                    transactStarted = false;
                    return false;
                }
                CashlinxDesktopSession.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                transactStarted = false;
                PawnLoan pVO = null;
                PawnAppVO pawnAppVO;
                CustomerVO custVO = null;
                string errorCode;
                string errorMsg;
                CustomerLoans.GetPawnLoan(GlobalDataAccessor.Instance.DesktopSession, Utilities.GetIntegerValue(storeNumber), Utilities.GetIntegerValue(ticketNumber), "0",
                                          StateStatus.BLNK, false, out pVO, out pawnAppVO, out custVO, out errorCode, out errorMsg);
                if (pVO != null)
                {
                    string origCustomer = pVO.CustomerNumber;
                    string puCustNumber = pVO.PuCustNumber;
                    if (origCustomer == puCustNumber || puCustNumber == string.Empty)
                        custVO = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, origCustomer);
                    else
                        custVO = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, puCustNumber);

                    if (custVO != null)
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = custVO;
                }

                ReceiptDetailsVO rDVO = new ReceiptDetailsVO();
                rDVO.RefDates.Add(ShopDateTime.Instance.ShopDate.ToShortDateString());

                //add ref time
                rDVO.RefTimes.Add(ShopDateTime.Instance.ShopShortTime.ToString());

                // ref number for new pawn loan is the ticket number
                rDVO.RefNumbers.Add("" + ticketNumber);

                rDVO.RefTypes.Add("1");

                // ref event will indicate the event we are trying to void

                rDVO.RefEvents.Add(lvd.OpCd.ToString());

                // ref amount for pawn loan is the amount we are trying to void
                rDVO.RefAmounts.Add(lvd.Amount.ToString());

                // ref store for pawn loan is the store the receipt was printed at
                rDVO.RefStores.Add(storeNumber);

                //Add the receipt number
                rDVO.ReceiptNumber = receiptNumber.ToString();

      
                ProcessTenderController pCntrl = ProcessTenderController.Instance;
                pCntrl.executeVoidLoanPrintReceipt(pVO, rDVO);
                return true;

            }


            if (voidOp != null)
            {
                string receiptNumber;
                //Get the pawnloan object
                PawnLoan pVO = null;
                PawnAppVO pawnAppVO;
                CustomerVO custVO = null;
                string errorCode;
                string errorMsg;
                PurchaseVO purchaseObject = new PurchaseVO();
                try
                {
                    if (lvd.OpCode.Equals("Purchase", StringComparison.OrdinalIgnoreCase) ||
                        lvd.OpCode.Equals("Return", StringComparison.OrdinalIgnoreCase))
                    {
                        //purchase data and customer data already retrieved
                        purchaseObject.TicketNumber = (int)tickNum;
                        purchaseObject.Amount = lvd.Amount;
                        purchaseObject.LoanStatus = ProductStatus.PUR;
                        purchaseObject.StoreNumber = lvd.StoreNum;
                    }
                    else
                    {
                        CustomerLoans.GetPawnLoan(GlobalDataAccessor.Instance.DesktopSession, Utilities.GetIntegerValue(storeNumber), Utilities.GetIntegerValue(ticketNumber), "0",
                                                  StateStatus.BLNK, false, out pVO, out pawnAppVO, out custVO, out errorCode, out errorMsg);
                        if (pVO != null)
                        {
                            string origCustomer = pVO.CustomerNumber;
                            string puCustNumber = pVO.PuCustNumber;
                            if (origCustomer == puCustNumber || puCustNumber == string.Empty)
                                custVO = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, origCustomer);
                            else
                                custVO = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, puCustNumber);

                            if (custVO != null)
                                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = custVO;
                        }
                    }
                    bool callSuccess = false;
                    receiptNumber = "";
                    string obj = string.Empty;
                    if (GlobalDataAccessor.Instance.beginTransactionBlock())
                    {
                        transactStarted = true;
                    }

                    if (!transactStarted)
                    {
                        //No transaction started, must exit
                        errText = "Could not start transaction block in Voidprocedures";
                        errCode = "99999";
                        return (false);
                    }

                    callSuccess = ExecuteVoidLoanChain(ticketNumber, storeNumber, voidOp, opRef, opAmt, opOrder, recIds, statusDate,
                                                       statusTime, createdBy, createdBy, lvd.VoidReason, lvd.VoidComment, out receiptNumber,
                                                       out errCode, out errText);

                    if ((errCode == "0" || errText == "Success") && callSuccess)
                    {
                        //Update teller tables for the void transaction
                        //bool retValue = true;
                        bool retValue = TellerProcedures.UpdateTellerOnVoid(lvd.RecId, storeNumber,
                                                                            GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                                            GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                                                                            receiptNumber,
                                                                            ShopDateTime.Instance.ShopDate.ToShortDateString(), out errCode,
                                                                            out errText);
                        if (retValue)
                        {
                            GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                            transactStarted = false;

                            //Create the receipt details VO
                            ReceiptDetailsVO rDVO = new ReceiptDetailsVO();
                            rDVO.RefDates.Add(ShopDateTime.Instance.ShopDate.ToShortDateString());

                            //add ref time
                            rDVO.RefTimes.Add(ShopDateTime.Instance.ShopShortTime.ToString());

                            // ref number for new pawn loan is the ticket number
                            rDVO.RefNumbers.Add("" + ticketNumber);

                            // ref type for  pawn loan is "PAWN" which is "1" in the DB
                            //and ref type for purchase is 2
                            if (lvd.OpCode.Equals("Purchase", StringComparison.OrdinalIgnoreCase) ||
                                lvd.OpCode.Equals("Return", StringComparison.OrdinalIgnoreCase))
                                rDVO.RefTypes.Add("2");
                            else
                                rDVO.RefTypes.Add("1");

                            // ref event will indicate the event we are trying to void

                            rDVO.RefEvents.Add(lvd.OpCd.ToString());

                            // ref amount for pawn loan is the amount we are trying to void
                            rDVO.RefAmounts.Add(lvd.Amount.ToString());

                            // ref store for pawn loan is the store the receipt was printed at
                            rDVO.RefStores.Add(storeNumber);

                            //Add the receipt number
                            rDVO.ReceiptNumber = receiptNumber;

                            //print receipt - DO NOT SPAWN process tender controller object,
                            //use the instance variable!!!!
                            ProcessTenderController pCntrl = ProcessTenderController.Instance;
                            if (lvd.OpCode.Equals("Purchase", StringComparison.OrdinalIgnoreCase) ||
                                lvd.OpCode.Equals("Return", StringComparison.OrdinalIgnoreCase))
                            {
                                pCntrl.executeVoidPurchasePrintReceipt(GlobalDataAccessor.Instance.DesktopSession.ActivePurchase, rDVO);
                            }
                            else
                            {
                                if (pVO != null)
                                {
                                    pCntrl.executeVoidLoanPrintReceipt(pVO, rDVO);
                                }
                            }

                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = null;
                            return (true);
                        }
                    }
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, "VoidProcedures", "Failed to update teller on void transaction" + errText);
                    }
                }
                catch(Exception eX)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, "VoidProcedures", "Exception thrown when attempting to void: {0} {1}",
                                                       eX, eX.StackTrace);
                    }

                }
                if (transactStarted)
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                }
                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = null;
                return (false);
            }
            errCode = "Invalid Void Op";
            errText = "Invalid Void Op";
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="ticketNumber"></param>
        /// <param name="loanChain"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteGetLoanChain(
            string storeNumber,
            Int64 ticketNumber,
            out DataTable loanChain,
            out string errorCode,
            out string errorText)
        {
            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;

            loanChain = new DataTable();

            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_ticket_number", ticketNumber));
            string tranDate = ShopDateTime.Instance.ShopDate.ToShortDateString();
            inParams.Add(new OracleProcParam("p_current_date", tranDate));

            // Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetLoanChain Failed",
                                                            new ApplicationException("ExecuteGetLoanChain Failed: Data accessor instance is invalid"));
                return (false);
            }

            DataSet outputDataSet;
            bool retVal;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            var refCursArr = new List<PairType<string, string>>
            {
                new PairType<string, string>("o_loan_chain_cursor", "loan_chain")
            };

            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_voids",
                    "get_loan_chain", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetLoanChain Failed", oEx);
                errorCode = " --- ExecuteGetLoanChainFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetLoanChain Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteGetLoanChainFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }
            if (outputDataSet == null || !outputDataSet.IsInitialized ||
                (outputDataSet.Tables == null || outputDataSet.Tables.Count <= 0))
            {
                return (false);
            }

            loanChain = outputDataSet.Tables["loan_chain"];
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <param name="storeNumber"></param>
        /// <param name="voidOp"></param>
        /// <param name="opRef"></param>
        /// <param name="opAmt"></param>
        /// <param name="opOrder"></param>
        /// <param name="recIds"></param>
        /// <param name="statusDate"></param>
        /// <param name="statusTime"></param>
        /// <param name="createdBy"></param>
        /// <param name="updatedBy"></param>
        /// <param name="receiptNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        public static bool ExecuteVoidLoanChain(
            Int64 ticketNumber,
            string storeNumber,
            string[] voidOp,
            Int64[] opRef,
            string[] opAmt,
            Int64[] opOrder,
            Int64[] recIds,
            DateTime statusDate,
            DateTime statusTime,
            string createdBy,
            string updatedBy,
            //out DataTable voidStatuses,
            string VoidReason,
            string VoidComment,
            out string receiptNumber,
            out string errorCode,
            out string errorText)
        {
            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;
            receiptNumber = string.Empty;

            var inParams = new List<OracleProcParam>();
            //voidStatuses = new DataTable();

            // Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteVoidLoanChain Failed",
                                                            new ApplicationException("ExecuteVoidLoanChain Failed: Data accessor instance is invalid"));
                return (false);
            }

            inParams.Add(new OracleProcParam("p_ticket_number", ticketNumber));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            OracleProcParam voidOpParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_void_op", voidOp.Length);
            for (int i = 0; i < voidOp.Length; ++i)
            {
                voidOpParam.AddValue(voidOp[i]);
            }
            inParams.Add(voidOpParam);
            OracleProcParam opRefParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTINT, "p_op_ref", opRef.Length);
            for (int i = 0; i < opRef.Length; ++i)
            {
                opRefParam.AddValue(opRef[i]);
            }
            inParams.Add(opRefParam);
            OracleProcParam opAmtParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_op_amt", opOrder.Length);
            for (int i = 0; i < opAmt.Length; ++i)
            {
                opAmtParam.AddValue(opAmt[i]);
            }
            inParams.Add(opAmtParam);
            OracleProcParam opOrderParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTINT, "p_op_order", opOrder.Length);
            for (int i = 0; i < opOrder.Length; ++i)
            {
                opOrderParam.AddValue(opOrder[i]);
            }
            inParams.Add(opOrderParam);
            //p_receiptdetail_number
            OracleProcParam opRecParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTINT, "p_receiptdetail_number", recIds.Length);
            for (int i = 0; i < recIds.Length; ++i)
            {
                opRecParam.AddValue(recIds[i]);
            }
            inParams.Add(opRecParam);
            inParams.Add(new OracleProcParam("p_status_date", ShopDateTime.Instance.ShopDate.ToShortDateString()));
            //inParams.Add(new OracleProcParam("p_status_time", statusTime.FormatDateAsTimestampWithTimeZone()));
            inParams.Add(new OracleProcParam("p_status_time",
                                             ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                                             ShopDateTime.Instance.ShopTime.ToString()));
            inParams.Add(new OracleProcParam("p_created_by", createdBy));
            inParams.Add(new OracleProcParam("p_updated_by", updatedBy));

            //BZ # 512
            inParams.Add(new OracleProcParam("p_void_reason", VoidReason));
            inParams.Add(new OracleProcParam("p_void_comment", VoidComment));
            //BZ # 512 - end

            //Add output param
            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value,
                                             ParameterDirection.Output, 1));


            DataSet outputDataSet;
            bool retVal;

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            //var refCursArr = new List<PairType<string, string>>
            //{
            //    new PairType<string, string>("o_void_return_cursor", "void_statuses")
            //};

            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_voids",
                    "void_loan_chain", inParams, null,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteVoidLoanChain Failed", oEx);
                errorCode = " --- ExecuteVoidLoanChainFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteVoidLoanChain Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteVoidLoanChainFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }
            //Get output number
            DataTable outputDt = outputDataSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object recNumObj = dr.ItemArray.GetValue(1);
                    if (recNumObj.ToString() != "null")
                    {
                        receiptNumber = (string)recNumObj;
                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }
            return (false);
        }

        /// <summary>
        /// voiding a bank transfer
        /// </summary>
        /// <param name="oDa"></param>
        /// <param name="bankTransferId"></param>
        /// <param name="storeNumber"></param>
        /// <param name="transferType"></param>
        /// <param name="voidReason"></param>
        /// <param name="voidComment"></param>
        /// <param name="userId"></param>
        /// <param name="statusDate"></param>
        /// <param name="transferAmount"></param>
        /// <param name="cashDrawerId"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool VoidBankTransfer(
            OracleDataAccessor oDa,
            string bankTransferId,
            string storeNumber,
            string transferType,
            string voidReason,
            string voidComment,
            string userId,
            string statusDate,
            string transferAmount,
            string cashDrawerId,
            out string errorCode,
            out string errorText)
        {
            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                oDa == null)
            {
                errorCode = "voidbanktransferfailed";
                errorText = "Invalid desktop session or data accessor";

                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_bank_transfer_id", bankTransferId));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_transfer_type", transferType));

            inParams.Add(new OracleProcParam("p_void_reason", voidReason));

            inParams.Add(new OracleProcParam("p_void_comment", voidComment));

            inParams.Add(new OracleProcParam("p_user_id", userId));

            inParams.Add(new OracleProcParam("p_status_date", statusDate));

            inParams.Add(new OracleProcParam("p_transfer_amount", transferAmount));

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashDrawerId));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                bool retVal;
                try
                {
                    DataSet outputDataSet;
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        "void_bank_transfer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling void_bank_transfer stored procedure", oEx);
                    errorCode = "VoidBanktransfer";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "VoidbankcashtransferFailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorText = "";
            return true;
        }

        /// <summary>
        /// Procedure to void a store to store cash transfer
        /// </summary>
        /// <param name="oDa"></param>
        /// <param name="storeTransferId"></param>
        /// <param name="storeNumber"></param>
        /// <param name="voidReason"></param>
        /// <param name="voidComment"></param>
        /// <param name="userId"></param>
        /// <param name="statusDate"></param>
        /// <param name="transferAmount"></param>
        /// <param name="cashDrawerId"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool VoidStoreCashTransfer(
            OracleDataAccessor oDa,
            string storeTransferId,
            string storeNumber,
            string voidReason,
            string voidComment,
            string userId,
            string statusDate,
            string transferAmount,
            string cashDrawerId,
            out string errorCode,
            out string errorText)
        {
            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                oDa == null)
            {
                errorCode = "voidstorecashtransferfailed";
                errorText = "Invalid desktop session or data accessor";

                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_transfer_id", storeTransferId));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_void_reason", voidReason));

            inParams.Add(new OracleProcParam("p_void_comment", voidComment));

            inParams.Add(new OracleProcParam("p_user_id", userId));

            inParams.Add(new OracleProcParam("p_status_date", statusDate));

            inParams.Add(new OracleProcParam("p_transfer_amount", transferAmount));

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashDrawerId));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                bool retVal;
                try
                {
                    DataSet outputDataSet;
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        "void_store_to_store_transfer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling void_store_to_store_transfer stored procedure", oEx);
                    errorCode = "VoidStorecashtransfer";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "VoidstorecashtransferFailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorText = "";
            return true;
        }

        /// <summary>
        /// Voiding all types of Catco merchandise transfer
        /// </summary>
        /// <param name="oDa"></param>
        /// <param name="transferTicketNumber"></param>
        /// <param name="storeNumber"></param>
        /// <param name="voidReason"></param>
        /// <param name="voidComment"></param>
        /// <param name="userId"></param>
        /// <param name="statusDate"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool VoidCatcoTransfer(
            OracleDataAccessor oDa,
            string transferTicketNumber,
            string storeNumber,
            string voidReason,
            string voidComment,
            string userId,
            string statusDate,
            string procedureName,
            out string errorCode,
            out string errorText)
        {
            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                oDa == null)
            {
                errorCode = procedureName + "failed";
                errorText = "Invalid desktop session or data accessor";

                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_transfer_ticket_number", transferTicketNumber));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_void_reason", voidReason));

            inParams.Add(new OracleProcParam("p_void_comment", voidComment));

            inParams.Add(new OracleProcParam("p_user_id", userId));

            inParams.Add(new OracleProcParam("p_status_date", statusDate));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                bool retVal;
                try
                {
                    DataSet outputDataSet;
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        procedureName, inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling " + procedureName + "stored procedure", oEx);
                    errorCode = procedureName;
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = procedureName + "Failed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorText = "";
            return true;
        }

        /// <summary>
        /// Voiding all types of Shop and Gun Room merchandise transfer
        /// </summary>
        /// <param name="oDa"></param>
        /// <param name="transferTicketNumber"></param>
        /// <param name="storeNumber"></param>
        /// <param name="voidReason"></param>
        /// <param name="voidComment"></param>
        /// <param name="userId"></param>
        /// <param name="statusDate"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool VoidShopAndGunTransfer(
            OracleDataAccessor oDa,
            string transferTicketNumber,
            string storeNumber,
            string statusDate,
            string statusTime,
            string userId,
            string voidReason,
            string voidComment,
            out string errorCode,
            out string errorText)
        {
            string procedureName = "void_transfer";//"void_to";
            var receiptNo = string.Empty;

            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                oDa == null)
            {
                errorCode = procedureName + "failed";
                errorText = "Invalid desktop session or data accessor";

                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();
            int tickint = Convert.ToInt32(transferTicketNumber);

            // inParams.Add(new OracleProcParam("p_transfer_ticket_number", transferTicketNumber));
            inParams.Add(new OracleProcParam("p_transfer_ticket_number", tickint));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_status_date", statusDate));

            inParams.Add(new OracleProcParam("p_status_time", statusTime));

            inParams.Add(new OracleProcParam("p_user_id", userId));

            inParams.Add(new OracleProcParam("p_void_reason", voidReason));

            inParams.Add(new OracleProcParam("p_void_comment", voidComment));

            //Add output param
            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value,
                                             ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        procedureName, inParams,
                        refCursArr,
                        "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling " + procedureName + "stored procedure", oEx);
                    errorCode = procedureName;
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                //incase of Proc1 failure dont proceed further
                if (retVal == false || outputDataSet == null)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }

                getReceiptDetail("VoidShopAndGunTransfer", outputDataSet, out receiptNo, out errorText, out errorCode);
                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = procedureName + "Failed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorText = "";
            return true;
        }

        public static bool getTransferToVoid(
            int transferTicketNumber,
            string storeNumber,
            out DataSet mdseInfo,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            mdseInfo = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "getTransferToVoid";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_transTicketNumber", transferTicketNumber));
            //inParams.Add(new OracleProcParam("p_status", "TI"));
            string date_in = DateTime.Today.ToString("d");
            inParams.Add(new OracleProcParam("p_transDate", date_in));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("o_transferInfo", "TRANSINFO"));
                refCursArr.Add(new PairType<string, string>("o_mdse", "MDSEINFO"));

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        "getTransferToVoid", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getTransferToVoid stored procedure", oEx);
                    errorCode = "getTransferToVoid";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    mdseInfo = outputDataSet;

                    return true;
                }

                errorCode = "getTransferToVoid";
                errorText = "Operation failed";
                return (false);
            }
            errorCode = "getTransferToVoidFailed";
            errorText = "No valid input parameters given";
            return (false);
        }

        private static void getReceiptDetail(string functionName, DataSet outputSet, out string receiptNo, out string errorText, out string errorCode)
        {
            DataTable outputDt = outputSet.Tables["OUTPUT"];
            receiptNo = "";
            errorCode = "0";
            errorText = "Success";

            if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object recNumObj = dr.ItemArray.GetValue(1);
                    if (recNumObj.ToString() != "null")
                    {
                        receiptNo = (string)recNumObj;
                        errorCode = "0";
                        errorText = "Success";
                        //return (true);
                    }
                    else
                    {
                        errorCode = functionName + " Failed";
                        errorText = functionName + "Failed - Could not retreive receipt number";
                        //return (false);
                    }
                }
            }
        }

        /// <summary>
        /// Get Eligible to scrap items
        /// </summary>
        /// <param name="transferTicketNumber"></param>
        /// <param name="storeNumber"></param>
        /// <param name="mdseInfo"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetEligibleToScrapItems(
            int transferTicketNumber,
            string storeNumber,
            out DataTable mdseInfo,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            mdseInfo = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetEligibleToScrapItemsFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_transfer_ticket_number", transferTicketNumber));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_mdse_list", "MDSEINFO"));

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        "void_eligible_TO_scrap_items", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling void_eligible_TO_scrap_items stored procedure", oEx);
                    errorCode = "void_eligible_TO_scrap_items";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    mdseInfo = outputDataSet.Tables["MDSEINFO"];
                    return true;
                }

                errorCode = "GetEligibleScrapItemsFailed";
                errorText = "Operation failed";
                return (false);
            }
            errorCode = "GetEligibleScrapItemsFailed";
            errorText = "No valid input parameters given";
            return (false);
        }

        public static bool VoidSale(
            OracleDataAccessor oDa,
            int saleTicketNumber,
            string storeNumber,
            string workstationName,
            string cashDrawerId,
            string voidReason,
            string voidComment,
            int opRef,
            string opAmt,
            int opOrder,
            int receiptDetailNumber,
            string statusDate,
            string statusTime,
            string userName,
            out int receiptNumber,
            out string errorCode,
            out string errorText)
        {
            receiptNumber = 0;
            if (oDa == null)
            {
                errorCode = "VoidSalefailed";
                errorText = "Invalid desktop session or data accessor";

                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_ticket_number", saleTicketNumber));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_workstation_name", workstationName));

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashDrawerId));

            inParams.Add(new OracleProcParam("p_op_ref", opRef));

            inParams.Add(new OracleProcParam("p_op_amt", opAmt));

            inParams.Add(new OracleProcParam("p_op_order", opOrder));

            inParams.Add(new OracleProcParam("p_receiptdetail_number", receiptDetailNumber));

            inParams.Add(new OracleProcParam("p_status_date", statusDate));

            inParams.Add(new OracleProcParam("p_status_time", statusTime));

            inParams.Add(new OracleProcParam("p_user_name", userName));

            inParams.Add(new OracleProcParam("p_reason", voidReason));

            inParams.Add(new OracleProcParam("p_comment", voidComment));

            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        "void_sale", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling void_sale stored procedure", oEx);
                    errorCode = "VoidSale";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized &&
                    outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object rcptNumber = dr.ItemArray.GetValue(1);
                        if (rcptNumber != null)
                        {
                            receiptNumber = Int32.Parse((string)rcptNumber);

                            errorCode = "0";
                            errorText = "Success";
                            return (true);
                        }
                    }
                }
                else
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
            }

            errorCode = "1";
            errorText = "No Valid input parameters given";
            return (false);
        }

        public static bool VoidSaleRefund(
            OracleDataAccessor oDa,
            int refundTicketNumber,
            string storeNumber,
            string workstationName,
            string cashDrawerId,
            string voidReason,
            string voidComment,
            int opRef,
            string opAmt,
            int opOrder,
            int receiptDetailNumber,
            string statusDate,
            string statusTime,
            string userName,
            out int receiptNumber,
            out string errorCode,
            out string errorText)
        {
            receiptNumber = 0;
            if (oDa == null)
            {
                errorCode = "VoidSaleRefundfailed";
                errorText = "Invalid desktop session or data accessor";

                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_sale_refund_number", refundTicketNumber));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_workstation_name", workstationName));

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashDrawerId));

            inParams.Add(new OracleProcParam("p_op_ref", opRef));

            inParams.Add(new OracleProcParam("p_op_amt", opAmt));

            inParams.Add(new OracleProcParam("p_op_order", opOrder));

            inParams.Add(new OracleProcParam("p_receiptdetail_number", receiptDetailNumber));

            inParams.Add(new OracleProcParam("p_status_date", statusDate));

            inParams.Add(new OracleProcParam("p_status_time", statusTime));

            inParams.Add(new OracleProcParam("p_user_name", userName));

            inParams.Add(new OracleProcParam("p_reason", voidReason));

            inParams.Add(new OracleProcParam("p_comment", voidComment));

            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        "void_sale_refund", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling void_sale_refund stored procedure", oEx);
                    errorCode = "VoidSaleRefund";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized &&
                    outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object rcptNumber = dr.ItemArray.GetValue(1);
                        if (rcptNumber != null)
                        {
                            receiptNumber = Int32.Parse((string)rcptNumber);

                            errorCode = "0";
                            errorText = "Success";
                            return (true);
                        }
                    }
                }
            }
            else
            {
                errorCode = "VoidSaleRefundfailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "1";
            errorText = "Error in void sale refund";
            return false;
        }

        public static bool VoidLayaway(
            OracleDataAccessor oDa,
            int layawayTicketNumber,
            string storeNumber,
            string workstationName,
            string cashDrawerId,
            string voidReason,
            string voidComment,
            int opRef,
            string opAmt,
            int opOrder,
            int receiptDetailNumber,
            string statusDate,
            string statusTime,
            string userName,
            out int receiptNumber,
            out string errorCode,
            out string errorText)
        {
            receiptNumber = 0;
            if (oDa == null)
            {
                errorCode = "VoidLayawayfailed";
                errorText = "Invalid desktop session or data accessor";

                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_ticket_number", layawayTicketNumber));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_workstation_name", workstationName));

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashDrawerId));

            inParams.Add(new OracleProcParam("p_op_ref", opRef));

            inParams.Add(new OracleProcParam("p_op_amt", opAmt));

            inParams.Add(new OracleProcParam("p_op_order", opOrder));

            inParams.Add(new OracleProcParam("p_receiptdetail_number", receiptDetailNumber));

            inParams.Add(new OracleProcParam("p_status_date", statusDate));

            inParams.Add(new OracleProcParam("p_status_time", statusTime));

            inParams.Add(new OracleProcParam("p_user_name", userName));

            inParams.Add(new OracleProcParam("p_reason", voidReason));

            inParams.Add(new OracleProcParam("p_comment", voidComment));

            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        "void_layaway", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling void_layaway stored procedure", oEx);
                    errorCode = "VoidLayaway";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized &&
                    outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object rcptNumber = dr.ItemArray.GetValue(1);
                        if (rcptNumber != null)
                        {
                            receiptNumber = Int32.Parse((string)rcptNumber);

                            errorCode = "0";
                            errorText = "Success";
                            return (true);
                        }
                    }
                }
            }
            else
            {
                errorCode = "VoidLayawayfailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "1";
            errorText = "Error in void layaway";
            return false;
        }

        public static bool VoidLayawayPayment(
            OracleDataAccessor oDa,
            int layawayTicketNumber,
            string storeNumber,
            string workstationName,
            string cashDrawerId,
            string opAmt,
            int receiptDetailNumber,
            string statusDate,
            string statusTime,
            string userName,
            string voidReason,
            string voidComment,
            List<string> tenderType,
            List<string> tenderAmount,
            List<string> paymentAuth,
            string receiptDate,
            out int receiptNumber,
            out string errorCode,
            out string errorText)
        {
            receiptNumber = 0;
            if (oDa == null)
            {
                errorCode = "VoidLayawayPaymentfailed";
                errorText = "Invalid desktop session or data accessor";

                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_layaway_number", layawayTicketNumber));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_workstation_name", workstationName));

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashDrawerId));

            inParams.Add(new OracleProcParam("p_op_amt", opAmt));

            inParams.Add(new OracleProcParam("p_receiptdetail_number", receiptDetailNumber));

            inParams.Add(new OracleProcParam("p_status_date", statusDate));

            inParams.Add(new OracleProcParam("p_status_time", statusTime));

            inParams.Add(new OracleProcParam("p_user_name", userName));

            inParams.Add(new OracleProcParam("p_reason", voidReason));
            inParams.Add(new OracleProcParam("p_comment", voidComment));

            inParams.Add(new OracleProcParam("p_payment_type", true, tenderType));
            inParams.Add(new OracleProcParam("p_payment_amt", true, tenderAmount));
            inParams.Add(new OracleProcParam("p_payment_auth", true, paymentAuth));

            inParams.Add(new OracleProcParam("p_receipt_date", receiptDate));

            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        "void_layaway_payment", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling void_layaway_payment stored procedure", oEx);
                    errorCode = "VoidLayawayPayment";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized &&
                    outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object rcptNumber = dr.ItemArray.GetValue(1);
                        if (rcptNumber != null)
                        {
                            receiptNumber = Utilities.GetIntegerValue(rcptNumber.ToString(), 0);

                            errorCode = "0";
                            errorText = "Success";
                            return (true);
                        }
                    }
                }
            }
            else
            {
                errorCode = "VoidLayawayPaymentfailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "1";
            errorText = "Error in void layaway payment";
            return false;
        }

        public static bool VoidLayawayForfeiture(
            OracleDataAccessor oDa,
            int layawayTicketNumber,
            string storeNumber,
            string workstationName,
            string cashDrawerId,
            int receiptDetailNumber,
            string statusDate,
            string statusTime,
            string userName,
            out int receiptNumber,
            out string errorCode,
            out string errorText)
        {
            receiptNumber = 0;
            if (oDa == null)
            {
                errorCode = "VoidLayawayForfeiturefailed";
                errorText = "Invalid desktop session or data accessor";

                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_layaway_number", layawayTicketNumber));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_workstation_name", workstationName));

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashDrawerId));

            inParams.Add(new OracleProcParam("p_op_ref", 1));

            inParams.Add(new OracleProcParam("p_op_amt", "0.00"));

            inParams.Add(new OracleProcParam("p_op_order", 1));

            inParams.Add(new OracleProcParam("p_receiptdetail_number", receiptDetailNumber));

            inParams.Add(new OracleProcParam("p_status_date", statusDate));

            inParams.Add(new OracleProcParam("p_status_time", statusTime));

            inParams.Add(new OracleProcParam("p_user_name", userName));

            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        "void_layaway_forfeiture", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling void_layaway_forfeiture stored procedure", oEx);
                    errorCode = "VoidLayawayForfeiture";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized &&
                    outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object rcptNumber = dr.ItemArray.GetValue(1);
                        if (rcptNumber != null)
                        {
                            receiptNumber = Utilities.GetIntegerValue(rcptNumber.ToString(), 0);

                            errorCode = "0";
                            errorText = "Success";
                            return (true);
                        }
                    }
                }
            }
            else
            {
                errorCode = "VoidLayawayForfeiturefailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "1";
            errorText = "Error in void layaway forfeiture";
            return false;
        }


        public static bool VoidVendorPurchase(
            OracleDataAccessor oDa,
            Int64 ticketNumber,
            string storeNumber,
            string statusDate,
            string statusTime,
            string voidReason,
            string voidComment,
            string userName,
            out int receiptNumber,
            out string errorCode,
            out string errorText)
        {
            receiptNumber = 0;
            if (oDa == null)
            {
                errorCode = "VoidVendorPurchasefailed";
                errorText = "Invalid desktop session or data accessor";

                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_ticket_number", ticketNumber));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_status_date", statusDate));

            inParams.Add(new OracleProcParam("p_status_time", statusTime));

            inParams.Add(new OracleProcParam("p_user_name", userName));

            inParams.Add(new OracleProcParam("p_reason", voidReason));

            inParams.Add(new OracleProcParam("p_comment", voidComment));

            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        "void_vendor_purchase", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling void_vendor_purchase stored procedure", oEx);
                    errorCode = "Void_Vendor_purchase";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized &&
                    outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object rcptNumber = dr.ItemArray.GetValue(1);
                        if (rcptNumber != null)
                        {
                            receiptNumber = Utilities.GetIntegerValue(rcptNumber.ToString(), 0);

                            errorCode = "0";
                            errorText = "Success";
                            return (true);
                        }
                    }
                }
            }
            else
            {
                errorCode = "Void_Vendor_purchasefailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "1";
            errorText = "Error in void vendor purchase";
            return false;
        }

        /// <summary>
        /// Wrapper call to void Pawn loan PFI
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <param name="storeNumber"></param>
        /// <param name="voidReason"></param>
        /// <param name="voidComment"></param>
        /// <param name="docType"></param>
        /// <param name="receiptDetailNumber"></param>
        /// <param name="statusDate"></param>
        /// <param name="statusTime"></param>
        /// <param name="userName"></param>
        /// <param name="receiptNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool VoidPFI(
            int ticketNumber,
            string storeNumber,
            string voidReason,
            string voidComment,
            string docType,
            int receiptDetailNumber,
            string statusDate,
            string statusTime,
            string userName,
            out int receiptNumber,
            out string errorCode,
            out string errorText)
        {
            receiptNumber = 0;
            OracleDataAccessor oDa = GlobalDataAccessor.Instance.OracleDA;

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_ticket_number", ticketNumber));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_doc_type", docType));

            inParams.Add(new OracleProcParam("p_receiptdetail_number", receiptDetailNumber));

            inParams.Add(new OracleProcParam("p_status_date", statusDate));

            inParams.Add(new OracleProcParam("p_status_time", statusTime));

            inParams.Add(new OracleProcParam("p_user_id", userName));

            inParams.Add(new OracleProcParam("p_void_reason", voidReason));

            inParams.Add(new OracleProcParam("p_void_comment", voidComment));

            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        "void_pfi", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling void_pfi stored procedure", oEx);
                    errorCode = "VoidPFI";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized &&
                    outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object rcptNumber = dr.ItemArray.GetValue(1);
                        if (rcptNumber != null)
                        {
                            receiptNumber = Int32.Parse((string)rcptNumber);

                            errorCode = "0";
                            errorText = "Success";
                            return (true);
                        }
                    }
                }
                else
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
            }

            errorCode = "1";
            errorText = "No Valid input parameters given";
            return (false);
        }


        /// <summary>
        /// Wrapper call to void Pawn loan partial payment
        /// </summary>
        public static bool VoidPartialPayment(
            string storeNumber,
            string voidReason,
            string voidComment,
            int receiptDetailNumber,
            string statusDate,
            string statusTime,
            string userName,
            out int receiptNumber,
            out string errorCode,
            out string errorText)
        {
            receiptNumber = 0;
            OracleDataAccessor oDa = GlobalDataAccessor.Instance.OracleDA;

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_receiptdetail_number", receiptDetailNumber));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_status_date", statusDate));

            inParams.Add(new OracleProcParam("p_status_time", statusTime));

            inParams.Add(new OracleProcParam("p_user_id", userName));

            inParams.Add(new OracleProcParam("p_void_reason", voidReason));

            inParams.Add(new OracleProcParam("p_void_comment", voidComment));

            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_voids",
                        "voidPartialPayment", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling voidPartialPayment stored procedure", oEx);
                    errorCode = "VoidPartialPayment";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized &&
                    outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object rcptNumber = dr.ItemArray.GetValue(1);
                        if (rcptNumber != null)
                        {
                            receiptNumber = Int32.Parse((string)rcptNumber);

                            errorCode = "0";
                            errorText = "Success";
                            return (true);
                        }
                    }
                }
                else
                {
                    errorCode = oDa.ErrorCode;
                    errorText = oDa.ErrorDescription;
                    return (false);
                }
            }

            errorCode = "1";
            errorText = "No Valid input parameters given";
            return (false);
        }



    }
}
