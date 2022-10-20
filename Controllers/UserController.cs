using Microsoft.AspNetCore.Mvc;
using Notes.DTOs.UserDTOs;
using Notes.Models;
using Notes.Utils;

namespace Notes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO userDTO)
        {
            try
            {
                User user = new()
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Password = userDTO.Password
                };

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