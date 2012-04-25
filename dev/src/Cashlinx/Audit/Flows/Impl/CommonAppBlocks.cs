using System;
using System.Windows.Forms;
using Audit.Forms.Inventory;
using Audit.Logic;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms.Pawn.Products.DescribeMerchandise;
using Common.Libraries.Utility.Shared;

namespace Audit.Flows.Impl
{
    public class CommonAppBlocks : MarshalByRefObject
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

        public override object InitializeLifetimeService()
        {
            return null;
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

        public enum ValidFormBlockTypes
        {
            None,
        }

        public ShowForm CreateSelectAuditShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            var selectAudit = new SelectAudit();
            var selectAuditBlk =
                this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    selectAudit,
                    selectAudit.NavControlBox,
                    fxn);
            return (selectAuditBlk);
        }

        public ShowForm CreateSelectStoreShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            var selectStore = new SelectStore();
            var selectStoreBlk =
                this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    selectStore,
                    selectStore.NavControlBox,
                    fxn);
            return (selectStoreBlk);
        }

        public ShowForm CreateInitiateAuditShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            var initiateAudit = new InitiateAudit();
            var initiateAuditBlk =
                this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    initiateAudit,
                    initiateAudit.NavControlBox,
                    fxn);
            return (initiateAuditBlk);
        }

        public ShowForm CreateAuditManagerShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            var auditManager = new AuditManager();
            var auditManagerBlk =
                this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    auditManager,
                    auditManager.NavControlBox,
                    fxn);
            return (auditManagerBlk);
        }

        public ShowForm CreateInventorySummaryShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            var inventorySummary = new InventorySummary();
            return this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    inventorySummary,
                    inventorySummary.NavControlBox,
                    fxn);
        }

        public ShowForm CreateInventoryQuestionsShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            var inventoryQuestions = new InventoryQuestions();
            return this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    inventoryQuestions,
                    inventoryQuestions.NavControlBox,
                    fxn);
        }

        public ShowForm CreateDownloadToTrakkerShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            var downloadToTrakker = new DownloadToTrakker();
            var downloadToTrakkerBlk =
                this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    downloadToTrakker,
                    downloadToTrakker.NavControlBox,
                    fxn);
            return (downloadToTrakkerBlk);
        }

        public ShowForm CreateUploadFromTrakkerShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            var uploadFromTrakker = new UploadFromTrakker();
            return this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    uploadFromTrakker,
                    uploadFromTrakker.NavControlBox,
                    fxn);
        }

        public ShowForm CreateProcessMissingItemsShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            var processMissingItems = new ProcessMissingItems();
            return this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    processMissingItems,
                    processMissingItems.NavControlBox,
                    fxn);
        }

        public ShowForm CreateProcessUnexpectedItemsShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            var processUnexpectedItems = new ProcessUnexpectedItems();
            return this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    processUnexpectedItems,
                    processUnexpectedItems.NavControlBox,
                    fxn);
        }

        public ShowForm CreateEnterCaccItemsShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            var enterCaccItems = new EnterCaccItems();
            return this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    enterCaccItems,
                    enterCaccItems.NavControlBox,
                    fxn);
        }

        public ShowForm CreateClosedAuditShowBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn)
        {
            var auditResults = new ClosedAudit();
            return this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    auditResults,
                    auditResults.NavControlBox,
                    fxn);
        }

        public ShowForm DescribeMerchChargeOnBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn, DesktopSession desktopSession)
        {
            var descMerchFrm = new DescribeMerchandise(desktopSession, CurrentContext.AUDITCHARGEON);
            return this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    descMerchFrm,
                    descMerchFrm.NavControlBox,
                    fxn, true);
        }

        public ShowForm DescribeItemBlock(
            Form parentForm,
            NavBox.NavBoxActionFired fxn,
            DesktopSession desktopSession)
        {
            var descItemFrm = new DescribeItem(desktopSession, AuditDesktopSession.Instance.DescribeItemContext,
                AuditDesktopSession.Instance.DescribeItemPawnItemIndex);
            ShowForm describeItemBlk =
                this.createShowFormBlock(
                    ValidFormBlockTypes.None,
                    parentForm,
                    descItemFrm,
                    descItemFrm.NavControlBox,
                    fxn);
            return (describeItemBlk);

        }


        private ShowForm createShowFormBlock(
            ValidFormBlockTypes nm,
            Form pFm,
            Form fm)
        {
            if (fm == null)
            {
                return (null);
            }

            var sFm = new ShowForm(AuditDesktopSession.Instance, pFm, fm);
            return (sFm);
        }

        private ShowForm createShowFormBlock(
            ValidFormBlockTypes nm,
            Form pFm,
            Form fm,
            NavBox nvBox)
        {
            if (fm == null)
            {
                return (null);
            }

            var sFm = new ShowForm(AuditDesktopSession.Instance, pFm, fm, nvBox);
            return (sFm);
        }

        private ShowForm createShowFormBlock(
            ValidFormBlockTypes nm,
            Form pFm,
            Form fm,
            NavBox nvBox,
            NavBox.NavBoxActionFired nvBoxDeleg)
        {
            if (fm == null)
            {
                return (null);
            }

            var sFm = new ShowForm(AuditDesktopSession.Instance, pFm, fm, nvBox, nvBoxDeleg);
            return (sFm);
        }

        private ShowForm createShowFormBlock(
             ValidFormBlockTypes nm,
             Form pFm,
             Form fm,
             NavBox nvBox,
             NavBox.NavBoxActionFired nvBoxDeleg,
            bool showFormAsDialog)
        {
            if (fm == null)
            {
                return (null);
            }

            var sFm = new ShowForm(AuditDesktopSession.Instance, pFm, fm, nvBox, nvBoxDeleg, showFormAsDialog);
            return (sFm);
        }
    }
}
