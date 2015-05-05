using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StructureMap.Integrations.Samples.OwinAndWebApi
{
    public interface ISession : IDisposable
    {
        string Id { get; }
        bool Disposed { get; }
        Task<IEnumerable<T>> List<T>();
        Task SaveChanges();
    }
}
