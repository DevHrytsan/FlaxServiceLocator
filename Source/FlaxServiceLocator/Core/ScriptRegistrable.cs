using System;
using System.Collections.Generic;
using System.Xml.Linq;
using FlaxEngine;

namespace FlaxServiceLocator
{
    /*
   * The "ScriptRegistrable" extends class "Script"
   * and implements "IRegistrableService" interface. 
   *
   * "ScriptRegistrable" can also be marked with [AutoRegisteredService] attribute.
   * In this case the ServiceLocator will first try to locate existing service
   * in the active level. If it not found, new Actor with this script added
   * to it will be instantiated. 
   */
    public abstract class ScriptRegistrable : Script, IRegistrableService
    {

    }
}