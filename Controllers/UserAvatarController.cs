using Microsoft.AspNetCore.Mvc;
using Notes.Models;
using Notes.Repository;
using Notes.Services;
using Notes.Utils;

namespace Notes.Controllers
{
    [ApiController]
    [Route("api/avatar")]
    public class UserAvatarController : BaseController
    {
        private readonly ILogger<UserAvatarController> _logger;
        public UserAvatarController(IUserRepository userRepository, ILogger<UserAvatarController> logger) : base(userRepository) 
        {
            _logger = logger;
        }

        [HttpPatch("upload")]
        public async Task<IActionResult> AvatarUpload(IFormFile avatar)
        {
            try
            {
                // Upload file
                Avatar userAvatar = new();

                await FileUploadService.Upload(avatar, userAvatar);

                // Save the image name in the database
                User dbUser = ReadToken();
                dbUser.Avatar = userAvatar.Title;

                await _userRepository.UpdateAsync(dbUser);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro durante atualização da foto de usuário: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new AppError()
                {
                    Message = "Ocorreu um erro ao atualizar a foto de usuário.",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}