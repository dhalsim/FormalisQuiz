using System.Linq;
using ForlamisQuiz.UI.Models;
using FormalisQuiz.Models;
using FormalisQuiz.Utilities;

namespace FormalisQuiz.DataLayer.Repositories
{
    public class AccountRepository : RepositoryBase<FormalisQuizContext>
    {
        public User GetUserWithRoleIncluded(string username)
        {
            using (DataContext)
            {
                return DataContext.Users
                    .Include("Roles")
                    .FirstOrDefault(u => u.UserName == username);
            }
        }

        public User SignIn(LoginContext context)
        {
            var user = GetUserWithRoleIncluded(context.UserName);

            if (user == null)
            {
                return null;
            }

            if (CryptoHelper.ConvertToMd5Hash(context.Password) == user.Password)
            {
                return user;
            }

            return null;
        }
    }
}