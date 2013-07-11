using System;
using System.Linq;
using System.Web.Mvc;
using ForlamisQuiz.UI.Models;
using FormalisQuiz.DataLayer;

namespace ForlamisQuiz.UI.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            if (SessionManager.UserContext.LoggedIn)
            {
                var user = SessionManager.UserContext.User;
                AccountIndexModel model = new AccountIndexModel
                                              {
                                                  Name = user.Name,
                                                  Surname = user.Surname
                                              };

                return View("Index", model);
            }
            
            return View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult LoginUser(LoginContext context)
        {
            AccountOperations accountOperations = new AccountOperations();
            var loggedInUser = accountOperations.SignIn(context);
            
            if (loggedInUser != null)
            {
                var adminRole = accountOperations.GetAdminRole();

                SessionManager.UserContext.LoggedIn = true;
                SessionManager.UserContext.User = loggedInUser;

                var isAdmin = loggedInUser.Roles != null;
                isAdmin = isAdmin  && loggedInUser.Roles.Contains(adminRole);

                return View("Index", new AccountIndexModel
                                         {
                                             Name = loggedInUser.Name,
                                             Surname = loggedInUser.Surname,
                                             IsAdmin = isAdmin
                                         });
            }
            
            ViewBag.LoginNotCorrect = true;
            return View("Login");
        }
    }
}
