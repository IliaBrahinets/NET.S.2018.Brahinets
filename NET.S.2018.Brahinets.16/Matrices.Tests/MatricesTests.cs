using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Matrices.Tests
{
    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void DefaultConstructorAndGetIndexerTest()
        {
            var actualMatrix = new Matrix<int>(2, 2);

            int expected = default(int);

            for(int i = 0; i < actualMatrix.N; i++)
            {
                for(int j = 0; j < actualMatrix.M; j++)
                {
                    if (!actualMatrix[i, j].Equals(expected))
                    {
                        Assert.Fail();
                    }
                }
            }

            Assert.Pass();
        }

        [Test]
        public void ArrayConstructorTest()
        {
            var expected = new int[][]{
                new[]{1,2},
                new[]{6,7}
            };

            var actualMatrix = new Matrix<int>(2, 2, expected);

            for (int i = 0; i < actualMatrix.N; i++)
            {
                for (int j = 0; j < actualMatrix.M; j++)
                {
                    if (!actualMatrix[i, j].Equals(expected[i][j]))
                    {
                        Assert.Fail();
                    }
                }
            }

            Assert.Pass();
        }

        [Test]
        public void TupleConstructorTest()
        {
            var expected = new int[][]{
                new[]{1,2},
                new[]{6,7}
            };

            var actualMatrix = new Matrix<int>(2, 2, new Tuple<int, int, int>[]{
                new Tuple<int, int, int>(0,0,1),
                new Tuple<int, int, int>(0,1,2),
                new Tuple<int, int, int>(1,0,6),
                new Tuple<int, int, int>(1,1,7)
            });

            for (int i = 0; i < actualMatrix.N; i++)
            {
                for (int j = 0; j < actualMatrix.M; j++)
                {
                    if (!actualMatrix[i, j].Equals(expected[i][j]))
                    {
                        Assert.Fail();
                    }
                }
            }

            Assert.Pass();
        }

        [Test]
        public void SetIndexerTest()
        {
            var matrix = new Matrix<int>(2, 2, new int[][]{
                new[]{1,2},
                new[]{6,7}
            });

            int i = 1;
            int j = 1;
            int value = matrix[i, j];

            int expected = value + 1;
            matrix[i, j] = expected;
            int actual = matrix[i, j];

            Assert.True(actual == expected);
        }

        [Test]
        public void ValueChangedEventTests()
        {
            var matrix = new Matrix<int>(2, 2, new int[][]{
                new[]{1,2},
                new[]{6,7}
            });

            int i = 1;
            int j = 1;

            matrix.ValueChanged += (sender, args) => Assert.True((args.i == i) && (args.j == j));

            matrix[1, 1] = matrix[1, 1] + 1;
        }

        [Test]
        public void NotValidSizes_ArgumentOutOfRangeException() 
            => Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix<int>(-1, -1));

        [Test]
        public void ArrayIsNull_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => new Matrix<int>(1, 1, (int[][])null));

        [Test]
        public void CollecionOfTuplesIsNull_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => new Matrix<int>(1, 1, (IEnumerable<Tuple<int,int,int>>)null));
    }
}
