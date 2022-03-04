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


        mwearEntities dbobj = new mwearEntities();


        public ActionResult Index()
        {
            var color = dbobj.Colors.Where(x => x.Active == true).ToList();
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
            dbobj.Colors.Add(col);
            dbobj.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditColor(int colorid)
        {

            var color = dbobj.Colors.Where(x => x.ColorID == colorid).FirstOrDefault();
            return View(color);
        }


        [HttpPost]
        public ActionResult EditColor(Color color)
        {
            Color col = dbobj.Colors.Where(x => x.ColorID == color.ColorID).FirstOrDefault();
            col.ColorName = color.ColorName;
            col.ColorHash = color.ColorHash;
            col.ColorDescription = color.ColorDescription;
            col.Active = true;
            dbobj.Entry(col).State = EntityState.Modified;
            dbobj.SaveChanges();
            return RedirectToAction("Index");
        }


        public JsonResult DeleteColor(int? colorid)
        {
            if (colorid != null)
            {
                var color = dbobj.Colors.Where(x => x.ColorID == colorid).FirstOrDefault();
                color.Active = false;
                dbobj.Entry(color).State = EntityState.Modified;
                dbobj.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }
    }
}