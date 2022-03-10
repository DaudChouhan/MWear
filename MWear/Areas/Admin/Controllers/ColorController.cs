using MWear.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Areas.Admin.Controllers
{
    public class ColorController : Controller
    {
        // GET: Admin/Color


        mwearEntities db = new mwearEntities();


        public ActionResult Index()
        {
            var color = db.Colors.Where(x => x.Active == true).ToList();
            return View(color);
        }


        public ActionResult AddColor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddColor(Color color)
        {
            Color col = new Color();
            col.ColorName = color.ColorName;
            col.ColorHash = color.ColorHash;
            col.ColorDescription = color.ColorDescription;
            col.Active = true;
            db.Colors.Add(col);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditColor(int colorid)
        {

            var color = db.Colors.Where(x => x.ColorID == colorid).FirstOrDefault();
            return View(color);
        }


        [HttpPost]
        public ActionResult EditColor(Color color)
        {
            Color col = db.Colors.Where(x => x.ColorID == color.ColorID).FirstOrDefault();
            col.ColorName = color.ColorName;
            col.ColorHash = color.ColorHash;
            col.ColorDescription = color.ColorDescription;
            col.Active = true;
            db.Entry(col).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public JsonResult DeleteColor(int? colorid)
        {
            if (colorid != null)
            {
                var color = db.Colors.Where(x => x.ColorID == colorid).FirstOrDefault();
                color.Active = false;
                db.Entry(color).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }
    }
}