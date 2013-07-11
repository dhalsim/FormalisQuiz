using System.Web.Mvc;
using ForlamisQuiz.UI.Filters;
using ForlamisQuiz.UI.Helpers;
using ForlamisQuiz.UI.Models;
using FormalisQuiz.DataLayer;

namespace ForlamisQuiz.UI.Controllers
{
    public class AccountController : BaseController
    {
        [UserAuthorization("Login", "Account")]
        public ActionResult Index()
        {
            return View("Index", new AccountIndexModel
                                     {
                                         IsAdmin = SessionHelper.IsAdmin(),
                                         Name = SessionManager.UserContext.User.Name,
                                         Surname = SessionManager.UserContext.User.Surname
                                     });
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(LoginContext context)
        {
            AccountOperations accountOperations = new AccountOperations();
            var loggedInUser = accountOperations.SignIn(context);
            
            if (SessionHelper.Login(loggedInUser))
            {
                return RedirectToAction("Index");
            }

            ViewBag.LoginNotCorrect = true;
            return RedirectToAction("Login");
        }
    }
}
