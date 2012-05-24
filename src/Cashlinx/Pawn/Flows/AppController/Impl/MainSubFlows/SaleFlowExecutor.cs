using System;
using System.Windows.Forms;
using CashlinxDesktop.DesktopForms.Pawn.Customer;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Pawn.Products.DescribeMerchandise;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Customer;
using Pawn.Forms.Pawn.Tender;
using Pawn.Forms.Retail;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class SaleFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "SaleFlowExecutor";

        public enum SaleFlowState
        {
            LookupCustomer,
            AddCustomer,
            AddVendor,
            ExistingCustomer,
            LookupCustomerResults,
            LookupVendorResults,
            ManagePawnApplication,
            ViewCustomerInformation,
            ViewReadOnlyCustomerInformation,
            ItemSearch,
            DescribeMerchandise,
            DescribeItem,
            ExitFlow,
            CancelFlow,
            ProcessTender,
            TenderIn,
            Error
        }

        private SaleFlowState nextState;
        private SaleFlowState suspendedState = SaleFlowState.Error;
        private Form parentForm;
        private FxnBlock endStateNotifier;
        private bool tenderInComplete;

        /// <summary>
        /// Main execution function for SaleFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
            {
                inputData = suspendedState;
            }
            SaleFlowState inputState = (SaleFlowState)inputData;

            switch (inputState)
            {
                case SaleFlowState.ItemSearch:
                    if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail == null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.Sales.Add(new SaleVO());
                        GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = -1;
                    }
                    //ItemSearch itemSearchFrm = new ItemSearch();
                    if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.HasFormName("ItemSearch"))
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.VisibleForm("ItemSearch");
                    else
                    {
                        ShowForm itemSearchBlk = CommonAppBlocks.Instance.CreateItemSearchShowBlock(this.parentForm, this.itemSearchFormNavAction);
                        if (!itemSearchBlk.execute())
                        {
                            throw new ApplicationException("Cannot execute Item Search block");
                        }
                    }

                    break;
                case SaleFlowState.DescribeMerchandise:
                    ShowForm describeMerchBlk = CommonAppBlocks.Instance.DescribeMerchandiseBlock(this.parentForm, this.describeMerchFormAction);
                    if (!describeMerchBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Merchandise block");
                    }

                    break;
                case SaleFlowState.DescribeItem:
                    ShowForm describeItemBlk = CommonAppBlocks.Instance.DescribeItemBlock(this.parentForm, this.describeItemFormAction);
                    if (!describeItemBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Describe Item Block");
                    }
                    break;

                case SaleFlowState.LookupCustomer:

                    ShowForm lookupCustBlk = CommonAppBlocks.Instance.CreateLookupCustomerShowBlock(this.parentForm, this.lookupCustFormNavAction);
                    if (!lookupCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomer block");
                    }

                    break;
                case SaleFlowState.LookupCustomerResults:
                    ShowForm lookupCustResBlk = CommonAppBlocks.Instance.CreateLookupCustomerResultsBlock(this.parentForm, this.lookupCustResultsFormNavAction);
                    if (!lookupCustResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupCustomerResults block");
                    }
                    break;
                case SaleFlowState.LookupVendorResults:
                    ShowForm lookupVendResBlk = CommonAppBlocks.Instance.CreateLookupVendorResultsBlock(this.parentForm, this.lookupVendResultsFormNavAction);
                    if (!lookupVendResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupVendorResults block");
                    }
                    break;

                case SaleFlowState.AddCustomer:
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
                case SaleFlowState.AddVendor:

                    ShowForm createVendBlk = CommonAppBlocks.Instance.CreateCreateVendorBlock(this.parentForm, this.createVendFormNavAction);
                    if (!createVendBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Create Vendor block");
                    }
                    break;

                case SaleFlowState.ExistingCustomer:
                    ShowForm existCustBlk = CommonAppBlocks.Instance.CreateExistingCustomerBlock(this.parentForm, this.existCustomerFormNavAction);
                    if (!existCustBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute ExistingCustomer block");
                    }
                    break;
                case SaleFlowState.ManagePawnApplication:
                    ShowForm managePawnAppBlk = CommonAppBlocks.Instance.CreateManagePawnApplicationBlock(this.parentForm, this.managePawnAppFormNavAction);
                    if (!managePawnAppBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute ManagePawnApplication block");
                    }
                    break;
                case SaleFlowState.ViewCustomerInformation:
                    ShowForm viewCustInfoBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustInfoFormNavAction);
                    if (!viewCustInfoBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute View Customer Information block");
                    }

                    break;
                case SaleFlowState.ViewReadOnlyCustomerInformation:
                    ShowForm viewCustInfoReadOnlyBlk = CommonAppBlocks.Instance.ViewCustomerInfoShowBlock(this.parentForm, this.viewCustInfoFormNavAction);
                    if (!viewCustInfoReadOnlyBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute View customer information Read Only block");
                    }
                    ((ViewCustomerInformation)viewCustInfoReadOnlyBlk.ClassForm).ShowReadOnly = true;
                    break;
                case SaleFlowState.TenderIn:
                    if (this.tenderInComplete)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.LoanStatus == ProductStatus.LAY)
                            GlobalDataAccessor.Instance.DesktopSession.showProcessTender(ProcessTenderProcedures.ProcessTenderMode.LAYAWAY);
                        else
                        {
                            ProcessTenderController pCntrl = ProcessTenderController.Instance;
                            //Perform the process tender process
                            bool success = pCntrl.ExecuteProcessTender(ProcessTenderProcedures.ProcessTenderMode.RETAILSALE);
                            if (success)
                            {
                                ReceiptConfirmation rcptConf = new ReceiptConfirmation();
                                rcptConf.ShowDialog();
                            }
                            else
                                goto case SaleFlowState.CancelFlow;
                                
                        }

                        goto case SaleFlowState.CancelFlow;
                    }
                    else
                    {
                        if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup("TenderIn"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.VisibleForm("TenderIn");
                        }
                        else
                        {
                            ShowForm tenderInBlk = CommonAppBlocks.Instance.TenderInShowBlock(this.parentForm, this.TenderInFormAction);
                            if (!tenderInBlk.execute())
                            {
                                throw new ApplicationException("Cannot execute Tender In block");
                            }
                        }
                    }

                    break;

                case SaleFlowState.CancelFlow:
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
                    GlobalDataAccessor.Instance.DesktopSession.ClearLoggedInUser();
                    GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();
                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;
                case SaleFlowState.ExitFlow:
                    break;
                default:
                    throw new ApplicationException("Invalid sale flow state");
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

            var lookupCustNavBox = (NavBox)sender;
            var lookupCustForm = (LookupCustomer)data;
            var lookupAction = lookupCustNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                lookupAction = NavBox.NavAction.SUBMIT;
            }
            var itemSearchFrm = new ItemSearch();
            Form currForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(itemSearchFrm);
            if (currForm.GetType() == typeof(ItemSearch))
            {
                currForm.Hide();
            }
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup("TenderIn"))
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.HideForm("TenderIn");
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
                            this.nextState = SaleFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ExistingCustomer"))
                        {
                            this.nextState = SaleFlowState.ExistingCustomer;
                        }
                        else if (custDet.Equals("LookupCustomerResults"))
                        {
                            this.nextState = SaleFlowState.LookupCustomerResults;
                        }
                        else if (custDet.Equals("LookupVendorResults"))
                        {
                            this.nextState = SaleFlowState.LookupVendorResults;
                        }
                        else if (custDet.Equals("ManagePawnApplication"))
                        {
                            this.nextState = SaleFlowState.ManagePawnApplication;
                        }
                    }
                    else
                    {
                        //Default happy path next state
                        this.parentForm = lookupCustForm;
                        this.nextState = SaleFlowState.LookupCustomerResults;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = SaleFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for LookupCustomer");
            }

            this.executeNextState();
        }

        /// <summary>
        /// Action class for LookupVendorResults
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void lookupVendResultsFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Lookup Vendor form navigation action handler received invalid data");
            }

            NavBox lookupVendResNavBox = (NavBox)sender;
            NavBox.NavAction action = lookupVendResNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            ItemSearch itemSearchFrm = new ItemSearch();
            Form currForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(itemSearchFrm);
            if (currForm.GetType() == typeof(ItemSearch))
            {
                currForm.Hide();
            }
            TenderIn tenderInFrm = new TenderIn();
            currForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(tenderInFrm);
            if (currForm.GetType() == typeof(TenderIn))
            {
                currForm.Hide();
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (lookupVendResNavBox.IsCustom)
                    {
                        string custDet = lookupVendResNavBox.CustomDetail;
                        //Look for add customer
                        if (custDet.Equals("CreateVendor"))
                        {
                            this.nextState = SaleFlowState.AddVendor;
                        }
                    }

                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = SaleFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = SaleFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for LookupVendorResults");
            }
            this.executeNextState();
        }

        /// <summary>
        /// NavBox OnAction Handler for Create Customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void createVendFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Create Vendor form navigation action handler received invalid data");
            }

            NavBox createVendNavBox = (NavBox)sender;
            NavBox.NavAction lookupAction = createVendNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                lookupAction = NavBox.NavAction.SUBMIT;
            }
            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:
                    if (createVendNavBox.IsCustom)
                    {
                        string custDet = createVendNavBox.CustomDetail;
                        //Look for add customer
                        if (custDet.Equals("AddVendorComplete"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                            this.nextState = SaleFlowState.TenderIn;
                        }
                    }
                    break;

                case NavBox.NavAction.CANCEL:
                    this.nextState = SaleFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + lookupAction.ToString() + " is not a valid state for LookupVendor");
            }

            this.executeNextState();
        }

        private void itemSearchFormNavAction(object sender, object data)
        {
            NavBox itemSearchNavBox = (NavBox)sender;
            ItemSearch itemSearchForm = (ItemSearch)data;
            NavBox.NavAction action = itemSearchNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (itemSearchNavBox.IsCustom)
                    {
                        string custDet = itemSearchNavBox.CustomDetail;
                        if (custDet.Equals("GetCategory") || custDet.Equals("TemporaryICN"))
                        {
                            itemSearchForm.Hide();
                            this.nextState = SaleFlowState.DescribeMerchandise;
                        }
                        else if (custDet.Equals("ViewItemDetails"))
                        {
                            //Show describe item form as show dialog
                            int iItemIdx = GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex;
                            int iCategoryMask = GlobalDataAccessor.Instance.DesktopSession.CategoryXML.GetCategoryMask(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems[iItemIdx].CategoryCode);
                            DescribedMerchandise dmPawnItem = new DescribedMerchandise(iCategoryMask);
                            Item pawnItem = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems[iItemIdx];
                            Item.PawnItemMerge(ref pawnItem, dmPawnItem.SelectedPawnItem, true);
                            ((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveRetail).Items.Insert(0, pawnItem);
                            // End GetCat5 populate
                            // Placeholder for ReadOnly DescribedItem.cs
                            DescribeItem myForm = new DescribeItem(GlobalDataAccessor.Instance.DesktopSession, CurrentContext.READ_ONLY, 0)
                            {
                                SelectedProKnowMatch = ((CustomerProductDataVO)GlobalDataAccessor.Instance.DesktopSession.ActiveRetail).Items[0].SelectedProKnowMatch
                            };
                            myForm.ShowDialog(itemSearchForm);
                        }
                        else if (custDet.Equals("LookupCustomer"))
                        {
                            itemSearchForm.Hide();
                            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null && !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber))
                                this.nextState = SaleFlowState.ManagePawnApplication;
                            else
                                this.nextState = SaleFlowState.LookupCustomer;
                        }
                        else if (custDet.Equals("Reload"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                            this.nextState = SaleFlowState.ItemSearch;
                        }
                        else if (custDet.Equals("ProcessTender"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                            this.nextState = SaleFlowState.TenderIn;
                        }
                    }
                    else
                    {
                        //Default happy path next state
                        this.parentForm = itemSearchForm;
                        this.nextState = SaleFlowState.ProcessTender;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = SaleFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for ItemSearch");
            }

            this.executeNextState();
        }

        private void describeMerchFormAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Describe Merchandise form navigation action handler received invalid data");
            }

            NavBox descMerchNavBox = (NavBox)sender;
            NavBox.NavAction action = descMerchNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (descMerchNavBox.IsCustom)
                    {
                        string custDet = descMerchNavBox.CustomDetail;
                        if (custDet.Equals("TemporaryICN"))
                        {
                            //ItemSearch itemSearchFrm = new ItemSearch();
                            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.HasFormName("ItemSearch"))
                            {
                                GlobalDataAccessor.Instance.DesktopSession.HistorySession.HideForm("ItemSearch");
                            }

                            this.nextState = SaleFlowState.DescribeItem;
                        }
                        else
                            this.nextState = SaleFlowState.ItemSearch;
                    }
                    else
                        this.nextState = SaleFlowState.ItemSearch;
                    break;
                case NavBox.NavAction.CANCEL:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = SaleFlowState.ItemSearch;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Describe Merchandise");
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
            TenderIn tenderForm = (TenderIn)data;
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
                            if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.LoanStatus == ProductStatus.LAY)
                                GlobalDataAccessor.Instance.DesktopSession.showProcessTender(ProcessTenderProcedures.ProcessTenderMode.LAYAWAY);
                            else
                            {
                                var pCntrl = ProcessTenderController.Instance;
                                //Perform the process tender process
                                bool success = pCntrl.ExecuteProcessTender(ProcessTenderProcedures.ProcessTenderMode.RETAILSALE);
                                if (success)
                                {
                                    ReceiptConfirmation rcptConf = new ReceiptConfirmation();
                                    rcptConf.ShowDialog();
                                }
                                else
                                    this.nextState = SaleFlowState.CancelFlow;
                            }
                            this.nextState = SaleFlowState.CancelFlow;
                        }
                        else if (custDet.Equals("LookupCustomer"))
                        {
                            this.tenderInComplete = true;
                            this.nextState = SaleFlowState.LookupCustomer;
                        }
                        else
                            this.nextState = SaleFlowState.CancelFlow;
                    }
                    else
                    {
                        GlobalDataAccessor.Instance.DesktopSession.CompleteSale = false;
                        GlobalDataAccessor.Instance.DesktopSession.CompleteLayaway = false;
                        this.nextState = SaleFlowState.ItemSearch;
                    }
                    break;
                case NavBox.NavAction.HIDEANDSHOW:
                    tenderForm.Hide();
                    if (tenderNavBox.IsCustom)
                    {
                        string custDet = tenderNavBox.CustomDetail;
                        this.nextState = custDet.Equals("LookupCustomer") ? SaleFlowState.LookupCustomer : SaleFlowState.CancelFlow;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = SaleFlowState.CancelFlow;
                    break;
                default:
                    throw new ApplicationException("" + action + " is not a valid state for Tender In");
            }
            this.executeNextState();
        }

        private void describeItemFormAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Describe Item form navigation action handler received invalid data");
            }

            NavBox descItemNavBox = (NavBox)sender;
            NavBox.NavAction action = descItemNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (GlobalDataAccessor.Instance.DesktopSession.GenerateTemporaryICN)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                        GlobalDataAccessor.Instance.DesktopSession.GenerateTemporaryICN = false;
                    }
                    this.nextState = SaleFlowState.ItemSearch;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = SaleFlowState.CancelFlow;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    if (GlobalDataAccessor.Instance.DesktopSession.GenerateTemporaryICN)
                    {
                        //ItemSearch itemSearchFrm = new ItemSearch();
                        if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup("ItemSearch"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.VisibleForm("ItemSearch");
                        }
                    }
                    this.nextState = GlobalDataAccessor.Instance.DesktopSession.GenerateTemporaryICN ? SaleFlowState.DescribeMerchandise : SaleFlowState.ItemSearch;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Describe Item");
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
                            this.nextState = SaleFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ViewCustomerInformation"))
                        {
                            this.nextState = SaleFlowState.ViewCustomerInformation;
                        }
                        else if (custDet.Equals("ViewCustomerInformationReadOnly"))
                        {
                            this.nextState = SaleFlowState.ViewReadOnlyCustomerInformation;
                        }
                    }
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = SaleFlowState.LookupCustomer;
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
                    this.nextState = SaleFlowState.CancelFlow;
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
                    if (managePawnAppNavBox.IsCustom)
                    {
                        string custDet = managePawnAppNavBox.CustomDetail;
                        if (custDet.Equals("ExistingCustomer"))
                        {
                            this.nextState = SaleFlowState.ExistingCustomer;
                        }
                    }
                    else
                    {
                        if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup("TenderIn"))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                            this.nextState = SaleFlowState.TenderIn;
                        }
                        else
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                            this.nextState = !this.tenderInComplete ? SaleFlowState.ItemSearch : SaleFlowState.TenderIn;
                        }
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = SaleFlowState.CancelFlow;
                    break;
                case NavBox.NavAction.BACK:
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                    this.nextState = SaleFlowState.LookupCustomerResults;
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
                    this.nextState = SaleFlowState.ManagePawnApplication;
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
                            this.nextState = SaleFlowState.ManagePawnApplication;
                        }
                        else if (custDet.Equals("ViewPawnCustomerInformation"))
                        {
                            this.nextState = SaleFlowState.ViewCustomerInformation;
                        }
                    }
                    break;
                case NavBox.NavAction.BACK:
                    this.nextState = SaleFlowState.LookupCustomer;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = SaleFlowState.CancelFlow;
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

        public SaleFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
        : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = SaleFlowState.ItemSearch;
            this.tenderInComplete = false;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }

        public SaleFlowState NextState
        {
            get
            {
                return (this.nextState);
            }
        }
    }
}
