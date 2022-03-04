using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MWear.Startup))]
namespace MWear
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
