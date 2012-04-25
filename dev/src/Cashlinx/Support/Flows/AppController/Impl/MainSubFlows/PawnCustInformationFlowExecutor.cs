//#define DisableTabs
using System;
using System.Windows.Forms;
using Support.Forms.Customer;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
//using Common.Libraries.Forms;
using Support.Libraries.Forms;
using Common.Libraries.Utility.Shared;
using Support.Flows.AppController.Impl.Common;
//using Pawn.Forms.Pawn.Customer.Stats;
//using Pawn.Forms.Pawn.ItemHistory;
//using Pawn.Forms.Pawn.Products.ProductDetails;
//using Pawn.Forms.Pawn.Products.ProductHistory;
//using Pawn.Forms.Pawn.Services.Receipt;
using Support.Forms.Customer.Products;
using Support.Forms.Pawn.Customer;
using Support.Logic;
//using Support.Logic.DesktopProcedures;

namespace Support.Flows.AppController.Impl.MainSubFlows
{
    public class PawnCustInformationFlowExecutor : SingleExecuteBlock
    {
        public const string NAME = "PawnCustInformationFlowExecutor";
        public const string MMPIFUNCTIONALITYNAME = "mmpi";
        public const string CUSTOMERPRODUCTS = "Controller_ProductServices";
        public const string LOOKUPCUSTOMER = "custmaint";

        public enum PawnCustInformationFlowState
        {
            ViewCustomerInformationReadOnly, // TODO CLEANUP NAME FOR READABILITY
            UpdateCustomerDetails,
            UpdateCustomerStatus,
            SupportCustomerComment,
            UpdateCustomerContactDetails,
            UpdateCommentsandNotes,
            UpdateCustomerIdentification,
            ViewPersonalInformationHistory,
            Controller_ProductServices,
            ProductServices,
            ViewPawnCustomerInfo, 
            UpdateAddress, // WCM 4/20/12 From here and above moved from LookupCustomerFlowExecutor.cs
            ViewReadOnlyCustomerInformation,        
            ViewCustomerInformation,
            ViewPawnCustomerProductDetails,
            ViewPawnCustomerStats,
            ItemHistory,
            InvokeMMPIChildFlow,
            ProductHistory,
            ViewReceipt,
            ManagePawnApplication,
            ExistingCustomer,
            TenderIn,
            ExitFlow,
            CancelFlow,
            Cancel,
            Error
        }

        private PawnCustInformationFlowState nextState;
        private Form parentForm;
        private SingleExecuteBlock parentFlow;
        private FxnBlock endStateNotifier;
        #region FlowExecutor & ExecuteNextState
        /*__________________________________________________________________________________________*/
        public PawnCustInformationFlowExecutor(Form parentForm, FxnBlock eStateNotifier, SingleExecuteBlock parentFlow)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = FindStateByTabClicked();
            this.parentFlow = parentFlow;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }
        /*__________________________________________________________________________________________*/
        private void executeNextState()
        {
            object evalExecFlag = this.executorFxn(this.nextState);
            if (evalExecFlag == null || ((bool)(evalExecFlag)) == false)
            {
                throw new ApplicationException("Cannot execute the next state: " + this.nextState.ToString());
            }
        }
        // WCM 4/18/12 Pulled code from PawnCustomerInformationFlowExecutor and modified 
        /*__________________________________________________________________________________________*/
        private static PawnCustInformationFlowState FindStateByTabClicked()
        {

            // WCM 4/18/12 GlobalDataAccessor.Instance.DesktopSession.TabStateClicked replace by CashlinxPawnSupportSession.Instance.TabStateClicked
            if (CashlinxPawnSupportSession.Instance.TabStateClicked == FlowTabController.State.ProductHistory)
            { return PawnCustInformationFlowState.Controller_ProductServices; }
            else if (CashlinxPawnSupportSession.Instance.TabStateClicked == FlowTabController.State.ItemHistory)
                return PawnCustInformationFlowState.Controller_ProductServices;
            else if (CashlinxPawnSupportSession.Instance.TabStateClicked == FlowTabController.State.ProductsAndServices)
                return PawnCustInformationFlowState.Controller_ProductServices;
            else if (CashlinxPawnSupportSession.Instance.TabStateClicked == FlowTabController.State.Customer)
                return PawnCustInformationFlowState.Controller_ProductServices;
            else if (CashlinxPawnSupportSession.Instance.TabStateClicked == FlowTabController.State.Stats)
                return PawnCustInformationFlowState.Controller_ProductServices;

            return PawnCustInformationFlowState.ViewCustomerInformationReadOnly; 

        }

        private void ReSetTabs()
        {
#if !DisableTags
            // disable tabs here!
            ShowForm viewCustInfoReadOnlyBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustomerInformationFormNavAction);
            CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, viewCustInfoReadOnlyBlk.ClassForm, FlowTabController.State.Customer);
