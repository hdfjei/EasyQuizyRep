using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Authentification.Startup))]
namespace Authentification
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
