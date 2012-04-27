using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.ProKnowService;

namespace Common.Controllers.Database
{
    public class PickingSlipHelper
    {
        private List<groupOfValuesOutputType> _ResponseValues;

        public PickingSlipReportContext GetPickingSlipReportContext(List<CustomerProductDataVO> productObjects, List<string> customerNames, bool bGetProKnowDetails)
        {
            var context = new PickingSlipReportContext();
            context.RunDate = ShopDateTime.Instance.FullShopDateTime;

            if (bGetProKnowDetails)
            {
                // Have extra enumeration to go ProKnow with only ONE call
                var lstGroupOfInputValues = new List<groupOfValuesInputType>();
                _ResponseValues = new List<groupOfValuesOutputType>();
                foreach (var productObject in productObjects)
                {
                    // Last step is to thread out individual Picking Slips
                    foreach (var pawnItem in productObject.Items)
                    {
                        if (pawnItem.CategoryCode > 0 &&
                            !string.IsNullOrEmpty(pawnItem.QuickInformation.Manufacturer) &&
                            !string.IsNullOrEmpty(pawnItem.QuickInformation.Model) &&
                            !string.IsNullOrEmpty(pawnItem.Icn))
                        {
                            var valueInputType = new groupOfValuesInputType()
                            {
                                categoryCode = pawnItem.CategoryCode,
                                Manufacturer = pawnItem.QuickInformation.Manufacturer,
                                Model = pawnItem.QuickInformation.Model,
                                responseKey = pawnItem.Icn
                            };
                            lstGroupOfInputValues.Add(valueInputType);
                        }
                    }
                }
                // Make call to ProKnow Service with batch of input values
                if (lstGroupOfInputValues.Count > 0)
                {
                    var proKnowService = new WebServiceProKnow(GlobalDataAccessor.Instance.DesktopSession);
                    var groupOfValuesResponse = proKnowService.GetGroupOfValues(lstGroupOfInputValues);
                    if (groupOfValuesResponse.serviceData != null)
                    {
                        if (groupOfValuesResponse.serviceData.Items[0].GetType() == typeof(groupOfValuesOutputType))
                        {
                            foreach (var o in groupOfValuesResponse.serviceData.Items)
                            {
                                _ResponseValues.Add((groupOfValuesOutputType)o);
                            }
                        }
                    }
                }
            }
            //foreach (CustomerProductDataVO productObject in productObjects)
            for (int i = 0; i < productObjects.Count; i++)
            {
                PickingSlipReportService service = null;

                var productObject = productObjects[i];
                var sCustomerName = customerNames[i];

                if (productObject.GetType() == typeof(PawnLoan))
                {
                    var pLoan = (PawnLoan)productObject;
                    service = new PickingSlipReportService()
                    {
                        OrgNumber = pLoan.OrigTicketNumber,
                        Customer = sCustomerName,
                        DateDue = pLoan.DueDate,
                        PreviousLoanNumber = Utilities.GetIntegerValue(pLoan.PrevTicketNumber, 0),
                        PfiEligible = Utilities.GetDateTimeValue(pLoan.PfiEligible, DateTime.MinValue),
                        PartialPayments = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.CurrentSiteId),
                        CurrentLoanNumber = Utilities.GetIntegerValue(pLoan.TicketNumber, 0),
                        CurrentLoanAmount = pLoan.PartialPaymentPaid ? pLoan.CurrentPrincipalAmount : Utilities.GetDecimalValue(pLoan.Amount, 0M),
                        LoanAmount = Utilities.GetDecimalValue(pLoan.Amount, 0M),
                        Finance = pLoan.InterestAmount,
                        Service = pLoan.ServiceCharge,
                        IsPurchase = false
                    };
                }
                else if (productObject.GetType() == typeof(PurchaseVO))
                {
                    var purchaseObj = (PurchaseVO)productObject;

                    service = new PickingSlipReportService()
                    {
                        OrgNumber = purchaseObj.RefNumber,
                        Customer = sCustomerName,
                        DateDue = purchaseObj.DateMade,
                        PreviousLoanNumber = 0,
                        PfiEligible = Utilities.GetDateTimeValue(purchaseObj.PfiEligible, DateTime.MinValue),
                        PartialPayments=false,
                        CurrentLoanNumber = Utilities.GetIntegerValue(purchaseObj.TicketNumber, 0),
                        CurrentLoanAmount = 0,
                        LoanAmount = Utilities.GetDecimalValue(purchaseObj.Amount, 0M),
                        Finance = 0,
                        Service = 0,
                        IsPurchase = true
                    };
                }
                // Last step is to thread out individual Picking Slips
                int pawnItemNumber = 1;
                foreach (var pawnItem in productObject.Items)
                {
                    var sAnswerIdx = string.Empty;
                    foreach (var ia in pawnItem.Attributes)
                    {
                        var a = ia.Answer;
                        var sAnswerText = a.AnswerText.ToLower();

                        if (sAnswerText.IndexOf("cond ", System.StringComparison.Ordinal) >= 0 && a.AnswerCode != 999)
                        {
                            sAnswerIdx = a.AnswerText.Substring(5, 1);
                            break;
                        }
                    }

                    if (service != null)
                    {
                        PickingSlipReportItem item = new PickingSlipReportItem()
                        {
                            ItemLocation = pawnItem.Location,
                            ItemLocationAisle = pawnItem.Location_Aisle,
                            ItemLocationShelf = pawnItem.Location_Shelf,
                            Amount = pawnItem.ItemAmount,
                            ItemDescription = pawnItem.TicketDescription,
                            ItemNumber = pawnItemNumber
                        };

                        service.Items.Add(item);
                    }
                    pawnItemNumber++;
                }

                context.Service.Add(service);
            }

            return context;
        }
    }
}
