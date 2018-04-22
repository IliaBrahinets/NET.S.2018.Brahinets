using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Matrices.Tests
{
    [TestFixture]
    public class MatricesTests
    {
        [Test]
        public void Test()
        {
            var matrix = new SymmetricalMatrix<int>(2, new Tuple<int, int, int>[]
            {
                new Tuple<int, int, int>(0,0,1),
                new Tuple<int, int, int>(1,1,2),
                new Tuple<int, int, int>(1,0,1)
            });

            TestContext.Write(matrix.ToString());

        } 
    }
}
