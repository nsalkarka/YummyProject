using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class ProductController : Controller
    {
        YummyContext context = new YummyContext();

        private void CategoryDropDown()
        {
            var categoryList = context.Categories.ToList();

            List<SelectListItem> categories = (from x in categoryList
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryId.ToString()
                                               }

                                               ).ToList();
            ViewBag.categories = categories;
        }

        public ActionResult Index()
        {
            var value = context.Products.ToList();
            return View(value);

        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            CategoryDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {



            if (product.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = Path.Combine(currentDirectory, "images", "product");
                var fileName = Path.Combine(saveLocation, product.ImageFile.FileName);
                product.ImageFile.SaveAs(fileName);
                product.ImageUrl = "/images/product/" + product.ImageFile.FileName;


            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            CategoryDropDown();

            context.Products.Add(product);
            var result = context.SaveChanges();
            if (result == 0)
            {
                ViewBag.error = "Değerler kaydedilirken bir hata ile karşılaşıldı";
                return View(product);
            }
            return RedirectToAction("Index");

        }

        public ActionResult DeleteProduct(int id)
        {
            var value = context.Products.Find(id);
            context.Products.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult UpdateProduct(int id) 
        {
            CategoryDropDown();
            var value = context.Products.Find(id);
            return View(value);
        
        }

        [HttpPost]
        public ActionResult UpdateProduct (Product product)
        {
            if (product.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = Path.Combine(currentDirectory, "images", "product");
                var fileName = Path.Combine(saveLocation, product.ImageFile.FileName);
                product.ImageFile.SaveAs(fileName);
                product.ImageUrl = "/images/product/" + product.ImageFile.FileName;


            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            CategoryDropDown();
            var value = context.Products.Find(product.ProductId);
            value.ProductName= product.ProductName;
            value.ImageUrl = product.ImageUrl;
            value.Price = product.Price;
            value.CategoryId = product.CategoryId;
            value.Ingredients = product.Ingredients;

            var result = context.SaveChanges();
            if (result == 0)
            {
                ViewBag.error = "Değerler kaydedilirken bir hata ile karşılaşıldı";
                return View(product);
            }
            return RedirectToAction("Index");
        }
    }
}