using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.Models.Entities;

namespace RealEstate.Controllers
{
    public class ContactsController : Controller
    {
        MyDatabase myDb = new MyDatabase();
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Services = myDb.Services.Select(x => x.Name).ToList();
            return View(new Request());
        }

        [HttpPost]
        public IActionResult Index(Request request)
        {
            myDb.Requests.Add(request);
            myDb.Services.Where(x => x.Name == request.Name).FirstOrDefault().Requests += 1;
            myDb.SaveChanges();
            return RedirectToAction("Success");
        }
        [HttpGet]
        public IActionResult Success() 
        {
            return View();
        }
    }
}
