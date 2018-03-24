using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Converters
{
    [TestFixture]
    public class StringExtensionTests
    {
        [TestCase("0110111101100001100001010111111", 2, ExpectedResult = 934331071)]
        [TestCase("01101111011001100001010111111", 2, ExpectedResult = 233620159)]
        [TestCase("11101101111011001100001010", 2, ExpectedResult = 62370570)]
        [TestCase("1AeF101", 16, ExpectedResult = 28242177)]
        [TestCase("764241", 8, ExpectedResult = 256161)]
        [TestCase("10", 5, ExpectedResult = 5)]
        public int ToDecimalConverteMethod(string number, int @base)
        {
            return number.ToDecmialConverter(new Notation(@base));
        }

        [Test]
        public void ToDecimalConverteMethod_OverflowNumber_OverflowException()
        {
            string number = "11111111111111111111111111111111111111111111111111111111111111";
            Notation notation = new Notation(2);

            Assert.Throws<OverflowException>(() => number.ToDecmialConverter(notation));
        }

        [Test]
        public void ToDecimalConverterMethod_UncorrecrtSymbForGivenNotation_ArgumentException()
        {
            string number = "1AeF101";
            Notation notation = new Notation(2);

            Assert.Throws<ArgumentException>(() => number.ToDecmialConverter(notation));
        }



    }
}
