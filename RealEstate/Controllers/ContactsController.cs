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
            request.SentAt = DateTime.Now;
            request.Answered = false;
            myDb.Requests.Add(request);

            if (request.Service != "Jiné")
                myDb.Services.Where(x => x.Name == request.Service).FirstOrDefault().Requests += 1;

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
