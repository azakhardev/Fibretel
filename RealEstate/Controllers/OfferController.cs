using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.Models;
using RealEstate.Models.Entities;

namespace RealEstate.Controllers
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
            Service offer = this.myDb.Services.Find(id);
            ViewBag.Photos = this.myDb.Photos.Where(x => x.ServiceId == id).ToList();
            return View(offer);
        }        
    }
}