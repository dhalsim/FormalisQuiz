using System.Web.Mvc;
using ForlamisQuiz.UI.Helpers;

namespace ForlamisQuiz.UI.Filters
{
    public class AdminAuthorizationAttribute : BaseRedirectAttribute
    {
        public AdminAuthorizationAttribute(string redirectToAction, string redirectToController) 
            : base(redirectToAction, redirectToController) { }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool authorized = SessionHelper.IsAdmin();
            RedirectToResult(filterContext, authorized);

            base.OnActionExecuting(filterContext);
        }
    }
}