using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Controllers
{
    public class CartController : Controller
    {
        HomeController home = new HomeController();
        // GET: Cart
        public ActionResult Index()
        {
            TempData["cat"] = home.category();
            TempData.Keep();
            return View();
        }
    }
}