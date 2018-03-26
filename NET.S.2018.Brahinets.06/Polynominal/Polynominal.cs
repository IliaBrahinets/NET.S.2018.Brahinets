using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Polynominal:IEnumerable<double>
{
    public int Degree { get; }

    public const double DefaultAccuracy = 0.001;
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

    public enum CoeffsOrder
    {
        AscendingOrder,
        DecreasingOrder
    }
    public const CoeffsOrder DefaultCoeffsOrder = CoeffsOrder.AscendingOrder;

    public Polynominal(double[] coeffs, CoeffsOrder coeffsOrder = DefaultCoeffsOrder, double accuracy = DefaultAccuracy)
    {
        if(coeffs == null)
        {
            throw new ArgumentNullException($"{nameof(coeffs)} can not be null");
        }

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

    public static Polynominal Add(Polynominal a, Polynominal b)
    {
        double[] newCoeffs = null;

        Polynominal more = a;
        Polynominal less = b;

        if (b.Degree > a.Degree)
        {
            more = b;
            less = a;
        }

        newCoeffs = new double[more.Degree + 1];

        more.Coeffs.CopyTo(newCoeffs, 0);

        for (int i = 0; i <= less.Degree; i++)
        {
            newCoeffs[i] += less.Coeffs[i];
        }

        return new Polynominal(newCoeffs);
    }


    public static Polynominal operator+(Polynominal a, Polynominal b)
    {
        return Add(a, b);
    }

    public static Polynominal Substract(Polynominal a, Polynominal b)
    {
        double[] newCoeffs = null;

        if (b.Degree > a.Degree)
        {
            newCoeffs = new double[b.Degree + 1];
        }
        else
        {
            newCoeffs = new double[a.Degree + 1];
        }

        a.Coeffs.CopyTo(newCoeffs, 0);

        for (int i = 0; i <= b.Degree; i++)
        {
            newCoeffs[i] -= b.Coeffs[i];
        }

        return new Polynominal(newCoeffs);
    }

    public static Polynominal operator-(Polynominal a, Polynominal b)
    {
        return Substract(a, b);
    } 

    public static Polynominal Multiply(Polynominal a, Polynominal b)
    {
        double[] newCoeffs = new double[a.Degree + b.Degree + 1];

        for (int i = 0; i <= a.Degree; i++)
        {
            for (int j = 0; j <= b.Degree; j++)
            {
                newCoeffs[i + j] += a.Coeffs[i] * b.Coeffs[j];
            }
        }

        return new Polynominal(newCoeffs);
    }

    public static Polynominal operator*(Polynominal a, Polynominal b)
    {
        return Multiply(a, b);
    }

    public override string ToString()
    {
        return ToString(" ");
    }

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

        Polynominal other = obj as Polynominal;

        if(this.Degree != other.Degree)
        {
            return false;
        }

        for(int i = 0; i <= this.Degree; i++)
        {
            if (!this.Coeffs[i].AccurateEquals(other.Coeffs[i], Accuracy))
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

    public IEnumerator<double> GetEnumerator()
    {
        for (int i = 0; i <= Degree; i++)
            yield return Coeffs[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<double>)this).GetEnumerator();
    }

    private double[] Coeffs { get; }

    private int WithoutLeadZeroCoeffs(double[] coeffs, CoeffsOrder coeffsOrder)
    {

        if (coeffsOrder == CoeffsOrder.AscendingOrder)
        {
            int i = coeffs.Length - 1;

            while (i >= 0 && coeffs[i].AccurateEquals(0, Accuracy))
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

            while (i < coeffs.Length && coeffs[i].AccurateEquals(0, Accuracy))
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
}