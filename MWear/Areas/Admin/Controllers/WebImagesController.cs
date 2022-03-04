using MWear.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Areas.Admin.Controllers
{
    public class WebImagesController : Controller
    {
        // GET: Admin/WebImages

        mwearEntities dbobj = new mwearEntities();
        //Web Images
        public ActionResult Index()
        {
            var images = dbobj.WebImages.Where(x => x.Active == true).ToList();
            return View(images);
        }

        public ActionResult AddWebImages()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddWebImages(WebImage image, HttpPostedFileBase file)
        {
            WebImage img = new WebImage();
            img.ImageName = image.ImageName;

            if (file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string fileimg = Path.Combine(Server.MapPath("~/Images/Products"), filename);
                file.SaveAs(fileimg);
                img.ImagePath = filename;

            }

            img.ImageAltText = image.ImageAltText;
            img.Position = image.Position;
            img.Active = true;
            dbobj.WebImages.Add(img);
            dbobj.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditWebImages(int webimages)
        {

            var image = dbobj.WebImages.Where(x => x.ImageID == webimages).FirstOrDefault();
            return View(image);
        }


        [HttpPost]
        public ActionResult EditWebImages(WebImage image)
        {
            WebImage img = dbobj.WebImages.Where(x => x.ImageID == image.ImageID).FirstOrDefault();
            img.ImageName = image.ImageName;
            img.ImagePath = image.ImagePath;
            img.ImageAltText = image.ImageAltText;
            img.Position = image.Position;
            img.Active = true;
            dbobj.Entry(img).State = EntityState.Modified;
            dbobj.SaveChanges();
            return RedirectToAction("Index");
        }





        public JsonResult DeleteWebImages(int? imageid)
        {
            if (imageid != null)
            {
                var imaage = dbobj.WebImages.Where(x => x.ImageID == imageid).FirstOrDefault();
                imaage.Active = false;
                dbobj.Entry(imaage).State = EntityState.Modified;
                dbobj.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }
    }
}