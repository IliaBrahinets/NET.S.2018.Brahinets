using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

public class Polynomial:IEquatable<Polynomial>,ICloneable
{
    public const CoeffsOrder DefaultCoeffsOrder = CoeffsOrder.AscendingOrder;
    public const double DefaultAccuracy = 0.1;

    public int Degree { get; }
    public static double Accuracy { get; }
    public double this[int variableDegree]
    {
        get
        {
            if(variableDegree > Degree)
            {
                throw new ArgumentException($"{nameof(Degree)} is more than polynomials degree");
            }

            if(variableDegree < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(variableDegree)} can't be < 0");
            }

            return Coeffs[variableDegree];
        }
    }

    /// <param name="coeffs">The coeffs of the created polynomial.</param>
    /// <param name="coeffsOrder">An order of polynomials coeffs, can be ascending or decreasing of powers.</param>
    /// <exception cref="ArgumentNullException">Thrown when the coeffs array is null.</exception>
    public Polynomial(double[] coeffs, CoeffsOrder coeffsOrder = DefaultCoeffsOrder)
    {
        ConstructorDataValidation(coeffs);

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
    }

    static Polynomial()
    {
        try
        {
            string fileString = ConfigurationSettings.AppSettings["accuracy"];
            Accuracy = double.Parse(fileString);
        }
        catch (Exception)
        {
            Accuracy = DefaultAccuracy;
        }
    }

    /// <exception cref="ArgumentNullException">Thrown when both or one of the polynomials is null.</exception>
    public static Polynomial Add(Polynomial left, Polynomial right)
    {
        OverloadedOperationValidation(left, right);

        double[] newCoeffs = null;

        Polynomial more = left;
        Polynomial less = right;

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

        return new Polynomial(newCoeffs);
    }

    /// <exception cref="ArgumentNullException">Thrown when the polynomial is null.</exception>
    public static Polynomial Add(Polynomial left, double right)
    {
        OverloadedOperationValidation(left);

        var newCoeffs = new double[left.Degree + 1];

        left.Coeffs.CopyTo(newCoeffs, 0);

        newCoeffs[0] += right;

        return new Polynomial(newCoeffs);
    }

    /// <exception cref="ArgumentNullException">Thrown when the polynomial is null.</exception>
    public static Polynomial Add(double left, Polynomial right)
    {
        return Add(right, left);
    }

    public static Polynomial operator+(Polynomial left, Polynomial right)
    {
        return Add(left, right);
    }

    public static Polynomial operator+(Polynomial left, double right)
    {
        return Add(left, right);
    }

    public static Polynomial operator+(double left, Polynomial right)
    {
        return Add(left, right);
    }

    /// <exception cref="ArgumentNullException">Thrown when both or one of the polynomials is null.</exception>
    public static Polynomial Substract(Polynomial left, Polynomial right)
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

        return new Polynomial(newCoeffs);
    }

    /// <exception cref="ArgumentNullException">Thrown when the polynomial is null.</exception>
    public static Polynomial Substract(Polynomial left, double right)
    {
        return Add(left, -right);
    }

    /// <exception cref="ArgumentNullException">Thrown when the polynomial is null.</exception>
    public static Polynomial Substract(double left, Polynomial right)
    {
        return Add(-left, right);
    }

    public static Polynomial operator-(Polynomial left, Polynomial right)
    {
        return Substract(left, right);
    }

    public static Polynomial operator-(Polynomial left, double right)
    {
        return Substract(left, right);
    }

    public static Polynomial operator-(double left, Polynomial right)
    {
        return Substract(left, right);
    }

    /// <exception cref="ArgumentNullException">Thrown when both or one of the polynomials is null.</exception>
    public static Polynomial Multiply(Polynomial left, Polynomial right)
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

        return new Polynomial(newCoeffs);
    }

    /// <exception cref="ArgumentNullException">Thrown when the polynomial is null.</exception>
    public static Polynomial Multiply(Polynomial left, double right)
    {
        OverloadedOperationValidation(left);

        double[] newCoeffs = new double[left.Degree + 1];

        left.Coeffs.CopyTo(newCoeffs, 0);

        for (int i = 0; i <= left.Degree; i++)
        {
            newCoeffs[i] *= right;
        }

        return new Polynomial(newCoeffs);
    }

    /// <exception cref="ArgumentNullException">Thrown when the polynomial is null.</exception>
    public static Polynomial Multiply(double left, Polynomial right)
    {
        return Multiply(right, left);
    }

    public static Polynomial operator*(Polynomial left, Polynomial right)
    {
        return Multiply(left, right);
    }

    public static Polynomial operator*(Polynomial left, double right)
    {
        return Multiply(left, right);
    }

    public static Polynomial operator*(double left, Polynomial right)
    {
        return Multiply(left, right);
    }

    public static bool operator==(Polynomial left, Polynomial right)
    {
        if (Object.ReferenceEquals(left, right))
        {
            return true;
        }

        if (Object.ReferenceEquals(left, null) || Object.ReferenceEquals(right, null))
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator!=(Polynomial left, Polynomial right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Returns the coeffs of a polynomials are separated by " ".
    /// </summary>
    public override string ToString()
    {
        return ToString(" ");
    }

    /// <summary>
    /// Returns the coeffs of a polynomials are separated by the given separator.
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
        if (obj.GetType() != typeof(Polynomial))
        {
            return false;
        }

        if (Object.ReferenceEquals(this, obj))
        {
            return true;
        }

        Polynomial other = obj as Polynomial;

        return Equals(other);
    }

    /// <summary>
    /// Test on equality two polinomials.
    /// </summary>
    public bool Equals(Polynomial other)
    {
        if (Object.ReferenceEquals(other, null))
        {
            return false;
        }

        if (Object.ReferenceEquals(other, this))
        {
            return true;
        }

        if(this.Degree != other.Degree)
        {
            return false;
        }

        for (int i = 0; i <= Degree; i++)
        {
            if (!Coeffs[i].AccurateEquals(other.Coeffs[i], Accuracy))
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

    private void ConstructorDataValidation(double[] coeffs)
    {
        if (coeffs == null)
        {
            throw new ArgumentNullException($"{nameof(coeffs)} can not be null");
        }
    }

    private static void OverloadedOperationValidation(Polynomial left, Polynomial right)
    {
        if(left == null || right == null)
        {
            throw new ArgumentNullException("Arguments can't be null");
        }
    }

    private static void OverloadedOperationValidation(Polynomial polynomial)
    {
        if (polynomial == null)
        {
            throw new ArgumentNullException("Arguments can't be null");
        }
    }

    private int WithoutLeadZeroCoeffs(double[] coeffs, CoeffsOrder coeffsOrder)
    {

        if (coeffsOrder == CoeffsOrder.AscendingOrder)
        {
            int i = coeffs.Length - 1;

            while (i >= 0 && coeffs[i].AccurateEquals(0,Accuracy))
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

            while (i < coeffs.Length && coeffs[i].AccurateEquals(0,Accuracy))
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

    public object Clone()
    {
        return new Polynomial(Coeffs);
    }

    public enum CoeffsOrder
    {
        AscendingOrder,
        DecreasingOrder
    }
}