using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SpecialBitInsertion.NUnitTests
{
    [TestFixture]
    public class IntExtensionTests
    {
        public static IEnumerable<TestCaseData> TestData
        {
            get
            {
                int expectedResult = 9;
                object[] args = { 8, 15, 0, 0 };
                yield return new TestCaseData(args).Returns(expectedResult).SetName(GenerateTestName(expectedResult, args));

                expectedResult = -7;
                args = new object[] { -8, 15, 0, 0 };
                yield return new TestCaseData(args).Returns(expectedResult).SetName(GenerateTestName(expectedResult, args));

                expectedResult = 15;
                args = new object[] { 15, 15, 0, 0 };
                yield return new TestCaseData(args).Returns(expectedResult).SetName(GenerateTestName(expectedResult, args));

                expectedResult = 120;
                args = new object[] { 8, 15, 3, 8 };
                yield return new TestCaseData(args).Returns(expectedResult).SetName(GenerateTestName(expectedResult, args));

                expectedResult = -8;
                args = new object[] { -8, -8, 0, 0 };
                yield return new TestCaseData(args).Returns(expectedResult).SetName(GenerateTestName(expectedResult, args));

                expectedResult = 0;
                args = new object[] { 0, 0, 0, 0 };
                yield return new TestCaseData(args).Returns(expectedResult).SetName(GenerateTestName(expectedResult, args));
            }
        }

        [TestCaseSource(nameof(TestData))]
        public int SpecialBitInsertionMethod(int numberSource, int numberIn, int i, int j)
        {
            return Int32Extension.SpecialBitInsertionV2(numberSource, numberIn, i, j);
        }

        [Test]
        public void SpecialBitInsertionMethod_InvalidNumbersOfBitsLessthanZero_ArgumentNullException()
        {
            int numberSource = 7;
            int numberIn = 7;
            int i = -1;
            int j = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => Int32Extension.SpecialBitInsertion(numberSource, numberIn, i, j));
        }

        [Test]
        public void SpecialBitInsertionMethod_InvalidNumbersOfBits_iMoreThanj_ArgumentException()
        {
            int numberSource = 7;
            int numberIn = 7;
            int i = 7;
            int j = 5;

            Assert.Throws<ArgumentException>(() => Int32Extension.SpecialBitInsertion(numberSource, numberIn, i, j));
        }

        private static string GenerateTestName(object expectedResult, params object[] args)
        {
            string name = string.Empty;

            name += "In ";

            for (int i = 0; i < args.Length; i++)
            {
                name += $"{ args[i].ToString() } ";
            }

            name += " | ";

            name += $"Out { expectedResult.ToString() }";

            return name;
        }
    }
}
