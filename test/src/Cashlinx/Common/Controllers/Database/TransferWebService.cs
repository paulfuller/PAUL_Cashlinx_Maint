using System;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Common.Libraries.Utility.Logger;
using Common.TransferService;

namespace Common.Controllers.Database
{
    public class TransferWebService : MarshalByRefObject
    {
        private TransferService.mdseTransferPortClient portClient;
        public string ShopNumber = "1";
        public string TerminalID = "1";
        public string UserID = "1";
        public string TransactionID = "1";
        public DateTime CurrentDateTime = ShopDateTime.Instance.ShopDate;

        public bool Error { get; private set; }

        public string ErrorMessage { get; private set; }

        public TransferWebService()
        {
            var conf = SecurityAccessor.Instance.EncryptConfig;
            var transferEsb = conf.GetMDSETransferService();
            portClient = new TransferService.mdseTransferPortClient(

                conf.DecryptValue(transferEsb.EndPointName),
                conf.DecryptValue(transferEsb.Uri));
        }

        public bool CompleteCatcoTypeTransfersWS(string storeNumber, int transferNumber, int destinationStoreNumber, string tranType, out string errormessage)
        {
            bool transferWebService = false;
            if (storeNumber != string.Empty && transferNumber != 0 && destinationStoreNumber != 0)
            {
                //TransferService.storeToStoreRequestType requestdata = new TransferService.storeToStoreRequestType();
                TransferService.JSUPRequestType requestdata = new JSUPRequestType();

                requestdata.serviceInformation = new serviceInformationType();
                requestdata.serviceInformation.source = new endpointType();
                //to do: what should the source values be?                
                requestdata.serviceInformation.source.systemId = "P2";
                requestdata.serviceInformation.source.systemName = "Cashlinx Phase 2";
                requestdata.serviceInformation.source.systemVersion = "4.0";
                var conf = SecurityAccessor.Instance.EncryptConfig;
                var transferEsb = conf.GetMDSETransferService();
                requestdata.serviceInformation.domain = conf.DecryptValue(transferEsb.Domain);

                requestdata.serviceInformation.shopNumber = ShopNumber;
                requestdata.serviceInformation.terminalID = TerminalID;
                requestdata.serviceInformation.userID = UserID;
                requestdata.serviceInformation.transactionID = TransactionID;
                requestdata.serviceInformation.timeStamp = CurrentDateTime;
                requestdata.serviceData = new TransferService.JSUPRequestTypeServiceData();
                requestdata.serviceData.JSUPtransfer = new JSUPtransferType();
                requestdata.serviceData.JSUPtransfer.storeNumber = storeNumber;
                requestdata.serviceData.JSUPtransfer.transferNumber = transferNumber.ToString();
                requestdata.serviceData.JSUPtransfer.destination = destinationStoreNumber.ToString();
                requestdata.serviceData.JSUPtransfer.transferType = tranType;
                JSUPReplyType replydata = null;

                try
                {
                    replydata = portClient.JSUPTransferOut(requestdata);
                }
                catch (Exception ee)
                {
                    errormessage = "Transfer web service call failed for" + transferNumber;
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer web service call failed for" + transferNumber + " " + ee);
                    return false;
                }

                transferWebService = HandleJSUPWSResponse(replydata, transferNumber);
            }
            else
            {
                ErrorMessage = "Invalid storeNumber or transferNumber or destinationStoreNumber";
            }
            errormessage = ErrorMessage;
            portClient.Close();
            return transferWebService;
        }

