using MWear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Controllers
{
    public class HomeController : Controller
    {

        mwearEntities dbobj = new mwearEntities();
        public ActionResult Index()
        {
            var images = dbobj.WebImages.Where(x => x.Active == true).ToList();
            return View(images);
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult CustomerService()
        {
            return View();
        }

        public ActionResult DeliveryAndHandling()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ReturnAndExchange()
        {
            return View();
        }
        

        public ActionResult TermsAndConditions()
        {
            return View();
        }

        public ActionResult OrderTracking()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult Wishlist()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

    }
}