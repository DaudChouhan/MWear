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

        mwearEntities db = new mwearEntities();
        public ActionResult Index()
        {
            ViewBag.WebImages = db.WebImages.Where(x => x.Active == true).ToList();
            ViewBag.Products = db.Products.Where(x => x.Active == true).ToList();

            
            var categories = db.Categories.Where(x => x.Active == true).ToList();
            ViewBag.Categories = categories;
            var productCategory = db.ProductCategories.ToList();
            ViewBag.productCategory = productCategory;

            var colors = db.Colors.Where(x => x.Active == true).ToList();
            ViewBag.Colors = colors;
            var productColor = db.AvailabeColors.ToList();
            ViewBag.productColor = productColor;


            var sizes = db.Sizes.Where(x => x.Active == true).ToList();
            ViewBag.Sizes = sizes;
            var productSize = db.AvailableSizes.ToList();
            ViewBag.productSize = productSize;

            var pictures = db.Pictures.ToList();
            ViewBag.Pictures = pictures;




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