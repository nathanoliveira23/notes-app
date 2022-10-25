using Notes.Data;
using Notes.Models;

namespace Notes.Repository.Implements
{
  public class NotesRepository : INotesRepository
  {
    private readonly NotesDbContext _context;
    public NotesRepository(NotesDbContext context) => _context = context;
    public async Task Save(Note note)
    {
        await _context.Notes.AddAsync(note);
        await _context.SaveChangesAsync();
    }
  }
}