        private bool HandleJSUPWSResponse(JSUPReplyType replydata, int transferNumber)
        {
            bool transferWebService = false;
            if (replydata.serviceInformation != null)
            {
                //check whether there is a general exception
                if (replydata.serviceInformation.status != null &&
                    replydata.serviceInformation.status.exceptionCode != null)
                {
                    ErrorMessage = replydata.serviceInformation.status.exceptionCode.ToString() +
                                   replydata.serviceInformation.status.sourceMessage;
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer web service came back with errors for" + transferNumber
                                                                         + replydata.serviceInformation.status.exceptionCode.ToString() +
                                                                         replydata.serviceInformation.status.sourceMessage);
                    //return false;
                }

                if (replydata.serviceData != null && replydata.serviceData.Items != null)
                {
                    if (replydata.serviceData.Items[0].GetType() != typeof(businessExceptionType))
                    {
                        //check the reply
                        transferWebService = true;
                    }
                    else
                    {
                        transferWebService = false;
                        businessExceptionType returnData = (businessExceptionType)replydata.serviceData.Items[0];
                        ErrorMessage = returnData.responseCode.ToString() +
                                       returnData.message;

                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer web service returned with business exception " + transferNumber + " " +
                                                                             returnData.responseCode.ToString() +
                                                                             returnData.message);
                    }
                }
                else
                //No data came back from the web service call...log it
                {
                    transferWebService = true;
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer JSUP web service No data returned for" + transferNumber);
                }
            }
            return transferWebService;
        }

        // Void Merchandise Transfer out
        public bool MDSETransferVoidWS(string storeNo, string transNumber, out string errorMessage)
        {
            bool transferWebService = false;

            TransferService.VMRequestType req = new TransferService.VMRequestType();
            req.serviceInformation = new serviceInformationType();

            req.serviceInformation.source = new endpointType();
            req.serviceInformation.source.systemId = "P2";
            req.serviceInformation.source.systemName = "Cashlinx Phase 2";
            req.serviceInformation.source.systemVersion = "4.0";
            var conf = SecurityAccessor.Instance.EncryptConfig;
            var transferEsb = conf.GetMDSETransferService();
            req.serviceInformation.domain = conf.DecryptValue(transferEsb.Domain);

            req.serviceInformation.shopNumber = ShopNumber;
            req.serviceInformation.terminalID = TerminalID;
            req.serviceInformation.userID = UserID;
            req.serviceInformation.transactionID = TransactionID;
            req.serviceInformation.timeStamp = CurrentDateTime;

            req.serviceData = new TransferService.VMRequestTypeServiceData();
            req.serviceData.VoidTransfer = new VoidTransferType();
            req.serviceData.VoidTransfer.storeNumber = storeNo;
            req.serviceData.VoidTransfer.transferNumber = transNumber;

            TransferService.VMReplyType response = portClient.CreateVoidedMissile(req);

            transferWebService = HandleWSResponseForVoid(response, out errorMessage, transNumber);

            portClient.Close();

            return transferWebService;
        }

        private bool HandleWSResponseForVoid(TransferService.VMReplyType replydata, out string errorMessage, string transNumber)
        {
            bool transferWebService = false;
            if (replydata.serviceInformation != null)
            {
                //check whether there is a general exception
                if (replydata.serviceInformation.status != null &&
                    replydata.serviceInformation.status.exceptionCode != null)
                {
                    ErrorMessage = replydata.serviceInformation.status.exceptionCode.ToString() +
                                   replydata.serviceInformation.status.sourceMessage;
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Void Transfer web service came back with errors for" + transNumber
                                                                         + replydata.serviceInformation.status.exceptionCode.ToString() +
                                                                         replydata.serviceInformation.status.sourceMessage);
                }

                if (replydata.serviceData != null && replydata.serviceData.Items != null)
                {
                    if (replydata.serviceData.Items[0].GetType() != typeof(businessExceptionType))
                    {
                        //check the reply
                        transferWebService = true;
                    }
                    else
                    {
                        businessExceptionType returnData = (businessExceptionType)replydata.serviceData.Items[0];
                        ErrorMessage = returnData.responseCode.ToString() +
                                       returnData.message;
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Void Transfer web service returned with business exception for" + transNumber +
                                                                             returnData.responseCode.ToString() +
                                                                             returnData.message);
                        transferWebService = false;
                    }
                }
                else
                //No data came back from the web service call...log it
                {
                    transferWebService = true;
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Void Transfer web service did not return any data ");
                }
            }
            errorMessage = ErrorMessage;
            return transferWebService;
        }

        public bool CompleteShopAndGunTransferWS(string storeNumber, int transferNumber, int destinationStoreNumber)
        {
            bool transferWebService = false;
            if (storeNumber != string.Empty && transferNumber != 0 && destinationStoreNumber != 0)
            {
                TransferService.storeToStoreRequestType requestdata = new TransferService.storeToStoreRequestType();
                requestdata.serviceInformation = new serviceInformationType();
                requestdata.serviceInformation.source = new endpointType();
                //to do: what should the source values be?
                requestdata.serviceInformation.source.systemId = "P2";
                requestdata.serviceInformation.source.systemName = "Cashlinx Phase 2";
                requestdata.serviceInformation.source.systemVersion = "4.0";
                var conf = SecurityAccessor.Instance.EncryptConfig;
                var transferEsb = conf.GetMDSETransferService();
                requestdata.serviceInformation.domain = conf.DecryptValue(transferEsb.Domain);

                requestdata.serviceInformation.shopNumber = ShopNumber;
                requestdata.serviceInformation.terminalID = TerminalID;
                requestdata.serviceInformation.userID = UserID;
                requestdata.serviceInformation.transactionID = TransactionID;
                requestdata.serviceInformation.timeStamp = CurrentDateTime;
                requestdata.serviceData = new TransferService.storeToStoreRequestTypeServiceData();
                requestdata.serviceData.transfer = new transferType();
                requestdata.serviceData.transfer.storeNumber = storeNumber;
                requestdata.serviceData.transfer.transferNumber = transferNumber.ToString();
                requestdata.serviceData.transfer.destination = destinationStoreNumber.ToString();
                TransferService.storeToStoreReplyType replydata = null;
                try
                {
                    replydata = portClient.StoreToStoreTransferOut(requestdata);
                }
                catch (Exception ee)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer web service call failed for" + transferNumber + " " + ee);
                    return false;
                }
                transferWebService = HandleWSResponse(replydata, transferNumber);
            }
            else
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Invalid storeNumber or transferNumber or destinationStoreNumber for" + transferNumber);
            }

            portClient.Close();
            return transferWebService;
        }

        private bool HandleWSResponse(TransferService.storeToStoreReplyType replydata, int transferNumber)
        {
            bool transferWebService = false;
            if (replydata.serviceInformation != null)
            {
                //check whether there is a general exception
                if (replydata.serviceInformation.status != null &&
                    replydata.serviceInformation.status.exceptionCode != null)
                {
                    ErrorMessage = replydata.serviceInformation.status.exceptionCode.ToString() +
                                   replydata.serviceInformation.status.sourceMessage;
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer web service came back with errors for " + transferNumber + " "
                                                                         + replydata.serviceInformation.status.exceptionCode.ToString() +
                                                                         replydata.serviceInformation.status.sourceMessage);
                    //return false;
                }

                if (replydata.serviceData != null && replydata.serviceData.Items != null)
                {
                    if (replydata.serviceData.Items[0].GetType() != typeof(businessExceptionType))
                    {
                        //check the reply
                        transferWebService = true;
                        // FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer web service call was successful for" + transferNumber + " " + returnValue);
                    }
                    else
                    {
                        businessExceptionType returnData = (businessExceptionType)replydata.serviceData.Items[0];
                        ErrorMessage = returnData.responseCode.ToString() +
                                       returnData.message;
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer web service returned with business exception for" + transferNumber + " " +
                                                                             returnData.responseCode.ToString() +
                                                                             returnData.message);
                        transferWebService = false;
                    }
                }
                else
                //No data came back from the web service call...log it
                {
                    transferWebService = true;
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer web service did not return any data ");
                }
            }
            return transferWebService;
        }

        // Void Merchandise Transfer out
        public bool MDSETransferINRejectWS(string storeNo, string transNumber, string reason, out string errorMessage)
        {
            bool transferWebService = false;

            TransferService.RJRequestType req = new TransferService.RJRequestType();
            req.serviceInformation = new serviceInformationType();

            req.serviceInformation.source = new endpointType();
            req.serviceInformation.source.systemId = "P2";
            req.serviceInformation.source.systemName = "Cashlinx Phase 2";
            req.serviceInformation.source.systemVersion = "4.0";
            var conf = SecurityAccessor.Instance.EncryptConfig;
            var transferEsb = conf.GetMDSETransferService();
            req.serviceInformation.domain = conf.DecryptValue(transferEsb.Domain);

            req.serviceInformation.shopNumber = ShopNumber;
            req.serviceInformation.terminalID = TerminalID;
            req.serviceInformation.userID = UserID;
            req.serviceInformation.transactionID = TransactionID;
            req.serviceInformation.timeStamp = CurrentDateTime;

            req.serviceData = new TransferService.RJRequestTypeServiceData();
            req.serviceData.RejectTransfer = new RejectTransferType();
            req.serviceData.RejectTransfer.storeNumber = storeNo;
            req.serviceData.RejectTransfer.transferNumber = transNumber;
            req.serviceData.RejectTransfer.rejectReason = reason;

            TransferService.RJReplyType response = portClient.CreateRejectMissile(req);

            transferWebService = HandleTransInRejectWSRespose(response, out errorMessage, transNumber);

            portClient.Close();

            return transferWebService;
        }

        private bool HandleTransInRejectWSRespose(TransferService.RJReplyType replydata, out string errorMessage, string transNumber)
        {
            bool transferWebService = false;
            if (replydata.serviceInformation != null)
            {
                //check whether there is a general exception
                if (replydata.serviceInformation.status != null &&
                    replydata.serviceInformation.status.exceptionCode != null)
                {
                    ErrorMessage = replydata.serviceInformation.status.exceptionCode.ToString() +
                                   replydata.serviceInformation.status.sourceMessage;
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer IN Reject web service came back with errors for" + transNumber
                                                                         + replydata.serviceInformation.status.exceptionCode.ToString() +
                                                                         replydata.serviceInformation.status.sourceMessage);
                }

                if (replydata.serviceData != null && replydata.serviceData.Items != null)
                {
                    if (replydata.serviceData.Items[0].GetType() != typeof(businessExceptionType))
                    {
                        //check the reply
                        transferWebService = true;
                    }
                    else
                    {
                        businessExceptionType returnData = (businessExceptionType)replydata.serviceData.Items[0];
                        ErrorMessage = returnData.responseCode.ToString() +
                                       returnData.message;
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer IN Reject web service came back with errors for" + transNumber + " " +
                                                                             returnData.responseCode.ToString() +
                                                                             returnData.message);
                        transferWebService = false;
                    }
                }
                else
                //No data came back from the web service call...log it
                {
                    transferWebService = true;
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer IN Reject web service did not return any data for" + transNumber);
                }
            }
            errorMessage = ErrorMessage;
            return transferWebService;
        }

        public class TransferType
        {
            public TransferType()
            {
                StoreNumber = "";
                TransferNumber = 0;
                Destination = 0;
            }

            public string StoreNumber { get; set; }

            public int TransferNumber { get; set; }

            public int Destination { get; set; }
        }
    }
}
