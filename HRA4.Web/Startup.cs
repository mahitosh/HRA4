using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HRA4.Web.Startup))]
namespace HRA4.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
