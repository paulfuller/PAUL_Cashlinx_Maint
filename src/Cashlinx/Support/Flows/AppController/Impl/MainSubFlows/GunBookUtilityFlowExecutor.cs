using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Navigation;
//using Pawn.Forms.GunUtilities.GunBook;
using Pawn.Logic;

namespace Support.Flows.AppController.Impl.MainSubFlows
{
    public class GunBookUtilityFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "GunBookUtilityFlowExecutor";

        public enum GunBookUtilityFlowFlowState
        {
            PRINT,
            CANCEL
        }
        private GunBookUtilityFlowFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;

        public GunBookUtilityFlowExecutor(Form parentForm, FxnBlock eStateNotifier) : base(NAME)
           
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            // this.nextState = LookupReceiptFlowState.LookupReceipt;
             this.setExecBlock(this.executorFxn);
             this.executeNextState();
        }


        private object executorFxn(object inputData)
        {
            if (inputData == null)
                return (false);

            GunBookUtilityFlowFlowState inputState = (GunBookUtilityFlowFlowState)inputData;
            switch (inputState)
            {



                case GunBookUtilityFlowFlowState.PRINT:

                    //ShowForm gunBookUtilites = CommonAppBlocks.Instance.CreateGunBookPrintUtilityBlock(this.parentForm, this.gunBookUtilityFormNavAction);

                    //if (!gunBookUtilites.execute())
                    //{
                    //    throw new ApplicationException("Cannot execute Gun book utilities block");
                    //}
                    break;

                case GunBookUtilityFlowFlowState.CANCEL:
                    //CommonAppBlocks.Instance.HideFlowTabController();

                    //if (this.endStateNotifier != null)
                    //    this.endStateNotifier.execute();
                    break;
            }

            return (true);
        }


        private void executeNextState()
        {
            object evalExecFlag = this.executorFxn(this.nextState);
            if (evalExecFlag == null || ((bool)(evalExecFlag)) == false)
            {
                throw new ApplicationException("Cannot execute the next state: print gunbook");
            }
        }


        private void gunBookUtilityFormNavAction(object sender, object data)
        {
 /*           if (sender == null || data == null)
            {
                throw new ApplicationException("Gun Book Utility navigation action handler received invalid data");
            }

            NavBox gunBookUtiltiyNavBox = (NavBox)sender;
            GunBookPrintOptions gunBookPrintOptions = (GunBookPrintOptions)data;
            NavBox.NavAction gunBookAction = gunBookUtiltiyNavBox.Action;
            if (gunBookAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                gunBookAction = NavBox.NavAction.SUBMIT;
            }
            switch (gunBookAction)
            {
                case NavBox.NavAction.SUBMIT:
                      this.nextState = GunBookUtilityFlowFlowState.PRINT;
                      break;
                case NavBox.NavAction.CANCEL:
                      this.nextState = GunBookUtilityFlowFlowState.CANCEL;
                    break;
                default:
                    throw new ApplicationException("" + gunBookAction.ToString() + " is not a valid state for LookupCustomer");
            }

            this.executeNextState();
*/
        }


    }
}

