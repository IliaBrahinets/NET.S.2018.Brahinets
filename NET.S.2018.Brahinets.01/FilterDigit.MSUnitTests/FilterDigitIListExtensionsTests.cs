using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FilterDigit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace FilterDigit.MSUnitTests
{
    [TestClass]
    public class FilterDigitIListExtensionsTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DeploymentItem("Objectivity.Test.Automation.MsTests\\Data.xml"),
        DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "|DataDirectory|\\Data.xml", "TestCase",
         DataAccessMethod.Sequential)]
        public void FilterDigitMethod_DDTTest()
        {
            IEnumerable<int> arrayIn = JSONArrayStringToIEnumerable<int>((string)TestContext.DataRow["Array"]);
            int number = int.Parse((string)TestContext.DataRow["Number"]);

            if (TestContext.DataRow["ExpectedResult"] != System.DBNull.Value)
            {
                IEnumerable<int> expectedResult = JSONArrayStringToIEnumerable<int>((string)TestContext.DataRow["ExpectedResult"]);

                IEnumerable<int> actual = arrayIn.FilterDigit(number);

                Assert.IsTrue(IsEnumerableEqual(expectedResult, actual));
            }
            else
            {
                Type expectedException = Type.GetType((string)TestContext.DataRow["ExpectedException"]);

                bool wasThrown = false;

                try
                {
                    arrayIn.FilterDigit(number);
                }
                catch (Exception e)
                {
                    if (e.GetType().Equals(expectedException))
                    {
                        wasThrown = true;
                    }
                    else
                    {
                        throw e;
                    }
                }

                Assert.IsTrue(wasThrown);
            }
        }

        private IEnumerable<T> JSONArrayStringToIEnumerable<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return null;
            }

            JArray jsonArray = JArray.Parse(jsonString);

            IEnumerable<T> array = jsonArray.Select(el => (T)Convert.ChangeType(el, typeof(T)));

            return array;
        }

        private bool IsEnumerableEqual<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;

            using (var firstIt = first.GetEnumerator())
            using (var secondIt = second.GetEnumerator())
            {

                bool isNextExist1;
                bool isNextExist2;

                do
                {
                    isNextExist1 = firstIt.MoveNext();
                    isNextExist2 = secondIt.MoveNext();

                    if (isNextExist1 != isNextExist2)
                    {
                        return false;
                    }

                    if (isNextExist1)
                    {
                        bool isEqual = equalityComparer.Equals(firstIt.Current, secondIt.Current);

                        if (!isEqual)
                        {
                            return false;
                        }
                    }
                }
                while (isNextExist1 && isNextExist2);
            }

            return true;
        }
    }
}
