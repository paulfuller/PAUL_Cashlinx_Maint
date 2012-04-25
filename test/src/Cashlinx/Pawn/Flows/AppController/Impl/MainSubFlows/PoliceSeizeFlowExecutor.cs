using System;
using System.Windows.Forms;
using CashlinxDesktop.DesktopForms.Pawn.Customer;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Pawn.Forms.Pawn.Customer;
using Pawn.Forms.Pawn.Customer.Holds;
using Pawn.Forms.Pawn.Customer.ItemRelease;
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class PoliceSeizeFlowExecutor : SingleExecuteBlock
    {
        public const string NAME = "PoliceSeizeFlowExecutor";

        public enum PoliceSeizeFlowState
        {
            LookupCustomer,
            LookupCustomerResults,
            PoliceHoldList,
            PoliceSeize,
            Exit,
            Cancel,
            Error
        }

        private PoliceSeizeFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;

        /// <summary>
        /// Main execution function for PoliceHoldReleaseFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
                return (false);
            PoliceSeizeFlowState inputState = (PoliceSeizeFlowState)inputData;

            switch (inputState)
            {
                case PoliceSeizeFlowState.LookupCustomer:
                    ShowForm lookupCustBlk = CommonAppBlocks.Instance.CreateLookupCustomerShowBlock(this.parentForm, this.lookupCustFormNavAction);
                    if (!lookupCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomer block");
                    }

                    break;
                case PoliceSeizeFlowState.LookupCustomerResults:
                    ShowForm lookupCustResBlk = CommonAppBlocks.Instance.CreateLookupCustomerResultsBlock(this.parentForm, this.lookupCustResultsFormNavAction);
                    if (!lookupCustResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomerResults block");
                    }
                    break;

                case PoliceSeizeFlowState.PoliceHoldList:
                    ShowForm loansListBlk = CommonAppBlocks.Instance.CreatePoliceHoldListShowBlock(this.parentForm, this.policeHoldListFormNavAction);
                    ((PoliceHoldsList)loansListBlk.ClassForm).PoliceSeize = true;
                    if (!loansListBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Loan List block");
                    }
                    
                    break;

                case PoliceSeizeFlowState.PoliceSeize:
                    ShowForm policeSeizeBlk = CommonAppBlocks.Instance.CreatePoliceHoldReleaseInfoShowBlock(this.parentForm, this.policeHoldReleaseInfoFormNavAction);
                    //Set the police seize property to true
                    ((PoliceHoldRelease)policeSeizeBlk.ClassForm).PoliceSeize = true;
                    if (!policeSeizeBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Police Seize block");
                    }
                    
                    
                    break;
                case PoliceSeizeFlowState.Cancel:
                    //Clear the customer from session
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();

                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;


                default:
                    throw new ApplicationException("Invalid Police Seize flow state");
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
                            this.nextState = PoliceSeizeFlowState.LookupCustomerResults;
                    }
                    else
                        this.nextState = PoliceSeizeFlowState.LookupCustomerResults;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceSeizeFlowState.Cancel;
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
                            this.nextState = PoliceSeizeFlowState.PoliceHoldList;
                    }
                    else

                        this.nextState = PoliceSeizeFlowState.PoliceHoldList;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = PoliceSeizeFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceSeizeFlowState.Cancel;
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
        private void policeHoldListFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Police Hold List form navigation action handler received invalid data");
            }

            NavBox policeHoldNavBox = (NavBox)sender;
            PoliceHoldsList policeHoldListForm = (PoliceHoldsList)data;
            NavBox.NavAction lookupAction = policeHoldNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                lookupAction = NavBox.NavAction.SUBMIT;
            }
            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:
                    this.nextState = PoliceSeizeFlowState.PoliceSeize;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceSeizeFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Police Hold List");
            }

            this.executeNextState();
        }


        /// <summary>
        /// NavBox OnAction Handler for Customer Hold Release Info Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void policeHoldReleaseInfoFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Police Hold Release Info form navigation action handler received invalid data");
            }

            NavBox custHoldNavBox = (NavBox)sender;
            
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
                            this.nextState = PoliceSeizeFlowState.PoliceHoldList;
                        else
                            this.nextState = PoliceSeizeFlowState.Cancel;
                    }
                    else
                    this.nextState = PoliceSeizeFlowState.Cancel;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceSeizeFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Police Seize");
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

        public PoliceSeizeFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = PoliceSeizeFlowState.LookupCustomer;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }
    }
}


