using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class ShopCashManagementFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "ShopCashManagementFlowExecutor";
        public enum ShopCashManagementFlowState
        {
            ViewCashDrawerAssignments,
            ShopCashManagement,
            AddCashDrawer,
            ExitFlow,
            CancelFlow
        }

        private ShopCashManagementFlowState nextState;
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
            ShopCashManagementFlowState inputState = (ShopCashManagementFlowState)inputData;

            switch (inputState)
            {
                case ShopCashManagementFlowState.ViewCashDrawerAssignments:
                    ShowForm viewCashDrawerFmBlk = CommonAppBlocks.Instance.CreateViewCashDrawerAssignmentsBlock(this.parentForm, this.viewCashDrawerNavFunction);
                    if (!viewCashDrawerFmBlk.execute())
                    {
                        throw new ApplicationException("Cannot show View Cash Drawer Assignment form");
                    }
                    break;
                case ShopCashManagementFlowState.ShopCashManagement:
                    ShowForm shopCashFmBlk = CommonAppBlocks.Instance.CreateShopCashManagementForm(this.parentForm, this.shopCashManagementNavFunction);
                    if (!shopCashFmBlk.execute())
                    {
                        throw new ApplicationException("Cannot show ShopCashMgr form");
                    }
 
                    break;
                case ShopCashManagementFlowState.AddCashDrawer:
                    ShowForm addCashDrawerFrmBlk = CommonAppBlocks.Instance.CreateAddCashDrawerShowBlock(this.parentForm, this.addCashDrawerNavFunction);
                    if (!addCashDrawerFrmBlk.execute())
                    {
                        throw new ApplicationException("Cannot show Add Cash Drawer form");
                    }
                    break;
                case ShopCashManagementFlowState.CancelFlow:
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
 
 
            }


            return (true);
        }

        /// <summary>
        /// Navigation actions for shop cash manager form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void shopCashManagementNavFunction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Shop Cash Management navigation action handler received invalid data");
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
                    if (navBox.IsCustom && navBox.CustomDetail.Equals("ADDCASHDRAWER", StringComparison.OrdinalIgnoreCase))
                        this.nextState = ShopCashManagementFlowState.AddCashDrawer;
                    else if (navBox.IsCustom && navBox.CustomDetail.Equals("RELOAD", StringComparison.OrdinalIgnoreCase))
                        this.nextState = ShopCashManagementFlowState.ShopCashManagement;
                    else if (navBox.IsCustom && navBox.CustomDetail.Equals("VIEWASSIGNMENTS", StringComparison.OrdinalIgnoreCase))
                        this.nextState = ShopCashManagementFlowState.ViewCashDrawerAssignments;
                    else
                        this.nextState = ShopCashManagementFlowState.ExitFlow;
                    break;

                case NavBox.NavAction.CANCEL:
                    this.nextState = ShopCashManagementFlowState.CancelFlow;
                    break;
            }

            this.executeNextState();
        }


        /// <summary>
        /// Navigation actions for add cash drawer form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void addCashDrawerNavFunction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Add Cash Drawer navigation action handler received invalid data");
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
                    this.nextState = ShopCashManagementFlowState.ShopCashManagement;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = ShopCashManagementFlowState.CancelFlow;
                    break;
            }

            this.executeNextState();
        }


        private void viewCashDrawerNavFunction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("View Cash Drawer navigation action handler received invalid data");
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
                    this.nextState = ShopCashManagementFlowState.ShopCashManagement;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = ShopCashManagementFlowState.CancelFlow;
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

        public ShopCashManagementFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.nextState = ShopCashManagementFlowState.ViewCashDrawerAssignments;
            this.setExecBlock(this.executorFxn);
            this.endStateNotifier = eStateNotifier;
            this.executeNextState();
        }
    }
}
