using System;
using System.Windows.Forms;
using CashlinxDesktop.DesktopForms.Pawn.Customer;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Objects.Purchase;
using Pawn.Forms.Pawn.Customer;
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class VendorPurchaseFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "VendorPurchaseFlowExecutor";
        public static readonly string MMPIFUNCTIONALITYNAME = "mmpi";

        public enum VendorPurchaseFlowState
        {
            LookupVendor,
            AddVendor,
            LookupVendorResults,
            InvokeMMPIChildFlow,
            Exit,
            Cancel,
            Error
        }

        private VendorPurchaseFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;


        /// <summary>
        /// Main execution function for LookupVendorFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null &&
                    !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name))
                    inputData = VendorPurchaseFlowState.AddVendor;
                else
                inputData = VendorPurchaseFlowState.LookupVendor;
            }
            VendorPurchaseFlowState inputState = (VendorPurchaseFlowState)inputData;
            var dSession = GlobalDataAccessor.Instance.DesktopSession;

            switch (inputState)
            {
                case VendorPurchaseFlowState.LookupVendor:

                    var lookupVendBlk = CommonAppBlocks.Instance.CreateLookupVendorShowBlock(this.parentForm, this.lookupVendFormNavAction);
                    if (!lookupVendBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupVendor block");
                    }

                    break;

                case VendorPurchaseFlowState.LookupVendorResults:
                    var lookupVendResBlk = CommonAppBlocks.Instance.CreateLookupVendorResultsBlock(this.parentForm, this.lookupVendResultsFormNavAction);
                    if (!lookupVendResBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute LookupVendorResults block");
                    }
                    break;

                case VendorPurchaseFlowState.AddVendor:

                    var createVendBlk = CommonAppBlocks.Instance.CreateCreateVendorBlock(this.parentForm, this.createVendFormNavAction);
                    if (!createVendBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Create Vendor block");
                    }
                    break;

                case VendorPurchaseFlowState.InvokeMMPIChildFlow:
                    //Initiate the child workflow
                    GlobalDataAccessor.Instance.DesktopSession.VenderFFLRequired = false;
                    if (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase == null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.VendorValidated = false;
                        GlobalDataAccessor.Instance.DesktopSession.Purchases.Add(new PurchaseVO());
                    }
                    else
                        GlobalDataAccessor.Instance.DesktopSession.VendorValidated = true;

                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(MMPIFUNCTIONALITYNAME, this.parentForm,
                                                                                 this.endStateNotifier, this); 
//                    dSession.Purchases.Add(new PurchaseVO());
//                    dSession.AppController.invokeWorkflow(MMPIFUNCTIONALITYNAME,
//                        this.parentForm, this.endStateNotifier, this);
                    break;

                case VendorPurchaseFlowState.Cancel:


                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;



            }

            return (true);
        }









        /// <summary>
        /// NavBox OnAction Handler for Lookup Vendor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void lookupVendFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Lookup vendor form navigation action handler received invalid data");
            }
            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            NavBox lookupVendNavBox = (NavBox)sender;
            LookupVendor lookupVendForm = (LookupVendor)data;
            NavBox.NavAction lookupAction = lookupVendNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                dSession.HistorySession.Back();
                lookupAction = NavBox.NavAction.SUBMIT;
            }
            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:
                    //Submit will be called both when Add Vendor button is pressed 
                    //and when Find vendors button is pressed
                    if (lookupVendNavBox.IsCustom)
                    {
                        string custDet = lookupVendNavBox.CustomDetail;
                        //Look for add customer
                        if (custDet.Equals("AddVendor") || custDet.Equals("CreateVendor"))
                        {
                            this.nextState = VendorPurchaseFlowState.AddVendor;
                        }
                        else
                            this.nextState = VendorPurchaseFlowState.LookupVendorResults;
                    }
                    else
                    {
                        this.nextState = VendorPurchaseFlowState.LookupVendorResults;
                    }
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = VendorPurchaseFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException(string.Format("{0} is not a valid state for LookupVendor", lookupAction));
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

            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            NavBox lookupVendResNavBox = (NavBox)sender;
            LookupVendorResults lookupCustResForm = (LookupVendorResults)data;
            NavBox.NavAction action = lookupVendResNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                dSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
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
                            this.nextState = VendorPurchaseFlowState.AddVendor;
                        }


                    }

                    break;
                case NavBox.NavAction.BACK:
                    dSession.HistorySession.Back();
                    this.nextState = VendorPurchaseFlowState.LookupVendor;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = VendorPurchaseFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException(string.Format("{0} is not a valid state for LookupVendorResults", action));
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

            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            NavBox createVendNavBox = (NavBox)sender;
            CreateVendor createVendForm = (CreateVendor)data;
            NavBox.NavAction lookupAction = createVendNavBox.Action;
            if (lookupAction == NavBox.NavAction.BACKANDSUBMIT)
            {
                dSession.HistorySession.Back();
                lookupAction = NavBox.NavAction.SUBMIT;
            }
            switch (lookupAction)
            {
                case NavBox.NavAction.SUBMIT:
                    if (createVendNavBox.IsCustom)
                    {
                        var custDet = createVendNavBox.CustomDetail;
                        //Look for add customer
                        if (custDet.Equals("AddVendorComplete"))
                        {
                            this.nextState = VendorPurchaseFlowState.InvokeMMPIChildFlow;
                        }
                    }
                    break;
 
                case NavBox.NavAction.CANCEL:
                    this.nextState = VendorPurchaseFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException(string.Format("{0} is not a valid state for LookupVendor", lookupAction));
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

        public VendorPurchaseFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = VendorPurchaseFlowState.LookupVendor;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }


    }
}

