using System;

namespace FlaxServiceLocator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegisteredService : Attribute {}
}
