using MWear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        mwearEntities db = new mwearEntities();
        // GET: Admin/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Where(x => x.OderStatus == "Pending").ToList();
            var orderids = orders.Select(x => x.OrderGuid).ToList();
            var orderdetails = db.OrderDetails.Where(x => orderids.Contains(x.OrderGuid)).ToList();
            ViewBag.orderdetails = orderdetails;
            var proids = orderdetails.Select(x => x.ProductID).ToList();
            var products = db.Products.Where(x => proids.Contains(x.ProductID)).ToList();
            ViewBag.products = products;
            ViewBag.city = db.Cities.ToList();
            ViewBag.state = db.States.ToList();
            return View(orders);
        }

        public ActionResult CanceledOrders()
        {
            var orders = db.Orders.Where(x => x.OderStatus == "Canceled").ToList();
            var orderids = orders.Select(x => x.OrderGuid).ToList();
            var orderdetails = db.OrderDetails.Where(x => orderids.Contains(x.OrderGuid)).ToList();
            ViewBag.orderdetails = orderdetails;
            var proids = orderdetails.Select(x => x.ProductID).ToList();
            var products = db.Products.Where(x => proids.Contains(x.ProductID)).ToList();
            ViewBag.products = products;
            ViewBag.city = db.Cities.ToList();
            ViewBag.state = db.States.ToList();
            return View(orders);
        }

        public ActionResult ProcessingOrders()
        {
            var orders = db.Orders.Where(x => x.OderStatus == "Processing").ToList();
            var orderids = orders.Select(x => x.OrderGuid).ToList();
            var orderdetails = db.OrderDetails.Where(x => orderids.Contains(x.OrderGuid)).ToList();
            ViewBag.orderdetails = orderdetails;
            var proids = orderdetails.Select(x => x.ProductID).ToList();
            var products = db.Products.Where(x => proids.Contains(x.ProductID)).ToList();
            ViewBag.products = products;
            ViewBag.city = db.Cities.ToList();
            ViewBag.state = db.States.ToList();
            return View(orders);
        }

        public ActionResult DeliveredOrders()
        {
            var orders = db.Orders.Where(x => x.OderStatus == "Delivered").ToList();
            var orderids = orders.Select(x => x.OrderGuid).ToList();
            var orderdetails = db.OrderDetails.Where(x => orderids.Contains(x.OrderGuid)).ToList();
            ViewBag.orderdetails = orderdetails;
            var proids = orderdetails.Select(x => x.ProductID).ToList();
            var products = db.Products.Where(x => proids.Contains(x.ProductID)).ToList();
            ViewBag.products = products;
            ViewBag.city = db.Cities.ToList();
            ViewBag.state = db.States.ToList();
            return View(orders);
        }

        public ActionResult ChangeStatus(int orderNo, string status)
        {
            var orders = db.Orders.Where(x => x.OrderID == orderNo).FirstOrDefault();
            var orderguid = orders.OrderGuid;
            orders.OderStatus = status;
            db.Entry(orders).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            if (status == "Canceled")
            {
                var orderdetails = db.OrderDetails.Where(x => x.OrderGuid == orderguid).ToList();
                foreach (var item in orderdetails)
                {
                    var prod = db.Products.Where(x => x.ProductID == item.ProductID).FirstOrDefault();
                    prod.UnitsInStock += item.Quantity;
                    db.Entry(prod).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}