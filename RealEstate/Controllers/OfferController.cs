using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    public class OfferController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
