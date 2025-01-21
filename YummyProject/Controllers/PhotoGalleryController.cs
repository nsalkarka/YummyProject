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
    public class PhotoGalleryController : Controller
    {
        YummyContext context=new YummyContext();
        public ActionResult Index()
        {
            var value= context.PhotoGalleries.ToList();
            return View(value);
        }

        public ActionResult AddPhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(PhotoGallery photoGallery)
        {

            if (photoGallery.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = Path.Combine(currentDirectory, "images", "gallery");
                var fileName = Path.Combine(saveLocation, photoGallery.ImageFile.FileName);
                photoGallery.ImageFile.SaveAs(fileName);
                photoGallery.ImageUrl = "/images/gallery/" + photoGallery.ImageFile.FileName;


            }

            if (!ModelState.IsValid)
            {
                return View(photoGallery);
            }

            context.PhotoGalleries.Add(photoGallery);
            var result = context.SaveChanges();
            if (result == 0)
            {
                ViewBag.error = "Değerler kaydedilirken bir hata ile karşılaşıldı";
                return View(photoGallery);
            }
            return RedirectToAction("Index");

        }

        public ActionResult DeletePhoto(int id)
        {
            var value = context.PhotoGalleries.Find(id);
            context.PhotoGalleries.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdatePhoto(int id)
        {
            var values = context.PhotoGalleries.Find(id);

            return View(values);
        }

        [HttpPost]
        public ActionResult UpdatePhoto(PhotoGallery photoGallery)
        {


            if (!ModelState.IsValid)
            {
                return View(photoGallery);
            }


            var values = context.PhotoGalleries.Find(photoGallery.PhotoGalleryId);



            if (photoGallery.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = Path.Combine(currentDirectory, "images", "gallery");
                var fileName = Path.Combine(saveLocation, photoGallery.ImageFile.FileName);
                photoGallery.ImageFile.SaveAs(fileName);
                photoGallery.ImageUrl = "/images/gallery/" + photoGallery.ImageFile.FileName;


            }
            values.ImageUrl = photoGallery.ImageUrl;

            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}