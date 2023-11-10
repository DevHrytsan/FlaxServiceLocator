using System;
using FlaxEngine;

namespace FlaxServiceLocator.Extension
{
    public static class TypeExtensions
    {
        public static bool IsScript(this Type t)
        {
            return typeof(Script).IsAssignableFrom(t);
        }
    }
}
