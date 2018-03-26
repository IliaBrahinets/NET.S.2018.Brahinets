using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Polynominal
{
    public const double DefaultAccuracy = 0.001;
    public const CoeffsOrder DefaultCoeffsOrder = CoeffsOrder.AscendingOrder;

    public int Degree { get; }
    public double Accuracy { get; }
    public double this[int variableDegree]
    {
        get
        {
            if(variableDegree > Degree + 1)
            {
                return 0;
            }

            if(variableDegree < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(variableDegree)} can't be < 0");
            }

            return Coeffs[variableDegree];
        }
    }

    /// <param name="coeffs">The coeffs of the created polynominal.</param>
    /// <param name="coeffsOrder">An order of polynominals coeffs, can be ascending or decreasing of powers.</param>
    /// <param name="accuracy">The accuracy used when polynominals are tested on equality.</param>
    /// <exception cref="ArgumentNullException">Thrown when the coeffs array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when accuracy is less than zero.</exception>
    public Polynominal(double[] coeffs, CoeffsOrder coeffsOrder = DefaultCoeffsOrder, double accuracy = DefaultAccuracy)
    {
        ConstructorDataValidation(coeffs, accuracy);

        int firstNonZeroIndex = WithoutLeadZeroCoeffs(coeffs, coeffsOrder);

        if(coeffs.Length == 0 || firstNonZeroIndex == -1)
        {
            Degree = 0;
            Coeffs = new double[1];
            return;
        }

        if (coeffsOrder == CoeffsOrder.AscendingOrder)
        {
            Coeffs = new double[firstNonZeroIndex + 1];

            for (int i = 0; i <= firstNonZeroIndex; i++)
            {
                Coeffs[i] = coeffs[i];
            }
        }
        else
        {
            Coeffs = new double[coeffs.Length - firstNonZeroIndex];

            for (int i = coeffs.Length - 1; i >= firstNonZeroIndex; i--)
            {
                Coeffs[coeffs.Length - 1 - i] = coeffs[i];
            }
        }

        Degree = Coeffs.Length - 1;

        Accuracy = accuracy;
    }

    /// <exception cref="ArgumentNullException">Thrown when both or one of the polynominals is null.</exception>
    public static Polynominal Add(Polynominal left, Polynominal right)
    {
        OverloadedOperationValidation(left, right);

        double[] newCoeffs = null;

        Polynominal more = left;
        Polynominal less = right;

        if (right.Degree > left.Degree)
        {
            more = right;
            less = left;
        }

        newCoeffs = new double[more.Degree + 1];

        more.Coeffs.CopyTo(newCoeffs, 0);

        for (int i = 0; i <= less.Degree; i++)
        {
            newCoeffs[i] += less.Coeffs[i];
        }

        double newAccuracy = AccuracySelectStrategy(more.Accuracy, less.Accuracy);

        return new Polynominal(newCoeffs, accuracy: newAccuracy);
    }

    /// <exception cref="ArgumentNullException">Thrown when the polynominal is null.</exception>
    public static Polynominal Add(Polynominal left, double right)
    {
        OverloadedOperationValidation(left);

        var newCoeffs = new double[left.Degree + 1];

        left.Coeffs.CopyTo(newCoeffs, 0);

        newCoeffs[0] += right;

        return new Polynominal(newCoeffs, accuracy: left.Accuracy);
    }

    /// <exception cref="ArgumentNullException">Thrown when the polynominal is null.</exception>
    public static Polynominal Add(double left, Polynominal right)
    {
        return Add(right, left);
    }

    public static Polynominal operator+(Polynominal left, Polynominal right)
    {
        return Add(left, right);
    }

    public static Polynominal operator+(Polynominal left, double right)
    {
        return Add(left, right);
    }

    public static Polynominal operator+(double left, Polynominal right)
    {
        return Add(left, right);
    }

    /// <exception cref="ArgumentNullException">Thrown when both or one of the polynominals is null.</exception>
    public static Polynominal Substract(Polynominal left, Polynominal right)
    {
        OverloadedOperationValidation(left, right);

        double[] newCoeffs = null;

        if (right.Degree > left.Degree)
        {
            newCoeffs = new double[right.Degree + 1];
        }
        else
        {
            newCoeffs = new double[left.Degree + 1];
        }

        left.Coeffs.CopyTo(newCoeffs, 0);

        for (int i = 0; i <= right.Degree; i++)
        {
            newCoeffs[i] -= right.Coeffs[i];
        }

        double newAccuracy = AccuracySelectStrategy(left.Accuracy, right.Accuracy);

        return new Polynominal(newCoeffs, accuracy: newAccuracy);
    }

    /// <exception cref="ArgumentNullException">Thrown when the polynominal is null.</exception>
    public static Polynominal Substract(Polynominal left, double right)
    {
        return Add(left, -right);
    }

    /// <exception cref="ArgumentNullException">Thrown when the polynominal is null.</exception>
    public static Polynominal Substract(double left, Polynominal right)
    {
        return Add(-left, right);
    }

    public static Polynominal operator-(Polynominal left, Polynominal right)
    {
        return Substract(left, right);
    }

    public static Polynominal operator-(Polynominal left, double right)
    {
        return Substract(left, right);
    }

    public static Polynominal operator-(double left, Polynominal right)
    {
        return Substract(left, right);
    }

    /// <exception cref="ArgumentNullException">Thrown when both or one of the polynominals is null.</exception>
    public static Polynominal Multiply(Polynominal left, Polynominal right)
    {
        OverloadedOperationValidation(left, right);
        
        double[] newCoeffs = new double[left.Degree + right.Degree + 1];

        for (int i = 0; i <= right.Degree; i++)
        {
            for (int j = 0; j <= left.Degree; j++)
            {
                newCoeffs[i + j] += left.Coeffs[i] * right.Coeffs[j];
            }
        }

        double newAccuracy = AccuracySelectStrategy(left.Accuracy, right.Accuracy);

        return new Polynominal(newCoeffs, accuracy: newAccuracy);
    }

    /// <exception cref="ArgumentNullException">Thrown when the polynominal is null.</exception>
    public static Polynominal Multiply(Polynominal left, double right)
    {
        OverloadedOperationValidation(left);

        double[] newCoeffs = new double[left.Degree + 1];

        left.Coeffs.CopyTo(newCoeffs, 0);

        for (int i = 0; i <= left.Degree; i++)
        {
            newCoeffs[i] *= right;
        }

        return new Polynominal(newCoeffs, accuracy: left.Accuracy);
    }

    /// <exception cref="ArgumentNullException">Thrown when the polynominal is null.</exception>
    public static Polynominal Multiply(double left, Polynominal right)
    {
        return Multiply(right, left);
    }

    public static Polynominal operator*(Polynominal left, Polynominal right)
    {
        return Multiply(left, right);
    }

    public static Polynominal operator*(Polynominal left, double right)
    {
        return Multiply(left, right);
    }

    public static Polynominal operator*(double left, Polynominal right)
    {
        return Multiply(left, right);
    }

    public static bool operator==(Polynominal left, Polynominal right)
    {
        if (Object.ReferenceEquals(left, right))
        {
            return true;
        }

        if (Object.ReferenceEquals(left, null) || Object.ReferenceEquals(right, null))
        {
            return false;
        }

        return IsEqual(left, right, null);
    }

    public static bool operator!=(Polynominal left, Polynominal right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Returns the coeffs of a polynominals are separated by " ".
    /// </summary>
    public override string ToString()
    {
        return ToString(" ");
    }

    /// <summary>
    /// Returns the coeffs of a polynominals are separated by the given separator.
    /// </summary>
    /// <param name="separator"></param>
    public string ToString(string separator)
    {
        StringBuilder result = new StringBuilder();

        result.Append(Coeffs[Degree]);

        int remain = Degree - 1;

        while(remain >= 0)
        {
            result.Append(separator);
            result.Append(Coeffs[remain]);
            remain--;
        }

        return result.ToString();
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Polynominal))
        {
            return false;
        }

        if (Object.ReferenceEquals(this, obj))
        {
            return true;
        }

        Polynominal other = obj as Polynominal;

        return IsEqual(this, other, null);
    }

    /// <summary>
    /// Test on equality two polinomials.
    /// </summary>
    /// <param name="accuracy">The maximum acceptable difference between the coeffs.</param>
    public bool Equals(Polynominal other, double accuracy)
    {
        if (Object.ReferenceEquals(other, null))
        {
            return false;
        }

        if (Object.ReferenceEquals(other, this))
        {
            return true;
        }

        return IsEqual(this, other, accuracy);
        
    }

    private static bool IsEqual(Polynominal a, Polynominal b, double? accuracy)
    {
        double equalityAccuracy;

        if (accuracy == null)
        {
            equalityAccuracy = AccuracySelectStrategy(a.Accuracy, b.Accuracy);
        }
        else
        {
            equalityAccuracy = (double)accuracy;
        }

        int maxDegree = Math.Max(a.Degree, b.Degree);

        for (int i = 0; i <= maxDegree; i++)
        {
            if (!a[i].AccurateEquals(b[i], equalityAccuracy))
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    private double[] Coeffs { get; }

    private void ConstructorDataValidation(double[] coeffs, double accuracy)
    {
        if (coeffs == null)
        {
            throw new ArgumentNullException($"{nameof(coeffs)} can not be null");
        }

        if (accuracy < 0)
        {
            throw new ArgumentOutOfRangeException($"{nameof(accuracy)} can't be < 0");
        }
    }

    private static void OverloadedOperationValidation(Polynominal left, Polynominal right)
    {
        if(left == null || right == null)
        {
            throw new ArgumentNullException("Arguments can't be null");
        }
    }

    private static void OverloadedOperationValidation(Polynominal polynominal)
    {
        if (polynominal == null)
        {
            throw new ArgumentNullException("Arguments can't be null");
        }
    }

    private static double AccuracySelectStrategy(double first, double second)
    {
        return Math.Min(first, second);
    }

    private int WithoutLeadZeroCoeffs(double[] coeffs, CoeffsOrder coeffsOrder)
    {

        if (coeffsOrder == CoeffsOrder.AscendingOrder)
        {
            int i = coeffs.Length - 1;

            while (i >= 0 && coeffs[i] == 0)
            {
                i--;
            }

            if (i < 0)
            {
                return -1;
            }

            return i;
        }
        else
        {
            int i = 0;

            while (i < coeffs.Length && coeffs[i] == 0)
            {
                i++;
            }

            if (coeffs.Length == i)
            {
                return -1;
            }

            return i;
        }
    }

    public enum CoeffsOrder
    {
        AscendingOrder,
        DecreasingOrder
    }
}