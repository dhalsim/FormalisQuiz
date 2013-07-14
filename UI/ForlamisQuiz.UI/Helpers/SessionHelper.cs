using System.Linq;
using ForlamisQuiz.UI.Models;
using FormalisQuiz.Models;

namespace ForlamisQuiz.UI.Helpers
{
    public class SessionHelper
    {
        public static bool IsAdmin()
        {
            return SessionManager.UserContext.LoggedIn
                   && SessionManager.UserContext.User.Roles.FirstOrDefault(r => r.Name == "Admin") != null;
        }

        public static bool Login(User user)
        {
            if (user != null)
            {
                SessionManager.UserContext.LoggedIn = true;
                SessionManager.UserContext.User = user;

                return true;
            }

            return false;
        }

        public static void Logout()
        {
            SessionManager.UserContext.LoggedIn = false;
            SessionManager.UserContext.User = null;
        }

        public static bool IsLoggedIn()
        {
            return SessionManager.UserContext.LoggedIn;
        }
    }
}