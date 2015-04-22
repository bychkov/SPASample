using Microsoft.Owin;
using Owin;
using STS;
using STS.Configuration;
using Thinktecture.IdentityServer.Core.Configuration;

[assembly: OwinStartup(typeof(Startup))]

namespace STS
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Map("/core", idsrvApp =>
            {
                var factory = InMemoryFactory.Create(
                    users: Users.Get(),
                    clients: Clients.Get(),
                    scopes: Scopes.Get());

                var options = new IdentityServerOptions
                {
                    SiteName = "Sample STS",
                    SigningCertificate = Certificate.Load(),
                    Factory = factory,
                };

                appBuilder.UseIdentityServer(options);
            });
        }
    }
}