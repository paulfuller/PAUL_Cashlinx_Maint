using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Pawn.Forms.ReleaseFingerprints;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class ReleaseFingerprintsFlowExecutor : SingleExecuteBlock
    {
        public const string NAME = "ReleaseFingerprintsFlowExecutor";

        public enum ReleaseFingerprintsFlowExecutorFlowState
        {
            BuyLoanLookup,
            ShowBuyLoanLookupResults,
            Exit,
            Cancel,
            Error
        }

        private ReleaseFingerprintsFlowExecutorFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;

        public ReleaseFingerprintsFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = ReleaseFingerprintsFlowExecutorFlowState.BuyLoanLookup;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }

        /// <summary>
        /// Main execution function for ReleaseFingerprintsFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
                return (false);
            ReleaseFingerprintsFlowExecutorFlowState inputState = (ReleaseFingerprintsFlowExecutorFlowState)inputData;

            switch (inputState)
            {
                case ReleaseFingerprintsFlowExecutorFlowState.BuyLoanLookup:
                    ShowForm lookupLoanBuyBlk = CommonAppBlocks.Instance.CreateLookupLoanBuyShowBlock(this.parentForm, this.lookupLoanBuyFormNavAction);
                    if (!lookupLoanBuyBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute BuyLoanLookup block");
                    }
                    break;

                case ReleaseFingerprintsFlowExecutorFlowState.ShowBuyLoanLookupResults:
                    ShowForm lookupLoanBuyResultsBlk = CommonAppBlocks.Instance.CreateReleaseFingerprintsAuthorizationBlock(this.parentForm, this.lookupLoanBuyFormNavAction);
                    if (!lookupLoanBuyResultsBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute BuyLoanLookupResults block");
                    }
                    break;

                case ReleaseFingerprintsFlowExecutorFlowState.Cancel:
                    //Clear the customer from session
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();

                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;


                default:
                    throw new ApplicationException("Invalid Release Fingerprints flow state");
            }

            return (true);
        }

        /// <summary>
        /// NavBox OnAction Handler for Lookup Customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void lookupLoanBuyFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Lookup loan/buy form navigation action handler received invalid data");
            }

            NavBox lookupLoanBuyNavBox = (NavBox)sender;

            NavBox.NavAction lookupAction = lookupLoanBuyNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                lookupAction = NavBox.NavAction.BACK;
            }
            switch (lookupAction)
            {
                case NavBox.NavAction.CANCEL:
                    this.nextState = ReleaseFingerprintsFlowExecutorFlowState.Cancel;
                    break;

                case NavBox.NavAction.SUBMIT:
                    this.nextState = ReleaseFingerprintsFlowExecutorFlowState.ShowBuyLoanLookupResults;
                    break;

                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = ReleaseFingerprintsFlowExecutorFlowState.BuyLoanLookup;
                    break;

                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for LookupLoanBuy");
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
    }
}
