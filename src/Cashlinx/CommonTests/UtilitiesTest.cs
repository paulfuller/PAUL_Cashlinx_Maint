using System;
using System.Linq;
using Common.Libraries.Utility;
using NUnit.Framework;

namespace CommonTests
{
    [TestFixture]
    public class UtilitiesTest
    {
        [Test]
        public void CanCurrencyValueDistibuteOverItemsWithZeroValue()
        {
            Assert.IsTrue(Utilities.CanCurrencyValueDistibuteOverItems(0M, 3));
        }

        [Test]
        public void CanCurrencyValueDistibuteOverItemsWithValueLessThanItems()
        {
            Assert.IsFalse(Utilities.CanCurrencyValueDistibuteOverItems(.01M, 3));
        }

        [Test]
        public void CanCurrencyValueDistibuteOverItemsWithValueMoreThanItems()
        {
            Assert.IsTrue(Utilities.CanCurrencyValueDistibuteOverItems(.04M, 3));
        }

        [Test]
        public void CanCurrencyValueDistibuteOverItemsWithNegativeValueLessThanItems()
        {
            Assert.IsFalse(Utilities.CanCurrencyValueDistibuteOverItems(-.01M, 3));
        }

        [Test]
        public void CanCurrencyValueDistibuteOverItemsWithNegativeValueMoreThanItems()
        {
            Assert.IsTrue(Utilities.CanCurrencyValueDistibuteOverItems(-.04M, 3));
        }

        [Test]
        public void GetDistributeValuesForCurrencyOverItemsWithZeroValue()
        {
            int numberOfItems = 3;
            decimal currencyValue = 0M;

            decimal[] values = Utilities.GetDistributeValuesForCurrencyOverItems(currencyValue, numberOfItems);

            Assert.IsNotNull(values);
            Assert.AreEqual(numberOfItems, values.Length);
            Assert.AreEqual(currencyValue, values.Sum());

            foreach (decimal value in values)
            {
                Assert.AreEqual(0M, value);
            }
        }

        [Test]
        public void GetDistributeValuesForCurrencyOverItemsWithValueLessThanItems()
        {
            int numberOfItems = 3;
            decimal currencyValue = .01M;

            decimal[] values = Utilities.GetDistributeValuesForCurrencyOverItems(currencyValue, numberOfItems);

            Assert.IsNotNull(values);
            Assert.AreEqual(numberOfItems, values.Length);
            Assert.AreEqual(currencyValue, values.Sum());

            Assert.AreEqual(.01M, values[0]);
            Assert.AreEqual(0M, values[1]);
            Assert.AreEqual(0M, values[2]);
        }

        [Test]
        public void GetDistributeValuesForCurrencyOverItemsWithValue1MoreThanItems()
        {
            int numberOfItems = 3;
            decimal currencyValue = .04M;

            decimal[] values = Utilities.GetDistributeValuesForCurrencyOverItems(currencyValue, numberOfItems);

            Assert.IsNotNull(values);
            Assert.AreEqual(numberOfItems, values.Length);
            Assert.AreEqual(currencyValue, values.Sum());

            Assert.AreEqual(.02M, values[0]);
            Assert.AreEqual(.01M, values[1]);
            Assert.AreEqual(.01M, values[2]);
        }

        [Test]
        public void GetDistributeValuesForCurrencyOverItemsWithValue2MoreThanItems()
        {
            int numberOfItems = 3;
            decimal currencyValue = .05M;

            decimal[] values = Utilities.GetDistributeValuesForCurrencyOverItems(currencyValue, numberOfItems);

            Assert.IsNotNull(values);
            Assert.AreEqual(numberOfItems, values.Length);
            Assert.AreEqual(currencyValue, values.Sum());

            Assert.AreEqual(.02M, values[0]);
            Assert.AreEqual(.02M, values[1]);
            Assert.AreEqual(.01M, values[2]);
        }

        [Test]
        public void GetDistributeValuesForCurrencyOverItemsWithNegativeValueLessThanItems()
        {
            int numberOfItems = 3;
            decimal currencyValue = -.01M;

            decimal[] values = Utilities.GetDistributeValuesForCurrencyOverItems(currencyValue, numberOfItems);

            Assert.IsNotNull(values);
            Assert.AreEqual(numberOfItems, values.Length);
            Assert.AreEqual(currencyValue, values.Sum());

            Assert.AreEqual(-.01M, values[0]);
            Assert.AreEqual(0M, values[1]);
            Assert.AreEqual(0M, values[2]);
        }

        [Test]
        public void GetDistributeValuesForCurrencyOverItemsWithNegativeValue1MoreThanItems()
        {
            int numberOfItems = 3;
            decimal currencyValue = -.04M;

            decimal[] values = Utilities.GetDistributeValuesForCurrencyOverItems(currencyValue, numberOfItems);

            Assert.IsNotNull(values);
            Assert.AreEqual(numberOfItems, values.Length);
            Assert.AreEqual(currencyValue, values.Sum());

            Assert.AreEqual(-.02M, values[0]);
            Assert.AreEqual(-.01M, values[1]);
            Assert.AreEqual(-.01M, values[2]);
        }

        [Test]
        public void GetDistributeValuesForCurrencyOverItemsWithNegativeValue2MoreThanItems()
        {
            int numberOfItems = 3;
            decimal currencyValue = -.05M;

            decimal[] values = Utilities.GetDistributeValuesForCurrencyOverItems(currencyValue, numberOfItems);

            Assert.IsNotNull(values);
            Assert.AreEqual(numberOfItems, values.Length);
            Assert.AreEqual(currencyValue, values.Sum());

            Assert.AreEqual(-.02M, values[0]);
            Assert.AreEqual(-.02M, values[1]);
            Assert.AreEqual(-.01M, values[2]);
        }

