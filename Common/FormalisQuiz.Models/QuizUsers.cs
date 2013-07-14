using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace FormalisQuiz.Models
{
    public class QuizUser
    {
        public QuizUser()
        {
            //Date = DateTime.ParseExact("17530101", "yyyyMMdd", CultureInfo.InvariantCulture);
        }

        [Key, Column(Order = 0)]
        public int QuizId { get; set; }

        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        public int CorrectAnswers { get; set; }
        
        public DateTime? Date { get; set; }
        
        public virtual Quiz Quiz { get; set; }
        
        public virtual User User { get; set; }
    }
}