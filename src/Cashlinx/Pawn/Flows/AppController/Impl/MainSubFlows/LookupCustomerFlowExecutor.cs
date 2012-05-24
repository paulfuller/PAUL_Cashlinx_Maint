using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms;
using Common.Libraries.Utility.Shared;
using Pawn.Flows.AppController.Impl.Common;
using Pawn.Forms.Pawn.Customer;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class LookupCustomerFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "LookupCustomerFlowExecutor";
        public static readonly string PAWNCUSTINFOFLOW = "pawncustinformation";
        public static readonly string NEWPAWNLOANFLOW = "newpawnloan";

        public enum LookupCustomerFlowState
        {
            LookupCustomer,
            LookupCustomerResults,
            AddCustomer,
            ManagePawnApplication,
            ExistingCustomer,
            ViewPawnCustomerInfo,
            ViewPawnCustomerInfoReadOnly,
            UpdateAddress,
            UpdatePhysicalDescription,
            PawnCustInformation,
            NewPawnLoanFlow,
            Exit,
            Cancel,
            Error
        }

        private LookupCustomerFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;
        

        /// <summary>
        /// Main execution function for LookupCustomerFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
            {
                inputData = LookupCustomerFlowState.LookupCustomer;
            }
            LookupCustomerFlowState inputState = (LookupCustomerFlowState)inputData;
            if (GlobalDataAccessor.Instance.DesktopSession.StartNewPawnLoan)
            {
                inputState = LookupCustomerFlowState.NewPawnLoanFlow;
            }

            switch (inputState)
            {
                case LookupCustomerFlowState.LookupCustomer:
                    ShowForm lookupCustBlk = CommonAppBlocks.Instance.CreateLookupCustomerShowBlock(this.parentForm, this.lookupCustFormNavAction);
                    if (!lookupCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomer block");
                    }

                    break;
                case LookupCustomerFlowState.LookupCustomerResults:
                    ShowForm lookupCustResBlk = CommonAppBlocks.Instance.CreateLookupCustomerResultsBlock(this.parentForm, this.lookupCustResultsFormNavAction);
                    if (!lookupCustResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomerResults block");
                    }
                    break;
                case LookupCustomerFlowState.AddCustomer:
                    //not sure how this trigger will be set
                    if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.MANAGEITEMRELEASE, StringComparison.OrdinalIgnoreCase))
                    {
                        CreateCustomer createCustFrm = new CreateCustomer();
                        Form currForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(createCustFrm);
                        if (currForm.GetType() == typeof(CreateCustomer))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                        }
                        else
                        {

                            ShowForm createCustBlk = CommonAppBlocks.Instance.CreateCreateCustomerBlock(this.parentForm, this.createCustFormNavAction);
                            if (!createCustBlk.execute())
                            {
                                throw new ApplicationException("Cannot execute Create Customer block");
                            }
                        }
                    }
                    else //if (CashlinxDesktopSession.Instance.HistorySession.Trigger.Equals("newpawnloan", StringComparison.OrdinalIgnoreCase))
                    {
                        ShowForm manageCustBlk = CommonAppBlocks.Instance.CreateManagePawnApplicationBlock(this.parentForm, this.managePawnAppFormNavAction);
                        if (!manageCustBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Manage Pawn Application block");
                        }

                    }


                    break;
                case LookupCustomerFlowState.ManagePawnApplication:
                    ShowForm manageCustAppBlk = CommonAppBlocks.Instance.CreateManagePawnApplicationBlock(this.parentForm, this.managePawnAppFormNavAction);
                    if (!manageCustAppBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Manage Pawn Application block");
                    }
                    break;
                case LookupCustomerFlowState.ExistingCustomer:
                    ShowForm existCustBlk = CommonAppBlocks.Instance.CreateExistingCustomerBlock(this.parentForm, this.existCustomerFormNavAction);
                    if (!existCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute ExistingCustomer block");
                    }
                    break;

                
                case LookupCustomerFlowState.ViewPawnCustomerInfoReadOnly:
                    ShowForm viewCustInfoReadOnlyBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustFormNavAction);
                    if (!viewCustInfoReadOnlyBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute View customer information block");
                    }
                    CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, viewCustInfoReadOnlyBlk.ClassForm, FlowTabController.State.Customer);
                    CommonAppBlocks.Instance.HideTabInFlowTab(FlowTabController.State.ItemHistory);
                    CommonAppBlocks.Instance.HideTabInFlowTab(FlowTabController.State.ProductsAndServices);
                    CommonAppBlocks.Instance.HideTabInFlowTab(FlowTabController.State.ProductHistory);
                    CommonAppBlocks.Instance.HideTabInFlowTab(FlowTabController.State.Stats);
                    ((ViewCustomerInformation)viewCustInfoReadOnlyBlk.ClassForm).ShowReadOnly = true;
                    break;


                case LookupCustomerFlowState.UpdateAddress:
                    UpdateAddress addrFrm = new UpdateAddress();
                    Form currentaddForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(addrFrm);
                    if (currentaddForm.GetType() == typeof(UpdateAddress))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    }
                    else
                    {

                        ShowForm updateAddrBlk = CommonAppBlocks.Instance.UpdateAddressShowFormBlock(this.parentForm, this.updateAddressFormNavAction);
                        if (!updateAddrBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Update Addess Form block");
                        }
                    }

                    break;
                case LookupCustomerFlowState.UpdatePhysicalDescription:

                    ShowForm updatePhysDescBlk = CommonAppBlocks.Instance.UpdatePhysDescShowFormBlock(this.parentForm, this.updatePhysicalDescFormNavAction);
                    if (!updatePhysDescBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Update Physical Description block");
                    }


                    break;
                case LookupCustomerFlowState.PawnCustInformation:
                    //Initiate the child workflow
                    GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(PAWNCUSTINFOFLOW,
                        this.parentForm, this.endStateNotifier, this);
                    break;
                case LookupCustomerFlowState.NewPawnLoanFlow:
                    //Initiate the child workflow for new pawn loan
                    GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(NEWPAWNLOANFLOW,
                        this.parentForm, this.endStateNotifier, this);
                    break;

                case LookupCustomerFlowState.Cancel:
                    CommonAppBlocks.Instance.HideFlowTabController();

                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;


 
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
                    //Submit will be called both when Add Customer button is pressed
                    //and when Find customers button is pressed
                    if (lookupCustNavBox.IsCustom)
                    {
                        string custDet = lookupCustNavBox.CustomDetail;
                        //Look for add customer
                        if (custDet.Equals("AddCustomer") || custDet.Equals("CreateCustomer"))
                        {
                            this.nextState = LookupCustomerFlowState.AddCustomer;
                        }
                        else
                            this.nextState = LookupCustomerFlowState.LookupCustomerResults;
                    }
                    else
                    {
                        this.nextState = LookupCustomerFlowState.LookupCustomerResults;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupCustomerFlowState.Cancel;
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
                        //Look for add customer
                        if (custDet.Equals("CreateCustomer") || custDet.Equals("ManagePawnApplication"))
                        {
                            this.nextState = LookupCustomerFlowState.AddCustomer;
                        }
                        else if (custDet.Equals("Complete"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                            this.nextState = LookupCustomerFlowState.PawnCustInformation;

                        }
                        else if (custDet.Equals("ViewCustomerInformationReadOnly"))
                        {
                            this.nextState = LookupCustomerFlowState.ViewPawnCustomerInfoReadOnly;

                        }

                    }
                    else
                    {
                        this.nextState = LookupCustomerFlowState.ViewPawnCustomerInfo;
                        LoadCustomerLoanKeys loanKeysBlk = new LoadCustomerLoanKeys();
                        loanKeysBlk.execute();
                    }
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupCustomerFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupCustomerFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupCustomerResults");
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
                    if (custDet.Equals(Commons.TriggerTypes.EXISTINGCUSTOMER))
                    {
                        this.nextState = LookupCustomerFlowState.ExistingCustomer;
                    }
                    else
                    {
                        this.nextState = LookupCustomerFlowState.Cancel;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupCustomerFlowState.Cancel;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupCustomerFlowState.LookupCustomerResults;
                    break;
            }
            this.executeNextState();
        }



        /// <summary>
        /// NavBox OnAction Handler for Create Customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void createCustFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Create Customer form navigation action handler received invalid data");
            }

            NavBox createCustNavBox = (NavBox)sender;
            CreateCustomer createCustForm = (CreateCustomer)data;
            NavBox.NavAction lookupAction = createCustNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LookupCustomerFlowState.ViewPawnCustomerInfo;
                    break;
                case NavBox.NavAction.HIDEANDSHOW:
                    createCustForm.Hide();
                    this.nextState = LookupCustomerFlowState.UpdateAddress;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupCustomerFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for LookupCustomer");
            }

            this.executeNextState();
        }


        /// <summary>
        /// NavBox OnAction Handler for Update Address
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void updateAddressFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Update Address form navigation action handler received invalid data");
            }

            NavBox addrNavBox = (NavBox)sender;
            UpdateAddress addrForm = (UpdateAddress)data;
            NavBox.NavAction lookupAction = addrNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.BACK:
                    this.nextState = LookupCustomerFlowState.AddCustomer;
                    break;
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LookupCustomerFlowState.ViewPawnCustomerInfo;
                    break;
                case NavBox.NavAction.HIDEANDSHOW:
                    addrForm.Hide();
                    this.nextState = LookupCustomerFlowState.UpdatePhysicalDescription;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupCustomerFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Update Address");
            }

            this.executeNextState();
        }

        private void viewCustFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("View Customer form navigation action handler received invalid data");
            }

            NavBox viewCustNavBox = (NavBox)sender;
            ViewCustomerInformation viewCustForm = (ViewCustomerInformation)data;
            NavBox.NavAction action = viewCustNavBox.Action;
            switch (action)
            {
                case NavBox.NavAction.CANCEL:
                    //PWNU00000354 SMurphy 3/10/2010 fixed the "Close" functionality on the customer "View"
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    CommonAppBlocks.Instance.HideFlowTabController();
                    this.nextState = LookupCustomerFlowState.Cancel;
                    break;

                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    CommonAppBlocks.Instance.HideFlowTabController();
                    this.nextState = LookupCustomerFlowState.LookupCustomerResults;
                //PWNU00000354 SMurphy 3/10/2010 fixed the "Close" functionality on the customer "View"
                //this.nextState = LookupCustomerFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupCustomer");
            }

            this.executeNextState();
        }


        /// <summary>
        /// NavBox OnAction Handler for Update Physical Description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void updatePhysicalDescFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Update Physical Desciption navigation action handler received invalid data");
            }

            NavBox physDescNavBox = (NavBox)sender;
            UpdatePhysicalDesc physDescForm = (UpdatePhysicalDesc)data;
            NavBox.NavAction lookupAction = physDescNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.BACK:
                    this.nextState = LookupCustomerFlowState.UpdateAddress;
                    break;
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LookupCustomerFlowState.ViewPawnCustomerInfo;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupCustomerFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Update Physical Description");
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
                            this.nextState = LookupCustomerFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ViewPawnCustomerInformation"))
                        {
                            this.nextState = LookupCustomerFlowState.PawnCustInformation;
                        }

                    }
                    break;
                case NavBox.NavAction.BACK:
                    this.nextState = LookupCustomerFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupCustomerFlowState.Cancel;
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

        public LookupCustomerFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = LookupCustomerFlowState.LookupCustomer;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }

        
    }
}

