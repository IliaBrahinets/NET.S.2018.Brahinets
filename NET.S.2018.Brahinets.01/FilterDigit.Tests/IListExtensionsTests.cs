using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FilterDigit.NUnitTests
{
    [TestFixture]
    public class IListExtensionsTests
    {
        public static IEnumerable<TestCaseData> TestValidData
        {
            get
            {
                yield return new TestCaseData(new List<int> { 123, 23 }, 1).Returns(new List<int> { 123 }).SetName("( {123,23} , 1 )-> {123} ");
                yield return new TestCaseData(new List<int> { 13, 23 }, 2).Returns(new List<int> { 23 }).SetName("( {13,23} , 2 )-> {23} ");
                yield return new TestCaseData(new List<int> { -782, 123 }, 7).Returns(new List<int> { -782 }).SetName("( {-782,123} , 7 )-> {-782} ");
            }
        }

        [Test, TestCaseSource(nameof(TestValidData))]
        public List<int> FilterDigitMethod(List<int> arr, int number)
        {
            return (List<int>)arr.FilterDigit(number);
        }

        [Test]
        public void FilterDigitMethod_NullArray_ArgumentNullException()
        {
            List<int> arr = null;
            int number = 1;

            Assert.Throws<ArgumentNullException>(() => arr.FilterDigit(number));
        }

        [Test]
        public void FilterDigitMethod_NumberDontMatch_ArgumentException()
        {
            List<int> arr = new List<int> { 1, 2, 3 };
            int number = 20;

            Assert.Throws<ArgumentException>(() => arr.FilterDigit(number));
        }




    }
}
