using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
//using Common.Libraries.Forms;
using Common.Libraries.Forms.Pawn.Products.DescribeMerchandise;
using Common.Libraries.Forms.Retail;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Shared;
using Support.Flows.AppController.Impl.Common;
using Support.Forms.Customer.ItemHistory;
using Support.Forms.Customer.Products.ProductHistory;
using Support.Forms.ShopAdmin.EditGunBook;
using Support.Libraries.Forms;
using Support.Forms.Customer.Products;
using Support.Forms.Customer;
using Support.Forms.Pawn.Customer;
using Support.Forms.Customer.ProductTabs;
using Support.Logic;

//Madhu Feb 17th
/*using Pawn.Flows.AppController.Impl.Common;
using Pawn.Forms.GunUtilities.EditGunBook;
using Pawn.Forms.Layaway;
using Pawn.Forms.Pawn.Customer;
using Pawn.Forms.Pawn.Customer.Holds;
using Pawn.Forms.Pawn.Customer.ItemRelease;
using Pawn.Forms.Pawn.Customer.Stats;
using Pawn.Forms.Pawn.ItemHistory;
using Pawn.Forms.Pawn.Products.ManageMultiplePawnItems;
using Pawn.Forms.Pawn.Products.ProductDetails;
using Pawn.Forms.Pawn.Products.ProductHistory;
using Pawn.Forms.Pawn.Services.MerchandiseTransfer;
using Pawn.Forms.Pawn.Services.PFI;
using Pawn.Forms.Pawn.Services.Receipt;
using Pawn.Forms.Pawn.Services.Return;
using Pawn.Forms.Pawn.Services.Ticket;
using Pawn.Forms.Pawn.ShopAdministration;
using Pawn.Forms.Pawn.ShopAdministration.Assignments;
using Pawn.Forms.Pawn.Tender;
using Pawn.Forms.Retail;*/

namespace Support.Flows.AppController.Impl
{
    public sealed class CommonAppBlocks : CommonAppBlocksBase
    {
        /// <summary>
        /// Singleton instance variable
        /// </summary>
#if __MULTI__
        // ReSharper disable InconsistentNaming
        static readonly object mutexObj = new object();
        static readonly Dictionary<int, CommonAppBlocks> multiInstance =
            new Dictionary<int, CommonAppBlocks>();
        // ReSharper restore InconsistentNaming
#else
        static readonly CommonAppBlocks instance = new CommonAppBlocks();
#endif

        private FlowTabController flowTabControllerForm;
        private IsCustomerLookedUp isCustomerLookedUpBlock;

        public enum ValidFormBlockTypes : uint
        {
            None,
            LookupCustBlock,
            LookupCustResultsBlock,
            ViewCustInfoBlock,
            UpdateCustomerDetailsBlock,
            UpdateCustomerStatus,
            UpdateAddress,
            UpdateCustomerContactDetails,
            UpdateCommentsandNotes,
            UpdateCustomerIdentification,
            ViewPersonalInformationHistory,
            SupportCustomerComment,
            Controller_ProductServices,
            AddViewSupportCustomerComment,
            Controller_ProductHistory,
            Controller_ItemHistory,
            Controller_Stats,
            PDLoanOtherDetails,
            GunBookEditBlock,
            CustReplaceBlock,
            DescribeItemBlock,
            DescribeMerchandiseBlock,
            ExtendedDepositDate
        }
        /// <summary>
        /// Static constructor - forces compiler to initialize the object prior to any code access
        /// </summary>
        #region Constructor
        /*__________________________________________________________________________________________*/
        static CommonAppBlocks()
        {
        }
        /// <summary>
        /// Static instance property accessor
        /// </summary>
        /*__________________________________________________________________________________________*/
        public static CommonAppBlocks Instance
        {
            get
            {
#if (!__MULTI__)
                return (instance);
#else
                lock (mutexObj)
                {
                    int tId = Thread.CurrentThread.ManagedThreadId;
                    if (multiInstance.ContainsKey(tId))
                    {
                        return (multiInstance[tId]);
                    }
                    var comA = new CommonAppBlocks();
                    multiInstance.Add(tId, comA);
                    return (comA);
                }
#endif
            }
        }
        /*__________________________________________________________________________________________*/
        public CommonAppBlocks()
            : base(GlobalDataAccessor.Instance.DesktopSession)
        {
            this.isCustomerLookedUpBlock = new IsCustomerLookedUp();

            //Create flow tab controller
            this.flowTabControllerForm = new FlowTabController();
            this.flowTabControllerForm.TabHandler = this.flowTabChangedHandler;

        }


        #endregion
        #region member properties
        /*__________________________________________________________________________________________*/
        public IsCustomerLookedUp IsCustomerLookedUpBlock
        {
            get
            {
                return (this.isCustomerLookedUpBlock);
            }
        }

