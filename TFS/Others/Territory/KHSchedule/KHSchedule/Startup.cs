using Microsoft.Owin;
using Owin;
[assembly: OwinStartupAttribute(typeof(KHSchedule.Startup))]
namespace KHSchedule
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
