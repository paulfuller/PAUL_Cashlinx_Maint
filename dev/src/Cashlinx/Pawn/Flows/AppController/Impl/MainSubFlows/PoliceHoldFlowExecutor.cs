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
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class PoliceHoldFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "PoliceHoldFlowExecutor";

        public enum PoliceHoldFlowState
        {
            LookupCustomer,
            LookupCustomerResults,
            PoliceHoldList,
            PoliceHoldInfo,
            PrintPickSlip,
            Exit,
            Cancel,
            Error
        }

        private PoliceHoldFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;

        /// <summary>
        /// Main execution function for CustomerHoldFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
                return (false);
            PoliceHoldFlowState inputState = (PoliceHoldFlowState)inputData;

            switch (inputState)
            {
                case PoliceHoldFlowState.LookupCustomer:
                    ShowForm lookupCustBlk = CommonAppBlocks.Instance.CreateLookupCustomerShowBlock(this.parentForm, this.lookupCustFormNavAction);
                    if (!lookupCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomer block");
                    }

                    break;
                case PoliceHoldFlowState.LookupCustomerResults:
                    ShowForm lookupCustResBlk = CommonAppBlocks.Instance.CreateLookupCustomerResultsBlock(this.parentForm, this.lookupCustResultsFormNavAction);
                    if (!lookupCustResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomerResults block");
                    }
                    break;

                case PoliceHoldFlowState.PoliceHoldList:
                    ShowForm custHoldListBlk = CommonAppBlocks.Instance.CreatePoliceHoldListShowBlock(this.parentForm,this.policeHoldListFormNavAction);
                    if (!custHoldListBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Police Hold List block");
                    }
                    break;
                case PoliceHoldFlowState.PoliceHoldInfo:
                    ShowForm custHoldInfoBlk = CommonAppBlocks.Instance.CreatePoliceInfoShowBlock(this.parentForm, this.policeHoldInfoFormNavAction);
                    if (!custHoldInfoBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Police Hold Info block");
                    }
                    break;


                case PoliceHoldFlowState.Cancel:
                    //Clear the customer from session
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();

                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;


                default:
                    throw new ApplicationException("Invalid Police Hold flow state");
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
                            this.nextState = PoliceHoldFlowState.LookupCustomerResults;
                    }
                    else
                    this.nextState = PoliceHoldFlowState.LookupCustomerResults;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceHoldFlowState.Cancel;
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
                            this.nextState = PoliceHoldFlowState.PoliceHoldList;
                    }
                    else
                        this.nextState = PoliceHoldFlowState.PoliceHoldList;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = PoliceHoldFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceHoldFlowState.Cancel;
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
                    this.nextState = PoliceHoldFlowState.PoliceHoldInfo;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceHoldFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Police Hold List");
            }

            this.executeNextState();
        }


        /// <summary>
        /// NavBox OnAction Handler for Customer Hold Info Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void policeHoldInfoFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Police Hold Info form navigation action handler received invalid data");
            }

            NavBox policeHoldNavBox = (NavBox)sender;
            PoliceHoldInfo policeHoldInfoForm = (PoliceHoldInfo)data;
            NavBox.NavAction lookupAction = policeHoldNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                lookupAction = NavBox.NavAction.SUBMIT;
            }
            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:
                    if (policeHoldNavBox.IsCustom)
                    {
                        string custDet = policeHoldNavBox.CustomDetail;
                        if (custDet.Equals("Back"))
                        {
                            this.nextState = PoliceHoldFlowState.PoliceHoldList;
                        }
                        else
                        {
                            this.nextState = PoliceHoldFlowState.Cancel;
                        }
                    }
                    else
                        this.nextState = PoliceHoldFlowState.Cancel;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceHoldFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Police Hold Info");
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

        public PoliceHoldFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = PoliceHoldFlowState.LookupCustomer;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }
    }
}


