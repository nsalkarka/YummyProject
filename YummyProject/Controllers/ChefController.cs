using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class ChefController : Controller
    {
        YummyContext context=new YummyContext();
        public ActionResult Index()
        {
            var value= context.Chefs.ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult AddChef()
        {
            return View();  
        }


        [HttpPost]
        public ActionResult AddChef(Chef chef)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Chefs.Add(chef);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(chef); // Model doğrulama hatalarını View'e iletin
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Veritabanı hatası: " + ex.Message);
                return View(chef);
            }
        }

        public ActionResult DeleteChef(int id)
        {
            var value = context.Chefs.Find(id);
           
            context.Chefs.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult UpdateChef(int id)
        {
            var chef = context.Chefs.Include(c=>c.ChefSocials).FirstOrDefault(c => c.ChefId == id);
            if (chef == null)
            {
                return HttpNotFound();
            }
            return View(chef);
        }

        [HttpPost]
        public ActionResult UpdateChef(Chef chef)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Chef existingChef = context.Chefs.Include(c=> c.ChefSocials).FirstOrDefault(c => c.ChefId == chef.ChefId);

                    if (existingChef == null)
                    {
                        return HttpNotFound();
                    }

                    existingChef.Name = chef.Name;
                    existingChef.Title = chef.Title;
                    existingChef.ImageUrl = chef.ImageUrl;
                    existingChef.Description = chef.Description;

                    context.ChefSocials.RemoveRange(existingChef.ChefSocials);
                    existingChef.ChefSocials = chef.ChefSocials;

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(chef);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Veritabanı hatası: " + ex.Message);
                return View(chef);
            }
        }


    }
}