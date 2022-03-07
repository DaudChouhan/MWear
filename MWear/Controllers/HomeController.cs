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
            ViewBag.WebImages = dbobj.WebImages.Where(x => x.Active == true).ToList();
            ViewBag.Products = dbobj.Products.Where(x => x.Active == true).ToList();

            
            var categories = dbobj.Categories.Where(x => x.Active == true).ToList();
            ViewBag.Categories = categories;
            var productCategory = dbobj.ProductCategories.ToList();
            ViewBag.productCategory = productCategory;

            var colors = dbobj.Colors.Where(x => x.Active == true).ToList();
            ViewBag.Colors = colors;
            var productColor = dbobj.AvailabeColors.ToList();
            ViewBag.productColor = productColor;


            var sizes = dbobj.Sizes.Where(x => x.Active == true).ToList();
            ViewBag.Sizes = sizes;
            var productSize = dbobj.AvailableSizes.ToList();
            ViewBag.productSize = productSize;




            return View();
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