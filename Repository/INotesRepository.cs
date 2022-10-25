using Notes.Models;

namespace Notes.Repository
{
    public interface INotesRepository
    {
        public Task Save(Note note);
    }
}