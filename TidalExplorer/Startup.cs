using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TidalExplorer.Startup))]
namespace TidalExplorer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
