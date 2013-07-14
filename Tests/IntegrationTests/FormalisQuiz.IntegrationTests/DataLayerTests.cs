using System.Collections.Generic;
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

                if (adminUser != null && adminUser.Roles.Count == 0)
                {
                    db.Roles.Add(new Role { Name = "Admin", User = adminUser });
                    db.SaveChanges();
                }
            }
        }

        [Test]
        public void Should_insert_quiz()
        {
            using (var db = new FormalisQuizContext())
            {
                bool isExist = db.Quizzes.FirstOrDefault(quiz => quiz.Name == "TestQuiz") != null;
                if (!isExist)
                {
                    db.Quizzes.Add(new Quiz { Name = "TestQuiz" });
                }

                db.SaveChanges();
            }
        }

        [Test]
        public void Should_insert_to_questions()
        {
            Should_insert_quiz();

            using (var db = new FormalisQuizContext())
            {
                var quiz = db.Quizzes.Include("Questions").FirstOrDefault(q => q.Name == "TestQuiz");
                bool isExist = quiz != null;
                
                if (isExist && quiz.Questions.Count == 0)
                {
                    var question = new Question
                                       {
                                           Quiz = quiz,
                                           Text = "Soru 1"
                                       };

                    question.Answers = new List<Answer>(4);
                    question.Answers.Add(new Answer { Question = question, Text = "CevA", IsCorrect = false });
                    question.Answers.Add(new Answer { Question = question, Text = "CevB", IsCorrect = false });
                    question.Answers.Add(new Answer { Question = question, Text = "CevC", IsCorrect = true });
                    question.Answers.Add(new Answer { Question = question, Text = "CevD", IsCorrect = false });

                    quiz.Questions.Add(question);

                    question = new Question
                    {
                        Quiz = quiz,
                        Text = "Soru 2"
                    };

                    question.Answers = new List<Answer>(4);
                    question.Answers.Add(new Answer { Question = question, Text = "A", IsCorrect = false });
                    question.Answers.Add(new Answer { Question = question, Text = "B", IsCorrect = true });
                    question.Answers.Add(new Answer { Question = question, Text = "C", IsCorrect = false });
                    question.Answers.Add(new Answer { Question = question, Text = "D", IsCorrect = false });

                    quiz.Questions.Add(question);

                    db.SaveChanges();
                }
            }
        }

        [Test]
        public void Should_match_student_to_quiz()
        {
            Should_insert_student_user();
            Should_insert_quiz();

            using (var db = new FormalisQuizContext())
            {
                var quiz = db.Quizzes.Include("QuizUsers").FirstOrDefault(q => q.Name == "TestQuiz");
                var student = db.Users.Include("QuizUsers").FirstOrDefault(u => u.UserName == "student1");

                if (quiz != null && student != null && student.QuizUsers.Count == 0)
                {
                    quiz.QuizUsers.Add(new QuizUser{User = student, Quiz = quiz});

                    db.SaveChanges();
                }
            }
        }


        [Test]
        public void Should_select_user_quiz_matching_table()
        {
            Should_match_student_to_quiz();

            using (var db = new FormalisQuizContext())
            {
                var quizUsers = db.QuizUsers.ToList();
                Assert.AreNotEqual(null, quizUsers);
            }
        }
    }
}
