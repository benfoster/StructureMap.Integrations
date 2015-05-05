using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace StructureMap.Integrations.Samples.OwinAndWebApi
{
    class DbSession : ISession
    {
        private List<Contact> contacts = new List<Contact>
        {
            new Contact { Name = "Ben" },
            new Contact { Name = "Tim" },
            new Contact { Name = "Phil" }
        };

        public DbSession()
        {
            Id = Guid.NewGuid().ToString();
            Trace.TraceInformation("Session {0}: Created.", Id);
        }
        
        public string Id { get; private set; }
        public bool Disposed { get; private set; }
        
        public Task<IEnumerable<T>> List<T>()
        {
            if (typeof(T) == typeof(Contact))
            {
                Trace.TraceInformation("Session {0}: Getting contacts.", Id);
                return Task.FromResult((IEnumerable<T>)contacts);
            }

            throw new ArgumentException();
        }

        public Task SaveChanges()
        {
            Trace.TraceInformation("Session {0}: Saving changes.", Id);
            return Task.Delay(1000);
        }

        public void Dispose()
        {
            Trace.TraceInformation("Session {0}: Disposing.", Id);
            Disposed = true;
        }
    }
}
