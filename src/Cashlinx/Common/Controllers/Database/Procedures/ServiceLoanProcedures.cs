using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Pawn.Loan;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;

namespace Common.Controllers.Database.Procedures
{
    public static class ServiceLoanProcedures
    {
        //Non DB Functions
        public static bool CheckForOverrides(ServiceTypes typeOfService, string activeCustomerNumber, ref List<PawnLoan> selectedLoans, ref string loansOverrideFailedMessage)
        {
            List<int> transactionsForServiceOverride = new List<int>();
            List<int> selectedLoanNumbers = new List<int>();
            List<OverrideTransaction> transactionsToOverride = new List<OverrideTransaction>();
            List<ManagerOverrideType> overrideTypes = new List<ManagerOverrideType>();
            List<ManagerOverrideTransactionType> transactionTypes = new List<ManagerOverrideTransactionType>();

            foreach (PawnLoan pl in selectedLoans)
            {
                List<string> overridereason = new List<string>();
                ManagerOverrideType overridetype = ManagerOverrideType.PFIE;
                List<ManagerOverrideType> overridetypes = new List<ManagerOverrideType>();
                if (IsOverrideRequired(typeOfService, pl, activeCustomerNumber, ref overridereason, ref overridetypes))
                {
                    OverrideTransaction tran = new OverrideTransaction();
                    tran.ReasonForOverride = new List<Commons.StringValue>();
                    foreach (var s in overridereason)
                        tran.ReasonForOverride.Add(new Commons.StringValue(s));

                    tran.OverrideType = overridetype;
                    if (typeOfService == ServiceTypes.EXTEND)
                        tran.TransactionType = ManagerOverrideTransactionType.EX;
                    else if (typeOfService == ServiceTypes.PICKUP)
                        tran.TransactionType = ManagerOverrideTransactionType.PU;
                    else if (typeOfService == ServiceTypes.RENEW)
                        tran.TransactionType = ManagerOverrideTransactionType.RN;
                    else if (typeOfService == ServiceTypes.ROLLOVER)
                        tran.TransactionType = ManagerOverrideTransactionType.RO;
                    else if (typeOfService == ServiceTypes.PAYDOWN)
                        tran.TransactionType = ManagerOverrideTransactionType.PD;
                    tran.TicketNumber = pl.TicketNumber;
                    transactionsToOverride.Add(tran);
                    transactionsForServiceOverride.Add(pl.TicketNumber);
                }
                selectedLoanNumbers.Add(pl.TicketNumber);
            }
            //Check overrides
            //call override reason form if there are transactions to override
            //If there is only 1 transaction to override go straight to manage override
            //otherwise go through the override reason form
            if (transactionsToOverride.Count == 1)
            {
                //Remove the ticket numbers that need override from selectedloannumbers
                selectedLoanNumbers = (selectedLoanNumbers.Except(transactionsForServiceOverride)).ToList();
                StringBuilder messageToShow = new StringBuilder();
                messageToShow.Append(transactionsToOverride[0].TicketNumber.ToString());
                messageToShow.Append("-");
                foreach (Commons.StringValue reason in transactionsToOverride[0].ReasonForOverride)
                    messageToShow.Append(reason.Value + System.Environment.NewLine);
                overrideTypes.Add(transactionsToOverride[0].OverrideType);
                transactionTypes.Add(transactionsToOverride[0].TransactionType);
                ManageOverrides overrideFrm = new ManageOverrides(GlobalDataAccessor.Instance.DesktopSession, ManageOverrides.OVERRIDE_TRIGGER)
                {
                    MessageToShow = messageToShow.ToString(),
                    ManagerOverrideTypes = overrideTypes,
                    ManagerOverrideTransactionTypes = transactionTypes,
                    TransactionNumbers = transactionsForServiceOverride

                };

                overrideFrm.ShowDialog();
                //If the override did not pass, but there are more selected loans
                //that did not need override then proceed with Service
                //but if all the loans selected needed override then end Service process
                if (!(overrideFrm.OverrideAllowed))
                {
                    RemoveTickets(transactionsForServiceOverride, ref selectedLoans);
                    transactionsForServiceOverride = new List<int>();
                    loansOverrideFailedMessage = "Manager override did not happen.Not proceeding with " + typeOfService.ToString() + " on " +
                                                 transactionsToOverride[0].TicketNumber.ToString();
                    if (selectedLoanNumbers.Count == 0)
                        return false;
                }
                else
                    return true;
            }
            else if (transactionsToOverride.Count > 1)
            {
                //Remove the ticket numbers that need override from _selectedloannumbers
                selectedLoanNumbers = (selectedLoanNumbers.Except(transactionsForServiceOverride)).ToList();
                OverrideReason overridereasonform = new OverrideReason();
                overridereasonform.Transactions = transactionsToOverride;
                overridereasonform.ShowDialog();
                //If the Transactions after the call is 0 that means
                //no override happened hence do not proceed
                List<OverrideTransaction> transactionsOverriden = overridereasonform.Transactions;
                if (transactionsOverriden.Count == 0)
                {
                    if (selectedLoanNumbers.Count == 0)
                    {
                        selectedLoanNumbers = selectedLoanNumbers.Union(transactionsForServiceOverride).ToList();
                        loansOverrideFailedMessage = "Manager override did not happen.Not proceeding with " + typeOfService.ToString();

                        RemoveTickets(selectedLoanNumbers, ref selectedLoans);
                        return false;
                    }
                    else
                    {
                        StringBuilder tktsNotProcessed = new StringBuilder();
                        foreach (int i in transactionsForServiceOverride)
                            tktsNotProcessed.Append(i.ToString() + System.Environment.NewLine);
                        RemoveTickets(transactionsForServiceOverride, ref selectedLoans);
                        loansOverrideFailedMessage = "Manager override did not happen.Not proceeding with " + typeOfService.ToString() + " on " +
                                                     tktsNotProcessed.ToString();
                        return false;
                    }
                }
                //Go through the transactions for which override did go through
                //and only let those transactions proceed
                transactionsForServiceOverride = new List<int>();
                for (int k = 0; k < transactionsOverriden.Count; k++)
                {
                    transactionsForServiceOverride.Add(transactionsOverriden[k].TicketNumber);
                }
                //Go through the list of transactions that passed override and
                //if there are any that were not overridden after the call to override 
                //form show a message to the user
                StringBuilder loansOverrideFailed = new StringBuilder();
                List<int> TicketsToRemove = new List<int>();
                for (int n = 0; n < transactionsToOverride.Count; n++)
                {
                    int index = n;
                    int idx = transactionsForServiceOverride.FindIndex(
                        tktNo => tktNo == transactionsToOverride[index].TicketNumber);
                    if (idx < 0)
                    {
                        TicketsToRemove.Add(transactionsToOverride[n].TicketNumber);
                        loansOverrideFailed.Append(transactionsToOverride[n].TicketNumber.ToString());
                        loansOverrideFailed.Append(System.Environment.NewLine);
                    }
                }
                if (loansOverrideFailed.Length > 0)
                {
                    loansOverrideFailedMessage = "Manager override did not happen for some loans. " +
                                                 System.Environment.NewLine +
                                                 "Not proceeding with " + typeOfService.ToString() +
                                                 " on the following loans "
                                                 + System.Environment.NewLine
                                                 + loansOverrideFailed.ToString();
                    RemoveTickets(TicketsToRemove, ref selectedLoans);
                    return false;
                }
            }
            else
                return true;

            return true;
        }

