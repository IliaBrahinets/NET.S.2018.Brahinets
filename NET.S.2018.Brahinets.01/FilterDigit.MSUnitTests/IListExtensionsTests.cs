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
    public class IListExtensionsTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DeploymentItem("Objectivity.Test.Automation.MsTests\\Data.xml"),
        DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "|DataDirectory|\\Data.xml", "TestCase",
         DataAccessMethod.Sequential)]
        public void FilterDigitMethod_DDTTest()
        {
            List<int> arrayIn = JArrayStringToList<int>((string)TestContext.DataRow["Array"]);
            int number = int.Parse((string)TestContext.DataRow["Number"]);

            if (TestContext.DataRow["ExpectedResult"] != System.DBNull.Value)
            {
                List<int> expectedResult = JArrayStringToList<int>((string)TestContext.DataRow["ExpectedResult"]);

                List<int> actualArray = (List<int>)arrayIn.FilterDigit(number);

                CollectionAssert.AreEqual(expectedResult, actualArray);
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
                }

                Assert.IsTrue(wasThrown);
            }
        }

        private List<T> JArrayStringToList<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return null;
            }

            JArray jsonArray = JArray.Parse(jsonString);

            List<T> array = jsonArray.Select(el => (T)Convert.ChangeType(el, typeof(T)))
                                     .ToList();

            return array;
        }
    }
}
