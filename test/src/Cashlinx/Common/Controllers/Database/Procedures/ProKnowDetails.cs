/************************************************************************
* Namespace:       CashlinxDesktop.DesktopProcedures
* Class:           ProKnowDetails
* 
* Description      The class parses data from a ProKnow WebService
*                  lookup.
* 
* History
* David D Wise, Initial Development
* 
* **********************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Common.Controllers.Application;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Common.ProKnowService;

namespace Common.Controllers.Database.Procedures
{
    public class ProKnowDetails
    {
        public DesktopSession DesktopSession { get; private set; }

        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

        public enum ProKnowLookupAction
        {
            DESCRIBE_ITEM_DOUBLE,
            MATCH_FOUND,
            MATCH_MULTI_FOUND,
            NO_ACTION,
            NO_MATCH_FOUND,
            SECONDARY
        }

        public ProKnowDetails(DesktopSession desktopSession)
        {
            DesktopSession = desktopSession;
            Error = false;
            ErrorMessage = string.Empty;
        }

        // Standard Method to update the ProKnow Manufacturer information
        private void ManufacturerUpdate(ref ProKnowMatch proKnowMatch, ActiveManufacturer currentManufacturer, string sAnswerText, int iAnswerCode)
        {
            Answer updatedAnswer = proKnowMatch.manufacturerModelInfo[(int)currentManufacturer];
            updatedAnswer.AnswerText = sAnswerText;
            updatedAnswer.AnswerCode = iAnswerCode;

            proKnowMatch.manufacturerModelInfo.RemoveAt((int)currentManufacturer);
            proKnowMatch.manufacturerModelInfo.Insert((int)currentManufacturer, updatedAnswer);
        }

        // Standard Method to update the ProKnow Manufacturer's Model information
        public List<ProKnowMatch> ProKnowLookup(string sManufacturer, string sModel, out ProKnowLookupAction LookupMatch)
        {
            ProKnowMatch proKnowMatch;
            var _lstProKnowMatch = new List<ProKnowMatch>();
            var _ReturnedManModelReplyType = new manModelReplyType();
            string sLevelNest = string.Empty;
            LookupMatch = ProKnowLookupAction.NO_ACTION;
            _ReturnedManModelReplyType = DesktopSession.CallProKnow.GetProKnowDetails(sManufacturer, sModel);

            if (DesktopSession.CallProKnow.Error)
            {
                Error = true;
                ErrorMessage = "Pro-Know is experiencing technical difficulties.  Please manually categorize items";
                return _lstProKnowMatch;
            }
            if (_ReturnedManModelReplyType.serviceData.Items.Length == 1)
            {
                Type serviceDataType = _ReturnedManModelReplyType.serviceData.Items[0].GetType();

                switch (serviceDataType.Name)
                {
                    case "businessExceptionType":
                        var statusBusinessExceptionType = (businessExceptionType)_ReturnedManModelReplyType.serviceData.Items[0];
                        if (statusBusinessExceptionType.responseCode == "NO_MATCH_FOUND")
                        {
                            LookupMatch = ProKnowLookupAction.NO_MATCH_FOUND;
                        }
                        else
                        {
                            Error = true;
                            ErrorMessage = "Unknown Business Exception Type.";
                        }
                        break;
                    case "manModelMatchType":
                        LookupMatch = ProKnowLookupAction.MATCH_FOUND;
                        break;
                }
            }
            else if (_ReturnedManModelReplyType.serviceData.Items.Length > 1)
            {
                if (_ReturnedManModelReplyType.serviceData.Items[0].GetType() == typeof(manModelMatchType))
                {
                    LookupMatch = ProKnowLookupAction.MATCH_MULTI_FOUND;
                }
            }

            switch (LookupMatch)
            {
                case ProKnowLookupAction.MATCH_FOUND:
                    var foundManModelMatchType = (manModelMatchType)_ReturnedManModelReplyType.serviceData.Items[0];
                    proKnowMatch = new ProKnowMatch
                                   {
                                       manufacturerModelInfo = new List<Answer>()
                                   };
                    ParseProKnowDetails(ref proKnowMatch, ref sLevelNest, foundManModelMatchType, ActiveManufacturer.PRIMARY, out LookupMatch);
                    _lstProKnowMatch.Add(proKnowMatch);
                    break;
                case ProKnowLookupAction.MATCH_MULTI_FOUND:
                    foreach (object oManModelMatchType in _ReturnedManModelReplyType.serviceData.Items)
                    {
                        var objectManModelMatchType = (manModelMatchType)oManModelMatchType;
                        proKnowMatch = new ProKnowMatch
                                       {
                                           manufacturerModelInfo = new List<Answer>()
                                       };
                        sLevelNest = string.Empty;
                        ParseProKnowDetails(ref proKnowMatch, ref sLevelNest, objectManModelMatchType, ActiveManufacturer.PRIMARY, out LookupMatch);
                        _lstProKnowMatch.Add(proKnowMatch);
                    }
                    break;
            }

            return _lstProKnowMatch;
        }

        public void GetProKnowDetails(string sManufacturer, string sModel, out manModelReplyType _ReturnedManModelReplyType, out ProKnowLookupAction LookupMatch)
        {
            Error = false;
            ErrorMessage = "";

            _ReturnedManModelReplyType = new manModelReplyType();
            LookupMatch = ProKnowLookupAction.NO_ACTION;

            if (sManufacturer == "" || sModel == "")
            {
                Error = true;
                ErrorMessage = "Need to provide both Manufacturer and Model before continuing.";
                return;
            }
            //Cleanup(ref sManufacturer);
            //Cleanup(ref sModel);
            _ReturnedManModelReplyType = DesktopSession.CallProKnow.GetProKnowDetails(sManufacturer, sModel);

            if (DesktopSession.CallProKnow.Error)
            {
                Error = true;
                ErrorMessage = "Pro-Know is experiencing technical difficulties.  Please manually categorize items";
            }
            else
            {
                if (_ReturnedManModelReplyType.serviceData.Items.Length == 1)
                {
                    Type serviceDataType = _ReturnedManModelReplyType.serviceData.Items[0].GetType();

                    switch (serviceDataType.Name)
                    {
                        case "businessExceptionType":
                            var statusBusinessExceptionType = (businessExceptionType)_ReturnedManModelReplyType.serviceData.Items[0];
                            if (statusBusinessExceptionType.responseCode == "NO_MATCH_FOUND")
                            {
                                LookupMatch = ProKnowLookupAction.NO_MATCH_FOUND;
                            }
                            else
                            {
                                Error = true;
                                ErrorMessage = "Unknown Business Exception Type.";
                            }
                            break;
                        case "manModelMatchType":
                            LookupMatch = ProKnowLookupAction.MATCH_FOUND;                            
                            break;
                    }
                }
                else if (_ReturnedManModelReplyType.serviceData.Items.Length > 1)
                {
                    if (_ReturnedManModelReplyType.serviceData.Items[0].GetType() == typeof(manModelMatchType))
                    {
                        LookupMatch = ProKnowLookupAction.MATCH_MULTI_FOUND;
                    }
                }
            }
        }

        private static void Cleanup(ref string stringToCleanUp)
        {

            string cleanString= stringToCleanUp.Replace(" ", "").Replace("-","");
            stringToCleanUp = cleanString;


        }

        // Extracts the information returned from ProKnow manModelMatchType object
        public void ParseProKnowDetails(ref ProKnowMatch proKnowMatch, ref string levelNest, manModelMatchType selectedManModelMatchType, ActiveManufacturer _ActiveManufacturerModel, out ProKnowLookupAction LookupMatch)
        {
            Error = false;
            ErrorMessage = "";            
            LookupMatch = ProKnowLookupAction.NO_ACTION;

            CategoryNode cnCategoryNodeWalker = DesktopSession.CategoryXML.GetMerchandiseCategory(selectedManModelMatchType.categoryCode.ToString(), ref levelNest);

            if (cnCategoryNodeWalker != null && _ActiveManufacturerModel == ActiveManufacturer.PRIMARY)
            {
                proKnowMatch.primaryCategoryCode = cnCategoryNodeWalker.CategoryCode;
                proKnowMatch.primaryCategoryCodeDescription = cnCategoryNodeWalker.Description;
                proKnowMatch.primaryMaskPointer = cnCategoryNodeWalker.Masks;
            }

            // Update Manufacturer and Model AnswerCodes
            ManufacturerUpdate(ref proKnowMatch, 
                               _ActiveManufacturerModel, 
                               proKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText, 
                               cnCategoryNodeWalker.CategoryCode);

            // Get all the masknn properties from the manModelMatchType returned
            foreach (PropertyInfo pi in selectedManModelMatchType.preAnswered.GetType().GetProperties())
            {
                if (pi.Name.StartsWith("mask", StringComparison.OrdinalIgnoreCase) && 
                    !pi.Name.EndsWith("Specified", StringComparison.OrdinalIgnoreCase))
                {
                    var iPiValue = Convert.ToInt32(pi.GetValue(selectedManModelMatchType.preAnswered, null));

                    if (iPiValue > 0)
                    {
                        var manModelMatchAnswer = new Answer
                                                  {
                                                      AnswerText = pi.Name,
                                                      AnswerCode =
                                                          Convert.ToInt32(pi.GetValue(
                                                              selectedManModelMatchType.preAnswered, null))
                                                  };

                        if (proKnowMatch.preAnsweredQuestions == null)
                            proKnowMatch.preAnsweredQuestions = new List<Answer>();

                        proKnowMatch.preAnsweredQuestions.Add(manModelMatchAnswer);
                    }
                }
            }
            // Get FixedFeatures 
            if (selectedManModelMatchType.fixedFeatures != null)
            {
                proKnowMatch.displayFixedFeaturesOnTag = selectedManModelMatchType.fixedFeatures.printTag;

                // Check to see if fixedFeaturesList has not been instantiated yet.  Instantiate it.
                if (proKnowMatch.fixedFeaturesList == null)
                    proKnowMatch.fixedFeaturesList = new List<FixedFeature>();

                foreach (var foundFixedFeatureType in selectedManModelMatchType.fixedFeatures.feature)
                {
                    var fixFeature = new FixedFeature
                                     {
                                         AnswerCode = foundFixedFeatureType.answerNumber,
                                         AnswerText = foundFixedFeatureType.description
                                     };
                    proKnowMatch.fixedFeaturesList.Add(fixFeature);
                }
            }
            // Get data from <values#> blocks
            foreach (PropertyInfo value in selectedManModelMatchType.GetType().GetProperties())
            {
                if (value != null && 
                    !string.IsNullOrEmpty(value.Name) && 
                    value.Name.StartsWith("values", StringComparison.OrdinalIgnoreCase))
                {
                    var foundValueType = (valuesType)value.GetValue(selectedManModelMatchType, null);

                    if (foundValueType != null)
                    {
                        var foundProKnowData = new ProKnowData
                                                {
                                                    ConditionLevel =
                                                        Convert.ToInt32(value.Name.Replace("values", string.Empty)),
                                                    LoanAmount = Convert.ToDecimal(foundValueType.loanAmount),
                                                    LoanVarHighAmount = Convert.ToDecimal(foundValueType.loanVarHigh),
                                                    LoanVarLowAmount = Convert.ToDecimal(foundValueType.loanVarLow),
                                                    PurchaseAmount = Convert.ToDecimal(foundValueType.purchaseAmount),
                                                    RetailAmount = Convert.ToDecimal(foundValueType.retailAmount),
                                                    RetailVarHighAmount =
                                                        Convert.ToDecimal(foundValueType.purchaseVarHigh),
                                                    RetailVarHighRetailer = string.Empty,
                                                    RetailVarLowAmount =
                                                        Convert.ToDecimal(foundValueType.purchaseVarLow),
                                                    RetailVarLowRetailer = string.Empty
                                                };

                        if (proKnowMatch.proKnowData == null)
                            proKnowMatch.proKnowData = new List<ProKnowData>();

                        proKnowMatch.selectedPKData = foundProKnowData;
                        proKnowMatch.proKnowData.Add(foundProKnowData);
                    }
                }
            }
            // Get <ProCallData> information
            if (selectedManModelMatchType.proCallData != null && _ActiveManufacturerModel == ActiveManufacturer.PRIMARY)
            {
                var proCallData = new ProCallData
                                  {
                                      LastUpdateDate = selectedManModelMatchType.proCallData.lastUpdateDate
                                  };
                if (!string.IsNullOrEmpty(selectedManModelMatchType.proCallData.newRetail))
                {
                    proCallData.NewRetail = Utilities.GetDecimalValue(selectedManModelMatchType.proCallData.newRetail, 0);
                }
                proCallData.YearDiscontinued = selectedManModelMatchType.proCallData.yearDiscontinued;
                proCallData.YearIntroduced = selectedManModelMatchType.proCallData.yearIntroduced;

                // Check to see if proCallData has not been instantiated yet.  Instantiate it.
                proKnowMatch.proCallData = proCallData;
            }

            if (selectedManModelMatchType.combinationCategoryInfo != null && proKnowMatch.transitionStatus != TransitionStatus.MAN_MODEL_PROKNOW_COMBO)
            {
                var describedMerchandise = new DescribedMerchandise(proKnowMatch.primaryMaskPointer);
                if (describedMerchandise.Exists)
                {
                    // If CashlinxDesktopSesson ActivePawnLoan is null, create a PawnLoad
                    // and add pawnItem to it
                    Item item = describedMerchandise.SelectedPawnItem;

                    if (DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.CUSTOMERPURCHASE, StringComparison.OrdinalIgnoreCase) ||
                        DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.DESCRIBEITEMCUSTOMERPURCHASE, StringComparison.OrdinalIgnoreCase) ||
                        DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.CUSTOMERPURCHASEPFI, StringComparison.OrdinalIgnoreCase) ||
                        DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VENDORPURCHASE, StringComparison.OrdinalIgnoreCase))
                    {
                        if (DesktopSession.ActivePurchase == null)
                            DesktopSession.ActivePurchase = new PurchaseVO();

                        item.mItemOrder = DesktopSession.ActivePurchase.Items.Count + 1;
                        DesktopSession.ActivePurchase.Items.Add(item);
                    }
                    else
                    {
                        if (DesktopSession.ActivePawnLoan == null)
                            DesktopSession.ActivePawnLoan = new PawnLoan();

                        item.mItemOrder = DesktopSession.ActivePawnLoan.Items.Count + 1;
                        DesktopSession.ActivePawnLoan.Items.Add(item);
                    }

                    if (_ActiveManufacturerModel == ActiveManufacturer.PRIMARY)
                    {
                        // Create Secondary Manufacturer Answer object and add to Manufacturer Model Info
                        var secondaryManufacturer = new Answer
                                                    {
                                                        AnswerCode =
                                                            selectedManModelMatchType.combinationCategoryInfo.
                                                            secondaryManufacturer
                                                    };

                        ItemAttribute secondManufacturerItemAttribute = item.Attributes.Find(
                            iaManufacturer => iaManufacturer.AttributeCode == secondaryManufacturer.AnswerCode);

                        secondaryManufacturer.AnswerText = secondManufacturerItemAttribute.Description;
                        secondaryManufacturer.InputKey = secondManufacturerItemAttribute.Description;
                        proKnowMatch.manufacturerModelInfo.Add(secondaryManufacturer);

                        // Create Secondary Model Answer object and add to Manufacturer Model Info
                        var secondaryModel = new Answer
                                             {
                                                 AnswerCode =
                                                     selectedManModelMatchType.combinationCategoryInfo.secondaryModel
                                             };

                        ItemAttribute secondModelItemAttribute = item.Attributes.Find(
                            iaModel => iaModel.AttributeCode == secondaryModel.AnswerCode);

                        secondaryModel.AnswerText = secondModelItemAttribute.Description;
                        secondaryModel.InputKey = secondModelItemAttribute.Description;
                        proKnowMatch.manufacturerModelInfo.Add(secondaryModel);

                        proKnowMatch.transitionStatus = TransitionStatus.MAN_MODEL_PROKNOW_COMBO;

                        LookupMatch = ProKnowLookupAction.SECONDARY;
                    }
                }
            }
            else
            {
                proKnowMatch.transitionStatus = proKnowMatch.transitionStatus != TransitionStatus.MAN_MODEL_PROKNOW_COMBO ? TransitionStatus.MAN_MODEL_PROKNOW : TransitionStatus.MAN_MODEL_PROKNOW_COMBO;

                if (proKnowMatch.transitionStatus == TransitionStatus.MAN_MODEL_PROKNOW_COMBO)
                {
                    // Update Manufacturer and Model AnswerCodes
                    DataRow[] myDataRows = DesktopSession.MerchandiseManufacturers.Select("ANS_DESC='" + proKnowMatch.manufacturerModelInfo[2].AnswerText + "'");
                    if (myDataRows.Length > 0)
                    {
                        ManufacturerUpdate(ref proKnowMatch, 
                                           _ActiveManufacturerModel, 
                                           proKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText, 
                                           Convert.ToInt32(myDataRows[0]["ANS_ID"]));
                    }
                }

                LookupMatch = ProKnowLookupAction.DESCRIBE_ITEM_DOUBLE;
            }
        }
    }
}