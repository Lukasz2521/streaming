using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace streaming_inż.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index(string userName)
        {
            return View();
        }

        public ActionResult UserProfile(int userId)
        {   
            return View("UserProfile");
        }
    }
}