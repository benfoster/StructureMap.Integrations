using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace StructureMap.Integrations.Samples.OwinAndWebApi
{
    public class ContactsController : ApiController
    {
        private readonly ISession session;
        public ContactsController(ISession session)
        {
            this.session = session;
        }
        
        public async Task<dynamic> Get()
        {
            return await session.List<Contact>();
        }
    }
}
