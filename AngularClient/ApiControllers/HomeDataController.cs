using System.Web.Http;

namespace AngularClient.ApiControllers
{
    public class HomeDataController : ApiController
    {
        [AllowAnonymous]
        public string Get()
        {
            return "Unsecured home data";
        }
    }
}
