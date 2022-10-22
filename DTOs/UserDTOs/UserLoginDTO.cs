using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.UserDTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "É necessário informar o email para efetuar o login")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "É necessário informar a senha para efetuar o login")]
        public string Password { get; set; }
    }
}