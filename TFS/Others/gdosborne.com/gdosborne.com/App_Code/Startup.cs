using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gdosborne.com.Startup))]
namespace gdosborne.com
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
