﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AngularClient.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("Home");
        }

        [Authorize]
        public ActionResult Secured()
        {
            return View("Secured", (User as ClaimsPrincipal).Claims);
        }
    }
}