#endif
        }

        /// <summary>
        /// Main execution function for NewPawnLoanFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        /*__________________________________________________________________________________________*/
        private object executorFxn(object inputData)
        {
            if (inputData == null)
                inputData = FindStateByTabClicked();

            PawnCustInformationFlowState inputState = (PawnCustInformationFlowState)inputData;

            switch (inputState)
            {
                #region SWITCH STATEMENTS MOVED
                /*_________________________________________________*/
                case PawnCustInformationFlowState.ViewCustomerInformationReadOnly:

                    ShowForm viewCustInfoReadOnlyBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustomerInformationFormNavAction);

                    if (!viewCustInfoReadOnlyBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute View customer information block");
                    }

#if !DisableTabs
                    // disable tabs here!
                    CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, viewCustInfoReadOnlyBlk.ClassForm, FlowTabController.State.Customer);
                    SetTabsInForm();
#else
                    CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, viewCustInfoReadOnlyBlk.ClassForm, FlowTabController.State.Customer);
                    CommonAppBlocks.Instance.HideTabInFlowTab(FlowTabController.State.ItemHistory);
                    CommonAppBlocks.Instance.HideTabInFlowTab(FlowTabController.State.ProductsAndServices);
                    CommonAppBlocks.Instance.HideTabInFlowTab(FlowTabController.State.ProductHistory);
                    CommonAppBlocks.Instance.HideTabInFlowTab(FlowTabController.State.Stats);   ((ViewCustomerInformation)viewCustInfoReadOnlyBlk.ClassForm).ShowReadOnly = true;
