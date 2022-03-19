using MWear.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Areas.Admin.Controllers
{
    public class ContactFormsController : Controller
    {

        mwearEntities db = new mwearEntities();
        // GET: Admin/ContactForms
        public ActionResult Index()
        {
            var contact = db.ContactForms.Where(x => x.Active == true && x.Seen== false).ToList();
            return View(contact);
        }

        public ActionResult SeenForms()
        {
            var contact = db.ContactForms.Where(x => x.Active == true && x.Seen == true).ToList();
            return View(contact);
        }
        public ActionResult DeletedForms()
        {
            var contact = db.ContactForms.Where(x => x.Active == false).ToList();
            return View(contact);
        }


        public JsonResult DeleteForm(int? formid)
        {
            if (formid != null)
            {
                var form = db.ContactForms.Where(x => x.CFormID == formid).FirstOrDefault();
                form.Active = false;
                db.Entry(form).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SeenForm(int? formid)
        {
            if (formid != null)
            {
                var form = db.ContactForms.Where(x => x.CFormID == formid).FirstOrDefault();
                form.Seen = true;
                db.Entry(form).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }

        public JsonResult UnSeenForm(int? formid)
        {
            if (formid != null)
            {
                var form = db.ContactForms.Where(x => x.CFormID == formid).FirstOrDefault();
                form.Seen = false;
                db.Entry(form).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }


    }
}