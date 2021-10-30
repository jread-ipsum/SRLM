using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SRLM.MVC.Startup))]
namespace SRLM.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