#endif

                    //((ViewCustomerInformation)viewCustInfoReadOnlyBlk.ClassForm).ShowReadOnly = false;

                    break;
                /*_________________________________________________*/
                case PawnCustInformationFlowState.UpdateCustomerDetails:

                    ShowForm UpdateCustomerDetailsResBlk = CommonAppBlocks.Instance.UpdateCustomerDetailsShowBlock(this.parentForm, this.UpdateCustomerDetailsFormNavAction);
                    if (!UpdateCustomerDetailsResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerDetails block");
                    }
                    break;
                /*_________________________________________________*/
                case PawnCustInformationFlowState.UpdateCustomerStatus:
                    ShowForm UpdateCustomerStatusResBlk = CommonAppBlocks.Instance.UpdateCustomerStatusShowBlock(this.parentForm, this.UpdateCustomerStatusFormNavAction);
                    if (!UpdateCustomerStatusResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerStatus block");
                    }
                    break;
                /*_________________________________________________*/
                case PawnCustInformationFlowState.SupportCustomerComment:
                    ShowForm SupportCustomerCommentResBlk = CommonAppBlocks.Instance.SupportCustomerCommentShowBlock(this.parentForm, this.SupportCustomerCommentFormNavAction);
                    if (!SupportCustomerCommentResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerStatus block");
                    }
                    break;

                /*_________________________________________________*/
                case PawnCustInformationFlowState.UpdateCustomerContactDetails:
                    ShowForm UpdateCustomerContactDetailsResBlk = CommonAppBlocks.Instance.UpdateCustomerContactDetailsShowBlock(this.parentForm, this.UpdateCustomerContactDetailsFormNavAction);
                    if (!UpdateCustomerContactDetailsResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerContactDetails block");
                    }
                    break;
                /*_________________________________________________*/
                case PawnCustInformationFlowState.UpdateCommentsandNotes:
                    ShowForm UpdateCommentsandNotesResBlk = CommonAppBlocks.Instance.UpdateCommentsandNotesShowBlock(this.parentForm, this.UpdateCommentsandNotesFormNavAction);
                    if (!UpdateCommentsandNotesResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerContactDetails block");
                    }
                    break;
                /*_________________________________________________*/
                case PawnCustInformationFlowState.UpdateCustomerIdentification:
                    ShowForm UpdateCustomerIdentificationResBlk = CommonAppBlocks.Instance.UpdateCustomerIdentificationShowBlock(this.parentForm, this.UpdateCustomerIdentificationFormNavAction);
                    if (!UpdateCustomerIdentificationResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerContactDetails block");
                    }
                    break;
                /*_________________________________________________*/
                case PawnCustInformationFlowState.ViewPersonalInformationHistory:
                    ShowForm ViewPersonalInformationHistoryResBlk = CommonAppBlocks.Instance.ViewPersonalInformationHistoryShowBlock(this.parentForm, this.ViewPersonalInformationHistoryFormNavAction);
                    if (!ViewPersonalInformationHistoryResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute CustomerContactDetails block");
                    }
                    break;
    

    
                #endregion
                /*_________________________________________________*/
                //case PawnCustInformationFlowState.ViewReadOnlyCustomerInformation:

                //    ShowForm viewCustInfoReadOnlyBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustFormNavAction);
                //    if (!viewCustInfoReadOnlyBlk.execute())
                //    {
                //        throw new ApplicationException("Cannot execute View customer information block");
                //    }
                //    CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, viewCustInfoReadOnlyBlk.ClassForm, FlowTabController.State.Customer);
                //    SetTabsInForm();
                //    ((ViewCustomerInformation)viewCustInfoReadOnlyBlk.ClassForm).ShowReadOnly = true;
                //    break;
                /*_________________________________________________*/
                //case PawnCustInformationFlowState.ViewCustomerInformation:

                //    ShowForm viewCustInfoBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustFormNavAction);
                //    if (!viewCustInfoBlk.execute())
                //    {
                //        throw new ApplicationException("Cannot execute View customer information block");
                //    }
                //    LoadCustomerLoanKeys loanKeysBlk = new LoadCustomerLoanKeys();
                //    if (!loanKeysBlk.execute())
                //    {
                //        throw new ApplicationException("Cannot get Loan keys for selected customer");
                //    }
                //    CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, viewCustInfoBlk.ClassForm, FlowTabController.State.Customer);
                //    break;
                /*_________________________________________________*/
                case PawnCustInformationFlowState.UpdateAddress:
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
                case PawnCustInformationFlowState.ProductServices:

                        LoadCustomerLoanKeys loanKeysDataBlk = new LoadCustomerLoanKeys();
                        if (!loanKeysDataBlk.execute())
                        {
                            throw new ApplicationException("Cannot get Loan keys for selected customer");
                        }
                        ShowForm Controller_ProductServicesFrmBlk =
                             CommonAppBlocks.Instance.Controller_ProductServicesShowBlock(this.parentForm,
                                                                            this.Controller_ProductServicesFormNavAction);

                        if (!Controller_ProductServicesFrmBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute View Pawn Customer Product Details block");
                        }

                        CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm,
                                                                            Controller_ProductServicesFrmBlk.ClassForm,
                                                                            FlowTabController.State.ProductsAndServices);

                    break;


                #region OBSOLETE CODE
                /* case PawnCustInformationFlowState.ViewPawnCustomerProductDetails:
                     //If form already there in session then show that else open a new one
                     Controller_ProductServices productServFrm = new Controller_ProductServices();
                     Form currentForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(productServFrm);
                     if (currentForm.GetType() == typeof(Controller_ProductServices))
                     {
                         currentForm.Show();
                         currentForm.Activate();
                         CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, currentForm, FlowTabController.State.ProductsAndServices);
                         if (GlobalDataAccessor.Instance.DesktopSession.PickupProcessContinue)
                         {
                             GlobalDataAccessor.Instance.DesktopSession.LockProductsTab = true;
                             ((Controller_ProductServices)currentForm).NavControlBox.IsCustom = true;
                             ((Controller_ProductServices)currentForm).NavControlBox.CustomDetail = "LoanService";
                             ((Controller_ProductServices)currentForm).NavControlBox.Action = NavBox.NavAction.SUBMIT;
                             ((Controller_ProductServices)currentForm).ContinuePickup = true;

                         }


                     }
                     else
                     {

                         //Get the loan keys for the selected customer
                         //Before calling view pawn cust prod details
                         LoadCustomerLoanKeys loanKeysDataBlk = new LoadCustomerLoanKeys();
                         if (!loanKeysDataBlk.execute())
                         {
                             throw new ApplicationException("Cannot get Loan keys for selected customer");
                         }

                         ShowForm pawnCustProdDetBlk =
                             CommonAppBlocks.Instance.CreateProductServicesBlock(this.parentForm,
                                                                                 this.productServicesFormNavAction);
                         if (!pawnCustProdDetBlk.execute())
                         {
                             throw new ApplicationException("Cannot execute View Pawn Customer Product Details block");
                         }

                         CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, pawnCustProdDetBlk.ClassForm,
                                                                        FlowTabController.State.ProductsAndServices);
                     }

                     break; 
                 case PawnCustInformationFlowState.ViewPawnCustomerStats:

                     ShowForm pawnCustStatsBlk = CommonAppBlocks.Instance.CreateStatsBlock(this.parentForm, this.productStatsFormNavAction);
                     if (!pawnCustStatsBlk.execute())
                     {
                         throw new ApplicationException("Cannot execute View Pawn Customer Stats block");
                     }
                     CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, pawnCustStatsBlk.ClassForm, FlowTabController.State.Stats);
                     SetTabsInForm();
                     break;
                 case PawnCustInformationFlowState.ItemHistory:
                     ShowForm itemHistBlk = CommonAppBlocks.Instance.CreateItemHistoryShowBlock(this.parentForm, this.itemHistoryFormNavAction);
                     if (!itemHistBlk.execute())
                     {
                         throw new ApplicationException("Cannot execute Item History block");
                     }
                     CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, itemHistBlk.ClassForm, FlowTabController.State.ItemHistory);
                     SetTabsInForm();
                     break;
                 case PawnCustInformationFlowState.ProductHistory:
                     ShowForm prodHistBlk = CommonAppBlocks.Instance.CreateProductHistoryShowBlock(this.parentForm, this.productHistoryFormNavAction);
                     if (!prodHistBlk.execute())
                     {
                         throw new ApplicationException("Cannot execute Product History block");
                     }
                     CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, prodHistBlk.ClassForm, FlowTabController.State.ProductHistory);
                     SetTabsInForm();
                     break;

                 case PawnCustInformationFlowState.ViewReceipt:
                     ViewReceipt receiptForm = new ViewReceipt();
                     Form recptForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(receiptForm);
                     if (recptForm.GetType() == typeof(ViewReceipt))
                     {
                         recptForm.BringToFront();
                     }
                     else
                     {

                         ShowForm viewReceiptBlk =
                             CommonAppBlocks.Instance.CreateViewReceiptBlock(
                             this.parentForm, this.viewReceiptFormNavAction);
                         if (!viewReceiptBlk.execute())
                         {
                             throw new ApplicationException("Cannot execute view receipt block");
                         }
                     }
                     break;
                 case PawnCustInformationFlowState.InvokeMMPIChildFlow:
                     //Initiate the child workflow
                     CommonAppBlocks.Instance.HideFlowTabController();
                     GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(MMPIFUNCTIONALITYNAME,
                         this.parentForm, this.endStateNotifier, this);
                     break;
                 case PawnCustInformationFlowState.ManagePawnApplication:
                     ShowForm managePawnAppBlock = CommonAppBlocks.Instance.CreateManagePawnApplicationBlock(this.parentForm, this.managePawnAppFormNavAction);
                     if (!managePawnAppBlock.execute())
                     {
                         throw new ApplicationException("Cannot execute Manage Pawn Applicaction block");
                     }
                     break;
                 case PawnCustInformationFlowState.ExistingCustomer:
                     ShowForm existCustBlk = CommonAppBlocks.Instance.CreateExistingCustomerBlock(this.parentForm, this.existCustomerFormNavAction);
                     if (!existCustBlk.execute())
                     {
                         throw new ApplicationException("Cannot execute ExistingCustomer block");
                     }
                     break;
                 case PawnCustInformationFlowState.TenderIn:
                         ShowForm tenderInBlk = CommonAppBlocks.Instance.TenderInShowBlock(this.parentForm, this.TenderInFormAction);
                         if (!tenderInBlk.execute())
                         {
                             throw new ApplicationException("Cannot execute Tender In block");
                         }
  

                     break;
                     */
                #endregion
                /*_________________________________________________*/
                case PawnCustInformationFlowState.CancelFlow:
                    CommonAppBlocks.Instance.HideFlowTabController();
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
                /*_________________________________________________*/
                case PawnCustInformationFlowState.ExitFlow:
                    if (parentFlow != null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                        CommonAppBlocks.Instance.HideFlowTabController();
                        this.parentFlow.execute();

                    }
                    break;

                default:
                    throw new ApplicationException("Invalid Pawn Cust Information flow state");
            }

            return (true);
        }
        /*__________________________________________________________________________________________*/
        private void SetTabsInForm()
        {
            if (GlobalDataAccessor.Instance.DesktopSession.ShowOnlyHistoryTabs)
            {
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Customer, false);
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductsAndServices, false);
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Stats, false);
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ItemHistory, true);
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductHistory, true);

            }
            else
            {
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Customer, true);
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductsAndServices,true);
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Stats, true);
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ItemHistory, true);
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductHistory, true);
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Comments, true);
            }
            if (parentFlow.Name.Equals("NewPawnLoanFlowExecutor"))
            {
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Customer, true);
                CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductsAndServices,
                                           false);

            }
        }
        #endregion

        
        #region FormNavAction

        #region CODE MOVE FROM LOOKUPCUSTOMERFLOWEXECUTOR

        /*__________________________________________________________________________________________*/
        private void viewCustomerInformationFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("View Customer form navigation action handler received invalid data");
            }
            NavBox senderNavBox = (NavBox)sender;
            ViewCustomerInformation viewCustForm = (ViewCustomerInformation)data;
            NavBox.NavAction action = senderNavBox.Action;
            switch (action)
            {
                case NavBox.NavAction.BACK:
                    this.nextState = PawnCustInformationFlowState.ExitFlow; // exit this flow

                    break;

                case NavBox.NavAction.SUBMIT:
                    CommonAppBlocks.Instance.HideFlowTabController();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();

                    string linkLabel = senderNavBox.CustomDetail;
                    if (linkLabel.Equals("UpdateCustomerDetails"))
                    { this.nextState = PawnCustInformationFlowState.UpdateCustomerDetails; }
                    else if (linkLabel.Equals("UpdateAddress"))
                    { this.nextState = PawnCustInformationFlowState.UpdateAddress; }
                    else if (linkLabel.Equals("UpdateCustomerContactDetails"))
                    { this.nextState = PawnCustInformationFlowState.UpdateCustomerContactDetails; }
                    else if (linkLabel.Equals("UpdateCommentsandNotes"))
                    { this.nextState = PawnCustInformationFlowState.UpdateCommentsandNotes; }
                    else if (linkLabel.Equals("UpdateCustomerIdentification"))
                    { this.nextState = PawnCustInformationFlowState.UpdateCustomerIdentification; }
                    else if (linkLabel.Equals("ViewPersonalInformationHistory"))
                    { this.nextState = PawnCustInformationFlowState.ViewPersonalInformationHistory; }
                    else if (linkLabel.Equals("ProductsAndServices"))
                    { this.nextState = PawnCustInformationFlowState.ProductServices; }
                    else if (linkLabel.Equals("SupportCustomerComment"))
                    { this.nextState = PawnCustInformationFlowState.SupportCustomerComment; }
                    break;

                default:

                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupCustomer");
            }

            this.executeNextState();
        }
        /*__________________________________________________________________________________________*/
        private void UpdateCustomerDetailsFormNavAction(object sender, object data)
        {

            bool RePaintTheTabs = false; 

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
                    this.nextState = PawnCustInformationFlowState.ExitFlow; // exit this flow
                    break;

                case NavBox.NavAction.BACK:

                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    RePaintTheTabs = true;
                    this.nextState = PawnCustInformationFlowState.ViewCustomerInformationReadOnly;
                    break;

                case NavBox.NavAction.SUBMIT:

                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    string ButtonSelect = senderNavBox.CustomDetail;
                    if (ButtonSelect.Equals("UpdateCustomerStatus"))
                    { this.nextState = PawnCustInformationFlowState.UpdateCustomerStatus; }
                    else if (ButtonSelect.Equals("ViewPersonalInformationHistory"))
                    {
                        this.nextState = PawnCustInformationFlowState.ViewPersonalInformationHistory; 
                    }
                    else if (ButtonSelect.Equals("SupportCustomerComment"))
                    { this.nextState = PawnCustInformationFlowState.SupportCustomerComment; }

                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Details");
            }

            if (RePaintTheTabs)
                this.ReSetTabs();

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
            NavBox.NavAction action = senderNavBox.Action;  
            switch (action)
            {
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = PawnCustInformationFlowState.ViewCustomerInformationReadOnly;
                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Status");
            }

            this.executeNextState();
        }
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
                    this.ReSetTabs();
                    this.nextState = PawnCustInformationFlowState.ViewCustomerInformationReadOnly;
                    break;
                case NavBox.NavAction.CANCEL:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = PawnCustInformationFlowState.ExitFlow; // exit this flow
                    break;

                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Update Address");
            }

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
            NavBox.NavAction action = senderNavBox.Action; 
            switch (action)
            {
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = PawnCustInformationFlowState.UpdateCustomerDetails;
                    break;

                case NavBox.NavAction.SUBMIT:
                    //GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    //this.nextState = PawnCustInformationFlowState.UpdateCustomerStatus; 
                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Status");
            }

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
                    this.nextState = PawnCustInformationFlowState.ViewCustomerInformationReadOnly;
                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Status");
            }

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
                    this.nextState = PawnCustInformationFlowState.ViewCustomerInformationReadOnly;
                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Comments and Notes");
            }

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
                        this.nextState = PawnCustInformationFlowState.UpdateCustomerDetails;
                    else if (parentForm.Equals("ViewCustomerInformationReadOnly"))
                        this.nextState = PawnCustInformationFlowState.ViewCustomerInformationReadOnly;

                    break;

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Personal Information History");
            }

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
                    this.nextState = PawnCustInformationFlowState.ViewCustomerInformationReadOnly;
                    break;
                case NavBox.NavAction.SUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = PawnCustInformationFlowState.ViewCustomerInformationReadOnly;
                    break;
                case NavBox.NavAction.RETRY:

                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Customer Comments and Notes");
            }

            this.executeNextState();
        }
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
                    this.nextState = PawnCustInformationFlowState.UpdateAddress;
                    break;
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = PawnCustInformationFlowState.ViewPawnCustomerInfo;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PawnCustInformationFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Update Physical Description");
            }

            this.executeNextState();
        }
        /*__________________________________________________________________________________________*/
        private void Controller_ProductServicesFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Product & Service navigation action handler received invalid data");
            }

            NavBox senderNavBox = (NavBox)sender;
            Controller_ProductServices physDescForm = (Controller_ProductServices)data;
            NavBox.NavAction Action = senderNavBox.Action;
            switch (Action)
            {
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    string ButtonSelect = senderNavBox.CustomDetail;
                    if (ButtonSelect.Equals("ViewPersonalInformationHistory"))
                    {
                        this.ReSetTabs();
                        this.nextState = PawnCustInformationFlowState.ViewCustomerInformationReadOnly;
                    }
                    else if (ButtonSelect.Equals("SupportCustomerComment"))
                    {
                        this.nextState = PawnCustInformationFlowState.SupportCustomerComment;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PawnCustInformationFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + Action.ToString() + " is not a valid state for Update Physical Description");
            }

            this.executeNextState();
        }
        #endregion
        #endregion
        #region OBSOLETE CODE
        /*     private void productServicesFormNavAction(object sender, object data)
             {
                 if (sender == null || data == null)
                 {
                     throw new ApplicationException("Product Services form navigation action handler received invalid data");
                 }
                 NavBox prodsrvNavBox = (NavBox)sender;
                 Controller_ProductServices prodSrvForm = (Controller_ProductServices)data;
                 NavBox.NavAction lookupAction = prodsrvNavBox.Action;
                 switch (lookupAction)
                 {
                     case NavBox.NavAction.SUBMIT:
                         if (prodsrvNavBox.IsCustom)
                         {
                             string custDet = prodsrvNavBox.CustomDetail;
                             if (custDet.Equals("LoanService", StringComparison.OrdinalIgnoreCase))
                             {
                                 if (GlobalDataAccessor.Instance.DesktopSession.LockProductsTab)
                                 {
                                     CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Customer, false);
                                     CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductsAndServices, true);
                                     CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Stats, false);
                                     CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ItemHistory, false);
                                     CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductHistory, false);

                                 }
                                 else
                                 {
                                     CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Customer, true);
                                     CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductsAndServices, true);
                                     CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Stats, true);
                                     CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ItemHistory, true);
                                     CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductHistory, true);

                                 }
                                 return;

                             }
                             if (custDet.Equals("Reload", StringComparison.OrdinalIgnoreCase))
                             {
                                 GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                                 CommonAppBlocks.Instance.HideFlowTabController();
                                 this.nextState = PawnCustInformationFlowState.ViewPawnCustomerProductDetails;
                             }
                             else if (custDet.Equals("ManagePawnApplication", StringComparison.OrdinalIgnoreCase))
                             {
                                 GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.VIEWPAWNCUSTPRODDETAILS;
                                 prodSrvForm.Hide();
                                 CommonAppBlocks.Instance.HideFlowTabController();
                                 this.nextState = PawnCustInformationFlowState.ManagePawnApplication;
                             }
                             else if (custDet.Equals("ProcessTender", StringComparison.OrdinalIgnoreCase))
                             {
                                 CommonAppBlocks.Instance.HideFlowTabController();
                                 GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                                 this.nextState = PawnCustInformationFlowState.TenderIn;
                             }

                             else
                                 this.nextState = PawnCustInformationFlowState.ViewReceipt;
                         }
                         else
                             this.nextState = PawnCustInformationFlowState.CancelFlow;
                         break;
                     case NavBox.NavAction.BACKANDSUBMIT:
                         GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                         if (prodsrvNavBox.IsCustom)
                         {
                             string custDet = prodsrvNavBox.CustomDetail;
                             if (custDet.Equals("ProductStats"))
                             {
                                 this.nextState = PawnCustInformationFlowState.ViewPawnCustomerStats;
                             }
                             else if (custDet.Equals("Customer"))
                                 this.nextState = PawnCustInformationFlowState.ViewCustomerInformation;
                             else if (custDet.Equals("ItemHistory"))
                                 this.nextState = PawnCustInformationFlowState.ItemHistory;
                             else if (custDet.Equals("ProductHistory"))
                                 this.nextState = PawnCustInformationFlowState.ProductHistory;

                         }
                         break;
                     case NavBox.NavAction.BACK:
                         if (prodsrvNavBox.IsCustom && prodsrvNavBox.CustomDetail.Equals("Menu", StringComparison.OrdinalIgnoreCase))
                         {
                             this.nextState = PawnCustInformationFlowState.CancelFlow;
                         }
                         else
                         {
                             GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                             CommonAppBlocks.Instance.HideFlowTabController();
                             this.nextState = PawnCustInformationFlowState.ExitFlow;
                         }
                         break;
                     default:
                         throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Products and services");
                 }

                 this.executeNextState();

             } 

             /// <summary>
             /// NavBox OnAction Handler for View Receipt
             /// </summary>
             /// <param name="sender"></param>
             /// <param name="data"></param>
             private void viewReceiptFormNavAction(object sender, object data)
             {
                 if (sender == null || data == null)
                 {
                     throw new ApplicationException("View receipt form navigation action handler received invalid data");
                 }

                 NavBox viewReceiptNavBox = (NavBox)sender;
                 ViewReceipt viewReceiptForm = (ViewReceipt)data;
                 NavBox.NavAction viewAction = viewReceiptNavBox.Action;
                 GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                 switch (viewAction)
                 {
                     case NavBox.NavAction.CANCEL:

                         if (viewReceiptNavBox.IsCustom)
                         {
                             string custDet = viewReceiptNavBox.CustomDetail;
                             if (custDet.Equals("Back", StringComparison.OrdinalIgnoreCase))
                             {
                                 return;
                             }
                             else
                                 this.nextState = PawnCustInformationFlowState.CancelFlow;
                         }
                         else
                         {

                             this.nextState = PawnCustInformationFlowState.CancelFlow;
                         }
                         break;
                     case NavBox.NavAction.SUBMIT:
                         //this.nextState = PawnCustInformationFlowState.ViewReceipt;  
                         //The products and services page needs to be reloaded
                         //so another back to remove it from session
                         GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                         this.nextState = PawnCustInformationFlowState.ViewPawnCustomerProductDetails;
                         break;
                 }

                 this.executeNextState();
             }

             /// <summary>
             /// Form nav action for the Item History form
             /// </summary>
             /// <param name="sender"></param>
             /// <param name="data"></param>
             private void itemHistoryFormNavAction(object sender, object data)
             {
                 if (sender == null || data == null)
                 {
                     throw new ApplicationException("Item History form navigation action handler received invalid data");
                 }
                 NavBox itemHistNavBox = (NavBox)sender;
                 Controller_ItemHistory itemHistForm = (Controller_ItemHistory)data;
                 NavBox.NavAction lookupAction = itemHistNavBox.Action;
                 switch (lookupAction)
                 {
                     case NavBox.NavAction.BACKANDSUBMIT:
                         GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                         if (itemHistNavBox.IsCustom)
                         {
                             string custDet = itemHistNavBox.CustomDetail;
                             if (custDet.Equals("ProductsAndServices"))
                             {
                                 this.nextState = PawnCustInformationFlowState.ViewPawnCustomerProductDetails;
                             }
                             else if (custDet.Equals("Customer"))
                             {
                                 if (this.parentFlow.Name.Equals("NewPawnLoanFlowExecutor", StringComparison.OrdinalIgnoreCase))
                                     this.nextState = PawnCustInformationFlowState.ViewReadOnlyCustomerInformation;
                                 else
                        
                                 this.nextState = PawnCustInformationFlowState.ViewCustomerInformation;
                             }
                             else if (custDet.Equals("ProductStats"))
                                 this.nextState = PawnCustInformationFlowState.ViewPawnCustomerStats;
                             else if (custDet.Equals("ProductHistory"))
                                 this.nextState = PawnCustInformationFlowState.ProductHistory;
                             else if (custDet.Equals("AddNewLoan", StringComparison.OrdinalIgnoreCase))
                             {
                                 CommonAppBlocks.Instance.HideFlowTabController();
                                 GlobalDataAccessor.Instance.DesktopSession.StartNewPawnLoan = true;
                                 GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                                 this.nextState = PawnCustInformationFlowState.ExitFlow;
                             }

                         }
                         break;
                     case NavBox.NavAction.BACK:
                         if (itemHistNavBox.IsCustom && itemHistNavBox.CustomDetail.Equals("Menu", StringComparison.OrdinalIgnoreCase))
                         {
                             this.nextState = PawnCustInformationFlowState.CancelFlow;
                         }
                         else
                         {
                             GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                             if (this.parentFlow != null && this.parentFlow.Name.Equals("NewPawnLoanFlowExecutor", StringComparison.OrdinalIgnoreCase))
                             {
                                 GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = -1;
                                 GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                             }

                             this.nextState = PawnCustInformationFlowState.ExitFlow;
                         }
                         break;
                     default:
                         throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Item History");
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
                             this.nextState = PawnCustInformationFlowState.ExistingCustomer;
                         }
                         else if (custDet.Equals("ViewPawnCustomerProductDetails"))
                         {
                             this.nextState = PawnCustInformationFlowState.ViewPawnCustomerProductDetails;
                         }
                         else
                         {
                             this.nextState = PawnCustInformationFlowState.CancelFlow;
                         }
                         break;
                     case NavBox.NavAction.CANCEL:
                         this.nextState = PawnCustInformationFlowState.CancelFlow;
                         break;
                     case NavBox.NavAction.BACK:
                         GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                         this.nextState = PawnCustInformationFlowState.ViewPawnCustomerProductDetails;
                         break;
                     default:
                         throw new ApplicationException("" + action.ToString() + " is not a valid state for ManagePawnApplication");
                 }
                 this.executeNextState();
             }


             /// <summary>
             /// Form nav action for the Product History form
             /// </summary>
             /// <param name="sender"></param>
             /// <param name="data"></param>
             private void productHistoryFormNavAction(object sender, object data)
             {
                 if (sender == null || data == null)
                 {
                     throw new ApplicationException("Product History form navigation action handler received invalid data");
                 }
                 NavBox prodHistNavBox = (NavBox)sender;
                 Controller_ProductHistory prodHistForm = (Controller_ProductHistory)data;
                 NavBox.NavAction lookupAction = prodHistNavBox.Action;
                 switch (lookupAction)
                 {
                     case NavBox.NavAction.SUBMIT:
                         this.nextState = PawnCustInformationFlowState.ViewReceipt;
                         break;

                     case NavBox.NavAction.BACKANDSUBMIT:
                         GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                         if (prodHistNavBox.IsCustom)
                         {
                             string custDet = prodHistNavBox.CustomDetail;
                             if (custDet.Equals("ProductsAndServices"))
                             {
                                 this.nextState = PawnCustInformationFlowState.ViewPawnCustomerProductDetails;
                             }
                             else if (custDet.Equals("Customer"))
                             {
                                 if (this.parentFlow.Name.Equals("NewPawnLoanFlowExecutor", StringComparison.OrdinalIgnoreCase))
                                     this.nextState = PawnCustInformationFlowState.ViewReadOnlyCustomerInformation;
                                 else
                                     this.nextState = PawnCustInformationFlowState.ViewCustomerInformation;
                             }
                             else if (custDet.Equals("ProductStats"))
                                 this.nextState = PawnCustInformationFlowState.ViewPawnCustomerStats;
                             else if (custDet.Equals("ItemHistory"))
                                 this.nextState = PawnCustInformationFlowState.ItemHistory;
                             else if (custDet.Equals("AddNewLoan", StringComparison.OrdinalIgnoreCase))
                             {
                                 this.nextState = PawnCustInformationFlowState.InvokeMMPIChildFlow;
                             }

                         }
                         break;
                     case NavBox.NavAction.BACK:
                         if (prodHistNavBox.IsCustom && prodHistNavBox.CustomDetail.Equals("Menu",StringComparison.OrdinalIgnoreCase))
                         {
                             this.nextState = PawnCustInformationFlowState.CancelFlow;
                         }
                         else
                         {
                             GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                             if (this.parentFlow != null && this.parentFlow.Name.Equals("NewPawnLoanFlowExecutor", StringComparison.OrdinalIgnoreCase))
                             {
                                 GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                                 GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = -1;
                             }
                             else
                                 CommonAppBlocks.Instance.HideFlowTabController();

                             this.nextState = PawnCustInformationFlowState.ExitFlow;

                        
                        
                         }
                         break;
                     default:
                         throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Product History");
                 }

                 this.executeNextState();

             }
              */


        /*
        private void productStatsFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Stats form navigation action handler received invalid data");
            }
            NavBox prodstatsNavBox = (NavBox)sender;
            Controller_Stats prodStatsForm = (Controller_Stats)data;
            NavBox.NavAction lookupAction = prodstatsNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    if (prodstatsNavBox.IsCustom)
                    {
                        string custDet = prodstatsNavBox.CustomDetail;
                        if (custDet.Equals("ProductsAndServices"))
                        {
                            this.nextState = PawnCustInformationFlowState.ViewPawnCustomerProductDetails;
                        }
                        else if (custDet.Equals("Customer"))
                        {
                            if (this.parentFlow.Name.Equals("NewPawnLoanFlowExecutor", StringComparison.OrdinalIgnoreCase))
                                this.nextState = PawnCustInformationFlowState.ViewReadOnlyCustomerInformation;
                            else

                                this.nextState = PawnCustInformationFlowState.ViewCustomerInformation;
                        }
                        else if (custDet.Equals("ItemHistory"))
                            this.nextState = PawnCustInformationFlowState.ItemHistory;
                        else if (custDet.Equals("ProductHistory"))
                            this.nextState = PawnCustInformationFlowState.ProductHistory;

                    }
                    break;
                case NavBox.NavAction.BACK:
                    if (prodstatsNavBox.IsCustom && prodstatsNavBox.CustomDetail.Equals("Menu", StringComparison.OrdinalIgnoreCase))
                    {
                        this.nextState = PawnCustInformationFlowState.CancelFlow;
                    }
                    else
                    {

                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                        CommonAppBlocks.Instance.HideFlowTabController();
                        if (prodstatsNavBox.IsCustom && prodstatsNavBox.CustomDetail.Equals("Close", StringComparison.OrdinalIgnoreCase))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                            GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = -1;
                            this.nextState = PawnCustInformationFlowState.ExitFlow;
                            
                        }
                        else
                            this.nextState = PawnCustInformationFlowState.CancelFlow;

                        
                    }
                    break;
                case NavBox.NavAction.SUBMIT:
                    this.nextState = PawnCustInformationFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Stats");
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
                            nextState = PawnCustInformationFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ViewPawnCustomerInformation"))
                        {
                            nextState = PawnCustInformationFlowState.ViewCustomerInformation;
                        }

                    }
                    break;
                case NavBox.NavAction.BACK:
                    nextState = PawnCustInformationFlowState.ManagePawnApplication;
                    break;
                case NavBox.NavAction.CANCEL:
                    nextState = PawnCustInformationFlowState.ExitFlow;
                    break;
            }
            this.executeNextState();
        }


        private void TenderInFormAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Tender In form navigation action handler received invalid data");
            }
            NavBox tenderNavBox = (NavBox)sender;
            NavBox.NavAction action = tenderNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (tenderNavBox.IsCustom)
                    {
                        string custDet = tenderNavBox.CustomDetail;
                        if (custDet.Equals("ProcessTender"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                            GlobalDataAccessor.Instance.DesktopSession.showProcessTender(ProcessTenderProcedures.ProcessTenderMode.LAYPAYMENT);

                        }
                        this.nextState = PawnCustInformationFlowState.CancelFlow;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = PawnCustInformationFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Tender In");
            }
            this.executeNextState();

        }

        */

        #endregion



    }
}
