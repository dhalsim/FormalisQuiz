using System.Collections.Generic;

namespace FormalisQuiz.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}