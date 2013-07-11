namespace FormalisQuiz.Models
{
    public class Result
    {
        public int Id { get; set; }

        public int False { get; set; }

        public int True { get; set; }

        public virtual User User { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}