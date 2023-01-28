using Appbay.Context;
using Appbay.Models;
using Appbay.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Appbay.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ModelController : Controller
    {
        public ModelController(AppDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment) 
        {
            _context = context;
            _env = environment;
        }

        public AppDbContext _context { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment _env { get; }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Models = _context.Models.ToList()
            };
            return View(homeViewModel);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Model model)
        {
            if (model.ImageFile.ContentType!="image/png"&&model.ImageFile.ContentType!="image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "You must upload only png or jpeg files");
                return View();
            }
            if (model.ImageFile.Length> 2097152)
            {
                ModelState.AddModelError("ImageFile", "You must upload only files under 2mb");
                return View();
            }
            if (!ModelState.IsValid) return NotFound();
            string filename = model.ImageFile.FileName;
            if (filename.Length>64)
            {
                filename = filename.Substring(filename.Length - 64, 64);
            }
            filename=Guid.NewGuid().ToString()+filename;
            string path = Path.Combine(_env.WebRootPath, "uploads/models", filename);
            using (FileStream stream = new FileStream(path,FileMode.Create))
            {
                model.ImageFile.CopyTo(stream);
            }
            model.ImageUrl= filename;
            _context.Models.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Model model=_context.Models.FirstOrDefault(x => x.Id==id);
            if(model is null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(Model model)
        {
            Model existModel = _context.Models.Find(model.Id);
            if (existModel is null) return NotFound();
            if (model.ImageFile is not null)
            {
                if (model.ImageFile.ContentType != "image/png" && model.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "You must upload only png or jpeg files");
                    return View();
                }
                if (model.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "You must upload only files under 2mb");
                    return View();
                }
                string deletePath = Path.Combine(_env.WebRootPath, "uploads/models", existModel.ImageUrl);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                string filename = model.ImageFile.FileName;
                if (filename.Length > 64)
                {
                    filename = filename.Substring(filename.Length - 64, 64);
                }
                filename = Guid.NewGuid().ToString() + filename;
                string path = Path.Combine(_env.WebRootPath, "uploads/models", filename);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }
                existModel.ImageUrl = filename;
                    
            }
            existModel.Name = model.Name;
            existModel.Description = model.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Model model=_context.Models.FirstOrDefault(x => x.Id == id);
            if(model is null) return NotFound();
            string deletePath = Path.Combine(_env.WebRootPath, "uploads/models", model.ImageUrl);
            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }
            _context.Models.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
