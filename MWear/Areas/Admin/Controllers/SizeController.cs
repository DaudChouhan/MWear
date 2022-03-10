using MWear.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Areas.Admin.Controllers
{
    public class SizeController : Controller
    {
        // GET: Admin/Size


        mwearEntities db = new mwearEntities();
       

        public ActionResult Index()
        {
            var size = db.Sizes.Where(x => x.Active == true).ToList();
            return View(size);
        }

        public ActionResult AddSize()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddSize(Size size)
        {
            Size siz = new Size();
            siz.SizeName = size.SizeName;
            siz.DisplayName = size.DisplayName;
            siz.SizeDescription = size.SizeDescription;
            siz.Active = true;
            db.Sizes.Add(siz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult EditSize(int sizeid)
        {
            //var pcat = db.Categories.Where(x => x.ParentCategory == null).ToList();
            //ViewBag.PCat = pcat;
            var size = db.Sizes.Where(x => x.SizeID == sizeid).FirstOrDefault();
            return View(size);
        }


        [HttpPost]
        public ActionResult EditSize(Size size)
        {
            Size siz = db.Sizes.Where(x => x.SizeID == size.SizeID).FirstOrDefault();
            siz.SizeName = size.SizeName;
            siz.DisplayName = size.DisplayName;
            siz.SizeDescription = size.SizeDescription;
            siz.Active = true;
            db.Entry(siz).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult DeleteSize(int? sizeid)
        {
            if (sizeid != null)
            {
                var size = db.Sizes.Where(x => x.SizeID == sizeid).FirstOrDefault();
                size.Active = false;
                db.Entry(size).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }

    }
}