  #endregion
        #region ControlBlocks
        /*__________________________________________________________________________________________*/
        public ShowForm LookupCustomerShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            LookupCustomer lookupCustomerFm = new LookupCustomer();
            ShowForm lookupCustBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.LookupCustBlock,
                    parentForm,
                    lookupCustomerFm,
                    lookupCustomerFm.NavControlBox,
                    fxn);
            return (lookupCustBlk);
        }
        /*__________________________________________________________________________________________*/
        public ShowForm ViewCustomerInfoShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            ViewCustomerInformation viewCustInfoFrm = new ViewCustomerInformation();
            ShowForm viewCustInfoBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.ViewCustInfoBlock,
                    parentForm,
                    viewCustInfoFrm,
                    viewCustInfoFrm.NavControlBox,
                    fxn);

            return (viewCustInfoBlk);
        }
        /*__________________________________________________________________________________________*/
        public ShowForm UpdateAddressShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            UpdateAddress UpdateAddressFrm = new UpdateAddress();
            ShowForm UpdateAddressBlk =
                this.createShowFormBlock(
                (uint)ValidFormBlockTypes.UpdateAddress,
                parentForm,
                UpdateAddressFrm,
                UpdateAddressFrm.NavControlBox,
                fxn);
            return (UpdateAddressBlk);
        }
        /*__________________________________________________________________________________________*/
        public ShowForm UpdateCustomerDetailsShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            UpdateCustomerDetails UpdateCustomerDetailsFrm = new UpdateCustomerDetails();
            ShowForm UpdateCustomerDetailsBlk =
                this.createShowFormBlock(
                (uint)ValidFormBlockTypes.UpdateCustomerDetailsBlock,
                parentForm,
                UpdateCustomerDetailsFrm,
                UpdateCustomerDetailsFrm.NavControlBox,
                fxn);

            return (UpdateCustomerDetailsBlk);
        }
        /*__________________________________________________________________________________________*/
        public ShowForm UpdateCustomerStatusShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            UpdateCustomerStatus UpdateCustomerStatusResFm = new UpdateCustomerStatus();
            ShowForm UpdateCustomerStatusResultsBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.UpdateCustomerStatus,
                    parentForm,
                    UpdateCustomerStatusResFm,
                    UpdateCustomerStatusResFm.NavControlBox,
                    fxn);
            return (UpdateCustomerStatusResultsBlk);
        }
        /*__________________________________________________________________________________________*/
        public ShowForm LookupCustomerResultsBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            LookupCustomerResults lookupCustomerResFm = new LookupCustomerResults();
            ShowForm lookupCustResultsBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.LookupCustResultsBlock,
                    parentForm,
                    lookupCustomerResFm,
                    lookupCustomerResFm.NavControlBox,
                    fxn);
            return (lookupCustResultsBlk);
        }
        /*__________________________________________________________________________________________*/
        public ShowForm UpdateCustomerContactDetailsShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            UpdateCustomerContactDetails UpdateCustomerContactDetailsResFrm = new UpdateCustomerContactDetails();
            ShowForm UpdateCustomerContactDetailsResultsBlk =
                this.createShowFormBlock(
                (uint)ValidFormBlockTypes.UpdateCustomerContactDetails,
                parentForm,
                UpdateCustomerContactDetailsResFrm,
                UpdateCustomerContactDetailsResFrm.NavControlBox,
                fxn);
            return (UpdateCustomerContactDetailsResultsBlk);

        }
        /*__________________________________________________________________________________________*/
        public ShowForm ViewPersonalInformationHistoryShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            ViewPersonalInformationHistory ViewPersonalInformationHistoryResFm = new ViewPersonalInformationHistory();
            ShowForm ViewPersonalInformationHistoryShowBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.ViewPersonalInformationHistory,
                    parentForm,
                    ViewPersonalInformationHistoryResFm,
                    ViewPersonalInformationHistoryResFm.NavControlBox,
                    fxn);
            return (ViewPersonalInformationHistoryShowBlk);
        }
        /*__________________________________________________________________________________________*/
        public ShowForm UpdateCommentsandNotesShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            UpdateCommentsandNotes UpdateCommentsandNotesResFrm = new UpdateCommentsandNotes();
            ShowForm UpdateCommentsandNotesResultsBlk =
                this.createShowFormBlock(
                (uint)ValidFormBlockTypes.UpdateCommentsandNotes,
                parentForm,
                UpdateCommentsandNotesResFrm,
                UpdateCommentsandNotesResFrm.NavControlBox,
                fxn);
            return (UpdateCommentsandNotesResultsBlk);

        }
        /*__________________________________________________________________________________________*/
        public ShowForm UpdateCustomerIdentificationShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            UpdateCustomerIdentification UpdateCustomerIdentificationResFrm = new UpdateCustomerIdentification();
            ShowForm UpdateCustomerIdentificationResultsBlk =
                this.createShowFormBlock(
                (uint)ValidFormBlockTypes.UpdateCustomerIdentification,
                parentForm,
                UpdateCustomerIdentificationResFrm,
                UpdateCustomerIdentificationResFrm.NavControlBox,
                fxn);
            return (UpdateCustomerIdentificationResultsBlk);

        }
