//  no ticket SMurphy 4/28/2010 commented unused reports pieces

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Forms.Pawn.Loan;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Pawn.Flows.AppController.Impl;
using Pawn.Forms.Admin;
using Pawn.Forms.GunUtilities.GunBook;
using Pawn.Forms.Layaway;
using Pawn.Forms.Pawn.Products;
using Pawn.Forms.Pawn.Products.ManageMultiplePawnItems;
using Pawn.Forms.Pawn.Services.ChargeOff;
using Pawn.Forms.Pawn.Services.MerchandiseTransfer;
using Pawn.Forms.Pawn.Services.PFI;
using Pawn.Forms.Pawn.Services.Void;
using Pawn.Forms.Pawn.ShopAdministration;
using Pawn.Forms.Pawn.ShopAdministration.ShopCash;
using Pawn.Forms.ReleaseFingerprints;
using Pawn.Forms.Report;
using Pawn.Forms.Retail;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;

//using PawnReports.Reports.GunDisposition;

namespace Pawn.Forms
{
    public sealed partial class NewDesktop : Form, IDisposable
    {
        public DesktopSession cdSession { get; set; } // Requires public access for Describe Merchandise upcalls for Global PawnItem
        private bool resetFlag;
        private object lastActiveMenuPanel;
        private FxnBlock endStateNotifier;
        private const string OK = "OK";
        private USBUtilities.DriveDetector detector;

        /// <summary>
        /// This function will reset the menu back to its initial state
        /// </summary>
        private void resetMenu()
        {
            if (this.resetFlag)
                return;
            this.resetFlag = true;
            //Will allow user to reset the menu back to the parent
            if (this.mainMenuPanel.Enabled == false)
            {
                if (this.lookupMenuPanel.Enabled)
                {
                    this.lookupMenuPanel.Enabled = false;
                    this.lookupMenuPanel.Visible = false;
                    this.lookupMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.lookupMenuPanel.SendToBack();
                }
                if (this.pawnMenuPanel.Enabled)
                {
                    this.pawnMenuPanel.Enabled = false;
                    this.pawnMenuPanel.Visible = false;
                    this.pawnMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.pawnMenuPanel.SendToBack();
                }
                if (this.utilitiesMenuPanel.Enabled)
                {
                    this.utilitiesMenuPanel.Enabled = false;
                    this.utilitiesMenuPanel.Visible = false;
                    this.utilitiesMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.utilitiesMenuPanel.SendToBack();
                }

                if (this.pfiMenuPanel.Enabled)
                {
                    this.pfiMenuPanel.Enabled = false;
                    this.pfiMenuPanel.Visible = false;
                    this.pfiMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.pfiMenuPanel.SendToBack();
                }

                if (this.customerHoldsMenuPanel.Enabled)
                {
                    this.customerHoldsMenuPanel.Enabled = false;
                    this.customerHoldsMenuPanel.Visible = false;
                    this.customerHoldsMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.customerHoldsMenuPanel.SendToBack();
                }

                if (this.policeMenuPanel.Enabled)
                {
                    this.policeMenuPanel.Enabled = false;
                    this.policeMenuPanel.Visible = false;
                    this.policeMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.policeMenuPanel.SendToBack();
                }

                if (this.reportsMenuPanel.Enabled)
                {
                    this.reportsMenuPanel.Enabled = false;
                    this.reportsMenuPanel.Visible = false;
                    this.reportsMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.reportsMenuPanel.SendToBack();
                }

                if (this.manageCashMenuPanel.Enabled)
                {
                    this.manageCashMenuPanel.Enabled = false;
                    this.manageCashMenuPanel.Visible = false;
                    this.manageCashMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.manageCashMenuPanel.SendToBack();
                }

                if (this.transferMenuPanel.Enabled)
                {
                    this.transferMenuPanel.Enabled = false;
                    this.transferMenuPanel.Visible = false;
                    this.transferMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.transferMenuPanel.SendToBack();
                }

                if (this.buyMenuPanel.Enabled)
                {
                    this.buyMenuPanel.Enabled = false;
                    this.buyMenuPanel.Visible = false;
                    this.buyMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.buyMenuPanel.SendToBack();
                }

                if (this.customerBuyMenuPanel.Enabled)
                {
                    this.customerBuyMenuPanel.Enabled = false;
                    this.customerBuyMenuPanel.Visible = false;
                    this.customerBuyMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.customerBuyMenuPanel.SendToBack();
                }

                if (this.refundReturnMenuPanel.Enabled)
                {
                    this.refundReturnMenuPanel.Enabled = false;
                    this.refundReturnMenuPanel.Visible = false;
                    this.refundReturnMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.refundReturnMenuPanel.SendToBack();
                }

                if (this.voidMenuPanel.Enabled)
                {
                    this.voidMenuPanel.Enabled = false;
                    this.voidMenuPanel.Visible = false;
                    this.voidMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.voidMenuPanel.SendToBack();
                }

                if (this.manageInventoryMenuPanel.Enabled)
                {
                    this.manageInventoryMenuPanel.Enabled = false;
                    this.manageInventoryMenuPanel.Visible = false;
                    this.manageInventoryMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.manageInventoryMenuPanel.SendToBack();
                }

                if (this.changePricingMenuPanel.Enabled)
                {
                    this.changePricingMenuPanel.Enabled = false;
                    this.changePricingMenuPanel.Visible = false;
                    this.changePricingMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.changePricingMenuPanel.SendToBack();
                }

                if (this.gunBookMenuPanel.Enabled)
                {
                    this.gunBookMenuPanel.Enabled = false;
                    this.gunBookMenuPanel.Visible = false;
                    this.gunBookMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.gunBookMenuPanel.SendToBack();
                }

                if (this.safeOperationsMenuPanel.Enabled)
                {
                    this.safeOperationsMenuPanel.Enabled = false;
                    this.safeOperationsMenuPanel.Visible = false;
                    this.safeOperationsMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.safeOperationsMenuPanel.SendToBack();
                }

                if (this.cashDrawerMenuPanel.Enabled)
                {
                    this.cashDrawerMenuPanel.Enabled = false;
                    this.cashDrawerMenuPanel.Visible = false;
                    this.cashDrawerMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.cashDrawerMenuPanel.SendToBack();
                }

                //Remove the connected cd user entry
                string errorCode;
                string errorMesg;
                ShopCashProcedures.DeleteConnectedCdUser(GlobalDataAccessor.Instance.DesktopSession.CashDrawerId,
                    SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId,
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, GlobalDataAccessor.Instance.DesktopSession,
                    out errorCode,
                    out errorMesg);
                GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile = new UserVO();
                GlobalDataAccessor.Instance.DesktopSession.ClearLoggedInUser();
                GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();

                this.mainMenuPanel.Enabled = true;
                this.mainMenuPanel.Visible = true;
                this.mainMenuPanel.ButtonControllers.resetGroupInitialState();
                this.mainMenuPanel.BringToFront();
                this.mainMenuPanel.Update();
                this.mainMenuPanel.setExitButtonHandler(this.exitButton_Click);
            }
            this.resetFlag = false;
        }

