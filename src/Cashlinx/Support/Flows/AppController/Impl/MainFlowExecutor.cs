using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl;
using Support.Flows.AppController.Impl.MainSubFlows;
using Support.Logic;

//using Pawn.Logic;

namespace Support.Flows.AppController.Impl
{
    public class MainFlowExecutor : MainFlowExecutorBase
    {
        #region OBSOLETE
        /*
        public const string NEWPAWNLOAN = "newpawnloan";
        public const string SHOPCASHMGMT = "shopcashmanagement";
        public const string LOOKUPTICKET = "lookupticket";
        //public const string LOOKUPCUSTOMER = "lookupcustomer";
        public const string LOOKUPVENDOR = "lookupvendor";
        public const string LOOKUPRECEIPT = "lookupreceipt";
        public const string CUSTOMERHOLD = "customerhold";
        public const string CUSTOMERHOLDRELEASE = "customerholdrelease";
        public const string POLICEHOLD = "policehold";
        public const string POLICEHOLDRELEASE = "policeholdrelease";
        public const string SERVICEPAWNLOAN = "servicepawnloans";
        public const string MMPI = "mmpi";
        public const string PFIVERIFY = "pfiverify";
        public const string POLICESEIZE = "policeseize";
        public const string SECURITY = "security";
        public const string CUSTOMERPURCHASE = "customerpurchase";
        public const string VENDORPURCHASE = "vendorpurchase";
        public const string RETAIL = "sale";
        public const string CHANGERETAILPRICE = "changeretailprice";
        public const string TRANSFERIN = "transferin";
        public const string PURCHASERETURN = "purchasereturn";
        public const string RETAILRETURN = "retailreturn";
        public const string LAYAWAYRETURN = "layawayreturn";
        public const string GUNBOOKEDIT = "gunbookedit";
        */
        #endregion

        public const string PAWNCUSTINFO = "pawncustinformation";
        public const string LOOKUPCUSTOMER = "custmaint";
        public const string CUSTOMERPRODUCTS = "Controller_ProductServices";
        private LookupCustomerFlowExecutor lookupCustFlowExecutor;
        private PawnCustInformationFlowExecutor pawnCustInfoFlowExecutor;

        public MainFlowExecutor() : base(GlobalDataAccessor.Instance.DesktopSession)
        //public MainFlowExecutor(): base(CashlinxPawnSupportSession.Instance)
        {
            //this.newPawnLoanFlowExecutor = null;
            this.lookupCustFlowExecutor = null;
            this.setExecBlock(executorFxn);
            this.endStateNotifier = null;
        }
        #region OBSOLETE        
        //Madhu Feb 17th
        //private NewPawnLoanFlowExecutor newPawnLoanFlowExecutor;
        //private LookupTicketFlowExecutor lookupTktFlowExecutor;
        //private ShopCashManagementFlowExecutor shopCashManagementFlowExecutor;
        //private VendorPurchaseFlowExecutor lookupVendFlowExecutor;
        //private LookupReceiptFlowExecutor lookupReceiptFlowExecutor;
        //private CustomerHoldFlowExecutor customerHoldFlowExecutor;
        //private CustomerHoldReleaseFlowExecutor customerHoldReleaseFlowExecutor;
        //private PoliceHoldFlowExecutor policeHoldFlowExecutor;
        //private PoliceHoldReleaseFlowExecutor policeHoldReleaseFlowExecutor;
        //private MMPIFlowExecutor mmpiFlowExecutor;
        //private ServicePawnLoanFlowExecutor servicePawnLoanFlowExecutor;
        //private PFIVerifyFlowExecutor pfiverifyFlowExecutor;
        //private PoliceSeizeFlowExecutor policeSeizeFlowExecutor;
        //private SecurityFlowExecutor securityFlowExecutor;
        //private CustomerPurchaseFlowExecutor customerPurchaseFlowExecutor;
        //private SaleFlowExecutor saleFlowExecutor;
        //private ChangeRetailPriceFlowExecutor changeRetailPriceFlowExecutor;
        //private TransferInFlowExecutor transferInFlowExecutor;
        //private PurchaseReturnFlowExecutor purchaseReturnFlowExecutor;
        //private RetailReturnFlowExecutor retailReturnFlowExecutor;
        //private LayawayReturnFlowExecutor layawayReturnFlowExecutor;
        //private GunBookEditFlowExecutor gunBookFlowExecutor; 
        #endregion

