using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Common.Controllers.Application;

namespace CommonTests
{
    [TestFixture]
    public class ShopDateTimeTests
    {
        private ShopDateTime ShopDateTime { get; set; }
        private DateTime CurrentTime { get; set; }

        [SetUp]
        public  void Setup()
        {
            CurrentTime = new DateTime(2012, 03, 29, 10, 24, 0, 0);
            ShopDateTime = new ShopDateTime();

            TimeSpan difference = ShopDateTime.FullShopDateTime - CurrentTime;
            ShopDateTime.setOffsets(0, 0, 0, 0, 0, (int)-difference.TotalSeconds, 0);
        }

        [Test]
        public void Test()
        {
            
        }

        [TearDown]
        public void Teardown()
        {

        }
    }
}
