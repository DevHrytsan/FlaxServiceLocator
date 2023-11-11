using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FlaxEngine;
using FlaxServiceLocator.Extension;

namespace FlaxServiceLocator
{
    /// <summary>
    /// ServiceLocator Script.
    /// </summary>
    public class ServiceLocator
    {
        #region Private
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        #endregion
        #region Public    
        public enum Retrieve
        {
            No,
            Find,
            FindOrCreate
        }

        #endregion
        #region Constructors
        public ServiceLocator()
        {
            //Level.SceneLoaded += OnSceneLoadEvent;
        }
        ~ServiceLocator()
        {
            _services.Clear();
            //Level.SceneLoaded -= OnSceneLoadEvent;
        }
        #endregion

        #region Private Methods   
        //private void OnSceneLoadEvent(Scene scene, Guid guid)
        //{
        //    CheckAutoRegisteredServices();
        //}

        /// <summary>
        /// Registers a new instance of a service type in the _services dictionary.
        /// </summary>
        /// <param name="serviceType">The type of the service to be registered.</param>
        private void RegisterNewInstance(Type serviceType)
        {
            _services[serviceType] = Activator.CreateInstance(serviceType);
        }

        /// <summary>
        /// Finds a Script service of the specified type and registers it in the _services dictionary.
        /// </summary>
        /// <param name="serviceType">The type of the Script service to find.</param>
        /// <returns>The found or newly created instance of the Script service.</returns>
        private object FindScriptService(Type serviceType)
        {
            var inGameService = Level.FindScript(serviceType);

            if (inGameService == null)
            {
                throw new Exception($"{serviceType.Name} can`t find any related services.");
            }

            return inGameService;
        }

        /// <summary>
        /// Finds or creates a Script service of the specified type and registers it in the _services dictionary.
        /// </summary>
        /// <param name="serviceType">The type of the Script service to find or create.</param>
        /// <returns>The found or newly created instance of the Script service.</returns>
        private object FindOrCreateScriptService(Type serviceType, Actor parent = null)
        {
            var inGameService = Level.FindScript(serviceType);

            if (inGameService == null)
            {
                var newObject = new EmptyActor();
                newObject.Parent = parent;
                newObject.AddScript(serviceType);
                newObject.Name = $"{serviceType.Name} Service";
                inGameService = newObject.GetScript(serviceType);
            }

            return inGameService;
        }

        #endregion
        #region Public Methods

        /// <summary>
        /// Checks and processes all services that have been automatically registered.
        /// </summary>
        public void CheckAutoRegisteredServices()
        {
            foreach (var serviceType in ReflectionService.GetAllAutoRegisteredServices())
            {
                if (IsRegistered(serviceType)) continue;

                if (serviceType.IsScript())
                {
                    var service = FindScriptService(serviceType);
                    _services[serviceType] = service;

                }
                else
                {
                    RegisterNewInstance(serviceType);
                }
            }
        }

        /// <summary>
        /// Registers a service of type IRegistrableService
        /// </summary>
        /// <typeparam name="TService">The type of the service to be registered.</typeparam>
        /// <param name="service">The instance of the service to be registered.</param>
        /// <param name="safe">If true, throws an exception if the service is already registered</param>
        public void Register<TService>(TService service, bool safe = true) where TService : IRegistrableService, new()
        {
            var serviceType = typeof(TService);

            if (IsRegistered<TService>() && safe)
            {
                throw new Exception($"{serviceType.Name} has been already registered.");
            }

            _services[typeof(TService)] = service;
        }

        /// <summary>
        /// Retrieves an instance of the registered service of type TService.
        /// </summary>
        /// <typeparam name="TService">The type of the service to be retrieved.</typeparam>
        /// <param name="forced">Forces retrieval even if not previously registered.</param>
        /// <returns>The instance of the requested service.</returns>
        public TService Get<TService>(Retrieve forced = Retrieve.No, Actor serviceParent = null) where TService : IRegistrableService, new()
        {
            var serviceType = typeof(TService);

            if (IsRegistered<TService>())
            {
                return (TService)_services[serviceType];
            }

            if (forced == Retrieve.No)
            {
                throw new Exception($"{serviceType.Name} hasn't been registered.");
            }

            TService service = default;

            if (serviceType.IsScript())
            {
                switch (forced)
                {
                    case Retrieve.Find:
                        service = (TService)FindScriptService(serviceType);
                    break;

                    case Retrieve.FindOrCreate:
                        service = (TService)FindOrCreateScriptService(serviceType, serviceParent);
                    break;
                }
            }
            else
            {
               service = new TService();
            }

            Register(service);

            return service;
        }

        /// <summary>
        /// Checks if a service of the specified type is registered.
        /// </summary>
        public bool IsRegistered(Type t)
        {
            return _services.ContainsKey(t);
        }
        /// <summary>
        /// Checks if a service of type TService is registered.
        /// </summary>
        public bool IsRegistered<TService>()
        {
            return IsRegistered(typeof(TService));
        }
        #endregion
    }
}