using System.Web.Mvc;
using ForlamisQuiz.UI.Filters;
using ForlamisQuiz.UI.Helpers;
using ForlamisQuiz.UI.Models;
using FormalisQuiz.DataLayer.Repositories;

namespace ForlamisQuiz.UI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly AccountRepository _accountRepository = new AccountRepository();

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
            if (SessionHelper.IsLoggedIn())
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Logout()
        {
            SessionHelper.Logout();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult LoginUser(LoginContext context)
        {
            var loggedInUser = _accountRepository.SignIn(context);
            
            if (SessionHelper.Login(loggedInUser))
            {
                return RedirectToAction("Index");
            }

            ViewBag.LoginNotCorrect = true;
            return RedirectToAction("Login");
        }
    }
}
