using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Services.MerchandiseTransfer;
using ReportObject = Common.Controllers.Application.ReportObject;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class TransferInFlowExecutor : SingleExecuteBlock
    {
        public const string NAME = "TransferInFlowExecutor";

        public enum TransferInFlowState
        {
            ManageTransfer,
            RejectComments,
            SelectTransferMethod,
            SelectItems,
            ExitFlow,
            CancelFlow,
            Error
        }

        private TransferInFlowState nextState;
        private TransferInFlowState suspendedState = TransferInFlowState.Error;
        private Form parentForm;
        private FxnBlock endStateNotifier;
        
        /// <summary>
        /// Main execution function for ChangeRetailPriceFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
            {
                inputData = suspendedState;
            }
            TransferInFlowState inputState = (TransferInFlowState)inputData;
            var dSession = GlobalDataAccessor.Instance.DesktopSession;

            switch (inputState)
            {
                case TransferInFlowState.ManageTransfer:
                    if (dSession.ActiveTransferIn == null)
                    {
                        dSession.ActiveTransferIn = new TransferVO();
                    }

                    dSession.TransferMethod = TransferMethod.QuickReceive;

                    var transferIn = new ManageTransferIn();
                    Form currForm = dSession.HistorySession.Lookup(transferIn);
                    if (currForm.GetType() == typeof(ManageTransferIn))
                    {
                        currForm.Show();
                    }
                    else
                    {
                        ShowForm manageTransferInBlk = CommonAppBlocks.Instance.CreateManageTransferInShowBlock(this.parentForm, this.manageTransferInFormNavAction);
                        if (!manageTransferInBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Manage Transfer In Show block");
                        }
                    }

                    break;
                case TransferInFlowState.SelectTransferMethod:
                    ShowForm selectTransferMethodBlk = CommonAppBlocks.Instance.CreateSelectTransferMethodShowBlock(this.parentForm, this.selectTransferMethodFormAction);
                    if (!selectTransferMethodBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Select Transfer Method Block");
                    }
                    break;
                case TransferInFlowState.SelectItems:
                    ShowForm selectTransferInItemsBlk = CommonAppBlocks.Instance.CreateSelectTransferInItemsShowBlock(this.parentForm, this.selectTransferInItemsFormAction);
                    if (!selectTransferInItemsBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Select Transfer In Items Block");
                    }
                    break;
                case TransferInFlowState.RejectComments:
                    ShowForm transferRejectCommentBlk = CommonAppBlocks.Instance.CreateTransferRejectCommentShowBlock(this.parentForm, this.transferRejectCommentFormAction);
                    if (!transferRejectCommentBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Transfer Reject Comment Block");
                    }
                    break;
                case TransferInFlowState.CancelFlow:
                    dSession.ClearLoggedInUser();
                    dSession.Transfers = new List<TransferVO>(1) { new TransferVO() };
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
                case TransferInFlowState.ExitFlow:
                    break;
                default:
                    throw new ApplicationException("Invalid change retail price flow state");
            }

            return (true);
        }

        private void transferRejectCommentFormAction(object sender, object data)
        {
            NavBox transferRejectCommentNavBox = (NavBox)sender;
            NavBox.NavAction action = transferRejectCommentNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    string errorCode, errorText;
                    bool retValue = TransferProcedures.RejectTransfer(dSession, dSession.ActiveTransferIn, out errorCode, out errorText);
                    if (!retValue)
                    {
                        MessageBox.Show("An error occured while rejecting the transfer.");
                    }
                    this.nextState = TransferInFlowState.ManageTransfer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = TransferInFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException(string.Format("{0} is not a valid state for SelectTransferInItems", action.ToString()));
            }

            this.executeNextState();
        }

        private void selectTransferInItemsFormAction(object sender, object data)
        {
            NavBox selectTransferInItemsNavBox = (NavBox)sender;
            NavBox.NavAction action = selectTransferInItemsNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    string errorCode, errorText;
                    int ticketNumber;
                    GlobalDataAccessor.Instance.beginTransactionBlock();
                    bool retValue = TransferProcedures.ProcessTransferIn(
                        dSession, 
                        dSession.ActiveTransferIn, 
                        out ticketNumber, out errorCode, out errorText);
  

                    if (!retValue || ticketNumber == 0)
                    {
                        GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                        MessageBox.Show("An error occured while completing the transfer. Please contact Shop Systems Support.");
                    }
                    else
                    {
                        GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                        //-----------
                        //MessageBox.Show("report");
                        var transferList = new List<TransferItemVO>();
                        var reportObj = new ReportObject.TransferINReportStruct();

                        reportObj.transDate = ShopDateTime.Instance.ShopDate.ToString();
                        reportObj.userID = dSession.FullUserName;
                        reportObj.ToStoreName = GlobalDataAccessor.Instance.CurrentSiteId.StoreNickName;
                        reportObj.ToStoreNo = string.Empty;
                        reportObj.FromShopName = string.Format("Shop # - {0}", dSession.ActiveTransferIn.StoreInfo.StoreNickName);
                        reportObj.FromShopNo = string.Empty;
                        reportObj.FromStoreAddrLine1 = string.Empty;
                        reportObj.FromStoreAddrLine2 = string.Empty;

                        reportObj.storeMgrPhone = string.Empty;
                        reportObj.storeMgrName = string.Empty;
                        reportObj.transNum = string.Format("{0}", ticketNumber);
                        reportObj.Carrier = dSession.ActiveTransferIn.Carrier;
                        reportObj.DateReceived = dSession.ActiveTransferIn.StatusDate.ToString();
                        reportObj.TransferReference = dSession.ActiveTransferIn.TransferTicketNumber.ToString();
                        reportObj.ReceivedBy = string.Empty;
                        reportObj.logPath =
                        SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;

                        var transreport = new Reports.TransferIN.SummaryReport(GlobalDataAccessor.Instance.DesktopSession.ActiveTransferIn.Items, reportObj, PdfLauncher.Instance);
                        transreport.CreateReport();

                        //TODO: Store report in couch db
                        if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                            dSession.LaserPrinter.IsValid)
                        {
                            string laserPrinterIp = dSession.LaserPrinter.IPAddress;
                            int laserPrinterPort = dSession.LaserPrinter.Port;
                            PrintingUtilities.printDocument(transreport.getReportFileName(),
                                                            laserPrinterIp,
                                                            laserPrinterPort,
                                                            1);
                        }//end if (PrintEnabled)

                        //------------
                    }
                    this.nextState = TransferInFlowState.ManageTransfer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = TransferInFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action + " is not a valid state for SelectTransferInItems");
            }

            this.executeNextState();
        }

        private void selectTransferMethodFormAction(object sender, object data)
        {
            NavBox selectTransferMethodNavBox = (NavBox)sender;
            NavBox.NavAction action = selectTransferMethodNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    this.nextState = TransferInFlowState.SelectItems;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = TransferInFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action + " is not a valid state for SelectTransferInMethod");
            }

            this.executeNextState();
        }

        private void manageTransferInFormNavAction(object sender, object data)
        {
            NavBox manageTransferInNavBox = (NavBox)sender;
            NavBox.NavAction action = manageTransferInNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    if (manageTransferInNavBox.IsCustom)
                    {
                        if (manageTransferInNavBox.CustomDetail.Equals("RejectComments"))
                        {
                            this.nextState = TransferInFlowState.RejectComments;
                        }
                        else
                        {
                            throw new ApplicationException("" + action.ToString() + " is not a valid state for ManageTransferIn");
                        }
                    }
                    else
                    {
                        if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsQuickReceiveTransferInAllowed(GlobalDataAccessor.Instance.CurrentSiteId))
                        {
                            this.nextState = TransferInFlowState.SelectTransferMethod;
                        }
                        else
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TransferMethod = TransferMethod.Manual;
                            this.nextState = TransferInFlowState.SelectItems;
                        }
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = TransferInFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for ManageTransferIn");
            }

            this.executeNextState();
        }

        private void executeNextState()
        {
            object evalExecFlag = this.executorFxn(this.nextState);
            if (evalExecFlag == null || ((bool)(evalExecFlag)) == false)
            {
                throw new ApplicationException("Cannot execute the next state: " + this.nextState.ToString());
            }
        }

        public TransferInFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
        : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = TransferInFlowState.ManageTransfer;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }

        public TransferInFlowState NextState
        {
            get
            {
                return (this.nextState);
            }
        }
    }
}
