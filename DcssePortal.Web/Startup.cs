using DcssePortal.Data;
using DcssePortal.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DcssePortal.Web.Startup))]
namespace DcssePortal.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
           
                ApplicationDbContext applicationDbContext   = new ApplicationDbContext();

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(applicationDbContext));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(applicationDbContext));


                // In Startup iam creating first Admin Role and creating a default Admin User     
                if (!roleManager.RoleExists("Admin"))
                {

                    // first we create Admin rool    
                    var role = new IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);

                    //Here we create a Admin super user who will maintain the website                   

                    var user = new ApplicationUser();
                    user.UserName = "shakeel";
                    user.Email = "shakeel8748@gmail.com";

                    string userPWD = "P@ssw0rd";

                    var chkUser = UserManager.Create(user, userPWD);

                    //Add default User to Role Admin    
                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, "Admin");

                    }
                }

                // creating Creating Manager role     
                if (!roleManager.RoleExists("Faculty"))
                {
                    var role = new IdentityRole();
                    role.Name = "Faculty";
                    roleManager.Create(role);

                }

                // creating Creating Employee role     
                if (!roleManager.RoleExists("Student"))
                {
                    var role = new IdentityRole();
                    role.Name = "Student";
                    roleManager.Create(role);
                }
            }
        }
    }
