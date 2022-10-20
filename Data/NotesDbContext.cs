using Microsoft.EntityFrameworkCore;
using Notes.Models;

namespace Notes.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }
}