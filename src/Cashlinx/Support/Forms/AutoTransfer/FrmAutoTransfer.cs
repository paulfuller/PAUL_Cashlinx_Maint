using System;
using System.Windows.Forms;
using System.Collections;
using Common.Controllers.Security;
using Common.Libraries.Utility.Shared;
using Common.TransferService;

namespace Support.Forms.AutoTransfer
{
    public partial class FrmAutoTransfer : Form
    {
        //string SearchType;
        private mdseTransferPortClient portClient;
        
        //webservice constants
        public const String _TRAN_TYPE_EXCESS = "JOEXC";
        public const String _TRAN_TYPE_SCRAP = "JOSCR";
        public const String _TRAN_TYPE_REFURB = "JORFB";
        public string ErrorMessage;
        private string TransType;
        string FromShop;
        string ToShop;
        string TransferNumber;
        bool waitvar = false;

        public string ShopNumber = "1";
        public string TerminalID = "1";
        public string UserID = "1";
        public string TransactionID = "1";
        //private TransferService.serviceDataRequestType request;
        public DateTime CurrentDateTime = DateTime.Now;



        public FrmAutoTransfer()
        {
            InitializeComponent();
            LoadTransferType();
            rbRecreate.Checked = true;
             var conf = SecurityAccessor.Instance.EncryptConfig;
             var transferEsb = conf.GetMDSETransferService();
             //MessageBox.Show("End point" +conf.DecryptValue(transferEsb.EndPointName));
             //MessageBox.Show("transferEsb" + conf.DecryptValue(transferEsb.Uri));
             portClient = new mdseTransferPortClient(conf.DecryptValue(transferEsb.EndPointName),
                conf.DecryptValue(transferEsb.Uri));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            portClient.Close();
            this.Close();
        }

        private void LoadTransferType()
        {
            var recordTypes = new ArrayList();
            //recordTypes.Add(new ComboBoxData("ap", "Appraisal"));
            recordTypes.Add(new ComboBoxData(_TRAN_TYPE_REFURB, "Refurb"));
            recordTypes.Add(new ComboBoxData(_TRAN_TYPE_SCRAP, "Scrap"));
            //recordTypes.Add(new ComboBoxData("wh", "Wholesale"));
            recordTypes.Add(new ComboBoxData(_TRAN_TYPE_EXCESS, "Excess"));
            recordTypes.Add(new ComboBoxData("STS", "Shop to Shop"));
            recordTypes.Add(new ComboBoxData("STS", "CAF"));
            this.cbTransferType.DataSource = recordTypes;
            this.cbTransferType.DisplayMember = "Description";
            this.cbTransferType.ValueMember = "Code";
            TransType = _TRAN_TYPE_REFURB;
        }

        private void cbTransferType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            TransType = cbTransferType.SelectedValue.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (waitvar)
                return;
            waitvar = true;
            bool ReturnValue = false;
            string msg = string.Empty;
            Int32 ToStoreInteger;
            Int32 TransferNumInt;
            bool result = Int32.TryParse(ToShop, out ToStoreInteger);
            Cursor.Current = Cursors.WaitCursor;

            if (result != true)
            {
                MessageBox.Show("To Shop must be a valid number.", "Error");
                return;
            }

            result = Int32.TryParse(TransferNumber, out TransferNumInt);

            if (result != true)
            {
                MessageBox.Show("Transfer Number must be a valid number.", "Error");
                return;
            }



            if (TransType == "STS")
            {
                ReturnValue = CompleteSTSTransfer(FromShop, TransferNumInt, ToStoreInteger, out msg);
            }
            else
            {
                ReturnValue = CompleteAllTypeTransfers(FromShop, TransferNumInt, ToStoreInteger,TransType, out msg);
            }
            //ReturnValue = CompleteAllTypeTransfers("02011", 100261, 64, _TRAN_TYPE_SCRAP, out msg);
//            ReturnValue = CompleteSTSTransfer("02030", 100261, 2030, out msg);
            waitvar = false;
            Cursor.Current = Cursors.Default;

            if (ReturnValue != true)
            {
                MessageBox.Show(msg, "Error Creating File");
                return;
            }

