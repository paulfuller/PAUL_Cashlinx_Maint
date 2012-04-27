using System;
using System.Windows.Forms;
using Support.Forms.Customer;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
//using Common.Libraries.Forms;
using Support.Forms.Customer.Products;
using Support.Libraries.Forms;
using Common.Libraries.Utility.Shared;
//using Pawn.Flows.AppController.Impl.Common;
//using Support.Flows.AppController.Impl.Common;
using Support.Forms.Pawn.Customer;
using ExistingCustomer = Support.Forms.Pawn.Customer.ExistingCustomer;

//using Pawn.Logic;

namespace Support.Flows.AppController.Impl.MainSubFlows
{
    /*__________________________________________________________________________________________*/
    public class LookupCustomerFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "LookupCustomerFlowExecutor";
        public static readonly string PAWNCUSTINFOFLOW = "pawncustinformation";
        public static readonly string NEWPAWNLOANFLOW = "newpawnloan";

        public enum LookupCustomerFlowState
        {
            LookupCustomer,
            LookupCustomerResults,
            ViewCustomerInformationReadOnly,
            UpdateCustomerDetails,
            UpdateCustomerStatus,
            SupportCustomerComment,
            UpdateCustomerContactDetails,
            UpdateCommentsandNotes,
            UpdateCustomerIdentification,
            ViewPersonalInformationHistory,
            PawnCustInformation,
            NewPawnLoanFlow,
            ViewPawnCustomerInfo,
            UpdatePhysicalDescription,
            ManagePawnApplication,
            AddCustomer,
            UpdateAddress,
            Controller_ProductServices,
            Exit,
            Cancel,
            Error

            //
            //ExistingCustomer,
            //
            ///*ViewPawnCustomerInfoReadOnly,*/
            //
            //

            //

        }

