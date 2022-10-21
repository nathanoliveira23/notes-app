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
        public IActionResult CreateUser(UserDTO userDTO)
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
                    _userRepository.SaveUser(user);
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

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var userId = _userRepository.GetUserId(id);
            
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

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                User userId = _userRepository.GetUserId(id);

                if (userId != null)
                {
                    _userRepository.Delete(userId);
                }
                else
                {
                    BadRequest(new AppError("Usuário não encontrado."));
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

        [HttpPut("update/{id}")]
        public IActionResult UpdateUser(UserUpdateDTO userUpdateDTO, int id)
        {
            try
            {
                User dbUserId = _userRepository.GetUserId(id);

                if (dbUserId == null)
                    return BadRequest(new AppError("Usuário não encontrado."));

                dbUserId.Name = userUpdateDTO.Name;
                dbUserId.Email = userUpdateDTO.Email;
                dbUserId.UpdatedAt = DateTime.Now;

                if (userUpdateDTO.Password == userUpdateDTO.NewPassword)
                    return BadRequest(new AppError("A nova senha não pode ser igual a anterior."));
                
                if (userUpdateDTO.Password != dbUserId.Password)
                    return BadRequest(new AppError("A senha atual informada é diferente da que consta na base de dados."));

                dbUserId.Password = Encrypt.EncryptPassword(userUpdateDTO.NewPassword);

                _userRepository.Update(dbUserId);

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