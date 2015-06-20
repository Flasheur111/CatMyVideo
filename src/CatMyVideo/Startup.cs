using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CatMyVideo.Startup))]
namespace CatMyVideo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
