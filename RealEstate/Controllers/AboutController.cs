using Microsoft.AspNetCore.Mvc;

namespace Fibretel.Controllers
{
    public class AboutController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
