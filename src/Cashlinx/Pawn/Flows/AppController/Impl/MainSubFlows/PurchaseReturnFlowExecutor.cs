using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Pawn.Forms.Pawn.Customer;
using Pawn.Forms.Pawn.Services.Pickup;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class PurchaseReturnFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "PurchaseReturnFlowExecutor";
        public enum PurchaseReturnFlowState
        {
            BuyReturn,
            BuyReturnItems,
            CancelFlow,
            ManagePawnApplication,
            FinalGunSubmit,
            ShowVendor,
        }

        private PurchaseReturnFlowState nextState;
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
            {
                return (false);
            }

            PurchaseReturnFlowState inputState = (PurchaseReturnFlowState)inputData;

            switch (inputState)
            {
                case PurchaseReturnFlowState.BuyReturn:
                    ShowForm buyReturnFrmBlk = CommonAppBlocks.Instance.BuyReturnFormBlock(this.parentForm, this.buyReturnFormNavAction);
                    if (!buyReturnFrmBlk.execute())
                    {
                        throw new ApplicationException("Cannot show Buy Return form");
                    }
                    break;
                case PurchaseReturnFlowState.BuyReturnItems:
                    ShowForm buyReturnItemBlk = CommonAppBlocks.Instance.BuyReturnItemsFormBlock(this.parentForm, this.buyReturnItemsFormNavAction);
                    if (!buyReturnItemBlk.execute())
                    {
                        throw new ApplicationException("Cannot show Return Items form");
                    }
 
                    break;
                case PurchaseReturnFlowState.CancelFlow:
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
                    GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
                case PurchaseReturnFlowState.ManagePawnApplication:
                    ShowForm managePawnAppBlk = CommonAppBlocks.Instance.CreateManagePawnApplicationBlock(this.parentForm, this.managePawnAppFormNavAction);
                    if (!managePawnAppBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute ManagePawnApplication block");
                    }
                    break;
                case PurchaseReturnFlowState.FinalGunSubmit:
                    var backgroundcheckFrm = new FirearmsBackgroundCheck();
                        backgroundcheckFrm.ShowDialog(null);

                    if (GlobalDataAccessor.Instance.DesktopSession.BackgroundCheckCompleted)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        GlobalDataAccessor.Instance.DesktopSession.showProcessTender(ProcessTenderProcedures.ProcessTenderMode.RETURNBUY);
                    }
                    break;
                case PurchaseReturnFlowState.ShowVendor:

                    ShowForm createVendBlk = CommonAppBlocks.Instance.CreateCreateVendorBlock(this.parentForm, this.createVendFormNavAction);
                    if (!createVendBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Show Vendor block");
                    }
                    break;
            }


            return (true);
        }

        /// <summary>
        /// Navigation actions for shop cash manager form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void buyReturnFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Buy Return navigation action handler received invalid data");
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
                        this.nextState = PurchaseReturnFlowState.BuyReturnItems;
                    else
                        this.nextState = PurchaseReturnFlowState.CancelFlow;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PurchaseReturnFlowState.CancelFlow;
                    break;
            }

            this.executeNextState();
        }


        /// <summary>
        /// Navigation actions for add cash drawer form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void buyReturnItemsFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Buy Return Items navigation action handler received invalid data");
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
                    if (navBox.IsCustom && navBox.CustomDetail.Equals("ManagePawnAppplication", StringComparison.OrdinalIgnoreCase))
                    {
                        this.nextState = PurchaseReturnFlowState.ManagePawnApplication;
                    }
                    else if (navBox.IsCustom && navBox.CustomDetail.Equals("ShowVendor", StringComparison.OrdinalIgnoreCase))
                    {
                        this.nextState = PurchaseReturnFlowState.ShowVendor;
                    }
                    else
                    {
                        this.nextState = PurchaseReturnFlowState.BuyReturn;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PurchaseReturnFlowState.CancelFlow;
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

        public PurchaseReturnFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.nextState = PurchaseReturnFlowState.BuyReturn;
            this.setExecBlock(this.executorFxn);
            this.endStateNotifier = eStateNotifier;
            this.executeNextState();
        }

        /// <summary>
        /// Action handler for ManagePawnApplication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void managePawnAppFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Manage pawn app form navigation action handler received invalid data");
            }

            NavBox managePawnAppNavBox = (NavBox)sender;
            NavBox.NavAction action = managePawnAppNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                CashlinxDesktopSession.Instance.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    this.nextState = PurchaseReturnFlowState.FinalGunSubmit;
                    break;
                case NavBox.NavAction.CANCEL:
                case NavBox.NavAction.BACK:
                    this.nextState = PurchaseReturnFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for ManagePawnApplication");
            }
            this.executeNextState();
        }

        private void createVendFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Create Vendor form navigation action handler received invalid data");
            }

            NavBox createVendNavBox = (NavBox)sender;
            CreateVendor createVendForm = (CreateVendor)data;
            NavBox.NavAction lookupAction = createVendNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                lookupAction = NavBox.NavAction.SUBMIT;
            }
            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:
                    if (createVendNavBox.IsCustom)
                    {
                        string custDet = createVendNavBox.CustomDetail;
                        //Look for add customer
                        if (custDet.Equals("AddVendorComplete"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                            GlobalDataAccessor.Instance.DesktopSession.showProcessTender(ProcessTenderProcedures.ProcessTenderMode.RETURNBUY);
                            return;
                        }
                    }
                    break;

                case NavBox.NavAction.CANCEL:
                    this.nextState = PurchaseReturnFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for LookupVendor");
            }

            this.executeNextState();
        }
    }
}
