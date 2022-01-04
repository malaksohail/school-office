using DcssePortal.Data;

using System.Linq;
using System.Web.Mvc;

namespace DcssePortal.Web.Controllers
{
  public class RoleController : Controller
    {
        ApplicationDbContext ApplicationDbContext = new ApplicationDbContext();
        // GET: Role
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {


                //if (!isAdminUser())
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            var Roles =  ApplicationDbContext.Roles.ToList();
            return View(Roles);

        }

    }
}