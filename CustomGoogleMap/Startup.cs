using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomGoogleMap.Startup))]
namespace CustomGoogleMap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
