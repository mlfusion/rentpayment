using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rent.Web.Startup))]
namespace Rent.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
