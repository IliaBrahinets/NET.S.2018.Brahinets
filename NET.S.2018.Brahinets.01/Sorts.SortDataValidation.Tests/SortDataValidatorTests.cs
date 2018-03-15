using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sorts.SortDataValidation;

namespace Sorts.SortDataValidation.Tests
{
    [TestClass]
    public class SortDataValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateMethod_ArrayNull_ArgumentNullException()
        {
            List<int> arrayIn = null;

            SortDataValidator.Validate(arrayIn, null, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateMethod_InvalidLeftSortingBounds_ArgumentException()
        {
            List<int> arrayIn = new List<int> { 1, 2, 3 };

            SortDataValidator.Validate(arrayIn, -1, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateMethod_InvalidRightSortingBounds_ArgumentException()
        {
            List<int> arrayIn = new List<int> { 1, 2, 3 };

            SortDataValidator.Validate(arrayIn, null, -1, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateMethod_InvalidBothSortingBounds_ArgumentException()
        {
            List<int> arrayIn = new List<int> { 1, 2, 3 };

            SortDataValidator.Validate(arrayIn, -1, 5, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValidateMethod_TypeHaveNoCompareTo_InvalidOperationException()
        {
            List<object> arrayIn = new List<object>
            {
                new object { }, new object { }, new object { }
            };

            SortDataValidator.Validate(arrayIn, null, null, null);
        }
    }
}
