using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Filters
{
    public class AuthorizeEmployee : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {

            //Condition to check status of current session
            if (HttpContext.Current.Session["LoggedIn"] == null)
            {
                //Return to login page if the session ended
                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    {"Controller", "Login"},
                    {"Action", "Login"}
                });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}