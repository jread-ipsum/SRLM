using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SRLM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Services
{
    public class RoleService
    {
        public void CreateAdmin()
        {
            var ctx = new ApplicationDbContext();
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
        }
        public void MakeMyUserAdmin()
        {
            var ctx = new ApplicationDbContext();
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

            var myUser = ctx.Users.SingleOrDefault(u => u.Email == "jread@gmail.com");
            if (myUser != null)
            {
                var adminRes = userManager.AddToRole(myUser.Id, "Admin");
            }
        }
    }
}
