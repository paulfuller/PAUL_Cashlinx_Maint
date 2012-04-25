using System;
using System.Collections.Generic;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility.Shared;
using NUnit.Framework;

namespace CommonTests.DesktopProcedures
{
    /// <summary>
    /// Summary description for LayawayPaymentHistoryBuilderTests
    /// </summary>
    [TestFixture]
    public class LayawayPaymentHistoryBuilderTests
    {
        public LayawayPaymentHistoryBuilderTests()
        {
        }

        private LayawayPaymentHistoryBuilder Builder { get; set; }
        private LayawayVO Layaway { get; set; }

        [SetUp]
        public void Setup()
        {
            Layaway = new LayawayVO();
            Layaway.TicketNumber = 227;
            Layaway.DownPayment = 19.49M;
            Layaway.DateMade = new DateTime(2011, 2, 8);
            Layaway.MonthlyPayment = 25.05M;
            Layaway.NumberOfPayments = 7;
            Layaway.FirstPayment = new DateTime(2011, 3, 8);
            Layaway.Receipts = new List<Receipt>();
            AddReceipt(21.49M, Layaway.DateMade, ReceiptEventTypes.LAY, "98580", "97906", new DateTime(2011, 2, 8, 14, 37, 1));
            
        }

        [Test]
        public void InitialLayawayCalculateWithDownPaymentTest()
        {
            Builder = new LayawayPaymentHistoryBuilder();
            Builder.Layaway = Layaway;
            Builder.Calculate();

            // Need to account for the down payment
            Assert.AreEqual(Layaway.NumberOfPayments + 1, Builder.ScheduledPayments.Count);

            // Is the first payment the downpayment
            Assert.AreEqual(1, Builder.ScheduledPayments[0].Payments.Count);
            Assert.IsTrue(Builder.ScheduledPayments[0].IsPaid());
        }

        [Test]
        public void CalculateWithExactPaymentTest()
        {
            AddReceipt(Layaway.MonthlyPayment, Layaway.DateMade, ReceiptEventTypes.LAYPMT, "98581", "97907", new DateTime(2011, 2, 8, 14, 38, 50));

            Builder = new LayawayPaymentHistoryBuilder();
            Builder.Layaway = Layaway;
            Builder.Calculate();

            // Need to account for the down payment
            Assert.AreEqual(Layaway.NumberOfPayments + 1, Builder.ScheduledPayments.Count);

            // Is the first payment the downpayment
            Assert.AreEqual(1, Builder.ScheduledPayments[0].Payments.Count);
            Assert.IsTrue(Builder.ScheduledPayments[0].IsPaid());

            Assert.AreEqual(1, Builder.ScheduledPayments[1].Payments.Count);
            Assert.AreEqual(Layaway.MonthlyPayment, Builder.ScheduledPayments[1].Payments[0].PaymentAmountMade);
            Assert.AreEqual(0M, Builder.ScheduledPayments[1].GetRemainingBalance());
            Assert.IsTrue(Builder.ScheduledPayments[1].IsPaid());
        }

        [Test]
        public void CalculateWithUnderPaymentTest()
        {
            AddReceipt(5M, Layaway.DateMade, ReceiptEventTypes.LAYPMT, "98581", "97907", new DateTime(2011, 2, 8, 14, 38, 50));

            Builder = new LayawayPaymentHistoryBuilder();
            Builder.Layaway = Layaway;
            Builder.Calculate();

            // Need to account for the down payment
            Assert.AreEqual(Layaway.NumberOfPayments + 1, Builder.ScheduledPayments.Count);

            // Is the first payment the downpayment
            Assert.AreEqual(1, Builder.ScheduledPayments[0].Payments.Count);
            Assert.IsTrue(Builder.ScheduledPayments[0].IsPaid());

            Assert.AreEqual(1, Builder.ScheduledPayments[1].Payments.Count);
            Assert.AreEqual(5M, Builder.ScheduledPayments[1].Payments[0].PaymentAmountMade);
            Assert.AreEqual(20.05M, Builder.ScheduledPayments[1].GetRemainingBalance());
            Assert.IsFalse(Builder.ScheduledPayments[1].IsPaid());
        }

        [Test]
        public void CalculateWith2UnderPaymentsTest()
        {
            AddReceipt(5M, Layaway.DateMade, ReceiptEventTypes.LAYPMT, "98581", "97907", new DateTime(2011, 2, 8, 14, 38, 50));
            AddReceipt(6M, Layaway.DateMade, ReceiptEventTypes.LAYPMT, "98582", "97908", new DateTime(2011, 2, 8, 14, 40, 50));

            Builder = new LayawayPaymentHistoryBuilder();
            Builder.Layaway = Layaway;
            Builder.Calculate();

            // Need to account for the down payment
            Assert.AreEqual(Layaway.NumberOfPayments + 1, Builder.ScheduledPayments.Count);

            // Is the first payment the downpayment
            Assert.AreEqual(1, Builder.ScheduledPayments[0].Payments.Count);
            Assert.IsTrue(Builder.ScheduledPayments[0].IsPaid());

            Assert.AreEqual(2, Builder.ScheduledPayments[1].Payments.Count);
            Assert.AreEqual(5M, Builder.ScheduledPayments[1].Payments[0].PaymentAmountMade);
            Assert.AreEqual(6M, Builder.ScheduledPayments[1].Payments[1].PaymentAmountMade);
            Assert.AreEqual(14.05M, Builder.ScheduledPayments[1].GetRemainingBalance());
            Assert.IsFalse(Builder.ScheduledPayments[1].IsPaid());
        }

