using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sorts.QuickSort;

namespace Sorts.QuickSort.Tests
{
    [TestClass]
    public class IListExtenionsTests
    {
        private TestContext testContextInstance;

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        /// </summary>
        /// <value>Automatically assigned by the test framework.</value>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void QuickSort_RandomUnorderedIn_OrderedOut()
        {
            List<int> arrayIn = new List<int>();
            Random randomGen = new Random(DateTime.Now.GetHashCode());
            for (int i = 0; i < TestSettings.Count; i++)
            {
                arrayIn.Add(randomGen.Next() % TestSettings.MaxRandomGeneratedValue);
            }

            List<int> arrayInCopy = new List<int>(arrayIn);

            arrayIn.QuickSort();
            arrayInCopy.Sort();

            CollectionAssert.AreEqual(arrayIn, arrayInCopy);
        }

        [TestMethod]
        public void QuickSort_RandomUnorderedWithReverseComparerIn_ReverseOrderedOut()
        {
            List<int> arrayIn = new List<int>();
            Random randomGen = new Random(DateTime.Now.GetHashCode());
            for (int i = 0; i < TestSettings.Count; i++)
            {
                arrayIn.Add(randomGen.Next() % TestSettings.MaxRandomGeneratedValue);
            }

            List<int> arrayInCopy = new List<int>(arrayIn);

            arrayIn.QuickSort((x, y) => y - x);
            arrayInCopy.Sort((x, y) => y - x);

            CollectionAssert.AreEqual(arrayIn, arrayInCopy);
        }

        [TestMethod]
        public void QuickSort_EmptyArray_DontBreakDown()
        {
            List<int> arrayIn = new List<int>();
            bool breakDown = false;

            try
            {
                arrayIn.QuickSort();
            }
            catch (Exception e)
            {
                breakDown = true;
                TestContext.WriteLine(e.ToString());
            }

            Assert.IsFalse(breakDown);
        }

        private static class TestSettings
        {
            public static readonly int Count = 10;
            public static readonly int MaxRandomGeneratedValue = 10000;
        }
    }
}