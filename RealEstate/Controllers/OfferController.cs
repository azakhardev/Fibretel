using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.Models.Entities;

namespace RealEstate.Controllers
{
    public class OfferController : BaseController
    {
        MyDatabase myDb = new MyDatabase();

        public IActionResult Index(int id = 1)
        {
            List<Offer> offers = myDb.Offers.ToList();
            ViewBag.Page = id;
            return View(offers);
        }

        public IActionResult Detail(int id) 
        {
            Offer offer = this.myDb.Offers.Find(id);
            ViewBag.Photos = this.myDb.Photos.Where(x => x.OfferId == id).ToList();
            return View(offer);
        }
    }
}