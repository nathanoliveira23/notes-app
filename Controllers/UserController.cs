using Microsoft.AspNetCore.Mvc;
using Notes.DTOs.UserDTOs;
using Notes.Models;
using Notes.Repository;
using Notes.Utils;
using Microsoft.AspNetCore.Authorization;

namespace Notes.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository) : base(userRepository)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserDTO userDTO)
        {
            try
            {
                if (userDTO.Email.Contains(" "))
                    return BadRequest(new AppError("O E-mail não pode ter espaços em branco."));

                User user = new()
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email.ToLower(),
                    Password = Encrypt.EncryptPassword(userDTO.Password)
                };

                if (!_userRepository.EmailVerify(userDTO.Email))
                {
                    await _userRepository.SaveUserAsync(user);
                }
                else
                {
                    return BadRequest(new AppError("O E-mail informado já existe."));
                }

                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
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

        [HttpGet()]
        public IActionResult GetUserById()
        {
            try
            {
                User userId = ReadToken();
            
                return userId != null 
                    ? Ok(userId)
                    : BadRequest(new AppError("Usuário não encontrado."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro durante a busca do usuário: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new AppError()
                {
                    Message = "Ocorreu um erro ao buscar o usuário",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                User userId = ReadToken();

                if (userId != null)
                {
                    await _userRepository.DeleteAsync(userId);
                }
                else
                {
                    NotFound(new AppError("Usuário não encontrado."));
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro durante a remoção do usuário: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new AppError()
                {
                    Message = "Ocorreu um erro ao deletar o usuário",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO userUpdateDTO, int id)
        {
            try
            {
                User dbUserId = ReadToken();

                if (dbUserId == null)
                    return NotFound(new AppError("Usuário não encontrado."));

                dbUserId.Name = userUpdateDTO.Name;
                dbUserId.Email = userUpdateDTO.Email;
                dbUserId.UpdatedAt = DateTime.Now;

                if (userUpdateDTO.Password == userUpdateDTO.NewPassword)
                    return BadRequest(new AppError("A nova senha não pode ser igual a anterior."));
                
                if (userUpdateDTO.Password != dbUserId.Password)
                    return BadRequest(new AppError("A senha atual informada é diferente da que consta na base de dados."));

                dbUserId.Password = Encrypt.EncryptPassword(userUpdateDTO.NewPassword);

                await _userRepository.UpdateAsync(dbUserId);

                return Ok(dbUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro durante a atualização do usuário: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new AppError()
                {
                    Message = "Ocorreu um erro ao atualizar o usuário.",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}