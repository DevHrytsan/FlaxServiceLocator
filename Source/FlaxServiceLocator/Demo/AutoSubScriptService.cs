using System;
using System.Collections.Generic;
using FlaxEngine;

namespace FlaxServiceLocator.Demo
{
    [AutoRegisteredService]
    public class AutoSubScriptService : ScriptRegistrable
    {
        public void DoWork()
        {
            Debug.Log($"{this.GetType().Name} is working!");
        }
    }
}