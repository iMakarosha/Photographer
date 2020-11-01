using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Photographer.Startup))]
namespace Photographer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
