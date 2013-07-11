using System.Web.Mvc;
using System.Web.Routing;

namespace ForlamisQuiz.UI.Filters
{
    public abstract class BaseRedirectAttribute : ActionFilterAttribute
    {
        protected string ActionName { get; set; }

        protected string ControllerName { get; set; }

        protected BaseRedirectAttribute(string redirectToAction, string redirectToController)
        {
            ActionName = redirectToAction;
            ControllerName = redirectToController;
        }

        protected void RedirectToResult(ActionExecutingContext filterContext, bool authorized)
        {
            if (!authorized)
            {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary { { "controller", ControllerName }, { "action", ActionName } });
            }
        }
    }
}