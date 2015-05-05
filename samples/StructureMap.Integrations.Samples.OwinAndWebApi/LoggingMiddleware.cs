using StructureMap.Integrations.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace StructureMap.Integrations.Samples.OwinAndWebApi
{
    public class LoggingMiddleware
    {
        private Func<IDictionary<string, object>, Task> next;

        public LoggingMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var container = environment.GetRequestContainer();
            var session = container.GetInstance<ISession>();

            Trace.TraceInformation("Logging 'Begin Request' with session {0}.", session.Id);

            await next.Invoke(environment);

            Trace.TraceInformation("Logging 'End Request' with session {0}.", session.Id);
        }
    }
}
