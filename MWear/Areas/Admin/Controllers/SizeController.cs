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


        mwearEntities dbobj = new mwearEntities();
       

        public ActionResult Index()
        {
            var size = dbobj.Sizes.Where(x => x.Active == true).ToList();
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
            dbobj.Sizes.Add(siz);
            dbobj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult EditSize(int sizeid)
        {
            //var pcat = dbobj.Categories.Where(x => x.ParentCategory == null).ToList();
            //ViewBag.PCat = pcat;
            var size = dbobj.Sizes.Where(x => x.SizeID == sizeid).FirstOrDefault();
            return View(size);
        }


        [HttpPost]
        public ActionResult EditSize(Size size)
        {
            Size siz = dbobj.Sizes.Where(x => x.SizeID == size.SizeID).FirstOrDefault();
            siz.SizeName = size.SizeName;
            siz.DisplayName = size.DisplayName;
            siz.SizeDescription = size.SizeDescription;
            siz.Active = true;
            dbobj.Entry(siz).State = EntityState.Modified;
            dbobj.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult DeleteSize(int? sizeid)
        {
            if (sizeid != null)
            {
                var size = dbobj.Sizes.Where(x => x.SizeID == sizeid).FirstOrDefault();
                size.Active = false;
                dbobj.Entry(size).State = EntityState.Modified;
                dbobj.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }

    }
}