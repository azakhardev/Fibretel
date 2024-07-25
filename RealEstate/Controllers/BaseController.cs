using Fibretel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fibretel.Controllers
{
    public class BaseController : Controller
    {
        MyDatabase myDb = new MyDatabase();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            ViewBag.Authorized = HttpContext.Session.GetString("userId") != null;

            if (ViewBag.Authorized != null && ViewBag.Authorized != false)
            {
                ViewBag.UserId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
                ViewBag.LoggedAs = myDb.Accounts.Find(ViewBag.UserId).Username;

                if (myDb.Accounts.Find(ViewBag.UserId).Superior == true)
                {
                    HttpContext.Session.SetString("superior", "true");
                    ViewBag.Superior = true;
                }
            }
        }
    }
}
