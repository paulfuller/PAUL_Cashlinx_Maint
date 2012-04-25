using System;
using System.Collections.Generic;
using System.Linq;
using Common.Libraries.Objects.Retail;

namespace Pawn.Logic.DesktopProcedures
{
    public class OutTheDoorCalculator
    {
        # region Member Variables

        private List<RetailItem> _retailItems;

        # endregion

        # region Constructors

        public OutTheDoorCalculator()
        {
            RetailItems = new List<RetailItem>();
            SalesTaxInfo = new SalesTaxInfo();
        }

        # endregion

        # region Properties

        public decimal BackgroundCheckFee { get; set; }
        public bool HasItemWithNegotiatedPriceGreaterThanRetail { get; set; }
        public decimal OutTheDoorGoal { get; private set; }
        public List<RetailItem> RetailItems
        {
            get
            {
                return _retailItems;
            }
            set
            {
                SetRetailItems(value);
            }
        }
        public decimal RetailPriceSubTotal { get; private set; }
        public SalesTaxInfo SalesTaxInfo { get; set; }
        public decimal ShippingAndHandlingCost { get; set; }

        private decimal CalculatedNegotiatedPriceTotal { get; set; }
        private decimal OutTheDoorGoalMinusShippingAndFees { get; set; }
        private decimal OutTheDoorGoalMinusShippingFeesAndTaxes { get; set; }

        # endregion

        # region Public Methods

        public void Calculate(decimal outTheDoorAmount)
        {
            HasItemWithNegotiatedPriceGreaterThanRetail = false;
            CalculatedNegotiatedPriceTotal = 0M;
            OutTheDoorGoal = outTheDoorAmount;


            if (outTheDoorAmount <= 0 || RetailPriceSubTotal <= 0)
            {
                return;
            }

            OutTheDoorGoalMinusShippingAndFees = outTheDoorAmount - ShippingAndHandlingCost - BackgroundCheckFee;
            OutTheDoorGoalMinusShippingFeesAndTaxes = Math.Round(SalesTaxInfo.CalculatePreTaxAmount(OutTheDoorGoalMinusShippingAndFees), 2);

            if (OutTheDoorGoalMinusShippingFeesAndTaxes > RetailPriceSubTotal)
            {
                return;
            }

            decimal overallDiscountPercentage = OutTheDoorGoalMinusShippingFeesAndTaxes / RetailPriceSubTotal;

            if (RetailItems.Count == 1)
            {
                RetailItem item = RetailItems[0];
                CalculateNegotiatedPrice(item, overallDiscountPercentage);
                CalculateDiscountPercentage(item);
            }
            else
            {
                for (int i = 0; i < RetailItems.Count - 1; i++)
                {
                    RetailItem item = RetailItems[i];
                    CalculateNegotiatedPrice(item, overallDiscountPercentage);
                    CalculateDiscountPercentage(item);
                }

                RetailItem lastItem = RetailItems.Last();
                lastItem.NegotiatedPrice = 0; // set the value to zero so that I can use the method below to add all negotiated prices
                lastItem.NegotiatedPrice = Math.Round((OutTheDoorGoalMinusShippingFeesAndTaxes - GetCalculatedTotalNegotiatedPrice()) / lastItem.Quantity, 2);
                CalculateDiscountPercentage(lastItem);
            }

            DetermineVarianceAndAlterValues();

            HasItemWithNegotiatedPriceGreaterThanRetail = RetailItems.Any(ri => ri.NegotiatedPrice > GetRetailOrNegotiatedPrice(ri));
        }

        public decimal GetCalculatedTotalNegotiatedPrice()
        {
            return (from ri in RetailItems select Math.Round(ri.NegotiatedPrice * ri.Quantity, 2)).Sum();
        }

        public decimal GetCalculatedOutTheDoor()
        {
            decimal totalNegotiatedPrice = GetCalculatedTotalNegotiatedPrice();
            decimal totalNegotiatedPricePlusTax = totalNegotiatedPrice + Math.Round(SalesTaxInfo.CalculateTax(totalNegotiatedPrice), 2);
            return totalNegotiatedPricePlusTax + ShippingAndHandlingCost + BackgroundCheckFee;
        }

