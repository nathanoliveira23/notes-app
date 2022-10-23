namespace Notes.DTOs.UserDTOs
{
    public class UserLoginResponseDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        
        public UserLoginResponseDTO(string email, string password, string token)
        {
            Email = email;
            Password = password;
            Token = token;
        }
    }
}