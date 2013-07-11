using System.ComponentModel.DataAnnotations.Schema;

namespace FormalisQuiz.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}