using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StructureMap.Integrations.Owin
{
    public class RequestContainerMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> next;
        private readonly IContainer rootContainer;
        private readonly Action<ConfigurationExpression> requestContainerConfiguration;

        public RequestContainerMiddleware(
            Func<IDictionary<string, object>, Task> next, 
            IContainer rootContainer,
            Action<ConfigurationExpression> requestContainerConfiguration = null)
        {
            if (rootContainer == null)
            {
                throw new ArgumentNullException("rootContainer");
            }
            
            this.next = next;
            this.rootContainer = rootContainer;
            this.requestContainerConfiguration = requestContainerConfiguration;
        }

        public async Task Invoke(IDictionary<string, object> owinEnvironment)
        {
            var requestContainer = rootContainer.GetNestedContainer();

            if (requestContainerConfiguration != null)
            {
                requestContainer.Configure(requestContainerConfiguration);
            }
            
            owinEnvironment.SetRequestContainer(requestContainer);

            using (requestContainer)
            {
                await next(owinEnvironment);
            }
        }
    }

}