        private FxnBlock endStateNotifier;
        /*__________________________________________________________________________________________*/
        public void setEndStateNotifier(FxnBlock fB)
        {
            this.endStateNotifier = fB;
        }
        /*__________________________________________________________________________________________*/
        protected override object executorFxn(object inputData)
        {
            if (inputData == null || (!(inputData is string)))
            {
                return (null);
            }

            string menuTrigger = (string)inputData;
            if (menuTrigger.Equals(PAWNCUSTINFO, StringComparison.OrdinalIgnoreCase))
            {
                this.pawnCustInfoFlowExecutor = new PawnCustInformationFlowExecutor(this.ParentForm, this.endStateNotifier, this.ParentFlowExecutor);
            }
            else if (menuTrigger.Equals(LOOKUPCUSTOMER, StringComparison.OrdinalIgnoreCase))
            {
                //this.lookupCustFlowExecutor = new LookupCustomerFlowExecutor(this.ParentForm, this.endStateNotifier);
                this.lookupCustFlowExecutor = new LookupCustomerFlowExecutor(this.ParentForm, base.EndStateNotifier);
            }
            else if (menuTrigger.Equals(CUSTOMERPRODUCTS,StringComparison.OrdinalIgnoreCase))
            {
                this.pawnCustInfoFlowExecutor = new PawnCustInformationFlowExecutor(this.ParentForm, this.endStateNotifier, this.ParentFlowExecutor);
            }
            return (null);
            #region OBSOLETE
            //Madhu Feb 17th
            /*if (menuTrigger.Equals(NEWPAWNLOAN, StringComparison.OrdinalIgnoreCase))
            {
                //Orchestrate the new pawn loan flow
                this.newPawnLoanFlowExecutor =
                new NewPawnLoanFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            elseif (menuTrigger.Equals(PAWNCUSTINFO, StringComparison.OrdinalIgnoreCase))
            {
                this.pawnCustInfoFlowExecutor = new PawnCustInformationFlowExecutor(this.ParentForm, this.endStateNotifier, this.ParentFlowExecutor);
            }
            else if (menuTrigger.Equals(SHOPCASHMGMT, StringComparison.OrdinalIgnoreCase))
            {
                //Orchestrate shop cash management flow
                this.shopCashManagementFlowExecutor = new ShopCashManagementFlowExecutor(
                    this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(LOOKUPTICKET, StringComparison.OrdinalIgnoreCase))
            {
                this.lookupTktFlowExecutor = new LookupTicketFlowExecutor(this.ParentForm,
                                                                          this.endStateNotifier);
            }
            //else 
            else if (menuTrigger.Equals(LOOKUPCUSTOMER, StringComparison.OrdinalIgnoreCase))
            {
                //this.lookupCustFlowExecutor = new LookupCustomerFlowExecutor(this.ParentForm, this.endStateNotifier);
                this.lookupCustFlowExecutor = new LookupCustomerFlowExecutor( this.ParentForm, base.EndStateNotifier );
            }
            //Madhu Feb 17th
                
            else if (menuTrigger.Equals(VENDORPURCHASE, StringComparison.OrdinalIgnoreCase))
            {
                this.lookupVendFlowExecutor = new VendorPurchaseFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(LOOKUPRECEIPT, StringComparison.OrdinalIgnoreCase))
            {
                this.lookupReceiptFlowExecutor = new LookupReceiptFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(CUSTOMERHOLD, StringComparison.OrdinalIgnoreCase))
            {
                this.customerHoldFlowExecutor = new CustomerHoldFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(CUSTOMERHOLDRELEASE, StringComparison.OrdinalIgnoreCase))
            {
                this.customerHoldReleaseFlowExecutor = new CustomerHoldReleaseFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(POLICEHOLD, StringComparison.OrdinalIgnoreCase))
            {
                this.policeHoldFlowExecutor = new PoliceHoldFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(POLICEHOLDRELEASE, StringComparison.OrdinalIgnoreCase))
            {
                this.policeHoldReleaseFlowExecutor = new PoliceHoldReleaseFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(MMPI, StringComparison.OrdinalIgnoreCase))
            {
                this.mmpiFlowExecutor = new MMPIFlowExecutor(this.ParentForm, this.endStateNotifier, this.ParentFlowExecutor);
            }
            else if (menuTrigger.Equals(SERVICEPAWNLOAN, StringComparison.OrdinalIgnoreCase))
            {
                this.servicePawnLoanFlowExecutor = new ServicePawnLoanFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(PFIVERIFY, StringComparison.OrdinalIgnoreCase))
            {
                this.pfiverifyFlowExecutor = new PFIVerifyFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(POLICESEIZE, StringComparison.OrdinalIgnoreCase))
            {
                this.policeSeizeFlowExecutor = new PoliceSeizeFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(SECURITY, StringComparison.OrdinalIgnoreCase))
            {
                this.securityFlowExecutor = new SecurityFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(CUSTOMERPURCHASE, StringComparison.OrdinalIgnoreCase))
            {
                this.customerPurchaseFlowExecutor = new CustomerPurchaseFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(RETAIL, StringComparison.OrdinalIgnoreCase))
                this.saleFlowExecutor = new SaleFlowExecutor(this.ParentForm, this.endStateNotifier);
            else if (menuTrigger.Equals(CHANGERETAILPRICE, StringComparison.OrdinalIgnoreCase))
            {
                this.changeRetailPriceFlowExecutor = new ChangeRetailPriceFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(TRANSFERIN, StringComparison.OrdinalIgnoreCase))
            {
                this.transferInFlowExecutor = new TransferInFlowExecutor(this.ParentForm, this.endStateNotifier);
            }
            else if (menuTrigger.Equals(PURCHASERETURN, StringComparison.OrdinalIgnoreCase))
                this.purchaseReturnFlowExecutor = new PurchaseReturnFlowExecutor(this.ParentForm, this.endStateNotifier);
            else if (menuTrigger.Equals(RETAILRETURN, StringComparison.OrdinalIgnoreCase))
                this.retailReturnFlowExecutor = new RetailReturnFlowExecutor(this.ParentForm, this.endStateNotifier);
            else if (menuTrigger.Equals(LAYAWAYRETURN, StringComparison.OrdinalIgnoreCase))
                this.layawayReturnFlowExecutor = new LayawayReturnFlowExecutor(this.ParentForm, this.endStateNotifier);
            else if (menuTrigger.Equals(GUNBOOKEDIT,StringComparison.OrdinalIgnoreCase))
                this.gunBookFlowExecutor = new GunBookEditFlowExecutor(this.ParentForm, this.endStateNotifier);
            
            return (null);
            */
            #endregion
        }
    }
}