using System;
using FlaxEngine;

namespace FlaxServiceLocator
{
    /// <summary>
    /// Flax Service Locator plugin.
    /// </summary>
    /// <seealso cref="FlaxEngine.GamePlugin" />
    public class ServiceLocatorPlugin : GamePlugin
    {
        public static ServiceLocator Services => _serviceLocator;
        private static ServiceLocator _serviceLocator;

        /// <inheritdoc />
        public ServiceLocatorPlugin()
        {
            _description = new PluginDescription
            {
                Name = "FlaxServiceLocator",
                Category = "Scripting",
                Author = "DevHrytsan",
                AuthorUrl = "https://github.com/DevHrytsan",
                HomepageUrl = null,
                RepositoryUrl = "https://github.com/DevHrytsan/FlaxServiceLocator",
                Description = "Centralized registry for obtaining references to various services or components.",
                Version = new Version(0, 5, 0),
                IsAlpha = false,
                IsBeta = false,
            };
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            _serviceLocator = new ServiceLocator();
        }


        /// <inheritdoc />
        public override void Deinitialize()
        {
            _serviceLocator = null;
        }
    }
}