#region PRODUCT TABS
        //Controller_ProductServices
        /*__________________________________________________________________________________________*/
        public ShowForm Controller_ProductServicesShowBlock(
                        Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            Controller_ProductServices Controller_ProductServicesResFrm = new Controller_ProductServices();
            ShowForm Controller_ProductServicesResultsBlk =
                this.createShowFormBlock(
                (uint)ValidFormBlockTypes.Controller_ProductServices,
                parentForm,
                Controller_ProductServicesResFrm,
                Controller_ProductServicesResFrm.NavControlBox,
                fxn);
            return (Controller_ProductServicesResultsBlk);

        }
        //Controller_ProductHistory
       /*__________________________________________________________________________________________*/
        public ShowForm Controller_ProductHistoryShowBlock(
                        Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            Controller_ProductHistory Controller_ProductHistoryResFrm = new Controller_ProductHistory();
            ShowForm Controller_ProductHistoryResultsBlk =
                this.createShowFormBlock(
                (uint)ValidFormBlockTypes.Controller_ProductHistory,
                parentForm,
                Controller_ProductHistoryResFrm,
                Controller_ProductHistoryResFrm.NavControlBox,
                fxn);
            return (Controller_ProductHistoryResultsBlk);
        }
        //Controller_ItemHistory
/*__________________________________________________________________________________________*/
        public ShowForm Controller_ItemHistoryShowBlock(
                        Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            Controller_ItemHistory Controller_ItemHistoryResFrm = new Controller_ItemHistory();
            ShowForm Controller_ItemHistoryResultsBlk =
                this.createShowFormBlock(
                (uint)ValidFormBlockTypes.Controller_ItemHistory,
                parentForm,
                Controller_ItemHistoryResFrm,
                Controller_ItemHistoryResFrm.NavControlBox,
                fxn);
            return (Controller_ItemHistoryResultsBlk);
        }
/*__________________________________________________________________________________________*/
        public ShowForm Controller_StatsShowBlock(
                        Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            Controller_Stats Controller_StatsResFrm = new Controller_Stats();
            ShowForm Controller_StatsResultsBlk =
                this.createShowFormBlock(
                (uint)ValidFormBlockTypes.Controller_Stats,
                parentForm,
                Controller_StatsResFrm,
                Controller_StatsResFrm.NavControlBox,
                fxn);
            return (Controller_StatsResultsBlk);
        }
        /*__________________________________________________________________________________________*/
        public ShowForm SupportCustomerCommentShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            SupportCustomerComments SupportCustomerCommentResFm = new SupportCustomerComments();
            ShowForm SupportCustomerCommentResultsBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.SupportCustomerComment,
                    parentForm,
                    SupportCustomerCommentResFm,
                    SupportCustomerCommentResFm.NavControlBox,
                    fxn);
            return (SupportCustomerCommentResultsBlk);
        }
        /*__________________________________________________________________________________________*/
        public ShowForm AddViewSupportCustomerCommentShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            AddViewSupportCustomerComment AddViewSupportCustomerCommentResFm = new AddViewSupportCustomerComment();
            ShowForm AddViewSupportCustomerCommentResultsBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.AddViewSupportCustomerComment,
                    parentForm,
                    AddViewSupportCustomerCommentResFm,
                    AddViewSupportCustomerCommentResFm.NavControlBox,
                    fxn, true);

            return (AddViewSupportCustomerCommentResultsBlk);
        }
        /*__________________________________________________________________________________________*/
        public ShowForm PDLoanOtherDetailsShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            PDLoanOtherDetails PDLOanOtherDetailsResFm = new PDLoanOtherDetails();
            ShowForm PDLoanOtherDetailsResultsBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.PDLoanOtherDetails,
                    parentForm,
                    PDLOanOtherDetailsResFm,
                    PDLOanOtherDetailsResFm.NavControlBox,
                    fxn, true);

            return (PDLoanOtherDetailsResultsBlk);
        }
        /*__________________________________________________________________________________________*/
        public ShowForm ExtendedDepositDateShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            ExtendedDepositDate ExtendedDepositDateResFm = new ExtendedDepositDate();
            ShowForm ExtendedDepositDateResultsBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.ExtendedDepositDate,
                    parentForm,
                    ExtendedDepositDateResFm,
                    ExtendedDepositDateResFm.NavControlBox,
                    fxn, true);

            return (ExtendedDepositDateResultsBlk);
        }
