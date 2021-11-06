using Microsoft.Owin;
using Owin;
using SRLM.Services;

[assembly: OwinStartupAttribute(typeof(SRLM.MVC.Startup))]
namespace SRLM.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var svc = new RoleService();
            svc.CreateAdmin();
            svc.MakeMyUserAdmin();
        }
    }
}
