using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Shared;
using Pawn.Flows.AppController.Impl.Common;
using Pawn.Forms.Pawn.Customer;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class NewPawnLoanFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "NewPawnLoanFlowExecutor";
        public static readonly string MMPIFUNCTIONALITYNAME = "mmpi";
        public static readonly string PAWNCUSTINFOFLOW = "pawncustinformation";
        public enum NewPawnLoanFlowState
        {
            LookupCustomer,
            AddCustomer,
            ExistingCustomer,
            LookupCustomerResults,
            ManagePawnApplication,
            ViewCustomerInformation,
            ViewReadOnlyCustomerInformation,
            InvokeMMPIChildFlow,
            InvokePawnCustInformationFlow,
            ExitFlow,
            CancelFlow,
            Error
        }

        private NewPawnLoanFlowState nextState;

        private Form parentForm;
        private FxnBlock endStateNotifier;

        /// <summary>
        /// Main execution function for NewPawnLoanFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            var inputState = NewPawnLoanFlowState.LookupCustomer;

            if (inputData == null  && !GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.DESCRIBEMERCHANDISE))
            {
                if (GlobalDataAccessor.Instance.DesktopSession.TabStateClicked != FlowTabController.State.None)
                    inputState = NewPawnLoanFlowState.InvokePawnCustInformationFlow;
                else if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null && !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber) &&
                                    GlobalDataAccessor.Instance.DesktopSession.TabStateClicked == FlowTabController.State.None)
                    inputState = NewPawnLoanFlowState.InvokeMMPIChildFlow;

            }
            else if (inputData !=  null)
            {
                inputState = (NewPawnLoanFlowState)inputData;
                if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null && !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber) &&
                                                    GlobalDataAccessor.Instance.DesktopSession.TabStateClicked == FlowTabController.State.None && GlobalDataAccessor.Instance.DesktopSession.StartNewPawnLoan)
                    inputState = NewPawnLoanFlowState.InvokeMMPIChildFlow;
            }
            //If the trigger is describe merchandise for a new pawn loan invoke mmpi flow
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.DESCRIBEMERCHANDISE))
            {
                if (inputState == NewPawnLoanFlowState.LookupCustomer)
                {
                    //If merchandise is already looked up then we are looking to lookup customer
                    IsLoanMerchandiseLookedUp lookupMdseFxn = CommonAppBlocks.Instance.IsLoanMerchandiseLookedUpBlock;
                    lookupMdseFxn.execute();
                    //Check if tab state is null..if it is not null then pawncustinfo flow should be invoked
                    if (GlobalDataAccessor.Instance.DesktopSession.TabStateClicked != FlowTabController.State.None)
                        inputState = NewPawnLoanFlowState.InvokePawnCustInformationFlow;
                    else if (lookupMdseFxn.Value && (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer == null || string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber)))
                        inputState = NewPawnLoanFlowState.LookupCustomer;
                    else
                        inputState = NewPawnLoanFlowState.InvokeMMPIChildFlow;
                }
            }
            else
            {

                //If the customer is already looked up go straight to manage pawn application
                IsCustomerLookedUp lookupFxn = CommonAppBlocks.Instance.IsCustomerLookedUpBlock;
                lookupFxn.execute();

                if (lookupFxn.Value == true && inputState == NewPawnLoanFlowState.LookupCustomer)
                {
                    inputState = NewPawnLoanFlowState.ManagePawnApplication;
                }
            }

            switch (inputState)
            {
                case NewPawnLoanFlowState.LookupCustomer:

                    ShowForm lookupCustBlk = CommonAppBlocks.Instance.CreateLookupCustomerShowBlock(this.parentForm, this.lookupCustFormNavAction);
                    if (!lookupCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomer block");
                    }

                    break;
                case NewPawnLoanFlowState.LookupCustomerResults:
                    ShowForm lookupCustResBlk = CommonAppBlocks.Instance.CreateLookupCustomerResultsBlock(this.parentForm, this.lookupCustResultsFormNavAction);
                    if (!lookupCustResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomerResults block");
                    }
                    break;
                case NewPawnLoanFlowState.AddCustomer:
                    if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.NEWPAWNLOAN, StringComparison.OrdinalIgnoreCase))
                    {
                        ShowForm manageCustBlk = CommonAppBlocks.Instance.CreateManagePawnApplicationBlock(this.parentForm, this.managePawnAppFormNavAction);
                        if (!manageCustBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Manage Pawn Application block");
                        }

                    }
                    else
                    {
                        ShowForm createCustBlk = CommonAppBlocks.Instance.CreateCreateCustomerBlock(this.parentForm, this.createCustomerFormNavAction);
                        if (!createCustBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute CreateCustomer block");
                        }
                    }
                    break;
                case NewPawnLoanFlowState.ExistingCustomer:
                    ShowForm existCustBlk = CommonAppBlocks.Instance.CreateExistingCustomerBlock(this.parentForm, this.existCustomerFormNavAction);
                    if (!existCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute ExistingCustomer block");
                    }
                    break;
                case NewPawnLoanFlowState.ManagePawnApplication:
                    ShowForm managePawnAppBlk = CommonAppBlocks.Instance.CreateManagePawnApplicationBlock(this.parentForm, this.managePawnAppFormNavAction);
                    if (!managePawnAppBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute ManagePawnApplication block");
                    }
                    break;
                case NewPawnLoanFlowState.ViewCustomerInformation:
                    ShowForm viewCustInfoBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustInfoFormNavAction);
                    if (!viewCustInfoBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute View Customer Information block");
                    }

                    break;
                case NewPawnLoanFlowState.ViewReadOnlyCustomerInformation:
                    ShowForm viewCustInfoReadOnlyBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustInfoFormNavAction);
                    if (!viewCustInfoReadOnlyBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute View customer information Read Only block");
                    }
                    ((ViewCustomerInformation)viewCustInfoReadOnlyBlk.ClassForm).ShowReadOnly = true;
                    break;
                case NewPawnLoanFlowState.InvokeMMPIChildFlow:
                    //Initiate the child workflow
                    if (GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Count == 0)
                    GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Add(new PawnLoan());
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(MMPIFUNCTIONALITYNAME,
                        this.parentForm, this.endStateNotifier, this);
                    break;
                case NewPawnLoanFlowState.InvokePawnCustInformationFlow:

                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(PAWNCUSTINFOFLOW,
                        this.parentForm, this.endStateNotifier, this);
                    break;

                case NewPawnLoanFlowState.CancelFlow:
                    CommonAppBlocks.Instance.HideFlowTabController();
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
                case NewPawnLoanFlowState.ExitFlow:
                    break;
                default:
                    throw new ApplicationException("Invalid new pawn loan flow state");
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
                        //Look for add customer
                        if (custDet.Equals("AddCustomer") ||
                            custDet.Equals("CreateCustomer"))
                        {
                            //Execute add customer 
                            this.nextState = NewPawnLoanFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ExistingCustomer"))
                        {
                            this.nextState = NewPawnLoanFlowState.ExistingCustomer;
                        }
                        else if (custDet.Equals("LookupCustomerResults"))
                        {
                            this.nextState = NewPawnLoanFlowState.LookupCustomerResults;
                        }
                        else if (custDet.Equals("ManagePawnApplication"))
                        {
                            this.nextState = NewPawnLoanFlowState.ManagePawnApplication;
                        }
                    }
                    else
                    {
                        //Default happy path next state
                        this.parentForm = lookupCustForm;
                        this.nextState = NewPawnLoanFlowState.LookupCustomerResults;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = NewPawnLoanFlowState.CancelFlow;
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
                throw new ApplicationException("Lookup customer form navigation action handler received invalid data");
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
                        if (custDet.Equals("Complete") || custDet.Equals("ManagePawnApplication"))
                        {
                            this.nextState = NewPawnLoanFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ViewCustomerInformation"))
                        {
                            this.nextState = NewPawnLoanFlowState.ViewCustomerInformation;
                        }
                        else if (custDet.Equals("ViewCustomerInformationReadOnly"))
                        {
                            this.nextState = NewPawnLoanFlowState.ViewReadOnlyCustomerInformation;
                        }

                    }
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = NewPawnLoanFlowState.LookupCustomer;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupCustomerResults");
            }
            this.executeNextState();
        }

        /// <summary>
        /// NavBox OnAction Handler for View Customer INformation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void viewCustInfoFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("View Customer Info form navigation action handler received invalid data");
            }

            NavBox viewCustInfoNavBox = (NavBox)sender;
            ViewCustomerInformation viewcustform = (ViewCustomerInformation)data;
            NavBox.NavAction lookupAction = viewCustInfoNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.BACK:
                    this.nextState = NewPawnLoanFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for View Customer INformation");
            }

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
            ManagePawnApplication managePawnAppForm = (ManagePawnApplication)data;
            NavBox.NavAction action = managePawnAppNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    string custDet = managePawnAppNavBox.CustomDetail;
                    if (custDet.Equals("ExistingCustomer"))
                    {
                        this.nextState = NewPawnLoanFlowState.ExistingCustomer;
                    }
                    else if (custDet.Equals("DescribeMerchandise"))
                    {
                        LoadCustomerLoanKeys loanKeysDataBlk = new LoadCustomerLoanKeys();
                        if (!loanKeysDataBlk.execute())
                        {
                            //throw new ApplicationException("Cannot get Loan keys for selected customer");
                            MessageBox.Show("An error occurred in getting loan details for the selected customer");
                            this.nextState = NewPawnLoanFlowState.CancelFlow;
                        }

                        this.nextState = NewPawnLoanFlowState.InvokeMMPIChildFlow;
                    }
                    else
                    {
                        this.nextState = NewPawnLoanFlowState.ExitFlow;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = NewPawnLoanFlowState.CancelFlow;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = NewPawnLoanFlowState.LookupCustomerResults;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for ManagePawnApplication");
            }
            this.executeNextState();
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void createCustomerFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Create customer form navigation action handler received invalid data");
            }

            NavBox createCustNavBox = (NavBox)sender;
            CreateCustomer createCustForm = (CreateCustomer)data;
            NavBox.NavAction action = createCustNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    this.nextState = NewPawnLoanFlowState.ManagePawnApplication;
                    break;
            }
            this.executeNextState();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void existCustomerFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Existing customer form navigation action handler received invalid data");
            }

            NavBox existCustNavBox = (NavBox)sender;
            ExistingCustomer existCustForm = (ExistingCustomer)data;
            NavBox.NavAction action = existCustNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (existCustNavBox.IsCustom)
                    {
                        string custDet = existCustNavBox.CustomDetail;
                        if (custDet.Equals("ManagePawnApplication"))
                        {
                            this.nextState = NewPawnLoanFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ViewPawnCustomerInformation"))
                        {
                            this.nextState = NewPawnLoanFlowState.ViewCustomerInformation;
                        }

                    }
                    break;
                case NavBox.NavAction.BACK:
                    this.nextState = NewPawnLoanFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = NewPawnLoanFlowState.CancelFlow;
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

        public NewPawnLoanFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = NewPawnLoanFlowState.LookupCustomer;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }

        public NewPawnLoanFlowState NextState
        {
            get
            {
                return (this.nextState);
            }
        }


    }
}
