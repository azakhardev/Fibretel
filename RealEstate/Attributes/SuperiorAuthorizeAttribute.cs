using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Attributes
{
    public class SuperiorAuthorizeAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = (Controller)context.Controller;

            if (controller.HttpContext.Session.GetString("superior") == null)
            {
                string c = controller.Request.RouteValues["controller"].ToString();
                string a = controller.Request.RouteValues["action"].ToString();
                controller.HttpContext.Session.Remove("userId");

                context.Result = new RedirectToActionResult("Index", "Login", new { c, a });
            }
        }
    }
}
