using MWear.Classes;
using MWear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Controllers
{
    public class CheckoutController : Controller
    {
        mwearEntities db = new mwearEntities();
        HomeController home = new HomeController();
        // GET: Checkout
        public ActionResult Index()
        {
            var states = db.States.ToList();
            ViewBag.states = states;
            ViewBag.cities = db.Cities.ToList();
            TempData["cat"] = home.category();
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult CheckoutDetails(Order order, FormCollection collection)
        {
            order.OrderGuid = Guid.NewGuid().ToString();
            //var paymethod = collection["radio-group"].ToString();
            order.OrderDate = DateTime.Now;
            order.OderStatus = "Pending";
            order.DeliverDate = DateTime.Now.AddDays(7);
            order.CreateDate = DateTime.Now;
            order.Status = true;
            List<Cart> li2 = TempData["cart"] as List<Cart>;
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in li2)
            {
                orderDetails.Add(new OrderDetail
                {
                    OrderGuid = order.OrderGuid,
                    ProductID = item.ProductID,
                    Quantity = (int)item.Quantity,
                    GrossPrice = Convert.ToDecimal(item.UnitPrice),
                    Discount = Convert.ToDecimal(item.UnitPrice) - Convert.ToDecimal(item.NetPrice),
                    NetPrice = Convert.ToDecimal(item.NetPrice),
                    TotalPrice = Convert.ToDecimal(item.Quantity) * Convert.ToDecimal(item.NetPrice),
                    OrderDate = DateTime.Now,
                });
                var products = db.Products.Where(x => x.ProductID == item.ProductID).FirstOrDefault();
                products.UnitsInStock -= (int)item.Quantity;
                db.Entry(products).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            order.GrossAmout = orderDetails.Sum(x => x.TotalPrice);
            order.NetAmount = order.DeliverCharges + order.GrossAmout;

            db.OrderDetails.AddRange(orderDetails);
            db.Orders.Add(order);
            db.SaveChanges();
            TempData["cart"] = null;
            TempData["count"] = null;
            TempData["total"] = null;
            TempData["cat"] = home.category();
            TempData.Keep();
            return RedirectToAction("CheckoutSuccess",new { orderGuid = order.OrderGuid});
        }
        public ActionResult CheckoutSuccess(string orderGuid)
        {
            var orders = db.Orders.Where(x => x.OrderGuid == orderGuid).FirstOrDefault();
            var orderdetails = db.OrderDetails.Where(x => x.OrderGuid == orderGuid).ToList();
            ViewBag.orderdetails = orderdetails;
            var products = db.Products.Where(x => x.Available == true).ToList();
            ViewBag.Products = products;
            TempData["cat"] = home.category();
            
            TempData.Keep();
            return View(orders);
        }
    }
}