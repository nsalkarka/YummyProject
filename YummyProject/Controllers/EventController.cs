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
    public class EventController : Controller
    {
        YummyContext context= new YummyContext();
        public ActionResult Index()
        {
            var value = context.Events.ToList();
            return View(value);
        }

        public ActionResult AddEvent()
        {
            return View();
        }

       

        [HttpPost]
        public ActionResult AddEvent(Event @event)
        {

            if (@event.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = Path.Combine(currentDirectory, "images", "event");
                var fileName = Path.Combine(saveLocation, @event.ImageFile.FileName);
                @event.ImageFile.SaveAs(fileName);
                @event.ImageUrl = "/images/event/" + @event.ImageFile.FileName;


            }

            if (!ModelState.IsValid)
            {
                return View(@event);
            }

            context.Events.Add(@event);
            var result = context.SaveChanges();
            if (result == 0)
            {
                ViewBag.error = "Değerler kaydedilirken bir hata ile karşılaşıldı";
                return View(@event);
            }
            return RedirectToAction("Index");

        }

        public ActionResult DeleteEvent(int id)
        {
            var value = context.Events.Find(id);
            context.Events.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateEvent(int id)
        {
            var values = context.Events.Find(id);

            return View(values);
        }

        [HttpPost]
        public ActionResult UpdateEvent(Event @event)
        {


            if (!ModelState.IsValid)
            {
                return View(@event);
            }


            var values = context.Events.Find(@event.EventId);


            if (@event.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = Path.Combine(currentDirectory, "images", "event");
                var fileName = Path.Combine(saveLocation, @event.ImageFile.FileName);
                @event.ImageFile.SaveAs(fileName);
                @event.ImageUrl = "/images/event/" + @event.ImageFile.FileName;


            }

            values.Title = @event.Title;
            values.Price = @event.Price;
            values.Description = @event.Description;
            values.ImageUrl = @event.ImageUrl;
            


            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}