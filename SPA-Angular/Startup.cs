using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SPATemplate;
using SPA_Angular;

[assembly: OwinStartup(typeof(Startup))]

namespace SPA_Angular
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub";
            app.UseCors(CorsOptions.AllowAll);

            HttpConfiguration config = new HttpConfiguration();
            ConfigureAuth(app, config);
        }
    }
}