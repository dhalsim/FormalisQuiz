using System;
using System.Web.Mvc;
using System.Web.Routing;
using ForlamisQuiz.UI.Models;
using FormalisQuiz.Models;

namespace ForlamisQuiz.UI.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (SessionManager.UserContext == null)
            {
                SessionManager.UserContext = new UserContext
                {
                    LoggedIn = false,
                    SessionId = System.Web.HttpContext.Current.Session.SessionID
                };                
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}