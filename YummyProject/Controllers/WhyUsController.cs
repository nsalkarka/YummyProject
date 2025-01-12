using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class WhyUsController : Controller
    {
        YummyContext context=new YummyContext();
        public ActionResult Index()
        {
            var values= context.Services.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddWhyUs() 
        {
            return View();  
        }

        [HttpPost]  
        public ActionResult AddWhyUs(Service service)
        {
            context.Services.Add(service);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteWhyUs(int id)
        {
            var value=context.Services.Find(id);
            context.Services.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateWhyUs(int id)
        {
            var value = context.Services.Find(id);
            return View(value);
        }

        [HttpPost]  
        public ActionResult UpdateWhyUs(Service service)
        {
            var value=context.Services.Find(service.ServiceId);
            value.Title= service.Title;
            value.Description= service.Description;
            value.Icon=service.Icon;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
            
    }
}