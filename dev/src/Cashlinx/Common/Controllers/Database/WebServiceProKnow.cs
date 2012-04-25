/************************************************************************
* Namespace:       CommonUI.DesktopProcedures
* Class:           WebServiceProKnow
* 
* Description      The class manages the connection and retrieval of
*                  ProKnow Web Service.
* 
* History
* David D Wise, Initial Development
* 
************************************************************************/

using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Common.Libraries.Utility.Logger;
using Common.ProKnowService;

namespace Common.Controllers.Database
{
    public class WebServiceProKnow : MarshalByRefObject
    {
        public DesktopSession DesktopSession { get; private set; }

        public proknowPortClient Port { get; set; }

        public bool Error { get; private set; }

        public string ErrorMessage { get; private set; }

        public WebServiceProKnow(DesktopSession desktopSession)
        {
            DesktopSession = desktopSession;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public alternameManufacturerReplyType GetAlternateManufacturer(string sManufacturer)
        {
            Error = false;
            ErrorMessage = "";

            alternameManufacturerReplyType replyReplyType = new alternameManufacturerReplyType();
            alternateManufacturerRequestType request = new alternateManufacturerRequestType();
            Guid gdTransactionID = Guid.NewGuid();

            try
            {
                // all of this information needs to be system variables
                var conf = SecurityAccessor.Instance.EncryptConfig;
                var proKnowService = conf.GetProKnowESBService();
                request.serviceInformation = new serviceInformationType();
                request.serviceInformation.domain = conf.DecryptValue(proKnowService.Domain);
                request.serviceInformation.shopNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                request.serviceInformation.terminalID = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId;
                request.serviceInformation.userID = DesktopSession.UserName;
                request.serviceInformation.transactionID = gdTransactionID.ToString();
                request.serviceInformation.timeStamp = ShopDateTime.Instance.ShopDate;
                request.serviceData = new alternateManufacturerRequestTypeServiceData();
                request.serviceData.manufacturer = sManufacturer.ToUpper();

                Port = new proknowPortClient(conf.DecryptValue(proKnowService.EndPointName), conf.DecryptValue(proKnowService.Uri));
                Port.Open();
                replyReplyType = Port.GetAlternateManufacturers(request);
                Port.Close();

                if (replyReplyType.serviceInformation.status != null)
                {
                    Error = true;
                    ErrorMessage = replyReplyType.serviceInformation.status.sourceMessage;
                    FileLogger.Instance.logMessage(LogLevel.WARN, this, "GetAlternateManufacturer errored:  " + ErrorMessage + " [" + gdTransactionID.ToString() + "]");
                }
                else
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, this, "GetAlternateManufacturer called. [" + gdTransactionID.ToString() + "]");
                }
            }
            catch (Exception exp)
            {
                Error = true;
                ErrorMessage = exp.Message;
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "GetAlternateManufacturer errored:  " + ErrorMessage + " [" + gdTransactionID.ToString() + "]");
            }

