using System.Data.Entity;
using FormalisQuiz.Models;
using FormalisQuiz.Models.Interfaces;

namespace FormalisQuiz.DataLayer
{
    public class FormalisQuizContext : DbContext, IDisposedTracker
    {
        public FormalisQuizContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuizUser> QuizUsers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
            base.Dispose(disposing);
        }
    }
}