namespace FormalisQuiz.Models
{
    public class UserQuiz
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}