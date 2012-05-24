using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class LoanInquiryFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "LoanInquiryFlowState";
        public static readonly string PAWNINQUIRYFLOW = "loaninquiry";

        public enum LoanInquiryFlowState
        {
            LoanInquiry,
            LoanInquiryResults,
            LoanInquiryDetails,
            Print,
            Back, 
            Exit,
            Cancel,
            Error
        }

        private LoanInquiryFlowState nextState;
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
                inputData = LoanInquiryFlowState.LoanInquiry;
            }
            LoanInquiryFlowState inputState = (LoanInquiryFlowState)inputData;

            switch (inputState)
            {
                case LoanInquiryFlowState.LoanInquiry:
                    ShowForm loanInquiry = CommonAppBlocks.Instance.LoanInquiryShowBlock(this.parentForm, this.inquiryFormNavAction);
                    if (!loanInquiry.execute())
                    {
                        throw new ApplicationException("Cannot execute LoanInquiry block");
                    }
                    
                    break;

                case LoanInquiryFlowState.LoanInquiryResults:
                    ////ShowForm loanInquiryResults = CommonAppBlocks.Instance.LoanInquiryResultsShowBlock(this.parentForm, this.inquiryResultsFormNavAction);
                    //if (!loanInquiryResults.execute())
                    //{
                    //    throw new ApplicationException("Cannot execute LoanInquiry Results block");
                    //}
                    break;

                case LoanInquiryFlowState.LoanInquiryDetails:
                    break;

                case LoanInquiryFlowState.Cancel:
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
        private void inquiryFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Loan inquiry result form navigation action handler received invalid data");
            }

            NavBox loanInquiryResultCustNavBox = (NavBox)sender;
            NavBox.NavAction inquiryAction = loanInquiryResultCustNavBox.Action;

            if (inquiryAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                inquiryAction = NavBox.NavAction.SUBMIT;
            }
            switch (inquiryAction)
            {
                case NavBox.NavAction.SUBMIT:
                    //Submit will be called when Find customers button is pressed
                    //need to check if rowcount>1 go to results - otherwise go to detail
                    if (loanInquiryResultCustNavBox.IsCustom)
                    {
                        this.nextState = LoanInquiryFlowState.LoanInquiryResults;
                        //this.nextState = LoanInquiryFlowState.LoanInquiryDetails;
                    }
                    break;
                
                case NavBox.NavAction.CANCEL:
                    this.nextState = LoanInquiryFlowState.Cancel;
                    break;
                
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LoanInquiryFlowState.LoanInquiry;
                    break;

                default:
                    throw new ApplicationException("" + inquiryAction.ToString() + " is not a valid state for LoanInquiry");
            }

            this.executeNextState();
        }

        /// <summary>
        /// Action class for LookupCustomerResults
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void inquiryResultsFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Loan inquiry result form navigation action handler received invalid data");
            }

            NavBox loanInquiryResultCustNavBox = (NavBox)sender;
            NavBox.NavAction inquiryAction = loanInquiryResultCustNavBox.Action;

            if (inquiryAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                inquiryAction = NavBox.NavAction.SUBMIT;
            }
            switch (inquiryAction)
            {
                case NavBox.NavAction.SUBMIT:
                    //Submit will be called when Find customers button is pressed
                    //need to check if rowcount>1 go to results - otherwise go to detail
                    if (loanInquiryResultCustNavBox.IsCustom)
                    {
                        //this.nextState = LoanInquiryFlowState.LoanInquiryResults;
                        this.nextState = LoanInquiryFlowState.LoanInquiryDetails;
                    }
                    break;

                case NavBox.NavAction.CANCEL:
                    this.nextState = LoanInquiryFlowState.Cancel;
                    break;

                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LoanInquiryFlowState.LoanInquiry;
                    break;

                default:
                    throw new ApplicationException("" + inquiryAction.ToString() + " is not a valid state for LoanInquiry");
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
                    //string custDet = managePawnAppNavBox.CustomDetail;
                    //if (custDet.Equals(TriggerTypes.EXISTINGCUSTOMER))
                    //{
                    //    this.nextState = InquiryFlowState.InquiryResults;
                    //}
                    //else
                    //{
                    //    this.nextState = InquiryFlowState.Cancel;
                    //}
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LoanInquiryFlowState.Cancel;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LoanInquiryFlowState.LoanInquiry;
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
                throw new ApplicationException("Loan Inquiry form navigation action handler received invalid data");
            }

            NavBox createCustNavBox = (NavBox)sender;
            //CreateCustomer createCustForm = (CreateCustomer)data;
            NavBox.NavAction lookupAction = createCustNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LoanInquiryFlowState.LoanInquiryResults;
                    break;
                case NavBox.NavAction.HIDEANDSHOW:
                    //createCustForm.Hide();
                    this.nextState = LoanInquiryFlowState.LoanInquiryResults;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LoanInquiryFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Inquiry");
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
            //UpdateAddress addrForm = (UpdateAddress)data;
            NavBox.NavAction lookupAction = addrNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.BACK:
                    this.nextState = LoanInquiryFlowState.LoanInquiryResults;
                    break;
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LoanInquiryFlowState.LoanInquiryResults;
                    break;
                case NavBox.NavAction.HIDEANDSHOW:
                    //addrForm.Hide();
                    this.nextState = LoanInquiryFlowState.LoanInquiryResults;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LoanInquiryFlowState.Cancel;
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
            //ViewCustomerInformation viewCustForm = (ViewCustomerInformation)data;
            NavBox.NavAction action = viewCustNavBox.Action;
            switch (action)
            {
                case NavBox.NavAction.CANCEL:
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    CommonAppBlocks.Instance.HideFlowTabController();
                    this.nextState = LoanInquiryFlowState.Cancel;
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
            //UpdatePhysicalDesc physDescForm = (UpdatePhysicalDesc)data;
            NavBox.NavAction lookupAction = physDescNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.BACK:
                    this.nextState = LoanInquiryFlowState.LoanInquiryResults;
                    break;
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LoanInquiryFlowState.LoanInquiryResults;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LoanInquiryFlowState.Cancel;
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
            //ExistingCustomer existCustForm = (ExistingCustomer)data;
            NavBox.NavAction action = existCustNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    //if (existCustNavBox.IsCustom)
                    //{
                    //    string custDet = existCustNavBox.CustomDetail;
                    //    if (custDet.Equals("ManagePawnApplication"))
                    //    {
                    //        this.nextState = InquiryFlowState.InquiryResults;
                    //    }
                    //    else if (custDet.Equals("ViewPawnCustomerInformation"))
                    //    {
                    //        this.nextState = InquiryFlowState.InquiryResults;
                    //    }

                    //}
                    break;
                case NavBox.NavAction.BACK:
                    this.nextState = LoanInquiryFlowState.LoanInquiryResults;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LoanInquiryFlowState.Cancel;
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

        public LoanInquiryFlowExecutor(Form parentForm, FxnBlock eStateNotifier) : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = LoanInquiryFlowState.LoanInquiry;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }

        
    }
}

