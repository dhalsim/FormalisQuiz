using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Parola")]
        public string Password { get; set; }

        public virtual ICollection<QuizUser> QuizUsers { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}