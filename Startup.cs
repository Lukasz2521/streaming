using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using streaming_inż.Models;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(streaming_inż.Startup))]
namespace streaming_inż
{
    public partial class Startup
    {
        private static ApplicationDbContext context { get; } = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndUsers();
        }

        private void createRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
     
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);                 

                var user = new ApplicationUser();
                user.UserName = "Lukasz";
                user.Email = "lukasz.radecki94@gmail.com";

                string userPassword = "Qaz123!";

                var chkUser = UserManager.Create(user, userPassword);
  
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }
        }
    }
}
