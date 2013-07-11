using System.Collections.Generic;

namespace FormalisQuiz.Models
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}