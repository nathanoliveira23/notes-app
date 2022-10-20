using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.UserDTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "É necessário informar o nome de usuário.")]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required(ErrorMessage = "É necessário informar o email de usuário.")]
        [EmailAddress(ErrorMessage = "É necessário informar um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "É necessário informar a senha de usuário.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Password { get; set; }
    }
}