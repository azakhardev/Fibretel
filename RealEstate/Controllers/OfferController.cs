using Fibretel.Models;
using Fibretel.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //Service service = myDb.Services.Find(id);
            Service service = myDb.Services.Include(x => x.Photos).Where(x => x.Id == id).FirstOrDefault();
            //ViewBag.Photos = myDb.Photos.Where(x => x.ServiceId == id).ToList();
            return View(service);
        }
    }
}