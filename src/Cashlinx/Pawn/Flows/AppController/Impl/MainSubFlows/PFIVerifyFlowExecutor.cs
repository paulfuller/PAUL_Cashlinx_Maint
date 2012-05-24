using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms.Pawn.Products.DescribeMerchandise;
using Pawn.Forms.Pawn.Services.PFI;
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class PFIVerifyFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "PFIVerifyFlowExecutor";
        public enum PFIVerifyFlowState
        {
            PfiVerify,
            DescribeMerchandise,
            DescribeMerchandisePFIMerge,
            DescribeMerchandisePFIAdd,
            DescribeMerchandisePFIReplace,
            DescribeItem,
            DescribeItemPFI,
            CancelFlow
        }

        private PFIVerifyFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;

        /// <summary>
        /// Main execution function for PFIVerify Flow Executor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
                return (false);
            PFIVerifyFlowState inputState = (PFIVerifyFlowState)inputData;

            switch (inputState)
            {
                case PFIVerifyFlowState.PfiVerify:

                    if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup("PFI_Verify"))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.VisibleForm("PFI_Verify");
                    }
                    else
                    {
                        ShowForm pfiVerifyBlk = CommonAppBlocks.Instance.PFIVerifyBlock(this.parentForm, this.PFIVerifyFormNavFunction);
                        if (!pfiVerifyBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute PFI Verify block");
                        }
                    }
                    break;
                case PFIVerifyFlowState.DescribeMerchandise:
                    ShowForm describeMerchBlk = CommonAppBlocks.Instance.DescribeMerchandiseBlock(this.parentForm, this.describeMerchFormAction);
                    if (!describeMerchBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Merchandise block");
                    }

                    break;
                case PFIVerifyFlowState.DescribeMerchandisePFIMerge:
                    ShowForm describeMerchandiseBlk = CommonAppBlocks.Instance.DescribeMerchandisePFIMergeBlock(this.parentForm, this.describeMerchFormAction);
                    if (!describeMerchandiseBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Merchandise PFI Merge block");
                    }

                    break;
                case PFIVerifyFlowState.DescribeMerchandisePFIAdd:
                    ShowForm describeMerchandiseAddBlk = CommonAppBlocks.Instance.DescribeMerchandisePFIAddBlock(this.parentForm, this.describeMerchFormAction);
                    if (!describeMerchandiseAddBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Merchandise PFI Add block");
                    }

                    break;
                case PFIVerifyFlowState.DescribeMerchandisePFIReplace:
                    ShowForm describeMerchandiseReplaceBlk = CommonAppBlocks.Instance.DescribeMerchandisePFIReplaceBlock(this.parentForm, this.describeMerchFormAction);
                    if (!describeMerchandiseReplaceBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Merchandise PFI Replace block");
                    }

                    break;

                case PFIVerifyFlowState.DescribeItem:
                    ShowForm describeItemBlk = CommonAppBlocks.Instance.DescribeItemBlock(this.parentForm, this.describeItemFormAction);
                    if (!describeItemBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Item Block");
                    }
                    break;
                case PFIVerifyFlowState.DescribeItemPFI:
                    ShowForm describeItemPFIBlk = CommonAppBlocks.Instance.DescribeItemPFIBlock(this.parentForm, this.describeItemFormAction);
                    if (!describeItemPFIBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Item PFI Block");
                    }
                    break;

                case PFIVerifyFlowState.CancelFlow:
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;


            }


            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void PFIVerifyFormNavFunction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("PFI Verify form navigation action handler received invalid data");
            }

            NavBox navBox = (NavBox)sender;
            PFI_Verify pfiverifyFm = (PFI_Verify)data;
            NavBox.NavAction action = navBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (navBox.IsCustom)
                    {
                        string custDet = navBox.CustomDetail;
                        if (custDet.Equals("DescribeMerchandisePFIMerge", StringComparison.OrdinalIgnoreCase))
                        {
                            this.nextState = PFIVerifyFlowState.DescribeMerchandisePFIMerge;
                        }
                        else if (custDet.Equals("DescribeMerchandisePFIAdd", StringComparison.OrdinalIgnoreCase))
                        {
                            pfiverifyFm.Hide();
                            this.nextState = PFIVerifyFlowState.DescribeMerchandisePFIAdd;
                        }
                        else if (custDet.Equals("DescribeItem", StringComparison.OrdinalIgnoreCase))
                        {
                            this.nextState = PFIVerifyFlowState.DescribeItem;
                        }
                        else if (custDet.Equals("DescribeItemPFIReDescribe", StringComparison.OrdinalIgnoreCase))
                        {
                            this.nextState = PFIVerifyFlowState.DescribeItemPFI;
                        }
                    }
                    break;
                case NavBox.NavAction.BACK:
                    this.nextState = PFIVerifyFlowState.CancelFlow;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PFIVerifyFlowState.CancelFlow;
                    break;

            }

            this.executeNextState();
        }

        /// <summary>
        /// The various form actions for the describe merchandise form in the new pawn loan flow
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
            DescribeMerchandise descMerchForm = (DescribeMerchandise)data;
            NavBox.NavAction action = descMerchNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (descMerchNavBox.IsCustom)
                    {
                        string custDet = descMerchNavBox.CustomDetail;
                        if (custDet.Equals("DescribeItemPFI", StringComparison.OrdinalIgnoreCase))
                            this.nextState = PFIVerifyFlowState.DescribeItemPFI;
                        else
                            this.nextState = PFIVerifyFlowState.DescribeItem;
                    }
                    else
                        this.nextState = PFIVerifyFlowState.DescribeItem;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PFIVerifyFlowState.CancelFlow;
                    break;
                case NavBox.NavAction.BACK:
                    if (descMerchNavBox.IsCustom)
                    {
                        string custdet = descMerchNavBox.CustomDetail;
                        if (custdet.Equals("PFIReplace", StringComparison.OrdinalIgnoreCase))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                            this.nextState = PFIVerifyFlowState.DescribeItemPFI;
                        }
                        else
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                            this.nextState = PFIVerifyFlowState.PfiVerify;

                        }
                    }
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
            DescribeItem descItemForm = (DescribeItem)data;
            NavBox.NavAction action = descItemNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (descItemNavBox.IsCustom)
                    {
                        string custDet = descItemNavBox.CustomDetail;
                        if (custDet.Equals("DescribeMerchandisePFIReplace", StringComparison.OrdinalIgnoreCase))
                        {
                            this.nextState = PFIVerifyFlowState.DescribeMerchandisePFIReplace;
                        }
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PFIVerifyFlowState.CancelFlow;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    if (descItemNavBox.IsCustom)
                    {
                        string custDet = descItemNavBox.CustomDetail;
                        if (custDet.Equals("Back", StringComparison.OrdinalIgnoreCase))
                            this.nextState = PFIVerifyFlowState.DescribeMerchandise;
                        else if (custDet.Equals("BackPFIAdd", StringComparison.OrdinalIgnoreCase))
                            this.nextState = PFIVerifyFlowState.DescribeMerchandisePFIAdd;
                    }
                    else
                        this.nextState = PFIVerifyFlowState.PfiVerify;

                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Describe Item");
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

        public PFIVerifyFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = PFIVerifyFlowState.PfiVerify;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }
    }
}