        [Test]
        public void GetBooleanValueWithValidValue()
        {
            Assert.IsTrue(Utilities.GetBooleanValue("true"));
        }

        [Test]
        public void GetBooleanValueWithInvalidValue()
        {
            Assert.IsFalse(Utilities.GetBooleanValue("aaa"));
        }

        [Test]
        public void GetBooleanValueWithInvalidValueAndDefaultValue()
        {
            Assert.IsTrue(Utilities.GetBooleanValue("aaa", true));
        }

        [Test]
        public void GetCharValueWithValidValue()
        {
            object value = 'A';
            Assert.AreEqual('A', Utilities.GetCharValue(value));
        }

        [Test]
        public void GetCharValueWithInvalidValue()
        {
            object value = null;
            Assert.AreEqual(0, Utilities.GetCharValue(value));
        }

        [Test]
        public void GetCharValueWithValidValueAndDefaultValue()
        {
            object value = null;
            Assert.AreEqual('A', Utilities.GetCharValue(value, 'A'));
        }

        [Test]
        public void GetDateTimeValueWithValidValue()
        {
            object value = "01/01/2010";
            Assert.AreEqual(DateTime.Parse("01/01/2010"), Utilities.GetDateTimeValue(value));
        }

        [Test]
        public void GetDateTimeValueWithInvalidValue()
        {
            object value = null;
            Assert.AreEqual(DateTime.MinValue, Utilities.GetDateTimeValue(value));
        }

        [Test]
        public void GetDateTimeValueWithValidValueAndDefaultValue()
        {
            object value = null;
            Assert.AreEqual(DateTime.Parse("01/01/2010"), Utilities.GetDateTimeValue(value, DateTime.Parse("01/01/2010")));
        }

        [Test]
        public void GetDecimalValueWithValidValue()
        {
            object value = 12.34;
            Assert.AreEqual(12.34M, Utilities.GetDecimalValue(value));
        }

        [Test]
        public void GetDecimalValueWithInvalidValue()
        {
            object value = null;
            Assert.AreEqual(0M, Utilities.GetDecimalValue(value));
        }

        [Test]
        public void GetDecimalValueWithValidValueAndDefaultValue()
        {
            object value = null;
            Assert.AreEqual(12.34M, Utilities.GetDecimalValue(value, 12.34M));
        }

        [Test]
        public void GetDoubleValueWithValidValue()
        {
            object value = 12.34;
            Assert.AreEqual(12.34, Utilities.GetDoubleValue(value));
        }

        [Test]
        public void GetDoubleValueWithInvalidValue()
        {
            object value = null;
            Assert.AreEqual(double.MinValue, Utilities.GetDoubleValue(value));
        }

        [Test]
        public void GetDoubleValueWithValidValueAndDefaultValue()
        {
            object value = null;
            Assert.AreEqual(12.34, Utilities.GetDoubleValue(value, 12.34));
        }

        [Test]
        public void GetFloatValueWithValidValue()
        {
            object value = 12.34f;
            Assert.AreEqual(12.34f, Utilities.GetFloatValue(value));
        }

        [Test]
        public void GetFloatValueWithInvalidValue()
        {
            object value = null;
            Assert.AreEqual(float.MinValue, Utilities.GetFloatValue(value));
        }

        [Test]
        public void GetFloatValueWithValidValueAndDefaultValue()
        {
            object value = null;
            Assert.AreEqual(12.34f, Utilities.GetFloatValue(value, 12.34f));
        }

        [Test]
        public void GetIntegerValueWithValidValue()
        {
            object value = 12;
            Assert.AreEqual(12, Utilities.GetIntegerValue(value));
        }

        [Test]
        public void GetIntegerValueWithInvalidValue()
        {
            object value = null;
            Assert.AreEqual(int.MinValue, Utilities.GetIntegerValue(value));
        }

        [Test]
        public void GetIntegerValueWithValidValueAndDefaultValue()
        {
            object value = null;
            Assert.AreEqual(12, Utilities.GetIntegerValue(value, 12));
        }

        [Test]
        public void GetStringValueWithValidValue()
        {
            object value = "abc";
            Assert.AreEqual("abc", Utilities.GetStringValue(value));
        }

        [Test]
        public void GetStringValueWithInvalidValue()
        {
            object value = null;
            Assert.AreEqual(string.Empty, Utilities.GetStringValue(value));
        }

        [Test]
        public void GetStringValueWithValidValueAndDefaultValue()
        {
            object value = null;
            Assert.AreEqual("abc", Utilities.GetStringValue(value, "abc"));
        }

        [Test]
        public void GetLongValueWithValidValue()
        {
            object value = 12;
            Assert.AreEqual(12, Utilities.GetLongValue(value));
        }

        [Test]
        public void GetLongValueWithInvalidValue()
        {
            object value = null;
            Assert.AreEqual(long.MinValue, Utilities.GetLongValue(value));
        }

        [Test]
        public void GetLongValueWithValidValueAndDefaultValue()
        {
            object value = null;
            Assert.AreEqual(12, Utilities.GetLongValue(value, 12));
        }

        [Test]
        public void GetULongValueWithValidValue()
        {
            object value = 12;
            Assert.AreEqual(12, Utilities.GetULongValue(value));
        }

        [Test]
        public void GetULongValueWithInvalidValue()
        {
            object value = null;
            Assert.AreEqual(ulong.MinValue, Utilities.GetULongValue(value));
        }

        [Test]
        public void GetULongValueWithValidValueAndDefaultValue()
        {
            object value = null;
            Assert.AreEqual(12, Utilities.GetULongValue(value, 12));
        }
    }
}
