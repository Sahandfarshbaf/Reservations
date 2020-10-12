using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Reservation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Speakers()
        {
            return View();
        }

        public IActionResult VerifyPayment()
        {
            return View();
        }

        public IActionResult Ticketdetails()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
