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
    public class TestimonialController : Controller
    {
        YummyContext context=new YummyContext();
        public ActionResult Index()
        {
            var value=context.Testimonials.ToList();
            return View(value);
        }

        public ActionResult AddTestimonial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTestimonial(Testimonial testimonial)
        {

            if (testimonial.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = Path.Combine(currentDirectory, "images", "testimonial");
                var fileName = Path.Combine(saveLocation, testimonial.ImageFile.FileName);
                testimonial.ImageFile.SaveAs(fileName);
                testimonial.ImageUrl = "/images/testimonial/" + testimonial.ImageFile.FileName;


            }

            if (!ModelState.IsValid)
            {
                return View(testimonial);
            }

            context.Testimonials.Add(testimonial);
            var result = context.SaveChanges();
            if (result == 0)
            {
                ViewBag.error = "Değerler kaydedilirken bir hata ile karşılaşıldı";
                return View(testimonial);
            }
            return RedirectToAction("Index");

        }

        public ActionResult DeleteTestimonial(int id)
        {
            var value = context.Testimonials.Find(id);
            context.Testimonials.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateTestimonial(int id)
        {
            var values = context.Testimonials.Find(id);

            return View(values);
        }

        [HttpPost]
        public ActionResult UpdateTestimonial(Testimonial testimonial)
        {


            if (!ModelState.IsValid)
            {
                return View(testimonial);
            }


            var values = context.Testimonials.Find(testimonial.TestimonialId);


            if (testimonial.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = Path.Combine(currentDirectory, "images", "testimonial");
                var fileName = Path.Combine(saveLocation, testimonial.ImageFile.FileName);
                testimonial.ImageFile.SaveAs(fileName);
                testimonial.ImageUrl = "/images/testimonial/" + testimonial.ImageFile.FileName;

            }

            values.Title = testimonial.Title;
            values.Rating = testimonial.Rating;
            values.ImageUrl = testimonial.ImageUrl;
            values.Comment = testimonial.Comment;
            values.NameSurname     = testimonial.NameSurname;
        

            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}