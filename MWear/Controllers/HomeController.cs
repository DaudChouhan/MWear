using MWear.Classes;
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


            TempData["cat"] = category();
            TempData.Keep();
            return View();
        }

        public List<Category> category()
        {
            List<Category> categories = db.Categories.Where(x => x.Active == true).OrderBy(x => x.CategoryID).ToList();
            return categories;

        }
        public ActionResult Contact()
        {

            TempData["cat"] = category();
            TempData.Keep();
            return View();

        }

        public ActionResult AboutUs()
        {

            TempData["cat"] = category();
            TempData.Keep();
            return View();
        }

        public ActionResult CustomerService()
        {

            TempData["cat"] = category();
            TempData.Keep();
            return View();
        }

        public ActionResult DeliveryAndHandling()
        {

            TempData["cat"] = category();
            TempData.Keep();
            return View();
        }
        public ActionResult FAQ()
        {

            TempData["cat"] = category();


            TempData.Keep();
            return View();
        }

        public ActionResult Login()
        {

            TempData["cat"] = category();
            TempData.Keep();
            return View();
        }

        public ActionResult PrivacyPolicy()
        {

            TempData["cat"] = category();
            TempData.Keep();
            return View();
        }

        public ActionResult Register()
        {

            TempData["cat"] = category();
            TempData.Keep();
            return View();
        }

        public ActionResult ReturnAndExchange()
        {

            TempData["cat"] = category();
            TempData.Keep();
            return View();
        }
        

        public ActionResult TermsAndConditions()
        {

            TempData["cat"] = category();
            TempData.Keep();
            return View();
        }

        public ActionResult OrderTracking()
        {

            TempData["cat"] = category();
            TempData.Keep();
            return View();
        }

        public ActionResult UserProfile()
        {

            TempData["cat"] = category();
            TempData.Keep();
            return View();
        }

        public ActionResult Wishlist()
        {

            TempData["cat"] = category();
            TempData.Keep();
            return View();
        }

        public ActionResult Test()
        {
            TempData["cat"] = category();
            return View();
        }

        public JsonResult GetCartDetails()
        {
            dynamic dyn;
            try
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                dyn = new
                {
                    cartDetails = li2,
                    msg = "Success",
                    msgType = 1
                };
            }
            catch (Exception ex)
            {
                dyn = new
                {
                    msg = ex.StackTrace,
                    msgType = 2
                };
            }
            TempData["cat"] = category();
            TempData.Keep();
            return Json(dyn, JsonRequestBehavior.AllowGet);
        }

    }
}