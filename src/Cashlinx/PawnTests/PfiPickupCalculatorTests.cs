using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Pawn.Logic.DesktopProcedures;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Shared;
using Common.Controllers.Application;
using PawnTests.TestEnvironment;

namespace PawnTests
{
    [TestFixture]
    public class PfiPickupCalculatorTests
    {
        private PfiPickupCalculator PfiPickupCalculator { get; set; }
        private PawnLoan PawnLoan { get; set; }
        private DateTime PickupDate { get; set; }

        [SetUp]
        public void Setup()
        {
            PickupDate = new DateTime(2011, 10, 1);

            PawnLoan = new PawnLoan();
            PawnLoan.InterestAmount = 3.75M; // Interest Amount before Partial Payment
            PawnLoan.ServiceCharge = 4M; // Service Amount before Partial Payment
            PawnLoan.DateMade = new DateTime(2011, 8, 1);
            PfiPickupCalculator = new PfiPickupCalculator(PawnLoan, TestSiteIds.Store00901, PickupDate);
        }

        [Test]
        public void TestNoFees()
        {
            PawnLoan.CurrentPrincipalAmount = 80M;

            Assert.AreEqual(0, PfiPickupCalculator.ApplicableFees.Count);
            Assert.IsFalse(PfiPickupCalculator.HasPartialPayment);

            PfiPickupCalculator.Calculate();

            Assert.IsFalse(PfiPickupCalculator.HasPartialPayment);
            Assert.AreEqual(80M, PfiPickupCalculator.PickupAmount);
            Assert.AreEqual(0, PfiPickupCalculator.ApplicableFees.Count);
        }

        [Test]
        public void TestwithInterstStorage()
        {
            PawnLoan.CurrentPrincipalAmount = 80M;
            AddFee(PawnLoan, FeeTypes.INTEREST, FeeStates.ASSESSED, 5M, false);
            AddFee(PawnLoan, FeeTypes.STORAGE, FeeStates.ASSESSED, 4M, false);

            Assert.AreEqual(0, PfiPickupCalculator.ApplicableFees.Count);
            Assert.IsFalse(PfiPickupCalculator.HasPartialPayment);

            PfiPickupCalculator.Calculate();

            Assert.IsFalse(PfiPickupCalculator.HasPartialPayment);
            Assert.AreEqual(87.75M, PfiPickupCalculator.PickupAmount);
            Assert.AreEqual(2, PfiPickupCalculator.ApplicableFees.Count);
        }

        [Test]
        public void TestwithInterstStorageLate()
        {
            PawnLoan.CurrentPrincipalAmount = 80M;
            PawnLoan.PickupLateFinAmount = 5M;
            PawnLoan.PickupLateServAmount = 5M;
            AddFee(PawnLoan, FeeTypes.INTEREST, FeeStates.ASSESSED, 5M, false);
            AddFee(PawnLoan, FeeTypes.STORAGE, FeeStates.ASSESSED, 4M, false);
            AddFee(PawnLoan, FeeTypes.LATE, FeeStates.ASSESSED, 10M, false);

            Assert.AreEqual(0, PfiPickupCalculator.ApplicableFees.Count);
            Assert.IsFalse(PfiPickupCalculator.HasPartialPayment);

            PfiPickupCalculator.Calculate();

            Assert.IsFalse(PfiPickupCalculator.HasPartialPayment);
            Assert.AreEqual(97.75M, PfiPickupCalculator.PickupAmount);
            Assert.AreEqual(3, PfiPickupCalculator.ApplicableFees.Count);
        }