#endregion
        #endregion
        #region Class Block Methods
        /*__________________________________________________________________________________________*/
        private void flowTabChangedHandler()
        {
            FlowTabController.State curState = this.flowTabControllerForm.TabState;
            Form frm = this.flowTabControllerForm.BelowForm;

            /*_________________________________________________*/
            if (frm.GetType() == typeof(ViewCustomerInformation))
            {
                if (curState == FlowTabController.State.ProductsAndServices)
                {
                    ((ViewCustomerInformation)frm).NavControlBox.IsCustom = true;
                    ((ViewCustomerInformation)frm).NavControlBox.CustomDetail = "ProductsAndServices";
                    ((ViewCustomerInformation)frm).NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }
                else if (curState == FlowTabController.State.ProductHistory)
                {
                    ((ViewCustomerInformation)frm).NavControlBox.IsCustom = true;
                    ((ViewCustomerInformation)frm).NavControlBox.CustomDetail = "Controller_ProductHistory";
                    ((ViewCustomerInformation)frm).NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }
                else if (curState == FlowTabController.State.ItemHistory)
                {
                    ((ViewCustomerInformation)frm).NavControlBox.IsCustom = true;
                    ((ViewCustomerInformation)frm).NavControlBox.CustomDetail = "Controller_ItemHistory";
                    ((ViewCustomerInformation)frm).NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }
                else if (curState == FlowTabController.State.Stats)
                {
                    ((ViewCustomerInformation)frm).NavControlBox.IsCustom = true;
                    ((ViewCustomerInformation)frm).NavControlBox.CustomDetail = "Controller_Stats";
                    ((ViewCustomerInformation)frm).NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }
                else if (curState == FlowTabController.State.Comments)
                {
                    ((ViewCustomerInformation)frm).NavControlBox.IsCustom = true;
                    ((ViewCustomerInformation)frm).NavControlBox.CustomDetail = "SupportCustomerComment";
                    ((ViewCustomerInformation)frm).NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }
            }
            /*_________________________________________________*/
            else if (frm.GetType() == typeof(Controller_ProductServices))
            {
                if (curState == FlowTabController.State.Customer)
                {
                    ((Controller_ProductServices)frm).NavControlBox.IsCustom = true;
                    ((Controller_ProductServices)frm).NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly";
                    ((Controller_ProductServices)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.ProductHistory)
                {
                    ((Controller_ProductServices)frm).NavControlBox.IsCustom = true;
                    ((Controller_ProductServices)frm).NavControlBox.CustomDetail = "Controller_ProductHistory";
                    ((Controller_ProductServices)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.ItemHistory)
                {
                    ((Controller_ProductServices)frm).NavControlBox.IsCustom = true;
                    ((Controller_ProductServices)frm).NavControlBox.CustomDetail = "Controller_ItemHistory";
                    ((Controller_ProductServices)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.Stats)
                {
                    ((Controller_ProductServices)frm).NavControlBox.IsCustom = true;
                    ((Controller_ProductServices)frm).NavControlBox.CustomDetail = "Controller_Stats";
                    ((Controller_ProductServices)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.Comments)
                {
                    ((Controller_ProductServices)frm).NavControlBox.IsCustom = true;
                    ((Controller_ProductServices)frm).NavControlBox.CustomDetail = "SupportCustomerComment";
                    ((Controller_ProductServices)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }

            }
            /*_________________________________________________*/
            else if (frm.GetType() == typeof(Controller_ProductHistory))
            {
                if (curState == FlowTabController.State.Customer)
                {
                    ((Controller_ProductHistory)frm).NavControlBox.IsCustom = true;
                    ((Controller_ProductHistory)frm).NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly";
                    ((Controller_ProductHistory)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.ProductsAndServices)
                {
                    ((Controller_ProductHistory)frm).NavControlBox.IsCustom = true;
                    ((Controller_ProductHistory)frm).NavControlBox.CustomDetail = "ProductsAndServices";
                    ((Controller_ProductHistory)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.ItemHistory)
                {
                    ((Controller_ProductHistory)frm).NavControlBox.IsCustom = true;
                    ((Controller_ProductHistory)frm).NavControlBox.CustomDetail = "Controller_ItemHistory";
                    ((Controller_ProductHistory)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.Stats)
                {
                    ((Controller_ProductHistory)frm).NavControlBox.IsCustom = true;
                    ((Controller_ProductHistory)frm).NavControlBox.CustomDetail = "Controller_Stats";
                    ((Controller_ProductHistory)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.Comments)
                {
                    ((Controller_ProductHistory)frm).NavControlBox.IsCustom = true;
                    ((Controller_ProductHistory)frm).NavControlBox.CustomDetail = "SupportCustomerComment";
                    ((Controller_ProductHistory)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
            }
            /*_________________________________________________*/
            else if (frm.GetType() == typeof(Controller_ItemHistory))
            {
                if (curState == FlowTabController.State.Customer)
                {
                    ((Controller_ItemHistory)frm).NavControlBox.IsCustom = true;
                    ((Controller_ItemHistory)frm).NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly";
                    ((Controller_ItemHistory)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.ProductsAndServices)
                {
                    ((Controller_ItemHistory)frm).NavControlBox.IsCustom = true;
                    ((Controller_ItemHistory)frm).NavControlBox.CustomDetail = "ProductsAndServices";
                    ((Controller_ItemHistory)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.ProductHistory)
                {
                    ((Controller_ItemHistory)frm).NavControlBox.IsCustom = true;
                    ((Controller_ItemHistory)frm).NavControlBox.CustomDetail = "Controller_ProductHistory";
                    ((Controller_ItemHistory)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.Stats)
                {
                    ((Controller_ItemHistory)frm).NavControlBox.IsCustom = true;
                    ((Controller_ItemHistory)frm).NavControlBox.CustomDetail = "Controller_Stats";
                    ((Controller_ItemHistory)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.Comments)
                {
                    ((Controller_ItemHistory)frm).NavControlBox.IsCustom = true;
                    ((Controller_ItemHistory)frm).NavControlBox.CustomDetail = "SupportCustomerComment";
                    ((Controller_ItemHistory)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
            }
            /*_________________________________________________*/
            else if (frm.GetType() == typeof(Controller_Stats))
            {
                if (curState == FlowTabController.State.Customer)
                {
                    ((Controller_Stats)frm).NavControlBox.IsCustom = true;
                    ((Controller_Stats)frm).NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly";
                    ((Controller_Stats)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.ProductsAndServices)
                {
                    ((Controller_Stats)frm).NavControlBox.IsCustom = true;
                    ((Controller_Stats)frm).NavControlBox.CustomDetail = "ProductsAndServices";
                    ((Controller_Stats)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.ProductHistory)
                {
                    ((Controller_Stats)frm).NavControlBox.IsCustom = true;
                    ((Controller_Stats)frm).NavControlBox.CustomDetail = "Controller_ProductHistory";
                    ((Controller_Stats)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.ItemHistory)
                {
                    ((Controller_Stats)frm).NavControlBox.IsCustom = true;
                    ((Controller_Stats)frm).NavControlBox.CustomDetail = "Controller_ItemHistory";
                    ((Controller_Stats)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else if (curState == FlowTabController.State.Comments)
                {
                    ((Controller_Stats)frm).NavControlBox.IsCustom = true;
                    ((Controller_Stats)frm).NavControlBox.CustomDetail = "SupportCustomerComment";
                    ((Controller_Stats)frm).NavControlBox.Action = NavBox.NavAction.BACK;
                }
            }
            /*_________________________________________________*/
            else if (frm.GetType() == typeof(SupportCustomerComments))
            {
                if (curState == FlowTabController.State.Customer)
                {
                    ((SupportCustomerComments)frm).NavControlBox.IsCustom = true;
                    ((SupportCustomerComments)frm).NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly";
                    ((SupportCustomerComments)frm).NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }
                else if (curState == FlowTabController.State.ProductsAndServices)
                {
                    ((SupportCustomerComments)frm).NavControlBox.IsCustom = true;
                    ((SupportCustomerComments)frm).NavControlBox.CustomDetail = "ProductsAndServices";
                    ((SupportCustomerComments)frm).NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }
                else if (curState == FlowTabController.State.ProductHistory)
                {
                    ((SupportCustomerComments)frm).NavControlBox.IsCustom = true;
                    ((SupportCustomerComments)frm).NavControlBox.CustomDetail = "Controller_ProductHistory";
                    ((SupportCustomerComments)frm).NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }
                else if (curState == FlowTabController.State.ItemHistory)
                {
                    ((SupportCustomerComments)frm).NavControlBox.IsCustom = true;
                    ((SupportCustomerComments)frm).NavControlBox.CustomDetail = "Controller_ItemHistory";
                    ((SupportCustomerComments)frm).NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }
                else if (curState == FlowTabController.State.Stats)
                {
                    ((SupportCustomerComments)frm).NavControlBox.IsCustom = true;
                    ((SupportCustomerComments)frm).NavControlBox.CustomDetail = "Controller_Stats";
                    ((SupportCustomerComments)frm).NavControlBox.Action = NavBox.NavAction.SUBMIT;
                }

            }
        }
        /*__________________________________________________________________________________________*/
        public void ShowFlowTabController(Form parentForm, Form belowForm, FlowTabController.State state)
        {
            if (parentForm == null || belowForm == null)
                return;


            int flowTabHeight = this.flowTabControllerForm.Height;
            int belowFormX = belowForm.Location.X;
            int belowFormY = belowForm.Location.Y;
            this.flowTabControllerForm.Location =
                new Point(belowFormX, belowFormY - flowTabHeight);
            parentForm.Controls.Add(this.flowTabControllerForm);
            this.flowTabControllerForm.Enabled = true;
            this.flowTabControllerForm.Visible = true;
            this.flowTabControllerForm.BelowForm = belowForm;
            this.flowTabControllerForm.TabState = state;
            SetTabEnabledFlagToggle();
            
            this.flowTabControllerForm.Show();
            this.flowTabControllerForm.BringToFront();
        }
        /*__________________________________________________________________________________________*/
        public void SetTabEnabledFlagToggle()
        {
            // disable tabs here
            this.flowTabControllerForm.setTabEnabledFlag(FlowTabController.State.Customer, true);
            this.flowTabControllerForm.setTabEnabledFlag(FlowTabController.State.ProductHistory, false);
            this.flowTabControllerForm.setTabEnabledFlag(FlowTabController.State.ProductsAndServices, true);
            this.flowTabControllerForm.setTabEnabledFlag(FlowTabController.State.ItemHistory, false);
            this.flowTabControllerForm.setTabEnabledFlag(FlowTabController.State.Stats, false);
            this.flowTabControllerForm.setTabEnabledFlag(FlowTabController.State.Comments, true);
        }
        /*__________________________________________________________________________________________*/
        public void HideFlowTabController()
        {
            this.flowTabControllerForm.Enabled = false;
            this.flowTabControllerForm.Hide();
        }
        /*__________________________________________________________________________________________*/
        public void SetFlowTabEnabled(FlowTabController.State tabState, bool stateEnabled)
        {
            this.flowTabControllerForm.setTabEnabledFlag(tabState, stateEnabled);
            this.flowTabControllerForm.Refresh();

        }
        /*__________________________________________________________________________________________*/
        public void HideTabInFlowTab(FlowTabController.State tabName)
        {
            this.flowTabControllerForm.setTabEnabledFlag(tabName, false);
        }
        /*__________________________________________________________________________________________*/
        public void SetTabState(FlowTabController.State tabName)
        {
            this.flowTabControllerForm.TabState = tabName;
        }


        /*
    if (curState == FlowTabController.State.Customer)
    {
        if (frm.GetType() == typeof(Controller_ProductServices))
        {
            ((Controller_ProductServices)frm).NavControlBox.IsCustom = true;
            ((Controller_ProductServices)frm).NavControlBox.CustomDetail = "Customer";
            ((Controller_ProductServices)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(Controller_Stats))
        {
            ((Controller_Stats)frm).NavControlBox.IsCustom = true;
            ((Controller_Stats)frm).NavControlBox.CustomDetail = "Customer";
            ((Controller_Stats)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(Controller_ItemHistory))
        {
            ((Controller_ItemHistory)frm).NavControlBox.IsCustom = true;
            ((Controller_ItemHistory)frm).NavControlBox.CustomDetail = "Customer";
            ((Controller_ItemHistory)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(Controller_ProductHistory))
        {
            ((Controller_ProductHistory)frm).NavControlBox.IsCustom = true;
            ((Controller_ProductHistory)frm).NavControlBox.CustomDetail = "Customer";
            ((Controller_ProductHistory)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(ManageMultiplePawnItems))
        {
            ((ManageMultiplePawnItems)frm).NavControlBox.IsCustom = true;
            ((ManageMultiplePawnItems)frm).NavControlBox.CustomDetail = "Customer";
            ((ManageMultiplePawnItems)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(DescribeMerchandise))
        {
            ((DescribeMerchandise)frm).NavControlBox.IsCustom = true;
            ((DescribeMerchandise)frm).NavControlBox.CustomDetail = "Customer";
            ((DescribeMerchandise)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }
        else if (frm.GetType() == typeof(DescribeItem))
        {
            ((DescribeItem)frm).NavControlBox.IsCustom = true;
            ((DescribeItem)frm).NavControlBox.CustomDetail = "Customer";
            ((DescribeItem)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
    }
                
    else if (curState == FlowTabController.State.ProductsAndServices)
    {

        if (frm.GetType() == typeof(ViewCustomerInformation))
        {
            ((ViewCustomerInformation)frm).NavControlBox.IsCustom = true;
            ((ViewCustomerInformation)frm).NavControlBox.CustomDetail = "ProductsAndServices";
            ((ViewCustomerInformation)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(Controller_Stats))
        {
            ((Controller_Stats)frm).NavControlBox.IsCustom = true;
            ((Controller_Stats)frm).NavControlBox.CustomDetail = "ProductsAndServices";
            ((Controller_Stats)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(Controller_ItemHistory))
        {
            ((Controller_ItemHistory)frm).NavControlBox.IsCustom = true;
            ((Controller_ItemHistory)frm).NavControlBox.CustomDetail = "ProductsAndServices";
            ((Controller_ItemHistory)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(Controller_ProductHistory))
        {
            ((Controller_ProductHistory)frm).NavControlBox.IsCustom = true;
            ((Controller_ProductHistory)frm).NavControlBox.CustomDetail = "ProductsAndServices";
            ((Controller_ProductHistory)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }

    }
    else if (curState == FlowTabController.State.Stats)
    {


        if (frm.GetType() == typeof(Controller_ProductServices))
        {
            ((Controller_ProductServices)frm).NavControlBox.IsCustom = true;
            ((Controller_ProductServices)frm).NavControlBox.CustomDetail = "ProductStats";
            ((Controller_ProductServices)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(ViewCustomerInformation))
        {
            ((ViewCustomerInformation)frm).NavControlBox.IsCustom = true;
            ((ViewCustomerInformation)frm).NavControlBox.CustomDetail = "ProductStats";
            ((ViewCustomerInformation)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(Controller_ItemHistory))
        {
            ((Controller_ItemHistory)frm).NavControlBox.IsCustom = true;
            ((Controller_ItemHistory)frm).NavControlBox.CustomDetail = "ProductStats";
            ((Controller_ItemHistory)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(Controller_ProductHistory))
        {
            ((Controller_ProductHistory)frm).NavControlBox.IsCustom = true;
            ((Controller_ProductHistory)frm).NavControlBox.CustomDetail = "ProductStats";
            ((Controller_ProductHistory)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(ManageMultiplePawnItems))
        {
            ((ManageMultiplePawnItems)frm).NavControlBox.IsCustom = true;
            ((ManageMultiplePawnItems)frm).NavControlBox.CustomDetail = "ProductStats";
            ((ManageMultiplePawnItems)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(DescribeMerchandise))
        {
            ((DescribeMerchandise)frm).NavControlBox.IsCustom = true;
            ((DescribeMerchandise)frm).NavControlBox.CustomDetail = "ProductStats";
            ((DescribeMerchandise)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }
        else if (frm.GetType() == typeof(DescribeItem))
        {
            ((DescribeItem)frm).NavControlBox.IsCustom = true;
            ((DescribeItem)frm).NavControlBox.CustomDetail = "ProductStats";
            ((DescribeItem)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
    }
    else if (curState == FlowTabController.State.ItemHistory)
    {
        if (frm.GetType() == typeof(Controller_ProductServices))
        {
            ((Controller_ProductServices)frm).NavControlBox.IsCustom = true;
            ((Controller_ProductServices)frm).NavControlBox.CustomDetail = "ItemHistory";
            ((Controller_ProductServices)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(ViewCustomerInformation))
        {
            ((ViewCustomerInformation)frm).NavControlBox.IsCustom = true;
            ((ViewCustomerInformation)frm).NavControlBox.CustomDetail = "ItemHistory";
            ((ViewCustomerInformation)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(Controller_Stats))
        {
            ((Controller_Stats)frm).NavControlBox.IsCustom = true;
            ((Controller_Stats)frm).NavControlBox.CustomDetail = "ItemHistory";
            ((Controller_Stats)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(Controller_ProductHistory))
        {
            ((Controller_ProductHistory)frm).NavControlBox.IsCustom = true;
            ((Controller_ProductHistory)frm).NavControlBox.CustomDetail = "ItemHistory";
            ((Controller_ProductHistory)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(ManageMultiplePawnItems))
        {
            ((ManageMultiplePawnItems)frm).NavControlBox.IsCustom = true;
            ((ManageMultiplePawnItems)frm).NavControlBox.CustomDetail = "ItemHistory";
            ((ManageMultiplePawnItems)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(DescribeMerchandise))
        {
            ((DescribeMerchandise)frm).NavControlBox.IsCustom = true;
            ((DescribeMerchandise)frm).NavControlBox.CustomDetail = "ItemHistory";
            ((DescribeMerchandise)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }
        else if (frm.GetType() == typeof(DescribeItem))
        {
            ((DescribeItem)frm).NavControlBox.IsCustom = true;
            ((DescribeItem)frm).NavControlBox.CustomDetail = "ItemHistory";
            ((DescribeItem)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
    }
    else if (curState == FlowTabController.State.ProductHistory)
    {
        if (frm.GetType() == typeof(Controller_ProductServices))
        {
            ((Controller_ProductServices)frm).NavControlBox.IsCustom = true;
            ((Controller_ProductServices)frm).NavControlBox.CustomDetail = "ProductHistory";
            ((Controller_ProductServices)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(ViewCustomerInformation))
        {
            ((ViewCustomerInformation)frm).NavControlBox.IsCustom = true;
            ((ViewCustomerInformation)frm).NavControlBox.CustomDetail = "ProductHistory";
            ((ViewCustomerInformation)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(Controller_Stats))
        {
            ((Controller_Stats)frm).NavControlBox.IsCustom = true;
            ((Controller_Stats)frm).NavControlBox.CustomDetail = "ProductHistory";
            ((Controller_Stats)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(Controller_ItemHistory))
        {
            ((Controller_ItemHistory)frm).NavControlBox.IsCustom = true;
            ((Controller_ItemHistory)frm).NavControlBox.CustomDetail = "ProductHistory";
            ((Controller_ItemHistory)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(ManageMultiplePawnItems))
        {
            ((ManageMultiplePawnItems)frm).NavControlBox.IsCustom = true;
            ((ManageMultiplePawnItems)frm).NavControlBox.CustomDetail = "ProductHistory";
            ((ManageMultiplePawnItems)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }
        else if (frm.GetType() == typeof(DescribeMerchandise))
        {
            ((DescribeMerchandise)frm).NavControlBox.IsCustom = true;
            ((DescribeMerchandise)frm).NavControlBox.CustomDetail = "ProductHistory";
            ((DescribeMerchandise)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }
        else if (frm.GetType() == typeof(DescribeItem))
        {
            ((DescribeItem)frm).NavControlBox.IsCustom = true;
            ((DescribeItem)frm).NavControlBox.CustomDetail = "ProductHistory";
            ((DescribeItem)frm).NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }
    } 
}
*/

        /// <summary>
        /// Create a block to show gun book search form
        /// </summary>
        /// <returns></returns>
        public ShowForm GunBookSearchFormBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            GunBookSearch gunBookSearchFrm = new GunBookSearch();
            ShowForm gunBookSearchBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.GunBookEditBlock,
                    parentForm,
                    gunBookSearchFrm,
                    gunBookSearchFrm.NavControlBox,
                    fxn);
            return (gunBookSearchBlk);
        }

        /// <summary>
        /// Create and return a showform block for Describe merchandise form
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="fxn"></param>
        /// <returns></returns>
        public ShowForm DescribeMerchandiseGunEditBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            DescribeMerchandise descMerchFrm = new DescribeMerchandise(GlobalDataAccessor.Instance.DesktopSession, CurrentContext.GUNEDIT);
            ShowForm describeMerchBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.DescribeMerchandiseBlock,
                    parentForm,
                    descMerchFrm,
                    descMerchFrm.NavControlBox,
                    fxn);
            return (describeMerchBlk);

        }
        public ShowForm DescribeItemBlock(
    Form parentForm,
    NavBox.NavBoxActionFired fxn)
        {
            DescribeItem descItemFrm = new DescribeItem(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext,
                GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex);
            ShowForm describeItemBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.DescribeItemBlock,
                    parentForm,
                    descItemFrm,
                    descItemFrm.NavControlBox,
                    fxn);
            return (describeItemBlk);

        }

        public ShowForm DescribeItemGunEditBlock(
    Form parentForm,
    NavBox.NavBoxActionFired fxn)
        {
            DescribeItem descItemFrm = new DescribeItem(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext,
                GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex)
            {
                SelectedProKnowMatch = null
            };
            ShowForm describeItemBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.DescribeItemBlock,
                    parentForm,
                    descItemFrm,
                    descItemFrm.NavControlBox,
                    fxn, true);
            return (describeItemBlk);

        }

        /// <summary>
        /// Create a block to show gun book edit form
        /// </summary>
        /// <returns></returns>
        public ShowForm GunBookEditFormBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            EditGunBookRecord gunBookEditFrm = new EditGunBookRecord();
            ShowForm gunBookEditBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.GunBookEditBlock,
                    parentForm,
                    gunBookEditFrm,
                    gunBookEditFrm.NavControlBox,
                    fxn);
            return (gunBookEditBlk);
        }

        /// <summary>
        /// Create a block to show customer replace form in edit gun book flow
        /// </summary>
        /// <returns></returns>
        public ShowForm CustomerReplaceBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            CustomerReplace custReplaceFrm = new CustomerReplace();
            ShowForm custReplaceBlk =
                this.createShowFormBlock(
                    (uint)ValidFormBlockTypes.CustReplaceBlock,
                    parentForm,
                    custReplaceFrm,
                    custReplaceFrm.NavControlBox,
                    fxn);
            return (custReplaceBlk);
        }

        #endregion

    }
}
