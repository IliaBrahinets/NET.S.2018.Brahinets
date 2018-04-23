using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Solution
{
    public class Calculator
    {
        public double CalculateAverage(List<double> values, IAveragingMethod averagingMethod)
        {
            if (values == null)
            {
                throw new ArgumentNullException($"{nameof(values)} is null");
            }

            if (averagingMethod == null)
            {
                throw new ArgumentNullException($"{nameof(averagingMethod)} is null");
            }

            double result = averagingMethod.Calculate(values);

            return result;
        }

        public double CalculateAverage(List<double> values, Func<List<double>, double> averagingMethod)
        {
            if (values == null)
            {
                throw new ArgumentNullException($"{nameof(values)} is null");
            }

            if (averagingMethod == null)
            {
                throw new ArgumentNullException($"{nameof(averagingMethod)} is null");
            }

            double result = averagingMethod(values);

            return result;
        }
    }
}
