using Notes.Models;

namespace Notes.Repository
{
    public interface IUserRepository
    {
        public Task SaveUserAsync(User user);
        public bool EmailVerify(string email);
        public User GetUserId(int id);
        public Task DeleteAsync(User user);
        public Task UpdateAsync(User user);
        public User GetUserLogin(string email, string password);
    }
}