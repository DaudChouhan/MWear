﻿using MWear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Controllers
{
    public class ShopController : Controller
    {


        mwearEntities db = new mwearEntities();
        HomeController home = new HomeController();
        // GET: Shop
        public ActionResult Index(int? CatId,int pagenumber)
        {
            ViewBag.WebImages = db.WebImages.Where(x => x.Active == true).ToList();
            var productsCount = db.Products.Where(x => x.Active == true).Count();


            var categories = db.Categories.Where(x => x.Active == true).ToList();
            ViewBag.Categories = categories;
            var productCategory = db.ProductCategories.ToList();
            ViewBag.productCategory = productCategory;

            var colors = db.Colors.Where(x => x.Active == true).ToList();
            ViewBag.Colors = colors;
            var productColor = db.AvailabeColors.ToList();
            ViewBag.productColor = productColor;
            ViewBag.pagenumber = pagenumber;

            //Pagination testing
            var products = db.Products.Where(x => x.Active == true).OrderByDescending(x => x.ProductID).Skip((pagenumber - 1) * 20).Take(20).ToList();


            var sizes = db.Sizes.Where(x => x.Active == true).ToList();
            ViewBag.Sizes = sizes;
            var productSize = db.AvailableSizes.ToList();
            ViewBag.productSize = productSize;

            var pictures = db.Pictures.ToList();
            ViewBag.Pictures = pictures;

            if (CatId != null  && CatId != 0)
            {
                var catsearch = db.Categories.Where(x => (x.ParentCategory == CatId || x.CategoryID == CatId) && x.Active == true).Select(x => x.CategoryID).ToList();
                var procat = db.ProductCategories.Where(x => catsearch.Contains(x.Category)).Select(x => x.Product).ToList();
                products = products.Where(x => procat.Contains(x.ProductGUID)).ToList();

            }
            ViewBag.Products = products;
            ViewBag.ProductsCount = productsCount;
            TempData["cat"] = home.category();
            TempData.Keep();
            return View();
        }
    }
}