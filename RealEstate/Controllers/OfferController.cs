using Fibretel.Models;
using Fibretel.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fibretel.Controllers
{
    public class OfferController : BaseController
    {
        MyDatabase myDb = new MyDatabase();

        [HttpGet]
        public IActionResult Index(int id = 1)
        {
            List<Service> services = myDb.Services.ToList();

            ViewBag.Page = id;
            ViewBag.Services = services;
            return View();
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Service offer = myDb.Services.Find(id);
            ViewBag.Photos = myDb.Photos.Where(x => x.ServiceId == id).ToList();
            return View(offer);
        }
    }
}