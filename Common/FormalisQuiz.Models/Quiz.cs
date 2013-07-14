using System.Collections.Generic;

namespace FormalisQuiz.Models
{
    public class Quiz
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<QuizUser> QuizUsers { get; set; }
    }
}