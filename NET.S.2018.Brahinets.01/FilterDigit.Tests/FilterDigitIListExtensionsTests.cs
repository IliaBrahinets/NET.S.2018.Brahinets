using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FilterDigit.NUnitTests
{
    [TestFixture]
    public class FilterDigitIListExtensionsTests
    {
        public static IEnumerable<TestCaseData> TestValidData
        {
            get
            {
                yield return new TestCaseData(new [] { 123, 23 }, 1).Returns(new [] { 123 }).SetName("( {123,23} , 1 )-> {123} ");
                yield return new TestCaseData(new [] { 13, 23 }, 2).Returns(new [] { 23 }).SetName("( {13,23} , 2 )-> {23} ");
                yield return new TestCaseData(new [] { -782, 123 }, 7).Returns(new [] { -782 }).SetName("( {-782,123} , 7 )-> {-782} ");
            }
        }

        [Test, TestCaseSource(nameof(TestValidData))]
        public IEnumerable<int> FilterDigitMethod(IEnumerable<int> arr, int number)
        {
            return arr.FilterDigit(number);
        }

        [Test]
        public void FilterDigitMethod_NullArray_ArgumentNullException()
        {
            int[] arr = null;
            int digit = 2;

            Assert.Throws<ArgumentNullException>(() => arr.FilterDigit(digit));
        }

        [Test]
        public void FilterDigitMethod_DigitDontMatch_ArgumentException()
        {
            var arr = new [] { 1, 2, 3 };
            int digit = 20;

            Assert.Throws<ArgumentException>(() => arr.FilterDigit(digit));
        }
    }
}
