using Owin;
using System.Web.Http;

namespace StructureMap.Integrations.Samples.OwinAndWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure StructureMap
            
            var container = new Container(cfg =>
            {
                cfg.For<ISession>().Use<DbSession>();
            });

            app.UseStructureMap(container);
            
            // Other middleware
            app.Use(typeof(LoggingMiddleware));
            app.UseWelcomePage("/");


            // Configure Web API

            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.XmlFormatter);
            
            httpConfiguration.Routes.MapHttpRoute("Default", "{controller}");
            httpConfiguration.Filters.Add(new UnitOfWorkFilter());

            app.UseWebApiWithStructureMap(httpConfiguration, container);
        }
    }
}
