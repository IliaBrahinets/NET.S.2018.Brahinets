using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FilterDigit.MSUnitTests
{
    [TestClass]
    public class IListExtensions
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "Data.xml", "Row", DataAccessMethod.Sequential)]
        public void TestMethod1()
        {
            Assert.AreEqual((int)TestContext.DataRow["Name"], 1);
        }
    }
}