        [Test]
        public void TestwithInterstStorageMailer()
        {
            PawnLoan.CurrentPrincipalAmount = 80M;
            PawnLoan.PickupLateFinAmount = 5M;
            PawnLoan.PickupLateServAmount = 5M;
            AddFee(PawnLoan, FeeTypes.INTEREST, FeeStates.ASSESSED, 5M, false);
            AddFee(PawnLoan, FeeTypes.STORAGE, FeeStates.ASSESSED, 4M, false);
            AddFee(PawnLoan, FeeTypes.LATE, FeeStates.ASSESSED, 10M, false);
            AddFee(PawnLoan, FeeTypes.MAILER_CHARGE, FeeStates.ASSESSED, 2M, false);

            Assert.AreEqual(0, PfiPickupCalculator.ApplicableFees.Count);
            Assert.IsFalse(PfiPickupCalculator.HasPartialPayment);

            PfiPickupCalculator.Calculate();

            Assert.IsFalse(PfiPickupCalculator.HasPartialPayment);
            Assert.AreEqual(99.75M, PfiPickupCalculator.PickupAmount);
            Assert.AreEqual(4, PfiPickupCalculator.ApplicableFees.Count);
        }

        [Test]
        public void TestwithPartialPayment()
        {
            PawnLoan.CurrentPrincipalAmount = 80M;
            AddFee(PawnLoan, FeeTypes.INTEREST, FeeStates.ASSESSED, 5M, false);
            AddFee(PawnLoan, FeeTypes.STORAGE, FeeStates.ASSESSED, 4M, false);

            PawnLoan.PartialPayments.Add(new PartialPayment
                                         {
                                             Date_Made = new DateTime(2011, 9, 12)
                                         });

            Assert.AreEqual(0, PfiPickupCalculator.ApplicableFees.Count);

            PfiPickupCalculator.Calculate();

            //WriteValues();

            Assert.IsTrue(PfiPickupCalculator.HasPartialPayment);
            Assert.AreEqual(0, PfiPickupCalculator.MonthsToPay);
            Assert.AreEqual(0, PfiPickupCalculator.DaysToPay);
            Assert.AreEqual(0, PfiPickupCalculator.ApplicableFees.Count);
            Assert.AreEqual(80M, Math.Round(PfiPickupCalculator.PickupAmount, 2));
        }

        [Test]
        public void TestwithPartialPaymentBZ1494()
        {
            /**********************************************/
            /*****SETUP OF LOAN****************************/
            /*****NOT USING SAME LOAN AS OTHER TESTS*******/
            /**********************************************/
            PickupDate = new DateTime(2012, 3, 26);

            PawnLoan = new PawnLoan();
            PawnLoan.InterestAmount = 14.48M;
            PawnLoan.ServiceCharge = 4M;
            PawnLoan.DateMade = new DateTime(2011, 10, 25);
            PawnLoan.DueDate = new DateTime(2012, 3, 25);
            PfiPickupCalculator = new PfiPickupCalculator(PawnLoan, TestSiteIds.Store00901, PickupDate);
            /**********************************************/


            PawnLoan.CurrentPrincipalAmount = 289.5M;
            PawnLoan.PickupLateFinAmount = 30M; // not accurate values, but should sum correctly
            PawnLoan.PickupLateServAmount = 43.92M; // not accurate values, but should sum correctly
            AddFee(PawnLoan, FeeTypes.INTEREST, FeeStates.ASSESSED, 15M, false);
            AddFee(PawnLoan, FeeTypes.STORAGE, FeeStates.ASSESSED, 4M, false);
            AddFee(PawnLoan, FeeTypes.LATE, FeeStates.ASSESSED, 73.92M, false);

            PawnLoan.PartialPayments.Add(new PartialPayment
            {
                Date_Made = new DateTime(2011, 11, 9)
            });

            Assert.AreEqual(0, PfiPickupCalculator.ApplicableFees.Count);

            PfiPickupCalculator.Calculate();

            //WriteValues();

            Assert.IsTrue(PfiPickupCalculator.HasPartialPayment);
            Assert.AreEqual(4, PfiPickupCalculator.MonthsToPay);
            Assert.AreEqual(15, PfiPickupCalculator.DaysToPay);
            Assert.AreEqual(3, PfiPickupCalculator.ApplicableFees.Count);
            Assert.AreEqual(372.66M, Math.Round(PfiPickupCalculator.PickupAmount, 2));
        }

