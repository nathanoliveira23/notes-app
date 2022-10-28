using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Notes.DTOs.UserDTOs;
using Notes.Repository;
using Notes.Utils;
using Notes.Models;
using Notes.Services;

namespace Notes.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public LoginController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult UserLogin(UserLoginDTO userLoginDTO)
        {
            try
            {
                User userLogin = _userRepository.GetUserLogin(userLoginDTO.Email.ToLower(),
                                                            Encrypt.EncryptPassword(userLoginDTO.Password));

                return Ok(new UserLoginResponseDTO(userLogin.Email, userLogin.Password, JWTTokenService.CreateToken(userLogin)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro durante o login de usuário: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new AppError()
                {
                    Message = "Ocorreu um erro ao efetuar o login de usuário.",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}