using System;
using System.Windows.Forms;
using CashlinxDesktop.DesktopForms.Pawn.Customer;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Customer;
using Pawn.Forms.Pawn.Customer.ItemRelease;
using Pawn.Logic;
using Utilities = Common.Libraries.Utility.Utilities;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class PoliceHoldReleaseFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "PoliceHoldReleaseFlowExecutor";

        public enum PoliceHoldReleaseFlowState
        {
            LookupCustomer,
            LookupCustomerResults,
            PoliceHoldReleaseList,
            PoliceHoldReleaseInfo,
            PoliceHoldReleaseToClaimant,
            ManageCustomer,
            UpdateAddress,
            UpdatePhysicalDescription,
            Exit,
            Cancel,
            Error
        }

        private PoliceHoldReleaseFlowState nextState;
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
            PoliceHoldReleaseFlowState inputState = (PoliceHoldReleaseFlowState)inputData;

            switch (inputState)
            {
                case PoliceHoldReleaseFlowState.LookupCustomer:
                    ShowForm lookupCustBlk = CommonAppBlocks.Instance.CreateLookupCustomerShowBlock(this.parentForm, this.lookupCustFormNavAction);
                    if (!lookupCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomer block");
                    }

                    break;
                case PoliceHoldReleaseFlowState.LookupCustomerResults:
                    ShowForm lookupCustResBlk = CommonAppBlocks.Instance.CreateLookupCustomerResultsBlock(this.parentForm, this.lookupCustResultsFormNavAction);
                    if (!lookupCustResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomerResults block");
                    }
                    break;

                case PoliceHoldReleaseFlowState.PoliceHoldReleaseList:
                    ShowForm custHoldRelListBlk = CommonAppBlocks.Instance.CreatePoliceHoldReleaseListShowBlock(this.parentForm, this.policeHoldReleaseListFormNavAction);
                    if (!custHoldRelListBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Police Hold Release List block");
                    }
                    break;
                case PoliceHoldReleaseFlowState.PoliceHoldReleaseInfo:
                    ShowForm custHoldRelInfoBlk = CommonAppBlocks.Instance.CreatePoliceHoldReleaseInfoShowBlock(this.parentForm, this.policeHoldReleaseInfoFormNavAction);
                    if (!custHoldRelInfoBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Police Hold Release Info block");
                    }
                    break;

                case PoliceHoldReleaseFlowState.PoliceHoldReleaseToClaimant:
                    //Clear Previous Customer Srch Data DF 0028
                    GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = new LookupCustomerSearchData();
                    ShowForm policeHoldRelToClaimantBlk = CommonAppBlocks.Instance.CreatePoliceHoldReleaseInfoShowBlock(this.parentForm, this.policeHoldReleaseInfoFormNavAction);
                    if (!policeHoldRelToClaimantBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Police Hold Release Info block");
                    }
                    //Set the release to claimant property to true
                    ((PoliceHoldRelease) policeHoldRelToClaimantBlk.ClassForm).ReleaseToClaimant = true;
                    break;
                case PoliceHoldReleaseFlowState.ManageCustomer:
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
                    break;
                case PoliceHoldReleaseFlowState.UpdateAddress:
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
                case PoliceHoldReleaseFlowState.UpdatePhysicalDescription:

                    ShowForm updatePhysDescBlk = CommonAppBlocks.Instance.UpdatePhysDescShowFormBlock(this.parentForm, this.updatePhysicalDescFormNavAction);
                    if (!updatePhysDescBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Update Physical Description block");
                    }


                    break;

                case PoliceHoldReleaseFlowState.Cancel:
                    //Clear the customer from session
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();

                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;


                default:
                    throw new ApplicationException("Invalid Police Hold release flow state");
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
                            if (GlobalDataAccessor.Instance.DesktopSession.ReleaseToClaimant)
                                this.nextState = PoliceHoldReleaseFlowState.ManageCustomer;
                            else
                            MessageBox.Show("Not a valid selection in this flow");
                        }
                        else
                            this.nextState = PoliceHoldReleaseFlowState.LookupCustomerResults;
                    }
                    else
                        this.nextState = PoliceHoldReleaseFlowState.LookupCustomerResults;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceHoldReleaseFlowState.Cancel;
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
                            //If release to claimant, allow customer creation
                            if (GlobalDataAccessor.Instance.DesktopSession.ReleaseToClaimant)
                                this.nextState = PoliceHoldReleaseFlowState.ManageCustomer;
                            else
                                MessageBox.Show("Not a valid selection in this flow");
                        }
                        else
                        {
                            if (GlobalDataAccessor.Instance.DesktopSession.ReleaseToClaimant)
                                this.nextState = PoliceHoldReleaseFlowState.PoliceHoldReleaseInfo;
                            else
                                this.nextState = PoliceHoldReleaseFlowState.PoliceHoldReleaseList;
                        }
                    }
                    else

                        this.nextState = PoliceHoldReleaseFlowState.PoliceHoldReleaseList;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = PoliceHoldReleaseFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceHoldReleaseFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupCustomerResults");
            }
            this.executeNextState();
        }


        /// <summary>
        /// NavBox OnAction Handler for Police Holds Release List Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void policeHoldReleaseListFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Police Hold Release List form navigation action handler received invalid data");
            }

            NavBox custHoldNavBox = (NavBox)sender;
            //PoliceHoldReleaseList policeHoldListForm = (PoliceHoldReleaseList)data;
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
                        if (custHoldNavBox.CustomDetail.Equals("ReleaseToClaimant",StringComparison.OrdinalIgnoreCase))
                            this.nextState = PoliceHoldReleaseFlowState.PoliceHoldReleaseToClaimant;
                        else
                            this.nextState = PoliceHoldReleaseFlowState.PoliceHoldReleaseInfo;
                    }
         
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceHoldReleaseFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Police Hold Release List");
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
            //CustomerHoldReleaseInfo custHoldInfoForm = (CustomerHoldReleaseInfo)data;
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
                            this.nextState = PoliceHoldReleaseFlowState.PoliceHoldReleaseList;
                        else if (custDet.Equals("FindClaimant", StringComparison.OrdinalIgnoreCase))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.ReleaseToClaimant = true;
                            this.nextState = PoliceHoldReleaseFlowState.LookupCustomer;
                        }
                        else
                            this.nextState = PoliceHoldReleaseFlowState.Cancel;
                    }
                    else
                    this.nextState = PoliceHoldReleaseFlowState.Cancel;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceHoldReleaseFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Police Hold Release Info");
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
                    this.nextState = PoliceHoldReleaseFlowState.PoliceHoldReleaseInfo;
                    break;
                case NavBox.NavAction.HIDEANDSHOW:
                    createCustForm.Hide();
                    this.nextState = PoliceHoldReleaseFlowState.UpdateAddress;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceHoldReleaseFlowState.Cancel;
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
                    this.nextState = PoliceHoldReleaseFlowState.ManageCustomer;
                    break;
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = PoliceHoldReleaseFlowState.PoliceHoldReleaseInfo;
                    break;
                case NavBox.NavAction.HIDEANDSHOW:
                    addrForm.Hide();
                    this.nextState = PoliceHoldReleaseFlowState.UpdatePhysicalDescription;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceHoldReleaseFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Update Address");
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
                    this.nextState = PoliceHoldReleaseFlowState.UpdateAddress;
                    break;
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = PoliceHoldReleaseFlowState.PoliceHoldReleaseInfo;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PoliceHoldReleaseFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Update Physical Description");
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

        public PoliceHoldReleaseFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = PoliceHoldReleaseFlowState.LookupCustomer;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }
    }
}