        [Test]
        public void TestTicket115416_1()
        {
            /**********************************************/
            /*****SETUP OF LOAN****************************/
            /*****NOT USING SAME LOAN AS OTHER TESTS*******/
            /**********************************************/
            PickupDate = new DateTime(2012, 1, 20);

            PawnLoan = new PawnLoan();
            PawnLoan.InterestAmount = 4M;
            PawnLoan.ServiceCharge = 4M;
            PawnLoan.DateMade = new DateTime(2011, 12, 31);
            PawnLoan.DueDate = new DateTime(2012, 1, 31);
            PfiPickupCalculator = new PfiPickupCalculator(PawnLoan, TestSiteIds.Store00901, PickupDate);
            /**********************************************/


            PawnLoan.CurrentPrincipalAmount = 80M;
            AddFee(PawnLoan, FeeTypes.INTEREST, FeeStates.ASSESSED, 4M, false);
            AddFee(PawnLoan, FeeTypes.STORAGE, FeeStates.ASSESSED, 4M, false);
            //AddFee(PawnLoan, FeeTypes.LATE, FeeStates.ASSESSED, 73.92M, false);

            PawnLoan.PartialPayments.Add(new PartialPayment
            {
                Date_Made = new DateTime(2012, 1, 10)
            });

            Assert.AreEqual(0, PfiPickupCalculator.ApplicableFees.Count);

            PfiPickupCalculator.Calculate();

            //WriteValues();

            Assert.IsTrue(PfiPickupCalculator.HasPartialPayment);
            Assert.AreEqual(0, PfiPickupCalculator.MonthsToPay);
            Assert.AreEqual(20, PfiPickupCalculator.DaysToPay);
            Assert.AreEqual(2, PfiPickupCalculator.ApplicableFees.Count);
            Assert.AreEqual(85.34M, Math.Round(PfiPickupCalculator.PickupAmount, 2));
        }

        [Test]
        public void TestTicket115416_2()
        {
            /**********************************************/
            /*****SETUP OF LOAN****************************/
            /*****NOT USING SAME LOAN AS OTHER TESTS*******/
            /**********************************************/
            PickupDate = new DateTime(2012, 1, 31);

            PawnLoan = new PawnLoan();
            PawnLoan.InterestAmount = 4M;
            PawnLoan.ServiceCharge = 4M;
            PawnLoan.DateMade = new DateTime(2011, 12, 31);
            PawnLoan.DueDate = new DateTime(2012, 1, 31);
            PfiPickupCalculator = new PfiPickupCalculator(PawnLoan, TestSiteIds.Store00901, PickupDate);
            /**********************************************/


            PawnLoan.CurrentPrincipalAmount = 80M;
            AddFee(PawnLoan, FeeTypes.INTEREST, FeeStates.ASSESSED, 4M, false);
            AddFee(PawnLoan, FeeTypes.STORAGE, FeeStates.ASSESSED, 4M, false);
            //AddFee(PawnLoan, FeeTypes.LATE, FeeStates.ASSESSED, 73.92M, false);

            PawnLoan.PartialPayments.Add(new PartialPayment
            {
                Date_Made = new DateTime(2012, 1, 10)
            });

            Assert.AreEqual(0, PfiPickupCalculator.ApplicableFees.Count);

            PfiPickupCalculator.Calculate();

            //WriteValues();

            Assert.IsTrue(PfiPickupCalculator.HasPartialPayment);
            Assert.AreEqual(0, PfiPickupCalculator.MonthsToPay);
            Assert.AreEqual(20, PfiPickupCalculator.DaysToPay);
            Assert.AreEqual(2, PfiPickupCalculator.ApplicableFees.Count);
            Assert.AreEqual(85.34M, Math.Round(PfiPickupCalculator.PickupAmount, 2));
        }

