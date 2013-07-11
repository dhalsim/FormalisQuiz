using System.Web.Mvc;
using ForlamisQuiz.UI.Helpers;

namespace ForlamisQuiz.UI.Filters
{
    public class UserAuthorizationAttribute : BaseRedirectAttribute
    {
        public UserAuthorizationAttribute(string redirectToAction, string redirectToController)
            : base(redirectToAction, redirectToController) { }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool authorized = SessionHelper.IsLoggedIn();
            RedirectToResult(filterContext, authorized);

            base.OnActionExecuting(filterContext);
        }
    }
}