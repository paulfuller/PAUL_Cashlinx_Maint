using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Retail;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility.Shared;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class ChangeRetailPriceFlowExecutor : SingleExecuteBlock
    {
        public const string NAME = "ChangeRetailPriceFlowExecutor";

        public enum ChangeRetailPriceFlowState
        {
            DescribeItem,
            ItemSearch,
            ExitFlow,
            CancelFlow,
            Error
        }

        private ChangeRetailPriceFlowState nextState;
        private ChangeRetailPriceFlowState suspendedState = ChangeRetailPriceFlowState.Error;
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
            ChangeRetailPriceFlowState inputState = (ChangeRetailPriceFlowState)inputData;

            switch (inputState)
            {
                case ChangeRetailPriceFlowState.ItemSearch:
                    if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail == null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.Sales.Add(new SaleVO());
                        GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = 0;
                    }
                    ChangeRetailPriceSearch itemSearchFrm = new ChangeRetailPriceSearch(GlobalDataAccessor.Instance.DesktopSession);
                    Form currForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(itemSearchFrm);
                    if (currForm.GetType() == typeof(ChangeRetailPriceSearch))
                    {
                        currForm.Show();
                    }
                    else
                    {
                        ShowForm itemSearchBlk = CommonAppBlocks.Instance.CreateChangeRetailPriceSearchShowBlock(this.parentForm, this.itemSearchFormNavAction);
                        if (!itemSearchBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Change Retail Price Search block");
                        }
                    }

                    break;
                case ChangeRetailPriceFlowState.DescribeItem:
                    //Show describe item form as show dialog
                    int iItemIdx = GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex;
                    int iCategoryMask = GlobalDataAccessor.Instance.DesktopSession.CategoryXML.GetCategoryMask(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems[iItemIdx].CategoryCode);
                    var dmPawnItem = new DescribedMerchandise(iCategoryMask);
                    Item pawnItem = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems[iItemIdx];
                    Item.PawnItemMerge(ref pawnItem, dmPawnItem.SelectedPawnItem, true);
                    ((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveRetail).Items.Insert(0, pawnItem);
                    // End GetCat5 populate
                    // Placeholder for ReadOnly DescribedItem.cs

                    GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext = CurrentContext.CHANGE_RETAIL_PRICE;
                    var describeItemBlk = CommonAppBlocks.Instance.DescribeItemChangeRetailPriceBlock(this.parentForm, this.describeItemFormAction, ((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveRetail).Items[0].SelectedProKnowMatch);
                    if (!describeItemBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Item Block");
                    }
                    break;
                case ChangeRetailPriceFlowState.CancelFlow:
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
                case ChangeRetailPriceFlowState.ExitFlow:
                    break;
                default:
                    throw new ApplicationException("Invalid change retail price flow state");
            }

            return (true);
        }

        private void describeItemFormAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Describe Item form navigation action handler received invalid data");
            }

            NavBox descItemNavBox = (NavBox)sender;
            NavBox.NavAction action = descItemNavBox.Action;
            bool backExecuted = false;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                backExecuted = true;
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                //BZ: 899 - Only cancel will get out of this flow, submit is the same as back
                case NavBox.NavAction.SUBMIT:                    
                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                    if (!backExecuted)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                        backExecuted = true;
                    }
                    this.nextState = ChangeRetailPriceFlowState.ItemSearch;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = ChangeRetailPriceFlowState.CancelFlow;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = ChangeRetailPriceFlowState.ItemSearch;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Describe Item");
            }
            this.executeNextState();
        }

        private void itemSearchFormNavAction(object sender, object data)
        {
            NavBox itemSearchNavBox = (NavBox)sender;
            ChangeRetailPriceSearch itemSearchForm = (ChangeRetailPriceSearch)data;
            NavBox.NavAction action = itemSearchNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //Default happy path next state
                    this.parentForm = itemSearchForm;
                    this.nextState = ChangeRetailPriceFlowState.DescribeItem;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = ChangeRetailPriceFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for ItemSearch");
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

        public ChangeRetailPriceFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
        : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = ChangeRetailPriceFlowState.ItemSearch;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }

        public ChangeRetailPriceFlowState NextState
        {
            get
            {
                return (this.nextState);
            }
        }
    }
}