        # endregion

        # region Helper Methods

        private void CalculateDiscountPercentage(RetailItem item)
        {
            item.DiscountPercent = item.CalculateDiscountPercentage(item.RetailPrice, item.NegotiatedPrice);
        }

        private void CalculateNegotiatedPrice(RetailItem item, decimal overallDiscountPercentage)
        {
            decimal retailPrice = GetRetailOrNegotiatedPrice(item);

            if (retailPrice == 0)
            {
                item.NegotiatedPrice = 0;
            }
            else
            {
                decimal negotiatedPrice = Math.Round((retailPrice * overallDiscountPercentage), 2);
                item.OutTheDoor = true;
                item.NegotiatedPrice = negotiatedPrice;
                CalculatedNegotiatedPriceTotal += Math.Round(negotiatedPrice * item.Quantity, 2);
            }
        }

        private void DetermineVarianceAndAlterValues()
        {
            decimal outTheDoorTaxSubTotal = 0;
            decimal outTheDoorTaxSubTotalVariance = 0;

            CalculatedNegotiatedPriceTotal = GetCalculatedTotalNegotiatedPrice();
            outTheDoorTaxSubTotal = (CalculatedNegotiatedPriceTotal + Math.Round(SalesTaxInfo.CalculateTax(CalculatedNegotiatedPriceTotal), 2));
            outTheDoorTaxSubTotalVariance = OutTheDoorGoalMinusShippingAndFees - outTheDoorTaxSubTotal;

            DistributeVarianceOverItems(outTheDoorTaxSubTotalVariance);
        }

        private void DistributeVarianceOverItems(decimal outTheDoorTaxSubTotalVariance)
        {
            if (outTheDoorTaxSubTotalVariance == 0M)
            {
                return;
            }

            decimal offSetAmount = (outTheDoorTaxSubTotalVariance < 0 ? -.01M : .01M);
            int quantityToFind = (int)(Math.Abs(outTheDoorTaxSubTotalVariance) * 100);

            var itemsWithDistributableQuantity = (from ri in RetailItems
                                                  where ri.Quantity <= quantityToFind && ri.Quantity % quantityToFind == 0 && ri.NegotiatedPrice + offSetAmount > 0
                                                  select ri).OrderByDescending(ri => ri.Quantity);
            foreach (RetailItem item in itemsWithDistributableQuantity)
            {
                item.NegotiatedPrice += offSetAmount;
                CalculateDiscountPercentage(item);
                outTheDoorTaxSubTotalVariance -= (offSetAmount * item.Quantity);

                if (Math.Abs(outTheDoorTaxSubTotalVariance) == 0)
                {
                    break;
                }
            }

            if (outTheDoorTaxSubTotalVariance != 0)
            {
                var itemsWithQuantity1 = RetailItems.FindAll(ri => ri.Quantity == 1 && ri.NegotiatedPrice + (offSetAmount * quantityToFind) > 0).OrderByDescending(ri => ri.NegotiatedPrice);
                foreach (RetailItem item in itemsWithQuantity1)
                {
                    item.NegotiatedPrice += offSetAmount * quantityToFind;
                    CalculateDiscountPercentage(item);
                    outTheDoorTaxSubTotalVariance -= (offSetAmount * quantityToFind);

                    if (Math.Abs(outTheDoorTaxSubTotalVariance) == 0)
                    {
                        break;
                    }
                }
            }
        }

        private decimal GetRetailOrNegotiatedPrice(RetailItem item)
        {
            return item.RetailPrice == 0 ? item.NegotiatedPrice : item.RetailPrice;
        }

        private void SetRetailItems(List<RetailItem> items)
        {
            RetailPriceSubTotal = 0;
            if (items == null)
            {
                return;
            }

            _retailItems = items.FindAll(ri => ri.Quantity > 0 && GetRetailOrNegotiatedPrice(ri) > 0);

            RetailPriceSubTotal = (from i in RetailItems
                                   select GetRetailOrNegotiatedPrice(i) * i.Quantity).Sum();
        }

        # endregion
    }
}
