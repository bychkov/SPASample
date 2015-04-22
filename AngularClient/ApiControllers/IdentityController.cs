using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace AngularClient.ApiControllers
{
    [Authorize]
    public class IdentityController : ApiController
    {
        public dynamic Get()
        {
            var principal = User as ClaimsPrincipal;

            return from c in principal.Identities.First().Claims
                   select new
                   {
                       c.Type,
                       c.Value
                   };
        }
    }
}
