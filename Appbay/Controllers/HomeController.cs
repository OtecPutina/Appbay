using Appbay.Context;
using Appbay.Models;
using Appbay.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Appbay.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public AppDbContext _context { get; }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Models = _context.Models.ToList()
            };
            return View(homeViewModel);
        }

       
    }
}