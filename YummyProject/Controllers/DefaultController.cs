using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        YummyContext context=new YummyContext();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult DefaultFeature()//tamam
        {
            var values= context.Features.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultAbout() //tamam
        {
            var values = context.Abouts.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultWhyUs() // tamam
        {
            var values = context.Services.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultStats() //tamam
        {
            ViewBag.soupCount = context.Products.Count(x => x.Category.CategoryName == "Çorbalar");
            ViewBag.mostExpensive = context.Products.OrderByDescending(x => x.Price).Select(x => x.ProductName).FirstOrDefault();
            ViewBag.avgPrice = (int)context.Products.Average(x => x.Price);
            ViewBag.cheapestPrice = context.Products.OrderBy(x => x.Price).Select(x => x.ProductName).FirstOrDefault();
            return PartialView();
        }

        public PartialViewResult DefaultProduct() //tamam
        { 
            var values=context.Categories.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultTestimonial() //tamam
        {
            var values=context.Testimonials.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultEvent() //tamam
        {
            var values=context.Events.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultChef() //tamam
        {
            var values = context.Chefs.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultBooking()  //tamam
        {
            var values=context.Bookings.ToList();
            return PartialView(values);
        }
            
        public PartialViewResult DefaultGallery() //tamam
        {
            var values= context.PhotoGalleries.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultContact() 
        {
            var values = context.ContactInfos.ToList();
            return PartialView(values);
        }

        
        [HttpPost]
        public ActionResult AddBooking(Booking booking, string date, string time)
        {

            // Tarih ve saat birleştir
            DateTime parsedDate = DateTime.Parse(date);
            TimeSpan parsedTime = TimeSpan.Parse(time);
            booking.BookingDate = parsedDate.Date + parsedTime;

            booking.IsApproved = false;
            context.Bookings.Add(booking);
            context.SaveChanges();
            return RedirectToAction("");


        }

        public ActionResult SendMessage(Message message)
        { 
            message.IsRead = false;
            context.Messages.Add(message);
            context.SaveChanges();
            return RedirectToAction("Index");
        
        
        }


    }
}