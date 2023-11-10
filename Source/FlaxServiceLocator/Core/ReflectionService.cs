using System;
using System.Collections.Generic;
using System.Linq;
using FlaxEngine;
using FlaxServiceLocator.Extensions;

namespace FlaxServiceLocator
{
    /// <summary>
    /// ReflectionService Script.
    /// </summary>
    public class ReflectionService
    {
        public static IEnumerable<Type> GetAllAutoRegisteredServices()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypesWithCustomAttribute<AutoRegisteredService>())
                .Where(service => typeof(IRegistrableService).IsAssignableFrom(service));

        }
    }
}