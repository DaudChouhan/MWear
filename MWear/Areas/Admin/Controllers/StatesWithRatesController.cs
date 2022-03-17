using MWear.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Areas.Admin.Controllers
{
    public class StatesWithRatesController : Controller
    {
        mwearEntities db = new mwearEntities();
        // GET: Admin/StatesWithRates
        public ActionResult Index()
        {
            var states = db.States.Where(x =>x.Active == true).ToList();
            return View(states);

        }
        public ActionResult AddStates()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStates(State states)
        {
            State state = new State();
            state.StateName = states.StateName;
            state.Charges = states.Charges;
            state.MaxOrderAmount = states.MaxOrderAmount;
            state.Active = true;
            db.States.Add(state);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditStates(int? stateId)
        {
            var stat = db.States.Where(x => x.Id == stateId).FirstOrDefault();
            return View(stat);
        }

        [HttpPost]
        public ActionResult EditStates(State states)
        {
            State stat = db.States.Where(x => x.Id == states.Id).FirstOrDefault();
            stat.StateName = states.StateName;
            stat.Charges = states.Charges;
            stat.MaxOrderAmount = states.MaxOrderAmount;
            stat.Active = true;
            db.Entry(stat).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult DeleteStates(int? stateId)
        {
            if (stateId != null)
            {
                var cat = db.States.Where(x => x.Id == stateId).FirstOrDefault();
                cat.Active = false;
                db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }



    }
}