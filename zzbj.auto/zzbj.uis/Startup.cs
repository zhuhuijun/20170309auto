using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(zzbj.uis.Startup))]
namespace zzbj.uis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
