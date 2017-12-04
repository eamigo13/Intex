using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Intex.Startup))]
namespace Intex
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