        [Test]
        public void TestTicket115417_1()
        {
            /**********************************************/
            /*****SETUP OF LOAN****************************/
            /*****NOT USING SAME LOAN AS OTHER TESTS*******/
            /**********************************************/
            PickupDate = new DateTime(2012, 2, 29);

            PawnLoan = new PawnLoan();
            PawnLoan.InterestAmount = 4M;
            PawnLoan.ServiceCharge = 4M;
            PawnLoan.DateMade = new DateTime(2011, 12, 31);
            PawnLoan.DueDate = new DateTime(2012, 1, 31);
            PfiPickupCalculator = new PfiPickupCalculator(PawnLoan, TestSiteIds.Store00901, PickupDate);
            /**********************************************/


            PawnLoan.CurrentPrincipalAmount = 80M;
            PawnLoan.PickupLateFinAmount = 4M;
            PawnLoan.PickupLateServAmount = 4M;
            AddFee(PawnLoan, FeeTypes.INTEREST, FeeStates.ASSESSED, 4M, false);
            AddFee(PawnLoan, FeeTypes.STORAGE, FeeStates.ASSESSED, 4M, false);
            AddFee(PawnLoan, FeeTypes.LATE, FeeStates.ASSESSED, 0M, false);

            PawnLoan.PartialPayments.Add(new PartialPayment
            {
                Date_Made = new DateTime(2012, 2, 14)
            });

            Assert.AreEqual(0, PfiPickupCalculator.ApplicableFees.Count);

            PfiPickupCalculator.Calculate();

            //WriteValues();

            Assert.IsTrue(PfiPickupCalculator.HasPartialPayment);
            Assert.AreEqual(0, PfiPickupCalculator.MonthsAlreadyPaid);
            Assert.AreEqual(14, PfiPickupCalculator.DaysAlreadyPaid);
            Assert.AreEqual(1, PfiPickupCalculator.ApplicableFees.Count);
            Assert.AreEqual(88M, Math.Round(PfiPickupCalculator.PickupAmount, 2));
        }

        [Test]
        public void TestTicket115417_2()
        {
            /**********************************************/
            /*****SETUP OF LOAN****************************/
            /*****NOT USING SAME LOAN AS OTHER TESTS*******/
            /**********************************************/
            PickupDate = new DateTime(2012, 3, 29);

            PawnLoan = new PawnLoan();
            PawnLoan.InterestAmount = 4M;
            PawnLoan.ServiceCharge = 4M;
            PawnLoan.DateMade = new DateTime(2011, 12, 31);
            PawnLoan.DueDate = new DateTime(2012, 1, 31);
            PfiPickupCalculator = new PfiPickupCalculator(PawnLoan, TestSiteIds.Store00901, PickupDate);
            /**********************************************/


            PawnLoan.CurrentPrincipalAmount = 80M;
            PawnLoan.PickupLateFinAmount = 4M;
            PawnLoan.PickupLateServAmount = 4M;
            AddFee(PawnLoan, FeeTypes.INTEREST, FeeStates.ASSESSED, 4M, false);
            AddFee(PawnLoan, FeeTypes.STORAGE, FeeStates.ASSESSED, 4M, false);
            AddFee(PawnLoan, FeeTypes.LATE, FeeStates.ASSESSED, 0M, false);

            PawnLoan.PartialPayments.Add(new PartialPayment
            {
                Date_Made = new DateTime(2012, 2, 14)
            });

            Assert.AreEqual(0, PfiPickupCalculator.ApplicableFees.Count);

            PfiPickupCalculator.Calculate();

            //WriteValues();

            Assert.IsTrue(PfiPickupCalculator.HasPartialPayment);
            Assert.AreEqual(0, PfiPickupCalculator.MonthsAlreadyPaid);
            Assert.AreEqual(14, PfiPickupCalculator.DaysAlreadyPaid);
            Assert.AreEqual(1, PfiPickupCalculator.ApplicableFees.Count);
            Assert.AreEqual(88M, Math.Round(PfiPickupCalculator.PickupAmount, 2));
        }

