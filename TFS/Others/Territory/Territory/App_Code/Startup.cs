using Microsoft.Owin;
using Owin;
[assembly: OwinStartupAttribute(typeof(Territory.Startup))]
namespace Territory
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
