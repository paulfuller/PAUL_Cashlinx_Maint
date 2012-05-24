using System;
using System.Collections.Generic;
using System.Diagnostics;
using Common.Controllers.Application;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
using NUnit.Framework;
using PawnTests.TestEnvironment;

namespace PawnTests.BusinessRules
{
    [TestFixture]
    
    public class TexasBusinessRulesTest : BusinessRulesBaseTest
    {
        protected override void Setup()
        {
            GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId = TestSiteIds.Store00152;
        }

        protected override void Teardown()
        {
            GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId = TestSiteIds.Store00152;
        }

       [Test]
        public void AllowFutureInterestPayments()
        {
            Assert.IsFalse(BusinessRulesProcedures.AllowFutureInterestPayments(CurrentSiteId));
        }

        [Test]
        public void GetPFIMailerFee()
        {
            Assert.AreEqual(0M, BusinessRulesProcedures.GetPFIMailerFee(CurrentSiteId));
        }

        [Test]
        public void GetPFIEligibleDateAdjustmentDirection()
        {
            Assert.AreEqual("F", BusinessRulesProcedures.GetPFIEligibleDateAdjustmentDirection(CurrentSiteId));
        }

        [Test]
        public void GetSalesTaxRoundingAdjustment()
        {
            Assert.AreEqual(0M, BusinessRulesProcedures.GetSalesTaxRoundingAdjustment(CurrentSiteId));
        }

        [Test]
        public void GetMaxLoanLimit()
        {
            decimal maxloanlimit = new decimal();
            Assert.AreEqual(true, BusinessRulesProcedures.GetMaxLoanLimit(CurrentSiteId, out maxloanlimit));
        }

        [Test]
        public void GetMinMaxLoanAmounts()
        {
            decimal minamt = new decimal();
            decimal maxamt = new decimal();
            Assert.AreEqual(true, BusinessRulesProcedures.GetMinMaxLoanAmounts(CurrentSiteId, out minamt, out maxamt));
        }

        [Test]
        public void IsMultipleDispositionOfCertainRiflesReportPrintedForSite()
        {
            Assert.AreEqual(true,BusinessRulesProcedures.IsMultipleDispositionOfCertainRiflesReportPrintedForSite(CurrentSiteId));
        }

        [Test]
        public void GetPFIEligibleAdjustmentTriggerDays()
        {
            Assert.AreEqual(0, BusinessRulesProcedures.GetPFIEligibleAdjustmentTriggerDays(CurrentSiteId));
        }

        [Test]
        public void IsPFIMaialersRequiredForState()
        {
            Assert.IsFalse(BusinessRulesProcedures.IsPFIMailersRequiredForState(CurrentSiteId));
        }

        [Test]
        public void GetNumberOfDaysToAddForPFIMailer()
        {
         
            Assert.AreEqual(0, BusinessRulesProcedures.GetNumberOfDaysToAddForPFIMailer(CurrentSiteId));
        }

        [Test]
        public void ShouldPrintPickupReceipt()
        {

            Assert.AreEqual(false, BusinessRulesProcedures.ShouldPrintPickupReceipt(CurrentSiteId));

        }

        [Test]
        public void GetStoreMinimumIntCharge()
        {

            Assert.AreEqual(15m, BusinessRulesProcedures.GetStoreMinimumIntCharge());

        }

        //[Test]
        //public void GetValidDueDate()
        //{
        //    ShopDateTime duedate = new ShopDateTime();
            
        //    var result = duedate.FullShopDateTime.Date.ToString("MM/dd/yyyy");
        //    // Assert.AreEqual("04/03/2012", BusinessRulesProcedures.GetValidDueDate(duedate.FullShopDateTime, CurrentSiteId));
        //    Assert.AreEqual(result, BusinessRulesProcedures.GetValidDueDate(duedate.FullShopDateTime, CurrentSiteId));

        //}

