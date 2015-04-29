using System.IdentityModel.Tokens;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityModel.Owin.ScopeValidation;
using Thinktecture.IdentityModel.Tokens;
using Thinktecture.IdentityServer.AccessTokenValidation;

namespace SPA_Angular
{
    public class FakeCertificateValidator : ICertificateValidator
    {
        public bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }

    public partial class Startup
    {
        private const string ClientId = "SPA-Angular";
        private const string IdentityServerUri = "https://localhost:44306/core";
        private const string ClientRedirectUri = "https://localhost:44308";

        public void ConfigureAuth(IAppBuilder app)
        {
            ThinktectureAuthentication(app);
        }

        private void ThinktectureAuthentication(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = ClaimMappings.None;

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions { AuthenticationType = CookieAuthenticationDefaults.AuthenticationType });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = IdentityServerUri,
                ClientId = ClientId,
                RedirectUri = ClientRedirectUri + "/#/",

                //ResponseType = "id_token",
                ResponseType = "code id_token token",
                Scope = "openid email profile localApi",
                //Scope = "openid email roles claims",

                BackchannelCertificateValidator = new FakeCertificateValidator(), //TODO: Remove the stub for prod certificate validation

                SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType
            });

            app.Map("/api", inner =>
            {
                inner.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = IdentityServerUri,
                    //RequiredScopes = new[] { "localApi" }
                });

                inner.RequireScopes(new ScopeValidationOptions()
                {
                    AllowAnonymousAccess = true,
                    Scopes = new[] { "localApi" }
                });


                HttpConfiguration config = new HttpConfiguration();

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

                inner.UseWebApi(config);
            });
        }
    }
}