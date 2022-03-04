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

        mwearEntities dbobj = new mwearEntities();
        

        public ActionResult Index()
        {
            var category = dbobj.Categories.Where(x => x.Active == true).ToList();
            return View(category);
        }

        public ActionResult AddCategory()
        {
            var pcat = dbobj.Categories.Where(x => x.ParentCategory == null).ToList();
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
            dbobj.Categories.Add(cat);
            dbobj.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditCategory(int categoryId)
        {
            var pcat = dbobj.Categories.Where(x => x.ParentCategory == null).ToList();
            ViewBag.PCat = pcat;
            var cat = dbobj.Categories.Where(x => x.CategoryID == categoryId).FirstOrDefault();
            return View(cat);
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            Category cat = dbobj.Categories.Where(x => x.CategoryID == category.CategoryID).FirstOrDefault();
            cat.CategoryName = category.CategoryName;
            cat.ParentCategory = category.ParentCategory;
            cat.CategoryDescription = category.CategoryDescription;
            cat.Active = true;
            dbobj.Entry(cat).State = EntityState.Modified;
            dbobj.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult DeleteCategory(int? categoryId)
        {
            if (categoryId != null)
            {
                var cat = dbobj.Categories.Where(x => x.CategoryID == categoryId).FirstOrDefault();
                cat.Active = false;
                dbobj.Entry(cat).State = EntityState.Modified;
                dbobj.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }


    }
}