using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Utility.Shared;
using Pawn.Flows.AppController.Impl.Common;
using Pawn.Forms.Pawn.Customer;
using Pawn.Forms.Pawn.Customer.Stats;
using Pawn.Forms.Pawn.ItemHistory;
using Pawn.Forms.Pawn.Products.ProductDetails;
using Pawn.Forms.Pawn.Products.ProductHistory;
using Pawn.Forms.Pawn.Services.Receipt;
using Pawn.Forms.Pawn.Services.Ticket;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class LookupTicketFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "LookupTicketFlowExecutor";
        public static readonly string MMPIFUNCTIONALITYNAME = "mmpi";
        
        
        public enum LookupTicketFlowState
        {
            LookupTicket,
            LookupTicketResults,
            ViewCustomerInformation,
            ViewReadOnlyCustomerInformation,
            LookupCustomer,
            LookupCustomerResults,
            AddCustomer,
            UpdateAddress,
            UpdatePhysicalDescription,
            ManagePawnApplication,
            ExistingCustomer,
            ViewPawnCustomerProductDetails,
            ViewPawnCustomerStats,
            ItemHistory,
            InvokeMMPIChildFlow,
            ProductHistory,
            ViewReceipt,
            TenderIn,
            Cancel,
            Hide,
            Error
        }

        private LookupTicketFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;

        /// <summary>
        /// Main execution function for LookupTicketFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
            {
               inputData= FindStateByTabClicked();
               
                if (inputData == null)
                    return false;
            }
            LookupTicketFlowState inputState = (LookupTicketFlowState)inputData;

            switch (inputState)
            {
                case LookupTicketFlowState.LookupTicket:
                    if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup("LookupTicket"))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.VisibleForm("LookupTicket");
                    }
                    else
                    {
                        ShowForm lookupTktBlk = CommonAppBlocks.Instance.CreateLookupTicketShowBlock(this.parentForm, this.lookupTicketFormNavAction);
                        if (!lookupTktBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute LookupCustomerTicket block");
                        }
                    }

                    break;
                case LookupTicketFlowState.LookupTicketResults:
                    ShowForm lookupTktResBlk = CommonAppBlocks.Instance.CreateLookupTicketResultsShowBlock(this.parentForm, this.lookupTktResultsFormNavAction);
                    if (!lookupTktResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupTicketResults block");
                    }
                    break;
                case LookupTicketFlowState.ViewReadOnlyCustomerInformation:

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
                    ((ViewCustomerInformation) viewCustInfoReadOnlyBlk.ClassForm).ShowReadOnly = true;
                    break;

                case LookupTicketFlowState.ViewCustomerInformation:

                    ShowForm viewCustInfoBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustFormNavAction);
                    if (!viewCustInfoBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute View customer information block");
                    }
                    LoadCustomerLoanKeys loanKeysBlk = new LoadCustomerLoanKeys();
                    if (!loanKeysBlk.execute())
                    {
                        throw new ApplicationException("Cannot get Loan keys for selected customer");
                    }

                    CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, viewCustInfoBlk.ClassForm, FlowTabController.State.Customer);
                    break;
                case LookupTicketFlowState.LookupCustomer:
                    ShowForm lookupCustBlk = CommonAppBlocks.Instance.CreateLookupCustomerShowBlock(this.parentForm, this.lookupCustFormNavAction);
                    if (!lookupCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Lookup customer block");
                    }
                    break;
                case LookupTicketFlowState.LookupCustomerResults:
                    ShowForm lookupCustResBlk = CommonAppBlocks.Instance.CreateLookupCustomerResultsBlock(this.parentForm, this.lookupCustResultsFormNavAction);
                    if (!lookupCustResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomerResults block");
                    }
                    break;

                case LookupTicketFlowState.AddCustomer:
                    ShowForm managePawnBlock = CommonAppBlocks.Instance.CreateManagePawnApplicationBlock(this.parentForm, this.managePawnAppFormNavAction);
                    if (!managePawnBlock.execute())
                    {
                        throw new ApplicationException("Cannot execute Add Customer- manage pawn block");
                    }
                    break;
                case LookupTicketFlowState.ManagePawnApplication:
                    ShowForm managePawnAppBlock = CommonAppBlocks.Instance.CreateManagePawnApplicationBlock(this.parentForm, this.managePawnAppFormNavAction);
                    if (!managePawnAppBlock.execute())
                    {
                        throw new ApplicationException("Cannot execute Manage Pawn Applicaction block");
                    }
                    break;
                case LookupTicketFlowState.UpdateAddress:
                    ShowForm updateAddrBlk = CommonAppBlocks.Instance.UpdateAddressShowFormBlock(this.parentForm, this.updateAddressFormNavAction);
                    if (!updateAddrBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Update Address Form block");
                    }

                    break;
                case LookupTicketFlowState.UpdatePhysicalDescription:
                    ShowForm updatePhysDescBlk = CommonAppBlocks.Instance.UpdatePhysDescShowFormBlock(this.parentForm, this.updatePhysicalDescFormNavAction);
                    if (!updatePhysDescBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Update Physical Description block");
                    }

                    break;
                case LookupTicketFlowState.ExistingCustomer:
                    ShowForm existCustBlk = CommonAppBlocks.Instance.CreateExistingCustomerBlock(this.parentForm, this.existCustomerFormNavAction);
                    if (!existCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute ExistingCustomer block");
                    }
                    break;
                case LookupTicketFlowState.ViewPawnCustomerProductDetails:
                    //If form already there in session then show that else open a new one
                        Controller_ProductServices productServFrm = new Controller_ProductServices();
                        Form currentForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(productServFrm);
                        if (currentForm.GetType() == typeof(Controller_ProductServices))
                        {
                            currentForm.Show();
                            currentForm.Activate();
                            CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, currentForm,FlowTabController.State.ProductsAndServices);
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
                case LookupTicketFlowState.ViewPawnCustomerStats:

                    ShowForm pawnCustStatsBlk = CommonAppBlocks.Instance.CreateStatsBlock(this.parentForm, this.productStatsFormNavAction);
                    if (!pawnCustStatsBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute View Pawn Customer Stats block");
                    }
                    CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, pawnCustStatsBlk.ClassForm, FlowTabController.State.Stats);
                    break;
                case LookupTicketFlowState.ItemHistory:
                    ShowForm itemHistBlk = CommonAppBlocks.Instance.CreateItemHistoryShowBlock(this.parentForm, this.itemHistoryFormNavAction);
                    if (!itemHistBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Item History block");
                    }
                    CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, itemHistBlk.ClassForm, FlowTabController.State.ItemHistory);
                    if (GlobalDataAccessor.Instance.DesktopSession.ShowOnlyHistoryTabs)
                    {
                        CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Customer, false);
                        CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductsAndServices,
                                                                   false);
                        CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Stats, false);
                        CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ItemHistory, true);
                        CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductHistory, true);

                    }
                    break;
                case LookupTicketFlowState.ProductHistory:
                    //Get the loan keys for the selected customer
                    //Before calling view pawn cust prod details
                    if (!new LoadCustomerLoanKeys().execute())
                    {
                        throw new ApplicationException("Cannot get Loan keys for selected customer");
                    }

                    ShowForm prodHistBlk = CommonAppBlocks.Instance.CreateProductHistoryShowBlock(this.parentForm, this.productHistoryFormNavAction);
                    if (!prodHistBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Product History block");
                    }
                    CommonAppBlocks.Instance.ShowFlowTabController(this.parentForm, prodHistBlk.ClassForm, FlowTabController.State.ProductHistory);
                    if (GlobalDataAccessor.Instance.DesktopSession.ShowOnlyHistoryTabs)
                    {
                        CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Customer, false);
                        CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductsAndServices,
                                                                   false);
                        CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.Stats, false);
                        CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ItemHistory, true);
                        CommonAppBlocks.Instance.SetFlowTabEnabled(FlowTabController.State.ProductHistory, true);

                    }
                    break;
                case LookupTicketFlowState.TenderIn:
                    ShowForm tenderInBlk = CommonAppBlocks.Instance.TenderInShowBlock(this.parentForm, this.TenderInFormAction);
                    if (!tenderInBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Tender In block");
                    }


                    break;

                case LookupTicketFlowState.ViewReceipt:
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
                case LookupTicketFlowState.InvokeMMPIChildFlow:
                    //Initiate the child workflow
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(MMPIFUNCTIONALITYNAME,
                        this.parentForm, this.endStateNotifier, this);
                    break;
                case LookupTicketFlowState.Cancel:
                    CommonAppBlocks.Instance.HideFlowTabController();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;

            }

            return (true);
        }

        private static LookupTicketFlowState FindStateByTabClicked()
        {
            if (GlobalDataAccessor.Instance.DesktopSession.TabStateClicked == FlowTabController.State.ProductHistory)
            {
                return LookupTicketFlowState.ProductHistory;
            }
            else if (GlobalDataAccessor.Instance.DesktopSession.TabStateClicked == FlowTabController.State.ItemHistory)
                return LookupTicketFlowState.ItemHistory;
            else
                return LookupTicketFlowState.LookupTicket;
        }

        /// <summary>
        /// NavBox OnAction Handler for Lookup Customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void lookupTicketFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Lookup ticket form navigation action handler received invalid data");
            }

            NavBox lookuptktNavBox = (NavBox)sender;
            LookupTicket lookupTktForm = (LookupTicket)data;
            NavBox.NavAction lookupAction = lookuptktNavBox.Action;
            switch (lookupAction)
            {
                case NavBox.NavAction.HIDEANDSHOW:
                    lookupTktForm.Hide();
                    this.nextState = LookupTicketFlowState.LookupTicketResults;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupTicketFlowState.Cancel;
                    break;
                //BZ # 490
                case NavBox.NavAction.BACKANDSUBMIT:
                    lookupTktForm.Hide();
                    this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                    string custDet = lookuptktNavBox.CustomDetail;
                    if (custDet.Equals("Stats"))
                    {
                        this.nextState = LookupTicketFlowState.ViewPawnCustomerStats;
                    }
                    else if (custDet.Equals("ProductsAndServices"))
                        this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                    else if (custDet.Equals("ItemHistory"))
                        this.nextState = LookupTicketFlowState.ItemHistory;
                    else if (custDet.Equals("ProductHistory") || custDet.Equals("ViewPawnCustomerProductHistory"))
                        this.nextState = LookupTicketFlowState.ProductHistory;
                    break;
                //BZ # 490 end
                default:
                    throw new ApplicationException(string.Format("{0} is not a valid state for LookupTicket", lookupAction));
            }

            this.executeNextState();
        }


        /// <summary>
        /// NavBox OnAction Handler for View customer information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
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
                    this.nextState = LookupTicketFlowState.Cancel;
                    break;
                case NavBox.NavAction.BACK:
                    string custDetail=viewCustNavBox.CustomDetail;
                    if (viewCustNavBox.IsCustom && custDetail.Equals("Menu"))
                    {
                        this.nextState = LookupTicketFlowState.Cancel;
                    }
                    else
                    {
                        Form f = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                        f.Visible = true;
                        CommonAppBlocks.Instance.HideFlowTabController();
                        this.nextState = LookupTicketFlowState.LookupTicketResults;
                    }
                    break;
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    if (viewCustNavBox.IsCustom)
                    {
                        string custDet = viewCustNavBox.CustomDetail;
                        if (custDet.Equals("Stats"))
                        {
                            this.nextState = LookupTicketFlowState.ViewPawnCustomerStats;
                        }
                        else if (custDet.Equals("ProductsAndServices"))
                            this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                        else if (custDet.Equals("ItemHistory"))
                            this.nextState = LookupTicketFlowState.ItemHistory;
                        else if (custDet.Equals("ProductHistory"))
                            this.nextState = LookupTicketFlowState.ProductHistory;

                    }
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupTicket");
            }

            this.executeNextState();
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
                            this.nextState = LookupTicketFlowState.AddCustomer;
                        }
                        else
                            this.nextState = LookupTicketFlowState.LookupCustomerResults;
                    }
                    else
                    {
                        this.nextState = LookupTicketFlowState.LookupCustomerResults;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupTicketFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for LookupCustomer");
            }

            this.executeNextState();
        }


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
                            this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                        }
                        else if (custDet.Equals("Customer"))
                            this.nextState = LookupTicketFlowState.ViewCustomerInformation;
                        else if (custDet.Equals("ItemHistory"))
                            this.nextState = LookupTicketFlowState.ItemHistory;
                        else if (custDet.Equals("ProductHistory"))
                            this.nextState = LookupTicketFlowState.ProductHistory;

                    }
                    break;
                case NavBox.NavAction.BACK:
                    string custDetail = prodstatsNavBox.CustomDetail;
                    if (prodstatsNavBox.IsCustom  && custDetail.Equals("Menu"))
                    {
                        this.nextState = LookupTicketFlowState.Cancel;
                    }
                    else
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                        CommonAppBlocks.Instance.HideFlowTabController();
                        this.nextState = LookupTicketFlowState.LookupTicket;
                    }
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Stats");
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
                        if (custDet.Equals("CreateCustomer"))
                        {
                            this.nextState = LookupTicketFlowState.AddCustomer;
                        }
                        else if (custDet.Equals("ViewCustomerInformation"))
                        {
                            this.nextState = LookupTicketFlowState.ViewCustomerInformation;
                        }
                        else if (custDet.Equals("ViewCustomerInformationReadOnly"))
                        {
                            this.nextState = LookupTicketFlowState.ViewReadOnlyCustomerInformation;
                        }

                        else if (custDet.Equals("Complete"))
                        {
                            //this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                            this.nextState = LookupTicketFlowState.ManagePawnApplication;
                        }
                    }
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupTicketFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupTicketFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupCustomerResults");
            }
            this.executeNextState();
        }


        /// <summary>
        /// Action class for LookupTicketResults
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void lookupTktResultsFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Lookup ticket form navigation action handler received invalid data");
            }

            NavBox lookupTktResNavBox = (NavBox)sender;
            LookupTicketResults lookupTktResForm = (LookupTicketResults)data;
            NavBox.NavAction action = lookupTktResNavBox.Action;


            switch (action)
            {
                case NavBox.NavAction.HIDEANDSHOW:
                    lookupTktResForm.Hide();
                    if (lookupTktResNavBox.IsCustom)
                    {
                        string custDet = lookupTktResNavBox.CustomDetail;
                        if (custDet.Equals("ViewCustomerReadOnlyInformation", StringComparison.OrdinalIgnoreCase))
                            this.nextState = LookupTicketFlowState.ViewReadOnlyCustomerInformation;
                        else
                        {
                            this.nextState = LookupTicketFlowState.ViewCustomerInformation;
                        }
                    }
                    break;
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();

                    if (lookupTktResNavBox.IsCustom)
                    {
                        string custDet = lookupTktResNavBox.CustomDetail;
                        if (custDet.Equals("ViewPawnCustomerProductDetails", StringComparison.OrdinalIgnoreCase))
                        {
                            this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                        }
                        else if (custDet.Equals("ViewPawnCustomerProductHistory", StringComparison.OrdinalIgnoreCase))
                        {
                            this.nextState = LookupTicketFlowState.ProductHistory;
                        }
                        else
                            this.nextState = LookupTicketFlowState.LookupCustomer;
                    }
                    else
                        this.nextState = LookupTicketFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupTicketFlowState.LookupTicket;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupTicketResults");
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
                    this.nextState = LookupTicketFlowState.ViewCustomerInformation;
                    break;
                case NavBox.NavAction.HIDEANDSHOW:
                    createCustForm.Hide();
                    this.nextState = LookupTicketFlowState.UpdateAddress;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupTicketFlowState.Cancel;
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
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupTicketFlowState.AddCustomer;
                    break;
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LookupTicketFlowState.ViewCustomerInformation;
                    break;
                case NavBox.NavAction.HIDEANDSHOW:
                    addrForm.Hide();
                    this.nextState = LookupTicketFlowState.UpdatePhysicalDescription;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupTicketFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Update Address");
            }

            this.executeNextState();
        }


        private void productServicesFormNavAction(object sender, object data)
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
                        else if (custDet.Equals("Reload", StringComparison.OrdinalIgnoreCase))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                            CommonAppBlocks.Instance.HideFlowTabController();
                            this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                        }
                        else if (custDet.Equals("ManagePawnApplication", StringComparison.OrdinalIgnoreCase))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.VIEWPAWNCUSTPRODDETAILS;
                            prodSrvForm.Hide();
                            CommonAppBlocks.Instance.HideFlowTabController();
                            this.nextState = LookupTicketFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ProcessTender", StringComparison.OrdinalIgnoreCase))
                        {
                            CommonAppBlocks.Instance.HideFlowTabController();
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                            this.nextState = LookupTicketFlowState.TenderIn;
                        }

                        else
                            this.nextState = LookupTicketFlowState.ViewReceipt;
                    }
                    else
                        this.nextState = LookupTicketFlowState.Cancel;
                    break;
                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    if (prodsrvNavBox.IsCustom)
                    {
                        string custDet = prodsrvNavBox.CustomDetail;
                        if (custDet.Equals("ProductStats"))
                        {
                            this.nextState = LookupTicketFlowState.ViewPawnCustomerStats;
                        }
                        else if (custDet.Equals("Customer"))
                            this.nextState = LookupTicketFlowState.ViewCustomerInformation;
                        else if (custDet.Equals("ItemHistory"))
                            this.nextState = LookupTicketFlowState.ItemHistory;
                        else if (custDet.Equals("ProductHistory"))
                            this.nextState = LookupTicketFlowState.ProductHistory;

                    }
                    break;
                case NavBox.NavAction.BACK:
                    if (prodsrvNavBox.IsCustom && prodsrvNavBox.CustomDetail.Equals("Menu", StringComparison.OrdinalIgnoreCase))
                    {
                        this.nextState = LookupTicketFlowState.Cancel;
                    }
                    else
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                        CommonAppBlocks.Instance.HideFlowTabController();
                        this.nextState = LookupTicketFlowState.LookupTicket;
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
                            this.nextState = LookupTicketFlowState.Cancel;
                    }
                    else
                    {

                        this.nextState = LookupTicketFlowState.Cancel;
                    }
                    break;
                case NavBox.NavAction.SUBMIT:
                    //this.nextState = LookupTicketFlowState.ViewReceipt;  
                    //The products and services page needs to be reloaded
                    //so so another back to remove it from session
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                    break;
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
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupTicketFlowState.UpdateAddress;
                    break;
                case NavBox.NavAction.BACKANDSUBMIT:
                    //clear all forms - even the hidden ones
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                    this.nextState = LookupTicketFlowState.ViewCustomerInformation;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupTicketFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Update Physical Description");
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
                        this.nextState = LookupTicketFlowState.ExistingCustomer;
                    }
                    else if (custDet.Equals("ViewPawnCustomerProductDetails"))
                    {
                        this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                    }
                    else
                    {
                        this.nextState = LookupTicketFlowState.Cancel;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupTicketFlowState.Cancel;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = LookupTicketFlowState.LookupCustomerResults;
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
                            nextState = LookupTicketFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ViewPawnCustomerInformation"))
                        {
                            nextState = LookupTicketFlowState.ViewCustomerInformation;
                        }

                    }
                    break;
                case NavBox.NavAction.BACK:
                    nextState = LookupTicketFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    nextState = LookupTicketFlowState.Cancel;
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
                            this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                        }
                        else if (custDet.Equals("Customer"))
                            this.nextState = LookupTicketFlowState.ViewCustomerInformation;
                        else if (custDet.Equals("ProductStats"))
                            this.nextState = LookupTicketFlowState.ViewPawnCustomerStats;
                        else if (custDet.Equals("ProductHistory"))
                            this.nextState = LookupTicketFlowState.ProductHistory;
                        else if (custDet.Equals("AddNewLoan", StringComparison.OrdinalIgnoreCase))
                        {
                            this.nextState = LookupTicketFlowState.InvokeMMPIChildFlow;
                        }

                    }
                    break;
                case NavBox.NavAction.BACK:
                    string custDetail = itemHistNavBox.CustomDetail;
                    if (custDetail.Equals("Exit"))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                        this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                    }
                    else if (custDetail.Equals("Menu"))
                    {
                        this.nextState = LookupTicketFlowState.Cancel;
                    }
                    else
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                        this.nextState = LookupTicketFlowState.Cancel;
                    }
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Item History");
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
                        this.nextState = LookupTicketFlowState.Cancel;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = LookupTicketFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Tender In");
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
                    this.nextState = LookupTicketFlowState.ViewReceipt;
                    break;

                case NavBox.NavAction.BACKANDSUBMIT:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    if (prodHistNavBox.IsCustom)
                    {
                        string custDet = prodHistNavBox.CustomDetail;
                        if (custDet.Equals("ProductsAndServices"))
                        {
                            this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                        }
                        else if (custDet.Equals("Customer"))
                            this.nextState = LookupTicketFlowState.ViewCustomerInformation;
                        else if (custDet.Equals("ProductStats"))
                            this.nextState = LookupTicketFlowState.ViewPawnCustomerStats;
                        else if (custDet.Equals("ItemHistory"))
                            this.nextState = LookupTicketFlowState.ItemHistory;
                        else if (custDet.Equals("AddNewLoan", StringComparison.OrdinalIgnoreCase))
                        {
                           this.nextState = LookupTicketFlowState.InvokeMMPIChildFlow;
                        }

                    }
                    break;
                case NavBox.NavAction.BACK:
                    string custDetail = prodHistNavBox.CustomDetail;
                   if (custDetail.Equals("Exit"))
                   {
                       GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                       this.nextState = LookupTicketFlowState.ViewPawnCustomerProductDetails;
                   }
                   else if (custDetail.Equals("Menu"))
                   {
                       this.nextState = LookupTicketFlowState.Cancel;
                   }
                   else
                   {

                       GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                       CommonAppBlocks.Instance.HideFlowTabController();
                       this.nextState = LookupTicketFlowState.LookupTicket;
                   }
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for Product History");
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

        public LookupTicketFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = LookupTicketFlowState.LookupTicket;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }
    }
}
