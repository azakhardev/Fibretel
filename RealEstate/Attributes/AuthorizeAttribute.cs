using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Attributes
{
    public class AuthorizeAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = (Controller)context.Controller;

            if (controller.HttpContext.Session.GetString("userId") == null)
            {
                string c = controller.Request.RouteValues["controller"].ToString();
                string a = controller.Request.RouteValues["action"].ToString();                

                context.Result = new RedirectToActionResult("Index", "Login", new { c, a });
            }
        }
    }
}
