using StructureMap;
using StructureMap.Integrations.WebApi;
using System.Web.Http.Dispatcher;

namespace System.Web.Http
{
    public static class HttpConfigurationExtensions
    {
        /// <summary>
        /// Configures Web API to use StructureMap for dependency resolution. 
        /// Overrides <see cref="IHttpControllerActivator"/> to make <see cref="HttpRequestMessage"/> injectable.
        /// </summary>
        /// <param name="httpConfiguration"></param>
        /// <param name="container"></param>
        public static void UseStructureMap(this HttpConfiguration httpConfiguration, IContainer container) 
        {
            if (httpConfiguration == null)
            {
                throw new ArgumentNullException("httpConfiguration");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            var dependencyResolver = new StructureMapDependencyResolver(container);
            httpConfiguration.DependencyResolver = dependencyResolver;
            httpConfiguration.Services.Replace(typeof(IHttpControllerActivator), dependencyResolver);
        }
    }
}
