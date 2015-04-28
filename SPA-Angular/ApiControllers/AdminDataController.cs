using System.Web.Http;

namespace SPA_Angular.ApiControllers
{
    [Authorize(Roles="Admin")]
    public class AdminDataController : ApiController
    {
        public string Get()
        {
            return "super secret admin only stuff";
        }
    }
}
