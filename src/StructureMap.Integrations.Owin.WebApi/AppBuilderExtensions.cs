using StructureMap;
using StructureMap.Integrations.Owin.WebApi;
using System;
using System.Web.Http;

namespace Owin
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseWebApiWithStructureMap(this IAppBuilder app, HttpConfiguration httpConfiguration, IContainer rootContainer)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            if (httpConfiguration == null)
            {
                throw new ArgumentNullException("httpConfiguration");
            }

            if (rootContainer == null)
            {
                throw new ArgumentNullException("rootContainer");
            }
            
            // Make sure Web API is configured to use StructureMap dependency resolver
            httpConfiguration.UseStructureMap(rootContainer);

            // Enable the web api middleware using the dependency scope adapter
            return app.UseWebApi(new DependencyScopeHttpServerAdapter(httpConfiguration));
        }
    }
}