        [Test]
        public void CalculateWithDownPaymentAndOneOverPaymentTest()
        {
            AddReceipt(60M, Layaway.DateMade, ReceiptEventTypes.LAYPMT, "98581", "97907", new DateTime(2011, 2, 8, 14, 38, 50));

            Builder = new LayawayPaymentHistoryBuilder();
            Builder.Layaway = Layaway;
            Builder.Calculate();

            // Need to account for the down payment
            Assert.AreEqual(Layaway.NumberOfPayments + 1, Builder.ScheduledPayments.Count);

            // Is the first payment the downpayment
            Assert.AreEqual(1, Builder.ScheduledPayments[0].Payments.Count);
            Assert.IsTrue(Builder.ScheduledPayments[0].IsPaid());

            Assert.AreEqual(1, Builder.ScheduledPayments[1].Payments.Count);
            Assert.AreEqual(Layaway.MonthlyPayment, Builder.ScheduledPayments[1].Payments[0].PaymentAmountMade);
            Assert.AreEqual(0M, Builder.ScheduledPayments[1].GetRemainingBalance());
            Assert.IsTrue(Builder.ScheduledPayments[1].IsPaid());

            Assert.AreEqual(1, Builder.ScheduledPayments[2].Payments.Count);
            Assert.AreEqual(Layaway.MonthlyPayment, Builder.ScheduledPayments[2].Payments[0].PaymentAmountMade);
            Assert.AreEqual(0M, Builder.ScheduledPayments[2].GetRemainingBalance());
            Assert.IsTrue(Builder.ScheduledPayments[2].IsPaid());

            Assert.AreEqual(1, Builder.ScheduledPayments[3].Payments.Count);
            Assert.AreEqual(9.9M, Builder.ScheduledPayments[3].Payments[0].PaymentAmountMade);
            Assert.AreEqual(15.15M, Builder.ScheduledPayments[3].GetRemainingBalance());
            Assert.IsFalse(Builder.ScheduledPayments[3].IsPaid());
        }

        [Test]
        public void CalculateWithDownPaymentAndTwoOverPaymentTest()
        {
            AddReceipt(60M, Layaway.DateMade, ReceiptEventTypes.LAYPMT, "98581", "97907", new DateTime(2011, 2, 8, 14, 38, 50));
            AddReceipt(25.05M, Layaway.DateMade, ReceiptEventTypes.LAYPMT, "98582", "97908", new DateTime(2011, 2, 8, 14, 40, 50));

            Builder = new LayawayPaymentHistoryBuilder();
            Builder.Layaway = Layaway;
            Builder.Calculate();

            // Need to account for the down payment
            Assert.AreEqual(Layaway.NumberOfPayments + 1, Builder.ScheduledPayments.Count);

            // Is the first payment the downpayment
            Assert.AreEqual(1, Builder.ScheduledPayments[0].Payments.Count);
            Assert.IsTrue(Builder.ScheduledPayments[0].IsPaid());

            Assert.AreEqual(1, Builder.ScheduledPayments[1].Payments.Count);
            Assert.AreEqual(Layaway.MonthlyPayment, Builder.ScheduledPayments[1].Payments[0].PaymentAmountMade);
            Assert.AreEqual(0M, Builder.ScheduledPayments[1].GetRemainingBalance());
            Assert.IsTrue(Builder.ScheduledPayments[1].IsPaid());

            Assert.AreEqual(1, Builder.ScheduledPayments[2].Payments.Count);
            Assert.AreEqual(Layaway.MonthlyPayment, Builder.ScheduledPayments[2].Payments[0].PaymentAmountMade);
            Assert.AreEqual(0M, Builder.ScheduledPayments[2].GetRemainingBalance());
            Assert.IsTrue(Builder.ScheduledPayments[2].IsPaid());

            Assert.AreEqual(2, Builder.ScheduledPayments[3].Payments.Count);
            Assert.AreEqual(9.9M, Builder.ScheduledPayments[3].Payments[0].PaymentAmountMade);
            Assert.AreEqual(15.15M, Builder.ScheduledPayments[3].Payments[1].PaymentAmountMade);
            Assert.AreEqual(0M, Builder.ScheduledPayments[3].GetRemainingBalance());
            Assert.IsTrue(Builder.ScheduledPayments[3].IsPaid());

            Assert.AreEqual(1, Builder.ScheduledPayments[4].Payments.Count);
            Assert.AreEqual(9.9M, Builder.ScheduledPayments[4].Payments[0].PaymentAmountMade);
            Assert.AreEqual(15.15M, Builder.ScheduledPayments[4].GetRemainingBalance());
            Assert.IsFalse(Builder.ScheduledPayments[4].IsPaid());

            Assert.AreEqual(15.15M, Builder.GetFirstUnpaidPaymentBalance());
        }

        private void AddReceipt(decimal amount, DateTime dateMade, ReceiptEventTypes refEvent, string receiptDetailNumber, string receiptNumber, DateTime timeMade)
        {
            Layaway.Receipts.Add(new Receipt()
            {
                Amount = amount,
                Date = dateMade,
                Event = refEvent.ToString(),
                ReceiptDetailNumber = receiptDetailNumber,
                ReceiptNumber = receiptNumber,
                RefTime = timeMade,
            });
        }
    }
}
