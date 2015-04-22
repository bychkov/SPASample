using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace AngularClient
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.EnableCors(new EnableCorsAttribute("http://localhost:21575, http://localhost:37045", "accept, authorization", "GET", "WWW-Authenticate"));

            config.SuppressDefaultHostAuthentication();

            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}", // note: api prefix is removed because it will be called from within app.Map("/api", innner => ... )
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
