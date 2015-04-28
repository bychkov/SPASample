using System.Web.Http;

namespace SPA_Angular.ApiControllers
{
    [Authorize]
    public class OtherDataController : ApiController
    {
        public string Get()
        {
            return "sensitive other data";
        }
    }
}
