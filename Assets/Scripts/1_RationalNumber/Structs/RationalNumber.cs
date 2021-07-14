using System;
using System.Globalization;
using UnityEngine;

public struct RationalNumber : IComparable, IComparable<RationalNumber>, IEquatable<RationalNumber>
{
    public int Denominator { get; private set; }
    public int Numerator { get; private set; }
    
    public RationalNumber(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("Denominator cant be 0!");

        Numerator = numerator;
        Denominator = denominator;
        
        Reduce();
    }
    
    //Conversion
    public static implicit operator float(RationalNumber a) => (float) a.Numerator / a.Denominator;
    public static explicit operator RationalNumber(float f)
    {
        //https://stackoverflow.com/questions/5124743/algorithm-for-simplifying-decimal-to-fractions
        float error = 0.000001f;
        var n = (int)Math.Floor(f);
        f -= n;

        if (f < error)
            return new RationalNumber(n, 1);
        if (1 - error < f)
            return new RationalNumber(n + 1, 1);

        var lower_n = 0;
        var lower_d = 1;
        var upper_n = 1;
        var upper_d = 1;

        while (true)
        {
            var middle_n = lower_n + upper_n;
            var middle_d = lower_d + upper_d;

            if (middle_d * (f + error) < middle_n)
            {
                upper_n = middle_n;
                upper_d = middle_d;
            }
            else if (middle_n < (f - error) * middle_d)
            {
                lower_n = middle_n;
                lower_d = middle_d;
            }
            else
                return new RationalNumber(n * middle_d + middle_n, middle_d);
        }
    }

    //Operator overload
    public static RationalNumber operator +(RationalNumber a) => a;
    public static RationalNumber operator -(RationalNumber a) => new RationalNumber(-a.Numerator, a.Denominator);
    
    public static RationalNumber operator +(RationalNumber a, RationalNumber b) =>
        new RationalNumber(a.Numerator * b.Denominator + a.Denominator * b.Numerator, a.Denominator * b.Denominator);
    public static RationalNumber operator -(RationalNumber a, RationalNumber b) => a + -b;
    public static RationalNumber operator *(RationalNumber a, RationalNumber b) =>
        new RationalNumber(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
    public static RationalNumber operator /(RationalNumber a, RationalNumber b)
    {
        if (b.Numerator == 0)
            throw new DivideByZeroException();
        
        return new RationalNumber(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
    }

    public static bool operator >(RationalNumber a, RationalNumber b) => a.CompareTo(b) > 0;
    public static bool operator >=(RationalNumber a, RationalNumber b) => a.CompareTo(b) >= 0;
    public static bool operator <(RationalNumber a, RationalNumber b) => a.CompareTo(b) < 0;
    public static bool operator <=(RationalNumber a, RationalNumber b) => a.CompareTo(b) <= 0;
    public static bool operator ==(RationalNumber a, RationalNumber b) => a.Equals(b);
    public static bool operator !=(RationalNumber a, RationalNumber b) => !(a == b);

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        if (!(obj is RationalNumber other)) throw new ArgumentException("Argument object is not rational number type");

        return CompareTo(other);
    }

    public int CompareTo(RationalNumber other)
    {
        return (int)(this - other);
    }

    public bool Equals(RationalNumber other)
    {
        return Numerator == other.Numerator && Denominator == other.Denominator;
    }
    
    public override string ToString()
    {
        return Numerator + "/" + Denominator;
    }

    private void Reduce()
    {
        if (Numerator == 0)
        {
            Denominator = 1;
        }
        else
        {
            var gcd = Mathplus.GCD(Math.Abs(Numerator), Math.Abs(Denominator));
            Numerator /= gcd;
            Denominator /= gcd;
        }

        if (Numerator < 0 && Denominator < 0)
        {
            Numerator = -Numerator;
            Denominator = -Denominator;
        }
    }
}
