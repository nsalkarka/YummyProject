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
    public class AboutController : Controller
    {
        YummyContext context=new YummyContext();
        public ActionResult Index()
        {
            var values=context.Abouts.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddAbout() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbout(About about) 
        {

            if (about.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName = Path.Combine(saveLocation, about.ImageFile.FileName);
                about.ImageFile.SaveAs(fileName);
                about.ImageUrl = "/images/" + about.ImageFile.FileName;


            }

            if (about.ImageFile2 != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName = Path.Combine(saveLocation, about.ImageFile2.FileName);
                about.ImageFile2.SaveAs(fileName);
                about.ImageUrl2 = "/images/" + about.ImageFile2.FileName;


            }

            if (!ModelState.IsValid)
            {
                return View(about);
            }

            context.Abouts.Add(about);
            var result=context.SaveChanges();

            if (result == 0)
            {
                ViewBag.error = "Değerler kaydedilirken bir hata ile karşılaşıldı";
                return View(about);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteAbout(int id)
        {
            var values= context.Abouts.Find(id);
            context.Abouts.Remove(values);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateAbout(int id) 
        {
            var values = context.Abouts.Find(id);
            return View(values);
        }

        [HttpPost]
        public ActionResult UpdateAbout(About about)
        {
            if (!ModelState.IsValid)
            {
                return View(about);
            }

            var values=context.Abouts.Find(about.AboutId);

            if (about.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName = Path.Combine(saveLocation, about.ImageFile.FileName);
                about.ImageFile.SaveAs(fileName);
                about.ImageUrl = "/images/" + about.ImageFile.FileName;


            }

            if (about.ImageFile2 != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName = Path.Combine(saveLocation, about.ImageFile2.FileName);
                about.ImageFile2.SaveAs(fileName);
                about.ImageUrl2 = "/images/" + about.ImageFile2.FileName;


            }


            values.PhoneNumber = about.PhoneNumber;
            values.Item1 = about.Item1;
            values.Item2 = about.Item2;
            values.Item3 = about.Item3;
            values.Description = about.Description;
            values.ImageUrl = about.ImageUrl;
            values.ImageUrl2 = about.ImageUrl2;
            values.VideoUrl = about.VideoUrl;
            

            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}