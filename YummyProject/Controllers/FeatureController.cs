using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class FeatureController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            var values = context.Features.ToList();
            return View(values);
        }

        public ActionResult AddFeature()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFeature(Feature feature)
        {

            if (feature.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName = Path.Combine(saveLocation, feature.ImageFile.FileName);
                feature.ImageFile.SaveAs(fileName);
                feature.ImageUrl = "/images/" + feature.ImageFile.FileName;


            }

            if (!ModelState.IsValid)
            {
                return View(feature);
            }

            context.Features.Add(feature);
            var result=context.SaveChanges();
            if (result == 0)
            {
                ViewBag.error = "Değerler kaydedilirken bir hata ile karşılaşıldı";
                return View(feature);
            }
            return RedirectToAction("Index");

        }

        public ActionResult DeleteFeature(int id)
        {
            var value = context.Features.Find(id);
            context.Features.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateFeature(int id)
        {
            var values = context.Features.Find(id);
            
            return View(values);
        }

        [HttpPost]
        public ActionResult UpdateFeature(Feature feature) 
        {

            
            if (!ModelState.IsValid)
            {
                return View(feature);
            }


            var values= context.Features.Find(feature.FeatureId);
           

            if (feature.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName = Path.Combine(saveLocation, feature.ImageFile.FileName);
                feature.ImageFile.SaveAs(fileName);
                feature.ImageUrl = "/images/" + feature.ImageFile.FileName;


            }

            values.Title = feature.Title;
            values.Description = feature.Description;
            values.VideoUrl = feature.VideoUrl;
            values.ImageUrl = feature.ImageUrl;
            
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}