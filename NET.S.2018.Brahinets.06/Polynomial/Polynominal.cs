﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Polynominal:IEnumerable<double>
{
    public int Degree { get; }

    public double this[int i]
    {
        get
        {
            return Coeffs[i];
        }
    }

    public enum CoeffsOrder
    {
        AscendingOrder,
        DecreasingOrder
    }

    public Polynominal(double[] coeffs, CoeffsOrder coeffsOrder = CoeffsOrder.AscendingOrder)
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
        if (this.Degree == 0)
        {
            return String.Empty;
        }

        StringBuilder result = new StringBuilder();

        result.Append(Coeffs[Degree + 1]);

        int remain = Degree;

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
            //so bad!!!
            //working on
            if(this.Coeffs[i] != other.Coeffs[i])
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

            //so bad!!!
            //working on
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

            //so bad!!!
            //working on
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


}

