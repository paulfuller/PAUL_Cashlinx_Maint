using System;
using System.Collections.Generic;
using Common.Libraries.Objects.Business;

namespace Common.Libraries.Objects
{
    public class SiteId
    {
        public string Alias
        {
            get;
            set;
        }
        public string StoreNumber
        {
            get;
            set;
        }
        public string Company
        {
            get;
            set;
        }
        public string State
        {
            get;
            set;
        }
        public string TerminalId
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }
        public decimal LoanAmount
        {
            get;
            set;
        }
        public string StoreId
        {
            get;
            set;
        }
        public string StoreName
        {
            get;
            set;
        }
        public string StoreAddress1
        {
            get;
            set;
        }
        public string StoreAddress2
        {
            get;
            set;
        }
        public string StoreCityName
        {
            get;
            set;
        }
        public string StoreZipCode
        {
            get;
            set;
        }

        public string LocalTimeZone
        {
            get;
            set;
        }
        public string Market
        {
            get;
            set;
        }
        public string Region
        {
            get;
            set;
        }
        public string BusinessUnit
        {
            get;
            set;
        }
        public string CountryCode
        {
            get;
            set;
        }
        public decimal StoreTax1
        {
            get;
            set;
        }
        public decimal StoreTax2
        {
            get;
            set;
        }
        public decimal StoreTax3
        {
            get;
            set;
        }
        public string StoreNickName
        {
            get;
            set;
        }
        public string StoreBrandName
        {
            get;
            set;
        }
        public DateTime StoreOpenDate
        {
            get;
            set;
        }
        public string StoreTaxID
        {
            get;
            set;
        }
        public string StoreEffin
        {
            get;set;
        }
        public string StoreLocator
        {
            get;
            set;
        }
        public DateTime EffDateChange
        {
            get;
            set;
        }
        public string StoreManager
        {
            get;
            set;
        }
        public string StorePhoneNo
        {
            get;
            set;
        }
        public string StoreFaxNo
        {
            get;
            set;
        }
        public string StoreModemNo
        {
            get;
            set;
        }
        public string StoreEmail
        {
            get;
            set;
        }
        public string StorePhoneNo1
        {
            get;
            set;
        }
        public string StorePhoneNo2
        {
            get;
            set;
        }
        public string StorePhoneNo3
        {
            get;
            set;
        }
        public bool DaylightSavings
        {
            get;
            set;
        }
        public string Collection800
        {
            get;
            set;
        }
        public bool DeferScanning
        {
            get;
            set;
        }
        public string CompanyNumber
        {
            get;
            set;
        }
        public string AchFileName
        {
            get;
            set;
        }
        public DateTime DeferScanDate
        {
            get;
            set;
        }
        public DateTime ConversionDate
        {
            get;
            set;
        }
        public int AliasID
        {
            get;
            set;
        }
        public bool IsTopsExist
        {
            get;
            set;
        }
        public bool IsTopsSafe
        {
            get;
            set;
        }
        public bool IsIntegrated
        {
            get;
            set;
        }
        public bool IsPawnPrimary
        {
            get;
            set;
        }
        public List<string> AvailableButtons
        {
            get;
            set;
        }
        public List<StoreTaxVO> StoreTaxes
        {
            get;
            set;
        }

        public string FireArmLicenceNo
        {
            get;
            set;
        }

        public DateTime ForceCloseTime
        {
            get; 
            set;
        }

        public string PartyRoleId
        {
            get; 
            set; 
        }

        public SiteId()
        {
            this.Alias = string.Empty;
            this.StoreNumber = string.Empty;
            this.Company = string.Empty;
            this.State = string.Empty;
            this.Date = DateTime.Now;
            this.LoanAmount = 0.0M;
            this.TerminalId = string.Empty;
            this.AchFileName = string.Empty;
            this.AliasID = 0;
            this.BusinessUnit = string.Empty;
            this.Collection800 = string.Empty;
            this.CompanyNumber = string.Empty;
            this.ConversionDate = DateTime.Now;
            this.CountryCode = string.Empty;
            this.DaylightSavings = true;
            this.DeferScanDate = DateTime.Now;
            this.DeferScanning = false;
            this.EffDateChange = DateTime.Now;
            this.LocalTimeZone = string.Empty;
            this.Market = string.Empty;
            this.Region = string.Empty;
            this.StoreAddress1 = string.Empty;
            this.StoreAddress2 = string.Empty;
            this.StoreBrandName = string.Empty;
            this.StoreCityName = string.Empty;
            this.StoreEffin = string.Empty;
            this.StoreEmail = string.Empty;
            this.StoreFaxNo = string.Empty;
            this.StoreId = string.Empty;
            this.StoreLocator = string.Empty;
            this.StoreManager = string.Empty;
            this.StoreModemNo = string.Empty;
            this.StoreName = string.Empty;
            this.StoreNickName = string.Empty;
            this.StoreOpenDate = DateTime.Now;
            this.StorePhoneNo = string.Empty;
            this.StorePhoneNo1 = string.Empty;
            this.StorePhoneNo2 = string.Empty;
            this.StorePhoneNo3 = string.Empty;
            this.StoreTax1 = 0.0M;
            this.StoreTax2 = 0.0M;
            this.StoreTax3 = 0.0M;
            this.StoreTaxID = string.Empty;
            this.StoreZipCode = string.Empty;
            this.FireArmLicenceNo = string.Empty;
            this.ForceCloseTime = DateTime.Now;
            this.PartyRoleId = string.Empty;
            AvailableButtons = new List<string>();
            StoreTaxes = new List<StoreTaxVO>();
            

        }
    }
}
