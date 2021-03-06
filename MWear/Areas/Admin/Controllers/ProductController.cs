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
            ViewBag.Colors = db.Colors.Where(x => procolids.Contains(x.ColorID)).ToList();

            var productSizes = db.AvailableSizes.ToList();
            ViewBag.AvailableSizes = productSizes;
            var prosizids = productSizes.Select(x => x.AvialableSize).ToList();
            ViewBag.Sizes = db.Sizes.Where(x => prosizids.Contains(x.SizeID)).ToList();




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
            var products = db.Products.Where(x => x.Active == true).ToList();
            ViewBag.Products = products;
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






            // Add selected Size in Available Size list start
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

            // Add selected Size in Available Size list End

            // Add Related Products in start
            var related = collection["relatedproducts"];
            if (related != null)
            {
                var relate = related.Split(',');

                List<RelatedProduct> relatedpro = new List<RelatedProduct>();

                for (int i = 0; i < relate.Length; i++)
                {
                    relatedpro.Add(new RelatedProduct()
                    {
                        Product = productGuid,
                        RelatedProduct1 = relate[i],
                        Active = true
                    }) ;
                }
                db.RelatedProducts.AddRange(relatedpro);
            }

            // Add Related Products in End







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
                            string fileimg = Path.Combine(Server.MapPath("~/Images/Products"), fname);
                            string fpath = fname;
                            item.SaveAs(fileimg);
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

            //Abdul Rehman Selecting Existing Category
            var productCategory = db.ProductCategories.Where(x => x.Product == pid).ToList();
            ViewBag.productCategory = productCategory;
            //End

            //Dawood Selecting Existing Color
            var productColor = db.AvailabeColors.Where(x => x.Product == pid).ToList();
            ViewBag.productColor = productColor;
            //End

            //Dawood Selecting Existing Color
            var productSize = db.AvailableSizes.Where(x => x.Product == pid).ToList();
            ViewBag.productSize = productSize;
            //End


            //Abdul Rehman Selecting Related Products
            var related = db.RelatedProducts.Where(x => x.Active == true && x.Product == pid).ToList();
            ViewBag.Related = related;

            var products = db.Products.Where(x => x.Active == true).ToList();
            ViewBag.Products = products;
            //End

            //Pictures
            var pics = db.Pictures.ToList();
            ViewBag.Pictures = pics;


            var product = db.Products.Where(x => x.ProductGUID == pid).FirstOrDefault();


            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product, string FeaturedChechbox, string AvailableChechbox, FormCollection collection, List<HttpPostedFileBase> secondaryImage, HttpPostedFileBase primaryImage)
        {
            var productguid = product.ProductGUID;

            // Product Table Edit Start
            Product pro = db.Products.Where(x => x.ProductGUID == product.ProductGUID).FirstOrDefault();

            if (primaryImage != null)
            {
                var propicold = db.Products.Where(x => x.ProductGUID == product.ProductGUID).FirstOrDefault().Picture;
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Images/Products"), propicold));
                if (primaryImage.ContentLength <= 4100000 && (primaryImage.ContentType == "image/jpeg" || primaryImage.ContentType == "image/png"))
                {
                    string filename = Path.GetFileName(primaryImage.FileName);
                    string fileimg = Path.Combine(Server.MapPath("~/Images/Products"), filename);
                    primaryImage.SaveAs(fileimg);
                    pro.Picture = filename;

                }
            }

            pro.SKU = product.SKU;
            pro.ProductName = product.ProductName;
            pro.ProductShortDesc = product.ProductShortDesc;
            pro.ProductLongDesc = product.ProductLongDesc;
            pro.QuantityPerUnit = product.QuantityPerUnit;
            pro.Price = product.Price;
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
            // Product Table Edit End


            // Re-Add selected category in product category table start
            var category = collection["category"];
            List<ProductCategory> procat = new List<ProductCategory>();
            if (category != null)
            {
                var existingcategory = db.ProductCategories.Where(x => x.Product == productguid).ToList();
                db.ProductCategories.RemoveRange(existingcategory);

                var catarr = category.Split(',');
                for (int i = 0; i < catarr.Length; i++)
                {
                    procat.Add(new ProductCategory()
                    {
                        Product = productguid,
                        Category = Convert.ToInt32(catarr[i])
                    });
                }
                db.ProductCategories.AddRange(procat);
            }
            // Re-Add selected category in product category table End




            // Re-Add selected color in available color list start
            var color = collection["colors"];
            List<AvailabeColor> procolor = new List<AvailabeColor>();
            if (color != null)
            {
                var existingcolor = db.AvailabeColors.Where(x => x.Product == productguid).ToList();
                db.AvailabeColors.RemoveRange(existingcolor);

                var colorr = color.Split(',');
                for (int i = 0; i < colorr.Length; i++)
                {
                    procolor.Add(new AvailabeColor()
                    {
                        Product = productguid,
                        AvialableColor = Convert.ToInt32(colorr[i])
                    });
                }
                db.AvailabeColors.AddRange(procolor);
            }
            // Re-Add selected color in avaiable color list end



            // Add selected Size in Available Size list start
            var size = collection["sizes"];
            List<AvailableSize> prosize = new List<AvailableSize>();
            if (size != null)
            {
                var existingsize = db.AvailableSizes.Where(x => x.Product == productguid).ToList();
                db.AvailableSizes.RemoveRange(existingsize);

                var sizee = size.Split(',');
                for (int i = 0; i < sizee.Length; i++)
                {
                    prosize.Add(new AvailableSize()
                    {
                        Product = productguid,
                        AvialableSize = Convert.ToInt32(sizee[i])
                    });
                }
                db.AvailableSizes.AddRange(prosize);
            }

            // Add selected Size in Available Size list End

            // Add Related Products start
            var related = collection["relatedproducts"];
            List<RelatedProduct> relate = new List<RelatedProduct>();
            if (related != null)
            {
                var existingrelated = db.RelatedProducts.Where(x => x.Product == productguid).ToList();
                db.RelatedProducts.RemoveRange(existingrelated);

                var relatee = related.Split(',');
                for (int i = 0; i < relatee.Length; i++)
                {
                    relate.Add(new RelatedProduct()
                    {
                        Product = productguid,
                        RelatedProduct1 = relatee[i],
                        Active = true
                    });
                }
                db.RelatedProducts.AddRange(relate);
            }

            // Add Related Products End


            // Add secondary image with product id in picture table & save secondary image in folder start
            if (secondaryImage != null)
            {
                List<Picture> productImage = new List<Picture>();

                foreach (var item in db.Pictures.Where(x=> x.Product == product.ProductGUID).ToList())
                {
                    if (item != null)
                    {
                        var propicold = db.Pictures.Where(x => x.Product == product.ProductGUID).FirstOrDefault().PictureName;
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Images/Products"), propicold));

                    }

                }
                var existingpics = db.Pictures.Where(x => x.Product == product.ProductGUID).ToList();
                db.Pictures.RemoveRange(existingpics);
                foreach (var item in secondaryImage)
                {
                    if (item != null)
                    {
                        if (item.ContentLength <= 4100000 && (item.ContentType == "image/jpeg" || item.ContentType == "image/png"))
                        {
                            var fname = item.FileName.Replace(' ', '-');
                            string fileimg = Path.Combine(Server.MapPath("~/Images/Products"), fname);
                            string fpath = fname;
                            item.SaveAs(fileimg);
                            productImage.Add(new Picture
                            {
                                PictureName = fname,
                                PicturePath = fpath,
                                PictureAltText = fname,
                                Product = product.ProductGUID
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











            // Saving Changes and Redirecting
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult DeleteProduct(int? productid)
        {
            if (productid != null)
            {
                var pro = db.Products.Where(x => x.ProductID == productid).FirstOrDefault();
                pro.Active = false;
                foreach (var item in db.Products.Where(x => x.ProductID == productid).ToList())
                {
                    var existingcategory = db.ProductCategories.Where(x => x.Product == item.ProductGUID).ToList();
                    db.ProductCategories.RemoveRange(existingcategory);
                }
                //var gettingid = db.Products.Where(x => x.ProductID == productid).ToList();
                //var existingcategory = db.ProductCategories.Where(x => x.Product == gettingid).ToList();
                

                db.Entry(pro).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }
    }
}