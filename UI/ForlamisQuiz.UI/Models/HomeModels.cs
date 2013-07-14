using System.Collections.Generic;
using FormalisQuiz.Models;

namespace ForlamisQuiz.UI.Models
{
    public class IndexModel
    {
        public IEnumerable<QuizUser> QuizUsers { get; set; }
    }
}