using Notes.Data;
using Notes.Models;

namespace Notes.Repository.Implements
{
  public class TagsRepository : ITagsRepository
  {
    private readonly NotesDbContext _context;
    public TagsRepository(NotesDbContext context) => _context = context;
    public async Task SaveTag(Tag tag)
    {
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
    }
  }
}