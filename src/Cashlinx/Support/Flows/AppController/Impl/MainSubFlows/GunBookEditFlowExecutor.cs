using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Support.Flows.AppController.Impl;
using Support.Forms.Customer;
using Support.Forms.Pawn.Customer;
using Pawn.Logic;
using Support.Logic;

namespace Support.Flows.AppController.Impl.MainSubFlows
{
    public class GunBookEditFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "GunBookEditFlowExecutor";

        public enum GunBookEditFlowState
        {
            CustomerReplace,
            EditGunBookRecord,
            GunBookSearch,
            LookupCustomer,
            LookupCustomerResults,
            DescribeMerchandise,
            DescribeItem,
            DescribeItemEdit,
            Exit,
            Cancel,
            Error,
            ExitFlow
        }

        private GunBookEditFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;
        private SingleExecuteBlock parentFlow;

        public GunBookEditFlowExecutor(Form parentForm, FxnBlock eStateNotifier )
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = GunBookEditFlowState.GunBookSearch;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }
        /// <summary>
        /// Main execution function for CustomerHoldFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
                return (false);
            GunBookEditFlowState inputState = (GunBookEditFlowState)inputData;

            switch (inputState)
            {
                case GunBookEditFlowState.LookupCustomer:
                    ShowForm lookupCustBlk = CommonAppBlocks.Instance.LookupCustomerShowBlock(this.parentForm, this.lookupCustFormNavAction);
                    if (!lookupCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomer block");
                    }

                    break;
                case GunBookEditFlowState.LookupCustomerResults:
                    ShowForm lookupCustResBlk = CommonAppBlocks.Instance.LookupCustomerResultsBlock(this.parentForm, this.lookupCustResultsFormNavAction);
                    if (!lookupCustResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomerResults block");
                    }
                    break;
                case GunBookEditFlowState.DescribeMerchandise:

                    ShowForm descMdseBlk = CommonAppBlocks.Instance.DescribeMerchandiseGunEditBlock(this.parentForm, this.describeMerchFormAction);
                    if (!descMdseBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Merchandise block");
                    }

                    break;
                case GunBookEditFlowState.DescribeItem:
                    ShowForm descItemBlk = CommonAppBlocks.Instance.DescribeItemBlock(this.parentForm, this.describeItemFormAction);
                    if (!descItemBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Item block");
                    }

                    break;
                case GunBookEditFlowState.DescribeItemEdit:
                    ShowForm describeItemEditBlk = CommonAppBlocks.Instance.DescribeItemGunEditBlock(this.parentForm, this.describeItemFormAction);
                    if (!describeItemEditBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Item PFI Block");
                    }
                    break;

                case GunBookEditFlowState.GunBookSearch:
                    ShowForm gunBookSearchBlk = CommonAppBlocks.Instance.GunBookSearchFormBlock(this.parentForm, this.gunBookSearchFormNavAction);
                    if (!gunBookSearchBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Gun Book Edit Search block");
                    }
                    break;
                case GunBookEditFlowState.EditGunBookRecord:
                    ShowForm editGunBookrecordBlk = CommonAppBlocks.Instance.GunBookEditFormBlock(this.parentForm, this.gunBookEditFormNavAction);
                    if (!editGunBookrecordBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Gun Book Edit block");
                    }
                    break;
                case GunBookEditFlowState.CustomerReplace:
                    ShowForm custReplaceBlk = CommonAppBlocks.Instance.CustomerReplaceBlock(this.parentForm, this.customerReplaceFormNavAction);
                    if (!custReplaceBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute customer replace block");
                    }
                    break;


                case GunBookEditFlowState.Cancel:
                    //Clear the customer from session
                    CashlinxPawnSupportSession.Instance.ClearCustomerList();

                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;


                default:
                    throw new ApplicationException("Invalid Gun Book Edit flow state");
            }

            return (true);
        }



        /// <summary>
        /// NavBox OnAction Handler for Lookup Customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void lookupCustFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Lookup customer form navigation action handler received invalid data");
            }

            NavBox lookupCustNavBox = (NavBox)sender;
            LookupCustomer lookupCustForm = (LookupCustomer)data;
            NavBox.NavAction lookupAction = lookupCustNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                CashlinxPawnSupportSession.Instance.HistorySession.Back();
                lookupAction = NavBox.NavAction.SUBMIT;
            }
            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:
                    if (lookupCustNavBox.IsCustom)
                    {
                        string custDet = lookupCustNavBox.CustomDetail;
                        if (custDet.Equals("CreateCustomer", StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("Not a valid selection in this flow");
                        }
                        else
                            this.nextState = GunBookEditFlowState.LookupCustomerResults;
                    }
                    else
                        this.nextState = GunBookEditFlowState.LookupCustomerResults;
                    break;
                case NavBox.NavAction.CANCEL:
                    CashlinxPawnSupportSession.Instance.HistorySession.Back();
                    this.nextState = GunBookEditFlowState.EditGunBookRecord;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for LookupCustomer");
            }

            this.executeNextState();
        }

        /// <summary>
        /// Action class for LookupCustomerResults
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void lookupCustResultsFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Lookup customer Results form navigation action handler received invalid data");
            }

            NavBox lookupCustResNavBox = (NavBox)sender;
            LookupCustomerResults lookupCustResForm = (LookupCustomerResults)data;
            NavBox.NavAction action = lookupCustResNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                CashlinxPawnSupportSession.Instance.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    this.nextState = GunBookEditFlowState.CustomerReplace;
                    break;
                case NavBox.NavAction.BACK:
                    CashlinxPawnSupportSession.Instance.HistorySession.Back();
                    this.nextState = GunBookEditFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    CashlinxPawnSupportSession.Instance.HistorySession.Back();
                    this.nextState = GunBookEditFlowState.EditGunBookRecord;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupCustomerResults");
            }
            this.executeNextState();
        }


        /// <summary>
        /// NavBox OnAction Handler for Police Holds List Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void gunBookSearchFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Gun Book Search form navigation action handler received invalid data");
            }

            NavBox gunBookSearchNavBox = (NavBox)sender;

            NavBox.NavAction lookupAction = gunBookSearchNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                CashlinxPawnSupportSession.Instance.HistorySession.Back();
                lookupAction = NavBox.NavAction.SUBMIT;
            }
            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:
                    this.nextState = GunBookEditFlowState.EditGunBookRecord;
                    break;
                case NavBox.NavAction.CANCEL:
                    CashlinxPawnSupportSession.Instance.HistorySession.Back();
                    this.nextState = GunBookEditFlowState.Cancel;   
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Gun book search");
            }

            this.executeNextState();
        }

        /// <summary>
        /// NavBox OnAction Handler for customer replace Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void customerReplaceFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Customer replace form navigation action handler received invalid data");
            }

            NavBox custReplaceNavBox = (NavBox)sender;

            NavBox.NavAction lookupAction = custReplaceNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:
                case NavBox.NavAction.BACKANDSUBMIT:
                case NavBox.NavAction.CANCEL:
                    CashlinxPawnSupportSession.Instance.HistorySession.Back();
                    this.nextState = GunBookEditFlowState.EditGunBookRecord;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for customer replace form");
            }

            this.executeNextState();
        }



        /// <summary>
        /// NavBox OnAction Handler for Edit gun book record Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void gunBookEditFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Gun Book Edit form navigation action handler received invalid data");
            }

            NavBox editGunBookRecordNavBox = (NavBox)sender;

            NavBox.NavAction lookupAction = editGunBookRecordNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                CashlinxPawnSupportSession.Instance.HistorySession.Back();
                lookupAction = NavBox.NavAction.SUBMIT;
            }
            switch (lookupAction)
            {

                case NavBox.NavAction.SUBMIT:
                    if (editGunBookRecordNavBox.IsCustom)
                    {
                        if (editGunBookRecordNavBox.CustomDetail == "DescribeMerchandise")
                            this.nextState = GunBookEditFlowState.DescribeItemEdit;
                        else if (editGunBookRecordNavBox.CustomDetail == "LookupCustomer")
                            this.nextState = GunBookEditFlowState.LookupCustomer;
                        else if (editGunBookRecordNavBox.CustomDetail == "EditCustomer")
                            this.nextState = GunBookEditFlowState.CustomerReplace;
                    }
                    else
                        this.nextState = GunBookEditFlowState.GunBookSearch;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Police Hold Info");
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
            NavBox.NavAction action = descMerchNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                CashlinxPawnSupportSession.Instance.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    this.nextState = GunBookEditFlowState.DescribeItem;
                    break;
                case NavBox.NavAction.CANCEL:
                    CashlinxPawnSupportSession.Instance.HistorySession.Back();
                    this.nextState = GunBookEditFlowState.EditGunBookRecord;
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
                CashlinxPawnSupportSession.Instance.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (descItemNavBox.CustomDetail == "DescribeMerchandise")
                    {
                        this.nextState = GunBookEditFlowState.DescribeMerchandise;
                    }
                    else
                    {
                        if (!GunBookUtilities.UpdateGunDescriptionData())
                            throw new ApplicationException("Error encountered in updating gun book description");
                        this.nextState = GunBookEditFlowState.EditGunBookRecord;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    
                    this.nextState = GunBookEditFlowState.EditGunBookRecord;
                    break;
                case NavBox.NavAction.BACK:
                    CashlinxPawnSupportSession.Instance.HistorySession.Back();
                    this.nextState = descItemNavBox.CustomDetail == "GUNEDIT" ? GunBookEditFlowState.EditGunBookRecord : GunBookEditFlowState.DescribeMerchandise;
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


    }
}


