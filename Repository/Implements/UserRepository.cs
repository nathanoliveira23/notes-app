using Notes.Data;
using Notes.Models;

namespace Notes.Repository.Implements
{
  public class UserRepository : IUserRepository
  {
    private readonly NotesDbContext _context;

    public UserRepository(NotesDbContext context) => _context = context;

    public async void SaveUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
  }
}