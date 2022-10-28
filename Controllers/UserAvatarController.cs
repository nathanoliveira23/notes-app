using Microsoft.AspNetCore.Mvc;
using Notes.Models;
using Notes.Repository;

namespace Notes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAvatarController : BaseController
    {
        public UserAvatarController(IUserRepository userRepository) : base(userRepository) { }

        [HttpPatch("upload")]
        public async Task<IActionResult> AvatarUpload(IFormFile avatar)
        {
            // Upload file
            string currentPath = Directory.GetCurrentDirectory();

            if (!Directory.Exists(Path.Combine(currentPath, "Temp")))
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Temp"));

            Avatar userAvatar = new()
            {
                Title = $"{Guid.NewGuid()} - {avatar.FileName}",
                Url = Path.Combine(currentPath, "Temp", avatar.FileName),
                ContentType = avatar.ContentType
            };

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Temp", userAvatar.Title);

            using (var fs = new FileStream(imagePath, FileMode.Create))
            {
                await avatar.CopyToAsync(fs);
            }


            // Save the image name in the database
            User dbUser = ReadToken();

            dbUser.Name = dbUser.Name;
            dbUser.Email = dbUser.Email;
            dbUser.Avatar = userAvatar.Title;
            dbUser.Password = dbUser.Password;

            await _userRepository.UpdateAsync(dbUser);

            return NoContent();
        }
    }
}