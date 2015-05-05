using StructureMap.Integrations.WebApi;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace StructureMap.Integrations.Owin.WebApi
{
    /// <summary>
    /// Adapter that retrieves the request container from the Owin environment and sets the dependency scope for the request.
    /// </summary>
    public class DependencyScopeHttpServerAdapter : HttpServer
    {
        public DependencyScopeHttpServerAdapter(HttpConfiguration configuration)
            : base(configuration)
        {
        }

        public DependencyScopeHttpServerAdapter(HttpConfiguration configuration, HttpMessageHandler dispatcher)
            : base(configuration, dispatcher)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var container = request.GetOwinContext().Environment.GetRequestContainer();

            if (container != null)
            {
                // beware that setting the dependency scope like this will not register it for disposal automatically
                var scope = new StructureMapDependencyScope(container);
                request.Properties[HttpPropertyKeys.DependencyScope] = scope;
            }

            return base.SendAsync(request, cancellationToken);
        }
    }

}
