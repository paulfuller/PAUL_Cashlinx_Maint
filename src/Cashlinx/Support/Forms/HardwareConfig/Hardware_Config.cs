using System;
using System.Data;

namespace Support.Forms.HardwareConfig
{
    public sealed class Hardware_Config
    {
        private string storeNumber;
        private string storeID;
        private Int32 currentTab;
        public DataSet PrinterList;
        public DataSet HardwareList;
        public DataSet AllPrinters;

        static readonly Hardware_Config instance = new Hardware_Config();


        public static Hardware_Config Instance
        {
            get
            {
                return ( instance ); 
            }
        }

        /// Static constructor - forces compiler to initialize the object prior to any code access
        static Hardware_Config()
        {

        }

        public Hardware_Config()
        {
            storeNumber = "00000";
            storeID = "0";
        }

        public string StoreNumber
        {
            get
            {
                return storeNumber;
            }
            set
            {
                storeNumber = value.PadLeft(5, '0');
            }
        }

        public string StoreID
        {
            get
            {
                return storeID;
            }
            set
            {
                storeID = value;
            }

        }
        public Int32 CurrentTab
        {
            get
            {
                return currentTab;
            }
            set
            {
                currentTab = value;
            }

        }


    }
}
