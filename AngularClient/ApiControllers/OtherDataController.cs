using System.Web.Http;

namespace AngularClient.ApiControllers
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
