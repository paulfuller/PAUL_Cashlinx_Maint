using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;
using Pawn.Flows.AppController.Impl.Common;
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class MMPIFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "MMPIFlowExecutor";

        public enum MMPIFlowState
        {
            DescribeMerchandise,
            DescribeItem,
            ManageMultiplePawnItems,
            ExitFlowToItemHistory,
            ExitFlowToProductHistory,
            ExitFlowToLookupCustomer,
            ExitFlowToViewCustomer,
            ExitFlowToVendor,
            ExitFlowToStats,
            CancelFlow,
            Error
        }

        private MMPIFlowState nextState;
        private Form parentForm;
        private SingleExecuteBlock parentFlow;
        private FxnBlock endStateNotifier;
        private string parentFlowName;

        /// <summary>
        /// Main execution function for NewPawnLoanFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
                return (false);
            MMPIFlowState inputState = (MMPIFlowState)inputData;
            if (parentFlow != null)
            {
                parentFlowName = parentFlow.Name;
            }
            if (inputState == MMPIFlowState.DescribeItem)
            {
                //check if activepawnloan is null or activepurchase is null
                if (parentFlowName.Equals("CustomerPurchaseFlowExecutor", StringComparison.OrdinalIgnoreCase))
                {
                    IsBuyMerchandiseLookedUp lookupFxn = CommonAppBlocks.Instance.IsBuyMerchandiseLookedUpBlock;
                    lookupFxn.execute();

                    //check if customer is looked up
                    IsCustomerLookedUp lookupCustFxn = CommonAppBlocks.Instance.IsCustomerLookedUpBlock;
                    lookupCustFxn.execute();

                    if (lookupFxn.Value == false)
                    {
                        inputState = MMPIFlowState.DescribeMerchandise;
                    }
                    //both merchandise and customer looked up and its describe item flow in customer purchase
                    else if (lookupFxn.Value && lookupCustFxn.Value &&
                             GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.DESCRIBEITEMCUSTOMERPURCHASE, StringComparison.OrdinalIgnoreCase) &&
                             GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext != CurrentContext.EDIT_MMP)
                    {
                        if (GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext == CurrentContext.NEW &&
                            (GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex >= 0 || GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.ProductDataComplete))
                        {
                            List<Item> items = GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items;
                            if (!(string.IsNullOrEmpty(items[items.Count - 1].TicketDescription)))
                                inputState = MMPIFlowState.ManageMultiplePawnItems;
                        }
                    }
                }
                else if (parentFlowName.Equals("VendorPurchaseFlowExecutor", StringComparison.OrdinalIgnoreCase))
                {
                    IsBuyMerchandiseLookedUp lookupFxn = CommonAppBlocks.Instance.IsBuyMerchandiseLookedUpBlock;
                    lookupFxn.execute();

                    if (lookupFxn.Value == false)
                    {
                        inputState = MMPIFlowState.DescribeMerchandise;
                    }
                    else if (!string.IsNullOrEmpty(CashlinxDesktopSession.Instance.ActivePurchase.PurchaseOrderNumber))
                        inputState = MMPIFlowState.ManageMultiplePawnItems;
                }
                else if (parentFlowName.Equals("NewPawnLoanFlowExecutor", StringComparison.OrdinalIgnoreCase) ||
                         GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.DESCRIBEMERCHANDISE, StringComparison.OrdinalIgnoreCase))
                {
                    IsLoanMerchandiseLookedUp lookupFxn = CommonAppBlocks.Instance.IsLoanMerchandiseLookedUpBlock;
                    lookupFxn.execute();

                    if (lookupFxn.Value == false)
                    {
                        inputState = MMPIFlowState.DescribeMerchandise;
                    }
                    else if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].mStore == 0)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = 0;
                        inputState = MMPIFlowState.DescribeItem;
                    }
                    else 
                    {
                        //check if customer is looked up
                        IsCustomerLookedUp lookupCustFxn = CommonAppBlocks.Instance.IsCustomerLookedUpBlock;
                        lookupCustFxn.execute();

                        if (lookupFxn.Value && lookupCustFxn.Value &&
                            GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext != CurrentContext.EDIT_MMP)
                        {
                            if (GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext == CurrentContext.NEW && GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex == -1 || GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.ProductDataComplete)
                            {
                                List<Item> items = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items;
                                if (!(string.IsNullOrEmpty(items[items.Count - 1].TicketDescription)))
                                    inputState = MMPIFlowState.ManageMultiplePawnItems;
                            }
                            else if (GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext == CurrentContext.NEW &&
                                (GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex >= 0 ))
                            {
                                inputState = MMPIFlowState.DescribeItem;
                            }
                        }
                    }
                }
                else
                {
                    if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan == null ||
                        GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items == null ||
                        GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Count == 0)
                        inputState = MMPIFlowState.DescribeMerchandise;
                }
            }
            

            switch (inputState)
            {
                case MMPIFlowState.DescribeMerchandise:
                    ShowForm describeMerchBlk = CommonAppBlocks.Instance.DescribeMerchandiseBlock(this.parentForm, this.describeMerchFormAction);
                    if (!describeMerchBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Merchandise block");
                    }
                    
                    if (!(parentFlowName.Equals("CustomerPurchaseFlowExecutor", StringComparison.OrdinalIgnoreCase)) &&
                        !(parentFlowName.Equals("VendorPurchaseFlowExecutor", StringComparison.OrdinalIgnoreCase)) && 
                        (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null && !String.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber)))
                        SetTabsInForm(describeMerchBlk);
                    break;
                case MMPIFlowState.DescribeItem:
                    ShowForm describeItemBlk = CommonAppBlocks.Instance.DescribeItemBlock(this.parentForm, this.describeItemFormAction);
                    if (!describeItemBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Item Block");
                    }
                    //show tabs only if its not new pawn loan flow that called this child flow
                    if (!(parentFlowName.Equals("CustomerPurchaseFlowExecutor", StringComparison.OrdinalIgnoreCase)) &&
                        !(parentFlowName.Equals("VendorPurchaseFlowExecutor", StringComparison.OrdinalIgnoreCase)) && 
                        (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null && !String.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber)))
                    {
                        //Check if Manage multiple pawn items form is still there
                        //If it is, no need to show the tabs
                        if (!(GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup("ManageMultiplePawnItems")))
                            SetTabsInForm(describeItemBlk);
                    }
                    break;
                case MMPIFlowState.ManageMultiplePawnItems:
                    if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup("ManageMultiplePawnItems"))
                    {
                        //Remove the MMPI form and reload
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    }

                    ShowForm mmpiBlk = CommonAppBlocks.Instance.ManageMultiplePawnItemsBlock(this.parentForm, this.multiplePawnItemsFormAction);
                    if (!mmpiBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Manage Multiple Pawn Items Block");
                    }
                    if (!(parentFlowName.Equals("CustomerPurchaseFlowExecutor", StringComparison.OrdinalIgnoreCase)) &&
                        !(parentFlowName.Equals("VendorPurchaseFlowExecutor", StringComparison.OrdinalIgnoreCase)) &&
                        (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null && !String.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber)))
                        SetTabsInForm(mmpiBlk);
                    break;
                case MMPIFlowState.CancelFlow:
                    CommonAppBlocks.Instance.HideFlowTabController();
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
                    
                case MMPIFlowState.ExitFlowToItemHistory:
                    if (parentFlow != null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.ItemHistory;
                        this.parentFlow.execute();
                    }
                    break;
                case MMPIFlowState.ExitFlowToProductHistory:
                    if (parentFlow != null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.ProductHistory;
                        this.parentFlow.execute();
                    }
                    break;
                case MMPIFlowState.ExitFlowToViewCustomer:
                    if (parentFlow != null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.Customer;
                        this.parentFlow.execute();
                    }
                    break;
                case MMPIFlowState.ExitFlowToLookupCustomer:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    if (parentFlow != null)
                    {
                        this.parentFlow.execute();
                    }
 
                    break;
                case MMPIFlowState.ExitFlowToVendor:
                    CashlinxDesktopSession.Instance.HistorySession.Back();
                    if (parentFlow != null)
                    {
                        this.parentFlow.execute();
                    }
                    break;
                case MMPIFlowState.ExitFlowToStats:
                    if (parentFlow != null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.Stats;
                        this.parentFlow.execute();
                    }
                    break;

                default:
                    throw new ApplicationException("Invalid MMPI flow state");
            }

            return (true);
        }

        private void SetTabsInForm(ShowForm blockName)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.FormsInTree() > 1)
            {
                if (parentFlow.Name.Equals("NewPawnLoanFlowExecutor", StringComparison.OrdinalIgnoreCase))
                {
                    CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, blockName.ClassForm,
                                                                   FlowTabController.State.ProductsAndServices);
                }
                else
                    CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, blockName.ClassForm,
                                                                   FlowTabController.State.Customer);
                if (GlobalDataAccessor.Instance.DesktopSession.ShowOnlyHistoryTabs)
                {
                    CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Customer, false);
                    CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductsAndServices,
                                                               false);
                    CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Stats, false);
                    CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ItemHistory, true);
                    CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductHistory, true);
                }
                else
                {
                    CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Customer, true);
                    CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductsAndServices,
                                                               true);
                    CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Stats, true);
                    CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ItemHistory, true);
                    CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductHistory, true);
                }
                if (parentFlow.Name.Equals("NewPawnLoanFlowExecutor", StringComparison.OrdinalIgnoreCase))
                {
                    CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Customer, true);
                    CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductsAndServices,
                                                               false);
                    //Get the loan keys for the selected customer
                    //
                    LoadCustomerLoanKeys loanKeysDataBlk = new LoadCustomerLoanKeys();
                    if (!loanKeysDataBlk.execute())
                    {
                        throw new ApplicationException("Cannot get Loan keys for selected customer");
                    }
                }
            }
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
                        if (custDet.Equals("ItemHistory"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.ItemHistory;
                            this.nextState = MMPIFlowState.ExitFlowToItemHistory;
                        }
                        else if (custDet.Equals("ProductHistory"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.ProductHistory;
                            this.nextState = MMPIFlowState.ExitFlowToProductHistory;
                        }
                        else if (custDet.Equals("LookupCustomer") || custDet.Equals("Customer"))
                        {
                            this.nextState = MMPIFlowState.ExitFlowToLookupCustomer;
                            if (custDet.Equals("Customer"))
                                GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.Customer;
                        }
                        else if (custDet.Equals("ProductStats"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.Stats;
                            this.nextState = MMPIFlowState.ExitFlowToStats;
                        }
                        else if (custDet.Equals("CloseTabs"))
                        {
                            CommonAppBlocks.Instance.HideFlowTabController();
                            return;
                        }
                        else
                            this.nextState = MMPIFlowState.DescribeItem;
                    }
                    else
                        this.nextState = MMPIFlowState.DescribeItem;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = MMPIFlowState.CancelFlow;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    //If we are clicking back from desc merchandise afetr having clicked
                    //on add item in MMPI go back to that
                    if (parentFlowName.Equals("CustomerPurchaseFlowExecutor", StringComparison.OrdinalIgnoreCase) || parentFlowName.Equals("VendorPurchaseFlowExecutor", StringComparison.OrdinalIgnoreCase))
                    {
                        if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase != null &&
                            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items.Count > 0)
                            this.nextState = MMPIFlowState.ManageMultiplePawnItems;
                        else
                            this.nextState = MMPIFlowState.CancelFlow;
                        break;
                    }
                    if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan != null &&
                        GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Count > 0)
                        this.nextState = MMPIFlowState.ManageMultiplePawnItems;
                    else
                        this.nextState = MMPIFlowState.CancelFlow;
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
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (descItemNavBox.IsCustom)
                    {
                        string custDet = descItemNavBox.CustomDetail;
                        if (custDet.Equals("ItemHistory"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.ItemHistory;
                            this.nextState = MMPIFlowState.ExitFlowToItemHistory;
                        }
                        else if (custDet.Equals("ProductHistory"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.ProductHistory;
                            this.nextState = MMPIFlowState.ExitFlowToProductHistory;
                        }
                        else if (custDet.Equals("Customer"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.Customer;
                            this.nextState = MMPIFlowState.ExitFlowToLookupCustomer;
                        }
                        else if (custDet.Equals("ProductStats"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.Stats;
                            this.nextState = MMPIFlowState.ExitFlowToStats;
                        }
                        else if (custDet.Equals("CloseTabs"))
                        {
                            CommonAppBlocks.Instance.HideFlowTabController();
                            return;
                        }
                        else
                            this.nextState = MMPIFlowState.ManageMultiplePawnItems;
                    }
                    else
                        this.nextState = MMPIFlowState.ManageMultiplePawnItems;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = MMPIFlowState.CancelFlow;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    if (this.parentFlow.Name.Equals("NewPawnLoanFlowExecutor", StringComparison.OrdinalIgnoreCase) ||
                        this.parentFlow.Name.Equals("CustomerPurchaseFlowExecutor", StringComparison.OrdinalIgnoreCase) ||
                        this.parentFlow.Name.Equals("VendorPurchaseFlowExecutor", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!(GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup("ManageMultiplePawnItems")))
                            this.nextState = MMPIFlowState.DescribeMerchandise;

                        else
                            this.nextState = MMPIFlowState.ManageMultiplePawnItems;
                    }
                    else 
                    {
                        if (!(GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup("DescribeMerchandise")))
                            this.nextState = MMPIFlowState.ManageMultiplePawnItems;

                        else
                            this.nextState = MMPIFlowState.DescribeMerchandise;
                    }
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Describe Item");
            }
            this.executeNextState();
        }

        private void multiplePawnItemsFormAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Manage Multiple Pawn Items form navigation action handler received invalid data");
            }

            NavBox mmpiNavBox = (NavBox)sender;
            NavBox.NavAction action = mmpiNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (mmpiNavBox.IsCustom)
                    {
                        string custDet = mmpiNavBox.CustomDetail;
                        if (custDet.Equals("DescribeMerchandise"))
                            this.nextState = MMPIFlowState.DescribeMerchandise;
                        else if (custDet.Equals("DescribeItem"))
                            this.nextState = MMPIFlowState.DescribeItem;
                        else if (custDet.Equals("ItemHistory"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.ItemHistory;
                            this.nextState = MMPIFlowState.ExitFlowToItemHistory;
                        }
                        else if (custDet.Equals("ProductHistory"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.ProductHistory;
                            this.nextState = MMPIFlowState.ExitFlowToProductHistory;
                        }
                        else if (custDet.Equals("Customer"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.Customer;
                            this.nextState = MMPIFlowState.ExitFlowToLookupCustomer;
                        }
                        else if (custDet.Equals("ProductStats"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.Stats;
                            this.nextState = MMPIFlowState.ExitFlowToStats;
                        }
                        else if (custDet.Equals("LookupCustomer"))
                            this.nextState = MMPIFlowState.ExitFlowToLookupCustomer;
                        else if (custDet.Equals("ShowVendor"))
                            this.nextState = MMPIFlowState.ExitFlowToVendor;
                        else if (custDet.Equals("CloseTabs"))
                        {
                            CommonAppBlocks.Instance.HideFlowTabController();
                            return;
                        }
                    }
                    
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = MMPIFlowState.CancelFlow;
                    
                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Manage Multiple Pawn Items");
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

        public MMPIFlowExecutor(Form parentForm, FxnBlock eStateNotifier, SingleExecuteBlock parentFlow)
        : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.parentFlow = parentFlow;
            this.nextState = MMPIFlowState.DescribeItem;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }

        public MMPIFlowState NextState
        {
            get
            {
                return (this.nextState);
            }
        }
    }
}
