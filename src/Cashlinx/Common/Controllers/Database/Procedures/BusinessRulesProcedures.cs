using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Controllers.Rules.Interface;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Common.Controllers.Database.Procedures
{
    public class BusinessRulesProcedures : IBusinessRulesProcedures
    {
        private static List<string> _BusinessRules = new List<String>
        {
            "PWN_BR-000", 
            "PWN_BR-010", 
            "PWN_BR-013",
            "PWN_BR-014",
            "PWN_BR-016", 
            "PWN_BR-017",
            "PWN_BR-018",
            "PWN_BR-019",
            "PWN_BR-020", 
            "PWN_BR-023",
            "PWN_BR-024", 
            "PWN_BR-025",
            "PWN_BR-026",
            "PWN_BR-030",
            "PWN_BR-033",
            "PWN_BR-042", 
            "PWN_BR-043",
            "PWN_BR-046", 
            "PWN_BR-047", 
            "PWN_BR-048", 
            "PWN_BR-057", 
            "PWN_BR-058",
            "PWN_BR-074",
            "PWN_BR-075", 
            "PWN_BR-077",
            "PWN_BR_089",
            "PWN_BR-092",
            "PWN_BR-094",
            "PWN_BR-096",
            "PWN_BR-097",
            "PWN_BR-106",
            "PWN_BR-116",
            "PWN_BR-117",
            "PWN_BR-130",
            "PWN_BR-133",
            "PWN_BR-148",
            "PWN_BR-141",
            "PWN_BR-142",
            "PWN_BR-134",
            "PWN_BR-138",
            "PWN_BR-169",
            "PWN_BR-170",
            "PWN_BR-171",
            "PWN_BR-172",
            "PWN_BR-175",
            "PWN_BR-176",
            "PWN_BR-179"
        };

        const string CONSTANTB = "B";
        const string CONSTANTF = "F";
        const string CONSTANTY = "Y";

        public DesktopSession DesktopSession { get; private set; }

        public BusinessRulesProcedures(DesktopSession desktopSession)
        {
            DesktopSession = desktopSession;
        }

        public decimal GetPFIMailerFee(SiteId siteID)
        {
            var mailerFee = "0";

            var brObject = GetBusinessRule(siteID, "PWN_BR-017");
            brObject.getComponentValue("CL_PWN_0040_PFIMAILFEE", ref mailerFee);

            return Utilities.GetDecimalValue(mailerFee, 0M);
        }

        public decimal GetSalesTaxRoundingAdjustment(SiteId siteID)
        {
            var salesTaxRoundingAdjustment = "0";

            var brObject = GetBusinessRule(siteID, "PWN_BR-179");
            brObject.getComponentValue("CL_PWN_270_STTXRDUP", ref salesTaxRoundingAdjustment);

            return Utilities.GetDecimalValue(salesTaxRoundingAdjustment, 0M);
        }

        public bool IsMultipleDispositionOfCertainRiflesReportPrintedForSite(SiteId siteID)
        {
            var isMultipleDispositionOfCertainRiflesReportPrintedForSite = "N";

            var brObject = GetBusinessRule(siteID, "PWN_BR-175");
            brObject.getComponentValue("CL-PWN-265_PrintMultipleDisposition", ref isMultipleDispositionOfCertainRiflesReportPrintedForSite);

            if (isMultipleDispositionOfCertainRiflesReportPrintedForSite == "Y")
                return true;
            else
                return false;
        }

        public string GetPFIEligibleDateAdjustmentDirection(SiteId siteID)
        {
            var pfiEligibleDateAdjustmentDirection = "F";

            var brObject = GetBusinessRule(siteID, "PWN_BR-033");
            brObject.getComponentValue("CL_PWN_0059_PFIDATEADJT", ref pfiEligibleDateAdjustmentDirection);

            return pfiEligibleDateAdjustmentDirection;
        }

        public int GetPFIEligibleAdjustmentTriggerDays(SiteId siteID)
        {
            var pfiDateAdjustmentDays = "0";

            var brObject = GetBusinessRule(siteID, "PWN_BR-033");
            brObject.getComponentValue("CL_PWN_0088_PFIMLRTRGRDAYS", ref pfiDateAdjustmentDays);

            return Convert.ToInt32(pfiDateAdjustmentDays);
        }

        public Boolean IsPFIMailersRequiredForState(SiteId siteID)
        {
            var isPFIMailersRequired = "N";

            var brObject = GetBusinessRule(siteID, "PWN_BR-042");
            brObject.getComponentValue("CL_PWN_086_PFIMAILERREQUIRED", ref isPFIMailersRequired);

            if (isPFIMailersRequired == "Y")
                return true;
            else
                return false;
        }

        public int GetNumberOfDaysToAddForPFIMailer(SiteId siteID)
        {
            var numberOfDaysToAdd = String.Empty;

            var brObject = GetBusinessRule(siteID, "PWN_BR-043");
            brObject.getComponentValue("CL_PWN_262_PFIMAILERADJDAYS", ref numberOfDaysToAdd);

            if (numberOfDaysToAdd.Length == 0)
                numberOfDaysToAdd = "0";

            return Utilities.GetIntegerValue(numberOfDaysToAdd);
        }

        private BusinessRuleVO GetBusinessRule(SiteId siteID, string sBusinessRule)
        {
            BusinessRuleVO _BusinessRule = null;
            Dictionary<string, BusinessRuleVO> _Rules;
            PawnRulesSystemInterface prs = DesktopSession.PawnRulesSys;
            prs.beginSite(prs, siteID, _BusinessRules, out _Rules);
            prs.getParameters(prs, siteID, ref _Rules);
            prs.endSite(prs, siteID);

            if (_Rules.ContainsKey(sBusinessRule))
            {
                _BusinessRule = _Rules[sBusinessRule];
            }
            return _BusinessRule;
        }

        public bool ShouldPrintPickupReceipt(SiteId siteID)
        {
            var componentValue = string.Empty;
            var foundValue = false;
            var rule = GetBusinessRule(siteID, "PWN_BR-178");
            if (rule != null)
                foundValue = rule.getComponentValue("CL_PWN_269_PRINTPICKUPRECEIPT", ref componentValue);

            return componentValue == "Y";
        }

        public decimal GetStoreMinimumIntCharge()
        {
            BusinessRuleVO brMinFinCharge = DesktopSession.PawnBusinessRuleVO["PWN_BR-002"];
            string minChargeComponentValue = "";
            decimal minFinanceCharge = 0.0M;
            bool ruleFound = brMinFinCharge.getComponentValue("CL_PWN_0013_MININTAMT", ref minChargeComponentValue);
            if (ruleFound && !minChargeComponentValue.Equals(string.Empty))
                minFinanceCharge = Utilities.GetDecimalValue(minChargeComponentValue);
            return minFinanceCharge;
        }

        public string GetValidDueDate(DateTime Duedate, SiteId site)
        {
            //Get Due date adjustment value
            var dueDateAdjustmentValue = string.Empty;
            var businessDayAdjustmentReqd = string.Empty;
            var brObject = GetBusinessRule(site, "PWN_BR-020");
            bool bPawnValue;
            bool badjValue;
            if (brObject != null)
            {
                bPawnValue = brObject.getComponentValue("CL_PWN_0054_DUEDATEADJT", ref dueDateAdjustmentValue);
                badjValue = brObject.getComponentValue("CL_PWN_0093_DUEDATENBDADJTREQ", ref businessDayAdjustmentReqd);
            }
            var uwpUtil = new UnderwritePawnLoanUtility(GlobalDataAccessor.Instance.DesktopSession);

            bool retValue = false;
            if (businessDayAdjustmentReqd.Equals(CONSTANTY))
            {
                do
                {
                    if (uwpUtil.IsShopClosed(Duedate))
                    {
                        if (dueDateAdjustmentValue.Equals(CONSTANTB))
                        {
                            Duedate = Duedate.AddDays(-1.0d).Date;
                        }
                        else if (dueDateAdjustmentValue.Equals(CONSTANTF))
                        {
                            Duedate = Duedate.AddDays(1.0d).Date;
                        }
                    }
                    else
                        retValue = true;
                }
                while (!retValue);
            }

            return Duedate.FormatDate();
        }

        public string GetValidDueDateFromPFIDate(DateTime Duedate, SiteId site)
        {
            //Get PFi wait days
            int pfiWaitDays = PFIWaitdays(site);
            Duedate = Duedate.AddDays(-pfiWaitDays);
            //Get Due date adjustment value
            var dueDateAdjustmentValue = string.Empty;
            var businessDayAdjustmentReqd = string.Empty;
            var brObject = GetBusinessRule(site, "PWN_BR-020");

            bool bPawnValue = brObject.getComponentValue("CL_PWN_0054_DUEDATEADJT", ref dueDateAdjustmentValue);
            bool adjValue = brObject.getComponentValue("CL_PWN_0093_DUEDATENBDADJTREQ", ref businessDayAdjustmentReqd);
            bool retValue = false;

            var uwpUtil = new UnderwritePawnLoanUtility(GlobalDataAccessor.Instance.DesktopSession);

            if (businessDayAdjustmentReqd.Equals(CONSTANTY))
            {
                do
                {
                    if (uwpUtil.IsShopClosed(Duedate))
                    {
                        if (dueDateAdjustmentValue.Equals(CONSTANTB))
                        {
                            Duedate = Duedate.AddDays(-1.0d).Date;
                        }
                        else if (dueDateAdjustmentValue.Equals(CONSTANTF))
                        {
                            Duedate = Duedate.AddDays(1.0d).Date;
                        }
                    }
                    else
                        retValue = true;
                }
                while (!retValue);
            }

            return Duedate.FormatDate();
        }

        public string GetValidPFIDateWithWaitdays(DateTime pfiDate, SiteId siteId, ref bool dateAdjusted)
        {
            //Get PFi wait days
            int pfiWaitDays = PFIWaitdays(siteId);
            pfiDate = pfiDate.AddDays(pfiWaitDays);
            //Now call get valid pfi date with the wait days added PFI date
            return GetValidPFIDate(pfiDate, siteId, ref dateAdjusted);
        }

        public string GetValidPFIDate(DateTime pfiDate, SiteId siteId, ref bool dateAdjusted)
        {
            //CL_PWN_0059_PFIDATEADJT
            string pfiDateAdjustmentValue = "";
            BusinessRuleVO brObject = GetBusinessRule(siteId, "PWN_BR-020");

            bool bPawnValue = brObject.getComponentValue("CL_PWN_0059_PFIDATEADJT", ref pfiDateAdjustmentValue);
            string businessDayAdjustmentReqd = "";
            bool adjValue = brObject.getComponentValue("CL_PWN_0169_PFIDATENBDADJTREQ", ref businessDayAdjustmentReqd);
            bool retValue = false;
            if (businessDayAdjustmentReqd.Equals(CONSTANTY))
            {
                do
                {
                    if (new UnderwritePawnLoanUtility(DesktopSession).IsShopClosed(pfiDate))
                    {
                        if (pfiDateAdjustmentValue.Equals(CONSTANTB))
                        {
                            pfiDate = pfiDate.AddDays(-1.0d).Date;
                            dateAdjusted = true;
                        }
                        else if (pfiDateAdjustmentValue.Equals(CONSTANTF))
                        {
                            pfiDate = pfiDate.AddDays(1.0d).Date;
                            dateAdjusted = true;
                        }
                    }
                    else
                        retValue = true;
                } while (!retValue);
            }
            return pfiDate.FormatDate();
        }

        public bool IsPickupRestricted(
            SiteId siteID, string currentCustomerNumber, string pawnloanCustomerNumber,
            DateTime pawnloanOrigDate, DateTime currentShopDate, ref string reason)
        {
            var brObject = GetBusinessRule(siteID, "PWN_BR-016");
            //var minHoursInPawn = GetMinimumHoursInPawn(siteID);
            var numofDaysValue = string.Empty;
            var bMinDaysRule = brObject.getComponentValue("CL_PWN_0116_RESTRICTPICKUP", ref numofDaysValue);
            var legalDocRequired = string.Empty;
            var legalDocRule = brObject.getComponentValue("CL_PWN_0188_LEGALDOCREQUIRED", ref legalDocRequired);

            //If customer != pledgor
            if (currentCustomerNumber != pawnloanCustomerNumber)
            {
                var numofDays = Utilities.GetIntegerValue(numofDaysValue, 0);
                //If pawn in loan < restrict pickup to cust days rule value
                //pickup is disabled
                if ((currentShopDate - pawnloanOrigDate).Days < numofDays &&
                    legalDocRequired == "N")
                {
                    reason = string.Format("Pickup is restricted to the original pledgor until {0}{1}", pawnloanOrigDate.AddDays(numofDays).Date.FormatDate(), Environment.NewLine);
                    return true;
                }
                if ((currentShopDate - pawnloanOrigDate).Days < numofDays &&
                    legalDocRequired == "Y")
                {
                    reason =
                        Commons.GetMessageString("PickupLegalDocRequiredMsg") + Environment.NewLine;
                    return false;
                }
            }
            return false;
        }

        public bool IsExtensionAllowed(SiteId siteID)
        {
            // IS extension allowed at this store
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            var brObject = GetBusinessRule(siteID, "PWN_BR-013");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0072_EXTALLWD", ref sComponentValue);

            return sComponentValue == "Y";
        }

        public bool IsRenewalAllowed(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is renewal allowed at this store
            var brObject = GetBusinessRule(siteID, "PWN_BR-047");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0073_RENALLWD", ref sComponentValue);

            return sComponentValue == "Y";
        }

        // Need correct ComponentKey
        // Update - no component
        public bool IsRenewalAtDifferentStoreAllowed(SiteId siteID)
        {
            //Is renewal allowed at this store
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-077");
            //bPawnValue = brObject.getComponentValue("CL_PWN_0077_RENALLWD", ref sComponentValue);
            //TODO - Hard-coded false until business determination
            return (false);// sComponentValue == "Y" ? true : false;
        }

        public int GetMinimumHoursInPawn(SiteId siteID)
        {
            var value = string.Empty;

            var brObject = GetBusinessRule(siteID, "PWN_BR-016");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_0076_MINWAITINPAWN", ref value);

            return Utilities.GetIntegerValue(value, 0);
        }

        public int GetGracePeriod(SiteId siteID)
        {
            var value = string.Empty;

            var brObject = GetBusinessRule(siteID, "PWN_BR-018");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_077_GRACEPERIOD", ref value);

            return Utilities.GetIntegerValue(value, 0);
        }

        public int GetMaxItemsForTransfer(SiteId siteID)
        {
            string maxItems = "";

            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-009");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_0222_MAXSCRPITMXFRLMT", ref maxItems);

            return Utilities.GetIntegerValue(maxItems, 0);
        }

        public int GetMaxLayawayNumberOfPayments(SiteId siteID)
        {
            string maxNumberOfPayments = "";

            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-130");
            if (brObject != null)
                brObject.getComponentValue("CL-RTL-137MAX_PMNTS_LAYAWAY", ref maxNumberOfPayments);
            return Utilities.GetIntegerValue(maxNumberOfPayments, 0);
        }

        public int GetMinimumPaymentDateLimit(SiteId siteID)
        {
            string minPaymentDateLimit = "";

            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-133");
            if (brObject != null)
                brObject.getComponentValue("CL_RTL-233USERDATEMINIMUM", ref minPaymentDateLimit);

            return Utilities.GetIntegerValue(minPaymentDateLimit, 0);
        }

        public bool IsQuickReceiveTransferInAllowed(SiteId siteID)
        {
            string isQuickReceiveAllowed = "";

            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR_073");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_154_QUICKTXFRALLWD", ref isQuickReceiveAllowed);

            return isQuickReceiveAllowed == "Y" ? true : false;
        }

        public int GetMaxPaymentDateLimit(SiteId siteID)
        {
            string maxPaymentDateLimit = "";

            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-092");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN-195USERDATELIMITOVER", ref maxPaymentDateLimit);

            return Utilities.GetIntegerValue(maxPaymentDateLimit, 0);
        }

        public int GetWaitDaysForLayawayForfeitureEligibility(SiteId siteID)
        {
            string waitDaysForLayawayForfeitureEligibility = "";

            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-154");
            if (brObject != null)
                brObject.getComponentValue("CL-RTL-250-WAITDAYPERIODLAYFORFEITURE", ref waitDaysForLayawayForfeitureEligibility);

            return 0; // Utilities.GetIntegerValue(waitDaysForLayawayForfeitureEligibility, 0);
        }

        public decimal GetLayawayRestockingFee(SiteId siteID)
        {
            string layawayRestockingFee = "";

            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-137");
            if (brObject != null)
                brObject.getComponentValue("CL-RTL-134-LAYRESTOCKINGFEE", ref layawayRestockingFee);

            return Utilities.GetDecimalValue(layawayRestockingFee, 0);
        }

        public int GetMaxDaysForRefundEligibility(SiteId siteID)
        {
            string maxDaysForRefundEligibility = "";

            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-087");
            if (brObject != null)
                brObject.getComponentValue("CL-RTL-249EligibleForRefund", ref maxDaysForRefundEligibility);

            return Utilities.GetIntegerValue(maxDaysForRefundEligibility, 0);
        }

        public int GetNXTItemApprovalAmount(SiteId siteID)
        {
            string approvalAmt = "";

            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-148");
            if (brObject != null)
                brObject.getComponentValue("CL-RTL-246_NXTITEM_MGRAPPROVAL_AMT", ref approvalAmt);

            return Utilities.GetIntegerValue(approvalAmt, 0);
        }

        public bool IsAutoForfeitStoreCreditAllowed(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            //Is auto forfeiture of store credit allowed at this store
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-141");
            if (brObject != null)
                brObject.getComponentValue("CL_RTL_129_AUTOSTFORF", ref sComponentValue);

            return sComponentValue == "Y";
        }


        public int GetSaleStoreCreditDuration(SiteId siteID)
        {
            var saleStoreCreditDays = string.Empty;

            var brObject = GetBusinessRule(siteID, "PWN_BR-142");
            if (brObject != null)
                brObject.getComponentValue("CL_RTL_130SALESCEXPIRY", ref saleStoreCreditDays);

            return Utilities.GetIntegerValue(saleStoreCreditDays, 0);
        }


        public int GetLayStoreCreditDuration(SiteId siteID)
        {
            string layStoreCreditDays = "";

            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-134");
            if (brObject != null)
                brObject.getComponentValue("CL_RTL_133LAYSCEXPIRY", ref layStoreCreditDays);

            return Utilities.GetIntegerValue(layStoreCreditDays, 0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteID"></param>
        /// <param name="currentCustomerNumber"></param>
        /// <param name="pawnloanCustomerNumber"></param>
        /// <param name="pawnloanOrigDate"></param>
        /// <param name="currentShopDate"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public bool IsRolloverRestricted(
            SiteId siteID, string currentCustomerNumber, string pawnloanCustomerNumber,
            DateTime pawnloanOrigDate, DateTime currentShopDate, ref string reason)
        {
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-016");
            string minHoursValue = "";
            bool bMinWaitRule = brObject.getComponentValue("CL_PWN_0076_MINWAITINPAWN", ref minHoursValue);
            string numofDaysValue = "";
            bool bMinDaysRule = brObject.getComponentValue("CL_PWN_0116_RESTRICTPICKUP", ref numofDaysValue);

            //If the customer is the same as the pledgor
            if (currentCustomerNumber.Equals(pawnloanCustomerNumber))
            {
                int minHoursInPawn = Utilities.GetIntegerValue(minHoursValue, 0);
                //If hours that pawn has been in loan < minimum number of hours in pawn rule value
                //pickup is disabled
                if ((currentShopDate - pawnloanOrigDate).TotalHours < minHoursInPawn)
                {
                    reason =
                    "Loan is not due eligible for renewal for " + minHoursInPawn + " hours from the time of pawn." + System.Environment.NewLine;
                    return (true);
                }
            }
            //Otherwise determine how long we must wait until allowing a renewal on a
            //loan for a non-pledgor
            else
            {
                int numofDays = Utilities.GetIntegerValue(numofDaysValue, 0);
                //If pawn in loan < restrict renewal to cust days rule value, then
                //renewal is disabled
                if ((currentShopDate - pawnloanOrigDate).Days < numofDays)
                {
                    reason =
                    "Renewal is restricted to the original pledgor until " + pawnloanOrigDate.AddDays(numofDays).Date.FormatDate() + System.Environment.NewLine;
                    return (true);
                }
            }
            return (false);
        }

        // Need correct ComponentKey
        public bool IsPawnLoanRolloverAllowedBeforeDueDate(SiteId siteID,
                                                                  out bool rolloverAllowedBeforeDueDate,
                                                                  out bool rolloverRenewSameDateMade,
                                                                  out bool rolloverRestrictPickUpToOriginalCustomer,
                                                                  out bool rolloverRenewalsBeforeConversionDate)
        {
            rolloverAllowedBeforeDueDate = false;
            rolloverRenewSameDateMade = false;
            rolloverRestrictPickUpToOriginalCustomer = false;
            rolloverRenewalsBeforeConversionDate = false;

            try
            {
                var sComponentValue = string.Empty;
                var bPawnValue = false;
                BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-046");
                bPawnValue = brObject.getComponentValue("CL_PWN_0084_ROLLOVERBEFDDATE", ref sComponentValue);
                rolloverAllowedBeforeDueDate = (sComponentValue == "Y");
                bPawnValue = brObject.getComponentValue("CL_PWN_0192_RENEWSAMEDATEMADE", ref sComponentValue);
                rolloverRenewSameDateMade = (sComponentValue == "Y");
                bPawnValue = brObject.getComponentValue("CL_PWN_0116_RESTRICTPICKUP", ref sComponentValue);
                rolloverRestrictPickUpToOriginalCustomer = (sComponentValue == "Y");
                bPawnValue = brObject.getComponentValue("CL_PWN_0085_RENEWBEFCONVDATE", ref sComponentValue);
                rolloverRenewalsBeforeConversionDate = (sComponentValue == "Y");

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsPayDownAllowed(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is Pay Down allowed at this store
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-057");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0074_PDALLWD", ref sComponentValue);

            return sComponentValue == "Y";
        }

        public bool IsLoanUpAllowed(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is Loan Up allowed at this store
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-058");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0075_LUALLWD", ref sComponentValue);

            return sComponentValue == "Y";
        }

        public bool IsPartialPaymentAllowed(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is Partial payment allowed at this store
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-169");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_255_PARTIALPMTALLWD", ref sComponentValue);
            return sComponentValue == "Y";
        }

        public bool AllowFutureInterestPayments(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is Partial payment allowed at this store
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-015");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_259_ALLOWFUTUREINTEREST", ref sComponentValue);
            return sComponentValue == "Y";
        }

        public bool IsExtensionAllowedBeforeDueDate(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is Extension allowed before due date
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-014");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0079_EXTALLWDBEFOREDUEDT", ref sComponentValue);

            return sComponentValue == "Y";
        }

        public bool IsExtensionAllowedPastPickupAmount(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is Extension allowed if extension amount is more than pickup amount
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-023");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0081_EXTNPASTPICKUPAMT", ref sComponentValue);

            return sComponentValue == "Y";
        }

        public bool IsExtensionAllowedPastRenewalAmount(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is Extension allowed if extension amount is more than renewal amount
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-024");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0082_EXTNPASTRENEWAMT", ref sComponentValue);

            return sComponentValue == "Y";
        }

        public int PFIWaitdays(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Number of wait days for PFI
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-020");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0056_WTDAYSPRPFI", ref sComponentValue);

            return Utilities.GetIntegerValue(sComponentValue, 0);
        }

        public decimal GetStorageFee(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            var brObject = GetBusinessRule(siteID, "PWN_BR-002");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0030_STRGFEE", ref sComponentValue);

            return Utilities.GetDecimalValue(sComponentValue, 0);
        }

        public bool PrintMultipleMemoOfExtension(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is Extension allowed if extension amount is more than renewal amount
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-025");
            bPawnValue = brObject.getComponentValue("CL_PWN_0070_PRINTMEMOOFEXTENSION", ref sComponentValue);

            return sComponentValue == "Y";
        }

        /// <summary>
        /// Gets the value of the business rule that specifies what the extension term
        /// is for the state.
        /// </summary>
        /// <returns></returns>
        public string GetStateExtensionTerm(SiteId siteID)
        {
            BusinessRuleVO brExtnTerm = GetBusinessRule(siteID, "PWN_BR-026");
            string termComponentValue = "";
            bool ruleFound = brExtnTerm.getComponentValue("CL_PWN_0094_EXTENSIONTERM", ref termComponentValue);
            if (ruleFound && !termComponentValue.Equals(string.Empty))
                return termComponentValue;
            return termComponentValue;
        }

        public bool CanWaiveAutoExtendFee(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-074");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0186_WAIVAUTOEXTENDFEE", ref sComponentValue);

            return sComponentValue == "Y";
        }

        public bool CanWaiveGunLockFee(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-074");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0185_WAIVGUNLOCKFEE", ref sComponentValue);

            return sComponentValue == "Y";
        }

        public bool CanWaiveLateFee(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-074");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0182_WAIVLATEFEE", ref sComponentValue);

            return sComponentValue == "Y";
        }

        public bool CanProrateLateFee(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-074");
            bPawnValue = brObject.getComponentValue("CL_PWN_0183_PRORATELATEFEE", ref sComponentValue);

            return sComponentValue == "Y";
        }

        public bool IsManagerOverrideRequiredForProrateWaive(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-075");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0122_MGRAPPRREQPRORATEWAIVES", ref sComponentValue);

            return sComponentValue == "Y";
        }

        public bool CanWaiveStorageFee(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-074");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0035_WAIVESTORAGEFEE", ref sComponentValue);
            return sComponentValue == "Y";
        }

        public bool CanWaiveFirearmBackgroundCheckFee(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;

            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-074");
            if (brObject != null)
                bPawnValue = brObject.getComponentValue("CL_PWN_0184_WAIVEFIREARMBACKGROUNDCHECKFEE", ref sComponentValue);
            return sComponentValue == "Y";
        }

        public bool CanWaiveLostTicketFee(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-074");
            bPawnValue = brObject.getComponentValue("CL_PWN_0189_WAIVELOSTTKTFEE", ref sComponentValue);

            return sComponentValue == "Y";
        }

        public string GetMinimumAgeHandGun(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-059");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_0174_MINAGEHGUNLN", ref sComponentValue);

            return sComponentValue;
        }

        public string GetMinimumAgeLongGun(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-059");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_0175_MINAGELGUNLN", ref sComponentValue);

            return sComponentValue;
        }

        public bool GetMinMaxLoanAmounts(SiteId siteID, out decimal minAmt, out decimal maxAmt)
        {
            minAmt = 0.00M;
            maxAmt = 0.00M;
            var sComponentValue = string.Empty;
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-019");
            if (brObject != null) brObject.getComponentValue("CL_PWN_0002_MINLOANAMT", ref sComponentValue);
            minAmt = Utilities.GetDecimalValue(sComponentValue, 1.00M);
            BusinessRuleVO brObject2 = GetBusinessRule(siteID, "PWN_BR-010");
            sComponentValue = string.Empty;
            if (brObject2 != null) brObject2.getComponentValue("CL_PWN_0001_MAXLOANAMT", ref sComponentValue);
            maxAmt = Utilities.GetDecimalValue(sComponentValue, 15000.00M);
            return (true);
        }

        public bool GetMaxVoidDays(SiteId siteID, out Int64 maxVoidDays)
        {
            maxVoidDays = 0L;
            var rt = false;
            var brObject = GetBusinessRule(siteID, "PWN_BR-000");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("MAXVOIDDAYS", ref sComponentValue))
                {
                    maxVoidDays = Utilities.GetLongValue(sComponentValue, 1L);
                    rt = true;
                }
            }
            return (rt);
        }
        public bool GetTicketName(SiteId siteID, out string pawnTicketName)
        {
            pawnTicketName = string.Empty;
            var rt = false;
            var brObject = GetBusinessRule(siteID, "PWN_BR-000");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("PAWNTICKETFORMAT", ref sComponentValue))
                {
                    pawnTicketName = Utilities.GetStringValue(sComponentValue);
                    rt = true;
                }
            }
            return (rt);
        }


        public bool GetMaxMdseTransferVoidDays(SiteId siteID, out Int64 maxVoidDays)
        {
            maxVoidDays = 0L;
            var rt = false;
            var brObject = GetBusinessRule(siteID, "PWN_BR-034");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("CL-PWN-244", ref sComponentValue))
                {
                    maxVoidDays = Utilities.GetLongValue(sComponentValue, 1L);
                    rt = true;
                }
            }
            return (rt);
        }

        public bool GetMaxLoanLimit(SiteId siteID, out decimal maxLoanLimit)
        {
            var rt = false;
            maxLoanLimit = 0.0m;
            var brObject = GetBusinessRule(siteID, "PWN_BR-010");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("CL_PWN_0001_MAXLOANAMT", ref sComponentValue))
                {
                    maxLoanLimit = Utilities.GetDecimalValue(sComponentValue, 0);
                    rt = true;
                }
            }
            return (rt);
        }

        public bool GetValidShopRoles(SiteId siteID, out List<string> shopRoles)
        {
            shopRoles = new List<string>();
            var brObject = GetBusinessRule(siteID, "PWN_BR-097");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("SHOP_ROLES", ref sComponentValue))
                {
                    shopRoles.AddRange(sComponentValue.Split('|'));
                    return true;
                }
            }
            return false;
        }

        public bool GetValidateUserLimit(SiteId siteID)
        {
            var brObject = GetBusinessRule(siteID, "PWN_BR-030");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("VALIDATE_USER_LIMIT", ref sComponentValue))
                {
                    if (sComponentValue == "Y")
                        return true;
                }
            }
            return false;
        }

        public bool GetMaxCashDrawerLimit(SiteId siteID, out decimal maxCDLimit)
        {
            var rt = false;
            maxCDLimit = 0.0m;
            var brObject = GetBusinessRule(siteID, "PWN_BR-096");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("CL_PWN-201_MaximumCashDrawerLimits", ref sComponentValue))
                {
                    maxCDLimit = Utilities.GetDecimalValue(sComponentValue, 0);
                    rt = true;
                }
            }
            return (rt);
        }

        public bool IsSeparateTicketForFirearm(SiteId siteID)
        {
            var brObject = GetBusinessRule(siteID, "PWN_BR-116");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("CL_PWN-212SEPARATEFIREARMTKT", ref sComponentValue))
                {
                    if (sComponentValue == "Y")
                        return true;
                }
            }
            return false;
        }

        public bool IsAllowedJewelryGenMDSEOneBuy(SiteId siteID)
        {
            var brObject = GetBusinessRule(siteID, "PWN_BR-116");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("CL_PWN-217JEWELRYGENMDSEONEBUY", ref sComponentValue))
                {
                    if (sComponentValue == "Y")
                        return true;
                }
            }
            return false;
        }

        public int GetMaxItemsInBuyTransaction(SiteId siteID)
        {
            const int maxItems = 0;
            var brObject = GetBusinessRule(siteID, "PWN_BR-117");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("CL_PWN-210MAXITEMSPURCHASE", ref sComponentValue))
                {
                    return Utilities.GetIntegerValue(sComponentValue, 0);
                }
            }
            return (maxItems);
        }


        public int GetPfiMailerAdjustmentDays(SiteId siteID)
        {
            const int pfiMailerDays = 0;
            var brObject = GetBusinessRule(siteID, "PWN_BR-043");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("CL_PWN_262_PFIMAILERADJUSTMENT", ref sComponentValue))
                {
                    return Utilities.GetIntegerValue(sComponentValue, 0);
                }
            }
            return (pfiMailerDays);
        }

        public string GetValidPFIDateWithWaitdaysForBuy(DateTime transactionDate, SiteId siteId)
        {
            DateTime pfiDate;
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Get PFi wait days
            BusinessRuleVO brObject = GetBusinessRule(siteId, "PWN_BR-106");
            bPawnValue = brObject.getComponentValue("CL_PWN-211_WAITDAYSPFIBUY", ref sComponentValue);
            int pfiWaitDays = Utilities.GetIntegerValue(sComponentValue, 0);
            pfiDate = transactionDate.AddDays(pfiWaitDays);
            //CL_PWN_0169_PFIDATENBDADJTREQ
            bool adjustPfiDate = false;
            if (brObject.getComponentValue("CL_PWN_0169_PFIDATENBDADJTREQ", ref sComponentValue))
            {
                if (!string.IsNullOrEmpty(sComponentValue))
                {
                    if (string.Equals(sComponentValue, "Y", StringComparison.OrdinalIgnoreCase))
                    {
                        adjustPfiDate = true;
                    }
                }
            }
            //CL_PWN_0059_PFIDATEADJT
            string pfiDateAdjustmentValue = "";
            bPawnValue = brObject.getComponentValue("CL_PWN_0059_PFIDATEADJT", ref pfiDateAdjustmentValue);
            if (!adjustPfiDate || !bPawnValue || string.IsNullOrEmpty(pfiDateAdjustmentValue))
                return pfiDate.FormatDate();
            bool retValue = false;
            do
            {
                if (new UnderwritePawnLoanUtility(DesktopSession).IsShopClosed(pfiDate))
                {
                    if (pfiDateAdjustmentValue.Equals(CONSTANTB))
                    {
                        pfiDate = pfiDate.AddDays(-1.0d).Date;
                    }
                    else if (pfiDateAdjustmentValue.Equals(CONSTANTF))
                    {
                        pfiDate = pfiDate.AddDays(1.0d).Date;
                    }
                }
                else
                    retValue = true;
            } while (!retValue);

            return pfiDate.FormatDate();
        }

        public decimal GetCashDrawerMaxEndingBalance(SiteId siteID)
        {
            var brObject = GetBusinessRule(siteID, "PWN_BR-094");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("CL_PWN_199MaximumEndingBalance", ref sComponentValue))
                {
                    return Utilities.GetDecimalValue(sComponentValue, 0);
                }
            }
            return (0);
        }

        public decimal GetCustomerInfoRequiredSaleValue(SiteId siteID)
        {
            var brObject = GetBusinessRule(siteID, "PWN_BR-089");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("CL_RTL_240VERIFYCUSTOMERINFOREQUIRED", ref sComponentValue))
                {
                    return Utilities.GetDecimalValue(sComponentValue, 0);
                }
            }
            return (0);
        }

        public BusinessRuleVO GetCustomerBusinessRule(SiteId siteID)
        {
            var brObject = GetBusinessRule(siteID, "PWN_BR-000");
            return brObject;
        }


        public int GetCustomerSurveyCardSaleTransactions(SiteId siteID)
        {
            const int transactions = 0;
            var brObject = GetBusinessRule(siteID, "PWN_BR-139");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("CL-RTL-148_CUST_CARD_SALE", ref sComponentValue))
                {
                    return Utilities.GetIntegerValue(sComponentValue, 0);
                }
            }
            return (transactions);
        }


        public bool IsServiceFeeTaxable(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bValue = false;
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-138");
            brObject.getComponentValue("CL_RTL-128_LAYAWAYSRVFEETAX", ref sComponentValue);

            return sComponentValue == "Y" ? true : false;
        }


        public bool CustomerSignatureOnBackofTicket(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            //Should customer sign back of ticket if there is more than 1 loan on the same day
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-172");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_261_SIGNBACKOFTICKET", ref sComponentValue);

            return sComponentValue == "Y";
        }



        public int GetPoliceHoldDefaultDays(SiteId siteID)
        {
            const int policeHoldDays = 0;
            var brObject = GetBusinessRule(siteID, "PWN_BR-176");
            var sComponentValue = string.Empty;
            if (brObject != null)
            {
                if (brObject.getComponentValue("CL_PWN_267_POLICEHOLDDURATION", ref sComponentValue))
                {
                    return Utilities.GetIntegerValue(sComponentValue, 0);
                }
            }
            return (policeHoldDays);
        }


        public bool IsPartialPaymentAllowedAfterDueDate(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is partial payment allowed on or after due date
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-170");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_257_PARTIALPMTONAFTERDUEDATE", ref sComponentValue);
            return sComponentValue == "Y";
        }



        public bool IsPartialPaymentAllowedAfterPfiDate(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is partial payment allowed on or after due date
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-170");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_258_PARTIALPMTONAFTERPFIDATE", ref sComponentValue);
            return sComponentValue == "Y";
        }

        public decimal GetPartialPaymentMinAmount(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Get minimum partial payment amount
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-171");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_256_PWNPARTIALPMTMIN", ref sComponentValue);
            return Utilities.GetDecimalValue(sComponentValue);

        }

        public bool IsPoliceCardNeededForStore(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is partial payment allowed on or after due date
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-173");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_263_PRINTPOLICECARD", ref sComponentValue);
            return sComponentValue == "Y";
        }

        public bool IsSubpoenaRequiredForReleaseFingerprints(SiteId siteID)
        {
            var sComponentValue = string.Empty;
            var bPawnValue = false;
            //Is partial payment allowed on or after due date
            BusinessRuleVO brObject = GetBusinessRule(siteID, "PWN_BR-177");
            if (brObject != null)
                brObject.getComponentValue("CL_PWN_268_RELFINGSUBPOENA", ref sComponentValue);
            return sComponentValue == "Y";
        }
    }
}