        private static void RemoveTickets<T>(IList<int> ticketNumbers, ref List<T> selectedLoans) where T : CustomerProductDataVO
        {
            for (int j = 0; j < ticketNumbers.Count; j++)
            {
                int index = j;
                int iDx = selectedLoans.FindIndex(loan => loan.TicketNumber == ticketNumbers[index]);
                if (iDx > -1)
                    selectedLoans.RemoveAt(iDx);
            }
        }

        public static bool CheckCurrentTempStatus<T>(ref List<T> selectedLoans,
                                                     string strUserId, ServiceTypes typeOfService) where T : CustomerProductDataVO
        {
            List<int> tktNumbers = new List<int>();
            List<string> currTempStatus = new List<string>();
            List<string> storeNumbers = new List<string>();

            //foreach (PawnLoan pl in selectedLoans)
            //{
            //    tktNumbers.Add(pl.TicketNumber);
            //    currTempStatus.Add(pl.TempStatus.ToString());
            //    storeNumbers.Add(pl.OrgShopNumber);
            //}
            foreach (T pl in selectedLoans)
            {
                tktNumbers.Add(pl.TicketNumber);
                currTempStatus.Add(pl.TempStatus.ToString());
                storeNumbers.Add(pl.OrgShopNumber);
            }
            DataTable tempStatus = null;

            bool retVal = false;
            do
            {
                string errorCode = "";
                string errorMsg = "";
                retVal = CustomerLoans.CheckAndUpdateTempStatus(tktNumbers, currTempStatus,
                                                                storeNumbers, typeOfService.ToString(),
                                                                strUserId, out tempStatus,
                                                                out errorCode, out errorMsg);

                if (tempStatus != null && tempStatus.Rows.Count > 0)
                {
                    StringBuilder tktNoMessage = new StringBuilder();
                    StringBuilder tktChangeStatusMessage = new StringBuilder();
                    List<TupleType<int, string, string>> pfiTicketNumbers = new List<TupleType<int, string, string>>();
                    List<int> lockedTicketNumbers = new List<int>();
                    foreach (DataRow dr in tempStatus.Rows)
                    {
                        //Show the loans that is being edited now for processing
                        string lockedProcess = "";
                        if (Commons.IsLockedStatus(dr[Tempstatuscursor.TEMPSTATUS].ToString(), ref lockedProcess))
                        {
                            tktNoMessage.Append(dr[Tempstatuscursor.TICKETNUMBER].ToString());
                            tktNoMessage.Append(" is locked by " + lockedProcess);
                            tktNoMessage.Append(System.Environment.NewLine);
                            lockedTicketNumbers.Add(Utilities.GetIntegerValue(
                                dr[Tempstatuscursor.TICKETNUMBER], 0));
                        }
                        else if (dr[Tempstatuscursor.TEMPSTATUS].ToString().Equals("PFI") ||
                                 dr[Tempstatuscursor.TEMPSTATUS].ToString().Equals("PFIW") ||
                                 dr[Tempstatuscursor.TEMPSTATUS].ToString().Equals("PFIS"))
                        {
                            tktChangeStatusMessage.Append(dr[Tempstatuscursor.TICKETNUMBER].ToString());
                            tktChangeStatusMessage.Append(System.Environment.NewLine);
                            TupleType<int, string, string> newPair = new TupleType<int, string, string>(Utilities.GetIntegerValue(
                                                                                                            dr[Tempstatuscursor.TICKETNUMBER], 0), dr[Tempstatuscursor.TEMPSTATUS].ToString(),
                                                                                                        dr[Tempstatuscursor.STORENUMBER].ToString());
                            pfiTicketNumbers.Add(newPair);
                        }
                        else if (dr[Tempstatuscursor.TEMPSTATUS].ToString().Equals("LYPMT"))
                        {
                            tktNoMessage.Append(dr["Ticket_Number"].ToString());
                            tktNoMessage.Append(" is locked by Layaway Payment");
                            tktNoMessage.Append(System.Environment.NewLine);
                            MessageBox.Show(tktNoMessage.ToString());
                            return false;

                        }
                    }
                    if (tktNoMessage.Length > 0)
                    {
                        MessageBox.Show(
                            "Some of the selected transactions are locked. Please try again later" +
                            System.Environment.NewLine +
                            tktNoMessage.ToString());
                        RemoveTickets(lockedTicketNumbers, ref selectedLoans);
                        tktNumbers = tktNumbers.Except(lockedTicketNumbers).ToList();
                    }
                    DialogResult dgr;
                    if (tktChangeStatusMessage.Length > 0)
                    {
                        dgr =
                        MessageBox.Show(
                            "The following loans are in PFI Processing " + System.Environment.NewLine +
                            tktChangeStatusMessage.ToString() + " Do you want to continue?", "Warning",
                            MessageBoxButtons.YesNo);
                        if (dgr == DialogResult.No)
                        {
                            //If the user does not wish to proceed
                            //with the ticket numbers which have been picked up for PFI now
                            //eliminate them from the selected loans list
                            List<int> ticketsToDelete = new List<int>();
                            for (int i = 0; i < pfiTicketNumbers.Count; i++)
                            {
                                ticketsToDelete.Add(pfiTicketNumbers[i].Left);
                            }
                            if (ticketsToDelete.Count > 0)
                            {
                                RemoveTickets(ticketsToDelete, ref selectedLoans);
                                tktNumbers = tktNumbers.Except(ticketsToDelete).ToList();
                            }
                            retVal = true;
                        }
                        else
                        {
                            if (typeOfService == ServiceTypes.PICKUP
                                || typeOfService == ServiceTypes.EXTEND
                                || typeOfService == ServiceTypes.RENEW
                                || typeOfService == ServiceTypes.ROLLOVER
                                || typeOfService == ServiceTypes.PAYDOWN)
                            {
                                List<ManagerOverrideType> overrideTypes = new List<ManagerOverrideType>();
                                List<ManagerOverrideTransactionType> transactionTypes =
                                new List<ManagerOverrideTransactionType>();
                                List<int> TicketNumbersToOverride = new List<int>();
                                for (int i = 0; i < pfiTicketNumbers.Count; i++)
                                {
                                    overrideTypes.Add(ManagerOverrideType.PFIE);
                                    TicketNumbersToOverride.Add(pfiTicketNumbers[i].Left);
                                    if (typeOfService == ServiceTypes.PICKUP)
                                        transactionTypes.Add(ManagerOverrideTransactionType.PU);
                                    else if (typeOfService == ServiceTypes.EXTEND)
                                        transactionTypes.Add(ManagerOverrideTransactionType.EX);
                                    else if (typeOfService == ServiceTypes.RENEW)
                                        transactionTypes.Add(ManagerOverrideTransactionType.RN);
                                }

                                tktChangeStatusMessage.Insert(0, "The following loan(s) are in PFI processing ");
                                var overrideFrm = new ManageOverrides(GlobalDataAccessor.Instance.DesktopSession, ManageOverrides.OVERRIDE_TRIGGER)
                                {
                                    MessageToShow = tktChangeStatusMessage.ToString(),
                                    ManagerOverrideTypes = overrideTypes,
                                    ManagerOverrideTransactionTypes = transactionTypes,
                                    TransactionNumbers = TicketNumbersToOverride

                                };
                                overrideFrm.ShowDialog();
                                if (!(overrideFrm.OverrideAllowed))
                                {
                                    MessageBox.Show("Manager override did not happen. " +
                                                    tktChangeStatusMessage.ToString());
                                    RemoveTickets(TicketNumbersToOverride, ref selectedLoans);
                                    tktNumbers = (tktNumbers.Except(TicketNumbersToOverride)).ToList();
                                }
                            }
                        }
                    }
                    if (tktNumbers.Count == 0)
                    {
                        return true;
                    }

                    //redo the check and update for the remaining ticket numbers
                    if (pfiTicketNumbers.Count == 0)
                        retVal = true;
                    else
                    {
                        currTempStatus = new List<string>();
                        tktNumbers = new List<int>();
                        storeNumbers = new List<string>();
                        for (int i = 0; i < pfiTicketNumbers.Count; i++)
                        {
                            tktNumbers.Add(pfiTicketNumbers[i].Left);
                            currTempStatus.Add(pfiTicketNumbers[i].Mid);
                            storeNumbers.Add(pfiTicketNumbers[i].Right);
                        }
                    }
                }
                else
                    retVal = true;
            } while (!retVal);
            return true;
        }

