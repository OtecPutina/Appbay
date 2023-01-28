using Appbay.Context;
using Appbay.Models;
using Appbay.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Appbay.Areas.Manage.Controllers
{
	[Area("Manage")]
	public class SettingController : Controller
	{
		public SettingController(AppDbContext context)
		{
            _context = context;
        }

        public AppDbContext _context { get; }

        public IActionResult Index()
		{
			HomeViewModel homeViewModel = new HomeViewModel
			{
				Settings = _context.Settings.ToList()
			};
			return View(homeViewModel);
		}
		[HttpGet]
		public IActionResult Update(int id)
		{
			Setting setting=_context.Settings.FirstOrDefault(x => x.Id == id);
			if (setting == null) return NotFound();
			return View(setting);
		}
		[HttpPost]
		public IActionResult Update(Setting setting)
		{
			if(!ModelState.IsValid) return View(setting);
			Setting existSetting = _context.Settings.Find(setting.Id);
			if (existSetting == null) return NotFound();
			existSetting.Value=setting.Value;
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
