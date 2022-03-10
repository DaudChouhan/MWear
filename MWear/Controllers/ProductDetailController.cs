using MWear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Controllers
{
    public class ProductDetailController : Controller
    {


        mwearEntities db = new mwearEntities();
        // GET: ProductDetail
        public ActionResult Index(string ProID)
        {

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


            var prod = db.Products.Where(x => x.ProductGUID == ProID).FirstOrDefault();
            return View(prod);
        }
    }
}