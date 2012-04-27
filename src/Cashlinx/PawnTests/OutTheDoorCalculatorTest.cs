using System.Collections.Generic;
using Common.Libraries.Objects.Retail;
using NUnit.Framework;
using Pawn.Logic.DesktopProcedures;
using Common.Controllers.Database.Procedures;

namespace PawnTests
{
    [TestFixture]
    public class OutTheDoorCalculatorTest
    {
        private OutTheDoorCalculator Calculator { get; set; }
        private List<RetailItem> RetailItems { get; set; }
        private SalesTaxInfo SalesTaxInfo { get; set; }

        [SetUp]
        public void Setup()
        {
            Calculator = new OutTheDoorCalculator();
            RetailItems = new List<RetailItem>();
            SalesTaxInfo = new SalesTaxInfo();
        }

        [Test]
        public void BlueSkySimpleCalculation()
        {
            Calculator.BackgroundCheckFee = 0M;
            Calculator.SalesTaxInfo = SalesTaxInfo;
            Calculator.ShippingAndHandlingCost = 0M;

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 10M,
                Quantity = 1
            });

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 10M,
                Quantity = 1
            });

            Calculator.RetailItems = RetailItems;

            foreach (RetailItem item in RetailItems)
            {
                Assert.AreEqual(0M, item.DiscountPercent);
                Assert.AreEqual(0M, item.NegotiatedPrice);
            }

            Assert.AreEqual(20M, Calculator.RetailPriceSubTotal);

            Calculator.Calculate(10M);
            Assert.AreEqual(10M, Calculator.GetCalculatedOutTheDoor());

            Assert.AreEqual(50M, RetailItems[0].DiscountPercent);
            Assert.AreEqual(5M, RetailItems[0].NegotiatedPrice);
            Assert.AreEqual(50M, RetailItems[1].DiscountPercent);
            Assert.AreEqual(5M, RetailItems[1].NegotiatedPrice);
        }

        [Test]
        public void BlueSkySimpleWithTaxesShippingAndFees()
        {
            Calculator.BackgroundCheckFee = 3M;
            Calculator.SalesTaxInfo = new SalesTaxInfo(8.25M);
            Calculator.ShippingAndHandlingCost = 8.5M;

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 10M,
                Quantity = 1
            });

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 10M,
                Quantity = 1
            });

            Calculator.RetailItems = RetailItems;

            foreach (RetailItem item in RetailItems)
            {
                Assert.AreEqual(0M, item.DiscountPercent);
                Assert.AreEqual(0M, item.NegotiatedPrice);
            }

            Assert.AreEqual(20M, Calculator.RetailPriceSubTotal);

            Calculator.Calculate(30M);
            Assert.AreEqual(30M, Calculator.GetCalculatedOutTheDoor());

            Assert.AreEqual(14.6M, RetailItems[0].DiscountPercent);
            Assert.AreEqual(8.54M, RetailItems[0].NegotiatedPrice);
            Assert.AreEqual(14.5M, RetailItems[1].DiscountPercent);
            Assert.AreEqual(8.55M, RetailItems[1].NegotiatedPrice);
        }

        [Test]
        public void CalculateWithCaccItem()
        {
            Calculator.BackgroundCheckFee = 0M;
            Calculator.SalesTaxInfo = SalesTaxInfo;
            Calculator.ShippingAndHandlingCost = 0M;

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 0M,
                Quantity = 1
            });

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 10M,
                Quantity = 1
            });

            Calculator.RetailItems = RetailItems;

            foreach (RetailItem item in RetailItems)
            {
                Assert.AreEqual(0M, item.DiscountPercent);
                Assert.AreEqual(0M, item.NegotiatedPrice);
            }

            Assert.AreEqual(10M, Calculator.RetailPriceSubTotal);

            Calculator.Calculate(8M);
            Assert.AreEqual(8M, Calculator.GetCalculatedOutTheDoor());

            Assert.AreEqual(0M, RetailItems[0].DiscountPercent);
            Assert.AreEqual(0M, RetailItems[0].NegotiatedPrice);
            Assert.AreEqual(20M, RetailItems[1].DiscountPercent);
            Assert.AreEqual(8M, RetailItems[1].NegotiatedPrice);
        }

        [Test]
        public void CalculateWithCaccItemAndTax()
        {
            Calculator.BackgroundCheckFee = 0M;
            Calculator.SalesTaxInfo = new SalesTaxInfo(8.25M);
            Calculator.ShippingAndHandlingCost = 0M;

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 0M,
                Quantity = 1
            });

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 10M,
                Quantity = 1
            });

            Calculator.RetailItems = RetailItems;

            foreach (RetailItem item in RetailItems)
            {
                Assert.AreEqual(0M, item.DiscountPercent);
                Assert.AreEqual(0M, item.NegotiatedPrice);
            }

            Assert.AreEqual(10M, Calculator.RetailPriceSubTotal);

            Calculator.Calculate(8M);
            Assert.AreEqual(8M, Calculator.GetCalculatedOutTheDoor());

            Assert.AreEqual(0M, RetailItems[0].DiscountPercent);
            Assert.AreEqual(0M, RetailItems[0].NegotiatedPrice);
            Assert.AreEqual(26.1M, RetailItems[1].DiscountPercent);
            Assert.AreEqual(7.39M, RetailItems[1].NegotiatedPrice);
        }

        [Test]
        public void CalculateWithCaccItemWithNegotiatedPrice()
        {
            Calculator.BackgroundCheckFee = 0M;
            Calculator.SalesTaxInfo = SalesTaxInfo;
            Calculator.ShippingAndHandlingCost = 0M;

            RetailItems.Add(new RetailItem()
            {
                NegotiatedPrice = 5M,
                RetailPrice = 0M,
                Quantity = 1
            });

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 10M,
                Quantity = 1
            });

            Calculator.RetailItems = RetailItems;

            foreach (RetailItem item in RetailItems)
            {
                Assert.AreEqual(0M, item.DiscountPercent);
            }

            Assert.AreEqual(15M, Calculator.RetailPriceSubTotal);

            Calculator.Calculate(8M);
            Assert.AreEqual(8M, Calculator.GetCalculatedOutTheDoor());

            Assert.AreEqual(0M, RetailItems[0].DiscountPercent);
            Assert.AreEqual(2.67M, RetailItems[0].NegotiatedPrice);
            Assert.AreEqual(46.7M, RetailItems[1].DiscountPercent);
            Assert.AreEqual(5.33M, RetailItems[1].NegotiatedPrice);
        }

        [Test]
        public void CalculateWithCaccItemWithNegotiatedPriceAndTax()
        {
            Calculator.BackgroundCheckFee = 0M;
            Calculator.SalesTaxInfo = new SalesTaxInfo(8.25M);
            Calculator.ShippingAndHandlingCost = 0M;

            RetailItems.Add(new RetailItem()
            {
                NegotiatedPrice = 5M,
                RetailPrice = 0M,
                Quantity = 1
            });

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 10M,
                Quantity = 1
            });

            Calculator.RetailItems = RetailItems;

            foreach (RetailItem item in RetailItems)
            {
                Assert.AreEqual(0M, item.DiscountPercent);
            }

            Assert.AreEqual(15M, Calculator.RetailPriceSubTotal);

            Calculator.Calculate(8M);
            Assert.AreEqual(8M, Calculator.GetCalculatedOutTheDoor());

            Assert.AreEqual(0M, RetailItems[0].DiscountPercent);
            Assert.AreEqual(2.46M, RetailItems[0].NegotiatedPrice);
            Assert.AreEqual(50.7M, RetailItems[1].DiscountPercent);
            Assert.AreEqual(4.93M, RetailItems[1].NegotiatedPrice);
        }

        [Test]
        public void ReduceItemRetailPriceToNotExceedOutTheDoorAmountWithSingleItem()
        {
            Calculator.BackgroundCheckFee = 0M;
            Calculator.SalesTaxInfo = new SalesTaxInfo(8.25M);
            Calculator.ShippingAndHandlingCost = 0M;

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 399.99M,
                Quantity = 1
            });

            Calculator.RetailItems = RetailItems;

            foreach (RetailItem item in RetailItems)
            {
                Assert.AreEqual(0M, item.DiscountPercent);
                Assert.AreEqual(0M, item.NegotiatedPrice);
            }

            Assert.AreEqual(399.99M, Calculator.RetailPriceSubTotal);

            Calculator.Calculate(400M);
            Assert.AreEqual(399.99M, Calculator.GetCalculatedOutTheDoor());

            Assert.AreEqual(7.62M, RetailItems[0].DiscountPercent);
            Assert.AreEqual(369.51M, RetailItems[0].NegotiatedPrice);
        }

        [Test]
        public void SingleItemQuantityGreaterThan1()
        {
            Calculator.BackgroundCheckFee = 0M;
            Calculator.SalesTaxInfo = new SalesTaxInfo(8.25M);
            Calculator.ShippingAndHandlingCost = 0M;

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 20M,
                Quantity = 3
            });

            Calculator.RetailItems = RetailItems;

            foreach (RetailItem item in RetailItems)
            {
                Assert.AreEqual(0M, item.DiscountPercent);
                Assert.AreEqual(0M, item.NegotiatedPrice);
            }

            Assert.AreEqual(60M, Calculator.RetailPriceSubTotal);

            Calculator.Calculate(40M);
            Assert.AreEqual(40.01M, Calculator.GetCalculatedOutTheDoor());

            Assert.AreEqual(38.4M, RetailItems[0].DiscountPercent);
            Assert.AreEqual(12.32M, RetailItems[0].NegotiatedPrice);
        }

        [Test]
        public void ReduceItemRetailPriceToNotExceedOutTheDoorAmountWithMultipleItems()
        {
            Calculator.BackgroundCheckFee = 0M;
            Calculator.SalesTaxInfo = new SalesTaxInfo(8.25M);
            Calculator.ShippingAndHandlingCost = 0M;

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 599.99M,
                Quantity = 1
            });
            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 30M,
                Quantity = 1
            });
            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 79.99M,
                Quantity = 1
            });

            Calculator.RetailItems = RetailItems;

            foreach (RetailItem item in RetailItems)
            {
                Assert.AreEqual(0M, item.DiscountPercent);
                Assert.AreEqual(0M, item.NegotiatedPrice);
            }

            Assert.AreEqual(709.98M, Calculator.RetailPriceSubTotal);

            Calculator.Calculate(750M);
            Assert.AreEqual(750M, Calculator.GetCalculatedOutTheDoor());

            Assert.AreEqual(2.413M, RetailItems[0].DiscountPercent);
            Assert.AreEqual(585.51M, RetailItems[0].NegotiatedPrice);

            Assert.AreEqual(2.40M, RetailItems[1].DiscountPercent);
            Assert.AreEqual(29.28M, RetailItems[1].NegotiatedPrice);

            Assert.AreEqual(2.425M, RetailItems[2].DiscountPercent);
            Assert.AreEqual(78.05M, RetailItems[2].NegotiatedPrice);
        }

        [Test]
        public void MultipleItemsAndQuantityGreaterThan1()
        {
            Calculator.BackgroundCheckFee = 0M;
            Calculator.SalesTaxInfo = new SalesTaxInfo(8.25M);
            Calculator.ShippingAndHandlingCost = 0M;

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 46.99M,
                Quantity = 1
            });
            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 7.99M,
                Quantity = 1
            });
            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 3.33M,
                Quantity = 3
            });

            Calculator.RetailItems = RetailItems;

            foreach (RetailItem item in RetailItems)
            {
                Assert.AreEqual(0M, item.DiscountPercent);
                Assert.AreEqual(0M, item.NegotiatedPrice);
            }

            Assert.AreEqual(64.97M, Calculator.RetailPriceSubTotal);

            Calculator.Calculate(70M);
            Assert.AreEqual(69.99M, Calculator.GetCalculatedOutTheDoor());

            Assert.AreEqual(0.511M, RetailItems[0].DiscountPercent);
            Assert.AreEqual(46.75M, RetailItems[0].NegotiatedPrice);

            Assert.AreEqual(0.501M, RetailItems[1].DiscountPercent);
            Assert.AreEqual(7.95M, RetailItems[1].NegotiatedPrice);

            Assert.AreEqual(0.3M, RetailItems[2].DiscountPercent);
            Assert.AreEqual(3.32M, RetailItems[2].NegotiatedPrice);
        }

        [Test]
        public void BZ_299()
        {
            Calculator.BackgroundCheckFee = 0M;
            Calculator.SalesTaxInfo = new SalesTaxInfo(8.25M);
            Calculator.ShippingAndHandlingCost = 0M;

            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 200M,
                Quantity = 1
            });
            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 20M,
                Quantity = 1
            });
            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 50M,
                Quantity = 1
            });
            RetailItems.Add(new RetailItem()
            {
                RetailPrice = 250M,
                Quantity = 1
            });

            Calculator.RetailItems = RetailItems;

            foreach (RetailItem item in RetailItems)
            {
                Assert.AreEqual(0M, item.DiscountPercent);
                Assert.AreEqual(0M, item.NegotiatedPrice);
            }

            Assert.AreEqual(520M, Calculator.RetailPriceSubTotal);

            Calculator.Calculate(400M);
            Assert.AreEqual(399.99M, Calculator.GetCalculatedOutTheDoor());

            Assert.AreEqual(28.945M, RetailItems[0].DiscountPercent);
            Assert.AreEqual(142.11M, RetailItems[0].NegotiatedPrice);

            Assert.AreEqual(28.95M, RetailItems[1].DiscountPercent);
            Assert.AreEqual(14.21M, RetailItems[1].NegotiatedPrice);

            Assert.AreEqual(28.94M, RetailItems[2].DiscountPercent);
            Assert.AreEqual(35.53M, RetailItems[2].NegotiatedPrice);

            Assert.AreEqual(28.936M, RetailItems[3].DiscountPercent);
            Assert.AreEqual(177.66M, RetailItems[3].NegotiatedPrice);
        }
    }
}
