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
    //[Authorize]
    public class ProductController : Controller
    {
        mwearEntities db = new mwearEntities();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var product = db.Products.Where(x => x.Active == true).ToList();

            var productCategories = db.ProductCategories.ToList();
            ViewBag.ProductCategories = productCategories;
            var procatids = productCategories.Select(x => x.Category).ToList();
            ViewBag.Categories = db.Categories.Where(x => procatids.Contains(x.CategoryID)).ToList();

            var productColors = db.AvailabeColors.ToList();
            ViewBag.AvailableColors = productColors;
            var procolids = productColors.Select(x => x.AvialableColor).ToList();
            ViewBag.Colors = db.Colors.Where(x => procatids.Contains(x.ColorID)).ToList();




            return View(product);
        }

        public ActionResult AddProduct()
        {
            var categories = db.Categories.Where(x => x.Active == true).ToList();
            ViewBag.Categories = categories;
            var colors = db.Colors.Where(x => x.Active == true).ToList();  
            ViewBag.Colors = colors;
            var sizes = db.Sizes.Where(x => x.Active == true).ToList();
            ViewBag.Sizes = sizes;
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product,List<HttpPostedFileBase> secondaryImage, FormCollection collection, HttpPostedFileBase primaryImage, string FeaturedChechbox, string AvailableChechbox)
        {
            var productGuid = Guid.NewGuid().ToString();




            // Add selected category in product category list start
            var category = collection["category"];
            List<ProductCategory> procat = new List<ProductCategory>();
            if (category != null)
            {
                var catarr = category.Split(',');
                for (int i = 0; i < catarr.Length; i++)
                {
                    procat.Add(new ProductCategory()
                    {
                        Product = productGuid,
                        Category = Convert.ToInt32(catarr[i])
                    });
                }
                db.ProductCategories.AddRange(procat);
            }
            

            

            
            // Add selected category in product category list end






            // Add selected color in available color list start
            var color = collection["colors"];
            if (color != null)
            {
                var colorr = color.Split(',');

                List<AvailabeColor> procolor = new List<AvailabeColor>();

                for (int i = 0; i < colorr.Length; i++)
                {
                    procolor.Add(new AvailabeColor()
                    {
                        Product = productGuid,
                        AvialableColor = Convert.ToInt32(colorr[i])
                    });
                }
                db.AvailabeColors.AddRange(procolor);
            }
            
            // Add selected color in avaiable color list end






            // Add selected category in product category list start
            var size = collection["sizes"];
            if (size != null)
            {
                var sizee = size.Split(',');

                List<AvailableSize> prosize = new List<AvailableSize>();

                for (int i = 0; i < sizee.Length; i++)
                {
                    prosize.Add(new AvailableSize()
                    {
                        Product = productGuid,
                        AvialableSize = Convert.ToInt32(sizee[i])
                    });
                }
                db.AvailableSizes.AddRange(prosize);
            }
            
            // Add selected category in product category list end






            // Add secondary image with product id in picture table & save secondary image in folder start
            if (secondaryImage != null)
            {
                List<Picture> productImage = new List<Picture>();
                foreach (var item in secondaryImage)
                {
                    if (item != null)
                    {
                        if (item.ContentLength <= 4100000 && (item.ContentType == "image/jpeg" || item.ContentType == "image/png"))
                        {
                            var fname = item.FileName.Replace(' ', '-');
                            fname = fname + DateTime.Now.ToString("ddMMyyyy HHmmss");
                            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Images" + "\\" + "Products" + "\\", fname);
                            string fpath = "/Images/Products/" + fname;
                            item.SaveAs(filepath);
                            productImage.Add(new Picture
                            {
                                PictureName = fname,
                                PicturePath = fpath,
                                PictureAltText = fname,
                                Product = productGuid
                            });
                        }

                    }
                }
                if (productImage.Count != 0)
                {
                    db.Pictures.AddRange(productImage);
                }
            }
            // Add secondary image with product id in picture table & save secondary image in folder end

            

            



            // Product Table Insert Start

            Product pro = new Product();
            pro.ProductGUID = productGuid;
            pro.SKU = product.SKU;
            pro.ProductName = product.ProductName;
            pro.ProductShortDesc = product.ProductShortDesc;
            pro.ProductLongDesc = product.ProductLongDesc;
            pro.QuantityPerUnit = product.QuantityPerUnit;
            pro.Price = product.Price;
            pro.Size = product.Size;
            pro.Color = product.Color;
            pro.Discount = product.Discount;
            pro.UnitWeight = product.UnitWeight;
            pro.UnitsInStock = product.UnitsInStock;
            pro.Note = product.Note;

            
                if (AvailableChechbox == "on")
            {
                product.Available = true;
            }
            else
            {
                product.Available = false;
            }
            pro.Available = product.Available;
            if (primaryImage != null)
            {
                if (primaryImage.ContentLength <= 4100000 && (primaryImage.ContentType == "image/jpeg" || primaryImage.ContentType == "image/png"))
                {
                    string filename = Path.GetFileName(primaryImage.FileName);
                    string fileimg = Path.Combine(Server.MapPath("~/Images/Products"), filename);
                    primaryImage.SaveAs(fileimg);
                    pro.Picture = filename;

                }
            }
            

            if (FeaturedChechbox == "on")
            {
                product.Featured = true;
            }
            else {
                product.Featured = false;
            }
            pro.Featured = product.Featured;
            pro.Active = true;
            db.Products.Add(pro);


            // Saving all changes start
            db.SaveChanges();
            // Saving all changes end
            // Redirecting to index start
            return RedirectToAction("Index");
            // Redirecting to index end
        }


        public ActionResult EditProduct(string pid)
        {
            var categories = db.Categories.Where(x => x.Active == true).ToList();
            ViewBag.Categories = categories;
            var colors = db.Colors.Where(x => x.Active == true).ToList();
            ViewBag.Colors = colors;
            var sizes = db.Sizes.Where(x => x.Active == true).ToList();
            ViewBag.Sizes = sizes;
            ///Abdul Rehman
            var productCategory = db.ProductCategories.Where(x => x.Product == pid).ToList();
            ViewBag.productCategory = productCategory;
            //End
            var product = db.Products.Where(x => x.ProductGUID == pid).FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product, string FeaturedChechbox, string AvailableChechbox)
        {
            Product pro = db.Products.Where(x => x.ProductGUID == product.ProductGUID).FirstOrDefault();
            pro.SKU = product.SKU;
            pro.ProductName = product.ProductName;
            pro.ProductShortDesc = product.ProductShortDesc;
            pro.ProductLongDesc = product.ProductLongDesc;
            pro.QuantityPerUnit = product.QuantityPerUnit;
            pro.Discount = product.Discount;
            pro.UnitWeight = product.UnitWeight;
            pro.UnitsInStock = product.UnitsInStock;
            if (AvailableChechbox == "on")
            {
                product.Available = true;
            }
            else
            {
                product.Available = false;
            }
            pro.Available = product.Available;
            pro.Note = product.Note;
            if (FeaturedChechbox == "on")
            {
                product.Featured = true;
            }
            else
            {
                product.Featured = false;
            }
            pro.Featured = product.Featured;
            pro.Active = true;

            db.Entry(pro).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult DeleteProduct(int? productid)
        {
            if (productid != null)
            {
                var pro = db.Products.Where(x => x.ProductID == productid).FirstOrDefault();
                pro.Active = false;
                db.Entry(pro).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }
    }
}