            MessageBox.Show(msg);
            return;

        }

        public bool CompleteSTSTransfer(string storeNumber, int transferNumber, int destinationStoreNumber, out string replymessage)
        {
            bool transferWebService = false;

            var requestdata = new storeToStoreRequestType();
            
            requestdata.serviceInformation = new serviceInformationType();
            requestdata.serviceInformation.source = new endpointType();
            requestdata.serviceInformation.source.systemId = "CS";
            requestdata.serviceInformation.source.systemName = "Cashlinx Support";
            requestdata.serviceInformation.source.systemVersion = "4.0";

            var conf = SecurityAccessor.Instance.EncryptConfig;
            var transferEsb = conf.GetMDSETransferService();
            requestdata.serviceInformation.domain = conf.DecryptValue(transferEsb.Domain);

            requestdata.serviceInformation.shopNumber = ShopNumber;
            requestdata.serviceInformation.terminalID = TerminalID;
            requestdata.serviceInformation.userID = UserID;
            requestdata.serviceInformation.transactionID = TransactionID;
            requestdata.serviceInformation.timeStamp = CurrentDateTime;

            //requestdata.serviceData = new TransferService.JSUPRequestTypeServiceData();
            //requestdata.serviceData.JSUPtransfer = new JSUPtransferType();
            requestdata.serviceData = new storeToStoreRequestTypeServiceData();
            requestdata.serviceData.transfer = new transferType();

            
            requestdata.serviceData.transfer.storeNumber = storeNumber;
            requestdata.serviceData.transfer.transferNumber = transferNumber.ToString();
            requestdata.serviceData.transfer.destination = destinationStoreNumber.ToString();
            //requestdata.serviceData.transfer.transferType = tranType;
            storeToStoreReplyType replydata = portClient.StoreToStoreTransferOut(requestdata);

            transferWebService = HandleSTSResponse(replydata);

            
            
            replymessage = ErrorMessage;
            //request = null;
            //portClient.Close();
            return transferWebService;

        }


        public bool CompleteAllTypeTransfers(string storeNumber, int transferNumber, int destinationStoreNumber, string tranType, out string errormessage)
        {
            bool transferWebService = false;
            if (storeNumber != string.Empty && transferNumber != 0 && destinationStoreNumber != 0)
            {
                //TransferService.storeToStoreRequestType requestdata = new TransferService.storeToStoreRequestType();
                var requestdata = new JSUPRequestType();
                

                requestdata.serviceInformation = new serviceInformationType();
                requestdata.serviceInformation.source = new endpointType();
                //to do: what should the source values be?                
                requestdata.serviceInformation.source.systemId = "CS";
                requestdata.serviceInformation.source.systemName = "Cashlinx Support";
                requestdata.serviceInformation.source.systemVersion = "4.0";
                var conf = SecurityAccessor.Instance.EncryptConfig;
                var transferEsb = conf.GetMDSETransferService();
                requestdata.serviceInformation.domain = conf.DecryptValue(transferEsb.Domain);

                requestdata.serviceInformation.shopNumber = ShopNumber;
                requestdata.serviceInformation.terminalID = TerminalID;
                requestdata.serviceInformation.userID = UserID;
                requestdata.serviceInformation.transactionID = TransactionID;
                requestdata.serviceInformation.timeStamp = CurrentDateTime;
                requestdata.serviceData = new JSUPRequestTypeServiceData();
                requestdata.serviceData.JSUPtransfer = new JSUPtransferType();

                requestdata.serviceData.JSUPtransfer.storeNumber = storeNumber;
                requestdata.serviceData.JSUPtransfer.transferNumber = transferNumber.ToString();
                requestdata.serviceData.JSUPtransfer.destination = destinationStoreNumber.ToString();
                requestdata.serviceData.JSUPtransfer.transferType = tranType;
                JSUPReplyType replydata = portClient.JSUPTransferOut(requestdata);

                transferWebService = HandleJSUPWSResponse(replydata);

            }
            errormessage = ErrorMessage;
            //request = null;
            //portClient.Close();
            return transferWebService;
        }

