namespace FormalisQuiz.Models
{
    public class UserContext
    {
        public string SessionId { get; set; }

        public bool LoggedIn { get; set; }

        public User User { get; set; }
    }
}