        //[Test]
        //public void GetValidDueDateFromPFIDate()
        //{
        //    ShopDateTime duedate = new ShopDateTime();
        //    var result = duedate.FullShopDateTime.AddDays(-32).Date.ToString("MM/dd/yyyy");
        //  // Assert.AreEqual("03/02/2012", BusinessRulesProcedures.GetValidDueDateFromPFIDate(duedate.FullShopDateTime, CurrentSiteId));
        //    Assert.AreEqual(result,BusinessRulesProcedures.GetValidDueDateFromPFIDate(duedate.FullShopDateTime,CurrentSiteId));
        //}
        
        
        //[Test]
        //public void GetValidPFIDateWithWaitdays()
        //{
        //    ShopDateTime pfiDate = new ShopDateTime();
        //    var result = pfiDate.FullShopDateTime.AddDays(32).Date.ToString("MM/dd/yyyy");
        //    var DateAdjusted = false;
        //   // Assert.AreEqual("05/05/2012",BusinessRulesProcedures.GetValidPFIDateWithWaitdays(pfiDate.FullShopDateTime,CurrentSiteId,ref DateAdjusted));
        //    Assert.AreEqual(result,BusinessRulesProcedures.GetValidPFIDateWithWaitdays(pfiDate.FullShopDateTime,CurrentSiteId,ref DateAdjusted));
        //}
        
        //[Test]
        //public void GetValidPFIDate()
        //{
            
        //   ShopDateTime pfiDate = new ShopDateTime();
        //   var result = pfiDate.FullShopDateTime.ToString("MM/dd/yyyy");
        //   var DateAdjusted = false;
        //  //  Assert.AreEqual("04/03/2012",BusinessRulesProcedures.GetValidPFIDate(pfiDate.FullShopDateTime.Date, CurrentSiteId, ref DateAdjusted));
        //   Assert.AreEqual(result,BusinessRulesProcedures.GetValidPFIDate(pfiDate.FullShopDateTime,CurrentSiteId,ref DateAdjusted));
        //}
        
        [Test]
        public void IsPickupRestricted()
        {

            var reason = string.Empty;
            ShopDateTime currentshopdate = new ShopDateTime();
            ShopDateTime pawnloanorigdate = new ShopDateTime();
            string pawnLoanCustomerNumber = string.Empty;
            string currentCustomerNumber = string.Empty;
            Assert.AreEqual(false, BusinessRulesProcedures.IsPickupRestricted(CurrentSiteId, currentCustomerNumber, pawnLoanCustomerNumber, pawnloanorigdate.FullShopDateTime, currentshopdate.FullShopDateTime, ref reason));
     
        }

        [Test]
        public void IsExtensionAllowed()
        {

            Assert.AreEqual(true, BusinessRulesProcedures.IsExtensionAllowed(CurrentSiteId));

        }

        [Test]
        public void IsRenewalAllowed()
        {

            Assert.AreEqual(true, BusinessRulesProcedures.IsRenewalAllowed(CurrentSiteId));

        }

        [Test]
        public void IsRenewalAtDifferentStoreAllowed()
        {
        Assert.AreEqual(false,BusinessRulesProcedures.IsRenewalAtDifferentStoreAllowed(CurrentSiteId));
        
        }

        [Test]
        public void GetMinimumHoursInPawn()
        {
            Assert.AreEqual(0, BusinessRulesProcedures.GetMinimumHoursInPawn(CurrentSiteId));

        }

        [Test]
        public void GetMaxItemsForTransfer()
        {

           Assert.AreEqual(25,BusinessRulesProcedures.GetMaxItemsForTransfer(CurrentSiteId));

        }

        [Test]
        public void GetMaxLayawayNumberOfPayments()
        {

            Assert.AreEqual(12, BusinessRulesProcedures.GetMaxLayawayNumberOfPayments(CurrentSiteId));

        }

        [Test]
        public void GetMinimumPaymentDateLimit()
        {
            Assert.AreEqual(14, BusinessRulesProcedures.GetMinimumPaymentDateLimit(CurrentSiteId));

        }

        [Test]
        public void IsQuickReceiveTransferInAllowed()
        {
         // need to check
        Assert.AreEqual(true,BusinessRulesProcedures.IsQuickReceiveTransferInAllowed(CurrentSiteId));

        }

