using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SPATemplate
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