        /// <summary>
        /// Based on the button clicked and the current panel shown, 
        /// invoke the proper functionality or show the proper sub panel
        /// </summary>
        /// <param name="menuCtrl"></param>
        /// <returns></returns>
        private bool triggerNextEvent(MenuLevelController menuCtrl)
        {
            bool rt = false;
            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            if (menuCtrl != null)
            {
                Button triggerBtn = menuCtrl.TriggerButton;
                if (triggerBtn != null)
                {
                    if (triggerBtn != menuCtrl.BackButton)
                    {
                        string targetPanelName = menuCtrl[triggerBtn];


                        if (!string.IsNullOrEmpty(targetPanelName) &&
                            !targetPanelName.Equals("null"))
                        {
                            UserVO currUser = dSession.LoggedInUserSecurityProfile;
                            if (currUser == null || (string.IsNullOrEmpty(currUser.UserName)))
                            {
                                dSession.ClearLoggedInUser();
                                dSession.PerformAuthorization();
                            }
                            if (!string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName))
                                rt = this.initializeProperMenu(targetPanelName);
                            else
                            {
                                this.resetMenu();
                                return (true);
                            }
                        }
                        else
                        {
                            UserVO currUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;
                            if (currUser == null || (string.IsNullOrEmpty(currUser.UserName)))
                            {
                                GlobalDataAccessor.Instance.DesktopSession.ClearLoggedInUser();
                                GlobalDataAccessor.Instance.DesktopSession.PerformAuthorization();
                            }
                            if (!string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName))
                            {

                                rt = this.initializeFunctionality(
                                    StringUtilities.removeFromString(triggerBtn.Name.ToLowerInvariant(), "button"));


                            }
                            else
                            {
                                this.resetMenu();
                                return (true);
                            }
                        }
                    }
                    else
                    {
                        //We have the back button - reset the menu
                        this.resetMenu();
                        return (true);
                    }
                }
            }
            return (rt);
        }

        /// <summary>
        /// Convenience method to show a menu panel
        /// </summary>
        /// <param name="targetPanelName"></param>
        /// <returns></returns>
        private bool initializeProperMenu(string targetPanelName)
        {
            bool rt = false;
            if (!string.IsNullOrEmpty(targetPanelName))
            {
                Control c = this.Controls[targetPanelName];
                if (c != null)
                {
                    c.Enabled = true;
                    c.Visible = true;
                    c.BringToFront();
                    c.Update();
                    this.lastActiveMenuPanel = c;
                    rt = true;
                }
            }

            if (rt == false)
            {
                MessageBox.Show("Could not navigate to child menu { " + targetPanelName + " }",
                    "MenuNavigationError", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        public object handleEndFlow(object noParam)
        {
            try
            {
                this.resetMenu();
            }
            //Catch all
            catch
            {
                //Ensure that the main menu is visible if the reset menu fails
                this.mainMenuPanel.Enabled = true;
                this.mainMenuPanel.Visible = true;
                this.mainMenuPanel.ButtonControllers.resetGroupInitialState();
                this.mainMenuPanel.BringToFront();
                this.mainMenuPanel.Update();
                this.mainMenuPanel.setExitButtonHandler(this.exitButton_Click);
                this.resetFlag = false;
            }

            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
            GlobalDataAccessor.Instance.DesktopSession.ApplicationExit = true;
            GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
            GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria =
                new LookupCustomerSearchData();

            return (null);
        }

        //<summary>
        //   determines if user has proper "resource" privledges to perfom the action they are looking to void
        //</summary>
        private bool canVoid (VoidSelector.VoidTransactionType voidType)
        {
            var retval = false;
            var overrideType = new List<ManagerOverrideType>(1);
            var overrideTransaction = new List<ManagerOverrideTransactionType>(1);

            var mask = ResourceSecurityMask.NONE;


            bool hasResource = false;


            switch (voidType)
            {
                case VoidSelector.VoidTransactionType.VOIDCASHTRANSSHOPTOBANK:
                case VoidSelector.VoidTransactionType.VOIDCASHTRANSBANKTOSHOP:
                case VoidSelector.VoidTransactionType.VOIDCASHTRANSSHOPTOSHOP:
                    hasResource = Common.Controllers.Database.Procedures.SecurityProfileProcedures.GetUserResourceAccess("SAFEMANAGEMENT", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance, out mask);
                    //if (hasResource && mask != ResourceSecurityMask.NONE)                        
                    //    hasResource = Common.Controllers.Database.Procedures.SecurityProfileProcedures.GetUserResourceAccess("CASH TRANSFER", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance, out mask);
                    break;

                case VoidSelector.VoidTransactionType.VOIDBUY:
                    hasResource = Common.Controllers.Database.Procedures.SecurityProfileProcedures.GetUserResourceAccess("CUSTOMERBUY", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance, out mask);
                    if (!hasResource)
                        hasResource = Common.Controllers.Database.Procedures.SecurityProfileProcedures.GetUserResourceAccess("VENDORBUY", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance, out mask);
                    break;

                case VoidSelector.VoidTransactionType.VOIDMDSETRANSOUT:
                case VoidSelector.VoidTransactionType.VOIDLAYAWAY:
                case VoidSelector.VoidTransactionType.VOIDMDSETRANSIN:
                case VoidSelector.VoidTransactionType.VOIDRETAILSALE:
                    hasResource = true;
                    mask = ResourceSecurityMask.VIEW;
                    break;
                    
                case VoidSelector.VoidTransactionType.VOIDPAWNLOAN:
                    hasResource = Common.Controllers.Database.Procedures.SecurityProfileProcedures.GetUserResourceAccess("SERVICES", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance, out mask);
                    break;

/*
                case VoidSelector.VoidTransactionType.VOIDMDSETRANSOUT:
                    hasResource = Common.Controllers.Database.Procedures.SecurityProfileProcedures.GetUserResourceAccess("TRANSFEROUT", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance, out mask);
                    break;
*/
                default:
                    hasResource = false;
                    break;

            }

            retval = !(!hasResource || (hasResource && mask == ResourceSecurityMask.NONE));
            

            if (!retval)
            {
                MessageBox.Show("Your user role is not authorized to complete this void transaction. ");

            }


            return retval; // need to retry
        }

        //<summary>
        //   gets Management override, if necessary.   If there are limits enforced for a particular type of void, the 
        //   resource is only noted, and the override is left for the limits checking to do the override (to avoid multiple overrides).
        //</summary>
        public bool checkOverride(VoidSelector.VoidTransactionType voidType, out bool hasResource)
        {
            bool hasVoidResource = false;
            hasResource = false;
            var mask = ResourceSecurityMask.NONE;
            bool overrideRqd = Common.Controllers.Database.Procedures.SecurityProfileProcedures.GetUserResourceAccess("VOIDTRANSACTION", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance, out mask);
            bool skipped = false;
            bool canVoid = false;
            bool retval = false;
            bool nxtOverride = false;

            if (!overrideRqd || (overrideRqd && mask == ResourceSecurityMask.NONE))
                hasVoidResource = false;
            else
                hasVoidResource = true;


            switch (voidType)
            {
                case VoidSelector.VoidTransactionType.VOIDBUY:
                    if (GlobalDataAccessor.Instance.DesktopSession.Purchases[0].EntityType == "V")
                    {
                        hasResource = Common.Controllers.Database.Procedures.SecurityProfileProcedures.GetUserResourceAccess("VENDORBUY", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance, out mask);

                    }
                    else
                    {
                        hasResource = Common.Controllers.Database.Procedures.SecurityProfileProcedures.GetUserResourceAccess("CUSTOMERBUY", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance, out mask);
                    }

                    retval = !(!hasResource || (hasResource && mask == ResourceSecurityMask.NONE));
            

                    if (!retval)
                    {
                        MessageBox.Show("Your user role is not authorized to complete this void transaction. ");
                        return false;
                    }
                    canVoid = ManageMultiplePawnItems.CheckForOverrides(true, GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Amount,  out skipped, !hasVoidResource);
                    break;

                case VoidSelector.VoidTransactionType.VOIDLAYAWAY:
                    hasResource = true;
                    var layawayObj = GlobalDataAccessor.Instance.DesktopSession.Layaways[0];
                    if (layawayObj != null)  // must handle override difference of 
                    {
                        
                        layawayObj.SalesTaxInfo = new SalesTaxInfo(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreTaxes);
                        GlobalDataAccessor.Instance.DesktopSession.LayawayPaymentCalc = new LayawayPaymentCalculator(layawayObj.SalesTaxInfo);
                        GlobalDataAccessor.Instance.DesktopSession.LayawayPaymentCalc.CalculateDefaultValues(Math.Round(layawayObj.Amount, 2));
                        GlobalDataAccessor.Instance.DesktopSession.LayawayPaymentCalc.DownPayment = layawayObj.DownPayment;
                        GlobalDataAccessor.Instance.DesktopSession.LayawayPaymentCalc.NumberOfPayments = layawayObj.NumberOfPayments;
                        canVoid = ItemSearch.CheckRetailOverides(
                            layawayObj.RetailItems, true, 1, layawayObj.DownPayment, (layawayObj.SalesTaxAmount == 0), ref nxtOverride, !hasVoidResource);
                    }
                    break;

                case VoidSelector.VoidTransactionType.VOIDPAWNLOAN:
                    hasResource = true;
                    //GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan
                    //canVoid = ManageMultiplePawnItems.CheckForOverrides(false, out skipped, !hasVoidResource);
                    canVoid = hasVoidResource;  
                    break;

                case VoidSelector.VoidTransactionType.VOIDRETAILSALE:
                    hasResource = true;
                    var saleObj = GlobalDataAccessor.Instance.DesktopSession.Sales[0];
                    if (saleObj != null)
                        canVoid = ItemSearch.CheckRetailOverides(saleObj.RetailItems, false, 1, saleObj.TotalSaleAmount, (saleObj.SalesTaxAmount == 0), ref nxtOverride, !hasVoidResource);

                    break;

                case VoidSelector.VoidTransactionType.VOIDMDSETRANSOUT:
                case VoidSelector.VoidTransactionType.VOIDMDSETRANSIN:
                case VoidSelector.VoidTransactionType.VOIDCASHTRANSSHOPTOBANK:
                case VoidSelector.VoidTransactionType.VOIDCASHTRANSBANKTOSHOP:
                case VoidSelector.VoidTransactionType.VOIDCASHTRANSSHOPTOSHOP:
                    hasResource = true;
                    if (!hasVoidResource)
                    {
                        var overrideType = new List<ManagerOverrideType>(1);
                        var overrideTransaction = new List<ManagerOverrideTransactionType>(1);

                        overrideType.Add(ManagerOverrideType.EXCASH);
                        overrideTransaction.Add(ManagerOverrideTransactionType.SAFE);

                        var mgrOverride = new ManageOverrides(GlobalDataAccessor.Instance.DesktopSession, ManageOverrides.VOID_TRIGGER)
                        {
                            MessageToShow = "Role requires override",
                            ManagerOverrideTypes = overrideType,
                            ManagerOverrideTransactionTypes = overrideTransaction
                        };

                        mgrOverride.ShowDialog();

                        canVoid = mgrOverride.OverrideAllowed;
                    }
                    else
                        canVoid = true;
                    break;


            }

            return canVoid;
        }

        /// <summary>
        /// Convenience method to trigger application functionality
        /// based on the "leaf" button pressed.  A leaf button is one
        /// that triggers functions, rather than new sub menus
        /// </summary>
        /// <param name="functionalityName"></param>
        /// <returns></returns>
        private bool initializeFunctionality(string functionalityName)
        {
            bool rt = false;
            if (!string.IsNullOrEmpty(functionalityName))
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = functionalityName;
                //SR 12/05/2011 Added to reset the data otherwise the nfsc launch after a 
                //safe operation did not insert entry in cd_connectedcduser
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                bool checkPassed = false;
                GlobalDataAccessor.Instance.DesktopSession.GetCashDrawerAssignmentsForStore();
                GlobalDataAccessor.Instance.DesktopSession.PerformCashDrawerChecks(out checkPassed);
                if (!checkPassed)
                {
                    this.resetMenu();
                    return true;
                }
                //Check functions here
                if (functionalityName.Equals("lookupcustomer", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        functionalityName, this, this.endStateNotifier);


                    rt = true;

                }
                else if (functionalityName.Equals("shoptransferout", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    ShopTransferOut transferOutFrm = new ShopTransferOut();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(transferOutFrm);
                    transferOutFrm.ShowDialog(this);
                    this.handleEndFlow(null);

                    rt = true;
                }
                else if (functionalityName.Equals("shoptransferin", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    bool proceed = true;
                    do
                    {
                        TransferPendingList transferList = new TransferPendingList();
                        transferList.ShowDialog(this);
                        if (GlobalDataAccessor.Instance.DesktopSession.SelectedStoreCashTransferNumber != 0)
                        {
                            ShopTransferIn transferIn = new ShopTransferIn();
                            transferIn.ShowDialog();
                            if (!transferIn.Back)
                                proceed = false;
                        }
                        else
                            proceed = false;
                    } while (proceed);
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("banktransferout", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    BankTransferTo bankTransferTo = new BankTransferTo();
                    bankTransferTo.ShowDialog(this);

                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("banktransferin", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    BankTransferFrom bankTransferFrom = new BankTransferFrom();
                    bankTransferFrom.ShowDialog(this);
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("internaltransfer", StringComparison.OrdinalIgnoreCase))
                {

                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.INTERNALSAFETRANSFER;
                    InternalTransfer intTransferFrom = new InternalTransfer();
                    intTransferFrom.ShowDialog(this);
                    this.handleEndFlow(null);
                    rt = true;
                }

                else if (functionalityName.Equals("cashdrawertransfer", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    InternalTransfer intTransferFrom = new InternalTransfer();
                    intTransferFrom.ShowDialog(this);
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("balancecashdrawer", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID = string.Empty;
                    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerName = string.Empty;
                    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherBegBalance = 0;
                    var cashbalanceForm = new BalanceCash();
                    cashbalanceForm.SafeBalance = false;
                    cashbalanceForm.ShowDialog();


                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("merchandisecostrevisions", StringComparison.OrdinalIgnoreCase))
                {
                    //TODO: Place merchandise cost revisions functionality here
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("balanceotherdrawers", StringComparison.OrdinalIgnoreCase))
                {
                    //TODO: Balance other drawer functionality here
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("balancesafe", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID = string.Empty;
                    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerName = string.Empty;
                    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherBegBalance = 0;
                    bool openCD = false;
                    do
                    {
                        GlobalDataAccessor.Instance.DesktopSession.CheckOpenCashDrawers(out openCD);
                        if (openCD && !GlobalDataAccessor.Instance.DesktopSession.TrialBalance && GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID == string.Empty)
                            break;
                        var cashbalanceForm = new BalanceCash();
                        cashbalanceForm.SafeBalance = openCD && !GlobalDataAccessor.Instance.DesktopSession.TrialBalance ? false : true;
                        cashbalanceForm.ShowDialog();


                    } while (openCD);

                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("gunbookedit", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = new LookupCustomerSearchData();
                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                    GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();


                    //Invoke gun book edit flow
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        functionalityName, this, this.endStateNotifier);


                    rt = true;
                }
                else if (functionalityName.Equals("describemerchandise", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = new LookupCustomerSearchData();
                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                    GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();

                    const string workflowname = "newpawnloan";
                    //Invoke new pawn loan flow starting at describe merchandise
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        workflowname, this, this.endStateNotifier);

                    rt = true;


                }
                else if (functionalityName.Equals("newpawnloan", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;

                    //Invoke initial portion of new pawn loan customer flow
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        functionalityName, this, this.endStateNotifier);

                    rt = true;
                }
                else if (functionalityName.Equals("servicepawnloans", StringComparison.OrdinalIgnoreCase))
                {
                    //CashlinxDesktopSession.Instance.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.LOOKUPTICKET;
                    GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = new LookupCustomerSearchData();
                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();

                    //Invoke initial portion of new pawn loan customer flow
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        functionalityName, this, this.endStateNotifier);

                    rt = true;
                }
                else if (functionalityName.Equals("lookupdocuments", StringComparison.OrdinalIgnoreCase))
                {
                    //TODO: Spawn lookup documents dialog - no flow needed
                    ReprintDocument d = new ReprintDocument();
                    d.ShowDialog();
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("cashdrawermanagement", StringComparison.OrdinalIgnoreCase))
                {
                    functionalityName = Commons.TriggerTypes.SHOPCASHMANAGEMENT;
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    //Invoke shop management flow
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        functionalityName, this, this.endStateNotifier);
                    rt = true;
                }

                else if (functionalityName.Equals("customerholdsaddhold", StringComparison.OrdinalIgnoreCase))
                {
                    functionalityName = Commons.TriggerTypes.CUSTOMERHOLD;

                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(functionalityName, this, this.endStateNotifier);


                    rt = true;
                }
                else if (functionalityName.Equals("customerholdseditreleasehold", StringComparison.OrdinalIgnoreCase))
                {
                    functionalityName = Commons.TriggerTypes.CUSTOMERHOLDRELEASE;
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                    functionalityName, this, this.endStateNotifier);
                    rt = true;
                }
                else if (functionalityName.Equals("assignitemphysicallocation", StringComparison.OrdinalIgnoreCase))
                {
                    //Invoke assign item physical location flow
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    var assignFm = new AssignPhysicalLocation();
                    if (assignFm != null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(assignFm);
                        assignFm.ShowDialog(this);
                    }
                    this.handleEndFlow(null);
                    rt = true;

                }
                else if (functionalityName.Equals("update_security_profile", StringComparison.OrdinalIgnoreCase))
                {
                    functionalityName = Commons.TriggerTypes.SECURITY;
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        functionalityName, this, this.endStateNotifier);


                    //this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("lookupticket", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = new LookupCustomerSearchData();
                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(functionalityName, this, this.endStateNotifier);
                    rt = true;
                }
                else if (functionalityName.Equals("pficreatelist", StringComparison.OrdinalIgnoreCase))
                {
                    //If prior PFI exists then don't allow. 
                    if (!HasPriorPFI())
                    {
                        //Invoke pfi create
                        var pfiSelectLoan = new PFI_SelectLoan();
                        // Fix for 329
                        //CashlinxDesktopSession.Instance.HistorySession.AddForm(pfiSelectLoan);
                        pfiSelectLoan.ShowDialog(this);

                        if (pfiSelectLoan.DialogResult == DialogResult.OK)
                        { //-- Restore Menu
                            this.pfiMenuPanel.Visible = true;
                            this.pfiMenuPanel.Enabled = true;
                            this.pfiMenuPanel.BringToFront();
                            this.pfiMenuPanel.ButtonControllers.resetGroupInitialState();
                            this.pfiMenuPanel.Update();
                        }

                    }
                    if (!pfiMenuPanel.Visible)
                        this.handleEndFlow(null);

                    rt = true;
                }
                else if (functionalityName.Equals("pfiverify", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(functionalityName, this, this.endStateNotifier);
                    rt = true;
                }
                else if (functionalityName.Equals("pfipost", StringComparison.OrdinalIgnoreCase))
                {
                    var pfiPostLoan = new PFI_Posting();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(pfiPostLoan);
                    pfiPostLoan.ShowDialog(this);
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("printpfimailers", StringComparison.OrdinalIgnoreCase))
                {
                    var pfiMailers = new PFI_Mailers();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(pfiMailers);
                    pfiMailers.ShowDialog(this);
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("policehold", StringComparison.OrdinalIgnoreCase))
                {
                    //Invoke police hold flow
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(functionalityName, this, this.endStateNotifier);
                    rt = true;
                }
                else if (functionalityName.Equals("itemrelease", StringComparison.OrdinalIgnoreCase))
                {
                    //Invoke item release flow
                    functionalityName = Commons.TriggerTypes.POLICEHOLDRELEASE;
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.ReleaseToClaimant = false;
                    GlobalDataAccessor.Instance.DesktopSession.PoliceInformation = null;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                    functionalityName, this, this.endStateNotifier);
                    rt = true;
                }
                else if (functionalityName.Equals("policeseize", StringComparison.OrdinalIgnoreCase))
                {
                    //Invoke police seize flow
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(functionalityName, this, this.endStateNotifier);
                    rt = true;
                }
                else if (functionalityName.Equals("releasefingerprints", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(functionalityName, this,
                                                                                            this.endStateNotifier);
                    rt = true;
                }
                else if (functionalityName.Equals("reports", StringComparison.OrdinalIgnoreCase))
                {
                    //Invoke reports flow
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    var assignFm = new global::Pawn.Forms.Report.Reports();
                    if (assignFm != null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(assignFm);
                        assignFm.ShowDialog(this);
                    }
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("voidtransaction", StringComparison.OrdinalIgnoreCase))
                {
                    //BZ # 419
                    VoidTransactionForm bTranForm = null;
                    //BZ # 419 end

                    bool retry;

                    do {
                        retry = false;

                        var voidTrxForm = new VoidSelector();
                        voidTrxForm.ShowDialog();
                        if (voidTrxForm.DialogResult == DialogResult.OK)
                        {
                            bool hasVoidResource = false;


                            var vType =
                                    voidTrxForm.SelectedVoidTransactionType;

                            retry = true;
                            if (canVoid(vType))
                            {
                                bool hasResource = false;

                                retry = false;

                                switch (vType)
                                {
                                    //Void Buy Activities
                                    case VoidSelector.VoidTransactionType.VOIDBUY:
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = "VoidBuy";
                                        GlobalDataAccessor.Instance.DesktopSession.PerformCashDrawerChecks(out checkPassed);
                                        if (!checkPassed)
                                            break;
                                        var vBuyForm = new VoidTransactionForm();
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(vBuyForm);
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.VOIDBUY;
                                        vBuyForm.ShowDialog(this);
                                        if (GlobalDataAccessor.Instance.DesktopSession.Purchases != null &&
                                            GlobalDataAccessor.Instance.DesktopSession.Purchases.Count > 0)
                                        {

                                            if (checkOverride(vType, out hasResource))
                                            {
                                                if (hasResource)
                                                {
                                                    var voidPRForm = new VoidTransactionDetail();
                                                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(voidPRForm);
                                                    voidPRForm.ShowDialog(this);
                                                }
                                            }
                                            else
                                                retry = true;
                                        }
                                        break;

                                    //Void Pawn Loan Activities
                                    case VoidSelector.VoidTransactionType.VOIDPAWNLOAN:
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = "VoidLoan";
                                        GlobalDataAccessor.Instance.DesktopSession.PerformCashDrawerChecks(out checkPassed);
                                        if (!checkPassed)
                                            break;

                                        
                                        var vForm = new VoidLoanForm(checkOverride(vType, out hasResource)); 
                                        vForm.ShowDialog(this);
                                        
                                        break;

                                    //Void Retail Sale
                                    case VoidSelector.VoidTransactionType.VOIDRETAILSALE:
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = "VoidSale";
                                        GlobalDataAccessor.Instance.DesktopSession.PerformCashDrawerChecks(out checkPassed);
                                        if (!checkPassed)
                                            break;

                                        var vRetSaleForm = new VoidTransactionForm();
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(vRetSaleForm);
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.VOIDSALE;
                                        vRetSaleForm.ShowDialog(this);
                                        if (GlobalDataAccessor.Instance.DesktopSession.Sales != null &&
                                            GlobalDataAccessor.Instance.DesktopSession.Sales.Count > 0)
                                        {
                                            if (checkOverride(vType, out hasResource))
                                            {
                                                var voidPRForm = new VoidTransactionDetail();
                                                GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(voidPRForm);
                                                voidPRForm.ShowDialog(this);
                                            }
                                            else
                                                retry = true;
                                        }
                                        break;

                                    //TODO: Link void item cost revision functionality here
                                    case VoidSelector.VoidTransactionType.VOIDITEMCOSTREV:
                                        MessageBox.Show("** BLOCK - VOID ITEM COST REVISION **");
                                        break;

                                    //TODO: Consolidate void merchandise transfer in void selector - build PWN_10
                                    case VoidSelector.VoidTransactionType.VOIDMDSETRANSIN:
                                    case VoidSelector.VoidTransactionType.VOIDMDSETRANSOUT:


                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.VOIDMERCHANDISETRANSFER;
                                        GlobalDataAccessor.Instance.DesktopSession.MdseTransferData = null;
                                        VoidTransactionForm mTranForm = new VoidTransactionForm(vType);
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(mTranForm);
                                        mTranForm.ShowDialog(this);
                                        if (GlobalDataAccessor.Instance.DesktopSession.MdseTransferData != null)
                                        {
                                            if (checkOverride(vType, out hasResource))
                                            {
                                                VoidMdseTransfer voidmdseTransfer = new VoidMdseTransfer();
                                                voidmdseTransfer.ShowDialog();
                                            }
                                            else
                                                retry = true;
                                        }
                                        break;

                                    //TODO: Consolidate void bank cash transfer in void selector - build PWN_10
                                    case VoidSelector.VoidTransactionType.VOIDCASHTRANSSHOPTOBANK:
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = "VoidShopToBank"; // BZ # 419
                                        GlobalDataAccessor.Instance.DesktopSession.PerformCashDrawerChecks(out checkPassed);
                                        if (!checkPassed)
                                            break;
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.VOIDSHOPTOBANK; // BZ # 419
                                        GlobalDataAccessor.Instance.DesktopSession.CashTransferData = null;
                                        bTranForm = new VoidTransactionForm();
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(bTranForm);
                                        bTranForm.ShowDialog(this);

                                        if (GlobalDataAccessor.Instance.DesktopSession.CashTransferData != null)
                                        {
                                            if (checkOverride(vType, out hasResource))
                                            {
                                                VoidBankTransfer voidBankTransfer = new VoidBankTransfer(vType);
                                                voidBankTransfer.ShowDialog();
                                            }
                                            else
                                                retry = true;
                                        }
                                        break;
                                    case VoidSelector.VoidTransactionType.VOIDCASHTRANSBANKTOSHOP:
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = "VoidBankToShop"; // BZ # 419
                                        GlobalDataAccessor.Instance.DesktopSession.PerformCashDrawerChecks(out checkPassed);
                                        if (!checkPassed)
                                            break;
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.VOIDBANKTOSHOP; // BZ # 419
                                        GlobalDataAccessor.Instance.DesktopSession.CashTransferData = null;
                                        bTranForm = new VoidTransactionForm();
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(bTranForm);
                                        bTranForm.ShowDialog(this);

                                        if (GlobalDataAccessor.Instance.DesktopSession.CashTransferData != null)
                                        {

                                            if (checkOverride(vType, out hasResource))
                                            {
                                                VoidBankTransfer voidBankTransfer = new VoidBankTransfer(vType);
                                                voidBankTransfer.ShowDialog();
                                            }
                                            else
                                                retry = true;
                                        }
                                        break;

                                    //Void Shop To Shop Cash Transfer
                                    case VoidSelector.VoidTransactionType.VOIDCASHTRANSSHOPTOSHOP:
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = "VoidShopToShopTransfer";
                                        GlobalDataAccessor.Instance.DesktopSession.PerformCashDrawerChecks(out checkPassed);
                                        if (!checkPassed)
                                            break;
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.VOIDSHOPTOSHOPTRANSFER;
                                        GlobalDataAccessor.Instance.DesktopSession.CashTransferData = null;
                                        VoidTransactionForm tranForm = new VoidTransactionForm();
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(tranForm);
                                        tranForm.ShowDialog(this);

                                        if (GlobalDataAccessor.Instance.DesktopSession.CashTransferData != null )
                                        {
                                            if (checkOverride(vType, out hasResource))
                                            {
                                                if (GlobalDataAccessor.Instance.DesktopSession.CashTransferData.TransferStatus == "PENDING")
                                                {
                                                    ShopTransferOut voidShopTransfer = new ShopTransferOut();
                                                    voidShopTransfer.ShowDialog();
                                                }
                                                else
                                                {
                                                    ShopTransferIn voidShopTransfer = new ShopTransferIn();
                                                    voidShopTransfer.ShowDialog();
                                                }
                                            }
                                            else
                                                retry = true;
                                        }
                                        break;

                                    //TODO: Link Void Police Sieze functionality here
                                    case VoidSelector.VoidTransactionType.VOIDPOLICESEIZE:
                                        MessageBox.Show("** BLOCK - VOID POLICE SIEZE **");
                                        break;

                                    //TODO: Link Void Restitution functionality here
                                    case VoidSelector.VoidTransactionType.VOIDRESTITUTION:
                                        MessageBox.Show("** BLOCK - VOID RESTITUTION **");
                                        break;

                                    //TODO: Link Void Release To Claimant functionality here
                                    case VoidSelector.VoidTransactionType.VOIDRTC:
                                        MessageBox.Show("** BLOCK - VOID RTC **");
                                        break;

                                    //Void layaway activities
                                    case VoidSelector.VoidTransactionType.VOIDLAYAWAY:
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = "VoidLayaway";
                                        GlobalDataAccessor.Instance.DesktopSession.PerformCashDrawerChecks(out checkPassed);
                                        if (!checkPassed)
                                            break;
                                        var vLayForm = new VoidTransactionForm();
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(vLayForm);
                                        GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.VOIDLAYAWAY;
                                        vLayForm.ShowDialog(this);
                                        if (GlobalDataAccessor.Instance.DesktopSession.Layaways != null &&
                                            GlobalDataAccessor.Instance.DesktopSession.Layaways.Count > 0)
                                        {
                                            if (checkOverride(vType, out hasResource))
                                            {
                                                var voidPRForm = new VoidLayawayActivity(!hasResource);
                                                GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(voidPRForm);
                                                voidPRForm.ShowDialog(this);
                                            }
                                            else
                                                retry = true;
                                        }
                                        break;

                                    //TODO: Link Void PFI functionality here
                                    case VoidSelector.VoidTransactionType.VOIDPFI:
                                        MessageBox.Show("** BLOCK - VOID PFI **");
                                        break;
		                            case VoidSelector.VoidTransactionType.VOIDRELEASEFINGERPRINTS:
		                                GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = "VoidReleaseFingerprints";
		                                GlobalDataAccessor.Instance.DesktopSession.PerformCashDrawerChecks(out checkPassed);
		                                if (!checkPassed)
		                                    break;
		
		                                var vReleaseFingerprintsForm = new VoidTransactionForm();
		                                GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(vReleaseFingerprintsForm);
		                                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.VOIDRELEASEFINGERPRINTS;
		                                vReleaseFingerprintsForm.ShowDialog(this);
										break;
                                }

                            }
                            else
                                retry = true;
                        }
                    } while (retry);

                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("merchandisetransferout", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    ManageTransferOut transferOutFrm = new ManageTransferOut();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(transferOutFrm);
                    transferOutFrm.ShowDialog(this);
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("returncustomerbuy", StringComparison.OrdinalIgnoreCase) ||
                    functionalityName.Equals("returnvendorbuy", StringComparison.OrdinalIgnoreCase))
                {

                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = new LookupCustomerSearchData();
                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                    const string workflowname = "purchasereturn";
                    //Invoke return purchase flow 
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        workflowname, this, this.endStateNotifier);

                    rt = true;

                }
                else if (functionalityName.Equals("refundsale", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = new LookupCustomerSearchData();
                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                    const string workflowname = "retailreturn";
                    //Invoke return sale flow 
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        workflowname, this, this.endStateNotifier);

                    rt = true;
                }
                else if (functionalityName.Equals("refundlayaway", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = new LookupCustomerSearchData();
                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                    const string workflowname = "layawayreturn";
                    //Invoke return sale flow 
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        workflowname, this, this.endStateNotifier);

                    rt = true;

                }

                else if (functionalityName.Equals("gunbookprint", StringComparison.OrdinalIgnoreCase))
                {
                    //Gun book utilities
                    var gForm = new GunBookPrintOptions();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(gForm);
                    gForm.ShowDialog();
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("buylookup", StringComparison.OrdinalIgnoreCase))
                {
                    //Customer purchase flow using lookup customer route
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.CUSTOMERPURCHASE;
                    functionalityName = "customerpurchase";
                    GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = new LookupCustomerSearchData();
                    GlobalDataAccessor.Instance.DesktopSession.Purchases = new List<PurchaseVO>();

                    //Invoke purchases flow for customer buy
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        functionalityName, this, this.endStateNotifier);

                    rt = true;

                }
                else if (functionalityName.Equals("buydescribe", StringComparison.OrdinalIgnoreCase))
                {
                    //Customer purchase flow using describe item route
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.DESCRIBEITEMCUSTOMERPURCHASE;
                    functionalityName = "customerpurchase";
                    GlobalDataAccessor.Instance.DesktopSession.Purchases = new List<PurchaseVO>();

                    //Invoke purchases flow for customer buy
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        functionalityName, this, this.endStateNotifier);


                    rt = true;

                }
                else if (functionalityName.Equals("vendorbuy", StringComparison.OrdinalIgnoreCase))
                {
                    //vendor purchase flow through lookup vendor
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = Commons.TriggerTypes.VENDORPURCHASE;
                    functionalityName = "vendorpurchase";
                    GlobalDataAccessor.Instance.DesktopSession.Purchases = new List<PurchaseVO>();

                    //Invoke purchases flow for vendor buy
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        functionalityName, this, this.endStateNotifier);

                    rt = true;

                }
                else if (functionalityName.Equals("nfsc", StringComparison.OrdinalIgnoreCase))
                {
                    var cds = GlobalDataAccessor.Instance.DesktopSession;
                    //if (cds != null && (cds.LoggedInUserSecurityProfile == null || string.IsNullOrEmpty(cds.LoggedInUserSecurityProfile.UserName)))
                    //    cds.PerformAuthorization();
                    if (cds != null && cds.LoggedInUserSecurityProfile != null && !string.IsNullOrEmpty(cds.LoggedInUserSecurityProfile.UserName))
                    {
                        //spawn internet explorer process with phase 1 URL
                        functionalityName = "nfsc";
                        CashlinxDesktopSession.SpawnCashlinxPDA();
                    }
                    if (cds != null)
                    {
                        cds.LoggedInUserSecurityProfile = null;
                    }
                    GlobalDataAccessor.Instance.DesktopSession.ClearLoggedInUser();
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("topstransfer", StringComparison.OrdinalIgnoreCase))
                {
                    //Gun book utilities
                    var gForm = new TOPSTransferDialog();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(gForm);
                    gForm.ShowDialog();
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("retail", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.CompleteSale = false;
                    GlobalDataAccessor.Instance.DesktopSession.CompleteLayaway = false;
                    GlobalDataAccessor.Instance.DesktopSession.LayawayMode = false;
                    GlobalDataAccessor.Instance.DesktopSession.LayawayPaymentCalc = null;
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.Sales = new List<SaleVO>(1) { new SaleVO() };
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                             "sale", this, this.endStateNotifier);
                    rt = true;
                }
                else if (functionalityName.Equals("changeretailprice", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.Sales = new List<SaleVO>(1) { new SaleVO() };
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                             MainFlowExecutor.CHANGERETAILPRICE, this, this.endStateNotifier);
                    rt = true;
                }
                else if (functionalityName.Equals("merchandisetransferin", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.Transfers = new List<TransferVO>(1) { new TransferVO() };
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                             MainFlowExecutor.TRANSFERIN, this, this.endStateNotifier);
                    rt = true;
                }
                else if (functionalityName.Equals("changeitemassigntype", StringComparison.OrdinalIgnoreCase))
                {
                    var gForm = new ChangeItemAssignmentType();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(gForm);
                    gForm.ShowDialog();
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("chargeoff", StringComparison.OrdinalIgnoreCase))
                {
                    InventoryChargeOffSearch invForm = new InventoryChargeOffSearch();
                    invForm.ShowDialog();
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("layawayforfeiture", StringComparison.OrdinalIgnoreCase))
                {
                    LayawayForfeitureSearch searchForm = new LayawayForfeitureSearch();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.AddForm(searchForm);
                    searchForm.ShowDialog();
                    this.handleEndFlow(null);
                    rt = true;
                }
            }

            if (rt == false)
            {
                MessageBox.Show("Could not invoke child functionality { " + functionalityName + " }",
                    "MenuFunctionalityError", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return (rt);
        }

        private bool HasPriorPFI()
        {
            List<int> refNumbers;
            string sErrorCode;
            string sErrorText;

            StoreLoans.CheckForPriorPFI(
                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                out refNumbers,
                out sErrorCode,
                out sErrorText);

            if (refNumbers.Count > 0)
            {
                string sMsg = "The following loans need to be PFI posted before continuing." + Environment.NewLine + Environment.NewLine;
                sMsg += "Pending Loans: ";

                foreach (var i in refNumbers)
                {
                    sMsg += i + ", ";
                }
                sMsg = sMsg.Substring(0, sMsg.Length - 2) + ".";

                MessageBox.Show(sMsg, "PFI Posting Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NewDesktop()
        {
            InitializeComponent();
            this.resetFlag = false;
            this.BackgroundImage = global::Pawn.Properties.Resources.DemoBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            var monitorSize = System.Windows.Forms.SystemInformation.PrimaryMonitorSize;
            this.ClientSize = new System.Drawing.Size(monitorSize.Width, monitorSize.Height);
            this.MaximumSize = new System.Drawing.Size(monitorSize.Width, monitorSize.Height);
            this.MinimumSize = new System.Drawing.Size(monitorSize.Width, monitorSize.Height);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(0, 0);

            //Compute center screen coords
            float halfScreenWidth = monitorSize.Width / 2.0f;
            float halfScreenHeight = monitorSize.Height / 2.0f;

            //Setup user control array
            var ucArray =
                    new UserControl[]
                    {
                            this.mainMenuPanel, this.pawnMenuPanel,
                            this.lookupMenuPanel, this.utilitiesMenuPanel,
                            this.pfiMenuPanel, this.customerHoldsMenuPanel,
                            this.policeMenuPanel, this.reportsMenuPanel,
                            this.manageCashMenuPanel, this.transferMenuPanel,
                            this.buyMenuPanel, this.customerBuyMenuPanel,
                            this.refundReturnMenuPanel, this.voidMenuPanel,
                            this.manageInventoryMenuPanel, this.changePricingMenuPanel,
                            this.gunBookMenuPanel, this.safeOperationsMenuPanel,
                            this.cashDrawerMenuPanel
                    };

            //Center all panels
            foreach (var curPanel in ucArray)
            {
                this.centerMenuPanel(curPanel, halfScreenWidth, halfScreenHeight);
            }

            //Make the version label appear in the bottom left
            float verLabelHeight = this.versionLabel.Height;
            this.versionLabel.Location = new Point(
                    0, (int)(System.Math.Floor(monitorSize.Height - verLabelHeight - 10)));

            //Make the date label appear in the bottom right
            float dateLabelWidth = this.shopDateLabel.Width;
            float dateLabelHeight = this.shopDateLabel.Height;
            this.shopDateLabel.Location = new Point(
                    (int)(System.Math.Floor(monitorSize.Width - dateLabelWidth - 10)),
                    (int)(System.Math.Floor(monitorSize.Height - dateLabelHeight - 10)));

            //Create the USB drive detector
            if (!DesignMode)
            {
                try
                {
                    if (USBUtilities.CreateDriveDetector(GlobalDataAccessor.Instance.DesktopSession.DeviceArrivedEventHandler, GlobalDataAccessor.Instance.DesktopSession.DeviceRemovedEventHandler, this, out this.detector))
                    {
                        if (FileLogger.Instance.IsLogDebug)
                        {
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Created USB drive detector");
                        }
                    }
                    else
                    {
                        if (FileLogger.Instance.IsLogWarn)
                        {
                            FileLogger.Instance.logMessage(LogLevel.WARN, this, "Could not create USB drive detector");
                        }
                    }
                }
                catch (Exception eX)
                {
                    this.detector = null;
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not create USB drive detector: {0}", eX);
                    }
                }
            }
        }

        /// <summary>
        /// Allows USB detector to intercept certain OS level events
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (!DesignMode)
            {
                if (this.detector != null)
                {
                    this.detector.WndProc(ref m);
                }
            }
        }

        /// <summary>
        /// Centers a user control on the given screen based on resolution
        /// </summary>
        /// <param name="uC"></param>
        /// <param name="halfScreenWidth"></param>
        /// <param name="halfScreenHeight"></param>
        private void centerMenuPanel(UserControl uC,
            float halfScreenWidth, float halfScreenHeight)
        {
            if (uC == null)
                return;
            //Center control
            float halfWidth = uC.Width / 2.0f;
            float halfHeight = uC.Height / 2.0f;
            uC.Location = new Point(
                (int)(System.Math.Floor(halfScreenWidth - halfWidth)),
                (int)(System.Math.Floor(halfScreenHeight - halfHeight)));
        }

        /// <summary>
        /// Disables a user control and its button controller - not in use yet - do not remove this method
        /// </summary>
        /// <param name="uC"></param>
        /// <param name="buttonControl"></param>
        private void disableMenuPanel(UserControl uC, ImageButtonControllerGroup buttonControl)
        {
            if (uC == null || buttonControl == null)
                return;

            if (uC.Enabled)
            {
                uC.Enabled = false;
                uC.Visible = false;
                buttonControl.resetGroupInitialState();
                uC.SendToBack();
            }
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
        }

        #endregion


        private void exitButton_Click(object sender, EventArgs e)
        {

            //02/10/2010 GL
            //Adding check for locked out users, they will not
            //be allowed to balance their cash drawers
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;

            //06/17/2010 GL
            //If no security profile has been loaded, exit will cause object not set reference exception

            this.KillApplication(null);
        }

        /// <summary>
        /// Kill the application
        /// </summary>
        /// <param name="reason">Log a reason for app exit - optional</param>
        public void KillApplication(string reason)
        {
            string resStr = reason ?? "";
            if (FileLogger.Instance != null)
            {
                if (FileLogger.Instance.IsEnabled && FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, this, "Exiting application " + resStr);
                }
                FileLogger.Instance.Dispose();
            }
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>



        /// <summary>
        /// Event handler for the main menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MainMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            //we only care about this event if the panel is being disabled
            if (this.mainMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.mainMenuPanel.Visible = false;
                this.mainMenuPanel.SendToBack();
                this.mainMenuPanel.Update();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.mainMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.mainMenuPanel.Visible = true;
                    this.mainMenuPanel.Enabled = true;
                    this.mainMenuPanel.BringToFront();
                    this.mainMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.mainMenuPanel.Update();
                }
            }
        }

        /// <summary>
        /// Event handler for the lookup sub panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LookupMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.lookupMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.lookupMenuPanel.Visible = false;
                this.lookupMenuPanel.Update();
                this.lookupMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.lookupMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.lookupMenuPanel.Visible = true;
                    this.lookupMenuPanel.Enabled = true;
                    this.lookupMenuPanel.BringToFront();
                    this.lookupMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.lookupMenuPanel.Update();
                }
            }
        }

        /// <summary>
        /// Event handler for the pawn menu sub panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PawnMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.pawnMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.pawnMenuPanel.Visible = false;
                this.pawnMenuPanel.Update();
                this.pawnMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.pawnMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.pawnMenuPanel.Visible = true;
                    this.pawnMenuPanel.Enabled = true;
                    this.pawnMenuPanel.BringToFront();
                    this.pawnMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.pawnMenuPanel.Update();
                }
            }
        }

        /// <summary>
        /// Event handler for the utilities menu sub panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UtilitiesMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.utilitiesMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.utilitiesMenuPanel.Visible = false;
                this.utilitiesMenuPanel.Update();
                this.utilitiesMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.utilitiesMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.utilitiesMenuPanel.Visible = true;
                    this.utilitiesMenuPanel.Enabled = true;
                    this.utilitiesMenuPanel.BringToFront();
                    this.utilitiesMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.utilitiesMenuPanel.Update();

                }
            }
        }

        /// <summary>
        /// Event handler for the utilities menu sub panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PfiMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.pfiMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.pfiMenuPanel.Visible = false;
                this.pfiMenuPanel.Update();
                this.pfiMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.pfiMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.pfiMenuPanel.Visible = true;
                    this.pfiMenuPanel.Enabled = true;
                    this.pfiMenuPanel.BringToFront();
                    this.pfiMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.pfiMenuPanel.Update();

                }
            }
        }
        /// <summary>
        /// Event handler for the customer holds menu sub panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CustomerHoldsMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.customerHoldsMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.customerHoldsMenuPanel.Visible = false;
                this.customerHoldsMenuPanel.Update();
                this.customerHoldsMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.customerHoldsMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.customerHoldsMenuPanel.Visible = true;
                    this.customerHoldsMenuPanel.Enabled = true;
                    this.customerHoldsMenuPanel.BringToFront();
                    this.customerHoldsMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.customerHoldsMenuPanel.Update();

                }
            }
        }

        /// <summary>
        /// Event handler for the police menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PoliceMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.policeMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.policeMenuPanel.Visible = false;
                this.policeMenuPanel.Update();
                this.policeMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.policeMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.policeMenuPanel.Visible = true;
                    this.policeMenuPanel.Enabled = true;
                    this.policeMenuPanel.BringToFront();
                    this.policeMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.policeMenuPanel.Update();
                }
            }
        }

        /// <summary>
        /// Event handler for the report menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReportMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.reportsMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.reportsMenuPanel.Visible = false;
                this.reportsMenuPanel.Update();
                this.reportsMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.reportsMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.reportsMenuPanel.Visible = true;
                    this.reportsMenuPanel.Enabled = true;
                    this.reportsMenuPanel.BringToFront();
                    this.reportsMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.reportsMenuPanel.Update();
                }
            }
        }

        /// <summary>
        /// Event handler for the transfer menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TransferMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.transferMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.transferMenuPanel.Visible = false;
                this.transferMenuPanel.Update();
                this.transferMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.transferMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.transferMenuPanel.Visible = true;
                    this.transferMenuPanel.Enabled = true;
                    this.transferMenuPanel.BringToFront();
                    this.transferMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.transferMenuPanel.Update();
                }
            }
        }


        /// <summary>
        /// Event handler for the manage cash menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ManageCashMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.manageCashMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.manageCashMenuPanel.Visible = false;
                this.manageCashMenuPanel.Update();
                this.manageCashMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.manageCashMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.manageCashMenuPanel.Visible = true;
                    this.manageCashMenuPanel.Enabled = true;
                    this.manageCashMenuPanel.BringToFront();
                    this.manageCashMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.manageCashMenuPanel.Update();
                }
            }
        }


        /// <summary>
        /// Event handler for the buy menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BuyMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.buyMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.buyMenuPanel.Visible = false;
                this.buyMenuPanel.Update();
                this.buyMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.buyMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.buyMenuPanel.Visible = true;
                    this.buyMenuPanel.Enabled = true;
                    this.buyMenuPanel.BringToFront();
                    this.buyMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.buyMenuPanel.Update();
                }
            }
        }
        /// <summary>
        /// Event handler for the customer buy menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CustomerBuyMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.customerBuyMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.customerBuyMenuPanel.Visible = false;
                this.customerBuyMenuPanel.Update();
                this.customerBuyMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.customerBuyMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.customerBuyMenuPanel.Visible = true;
                    this.customerBuyMenuPanel.Enabled = true;
                    this.customerBuyMenuPanel.BringToFront();
                    this.customerBuyMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.customerBuyMenuPanel.Update();
                }
            }
        }
        /// <summary>
        /// Event handler for the refund return menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefundReturnMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.refundReturnMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.refundReturnMenuPanel.Visible = false;
                this.refundReturnMenuPanel.Update();
                this.refundReturnMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.refundReturnMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.refundReturnMenuPanel.Visible = true;
                    this.refundReturnMenuPanel.Enabled = true;
                    this.refundReturnMenuPanel.BringToFront();
                    this.refundReturnMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.refundReturnMenuPanel.Update();
                }
            }
        }
        /// <summary>
        /// Event handler for the void menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void VoidMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.voidMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.voidMenuPanel.Visible = false;
                this.voidMenuPanel.Update();
                this.voidMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.voidMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.voidMenuPanel.Visible = true;
                    this.voidMenuPanel.Enabled = true;
                    this.voidMenuPanel.BringToFront();
                    this.voidMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.voidMenuPanel.Update();
                }
            }
        }

        /// <summary>
        /// Event handler for the manage inventory menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ManageInventoryMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
                return;
            if (this.manageInventoryMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.manageInventoryMenuPanel.Visible = false;
                this.manageInventoryMenuPanel.Update();
                this.manageInventoryMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.manageInventoryMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.manageInventoryMenuPanel.Visible = true;
                    this.manageInventoryMenuPanel.Enabled = true;
                    this.manageInventoryMenuPanel.BringToFront();
                    this.manageInventoryMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.manageInventoryMenuPanel.Update();
                }
            }
        }

        /// <summary>
        /// Event handler for the cash drawer menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CashDrawerMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag)
                return;
            if (this.cashDrawerMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.cashDrawerMenuPanel.Visible = false;
                this.cashDrawerMenuPanel.Update();
                this.cashDrawerMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.cashDrawerMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.cashDrawerMenuPanel.Visible = true;
                    this.cashDrawerMenuPanel.Enabled = true;
                    this.cashDrawerMenuPanel.BringToFront();
                    this.cashDrawerMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.cashDrawerMenuPanel.Update();
                }
            }
        }

        /// <summary>
        /// Event handler for the change pricing menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangePricingMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag)
                return;
            if (this.changePricingMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.changePricingMenuPanel.Visible = false;
                this.changePricingMenuPanel.Update();
                this.changePricingMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.changePricingMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.changePricingMenuPanel.Visible = true;
                    this.changePricingMenuPanel.Enabled = true;
                    this.changePricingMenuPanel.BringToFront();
                    this.changePricingMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.changePricingMenuPanel.Update();
                }
            }
        }

        /// <summary>
        /// Event handler for the gun book menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GunBookMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag)
                return;
            if (this.gunBookMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.gunBookMenuPanel.Visible = false;
                this.gunBookMenuPanel.Update();
                this.gunBookMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.gunBookMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.gunBookMenuPanel.Visible = true;
                    this.gunBookMenuPanel.Enabled = true;
                    this.gunBookMenuPanel.BringToFront();
                    this.gunBookMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.gunBookMenuPanel.Update();
                }
            }
        }

        public void SafeOperationsMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag)
                return;
            if (this.safeOperationsMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.safeOperationsMenuPanel.Visible = false;
                this.safeOperationsMenuPanel.Update();
                this.safeOperationsMenuPanel.SendToBack();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.safeOperationsMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.safeOperationsMenuPanel.Visible = true;
                    this.safeOperationsMenuPanel.Enabled = true;
                    this.safeOperationsMenuPanel.BringToFront();
                    this.safeOperationsMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.safeOperationsMenuPanel.Update();
                }
            }
        }

        /// Load event handler for the desktop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDesktop_Load(object sender, EventArgs e)
        {
            this.endStateNotifier = new FxnBlock();
            this.endStateNotifier.InputParameter = null;
            this.endStateNotifier.Function = this.handleEndFlow;
            this.mainMenuPanel.setExitButtonHandler(this.exitButton_Click);

            //Acquire cashlinx desktop session object
            this.cdSession = CashlinxDesktopSession.Instance;
            try
            {
                cdSession.Setup(this);

                cdSession.ResourceProperties.DP = Properties.Resources.DP;
                cdSession.ResourceProperties.HB = Properties.Resources.HB;
                cdSession.ResourceProperties.HP = Properties.Resources.HP;
                cdSession.ResourceProperties.Pearl = Properties.Resources.Pearl;

                cdSession.ResourceProperties.oldvistabutton_blue = Properties.Resources.oldvistabutton_blue;
                cdSession.ResourceProperties.vistabutton_blue = Properties.Resources.vistabutton_blue;
                cdSession.ResourceProperties.cl1 = Common.Properties.Resources.cl1;
                cdSession.ResourceProperties.cl2 = Common.Properties.Resources.cl2;
                cdSession.ResourceProperties.cl3 = Common.Properties.Resources.cl3;
                cdSession.ResourceProperties.cl4 = Common.Properties.Resources.cl4;
                cdSession.ResourceProperties.cl5 = Common.Properties.Resources.cl5;
                cdSession.ResourceProperties.newDialog_400_BlueScale = Common.Properties.Resources.newDialog_400_BlueScale;
                cdSession.ResourceProperties.newDialog_512_BlueScale = Common.Properties.Resources.newDialog_512_BlueScale;
                cdSession.ResourceProperties.newDialog_600_BlueScale = Common.Properties.Resources.newDialog_600_BlueScale;

                cdSession.ResourceProperties.OverrideMachineName = global::Pawn.Properties.Resources.OverrideMachineName;

            }
            catch(Exception eX)
            {
                KillApplication("Cashlinx Desktop Session Setup failed: " + eX.Message);
            }


        }


        /// <summary>
        /// Key down event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDesktop_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.resetFlag == true)
                return;
            int formsInTreeCnt = GlobalDataAccessor.Instance.DesktopSession.HistorySession.FormsInTree();
            if (formsInTreeCnt > 1)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.ResetFocus();
                return;
            }

            //Only use menu hot key logic when a portion of the menu
            //is actually in focus
            if ((this.mainMenuPanel.Visible && this.mainMenuPanel.Enabled) ||
                (this.pawnMenuPanel.Visible && this.pawnMenuPanel.Enabled) ||
                (this.lookupMenuPanel.Visible && this.lookupMenuPanel.Enabled) ||
                (this.utilitiesMenuPanel.Visible && this.utilitiesMenuPanel.Enabled) ||
                (this.reportsMenuPanel.Visible && this.reportsMenuPanel.Enabled) ||
                (this.pfiMenuPanel.Visible && this.pfiMenuPanel.Enabled) ||
                (this.customerHoldsMenuPanel.Visible && this.customerHoldsMenuPanel.Enabled) ||
                (this.policeMenuPanel.Visible && this.policeMenuPanel.Enabled) ||
                (this.transferMenuPanel.Visible && this.transferMenuPanel.Enabled) ||
                (this.manageCashMenuPanel.Visible && this.manageCashMenuPanel.Enabled) ||
                (this.buyMenuPanel.Visible && this.buyMenuPanel.Enabled) ||
                (this.customerBuyMenuPanel.Visible && this.customerBuyMenuPanel.Enabled) ||
                (this.refundReturnMenuPanel.Visible && this.refundReturnMenuPanel.Enabled) ||
                (this.voidMenuPanel.Visible && this.voidMenuPanel.Enabled) ||
                (this.manageInventoryMenuPanel.Visible && this.manageInventoryMenuPanel.Enabled) ||
                (this.cashDrawerMenuPanel.Visible && this.cashDrawerMenuPanel.Enabled) ||
                (this.changePricingMenuPanel.Visible && this.changePricingMenuPanel.Enabled) ||
                (this.gunBookMenuPanel.Visible && this.gunBookMenuPanel.Enabled) ||
                (this.safeOperationsMenuPanel.Visible && this.safeOperationsMenuPanel.Enabled))
            {
                MenuLevelController curMenuLevel = null;
                if (this.mainMenuPanel.Enabled)
                {
                    curMenuLevel = this.mainMenuPanel.MenuController;
                }
                else if (this.pawnMenuPanel.Enabled)
                {
                    curMenuLevel = this.pawnMenuPanel.MenuController;
                }
                else if (this.lookupMenuPanel.Enabled)
                {
                    curMenuLevel = this.lookupMenuPanel.MenuController;
                }
                else if (this.utilitiesMenuPanel.Enabled)
                {
                    curMenuLevel = this.utilitiesMenuPanel.MenuController;
                }
                else if (this.reportsMenuPanel.Enabled)
                {
                    curMenuLevel = this.reportsMenuPanel.MenuController;
                }
                else if (this.pfiMenuPanel.Enabled)
                {
                    curMenuLevel = this.pfiMenuPanel.MenuController;
                }
                else if (this.customerHoldsMenuPanel.Enabled)
                {
                    curMenuLevel = this.customerHoldsMenuPanel.MenuController;
                }
                else if (this.policeMenuPanel.Enabled)
                {
                    curMenuLevel = this.policeMenuPanel.MenuController;
                }
                else if (this.transferMenuPanel.Enabled)
                {
                    curMenuLevel = this.transferMenuPanel.MenuController;
                }
                else if (this.manageCashMenuPanel.Enabled)
                {
                    curMenuLevel = this.manageCashMenuPanel.MenuController;
                }
                else if (this.buyMenuPanel.Enabled)
                {
                    curMenuLevel = this.buyMenuPanel.MenuController;
                }
                else if (this.customerBuyMenuPanel.Enabled)
                {
                    curMenuLevel = this.customerBuyMenuPanel.MenuController;
                }
                else if (this.refundReturnMenuPanel.Enabled)
                {
                    curMenuLevel = this.refundReturnMenuPanel.MenuController;
                }
                else if (this.voidMenuPanel.Enabled)
                {
                    curMenuLevel = this.voidMenuPanel.MenuController;
                }
                else if (this.manageInventoryMenuPanel.Enabled)
                {
                    curMenuLevel = this.manageInventoryMenuPanel.MenuController;
                }
                else if (this.cashDrawerMenuPanel.Enabled)
                {
                    curMenuLevel = this.cashDrawerMenuPanel.MenuController;
                }
                else if (this.changePricingMenuPanel.Enabled)
                {
                    curMenuLevel = this.changePricingMenuPanel.MenuController;
                }
                else if (this.gunBookMenuPanel.Enabled)
                {
                    curMenuLevel = this.gunBookMenuPanel.MenuController;
                }
                else if (this.safeOperationsMenuPanel.Enabled)
                {
                    curMenuLevel = this.safeOperationsMenuPanel.MenuController;
                }

                if (curMenuLevel == null)
                    return;
                var cds = GlobalDataAccessor.Instance.DesktopSession;
                if (e.Shift)
                {
                    if (e.KeyCode == Keys.O)
                    {
                        if (cds != null && (cds.LoggedInUserSecurityProfile == null || string.IsNullOrEmpty(cds.LoggedInUserSecurityProfile.UserName)))
                            cds.PerformAuthorization();
                        if (cds != null && cds.LoggedInUserSecurityProfile != null &&
                            cds.LoggedInUserSecurityProfile.ShopLevelUser)
                        {
                            // Launches the shop offset form
                            var offsetForm = new ShopOffset();
                            offsetForm.ShowDialog(this);
                            //Adjust the site id
                            cds.CurrentSiteId.Date = ShopDateTime.Instance.FullShopDateTime;
                            //Adjust the desktop date 
                            cds.UpdateShopDate(this);
                            //Re-load the rules to pick up the latest date
                            cds.GetPawnBusinessRules();
                        }
                        else
                        {
                            MessageBox.Show(
                                    "You do not have sufficient system permission to access official shop time offset functionality.",
                                    "Pawn Security Message",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                        }
                        if (cds != null)
                        {
                            cds.LoggedInUserSecurityProfile = null;
                        }
                        GlobalDataAccessor.Instance.DesktopSession.ClearLoggedInUser();

                    }
                    else if (e.KeyCode == Keys.T)
                    {
                        if (cds != null && (cds.LoggedInUserSecurityProfile == null ||
                            string.IsNullOrEmpty(cds.LoggedInUserSecurityProfile.UserName)))
                        {
                            //Ask the user if they are sure they want to change their password
                            var pwdChangeConfirm =
                                    MessageBox.Show(
                                            "Are you sure you want to change your password?",
                                            "Pawn Security Message",
                                            MessageBoxButtons.YesNoCancel,
                                            MessageBoxIcon.Question);
                            //If they do want to change
                            if (pwdChangeConfirm == DialogResult.Yes)
                            {
                                //Go through the change password process
                                cds.PerformAuthorization(true);
                            }
                        }
                        if (cds != null)
                        {
                            cds.LoggedInUserSecurityProfile = null;
                        }
                        GlobalDataAccessor.Instance.DesktopSession.ClearLoggedInUser();

                        /*if (cds != null && cds.LoggedInUserSecurityProfile != null &&
                            !string.IsNullOrEmpty(cds.LoggedInUserSecurityProfile.UserName))
                        {
                            //Ask the user if they are sure they want to change their password
                            var pwdChangeConfirm =
                                    MessageBox.Show(
                                            "Are you sure you want to change your password?",
                                            "Pawn Security Message",
                                            MessageBoxButtons.YesNoCancel,
                                            MessageBoxIcon.Question);

                            //If they do want to change
                            if (pwdChangeConfirm == DialogResult.Yes)
                            {
                                //Afterwards, generate employee list for barcode printing and
                                //usb key writing
                                string pwd;
                                string displayName;
                                if (PawnLDAPAccessor.Instance.GetUserPassword(
                                    cds.LoggedInUserSecurityProfile.UserName, 
                                    out pwd, out displayName))
                                {
                                    //Generate the encrypted string that goes on the barcode
                                    string encStr = cds.LoggedInUserSecurityProfile.UserName + PawnSecurityAccessor.SEP + pwd;
                                    var sEmployeeFirstName = cds.LoggedInUserSecurityProfile.UserFirstName;
                                    var sEncryptData =
                                            SecurityAccessor.Instance.EncryptConfig.EncryptValue(encStr);
                                    //Acquire intermec printer interface
                                    var intermecBarcodeTagPrint =
                                            new IntermecBarcodeTagPrint("",
                                                Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                                    GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                                                    IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i,
                                                    CashlinxDesktopSession.Instance.IntermecPrinterName);

                                    //Print new bar code
                                    intermecBarcodeTagPrint.PrintUserBarCode(sEncryptData, sEmployeeFirstName);
                                }
                            }
                        }

                        if (cds != null)
                        {
                            cds.LoggedInUserSecurityProfile = null;
                        }
                        CashlinxDesktopSession.Instance.ClearLoggedInUser();*/
                    }
                    else if (e.KeyCode == Keys.R)
                    {
                        //Execute resetting the menu
                        this.resetMenu();
                    }
                    else if (e.KeyCode == Keys.F)
                    {
                        if (cds != null && (cds.LoggedInUserSecurityProfile == null || string.IsNullOrEmpty(cds.LoggedInUserSecurityProfile.UserName)))
                            cds.PerformAuthorization();
                        if (cds != null && cds.LoggedInUserSecurityProfile != null &&
                            cds.LoggedInUserSecurityProfile.ShopLevelUser)
                        {
                            var resets = new Resets();
                            resets.ShowDialog(this);
                        }
                        else
                        {
                            MessageBox.Show(
                                    "You do not have sufficient system permission to access the barcode reset functionality.",
                                    "Pawn Security Message",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                        }
                        if (cds != null)
                        {
                            cds.LoggedInUserSecurityProfile = null;
                        }
                        GlobalDataAccessor.Instance.DesktopSession.ClearLoggedInUser();
                    }

                }
                else
                {
                    curMenuLevel.buttonTriggerHotKey(e.KeyCode);
                }
            }
        }

        /*private void button2_Click(object sender, EventArgs e)
        {
            UserVO currUser = CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile;
            if (currUser == null || (string.IsNullOrEmpty(currUser.UserName)))
            {
                CashlinxDesktopSession.Instance.ClearLoggedInUser();
                CashlinxDesktopSession.Instance.PerformAuthorization();
            }
            if (!string.IsNullOrEmpty(CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile.UserName))
            {
                initializeFunctionality("transferin");
            }
        }*/
    }
}
