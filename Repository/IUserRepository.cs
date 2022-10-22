using Notes.Models;

namespace Notes.Repository
{
    public interface IUserRepository
    {
        public void SaveUser(User user);
        public bool EmailVerify(string email);
        public User GetUserId(int id);
        public void Delete(User user);
        public void Update(User user);
        public User GetUserLogin(string email, string password);
    }
}