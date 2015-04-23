using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityModel.Owin.ScopeValidation;
using Thinktecture.IdentityModel.Tokens;
using Thinktecture.IdentityServer.AccessTokenValidation;

namespace AngularClient
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
        private const string ClientId = "AngularClient";
        private const string IdentityServerUri = "https://localhost:44306/core";
        private const string ClientRedirectUri = "https://localhost:44307";

        public void ConfigureAuth(IAppBuilder app, HttpConfiguration config)
        {
            ThinktectureAuthentication(app, config);
        }

        private void ThinktectureAuthentication(IAppBuilder app, HttpConfiguration config)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = ClaimMappings.None;

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = IdentityServerUri,
                ClientId = ClientId,
                RedirectUri = ClientRedirectUri,

                //ResponseType = "id_token",
                ResponseType = "code id_token token",
                Scope = "openid email profile localApi",
                //Scope = "openid email roles claims",

                BackchannelCertificateValidator = new FakeCertificateValidator(), //TODO: Remove the stub for prod certificate validation

                SignInAsAuthenticationType = "Cookies"
            });

            app.Map("/api", inner =>
            {
                inner.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = IdentityServerUri,
                    // RequiredScopes = new[] { "localApi" }
                });

                inner.RequireScopes(new ScopeValidationOptions()
                {
                    AllowAnonymousAccess = true,
                    Scopes = new[] { "localApi" }
                });

                inner.UseWebApi(config);
            });
        }
    }
}