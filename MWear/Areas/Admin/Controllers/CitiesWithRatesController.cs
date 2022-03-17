using MWear.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Areas.Admin.Controllers
{
    public class CitiesWithRatesController : Controller
    {
        mwearEntities db = new mwearEntities();
        // GET: Admin/CitiesWithRates
        public ActionResult Index()
        {

            var cities = db.Cities.Where(x => x.Active == true).ToList();
            var states = db.States.Where(x => x.Active == true).ToList();
            ViewBag.States = states;
            return View(cities);
        }
        public ActionResult AddCities()
        {
            var state = db.States.Where(x => x.Active == true).ToList();
            ViewBag.States = state;
            return View();
        }
        [HttpPost]
        public ActionResult AddCities(City cities, string AvailableChechbox)
        {
            City cit = new City();
            cit.CityName = cities.CityName;
            cit.StateId = cities.StateId;
            cit.Charges = cities.Charges;
            cit.MaxOrderAmount = cities.MaxOrderAmount;
            if (AvailableChechbox == "on")
            {
                cities.IsFixedCharges = true;
            }
            else
            {
                cities.IsFixedCharges = false;
            }
            cit.IsFixedCharges = cities.IsFixedCharges;
            cit.Active = true;
            db.Cities.Add(cit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditCities(int cityId)
        {
            var states = db.States.Where(x => x.Active == true).ToList();
            ViewBag.Sattes = states;
            var city = db.Cities.Where(x => x.Id == cityId).FirstOrDefault();
            return View(city);
        }

        [HttpPost]
        public ActionResult EditCities(City cities, string AvailableChechbox)
        {
            City cit = db.Cities.Where(x => x.Id == cities.Id).FirstOrDefault();
            cit.CityName = cities.CityName;
            cit.StateId = cities.StateId;
            cit.Charges = cities.Charges;
            cit.MaxOrderAmount = cities.MaxOrderAmount;
            if (AvailableChechbox == "on")
            {
                cities.IsFixedCharges = true;
            }
            else
            {
                cities.IsFixedCharges = false;
            }
            cit.IsFixedCharges = cities.IsFixedCharges;
            cit.Active = true;
            db.Entry(cit).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult DeleteCities(int? cityId)
        {
            if (cityId != null)
            {
                var cat = db.Cities.Where(x => x.Id == cityId).FirstOrDefault();
                cat.Active = false;
                db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }










    }
}