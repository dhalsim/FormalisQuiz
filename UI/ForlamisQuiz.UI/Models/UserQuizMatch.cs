using System;
using System.Collections.Generic;
using FormalisQuiz.Models;

namespace ForlamisQuiz.UI.Models
{
    public class UserQuizMatch
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string QuizName { get; set; }

        public int UserId { get; set; }

        public int QuizId { get; set; }

        public int CorrectAnswers { get; set; }

        public int QuestionCount { get; set; }

        public DateTime? Date { get; set; }

        public IList<User> Users { get; set; }

        public IList<Quiz> Quizzes { get; set; }
    }
}