using Notes.Data;
using Notes.Models;

namespace Notes.Repository.Implements
{
    public class LinksRepository : ILinksRepository
    {
        private readonly NotesDbContext _context;
        public LinksRepository(NotesDbContext context) => _context = context;

        public async Task SaveLink(Link link)
        {
            await _context.Links.AddAsync(link);
            await _context.SaveChangesAsync();
        }
  }
}