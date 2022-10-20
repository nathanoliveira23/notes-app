using Microsoft.AspNetCore.Mvc;
using Notes.DTOs.UserDTOs;
using Notes.Models;
using Notes.Repository;
using Notes.Utils;

namespace Notes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO userDTO)
        {
            try
            {
                if (userDTO.Email.Contains(" "))
                    return BadRequest(new AppError()
                    {
                        Message = "O E-mail não pode ter espaços em branco.",
                        Status = StatusCodes.Status400BadRequest
                    });

                User user = new()
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email.ToLower(),
                    Password = Encrypt.EncryptPassword(userDTO.Password)
                };

                if (!_userRepository.EmailVerify(userDTO.Email))
                {
                    _userRepository.SaveUser(user);
                }
                else
                {
                    return BadRequest(new AppError()
                    {
                        Message = "O E-mail informado já existe.",
                        Status = StatusCodes.Status400BadRequest
                    });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro durante o cadastro de usuário: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new AppError()
                {
                    Message = "Ocorreu um erro ao cadastrar o usuário",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}