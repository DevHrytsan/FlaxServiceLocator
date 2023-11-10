using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlaxServiceLocator.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypesWithCustomAttribute<TAttribute>(this Assembly assembly) where TAttribute : Attribute
        {
            return assembly.GetTypes().Where(t => t.GetCustomAttribute<TAttribute>() != null);
        }
    }
}