        private LookupCustomerFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;
        #region Consturctor
        /*__________________________________________________________________________________________*/
        public LookupCustomerFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = LookupCustomerFlowState.LookupCustomer;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }
        #endregion
        #region FlowExecutor & ExecuteNextState
        /// <summary>
        /// Main execution function for LookupCustomerFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        /*__________________________________________________________________________________________*/
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
                /*_________________________________________________*/
                case LookupCustomerFlowState.LookupCustomer:
                    ShowForm lookupCustBlk = CommonAppBlocks.Instance.LookupCustomerShowBlock(this.parentForm, this.lookupCustomerFormNavAction);
                    if (!lookupCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomer block");
                    }

                    break;
                /*_________________________________________________*/
                case LookupCustomerFlowState.LookupCustomerResults:
                    ShowForm lookupCustResBlk = CommonAppBlocks.Instance.LookupCustomerResultsBlock(this.parentForm, this.lookupCustomerResultsFormNavAction);
                    if (!lookupCustResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomerResults block");
                    }
                    break;
                /*_________________________________________________*/
                case LookupCustomerFlowState.ViewCustomerInformationReadOnly:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = "ViewCustomerInformationReadOnly";
                    ShowForm viewCustInfoReadOnlyBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustomerInformationFormNavAction);
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
                /*_________________________________________________*/
                case LookupCustomerFlowState.UpdateCustomerDetails:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = "UpdateCustomerDetails";
                    ShowForm UpdateCustomerDetailsResBlk = CommonAppBlocks.Instance.UpdateCustomerDetailsShowBlock(this.parentForm, this.UpdateCustomerDetailsFormNavAction);
                    if (!UpdateCustomerDetailsResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerDetails block");
                    }
                    break;
                /*_________________________________________________*/
                case LookupCustomerFlowState.UpdateCustomerStatus:
                    ShowForm UpdateCustomerStatusResBlk = CommonAppBlocks.Instance.UpdateCustomerStatusShowBlock(this.parentForm, this.UpdateCustomerStatusFormNavAction);
                    if (!UpdateCustomerStatusResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerStatus block");
                    }
                    break;
                /*_________________________________________________*/
                case LookupCustomerFlowState.SupportCustomerComment:
                    ShowForm SupportCustomerCommentResBlk = CommonAppBlocks.Instance.SupportCustomerCommentShowBlock(this.parentForm, this.SupportCustomerCommentFormNavAction);
                    if (!SupportCustomerCommentResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerStatus block");
                    }
                    break;

                /*_________________________________________________*/
                case LookupCustomerFlowState.UpdateCustomerContactDetails:
                    ShowForm UpdateCustomerContactDetailsResBlk = CommonAppBlocks.Instance.UpdateCustomerContactDetailsShowBlock(this.parentForm, this.UpdateCustomerContactDetailsFormNavAction);
                    if (!UpdateCustomerContactDetailsResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerContactDetails block");
                    }
                    break;
                /*_________________________________________________*/
                case LookupCustomerFlowState.UpdateCommentsandNotes:
                    ShowForm UpdateCommentsandNotesResBlk = CommonAppBlocks.Instance.UpdateCommentsandNotesShowBlock(this.parentForm, this.UpdateCommentsandNotesFormNavAction);
                    if (!UpdateCommentsandNotesResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerContactDetails block");
                    }
                    break;
                /*_________________________________________________*/
                case LookupCustomerFlowState.UpdateCustomerIdentification:
                    ShowForm UpdateCustomerIdentificationResBlk = CommonAppBlocks.Instance.UpdateCustomerIdentificationShowBlock(this.parentForm, this.UpdateCustomerIdentificationFormNavAction);
                    if (!UpdateCustomerIdentificationResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerContactDetails block");
                    }
                    break;
                /*_________________________________________________*/
                case LookupCustomerFlowState.UpdateAddress:
                    UpdateAddress addrFrm = new UpdateAddress();
                    Form currentaddForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(addrFrm);
                    if (currentaddForm.GetType() == typeof(UpdateAddress))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    }
                    else
                    {
                        ShowForm updateAddrBlk = CommonAppBlocks.Instance.UpdateAddressShowBlock(this.parentForm, this.updateAddressFormNavAction);
                        if (!updateAddrBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Update Addess Form block");
                        }
                    }

                    break;
                /*_________________________________________________*/
                case LookupCustomerFlowState.ViewPersonalInformationHistory:
                    ShowForm ViewPersonalInformationHistoryResBlk = CommonAppBlocks.Instance.ViewPersonalInformationHistoryShowBlock(this.parentForm, this.ViewPersonalInformationHistoryFormNavAction);
                    if (!ViewPersonalInformationHistoryResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerContactDetails block");
                    }
                    break;
                /*_________________________________________________*/
                case LookupCustomerFlowState.PawnCustInformation:
                    // WCM 4/17/12 Comment out to get code working after moving FlowTabController to support.libaries.forms
                    ////Initiate the child workflow
                    //GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                    //GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(PAWNCUSTINFOFLOW,
                    //    this.parentForm, this.endStateNotifier, this);
                    break;
                /*_________________________________________________*/
                case LookupCustomerFlowState.NewPawnLoanFlow:
                    // WCM 4/17/12 Comment out to get code working after moving FlowTabController to support.libaries.forms
                    ////Initiate the child workflow for new pawn loan
                    //GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                    //GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(NEWPAWNLOANFLOW,
                    //    this.parentForm, this.endStateNotifier, this);
                    break;

                //Controller_ProductServices
                /*_________________________________________________*/
                case LookupCustomerFlowState.Controller_ProductServices:
                    ShowForm Controller_ProductServicestResBlk = CommonAppBlocks.Instance.Controller_ProductServicesShowBlock(this.parentForm, this.Controller_ProductServicesFormNavAction);
                    if (!Controller_ProductServicestResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Controller_ProductServices block");
                    }
                    break;

                #region Commented Out Case Statement Logic
                /*

                    */
                /* case LookupCustomerFlowState.AddCustomer:
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


                     break; */
                /*            case LookupCustomerFlowState.ManagePawnApplication:
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
                                */



                /* 
                 case LookupCustomerFlowState.UpdatePhysicalDescription:

                     ShowForm updatePhysDescBlk = CommonAppBlocks.Instance.UpdatePhysDescShowFormBlock(this.parentForm, this.updatePhysicalDescFormNavAction);
                     if (!updatePhysDescBlk.execute())
                     {
                         throw new ApplicationException("Cannot execute Update Physical Description block");
                     }


                     break;*/
                #endregion
                case LookupCustomerFlowState.Cancel:
                    CommonAppBlocks.Instance.HideFlowTabController();

                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;

            }

            return (true);
        }
        /// <summary>
        /// 
        /// </summary>
        /*__________________________________________________________________________________________*/
        private void executeNextState()
        {
            object evalExecFlag = this.executorFxn(this.nextState);
            if (evalExecFlag == null || ((bool)(evalExecFlag)) == false)
            {
                throw new ApplicationException("Cannot execute the next state: " + this.nextState.ToString());
            }
        }
        #endregion
        #region FormNavAction
        /// <summary>
        /// NavBox OnAction Handler for Lookup Customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /*__________________________________________________________________________________________*/
        private void lookupCustomerFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Lookup customer form navigation action handler received invalid data");
            }

            NavBox senderNavBox = (NavBox)sender;
            LookupCustomer lookupCustForm = (LookupCustomer)data;
            NavBox.NavAction lookupAction = senderNavBox.Action;

            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:

                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupCustomerFlowState.LookupCustomerResults;
                    break;
                case NavBox.NavAction.CANCEL:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupCustomerFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for LookupCustomer");
            }
            senderNavBox.CustomDetail = "LookupCustomer";
            this.executeNextState();
        }
        /// <summary>
        /// Action class for LookupCustomerResults
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /*__________________________________________________________________________________________*/
        private void lookupCustomerResultsFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Lookup customer form navigation action handler received invalid data");
            }

            NavBox senderNavBox = (NavBox)sender;
            LookupCustomerResults lookupCustResForm = (LookupCustomerResults)data;
            NavBox.NavAction action = senderNavBox.Action;

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:

                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    //this.nextState = LookupCustomerFlowState.ViewCustomerInformationReadOnly;

                    string linkLabel = senderNavBox.CustomDetail;
                    if (linkLabel.Equals("Controller_ProductServices"))
                    { this.nextState = LookupCustomerFlowState.Controller_ProductServices; }
                    else if (linkLabel.Equals("ViewCustomerInformationReadOnly"))
                    { this.nextState = LookupCustomerFlowState.ViewCustomerInformationReadOnly; }

                    break;
                #region Obsolete
                //if (lookupCustResNavBox.IsCustom)
                //{
                //    string custDet = lookupCustResNavBox.CustomDetail;
                //    //Look for add customer
                //    if (custDet.Equals("CreateCustomer") || custDet.Equals("ManagePawnApplication"))
                //    {
                //        this.nextState = LookupCustomerFlowState.AddCustomer;
                //    }
                //    else if (custDet.Equals("Complete"))
                //    {
                //        GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                //        this.nextState = LookupCustomerFlowState.PawnCustInformation;

                //    }
                //    else if (custDet.Equals("ViewCustomerInformationReadOnly"))
                //    {
                //        this.nextState = LookupCustomerFlowState.ViewCustomerInformationReadOnly; // wcm 3/28/12 ViewPawnCustomerInfoReadOnly;

                //    }

                //}
                //else
                //{
                //    this.nextState = LookupCustomerFlowState.ViewPawnCustomerInfo;
                //    //LoadCustomerLoanKeys loanKeysBlk = new LoadCustomerLoanKeys();
                //    //loanKeysBlk.execute();
                //}
                //break;
                #endregion
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupCustomerFlowState.LookupCustomer;   //.ViewCustomerInformationReadOnly; 
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupCustomerFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupCustomerResults");
            }
            senderNavBox.CustomDetail = "LookupCustomerResults";
            this.executeNextState();
        }
        /*__________________________________________________________________________________________*/
        private void viewCustomerInformationFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("View Customer form navigation action handler received invalid data");
            }

            CommonAppBlocks.Instance.HideFlowTabController();

            NavBox senderNavBox = (NavBox)sender;
            ViewCustomerInformation viewCustForm = (ViewCustomerInformation)data;
            NavBox.NavAction action = senderNavBox.Action;
            switch (action)
            {
                case NavBox.NavAction.CANCEL:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LookupCustomerFlowState.Cancel;

                    break;

                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupCustomerFlowState.LookupCustomer;
                    break;

                case NavBox.NavAction.SUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();

                    string linkLabel = senderNavBox.CustomDetail;
                    if (linkLabel.Equals("UpdateCustomerDetails"))
                    { this.nextState = LookupCustomerFlowState.UpdateCustomerDetails; }
                    else if (linkLabel.Equals("UpdateAddress"))
                    { this.nextState = LookupCustomerFlowState.UpdateAddress; }
                    else if (linkLabel.Equals("UpdateCustomerContactDetails"))
                    { this.nextState = LookupCustomerFlowState.UpdateCustomerContactDetails; }
                    else if (linkLabel.Equals("UpdateCommentsandNotes"))
                    { this.nextState = LookupCustomerFlowState.UpdateCommentsandNotes; }
                    else if (linkLabel.Equals("UpdateCustomerIdentification"))
                    { this.nextState = LookupCustomerFlowState.UpdateCustomerIdentification; }
                    else if (linkLabel.Equals("ViewPersonalInformationHistory"))
                    { this.nextState = LookupCustomerFlowState.ViewPersonalInformationHistory; }
                    break;

                default:

                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupCustomer");
            }
            senderNavBox.CustomDetail = "ViewCustomerInformationReadOnly";
            this.executeNextState();
        }
        /*__________________________________________________________________________________________*/
        private void UpdateCustomerDetailsFormNavAction(object sender, object data)
        {

            if (sender == null || data == null)
            {
                throw new ApplicationException("Customer Details form navigation action handler received invalid data");
            }

            NavBox senderNavBox = (NavBox)sender;
            UpdateCustomerDetails UpdateCustomerDetailsForm = (UpdateCustomerDetails)data;
            NavBox.NavAction action = senderNavBox.Action;


            switch (action)
            {
                case NavBox.NavAction.CANCEL:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LookupCustomerFlowState.LookupCustomer;
                    break;

                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupCustomerFlowState.ViewCustomerInformationReadOnly;
                    break;

                case NavBox.NavAction.SUBMIT:

                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    string ButtonSelect = senderNavBox.CustomDetail;
                    if (ButtonSelect.Equals("UpdateCustomerStatus"))
                    { this.nextState = LookupCustomerFlowState.UpdateCustomerStatus; }
                    else if (ButtonSelect.Equals("ViewPersonalInformationHistory"))
                    { this.nextState = LookupCustomerFlowState.ViewPersonalInformationHistory; }
                    else if (ButtonSelect.Equals("SupportCustomerComment"))
                    { this.nextState = LookupCustomerFlowState.SupportCustomerComment; }

                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Details");
            }

            this.executeNextState();
        }

        /*__________________________________________________________________________________________*/
        private void SupportCustomerCommentFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Customer Details form navigation action handler received invalid data");
            }

            NavBox senderNavBox = (NavBox)sender;
            SupportCustomerComment SupportCustomerCommentForm = (SupportCustomerComment)data;
            NavBox.NavAction action = senderNavBox.Action;  //UpdateCustomerDetailsNavBox.Action;
            switch (action)
            {
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LookupCustomerFlowState.UpdateCustomerDetails;
                    break;

                case NavBox.NavAction.SUBMIT:
                    //GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    //this.nextState = LookupCustomerFlowState.UpdateCustomerStatus; 
                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Status");
            }
            senderNavBox.CustomDetail = "SupportCustomerComment";
            this.executeNextState();
        }
        /// <summary>
        /// NavBox OnAction Handler for Update Address
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /*__________________________________________________________________________________________*/
        private void updateAddressFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Update Address form navigation action handler received invalid data");
            }

            NavBox senderNavBox = (NavBox)sender;
            UpdateAddress addrForm = (UpdateAddress)data;
            NavBox.NavAction lookupAction = senderNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupCustomerFlowState.ViewCustomerInformationReadOnly;
                    break;
                #region obsolete
                //this.nextState = LookupCustomerFlowState.AddCustomer;
                //break;
                //case NavBox.NavAction.BACKANDSUBMIT:
                //    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                //    this.nextState = LookupCustomerFlowState.ViewPawnCustomerInfo;
                //    break;
                //case NavBox.NavAction.HIDEANDSHOW:
                //    addrForm.Hide();
                //    this.nextState = LookupCustomerFlowState.UpdatePhysicalDescription;
                //    break;
                #endregion
                case NavBox.NavAction.CANCEL:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LookupCustomerFlowState.LookupCustomer;
                    break;

                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Update Address");
            }
            senderNavBox.CustomDetail = "UpdateAddress";
            this.executeNextState();
        }
        /*__________________________________________________________________________________________*/
        private void UpdateCustomerStatusFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Customer Details form navigation action handler received invalid data");
            }

            NavBox senderNavBox = (NavBox)sender;
            UpdateCustomerStatus UpdateCustomerStatusForm = (UpdateCustomerStatus)data;
            NavBox.NavAction action = senderNavBox.Action;  //UpdateCustomerDetailsNavBox.Action;
            switch (action)
            {
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LookupCustomerFlowState.UpdateCustomerDetails;
                    break;

                case NavBox.NavAction.SUBMIT:
                    //GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    //this.nextState = LookupCustomerFlowState.UpdateCustomerStatus; 
                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Status");
            }
            senderNavBox.CustomDetail = "UpdateCustomerStatus";
            this.executeNextState();
        }
        /*__________________________________________________________________________________________*/
        private void UpdateCustomerContactDetailsFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Customer Details form navigation action handler received invalid data");
            }

            NavBox senderNavBox = (NavBox)sender;
            UpdateCustomerContactDetails UpdateCustomerContactDetailsForm = (UpdateCustomerContactDetails)data;
            NavBox.NavAction action = senderNavBox.Action;
            switch (action)
            {
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LookupCustomerFlowState.ViewCustomerInformationReadOnly;
                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Status");
            }
            senderNavBox.CustomDetail = "UpdateCustomerContactDetails";
            this.executeNextState();
        }
        /*__________________________________________________________________________________________*/
        private void UpdateCommentsandNotesFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Customer Comments and Notes form navigation action handler received invalid data");
            }

            NavBox senderNavBox = (NavBox)sender;
            UpdateCommentsandNotes UpdateCommentsandNotesForm = (UpdateCommentsandNotes)data;
            NavBox.NavAction action = senderNavBox.Action;
            switch (action)
            {
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupCustomerFlowState.ViewCustomerInformationReadOnly;
                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Comments and Notes");
            }
            senderNavBox.CustomDetail = "UpdateCommentsandNotes";
            this.executeNextState();
        }

        /*__________________________________________________________________________________________*/
        private void ViewPersonalInformationHistoryFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Customer Comments and Notes form navigation action handler received invalid data");
            }

            NavBox senderNavBox = (NavBox)sender;
            ViewPersonalInformationHistory ViewPersonalInformationHistoryForm = (ViewPersonalInformationHistory)data;
            NavBox.NavAction action = senderNavBox.Action;
            switch (action)
            {
                case NavBox.NavAction.BACK:

                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();

                    string parentForm = senderNavBox.CustomDetail;

                    if (parentForm.Equals("UpdateCustomerDetails"))
                        this.nextState = LookupCustomerFlowState.UpdateCustomerDetails;
                    else if (parentForm.Equals("ViewCustomerInformationReadOnly"))
                        this.nextState = LookupCustomerFlowState.ViewCustomerInformationReadOnly;

                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Personal Information History");
            }
            senderNavBox.CustomDetail = "ViewPersonalInformationHistory";
            this.executeNextState();
        }
        /*__________________________________________________________________________________________*/
        private void UpdateCustomerIdentificationFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Customer Comments and Notes form navigation action handler received invalid data");
            }

            NavBox senderNavBox = (NavBox)sender;
            UpdateCustomerIdentification UpdateCustomerIdentificationForm = (UpdateCustomerIdentification)data;
            NavBox.NavAction action = senderNavBox.Action;
            switch (action)
            {
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupCustomerFlowState.ViewCustomerInformationReadOnly;
                    break;
                case NavBox.NavAction.SUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupCustomerFlowState.ViewCustomerInformationReadOnly;
                    break;
                case NavBox.NavAction.RETRY:

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Comments and Notes");
            }
            senderNavBox.CustomDetail = "UpdateCustomerIdentification";
            this.executeNextState();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /*__________________________________________________________________________________________*/
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
        /// Action handler for ManagePawnApplication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /*__________________________________________________________________________________________*/
        private void managePawnAppFormNavAction(object sender, object data)
        {
            /*     if (sender == null || data == null)
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
                 this.executeNextState(); */
        }
        /// <summary>
        /// NavBox OnAction Handler for Create Customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /*__________________________________________________________________________________________*/
        private void createCustFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Create Customer form navigation action handler received invalid data");
            }

            /*          NavBox createCustNavBox = (NavBox)sender;
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

                      this.executeNextState();*/
        }

        /// <summary>
        /// NavBox OnAction Handler for Update Physical Description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /*__________________________________________________________________________________________*/
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

        //Controller_ProductServices
        /*__________________________________________________________________________________________*/
        private void Controller_ProductServicesFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Product & Service navigation action handler received invalid data");
            }

            NavBox physDescNavBox = (NavBox)sender;
            Controller_ProductServices physDescForm = (Controller_ProductServices)data;
            NavBox.NavAction lookupAction = physDescNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.BACK:
                    this.nextState = LookupCustomerFlowState.Controller_ProductServices;
                    break;
                //case NavBox.NavAction.BACKANDSUBMIT:
                //    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                //    this.nextState = LookupCustomerFlowState.ViewPawnCustomerInfo;
                //    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupCustomerFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Update Physical Description");
            }

            this.executeNextState();
        }
        #endregion


    }
}

