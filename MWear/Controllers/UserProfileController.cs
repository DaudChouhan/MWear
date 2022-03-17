using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Controllers
{
    public class UserProfileController : Controller
    {
        HomeController home = new HomeController();
        // GET: UserProfile
        public ActionResult Index()
        {
            TempData["cat"] = home.category();
            TempData.Keep();
            return View();
        }

        public ActionResult EditProfile()
        {
            TempData["cat"] = home.category();
            TempData.Keep();
            return View();
        }

        public ActionResult UserOrderHistory()
        {
            TempData["cat"] = home.category();
            TempData.Keep();
            return View();
        }

        public ActionResult Wishlist()
        {
            TempData["cat"] = home.category();
            TempData.Keep();
            return View();
        }
        

    }
}