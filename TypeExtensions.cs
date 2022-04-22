using System;
using System.Collections.Generic;
using System.Linq;

// https://stackoverflow.com/questions/1613867/how-do-i-know-when-an-interface-is-directly-implemented-in-a-type-ignoring-inheri
public static class TypeExtensions
{
    public static IEnumerable<Type> GetInterfaces(this Type type, bool includeInherited)
    {
        if (includeInherited || type.BaseType == null)
            return type.GetInterfaces();
        else
            return type.GetInterfaces().Except(type.BaseType.GetInterfaces());
    }
}