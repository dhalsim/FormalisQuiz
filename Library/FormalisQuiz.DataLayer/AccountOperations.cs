using System.Linq;
using ForlamisQuiz.UI.Models;
using FormalisQuiz.Models;
using FormalisQuiz.Utilities;

namespace FormalisQuiz.DataLayer
{
    public class AccountOperations
    {
        public User SignIn(LoginContext context)
        {
            using (var dbContext = new FormalisQuizContext(false))
            {
                var user = dbContext.Users.FirstOrDefault(u => u.UserName == context.UserName);

                if (user == null)
                {
                    return null;
                }

                if (CryptoHelper.ConvertToMd5Hash(context.Password) == user.Password)
                {
                    return user;
                }
            }

            return null;
        }

        public Role GetAdminRole()
        {
            using (var dbContext = new FormalisQuizContext())
            {
                return dbContext.Roles.FirstOrDefault(role => role.Name == "Admin");
            }
        }
    }
}