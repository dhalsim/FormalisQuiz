using System.Collections.Generic;
using System.Data;
using FormalisQuiz.Models;
using FormalisQuiz.Utilities;

namespace FormalisQuiz.DataLayer.Repositories
{
    public class UserRepository : RepositoryBase<FormalisQuizContext>
    {
        public IEnumerable<User> GetUsers()
        {
            return DataContext.Users;
        }

        public User Find(int userId)
        {
            using (DataContext)
            {
                return DataContext.Users.Find(userId);
            }
        }

        public void Create(User user)
        {
            using (DataContext)
            {
                DataContext.Users.Add(user);
                DataContext.SaveChanges();
            }
        }

        public void Update(User user)
        {
            using (DataContext)
            {
                // same as encrypted password
                User userOnDb = DataContext.Users.Find(user.Id);

                if (string.IsNullOrEmpty(user.Password))
                {
                    user.Password = userOnDb.Password;
                }
                else
                {
                    // encrypt password and set
                    user.Password = CryptoHelper.ConvertToMd5Hash(user.Password);
                }

                DataContext.Entry(userOnDb).CurrentValues.SetValues(user);
                DataContext.SaveChanges();
            }
        }

        public void Delete(int userId)
        {
            using (DataContext)
            {
                User user = DataContext.Users.Find(userId);
                DataContext.Users.Remove(user);
                DataContext.SaveChanges();
            }
        }
    }
}