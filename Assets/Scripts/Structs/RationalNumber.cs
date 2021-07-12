using System;
using System.Globalization;

public struct RationalNumber : IComparable, IComparable<RationalNumber>, IEquatable<RationalNumber>
{
    public int Denominator { get; private set; }
    public int Numerator { get; private set; }
    
    public RationalNumber(int numerator, int denominator)
    {
        var gcd = Mathplus.GCD(numerator, denominator);

        Numerator = numerator / gcd;
        Denominator = denominator / gcd;
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
        throw new NotImplementedException();
    }
    
    public override string ToString()
    {
        return Numerator.ToString() + "/" + Denominator.ToString();
    }
}
