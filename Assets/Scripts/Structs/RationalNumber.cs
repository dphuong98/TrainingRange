using System;
using System.Globalization;

public struct RationalNumber : IComparable, IComparable<RationalNumber>, IEquatable<RationalNumber>
{
    public int Denominator { get; private set; }
    public int Numerator { get; private set; }
    
    public RationalNumber(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("Denominator cant be 0!");

        if (numerator == 0) {
            Numerator = numerator;
            Denominator = denominator;
        }
        else {
            var gcd = Mathplus.GCD(numerator, denominator);
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }
    }

    public int CompareTo(object obj)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(RationalNumber other)
    {
        throw new NotImplementedException();
    }

    public bool Equals(RationalNumber other)
    {
        if (Numerator == 0 && other.Numerator == 0)
            return true;

        return Numerator == other.Numerator && Denominator == other.Denominator;
    }
    
    public override string ToString()
    {
        return Numerator.ToString() + "/" + Denominator.ToString();
    }
}
