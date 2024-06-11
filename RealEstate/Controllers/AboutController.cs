using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    public class AboutController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
