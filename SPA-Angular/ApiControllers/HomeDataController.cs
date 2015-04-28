using System.Web.Http;

namespace SPA_Angular.ApiControllers
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
