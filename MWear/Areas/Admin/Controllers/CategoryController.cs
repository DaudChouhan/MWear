using MWear.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category

        mwearEntities db = new mwearEntities();
        

        public ActionResult Index()
        {
            var category = db.Categories.Where(x => x.Active == true).ToList();
            return View(category);
        }

        public ActionResult AddCategory()
        {
            var pcat = db.Categories.Where(x => x.Active == true).ToList();
            ViewBag.PCat = pcat;
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            Category cat = new Category();
            cat.CategoryName = category.CategoryName;
            cat.ParentCategory = category.ParentCategory;
            cat.CategoryDescription = category.CategoryDescription;
            cat.Active = true;
            db.Categories.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditCategory(int categoryId)
        {
            var pcat = db.Categories.Where(x => x.ParentCategory == null).ToList();
            ViewBag.PCat = pcat;
            var cat = db.Categories.Where(x => x.CategoryID == categoryId).FirstOrDefault();
            return View(cat);
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            Category cat = db.Categories.Where(x => x.CategoryID == category.CategoryID).FirstOrDefault();
            cat.CategoryName = category.CategoryName;
            cat.ParentCategory = category.ParentCategory;
            cat.CategoryDescription = category.CategoryDescription;
            cat.Active = true;
            db.Entry(cat).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult DeleteCategory(int? categoryId)
        {
            if (categoryId != null)
            {
                var cat = db.Categories.Where(x => x.CategoryID == categoryId).FirstOrDefault();
                cat.Active = false;
                db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }


    }
}