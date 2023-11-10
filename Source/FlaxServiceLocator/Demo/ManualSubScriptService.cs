using System;
using System.Collections.Generic;
using FlaxEngine;

namespace FlaxServiceLocator.Demo
{
    public class ManualSubScriptService : ScriptRegistrable
    {
        public override void OnAwake()
        {
            //Manual subscription must be in OnAwake
            ServiceLocatorPlugin.Services.Register(this);
        }

        public void DoWork()
        {
            Debug.Log($"{this.GetType().Name} is working!");
        }
    }
}