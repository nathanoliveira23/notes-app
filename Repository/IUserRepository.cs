using Notes.Models;

namespace Notes.Repository
{
    public interface IUserRepository
    {
        public void SaveUser(User user);
    }
}