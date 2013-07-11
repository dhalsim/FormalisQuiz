using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormalisQuiz.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Display(Name = "Soyisim")]
        public string Surname { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}