        public static bool UpdateLayawayTempStatus()
        {
            bool retval = true;

            return retval;
        }

        public static bool IsOverrideRequired(ServiceTypes serviceType, PawnLoan custPawnLoan, string TicketHolderCustNumber, ref List<string> overrideReason, ref List<ManagerOverrideType> overrideType)
        {
            //Get the current date
            var currentDate = ShopDateTime.Instance.ShopDate;
            if (serviceType == ServiceTypes.PICKUP)
            {
                //If the current date is > pfi eligible date and the temp status is neither
                //PFI verify nor wait then pickup override is required
                //SR 07/26/2011 CQ INTG100013617
                /* if (currentDate.Date > custPawnLoan.PfiEligible.Date &&
                     !(Utilities.IsPFI(custPawnLoan.TempStatus)))
                 {
                     overrideReason.Add(serviceType.ToString() + " " + Common.GetMessageString("OverrideReasonPFIEligible"));
                     overrideType.Add(ManagerOverrideType.PFIE);
                 }*/

                //If the current date is > pfi eligible date and the temp status is either
                //PFI verify or wait then pickup override is required
                if (currentDate.Date > custPawnLoan.PfiEligible.Date &&
                    Utilities.IsPFI(custPawnLoan.TempStatus))
                {
                    overrideReason.Add(string.Format("{0} {1}", serviceType, Commons.GetMessageString("OverrideReasonPFIInProcess")));
                    overrideType.Add(ManagerOverrideType.PFIP);
                }
                //If the customer is not the pledgor and loan in pawn is < number of days specified
                //in the 116 rule and if legal doc required is set to Y which is in
                //the business rule PWN_BR-16 CL_PWN_188 then override is required
                //We can get the value from the session business rule since the rules for pickup
                //are the rules set for the store where the pickup is happening
                var brLegalDocRequired = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO["PWN_BR-016"];
                var legalComponentValue = string.Empty;
                var daysComponentValue = string.Empty;
                var hoursComponentValue = string.Empty;

                var ruleFound = brLegalDocRequired.getComponentValue("CL_PWN_0188_LEGALDOCREQUIRED", ref legalComponentValue);
                var mindaysruleFound = brLegalDocRequired.getComponentValue("CL_PWN_0116_RESTRICTPICKUP", ref daysComponentValue);
                var hoursRuleFound = brLegalDocRequired.getComponentValue("CL_PWN_0076_MINWAITINPAWN", ref hoursComponentValue);

                if (hoursRuleFound && hoursComponentValue != "0")
                {
                    var minHoursInPawn = Utilities.GetIntegerValue(hoursComponentValue, 0);
                    if ((ShopDateTime.Instance.FullShopDateTime - custPawnLoan.OriginationDate).TotalHours < minHoursInPawn)
                    {
                        overrideReason.Add(serviceType.ToString() + " " + "Loan is not due eligible for pickup for " + minHoursInPawn.ToString() + " hours from the time of pawn." + System.Environment.NewLine);
                        overrideType.Add(ManagerOverrideType.PWNP72);
                        return true;
                    }
                }

                if (custPawnLoan.CustomerNumber != TicketHolderCustNumber && ruleFound && mindaysruleFound)
                {
                    var daysInPawnRule = Utilities.GetIntegerValue(daysComponentValue, 0);
                    if (daysInPawnRule != 0)
                    {
                        if ((ShopDateTime.Instance.ShopDate - custPawnLoan.OriginationDate).Days < daysInPawnRule &&
                            legalComponentValue == "Y")
                        {
                            overrideReason.Add(serviceType.ToString() + " " + Commons.GetMessageString("OverrideReasonLegalDoc"));
                            overrideType.Add(ManagerOverrideType.DOC);
                        }
                    }
                }
            }
            else if (serviceType == ServiceTypes.EXTEND)
            {
                //If the current date is > pfi eligible date and the temp status is neither
                //PFI verify nor wait then pickup override is required
                //SR 07/26/2011 CQ INTG100013617
                /*if (currentDate.Date > custPawnLoan.PfiEligible.Date &&
                    !Utilities.IsPFI(custPawnLoan.TempStatus))
                {
                    overrideReason.Add(serviceType.ToString() + " " + Common.GetMessageString("OverrideReasonPFIEligible"));
                    overrideType.Add(ManagerOverrideType.PFIE);
                }*/

                //If the current date is > pfi eligible date and the temp status is either
                //PFI verify or wait then pickup override is required
                if (currentDate.Date > custPawnLoan.PfiEligible.Date &&
                    (Utilities.IsPFI(custPawnLoan.TempStatus)))
                {
                    overrideReason.Add(serviceType + " " + Commons.GetMessageString("OverrideReasonPFIInProcess"));
                    overrideType.Add(ManagerOverrideType.PFIP);
                }

                if (custPawnLoan.HoldDesc == HoldData.POLICE_HOLD || custPawnLoan.HoldDesc.Equals("Police Hold", StringComparison.OrdinalIgnoreCase))
                {
                    overrideReason.Add(Commons.GetMessageString("OverrideReasonPoliceHold"));
                    overrideType.Add(ManagerOverrideType.WV);
                }
            }
            else if (serviceType == ServiceTypes.RENEW ||
                     serviceType == ServiceTypes.ROLLOVER ||
                     serviceType == ServiceTypes.PAYDOWN)
            {
                if (custPawnLoan.HoldDesc == HoldData.POLICE_HOLD || custPawnLoan.HoldDesc.Equals("Police Hold", StringComparison.OrdinalIgnoreCase))
                {
                    overrideReason.Add(Commons.GetMessageString("OverrideReasonPoliceHold"));
                    overrideType.Add(ManagerOverrideType.WV);
                }
                //SR 07/26/2011 CQ INTG100013617
                /*if (currentDate.Date > custPawnLoan.PfiEligible.Date &&
                    !Utilities.IsPFI(custPawnLoan.TempStatus))
                {
                    overrideReason.Add(serviceType.ToString() + " " + Common.GetMessageString("OverrideReasonPFIEligible"));
                    overrideType.Add(ManagerOverrideType.PFIE);
                }*/

                if (currentDate.Date.Date > custPawnLoan.PfiEligible.Date
                    && Utilities.IsPFI(custPawnLoan.TempStatus))
                {
                    overrideReason.Add(serviceType + " " + Commons.GetMessageString("OverrideReasonPFIInProcess"));
                    overrideType.Add(ManagerOverrideType.PFIP);
                }
            }
            if (overrideReason.Count > 0)
                return true;
            return false;
        }

