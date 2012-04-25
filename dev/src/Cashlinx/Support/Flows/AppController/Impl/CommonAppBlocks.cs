﻿using System.Drawing;
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
using Support.Libraries.Forms;
using Support.Forms.Customer.Products;
using Support.Forms.Customer;
using Support.Forms.Pawn.Customer;
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
        /// <summary>
        /// Static constructor - forces compiler to initialize the object prior to any code access
        /// </summary>
        static CommonAppBlocks()
        {
        }



        /// <summary>
        /// Static instance property accessor
        /// </summary>
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
        #region Constructor
        /*__________________________________________________________________________________________*/
        public void HideFlowTabController()
        {
            this.flowTabControllerForm.Enabled = false;
            this.flowTabControllerForm.Hide();
        }

        /*__________________________________________________________________________________________*/
        public CommonAppBlocks()
            : base(GlobalDataAccessor.Instance.DesktopSession)
        {
            this.isCustomerLookedUpBlock = new IsCustomerLookedUp();
            //this.isBuyMerchandiseLookedUpBlock = new IsBuyMerchandiseLookedUp();
            //this.isLoanMerchandiseLookedUpBlock = new IsLoanMerchandiseLookedUp();
            //this.loadCustomerLoanKeysBlock = new LoadCustomerLoanKeys();

            //Create flow tab controller
            this.flowTabControllerForm = new FlowTabController();
            this.flowTabControllerForm.TabHandler = this.flowTabChangedHandler;

        }
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
            Controller_ProductServices
        }




        #endregion
        #region member properties
        /*__________________________________________________________________________________________*/
        private IsCustomerLookedUp isCustomerLookedUpBlock;
        public IsCustomerLookedUp IsCustomerLookedUpBlock
        {
            get
            {
                return (this.isCustomerLookedUpBlock);
            }
        }

        /*    
        
               private IsBuyMerchandiseLookedUp isBuyMerchandiseLookedUpBlock;
               public IsBuyMerchandiseLookedUp IsBuyMerchandiseLookedUpBlock
               {
                   get
                   {
                       return (this.isBuyMerchandiseLookedUpBlock);
                   }
               }

               private IsLoanMerchandiseLookedUp isLoanMerchandiseLookedUpBlock;
               public IsLoanMerchandiseLookedUp IsLoanMerchandiseLookedUpBlock
               {
                   get
                   {
                       return (this.isLoanMerchandiseLookedUpBlock);
                   }
               }

               private LoadCustomerLoanKeys loadCustomerLoanKeysBlock;
               public LoadCustomerLoanKeys LoadCustomerLoanKeysBlock
               {
                   get
                   {
                       return (this.loadCustomerLoanKeysBlock);
                   }
               }
       */
        /*__________________________________________________________________________________________*/
        private FlowTabController flowTabControllerForm;

        public void SetFlowTabEnabled(FlowTabController.State tabState, bool stateEnabled)
        {
            this.flowTabControllerForm.setTabEnabledFlag(tabState, stateEnabled);
            this.flowTabControllerForm.Refresh();

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
        public ShowForm SupportCustomerCommentShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            SupportCustomerComment SupportCustomerCommentResFm = new SupportCustomerComment();
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

        #endregion
        #region CommentedOut ControlBlocks
        /// <summary>
        /// Create a block to show LoanInquiry
        /// </summary>
        /// <returns></returns>
        /*__________________________________________________________________________________________*/
        /*
        public ShowForm LoanInquiryShowBlock(
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
        */
        /// <summary>
        /// Create a block to show LookupCustomer
        /// </summary>
        /// <returns></returns>
        /*        public ShowForm CreateItemSearchShowBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    ItemSearch itemSearchFm = new ItemSearch();
                    ShowForm itemSearchBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.LookupCustBlock,
                            parentForm,
                            itemSearchFm,
                            itemSearchFm.NavControlBox,
                            fxn);
                    return (itemSearchBlk);
                }

                public ShowForm CreateChangeRetailPriceSearchShowBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    ChangeRetailPriceSearch itemSearchFm = new ChangeRetailPriceSearch(GlobalDataAccessor.Instance.DesktopSession);
                    ShowForm itemSearchBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.LookupCustBlock,
                            parentForm,
                            itemSearchFm,
                            itemSearchFm.NavControlBox,
                            fxn);
                    return (itemSearchBlk);
                }

                public ShowForm CreateManageTransferInShowBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    ManageTransferIn transferInFm = new ManageTransferIn();
                    ShowForm manageTransferInBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.LookupCustBlock,
                            parentForm,
                            transferInFm,
                            transferInFm.NavControlBox,
                            fxn);
                    return (manageTransferInBlk);
                }

                public ShowForm CreateSelectTransferMethodShowBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    SelectTransferInMethod selectTransferInMethodFm = new SelectTransferInMethod();
                    ShowForm selectTransferInMethodBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.LookupCustBlock,
                            parentForm,
                            selectTransferInMethodFm,
                            selectTransferInMethodFm.NavControlBox,
                            fxn);
                    return (selectTransferInMethodBlk);
                }

                public ShowForm CreateTransferRejectCommentShowBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    TransferRejectComment transferRejectCommentFm = new TransferRejectComment();
                    ShowForm transferRejectCommentBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.LookupCustBlock,
                            parentForm,
                            transferRejectCommentFm,
                            transferRejectCommentFm.NavControlBox,
                            fxn);
                    return (transferRejectCommentBlk);
                }

                public ShowForm CreateSelectTransferInItemsShowBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    SelectTransferInItems selectTransferInItemsFm = new SelectTransferInItems();
                    ShowForm selectTransferInItemsBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.LookupCustBlock,
                            parentForm,
                            selectTransferInItemsFm,
                            selectTransferInItemsFm.NavControlBox,
                            fxn);
                    return (selectTransferInItemsBlk);
                }

                /// <summary>
                /// Create a block to show LookupCustomer
                /// </summary>
                /// <returns></returns>
                public ShowForm CreateLookupVendorShowBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    LookupVendor lookupVendorFm = new LookupVendor();
                    ShowForm lookupCustBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.LookupCustBlock,
                            parentForm,
                            lookupVendorFm,
                            lookupVendorFm.NavControlBox,
                            fxn);
                    return (lookupCustBlk);
                }

                /// <summary>
                /// Create a block to show LookupVendorResults
                /// </summary>
                /// <returns></returns>
                public ShowForm CreateLookupVendorResultsBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    LookupVendorResults lookupVendorResFm = new LookupVendorResults();
                    ShowForm lookupVendResultsBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.LookupVendResultsBlock,
                            parentForm,
                            lookupVendorResFm,
                            lookupVendorResFm.NavControlBox,
                            fxn);
                    return (lookupVendResultsBlk);
                }

                /// <summary>
                /// Create a block to show LookupTicket
                /// </summary>
                /// <returns></returns>
                public ShowForm CreateLookupTicketShowBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    LookupTicket lookupTicketFm = new LookupTicket();
                    ShowForm lookupTktBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.LookupTktBlock,
                            parentForm,
                            lookupTicketFm,
                            lookupTicketFm.NavControlBox,
                            fxn);
                    return (lookupTktBlk);
                }

                /// <summary>
                /// Create a block to show LookupTicketResults
                /// </summary>
                /// <returns></returns>
                public ShowForm CreateLookupTicketResultsShowBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    LookupTicketResults lookupTicketResFm = new LookupTicketResults();
                    ShowForm lookupTktResBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.LookupTktResBlock,
                            parentForm,
                            lookupTicketResFm,
                            lookupTicketResFm.NavControlBox,
                            fxn);
                    return (lookupTktResBlk);
                }

                public ShowForm TenderInShowBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    TenderIn tenderFrm = new TenderIn();

                    ShowForm tenderInfoBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.TenderInBlock,
                            parentForm,
                            tenderFrm,
                            tenderFrm.NavControlBox,
                            fxn);

                    return (tenderInfoBlk);
                }


                /// <summary>
                /// Create a block to show Existing Customer Form
                /// </summary>
                /// <returns></returns>
              public ShowForm ExistingCustomerFormBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {

                    ExistingCustomer existCustFrm = new ExistingCustomer();
                    ShowForm existingCustBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.ExistingCustBlock,
                            parentForm,
                            existCustFrm,
                            existCustFrm.NavControlBox,
                            fxn);

                    return (existingCustBlk);
                } 

                /// <summary>
                /// Create a block to show Update Physical Description Form
                /// </summary>
                /// <returns></returns>
                public ShowForm UpdatePhysDescShowFormBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {

                    UpdatePhysicalDesc updatePhysDescFrm = new UpdatePhysicalDesc();
                    ShowForm updatePhysDescBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.UpdatePhysicalDescBlock,
                            parentForm,
                            updatePhysDescFrm,
                            updatePhysDescFrm.NavControlBox,
                            fxn);

                    return (updatePhysDescBlk);
                }


                /// <summary>
                /// Create and return a ShowForm block for manage pawn application
                /// </summary>
                /// <returns></returns>
               public ShowForm CreateManagePawnApplicationBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    ManagePawnApplication managePawnAppFm = new ManagePawnApplication();
                    ShowForm managePawnAppBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.ManagePawnAppBlock,
                            parentForm,
                            managePawnAppFm,
                            managePawnAppFm.NavControlBox,
                            fxn);
                    return (managePawnAppBlk);
                }

                /// <summary>
                /// Create and return a showform block for Describe merchandise form
                /// </summary>
                /// <param name="parentForm"></param>
                /// <param name="fxn"></param>
                /// <returns></returns>
                public ShowForm DescribeMerchandiseBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    DescribeMerchandise descMerchFrm = new DescribeMerchandise(GlobalDataAccessor.Instance.DesktopSession);
                    ShowForm describeMerchBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.DescribeMerchandiseBlock,
                            parentForm,
                            descMerchFrm,
                            descMerchFrm.NavControlBox,
                            fxn);
                    return (describeMerchBlk);

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
                    DescribeMerchandise descMerchFrm = new DescribeMerchandise(GlobalDataAccessor.Instance.DesktopSession,CurrentContext.GUNEDIT);
                    ShowForm describeMerchBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.DescribeMerchandiseBlock,
                            parentForm,
                            descMerchFrm,
                            descMerchFrm.NavControlBox,
                            fxn);
                    return (describeMerchBlk);

                }


                /// <summary>
                /// Create and return a showform block for Describe merchandise form
                /// in PFI_MERGE context
                /// </summary>
                /// <param name="parentForm"></param>
                /// <param name="fxn"></param>
                /// <returns></returns>
                public ShowForm DescribeMerchandisePFIMergeBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    DescribeMerchandise descMerchFrm = new DescribeMerchandise(GlobalDataAccessor.Instance.DesktopSession, CurrentContext.PFI_MERGE);
                    ShowForm describeMerchBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.DescribeMerchandiseBlock,
                            parentForm,
                            descMerchFrm,
                            descMerchFrm.NavControlBox,
                            fxn, true);
                    return (describeMerchBlk);

                }

                /// <summary>
                /// Create and return a showform block for Describe merchandise form
                /// in PFI_REplace context
                /// </summary>
                /// <param name="parentForm"></param>
                /// <param name="fxn"></param>
                /// <returns></returns>
                public ShowForm DescribeMerchandisePFIReplaceBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    DescribeMerchandise descMerchFrm = new DescribeMerchandise(GlobalDataAccessor.Instance.DesktopSession, CurrentContext.PFI_REPLACE);
                    ShowForm describeMerchBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.DescribeMerchandiseBlock,
                            parentForm,
                            descMerchFrm,
                            descMerchFrm.NavControlBox,
                            fxn, true);
                    return (describeMerchBlk);

                }

                /// <summary>
                /// Create and return a showform block for Describe merchandise form
                /// in PFI_ADD context
                /// </summary>
                /// <param name="parentForm"></param>
                /// <param name="fxn"></param>
                /// <returns></returns>
                public ShowForm DescribeMerchandisePFIAddBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    DescribeMerchandise descMerchFrm = new DescribeMerchandise(GlobalDataAccessor.Instance.DesktopSession, CurrentContext.PFI_ADD);
                    ShowForm describeMerchBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.DescribeMerchandiseBlock,
                            parentForm,
                            descMerchFrm,
                            descMerchFrm.NavControlBox,
                            fxn, true);
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

                public ShowForm DescribeItemChangeRetailPriceBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn, ProKnowMatch proKnowMatch)
                {
                    DescribeItem descItemFrm = new DescribeItem(GlobalDataAccessor.Instance.DesktopSession, GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext,
                        GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex)
                        {
                            SelectedProKnowMatch = proKnowMatch
                        };
                    ShowForm describeItemBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.DescribeItemBlock,
                            parentForm,
                            descItemFrm,
                            descItemFrm.NavControlBox,
                            fxn);
                    return (describeItemBlk);

                }

                public ShowForm DescribeItemPFIBlock(
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
                            fxn, true);
                    return (describeItemBlk);

                }


                public ShowForm ManageMultiplePawnItemsBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    ManageMultiplePawnItems mmpiForm = new ManageMultiplePawnItems();
                    ShowForm mmpiBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.ManageMultiplePawnItemsBlock,
                            parentForm,
                            mmpiForm,
                            mmpiForm.NavControlBox,
                            fxn);
                    return (mmpiBlk);

                }

                /// <summary>
                /// Create and return a ShowForm block for create customer
                /// </summary>
                /// <param name="parentForm"></param>
                /// <param name="fxn"></param>
                /// <returns></returns>
                public ShowForm CreateCreateCustomerBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    CreateCustomer createCustomerFm = new CreateCustomer();
                    ShowForm createCustBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.CreateCustBlock,
                            parentForm,
                            createCustomerFm,
                            createCustomerFm.NavControlBox,
                            fxn);
                    return (createCustBlk);
                }

                /// <summary>
                /// Create and return a ShowForm block for create vendor
                /// </summary>
                /// <param name="parentForm"></param>
                /// <param name="fxn"></param>
                /// <returns></returns>
                public ShowForm CreateCreateVendorBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    CreateVendor createVendorFm = new CreateVendor();
                    ShowForm createVendBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.CreateVendBlock,
                            parentForm,
                            createVendorFm,
                            createVendorFm.NavControlBox,
                            fxn);
                    return (createVendBlk);
                }

                /// <summary>
                /// Create and return a ShowForm block for shop cash management
                /// </summary>
                /// <param name="parentForm"></param>
                /// <param name="fxn"></param>
                /// <returns></returns>
                public ShowForm CreateShopCashManagementForm(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    ShopCashMgr shopCashMgrFm = new ShopCashMgr();
                    //ShopAdminConsole shopCashMgrFm = new ShopAdminConsole();
                    ShowForm shopCashMgmtBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.ShopCashMgmtBlock,
                            parentForm,
                            shopCashMgrFm,
                            shopCashMgrFm.NavControlBox,
                            fxn);
                    return (shopCashMgmtBlk);
                }

                public ShowForm CreateGunBookPrintUtilityBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                }


                /// <summary>
                /// 
                /// </summary>
                /// <param name="parentForm"></param>
                /// <param name="fxn"></param>
                /// <returns></returns>
                public ShowForm CreateExistingCustomerBlock(
                    Form parentForm,
                    NavBox.NavBoxActionFired fxn)
                {
                    ExistingCustomer existCustFm = new ExistingCustomer();
                    ShowForm existCustBlk =
                        this.createShowFormBlock(
                            (uint)ValidFormBlockTypes.ExistingCustBlock,
                            parentForm,
                            existCustFm,
                            existCustFm.NavControlBox,
                            fxn);
                    return (existCustBlk);
                } 
        
                        /// <summary>
                        /// 
                        /// </summary>
                        /// <param name="parentForm"></param>
                        /// <param name="fxn"></param>
                        /// <returns></returns>
                        public ShowForm CreateProductServicesBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            Controller_ProductServices productServicesFm = new Controller_ProductServices();
                            ShowForm productServicesBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.ProductServicesBlock,
                                    parentForm,
                                    productServicesFm,
                                    productServicesFm.NavControlBox,
                                    fxn);
                            return (productServicesBlk);
                        }

                        /// <summary>
                        /// 
                        /// </summary>
                        /// <param name="parentForm"></param>
                        /// <param name="fxn"></param>
                        /// <returns></returns>
                        public ShowForm CreateStatsBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            Controller_Stats productStatsFm = new Controller_Stats();
                            ShowForm productStatsBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.ProductStatsBlock,
                                    parentForm,
                                    productStatsFm,
                                    productStatsFm.NavControlBox,
                                    fxn);
                            return (productStatsBlk);
                        }

                        /// <summary>
                        /// 
                        /// </summary>
                        /// <param name="parentForm"></param>
                        /// <param name="fxn"></param>
                        /// <returns></returns>
                        public ShowForm CreateLookupReceiptBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            LookupReceipt lookupReceiptFm = new LookupReceipt();
                            ShowForm lookupReceiptBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.LookupReceiptBlock,
                                    parentForm,
                                    lookupReceiptFm,
                                    lookupReceiptFm.NavControlBox,
                                    fxn);
                            return (lookupReceiptBlk);
                        }

                        /// <summary>
                        /// 
                        /// </summary>
                        /// <param name="parentForm"></param>
                        /// <param name="fxn"></param>
                        /// <returns></returns>
                        public ShowForm CreateViewReceiptBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            ViewReceipt viewReceiptFm = new ViewReceipt();
                            ShowForm viewReceiptBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.ViewReceipt,
                                    parentForm,
                                    viewReceiptFm,
                                    viewReceiptFm.NavControlBox,
                                    fxn);
                            return (viewReceiptBlk);
                        }

                        /// <summary>
                        /// Create a block to show CustomerHoldList
                        /// </summary>
                        /// <returns></returns>
                        public ShowForm CreateCustomerHoldListShowBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            CustomerHoldsList customerHoldFm = new CustomerHoldsList();
                            ShowForm custHoldListBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.CustHoldListBlock,
                                    parentForm,
                                    customerHoldFm,
                                    customerHoldFm.NavControlBox,
                                    fxn);
                            return (custHoldListBlk);
                        }

                        /// <summary>
                        /// Create a block to show CustomerHoldReleaseList
                        /// </summary>
                        /// <returns></returns>
                        public ShowForm CreateCustomerHoldReleaseListShowBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            CustomerHoldReleaseList customerHoldRelFm = new CustomerHoldReleaseList();
                            ShowForm custHoldReleaseListBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.CustHoldReleaseListBlock,
                                    parentForm,
                                    customerHoldRelFm,
                                    customerHoldRelFm.NavControlBox,
                                    fxn);
                            return (custHoldReleaseListBlk);
                        }

                        /// <summary>
                        /// Create a block to show CustomerHoldReleaseInfo
                        /// </summary>
                        /// <returns></returns>
                        public ShowForm CreateCustomerHoldReleaseInfoShowBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            CustomerHoldReleaseInfo customerHoldRelInfoFm = new CustomerHoldReleaseInfo();
                            ShowForm custHoldReleaseInfoBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.CustHoldReleaseInfoBlock,
                                    parentForm,
                                    customerHoldRelInfoFm,
                                    customerHoldRelInfoFm.NavControlBox,
                                    fxn);
                            return (custHoldReleaseInfoBlk);
                        }



                        /// <summary>
                        /// Create a block to show CustomerHoldInfo
                        /// </summary>
                        /// <returns></returns>
                        public ShowForm CreateCustomerHoldInfoShowBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            CustomerHoldInfo customerHoldInfoFm = new CustomerHoldInfo();
                            ShowForm custHoldInfoBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.CustHoldInfoBlock,
                                    parentForm,
                                    customerHoldInfoFm,
                                    customerHoldInfoFm.NavControlBox,
                                    fxn);
                            return (custHoldInfoBlk);
                        }

                        /// <summary>
                        /// Create a block to show PoliceHoldList Form
                        /// </summary>
                        /// <returns></returns>
                        public ShowForm CreatePoliceHoldListShowBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            PoliceHoldsList policeHoldListFm = new PoliceHoldsList();
                            ShowForm policeHoldListBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.PoliceHoldListBlock,
                                    parentForm,
                                    policeHoldListFm,
                                    policeHoldListFm.NavControlBox,
                                    fxn);
                            return (policeHoldListBlk);
                        }

                        /// <summary>
                        /// Create a block to show PoliceHoldList Form
                        /// </summary>
                        /// <returns></returns>
                        public ShowForm CreatePoliceInfoShowBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            PoliceHoldInfo policeHoldInfoFm = new PoliceHoldInfo();
                            ShowForm policeHoldInfoBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.PoliceHoldInfoBlock,
                                    parentForm,
                                    policeHoldInfoFm,
                                    policeHoldInfoFm.NavControlBox,
                                    fxn);
                            return (policeHoldInfoBlk);
                        }

                        /// <summary>
                        /// Create a block to show PoliceHoldReleaseList Form
                        /// </summary>
                        /// <returns></returns>
                        public ShowForm CreatePoliceHoldReleaseListShowBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            PoliceHoldReleaseList policeHoldReleaseListFm = new PoliceHoldReleaseList();
                            ShowForm policeHoldReleaseListBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.PoliceHoldReleaseListBlock,
                                    parentForm,
                                    policeHoldReleaseListFm,
                                    policeHoldReleaseListFm.NavControlBox,
                                    fxn);
                            return (policeHoldReleaseListBlk);
                        }


                        /// <summary>
                        /// Create a block to show PoliceHoldReleaseInfo Form
                        /// </summary>
                        /// <returns></returns>
                        public ShowForm CreatePoliceHoldReleaseInfoShowBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            PoliceHoldRelease policeHoldReleaseInfoFm = new PoliceHoldRelease();
                            ShowForm policeHoldReleaseInfoBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.PoliceHoldReleaseListBlock,
                                    parentForm,
                                    policeHoldReleaseInfoFm,
                                    policeHoldReleaseInfoFm.NavControlBox,
                                    fxn);
                            return (policeHoldReleaseInfoBlk);
                        }

                        /// <summary>
                        /// Create a block to show Item History Form
                        /// </summary>
                        /// <returns></returns>
                        public ShowForm CreateItemHistoryShowBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            Controller_ItemHistory itemHistoryFrm = new Controller_ItemHistory();
                            ShowForm itemHistBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.ItemHistoryBlock,
                                    parentForm,
                                    itemHistoryFrm,
                                    itemHistoryFrm.NavControlBox,
                                    fxn);
                            return (itemHistBlk);
                        }

                        /// <summary>
                        /// Create a block to show Product History Form
                        /// </summary>
                        /// <returns></returns>
                        public ShowForm CreateProductHistoryShowBlock(
                            Form parentForm,
                            NavBox.NavBoxActionFired fxn)
                        {
                            Controller_ProductHistory prodHistoryFrm = new Controller_ProductHistory();
                            ShowForm prodHistBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.ProdHistoryBlock,
                                    parentForm,
                                    prodHistoryFrm,
                                    prodHistoryFrm.NavControlBox,
                                    fxn);
                            return (prodHistBlk);
                        }


                        public ShowForm PFIVerifyBlock(
                             Form parentForm,
                             NavBox.NavBoxActionFired fxn)
                        {
                            PFI_Verify pfiVerifyFrm = new PFI_Verify();
                            ShowForm pfiverifyBlk =
                                this.createShowFormBlock(
                                    (uint)ValidFormBlockTypes.PFIVerifyBlock,
                                    parentForm,
                                    pfiVerifyFrm,
                                    pfiVerifyFrm.NavControlBox,
                                    fxn);
                            return (pfiverifyBlk);

                        }
        
                /// <summary>
                /// Create a block to show Add Cash Drawer Form
                /// </summary>
                /// <returns></returns>
                       public ShowForm CreateAddCashDrawerShowBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           AddCashDrawer addcashdrawerFrm = new AddCashDrawer();
                           ShowForm addcashdrawerBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.AddCashDrawerBlock,
                                   parentForm,
                                   addcashdrawerFrm,
                                   addcashdrawerFrm.NavControlBox,
                                   fxn);
                           return (addcashdrawerBlk);
                       }

                       /// <summary>
                       /// Create a block to show View cash drawer assignments
                       /// </summary>
                       /// <returns></returns>
                       public ShowForm CreateViewCashDrawerAssignmentsBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           ViewCashDrawerAssignments viewCashDrawerFrm = new ViewCashDrawerAssignments();
                           ShowForm viewCashDrawerBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.ViewCashDrawerAssignmentBlock,
                                   parentForm,
                                   viewCashDrawerFrm,
                                   viewCashDrawerFrm.NavControlBox,
                                   fxn);
                           return (viewCashDrawerBlk);
                       }

                       /// <summary>
                       /// Create a block to show Select Employee Form
                       /// </summary>
                       /// <returns></returns>
                       public ShowForm SelectEmployeeFormBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           SelectEmployee selEmployeeFrm = new SelectEmployee();
                           ShowForm selEmployeeBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.SelectEmployeeBlock,
                                   parentForm,
                                   selEmployeeFrm,
                                   selEmployeeFrm.NavControlBox,
                                   fxn);
                           return (selEmployeeBlk);
                       }

                       /// <summary>
                       /// Create a block to show Security Profile Form
                       /// </summary>
                       /// <returns></returns>
                       public ShowForm SecurityProfileFormBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           SecurityProfile secProfileFrm = new SecurityProfile();
                           ShowForm secProfileBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.SecurityProfileBlock,
                                   parentForm,
                                   secProfileFrm,
                                   secProfileFrm.NavControlBox,
                                   fxn);
                           return (secProfileBlk);
                       }

                       /// <summary>
                       /// Create a block to show Add Employee Form
                       /// </summary>
                       /// <returns></returns>
                       public ShowForm AddEmployeeFormBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           EmployeeAdd addEmployeeFrm = new EmployeeAdd();
                           ShowForm addEmployeeBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.AddEmployeeBlock,
                                   parentForm,
                                   addEmployeeFrm,
                                   addEmployeeFrm.NavControlBox,
                                   fxn);
                           return (addEmployeeBlk);
                       }

                       /// <summary>
                       /// Create a block to show Buy Return form
                       /// </summary>
                       /// <returns></returns>
                       public ShowForm BuyReturnFormBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           BuyReturn buyReturnFrm = new BuyReturn();
                           ShowForm buyReturnBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.BuyReturnBlock,
                                   parentForm,
                                   buyReturnFrm,
                                   buyReturnFrm.NavControlBox,
                                   fxn);
                           return (buyReturnBlk);
                       }


                       /// <summary>
                       /// Create a block to show Buy Return Items form
                       /// </summary>
                       /// <returns></returns>
                       public ShowForm BuyReturnItemsFormBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           BuyReturnItems buyReturnItmFrm = new BuyReturnItems();
                           ShowForm buyReturnItemBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.BuyReturnItemsBlock,
                                   parentForm,
                                   buyReturnItmFrm,
                                   buyReturnItmFrm.NavControlBox,
                                   fxn);
                           return (buyReturnItemBlk);
                       }
        

                       /// <summary>
                       /// Create a block to show receipt search form
                       /// </summary>
                       /// <returns></returns>
                       public ShowForm ReceiptSearchFormBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           ReceiptSearch rcptSearchFrm = new ReceiptSearch();
                           ShowForm rcptSearchBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.ReceiptSearchBlock,
                                   parentForm,
                                   rcptSearchFrm,
                                   rcptSearchFrm.NavControlBox,
                                   fxn);
                           return (rcptSearchBlk);
                       }

                       /// <summary>
                       /// Create a block to show refund quantity form
                       /// </summary>
                       /// <returns></returns>
                       public ShowForm RefundQuantityFormBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           RefundQuantity refQtyFrm = new RefundQuantity();
                           ShowForm rcptSearchBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.RefundQtyBlock,
                                   parentForm,
                                   refQtyFrm,
                                   refQtyFrm.NavControlBox,
                                   fxn);
                           return (rcptSearchBlk);
                       }


                       public ShowForm TenderOutShowBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           TenderOut tenderFrm = new TenderOut();

                           ShowForm tenderInfoBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.TenderOutBlock,
                                   parentForm,
                                   tenderFrm,
                                   tenderFrm.NavControlBox,
                                   fxn);

                           return (tenderInfoBlk);
                       }

                       public ShowForm TenderOutExistingRefundShowBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           TenderOut tenderFrm = new TenderOut(GlobalDataAccessor.Instance.DesktopSession.RefundEntries,
                               GlobalDataAccessor.Instance.DesktopSession.PartialRefundEntries);

                           ShowForm tenderInfoBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.TenderOutBlock,
                                   parentForm,
                                   tenderFrm,
                                   tenderFrm.NavControlBox,
                                   fxn);

                           return (tenderInfoBlk);
                       }

                       /// <summary>
                       /// Create a block to show layaway search form
                       /// </summary>
                       /// <returns></returns>
                       public ShowForm LayawaySearchFormBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           LayawaySearch laySearchFrm = new LayawaySearch();
                           ShowForm laySearchBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.LayawaySearchBlock,
                                   parentForm,
                                   laySearchFrm,
                                   laySearchFrm.NavControlBox,
                                   fxn);
                           return (laySearchBlk);
                       }


                       /// <summary>
                       /// Create a block to show layaway search form
                       /// </summary>
                       /// <returns></returns>
                       public ShowForm LayawayRefundFormBlock(
                           Form parentForm,
                           NavBox.NavBoxActionFired fxn)
                       {
                           LayawayRefund layRefFrm = new LayawayRefund();
                           ShowForm layRefBlk =
                               this.createShowFormBlock(
                                   (uint)ValidFormBlockTypes.LayawayRefundBlock,
                                   parentForm,
                                   layRefFrm,
                                   layRefFrm.NavControlBox,
                                   fxn);
                           return (layRefBlk);
                       }

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
               */
        #endregion
        #region Class Block Methods
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
            this.flowTabControllerForm.setTabEnabledFlag(FlowTabController.State.Customer, true);
            this.flowTabControllerForm.setTabEnabledFlag(FlowTabController.State.ItemHistory, true);
            this.flowTabControllerForm.setTabEnabledFlag(FlowTabController.State.ProductHistory, true);
            this.flowTabControllerForm.setTabEnabledFlag(FlowTabController.State.ProductsAndServices, true);
            this.flowTabControllerForm.setTabEnabledFlag(FlowTabController.State.Stats, true);
            this.flowTabControllerForm.setTabEnabledFlag(FlowTabController.State.Comments, true);
            this.flowTabControllerForm.Show();
            this.flowTabControllerForm.BringToFront();
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
        /*__________________________________________________________________________________________*/
        private void flowTabChangedHandler()
        {
            //Determine the tab state, and ensure the trigger to the 
            //app controller is fired to communicate main tab change
            FlowTabController.State curState = this.flowTabControllerForm.TabState;
            Form frm = this.flowTabControllerForm.BelowForm;
            


                if (curState == FlowTabController.State.ProductsAndServices)
                {

                    if (frm.GetType() == typeof(ViewCustomerInformation))
                    {
                        ((ViewCustomerInformation)frm).NavControlBox.IsCustom = true;
                        ((ViewCustomerInformation)frm).NavControlBox.CustomDetail = "ProductsAndServices";
                        ((ViewCustomerInformation)frm).NavControlBox.Action = NavBox.NavAction.SUBMIT; //BACKANDSUBMIT;
                    }
                }
                else if (curState == FlowTabController.State.Comments)
                {
                    if (frm.GetType() == typeof(ViewCustomerInformation))
                    {
                        ((ViewCustomerInformation)frm).NavControlBox.IsCustom = true;
                        ((ViewCustomerInformation)frm).NavControlBox.CustomDetail = "SupportCustomerComment";
                        ((ViewCustomerInformation)frm).NavControlBox.Action = NavBox.NavAction.SUBMIT; //BACKANDSUBMIT;

                    }
                    else if (frm.GetType() == typeof(Controller_ProductServices))
                    {
                        ((Controller_ProductServices)frm).NavControlBox.IsCustom = true;
                        ((Controller_ProductServices)frm).NavControlBox.CustomDetail = "SupportCustomerComment";
                        ((Controller_ProductServices)frm).NavControlBox.Action = NavBox.NavAction.BACK; // SUBMIT; //BACKANDSUBMIT;

                    }
                }
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

        #endregion

    }
}
