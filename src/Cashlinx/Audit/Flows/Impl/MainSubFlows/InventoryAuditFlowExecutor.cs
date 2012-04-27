using System;
using System.Windows.Forms;
using Audit.Forms.Inventory;
using Audit.Logic;
using Audit.Procedures;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Objects.Audit;
using System.Collections.Generic;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Audit.Flows.Impl.MainSubFlows
{
    public class InventoryAuditFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "InventoryAuditFlowExecutor";

        public enum InventoryAuditFlowState
        {
            SelectAudit,
            InitiateAudit,
            AuditResults,
            SelectStore,
            AuditManager,
            SelectCategory,
            AbortAudit,
            DownloadToTrakker,
            UploadFromTrakker,
            ProcessMissing,
            ProcessUnexpected,
            InventorySummary,
            InventoryQuestions,
            ChargeOn,
            DescribeItem,
            CountCACC,
            ExitFlow,
            CancelFlow,
            Error
        }

        public InventoryAuditFlowExecutor(AuditDesktopSession desktopSession, Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.DesktopSession = desktopSession;
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = InventoryAuditFlowState.SelectAudit;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }

        public AuditDesktopSession DesktopSession { get; private set; }

        public InventoryAuditFlowState NextState
        {
            get
            {
                return (this.nextState);
            }
        }

        private InventoryAuditFlowState nextState;
        private InventoryAuditFlowState suspendedState = InventoryAuditFlowState.Error;
        private Form parentForm;
        private FxnBlock endStateNotifier;

        private object executorFxn(object inputData)
        {
            if (inputData == null)
            {
                inputData = suspendedState;
            }
            InventoryAuditFlowState inputState = (InventoryAuditFlowState)inputData;

            Form currForm;
            switch (inputState)
            {
                case InventoryAuditFlowState.SelectAudit:
                    SelectAudit selectAudit = new SelectAudit();
                    currForm = DesktopSession.HistorySession.Lookup(selectAudit);
                    if (currForm.GetType() == typeof(SelectAudit))
                    {
                        currForm.Show();
                    }
                    else
                    {
                        ShowForm selectAuditBlk = CommonAppBlocks.Instance.CreateSelectAuditShowBlock(this.parentForm, this.selectAuditFormNavAction);
                        if (!selectAuditBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Select Audit block");
                        }
                    }

                    break;
                case InventoryAuditFlowState.SelectStore:
                    SelectStore selectStore = new SelectStore();
                    currForm = DesktopSession.HistorySession.Lookup(selectStore);
                    if (currForm.GetType() == typeof(SelectStore))
                    {
                        currForm.Show();
                    }
                    else
                    {
                        ShowForm selectStoreBlk = CommonAppBlocks.Instance.CreateSelectStoreShowBlock(this.parentForm, this.selectStoreFormNavAction);
                        if (!selectStoreBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Select Store block");
                        }
                    }

                    break;
                case InventoryAuditFlowState.InitiateAudit:
                    InitiateAudit initiateAudit = new InitiateAudit();
                    currForm = DesktopSession.HistorySession.Lookup(initiateAudit);
                    if (currForm.GetType() == typeof(InitiateAudit))
                    {
                        currForm.Show();
                    }
                    else
                    {
                        ShowForm initiateAuditBlk = CommonAppBlocks.Instance.CreateInitiateAuditShowBlock(this.parentForm, this.initiateAuditFormNavAction);
                        if (!initiateAuditBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Initiate Audit block");
                        }
                    }

                    break;
                case InventoryAuditFlowState.AuditManager:
                    AuditManager auditManager = new AuditManager();
                    currForm = DesktopSession.HistorySession.Lookup(auditManager);
                    if (currForm.GetType() == typeof(AuditManager))
                    {
                        currForm.Show();
                    }
                    else
                    {
                        ShowForm auditManagerBlk = CommonAppBlocks.Instance.CreateAuditManagerShowBlock(this.parentForm, this.auditManagerFormNavAction);
                        if (!auditManagerBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Audit Manager block");
                        }
                    }

                    break;
                case InventoryAuditFlowState.InventorySummary:
                    InventorySummary inventorySummary = new InventorySummary();
                    currForm = DesktopSession.HistorySession.Lookup(inventorySummary);

                    ShowForm inventorySummaryBlk = CommonAppBlocks.Instance.CreateInventorySummaryShowBlock(this.parentForm, this.inventorySummaryFormNavAction);
                    if (!inventorySummaryBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Inventory Summary block");
                    }
                    

                    break;
                case InventoryAuditFlowState.InventoryQuestions:
                    InventoryQuestions inventoryQuestions = new InventoryQuestions();
                    currForm = DesktopSession.HistorySession.Lookup(inventoryQuestions);
                    if (currForm.GetType() == typeof(InventoryQuestions))
                    {
                        currForm.Show();
                    }
                    else
                    {
                        ShowForm inventoryQuestionsBlk = CommonAppBlocks.Instance.CreateInventoryQuestionsShowBlock(this.parentForm, this.inventoryQuestionsFormNavAction);
                        if (!inventoryQuestionsBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Inventory Questions block");
                        }
                    }

                    break;
                case InventoryAuditFlowState.DownloadToTrakker:
                    //DownloadToTrakker downloadToTrakker = new DownloadToTrakker();
                    //currForm = DesktopSession.HistorySession.Lookup(downloadToTrakker);
                    //if (currForm.GetType() == typeof(DownloadToTrakker))
                    //{
                    //    currForm.Show();
                    //}
                    //else
                    //{
                    ShowForm downloadToTrakkerBlk = CommonAppBlocks.Instance.CreateDownloadToTrakkerShowBlock(this.parentForm, this.downloadToTrakkerFormNavAction);
                    if (!downloadToTrakkerBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Download to Trakker block");
                    }
                    //}

                    break;
                case InventoryAuditFlowState.UploadFromTrakker:
                    //UploadFromTrakker uploadFromTrakker = new UploadFromTrakker();
                    //currForm = DesktopSession.HistorySession.Lookup(uploadFromTrakker);
                    //if (currForm.GetType() == typeof(DownloadToTrakker))
                    //{
                    //    currForm.Show();
                    //}
                    //else
                    //{
                    ShowForm uploadFromTrakkerBlk = CommonAppBlocks.Instance.CreateUploadFromTrakkerShowBlock(this.parentForm, this.uploadFromTrakkerFormNavAction);
                    if (!uploadFromTrakkerBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Upload from Trakker block");
                    }
                    //}

                    break;
                case InventoryAuditFlowState.ProcessMissing:
                    //ProcessMissingItems processMissingItems = new ProcessMissingItems();
                    //currForm = DesktopSession.HistorySession.Lookup(processMissingItems);
                    //if (currForm.GetType() == typeof(ProcessMissingItems))
                    //{
                    //    currForm.Show();
                    //}
                    //else
                    //{
                    ShowForm processMissingItemsBlk = CommonAppBlocks.Instance.CreateProcessMissingItemsShowBlock(this.parentForm, this.processMissingItemsFormNavAction);
                    if (!processMissingItemsBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Process Missing Items block");
                    }
                    //}

                    break;
                case InventoryAuditFlowState.ProcessUnexpected:
                    //ProcessUnexpectedItems processUnexpectedItems = new ProcessUnexpectedItems();
                    //currForm = DesktopSession.HistorySession.Lookup(processUnexpectedItems);
                    //if (currForm.GetType() == typeof(ProcessUnexpectedItems))
                    //{
                    //    currForm.Show();
                    //}
                    //else
                    //{
                    ShowForm processUnexpectedItemsBlk = CommonAppBlocks.Instance.CreateProcessUnexpectedItemsShowBlock(this.parentForm, this.processUnexpectedItemsFormNavAction);
                    if (!processUnexpectedItemsBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Process Unexpected Items block");
                    }
                    //}

                    break;
                case InventoryAuditFlowState.CountCACC:
                    EnterCaccItems enterCaccItems = new EnterCaccItems();
                    currForm = DesktopSession.HistorySession.Lookup(enterCaccItems);
                    if (currForm.GetType() == typeof(EnterCaccItems))
                    {
                        currForm.Show();
                    }
                    else
                    {
                        ShowForm enterCaccItemsBlk = CommonAppBlocks.Instance.CreateEnterCaccItemsShowBlock(this.parentForm, this.enterCaccItemsFormNavAction);
                        if (!enterCaccItemsBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Enter Cacc Items block");
                        }
                    }

                    break;
                case InventoryAuditFlowState.AuditResults:
                    ClosedAudit closedAudit = new ClosedAudit();
                    currForm = DesktopSession.HistorySession.Lookup(closedAudit);
                    if (currForm.GetType() == typeof(ClosedAudit))
                    {
                        currForm.Show();
                    }
                    else
                    {
                        ShowForm closedAuditBlk = CommonAppBlocks.Instance.CreateClosedAuditShowBlock(this.parentForm, this.closedAuditFormNavAction);
                        if (!closedAuditBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Closed Audit block");
                        }
                    }

                    break;
                case InventoryAuditFlowState.ChargeOn:
                    ShowForm describeMerchBlk = CommonAppBlocks.Instance.DescribeMerchChargeOnBlock(this.parentForm, this.describeMerchFormAction, DesktopSession);
                    if (!describeMerchBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Merchandise Block");
                    }
                    break;
                case InventoryAuditFlowState.DescribeItem:
                    ShowForm describeItemBlk = CommonAppBlocks.Instance.DescribeItemBlock(this.parentForm, this.describeItemFormAction, DesktopSession);
                    if (!describeItemBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Item Block");
                    }
                    break;

                case InventoryAuditFlowState.CancelFlow:
                    AuditDesktopSession.Instance.ClearLoggedInUser();
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
                case InventoryAuditFlowState.ExitFlow:
                    AuditDesktopSession.Instance.ClearLoggedInUser();
                    break;
                default:
                    throw new ApplicationException("Invalid inventory audit flow state");
            }

            return true;
        }

        private void executeNextState()
        {
            object evalExecFlag = this.executorFxn(this.nextState);
            if (evalExecFlag == null || ((bool)(evalExecFlag)) == false)
            {
                throw new ApplicationException("Cannot execute the next state: " + this.nextState.ToString());
            }
        }

        /// <summary>
        /// The various form actions for the describe merchandise form in the charge on flow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void describeMerchFormAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Describe Merchandise form navigation action handler received invalid data");
            }

            NavBox descMerchNavBox = (NavBox)sender;
            NavBox.NavAction action = descMerchNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                AuditDesktopSession.Instance.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (AuditDesktopSession.Instance.ActivePawnLoan != null)
                    {
                        if (AuditDesktopSession.Instance.ActivePawnLoan.Items != null && AuditDesktopSession.Instance.ActivePawnLoan.Items.Count > 0 &&
                            AuditDesktopSession.Instance.DescribeItemPawnItemIndex > -1)
                        {
                            Item rItem = AuditDesktopSession.Instance.ActivePawnLoan.Items[AuditDesktopSession.Instance.DescribeItemPawnItemIndex];
                            List<int> caccCatCodes = new List<int> { 3362, 3350, 3362, 3363, 3380 };
                            if (caccCatCodes.Contains(rItem.CategoryCode))
                            {
                                MessageBox.Show("Unable to charge on a CACC item in the Audit Application.");
                                AuditDesktopSession.Instance.HistorySession.Back();
                                this.nextState = InventoryAuditFlowState.ProcessUnexpected;
                            }
                            else
                            {
                                this.nextState = InventoryAuditFlowState.DescribeItem;
                            }
                        }
                        else
                        {
                            this.nextState = InventoryAuditFlowState.DescribeItem;
                        }
                    }
                    else
                    {
                        this.nextState = InventoryAuditFlowState.DescribeItem;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    AuditDesktopSession.Instance.HistorySession.Back();
                    this.nextState = InventoryAuditFlowState.ProcessUnexpected;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Describe Merchandise");
            }
            this.executeNextState();
        }

        /// <summary>
        /// The various form actions for describe item form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void describeItemFormAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Describe Item form navigation action handler received invalid data");
            }

            NavBox descItemNavBox = (NavBox)sender;
            NavBox.NavAction action = descItemNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                AuditDesktopSession.Instance.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (AuditDesktopSession.Instance.ActivePawnLoan != null)
                    {
                        if (AuditDesktopSession.Instance.ActivePawnLoan.Items != null && AuditDesktopSession.Instance.ActivePawnLoan.Items.Count > 0 &&
                            AuditDesktopSession.Instance.DescribeItemPawnItemIndex > -1)
                        {
                            Item rItem = AuditDesktopSession.Instance.ActivePawnLoan.Items[AuditDesktopSession.Instance.DescribeItemPawnItemIndex];
                            rItem.TempStatus = StateStatus.CON;
                            rItem.ItemStatus = ProductStatus.CON;
                            rItem.mStore = Utilities.GetIntegerValue(AuditDesktopSession.Instance.ActiveAudit.StoreNumber);
                            rItem.Icn = RetailProcedures.GenerateTempICN(AuditDesktopSession.Instance, AuditDesktopSession.Instance.ActiveAudit.StoreNumber, rItem.mStore, rItem.mYear);
                            rItem.mDocNumber = Utilities.GetIntegerValue(rItem.Icn.Substring(6, 6), 0);
                            rItem.mDocType = "8";
                            rItem.PfiTags = 1;
                            AuditDesktopSession.Instance.ActivePawnLoan.Items.RemoveAt(AuditDesktopSession.Instance.DescribeItemPawnItemIndex);
                            AuditDesktopSession.Instance.ActivePawnLoan.Items.Add(rItem);
                        }
                    }
                    if (AuditDesktopSession.Instance.ActivePawnLoan != null)
                    {
                        AuditProcedures.ProcessChargeonNewItems(AuditDesktopSession.Instance,AuditDesktopSession.Instance.ActivePawnLoan.Items,
                            AuditDesktopSession.Instance.ActiveAudit.StoreNumber);
                    }
                    AuditDesktopSession.Instance.PawnLoans.Clear();
                    this.nextState = InventoryAuditFlowState.ProcessUnexpected;
                    break;
                case NavBox.NavAction.CANCEL:
                    AuditDesktopSession.Instance.HistorySession.Back();
                    this.nextState = InventoryAuditFlowState.ProcessUnexpected;
                    break;
                case NavBox.NavAction.BACK:
                    AuditDesktopSession.Instance.HistorySession.Back();
                    this.nextState = InventoryAuditFlowState.ChargeOn;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Describe Item");
            }
            this.executeNextState();
        }



        private void selectAuditFormNavAction(object sender, object data)
        {
            NavBox selectAuditNavBox = (NavBox)sender;
            SelectAudit selectAuditForm = (SelectAudit)data;
            NavBox.NavAction action = selectAuditNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    this.parentForm = selectAuditForm;
                    if (!selectAuditNavBox.IsCustom)
                    {
                        throw new ApplicationException("SelectAudit only supports Custom Actions");
                    }
                    if (selectAuditNavBox.CustomDetail.Equals("ABORT"))
                    {
                        this.nextState = InventoryAuditFlowState.AbortAudit;
                    }
                    else if (selectAuditNavBox.CustomDetail.Equals("SELECTSTORE")) // not currently used due to requirement changes
                    {
                        selectAuditForm.Hide();
                        this.nextState = InventoryAuditFlowState.SelectStore;
                    }
                    else if (selectAuditNavBox.CustomDetail.Equals("INITIATEAUDIT"))
                    {
                        selectAuditForm.Hide();
                        this.nextState = InventoryAuditFlowState.InitiateAudit;
                    }
                    else if (selectAuditNavBox.CustomDetail.Equals("VIEWACTIVEAUDIT"))
                    {
                        selectAuditForm.Hide();
                        this.nextState = InventoryAuditFlowState.AuditManager;
                    }
                    else if (selectAuditNavBox.CustomDetail.Equals("VIEWCLOSEDAUDIT"))
                    {
                        selectAuditForm.Hide();
                        this.nextState = InventoryAuditFlowState.AuditResults;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = InventoryAuditFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for SelectAudit");
            }

            this.executeNextState();
        }

        private void selectStoreFormNavAction(object sender, object data)
        {
            NavBox selectStoreNavBox = (NavBox)sender;
            SelectStore selectStoreForm = (SelectStore)data;
            NavBox.NavAction action = selectStoreNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    this.parentForm = selectStoreForm;
                    selectStoreForm.Hide();
                    this.nextState = InventoryAuditFlowState.InitiateAudit;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = InventoryAuditFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for SelectStore");
            }

            this.executeNextState();
        }

        private void initiateAuditFormNavAction(object sender, object data)
        {
            NavBox initiateAuditNavBox = (NavBox)sender;
            InitiateAudit initiateAuditForm = (InitiateAudit)data;
            NavBox.NavAction action = initiateAuditNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    this.parentForm = initiateAuditForm;
                    initiateAuditForm.Hide();
                    if (AuditDesktopSession.Instance.ActiveAudit.AuditScope == AuditScope.PARTIAL)
                    {
                        this.nextState = InventoryAuditFlowState.SelectCategory;
                    }
                    else
                    {
                        this.nextState = InventoryAuditFlowState.AuditManager;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = InventoryAuditFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for InitiateAudit");
            }

            this.executeNextState();
        }

        private void auditManagerFormNavAction(object sender, object data)
        {
            NavBox auditManagerNavBox = (NavBox)sender;
            AuditManager auditManagerForm = (AuditManager)data;
            NavBox.NavAction action = auditManagerNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    this.parentForm = auditManagerForm;
                    if (auditManagerNavBox.IsCustom)
                    {
                        auditManagerForm.Hide();
                        if (auditManagerNavBox.CustomDetail.Equals("DOWNLOAD"))
                        {
                            this.nextState = InventoryAuditFlowState.DownloadToTrakker;
                        }
                        else if (auditManagerNavBox.CustomDetail.Equals("UPLOAD"))
                        {
                            this.nextState = InventoryAuditFlowState.UploadFromTrakker;
                        }
                        else if (auditManagerNavBox.CustomDetail.Equals("PROCESSMISSING"))
                        {
                            this.nextState = InventoryAuditFlowState.ProcessMissing;
                        }
                        else if (auditManagerNavBox.CustomDetail.Equals("PROCESSUNEXPECTED"))
                        {
                            this.nextState = InventoryAuditFlowState.ProcessUnexpected;
                        }
                        else if (auditManagerNavBox.CustomDetail.Equals("COUNTCACC"))
                        {
                            this.nextState = InventoryAuditFlowState.CountCACC;
                        }
                    }
                    else
                    {
                        auditManagerForm.Hide();
                        this.nextState = InventoryAuditFlowState.InventorySummary;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = InventoryAuditFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for InitiateAudit");
            }

            this.executeNextState();
        }

        private void inventorySummaryFormNavAction(object sender, object data)
        {
            NavBox inventorySummaryNavBox = (NavBox)sender;
            InventorySummary inventorySummaryForm = (InventorySummary)data;
            NavBox.NavAction action = inventorySummaryNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    inventorySummaryForm.Hide();
                    this.nextState = InventoryAuditFlowState.InventoryQuestions;
                    break;
                case NavBox.NavAction.CANCEL:
                    inventorySummaryForm.Hide();
                    this.nextState = InventoryAuditFlowState.AuditManager;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for InventorySummary");
            }

            this.executeNextState();
        }

        private void inventoryQuestionsFormNavAction(object sender, object data)
        {
            NavBox inventoryQuestionsNavBox = (NavBox)sender;
            InventoryQuestions inventoryQuestionsForm = (InventoryQuestions)data;
            NavBox.NavAction action = inventoryQuestionsNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    inventoryQuestionsForm.Hide();
                    this.nextState = InventoryAuditFlowState.AuditResults;
                    break;
                case NavBox.NavAction.CANCEL:
                    inventoryQuestionsForm.Hide();
                    this.nextState = InventoryAuditFlowState.AuditManager;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for InventoryQuestions");
            }

            this.executeNextState();
        }

        private void downloadToTrakkerFormNavAction(object sender, object data)
        {
            NavBox downloadToTrakkerNavBox = (NavBox)sender;
            DownloadToTrakker downloadToTrakkerForm = (DownloadToTrakker)data;
            NavBox.NavAction action = downloadToTrakkerNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    //this.parentForm = downloadToTrakkerForm;
                    downloadToTrakkerForm.Hide();
                    this.nextState = InventoryAuditFlowState.AuditManager;
                    break;
                case NavBox.NavAction.CANCEL:
                    downloadToTrakkerForm.Hide();
                    this.nextState = InventoryAuditFlowState.AuditManager;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for DownloadToTrakker");
            }

            this.executeNextState();
        }

        private void uploadFromTrakkerFormNavAction(object sender, object data)
        {
            NavBox uploadFromTrakkerNavBox = (NavBox)sender;
            UploadFromTrakker uploadFromTrakkerForm = (UploadFromTrakker)data;
            NavBox.NavAction action = uploadFromTrakkerNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    uploadFromTrakkerForm.Hide();
                    this.nextState = InventoryAuditFlowState.AuditManager;
                    break;
                case NavBox.NavAction.CANCEL:
                    uploadFromTrakkerForm.Hide();
                    this.nextState = InventoryAuditFlowState.AuditManager;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for UploadFromTrakker");
            }

            this.executeNextState();
        }

        private void processMissingItemsFormNavAction(object sender, object data)
        {
            NavBox processMissingItemsNavBox = (NavBox)sender;
            ProcessMissingItems processMissingItemsForm = (ProcessMissingItems)data;
            NavBox.NavAction action = processMissingItemsNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    processMissingItemsForm.Hide();
                    this.nextState = InventoryAuditFlowState.AuditManager;
                    break;
                case NavBox.NavAction.CANCEL:
                    processMissingItemsForm.Hide();
                    this.nextState = InventoryAuditFlowState.AuditManager;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for ProcessMissingItems");
            }

            this.executeNextState();
        }

        private void processUnexpectedItemsFormNavAction(object sender, object data)
        {
            NavBox processUnexpectedItemsNavBox = (NavBox)sender;
            ProcessUnexpectedItems processUnexpectedItemsForm = (ProcessUnexpectedItems)data;
            NavBox.NavAction action = processUnexpectedItemsNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    processUnexpectedItemsForm.Hide();
                    if (processUnexpectedItemsNavBox.IsCustom)
                    {
                        if (processUnexpectedItemsNavBox.CustomDetail.Equals("CHARGEON"))
                        {
                            this.nextState = InventoryAuditFlowState.ChargeOn;
                        }
                    }
                    else
                    {
                        this.nextState = InventoryAuditFlowState.AuditManager;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    processUnexpectedItemsForm.Hide();
                    this.nextState = InventoryAuditFlowState.AuditManager;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for ProcessUnexpectedItems");
            }

            this.executeNextState();
        }

        private void enterCaccItemsFormNavAction(object sender, object data)
        {
            NavBox enterCaccItemsNavBox = (NavBox)sender;
            EnterCaccItems enterCaccItemsForm = (EnterCaccItems)data;
            NavBox.NavAction action = enterCaccItemsNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    enterCaccItemsForm.Hide();
                    this.nextState = InventoryAuditFlowState.AuditManager;
                    break;
                case NavBox.NavAction.CANCEL:
                    enterCaccItemsForm.Hide();
                    this.nextState = InventoryAuditFlowState.AuditManager;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for EnterCaccItems");
            }

            this.executeNextState();
        }

        private void closedAuditFormNavAction(object sender, object data)
        {
            NavBox closedAuditNavBox = (NavBox)sender;
            ClosedAudit closedAuditForm = (ClosedAudit)data;
            NavBox.NavAction action = closedAuditNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    closedAuditForm.Hide();
                    this.nextState = InventoryAuditFlowState.SelectAudit;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = InventoryAuditFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for ClosedAudit");
            }

            this.executeNextState();
        }
    }
}
