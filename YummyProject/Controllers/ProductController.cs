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
        YummyContext context=new YummyContext();

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
    }
}