        public static void SetBusinessRules(ref PawnLoan pawnLoan, SiteId siteID, string strStoreNumber, ServiceTypes serviceType)
        {
            pawnLoan.PickupAllowed = true;
            pawnLoan.IsExtensionAllowed = true;
            pawnLoan.PickupNotAllowedReason = string.Empty;
            pawnLoan.ExtensionNotAllowedReason = string.Empty;
            pawnLoan.ServiceAllowed = true;
            pawnLoan.ServiceMessage = string.Empty;
            //Run through the business rules pertaining to the pawn loan and the origination state
            //to determine whether the service is allowed
            //Business rule check for Pickup
            if (serviceType == ServiceTypes.PICKUP)
            {
                var pickupRestrictionReason = string.Empty;
                if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPickupRestricted(siteID,
                                                               GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.
                                                               CustomerNumber,
                                                               pawnLoan.CustomerNumber, pawnLoan.MadeTime,
                                                               Utilities.GetDateTimeValue(ShopDateTime.Instance.ShopTransactionTime, DateTime.Now),
                                                               ref pickupRestrictionReason))
                {
                    pawnLoan.PickupAllowed = false;
                    pawnLoan.PickupNotAllowedReason = pickupRestrictionReason;
                }
                else
                {
                    if (pickupRestrictionReason.Length > 0)
                        pawnLoan.ServiceMessage = pickupRestrictionReason;
                }

                if (pawnLoan.OrgShopNumber != strStoreNumber)
                {
                    pawnLoan.PickupNotAllowedReason = "Pickup can only be in the store where the loan originated";
                    pawnLoan.PickupAllowed = false;
                }

                if (pawnLoan.HoldDesc == Commons.GetHoldDescription(HoldTypes.POLICEHOLD))
                {
                    pawnLoan.PickupNotAllowedReason +=
                    Commons.GetMessageString("PoliceHoldPickupIneligibleMsg") + System.Environment.NewLine;

                    pawnLoan.PickupAllowed = false;
                }
            }
            //Business rule check for Pickup
            //If the IsExtensionAllowedBeforeDueDate is Y and if the pawn loan's due date
            //is greater than today's date allow the extension. 
            //If the IsExtensionAllowedBeforeDueDate is N and if the pawn loan's due date
            //is greater than today's date do not allow the extension
            //In either cases, if the due date is past allow the extension
            if (serviceType == ServiceTypes.EXTEND)
            {
                if (pawnLoan.IsExtensionAllowed && pawnLoan.DueDate > ShopDateTime.Instance.ShopDate)
                {
                    if (!(new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsExtensionAllowedBeforeDueDate(siteID)))
                    {
                        pawnLoan.ExtensionNotAllowedReason += "This loan cannot be extended prior to the due date" +
                                                              System.Environment.NewLine;
                        pawnLoan.IsExtensionAllowed = false;
                    }
                }
                if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(siteID))
                {
                    string extTerm=(new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetStateExtensionTerm(siteID));
                    if (extTerm.Equals(ExtensionTerms.MONTHLY.ToString()))
                    {
                        ExtensionTerms extensionType;
                        bool retVal = ExtensionProcedures.ExtensionEligibility(pawnLoan.DateMade,
                                                                             ShopDateTime.Instance.ShopDate,
                                                                             pawnLoan.DueDate,
                                                                             pawnLoan.PfiNote,
                                                                             pawnLoan.PartialPaymentPaid ? pawnLoan.LastPartialPaymentDate : DateTime.MaxValue,
                                                                             pawnLoan.PfiEligible,
                                                                             out extensionType);
                        if (retVal)
                            pawnLoan.ExtensionType = extensionType;
                        else
                        {
                            pawnLoan.ExtensionNotAllowedReason += "Loan does not follow eligibility rules" + System.Environment.NewLine;
                            pawnLoan.IsExtensionAllowed = false;
                        }
                    }
                }


            }
            //Common business rules for pickup and extend