        [Test]
        public void GetMaxPaymentDateLimit()
        {
            Assert.AreEqual(16, BusinessRulesProcedures.GetMaxPaymentDateLimit(CurrentSiteId));

        }
        [Test]
        public void GetWaitDaysForLayawayForfeitureEligibility()
        {
          //No need of this method(According to BR)
            Assert.AreEqual(0,BusinessRulesProcedures.GetWaitDaysForLayawayForfeitureEligibility(CurrentSiteId));
        
        }

        [Test]
        public void GetLayawayRestockingFee()
        {

            Assert.AreEqual(10M, BusinessRulesProcedures.GetLayawayRestockingFee(CurrentSiteId));


        }

        [Test]
        public void GetMaxDaysForRefundEligibility()
        {

            Assert.AreEqual(180, BusinessRulesProcedures.GetMaxDaysForRefundEligibility(CurrentSiteId));

        }

        [Test]
        public void GetNXTItemApprovalAmount()
        {
            Assert.Greater(12, BusinessRulesProcedures.GetNXTItemApprovalAmount(CurrentSiteId));

        }

        [Test]
        public void IsAutoForfeitStoreCreditAllowed()
        {

            Assert.AreEqual(true,BusinessRulesProcedures.IsAutoForfeitStoreCreditAllowed(CurrentSiteId));

        }

        [Test]
        public void GetSaleStoreCreditDuration()
        {
            Assert.AreEqual(180, BusinessRulesProcedures.GetSaleStoreCreditDuration(CurrentSiteId));


        }

        [Test]
        public void GetLayStoreCreditDuration()
        {
            Assert.AreEqual(180, BusinessRulesProcedures.GetLayStoreCreditDuration(CurrentSiteId));

        }

        [Test]
        public void IsRolloverRestricted()
        {
            ShopDateTime pwnloanOriginalDate = new ShopDateTime();
            var reason = string.Empty;
            string pawnLoanCustomerNumber = string.Empty;
            string currentCustomerNumber = string.Empty;
            ShopDateTime currentdate = new ShopDateTime();
            Assert.AreEqual(false, BusinessRulesProcedures.IsRolloverRestricted(CurrentSiteId, currentCustomerNumber, pawnLoanCustomerNumber, pwnloanOriginalDate.FullShopDateTime, currentdate.FullShopDateTime, ref reason));

        }

        [Test]
        public void IsPawnLoanRolloverAllowedBeforeDueDate()
        {
            bool rolloverAllowedBeforeDueDate;
            bool rolloverRenewSameDateMade;
            bool rolloverRestrictPickUpToOriginalCustomer;
            bool rolloverRenewalsBeforeConversionDate;
            Assert.AreEqual(true, BusinessRulesProcedures.IsPawnLoanRolloverAllowedBeforeDueDate(CurrentSiteId, out rolloverAllowedBeforeDueDate, out rolloverRenewSameDateMade, out rolloverRestrictPickUpToOriginalCustomer, out rolloverRenewalsBeforeConversionDate));
        }

        [Test]
        public void IsPayDownAllowed()
        {
            //see store parameters
            Assert.AreEqual(true, BusinessRulesProcedures.IsPayDownAllowed(CurrentSiteId));

        }

        [Test]
        public void IsLoanUpAllowed()
        {
            Assert.IsTrue(BusinessRulesProcedures.IsLoanUpAllowed(CurrentSiteId));

        }

        [Test]
        public void IsPartialPaymentAllowed()
        {
            Assert.AreEqual(false,BusinessRulesProcedures.IsPartialPaymentAllowed(CurrentSiteId));

        }


        [Test]
        public void IsExtensionAllowedBeforeDueDate()
        {
            Assert.AreEqual(true, BusinessRulesProcedures.IsExtensionAllowedBeforeDueDate(CurrentSiteId));
        }
        [Test]
        public void IsExtensionAllowedPastPickupAmount()
        {
            //see store parameters tab
            Assert.AreEqual(true, BusinessRulesProcedures.IsExtensionAllowedPastPickupAmount(CurrentSiteId));

        }

