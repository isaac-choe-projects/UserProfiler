using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EntertainmentProfiler.Startup))]
namespace EntertainmentProfiler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
