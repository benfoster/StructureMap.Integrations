using StructureMap;
using StructureMap.Integrations.Owin;
using System;

namespace Owin
{
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Enables StructureMap middleware that creates a new nested container at the beginning of each request
        /// and disposes of it at the end of the request.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="rootContainer"></param>
        /// <param name="requestContainerConfiguration"></param>
        /// <returns></returns>
        public static IAppBuilder UseStructureMap(this IAppBuilder app, IContainer rootContainer, Action<ConfigurationExpression> requestContainerConfiguration = null)
        {
            rootContainer.Inject<IAppBuilder>(app);
            app.Use(typeof(RequestContainerMiddleware), rootContainer, requestContainerConfiguration);

            return app;
        }
    }
}
