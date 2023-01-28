using Appbay.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Appbay.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

       
    }
}