using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
