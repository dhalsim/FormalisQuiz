using System.Data.Entity;
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
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            //Database.SetInitializer<FormalisQuizContext>(null);
        }

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
        public void Should_insert_student_user()
        {
            using (var db = new FormalisQuizContext())
            {
                bool exists = db.Users.FirstOrDefault(u => u.UserName == "student1") != null;

                if (!exists)
                {
                    db.Users.Add(new User
                    {
                        Name = "Can",
                        Surname = "Kemal",
                        UserName = "student1",
                        Password = CryptoHelper.ConvertToMd5Hash("password")
                    });

                    db.SaveChanges();
                }
            }
        }

        [Test]
        public void Should_create_admin_role_to_user()
        {
            Should_insert_to_user_table();

            using (var db = new FormalisQuizContext())
            {
                var adminUser = db.Users
                    .Include("Roles")
                    .FirstOrDefault(u => u.UserName == "testUser");

                if (adminUser != null && adminUser.Roles == null)
                {
                    db.Roles.Add(new Role { Name = "Admin", User = adminUser });
                    db.SaveChanges();
                }
            }
        }
    }
}
