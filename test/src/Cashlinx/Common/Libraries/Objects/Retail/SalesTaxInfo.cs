using System;
using System.Collections.Generic;
using Common.Libraries.Objects.Business;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Application;

namespace Common.Libraries.Objects.Retail
{
    [Serializable]
    public class SalesTaxInfo
    {
        // for Serialization
        public SalesTaxInfo()
        {
        }

        public SalesTaxInfo(List<StoreTaxVO> storeTaxes)
        {
            Initialize(storeTaxes);
        }

        public SalesTaxInfo(decimal actualTaxPercentage)
        {
            Initialize(actualTaxPercentage);
        }

        public StoreTaxVO CityStoreTax { get; set; }
        public StoreTaxVO CountyStoreTax { get; set; }
        public StoreTaxVO StateStoreTax { get; set; }

        public decimal ActualTaxPercentage { get; set; }
        public string Comments { get; set; }
        public bool ExcludeCity { get; set; }
        public bool ExcludeCounty { get; set; }
        public bool ExcludeState { get; set; }

        public decimal AdjustedTaxRate
        {
            get
            {
                if (!CalculateAdjustedTaxRate)
                {
                    return ActualTaxPercentage;
                }

                decimal taxRate = 0;

                if (!ExcludeCity)
                {
                    taxRate += CityStoreTax.TaxRate;
                }

                if (!ExcludeCounty)
                {
                    taxRate += CountyStoreTax.TaxRate;
                }

                if (!ExcludeState)
                {
                    taxRate += StateStoreTax.TaxRate;
                }

                return taxRate;
            }
        }

        private bool CalculateAdjustedTaxRate { get; set; }

        private List<StoreTaxVO> CachedStoreTaxes { get; set; }
        private decimal CachedActualTaxPercentage { get; set; }

        //Takes the Tax Adjustment Rounding Number into consideration when calculating taxes based on rule PWN_BR-179 
        //(total * tax_rate * 0.01 + taxAdjustmentRoundingNumber)
        public decimal CalculateTax(decimal subtotal)
        {
            var taxAdjustmentRoundingNumber = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetSalesTaxRoundingAdjustment(GlobalDataAccessor.Instance.CurrentSiteId);
            var tax = 0.0m; 

            tax = Math.Round(subtotal * AdjustedTaxRate / 100, 2, MidpointRounding.AwayFromZero);

            tax = tax + taxAdjustmentRoundingNumber;

            return tax;
        }

        public decimal CalculatePreTaxAmount(decimal amountWithTax)
        {
            return Math.Round(amountWithTax / (1 + AdjustedTaxRate / 100), 2);
        }

        public bool HasExclusions()
        {
            return ExcludeCity || ExcludeCounty || ExcludeState;
        }

        public void RevertExclusions()
        {
            if (CachedStoreTaxes != null)
            {
                Initialize(CachedStoreTaxes);
            }
            else
            {
                Initialize(CachedActualTaxPercentage);
            }
        }

        private void CacheTaxInfo(List<StoreTaxVO> storeTaxes)
        {
            foreach (StoreTaxVO taxInfo in storeTaxes)
            {
                if (taxInfo.TaxAuth == "CITY")
                {
                    CityStoreTax = taxInfo;
                }
                if (taxInfo.TaxAuth == "STATE")
                {
                    StateStoreTax = taxInfo;
                }
                if (taxInfo.TaxAuth == "COUNTY")
                {
                    CountyStoreTax = taxInfo;
                }
            }
        }

        private void Initialize(List<StoreTaxVO> storeTaxes)
        {
            CalculateAdjustedTaxRate = true;
            CacheTaxInfo(storeTaxes);
            CachedStoreTaxes = storeTaxes;
            ActualTaxPercentage = 0M;
        }

        private void Initialize(decimal actualTaxPercentage)
        {
            CalculateAdjustedTaxRate = false;
            ActualTaxPercentage = actualTaxPercentage;
        }
    }
}
