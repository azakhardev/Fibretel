using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    public class BaseController : Controller
    {
        MyDatabase myDb = new MyDatabase();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            this.ViewBag.Authorized = this.HttpContext.Session.GetString("userId") != null;

            if (this.ViewBag.Authorized != null && this.ViewBag.Authorized != false)
            {
                this.ViewBag.UserId = Convert.ToInt32(this.HttpContext.Session.GetString("userId"));
                this.ViewBag.LoggedAs = myDb.Accounts.Find(this.ViewBag.UserId).Username;
                this.ViewBag.Superior = myDb.Accounts.Find(this.ViewBag.UserId).Superior;
            }
        }
    }
}
