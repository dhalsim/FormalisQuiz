namespace FormalisQuiz.Models
{
    public class Result
    {
        public int Id { get; set; }

        public int True { get; set; }

        public int QuestionCount { get; set; }

        public virtual User User { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}