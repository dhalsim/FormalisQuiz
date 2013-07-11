using System.Data.Entity;
using FormalisQuiz.Models;

namespace FormalisQuiz.DataLayer
{
    public class FormalisQuizContext : DbContext
    {
        public FormalisQuizContext()
        {
            
        }

        public FormalisQuizContext(bool lazy = true)
        {
            Configuration.LazyLoadingEnabled = lazy;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }
        
        public DbSet<Role> Roles { get; set; }
    }
}