            if (pawnLoan.HoldDesc == Commons.GetHoldDescription(HoldTypes.BKHOLD))
            {
                pawnLoan.PickupNotAllowedReason +=
                Commons.GetMessageString("BankruptcyHoldPickupIneligibleMsg") + System.Environment.NewLine;
                pawnLoan.ExtensionNotAllowedReason += Commons.GetMessageString("BankruptcyHoldPickupIneligibleMsg") + System.Environment.NewLine;
                pawnLoan.PickupAllowed = false;
                pawnLoan.IsExtensionAllowed = false;
            }

            if (serviceType == ServiceTypes.PICKUP)
            {
                if (!(pawnLoan.PickupAllowed))
                {
                    pawnLoan.ServiceAllowed = false;
                    pawnLoan.ServiceMessage = pawnLoan.PickupNotAllowedReason;
                }
            }
            if (serviceType == ServiceTypes.EXTEND)
            {
                if (!(pawnLoan.IsExtensionAllowed))
                {
                    pawnLoan.ServiceAllowed = false;
                    pawnLoan.ServiceMessage = pawnLoan.ExtensionNotAllowedReason;
                }
            }

            if (serviceType == ServiceTypes.ROLLOVER || serviceType == ServiceTypes.PAYDOWN ||
                serviceType == ServiceTypes.RENEW)
            {
                var rolloverRestrictMsg = string.Empty;

                if (!(new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsRenewalAtDifferentStoreAllowed(siteID)))
                {
                    //If we cannot process a renewal originating at a different store,
                    //verify that the originating shop number is equal to the current shop number
                    if (!siteID.StoreNumber.Equals(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber))
                    {
                        pawnLoan.ServiceAllowed = false;
                        pawnLoan.ServiceMessage +=
                        "Rollover cannot be performed at a non-origination store." +
                        System.Environment.NewLine;
                    }
                }

                bool rolloverAllowedBeforeDueDate;
                bool rolloverRenewSameDateMade;
                bool rolloverRestrictPickUpToOriginalCustomer;
                bool rolloverRenewalsBeforeConversionDate;

                if ((new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPawnLoanRolloverAllowedBeforeDueDate(siteID,
                                                                                    out rolloverAllowedBeforeDueDate,
                                                                                    out rolloverRenewSameDateMade,
                                                                                    out rolloverRestrictPickUpToOriginalCustomer,
                                                                                    out rolloverRenewalsBeforeConversionDate)))
                {
                    if (!rolloverAllowedBeforeDueDate)
                    {
                        if (ShopDateTime.Instance.ShopDate < pawnLoan.DueDate)
                        {
                            pawnLoan.ServiceAllowed = false;
                            pawnLoan.ServiceMessage += "Rollover not allowed before Due Date." +
                                                       System.Environment.NewLine;
                        }
                    }
                    if (!rolloverRenewSameDateMade)
                    {
                        if (ShopDateTime.Instance.ShopDate == pawnLoan.DateMade)
                        {
                            pawnLoan.ServiceAllowed = false;
                            pawnLoan.ServiceMessage +=
                            "Rollover not allowed on the same date the pawn loan was made." +
                            System.Environment.NewLine;
                        }
                    }
                    if (rolloverRestrictPickUpToOriginalCustomer)
                    {
                        if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsRolloverRestricted(siteID,
                                                                         GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber,
                                                                         pawnLoan.CustomerNumber, pawnLoan.MadeTime,
                                                                         ShopDateTime.Instance.ShopDate,
                                                                         ref rolloverRestrictMsg))
                        {
                            pawnLoan.ServiceAllowed = false;
                            pawnLoan.ServiceMessage += rolloverRestrictMsg +
                                                       System.Environment.NewLine;
                        }
                    }

                    /*
                    * TODO - We need the c_date field in the pawn header to make
                    * this determination
                    if (rolloverRenewalsBeforeConversionDate)
                    {                        
                    pawnLoan.ServiceAllowed = false;
                    pawnLoan.ServiceMessage += "Renewals are not allowed before Conversion Date." + System.Environment.NewLine;
                    }*/
                }

