using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class RegisterUserDto
    {

        public string Email { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string Nationality { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleId { get;} = 2;
    }
}
