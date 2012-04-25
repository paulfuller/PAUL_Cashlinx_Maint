using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Pawn.Forms.Pawn.Tender;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class LayawayReturnFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "LayawayReturnFlowExecutor";
        public enum LayawayReturnFlowState
        {
            LayawaySearch,
            LayawayRefund,
            TenderOut,
            CancelFlow
        }

        private LayawayReturnFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;

        /// <summary>
        /// Main execution function for RetailReturnFlowexecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
                return (false);
            LayawayReturnFlowState inputState = (LayawayReturnFlowState)inputData;

            switch (inputState)
            {
                case LayawayReturnFlowState.LayawaySearch:
                    ShowForm buyReturnFrmBlk = CommonAppBlocks.Instance.LayawaySearchFormBlock(this.parentForm, this.layawaySearchFormNavAction);
                    if (!buyReturnFrmBlk.execute())
                    {
                        throw new ApplicationException("Cannot show layaway search form");
                    }
                    break;
                case LayawayReturnFlowState.LayawayRefund:
                    ShowForm buyReturnItemBlk = CommonAppBlocks.Instance.LayawayRefundFormBlock(this.parentForm, this.layRefundFormNavAction);
                    if (!buyReturnItemBlk.execute())
                    {
                        throw new ApplicationException("Cannot show layaway refund form");
                    }
 
                    break;
                case LayawayReturnFlowState.TenderOut:
                    if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup("TenderOut"))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.VisibleForm("TenderOut");
                    }
                    else
                    {
                        ShowForm tenderBlk = CommonAppBlocks.Instance.TenderOutExistingRefundShowBlock(this.parentForm, this.TenderOutFormAction);
                        if (!tenderBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Tender Out Block");
                        }
                    }
                    break;
                case LayawayReturnFlowState.CancelFlow:
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
                    GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
 
 
            }


            return (true);
        }

        /// <summary>
        /// Navigation actions for layaway search form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void layawaySearchFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Layaway search navigation action handler received invalid data");
            }

            NavBox navBox = (NavBox)sender;
            NavBox.NavAction action = navBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (navBox.IsCustom && navBox.CustomDetail.Equals("SHOWITEMS", StringComparison.OrdinalIgnoreCase))
                        this.nextState = LayawayReturnFlowState.LayawayRefund;
                    else
                        this.nextState = LayawayReturnFlowState.CancelFlow;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LayawayReturnFlowState.CancelFlow;
                    break;
            }

            this.executeNextState();
        }


        /// <summary>
        /// Navigation actions for layaway refund form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void layRefundFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Layaway Refund Form navigation action handler received invalid data");
            }

            NavBox navBox = (NavBox)sender;
           
            NavBox.NavAction action = navBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    string custDet = navBox.CustomDetail;
                    if (custDet.Equals("Tender", StringComparison.OrdinalIgnoreCase))
                    {
                        this.nextState = LayawayReturnFlowState.TenderOut;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LayawayReturnFlowState.CancelFlow;
                    break;
                    
            }

            this.executeNextState();
        }


        private void TenderOutFormAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Tender Out form navigation action handler received invalid data");
            }
            NavBox tenderNavBox = (NavBox)sender;
            TenderOut tenderForm = (TenderOut)data;
            NavBox.NavAction action = tenderNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (tenderNavBox.IsCustom)
                    {
                        string custDet = tenderNavBox.CustomDetail;
                        if (custDet.Equals("ProcessTender"))
                        {
                            //call process tender
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                            GlobalDataAccessor.Instance.DesktopSession.showProcessTender(
                                    ProcessTenderProcedures.ProcessTenderMode.LAYPAYMENTREF);
                            this.nextState = LayawayReturnFlowState.CancelFlow;

                        }
                        else
                            this.nextState = LayawayReturnFlowState.CancelFlow;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LayawayReturnFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Tender Out");
            }
            this.executeNextState();

        }



        /// <summary>
        /// 
        /// </summary>
        private void executeNextState()
        {
            object evalExecFlag = this.executorFxn(this.nextState);
            if (evalExecFlag == null || ((bool)(evalExecFlag)) == false)
            {
                throw new ApplicationException("Cannot execute the next state: " + this.nextState.ToString());
            }
        }

        public LayawayReturnFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.nextState = LayawayReturnFlowState.LayawaySearch;
            this.setExecBlock(this.executorFxn);
            this.endStateNotifier = eStateNotifier;
            this.executeNextState();
        }
    }
}
