using System;
using System.Collections.Generic;
using System.Diagnostics;
using Common.Controllers.Application;
using Common.Controllers.Rules.Data;
using NUnit.Framework;
using PawnTests.TestEnvironment;

namespace PawnTests.BusinessRules
{
    [TestFixture]
    public class OklahomaBusinessRulesTest : BusinessRulesBaseTest
    {
        protected override void Setup()
        {
            GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId = TestSiteIds.Store00604;
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
        public void GetMinMaxLoanAmounts()
        {
            decimal minLoanLimit = 0;
            decimal maxLoanLimit = 0;
            BusinessRulesProcedures.GetMinMaxLoanAmounts(CurrentSiteId, out minLoanLimit, out maxLoanLimit);

            Assert.AreEqual(1M, minLoanLimit);
            Assert.AreEqual(25000M, maxLoanLimit);
        }

        [Test]
        public void IsMultipleDispositionOfCertainRiflesReportPrintedForSite()
        {
            Assert.AreEqual(false, BusinessRulesProcedures.IsMultipleDispositionOfCertainRiflesReportPrintedForSite(CurrentSiteId));
        }

        [Test]
        public void GetPFIEligibleAdjustmentTriggerDays()
        {
            Assert.AreEqual(0, BusinessRulesProcedures.GetPFIEligibleAdjustmentTriggerDays(CurrentSiteId));
        }
        
        [Test]
        public void IsPFIMailersRequiredForState()
        {
            Assert.AreEqual(false, BusinessRulesProcedures.IsPFIMailersRequiredForState(CurrentSiteId));
        }

        [Test]
        public void GetNumberOfDaysToAddForPFIMailer()
        {
            Assert.AreEqual(0, BusinessRulesProcedures.GetNumberOfDaysToAddForPFIMailer(CurrentSiteId));
        }

        [Test]
        public void GetSalesTaxRoundingAdjustment()
        {
            Assert.AreEqual(0M, BusinessRulesProcedures.GetSalesTaxRoundingAdjustment(CurrentSiteId));
        }

        [Test]
        public void IsExtensionAllowed()
        {
            Assert.AreEqual(false, BusinessRulesProcedures.IsExtensionAllowed(CurrentSiteId));
        }

        [Test]
        public void IsRenewalAllowed()
        {
            Assert.AreEqual(true, BusinessRulesProcedures.IsRenewalAllowed(CurrentSiteId));
        }

        [Test]
        public void IsRenewalAtDifferentStoreAllowed()
        {
            Assert.AreEqual(false, BusinessRulesProcedures.IsRenewalAtDifferentStoreAllowed(CurrentSiteId));
        }

        [Test]
        public void GetMinimumHoursInPawn()
        {
            Assert.AreEqual(0, BusinessRulesProcedures.GetMinimumHoursInPawn(CurrentSiteId));
        }

        [Test]
        public void GetMaxItemsForTransfer()
        {
            Assert.AreEqual(25, BusinessRulesProcedures.GetMaxItemsForTransfer(CurrentSiteId));
        }

        [Test]
        public void GetMaxLayawayNumberOfPayments()
        {
            Assert.AreEqual(12, BusinessRulesProcedures.GetMaxLayawayNumberOfPayments(CurrentSiteId));
        }

        [Test]
        public void IsQuickReceiveTransferInAllowed()
        {
            Assert.Inconclusive("Could not find business rule.  Referrred to store tab.  Is this TRIS_QRCV?");
            Assert.AreEqual(true, BusinessRulesProcedures.IsQuickReceiveTransferInAllowed(CurrentSiteId));
        }

        [Test]
        public void GetValidDueDate()
        {
            var currentTime = new DateTime(2012, 11, 25, 10, 24, 0, 0);
            var shopDateTime = new ShopDateTime();

            TimeSpan difference = shopDateTime.FullShopDateTime - currentTime;
            shopDateTime.setOffsets(0, 0, 0, 0, 0, (int)-difference.TotalSeconds, 0);

            DateTime dueDate = new DateTime(2012, 12, 25, 0, 0, 0, 0);
            DateTime expectedDueDate = new DateTime(2012, 12, 25, 0, 0, 0, 0);

            var adjustedDueDate = BusinessRulesProcedures.GetValidDueDate(dueDate, CurrentSiteId);
            //Due date non business day adjustment is not required.  Original due date will always be returned.
            Assert.AreEqual(string.Format("{0:MM/dd/yyyy}", expectedDueDate), adjustedDueDate);
        }

        [Test]
        public void GetMaxPaymentDateLimit()
        {
            Assert.Inconclusive("Blank on spreadsheet.  16 is value returned.");
            Assert.AreEqual(16, BusinessRulesProcedures.GetMaxPaymentDateLimit(CurrentSiteId));
        }

        [Test]
        public void GetWaitDaysForLayawayForfeitureEligibility()
        {
            Assert.AreEqual(0, BusinessRulesProcedures.GetWaitDaysForLayawayForfeitureEligibility(CurrentSiteId));
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
            Assert.Inconclusive("Blank on spreadsheet.  10 is returned.");
            Assert.AreEqual(10, BusinessRulesProcedures.GetNXTItemApprovalAmount(CurrentSiteId));
        }

        [Test]
        public void IsAutoForfeitStoreCreditAllowed()
        {
            Assert.AreEqual(true, BusinessRulesProcedures.IsAutoForfeitStoreCreditAllowed(CurrentSiteId));
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
            var currentTime = new DateTime(2012, 03, 25, 10, 24, 0, 0);
            var shopDateTime = new ShopDateTime();

            TimeSpan difference = shopDateTime.FullShopDateTime - currentTime;
            shopDateTime.setOffsets(0, 0, 0, 0, 0, (int)-difference.TotalSeconds, 0);

            DateTime pawnloanOrigDate = new DateTime(2012, 03, 25, 0, 0, 0, 0);

            string reason = string.Empty;

            var isRestricted = BusinessRulesProcedures.IsRolloverRestricted(CurrentSiteId, "0", "0",pawnloanOrigDate, shopDateTime.FullShopDateTime, ref reason );

            Assert.AreEqual(false, isRestricted);
        }

        [Test]
        public void IsPawnLoanRolloverAllowedBeforeDueDate()
        {
            bool rolloverAllowedBeforeDueDate;
            bool rolloverRenewSameDateMade;
            bool rolloverRestrictPickUpToOriginalCustomer;
            bool rolloverRenewalsBeforeConversionDate;

            var allowed = BusinessRulesProcedures.IsPawnLoanRolloverAllowedBeforeDueDate(CurrentSiteId,
                                                out rolloverAllowedBeforeDueDate,
                                                out rolloverRenewSameDateMade,
                                                out rolloverRestrictPickUpToOriginalCustomer,
                                                out rolloverRenewalsBeforeConversionDate);

            Assert.AreEqual(true, rolloverAllowedBeforeDueDate);
            Assert.AreEqual(false, rolloverRenewSameDateMade);
            Assert.AreEqual(false, rolloverRestrictPickUpToOriginalCustomer);
            Assert.AreEqual(false, rolloverRenewalsBeforeConversionDate);
            Assert.AreEqual(true, allowed);
        }

        [Test]
        public void IsPayDownAllowed()
        {
            Assert.AreEqual(true, BusinessRulesProcedures.IsPayDownAllowed(CurrentSiteId));
        }

        [Test]
        public void IsLoanUpAllowed()
        {
            Assert.AreEqual(true, BusinessRulesProcedures.IsLoanUpAllowed(CurrentSiteId));
        }

        [Test]
        public void IsPartialPaymentAllowed()
        {
            Assert.AreEqual(false, BusinessRulesProcedures.IsPartialPaymentAllowed(CurrentSiteId));
        }

        [Test]
        public void IsExtensionAllowedBeforeDueDate()
        {
            Assert.AreEqual(true, BusinessRulesProcedures.IsExtensionAllowedBeforeDueDate(CurrentSiteId));
        }

        [Test]
        public void IsExtensionAllowedPastPickupAmount()
        {
            Assert.Inconclusive("Spreadsheet says 'No', but true is returned");
            Assert.AreEqual(true, BusinessRulesProcedures.IsExtensionAllowedPastPickupAmount(CurrentSiteId));
        }

        [Test]
        public void IsExtensionAllowedPastRenewalAmount()
        {
            Assert.Inconclusive("Spreadsheet says 'No', but true is returned");
            Assert.AreEqual(true, BusinessRulesProcedures.IsExtensionAllowedPastRenewalAmount(CurrentSiteId));
        }

        [Test]
        public void PFIWaitdays()
        {
            Assert.Inconclusive("Spreadsheet says 0, but 31 is returned.");
            Assert.AreEqual(31, BusinessRulesProcedures.PFIWaitdays(CurrentSiteId));
        }

        [Test]
        public void GetStorageFee()
        {
            Assert.AreEqual(0M, BusinessRulesProcedures.GetStorageFee(CurrentSiteId));
        }

        [Test]
        public void PrintMultipleMemoOfExtension()
        {
            Assert.Inconclusive("Spreadsheet says 'No', but true is returned");
            Assert.AreEqual(true, BusinessRulesProcedures.PrintMultipleMemoOfExtension(CurrentSiteId));
        }

        [Test]
        public void GetStateExtensionTerm()
        {
            Assert.Inconclusive("Spreadsheet says 'N/A'.  'DAILY' is returned.");
            Assert.AreEqual("DAILY", BusinessRulesProcedures.GetStateExtensionTerm(CurrentSiteId));
        }

        [Test]
        public void CanWaiveAutoExtendFee()
        {
            Assert.Inconclusive("Blank in spreadsheet.");
            Assert.AreEqual(false, BusinessRulesProcedures.CanWaiveAutoExtendFee(CurrentSiteId));
        }

        [Test]
        public void CanWaiveGunLockFee()
        {
            Assert.Inconclusive("Blank in spreadsheet");
            Assert.AreEqual(true, BusinessRulesProcedures.CanWaiveGunLockFee(CurrentSiteId));
        }

        [Test]
        public void CanWaiveLateFee()
        {
            Assert.Inconclusive("Blank in spreadsheet");
            Assert.AreEqual(true, BusinessRulesProcedures.CanWaiveLateFee(CurrentSiteId));
        }

        [Test]
        public void CanProrateLateFee()
        {
            Assert.Inconclusive("Blank in spreadsheet");
            Assert.AreEqual(true, BusinessRulesProcedures.CanProrateLateFee(CurrentSiteId));
        }

        [Test]
        public void IsManagerOverrideRequiredForProrateWaive()
        {
            Assert.Inconclusive("Store parameter in spreadsheet.  If I am looking at the right column, then this is 'N' for all stores.  Column 'MGRAPPRREQ_WAIVES'?");
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
            Assert.Inconclusive("Blank in spreadsheet");
            Assert.AreEqual(false, BusinessRulesProcedures.CanWaiveFirearmBackgroundCheckFee(CurrentSiteId));
        }

        [Test]
        public void CanWaiveLostTicketFee()
        {
            Assert.Inconclusive("Blank in spreadsheet");
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
            Assert.AreEqual("18",BusinessRulesProcedures.GetMinimumAgeLongGun(CurrentSiteId));
        }

        [Test]
        public void GetMaxVoidDays()
        {
            Int64 days;

            var success = BusinessRulesProcedures.GetMaxVoidDays(CurrentSiteId, out days);

            Assert.AreEqual(10, days);
            Assert.AreEqual(true, success);
        }

        [Test]
        public void GetTicketName()
        {
            string pawnTicketName;
            var success = BusinessRulesProcedures.GetTicketName(CurrentSiteId, out pawnTicketName);

            Assert.Inconclusive("Could not find rule in spreadsheet.");
            Assert.AreEqual("ticketfmtL.OK", pawnTicketName);
            Assert.AreEqual(true,success);
        }

        [Test]
        public void GetMaxMdseTransferVoidDays()
        {
            Int64 maxVoidDays;
            var success = BusinessRulesProcedures.GetMaxMdseTransferVoidDays(CurrentSiteId, out maxVoidDays);

            Assert.Inconclusive("Strike-through on rule in spreadsheet.");
            Assert.AreEqual(1, maxVoidDays);
            Assert.AreEqual(true, success);
        }

        [Test]
        public void GetMaxLoanLimit()
        {
            decimal maxLoanLimit;
            var success = BusinessRulesProcedures.GetMaxLoanLimit(CurrentSiteId, out maxLoanLimit);

            Assert.AreEqual(25000M, maxLoanLimit);
            Assert.AreEqual(true, success);
        }

        [Test]
        public void GetValidShopRoles()
        {
            List<string> shopRoles;

            var success = BusinessRulesProcedures.GetValidShopRoles(CurrentSiteId, out shopRoles);

            Assert.Inconclusive("I'm guessing that this is a per store test.");
            Assert.AreEqual(8, shopRoles.Count);
            Assert.AreEqual("CSR - I", shopRoles[0]);
            Assert.AreEqual("CSR - II", shopRoles[1]);
            Assert.AreEqual("CSR - III", shopRoles[2]);
            Assert.AreEqual("Shop Manager", shopRoles[3]);
            Assert.AreEqual("Assistant Manager", shopRoles[4]);
            Assert.AreEqual("Regional Vice President", shopRoles[5]);
            Assert.AreEqual("Market Manager", shopRoles[6]);
            Assert.AreEqual("Operations Director", shopRoles[7]);

            Assert.AreEqual(true,success);
        }

        [Test]
        public void GetValidateUserLimit()
        {
            Assert.AreEqual(true, BusinessRulesProcedures.GetValidateUserLimit(CurrentSiteId));
        }

        [Test]
        public void GetMaxCashDrawerLimit()
        {
            decimal maxCDLimit;
            var success = BusinessRulesProcedures.GetMaxCashDrawerLimit(CurrentSiteId, out maxCDLimit);

            Assert.Inconclusive("Blank on spreadsheet.");
            Assert.AreEqual(10000M, maxCDLimit);
            Assert.AreEqual(true, success);
        }

        [Test]
        public void IsSeparateTicketForFirearm()
        {
            Assert.Inconclusive("Blank on spreadsheet.");
            Assert.AreEqual(false, BusinessRulesProcedures.IsSeparateTicketForFirearm(CurrentSiteId));
        }

        [Test]
        public void IsAllowedJewelryGenMDSEOneBuy()
        {
            Assert.Inconclusive("Blank on spreadsheet.");
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
            Assert.Inconclusive("Blank on spreadsheet.  I think this may be PFIMAILDEFADJ on 'store parameters' spreadsheet which has '1' for everystore, but '0' is being returned.");
            Assert.AreEqual(0, BusinessRulesProcedures.GetPfiMailerAdjustmentDays(CurrentSiteId));
        }

        [Test]
        public void GetValidPFIDateWithWaitdaysForBuy()
        {
            var currentTime = new DateTime(2012, 03, 25, 10, 24, 0, 0);
            var shopDateTime = new ShopDateTime();

            TimeSpan difference = shopDateTime.FullShopDateTime - currentTime;
            shopDateTime.setOffsets(0, 0, 0, 0, 0, (int)-difference.TotalSeconds, 0);

            DateTime transactionDate = new DateTime(2012, 03, 25, 0, 0, 0, 0);

            Assert.AreEqual("04/04/2012", BusinessRulesProcedures.GetValidPFIDateWithWaitdaysForBuy(transactionDate, CurrentSiteId));
        }

        [Test]
        public void GetCashDrawerMaxEndingBalance()
        {
            Assert.Inconclusive("Blank on spreadsheet.");
            Assert.AreEqual(10000M, BusinessRulesProcedures.GetCashDrawerMaxEndingBalance(CurrentSiteId));
        }

        [Test]
        public void GetCustomerInfoRequiredSaleValue()
        {
            Assert.AreEqual(10M, BusinessRulesProcedures.GetCustomerInfoRequiredSaleValue(CurrentSiteId));
        }

        [Test]
        public void GetCustomerSurveyCardSaleTransactions()
        {
            Assert.AreEqual(1, BusinessRulesProcedures.GetCustomerSurveyCardSaleTransactions(CurrentSiteId));
        }

        [Test]
        public void IsServiceFeeTaxable()
        {
            Assert.AreEqual(false, BusinessRulesProcedures.IsServiceFeeTaxable(CurrentSiteId));
        }

        [Test]
        public void CustomerSignatureOnBackofTicket()
        {
            Assert.Inconclusive("Blank on spreadsheet");
            Assert.AreEqual(false, BusinessRulesProcedures.CustomerSignatureOnBackofTicket(CurrentSiteId));
        }

        [Test]
        public void GetPoliceHoldDefaultDays()
        {
            Assert.Inconclusive("Blank on spreadsheet");
            Assert.AreEqual(180, BusinessRulesProcedures.GetPoliceHoldDefaultDays(CurrentSiteId));
        }

        [Test]
        public void IsPartialPaymentAllowedAfterDueDate()
        {
            //Partial payment not allowed for OK
            Assert.AreEqual(false, BusinessRulesProcedures.IsPartialPaymentAllowedAfterDueDate(CurrentSiteId));
        }

        [Test]
        public void IsPartialPaymentAllowedAfterPfiDate()
        {

            //Partial payment not allowed for OK
            Assert.AreEqual(false, BusinessRulesProcedures.IsPartialPaymentAllowedAfterPfiDate(CurrentSiteId));
        }

        [Test]
        public void GetPartialPaymentMinAmount()
        {
            //Partial payment not allowed for OK
            Assert.AreEqual(0M, BusinessRulesProcedures.GetPartialPaymentMinAmount(CurrentSiteId));
        }

        [Test]
        public void ShouldPrintPickupReceipt()
        {
            Assert.Inconclusive("Spreadsheet says 'Yes', but return is false");
            Assert.AreEqual(false, BusinessRulesProcedures.ShouldPrintPickupReceipt(CurrentSiteId));
        }

        [Test]
        public void GetStoreMinimumIntCharge()
        {
            Assert.Inconclusive("Spreadsheet says '1', but return is 15");
            Assert.AreEqual(15M, BusinessRulesProcedures.GetStoreMinimumIntCharge());
        }

        [Test]
        public void GetValidDueDateFromPFIDate()
        {
            var currentTime = new DateTime(2012, 11, 25, 10, 24, 0, 0);
            var shopDateTime = new ShopDateTime();

            TimeSpan difference = shopDateTime.FullShopDateTime - currentTime;
            shopDateTime.setOffsets(0, 0, 0, 0, 0, (int)-difference.TotalSeconds, 0);

            DateTime dueDate = new DateTime(2012, 12, 25, 0, 0, 0, 0);
            DateTime expectedDate = new DateTime(2012, 11, 24, 0, 0, 0, 0);

            var pfiDate = BusinessRulesProcedures.GetValidDueDateFromPFIDate(dueDate, CurrentSiteId);
            Assert.Inconclusive(@"PFI wait days is being subtracted from due date.  Spreadsheet says PFI wait days should be 0, but 31 days are being subtracted.");
            Assert.AreEqual(string.Format("{0:MM/dd/yyyy}", expectedDate), pfiDate);            
        }

        [Test]
        public void GetValidPFIDateWithWaitdays()
        {
            var currentTime = new DateTime(2012, 11, 25, 10, 24, 0, 0);
            var shopDateTime = new ShopDateTime();

            TimeSpan difference = shopDateTime.FullShopDateTime - currentTime;
            shopDateTime.setOffsets(0, 0, 0, 0, 0, (int)-difference.TotalSeconds, 0);

            DateTime dueDate = new DateTime(2012, 12, 25, 0, 0, 0, 0);
            DateTime expectedDate = new DateTime(2013, 1, 25, 0, 0, 0, 0);

            bool dateAdjusted = false;

            var pfiDate = BusinessRulesProcedures.GetValidPFIDateWithWaitdays(dueDate, CurrentSiteId, ref dateAdjusted);
            Assert.Inconclusive(@"Spreadsheet says '0' for PFI wait days, but 31 days are being added.");
            Assert.AreEqual(string.Format("{0:MM/dd/yyyy}", expectedDate), pfiDate);   
            Assert.AreEqual(false, dateAdjusted);
        }

        [Test]
        public void GetValidPFIDate()
        {
            var currentTime = new DateTime(2012, 11, 25, 10, 24, 0, 0);
            var shopDateTime = new ShopDateTime();

            TimeSpan difference = shopDateTime.FullShopDateTime - currentTime;
            shopDateTime.setOffsets(0, 0, 0, 0, 0, (int)-difference.TotalSeconds, 0);

            DateTime dueDate = new DateTime(2012, 12, 25, 0, 0, 0, 0);
            DateTime expectedDate = new DateTime(2012, 12, 25, 0, 0, 0, 0);

            bool dateAdjusted = false;

            var pfiDate = BusinessRulesProcedures.GetValidPFIDate(dueDate, CurrentSiteId, ref dateAdjusted);
            Assert.Inconclusive(@"Doesn't look like the date will ever get adjusted because rules.xml has 'N' for CL_PWN_0093_DUEDATENBDADJTREQ");
            Assert.AreEqual(string.Format("{0:MM/dd/yyyy}", expectedDate), pfiDate);
            Assert.AreEqual(false, dateAdjusted);
        }

        [Test]
        public void IsPickupRestricted()
        {
            var currentTime = new DateTime(2012, 11, 25, 10, 24, 0, 0);
            var shopDateTime = new ShopDateTime();

            TimeSpan difference = shopDateTime.FullShopDateTime - currentTime;
            shopDateTime.setOffsets(0, 0, 0, 0, 0, (int)-difference.TotalSeconds, 0);

            DateTime dueDate = new DateTime(2012, 12, 25, 0, 0, 0, 0);
            DateTime expectedDate = new DateTime(2012, 12, 25, 0, 0, 0, 0);

            string reason = "";

            var isResticted = BusinessRulesProcedures.IsPickupRestricted(CurrentSiteId,"0","0", dueDate, shopDateTime.FullShopDateTime, ref reason);
           
            Assert.AreEqual(false, isResticted);
            Assert.AreEqual("", reason);
        }

        [Test]
        public void GetMinimumPaymentDateLimit()
        {
            Assert.Inconclusive("Blank on spreadsheet");
            Assert.AreEqual(14, BusinessRulesProcedures.GetMinimumPaymentDateLimit(CurrentSiteId));
        }
    }
}