        private bool HandleJSUPWSResponse(JSUPReplyType replydata)
        {
            bool transferWebService = false;
            if (replydata.serviceInformation != null)
            {
                //check whether there is a general exception
                if (replydata.serviceInformation.status != null &&
                    replydata.serviceInformation.status.exceptionCode != null)
                {
                    ErrorMessage = replydata.serviceInformation.status.exceptionCode.ToString() + " " +
                        replydata.serviceInformation.status.sourceMessage;
                   // FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer web service came back with errors "
                   //     + replydata.serviceInformation.status.exceptionCode.ToString() +
                   //     replydata.serviceInformation.status.sourceMessage);
                    //return false;
                }

                if (replydata.serviceData != null && replydata.serviceData.Items != null)
                {

                    if (replydata.serviceData.Items[0].GetType() != typeof(businessExceptionType))
                    {

                        //check the reply
                        ErrorMessage = replydata.serviceData.Items[0].ToString();
                        transferWebService = true;
                    }
                    else
                    {
                        transferWebService = false;
                        var returnData = (businessExceptionType)replydata.serviceData.Items[0];
                        ErrorMessage = string.Format("{0} {1}", returnData.responseCode, returnData.message);

                 //       FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer web service returned with business exception " +
                 //           returnData.responseCode.ToString() +
                 //           returnData.message);
                    }
                }
                else
                //No data came back from the web service call...log it
                {
                    transferWebService = true;
             //       FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer JSUP web service No data returned ");
                }
            }
            return transferWebService;
        }

        private bool HandleSTSResponse(storeToStoreReplyType replydata)
        {
            bool transferWebService = false;
            if (replydata.serviceInformation != null)
            {
                //check whether there is a general exception
                if (replydata.serviceInformation.status != null &&
                    replydata.serviceInformation.status.exceptionCode != null)
                {
                    ErrorMessage = string.Format("{0} {1}", replydata.serviceInformation.status.exceptionCode, replydata.serviceInformation.status.sourceMessage);
                    // FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer web service came back with errors "
                    //     + replydata.serviceInformation.status.exceptionCode.ToString() +
                    //     replydata.serviceInformation.status.sourceMessage);
                    //return false;
                }

                if (replydata.serviceData != null && replydata.serviceData.Items != null)
                {

                    if (replydata.serviceData.Items[0].GetType() != typeof(businessExceptionType))
                    {

                        //check the reply
                        ErrorMessage = replydata.serviceData.Items[0].ToString();
                        transferWebService = true;
                    }
                    else
                    {
                        transferWebService = false;
                        var returnData = (businessExceptionType)replydata.serviceData.Items[0];
                        ErrorMessage = string.Format("{0} {1}", returnData.responseCode, returnData.message);

                        //       FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer web service returned with business exception " +
                        //           returnData.responseCode.ToString() +
                        //           returnData.message);
                    }
                }
                else
                //No data came back from the web service call...log it
                {
                    transferWebService = true;
                    //       FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer JSUP web service No data returned ");
                }
            }
            return transferWebService;
        }

        private void txtFromShop_Leave(object sender, EventArgs e)
        {
            string str;
            str = txtFromShop.Text;
            if (str == string.Empty)
            {
                return;
            }
            int TextLength = str.Length;
            if (TextLength < 5)
            {
                str = str.PadLeft(5, '0');
                txtFromShop.Text = str;
            }
            FromShop = str;
        }

        private void txtToShop_Leave(object sender, EventArgs e)
        {
            string str = txtToShop.Text;
            if (str == string.Empty)
            {
                return;
            }
            int TextLength = str.Length;
            if (TextLength < 5)
            {
                str = str.PadLeft(5, '0');
                txtToShop.Text = str;
            }
            ToShop = str;
        }


        private void rbRecreate_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbRecreate.Checked)
           // {
            //    SearchType = "RECREATE";
           // }
        }

        private void rbInbound_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbInbound.Checked)
           // {
           //     SearchType = "INBOUND";
           // }
        }

        private void FrmAutoTransfer_Load(object sender, EventArgs e)
        {

        }

        private void txtTransferNum_Leave(object sender, EventArgs e)
        {
            TransferNumber = txtTransferNum.Text;
        }

    }
}
