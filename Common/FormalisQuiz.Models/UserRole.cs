namespace FormalisQuiz.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}