        [Test]
        public void IsExtensionAllowedPastRenewalAmount()
        {
            //see store parameters tab
            Assert.AreEqual(true, BusinessRulesProcedures.IsExtensionAllowedPastRenewalAmount(CurrentSiteId));

        }

        [Test]
        public void PFIWaitdays()
        {
            Assert.AreEqual(32, BusinessRulesProcedures.PFIWaitdays(CurrentSiteId));

        }

        [Test]
        public void GetStorageFee()
        {
            Assert.AreEqual(0m, BusinessRulesProcedures.GetStorageFee(CurrentSiteId));

        }

        [Test]
        public void PrintMultipleMemoOfExtension()
        {
            Assert.AreEqual(true, BusinessRulesProcedures.PrintMultipleMemoOfExtension(CurrentSiteId));

        }

        [Test]
        public void GetStateExtensionTerm()
        {

            Assert.AreEqual("DAILY", BusinessRulesProcedures.GetStateExtensionTerm(CurrentSiteId));
        }

        [Test]
        public void CanWaiveAutoExtendFee()
        {
            Assert.AreEqual(false, BusinessRulesProcedures.CanWaiveAutoExtendFee(CurrentSiteId));

        }

        [Test]
        public void CanWaiveGunLockFee()
        {
            Assert.AreEqual(true, BusinessRulesProcedures.CanWaiveGunLockFee(CurrentSiteId));

        }

        [Test]
        public void CanWaiveLateFee()
        {
            //need store param
            Assert.AreEqual(true, BusinessRulesProcedures.CanWaiveLateFee(CurrentSiteId));

        }

        [Test]
        public void CanProrateLateFee()
        {
            //need store param
            Assert.AreEqual(true, BusinessRulesProcedures.CanProrateLateFee(CurrentSiteId));

        }

        [Test]
        public void IsManagerOverrideRequiredForProrateWaive()
        {
            //need store param
            Assert.AreEqual(true, BusinessRulesProcedures.IsManagerOverrideRequiredForProrateWaive(CurrentSiteId));

        }
        
        [Test]
        public void CanWaiveStorageFee()
        {
            Assert.AreEqual(false, BusinessRulesProcedures.CanWaiveStorageFee(CurrentSiteId));

        }

        [Test]
        public void CanWaiveFirearmBackgroundCheckFee()
        {
            Assert.AreEqual(false, BusinessRulesProcedures.CanWaiveFirearmBackgroundCheckFee(CurrentSiteId));

        }

        [Test]
        public void CanWaiveLostTicketFee()
        {
            //include in BR
            Assert.AreEqual(false, BusinessRulesProcedures.CanWaiveLostTicketFee(CurrentSiteId));

        }

        [Test]
        public void GetMinimumAgeHandGun()
        {
            Assert.AreEqual("21", BusinessRulesProcedures.GetMinimumAgeHandGun(CurrentSiteId));

        }
        
        [Test]
        public void GetMinimumAgeLongGun()
        {
            Assert.AreEqual("18", BusinessRulesProcedures.GetMinimumAgeLongGun(CurrentSiteId));

        }

        [Test]
        public void GetMaxVoidDay()
        {
            var maxVoidDays = new long();
            Assert.AreEqual(true, BusinessRulesProcedures.GetMaxVoidDays(CurrentSiteId, out maxVoidDays));

        }

        [Test]
        public void GetTicketName()
        {
            string pawnTicketName = string.Empty;
            Assert.AreEqual(true, BusinessRulesProcedures.GetTicketName(CurrentSiteId, out pawnTicketName));

        }
        
        [Test]
        public void GetMaxMdseTransferVoidDay()
        {
            //strike in BR
            var maxVoidDays = new long();
            Assert.AreEqual(true, BusinessRulesProcedures.GetMaxMdseTransferVoidDays(CurrentSiteId, out maxVoidDays));

        }

        [Test]
        public void GetValidateUserLimit()
        {
            Assert.AreEqual(true, BusinessRulesProcedures.GetValidateUserLimit(CurrentSiteId));

        }

