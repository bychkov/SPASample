using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SPATemplate
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
