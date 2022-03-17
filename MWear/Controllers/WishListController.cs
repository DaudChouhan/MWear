using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Controllers
{
    public class WishListController : Controller
    {
        HomeController home = new HomeController();
        // GET: WishList
        public ActionResult Index()
        {
            TempData["cat"] = home.category();
            TempData.Keep();
            return View();
        }
    }
}