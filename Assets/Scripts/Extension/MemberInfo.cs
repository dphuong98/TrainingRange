using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public static class MemberInfo
{
    public enum AccessModifiers
    {
        Private,
        Protected, 
        ProtectedInternal,
        Internal,
        Public
    }
    
    public static AccessModifiers AccessModifier(this FieldInfo f)
    {
        if (f.IsPrivate)
            return AccessModifiers.Private;
        if (f.IsFamily)
            return AccessModifiers.Protected;
        if (f.IsFamilyOrAssembly)
            return AccessModifiers.ProtectedInternal;
        if (f.IsAssembly)
            return AccessModifiers.Internal;
        if (f.IsPublic)
            return AccessModifiers.Public;
        throw new ArgumentException("Field did not contain any access modifier");
    }

    public static string Modifier(this FieldInfo f)
    {
        return f.AccessModifier().ToString().ToLower();
    }

    public static AccessModifiers AccessModifier(this PropertyInfo p)
    {
        if (p.SetMethod == null)
            return p.GetMethod.AccessModifier();
        if (p.GetMethod == null)
            return p.SetMethod.AccessModifier();
        var max = Math.Max((int)(p.GetMethod.AccessModifier()),
            (int)(p.GetMethod.AccessModifier()));
        return (AccessModifiers)max;
    }
    
    public static string Modifier(this PropertyInfo p)
    {
        return p.AccessModifier().ToString().ToLower();
    }

    public static AccessModifiers AccessModifier(this MethodInfo m)
    {
        if (m.IsPrivate)
            return AccessModifiers.Private;
        if (m.IsFamily)
            return AccessModifiers.Protected;
        if (m.IsFamilyOrAssembly)
            return AccessModifiers.ProtectedInternal;
        if (m.IsAssembly)
            return AccessModifiers.Internal;
        if (m.IsPublic)
            return AccessModifiers.Public;
        throw new ArgumentException("Method did not contain any access modifier");
    }
    
    public static string Modifier(this MethodInfo m)
    {
        return m.AccessModifier().ToString().ToLower();
    }
}
