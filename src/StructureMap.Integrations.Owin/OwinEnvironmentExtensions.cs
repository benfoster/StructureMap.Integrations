using System;
using System.Collections.Generic;

namespace StructureMap.Integrations.Owin
{
    public static class OwinEnvironmentExtensions
    {
        private const string RequestContainerKey = "sm:requestContainer";

        public static void SetRequestContainer(this IDictionary<string, object> owinEnvironment, IContainer requestContainer)
        {
            if (owinEnvironment == null)
            {
                throw new ArgumentNullException("owinEnvironment");
            }

            if (requestContainer == null)
            {
                throw new ArgumentNullException("requestContainer");
            }
            
            owinEnvironment.Add(RequestContainerKey, requestContainer);
        }

        public static IContainer GetRequestContainer(this IDictionary<string, object> owinEnvironment)
        {
            if (owinEnvironment == null)
            {
                throw new ArgumentNullException("owinEnvironment");
            }
            
            object requestContainer;

            if (owinEnvironment.TryGetValue(RequestContainerKey, out requestContainer))
            {
                return requestContainer as IContainer;
            }

            return null;
        }
    }

}
