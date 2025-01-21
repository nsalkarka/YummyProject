using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    
    public class BookingController : Controller
    {
        YummyContext context=new YummyContext();
        public ActionResult Index()
        {
            var value=context.Bookings.ToList();
            return View(value);
        }

        [HttpPost]
        public ActionResult ApproveReservation(int[] approvedBookings)
        {
            try
            {
                using (var context = new YummyContext())
                {
                    // Tüm rezervasyonları al
                    var allBookings = context.Bookings.ToList();

                    // İşaretli rezervasyonları güncelle
                    foreach (var booking in allBookings)
                    {
                        // Eğer `approvedBookings` null değil ve rezervasyon checkbox işaretlenmişse
                        if (approvedBookings != null && approvedBookings.Contains(booking.BookingId))
                        {
                            booking.IsApproved = true; // Onayla
                        }
                        else
                        {
                            booking.IsApproved = false; // Onayı kaldır
                        }
                    }

                    context.SaveChanges(); // Veritabanına kaydet
                }


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
        public ActionResult ShowApproved()
        {
            var bookings = context.Bookings.Where(x => x.IsApproved == true).ToList();
            return View(bookings);
        }
        public ActionResult ShowUnApproved()
        {
            var bookings = context.Bookings.Where(x => x.IsApproved == false).ToList();
            return View(bookings);
        }


    }
}