            return replyReplyType;
        }

        public getModelsReplyType GetModels(string sManufacturer)
        {
            Error = false;
            ErrorMessage = "";

            getModelsReplyType modelsReplyType = new getModelsReplyType();
            getModelsRequestType modelRequestType = new getModelsRequestType();
            Guid gdTransactionID = Guid.NewGuid();

            try
            {
                // all of this information needs to be system variables
                var conf = SecurityAccessor.Instance.EncryptConfig;
                var proKnowService = conf.GetProKnowESBService();
                modelRequestType.serviceInformation = new serviceInformationType();
                modelRequestType.serviceInformation.domain = conf.DecryptValue(proKnowService.Domain);
                modelRequestType.serviceInformation.shopNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                modelRequestType.serviceInformation.terminalID = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId;
                modelRequestType.serviceInformation.userID = DesktopSession.UserName;
                modelRequestType.serviceInformation.transactionID = gdTransactionID.ToString();
                modelRequestType.serviceInformation.timeStamp = ShopDateTime.Instance.ShopDate;
                modelRequestType.serviceData = new getModelsRequestTypeServiceData();
                modelRequestType.serviceData.manufacturer = sManufacturer.ToUpper();

                Port = new proknowPortClient(conf.DecryptValue(proKnowService.EndPointName), conf.DecryptValue(proKnowService.Uri));
                Port.Open();
                modelsReplyType = Port.GetModels(modelRequestType);
                Port.Close();

                if (modelRequestType.serviceInformation.status != null)
                {
                    Error = true;
                    ErrorMessage = modelRequestType.serviceInformation.status.sourceMessage;
                    FileLogger.Instance.logMessage(LogLevel.WARN, this, "GetModels errored:  " + ErrorMessage + " [" + gdTransactionID.ToString() + "]");
                }
                else
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, this, "GetModels called. [" + gdTransactionID.ToString() + "]");
                }
            }
            catch (Exception exp)
            {
                Error = true;
                ErrorMessage = exp.Message;
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "GetModels errored:  " + ErrorMessage + " [" + gdTransactionID.ToString() + "]");
            }

            return modelsReplyType;
        }

        public manModelReplyType GetProKnowDetails(string sManufacturer, string sModel)
        {
            Error = false;
            ErrorMessage = "";

            manModelReplyType modelsReplyType = new manModelReplyType();
            manModelRequestType manModelRequestType = new manModelRequestType();
            Guid gdTransactionID = Guid.NewGuid();

            try
            {
                // all of this information needs to be system variables
                var conf = SecurityAccessor.Instance.EncryptConfig;
                var proKnowService = conf.GetProKnowESBService();
                manModelRequestType.serviceInformation = new serviceInformationType();
                manModelRequestType.serviceInformation.domain = conf.DecryptValue(proKnowService.Domain);
                manModelRequestType.serviceInformation.shopNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                manModelRequestType.serviceInformation.terminalID = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId;
                manModelRequestType.serviceInformation.userID = DesktopSession.UserName;
                manModelRequestType.serviceInformation.transactionID = gdTransactionID.ToString();
                manModelRequestType.serviceInformation.timeStamp = ShopDateTime.Instance.ShopDate;
                manModelRequestType.serviceData = new manModelRequestTypeServiceData();
                manModelRequestType.serviceData.manufacturer = sManufacturer.ToUpper();
                manModelRequestType.serviceData.model = sModel;

                string ePName = conf.DecryptValue(proKnowService.EndPointName);
                string ePUri = conf.DecryptValue(proKnowService.Uri);
                Port = new proknowPortClient(ePName, ePUri);
                Port.Open();
                modelsReplyType = Port.GetProknowDetails(manModelRequestType);
                Port.Close();

                if (manModelRequestType.serviceInformation.status != null)
                {
                    Error = true;
                    ErrorMessage = manModelRequestType.serviceInformation.status.sourceMessage;
                    FileLogger.Instance.logMessage(LogLevel.WARN, this, "GetProKnowDetails errored:  " + ErrorMessage + " [" + gdTransactionID.ToString() + "]");
                }
                else
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, this, "GetProKnowDetails called. [" + gdTransactionID.ToString() + "]");
                }
            }
            catch (Exception exp)
            {
                Error = true;
                ErrorMessage = exp.Message;
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "GetProKnowDetails errored:  " + ErrorMessage + " [" + gdTransactionID.ToString() + "]");
            }

            return modelsReplyType;
        }

        public groupOfValuesType GetGroupOfValues(List<groupOfValuesInputType> lstGroupOfInputValues)
        {
            Error = false;
            ErrorMessage = "";

            groupOfValuesType groupofValues = new groupOfValuesType();
            Guid gdTransactionID = Guid.NewGuid();

            try
            {
                var conf = SecurityAccessor.Instance.EncryptConfig;
                var proKnowService = conf.GetProKnowESBService();
                groupOfValuesRequestType groupOfValuesRequest = new groupOfValuesRequestType();
                groupOfValuesRequest.serviceInformation = new serviceInformationType()
                {
                    domain = conf.DecryptValue(proKnowService.Domain),
                    shopNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    terminalID = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                    timeStamp = ShopDateTime.Instance.ShopDate,
                    transactionID = gdTransactionID.ToString(),
                    userID = DesktopSession.UserName
                };

                groupOfValuesInputType[] groupOfInputTypeValues = lstGroupOfInputValues.ToArray();
                groupOfValuesRequest.serviceData = new groupOfValuesRequestTypeServiceData()
                {
                    groupOfValuesInput = groupOfInputTypeValues
                };

                Port = new proknowPortClient(conf.DecryptValue(proKnowService.EndPointName), conf.DecryptValue(proKnowService.Uri));
                Port.Open();
                groupofValues = Port.GroupOfValues(groupOfValuesRequest);                
                Port.Close();
            }
            catch (Exception exp)
            {
                Error = true;
                ErrorMessage = exp.Message;
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "GetProKnowDetails errored:  " + ErrorMessage + " [" + gdTransactionID.ToString() + "]");
            }
            return groupofValues;
        }
    }
}