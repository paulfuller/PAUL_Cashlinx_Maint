/**************************************************************************************************************
* CashlinxDesktop.DesktopProcedures
* AddressWebService
* Class to call the USPS address web service
* Sreelatha Rengarajan 5/12/2009 Initial version
**************************************************************************************************************/

using System;
using Common.AddressService;
using Common.Controllers.Security;
using Common.Libraries.Utility.Logger;

namespace Common.Controllers.Database
{
    public class AddressWebService
    {
        private addressServicePortClient client;
        private cityStateLookupReplyType reply;
        private cityStateLookupRequestType request;
        private addressValidationRequestType addrRequest;
        private addressValidationReplyType addrReply;

        public addressValidationReplyType AddrReply
        {
            get { return addrReply; }
        }

        public addressServicePortClient Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
            }
        }

        public cityStateLookupReplyType Reply
        {
            get
            {
                return reply;
            }
            set
            {
                reply = value;
            }
        }

        public cityStateLookupRequestType Request
        {
            get
            {
                return request;
            }
            set
            {
                request = value;
            }
        }

        public string ShopNumber;
        public string TerminalID;
        public string UserID;
        public string TransactionID;
        public DateTime CurrentDateTime;


        public AddressWebService()
        {
            var conf = SecurityAccessor.Instance.EncryptConfig;
            var addressEsb = conf.GetAddressESBService();
            client = new AddressService.addressServicePortClient(conf.DecryptValue(addressEsb.EndPointName), conf.DecryptValue(addressEsb.Uri));
            reply = null;
            request = null;
            addrReply = null;
            addrRequest = null;
        }



        public AddressData LookupCityState(string zipcode, string zipplus)
        {
            var custAddrData = new AddressData();
            if (!string.IsNullOrEmpty(zipcode))
            {
                var conf = SecurityAccessor.Instance.EncryptConfig;
                var addressEsb = conf.GetAddressESBService();
                request = new AddressService.cityStateLookupRequestType
                          {
                              serviceInformation = new AddressService.serviceInformationType
                                                   {
                                                       domain = conf.DecryptValue(addressEsb.Domain),
                                                       shopNumber = ShopNumber,
                                                       terminalID = TerminalID,
                                                       userID = UserID,
                                                       transactionID = TransactionID,
                                                       timeStamp = CurrentDateTime
                                                   },
                              serviceData =
                                  new AddressService.cityStateLookupRequestTypeServiceData
                                  {
                                      postalCode = new AddressService.postalCodeType {zipCode = zipcode}
                                  }
                          };
                if (zipplus != null && zipplus.Trim().Length != 0)
                {
                    request.serviceData.postalCode.zipPlus = zipplus;
                }

                reply = client.lookupCityState(request);
                if (reply.serviceInformation != null)
                {
                    if (reply.serviceData != null && reply.serviceData.Item != null)
                    {

                        if (reply.serviceData.Item.GetType() != typeof(businessExceptionType))
                        {
                            var returnedAddress = (addressType)reply.serviceData.Item;

                            custAddrData.City = returnedAddress.city;
                            custAddrData.State = returnedAddress.state;
                        }
                        else
                        {
                            if (reply.serviceInformation.status.exceptionCode != null)
                            {
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, string.Format("Address web service came back with errors {0}", reply.serviceInformation.status.exceptionCode));
                            }
                            else
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Address web service came back with errors ");
                        }
                    }
                    else
                    //No data came back from the web service call...log it
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Address web service came back with errors ");
                    }
                }




            }

            request = null;
            reply = null;
            client.Close();
            return custAddrData;
        }



        public bool validateAddress(string strAddress1, string strAddress2, string strCity, string strState)
        {
            bool addrCheck = false;
            try
            {
                var conf = SecurityAccessor.Instance.EncryptConfig;
                var addressEsb = conf.GetAddressESBService();
                addrRequest = new AddressService.addressValidationRequestType();
                addrRequest.serviceInformation = new AddressService.serviceInformationType();
                addrRequest.serviceInformation.domain = conf.DecryptValue(addressEsb.Domain);
                request.serviceInformation.shopNumber = ShopNumber;
                request.serviceInformation.terminalID = TerminalID;
                request.serviceInformation.userID = UserID;
                request.serviceInformation.transactionID = TransactionID;
                request.serviceInformation.timeStamp = CurrentDateTime;
                addrRequest.serviceData = new AddressService.addressValidationRequestTypeServiceData();
                string[] strStreet;
                if (strAddress2.Trim().Length > 0)
                {
                    strStreet = new string[2];
                    strStreet[0] = strAddress1;
                    strStreet[1] = strAddress2;
                }
                else
                {
                    strStreet = new string[1];
                    strStreet[0] = strAddress1;
                }

                addrRequest.serviceData.address = new AddressService.addressType
                                                  {
                                                      street = strStreet,
                                                      city = strCity,
                                                      state = strState,
                                                      type = AddressService.addressTypeType.PHYSICAL
                                                  };

                client.Open();
                addrReply = client.validateAddress(addrRequest);
                client.Close();
                var returnedAddress = (AddressService.addressType)reply.serviceData.Item;
                if (returnedAddress.postalCode.zipCode != null)
                    addrCheck = true;
                else
                    addrCheck = false;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                client = null;
                request = null;
                reply = null;

            }
            return addrCheck;

        }




    }


    public class AddressData
    {

        private string city;
        private string state;

        public AddressData()
        {
            city = string.Empty;
            state = string.Empty;
        }

        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

    }



}
