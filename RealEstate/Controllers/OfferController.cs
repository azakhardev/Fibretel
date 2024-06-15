using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.Models.Entities;

namespace RealEstate.Controllers
{
    public class OfferController : BaseController
    {
        MyDatabase myDb = new MyDatabase();

        public IActionResult Index()
        {
            List<Offer> offers = myDb.Offers.ToList();
            return View(offers);
        }

        public IActionResult Detail(int Id) 
        {
            Offer offer = this.myDb.Offers.Find(Id);
            ViewBag.Photos = this.myDb.Photos.Where(x => x.OfferId == Id).ToList();
            return View(offer);
        }
    }
}