using Notes.Models;

namespace Notes.Services
{
    public class FileUploadService
    {
        public static async Task Upload(IFormFile avatar, Avatar userAvatar)
        {
            string currentPath = Directory.GetCurrentDirectory();

            if (!Directory.Exists(Path.Combine(currentPath, "Temp")))
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Temp"));
            
            userAvatar.Title = $"{Guid.NewGuid()} - {avatar.FileName}";
            userAvatar.Url = Path.Combine(currentPath, "Temp", avatar.FileName);
            userAvatar.ContentType = avatar.ContentType;

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Temp", userAvatar.Title);

            using (var fs = new FileStream(imagePath, FileMode.Create))
            {
                await avatar.CopyToAsync(fs);
            }
        }
    }
}