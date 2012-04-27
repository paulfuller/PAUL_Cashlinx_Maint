using System;
using System.Windows.Forms;
using CashlinxDesktop.DesktopForms.Pawn.Customer;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility.Shared;
using Pawn.Flows.AppController.Impl.Common;
using Pawn.Forms.Pawn.Customer;
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class CustomerPurchaseFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "CustomerPurchaseFlowExecutor";
        public static readonly string MMPIFUNCTIONALITYNAME = "mmpi";
        private static readonly string DESCRIBEITEMTRIGGER = "describeitemcustomerpurchase";

        public enum PurchaseFlowState
        {
            LookupCustomer,
            AddCustomer,
            ExistingCustomer,
            LookupCustomerResults,
            ManagePawnApplication,
            ViewCustomerInformation,
            ViewReadOnlyCustomerInformation,
            InvokeMMPIChildFlow,
            ExitFlow,
            CancelFlow,
            Error
        }

        private PurchaseFlowState nextState;
        private PurchaseFlowState suspendedState = PurchaseFlowState.LookupCustomer;
        private Form parentForm;
        private FxnBlock endStateNotifier;

        /// <summary>
        /// Main execution function for CustomerPurchaseFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
            {
                inputData = suspendedState;
            }
            PurchaseFlowState inputState = (PurchaseFlowState)inputData;
            //If the customer is already looked up go straight to manage pawn application
            IsCustomerLookedUp lookupFxn = CommonAppBlocks.Instance.IsCustomerLookedUpBlock;
            lookupFxn.execute();

            if (inputState == PurchaseFlowState.LookupCustomer && GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(DESCRIBEITEMTRIGGER, StringComparison.OrdinalIgnoreCase))
            {
                //If merchandise is already looked up then we are looking to lookup customer
                IsBuyMerchandiseLookedUp lookupMdseFxn = CommonAppBlocks.Instance.IsBuyMerchandiseLookedUpBlock;
                lookupMdseFxn.execute();

                inputState = lookupMdseFxn.Value ? PurchaseFlowState.LookupCustomer : PurchaseFlowState.InvokeMMPIChildFlow;
            }

            switch (inputState)
            {
                case PurchaseFlowState.LookupCustomer:
                    GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext = CurrentContext.NEW;
                    ShowForm lookupCustBlk = CommonAppBlocks.Instance.CreateLookupCustomerShowBlock(this.parentForm, this.lookupCustFormNavAction);
                    if (!lookupCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomer block");
                    }

                    break;
                case PurchaseFlowState.LookupCustomerResults:
                    ShowForm lookupCustResBlk = CommonAppBlocks.Instance.CreateLookupCustomerResultsBlock(this.parentForm, this.lookupCustResultsFormNavAction);
                    if (!lookupCustResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomerResults block");
                    }
                    break;
                case PurchaseFlowState.AddCustomer:
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
                case PurchaseFlowState.ExistingCustomer:
                    ShowForm existCustBlk = CommonAppBlocks.Instance.CreateExistingCustomerBlock(this.parentForm, this.existCustomerFormNavAction);
                    if (!existCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute ExistingCustomer block");
                    }
                    break;
                case PurchaseFlowState.ManagePawnApplication:
                    ShowForm managePawnAppBlk = CommonAppBlocks.Instance.CreateManagePawnApplicationBlock(this.parentForm, this.managePawnAppFormNavAction);
                    if (!managePawnAppBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute ManagePawnApplication block");
                    }
                    break;
                case PurchaseFlowState.ViewCustomerInformation:
                    ShowForm viewCustInfoBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustInfoFormNavAction);
                    if (!viewCustInfoBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute View Customer Information block");
                    }

                    break;
                case PurchaseFlowState.ViewReadOnlyCustomerInformation:
                    ShowForm viewCustInfoReadOnlyBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustInfoFormNavAction);
                    if (!viewCustInfoReadOnlyBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute View customer information Read Only block");
                    }
                    ((ViewCustomerInformation)viewCustInfoReadOnlyBlk.ClassForm).ShowReadOnly = true;
                    break;
                case PurchaseFlowState.InvokeMMPIChildFlow:
                    //Initiate the child workflow
                    if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase == null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.Purchases.Add(new PurchaseVO());
                        GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = -1;
                    }
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(MMPIFUNCTIONALITYNAME,
                                                                                 this.parentForm, this.endStateNotifier, this);
                    break;

                case PurchaseFlowState.CancelFlow:
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
                case PurchaseFlowState.ExitFlow:
                    break;
                default:
                    throw new ApplicationException("Invalid customer purchase flow state");
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
                            this.nextState = PurchaseFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ExistingCustomer"))
                        {
                            this.nextState = PurchaseFlowState.ExistingCustomer;
                        }
                        else if (custDet.Equals("LookupCustomerResults"))
                        {
                            this.nextState = PurchaseFlowState.LookupCustomerResults;
                        }
                        else if (custDet.Equals("ManagePawnApplication"))
                        {
                            this.nextState = PurchaseFlowState.ManagePawnApplication;
                        }
                    }
                    else
                    {
                        //Default happy path next state
                        this.parentForm = lookupCustForm;
                        this.nextState = PurchaseFlowState.LookupCustomerResults;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PurchaseFlowState.CancelFlow;
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
                        if (custDet.Equals("Complete") || custDet.Equals("ManagePawnApplication") ||
                            custDet.Equals("CreateCustomer"))
                        {
                            this.nextState = PurchaseFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ViewCustomerInformation"))
                        {
                            this.nextState = PurchaseFlowState.ViewCustomerInformation;
                        }
                        else if (custDet.Equals("ViewCustomerInformationReadOnly"))
                        {
                            this.nextState = PurchaseFlowState.ViewReadOnlyCustomerInformation;
                        }
                    }
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = PurchaseFlowState.LookupCustomer;
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
            NavBox.NavAction lookupAction = viewCustInfoNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.BACK:
                    this.nextState = PurchaseFlowState.CancelFlow;
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
                        this.nextState = PurchaseFlowState.ExistingCustomer;
                    }
                    else if (custDet.Equals("DescribeMerchandise"))
                    {
                        this.nextState = PurchaseFlowState.InvokeMMPIChildFlow;
                    }
                    else
                    {
                        this.nextState = PurchaseFlowState.ExitFlow;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PurchaseFlowState.CancelFlow;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = PurchaseFlowState.LookupCustomerResults;
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
            NavBox.NavAction action = createCustNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    this.nextState = PurchaseFlowState.ManagePawnApplication;
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
                            this.nextState = PurchaseFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ViewPawnCustomerInformation"))
                        {
                            this.nextState = PurchaseFlowState.ViewCustomerInformation;
                        }
                    }
                    break;
                case NavBox.NavAction.BACK:
                    this.nextState = PurchaseFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PurchaseFlowState.CancelFlow;
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

        public CustomerPurchaseFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
        : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = PurchaseFlowState.LookupCustomer;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }

        public PurchaseFlowState NextState
        {
            get
            {
                return (this.nextState);
            }
        }
    }
}
