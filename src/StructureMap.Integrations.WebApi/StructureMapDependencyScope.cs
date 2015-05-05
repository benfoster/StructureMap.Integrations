using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace StructureMap.Integrations.WebApi
{
    public class StructureMapDependencyScope : IDependencyScope
    {
        protected IContainer container;
        
        public StructureMapDependencyScope(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        public IContainer Container
        {
            get
            {
                return container;
            }
        }

        public object GetService(Type serviceType)
        {
            return serviceType.IsAbstract || serviceType.IsInterface
                     ? container.TryGetInstance(serviceType)
                     : container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }
    }
}
