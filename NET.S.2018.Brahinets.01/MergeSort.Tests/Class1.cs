using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MergeSort;
using System.Diagnostics;

namespace MergeSort.Tests
{
    [TestClass]
    public class IListExtensionsTests
    {
        [TestMethod]
        public void MergeSort_RandomUnorderedIn_OrderedOut()
        {
            List<int> arrayIn = new List<int>();
            Random randomGen = new Random(DateTime.Now.GetHashCode());
            for (int i = 0; i < TestSettings.Count; i++)
            {
                arrayIn.Add(randomGen.Next() % TestSettings.MaxRandomGeneratedValue);
            }
            List<int> arrayInCopy = new List<int>(arrayIn);

            arrayIn.MergeSort();
            arrayInCopy.Sort();

            CollectionAssert.AreEqual(arrayIn, arrayInCopy);

        }

        [TestMethod]
        public void MergeSort_RandomUnorderedWithReverseComparerIn_ReverseOrderedOut()
        {
            List<int> arrayIn = new List<int>();
            Random randomGen = new Random(DateTime.Now.GetHashCode());
            for (int i = 0; i < TestSettings.Count; i++)
            {
                arrayIn.Add(randomGen.Next() % TestSettings.MaxRandomGeneratedValue);
            }
            List<int> arrayInCopy = new List<int>(arrayIn);

            arrayIn.MergeSort((x, y) => y - x);
            arrayInCopy.Sort((x, y) => y - x);

            CollectionAssert.AreEqual(arrayIn, arrayInCopy);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MergeSort_ArrayNull_ArgumentNullException()
        {
            List<int> arrayIn = null;

            arrayIn.MergeSort();
        }

        [TestMethod]
        public void MergeSort_EmptyArray_DontBreakDown()
        {
            List<int> arrayIn = new List<int>();
            bool breakDown = false;

            try
            {
                arrayIn.MergeSort();
            }
            catch (Exception)
            {
                breakDown = true;
            }

            Assert.IsFalse(breakDown);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MergeSort_InvalidLeftSortingBounds_ArgumentException()
        {
            List<int> arrayIn = new List<int> { 1, 2, 3 };

            arrayIn.MergeSort(-1, 2, null);
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MergeSort_InvalidRightSortingBounds_ArgumentException()
        {
            List<int> arrayIn = new List<int> { 1, 2, 3 };

            arrayIn.MergeSort(0, -1, null);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MergeSort_InvalidBothSortingBounds_ArgumentException()
        {
            List<int> arrayIn = new List<int> { 1, 2, 3 };

            arrayIn.MergeSort(-1, 5, null);

        }

        private static class TestSettings
        {
            public static readonly int Count = 10;
            public static readonly int MaxRandomGeneratedValue = 10000;
        }
    }
}
