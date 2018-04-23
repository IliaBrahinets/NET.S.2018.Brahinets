using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task6.Solution;

namespace Task6.Tests
{
    [TestFixture]
    public class CustomEnumerableTests
    {
        [Test]
        public void Generator_ForSequence1()
        {
            int[] expected = {1, 1, 2, 3, 5, 8, 13, 21, 34, 55};

            int[] actual = SequenceGenerator.GetSequence(1, 1, 10, (xn, xn_1) => xn + xn_1).ToArray();

            CollectionAssert.AreEqual(expected,actual);
        }

        [Test]
        public void Generator_ForSequence2()
        {
            int[] expected = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512 };

            int[] actual = SequenceGenerator.GetSequence(1, 2, 10, (xn, xn_1) => 2*xn).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Generator_ForSequence3()
        {
            double[] expected = { 1, 2, 2.5, 3.3, 4.05757575757576, 4.87086926018965, 5.70389834408211, 6.55785277425587, 7.42763417076325, 8.31053343902137 };

            double[] actual = SequenceGenerator.GetSequence(1.0, 2.0, 10, (xn, xn_1) => xn + xn_1 / xn).ToArray();

            double accuracy = 1e-7;

            CollectionAssert.AreEqual(expected, actual, Comparer<double>.Create(
                (a, b) =>
                {
                    if (Math.Abs(a - b) <= accuracy)
                    {
                        return 0;
                    }

                    return (int)(a - b);
                }));
        }


    }
}
