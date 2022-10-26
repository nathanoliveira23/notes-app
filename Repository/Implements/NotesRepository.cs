using Notes.Data;
using Notes.Models;

namespace Notes.Repository.Implements
{
  public class NotesRepository : INotesRepository
  {
    private readonly NotesDbContext _context;
    public NotesRepository(NotesDbContext context) => _context = context;

    public async Task SaveNote(Note note)
    {
        await _context.Notes.AddAsync(note);
        await _context.SaveChangesAsync();
    }

    public Note GetNoteId(int id)
    {
        return _context.Notes.FirstOrDefault(x => x.Id == id);
    }

    public async Task RemoveNote(Note note)
    {
        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
    }
  }
}