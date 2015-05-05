using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;

namespace StructureMap.Integrations.WebApi
{
    public class StructureMapDependencyResolver 
        : StructureMapDependencyScope, IDependencyResolver, IHttpControllerActivator
    {
        public StructureMapDependencyResolver(IContainer container) : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            return new StructureMapDependencyScope(container.GetNestedContainer());
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            // Make the current request injectable
            var scope = (StructureMapDependencyScope)request.GetDependencyScope();
            scope.Container.Inject<HttpRequestMessage>(request);

            return scope.GetService(controllerType) as IHttpController;
        }
    }
}
