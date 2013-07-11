using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormalisQuiz.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Display(Name = "Soru Metni")]
        public string Text { get; set; }

        public virtual List<Answer> Answers { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}