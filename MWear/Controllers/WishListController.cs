using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Controllers
{
    public class WishListController : Controller
    {
        // GET: WishList
        public ActionResult Index()
        {
            return View();
        }
    }
}