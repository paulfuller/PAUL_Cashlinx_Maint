using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
using NUnit.Framework;
using PawnTests.TestEnvironment;

namespace PawnTests.BusinessRules
{
    class IndianaBusinessRulesTest : BusinessRulesBaseTest
    {
        protected override void Setup()
        {
            GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId = TestSiteIds.Store01401;

        }

        protected override void Teardown()
        {
            GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId = TestSiteIds.Store00152;

        }

//        [Test]
//        public void TestPrintingReceiptSample()
//        {
            
//        }

//        //[Test]
//        //public void PrintBASample()
//        //{
//        //    var ds = GlobalDataAccessor.Instance.DesktopSession;
//        //    ds.CurrentSiteId = TestSiteIds.Store00901;

//        //    pfiMailer.customerAddress = "100 OHIO STREET";
//        //    pfiMailer.customerCity = "CINCINNATI";
//        //    pfiMailer.customerId = "XXXXXXXX";
//        //    pfiMailer.customerName = "WAGES, JOHN Q";
//        //    pfiMailer.customerNumber = 99999;
//        //    pfiMailer.customerState = "OH";
//        //    pfiMailer.customerZipCode = "45239";
//        //    pfiMailer.pfiEligibleDate = DateTime.Parse("03/19/2012");
//        //    pfiMailer.pfiMailerFee = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetPFIMailerFee(
//        //                GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);
//        //    pfiMailer.storeAddress = ds.CurrentSiteId.StoreAddress1;
//        //    pfiMailer.storeCity = ds.CurrentSiteId.StoreCityName;
//        //    pfiMailer.storeName = ds.CurrentSiteId.StoreName;
//        //    pfiMailer.storeNumber = 0;
//        //    pfiMailer.storePhone = ds.CurrentSiteId.StorePhoneNo;
//        //    pfiMailer.storeState = ds.CurrentSiteId.State;
//        //    pfiMailer.storeZipCode = ds.CurrentSiteId.StoreZipCode;
//        //    pfiMailer.ticketNumber = 287549;

//        //    pfiMailerDocument.CreateReport(pfiMailer);

//        //    //Uncomment to print document
//        //    //var strReturnMessage =
//        //    //    PrintingUtilities.printDocument(
//        //    //        reportObject.ReportTempFileFullName,
//        //    //        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
//        //    //        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);

//        //    //Assert.IsTrue(strReturnMessage.Contains("SUCCESS"));
//        //}



//        [Test]
//        public void AllowFutureInterestPayments()
//        {
//            Assert.IsFalse(BusinessRulesProcedures.AllowFutureInterestPayments(CurrentSiteId));
//        }

//        [Test]
//        public void GetPFIMailerFee()
//        {
//            Assert.AreEqual(2M, BusinessRulesProcedures.GetPFIMailerFee(CurrentSiteId));
//        }

//        [Test]
//        public void GetPFIEligibleDateAdjustmentDirection()
//        {
//            Assert.AreEqual("F", BusinessRulesProcedures.GetPFIEligibleDateAdjustmentDirection(CurrentSiteId));
//        }

//        [Test]
//        public void GetSalesTaxRoundingAdjustment()
//        {
//            Assert.AreEqual(0.00499M, BusinessRulesProcedures.GetSalesTaxRoundingAdjustment(CurrentSiteId));
//        }

//        [Test]
//        public void GetStoreMinimumIntCharge()
//        {
//#warning - This should be 0
//            Assert.AreEqual(15M, BusinessRulesProcedures.GetStoreMinimumIntCharge());
//        }

//        [Test]
//        public void IsMultipleDispositionOfCertainRiflesReportPrintedForSite()
//        {
//            Assert.IsFalse(BusinessRulesProcedures.IsMultipleDispositionOfCertainRiflesReportPrintedForSite(CurrentSiteId));
//        }

//        [Test]
//        public void GetPFIEligibleAdjustmentTriggerDays()
//        {
//            Assert.AreEqual(30, BusinessRulesProcedures.GetPFIEligibleAdjustmentTriggerDays(CurrentSiteId));
//        }

//        [Test]
//        public void IsPFIMailersRequiredForState()
//        {
//            Assert.IsTrue(BusinessRulesProcedures.IsPFIMailersRequiredForState(CurrentSiteId));
//        }

//        [Test]
//        public void GetNumberOfDaysToAddForPFIMailer()
//        {
//#warning - This should be 1
//            Assert.AreEqual(0, BusinessRulesProcedures.GetNumberOfDaysToAddForPFIMailer(CurrentSiteId));
//        }

//        [Test]
//        public void ShouldPrintPickupReceipt()
//        {
//            Assert.IsTrue(BusinessRulesProcedures.ShouldPrintPickupReceipt(CurrentSiteId));
//        }

//        [Test]
//        public void GetValidDueDate()
//        {
//#warning GetValidDueDate() -> Not sure how to test this.
//            // Ohio due dates adjust backwards according to business rule in excel
//            DateTime dateToCheck = new DateTime(2012, 07, 13);

//            BusinessRulesProcedures.GetValidDueDate(dateToCheck, CurrentSiteId);
//            Assert.Inconclusive("Not sure how to test this.");
//        }

//        [Test]
//        public void GetValidDueDateFromPFIDate()
//        {
//#warning GetValidDueDateFromPFIDate() -> Not sure how to test this.
//            DateTime dateToCheck = new DateTime(2012, 07, 13);

//            BusinessRulesProcedures.GetValidDueDateFromPFIDate(dateToCheck, CurrentSiteId);
//            Assert.Inconclusive("Not sure how to test this.");
//            Assert.IsTrue(true);
//        }

//        [Test]
//        public void GetValidPFIDateWithWaitdays()
//        {
//#warning GetValidPFIDateWithWaitdays() do not know how to test
//            bool dateAdjusted = false;
//            string PFIDate = BusinessRulesProcedures.GetValidPFIDateWithWaitdays(DateTime.Now, CurrentSiteId, ref dateAdjusted);
//            Assert.IsTrue(true);
//        }

//        [Test]
//        public void GetValidPFIDate()
//        {
//#warning GetValidPFIDate() -> need help with test.
//            DateTime dateToCheck = new DateTime(2012, 07, 13);
//            bool dateAdjusted = false;
//            BusinessRulesProcedures.GetValidPFIDate(dateToCheck, CurrentSiteId, ref dateAdjusted);
//            Assert.IsTrue(true);
//        }

//        [Test]
//        public void IsPickupRestricted()
//        {
//#warning IsPickupRestricted() -> need help with test.

//            string currentCustomerNumber = string.Empty;
//            string pawnloanCustomerNumber = string.Empty;
//            DateTime pawnloanOrigDate = DateTime.Today;
//            DateTime currentShopDate = DateTime.Today;
//            string reason = string.Empty;
//            BusinessRulesProcedures.IsPickupRestricted(
//                CurrentSiteId, currentCustomerNumber, pawnloanCustomerNumber, pawnloanOrigDate, currentShopDate, ref reason);
//            Assert.Inconclusive("need help with this test.");
//            Assert.IsTrue(true);
//        }

//        [Test]
//        public void IsExtensionAllowed()
//        {
//            Assert.IsTrue(BusinessRulesProcedures.IsExtensionAllowed(CurrentSiteId));
//        }

//        [Test]
//        public void IsRenewalAllowed()
//        {
//            Assert.IsFalse(BusinessRulesProcedures.IsRenewalAllowed(CurrentSiteId));
//        }

//        [Test]
//        public void IsRenewalAtDifferentStoreAllowed()
//        {
//#warning IsRenewalAtDifferentStoreAllowed() is hardcoded to false. No business rule checked.
//            Assert.Inconclusive("IsRenewalAtDifferentStoreAllowed() is hardcoded to false. No business rule checked.");
//            Assert.IsFalse(BusinessRulesProcedures.IsRenewalAtDifferentStoreAllowed(CurrentSiteId));
//        }

//        [Test]
//        public void GetMinimumHoursInPawn()
//        {
//            Assert.AreEqual(72, BusinessRulesProcedures.GetMinimumHoursInPawn(CurrentSiteId));
//        }

//        [Test]
//        public void GetMaxItemsForTransfer()
//        {
//#warning Should this business rule be CL-PWN-220 (222 is Scrap).
//            Assert.AreEqual(25, BusinessRulesProcedures.GetMaxItemsForTransfer(CurrentSiteId));
//        }

//        [Test]
//        public void GetMaxLayawayNumberOfPayments()
//        {
//            Assert.AreEqual(12, BusinessRulesProcedures.GetMaxLayawayNumberOfPayments(CurrentSiteId));
//        }

//        [Test]
//        public void GetMinimumPaymentDateLimit()
//        {
//#warning GetMinimumPaymentDateLimit() - No setting for this in Excel business rules.
//            Assert.AreEqual(14, BusinessRulesProcedures.GetMinimumPaymentDateLimit(CurrentSiteId));
//        }

//        [Test]
//        public void IsQuickReceiveTransferInAllowed()
//        {
//            Assert.IsTrue(BusinessRulesProcedures.IsQuickReceiveTransferInAllowed(CurrentSiteId));
//        }

//        [Test]
//        public void GetMaxPaymentDateLimit()
//        {
//#warning GetMaxPaymentDateLimit() - This is not set in business rules
//            Assert.AreEqual(16, BusinessRulesProcedures.GetMaxPaymentDateLimit(CurrentSiteId));
//        }

//        [Test]
//        public void GetWaitDaysForLayawayForfeitureEligibility()
//        {
//            // crossed out. Not needed?
//            Assert.AreEqual(0, BusinessRulesProcedures.GetWaitDaysForLayawayForfeitureEligibility(CurrentSiteId));
//        }

//        [Test]
//        public void GetLayawayRestockingFee()
//        {
//            Assert.AreEqual(10F, BusinessRulesProcedures.GetLayawayRestockingFee(CurrentSiteId));
//        }

//        [Test]
//        public void GetMaxDaysForRefundEligibility()
//        {
//            Assert.AreEqual(30, BusinessRulesProcedures.GetMaxDaysForRefundEligibility(CurrentSiteId));
//        }

//        [Test]
//        public void GetNXTItemApprovalAmount()
//        {
//            Assert.AreEqual(10, BusinessRulesProcedures.GetNXTItemApprovalAmount(CurrentSiteId));
//        }

//        [Test]
//        public void IsAutoForfeitStoreCreditAllowed()
//        {
//            Assert.IsTrue(BusinessRulesProcedures.IsAutoForfeitStoreCreditAllowed(CurrentSiteId));
//        }

//        [Test]
//        public void GetSaleStoreCreditDuration()
//        {
//            Assert.AreEqual(180, BusinessRulesProcedures.GetSaleStoreCreditDuration(CurrentSiteId));
//        }

//        [Test]
//        public void GetLayStoreCreditDuration()
//        {
//            Assert.AreEqual(180, BusinessRulesProcedures.GetLayStoreCreditDuration(CurrentSiteId));
//        }

//        [Test]
//        public void IsRolloverRestricted()
//        {
//            string currentCustomerNumber = string.Empty;
//            string pawnloanCustomerNumber = string.Empty;
//            DateTime pawnloanOrigDate = DateTime.Today;
//            DateTime currentShopDate = DateTime.Today;
//            string reason = string.Empty;
//            // restrict pickup to original customer is set to 0 [PWN_0116]
//            // Minimum hours in pawn  = 76 [PWN_0076]
//            Assert.IsTrue(BusinessRulesProcedures.IsRolloverRestricted(CurrentSiteId, currentCustomerNumber, pawnloanCustomerNumber,
//                            pawnloanOrigDate, currentShopDate, ref reason));


//        }

//        [Test]
//        public void IsPawnLoanRolloverAllowedBeforeDueDate901()
//        {
//#warning IsPawnLoanRolloverAllowedBeforeDueDate901() -> in excel, rolloverRestrictPickUpToOriginalCustomer is an integer.
//            bool rolloverAllowedBeforeDueDate;
//            bool rolloverRenewSameDateMade;
//            bool rolloverRestrictPickUpToOriginalCustomer;
//            bool rolloverRenewalsBeforeConversionDate;

//            Assert.IsTrue(BusinessRulesProcedures.IsPawnLoanRolloverAllowedBeforeDueDate(CurrentSiteId,
//                            out rolloverAllowedBeforeDueDate,
//                            out rolloverRenewSameDateMade,
//                            out rolloverRestrictPickUpToOriginalCustomer,
//                            out rolloverRenewalsBeforeConversionDate));

//            Assert.IsTrue(!rolloverRenewalsBeforeConversionDate && !rolloverRenewSameDateMade
//                            && rolloverAllowedBeforeDueDate && !rolloverRestrictPickUpToOriginalCustomer);
//        }

//        [Test]
//        public void IsPayDownAllowed()
//        {
//            Assert.IsFalse(BusinessRulesProcedures.IsPayDownAllowed(CurrentSiteId));
//        }

//        [Test]
//        public void IsLoanUpAllowed()
//        {
//            Assert.IsTrue(BusinessRulesProcedures.IsLoanUpAllowed(CurrentSiteId));
//        }

//        [Test]
//        public void IsPartialPaymentAllowed()
//        {
//            Assert.IsTrue(BusinessRulesProcedures.IsPartialPaymentAllowed(CurrentSiteId));
//        }

//        [Test]
//        public void IsExtensionAllowedBeforeDueDate()
//        {
//            Assert.IsFalse(BusinessRulesProcedures.IsExtensionAllowedBeforeDueDate(CurrentSiteId));
//        }

//        [Test]
//        public void IsExtensionAllowedPastPickupAmount()
//        {
//            Assert.IsFalse(BusinessRulesProcedures.IsExtensionAllowedPastPickupAmount(CurrentSiteId));
//        }

//        [Test]
//        public void IsExtensionAllowedPastRenewalAmount()
//        {
//#warning IsExtensionAllowedPastRenewalAmount() - This is set to N/A in rules [PWN-082]
//            Assert.IsTrue(BusinessRulesProcedures.IsExtensionAllowedPastRenewalAmount(CurrentSiteId));
//        }

//        [Test]
//        public void PFIWaitdays()
//        {
//#warning PFIWaitDays() -> value should be 0, but is set to 32
//            Assert.AreEqual(32, BusinessRulesProcedures.PFIWaitdays(CurrentSiteId));
//        }

//        [Test]
//        public void GetStorageFee()
//        {
//            Assert.AreEqual(4M, BusinessRulesProcedures.GetStorageFee(CurrentSiteId));
//        }

//        [Test]
//        public void PrintMultipleMemoOfExtension()
//        {
//            Assert.IsFalse(BusinessRulesProcedures.PrintMultipleMemoOfExtension(CurrentSiteId));
//        }

//        [Test]
//        public void GetStateExtensionTerm()
//        {
//            Assert.AreEqual("MONTHLY", BusinessRulesProcedures.GetStateExtensionTerm(CurrentSiteId));
//        }

//        [Test]
//        public void CanWaiveAutoExtendFee()
//        {
//#warning CanWaiveAutoExtendFee() -> This is blank in excel
//            Assert.IsFalse(BusinessRulesProcedures.CanWaiveAutoExtendFee(CurrentSiteId));
//        }

//        [Test]
//        public void CanWaiveGunLockFee()
//        {
//#warning CanWaiveGunLockFee() -> Not set in excel
//            Assert.IsTrue(BusinessRulesProcedures.CanWaiveGunLockFee(CurrentSiteId));
//        }

//        [Test]
//        public void CanWaiveLateFee()
//        {
//#warning CanWaiveLateFee -> This should be false, but is returning true


//            Assert.IsTrue(BusinessRulesProcedures.CanWaiveLateFee(CurrentSiteId));
//        }

//        [Test]
//        public void CanProrateLateFee()
//        {
//#warning CanProrateLateFee() -> this is returning true, but should be false
//            Assert.IsTrue(BusinessRulesProcedures.CanProrateLateFee(CurrentSiteId));
//        }

//        [Test]
//        public void IsManagerOverrideRequiredForProrateWaive()
//        {
//#warning IsManagerOverrideRequiredForProrateWaive() -> set to no in excel. returning true.
//            Assert.IsTrue(BusinessRulesProcedures.IsManagerOverrideRequiredForProrateWaive(CurrentSiteId));
//        }

//        [Test]
//        public void CanWaiveStorageFee()
//        {
//            Assert.IsFalse(BusinessRulesProcedures.CanWaiveStorageFee(CurrentSiteId));
//        }

//        [Test]
//        public void CanWaiveFirearmBackgroundCheckFee()
//        {
//#warning CanWaiveFirearmBackgroundCheckFee() -> not set in excel!
//            Assert.IsFalse(BusinessRulesProcedures.CanWaiveFirearmBackgroundCheckFee(CurrentSiteId));
//        }

//        [Test]
//        public void CanWaiveLostTicketFee()
//        {
//#warning CanWaiveLostTicketFee() -> not set in excel!
//            Assert.IsFalse(BusinessRulesProcedures.CanWaiveLostTicketFee(CurrentSiteId));
//        }

//        [Test]
//        public void GetMinimumAgeHandGun()
//        {
//            Assert.AreEqual(21, Convert.ToInt32(BusinessRulesProcedures.GetMinimumAgeHandGun(CurrentSiteId)));
//        }

//        [Test]
//        public void GetMinimumAgeLongGun()
//        {
//            Assert.AreEqual(18, Convert.ToInt32(BusinessRulesProcedures.GetMinimumAgeLongGun(CurrentSiteId)));
//        }

//        [Test]
//        public void GetMinMaxLoanAmounts()
//        {
//#warning GetMinMaxLoanAmounts() -> Minimum value should be 0.01, but is set to 1
//            decimal min = 0, max = 0;
//            BusinessRulesProcedures.GetMinMaxLoanAmounts(CurrentSiteId, out min, out max);

//            Assert.AreEqual(1M, min, "Minimum value failed.");
//            Assert.AreEqual(99500, max, "Maximum value failed.");
//        }

//        [Test]
//        public void GetMaxVoidDays()
//        {
//#warning GetMaxVoidDays() -> oddly written. Why not return the value instead of a bool?

//            Int64 maxVoidDays;
//            BusinessRulesProcedures.GetMaxVoidDays(CurrentSiteId, out maxVoidDays);

//            Assert.AreEqual(10, maxVoidDays);
//        }

//        [Test]
//        public void GetTicketName()
//        {
//#warning GetTicketName() -> cannot find where the value should come from!!
//            string pawnTicketName = string.Empty;
//            Assert.IsTrue(BusinessRulesProcedures.GetTicketName(CurrentSiteId, out pawnTicketName));
//            Assert.AreEqual("ticketfmtL.OH", pawnTicketName);
//        }

//        [Test]
//        public void GetMaxMdseTransferVoidDays()
//        {
//#warning GetMaxMdseTransferVoidDays() -> set to 3 in excel. also crossed out.
//            Int64 maxVoidDays = 0;
//            bool temp = BusinessRulesProcedures.GetMaxMdseTransferVoidDays(CurrentSiteId, out maxVoidDays);
//            Assert.Inconclusive("This is set to 3 in excel. This is also crossed out in excel.");
//            Assert.IsTrue(temp);
//            Assert.AreEqual(1, maxVoidDays);
//        }

//        [Test]
//        public void GetMaxLoanLimit()
//        {
//            decimal rt;
//            Assert.IsTrue(BusinessRulesProcedures.GetMaxLoanLimit(CurrentSiteId, out rt));

//            Assert.AreEqual(99500, rt);
//        }

//        [Test]
//        public void GetValidShopRoles()
//        {
//            List<string> returnedRoles = new List<string>();
//            Assert.IsTrue(BusinessRulesProcedures.GetValidShopRoles(CurrentSiteId, out returnedRoles));
//            string allValidAnswers = "CSR - I|CSR - II|CSR - III|Shop Manager|Assistant Manager|Regional Vice President|Market Manager|Operations Director";

//            List<string> validRoles = new List<string>();
//            validRoles.AddRange(allValidAnswers.Split('|'));


//            // Make sure every role in the valid roles is actually in the returnedRoles
//            Assert.IsEmpty(validRoles.Where(a => !returnedRoles.Contains(a)));
//        }

//        [Test]
//        public void GetValidateUserLimit()
//        {
//#warning GetValidateUserLimit() -> unable to determine how to test.
//            var temp = BusinessRulesProcedures.GetValidateUserLimit(CurrentSiteId);
//            Assert.Inconclusive("unable to determine how to test this.");
//            Assert.IsTrue(temp);
//        }

//        [Test]
//        public void GetMaxCashDrawerLimit()
//        {
//            decimal cashDrawerLimit;
//            Assert.IsTrue(BusinessRulesProcedures.GetMaxCashDrawerLimit(CurrentSiteId, out cashDrawerLimit));
//            Assert.Inconclusive("No data in excel.");
//            Assert.AreEqual(0, cashDrawerLimit);
//        }

//        [Test]
//        public void IsSeparateTicketForFirearm()
//        {
//            Assert.IsFalse(BusinessRulesProcedures.IsSeparateTicketForFirearm(CurrentSiteId));
//        }

//        [Test]
//        public void IsAllowedJewelryGenMDSEOneBuy()
//        {
//#warning IsAllowedJewelryGenMDSEOneBuy() -> this should return false, but is returning true
//            Assert.IsTrue(BusinessRulesProcedures.IsAllowedJewelryGenMDSEOneBuy(CurrentSiteId));
//        }

//        [Test]
//        public void GetMaxItemsInBuyTransaction()
//        {
//            Assert.AreEqual(999, BusinessRulesProcedures.GetMaxItemsInBuyTransaction(CurrentSiteId));
//        }

//        [Test]
//        public void GetPfiMailerAdjustmentDays()
//        {
//#warning GetPfiMailerAdjustmentDays() -> This should be 1, but is returning 0
//            Assert.AreEqual(0, BusinessRulesProcedures.GetPfiMailerAdjustmentDays(CurrentSiteId));
//        }

//        [Test]
//        public void GetValidPFIDateWithWaitdaysForBuy()
//        {
//            DateTime transDate = new DateTime(2012, 01, 01);
//            Assert.Inconclusive("Unsure how to test.");
//            Assert.AreEqual(transDate, Utilities.GetDateTimeValue(BusinessRulesProcedures.GetValidPFIDateWithWaitdaysForBuy(transDate, CurrentSiteId)));
//        }

//        [Test]
//        public void GetCashDrawerMaxEndingBalance()
//        {
//            decimal temp = BusinessRulesProcedures.GetCashDrawerMaxEndingBalance(CurrentSiteId);
//            Assert.Inconclusive("This value is not set in excel");
//            Assert.AreEqual(0, temp);
//        }

//        [Test]
//        public void GetCustomerInfoRequiredSaleValue()
//        {
//            var temp = BusinessRulesProcedures.GetCustomerInfoRequiredSaleValue(CurrentSiteId);
//            Assert.Inconclusive("This value is not set in excel");
//            Assert.AreEqual(10, temp);
//        }

//        [Test]
//        public void GetCustomerBusinessRule()
//        {
//            var temp = BusinessRulesProcedures.GetCustomerBusinessRule(CurrentSiteId);
//            Assert.Inconclusive("Need help coding this test.");
//            Assert.AreEqual(new BusinessRuleVO(), temp);
//        }

//        [Test]
//        public void GetCustomerSurveyCardSaleTransactions()
//        {
//#warning GetCustomerSurveyCardSaleTransactions() Should be 1, but is returning 4
//            Assert.AreEqual(4, BusinessRulesProcedures.GetCustomerSurveyCardSaleTransactions(CurrentSiteId));
//        }

//        [Test]
//        public void IsServiceFeeTaxable()
//        {
//            Assert.IsFalse(BusinessRulesProcedures.IsServiceFeeTaxable(CurrentSiteId));
//        }

//        [Test]
//        public void CustomerSignatureOnBackofTicket()
//        {
//            Assert.IsTrue(BusinessRulesProcedures.CustomerSignatureOnBackofTicket(CurrentSiteId));
//        }

//        [Test]
//        public void GetPoliceHoldDefaultDays()
//        {
//            Assert.AreEqual(30, BusinessRulesProcedures.GetPoliceHoldDefaultDays(CurrentSiteId));
//        }

//        [Test]
//        public void IsPartialPaymentAllowedAfterDueDate()
//        {
//            Assert.IsTrue(BusinessRulesProcedures.IsPartialPaymentAllowedAfterDueDate(CurrentSiteId));
//        }

//        [Test]
//        public void IsPartialPaymentAllowedAfterPfiDate()
//        {
//            Assert.IsFalse(BusinessRulesProcedures.IsPartialPaymentAllowedAfterPfiDate(CurrentSiteId));
//        }

//        [Test]
//        public void GetPartialPaymentMinAmount()
//        {
//            Assert.AreEqual(1, BusinessRulesProcedures.GetPartialPaymentMinAmount(CurrentSiteId));
//        }
    }
}
