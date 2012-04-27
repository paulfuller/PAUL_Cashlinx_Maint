using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Common.Libraries.Objects.Config
{
    public class StorePrinterVO
    {
        public const string EMPTY_IP = "0.0.0.0";
        public const int  EMPTY_PORT = 0;

        public enum StorePrinterType
        {
            LASER = 0,
            RECEIPT = 1,
            BARCODE = 2,
            UNKNOWN = 3
        }

        public StorePrinterType PrinterType
        {
            private set;
            get;
        }
        
        public string IPAddress
        {
            private set;
            get;
        }

        public int Port
        {
            private set;
            get;
        }

        public bool IsValid
        {
            private set;
            get;
        }

        private void ValidateFields()
        {
            if (this.PrinterType == StorePrinterVO.StorePrinterType.UNKNOWN)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.IPAddress))
            {
                return;
            }

            if (this.Port < 0 || this.Port > 65535)
            {
                return;
            }

            //Printer is valid if this point is reached
            this.IsValid = true;
        }
        
        public StorePrinterVO()
        {
            this.PrinterType = StorePrinterVO.StorePrinterType.UNKNOWN;
            this.IPAddress = EMPTY_IP;
            this.Port = EMPTY_PORT;
            this.IsValid = false;
        }

        public StorePrinterVO(StorePrinterVO.StorePrinterType type, string ipAddr, int port)
        {
            this.IsValid = false;
            this.PrinterType = type;
            this.IPAddress = ipAddr;
            this.Port = port;
            this.ValidateFields();
        }

        public void SetAllPrinterFields(
            StorePrinterVO.StorePrinterType type,
            string ipAddr,
            int port)
        {
            this.IsValid = false;
            this.PrinterType = type;
            this.IPAddress = ipAddr;
            this.Port = port;
            this.ValidateFields();
        }

        public void SetIPAddressAndPort(
            string ipAddr,
            int port)
        {
            this.IsValid = false;
            this.IPAddress = ipAddr;
            this.Port = port;
            this.ValidateFields();
        }

        public void AddDefaultFieldsToPrinterData(string formName, int numCopies, ref Hashtable hashtable)
        {
            hashtable.Add("##TEMPLATEFILENAME##", formName);
            hashtable.Add("##HOWMANYCOPIES##", numCopies.ToString());
            hashtable.Add("##IPADDRESS00##", IPAddress);
            hashtable.Add("##IPADDRESS01##", IPAddress);
            hashtable.Add("##PORTNUMBER00##", Port.ToString());
            hashtable.Add("##PORTNUMBER01##", Port.ToString());
        }

        public void AddDefaultFieldsToPrinterData(string formName, int numCopies, ref Dictionary<string, string> hashtable)
        {
            hashtable.Add("##TEMPLATEFILENAME##", formName);
            hashtable.Add("##HOWMANYCOPIES##", numCopies.ToString());
            hashtable.Add("##IPADDRESS00##", IPAddress);
            hashtable.Add("##IPADDRESS01##", IPAddress);
            hashtable.Add("##PORTNUMBER00##", Port.ToString());
            hashtable.Add("##PORTNUMBER01##", Port.ToString());
        }

        public override string ToString()
        {
            if (!this.IsValid)
                return ("Invalid Printer");
            var sb = new StringBuilder();
            sb.AppendFormat("Type: {0}, ", this.PrinterType);
            sb.AppendFormat("IPAddress: {0}, ", this.IPAddress);
            sb.AppendFormat("Port: {0}", this.Port);
            return (sb.ToString());
        }
    }
}