        [Test]
        public void GetValidShopRoles()
        {
            var shopRoles = new List<string>();
            Assert.AreEqual(true, BusinessRulesProcedures.GetValidShopRoles(CurrentSiteId, out shopRoles));

        }

        [Test]
        public void GetMaxCashDrawerLimit()
        {
            //see store param
            decimal maxCDLimit = new decimal();
            Assert.AreEqual(true, BusinessRulesProcedures.GetMaxCashDrawerLimit(CurrentSiteId, out maxCDLimit));

        }

        [Test]
        public void IsSeparateTicketForFirearm()
        {
            Assert.AreEqual(false, BusinessRulesProcedures.IsSeparateTicketForFirearm(CurrentSiteId));

        }

        [Test]
        public void IsAllowedJewelryGenMDSEOneBuy()
        {
            Assert.AreEqual(true, BusinessRulesProcedures.IsAllowedJewelryGenMDSEOneBuy(CurrentSiteId));

        }

        [Test]
        public void GetMaxItemsInBuyTransaction()
        {
            Assert.AreEqual(999, BusinessRulesProcedures.GetMaxItemsInBuyTransaction(CurrentSiteId));

        }

        [Test]
        public void GetPfiMailerAdjustmentDays()
        {
            Assert.AreEqual(0, BusinessRulesProcedures.GetPfiMailerAdjustmentDays(CurrentSiteId));

        }

        //[Test]
        //public void GetValidPFIDateWithWaitdaysForBuy()
        //{
        //    ShopDateTime transactionDate = new ShopDateTime();
        //    var result = transactionDate.FullShopDateTime.AddDays(21).Date.ToString("MM/dd/yyyy");
        //  // Assert.AreEqual("04/24/2012", BusinessRulesProcedures.GetValidPFIDateWithWaitdaysForBuy(transactionDate.FullShopDateTime, CurrentSiteId));
        //    Assert.AreEqual(result, BusinessRulesProcedures.GetValidPFIDateWithWaitdaysForBuy(transactionDate.FullShopDateTime, CurrentSiteId));
            
        //}

        [Test]
        public void GetCashDrawerMaxEndingBalance()
        {
            //In BR it was 90..need to check
            Assert.AreEqual(10000m, BusinessRulesProcedures.GetCashDrawerMaxEndingBalance(CurrentSiteId));

        }

        [Test]
        public void GetCustomerInfoRequiredSaleValue()
        {
            Assert.AreEqual(10m, BusinessRulesProcedures.GetCustomerInfoRequiredSaleValue(CurrentSiteId));

        }

      
        [Test]
        public void GetCustomerSurveyCardSaleTransactions()
        {
            //need to check store parameters
            Assert.AreEqual(4, BusinessRulesProcedures.GetCustomerSurveyCardSaleTransactions(CurrentSiteId));

        }

        [Test]
        public void IsServiceFeeTaxable()
        {
            Assert.AreEqual(true, BusinessRulesProcedures.IsServiceFeeTaxable(CurrentSiteId));

        }

        [Test]
        public void CustomerSignatureOnBackofTicket()
        {
            Assert.AreEqual(false, BusinessRulesProcedures.CustomerSignatureOnBackofTicket(CurrentSiteId));

        }

        [Test]
        public void GetPoliceHoldDefaultDay()
        {
            //need to include in BR
            Assert.AreEqual(180, BusinessRulesProcedures.GetPoliceHoldDefaultDays(CurrentSiteId));

        }

        [Test]
        public void IsPartialPaymentAllowedAfterDueDate()
        {
            Assert.IsFalse(BusinessRulesProcedures.IsPartialPaymentAllowedAfterDueDate(CurrentSiteId));

        }

        [Test]
        public void IsPartialPaymentAllowedAfterPfiDate()
        {
            Assert.IsFalse(BusinessRulesProcedures.IsPartialPaymentAllowedAfterPfiDate(CurrentSiteId));

        }

        [Test]
        public void GetPartialPaymentMinAmount()
        {
            Assert.AreEqual(0,BusinessRulesProcedures.GetPartialPaymentMinAmount(CurrentSiteId));

        }

   }
}

