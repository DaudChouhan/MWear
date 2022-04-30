using MWear.Classes;
using MWear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWear.Controllers
{
    public class ProductDetailController : Controller
    {


        mwearEntities db = new mwearEntities();
        List<Cart> cartlist = new List<Cart>();
        HomeController home = new HomeController();
        // GET: ProductDetail
        public ActionResult Index(string ProID)
        {

            var ppp = db.Products.Where(x => x.Active == true && x.Available == true && x.UnitsInStock != 0).ToList();
            ViewBag.Products = ppp;
            var categories = db.Categories.Where(x => x.Active == true).ToList();
            ViewBag.Categories = categories;

            var productCategory = db.ProductCategories.ToList();
            ViewBag.productCategory = productCategory;


            var colors = db.Colors.Where(x => x.Active == true).ToList();
            ViewBag.Colors = colors;
            var productColor = db.AvailabeColors.ToList();
            ViewBag.productColor = productColor;

            var sizes = db.Sizes.Where(x => x.Active == true).ToList();
            ViewBag.Sizes = sizes;
            var productSize = db.AvailableSizes.ToList();
            ViewBag.productSize = productSize;

            var pictures = db.Pictures.ToList();
            ViewBag.Pictures = pictures;

            var related = db.RelatedProducts.Where(x => x.Active == true && x.Product == ProID).ToList();
            ViewBag.Related = related;





            var prod = db.Products.Where(x => x.ProductGUID == ProID).FirstOrDefault();
            TempData["cat"] = home.category();
            TempData.Keep();
            return View(prod);
        }

        public JsonResult CartDetail(int? ProID, double? QTY)
        {
            var message = "";
            Cart carts = new Cart();
            if (ProID != null && QTY != null)
            {


                var productimage = db.Products.Where(x => x.ProductID == ProID).FirstOrDefault();
                var product = db.Products.Where(x => x.ProductID == ProID).FirstOrDefault();
                carts.ProductID = ProID;
                if (productimage != null)
                {
                    carts.ProductImage = productimage.Picture;
                }
                else
                {
                    carts.ProductImage = "/Images/Products/no-image.jpg";
                }
                carts.ProductName = product.ProductName;
                carts.UnitPrice = Convert.ToDouble(product.Price.Value.ToString("0.00"));
                if (product.Discount != null && product.Discount != 0)
                {
                    var net = product.Price - product.Discount;
                    carts.NetPrice = Convert.ToDouble(net.Value.ToString("0.00"));
                }
                else
                {
                    carts.NetPrice = Convert.ToDouble(product.Price.Value.ToString("0.00"));
                }

                carts.Quantity = QTY;
                if (db.AvailabeColors.Where(x => x.Product == product.ProductGUID).Count() > 0)
                {
                    carts.colorId = db.AvailabeColors.Where(x => x.Product == product.ProductGUID).FirstOrDefault().AvialableColor;
                    carts.colorName = db.Colors.Where(x => x.ColorID == carts.colorId).FirstOrDefault().ColorName;
                }
                if (db.AvailableSizes.Where(x => x.Product == product.ProductGUID).Count() > 0)
                {
                    carts.sizeId = db.AvailableSizes.Where(x => x.Product == product.ProductGUID).FirstOrDefault().AvialableSize;
                    carts.sizeName = db.Sizes.Where(x => x.SizeID == carts.sizeId).FirstOrDefault().SizeName;
                }
                
                if (TempData["cart"] == null)
                {
                    cartlist.Add(carts);
                    TempData["cart"] = cartlist;
                }
                else
                {
                    List<Cart> li2 = TempData["cart"] as List<Cart>;
                    if (li2.Where(x => x.ProductID == ProID).Count() == 0)
                    {
                        li2.Add(carts);
                    }
                    else
                    {
                        var fqty = li2.Where(x => x.ProductID == ProID).FirstOrDefault().Quantity;
                        fqty += QTY;
                        li2.Where(x => x.ProductID == ProID).FirstOrDefault().Quantity = fqty;

                    }

                    TempData["cart"] = li2;
                }
                if (TempData["cart"] != null)
                {
                    double total = 0.0;
                    int count = 0;
                    List<Cart> li2 = TempData["cart"] as List<Cart>;
                    foreach (var item in li2)
                    {
                        count += 1;
                        var gross = item.Quantity * item.NetPrice;
                        total += Convert.ToDouble(gross.Value.ToString("0.00"));
                    }
                    TempData["count"] = count;
                    TempData["total"] = total;
                }
                TempData["cat"] = home.category();
                TempData.Keep();
                message = "Success";
            }
            else
            {
                TempData["cat"] = home.category();
                TempData.Keep();
                message = "Error";
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}