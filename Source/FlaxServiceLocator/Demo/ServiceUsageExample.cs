using System;
using System.Collections.Generic;
using FlaxEngine;

namespace FlaxServiceLocator.Demo
{

    /// <summary>
    /// ServiceUsageExample Script.
    /// </summary>
    public class ServiceUsageExample : Script
    {
         public AutoSubScriptService _autoSubScriptService;
         public ManualSubScriptService _manualSubScriptService;
   
        public override void OnStart()
        {
            //Unfortunately, I can`t manage it to work with just plugin initialization.
            //In Flax, order execution is random. So It`s quite problematic.
            //If u have solution, feel free to contribute
            ServiceLocatorPlugin.Services.CheckAutoRegisteredServices();

            if (ServiceLocatorPlugin.Services.IsRegistered<AutoSubScriptService>())
            {
                _autoSubScriptService = ServiceLocatorPlugin.Services.Get<AutoSubScriptService>();
                _autoSubScriptService.DoWork();

            }

            //Get registered service
            if (ServiceLocatorPlugin.Services.IsRegistered<ManualSubScriptService>())
            {
                _manualSubScriptService = ServiceLocatorPlugin.Services.Get<ManualSubScriptService>();
                Debug.Log($"{nameof(_manualSubScriptService)} has been registered and received!");
            }

            try
            {
                _manualSubScriptService = ServiceLocatorPlugin.Services.Get<ManualSubScriptService>();
            }
            catch (Exception)
            {
                Debug.Log($"Sorry, but no instance of the {nameof(ManualSubScriptService)} = no service reference");

                //Get service and if it doesn't exist create one. Be careful: Some services may
                //require some configuration. Especially Script based services. 
                _manualSubScriptService = ServiceLocatorPlugin.Services.Get<ManualSubScriptService>(ServiceLocator.Retrieve.FindOrCreate, Actor);
                Debug.Log($"Give me my {nameof(ManualSubScriptService)}!");

            }

            _manualSubScriptService.DoWork();

        }

    }
}