using System.Linq;
using FormalisQuiz.DataLayer;
using FormalisQuiz.Models;
using FormalisQuiz.Utilities;
using NUnit.Framework;

namespace FormalisQuiz.IntegrationTests
{
    [TestFixture]
    public class DataLayerTests
    {
        [Test]
        public void Should_insert_to_user_table()
        {
            using (var db = new FormalisQuizContext())
            {
                bool exists = db.Users.FirstOrDefault(u => u.UserName == "testUser") != null;

                if (!exists)
                {
                    db.Users.Add(new User
                    {
                        Name = "test",
                        Surname = "user",
                        UserName = "testUser",
                        Password = CryptoHelper.ConvertToMd5Hash("password")
                    });

                    db.SaveChanges();                    
                }
            }
        }

        [Test]
        public void Should_create_admin_role()
        {
            using (var db = new FormalisQuizContext())
            {
                bool adminExists = db.Roles.FirstOrDefault(r => r.Name == "Admin") != null;
                if (!adminExists)
                {
                    db.Roles.Add(new Role {Name = "Admin"});
                    db.SaveChanges();
                }
            }
        }

        [Test]
        public void Should_give_admin_to_user()
        {
            Should_insert_to_user_table();
            Should_insert_to_user_table();

            using (var db = new FormalisQuizContext())
            {
                var adminRole = db.Roles.First(r => r.Name == "Admin");
                db.Users.First(u => u.UserName == "testUser").Roles.Add(adminRole);
                db.SaveChanges();
            }
        }
    }
}
