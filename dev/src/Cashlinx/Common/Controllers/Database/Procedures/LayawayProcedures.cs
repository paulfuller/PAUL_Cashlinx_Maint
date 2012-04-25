using System;
using System.Collections.Generic;
using System.Linq;
using Common.Controllers.Application;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Common.Controllers.Database.Procedures
{
    public static class LayawayProcedures
    {
        public static bool CustomerPassesFirearmAgeCheckForItems(LayawayVO layaway, CustomerVO currentCustomer)
        {
            bool firearmAgeCheckPassed = true;

            var item = (from itemData in layaway.RetailItems
                        where itemData.IsGun
                        select itemData).FirstOrDefault();
            if (item != null)
            {
                return true; // no guns
            }

            if (currentCustomer == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(currentCustomer.CustomerNumber))
            {
                return true; //This is a vendor sale so no background check form needed
            }

            var handGunItem = (from itemData in layaway.RetailItems
                               where itemData.IsHandGun()
                               select itemData).FirstOrDefault();

            var lGunItem = (from itemData in layaway.RetailItems
                            where itemData.IsLongGun()
                            select itemData).FirstOrDefault();

            if (lGunItem != null)
            {
                if (currentCustomer.Age < Convert.ToInt16(CustomerProcedures.getLongGunValidAge(GlobalDataAccessor.Instance.DesktopSession)))
                {
                    firearmAgeCheckPassed = false;
                }
            }
            if (firearmAgeCheckPassed && handGunItem != null)
            {
                if (currentCustomer.Age < Convert.ToInt16(CustomerProcedures.getHandGunValidAge(GlobalDataAccessor.Instance.DesktopSession)))
                {
                    firearmAgeCheckPassed = false;
                }
            }

            return firearmAgeCheckPassed;
        }

        public static bool IsLayawayEligibleForForfeiture(DateTime lastPayment, SiteId siteId)
        {
            int waitDaysForLayawayForfeitureEligibility = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetWaitDaysForLayawayForfeitureEligibility(siteId);
            return lastPayment <= ShopDateTime.Instance.FullShopDateTime.AddDays(-waitDaysForLayawayForfeitureEligibility);
        }

        public static void SetBackgroundCheckFee(LayawayVO layaway, decimal backgroundCheckFee)
        {
            if (layaway.Fees != null)
            {
                int idx = layaway.Fees.FindIndex(feeData => feeData.FeeType == FeeTypes.BACKGROUND_CHECK_FEE);
                if (idx >= 0)
                    layaway.Fees.RemoveAt(idx);
            }
            else
                layaway.Fees = new List<Fee>();

            if (backgroundCheckFee <= 0)
            {
                return;
            }

            Fee fee = new Fee()
            {
                FeeType = FeeTypes.BACKGROUND_CHECK_FEE,
                Value = backgroundCheckFee,
                OriginalAmount = backgroundCheckFee,
                FeeState = FeeStates.ASSESSED,
                FeeDate = Utilities.GetDateTimeValue(ShopDateTime.Instance.ShopTransactionTime, DateTime.Now)
            };
            layaway.Fees.Add(fee);
        }
    }
}
