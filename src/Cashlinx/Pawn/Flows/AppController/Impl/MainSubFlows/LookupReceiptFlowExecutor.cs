using System;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Pawn.Forms.Pawn.Services.Receipt;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class LookupReceiptFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "LookupReceiptFlowExecutor";
        public enum LookupReceiptFlowState
        {
            LookupReceipt,
            ViewReceipt,
            ExitFlow,
            CancelFlow,
            Error
        }

        private LookupReceiptFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;

        /// <summary>
        /// Main execution function for NewPawnLoanFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
                return (false);

            LookupReceiptFlowState curState = (LookupReceiptFlowState)inputData;
            switch (curState)
            {
                case LookupReceiptFlowState.LookupReceipt:
                    ShowForm lookupReceiptBlk = 
                        CommonAppBlocks.Instance.CreateLookupReceiptBlock(
                        this.parentForm, this.lookupReceiptFormNavAction);
                    if (!lookupReceiptBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute lookup receipt block");
                    }
                    break;
                case LookupReceiptFlowState.ViewReceipt:
                    ShowForm viewReceiptBlk =
                        CommonAppBlocks.Instance.CreateViewReceiptBlock(
                        this.parentForm, this.viewReceiptFormNavAction);
                    if (!viewReceiptBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute view receipt block");
                    }
                    
                    break;
                case LookupReceiptFlowState.CancelFlow:
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
                case LookupReceiptFlowState.Error:
                    break;
                case LookupReceiptFlowState.ExitFlow:
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
                default:
                    throw new ApplicationException("Invalid flow state for LookupReceiptFlowExecutor");
            }

            return (true);
        }

        /// <summary>
        /// NavBox OnAction Handler for Lookup Receipt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void lookupReceiptFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Lookup receipt form navigation action handler received invalid data");
            }

            NavBox lookupReceiptNavBox = (NavBox)sender;
            LookupReceipt lookupReceiptForm = (LookupReceipt)data;
            NavBox.NavAction lookupAction = lookupReceiptNavBox.Action;

            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:
                    this.nextState = LookupReceiptFlowState.ViewReceipt;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupReceiptFlowState.ExitFlow;
                    break;
                default:
                    throw new ApplicationException(string.Format("{0} is not a valid state for LookupReceipt", lookupAction));
            }

            this.executeNextState();
        }

        /// <summary>
        /// NavBox OnAction Handler for View Receipt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void viewReceiptFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Lookup receipt form navigation action handler received invalid data");
            }

            NavBox viewReceiptNavBox = (NavBox)sender;
            ViewReceipt viewReceiptForm = (ViewReceipt)data;
            NavBox.NavAction viewAction = viewReceiptNavBox.Action;

            switch (viewAction)
            {
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupReceiptFlowState.ExitFlow;
                    break;
                case NavBox.NavAction.SUBMIT:
                    this.nextState = LookupReceiptFlowState.ExitFlow;
                    break;
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

        public LookupReceiptFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = LookupReceiptFlowState.LookupReceipt;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }

        public LookupReceiptFlowState NextState
        {
            get
            {
                return (this.nextState);
            }
        }
    }
}
