using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace StructureMap.Integrations.Samples.OwinAndWebApi
{
    public class UnitOfWorkFilter : ActionFilterAttribute
    {
        public async override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var session = actionExecutedContext.Request.GetDependencyScope().GetService(typeof(ISession)) as ISession;

            if (session != null)
            {
                await session.SaveChanges();
            }
        }
    }
}