        [Test]
        public void TestGetDaysAlreadyPaid_1()
        {
            PickupDate = new DateTime(2012, 3, 31);

            PawnLoan = new PawnLoan
                       {
                           DateMade = new DateTime(2011, 12, 31),
                           DueDate = new DateTime(2012, 1, 31)
                       };
            PfiPickupCalculator = new PfiPickupCalculator(PawnLoan, TestSiteIds.Store00901, PickupDate);

            PawnLoan.PartialPayments.Add(new PartialPayment
            {
                Date_Made = new DateTime(2012, 2, 14)
            });

            PfiPickupCalculator.DeterminePartialPaymentInfo();
            Assert.AreEqual(14, PfiPickupCalculator.GetDaysAlreadyPaid());
            Assert.AreEqual(0, PfiPickupCalculator.GetMonthsAlreadyPaid());
        }

        [Test]
        public void TestGetDaysAlreadyPaid_2()
        {
            PickupDate = new DateTime(2012, 3, 31);

            PawnLoan = new PawnLoan
            {
                DateMade = new DateTime(2011, 12, 31),
                DueDate = new DateTime(2012, 1, 31)
            };
            PfiPickupCalculator = new PfiPickupCalculator(PawnLoan, TestSiteIds.Store00901, PickupDate);

            PawnLoan.PartialPayments.Add(new PartialPayment
            {
                Date_Made = new DateTime(2012, 4, 10)
            });

            PfiPickupCalculator.DeterminePartialPaymentInfo();
            Assert.AreEqual(10, PfiPickupCalculator.GetDaysAlreadyPaid());
            Assert.AreEqual(2, PfiPickupCalculator.GetMonthsAlreadyPaid());
        }

        [Test]
        public void TestGetDaysAlreadyPaid_3()
        {
            PickupDate = new DateTime(2012, 3, 31);

            PawnLoan = new PawnLoan
            {
                DateMade = new DateTime(2011, 12, 31),
                DueDate = new DateTime(2012, 1, 31)
            };
            PfiPickupCalculator = new PfiPickupCalculator(PawnLoan, TestSiteIds.Store00901, PickupDate);

            PawnLoan.PartialPayments.Add(new PartialPayment
            {
                Date_Made = new DateTime(2012, 3, 31)
            });

            PfiPickupCalculator.DeterminePartialPaymentInfo();
            Assert.AreEqual(0, PfiPickupCalculator.GetDaysAlreadyPaid());
            Assert.AreEqual(2, PfiPickupCalculator.GetMonthsAlreadyPaid());
        }

        private void WriteValues()
        {
            Console.WriteLine("{0}:{1:C}", "Current Principal Amount", PfiPickupCalculator.PawnLoan.CurrentPrincipalAmount);
            Console.WriteLine("{0}:{1}", "Date Made", PfiPickupCalculator.PawnLoan.DateMade.ToShortDateString());
            Console.WriteLine("{0}:{1}", "Has Partial Payment", PfiPickupCalculator.HasPartialPayment);
            if (PfiPickupCalculator.HasPartialPayment)
            {
                Console.WriteLine("{0}:{1}", "Last Partial Payment Date", PfiPickupCalculator.LastPartialPayment.Date_Made.ToShortDateString());
            }
            Console.WriteLine("{0}:{1}", "Months To Pay", PfiPickupCalculator.MonthsToPay);
            Console.WriteLine("{0}:{1}", "Days To Pay", PfiPickupCalculator.DaysToPay);
            Console.WriteLine("{0}:{1:C}", "Pickup Amount", PfiPickupCalculator.PickupAmount);

            foreach (var fee in PfiPickupCalculator.ApplicableFees)
            {
                Console.WriteLine("{0}:{1:C}", fee.FeeType, fee.Value);
            }

            Console.WriteLine();
        }

        private void AddFee(PawnLoan loan, FeeTypes feeType, FeeStates state, decimal value, bool waived)
        {
            loan.OriginalFees.Add(new Fee
            {
                FeeType = feeType,
                CanBeProrated = false,
                CanBeWaived = false,
                FeeDate = DateTime.MinValue,
                FeeRef = 0,
                FeeRefType = FeeRefTypes.PAWN,
                FeeState = state,
                OriginalAmount = value,
                Prorated = false,
                Tag = string.Empty,
                Value = value,
                Waived = waived
            });
        }
    }
}
