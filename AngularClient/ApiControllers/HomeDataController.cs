using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SPATemplate
{
    public class HomeDataController : ApiController
    {
        public string Get()
        {
            return "Unsecured home data";
        }
    }
}
