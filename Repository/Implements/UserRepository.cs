using Notes.Data;
using Notes.Models;

namespace Notes.Repository.Implements
{
  public class UserRepository : IUserRepository
  {
    private readonly NotesDbContext _context;

    public UserRepository(NotesDbContext context) => _context = context;

    public async Task SaveUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public bool EmailVerify(string email)
    {
        return _context.Users.Any(x => x.Email == email);
    }

    public User GetUserId(int id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }
    
    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public User GetUserLogin(string email, string password)
    {
        return _context.Users.FirstOrDefault(user => user.Email == email && user.Password == password);
    }
  }
}