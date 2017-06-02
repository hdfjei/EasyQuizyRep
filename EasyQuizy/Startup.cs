using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EasyQuizy.Startup))]
namespace EasyQuizy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
