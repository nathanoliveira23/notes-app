using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.UserDTOs
{
    public class UserUpdateDTO : UserDTO
    {
        [Required(ErrorMessage = "É necessário informar a senha de usuário.")]
        [MinLength(6, ErrorMessage = "A nova senha deve ter no mínimo 6 caracteres")]
        public string NewPassword { get; set; }
    }
}