                else
                {
                    pawnLoan.ServiceAllowed = false;
                    pawnLoan.ServiceMessage += "Error occured while validating PawnLoan rollover allowance before Due Date." + Environment.NewLine;
                }

            }
            if (serviceType == ServiceTypes.PARTIALPAYMENT)
            {
                bool paymentOnOrAfterDueDate = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowedAfterDueDate(siteID);
                bool paymentOnOrAfterPfiDate = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowedAfterPfiDate(siteID);
                DateTime origDueDate = pawnLoan.DueDate;
                DateTime origPfiDate = pawnLoan.PfiEligible;
                if (pawnLoan.IsExtended && siteID.State == States.Indiana)
                {

                origDueDate = pawnLoan.OrigDueDate;
                origPfiDate = pawnLoan.OrigPfiDate;

                }
                if (!paymentOnOrAfterDueDate)
                {
                    if (ShopDateTime.Instance.ShopDate >= origDueDate)
                    {
                        pawnLoan.ServiceAllowed = false;
                        pawnLoan.ServiceMessage += "Partial Payments are not allowed on or after the due date "
                            + System.Environment.NewLine;
                    }
                }
                /*if (pawnLoan.PartialPaymentPaid)
                {
                    if (pawnLoan.LastPartialPaymentDate != DateTime.MaxValue && pawnLoan.LastPartialPaymentDate == ShopDateTime.Instance.ShopDate)
                    {
                        pawnLoan.ServiceAllowed = false;
                        pawnLoan.ServiceMessage += "A Partial payment was already made today "
                            + System.Environment.NewLine;

                    }
                }*/
                if (!paymentOnOrAfterPfiDate)
                {
                    if (ShopDateTime.Instance.ShopDate >= origPfiDate)
                    {
                        pawnLoan.ServiceAllowed = false;
                        pawnLoan.ServiceMessage += "Partial Payments are not allowed on after the PFI date "
                            + System.Environment.NewLine;
                    }
                }

            }
        }

        /// <summary>
        /// Calculates the fees based on the type of service
        /// The fees are calculated based on the loan's originating store rules
        /// whereas the ability to waive or prorate a fee is based on the store where
        /// the service is being done which is passed in as currentstoreid
        /// Latefee is calculated in the database when the loans are pulled up for 
        /// a customer and that is passed in as well for pickup otherwise its 0
        /// </summary>
        /// <param name="currentStoreSiteId"></param>
        /// <param name="typeOfService"></param>
        /// <param name="lateFee"></param>
        /// <param name="otherTranServiceAmt"></param>
        /// <param name="pawnLoan"></param>
        /// <param name="underwritePawnLoanVO"></param>
        /// <param name="pickupInterest"></param>
        /// <param name="pickupServiceAmt"></param>
        /// <param name="otherTranInterest"></param>
        /// <returns></returns>
        public static PawnLoan GetLoanFees(SiteId currentStoreSiteId, ServiceTypes typeOfService,
                                           decimal pickupInterest, decimal pickupServiceAmt, decimal otherTranInterest, decimal otherTranServiceAmt, PawnLoan pawnLoan, out UnderwritePawnLoanVO underwritePawnLoanVO)
        {
            decimal currentValue = 0.0M;

            string feedate = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                             ShopDateTime.Instance.ShopTime.ToString();

            string orgdate = pawnLoan.OriginationDate.ToShortDateString() + " " +
                             pawnLoan.MadeTime.TimeOfDay.ToString();

            SiteId siteId = new SiteId()
            {
                Alias = GlobalDataAccessor.Instance.CurrentSiteId.Alias,
                Company = GlobalDataAccessor.Instance.CurrentSiteId.Company,
                Date = ShopDateTime.Instance.ShopDate,
                LoanAmount = pawnLoan.Amount,
                State = pawnLoan.OrgShopState,
                StoreNumber = pawnLoan.OrgShopNumber,
                TerminalId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId
            };

            SiteId siteIdOrg = new SiteId()
            {
                Alias = GlobalDataAccessor.Instance.CurrentSiteId.Alias,
                Company = GlobalDataAccessor.Instance.CurrentSiteId.Company,
                Date = pawnLoan.OriginationDate,
                LoanAmount = pawnLoan.Amount,
                State = pawnLoan.OrgShopState,
                StoreNumber = pawnLoan.OrgShopNumber,
                TerminalId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId
            };

            // Create UnderWrite Pawn Loan object to be used for current date calculations
            var upw = new UnderwritePawnLoanUtility(GlobalDataAccessor.Instance.DesktopSession);
            // Create underwrite pawn loan object to be used for originating date calculations
            var upwOrg = new UnderwritePawnLoanUtility(GlobalDataAccessor.Instance.DesktopSession);
            // call underwrite Pawn Loan for orig
            bool renewFinanceSame = false;
            if (typeOfService == ServiceTypes.RENEW ||
                typeOfService == ServiceTypes.ROLLOVER ||
                typeOfService == ServiceTypes.EXTEND)
            {
                upwOrg.RunUWP(siteIdOrg);
                // Check for Grandfathering FinanceCharge PWN_BR-48
                upwOrg.PawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0045_RENEWFINANCESAME", out currentValue);
                renewFinanceSame = (currentValue > 0.0M);
            }
            else
            {
                upw.RunUWP(siteId);
            }

            //If grandfathering of interest rates is enabled, use the originating
            //date underwriting object 
            if (renewFinanceSame)
            {
                underwritePawnLoanVO = upwOrg.PawnLoanVO;
            }
            else
            {
                underwritePawnLoanVO = upw.PawnLoanVO;
            }


            PawnLoan _PawnLoan = Utilities.CloneObject(pawnLoan);
            if (pickupInterest != 0)
                _PawnLoan.PickupLateFinAmount = pickupInterest;
            if (pickupServiceAmt != 0)
                _PawnLoan.PickupLateServAmount = pickupServiceAmt;
            if (otherTranInterest != 0)
                _PawnLoan.OtherTranLateFinAmount = otherTranInterest;
            if (otherTranServiceAmt != 0)
                _PawnLoan.OtherTranLateServAmount = otherTranServiceAmt;

            //Compute rollover (renew/paydown) fees
            if (typeOfService == ServiceTypes.RENEW ||
                typeOfService == ServiceTypes.PAYDOWN ||
                typeOfService == ServiceTypes.ROLLOVER)
            {
                _PawnLoan.Fees = new List<Fee>();

                // Get the total finance charge on the loan which is the interest amount
                Fee IntAmtFee = new Fee()
                {
                    FeeType = FeeTypes.INTEREST,
                    Value = Math.Round(underwritePawnLoanVO.totalFinanceCharge, 2),
                    OriginalAmount = Math.Round(underwritePawnLoanVO.totalFinanceCharge, 2),
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now),
                    CanBeProrated = false,
                    CanBeWaived = false
                };
                UpdatePawnLoanFee(_PawnLoan, IntAmtFee);

                // CL_PWN_0013_MININTAMT
                underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0013_MININTAMT", out currentValue);
                Fee MinIntAmtFee = new Fee()
                {
                    FeeType = FeeTypes.MINIMUM_INTEREST,
                    Value = currentValue,
                    OriginalAmount = currentValue,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now),
                    CanBeProrated = false,
                    CanBeWaived = false
                };
                UpdatePawnLoanFee(_PawnLoan, MinIntAmtFee);

                // CL_PWN_0018_SETUPFEEAMT
                underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0018_SETUPFEEAMT", out currentValue);
                Fee SetupFee = new Fee()
                {
                    FeeType = FeeTypes.SETUP,
                    Value = currentValue,
                    OriginalAmount = currentValue,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now),
                    CanBeProrated = false,
                    CanBeWaived = false
                };
                UpdatePawnLoanFee(_PawnLoan, SetupFee);

                // CL_PWN_0022_CITYFEEAMT
                underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0022_CITYFEEAMT", out currentValue);
                Fee CityFee = new Fee()
                {
                    FeeType = FeeTypes.CITY,
                    Value = currentValue,
                    OriginalAmount = currentValue,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now),
                    CanBeProrated = false,
                    CanBeWaived = false
                };
                UpdatePawnLoanFee(_PawnLoan, CityFee);

                // CL_PWN_0115_PREPAIDCITYFEEAMT
                underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0115_PPCITYFEEAMT", out currentValue);
                Fee PPDCityFee = new Fee()
                {
                    FeeType = FeeTypes.PREPAID_CITY,
                    Value = currentValue,
                    OriginalAmount = currentValue,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now),
                    CanBeProrated = false,
                    CanBeWaived = false
                };
                UpdatePawnLoanFee(_PawnLoan, PPDCityFee);

                // CL_PWN_0026_FIREARMFEEAMT
                underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0026_FIREARMFEEAMT", out currentValue);
                Fee FirearmFee = new Fee()
                {
                    FeeType = FeeTypes.FIREARM,
                    Value = currentValue,
                    OriginalAmount = currentValue,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now),
                    CanBeProrated = false,
                    CanBeWaived = false
                };
                UpdatePawnLoanFee(_PawnLoan, FirearmFee);

                // CL_PWN_0040_PFIMAILFEE
                underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0040_PFIMAILFEE", out currentValue);
                Fee PFIMailFee = new Fee()
                {
                    FeeType = FeeTypes.MAILER_CHARGE,
                    Value = currentValue,
                    OriginalAmount = currentValue,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now),
                    CanBeProrated = false,
                    CanBeWaived = false
                };
                UpdatePawnLoanFee(_PawnLoan, PFIMailFee);

                // CL_PWN_0030_STRGFEE
                underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0030_STRGFEE", out currentValue);
                Fee StorageFee = new Fee()
                {
                    FeeType = FeeTypes.STORAGE,
                    Value = currentValue,
                    OriginalAmount = currentValue,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now),
                    CanBeProrated = false,
                    CanBeWaived = false
                };
                UpdatePawnLoanFee(_PawnLoan, StorageFee);
            }

            if (typeOfService == ServiceTypes.PICKUP)
            {

                if (_PawnLoan.PickupLateFinAmount > _PawnLoan.InterestAmount && _PawnLoan.InterestAmount > 0)
                {
                    _PawnLoan.CyclesLate = Utilities.GetIntegerValue(Math.Floor(_PawnLoan.PickupLateFinAmount / _PawnLoan.InterestAmount));
                }


                //Add the late fee amount to the fee collection
                if (_PawnLoan.PickupLateFinAmount != 0 || _PawnLoan.PickupLateServAmount != 0)
                {
                    //Check if late fee can be waived
                    var canWaiveLateFee = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).CanWaiveLateFee(currentStoreSiteId);
                    var newLateFee = new Fee
                    {
                        Value = _PawnLoan.PickupLateFinAmount + _PawnLoan.PickupLateServAmount,
                        FeeType = FeeTypes.LATE,
                        FeeState = FeeStates.ASSESSED,
                        FeeDate = Utilities.GetDateTimeValue(feedate, ShopDateTime.Instance.FullShopDateTime),
                        Prorated = false,
                        OriginalAmount = _PawnLoan.PickupLateFinAmount + _PawnLoan.PickupLateServAmount,
                        CanBeWaived = canWaiveLateFee
                    };
                    UpdatePawnLoanFee(_PawnLoan, newLateFee);
                }

                if (_PawnLoan.PfiMailerSent)
                {
                    var mailerFeeValue = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetPFIMailerFee(
                        GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);
                    var mailerFee = new Fee
                    {
                        Value = mailerFeeValue,
                        FeeType = FeeTypes.MAILER_CHARGE,
                        FeeState = FeeStates.ASSESSED,
                        FeeDate = Utilities.GetDateTimeValue(feedate, ShopDateTime.Instance.FullShopDateTime),
                        Prorated = false,
                        OriginalAmount = mailerFeeValue,
                        CanBeWaived = false
                    };
                    UpdatePawnLoanFee(_PawnLoan, mailerFee);
                }

                //Add lost ticket fee
                if (pawnLoan.LostTicketInfo != null && pawnLoan.LostTicketInfo.LostTicketFee > 0)
                {
                    //Check if lost ticket fee can be waived
                    bool canWaiveLostTicketFee = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).CanWaiveLostTicketFee(currentStoreSiteId);

                    Fee lostTktFee = new Fee
                    {
                        Value = pawnLoan.LostTicketInfo.LostTicketFee,
                        FeeType = FeeTypes.LOST_TICKET,
                        FeeState = FeeStates.ASSESSED,
                        FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now),
                        Prorated = false,
                        OriginalAmount = pawnLoan.LostTicketInfo.LostTicketFee,
                        CanBeWaived = canWaiveLostTicketFee
                    };
                    UpdatePawnLoanFee(_PawnLoan, lostTktFee);
                }
                else
                {
                    //If lost ticket is not set in the pawn loan but if the lost ticket fee is there in 
                    //the pawn loan list void it
                    int iDx = pawnLoan.Fees.FindIndex(l => l.FeeType == FeeTypes.LOST_TICKET);
                    if (iDx >= 0)
                    {
                        Fee lostTktFee = new Fee
                        {
                            Value = pawnLoan.Fees[iDx].Value,
                            FeeType = FeeTypes.LOST_TICKET,
                            FeeState = FeeStates.VOID,
                            FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now),
                            Prorated = false,
                            OriginalAmount = pawnLoan.Fees[iDx].OriginalAmount

                        };
                        UpdatePawnLoanFee(_PawnLoan, lostTktFee);
                    }
                }

                //Minimum Interest at Redemption
                underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0043_MININTAMTATREDEMPTION", out currentValue);
                if (currentValue > 0)
                {
                    Fee fee = new Fee()
                    {
                        FeeType = FeeTypes.MINIMUM_INTEREST_AT_REDEMPTION,
                        Value = currentValue,
                        OriginalAmount = currentValue,
                        FeeState = FeeStates.ASSESSED,
                        FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
                    };
                    UpdatePawnLoanFee(_PawnLoan, fee);
                }

                underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0042_AUTOEXTFEE", out currentValue);
                if (currentValue > 0)
                {
                    //Check if auto extend fee can be waived
                    bool canWaiveAutoExtendFee = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).CanWaiveAutoExtendFee(currentStoreSiteId);
                    Fee fee = new Fee()
                    {
                        FeeType = FeeTypes.AUTOEXTEND,
                        Value = currentValue,
                        OriginalAmount = currentValue,
                        FeeState = FeeStates.ASSESSED,
                        FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now),
                        CanBeWaived = canWaiveAutoExtendFee
                    };
                    UpdatePawnLoanFee(_PawnLoan, fee);
                }

                //If gun lock not there, get gun lock fee
                foreach (Item items in pawnLoan.Items)
                {
                    if (items.IsGun && !(items.HasGunLock))
                    {
                        underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0190_GUNLOCKFEE", out currentValue);
                        if (currentValue > 0)
                        {
                            //Check if Gun lock fee can be waived
                            bool canWaiveGunLockFee = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).CanWaiveGunLockFee(currentStoreSiteId);
                            Fee fee = new Fee()
                            {
                                FeeType = FeeTypes.GUNLOCK,
                                Value = currentValue,
                                CanBeWaived = canWaiveGunLockFee,
                                CanBeProrated = false,
                                Prorated = false,
                                FeeState = FeeStates.ASSESSED,
                                OriginalAmount = currentValue,
                                FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
                            };
                            UpdatePawnLoanFee(_PawnLoan, fee);
                        }
                        break;
                    }
                }
            }

            if (typeOfService == ServiceTypes.EXTEND)
            {
                underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0037_TICKETFEE", out currentValue);
                if (currentValue > 0)
                {
                    Fee tktFee = new Fee()
                    {
                        FeeType = FeeTypes.TICKET,
                        Value = currentValue,
                        OriginalAmount = currentValue,
                        FeeState = FeeStates.ASSESSED,
                        FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
                    };
                    UpdatePawnLoanFee(_PawnLoan, tktFee);
                }
            }
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0033_MAXSTRGFEE", out currentValue);
            if (currentValue > 0)
            {
                Fee maxStrgfee = new Fee()
                {
                    FeeType = FeeTypes.STORAGE_MAXIMUM,
                    Value = currentValue,
                    OriginalAmount = currentValue,
                    FeeState = FeeStates.ASSESSED,
                    FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
                };
                UpdatePawnLoanFee(_PawnLoan, maxStrgfee);
            }
            if (typeOfService == ServiceTypes.PARTIALPAYMENT)
            {
                if (otherTranInterest > 0)
                {
                    Fee lateFee = new Fee()
                    {
                        FeeType = FeeTypes.LATE,
                        Value = otherTranInterest,
                        OriginalAmount = otherTranInterest,
                        FeeState = FeeStates.PAID,
                        FeeDate = Utilities.GetDateTimeValue(feedate, DateTime.Now)
                    };
                    UpdatePawnLoanFee(_PawnLoan, lateFee);

                }
            }

            return _PawnLoan;
        }

        private static void UpdatePawnLoanFee(PawnLoan pawnLoan, Fee fee)
        {
            int iDx = pawnLoan.Fees.FindIndex(l => l.FeeType == fee.FeeType);

            if (iDx < 0)
                pawnLoan.Fees.Add(fee);
            else
            {
                pawnLoan.Fees.RemoveAt(iDx);
                pawnLoan.Fees.Insert(iDx, fee);
            }
        }

        public static decimal GetDailyAmount(ExtensionTerms _extensionType, decimal loanInterestAmount, decimal serviceCharge)
        {
            decimal dailyAmount = 0.0M;
            if (_extensionType == ExtensionTerms.MONTHLY)
                dailyAmount = Math.Round(loanInterestAmount + serviceCharge, 3);
            else
                dailyAmount = Math.Round(loanInterestAmount / 30, 4) + Math.Round(serviceCharge / 30, 4);
            return dailyAmount;
        }

        public static string GetAmountToExtend(int numberOfDaysToExtend, decimal dailyAmount)
        {
            return Math.Round((numberOfDaysToExtend * dailyAmount), 2).ToString("f2");
        }

        public static int GetNumberOfDaysToExtendBy(ExtensionTerms _extensionType)
        {
            if (_extensionType == ExtensionTerms.MONTHLY)
                return 30;
            return 1;
        }

        public static string GetNumberOfExtendDaysFromExtensionAmount(decimal amountToExtend, decimal dailyAmount)
        {
            if (dailyAmount == decimal.Zero)
            {
                return "0";
            }

            return (Math.Truncate(amountToExtend / dailyAmount)).ToString();
        }
    }
}
