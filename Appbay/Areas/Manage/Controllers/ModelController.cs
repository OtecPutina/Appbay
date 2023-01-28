using Microsoft.AspNetCore.Mvc;

namespace Appbay.Areas.Manage.Controllers
{
    public class ModelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
