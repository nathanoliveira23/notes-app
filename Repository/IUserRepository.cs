using Notes.Models;

namespace Notes.Repository
{
    public interface IUserRepository
    {
        public void SaveUser(User user);
        public bool EmailVerify(string email);
    }
}