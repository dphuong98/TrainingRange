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
    //TODO
    
    //Operator overload
    public static RationalNumber operator +(RationalNumber a) => a;
    public static RationalNumber operator -(RationalNumber a) => new RationalNumber(-a.Numerator, a.Denominator);
    
    public static RationalNumber operator +(RationalNumber a, RationalNumber b) =>
        new RationalNumber(a.Numerator * b.Denominator + a.Denominator * b.Numerator, a.Denominator * b.Denominator)
            .Reduce();
    public static RationalNumber operator -(RationalNumber a, RationalNumber b) => a + -b;
    public static RationalNumber operator *(RationalNumber a, RationalNumber b) =>
        new RationalNumber(a.Numerator * b.Numerator, a.Denominator * b.Denominator).Reduce();
    public static RationalNumber operator /(RationalNumber a, RationalNumber b)
    {
        if (b.Numerator == 0)
            throw new DivideByZeroException();
        
        return new RationalNumber(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
    }
    
    //TODO > >= < <=
    public static bool operator ==(RationalNumber a, RationalNumber b) => a.Equals(b);
    public static bool operator !=(RationalNumber a, RationalNumber b) => !(a == b);

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
        return Numerator == other.Numerator && Denominator == other.Denominator;
    }
    
    public override string ToString()
    {
        return Numerator + "/" + Denominator;
    }

    private RationalNumber Reduce()
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
        
        return this;
    }
}
