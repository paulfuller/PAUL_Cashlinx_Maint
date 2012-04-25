using System;
using System.Windows.Forms;
using CashlinxDesktop.DesktopForms.Pawn.Customer;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Pawn.Forms.Pawn.Customer;
using Pawn.Forms.Pawn.Customer.ItemRelease;
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class CustomerHoldReleaseFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "CustomerHoldReleaseFlowExecutor";

        public enum CustomerHoldReleaseFlowState
        {
            LookupCustomer,
            LookupCustomerResults,
            CustomerHoldReleaseList,
            CustomerHoldReleaseInfo,
            Exit,
            Cancel,
            Error
        }

        private CustomerHoldReleaseFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;

        /// <summary>
        /// Main execution function for CustomerHoldReleaseFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
                return (false);
            CustomerHoldReleaseFlowState inputState = (CustomerHoldReleaseFlowState)inputData;

            switch (inputState)
            {
                case CustomerHoldReleaseFlowState.LookupCustomer:
                    ShowForm lookupCustBlk = CommonAppBlocks.Instance.CreateLookupCustomerShowBlock(this.parentForm, this.lookupCustFormNavAction);
                    if (!lookupCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomer block");
                    }

                    break;
                case CustomerHoldReleaseFlowState.LookupCustomerResults:
                    ShowForm lookupCustResBlk = CommonAppBlocks.Instance.CreateLookupCustomerResultsBlock(this.parentForm, this.lookupCustResultsFormNavAction);
                    if (!lookupCustResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomerResults block");
                    }
                    break;

                case CustomerHoldReleaseFlowState.CustomerHoldReleaseList:
                    ShowForm custHoldRelListBlk = CommonAppBlocks.Instance.CreateCustomerHoldReleaseListShowBlock(this.parentForm, this.custHoldReleaseListFormNavAction);
                    if (!custHoldRelListBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Customer Hold Release List block");
                    }
                    break;
                case CustomerHoldReleaseFlowState.CustomerHoldReleaseInfo:
                    ShowForm custHoldRelInfoBlk = CommonAppBlocks.Instance.CreateCustomerHoldReleaseInfoShowBlock(this.parentForm, this.custHoldReleaseInfoFormNavAction);
                    if (!custHoldRelInfoBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Customer Hold Release Info block");
                    }
                    break;


                case CustomerHoldReleaseFlowState.Cancel:
                    //Clear the customer from session
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();

                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;


                default:
                    throw new ApplicationException("Invalid Customer Hold flow release state");
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
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
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
                            this.nextState = CustomerHoldReleaseFlowState.LookupCustomerResults;
                    }
                    else
                        this.nextState = CustomerHoldReleaseFlowState.LookupCustomerResults;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = CustomerHoldReleaseFlowState.Cancel;
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
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (lookupCustResNavBox.IsCustom)
                    {
                        string custDet = lookupCustResNavBox.CustomDetail;
                        if (custDet.Equals("CreateCustomer", StringComparison.OrdinalIgnoreCase) ||
                            custDet.Equals("ManagePawnAppplication", StringComparison.OrdinalIgnoreCase) ||
                            custDet.Equals("ViewCustomerInformation", StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("Not a valid selection in this flow");
                        }
                        else
                            this.nextState = CustomerHoldReleaseFlowState.CustomerHoldReleaseList;
                    }
                    else

                        this.nextState = CustomerHoldReleaseFlowState.CustomerHoldReleaseList;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = CustomerHoldReleaseFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = CustomerHoldReleaseFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupCustomerResults");
            }
            this.executeNextState();
        }


        /// <summary>
        /// NavBox OnAction Handler for Customer Holds Release List Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void custHoldReleaseListFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Customer Hold Release List form navigation action handler received invalid data");
            }

            NavBox custHoldNavBox = (NavBox)sender;
            CustomerHoldReleaseList custHoldListForm = (CustomerHoldReleaseList)data;
            NavBox.NavAction lookupAction = custHoldNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                lookupAction = NavBox.NavAction.SUBMIT;
            }
            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:
                    this.nextState = CustomerHoldReleaseFlowState.CustomerHoldReleaseInfo;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = CustomerHoldReleaseFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Customer Hold Release List");
            }

            this.executeNextState();
        }


        /// <summary>
        /// NavBox OnAction Handler for Customer Hold Release Info Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void custHoldReleaseInfoFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Customer Hold Release Info form navigation action handler received invalid data");
            }

            NavBox custHoldNavBox = (NavBox)sender;
            CustomerHoldReleaseInfo custHoldInfoForm = (CustomerHoldReleaseInfo)data;
            NavBox.NavAction lookupAction = custHoldNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                lookupAction = NavBox.NavAction.SUBMIT;
            }
            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:
                    if (custHoldNavBox.IsCustom)
                    {
                        string custDet = custHoldNavBox.CustomDetail;
                        if (custDet.Equals("Back", StringComparison.OrdinalIgnoreCase))
                            this.nextState = CustomerHoldReleaseFlowState.CustomerHoldReleaseList;
                        else
                            this.nextState = CustomerHoldReleaseFlowState.Cancel;
                    }
                    else
                    this.nextState = CustomerHoldReleaseFlowState.Cancel;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = CustomerHoldReleaseFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Customer Hold Release Info");
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

        public CustomerHoldReleaseFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = CustomerHoldReleaseFlowState.LookupCustomer;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }
    }
}


