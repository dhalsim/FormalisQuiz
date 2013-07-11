namespace FormalisQuiz.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public virtual Question Question { get; set; }
    }
}